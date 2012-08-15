//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.42
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NhsCui.Toolkit {
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
    internal class NhsNumberExceptionResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal NhsNumberExceptionResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("NhsCui.Toolkit.NhsNumberExceptionResources", typeof(NhsNumberExceptionResources).Assembly);
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
        ///   Looks up a localized string similar to {0} is not a valid value for an NHS Number. The value contains alphabetic characters..
        /// </summary>
        internal static string InvalidNhsNumberMessageAlphaChars {
            get {
                return ResourceManager.GetString("InvalidNhsNumberMessageAlphaChars", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is not a valid value for an NHS Number. The value failed a checksum calculation..
        /// </summary>
        internal static string InvalidNhsNumberMessageChecksum {
            get {
                return ResourceManager.GetString("InvalidNhsNumberMessageChecksum", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is not a valid value for an NHS Number. The value does not contain exactly 10 digits..
        /// </summary>
        internal static string InvalidNhsNumberMessageDigitCount {
            get {
                return ResourceManager.GetString("InvalidNhsNumberMessageDigitCount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is not a valid value for an NHS Number. The value consists of a single repeating digit..
        /// </summary>
        internal static string InvalidNhsNumberMessageDigitsAllSame {
            get {
                return ResourceManager.GetString("InvalidNhsNumberMessageDigitsAllSame", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is not a valid value for an NHS Number. The value could not be parsed..
        /// </summary>
        internal static string InvalidNhsNumberMessageUnknownError {
            get {
                return ResourceManager.GetString("InvalidNhsNumberMessageUnknownError", resourceCulture);
            }
        }
    }
}