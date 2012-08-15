//-----------------------------------------------------------------------
// <copyright file="LinkingWrapPanel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010.
// All rights reserved
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
// <date>27-Aug-2009</date>
// <summary>
//      A wrap panel that links children together.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
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
    using System.Collections.Generic;

    /// <summary>
    /// A wrap panel that links children together.
    /// </summary>
    public class LinkingWrapPanel : Panel
    {
        /// <summary>
        /// The IsLinkToNextChild Attached Property.
        /// </summary>
        public static readonly DependencyProperty IsLinkedToNextChildProperty =
            DependencyProperty.RegisterAttached("IsLinkedToNextChild", typeof(bool), typeof(LinkingWrapPanel), new PropertyMetadata(false));

        /// <summary>
        /// The CanResize Attached Property.
        /// </summary>
        public static readonly DependencyProperty CanResizeProperty =
            DependencyProperty.RegisterAttached("CanResize", typeof(bool), typeof(LinkingWrapPanel), new PropertyMetadata(false));
       
        /// <summary>
        /// Gets whether the child is linked to the next child.
        /// </summary>
        /// <param name="obj">The child object.</param>
        /// <returns>Wether the child is linked to the next child.</returns>
        public static bool GetIsLinkedToNextChild(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsLinkedToNextChildProperty);
        }

        /// <summary>
        /// Sets whether the child is linked to the next child.
        /// </summary>
        /// <param name="obj">The child object.</param>
        /// <param name="value">Whether the child is linked to the next child.</param>
        public static void SetIsLinkedToNextChild(DependencyObject obj, bool value)
        {
            obj.SetValue(IsLinkedToNextChildProperty, value);
        }

        /// <summary>
        /// Gets whether an element can resize.
        /// </summary>
        /// <param name="obj">The element to check.</param>
        /// <returns>Whether the element can resize.</returns>
        public static bool GetCanResize(DependencyObject obj)
        {
            return (bool)obj.GetValue(CanResizeProperty);
        }

        /// <summary>
        /// Sets whether the element can resize or not.
        /// </summary>
        /// <param name="obj">The element to set.</param>
        /// <param name="value">Whether the element can resize.</param>
        public static void SetCanResize(DependencyObject obj, bool value)
        {
            obj.SetValue(CanResizeProperty, value);
        }

        /// <summary>
        /// Measure override.
        /// </summary>
        /// <param name="availableSize">The available size.</param>
        /// <returns>The measure size.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            return this.MeasureArrangeChildren(false, availableSize);
        }

        /// <summary>
        /// Arrange override.
        /// </summary>
        /// <param name="finalSize">The available size.</param>
        /// <returns>The arrange size.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            return this.MeasureArrangeChildren(true, finalSize);
        }

        /// <summary>
        /// The main layout algorithm for measure and arrange.
        /// </summary>
        /// <param name="arrange">Whether to arrange the children.</param>
        /// <param name="availableSize">The available size.</param>
        /// <returns>The final size.</returns>
        private Size MeasureArrangeChildren(bool arrange, Size availableSize)
        {
            double currentChildWidth = 0;
            double tallestChildHeight = 0;
            double currentX = 0;
            double currentY = 0;
            double width = 0;
            List<UIElement> linkedChildren = new List<UIElement>();
            UIElement previousChild = null;
            foreach (UIElement child in this.Children)
            {
                if (previousChild != null && !LinkingWrapPanel.GetIsLinkedToNextChild(previousChild))
                {
                    foreach (UIElement linkedChild in linkedChildren)
                    {
                        double childWidth = linkedChild.DesiredSize.Width;
                        if (LinkingWrapPanel.GetCanResize(linkedChild) && childWidth > availableSize.Width && currentX == 0)
                        {
                            childWidth = availableSize.Width;
                        }

                        if (arrange)
                        {
                            linkedChild.Arrange(new Rect(currentX, currentY, childWidth, linkedChild.DesiredSize.Height));
                        }

                        currentX += childWidth;
                        if (currentX > width)
                        {
                            width = currentX;
                        }

                        if (tallestChildHeight < linkedChild.DesiredSize.Height)
                        {
                            tallestChildHeight = linkedChild.DesiredSize.Height;
                        }
                    }

                    currentChildWidth = 0;
                    linkedChildren.Clear();
                }

                if (!arrange)
                {
                    child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                }

                double childwidth = child.DesiredSize.Width;
                currentChildWidth += childwidth;
                if (currentX != 0.0 && currentX + currentChildWidth > availableSize.Width)
                {
                    currentX = 0;
                    currentY += tallestChildHeight;
                    tallestChildHeight = 0;
                }

                if (LinkingWrapPanel.GetCanResize(child) && childwidth > availableSize.Width && currentX == 0)
                {
                    currentChildWidth -= childwidth;
                    childwidth = availableSize.Width;
                    if (!arrange)
                    {
                        child.Measure(new Size(childwidth, double.PositiveInfinity));
                    }

                    currentChildWidth += childwidth;
                }

                if (previousChild != null && !LinkingWrapPanel.GetIsLinkedToNextChild(previousChild) && !LinkingWrapPanel.GetIsLinkedToNextChild(child))
                {
                    if (arrange)
                    {
                        child.Arrange(new Rect(currentX, currentY, childwidth, child.DesiredSize.Height));
                    }

                    currentX += childwidth;

                    if (currentX > width)
                    {
                        width = currentX;
                    }

                    currentChildWidth = 0;

                    if (tallestChildHeight < child.DesiredSize.Height)
                    {
                        tallestChildHeight = child.DesiredSize.Height;
                    }
                }
                else
                {
                    linkedChildren.Add(child);
                }

                previousChild = child;
            }

            foreach (UIElement linkedChild in linkedChildren)
            {
                double childWidth = linkedChild.DesiredSize.Width;
                if (LinkingWrapPanel.GetCanResize(linkedChild) && childWidth > availableSize.Width && currentX == 0)
                {
                    childWidth = availableSize.Width;
                }

                if (arrange)
                {
                    linkedChild.Arrange(new Rect(currentX, currentY, childWidth, linkedChild.DesiredSize.Height));
                }

                currentX += childWidth;
                if (currentX > width)
                {
                    width = currentX;
                }

                if (tallestChildHeight < linkedChild.DesiredSize.Height)
                {
                    tallestChildHeight = linkedChild.DesiredSize.Height;
                }
            }

            return new Size(
                this.HorizontalAlignment == HorizontalAlignment.Stretch ? (!double.IsPositiveInfinity(availableSize.Width) ? availableSize.Width : width) : width,
                this.VerticalAlignment == VerticalAlignment.Stretch ? (!double.IsPositiveInfinity(availableSize.Height) ? availableSize.Height : currentY + tallestChildHeight) : currentY + tallestChildHeight);
        }
    }
}
