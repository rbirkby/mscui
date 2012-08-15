//-----------------------------------------------------------------------
// <copyright file="DownloadMediaControl.xaml.cs" company="Microsoft Corporation copyright 2008.">
// Copyright (c) Microsoft Corporation copyright 2008.
// All rights reserved.
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>16-Mar-2008</date>
// <summary>DownloadMediaControl class.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.ShowcaseControls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.Windows.Browser;
    using System.Text;

    /// <summary>
    /// Usercontrol for downloading resource in showcase demonstartorspage.
    /// </summary>
    public partial class DownloadMediaControl : UserControl
    {
        #region Private Member variables
        /// <summary>
        /// Member variable to hold description text for the control.
        /// </summary>
        private string txtMediaText;

        /// <summary>
        /// Member variable to hold the download image source. 
        /// </summary>
        private string downloadImage;

        /// <summary>
        /// Member variable to hold the download source.
        /// </summary>
        private string downloadSource;

        /// <summary>
        /// Member variable to hold the tooltip text for the image.
        /// </summary>
        private string imageToolTip;

        /// <summary>
        /// Member variable to hold the hyperlink text for the control.
        /// </summary>
        private string hyperlinkText;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor, initialize the instance of the class.
        /// </summary>
        public DownloadMediaControl()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(this.DownloadMediaControl_Loaded);
            this.imgDownload.MouseLeftButtonDown += new MouseButtonEventHandler(this.ImgDownload_MouseLeftButtonDown);

            this.imgDownloadContent.GotFocus += new RoutedEventHandler(this.ImgDownloadContent_GotFocus);
            this.imgDownloadContent.LostFocus += new RoutedEventHandler(this.ImgDownloadContent_LostFocus);
            this.imgDownloadContent.KeyDown += new KeyEventHandler(this.ImgDownloadContent_KeyDown);
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Image Tool Tip value.
        /// </summary>
        /// <value>
        /// Image tooltip.
        /// </value>
        public string ImageToolTip
        {
            get
            {
                return this.imageToolTip;
            }

            set
            {
                this.imageToolTip = value;
            }
        }

        /// <summary>
        /// Gets or sets the control text.
        /// </summary>
        /// <value>
        /// Media Text.
        /// </value>
        public string MediaText
        {
            get
            {
                return this.txtMediaText;
            }

            set
            {
                this.txtMediaText = value;
                this.txtText.Text = this.txtMediaText;
            }
        }

        /// <summary>
        /// Gets or sets the hyperlink text.
        /// </summary>
        /// <value>
        /// Hyperlink text.
        /// </value>
        public string HyperlinkText
        {
            get
            {
                return this.hyperlinkText;
            }

            set
            {
                this.hyperlinkText = value;
            }
        }

        /// <summary>
        /// Gets or sets the download image source.
        /// </summary>
        /// <value>
        /// Download Image.
        /// </value>
        public string DownloadImage
        {
            get
            {
                return this.downloadImage;
            }

            set
            {
                this.downloadImage = value;
            }
        }

        /// <summary>
        /// Gets or sets the download source.
        /// </summary>
        /// <value>
        /// Download source.
        /// </value>
        public string DownloadSource
        {
            get
            {
                return this.downloadSource;
            }

            set
            {
                this.downloadSource = value;
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Click event of the download image which download the intended source.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ImgDownload_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ImgDownloadKeyDown();
        }

        /// <summary>
        /// Video Image Content key down Event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Key event arguments.</param>
        private void ImgDownloadContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.ImgDownloadKeyDown();
            }
        }

        /// <summary>
        /// Video Image Content lost focus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void ImgDownloadContent_LostFocus(object sender, RoutedEventArgs e)
        {
            this.imgDownloadContent.Opacity = 1;
        }

        /// <summary>
        /// Video Image Content got focus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void ImgDownloadContent_GotFocus(object sender, RoutedEventArgs e)
        {
            this.imgDownloadContent.Opacity = 0.5;
        }

        /// <summary>
        /// Click event of the video image to trigger the open animation.
        /// </summary>
        private void ImgDownloadKeyDown()
        {
            StringBuilder uri = new StringBuilder(App.ApplicationPath);
            uri.Append("Showcase/Download/");
            uri.Append(this.downloadSource);

            Uri navigateToUri = new Uri(uri.ToString());
            System.Windows.Browser.HtmlPage.Window.Navigate(navigateToUri);
        }

        /// <summary>
        /// Event onload of the control.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void DownloadMediaControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the image source of the control
            System.Windows.Media.Imaging.BitmapImage imgSource = new System.Windows.Media.Imaging.BitmapImage();
            
            StringBuilder uri = new StringBuilder(App.ApplicationPath);
            uri.Append("Showcase/images/");
            uri.Append(this.downloadImage);

            imgSource.UriSource = new Uri(uri.ToString());
            imgDownload.Source = imgSource;

            // Set the tool tip for the video image            
            ToolTipService.SetToolTip(this.imgDownload, this.imageToolTip);

            this.PopulateMediaText();
        }

        /// <summary>
        /// Function populate the text section the control with correct formatting.
        /// </summary>
        private void PopulateMediaText()
        {
            double height = 0;

            // Parse the media text and create the corresponding textblocks
            string[] textArray = this.txtMediaText.Split('|');
            TextBlock txtBlock = null;

            textContainer.Children.Clear();

            txtBlock = new TextBlock();
            txtBlock.Margin = new Thickness(0, height, 0, 0);
            txtBlock.Width = 560;

            // Measure the textblock height based on the text length
            txtBlock.Height = System.Math.Ceiling((double)textArray[0].Length / 75) * 15;

            // Add the hyperlink text with correct formatting
            txtBlock.HorizontalAlignment = HorizontalAlignment.Left;
            txtBlock.VerticalAlignment = VerticalAlignment.Top;
            txtBlock.FontSize = 13;
            txtBlock.FontFamily = new FontFamily("Verdana");
            txtBlock.Text = textArray[0];
            txtBlock.TextWrapping = TextWrapping.Wrap;

            // Add the textblock to the container
            textContainer.Children.Add(txtBlock);

            height += txtBlock.Height;

            for (int index = 1; index < textArray.Length; index++)
            {
                height += 15;
                txtBlock = new TextBlock();
                txtBlock.Margin = new Thickness(0, height, 0, 0);
                txtBlock.Width = 560;

                // Measure the textblock height based on the text length
                txtBlock.Height = System.Math.Ceiling((double)textArray[index].Length / 75) * 15;
                height += txtBlock.Height;

                // Add the hyperlink text with correct formatting
                txtBlock.HorizontalAlignment = HorizontalAlignment.Left;
                txtBlock.VerticalAlignment = VerticalAlignment.Top;
                txtBlock.FontSize = 13;
                txtBlock.FontFamily = new FontFamily("Verdana");
                txtBlock.Text = textArray[index];
                txtBlock.TextWrapping = TextWrapping.Wrap;

                // Change the textdecoration for the special text
                if (textArray[index].StartsWith("Click the thumbnail", StringComparison.Ordinal))
                {
                    txtBlock.FontSize = 11;
                    txtBlock.FontStyle = FontStyles.Italic;
                }

                // Add the textblock to the container
                textContainer.Children.Add(txtBlock);
            }

            this.PopulateHyperlink(height);
        }

        /// <summary>
        /// Function populate the hyperlinks of the control with correct formatting.
        /// </summary>
        /// <param name="height">Top position of the hyperlinks in the canvas.</param>
        private void PopulateHyperlink(double height)
        {
            if (this.hyperlinkText != null && !string.IsNullOrEmpty(this.hyperlinkText))
            {
                string[] textArray = this.hyperlinkText.Split('|');
                string[] hyperlinkDetail = null;
                TextBlock txtBlock = null;

                for (int index = 0; index < textArray.Length; index++)
                {
                    height += 15;
                    txtBlock = new TextBlock();
                    txtBlock.Margin = new Thickness(0, height, 0, 0);
                    txtBlock.Width = 560;

                    hyperlinkDetail = textArray[index].Split('#');

                    // Measure the textblock height based on the text length
                    txtBlock.Height = System.Math.Ceiling((double)hyperlinkDetail[0].Length / 75) * 15;
                    height += txtBlock.Height;

                    // Add the hyperlink text with correct formatting
                    txtBlock.HorizontalAlignment = HorizontalAlignment.Left;
                    txtBlock.VerticalAlignment = VerticalAlignment.Top;
                    txtBlock.FontSize = 13;
                    txtBlock.Foreground = new SolidColorBrush(Colors.Blue);

                    txtBlock.FontFamily = new FontFamily("Verdana");
                    txtBlock.Text = hyperlinkDetail[0];
                    txtBlock.Tag = hyperlinkDetail[1];

                    // Attach the eventhandler to the hyperlink click event
                    txtBlock.MouseLeftButtonDown += new MouseButtonEventHandler(this.Hyperlink_MouseLeftButtonDown);
                    txtBlock.TextWrapping = TextWrapping.Wrap;
                    txtBlock.Cursor = Cursors.Hand;
                    txtBlock.TextDecorations = TextDecorations.Underline;

                    // Add the hyperlink to the container
                    textContainer.Children.Add(txtBlock);
                }
            }

            textContainer.Height = height;

            // Set the container layout height based on the text container height in the media control
            if (textContainer.Height + 50 > 138)
            {
                LayoutRoot.Height = textContainer.Height + 50;
            }
        }

        /// <summary>
        /// Click event of the hyperlink which open the url in new window.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void Hyperlink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock txtBlock = (TextBlock)sender;
            Uri navigateToUri = new Uri(txtBlock.Tag.ToString());

            // Open the URL in new window
            System.Windows.Browser.HtmlPage.Window.Navigate(navigateToUri, "blank");
        }
        #endregion
    }
}
