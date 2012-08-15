//-----------------------------------------------------------------------
// <copyright file="DateLabelAutomationPeer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>23-June-2009</date>
// <summary>Implements automation peer for the date label control.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
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
    #endregion

    /// <summary>
    /// Implements automation peer for the date label control.
    /// </summary>
    public sealed class DateLabelAutomationPeer : LabelAutomationPeer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateLabelAutomationPeer"/> class.
        /// </summary>
        /// <param name="control">The date label control.</param>
        public DateLabelAutomationPeer(DateLabel control) : base(control)
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
                DateLabel label = Owner as DateLabel;

                return label != null ? label.DisplayValue : null;
            }
        }

        /// <summary>
        /// This method may be called by automation clients to retrieve the class name.
        /// </summary>
        /// <returns>
        /// Automation class name.
        /// </returns>
        protected override string GetClassNameCore()
        {
            return "DateLabel";
        }

        /// <summary>
        /// This method may be called by automation clients to retrieve the control name.
        /// </summary>
        /// <returns>
        /// Automation control name.
        /// </returns>
        protected override string GetNameCore()
        {
            return "Date label control";
        }
    }
}
