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
// <date>22-Jan-2007</date>
// <summary>Enumerations used by the Parser class.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.PatientSearch
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    ///  Specifies values indicating genders for PatientSearch. 
    /// </summary>
    [Serializable]
    public enum Gender
    {
        /// <summary>
        /// The male search criterion. 
        /// </summary>
        Male,

        /// <summary>
        /// The female search criterion. 
        /// </summary>
        Female,

        /// <summary>
        /// No gender search criterion. 
        /// </summary>
        None,
    }
  
    /// <summary>
    /// Specifies information values for PatientSearch. 
    /// </summary>
    [Flags]
    [SuppressMessage("Microsoft.Naming", "CA1714:FlagsEnumsShouldHavePluralNames", Justification = "Required by Spec.")]
    [Serializable]
    public enum Information
    {
        /// <summary>
        /// All flags cleared. 
        /// </summary>
        None = 0,

        /// <summary>
        /// The address search criterion. 
        /// </summary>
        Address = 1,

        /// <summary>
        /// The age search criterion. 
        /// </summary>
        Age = 2,

        /// <summary>
        /// The date of birth search criterion. 
        /// </summary>
        DateOfBirth = 4,

        /// <summary>
        /// The family name search criterion.
        /// </summary>
        FamilyName = 8,

        /// <summary>
        /// The gender search criterion. 
        /// </summary>
        Gender = 16,

        /// <summary>
        /// The given name search criterion. 
        /// </summary>
        GivenName = 32,

        /// <summary>
        /// The NHS number search criterion. 
        /// </summary>
        NhsNumber = 64,

        /// <summary>
        /// The postcode search criterion. 
        /// </summary>
        Postcode = 128,

        /// <summary>
        /// The title search criterion. 
        /// </summary>
        Title = 256
    }

    /// <summary>
    /// Specifies found data type values for PatientSearch. 
    /// </summary>
    [Serializable]
    internal enum FoundDataType
    {
        /// <summary>
        /// No specific type.
        /// </summary>
        NotSpecified,

        /// <summary>
        /// A string containing the integer day, integer month and integer year.
        /// </summary>
        DateDayIntMonthYear,

        /// <summary>
        /// A string containing the integer day, text month and integer year.
        /// </summary>
        DateDayTextMonthYear,

        /// <summary>
        /// A string containing the integer month and integer year.
        /// </summary>
        DateIntMonthYear,

        /// <summary>
        /// Specifies a value indicating a string containing the text month and integer year.
        /// </summary>
        DateTextMonthYear,

        /// <summary>
        /// Specifies a value indicating a string containing the integer year.
        /// </summary>
        DateYear
    }
}
