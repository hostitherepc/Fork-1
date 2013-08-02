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

//using System;
//using System.Collections.Generic;
//using System.Text;
////using T2.Kinesis.Gidgets;

//namespace CallButler.Service.Services
//{
//    public class ExtensionStateService
//    {
//        private Dictionary<Guid, Dictionary<StateParameterType, object>> extensionStates;
//        private Dictionary<Guid, Dictionary<int, CallInfo>> callStates;
//        private WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider;

//        public event EventHandler<ExtensionStateEventArgs> ExtensionStateChanged;
//        public event EventHandler<CallStateEventArgs> CallStateChanged;

//        public ExtensionStateService(WOSI.CallButler.Data.DataProviders.CallButlerDataProviderBase dataProvider)
//        {
//            this.dataProvider = dataProvider;
//            extensionStates = new Dictionary<Guid, Dictionary<StateParameterType, object>>();

//            callStates = new Dictionary<Guid, Dictionary<int, CallInfo>>();
//        }

//        public void UpdateCallState(Guid extensionID, CallInfo callInfo)
//        {
//            lock (callStates)
//            {
//                if (!callStates.ContainsKey(extensionID))
//                    callStates.Add(extensionID, new Dictionary<int, CallInfo>());

//                callStates[extensionID][callInfo.LineNumber] = callInfo;
//            }

//            if (CallStateChanged != null)
//                CallStateChanged(this, new CallStateEventArgs(extensionID, callInfo));
//        }

//        public void RemoveCallState(Guid extensionID, int lineNumber)
//        {
//            lock (callStates)
//            {
//                if (callStates.ContainsKey(extensionID))
//                {
//                    if (callStates[extensionID].ContainsKey(lineNumber))
//                        callStates[extensionID].Remove(lineNumber);
//                }
//            }
//        }

//        public void UpdateExtensionState(bool saveState, int extensionNumber, StateEventType eventType, params ExtensionStateParameter[] stateParams)
//        {
//            WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow extRow = GetExtensionFromNumber(extensionNumber);

//            if (extRow != null)
//                UpdateExtensionState(saveState, extRow.ExtensionID, eventType, stateParams);
//        }

//        public void UpdateExtensionState(bool saveState, Guid extensionID, StateEventType eventType, params ExtensionStateParameter[] stateParams)
//        {
//            List<ExtensionStateParameter> changedParams = new List<ExtensionStateParameter>();

//            if (!saveState)
//            {
//                changedParams.AddRange(stateParams);
//            }
//            else
//            {
//                foreach (ExtensionStateParameter stateParam in stateParams)
//                {
//                    lock (extensionStates)
//                    {
//                        if (!extensionStates.ContainsKey(extensionID))
//                            extensionStates.Add(extensionID, new Dictionary<StateParameterType, object>());

//                        // Check to see if the parameter has actually changed
//                        if (!extensionStates[extensionID].ContainsKey(stateParam.StateParameter) || extensionStates[extensionID][stateParam.StateParameter] != stateParam.StateValue)
//                            changedParams.Add(stateParam);

//                        extensionStates[extensionID][stateParam.StateParameter] = stateParam.StateValue;
//                    }
//                }
//            }

//            if (changedParams.Count > 0 && ExtensionStateChanged != null)
//            {
//                ExtensionStateChanged(this, new ExtensionStateEventArgs(eventType, extensionID, changedParams.ToArray()));
//            }
//        }

//        public ExtensionStateParameter[] GetExtensionState(Guid extensionID)
//        {
//            List<ExtensionStateParameter> stateParams = new List<ExtensionStateParameter>();

//            lock (extensionStates)
//            {
//                if (extensionStates.ContainsKey(extensionID))
//                {
//                    Dictionary<StateParameterType, object>.Enumerator dictEnum = extensionStates[extensionID].GetEnumerator();

//                    while (dictEnum.MoveNext())
//                    {
//                        stateParams.Add(new ExtensionStateParameter(dictEnum.Current.Key, dictEnum.Current.Value));
//                    }
//                }
//            }

//            return stateParams.ToArray();
//        }

//        public T GetExtensionStateValue<T>(Guid extensionID, StateParameterType parameterType, T defaultValue)
//        {
//            return (T)GetExtensionState(extensionID, parameterType, defaultValue).StateValue;
//        }

//        public ExtensionStateParameter GetExtensionState(Guid extensionID, StateParameterType parameterType, object defaultValue)
//        {
//            lock (extensionStates)
//            {
//                if (extensionStates.ContainsKey(extensionID))
//                {
//                    if (extensionStates[extensionID].ContainsKey(parameterType))
//                    {
//                        return new ExtensionStateParameter(parameterType, extensionStates[extensionID][parameterType]);
//                    }
//                }
//            }

//            return new ExtensionStateParameter(parameterType, defaultValue);
//        }

//        public ExtensionStateParameter[] GetExtensionState(Guid extensionID, params StateParameterType[] parameterTypes)
//        {
//            List<ExtensionStateParameter> stateParams = new List<ExtensionStateParameter>();

//            foreach (StateParameterType paramType in parameterTypes)
//            {
//                stateParams.Add(GetExtensionState(extensionID, paramType, null));
//            }

//            return stateParams.ToArray();
//        }

//        public void ClearState()
//        {
//        }

//        private WOSI.CallButler.Data.CallButlerDataset.ExtensionsRow GetExtensionFromNumber(int extensionNumber)
//        {
//            return dataProvider.GetExtensionNumber(Properties.Settings.Default.CustomerID, extensionNumber);
//        }
//    }
//}
