///////////////////////////////////////////////////////////////////////////////////////////////
//
//    This File is Part of the CallButler Open Source PBX (http://www.codeplex.com/callbutler
//
//    Copyright (c) 2005-2008, Jim Heising
//    All rights reserved.
//
//    Redistribution and use in source and binary forms, with or without modification,
//    are permitted provided that the following conditions are met:
//
//    * Redistributions of source code must retain the above copyright notice,
//      this list of conditions and the following disclaimer.
//
//    * Redistributions in binary form must reproduce the above copyright notice,
//      this list of conditions and the following disclaimer in the documentation and/or
//      other materials provided with the distribution.
//
//    * Neither the name of Jim Heising nor the names of its contributors may be
//      used to endorse or promote products derived from this software without specific prior
//      written permission.
//
//    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//    ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//    WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
//    IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
//    INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
//    NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//    PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
//    WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
//    ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
//    POSSIBILITY OF SUCH DAMAGE.
//
///////////////////////////////////////////////////////////////////////////////////////////////

/*=====================================================================

  File:        PipeConnection.cs

=====================================================================*/

using System;
using System.Security;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization;
using System.IO;
using System.Text;
using System.Collections;
using System.Threading;

namespace NET.Remoting
{

    internal class PipeConnectionCookie
    {
        public PipeConnection pipe;
        public IClientChannelSinkStack sinkStack;
    }

    internal class PipeConnection : IDisposable
    {
        private int          _handle;
        private String       _pipeName;

        private int         _lastTimeAccessed=-1;
        private static int  _defaultAgeLastTimeAccessed=30000; // 30 seconds

        private const String  ChannelScheme = "pipe";

        // Output information...
        private int          _read;
        private int          _written;
        private MemoryStream _stream;
        private BinaryWriter _writer;
        private BinaryReader _reader;

        public void UpdateLastTimeAccessed()
        {
            _lastTimeAccessed = Environment.TickCount;
        }

        public bool IsConnectionStale()
        {
            long now  = Environment.TickCount;
            long result = now - _lastTimeAccessed;

            // Did we wrap 24.9 days ?
            if (result < 0)
                result = (Int32.MaxValue - _lastTimeAccessed) + now;

            // is stale connection
            if (result > _defaultAgeLastTimeAccessed)
                return true;
            else
                return false;
        }


        private void Connect()
        {
            while (true)
            {
                _handle = PipeNative.CreateFile(_pipeName, 
                                     PipeNative.GENERIC_READ | PipeNative.GENERIC_WRITE, 
                                     0,
                                     null, 
                                     PipeNative.OPEN_EXISTING, 
                                     0, 
                                     0);

                if(_handle != PipeNative.INVALID_HANDLE_VALUE)
                    return;
            
                if(PipeNative.GetLastError() != PipeNative.ERROR_PIPE_BUSY)
                {
                    throw new PipeIOException("Could not open pipe: " + _pipeName);
                }

                if(!PipeNative.WaitNamedPipe(_pipeName, 20000))
                    throw new PipeIOException("Specified pipe was over-burdened: " + _pipeName);
            }
        }

        private void Create(IntPtr pipeSecurityDescriptor)
        {
            _handle = PipeNative.CreateNamedPipe(_pipeName, 
                                      PipeNative.PIPE_ACCESS_DUPLEX, 
                                      PipeNative.PIPE_TYPE_BYTE | PipeNative.PIPE_READMODE_BYTE | PipeNative.PIPE_WAIT,
                                      PipeNative.PIPE_UNLIMITED_INSTANCES,
                                      8192,
                                      8192,
                                      PipeNative.NMPWAIT_WAIT_FOREVER,
                                      pipeSecurityDescriptor);
            
            if (_handle == PipeNative.INVALID_HANDLE_VALUE) 
            {
                throw new PipeIOException("Could not create the pipe (" + _pipeName + ") - os returned " + PipeNative.GetLastError());
            }
        }

        public PipeConnection(String pipeName, bool create, IntPtr pipeSecurityDescriptor)
        {
            //_pipeName = pipeName;
            _pipeName = "\\\\.\\pipe\\" + pipeName;

            // Try to open the named pipe:
            if(create)
            {
                Create(pipeSecurityDescriptor);
            }
            else
            {
                Connect();
            }

            _read = _written = 0;
        }

        public PipeConnection(int handle)
        {
            _handle = handle;
        }

        //
        // Sending (writing) functions
        //
        public void BeginWriteMessage()
        {
            //_stream = new MemoryStream(4096);
            _stream = new MemoryStream(128);
            _writer = new BinaryWriter(_stream, Encoding.UTF8);
        }

        public void Write(byte[] buffer)
        {
            Write(buffer, buffer.Length);
        }

        public void Write(byte[] buffer, int length)
        {
            DBG.Info(null, _handle + "> Write buffer " + length);            

            _writer.Write(length);
            _writer.Write(buffer);

            _written += length;

            DBG.Info(null, _handle + ">\t\twritten = " + _written);
            DBG.Info(null, _handle + "> Write finished.");
        }

        public void Write(ushort val)
        {
            DBG.Info(null, _handle + "> Write ushort " + val);
            _writer.Write(val);
        }

        public void Write(int val)
        {
            DBG.Info(null, _handle + "> Write int " + val);
            _writer.Write(val);
        }

        public void Write(String str)
        {
            DBG.Info(null, _handle + "> Write string " + str);

            if (str == null)
            {
                str = "";
            }

            _writer.Write(str); // Length prefixed string
        }


        public void Write(Stream str)
        {
            DBG.Info(null, _handle + "> Write stream " + str.Length);

            //int  chunk = 4096;
            int  chunk = 128;
            long len   = chunk;
            byte[] buffer = new byte[chunk];

            _writer.Write((int)(str.Length));
            
            while (len != 0)
            {
                len = str.Read(buffer, 0, chunk);
                if (len > 0)
                {
                    _writer.Write(buffer);
                }
            }
        }

        public void EndWriteMessage()
        {
            // This is just a temporary and ignored array used on the WriteFile calls.
            byte[] _numReadWritten = new byte[4];

            _writer.Flush();
            
            uint len = (uint)_stream.Length;
            
            //TODO: Replace BitConverter
            bool fOk = PipeNative.WriteFile(_handle,
                                 BitConverter.GetBytes(len), 
                                 4,
                                 _numReadWritten,
                                 0);           
            if (fOk)
            {
                fOk = PipeNative.WriteFile(_handle, 
                                _stream.GetBuffer(),
                                len,
                                _numReadWritten,
                                0);
            }

            if (!fOk)
            {
                throw new PipeIOException("Error writing to pipe " + _handle + ": error " + PipeNative.GetLastError());
            }

            Flush();

            _stream = null;
            _writer = null;
        }

        public void BeginReadMessage()
        {
            // This is just a temporary and ignored array used on the WriteFile calls.
            byte[] _numReadWritten = new byte[4];

            // TODO: Fix the pinvoke to eliminate these byte arrays
            byte[] intBytes = new byte[4];
            byte[] msgBytes = null;
            int len;

            bool fOk = PipeNative.ReadFile(_handle,
                                intBytes,
                                4,
                                _numReadWritten,
                                0);
            if (fOk)
            {
                len = BitConverter.ToInt32(intBytes, 0);

                msgBytes = new byte[len];

                fOk = PipeNative.ReadFile(_handle,
                               msgBytes,
                               (uint)len,
                               _numReadWritten,
                               0);                               
            }
            
            if (!fOk)
            {
                throw new PipeIOException("Error reading from pipe " + _handle + ": error " + PipeNative.GetLastError());
            }

            _stream = new MemoryStream(msgBytes, false);
         
                if (_stream.CanRead)
                {
                    string s = GetStringFromStream(_stream);
                    if (s.Contains("Exception"))
                    {
                        RemotingException rex = new RemotingException(s);
                        throw rex;


                    }
                }
        
            _stream.Position = 0;
            _reader = new BinaryReader(_stream, Encoding.UTF8);
        }

        public string GetStringFromStream(Stream stream)
        {
            try
            {
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
            catch
            {
                return "";
            }
            
        }


        public Stream ReadStream()
        {
            DBG.Info(null, _handle + "> Read stream...");

            int length = _reader.ReadInt32();
            DBG.Info(null, _handle + "> Read stream len " + length);

            byte[] buffer = _reader.ReadBytes(length);
            
            MemoryStream ms = new MemoryStream(buffer, false);
            return(ms);
        }

        public byte[] ReadBytes(int cb)
        {
            DBG.Info(null, _handle + "> Reading bytes len " + cb);
            
            return _reader.ReadBytes(cb);
        }

        public ushort ReadUShort()
        {
            DBG.Info(null, _handle + "> Reading ushort...");
            return _reader.ReadUInt16();
        }


        public int ReadInt()
        {
            DBG.Info(null, _handle + "> Reading int...");
            return _reader.ReadInt32();
        }

        public String ReadString()
        {
            DBG.Info(null, _handle + "> Reading string...");
            return _reader.ReadString();
        }

        public void EndReadMessage()
        {
            _stream = null;
            _reader = null;
        }

        //private const String _pipe = "pipe://";

        public const ushort HeaderMarker    = 0xFFA1;
        public const ushort HeaderEndMarker = 0xFFA2;

        internal static String Parse(String url, out String objuri)
        {       
            String pipename = null;
            DBG.Info(null, "Parse: IN: url = " + url);

            // Format is pipe://pipename/objname

            //TODO Look for pipe: scheme prefix
            String urlCompare = url.ToLower();

            // Starts with pipe:// ?
            if (urlCompare.StartsWith(ChannelScheme) == false)
            {
                objuri = null;
                return null;
            }

            // Parse out the ://
            int start = url.IndexOf("://"); 
            start += 3;

            int end   = url.IndexOf("/", start);
            if(end != -1)
            {
                pipename = url.Substring(start, end - start);       
                objuri = url.Substring(end+1);
            }
            else
            {
                pipename = url.Substring(start);
                objuri = null;
            }

            DBG.Info(null, "Parse: OUT: pipename = " + pipename + ", objuri = " + objuri);

            
             
            return objuri;
        }

        public void WriteHeaders(String uri, ITransportHeaders head)
        {
            // Write the URI
            Write(uri);

            // Since we cannot count the headers, just begin writing counted strings.
            // We'll write a terminator marker at the end.        
            
            foreach (DictionaryEntry header in head)
            {
                String headerName = (String)header.Key;
                
                //if (!headerName.StartsWith("__")) // exclude special headers
                if ((headerName.Length < 2) || ((headerName[0] != '_') && (headerName[1] != '_')))
                {
                    Write(PipeConnection.HeaderMarker);
                
                    Write(headerName);
                    Write(header.Value.ToString());                  
                }
            }       
            
            Write(PipeConnection.HeaderEndMarker);
        }

        public ITransportHeaders ReadHeaders()
        {
            TransportHeaders headers = new TransportHeaders();

            // read uri (and make sure that no channel specific data is present)
            String uri = ReadString();
            
            if(uri != "" && uri != null)
            {
                String chanuri, objuri;
                chanuri = PipeConnection.Parse(uri, out objuri);
                if (chanuri == null)
                {
                    objuri = uri;
                }                                    
                headers[CommonTransportKeys.RequestUri] = objuri;
            }

            // read to end of headers  
            ushort marker = ReadUShort();
            while(marker == HeaderMarker)
            {
                String hname  = ReadString();
                String hvalue = ReadString();

                headers[hname] = hvalue;

                marker = ReadUShort();
            }

            return headers;
        }

        //////////////////////////////////////////////////////////////

        public bool WaitForConnect()
        {
            bool fRet = PipeNative.ConnectNamedPipe(_handle, null);
            
            return fRet ? true : (PipeNative.GetLastError() == PipeNative.ERROR_PIPE_CONNECTED);
        }

        public void Flush()
        {
            PipeNative.FlushFileBuffers(_handle);
        }
        
        public void Dispose()
        {
            if (_handle != 0)
            {
                PipeNative.FlushFileBuffers(_handle); 
                PipeNative.DisconnectNamedPipe(_handle); 
                PipeNative.CloseHandle(_handle); 
                
                _handle = 0;

                DBG.Info("PipeChannel.Dispose", _pipeName);
            }
        }

        public uint GetLastError()
        {
            return PipeNative.GetLastError();
        }

    } // PipeConnection


    //////////////////////////////////////////////////////////////////////
    // Pool for PipeConnections
    //////////////////////////////////////////////////////////////////////

    internal class PipeConnectionPoolManager
    {
        static bool initialized=false;
        static Timer timer;
        static TimerCallback timerDelegate;
        static int timerDueTime = 30000;
        //static int timePeriod = 30000;

        static Hashtable _poolInstances = new Hashtable();

        private static void Init()
        {
            lock(_poolInstances)
            {
                if (initialized == false)
                {
                    // Setup timer
                    timerDelegate = new TimerCallback(PipeConnectionPoolManagerCallback);
                    timer = new Timer(timerDelegate, null ,timerDueTime, 0);

                    initialized = true;
                }
            }
        }

        public static void Cleanup()
        {
            // Cleanout cache items

            // Stop timer
            timer.Dispose();
        }

        private static void PipeConnectionPoolManagerCallback(Object state)
        {
            DBG.Info(null, "PipeConnectionPoolManagerCallback");
            lock(_poolInstances)
            {
                foreach(DictionaryEntry entry in _poolInstances)
                {
                    PipeConnectionPool pool = (PipeConnectionPool)entry.Value;

                    lock(pool)
                    {
                        pool.CloseStaleConnections();
                    }
                }
            }

            // Requeue timer

            timer = new Timer(timerDelegate, null, timerDueTime, 0);
        }

        //////////////////////////////////////////////////////////////

        public static PipeConnectionPool LookupPool(String key)
        {
            PipeConnectionPool  pool;

            if (initialized == false)
                Init();

            lock(_poolInstances)
            {
                pool = (PipeConnectionPool)_poolInstances[key];

                if (pool == null)
                {
                    pool = new PipeConnectionPool(key);
                     _poolInstances[key] = pool;
                }
            }

            return pool;
        }
    }

    internal class PipeConnectionPool : IDisposable
    {
        String _key;
        public ArrayList _list;

        public PipeConnectionPool(String key)
        { 
            _key = key;
            _list = new ArrayList();
        }

        public void Dispose()
        {
            // Cleanout cache items
        }

        ////////////////////////////////////////////////////////////

        public void CloseStaleConnections()
        {
            ArrayList _activeConnections = new ArrayList();
            foreach(Object obj in _list)
            {
                PipeConnection _pipe = (PipeConnection)obj;

                if (_pipe.IsConnectionStale() == true)
                {
                    DBG.Info(null, "Disposing of stale connection");
                    _pipe.Dispose();
                    _pipe = null;
                }
                else
                {
                    _activeConnections.Add(_pipe);
                }

            }

            _list = _activeConnections;
        }


        public Object Obtain()
        {
            Object obj=null;

            lock(_list)
            {
                int count = _list.Count;
                if ( count > 0)
                {
                  obj = _list[count-1];
                  _list.RemoveAt(count-1);
                }
            }

            return obj;
        }

        public void ReturnToPool(Object obj)
        {
            lock(_list)
            {
                PipeConnection _pipe = (PipeConnection)obj;
                _pipe.UpdateLastTimeAccessed();
                _list.Add(obj);
            }
        }

    }

    //
    // Imported namedpipe entry points for p/invoke into native code.
    //
    [StructLayout(LayoutKind.Sequential)]
    internal class SecurityAttributes
    {
    }


    [StructLayout(LayoutKind.Sequential)]
    internal class Overlapped
    {
    }

    [SuppressUnmanagedCodeSecurity]
    internal class PipeNative 
    {
        public const uint PIPE_ACCESS_OUTBOUND = 0x00000002;
        public const uint PIPE_ACCESS_DUPLEX = 0x00000003;
        public const uint PIPE_ACCESS_INBOUND = 0x00000001;
        
        public const uint PIPE_WAIT = 0x00000000;
        public const uint PIPE_NOWAIT = 0x00000001;
        public const uint PIPE_READMODE_BYTE = 0x00000000;
        public const uint PIPE_READMODE_MESSAGE = 0x00000002;
        public const uint PIPE_TYPE_BYTE = 0x00000000;
        public const uint PIPE_TYPE_MESSAGE = 0x00000004;
        
        public const uint PIPE_CLIENT_END = 0x00000000;
        public const uint PIPE_SERVER_END = 0x00000001;
        
        public const uint PIPE_UNLIMITED_INSTANCES = 255;
        
        public const uint NMPWAIT_WAIT_FOREVER = 0xffffffff;
        public const uint NMPWAIT_NOWAIT = 0x00000001;
        public const uint NMPWAIT_USE_DEFAULT_WAIT = 0x00000000;
        
        public const uint GENERIC_READ = (0x80000000);
        public const uint GENERIC_WRITE = (0x40000000);
        public const uint GENERIC_EXECUTE = (0x20000000);
        public const uint GENERIC_ALL = (0x10000000);
        
        public const uint CREATE_NEW        = 1;
        public const uint CREATE_ALWAYS     = 2;
        public const uint OPEN_EXISTING     = 3;
        public const uint OPEN_ALWAYS       = 4;
        public const uint TRUNCATE_EXISTING = 5;
        
        public const int INVALID_HANDLE_VALUE = -1;
        public const ulong ERROR_PIPE_BUSY = 231;
        public const ulong ERROR_NO_DATA = 232;
        public const ulong ERROR_PIPE_NOT_CONNECTED = 233;
        public const ulong ERROR_PIPE_CONNECTED = 535;
        public const ulong ERROR_PIPE_LISTENING = 536;
        
        //CreatePipe

        [DllImport("kernel32.dll")]
        public static extern int CreateNamedPipe(String lpName,							 // pipe name
                                                 uint dwOpenMode,						 // pipe open mode
                                                 uint dwPipeMode,						 // pipe-specific modes
                                                 uint nMaxInstances,						 // maximum number of instances
                                                 uint nOutBufferSize,					 // output buffer size
                                                 uint nInBufferSize,						 // input buffer size
                                                 uint nDefaultTimeOut,					 // time-out interval
                                                 //SecurityAttributes attr					 // SD
                                                 IntPtr pipeSecurityDescriptor					 // SD
                                                 );
        
        
        [DllImport("kernel32.dll")]
        public static extern bool ConnectNamedPipe(int hNamedPipe,							 // handle to named pipe
                                                    Overlapped lpOverlapped					 // overlapped structure
                                                    );
        
        
        [DllImport("kernel32.dll")]
        public static extern int CreateFile(String lpFileName,						  // file name
                                             uint dwDesiredAccess,					  // access mode
                                             uint dwShareMode,						  // share mode
                                             SecurityAttributes attr,				  // SD
                                             uint dwCreationDisposition,			  // how to create
                                             uint dwFlagsAndAttributes,				  // file attributes
                                             uint hTemplateFile);					  // handle to template file
        
        
        [DllImport("kernel32.dll")]
        public static extern bool ReadFile(int hFile,								 // handle to file
                                           byte[] lpBuffer,							 // data buffer
                                           uint nNumberOfBytesToRead,				 // number of bytes to read
                                           byte[] lpNumberOfBytesRead,				 // number of bytes read
                                           uint lpOverlapped							 // overlapped buffer
                                           );
        
        
        [DllImport("kernel32.dll")]
        public static extern bool WriteFile(
                                            int hFile,								  // handle to file
                                            byte[] lpBuffer,							  // data buffer
                                            uint nNumberOfBytesToWrite,				  // number of bytes to write
                                            byte[] lpNumberOfBytesWritten,			  // number of bytes written
                                            uint lpOverlapped						  // overlapped buffer
                                            );
        
        [DllImport("kernel32.dll")]
        public static extern bool WaitNamedPipe(String name,
                                                 int timeout);
        
        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();
        
        
        [DllImport("kernel32.dll")]
        public static extern bool FlushFileBuffers(int hFile);

        [DllImport("kernel32.dll")]
        public static extern bool DisconnectNamedPipe(int hNamedPipe);
        
        [DllImport("kernel32.dll")]
        public static extern bool SetNamedPipeHandleState(int hNamedPipe,
                                                           ref int mode,
                                                           IntPtr cc,
                                                           IntPtr cd);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(int hObject);        

    }

    [Serializable]
    public class PipeIOException : Exception
    {
        public PipeIOException(String text) : base(text)
        {
        }

        protected PipeIOException(SerializationInfo info, StreamingContext context) : base(info, context) 
        {
        }

    }

	internal class DBG
	{
		[System.Diagnostics.Conditional("_DEBUG")]
        public static void Info(Object sw, String msg)
        {
            //Console.WriteLine("{0} {1}", sw, msg);
        }
	}
}










