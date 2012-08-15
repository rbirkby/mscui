//-----------------------------------------------------------------------
// <copyright file="Graphhost.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>05-Sep-2008</date>
// <summary>Graph host class.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Graph host class.
    /// </summary>
    [TemplatePart(Name = GraphHost.RootPanelElementName, Type = typeof(StackPanel))]
    [TemplatePart(Name = GraphHost.LayoutRootElementName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = GraphHost.TimeSelectorElementName, Type = typeof(System.Windows.Controls.ComboBox))]
    [TemplatePart(Name = GraphHost.TopAxisElementName, Type = typeof(TimeGraphBase))]
    [TemplatePart(Name = GraphHost.BottomAxisElementName, Type = typeof(TimeGraphBase))]
    [TemplatePart(Name = GraphHost.ScrollBarElementName, Type = typeof(ScrollBar))]
    [TemplatePart(Name = GraphHost.GraphAreaElementName, Type = typeof(Panel))]
    [TemplatePart(Name = GraphHost.ShowVFLButtonName, Type = typeof(ToggleButton))]
    [TemplatePart(Name = GraphHost.ShowDataPointLabelsButtonName, Type = typeof(ToggleButton))]
    [TemplatePart(Name = GraphHost.ShowGridLinesButtonName, Type = typeof(ToggleButton))]
    [TemplatePart(Name = GraphHost.ShowInterpolationLinesButtonName, Type = typeof(ToggleButton))]
    [TemplatePart(Name = GraphHost.ResetButtonElementName, Type = typeof(Button))]
    [TemplatePart(Name = GraphHost.VisualFocusLineElementName, Type = typeof(Line))]
    [TemplatePart(Name = GraphHost.ScrollViewerElementName, Type = typeof(ScrollViewer))]    
    [TemplatePart(Name = GraphHost.DataSelectorControlElementName, Type = typeof(DataSelector))]
    public class GraphHost : Control
    {
        #region Dependency properties
        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.GraphHost.VisibleWindow" /> dependency property.
        /// </summary>
        internal static readonly DependencyProperty VisibleWindowProperty = DependencyProperty.Register("VisibleWindow", typeof(TimeFrequency), typeof(GraphHost), new PropertyMetadata(new PropertyChangedCallback(VisibleWindowPropertyChanged)));

        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.GraphHost.TimeFrequencySelectedIndex" /> dependency property.
        /// </summary>
        internal static readonly DependencyProperty TimeFrequencySelectedIndexProperty = DependencyProperty.Register("TimeFrequencySelectedIndex", typeof(int), typeof(GraphHost), new PropertyMetadata(new PropertyChangedCallback(TimeFrequencySelectedIndexPropertyChanged)));

        #endregion
        
        #region Template Part Element Names
        /// <summary>
        /// Root panel element name.
        /// </summary>
        private const string RootPanelElementName = "ELEMENT_RootPanel";

        /// <summary>
        /// Layout root element name.
        /// </summary>
        private const string LayoutRootElementName = "ELEMENT_LayoutRoot";

        /// <summary>
        /// Time selector element name.
        /// </summary>
        private const string TimeSelectorElementName = "ELEMENT_TimeSelector";

        /// <summary>
        /// Top axis element name.
        /// </summary>
        private const string TopAxisElementName = "ELEMENT_TopAxis";

        /// <summary>
        /// Top axis element container name.
        /// </summary>
        private const string TopAxisContainerElementName = "ELEMENT_TopAxisContainer";

        /// <summary>
        /// Bottom axis element name.
        /// </summary>
        private const string BottomAxisElementName = "ELEMENT_BottomAxis";

        /// <summary>
        /// Bottom axis element container name.
        /// </summary>
        private const string BottomAxisContainerElementName = "ELEMENT_BottomAxisContainer";

        /// <summary>
        /// Scrollbar element name.
        /// </summary>
        private const string ScrollBarElementName = "ELEMENT_ScrollBar";

        /// <summary>
        /// Graph area element name.
        /// </summary>
        private const string GraphAreaElementName = "ELEMENT_GraphArea";

        /// <summary>
        /// Show visual focus line button name.
        /// </summary>
        private const string ShowVFLButtonName = "ELEMENT_ShowVFL";

        /// <summary>
        /// Show interpolation lines button name.
        /// </summary>
        private const string ShowInterpolationLinesButtonName = "ELEMENT_ShowInterpolationLines";

        /// <summary>
        /// Show grid lines button name.
        /// </summary>
        private const string ShowGridLinesButtonName = "ELEMENT_ShowGridLines";

        /// <summary>
        /// Show data point labels button name.
        /// </summary>
        private const string ShowDataPointLabelsButtonName = "ELEMENT_ShowDataPointLabels";

        /// <summary>
        /// Visual focus line element name.
        /// </summary>
        private const string VisualFocusLineElementName = "ELEMENT_VisualFocusLine";

        /// <summary>
        /// ScrollViewer element name.
        /// </summary>
        private const string ScrollViewerElementName = "ELEMENT_ScrollViewer";
                
        /// <summary>
        /// Data selector control element name.
        /// </summary>
        private const string DataSelectorControlElementName = "ELEMENT_DataSelectorControl";

        /// <summary>
        /// Reset button element name.
        /// </summary>
        private const string ResetButtonElementName = "ELEMENT_Reset";

        /// <summary>
        /// FocusVisual element name.
        /// </summary>
        private const string FocusVisualElementName = "FocusVisualElement";

        /// <summary>
        /// FocusVisual for horizontal scrollbar element name.
        /// </summary>
        private const string HorizontalScrollBarFocusVisualElementName = "ScrollBarFocusVisualElement";

        /// <summary>
        /// Default style for the top axis.
        /// </summary>
        private const string TopAxisDefaultStyleResourceKey = "TopAxisStyle";

        /// <summary>
        /// Default style for the bottom axis.
        /// </summary>
        private const string BottomAxisDefaultStyleResourceKey = "BottomAxisStyle";

        /// <summary>
        /// Resource key for time frequency binding helper.
        /// </summary>
        private const string TimeFrequencyBindingHelperResourceKey = "timeFrequency";
        #endregion

        #region Error Messages
        /// <summary>
        /// Error message string used when the template part element is missing.
        /// </summary>
        private const string TemplatePartElementNullMessage = @"Could not find an element with name '{0}' in the template.";

        /// <summary>
        /// Error message string used when the template part element is of incorrect type.
        /// </summary>
        private const string TemplatePartElementTypeInvalidMessage = @"Element with name '{0}' in the template is of invalid type. Expected type is '{1}'.";
        #endregion

        #region Template elements
        /// <summary>
        /// Member variable to hold horizontal scrollbar.
        /// </summary>
        private ScrollBar horizontalScrollBar;

        /// <summary>
        /// Member variable to hold top axis.
        /// </summary>
        private TimeGraphBase topAxis;

        /// <summary>
        /// Member variable to hold top axis container.
        /// </summary>
        private FrameworkElement topAxisContainer;

        /// <summary>
        /// Member variable to hold bottom axis.
        /// </summary>
        private TimeGraphBase bottomAxis;

        /// <summary>
        /// Member variable to hold bottom axis container.
        /// </summary>
        private FrameworkElement bottomAxisContainer;

        /// <summary> 
        /// Member variable to hold stack panel.
        /// </summary>
        private StackPanel stackPanel;

        /// <summary>
        /// Member variable to hold time selector element.
        /// </summary>
        private System.Windows.Controls.ComboBox timeSelector;

        /// <summary>
        /// Member variable to hold root element.
        /// </summary>
        private FrameworkElement layoutRoot;

        /// <summary>
        /// Member variable to hold graph area.
        /// </summary>
        private Panel graphArea;

        /// <summary>
        /// Member variable to hold data selector control.
        /// </summary>
        private DataSelector dataSelector;

        /// <summary>
        /// Member variable to hold show visual focus line button.
        /// </summary>
        private ToggleButton showVFL;

        /// <summary>
        /// Member variable to hold show grid lines button.
        /// </summary>
        private ToggleButton showGridLines;

        /// <summary>
        /// Member variable to hold show interpolation lines button.
        /// </summary>
        private ToggleButton showInterpolationLines;

        /// <summary>
        /// Member variable to hold show data point labels button.
        /// </summary>
        private ToggleButton showDataPointLabels;

        /// <summary>
        /// Member variable to hold reset button.
        /// </summary>
        private Button reset;

        /// <summary>
        /// Member variable to hold visual focus line.
        /// </summary>
        private VisualFocusLine visualFocusLine;

        /// <summary>
        /// Member variable to hold scroll viewer.
        /// </summary>
        private ScrollViewer scrollViewer;

        /// <summary>
        /// Member variable to hold the scroll stack panel.
        /// </summary>
        private StackPanel scrollStack;

        /// <summary>
        /// Member variable to hold the focus visual element.
        /// </summary>
        private UIElement focusVisual;

        /// <summary>
        /// Member variable to hold focus visual for horizontal scrollbar.
        /// </summary>
        private UIElement horizontalScrollbarFocusVisual;
        #endregion

        #region Private Members

        /// <summary>
        /// Member variable to hold graph number.
        /// </summary>
        private int graphCounter;

        /// <summary>
        /// Member variable to hold graphs.
        /// </summary>
        private ObservableCollection<GraphBase> graphs = new ObservableCollection<GraphBase>();

        /// <summary>
        /// Member variable to hold axis.
        /// </summary>
        private Collection<GraphBase> axes = new Collection<GraphBase>();

        /// <summary>
        /// Member variable to convert scrollbar value to a double.
        /// </summary>
        private DoubleBindingHelper doubleBindingHelper = new DoubleBindingHelper();

        /// <summary>
        /// Binding helper object for scrollbar max value.
        /// </summary>
        private DoubleBindingHelper scrollBarMaxValueBindingHelper = new DoubleBindingHelper();
        
        /// <summary>
        /// Binding helper object for scrollbar viewport size.
        /// </summary>
        private DoubleBindingHelper scrollBarViewportSizeBindingHelper = new DoubleBindingHelper();

        /// <summary>
        /// Member variable to convert selected time range to a Time frequency object.
        /// </summary>
        private TimeFrequencyBindingHelper timeFrequencyBindingHelper;

        /// <summary>
        /// Member variable to help in scrollbar binding.
        /// </summary>
        private Binding scrollBarBinding = new Binding("Value");

        /// <summary>
        /// Binding object used for binding scrollbar max value.
        /// </summary>
        private Binding scrollBarMaxValueBinding = new Binding("Value");
            
        /// <summary>
        /// Binding object used for binding scrollbar viewport size.
        /// </summary>
        private Binding scrollBarViewportSizeBinding = new Binding("Value");

        /// <summary>
        /// Member variable to help in visible window binding.
        /// </summary>
        private Binding timeFrequencyBinding = new Binding("TimeFrequency");        

        /// <summary>
        /// Member variable to hold start date of the axis.
        /// </summary>
        private DateTime? axisStartDateTime;

        /// <summary>
        /// Member variable to hold end date of the axis.
        /// </summary>
        private DateTime? axisEndDateTime;

        /// <summary>
        /// Member variable to hold visual focus line offset.
        /// </summary>
        private double vflOffset;

        /// <summary>
        /// Member variable to hold the max offset value for visual focus line.
        /// </summary>
        private double vflMaxOffset;

        /// <summary>
        /// Member variable to indicate whether to show VFL.
        /// </summary>
        private bool visualFocusLineVisibility;

        /// <summary>
        /// Member variable to indicate whether to show data point labels.
        /// </summary>
        private Visibility dataPointLabelsVisibility = Visibility.Collapsed;

        /// <summary>
        /// Member variable to indicate whether to show grid lines.
        /// </summary>
        private GridLineVisibility gridLinesVisibility = GridLineVisibility.Both;

        /// <summary>
        /// Member variable to indicate whether to show interpolation lines.
        /// </summary>
        private bool interpolationLinesVisibility = true;

        /// <summary>
        /// Member variable to hold the offset of the graph from scroll viewer.
        /// </summary>
        /// <remarks>Contains the offset of the graph from scroll viewer.</remarks>
        private double graphMargin;

        /// <summary>
        /// Member variable to hold the distance from the left edge of the graph to the plot layer.
        /// </summary>
        private double graphPadding;

        /// <summary>
        /// Member variable to hold default style of the top axis.
        /// </summary>
        private Style defaultTopAxisStyle;

        /// <summary>
        /// Member variable to hold default style of the bottom axis.
        /// </summary>
        private Style defaultBottomAxisStyle;

        /// <summary>
        /// Member variable to hold graphs for data selector.
        /// </summary>
        private ObservableCollection<GraphSummary> dataSelectorGraphs = new ObservableCollection<GraphSummary>();

        /// <summary>
        /// Gets the list of data markers that have been snapped by visual focus line.
        /// </summary>
        private System.Collections.Generic.List<VisualFocusSnappedItem> snappedDataMarkers = new System.Collections.Generic.List<VisualFocusSnappedItem>();

        /// <summary>
        /// Default Indicator for the showVFL Button.
        /// </summary>
        private bool? defaultVisualFocusLine;

        /// <summary>
        /// Default Indicator for the showVFL Button.
        /// </summary>
        private GridLineVisibility? defaultGridLines;

        /// <summary>
        /// Default Indicator for the showVFL Button.
        /// </summary>
        private bool? defaultInterpolationLines;

        /// <summary>
        /// Default Indicator for the showVFL Button.
        /// </summary>
        private Visibility? defaultDataPointLabels;

        /// <summary>
        /// Indicator for whether bindings have been created.
        /// </summary>
        private bool bindingsCreated;

#if SILVERLIGHT
        /// <summary>
        /// Value for the alignment of the x-axis.
        /// </summary>
        private double? xaxisAlignment;
#endif

        /// <summary>
        /// Member variable to indicate current scrollbar position.
        /// </summary>
        /// <remarks>
        /// Possible values are -1,0,1. 
        /// -1 indicates scrollbar is at minimum, 1 indicates at scrollbar at maximun and 0 at other.
        /// when 0 scroll to retain the date in viewport
        /// </remarks>         
        private sbyte scrollPosition = 1;

        /// <summary>
        /// Member variable to hold current date time in the viewport.
        /// </summary>
        private DateTime? viewPortDateTime;
        #endregion        

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphHost"/> class.
        /// </summary>
        public GraphHost()
        {
            this.DefaultStyleKey = typeof(GraphHost);
            this.VisualFocusLineProximity = 10;            
        }
        #endregion

        #region Public properties
        /// <summary>
        /// Gets or sets the value of the selected index of the time frequency.
        /// </summary>
        /// <value>Selected index of the time frequency.</value>
        [Category("GraphHost Appearance")]
        public int TimeFrequencySelectedIndex
        {
            get { return (int)this.GetValue(GraphHost.TimeFrequencySelectedIndexProperty); }
            set { this.SetValue(GraphHost.TimeFrequencySelectedIndexProperty, value); }
        }       

        /// <summary>
        /// Gets the graph collection.
        /// </summary>
        /// <value>Graph collection.</value>
        [Category("GraphHost Appearance")]
        public ObservableCollection<GraphBase> Graphs
        {
            get { return this.graphs; }
        }

        /// <summary>
        /// Gets or sets the proximity for data points that can be snapped by visual focus line.
        /// </summary>
        /// <value>Proximity for data points that can be snapped by visual focus line.</value>
        [Category("GraphHost Appearance")]
        public double VisualFocusLineProximity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the Visual Focus Line is shown.
        /// </summary>
        /// <value>Indicator for whether the VFL is shown.</value>
        [Category("GraphHost Appearance")]
        public bool VisualFocusLineVisibility
        {
            get
            {
                return this.visualFocusLineVisibility;
            }

            set
            {               
                if (this.defaultVisualFocusLine.HasValue)
                {
                    if (value)
                    {
                        this.DataPointLabelsVisibility = Visibility.Collapsed;
                        this.visualFocusLineVisibility = true;
                    }

                    // Hiding the data point labels prior to updating visual focus line visibility.
                    // Turning of the data point labels will try to retain the scrollbar position and hence VFL may be turned off.
                    this.visualFocusLineVisibility = value;
                    this.ShowVisualFocusLine();
                }
                else
                {
                    this.defaultVisualFocusLine = this.visualFocusLineVisibility;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Interpolation Lines are shown.
        /// </summary>
        /// <value>Indicator for the Visibility of the Interpolation Lines.</value>
        [Category("GraphHost Appearance")]
        public bool InterpolationLinesVisibility
        {
            get
            {
                return this.interpolationLinesVisibility;
            }

            set
            {
                this.interpolationLinesVisibility = value;

                if (this.defaultInterpolationLines.HasValue)
                {
                    this.showInterpolationLines.IsChecked = value;
                    this.SetInterpolationLinesVisibility();
                    this.ShowAllSnappedDataPointLabels();
                }
                else
                {
                    this.defaultInterpolationLines = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Visibility for the Grid Lines.
        /// </summary>
        /// <value>Indicator for the Visibility of the Grid Lines.</value>
        [Category("GraphHost Appearance")]
        public GridLineVisibility GridLinesVisibility
        {
            get
            {
                return this.gridLinesVisibility;
            }

            set
            {
                this.gridLinesVisibility = value;

                if (this.defaultGridLines.HasValue)
                {
                    this.showGridLines.IsChecked = this.gridLinesVisibility == GridLineVisibility.None ? false : true;

                    double scrollValue = this.horizontalScrollBar.Value;
                    this.SetGridLinesVisibility();
                    this.horizontalScrollBar.Value = scrollValue;
                    this.ShowAllSnappedDataPointLabels();
                }
                else
                {
                    this.defaultGridLines = this.gridLinesVisibility;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Visibility for the Data Point Labels.
        /// </summary>
        /// <value>Indicator for the Visibility of the Data Point Labels.</value>
        [Category("GraphHost Appearance")]
        public Visibility DataPointLabelsVisibility
        {
            get
            {
                return this.dataPointLabelsVisibility;
            }

            set
            {
                this.dataPointLabelsVisibility = value;

                if (this.defaultDataPointLabels.HasValue)
                {
                    this.showDataPointLabels.IsChecked = (this.dataPointLabelsVisibility == Visibility.Visible) ? true : false;
                    this.SetDataPointLabelsVisibility();

                    if (this.dataPointLabelsVisibility == Visibility.Visible)
                    {
                        this.VisualFocusLineVisibility = false;
                    }
                }
                else
                {
                    this.defaultDataPointLabels = this.dataPointLabelsVisibility;
                }
            }
        }
        #endregion

        #region Internal Properties
        /// <summary>
        /// Gets or sets the visible window time frequency.
        /// </summary>
        /// <value>Time frequency for the visible window.</value>
        internal TimeFrequency VisibleWindow
        {
            get { return (TimeFrequency)this.GetValue(VisibleWindowProperty); }
            set { this.SetValue(VisibleWindowProperty, value); }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Overridden. Applies the specified template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.LoadTemplateParts();
            this.dataSelectorGraphs.Clear();
            this.GetDataSelectorGraphs();

            this.dataSelector.ItemsSource = this.dataSelectorGraphs;
            this.dataSelector.Swap += new EventHandler<ReorderedEventArgs>(this.DataSelector_Swap);

            if (this.topAxis.Style == null)
            {
                this.topAxis.Style = this.defaultTopAxisStyle;
            }

            if (this.bottomAxis.Style == null)
            {
                this.bottomAxis.Style = this.defaultBottomAxisStyle;
            }

            this.showVFL.Click += new RoutedEventHandler(this.ShowVFL_Click);
            this.showDataPointLabels.Click += new RoutedEventHandler(this.ShowDataPointLabels_Click);
            this.showGridLines.Click += new RoutedEventHandler(this.ShowGridLines_Click);
            this.showInterpolationLines.Click += new RoutedEventHandler(this.ShowInterpolationLines_Click);
            this.reset.Click += new RoutedEventHandler(this.Reset_Click);

            this.SizeChanged += new SizeChangedEventHandler(this.GraphHost_SizeChanged);
            this.horizontalScrollBar.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.HorizontalScrollBar_ValueChanged);
            this.horizontalScrollBar.Scroll += new ScrollEventHandler(this.HorizontalScrollBar_Scroll);
            this.Loaded += new RoutedEventHandler(this.GraphHost_Loaded); 

            this.horizontalScrollBar.KeyDown += new KeyEventHandler(this.HorizontalScrollBar_KeyDown);
            this.horizontalScrollBar.GotFocus += new RoutedEventHandler(this.HorizontalScrollBar_GotFocus);
            this.horizontalScrollBar.LostFocus += new RoutedEventHandler(this.HorizontalScrollBar_LostFocus);

            this.scrollViewer.GotFocus += new RoutedEventHandler(this.ScrollViewer_GotFocus);
            this.scrollViewer.LostFocus += new RoutedEventHandler(this.ScrollViewer_LostFocus);

            this.axes = new Collection<GraphBase>();
            this.axes.Add(this.topAxis);
            this.axes.Add(this.bottomAxis);

#if !SILVERLIGHT
            this.horizontalScrollBar.Focusable = true;
            this.horizontalScrollBar.FocusVisualStyle = null;
#endif
            if (this.VisibleWindow == null)
            {
                this.VisibleWindow = StyleParser.FindResource<TimeFrequency>(this.layoutRoot, "DefaultVisibleWindow", false);
            }
        }               

        /// <summary>
        /// Gets a graph with a specified name from the graph collection.
        /// </summary>
        /// <param name="graphName">Name of the graph.</param>
        /// <returns>Graph with the specified name.</returns>
        public TimeGraphBase GetGraphByName(string graphName)
        {
            TimeGraphBase returnGraph = null;

            foreach (TimeGraphBase graph in this.graphs)
            {
                if (string.Compare(graphName, graph.Name, StringComparison.CurrentCulture) == 0)
                {
                    returnGraph = graph;
                    break;
                }
            }

            return returnGraph;
        }

        /// <summary>
        /// Refreshes the graph axis data.
        /// </summary>
        public void Refresh()
        {
            // Refresh and then set the scroll position to the maximum value
            this.RefreshInternal();
            this.horizontalScrollBar.Value = this.horizontalScrollBar.Maximum;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Handles the keydown on the control.
        /// </summary>
        /// <param name="e">Event args.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            bool ctrlKeyPressed;
            bool shiftKeyPressed;
            bool altKeyPressed;

            KeyboardHelper.GetMetaKeyState(out ctrlKeyPressed, out shiftKeyPressed, out altKeyPressed);

            if (ctrlKeyPressed && shiftKeyPressed && !altKeyPressed && !e.Handled)
            {
                switch (e.Key)
                {
                    case Key.D5:
                        this.Reset_Click(this, null);
                        e.Handled = true;
                        break;
                    case Key.D6:
                        this.ShowVFL_Click(this, null);
                        this.showVFL.IsChecked = !this.showVFL.IsChecked;
                        e.Handled = true;
                        break;
                    case Key.D7:
                        this.ShowDataPointLabels_Click(this, null);
                        this.showDataPointLabels.IsChecked = !this.showDataPointLabels.IsChecked;
                        e.Handled = true;
                        break;
                    case Key.D8:
                        this.ShowInterpolationLines_Click(this, null);
                        this.showInterpolationLines.IsChecked = !this.showInterpolationLines.IsChecked;
                        e.Handled = true;
                        break;
                    case Key.D9:
                        this.ShowGridLines_Click(this, null);
                        this.showGridLines.IsChecked = !this.showGridLines.IsChecked;
                        e.Handled = true;
                        break;
                }
            }
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Handles the visible window property changed event.
        /// </summary>
        /// <param name="d">Sender of the event.</param>
        /// <param name="e">Event args containing instance data.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "e", Justification = "Needed to match the signature of the delegate.")]
        private static void VisibleWindowPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GraphHost graphHost = d as GraphHost;
            if (graphHost != null)
            {
                bool scrollToDate = false;
                sbyte scrollPosition = 1;
                DateTime? viewPortDateTime = null;

                if (e.OldValue != null)
                {
                    if (graphHost.horizontalScrollBar.Value == graphHost.horizontalScrollBar.Maximum)
                    {
                        scrollPosition = 1;
                    }
                    else if (graphHost.horizontalScrollBar.Value == graphHost.horizontalScrollBar.Minimum)
                    {
                        scrollPosition = -1;
                    }
                    else
                    {
                        viewPortDateTime = graphHost.GetDateInView(e.OldValue as TimeFrequency);
                        scrollPosition = 0;
                    }

                    scrollToDate = true;
                }

                graphHost.RefreshInternal();

                if (scrollToDate)
                {
                    if (scrollPosition == 1)
                    {
                        graphHost.horizontalScrollBar.Value = graphHost.horizontalScrollBar.Maximum;
                    }
                    else if (scrollPosition == -1)
                    {
                        graphHost.horizontalScrollBar.Value = graphHost.horizontalScrollBar.Minimum;
                    }
                    else
                    {
                        if (viewPortDateTime.HasValue)
                        {
                            graphHost.topAxis.ScrollToDate(viewPortDateTime.Value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the time frequency selected index property changed event.
        /// </summary>
        /// <param name="d">Sender of the event.</param>
        /// <param name="e">Event args containing instance data.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "e", Justification = "Needed to match the signature of the delegate.")]
        private static void TimeFrequencySelectedIndexPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GraphHost graphHost = d as GraphHost;
            if (graphHost != null && graphHost.timeSelector != null)
            {
                int timeFrequencySelectedIndex = (int)e.NewValue;
                graphHost.timeSelector.SelectedIndex = timeFrequencySelectedIndex;
            }
        }
        
        /// <summary>
        /// Determines whether the data marker is overlapping.
        /// </summary>
        /// <param name="timeGraph">Graph in which the data marker is plotted.</param>
        /// <param name="data">Actual data for the data marker.</param>
        /// <param name="offset">Offset of the data.</param>
        /// <returns>True if the data marker is overlapping, otherwise false.</returns>
        private static bool IsDataMarkerOverlapping(TimeGraphBase timeGraph, object data, double offset)
        {
            FrameworkElement marker = timeGraph.GetDataMarker(data, offset);
            if (marker != null)
            {
                return GraphBase.GetDataPointOverlapProperty(marker);
            }

            return false;
        }

        /// <summary>
        /// Show or hide the focus visual.
        /// </summary>
        /// <param name="focusVisual">Focus visual element.</param>
        /// <param name="show">Boolean indicating whether to show focus visual.</param>
        private static void ShowFocusVisual(UIElement focusVisual, bool show)
        {
            if (focusVisual != null)
            {
                focusVisual.Opacity = show ? 1 : 0;
            }
        }
        #endregion

        #region Get Template Childs   
        /// <summary>
        /// Gets an element in the template.
        /// </summary>
        /// <typeparam name="T">Type of the element.</typeparam>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="raiseExceptions">Boolean to indicate whether to raise exceptions when not found.</param>
        /// <returns>If an element is found with the specified name and type then returns the element, else null.</returns>
        /// <remarks>If <paramref name="raiseExceptions"/> is true and the element is not found then an exception of type <see cref="System.ArgumentNullException"/> is thrown. 
        /// If an element is found but is not of specified type then an exception of type <see cref="System.ArgumentException"/> is thrown</remarks>                
        private T GetTemplateChild<T>(string elementName, bool raiseExceptions)
        {
            object obj = null;
            obj = this.GetTemplateChild(elementName);
            T typeCastedObj;

            if (raiseExceptions)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(string.Format(System.Globalization.CultureInfo.CurrentCulture, TemplatePartElementNullMessage, elementName));
                }

                typeCastedObj = (T)obj;
                if (typeCastedObj == null)
                {
                    throw new ArgumentException(string.Format(System.Globalization.CultureInfo.CurrentCulture, TemplatePartElementTypeInvalidMessage, elementName, typeof(T).FullName));
                }
            }

            return (T)obj;
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// Handles the keydown event on horizontal scrollbar.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void HorizontalScrollBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled == false)
            {
                bool ctrlKeyPressed = ModifierKeys.Control == (Keyboard.Modifiers & ModifierKeys.Control);
                switch (e.Key)
                {
                    case Key.Left:
                        if (ctrlKeyPressed)
                        {
                            this.horizontalScrollBar.Value -= this.horizontalScrollBar.LargeChange;
                            e.Handled = true;
                        }
                        else
                        {
                            this.horizontalScrollBar.Value -= this.horizontalScrollBar.SmallChange;
                            e.Handled = true;
                        }

                        break;
                    case Key.Right:
                        if (ctrlKeyPressed)
                        {
                            this.horizontalScrollBar.Value += this.horizontalScrollBar.LargeChange;
                            e.Handled = true;
                        }
                        else
                        {
                            this.horizontalScrollBar.Value += this.horizontalScrollBar.SmallChange;
                            e.Handled = true;
                        }

                        break;
                    case Key.Home:
                        if (ctrlKeyPressed)
                        {
                            this.horizontalScrollBar.Value = this.horizontalScrollBar.Minimum;
                            e.Handled = true;
                        }

                        break;
                    case Key.End:
                        if (ctrlKeyPressed)
                        {
                            this.horizontalScrollBar.Value = this.horizontalScrollBar.Maximum;
                            e.Handled = true;
                        }

                        break;
                }

                if (e.Handled)
                {
                    this.GetViewportDate();
                }
            }
        }

        /// <summary>
        /// Handles the Swap event in data selector control.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing information about the indices to be swapped.</param>
        private void DataSelector_Swap(object sender, ReorderedEventArgs e)
        {
            // Swap graphs in view.
            TimeGraphBase graph1 = this.stackPanel.Children[e.OldIndex] as TimeGraphBase;
            this.stackPanel.Children.RemoveAt(e.OldIndex);
            this.stackPanel.Children.Insert(e.NewIndex, graph1);

            // Swap data selector items order
            // Could have simply replace elements, but in order to retain focus, we are not moving the current element.
            // Fix for PS: 11704
            if (e.OldIndex < e.NewIndex)
            {
                for (int i = e.OldIndex; i < e.NewIndex; i++)
                {
                    GraphSummary summary1 = this.dataSelectorGraphs[e.NewIndex];
                    this.dataSelectorGraphs.RemoveAt(e.NewIndex);
                    this.dataSelectorGraphs.Insert(e.OldIndex, summary1);
                }
            }
            else
            {
                for (int i = e.NewIndex; i < e.OldIndex; i++)
                {
                    GraphSummary summary1 = this.dataSelectorGraphs[e.NewIndex];
                    this.dataSelectorGraphs.RemoveAt(e.NewIndex);
                    this.dataSelectorGraphs.Insert(e.OldIndex, summary1);
                }
            }

            // Shows the data point label for the graph reordered.
            foreach (VisualFocusSnappedItem snappedItem in this.snappedDataMarkers)
            {
                if (snappedItem.Graph.Name == graph1.Name)
                {
                    this.ShowDataPointLabel(snappedItem.Graph, snappedItem.Data, snappedItem.Offset);
                    break;
                }
            }   
        }          

        /// <summary>
        /// Handles the graph collection changed event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void Graphs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    this.AddGraphs(e.NewItems);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    this.RemoveGraphs(e.OldItems);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    this.RemoveGraphs(e.OldItems);
                    this.AddGraphs(e.NewItems);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    this.dataSelectorGraphs.Clear();
                    this.GetDataSelectorGraphs();
                    this.LoadGraphs();
                    break;
            }            
        }        

        /// <summary>
        /// Handles the graph initialized event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void Graph_Initialized(object sender, RoutedEventArgs e)
        {
            TimeGraphBase graph = sender as TimeGraphBase;

            if (!graph.ScrollBarBindingCreated)
            {
                graph.ScrollBar.SetBinding(ScrollBar.ValueProperty, this.scrollBarBinding);

                // Setting the value to max on initialize as scrollbar binding object has default value as 0
                graph.ScrollBar.Value = graph.ScrollBar.Maximum;
                graph.ScrollBarBindingCreated = true;
            }
            else
            {
                this.ScrollToViewportDate();
            }

            // Shows the data point label for the graph loaded.
            foreach (VisualFocusSnappedItem snappedItem in this.snappedDataMarkers)
            {
                if (snappedItem.Graph.Name == graph.Name)
                {
                    this.ShowDataPointLabel(snappedItem.Graph, snappedItem.Data, snappedItem.Offset);
                    break;
                }
            }   
        }

        /// <summary>
        /// Handles the Loaded event of the control.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args containing instace data.</param>
        private void GraphHost_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this.bindingsCreated)
            {
                this.scrollBarBinding.Mode = BindingMode.TwoWay;
                this.scrollBarBinding.Source = this.doubleBindingHelper;
                this.horizontalScrollBar.SetBinding(ScrollBar.ValueProperty, this.scrollBarBinding);

                this.scrollBarMaxValueBinding.Mode = BindingMode.TwoWay;
                this.scrollBarMaxValueBinding.Source = this.scrollBarMaxValueBindingHelper;

                this.scrollBarViewportSizeBinding.Mode = BindingMode.TwoWay;
                this.scrollBarViewportSizeBinding.Source = this.scrollBarViewportSizeBindingHelper;

                this.horizontalScrollBar.SetBinding(ScrollBar.MaximumProperty, this.scrollBarMaxValueBinding);
                this.horizontalScrollBar.SetBinding(ScrollBar.ViewportSizeProperty, this.scrollBarViewportSizeBinding);

                if (this.topAxis.ScrollBar != null)
                {
                    if (!this.topAxis.ScrollBarBindingCreated)
                    {
                        this.topAxis.ScrollBar.SetBinding(ScrollBar.ValueProperty, this.scrollBarBinding);
                        this.topAxis.ScrollBarBindingCreated = true;
                    }

                    this.topAxis.ScrollBar.SetBinding(ScrollBar.MaximumProperty, this.scrollBarMaxValueBinding);
                    this.topAxis.ScrollBar.SetBinding(ScrollBar.ViewportSizeProperty, this.scrollBarViewportSizeBinding);
                }

                if (this.bottomAxis.ScrollBar != null && !this.bottomAxis.ScrollBarBindingCreated)
                {
                    this.bottomAxis.ScrollBar.SetBinding(ScrollBar.ValueProperty, this.scrollBarBinding);
                    this.bottomAxis.ScrollBarBindingCreated = true;
                }

                this.timeFrequencyBinding.Mode = BindingMode.TwoWay;
                this.timeFrequencyBinding.Source = this.timeFrequencyBindingHelper;
                this.SetBinding(GraphHost.VisibleWindowProperty, this.timeFrequencyBinding);

                this.bindingsCreated = true;
            }

            this.timeSelector.SelectedIndex = this.TimeFrequencySelectedIndex;            

            if (this.VisibleWindow != null)
            {
                this.LoadGraphs();

                this.visualFocusLine.VisualFocusLineMove += new RoutedEventHandler(this.OnVisualFocusLineMove);
                this.visualFocusLine.VisualFocusLineDragComplete += new MouseButtonEventHandler(this.OnVisualFocusLineDragComplete);

                // Now watch for changes in the Graphs collection
                this.graphs.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(this.Graphs_CollectionChanged);
                this.horizontalScrollBar.Value = this.horizontalScrollBar.Maximum;

                // If not set define the default states based on button values
                if (!this.defaultVisualFocusLine.HasValue)
                {
                    this.defaultVisualFocusLine = this.showVFL.IsChecked.Value;
                }

                if (!this.defaultGridLines.HasValue)
                {
                    this.defaultGridLines = this.showGridLines.IsChecked.Value ? GridLineVisibility.Both : GridLineVisibility.None;
                }

                if (!this.defaultInterpolationLines.HasValue)
                {
                    this.defaultInterpolationLines = this.showInterpolationLines.IsChecked.Value;
                }

                if (!this.defaultDataPointLabels.HasValue)
                {
                    this.defaultDataPointLabels = this.showDataPointLabels.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                }

                if (this.defaultDataPointLabels.Value == Visibility.Visible)
                {
                    this.defaultVisualFocusLine = false;
                    this.showVFL.IsChecked = false;
                }

                // Set the local values based on XAML defaults
                this.visualFocusLineVisibility = this.defaultVisualFocusLine.Value;
                this.gridLinesVisibility = this.defaultGridLines.Value;
                this.dataPointLabelsVisibility = this.defaultDataPointLabels.Value;
                this.interpolationLinesVisibility = this.defaultInterpolationLines.Value;

                // Ensure graph state is correctly defined based on button states
                this.ShowVisualFocusLine();
                this.SetGridLinesVisibility();
                this.SetInterpolationLinesVisibility();
                this.SetDataPointLabelsVisibility();
            }
        }

        /// <summary>
        /// Handles the change in the stack panel holding the graphs.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void ScrollStack_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.MatchAxisDefinitions();
        }

        /// <summary>
        /// Handles the visual focus line drag complete event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void OnVisualFocusLineDragComplete(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(this.topAxis);
            this.RenderVFL(point, true, true);
        }

        /// <summary>
        /// Handles the visual focus line move event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void OnVisualFocusLineMove(object sender, RoutedEventArgs e)
        {
            MouseEventArgs mouseArgs = e as MouseEventArgs;
            KeyEventArgs keyArgs = e as KeyEventArgs;
            if (mouseArgs != null)
            {                
                Point point = mouseArgs.GetPosition(this.topAxis);
                this.RenderVFL(point, false, true);
            }
            else if (keyArgs != null)
            {
                bool ctrlKeyPressed;
                bool shiftKeyPressed;

                KeyboardHelper.GetMetaKeyState(out ctrlKeyPressed, out shiftKeyPressed);

                int movement = ctrlKeyPressed ? 4 : 1;

                switch (keyArgs.Key)
                {
                    case Key.Left:

                        if (this.visualFocusLineVisibility == true)
                        {
                            this.RenderVFL(new Point(this.vflOffset - movement, 0), false, false);
                        }

                        break;
                    case Key.Right:
                        if (this.visualFocusLineVisibility == true)
                        {
                            this.RenderVFL(new Point(this.vflOffset + movement, 0), false, false);
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Handles the click event of the show visual focus line button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args containing instance data.</param>
        private void ShowVFL_Click(object sender, RoutedEventArgs e)
        {
            this.VisualFocusLineVisibility = !this.VisualFocusLineVisibility;
        }

        /// <summary>
        /// Handles the click event of the show visual focus line button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args containing instance data.</param>
        private void ShowInterpolationLines_Click(object sender, RoutedEventArgs e)
        {
            this.InterpolationLinesVisibility = !this.InterpolationLinesVisibility;
        }

        /// <summary>
        /// Handles the click event of the show visual focus line button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args containing instance data.</param>
        private void ShowGridLines_Click(object sender, RoutedEventArgs e)
        {
            if (this.GridLinesVisibility == GridLineVisibility.None)
            {
                this.GridLinesVisibility = this.defaultGridLines.Value == GridLineVisibility.None ? GridLineVisibility.Both : this.defaultGridLines.Value;
            }
            else
            {
                this.GridLinesVisibility = GridLineVisibility.None;
            }
        }

        /// <summary>
        /// Handles the click event of the show visual focus line button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args containing instance data.</param>
        private void ShowDataPointLabels_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataPointLabelsVisibility == Visibility.Visible)
            {
                this.DataPointLabelsVisibility = Visibility.Collapsed;
            }
            else
            {
                this.DataPointLabelsVisibility = Visibility.Visible;
            }                
        } 

        /// <summary>
        /// Handles the visibility changed event for a graph in data selctor.
        /// </summary>
        /// <param name="sender">Graph whose visibility got changed.</param>
        /// <param name="e">Events args containing instance data.</param>
        private void DataSelectorGraphVisibilityChanged(object sender, EventArgs e)
        {
            GraphSummary dataSelectorItem = sender as GraphSummary;
            
            if (dataSelectorItem.Show)
            {
                this.AddToView(dataSelectorItem.Name);

                foreach (VisualFocusSnappedItem snappedItem in this.snappedDataMarkers)
                {
                    if (snappedItem.Graph.Name == dataSelectorItem.Name)
                    {
                        this.ShowDataPointLabel(snappedItem.Graph, snappedItem.Data, snappedItem.Offset);
                        break;
                    }
                }   
            }
            else
            {
                this.RemoveFromView(dataSelectorItem.Name);
            }
        }        

        /// <summary>
        /// Handles the scroll event on the horizontal scrollbar.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args containing instance data.</param>
        private void HorizontalScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {           
            this.HideVFL();
        }

        /// <summary>
        /// Handles the scroll event of the horizontal scrollbar.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args containing instance data.</param>
        private void HorizontalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.GetViewportDate();
        }   

        /// <summary>
        /// Handles the size changed event of the root element.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args containing instance data.</param>
        private void GraphHost_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.PreviousSize.Width != e.NewSize.Width)
            {
                this.HideVFL();
            }
        }

        /// <summary>
        /// Handles the click event of reset button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            this.Reset();
        }
        
        /// <summary>
        /// Handles the got focus event of scroll viewer.
        /// </summary>
        /// <param name="sender">Sender of the Event.</param>
        /// <param name="e">Routed Event Arguements.</param>
        private void ScrollViewer_GotFocus(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource == this.scrollViewer)
            {
                GraphHost.ShowFocusVisual(this.focusVisual, true);
            }
            else
            {
#if SILVERLIGHT
                TimeGraphBase graph = e.OriginalSource as TimeGraphBase;
                if (graph != null)
                {
                    Rect itemBounds = LayoutInformation.GetLayoutSlot(graph);

                    if (!double.IsNaN(itemBounds.Top) && !double.IsNaN(itemBounds.Bottom))
                    {
                        double scrollOffset = (double)this.scrollViewer.GetValue(ScrollViewer.VerticalOffsetProperty);
                        double newScrollOffset = double.NaN;

                       if (itemBounds.Top < scrollOffset)
                        {
                            // Top not visible
                            newScrollOffset = itemBounds.Top;
                        }
                        else if (itemBounds.Bottom > scrollOffset + this.scrollViewer.ViewportHeight)
                        {
                            // Bottom not visible
                            newScrollOffset = itemBounds.Bottom - this.scrollViewer.ViewportHeight;
                        }

                        if (!double.IsNaN(newScrollOffset))
                        {
                            this.scrollViewer.ScrollToVerticalOffset(newScrollOffset);
                        }
                    }
                }
#endif
            }
        }

        /// <summary>
        /// Handles the lost focus event of scroll viewer.
        /// </summary>
        /// <param name="sender">Sender of the Event.</param>
        /// <param name="e">Routed Event Arguements.</param>
        private void ScrollViewer_LostFocus(object sender, RoutedEventArgs e)
        {
            GraphHost.ShowFocusVisual(this.focusVisual, false);
        }

        /// <summary>
        /// Handles the got focus event of horizontal scroll bar.
        /// </summary>
        /// <param name="sender">Sender of the Event.</param>
        /// <param name="e">Routed Event Arguements.</param>
        private void HorizontalScrollBar_GotFocus(object sender, RoutedEventArgs e)
        {
            GraphHost.ShowFocusVisual(this.horizontalScrollbarFocusVisual, true);                     
        }

        /// <summary>
        /// Handles the lost focus event of horizontal scroll bar.
        /// </summary>
        /// <param name="sender">Sender of the Event.</param>
        /// <param name="e">Routed Event Arguements.</param>
        private void HorizontalScrollBar_LostFocus(object sender, RoutedEventArgs e)
        {
                GraphHost.ShowFocusVisual(this.horizontalScrollbarFocusVisual, false);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Ensures the top and bottom axis definitions are aligned to the graphs.
        /// </summary>
        private void MatchAxisDefinitions()
        {
            this.topAxisContainer.Width = this.scrollStack.ActualWidth;
            this.bottomAxisContainer.Width = this.scrollStack.ActualWidth;

#if SILVERLIGHT
            // due to silverlight layout rounding need to ensure that the panels are exactly aligned for correct plotting
            Point zero, transform;
            MatrixTransform globalTransform;

            if (!this.xaxisAlignment.HasValue && this.topAxis.LayoutRoot != null)
            {
                zero = new Point(0, 0);
                globalTransform = this.topAxis.LayoutRoot.TransformToVisual(Application.Current.RootVisual) as MatrixTransform;
                transform = globalTransform.Matrix.Transform(zero);
                this.xaxisAlignment = transform.X;
            }

            if (this.xaxisAlignment.HasValue)
            {
                foreach (TimeGraphBase graph in this.graphs)
                {
                    // ensure graphs regions are correctly left aligned
                    FrameworkElement graphLayout = graph.LayoutRoot;

                    if (graphLayout != null)
                    {
                        TranslateTransform savedTransform = graphLayout.RenderTransform as TranslateTransform;
                        if (savedTransform != null)
                        {
                            graphLayout.RenderTransform = null;
                        }

                        zero = new Point(0, 0);
                        globalTransform = graphLayout.TransformToVisual(Application.Current.RootVisual) as MatrixTransform;
                        transform = globalTransform.Matrix.Transform(zero);
                        double deltaX = this.xaxisAlignment.Value - transform.X;

                        // Set new transform
                        if (deltaX != 0)
                        {
                            if (savedTransform == null)
                            {
                                savedTransform = new TranslateTransform();
                            }

                            graphLayout.RenderTransform = savedTransform;
                            savedTransform.X = deltaX;
                        }
                    }
                }
            }
#endif
        }

        /// <summary>
        /// Refreshes the graph axis data.
        /// </summary>
        private void RefreshInternal()
        {
            if (this.VisibleWindow != null)
            {
                this.HideVFL();
                this.axisStartDateTime = null;
                this.axisEndDateTime = null;
                this.GetAxisDates();
                this.SetGraphDates();

                if (this.topAxis != null && this.topAxis.ScrollBar != null)
                {
                    this.horizontalScrollBar.SmallChange = this.topAxis.ScrollBar.SmallChange;
                    this.horizontalScrollBar.LargeChange = this.topAxis.ScrollBar.LargeChange;
                }
            }
        }

        /// <summary>
        /// Hides the visual focus line.
        /// </summary>
        private void HideVFL()
        {
            this.vflOffset = 0;
            this.visualFocusLineVisibility = false;
            this.ShowVisualFocusLine();
        }

        /// <summary>
        /// Asserts for the template elements.
        /// </summary>
        private void LoadTemplateParts()
        {
            this.stackPanel = this.GetTemplateChild<StackPanel>(RootPanelElementName, true);
            this.horizontalScrollBar = this.GetTemplateChild<ScrollBar>(ScrollBarElementName, true);
            this.topAxis = this.GetTemplateChild<TimeGraphBase>(TopAxisElementName, true);
            this.bottomAxis = this.GetTemplateChild<TimeGraphBase>(BottomAxisElementName, true);
            this.timeSelector = this.GetTemplateChild<System.Windows.Controls.ComboBox>(TimeSelectorElementName, true);
            this.layoutRoot = this.GetTemplateChild<FrameworkElement>(LayoutRootElementName, true);
            this.graphArea = this.GetTemplateChild<Panel>(GraphAreaElementName, true);
            this.visualFocusLine = this.GetTemplateChild<VisualFocusLine>(VisualFocusLineElementName, true);
            
            this.scrollViewer = this.GetTemplateChild<ScrollViewer>(ScrollViewerElementName, true);
            this.showVFL = this.GetTemplateChild<ToggleButton>(ShowVFLButtonName, true);
            this.showGridLines = this.GetTemplateChild<ToggleButton>(ShowGridLinesButtonName, true);
            this.showInterpolationLines = this.GetTemplateChild<ToggleButton>(ShowInterpolationLinesButtonName, true);
            this.showDataPointLabels = this.GetTemplateChild<ToggleButton>(ShowDataPointLabelsButtonName, true);
            this.dataSelector = this.GetTemplateChild<DataSelector>(DataSelectorControlElementName, true);
            this.reset = this.GetTemplateChild<Button>(ResetButtonElementName, true);

            this.timeFrequencyBindingHelper = StyleParser.FindResource<TimeFrequencyBindingHelper>(this.layoutRoot, TimeFrequencyBindingHelperResourceKey, true);
            this.defaultTopAxisStyle = StyleParser.FindResource<Style>(this.layoutRoot, TopAxisDefaultStyleResourceKey, false);
            this.defaultBottomAxisStyle = StyleParser.FindResource<Style>(this.layoutRoot, BottomAxisDefaultStyleResourceKey, false);

            this.focusVisual = this.GetTemplateChild<UIElement>(FocusVisualElementName, false);
            this.horizontalScrollbarFocusVisual = this.GetTemplateChild<UIElement>(HorizontalScrollBarFocusVisualElementName, false);

            this.scrollStack = this.scrollViewer.Content as StackPanel;
            this.scrollStack.SizeChanged += new SizeChangedEventHandler(this.ScrollStack_SizeChanged);

            this.topAxisContainer = this.GetTemplateChild<FrameworkElement>(TopAxisContainerElementName, true);
            this.bottomAxisContainer = this.GetTemplateChild<FrameworkElement>(BottomAxisContainerElementName, true);
        }      

        /// <summary>
        /// Loads all the graphs into the stack panel.
        /// </summary>
        private void LoadGraphs()
        {
            this.stackPanel.Children.Clear();

            foreach (TimeGraphBase graph in this.graphs)
            {
                this.AddGraphToStackPanel(graph);
            }

            foreach (TimeGraphBase axis in this.axes)
            {
                axis.Initialized += new EventHandler<RoutedEventArgs>(this.Graph_Initialized);
            }
        }

        /// <summary>
        /// Handles the Scale to fit click event of the graph.
        /// </summary>
        /// <param name="sender">Graph on which scale to fit was clicked.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void Graph_ScaleToFitClick(object sender, RoutedEventArgs e)
        {
            foreach (VisualFocusSnappedItem snappedItem in this.snappedDataMarkers)
            {
                if (snappedItem.Graph == sender)
                {
                    this.ShowDataPointLabel(snappedItem.Graph, snappedItem.Data, snappedItem.Offset);                    
                    break;
                }
            }
        }
        
        /// <summary>
        /// Adds graphs to the host.
        /// </summary>
        /// <param name="graphs">Graphs to add.</param>
        private void AddGraphs(System.Collections.IList graphs)
        {
            bool newGraph = false;

            foreach (TimeGraphBase graph in graphs)
            {
                this.AddGraphToStackPanel(graph);

                // see if this is a new graph after the panels have been initialized
                if (!this.dataSelectorGraphs.Contains(graph.GraphSummary))
                {
                    newGraph = true;
                    graph.Visibility = Visibility.Visible;
                    graph.GraphSummary.Show = true;
                    this.AddGraphToSelector(graph);

                    // define some base properties before adding the graph
                    graph.ShowDataPointLabels = this.dataPointLabelsVisibility;
                    graph.ShowGridLines = this.gridLinesVisibility;

                    ISupportInterpolation interpolationGraph = graph as ISupportInterpolation;
                    if (interpolationGraph != null)
                    {
                        interpolationGraph.ShowInterpolationLines = this.interpolationLinesVisibility;
                    }
                }
            }

            if (newGraph)
            {
                this.RefreshInternal();
            }
        }

        /// <summary>
        /// Add a graph definition to a stck panel.
        /// </summary>
        /// <param name="graph">Graph to add to the panel.</param>
        private void AddGraphToStackPanel(TimeGraphBase graph)
        {
            this.stackPanel.Children.Add(graph);

            graph.Initialized += new EventHandler<RoutedEventArgs>(this.Graph_Initialized);
            graph.ScaleToFitClick += new RoutedEventHandler(this.Graph_ScaleToFitClick);
        }        

        /// <summary>
        /// Removes the specified graphs from host.
        /// </summary>
        /// <param name="graphs">Graphs to remove.</param>
        private void RemoveGraphs(System.Collections.IList graphs)
        {
            bool graphRemoved = false;

            foreach (TimeGraphBase graph in graphs)
            {
                GraphSummary graphSummary = graph.GraphSummary;

                if (this.dataSelectorGraphs.Contains(graphSummary))
                {
                    this.dataSelectorGraphs.Remove(graphSummary);
                }

                if (this.stackPanel.Children.Contains(graph))
                {
                    graphRemoved = true;
                    this.stackPanel.Children.Remove(graph);
                }
            }

            if (graphRemoved)
            {
                this.RefreshInternal();
            }
        }

        /// <summary>
        /// Sets the graph dates.
        /// </summary>
        private void SetGraphDates()
        {
            bool callRefresh = false;

            foreach (TimeGraphBase graph in this.graphs)
            {
                if (graph.AxisStartDate != this.axisStartDateTime || graph.AxisEndDate != this.axisEndDateTime || graph.VisibleWindow.Ticks != this.VisibleWindow.Ticks)
                {
                    callRefresh = true;
                    break;
                }
            }

            if (callRefresh)
            {
                foreach (TimeGraphBase graph in this.graphs)
                {
                        graph.AxisStartDate = this.axisStartDateTime.GetValueOrDefault();
                        graph.AxisEndDate = this.axisEndDateTime.GetValueOrDefault();
                        graph.DetermineAxisDates = false;
                        graph.VisibleWindow = this.VisibleWindow;
                        graph.Refresh(true);
                }

                foreach (TimeGraphBase axis in this.axes)
                {
                        axis.AxisStartDate = this.axisStartDateTime.GetValueOrDefault();
                        axis.AxisEndDate = this.axisEndDateTime.GetValueOrDefault();
                        axis.DetermineAxisDates = false;
                        axis.VisibleWindow = this.VisibleWindow;
                        axis.Refresh(true);
                }
            }
        }

        /// <summary>
        /// Gets the start and end dates of the graph.
        /// </summary>        
        private void GetAxisDates()
        {
            DateTime? graphAxisStartDate = null;
            DateTime? graphAxisEndDate = null;
            TimeFrequency frequency = AxisHelper.GetAxisDesign(this.VisibleWindow.Ticks);

            foreach (TimeGraphBase graph in this.graphs)
            {
                if (graph.DataContext != null)
                {
                    DateTime dataEndDate = DataHelper.GetLastDateTime(graph.DataContext);
                    DateTime dataStartDate = DataHelper.GetFirstDateTime(graph.DataContext);

                    if (!graphAxisStartDate.HasValue || graphAxisStartDate.GetValueOrDefault() > dataStartDate)
                    {
                        graphAxisStartDate = dataStartDate;
                    }

                    if (graphAxisEndDate == DateTime.MinValue || graphAxisEndDate.GetValueOrDefault() < dataEndDate)
                    {
                        graphAxisEndDate = dataEndDate;
                    }
                }
            }

            if (graphAxisStartDate.HasValue && graphAxisEndDate.HasValue)
            {
                DateTime graphHostStartDate = AxisHelper.GetAxisStartDate(graphAxisStartDate.GetValueOrDefault(), frequency);
                DateTime graphHostEndDate = AxisHelper.GetAxisEndDate(graphAxisEndDate.GetValueOrDefault(), frequency);

                AxisHelper.AdjustAxisStartDate(ref graphHostStartDate, ref graphHostEndDate, frequency);

                if (!this.axisStartDateTime.HasValue || this.axisStartDateTime.GetValueOrDefault() > graphHostStartDate)
                {
                    this.axisStartDateTime = graphHostStartDate;
                }

                if (!this.axisEndDateTime.HasValue || this.axisEndDateTime.GetValueOrDefault() < graphHostEndDate)
                {
                    this.axisEndDateTime = graphHostEndDate;
                }
            }
        }        
        
        /// <summary>
        /// Gets the graphs for data selector control.
        /// </summary>
        private void GetDataSelectorGraphs()
        {
            foreach (TimeGraphBase graph in this.graphs)
            {
                this.AddGraphToSelector(graph);
            }
        }

        /// <summary>
        /// Adds a graph into the Data Selector.
        /// </summary>
        /// <param name="graph">Graph to be added.</param>
        private void AddGraphToSelector(TimeGraphBase graph)
        {
            GraphSummary graphSummary = graph.GraphSummary;
            graphSummary.DataSelectorGraphVisibilityChanged += new EventHandler<EventArgs>(this.DataSelectorGraphVisibilityChanged);

            string graphName = graph.GetValue(FrameworkElement.NameProperty) as string;
            if (string.IsNullOrEmpty(graphName))
            {
                ++this.graphCounter;
                graphName = string.Format(CultureInfo.CurrentCulture, "Graph{0}", this.graphCounter);
                graph.SetValue(FrameworkElement.NameProperty, graphName);
            }

            graphSummary.Name = graphName;
            this.dataSelectorGraphs.Add(graphSummary);
        }

        /// <summary>
        /// Adds a graph with the given name into view.
        /// </summary>
        /// <param name="graphName">Name of the graph that needs to be added.</param>
        private void AddToView(string graphName)
        {
            foreach (UIElement element in this.graphs)
            {
                if (string.Compare(graphName, element.GetValue(FrameworkElement.NameProperty) as string, StringComparison.CurrentCulture) == 0)
                {
                    this.AddToView(element);
                    break;
                }
            }
        }

        /// <summary>
        /// Adds a graph into view.
        /// </summary>
        /// <param name="element">Graph that needs to be added.</param>
        private void AddToView(UIElement element)
        {
            if (this.stackPanel.Children.Contains(element))
            {
                element.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Removes a graph with the specified name from view.
        /// </summary>
        /// <param name="graphName">Name of the graph that needs to be removed.</param>
        private void RemoveFromView(string graphName)
        {
            foreach (TimeGraphBase timeGraph in this.graphs)
            {
                if (string.Compare(graphName, timeGraph.GetValue(FrameworkElement.NameProperty) as string, StringComparison.CurrentCulture) == 0)
                {
                    this.RemoveFromView(timeGraph);
                    break;
                }
            }
        }

        /// <summary>
        /// Removes a graph from view.
        /// </summary>
        /// <param name="timeGraph">Graph that needs to be removed.</param>
        private void RemoveFromView(TimeGraphBase timeGraph)
        {
            if (this.stackPanel.Children.Contains(timeGraph))
            {
                timeGraph.Visibility = Visibility.Collapsed;
                timeGraph.GraphSummary.VisualFocusLineSelectedObject = null;
            }
        }

        /// <summary>
        /// Renders the visual focus line at the point where mouse left button was released.
        /// </summary>
        /// <param name="axisOffset">X offset where mouse button was released.</param>
        /// <param name="snapToDataPoint">Boolean indicating whether the VFL should be snapped to nearest datapoint.</param>
        /// <param name="vflMovedUsingMouse">Boolean indicating whether the VFL is moved using mouse or keyboard.</param>
        /// <returns>DateTime at which VFL is being drawn.</returns>
        /// <remarks>If VFL snaps data point(s), then VFL is moved to the location where it snapped.
        /// If VFL is moved using keyboard snapping will not be done.
        /// Returns the datetime for the points snapped.</remarks>
        private DateTime SnapData(double axisOffset, bool snapToDataPoint, bool vflMovedUsingMouse)
        {
            axisOffset -= this.graphPadding;
            DateTime timeToSnap = this.topAxis.GetDateForX(axisOffset);
            double vflOffset = -1;
            DateTime vflDate = timeToSnap;
            bool foundDataPointToSnap = false;
            double proximity = vflMovedUsingMouse ? this.VisualFocusLineProximity : this.visualFocusLine.VisualFocusLineThickness;
            double prevDistanceFromVFL = 0;

            // Loop through all the visible graphs and find the nearest data point
            foreach (TimeGraphBase timeGraph in this.graphs)
            {
                double offset = -1;
                timeGraph.GraphSummary.VisualFocusLineSelectedObject = null;
                if (timeGraph.GraphSummary.Show)
                {
                    System.Collections.IEnumerable enumerable = timeGraph.GetFilteredData(timeToSnap, timeToSnap);
                    foreach (object data in enumerable)
                    {
                        DateTime dateTime = DataHelper.GetGraphPointDateTime(data);
                        offset = timeGraph.GetXForDateInView(dateTime);
                        double distanceFromVFL = axisOffset > offset ? axisOffset - offset - this.visualFocusLine.VisualFocusLineThickness : offset - axisOffset - this.visualFocusLine.VisualFocusLineThickness;
                        if (distanceFromVFL <= proximity)
                        {
                            if (GraphHost.IsDataMarkerOverlapping(timeGraph, data, offset) == false)
                            {
                                if (foundDataPointToSnap == false || distanceFromVFL <= prevDistanceFromVFL)
                                {
                                    foundDataPointToSnap = true;
                                    vflOffset = offset;
                                    vflDate = dateTime;
                                    prevDistanceFromVFL = distanceFromVFL;
                                }
                            }
                        }
                    }
                }
            }

            if (foundDataPointToSnap)
            {
                // Snap the data points for all the graphs
                foreach (TimeGraphBase timeGraph in this.graphs)
                {
                    System.Collections.IEnumerable enumerable = timeGraph.GetFilteredData(timeToSnap, timeToSnap);
                    foreach (object data in enumerable)
                    {
                        DateTime dateTime = DataHelper.GetGraphPointDateTime(data);
                        if (dateTime == vflDate)
                        {
                            if (GraphHost.IsDataMarkerOverlapping(timeGraph, data, vflOffset) == false)
                            {
                                this.ShowDataPointLabel(timeGraph, data, vflOffset);
                                break;
                            }
                        }
                    }
                }

                if (snapToDataPoint)
                {
                    // Move the VFL to the point snapped (Magnetic behavior)
                    this.visualFocusLine.XOffset = vflOffset + this.graphMargin + this.graphPadding;
                }
            }            

            return vflDate;
        }        

        /// <summary>
        /// Shows the data point label.
        /// </summary>
        /// <param name="timeGraph">Graph in which the data point marker resides.</param>
        /// <param name="data">Actual data for which data point label needs to be shown.</param>
        /// <param name="offset">Offset of the data in the graph.</param>        
        private void ShowDataPointLabel(TimeGraphBase timeGraph, object data, double offset)
        {
            FrameworkElement marker = timeGraph.GetDataMarker(data, offset);
            if (marker != null)
            {
                timeGraph.OnDataMarkerMouseEnter(marker, false);

                VisualFocusSnappedItem snappedItem = new VisualFocusSnappedItem(marker, timeGraph, data, offset);
                this.snappedDataMarkers.Add(snappedItem);

                if (timeGraph.GraphSummary.Show)
                {
                    if (timeGraph.GraphSummary.VisualFocusLineSelectedObject == null || timeGraph.GraphSummary.VisualFocusLineSelectedObject != data)
                    {
                        timeGraph.GraphSummary.VisualFocusLineSelectedObject = data;
                    }
                }
            }
        }

        /// <summary>
        /// Hides the data point labels that were snapped by visual focus line.
        /// </summary>
        private void HidePreviousSnappedLabels()
        {
            foreach (VisualFocusSnappedItem snappedElement in this.snappedDataMarkers)
            {
                snappedElement.Graph.OnDataMarkerMouseLeave(snappedElement.SnappedElement);                    
            }

            this.snappedDataMarkers.Clear();
        }        

        /// <summary>
        /// Shows or hides the visual focus line element.
        /// </summary>
        /// <remarks>When shown it will be placed at a default location.</remarks>
        private void ShowVisualFocusLine()
        {
            if (this.visualFocusLine != null)
            {
                if (this.visualFocusLineVisibility)
                {
                    this.visualFocusLine.Visibility = Visibility.Visible;
                    this.RenderVFL();
                    this.ShowDataPointsLabelsOnHover(false);
                    this.visualFocusLine.Focus();
                }
                else
                {
                    if (this.visualFocusLine.Visibility == Visibility.Visible)
                    {
                        this.visualFocusLine.Visibility = Visibility.Collapsed;
                        this.showVFL.IsChecked = false;
                        this.ShowDataPointsLabelsOnHover(true);
                        this.HidePreviousSnappedLabels();
                        this.ClearDataSelectorValues();
                    }
                }
            }
        }

        /// <summary>
        /// Renders VFL at the specified point.
        /// </summary>
        /// <param name="point">Point at which VFL needs to be rendered.</param>
        /// <param name="snapToDataPoint">Boolean indicating whether the VFL should snap to the nearest data point in tolerance range.</param>
        /// <param name="vflMovedUsingMouse">Boolean indicating whether the VFL is moved using mouse.</param>
        private void RenderVFL(Point point, bool snapToDataPoint, bool vflMovedUsingMouse)
        {
            double currentOffset = point.X;
            if (!vflMovedUsingMouse)
            {
                // If VFL moved using Keyboard, then Point is with respect to scrollviewer.
                // Hence translating to graph by removing the padding.
                // Fix for PS#12340
                currentOffset = point.X - this.graphPadding;
            }

            // If mouse moves beyond the plot area, do not move the VFL.
            if (this.visualFocusLineVisibility && currentOffset >= this.graphPadding && currentOffset <= this.vflMaxOffset)
            {
                this.HidePreviousSnappedLabels();
                
                // When VFL is moved with mouse, point is with respect to top axis 
                // whereas when moved with keyboard, points are with respect to scrollviewer
                if (vflMovedUsingMouse)
                {
                    this.visualFocusLine.XOffset = point.X + this.graphPadding;
                    this.visualFocusLine.DateTime = this.SnapData(point.X, snapToDataPoint, vflMovedUsingMouse);
                }
                else
                {
                    this.visualFocusLine.XOffset = point.X;
                    this.visualFocusLine.DateTime = this.SnapData(point.X - this.graphPadding, snapToDataPoint, vflMovedUsingMouse);
                }
                
                this.vflOffset = this.visualFocusLine.XOffset;
            }
        }

        /// <summary>
        /// Renders the VFL at the middle of the axis.
        /// </summary>
        private void RenderVFL()
        {
            if (this.topAxis != null)
            {
                if (this.vflOffset == 0)
                {
                    GeneralTransform transform = this.topAxis.TransformToVisual(this.scrollViewer);
                    Point transformedPoint = transform.Transform(new Point());
                    this.graphPadding = transformedPoint.X; 
                    this.graphMargin = this.topAxis.Margin.Left + this.scrollViewer.Padding.Left;

                    this.vflOffset = this.topAxis.GetXForDateInView(this.topAxis.AxisStartDate.AddTicks(this.topAxis.VisibleWindow.Ticks / 2));
                    this.vflOffset += this.horizontalScrollBar.Value + this.graphPadding;

                    this.vflOffset += this.graphPadding;

                    this.vflMaxOffset = this.topAxis.GetXForDateInView(this.topAxis.AxisStartDate.AddTicks(this.topAxis.VisibleWindow.Ticks));
                    this.vflMaxOffset += this.horizontalScrollBar.Value + this.graphPadding;
                }

                this.RenderVFL(new Point(this.vflOffset, 0), false, false);
            }
        }

        /// <summary>
        /// Gets the DateTime visible at the center of the viewport.
        /// </summary>
        /// <param name="visibleWindow">Visible window of the viewport used to determine the date.</param>
        /// <returns>DateTime visible at the center of the viewport.</returns>
        private DateTime GetDateInView(TimeFrequency visibleWindow)
        {
            DateTime dateTime = this.topAxis.AxisEndDate;
            double offset = 0;

            offset = this.topAxis.GetXForDateInView(this.topAxis.AxisStartDate.AddTicks(visibleWindow.Ticks / 2));
            offset += this.horizontalScrollBar.Value;

            if (offset > 0)
            {
                dateTime = this.topAxis.GetDateForX(offset);
            }

            return dateTime;
        }

        /// <summary>
        /// Shows or hides the grid lines.
        /// </summary>
        private void SetGridLinesVisibility()
        {
            foreach (TimeGraphBase graph in this.graphs)
            {
                graph.ShowGridLines = this.gridLinesVisibility;
            }
        }

        /// <summary>
        /// Shows or hides the interpolation lines.
        /// </summary>
        private void SetInterpolationLinesVisibility()
        {
            foreach (TimeGraphBase graph in this.graphs)
            {
                ISupportInterpolation interpolationGraph = graph as ISupportInterpolation;
                if (interpolationGraph != null)
                {
                    interpolationGraph.ShowInterpolationLines = this.interpolationLinesVisibility;
                }
            }
        }

        /// <summary>
        /// Shows or hides the data point labels.
        /// </summary>
        private void SetDataPointLabelsVisibility()
        {
            foreach (TimeGraphBase graph in this.graphs)
            {
                graph.ShowDataPointLabels = this.dataPointLabelsVisibility;
            }
        }

        /// <summary>
        /// Resets the graph to initial state.
        /// </summary>
        private void Reset()
        {
            this.horizontalScrollBar.Value = this.horizontalScrollBar.Maximum;
            this.scrollViewer.ScrollToVerticalOffset(0);
            this.scrollPosition = 1;

            // Reset VFL to default State
            this.visualFocusLineVisibility = this.defaultVisualFocusLine.Value;
            this.ShowVisualFocusLine();
            this.HidePreviousSnappedLabels();

            // Reset member variables.
            this.dataPointLabelsVisibility = this.defaultDataPointLabels.Value;
            this.gridLinesVisibility = this.defaultGridLines.Value;
            this.interpolationLinesVisibility = this.defaultInterpolationLines.Value;            
            
            // Set button status
            this.showVFL.IsChecked = this.defaultVisualFocusLine.Value;
            this.showInterpolationLines.IsChecked = this.defaultInterpolationLines.Value;
            this.showGridLines.IsChecked = this.defaultGridLines.Value == GridLineVisibility.None ? false : true;
            this.showDataPointLabels.IsChecked = this.defaultDataPointLabels.Value == Visibility.Visible ? true : false;

            if (this.timeSelector.SelectedIndex != this.TimeFrequencySelectedIndex)
            {
                this.timeSelector.SelectedIndex = this.TimeFrequencySelectedIndex;
            }

            this.ResetGraphs();
        }

        /// <summary>
        /// Resets the graphs.
        /// </summary>
        private void ResetGraphs()
        {
            bool reordered = false;
            for (int i = 0; i < this.graphs.Count; i++)
            {
                if (!this.graphs[i].Equals(this.stackPanel.Children[i]))
                {
                    reordered = true;
                    break;
                }
            }

            if (reordered)
            {
                this.dataSelectorGraphs.Clear();
                this.GetDataSelectorGraphs();
                this.LoadGraphs();
                this.ShowAllGraphs();
            }

            foreach (TimeGraphBase graph in this.graphs)
            {
                graph.Reset();
            }

            foreach (TimeGraphBase axis in this.axes)
            {
                axis.Reset();
            }
        }

        /// <summary>
        /// Shows all the graphs.
        /// </summary>
        private void ShowAllGraphs()
        {
            foreach (GraphSummary graphSummary in this.dataSelectorGraphs)
            {
                if (graphSummary.Show == false)
                {
                    graphSummary.Show = true;
                }
            }
        }

        /// <summary>
        /// Clears the values in the data selector.
        /// </summary>
        private void ClearDataSelectorValues()
        {
            foreach (GraphSummary graphSummary in this.dataSelectorGraphs)
            {
                graphSummary.VisualFocusLineSelectedObject = null;
            }
        }

        /// <summary>
        /// Sets the property for the graph to show or hide data point labels on hover.
        /// </summary>
        /// <param name="showLabelsOnHover">Boolean indication whether to show or hide data point labels.</param>
        private void ShowDataPointsLabelsOnHover(bool showLabelsOnHover)
        {
            foreach (TimeGraphBase timeGraph in this.graphs)
            {
                timeGraph.ShowDataPointLabelsOnHover = showLabelsOnHover;

                if (showLabelsOnHover)
                {
                    timeGraph.GraphSummary.VisualFocusLineVisibility = Visibility.Collapsed;
                }
                else
                {
                    timeGraph.GraphSummary.VisualFocusLineVisibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Shows the labels for all the data points snapped by VFL.
        /// </summary>
        private void ShowAllSnappedDataPointLabels()
        {
            VisualFocusSnappedItem[] snappedMarkers = new VisualFocusSnappedItem[this.snappedDataMarkers.Count];
            this.snappedDataMarkers.CopyTo(snappedMarkers);
                
            foreach (VisualFocusSnappedItem snappedItem in snappedMarkers)
            {
                this.ShowDataPointLabel(snappedItem.Graph, snappedItem.Data, snappedItem.Offset);
            }
        }

        /// <summary>
        /// Gets the viewport date.
        /// </summary>
        /// <remarks>Sets the scroll position variable to a value to determine the scrollbar position.</remarks>
        private void GetViewportDate()
        {
            if ((int)this.horizontalScrollBar.Value == (int)this.horizontalScrollBar.Maximum)
            {
                this.scrollPosition = 1;
            }
            else if ((int)this.horizontalScrollBar.Value == (int)this.horizontalScrollBar.Minimum)
            {
                this.scrollPosition = -1;
            }
            else
            {
                this.viewPortDateTime = this.GetDateInView(this.VisibleWindow as TimeFrequency);
                this.scrollPosition = 0;
            }
        }

        /// <summary>
        /// Scrolls to the previous scrollbar position or viewport date.
        /// </summary>
        private void ScrollToViewportDate()
        {
            if (this.scrollPosition == 1)
            {
                this.horizontalScrollBar.Value = this.horizontalScrollBar.Maximum;
            }
            else if (this.scrollPosition == -1)
            {
                this.horizontalScrollBar.Value = this.horizontalScrollBar.Minimum;
            }
            else
            {
                if (this.viewPortDateTime.HasValue)
                {
                    this.topAxis.ScrollToDate(this.viewPortDateTime.Value);
                }
            }
        }
        #endregion
    }
}
