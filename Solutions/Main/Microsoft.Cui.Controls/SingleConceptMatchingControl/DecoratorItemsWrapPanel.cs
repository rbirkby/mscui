//-----------------------------------------------------------------------
// <copyright file="DecoratorItemsWrapPanel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>12-Dec-2008</date>
// <summary>The panel control for displaying decorator containers.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// The panel control for displaying decorator containers.
    /// </summary>
    public class DecoratorItemsWrapPanel : Panel
    {
        /// <summary>
        /// Stores the DecoratorItemsControl parent.
        /// </summary>
        private DecoratorItemsControl decoratorItemsControl;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DecoratorItemsWrapPanel"/> class.
        /// </summary>
        public DecoratorItemsWrapPanel()
        {
            // Some differences in Margin rendering between Silverlight and WPF.
#if SILVERLIGHT
            this.UseLayoutRounding = false;
#endif
        }

        /// <summary>
        /// Measure override.
        /// </summary>
        /// <param name="availableSize">The size available for the control.</param>
        /// <returns>The desired size of the control.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            return base.MeasureOverride(availableSize);
        }

        /// <summary>
        /// Arrange override.
        /// <para>
        /// The arrange override works through the text working out where the text is expected to wrap. To work this
        /// out, we create a TextBlock on the fly that has the same maximum width as the wrap panel, and start adding 
        /// a character one at a time until the TextBlock wraps.
        /// </para>
        /// <para>
        /// The characters are then travered both backwards and forwards to calculate which line in the wrapped TextBlock
        /// is responsible for the actual width of the TextBlock. Basically, calculating which is the widest line. This is
        /// achieved by creating 2 more TextBlocks, one for the forward pass, and one for the backward pass, and characters
        /// are again added one at a time until the width matches that of the first TextBlock.
        /// </para>
        /// <para>
        /// Once a width match is found, the width of the top line can be calculated. This is then set to be the width
        /// of the first text block. Characters are then removed from the end, one by one, until the TextBlock returns
        /// to just one line high. The text in that TextBlock then represents one line, and can be arranged.
        /// </para>
        /// <para>
        /// The differences between WPF and Silverlight here are to do with measuring the TextBlock as characters are added. In 
        /// Silverlight, the TextBlock's actual size is set immediately. In WPF, we need to call Measure, and then read the 
        /// desired size.
        /// </para>
        /// </summary>
        /// <param name="finalSize">The final size for the panel.</param>
        /// <returns>Returns the size of the panel.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            int count = 0;
            double left = 0;
            double top = 0;

            while (count < this.Children.Count)
            {
                int startIndex = count;
                TextBlock textBlock = this.CreateTextBlock();
#if SILVERLIGHT
                textBlock.Width = finalSize.Width;
#else
                textBlock.MaxWidth = finalSize.Width;
#endif

                textBlock.Text = this.GetTextAt(count);
                double oneLineHeight;
#if SILVERLIGHT
                oneLineHeight = textBlock.ActualHeight;
#else
                textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                oneLineHeight = textBlock.DesiredSize.Height;
#endif

                bool wrap = false;

                // This loop adds characters from the current position until the TextBlock
                // wraps. The loop is then broken.
                for (int i = count + 1; i < this.Children.Count; i++)
                {
                    textBlock.Text += this.GetTextAt(i);

                    double textBlockHeight;
#if SILVERLIGHT
                    textBlockHeight = textBlock.ActualHeight;
#else
                    textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    textBlockHeight = textBlock.DesiredSize.Height;
#endif

                    if (oneLineHeight != textBlockHeight)
                    {
                        count = i - 1;
                        wrap = true;
                        break;
                    }
                }

                // If there was a wrap...
                if (wrap)
                {
                    double textBlockWidth;
#if SILVERLIGHT
                    textBlockWidth = textBlock.ActualWidth;
#else
                    textBlockWidth = textBlock.DesiredSize.Width;
#endif

                    // Create a forward and backward pass TextBlock
                    TextBlock forwardPassTextBlock = this.CreateTextBlock();
                    TextBlock backwardPassTextBlock = this.CreateTextBlock();
                    bool breakFound = false;
                    int passCount = 0;

                    double forwardPassTextBlockWidth = 0;
                    double backwardPassTextBlockWidth = 0;
                    
                    while (!breakFound)
                    {
                        forwardPassTextBlock.Text = string.Empty;
                        backwardPassTextBlock.Text = string.Empty;

                        // Add characters one at a time until either the forward or backward pass
                        // TextBlock reaches the desired width.
                        for (int i = 0 + passCount; i < textBlock.Text.Length; i++)
                        {
                            forwardPassTextBlock.Text += textBlock.Text[i];
                            backwardPassTextBlock.Text = backwardPassTextBlock.Text.Insert(0, textBlock.Text[textBlock.Text.Length - i - 1].ToString(CultureInfo.CurrentCulture));
#if SILVERLIGHT
                            forwardPassTextBlockWidth = forwardPassTextBlock.ActualWidth;
                            backwardPassTextBlockWidth = backwardPassTextBlock.ActualWidth;
#else
                            forwardPassTextBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                            forwardPassTextBlockWidth = forwardPassTextBlock.DesiredSize.Width;

                            backwardPassTextBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                            backwardPassTextBlockWidth = backwardPassTextBlock.DesiredSize.Width;
#endif

                            if (forwardPassTextBlockWidth == textBlockWidth)
                            {
                                // If the width was matched in the forward pass, break out of the loop.
                                breakFound = true;
                                break;
                            }
                            else if (backwardPassTextBlockWidth == textBlockWidth)
                            {
                                // If the width was matched in the backward pass, store the character index and flag break found.
                                backwardPassTextBlock.Text = backwardPassTextBlock.Text.Remove(backwardPassTextBlock.Text.Length - 1);
#if SILVERLIGHT
                                backwardPassTextBlockWidth = backwardPassTextBlock.ActualWidth;
#else
                                backwardPassTextBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                                backwardPassTextBlockWidth = backwardPassTextBlock.DesiredSize.Width;
#endif
                                breakFound = true;
                            }
                        }

#if SILVERLIGHT
                        forwardPassTextBlockWidth = forwardPassTextBlock.ActualWidth;
#else
                        forwardPassTextBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                        forwardPassTextBlockWidth = forwardPassTextBlock.DesiredSize.Width;
#endif
                        passCount++;

                        if (passCount == textBlock.Text.Length)
                        {
                            break;
                        }
                    }

                    // Set the TextBlock width to match that of the first line of text.
                    textBlock.Width = forwardPassTextBlockWidth;
                    int textBlockLength = textBlock.Text.Length;

                    // This loop works backwards through the text block, removing the characters
                    // until the TextBlock is just one line high again.
                    for (int i = textBlockLength - 1; i > 0; i--)
                    {
                        textBlock.Text = textBlock.Text.Remove(i);

                        double textBlockHeight;
#if SILVERLIGHT
                        textBlockHeight = textBlock.ActualHeight;
#else
                        textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                        textBlockHeight = textBlock.DesiredSize.Height;
#endif

                        if (textBlockHeight == oneLineHeight)
                        {
                            break;
                        }
                    }
                }

                double highest = 0;
                count = startIndex + textBlock.Text.Length;

                // Arrange the items on the current line.
                for (int i = startIndex; i < count; i++)
                {
                    this.Children[i].Measure(finalSize);
                    this.Children[i].Arrange(
                        new Rect(left, top, this.Children[i].DesiredSize.Width, this.Children[i].DesiredSize.Height));

                    ((DecoratorItemContainer)this.Children[i]).Top = top;

                    left += this.Children[i].DesiredSize.Width;

                    if (highest < this.Children[i].DesiredSize.Height)
                    {
                        highest = this.Children[i].DesiredSize.Height;
                    }
                }

                top += highest;
                left = 0;
            }

            return base.ArrangeOverride(finalSize);
        }

        /// <summary>
        /// Creates a textblock with the correct styles applied.
        /// </summary>
        /// <returns>A textblock with the correct style.</returns>
        private TextBlock CreateTextBlock()
        {
            if (this.decoratorItemsControl == null)
            {
                DependencyObject currentObject = VisualTreeHelper.GetParent(this);
                while (currentObject != null)
                {
                    if (currentObject.GetType() == typeof(DecoratorItemsControl))
                    {
                        this.decoratorItemsControl = (DecoratorItemsControl)currentObject;
                        break;
                    }

                    currentObject = VisualTreeHelper.GetParent(currentObject);
                }
            }

            if (this.decoratorItemsControl != null)
            {
                return new TextBlock()
                {
                    FontFamily = this.decoratorItemsControl.FontFamily,
                    FontSize = this.decoratorItemsControl.FontSize,
                    FontStyle = this.decoratorItemsControl.FontStyle,
                    FontWeight = this.decoratorItemsControl.FontWeight,
                    TextWrapping = TextWrapping.Wrap,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
            }
            else
            {
                return new TextBlock()
                {
                    TextWrapping = TextWrapping.Wrap,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
            }
        }

        /// <summary>
        /// Gets the full text for all of the panel items.
        /// </summary>
        /// <returns>The full text.</returns>
        private string GetFullText()
        {
            string text = string.Empty;
            for (int i = 0; i < this.Children.Count; i++)
            {
                text += this.GetTextAt(i);
            }

            return text;
        }

        /// <summary>
        /// Gets the text at a specified index.
        /// </summary>
        /// <param name="index">The index to get the text at.</param>
        /// <returns>The text at the specified index.</returns>
        private string GetTextAt(int index)
        {
            if (this.Children.Count > index)
            {
                return ((DecoratorTextBoxItem)((DecoratorItemContainer)this.Children[index]).Content).Text;
            }

            return string.Empty;
        }
    }
}
