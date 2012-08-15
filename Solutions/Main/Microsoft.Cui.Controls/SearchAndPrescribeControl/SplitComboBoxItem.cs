//-----------------------------------------------------------------------
// <copyright file="SplitComboBoxItem.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      A split combo box item.
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
    using System.Windows.Automation.Peers;

    /// <summary>
    /// A split combo box item.
    /// </summary>
    [TemplateVisualState(Name = "UnconfirmedSelectedItem", GroupName = "ConfirmedSelectedItemStates")]
    [TemplateVisualState(Name = "ConfirmedSelectedItem", GroupName = "ConfirmedSelectedItemStates")]
    public class SplitComboBoxItem : ComboBoxItem, ISplitItemsControlItem
    {
        /// <summary>
        /// The Header Dependency Property.
        /// </summary>
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(object), typeof(SplitComboBoxItem), new PropertyMetadata(null, new PropertyChangedCallback(Header_Changed)));

        /// <summary>
        /// The HeaderTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(SplitComboBoxItem), null);

        /// <summary>
        /// The ComputedHeaderVisibility Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ComputedHeaderVisibilityProperty =
            DependencyProperty.Register("ComputedHeaderVisibility", typeof(Visibility), typeof(SplitComboBoxItem), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// The SplitBorderThickness Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SplitBorderThicknessProperty =
            DependencyProperty.Register("SplitBorderThickness", typeof(Thickness), typeof(SplitComboBoxItem), null);

        /// <summary>
        /// The SplitBorderBrush Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SplitBorderBrushProperty =
           DependencyProperty.Register("SplitBorderBrush", typeof(Brush), typeof(SplitComboBoxItem), null);

        /// <summary>
        /// The IsSelectedValue Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsSelectedValueProperty =
            DependencyProperty.Register("IsSelectedValue", typeof(bool), typeof(SplitComboBoxItem), new PropertyMetadata(false, new PropertyChangedCallback(IsSelectedValue_Changed)));

        /// <summary>
        /// The ShortcutKeyText Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShortcutKeyTextProperty =
            DependencyProperty.Register("ShortcutKeyText", typeof(string), typeof(SplitComboBoxItem), new PropertyMetadata(string.Empty));

        /// <summary>
        /// SplitComboBoxItem constructor.
        /// </summary>
        public SplitComboBoxItem()
        {
            this.DefaultStyleKey = typeof(SplitComboBoxItem);
            this.GotFocus += new RoutedEventHandler(this.SplitComboBoxItem_GotFocus);

#if !SILVERLIGHT
            this.FocusVisualStyle = null;
#endif
        }

        /// <summary>
        /// The ItemSelected Event.
        /// </summary>
        public event EventHandler<SelectedEventArgs> ItemSelected;

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header value.</value>
        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the header template.
        /// </summary>
        /// <value>The HeaderTemplate value.</value>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        /// <summary>
        /// Gets the computed header visibility.
        /// </summary>
        /// <value>The computed header visibility value.</value>
        public Visibility ComputedHeaderVisibility
        {
            get { return (Visibility)GetValue(ComputedHeaderVisibilityProperty); }
        }

        /// <summary>
        /// Gets or sets the split border thickness.
        /// </summary>
        /// <value>The split border thickness value.</value>
        public Thickness SplitBorderThickness
        {
            get { return (Thickness)GetValue(SplitBorderThicknessProperty); }
            set { SetValue(SplitBorderThicknessProperty, value); }
        }

        /// <summary>
        /// Gets or sets the split border brush.
        /// </summary>
        /// <value>The split border brush value.</value>
        public Brush SplitBorderBrush
        {
            get { return (Brush)GetValue(SplitBorderBrushProperty); }
            set { SetValue(SplitBorderBrushProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this is the selected value.
        /// </summary>
        /// <value>The is selected value.</value>
        public bool IsSelectedValue
        {
            get { return (bool)GetValue(IsSelectedValueProperty); }
            set { SetValue(IsSelectedValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets the shortcut key text.
        /// </summary>
        /// <value>The shortcut key text value.</value>
        public string ShortcutKeyText
        {
            get { return (string)GetValue(ShortcutKeyTextProperty); }
            set { SetValue(ShortcutKeyTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the parent split combo box.
        /// </summary>
        /// <value>The parent split combo box.</value>
        public SplitComboBox ParentSplitComboBox
        {
            get;
            set;
        }

        /// <summary>
        /// Selects the item.
        /// </summary>
        public void SelectItem()
        {
            this.IsSelectedValue = true;
            if (this.ItemSelected != null)
            {
                this.ItemSelected(this, new SelectedEventArgs(this.DataContext));
            }
        }

        /// <summary>
        /// Updates the visual state of the control.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UpdateVisualState(false);
        }

        /// <summary>
        /// Gets the bounding rectangle relative to the split combo box parent.
        /// </summary>
        /// <returns>The bounding rectangle relative to the split combo box.</returns>
        internal Rect GetBoundingRectRelativeToParent()
        {
            if (this.ParentSplitComboBox != null)
            {
                try
                {
                    Point position = this.TransformToVisual(this.ParentSplitComboBox).Transform(new Point(0, 0));
                    return new Rect(position.X, position.Y, this.ActualWidth, this.ActualHeight);
                }
                catch (ArgumentException)
                {
                    return new Rect(0, 0, 0, 0);
                }
            }

            return new Rect(0, 0, 0, 0);
        }

        /// <summary>
        /// Creates an automation peer.
        /// </summary>
        /// <returns>The automation peer.</returns>
        protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
        {
#if SILVERLIGHT
            return new SplitComboBoxItemAutomationPeer(this, this.ParentSplitComboBox != null ? SelectorAutomationPeer.FromElement(this.ParentSplitComboBox) as SelectorAutomationPeer : null);
#else
            return new SplitComboBoxItemAutomationPeer(this);
#endif
        }

        /// <summary>
        /// Raises the selected event.
        /// </summary>
        /// <param name="e">Mouse Button Event Args.</param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            this.Focus();
            this.SelectItem();
        }

        /// <summary>
        /// Handles keyboard input.
        /// </summary>
        /// <param name="e">Key Event Args.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {            
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                this.SelectItem();
                e.Handled = true;
            }

            base.OnKeyDown(e);
        }

        /// <summary>
        /// Prevents WPF selecting the item on hover.
        /// </summary>
        /// <param name="e">Mouse Event Args.</param>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
#if SILVERLIGHT
            base.OnMouseEnter(e);
#endif
        }

        /// <summary>
        /// Updates the computed header visibility.
        /// </summary>
        /// <param name="obj">The headered list box item.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void Header_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SplitComboBoxItem splitComboBoxItem = obj as SplitComboBoxItem;
            if (args.NewValue != null)
            {
                splitComboBoxItem.SetValue(SplitComboBoxItem.ComputedHeaderVisibilityProperty, Visibility.Visible);
            }
            else
            {
                splitComboBoxItem.SetValue(SplitComboBoxItem.ComputedHeaderVisibilityProperty, Visibility.Collapsed);
            }
        }

        /// <summary>
        /// Updates the visual state of the control.
        /// </summary>
        /// <param name="obj">The CascadingListBoxItem.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void IsSelectedValue_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SplitComboBoxItem splitComboBoxItem = obj as SplitComboBoxItem;
            splitComboBoxItem.UpdateVisualState(true);
        }

        /// <summary>
        /// Updates the visual state.
        /// </summary>
        /// <param name="useTransitions">Whether to use transitions.</param>
        private void UpdateVisualState(bool useTransitions)
        {
            if (this.IsSelectedValue)
            {
                VisualStateManager.GoToState(this, "ConfirmedSelectedItem", useTransitions);
                Canvas.SetZIndex(this, 1);
            }
            else
            {
                VisualStateManager.GoToState(this, "UnconfirmedSelectedItem", useTransitions);
                Canvas.SetZIndex(this, 0);
            }
        }

        /// <summary>
        /// Selected the item when it gets focus.
        /// </summary>
        /// <param name="sender">The split combo box item.</param>
        /// <param name="e">Routed Event Args.</param>
        private void SplitComboBoxItem_GotFocus(object sender, RoutedEventArgs e)
        {
            this.IsSelected = true;
        }
    }
}
