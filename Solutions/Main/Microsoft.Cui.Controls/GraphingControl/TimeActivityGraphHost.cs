//-----------------------------------------------------------------------
// <copyright file="TimeActivityGraphHost.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>07-Oct-2009</date>
// <summary>TimeActivity graph host class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;

    /// <summary>
    /// TimeActivityGraph Host control.
    /// </summary>
    [TemplatePart(Name = LayoutRootElementName, Type = typeof(Grid))]
    [TemplatePart(Name = RootPanelElementName, Type = typeof(Grid))]
    [TemplatePart(Name = TopAxisElementName, Type = typeof(TimeGraphBase))]
    [TemplatePart(Name = BottomAxisElementName, Type = typeof(TimeGraphBase))]
    [TemplatePart(Name = ScrollbarElementName, Type = typeof(ScrollBar))]
    [TemplatePart(Name = VisualFocusLineElementName, Type = typeof(VisualFocusLine))]
    [TemplatePart(Name = ResetElementName, Type = typeof(Button))]
    [TemplatePart(Name = RefreshElementName, Type = typeof(Button))]
    [TemplatePart(Name = JumpToNowElementName, Type = typeof(Button))]    
    [TemplatePart(Name = LoadAnimationStoryBoardName, Type = typeof(Storyboard))]
    [TemplatePart(Name = TimeSelectorElementName, Type = typeof(ComboBox))]
    public class TimeActivityGraphHost : Control
    {
        #region Dependency Properties
        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.TimeActivityGraphHost.SectionName" /> attached property.
        /// </summary>
        public static readonly DependencyProperty SectionNameProperty =
            DependencyProperty.RegisterAttached("SectionName", typeof(string), typeof(TimeActivityGraphHost), new PropertyMetadata(string.Empty));
        
        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.TimeActivityGraphHost.Graphs" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty GraphsProperty =
            DependencyProperty.Register("Graphs", typeof(ObservableCollection<TimeGraphBase>), typeof(TimeActivityGraphHost), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.TimeActivityGraphHost.VisibleWindow" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty VisibleWindowProperty =
            DependencyProperty.Register("VisibleWindow", typeof(TimeFrequency), typeof(TimeActivityGraphHost), new PropertyMetadata(null, new PropertyChangedCallback(OnVisibleWindowChanged)));

        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.TimeActivityGraphHost.TimeFrequencySelectedIndex" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TimeFrequencySelectedIndexProperty =
            DependencyProperty.Register("TimeFrequencySelectedIndex", typeof(int), typeof(TimeActivityGraphHost), new PropertyMetadata(9, new PropertyChangedCallback(OnTimeFrequencySelectedIndexChanged)));

        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.TimeActivityGraphHost.AxisStartDate" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AxisStartDateProperty =
            DependencyProperty.Register("AxisStartDate", typeof(DateTime?), typeof(TimeActivityGraphHost), new PropertyMetadata(null, new PropertyChangedCallback(OnAxisDatesChanged)));

        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.TimeActivityGraphHost.AxisEndDate" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty AxisEndDateProperty =
            DependencyProperty.Register("AxisEndDate", typeof(DateTime?), typeof(TimeActivityGraphHost), new PropertyMetadata(null, new PropertyChangedCallback(OnAxisDatesChanged)));
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
        
        #region Template Part Names

        /// <summary>
        /// Template part element name for the root element.
        /// </summary>
        private const string LayoutRootElementName = "ELEMENT_LayoutRoot";

        /// <summary>
        /// Template part element name for root panel.
        /// </summary>
        private const string RootPanelElementName = "ELEMENT_RootPanel";

        /// <summary>
        /// Template part element name for top axis.
        /// </summary>
        private const string TopAxisElementName = "ELEMENT_TopAxis";

        /// <summary>
        /// Template part element name for bottom axis.
        /// </summary>
        private const string BottomAxisElementName = "ELEMENT_BottomAxis";

        /// <summary>
        /// Template part element name for horizontal scrollbar.
        /// </summary>
        private const string ScrollbarElementName = "ELEMENT_ScrollBar";

        /// <summary>
        /// Template part element name for visual focus line.
        /// </summary>
        private const string VisualFocusLineElementName = "ELEMENT_VisualFocusLine";

        /// <summary>
        /// Template part name for the reset button.
        /// </summary>
        private const string ResetElementName = "ELEMENT_Reset";

        /// <summary>
        /// Template part name for the refresh button.
        /// </summary>
        private const string RefreshElementName = "ELEMENT_Refresh";

        /// <summary>
        /// Template part name for jump to now button.
        /// </summary>
        private const string JumpToNowElementName = "ELEMENT_ScrollToNow";

        /// <summary>
        /// Template part name for Horizontal scrollbar focus visual.
        /// </summary>
        private const string ScrollBarFocusVisualElementName = "ScrollBarFocusVisualElement";

        /// <summary>
        /// Template part name for wait cotent.
        /// </summary>
        private const string WaitContentElementName = "WaitContent";

        /// <summary>
        /// Story board name for load animation.
        /// </summary>
        private const string LoadAnimationStoryBoardName = "LoadingAnimation";

        /// <summary>
        /// Template part name for time selector combobox.
        /// </summary>
        private const string TimeSelectorElementName = "ELEMENT_TimeSelector";

        /// <summary>
        /// Resource key for time frequency binding helper.
        /// </summary>
        private const string TimeFrequencyBindingHelperResourceKey = "timeFrequency";
        #endregion

        #region Template parts
        /// <summary>
        /// Member variable to hold the root panel containing all the graphs.
        /// </summary>
        private Grid rootPanel;
        
        /// <summary>
        /// Member variable to hold the root element.
        /// </summary>
        private Grid layoutRoot;

        /// <summary>
        /// Member variable to hold top axis.
        /// </summary>
        private TimeGraphBase topAxis;

        /// <summary>
        /// Member variable to hold bottom axis.
        /// </summary>
        private TimeGraphBase bottomAxis;

        /// <summary>
        /// Member variable to hold horizontal scrollbar.
        /// </summary>
        private ScrollBar horizontalScrollbar;

        /// <summary>
        /// Member variable to hold visual focus line.
        /// </summary>
        private VisualFocusLine visualFocusLine;

        /// <summary>
        /// Member variable to hold reset button.
        /// </summary>
        private Button resetButton;

        /// <summary>
        /// Member variable to hold refresh button.
        /// </summary>
        private Button refreshButton;

        /// <summary>
        /// Member variable to hold jump to now button.
        /// </summary>
        private Button jumpToNowButton;

        /// <summary>
        /// Member variable to hold scrollbar focus visual.
        /// </summary>
        private FrameworkElement scrollbarFocusVisual;

        /// <summary>
        /// Member variable to hold wait content.
        /// </summary>
        private FrameworkElement waitContent;

        /// <summary>
        /// Member variable to hold load animation storyboard.
        /// </summary>
        private Storyboard loadAnimation;

        /// <summary>
        /// Member variable to hold the timeselector combobox.
        /// </summary>
        private ComboBox timeSelector;

        /// <summary>
        /// Member variable to convert selected time range to a Time frequency object.
        /// </summary>
        private TimeFrequencyBindingHelper timeFrequencyBindingHelper;
        #endregion

        #region Private Members
        /// <summary>
        /// Member variable to hold graph sections.
        /// </summary>
        private Collection<GraphSection> sectionGraphs = new Collection<GraphSection>();

        /// <summary>
        /// Member variable to hold start date from the graphs data.
        /// </summary>
        private DateTime graphDataStartDate = DateTime.MinValue;

        /// <summary>
        /// Member variable to hold end date from the graphs data.
        /// </summary>
        private DateTime graphDataEndDate = DateTime.MinValue;        

        /// <summary>
        /// Member variable to hold axis design.
        /// </summary>
        private TimeFrequency axisDesign;

        /// <summary>
        /// Member variable to hold horizontal scrollbar binding.
        /// </summary>
        private Binding scrollBarValueBinding = new Binding("Value");

        /// <summary>
        /// Member variable to hold VisibleWindow binding object.
        /// </summary>
        private Binding visibleWindowBinding;        

        /// <summary>
        /// Member variable to indicate whether the template parts have been initialized.
        /// </summary>
        private bool templatePartsInitialized;

        /// <summary>
        /// Member variable to hold the binding helper object for scrollbar value.
        /// </summary>
        private DoubleBindingHelper scrollbarValueBindingHelper = new DoubleBindingHelper();

        /// <summary>
        /// Member variable to indicate whether scrollbar bindings have been created.
        /// </summary>
        private bool scrollbarBindingsCreated;

        /// <summary>
        /// DispatcherTimer object for refresh operation.
        /// </summary>
        private DispatcherTimer refreshTimer;

        /// <summary>
        /// Member variable to indicate a refresh is pending.
        /// </summary>
        private bool pendingRefresh;

        /// <summary>
        /// Member variable to indicate whether to reload graphs.
        /// </summary>
        private bool reloadGraphs = true;

        /// <summary>
        /// Member variable to indicate whether to update graph dates even if they are same.
        /// </summary>
        private bool forceUpdateGraphDates;
        #endregion        

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeActivityGraphHost"/> class.
        /// </summary>
        public TimeActivityGraphHost()
        {
            this.DefaultStyleKey = typeof(TimeActivityGraphHost);
            
            this.SectionMinHeight = 30;
            this.AllowSectionResize = true;
            this.Graphs = new ObservableCollection<TimeGraphBase>();

            this.visibleWindowBinding = new Binding("VisibleWindow");
            this.visibleWindowBinding.Source = this;
            this.visibleWindowBinding.Mode = BindingMode.TwoWay;

            this.scrollBarValueBinding.Mode = BindingMode.TwoWay;
            this.scrollBarValueBinding.Source = this.scrollbarValueBindingHelper;

            this.Graphs.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(this.Graphs_CollectionChanged);
            this.Loaded += new RoutedEventHandler(this.TimeActivityGraphHost_Loaded);
            this.NowDateTime = DateTime.Now;
        }
#endregion                

        #region Events
        /// <summary>
        /// Occurs when [section initialized].
        /// </summary>
        public event RoutedEventHandler SectionInitialized;

        /// <summary>
        /// Occurs when [section reset].
        /// </summary>
        public event RoutedEventHandler SectionReset;

        /// <summary>
        /// Occurs when [refresh].
        /// </summary>
        public event RoutedEventHandler Refresh;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the graphs.
        /// </summary>
        /// <value>The graphs.</value>
        public ObservableCollection<TimeGraphBase> Graphs
        {
            get
            {
                return (ObservableCollection<TimeGraphBase>)this.GetValue(GraphsProperty);
            }

            set
            {
                this.SetValue(GraphsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the section template.
        /// </summary>
        /// <value>The section template.</value>
        public DataTemplate SectionTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [allow section resize].
        /// </summary>
        /// <value>If [allow section resize] <c>true</c>; otherwise, <c>false</c>.</value>
        public bool AllowSectionResize
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the visible window.
        /// </summary>
        /// <value>The visible window.</value>
        public TimeFrequency VisibleWindow
        {
            get { return (TimeFrequency)GetValue(VisibleWindowProperty); }
            set { SetValue(VisibleWindowProperty, value); }
        }

        /// <summary>
        /// Gets or sets the index of the time frequency selected.
        /// </summary>
        /// <value>The index of the time frequency selected.</value>
        public int TimeFrequencySelectedIndex
        {
            get 
            { 
                return (int)GetValue(TimeFrequencySelectedIndexProperty); 
            }

            set 
            {
                SetValue(TimeFrequencySelectedIndexProperty, value); 
            }
        }

        /// <summary>
        /// Gets or sets the min height of the section.
        /// </summary>
        /// <value>The min height of the section.</value>
        public double SectionMinHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the style to be used for separator.
        /// </summary>
        /// <value>Style for the section separator.</value>
        public Style SeparatorStyle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the date time for the now indicator.
        /// </summary>
        /// <value>The date time for the now indicator.</value>
        public DateTime NowDateTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the axis start date.
        /// </summary>
        /// <value>The axis start date.</value>
        [TypeConverter(typeof(DateTimeConverter))]
        public DateTime? AxisStartDate
        {
            get { return (DateTime?)GetValue(AxisStartDateProperty); }
            set { SetValue(AxisStartDateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the axis end date.
        /// </summary>
        /// <value>The axis end date.</value>
        [TypeConverter(typeof(DateTimeConverter))]
        public DateTime? AxisEndDate
        {
            get { return (DateTime?)GetValue(AxisEndDateProperty); }
            set { SetValue(AxisEndDateProperty, value); }
        }
        #endregion

        #region Attached Properties Get/Set
        /// <summary>
        /// Gets the section name for the given object.
        /// </summary>
        /// <param name="obj">The object whose section name has to be retreived.</param>
        /// <returns>Section name of the object.</returns>
        public static string GetSectionName(DependencyObject obj)
        {
            return (string)obj.GetValue(SectionNameProperty);
        }

        /// <summary>
        /// Sets the section name for the given object.
        /// </summary>
        /// <param name="obj">The object whose section name has to be set.</param>
        /// <param name="value">Section name.</param>
        public static void SetSectionName(DependencyObject obj, string value)
        {
            obj.SetValue(SectionNameProperty, value);
        }
        #endregion        

        #region Overridden Methods
        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.InitializeTemplateParts();
            this.timeSelector.SelectionChanged += new SelectionChangedEventHandler(this.TimeSelector_SelectionChanged);

            this.timeSelector.SelectedIndex = this.TimeFrequencySelectedIndex;            

            this.topAxis.Loaded += new RoutedEventHandler(this.Graph_Loaded);
            this.topAxis.PanelLayoutCompleted += new EventHandler<RoutedEventArgs>(this.TopAxis_PanelLayoutCompleted);
            this.topAxis.RetainViewportDateOnSizeChange = true;

            this.horizontalScrollbar.KeyDown += new KeyEventHandler(this.HorizontalScrollBar_KeyDown);
            this.horizontalScrollbar.GotFocus += new RoutedEventHandler(this.HorizontalScrollbar_GotFocus);
            this.horizontalScrollbar.LostFocus += new RoutedEventHandler(this.HorizontalScrollbar_LostFocus);

            this.topAxis.ApplyTemplate();
            this.bottomAxis.ApplyTemplate();

            if (this.resetButton != null)
            {
                this.resetButton.Click += new RoutedEventHandler(this.ResetButton_Click);
            }

            if (this.refreshButton != null)
            {
                this.refreshButton.Click += new RoutedEventHandler(this.RefreshButton_Click);
            }

            if (this.jumpToNowButton != null)
            {
                this.jumpToNowButton.Click += new RoutedEventHandler(this.ScrollToNow_Click);
            }

            this.reloadGraphs = true;
            this.RefreshLayout();
        }
                
        /// <summary>
        /// Refreshes the whole layout.
        /// </summary>
        public void RefreshLayout()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                this.ShowWaitContent(true);
            }

            if (this.refreshTimer == null)
            {
                this.refreshTimer = new DispatcherTimer();
                this.refreshTimer.Interval = TimeSpan.FromTicks(1);
                this.refreshTimer.Tick += new EventHandler(this.RefreshTimer_Tick);
            }            

            if (!this.refreshTimer.IsEnabled)
            {
                this.pendingRefresh = true;
                this.refreshTimer.Start();
            }
        }

        /// <summary>
        /// Shows the loading screen.
        /// </summary>
        public void ShowLoadingScreen()
        {
            this.ShowWaitContent(true);
        }

        /// <summary>
        /// Hides the loading screen.
        /// </summary>
        public void HideLoadingScreen()
        {
            this.ShowWaitContent(false);
        }
        #endregion

        #region Dependency Property Callbacks
        /// <summary>
        /// Called when [visible window changed].
        /// </summary>
        /// <param name="d">The dependency object whose visible window property was changed.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnVisibleWindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeActivityGraphHost host = d as TimeActivityGraphHost;
            if (host != null)
            {                
                if (host.topAxis != null)
                {
                    host.topAxis.SetViewportDate();
                    host.reloadGraphs = false;
                    host.forceUpdateGraphDates = true;
                    host.RefreshLayout();
                    host.topAxis.ScrollToViewportDate();
                }                                
            }
        }

        /// <summary>
        /// Called when [visible window changed].
        /// </summary>
        /// <param name="d">The dependency object whose visible window property was changed.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnAxisDatesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeActivityGraphHost host = d as TimeActivityGraphHost;
            if (host != null)
            {
                if (host.topAxis != null)
                {
                    host.topAxis.SetViewportDate();
                    host.RefreshLayout();
                    host.topAxis.ScrollToViewportDate();
                }
            }
        }

        /// <summary>
        /// Called when time frequency selected index changed.
        /// </summary>
        /// <param name="d">The object whose timefrequency selected index got changed.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnTimeFrequencySelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeActivityGraphHost host = d as TimeActivityGraphHost;
            if (host != null && host.timeSelector != null)
            {
                host.timeSelector.SelectedIndex = (int)e.NewValue;
            }
        }
        #endregion

        #region Private Methods
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

        /// <summary>
        /// Initializes the template parts.
        /// </summary>
        private void InitializeTemplateParts()
        {
            this.layoutRoot = this.GetTemplateChild<Grid>(LayoutRootElementName, true);
            this.rootPanel = this.GetTemplateChild<Grid>(RootPanelElementName, true);
            this.topAxis = this.GetTemplateChild<TimeGraphBase>(TopAxisElementName, true);
            this.bottomAxis = this.GetTemplateChild<TimeGraphBase>(BottomAxisElementName, true);
            this.horizontalScrollbar = this.GetTemplateChild<ScrollBar>(ScrollbarElementName, true);
            this.visualFocusLine = this.GetTemplateChild<VisualFocusLine>(VisualFocusLineElementName, true);
            this.timeSelector = this.GetTemplateChild<ComboBox>(TimeSelectorElementName, true);
            this.timeFrequencyBindingHelper = StyleParser.FindResource<TimeFrequencyBindingHelper>(this.layoutRoot, TimeFrequencyBindingHelperResourceKey, true);
            this.resetButton = this.GetTemplateChild<Button>(ResetElementName, false);
            this.refreshButton = this.GetTemplateChild<Button>(RefreshElementName, false);
            this.jumpToNowButton = this.GetTemplateChild<Button>(JumpToNowElementName, false);
            this.scrollbarFocusVisual = this.GetTemplateChild<FrameworkElement>(ScrollBarFocusVisualElementName, false);
            this.waitContent = this.GetTemplateChild<FrameworkElement>(WaitContentElementName, false);
            this.loadAnimation = this.layoutRoot.Resources[LoadAnimationStoryBoardName] as Storyboard;            
            this.templatePartsInitialized = true;
        }       

        /// <summary>
        /// Sets the horizontal scrollbar bindings.
        /// </summary>
        private void SetHorizontalScrollbarBindings()
        {
            if (!this.scrollbarBindingsCreated)
            {
                this.horizontalScrollbar.SetBinding(ScrollBar.SmallChangeProperty, this.GetScrollBarBindingFromProperty("SmallChange"));
                this.horizontalScrollbar.SetBinding(ScrollBar.LargeChangeProperty, this.GetScrollBarBindingFromProperty("LargeChange"));
                this.horizontalScrollbar.SetBinding(ScrollBar.ViewportSizeProperty, this.GetScrollBarBindingFromProperty("ViewportSize"));
                this.horizontalScrollbar.SetBinding(ScrollBar.MaximumProperty, this.GetScrollBarBindingFromProperty("Maximum"));
                this.horizontalScrollbar.SetBinding(ScrollBar.MinimumProperty, this.GetScrollBarBindingFromProperty("Minimum"));
                this.horizontalScrollbar.SetBinding(ScrollBar.ValueProperty, this.scrollBarValueBinding);
                this.scrollbarBindingsCreated = true;
            }
        }

        /// <summary>
        /// Gets the scroll bar binding from property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Binding object with source as top axis scrollbar.</returns>
        private Binding GetScrollBarBindingFromProperty(string propertyName)
        {
            Binding binding = new Binding(propertyName);

            if (propertyName == "Value")
            {
                binding.Source = this.horizontalScrollbar;
                binding.Mode = BindingMode.TwoWay;
            }
            else
            {
                binding.Source = this.topAxis.ScrollBar;
                binding.Mode = BindingMode.TwoWay;
            }

            return binding;
        }

        /// <summary>
        /// Adds the graphs to panel.
        /// </summary>
        private void AddGraphsToPanel()
        {
            if (this.SectionTemplate == null)
            {
                this.SectionTemplate = this.layoutRoot.Resources["DefaultSectionTemplate"] as DataTemplate;
            }

            if (this.rootPanel != null && this.SectionTemplate != null)
            {
                this.rootPanel.RowDefinitions.Clear();
                this.rootPanel.Children.Clear();

                int rowIndex = 0;
                for (int i = 0; i < this.sectionGraphs.Count; i++)
                {
                    GraphSection section = this.sectionGraphs[i];
                    this.IntializeSection(section);

                    RowDefinition row = new RowDefinition();
                    row.MinHeight = this.SectionMinHeight;                                       
                    this.rootPanel.RowDefinitions.Add(row);

                    FrameworkElement sectionContent = section.HeaderTemplate.LoadContent() as FrameworkElement;
                    sectionContent.DataContext = section;

                    Grid.SetRow(sectionContent, rowIndex);
                    this.rootPanel.Children.Add(sectionContent);
                    rowIndex++;

                    if (this.AllowSectionResize && i < this.sectionGraphs.Count - 1)
                    {
                        RowDefinition separatorRow = new RowDefinition() { Height = new GridLength(1.0, GridUnitType.Auto) };
                        this.rootPanel.RowDefinitions.Add(separatorRow);
                        GridSplitter separator = new GridSplitter() { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Stretch, Height = 5 };

                        if (this.SeparatorStyle != null)
                        {
                            separator.Style = this.SeparatorStyle;
                        }

                        separator.Background = new SolidColorBrush(Colors.LightGray);
                        Grid.SetRow(separator, rowIndex);
                        this.rootPanel.Children.Add(separator);

                        rowIndex++;
                    }
                }
            }
        }        

        /// <summary>
        /// Gets the axis dates.
        /// </summary>
        private void GetAxisDates()
        {
            foreach (TimeGraphBase graph in this.Graphs)
            {
                graph.VisibleWindow = this.VisibleWindow;
                DateTime? graphAxisStartDate;
                DateTime? graphAxisEndDate;
                graph.ApplyTemplate();
                graph.GetAxisDates(out graphAxisStartDate, out graphAxisEndDate);

                if (graphAxisStartDate.HasValue)
                {
                    if (this.graphDataStartDate == DateTime.MinValue || this.graphDataStartDate > graphAxisStartDate.Value)
                    {
                        this.graphDataStartDate = graphAxisStartDate.Value;
                    }
                }

                if (graphAxisEndDate.HasValue)
                {
                    if (this.graphDataEndDate == DateTime.MinValue || this.graphDataEndDate < graphAxisEndDate.Value)
                    {
                        this.graphDataEndDate = graphAxisEndDate.Value;
                    }
                }
            }
        }      
        
        /// <summary>
        /// Sets the axis dates.
        /// </summary>
        private void SetAxisDates()
        {
            if (this.VisibleWindow != null)
            {
                DateTime? startDate = null;
                DateTime? endDate = null;
                if (this.AxisStartDate.HasValue && this.AxisEndDate.HasValue)
                {
                    startDate = this.AxisStartDate.Value;
                    endDate = this.AxisEndDate.Value;
                }
                else
                {
                    if (this.Graphs.Count > 0)
                    {
                        this.GetAxisDates();
                        if (this.graphDataStartDate != DateTime.MinValue && this.graphDataEndDate != DateTime.MinValue)
                        {
                            this.axisDesign = AxisHelper.GetAxisDesign(this.VisibleWindow.Ticks);
                            DateTime hostStartDateTime = AxisHelper.GetAxisStartDate(this.graphDataStartDate, this.axisDesign);
                            DateTime hostEndDateTime = AxisHelper.GetAxisEndDate(this.graphDataEndDate, this.axisDesign);
                            AxisHelper.AdjustAxisStartDate(ref hostStartDateTime, ref hostEndDateTime, this.axisDesign);

                            startDate = this.AxisStartDate ?? hostStartDateTime;
                            endDate = this.AxisEndDate ?? hostEndDateTime;
                        }
                    }
                    else
                    {
                        startDate = DateTime.MinValue;
                        endDate = startDate.Value.AddTicks(this.VisibleWindow.Ticks);
                    }
                }

                foreach (TimeGraphBase graph in this.Graphs)
                {
                    this.SetDatesToGraph(graph, startDate, endDate);
                }

                this.SetDatesToGraph(this.topAxis, startDate, endDate);
                this.SetDatesToGraph(this.bottomAxis, startDate, endDate);
            }
        }

        /// <summary>
        /// Sets the dates to graph.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="startDate">The axis start date.</param>
        /// <param name="endDate">The axis end date.</param>
        private void SetDatesToGraph(TimeGraphBase graph, DateTime? startDate, DateTime? endDate)
        {
            bool invalidDates = false;
            if (!startDate.HasValue || !endDate.HasValue)
            {
                invalidDates = true;
            }
            else if (startDate > endDate)
            {
                invalidDates = true;
            }

            if (invalidDates)
            {
                throw new ArgumentException("Invalid axis dates");
            }
            
            if (graph != null && (graph.AxisStartDate != startDate || graph.AxisEndDate != endDate || this.forceUpdateGraphDates))
            {
                graph.ShowAxisEndDateOnLoad = false;
                graph.VisibleWindow = this.VisibleWindow;
                graph.DetermineAxisDates = false;
                graph.AutoUpdateFutureShading = false;
                graph.AxisStartDate = startDate.Value;
                graph.AxisEndDate = endDate.Value;
                graph.NowDateTime = this.NowDateTime;

                if (!graph.ScrollBarBindingCreated && this.scrollBarValueBinding != null)
                {
                    if (graph.ScrollBar == null)
                    {
                        graph.ApplyTemplate();
                    }

                    graph.ScrollBar.SetBinding(ScrollBar.ValueProperty, this.scrollBarValueBinding);
                    graph.ScrollBarBindingCreated = true;
                }

                graph.Refresh(true);
            }
        }

        /// <summary>
        /// Adds the graph to section.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <returns>Boolean indicating whether a new section has been created.</returns>
        private bool AddGraphToSection(TimeGraphBase graph)
        {
            bool newSection = false;
            string sectionName = GetSectionName(graph);
            GraphSection section = this.GetGraphSection(sectionName);

            if (section == null)
            {
                section = this.CreateGraphSection(sectionName);
                newSection = true;
            }

            graph.DetermineAxisDates = false;
            graph.AutoUpdateFutureShading = false;
            section.Graphs.Add(graph);

            return newSection;
        }

        /// <summary>
        /// Removes the graph from section.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <returns>Boolean indicating whether a section also has been removed.</returns>
        /// <remarks>Section will be removed when the Graph count in the section is 0.</remarks>
        private bool RemoveGraphFromSection(TimeGraphBase graph)
        {
            bool sectionRemoved = false;
            string sectionName = GetSectionName(graph);
            GraphSection section = this.GetGraphSection(sectionName);

            if (section != null)
            {                
                section.Graphs.Remove(graph);
                if (section.Graphs.Count == 0)
                {
                    sectionRemoved = this.sectionGraphs.Remove(section);
                }
            }

            return sectionRemoved;
        }

        /// <summary>
        /// Gets the graph section.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>Gets the <see cref="GraphSection"/> instance from the section name.</returns>
        private GraphSection GetGraphSection(string sectionName)
        {
            GraphSection section = null;
            foreach (GraphSection graphSection in this.sectionGraphs)
            {
                if (string.Compare(sectionName, graphSection.SectionName, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    section = graphSection;
                    break;
                }
            }            

            return section;
        }

        /// <summary>
        /// Creates the graph section.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns>A new graph section with the specified section name.</returns>
        private GraphSection CreateGraphSection(string sectionName)
        {
            GraphSection section = new GraphSection();
            section.SectionName = sectionName;
            this.sectionGraphs.Add(section);

            return section;
        }

        /// <summary>
        /// Intializes the section.
        /// </summary>
        /// <param name="section">The section.</param>
        private void IntializeSection(GraphSection section)
        {
            if (!section.Initialized && this.templatePartsInitialized)
            {
                if (this.SectionInitialized != null)
                {
                    this.SectionInitialized(section, new RoutedEventArgs());
                }
                else
                {
                    section.HeaderTemplate = this.SectionTemplate;
                }

                if (section.HeaderTemplate == null)
                {
                    section.HeaderTemplate = this.layoutRoot.Resources["DefaultSectionTemplate"] as DataTemplate;
                }

                section.Initialized = true;
            }
        }

        /// <summary>
        /// Resets this graph to the default state.
        /// </summary>
        private void Reset()
        {
            foreach (FrameworkElement section in this.rootPanel.Children)
            {
                if (section.DataContext is GraphSection)
                {
                    RowDefinition row = this.rootPanel.RowDefinitions[Grid.GetRow(section)];
                    row.Height = new GridLength(1.0, GridUnitType.Star);
                    if (this.SectionReset != null)
                    {
                        this.SectionReset(section, new RoutedEventArgs());
                    }                    
                }
            }

            if (this.timeSelector.SelectedIndex != this.TimeFrequencySelectedIndex)
            {
                this.timeSelector.SelectedIndex = this.TimeFrequencySelectedIndex;
            }
                        
            this.topAxis.ScrollToDate(this.NowDateTime);
        }

        /// <summary>
        /// Sets the past date to graphs.
        /// </summary>
        private void SetPastDateToGraphs()
        {
            foreach (TimeGraphBase graph in this.Graphs)
            {
                graph.NowDateTime = this.NowDateTime;
            }
        }              
                
        /// <summary>
        /// Raises the refresh event.
        /// </summary>
        private void RaiseRefreshEvent()
        {
            if (this.Refresh != null)
            {
                this.Refresh(this, new RoutedEventArgs());
            }
        }

        /// <summary>
        /// Shows the content of the wait.
        /// </summary>
        /// <param name="show">If set to <c>true</c> [show].</param>
        private void ShowWaitContent(bool show)
        {
            if (this.waitContent != null)
            {
                if (show)
                {
                    this.waitContent.Opacity = 1;
                    if (this.loadAnimation != null)
                    {
                        this.loadAnimation.Begin();
                    }
                }
                else
                {
                    this.waitContent.Opacity = 0;
                    if (this.loadAnimation != null)
                    {
                        this.loadAnimation.Stop();
                    }
                }
            }
        }

        /// <summary>
        /// Refreshes the layout.
        /// </summary>        
        private void RefreshLayoutInternal()
        {
            this.SetAxisDates();
            this.forceUpdateGraphDates = false;
            if (this.reloadGraphs)
            {
                this.AddGraphsToPanel();
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the Loaded event of the TimeActivityGraphHost control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void TimeActivityGraphHost_Loaded(object sender, RoutedEventArgs e)
        {            
            this.SizeChanged += new SizeChangedEventHandler(this.TimeActivityGraphHost_SizeChanged);
        }

        /// <summary>
        /// Handles the SizeChanged event of the TimeActivityGraphHost control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        private void TimeActivityGraphHost_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.topAxis.SetViewportDate();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the TimeSelector control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void TimeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.VisibleWindow = this.timeSelector.SelectedItem as TimeFrequency;
        }

        /// <summary>
        /// Handles the Loaded event of the Graph control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Graph_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetHorizontalScrollbarBindings();
            this.topAxis.ScrollToDate(this.NowDateTime);
        }              

        /// <summary>
        /// Handles the CollectionChanged event of the Graphs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void Graphs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:                    
                    foreach (TimeGraphBase graph in e.NewItems)
                    {
                        this.AddGraphToSection(graph);                        
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (TimeGraphBase graph in e.NewItems)
                    {
                        this.RemoveGraphFromSection(graph);                        
                    }

                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach (TimeGraphBase graph in e.OldItems)
                    {
                       this.RemoveGraphFromSection(graph);                       
                    }

                    foreach (TimeGraphBase graph in e.NewItems)
                    {
                        this.AddGraphToSection(graph);                        
                    }

                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.sectionGraphs.Clear();
                    foreach (TimeGraphBase graph in this.Graphs)
                    {
                        this.AddGraphToSection(graph);
                    }

                    break;
            }

            this.reloadGraphs = true;
        }        

        /// <summary>
        /// Handles the PanelLayoutCompleted event of the TopAxis control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void TopAxis_PanelLayoutCompleted(object sender, RoutedEventArgs e)
        {
            if (this.topAxis.TitleStartDate <= this.NowDateTime && this.topAxis.TitleEndDate >= this.NowDateTime)
            {
                this.visualFocusLine.Visibility = Visibility.Visible;
                this.visualFocusLine.DateTime = this.NowDateTime;
                this.visualFocusLine.XOffset = this.topAxis.GetXForDateInView(this.NowDateTime);
            }
            else
            {
                this.visualFocusLine.Visibility = Visibility.Collapsed;
            }

            foreach (TimeGraphBase graph in this.Graphs)
            {
                graph.UpdateFutureShadingOffset();
            }
        }
        
        /// <summary>
        /// Handles the Click event of the ResetButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.Reset();
        }

        /// <summary>
        /// Handles the Click event of the ScrollToNow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ScrollToNow_Click(object sender, RoutedEventArgs e)
        {
            this.topAxis.ScrollToDate(this.NowDateTime);
        }

        /// <summary>
        /// Handles the Click event of the RefreshButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            this.topAxis.SetViewportDate();
            this.reloadGraphs = false;
            this.NowDateTime = DateTime.Now;
            this.visualFocusLine.DateTime = this.NowDateTime;
            this.visualFocusLine.XOffset = this.topAxis.GetXForDateInView(this.NowDateTime);            
            this.SetPastDateToGraphs();
            this.RaiseRefreshEvent();
            this.topAxis.ScrollToViewportDate();
        }

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
                            this.horizontalScrollbar.Value -= this.horizontalScrollbar.LargeChange;
                            e.Handled = true;
                        }
                        else
                        {
                            this.horizontalScrollbar.Value -= this.horizontalScrollbar.SmallChange;
                            e.Handled = true;
                        }

                        break;
                    case Key.Right:
                        if (ctrlKeyPressed)
                        {
                            this.horizontalScrollbar.Value += this.horizontalScrollbar.LargeChange;
                            e.Handled = true;
                        }
                        else
                        {
                            this.horizontalScrollbar.Value += this.horizontalScrollbar.SmallChange;
                            e.Handled = true;
                        }

                        break;
                    case Key.Home:
                        if (ctrlKeyPressed)
                        {
                            this.horizontalScrollbar.Value = this.horizontalScrollbar.Minimum;
                            e.Handled = true;
                        }

                        break;
                    case Key.End:
                        if (ctrlKeyPressed)
                        {
                            this.horizontalScrollbar.Value = this.horizontalScrollbar.Maximum;
                            e.Handled = true;
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Handles the LostFocus event of the HorizontalScrollbar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void HorizontalScrollbar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.scrollbarFocusVisual != null)
            {
                this.scrollbarFocusVisual.Opacity = 0;
            }
        }

        /// <summary>
        /// Handles the GotFocus event of the HorizontalScrollbar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void HorizontalScrollbar_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.scrollbarFocusVisual != null)
            {
                this.scrollbarFocusVisual.Opacity = 1;
            }
        }

        /// <summary>
        /// Handles the Tick event of the RefreshTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (this.pendingRefresh)
            {
                this.RefreshLayoutInternal();
                this.pendingRefresh = false;                
                this.refreshTimer.Stop();
                if (this.topAxis != null)
                {
                    if (this.reloadGraphs)
                    {
                        this.topAxis.ScrollToDate(this.NowDateTime);
                    }
                    else
                    {
                        this.topAxis.ScrollToViewportDate();
                    }
                }

                this.ShowWaitContent(false);
            }
        }
        #endregion
    }
}
