//-----------------------------------------------------------------------
// <copyright file="MonthCalendarClientState.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>29-Oct-2007</date>
// <summary>Represents the state sent back and forth between client and server</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NhsCui.Toolkit.DateAndTime;
    using AjaxControlToolkit;

    /// <summary>Represents the state sent back and forth between client and server</summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "FxCop gets it wrong again...")]
    internal class MonthCalendarClientState
    {
        /// <summary>nhs date value as string</summary>
        private NhsDate value;

        /// <summary>
        /// headerClass className value
        /// </summary>
        private string headerClass;

        /// <summary>
        /// dayHeaderClass className value
        /// </summary>
        private string dayHeaderClass;

        /// <summary>
        /// dateInputClass className value
        /// </summary>
        private string dateInputClass;

        /// <summary>nhs date</summary>
        public NhsDate Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// The CSS Class for the header section of the control
        /// </summary>
        public string HeaderClass
        {
            get
            {
                return this.headerClass;
            }

            set
            {
                this.headerClass = value;
            }
        }

        /// <summary>
        /// The CSS Class for the day header section of the control
        /// </summary>
        public string DayHeaderClass
        {
            get
            {
                return this.dayHeaderClass;
            }

            set
            {
                this.dayHeaderClass = value;
            }
        }

        /// <summary>
        /// The CSS Class for the dates section of the control
        /// </summary>
        public string DateInputClass
        {
            get
            {
                return this.dateInputClass;
            }

            set
            {
                this.dateInputClass = value;
            }
        }
    }
}
