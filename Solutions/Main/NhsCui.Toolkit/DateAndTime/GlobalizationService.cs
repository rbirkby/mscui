//-----------------------------------------------------------------------
// <copyright file="GlobalizationService.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary> Provides globalization information specific to the NHS. 
//</summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.DateAndTime
{
    using System;
    using System.Configuration;
    using System.Text.RegularExpressions;

    /// <summary>
    /// An internal service which provides globalization information specific to the NHS. 
    /// </summary>
    /// <remarks>
    /// Globalization values are set independently of the Regional 
    /// and Language Options dialog box. 
    /// </remarks>
    public class GlobalizationService
    {
        #region Member Variables
        /// <summary>
        /// The RFC 1766 culture identifier. 
        /// </summary>
        /// <remarks>
        /// This is read-only. 
        ///</remarks>
        private readonly string name;
        
        /// <summary>
        /// The format string to use for short dates. 
        /// </summary>
        /// <remarks>
        /// This is read-only. 
        /// </remarks>
        private readonly string shortDatePattern;
                
        /// <summary>
        /// The format string to use for short dates which include a day of the week. 
        /// </summary>
        /// <remarks>
        /// This is read-only. 
        /// </remarks>
        private readonly string shortDatePatternWithDayOfWeek;
        
        /// <summary>
        /// The format string to use for short times. 
        /// </summary>
        /// <remarks>
        /// This is read-only. 
        /// </remarks>
        private readonly string shortTimePattern;

        /// <summary>
        /// The format string to use for short times including seconds
        /// </summary>
        /// <remarks>
        /// This is read-only.
        /// </remarks>
        private readonly string shortTimePatternWithSeconds;

        /// <summary>
        /// The format string to use for short times including seconds using 
        /// 12-hour clock
        /// </summary>
        /// <remarks>
        /// This is read-only.
        /// </remarks>
        private readonly string shortTimePatternWithSeconds12Hour;

        /// <summary>
        /// The format string to use for short times including seconds
        /// using 12-hour clock with am/pm indicator
        /// </summary>
        /// <remarks>
        /// This is read-only.
        /// </remarks>
        private readonly string shortTimePatternWithSeconds12HourAMPM;

        /// <summary>
        /// The format string to use for short times including seconds
        /// using 24-hour clock with am/pm indicator
        /// </summary>
        /// <remarks>
        /// This is read-only.
        /// </remarks>
        private readonly string shortTimePatternWithSecondsAMPM;

        /// <summary>
        /// The format string to use for short times using 24-hour clock
        /// with am/pm indicator
        /// </summary>
        /// <remarks>
        /// This is read-only.
        /// </remarks>
        private readonly string shortTimePatternAMPM;

        /// <summary>
        /// The format string to use for short times using the 12-hour clock.
        /// </summary>
        /// <remarks>
        /// This is read-only.
        /// </remarks>
        private readonly string shortTimePattern12Hour;

        /// <summary>
        /// The format string to use for short times using 12-hour clock
        /// with am/pm indicator
        /// </summary>
        /// <remarks>
        /// This is read-only.
        /// </remarks>
        private readonly string shortTimePattern12HourAMPM;
        #endregion

        #region Constructors
        /// <summary>
        ///Constructs a GlobalizationService object. 
        /// </summary>
        public GlobalizationService()
        {
            this.name = "en-GB";
            this.shortTimePattern = "HH:mm";
            this.shortDatePattern = "dd-MMM-yyyy";
            this.shortDatePatternWithDayOfWeek = "ddd dd-MMM-yyyy";
            this.shortTimePatternWithSeconds = "HH:mm:ss";
            this.shortTimePatternWithSeconds12Hour = "hh:mm:ss";
            this.shortTimePatternWithSeconds12HourAMPM = "hh:mm:ss (tt)";
            this.shortTimePatternWithSecondsAMPM = "HH:mm:ss (tt)";
            this.shortTimePatternAMPM = "HH:mm (tt)";
            this.shortTimePattern12Hour = "hh:mm";
            this.shortTimePattern12HourAMPM = "hh:mm (tt)";
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The RFC 1766 culture identifier.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// The format string to use for short dates. 
        /// </summary>
        /// <returns>
        /// dd-MMM-yyyy for the en-GB culture.
        /// </returns>
        public string ShortDatePattern
        {
            get
            {
                return this.shortDatePattern;
            }
        }

        /// <summary>
        /// The format string to use for short dates that include a day of the week. 
        /// </summary>
        /// <returns>
        /// ddd dd-MMM-yyy for the en-GB culture. 
        /// </returns>
        public string ShortDatePatternWithDayOfWeek
        {
            get
            {
                return this.shortDatePatternWithDayOfWeek;
            }
        }

        /// <summary>
        /// The format string to use for short times. 
        /// </summary>
        /// <returns>
        /// HH:mm for the en-GB culture. 
        /// </returns>
        public string ShortTimePattern
        {
            get
            {
                return this.shortTimePattern;
            }
        }

        /// <summary>
        /// The format string to use for short times including seconds. 
        /// </summary>
        /// <returns>
        ///  HH:mm:ss for the en-GB culture. 
        /// </returns>
        public string ShortTimePatternWithSeconds
        {
            get
            {
                return this.shortTimePatternWithSeconds;
            }
        }

        /// <summary>
        /// The format string to use for short times including seconds using the
        /// 12-hour clock. 
        /// </summary>
        /// <returns>
        /// hh:mm:ss for the en-GB culture. 
        /// </returns>
        public string ShortTimePatternWithSeconds12Hour
        {
            get
            {
                return this.shortTimePatternWithSeconds12Hour;
            }
        }

        /// <summary>
        /// The format string to use for short times including seconds
        /// using the 12-hour clock and displaying an AM/PM indicator. 
        /// </summary>
        /// <returns>
        /// hh:mm:ss (tt) for the en-GB culture. 
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", Justification = "As specified")]
        public string ShortTimePatternWithSeconds12HourAMPM
        {
            get
            {
                return this.shortTimePatternWithSeconds12HourAMPM;
            }
        }

        /// <summary>
        /// The format string to use for short times including seconds
        /// using the 24-hour clock and displaying an AM/PM indicator. 
        /// </summary>
        /// <returns>
        /// Always returns HH:mm:ss (tt) for the en-GB culture. 
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", Justification = "As specified")]
        public string ShortTimePatternWithSecondsAMPM
        {
            get
            {
                return this.shortTimePatternWithSecondsAMPM;
            }
        }

        /// <summary>
        /// The format string to use for short times using the 24-hour clock
        /// and displaying an AM/PM indicator.
        /// </summary>
        /// <returns>
        /// HH:mm (tt) for the en-GB culture. 
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", Justification = "As specified")]
        public string ShortTimePatternAMPM
        {
            get
            {
                return this.shortTimePatternAMPM;
            }
        }

        /// <summary>
        /// The format string to use for short times using the 12-hour clock.
        /// </summary>
        /// <returns>
        /// hh:mm for the en-GB culture. 
        /// </returns>
        public string ShortTimePattern12Hour
        {
            get
            {
                return this.shortTimePattern12Hour;
            }
        }

        /// <summary>
        /// The format string to use for short times using the 12-hour clock and displaying an AM/PM indicator. 
        /// </summary>
        /// <returns>
        /// hh:mm (tt) for the en-GB culture. 
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", Justification = "As specified")]
        public string ShortTimePattern12HourAMPM
        {
            get
            {
                return this.shortTimePattern12HourAMPM;
            }
        }
        #endregion
    }
}
