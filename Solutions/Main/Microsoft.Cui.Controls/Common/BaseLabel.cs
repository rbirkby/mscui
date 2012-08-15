//-----------------------------------------------------------------------
// <copyright file="BaseLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>25-Feb-2009</date>
// <summary>Base Label control.</summary>
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

    /// <summary>
    /// Enumeration listing possible HoverAction states.
    /// </summary>
    public enum HoverAction
    {
        /// <summary>
        /// No action.
        /// </summary>
        None,

        /// <summary>
        /// Shows a border around the label.
        /// </summary>
        Border,

        /// <summary>
        /// Underlines the text.
        /// </summary>
        Underline
    }

    /// <summary>
    /// Enumeration listing the possible control states.
    /// </summary>
    internal enum ControlState
    {
        /// <summary>
        /// Normal state.
        /// </summary>
        Normal,

        /// <summary>
        /// Focused state.
        /// </summary>
        Focused,

        /// <summary>
        /// Hovered state.
        /// </summary>
        Hovered
    }

    /// <summary>
    /// Base label control.
    /// </summary>
    public abstract class BaseLabel : Control
    {
        #region Dependency Properties
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.BaseLabel.DisplayValue"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplayValueProperty = DependencyProperty.Register(
            "DisplayValue", typeof(string), typeof(BaseLabel), new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnDisplayValueChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.BaseLabel.HoverAction"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HoverActionProperty = DependencyProperty.Register(
            "HoverAction", typeof(HoverAction), typeof(BaseLabel), new PropertyMetadata(HoverAction.None));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.BaseLabel.TextWrapping"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register(
            "TextWrapping", typeof(TextWrapping), typeof(BaseLabel), new PropertyMetadata(TextWrapping.Wrap));

#if SILVERLIGHT
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.BaseLabel.ToolTip"/> dependency property.
        /// </summary>
        private static readonly DependencyProperty ToolTipProperty = DependencyProperty.Register(
            "ToolTip", typeof(object), typeof(BaseLabel), new PropertyMetadata(new PropertyChangedCallback(OnToolTipChanged)));
#endif
        #endregion

        #region Private Members
        /// <summary>
        /// Member variable to indicate whether the control has focus.
        /// </summary>
        private bool hasFocus;        
        #endregion
       
        #region Constructor
        /// <summary>
        /// Initializes a new instance of BaseLabel control.
        /// </summary>
        internal BaseLabel()
        {
            this.DefaultStyleKey = typeof(BaseLabel);
            this.MouseEnter += new MouseEventHandler(this.Label_MouseEnter);
            this.MouseLeave += new MouseEventHandler(this.Label_MouseLeave);
            this.GotFocus += new RoutedEventHandler(this.Label_GotFocus);
            this.LostFocus += new RoutedEventHandler(this.Label_LostFocus);
            this.KeyDown += new KeyEventHandler(this.Label_KeyDown);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(this.Label_MouseLeftButtonDown);

            this.ShowHandOnHover = true;

#if !SILVERLIGHT
            this.FocusVisualStyle = null;        
#endif
        }
        #endregion

        #region Events
        /// <summary>
        /// Fires when mouse left button is pressed on the control.
        /// </summary>
        public event RoutedEventHandler Click;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the display value.
        /// </summary>
        /// <value>Display Value of the label.</value>
        [Category("Appearance")]
        public string DisplayValue
        {
            get { return (string)GetValue(DisplayValueProperty); }
            internal set { SetValue(DisplayValueProperty, value); }
        }        

        /// <summary>
        /// Gets or sets a value indicating the action that needs to be taken on Hover.
        /// </summary>
        /// <value>Value indicating action that needs to be taken on Hover.</value>
        [Category("Behavior")]
        public HoverAction HoverAction
        {
            get { return (HoverAction)GetValue(HoverActionProperty); }
            set { SetValue(HoverActionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text wrapping behavior.
        /// </summary>
        /// <value>Value indicating the text wrapping behavior.</value>
        [Category("Behavior")]
        public TextWrapping TextWrapping
        {
            get { return (TextWrapping)this.GetValue(TextWrappingProperty); }
            set { this.SetValue(TextWrappingProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show to hand cursor on hover.
        /// </summary>
        /// <value>Value indicating whether to show Hand cursor on hover.</value>
        /// <remarks>Defaults to true.</remarks>
        [Category("Behavior")]
        public bool ShowHandOnHover
        {
            get;
            set;
        }

#if SILVERLIGHT
        /// <summary>
        /// Gets or sets the ToolTip for the control.
        /// </summary>
        /// <value>ToolTip for the control.</value>
        public object ToolTip
        {
            get { return this.GetValue(ToolTipProperty); }
            set { this.SetValue(ToolTipProperty, value); }
        }
#endif
        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets or sets a value indicating whether the DisplayValue property can be changed.
        /// </summary>
        /// <value>Boolean indicating whether the DisplayValue property can be changed.</value>
        protected bool CanChangeDisplayValue
        {
            get;
            set;
        }
        #endregion        

        #region Overrides
        /// <summary>
        /// Overridden. Applies the specified template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            object currentFocusedElement;
#if SILVERLIGHT
            currentFocusedElement = FocusManager.GetFocusedElement();
#else
            currentFocusedElement = FocusManager.GetFocusedElement(this);
#endif

            if (currentFocusedElement != null)
            {
                this.hasFocus = currentFocusedElement.Equals(this);
            }

            if (this.hasFocus == true)
            {
                this.GoToState(ControlState.Focused);
            }
            else
            {
                this.GoToState(ControlState.Normal);
            }

            this.UpdateDisplayValue();
        }

        /// <summary>
        /// Updates the display value.
        /// </summary>
        protected virtual void UpdateDisplayValue()
        {
        }
        #endregion

        #region Property Changed Callbacks
        /// <summary>
        /// Handles the property changed event of the DisplayValue property.
        /// </summary>
        /// <param name="sender">Object whose value got changed.</param>
        /// <param name="args">Event args containing instance data.</param>
        private static void OnDisplayValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            BaseLabel label = sender as BaseLabel;
            if (label != null)
            {
                if (args.Property == DisplayValueProperty && !label.CanChangeDisplayValue)
                {
                    label.CanChangeDisplayValue = true;
                    label.DisplayValue = args.OldValue as string;
                    label.CanChangeDisplayValue = false;
                    throw new InvalidOperationException("DisplayValue property is readonly.");
                }

                if (string.IsNullOrEmpty(label.DisplayValue))
                {
                    label.Visibility = Visibility.Collapsed;
                }
                else
                {
                    label.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Handles the property changed event of the ToolTip property.
        /// </summary>
        /// <param name="sender">Object whose value got changed.</param>
        /// <param name="args">Event args containing instance data.</param>
        private static void OnToolTipChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            BaseLabel label = sender as BaseLabel;
            if (label != null)
            {
                ToolTipService.SetToolTip(label, args.NewValue);                
            }
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Handles the MouseLeftButtonDown event on the control.
        /// </summary>
        /// <param name="sender">Object on which the MouseLeftButton was pressed.</param>
        /// <param name="e">EventArgs containing instance data.</param>
        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.RaiseClickEvent();
            this.Focus();
        }

        /// <summary>
        /// Handles the KeyDown event on the control.
        /// </summary>
        /// <param name="sender">Object on which the Key was pressed.</param>
        /// <param name="e">EventArgs containing instance data.</param>
        private void Label_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                this.RaiseClickEvent();
            }
        }

        /// <summary>
        /// Handles the LostFocus event on the control.
        /// </summary>
        /// <param name="sender">Object which lost the focus.</param>
        /// <param name="e">EventArgs containing instance data.</param>
        private void Label_LostFocus(object sender, RoutedEventArgs e)
        {
            this.hasFocus = false;
            this.GoToState(ControlState.Normal);
        }

        /// <summary>
        /// Handles the GotFocus event on the control.
        /// </summary>
        /// <param name="sender">Object which got the focus.</param>
        /// <param name="e">EventArgs containing instance data.</param>
        private void Label_GotFocus(object sender, RoutedEventArgs e)
        {
            this.hasFocus = true;
            this.GoToState(ControlState.Focused);
        }

        /// <summary>
        /// Handles the MouseLeave event on the control.
        /// </summary>
        /// <param name="sender">Object on which the mouse exited.</param>
        /// <param name="e">EventArgs containing instance data.</param>
        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!this.hasFocus)
            {
                this.GoToState(ControlState.Normal);
            }
            else
            {
                this.GoToState(ControlState.Focused);
            }

            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Handles the MouseEnter event on the control.
        /// </summary>
        /// <param name="sender">Object on which the mouse entered.</param>
        /// <param name="e">EventArgs containing instance data.</param>
        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            this.GoToState(ControlState.Hovered);

            if (this.ShowHandOnHover)
            {
                this.Cursor = Cursors.Hand;
            }
        }
        #endregion
        
        #region Private Methods
        /// <summary>
        /// Raise the click event.
        /// </summary>
        private void RaiseClickEvent()
        {
            if (this.Click != null)
            {
                this.Click(this, new RoutedEventArgs());
            }
        }

        /// <summary>
        /// Changes the state of the control.
        /// </summary>
        /// <param name="controlState">Name of the state to which the control needs to be transitioned to.</param>
        private void GoToState(ControlState controlState)
        {
            string stateName = "Normal";
            switch (controlState)
            {
                case ControlState.Normal:
                    stateName = "Normal";
                    break;
                case ControlState.Focused:
                    if (this.HoverAction == HoverAction.Border)
                    {
                        stateName = "HoverActionBorderWithFocus";
                    }
                    else if (this.HoverAction == HoverAction.Underline)
                    {
                        stateName = "HoverActionUnderlineWithFocus";
                    }

                    break;
                case ControlState.Hovered:
                    if (this.HoverAction == HoverAction.Border)
                    {
                        stateName = "HoverActionBorder";
                    }
                    else if (this.HoverAction == HoverAction.Underline)
                    {
                        stateName = "HoverActionUnderline";
                    }

                    break;
            }

            VisualStateManager.GoToState(this, stateName, true);
        }        
        #endregion
    }
}
