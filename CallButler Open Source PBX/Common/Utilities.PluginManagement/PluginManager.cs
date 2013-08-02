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
using System.IO;
using System.Reflection;

namespace Utilities.PluginManagement
{
    public class PluginEventArgs : EventArgs
    {
        public object Plugin;
        public bool Cancel = false;

        public PluginEventArgs(object plugin)
        {
            this.Plugin = plugin;
        }
    }

    public class PluginManager
    {
        public event EventHandler<PluginEventArgs> PluginLoaded;

        private List<Type> pluginTypes;
        private List<object> plugins;

        public PluginManager()
        {
            pluginTypes = new List<Type>();
            plugins = new List<object>();
        }

        public void LoadPlugins(string pluginDirectory, string filter, bool useSubDirectories, params Type[] pluginTypes)
        {
            UnloadPlugins();

            // Load all of the plugins in our plugin directory
            if (Directory.Exists(pluginDirectory))
            {
                SearchOption so = SearchOption.TopDirectoryOnly;

                if(useSubDirectories)
                    so = SearchOption.AllDirectories;

                string[] pluginFiles = Directory.GetFiles(pluginDirectory, filter, so);

                foreach (string pluginFile in pluginFiles)
                {
                    try
                    {
                        Assembly asm = Assembly.LoadFile(pluginFile);

                        // Loop though all the types in this assembly and get any CallButlerServicePlugin types
                        Type[] loadedTypes = asm.GetTypes();

                        foreach (Type pluginType in loadedTypes)
                        {
                            if(IsPluginOfType(pluginType, pluginTypes))
                            {
                                // Create our plugin object
                                object plugin = Activator.CreateInstance(pluginType);

                                PluginEventArgs pluginEventArgs = new PluginEventArgs(plugin);

                                if (PluginLoaded != null)
                                {
                                    PluginLoaded(this, pluginEventArgs);
                                }

                                if (!pluginEventArgs.Cancel)
                                    plugins.Add(plugin);
                                else
                                    plugin = null;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
        }

        public IList<object> Plugins
        {
            get
            {
                return plugins.AsReadOnly();
            }
        }

        public T GetFirstPluginOfType<T>()
        {
            foreach (object plugin in plugins)
            {
                if (IsPluginOfType(plugin.GetType(), typeof(T)))
                    return (T)plugin;
            }

            return default(T);
        }

        public T[] GetAllPluginsOfType<T>()
        {
            List<T> tmp = new List<T>();

            foreach (object plugin in plugins)
            {
                if (IsPluginOfType(plugin.GetType(), typeof(T)))
                    tmp.Add((T)plugin);
            }

            return tmp.ToArray();
        }

        private bool IsPluginOfType(Type pluginType, Type[] types)
        {
            foreach (Type type in types)
            {
                if (IsPluginOfType(pluginType, type))
                    return true;
            }

            return false;
        }

        private bool IsPluginOfType(Type pluginType, Type type)
        {
            Type currentType = pluginType;

            do
            {
                if (currentType == type)
                {
                    return true;
                }

                currentType = currentType.BaseType;
            }
            while (currentType != null);

            return false;
        }

        public void UnloadPlugins()
        {
            if (plugins != null)
            {
                plugins.Clear();
            }
        }
    }
}
