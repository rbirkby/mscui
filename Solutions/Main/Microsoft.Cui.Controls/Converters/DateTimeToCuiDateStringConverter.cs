//-----------------------------------------------------------------------
// <copyright file="DateTimeToCuiDateStringConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2009.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2009.
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
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Data;
    using System.Globalization;

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
            if (value == null)
            {
                return null;
            }

            DateTime dateToTranslate = DateTime.Now;
            string valueString = value as string;
            if (string.IsNullOrEmpty(valueString))
            {
                valueString = value.ToString();
            }

            if (!string.IsNullOrEmpty(valueString))
            {
                string dateString = valueString;
                if (dateString.Length == 26)
                {
                    int yearPart = int.Parse(dateString.Substring(0, 4), CultureInfo.InvariantCulture);
                    int monthPart = int.Parse(dateString.Substring(4, 2), CultureInfo.InvariantCulture);
                    int dayPart = int.Parse(dateString.Substring(6, 2), CultureInfo.InvariantCulture);
                    dateToTranslate = new DateTime(yearPart, monthPart, dayPart);
                }
                else if (!DateTime.TryParse(valueString, CultureInfo.CurrentCulture, DateTimeStyles.None, out dateToTranslate))
                {
                    return null;
                }
            }
            else if (!string.IsNullOrEmpty(valueString))
            {
                return null;
            }
            else if (value is DateTime)
            {
                DateTime? date = value as DateTime?;
                if (date == null)
                {
                    return date;
                }

                dateToTranslate = date.Value;
            }

            string month = "Jan";
            switch (dateToTranslate.Month)
            {
                case 1:
                    month = "Jan";
                    break;
                case 2:
                    month = "Feb";
                    break;
                case 3:
                    month = "Mar";
                    break;
                case 4:
                    month = "Apr";
                    break;
                case 5:
                    month = "May";
                    break;
                case 6:
                    month = "Jun";
                    break;
                case 7:
                    month = "Jul";
                    break;
                case 8:
                    month = "Aug";
                    break;
                case 9:
                    month = "Sep";
                    break;
                case 10:
                    month = "Oct";
                    break;
                case 11:
                    month = "Nov";
                    break;
                case 12:
                    month = "Dec";
                    break;
            }

            return string.Format(culture, "{0}-{1}-{2}", dateToTranslate.Day.ToString("00", culture), month, dateToTranslate.Year);
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
            DateConverter dateConverter = new DateConverter();
            try
            {
                object convertedDate = dateConverter.ConvertFromString(value.ToString());
                return convertedDate;
            }
            catch (ArgumentException)
            {
                if (value.ToString().Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length == 3)
                {
                    string[] parts = value.ToString().Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                    int month = 1;

                    switch (parts[1].ToLower(culture).Trim())
                    {
                        case "jan":
                            month = 1;
                            break;
                        case "feb":
                            month = 2;
                            break;
                        case "mar":
                            month = 3;
                            break;
                        case "apr":
                            month = 4;
                            break;
                        case "may":
                            month = 5;
                            break;
                        case "jun":
                            month = 6;
                            break;
                        case "jul":
                            month = 7;
                            break;
                        case "aug":
                            month = 8;
                            break;
                        case "sep":
                            month = 9;
                            break;
                        case "oct":
                            month = 10;
                            break;
                        case "nov":
                            month = 11;
                            break;
                        case "dec":
                            month = 12;
                            break;
                    }

                    DateTime date = DateTime.Now;
                    try
                    {
                        date = new DateTime(int.Parse(parts[2], CultureInfo.CurrentCulture), month, int.Parse(parts[0], culture));
                        return date;
                    }
                    catch (ArgumentException)
                    {
                    }
                    catch (FormatException)
                    {
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
