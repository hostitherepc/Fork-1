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
using System.Data;

namespace CallButler.Manager.Plugin
{
    public abstract class CallButlerManagementProviderPlugin : CallButlerManagementPlugin
    {
        public virtual ProviderData AddNewProvider()
        {
            return null;
        }

        public virtual ProviderData EditProvider(ProviderData providerData)
        {
            return providerData;
        }
    }

    public abstract class VoIPProviderBase
    {
        public virtual Guid ServicePluginID
        {
            get
            {
                return System.Guid.Empty;
            }
        }

        public virtual bool IsTrialAllowed(string emailAddress, string ipAddress)
        {
            return false;
        }

        public virtual DataSet GetPlans()
        {
            return null;
        }

        public virtual DataSet GetPlanInformation(int planID)
        {
            return null;
        }

        public virtual string[] GetTollFreeNumbers()
        {
            return null;
        }

        public virtual string[] GetStates()
        {
            return null;
        }

        public virtual string[] GetCities(string areaCode)
        {
            return null;
        }

        public virtual string[] GetAreaCodes(string stateCode)
        {
            return null;
        }

        public virtual string[] GetTelephoneNumbers(string stateCode, string city)
        {
            return null;
        }

        public virtual ProviderData GetExistingAccount(string username, string password)
        {
            return null;
        }

        public virtual VoIPTransaction CreateAccount(string firstname, string lastname, string username, string password, string vpassword, string emailAddress, string address, string address2, string city, string state, string zipCode, string country, string dayphone, string planNumber, string ratePlan)
        {
            return null;
        }

        public virtual VoIPTransaction CreateTrialAccount(string firstname, string lastname, string username, string emailAddress, string country, string dayphone)
        {
            return null;
        }

        public virtual ProviderData SendPayment(VoIPTransaction transaction, string cardFirstName, string cardLastName, string cardNumber, string cardExpiration, string cvv2, string cardAddress, string cardZip, string cardType)
        {
            return null;
        }

        public virtual void SendActivationEmail(string firstname, string lastname, string emailAddress, string activationCode)
        {

        }

        public virtual ProviderData ConvertTrialAccount(string newPlan, string newNumber, string username, string password, string accountID, double amountDue, string cardFirstName, string cardLastName, string cardNumber, string cardExpiration, string cvv2, string cardAddress, string cardZip, string cardType)
        {
            return null;
        }

        public virtual ProviderData SendTrialPayment(VoIPTransaction tx)
        {
            return null;
        }
    }

    public class VoIPTransaction
    {
        public string AccountID;

        public string SessionKey;

        public string MinimumDue;

        public string Username;

        public string Password;

        public string TelephoneNumber;

        public VoIPTransaction() { }
    }

    public class ProviderData
    {
        public string Name;
        public string DisplayName;
        public string Username;
        public string AuthUsername;
        public string AuthPassword;
        public string Domain;
        public string SIPProxy;
        public string SIPRegistrar;
        public int Expires;
        public bool IsEnabled;
        public string Status;
        public bool EnableRegistration;
        public string ExtraData;
        public string ActivationCode;
        public string TelephoneNumber;
        public string AccountID;
        public bool SupressOutboundUsername;
        public string OutboundUsernamePrefix;
    }
}
