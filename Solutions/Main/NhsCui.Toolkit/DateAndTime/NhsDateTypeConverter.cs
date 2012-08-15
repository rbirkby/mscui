//-----------------------------------------------------------------------
// <copyright file="NhsDateTypeConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>A type converter for the NhsDate class. </summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.DateAndTime
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;

    /// <exclude />
    /// <summary>
    /// A type converter for the NhsDate class. 
    /// </summary>
    public class NhsDateTypeConverter : TypeConverter
    {
        #region Public Methods
        /// <summary>
        /// Returns whether the converter can convert an object of the given type using the 
        /// specified context. 
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context. </param>
        /// <param name="sourceType">A Type that represents the type to be converted from. </param>
        /// <returns>True if the converter can perform the conversion; otherwise, false. </returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Returns whether the converter can convert the object to the specified type using the specified context. 
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.  </param>
        /// <param name="destinationType">A Type that represents the type to be converted to. </param>
        /// <returns>True if the converter can perform the conversion; otherwise, false. </returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the given object to the type of the converter using the specified context and culture information.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context. </param>
        /// <param name="culture">CultureInfo culture. </param>
        /// <param name="value">The Object to be converted. </param>
        /// <returns>An Object that represents the converted value. </returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string stringValue = value as string;

            if (stringValue != null)
            {
                NhsDate date = NhsDate.ParseExact(stringValue, culture);
                return date;
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Converts the given value object to the specified type
        /// using the specified context and culture information.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
        /// <param name="culture">A CultureInfo. If a null reference (nothing in Visual Basic) is passed, the current culture is assumed. </param>
        /// <param name="value">The Object to be converted. </param>
        /// <param name="destinationType">The Type the value parameter is to be converted to.  </param>
        /// <returns>The Type the value parameter is to be converted to.  </returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor))
            {
                NhsDate date = (NhsDate)value;
                bool dateIsApproximate = (date.DateType == DateType.Approximate);
                CultureInfo cultureInfo = (culture == null ? CultureInfo.InvariantCulture : culture);

                string formattedDate = date.ToString(false, dateIsApproximate, false, cultureInfo);

                if (destinationType == typeof(string))
                {
                    return formattedDate;
                }

                ConstructorInfo constructor = typeof(NhsDate).GetConstructor(new Type[] { typeof(string) });
                return new InstanceDescriptor(constructor, new object[] { formattedDate });
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
        #endregion
    }
}
