﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YahtzeeTDD {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("YahtzeeTDD.Strings", typeof(Strings).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dice tried to save an invalid number..
        /// </summary>
        public static string DiceNumberOutOfRange {
            get {
                return ResourceManager.GetString("DiceNumberOutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ╚═══════════╝.
        /// </summary>
        public static string LogoBottom {
            get {
                return ResourceManager.GetString("LogoBottom", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ║TDD Yahtzee║.
        /// </summary>
        public static string LogoText {
            get {
                return ResourceManager.GetString("LogoText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ╔═══════════╗.
        /// </summary>
        public static string LogoTop {
            get {
                return ResourceManager.GetString("LogoTop", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [R]oll, [C]heck Score, [S]ave Die, [A]dd Score.
        /// </summary>
        public static string PlayingCommands {
            get {
                return ResourceManager.GetString("PlayingCommands", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [1-5] Save or unsave die.
        /// </summary>
        public static string SaveDieCommands {
            get {
                return ResourceManager.GetString("SaveDieCommands", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [1-15] Save score.
        /// </summary>
        public static string SaveScoreCommands {
            get {
                return ResourceManager.GetString("SaveScoreCommands", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [N]ew Game, [Q]uit.
        /// </summary>
        public static string StandardCommands {
            get {
                return ResourceManager.GetString("StandardCommands", resourceCulture);
            }
        }
    }
}
