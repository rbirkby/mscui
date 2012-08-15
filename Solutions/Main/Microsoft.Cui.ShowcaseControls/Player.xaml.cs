//-----------------------------------------------------------------------
// <copyright file="Player.xaml.cs" company="Microsoft Corporation copyright 2008.">
// Copyright (c) Microsoft Corporation copyright 2008.
// All rights reserved.
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>16-Mar-2008</date>
// <summary>Player class.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.ShowcaseControls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.Collections.Generic;
    using System.Net;
    using System.Windows.Markup;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Media Player control.
    /// </summary>
    public partial class Player : UserControl
    {
        #region fields
        /// <summary>
        /// Player Height.
        /// </summary>        
        private double height;

        /// <summary>
        /// Player Volume before Mute.
        /// </summary>        
        private double tempVolume = 0.4;

        /// <summary>
        /// Player FullScreenmode.
        /// </summary>        
        private bool allowFullScreen;

        /// <summary>
        /// Player Width.
        /// </summary>        
        private double width;

        /// <summary>
        /// Player Source.
        /// </summary>        
        private string mediaSource;

        /// <summary>
        /// Player Position.
        /// </summary> 
        private TimeSpan videoPosition;

        /// <summary>
        /// Play mode for the media element.
        /// </summary>
        private string playMode;

        /// <summary>
        /// Media element volume level.
        /// </summary>
        private double volume;

        /// <summary>
        /// Media Player CurrentState..
        /// </summary>
        private MediaElementState playerCurrentState;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new Player.
        /// </summary>
        public Player()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(this.Player_Loaded);

            this.thePlayer.DownloadProgressChanged += new RoutedEventHandler(this.ThePlayer_DownloadProgressChanged);

            this.BtnPlay.MouseLeftButtonDown += new MouseButtonEventHandler(this.BtnPlay_MouseLeftButtonDown);
            this.BtnPlay.MouseEnter += new MouseEventHandler(this.BtnPlay_MouseEnter);
            this.BtnPlay.MouseLeave += new MouseEventHandler(this.BtnPlay_MouseLeave);

            this.BtnPause.MouseLeftButtonDown += new MouseButtonEventHandler(this.BtnPause_MouseLeftButtonDown);
            this.BtnPause.MouseEnter += new MouseEventHandler(this.BtnPause_MouseEnter);
            this.BtnPause.MouseLeave += new MouseEventHandler(this.BtnPause_MouseLeave);

            this.BtnStop.MouseLeftButtonDown += new MouseButtonEventHandler(this.BtnStop_MouseLeftButtonDown);
            this.BtnStop.MouseEnter += new MouseEventHandler(this.BtnStop_MouseEnter);
            this.BtnStop.MouseLeave += new MouseEventHandler(this.BtnStop_MouseLeave);

            this.BtnClose.MouseLeftButtonDown += new MouseButtonEventHandler(this.BtnClose_MouseLeftButtonDown);
            this.BtnClose.MouseEnter += new MouseEventHandler(this.BtnClose_MouseEnter);
            this.BtnClose.MouseLeave += new MouseEventHandler(this.BtnClose_MouseLeave);

            this.BtnMute.MouseLeftButtonDown += new MouseButtonEventHandler(this.BtnMute_MouseLeftButtonDown);
            this.BtnMute.MouseEnter += new MouseEventHandler(this.BtnMute_MouseEnter);
            this.BtnMute.MouseLeave += new MouseEventHandler(this.BtnMute_MouseLeave);

            this.BtnMuted.MouseEnter += new MouseEventHandler(this.BtnMuted_MouseEnter);
            this.BtnMuted.MouseLeave += new MouseEventHandler(this.BtnMuted_MouseLeave);

            this.BtnFullScreen.MouseLeftButtonDown += new MouseButtonEventHandler(this.BtnFullScreen_MouseLeftButtonDown);
            this.BtnFullScreen.MouseEnter += new MouseEventHandler(this.BtnFullScreen_MouseEnter);
            this.BtnFullScreen.MouseLeave += new MouseEventHandler(this.BtnFullScreen_MouseLeave);

            this.BtnPrev.MouseLeftButtonDown += new MouseButtonEventHandler(this.BtnPrev_MouseLeftButtonDown);
            this.BtnPrev.MouseEnter += new MouseEventHandler(this.BtnPrev_MouseEnter);
            this.BtnPrev.MouseLeave += new MouseEventHandler(this.BtnPrev_MouseLeave);

            this.BtnNext.MouseLeftButtonDown += new MouseButtonEventHandler(this.BtnNext_MouseLeftButtonDown);
            this.BtnNext.MouseEnter += new MouseEventHandler(this.BtnNext_MouseEnter);
            this.BtnNext.MouseLeave += new MouseEventHandler(this.BtnNext_MouseLeave);

            this.PlaySlider.Minimum = 0;
            this.PlaySlider.Maximum = 100;
            this.PlaySlider.Value = 0;
            this.PlaySlider.MouseLeftButtonUp += new MouseButtonEventHandler(this.PlaySlider_MouseLeftButtonUp);
            this.PlaySlider.MouseLeftButtonDown += new MouseButtonEventHandler(this.PlaySlider_MouseLeftButtonDown);
            this.thePlayer.Stretch = Stretch.Uniform;

            this.VolumeSlider.Minimum = 0;
            this.VolumeSlider.Maximum = 1;
            this.VolumeSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.VolumeSlider_ValueChanged);

            this.Timer.Completed += new EventHandler(this.Timer_Completed);

            this.thePlayer.MediaFailed += new EventHandler<ExceptionRoutedEventArgs>(this.ThePlayer_MediaFailed);

            //// Focus change event handler
            this.BtnClose.LostFocus += new RoutedEventHandler(this.BtnClose_LostFocus);
            this.BtnClose.GotFocus += new RoutedEventHandler(this.BtnClose_GotFocus);
            this.BtnClose.KeyDown += new KeyEventHandler(this.BtnClose_KeyDown);

            this.BtnFullScreen.GotFocus += new RoutedEventHandler(this.BtnFullScreen_GotFocus);
            this.BtnFullScreen.LostFocus += new RoutedEventHandler(this.BtnFullScreen_LostFocus);
            this.BtnFullScreen.KeyDown += new KeyEventHandler(this.BtnFullScreen_KeyDown);

            this.BtnPlay.GotFocus += new RoutedEventHandler(this.BtnPlay_GotFocus);
            this.BtnPlay.LostFocus += new RoutedEventHandler(this.BtnPlay_LostFocus);
            this.BtnPlay.KeyDown += new KeyEventHandler(this.BtnPlay_KeyDown);

            this.BtnPause.GotFocus += new RoutedEventHandler(this.BtnPause_GotFocus);
            this.BtnPause.LostFocus += new RoutedEventHandler(this.BtnPause_LostFocus);
            this.BtnPause.KeyDown += new KeyEventHandler(this.BtnPause_KeyDown);

            this.BtnPrev.GotFocus += new RoutedEventHandler(this.BtnPrev_GotFocus);
            this.BtnPrev.LostFocus += new RoutedEventHandler(this.BtnPrev_LostFocus);
            this.BtnPrev.KeyDown += new KeyEventHandler(this.BtnPrev_KeyDown);

            this.BtnNext.GotFocus += new RoutedEventHandler(this.BtnNext_GotFocus);
            this.BtnNext.LostFocus += new RoutedEventHandler(this.BtnNext_LostFocus);
            this.BtnNext.KeyDown += new KeyEventHandler(this.BtnNext_KeyDown);

            this.BtnMuted.GotFocus += new RoutedEventHandler(this.BtnMuted_GotFocus);
            this.BtnMuted.LostFocus += new RoutedEventHandler(this.BtnMuted_LostFocus);

            this.BtnMute.GotFocus += new RoutedEventHandler(this.BtnMute_GotFocus);
            this.BtnMute.LostFocus += new RoutedEventHandler(this.BtnMute_LostFocus);
            this.BtnMute.KeyDown += new KeyEventHandler(this.BtnMute_KeyDown);

            this.BtnStop.GotFocus += new RoutedEventHandler(this.BtnStop_GotFocus);
            this.BtnStop.LostFocus += new RoutedEventHandler(this.BtnStop_LostFocus);
            this.BtnStop.KeyDown += new KeyEventHandler(this.BtnStop_KeyDown);

            //// Focus the pause button on load
            this.BtnPause.Focus();
            this.BtnPause.Opacity = 0.75;
            this.gradient.Opacity = .6;
        }

        #endregion

        #region Event Handler
        /// <summary>
        /// Routed even handler for attaching event at it's parent Handles the full screen mode changed event of the combobox.
        /// </summary>        
        public event RoutedEventHandler FullScreenEventHandler;

        /// <summary>
        /// Routed even handler for attaching event at it's parent Handles the player close button clicked event of the combobox.
        /// </summary>        
        public event RoutedEventHandler CloseEventHandler;

        #endregion

        #region Properties and variables

        /// <summary>
        /// Gets or sets a value indicating whether the AllowFullScreen is set.
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
        /// Gets or sets the Height.
        /// </summary>
        /// <value>
        /// Height of the control.
        /// </value>
        public double ControlHeight
        {
            set
            {
                this.height = value;
            }

            get
            {
                return this.height;
            }
        }

        /// <summary>
        /// Gets or sets the TempVolume.
        /// </summary>
        /// <value>
        /// TempVolume of the control.
        /// </value>
        public double TempVolume
        {
            set
            {
                this.tempVolume = value;
            }

            get
            {
                return this.tempVolume;
            }
        }

        /// <summary>
        /// Gets or sets the Width.
        /// </summary>
        /// <value>
        /// Control width.
        /// </value>
        public double ControlWidth
        {
            set
            {
                this.width = value;
            }

            get
            {
                return this.width;
            }
        }

        /// <summary>
        /// Gets or sets the Source.
        /// </summary>
        /// <value>
        /// Media source.
        /// </value>
        public string MediaSource
        {
            set
            {
                this.mediaSource = value;
            }

            get
            {
                return this.mediaSource;
            }
        }

        /// <summary>
        /// Gets or sets the Seek Position.
        /// </summary>  
        /// <value>
        /// Video time position.
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
        /// Gets Media Player CurrentState.
        /// </summary>
        /// <value>
        /// Media Player CurrentState.
        /// </value>
        public MediaElementState PlayerCurrentState
        {
            get
            {
                return this.playerCurrentState;
            }
        }
        #endregion

        #region Methods

        #region Other Method

        /// <summary>
        /// Set the media source of the player.
        /// </summary>
        public void SetMediaSource()
        {
            StringBuilder uri = new StringBuilder(App.ApplicationPath);
            uri.Append(this.mediaSource);
            this.thePlayer.Source = new Uri(uri.ToString(), UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Unmute the video.
        /// </summary>
        public void UNMuteVideo()
        {
            this.BtnMute.Opacity = 1;
            this.BtnMuted.Opacity = 0;
            this.VolumeSlider.Value = this.TempVolume;
        }

        /// <summary>
        /// Unmute the mediaplayer video.
        /// </summary>
        public void UNMutePlayer()
        {
            this.thePlayer.IsMuted = false;
            if (this.thePlayer.Volume == 0)
            {
                this.BtnMute.Opacity = .000001;
                this.BtnMuted.Opacity = 1;
            }
        }

        /// <summary>
        /// Handles the play functionality of the Media Player.
        /// </summary>
        /// <param name="time">Video position.</param>
        public void PlayVideo(TimeSpan time)
        {
            ////Handle video resolution/ aspect ratio
            if (this.PlayMode == "SMALL")
            {
                this.thePlayer.Stretch = Stretch.None;
            }
            else
            {
                this.thePlayer.Stretch = Stretch.Uniform;
            }

            //// Handle video fullscreen allowed/ dis-allowed
            if (!this.AllowFullScreen)
            {
                this.BtnFullScreen.Visibility = Visibility.Collapsed;
            }

            //// Play the video from the current position if the media element is in playing state else wait
            if (this.thePlayer.CurrentState == MediaElementState.Closed || this.thePlayer.CurrentState == MediaElementState.Opening)
            {
                Timer.Begin();
            }
            else
            {
                thePlayer.Stop();
                thePlayer.Position = time;
                thePlayer.Play();
                Timer.Begin();
            }

            this.BtnPlay.Visibility = Visibility.Collapsed;
            this.BtnPause.Visibility = Visibility.Visible;
            this.UNMuteVideo();

            this.BtnPause.Focus();
        }

        /// <summary>
        /// Handles the mute functionality of the Media Player.
        /// </summary>
        public void MuteVideo()
        {
            if (this.BtnMuted.Opacity == 1)
            {
                this.BtnMute.Opacity = 1;
                this.BtnMuted.Opacity = 0;
                this.VolumeSlider.Value = this.TempVolume;

                // Set the tool tip for the video image            
                ToolTipService.SetToolTip(this.BtnMute, "Mute");
            }
            else
            {
                this.TempVolume = this.VolumeSlider.Value;
                this.VolumeSlider.Value = 0;
                this.BtnMute.Opacity = .000001;
                this.BtnMuted.Opacity = 1;

                // Set the tool tip for the video image            
                ToolTipService.SetToolTip(this.BtnMute, "Sound");
            }
        }

        /// <summary>
        /// Handles the stop functionality of the Media Player.
        /// </summary>
        public void StopVideo()
        {
            // Stop the media if it is playing
            if (thePlayer.CurrentState == MediaElementState.Playing || thePlayer.CurrentState == MediaElementState.Paused)
            {
                thePlayer.Stop();
                Timer.Stop();
                PlaySlider.Value = 0;
                this.statusText.Text = "0:0:0" + " / " + thePlayer.NaturalDuration.TimeSpan.Hours.ToString(CultureInfo.InvariantCulture) + ":" + thePlayer.NaturalDuration.TimeSpan.Minutes.ToString(CultureInfo.InvariantCulture) + ":" + thePlayer.NaturalDuration.TimeSpan.Seconds.ToString(CultureInfo.InvariantCulture);
                this.BtnPause.Visibility = Visibility.Collapsed;
                this.BtnPlay.Visibility = Visibility.Visible;

                this.BtnPlay.Focus();
            }
        }

        /// <summary>
        /// Event Handler for Load of the Media Player.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>        
        private void Player_Loaded(object sender, RoutedEventArgs e)
        {
            ////Resize the player using the resize animation
            LayoutScaleX.KeyFrames[0].Value = this.width / LayoutRoot.Width;
            LayoutScaleY.KeyFrames[0].Value = this.height / LayoutRoot.Height;
            this.Resize.Begin();
        }

        /// <summary>
        /// Media Element failed event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>   
        private void ThePlayer_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            return;
        }

        #endregion

        #region Mouse Leave and Enter Methods

        //// Btn Mute

        /// <summary>
        /// Event Handler for Mouse Leave on Mute Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnMute_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.BtnMute.Opacity == 0.75)
            {
                this.BtnMute.Opacity = 1;
                this.gradient_Mute.Opacity = 0;
            }
        }

        /// <summary>
        /// Button lost focus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnMute_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.BtnMute.Opacity == 0.75)
            {
                this.BtnMute.Opacity = 1;
                this.gradient_Mute.Opacity = 0;
            }
        }

        /// <summary>
        /// Event Handler for Mouse Enter on Mute Button.
        /// </summary>     
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnMute_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.BtnMute.Opacity == 1)
            {
                this.BtnMute.Opacity = 0.75;
                this.gradient_Mute.Opacity = 0.6;
            }
        }

        /// <summary>
        /// Button gotfocus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnMute_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.BtnMute.Opacity == 1)
            {
                this.BtnMute.Opacity = 0.75;
                this.gradient_Mute.Opacity = 0.6;
            }
        }

        //// Btn Muted

        /// <summary>
        /// Event Handler for Mouse Leave on Muted Button.
        /// </summary>     
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnMuted_MouseLeave(object sender, MouseEventArgs e)
        {
            this.BtnMute.Opacity = 1;
            this.gradient_Mute.Opacity = 0;
        }

        /// <summary>
        /// Button lost focus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnMuted_LostFocus(object sender, RoutedEventArgs e)
        {
            this.BtnMute.Opacity = 1;
            this.gradient_Mute.Opacity = 0;
        }

        /// <summary>
        /// Event Handler for Mouse Enter on Muted Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnMuted_MouseEnter(object sender, MouseEventArgs e)
        {
            this.BtnMuted.Opacity = 0.75;
            this.gradient_Mute.Opacity = 0.6;
        }

        /// <summary>
        /// Button gotfocus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnMuted_GotFocus(object sender, RoutedEventArgs e)
        {
            this.BtnMuted.Opacity = 0.75;
            this.gradient_Mute.Opacity = 0.6;
        }

        //// Btn Pause

        /// <summary>
        /// Event Handler for Mouse Leave on Pause Button.
        /// </summary>  
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPause_MouseLeave(object sender, MouseEventArgs e)
        {
            this.BtnPause.Opacity = 1;
            this.gradient.Opacity = 0;
        }

        /// <summary>
        /// Button lost focus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPause_LostFocus(object sender, RoutedEventArgs e)
        {
            this.BtnPause.Opacity = 1;
            this.gradient.Opacity = 0;
        }

        /// <summary>
        /// Event Handler for Mouse Enter on Pause Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPause_MouseEnter(object sender, MouseEventArgs e)
        {
            this.BtnPause.Opacity = 0.75;
            this.gradient.Opacity = .6;
        }

        /// <summary>
        /// Button gotfocus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPause_GotFocus(object sender, RoutedEventArgs e)
        {
            this.BtnPause.Opacity = 0.75;
            this.gradient.Opacity = .6;
        }

        //// Btn Play

        /// <summary>
        /// Event Handler for Mouse Leave on Play Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPlay_MouseLeave(object sender, MouseEventArgs e)
        {
            this.BtnPlay.Opacity = 1;
            this.gradient_stop.Opacity = 0;
        }

        /// <summary>
        /// Button lost focus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPlay_LostFocus(object sender, RoutedEventArgs e)
        {
            this.BtnPlay.Opacity = 1;
            this.gradient_stop.Opacity = 0;
        }

        /// <summary>
        /// Event Handler for Mouse Enter on Play Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPlay_MouseEnter(object sender, MouseEventArgs e)
        {
            this.BtnPlay.Opacity = 0.75;
            this.gradient.Opacity = 0.6;
        }

        /// <summary>
        /// Button gotfocus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPlay_GotFocus(object sender, RoutedEventArgs e)
        {
            this.BtnPlay.Opacity = 0.75;
            this.gradient.Opacity = 0.6;
        }

        //// Btn Stop

        /// <summary>
        /// Event Handler for Mouse Leave on Stop Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnStop_MouseLeave(object sender, MouseEventArgs e)
        {
            this.BtnStop.Opacity = 1;
            this.gradient_stop.Opacity = 0;
        }

        /// <summary>
        /// Button lost focus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnStop_LostFocus(object sender, RoutedEventArgs e)
        {
            this.BtnStop.Opacity = 1;
            this.gradient_stop.Opacity = 0;
        }

        /// <summary>
        /// Event Handler for Mouse Enter on Stop Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnStop_MouseEnter(object sender, MouseEventArgs e)
        {
            this.BtnStop.Opacity = 0.75;
            this.gradient_stop.Opacity = 0.6;
        }

        /// <summary>
        /// Button gotfocus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnStop_GotFocus(object sender, RoutedEventArgs e)
        {
            this.BtnStop.Opacity = 0.75;
            this.gradient_stop.Opacity = 0.6;
        }

        //// Btn Next

        /// <summary>
        /// Event Handler for Mouse Leave on Next Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnNext_MouseLeave(object sender, MouseEventArgs e)
        {
            this.BtnNext.Opacity = 1;
            gradient_Next.Opacity = 0;
        }

        /// <summary>
        /// Button lost focus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnNext_LostFocus(object sender, RoutedEventArgs e)
        {
            this.BtnNext.Opacity = 1;
            gradient_Next.Opacity = 0;
        }

        /// <summary>
        /// Event Handler for Mouse Enter on Next Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnNext_MouseEnter(object sender, MouseEventArgs e)
        {
            this.BtnNext.Opacity = 0.75;
            gradient_Next.Opacity = 0.6;
        }

        /// <summary>
        /// Button gotfocus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnNext_GotFocus(object sender, RoutedEventArgs e)
        {
            this.BtnNext.Opacity = 0.75;
            gradient_Next.Opacity = 0.6;
        }

        //// Btn Prev

        /// <summary>
        /// Event Handler for Mouse Leave on Previous Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPrev_MouseLeave(object sender, MouseEventArgs e)
        {
            this.BtnPrev.Opacity = 1;
            gradient_Prev.Opacity = 0;
        }

        /// <summary>
        /// Button lost focus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPrev_LostFocus(object sender, RoutedEventArgs e)
        {
            this.BtnPrev.Opacity = 1;
            gradient_Prev.Opacity = 0;
        }

        /// <summary>
        /// Event Handler for Mouse Enter on Previous Button.
        /// </summary>
        ///  <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPrev_MouseEnter(object sender, MouseEventArgs e)
        {
            this.BtnPrev.Opacity = 0.75;
            gradient_Prev.Opacity = 0.6;
        }

        /// <summary>
        /// Button gotfocus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPrev_GotFocus(object sender, RoutedEventArgs e)
        {
            this.BtnPrev.Opacity = 0.75;
            gradient_Prev.Opacity = 0.6;
        }

        //// Btn Fullscreen

        /// <summary>
        /// Event Handler for Mouse Leave on Full Screen Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnFullScreen_MouseLeave(object sender, MouseEventArgs e)
        {
            this.BtnFullScreen.Opacity = 1;
            gradient_Fullscreen.Opacity = 0;
        }

        /// <summary>
        /// Button lost focus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnFullScreen_LostFocus(object sender, RoutedEventArgs e)
        {
            this.BtnFullScreen.Opacity = 1;
            gradient_Fullscreen.Opacity = 0;
        }

        /// <summary>
        /// Event Handler for Mouse Enter on Full Screen Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnFullScreen_MouseEnter(object sender, MouseEventArgs e)
        {
            this.BtnFullScreen.Opacity = 0.75;
            gradient_Fullscreen.Opacity = 0.6;
        }

        /// <summary>
        /// Button gotfocus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnFullScreen_GotFocus(object sender, RoutedEventArgs e)
        {
            this.BtnFullScreen.Opacity = 0.75;
            gradient_Fullscreen.Opacity = 0.6;
        }

        //// Btn Close

        /// <summary>
        /// Event Handler for Mouse Leave on Close Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            this.BtnClose.Opacity = 1;
            gradient_Close.Opacity = 0;
        }

        /// <summary>
        /// Button lost focus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnClose_LostFocus(object sender, RoutedEventArgs e)
        {
            this.BtnClose.Opacity = 1;
            this.gradient_Close.Opacity = 0;
        }

        /// <summary>
        /// Event Handler for Mouse Enter on Close Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            this.BtnClose.Opacity = 0.75;
            gradient_Close.Opacity = 0.6;
        }

        /// <summary>
        /// Button gotfocus event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnClose_GotFocus(object sender, RoutedEventArgs e)
        {
            this.BtnClose.Opacity = 0.75;
            this.gradient_Close.Opacity = 0.6;
        }

        #endregion

        #region Mouse Left button down

        //// Mouse left button down events

        /// <summary>
        /// Event Handler for MouseLeftButtonDown on Close Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnClose_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.BtnCloseKeyDown(sender);
        }

        /// <summary>
        /// Event Handler for MouseLeftButtonDown on Full Screen Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnFullScreen_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.BtnFullScreenKeyDown(sender);
        }

        /// <summary>
        /// Event Handler for MouseLeftButtonDown on Next Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnNext_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.BtnNextKeyDown();
        }

        /// <summary>
        /// Event Handler for MouseLeftButtonDown on Previous Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPrev_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.BtnPrevKeyDown();
        }

        /// <summary>
        /// Event Handler for MouseLeftButtonDown on Mute Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnMute_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.MuteVideo();
        }

        /// <summary>
        /// Event Handler for MouseLeftButtonDown on Stop Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnStop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.StopVideo();
        }

        /// <summary>
        /// Event Handler for MouseLeftButtonDown on Pause Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPause_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.BtnPauseKeyDown();
        }

        /// <summary>
        /// Event Handler for MouseLeftButtonDown on Play Button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPlay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.BtnPlayKeyDown();
            this.BtnPause.Focus();
        }

        #endregion

        #region Rest of the medthods

        /// <summary>
        /// Event Handler for ValueChanged of the Volume Slider.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.BtnMuted.Opacity == 1)
            {
                this.VolumeSlider.Value = 0;
                return;
            }
            ////Set the media volume based on the slider value
            this.thePlayer.Volume = this.VolumeSlider.Value;
            if (this.VolumeSlider.Value != 0)
            {
                this.TempVolume = this.VolumeSlider.Value;
            }
        }

        /// <summary>
        /// Event Handler for completed of the Timer storyboard.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void Timer_Completed(object sender, EventArgs e)
        {
            //// If the media has reached its end, stop it.
            //// Else if already playing, set the play slider value based on the percentage of media played and update the status text
            //// Else Wait for the stream to be downloaded and start playing
            if (thePlayer.Position == null || this.thePlayer.NaturalDuration == null)
            {
                return;
            }
            else if ((int)Math.Round(thePlayer.Position.TotalSeconds) == (int)Math.Round(thePlayer.NaturalDuration.TimeSpan.TotalSeconds))
            {
                this.StopVideo();
            }
            else if (thePlayer.Position != null && thePlayer.Position.TotalSeconds > 0 && thePlayer.CurrentState == MediaElementState.Playing)
            {
                double percentageplayed = thePlayer.Position.TotalSeconds / thePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                this.PlaySlider.Value = percentageplayed * 100;
                this.UpdateStatusText();
            }
            else if (thePlayer.CurrentState == MediaElementState.Closed || thePlayer.CurrentState == MediaElementState.Paused || thePlayer.CurrentState == MediaElementState.Opening)
            {
                if (thePlayer.Position != null && thePlayer.DownloadProgress > 0)
                {
                    this.UpdateStatusText();

                    //// Play only if download progess is more than the current position
                    if (thePlayer.DownloadProgress * thePlayer.NaturalDuration.TimeSpan.TotalSeconds > thePlayer.Position.TotalSeconds)
                    {
                        thePlayer.Play();
                    }
                }
            }
            else
            {
                return;
            }

            this.Timer.Begin();
        }

        /// <summary>
        /// Event Handler for MouseLeftButtonDown of the Play Slider.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void PlaySlider_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Timer.Stop();
        }

        /// <summary>
        /// Event Handler for MouseLeftButtonUp of the Play Slider.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void PlaySlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.HandlePlaySliderChange();
        }

        /// <summary>
        /// Event Handler for DownloadProgressChanged of the Player media.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ThePlayer_DownloadProgressChanged(object sender, RoutedEventArgs e)
        {
            if (thePlayer.DownloadProgress <= 1)
            {
                progressRect.Width = (double)(thePlayer.DownloadProgress * 600);
            }
        }

        /// <summary>
        /// Handles the forward and backward seek functionality of the Media Player.
        /// </summary>
        private void HandlePlaySliderChange()
        {
            if (thePlayer.Position == null)
            {
                return;
            }

            Duration videospan = new Duration();
            double changeperct = PlaySlider.Value;
            Timer.Stop();
            videospan = thePlayer.NaturalDuration;

            //// alter the media position based on the slider value
            thePlayer.Position = new TimeSpan(0, 0, (int)(videospan.TimeSpan.TotalSeconds * (int)changeperct * .01));
            if (thePlayer.CurrentState == MediaElementState.Playing)
            {
                this.UpdateStatusText();
                thePlayer.Play();
            }
            else
            {
                this.UpdateStatusText();
                this.thePlayer.Pause();
            }

            Timer.Begin();
        }

        /// <summary>
        /// Updates Status Text.
        /// </summary>
        private void UpdateStatusText()
        {
            this.statusText.Text = thePlayer.Position.Hours.ToString(CultureInfo.InvariantCulture) + ":" + thePlayer.Position.Minutes.ToString(CultureInfo.InvariantCulture) + ":" + thePlayer.Position.Seconds.ToString(CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture) + " / " + thePlayer.NaturalDuration.TimeSpan.Hours.ToString(CultureInfo.InvariantCulture) + ":" + thePlayer.NaturalDuration.TimeSpan.Minutes.ToString(CultureInfo.InvariantCulture) + ":" + thePlayer.NaturalDuration.TimeSpan.Seconds.ToString(CultureInfo.InvariantCulture);
        }

        #endregion

        #endregion

        #region Button key press methods

        /// <summary>
        /// Close button key down.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        private void BtnCloseKeyDown(object sender)
        {
            if (this.CloseEventHandler != null)
            {
                this.CloseEventHandler.Invoke(sender, null);
            }
        }

        /// <summary>
        /// Fullscreen button key down.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        private void BtnFullScreenKeyDown(object sender)
        {
            this.videoPosition = thePlayer.Position;
            this.volume = thePlayer.Volume;
            this.playerCurrentState = thePlayer.CurrentState;
            this.thePlayer.IsMuted = true;
            this.BtnMute.Opacity = 1;
            if (this.FullScreenEventHandler != null)
            {
                this.FullScreenEventHandler.Invoke(sender, null);
            }
        }

        /// <summary>
        ///  BtnNext key down.
        /// </summary>
        private void BtnNextKeyDown()
        {
            this.PlaySlider.Value += 5;
            this.HandlePlaySliderChange();
        }

        /// <summary>
        ///  BtnPrev key down.
        /// </summary>
        private void BtnPrevKeyDown()
        {
            this.PlaySlider.Value -= 5;
            this.HandlePlaySliderChange();
        }

        /// <summary>
        ///  BtnPause key down.
        /// </summary>
        private void BtnPauseKeyDown()
        {
            //// Pause the media if it is playing
            if (thePlayer.CurrentState == MediaElementState.Playing)
            {
                thePlayer.Pause();
                Timer.Stop();
                this.BtnPause.Visibility = Visibility.Collapsed;
                this.BtnPlay.Visibility = Visibility.Visible;

                this.BtnPlay.Focus();
            }
        }

        /// <summary>
        ///  BtnPlay key down.
        /// </summary>
        private void BtnPlayKeyDown()
        {
            //// Play the media from the given time offset
            TimeSpan time = thePlayer.Position;
            this.PlayVideo(time);
        }
        #endregion

        #region Button Enter key down Methods

        /// <summary>
        /// Pause button key down event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPause_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.BtnPauseKeyDown();
            }
        }

        /// <summary>
        /// Close button key down event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnClose_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.BtnCloseKeyDown(sender);
            }
        }

        /// <summary>
        /// Play button key down event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPlay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.BtnPlayKeyDown();
            }
        }

        /// <summary>
        /// Stop button key down event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnStop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.StopVideo();
            }
        }

        /// <summary>
        /// Mute button key down event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnMute_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.MuteVideo();
            }
        }

        /// <summary>
        /// Next button key down event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnNext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.BtnNextKeyDown();
            }
        }

        /// <summary>
        /// Prev button key down event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnPrev_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.BtnPrevKeyDown();
            }
        }

        /// <summary>
        /// Fullscreen button key down event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnFullScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.BtnFullScreenKeyDown(sender);
            }
        }

        #endregion
    }
}