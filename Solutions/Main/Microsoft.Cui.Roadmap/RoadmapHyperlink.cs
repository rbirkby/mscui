//-----------------------------------------------------------------------
// <copyright file="RoadmapHyperlink.cs" company="Microsoft Corporation copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation copyright 2007 - 2010.
// All rights reserved.
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>23-Jan-2009</date>
// <summary>RoadmapHyperlink class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Roadmap
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
    /// RoadmapHyperlink class.
    /// </summary>
    public class RoadmapHyperlink : Control
    {   
        /// <summary>
        /// Using a DependencyProperty as the backing store for Uri.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty LinkProperty =
            DependencyProperty.Register("Link", typeof(string), typeof(RoadmapHyperlink), new PropertyMetadata(new PropertyChangedCallback(RoadmapHyperlink.Callback)));

        /// <summary>
        /// Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        /// </summary> 
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(RoadmapHyperlink), new PropertyMetadata(new PropertyChangedCallback(RoadmapHyperlink.Callback)));

        /// <summary>
        /// Textblock used for hyperlink.
        /// </summary>
        private TextBlock hyperlinkTextblock;

        /// <summary>
        /// Flag to show if loaded.
        /// </summary>
        private bool loaded;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoadmapHyperlink"/> class.
        /// </summary>
        public RoadmapHyperlink()
        {
            this.DefaultStyleKey = typeof(RoadmapHyperlink);
            this.Loaded += new RoutedEventHandler(this.RoadmapHyperlink_Loaded);
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text for the hyperlink.</value>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>The URI property.</value>
        public string Link
        {
            get { return (string)GetValue(LinkProperty); }
            set { SetValue(LinkProperty, value); }
        }

        /// <summary>
        /// Callbacks the specified dep obj.
        /// </summary>
        /// <param name="dependencyObj">The dependency obj.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        public static void Callback(DependencyObject dependencyObj, DependencyPropertyChangedEventArgs args)
        {
            ((RoadmapHyperlink)dependencyObj).ValidateLayout();
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.hyperlinkTextblock = this.GetTemplateChild<TextBlock>("HyperlinkTextblock", true);
        }

        /// <summary>
        /// Handles the Loaded event of the RoadmapHyperlink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void RoadmapHyperlink_Loaded(object sender, RoutedEventArgs e)
        {
            this.loaded = true;
            this.ValidateLayout();    
        }

        /// <summary>
        /// Validates the layout.
        /// </summary>
        private void ValidateLayout()
        {
            if (true == this.loaded)
            {
                this.hyperlinkTextblock.Text = this.Text;            
                if (!String.IsNullOrEmpty(this.Link))
                {
                    this.hyperlinkTextblock.MouseLeftButtonDown += new MouseButtonEventHandler(this.HyperlinkTextblock_MouseLeftButtonDown);
                    this.hyperlinkTextblock.Foreground = new SolidColorBrush(Colors.Blue);
                    this.hyperlinkTextblock.TextDecorations = TextDecorations.Underline;
                    this.hyperlinkTextblock.Cursor = Cursors.Hand;
                }
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the hyperlinkTextblock control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void HyperlinkTextblock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Browser.HtmlPage.Window.Navigate(new Uri(App.ApplicationPath + this.Link)); 
        }

        /// <summary>
        /// Gets the template child.
        /// </summary>
        /// <typeparam name="T">The return type from the template.</typeparam>
        /// <param name="name">The name of the template.</param>
        /// <param name="mustHave">If set to <c>true</c> [must have].</param>
        /// <returns>The item created via the template.</returns>
        private T GetTemplateChild<T>(string name, bool mustHave) where T : class
        {
            DependencyObject obj = this.GetTemplateChild(name);
            T returnValue;
            returnValue = obj as T;
            if (null == obj && true == mustHave)
            {
                throw new ArgumentNullException(name, "Invalid Type");
            }

            return returnValue;
        }
    }
}
