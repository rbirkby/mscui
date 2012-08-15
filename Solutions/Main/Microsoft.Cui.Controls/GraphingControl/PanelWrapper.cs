//-----------------------------------------------------------------------
// <copyright file="PanelWrapper.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Panel wrapper class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    #endregion

    /// <summary>
    /// Base of our panels.
    /// </summary>
    public class PanelWrapper : Grid
    {
        /// <summary>
        /// Private collisionCollection member.
        /// </summary>
        private CollisionCollection collisionCollection;

        /// <summary>
        /// Private seamedElementCollection member.
        /// </summary>
        private SeamedElementCollection seamedElementCollection;

        /// <summary>
        /// Collection to hold all label elements in the panel.
        /// </summary>
        private Collection<UIElement> labelElements = new Collection<UIElement>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelWrapper"/> class.
        /// </summary>
        public PanelWrapper()
        {
            this.collisionCollection = new CollisionCollection();
            this.seamedElementCollection = new SeamedElementCollection();
        }

        /// <summary>
        /// Gets the collision collection.
        /// </summary>
        /// <value>The collision collection.</value>
        public CollisionCollection CollisionCollection
        {
            get
            {
                return this.collisionCollection;
            }          
        }

        /// <summary>
        /// Gets the label elements.
        /// </summary>
        /// <value>The label elements.</value>
        public Collection<UIElement> LabelElements
        {
            get
            {
                return this.labelElements;
            }
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        public DateTime StartDate 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime EndDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to [abort layout].
        /// </summary>
        /// <value>If [abort layout] <c>true</c>; otherwise, <c>false</c>.</value>
        public bool AbortLayout
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the seamed element collection.
        /// </summary>
        /// <value>The Seamed Element Collection collection.</value>
        public SeamedElementCollection SeamedElementCollection
        {
            get
            {
                return this.seamedElementCollection;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is in view.
        /// </summary>
        /// <value>
        /// Returns <c>true</c> if this instance is in view; otherwise, <c>false</c>.
        /// </value>
        public bool IsInView
        {
            get
            {
                bool viewable = false;
                double left = Canvas.GetLeft(this);

                if (left < 0 || left < this.ActualWidth)
                {
                    viewable = true;
                }

                return viewable;
            }
        }

        /// <summary>
        /// Adds the seam boundary.
        /// </summary>
        internal void AddSeamBoundary()
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Height = this.Height;
            rectangle.Width = 1;
            rectangle.Fill = new SolidColorBrush(Colors.Red);
            Canvas.SetZIndex(rectangle, 100000);
            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            this.Children.Add(rectangle);
        }          
    }
}
