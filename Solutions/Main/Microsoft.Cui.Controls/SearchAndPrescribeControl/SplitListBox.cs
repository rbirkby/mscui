//-----------------------------------------------------------------------
// <copyright file="SplitListBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>19-Aug-2009</date>
// <summary>
//      A list box that presents groups of options.
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows.Controls.Primitives;

    /// <summary>
    /// A list box that presents groups of options.
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(SplitListBoxItem))]
    [TemplatePart(Name = SplitListBox.ElementItemsPresenterName, Type = typeof(ItemsPresenter))]
    [TemplatePart(Name = SplitListBox.ElementScrollViewer, Type = typeof(ScrollViewer))]
    public class SplitListBox : ListBox, ISplitItemsControl
    {
        /// <summary>
        /// The PrimaryItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty PrimaryItemsSourceProperty =
            DependencyProperty.Register("PrimaryItemsSource", typeof(IEnumerable), typeof(SplitListBox), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The SecondaryItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SecondaryItemsSourceProperty =
            DependencyProperty.Register("SecondaryItemsSource", typeof(IEnumerable), typeof(SplitListBox), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The OtherItem Dependency Property.
        /// </summary>
        public static readonly DependencyProperty OtherItemProperty =
            DependencyProperty.Register("OtherItem", typeof(object), typeof(SplitListBox), new PropertyMetadata("other..."));

        /// <summary>
        /// The OtherItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty OtherItemTemplateProperty =
            DependencyProperty.Register("OtherItemTemplate", typeof(DataTemplate), typeof(SplitListBox), new PropertyMetadata(null));

        /// <summary>
        /// The CustomValueItem Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomValueItemProperty =
           DependencyProperty.Register("CustomValueItem", typeof(object), typeof(SplitListBox), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The ShowCustomValueOption Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShowCustomValueOptionProperty =
         DependencyProperty.Register("ShowCustomValueOption", typeof(bool), typeof(SplitListBox), new PropertyMetadata(false, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The CustomValueItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomValueItemTemplateProperty =
            DependencyProperty.Register("CustomValueItemTemplate", typeof(DataTemplate), typeof(SplitListBox), new PropertyMetadata(null));

        /// <summary>
        /// The OtherItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty OtherItemsSourceProperty =
            DependencyProperty.Register("OtherItemsSource", typeof(IEnumerable), typeof(SplitListBox), new PropertyMetadata(null, new PropertyChangedCallback(OtherItemsSource_Changed)));

        /// <summary>
        /// The ShowOtherOption Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShowOtherOptionProperty =
            DependencyProperty.Register("ShowOtherOption", typeof(bool), typeof(SplitListBox), new PropertyMetadata(false, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The ShowOtherItems Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShowOtherItemsProperty =
            DependencyProperty.Register("ShowOtherItems", typeof(bool), typeof(SplitListBox), new PropertyMetadata(false, new PropertyChangedCallback(ShowOtherItems_Changed)));

        /// <summary>
        /// The ItemHeaderTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ItemHeaderTemplateProperty =
            DependencyProperty.Register("ItemHeaderTemplate", typeof(DataTemplate), typeof(SplitListBox), null);

        /// <summary>
        /// The RowBackground Dependency Property.
        /// </summary>
        public static readonly DependencyProperty RowBackgroundProperty =
            DependencyProperty.Register("RowBackground", typeof(Brush), typeof(SplitListBox), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        /// <summary>
        /// The AlternateRowBackground Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AlternateRowBackgroundProperty =
            DependencyProperty.Register("AlternateRowBackground", typeof(Brush), typeof(SplitListBox), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        /// <summary>
        /// The ConfirmedSelectedItem Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ConfirmedSelectedItemProperty =
            DependencyProperty.Register("ConfirmedSelectedItem", typeof(object), typeof(SplitListBox), new PropertyMetadata(null, new PropertyChangedCallback(ConfirmedSelectedItem_Changed)));

        /// <summary>
        /// The PrimaryItemsSourceHeader Dependency Property.
        /// </summary>
        public static readonly DependencyProperty PrimaryItemsSourceHeaderProperty =
            DependencyProperty.Register("PrimaryItemsSourceHeader", typeof(object), typeof(SplitListBox), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The SecondaryItemsSourceHeader Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SecondaryItemsSourceHeaderProperty =
            DependencyProperty.Register("SecondaryItemsSourceHeader", typeof(object), typeof(SplitListBox), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The OtherItemsSourceHeader Dependency Property.
        /// </summary>
        public static readonly DependencyProperty OtherItemsSourceHeaderProperty =
            DependencyProperty.Register("OtherItemsSourceHeader", typeof(object), typeof(SplitListBox), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The AddShortcutKeysToPrimaryItems Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AddShortcutKeysToPrimaryItemsProperty =
            DependencyProperty.Register("AddShortcutKeysToPrimaryItems", typeof(bool), typeof(SplitListBox), new PropertyMetadata(false));

        /// <summary>
        /// The SplitBorderThickness Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SplitBorderThicknessProperty =
            DependencyProperty.Register("SplitBorderThickness", typeof(Thickness), typeof(SplitListBox), new PropertyMetadata(new Thickness(0, 0, 0, 1)));

        /// <summary>
        /// The SplitBorderBrush Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SplitBorderBrushProperty =
            DependencyProperty.Register("SplitBorderBrush", typeof(Brush), typeof(SplitListBox), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0x00))));

        /// <summary>
        /// The ItemsPresenter Element Name.
        /// </summary>
        private const string ElementItemsPresenterName = "ItemsPresenter";

        /// <summary>
        /// The ScrollViewer Element Name.
        /// </summary>
        private const string ElementScrollViewer = "ScrollViewer";

        /// <summary>
        /// Stores the item headers by item.
        /// </summary>
        private Dictionary<object, object> itemHeadersByItem = new Dictionary<object, object>();

        /// <summary>
        /// Stores the split items.
        /// </summary>
        private List<object> splitItems = new List<object>();

        /// <summary>
        /// Stores the cascading list box item containers by item.
        /// </summary>
        private Dictionary<object, SplitListBoxItem> containersByItem = new Dictionary<object, SplitListBoxItem>();

        /// <summary>
        /// Stores the items by shortcut key.
        /// </summary>
        private Dictionary<Key, object> itemsByShortcutKey = new Dictionary<Key, object>();

        /// <summary>
        /// Stores the shortcut keys by item.
        /// </summary>
        private Dictionary<object, Key> shortcutKeysByItem = new Dictionary<object, Key>();

        /// <summary>
        /// Stores the vertical scroll bar.
        /// </summary>
        private ScrollBar verticalScrollBar;

        /// <summary>
        /// Stores the scroll viewer.
        /// </summary>
        private ScrollViewer scrollViewer;

        /// <summary>
        /// Stores if the short cut keys are showing.
        /// </summary>
        private bool showingShortcutKeys;

        /// <summary>
        /// SplitListBox constructor.
        /// </summary>
        public SplitListBox()
        {
            this.DefaultStyleKey = typeof(SplitListBox);
            this.GotFocus += new RoutedEventHandler(this.SplitListBox_GotFocus);
            this.LostFocus += new RoutedEventHandler(this.SplitListBox_LostFocus);
        }

        /// <summary>
        /// The OtherSelected Event.
        /// </summary>
        public event EventHandler OtherSelected;

        /// <summary>
        /// The Scroll Event.
        /// </summary>
        public event EventHandler Scroll;

        /// <summary>
        /// The SelectedValueChanged Event.
        /// </summary>
        public event EventHandler ConfirmedSelectedItemChanged;

        /// <summary>
        /// The ItemSelected Event.
        /// </summary>
        public event EventHandler ItemSelected;

        /// <summary>
        /// The ItemsChanged event.
        /// </summary>
        public event System.Collections.Specialized.NotifyCollectionChangedEventHandler ItemsChanged;

        /// <summary>
        /// Gets or sets the primary items source.
        /// </summary>
        /// <value>The primary items source value.</value>
        public IEnumerable PrimaryItemsSource
        {
            get { return (IEnumerable)GetValue(PrimaryItemsSourceProperty); }
            set { SetValue(PrimaryItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the secondary items source.
        /// </summary>
        /// <value>The secondary items source value.</value>
        public IEnumerable SecondaryItemsSource
        {
            get { return (IEnumerable)GetValue(SecondaryItemsSourceProperty); }
            set { SetValue(SecondaryItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the other item.
        /// </summary>
        /// <value>The other item value.</value>
        public object OtherItem
        {
            get { return (object)GetValue(OtherItemProperty); }
            set { SetValue(OtherItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the other item template.
        /// </summary>
        /// <value>The other item template value.</value>
        public DataTemplate OtherItemTemplate
        {
            get { return (DataTemplate)GetValue(OtherItemTemplateProperty); }
            set { SetValue(OtherItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the other items source.
        /// </summary>
        /// <value>The other items source value.</value>
        public IEnumerable OtherItemsSource
        {
            get { return (IEnumerable)GetValue(OtherItemsSourceProperty); }
            set { SetValue(OtherItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the other option should be shown.
        /// </summary>
        /// <value>The show other option value.</value>
        public bool ShowOtherOption
        {
            get { return (bool)GetValue(ShowOtherOptionProperty); }
            set { SetValue(ShowOtherOptionProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the list box should show the other items.
        /// </summary>
        /// <value>The show other items value.</value>
        public bool ShowOtherItems
        {
            get { return (bool)GetValue(ShowOtherItemsProperty); }
            set { SetValue(ShowOtherItemsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the custom value item.
        /// </summary>
        /// <value>The custom value item.</value>
        public object CustomValueItem
        {
            get { return (object)GetValue(CustomValueItemProperty); }
            set { SetValue(CustomValueItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the custom value option.
        /// </summary>
        /// <value>The show custom value option value.</value>
        public bool ShowCustomValueOption
        {
            get { return (bool)GetValue(ShowCustomValueOptionProperty); }
            set { SetValue(ShowCustomValueOptionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the custom value data template.
        /// </summary>
        /// <value>The customer value item template value.</value>
        public DataTemplate CustomValueItemTemplate
        {
            get { return (DataTemplate)GetValue(CustomValueItemTemplateProperty); }
            set { SetValue(CustomValueItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the item header template.
        /// </summary>
        /// <value>The item header template.</value>
        public DataTemplate ItemHeaderTemplate
        {
            get { return (DataTemplate)GetValue(ItemHeaderTemplateProperty); }
            set { SetValue(ItemHeaderTemplateProperty, value); }
        }
     
        /// <summary>
        /// Gets or sets the row background.
        /// </summary>
        /// <value>The row background value.</value>
        public Brush RowBackground
        {
            get { return (Brush)GetValue(RowBackgroundProperty); }
            set { SetValue(RowBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the alternate row background.
        /// </summary>
        /// <value>The alternate row background value.</value>
        public Brush AlternateRowBackground
        {
            get { return (Brush)GetValue(AlternateRowBackgroundProperty); }
            set { SetValue(AlternateRowBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the confirmed selected value.
        /// </summary>
        /// <value>The confirmed selected value.</value>
        public object ConfirmedSelectedItem
        {
            get { return (object)GetValue(ConfirmedSelectedItemProperty); }
            set { SetValue(ConfirmedSelectedItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the primary items source header.
        /// </summary>
        /// <value>The primary items source header.</value>
        public object PrimaryItemsSourceHeader
        {
            get { return (object)GetValue(PrimaryItemsSourceHeaderProperty); }
            set { SetValue(PrimaryItemsSourceHeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the secondary items source header.
        /// </summary>
        /// <value>The secondary items source header.</value>
        public object SecondaryItemsSourceHeader
        {
            get { return (object)GetValue(SecondaryItemsSourceHeaderProperty); }
            set { SetValue(SecondaryItemsSourceHeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the other items source header.
        /// </summary>
        /// <value>The other items source header.</value>
        public object OtherItemsSourceHeader
        {
            get { return (object)GetValue(OtherItemsSourceHeaderProperty); }
            set { SetValue(OtherItemsSourceHeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether shortcut keys should be added to primary items.
        /// </summary>
        /// <value>Whether shortcut keys should be added to primary items.</value>
        public bool AddShortcutKeysToPrimaryItems
        {
            get { return (bool)GetValue(AddShortcutKeysToPrimaryItemsProperty); }
            set { SetValue(AddShortcutKeysToPrimaryItemsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the split border thickness.
        /// </summary>
        /// <value>The split border thickness.</value>
        public Thickness SplitBorderThickness
        {
            get { return (Thickness)GetValue(SplitBorderThicknessProperty); }
            set { SetValue(SplitBorderThicknessProperty, value); }
        }

        /// <summary>
        /// Gets or sets the split border brush.
        /// </summary>
        /// <value>The split border brush.</value>
        public Brush SplitBorderBrush
        {
            get { return (Brush)GetValue(SplitBorderBrushProperty); }
            set { SetValue(SplitBorderBrushProperty, value); }
        }

        /// <summary>
        /// Adds a header to an item.
        /// </summary>
        /// <param name="item">The item to add the header too.</param>
        /// <param name="header">The header to add.</param>
        public void AddItemHeader(object item, object header)
        {
            if (this.containersByItem.ContainsKey(item))
            {
                this.containersByItem[item].Header = header;
            }

            if (!this.itemHeadersByItem.ContainsKey(item))
            {
                this.itemHeadersByItem.Add(item, header);
            }
            else if (this.itemHeadersByItem.ContainsKey(item))
            {
                if (header != null)
                {
                    this.itemHeadersByItem[item] = header;
                }
                else
                {
                    this.itemHeadersByItem.Remove(item);
                }
            }

            this.UpdateItemBackgrounds();
        }

        /// <summary>
        /// Adds a shortcut key to an item.
        /// </summary>
        /// <param name="item">The item to add the key too.</param>
        /// <param name="key">The short cut key.</param>
        public void AddItemShortcutKey(object item, Key? key)
        {
            if (key.HasValue && item != null)
            {
                if (this.itemsByShortcutKey.ContainsKey(key.Value))
                {
                    this.itemsByShortcutKey[key.Value] = item;
                }
                else
                {
                    this.itemsByShortcutKey.Add(key.Value, item);
                }

                if (this.shortcutKeysByItem.ContainsKey(item))
                {
                    this.shortcutKeysByItem[item] = key.Value;
                }
                else
                {
                    this.shortcutKeysByItem.Add(item, key.Value);
                }

                if (this.containersByItem.ContainsKey(item))
                {
                    this.containersByItem[item].ShortcutKeyText = ShortcutKeyHelper.ShortcutKeyPrefix + ShortcutKeyHelper.GetKeyAsString(key.Value);
                    this.containersByItem[item].ShortcutKeyTextOpacity = this.showingShortcutKeys ? 1.0 : 0.0;
                }
            }
        }

        /// <summary>
        /// Removes a shortcut key from an item.
        /// </summary>
        /// <param name="item">The item to remove the shortcut key from.</param>
        /// <param name="key">The shortcut key to remove.</param>
        public void RemoveItemShortcutKey(object item, Key? key)
        {
            if (key.HasValue && this.itemsByShortcutKey.ContainsKey(key.Value))
            {
                this.itemsByShortcutKey.Remove(key.Value);
            }

            if (this.containersByItem.ContainsKey(item))
            {
                this.containersByItem[item].ShortcutKeyText = null;
            }

            if (this.shortcutKeysByItem.ContainsKey(item))
            {
                this.shortcutKeysByItem.Remove(item);
            }
        }

        /// <summary>
        /// Shows the shortcut keys.
        /// </summary>
        public void ShowShortcutKeys()
        {
            this.showingShortcutKeys = true;
            foreach (object item in this.Items)
            {
                if (this.containersByItem.ContainsKey(item))
                {
                    this.containersByItem[item].ShortcutKeyTextOpacity = 1.0;
                }
            }
        }

        /// <summary>
        /// Hides the shortcut keys.
        /// </summary>
        public void HideShortcutKeys()
        {
            this.showingShortcutKeys = false;
            foreach (object item in this.Items)
            {
                if (this.containersByItem.ContainsKey(item))
                {
                    this.containersByItem[item].ShortcutKeyTextOpacity = 0.0;
                }
            }
        }

        /// <summary>
        /// Adds a split to an item.
        /// </summary>
        /// <param name="item">The item to add the split to.</param>
        public void AddItemSplit(object item)
        {
            if (item != null)
            {
                if (this.containersByItem.ContainsKey(item))
                {
                    this.containersByItem[item].SplitBorderThickness = this.SplitBorderThickness;
                    this.containersByItem[item].SplitBorderBrush = this.SplitBorderBrush;
                }

                if (!this.splitItems.Contains(item))
                {
                    this.splitItems.Add(item);
                }
            }
        }

        /// <summary>
        /// Removes a split to an item.
        /// </summary>
        /// <param name="item">The item to remove the split from.</param>
        public void RemoveItemSplit(object item)
        {
            if (item != null)
            {
                if (this.containersByItem.ContainsKey(item))
                {
                    this.containersByItem[item].SplitBorderThickness = new Thickness(0, 0, 0, 0);
                }

                if (this.splitItems.Contains(item))
                {
                    this.splitItems.Remove(item);
                }
            }
        }

        /// <summary>
        /// Selects the item with an assigned shortcut key.
        /// </summary>
        /// <param name="key">The shortcut key.</param>
        /// <returns>Whether an item as selected.</returns>
        public bool SelectItemWithShortcutKey(Key key)
        {
            if (this.itemsByShortcutKey.ContainsKey(key) && this.Items.Contains(this.itemsByShortcutKey[key]))
            {
                this.ConfirmedSelectedItem = this.itemsByShortcutKey[key];
                this.RaiseItemSelected();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Raises the item selected event.
        /// </summary>
        public void RaiseItemSelected()
        {
            if (this.ConfirmedSelectedItem != null && this.ItemSelected != null)
            {
                this.ItemSelected(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets the template parts from the template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.scrollViewer = this.GetTemplateChild(SplitListBox.ElementScrollViewer) as ScrollViewer;
        }

        /// <summary>
        /// Checks if the item is a list box item.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>Returns true if the item is a list box item.</returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is SplitListBoxItem);
        }

        /// <summary>
        /// Creates a cascading list box item.
        /// </summary>
        /// <returns>A cascading list box item.</returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new SplitListBoxItem();
        }

        /// <summary>
        /// Prepares the split list box item.
        /// </summary>
        /// <param name="element">The split list box item.</param>
        /// <param name="item">The item object.</param>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            if (this.verticalScrollBar == null)
            {
                this.verticalScrollBar = this.GetVerticalScrollBar(element) as ScrollBar;
                if (this.verticalScrollBar != null)
                {
                    this.verticalScrollBar.Scroll += new ScrollEventHandler(this.VerticalScrollBar_Scroll);
                    this.verticalScrollBar.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.VerticalScrollBar_ValueChanged);
                }
            }

            SplitListBoxItem splitListBoxItem = element as SplitListBoxItem;
            if (splitListBoxItem != null)
            {
                splitListBoxItem.ParentSplitListBox = this;
                splitListBoxItem.ItemSelected += new EventHandler<SelectedEventArgs>(this.SplitListBoxItem_Selected);

                if (!this.containersByItem.ContainsKey(item))
                {
                    this.containersByItem.Add(item, splitListBoxItem);
                }
                else
                {
                    this.containersByItem[item] = splitListBoxItem;
                }

                if (this.ItemHeaderTemplate != null)
                {
                    splitListBoxItem.HeaderTemplate = this.ItemHeaderTemplate;
                }

                if (this.itemHeadersByItem.ContainsKey(item))
                {
                    splitListBoxItem.Header = this.itemHeadersByItem[item];
                    this.itemHeadersByItem.Remove(item);
                }

                if (this.shortcutKeysByItem.ContainsKey(item))
                {
                    splitListBoxItem.ShortcutKeyText = ShortcutKeyHelper.ShortcutKeyPrefix + ShortcutKeyHelper.GetKeyAsString(this.shortcutKeysByItem[item]);
                    splitListBoxItem.ShortcutKeyTextOpacity = this.showingShortcutKeys ? 1.0 : 0.0;
                }

                if (this.splitItems.Contains(item))
                {
                    splitListBoxItem.SplitBorderThickness = this.SplitBorderThickness;
                    splitListBoxItem.SplitBorderBrush = this.SplitBorderBrush;
                }

                if (item == this.OtherItem && this.OtherItemTemplate != null)
                {
                    splitListBoxItem.ContentTemplate = this.OtherItemTemplate;
                }
                else if (item == this.CustomValueItem && this.CustomValueItemTemplate != null)
                {
                    splitListBoxItem.ContentTemplate = this.CustomValueItemTemplate;
                }

                this.UpdateItemBackgrounds();
            }
        }

        /// <summary>
        /// Scroll callback.
        /// </summary>
        /// <param name="e">Event Args.</param>
        protected virtual void OnScroll(EventArgs e)
        {
        }

        /// <summary>
        /// Clears the container.
        /// </summary>
        /// <param name="element">The list box item.</param>
        /// <param name="item">The item object.</param>
        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);
            this.containersByItem.Remove(item);
        }

        /// <summary>
        /// Raises the items changed event.
        /// </summary>
        /// <param name="e">Notify Collection Changed Event Args.</param>
        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {            
            base.OnItemsChanged(e);
            if (this.scrollViewer != null)
            {
                this.scrollViewer.ScrollToVerticalOffset(0);
            }

#if SILVERLIGHT
            if (this.verticalScrollBar != null)
            {
                this.verticalScrollBar.Value = 0;
            }
#endif

            if (this.ConfirmedSelectedItem != null && !this.Items.Contains(this.ConfirmedSelectedItem))
            {
                this.ConfirmedSelectedItem = null;
            }

            if (this.ItemsChanged != null)
            {
                this.ItemsChanged(this, e);
            }
        }

        /// <summary>
        /// If a shortcut key was pressed, selects the item.
        /// </summary>
        /// <param name="e">Key Event Args.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ShortcutKeyHelper.ShortcutModifier && this.itemsByShortcutKey.ContainsKey(e.Key))
            {
                base.OnKeyDown(e);
                this.SelectItemWithShortcutKey(e.Key);
            }
            else if ((e.Key == Key.Down && this.SelectedIndex == this.Items.Count - 1) || e.Key == Key.Up && this.SelectedIndex == 0)
            {
                e.Handled = true;
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        /// <summary>
        /// Updates the items source.
        /// </summary>
        /// <param name="obj">The split list box.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void ItemsSource_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SplitListBox splitListBox = obj as SplitListBox;

            if (splitListBox.ShowOtherItems)
            {
                splitListBox.ShowOtherItems = false;
            }
            else
            {
                SplitItemsControlHelper.UpdateItemsSource(splitListBox);
            }
        }

        /// <summary>
        /// Updates the items source.
        /// </summary>
        /// <param name="obj">The split list box.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void OtherItemsSource_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SplitListBox splitListBox = obj as SplitListBox;
            SplitItemsControlHelper.UpdateItemsSource(splitListBox);
        }

        /// <summary>
        /// Updates the items source.
        /// </summary>
        /// <param name="obj">The split list box.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void ShowOtherItems_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SplitListBox splitListBox = obj as SplitListBox;
            SplitItemsControlHelper.UpdateItemsSource(splitListBox);
        }

        /// <summary>
        /// Updates the confirmed selected value.
        /// </summary>
        /// <param name="obj">The cascading list box.</param>
        /// <param name="args">Dependency Property Changed Args.</param>
        private static void ConfirmedSelectedItem_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SplitListBox splitListBox = obj as SplitListBox;
            splitListBox.UpdateSelectedValue(args.OldValue);
        }

        /// <summary>
        /// Updates the selected value.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        private void UpdateSelectedValue(object oldValue)
        {
            if (oldValue != null && this.containersByItem.ContainsKey(oldValue))
            {
                this.containersByItem[oldValue].IsSelectedValue = false;
            }

            if (this.ConfirmedSelectedItem != null)
            {
                if (this.containersByItem.ContainsKey(this.ConfirmedSelectedItem))
                {
                    this.containersByItem[this.ConfirmedSelectedItem].IsSelectedValue = true;
                }

                this.SelectedItem = this.ConfirmedSelectedItem;
            }

            if (this.ConfirmedSelectedItemChanged != null)
            {
                this.ConfirmedSelectedItemChanged(this, EventArgs.Empty);
            }

            if (this.ConfirmedSelectedItem != oldValue)
            {
                VisualStateManager.GoToState(this, "ConfirmedSelectedItemCleared", false);
                VisualStateManager.GoToState(this, "ConfirmedSelectedItemChanged", true);
            }
        }

        /// <summary>
        /// Updates the item backgrounds.
        /// </summary>
        private void UpdateItemBackgrounds()
        {
            int count = 0;
            foreach (object item in this.Items)
            {
                if (this.containersByItem.ContainsKey(item))
                {
                    if (this.containersByItem[item].Header != null || item == this.OtherItem || item == this.CustomValueItem)
                    {
                        count = 0;
                    }

                    if (count % 2 == 0)
                    {
                        this.containersByItem[item].Background = this.RowBackground;
                    }
                    else
                    {
                        this.containersByItem[item].Background = this.AlternateRowBackground;
                    }

                    count++;

                    if (this.containersByItem[item].BorderThickness != this.containersByItem[item].SplitBorderThickness)
                    {
                        count = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Shows the shortcut keys.
        /// </summary>
        /// <param name="sender">The split list box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void SplitListBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.HideShortcutKeys();
        }

        /// <summary>
        /// Hides the shortcut keys.
        /// </summary>
        /// <param name="sender">The split list box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void SplitListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ShowShortcutKeys();
        }

        /// <summary>
        /// Checks if other was selected and handles.
        /// </summary>
        /// <param name="sender">The split list box.</param>
        /// <param name="e">Selected Event Args.</param>
        private void SplitListBoxItem_Selected(object sender, SelectedEventArgs e)
        {
            if (e.SelectedItem == this.OtherItem)
            {
                e.Handled = true;
                this.ConfirmedSelectedItem = null;
                SplitListBoxItem splitListBoxItem = sender as SplitListBoxItem;
                splitListBoxItem.IsSelectedValue = false;

                if (this.OtherItemsSource != null)
                {
                    this.ShowOtherItems = true;
                }                

                if (this.OtherSelected != null)
                {
                    this.OtherSelected(this, EventArgs.Empty);
                }                
            }
            else
            {
                this.SelectedItem = e.SelectedItem;
                this.ConfirmedSelectedItem = e.SelectedItem;
                if (this.ConfirmedSelectedItem != null)
                {
                    this.ScrollIntoView(this.ConfirmedSelectedItem);
                }
            }
        }

        /// <summary>
        /// Raises the scroll event.
        /// </summary>
        /// <param name="sender">The vertical scroll bar.</param>
        /// <param name="e">Event Args.</param>
        private void VerticalScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.OnScroll(e);
            if (this.Scroll != null)
            {
                this.Scroll(this, e);
            }
        }

        /// <summary>
        /// Raises the scroll event.
        /// </summary>
        /// <param name="sender">The vertical scroll bar.</param>
        /// <param name="e">Event Args.</param>
        private void VerticalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.OnScroll(e);
            if (this.Scroll != null)
            {
                this.Scroll(this, e);
            }
        }

        /// <summary>
        /// Gets the vertical scroll bar from the scroll viewer.
        /// </summary>
        /// <param name="obj">The current element..</param>
        /// <returns>The vertical scroll bar.</returns>
        private ScrollBar GetVerticalScrollBar(DependencyObject obj)
        {
            if (obj == null)
            {
                return null;
            }

            if (VisualTreeHelper.GetParent(obj) is ScrollViewer)
            {
#if SILVERLIGHT
                return (obj as FrameworkElement).FindName("VerticalScrollBar") as ScrollBar;
#endif
#if !SILVERLIGHT
                return (obj as FrameworkElement).FindName("PART_VerticalScrollBar") as ScrollBar;
#endif
            }
            else if (VisualTreeHelper.GetParent(obj) != null)
            {
                return this.GetVerticalScrollBar(VisualTreeHelper.GetParent(obj));
            }

            return null;
        }
    }
}
