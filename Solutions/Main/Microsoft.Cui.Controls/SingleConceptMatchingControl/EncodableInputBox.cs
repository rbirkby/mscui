//-----------------------------------------------------------------------
// <copyright file="EncodableInputBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to allow entry of a single concept from a list.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    /// <summary>
    /// The control used to allow entry of a single concept from a list.
    /// </summary>    
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(ListBoxItem))]
    [StyleTypedProperty(Property = "EncodedButtonStyle", StyleTargetType = typeof(Button))]
    [StyleTypedProperty(Property = "SearchButtonStyle", StyleTargetType = typeof(Button))]
    [TemplatePart(Name = EncodableInputBox.ElementTextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = EncodableInputBox.ElementSearchButton, Type = typeof(Button))]
    [TemplatePart(Name = EncodableInputBox.ElementEncodedButton, Type = typeof(Button))]
    [TemplatePart(Name = EncodableInputBox.ElementPopup, Type = typeof(Popup))]
    [TemplatePart(Name = EncodableInputBox.ElementConceptListBox, Type = typeof(ConceptListBox))]
    [TemplatePart(Name = EncodableInputBox.ElementFlyOutGrid, Type = typeof(Grid))]
    [TemplatePart(Name = EncodableInputBox.ElementPopupBorder, Type = typeof(Border))]
    [TemplatePart(Name = EncodableInputBox.ElementFlyOutContentPresenter, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = EncodableInputBox.ElementWatermarkContentPresenter, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = EncodableInputBox.ElementHidePopupGrid, Type = typeof(Grid))]
    [TemplatePart(Name = EncodableInputBox.ElementFlyOutCanvas, Type = typeof(Canvas))]
    public class EncodableInputBox : Control
    {
        #region Dependency properties
        /// <summary>
        /// The ItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(EncodableInputBox), new PropertyMetadata(null));

        /// <summary>
        /// The Text Dependency Property.
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(EncodableInputBox), new PropertyMetadata(string.Empty, new PropertyChangedCallback(Text_Changed)));

        /// <summary>
        /// The drop down maximum height Dependency Property.
        /// </summary>
        public static readonly DependencyProperty DropdownMaxHeightProperty =
            DependencyProperty.Register("DropdownMaxHeight", typeof(double), typeof(EncodableInputBox), new PropertyMetadata(200d));

        /// <summary>
        /// The flyout width Dependency Property.
        /// </summary>
        public static readonly DependencyProperty FlyOutWidthProperty =
            DependencyProperty.Register("FlyOutWidth", typeof(double), typeof(EncodableInputBox), new PropertyMetadata(220d));

        /// <summary>
        /// The item template Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(EncodableInputBox), new PropertyMetadata(null));

        /// <summary>
        /// The Encoded Item Template Dependency Property.
        /// </summary>
        public static readonly DependencyProperty EncodedItemTemplateProperty =
            DependencyProperty.Register("EncodedItemTemplate", typeof(DataTemplate), typeof(EncodableInputBox), new PropertyMetadata(null));

        /// <summary>
        /// The Is Search Button Visible Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsSearchButtonVisibleProperty =
            DependencyProperty.Register("IsSearchButtonVisible", typeof(bool), typeof(EncodableInputBox), new PropertyMetadata(new PropertyChangedCallback(IsSearchButtonVisible_Changed)));

        /// <summary>
        /// The item container style Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ItemContainerStyleProperty =
            DependencyProperty.Register("ItemContainerStyle", typeof(Style), typeof(EncodableInputBox), null);

        /// <summary>
        /// The FlyOut Content Dependency Property.
        /// </summary>
        public static readonly DependencyProperty FlyOutContentProperty =
            DependencyProperty.Register("FlyOutContent", typeof(object), typeof(EncodableInputBox), new PropertyMetadata(null));

        /// <summary>
        /// The FlyOut Content Template Dependency Property.
        /// </summary>
        public static readonly DependencyProperty FlyOutContentTemplateProperty =
            DependencyProperty.Register("FlyOutContentTemplate", typeof(DataTemplate), typeof(EncodableInputBox), null);

        /// <summary>
        /// The Encoded Button Style Dependency Property.
        /// </summary>
        public static readonly DependencyProperty EncodedButtonStyleProperty =
            DependencyProperty.Register("EncodedButtonStyle", typeof(Style), typeof(EncodableInputBox), null);

        /// <summary>
        /// The Search Button Style Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SearchButtonStyleProperty =
            DependencyProperty.Register("SearchButtonStyle", typeof(Style), typeof(EncodableInputBox), null);

        /// <summary>
        /// The Selected Item Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(EncodableInputBox), new PropertyMetadata(new PropertyChangedCallback(SelectedItem_Changed)));

        /// <summary>
        /// The Is ReadOnly Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(EncodableInputBox), new PropertyMetadata(new PropertyChangedCallback(IsReadOnly_Changed)));

        /// <summary>
        /// The watermark Dependency Property.
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(object), typeof(EncodableInputBox), null);

        /// <summary>
        /// The watermark template Dependency Property.
        /// </summary>
        public static readonly DependencyProperty WatermarkTemplateProperty =
            DependencyProperty.Register("WatermarkTemplate", typeof(DataTemplate), typeof(EncodableInputBox), null);

        /// <summary>
        /// The Maximum Length template Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof(int), typeof(EncodableInputBox), new PropertyMetadata(255));

        /// <summary>
        /// The SearchButtonToolTip Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SearchButtonToolTipProperty =
            DependencyProperty.Register("SearchButtonToolTip", typeof(object), typeof(EncodableInputBox), new PropertyMetadata("Start search", new PropertyChangedCallback(SearchButtonToolTip_Changed)));
        #endregion

        /// <summary>
        /// The template part name for the text box.
        /// </summary>
        private const string ElementTextBox = "TextBox";

        /// <summary>
        /// The template part name for the search button.
        /// </summary>
        private const string ElementSearchButton = "SearchButton";

        /// <summary>
        /// The template part name for the encoded button.
        /// </summary>
        private const string ElementEncodedButton = "EncodedButton";

        /// <summary>
        /// The template part name for the popup.
        /// </summary>
        private const string ElementPopup = "Popup";

        /// <summary>
        /// The template part name for the list box.
        /// </summary>
        private const string ElementConceptListBox = "ConceptListBox";

        /// <summary>
        /// The template part name for the flyout grid.
        /// </summary>
        private const string ElementFlyOutGrid = "FlyOutGrid";

        /// <summary>
        /// The template part name for the popup border.
        /// </summary>
        private const string ElementPopupBorder = "PopupBorder";

        /// <summary>
        /// The template part name for the flyout content presenter.
        /// </summary>
        private const string ElementFlyOutContentPresenter = "FlyOutContentPresenter";

        /// <summary>
        /// The watermark ContentPresenter name.
        /// </summary>
        private const string ElementWatermarkContentPresenter = "WatermarkContentPresenter";

        /// <summary>
        /// The template part name for the popup canvas.
        /// </summary>
        private const string ElementHidePopupGrid = "HidePopupGrid";

        /// <summary>
        /// The template part name for the flyout canvas.
        /// </summary>
        private const string ElementFlyOutCanvas = "FlyOutCanvas";

        /// <summary>
        /// Stores the popup control.
        /// </summary>
        private Popup popup;

        /// <summary>
        /// Stores the hide popup grid.
        /// </summary>
        private Grid hidePopupGrid;

        /// <summary>
        /// Stores the textbox control.
        /// </summary>
        private TextBox textBox;

        /// <summary>
        /// Stores the listbox control.
        /// </summary>
        private ConceptListBox conceptListBox;

        /// <summary>
        /// Stores the flyout grid.
        /// </summary>
        private Grid flyoutGrid;

        /// <summary>
        /// Stores the popup border.
        /// </summary>
        private Border popupBorder;

        /// <summary>
        /// Stores the search button.
        /// </summary>
        private Button searchButton;

        /// <summary>
        /// Stores the encoded button.
        /// </summary>
        private Button encodedButton;

        /// <summary>
        /// Stores is the control has focus.
        /// </summary>
        private bool hasFocus;

        /// <summary>
        /// Member variable to indicate whether the lost focus and got focus events for the root visual have been initialized.
        /// </summary>
        private bool rootVisualFocusHandlersIntialized;

        /// <summary>
        /// Timer for hiding the popup.
        /// </summary>
        private DispatcherTimer hidePopupTimer;

        /// <summary>
        /// Timer for showing the popup.
        /// </summary>
        private DispatcherTimer showPopupTimer;

        /// <summary>
        /// Stores the flyout content presenter.
        /// </summary>
        private ContentPresenter flyoutContentPresenter;

        /// <summary>
        /// Stores the watermark content presenter.
        /// </summary>
        private ContentPresenter watermarkContentPresenter;

        /// <summary>
        /// Stores the fly out canvas.
        /// </summary>
        private Canvas flyoutCanvas;

#if SILVERLIGHT
        /// <summary>
        /// Stores if the root visual has focus.
        /// </summary>
        private bool rootVisualHasFocus;

        /// <summary>
        /// A timer for recording when the Silverlight application loses focus.
        /// </summary>
        private DispatcherTimer rootVisualFocusTimer;
#endif

        /// <summary>
        /// Stores the currently selected item.
        /// </summary>
        private object currentlySelectedItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodableInputBox"/> class.
        /// </summary>
        public EncodableInputBox()
        {
            this.DefaultStyleKey = typeof(EncodableInputBox);
            this.Text = string.Empty;
            this.hidePopupTimer = new DispatcherTimer();
            this.hidePopupTimer.Tick += new EventHandler(this.HidePopupTimer_Tick);
            this.hidePopupTimer.Interval = TimeSpan.FromMilliseconds(0);

            this.showPopupTimer = new DispatcherTimer();
            this.showPopupTimer.Tick += new EventHandler(this.ShowPopupTimer_Tick);
            this.showPopupTimer.Interval = TimeSpan.FromMilliseconds(0);

            this.SizeChanged += new SizeChangedEventHandler(this.EncodableInputBox_SizeChanged);
            this.GotFocus += new RoutedEventHandler(this.EncodableInputBox_GotFocus);
            this.LostFocus += new RoutedEventHandler(this.EncodableInputBox_LostFocus);

            this.MouseMove += new MouseEventHandler(this.EncodableInputBox_MouseMove);
        }

        /// <summary>
        /// The Text Changed event.
        /// </summary>
        public event TextChangedEventHandler TextChanged;

        /// <summary>
        /// The search clicked event.
        /// </summary>
        public event RoutedEventHandler SearchClicked;

        /// <summary>
        /// The enter key pressed event.
        /// </summary>
        public event KeyEventHandler EnterPressed;

        /// <summary>
        /// The selection changed event.
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// The mouse entered popup event.
        /// </summary>
        public event MouseEventHandler MouseEnterPopup;

        /// <summary>
        /// The mouse leaves popup event.
        /// </summary>
        public event MouseEventHandler MouseLeavePopup;

        /// <summary>
        /// The PopupOpening event.
        /// </summary>
        public event EventHandler PopupOpening;

        /// <summary>
        /// The PopupClosing event.
        /// </summary>
        public event EventHandler PopupClosing;

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>An IEnumerable value.</value>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text property.
        /// </summary>
        /// <value>A string value.</value>
        public string Text
        {
            get 
            {
                if (GetValue(TextProperty) == null)
                {
                    return string.Empty;
                }

                return (string)GetValue(TextProperty); 
            }

            set 
            { 
                SetValue(TextProperty, value); 
            }
        }

        /// <summary>
        /// Gets or sets the drop down maximum height.
        /// </summary>
        /// <value>A double value.</value>
        public double DropdownMaxHeight
        {
            get { return (double)GetValue(DropdownMaxHeightProperty); }
            set { SetValue(DropdownMaxHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the flyout width.
        /// </summary>
        /// <value>A double value.</value>
        public double FlyOutWidth
        {
            get { return (double)GetValue(FlyOutWidthProperty); }
            set { SetValue(FlyOutWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the item template.
        /// </summary>
        /// <value>A data template value.</value>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
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

        /// <summary>
        /// Gets or sets a value indicating whether the search button is visible.
        /// </summary>
        /// <value>A bool value.</value>
        public bool IsSearchButtonVisible
        {
            get { return (bool)GetValue(IsSearchButtonVisibleProperty); }
            set { SetValue(IsSearchButtonVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the item container style.
        /// </summary>
        /// <value>The item container style.</value>
        public Style ItemContainerStyle
        {
            get { return (Style)GetValue(ItemContainerStyleProperty); }
            set { SetValue(ItemContainerStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the flyout content.
        /// </summary>
        /// <value>The flyout content.</value>
        public object FlyOutContent
        {
            get { return (object)GetValue(FlyOutContentProperty); }
            set { SetValue(FlyOutContentProperty, value); }
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
        /// Gets or sets the encoded button style.
        /// </summary>
        /// <value>The encoded button style.</value>
        public Style EncodedButtonStyle
        {
            get { return (Style)GetValue(EncodedButtonStyleProperty); }
            set { SetValue(EncodedButtonStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search button style.
        /// </summary>
        /// <value>The search button style.</value>
        public Style SearchButtonStyle
        {
            get { return (Style)GetValue(SearchButtonStyleProperty); }
            set { SetValue(SearchButtonStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is read only.
        /// </summary>
        /// <value>The is read only value.</value>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the watermark.
        /// </summary>
        /// <value>An object value.</value>
        public object Watermark
        {
            get { return (object)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        /// <summary>
        /// Gets or sets the watermark template.
        /// </summary>
        /// <value>A DataTemplate value.</value>
        public DataTemplate WatermarkTemplate
        {
            get { return (DataTemplate)GetValue(WatermarkTemplateProperty); }
            set { SetValue(WatermarkTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the max length.
        /// </summary>
        /// <value>The max length.</value>
        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the search button tooltip.
        /// </summary>
        /// <value>The search button tooltip.</value>
        public object SearchButtonToolTip
        {
            get { return (object)GetValue(SearchButtonToolTipProperty); }
            set { SetValue(SearchButtonToolTipProperty, value); }
        }

#if SILVERLIGHT
        /// <summary>
        /// Gets a value indicating whether the control has focus.
        /// </summary>
        /// <value>A value indicating whether the control has focus.</value>
        public bool IsFocused
        {
            get { return this.hasFocus; }
        }
#else
        /// <summary>
        /// Gets a value indicating whether the control has focus.
        /// </summary>
        /// <value>A value indicating whether the control has focus.</value>
        public new bool IsFocused
        {
            get { return this.hasFocus; }
        }
#endif

        /// <summary>
        /// Gets the parts from the template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.popup = this.GetTemplateChild(EncodableInputBox.ElementPopup) as Popup;
            if (this.popup != null)
            {
                this.popup.Opened += new EventHandler(this.Popup_Opened);
                this.popup.Closed += new EventHandler(this.Popup_Closed);
#if !SILVERLIGHT
                this.popup.AllowsTransparency = true;
                this.popup.StaysOpen = false;
#endif
            }

            this.textBox = (TextBox)this.GetTemplateChild(EncodableInputBox.ElementTextBox);
            if (this.textBox != null)
            {
                this.textBox.SizeChanged += new SizeChangedEventHandler(this.TextBox_SizeChanged);
                this.textBox.TextChanged += new TextChangedEventHandler(this.TextBox_TextChanged);
                this.textBox.GotFocus += new RoutedEventHandler(this.TextBox_GotFocus);
                this.textBox.LostFocus += new RoutedEventHandler(this.TextBox_LostFocus);
#if SILVERLIGHT
                this.textBox.KeyDown += new KeyEventHandler(this.EncodableInputBox_KeyDown);
                this.textBox.TabIndex = 0;
#else
                this.textBox.PreviewKeyDown += new KeyEventHandler(this.EncodableInputBox_KeyDown);
#endif

                if (!string.IsNullOrEmpty(this.Text))
                {
                    this.textBox.Text = this.Text;
                }
            }

            this.encodedButton = (Button)this.GetTemplateChild(EncodableInputBox.ElementEncodedButton);
            if (this.encodedButton != null)
            {
                this.encodedButton.Click += new RoutedEventHandler(this.EncodedButton_Click);
                this.encodedButton.GotFocus += new RoutedEventHandler(this.EncodedButton_GotFocus);

#if SILVERLIGHT
                this.encodedButton.KeyDown += new KeyEventHandler(this.EncodableInputBox_KeyDown);
#else
                this.encodedButton.PreviewKeyDown += new KeyEventHandler(this.EncodableInputBox_KeyDown);

                // The control draws its own dotted line indicating focus.
                this.encodedButton.FocusVisualStyle = null;
#endif
            }

            this.conceptListBox = (ConceptListBox)this.GetTemplateChild(EncodableInputBox.ElementConceptListBox);
            if (this.conceptListBox != null)
            {
                this.conceptListBox.ItemsChanged += new EventHandler(this.ConceptListBox_ItemsChanged);
                this.conceptListBox.SelectionChanged += new SelectionChangedEventHandler(this.ConceptListBox_SelectionChanged);
                this.conceptListBox.ItemClicked += new MouseButtonEventHandler(this.ConceptListBox_ItemClicked);
                this.conceptListBox.GotFocus += new RoutedEventHandler(this.ConceptListBox_GotFocus);                
                this.conceptListBox.Scroll += new EventHandler(this.ConceptListBox_Scroll);
                this.conceptListBox.IsTabStop = false;
            }

            this.popupBorder = (Border)this.GetTemplateChild(EncodableInputBox.ElementPopupBorder);
            if (this.popupBorder != null)
            {
                this.popupBorder.Width = this.ActualWidth;
                this.popupBorder.SizeChanged += new SizeChangedEventHandler(this.PopupBorder_SizeChanged);
                this.popupBorder.MouseEnter += new MouseEventHandler(this.Popup_MouseEnter);
                this.popupBorder.MouseLeave += new MouseEventHandler(this.Popup_MouseLeave);
            }

            this.flyoutCanvas = (Canvas)this.GetTemplateChild(EncodableInputBox.ElementFlyOutCanvas);

            this.flyoutGrid = (Grid)this.GetTemplateChild(EncodableInputBox.ElementFlyOutGrid);
            if (this.flyoutGrid != null)
            {
                this.flyoutGrid.SizeChanged += new SizeChangedEventHandler(this.FlyOutGrid_SizeChanged);
            }            

            this.searchButton = (Button)this.GetTemplateChild(EncodableInputBox.ElementSearchButton);
            if (this.searchButton != null)
            {
                if (this.IsSearchButtonVisible)
                {
                    this.searchButton.Visibility = Visibility.Visible;
                }
                else
                {
                    this.searchButton.Visibility = Visibility.Collapsed;
                }

#if SILVERLIGHT
                this.searchButton.KeyDown += new KeyEventHandler(this.EncodableInputBox_KeyDown);
                this.searchButton.TabIndex = 1;
#else
                this.searchButton.PreviewKeyDown += new KeyEventHandler(this.EncodableInputBox_KeyDown);
#endif

                this.searchButton.GotFocus += new RoutedEventHandler(this.SearchButton_GotFocus);
                this.searchButton.Click += new RoutedEventHandler(this.SearchButton_Click);
                ToolTipService.SetToolTip(this.searchButton, this.SearchButtonToolTip);
            }

            this.watermarkContentPresenter = (ContentPresenter)this.GetTemplateChild(ElementWatermarkContentPresenter);
            if (this.watermarkContentPresenter != null && !string.IsNullOrEmpty(this.Text))
            {
                this.watermarkContentPresenter.Visibility = Visibility.Collapsed;
            }

            this.flyoutContentPresenter = (ContentPresenter)this.GetTemplateChild(EncodableInputBox.ElementFlyOutContentPresenter);

            this.hidePopupGrid = (Grid)this.GetTemplateChild(EncodableInputBox.ElementHidePopupGrid);
            if (this.hidePopupGrid != null)
            {
                this.hidePopupGrid.MouseLeftButtonDown += new MouseButtonEventHandler(this.HidePopupGrid_MouseLeftButtonDown);
#if !SILVERLIGHT
                this.hidePopupGrid.Visibility = Visibility.Collapsed;
#endif
            }

            this.SetSearchButtonVisibility(this.IsSearchButtonVisible);
            this.SelectItem(this.SelectedItem);
            this.SetIsReadOnly(this.IsReadOnly);
        }

        /// <summary>
        /// Updates the text in the text box.
        /// </summary>
        /// <param name="text">The text to set.</param>
        internal void SetText(string text)
        {
            if (this.textBox != null)
            {
                this.textBox.Text = text;                
            }
        }

        /// <summary>
        /// Updates the search button visibility.
        /// </summary>
        /// <param name="buttonVisible">Whether the button is visible.</param>
        internal void SetSearchButtonVisibility(bool buttonVisible)
        {
            if (this.searchButton != null)
            {
                if (buttonVisible && (this.encodedButton == null || this.encodedButton.Visibility != Visibility.Visible))
                {
                    this.searchButton.Visibility = Visibility.Visible;
                }
                else
                {
                    this.searchButton.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Selects an item.
        /// </summary>
        /// <param name="item">The item to select.</param>
        internal void SelectItem(object item)
        {
            if (item != null)
            {
                this.currentlySelectedItem = item;
                if (this.encodedButton != null)
                {
                    this.encodedButton.Visibility = Visibility.Visible;
                }

                if (this.textBox != null)
                {
                    this.textBox.Visibility = Visibility.Collapsed;
                }

                if (this.searchButton != null)
                {
                    this.searchButton.Visibility = Visibility.Collapsed;
                }

                if (this.popup != null)
                {
                    if (this.popup.IsOpen)
                    {
                        if (this.PopupClosing != null)
                        {
                            this.PopupClosing(this, EventArgs.Empty);
                        }
                    }

                    this.popup.IsOpen = false;
                }

                if (this.watermarkContentPresenter != null)
                {
                    this.watermarkContentPresenter.Visibility = Visibility.Collapsed;
                }

                if (this.hasFocus && this.encodedButton != null && !this.IsReadOnly)
                {
                    this.encodedButton.Focus();
                }
            }
            else
            {
                if (this.textBox != null)
                {
                    this.textBox.Visibility = Visibility.Visible;

                    if (this.hasFocus)
                    {
                        this.textBox.Focus();
                    }
                }

                if (this.encodedButton != null)
                {
                    this.encodedButton.Visibility = Visibility.Collapsed;
                }

                this.SetSearchButtonVisibility(this.IsSearchButtonVisible);
            }

            if (this.SelectionChanged != null)
            {
                this.SelectionChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Sets the control's IsReadOnly state.
        /// </summary>
        /// <param name="readyOnly">Whether the control is read only.</param>
        internal void SetIsReadOnly(bool readyOnly)
        {
            if (this.textBox != null)
            {
                this.textBox.IsEnabled = !readyOnly;
            }

            if (this.searchButton != null)
            {
                this.searchButton.IsEnabled = !readyOnly;
            }
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="dependencyObject">The encodable input box.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void Text_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            EncodableInputBox encodableInputBox = (EncodableInputBox)dependencyObject;

            if (eventArgs.NewValue != null)
            {
                encodableInputBox.SetText(eventArgs.NewValue.ToString());
            }
        }

        /// <summary>
        /// Sets the search button visibility.
        /// </summary>
        /// <param name="dependencyObject">The encodable input box.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void IsSearchButtonVisible_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            EncodableInputBox encodableInputBox = (EncodableInputBox)dependencyObject;
            encodableInputBox.SetSearchButtonVisibility((bool)eventArgs.NewValue);
        }

        /// <summary>
        /// Sets the selected item.
        /// </summary>
        /// <param name="dependencyObject">The encodable input box.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void SelectedItem_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            EncodableInputBox encodableInputBox = (EncodableInputBox)dependencyObject;
            encodableInputBox.SelectItem(eventArgs.NewValue);
        }

        /// <summary>
        /// Sets the is read only UI.
        /// </summary>
        /// <param name="dependencyObject">The encodable input box.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void IsReadOnly_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            EncodableInputBox encodableInputBox = (EncodableInputBox)dependencyObject;
            encodableInputBox.SetIsReadOnly((bool)eventArgs.NewValue);
        }

        /// <summary>
        /// Sets the search button tool tip.
        /// </summary>
        /// <param name="dependencyObject">The encodable input box.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void SearchButtonToolTip_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            EncodableInputBox encodableInputBox = (EncodableInputBox)dependencyObject;
            if (encodableInputBox.searchButton != null)
            {
                ToolTipService.SetToolTip(encodableInputBox.searchButton, eventArgs.NewValue);
            }
        }

        /// <summary>
        /// Shows the text box and search button.
        /// </summary>
        /// <param name="sender">The encoded button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void EncodedButton_Click(object sender, RoutedEventArgs e)
        {            
            if (!this.IsReadOnly)
            {
                this.SelectedItem = null;
            }
            else if (this.popup != null && this.conceptListBox != null)
            {
                if (this.popup.IsOpen)
                {
                    if ((this.IsReadOnly && this.conceptListBox.SelectedItem != null) || !this.IsReadOnly)
                    {
                        if (this.SelectedItem != this.conceptListBox.SelectedItem)
                        {
                            this.SelectedItem = this.conceptListBox.SelectedItem;
                        }
                        else
                        {
                            this.SelectItem(this.conceptListBox.SelectedItem);
                        }
                    }
                }
                else if (this.conceptListBox.Items.Count > 0)
                {
                    if (this.PopupOpening != null)
                    {
                        this.PopupOpening(this, EventArgs.Empty);
                    }

#if SILVERLIGHT
                    this.popup.IsOpen = true;
#else
                    this.showPopupTimer.Start();
#endif
                }
            }
        }

        /// <summary>
        /// Sets focus to the control.
        /// </summary>
        /// <param name="sender">The encoded button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void EncodedButton_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hasFocus = true;
            this.hidePopupTimer.Stop();
        }

        /// <summary>
        /// Gives focus to the control and opens the popup if the control has items.
        /// </summary>
        /// <param name="sender">The search button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void SearchButton_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hasFocus = true;
            if (this.conceptListBox != null && this.conceptListBox.Items.Count > 0 && !string.IsNullOrEmpty(this.textBox.Text.Trim()) && this.encodedButton.Visibility == Visibility.Collapsed)
            {
                this.hidePopupTimer.Stop();

                if (this.PopupOpening != null)
                {
                    this.PopupOpening(this, EventArgs.Empty);
                }

                this.popup.IsOpen = true;
            }
        }

        /// <summary>
        /// Focuses the text box and initiates a search.
        /// </summary>
        /// <param name="sender">The search button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.textBox != null)
            {
                this.textBox.Focus();
            }

            if (this.SearchClicked != null)
            {
                this.SearchClicked(this, e);
            }
        }

        /// <summary>
        /// Hides the popup.
        /// </summary>
        /// <param name="sender">The hide popup timer.</param>
        /// <param name="e">Event Args.</param>
        private void HidePopupTimer_Tick(object sender, EventArgs e)
        {
            this.hidePopupTimer.Stop();            
            if (this.popup != null)
            {
                if (this.popup.IsOpen)
                {
                    if (this.PopupClosing != null)
                    {
                        this.PopupClosing(this, EventArgs.Empty);
                    }
                }

                this.popup.IsOpen = false;
            }
        }

        #region ConceptListBox event handlers
        /// <summary>
        /// Hides the popup when there are no items.
        /// </summary>
        /// <param name="sender">The concept list box.</param>
        /// <param name="e">Event Args.</param>
        private void ConceptListBox_ItemsChanged(object sender, EventArgs e)
        {
            this.currentlySelectedItem = null;
            if (this.conceptListBox.Items.Count == 0)
            {
                if (this.popup.IsOpen)
                {
                    if (this.PopupClosing != null)
                    {
                        this.PopupClosing(this, EventArgs.Empty);
                    }
                }

                this.popup.IsOpen = false;
            }
            else if (this.textBox != null && this.textBox.Visibility == Visibility.Visible && this.hasFocus)
            {                
                this.hidePopupTimer.Stop();
                if (this.PopupOpening != null)
                {
                    this.PopupOpening(this, EventArgs.Empty);
                }

                this.popup.IsOpen = true;
            }
        }

        /// <summary>
        /// Returns focus to the text box.
        /// </summary>
        /// <param name="sender">The list box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void ConceptListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hidePopupTimer.Stop();
#if SILVERLIGHT
            if (this.textBox != null && !this.IsReadOnly)
            {
                this.textBox.Focus();
            }
            else if (this.IsReadOnly && this.encodedButton != null)
            {
                this.encodedButton.Focus();
            }
#endif
        }

        /// <summary>
        /// Re-positions the flyout to the current list box item.
        /// </summary>
        /// <param name="sender">The concept list box.</param>
        /// <param name="e">Event Args.</param>
        private void ConceptListBox_Scroll(object sender, EventArgs e)
        {
            this.PositionFlyOut(this.conceptListBox.GetContainerFromItem(this.conceptListBox.SelectedItem));
        }

        /// <summary>
        /// Selects the clicked item.
        /// </summary>
        /// <param name="sender">The clicked list box item.</param>
        /// <param name="e">Mouse Button Event Args.</param>
        private void ConceptListBox_ItemClicked(object sender, MouseButtonEventArgs e)
        {
            if (this.popup != null && this.popup.IsOpen)
            {
                this.hasFocus = true;
                if (this.SelectedItem == this.conceptListBox.SelectedItem)
                {                    
                    this.SelectItem(this.conceptListBox.SelectedItem);
                }
                else
                {                    
                    this.SelectedItem = this.conceptListBox.SelectedItem;
                }
            }
        }

        /// <summary>
        /// Updates the selection, presents the flyout and raises the selection changed event.
        /// </summary>
        /// <param name="sender">The list box.</param>
        /// <param name="e">Selection Change Event Args.</param>
        private void ConceptListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.conceptListBox.SelectedItem != null)
            {
                this.ShowFlyOut(this.conceptListBox.SelectedItem);
            }
            else if (this.flyoutGrid != null)
            {
                this.flyoutGrid.Visibility = Visibility.Collapsed;
            }

            if (this.textBox != null)
            {
                this.textBox.Focus();
            }
        }
        #endregion

        #region TextBox event handlers
        /// <summary>
        /// Shows the popup if there are items.
        /// </summary>
        /// <param name="sender">The text box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hasFocus = true;
            this.hidePopupTimer.Stop();
            if (this.watermarkContentPresenter != null)
            {
                this.watermarkContentPresenter.Visibility = Visibility.Collapsed;
            }

            if (this.conceptListBox != null && this.conceptListBox.Items.Count > 0 && !string.IsNullOrEmpty(this.textBox.Text.Trim()))
            {
                if (this.PopupOpening != null)
                {
                    this.PopupOpening(this, EventArgs.Empty);
                }
#if SILVERLIGHT
                if (this.rootVisualHasFocus)
                {
                    this.popup.IsOpen = true;
                }
#else
                this.showPopupTimer.Start();
#endif
            }            
        }

        /// <summary>
        /// Adjust the position of the hole in the popup grid.
        /// </summary>
        /// <param name="sender">The text box.</param>
        /// <param name="e">Size Changed Event Args.</param>
        private void TextBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.hidePopupGrid != null)
            {
                this.hidePopupGrid.Margin = new Thickness(0, -e.NewSize.Height, 0, 0);
            }
        }

        /// <summary>
        /// Raises the TextChanged event.
        /// </summary>
        /// <param name="sender">The text box.</param>
        /// <param name="e">Text Changed Event Args.</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox.Text.Trim()))
            {
                if (this.popup.IsOpen)
                {
                    if (this.PopupClosing != null)
                    {
                        this.PopupClosing(this, EventArgs.Empty);
                    }
                }

                this.popup.IsOpen = false;                
            }

            this.Text = this.textBox.Text;

            if (string.IsNullOrEmpty(this.Text) && !this.hasFocus && this.SelectedItem == null && this.watermarkContentPresenter != null)
            {
                this.watermarkContentPresenter.Visibility = Visibility.Visible;
            }
            else if (!string.IsNullOrEmpty(this.Text))
            {
                this.watermarkContentPresenter.Visibility = Visibility.Collapsed;
            }

            if (this.TextChanged != null)
            {
                this.TextChanged(this, e);
            }
        }

        /// <summary>
        /// Shows the popup.
        /// </summary>
        /// <param name="sender">The show popup timer.</param>
        /// <param name="e">Event Args.</param>
        private void ShowPopupTimer_Tick(object sender, EventArgs e)
        {
            this.showPopupTimer.Stop();
            if (this.conceptListBox != null && this.conceptListBox.Items.Count > 0 && ((!string.IsNullOrEmpty(this.textBox.Text.Trim()) && this.SelectedItem == null) || this.IsReadOnly) && this.hasFocus)
            {
                this.hidePopupTimer.Stop();
                this.popup.IsOpen = true;
            }
        }

        /// <summary>
        /// Shows the watermark is the text box is empty.
        /// </summary>
        /// <param name="sender">The text box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.Text) && this.watermarkContentPresenter != null && this.encodedButton.Visibility == Visibility.Collapsed)
            {
                this.watermarkContentPresenter.Visibility = Visibility.Visible;
            }
        }
        #endregion

        /// <summary>
        /// Shows the popup / selects an item / navigates the listbox when a key is pressed.
        /// </summary>
        /// <param name="sender">A control within the encodable input box.</param>
        /// <param name="e">Key Event Args.</param>
        private void EncodableInputBox_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.Key == Key.Enter)
            {
                e.Handled = true;

                if (this.conceptListBox != null && this.popup != null)
                {
                    if (this.popup.IsOpen)
                    {                        
                        if ((this.IsReadOnly && this.conceptListBox.SelectedItem != null) || !this.IsReadOnly)
                        {
                            if (this.SelectedItem != this.conceptListBox.SelectedItem)
                            {
                                this.SelectedItem = this.conceptListBox.SelectedItem;
                            }
                            else
                            {
                                this.SelectItem(this.conceptListBox.SelectedItem);
                            }
                        }
                    }
                    else if (this.SelectedItem != null && !this.IsReadOnly)
                    {
                        this.SelectedItem = null;
                    }
                    else if (this.conceptListBox.Items.Count > 0)
                    {
                        if (this.PopupOpening != null)
                        {
                            this.PopupOpening(this, EventArgs.Empty);
                        }

#if SILVERLIGHT
                        this.popup.IsOpen = true;
#else
                        this.showPopupTimer.Start();
#endif
                    }
                    else if (!this.IsReadOnly)
                    {
                        if (this.EnterPressed != null)
                        {
                            this.EnterPressed(this, e);
                        }
                    }
                }
                else
                {
                    if (this.EnterPressed != null)
                    {
                        this.EnterPressed(this, e);
                    }
                }
            }
            else
            {
                // Handle the list box navigation.
                if (this.conceptListBox != null && this.conceptListBox.Items.Count > 0 && this.popup != null && this.popup.IsOpen)
                {
                    if (e.Key == Key.Up)
                    {
                        if (this.conceptListBox.SelectedIndex <= 0)
                        {
                            this.conceptListBox.SelectedIndex = this.conceptListBox.Items.Count - 1;
                        }
                        else
                        {
                            this.conceptListBox.SelectedIndex--;
                        }

                        e.Handled = true;
                    }
                    else if (e.Key == Key.Down)
                    {
                        if (this.conceptListBox.SelectedIndex == this.conceptListBox.Items.Count - 1)
                        {
                            this.conceptListBox.SelectedIndex = 0;
                        }
                        else
                        {
                            this.conceptListBox.SelectedIndex++;
                        }

                        e.Handled = true;
                    }
                    else if (e.Key == Key.PageUp && this.textBox.SelectionStart == 0)
                    {
                        e.Handled = true;
                        this.conceptListBox.PageUp();
                    }
                    else if (e.Key == Key.PageDown && this.textBox.SelectionStart == this.textBox.Text.Length)
                    {
                        e.Handled = true;
                        this.conceptListBox.PageDown();
                    }
                    else if (e.Key == Key.Home && this.textBox.SelectionStart == 0)
                    {                        
                        this.conceptListBox.PageToBeginning();
                    }
                    else if (e.Key == Key.End && this.textBox.SelectionStart == this.textBox.Text.Length)
                    {                        
                        this.conceptListBox.PageToEnd();
                    }

                    if (this.popup != null && this.popup.IsOpen)
                    {
                        this.conceptListBox.ScrollIntoView(this.conceptListBox.SelectedItem);
                    }
                }
            }
        }

        /// <summary>
        /// Passes focus to a sub control.        
        /// </summary>
        /// <param name="sender">The Encodable input box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void EncodableInputBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.IsTabStop = false;
            if (this.searchButton != null && e.OriginalSource == this.searchButton)
            {
                this.searchButton.Focus();
            }
            else if (this.encodedButton != null && !this.hasFocus && (e.OriginalSource == this.encodedButton || this.encodedButton.Visibility == Visibility.Visible))
            {
                this.encodedButton.Focus();
            }
            else if (this.textBox != null && this.SelectedItem == null && e.OriginalSource == this)
            {
                this.textBox.Focus();
            }
        }
        
        /// <summary>
        /// Hides the popup when the control loses focus.
        /// </summary>
        /// <param name="sender">The encodable input box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void EncodableInputBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.hasFocus = false;
            this.IsTabStop = true;         
            if (this.popup != null && this.popup.IsOpen)
            {
                this.hidePopupTimer.Start();
            }
        }

        /// <summary>
        /// Re-positions the flyout to take into account the new popup size.
        /// </summary>
        /// <param name="sender">The popup border.</param>
        /// <param name="e">Size Changed Event Args.</param>
        private void PopupBorder_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.PositionFlyOut(this.conceptListBox.GetContainerFromItem(this.conceptListBox.SelectedItem));
        }

        /// <summary>
        /// Hides the popup.
        /// </summary>
        /// <param name="sender">The popup canvas.</param>
        /// <param name="e">Mouse Button Event Args.</param>
        private void HidePopupGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.popup != null && this.popup.IsOpen)
            {
                if (this.PopupClosing != null)
                {
                    this.PopupClosing(this, EventArgs.Empty);
                }

                this.popup.IsOpen = false;
            }
        }

        /// <summary>
        /// Re-positions the flyout to take into account the new flyout size.
        /// </summary>
        /// <param name="sender">The flyout grid.</param>
        /// <param name="e">Size Changed Event Args.</param>
        private void FlyOutGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {         
            this.PositionFlyOut(this.conceptListBox.GetContainerFromItem(this.conceptListBox.SelectedItem));
        }

        /// <summary>
        /// Shows the flyout when the popup opens.
        /// </summary>
        /// <param name="sender">The popup control.</param>
        /// <param name="e">Event Args.</param>
        private void Popup_Opened(object sender, EventArgs e)
        {
            this.textBox.Focus();

#if SILVERLIGHT
            if (this.MouseEnterPopup != null)
            {
                this.MouseEnterPopup(this, null);
            }
#endif
            if (this.conceptListBox != null && this.currentlySelectedItem != null && this.conceptListBox.Items.Contains(this.currentlySelectedItem))
            {
                this.conceptListBox.SelectedItem = this.currentlySelectedItem;
            }

            if (this.conceptListBox != null && this.conceptListBox.SelectedItem != null)
            {
                this.ShowFlyOut(this.conceptListBox.SelectedItem);
            }
        }

        /// <summary>
        /// Re-selects and item, if one was previously selected.
        /// </summary>
        /// <param name="sender">The popup.</param>
        /// <param name="e">Event args.</param>
        private void Popup_Closed(object sender, EventArgs e)
        {
            if (this.conceptListBox != null && this.currentlySelectedItem != null)
            {
                if (this.SelectedItem != this.currentlySelectedItem)
                {
                    this.SelectedItem = this.currentlySelectedItem;
                }
            }
        }

        /// <summary>
        /// Raises the popup mouse entered event.
        /// </summary>
        /// <param name="sender">The popup.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void Popup_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.MouseEnterPopup != null)
            {
                this.MouseEnterPopup(this, e);
            }
        }

        /// <summary>
        /// Raises the popup mouse leave event.
        /// </summary>
        /// <param name="sender">The popup.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void Popup_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.MouseLeavePopup != null)
            {
                this.MouseLeavePopup(this, e);
            }
        }

#if !SILVERLIGHT
        /// <summary>
        /// Hides the popup when the main window location changes.
        /// </summary>
        /// <param name="sender">The main application window.</param>
        /// <param name="e">Event Args.</param>
        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            if (this.popup != null)
            {
                this.hidePopupTimer.Start();
            }
        }
#endif

        /// <summary>
        /// Updates the width of the popup.
        /// </summary>
        /// <param name="sender">The encodable input box.</param>
        /// <param name="e">Size Changed Event Args.</param>
        private void EncodableInputBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!this.rootVisualFocusHandlersIntialized)
            {
#if SILVERLIGHT
                // These event are handled to cope with when the Silverlight application loses focus.
                Application.Current.RootVisual.GotFocus += new RoutedEventHandler(this.RootVisual_GotFocus);
                Application.Current.RootVisual.LostFocus += new RoutedEventHandler(this.RootVisual_LostFocus);

                this.rootVisualFocusTimer = new DispatcherTimer();
                this.rootVisualFocusTimer.Tick += new EventHandler(this.RootVisualFocusTimer_Tick);
                this.rootVisualFocusTimer.Interval = TimeSpan.FromMilliseconds(0);
#else
            if (Application.Current != null && Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.LocationChanged += new EventHandler(this.MainWindow_LocationChanged);
            }
#endif
                this.rootVisualFocusHandlersIntialized = true;
            }

            if (this.popupBorder != null)
            {
                this.popupBorder.Width = e.NewSize.Width;
            }
        }

        /// <summary>
        /// Raises the popup mouse enter / leave events if the popup is open.
        /// </summary>
        /// <param name="sender">The encodable input box control.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void EncodableInputBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.popup != null && this.popupBorder != null && this.popup.IsOpen)
            {
                Point pos = e.GetPosition(this);
                Point posInPopup = e.GetPosition(this.popup);
                if ((pos.X >= 0 && pos.X <= this.ActualWidth && pos.Y >= 0 && pos.Y <= this.ActualHeight) ||
                    (posInPopup.X >= 0 && posInPopup.X <= this.popupBorder.ActualWidth && posInPopup.Y >= 0 && posInPopup.Y <= this.popupBorder.ActualHeight))
                {
                    if (this.MouseEnterPopup != null)
                    {
                        this.MouseEnterPopup(this, e);
                    }
                }
                else
                {
                    if (this.MouseLeavePopup != null)
                    {
                        this.MouseLeavePopup(this, e);
                    }
                }
            }
        }

#if SILVERLIGHT
        /// <summary>
        /// Handles when the Silverlight application loses focus.
        /// </summary>
        /// <param name="sender">The root visual.</param>
        /// <param name="e">Routed Event Args.</param>
        private void RootVisual_LostFocus(object sender, RoutedEventArgs e)
        {
            this.rootVisualFocusTimer.Start();
        }

        /// <summary>
        /// Handles when the Silverlight application gets focus.
        /// </summary>
        /// <param name="sender">The root visual.</param>
        /// <param name="e">Routed Event Args.</param>
        private void RootVisual_GotFocus(object sender, RoutedEventArgs e)
        {
            this.rootVisualFocusTimer.Stop();
            this.rootVisualHasFocus = true;

            if (this.conceptListBox != null && this.conceptListBox.Items.Count > 0 && !string.IsNullOrEmpty(this.textBox.Text.Trim()) && this.hasFocus)
            {
                if (this.PopupOpening != null)
                {
                    this.PopupOpening(this, EventArgs.Empty);
                }

                this.showPopupTimer.Start();
            }
        }

        /// <summary>
        /// Handles to root visual focus timer tick.
        /// </summary>
        /// <param name="sender">The root visual focus timer.</param>
        /// <param name="e">Event Args.</param>
        private void RootVisualFocusTimer_Tick(object sender, EventArgs e)
        {
            this.rootVisualFocusTimer.Stop();
            this.rootVisualHasFocus = false;
        }
#endif

        /// <summary>
        /// Shows a flyout with the the selected item data.
        /// </summary>
        /// <param name="term">The term to show the flyout for.</param>
        private void ShowFlyOut(object term)
        {
            if (this.flyoutGrid != null && term != null)
            {
                this.flyoutGrid.Visibility = Visibility.Collapsed;

                if (this.flyoutContentPresenter != null)
                {
                    this.flyoutContentPresenter.Content = term;
                }

                if (this.conceptListBox != null)
                {
                    this.PositionFlyOut(this.conceptListBox.GetContainerFromItem(term));
                }
            }
            else
            {
                this.flyoutGrid.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Positions the flyout relative to a list box item.
        /// </summary>
        /// <param name="listBoxItem">The list box item.</param>
        private void PositionFlyOut(ListBoxItem listBoxItem)
        {
            if (listBoxItem != null && VisualTreeHelper.GetParent(listBoxItem) != null && this.popupBorder != null && this.popup.IsOpen)
            {
                GeneralTransform transform = listBoxItem.TransformToVisual(this.conceptListBox);
                double y = transform.Transform(new Point(0, 0)).Y;

                if (y + listBoxItem.ActualHeight >= 0 && y <= this.popupBorder.ActualHeight)
                {
                    this.flyoutGrid.Visibility = Visibility.Visible;
                    double top = Math.Max(0, Math.Min(this.popupBorder.ActualHeight - this.flyoutGrid.ActualHeight, y));

                    Canvas.SetTop(this.flyoutGrid, top);
                    
                    if (this.flyoutCanvas != null)
                    {
                        this.flyoutCanvas.Height = top + this.flyoutGrid.ActualHeight;
                    }
                }
                else
                {
                    this.flyoutGrid.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                this.flyoutGrid.Visibility = Visibility.Collapsed;
            }
        }
    }
}
