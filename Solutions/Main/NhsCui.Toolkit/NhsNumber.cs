//-----------------------------------------------------------------------
// <copyright file="NhsNumber.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Performs validation and formatting of NHS numbers. </summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Globalization;

    /// <summary>
    /// Performs validation and formatting of NHS numbers.
    /// </summary>
    public sealed class NhsNumber
    {
        #region Member Vars

        /// <summary>
        /// RegEx to enforce no alphabetic characters.
        /// </summary>
        private static string alphasRegEx = @"[A-Za-z]";

        /// <summary>
        /// RegEx to enforce digits only.
        /// </summary>
        private static string nonDigitCharactersRegEx = @"\D";

        /// <summary>
        /// RegEx to check for single-digit-only repeating number.
        /// </summary>
        private static string repeatingDigitsRegEx = @"0{10}|1{10}|2{10}|3{10}|4{10}|5{10}|6{10}|7{10}|8{10}|9{10}";

        /// <summary>
        /// Internal string for the instance var of the NHS identifier.
        /// </summary>
        private string nhsNumber;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an NhsNumber object using a string.
        /// </summary>
        /// <param name="nhsNumber">NhsNumber as a string. </param>
        public NhsNumber(string nhsNumber)
        {
            this.SetNhsNumber(nhsNumber);
        }

        /// <summary>
        /// Constructs an NhsNumber object using an Int32. 
        /// </summary>
        /// <param name="nhsNumber">NhsNumber as an Int32.</param>
        public NhsNumber(Int32 nhsNumber)
        {
            string nhsNumberString = nhsNumber.ToString(CultureInfo.InvariantCulture);
            this.SetNhsNumber(nhsNumberString);
        }

        /// <summary>
        /// Constructs an NhsNumber object using an Int64.
        /// </summary>
        /// <param name="nhsNumber">NhsNumber as an Int64.</param>
        public NhsNumber(Int64 nhsNumber)
        {
            string nhsNumberString = nhsNumber.ToString(CultureInfo.InvariantCulture);
            this.SetNhsNumber(nhsNumberString);
        }

        /// <summary>
        /// Constructs an NhsNumber object using a double. 
        /// </summary>
        /// <param name="nhsNumber">NhsNumber as a double.</param>
        public NhsNumber(double nhsNumber)
        {
            string nhsNumberString = nhsNumber.ToString(CultureInfo.InvariantCulture);
            this.SetNhsNumber(nhsNumberString);
        }

        #endregion

        #region Enums

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

        #endregion

        #region Methods

        /// <summary>
        /// Converts the specified string representation of an NHS identifier to its correctly-formatted equivalent. 
        /// </summary>
        /// <remarks>
        /// If parsing fails, the out parsedValue is not changed. 
        /// </remarks>
        /// <param name="value">A string containing an NHS identifier to be parsed. </param>
        /// <param name="result">When this method returns, it contains the formatted NHS identifier equivalent to 
        /// that contained in value if the conversion succeeded; otherwise it contains the original value. The conversion fails 
        /// if the value parameter is null or does not contain a valid string 
        /// representation of an NHS identifier.</param>
        /// <returns>An NhsNumberParseResult indicating the outcome of the attempted parse operation.</returns>
        public static NhsNumberParseResult TryParseNhsNumber(string value, out string result)
        {
            NhsNumberParseResult returnValue = NhsNumberParseResult.FailedUnknownReason;
            string parsedValue = null;
            string defaultResult = string.Empty;

            if (Regex.IsMatch(value, alphasRegEx) == true)
            {
                returnValue = NhsNumberParseResult.FailedAlphaCharacterContent;
            }
            else
            {
                parsedValue = Regex.Replace(value, nonDigitCharactersRegEx, "");
                if (parsedValue.Length != 10)
                {
                    returnValue = NhsNumberParseResult.FailedDigitCount;
                }
                else if (NhsNumberCheckSumIsValid(parsedValue) == false)
                {
                    returnValue = NhsNumberParseResult.FailedChecksum;
                }
                else if (Regex.IsMatch(parsedValue, repeatingDigitsRegEx) == true)
                {
                    returnValue = NhsNumberParseResult.FailedAllSameDigits;
                }
                else
                {
                    returnValue = NhsNumberParseResult.Success;
                }
            }

            if (returnValue == NhsNumberParseResult.Success)
            {
                result = parsedValue;
            }
            else
            {
                result = defaultResult;
            }

            return returnValue;
        }

        /// <summary>
        /// Converts the specified string representation of an NHS identifier to its correctly-formatted equivalent.
        /// </summary>
        /// <remarks>
        /// Throws an exception if the string cannot be parsed.
        /// </remarks>
        /// <param name="value">A string containing an NHS identifier to be parsed. </param>
        /// <returns>An NHS identifier constructed from the value parameter. </returns>
        public static NhsNumber ParseNhsNumber(string value)
        {
            string parsedValue;

            NhsNumber.NhsNumberParseResult result = NhsNumber.TryParseNhsNumber(value, out parsedValue);
            if (result == NhsNumber.NhsNumberParseResult.Success)
            {
                return new NhsNumber(parsedValue);
            }
            else
            {
                if (result == NhsNumber.NhsNumberParseResult.FailedAlphaCharacterContent)
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, NhsNumberExceptionResources.InvalidNhsNumberMessageAlphaChars, value));
                }
                else if (result == NhsNumber.NhsNumberParseResult.FailedChecksum)
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, NhsNumberExceptionResources.InvalidNhsNumberMessageChecksum, value));
                }
                else if (result == NhsNumber.NhsNumberParseResult.FailedDigitCount)
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, NhsNumberExceptionResources.InvalidNhsNumberMessageDigitCount, value));
                }
                else if (result == NhsNumber.NhsNumberParseResult.FailedAllSameDigits)
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, NhsNumberExceptionResources.InvalidNhsNumberMessageDigitsAllSame, value));
                }
                else
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, NhsNumberExceptionResources.InvalidNhsNumberMessageUnknownError, value));
                }
            }
        }

        /// <summary>
        /// Provides the string representation of NhsNumber.
        /// </summary>
        /// <returns>A string representation of NhsNumber.</returns>
        public override string ToString()
        {
            return this.nhsNumber;
        }

        /// <summary>
        /// Performs the NHS identifier CheckSum calculation. 
        /// </summary>
        /// <param name="identifier">A numeric string representing an NHS identifier.</param>
        /// <returns>True if numeric string conforms to NHS identifier CheckSum, otherwise false.</returns>
        private static bool NhsNumberCheckSumIsValid(string identifier)
        {
            int calcResult = 0;

            for (int digitIndex = 0; digitIndex < 9; digitIndex++)
            {
                calcResult += Convert.ToInt32(identifier.Substring(digitIndex, 1), new CultureInfo("en-gb")) * (10 - digitIndex);
            }

            calcResult = calcResult % 11;

            calcResult = 11 - calcResult;
            if (calcResult == 11)
            {
                calcResult = 0;
            }

            if (Convert.ToInt32(identifier.Substring(9, 1), new CultureInfo("en-gb")) == calcResult)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Parses/sets the NHS Number.
        /// </summary>
        /// <param name="nhsNumber">String to assign to the NHS Number.</param>
        private void SetNhsNumber(string nhsNumber)
        {
            // Parse the text passed in to see if meets the formatting criteria
            // If it is store the value and format it for display
            string parsedValue;

            NhsNumber.NhsNumberParseResult result = NhsNumber.TryParseNhsNumber(nhsNumber, out parsedValue);
            if (result == NhsNumber.NhsNumberParseResult.Success)
            {
                this.nhsNumber = parsedValue.Substring(0, 3) + " " + parsedValue.Substring(3, 3) + " " + parsedValue.Substring(6, 4);
            }
            else
            {
                if (result == NhsNumber.NhsNumberParseResult.FailedAlphaCharacterContent)
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, NhsNumberExceptionResources.InvalidNhsNumberMessageAlphaChars, nhsNumber));
                }
                else if (result == NhsNumber.NhsNumberParseResult.FailedChecksum)
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, NhsNumberExceptionResources.InvalidNhsNumberMessageChecksum, nhsNumber));
                }
                else if (result == NhsNumber.NhsNumberParseResult.FailedDigitCount)
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, NhsNumberExceptionResources.InvalidNhsNumberMessageDigitCount, nhsNumber));
                }
                else if (result == NhsNumber.NhsNumberParseResult.FailedAllSameDigits)
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, NhsNumberExceptionResources.InvalidNhsNumberMessageDigitsAllSame, nhsNumber));
                }
                else
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, NhsNumberExceptionResources.InvalidNhsNumberMessageUnknownError, nhsNumber));
                }
            }
        }

        #endregion
    }
}
