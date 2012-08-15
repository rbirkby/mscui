//-----------------------------------------------------------------------
// <copyright file="TimeSpanInputClientState.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>01-May-2007</date>
// <summary>Represents the state sent back and forth between client and server</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using NhsCui.Toolkit.DateAndTime;

    /// <summary>
    /// Represents the TimeSpanInputBox state sent back and forth between client and server
    /// </summary>
    internal class TimeSpanInputClientState
    {
        #region Member Vars

        /// <summary>NhsTimeSpan from as string</summary>
        private string from;

        /// <summary>NhsTimeSpan granularity as string</summary>
        private int granularity;

        /// <summary>NhsTimeSpan isAge as string</summary>
        private bool ageFlag;

        /// <summary>NhsTimeSpan value as string</summary>
        private string text = "";

        /// <summary>NhsTimeSpan text as string</summary>
        private string to;

        /// <summary>NhsTimeSpan threshold as string</summary>
        private int threshold;

        /// <summary>NhsTimeSpan value</summary>
        private NhsTimeSpan value;

        /// <summary>
        /// Specifies whether long or short units are displayed.
        /// </summary>
        private TimeSpanUnitLength unitLength;

        #endregion

        #region Public Properties

        /// <summary>NhsTimeSpan from as string</summary>
        public string From
        {
            get
            {
                return this.from;
            }

            set
            {
                this.from = value;
            }
        }

        /// <summary>NhsTimeSpan granularity as string</summary>
        public int Granularity
        {
            get
            {
                return this.granularity;
            }

            set
            {
                this.granularity = value;
            }
        }

        /// <summary>NhsTimeSpan isAge as string</summary>
        public bool IsAge
        {
            get
            {
                return this.ageFlag;
            }

            set
            {
                this.ageFlag = value;
            }
        }

        /// <summary>NhsTimeSpan text as string</summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
            }
        }

        /// <summary>NhsTimeSpan to as string</summary>
        public string To
        {
            get
            {
                return this.to;
            }

            set
            {
                this.to = value;
            }
        }

        /// <summary>NhsTimeSpan threshold as string</summary>
        public int Threshold
        {
            get
            {
                return this.threshold;
            }

            set
            {
                this.threshold = value;
            }
        }

        /// <summary>NhsTimeSpan value as string</summary>
        public NhsTimeSpan Value
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
        /// Specifies whether long or short units are displayed. 
        /// </summary>
        public TimeSpanUnitLength UnitLength
        {
            get
            {
                return this.unitLength;
            }

            set
            {
                this.unitLength = value;
            }
        }

        #endregion
    }
}
