//-----------------------------------------------------------------------
// <copyright file="AddressLabelAutomationPeer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Implements automation peer for the address label control.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Text;
    using System.Windows;
    using System.Windows.Automation.Peers;
    #endregion

    /// <summary>
    /// Implements automation peer for the address label control.
    /// </summary>
    public sealed class AddressLabelAutomationPeer : LabelAutomationPeer
    {
        /// <summary>
        /// Creates a new instance of address label automation peer.
        /// </summary>
        /// <param name="control">Address label control.</param>
        public AddressLabelAutomationPeer(AddressLabel control)
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
                AddressLabel owner = Owner as AddressLabel;
                if (owner != null)
                {
                    if (owner.AddressDisplayFormat == AddressDisplayFormat.InLine)
                    {
                        return owner.InlineAddressDisplayText;
                    }
                    else
                    {
                        // Return all address components if AddressDisplayFormat set to InForm
                        StringBuilder text = new StringBuilder();
                        if (!String.IsNullOrEmpty(owner.Address1))
                        {
                            text.AppendLine(owner.Address1);
                        }

                        if (!String.IsNullOrEmpty(owner.Address2))
                        {
                            text.AppendLine(owner.Address2);
                        }

                        if (!String.IsNullOrEmpty(owner.Address3))
                        {
                            text.AppendLine(owner.Address3);
                        }

                        if (!String.IsNullOrEmpty(owner.Town))
                        {
                            text.AppendLine(owner.Town);
                        }

                        if (!String.IsNullOrEmpty(owner.County))
                        {
                            text.AppendLine(owner.County);
                        }

                        if (!String.IsNullOrEmpty(owner.Postcode))
                        {
                            text.AppendLine(owner.Postcode);
                        }

                        if (!String.IsNullOrEmpty(owner.Country))
                        {
                            text.Append(owner.Country);
                        }

                        return text.ToString();
                    }
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
            return "AddressLabel";
        }

        /// <summary>
        /// This method may be called by automation clients to retrieve the control name.
        /// </summary>
        /// <returns>Automation control name.</returns>
        protected override string GetNameCore()
        {
            return "Address label control";
        }
    }
}
