//-----------------------------------------------------------------------
// <copyright file="DosePicker.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>25-Sep-2009</date>
// <summary>
//      A control for picking a dose.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    ///  A control for picking a dose.
    /// </summary>
    public partial class DosePicker : UserControl
    {
        /// <summary>
        /// The SelectedDose Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedDoseProperty =
            DependencyProperty.Register("SelectedDose", typeof(string), typeof(DosePicker), new PropertyMetadata(string.Empty, new PropertyChangedCallback(SelectedDose_Changed)));

        /// <summary>
        /// Stores if the selected dose is being updated.
        /// </summary>
        private bool updatingSelectedDose;

        /// <summary>
        /// Initializes a new instance of the DosePicker class.
        /// </summary>
        public DosePicker()
        {
            InitializeComponent();
            this.DoseTextBox.TextChanged += new TextChangedEventHandler(this.DoseTextBox_TextChanged);
            this.GotFocus += new RoutedEventHandler(this.DosePicker_GotFocus);
        }

        /// <summary>
        /// Gets or sets the selected dose.
        /// </summary>
        /// <value>The selected dose value.</value>
        public string SelectedDose
        {
            get { return (string)GetValue(SelectedDoseProperty); }
            set { SetValue(SelectedDoseProperty, value); }
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        /// <param name="obj">The dose picker.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedDose_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            DosePicker dosePicker = obj as DosePicker;
            dosePicker.UpdateUI();
        }

        /// <summary>
        /// Updates the selected dose.
        /// </summary>
        /// <param name="sender">The dose text box.</param>
        /// <param name="e">Text Changed Event Args.</param>
        private void DoseTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateSelectedDose();
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        private void UpdateUI()
        {
            if (!this.updatingSelectedDose)
            {
                if (this.SelectedDose != null && this.SelectedDose.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length > 1)
                {
                    this.DoseTextBox.Text = this.SelectedDose.Replace(this.SelectedDose.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[this.SelectedDose.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length - 1], string.Empty).Trim();
                }
                else
                {
                    this.DoseTextBox.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// Updates the selected dose.
        /// </summary>
        private void UpdateSelectedDose()
        {
            this.updatingSelectedDose = true;
            this.SelectedDose = string.Format(CultureInfo.CurrentCulture, "{0} {1}", this.DoseTextBox.Text, this.DoseUnit.Text);
            this.updatingSelectedDose = false;
        }

        /// <summary>
        /// Sets focus to the dose text box.
        /// </summary>
        /// <param name="sender">The dose picker.</param>
        /// <param name="e">Routed Event Args.</param>
        private void DosePicker_GotFocus(object sender, RoutedEventArgs e)
        {
            this.DoseTextBox.Focus();
        }
    }
}
