﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3074
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CallButler.ManagementPlugins.GenericSIPProviderPlugin.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class LocalizedStrings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LocalizedStrings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CallButler.ManagementPlugins.GenericSIPProviderPlugin.Properties.LocalizedStrings" +
                            "", typeof(LocalizedStrings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The test connection to your VoIP provider failed.
        ///
        ///Reason: {0}.
        /// </summary>
        internal static string ProviderEditorForm_TestFailure {
            get {
                return ResourceManager.GetString("ProviderEditorForm_TestFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Test Failure.
        /// </summary>
        internal static string ProviderEditorForm_TestFailureTitle {
            get {
                return ResourceManager.GetString("ProviderEditorForm_TestFailureTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The test connection to your VoIP provider was successful.
        ///
        ///In most cases CallButler should now be able to receive calls with this provider..
        /// </summary>
        internal static string ProviderEditorForm_TestSuccess {
            get {
                return ResourceManager.GetString("ProviderEditorForm_TestSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Test Successful.
        /// </summary>
        internal static string ProviderEditorForm_TestSuccessTitle {
            get {
                return ResourceManager.GetString("ProviderEditorForm_TestSuccessTitle", resourceCulture);
            }
        }
    }
}