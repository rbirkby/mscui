//-----------------------------------------------------------------------
// <copyright file="PatientName.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>A helper class used to format a patient's name. </summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// A helper class used to format a patient's name. 
    /// </summary>
    public static class PatientName
    {
        #region Const Values

        /// <summary>
        /// Index of the FamilyName in an array returned by the 
        /// <see cref="M:NhsCui.Toolkit.PatientName.FormatNameArray()">FormatNameArray</see> method. 
        /// </summary>
        public const int FamilyNameIndex = 0;

        /// <summary>
        /// Index of the GivenName in an array returned by the <see cref="M:NhsCui.Toolkit.PatientName.FormatNameArray">FormatNameArray</see> method. 
        /// </summary>
        public const int GivenNameIndex = 1;

        /// <summary>
        /// Index of the Title in an array returned by the <see cref="M:NhsCui.Toolkit.PatientName.FormatNameArray">FormatNameArray</see> method.
        /// </summary>
        public const int TitleIndex = 2;

        /// <summary>
        /// An ellipsis.
        /// </summary>
        public const string Ellipsis = "...";

        /// <summary>
        /// The maximum number of characters allowed in FamilyName.
        /// </summary>
        private const int MaxFamilyNameChars = 40;

        /// <summary>
        /// The maximum number of characters allowed in GivenName.
        /// </summary>
        private const int MaxGivenNameChars = 40;

        /// <summary>
        /// The maximum number of characters in Title.
        /// </summary>
        private const int MaxTitleChars = 35;

        #endregion

        #region Public Methods
        /// <summary>
        /// Formats the patient's name. 
        /// </summary>
        /// <param name="familyName">The patient's family name.</param>
        /// <param name="givenName">The patient's given name.</param>
        /// <param name="title">The patient's title. </param>
        /// <returns>The patient's name formatted as FamilyName, GivenName (Title), where FamilyName is capitalized</returns>
        public static string Format(string familyName, string givenName, string title)
        {
            string[] formattedNameParts = FormatNameArray(familyName, givenName, title);
            string formattedName = string.Concat(formattedNameParts);
            return formattedName;
        }

        /// <summary>
        /// Returns an array containing the formatted name elements. 
        /// </summary>
        /// <param name="familyName">The patient's family name.</param>
        /// <param name="givenName">The patient given name.</param>
        /// <param name="title">The patient's title. </param>
        /// <returns>An array of three formatted strings for FamilyName, GivenName and Title. </returns>
        public static string[] FormatNameArray(string familyName, string givenName, string title)
        {
            string[] returnArray =
                                    {
                                        FormatFamilyName(familyName),
                                        FormatGivenName(givenName),
                                        FormatTitle(title)
                                    };

            // if we have a first name and last name separate them
            if (returnArray[FamilyNameIndex].Length > 0 && returnArray[GivenNameIndex].Length > 0)
            {
                returnArray[FamilyNameIndex] += PatientNameResources.FamilyAndGivenNameSeparator;
            }
            else if (returnArray[FamilyNameIndex].Length == 0 && returnArray[GivenNameIndex].Length == 0)
            {
                // if no first or last name don't display title
                returnArray[TitleIndex] = string.Empty;
            }

            return returnArray;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Format the Title
        /// </summary>
        /// <param name="title">Patient title</param>
        /// <returns>Formatted Title</returns>
        private static string FormatTitle(string title)
        {
            // check argument
            if (title == null)
            {
                throw new ArgumentNullException("title");
            }

            string trimTitle = title.Trim();
            if (trimTitle.Length > 0)
            {
                string formattedTitle = string.Format(CultureInfo.CurrentCulture, PatientNameResources.TitleFormat, TruncateWithEllipsisIfNeeded(trimTitle, MaxTitleChars));
                return formattedTitle;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Format the GivenName
        /// </summary>
        /// <param name="givenName">Patient given name</param>
        /// <returns>Formatted GivenName</returns>
        private static string FormatGivenName(string givenName)
        {
            // check argument
            if (givenName == null)
            {
                throw new ArgumentNullException("givenName");
            }

            string trimFirstName = givenName.Trim();
            if (trimFirstName.Length > 0)
            {
                return TruncateWithEllipsisIfNeeded(trimFirstName, MaxGivenNameChars);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Format the FamilyName
        /// </summary>
        /// <param name="familyName">Patient family name</param>
        /// <returns>Formatted FamilyName</returns>
        private static string FormatFamilyName(string familyName)
        {
            // check argument
            if (familyName == null)
            {
                throw new ArgumentNullException("familyName");
            }

            string trimLastName = familyName.Trim().ToUpper(CultureInfo.CurrentCulture);
            if (trimLastName.Length > 0)
            {
                return TruncateWithEllipsisIfNeeded(trimLastName, MaxFamilyNameChars);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Truncate the supplied value and append ellipsis so that the total
        /// length does not exceed the maximum specified
        /// </summary>
        /// <param name="value">value to truncate</param>
        /// <param name="maxLength">maximum length</param>
        /// <returns>truncated value</returns>
        private static string TruncateWithEllipsisIfNeeded(string value, int maxLength)
        {
            if (value.Length > maxLength)
            {
                return value.Substring(0, maxLength - Ellipsis.Length) + Ellipsis;
            }

            return value;
        }

        #endregion
    }
}
