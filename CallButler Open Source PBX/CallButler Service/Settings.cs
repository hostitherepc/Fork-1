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

namespace CallButler.Service.Properties
{
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    [System.Configuration.SettingsProvider(typeof(WOSI.Utilities.WOSISettingsProvider))]
    internal sealed partial class Settings
    {
        public Settings()
        {
        }

        public string[] AvailableProductIDs
        {
            get
            {
#if(LIVE_MANAGED_BUILD)
                return new string[] { "CB-LIVEM-1" };
#endif
#if(SIP_BUILD)
                return new string[] { "CB-FREE-1", "CB-PRO-1", "CB-ULM-1B", "CB-ENT-1" };
#endif
#if(SKYPE_BUILD)
                return new string[] { "CB-SKYPEPRO-1", "CB-SKYPEFREE-1"/*, "CB-SKYPEULM-1"*/};
#endif
            }
        }

        public string[] AvailableProductDescriptions
        {
            get
            {
/*#if(LIVE_MANAGED_BUILD)
                return new string[] { Services.SkinService.ReplaceProductName("CallButler Live") };
#endif*/
#if(SIP_BUILD)
                return new string[] { Services.PrivateLabelService.ReplaceProductName("CallButler Free"), Services.PrivateLabelService.ReplaceProductName("CallButler Professional"), Services.PrivateLabelService.ReplaceProductName("CallButler Unlimited"), Services.PrivateLabelService.ReplaceProductName("CallButler Enterprise") };
#endif
#if(SKYPE_BUILD)
                return new string[] { "CallButler Pro for Skype", "CallButler Free for Skype"/*, "CallButler Skype Unlimited (BETA)"*/};
#endif
            }
        }

        public string ProductDescription
        {
            get
            {
                if (ProductID != null && ProductID.Length > 0)
                {
                    for(int index = 0; index < AvailableProductIDs.Length; index++)
                    {
                        if (AvailableProductIDs[index] == ProductID)
                            return AvailableProductDescriptions[index];
                    }
                }
#if(LIVE_MANAGED_BUILD)
                return Services.SkinService.ReplaceProductName("CallButler Live");
#endif
#if(SIP_BUILD)
                return Services.PrivateLabelService.ReplaceProductName("CallButler");
#endif
#if(SKYPE_BUILD)
                return "CallButler for Skype";
#endif
            }
        }

        public string TelephoneNumberDescription
        {
            get
            {
#if(SKYPE_BUILD)
                return "Skype Name or Telephone Number";
#else
                return "a Telephone Number";
#endif
            }
        }

        public bool IsFreeVersion
        {
            get
            {
                if(ProductID == "CB-FREE-1")
                    return true;
                else
                    return false;
            }
        }

        public string AdditionalCopyrightNotice
        {
            get
            {
#if(SKYPE_BUILD)
                return "This product is certified by Skype.";
#endif
                return "";
            }
        }

        public CallButler.Telecom.TelecomProviders TelecomProviderType
        {
            get
            {
#if(SKYPE_BUILD)
                if(ProductID == "CB-SKYPEULM-1")
                    return CallButler.Telecom.TelecomProviders.SkypeUnlimitedTelecomProvider;
                else
                    return CallButler.Telecom.TelecomProviders.SkypeTelecomProvider;
#endif
#if(SIP_BUILD || LIVE_MANAGED_BUILD)
                return CallButler.Telecom.TelecomProviders.inTELIPhoneTelecomProvider;
#endif
            }
        }

        public bool UseSleepForAnswerDelay
        {
            get
            {
#if(SKYPE_BUILD)
                return true;
#else
                return false;
#endif
            }
        }

        public string SplashInfo
        {
            get
            {
                if (Default.ProductID == "CB-FREE-1")
                {
                    return Resources.FreeInfo;
                }

                return null;
            }
        }

        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e)
        {
            // Add code to handle the SettingChangingEvent event here.
        }

        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Add code to handle the SettingsSaving event here.
        }
    }
}
