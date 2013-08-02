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

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace WOSI.Utilities
{
    public class EventUtils
    {
        private delegate void AsyncInvokeDelegate(Delegate del, object[] args);

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void FireSyncEvent(Delegate eventDelegate, object sender, EventArgs args)
        {
            if (eventDelegate != null)
            {
                object[] argsObj = new object[2];

                argsObj[0] = sender;
                argsObj[1] = args;

                Delegate[] delegates = eventDelegate.GetInvocationList();

                foreach (Delegate sink in delegates)
                {
                    //try
                    //{
                        InvokeDelegate(sink, argsObj);
                    //}
                    //catch
                    //{ }
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void FireAsyncEvent(Delegate eventDelegate, object sender, EventArgs args)
        {
            if (eventDelegate != null)
            {
                object[] argsObj = new object[2];

                argsObj[0] = sender;
                argsObj[1] = args;

                Delegate[] delegates = eventDelegate.GetInvocationList();

                AsyncInvokeDelegate invoker = new AsyncInvokeDelegate(InvokeDelegate);

                AsyncCallback cleanUp = new AsyncCallback(AsyncDelegateCleanup);

                foreach (Delegate sink in delegates)
                {
                    invoker.BeginInvoke(sink, argsObj, cleanUp, null);
                }
            }
        }

        public static void CallAsyncMethod(Delegate eventDelegate, params object[] args)
        {
            if (eventDelegate != null)
            {
                Delegate[] delegates = eventDelegate.GetInvocationList();

                AsyncInvokeDelegate invoker = new AsyncInvokeDelegate(InvokeDelegate);

                AsyncCallback cleanUp = new AsyncCallback(AsyncDelegateCleanup);

                foreach (Delegate sink in delegates)
                {
                    invoker.BeginInvoke(sink, args, cleanUp, null);
                }
            }
        }

        private static void AsyncDelegateCleanup(IAsyncResult asyncResult)
        {
            asyncResult.AsyncWaitHandle.Close();
        }

        private static void InvokeDelegate(Delegate eventDelegate, object[] args)
        {
            ISynchronizeInvoke synchronizer = eventDelegate.Target as ISynchronizeInvoke;

            if (synchronizer != null)
            {
                if (synchronizer.InvokeRequired)
                {
                    synchronizer.Invoke(eventDelegate, args);
                    return;
                }
            }

            try
            {
                eventDelegate.DynamicInvoke(args);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }
    }
}
