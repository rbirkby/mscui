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
// <date>23-May-2008</date>
// <summary>
// Holds all the enumerations required to support the classes.. 
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    /// <summary>
    /// Type of borders.
    /// </summary>
    public enum BorderStyle
    {
        /// <summary>
        /// Solid border.
        /// </summary>
        Solid,

        /// <summary>
        /// Dashed border.
        /// </summary>
        Dashed
    }

    /// <summary>
    /// Specifies values for possible results of TryParseNhsNumber.
    /// </summary>
    public enum NhsNumberParseResult
    {
        /// <summary>
        /// The value was successfully parsed. 
        /// </summary>
        Success,

        /// <summary>
        /// The value was successfully parsed but did not contain a valid NHS identifier. The value failed a CheckSum calculation. 
        /// </summary>
        FailedChecksum,

        /// <summary>
        /// The value contained too many or too few digits. A valid NHS identifier must contain exactly 10 non-alphabetic digits which 
        /// cannot all be the same. 
        /// </summary>
        FailedDigitCount,

        /// <summary>
        /// The value contained alphabetic characters. A valid NHS identifier must contain exactly 10 non-alphabetic digits which cannot 
        /// all be the same. 
        /// </summary>
        FailedAlphaCharacterContent,

        /// <summary>
        /// The value digits cannot all be the same. A valid NHS identifier must contain exactly 10 non-alphabetic digits which 
        /// cannot all be the same. 
        /// </summary>
        FailedAllSameDigits,

        /// <summary>
        /// The value could not be parsed. A valid NHS identifier must contain exactly 10 non-alphabetic digits which 
        /// cannot all be the same. 
        /// </summary>
        FailedUnknownReason
    }
}
