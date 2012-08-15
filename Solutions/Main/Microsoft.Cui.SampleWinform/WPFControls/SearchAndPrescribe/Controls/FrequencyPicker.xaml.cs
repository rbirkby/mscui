//-----------------------------------------------------------------------
// <copyright file="FrequencyPicker.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      A control for picking a frequency.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// A control for picking a frequency.
    /// </summary>
    public partial class FrequencyPicker : UserControl
    {
        /// <summary>
        /// The SelectedFrequency Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedFrequencyProperty =
            DependencyProperty.Register("SelectedFrequency", typeof(string), typeof(FrequencyPicker), new PropertyMetadata(string.Empty, new PropertyChangedCallback(SelectedDose_Changed)));

        /// <summary>
        /// Stores if the selected dose is being updated.
        /// </summary>
        private bool updatingSelectedFrequency;

        /// <summary>
        /// Initializes a new instance of the FrequencyPicker class.
        /// </summary>
        public FrequencyPicker()
        {
            InitializeComponent();
            this.FrequencyTextBox.TextChanged += new TextChangedEventHandler(this.FrequencyTextBox_TextChanged);
            this.GotFocus += new RoutedEventHandler(this.FrequencyPicker_GotFocus);
        }

        /// <summary>
        /// Gets or sets the selected frequency.
        /// </summary>
        /// <value>The selected frequency value.</value>
        public string SelectedFrequency
        {
            get { return (string)GetValue(SelectedFrequencyProperty); }
            set { SetValue(SelectedFrequencyProperty, value); }
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        /// <param name="obj">The frequency picker.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedDose_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            FrequencyPicker frequencyPicker = obj as FrequencyPicker;
            frequencyPicker.UpdateUI();
        }

        /// <summary>
        /// Updates the selected frequency.
        /// </summary>
        /// <param name="sender">The frequency text box.</param>
        /// <param name="e">Text Changed Event Args.</param>
        private void FrequencyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateSelectedFrequency();
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        private void UpdateUI()
        {
            if (this.SelectedFrequency != null && !this.updatingSelectedFrequency)
            {
                this.FrequencyTextBox.Text = this.SelectedFrequency.Replace("every ", "").Replace(" hours", "").Replace(" hour", string.Empty).Trim();
            }
        }

        /// <summary>
        /// Updates the selected frequency.
        /// </summary>
        private void UpdateSelectedFrequency()
        {
            this.updatingSelectedFrequency = true;

            int hours = 0;
            if (int.TryParse(this.FrequencyTextBox.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out hours) && hours == 1)
            {
                this.SelectedFrequency = string.Format(CultureInfo.CurrentCulture, "every {0} hour", this.FrequencyTextBox.Text);
            }
            else
            {
                this.SelectedFrequency = string.Format(CultureInfo.CurrentCulture, "every {0} hours", this.FrequencyTextBox.Text);
            }

            this.updatingSelectedFrequency = false;
        }

        /// <summary>
        /// Moves focus to the frequency picker text box.
        /// </summary>
        /// <param name="sender">The frequency picker.</param>
        /// <param name="e">Routed Event Args.</param>
        private void FrequencyPicker_GotFocus(object sender, RoutedEventArgs e)
        {
            this.FrequencyTextBox.Focus();
        }
    }
}
