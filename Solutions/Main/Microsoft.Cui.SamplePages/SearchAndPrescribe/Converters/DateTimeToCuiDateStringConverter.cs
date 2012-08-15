//-----------------------------------------------------------------------
// <copyright file="DateTimeToCuiDateStringConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>17-Aug-2009</date>
// <summary>Converts a DateTime to a CUI compliant date string.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.Windows;
    using System.Windows.Data;
    using System.Globalization;
    using Microsoft.Cui.Controls;
    using Microsoft.Cui.Controls.Common.DateAndTime;

    /// <summary>
    /// Converts a DateTime to a CUI compliant date string.
    /// </summary>
    public class DateTimeToCuiDateStringConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Convert forward function.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>The converted value.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool includeTime = false;
            if (value == null)
            {
                return null;
            }

            if (parameter != null)
            {
                if (!bool.TryParse(parameter.ToString(), out includeTime))
                {
                    includeTime = false;
                }
            }

            DateTime? dateTime = value as DateTime?;
            if (dateTime.HasValue)
            {
                if (!includeTime)
                {
                    return new CuiDate(dateTime.Value).ToString();
                }
                else
                {
                    return dateTime.Value.ToString("dd-MMM-yyyy HH:mm", CultureInfo.CurrentCulture);
                }
            }

            return null;
        }

        /// <summary>
        /// Convert back function.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>The converted back value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            CuiDate cuiDate = new CuiDate(value.ToString());
            if (!cuiDate.IsNull)
            {
                return cuiDate.DateValue;
            }

            return null;
        }

        #endregion
    }
}
