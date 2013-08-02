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
using global::WOSI.CallButler.Data;

namespace WOSI.CallButler.ManagementInterface
{
    public interface ICallButlerClientInterfaceServer
    {
        bool IsConnected { get;}

        #region Settings Functions
        string GetDefaultLanguage(CallButlerAuthInfo authInfo);
        
        string GetApplicationPermissions(CallButlerAuthInfo authInfo);
        
        string ProductID { get;}
       
        string LicenseName { get;set;}
        string LicenseKey { get; set;}
       
        #endregion

        #region Client Functions
        void AttachClient(CallButlerAuthInfo authInfo, ICallButlerClientInterfaceClient client);
        void DetachClient(CallButlerAuthInfo authInfo, ICallButlerClientInterfaceClient client);
        #endregion

        #region Scheduling Functions
        void PlaceScheduleReminderCall(CallButlerAuthInfo authInfo, Guid extensionID, OutlookReminder [] reminders);
        #endregion

        #region Extension Functions
        CallButlerDataset.ExtensionsDataTable GetExtensions(CallButlerAuthInfo authInfo);
        CallButlerDataset.ExtensionsDataTable GetExtensionNumber(CallButlerAuthInfo authInfo, int extensionNumber);
        CallButlerDataset.LocalizedGreetingsDataTable GetExtensionVoicemailGreeting(CallButlerAuthInfo authInfo, Guid extensionID);

        void PersistExtension(CallButlerAuthInfo authInfo, global::WOSI.CallButler.Data.CallButlerDataset.ExtensionsDataTable extension);
       
        CallButlerDataset.ExtensionContactNumbersDataTable GetExtensionContactNumbers(CallButlerAuthInfo authInfo, Guid extensionID);
        void PersistExtensionContactNumbers(CallButlerAuthInfo authInfo, global::WOSI.CallButler.Data.CallButlerDataset.ExtensionContactNumbersDataTable changes);
        #endregion

        #region Voicemail Functions
        byte[] GetVoicemailSound(CallButlerAuthInfo authInfo,Guid extensionID, Guid voicemailId);
        CallButlerDataset.VoicemailsDataTable GetVoicemails(CallButlerAuthInfo authInfo, Guid extensionID);
        void PersistVoicemail(CallButlerAuthInfo authInfo, CallButlerDataset.VoicemailsDataTable voicemail);
        void DeleteVoicemail(CallButlerAuthInfo authInfo, Guid extensionId, Guid voicemailID);
        #endregion

        #region GreetingsFunctions
        void PersistLocalizedGreeting(CallButlerAuthInfo authInfo, global::WOSI.CallButler.Data.CallButlerDataset.LocalizedGreetingsDataTable localizedGreeting);

        byte[] GetLocalizedGreetingSound(CallButlerAuthInfo authInfo, Guid greetingID, Guid localizedGreetingID);
        void PersistLocalizedGreetingSound(CallButlerAuthInfo authInfo, Guid greetingID, Guid localizedGreetingID, byte[] soundBytes);
        #endregion

        void PersistVoicemailSound(CallButlerAuthInfo authInfo, Guid extensionID, CallButlerDataset.ExtensionsDataTable extensions, byte[] soundBytes);
    }
}
