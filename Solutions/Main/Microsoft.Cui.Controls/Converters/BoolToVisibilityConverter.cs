//-----------------------------------------------------------------------
// <copyright file="BoolToVisibilityConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>13-Aug-2009</date>
// <summary>Converts a bool to a visibility.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Converts a bool to a visibility.
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
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
            bool negated = false;
            if (parameter != null)
            {
                if (!bool.TryParse(parameter.ToString(), out negated))
                {
                    negated = false;
                }
            }

            if (value == null)
            {
                return negated ? Visibility.Visible : Visibility.Collapsed;
            }

            string valueString = value.ToString();
            if (value.ToString() == "1")
            {
                valueString = "True";
            }
            else if (value.ToString() == "0")
            {
                valueString = "False";
            }

            if (value == null)
            {
                return negated ? Visibility.Visible : Visibility.Collapsed;
            }            

            bool boolValue = false;

            if (!bool.TryParse(valueString, out boolValue))
            {
                boolValue = false;
            }

            if ((boolValue && !negated) || (!boolValue && negated))
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
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
            throw new NotImplementedException();
        }

        #endregion
    }
}
