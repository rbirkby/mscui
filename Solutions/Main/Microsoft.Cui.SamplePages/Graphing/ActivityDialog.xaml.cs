//-----------------------------------------------------------------------
// <copyright file="ActivityDialog.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>19-Nov-2009</date>
// <summary>Control to show activity details.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using Microsoft.Cui.Controls;
#if !SILVERLIGHT
    using Microsoft.Cui.SampleWinform;    
#endif
    using Microsoft.Cui.Controls.Common.DateAndTime;

    /// <summary>
    /// Control to show activity details.
    /// </summary>
    public partial class ActivityDialog : UserControl
    {
        /// <summary>
        /// Label content control to display event status.
        /// </summary>
        private LabeledContentControl eventStatus = new LabeledContentControl();

        /// <summary>
        /// Label content control to display planned start time.
        /// </summary>
        private LabeledContentControl plannedStartDate = new LabeledContentControl();

        /// <summary>
        /// Label content control to display planned end time.
        /// </summary>
        private LabeledContentControl plannedEndDate = new LabeledContentControl();

        /// <summary>
        /// Label content control to display actual start time.
        /// </summary>
        private LabeledContentControl actualStartDate = new LabeledContentControl();
        
        /// <summary>
        /// Label content control to display actual end time.
        /// </summary>
        private LabeledContentControl actualEndDate = new LabeledContentControl();

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityDialog"/> class.
        /// </summary>
        public ActivityDialog()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(this.ActivityDialog_Loaded);
        }        

        /// <summary>
        /// Occurs when [close].
        /// </summary>
        public event RoutedEventHandler Close;
        
        /// <summary>
        /// Updates the display.
        /// </summary>
        public void UpdateDisplay()
        {
            GraphPoint graphPoint = this.DataContext as GraphPoint;
            if (graphPoint != null)
            {
                this.EventsCloseButton.IsTabStop = true;
                TimelineMedicationDetails medicationDetails = (graphPoint.DataContext as TimeActivityPoint).AdditionalInformation as TimelineMedicationDetails;

                bool isContinuousAdministration = false;
                if (graphPoint.X2.HasValue || string.Compare("started", medicationDetails.Status, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    isContinuousAdministration = true;
                }

                this.ResetLabels(isContinuousAdministration);
                
                switch (medicationDetails.Status.ToLower(CultureInfo.CurrentCulture))
                {
                    case "late":
                        this.actualStartDate.Content = GetDate(graphPoint.X1);
                        this.actualEndDate.Content = GetDate(graphPoint.X2);
                        this.plannedStartDate.Content = GetDate(medicationDetails.PlannedStartDate);
                        this.plannedEndDate.Content = GetDate(medicationDetails.PlannedEndDate);
                        break;
                    case "planned":
                        this.actualStartDate.Content = string.Empty;
                        this.actualEndDate.Content = string.Empty;
                        this.plannedStartDate.Content = GetDate(medicationDetails.PlannedStartDate);
                        this.plannedEndDate.Content = GetDate(medicationDetails.PlannedEndDate);
                        break;
                    case "completed":
                        this.actualStartDate.Content = GetDate(graphPoint.X1);
                        this.actualEndDate.Content = GetDate(graphPoint.X2);
                        this.plannedStartDate.Content = string.Empty;
                        this.plannedEndDate.Content = string.Empty;
                        break;
                    case "started":
                        this.actualStartDate.Content = GetDate(graphPoint.X1);
                        this.actualEndDate.Content = string.Empty;
                        this.plannedStartDate.Content = string.Empty;
                        this.plannedEndDate.Content = string.Empty;
                        break;
                    case "issued":
                        this.DialogTitle.Text = "Issue event";
                        this.eventStatus.Label = "prescription status";
                        this.actualStartDate.Content = GetDate(graphPoint.X1);
                        this.actualEndDate.Content = string.Empty;
                        this.plannedStartDate.Content = string.Empty;
                        this.plannedEndDate.Content = string.Empty;
                        break;
                    default:
                        break;
                }

                this.eventStatus.Content = medicationDetails.Status;

                this.actualStartDate.Visibility = string.IsNullOrEmpty(this.actualStartDate.Content.ToString()) ? Visibility.Collapsed : Visibility.Visible;
                this.actualEndDate.Visibility = string.IsNullOrEmpty(this.actualEndDate.Content.ToString()) ? Visibility.Collapsed : Visibility.Visible;
                this.plannedStartDate.Visibility = string.IsNullOrEmpty(this.plannedStartDate.Content.ToString()) ? Visibility.Collapsed : Visibility.Visible;
                this.plannedEndDate.Visibility = string.IsNullOrEmpty(this.plannedEndDate.Content.ToString()) ? Visibility.Collapsed : Visibility.Visible;

                switch (medicationDetails.Status.ToLower(CultureInfo.CurrentCulture))
                {
                    case "late":
                        UpdateLabels(this.actualStartDate, this.actualEndDate, "actual time");
                        UpdateLabels(this.plannedStartDate, this.plannedEndDate, "planned time");
                        break;
                    case "planned":
                        UpdateLabels(this.plannedStartDate, this.plannedEndDate, "planned time");
                        break;
                    case "completed":                        
                    case "started":
                        if (!isContinuousAdministration)
                        {
                            UpdateLabels(this.actualStartDate, this.actualEndDate, "actual time");
                        }
                        else
                        {
                            UpdateLabels(this.actualStartDate, this.actualEndDate, "start time");
                        }
                        break;
                    case "issued":
                        this.actualStartDate.Label = "actual time";                        
                        break;
                    default:
                        break;
                }

#if !SILVERLIGHT
                FocusHelper.FocusControl(this.EventsCloseButton);    
#endif
            }

        }        

        /// <summary>
        /// Updates the labels.
        /// </summary>
        /// <param name="startLabel">The start labeledcontentcontrol.</param>
        /// <param name="endLabel">The end labeledcontentcontrol.</param>
        /// <param name="newLabel">New label text for the start labeledcontentcontrol.</param>
        private static void UpdateLabels(LabeledContentControl startLabel, LabeledContentControl endLabel, string newLabel)
        {
            bool changeActualDateLabel = true;
            if (endLabel.Visibility == Visibility.Visible)
            {
                DateTime endDate = DateTime.Parse(endLabel.Content.ToString(), CultureInfo.CurrentCulture);
                DateTime startDate = DateTime.Parse(startLabel.Content.ToString(), CultureInfo.CurrentCulture);
                if (!endDate.Equals(startDate))
                {
                    changeActualDateLabel = false;
                }
            }

            if (changeActualDateLabel && startLabel.Visibility == Visibility.Visible)
            {
                startLabel.Label = newLabel;
            }
        }
        
        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>DateTime formatted in CuiDate format if the date has value.</returns>
        private static string GetDate(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToString("dd-MMM-yyyy HH:mm", CultureInfo.CurrentCulture);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Resets the labels.
        /// </summary>
        /// <param name="continuousAdministration">If set to <c>true</c> [continuous administration].</param>
        private void ResetLabels(bool continuousAdministration)
        {
            this.DialogTitle.Text = "Administrations";
            this.eventStatus.Label = "status";
            this.plannedStartDate.Label = "planned start time";
            this.plannedEndDate.Label = "planned end time";
            if (continuousAdministration)
            {
                this.actualStartDate.Label = "start time";
                this.actualEndDate.Label = "end time";
            }
            else
            {
                this.actualStartDate.Label = "actual start time";
                this.actualEndDate.Label = "actual end time";
            }
        }

        /// <summary>
        /// Handles the Loaded event of the ActivityDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ActivityDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.eventStatus.Style = this.LayoutRoot.Resources["LabelStyle"] as Style;
            this.actualStartDate.Style = this.eventStatus.Style;
            this.actualEndDate.Style = this.eventStatus.Style;
            this.plannedStartDate.Style = this.eventStatus.Style;
            this.plannedEndDate.Style = this.eventStatus.Style;

            Grid.SetRow(this.eventStatus, 1);
            Grid.SetRow(this.actualStartDate, 2);
            Grid.SetRow(this.actualEndDate, 3);
            Grid.SetRow(this.plannedStartDate, 4);
            Grid.SetRow(this.plannedEndDate, 5);

            if (!this.Dialog.Children.Contains(this.eventStatus))
            {
                this.Dialog.Children.Add(this.eventStatus);
                this.Dialog.Children.Add(this.actualStartDate);
                this.Dialog.Children.Add(this.actualEndDate);
                this.Dialog.Children.Add(this.plannedStartDate);
                this.Dialog.Children.Add(this.plannedEndDate);
            }
        }

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.EventsCloseButton.IsTabStop = false;
            if (this.Close != null)
            {
                this.Close(sender, e);
            }
        }        
    }
}
