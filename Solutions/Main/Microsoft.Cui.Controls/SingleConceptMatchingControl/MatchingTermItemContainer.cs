//-----------------------------------------------------------------------
// <copyright file="MatchingTermItemContainer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>28-Jan-2009</date>
// <summary>The container control for displaying a matching term item.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;

    /// <summary>
    /// The container control for displaying a matching term item.
    /// </summary>
    [TemplatePart(Name = MatchingTermItemContainer.ElementEncodableInputBox, Type = typeof(EncodableInputBox))]
    [TemplatePart(Name = MatchingTermItemContainer.ElementCheckBox, Type = typeof(CheckBox))]
    [TemplateVisualState(Name = "Unhighlighted", GroupName = "HighlightStates")]
    [TemplateVisualState(Name = "Highlighted", GroupName = "HighlightStates")]
    [TemplateVisualState(Name = "CheckBoxFocused", GroupName = "ControlFocusStates")]
    [TemplateVisualState(Name = "EncodableInputBoxFocused", GroupName = "ControlFocusStates")]
    [TemplateVisualState(Name = "NoFocus", GroupName = "ControlFocusStates")]
    public class MatchingTermItemContainer : ContentControl
    {
        /// <summary>
        /// The Selected Item Member Path Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemMemberPathProperty =
            DependencyProperty.Register("SelectedItemMemberPath", typeof(string), typeof(MatchingTermItemContainer), new PropertyMetadata(string.Empty, new PropertyChangedCallback(SelectedItemMemberPath_Changed)));

        /// <summary>
        /// The Is Selected Memeber Path Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsSelectedMemberPathProperty =
            DependencyProperty.Register("IsSelectedMemberPath", typeof(string), typeof(MatchingTermItemContainer), new PropertyMetadata(string.Empty, new PropertyChangedCallback(IsSelectedMemberPath_Changed)));

        /// <summary>
        /// The Selected Item Text Member Path Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemTextMemberPathProperty =
            DependencyProperty.Register("SelectedItemTextMemberPath", typeof(string), typeof(MatchingTermItemContainer), new PropertyMetadata(new PropertyChangedCallback(SelectedItemTextMemberPath_Changed)));

        /// <summary>
        /// The Selected Item Text Member Path Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AlternateItemsMemberPathProperty =
            DependencyProperty.Register("AlternateItemsMemberPath", typeof(string), typeof(MatchingTermItemContainer), new PropertyMetadata(string.Empty, new PropertyChangedCallback(AlternateItemsMemberPath_Changed)));

        /// <summary>
        /// The Selected Item Text Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemTextProperty =
            DependencyProperty.Register("SelectedItemText", typeof(string), typeof(MatchingTermItemContainer), null);

        /// <summary>
        /// The FlyOut Content Template Dependency Property.
        /// </summary>
        public static readonly DependencyProperty FlyOutContentTemplateProperty =
            DependencyProperty.Register("FlyOutContentTemplate", typeof(DataTemplate), typeof(MatchingTermItemContainer), null);

        /// <summary>
        /// The Is Highlighted Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsHighlightedProperty =
            DependencyProperty.Register("IsHighlighted", typeof(bool), typeof(MatchingTermItemContainer), new PropertyMetadata(new PropertyChangedCallback(OnStateChanged)));

        /// <summary>
        /// The Encoded Item Template Dependency Property.
        /// </summary>
        public static readonly DependencyProperty EncodedItemTemplateProperty =
            DependencyProperty.Register("EncodedItemTemplate", typeof(DataTemplate), typeof(MatchingTermItemContainer), null);

        /// <summary>
        /// The Selected Item Text Length Dependeny Property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemTextLengthProperty =
            DependencyProperty.Register("SelectedItemTextLength", typeof(int), typeof(MatchingTermItemContainer), null);

        /// <summary>
        /// The template part name for the encodable input box.
        /// </summary>
        private const string ElementEncodableInputBox = "EncodableInputBox";

        /// <summary>
        /// The template part name for the check box element.
        /// </summary>
        private const string ElementCheckBox = "CheckBox";

        /// <summary>
        /// Stores the encodable input box.
        /// </summary>
        private EncodableInputBox encodbleInputBox;

        /// <summary>
        /// Stores the check box.
        /// </summary>
        private CheckBox checkBox;

        /// <summary>
        /// Stores whether the selected item change is the first.
        /// </summary>
        private bool firstSelectedItemChange = true;

        /// <summary>
        /// Stores the selected item.
        /// </summary>
        private object selectedItem;        

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchingTermItemContainer"/> class.
        /// </summary>
        public MatchingTermItemContainer()
        {
            this.DefaultStyleKey = typeof(MatchingTermItemContainer);
            this.IsTabStop = false;
            base.GotFocus += new RoutedEventHandler(this.MatchingTermItemContainer_GotFocus);
            this.LostFocus += new RoutedEventHandler(this.MatchingTermItemContainer_LostFocus);
#if !SILVERLIGHT
            // The control draws its own dotted line indicating focus.
            this.FocusVisualStyle = null;
#endif
        }

        /// <summary>
        /// The SelectionChanged event.
        /// </summary>
        public event EventHandler SelectionChanged;

        /// <summary>
        /// The popup mouse enter event.
        /// </summary>
        public event MouseEventHandler MouseEnterPopup;

        /// <summary>
        /// The popup mouse leave event.
        /// </summary>
        public event MouseEventHandler MouseLeavePopup;

        /// <summary>
        /// Override GotFocus event.
        /// </summary>
        public new event RoutedEventHandler GotFocus;

        /// <summary>
        /// The PopupOpening event.
        /// </summary>
        public event EventHandler PopupOpening;

        /// <summary>
        /// Gets or sets the selected item text length.
        /// </summary>
        /// <value>The selected item text length.</value>
        public int SelectedItemTextLength
        {
            get { return (int)GetValue(SelectedItemTextLengthProperty); }
            set { SetValue(SelectedItemTextLengthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected item member path.
        /// </summary>
        /// <value>The selected item member path.</value>
        public string SelectedItemMemberPath
        {
            get { return (string)GetValue(SelectedItemMemberPathProperty); }
            set { SetValue(SelectedItemMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the is selected member path.
        /// </summary>
        /// <value>The is selected member path.</value>
        public string IsSelectedMemberPath
        {
            get { return (string)GetValue(IsSelectedMemberPathProperty); }
            set { SetValue(IsSelectedMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected item text member path.
        /// </summary>
        /// <value>Selected item text member path.</value>
        public string SelectedItemTextMemberPath
        {
            get { return (string)GetValue(SelectedItemTextMemberPathProperty); }
            set { SetValue(SelectedItemTextMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the alternate items member path.
        /// </summary>
        /// <value>Alternate items member path.</value>
        public string AlternateItemsMemberPath
        {
            get { return (string)GetValue(AlternateItemsMemberPathProperty); }
            set { SetValue(AlternateItemsMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected item text.
        /// </summary>
        /// <value>The selected item text.</value>
        public string SelectedItemText
        {
            get { return (string)GetValue(SelectedItemTextProperty); }
            set { SetValue(SelectedItemTextProperty, value); }
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
        /// Gets or sets a value indicating whether this item is highlighted.
        /// </summary>
        /// <value>The is highlighted value.</value>
        public bool IsHighlighted
        {
            get { return (bool)GetValue(IsHighlightedProperty); }
            set { SetValue(IsHighlightedProperty, value); }
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
        /// Gets the template parts and stores them.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.encodbleInputBox = (EncodableInputBox)this.GetTemplateChild(MatchingTermItemContainer.ElementEncodableInputBox);
            if (this.encodbleInputBox != null)
            {
                this.encodbleInputBox.MouseEnterPopup += new MouseEventHandler(this.EncodbleInputBox_MouseEnterPopup);
                this.encodbleInputBox.MouseLeavePopup += new MouseEventHandler(this.EncodbleInputBox_MouseLeavePopup);
                this.encodbleInputBox.SelectionChanged += new EventHandler(this.EncodbleInputBox_SelectionChanged);
                this.encodbleInputBox.GotFocus += new RoutedEventHandler(this.MatchingTermItemContainer_GotFocus);
                this.encodbleInputBox.PopupOpening += new EventHandler(this.EncodbleInputBox_PopupOpening);                
                this.UpdateSelectedItemMemberPath();
                this.UpdatedSelectedItemTextMemberPath();
                this.UpdateAlternateItemsMemberPath();
            }

            this.checkBox = (CheckBox)this.GetTemplateChild(MatchingTermItemContainer.ElementCheckBox);
            if (this.checkBox != null)
            {
#if !SILVERLIGHT
                this.checkBox.FocusVisualStyle = null;
#endif
                this.UpdateIsSelectedMemberPath();
            }
        }

        /// <summary>
        /// Update the current visual state of the button.
        /// </summary>
        /// <param name="useTransitions">
        /// True to use transitions when updating the visual state, false to
        /// snap directly to the new visual state.
        /// </param>
        internal void UpdateVisualState(bool useTransitions)
        {
            if (this.IsHighlighted)
            {
                VisualStateManager.GoToState(this, "Highlighted", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Unhighlighted", useTransitions);
            }

            if (this.encodbleInputBox.IsFocused)
            {
                VisualStateManager.GoToState(this, "EncodableInputBoxFocused", useTransitions);
            }
            else if (this.checkBox.IsFocused)
            {
                VisualStateManager.GoToState(this, "CheckBoxFocused", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "NoFocus", useTransitions);
            }
        }

        /// <summary>
        /// Updates the selected item member path.
        /// </summary>
        /// <param name="dependencyObject">The MatchingTermItemContainer.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void SelectedItemMemberPath_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            MatchingTermItemContainer container = (MatchingTermItemContainer)dependencyObject;
            container.UpdateSelectedItemMemberPath();
        }

        /// <summary>
        /// Updates the is selected member path.
        /// </summary>
        /// <param name="dependencyObject">The MatchingTermItemContainer.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void IsSelectedMemberPath_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            MatchingTermItemContainer container = (MatchingTermItemContainer)dependencyObject;
            container.UpdateIsSelectedMemberPath();
        }

        /// <summary>
        /// Updates the selected item text member path.
        /// </summary>
        /// <param name="dependencyObject">The MatchingTermItemContainer.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void SelectedItemTextMemberPath_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            MatchingTermItemContainer container = (MatchingTermItemContainer)dependencyObject;
            container.UpdatedSelectedItemTextMemberPath();
        }

        /// <summary>
        /// Updates the alternate items member path.
        /// </summary>
        /// <param name="dependencyObject">The MatchingTermItemContainer.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void AlternateItemsMemberPath_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            MatchingTermItemContainer container = (MatchingTermItemContainer)dependencyObject;
            container.UpdateAlternateItemsMemberPath();
        }

        /// <summary>
        /// Updates the visual state of the control.
        /// </summary>
        /// <param name="dependencyObject">The instance of the container control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void OnStateChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            MatchingTermItemContainer container = (MatchingTermItemContainer)dependencyObject;
            container.UpdateVisualState(true);
        }

        /// <summary>
        /// Raises the SelectionChanged event.
        /// </summary>
        /// <param name="raiseEvent">Indicated whether to raise the event or not.</param>
        private void UpdatedSelectedItem(bool raiseEvent)
        {
            this.selectedItem = this.encodbleInputBox.SelectedItem;
            Binding binding = new Binding();
            binding.Source = this.encodbleInputBox.SelectedItem;
            binding.Path = new PropertyPath(this.SelectedItemTextMemberPath);
            this.SetBinding(MatchingTermItemContainer.SelectedItemTextProperty, binding);
                        
            if (raiseEvent)
            {
                this.SelectedItemTextLength = this.SelectedItemText.Length;

                if (this.SelectionChanged != null)
                {
                    this.SelectionChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Updates the selected item member path.
        /// </summary>
        private void UpdateSelectedItemMemberPath()
        {
            if (this.SelectedItemMemberPath != null && this.encodbleInputBox != null)
            {
                Binding binding = new Binding(this.SelectedItemMemberPath);
                binding.Mode = BindingMode.TwoWay;
                this.encodbleInputBox.SetBinding(EncodableInputBox.SelectedItemProperty, binding);
            }
        }

        /// <summary>
        /// Updates the is selected member path.
        /// </summary>
        private void UpdateIsSelectedMemberPath()
        {
            if (this.IsSelectedMemberPath != null && this.checkBox != null)
            {
                Binding binding = new Binding(this.IsSelectedMemberPath);
                binding.Mode = BindingMode.TwoWay;
                this.checkBox.SetBinding(CheckBox.IsCheckedProperty, binding);
            }
        }

        /// <summary>
        /// Updates the selected item text member path.
        /// </summary>
        private void UpdatedSelectedItemTextMemberPath()
        {
            if (this.SelectedItemTextMemberPath != null && this.encodbleInputBox != null)
            {
                Binding binding = new Binding();
                binding.Source = this.encodbleInputBox.SelectedItem;
                binding.Path = new PropertyPath(this.SelectedItemTextMemberPath);
                this.SetBinding(MatchingTermItemContainer.SelectedItemTextProperty, binding);
            }
        }

        /// <summary>
        /// Updates the alternate items member path.
        /// </summary>
        private void UpdateAlternateItemsMemberPath()
        {
            if (this.AlternateItemsMemberPath != null && this.encodbleInputBox != null)
            {
                this.encodbleInputBox.SetBinding(EncodableInputBox.ItemsSourceProperty, new Binding(this.AlternateItemsMemberPath));
            }
        }

        /// <summary>
        /// Updates the selected item.
        /// </summary>
        /// <param name="sender">The encodable input box.</param>
        /// <param name="e">Event Args.</param>
        private void EncodbleInputBox_SelectionChanged(object sender, EventArgs e)
        {
            if (!this.firstSelectedItemChange)
            {
                if (this.encodbleInputBox != null)
                {
                    this.UpdatedSelectedItem(this.encodbleInputBox.SelectedItem != this.selectedItem);
                }
            }
            else
            {
                this.selectedItem = this.encodbleInputBox.SelectedItem;
                this.firstSelectedItemChange = false;
            }
        }

        /// <summary>
        /// Raises the mouse enter popup event.
        /// </summary>
        /// <param name="sender">The encodable input box.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void EncodbleInputBox_MouseEnterPopup(object sender, MouseEventArgs e)
        {
            if (this.MouseEnterPopup != null)
            {
                this.MouseEnterPopup(this, e);
            }
        }

        /// <summary>
        /// Raises the mouse leave popup event.
        /// </summary>
        /// <param name="sender">The encodable input box.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void EncodbleInputBox_MouseLeavePopup(object sender, MouseEventArgs e)
        {
            if (this.MouseLeavePopup != null)
            {
                this.MouseLeavePopup(this, e);
            }
        }
        
        /// <summary>
        /// Raises the popup opening event.
        /// </summary>
        /// <param name="sender">The encodable input box.</param>
        /// <param name="e">Event Args.</param>
        private void EncodbleInputBox_PopupOpening(object sender, EventArgs e)
        {
            if (this.PopupOpening != null)
            {
                this.PopupOpening(this, e);
            }
        }

        /// <summary>
        /// Raises the control's GotFocus event.
        /// </summary>
        /// <param name="sender">The container.</param>
        /// <param name="e">Routed Event Args.</param>
        private void MatchingTermItemContainer_GotFocus(object sender, RoutedEventArgs e)
        {            
            this.UpdateVisualState(true);

            if (this.GotFocus != null)
            {
                this.GotFocus(this, e);
            }
        }

        /// <summary>
        /// Sets the currently focused control to null.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="e">Routed Event Args.</param>
        private void MatchingTermItemContainer_LostFocus(object sender, RoutedEventArgs e)
        {
            this.UpdateVisualState(true);
        }
    }
}
