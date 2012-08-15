//-----------------------------------------------------------------------
// <copyright file="ShortcutKeyHelper.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010.
// All rights reserved
//
// CERTAIN PARTS OF THIS WORK CONTAIN SOFTWARE CODE THAT IS LICENSED 
// FOR USE UNDER THE MICROSOFT PUBLIC LICENSE. DISTRIBUTION, IN SOURCE CODE 
// OR OBJECT CODE FORM, OF THOSE PARTS MUST COMPLY WITH THE TERMS OF THE 
// PUBLIC LICENSE. SEE http://www.microsoft.com/opensource/licenses.mspx 
// FOR DETAILS.  
// IF YOU BRING A PATENT CLAIM AGAINST ANY CONTRIBUTOR OVER PATENTS THAT 
// YOU CLAIM ARE INFRINGED BY THE PUBLIC LICENSE SOFTWARE, YOUR PATENT 
// LICENSE FROM SUCH CONTRIBUTOR TO THE SOFTWARE ENDS AUTOMATICALLY.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>18-Sep-2009</date>
// <summary>
//      Provides helper methods for dealing with shortcut keys.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.Globalization;

    /// <summary>
    /// Provides helper methods for dealing with shortcut keys.
    /// </summary>
    public static class ShortcutKeyHelper
    {
        /// <summary>
        /// Stores the shortcut key prefix.
        /// </summary>
        public const string ShortcutKeyPrefix = "Shift + ";

        /// <summary>
        /// Stores the shorcut modifier.
        /// </summary>
        public const ModifierKeys ShortcutModifier = ModifierKeys.Shift;

        /// <summary>
        /// Gets a key from an index.
        /// </summary>
        /// <param name="index">The index to get the key from.</param>
        /// <returns>The key from the index.</returns>
        public static Key? GetKeyFromIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return Key.D1;
                case 1:
                    return Key.D2;
                case 2:
                    return Key.D3;
                case 3:
                    return Key.D4;
                case 4:
                    return Key.D5;
                case 5:
                    return Key.D6;
                case 6:
                    return Key.D7;
                case 7:
                    return Key.D8;
                case 8:
                    return Key.D9;
                case 9:
                    return Key.D0;
            }

            return null;
        }

        /// <summary>
        /// Gets the key as a string.
        /// </summary>
        /// <param name="key">The key to get as a string.</param>
        /// <returns>The string for the key.</returns>
        public static string GetKeyAsString(Key key)
        {
            switch (key)
            {
                case Key.A:
                    return "A";
                case Key.B:
                    return "B";
                case Key.C:
                    return "C";
                case Key.D:
                    return "D";
                case Key.E:
                    return "E";
                case Key.F:
                    return "F";
                case Key.G:
                    return "G";
                case Key.H:
                    return "H";
                case Key.I:
                    return "I";
                case Key.J:
                    return "J";
                case Key.K:
                    return "K";
                case Key.L:
                    return "L";
                case Key.M:
                    return "M";
                case Key.N:
                    return "N";
                case Key.O:
                    return "O";
                case Key.P:
                    return "P";
                case Key.Q:
                    return "Q";
                case Key.R:
                    return "R";
                case Key.S:
                    return "S";
                case Key.T:
                    return "T";
                case Key.U:
                    return "U";
                case Key.V:
                    return "V";
                case Key.W:
                    return "W";
                case Key.X:
                    return "X";
                case Key.Y:
                    return "Y";
                case Key.Z:
                    return "Z";
                case Key.D0:
                case Key.NumPad0:
                    return "0";
                case Key.D1:
                case Key.NumPad1:
                    return "1";
                case Key.D2:
                case Key.NumPad2:
                    return "2";
                case Key.D3:
                case Key.NumPad3:
                    return "3";
                case Key.D4:
                case Key.NumPad4:
                    return "4";
                case Key.D5:
                case Key.NumPad5:
                    return "5";
                case Key.D6:
                case Key.NumPad6:
                    return "6";
                case Key.D7:
                case Key.NumPad7:
                    return "7";
                case Key.D8:
                case Key.NumPad8:
                    return "8";
                case Key.D9:
                case Key.NumPad9:
                    return "9";
                default:
                    return null;
            }
        }
    }
}
