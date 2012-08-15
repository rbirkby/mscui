//-----------------------------------------------------------------------
// <copyright file="CascadingListBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>18-Aug-2009</date>
// <summary>
//      Cascading ListBox control.
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
    /// Cascading ListBox control.
    /// </summary>
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(CascadingListBoxItem))]
    [TemplatePart(Name = CascadingListBox.ElementSelectedItemButtonName, Type = typeof(Button))]
    [TemplatePart(Name = CascadingListBox.ElementLookBehindButton, Type = typeof(Button))]
    [TemplatePart(Name = CascadingListBox.ElementLookAheadButton, Type = typeof(Button))]
    [TemplateVisualState(Name = "ConfirmedSelectedItemCleared", GroupName = "ConfirmedSelectedItemStates")]
    [TemplateVisualState(Name = "ConfirmedSelectedItemChanged", GroupName = "ConfirmedSelectedItemStates")]
    [TemplateVisualState(Name = "Expanded", GroupName = "ExpandedStates")]
    [TemplateVisualState(Name = "Collapsed", GroupName = "ExpandedStates")]
    public class CascadingListBox : SplitListBox
    {
        #region Depedency Properties
        /// <summary>
        /// The IsExpanded Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(CascadingListBox), new PropertyMetadata(false, new PropertyChangedCallback(IsExpanded_Changed)));

        /// <summary>
        /// The CollapseOnSelection Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CollapseOnSelectionProperty =
            DependencyProperty.Register("CollapseOnSelection", typeof(bool), typeof(CascadingListBox), new PropertyMetadata(true));

        /// <summary>
        /// The ExpandOnDESelection Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ExpandOnDESelectionProperty =
            DependencyProperty.Register("ExpandOnDESelection", typeof(bool), typeof(CascadingListBox), new PropertyMetadata(true));

        /// <summary>
        /// The MinListBoxWidth Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MinListBoxWidthProperty =
            DependencyProperty.Register("MinListBoxWidth", typeof(double), typeof(CascadingListBox), null);

        /// <summary>
        /// The SelectedItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemTemplateProperty =
            DependencyProperty.Register("SelectedItemTemplate", typeof(DataTemplate), typeof(CascadingListBox), new PropertyMetadata(null, new PropertyChangedCallback(SelectedItemTemplate_Changed)));

        /// <summary>
        /// The LookBehindVisibility Dependency Property.
        /// </summary>
        public static readonly DependencyProperty LookBehindVisibilityProperty =
            DependencyProperty.Register("LookBehindVisibility", typeof(Visibility), typeof(CascadingListBox), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// The LookAheadVisibility Dependency Property.
        /// </summary>
        public static readonly DependencyProperty LookAheadVisibilityProperty =
            DependencyProperty.Register("LookAheadVisibility", typeof(Visibility), typeof(CascadingListBox), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// The Message Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(object), typeof(CascadingListBox), null);

        /// <summary>
        /// The MessageTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MessageTemplateProperty =
         DependencyProperty.Register("MessageTemplate", typeof(DataTemplate), typeof(CascadingListBox), null);

        /// <summary>
        /// The MessageVisibility Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MessageVisibilityProperty =
            DependencyProperty.Register("MessageVisibility", typeof(Visibility), typeof(CascadingListBox), new PropertyMetadata(Visibility.Visible));
        #endregion

        /// <summary>
        /// The SelectedItemButton Element Name.
        /// </summary>
        private const string ElementSelectedItemButtonName = "SelectedItemButton";

        /// <summary>
        /// The LookBehindButton ElementName.
        /// </summary>
        private const string ElementLookBehindButton = "LookBehindButton";

        /// <summary>
        /// The LookAheadButton ElementName.
        /// </summary>
        private const string ElementLookAheadButton = "LookAheadButton";

        /// <summary>
        /// Stores the cascading list box item containers by item.
        /// </summary>
        private Dictionary<object, CascadingListBoxItem> containersByItem = new Dictionary<object, CascadingListBoxItem>();

        /// <summary>
        /// Stores the selected item button.
        /// </summary>
        private Button selectedItemButton;

        /// <summary>
        /// Stores if the control should focus the selected item.
        /// </summary>
        private bool focusSelectedItem;

#if !SILVERLIGHT
        /// <summary>
        /// Stores the last focussed element for WPF's focus management.
        /// </summary>
        private object lastFocusedElement;
#endif

        /// <summary>
        /// Stores if the control has focus.
        /// </summary>
        private bool isFocused;

        /// <summary>
        /// Stores the currently focused item.
        /// </summary>
        private object focusedItem;

        /// <summary>
        /// CascadingListBox constructor.
        /// </summary>
        public CascadingListBox()
        {            
            this.DefaultStyleKey = typeof(CascadingListBox);
            this.SelectionChanged += new SelectionChangedEventHandler(this.CascadingListBox_SelectionChanged);
            this.ConfirmedSelectedItemChanged += new EventHandler(this.CascadingListBox_ConfirmedSelectedItemChanged);
            this.ItemsChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(this.CascadingListBox_ItemsChanged);
            this.GotFocus += new RoutedEventHandler(this.CascadingListBox_GotFocus);
            this.LostFocus += new RoutedEventHandler(this.CascadingListBox_LostFocus);
            this.SizeChanged += new SizeChangedEventHandler(this.CascadingListBox_SizeChanged);
        }

        /// <summary>
        /// The Expanded Event.
        /// </summary>
        public event EventHandler Expanded;

        /// <summary>
        /// The Collpsed Event.
        /// </summary>
        public event EventHandler Collapsed;

        /// <summary>
        /// Gets or sets a value indicating whether the list box is expanded.
        /// </summary>
        /// <value>The is expanded value.</value>
        public bool IsExpanded
        {
            get 
            { 
                return (bool)GetValue(IsExpandedProperty); 
            }

            set 
            {
                if (!value && (this.ConfirmedSelectedItem != null && this.ConfirmedSelectedItem != this.OtherItem))
                {
                    SetValue(IsExpandedProperty, value);
                }
                else if (value)
                {
                    SetValue(IsExpandedProperty, value);                    
                }
            }
        }

        /// <summary>
        /// Gets or sets the min list box width.
        /// </summary>
        /// <value>The min list box width value.</value>
        public double MinListBoxWidth
        {
            get { return (double)GetValue(MinListBoxWidthProperty); }
            set { SetValue(MinListBoxWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Selected Item Template.
        /// </summary>
        /// <value>The selected item template value.</value>
        public DataTemplate SelectedItemTemplate
        {
            get { return (DataTemplate)GetValue(SelectedItemTemplateProperty); }
            set { SetValue(SelectedItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the list box should collapse on selection.
        /// </summary>
        /// <value>The collapse on selection value.</value>
        public bool CollapseOnSelection
        {
            get { return (bool)GetValue(CollapseOnSelectionProperty); }
            set { SetValue(CollapseOnSelectionProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control should expand when the selected item is null.
        /// </summary>
        /// <value>The expand on de-selection value.</value>
        public bool ExpandOnDESelection
        {
            get { return (bool)GetValue(ExpandOnDESelectionProperty); }
            set { SetValue(ExpandOnDESelectionProperty, value); }
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
        /// Gets the selected item container.
        /// </summary>
        /// <value>The selected item container value.</value>
        public FrameworkElement SelectedItemContainer
        {
            get
            {
                if (this.ConfirmedSelectedItem != null)
                {
                    if (this.IsExpanded && this.containersByItem.ContainsKey(this.ConfirmedSelectedItem))
                    {
                        return this.containersByItem[this.ConfirmedSelectedItem].ContentBorder;
                    }
                    else
                    {
                        return this.selectedItemButton;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the look behind visibility.
        /// </summary>
        /// <value>The look behind visibility value.</value>
        public Visibility LookBehindVisibility
        {
            get { return (Visibility)GetValue(LookBehindVisibilityProperty); }
        }

        /// <summary>
        /// Gets the lookahead visibility.
        /// </summary>
        /// <value>The look ahead visibility value.</value>
        public Visibility LookAheadVisibility
        {
            get { return (Visibility)GetValue(LookAheadVisibilityProperty); }
        }

        /// <summary>
        /// Gets or sets the control message (when no items are listed).
        /// </summary>
        /// <value>The control message value.</value>
        public object Message
        {
            get { return (object)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        /// <summary>
        /// Gets or sets the control message template (when no items are listed).
        /// </summary>
        /// <value>The control message template value.</value>
        public DataTemplate MessageTemplate
        {
            get { return (DataTemplate)GetValue(MessageTemplateProperty); }
            set { SetValue(MessageTemplateProperty, value); }
        }

        /// <summary>
        /// Gets the message visibility.
        /// </summary>
        /// <value>The message visibility value.</value>
        public Visibility MessageVisibility
        {
            get { return (Visibility)GetValue(MessageVisibilityProperty); }
        }

        /// <summary>
        /// Updates the expanded visual state.
        /// </summary>
        public void UpdateExpandedState()
        {
            if (this.IsExpanded)
            {
                this.SelectedItem = this.ConfirmedSelectedItem;
                if (this.ConfirmedSelectedItem != null)
                {
                    this.ScrollIntoView(this.ConfirmedSelectedItem);
                }

                this.GoToVisualState("Expanded", true);

                if (this.selectedItemButton != null)
                {
                    VisualStateManager.GoToState(this.selectedItemButton, "Normal", false);
                }
            }
            else
            {
                this.focusSelectedItem = true;
                this.GoToVisualState("Collapsed", true);
                if (this.Collapsed != null)
                {
                    this.Collapsed(this, EventArgs.Empty);
                }

                if (this.IsFocused && this.selectedItemButton != null)
                {
                    FocusHelper.FocusControl(this.selectedItemButton);
                }
            }
        }

        /// <summary>
        /// Gets the parts from the template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.selectedItemButton = this.GetTemplateChild(CascadingListBox.ElementSelectedItemButtonName) as Button;
            if (this.selectedItemButton != null)
            {
                this.selectedItemButton.Click += new RoutedEventHandler(this.SelectedItemButton_Click);
                this.UpdateSelectedItemTemplate();
            }

            Button lookBehindButton = this.GetTemplateChild(CascadingListBox.ElementLookBehindButton) as Button;
            if (lookBehindButton != null)
            {
                lookBehindButton.Click += new RoutedEventHandler(this.LookAheadBehindButton_Click);
            }

            Button lookAheadButton = this.GetTemplateChild(CascadingListBox.ElementLookAheadButton) as Button;
            if (lookAheadButton != null)
            {
                lookAheadButton.Click += new RoutedEventHandler(this.LookAheadBehindButton_Click);
            }

            if (this.ConfirmedSelectedItem == null && this.Items.Count > 0)
            {
                this.IsExpanded = true;
            }

            this.UpdateExpandedState();
        }

        /// <summary>
        /// Creates an automation peer.
        /// </summary>
        /// <returns>The automation peer.</returns>
        protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
        {
            return new CascadingListBoxAutomationPeer(this);
        }

        /// <summary>
        /// Checks if the item is a list box item.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>Returns true if the item is a list box item.</returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is CascadingListBoxItem);
        }

        /// <summary>
        /// Creates a cascading list box item.
        /// </summary>
        /// <returns>A cascading list box item.</returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CascadingListBoxItem();
        }

        /// <summary>
        /// Prepares the container.
        /// </summary>
        /// <param name="element">The list box item.</param>
        /// <param name="item">The item object.</param>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            CascadingListBoxItem cascadingListBoxItem = element as CascadingListBoxItem;
            if (cascadingListBoxItem != null)
            {
                cascadingListBoxItem.GotFocus += new RoutedEventHandler(this.CascadingListBoxItem_GotFocus);
                cascadingListBoxItem.LostFocus += new RoutedEventHandler(this.CascadingListBoxItem_LostFocus);
                cascadingListBoxItem.ItemSelected += new EventHandler<SelectedEventArgs>(this.CascadingListBoxItem_Selected);

#if !SILVERLIGHT
                cascadingListBoxItem.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.CascadingListBoxItem_PreviewMouseLeftButtonDown);
#endif

                if (!this.containersByItem.ContainsKey(item))
                {
                    this.containersByItem.Add(item, cascadingListBoxItem);
                }
                else
                {
                    this.containersByItem[item] = cascadingListBoxItem;
                }

#if !SILVERLIGHT
                if (this.IsExpanded && this.IsFocused && (this.ConfirmedSelectedItem == item || this.SelectedItem == item))
                {
                    cascadingListBoxItem.Focus();
                }
#endif
                if (item == this.ConfirmedSelectedItem)
                {
                    cascadingListBoxItem.IsSelectedValue = true;
                }
            }
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
        /// Handles the keyboard by blocking navigation when the list box is collpsed.
        /// </summary>
        /// <param name="e">Key Event Args.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!this.IsExpanded)
            {
                switch (e.Key)
                {
                    case Key.Up:
                    case Key.Down:
                        e.Handled = true;
                        this.IsExpanded = true;
                        this.Focus();
                        break;
                    case Key.PageUp:
                    case Key.PageDown:
                    case Key.Home:
                    case Key.End:
                        e.Handled = true;
                        break;
                    default:
                        base.OnKeyDown(e);
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {                    
                    default:
                        base.OnKeyDown(e);
                        break;
                }
            }
        }

        /// <summary>
        /// Overrides scroll and updates look ahead and look behind borders.
        /// </summary>
        /// <param name="e">Event Args.</param>
        protected override void OnScroll(EventArgs e)
        {
            base.OnScroll(e);
            this.UpdateLookAheadBehindBorders();
        }

        /// <summary>
        /// Updates the state of the list box.
        /// </summary>
        /// <param name="obj">The cascading list box.</param>
        /// <param name="args">Dependency Property Changed Args.</param>
        private static void IsExpanded_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            CascadingListBox cascadingListBox = obj as CascadingListBox;
            cascadingListBox.UpdateExpandedState();
            cascadingListBox.UpdateLookAheadBehindBorders();

            if ((bool)args.NewValue)
            {
                if (cascadingListBox.Expanded != null)
                {
                    cascadingListBox.Expanded(cascadingListBox, EventArgs.Empty);
                }
            }
            else
            {
                if (cascadingListBox.Collapsed != null)
                {
                    cascadingListBox.Collapsed(cascadingListBox, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Updates the selected item template.
        /// </summary>
        /// <param name="obj">The cascading list box.</param>
        /// <param name="args">Dependency Property Changed Args.</param>
        private static void SelectedItemTemplate_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            CascadingListBox cascadingListBox = obj as CascadingListBox;
            cascadingListBox.UpdateSelectedItemTemplate();
        }

#if !SILVERLIGHT
        /// <summary>
        /// Sets the selected item to the clicked item.
        /// </summary>
        /// <param name="sender">The cascading list box item.</param>
        /// <param name="e">Mouse Button Event Args.</param>
        private void CascadingListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CascadingListBoxItem cascadingListBoxItem = sender as CascadingListBoxItem;
            if (cascadingListBoxItem != null)
            {
                this.SelectedItem = cascadingListBoxItem.Content;
            }
        }
#endif

        /// <summary>
        /// Moves the selected item into view.
        /// </summary>
        /// <param name="sender">A look behind / ahead button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void LookAheadBehindButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.ConfirmedSelectedItem != null)
            {
                if (this.SelectedItem != this.ConfirmedSelectedItem)
                {
                    this.SelectedItem = this.ConfirmedSelectedItem;
                }
                else
                {
                    this.ScrollIntoView(this.ConfirmedSelectedItem);
                }
            }
        }

        /// <summary>
        /// Collapses the list, if behaviour is not overriden.
        /// </summary>
        /// <param name="sender">The cascading list box item.</param>
        /// <param name="e">Selected Event Args.</param>
        private void CascadingListBoxItem_Selected(object sender, SelectedEventArgs e)
        {
            if (this.CollapseOnSelection && !e.Handled)
            {
                this.IsExpanded = false;
            }

            FocusHelper.FocusControl(this);

            if (e.SelectedItem != this.OtherItem)
            {
                this.RaiseItemSelected();
            }
        }

        /// <summary>
        /// Sets the flag to focus selected item to true.
        /// </summary>
        /// <param name="sender">The cascading list box.</param>
        /// <param name="e">Notify Collection Changed Event Args.</param>
        private void CascadingListBox_ItemsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SetValue(CascadingListBox.MessageVisibilityProperty, this.Items.Count > 0 ? Visibility.Collapsed : Visibility.Visible);
            this.focusSelectedItem = true;
        }

        /// <summary>
        /// Sets the currently focused item to null.
        /// </summary>
        /// <param name="sender">The cascading list box item.</param>
        /// <param name="e">Routed Event Args.</param>
        private void CascadingListBoxItem_LostFocus(object sender, RoutedEventArgs e)
        {
            this.focusedItem = null;
        }

        /// <summary>
        /// Updates the currently focused item.
        /// </summary>
        /// <param name="sender">The cascading list box item.</param>
        /// <param name="e">Routed Event Args.</param>
        private void CascadingListBoxItem_GotFocus(object sender, RoutedEventArgs e)
        {
            CascadingListBoxItem cascadingListBoxItem = sender as CascadingListBoxItem;
            if (cascadingListBoxItem != null)
            {
                this.focusedItem = cascadingListBoxItem.Content;
            }
        }

        /// <summary>
        /// Updates the state of the listbox.
        /// </summary>
        /// <param name="sender">The cascading list box.</param>
        /// <param name="e">Selection Changed Event Args.</param>
        private void CascadingListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
#if !SILVERLIGHT
            if (this.SelectedItem != null)
            {
                this.ScrollIntoView(this.SelectedItem);
                if (this.containersByItem.ContainsKey(this.SelectedItem))
                {
                    if (this.isFocused)
                    {
                        this.containersByItem[this.SelectedItem].Focus();
                    }
                    else
                    {
                        this.lastFocusedElement = this.containersByItem[this.SelectedItem];
                    }
                }
            }
#endif
        }

        /// <summary>
        /// Updates the control for the new selected value.
        /// </summary>
        /// <param name="sender">The CascadingListBox.</param>
        /// <param name="e">Event Args.</param>
        private void CascadingListBox_ConfirmedSelectedItemChanged(object sender, EventArgs e)
        {
            this.SelectedItem = this.ConfirmedSelectedItem;
            this.UpdateSelectedItemTemplate();
            this.UpdateLookAheadBehindBorders();

            if (this.ConfirmedSelectedItem != null && this.containersByItem.ContainsKey(this.ConfirmedSelectedItem))
            {
                this.containersByItem[this.ConfirmedSelectedItem].IsSelectedValue = true;
            }

            if (this.ConfirmedSelectedItem != null && this.ConfirmedSelectedItem != this.OtherItem)
            {
                this.GoToVisualState("ItemSelected", true);
            }
            else
            {
                if (this.ExpandOnDESelection)
                {
                    this.IsExpanded = true;
                }

                this.GoToVisualState("ItemUnselected", true);
            }
        }

        /// <summary>
        /// Moves focus to the correct control.
        /// </summary>
        /// <param name="sender">The cascading list box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void CascadingListBox_GotFocus(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_LostFocus);
            this.isFocused = true;            
#if !SILVERLIGHT
            if (e.OriginalSource == this || e.OriginalSource == this.selectedItemButton || e.OriginalSource is CascadingListBoxItem)
            {
                this.IsTabStop = false;
            }
#endif            
#if !SILVERLIGHT
            if (this.lastFocusedElement != e.OriginalSource && this.SelectedItem == null || (!this.IsExpanded && e.OriginalSource is CascadingListBoxItem))
            {
                this.lastFocusedElement = e.OriginalSource;
            }

            if (!this.IsExpanded && (e.OriginalSource is CascadingListBoxItem) && this.lastFocusedElement == e.OriginalSource && this.selectedItemButton != null)
            {
                FocusHelper.FocusControl(this.selectedItemButton);
            }
            else
            {
                this.lastFocusedElement = e.OriginalSource;
            }
#endif

            if (this.focusSelectedItem && this.IsExpanded && this.ConfirmedSelectedItem != null && this.containersByItem.ContainsKey(this.ConfirmedSelectedItem))
            {
                this.containersByItem[this.ConfirmedSelectedItem].Focus();
                this.focusSelectedItem = false;
            }
            else if (this.IsExpanded && this.SelectedItem != null && this.containersByItem.ContainsKey(this.SelectedItem))
            {
                this.containersByItem[this.SelectedItem].Focus();
            }
            else if (this.IsExpanded && (this.SelectedItem == null || this.SelectedItem == this.OtherItem) && this.Items.Count > 0 && (!this.Items.Contains(this.OtherItem) || this.SelectedIndex != this.Items.IndexOf(this.OtherItem)))
            {
                this.SelectedItem = this.Items[0];

                if (this.containersByItem.ContainsKey(this.Items[0]))
                {
                    this.containersByItem[this.Items[0]].Focus();
                }
            }
            else if (!this.IsExpanded && this.selectedItemButton != null && this.ConfirmedSelectedItem != null && e.OriginalSource != this.selectedItemButton)
            {
                this.selectedItemButton.Focus();
            }
        }

        /// <summary>
        /// Handles when the control loses focus.
        /// </summary>
        /// <param name="sender">The cascading list box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void CascadingListBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.isFocused = false;
#if !SILVERLIGHT
            this.IsTabStop = true;
#endif            
            CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_LostFocus);
            CompositionTarget.Rendering += new EventHandler(this.CompositionTargetRendering_LostFocus);
        }

        /// <summary>
        /// Resets the selected item.
        /// </summary>
        /// <param name="sender">The composition target.</param>
        /// <param name="e">Event Args.</param>
        private void CompositionTargetRendering_LostFocus(object sender, EventArgs e)
        {
            CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_LostFocus);
            this.SelectedItem = this.ConfirmedSelectedItem;
            if (this.ConfirmedSelectedItem != null)
            {
                this.ScrollIntoView(this.ConfirmedSelectedItem);
            }
        }

        /// <summary>
        /// Updates the look ahead / look behind borders.
        /// </summary>
        /// <param name="sender">The Cascading List Box.</param>
        /// <param name="e">Size Changed Event Args.</param>
        private void CascadingListBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateLookAheadBehindBorders();
        }

        /// <summary>
        /// Expands the list box.
        /// </summary>
        /// <param name="sender">The Selected Item Button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void SelectedItemButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsExpanded = true;

            if (this.IsExpanded)
            {
                if (this.ConfirmedSelectedItem != null && this.containersByItem.ContainsKey(this.ConfirmedSelectedItem))
                {
                    this.containersByItem[this.ConfirmedSelectedItem].Focus();
                }
                else if (this.Items.Count > 0 && this.containersByItem.ContainsKey(this.Items[0]))
                {
                    this.containersByItem[this.Items[0]].Focus();
                }
            }
        }

        /// <summary>
        /// Updates the selected item template.
        /// </summary>
        private void UpdateSelectedItemTemplate()
        {
            if (this.selectedItemButton != null)
            {
                if (this.ConfirmedSelectedItem != null)
                {
                    if ((this.ConfirmedSelectedItem == this.CustomValueItem || this.ConfirmedSelectedItem.Equals(this.CustomValueItem)) && this.CustomValueItemTemplate != null)
                    {
                        this.selectedItemButton.ContentTemplate = this.CustomValueItemTemplate;
                    }
                    else if ((this.ConfirmedSelectedItem == this.OtherItem || this.ConfirmedSelectedItem.Equals(this.OtherItem)) && this.OtherItemTemplate != null)
                    {
                        this.selectedItemButton.ContentTemplate = this.OtherItemTemplate;
                    }
                    else if (this.SelectedItemTemplate != null)
                    {
                        this.selectedItemButton.ContentTemplate = this.SelectedItemTemplate;
                    }
                    else
                    {
                        this.selectedItemButton.ContentTemplate = this.ItemTemplate;
                    }
                }
                else
                {
                    this.selectedItemButton.ContentTemplate = null;
                }
            }
        }

        /// <summary>
        /// Goes to a visual state.
        /// </summary>
        /// <param name="state">The visual state.</param>
        /// <param name="useTransitions">Whether to use transitions.</param>
        private void GoToVisualState(string state, bool useTransitions)
        {
            VisualStateManager.GoToState(this, state, useTransitions);
        }

        /// <summary>
        /// Updates the look ahead / look behind borders.
        /// </summary>
        private void UpdateLookAheadBehindBorders()
        {
            CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_LookAheadBehindBorders);
            CompositionTarget.Rendering += new EventHandler(this.CompositionTargetRendering_LookAheadBehindBorders);
        }

        /// <summary>
        /// Updates the look ahead / look behind after a render pass.
        /// </summary>
        /// <param name="sender">The composition target.</param>
        /// <param name="e">Event Args.</param>
        private void CompositionTargetRendering_LookAheadBehindBorders(object sender, EventArgs e)
        {
            CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_LookAheadBehindBorders);
            if (this.IsExpanded && this.SelectedItemContainer != null)
            {
                double selectedItemYPos = this.SelectedItemContainer.TransformToVisual(this).Transform(new Point(0, 0)).Y;
                if (selectedItemYPos + (this.SelectedItemContainer.ActualHeight / 2) < 0)
                {
                    SetValue(CascadingListBox.LookBehindVisibilityProperty, Visibility.Visible);
                    SetValue(CascadingListBox.LookAheadVisibilityProperty, Visibility.Collapsed);
                }
                else if (selectedItemYPos + (this.SelectedItemContainer.ActualHeight / 2) > this.ActualHeight)
                {
                    SetValue(CascadingListBox.LookAheadVisibilityProperty, Visibility.Visible);
                    SetValue(CascadingListBox.LookBehindVisibilityProperty, Visibility.Collapsed);
                }
                else
                {
                    SetValue(CascadingListBox.LookBehindVisibilityProperty, Visibility.Collapsed);
                    SetValue(CascadingListBox.LookAheadVisibilityProperty, Visibility.Collapsed);
                }
            }
            else
            {
                SetValue(CascadingListBox.LookBehindVisibilityProperty, Visibility.Collapsed);
                SetValue(CascadingListBox.LookAheadVisibilityProperty, Visibility.Collapsed);
            }
        }
    }
}
