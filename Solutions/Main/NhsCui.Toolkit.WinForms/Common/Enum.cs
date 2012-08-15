//-----------------------------------------------------------------------
// <copyright file="Enum.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>12-Apr-2007</date>
// <summary>The file contains enums for WinForms.</summary>
//-----------------------------------------------------------------------


namespace NhsCui.Toolkit.WinForms
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Specifies a value for the time field in the control.
    /// </summary>
    public enum Field
    {
        /// <summary>
        /// Null
        /// </summary>
        Null = -1,

        /// <summary>
        /// Hours
        /// </summary>
        Hours = 0,

        /// <summary>
        /// Minutes
        /// </summary>
        Minutes,

        /// <summary>
        /// Seconds
        /// </summary>
        Seconds,

        /// <summary>
        /// AM PM
        /// </summary>
        AmPM
    }

    /// <summary>
    /// Specifies a value for the input mode in the control
    /// </summary>
    /// <remarks>Used to identify whether the input is for arithmetic mode or simple mode</remarks>
    public enum InputMode
    {
        /// <summary>
        /// Simple
        /// </summary>
        Simple = 0,

        /// <summary>
        /// Arithmetic
        /// </summary>
        Arithmetic
    }

    /// <summary>
    /// Specifies a value for the time field in the control.
    /// </summary>
    public enum DateField
    {
        /// <summary>
        /// Null
        /// </summary>
        Null = -1,

        /// <summary>
        /// Day Name
        /// </summary>
        DayName = 0,

        /// <summary>
        /// Day
        /// </summary>
        Day,

        /// <summary>
        /// Month
        /// </summary>
        Month,

        /// <summary>
        /// Year
        /// </summary>
        Year
    }

    /// <summary>
    /// Specifies the values for date selection argument
    /// </summary>
    [Flags]
    public enum ChangedDateParts : int
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,

        /// <summary>
        /// Month changed
        /// </summary>
        MonthChanged = 1,

        /// <summary>
        /// Month selected
        /// </summary>
        MonthSelected = 2,

        /// <summary>
        /// Year changed
        /// </summary>
        YearChanged = 4,

        /// <summary>
        /// Year selected
        /// </summary>
        YearSelected = 8,       

        /// <summary>
        /// Null string selected
        /// </summary>
        NullStringSelected = 16
    }
}
