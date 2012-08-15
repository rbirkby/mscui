//-----------------------------------------------------------------------
// <copyright file="TimeInputClientState.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>03-Jan-2007</date>
// <summary>Represents the state sent back and forth between client and server</summary>
//-----------------------------------------------------------------------

//Although the control currently support both 24 hour and 12 hour clock the use of 12 hour clock is NOT supported 
//for safety critical environments, and the ISV has a responsibility to use the controls in a manner appropriate to 
//the clinical context.

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NhsCui.Toolkit.DateAndTime;

    /// <summary>‍</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "FxCop gets it wrong again...")]
    internal class TimeInputClientState
    {
        /// <summary>‍</summary>
        private bool allowApproximate;

        /// <summary>‍
        /// Specifies whether the AM/PM suffix should be included
        /// </summary>
        private bool displaySeconds;

        /// <summary>‍
        /// Specifies whether hours should be displayed as 12 hour or 24 hour
        /// </summary>
        private bool display12Hour;

        /// <summary>‍
        /// Specifies whether the AM/PM suffix should be included
        /// </summary>
        private bool displayAMPM;

        /// <summary>‍</summary>
        private NhsTime value;

        /// <summary>‍</summary>
        private int functionality;

        /// <summary>Checkbox css class</summary>
        private string checkBoxCssClass;

        /// <summary>‍</summary>
        public bool AllowApproximate
        {
            get { return this.allowApproximate; }
            set { this.allowApproximate = value; }
        }

        /// <summary>‍
        /// Specifies whether the AM/PM suffix should be included
        /// </summary>
        public bool DisplaySeconds
        {
            get
            {
                return this.displaySeconds;
            }

            set
            { 
                this.displaySeconds = value; 
            }
        }

        /// <summary>‍
        /// Specifies whether hours should be displayed as 12 hour or 24 hour
        /// </summary>
        public bool Display12Hour
        {
            get
            {
                return this.display12Hour;
            }

            set
            {
                this.display12Hour = value;
            }
        }

        /// <summary>‍
        /// Specifies whether the AM/PM suffix should be included
        /// </summary>
        public bool DisplayAMPM
        {
            get
            {
                return this.displayAMPM;
            }

            set
            {
                this.displayAMPM = value;
            }
        }

        /// <summary>‍</summary>
        public NhsTime Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>Checkbox css class </summary>
        public string CheckBoxCssClass
        {
            get { return this.checkBoxCssClass; }
            set { this.checkBoxCssClass = value; }
        }

        /// <summary>‍</summary>
        public int Functionality
        {
            get { return this.functionality; }
            set { this.functionality = value; }
        }
    }
}
