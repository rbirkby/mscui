//-----------------------------------------------------------------------
// <copyright file="TimeGraphBase.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Time graph base class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using Microsoft.Cui.Controls.GraphingControl;    
    #endregion

    /// <summary>
    /// Base class for time graph.
    /// </summary>
    public class TimeGraphBase : GraphBase
    {
        #region Dependency Properties
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeGraphBase.VisibleWindow"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty VisibleWindowProperty = DependencyProperty.Register("VisibleWindow", typeof(TimeFrequency), typeof(TimeGraphBase), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeGraphBase.AxisStartDate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AxisStartDateProperty = DependencyProperty.Register("AxisStartDate", typeof(DateTime), typeof(TimeGraphBase), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeGraphBase.TickLineBrush"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TickLineBrushProperty = DependencyProperty.Register("TickLineBrush", typeof(Brush), typeof(TimeGraphBase), new PropertyMetadata(new PropertyChangedCallback(GraphLayoutChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeGraphBase.TickLineThickness"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TickLineThicknessProperty = DependencyProperty.Register("TickLineThickness", typeof(double), typeof(TimeGraphBase), new PropertyMetadata(new PropertyChangedCallback(GraphLayoutChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeGraphBase.GridLineBrush"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty GridLineBrushProperty = DependencyProperty.Register("GridLineBrush", typeof(Brush), typeof(TimeGraphBase), new PropertyMetadata(new PropertyChangedCallback(GraphLayoutChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeGraphBase.GridLineThickness"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty GridLineThicknessProperty = DependencyProperty.Register("GridLineThickness", typeof(double), typeof(TimeGraphBase), new PropertyMetadata(new PropertyChangedCallback(GraphLayoutChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeGraphBase.Title" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(TimeGraphBase), null);
                
        /// <summary>
        /// Identifies the dependency property for label visibility.
        /// </summary>
        public static readonly DependencyProperty LabelVisibilityProperty = DependencyProperty.Register("LabelVisibility", typeof(Visibility), typeof(TimeGraphBase), new PropertyMetadata(new PropertyChangedCallback(LabelVisibilityCallback)));

        /// <summary>
        /// Identifies the dependency property for show grid lines.
        /// </summary>
        public static readonly DependencyProperty ShowGridLinesProperty = DependencyProperty.Register("ShowGridLines", typeof(GridLineVisibility), typeof(TimeGraphBase), new PropertyMetadata(new PropertyChangedCallback(GridLinesVisibilityCallback)));

        /// <summary>
        /// Identifies the dependency property for show minor interval lines.
        /// </summary>
        public static readonly DependencyProperty ShowMinorGridLinesProperty = DependencyProperty.Register("ShowMinorGridLines", typeof(GridLineVisibility), typeof(TimeGraphBase), new PropertyMetadata(new PropertyChangedCallback(GridLinesVisibilityCallback)));
                
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeGraphBase.Minimized"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MinimizedProperty = DependencyProperty.Register("Minimized", typeof(bool), typeof(TimeGraphBase), new PropertyMetadata(new PropertyChangedCallback(MinimizedPropertyChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeGraphBase.Description"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(TimeGraphBase), new PropertyMetadata(string.Empty));
        #endregion

        #region Template Part Names
        /// <summary>
        /// Main top axis viewport layer.
        /// </summary>
        internal const string DynamicTopAxisLayerViewportElementName = "ELEMENT_dynamicTopAxisLayerViewport";

        /// <summary>
        /// Main top axis layer.
        /// </summary>
        internal const string DynamicTopAxisLayerElementName = "ELEMENT_dynamicTopAxisLayer";

        /// <summary>
        /// Data marker template resource key name.
        /// </summary>
        internal const string DataMarkerResourceKey = "ELEMENT_dataMarkerTemplate";

        /// <summary>
        /// Data point template resource key name.
        /// </summary>
        internal const string DataPointTemplateResourceKey = "ELEMENT_PointTemplate";

        /// <summary>
        /// Data point label template resource key name.
        /// </summary>
        internal const string DataPointLabelTemplateResourceKey = "ELEMENT_LabelTemplate";

        /// <summary>
        /// Data point template transform resource key name.
        /// </summary>
        internal const string DataPointLabelTransformResourceKey = "ELEMENT_LabelTransform";

        /// <summary>
        /// X Axis Label element name.
        /// </summary>
        internal const string XAxisLabelTemplateElementName = "ELEMENT_XAxisLabelTemplate";

        /// <summary>
        /// Y Axis Label element name.
        /// </summary>
        internal const string YAxisLabelTemplateElementName = "ELEMENT_YAxisLabelTemplate";

        /// <summary>
        /// Template part name for Minimized view plot layer.
        /// </summary>
        internal const string MinimizedPlotLayerElementName = "ELEMENT_minimizedPlotLayer";

        /// <summary>
        /// Template part name for minimized view interpolation line.
        /// </summary>
        internal const string MinimizedViewInterpolationLineElementName = "Element_MinimizedViewLine";

        /// <summary>
        /// Resource key name for default visible window.
        /// </summary>
        internal const string DefaultVisibleWindowResourceKeyName = "DefaultVisibleWindow";

        /// <summary>
        /// Template part name for future canvas layer.
        /// </summary>
        internal const string FutureCanvasElementName = "ELEMENT_FutureCanvas";
        #endregion      

        #region Member Variables
#if !SILVERLIGHT
        /// <summary>
        /// Binding expression for Y1.
        /// </summary>
        private BindingExpression bindingY1;

        /// <summary>
        /// Binding expression for Y2.
        /// </summary>
        private BindingExpression bindingY2;

        /// <summary>
        /// Binding expression for X1.
        /// </summary>
        private BindingExpression bindingX1;

        /// <summary>
        /// Binding expression for X2.
        /// </summary>
        private BindingExpression bindingX2;

        /// <summary>
        /// Binding expression for DataMarkerTemplate.
        /// </summary>
        private BindingExpression bindingDataMarkerTemplate;

        /// <summary>
        /// Binding expression for Label.
        /// </summary>
        private BindingExpression bindingLabel;
#endif

        /// <summary>
        /// Member variable to hold scale factor.
        /// </summary>
        private double scaleFactor;

        /// <summary>
        /// Member variable to hold invalid date selected.
        /// </summary>
        private bool invalidDateSelected;       

        /// <summary>
        /// Member variable to hold template for X axis labels.
        /// </summary>
        private DataTemplate xaxisLabelTemplate;
        
        /// <summary>
        /// Member variable to hold time frequency for the x axis.
        /// </summary>
        private TimeFrequency frequency;
        
        /// <summary>
        /// Member variable to hold textblock used for showing information at design time.
        /// </summary>
        private TextBlock designModeInformationBlock;

        /// <summary>
        /// Base value for the label zindex.
        /// </summary>
        private int? labelTemplateZindex;

        /// <summary>
        /// Member variable to hold last data points.
        /// </summary>
        private object[] lastDataPoints;

        /// <summary>
        /// Member variable to hold previous data context.
        /// </summary>
        private object previousDataContext;

        /// <summary>
        /// Member variable to indicate current scrollbar position.
        /// </summary>
        /// <remarks>
        /// Possible values are -2,-1,0,1. 
        /// -2 inicates scrollbar thumb is disabled (maximum and minimum are 0),
        /// -1 indicates scrollbar is at minimum,
        /// 1 indicates at scrollbar at maximum and 0 at other.
        /// when -2 scroll to now datetime and when 0 scroll to retain the date in viewport
        /// </remarks>         
        private sbyte scrollPosition;

        /// <summary>
        /// Member variable to hold current date time in the viewport.
        /// </summary>
        private DateTime? viewPortDateTime;

        /// <summary>
        /// Member variable to hold future canvas.
        /// </summary>
        private FrameworkElement futureCanvas;
        #endregion        

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeGraphBase"/> class.
        /// </summary>
        public TimeGraphBase()
        {                        
            this.DetermineAxisDates = true;
            this.ShowDataPointLabels = Visibility.Collapsed;
            this.ShowDataPointLabelsOnHover = true;            
            this.ShowGridLines = GridLineVisibility.Both;
            this.ShowMinorGridLines = GridLineVisibility.None;
            this.TickLineBrush = new SolidColorBrush(Colors.DarkGray);
            this.TickLineThickness = 1;
            this.GridLineBrush = new SolidColorBrush(Colors.LightGray);
            this.GridLineThickness = 0.5;
            this.NowDateTime = DateTime.Now;
            this.AddYAxisSeparator = true;
            this.DetectCollisions = true;
            this.AutoUpdateFutureShading = true;
        }

        #endregion       

        #region Public Properties
        /// <summary>
        /// Gets the Axis start date.
        /// </summary>
        /// <value>Start date of the Axis.</value>        
        [Category("Axis Details")]
        public DateTime AxisStartDate
        {
            get { return (DateTime)this.GetValue(TimeGraphBase.AxisStartDateProperty); }
            internal set { this.SetValue(TimeGraphBase.AxisStartDateProperty, value); }
        }

        /// <summary>
        /// Gets end date of the Axis based on the data.
        /// </summary>
        /// <value>End date of the Axis.</value>
        [Category("Axis Details")]
        public DateTime AxisEndDate
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the time frequency of the x-axis.
        /// </summary>
        /// <value>Time frequency of the x axis.</value>
        [Category("Axis Details")]
        public TimeFrequency VisibleWindow
        {
            get { return (TimeFrequency)this.GetValue(TimeGraphBase.VisibleWindowProperty); }
            set { this.SetValue(TimeGraphBase.VisibleWindowProperty, value); }
        }        
        
        /// <summary>
        /// Gets or sets the thickness for the tick lines.
        /// </summary>
        /// <value>Thickness of the tick lines.</value>
        [Category("Graph Appearance")]
        public double TickLineThickness
        {
            get { return (double)this.GetValue(TickLineThicknessProperty); }
            set { this.SetValue(TickLineThicknessProperty, value); }
        }

        /// <summary>
        /// Gets or sets the brush to use to draw the tick lines.
        /// </summary>
        /// <value>Brush used to paint tick lines.</value>
        [Category("Graph Appearance")]
        public Brush TickLineBrush
        {
            get { return (Brush)this.GetValue(TickLineBrushProperty); }
            set { this.SetValue(TickLineBrushProperty, value); }
        }

        /// <summary>
        /// Gets or sets the thickness for the grid lines.
        /// </summary>
        /// <value>Thickness of the grid lines.</value>
        [Category("Graph Appearance")]
        public double GridLineThickness
        {
            get { return (double)this.GetValue(GridLineThicknessProperty); }
            set { this.SetValue(GridLineThicknessProperty, value); }
        }

        /// <summary>
        /// Gets or sets the brush to use to draw the grid lines.
        /// </summary>
        /// <value>Brush used to paint grid lines.</value>
        [Category("Graph Appearance")]
        public Brush GridLineBrush
        {
            get { return (Brush)this.GetValue(GridLineBrushProperty); }
            set { this.SetValue(GridLineBrushProperty, value); }
        }

        /// <summary>
        /// Gets or sets the title of the graph.
        /// </summary>
        /// <value>Title of the graph.</value>
        [Category("Graph Appearance")]
        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the description for the graph.
        /// </summary>
        /// <value>The description for the graph.</value>
        [Category("Graph Appearance")]
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the brush to be used for background on hover.
        /// </summary>
        /// <value>Background brush to be used on hover.</value>
        /// <remarks>Applied in data selector only.</remarks>
        [Category("Graph Appearance")]
        public Brush HoverBackground
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the data point labels on hover.
        /// </summary>
        /// <value>Value indicating whether to show the data point labels on hover.</value>
        /// <remarks>This value is modified when VFL is shown.</remarks>
        [Category("Graph Appearance")]
        public bool ShowDataPointLabelsOnHover
        {
            get;
            set;
        }
       
        /// <summary>
        /// Gets or sets the grid line visibility.
        /// </summary>
        /// <value>Value indicating which grid lines are visible.</value>
        [Category("Graph Appearance")]
        public GridLineVisibility ShowGridLines
        {
            get { return (GridLineVisibility)this.GetValue(ShowGridLinesProperty); }
            set { this.SetValue(ShowGridLinesProperty, value); }
        }

        /// <summary>
        /// Gets or sets a grid lines visibility for minor interval.
        /// </summary>
        /// <value>Value indicating whether grid lines will be displayed for minor interval.</value>
        [Category("Graph Appearance")]
        public GridLineVisibility ShowMinorGridLines
        {
            get { return (GridLineVisibility)this.GetValue(ShowMinorGridLinesProperty); }
            set { this.SetValue(ShowMinorGridLinesProperty, value); }
        }

        /// <summary>
        /// Gets or sets visibility for the labels.
        /// </summary>
        /// <value>The label visiblity.</value>
        [Category("Graph Appearance")]
        public Visibility ShowDataPointLabels
        {
            get
            {
                return (Visibility)this.GetValue(LabelVisibilityProperty);
            }

            set
            {
                this.SetValue(LabelVisibilityProperty, value);
            }
        }        

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TimeGraphBase"/> is minimized.
        /// </summary>
        /// <value>If minimized <c>true</c>; otherwise, <c>false</c>.</value>
        [Category("Graph Appearance")]
        public bool Minimized
        {
            get { return (bool)this.GetValue(MinimizedProperty); }
            set { this.SetValue(MinimizedProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to add Y axis separator.
        /// </summary>
        /// <value>If add Y axis separator then <c>true</c>; otherwise, <c>false</c>.</value>
        /// <remarks>Defaults to true.</remarks>
        [Category("Graph Appearance")]
        public bool AddYAxisSeparator
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [auto update future shading].
        /// </summary>
        /// <value>
        /// If [auto update future shading] <c>true</c>; otherwise, <c>false</c>.
        /// </value>
        /// <remarks>Defaults to true.</remarks>
        [Category("Graph Appearance")]
        public bool AutoUpdateFutureShading
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to [detect collisions].
        /// </summary>
        /// <value>If [detect collisions]<c>true</c>; otherwise, <c>false</c>.</value>
        [Category("Graph Appearance")]
        public bool DetectCollisions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the datetime that represents the past.
        /// </summary>
        /// <value>The datetime that represents the past.</value>
        [Category("Graph Appearance")]
        [TypeConverter(typeof(DateTimeConverter))]
        public DateTime NowDateTime
        {
            get;
            set;
        }
        #endregion
        
        #region Internal Properties
        /// <summary>
        /// Gets or sets the start date time of the current window.
        /// </summary>
        /// <value>Start date time of the current window.</value>
        internal DateTime CurrentWindowStartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether an invalid date was selected.
        /// </summary>
        /// <value>Value indicating whether invalid date was selected.</value>
        internal bool InvalidDateSelected
        {
            get { return this.invalidDateSelected; }
            set { this.invalidDateSelected = value; }
        }

        /// <summary>
        /// Gets or sets the end date for the current window.
        /// </summary>
        /// <value>End date being currently shown.</value>
        internal DateTime CurrentWindowEndDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current working panel number.
        /// </summary>
        /// <value>The panel that is being used.</value>
        internal int CurrentWorkingPanelCallback
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to determine the X axis dates.
        /// </summary>
        /// <value>Value indicating whether to determine the X axis dates.</value>
        /// <remarks>Defaults to true. GraphHost sets to false and sets the start and end dates.</remarks>
        internal bool DetermineAxisDates
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the Up Scroll Amount for the Arrow.
        /// </summary>
        internal double ArrowUpScrollAmount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Down Scroll Amount for the Arrow.
        /// </summary>
        internal double ArrowDownScrollAmount
        {
            get;
            set;
        }        

        /// <summary>
        /// Gets or sets a value indicating whether [show axis end date on load].
        /// </summary>
        /// <value>
        /// If [show axis end date on load] <c>true</c>; otherwise, <c>false</c>.
        /// </value>
        internal bool ShowAxisEndDateOnLoad
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to retain viewport date on size change.
        /// </summary>
        /// <value>
        /// If [retain viewport date on size change] <c>true</c>; otherwise, <c>false</c>.
        /// </value>
        internal bool RetainViewportDateOnSizeChange
        {
            get;
            set;
        }
        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets or sets layer for Top Axis element.
        /// </summary>
        /// <value>Layer for Top Axis element.</value>
        protected Canvas DynamicTopAxisLayer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the dynamic plot layer viewport.
        /// </summary>
        /// <value>The dynamic plot layer viewport.</value>
        protected Canvas DynamicPlotLayerViewport
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the dynamic plot layer.
        /// </summary>
        /// <value>The dynamic plot layer.</value>
        protected Canvas DynamicPlotLayer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the dynamic top axis layer viewport.
        /// </summary>
        /// <value>The dynamic top axis layer viewport.</value>
        protected Canvas DynamicTopAxisLayerViewport
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Viewport panel for the static layer.
        /// </summary>
        /// <value>Viewport for static layer.</value>
        protected Canvas NonDynamicRightAxisViewPort
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the scrollbar.
        /// </summary>
        /// <value>Scroll bar element.</value>
        protected ScrollBar VerticalScrollBar
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the minimized plot layer.
        /// </summary>
        /// <value>The minimized plot layer.</value>
        protected Canvas MinimizedPlotLayer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the frequency.
        /// </summary>
        /// <value>The frequency.</value>
        protected TimeFrequency Frequency
        {
            get
            {
                return this.frequency;
            }
        }
        #endregion

        #region Virtual methods
        /// <summary>
        /// Overridden. Applies the specified template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.VerticalScrollBar = this.GetTemplateChild<ScrollBar>(VerticalScrollBarElementName, true);
            this.NonDynamicRightAxisViewPort = this.GetTemplateChild<Canvas>(NonDynamicRightAxisViewPortElementName, true);
            this.futureCanvas = this.GetTemplateChild<FrameworkElement>(FutureCanvasElementName, false);
           
            if (this.PointTemplate == null)
            {
                this.PointTemplate = StyleParser.FindResource<DataTemplate>(this.LayoutRoot, DataPointTemplateResourceKey, true);
            }

            if (this.LabelTemplate == null)
            {
                this.LabelTemplate = StyleParser.FindResource<DataTemplate>(this.LayoutRoot, DataPointLabelTemplateResourceKey, false);
            }

            if (this.LabelTransform == null)
            {
                this.LabelTransform = StyleParser.FindResource<Transform>(this.LayoutRoot, DataPointLabelTransformResourceKey, false);
            }

            this.xaxisLabelTemplate = StyleParser.FindResource<DataTemplate>(this.LayoutRoot, XAxisLabelTemplateElementName, true);

#if !SILVERLIGHT
            DependencyObject dp = this.PointTemplate.LoadContent();
            if (null != dp)
            {
                GraphPoint gp = dp as GraphPoint;
                if (null != gp)
                {
                    this.bindingY1 = gp.GetBindingExpression(GraphPoint.Y1Property);
                    this.bindingY2 = gp.GetBindingExpression(GraphPoint.Y2Property);
                    this.bindingX1 = gp.GetBindingExpression(GraphPoint.X1Property);
                    this.bindingX2 = gp.GetBindingExpression(GraphPoint.X2Property);
                    this.bindingDataMarkerTemplate = gp.GetBindingExpression(GraphPoint.DataMarkerTemplateProperty);
                    this.bindingLabel = gp.GetBindingExpression(GraphPoint.LabelProperty);
                }
            }
#endif
            // define the base zindex for the label
            FrameworkElement labelElement = null;
            if (null != this.LabelTemplate)
            {
                labelElement = this.LabelTemplate.LoadContent() as FrameworkElement;
                this.labelTemplateZindex = Canvas.GetZIndex(labelElement);
            }
        }

        /// <summary>
        /// Gets the data marker symbol.
        /// </summary>
        /// <param name="timePoint">Actual data for which data marker symbol.</param>
        /// <param name="offset">Offset of the data.</param>
        /// <returns>Data marker symbol.</returns>
        public FrameworkElement GetDataMarker(object timePoint, double offset)
        {
            Panel panel = this.GetPanelAtOffset(offset);
            if (panel != null)
            {
                CollisionDetectionManager collisionDetectionManager = GraphBase.GetCollisionDetectionManager(panel);
                if (collisionDetectionManager != null)
                {
                    int offsetRelativeToPanel = (int)(offset - Canvas.GetLeft(panel));
                    if (collisionDetectionManager.ElementLookupToX.ContainsKey(offsetRelativeToPanel))
                    {
                        System.Collections.Generic.LinkedList<FrameworkElement> elementsAtOffset = collisionDetectionManager.ElementLookupToX[offsetRelativeToPanel];
                        foreach (FrameworkElement element in elementsAtOffset)
                        {
                            if (GraphBase.GetDataPoint(element) == true)
                            {
                                FrameworkElement dataPointLabel = GraphBase.GetDataPointLabel(element);
                                if (dataPointLabel != null)
                                {
                                    GraphPoint gp = dataPointLabel.DataContext as GraphPoint;
                                    if (gp != null)
                                    {
                                        object data = gp.DataContext;
                                        if (data == timePoint)
                                        {
                                            return element;
                                        }
                                    }                                    
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Resets the graph. Restores the default layout of the graph.
        /// </summary>
        public override void Reset()
        {
            base.Reset();
            this.ShowGridLines = GridLineVisibility.Both;
            this.ShowDataPointLabels = Visibility.Collapsed;
        }

        /// <summary>
        /// Gets data filtered between two dates.
        /// </summary>
        /// <param name="startDate">Start date of the data.</param>
        /// <param name="endDate">End date of the data.</param>
        /// <returns>Filtered data between two dates.</returns>
        public virtual IEnumerable GetFilteredData(DateTime startDate, DateTime endDate)
        {
            IEnumerable enumerable = null;

            // if the datacontext supports filtering then we should tell it to filter.
            if (null != this.DataContext)
            {
                ISupportTimeWindow filter = this.DataContext as ISupportTimeWindow;
                if (null != filter)
                {
                    enumerable = (System.Collections.IEnumerable)filter.Filter(startDate, endDate);
                }
                else
                {
                    // need to manually filter the graph datasource
                    enumerable = DataHelper.Filter(this.DataContext, startDate, endDate);
                }
            }

            return enumerable;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Looks the through adjacent panel for overlap.
        /// </summary>
        /// <param name="panel">The panel.</param>
        /// <param name="comparisonPanel">The comparison panel.</param>
        /// <param name="offset">The offset.</param>
        internal static void LookThroughAdjacentPanelForOverlap(PanelWrapper panel, PanelWrapper comparisonPanel, double offset)
        {
            // all we need to do is determine if the items on the edge of the panels overlap.
            // so, really we are talking about a datamarker synmbol wide on either edge.
            // the seamedelementcolelction gives us the elements that are plotted
            // outside the bounds of our panel.
            foreach (FrameworkElement element in panel.SeamedElementCollection)
            {
                double elementLeft = Canvas.GetLeft(element);
                double elementWidth = element.Width;
                Collision collision;
                foreach (FrameworkElement comparisonElement in comparisonPanel.SeamedElementCollection)
                {
                    FrameworkElement elementLabel = GraphBase.GetDataPointLabel(element);
                    FrameworkElement comparisonElementLabel = GraphBase.GetDataPointLabel(comparisonElement);

                    if (null != elementLabel && null != comparisonElementLabel)
                    {
                        GraphPoint elementGraphPoint = elementLabel.DataContext as GraphPoint;
                        GraphPoint comparisonElementGraphPoint = comparisonElementLabel.DataContext as GraphPoint;

                        if (null != elementGraphPoint && null != comparisonElementGraphPoint && elementGraphPoint.DataContext != comparisonElementGraphPoint.DataContext)
                        {
                            double comparisonLeft = Canvas.GetLeft(comparisonElement) + offset;
                            bool flag = false;

                            if (elementLeft < comparisonLeft && (elementLeft + elementWidth) > comparisonLeft)
                            {
                                // the elements collide
                                // the element is to the left of the comparison and overlaps.
                                flag = true;
                            }
                            else if (elementLeft > comparisonLeft && elementLeft < (comparisonLeft + elementWidth))
                            {
                                // the elements collide
                                // the element is to the right of the comparison and overlaps.
                                flag = true;
                            }

                            if (true == flag)
                            {
                                collision = new Collision(element, element, true);
                                panel.CollisionCollection.Add(collision);                                                                
                                GraphBase.SetDataPointOverlapProperty(element, true);
                                GraphBase.SetDataPointOverlapProperty(elementLabel, true);
                                elementLabel.Visibility = Visibility.Collapsed;
                                GraphBase.SetDataPointOverlapProperty(comparisonElement, true);
                                GraphBase.SetDataPointOverlapProperty(comparisonElementLabel, true);
                                comparisonElementLabel.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the data marker mouse enter event.
        /// </summary>
        /// <param name="element">Data marker on which mouse entered.</param>
        /// <param name="showLabelIfOverlaps">Boolean indicating whether to show the symbol if the label overlaps.</param>
        internal void OnDataMarkerMouseEnter(DependencyObject element, bool showLabelIfOverlaps)
        {
            if (false == GraphBase.GetLastItem(element))
            {
                // If the data marker is overlapping another, then do not show its label.
                if (false == GraphBase.GetDataPointOverlapProperty(element))
                {
                    FrameworkElement label = GraphBase.GetDataPointLabel(element);
                    if (label != null)
                    {
                        label.RenderTransform = this.LabelTransform;
                        if (this.labelTemplateZindex.HasValue)
                        {
                            Canvas.SetZIndex(label, this.labelTemplateZindex.Value + 2);
                        }

                        if (showLabelIfOverlaps)
                        {
                            label.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            if (false == GraphBase.GetDataPointOverlapProperty(label))
                            {
                                label.Visibility = Visibility.Visible;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles the MouseLeave event on the data marker.
        /// </summary>
        /// <param name="element">Data marker on which mouse has exited.</param>
        internal void OnDataMarkerMouseLeave(DependencyObject element)
        {
            if (false == GraphBase.GetLastItem(element))
            {
                FrameworkElement label = GraphBase.GetDataPointLabel(element);
                if (label != null)
                {
                    if (label.RenderTransform != null)
                    {
                        label.RenderTransform = null;
                    }

                    if (this.labelTemplateZindex.HasValue)
                    {
                        Canvas.SetZIndex(label, this.labelTemplateZindex.Value);
                    }

                    if (false == GraphBase.GetSecondToLast(element))
                    {
                        if (Visibility.Collapsed == this.ShowDataPointLabels || GraphBase.GetDataPointOverlapProperty(label) == true)
                        {
                            label.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        if (GraphBase.GetDataPointOverlapProperty(label) == true)
                        {
                            label.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }                
        
        /// <summary>
        /// Gets the x co-ordinate for a specified date.
        /// </summary>
        /// <param name="time">Time for which the x co-ordinate is needed.</param>
        /// <returns>X co-ordinate for the specified time.</returns>
        internal double GetXForDate(DateTime time)
        {
            long offsetIntoWindowInTicks = time.Ticks - this.CurrentWindowStartDate.Ticks;
            return offsetIntoWindowInTicks / this.scaleFactor;
        }

        /// <summary>
        /// Gets the x co-ordinate for a specified date in the view. 
        /// </summary>
        /// <param name="time">Time for which the x co-ordinate is needed.</param>
        /// <returns>X co-ordinate for the specified time.</returns>
        /// <remarks>Unlike GetXForDate(), this method takes into account partial pages in view and returns the x co-ordinate.</remarks>
        internal double GetXForDateInView(DateTime time)
        {
            double position = 0;
            if (this.ScrollBar != null)
            {
                position = this.ScrollBar.Value;
            }

            long ticksInView = time.Subtract(this.AxisStartDate).Ticks;
            return (ticksInView / this.scaleFactor) - position;
        }

        /// <summary>
        /// Gets the X for date in panel.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="panelStartDate">The panel start date.</param>
        /// <returns>X co-ordiante for the date in the current panel.</returns>
        internal double GetXForDateInPanel(DateTime dateTime, DateTime panelStartDate)
        {
            long ticks = dateTime.Subtract(panelStartDate).Ticks;
            return ticks / this.scaleFactor;
        }

        /// <summary>
        /// Gets the Date for a given x co-ordinate.
        /// </summary>
        /// <param name="axisOffset">X co-ordinate value.</param>
        /// <returns>DateTime at the specified X co-ordinate.</returns>
        internal DateTime GetDateForX(double axisOffset)
        {
            return this.AxisStartDate.AddTicks((long)((this.ScrollBar.Value + axisOffset) * this.scaleFactor));
        }        

        /// <summary>
        /// Gets a data bound Graph Point element.
        /// </summary>
        /// <param name="o">Object containing binding data.</param>
        /// <returns>GraphPoint with bound data.</returns>
        internal GraphPoint GetBoundGraphPoint(object o)
        {
#if SILVERLIGHT
            DependencyObject dp = this.PointTemplate.LoadContent();
            if (null == dp)
            {
                return null;
            }

            GraphPoint gp = dp as GraphPoint;
            if (null == gp)
            {
                return null;
            }

            gp.DataContext = o;
#else
            GraphPoint gp = new GraphPoint();
            if (null == this.bindingY1 && null == this.bindingY2 && null == this.bindingX2)
            {
                return null;
            }

            if (null != this.bindingY1)
            {
                Binding b1 = new Binding();
                b1.Mode = BindingMode.OneTime;
                b1.Path = this.bindingY1.ParentBinding.Path;
                b1.Source = o;
                gp.SetBinding(GraphPoint.Y1Property, b1);
            }

            if (null != this.bindingY2)
            {
                Binding b2 = new Binding();
                b2.Mode = BindingMode.OneTime;
                b2.Path = this.bindingY2.ParentBinding.Path;
                b2.Source = o;
                gp.SetBinding(GraphPoint.Y2Property, b2);
            }

            if (null != this.bindingX1)
            {
                Binding b3 = new Binding();
                b3.Mode = BindingMode.OneTime;
                b3.Path = this.bindingX1.ParentBinding.Path;
                b3.Source = o;
                gp.SetBinding(GraphPoint.X1Property, b3);
            }

            if (null != this.bindingX2)
            {
                Binding b4 = new Binding();
                b4.Mode = BindingMode.OneTime;
                b4.Path = this.bindingX2.ParentBinding.Path;
                b4.Source = o;
                gp.SetBinding(GraphPoint.X2Property, b4);
            }

            if (null != this.bindingDataMarkerTemplate)
            {
                Binding b5 = new Binding();
                b5.Mode = BindingMode.OneTime;
                b5.Path = this.bindingDataMarkerTemplate.ParentBinding.Path;
                b5.Source = o;
                gp.SetBinding(GraphPoint.DataMarkerTemplateProperty, b5);
            }

            if (null != this.bindingLabel)
            {
                Binding b6 = new Binding();
                b6.Mode = BindingMode.OneTime;
                b6.Path = this.bindingLabel.ParentBinding.Path;
                b6.Source = o;
                gp.SetBinding(GraphPoint.LabelProperty, b6);
            }

            gp.DataContext = o;
#endif

            return gp;
        }

        /// <summary>
        /// Handles the MouseLeave event of the control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        internal void DataPointMarker_MouseLeave(object sender, MouseEventArgs e)
        {
            this.OnDataMarkerMouseLeave(sender as FrameworkElement);            
        }

        /// <summary>
        /// Handles the MouseEnter event of the control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        internal void DataPointMarker_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.ShowDataPointLabelsOnHover)
            {
                this.OnDataMarkerMouseEnter(sender as FrameworkElement, true);
            }
        }

        /// <summary>
        /// Scrolls the graph to a specified date.
        /// </summary>
        /// <param name="dateTime">DateTime to scroll the graph.</param>
        internal void ScrollToDate(DateTime dateTime)
        {
            double scrollBarValue = this.GetScrollbarValue(dateTime);
            this.ScrollBar.Value = scrollBarValue;
            this.SetViewportDate();
        }

#if SILVERLIGHT
        /// <summary>
        /// Snaps the GraphPoint to the nearest Pixel.
        /// </summary>
        /// <param name="element">Framework to be Snapped to Grid.</param>
        internal void SnapGraphPoint(FrameworkElement element)
        {
            if (element == null || this.DynamicPlotLayer == null)
            {
                return;
            }

            bool snap = GraphBase.GetSnapToPixels(element);
            if (snap == false)
            {
                return;
            }

            // Remove existing transform
            TranslateTransform savedTransform = element.RenderTransform as TranslateTransform;
            if (savedTransform != null)
            {
                element.RenderTransform = null;
            }

            // Calculate actual location
            Point zero = new Point(0, 0);
            MatrixTransform globalTransform = element.TransformToVisual(Application.Current.RootVisual) as MatrixTransform;
            Point p = globalTransform.Matrix.Transform(zero);

            double deltaX = (int)p.X - p.X;
            double deltaY = (int)p.Y - p.Y;

            // Set new transform
            if (deltaX != 0 || deltaY != 0)
            {
                if (savedTransform == null)
                {
                    savedTransform = new TranslateTransform();
                }

                element.RenderTransform = savedTransform;
                savedTransform.X = deltaX;
                savedTransform.Y = deltaY;
            }
        }
#endif

        /// <summary>
        /// Virtual. Plot a time line graph.
        /// </summary>
        internal virtual void DrawTimeGraph()
        {
            // in here do code to draw time based graph ....
            // use the same method as I did for the x axis to find th epoints.
            // the data should be the datacontext for this item.
            // not sure of the shape of the data
            // might be similar to that in the previous code that is checked in.
            // dummmy graph implementation 
            SolidColorBrush brush = new SolidColorBrush(Colors.Blue);

            int number = 20;

            double scaleX = this.DynamicPlotLayerViewport.ActualWidth / number;

            Point p1 = new Point(0, 0);
            Point p2;

            Random rand = new Random();

            for (int counter = 0; counter < number + 1; counter++)
            {
                p2 = p1;
                p1.X = counter * scaleX;
                p1.Y = rand.Next((int)this.DynamicPlotLayerViewport.ActualHeight);
                Path path = new Path();
                path.Stroke = brush;
                path.StrokeThickness = 3;
                LineGeometry geo = new LineGeometry();
                geo.StartPoint = p2;
                geo.EndPoint = p1;
                path.Data = geo;
                this.DynamicPlotLayer.Children.Add(path);
            }
        }       
        
        /// <summary>
        /// Gets the axis dates.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        internal virtual void GetAxisDates(out DateTime? startDate, out DateTime? endDate)
        {
            startDate = endDate = null;

            if (this.DataContext != null && this.VisibleWindow != null)
            {
                startDate = DataHelper.GetFirstDateTime(this.DataContext);
                endDate = DataHelper.GetLastDateTime(this.DataContext);
            }
        }

        /// <summary>
        /// Sets the scroll position.
        /// </summary>
        internal void SetViewportDate()
        {
            if (this.ScrollBar != null && this.VisibleWindow != null)
            {
                if (this.ScrollBar.Maximum == this.ScrollBar.Minimum)
                {
                    this.scrollPosition = -2;
                }
                else if (this.ScrollBar.Value == this.ScrollBar.Maximum)
                {
                    this.scrollPosition = 1;
                }
                else if (this.ScrollBar.Value == this.ScrollBar.Minimum)
                {
                    this.scrollPosition = -1;
                }
                else
                {
                    this.scrollPosition = 0;
                }

                this.viewPortDateTime = this.TitleStartDate.AddTicks(this.VisibleWindow.Ticks / 2);
            }
        }

        /// <summary>
        /// Scrolls to viewport date.
        /// </summary>
        internal void ScrollToViewportDate()
        {
            if (this.scrollPosition == -2)
            {
                this.ScrollToDate(this.NowDateTime);
            }
            else if (this.scrollPosition == -1)
            {
                this.ScrollBar.Value = this.ScrollBar.Minimum;
            }
            else if (this.scrollPosition == 1)
            {
                this.ScrollBar.Value = this.ScrollBar.Maximum;
            }
            else if (this.scrollPosition == 0 && this.viewPortDateTime.HasValue)
            {
                this.ScrollToDate(this.viewPortDateTime.Value);
            }
        }

        /// <summary>
        /// Updates the future shading offset.
        /// </summary>
        internal void UpdateFutureShadingOffset()
        {
            if (this.futureCanvas != null)
            {
                double nowX = this.GetXForDateInView(this.NowDateTime);
                if (nowX <= this.DynamicMainLayerViewport.ActualWidth)
                {
                    this.futureCanvas.Width = this.DynamicMainLayerViewport.ActualWidth - Math.Max(0, nowX);
                    this.futureCanvas.Visibility = Visibility.Visible;
                }
                else
                {
                    this.futureCanvas.Visibility = Visibility.Collapsed;
                }
            }
        }
        #endregion

        #region Property Changed Callbacks (Protected)
        /// <summary>
        /// Graph properties changed callback method.
        /// </summary>
        /// <param name="dependencyObject">DependencyObject for callback.</param>
        /// <param name="args">The arguments.</param>
        protected static void GraphPropertiesChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            TimeGraphBase graph = dependencyObject as TimeGraphBase;
            if (null != graph)
            {
                graph.Refresh(false);
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Adds a separator for Y axis labels.
        /// </summary>
        protected virtual void AddYAxisLabelsSeparator()
        {
            if (!this.Minimized && this.AddYAxisSeparator)
            {
                Line line = new Line();
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 1;
                line.X1 = line.X2 = Math.Abs(this.DynamicMainLayerViewport.Margin.Left - this.NonDynamicRightAxisViewPort.Margin.Left);
                line.Y1 = 0;
                line.Y2 = this.NonDynamicRightAxisViewPort.ActualHeight;

                this.NonDynamicRightAxisLinesLayer.Children.Add(line);
            }
        }

        /// <summary>
        /// Virtual. Plots a time graph with a filtered set of data.
        /// </summary>
        /// <param name="subSet">Filtered set of data.</param>
        /// <param name="panel">The panel.</param>
        protected virtual void DrawFilteredTimeGraph(System.Collections.IEnumerable subSet, PanelWrapper panel)
        {
        }

        /// <summary>
        /// Virtual. process the Point based on the previous value.
        /// </summary>
        /// <param name="previousPoint">Previous Point Plotted.</param>
        /// <param name="currentPoint">Current Point Plotted.</param>
        protected virtual void ProcessPlottedDataMarker(GraphPoint previousPoint, GraphPoint currentPoint)
        {
            // override to process other information
            if (this.Minimized && this.MinimizedPlotLayer != null)
            {
                if (previousPoint.X1Pixel != 0 || previousPoint.Y1Pixel != 0)
                {
                    GraphPoint gp = new GraphPoint();
                    gp.X1Pixel = previousPoint.X1Pixel;
                    gp.X2Pixel = currentPoint.X1Pixel;
                    gp.Y1Pixel = this.NonDynamicRightAxisViewPort.ActualHeight / 2;
                    this.MinimizedPlotLayer.Children.Add(this.GetInterpolationLine(gp));
                }
            }
        }

        /// <summary>
        /// Draws the graph.
        /// </summary>
        /// <param name="panel">The panel.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="panelNumber">The panel number.</param>        
        protected override void DrawPanel(PanelWrapper panel, long offset, int panelNumber)
        {
            if (null == this.VisibleWindow || !this.IsInitialized)
            {
                return;
            }

            this.CurrentWorkingPanelCallback = panelNumber;
            this.DynamicTopAxisLayer = ((Grid)panel.Children[0]).FindName(DynamicTopAxisLayerElementName) as Canvas;
            this.DynamicPlotLayerViewport = ((Grid)panel.Children[0]).FindName(DynamicPlotLayerViewportElementName) as Canvas;
            this.DynamicPlotLayerViewport.SizeChanged += new SizeChangedEventHandler(this.DynamicPlotLayerViewport_SizeChanged);

            this.DynamicPlotLayer = ((Grid)panel.Children[0]).FindName(DynamicPlotLayerElementName) as Canvas;
            this.DynamicTopAxisLayerViewport = ((Grid)panel.Children[0]).FindName(DynamicTopAxisLayerViewportElementName) as Canvas;
            this.MinimizedPlotLayer = ((Grid)panel.Children[0]).FindName(MinimizedPlotLayerElementName) as Canvas;

            this.DynamicPlotLayer.Visibility = this.Minimized ? Visibility.Collapsed : Visibility.Visible;
            if (this.MinimizedPlotLayer != null)
            {
                this.MinimizedPlotLayer.Visibility = this.Minimized ? Visibility.Visible : Visibility.Collapsed;
            }

            CollisionDetectionManager collisionDetectionManager = new CollisionDetectionManager();
            GraphBase.SetCollisionDetectionManager(panel, collisionDetectionManager);
            GraphBase.SetCollisionDetectionManager(this.DynamicPlotLayer, collisionDetectionManager);

            // need to increment the currentWindowStartDate by figuring out the amount of time a 
            // window/page represents, and times that by the number of pages we are from the start.
            // we do this by figuring out the ticks for a window.
            // we then multiply this by the number in the offset.
            long ticks = 0;
            long totalTicks = 0;
            try
            {
                if (null != this.VisibleWindow)
                {
                    ticks = checked(this.VisibleWindow.Ticks * offset);
                    totalTicks = checked(this.AxisStartDate.Ticks + ticks);
                }
            }
            catch (System.OverflowException e)
            {
                // style update.
                if (null != e)
                {
                    totalTicks = DateTime.MaxValue.Ticks + 1;
                }
            }

            ////it is possible to configure the Graph so that we try to go to a date that is beyond that
            ////that can be represented. e.g Window size is 10 years, and goto some huge page offset.
            ////if we do try that, then do nothing.

            if (totalTicks <= DateTime.MaxValue.Ticks)
            {
                this.CurrentWindowStartDate = this.AxisStartDate.AddTicks(ticks);
                panel.StartDate = this.CurrentWindowStartDate;
                panel.EndDate = panel.StartDate.AddTicks(this.VisibleWindow.Ticks);
                this.DynamicPlotLayer.Children.Clear();
                this.DynamicTopAxisLayer.Children.Clear();                

                // when we draw the graph, we want to plot first because the overlap 
                // detection does not want to have grid lines to worry about.
                if (this.DataContext != null)
                {
                    this.InvalidDateSelected = false;

                    DateTime tempDate = this.CurrentWindowStartDate.AddTicks(this.VisibleWindow.Ticks);
                    IEnumerable enumerable = this.GetFilteredData(this.CurrentWindowStartDate, tempDate);
                    if (enumerable != null)
                    {
                        this.DrawFilteredTimeGraph(enumerable, panel);
                    }
                    else
                    {
                        this.DrawTimeGraph();
                    }
                }

                this.DrawTimeXAxis(offset);
            }            
        }

        /// <summary>
        /// Updates the non dynamic layers.
        /// </summary>
        protected override void UpdateNonDynamicLayers()
        {
            base.UpdateNonDynamicLayers();
            if (!this.IsInitialized)
            {
                this.NonDynamicRightAxisLabelsLayer.Children.Clear();
                this.NonDynamicRightAxisLinesLayer.Children.Clear();

                this.InitializeGraph();               
                this.AddYAxisLabelsSeparator();
            }
        }

        /// <summary>
        /// Initializes the graph.
        /// </summary>
        protected virtual void InitializeGraph()
        {
            if (this.VisibleWindow == null)
            {
                this.VisibleWindow = StyleParser.FindResource<TimeFrequency>(this.LayoutRoot, DefaultVisibleWindowResourceKeyName, false);
            }

            if (null != this.VisibleWindow)
            {
                this.scaleFactor = this.VisibleWindow.Ticks / (this.DynamicMainLayerViewport.ActualWidth > 0 ? this.DynamicMainLayerViewport.ActualWidth : 100);
                this.frequency = AxisHelper.GetAxisDesign(this.VisibleWindow.Ticks);
            }            
                        
            // Axes dates will be calculated only when used as independent controls. 
            // If used in GraphHost, graph host will be setting the dates.
            if (this.DetermineAxisDates)
            {
                this.SetAxisDates();
            }

            if (null != this.VisibleWindow && this.AxisStartDate.AddTicks(this.VisibleWindow.Ticks) <= this.AxisEndDate)
            {
                double numberOfPixelsInWindows = (this.AxisEndDate - this.AxisStartDate).Ticks / this.scaleFactor;

                this.ScrollBar.SmallChange = 1;
                this.ScrollBar.Maximum = numberOfPixelsInWindows - this.DynamicMainLayerViewport.ActualWidth > this.ScrollBar.SmallChange ? numberOfPixelsInWindows - this.DynamicMainLayerViewport.ActualWidth : 0; // substracting viewport width as one page is already being shown.                
                this.ScrollBar.LargeChange = this.VisibleWindow.Ticks / this.scaleFactor;
                this.ScrollBar.ViewportSize = this.DynamicMainLayerViewport.ActualWidth;
            }
            else
            {
                this.ScrollBar.Maximum = 0;
                this.ScrollBar.Minimum = 0;
            }

            this.RefreshTitle();

            if (this.DesignMode)
            {
                if (this.NonDynamicRightAxisLabelsLayer.Children.Contains(this.designModeInformationBlock))
                {
                    this.designModeInformationBlock = this.NonDynamicRightAxisLabelsLayer.Children[this.NonDynamicRightAxisLabelsLayer.Children.IndexOf(this.designModeInformationBlock)] as TextBlock;
                }
                else
                {
                    this.designModeInformationBlock = new TextBlock();
                    this.designModeInformationBlock.TextWrapping = TextWrapping.Wrap;
                    this.designModeInformationBlock.FontSize = 14;
                    this.designModeInformationBlock.FontWeight = FontWeights.Bold;
                    this.NonDynamicRightAxisLabelsLayer.Children.Add(this.designModeInformationBlock);
                }

                if (string.IsNullOrEmpty(this.Title))
                {
                    this.Title = GraphingResources.DesignModeTitleLabel;
                }

                if (this.VisibleWindow == null)
                {
                    this.designModeInformationBlock.Text = GraphingResources.DesignModeInvalidWindow;
                }
                else if (this.DataContext == null)
                {
                    this.designModeInformationBlock.Text = GraphingResources.DesignModeInvalidDataContext;
                }
            }
            else
            {
                if (this.NonDynamicRightAxisLabelsLayer.Children.Contains(this.designModeInformationBlock))
                {
                    this.NonDynamicRightAxisLabelsLayer.Children.Remove(this.designModeInformationBlock);
                }
            }

            this.SetGraphSummary();
            if (this.TitleArea != null)
            {
                this.TitleArea.DataContext = this.GraphSummary;
            }            

            this.IsInitialized = true;
        }       

        /// <summary>
        /// Overridden. Updates the dates in graph title based on the current viewport.
        /// </summary>
        protected override void OnHorizontalScrollbarValueChanged()
        {
            base.OnHorizontalScrollbarValueChanged();
            this.RefreshTitle();
        }

        /// <summary>
        /// Handles the keypressed event on the control.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            bool ctrlKeyPressed;
            bool shiftKeyPressed;
            bool altKeyPressed;

            KeyboardHelper.GetMetaKeyState(out ctrlKeyPressed, out shiftKeyPressed, out altKeyPressed);

            if (e.Handled == false)
            {
                switch (e.Key)
                {
                    case Key.Left:
                        if (ctrlKeyPressed && !shiftKeyPressed && !altKeyPressed)
                        {
                            this.ScrollBar.Value -= this.ScrollBar.LargeChange;
                            e.Handled = true;
                        }
                        else
                        {
                            this.ScrollBar.Value -= this.ScrollBar.SmallChange;
                            e.Handled = true;
                        }

                        break;
                    case Key.Right:
                        if (ctrlKeyPressed && !shiftKeyPressed && !altKeyPressed)
                        {
                            this.ScrollBar.Value += this.ScrollBar.LargeChange;
                            e.Handled = true;
                        }
                        else
                        {
                            this.ScrollBar.Value += this.ScrollBar.SmallChange;
                            e.Handled = true;
                        }

                        break;
                    case Key.Up:
                        if (ctrlKeyPressed && !shiftKeyPressed && !altKeyPressed)
                        {
                            this.PageUp();
                            e.Handled = true;
                        }
                        else
                        {
                            this.VerticalScrollBar.Value -= this.VerticalScrollBar.SmallChange;
                            e.Handled = true;
                        }
                        
                        break;
                    case Key.Down:
                        if (ctrlKeyPressed && !shiftKeyPressed && !altKeyPressed)
                        {
                            this.PageDown();
                            e.Handled = true;
                        }
                        else
                        {
                            this.VerticalScrollBar.Value += this.VerticalScrollBar.SmallChange;
                            e.Handled = true;
                        }

                        break;
                    case Key.PageDown:
                        this.PageDown();
                        e.Handled = true;
                        break;
                    case Key.PageUp:
                        this.PageUp();
                        e.Handled = true;
                        break;
                    case Key.Home:
                        if (ctrlKeyPressed)
                        {                            
                            this.MoveTop();
                            e.Handled = true;
                        }

                        break;
                    case Key.End:
                        if (ctrlKeyPressed)
                        {                            
                            this.MoveBottom();
                            e.Handled = true;
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Moves the vertical scrollbar to top.
        /// </summary>
        protected void MoveTop()
        {
            this.VerticalScrollBar.Value = 0;
        }

        /// <summary>
        /// Moves the vertical scrollbar to bottom.
        /// </summary>
        protected void MoveBottom()
        {
            this.VerticalScrollBar.Value = this.VerticalScrollBar.Maximum;
        }

        /// <summary>
        /// Moves vertical scrollbar up by a page.
        /// </summary>
        protected void PageUp()
        {
            this.VerticalScrollBar.Value -= this.VerticalScrollBar.LargeChange;
        }

        /// <summary>
        /// Moves vertical scrollbar down by a page.
        /// </summary>
        protected void PageDown()
        {
            this.VerticalScrollBar.Value += this.VerticalScrollBar.LargeChange;
        }

        /// <summary>
        /// Gets the interpolation line for the minimized plot layer.
        /// </summary>
        /// <param name="gp">The GraphPoint containig the co-ordinates.</param>
        /// <returns>Returns the interpolation line.</returns>
        protected FrameworkElement GetInterpolationLine(GraphPoint gp)
        {
            Shape interpolationLine = null;
            DataTemplate template = null;
            if (this.LayoutRoot.Resources.Contains(MinimizedViewInterpolationLineElementName))
            {
                template = this.LayoutRoot.Resources[MinimizedViewInterpolationLineElementName] as DataTemplate;
                if (template != null)
                {
                    interpolationLine = template.LoadContent() as Shape;
                    interpolationLine.DataContext = gp;
                }
            }

            if (interpolationLine == null || template == null)
            {
                throw new ArgumentException(GraphingResources.InterpolationLineTemplateNotFound);
            }

            Line minimizedLine = interpolationLine as Line;
            if (minimizedLine != null)
            {
                minimizedLine.X1 = Math.Max(0, gp.X1Pixel);
                minimizedLine.X2 = Math.Min(gp.X2Pixel, this.DynamicMainLayerViewport.ActualWidth);

                if (gp.X1X2Pixel < interpolationLine.StrokeThickness)
                {
                    minimizedLine.X1 = gp.X1Pixel;
                    minimizedLine.X2 = gp.X1Pixel + interpolationLine.StrokeThickness;
                }
            }

            return interpolationLine;
        }
        
        /// <summary>
        /// Sets the start and end dates of the x axis.
        /// </summary>
        protected virtual void SetAxisDates()
        {
            DateTime? dataStartDate;
            DateTime? dataEndDate;
            this.GetAxisDates(out dataStartDate, out dataEndDate);

            if (dataStartDate.HasValue && dataEndDate.HasValue)
            {                
                DateTime graphAxisStartDate = AxisHelper.GetAxisStartDate(dataStartDate.Value, this.frequency);
                DateTime graphAxisEndDate = AxisHelper.GetAxisEndDate(dataEndDate.Value, this.frequency);
                AxisHelper.AdjustAxisStartDate(ref graphAxisStartDate, ref graphAxisEndDate, this.frequency);

                this.AxisStartDate = graphAxisStartDate;
                this.AxisEndDate = graphAxisEndDate;
            }
        }

        /// <summary>
        /// Gets the X axis grid line.
        /// </summary>
        /// <param name="axisOffset">The offset in the x axis.</param>
        /// <returns>Line for the X axis at specified horizontal offset.</returns>
        protected virtual LineGeometry GetXAxisGridLine(double axisOffset)
        {
            LineGeometry geo = new LineGeometry();
            geo.StartPoint = new Point(axisOffset, 0);
            geo.EndPoint = new Point(axisOffset, this.DynamicMainLayerViewport.ActualHeight);

            return geo;
        }

        /// <summary>
        /// Sets the summary of the graph.
        /// </summary>
        protected virtual void SetGraphSummary()
        {
            this.GraphSummary.Background = this.Background;
            this.GraphSummary.HoverBackground = this.HoverBackground;
            this.GraphSummary.Title = this.Title;            
        }

        /// <summary>
        /// Processes the graph points.
        /// </summary>
        protected virtual void ProcessGraphPoints()
        {
        }

        /// <summary>
        /// Gets the last data point value in the data series.
        /// </summary>
        /// <returns>Last data points in the data series.</returns>
        protected object[] GetLastDataPoints()
        {
            if (this.previousDataContext != this.DataContext || this.lastDataPoints == null)
            {
                this.lastDataPoints = new object[2];

                // Ensure the colleciton is sorted and get the last 2 points.
                IEnumerable enumerable = this.DataContext as IEnumerable;
                if (null == enumerable)
                {
                    enumerable = DataHelper.Filter(this.DataContext, DateTime.MinValue, DateTime.MaxValue);
                }

                // Should be valid now as it was either of correct type, or has been sorted ionto correct type.
                if (null != enumerable)
                {
                    List<object> timePoints = enumerable.Cast<object>().ToList<object>();
                    if (timePoints.Count > 0)
                    {
                        timePoints.Sort();
                        if (timePoints.Count > 1)
                        {
                            this.lastDataPoints[0] = timePoints[timePoints.Count - 2];
                            this.lastDataPoints[1] = timePoints[timePoints.Count - 1];
                        }
                        else
                        {
                            this.lastDataPoints[0] = null;
                            this.lastDataPoints[1] = timePoints[timePoints.Count - 1];
                        }
                    }
                }

                this.previousDataContext = this.DataContext;
            }

            return this.lastDataPoints;
        }

        /// <summary>
        /// Overridden. Called when [size changed].
        /// </summary>
        protected override void OnSizeChanged()
        {
            base.OnSizeChanged();
            if (this.RetainViewportDateOnSizeChange)
            {
                this.ScrollToViewportDate();
            }
        }
        #endregion

        #region Property Changed Callbacks(Private)
        /// <summary>
        /// Graph layout properties changed callback method.
        /// </summary>
        /// <param name="dependencyObject">DependencyObject for callback.</param>
        /// <param name="args">The arguments.</param>
        private static void GraphLayoutChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            TimeGraphBase graph = dependencyObject as TimeGraphBase;
            if (null != graph)
            {
                graph.Refresh(true);
            }
        }

        /// <summary>
        /// LabelVisiblityCallback method.
        /// </summary>
        /// <param name="dependencyObject">DependencyObject for callback.</param>
        /// <param name="args">The arguments.</param>
        private static void LabelVisibilityCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            TimeGraphBase graph = dependencyObject as TimeGraphBase;
            if (null != graph)
            {
                graph.InvalidateMeasure();
                graph.Refresh(false);
            }
        }

        /// <summary>
        /// GridLinesVisibilityCallback method.
        /// </summary>
        /// <param name="dependencyObject">DependencyObject for callback.</param>
        /// <param name="args">The arguments.</param>
        private static void GridLinesVisibilityCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            TimeGraphBase graph = dependencyObject as TimeGraphBase;
            if (null != graph)
            {
                // as initializing preserve the vertical scroll value
                double? vertPosition = null;
                if (graph.VerticalScrollBar != null)
                {
                    vertPosition = graph.VerticalScrollBar.Value;
                }

                graph.Refresh(true);

                if (vertPosition.HasValue)
                {
                    graph.VerticalScrollBar.Value = vertPosition.Value;
                }
            }
        }

        /// <summary>
        /// Minimized property changed callback.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void MinimizedPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            TimeGraphBase graph = dependencyObject as TimeGraphBase;
            if (null != graph)
            {                
                graph.InvalidateMeasure();
                graph.Refresh();

                if (graph.Minimized)
                {
                    VisualStateManager.GoToState(graph, "Minimized", true);
                }
                else
                {
                    VisualStateManager.GoToState(graph, "Maximized", true);
                }
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the scrollbar value.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>Value for the datetime in the scrollbar.</returns>
        private double GetScrollbarValue(DateTime dateTime)
        {
            double scrollBarValue = (dateTime.Ticks - this.AxisStartDate.Ticks) / this.scaleFactor;
            scrollBarValue -= this.DynamicMainLayerViewport.ActualWidth / 2;

            if (scrollBarValue > this.ScrollBar.Maximum)
            {
                scrollBarValue = this.ScrollBar.Maximum;
            }
            else if (scrollBarValue < 0)
            {
                scrollBarValue = this.ScrollBar.Minimum;
            }

            return scrollBarValue;
        }
       
        /// <summary>
        /// Plots the time text block.
        /// </summary>
        /// <param name="block">The block element.</param>
        /// <param name="x">The x value.</param>        
        /// <param name="showTicks">Boolean indicating whether to show ticks.</param>
        private void PlotTimeTextBlock(TextBlock block, double x, bool showTicks)
        {
            double top = 0;
            double tickLength = block != null ? this.MajorAxisTickLength : this.MinorAxisTickLength;
            if (showTicks)
            {
                Line axisTick = new Line();
                axisTick.Stroke = this.TickLineBrush;
                axisTick.StrokeThickness = this.TickLineThickness;

                if (this.PlotTicksBeforeLabels)
                {
                    axisTick.Y1 = 0;
                    axisTick.Y2 = tickLength;
                }
                else
                {
                    axisTick.Y1 = this.DynamicTopAxisLayerViewport.Height;
                    axisTick.Y2 = this.DynamicTopAxisLayerViewport.Height - tickLength;
                }

                axisTick.X1 = axisTick.X2 = x;

                this.DynamicTopAxisLayer.Children.Add(axisTick);

                top = axisTick.Y2;
            }

            if (block != null)
            {
                block.SetValue(Canvas.LeftProperty, (x - (TextBlockHelper.GetDesiredWidth(block) / 2)));

                if (!this.PlotTicksBeforeLabels)
                {
                    top -= TextBlockHelper.GetDesiredHeight(block);
                }

                block.SetValue(Canvas.TopProperty, top);
                this.DynamicTopAxisLayer.Children.Add(block);
            }
        }

        /// <summary>
        /// Renders the Time Axis.
        /// </summary>
        /// <param name="offset">Offset number of the graph page.</param>
        private void DrawTimeXAxis(long offset)
        {
            // get an axis design for this time window (the subtraction of a single ticks ensure there is no overlap)
            DateTime minVisualTime = this.AxisStartDate.AddTicks(this.VisibleWindow.Ticks * offset);
            DateTime maxVisualTime = this.AxisStartDate.AddTicks((this.VisibleWindow.Ticks * (offset + 1)) - 1);

            DateTime majorTime, minorTime;
            long ticks;

            switch (this.frequency.Unit)
            {
                case TimeFrequency.TimeUnit.Year:
                case TimeFrequency.TimeUnit.Month:
                    // calculate the major start date
                    majorTime = this.AxisStartDate;
                    while (majorTime < minVisualTime)
                    {
                        ticks = AxisHelper.GetTickIncrease(this.frequency.MajorInterval, majorTime, this.frequency.MajorUnit, this.frequency.MajorValue);
                        majorTime = majorTime.AddTicks(ticks);
                    }

                    // calculate the minor start date
                    minorTime = this.AxisStartDate;
                    if (this.frequency.MinorUnit == TimeFrequency.TimeUnit.Week)
                    {
                        // re-align as the week must start on Monday
                        TimeSpan tt = minorTime.Subtract(DateTime.MinValue);
                        int days = (int)tt.TotalDays;
                        days -= (days % 7);
                        minorTime = DateTime.MinValue.Add(new TimeSpan(days, 0, 0, 0));
                    }

                    while (minorTime < minVisualTime)
                    {
                        ticks = AxisHelper.GetTickIncrease(this.frequency.MinorInterval, minorTime, this.frequency.MinorUnit, this.frequency.MinorValue);
                        minorTime = minorTime.AddTicks(ticks);
                    }

                    break;
                case TimeFrequency.TimeUnit.Week:
                case TimeFrequency.TimeUnit.Day:
                case TimeFrequency.TimeUnit.Hour:
                case TimeFrequency.TimeUnit.Minute:
                case TimeFrequency.TimeUnit.Second:
                default:
                    // we know the major and minor time units are intervals of each other so optimize
                    majorTime = minVisualTime;
                    minorTime = majorTime;

                    break;
            }

            // plot axis whilst the date does not increase by the number of ticks visible for the window
            while (majorTime <= maxVisualTime)
            {
                this.TryDrawVerticalAxisLine(majorTime, this.frequency, true);

                ticks = AxisHelper.GetTickIncrease(this.frequency.MajorInterval, majorTime, this.frequency.MajorUnit, this.frequency.MajorValue);
                majorTime = majorTime.AddTicks(ticks);

                // now look and see what minor intervals need plotting
                while (minorTime <= majorTime && minorTime <= maxVisualTime)
                {
                    // Ensure not plotting at the same time
                    if (minorTime != majorTime)
                    {
                        this.TryDrawVerticalAxisLine(minorTime, this.frequency, false);
                    }

                    ticks = AxisHelper.GetTickIncrease(this.frequency.MinorInterval, minorTime, this.frequency.MinorUnit, this.frequency.MinorValue);
                    minorTime = minorTime.AddTicks(ticks);
                }
            }

            this.CurrentWindowEndDate = maxVisualTime;
        }        
        
        /// <summary>
        /// Renders the X Axis line.
        /// </summary>
        /// <param name="time">Time of the axis.</param>
        /// <param name="frequency">Frequency of the axis.</param>
        /// <param name="majorInterval">Value indicating whether the current axis is major interval.</param>
        /// <returns>A value indicating whether the rendering was successful.</returns>
        private bool TryDrawVerticalAxisLine(DateTime time, TimeFrequency frequency, bool majorInterval)
        {
            // thise code is for the lines on the Dynamic pLotting area. They extend up for scolling, and need to reach to max y
            // value.
            double x = this.GetXForDate(time);
            if (this.ShowGridLines == GridLineVisibility.Both || this.ShowGridLines == GridLineVisibility.Vertical)
            {
                if (majorInterval || (this.ShowMinorGridLines == GridLineVisibility.Vertical || this.ShowMinorGridLines == GridLineVisibility.Both))
                {
                    Path path = new Path();
                    path.Stroke = this.GridLineBrush;
                    path.StrokeThickness = this.GridLineThickness;
                    LineGeometry geo = this.GetXAxisGridLine(x);
                    path.Data = geo;

                    if (!this.Minimized)
                    {
                        this.DynamicPlotLayer.Children.Add(path);
                    }
                    else
                    {
                        this.MinimizedPlotLayer.Children.Add(path);
                    }
                }
            }

            TextBlock axisLabel = null;
            if (majorInterval)
            {
                axisLabel = this.xaxisLabelTemplate.LoadContent() as TextBlock;
                axisLabel.FontFamily = this.FontFamily;
                if (axisLabel != null)
                {
                    axisLabel.Text = time.ToString(frequency.LabelFormat, CultureInfo.CurrentCulture);
                }
            }

            bool showTicks = this.CanShowXAxisTicks(majorInterval);
            this.PlotTimeTextBlock(axisLabel, x, showTicks);
            return true;
        }
       
        /// <summary>
        /// Gets a value indicating whether the ticks can be shown for the current X axis interval.
        /// </summary>
        /// <param name="majorInterval">Boolean indicating whether the current interval is a major interval.</param>
        /// <returns>Value indicating whether or not tick should be shown.</returns>
        private bool CanShowXAxisTicks(bool majorInterval)
        {
            bool showTicks = false;

            if (majorInterval)
            {
                showTicks = true;                
            }
            else
            {
                if (this.ShowXAxisMinorIntervalTicks == true)
                {
                    showTicks = true;
                }
            }

            return showTicks;
        }
        
        /// <summary>
        /// Refreshes the start and end dates in the title.
        /// </summary>
        private void RefreshTitle()
        {
            if (null != this.VisibleWindow && null != this.frequency)
            {
                this.TitleStartDate = this.AxisStartDate.AddTicks((long)(this.ScrollBar.Value * this.scaleFactor));
                this.TitleEndDate = this.TitleStartDate.AddTicks(this.VisibleWindow.Ticks);
                this.TitleStartDateTextBlock.Text = this.TitleStartDate.ToString(this.frequency.TitleFormat, System.Globalization.CultureInfo.CurrentCulture);
                this.TitleEndDateTextBlock.Text = this.TitleEndDate.ToString(this.frequency.TitleFormat, System.Globalization.CultureInfo.CurrentCulture);
            }

            if (this.AutoUpdateFutureShading)
            {
                this.UpdateFutureShadingOffset();
            }
        }       
        #endregion

        #region Event Handlers
        /// <summary>
        /// Calls the Show Arrow when viewport changes.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        private void DynamicPlotLayerViewport_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.ProcessGraphPoints();
        }
        #endregion
    }
}
