//-----------------------------------------------------------------------
// <copyright file="CollapsibleContentControl.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>02-Sep-2009</date>
// <summary>
//      A collapsible content control.
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

    /// <summary>
    /// A collapsible content control.
    /// </summary>
    [TemplatePart(Name = CollapsibleContentControl.ElementContentContainerName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = CollapsibleContentControl.ElementContentPresenterName, Type = typeof(ContentPresenter))]
    [TemplateVisualState(Name = "Expanded", GroupName = "ExpandedStates")]
    [TemplateVisualState(Name = "Collapsed", GroupName = "ExpandedStates")]
    public class CollapsibleContentControl : ContentControl
    {
        /// <summary>
        /// The IsExpanded Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(CollapsibleContentControl), new PropertyMetadata(true, new PropertyChangedCallback(IsExpanded_Changed)));

        /// <summary>
        /// The ContentContainer Element Name.
        /// </summary>
        private const string ElementContentContainerName = "ContentContainer";

        /// <summary>
        /// The ResizeStoryboard Element Name.
        /// </summary>
        private const string ElementResizeStoryboardName = "ResizeStoryboard";

        /// <summary>
        /// The ContentPresenter Element Name.
        /// </summary>
        private const string ElementContentPresenterName = "ContentPresenter";

        /// <summary>
        /// Stores the content container.
        /// </summary>
        private FrameworkElement contentContainer;

        /// <summary>
        /// Stores the resize story board.
        /// </summary>
        private Storyboard resizeStoryboard;

        /// <summary>
        /// Store the resize double key frame.
        /// </summary>
        private DoubleKeyFrame resizeHeightKeyFrame;

        /// <summary>
        /// Stores the content presenter.
        /// </summary>
        private ContentPresenter contentPresenter;

        /// <summary>
        /// Stores whether the control has been animated.
        /// </summary>
        private bool animated;

        /// <summary>
        /// Stores the default key time.
        /// </summary>
        private KeyTime defaultKeyTime;

        /// <summary>
        /// CollapsibleContentControl constructor.
        /// </summary>
        public CollapsibleContentControl()
        {
            this.DefaultStyleKey = typeof(CollapsibleContentControl);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is expanded or collapsed.
        /// </summary>
        /// <value>The IsExapnded value.</value>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        /// <summary>
        /// Updates the is expanded state.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.contentContainer = this.GetTemplateChild(CollapsibleContentControl.ElementContentContainerName) as FrameworkElement;
            if (this.contentContainer != null)
            {
                this.resizeStoryboard = this.contentContainer.Resources[CollapsibleContentControl.ElementResizeStoryboardName] as Storyboard;
                if (this.resizeStoryboard != null && this.resizeStoryboard.Children.Count == 1)
                {
                    this.resizeHeightKeyFrame = (this.resizeStoryboard.Children[0] as DoubleAnimationUsingKeyFrames).KeyFrames[0];
                    if (this.resizeHeightKeyFrame != null)
                    {
                        this.defaultKeyTime = this.resizeHeightKeyFrame.KeyTime;
                    }
                }
            }

            this.contentPresenter = this.GetTemplateChild(CollapsibleContentControl.ElementContentPresenterName) as ContentPresenter;
            if (this.contentPresenter != null)
            {
                this.contentPresenter.SizeChanged += new SizeChangedEventHandler(this.ContentPresenter_SizeChanged);
            }

            this.UpdateExpandedState(false);
        }

        /// <summary>
        /// Updates the expanded stated.
        /// </summary>
        /// <param name="obj">The collapsible content control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void IsExpanded_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            CollapsibleContentControl collapsibleContentControl = obj as CollapsibleContentControl;
            collapsibleContentControl.UpdateExpandedState(true);
        }

        /// <summary>
        /// Resizes the expanded control.
        /// </summary>
        /// <param name="sender">The content presenter.</param>
        /// <param name="e">Size Changed Event Args.</param>
        private void ContentPresenter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.contentContainer != null)
            {
                this.contentContainer.Width = e.NewSize.Width;
            }

            if (this.IsExpanded)
            {
                if (this.contentContainer != null && !this.animated)
                {
                    this.contentContainer.Height = e.NewSize.Height;
                    this.resizeHeightKeyFrame.Value = e.NewSize.Height;
                }
                else
                {
                    this.ResizeContent(e.NewSize, KeyTime.FromTimeSpan(TimeSpan.Zero));
                }
            }
            else
            {
                if (!this.animated)
                {
                    this.contentContainer.Height = 0;
                    this.resizeHeightKeyFrame.Value = e.NewSize.Height;
                }
                else
                {
                    this.ResizeContent(new Size(0, 0), this.defaultKeyTime);
                }
            }
        }

        /// <summary>
        /// Resize the expanded control.
        /// </summary>
        /// <param name="size">The new size.</param>
        /// <param name="keyTime">The key time.</param>
        private void ResizeContent(Size size, KeyTime keyTime)
        {
            if (this.resizeHeightKeyFrame != null)
            {
                if (size.Height == 0  || size.Height != this.resizeHeightKeyFrame.Value)
                {
                    this.resizeHeightKeyFrame.Value = size.Height;
                    this.resizeHeightKeyFrame.KeyTime = keyTime;
                    this.resizeStoryboard.Begin();
                    this.animated = true;
                }
            }
        }

        /// <summary>
        /// Updates the expanded visual state.
        /// </summary>
        /// <param name="useTransitions">Whether the state change should use transitions.</param>
        private void UpdateExpandedState(bool useTransitions)
        {
            if (this.IsExpanded)
            {
                this.GoToVisualState("Expanded", useTransitions);
                if (this.contentPresenter != null)
                {
                    this.ResizeContent(new Size(0, 0), KeyTime.FromTimeSpan(TimeSpan.Zero));

                    Size size = new Size(this.contentPresenter.ActualWidth, this.contentPresenter.ActualHeight);
#if !SILVERLIGHT
                    FrameworkElement content = this.contentPresenter.Content as FrameworkElement;
                    if (content != null && content.DesiredSize.Width > 0 && content.DesiredSize.Height > 0)
                    {
                        size = new Size(content.DesiredSize.Width, content.DesiredSize.Height);
                    }
#endif

                    this.ResizeContent(size, this.defaultKeyTime);
                }
            }
            else
            {
                this.GoToVisualState("Collapsed", useTransitions);
                if (this.contentPresenter != null)
                {
                    this.ResizeContent(new Size(0, 0), this.defaultKeyTime);
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
    }
}
