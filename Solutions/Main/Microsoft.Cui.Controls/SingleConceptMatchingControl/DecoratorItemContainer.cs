//-----------------------------------------------------------------------
// <copyright file="DecoratorItemContainer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The container control for displaying a decorated text item.</summary>
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

    /// <summary>
    /// Defines the container position in the term.
    /// </summary>
    public enum ContainerPosition
    {
        /// <summary>
        /// The container has no position in a term.
        /// </summary>
        None,

        /// <summary>
        /// The container is at the start of the term.
        /// </summary>
        Start,

        /// <summary>
        /// The container is in the middle of the term.
        /// </summary>
        Middle,

        /// <summary>
        /// The container is at the end of the term.
        /// </summary>
        End,

        /// <summary>
        /// The container is the only one in this term.
        /// </summary>
        Only
    }

    /// <summary>
    /// The container control for displaying a decorated text item.
    /// </summary>
    [TemplatePart(Name = DecoratorItemContainer.ElementContentPresenter, Type = typeof(ContentPresenter))]
    [TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
    [TemplateVisualState(Name = "Normal", GroupName = "EncodeStates")]
    [TemplateVisualState(Name = "Encoded", GroupName = "EncodeStates")]
    [TemplateVisualState(Name = "Encodable", GroupName = "EncodeStates")]
    [TemplateVisualState(Name = "FocusUnhighlighted", GroupName = "FocusHighlightStates")]
    [TemplateVisualState(Name = "FocusHighlighted", GroupName = "FocusHighlightStates")]
    [TemplateVisualState(Name = "MouseUnhighlighted", GroupName = "MouseHighlightStates")]
    [TemplateVisualState(Name = "MouseHighlighted", GroupName = "MouseHighlightStates")]
    [TemplateVisualState(Name = "None", GroupName = "PositionStates")]
    [TemplateVisualState(Name = "Only", GroupName = "PositionStates")]
    [TemplateVisualState(Name = "Start", GroupName = "PositionStates")]
    [TemplateVisualState(Name = "Middle", GroupName = "PositionStates")]
    [TemplateVisualState(Name = "End", GroupName = "PositionStates")]
    public class DecoratorItemContainer : ContentControl
    {
        /// <summary>
        /// The IsEncodable Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsEncodableProperty =
            DependencyProperty.Register("IsEncodable", typeof(bool), typeof(DecoratorItemContainer), new PropertyMetadata(new PropertyChangedCallback(OnStateChanged)));

        /// <summary>
        /// The IsEncoded Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsEncodedProperty =
            DependencyProperty.Register("IsEncoded", typeof(bool), typeof(DecoratorItemContainer), new PropertyMetadata(new PropertyChangedCallback(OnStateChanged)));

        /// <summary>
        /// The container position Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ContainerPositionProperty =
            DependencyProperty.Register("ContainerPosition", typeof(ContainerPosition), typeof(DecoratorItemContainer), new PropertyMetadata(new PropertyChangedCallback(OnStateChanged)));

        /// <summary>
        /// The Is Mouse Highlighted Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsMouseHighlightedProperty =
            DependencyProperty.Register("IsMouseHighlighted", typeof(bool), typeof(DecoratorItemContainer), new PropertyMetadata(new PropertyChangedCallback(OnStateChanged)));

        /// <summary>
        /// The Is Focus Highlighted Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsFocusHighlightedProperty =
            DependencyProperty.Register("IsFocusHighlighted", typeof(bool), typeof(DecoratorItemContainer), new PropertyMetadata(new PropertyChangedCallback(OnStateChanged)));

        /// <summary>
        /// The ContentPresenter element name.
        /// </summary>
        public const string ElementContentPresenter = "ContentPresenter";

        /// <summary>
        /// Stores the containers top.
        /// </summary>
        private double top;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DecoratorItemContainer"/> class.
        /// </summary>
        public DecoratorItemContainer()
        {
            this.DefaultStyleKey = typeof(DecoratorItemContainer);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the data is encodable.
        /// </summary>
        /// <value>A bool value.</value>
        public bool IsEncodable
        {
            get { return (bool)GetValue(IsEncodableProperty); }
            set { SetValue(IsEncodableProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the data is encoded.
        /// </summary>
        /// <value>A bool value.</value>
        public bool IsEncoded
        {
            get { return (bool)GetValue(IsEncodedProperty); }
            set { SetValue(IsEncodedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the container position.
        /// </summary>
        /// <value>A container position value.</value>
        public ContainerPosition ContainerPosition
        {
            get { return (ContainerPosition)GetValue(ContainerPositionProperty); }
            set { SetValue(ContainerPositionProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the container is mouse highlighted.
        /// </summary>
        /// <value>A bool value.</value>
        public bool IsMouseHighlighted
        {
            get { return (bool)GetValue(IsMouseHighlightedProperty); }
            set { SetValue(IsMouseHighlightedProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the container is focus highlighted.
        /// </summary>
        /// <value>A bool value.</value>
        public bool IsFocusHighlighted
        {
            get { return (bool)GetValue(IsFocusHighlightedProperty); }
            set { SetValue(IsFocusHighlightedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the top value.
        /// </summary>
        /// <value>A double value.</value>
        public double Top
        {
            get
            {
                return this.top;
            }

            set
            {
                this.top = value;
            }
        }

        /// <summary>
        /// Gets the template parts out and sets properties.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ContentPresenter contentPresenter = (ContentPresenter)this.GetTemplateChild(ElementContentPresenter);
            if (contentPresenter != null)
            {
#if SILVERLIGHT
                contentPresenter.UseLayoutRounding = false;
#endif
                contentPresenter.Margin = new Thickness(0);                
            }

            this.UpdateVisualState(false);
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
            if (!this.IsEnabled)
            {
                VisualStateManager.GoToState(this, "Disabled", useTransitions);
            }
            else if (this.IsEncoded)
            {
                VisualStateManager.GoToState(this, "Encoded", useTransitions);
            }
            else if (this.IsEncodable)
            {
                VisualStateManager.GoToState(this, "Encodable", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Normal", useTransitions);
            }

            switch (this.ContainerPosition)
            {
                case ContainerPosition.None:
                    VisualStateManager.GoToState(this, "None", useTransitions);
                    break;
                case ContainerPosition.Only:
                    VisualStateManager.GoToState(this, "Only", useTransitions);
                    break;
                case ContainerPosition.Start:
                    VisualStateManager.GoToState(this, "Start", useTransitions);
                    break;
                case ContainerPosition.Middle:
                    VisualStateManager.GoToState(this, "Middle", useTransitions);
                    break;
                case ContainerPosition.End:
                    VisualStateManager.GoToState(this, "End", useTransitions);
                    break;
            }

            if (this.IsFocusHighlighted)
            {
                VisualStateManager.GoToState(this, "FocusHighlighted", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "FocusUnhighlighted", useTransitions);
            }

            if (this.IsMouseHighlighted)
            {
                VisualStateManager.GoToState(this, "MouseHighlighted", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "MouseUnhighlighted", useTransitions);
            }
        }

        /// <summary>
        /// Updates the visual state of the control.
        /// </summary>
        /// <param name="dependencyObject">The instance of the container control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void OnStateChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            DecoratorItemContainer container = (DecoratorItemContainer)dependencyObject;
            container.UpdateVisualState(true);
        }
    }
}
