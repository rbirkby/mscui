//-----------------------------------------------------------------------
// <copyright file="WrapDataGridPage.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>15-Apr-2008</date>
// <summary>The WrapDataGridPage sample WPF page. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.SampleWinform
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Microsoft.Cui.Controls;
    using Microsoft.Cui.IsvData;
    using Microsoft.Cui.Data;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System;
    using System.Text;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for WrapDataGridPage.xaml.
    /// </summary>
    public partial class WrapDataGridPage : UserControl
    {
        /// <summary>
        /// Private member to hold notification grid.
        /// </summary>
        private Grid notificationArea;

        /// <summary>
        /// Private member to hold notification template.
        /// </summary>
        private TextBlock notification;

        /// <summary>
        /// Private member to hold the selected row id's in the grid.
        /// </summary>
        private List<int> selectedRowIds = new List<int>();

        /// <summary>
        /// Private member to hold the currently selected Control.
        /// </summary>
        private Control previouslySelectedControl;

        /// <summary>
        /// Grouping animation timer.
        /// </summary>
        private DispatcherTimer groupingAnimationTimer;

        /// <summary>
        /// Filtering animation timer.
        /// </summary>
        private DispatcherTimer filteringAnimationTimer;

        /// <summary>
        /// Level of detail animation timer.
        /// </summary>
        private DispatcherTimer levelOfDetailAnimationTimer;

        /// <summary>
        /// Reload animation timer.
        /// </summary>
        private DispatcherTimer reloadAnimationTimer;

        /// <summary>
        /// Private member to hold the patient ID for which the details are displayed.
        /// </summary>
        private string patientId = string.Empty;

        /// <summary>
        /// The Host Page.
        /// </summary>
        public WrapDataGridPage()
        {
            // Required to initialize variables
            this.Cursor = Cursors.Wait;
            InitializeComponent();
            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Constructor accepting a patient id.
        /// </summary>
        /// <param name="patientId">Patient id to initialise the control with.</param>
        public WrapDataGridPage(string patientId)
            : this()
        {
            this.Cursor = Cursors.Wait;
            this.patientId = patientId;
            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Gets the ISV data provider object defined in XAML.
        /// </summary>
        private IsvDataProvider PageIsvDataProvider
        {
            get
            {
                return this.Resources["IsvDataProvider"] as IsvDataProvider;
            }
        }

        /// <summary>
        /// Gets the Data manager object defined in XAML.
        /// </summary>
        private DataManager PageDataManager
        {
            get
            {
                return this.Resources["DataManager"] as DataManager;
            }
        }

        /// <summary>
        /// Gets Rule strategy object defined in XAML.
        /// </summary>
        private RuleStrategy PageRuleStrategy
        {
            get
            {
                return this.Resources["RuleStrategy"] as RuleStrategy;
            }
        }

        /// <summary>
        /// Gets the notification message to be shown for a filter condition.
        /// </summary>
        /// <param name="filter">Filter condition for the message.</param>
        /// <returns>Returns the message to be shown in notification area.</returns>
        private static string GetNotificationMessage(FilterCondition filter)
        {
            string notificationText = "The list is filtered to show: ";

            switch (filter)
            {
                case FilterCondition.Past:
                    notificationText = string.Empty;
                    break;
                case FilterCondition.PastTwoMonths:
                    notificationText += " Past 2 months medications";
                    break;
                case FilterCondition.PastSixMonths:
                    notificationText += " Past 6 months medications";
                    break;
                case FilterCondition.Current:
                    notificationText = string.Empty;
                    break;
            }

            return notificationText;
        }

        /// <summary>
        /// Handles the ColumnHeaderClick event of the WrapDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e1">The <see cref="Microsoft.Cui.Controls.ColumnHeaderClickEventArgs"/> instance containing the event data.</param>
        private void WrapDataGrid_ColumnHeaderClick(object sender, System.EventArgs e1)
        {
            ColumnHeaderClickEventArgs e = e1 as ColumnHeaderClickEventArgs;
            if (this.PageIsvDataProvider != null && this.PageDataManager != null)
            {
                this.GetSelectedRows();
                string sortColumnName = string.Empty;

                switch (e.ColumnIndex)
                {
                    case 1:
                        sortColumnName = "StartDate";
                        this.PageIsvDataProvider.ColumnDataType = ColumnDataType.DateTime;
                        break;
                    case 2:
                        sortColumnName = "DrugName";
                        this.PageIsvDataProvider.ColumnDataType = ColumnDataType.String;
                        break;
                    case 3:
                        sortColumnName = "Reason";
                        this.PageIsvDataProvider.ColumnDataType = ColumnDataType.String;
                        break;
                    case 4:
                        sortColumnName = "MedicationStatus";
                        this.PageIsvDataProvider.ColumnDataType = ColumnDataType.String;
                        break;
                    case 5:
                        sortColumnName = "StopDate";
                        this.PageIsvDataProvider.ColumnDataType = ColumnDataType.DateTime;
                        break;
                    default:
                        sortColumnName = "DrugName";
                        this.PageIsvDataProvider.ColumnDataType = ColumnDataType.String;
                        break;
                }

                if (string.Compare(sortColumnName, this.PageIsvDataProvider.SortColumn, System.StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    if (this.PageIsvDataProvider.SortDirection == Microsoft.Cui.IsvData.SortDirection.Ascending)
                    {
                        this.PageIsvDataProvider.SortDirection = Microsoft.Cui.IsvData.SortDirection.Descending;
                    }
                    else
                    {
                        this.PageIsvDataProvider.SortDirection = Microsoft.Cui.IsvData.SortDirection.Ascending;
                    }
                }
                else
                {
                    this.PageIsvDataProvider.SortDirection = Microsoft.Cui.IsvData.SortDirection.Ascending;
                }

                this.WrapDataGrid.ClearSortIndicators();

                if (this.PageIsvDataProvider.SortDirection == Microsoft.Cui.IsvData.SortDirection.Ascending)
                {
                    this.WrapDataGrid.AddSortIndicator(e.ColumnIndex, Microsoft.Cui.Controls.SortDirection.Ascending);
                }
                else
                {
                    this.WrapDataGrid.AddSortIndicator(e.ColumnIndex, Microsoft.Cui.Controls.SortDirection.Descending);
                }

                this.PageIsvDataProvider.SortColumn = sortColumnName;

                this.PageDataManager.Flush();

                if (this.WrapDataGrid != null)
                {
                    this.WrapDataGrid.ShowAnimation(true);
                    this.reloadAnimationTimer = new DispatcherTimer();
                    this.reloadAnimationTimer.Interval = TimeSpan.FromSeconds(0.5);
                    this.reloadAnimationTimer.Tick += delegate { this.HandleReloading(); };
                    this.reloadAnimationTimer.Start();
                }                
            }
        }

        /// <summary>
        /// Handles the reloading of the grid.
        /// </summary>
        private void HandleReloading()
        {
            if (this.WrapDataGrid != null)
            {
                this.WrapDataGrid.Reload();                
                this.SetSelectedRows();
                this.WrapDataGrid.ShowAnimation(false);
                this.reloadAnimationTimer.Stop();
            }
        }

        /// <summary>
        /// Handles the OnFilter event of the FilterControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e1">The <see cref="Microsoft.Cui.Controls.FilterEventArgs"/> instance containing the event data.</param>
        private void FilterControl_OnFilter(object sender, System.EventArgs e1)
        {
            FilterEventArgs e = e1 as FilterEventArgs;
            if (this.PageIsvDataProvider != null && this.PageDataManager != null)
            {               
                FilterCondition newFilterCondition = (FilterCondition)System.Enum.Parse(typeof(FilterCondition), e.FilterCondition, true);

                // Don't refresh unnecessarily...
                if (this.PageIsvDataProvider.FilterCondition == newFilterCondition)
                {
                   return;
                }

                if (this.patientId == "1")
                {
                    this.PageIsvDataProvider.CustomDate = new DateTime(2007, 8, 1);
                }
                else if (this.patientId == "2")
                {
                    this.PageIsvDataProvider.CustomDate = new DateTime(2008, 3, 30);
                }

                this.PageIsvDataProvider.FilterCondition = newFilterCondition;

                this.WrapDataGrid.SelectedItems.Clear();
                this.WrapDataGrid.ClearSortIndicators();

                if (this.WrapDataGrid != null)
                {
                    this.WrapDataGrid.ShowAnimation(true);
                    this.WrapDataGrid.SelectedItems.Clear();
                    this.WrapDataGrid.SelectedIndex = -1;
                    this.WrapDataGrid.SelectedItem = null;
                    this.WrapDataGrid.ClearSortIndicators();
                }

                if (this.PageIsvDataProvider.FilterCondition == FilterCondition.Current)
                {
                    this.PageIsvDataProvider.SortColumn = "StartDate";
                    this.PageIsvDataProvider.SortDirection = Microsoft.Cui.IsvData.SortDirection.Descending;
                    this.PageIsvDataProvider.ColumnDataType = ColumnDataType.DateTime;
                    this.WrapDataGrid.AddSortIndicator(1, Microsoft.Cui.Controls.SortDirection.Descending);
                }
                else
                {
                    this.PageIsvDataProvider.SortColumn = "StopDate";
                    this.PageIsvDataProvider.SortDirection = Microsoft.Cui.IsvData.SortDirection.Descending;
                    this.PageIsvDataProvider.ColumnDataType = ColumnDataType.DateTime;
                    this.WrapDataGrid.AddSortIndicator(5, Microsoft.Cui.Controls.SortDirection.Descending);
                }

                this.PageDataManager.Flush();
                if (this.WrapDataGrid != null)
                {
                    this.filteringAnimationTimer = new DispatcherTimer();
                    this.filteringAnimationTimer.Interval = TimeSpan.FromSeconds(0.5);
                    this.filteringAnimationTimer.Tick += delegate { this.HandleFiltering(); };
                    this.filteringAnimationTimer.Start();
                }                
            }
        }

        /// <summary>
        /// Handles the filtering for the grid.
        /// </summary>
        private void HandleFiltering()
        {
            this.WrapDataGrid.Reload();
            this.AddNotification(this.PageIsvDataProvider.FilterCondition);
            this.WrapDataGrid.ShowAnimation(false);
            this.filteringAnimationTimer.Stop();            
            this.WrapDataGrid.HandleLostFocus();
        }

        /// <summary>
        /// Handles the Level of details changed event.
        /// </summary>
        /// <param name="sender">Level of detail control.</param>
        /// <param name="e1">Event argument.</param>
        private void LevelOfDetailControl_OnLevelOfDetailSliderValueChanged(object sender, System.EventArgs e1)
        {
            RoutedPropertyChangedEventArgs<double> e = e1 as RoutedPropertyChangedEventArgs<double>;
            if (this.PageIsvDataProvider != null && this.PageDataManager != null && this.PageRuleStrategy != null)
            {
                this.GetSelectedRows();

                bool rangeMessageRequired = false;

                // Currently only support levels 1 and 2...
                if (e.NewValue <= 2)
                {
                    this.PageRuleStrategy.LevelName = "Level" + ((int)e.NewValue).ToString(System.Globalization.CultureInfo.CurrentCulture);
                }
                else
                {
                    rangeMessageRequired = true;
                    this.PageRuleStrategy.LevelName = "Level2";
                }

                this.PageDataManager.Flush();
                if (this.WrapDataGrid != null)
                {
                    this.WrapDataGrid.ShowAnimation(true);
                    this.levelOfDetailAnimationTimer = new DispatcherTimer();
                    this.levelOfDetailAnimationTimer.Interval = TimeSpan.FromSeconds(0.5);
                    this.levelOfDetailAnimationTimer.Tick += delegate { this.HandleLevelOfDetailChange(rangeMessageRequired); };
                    this.levelOfDetailAnimationTimer.Start();
                }
            }
        }

         /// <summary>
        /// Handles the level of detail change for the grid.
        /// </summary>
        /// <param name="rangeMessageRequired">
        /// Boolean flag value.
        /// </param>
        public void HandleLevelOfDetailChange(bool rangeMessageRequired)
        {
            if (this.WrapDataGrid != null)
            {
                this.WrapDataGrid.Reload();
                this.SetSelectedRows();
                //// Alert is synchronous - show message last...
                if (rangeMessageRequired == true)
                {
                    MessageBox.Show("Although multiple Levels of Detail can be defined, at present this sample only illustrates two.");
                }
                               
                this.WrapDataGrid.ShowAnimation(false);
                this.levelOfDetailAnimationTimer.Stop();
                this.LevelOfDetailControl.Focus();
                this.WrapDataGrid.HandleLostFocus();
            }
        }

        /// <summary>
        /// Handles the selected index changed for the grouping control.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e1">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void Grouping_SelectedIndexChanged(object sender, System.EventArgs e1)
        {
            SelectionChangedEventArgs e = e1 as SelectionChangedEventArgs;
            if (this.WrapDataGrid != null && this.PageIsvDataProvider != null && this.PageDataManager != null && e != null)
            {               
                this.GetSelectedRows();

                this.WrapDataGrid.UpdateGroupingColumnSize();
                DataTemplate groupingTemplate = null;
                string groupingKey = (this.Grouping.Items[this.Grouping.SelectedIndex] as ListBoxItem).Tag.ToString();

                if (string.IsNullOrEmpty(groupingKey))
                {
                    groupingTemplate = null;
                }
                else
                {
                    switch (groupingKey)
                    {
                        case "MedicationStatus":
                            groupingTemplate = this.LayoutRoot.Resources["groupingTemplateLogicMedicationStatus"] as DataTemplate;
                            this.PageIsvDataProvider.ColumnDataType = ColumnDataType.String;
                            break;
                        case "MedicationType":
                            groupingTemplate = this.LayoutRoot.Resources["groupingTemplateLogicMedicationType"] as DataTemplate;
                            this.PageIsvDataProvider.ColumnDataType = ColumnDataType.String;
                            break;
                        case "Prescriber":
                            groupingTemplate = this.LayoutRoot.Resources["groupingTemplateLogicPrescriber"] as DataTemplate;
                            this.PageIsvDataProvider.ColumnDataType = ColumnDataType.String;
                            break;
                        case "Reason":
                            groupingTemplate = this.LayoutRoot.Resources["groupingTemplateLogicReason"] as DataTemplate;
                            this.PageIsvDataProvider.ColumnDataType = ColumnDataType.String;
                            break;
                    }
                }

                this.PageIsvDataProvider.GroupBy = (this.Grouping.Items[this.Grouping.SelectedIndex] as ListBoxItem).Tag.ToString();
                this.PageDataManager.Flush();
                this.WrapDataGrid.ShowAnimation(true);
                this.WrapDataGrid.GroupingDataTemplateLogic = groupingTemplate;
                this.groupingAnimationTimer = new DispatcherTimer();
                this.groupingAnimationTimer.Interval = TimeSpan.FromSeconds(1);
                this.groupingAnimationTimer.Tick += delegate { this.HandleGrouping(); };
                this.groupingAnimationTimer.Start(); 
            }
        }

        /// <summary>
        /// Handles the grouping for the grid.
        /// </summary>
        private void HandleGrouping()
        {
            this.WrapDataGrid.Reload();
            if (this.WrapDataGrid.SelectedItems.Count == 1)
            {
                this.SetSelectedRows();
            }
            else
            {
                this.WrapDataGrid.SelectedItems.Clear();
                this.WrapDataGrid.SelectedIndex = -1;
            }
           
            this.WrapDataGrid.ShowAnimation(false);
            this.groupingAnimationTimer.Stop();
            this.WrapDataGrid.HandleLostFocus();
        }

        /// <summary>
        /// Handles the load event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.patientId) && this.PageIsvDataProvider.PatientId != this.patientId)
            {
                this.PageIsvDataProvider.PatientId = this.patientId;
                this.PageDataManager.Flush();
            }

            this.WrapDataGrid.LoadUpControl();

            this.PopulateDropDowns();

            this.PageIsvDataProvider.FilterCondition = FilterCondition.Current;
            this.PageIsvDataProvider.SortColumn = "StartDate";
            this.PageIsvDataProvider.SortDirection = Microsoft.Cui.IsvData.SortDirection.Descending;

            this.WrapDataGrid.AddSortIndicator(1, Microsoft.Cui.Controls.SortDirection.Descending);

            this.notificationArea = this.Resources["NotificationGrid"] as Grid;

            if (this.notificationArea != null && this.notificationArea.Children != null)
            {
                this.notificationArea.SetValue(Grid.ColumnSpanProperty, this.WrapDataGrid.WrapDataGridColumns.Count + 1);
                this.notification = (this.notificationArea.Children[0] as Border).Child as TextBlock;
            }

            this.AddNotification(this.PageIsvDataProvider.FilterCondition);
            (this.FilterControl.FindName("FilterDropDown") as System.Windows.Controls.ComboBox).Width = 18.9;
        }

        /// <summary>
        /// Populates Group and Filter dropdowns.
        /// </summary>
        private void PopulateDropDowns()
        {
            ////items in the past filter combo box
            this.FilterControl.Items.Add(new ComboBoxItem().Content = "Past two months");
            this.FilterControl.Items.Add(new ComboBoxItem().Content = "Past six months");
            ////A collection representing the items in the grouping combo box
            ICollection<ComboBoxItem> groupItems = new Collection<ComboBoxItem>();
            ComboBoxItem itemNone = new ComboBoxItem();
            itemNone.Content = "None";
            itemNone.Tag = "";
            groupItems.Add(itemNone);
            ComboBoxItem itemPrescriber = new ComboBoxItem();
            itemPrescriber.Content = "Prescriber";
            itemPrescriber.Tag = "Prescriber";
            groupItems.Add(itemPrescriber);
            ComboBoxItem itemMedicationStatus = new ComboBoxItem();
            itemMedicationStatus.Content = "Medication Status";
            itemMedicationStatus.Tag = "MedicationStatus";
            groupItems.Add(itemMedicationStatus);
            ComboBoxItem itemMedicationType = new ComboBoxItem();
            itemMedicationType.Content = "Medication Type";
            itemMedicationType.Tag = "MedicationType";
            groupItems.Add(itemMedicationType);
            ComboBoxItem itemReason = new ComboBoxItem();
            itemReason.Content = "Reason";
            itemReason.Tag = "Reason";
            groupItems.Add(itemReason);
            this.Grouping.ItemsSource = groupItems;
            this.Grouping.SelectedIndex = 0;
        }

        /// <summary>
        /// Handles the OnGroupingRender event for the WrapDataGrid.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e1">The <see cref="Microsoft.Cui.Controls.GroupingRenderEventArgs"/> instance containing the event data.</param>
        private void WrapDataGrid_OnGroupingRender(object sender, System.EventArgs e1)
        {
            GroupingRenderEventArgs e = e1 as GroupingRenderEventArgs;
            if (sender != null && e.GroupingHeader != null)
            {
                Grid groupHeader = e.GroupingHeader as Grid;

                if (groupHeader != null)
                {
                    //// ALTERED to remove "findname" from WPF version
                    Button expandCollapse = groupHeader.Children[0] as Button;
                    if (expandCollapse != null)
                    {
                        expandCollapse.IsTabStop = true;
                        expandCollapse.Tag = e.GroupingKey;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the click event of the expand collapse button.
        /// </summary>
        /// <param name="sender">Expand collapse button.</param>
        /// <param name="e">Event args.</param>
        private void ExpandCollapse_Click(object sender, RoutedEventArgs e)
        {
            if (sender != null && e != null)
            {
                Button expandCollapse = sender as Button;
                if (expandCollapse != null)
                {
                    this.Cursor = Cursors.Wait;
                    if (expandCollapse.Content.ToString() == "+")
                    {
                        expandCollapse.Content = "-";
                        this.WrapDataGrid.Focus();
                    }
                    else
                    {
                        expandCollapse.Content = "+";
                        expandCollapse.Focus();
                    }

                    if (this.WrapDataGrid != null && expandCollapse.Tag != null)
                    {
                        this.WrapDataGrid.ToggleGroupState(expandCollapse.Tag.ToString());
                    }

                    this.Cursor = Cursors.Arrow;
                }
            }
        }

        /// <summary>
        /// Handles the remove notification button click event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>        
        private void RemoveNotification_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            this.RemoveNotification();
            this.PageIsvDataProvider.FilterCondition = FilterCondition.Past;
            this.PageDataManager.Flush();
            this.WrapDataGrid.Reload();

            this.FilterControl.SelectedIndex = -1;
            this.FilterControl.SetFilter(this.PageIsvDataProvider.FilterCondition.ToString());
            this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// Removes the notification from wrap data grid.
        /// </summary>
        private void RemoveNotification()
        {
            if (this.notificationArea != null)
            {
                if (this.WrapDataGrid.ContainsChild(this.notificationArea))
                {
                    this.WrapDataGrid.RemoveChild(this.notificationArea);
                }
            }
        }

        /// <summary>
        /// Add the notification to the wrap data grid.
        /// </summary>
        /// <param name="filter">Filter condition.</param>
        private void AddNotification(FilterCondition filter)
        {
            string notificationText = WrapDataGridPage.GetNotificationMessage(filter);

            if (this.notificationArea != null && this.notification != null)
            {
                this.RemoveNotification();

                if (!string.IsNullOrEmpty(notificationText))
                {
                    this.notification.Text = notificationText;
                    this.WrapDataGrid.AddChild(this.notificationArea);
                }
            }
        }

        /// <summary>
        /// Handles the size changed event of group by panel.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void StackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            if (panel != null && e != null)
            {
                double calculatedWidth = panel.ActualWidth * 0.38;
                if (calculatedWidth > 75)
                {
                    this.Grouping.Width = panel.ActualWidth - 80;
                    this.ComboBoxDisplayText.Width = 75;
                }
                else
                {
                    this.Grouping.Width = panel.ActualWidth * 0.6;
                    this.ComboBoxDisplayText.Width = panel.ActualWidth * 0.38;
                }
            }
        }

        /// <summary>
        /// Gets the selected rows in the grid.
        /// </summary>
        private void GetSelectedRows()
        {
            this.selectedRowIds.Clear();


            // ADDED to avoid the null exception when the level of detail control is set
            if (this.WrapDataGrid == null)
            {
                return;
            }

            foreach (DataBoundRow row in this.WrapDataGrid.SelectedItems)
            {
                Dictionary<string, string> dataContext = row.DataContext as Dictionary<string, string>;
                int rowId;

                if (dataContext != null && int.TryParse(dataContext["MedicationId"], out rowId))
                {
                    this.selectedRowIds.Add(rowId);
                }
            }
        }

        /// <summary>
        /// Sets the selected rows in the grid.
        /// </summary>
        private void SetSelectedRows()
        {
            int lastSelectedRowIndex = -1;

            if (this.selectedRowIds.Count > 0)
            {
                this.WrapDataGrid.SelectedItems.Clear();
                foreach (DataBoundRow row in this.WrapDataGrid.MainView.Rows)
                {
                    Dictionary<string, string> dataContext = row.DataContext as Dictionary<string, string>;
                    int rowId = -1;

                    if (dataContext != null && int.TryParse(dataContext["MedicationId"], out rowId))
                    {
                        if (this.selectedRowIds.Contains(rowId))
                        {
                            this.WrapDataGrid.SelectRow(row.Index);
                        }

                        if (rowId == this.selectedRowIds[this.selectedRowIds.Count - 1])
                        {
                            lastSelectedRowIndex = row.Index;
                        }
                    }
                }
            }

            if (lastSelectedRowIndex != -1)
            {
                this.WrapDataGrid.SetVisibleRow(lastSelectedRowIndex);
            }
        }

        /// <summary>
        /// Handles the OnKeyPress event for the WrapDataGrid.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void WrapDataGrid_OnKeyPress(object sender, EventArgs e)
        {
            if (e != null && e != EventArgs.Empty)
            {
                KeyEventArgs args = e as KeyEventArgs;
                Key pressedKey = args.Key;

                if (args.OriginalSource != null && args.OriginalSource.GetType() == typeof(Button))
                {
                    Button originalSource = args.OriginalSource as Button;
                    if (originalSource.Name == "ExpandCollapse")
                    {
                        return;
                    }
                }

                switch (pressedKey)
                {
                    case Key.Enter:
                        Collection<DataBoundRow> selectedRows = (sender as Microsoft.Cui.Controls.WrapDataGrid).SelectedRows;
                        StringBuilder drugNames = new StringBuilder(selectedRows.Count);
                        foreach (DataBoundRow currentRow in selectedRows)
                        {
                            drugNames.Append((currentRow.DataContext as Dictionary<string, string>)["DrugName"] + ", ");
                        }

                        if (drugNames.Length > 0)
                        {
                            drugNames.Remove(drugNames.Length - 2, 2);
                            drugNames.Append(".");

                            if (drugNames.ToString().Contains(", "))
                            {
                                drugNames.Replace(", ", " & ", drugNames.ToString().LastIndexOf(", ", StringComparison.OrdinalIgnoreCase), 2);
                            }

                            System.Windows.MessageBox.Show(string.Format(System.Globalization.CultureInfo.CurrentCulture, "You have selected: {0}", drugNames.ToString()));
                        }



                        break;
                    case Key.Up:
                    case Key.Down:
                    case Key.End:
                    case Key.Home:
                    case Key.PageUp:
                    case Key.PageDown:
                    case Key.Space:
                        if (((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                            && this.previouslySelectedControl != null)
                        {
                            //// Dont remember previously selected toolbar button if any ctrl+keystroke is 
                            //// pressed when focus is on the grid
                            this.previouslySelectedControl = null;
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Handles the key down event for the control.
        /// </summary>
        /// <param name="sender">Sender of the key-down event.</param>
        /// <param name="e">EventArgs for the key down event.</param>
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            //// Dont handle key events when the Grid has focus 
            WrapDataGrid sourceOfEvent = e.Source as WrapDataGrid;
            if (sourceOfEvent != null)
            {
                return;
            }
            else
            {
                System.Windows.Controls.ComboBox groupCombo = e.OriginalSource as System.Windows.Controls.ComboBox;
                if (groupCombo != null)
                {                   
                    return;
                }
            }

            //// Pass all key events raised on the control (except the ones below) to the grid and 
            //// set focus on the grid after that.
            if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.PageUp || e.Key == Key.PageDown || e.Key == Key.Home || e.Key == Key.End || e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                this.WrapDataGrid.HandleParentKeyEvent(sender, e);

                if (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
                {
                    this.previouslySelectedControl = (this.previouslySelectedControl == null) ? (Control)e.OriginalSource : this.previouslySelectedControl;
                }

                this.WrapDataGrid.Focus();
            }            
        }

        /// <summary>
        /// Handles the key up event for the control.
        /// </summary>
        /// <param name="sender">Sender of the key-down event.</param>
        /// <param name="e">EventArgs for the key down event.</param>
        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.previouslySelectedControl != null && (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl))
            {
                this.previouslySelectedControl.Focus();
                this.previouslySelectedControl = null;
            }
        }

        /// <summary>
        /// Handles the key down event for the group combo box.
        /// </summary>
        /// <param name="sender">Sender of the key-down event.</param>
        /// <param name="e">EventArgs for the key down event.</param>
        private void Grouping_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.PageUp)
            {
                this.Grouping.SelectedIndex = 0;
            }
            else if (e.Key == Key.PageDown)
            {
                this.Grouping.SelectedIndex = this.Grouping.Items.Count - 1;
            }
        }
    }
}
