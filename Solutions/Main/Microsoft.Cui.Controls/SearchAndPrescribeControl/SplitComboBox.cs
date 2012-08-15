//-----------------------------------------------------------------------
// <copyright file="SplitComboBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>25-Aug-2009</date>
// <summary>
//      A split combo box control.
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
    using System.Windows.Data;

    /// <summary>
    ///  A split combo box control.
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(SplitComboBoxItem))]
    [TemplatePart(Name = SplitComboBox.ElementToggleButtonName, Type = typeof(ToggleButton))]
    [TemplatePart(Name = SplitComboBox.ElementPopupBorderName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = SplitComboBox.ElementPopupName, Type = typeof(Popup))]
    [TemplatePart(Name = SplitComboBox.ElementContentPresenterName, Type = typeof(ContentPresenter))]
    [TemplateVisualState(Name = "ConfirmedSelectedItemCleared", GroupName = "ConfirmedSelectedItemStates")]
    [TemplateVisualState(Name = "ConfirmedSelectedItemChanged", GroupName = "ConfirmedSelectedItemStates")]
    public class SplitComboBox : ComboBox, ISplitItemsControl
    {
        #region Dependency Properties
        /// <summary>
        /// The PrimaryItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty PrimaryItemsSourceProperty =
            DependencyProperty.Register("PrimaryItemsSource", typeof(IEnumerable), typeof(SplitComboBox), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The SecondaryItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SecondaryItemsSourceProperty =
            DependencyProperty.Register("SecondaryItemsSource", typeof(IEnumerable), typeof(SplitComboBox), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The OtherItem Dependency Property.
        /// </summary>
        public static readonly DependencyProperty OtherItemProperty =
            DependencyProperty.Register("OtherItem", typeof(object), typeof(SplitComboBox), new PropertyMetadata("other..."));

        /// <summary>
        /// The OtherItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty OtherItemTemplateProperty =
            DependencyProperty.Register("OtherItemTemplate", typeof(DataTemplate), typeof(SplitComboBox), new PropertyMetadata(null));

        /// <summary>
        /// The OtherItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty OtherItemsSourceProperty =
            DependencyProperty.Register("OtherItemsSource", typeof(IEnumerable), typeof(SplitComboBox), new PropertyMetadata(null, new PropertyChangedCallback(OtherItemsSource_Changed)));

        /// <summary>
        /// The ShowOtherOption Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShowOtherOptionProperty =
            DependencyProperty.Register("ShowOtherOption", typeof(bool), typeof(SplitComboBox), new PropertyMetadata(false, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The ShowOtherItems Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShowOtherItemsProperty =
            DependencyProperty.Register("ShowOtherItems", typeof(bool), typeof(SplitComboBox), new PropertyMetadata(false, new PropertyChangedCallback(ShowOtherItems_Changed)));

        /// <summary>
        /// The CustomValueItem Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomValueItemProperty =
           DependencyProperty.Register("CustomValueItem", typeof(object), typeof(SplitComboBox), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The ShowCustomValueOption Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShowCustomValueOptionProperty =
         DependencyProperty.Register("ShowCustomValueOption", typeof(bool), typeof(SplitComboBox), new PropertyMetadata(false, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The CustomValueItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomValueItemTemplateProperty =
            DependencyProperty.Register("CustomValueItemTemplate", typeof(DataTemplate), typeof(SplitComboBox), new PropertyMetadata(null));

        /// <summary>
        /// The ItemHeaderTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ItemHeaderTemplateProperty =
            DependencyProperty.Register("ItemHeaderTemplate", typeof(DataTemplate), typeof(SplitComboBox), null);

        /// <summary>
        /// The RowBackground Dependency Property.
        /// </summary>
        public static readonly DependencyProperty RowBackgroundProperty =
            DependencyProperty.Register("RowBackground", typeof(Brush), typeof(SplitComboBox), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        /// <summary>
        /// The AlternateRowBackground Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AlternateRowBackgroundProperty =
            DependencyProperty.Register("AlternateRowBackground", typeof(Brush), typeof(SplitComboBox), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        /// <summary>
        /// The ConfirmedSelectedItem Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ConfirmedSelectedItemProperty =
            DependencyProperty.Register("ConfirmedSelectedItem", typeof(object), typeof(SplitComboBox), new PropertyMetadata(null, new PropertyChangedCallback(ConfirmedSelectedItem_Changed)));

        /// <summary>
        /// The Watermark Dependency Property.
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(object), typeof(SplitComboBox), null);

        /// <summary>
        /// The WatermarkTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty WatermarkTemplateProperty =
            DependencyProperty.Register("WatermarkTemplate", typeof(DataTemplate), typeof(SplitComboBox), null);

        /// <summary>
        /// The WatermarkVisibility Dependency Property.
        /// </summary>
        public static readonly DependencyProperty WatermarkVisibilityProperty =
            DependencyProperty.Register("WatermarkVisibility", typeof(Visibility), typeof(SplitComboBox), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// The PrimaryItemsSourceHeader Dependency Property.
        /// </summary>
        public static readonly DependencyProperty PrimaryItemsSourceHeaderProperty =
            DependencyProperty.Register("PrimaryItemsSourceHeader", typeof(object), typeof(SplitComboBox), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The SecondaryItemsSourceHeader Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SecondaryItemsSourceHeaderProperty =
            DependencyProperty.Register("SecondaryItemsSourceHeader", typeof(object), typeof(SplitComboBox), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The OtherItemsSourceHeader Dependency Property.
        /// </summary>
        public static readonly DependencyProperty OtherItemsSourceHeaderProperty =
            DependencyProperty.Register("OtherItemsSourceHeader", typeof(object), typeof(SplitComboBox), new PropertyMetadata(null, new PropertyChangedCallback(ItemsSource_Changed)));

        /// <summary>
        /// The AddShortcutKeysToPrimaryItems Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AddShortcutKeysToPrimaryItemsProperty =
            DependencyProperty.Register("AddShortcutKeysToPrimaryItems", typeof(bool), typeof(SplitComboBox), new PropertyMetadata(false));

        /// <summary>
        /// The SelectedItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemTemplateProperty =
            DependencyProperty.Register("SelectedItemTemplate", typeof(DataTemplate), typeof(SplitComboBox), new PropertyMetadata(null, new PropertyChangedCallback(SelectedItemTemplate_Changed)));

        /// <summary>
        /// The PopupVerticalAlignment Dependency Property.
        /// </summary>
        public static readonly DependencyProperty PopupVerticalAlignmentProperty =
            DependencyProperty.Register("PopupVerticalAlignment", typeof(VerticalAlignment), typeof(SplitComboBox), new PropertyMetadata(VerticalAlignment.Top));

        /// <summary>
        /// The IsOuterBorderShowing Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsOuterBorderShowingProperty =
            DependencyProperty.Register("IsOuterBorderShowing", typeof(bool), typeof(SplitComboBox), new PropertyMetadata(false));

        /// <summary>
        /// The LinkBorderMargin Dependency Property.
        /// </summary>
        public static readonly DependencyProperty LinkBorderMarginProperty =
            DependencyProperty.Register("LinkBorderMargin", typeof(Thickness), typeof(SplitComboBox), new PropertyMetadata(new Thickness(0d)));

        /// <summary>
        /// The IsLinkBorderShowing Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsLinkBorderShowingProperty =
            DependencyProperty.Register("IsLinkBorderShowing", typeof(bool), typeof(SplitComboBox), new PropertyMetadata(false));

        /// <summary>
        /// The ResizeOnDropDownOpened Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ResizeOnDropDownOpenedProperty =
            DependencyProperty.Register("ResizeOnDropDownOpened", typeof(bool), typeof(SplitComboBox), new PropertyMetadata(true));

        /// <summary>
        /// The ClearConfirmedSelectedItemWhenOtherSelected Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ClearConfirmedSelectedItemWhenOtherSelectedProperty =
            DependencyProperty.Register("ClearConfirmedSelectedItemWhenOtherSelected", typeof(bool), typeof(SplitComboBox), new PropertyMetadata(true));

        /// <summary>
        /// The ShowSelectedItemChangedState Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShowSelectedItemChangedStateProperty =
            DependencyProperty.Register("ShowSelectedItemChangedState", typeof(bool), typeof(SplitComboBox), new PropertyMetadata(true));

        /// <summary>
        /// The AlwaysTopAlignDropDown Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AlwaysTopAlignDropDownProperty =
            DependencyProperty.Register("AlwaysTopAlignDropDown", typeof(bool), typeof(SplitComboBox), new PropertyMetadata(false));

        /// <summary>
        /// The SplitBorderThickness Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SplitBorderThicknessProperty =
            DependencyProperty.Register("SplitBorderThickness", typeof(Thickness), typeof(SplitComboBox), new PropertyMetadata(new Thickness(0, 0, 0, 1)));

        /// <summary>
        /// The SplitBorderBrush Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SplitBorderBrushProperty =
            DependencyProperty.Register("SplitBorderBrush", typeof(Brush), typeof(SplitComboBox), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0x00))));

        /// <summary>
        /// The CustomValueControlFocusContainer Attached Property.
        /// </summary>
        public static readonly DependencyProperty CustomValueControlFocusContainerProperty =
            DependencyProperty.RegisterAttached("CustomValueControlFocusContainer", typeof(object), typeof(SplitComboBox), new PropertyMetadata(null, new PropertyChangedCallback(CustomValueControlFocusContainer_Changed)));
        #endregion
#if !SILVERLIGHT
        /// <summary>
        /// The ToggleButton Element Name.
        /// </summary>
        private const string ElementToggleButtonName = "ToggleButton";

        /// <summary>
        /// The DropDown Element Name.
        /// </summary>
        private const string ElementPopupBorderName = "DropDownBorder";
#endif

#if SILVERLIGHT
        /// <summary>
        /// The DropDownToggle Element Name.
        /// </summary>
        private const string ElementToggleButtonName = "DropDownToggle";

        /// <summary>
        /// The PopupBorder Element Name.
        /// </summary>
        private const string ElementPopupBorderName = "PopupBorder";
#endif
        /// <summary>
        /// The Popup Element Name.
        /// </summary>
        private const string ElementPopupName = "Popup";

        /// <summary>
        /// The SelectedItemContentControl Element Name.
        /// </summary>
        private const string ElementContentPresenterName = "SelectedItemContentPresenter";

        /// <summary>
        /// Stores the toggle button.
        /// </summary>
        private ToggleButton toggleButton;

        /// <summary>
        /// Stores the selected item content control.
        /// </summary>
        private ContentPresenter contentPresenter;

        /// <summary>
        /// Stores the popup.
        /// </summary>
        private Popup popup;

        /// <summary>
        /// Stores the popup border.
        /// </summary>
        private FrameworkElement popupBorder;

        /// <summary>
        /// Stores if the control is focused.
        /// </summary>
        private bool isFocused;

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
        private Dictionary<object, SplitComboBoxItem> containersByItem = new Dictionary<object, SplitComboBoxItem>();

        /// <summary>
        /// Stores the items by shortcut key.
        /// </summary>
        private Dictionary<Key, object> itemsByShortcutKey = new Dictionary<Key, object>();

        /// <summary>
        /// Stores the shortcut keys by item.
        /// </summary>
        private Dictionary<object, Key> shortcutKeysByItem = new Dictionary<object, Key>();

        /// <summary>
        /// Stores the control in the custom value item template to focus.
        /// </summary>
        private Control customValueControlToFocus;

#if !SILVERLIGHT
        /// <summary>
        /// Stores if any top margin was added to the drop down.
        /// </summary>
        private bool dropDownTopMarginAdded;
#endif

        /// <summary>
        /// SplitComboBox constructor.
        /// </summary>
        public SplitComboBox()
        {
            this.DefaultStyleKey = typeof(SplitComboBox);
            this.DropDownOpened += new EventHandler(this.SplitComboBox_DropDownOpened);
            this.DropDownClosed += new EventHandler(this.SplitComboBox_DropDownClosed);
            this.GotFocus += new RoutedEventHandler(this.SplitComboBox_GotFocus);
            this.LostFocus += new RoutedEventHandler(this.SplitComboBox_LostFocus);
        }

        /// <summary>
        /// The ItemSelected Event.
        /// </summary>
        public event EventHandler ItemSelected;

        /// <summary>
        /// The OtherSelected Event.
        /// </summary>
        public event EventHandler OtherSelected;

        /// <summary>
        /// The ConfirmedSelectedItemChanged Event.
        /// </summary>
        public event EventHandler ConfirmedSelectedItemChanged;

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
        /// <value>The custom value item value.</value>
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
        /// <value>The custom value item template value.</value>
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
        /// Gets or sets primary row background.
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
        /// Gets or sets the confirmed selected item.
        /// </summary>
        /// <value>The confirmed selected item value.</value>
        public object ConfirmedSelectedItem
        {
            get { return (object)GetValue(ConfirmedSelectedItemProperty); }
            set { SetValue(ConfirmedSelectedItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the watermark.
        /// </summary>
        /// <value>The watermark value.</value>
        public object Watermark
        {
            get { return (object)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
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
        /// Gets or sets the watermark template.
        /// </summary>
        /// <value>The watermark template value.</value>
        public DataTemplate WatermarkTemplate
        {
            get { return (DataTemplate)GetValue(WatermarkTemplateProperty); }
            set { SetValue(WatermarkTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the watermark visibility.
        /// </summary>
        /// <value>The watermark visibility.</value>
        public Visibility WatermarkVisibility
        {
            get { return (Visibility)GetValue(WatermarkVisibilityProperty); }
            set { SetValue(WatermarkVisibilityProperty, value); }
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
        /// Gets or sets the selected item template.
        /// </summary>
        /// <value>The selected item template value.</value>
        public DataTemplate SelectedItemTemplate
        {
            get { return (DataTemplate)GetValue(SelectedItemTemplateProperty); }
            set { SetValue(SelectedItemTemplateProperty, value); }
        }

#if !SILVERLIGHT
        /// <summary>
        /// Gets or sets a value indicating whether the contol is focused.
        /// </summary>
        /// <value>The is focused value.</value>
        public new bool IsFocused
        {
            get { return this.isFocused; }
            set { this.isFocused = value; }
        }
#endif

#if SILVERLIGHT
        /// <summary>
        /// Gets or sets a value indicating whether the contol is focused.
        /// </summary>
        /// <value>The is focused value.</value>
        public bool IsFocused
        {
            get { return this.isFocused; }
            set { this.isFocused = value; }
        }
#endif

        /// <summary>
        /// Gets or sets a value indicating whether the drop down should open on got focus.
        /// </summary>
        /// <value>The open drop down on got focus value.</value>
        public bool OpenDropDownOnGotFocus
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the popup vertical alignment.
        /// </summary>
        /// <value>The popup vertical alignment.</value>
        public VerticalAlignment PopupVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(PopupVerticalAlignmentProperty); }
            set { SetValue(PopupVerticalAlignmentProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the outer border is showing.
        /// </summary>
        /// <value>Whether the outer border is showing.</value>
        public bool IsOuterBorderShowing
        {
            get { return (bool)GetValue(IsOuterBorderShowingProperty); }
            set { SetValue(IsOuterBorderShowingProperty, value); }
        }

        /// <summary>
        /// Gets or sets the link border margin.
        /// </summary>
        /// <value>The link border margin.</value>
        public Thickness LinkBorderMargin
        {
            get { return (Thickness)GetValue(LinkBorderMarginProperty); }
            set { SetValue(LinkBorderMarginProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the link border is showing.
        /// </summary>
        /// <value>Whether the link border is showing.</value>
        public bool IsLinkBorderShowing
        {
            get { return (bool)GetValue(IsLinkBorderShowingProperty); }
            set { SetValue(IsLinkBorderShowingProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control should resize when the drop down opens.
        /// </summary>
        /// <value>Whether the control resizes when the drop down opens.</value>
        public bool ResizeOnDropDownOpened
        {
            get { return (bool)GetValue(ResizeOnDropDownOpenedProperty); }
            set { SetValue(ResizeOnDropDownOpenedProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to clear the selected item when 'Other' is selected.
        /// </summary>
        /// <value>Whether to clear the selected item when 'Other' is selected.</value>
        public bool ClearConfirmedSelectedItemWhenOtherSelected
        {
            get { return (bool)GetValue(ClearConfirmedSelectedItemWhenOtherSelectedProperty); }
            set { SetValue(ClearConfirmedSelectedItemWhenOtherSelectedProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the selected item changed state.
        /// </summary>
        /// <value>Whether to show the selected item changed state.</value>
        public bool ShowSelectedItemChangedState
        {
            get { return (bool)GetValue(ShowSelectedItemChangedStateProperty); }
            set { SetValue(ShowSelectedItemChangedStateProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the drop down should always be top aligned.
        /// </summary>
        /// <value>Whether the drop down should always be top aligned.</value>
        public bool AlwaysTopAlignDropDown
        {
            get { return (bool)GetValue(AlwaysTopAlignDropDownProperty); }
            set { SetValue(AlwaysTopAlignDropDownProperty, value); }
        }

        /// <summary>
        /// Gets or sets the custom item focus control.
        /// </summary>
        /// <value>The custom value item control to set focus to.</value>
        public Control CustomValueControlToFocus
        {
            get
            {
                return this.customValueControlToFocus;
            }

            set
            {
                this.customValueControlToFocus = value;
                if (this.IsFocused && this.customValueControlToFocus != null)
                {
                    FocusHelper.FocusControl(this.customValueControlToFocus);
                }
            }
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
        /// Gets the custom value item template's control to focus container.
        /// </summary>
        /// <param name="obj">The custom value item template's control to focus.</param>
        /// <returns>The container.</returns>
        public static object GetCustomValueControlFocusContainer(DependencyObject obj)
        {
            return (object)obj.GetValue(CustomValueControlFocusContainerProperty);
        }

        /// <summary>
        /// Sets the custom value item template's control to focus container.
        /// </summary>
        /// <param name="obj">The custom value item template's control to focus.</param>
        /// <param name="value">The container.</param>
        public static void SetCustomValueControlFocusContainer(DependencyObject obj, object value)
        {
            obj.SetValue(CustomValueControlFocusContainerProperty, value);
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
        /// Adds a split to an item.
        /// </summary>
        /// <param name="item">The item to add the split to.</param>
        public void AddItemSplit(object item)
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

        /// <summary>
        /// Removes a split to an item.
        /// </summary>
        /// <param name="item">The item to remove the split from.</param>
        public void RemoveItemSplit(object item)
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

        /// <summary>
        /// Selects the item with an assigned shortcut key.
        /// </summary>
        /// <param name="key">The shortcut key.</param>
        /// <returns>Whether an item was selected.</returns>
        public bool SelectItemWithShortcutKey(Key key)
        {
            if (this.itemsByShortcutKey.ContainsKey(key) && this.Items.Contains(this.itemsByShortcutKey[key]))
            {
                this.ConfirmedSelectedItem = this.itemsByShortcutKey[key];
                this.IsDropDownOpen = false;
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
            this.toggleButton = this.GetTemplateChild(SplitComboBox.ElementToggleButtonName) as ToggleButton;
            this.popup = this.GetTemplateChild(SplitComboBox.ElementPopupName) as Popup;
#if SILVERLIGHT
#endif

            this.popupBorder = this.GetTemplateChild(SplitComboBox.ElementPopupBorderName) as FrameworkElement;
            if (this.popupBorder != null)
            {
                this.popupBorder.SizeChanged += new SizeChangedEventHandler(this.PopupBorder_SizeChanged);
            }

            this.contentPresenter = this.GetTemplateChild(SplitComboBox.ElementContentPresenterName) as ContentPresenter;
            if (this.contentPresenter != null)
            {
                this.contentPresenter.IsHitTestVisible = true;
            }

            this.UpdateWatermarkVisibility();
            this.UpdateSelectedItemTemplate();

            if (this.ShowSelectedItemChangedState && this.ConfirmedSelectedItem != null)
            {
                VisualStateManager.GoToState(this, "ConfirmedSelectedItemCleared", false);
                VisualStateManager.GoToState(this, "ConfirmedSelectedItemChanged", true);
            }
        }

        /// <summary>
        /// Returns is the item is a split combo box item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <returns>Whether the item is a split combo box item.</returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is SplitComboBoxItem;
        }

        /// <summary>
        /// Returns a new split combo box item.
        /// </summary>
        /// <returns>A new split combo box item.</returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new SplitComboBoxItem();
        }

        /// <summary>
        /// Prepares the split combo box item.
        /// </summary>
        /// <param name="element">The split combo box item.</param>
        /// <param name="item">The item object.</param>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            SplitComboBoxItem splitComboBoxItem = element as SplitComboBoxItem;
            if (splitComboBoxItem != null)
            {
                splitComboBoxItem.ParentSplitComboBox = this;
                splitComboBoxItem.ItemSelected += new EventHandler<SelectedEventArgs>(this.SplitComboBoxItem_ItemSelected);
                if (item == this.ConfirmedSelectedItem || item.Equals(this.ConfirmedSelectedItem))
                {
                    splitComboBoxItem.IsSelectedValue = true;
                }

                if (!this.containersByItem.ContainsKey(item))
                {
                    this.containersByItem.Add(item, splitComboBoxItem);
                }
                else
                {
                    this.containersByItem[item] = splitComboBoxItem;
                }

                if (this.ItemHeaderTemplate != null)
                {
                    splitComboBoxItem.HeaderTemplate = this.ItemHeaderTemplate;
                }

                if (this.itemHeadersByItem.ContainsKey(item))
                {
                    splitComboBoxItem.Header = this.itemHeadersByItem[item];
                    this.itemHeadersByItem.Remove(item);
                }

                if (this.shortcutKeysByItem.ContainsKey(item))
                {
                    splitComboBoxItem.ShortcutKeyText = ShortcutKeyHelper.ShortcutKeyPrefix + ShortcutKeyHelper.GetKeyAsString(this.shortcutKeysByItem[item]);
                }

                if (this.splitItems.Contains(item))
                {
                    splitComboBoxItem.SplitBorderThickness = this.SplitBorderThickness;
                    splitComboBoxItem.SplitBorderBrush = this.SplitBorderBrush;
                }

                if (item == this.OtherItem && this.OtherItemTemplate != null)
                {
                    splitComboBoxItem.ContentTemplate = this.OtherItemTemplate;
                }
                else if (item == this.CustomValueItem && this.CustomValueItemTemplate != null)
                {
                    splitComboBoxItem.ContentTemplate = this.CustomValueItemTemplate;
                }
#if !SILVERLIGHT
                else if (this.DisplayMemberPath != null && this.ItemTemplate == null)
                {
                    Binding contentBinding = new Binding(this.DisplayMemberPath);
                    contentBinding.Source = item;
                    splitComboBoxItem.SetBinding(ContentPresenter.ContentProperty, contentBinding);
                }
#endif

                if (this.IsDropDownOpen && this.ConfirmedSelectedItem == null && item == this.Items[0])
                {
                    FocusHelper.FocusControl(splitComboBoxItem);
                }

                this.UpdateItemBackgrounds();
            }
        }

        /// <summary>
        /// Clears the container.
        /// </summary>
        /// <param name="element">The split items control item item.</param>
        /// <param name="item">The item object.</param>
        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);
            this.containersByItem.Remove(item);
        }
        
        /// <summary>
        /// Updates the items source.
        /// </summary>
        /// <param name="e">Notify Collection Changed Event Args.</param>
        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            if (this.ItemsChanged != null)
            {
                this.ItemsChanged(this, e);
            }

            this.UpdateSelectedValue(this.ConfirmedSelectedItem);
        }

        /// <summary>
        /// If a shortcut key was pressed, selects the item.
        /// </summary>
        /// <param name="e">Key Event Args.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if ((e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right) && !this.IsDropDownOpen)
            {
                this.IsDropDownOpen = true;
            }
            else if (this.IsDropDownOpen && Keyboard.Modifiers == ShortcutKeyHelper.ShortcutModifier && this.itemsByShortcutKey.ContainsKey(e.Key))
            {
                this.SelectItemWithShortcutKey(e.Key);
            }
        }

        /// <summary>
        /// Updates the items source.
        /// </summary>
        /// <param name="obj">The split items control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void ItemsSource_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SplitComboBox splitComboBox = obj as SplitComboBox;
            if (splitComboBox.ShowOtherItems)
            {
                splitComboBox.ShowOtherItems = false;
            }
            else
            {
                SplitItemsControlHelper.UpdateItemsSource(splitComboBox);
            }
        }

        /// <summary>
        /// Updates the items source.
        /// </summary>
        /// <param name="obj">The split items control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void OtherItemsSource_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SplitComboBox splitComboBox = obj as SplitComboBox;
            SplitItemsControlHelper.UpdateItemsSource(splitComboBox);
        }

        /// <summary>
        /// Updates the items source.
        /// </summary>
        /// <param name="obj">The split items control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void ShowOtherItems_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SplitComboBox splitComboBox = obj as SplitComboBox;
            SplitItemsControlHelper.UpdateItemsSource(splitComboBox);
        }

        /// <summary>
        /// Updates the selected item template.
        /// </summary>
        /// <param name="obj">The cascading list box.</param>
        /// <param name="args">Dependency Property Changed Args.</param>
        private static void SelectedItemTemplate_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SplitComboBox splitComboBox = obj as SplitComboBox;
            splitComboBox.UpdateSelectedItemTemplate();
        }

        /// <summary>
        /// Updates the confirmed selected item.
        /// </summary>
        /// <param name="obj">The split items control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void ConfirmedSelectedItem_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SplitComboBox splitComboBox = obj as SplitComboBox;
            splitComboBox.UpdateSelectedValue(args.OldValue);
            splitComboBox.UpdateWatermarkVisibility();
            splitComboBox.UpdateSelectedItemTemplate();
        }

        /// <summary>
        /// Sets the control to focus within the custom value item template.
        /// </summary>
        /// <param name="obj">The control to focus.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void CustomValueControlFocusContainer_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            Control control = obj as Control;

            if (control != null)
            {
                SplitComboBox splitComboBox = GetSplitComboBoxFromDependencyObject(args.NewValue as DependencyObject);
                if (splitComboBox != null)
                {
                    splitComboBox.CustomValueControlToFocus = control;
                }
            }
        }

        /// <summary>
        /// Moves up the visual tree from a control until a SplitComboBox is reached.
        /// </summary>
        /// <param name="obj">A control in the custom value item template.</param>
        /// <returns>The parent split combo box.</returns>
        private static SplitComboBox GetSplitComboBoxFromDependencyObject(DependencyObject obj)
        {
            if (obj != null)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(obj);
                SplitComboBox splitComboBox = parent as SplitComboBox;
                while (parent != null && splitComboBox == null)
                {
                    parent = VisualTreeHelper.GetParent(parent);
                    splitComboBox = parent as SplitComboBox;
                }

                if (splitComboBox != null)
                {
                    return splitComboBox;
                }
            }

            return null;
        }

#if SILVERLIGHT
        /// <summary>
        /// Gets the rendered popup element from a child in the visual tree.
        /// </summary>
        /// <param name="child">The child to get the rendered popup element from.</param>
        /// <returns>The rendered popup element.</returns>
        private static FrameworkElement GetPopupElement(DependencyObject child)
        {
            if (child == null)
            {
                return null;
            }

            if (VisualTreeHelper.GetParent(child) != null)
            {
                Canvas canvas = VisualTreeHelper.GetParent(child) as Canvas;
                if (canvas != null && VisualTreeHelper.GetParent(canvas) == null)
                {
                    return child as FrameworkElement;
                }
                else
                {
                    return SplitComboBox.GetPopupElement(VisualTreeHelper.GetParent(child));
                }
            }

            return null;
        }
#endif

        /// <summary>
        /// Updates the selected value.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        private void UpdateSelectedValue(object oldValue)
        {
            this.SelectedItem = this.ConfirmedSelectedItem;
            if (oldValue != null && oldValue != this.ConfirmedSelectedItem && !oldValue.Equals(this.ConfirmedSelectedItem) && this.containersByItem.ContainsKey(oldValue))
            {
                this.containersByItem[oldValue].IsSelectedValue = false;
            }

            if (this.ConfirmedSelectedItem != oldValue && this.ConfirmedSelectedItemChanged != null)
            {
                this.ConfirmedSelectedItemChanged(this, EventArgs.Empty);
            }

            if (this.ShowSelectedItemChangedState && this.ConfirmedSelectedItem != oldValue && (this.ConfirmedSelectedItem == null || !this.ConfirmedSelectedItem.Equals(oldValue)))
            {
                VisualStateManager.GoToState(this, "ConfirmedSelectedItemCleared", false);
                VisualStateManager.GoToState(this, "ConfirmedSelectedItemChanged", true);
            }
        }

        /// <summary>
        /// Updates the selected item template.
        /// </summary>
        private void UpdateSelectedItemTemplate()
        {
            if (this.contentPresenter != null)
            {
                if (this.ConfirmedSelectedItem != null)
                {
                    if ((this.ConfirmedSelectedItem == this.CustomValueItem || this.ConfirmedSelectedItem.Equals(this.CustomValueItem)) && this.CustomValueItemTemplate != null)
                    {
                        this.contentPresenter.ContentTemplate = this.CustomValueItemTemplate;
                    }
                    else if ((this.ConfirmedSelectedItem == this.OtherItem || this.ConfirmedSelectedItem.Equals(this.OtherItem)) && this.OtherItemTemplate != null)
                    {
                        this.contentPresenter.ContentTemplate = this.OtherItemTemplate;
                    }
                    else if (this.SelectedItemTemplate != null)
                    {
                        this.contentPresenter.ContentTemplate = this.SelectedItemTemplate;
                    }
                    else if (this.ItemTemplate != null)
                    {
                        this.contentPresenter.ContentTemplate = this.ItemTemplate;
                    }

                    this.UpdateContent();
                }
                else
                {
                    this.contentPresenter.Content = null;
                    this.contentPresenter.ContentTemplate = null;
                }
            }
        }

        /// <summary>
        /// Updates the selected content presenter.
        /// </summary>
        private void UpdateContent()
        {
#if SILVERLIGHT
            if (this.DisplayMemberPath != null && this.contentPresenter.ContentTemplate == null)
            {
                Binding contentBinding = new Binding(this.DisplayMemberPath);
                contentBinding.Source = this.ConfirmedSelectedItem;
                this.contentPresenter.SetBinding(ContentPresenter.ContentProperty, contentBinding);
            }
            else
            {
                this.contentPresenter.Content = this.ConfirmedSelectedItem;
            }
#else
            if ((this.ConfirmedSelectedItem == null || this.Items.Contains(this.ConfirmedSelectedItem)) && this.SelectionBoxItem != null && !string.IsNullOrEmpty(this.SelectionBoxItem.ToString()))
            {
                if (this.DisplayMemberPath != null && this.contentPresenter.ContentTemplate == null)
                {
                    Binding contentBinding = new Binding(this.DisplayMemberPath);
                    contentBinding.Source = this.SelectionBoxItem;
                    this.contentPresenter.SetBinding(ContentPresenter.ContentProperty, contentBinding);
                }
                else
                {
                    this.contentPresenter.Content = this.SelectionBoxItem;
                }
            }
            else
            {
                if (this.DisplayMemberPath != null && this.contentPresenter.ContentTemplate == null)
                {
                    Binding contentBinding = new Binding(this.DisplayMemberPath);
                    contentBinding.Source = this.ConfirmedSelectedItem;
                    this.contentPresenter.SetBinding(ContentPresenter.ContentProperty, contentBinding);
                }
                else
                {
                    this.contentPresenter.Content = this.ConfirmedSelectedItem;
                }
            }

            UIElement selectionBoxItem = this.SelectionBoxItem as UIElement;
            if (selectionBoxItem != null)
            {
                selectionBoxItem.IsHitTestVisible = false;
            }
#endif
        }

        /// <summary>
        /// Updates the watermark visibility.
        /// </summary>
        private void UpdateWatermarkVisibility()
        {
            if (this.ConfirmedSelectedItem != null)
            {
                this.WatermarkVisibility = Visibility.Collapsed;
            }
            else
            {
                this.WatermarkVisibility = Visibility.Visible;
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
        /// Selected the item as the confirmed selected item.
        /// </summary>
        /// <param name="sender">The split combo box item.</param>
        /// <param name="e">Event Args.</param>
        private void SplitComboBoxItem_ItemSelected(object sender, SelectedEventArgs e)
        {
            this.IsDropDownOpen = false;

            object[] items = IEnumerableHelper.GetItems(this.containersByItem.Keys);
            foreach (object item in items)
            {
                if (item == this.ConfirmedSelectedItem || item.Equals(this.ConfirmedSelectedItem))
                {
                    this.containersByItem[item].IsSelectedValue = false;
                }
            }

            if (e.SelectedItem == this.OtherItem)
            {
                e.Handled = true;

                if (this.ClearConfirmedSelectedItemWhenOtherSelected)
                {
                    this.ConfirmedSelectedItem = null;
                }

                SplitComboBoxItem splitComboBoxItem = sender as SplitComboBoxItem;
                splitComboBoxItem.IsSelectedValue = false;

                if (this.OtherItemsSource != null)
                {
                    this.ShowOtherItems = true;
                }
            }
            else
            {
                this.SelectedItem = e.SelectedItem;
                this.ConfirmedSelectedItem = e.SelectedItem;
            }

            if (e.SelectedItem != this.OtherItem)
            {
                this.RaiseItemSelected();
            }
            else
            {
                if (this.OtherSelected != null)
                {
                    this.OtherSelected(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Positions the popup border and sets focus.
        /// </summary>
        /// <param name="sender">The drop down.</param>
        /// <param name="e">Event Args.</param>
        private void SplitComboBox_DropDownOpened(object sender, EventArgs e)
        {
            if (this.popupBorder != null && this.toggleButton != null)
            {
                this.popupBorder.MinWidth = this.toggleButton.ActualWidth;
            }

            if (this.ConfirmedSelectedItem != null && this.containersByItem.ContainsKey(this.ConfirmedSelectedItem))
            {
                FocusHelper.FocusControl(this.containersByItem[this.ConfirmedSelectedItem]);
                this.containersByItem[this.ConfirmedSelectedItem].IsSelectedValue = true;
            }
            else if (this.ConfirmedSelectedItem == null && this.Items.Count > 0 && this.containersByItem.ContainsKey(this.Items[0]))
            {
                FocusHelper.FocusControl(this.containersByItem[this.Items[0]]);
            }

#if !SILVERLIGHT
            if (this.popup != null)
            {
                this.popup.VerticalOffset += this.toggleButton.ActualHeight;
                this.popup.HorizontalOffset -= this.popupBorder.ActualWidth - this.toggleButton.ActualWidth;

                if (this.PopupVerticalAlignment != VerticalAlignment.Bottom)
                {
                    if (this.ConfirmedSelectedItem == null || this.AlwaysTopAlignDropDown)
                    {
                        this.dropDownTopMarginAdded = true;
                        this.popup.VerticalOffset -= this.toggleButton.ActualHeight;
                    }

                    this.popup.HorizontalOffset += this.popupBorder.ActualWidth - this.toggleButton.ActualWidth;
                }
            }

#endif

            if (this.ResizeOnDropDownOpened && this.popupBorder != null && this.toggleButton != null)
            {
                this.toggleButton.MinWidth = this.popupBorder.ActualWidth;
            }

#if SILVERLIGHT
            if (this.ConfirmedSelectedItem is UIElement && this.contentPresenter != null)
            {
                this.contentPresenter.Content = null;
                this.contentPresenter.ContentTemplate = null;

                foreach (object item in this.Items)
                {
                    if (item is UIElement && this.containersByItem.ContainsKey(item))
                    {
                        this.containersByItem[item].Content = item;
                    }
                }
            }
            else
            {
                this.UpdateSelectedItemTemplate();
            }
#else
            this.UpdateSelectedItemTemplate();
#endif
        }

        /// <summary>
        /// Updates the position of the popup border.
        /// </summary>
        /// <param name="sender">The popup border.</param>
        /// <param name="e">Size Changed Event Args.</param>
        private void PopupBorder_SizeChanged(object sender, SizeChangedEventArgs e)
        {
#if SILVERLIGHT
            if (this.PopupVerticalAlignment == VerticalAlignment.Bottom)
            {
                if (Application.Current.RootVisual != null)
                {
                    Point controlPosition = this.TransformToVisual(Application.Current.RootVisual).Transform(new Point(0, 0));
                    this.popupBorder.Margin = new Thickness(-Math.Min(e.NewSize.Width, controlPosition.X), 0, 0, 0);
                }
                else
                {
                    this.popupBorder.Margin = new Thickness(-e.NewSize.Width, 0, 0, 0);
                }
            }
            else
            {
                double top = (this.ConfirmedSelectedItem == null || this.AlwaysTopAlignDropDown) ? -this.toggleButton.ActualHeight : 0;
                this.popupBorder.Margin = new Thickness(0, top, 0, this.toggleButton.ActualHeight);
            }

            CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_UpdatePopupPosition);
            CompositionTarget.Rendering += new EventHandler(this.CompositionTargetRendering_UpdatePopupPosition);
#endif

            if (this.ResizeOnDropDownOpened && this.popupBorder != null && this.toggleButton != null)
            {
                this.toggleButton.MinWidth = this.popupBorder.ActualWidth;
            }
        }

#if SILVERLIGHT
        /// <summary>
        /// Updates the popup position if it is set to appear partly off screen.
        /// </summary>
        /// <param name="sender">The composition target.</param>
        /// <param name="e">Event Args.</param>
        private void CompositionTargetRendering_UpdatePopupPosition(object sender, EventArgs e)
        {
            CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_UpdatePopupPosition);
            FrameworkElement popupElement = SplitComboBox.GetPopupElement(this.popupBorder);
            if (popupElement != null)
            {
                if (Canvas.GetLeft(popupElement) < 0)
                {
                    popupElement.Margin = new Thickness(Math.Abs(Canvas.GetLeft(popupElement)), 0, 0, 0);
                }
                else
                {
                    popupElement.Margin = new Thickness(0, 0, 0, 0);
                }
            }
        }
#endif

        /// <summary>
        /// Focuses the toggle button.
        /// </summary>
        /// <param name="sender">The drop down.</param>
        /// <param name="e">Event Args.</param>
        private void SplitComboBox_DropDownClosed(object sender, EventArgs e)
        {
#if SILVERLIGHT
            foreach (object item in this.Items)
            {
                if (item is UIElement && this.containersByItem.ContainsKey(item) && this.containersByItem[item].Content == item)
                {
                    this.containersByItem[item].Content = null;
                }
            }
#endif

#if !SILVERLIGHT 
            if (this.PopupVerticalAlignment == VerticalAlignment.Bottom)
            {
                this.popup.HorizontalOffset += this.popupBorder.ActualWidth - this.toggleButton.ActualWidth;
                this.popup.VerticalOffset -= this.toggleButton.ActualHeight;
            }
            else
            {
                if (this.dropDownTopMarginAdded)
                {
                    this.dropDownTopMarginAdded = false;
                }
                else
                {
                    this.popup.VerticalOffset -= this.toggleButton.ActualHeight;
                }
            }
#endif

            if (this.toggleButton != null)
            {
                this.toggleButton.MinWidth = 0;
                FocusHelper.FocusControl(this.toggleButton);
            }

            this.SelectedItem = this.ConfirmedSelectedItem;
            this.UpdateSelectedItemTemplate();

            if (this.CustomValueControlToFocus != null && this.ConfirmedSelectedItem != null && (this.ConfirmedSelectedItem == this.CustomValueItem || this.ConfirmedSelectedItem.Equals(this.CustomValueItem)))
            {
                FocusHelper.FocusControl(this.CustomValueControlToFocus);
            }
        }

        /// <summary>
        /// Opens the drop down if there is no selected item.
        /// </summary>
        /// <param name="sender">The Split Comb Box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void SplitComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.isFocused = true;

            if (!this.IsDropDownOpen)
            {
                if (this.toggleButton != null && e.OriginalSource == this)
                {
                    this.toggleButton.Focus();
                }
            }

            if (this.OpenDropDownOnGotFocus && this.ConfirmedSelectedItem == null && this.Items.Count > 0)
            {
                CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_OpenDropDown);
                CompositionTarget.Rendering += new EventHandler(this.CompositionTargetRendering_OpenDropDown);
            }
        }

        /// <summary>
        /// Opens the drop down.
        /// </summary>
        /// <param name="sender">The composition target.</param>
        /// <param name="e">Event Args.</param>
        private void CompositionTargetRendering_OpenDropDown(object sender, EventArgs e)
        {
            CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_OpenDropDown);
            this.IsDropDownOpen = true;
            this.OpenDropDownOnGotFocus = false;
        }

        /// <summary>
        /// Sets the is focused flag to false.
        /// </summary>
        /// <param name="sender">The split combo box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void SplitComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.isFocused = false;
        }
    }
}
