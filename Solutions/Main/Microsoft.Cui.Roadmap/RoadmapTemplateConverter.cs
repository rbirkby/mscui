//-----------------------------------------------------------------------
// <copyright file="RoadMapTemplateConverter.cs" company="Microsoft Corporation copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation copyright 2007 - 2010.
// All rights reserved.
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>23-Jan-2009</date>
// <summary>RoadMapTemplateConverter class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Roadmap
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

    /// <summary>
    /// RoadMapTemplateConverter class.
    /// </summary>
    public class RoadmapTemplateConverter : System.Windows.Data.IValueConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoadmapTemplateConverter"/> class.
        /// </summary>
        public RoadmapTemplateConverter()
        {
        }

        #region IValueConverter Members

        /// <summary>
        /// Modifies the source data before passing it to the target for display in the UI.
        /// </summary>
        /// <param name="value">The source data being passed to the target.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the target dependency property.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the target dependency property.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            RoadmapItem item = value as RoadmapItem;
            FrameworkElement obj = null;
            if (null != item)
            {
                obj = item.Template.LoadContent() as FrameworkElement;
                if (null != obj)
                {
                    obj.DataContext = value;
                }
            }

            return obj;
        }

        /// <summary>
        /// Modifies the target data before passing it to the source object.  This method is called only in <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> bindings.
        /// </summary>
        /// <param name="value">The target data being passed to the source.</param>
        /// <param name="targetType">The <see cref="T:System.Type"/> of data expected by the source object.</param>
        /// <param name="parameter">An optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>
        /// The value to be passed to the source object.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
