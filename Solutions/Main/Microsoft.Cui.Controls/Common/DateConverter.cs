//-----------------------------------------------------------------------
// <copyright file="DateConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>2-Jun-2008</date>
// <summary>Date converter.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Globalization;
    using Common.DateAndTime;
    using Microsoft.Cui.Controls.Common;
    #endregion

    /// <summary>
    /// Class implementing date converter.
    /// </summary>
    public class DateConverter : TypeConverter
    {
        /// <summary>
        /// Returns whether this converter can convert an object of one type to the
        /// type of this converter.
        /// </summary>
        /// <param name="context">Type descriptor context of the source.</param>
        /// <param name="sourceType">Source type.</param>
        /// <returns>True if type can be converted, else False.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Returns whether this converter can convert the object to the specified type.
        /// </summary>
        /// <param name="context">Type descriptor context of the source.</param>
        /// <param name="destinationType">Destination type.</param>
        /// <returns>True if conversion can be made, else False.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the given value to the type of this converter.
        /// </summary>
        /// <param name="context">Type descriptor context of the source.</param>
        /// <param name="culture">Culture information.</param>
        /// <param name="value">Input value.</param>
        /// <returns>Converted type.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string stringValue = (string)value;
            if (stringValue != null)
            {
                return DateConverter.GetDateFromString(stringValue);
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Converts the given value object to the specified type.
        /// </summary>
        /// <param name="context">Type descriptor context of the source.</param>
        /// <param name="culture">Culture information.</param>
        /// <param name="value">Given value.</param>
        /// <param name="destinationType">Destination type.</param>
        /// <returns>Converted type.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value != null)
            {
                return value.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        } 

        /// <summary>
        /// Converts a given string to an object of type DateTime.
        /// </summary>
        /// <param name="text">String to be converted.</param>
        /// <returns>DateTime object.</returns>
        private static DateTime GetDateFromString(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentOutOfRangeException(CommonResources.NullText);
            }

            DateTime parsedDateTime;
            string[] validFormats = new string[] 
                                   {
                                        "d-MMM-yyyy", "d-M-yyyy", "d-MM-yyyy", "d-MMMM-yyyy",
                                        "d/MMM/yyyy", "d/M/yyyy", "d/MM/yyyy", "d/MMMM/yyyy",
                                        "d MMM yyyy", "d M yyyy", "d MM yyyy", "d MMMM yyyy",
                                        "ddd d-MMM-yyyy", "ddd d-M-yyyy", "ddd d-MM-yyyy", "ddd d-MMMM-yyyy",
                                        "ddd d/MMM/yyyy", "ddd d/M/yyyy", "ddd d/MM/yyyy", "ddd d/MMMM/yyyy",
                                        "ddd d MMM yyyy", "ddd d M yyyy", "ddd d MM yyyy", "ddd d MMMM yyyy",
                                        "d-MMM-yy", "d-M-yy", "d-MM-yy", "d-MMMM-yy",
                                        "d/MMM/yy", "d/M/yy", "d/MM/yy", "d/MMMM/yy",
                                        "d MMM yy", "d M yy", "d MM yy", "d MMMM yy",
                                        "ddd d-MMM-yy", "ddd d-M-yy", "ddd d-MM-yy", "ddd d-MMMM-yy",
                                        "ddd d/MMM/yy", "ddd d/M/yy", "ddd d/MM/yy", "ddd d/MMMM/yy",
                                        "ddd d MMM yy", "ddd d M yy", "ddd d MM yy", "ddd d MMMM yy"
                                  };

            if (DateTime.TryParseExact(text, validFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime))
            {
                return parsedDateTime;
            }
            else
            {
                throw new ArgumentException("invalid date specified");
            }
        }
    }
}
