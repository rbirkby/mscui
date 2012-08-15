//-----------------------------------------------------------------------
// <copyright file="DataSelector.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>30-Oct-2008</date>
// <summary>Class used to represent data selector control.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    
    /// <summary>
    /// Class used to represent data selector control.
    /// </summary>
    public class DataSelector : ItemsControl
    {
        /// <summary>
        /// Member variable to indicate whether the mouse movements are currently being captured.
        /// </summary>
        private bool mouseCaptured;

        /// <summary>
        /// Member variable to hold the current index of the item being dragged.
        /// </summary>
        private int oldIndex;

        /// <summary>
        /// Member variable to hold popup data template.
        /// </summary>
        private DataTemplate popupTemplate;

        /// <summary>
        /// Member variable to hold popup.
        /// </summary>
        private Popup popUp;

        /// <summary>
        /// Member variable to hold data selector item being dragged.
        /// </summary>
        private DataSelectorItem dataSelectorItem;
                
        /// <summary>
        /// Initializes a new instance of data selector control.
        /// </summary>
        public DataSelector()
        {            
            this.MouseLeftButtonDown += new MouseButtonEventHandler(this.DataSelector_MouseLeftButtonDown);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(this.DataSelector_MouseLeftButtonUp);
            this.MouseMove += new MouseEventHandler(this.DataSelector_MouseMove);
            this.KeyDown += new KeyEventHandler(this.DataSelector_KeyDown);
        }        
        
        /// <summary>
        /// Occurs when an data selector item is dragged on to a new location.
        /// </summary>
        public event EventHandler<ReorderedEventArgs> Swap;

        /// <summary>
        /// Gets or sets the popup that will be shown while dragging a data selector item.
        /// </summary>
        /// <value>Popup that will be shown while dragging a data selector item.</value>
        public DataTemplate PopupTemplate
        {
            get 
            {
                return this.popupTemplate; 
            }

            set
            {
                this.popupTemplate = value;
                if (this.popupTemplate != null)
                {
                    this.popUp = this.popupTemplate.LoadContent() as Popup;
                }
            }
        }

#if SILVERLIGHT
        /// <summary>
        /// Gets a value indicating whether the mouse is currently being captured.
        /// </summary>
        /// <value>Value indicating whether the mouse is currently being captured.</value>
        public bool IsMouseCaptured
        {
            get { return this.mouseCaptured; }
        }
#endif

        /// <summary>
        /// Handles KeyPressed event on the data selector control.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void DataSelector_KeyDown(object sender, KeyEventArgs e)
        {
            int oldIndex = this.GetIndex(e.OriginalSource);            
            if (e.Handled == false && oldIndex > -1)
            {
                int newIndex = -1;
                switch (e.Key)
                {
                    case Key.PageUp:
                        newIndex = oldIndex - 1;

                        if (newIndex >= 0)
                        {
                            if (oldIndex > -1 && newIndex > -1 && oldIndex != newIndex && this.Swap != null)
                            {
                                this.Swap(this, new ReorderedEventArgs(oldIndex, newIndex));
                            }
                                                        
                            e.Handled = true;
                        }

                        break;
                    case Key.PageDown:
                        newIndex = oldIndex + 1;

                        if (newIndex < this.Items.Count)
                        {
                            if (oldIndex > -1 && newIndex > -1 && oldIndex != newIndex && this.Swap != null)
                            {
                                this.Swap(this, new ReorderedEventArgs(oldIndex, newIndex));
                            }

                            e.Handled = true;
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Handles the mouse mouse event on data selector.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void DataSelector_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                this.popUp.IsOpen = true;
                Point p = new Point();
#if SILVERLIGHT
                p = e.GetPosition(Application.Current.RootVisual);
#else
                p = this.PointToScreen(e.GetPosition(sender as IInputElement));
#endif

                this.popUp.VerticalOffset = p.Y;
                this.popUp.HorizontalOffset = p.X + 10;
            }
        }

        /// <summary>
        /// Handles the mouse left button up event on data selector.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void DataSelector_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.IsMouseCaptured && this.dataSelectorItem != null)
            {
                DataSelectorItem newDataSelectorItem = null;
                this.popUp.IsOpen = false;
                this.ReleaseMouseCapture();
                this.mouseCaptured = false;
                
                int newIndex = -1;

#if SILVERLIGHT
                if (e.OriginalSource != null)
                {
                    newIndex = this.GetIndex(e.OriginalSource);
                    newDataSelectorItem = this.GetDataSelectorItem(e.OriginalSource as FrameworkElement);
                }
#else
                HitTestResult result = VisualTreeHelper.HitTest(this, e.GetPosition(this));
                if (result != null)
                {
                    newIndex = this.GetIndex(result.VisualHit);
                    newDataSelectorItem = this.GetDataSelectorItem(result.VisualHit as FrameworkElement);
                }
#endif

                if (this.oldIndex > -1 && newIndex > -1 && this.oldIndex != newIndex && newDataSelectorItem != null)
                {
                    this.Swap(this, new ReorderedEventArgs(this.oldIndex, newIndex));                    
                }

                if (this.dataSelectorItem != newDataSelectorItem)
                {
                    this.dataSelectorItem.ClearHoverStyle();
                }

                this.dataSelectorItem = null;
                this.oldIndex = -1;
            }
        }

        /// <summary>
        /// Handles the mouse left button down event on data selector.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void DataSelector_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!this.IsMouseCaptured)
            {
                FrameworkElement element = e.OriginalSource as FrameworkElement;
                if (element.DataContext != null)
                {
                    this.mouseCaptured = this.CaptureMouse();
                    this.oldIndex = this.GetIndex(e.OriginalSource);
                    this.popUp.DataContext = element.DataContext;
                    this.dataSelectorItem = this.GetDataSelectorItem(element);
                }
            }
        }

        /// <summary>
        /// Returns the index of the element in the items control.
        /// </summary>
        /// <param name="element">Element whose index needs to be determined.</param>
        /// <returns>Index of the element in items control.</returns>
        private int GetIndex(object element)
        {
            int newIndex = -1;
            FrameworkElement frameworkElement = element as FrameworkElement;
            if (frameworkElement != null)
            {
                if (this.Items.Contains(frameworkElement.DataContext))
                {
                    newIndex = this.Items.IndexOf(frameworkElement.DataContext);
                }
            }

            return newIndex;
        }

        /// <summary>
        /// Gets the data selector item corresponding to the element.
        /// </summary>
        /// <param name="element">Element to which data selector item needs to be determined.</param>
        /// <returns>Data selector item corresponding to the element.</returns>
        private DataSelectorItem GetDataSelectorItem(FrameworkElement element)
        {
            DataSelectorItem dataSelectorItem = element as DataSelectorItem;
            if (dataSelectorItem != null)
            {
                return dataSelectorItem;
            }
            else
            {
                FrameworkElement parent = element.Parent as FrameworkElement;
                if (parent != null)
                {
                    return this.GetDataSelectorItem(parent);
                }
            }

            return null;
        }
    }
}
