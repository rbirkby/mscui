//-----------------------------------------------------------------------
// <copyright file="ToolkitControlsForm.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>04-June-2007</date>
// <summary>NHS CUI Toolkit SampleWinForm ToolkitControlsForm.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.SampleWinform
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using NhsCui.Toolkit.WinForms;
    using NhsCui.Toolkit.DateAndTime;
    using System.Globalization;
    using System.Configuration;
    using System.Xml;

    /// <summary>
    /// Master form for the WinForms controls.
    /// </summary>
    public partial class ToolkitControlsForm : Form
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ToolkitControlsForm()
        {
            this.InitializeComponent();
            this.assemblyVersionLabel.Text += System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion;

            this.patientBanner1.ViewAllContactDetailsClick += new System.EventHandler(this.PatientBanner1_ViewAllContactDetailsClick);
            this.patientBanner1.ViewAllAddressesClick += new System.EventHandler(this.PatientBanner1_ViewAllAddressesClick);
            this.patientBanner1.GenderValueClick += new EventHandler(this.PatientBanner1_GenderValueClick);
            this.patientBanner1.IdentifierClick += new EventHandler(this.PatientBanner1_IdentifierClick);

            this.copyrightLabel.Text = ConfigurationManager.AppSettings["CopyrightText"].ToString();

            if (string.Compare(ConfigurationManager.AppSettings["HeaderImage"].ToString(), Properties.Resources.MsCui, true, CultureInfo.CurrentCulture) == 0)
            {
                this.titlePictureBox.Image = Properties.Resources.cuibann;
            }

            this.Text = ConfigurationManager.AppSettings["ApplicationTitle"].ToString();

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Avoid nulls and 'empty' values.
        /// </summary>
        /// <param name="targetLabel">Result Label to set.</param>
        /// <param name="value">Property value to assign.</param>
        private static void SetValidValue(Label targetLabel, object value)
        {
            if (value != null && value.ToString() != "-1")
            {
                targetLabel.Text = value.ToString();
            }
            else
            {
                targetLabel.Text = string.Empty;
            }
        }

        /// <summary>
        /// Form Load event.
        /// </summary>
        /// <param name="sender">Sender of the Event.</param>
        /// <param name="e">Event args for the event.</param>
        private void ToolkitControlsForm_Load(object sender, EventArgs e)
        {
            this.contentWebBrowser.Dock = DockStyle.Fill;
            this.controlSelectorComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Control selection changed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args for the event.</param>
        private void ControlSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ResetAllParenting();
            this.controlSamplePanel.Visible = true;
            this.contentWebBrowser.BringToFront();
            bool allowResize = bool.Parse(ConfigurationManager.AppSettings["AllowResize"]);

            this.wpfControlsElementHost.AutoSize = false;
            this.wpfControlsElementHost.Dock = DockStyle.None;

            switch (this.controlSelectorComboBox.SelectedIndex)
            {
                case 0:
                    this.SetBaseWpfHost();
                    this.controlSamplePanel.Visible = false;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/Intro.htm"));
                    break;
                case 1:
                    this.controlSamplePanel.Visible = false;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/WPFIntro.htm"));
                    break;
                case 2:
                    AddressLabelPage addressLabelPage = new AddressLabelPage();
                    addressLabelPage.AllowResize = allowResize;
                    this.wpfControlsElementHost.Size = new Size(800, 130);
                    this.wpfControlsElementHost.Parent = this.wpfAddressLabelPanel;
                    this.wpfControlsElementHost.Child = addressLabelPage;
                    this.wpfAddressLabelPanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/AddressLabel.htm"));
                    break;
                case 3:
                    ContactLabelPage contactLabelPage = new ContactLabelPage();
                    contactLabelPage.AllowResize = allowResize;
                    this.wpfControlsElementHost.Size = new Size(800, 120);
                    this.wpfControlsElementHost.Parent = this.wpfContactLabelPanel;
                    this.wpfControlsElementHost.Child = contactLabelPage;
                    this.wpfContactLabelPanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/ContactLabel.htm"));
                    break;
                case 4:
                    DateLabelPage dateLabelPage = new DateLabelPage();
                    dateLabelPage.AllowResize = allowResize;
                    this.wpfControlsElementHost.Size = new Size(800, 75);
                    this.wpfControlsElementHost.Parent = this.wpfDateLabelPanel;
                    this.wpfControlsElementHost.Child = dateLabelPage;
                    this.wpfDateLabelPanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/DateLabel.htm"));
                    break;
                case 5:
                    GenderLabelPage genderLabelPage = new GenderLabelPage();
                    genderLabelPage.AllowResize = allowResize;
                    this.wpfControlsElementHost.Size = new Size(800, 75);
                    this.wpfControlsElementHost.Parent = this.wpfGenderLabelPanel;
                    this.wpfControlsElementHost.Child = genderLabelPage;
                    this.wpfGenderLabelPanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/GenderLabel.htm"));
                    break;
                case 6:
                    GraphingSamplePage graphingSamplePage = new GraphingSamplePage();
                    this.wpfControlsElementHost.AutoSize = true;
                    this.wpfControlsElementHost.Dock = DockStyle.Fill;
                    this.graphingControlPanel.Dock = DockStyle.Fill;
                    this.contentSplitContainer.SplitterDistance = 575;
                    this.wpfControlsElementHost.Parent = this.graphingControlPanel;
                    this.wpfControlsElementHost.Child = graphingSamplePage;
                    this.graphingControlPanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/Graphing.htm"));
                    break;
                case 7:
                    IdentifierLabelPage identifierLabelPage = new IdentifierLabelPage();
                    identifierLabelPage.AllowResize = allowResize;
                    this.wpfControlsElementHost.Size = new Size(800, 75);
                    this.wpfControlsElementHost.Parent = this.wpfIdentifierLabelPanel;
                    this.wpfControlsElementHost.Child = identifierLabelPage;
                    this.wpfIdentifierLabelPanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/IdentifierLabel.htm"));
                    break;
                case 8:
                    this.medsListViewSamplePanel.Parent = this.controlSamplePanel;                    
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/MedsListView.htm"));
                    this.patientListBox.DataSource = GetPatientNames();
                    break;
                case 9:
                    NameLabelPage nameLabelPage = new NameLabelPage();
                    nameLabelPage.AllowResize = allowResize;
                    this.wpfControlsElementHost.Size = new Size(800, 120);
                    this.wpfControlsElementHost.Parent = this.wpfNameLabelPanel;
                    this.wpfControlsElementHost.Child = nameLabelPage;
                    this.wpfNameLabelPanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/NameLabel.htm"));
                    break;
                case 10:
                    PatientBannerPage patientBannerPage = new PatientBannerPage();
                    patientBannerPage.AllowResize = allowResize;
                    this.wpfControlsElementHost.Size = new Size(850, 250);
                    this.wpfControlsElementHost.Parent = this.wpfPatientBannerPanel;
                    this.wpfControlsElementHost.Child = patientBannerPage;
                    this.wpfPatientBannerPanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/PatientBanner.htm"));
                    break;
                case 11:
                    SamplePages.SearchAndPrescribePage searchAndPrescribePage = new SamplePages.SearchAndPrescribePage();
                    this.wpfControlsElementHost.AutoSize = true;
                    this.wpfControlsElementHost.Dock = DockStyle.Fill;
                    this.singleConceptMatchingPanel.Dock = DockStyle.Fill;
                    this.contentSplitContainer.SplitterDistance = 575;
                    this.wpfControlsElementHost.Parent = this.singleConceptMatchingPanel;
                    this.wpfControlsElementHost.Child = searchAndPrescribePage;
                    this.singleConceptMatchingPanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/SearchAndPrescribe.htm"));
                    break;
                case 12:
                    SingleConceptMatchingPage singleConceptMatchingPage = new SingleConceptMatchingPage();
                    this.wpfControlsElementHost.AutoSize = true;
                    this.wpfControlsElementHost.Dock = DockStyle.Fill;
                    this.singleConceptMatchingPanel.Dock = DockStyle.Fill;
                    this.contentSplitContainer.SplitterDistance = 575;
                    this.wpfControlsElementHost.Parent = this.singleConceptMatchingPanel;
                    this.wpfControlsElementHost.Child = singleConceptMatchingPage;
                    this.singleConceptMatchingPanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/SingleConceptMatching.htm"));
                    break;
                case 13:
                    TimeLabelPage timeLabelPage = new TimeLabelPage();
                    timeLabelPage.AllowResize = allowResize;
                    this.wpfControlsElementHost.Size = new Size(800, 180);
                    this.wpfControlsElementHost.Parent = this.wpfTimeLabelPanel;
                    this.wpfControlsElementHost.Child = timeLabelPage;
                    this.wpfTimeLabelPanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/TimeLabel.htm"));
                    break;
                case 14:
                    TimeActivityGraphHostPage timeActivityGraphHostPage = new TimeActivityGraphHostPage();
                    this.wpfControlsElementHost.AutoSize = true;
                    this.wpfControlsElementHost.Dock = DockStyle.Fill;
                    this.timelineGraphPanel.Dock = DockStyle.Fill;
                    this.contentSplitContainer.SplitterDistance = 575;
                    this.wpfControlsElementHost.Parent = this.timelineGraphPanel;
                    this.wpfControlsElementHost.Child = timeActivityGraphHostPage;
                    this.timelineGraphPanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/Timeline.htm"));
                    break;
                case 15:
                    this.controlSamplePanel.Visible = false;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/WinFormsIntro.htm"));
                    break;
                case 16:
                    this.addressLabelSamplePanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/AddressLabel.htm"));
                    break;
                case 17:
                    this.contactLabelSamplePanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/ContactLabel.htm"));
                    break;
                case 18:
                    this.dateInputBoxSamplePanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/DateInputBox.htm"));
                    break;
                case 19:
                    this.dateLabelSamplePanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/DateLabel.htm"));
                    break;
                case 20:
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/ErrorProvider.htm"));
                    this.ErrorProviderSamplePanel.Parent = this.controlSamplePanel;
                    break;
                case 21:
                    this.genderLabelSamplePanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/GenderLabel.htm"));
                    break;
                case 22:
                    this.identifierLabelSamplePanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/IdentifierLabel.htm"));
                    break;
                case 23:
                    this.nameLabelSamplePanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/NameLabel.htm"));
                    break;
                case 24:
                    this.patientBannerSamplePanel.Parent = this.controlSamplePanel;
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/PatientBanner.htm"));
                    this.patientBannerSamplePanel.BringToFront();
                    break;
                case 25:
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/PatientSearchInputBox.htm"));
                    this.patientSearchInputBoxSamplePanel.Parent = this.controlSamplePanel;
                    break;
                case 26:
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/TimeInputBox.htm"));
                    this.timeInputBoxSamplePanel.Parent = this.controlSamplePanel;
                    break;
                case 27:
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/TimeLabel.htm"));
                    this.timeLabelSamplePanel.Parent = this.controlSamplePanel;
                    break;
                case 28:
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/TimeSpanLabel.htm"));
                    this.timeSpanLabelSamplePanel.Parent = this.controlSamplePanel;
                    break;
                case 29:
                    this.contentWebBrowser.Navigate(new System.Uri(Application.StartupPath + "/Pages/TimeSpanInputBox.htm"));
                    this.timeSpanInputBoxSamplePanel.Parent = this.controlSamplePanel;
                    break;
            }

            this.AdjustControlSamplePanelHeight();
        }

        /// <summary>
        /// Set the base WPF container to hold something.
        /// </summary>
        private void SetBaseWpfHost()
        {
            // Ensure the HostElement contains something
            System.Windows.UIElement baseControl = new System.Windows.UIElement();
            this.wpfControlsElementHost.AutoSize = false;
            this.wpfControlsElementHost.Dock = DockStyle.None;
            this.wpfControlsElementHost.Parent = this.wpfHostPanel;
            this.wpfControlsElementHost.Child = baseControl;
            this.wpfHostPanel.Parent = this.controlSamplePanel;
        }

        /// <summary>
        /// Show the opening intro documentation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args for the event.</param>
        private void IntroductionButton_Click(object sender, EventArgs e)
        {
            this.controlSelectorComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Handle the form resize.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args for the event.</param>
        private void ToolkitControlsForm_Resize(object sender, EventArgs e)
        {
            // Maintain aspect ratio of the docked top image
            this.titlePictureBox.Height = (this.titlePictureBox.Width * this.titlePictureBox.Image.Height) / this.titlePictureBox.Image.Width;
        }

        /// <summary>
        /// Toggle AllowApproximate.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args for the event.</param>
        private void AllowApproxCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.dateInputBox1.AllowApproximate = this.allowApproxCheckBox.Checked;
        }

        /// <summary>
        /// Toggle DisplayDayOfWeek.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args for the event.</param>
        private void DisplayDayOfWeekCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.dateInputBox1.DisplayDayOfWeek = this.displayDayOfWeekCheckBox.Checked;
        }

        /// <summary>
        /// Toggle DisplayDateAsText.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args for the event.</param>
        private void DisplayDateAsTextCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.dateInputBox1.DisplayDateAsText = this.displayDateAsTextCheckBox.Checked;
        }

        /// <summary>
        /// Toggle immediate updating.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args for the event.</param>
        private void KeystrokeUpdateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.keystrokeUpdateCheckBox.Checked == true)
            {
                this.updateButton.Enabled = false;
                this.patientSearchInputBox1.TextChanged += new EventHandler(this.ParseAndDisplay);
            }
            else
            {
                this.updateButton.Enabled = true;
                this.patientSearchInputBox1.TextChanged -= new EventHandler(this.ParseAndDisplay);
            }
        }

        /// <summary>
        /// Call Parse and process the results.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void ParseAndDisplay(object sender, EventArgs e)
        {
            this.patientSearchInputBox1.Parse();

            ToolkitControlsForm.SetValidValue(this.familyNameResult, this.patientSearchInputBox1.FamilyName);
            ToolkitControlsForm.SetValidValue(this.givenNameResult, this.patientSearchInputBox1.GivenName);
            ToolkitControlsForm.SetValidValue(this.nhsNumberResult, this.patientSearchInputBox1.NhsNumber);
            ToolkitControlsForm.SetValidValue(this.ageResult, this.patientSearchInputBox1.Age);
            ToolkitControlsForm.SetValidValue(this.dateOfBirthResult, this.patientSearchInputBox1.DateOfBirth);
            ToolkitControlsForm.SetValidValue(this.genderResult, this.patientSearchInputBox1.Gender);
            ToolkitControlsForm.SetValidValue(this.titleResult, this.patientSearchInputBox1.Title);
            ToolkitControlsForm.SetValidValue(this.addressResult, this.patientSearchInputBox1.Address);
            ToolkitControlsForm.SetValidValue(this.postcodeResult, this.patientSearchInputBox1.Postcode);
            this.postcodeResult.Text = this.postcodeResult.Text.ToUpper(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Handle parsing via the update button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args for the event.</param>
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            this.ParseAndDisplay(sender, e);
        }

        /// <summary>
        /// Toggle complex functionality.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args for the event.</param>
        private void ComplexFunctionalityCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.timeInputBox3.Functionality == TimeFunctionality.Simple)
            {
                this.timeInputBox3.Functionality = TimeFunctionality.Complex;
                this.timeInputBox3.TooltipText = "Enter a time, e.g. 02:05 or a shortcut, e.g. +3h";

                this.allowApproximateValueCheckBox.Visible = true;
            }
            else
            {
                this.timeInputBox3.Functionality = TimeFunctionality.Simple;
                this.timeInputBox3.TooltipText = "Enter a time, e.g. 02:05";

                this.allowApproximateValueCheckBox.Visible = false;
            }
        }

        /// <summary>
        /// Toggle AllowApproximate.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args for the event.</param>
        private void AllowApproximateValueCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.timeInputBox3.AllowApproximate = this.allowApproximateValueCheckBox.Checked;
        }

        /// <summary>
        /// Toggle seconds display.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args for the event.</param>
        private void HideSecondsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.timeInputBox4.DisplaySeconds = !this.hideSecondsCheckBox.Checked;
        }

        /// <summary>
        /// Set the value to 23:59.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args for the event.</param>
        private void SetTo2359Button_Click(object sender, EventArgs e)
        {
            this.timeInputBox3.Value = NhsTime.ParseExact("23:59", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Shrink/Grow the control sample container.
        /// </summary>
        private void AdjustControlSamplePanelHeight()
        {
            if (this.controlSelectorComboBox.SelectedIndex == 0)
            {
                this.contentSplitContainer.SplitterDistance = 0;
                this.contentSplitContainer.SplitterWidth = 1;
            }
            else
            {
                int bottomExtent = 0;

                this.contentSplitContainer.SplitterWidth = 3;

                foreach (Control currentPanel in this.controlSamplePanel.Controls)
                {
                    foreach (Control currentControl in currentPanel.Controls)
                    {
                        bool previousState = false;
                        PatientBanner patientBanner = currentControl as PatientBanner;
                        if (patientBanner != null)
                        {
                            patientBanner.Dock = DockStyle.None;
                            previousState = patientBanner.ZoneTwoExpanded;
                            patientBanner.ZoneTwoExpanded = true;
                        }

                        if (currentControl.Bounds.Bottom > bottomExtent)
                        {
                            bottomExtent = currentControl.Bounds.Bottom;
                        }

                        if (patientBanner != null)
                        {
                            patientBanner.ZoneTwoExpanded = previousState;
                        }
                    }
                }

                if (this.Size.Height > this.MinimumSize.Height)
                {
                    this.contentSplitContainer.SplitterDistance = bottomExtent + 10;
                }
            }
        }

        /// <summary>
        /// Reset the parent of each control sample panel.
        /// </summary>
        private void ResetAllParenting()
        {
            this.addressLabelSamplePanel.Parent = this.addressLabelTabPage;
            this.contactLabelSamplePanel.Parent = this.contactLabelTabPage;
            this.dateInputBoxSamplePanel.Parent = this.dateInputBoxTabPage;
            this.dateLabelSamplePanel.Parent = this.dateLabelTabPage;
            this.genderLabelSamplePanel.Parent = this.genderLabelTabPage;
            this.identifierLabelSamplePanel.Parent = this.identifierLabelTabPage;
            this.medsListViewSamplePanel.Parent = this.medsListViewTabPage;
            this.nameLabelSamplePanel.Parent = this.nameLabelTabPage;
            this.patientBannerSamplePanel.Parent = this.patientBannerTabPage;
            this.patientSearchInputBoxSamplePanel.Parent = this.patientSearchInputBoxTabPage;
            this.timeInputBoxSamplePanel.Parent = this.timeInputBoxTabPage;
            this.timeLabelSamplePanel.Parent = this.timeLabelTabPage;
            this.timeSpanLabelSamplePanel.Parent = this.timeSpanLabelTabPage;
            this.ErrorProviderSamplePanel.Parent = this.ErrorProviderTabPage;
            this.timeSpanInputBoxSamplePanel.Parent = this.timeSpanInputBoxTabPage;
            this.wpfPatientBannerPanel.Parent = this.patientBannerWPFTabPage;
            this.wpfAddressLabelPanel.Parent = this.addressLabelWPFTabPage;
            this.wpfContactLabelPanel.Parent = this.contactLabelWPFTabPage;
            this.graphingControlPanel.Parent = this.graphingControlTabPage;
            this.singleConceptMatchingPanel.Parent = this.singleConceptMatchingTabPage;
            this.wpfGenderLabelPanel.Parent = this.genderLabelWPFTabPage;
            this.wpfTimeLabelPanel.Parent = this.timeLabelWPFTabPage;
            this.wpfIdentifierLabelPanel.Parent = this.identifierLabelWPFTabPage;
            this.wpfNameLabelPanel.Parent = this.nameLabelWPFTabPage;
            this.wpfHostPanel.Parent = this.wpfHostTabPage;
            this.wpfDateLabelPanel.Parent = this.dateLabelWPFTabPage;
            this.timelineGraphPanel.Parent = this.timelineGraphTabPage;
        }

        /// <summary>
        /// To retrieve the patient banner to bind to list box.
        /// </summary>
        /// <returns>List<string>Patient names.</string></returns>
        private static List<string> GetPatientNames()
        {
            XmlTextReader reader = new XmlTextReader("SampleData/XMLPatientData.xml");
            List<string> returnList = new List<string>();
            while (reader.Read())
            {
                // keep reading to find Patient elements
                if (reader.Name.Equals("Patient") && (reader.NodeType == XmlNodeType.Element))
                {
                    if (reader.GetAttribute("FamilyName") != null)
                    {
                        returnList.Add(reader.GetAttribute("FamilyName"));
                    }
                    else
                    {
                        reader.Skip();
                    }
                }
            }

            return returnList;
        }

        /// <summary>
        /// Get details of the selected patient.
        /// </summary>
        /// <param name="patientFamilyName">Patient FamilyName.</param>
        private void GetPatientDetails(string patientFamilyName)
        {
            XmlTextReader reader = new XmlTextReader("SampleData/XMLPatientData.xml");
            while (reader.Read())
            {
                // keep reading to find Patient elements
                if (reader.Name.Equals("Patient") && (reader.NodeType == XmlNodeType.Element))
                {
                    if (reader.GetAttribute("FamilyName") == patientFamilyName)
                    {
                        this.ClearControl();
                        this.patientBanner.FamilyName = reader.GetAttribute("FamilyName");
                        while (reader.NodeType != XmlNodeType.EndElement && reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                switch (reader.Name)
                                {
                                    case "GivenName":
                                        this.patientBanner.GivenName = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "Title":
                                        this.patientBanner.Title = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "DateOfBirth":
                                        this.patientBanner.DateOfBirth = DateTime.Parse(reader.ReadString(), CultureInfo.InvariantCulture);
                                        reader.Read();
                                        break;
                                    case "DateOfDeath":
                                        this.patientBanner.DateOfDeath = DateTime.Parse(reader.ReadString(), CultureInfo.InvariantCulture);
                                        reader.Read();
                                        break;
                                    case "Identifier":
                                        this.patientBanner.Identifier = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "Gender":
                                        this.patientBanner.Gender = (NhsCui.Toolkit.PatientGender)Enum.Parse(typeof(NhsCui.Toolkit.PatientGender), reader.ReadString(), true);
                                        reader.Read();
                                        break;
                                    case "HomePhoneNumber":
                                        this.patientBanner.HomePhoneNumber = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "WorkPhoneNumber":
                                        this.patientBanner.WorkPhoneNumber = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "MobilePhoneNumber":
                                        this.patientBanner.MobilePhoneNumber = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "EmailAddress":
                                        this.patientBanner.EmailAddress = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "Address1":
                                        this.patientBanner.Address1 = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "Address2":
                                        this.patientBanner.Address2 = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "Address3":
                                        this.patientBanner.Address3 = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "Town":
                                        this.patientBanner.Town = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "County":
                                        this.patientBanner.County = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "PostCode":
                                        this.patientBanner.Postcode = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "AccessKey":
                                    ////this.patientBanner.access
                                    ////this.patientBanner.AccessKey = reader.ReadString();
                                    //reader.ReadString();
                                    //reader.Read();
                                    //break;
                                    case "PatientImage":
                                        this.patientBanner.PatientImage = Image.FromFile(Application.StartupPath + reader.ReadString());
                                        reader.Read();
                                        break;
                                    case "AllergyInformation":
                                        this.patientBanner.AllergyInformation = (NhsCui.Toolkit.AllergyInformation)Enum.Parse(typeof(NhsCui.Toolkit.AllergyInformation), reader.ReadString(), true);
                                        if (this.patientBanner.AllergyInformation != NhsCui.Toolkit.AllergyInformation.Present)
                                        {
                                            this.patientBanner.Allergies.Clear();
                                        }
                                        else
                                        {
                                            this.AddAllergies();
                                        }

                                        reader.Read();
                                        break;
                                    case "PreferredName":
                                        this.patientBanner.PreferredName = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "PreferredNameLabel":
                                        string preferredNameLabel = reader.ReadString();
                                        if (String.IsNullOrEmpty(preferredNameLabel))
                                        {
                                            this.patientBanner.PreferredNameLabelText = null;
                                        }
                                        else
                                        {
                                            this.patientBanner.PreferredNameLabelText = preferredNameLabel.Trim();
                                        }

                                        reader.Read();
                                        break;
                                    case "GenderLabelTooltip":
                                        this.patientBanner.GenderLabelTooltip = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "GenderValueTooltip":
                                        this.patientBanner.GenderValueTooltip = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "IdentifierTooltip":
                                        this.patientBanner.IdentifierTooltip = reader.ReadString();
                                        reader.Read();
                                        break;
                                    case "IdentifierLabelTooltip":
                                        this.patientBanner.IdentifierLabelTooltip = reader.ReadString();
                                        reader.Read();
                                        break;
                                    default:
                                        reader.ReadString();
                                        reader.Read();
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        reader.Skip();
                    }
                }
            }
        }

        /// <summary>
        /// Add patient allergies.
        /// </summary>
        private void AddAllergies()
        {
            NhsCui.Toolkit.WinForms.Allergy allergy = new NhsCui.Toolkit.WinForms.Allergy();

            allergy.AllergyName = "Dust";
            allergy.LastUpdatedOn = new DateTime(2007, 6, 1);
            this.patientBanner.Allergies.Add(allergy);

            allergy = new NhsCui.Toolkit.WinForms.Allergy();
            allergy.AllergyName = "Smoke";
            allergy.LastUpdatedOn = new DateTime(2007, 6, 10);
            this.patientBanner.Allergies.Add(allergy);

            allergy = new NhsCui.Toolkit.WinForms.Allergy();
            allergy.AllergyName = "Perfume";
            allergy.LastUpdatedOn = new DateTime(2006, 6, 14);
            this.patientBanner.Allergies.Add(allergy);

            allergy = new NhsCui.Toolkit.WinForms.Allergy();
            allergy.AllergyName = "Latex";
            allergy.LastUpdatedOn = new DateTime(2004, 6, 21);
            this.patientBanner.Allergies.Add(allergy);

            allergy = new NhsCui.Toolkit.WinForms.Allergy();
            allergy.AllergyName = "Pollen";
            allergy.LastUpdatedOn = new DateTime(2005, 6, 11);
            this.patientBanner.Allergies.Add(allergy);

            allergy = new NhsCui.Toolkit.WinForms.Allergy();
            allergy.AllergyName = "Serum";
            allergy.LastUpdatedOn = new DateTime(2007, 1, 1);
            this.patientBanner.Allergies.Add(allergy);
        }

        /// <summary>
        /// Function to clear out the previous values.
        /// </summary>
        private void ClearControl()
        {
            //this.patientBanner.AccessKey = "";
            this.patientBanner.Address1 = "";
            this.patientBanner.Address2 = "";
            this.patientBanner.Address3 = "";
            this.patientBanner.DateOfBirth = new DateTime();
            this.patientBanner.DateOfDeath = DateTime.Parse("01/01/0001", CultureInfo.InvariantCulture);
            this.patientBanner.PreferredNameLabelText = null;
        }

        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the checked changed event of radio button.
        /// </summary>
        /// <param name="sender">Object calling the event.</param>
        /// <param name="e">Event arguments for the event.</param>
        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked)
            {
                this.dateInputBoxErrorDemo.CancelOnError = true;
                this.timeInputBoxErrorDemo.CancelOnError = true;
                this.timeSpanInputBoxErrorDemo.CancelOnError = true;
                this.dateInputBoxBirth.CancelOnError = true;
                this.dateInputBoxDeath.CancelOnError = true;
                this.timeInputBoxBirth.CancelOnError = true;
                this.timeInputBoxDeath.CancelOnError = true;
            }
            else
            {
                this.dateInputBoxErrorDemo.CancelOnError = false;
                this.timeInputBoxErrorDemo.CancelOnError = false;
                this.timeSpanInputBoxErrorDemo.CancelOnError = false;
                this.dateInputBoxBirth.CancelOnError = false;
                this.dateInputBoxDeath.CancelOnError = false;
                this.timeInputBoxBirth.CancelOnError = false;
                this.timeInputBoxDeath.CancelOnError = false;
            }
        }

        /// <summary>
        /// Handles the checked changed event of enable validation check box.
        /// </summary>
        /// <param name="sender">Object calling the event.</param>
        /// <param name="e">Event arguments for the event.</param>
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                this.dateInputBoxErrorDemo.ValidationManager = this.errorProviderValidationManager1;
                this.timeInputBoxErrorDemo.ValidationManager = this.errorProviderValidationManager1;
                this.timeSpanInputBoxErrorDemo.ValidationManager = this.errorProviderValidationManager1;
                this.dateInputBoxBirth.ValidationManager = this.DateValidatorBirth;
                this.dateInputBoxDeath.ValidationManager = this.dateValidatorDeath;
                this.timeInputBoxBirth.ValidationManager = this.timeValidatorBirth;
                this.timeInputBoxDeath.ValidationManager = this.timeValidatorDeath;
            }
            else
            {
                this.errorProviderValidationManager1.ClearError(this.dateInputBoxErrorDemo);
                this.errorProviderValidationManager1.ClearError(this.timeInputBoxErrorDemo);
                this.errorProviderValidationManager1.ClearError(this.timeSpanInputBoxErrorDemo);
                this.DateValidatorBirth.ClearError(this.dateInputBoxBirth);
                this.dateValidatorDeath.ClearError(this.dateInputBoxDeath);
                this.timeValidatorBirth.ClearError(this.timeInputBoxBirth);
                this.timeValidatorDeath.ClearError(this.timeValidatorDeath);
                this.dateInputBoxErrorDemo.ValidationManager = null;
                this.timeInputBoxErrorDemo.ValidationManager = null;
                this.timeSpanInputBoxErrorDemo.ValidationManager = null;
                this.dateInputBoxBirth.ValidationManager = null;
                this.dateInputBoxDeath.ValidationManager = null;
                this.timeInputBoxBirth.ValidationManager = null;
                this.timeInputBoxDeath.ValidationManager = null;
            }
        }

        /// <summary>
        /// Handles property changed event of approx check box.
        /// </summary>
        /// <param name="sender">Object calling the event.</param>
        /// <param name="e">Event arguments for the event.</param>
        private void TimeInputBox3_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.allowApproximateValueCheckBox.Enabled = this.timeInputBox3.TimeType == TimeType.Approximate || this.timeInputBox3.TimeType == TimeType.Exact;
        }

        /// <summary>
        /// Handles checked changed event of IsAge checkbox for timespaninputbox.
        /// </summary>
        /// <param name="sender">Object calling the event.</param>
        /// <param name="e">Event arguments.</param>
        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.timeSpanInputBox1.IsAge = this.checkBox2.Checked;
        }

        /// <summary>
        /// Handles the closing event of the form.
        /// </summary>
        /// <param name="sender">Object calling the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ToolkitControlsForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && e.Cancel)
            {
                e.Cancel = false;
            }
        }

        /// <summary>
        /// Handles the closed event of the form.
        /// </summary>
        /// <param name="sender">Object calling the event.</param>
        /// <param name="e">Event arguments.</param>
        private void ToolkitControlsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Event handler for View all addresses link in patient banner.
        /// </summary>
        /// <param name="sender">Patient banner for the event.</param>
        /// <param name="e">Event args for the event.</param>
        private void PatientBanner1_ViewAllAddressesClick(object sender, EventArgs e)
        {
            MessageBox.Show(Common.Resources.ViewAllAddressesClick, Common.Resources.PatientBannerCaption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
        }

        /// <summary>
        /// Event handler for View all contact details link in patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args for the event.</param>
        private void PatientBanner1_ViewAllContactDetailsClick(object sender, EventArgs e)
        {
            MessageBox.Show(Common.Resources.ViewAllContactDetailsClick, Common.Resources.PatientBannerCaption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
        }

        /// <summary>
        /// Event handler for View all addresses link in patient banner.
        /// </summary>
        /// <param name="sender">Patient banner control.</param>
        /// <param name="e">Event args for the event.</param>
        private void PatientBanner1_GenderValueClick(object sender, EventArgs e)
        {
            MessageBox.Show(Common.Resources.GenderValueClick, Common.Resources.PatientBannerCaption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
        }

        /// <summary>
        /// Event handler for View all addresses link in patient banner.
        /// </summary>
        /// <param name="sender">Patient banner control.</param>
        /// <param name="e">Event args for the event.</param>
        private void PatientBanner1_IdentifierClick(object sender, EventArgs e)
        {
            MessageBox.Show(Common.Resources.IdentifierClick, Common.Resources.PatientBannerCaption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, 0);
        }

        /// <summary>
        /// Raised when the seleced value in the listbox is changed.
        /// </summary>
        /// <param name="sender">Sender of the Event.</param>
        /// <param name="e">Event arguments for the event.</param>
        private void PatientListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.medsListViewElementHost.Child = new WrapDataGridPage((patientListBox.SelectedIndex + 1).ToString(CultureInfo.InvariantCulture));
            this.GetPatientDetails(patientListBox.SelectedValue.ToString());
        }
        #endregion
    }
}
