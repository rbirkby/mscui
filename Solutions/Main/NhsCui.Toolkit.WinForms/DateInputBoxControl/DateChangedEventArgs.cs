//-----------------------------------------------------------------------
// <copyright file="DateChangedEventArgs.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>3-May-2007</date>
// <summary>The event argument class for date changed event.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.WinForms
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Event argument class.
    /// </summary>
    public class DateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// selected date
        /// </summary>
        private DateTime selectedDate;      

        /// <summary>
        /// Specifies changes made to the different date fields.
        /// </summary>    
        private ChangedDateParts changes;       

        /// <summary>
        /// Creates a new instance of the date changed event arguments.
        /// </summary>
        public DateChangedEventArgs()
        {
            this.changes = ChangedDateParts.None;
        }

        /// <summary>
        /// Creates a new instance of the date changed event arguments.
        /// </summary>
        /// <param name="dateSelected">Date</param>
        public DateChangedEventArgs(DateTime dateSelected)
        {
            this.changes = ChangedDateParts.None;
            this.selectedDate = dateSelected;
        }

        /// <summary>
        /// The selected date.
        /// </summary>
        public DateTime SelectedDate
        {
            get
            {
                return this.selectedDate;
            }
        }

        /// <summary>
        /// Specifies changes made to the different date fields.
        /// </summary>
        public ChangedDateParts Changes
        {
            get
            {
                return this.changes;
            }

            set
            {
                this.changes = value;
            }
        }
    }  
}
