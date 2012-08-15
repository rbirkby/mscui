//-----------------------------------------------------------------------
// <copyright file="TextBlockHelper.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>09-July-2008</date>
// <summary>Class to provide information about text block elements.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Globalization;
    #endregion

    /// <summary>
    /// Helper class to provide utility information about text block elements.
    /// </summary>
    internal class TextBlockHelper
    {
        /// <summary>
        /// Initializes an instance of TextBlock helper.
        /// </summary>
        /// <remarks>Private Ctor will prohibit the class from being instantiated.</remarks>
        private TextBlockHelper()
        {
        }

        /// <summary>
        /// Gets the width required for the text block element without wrapping.
        /// </summary>
        /// <param name="textElement">Textblock whose width needs to be calculated.</param>
        /// <returns>Returns the width needed to show the text without wrapping.</returns>
        public static double GetDesiredWidth(TextBlock textElement)
        {
            double desiredWidth = 0;
#if SILVERLIGHT
            TextWrapping wrap = textElement.TextWrapping;
            textElement.TextWrapping = TextWrapping.NoWrap;
            desiredWidth = textElement.ActualWidth;
            textElement.TextWrapping = wrap;
#else
            if (!string.IsNullOrEmpty(textElement.Text))
            {
                FormattedText formattedText = new FormattedText(textElement.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new System.Windows.Media.Typeface(textElement.FontFamily.Source), textElement.FontSize, textElement.Foreground);
                formattedText.SetFontStyle(textElement.FontStyle);
                formattedText.SetFontWeight(textElement.FontWeight);
                desiredWidth = formattedText.WidthIncludingTrailingWhitespace;
            }
            else
            {
                desiredWidth = 0;
            }    
#endif

            return desiredWidth;
        }

        /// <summary>
        /// Gets the height required for the text block element without wrapping.
        /// </summary>
        /// <param name="textElement">Textblock whose width needs to be calculated.</param>
        /// <returns>Returns the height needed to show the text without wrapping.</returns>
        public static double GetDesiredHeight(TextBlock textElement)
        {
            double desiredHeight = 0;
#if SILVERLIGHT
            TextWrapping wrap = textElement.TextWrapping;
            textElement.TextWrapping = TextWrapping.NoWrap;
            desiredHeight = textElement.ActualHeight;
            textElement.TextWrapping = wrap;
#else
            if (!string.IsNullOrEmpty(textElement.Text))
            {
                FormattedText formattedText = new FormattedText(textElement.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new System.Windows.Media.Typeface(textElement.FontFamily.Source), textElement.FontSize, textElement.Foreground);
                formattedText.SetFontStyle(textElement.FontStyle);
                formattedText.SetFontWeight(textElement.FontWeight);
                desiredHeight = formattedText.Height;
            }
            else
            {
                desiredHeight = 0;
            }
#endif

            return desiredHeight;
        }
    }
}
