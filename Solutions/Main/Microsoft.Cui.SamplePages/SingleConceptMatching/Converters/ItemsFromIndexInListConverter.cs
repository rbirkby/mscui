//-----------------------------------------------------------------------
// <copyright file="ItemsFromIndexInListConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Converts a list into a sublist, starting from the specified index.</summary>
//-----------------------------------------------------------------------
#if SILVERLIGHT
namespace Microsoft.Cui.SamplePages
#else
namespace Microsoft.Cui.SampleWinform
#endif
{
    using System;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.Windows.Data;
    using System.Collections;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Converts a list into a sublist, starting from the specified index.
    /// </summary>
    public class ItemsFromIndexInListConverter : IValueConverter
    {

        #region IValueConverter Members

        /// <summary>
        /// Converts a list into a sublist, starting from a specified index.
        /// </summary>
        /// <param name="value">The IList source.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The start index.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>The requested item.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            IList list = (IList)value;
            int index = 0;
            ObservableCollection<object> items = new ObservableCollection<object>();

            if (int.TryParse(parameter.ToString(), out index) && list.Count > index)
            {
                for (int i = index; i < list.Count; i++)
                {
                    items.Add(list[i]);
                }
            }

            return items;
        }

        /// <summary>
        /// Convert back. Not implemented.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>The retun value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
