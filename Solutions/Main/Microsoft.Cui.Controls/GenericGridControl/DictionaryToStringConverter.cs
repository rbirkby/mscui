//-----------------------------------------------------------------------
// <copyright file="DictionaryToStringConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>11-Feb-2008</date>
// <summary>The DictionaryToStringConverter helper class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Collections.Generic;
    using System.Windows.Controls;

    /// <summary>
    /// The DictionaryToStringConverter helper class.
    /// </summary>
    public class DictionaryToStringConverter : System.Windows.Data.IValueConverter
    {        
        #region IValueConverter Members     

        /// <summary>
        /// Converts the specified value to the target type.
        /// </summary>
        /// <param name="value">Input value to convert.</param>
        /// <param name="targetType">Desired target type.</param>
        /// <param name="parameter">Conversion parameters.</param>
        /// <param name="culture">Culture settings.</param>
        /// <returns>The converted type.</returns>
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && parameter != null)
            {
                if (value.GetType() != typeof(System.Collections.Generic.Dictionary<string, string>))
                {
                    throw new System.ArgumentException("This value converter can only convert Dictionary Items of string string type.");
                }

                if (parameter.GetType() != typeof(string))
                {
                    throw new System.ArgumentException("The parameter for the converter was not specified. Please specify Key name as ConverterParameter");
                }                

                Dictionary<string, string> dictionary;
                dictionary = value as Dictionary<string, string>;
                if (null != dictionary)
                {
                    return dictionary[(string)parameter];
                }
            }

            return null;
        }

        /// <summary>
        /// Converts back to the original type from the target type.
        /// </summary>
        /// <param name="value">Input value to convert.</param>
        /// <param name="targetType">Desired target type.</param>
        /// <param name="parameter">Conversion parameters.</param>
        /// <param name="culture">Culture settings.</param>
        /// <returns>The original type.</returns>
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
