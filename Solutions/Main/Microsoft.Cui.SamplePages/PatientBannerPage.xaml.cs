//-----------------------------------------------------------------------
// <copyright file="PatientBannerPage.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>19-May-2008</date>
// <summary>Sample page to host patient banner control. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.SamplePages
{
    #region Using
    using System;
    using System.Windows;
    using System.Windows.Controls;
    #endregion

    /// <summary>
    /// Sample page to host patient banner control.
    /// </summary>
    public partial class PatientBannerPage : UserControl
    {
        /// <summary>
        /// Delegate to handle Alerts.
        /// </summary>
        /// <param name="message">
        /// Alert text.
        /// </param>
        private delegate void ShowUIAlert(string message);
        
        /// <summary>
        /// Member variable to hold AllowResize.
        /// </summary>
        private bool allowResize = true;

        /// <summary>
        /// Member variable to hold last clicked link.
        /// </summary>
        private object lastClickedLink;

        /// <summary>
        /// Initializes a new instance of patient banner page.
        /// </summary>
        public PatientBannerPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(this.PatientBannerPage_Loaded);
        }

        /// <summary>
        /// Gets or sets a value indicating whether resizers can be shown on the page.
        /// </summary>
        /// <value>Boolean indicating whether resizers can be shown.</value>
        public bool AllowResize
        {
            get
            {
                return this.allowResize;
            }

            set
            {
                this.allowResize = value;
                this.ShowOrHideResizers();
            }
        }

        /// <summary>
        /// Shows or Hides the resizers based on AllowResize Property.
        /// </summary>
        private void ShowOrHideResizers()
        {
            if (this.TopResizer != null && this.BottomResizer != null && this.LeftResizer != null && this.RightResizer != null)
            {
                if (this.allowResize)
                {
                    this.TopResizer.Visibility = Visibility.Visible;
                    this.BottomResizer.Visibility = Visibility.Visible;
                    this.LeftResizer.Visibility = Visibility.Visible;
                    this.RightResizer.Visibility = Visibility.Visible;
                }
                else
                {
                    this.TopResizer.Visibility = Visibility.Collapsed;
                    this.BottomResizer.Visibility = Visibility.Collapsed;
                    this.LeftResizer.Visibility = Visibility.Collapsed;
                    this.RightResizer.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Handles the loaded event of the page.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBannerPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.ShowOrHideResizers(); 
        }
        
        /// <summary>
        /// Handles the ViewAllAddressesClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_ViewAllAddressesClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "View all addresses clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText);                
                this.lastClickedLink = sender;
            }
        }

        /// <summary>
        /// Handles the ViewAllergyRecordClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_ViewAllergyRecordClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "View allergy record clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText);   
                this.lastClickedLink = sender;
            }
        }

        /// <summary>
        /// Handles the ViewContactDetailsClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_ViewContactDetailsClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "View contact details clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText); 
                this.lastClickedLink = sender;
            }
        }

        /// <summary>
        /// Shows an Alert dialog.
        /// </summary>
        /// <param name="message">
        /// Alert text.
        /// </param>
        private void ShowAlert(string message)
        {
            System.Windows.Browser.HtmlPage.Window.Alert(message);
        }

        /// <summary>
        /// Handles the IdentifierClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_IdentifierClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "Identifier clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText);                
                this.lastClickedLink = sender;
            }
        }

        /// <summary>
        /// Handles the IdentifierLabelClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_IdentifierLabelClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "Identifier label clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText);        
                this.lastClickedLink = sender;
            }
        }

        /// <summary>
        /// Handles the DateOfBirthClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_DateOfBirthClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "Date of birth clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText);   
                this.lastClickedLink = sender;
            }
        }

        /// <summary>
        /// Handles the DateOfBirthLabelClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_DateOfBirthLabelClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "Date of birth label clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText); 
                this.lastClickedLink = sender;
            }
        }

        /// <summary>
        /// Handles the DateOfDeathClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_DateOfDeathClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "Date of death clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText);
                this.lastClickedLink = sender;
            }
        }

        /// <summary>
        /// Handles the DateOfDeathLabelClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_DateOfDeathLabelClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "Date of death label clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText);
                this.lastClickedLink = sender;
            }
        }

        /// <summary>
        /// Handles the GenderClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_GenderClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "Gender clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText); 
                this.lastClickedLink = sender;
            }
        }

        /// <summary>
        /// Handles the GenderLabelClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_GenderLabelClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "Gender label clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText); 
                this.lastClickedLink = sender;
            }
        }

        /// <summary>
        /// Handles the AgeAtDeathClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_AgeAtDeathClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "Age at death clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText); 
                this.lastClickedLink = sender;
            }
        }

        /// <summary>
        /// Handles the AgeAtDeathLabelClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_AgeAtDeathLabelClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "Age at death label clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText); 
                this.lastClickedLink = sender;
            }
        }

        /// <summary>
        /// Handles the PreferredNameClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_PreferredNameClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "Preferred name clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText); 
                this.lastClickedLink = sender;
            }
        }

        /// <summary>
        /// Handles the PreferredNameLabelClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_PreferredNameLabelClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "Preferred name label clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText); 
                this.lastClickedLink = sender;
            }
        }

        /// <summary>
        /// Handles the NameClick event on Patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args.</param>
        private void PatientBanner_NameClick(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                string alertText = "Patient name clicked";
                this.Dispatcher.BeginInvoke(new ShowUIAlert(this.ShowAlert), alertText); 
                this.lastClickedLink = sender;
            }
        }
    }
}
