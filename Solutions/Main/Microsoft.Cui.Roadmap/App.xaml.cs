//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Microsoft Corporation copyright 2008.">
// Copyright (c) Microsoft Corporation copyright 2008.
// All rights reserved.
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>27-Mar-2008</date>
// <summary>App class.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Roadmap
{
    #region using
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
    #endregion
    /// <summary>
    /// App startup Class.
    /// </summary>
    public partial class App : Application
    {
        #region constructor
        /// <summary>
        /// Constructor method.
        /// </summary>
        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }
        #endregion

        #region properties
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
        #endregion

        #region methods
        /// <summary>
        /// Application_Startup method.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event args.</param>
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Load the main control
            this.RootVisual = new Microsoft.Cui.Roadmap.Page();
        }
        
        /// <summary>
        /// Application_Exit method.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event args.</param>
        private void Application_Exit(object sender, EventArgs e)
        {
        }
        
        /// <summary>
        /// Application_UnhandledException method.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event args.</param>
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
        }        
        #endregion
    }
}
