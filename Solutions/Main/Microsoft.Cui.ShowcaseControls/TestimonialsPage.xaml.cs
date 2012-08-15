//-----------------------------------------------------------------------
// <copyright file="TestimonialsPage.xaml.cs" company="Microsoft Corporation copyright 2008.">
// Copyright (c) Microsoft Corporation copyright 2008.
// All rights reserved.
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>16-Mar-2008</date>
// <summary>TestimonialsPage class.</summary>
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
    using Microsoft.Cui.ShowcaseControls;
    using System.Text;

    /// <summary>
    /// Showcase testimonials page hosting the media and download controls.
    /// </summary>
    public partial class TestimonialsPage : UserControl
    {
        #region Private Member variables

        /// <summary>
        /// The buffer between the real video position and the captured video position.
        /// </summary>
        private readonly int videoPositionBuffer = 250;

        /// <summary>
        /// List containing the showcase media control used in the page.
        /// </summary>
        private List<ShowcaseMediaControl> lstShowcaseMediaControl;

        /// <summary>
        /// Member variable to hold the media control for the page.
        /// </summary>
        private Microsoft.Cui.ShowcaseControls.ShowcaseMediaControl mediaControl;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor, initialize the instance of the class.
        /// </summary>       
        public TestimonialsPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(this.TestimonialsPage_Loaded);
            this.media.MediaFailed += new EventHandler<ExceptionRoutedEventArgs>(this.Media_MediaFailed);
            this.media.MediaEnded += new RoutedEventHandler(this.Media_MediaEnded);
        }            
        #endregion

        #region Private Methods

        /// <summary>
        /// Media Element failed event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>   
        private void Media_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            return;
        }

        /// <summary>
        /// Media Element ended event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>   
        private void Media_MediaEnded(object sender, RoutedEventArgs e)
        {
            Application.Current.Host.Content.IsFullScreen = false;
        }

        /// <summary>
        /// Event handling closing all the media controls.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CloseAllMedia(object sender, RoutedEventArgs e)
        {
            if (this.lstShowcaseMediaControl == null)
            {
                this.lstShowcaseMediaControl = new List<ShowcaseMediaControl>();
                foreach (object ctl in this.pnlRoot.Children)
                {
                    if (ctl.GetType().ToString() == "Microsoft.Cui.ShowcaseControls.ShowcaseMediaControl")
                    {
                        this.lstShowcaseMediaControl.Add((Microsoft.Cui.ShowcaseControls.ShowcaseMediaControl)ctl);
                    }
                }
            }

            // Cast the sender object to showcase media control
            this.mediaControl = (Microsoft.Cui.ShowcaseControls.ShowcaseMediaControl)sender;

            StringBuilder mediaSource;
            foreach (ShowcaseMediaControl mediaPage in this.lstShowcaseMediaControl)
            {
                if (mediaPage.IsOpen && mediaPage.Name != this.mediaControl.Name)
                {
                    mediaPage.CloseMediaControl();
                }
                else if (mediaPage.Name == this.mediaControl.Name)
                {
                    mediaSource = new StringBuilder(App.ApplicationPath);

                    // Set the media source
                    mediaSource.Append(mediaPage.MediaSource);
                    this.media.Source = new Uri(mediaSource.ToString());
                    this.media.Volume = mediaPage.Volume;
                    this.media.AutoPlay = true;
                    this.media.IsMuted = true;
                }
            }
        }

        /// <summary>
        /// Event onload of the control.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void TestimonialsPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Attach event to fullscreen change eventhandler
            Application.Current.Host.Content.FullScreenChanged += new EventHandler(this.Content_FullScreenChanged);

            // Hide the media and video brush responsible for making the media full screen
            this.media.Visibility = Visibility.Collapsed;
            this.rectVideoBrush.Visibility = Visibility.Collapsed;

            this.SetVideoSource();
        }

        /// <summary>
        /// Event handling the.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void Content_FullScreenChanged(object sender, EventArgs e)
        {
            if (Application.Current.Host.Content.IsFullScreen)
            {
                // Goto fullscreen mode
                // Check the visibility of the fullscreen media
                if (this.media.Visibility == Visibility.Visible)
                {
                    return;
                }

                // Make the fullscreen media and videobrush visible
                this.media.Visibility = Visibility.Visible;
                this.rectVideoBrush.Visibility = Visibility.Visible;

                // Set the height and width of the video brush rectangle to the host content height and width
                this.rectVideoBrush.Height = Application.Current.Host.Content.ActualHeight;
                this.rectVideoBrush.Width = Application.Current.Host.Content.ActualWidth;

                // Set the height and width of the media to the host content height and width
                this.media.Height = Application.Current.Host.Content.ActualHeight;
                this.media.Width = Application.Current.Host.Content.ActualWidth;

                // Set the container bacground color
                this.pnlRoot.Background = new SolidColorBrush(Colors.Black);

                // Set the fullscreenmedia position to the current video position of the showcase control
                if (this.mediaControl.PlayerCurrentState == MediaElementState.Paused)
                {
                    this.media.Position = this.mediaControl.VideoPosition;
                    this.media.Pause();
                }
                else if (this.mediaControl.PlayerCurrentState == MediaElementState.Stopped)
                {
                    this.media.Position = this.mediaControl.VideoPosition;
                    this.media.Stop();
                }
                else
                {
                    this.media.Position = this.mediaControl.VideoPosition.Add(new TimeSpan(0, 0, 0, 0, this.videoPositionBuffer));
                    this.media.Play();
                    this.media.IsMuted = false;
                    this.media.Volume = this.mediaControl.Volume;
                }

                this.ChangeMediaControlVisibility(Visibility.Collapsed);
            }
            else
            {
                // Return to normal mode from fullscreen mode
                // Check the visibility of the fullscreen media
                if (this.media.Visibility == Visibility.Collapsed)
                {
                    return;
                }

                // Unmute the mediaplayer video
                this.mediaControl.UNMutePlayer();
                this.media.IsMuted = true;

                this.media.Visibility = Visibility.Collapsed;
                this.rectVideoBrush.Visibility = Visibility.Collapsed;

                // Set the container bacground color
                this.pnlRoot.Background = new SolidColorBrush(Colors.White);

                this.ChangeMediaControlVisibility(Visibility.Visible);
            }
        }

        /// <summary>
        /// Change the visibility status on the media control in the page.
        /// </summary>
        /// <param name="status">Control visibiliyu status.</param>
        private void ChangeMediaControlVisibility(Visibility status)
        {
            foreach (object ctl in this.pnlRoot.Children)
            {
                if (ctl.GetType().ToString() == "Microsoft.Cui.ShowcaseControls.ShowcaseMediaControl")
                {
                    Microsoft.Cui.ShowcaseControls.ShowcaseMediaControl mediaPage = null;
                    mediaPage = (Microsoft.Cui.ShowcaseControls.ShowcaseMediaControl)ctl;
                    mediaPage.Visibility = status;
                }
                else if (ctl.GetType().ToString() == "Microsoft.Cui.ShowcaseControls.DownloadMediaControl")
                {
                    Microsoft.Cui.ShowcaseControls.DownloadMediaControl downloadPage = null;
                    downloadPage = (Microsoft.Cui.ShowcaseControls.DownloadMediaControl)ctl;
                    downloadPage.Visibility = status;
                }
                else if (ctl.GetType().ToString() == "System.Windows.Shapes.Rectangle")
                {
                    Rectangle fillerRect = (Rectangle)ctl;
                    if (fillerRect.Name.Contains("filler"))
                    {
                        fillerRect.Visibility = status;
                    }
                }
            }
        }

        /// <summary>
        /// Set the video source for the Showcase Media Control objects.
        /// </summary>
        private void SetVideoSource()
        {
            this.testimonials01.MediaSource = App.GetVideoSource("testimonials01");
            this.testimonials02.MediaSource = App.GetVideoSource("testimonials02");
            this.testimonials03.MediaSource = App.GetVideoSource("testimonials03");
            this.testimonials04.MediaSource = App.GetVideoSource("testimonials04");
            this.testimonials05.MediaSource = App.GetVideoSource("testimonials05");
            this.testimonials06.MediaSource = App.GetVideoSource("testimonials06");
        }
        #endregion
    }
}
