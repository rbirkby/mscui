//-----------------------------------------------------------------------
// <copyright file="ViewVideo.xaml.cs" company="Microsoft Corporation copyright 2008.">
// Copyright (c) Microsoft Corporation copyright 2008.
// All rights reserved.
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>16-Mar-2008</date>
// <summary>ViewVideo class.</summary>
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
    /// Video view usercontrol for viewing the demonstartor video url in showcase - demonstartor page.
    /// </summary>
    public partial class ViewVideo : UserControl
    {
        #region Private Member variables
        /// <summary>
        /// Member variable to hold description text for the control.
        /// </summary>
        private string txtMediaText;

        /// <summary>
        /// Member variable to hold the video image source. 
        /// </summary>
        private string videoImage;

        /// <summary>
        /// Member variable to hold the video source.
        /// </summary>
        private string videoSource;

        /// <summary>
        /// Member variable to hold control title.
        /// </summary>
        private string txtMediaTitle;

        /// <summary>
        /// Member variable to hold the tooltip text for the image.
        /// </summary>
        private string imageToolTip;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor, initialize the instance of the class.
        /// </summary>
        public ViewVideo()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(this.DownloadMediaControl_Loaded);
            this.imgVideo.MouseLeftButtonDown += new MouseButtonEventHandler(this.ImgVideo_MouseLeftButtonDown);
            this.imgVideoContent.GotFocus += new RoutedEventHandler(this.ImgVideoContent_GotFocus);
            this.imgVideoContent.LostFocus += new RoutedEventHandler(this.ImgVideoContent_LostFocus);
            this.imgVideoContent.KeyDown += new KeyEventHandler(this.ImgVideoContent_KeyDown);
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
        /// Gets or sets the control title.
        /// </summary>
        /// <value>
        /// Text title.
        /// </value>
        public string TextTitle
        {
            get
            {
                return this.txtMediaTitle;
            }

            set
            {
                this.txtMediaTitle = value;
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
            }
        }

        /// <summary>
        /// Gets or sets the Image source.
        /// </summary>
        /// <value>
        /// Video Image.
        /// </value>
        public string VideoImage
        {
            get
            {
                return this.videoImage;
            }

            set
            {
                this.videoImage = value;
            }
        }

        /// <summary>
        /// Gets or sets the video source.
        /// </summary>
        /// <value>
        /// Video source of the control.
        /// </value>
        public string VideoSource
        {
            get
            {
                return this.videoSource;
            }

            set
            {
                this.videoSource = value;
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Click event of the video image to trigger the open animation.
        /// </summary>
        private static void ImgVideoKeyDown()
        {
            StringBuilder uri;

            // If the PJD url is not absolute, create the absolute url by replacing Showcase folder with PJD directory name 
            if (!App.PatientJourneyDemonstratorDirectory.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                uri = new StringBuilder(App.ApplicationPath.Replace("Showcase/", string.Empty));
                uri.Append(App.PatientJourneyDemonstratorDirectory);
            }
            else
            {
                uri = new StringBuilder(App.PatientJourneyDemonstratorDirectory);
            }

            Uri navigateToUri = new Uri(uri.ToString());

            // Open the URL in new window
            System.Windows.Browser.HtmlPage.Window.Navigate(navigateToUri, "blank");
        }

        /// <summary>
        /// Click event of the video image to trigger the open animation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ImgVideo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ImgVideoKeyDown();
        }

        /// <summary>
        /// Video Image Content key down Event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Key event arguments.</param>
        private void ImgVideoContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ImgVideoKeyDown();
            }
        }

        /// <summary>
        /// Video Image Content lost focus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void ImgVideoContent_LostFocus(object sender, RoutedEventArgs e)
        {
            this.imgVideoContent.Opacity = 1;
        }

        /// <summary>
        /// Video Image Content got focus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Routed event arguments.</param>
        private void ImgVideoContent_GotFocus(object sender, RoutedEventArgs e)
        {
            this.imgVideoContent.Opacity = 0.5;
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
            uri.Append(this.videoImage);

            imgSource.UriSource = new Uri(uri.ToString());

            this.imgVideo.Source = imgSource;

            // Set the tool tip for the video image            
            ToolTipService.SetToolTip(this.imgVideo, this.imageToolTip);

            this.txtTitle.Text = this.TextTitle;

            this.PopulateMediaText();
        }

        /// <summary>
        /// Function populate the text section the control with correct formatting.
        /// </summary>
        private void PopulateMediaText()
        {
            double height = 0;
            string[] textArray = this.txtMediaText.Split('|');
            TextBlock txtBlock = null;

            this.textContainer.Children.Clear();

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

            this.textContainer.Height = height;

            if (this.textContainer.Height + 50 > 138)
            {
                this.LayoutRoot.Height = this.textContainer.Height + 50;
            }
        }
        #endregion
    }
}
