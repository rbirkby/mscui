//-----------------------------------------------------------------------
// <copyright file="MatchingTermItemsControl.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>20-Jan-2009</date>
// <summary>The items control for displaying matching terms from the DecoratorTextBox.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// The items control for displaying matching terms from the DecoratorTextBox.
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(MatchingTermItemContainer))]
    [TemplatePart(Name = MatchingTermItemsControl.ElementScrollViewer, Type = typeof(ContentPresenter))]
    public class MatchingTermItemsControl : ItemsControl
    {
        /// <summary>
        /// The LengthMemberPath dependency property.
        /// </summary>
        public static readonly DependencyProperty LengthMemberPathProperty =
            DependencyProperty.Register("LengthMemberPath", typeof(string), typeof(MatchingTermItemsControl), new PropertyMetadata(string.Empty, new PropertyChangedCallback(MemberPath_Changed)));

        /// <summary>
        /// The IsSelectedMemberPath dependency property.
        /// </summary>
        public static readonly DependencyProperty IsSelectedMemberPathProperty =
            DependencyProperty.Register("IsSelectedMemberPath", typeof(string), typeof(MatchingTermItemsControl), new PropertyMetadata(string.Empty, new PropertyChangedCallback(MemberPath_Changed)));

        /// <summary>
        /// The SelectedItemMemberPath dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemMemberPathProperty =
            DependencyProperty.Register("SelectedItemMemberPath", typeof(string), typeof(MatchingTermItemsControl), new PropertyMetadata(string.Empty, new PropertyChangedCallback(MemberPath_Changed)));

        /// <summary>
        /// The SelectedItemTextMemberPath dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemTextMemberPathProperty =
            DependencyProperty.Register("SelectedItemTextMemberPath", typeof(string), typeof(MatchingTermItemsControl), new PropertyMetadata(string.Empty, new PropertyChangedCallback(MemberPath_Changed)));

        /// <summary>
        /// The AlternateItemsMemberPath dependency property.
        /// </summary>
        public static readonly DependencyProperty AlternateItemsMemberPathProperty =
            DependencyProperty.Register("AlternateItemsMemberPath", typeof(string), typeof(MatchingTermItemsControl), new PropertyMetadata(string.Empty, new PropertyChangedCallback(MemberPath_Changed)));

        /// <summary>
        /// The FlyOutContentTemplate dependency property.
        /// </summary>
        public static readonly DependencyProperty FlyOutContentTemplateProperty =
            DependencyProperty.Register("FlyOutContentTemplate", typeof(DataTemplate), typeof(MatchingTermItemsControl), new PropertyMetadata(new PropertyChangedCallback(FlyOutContentTemplate_Changed)));

        /// <summary>
        /// The EncodedItemTemplate dependency property.
        /// </summary>
        public static readonly DependencyProperty EncodedItemTemplateProperty =
            DependencyProperty.Register("EncodedItemTemplate", typeof(DataTemplate), typeof(MatchingTermItemsControl), new PropertyMetadata(new PropertyChangedCallback(EncodedItemTemplate_Changed)));

#if SILVERLIGHT
        /// <summary>
        /// The ItemContainerStyle dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemContainerStyleProperty =
            DependencyProperty.Register("ItemContainerStyle", typeof(Style), typeof(MatchingTermItemsControl), new PropertyMetadata(new PropertyChangedCallback(ItemContainerStyle_Changed)));
#else
        /// <summary>
        /// The ItemContainerStyle dependency property.
        /// </summary>
        public static new readonly DependencyProperty ItemContainerStyleProperty =
            DependencyProperty.Register("ItemContainerStyle", typeof(Style), typeof(MatchingTermItemsControl), new PropertyMetadata(new PropertyChangedCallback(ItemContainerStyle_Changed)));
#endif

        /// <summary>
        /// The template part name for the ScrollViewer.
        /// </summary>
        private const string ElementScrollViewer = "ScrollViewer";

        /// <summary>
        /// Stores a collection of containers.
        /// </summary>
        private List<MatchingTermItemContainer> containers = new List<MatchingTermItemContainer>(); 

        /// <summary>
        /// Stores the containers by item.
        /// </summary>
        private Dictionary<object, MatchingTermItemContainer> containersByItem = new Dictionary<object, MatchingTermItemContainer>();

        /// <summary>
        /// Stores the scroll viewer.
        /// </summary>
        private ScrollViewer scrollViewer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchingTermItemsControl"/> class.
        /// </summary>
        public MatchingTermItemsControl()
        {
            this.DefaultStyleKey = typeof(MatchingTermItemsControl);
        }

        /// <summary>
        /// Item got focus event.
        /// </summary>
        public event RoutedEventHandler ItemGotFocus;

        /// <summary>
        /// Item lost focus event.
        /// </summary>
        public event RoutedEventHandler ItemLostFocus;

        /// <summary>
        /// Item mouse entered event.
        /// </summary>
        public event MouseEventHandler ItemMouseEnter;

        /// <summary>
        /// Item mouse left event.
        /// </summary>
        public event MouseEventHandler ItemMouseLeave;

        /// <summary>
        /// Item selection changed event.
        /// </summary>
        public event EventHandler ItemSelectionChanged;

        /// <summary>
        /// Gets or sets the Length member path.
        /// </summary>
        /// <value>The length member path.</value>
        public string LengthMemberPath
        {
            get { return (string)GetValue(LengthMemberPathProperty); }
            set { SetValue(LengthMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the IsSelected member path.
        /// </summary>
        /// <value>The is selected member path.</value>
        public string IsSelectedMemberPath
        {
            get { return (string)GetValue(IsSelectedMemberPathProperty); }
            set { SetValue(IsSelectedMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the SelectedItem member path.
        /// </summary>
        /// <value>The selected item member path.</value>
        public string SelectedItemMemberPath
        {
            get { return (string)GetValue(SelectedItemMemberPathProperty); }
            set { SetValue(SelectedItemMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the SelectedItemText member path.
        /// </summary>
        /// <value>The selected item text member path.</value>
        public string SelectedItemTextMemberPath
        {
            get { return (string)GetValue(SelectedItemTextMemberPathProperty); }
            set { SetValue(SelectedItemTextMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the alternate items member path.
        /// </summary>
        /// <value>The alternate items member path.</value>
        public string AlternateItemsMemberPath
        {
            get { return (string)GetValue(AlternateItemsMemberPathProperty); }
            set { SetValue(AlternateItemsMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the flyout content template.
        /// </summary>
        /// <value>The flyout content template.</value>
        public DataTemplate FlyOutContentTemplate
        {
            get { return (DataTemplate)GetValue(FlyOutContentTemplateProperty); }
            set { SetValue(FlyOutContentTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the encoded item template.
        /// </summary>
        /// <value>The encoded item template.</value>
        public DataTemplate EncodedItemTemplate
        {
            get { return (DataTemplate)GetValue(EncodedItemTemplateProperty); }
            set { SetValue(EncodedItemTemplateProperty, value); }
        }

#if SILVERLIGHT
        /// <summary>
        /// Gets or sets the item container style.
        /// </summary>
        /// <value>The item container style.</value>
        public Style ItemContainerStyle
        {
            get { return (Style)GetValue(ItemContainerStyleProperty); }
            set { SetValue(ItemContainerStyleProperty, value); }
        }
#else
        /// <summary>
        /// Gets or sets the item container style.
        /// </summary>
        /// <value>The item container style.</value>
        public new Style ItemContainerStyle
        {
            get { return (Style)GetValue(ItemContainerStyleProperty); }
            set { SetValue(ItemContainerStyleProperty, value); }
        }
#endif

        /// <summary>
        /// Gets the template parts from the template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.scrollViewer = (ScrollViewer)this.GetTemplateChild(MatchingTermItemsControl.ElementScrollViewer);
        }

        /// <summary>
        /// Gets a container from a data item.
        /// </summary>
        /// <param name="item">The data item.</param>
        /// <returns>The matching term item container.</returns>
        public MatchingTermItemContainer GetContainerFromItem(object item)
        {
            if (this.containersByItem.ContainsKey(item))
            {
                return this.containersByItem[item];
            }

            return null;
        }

        /// <summary>
        /// Highlights a specific term.
        /// </summary>
        /// <param name="term">The term data object.</param>
        /// <param name="highlighted">Whether the term should appear highlighted.</param>
        public void SetTermHighlight(object term, bool highlighted)
        {
            if (this.containersByItem.ContainsKey(term))
            {
                this.containersByItem[term].IsHighlighted = highlighted;
            }

            this.ScrollIntoView(term);
        }

        /// <summary>
        /// Scrolls and item into view.
        /// </summary>
        /// <param name="item">The item to scroll into view.</param>
        public void ScrollIntoView(object item)
        {
            if (this.scrollViewer != null && this.containersByItem.ContainsKey(item))
            {
                MatchingTermItemContainer container = this.containersByItem[item];
                if (System.Windows.Media.VisualTreeHelper.GetParent(container) != null)
                {
                    System.Windows.Media.GeneralTransform transform = container.TransformToVisual(this.scrollViewer);
                    double verticalOffset = transform.Transform(new Point(0, 0)).Y;

                    if (verticalOffset + container.ActualHeight > this.scrollViewer.ViewportHeight)
                    {
                        this.scrollViewer.ScrollToVerticalOffset(this.scrollViewer.VerticalOffset + (verticalOffset - this.scrollViewer.ViewportHeight) + container.ActualHeight);
                    }
                    else if (verticalOffset < 0)
                    {
                        this.scrollViewer.ScrollToVerticalOffset(this.scrollViewer.VerticalOffset + verticalOffset);
                    }
                }
            }
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
        }

        /// <summary>
        /// Generates a check box as the item container.
        /// </summary>
        /// <returns>A checkbox item container.</returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            MatchingTermItemContainer matchingTermItemContainer = new MatchingTermItemContainer();
            return matchingTermItemContainer;
        }

        /// <summary>
        /// Prepares the checkbox by creating bindings and hooking events.
        /// </summary>
        /// <param name="element">The container element.</param>
        /// <param name="item">The data item.</param>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            MatchingTermItemContainer container = (MatchingTermItemContainer)element;
            if (this.ItemContainerStyle != null)
            {
                container.Style = this.ItemContainerStyle;
            }            

            container.GotFocus += new RoutedEventHandler(this.Container_GotFocus);
            container.LostFocus += new RoutedEventHandler(this.Container_LostFocus);
            container.MouseEnter += new MouseEventHandler(this.Container_MouseEnter);
            container.MouseLeave += new MouseEventHandler(this.Container_MouseLeave);
            container.MouseEnterPopup += new MouseEventHandler(this.Container_MouseEnter);
            container.MouseLeavePopup += new MouseEventHandler(this.Container_MouseLeave);
            container.SelectionChanged += new EventHandler(this.Container_SelectionChanged);
            container.PopupOpening += new EventHandler(this.Container_PopupOpening);
            container.DataContext = item;
            container.SelectedItemMemberPath = this.SelectedItemMemberPath;
            container.AlternateItemsMemberPath = this.AlternateItemsMemberPath;
            container.SelectedItemTextMemberPath = this.SelectedItemTextMemberPath;
            container.FlyOutContentTemplate = this.FlyOutContentTemplate;
            container.EncodedItemTemplate = this.EncodedItemTemplate;
            container.IsSelectedMemberPath = this.IsSelectedMemberPath;

            System.Windows.Data.Binding lengthBinding = new System.Windows.Data.Binding(this.LengthMemberPath);
            lengthBinding.Mode = System.Windows.Data.BindingMode.TwoWay;
            container.SetBinding(MatchingTermItemContainer.SelectedItemTextLengthProperty, lengthBinding);

            this.containers.Add(container);
            this.containersByItem.Add(item, container);
        }

        /// <summary>
        /// Unhooks the checkbox events before clearing the container.
        /// </summary>
        /// <param name="element">The container element.</param>
        /// <param name="item">The data item.</param>
        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);

            MatchingTermItemContainer container = (MatchingTermItemContainer)element;
            container.GotFocus -= new RoutedEventHandler(this.Container_GotFocus);
            container.LostFocus -= new RoutedEventHandler(this.Container_LostFocus);
            container.MouseEnter -= new MouseEventHandler(this.Container_MouseEnter);
            container.MouseLeave -= new MouseEventHandler(this.Container_MouseLeave);
            container.MouseEnterPopup -= new MouseEventHandler(this.Container_MouseEnter);
            container.MouseLeavePopup -= new MouseEventHandler(this.Container_MouseLeave);
            container.SelectionChanged -= new EventHandler(this.Container_SelectionChanged);
            container.PopupOpening -= new EventHandler(this.Container_PopupOpening);

            this.containers.Remove(container);
            this.containersByItem.Remove(container.DataContext);
        }

        /// <summary>
        /// Updates the property bindings for the container.
        /// </summary>
        /// <param name="dependencyObject">The matching term items control.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void MemberPath_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            MatchingTermItemsControl matchingTermItemsControl = (MatchingTermItemsControl)dependencyObject;
            matchingTermItemsControl.UpdateBindings();
        }

        /// <summary>
        /// Updates the item container style.
        /// </summary>
        /// <param name="dependencyObject">The matching term items control.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void ItemContainerStyle_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            MatchingTermItemsControl matchingTermItemsControl = (MatchingTermItemsControl)dependencyObject;
            matchingTermItemsControl.UpdateItemContainerStyle();
        }

        /// <summary>
        /// Updates the flyout content template style.
        /// </summary>
        /// <param name="dependencyObject">The matching term items control.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void FlyOutContentTemplate_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            MatchingTermItemsControl matchingTermItemsControl = (MatchingTermItemsControl)dependencyObject;
            matchingTermItemsControl.UpdatedFlyOutContentTemplate();
        }

        /// <summary>
        /// Updates the encoded item templates.
        /// </summary>
        /// <param name="dependencyObject">The matching term items control.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void EncodedItemTemplate_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            MatchingTermItemsControl matchingTermItemsControl = (MatchingTermItemsControl)dependencyObject;
            matchingTermItemsControl.UpdateEncodedItemTemplate();
        }

        /// <summary>
        /// Updates the bindings.
        /// </summary>
        private void UpdateBindings()
        {
            if (this.IsSelectedMemberPath != null)
            {
                foreach (MatchingTermItemContainer container in this.containers)
                {
                    container.SelectedItemMemberPath = this.SelectedItemMemberPath;
                    container.SelectedItemTextMemberPath = this.SelectedItemTextMemberPath;
                    container.AlternateItemsMemberPath = this.AlternateItemsMemberPath;
                    container.IsSelectedMemberPath = this.IsSelectedMemberPath;

                    System.Windows.Data.Binding lengthBinding = new System.Windows.Data.Binding();
                    lengthBinding.Mode = System.Windows.Data.BindingMode.TwoWay;
                    container.SetBinding(MatchingTermItemContainer.SelectedItemTextLengthProperty, lengthBinding);
                }
            }
        }

        /// <summary>
        /// Updates the item container styles.
        /// </summary>
        private void UpdateItemContainerStyle()
        {
            if (this.ItemContainerStyle != null)
            {
                foreach (MatchingTermItemContainer container in this.containers)
                {
                    container.Style = this.ItemContainerStyle;
                }
            }
        }

        /// <summary>
        /// Updates the flyout content template in each container.
        /// </summary>
        private void UpdatedFlyOutContentTemplate()
        {
            if (this.FlyOutContentTemplate != null)
            {
                foreach (MatchingTermItemContainer container in this.containers)
                {
                    container.FlyOutContentTemplate = this.FlyOutContentTemplate;
                }
            }
        }

        /// <summary>
        /// Updates the encoded item template in each container.
        /// </summary>
        private void UpdateEncodedItemTemplate()
        {
            if (this.EncodedItemTemplate != null)
            {
                foreach (MatchingTermItemContainer container in this.containers)
                {
                    container.EncodedItemTemplate = this.EncodedItemTemplate;
                }
            }
        }

        /// <summary>
        /// Raises the ItemSelectionChanged event.
        /// </summary>
        /// <param name="sender">The container.</param>
        /// <param name="e">Event Args.</param>
        private void Container_SelectionChanged(object sender, EventArgs e)
        {
            if (this.ItemSelectionChanged != null)
            {
                this.ItemSelectionChanged(sender, e);
            }
        }

        /// <summary>
        /// Raises the ItemMouseLeave event.
        /// </summary>
        /// <param name="sender">The container.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void Container_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.ItemMouseLeave != null)
            {
                this.ItemMouseLeave(sender, e);
            }
        }

        /// <summary>
        /// Raises the ItemMouseEnter event.
        /// </summary>
        /// <param name="sender">The container.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void Container_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.ItemMouseEnter != null)
            {
                this.ItemMouseEnter(sender, e);
            }
        }

        /// <summary>
        /// Raises the ItemLostFocus event.
        /// </summary>
        /// <param name="sender">The container.</param>
        /// <param name="e">Routed Event Args.</param>
        private void Container_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.ItemLostFocus != null)
            {
                this.ItemLostFocus(sender, e);
            }
        }

        /// <summary>
        /// Raises the ItemGotFocus event.
        /// </summary>
        /// <param name="sender">The container.</param>
        /// <param name="e">Routed Event Args.</param>
        private void Container_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ScrollIntoView(((MatchingTermItemContainer)sender).Content);
            if (this.ItemGotFocus != null)
            {
                this.ItemGotFocus(sender, e);
            }
        }

        /// <summary>
        /// Scrolls to bring the item into view.
        /// </summary>
        /// <param name="sender">The container item.</param>
        /// <param name="e">Event Args.</param>
        private void Container_PopupOpening(object sender, EventArgs e)
        {
            this.ScrollIntoView(((MatchingTermItemContainer)sender).Content);
        }
    }
}
