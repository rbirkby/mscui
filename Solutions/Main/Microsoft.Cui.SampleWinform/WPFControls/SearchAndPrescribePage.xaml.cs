//-----------------------------------------------------------------------
// <copyright file="SearchAndPrescribePage.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010.
// All rights reserved
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
// <date>18-Aug-2009</date>
// <summary>
//      The Search and Prescribe page.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media.Animation;
    using Microsoft.Cui.Controls;

    /// <summary>
    /// The Search and Prescribe page.
    /// </summary>
    public partial class SearchAndPrescribePage : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the SearchAndPrescribePage class.
        /// </summary>
        public SearchAndPrescribePage()
        {
            InitializeComponent();
            if (SystemParameters.HighContrast)
            {
                this.PatientBanner.Template = this.Resources["HC_PatientBannerTemplate"] as ControlTemplate;
            }

            this.Prescribe.Click += new RoutedEventHandler(this.Prescribe_Click);
            this.GotoMedsListView.Click += new RoutedEventHandler(this.GotoMedsListView_Click);
            this.SearchAndPrescribe.PrimaryDrugsItemsHeader = "Commonly prescribed drugs";
            this.SearchAndPrescribe.PrimaryDrugsItemsSource = DrugDataHelper.CommonDrugs;
            this.SearchAndPrescribe.SecondaryDrugsItemsSource = null;
            this.SearchAndPrescribe.RefreshDrugsList();
            this.SearchAndPrescribe.DrugSearchTextChanged += new EventHandler(this.SearchAndPrescribe_DrugSearchTextChanged);
            this.SearchAndPrescribe.Preview += new RoutedEventHandler(this.SearchAndPrescribe_Preview);
            this.SearchAndPrescribe.Authorize += new RoutedEventHandler(this.SearchAndPrescribe_Authorize);
            this.SearchAndPrescribe.Clear += new EventHandler(this.SearchAndPrescribe_Clear);
            this.SearchAndPrescribe.Close += new RoutedEventHandler(this.SearchAndPrescribe_Close);
            this.SearchAndPrescribe.ConciseAdministrationTimesListOtherSelected += new EventHandler(this.SearchAndPrescribe_AdministrationTimesListOtherSelected);
            this.SearchAndPrescribe.DetailedAdministrationTimesListOtherSelected += new EventHandler(this.SearchAndPrescribe_AdministrationTimesListOtherSelected);

            this.PreviewClosePreviewButton.Click += new RoutedEventHandler(this.PreviewClosePreviewButton_Click);
            this.PreviewAuthorizeButton.Click += new RoutedEventHandler(this.PreviewAuthorizeButton_Click);

            this.DontClearAndCloseButton.Click += new RoutedEventHandler(this.DontClearAndCloseButton_Click);
            this.ClearAndCloseButton.Click += new RoutedEventHandler(this.ClearAndCloseButton_Click);

            this.FeatureUnavailableCloseButton.Click += new RoutedEventHandler(this.FeatureUnavailableCloseButton_Click);

            (this.Resources["ShowPreview"] as Storyboard).Completed += new EventHandler(this.ShowPreview_Completed);
            (this.Resources["ShowCloseConfirmation"] as Storyboard).Completed += new EventHandler(this.ShowCloseConfirmation_Completed);
            (this.Resources["ShowFeatureUnavailable"] as Storyboard).Completed += new EventHandler(this.ShowFeatureUnavailable_Completed);
        }

        /// <summary>
        /// Close the feature unavailable popup.
        /// </summary>
        /// <param name="sender">The close feature unavailble button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void FeatureUnavailableCloseButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Resources["HideFeatureUnavailable"] as Storyboard).Begin();
            if (!this.SearchAndPrescribe.FocusNextControl())
            {
                if (!this.SearchAndPrescribe.FocusNextControl())
                {
                    this.SearchAndPrescribe.FocusPreviewButton();
                }
            }
        }

        /// <summary>
        /// Moves focus to the feature unavailble close button.
        /// </summary>
        /// <param name="sender">The show feature unavailble storyboard.</param>
        /// <param name="e">Event Args.</param>
        private void ShowFeatureUnavailable_Completed(object sender, EventArgs e)
        {
            FocusHelper.FocusControl(this.FeatureUnavailableCloseButton);
        }

        /// <summary>
        /// Shows the feature unavailable popup.
        /// </summary>
        /// <param name="sender">The search and prescribe control.</param>
        /// <param name="e">Event Args.</param>
        private void SearchAndPrescribe_AdministrationTimesListOtherSelected(object sender, EventArgs e)
        {
            this.ShowFeatureUnavailable();
        }

        /// <summary>
        /// Show the feature unavailable popup.
        /// </summary>
        private void ShowFeatureUnavailable()
        {
            (this.Resources["ShowFeatureUnavailable"] as Storyboard).Begin();
        }

        /// <summary>
        /// Creates a new prescription.
        /// </summary>
        private void CreateNewPrescription()
        {
            Prescription prescription = new Prescription();
            this.SearchAndPrescribe.DataContext = prescription;
            this.SearchAndPrescribe.CustomReasonForPrescribingItem = new DrugElement()
            {
                IsCustomValue = true
            };

            this.SearchAndPrescribe.CustomSiteItem = new DrugElement()
            {
                IsCustomValue = true
            };

            this.SearchAndPrescribe.CustomDoseItem = prescription.CustomDose;
            this.SearchAndPrescribe.CustomFrequencyItem = new Frequency()
            {
                Value = string.Empty,
                IsCustomValue = true
            };

            this.SearchAndPrescribe.CustomStartingConditionItem = new DrugElement()
            {
                IsCustomValue = true
            };

            this.SearchAndPrescribe.CustomDurationItem = new Duration()
            {
                Value = TimeSpan.FromMilliseconds(1),
                IsCustomValue = true
            };
        }

        /// <summary>
        /// Clears and closes the search and prescribe control.
        /// </summary>
        /// <param name="sender">The clear and close button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void ClearAndCloseButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Resources["HideCloseConfirmation"] as Storyboard).Begin();
            this.ClearAndClose();
        }

        /// <summary>
        /// Returns to the search and prescribe control.
        /// </summary>
        /// <param name="sender">The don't clear and close button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void DontClearAndCloseButton_Click(object sender, RoutedEventArgs e)
        {
            (this.Resources["HideCloseConfirmation"] as Storyboard).Begin();
            if (!this.SearchAndPrescribe.FocusNextControl())
            {
                this.SearchAndPrescribe.FocusCloseButton();
            }
        }

        /// <summary>
        /// Focuses the dont close and clear button.
        /// </summary>
        /// <param name="sender">The show close confirmation storyboard.</param>
        /// <param name="e">Event Args.</param>
        private void ShowCloseConfirmation_Completed(object sender, EventArgs e)
        {
            FocusHelper.FocusControl(this.DontClearAndCloseButton);
        }

        /// <summary>
        /// Authorizes the prescription.
        /// </summary>
        /// <param name="sender">The preview authorize button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void PreviewAuthorizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.PreviewAuthorizeButtonContainer.IsEnabled = false;
            this.MedsListViewHost.AddPrescription((this.SearchAndPrescribe.DataContext as Prescription).CopyForAuthorizing());
            (this.Resources["HidePreview"] as Storyboard).Begin();
            this.ClearAndClose();
        }

        /// <summary>
        /// Sets focus to the close preview button.
        /// </summary>
        /// <param name="sender">The show preview animation.</param>
        /// <param name="e">Event Args.</param>
        private void ShowPreview_Completed(object sender, EventArgs e)
        {
            this.PreviewAuthorizeButtonContainer.IsEnabled = true;
        }

        /// <summary>
        /// Closes the preview.
        /// </summary>
        /// <param name="sender">The close preview button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void PreviewClosePreviewButton_Click(object sender, RoutedEventArgs e)
        {
            this.PreviewAuthorizeButtonContainer.IsEnabled = false;
            (this.Resources["HidePreview"] as Storyboard).Begin();
            if (!this.SearchAndPrescribe.FocusNextControl())
            {
                this.SearchAndPrescribe.FocusPreviewButton();
            }
        }

        /// <summary>
        /// Shows the preview.
        /// </summary>
        /// <param name="sender">The search and prescribe control.</param>
        /// <param name="e">Routed Event Args.</param>
        private void SearchAndPrescribe_Preview(object sender, RoutedEventArgs e)
        {
            (this.Resources["ShowPreview"] as Storyboard).Begin();
            FocusHelper.FocusControl(this.PreviewClosePreviewButton);
        }

        /// <summary>
        /// Clears the control and starts again.
        /// </summary>
        /// <param name="sender">The search and prescribe control.</param>
        /// <param name="e">Event Args.</param>
        private void SearchAndPrescribe_Clear(object sender, EventArgs e)
        {
            this.SearchAndPrescribe.Reset();
            this.CreateNewPrescription();
            this.SearchAndPrescribe.FocusDrugSearchTextBox();
        }

        /// <summary>
        /// Clears and closes the control.
        /// </summary>
        /// <param name="sender">The search and prescribe control.</param>
        /// <param name="e">Routed Event Args.</param>
        private void SearchAndPrescribe_Close(object sender, RoutedEventArgs e)
        {
            (this.Resources["ShowCloseConfirmation"] as Storyboard).Begin();
        }

        /// <summary>
        /// Starts a new prescription.
        /// </summary>
        /// <param name="sender">The prescribe button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void Prescribe_Click(object sender, RoutedEventArgs e)
        {
            this.CreateNewPrescription();
            this.Prescribe.Opacity = 0;
            this.Prescribe.IsHitTestVisible = false;
            this.Prescribe.IsEnabled = false;
            this.SearchAndPrescribe.IsEnabled = true;
            (this.Resources["ShowSearchAndPrescribeControl"] as Storyboard).Begin();
            this.SearchAndPrescribe.FocusDrugSearchTextBox();
        }

        /// <summary>
        /// Navigates to the meds list view sample.
        /// </summary>
        /// <param name="sender">The goto meds list view button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void GotoMedsListView_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.mscui.net/Components/MedicationsListView.aspx");
        }

        /// <summary>
        /// Authorizes the current prescription.
        /// </summary>
        /// <param name="sender">The search and prescribe control.</param>
        /// <param name="e">Routed Event Args.</param>
        private void SearchAndPrescribe_Authorize(object sender, RoutedEventArgs e)
        {
            this.MedsListViewHost.AddPrescription((this.SearchAndPrescribe.DataContext as Prescription).CopyForAuthorizing());
            this.ClearAndClose();
        }

        /// <summary>
        /// Clears and closes the control.
        /// </summary>
        private void ClearAndClose()
        {
            this.SearchAndPrescribe.DataContext = new Prescription();
            this.SearchAndPrescribe.Reset();
            this.Prescribe.Opacity = 1;
            this.Prescribe.IsHitTestVisible = true;
            this.Prescribe.IsEnabled = true;
            this.SearchAndPrescribe.IsEnabled = false;
            (this.Resources["HideSearchAndPrescribeControl"] as Storyboard).Begin();
            FocusHelper.FocusControl(this.Prescribe);
        }

        /// <summary>
        /// Updates the drug search results.
        /// </summary>
        /// <param name="sender">The search and prescribe control.</param>
        /// <param name="e">Text Changed Event Args.</param>
        private void SearchAndPrescribe_DrugSearchTextChanged(object sender, EventArgs e)
        {
            this.SearchAndPrescribe.SelectedDrug = null;
            this.SearchDrugs();
        }

        /// <summary>
        /// Searches the drugs.
        /// </summary>
        private void SearchDrugs()
        {
            string text = this.SearchAndPrescribe.DrugSearchText;
            if (DrugSearchHelper.ContainsWordWithMinimumCharacters(text, 2))
            {
                this.SearchAndPrescribe.DrugsResultsListMessage = "No items were matched";
                Drug[] matches = DrugSearchHelper.Search(text);
                if (matches != null && matches.Length > 0)
                {
                    List<Drug> commonMatches = new List<Drug>();
                    List<Drug> standardMatches = new List<Drug>();

                    foreach (Drug drug in matches)
                    {
                        if (drug.CommonlyPrescribed || drug.Routes != null)
                        {
                            commonMatches.Add(drug);
                        }
                        else
                        {
                            standardMatches.Add(drug);
                        }
                    }

                    this.SearchAndPrescribe.PrimaryDrugsItemsHeader = "Common matches";
                    this.SearchAndPrescribe.PrimaryDrugsItemsSource = commonMatches;

                    if (commonMatches.Count > 0)
                    {
                        this.SearchAndPrescribe.SecondaryDrugsItemsHeader = "Standard matches";
                    }
                    else
                    {
                        this.SearchAndPrescribe.SecondaryDrugsItemsHeader = null;
                    }

                    this.SearchAndPrescribe.SecondaryDrugsItemsSource = standardMatches;
                    this.SearchAndPrescribe.IsShowingAllDrugResults = false;
                    this.SearchAndPrescribe.RefreshDrugsList();
                }
                else
                {
                    this.SearchAndPrescribe.PrimaryDrugsItemsSource = null;
                    this.SearchAndPrescribe.SecondaryDrugsItemsSource = null;
                    this.SearchAndPrescribe.RefreshDrugsList();
                }
            }
            else if (text.Trim().Length == 0)
            {
                this.SearchAndPrescribe.PrimaryDrugsItemsHeader = "Commonly prescribed drugs";
                this.SearchAndPrescribe.PrimaryDrugsItemsSource = DrugDataHelper.CommonDrugs;
                this.SearchAndPrescribe.SecondaryDrugsItemsSource = null;
                this.SearchAndPrescribe.IsShowingAllDrugResults = false;
                this.SearchAndPrescribe.RefreshDrugsList();
            }
            else
            {
                this.SearchAndPrescribe.DrugsResultsListMessage = "Please enter at least 2 characters";
                this.SearchAndPrescribe.PrimaryDrugsItemsSource = null;
                this.SearchAndPrescribe.SecondaryDrugsItemsSource = null;
                this.SearchAndPrescribe.RefreshDrugsList();
            }
        }
    }
}
