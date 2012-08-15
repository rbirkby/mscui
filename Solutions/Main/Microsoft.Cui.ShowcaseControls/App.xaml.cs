//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Microsoft Corporation copyright 2008.">
// Copyright (c) Microsoft Corporation copyright 2008.
// All rights reserved.
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>16-Mar-2008</date>
// <summary>App class.</summary>
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
    using System.Text;
    using System.Collections;

    /// <summary>
    /// Application class.
    /// </summary>
    public partial class App : Application
    {
        #region Private Members
        /// <summary>
        /// Patient Journey Demonstrator Directory.
        /// </summary>
        private static string patientJourneyDemonstratorDirectory = string.Empty;

        /// <summary>
        /// Video source collection for the showcase media controls.
        /// </summary>
        private static Dictionary<string, string> mediaControlVideoSource;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public App()
        {
            this.Startup += this.Application_Startup;
            InitializeComponent();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the value of host application path.
        /// </summary>
        /// <value>
        /// The Host application path.
        /// </value>
        public static string ApplicationPath
        {
            get
            {
                string hostApplicationPath = System.Windows.Browser.HtmlPage.Document.DocumentUri.AbsoluteUri;
                hostApplicationPath = hostApplicationPath.Substring(0, hostApplicationPath.LastIndexOf('/'));
                hostApplicationPath = hostApplicationPath.Substring(0, hostApplicationPath.LastIndexOf('/') + 1);
                return hostApplicationPath;
            }
        }

        /// <summary>
        /// Gets the PatientJourneyDemonstratorDirectory.
        /// </summary>
        /// <value>
        /// Patient Journey Demonstrator Directory.
        /// </value>
        public static string PatientJourneyDemonstratorDirectory
        {
            get
            {
                return patientJourneyDemonstratorDirectory;
            }
        }
        #endregion
        #region Public Events
        /// <summary>
        /// Get the video source based on the key.
        /// </summary>
        /// <param name="key">Showcase media control video key.</param>
        /// <returns>Showcase media control video source.</returns>
        public static string GetVideoSource(string key)
        {
            string videoSource = string.Empty;
            if (mediaControlVideoSource.ContainsKey(key))
            {
                videoSource = mediaControlVideoSource[key];
            }

            return videoSource;
        }
        #endregion

        #region Private Events
        /// <summary>
        /// Pupulate the video source dictionary.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        private static void PopulateVideoSourceDictionary(StartupEventArgs e)
        {
            mediaControlVideoSource = new Dictionary<string, string>();

            // Add the testimonials video source
            if (e.InitParams.ContainsKey("testimonials01"))
            {
                mediaControlVideoSource.Add("testimonials01", e.InitParams["testimonials01"]);
            }

            if (e.InitParams.ContainsKey("testimonials02"))
            {
                mediaControlVideoSource.Add("testimonials02", e.InitParams["testimonials02"]);
            }

            if (e.InitParams.ContainsKey("testimonials03"))
            {
                mediaControlVideoSource.Add("testimonials03", e.InitParams["testimonials03"]);
            }

            if (e.InitParams.ContainsKey("testimonials04"))
            {
                mediaControlVideoSource.Add("testimonials04", e.InitParams["testimonials04"]);
            }

            if (e.InitParams.ContainsKey("testimonials05"))
            {
                mediaControlVideoSource.Add("testimonials05", e.InitParams["testimonials05"]);
            }

            if (e.InitParams.ContainsKey("testimonials06"))
            {
                mediaControlVideoSource.Add("testimonials06", e.InitParams["testimonials06"]);
            }

            // Add the demonstrators video source
            if (e.InitParams.ContainsKey("demonstrators01"))
            {
                mediaControlVideoSource.Add("demonstrators01", e.InitParams["demonstrators01"]);
            }

            if (e.InitParams.ContainsKey("demonstrators02"))
            {
                mediaControlVideoSource.Add("demonstrators02", e.InitParams["demonstrators02"]);
            }

            if (e.InitParams.ContainsKey("demonstrators03"))
            {
                mediaControlVideoSource.Add("demonstrators03", e.InitParams["demonstrators03"]);
            }

            if (e.InitParams.ContainsKey("demonstrators04"))
            {
                mediaControlVideoSource.Add("demonstrators04", e.InitParams["demonstrators04"]);
            }

            if (e.InitParams.ContainsKey("demonstrators05"))
            {
                mediaControlVideoSource.Add("demonstrators05", e.InitParams["demonstrators05"]);
            }
        }

        /// <summary>
        /// Application startup event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Set the PatientJourneyDemonstrator URL 
            if (e.InitParams.ContainsKey("PatientJourneyDemonstrator"))
            {
                patientJourneyDemonstratorDirectory = e.InitParams["PatientJourneyDemonstrator"];
            }

            string hostPageName = string.Empty;
            if (e.InitParams.ContainsKey("HostPageName"))
            {
                hostPageName = e.InitParams["HostPageName"];
            }

            if (hostPageName == "Testimonials")
            {
                this.RootVisual = new TestimonialsPage();
            }
            else if (hostPageName == "Demonstrators")
            {
                this.RootVisual = new DemonstratorsPage();
            }

            PopulateVideoSourceDictionary(e);
        }
        
        #endregion        
    }
}
