//-----------------------------------------------------------------------
// <copyright file="WrapPanel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>06-Mar-2008</date>
// <summary>The wrap panel control class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region "Using"
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    #endregion

    /// <summary>
    /// Implements a wrap panel.
    /// </summary>
    /// <remarks>
    /// Wraps the children control based upon width. 
    /// </remarks>
    public class WrapPanel : Panel
    {
        /// <summary>
        /// Provides the behavior for the "measure" pass of Silverlight layout.
        /// </summary>
        /// <param name="availableSize">The available size that this object can give to child objects.</param>
        /// <returns>The size that this object determines it needs during layout, based on its calculations of child object allotted sizes.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            double curX = 0, curLineHeight = 0;
            Size totalSize = new Size();
            foreach (UIElement child in Children)
            {
                child.Measure(availableSize);

                if (curX + child.DesiredSize.Width > availableSize.Width)
                {
                    // Wrap to next line
                    totalSize.Height += curLineHeight;                    
                    curX = 0;
                    curLineHeight = 0;
                }

                curX += child.DesiredSize.Width;

                totalSize.Width = Math.Max(totalSize.Width, curX);
                curLineHeight = Math.Max(child.DesiredSize.Height, curLineHeight);                
            }

            totalSize.Height += curLineHeight;            

            return totalSize;
        }

        /// <summary>
        /// Provides the behavior for the "Arrange" pass of Silverlight layout. 
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this object should use to arrange itself and its children. </param>
        /// <returns>The actual size used.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            double xoffset = 0;
            double yoffset = 0;

            double? largestHeight = null;

            int lineStart = 0;
            for (int lineEnd = 0; lineEnd < this.Children.Count; lineEnd++)
            {
                UIElement element = this.Children[lineEnd];
                if (xoffset + element.DesiredSize.Width > finalSize.Width)
                {
                    this.ArrangeLine(lineStart, lineEnd, yoffset);
                    yoffset += largestHeight == null ? element.DesiredSize.Height : largestHeight.Value;

                    xoffset = element.DesiredSize.Width;
                    largestHeight = element.DesiredSize.Height;

                    if (element.DesiredSize.Width > finalSize.Width)
                    {
                        this.ArrangeLine(lineEnd, ++lineEnd, yoffset);
                        yoffset += element.DesiredSize.Height;
                        xoffset = 0;
                    }

                    lineStart = lineEnd;
                }
                else
                {
                    xoffset += element.DesiredSize.Width;
                    largestHeight = Math.Max(element.DesiredSize.Height, largestHeight.GetValueOrDefault());
                }
            }

            // Arrange any elements on the last line
            if (lineStart < this.Children.Count)
            {
                this.ArrangeLine(lineStart, this.Children.Count, yoffset);
            }            

            return finalSize;
        }

        /// <summary>
        /// Arranges the elements in a line.
        /// </summary>
        /// <param name="lineStart">Starting index in the children collection.</param>
        /// <param name="lineEnd">End index in the children collection.</param>
        /// <param name="yoffset">Y offset for the controls to be placed in the line.</param>
        private void ArrangeLine(int lineStart, int lineEnd, double yoffset)
        {
            double xoffset = 0.0;
            for (int index = lineStart; index < lineEnd; index++)
            {
                UIElement element = this.Children[index];
                Rect bounds = new Rect(xoffset, yoffset, element.DesiredSize.Width, element.DesiredSize.Height);
                element.Arrange(bounds);
                xoffset += element.DesiredSize.Width;
            }
        }
    }
}
