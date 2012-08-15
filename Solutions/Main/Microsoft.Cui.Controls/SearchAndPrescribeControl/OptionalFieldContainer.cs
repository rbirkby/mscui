//-----------------------------------------------------------------------
// <copyright file="OptionalFieldContainer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>04-Aug-2009</date>
// <summary>
//      Content control for hiding / showing optional fields.
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
    using System.Windows.Data;

    /// <summary>
    /// Content control for hiding / showing optional fields.
    /// </summary>
    [StyleTypedProperty(Property = "ShowFieldButtonStyle", StyleTargetType = typeof(Button))]
    [TemplatePart(Name = OptionalFieldContainer.ElementContentPresenterName, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = OptionalFieldContainer.ElementShowFieldButtonName, Type = typeof(Button))]
    public class OptionalFieldContainer : ContentControl
    {
        /// <summary>
        /// The IsFieldShowing Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsFieldShowingProperty =
            DependencyProperty.Register("IsFieldShowing", typeof(bool), typeof(OptionalFieldContainer), new PropertyMetadata(false, new PropertyChangedCallback(IsFieldShowing_Changed)));

        /// <summary>
        /// The ShowFieldButtonContent Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShowFieldButtonContentProperty =
            DependencyProperty.Register("ShowFieldButtonContent", typeof(object), typeof(OptionalFieldContainer), null);

        /// <summary>
        /// The ShowFieldButtonContentTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShowFieldButtonContentTemplateProperty =
            DependencyProperty.Register("ShowFieldButtonContentTemplate", typeof(DataTemplate), typeof(OptionalFieldContainer), null);

        /// <summary>
        /// The ShowFieldButtonStyle Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShowFieldButtonStyleProperty =
            DependencyProperty.Register("ShowFieldButtonStyle", typeof(Style), typeof(OptionalFieldContainer), null);

        /// <summary>
        /// The IsOptionalFieldEnabled Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsOptionalFieldEnabledProperty =
            DependencyProperty.Register("IsOptionalFieldEnabled", typeof(bool), typeof(OptionalFieldContainer), new PropertyMetadata(true));

        /// <summary>
        /// The Control Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ControlProperty =
            DependencyProperty.Register("Control", typeof(Control), typeof(OptionalFieldContainer), null);

        /// <summary>
        /// The ContentPresenter Element Name.
        /// </summary>
        private const string ElementContentPresenterName = "ContentPresenter";

        /// <summary>
        /// The ShowFieldButton Element Name.
        /// </summary>
        private const string ElementShowFieldButtonName = "ShowFieldButton";

        /// <summary>
        /// Stores the content presenter.
        /// </summary>
        private ContentPresenter contentPresenter;

        /// <summary>
        /// Stores the show field button.
        /// </summary>
        private Button showFieldButton;

        /// <summary>
        /// Stores if the control is focused.
        /// </summary>
        private bool isFocused;

        /// <summary>
        /// OptionalFieldContainer constructor.
        /// </summary>
        public OptionalFieldContainer()
        {
            this.DefaultStyleKey = typeof(OptionalFieldContainer);
            this.GotFocus += new RoutedEventHandler(this.OptionalFieldContainer_GotFocus);
            this.LostFocus += new RoutedEventHandler(this.OptionalFieldContainer_LostFocus);
        }

        /// <summary>
        /// The IsFieldShowingChanged event.
        /// </summary>
        public event EventHandler IsFieldShowingChanged;

        /// <summary>
        /// The Expanded Event.
        /// </summary>
        public event EventHandler Expanded;

        /// <summary>
        /// The collapsed event.
        /// </summary>
        public event EventHandler Collapsed;

        /// <summary>
        /// Gets or sets a value indicating whether the field is showing.
        /// </summary>
        /// <value>The IsFieldShowing value.</value>
        public bool IsFieldShowing
        {
            get { return (bool)GetValue(IsFieldShowingProperty); }
            set { SetValue(IsFieldShowingProperty, value); }
        }

        /// <summary>
        /// Gets or sets the show field button content.
        /// </summary>
        /// <value>The ShowFieldButtonContent value.</value>
        public object ShowFieldButtonContent
        {
            get { return (object)GetValue(ShowFieldButtonContentProperty); }
            set { SetValue(ShowFieldButtonContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets the show field button style.
        /// </summary>
        /// <value>The show field button style.</value>
        public Style ShowFieldButtonStyle
        {
            get { return (Style)GetValue(ShowFieldButtonStyleProperty); }
            set { SetValue(ShowFieldButtonStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the show field button content template.
        /// </summary>
        /// <value>The show field button content template value.</value>
        public DataTemplate ShowFieldButtonContentTemplate
        {
            get { return (DataTemplate)GetValue(ShowFieldButtonContentTemplateProperty); }
            set { SetValue(ShowFieldButtonContentTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the optional field is enabled.
        /// </summary>
        /// <value>Whether the optional field is enabled.</value>
        public bool IsOptionalFieldEnabled
        {
            get { return (bool)GetValue(IsOptionalFieldEnabledProperty); }
            set { SetValue(IsOptionalFieldEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets the control inside the content.
        /// </summary>
        /// <value>The control content.</value>
        public Control Control
        {
            get { return (Control)GetValue(ControlProperty); }
            set { SetValue(ControlProperty, value); }
        }

        /// <summary>
        /// Collapses the control without writing to the is field showing property.
        /// </summary>
        public void Collapse()
        {
            this.UpdateIsFieldShowing(false);
        }

        /// <summary>
        /// Expands the control without writing to the is field showing property.
        /// </summary>
        public void Expand()
        {
            this.UpdateIsFieldShowing(true);
        }

        /// <summary>
        /// Gets the template parts from the template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.contentPresenter = this.GetTemplateChild(OptionalFieldContainer.ElementContentPresenterName) as ContentPresenter;
            this.showFieldButton = this.GetTemplateChild(OptionalFieldContainer.ElementShowFieldButtonName) as Button;

            if (this.showFieldButton != null)
            {
                this.showFieldButton.Click += new RoutedEventHandler(this.ShowFieldButton_Click);
            }

            this.UpdateIsFieldShowing(this.IsFieldShowing);
        }

        /// <summary>
        /// Updates the UI to show / hide the field.
        /// </summary>
        /// <param name="obj">The Optional Field Container.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void IsFieldShowing_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            OptionalFieldContainer optionalFieldContainer = obj as OptionalFieldContainer;
            optionalFieldContainer.UpdateIsFieldShowing((args.NewValue as bool?).Value);

            if (optionalFieldContainer.IsFieldShowingChanged != null)
            {
                optionalFieldContainer.IsFieldShowingChanged(optionalFieldContainer, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets the first control in the tree.
        /// </summary>
        /// <param name="parent">The parent element to search.</param>
        /// <returns>The first control within the parent element.</returns>
        private static Control GetFirstControl(UIElement parent)
        {
            if (parent == null)
            {
                return null;
            }

            Control parentControl = parent as Control;
            if (parentControl != null)
            {
                return parentControl;
            }

            Border parentBorder = parent as Border;
            if (parentBorder != null)
            {
                return GetFirstControl(parentBorder.Child);
            }

            Panel parentPanel = parent as Panel;
            if (parentPanel != null)
            {
                foreach (UIElement child in parentPanel.Children)
                {
                    Control childControl = GetFirstControl(child);
                    if (childControl != null)
                    {
                        return childControl;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        /// <param name="isFieldShowing">Whether the field is showing.</param>
        private void UpdateIsFieldShowing(bool isFieldShowing)
        {
            if (this.contentPresenter != null && this.showFieldButton != null)
            {
                if (isFieldShowing && this.Content != null)
                {
                    this.contentPresenter.Visibility = Visibility.Visible;
                    this.showFieldButton.Visibility = Visibility.Collapsed;

                    if (this.isFocused)
                    {
                        Control control = this.Control != null ? this.Control : GetFirstControl(this.Content as UIElement);
                        SplitComboBox splitComboBox = control as SplitComboBox;
                        if (splitComboBox != null)
                        {
                            splitComboBox.OpenDropDownOnGotFocus = true;
                        }

                        FocusHelper.FocusControl(control);
                    }

                    if (this.Expanded != null)
                    {
                        this.Expanded(this, EventArgs.Empty);
                    }
                }
                else
                {
                    this.showFieldButton.Visibility = Visibility.Visible;
                    this.contentPresenter.Visibility = Visibility.Collapsed;

                    if (this.isFocused)
                    {
                        FocusHelper.FocusControl(this.showFieldButton);
                    }

                    if (this.Collapsed != null)
                    {
                        this.Collapsed(this, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Shows the field.
        /// </summary>
        /// <param name="sender">The ShowFieldButton.</param>
        /// <param name="e">Routed Event Args.</param>
        private void ShowFieldButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Content != null && this.IsOptionalFieldEnabled)
            {
                this.UpdateIsFieldShowing(true);
            }
        }

        /// <summary>
        /// Sets the is focused flag to false.
        /// </summary>
        /// <param name="sender">The optional field container.</param>
        /// <param name="e">Routed Event Args.</param>
        private void OptionalFieldContainer_LostFocus(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_LostFocus);
            CompositionTarget.Rendering += new EventHandler(this.CompositionTargetRendering_LostFocus);
        }

        /// <summary>
        /// Sets the is focused flag to false.
        /// </summary>
        /// <param name="sender">The composition target.</param>
        /// <param name="e">Event Args.</param>
        private void CompositionTargetRendering_LostFocus(object sender, EventArgs e)
        {
            CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_LostFocus);
            this.isFocused = false;
        }

        /// <summary>
        /// Sets the is focused flag to true.
        /// </summary>
        /// <param name="sender">The optional field container.</param>
        /// <param name="e">Routed Event Args.</param>
        private void OptionalFieldContainer_GotFocus(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_LostFocus);
            this.isFocused = true;

            if (this.showFieldButton.Visibility == Visibility.Visible && this.showFieldButton != null && e.OriginalSource != this.showFieldButton)
            {
                FocusHelper.FocusControl(this.showFieldButton);
            }
        }
    }
}
