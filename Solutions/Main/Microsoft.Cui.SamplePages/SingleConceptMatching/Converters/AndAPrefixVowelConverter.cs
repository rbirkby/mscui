//-----------------------------------------------------------------------
// <copyright file="AndAPrefixVowelConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Selects between 'and an' and 'and a' dependent on the start of the next word.</summary>
//-----------------------------------------------------------------------
#if SILVERLIGHT
namespace Microsoft.Cui.SamplePages
#else
namespace Microsoft.Cui.SampleWinform
#endif
{
    using System;
    using System.Windows.Data;

    /// <summary>
    /// Selects between 'and an' and 'and a' dependent on the start of the next word.
    /// </summary>
    public class AndAPrefixVowelConverter : IValueConverter
    {
        #region IValueConverter Members
        /// <summary>
        /// Returns 'and a' or 'and an' based on the start of the next word.
        /// </summary>
        /// <param name="value">The next piece of text.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>Either 'and a' or 'and an'.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string text = value.ToString().ToUpper(culture);
            if (text.StartsWith("A", StringComparison.CurrentCulture) ||
                text.StartsWith("E", StringComparison.CurrentCulture) ||
                text.StartsWith("I", StringComparison.CurrentCulture) ||
                text.StartsWith("O", StringComparison.CurrentCulture) ||
                text.StartsWith("U", StringComparison.CurrentCulture))
            {
                return "and an";
            }
            else
            {
                return "and a";
            }
        }

        /// <summary>
        /// Convert back. Not implemented.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>The return value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
