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
// <date>31-Oct-2008</date>
// <summary>Sample page used to host patient banner control.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.SampleWinform
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using Microsoft.Cui.Controls;

    /// <summary>
    /// Interaction logic for PatientBannerPage.xaml
    /// </summary>
    public sealed partial class PatientBannerPage : UserControl
    {
        /// <summary>
        /// Member variable to hold AllowResize.
        /// </summary>
        private bool allowResize = true;

        /// <summary>
        /// Initializes a new instance of PatientBanner.
        /// </summary>
        public PatientBannerPage()
        {
            InitializeComponent();
            this.PatientBanner1.Allergies.Add(new Allergy("Dust", new DateTime(2007, 7, 1)));
            this.PatientBanner1.Allergies.Add(new Allergy("Smoke", new DateTime(2007, 6, 10)));
            this.PatientBanner1.Allergies.Add(new Allergy("Perfume", new DateTime(2006, 6, 14)));
            this.PatientBanner1.Allergies.Add(new Allergy("Latex", new DateTime(2006, 6, 21)));
            this.PatientBanner1.Allergies.Add(new Allergy("Peanuts", new DateTime(2007, 1, 6)));
            this.PatientBanner1.Allergies.Add(new Allergy("Hay", new DateTime(2007, 3, 6)));
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
        /// Handles the Page loaded event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
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
                MessageBox.Show("View all addresses clicked");
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
                MessageBox.Show("View allergy record clicked");
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
                MessageBox.Show("View contact details clicked");
            }
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
                MessageBox.Show("Identifier clicked");
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
                MessageBox.Show("Identifier label clicked");
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
                MessageBox.Show("Date of birth clicked");
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
                MessageBox.Show("Date of birth label clicked");
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
                MessageBox.Show("Date of death clicked");
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
                MessageBox.Show("Date of death label clicked");
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
                MessageBox.Show("Gender clicked");
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
                MessageBox.Show("Gender label clicked");
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
                MessageBox.Show("Age at death clicked");
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
                MessageBox.Show("Age at death label clicked");
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
                MessageBox.Show("Preferred name clicked");
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
                MessageBox.Show("Preferred name label clicked");
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
                MessageBox.Show("Patient name clicked");
            }
        }
    }
}
