//-----------------------------------------------------------------------
// <copyright file="StringArrayTypeConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>28-May-2007</date>
// <summary>Type converter for comma separated list of strings</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.ComponentModel;
    using System.Globalization;
    #endregion

    /// <summary>
    /// Type converter for comma separated list of strings.
    /// </summary>
    public class StringArrayTypeConverter : TypeConverter
    {
        /// <summary>
        /// Returns whether the converter can convert an object of the given type using the 
        /// specified context.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="sourceType">A Type that represents the type to be converted from.</param>
        /// <returns>true if the converter can perform the conversion; otherwise, false.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        /// <summary>
        /// Returns whether the converter can convert the object to the specified type, using the specified context. 
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="destinationType">A Type that represents the type to be converted to.</param> 
        /// <returns>true if the converter can perform the conversion; otherwise, false.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        /// <summary>
        /// Converts the given object to the type of the converter using the specified context and culture information. 
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="culture">CultureInfo culture.</param>
        /// <param name="value">The Object to be converted. </param>
        /// <returns>An Object that represents the converted value.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string stringValue = value as string;
            if (stringValue == null)
            {
                throw new ArgumentNullException("value");
            }

            string[] items = stringValue.Split(',');
            string[] trimmedItems = new string[items.Length];

            for (int i = 0; i < items.Length; i++)
            {
                trimmedItems[i] = items[i].Trim();
            }

            return trimmedItems;
        }

        /// <summary>
        /// Converts the given value object to the specified type
        /// using the specified context and culture information.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="culture">A CultureInfo. If a null reference (nothing in Visual Basic) is passed, the current culture is assumed.</param>
        /// <param name="value">The object to be converted.</param>
        /// <param name="destinationType">The type to convert the object to.</param>
        /// <returns>The converted object.</returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The conversion cannot be performed.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="destinationType"/> is null.
        /// </exception>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
            {
                throw new ArgumentException("string required", "value");
            }

            string[] array = value as string[];

            if (array == null)
            {
                return string.Empty;
            }

            return string.Join(",", array);
        }
    }
}
