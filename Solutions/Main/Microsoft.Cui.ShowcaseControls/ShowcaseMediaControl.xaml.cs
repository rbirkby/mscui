//-----------------------------------------------------------------------
// <copyright file="ShowcaseMediaControl.xaml.cs" company="Microsoft Corporation copyright 2008.">
// Copyright (c) Microsoft Corporation copyright 2008.
// All rights reserved.
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>16-Mar-2008</date>
// <summary>ShowcaseMediaControl class.</summary>
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
    /// ShowcaseMediaControl encapsulating the media, text and animation.
    /// </summary>
    public partial class ShowcaseMediaControl : UserControl
    {
        #region Private Member variables
        /// <summary>
        /// Member variable to hold control title.
        /// </summary>
        private string txtMediaTitle;

        /// <summary>
        /// Member variable to hold description text for the control.
        /// </summary>
        private string txtMediaText;

        /// <summary>
        /// Member variable to hold the open status of the control.
        /// </summary>
        private bool isopen;

        /// <summary>
        /// Member variable to hold the video image source. 
        /// </summary>
        private string videoImage;

        /// <summary>
        /// Member variable to hold the media source.
        /// </summary>
        private string mediaSource;

        /// <summary>
        /// Member variable to hold the video position of the media element.
        /// </summary>
        private TimeSpan videoPosition;

        /// <summary>
        /// Member variable to hold the tooltip text for the image.
        /// </summary>
        private string imageToolTip;

        /// <summary>
        /// Member variable to hold the hyperlink text of the control.
        /// </summary>
        private string hyperlinkText;

        /// <summary>
        /// Member variable to hold the full screen status.
        /// </summary>
        private bool allowFullScreen = true;

        /// <summary>
        /// Member variable to hold the play mode.
        /// </summary>
        private string playMode = "LARGE";

        /// <summary>
        /// Player volume level.
        /// </summary>
        private double volume;

        /// <summary>
        /// Media element current state.
        /// </summary>
        private MediaElementState playerCurrentState;

        /// <summary>
        /// Media Player.
        /// </summary>
        private Player showcaseMediaPlayer;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor, initialize the instance of the class.
        /// </summary>
        public ShowcaseMediaControl()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(this.ShowcaseMediaControl_Loaded);

            this.closeVideo.Completed += new EventHandler(this.CloseVideo_Completed);
            this.openVideo.Completed += new EventHandler(this.OpenVideo_Completed);
            this.imgVideo.MouseLeftButtonDown += new MouseButtonEventHandler(this.ImgVideo_MouseLeftButtonDown);

            this.imgVideoContent.GotFocus += new RoutedEventHandler(this.ImgVideoContent_GotFocus);
            this.imgVideoContent.LostFocus += new RoutedEventHandler(this.ImgVideoContent_LostFocus);
            this.imgVideoContent.KeyDown += new KeyEventHandler(this.ImgVideoContent_KeyDown);
        }

        #endregion

        #region Event Handler
        /// <summary>
        /// Routed even handler for attaching event at it's parent for triggering the close control event.
        /// </summary>        
        public event RoutedEventHandler CloseControlEventHandler;
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the Image Tool Tip value.
        /// </summary>
        /// <value>
        /// Image tooltip text.
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
        /// Gets or sets a value indicating whether running status of the control is set or not.
        /// </summary>
        /// <value>
        /// Open status of the control.
        /// </value>
        public bool IsOpen
        {
            get
            {
                return this.isopen;
            }

            set
            {
                this.isopen = value;
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
        /// Gets or sets the video inage source.
        /// </summary>
        /// <value>
        /// Image source.
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
        /// Gets or sets the media source.
        /// </summary>
        /// <value>
        /// Media source.
        /// </value>
        public string MediaSource
        {
            get
            {
                return this.mediaSource;
            }

            set
            {
                this.mediaSource = value;
            }
        }

        /// <summary>
        /// Gets or sets the position of the media element.
        /// </summary>
        /// <value>
        /// Media play position.
        /// </value>
        public TimeSpan VideoPosition
        {
            get
            {
                return this.videoPosition;
            }

            set
            {
                this.videoPosition = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the fullscreen status of the media element.
        /// </summary>
        /// <value>
        /// Allow full screen status.
        /// </value>
        public bool AllowFullScreen
        {
            set
            {
                this.allowFullScreen = value;
            }

            get
            {
                return this.allowFullScreen;
            }
        }

        /// <summary>
        /// Gets or sets the play mode.
        /// </summary>
        /// <value>
        /// Play mode.
        /// </value>
        public string PlayMode
        {
            get
            {
                return this.playMode;
            }

            set
            {
                this.playMode = value;
            }
        }

        /// <summary>
        /// Gets the media player volume.
        /// </summary>
        /// <value>
        /// Volume level of the player.
        /// </value>
        public double Volume
        {
            get
            {
                return this.volume;
            }
        }

        /// <summary>
        /// Gets Media element current state.
        /// </summary>
        /// <value>
        /// Media element current stat.
        /// </value>
        public MediaElementState PlayerCurrentState
        {
            get
            {
                return this.playerCurrentState;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Trigger the animation in the media player.
        /// </summary>
        public void TriggerAnimation()
        {
            this.showcaseMediaPlayer.StopVideo();
            this.showcaseMediaPlayer.PlayVideo(new TimeSpan(0, 0, 0));

            // Start the animation to open the media player
            openVideo.Begin();
            openVideo.AutoReverse = false;

            // Set the open status of the control
            this.isopen = true;
        }

        /// <summary>
        /// Event triggering the close animation.
        /// </summary>
        public void CloseMediaControl()
        {
            this.showcaseMediaPlayer.MuteVideo();
            closeVideo.Begin();
            this.isopen = false;
        }

        /// <summary>
        /// Event start playing the media element.
        /// </summary>
        /// <param name="time">Video position.</param>
        public void PlayVideo(TimeSpan time)
        {
            this.showcaseMediaPlayer.PlayVideo(time);
        }

        /// <summary>
        /// Unmute the mediaplayer video.
        /// </summary>
        public void UNMutePlayer()
        {
            this.showcaseMediaPlayer.UNMutePlayer();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Event handle the close event of the control.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ClosePlayerControl(object sender, EventArgs e)
        {
            closeVideo.Begin();
            this.isopen = false;
        }

        /// <summary>
        /// Event handling the fullscreen change event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>>
        private void FullscreenChange(object sender, EventArgs e)
        {
            Application.Current.Host.Content.IsFullScreen = true;
            this.videoPosition = this.showcaseMediaPlayer.VideoPosition;
            this.volume = this.showcaseMediaPlayer.Volume;
            this.playerCurrentState = this.showcaseMediaPlayer.PlayerCurrentState;
        }

        /// <summary>
        /// Event onload of the control.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ShowcaseMediaControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the image source of the control
            System.Windows.Media.Imaging.BitmapImage imgSource = new System.Windows.Media.Imaging.BitmapImage();

            StringBuilder uri = new StringBuilder(App.ApplicationPath);
            uri.Append("Showcase/images/");
            uri.Append(this.videoImage);

            imgSource.UriSource = new Uri(uri.ToString(), UriKind.RelativeOrAbsolute);
            imgVideo.Source = imgSource;

            // Set the tool tip for the video image            
            ToolTipService.SetToolTip(imgVideo, this.imageToolTip);

            this.txtTitle.Text = this.TextTitle;

            this.PopulateMediaText();

            forwardLayOutAnimation.KeyFrames[0].Value = LayoutRoot.Height;
            forwardLayOutAnimation.KeyFrames[1].Value = LayoutRoot.Height;

            if (LayoutRoot.Height <= 400)
            {
                forwardLayOutAnimation.KeyFrames[2].Value = 400;
            }
            else
            {
                forwardLayOutAnimation.KeyFrames[2].Value = LayoutRoot.Height;
            }

            reverseLayOutAnimation.KeyFrames[0].Value = LayoutRoot.Height;
        }

        /// <summary>
        /// Click event of the video image to trigger the open animation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ImgVideo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ImgVideoKeyDown();
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
                this.ImgVideoKeyDown();
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
        /// Click event of the video image to trigger the open animation.
        /// </summary>
        private void ImgVideoKeyDown()
        {
            if (!(imgVideo.Opacity > 0))
            {
                return;
            }

            this.showcaseMediaPlayer = new Player();
            this.showcaseMediaPlayer.Name = "showcaseMediaPlayer";
            this.showcaseMediaPlayer.ControlHeight = 117;
            this.showcaseMediaPlayer.ControlWidth = 154;

            this.mediaCanvas.Children.Add(this.showcaseMediaPlayer);

            this.showcaseMediaPlayer.CloseEventHandler += new RoutedEventHandler(this.ClosePlayerControl);
            this.showcaseMediaPlayer.FullScreenEventHandler += new RoutedEventHandler(this.FullscreenChange);

            // Set the attributes of the media control
            this.showcaseMediaPlayer.AllowFullScreen = this.AllowFullScreen;
            this.showcaseMediaPlayer.PlayMode = this.playMode;
            this.showcaseMediaPlayer.MediaSource = this.mediaSource;

            this.showcaseMediaPlayer.SetMediaSource();
            this.showcaseMediaPlayer.PlayVideo(new TimeSpan(0, 0, 0));

            // Start the animation to open the media player
            openVideo.Begin();
            openVideo.AutoReverse = false;

            // Set the open status of the control
            this.isopen = true;

            // Triger the close event to close the other controls in the host control           
            if (this.CloseControlEventHandler != null)
            {
                this.CloseControlEventHandler.Invoke(this, null);
            }
        }

        /// <summary>
        /// Event triggered on completion of the close animation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CloseVideo_Completed(object sender, EventArgs e)
        {
            this.showcaseMediaPlayer.StopVideo();
            this.imgVideo.Visibility = Visibility.Visible;
            this.mediaCanvas.Children.Remove(this.showcaseMediaPlayer);
            this.imgVideoContent.Focus();
        }

        /// <summary>
        /// Event handle the opening the media control.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OpenVideo_Completed(object sender, EventArgs e)
        {
            imgVideo.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Populate the text section the control with correct formatting.
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
            int noofLines = 0;

            noofLines = textArray[0].Length / 75;
            if ((textArray[0].Length - noofLines * 75) > 15)
            {
                noofLines += 1;
            }

            txtBlock.Height = noofLines * 15;

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
        /// <param name="height">Top location of the hyperlink in canvas.</param>
        private void PopulateHyperlink(double height)
        {
            if (this.hyperlinkText != null && !string.IsNullOrEmpty(this.hyperlinkText))
            {
                // Parse the heperlink text and create the hyperlinks
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

                    // Add the hyperlinks to the container
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
        /// Click event of the hyperlinks.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void Hyperlink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock txtBlock = (TextBlock)sender;
            if (txtBlock.Tag != null && !string.IsNullOrEmpty(txtBlock.Tag.ToString()))
            {
                // Open the URL in same window
                System.Windows.Browser.HtmlPage.Window.Navigate(new Uri(txtBlock.Tag.ToString()));
            }
        }
        #endregion
    }
}
