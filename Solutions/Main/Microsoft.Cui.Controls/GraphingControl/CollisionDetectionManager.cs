//-----------------------------------------------------------------------
// <copyright file="CollisionDetectionManager.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>14-11-2008</date>
// <summary>Collision Detection.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Collision Detection Manager Class.
    /// </summary>
    public class CollisionDetectionManager
    {        
        /// <summary>
        /// Element lookup table.
        /// </summary>
        private Dictionary<double, LinkedList<FrameworkElement>> elementLookupToX = new System.Collections.Generic.Dictionary<double, LinkedList<FrameworkElement>>();

        /// <summary>
        /// List to hold the event markers.
        /// </summary>
        private List<FrameworkElement> eventMarkers = new List<FrameworkElement>();

        /// <summary>
        /// Gets the list of elements and their x co-ordinates.
        /// </summary>
        /// <value>Elements and their x co-ordinates.</value>
        internal Dictionary<double, LinkedList<FrameworkElement>> ElementLookupToX
        {
            get { return this.elementLookupToX; }
        }

        /// <summary>
        /// Register And Test If Overlaps.
        /// </summary>
        /// <param name="element">Element to register.</param>
        /// <returns>The clash element.</returns>
        public FrameworkElement RegisterAndTestIfMarkerOverlaps(FrameworkElement element)
        {
            FrameworkElement result = null;
            result = this.Register(element, false);
            return result;                
        }

        /// <summary>
        /// Register And Test If Overlaps.
        /// </summary>
        /// <param name="element">Element to register.</param>
        /// <returns>True if registered.</returns>
        public bool RegisterAndTestIfLabelOverlaps(FrameworkElement element)
        {
            bool result = false;
            result = (null != this.Register(element, true));
            return result;
        }

        /// <summary>
        /// Registers the element and tests if event marker overlaps.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// Returns the element with which the clash has occurred, if no element returns null.
        /// </returns>
        public FrameworkElement RegisterAndTestIfEventMarkerOverlaps(FrameworkElement element)
        {
            FrameworkElement result = null;
            result = this.RegisterEventMarker(element);
            return result;                
        }

        /// <summary>
        /// Registers the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="label">Tells method if this is a label.</param>
        /// <returns>The clash element.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "label", Justification = "The label is only used in WPF, so it is used, just not in this compile.")]
        private FrameworkElement Register(FrameworkElement element, bool label)
        {
            FrameworkElement result = null;

            double left = Canvas.GetLeft(element);

            bool pad = false;

            if (left % 1 > 0)
            {
                // add 1 to the upper x so we include all pixels we touch
                pad = true;
            }

            int lowerX = (int)left;            

            // The code below performs the measuring of the elements.
            // Wpf and Silverlight behave in different ways, so the code
            // is built for both.
            // SL will evaluate the size of a textblock immediately
            // but WPF does not. Also, Binding happens immediately but in WPF it does not.

            // For the above reasons we restrict the label template to be a border and a textblock,
            // and we assume the binding is to the ToString property of the bound
            // TimePoint.

            // The code here ensures that any TextBlock has its height and width
            // Set explicitly so the measure clal will report the correct size.
#if SILVERLIGHT
            int upperX = (int)(lowerX + element.ActualWidth);

            if (0 == element.ActualWidth)
            {
                element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                upperX = (int)(lowerX + element.DesiredSize.Width);
            }
#else
            Visibility rememberVisibility = element.Visibility;
            element.Visibility = Visibility.Visible;
            int upperX;

            if (true == label)
            {
                Border border = element as Border;
                if (null != border)
                {
                    TextBlock textBlock = border.FindName("ELEMENT_labelTextBlock") as TextBlock;
                    if (null != textBlock)
                    {
                        GraphPoint gp = textBlock.DataContext as GraphPoint;                        
                        if (null != gp.DataContext)
                        {
                            if (string.IsNullOrEmpty(textBlock.Text))
                            {
                                // WPF will not have bound the text for the label.
                                // The default template for the label is ToString.
                                // We will measure based off of this for WPF.
                                // Should the developer using this control not want
                                // to use the default binding of ToString of their supplied
                                // object then this will need to be changed to set 
                                // the text to what they require.
                                textBlock.Text = gp.DataContext.ToString();
                            }
                        }
                    }
                }
            }            

            element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            upperX = (int)(lowerX + element.DesiredSize.Width); 
            element.Visibility = rememberVisibility;
#endif

            if (true == pad)
            {
                upperX++;
            }

            for (int i = lowerX; i < upperX; i++)
            {
                this.RegisterXToElement(element, i);                
            }

            result = this.DoesElementClash(element);

            return result;
        }

        /// <summary>
        /// Registers the X to element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="x">The x that we should register against.</param>
        private void RegisterXToElement(FrameworkElement element, int x)
        {
            LinkedList<FrameworkElement> list;
            if (false == this.elementLookupToX.ContainsKey(x))
            {
                list = new LinkedList<FrameworkElement>();
                this.elementLookupToX.Add(x, list);
            }
            else
            {
                list = this.elementLookupToX[x];
            }

            list.AddLast(element);
        }

        /// <summary>
        /// Doeses the element clash.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The clash element.</returns>
        private FrameworkElement DoesElementClash(FrameworkElement element)
        {
            FrameworkElement result = null;            
            double leftPointX = Canvas.GetLeft(element);

            double elementWidth = 0;
            elementWidth = element.ActualWidth;
            if (0 == element.ActualWidth)
            {
                element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                elementWidth = element.DesiredSize.Width;
            }

            for (int x = (int)(leftPointX + elementWidth); x >= (int)leftPointX; x--)
            {
                if (true == this.elementLookupToX.ContainsKey(x))
                {
                    foreach (FrameworkElement clashElement in this.elementLookupToX[x])
                    {
                        if (clashElement != element)
                        {
                            // check the X as well because we register against an Int
                            // not a double, so it is possible they do not overlap
                            // as one may finish at say 6.2, and the next might start at 6.4.
                            double clashElementWidth = 0;
                            clashElementWidth = clashElement.ActualWidth;
                            if (0 == clashElement.ActualWidth)
                            {
                                clashElement.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                                clashElementWidth = clashElement.DesiredSize.Width;
                            }

                            double clashElementLeft = Canvas.GetLeft(clashElement);
                            double elementLeft = Canvas.GetLeft(element);

                            // the middle point should fall in betweent eh left and right if this is to be a collision.
                            double leftPoint = 0;
                            double middlePoint = 0;
                            double rightPoint = 0;
                            
                            bool performHeightChecks = false;

                            // if non of these then they are the same and must overlap on X
                            if (clashElementLeft < elementLeft)
                            {
                                // clash element right point should be between start and end point of element
                                middlePoint = elementLeft;                                
                                leftPoint = clashElementLeft;
                                rightPoint = clashElementLeft + clashElementWidth;                                
                            }
                            else if (clashElementLeft > elementLeft)
                            {
                                // element right point should be between start and end point of clash element
                                 middlePoint = clashElementLeft;
                                 leftPoint = elementLeft;
                                 rightPoint = elementLeft + elementWidth;
                            }
                            else
                            {
                                // they are at same x position.
                                performHeightChecks = true;
                            }

                            if (middlePoint > leftPoint && middlePoint < rightPoint)
                            {
                                performHeightChecks = true;
                            }

                            if (true == performHeightChecks)
                            {
                                double clashHeight = clashElement.ActualHeight;
                                double elementHeight = element.ActualHeight;

                                if (0 == clashHeight)
                                {
                                    clashElement.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                                    clashHeight = clashElement.DesiredSize.Height;
                                }

                                if (0 == elementHeight)
                                {
                                    element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                                    elementHeight = element.DesiredSize.Height;
                                }

                                double topOfClash = Canvas.GetTop(clashElement);
                                double bottomOfClash = topOfClash + clashHeight;

                                double topOfElement = Canvas.GetTop(element);
                                double bottomOfElement = topOfElement + elementHeight;

                                if ((topOfElement >= topOfClash && topOfElement <= bottomOfClash) ||
                                    (bottomOfElement >= topOfClash && bottomOfElement <= bottomOfClash))
                                {
                                    // we have a clash
                                    result = clashElement;
                                }

                                if ((topOfClash >= topOfElement && topOfClash <= bottomOfElement) ||
                                    (bottomOfClash >= topOfElement && bottomOfClash <= bottomOfElement))
                                {
                                    // we have a clash
                                    result = clashElement;
                                }

                                if (false == GraphBase.GetDataPoint(clashElement))
                                {
                                    // this element is a label
                                    if (true == GraphBase.GetDataPointOverlapProperty(clashElement))
                                    {
                                        // this element will not be visible so it does not clash
                                        result = null;
                                    }
                                }
                            }

                            if (null != result)
                            {
                                break;
                            }
                        }
                    }
                }

                if (null != result)
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Registers the event marker.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>Returns the marker with which the event marker clashes.</returns>
        private FrameworkElement RegisterEventMarker(FrameworkElement element)
        {
            FrameworkElement result = null;
            if (!this.eventMarkers.Contains(element))
            {
                this.eventMarkers.Add(element);
            }

            result = this.DoesEventMarkerClash(element);

            return result;
        }

        /// <summary>
        /// Checks whether the event marker overlaps.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>Element with which the marker overlaps.</returns>
        private FrameworkElement DoesEventMarkerClash(FrameworkElement element)
        {
            FrameworkElement result = null;            
            Size elementSize = new Size(element.ActualWidth, element.ActualHeight);
            if (0 == element.ActualWidth)
            {
                element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                elementSize = element.DesiredSize;
            }
            
            foreach (FrameworkElement clashElement in this.eventMarkers)
            {
                Rect elementBounds = new Rect(new Point(Canvas.GetLeft(element), Canvas.GetTop(element)), elementSize);            
                if (clashElement != element)
                {
                    Size clashElementSize = new Size(clashElement.ActualWidth, clashElement.ActualHeight);
                    if (0 == clashElement.ActualWidth)
                    {
                        clashElement.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                        clashElementSize = clashElement.DesiredSize;
                    }

                    Rect clashElementBounds = new Rect(new Point(Canvas.GetLeft(clashElement), Canvas.GetTop(clashElement)), clashElementSize);

                    elementBounds.Intersect(clashElementBounds);
                    if (!elementBounds.IsEmpty && elementBounds.Width > 1 && elementBounds.Height > 1)
                    {
                        result = clashElement;
                        break;
                    }
                }                
            }

            return result;
        }
    }
}
