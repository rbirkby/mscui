//-----------------------------------------------------------------------
// <copyright file="DateInputClientState.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NhsCui.Toolkit.DateAndTime;
    using AjaxControlToolkit;

    /// <summary>‍</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "FxCop gets it wrong again...")]
    internal class DateInputClientState
    {
        /// <summary>nhs date value as string</summary>
        private NhsDate value;

        /// <summary>
        /// Calendar position.
        /// </summary>
        private PositioningMode calendarPosition = PositioningMode.BottomLeft;

        /// <summary>whether to show approx checkbox</summary>
        private bool allowApproximate;

        /// <summary>whether date is approximate</summary>
        private bool dateIsApproximate;

        /// <summary>whether to enable complex features or not</summary>
        private int functionality = 1;

        /// <summary>whether to display Yesterday, Today, etc</summary>
        private bool displayDateAsText;
        
        /// <summary>whether to display day of week</summary>
        private bool displayDayOfWeek;

        /// <summary>Watermark text to appear when the control has no value</summary>
        private string watermarkText;

        /// <summary>Watermark css class</summary>
        private string watermarkCssClass;

        /// <summary>Checkbox css class</summary>
        private string checkBoxCssClass;

        /// <summary>nhs date</summary>
        public NhsDate Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        /// <summary>whether to show approx checkbox</summary>
        public bool AllowApproximate
        {
            get { return this.allowApproximate; }
            set { this.allowApproximate = value; }
        }

        /// <summary>whether to enable complex features or not</summary>
        public int Functionality
        {
            get { return this.functionality; }
            set { this.functionality = value; }
        }

        /// <summary>whether to display Yesterday, Today, etc</summary>
        public bool DisplayDateAsText
        {
            get { return this.displayDateAsText; }
            set { this.displayDateAsText = value; }
        }

        /// <summary>whether date is approximate</summary>
        public bool DateIsApproximate
        {
            get { return this.dateIsApproximate; }
            set { this.dateIsApproximate = value; }
        }

        /// <summary>whether to display day of week</summary>
        public bool DisplayDayOfWeek
        {
            get { return this.displayDayOfWeek; }
            set { this.displayDayOfWeek = value; }
        }

        /// <summary>Watermark text to appear when the control has no value</summary>
        public string WatermarkText
        {
           get { return this.watermarkText; }
           set { this.watermarkText = value; }
        }
        
        /// <summary>Checkbox css class </summary>
        public string CheckBoxCssClass
        {
            get { return this.checkBoxCssClass; }
            set { this.checkBoxCssClass = value; }
        }

        /// <summary>Watermark css class </summary>
        public string WatermarkCssClass
        {
            get { return this.watermarkCssClass; }
            set { this.watermarkCssClass = value; }
        }

        /// <summary>
        /// Position to display the calendar
        /// </summary>
        public PositioningMode CalendarPosition
        {
            get
            {
                return this.calendarPosition;
            }

            set
            {
                this.calendarPosition = value;
            }
        }
    }
}
