//-----------------------------------------------------------------------
// <copyright file="FlyOutConceptNameConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Converts an InputFieldResult into a TextBlock.</summary>
//-----------------------------------------------------------------------
#if SILVERLIGHT
namespace Microsoft.Cui.SamplePages
#else
namespace Microsoft.Cui.SampleWinform
#endif
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;   
    using System.Windows.Documents; 
#if SILVERLIGHT
    using Microsoft.Cui.SamplePages.TerminologyProvider;
#else
    using Microsoft.Cui.SampleWinform.TerminologyProvider;
#endif

    /// <summary>
    /// Converts an InputFieldResult into a TextBlock.
    /// </summary>
    public class FlyOutConceptNameConverter : IValueConverter
    {
        #region IValueConverter Members
        /// <summary>
        /// Converts an InputFieldResult into a TextBlock.
        /// </summary>
        /// <param name="value">The InputFieldResult.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The font size parameter.</param>
        /// <param name="culture">The culture info.</param>
        /// <returns>A formatted textblock.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            TextBlock textBlock = new TextBlock();
            textBlock.TextWrapping = TextWrapping.Wrap;

            if (value.GetType() == typeof(InputFieldResult))
            {
                InputFieldResult result = (InputFieldResult)value;

                if (result.IsNegated)
                {
                    Run description = new Run();
                    description.Text = string.Format(culture, "No {0}, ", result.Description);
                    textBlock.Inlines.Add(description);

                    Run knownAbsence = new Run();
                    knownAbsence.FontStyle = FontStyles.Italic;
                    knownAbsence.Text = "known absence of ";
                    textBlock.Inlines.Add(knownAbsence);

                    Run fullySpecifiedName = new Run();
                    fullySpecifiedName.Text = result.Concept.FullySpecifiedName;
                    textBlock.Inlines.Add(fullySpecifiedName);
                }
                else if (result.Description != result.Concept.PreferredTerm)
                {
                    Run descriptionName = new Run();
                    descriptionName.Text = result.Description;
                    textBlock.Inlines.Add(descriptionName);

                    Run knownAbsence = new Run();
                    knownAbsence.FontSize = knownAbsence.FontSize - 1;
                    knownAbsence.Text = " synonym of";
                    textBlock.Inlines.Add(knownAbsence);
                }
                else
                {
                    Run fullySpecifiedName = new Run();
                    fullySpecifiedName.Text = result.Concept.FullySpecifiedName;
                    textBlock.Inlines.Add(fullySpecifiedName);
                }
            }
            else if (value.GetType() == typeof(ConceptResult))
            {
                ConceptResult result = (ConceptResult)value;
                Run fullySpecifiedName = new Run();
                fullySpecifiedName.Text = result.FullySpecifiedName;
                textBlock.Inlines.Add(fullySpecifiedName);
            }
            
            return textBlock;
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
