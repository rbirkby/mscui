//-----------------------------------------------------------------------
// <copyright file="ItemCountGreaterThanToVisibilityConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>26/01/2009</date>
// <summary>Returns a visibility based on whether the number of items in a list is greater than a specified count.</summary>
//-----------------------------------------------------------------------
#if SILVERLIGHT
namespace Microsoft.Cui.SamplePages
#else
namespace Microsoft.Cui.SampleWinform
#endif
{
    using System;
    using System.Collections;
    using System.Windows;
    using System.Windows.Data;

    /// <summary>
    /// Returns a visibility based on whether the number of items in a list is greater than a specified count.
    /// </summary>
    public class ItemCountGreaterThanToVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Returns a visibility based on whether the number of items in a list is greater than a specified count.
        /// </summary>
        /// <param name="value">The IList value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The greater parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>A visibility.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }

            IList list = (IList)value;
            int count = 0;
            if (list != null && int.TryParse(parameter.ToString(), out count))
            {
                if (list.Count >= count)
                {
                    return Visibility.Visible;
                }
            }

            return Visibility.Collapsed;
        }

        /// <summary>
        /// Convert back function. Not implemented here.
        /// </summary>
        /// <param name="value">The source value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>A bool value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
