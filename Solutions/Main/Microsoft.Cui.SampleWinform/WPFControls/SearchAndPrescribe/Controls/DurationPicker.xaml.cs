//-----------------------------------------------------------------------
// <copyright file="DurationPicker.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      A control for picking a duration.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// A control for picking a duration.
    /// </summary>
    public partial class DurationPicker : UserControl
    {
        /// <summary>
        /// The SelectedDuration Dependecy Property.
        /// </summary>
        public static readonly DependencyProperty SelectedDurationProperty =
            DependencyProperty.Register("SelectedDuration", typeof(TimeSpan), typeof(DurationPicker), new PropertyMetadata(TimeSpan.FromMilliseconds(1), new PropertyChangedCallback(SelectedDuration_Changed)));

        /// <summary>
        /// The DisplayDuration Dependecy Property.
        /// </summary>
        public static readonly DependencyProperty DisplayDurationProperty =
            DependencyProperty.Register("DisplayDuration", typeof(string), typeof(DurationPicker), null);

        /// <summary>
        /// Store whether the selected duration is being updated.
        /// </summary>
        private bool updatingSelectedDuration;

        /// <summary>
        /// Initializes a new instance of the DurationPicker class.
        /// </summary>
        public DurationPicker()
        {
            InitializeComponent();
            this.DurationTextBox.TextChanged += new TextChangedEventHandler(this.DurationTextBox_TextChanged);
            this.DurationUnitsComboBox.SelectionChanged += new SelectionChangedEventHandler(this.DurationUnitsComboBox_SelectionChanged);
            this.LostFocus += new RoutedEventHandler(this.DurationPicker_LostFocus);
            this.GotFocus += new RoutedEventHandler(this.DurationPicker_GotFocus);
        }

        /// <summary>
        /// Gets or sets the selected duration.
        /// </summary>
        /// <value>The selected duration value.</value>
        public TimeSpan SelectedDuration
        {
            get { return (TimeSpan)GetValue(SelectedDurationProperty); }
            set { SetValue(SelectedDurationProperty, value); }
        }

        /// <summary>
        /// Gets or sets the display duration.
        /// </summary>
        /// <value>The display duration.</value>
        public string DisplayDuration
        {
            get { return (string)GetValue(DisplayDurationProperty); }
            set { SetValue(DisplayDurationProperty, value); }
        }

        /// <summary>
        /// Updates the UI.
        /// </summary>
        /// <param name="obj">The duration picker.</param>
        /// <param name="args">Dependency Propert yChanged Event Args.</param>
        private static void SelectedDuration_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            DurationPicker durationPicker = obj as DurationPicker;
            if (!durationPicker.updatingSelectedDuration)
            {
                durationPicker.UpdateUI();
            }
        }

        /// <summary>
        /// Updates the selected duration.
        /// </summary>
        /// <param name="sender">The duration units combo box.</param>
        /// <param name="e">Selection Changed Event Args.</param>
        private void DurationUnitsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.UpdateDuration();
        }

        /// <summary>
        /// Updates the selected duration.
        /// </summary>
        /// <param name="sender">The duration text box.</param>
        /// <param name="e">Text Changed Event Args.</param>
        private void DurationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.UpdateDuration();
        }

        /// <summary>
        /// Updates the selected duration based on the UI controls.
        /// </summary>
        private void UpdateDuration()
        {
            this.updatingSelectedDuration = true;
            double value = 0;
            if (double.TryParse(this.DurationTextBox.Text, out value))
            {
                if (value >= 1)
                {
                    this.DisplayDuration = string.Format(CultureInfo.InvariantCulture, "{0} {1}", this.DurationTextBox.Text, this.DurationUnitsComboBox.SelectedItem.ToString());

                    switch (this.DurationUnitsComboBox.SelectedItem.ToString())
                    {
                        case "hours":
                            this.SelectedDuration = TimeSpan.FromHours(value);
                            break;
                        case "days":
                            this.SelectedDuration = TimeSpan.FromDays(value);
                            break;
                        case "weeks":
                            this.SelectedDuration = TimeSpan.FromDays(value * 7);
                            break;
                        case "months":
                            this.SelectedDuration = TimeSpan.FromDays(Math.Ceiling(value * (365.0 / 12.0)));
                            break;
                    }
                }
            }

            this.updatingSelectedDuration = false;
        }

        /// <summary>
        /// Updates the UI elements to the new selected duration.
        /// </summary>
        private void UpdateUI()
        {
            if (!this.updatingSelectedDuration)
            {
                if (!string.IsNullOrEmpty(this.DisplayDuration))
                {
                    string[] displayDurationParts = this.DisplayDuration.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    double value = 0;
                    if (displayDurationParts.Length > 1 && double.TryParse(displayDurationParts[0], out value))
                    {
                        if (value >= 1)
                        {
                            this.DurationTextBox.Text = value.ToString(CultureInfo.CurrentCulture);
                        }
                        else
                        {
                            this.DurationTextBox.Text = displayDurationParts[0];
                        }
                    }
                    else
                    {
                        this.DurationTextBox.Text = string.Empty;
                        this.DurationUnitsComboBox.SelectedItem = "days";
                    }

                    if (displayDurationParts.Length >= 2 && this.DurationUnitsComboBox.Items.Contains(displayDurationParts[1]))
                    {
                        this.DurationUnitsComboBox.SelectedItem = displayDurationParts[1];
                    }
                }
                else
                {
                    this.DurationTextBox.Text = string.Empty;
                    this.DurationUnitsComboBox.SelectedItem = "days";
                }
            }
        }

        /// <summary>
        /// Removes the composition target render handler for lost focus.
        /// </summary>
        /// <param name="sender">The duration picker.</param>
        /// <param name="e">Routed Event Args.</param>
        private void DurationPicker_GotFocus(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= new EventHandler(this.DurationPickerLostFocus_CompositionTargetRendering);

            if (e.OriginalSource == this)
            {
                this.DurationTextBox.Focus();
            }
        }

        /// <summary>
        /// Sets control to display selected date, after next UI render.
        /// </summary>
        /// <param name="sender">The duration picker.</param>
        /// <param name="e">Routed Event Args.</param>
        private void DurationPicker_LostFocus(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= new EventHandler(this.DurationPickerLostFocus_CompositionTargetRendering);
            CompositionTarget.Rendering += new EventHandler(this.DurationPickerLostFocus_CompositionTargetRendering);
        }

        /// <summary>
        /// Sets control to display selected date, after next UI render.
        /// </summary>
        /// <param name="sender">The composition target.</param>
        /// <param name="e">Routed Event Args.</param>
        private void DurationPickerLostFocus_CompositionTargetRendering(object sender, EventArgs e)
        {
            CompositionTarget.Rendering -= new EventHandler(this.DurationPickerLostFocus_CompositionTargetRendering);
            this.UpdateUI();
        }
    }
}
