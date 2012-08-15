//-----------------------------------------------------------------------
// <copyright file="IndentedLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>19-Jan-2009</date>
// <summary>A label type control providing wrapping with an indentation.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// A label type control providing wrapping with an indentation.
    /// </summary>
    public class IndentedLabel : Control
    {
        /// <summary>
        /// The Text Dependency Property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(IndentedLabel), new PropertyMetadata(new PropertyChangedCallback(LayoutProperty_Changed)));
        
        /// <summary>
        /// The Maximum Lines Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MaxLinesProperty =
            DependencyProperty.Register("MaxLines", typeof(int), typeof(IndentedLabel), new PropertyMetadata(new PropertyChangedCallback(LayoutProperty_Changed)));

        /// <summary>
        /// The Indent Character Count dependency property.
        /// </summary>        
        public static readonly DependencyProperty IndentCharacterCountProperty =
            DependencyProperty.Register("IndentCharacterCount", typeof(int), typeof(IndentedLabel), new PropertyMetadata(new PropertyChangedCallback(LayoutProperty_Changed)));

        /// <summary>
        /// The template part name for the stack panel.
        /// </summary>
        private const string ElementStackPanel = "StackPanel";

        /// <summary>
        /// Stores the stack panel.
        /// </summary>
        private StackPanel stackPanel;

        /// <summary>
        /// Stores the control size.
        /// </summary>
        private Size controlSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndentedLabel"/> class.
        /// </summary>
        public IndentedLabel()
        {
            this.DefaultStyleKey = typeof(IndentedLabel);
#if SILVERLIGHT
            this.UseLayoutRounding = false;
#endif      
            this.IsTabStop = false;
        }

        /// <summary>
        /// Gets or sets the labels text.
        /// </summary>
        /// <value>The text value.</value>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the maximum lines for the control.
        /// </summary>
        /// <value>The maximum lines value.</value>
        public int MaxLines
        {
            get { return (int)GetValue(MaxLinesProperty); }
            set { SetValue(MaxLinesProperty, value); }
        }

        /// <summary>
        /// Gets or sets the indent character count.
        /// </summary>
        /// <value>The indent character count.</value>
        public int IndentCharacterCount
        {
            get { return (int)GetValue(IndentCharacterCountProperty); }
            set { SetValue(IndentCharacterCountProperty, value); }
        }

        /// <summary>
        /// Gets the template parts from the template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.stackPanel = (StackPanel)this.GetTemplateChild(IndentedLabel.ElementStackPanel);            
            this.UpdateLabel();
        }

        /// <summary>
        /// Stores the available size for the control.
        /// </summary>
        /// <param name="availableSize">The available size.</param>
        /// <returns>Result of the base measure.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            this.controlSize = new Size(Math.Min(double.MaxValue, base.MeasureOverride(availableSize).Width), base.MeasureOverride(availableSize).Height);

            if (!double.IsPositiveInfinity(availableSize.Width) && availableSize.Width > this.controlSize.Width)
            {
                this.controlSize.Width = availableSize.Width;
            }
            
            this.UpdateLabel();
            return this.controlSize;
        }

        /// <summary>
        /// Updates the text labels.
        /// </summary>
        /// <param name="dependencyObject">The indented label control.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void LayoutProperty_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            IndentedLabel indentedLabel = (IndentedLabel)dependencyObject;
            indentedLabel.UpdateLabel();
        }

        /// <summary>
        /// Updates the text displayed in the control.
        /// <para>
        /// The method is used to work out where to wrap the text, creating a string for each line
        /// and finally adding a textblock to a stack panel for each line. When calculating the
        /// wrapping, the indentation size and ellipses size is taken into account also.
        /// </para>
        /// <para>
        /// The arrange override works through the text working out where the text is expected to wrap. To work this
        /// out, we create a TextBlock on the fly that has the same maximum width as the wrap panel, and start adding 
        /// a character one at a time until the TextBlock wraps.
        /// </para>
        /// <para>
        /// The actual width of the text block is then set to be the Width of the text block.
        /// Characters are then removed from the end, one by one, until the TextBlock returns
        /// to just one line high. The text in that TextBlock then represents one line. The string can then be stored
        /// as a line, and once each line is found, added to a stack panel of text blocks.
        /// </para>
        /// <para>
        /// The differences between WPF and Silverlight here are to do with measuring the TextBlock as characters are added. In 
        /// Silverlight, the TextBlock's actual size is set immediately. In WPF, we need to call Measure, and then read the 
        /// desired size.
        /// </para>
        /// </summary>
        private void UpdateLabel()
        {
            if (this.Text == null)
            {
                return;
            }

            List<string> lines = new List<string>();
            int count = 0;
            bool maxLinesReached = false;            

            while (count < this.Text.Length && !maxLinesReached)
            {
                int startIndex = count;
                TextBlock measureTextBlock = this.CreateTextBlock();
                measureTextBlock.Text += this.Text[count];
                double indentWidth = 0;

                if (lines.Count > 0)
                {
                    indentWidth += this.GetCharacterWidth(this.IndentCharacterCount);
                }

                if (lines.Count + 1 == this.MaxLines)
                {
                    indentWidth += this.GetEllipsesWidth();
                }

                double oneLineHeight;
#if SILVERLIGHT
                measureTextBlock.Width = Math.Max(0, this.controlSize.Width - indentWidth);
                oneLineHeight = measureTextBlock.ActualHeight;
#else
                measureTextBlock.MaxWidth = Math.Max(0, this.controlSize.Width - indentWidth);
                measureTextBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                oneLineHeight = measureTextBlock.DesiredSize.Height;
#endif
                bool wrap = false;
                for (int i = count + 1; i < this.Text.Length; i++)
                {
                    measureTextBlock.Text += this.Text[i];
                    double textBlockHeight;
#if SILVERLIGHT
                    textBlockHeight = measureTextBlock.ActualHeight;
#else
                    measureTextBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    textBlockHeight = measureTextBlock.DesiredSize.Height;
#endif

                    if (oneLineHeight != textBlockHeight)
                    {
                        count = i - 1;
                        wrap = true;
                        break;
                    }
                }

                if (wrap)
                {
                    if (this.MaxLines == 0 || lines.Count + 1 < this.MaxLines)
                    {
#if SILVERLIGHT
                        measureTextBlock.Width = measureTextBlock.ActualWidth;
#else
                        measureTextBlock.Width = measureTextBlock.DesiredSize.Width;
#endif
                        int textBlockLength = measureTextBlock.Text.Length;

                        // This loop works backwards through the text block, removing the characters
                        // until the TextBlock is just one line high again.
                        for (int i = textBlockLength - 1; i > 0; i--)
                        {
                            measureTextBlock.Text = measureTextBlock.Text.Remove(i);

                            double textBlockHeight;
#if SILVERLIGHT
                            textBlockHeight = measureTextBlock.ActualHeight;
#else
                            measureTextBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                            textBlockHeight = measureTextBlock.DesiredSize.Height;
#endif

                            if (textBlockHeight == oneLineHeight)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        measureTextBlock.Text = measureTextBlock.Text.Remove(measureTextBlock.Text.Length - 1);
                    }
                }

                count = startIndex + measureTextBlock.Text.Length;                
                if (this.MaxLines != 0 && this.MaxLines == lines.Count + 1)
                {
                    measureTextBlock.Text = measureTextBlock.Text.Trim();
                    maxLinesReached = true;
                }

                lines.Add(measureTextBlock.Text);
            }

            if (this.stackPanel != null)
            {
                double width = 0;

                this.stackPanel.Children.Clear();                
                for (int i = 0; i < lines.Count; i++)
                {
                    TextBlock textBlock = this.CreateTextBlock();
                    textBlock.TextWrapping = TextWrapping.NoWrap;
                    textBlock.Text = lines[i];

                    if (i == lines.Count - 1 && count < this.Text.Length)
                    {                        
                        textBlock.Text += "...";
                    }

                    if (i > 0)
                    {
                        textBlock.Margin = new Thickness(this.GetCharacterWidth(this.IndentCharacterCount), 0, 0, 0);
                    }

#if SILVERLIGHT
                    if (width < textBlock.ActualWidth + textBlock.Margin.Left)
                    {
                        width = textBlock.ActualWidth + textBlock.Margin.Left;
                    }
#else
                    textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    if (width < textBlock.DesiredSize.Width + textBlock.Margin.Left)
                    {
                        width = textBlock.DesiredSize.Width + textBlock.Margin.Left;
                    }
#endif

                    this.stackPanel.Children.Add(textBlock);
                }

                if (width > 0)
                {
                    this.controlSize.Width = width;
                }
            }
        }

        /// <summary>
        /// Creates a text block with the same properties as this control.
        /// </summary>
        /// <returns>A textblock with the same properties as this control.</returns>
        private TextBlock CreateTextBlock()
        {
            return new TextBlock()
            {
                FontFamily = this.FontFamily,
                FontSize = this.FontSize,
                FontStyle = this.FontStyle,
                FontWeight = this.FontWeight,
                TextWrapping = TextWrapping.Wrap,
                HorizontalAlignment = HorizontalAlignment.Left
            };
        }

        /// <summary>
        /// Gets the width of a specified amount of characters,.
        /// </summary>
        /// <param name="characterCount">The amount of characters.</param>
        /// <returns>The width of the specified count of characters.</returns>
        private double GetCharacterWidth(int characterCount)
        {
            TextBlock textBlock = this.CreateTextBlock();
            textBlock.FontFamily = new FontFamily("Courier New");
            string text = string.Empty;
            
            for (int i = 0; i < characterCount; i++)
            {
                text += "0";
            }

            textBlock.Text = text;
#if SILVERLIGHT
            return textBlock.ActualWidth;
#else
            textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return textBlock.DesiredSize.Width;
#endif
        }

        /// <summary>
        /// Gets the width of the ellipses.
        /// </summary>
        /// <returns>The width of the ellipses text.</returns>
        private double GetEllipsesWidth()
        {
            TextBlock textBlock = this.CreateTextBlock();
            textBlock.Text = "...";
#if SILVERLIGHT
            return textBlock.ActualWidth;
#else
            textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return textBlock.DesiredSize.Width;
#endif
        }
    }
}
