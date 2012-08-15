//-----------------------------------------------------------------------
// <copyright file="ConceptListBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>
//     A list box for displaying a list of concepts. Additional methods have been added
//     extending the normal list box.
// </summary>
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
    using System.Windows.Threading;

    /// <summary>
    /// A list box for displaying a list of concepts. Additional methods have been added
    /// extending the normal list box.
    /// </summary>
    [TemplatePart(Name = ConceptListBox.ElementItemsPresenter, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = ConceptListBox.ElementScrollViewer, Type = typeof(ScrollViewer))]
    public class ConceptListBox : ListBox
    {
        /// <summary>
        /// The horizontal scrollbar visibility Dependency Property.
        /// </summary>
        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty =
            DependencyProperty.Register("HorizontalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(ConceptListBox), new PropertyMetadata(ScrollBarVisibility.Disabled));

        /// <summary>
        /// The VerticalScrollBarVisibility Dependency Property.
        /// </summary>
        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty =
            DependencyProperty.Register("VerticalScrollBarVisibility", typeof(ScrollBarVisibility), typeof(ConceptListBox), new PropertyMetadata(ScrollBarVisibility.Auto));

        /// <summary>
        /// The template part name for the items presenter.
        /// </summary>
        private const string ElementItemsPresenter = "ItemsPresenter";

        /// <summary>
        /// The template part name for the scroll viewer.
        /// </summary>
        private const string ElementScrollViewer = "ScrollViewer";

        /// <summary>
        /// Stores the list box items by data item.
        /// </summary>
        private Dictionary<object, ListBoxItem> containersByItem = new Dictionary<object, ListBoxItem>();

        /// <summary>
        /// Stores the list box item containers.
        /// </summary>
        private List<ListBoxItem> containers = new List<ListBoxItem>();

        /// <summary>
        /// The scroll timer.
        /// </summary>
        private DispatcherTimer scrollTimer;

        /// <summary>
        /// Stores the controls items presenter.
        /// </summary>
        private ItemsPresenter itemsPresenter;

        /// <summary>
        /// Stores the control's scroll viewer.
        /// </summary>
        private ScrollViewer scrollViewer;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ConceptListBox"/> class.
        /// </summary>
        public ConceptListBox()
        {
            this.DefaultStyleKey = typeof(ConceptListBox);

            this.scrollTimer = new DispatcherTimer();
            this.scrollTimer.Interval = TimeSpan.FromMilliseconds(0);
            this.scrollTimer.Tick += new EventHandler(this.ScrollTimer_Tick);
        }

        /// <summary>
        /// The items changed event.
        /// </summary>
        public event EventHandler ItemsChanged;

        /// <summary>
        /// Occurs when an item is clicked.
        /// </summary>
        public event MouseButtonEventHandler ItemClicked;

        /// <summary>
        /// Occurs when the list is scrolled.
        /// </summary>
        public event EventHandler Scroll;

        /// <summary>
        /// Gets or sets the horizontal scroll bar visibility.
        /// </summary>
        /// <value>A ScrollBarVisibility value.</value>
        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(HorizontalScrollBarVisibilityProperty); }
            set { SetValue(HorizontalScrollBarVisibilityProperty, value); }
        }

        /// <summary>
        /// Gets or sets the vertical scroll bar visibility.
        /// </summary>
        /// <value>The vertical scroll bar visibility.</value>
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(VerticalScrollBarVisibilityProperty); }
            set { SetValue(VerticalScrollBarVisibilityProperty, value); }
        }

        /// <summary>
        /// Gets a container from a data item.
        /// </summary>
        /// <param name="item">The item to retrieve the container for.</param>
        /// <returns>The container wrapper for the item.</returns>
        public ListBoxItem GetContainerFromItem(object item)
        {
            if (item == null)
            {
                return null;
            }

            if (this.containersByItem.ContainsKey(item))
            {
                return this.containersByItem[item];
            }

            return null;
        }

        /// <summary>
        /// Pages the scroll viewer Up.
        /// </summary>
        public void PageUp()
        {
            if (this.scrollViewer != null)
            {
                int selectedIndex = Math.Max(0, this.SelectedIndex);
                double itemsHeight = 0.0;
                for (int i = selectedIndex; i >= 0; i--)
                {
                    itemsHeight += this.containers[i].ActualHeight;
                    if (itemsHeight > this.scrollViewer.ViewportHeight)
                    {
                        this.SelectedIndex = i;
                        break;
                    }
                    else if (i == 0)
                    {
                        this.SelectedIndex = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Pages the scroll viewer Down.
        /// </summary>
        public void PageDown()
        {
            if (this.scrollViewer != null)
            {
                int selectedIndex = Math.Max(0, this.SelectedIndex);
                double itemsHeight = 0.0;
                for (int i = selectedIndex; i < this.containers.Count; i++)
                {
                    itemsHeight += this.containers[i].ActualHeight;
                    if (itemsHeight > this.scrollViewer.ViewportHeight)
                    {
                        this.SelectedIndex = i;
                        break;
                    }
                    else if (i == this.containers.Count - 1)
                    {
                        this.SelectedIndex = this.containers.Count - 1;
                    }
                }
            }
        }

        /// <summary>
        /// Pages to the beginning of the list.
        /// </summary>
        public void PageToBeginning()
        {
            if (this.containers.Count > 0)
            {
                this.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Pages to the end of the list.
        /// </summary>
        public void PageToEnd()
        {
            if (this.containers.Count > 0)
            {
                this.SelectedIndex = this.containers.Count - 1;
            }
        }

        /// <summary>
        /// Gets the template parts from the template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.itemsPresenter = (ItemsPresenter)this.GetTemplateChild(ConceptListBox.ElementItemsPresenter);
            if (this.itemsPresenter != null)
            {
                this.itemsPresenter.Loaded += new RoutedEventHandler(this.ItemsPresenter_Loaded);
            }

            this.scrollViewer = (ScrollViewer)this.GetTemplateChild(ConceptListBox.ElementScrollViewer);
        }

        /// <summary>
        /// Fires the items changed event.
        /// </summary>
        /// <param name="e">Notify Collection Changed Event Args.</param>
        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            if (this.scrollViewer != null)
            {
                this.scrollViewer.ScrollToVerticalOffset(0);
            }

            if (this.ItemsChanged != null)
            {
                this.ItemsChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Removes an item from the containers dictionary.
        /// </summary>
        /// <param name="element">The container element.</param>
        /// <param name="item">The data item.</param>
        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);
            ListBoxItem listBoxItem = (ListBoxItem)element;
            listBoxItem.MouseEnter -= new MouseEventHandler(this.ListBoxItem_MouseEnter);
            listBoxItem.MouseLeftButtonUp -= new MouseButtonEventHandler(this.ListBoxItem_MouseLeftButtonUp);
            this.containers.Remove(listBoxItem);
            this.containersByItem.Remove(item);
        }

        /// <summary>
        /// Adds an item to the containers dictionary.
        /// </summary>
        /// <param name="element">The container element.</param>
        /// <param name="item">The data item.</param>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            ListBoxItem listBoxItem = (ListBoxItem)element;
            listBoxItem.MouseEnter += new MouseEventHandler(this.ListBoxItem_MouseEnter);
            listBoxItem.MouseLeftButtonUp += new MouseButtonEventHandler(this.ListBoxItem_MouseLeftButtonUp);
            this.containers.Add(listBoxItem);
            this.containersByItem.Add(item, listBoxItem);
        }

        /// <summary>
        /// Raises the item clicked event.
        /// </summary>
        /// <param name="sender">The list box item.</param>
        /// <param name="e">Mouse Button Event Args.</param>
        private void ListBoxItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {            
            if (this.ItemClicked != null)
            {
                this.ItemClicked(sender, e);
            }
        }

        /// <summary>
        /// Raises the mouse entered item event.
        /// </summary>
        /// <param name="sender">A list box item.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void ListBoxItem_MouseEnter(object sender, MouseEventArgs e)
        {
            this.SelectedItem = ((ListBoxItem)sender).Content;
        }

        /// <summary>
        /// Finds the scrollbars, and hooks up the scroll events.
        /// </summary>
        /// <param name="sender">The items presenter.</param>
        /// <param name="e">Routed Event Args.</param>
        private void ItemsPresenter_Loaded(object sender, RoutedEventArgs e)
        {
            DependencyObject currentElement = this.itemsPresenter;
            while (currentElement != null)
            {
                if (VisualTreeHelper.GetParent(currentElement).GetType() == typeof(ScrollViewer))
                {
                    break;
                }
                else
                {
                    currentElement = VisualTreeHelper.GetParent(currentElement);
                }
            }

            ScrollBar verticalScrollBar = null;
            ScrollBar horizontalScrollBar = null;
            FrameworkElement rootElement = (FrameworkElement)currentElement;

#if SILVERLIGHT
            verticalScrollBar = (ScrollBar)rootElement.FindName("VerticalScrollBar");
            horizontalScrollBar = (ScrollBar)rootElement.FindName("HorizontalScrollBar");
#else
            verticalScrollBar = (ScrollBar)rootElement.FindName("PART_VerticalScrollBar");
            horizontalScrollBar = (ScrollBar)rootElement.FindName("PART_HorizontalScrollBar");
#endif

            if (verticalScrollBar != null)
            {
                verticalScrollBar.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.VerticalScrollBar_ValueChanged);                
            }

            if (horizontalScrollBar != null)
            {
                verticalScrollBar.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.VerticalScrollBar_ValueChanged);
            }            
        }

        /// <summary>
        /// Starts the scroll timer.
        /// </summary>
        /// <param name="sender">A scroll bar.</param>
        /// <param name="e">Routed Property Changed Event Args.</param>
        private void VerticalScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {            
            this.scrollTimer.Start();
        }

        /// <summary>
        /// Raises the scroll event.
        /// </summary>
        /// <param name="sender">The scroll timer.</param>
        /// <param name="e">Event Args.</param>
        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            this.scrollTimer.Stop();            
            if (this.Scroll != null)
            {
                this.Scroll(this, EventArgs.Empty);
            }
        }
    }
}
