//-----------------------------------------------------------------------
// <copyright file="Enums.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Holds all the enumerations required to support the classes in the DateAndTime namespace. 
// </summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.DateAndTime
{   
    /// <summary>
    /// Specfies a value indicating a time type. 
    /// </summary>
    public enum TimeType
    {
        /// <summary>
        /// Time should be treated as an exact value. 
        /// </summary>
        Exact,

        /// <summary>
        /// Time should be treated as an approximate value. 
        /// </summary>
        Approximate,     

        /// <summary>
        /// Time should be treated as a null index value with the NullIndex property
        /// identifying a type of null index. 
        /// </summary>
        NullIndex,

        /// <summary>
        /// Time should be treated as a true null 
        /// </summary>
        Null
    }

    /// <summary>
    /// Specifies a value indicating a date type. 
    /// </summary>
    public enum DateType
    {        
        /// <summary>
        /// The date should be treated as an exact value.
        /// </summary>
        Exact,

        /// <summary>
        /// The date should be treated as a year only. 
        /// </summary>
        Year,

        /// <summary>
        /// The date should be treated as a combination of the month and the year. 
        /// </summary>
        YearMonth,
       
        /// <summary>
        /// The date should be treated as a null index value with the NullIndex property
        /// identifying a type of null index. 
        /// </summary>
        NullIndex,

        /// <summary>
        /// The date should be treated as an approximate value. 
        /// </summary>
        Approximate,

        /// <summary>
        /// The date should be treated as a true null. 
        /// </summary>
        Null
    }
   
    /// <summary>
    /// Specifies a value for the units of measurement that can be used in a time span. 
    /// </summary>
    public enum TimeSpanUnit
    {
         /// <summary>
       /// Seconds
       /// </summary>
       Seconds,
 
        /// <summary>
        /// Minutes 
        /// </summary>
        Minutes,
        
        /// <summary>
        /// Hours 
        /// </summary>
        Hours,
        
        /// <summary>
        /// Days 
        /// </summary>
        Days,

        /// <summary>
        /// Weeks 
        /// </summary>
        Weeks,
        
        /// <summary>
        /// Months 
        /// </summary>
        Months,
                
        /// <summary>
        /// Years
        /// </summary>
        Years      
    }

    /// <summary>
    /// Specifies a value for time functionality. 
    /// </summary>
    public enum TimeFunctionality
    {
        /// <summary>Full functionality is enabled in the TimeInputBox. 
        /// </summary>
        Complex = 0,

        /// <summary>Simple functionality is enabled in the TimeInputBox. 
        /// </summary>
        Simple
    }

    /// <summary>
    /// Specifies a value for date functionality. 
    /// </summary>
    public enum DateFunctionality
    {
        /// <summary>Full functionality is enabled in the DateInputBox. </summary>
        Complex = 0,

        /// <summary>Simple functionality is enabled in the DateInputBox. </summary>
        Simple
    }
}
