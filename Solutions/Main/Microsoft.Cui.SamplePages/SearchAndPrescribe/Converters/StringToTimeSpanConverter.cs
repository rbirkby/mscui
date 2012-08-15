//-----------------------------------------------------------------------
// <copyright file="StringToTimeSpanConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>26-Jan-2009</date>
// <summary>Converts an string to a timespan.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converts an string to a timespan.
    /// </summary>
    public class StringToTimeSpanConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Forward converting function.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>A return value.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                if (value.ToString().ToLower(culture).Contains("once only"))
                {
                    return TimeSpan.Zero;
                }
                else
                {
                    string[] parts = value.ToString().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 1)
                    {
                        if (parts[0].ToLower(culture) == "unlimited" || parts[0].ToLower(culture) == "ongoing")
                        {
                            return TimeSpan.MaxValue;
                        }
                    }
                    else if (parts.Length > 1)
                    {
                        int amount = 0;
                        if (!int.TryParse(parts[0], out amount))
                        {
                            amount = 0;
                        }

                        switch (parts[1].ToLower(culture))
                        {
                            case "mins":
                            case "minutes":
                                return TimeSpan.FromMinutes(amount);
                            case "hours":
                                return TimeSpan.FromHours(amount);
                            case "days":
                                return TimeSpan.FromDays(amount);
                            case "weeks":
                                return TimeSpan.FromDays(amount * 7);
                        }
                    }
                }
            }

            return TimeSpan.Zero;
        }

        /// <summary>
        /// Convert back function. Not implemented here.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>A return value value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "";
        }

        #endregion
    }
}
