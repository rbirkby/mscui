//-----------------------------------------------------------------------
// <copyright file="KeyboardHelper.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010.
// All rights reserved.
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
// <date>13-Mar-2008</date>
// <summary>Keyboard Helper class - taken from System.Windows.Controls.DataGrid source.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Windows.Input;

    /// <summary>
    /// Implementation of the KeyboardHelper class.
    /// </summary>
    internal static class KeyboardHelper
    {
        /// <summary>
        /// Gets the state of the meta key.
        /// </summary>
        /// <param name="ctrl">If set to <c>true</c> [CTRL] is pressed.</param>
        /// <param name="shift">If set to <c>true</c> [shift] is pressed.</param>
        public static void GetMetaKeyState(out bool ctrl, out bool shift)
        {
            ctrl = (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control;
            shift = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;
        }

        /// <summary>
        /// Gets the state of the meta key.
        /// </summary>
        /// <param name="ctrl">If set to <c>true</c> [CTRL] is pressed.</param>
        /// <param name="shift">If set to <c>true</c> [shift] is pressed.</param>
        /// <param name="alt">If set to <c>true</c> [alt] is pressed.</param>
        public static void GetMetaKeyState(out bool ctrl, out bool shift, out bool alt)
        {
            GetMetaKeyState(out ctrl, out shift);
            alt = (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt;
        }
    }
}
