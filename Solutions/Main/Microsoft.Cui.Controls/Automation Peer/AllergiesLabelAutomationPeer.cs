//-----------------------------------------------------------------------
// <copyright file="AllergiesLabelAutomationPeer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>28-Apr-2009</date>
// <summary>Implements automation peer for the allergies label control.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Globalization;
    using System.Text;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using Microsoft.Cui.Controls.Common.DateAndTime;
    #endregion

    /// <summary>
    /// Implements automation peer for the allergies label control.
    /// </summary>
    public sealed class AllergiesLabelAutomationPeer : LabelAutomationPeer
    {
        /// <summary>
        /// Creates a new instance of allergies label automation peer.
        /// </summary>
        /// <param name="control">Allergies label control.</param>
        public AllergiesLabelAutomationPeer(AllergiesLabel control)
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
                AllergiesLabel owner = Owner as AllergiesLabel;
                if (owner != null)
                {
                    StringBuilder text = new StringBuilder();
                    AllergyCollection allergies = owner.DisplayItems;
                    foreach (Allergy allergy in allergies)
                    {
                        text.Append(allergy.AllergyName);
                        text.Append(" ");
                        text.Append(Microsoft.Cui.Controls.Common.DateAndTime.CuiDate.ParseExact(allergy.LastUpdatedOn.ToShortDateString(), CultureInfo.CurrentCulture).ToString());
                        text.AppendLine();
                    }

                    return text.ToString();
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
            return "AllergiesLabel";
        }

        /// <summary>
        /// This method may be called by automation clients to retrieve the control name.
        /// </summary>
        /// <returns>Automation control name.</returns>
        protected override string GetNameCore()
        {
            return "Allergies Label control";
        }
    }
}
