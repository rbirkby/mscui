//-----------------------------------------------------------------------
// <copyright file="DateTimeConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>24-Sep-2008</date>
// <summary>DateTime converter.</summary>
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
    public class DateTimeConverter : TypeConverter
    {
#if SILVERLIGHT
        /// <summary>
        /// Returns whether the type converter can convert an object from the specified type to the type of this converter.
        /// </summary>
        /// <param name="context">An object that provides a format context.</param>
        /// <param name="sourceType">The type you want to convert from.</param>
        /// <returns>
        /// True if this converter can perform the conversion; otherwise, false.
        /// </returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(sourceType);
        }

        /// <summary>
        /// Converts from the specified value to the type of this converter.
        /// </summary>
        /// <param name="context">An object that provides a format context.</param>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The value to convert to the type of this converter.</param>
        /// <returns>The converted value.</returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The conversion cannot be performed.
        /// </exception>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string val = value as string;
            if (val != null)
            {
                return this.ConvertFromString(val);
            }

            return base.ConvertFrom(value);
        }

        /// <summary>
        /// Returns whether the type converter can convert an object to the specified type.
        /// </summary>
        /// <param name="context">An object that provides a format context.</param>
        /// <param name="destinationType">The type you want to convert to.</param>
        /// <returns>
        /// True if this converter can perform the conversion; otherwise, false.
        /// </returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return true;
            }

            return base.CanConvertTo(destinationType);
        }

        /// <summary>
        /// Converts the specified value object to the specified type.
        /// </summary>
        /// <param name="context">An object that provides a format context.</param>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The object to convert.</param>
        /// <param name="destinationType">The type to convert the object to.</param>
        /// <returns>The converted object.</returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The conversion cannot be performed.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="destinationType"/> is null.
        /// </exception>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return this.ConvertToString(value);
            }

            return base.ConvertTo(value, destinationType);
        }

        /// <summary>
        /// Converts the specified text to an object.
        /// </summary>
        /// <param name="text">Input text.</param>
        /// <returns>Converted type.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Previous Code")]
        public new object ConvertFromString(string text)
        {
            return DateTimeConverter.GetDateFromString(text);
        }

        /// <summary>
        /// Converts the specified value to a string representation.
        /// </summary>
        /// <param name="value">Input value.</param>
        /// <returns>Converted string.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Previous Code")]
        public new string ConvertToString(object value)
        {
            if (value != null)
            {
                return value.ToString();
            }

            return (string)value;
        }
#else
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
                return DateTimeConverter.GetDateFromString(stringValue);
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
            return base.ConvertTo(context, culture, value, destinationType);
        }
#endif

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
            
            if (DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.None, out parsedDateTime))
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
