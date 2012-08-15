//-----------------------------------------------------------------------
// <copyright file="WatermarkedTextBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>28-Aug-2009</date>
// <summary>
//      A text box with watermark.
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
    using System.ComponentModel;
    using System.Windows.Data;

    /// <summary>
    /// A text box with watermark.
    /// </summary>
    public class WatermarkedTextBox : TextBox
    {
        /// <summary>
        /// The Watermark Dependency Property.
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty =
           DependencyProperty.Register("Watermark", typeof(object), typeof(WatermarkedTextBox), null);

        /// <summary>
        /// The WatermarkTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty WatermarkTemplateProperty =
         DependencyProperty.Register("WatermarkTemplate", typeof(DataTemplate), typeof(WatermarkedTextBox), null);

        /// <summary>
        /// The WatermarkVisibility Dependency Property.
        /// </summary>
        public static readonly DependencyProperty WatermarkVisibilityProperty =
          DependencyProperty.Register("WatermarkVisibility", typeof(Visibility), typeof(WatermarkedTextBox), null);

        /// <summary>
        /// The SelectTextOnFocus Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectTextOnFocusProperty =
            DependencyProperty.Register("SelectTextOnFocus", typeof(bool), typeof(WatermarkedTextBox), new PropertyMetadata(true));

#if SILVERLIGHT
        /// <summary>
        /// The HighContrastCaretColor Dependency Property.
        /// </summary>
        public static readonly DependencyProperty HighContrastCaretColorProperty =
            DependencyProperty.Register("HighContrastCaretColor", typeof(Color), typeof(WatermarkedTextBox), new PropertyMetadata(Colors.White));
#endif

#if !SILVERLIGHT
        /// <summary>
        /// Stores whether the text box should select all on click.
        /// </summary>
        private bool selectAllOnClick;
#endif

        /// <summary>
        /// WatermarkedTextBox constructor.
        /// </summary>
        public WatermarkedTextBox()
        {
            this.DefaultStyleKey = typeof(WatermarkedTextBox);
            this.TextChanged += new TextChangedEventHandler(this.WatermarkedTextBox_TextChanged);
            this.GotFocus += new RoutedEventHandler(this.WatermarkedTextBox_GotFocus);
            this.LayoutUpdated += new EventHandler(this.WatermarkedTextBox_LayoutUpdated);
        }        

        /// <summary>
        /// The KeyDown Event.
        /// </summary>
        public new event KeyEventHandler KeyDown;

        /// <summary>
        /// The Pressed Event.
        /// </summary>
        public event MouseButtonEventHandler Pressed;

        /// <summary>
        /// The Click Event.
        /// </summary>
        public event MouseButtonEventHandler Click;

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
        /// Gets or sets the watermark template.
        /// </summary>
        /// <value>The watermark template value.</value>
        public DataTemplate WatermarkTemplate
        {
            get { return (DataTemplate)GetValue(WatermarkTemplateProperty); }
            set { SetValue(WatermarkTemplateProperty, value); }
        }

        /// <summary>
        /// Gets the watermark visibility.
        /// </summary>
        /// <value>The watermark visibility value.</value>
        public Visibility WatermarkVisibility
        {
            get { return (Visibility)GetValue(WatermarkVisibilityProperty); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text should be selected on focus.
        /// </summary>
        /// <value>Whether the text should be focused on focus.</value>
        public bool SelectTextOnFocus
        {
            get { return (bool)GetValue(SelectTextOnFocusProperty); }
            set { SetValue(SelectTextOnFocusProperty, value); }
        }

#if SILVERLIGHT
        /// <summary>
        /// Gets or sets the high contrast caret color.
        /// </summary>
        /// <value>The high contrast caret color.</value>
        public Color HighContrastCaretColor
        {
            get { return (Color)GetValue(HighContrastCaretColorProperty); }
            set { SetValue(HighContrastCaretColorProperty, value); }
        }
#endif

        /// <summary>
        /// Hides the watermark if necessary.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            SetValue(WatermarkedTextBox.WatermarkVisibilityProperty, string.IsNullOrEmpty(this.Text) ? Visibility.Visible : Visibility.Collapsed);
        }

        /// <summary>
        /// KeyDown override.
        /// </summary>
        /// <param name="e">Key Event Args.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key != Key.Space)
            {
                if (this.KeyDown != null)
                {
                    this.KeyDown(this, e);
                }

                base.OnKeyDown(e);
            }
            else
            {
                int selectionStart = this.SelectionStart;
                int maxLength = this.MaxLength != 0 ? this.MaxLength : int.MaxValue;
                if (this.SelectionLength == 0 && this.Text.Length < maxLength)
                {
                    this.Text = this.Text.Insert(selectionStart, " ");
                    this.SelectionStart = selectionStart + 1;
                }
                else if (this.Text.Length - this.SelectionLength < maxLength)
                {
                    this.Text = this.Text.Remove(this.SelectionStart, this.SelectionLength).Insert(selectionStart, " ");
                    this.SelectionStart = selectionStart + 1;
                }

                e.Handled = true;
            }
        }

#if SILVERLIGHT
        /// <summary>
        /// Raises the pressed event.
        /// </summary>
        /// <param name="e">Mouse Button Event Args.</param>
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (this.Pressed != null)
            {
                this.Pressed(this, e);
            }
        }
#else
        /// <summary>
        /// Raises the pressed event.
        /// </summary>
        /// <param name="e">Mouse Button Event Args.</param>
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if (this.SelectTextOnFocus && !this.IsFocused)
            {
                this.selectAllOnClick = true;
            }

            base.OnPreviewMouseLeftButtonDown(e);            
            if (this.Pressed != null)
            {
                this.Pressed(this, e);
            }
        }
#endif

#if SILVERLIGHT
        /// <summary>
        /// Raises a click event.
        /// </summary>
        /// <param name="e">Mouse Button Event Args.</param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }
#else
        /// <summary>
        /// Raises the click event.
        /// </summary>
        /// <param name="e">Mouse Button Event Args.</param>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (this.Click != null)
            {
                this.Click(this, null);
            }

            if (this.selectAllOnClick)
            {
                this.SelectAll();
                this.selectAllOnClick = false;
            }
        }
#endif
        /// <summary>
        /// Updates the watermarks visibility.
        /// </summary>
        /// <param name="sender">The icon text box.</param>
        /// <param name="e">Text Changed Event Args.</param>
        private void WatermarkedTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetValue(WatermarkedTextBox.WatermarkVisibilityProperty, string.IsNullOrEmpty(this.Text) ? Visibility.Visible : Visibility.Collapsed);
            BindingExpression bindingExpression = this.GetBindingExpression(TextBox.TextProperty);
            if (bindingExpression != null)
            {
                bindingExpression.UpdateSource();
            }
        }

        /// <summary>
        /// Selects the text if the control is set to on focus.
        /// </summary>
        /// <param name="sender">The watermarked text box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void WatermarkedTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.SelectTextOnFocus)
            {
                this.SelectAll();
            }

#if SILVERLIGHT
            if (SystemParameters.HighContrast && this.HighContrastCaretColor != null)
            {
                this.CaretBrush = new SolidColorBrush(this.HighContrastCaretColor);
            }
#endif
        }

        /// <summary>
        /// Updates the watermark visibilty.
        /// </summary>
        /// <param name="sender">The watermarked text box.</param>
        /// <param name="e">Event Args.</param>
        private void WatermarkedTextBox_LayoutUpdated(object sender, EventArgs e)
        {
            SetValue(WatermarkedTextBox.WatermarkVisibilityProperty, string.IsNullOrEmpty(this.Text) ? Visibility.Visible : Visibility.Collapsed);
        }
    }
}
