//-----------------------------------------------------------------------
// <copyright file="CompletedPrescriptionToMedsLineConverter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>15-Oct-2009</date>
// <summary>Converts a completed prescription into a meds line text block.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Media;

    /// <summary>
    /// Converts a string to lower case.
    /// </summary>
    public class CompletedPrescriptionToMedsLineConverter : IValueConverter
    {
        /// <summary>
        /// The non breaking character.
        /// </summary>
        private const string NonBreakingCharacter = "A";

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
            CompletedPrescription prescription = value as CompletedPrescription;
            if (prescription != null)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.TextWrapping = TextWrapping.Wrap;

                if (!string.IsNullOrEmpty(prescription.Name))
                {
                    string[] ingredients = prescription.Name.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < ingredients.Length; i++)
                    {
                        CompletedPrescriptionToMedsLineConverter.ReplaceHyphensWithNonBreakingCharacter(textBlock, ingredients[i].Trim(), FontWeights.Bold);
                        if (i < ingredients.Length - 1)
                        {
                            CompletedPrescriptionToMedsLineConverter.AddCharacterJoin(textBlock);
                            textBlock.Inlines.Add(new Run()
                            {
                                Text = "+ ",
                                FontWeight = FontWeights.Bold
                            });
                        }
                    }
                }

                if (!string.IsNullOrEmpty(prescription.BrandName))
                {
                    AddCharacterJoin(textBlock);

                    textBlock.Inlines.Add(new Run()
                    {
                        Text = "― "
                    });

                    string[] parts = prescription.BrandName.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < parts.Length; i++)
                    {
                        CompletedPrescriptionToMedsLineConverter.ReplaceHyphensWithNonBreakingCharacter(textBlock, parts[i].Trim(), FontWeights.Normal);
                        if (i < parts.Length - 1)
                        {
                            CompletedPrescriptionToMedsLineConverter.AddCharacterJoin(textBlock);
                            textBlock.Inlines.Add(new Run()
                            {
                                Text = "+ ",
                            });
                        }
                    }                     
                }

                if (!string.IsNullOrEmpty(prescription.Strength))
                {
                    textBlock.Inlines.Add(new Run()
                    {
                        Text = " "
                    });

                    AddNonBreakingInlines(textBlock, prescription.Strength, FontWeights.Normal);
                }

                if (!string.IsNullOrEmpty(prescription.Form))
                {
                    AddCharacterJoin(textBlock);

                    textBlock.Inlines.Add(new Run()
                    {
                        Text = "― "
                    });

                    AddNonBreakingInlines(textBlock, prescription.Form, FontWeights.Normal);
                }

                if (!string.IsNullOrEmpty(prescription.Dose))
                {
                    AddCharacterJoin(textBlock);
                    textBlock.Inlines.Add(new Run()
                    {
                        Text = "― "
                    });

                    textBlock.Inlines.Add(new Run()
                    {
                        Text = "DOSE",
                        FontFamily = new FontFamily("Courier New"),
                        Foreground = SystemParameters.HighContrast ? new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xff, 0xf0)) : new SolidColorBrush(Color.FromArgb(0xff, 0x50, 0x8d, 0xc2))
                    });

                    AddCharacterJoin(textBlock);

                    AddNonBreakingInlines(textBlock, prescription.Dose, FontWeights.Bold);
                }

                if (!string.IsNullOrEmpty(prescription.Method))
                {
                    AddCharacterJoin(textBlock);

                    textBlock.Inlines.Add(new Run()
                    {
                        Text = "― "
                    });

                    textBlock.Inlines.Add(new Run()
                    {
                        Text = "METHOD",
                        FontFamily = new FontFamily("Courier New"),
                        Foreground = SystemParameters.HighContrast ? new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0xff, 0xf0)) : new SolidColorBrush(Color.FromArgb(0xff, 0x50, 0x8d, 0xc2))
                    });

                    AddCharacterJoin(textBlock);

                    AddNonBreakingInlines(textBlock, prescription.Method, FontWeights.Normal);
                }

                if (!string.IsNullOrEmpty(prescription.Site))
                {
                    AddCharacterJoin(textBlock);

                    textBlock.Inlines.Add(new Run()
                    {
                        Text = "― "
                    });

                    AddNonBreakingInlines(textBlock, prescription.Site, FontWeights.Normal);
                }

                if (!string.IsNullOrEmpty(prescription.Route))
                {
                    AddCharacterJoin(textBlock);

                    textBlock.Inlines.Add(new Run()
                    {
                        Text = "― "
                    });

                    AddNonBreakingInlines(textBlock, prescription.Route, FontWeights.Normal);
                }

                if (!string.IsNullOrEmpty(prescription.Frequency))
                {
                    AddCharacterJoin(textBlock);

                    textBlock.Inlines.Add(new Run()
                    {
                        Text = "― "
                    });

                    AddNonBreakingInlines(textBlock, prescription.Frequency, FontWeights.Normal);
                }

                if (prescription.IsAsRequired)
                {
                    AddCharacterJoin(textBlock);

                    textBlock.Inlines.Add(new Run()
                    {
                        Text = "― "
                    });

                    AddNonBreakingInlines(textBlock, "as required", FontWeights.Normal);
                }

                return textBlock;
            }

            return null;
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

        /// <summary>
        /// Adds inlines for text that will not break unless necessary.
        /// </summary>
        /// <param name="textBlock">The text block to add the inlines too.</param>
        /// <param name="text">The text to add.</param>
        /// <param name="fontWeight">The desired font weight of the text.</param>
        private static void AddNonBreakingInlines(TextBlock textBlock, string text, FontWeight fontWeight)
        {
            // Split text into parts around a long dash
            // e.g. oral ― modified-release becomes "oral", "modified-release"
            string[] parts = text.Split("―".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (text.StartsWith("― ", StringComparison.CurrentCulture))
            {
                textBlock.Inlines.Add(new Run()
                {
                    Text = "― ",
                    FontWeight = fontWeight,
                });
            }

            for (int p = 0; p < parts.Length; p++)
            {
                // Split words by space,
                string[] words = parts[p].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < words.Length; i++)
                {
                    CompletedPrescriptionToMedsLineConverter.ReplaceHyphensWithNonBreakingCharacter(textBlock, words[i], fontWeight);
                    if (i != words.Length - 1)
                    {
                        AddCharacterJoin(textBlock);
                    }
                }

                if (p != parts.Length - 1)
                {
                    AddCharacterJoin(textBlock);

                    textBlock.Inlines.Add(new Run()
                    {
                        Text = "― "
                    });
                }
            }
        }

        /// <summary>
        /// Replaces the hyphens with a non breaking character.
        /// </summary>
        /// <param name="textBlock">The text block to add the inlines too.</param>
        /// <param name="text">The text to replace the hyphens with.</param>
        /// <param name="fontWeight">The font weight.</param>
        private static void ReplaceHyphensWithNonBreakingCharacter(TextBlock textBlock, string text, FontWeight fontWeight)
        {
            // Split words by hyphen breaks (to prevent wrapping around the hyphen).
            string[] breaks = text.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int b = 0; b < breaks.Length; b++)
            {
                textBlock.Inlines.Add(new Run()
                {
                    Text = breaks[b].Trim(),
                    FontWeight = fontWeight,
                });

                // Add a character that looks like a hyphen, but will not wrap.
                if (b < breaks.Length - 1)
                {
                    // Add small white space.
                    textBlock.Inlines.Add(new Run()
                    {
                        Text = CompletedPrescriptionToMedsLineConverter.NonBreakingCharacter,
                        FontSize = 2,
                        Foreground = new SolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00))
                    });

                    // Add similar to hyphen character.
                    textBlock.Inlines.Add(new Run()
                    {
                        Text = "¯",
                        FontSize = 5,
                    });

                    // Add some more small white space.
                    textBlock.Inlines.Add(new Run()
                    {
                        Text = CompletedPrescriptionToMedsLineConverter.NonBreakingCharacter,
                        FontSize = 2,
                        Foreground = new SolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00))
                    });
                }
            }
        }

        /// <summary>
        /// Adds a character join in line (that joins 2 character togetherm without breaking).
        /// </summary>
        /// <param name="textBlock">The textblock to add the join to.</param>
        private static void AddCharacterJoin(TextBlock textBlock)
        {
            textBlock.Inlines.Add(new Run()
            {
                Text = CompletedPrescriptionToMedsLineConverter.NonBreakingCharacter,
                FontSize = 8,
                Foreground = new SolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00))
            });
        }
        #endregion
    }
}
