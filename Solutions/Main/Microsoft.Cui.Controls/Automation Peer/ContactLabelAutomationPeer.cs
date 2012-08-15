//-----------------------------------------------------------------------
// <copyright file="ContactLabelAutomationPeer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>19-Aug-2008</date>
// <summary>Implements automation peer for the Contact label control.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Windows;
    using System.Windows.Automation.Peers;
    #endregion

    /// <summary>
    /// Implements automation peer for the contact label control.
    /// </summary>
    public sealed class ContactLabelAutomationPeer : LabelAutomationPeer
    {
        /// <summary>
        /// Creates a new instance of contact label automation peer.
        /// </summary>
        /// <param name="control">Contact label control.</param>
        public ContactLabelAutomationPeer(ContactLabel control)
            : base(control)
        {
        }

        /// <summary>
        /// Gets the display value of automation peer.
        /// </summary>
        /// <value>Display value of control.</value>
        protected override string Value
        {
            get
            {
                ContactLabel owner = Owner as ContactLabel;
                if (owner != null)
                {
                    return owner.DisplayText;                    
                }

                return null;
            }
        }

        /// <summary>
        /// This method may be called by automation clients to retrieve the class name.
        /// </summary>
        /// <returns>Automation class name.</returns>
        protected override string GetClassNameCore()
        {
            return "ContactLabel";
        }

        /// <summary>
        /// This method may be called by automation clients to retrieve the control name.
        /// </summary>
        /// <returns>Automation control name.</returns>
        protected override string GetNameCore()
        {
            return "Contact label control";
        }
    }
}
