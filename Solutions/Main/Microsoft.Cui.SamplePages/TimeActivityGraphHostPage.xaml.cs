//-----------------------------------------------------------------------
// <copyright file="TimeActivityGraphHostPage.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010..
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
// <date>21-Sep-2009</date>
// <summary>Sample page to host TimeActivityGraphHost control.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Xml;
    using System.Xml.Serialization;
    using Microsoft.Cui.Controls;

    /// <summary>
    /// Sample page to host TimeActivityGraphHostPage control
    /// </summary>
    public partial class TimeActivityGraphHostPage : UserControl
    {
        #region Attached Properties
        /// <summary>
        /// Identifies the ParentScrollViewer attached property.
        /// </summary>
        public static readonly DependencyProperty ParentScrollViewerProperty =
            DependencyProperty.RegisterAttached("ParentScrollViewer", typeof(ScrollViewer), typeof(TimeActivityGraphHostPage), null);
        #endregion

        #region Private members
        /// <summary>
        /// Constant to denote the minimum level of detail to show activities.
        /// </summary>
        private const int MinimumLevelOfDetailForActivities = 4;

        /// <summary>
        /// Member variable to hold time event graph data from XML file.
        /// </summary>
        private Collection<TimelineGraphData> xmlGraphData = new Collection<TimelineGraphData>();
                        
        /// <summary>
        /// Member variable to hold time frequency selected index.
        /// </summary>
        private int timeFrequencySelectedIndex;        

        /// <summary>
        /// Member variable to hold the last focused element before opening the dialog.
        /// </summary>
        private Control lastFocusedElement;        
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeActivityGraphHostPage"/> class.
        /// </summary>
        public TimeActivityGraphHostPage()
        {
            InitializeComponent();
            this.DataFilePath = "Graphing/TimelineData.xml";
            this.Loaded += new System.Windows.RoutedEventHandler(this.TimeActivityGraphHostPage_Loaded);
            this.KeyDown += new System.Windows.Input.KeyEventHandler(this.TimeActivityGraphHostPage_KeyDown);
        }
#endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the patient Id.
        /// </summary>
        /// <value>The patient Id.</value>
        public string PatientId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the data file path.
        /// </summary>
        /// <value>The data file path.</value>
        public string DataFilePath
        {
            get;
            set;
        }
        #endregion

        #region Attached Properties Set/Get
        /// <summary>
        /// Gets the parent scroll viewer.
        /// </summary>
        /// <param name="obj">The object for which the parent scrollbar needs to be retreived.</param>
        /// <returns>Returns the scrollviewer control associated with the scrollbar.</returns>
        public static ScrollViewer GetParentScrollViewer(DependencyObject obj)
        {
            return (ScrollViewer)obj.GetValue(ParentScrollViewerProperty);
        }

        /// <summary>
        /// Sets the parent scroll viewer.
        /// </summary>
        /// <param name="obj">The object for which the parent scroll viewer control needs to be set.</param>
        /// <param name="value">The parent scrollviewer control associated with the object.</param>
        public static void SetParentScrollViewer(DependencyObject obj, ScrollViewer value)
        {
            obj.SetValue(ParentScrollViewerProperty, value);
        }

        /// <summary>
        /// Initializes the graphs.
        /// </summary>
        /// <param name="resetTimeFrequency">If set to <c>true</c> [reset time frequency].</param>
        public void InitializeGraphs(bool resetTimeFrequency)
        {
            if (resetTimeFrequency)
            {
                this.xmlGraphData.Clear();
                this.ReadGraphData();
                this.TimeActivityGraphHost.TimeFrequencySelectedIndex = this.timeFrequencySelectedIndex;
                this.TimeActivityGraphHost.Graphs.Clear();
                this.AddGraphsToHost();                                
            }
            else
            {
                this.ReadGraphData();
                this.UpdateGraphData();
            }

            this.TimeActivityGraphHost.RefreshLayout();
        }
        #endregion

        #region Private Static Methods
        /// <summary>
        /// Updates the out of view values.
        /// </summary>
        /// <param name="scrollBar">The scroll bar.</param>
        private static void UpdateOutOfViewValues(ScrollBar scrollBar)
        {
            ItemsControl itemsControl = ((scrollBar.Parent as Grid).Children[1] as ScrollContentPresenter).Content as ItemsControl;
            GraphSection graphSection = scrollBar.DataContext as GraphSection;
            ScrollViewer scrollViewer = GetParentScrollViewer(scrollBar);

            UpdateGraphSectionValues(scrollViewer, itemsControl, graphSection);            
        }

        /// <summary>
        /// Updates the graph section values.
        /// </summary>
        /// <param name="scrollViewer">The scroll viewer.</param>
        /// <param name="itemsControl">The items control.</param>
        /// <param name="graphSection">The graph section.</param>
        private static void UpdateGraphSectionValues(ScrollViewer scrollViewer, ItemsControl itemsControl, GraphSection graphSection)
        {
            ScrollBar verticalScrollBar = GetVerticalScrollbar(scrollViewer);            
            double scrollOffset = verticalScrollBar.Value;

            graphSection.ItemsOutViewTop = graphSection.ItemsOutViewBottom = 0;
            LevelOfDetail lodControl = scrollViewer.FindName("LevelOfDetail") as LevelOfDetail;

            string title = string.Empty;
            if (lodControl != null && lodControl.CurrentLevel != 1)
            {
                foreach (object o in itemsControl.Items)
                {
                    TimeGraphBase graph = o as TimeGraphBase;
                    if (!string.IsNullOrEmpty(graph.Title))
                    {
                        Rect itemBounds = LayoutInformation.GetLayoutSlot(graph);

                        if (!double.IsNaN(itemBounds.Top) && !double.IsNaN(itemBounds.Bottom))
                        {
                            if (itemBounds.Top < scrollOffset && itemBounds.Bottom < scrollOffset)
                            {
                                // Completely out of view on top
                                graphSection.ItemsOutViewTop++;
                            }
                            else if (itemBounds.Top > scrollOffset + scrollViewer.ViewportHeight && itemBounds.Bottom > scrollOffset + scrollViewer.ViewportHeight)
                            {
                                // Completely out of view at bottom
                                graphSection.ItemsOutViewBottom++;
                            }
                            else if ((itemBounds.Top + (itemBounds.Height / 2)) < scrollOffset)
                            {
                                // Partially out of view on top
                                graphSection.ItemsOutViewTop++;
                            }
                            else if ((itemBounds.Bottom - (itemBounds.Height / 2)) > scrollOffset + scrollViewer.ViewportHeight)
                            {
                                // Partially out of view at bottom
                                graphSection.ItemsOutViewBottom++;
                            }
                        }
                    }
                }

                string formatText = "({0} rows are not in view - scroll {1})";
                if (graphSection.ItemsOutViewTop > 0 || graphSection.ItemsOutViewBottom > 0)
                {
                    string scrollDirection = string.Empty;
                    if (graphSection.ItemsOutViewTop > 0 && graphSection.ItemsOutViewBottom > 0)
                    {
                        scrollDirection = "UP/DOWN";
                    }
                    else if (graphSection.ItemsOutViewTop > 0)
                    {
                        scrollDirection = "UP";
                    }
                    else
                    {
                        scrollDirection = "DOWN";
                    }

                    if (graphSection.ItemsOutViewTop + graphSection.ItemsOutViewBottom == 1)
                    {
                        formatText = "({0} row is not in view - scroll {1})";
                    }

                    title = string.Format(CultureInfo.CurrentCulture, formatText, graphSection.ItemsOutViewTop + graphSection.ItemsOutViewBottom, scrollDirection);
                }                
            }

            TextBlock outOfViewTitle = scrollViewer.FindName("OutOfViewTitle") as TextBlock;
            if (outOfViewTitle != null)
            {
                outOfViewTitle.Text = title;
            }
        }

        /// <summary>
        /// Sets the visibility.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        private static void SetVisibility(object element, bool visible)
        {
            FrameworkElement frameworkElement = element as FrameworkElement;
            if (frameworkElement != null)
            {
                if (visible)
                {
                    frameworkElement.Visibility = Visibility.Visible;
                }
                else
                {
                    frameworkElement.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Prints the time event data.
        /// </summary>
        /// <param name="graph">The graph.</param>
        private static void PrintData(TimeGraphBase graph)
        {
            TimeActivityGraph timeActivityGraph = graph as TimeActivityGraph;
            if (timeActivityGraph != null)
            {
                IEnumerable enumerable = timeActivityGraph.DataContext as IEnumerable;
                if (enumerable != null)
                {
                    System.Diagnostics.Debug.WriteLine("Graph :" + timeActivityGraph.Name);
                    foreach (TimeActivityPoint point in enumerable)
                    {
                        System.Diagnostics.Debug.WriteLine("Start :" + point.StartDate + " End: " + point.EndDate);
                    }

                    System.Diagnostics.Debug.WriteLine("-------------------------------");
                }
            }
            else
            {
                IEnumerable enumerable = graph.DataContext as IEnumerable;
                if (enumerable != null)
                {
                    System.Diagnostics.Debug.WriteLine("Graph :" + graph.Name);
                    foreach (TimePoint point in enumerable)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("Date :" + point.DateTime);
                        sb.Append(" Y1 :" + point.Y1);
                        sb.Append(" Y2 :" + (double.IsNaN(point.Y2) ? string.Empty : point.Y2.ToString(CultureInfo.CurrentCulture)));
                        System.Diagnostics.Debug.WriteLine(sb.ToString());
                    }

                    System.Diagnostics.Debug.WriteLine("-------------------------------");
                }
            }
        }
                
        /// <summary>
        /// Gets the resource from app.
        /// </summary>
        /// <typeparam name="T">Type of the resource.</typeparam>
        /// <param name="keyName">Name of the key.</param>
        /// <returns>Resource from the App.</returns>
        private static T GetResourceFromApp<T>(string keyName)
        {            
            object obj = null;
            if (!string.IsNullOrEmpty(keyName))
            {
                keyName = keyName.Replace("{StaticResource ", string.Empty).Replace("}", string.Empty);
                if (App.Current.Resources.Contains(keyName))
                {
                    obj = App.Current.Resources[keyName];
                }
            }

            return (T)obj;
        }

        /// <summary>
        /// Gets the brush from color code.
        /// </summary>
        /// <param name="colorCode">The color code.</param>
        /// <returns>SolidColorBrush from the color code.</returns>
        private static SolidColorBrush GetBrushFromColorCode(string colorCode)
        {
            if (!string.IsNullOrEmpty(colorCode))
            {
                colorCode = colorCode.Replace("#", string.Empty);
                byte a = System.Convert.ToByte("ff", 16);
                byte pos = 0;
                if (colorCode.Length == 8)
                {
                    a = System.Convert.ToByte(colorCode.Substring(pos, 2), 16);
                    pos = 2;
                }

                byte r = System.Convert.ToByte(colorCode.Substring(pos, 2), 16);

                pos += 2;
                byte g = System.Convert.ToByte(colorCode.Substring(pos, 2), 16);

                pos += 2;
                byte b = System.Convert.ToByte(colorCode.Substring(pos, 2), 16);

                Color col = Color.FromArgb(a, r, g, b);
                return new SolidColorBrush(col);
            }

            return new SolidColorBrush(Colors.Transparent);
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="dateString">The date string.</param>
        /// <returns>Gets the datetime from the string.</returns>
        private static DateTime? GetDate(string dateString)
        {
            DateTime? dateTime = null;

            if (!string.IsNullOrEmpty(dateString))
            {
                DateTime parsedValue;
                if (DateTime.TryParse(dateString, CultureInfo.CurrentCulture, DateTimeStyles.None, out parsedValue))
                {
                    dateTime = parsedValue;
                }
            }

            return dateTime;
        }

        /// <summary>
        /// Gets the date.
        /// </summary>
        /// <param name="dateString">The date string.</param>
        /// <param name="adjustment">The adjustment.</param>
        /// <returns>Gets the adjusted date time from the string.</returns>
        private static DateTime? GetDate(string dateString, TimeSpan adjustment)
        {
            DateTime? dateTime = GetDate(dateString);
            if (dateTime.HasValue && adjustment != TimeSpan.Zero)
            {
                dateTime = dateTime.Value.Add(adjustment);
            }

            return dateTime;
        }

        /// <summary>
        /// Gets the adjusted base date.
        /// </summary>
        /// <param name="baseDate">The base date.</param>
        /// <returns>TimeSpan for the date adjustment.</returns>
        private static TimeSpan GetAdjustedBaseDate(DateTime? baseDate)
        {
            TimeSpan scenarioBaseDate = TimeSpan.Zero;

            // assumption: the BaseDate must be in the past, so all dates are only adjusted forward
            if (baseDate.HasValue && baseDate < DateTime.Now)
            {
                scenarioBaseDate = DateTime.Now.Subtract(baseDate.Value);
                scenarioBaseDate = new TimeSpan(scenarioBaseDate.Ticks - (scenarioBaseDate.Ticks % TimeSpan.TicksPerSecond)); // truncate milliseconds
            }

            return scenarioBaseDate;
        }

        /// <summary>
        /// Brings the into view.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="scrollViewer">The scroll viewer.</param>
        private static void BringIntoView(TimeGraphBase graph, ScrollViewer scrollViewer)
        {
            Rect itemBounds = LayoutInformation.GetLayoutSlot(graph);

            if (!double.IsNaN(itemBounds.Top) && !double.IsNaN(itemBounds.Bottom))
            {
                double scrollOffset = (double)scrollViewer.GetValue(ScrollViewer.VerticalOffsetProperty);
                double newScrollOffset = double.NaN;

                if (itemBounds.Top < scrollOffset)
                {
                    // Top not visible
                    newScrollOffset = itemBounds.Top;
                }
                else if (itemBounds.Bottom > scrollOffset + scrollViewer.ViewportHeight)
                {
                    // Bottom not visible
                    newScrollOffset = itemBounds.Bottom - scrollViewer.ViewportHeight;
                }

                if (!double.IsNaN(newScrollOffset))
                {
                    scrollViewer.ScrollToVerticalOffset(newScrollOffset);
                }
            }
        }

        /// <summary>
        /// Gets the vertical scrollbar.
        /// </summary>
        /// <param name="scrollViewer">The scroll viewer.</param>
        /// <returns>Vertical scrollbar from the scrollviewer template.</returns>
        private static ScrollBar GetVerticalScrollbar(ScrollViewer scrollViewer)
        {
            Panel panel = VisualTreeHelper.GetChild(scrollViewer, 0) as Panel;
            ScrollBar verticalSb = panel.Children[3] as ScrollBar;
            return verticalSb;
        }

        /// <summary>
        /// Gets the medication details.
        /// </summary>
        /// <param name="item">The medication item.</param>
        /// <param name="administrationEvent">The administration event.</param>
        /// <param name="dateAdjustment">TimeSpan to be added for the base dates.</param>
        /// <returns>Returns TimelineMedicationDetails with medication details.</returns>
        private static TimelineMedicationDetails GetMedicationDetails(TimelineSampleDataScenarioSectionRowItem item, TimelineSampleDataScenarioSectionRowItemEvent administrationEvent, TimeSpan dateAdjustment)
        {
            TimelineMedicationDetails additionalInfo = new TimelineMedicationDetails();
            if (item != null)
            {
                additionalInfo.BrandName = item.Brand;
                additionalInfo.Dose = item.Dose;
                additionalInfo.DoseDuration = item.DoseDuration;
                additionalInfo.DoseLabel = item.DoseLabel;
                additionalInfo.Form = item.Form;
                additionalInfo.Frequency = item.Frequency;
                additionalInfo.MedicationName = item.Name;
                additionalInfo.Route = item.Route;
                additionalInfo.FluidStrength = item.FluidStrength;
                additionalInfo.SolidStrength = item.SolidStrength;
            }

            if (administrationEvent != null)
            {
                additionalInfo.PlannedStartDate = GetDate(administrationEvent.PlannedStartDate, dateAdjustment);
                additionalInfo.PlannedEndDate = GetDate(administrationEvent.PlannedEndDate, dateAdjustment);
                additionalInfo.Status = administrationEvent.Status;
            }

            return additionalInfo;
        }

        /// <summary>
        /// Registers the scroll position for the srollviewer at current lod.
        /// </summary>
        /// <param name="lodControl">Level of detail control.</param>
        /// <param name="scrollViewer">Scroll viewer control.</param>
        /// <param name="section">Graph section.</param>
        private static void RegisterScrollViewerPosition(LevelOfDetail lodControl, ScrollViewer scrollViewer, GraphSection section)
        {
            if (scrollViewer.VerticalOffset == 0)
            {
                lodControl.Tag = int.MinValue;
            }
            else if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
            {
                lodControl.Tag = int.MaxValue;
            }
            else
            {
                int sectionGraphsCount = section.Graphs.Count;
                if (string.IsNullOrEmpty(section.Graphs[sectionGraphsCount - 1].Title))
                {
                    sectionGraphsCount--;
                }

                int midGraphIndex = (int)((sectionGraphsCount - section.ItemsOutViewBottom - section.ItemsOutViewTop) / 2);
                lodControl.Tag = midGraphIndex + section.ItemsOutViewTop;
            }

        }

        /// <summary>
        /// Updates the graph properties.
        /// </summary>
        /// <param name="level">The level of detail.</param>
        /// <param name="section">The section containing graphs.</param>
        private static void UpdateGraphProperties(int level, GraphSection section)
        {
            foreach (TimeGraphBase graph in section.Graphs)
            {
                graph.BeginEdit();
                graph.Reset();
                TimeActivityGraph timeActivityGraph = graph as TimeActivityGraph;

                if (level == 1)
                {
                    if (timeActivityGraph != null && !string.IsNullOrEmpty(timeActivityGraph.Title))
                    {
                        timeActivityGraph.Minimized = true;
                        timeActivityGraph.IsTabStop = false;
                        timeActivityGraph.Activities.Clear();
                    }
                    else
                    {
                        graph.ShowDataPointLabels = Visibility.Collapsed;
                    }
                }
                else if (level == 2)
                {
                    if (timeActivityGraph != null && !string.IsNullOrEmpty(timeActivityGraph.Title))
                    {
                        timeActivityGraph.Minimized = false;
                        timeActivityGraph.IsTabStop = true;
                        timeActivityGraph.Activities.Clear();                        
                        timeActivityGraph.ShowDataPointLabels = Visibility.Collapsed;
                    }
                    else
                    {
                        graph.ShowDataPointLabels = Visibility.Visible;
                    }
                }
                else if (level == 3)
                {
                    if (timeActivityGraph != null && !string.IsNullOrEmpty(timeActivityGraph.Title))
                    {
                        graph.ShowDataPointLabels = Visibility.Visible;
                        timeActivityGraph.IsTabStop = true;
                        timeActivityGraph.Minimized = false;
                        timeActivityGraph.StackLabels = false;
                        timeActivityGraph.Activities.Clear(); 
                        if (timeActivityGraph.LabelMode != LabelMode.Simple)
                        {
                            timeActivityGraph.LabelMode = LabelMode.Partial;
                        }
                    }
                }
                else if (level == 4)
                {
                    if (timeActivityGraph != null && !string.IsNullOrEmpty(timeActivityGraph.Title))
                    {
                        graph.ShowDataPointLabels = Visibility.Visible;
                        timeActivityGraph.IsTabStop = true;
                        timeActivityGraph.Minimized = false;
                        timeActivityGraph.StackLabels = true;
                        if (timeActivityGraph.LabelMode != LabelMode.Simple)
                        {
                            timeActivityGraph.LabelMode = LabelMode.Full;
                        }
                    }
                }

                graph.Refresh();
                graph.EndEdit();
            }
        }
#endregion

        #region Private Methods
        /// <summary>
        /// Adds the graphs to host.
        /// </summary>
        private void AddGraphsToHost()
        {
            foreach (TimelineGraphData graphData in this.xmlGraphData)
            {
                TimeGraphBase graph = this.GetGraphFromGraphData(graphData);
                DockPanel.SetDock(graph, Dock.Top);
                graph.GotFocus += new RoutedEventHandler(this.Graph_GotFocus);
                this.TimeActivityGraphHost.Graphs.Add(graph);
            }
        }

        /// <summary>
        /// Updates the graph data.
        /// </summary>
        private void UpdateGraphData()
        {
            foreach (TimeGraphBase graph in this.TimeActivityGraphHost.Graphs)
            {
                if (graph.Tag != null)
                {
                    string graphId = graph.Tag.ToString();
                    List<TimelineGraphData> graphData = (from data in this.xmlGraphData
                                                          where string.Compare(data.GraphId, graphId, StringComparison.CurrentCultureIgnoreCase) == 0
                                                          select data).ToList();

                    graph.DataContext = graphData[0].Data;

                    TimeActivityGraph timeActivityGraph = graph as TimeActivityGraph;
                    if (graphData.Count > 0 && timeActivityGraph != null)
                    {
                        timeActivityGraph.Activities.Clear();
                        if (graphData[0].ActivityTypesDisplayed.Count > 0)
                        {
                            foreach (string activityType in graphData[0].ActivityTypesDisplayed)
                            {
                                timeActivityGraph.Activities.Add(graphData[0].Activities[activityType]);
                            }
                        }
                    }
                }

                graph.Refresh();
            }            
        }
        
        /// <summary>
        /// Reads the graph data from the XML.
        /// </summary>
        private void ReadGraphData()
        {           
            XmlSerializer ser = new XmlSerializer(typeof(TimelineSampleData));
            XmlReader reader = XmlReader.Create(this.DataFilePath);

            if (ser.CanDeserialize(reader))
            {
                TimelineSampleData sampleData = (TimelineSampleData)ser.Deserialize(reader);
                foreach (TimelineSampleDataScenario scenario in sampleData.Scenario)
                {                    
                    if (string.Compare(scenario.PatientId, this.PatientId, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        DateTime? scenarioBaseDate = GetDate(scenario.BaseDate);
                        TimeSpan dateAdjustement = GetAdjustedBaseDate(scenarioBaseDate);

                        foreach (TimelineSampleDataScenarioSection section in scenario.Section)
                        {
                            this.CreateGraphData(section, dateAdjustement);
                            this.timeFrequencySelectedIndex = (int)scenario.TimeFrequencySelectedIndex;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates the graph data.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="dateAdjustment">The date adjustment.</param>
        private void CreateGraphData(TimelineSampleDataScenarioSection section, TimeSpan dateAdjustment)
        {
            foreach (TimelineSampleDataScenarioSectionRow row in section.Row)
            {
                TimelineGraphData graphData = null;
                FilteredCollection data = new FilteredCollection();
                Dictionary<string, FilteredCollection> events = new Dictionary<string, FilteredCollection>();

                if (row.IdSpecified)
                {
                    var graphs = (from graph in this.xmlGraphData
                                  where string.Compare(graph.GraphId, row.Id.ToString(CultureInfo.CurrentCulture), StringComparison.CurrentCultureIgnoreCase) == 0
                                  select graph).ToList<TimelineGraphData>();
                    if (graphs.Count > 0)
                    {
                        graphData = graphs[0];
                    }                                        
                }

                if (graphData == null)
                {
                    graphData = new TimelineGraphData();
                    graphData.GraphId = row.Id.ToString(CultureInfo.CurrentCulture);
                }

                graphData.Title = row.Name;
                graphData.SectionName = section.Name;
                graphData.Description = row.Description;
                graphData.Data = data;
                graphData.Background = GetBrushFromColorCode(row.Background);

                if (row.MaxLabelStackLevelsSpecified)
                {
                    graphData.MaxLabelStackLevels = (int)row.MaxLabelStackLevels;
                }
                else
                {
                    graphData.MaxLabelStackLevels = 1;
                }

                if (!string.IsNullOrEmpty(row.ShowLabelOvercrowdingNotifications))
                {
                    graphData.ShowLabelOvercrowdingNotifications = bool.Parse(row.ShowLabelOvercrowdingNotifications);
                }
                else
                {
                    graphData.ShowLabelOvercrowdingNotifications = false;
                }
                
                if (row.Item != null)
                {
                    foreach (TimelineSampleDataScenarioSectionRowItem item in row.Item)
                    {
                        graphData.Style = GetResourceFromApp<Style>(item.Style);                        
                        graphData.LabelTemplate = GetResourceFromApp<DataTemplate>(item.LabelTemplate);
                        graphData.PointTemplate = GetResourceFromApp<DataTemplate>(item.PointTemplate);
                        graphData.DataMarkerTemplate = GetResourceFromApp<DataTemplate>(item.DataMarkerTemplate);
                        graphData.InterpolationLineColor = GetBrushFromColorCode(item.InterpolationLineColor);
                        graphData.NormalRangeBrush = GetBrushFromColorCode(item.NormalRangeBrush);
                        graphData.UnitsDescription = item.UnitsDescription;
                        graphData.Units = item.Units;                        

                        graphData.NormalRangeDescription = item.NormalRangeDescription;

                        if (item.YAxisPaddingSpecified)
                        {
                            graphData.YAxisPadding = new Thickness((double)item.YAxisPadding);
                        }
                        else
                        {
                            graphData.YAxisPadding = new Thickness(5);
                        }

                        graphData.ShowNormalRange = string.IsNullOrEmpty(item.ShowNormalRange) ? false : bool.Parse(item.ShowNormalRange);
                                                
                        if (item.NormalRangeMaximumValueSpecified)
                        {
                            graphData.NormalRangeMaxValue = (double)item.NormalRangeMaximumValue;
                        }

                        if (item.NormalRangeMinimumValueSpecified)
                        {
                            graphData.NormalRangeMinValue = (double)item.NormalRangeMinimumValue;
                        }

                        if (item.YAxisMaxValueSpecified)
                        {
                            graphData.YAxisMaxValue = (double)item.YAxisMaxValue;
                        }

                        if (item.YAxisMinValueSpecified)
                        {
                            graphData.YAxisMinValue = (double)item.YAxisMinValue;
                        }

                        if (item.YAxisMajorIntervalSpecified)
                        {
                            graphData.YAxisMajorInterval = (double)item.YAxisMajorInterval;
                        }

                        if (item.YAxisIntervalMinimumHeightSpecified)
                        {
                            graphData.YAxisIntervalMinHeight = (double)item.YAxisIntervalMinimumHeight;
                        }

                        if (!string.IsNullOrEmpty(item.StartDate))
                        {
                            TimeActivityPoint timeActivityPoint = new TimeActivityPoint();
                            timeActivityPoint.StartDate = GetDate(item.StartDate, dateAdjustment).Value;
                            timeActivityPoint.EndDate = GetDate(item.EndDate, dateAdjustment);
                            timeActivityPoint.AdditionalInformation = GetMedicationDetails(item, null, dateAdjustment);
                            MedicationLabel label = new MedicationLabel();
                            if (!string.IsNullOrEmpty(item.Type) && string.Compare(item.Type, "SecondaryCareMedication", StringComparison.CurrentCultureIgnoreCase) == 0)
                            {
                                label.Mode = LabelMode.Full;
                            }
                            else
                            {
                                label.Mode = LabelMode.Simple;
                            }

                            if (label != null)
                            {
                                label.MedicationName = item.Name;
                                label.Dose = item.Dose;
                                label.Frequency = item.Frequency;
                                label.Route = item.Route;
                                label.FluidStrength = item.FluidStrength;
                                label.SolidStrength = item.SolidStrength;
                                label.Dose = item.Dose;
                                label.DoseDuration = item.DoseDuration;
                                label.DoseLabel = item.DoseLabel;
                                label.BrandName = item.Brand;
                                label.Form = item.Form;
                            }

                            timeActivityPoint.Label = label;
                            graphData.LabelMode = label.Mode;
                            string templateShortName;

                            if (timeActivityPoint.EndDate.HasValue)
                            {
                                if (timeActivityPoint.StartDate != timeActivityPoint.EndDate)
                                {
                                    templateShortName = "InterpolationLine";
                                }
                                else
                                {
                                    templateShortName = "InterpolationLineOneTime";
                                }
                            }
                            else
                            {
                                templateShortName = "InterpolationLineOpenDuration";
                            }

                            timeActivityPoint.DataMarkerTemplate = this.LayoutRoot.Resources[section.Name.ToUpper(CultureInfo.CurrentCulture) + "_" + templateShortName] as DataTemplate;
                            data.Add(timeActivityPoint);
                        }

                        if (item.Events != null)
                        {
                            foreach (TimelineSampleDataScenarioSectionRowItemEvent medsEvent in item.Events)
                            {
                                if (medsEvent.Type == "GraphData")
                                {
                                    TimePoint timePoint = new TimePoint();
                                    timePoint.DateTime = GetDate(medsEvent.ActualStartDate, dateAdjustment).Value;
                                    timePoint.Y1 = (double)medsEvent.Y1;
                                    graphData.Height = (double)item.Height;
                                    if (medsEvent.Y2Specified)
                                    {
                                        timePoint.Y2 = (double)medsEvent.Y2;
                                        graphData.GraphType = GraphType.TimeIBar;
                                    }
                                    else
                                    {
                                        graphData.GraphType = GraphType.TimeLine;
                                    }

                                    data.Add(timePoint);
                                }
                                else
                                {
                                    TimeActivityPoint administrationPoint = new TimeActivityPoint();

                                    if (string.IsNullOrEmpty(medsEvent.ActualStartDate))
                                    {
                                        administrationPoint.StartDate = GetDate(medsEvent.PlannedStartDate, dateAdjustment).Value;
                                    }
                                    else
                                    {
                                        administrationPoint.StartDate = GetDate(medsEvent.ActualStartDate, dateAdjustment).Value;
                                    }

                                    administrationPoint.EndDate = GetDate(medsEvent.ActualEndDate, dateAdjustment);
                                    administrationPoint.AdditionalInformation = GetMedicationDetails(item, medsEvent, dateAdjustment);

                                    string templateShortName = "MedsAdminEvent";
                                    if (administrationPoint.EndDate.HasValue && administrationPoint.EndDate.Value > administrationPoint.StartDate)
                                    {
                                        templateShortName = "ContinuousMedsAdminEvent";
                                    }
                                    else if (medsEvent.Type == "prescription issue events")
                                    {
                                        templateShortName = "PrescriptionEvent";
                                    }
                                    
                                    administrationPoint.DataMarkerTemplate = this.LayoutRoot.Resources[templateShortName + "_" + medsEvent.Status.ToLower(CultureInfo.CurrentCulture)] as DataTemplate;

                                    string eventType = medsEvent.Type;
                                    if (!events.ContainsKey(eventType))
                                    {
                                        events.Add(eventType, new FilteredCollection());
                                    }

                                    //// Do not display medication administration events of Significant Duration in the Timeline sample.
                                    //// The display of medications of Significant Duration was not explored in the
                                    //// 'Design Guidance – Timeline View' document. 
                                    //// Any visual representation of these events in Timeline will need to be re-evaluated in line
                                    //// with the more up-to-date guidance published in the 'Design Guidance – Drug Administration' document.
                                    if (templateShortName != "ContinuousMedsAdminEvent" && String.Compare(medsEvent.Status, "Started", StringComparison.CurrentCultureIgnoreCase) != 0)
                                    {
                                        events[eventType].Add(administrationPoint);
                                    }
                                }
                            }
                        }
                    }

                    foreach (KeyValuePair<string, FilteredCollection> medsEvents in events)
                    {
                        if (!graphData.Activities.ContainsKey(medsEvents.Key))
                        {
                            graphData.Activities.Add(medsEvents.Key, new FilteredCollection());
                        }

                        graphData.Activities[medsEvents.Key] = medsEvents.Value;
                    }
                }

                this.xmlGraphData.Add(graphData);
            }
        }

        /// <summary>
        /// Gets the graph from graph data.
        /// </summary>
        /// <param name="graphData">The graph data.</param>
        /// <returns>TimeGraphBase object with the data set from the object.</returns>
        private TimeGraphBase GetGraphFromGraphData(TimelineGraphData graphData)
        {
            TimeGraphBase graph = null;
            switch (graphData.GraphType)
            {
                case GraphType.TimeActivity:
                    graph = new TimeActivityGraph();
                    graph.Style = App.Current.Resources["MedsTimelineStyle"] as Style;
                    TimeActivityGraph timeActivityGraph = graph as TimeActivityGraph;                    
                    timeActivityGraph.MaxLabelStackLevels = graphData.MaxLabelStackLevels;
                    timeActivityGraph.ShowLabelOvercrowdingNotifications = graphData.ShowLabelOvercrowdingNotifications;
                    timeActivityGraph.LabelMode = graphData.LabelMode;
                    if (string.Compare(graphData.SectionName, "Problems", StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        timeActivityGraph.LabelClick += new RoutedEventHandler(this.ProblemsGraph_LabelClick);
                        timeActivityGraph.InterpolationLineClick += new RoutedEventHandler(this.ProblemsLine_MouseLeftButtonDown);
                    }
                    else
                    {
                        timeActivityGraph.ShowActivities = true;
                        if (timeActivityGraph.LabelMode == LabelMode.Simple)
                        {
                            timeActivityGraph.LabelClick += new RoutedEventHandler(this.MedicationsGraph_LabelClick);
                            timeActivityGraph.InterpolationLineClick += new RoutedEventHandler(this.MedicationsLine_MouseLeftButtonDown);
                        }
                        else
                        {
                            timeActivityGraph.LabelClick += new RoutedEventHandler(this.SecondaryCareMedicationsGraph_LabelClick);
                            timeActivityGraph.InterpolationLineClick += new RoutedEventHandler(this.SecondaryCareMedicationsLine_MouseLeftButtonDown);
                        }
                    }

                    timeActivityGraph.ActivityClick += new RoutedEventHandler(this.Event_MouseLeftButtonDown);

                    if (string.IsNullOrEmpty(graphData.Title))
                    {
                        timeActivityGraph.VerticalAlignment = VerticalAlignment.Stretch;                        
                        timeActivityGraph.IsTabStop = false;
                    }

                    break;
                case GraphType.TimeIBar:
                    graph = new TimeIBarGraph();
                    (graph as TimeIBarGraph).InterpolationLineColor = graphData.InterpolationLineColor;
                    break;
                case GraphType.TimeLine:
                    graph = new TimeLineGraph();
                    (graph as TimeLineGraph).InterpolationLineColor = graphData.InterpolationLineColor;
                    break;
            }

            if (!string.IsNullOrEmpty(graphData.GraphId))
            {
                graph.Tag = graphData.GraphId;
            }

            graph.DataContext = graphData.Data;
            graph.Title = graphData.Title;
            graph.Description = graphData.Description;
            graph.Background = graphData.Background;
            graph.LabelTemplate = graphData.LabelTemplate;
            graph.AddYAxisSeparator = false;            
            
            TimeAndYGraphBase timeYGraph = graph as TimeAndYGraphBase;
            if (timeYGraph != null)
            {
                timeYGraph.ShowDataPointLabels = Visibility.Visible;
                timeYGraph.Style = App.Current.Resources["TimelineStyle"] as Style;
                timeYGraph.Units = graphData.Units;
                timeYGraph.UnitsDescription = graphData.UnitsDescription;
                timeYGraph.DataMarkerTemplate = graphData.DataMarkerTemplate;
                timeYGraph.YAxisIntervalMinimumHeight = graphData.YAxisIntervalMinHeight;
                timeYGraph.YAxisMajorInterval = graphData.YAxisMajorInterval;
                timeYGraph.YAxisMaxValue = graphData.YAxisMaxValue;
                timeYGraph.YAxisMinValue = graphData.YAxisMinValue;
                timeYGraph.NormalRangeBrush = graphData.NormalRangeBrush;
                timeYGraph.ShowNormalRange = graphData.ShowNormalRange;
                timeYGraph.NormalRangeMinimumValue = graphData.NormalRangeMinValue;
                timeYGraph.NormalRangeMaximumValue = graphData.NormalRangeMaxValue;
                timeYGraph.NormalRangeDescription = graphData.NormalRangeDescription;
                timeYGraph.YAxisPadding = graphData.YAxisPadding;
            }

            TimeActivityGraphHost.SetSectionName(graph, graphData.SectionName.ToUpper(CultureInfo.CurrentCulture));

            if (!double.IsNaN(graphData.Height))
            {
                graph.Height = graphData.Height;
            }

            return graph;
        }

        /// <summary>
        /// Adds the Activity to graphs in a section.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="activityTypeName">Name of the activity type.</param>
        private void AddActivity(string sectionName, string activityTypeName)
        {
            var graphs = from graph in this.TimeActivityGraphHost.Graphs
                         where string.Compare(TimeActivityGraphHost.GetSectionName(graph), sectionName, StringComparison.CurrentCultureIgnoreCase) == 0
                         select graph;

            foreach (TimeActivityGraph graph in graphs)
            {
                string graphId = string.Empty;
                if (graph.Tag != null)
                {
                    graphId = graph.Tag.ToString();
                }

                foreach (TimelineGraphData data in this.xmlGraphData)
                {
                    if (string.Compare(data.GraphId, graphId, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {                        
                        if (data.Activities.ContainsKey(activityTypeName))
                        {
                            if (!data.ActivityTypesDisplayed.Contains(activityTypeName))
                            {
                                data.ActivityTypesDisplayed.Add(activityTypeName);
                            }

                            graph.Activities.Add(data.Activities[activityTypeName]);
                        }
                        
                        graph.Refresh();
                        break;
                    }
                }
            }
        }
        
        /// <summary>
        /// Removes the activity.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="activityTypeName">Name of the activity type.</param>
        private void RemoveActivity(string sectionName, string activityTypeName)
        {
            var graphs = from graph in this.TimeActivityGraphHost.Graphs
                         where string.Compare(TimeActivityGraphHost.GetSectionName(graph), sectionName, StringComparison.CurrentCultureIgnoreCase) == 0
                         select graph;

            foreach (TimeActivityGraph graph in graphs)
            {
                string graphId = string.Empty;
                if (graph.Tag != null)
                {
                    graphId = graph.Tag.ToString();
                }

                foreach (TimelineGraphData data in this.xmlGraphData)
                {
                    if (string.Compare(data.GraphId, graphId, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        if (data.Activities.ContainsKey(activityTypeName))
                        {
                            if (data.ActivityTypesDisplayed.Contains(activityTypeName))
                            {
                                data.ActivityTypesDisplayed.Remove(activityTypeName);
                            }

                            graph.Activities.Remove(data.Activities[activityTypeName]);
                        }

                        graph.Refresh();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Restores the focus on close of dialog.
        /// </summary>
        /// <param name="focusedElement">The focused element.</param>
        private void RestoreFocus(Control focusedElement)
        {
            focusedElement.IsTabStop = false;

            if (this.lastFocusedElement != null)
            {
                FocusHelper.FocusControl(this.lastFocusedElement);
                this.lastFocusedElement = null;
            }
        }

        /// <summary>
        /// Sets the focus in dialog.
        /// </summary>
        /// <param name="dialog">The dialog.</param>
        /// <param name="elementName">Name of the element.</param>
        private void SetFocusInDialog(ModalDialog dialog, string elementName)
        {
            this.lastFocusedElement = FocusManager.GetFocusedElement() as Control;
            Control elementToFocus = (dialog.ContentPlaceHolder.Content as FrameworkElement).FindName(elementName) as Control;
            elementToFocus.IsTabStop = true;
            FocusHelper.FocusControl(elementToFocus);            
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// Handles the Loaded event of the TimeActivityGraphHostPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void TimeActivityGraphHostPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {            
            if (string.IsNullOrEmpty(this.PatientId))
            {
                this.PatientId = this.Primarycare.IsChecked.Value ? this.Primarycare.Tag.ToString() : this.Secondarycare.Tag.ToString();
            }
            else
            {
                if (string.Compare(this.PatientId, this.Primarycare.Tag.ToString(), StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    this.Primarycare.IsChecked = true;
                }
                else
                {
                    this.Secondarycare.IsChecked = true;
                }
            }

            this.Primarycare.Click += new RoutedEventHandler(this.Scenario_Click);
            this.Secondarycare.Click += new RoutedEventHandler(this.Scenario_Click);
            this.InitializeGraphs(true);
        }

        /// <summary>
        /// Handles the Click event of the Scenario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Scenario_Click(object sender, RoutedEventArgs e)
        {
            string newPatientId = this.Primarycare.IsChecked.Value ? this.Primarycare.Tag.ToString() : this.Secondarycare.Tag.ToString();
            if (string.Compare(newPatientId, this.PatientId, StringComparison.CurrentCultureIgnoreCase) != 0)
            {
                this.PatientId = newPatientId;
                this.TimeActivityGraphHost.NowDateTime = DateTime.Now;
                this.InitializeGraphs(true);                
            }            
        }
        
        /// <summary>
        /// Handles the KeyDown event of the TimeActivityGraphHostPage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void TimeActivityGraphHostPage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            bool ctrl = (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control;
            bool shift = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;

            if (ctrl && shift)
            {
                TimeGraphBase graph = FocusManager.GetFocusedElement() as TimeGraphBase;
                if (graph != null)
                {
                    ScrollViewer scrollViewer = null;
                    scrollViewer = graph.GetVisualAncestorsAndSelf().OfType<ScrollViewer>().ElementAt(0);
                    if (scrollViewer != null)
                    {
                        LevelOfDetail lod = (scrollViewer.Parent as FrameworkElement).FindName("LevelOfDetail") as LevelOfDetail;
                        if (lod != null)
                        {
                            if (e.Key == Key.Add || e.PlatformKeyCode == 187)
                            {
                                lod.CurrentLevel++;
                                e.Handled = true;
                            }
                            else if (e.Key == Key.Subtract || e.PlatformKeyCode == 189)
                            {
                                lod.CurrentLevel--;
                                e.Handled = true;
                            }                            
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Handles the OnLevelOfDetailChange event of the LevelOfDetail control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void LevelOfDetail_OnLevelOfDetailChange(object sender, EventArgs e)
        {
            this.TimeActivityGraphHost.ShowLoadingScreen();

            System.Windows.Threading.DispatcherTimer lodTimer = new System.Windows.Threading.DispatcherTimer();
            lodTimer.Tick += delegate(object s, EventArgs args)
            {
                LevelOfDetail lodControl = sender as LevelOfDetail;
                GraphSection section = lodControl.DataContext as GraphSection;
                ScrollViewer scrollViewer = lodControl.FindName("ScrollViewer") as ScrollViewer;
                RegisterScrollViewerPosition(lodControl, scrollViewer, section);

                UpdateGraphProperties(lodControl.CurrentLevel, section);
                if (lodControl.CurrentLevel < MinimumLevelOfDetailForActivities)
                {
                    Panel activityTypeList = lodControl.FindName("ActivityTypeList") as Panel;
                    if (activityTypeList != null)
                    {
                        foreach (UIElement child in activityTypeList.Children)
                        {
                            CheckBox checkBox = child as CheckBox;
                            if (checkBox != null)
                            {
                                checkBox.IsChecked = false;
                            }
                        }
                    }
                }

                FrameworkElement lodMinMessage = lodControl.FindName("LODMinMessage") as FrameworkElement;
                if (lodMinMessage != null)
                {
                    if (lodControl.CurrentLevel == 1)
                    {
                        lodMinMessage.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        lodMinMessage.Visibility = Visibility.Collapsed;
                    }
                }

                lodTimer.Stop();
                this.TimeActivityGraphHost.HideLoadingScreen();
            };

            lodTimer.Interval = TimeSpan.FromTicks(1);
            lodTimer.Start();               
        }

        /// <summary>
        /// Handles the SectionInitialized event of the TimeActivityGraphHost control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void TimeActivityGraphHost_SectionInitialized(object sender, RoutedEventArgs e)
        {
            GraphSection section = sender as GraphSection;
            if (section != null)
            {
                if (string.Compare(section.SectionName, "measurements", StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    section.HeaderTemplate = this.LayoutRoot.Resources["ObsSectionTemplate"] as DataTemplate;
                }
                else
                {
                    section.HeaderTemplate = this.LayoutRoot.Resources["SectionTemplate"] as DataTemplate;
                }
            }
        }

        /// <summary>
        /// Handles the SizeChanged event of the ScrollViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        private void ScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScrollViewer scrollViewer = sender as ScrollViewer;
            scrollViewer.MouseWheel += new MouseWheelEventHandler(this.ScrollViewer_MouseWheel);
            ScrollBar verticalSb = GetVerticalScrollbar(scrollViewer);

            if (GetParentScrollViewer(verticalSb) == null)
            {
                SetParentScrollViewer(verticalSb, scrollViewer);
                verticalSb.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.VerticalScrollbar_ValueChanged);
            }

            UpdateOutOfViewValues(verticalSb);
            scrollViewer.GotFocus += new RoutedEventHandler(this.ScrollViewer_GotFocus);
            scrollViewer.LostFocus += new RoutedEventHandler(this.ScrollViewer_LostFocus);
        }

        /// <summary>
        /// Handles the MouseWheel event of the scrollViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseWheelEventArgs"/> instance containing the event data.</param>
        private void ScrollViewer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = sender as ScrollViewer;
            double offset = e.Delta / 120; // 120 is the factor SL runtime uses to generate delta.
            offset *= -scrollViewer.ViewportHeight / 4; // scrolling quarter of the viewport on one scroll.
            
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + offset);
        }

        /// <summary>
        /// Handles the LostFocus event of the ScrollViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ScrollViewer_LostFocus(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(sender as Control, "Unfocused", true);
        }

        /// <summary>
        /// Handles the GotFocus event of the ScrollViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ScrollViewer_GotFocus(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource == sender)
            {
                VisualStateManager.GoToState(sender as Control, "Focused", true);
            }
        }

        /// <summary>
        /// Handles the SizeChanged event of the SectionRoot control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        private void SectionRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FrameworkElement sectionRoot = sender as FrameworkElement;
            bool visible = e.NewSize.Height > this.TimeActivityGraphHost.SectionMinHeight;
            SetVisibility(sectionRoot.FindName("OutOfViewIndicators"), visible);
            SetVisibility(sectionRoot.FindName("GraphArea"), visible);
            SetVisibility(sectionRoot.FindName("SectionControlsPanel"), visible);
            SetVisibility(sectionRoot.FindName("OutOfViewTitle"), visible);
        }
        
        /// <summary>
        /// Handles the ValueChanged event of the VerticalScrollbar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event args instance containing the event data.</param>
        private void VerticalScrollbar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScrollBar scrollBar = sender as ScrollBar;           
            UpdateOutOfViewValues(scrollBar);            
        }

        /// <summary>
        /// Handles the SizeChanged event of the ItemsControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        private void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ItemsControl itemsControl = sender as ItemsControl;
            ScrollViewer scrollViewer = itemsControl.FindName("ScrollViewer") as ScrollViewer;
            GraphSection graphSection = scrollViewer.DataContext as GraphSection;

            object focusedElement = FocusManager.GetFocusedElement();
            bool checkTag = true;
            if (focusedElement != null)
            {
                if (itemsControl.Items.Contains(focusedElement))
                {
                    checkTag = false;
                }
            }

            if (!checkTag)
            {
                TimeGraphBase graph = focusedElement as TimeGraphBase;
                if (graph != null)
                {
                    double margin = (scrollViewer.ViewportHeight - graph.ActualHeight) / 2;
                    scrollViewer.ScrollIntoView(graph, 0d, margin, new System.Windows.Duration(TimeSpan.FromMilliseconds(0)));
                }
            }
            else
            {
                LevelOfDetail lod = itemsControl.FindName("LevelOfDetail") as LevelOfDetail;
                if (lod.Tag != null)
                {
                    int index = int.Parse(lod.Tag.ToString(), CultureInfo.CurrentCulture);
                    if (index == int.MinValue)
                    {
                        scrollViewer.ScrollToTop();
                    }
                    else if (index == int.MaxValue)
                    {
                        scrollViewer.ScrollToBottom();
                    }
                    else
                    {
                        TimeGraphBase graph = itemsControl.Items[index] as TimeGraphBase;
                        double margin = (scrollViewer.ViewportHeight - graph.ActualHeight) / 2;
                        scrollViewer.ScrollIntoView(graph, 0d, margin, new System.Windows.Duration(TimeSpan.FromMilliseconds(0)));
                    }

                    lod.Tag = null;
                }
                else
                {
                    scrollViewer.ScrollToTop();
                }
            }

            UpdateGraphSectionValues(scrollViewer, itemsControl, graphSection);            
        }        
                
        /// <summary>
        /// Handles the Click event of the ItemsOutOfViewBottomArrow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ItemsOutOfViewBottomArrow_Click(object sender, RoutedEventArgs e)
        {
            Button outOfViewButton = sender as Button;            
            ScrollViewer scrollViewer = outOfViewButton.FindName("ScrollViewer") as ScrollViewer;
            ItemsControl itemsControl = outOfViewButton.FindName("ItemsControl") as ItemsControl;
            int currentOutOfViewItem = int.Parse(((outOfViewButton.Content as Panel).Children[0] as TextBlock).Text, CultureInfo.CurrentCulture);
            int itemCount = itemsControl.Items.Count;
            if (itemCount > 0)
            {
                TimeActivityGraph dummyGraph = itemsControl.Items[itemCount - 1] as TimeActivityGraph;
                if (dummyGraph != null && string.IsNullOrEmpty(dummyGraph.Title))
                {
                    itemCount--;
                }
            }

            FrameworkElement topElement = itemsControl.Items[itemCount - currentOutOfViewItem] as FrameworkElement;
            Rect bounds = LayoutInformation.GetLayoutSlot(topElement);
            scrollViewer.ScrollToVerticalOffset(bounds.Top);
        }

        /// <summary>
        /// Handles the Click event of the ItemsOutOfViewTopArrow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ItemsOutOfViewTopArrow_Click(object sender, RoutedEventArgs e)
        {
            Button outOfViewButton = sender as Button;
            ScrollViewer scrollViewer = outOfViewButton.FindName("ScrollViewer") as ScrollViewer;
            ItemsControl itemsControl = outOfViewButton.FindName("ItemsControl") as ItemsControl;
            int currentOutOfViewItem = int.Parse(((outOfViewButton.Content as Panel).Children[1] as TextBlock).Text, CultureInfo.CurrentCulture);

            FrameworkElement bottomElement = itemsControl.Items[currentOutOfViewItem - 1] as FrameworkElement;

            scrollViewer.ScrollIntoView(bottomElement, 0, scrollViewer.ViewportHeight - bottomElement.ActualHeight, new System.Windows.Duration(TimeSpan.FromSeconds(0)));
        }

        /// <summary>
        /// Handles the GotFocus event of the Graph control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Graph_GotFocus(object sender, RoutedEventArgs e)
        {
            TimeGraphBase graph = sender as TimeGraphBase;
            ScrollViewer scrollViewer = null;

            scrollViewer = graph.GetVisualAncestorsAndSelf().OfType<ScrollViewer>().ElementAt(0);
            if (scrollViewer != null)
            {
                BringIntoView(graph, scrollViewer);
            }
        }

        /// <summary>
        /// Handles the SectionReset event of the TimeActivityGraphHost control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void TimeActivityGraphHost_SectionReset(object sender, RoutedEventArgs e)
        {
            FrameworkElement sectionRoot = sender as FrameworkElement;
            LevelOfDetail lodControl = sectionRoot.FindName("LevelOfDetail") as LevelOfDetail;

            if (lodControl.CurrentLevel != 2)
            {
                lodControl.CurrentLevel = 2;
            }
            else
            {
                UpdateGraphProperties(2, lodControl.DataContext as GraphSection);
            }

            SetVisibility(sectionRoot.FindName("OutOfViewIndicators"), true);
            SetVisibility(sectionRoot.FindName("GraphArea"), true);
            SetVisibility(sectionRoot.FindName("SectionControlsPanel"), true);

            ScrollViewer scrollViewer = sectionRoot.FindName("ScrollViewer") as ScrollViewer;
            scrollViewer.ScrollToVerticalOffset(0);            
        }

        /// <summary>
        /// Handles the Refresh event of the TimeActivityGraphHost control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void TimeActivityGraphHost_Refresh(object sender, RoutedEventArgs e)
        {
            this.InitializeGraphs(false);
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the MedicationsLine control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void MedicationsLine_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.MedsDetailsDialog, "MedsDetailsDialogClose");
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.MedsDetailsDialog.DataContext = graphPoint.DataContext;
            this.MedsDetailsDialog.Show();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the ProblemsLine control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ProblemsLine_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.ProblemsDialog, "ProblemsCloseButton");
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.ProblemsDialog.DataContext = graphPoint.DataContext;
            this.ProblemsDialog.Show();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Events.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Event_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.EventsDialog, "EventsCloseButton");
            this.EventsDialog.DataContext = (sender as FrameworkElement).DataContext;
            (this.EventsDialog.DialogContent as ActivityDialog).UpdateDisplay();
            this.EventsDialog.Show();
        }

        /// <summary>
        /// Handles the LabelClick event of the TimeActivityGraph control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void MedicationsGraph_LabelClick(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.MedsDetailsDialog, "MedsDetailsDialogClose");
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.MedsDetailsDialog.DataContext = graphPoint.DataContext;
            this.MedsDetailsDialog.Show();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the MedicationsLine control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SecondaryCareMedicationsLine_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.SecondaryCareMedsDetailsDialog, "CloseButton");
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.SecondaryCareMedsDetailsDialog.DataContext = graphPoint.DataContext;
            ((this.SecondaryCareMedsDetailsDialog.DialogContent as Grid).Children[1] as MedicationLabel).GetDesiredWidth(this.MaxWidth);
            this.SecondaryCareMedsDetailsDialog.Show();
        }

        /// <summary>
        /// Handles the LabelClick event of the TimeActivityGraph control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SecondaryCareMedicationsGraph_LabelClick(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.SecondaryCareMedsDetailsDialog, "CloseButton");
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.SecondaryCareMedsDetailsDialog.DataContext = graphPoint.DataContext;
            ((this.SecondaryCareMedsDetailsDialog.DialogContent as Grid).Children[1] as MedicationLabel).GetDesiredWidth(this.MaxWidth);
            this.SecondaryCareMedsDetailsDialog.Show();
        }

        /// <summary>
        /// Handles the LabelClick event of the ProblemsGraph control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ProblemsGraph_LabelClick(object sender, RoutedEventArgs e)
        {
            this.SetFocusInDialog(this.ProblemsDialog, "ProblemsCloseButton");
            GraphPoint graphPoint = (sender as FrameworkElement).DataContext as GraphPoint;
            this.ProblemsDialog.DataContext = graphPoint.DataContext;
            this.ProblemsDialog.Show();
        }
        
        /// <summary>
        /// Handles the Loaded event of the ActivityTypeList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ActivityTypeList_Loaded(object sender, RoutedEventArgs e)
        {
            Panel activityTypeList = sender as Panel;
            activityTypeList.Children.Clear();

            string sectionName = activityTypeList.Tag.ToString();
            List<string> activityTypes = new List<string>();
            var result = from graphData in this.xmlGraphData
                         where string.Compare(graphData.SectionName, sectionName, StringComparison.CurrentCultureIgnoreCase) == 0
                         select graphData;

            foreach (TimelineGraphData graph in result)
            {
                foreach (string key in graph.Activities.Keys)
                {
                    if (!activityTypes.Contains(key))
                    {
                        activityTypes.Add(key);
                        CheckBox checkBox = new CheckBox();
                        checkBox.Content = key;
                        checkBox.Tag = graph.SectionName;
                        checkBox.VerticalAlignment = VerticalAlignment.Center;
                        checkBox.Style = App.Current.Resources["CheckBoxStyle"] as Style;
                        checkBox.Foreground = this.LayoutRoot.Resources["SectionHeader_Foreground_Brush"] as Brush;
                        checkBox.FontWeight = FontWeights.Bold;                        
                        checkBox.Click += new RoutedEventHandler(this.CheckBox_Click);
                        activityTypeList.Children.Add(checkBox);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the CheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            this.TimeActivityGraphHost.ShowLoadingScreen();

            CheckBox activityTypeCheckBox = sender as CheckBox;
            Panel activityTypeList = (sender as FrameworkElement).Parent as Panel;
            ScrollViewer scrollViewer = activityTypeList.FindName("ScrollViewer") as ScrollViewer;
            LevelOfDetail lodControl = activityTypeList.FindName("LevelOfDetail") as LevelOfDetail;
            RegisterScrollViewerPosition(lodControl, scrollViewer, lodControl.DataContext as GraphSection);
            
            System.Windows.Threading.DispatcherTimer activityTimer = new System.Windows.Threading.DispatcherTimer();
            activityTimer.Tick += delegate(object s, EventArgs args)
            {
                if (activityTypeCheckBox.IsChecked.Value)
                {
                    if (lodControl.CurrentLevel < MinimumLevelOfDetailForActivities && activityTypeCheckBox.IsChecked.Value)
                    {
                        lodControl.CurrentLevel = MinimumLevelOfDetailForActivities;
                    }

                    this.AddActivity(activityTypeList.Tag.ToString(), activityTypeCheckBox.Content.ToString());
                }
                else
                {
                    this.RemoveActivity(activityTypeList.Tag.ToString(), activityTypeCheckBox.Content.ToString());
                }

                activityTimer.Stop();
                this.TimeActivityGraphHost.HideLoadingScreen();
            };

            activityTimer.Interval = TimeSpan.FromTicks(1);
            activityTimer.Start();                     
        }        

        /// <summary>
        /// Handles the Close button click event of the SecondaryCareMedsDetailsDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CloseSecondaryCareMedsDetailsDialog_Click(object sender, RoutedEventArgs e)
        {
            this.RestoreFocus(sender as Control);
            this.SecondaryCareMedsDetailsDialog.Hide();
        }

        /// <summary>
        /// Handles the Click event of the CloseDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CloseMedsDialog_Click(object sender, RoutedEventArgs e)
        {
            this.RestoreFocus(sender as Control);
            this.MedsDetailsDialog.Hide();
        }

        /// <summary>
        /// Handles the Click event of the CloseProblemsDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CloseProblemsDialog_Click(object sender, RoutedEventArgs e)
        {
            this.RestoreFocus(sender as Control);
            this.ProblemsDialog.Hide();
        }

        /// <summary>
        /// Handles the Click event of the CloseEventsDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void CloseEventsDialog_Click(object sender, RoutedEventArgs e)
        {
            this.RestoreFocus(sender as Control);
            this.EventsDialog.Hide();
        }
        #endregion
    }
}
