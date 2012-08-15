//-----------------------------------------------------------------------
// <copyright file="TimeAndYGraphBase.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>27-Oct-2009</date>
// <summary>Time and Y graph base class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using Microsoft.Cui.Controls.GraphingControl;

    /// <summary>
    /// Base class for Time axis and numerical Y axis graphs.
    /// </summary>
    public class TimeAndYGraphBase : TimeGraphBase
    {
        #region Dependency Properties
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeAndYGraphBase.YAxisMajorInterval"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty YAxisMajorIntervalProperty = DependencyProperty.Register("YAxisMajorInterval", typeof(double), typeof(TimeAndYGraphBase), new PropertyMetadata(new PropertyChangedCallback(GraphLayoutChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeAndYGraphBase.YAxisMinorInterval"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty YAxisMinorIntervalProperty = DependencyProperty.Register("YAxisMinorInterval", typeof(double), typeof(TimeAndYGraphBase), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeAndYGraphBase.YAxisPosition"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty YAxisPositionProperty = DependencyProperty.Register("YAxisPosition", typeof(YAxisPosition), typeof(TimeAndYGraphBase), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeAndYGraphBase.NormalRangeDescription"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty NormalRangeDescriptionProperty =
            DependencyProperty.Register("NormalRangeDescription", typeof(string), typeof(TimeAndYGraphBase), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeAndYGraphBase.Units" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty UnitsProperty = DependencyProperty.Register("Units", typeof(string), typeof(TimeAndYGraphBase), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeAndYGraphBase.UnitsDescription" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty UnitsDescriptionProperty = DependencyProperty.Register("UnitsDescription", typeof(string), typeof(TimeAndYGraphBase), null);

        #endregion

        #region Private Members
        /// <summary>
        /// Member variable to hold number of minor intervals in a major interval on y axis.
        /// </summary>
        private int yaxisMinorIntervalsInMajorInterval = 2;

        /// <summary>
        /// Member variable to hold templates for Y axis labels.
        /// </summary>
        private DataTemplate yaxisLabelTemplate;

        /// <summary>
        /// Member variable to hold the min value of the data.
        /// </summary>
        private double dataSeriesMinValue;

        /// <summary>
        /// Member variable to hold the max value of the data.
        /// </summary>
        private double dataSeriesMaxValue;

        /// <summary>
        /// Member variable to hold computed YAxisMinValue.
        /// </summary>
        private double actualYAxisMinValue;

        /// <summary>
        /// Member variable to hold computed YAxisMaxValue.
        /// </summary>
        private double actualYAxisMaxValue;

        /// <summary>
        /// Member variable to indicate whether the horizontal scrollbar was initialized once.
        /// </summary>
        private bool horizontalScrollBarInitialized;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeAndYGraphBase"/> class.
        /// </summary>
        protected TimeAndYGraphBase()
        {
            this.YAxisIntervalMinimumHeight = 20;
            this.PlotData = true;
            this.YAxisMaxValue = double.NaN;
            this.YAxisMinValue = double.NaN;
            this.YAxisPadding = new Thickness(5);
            this.YAxisMajorInterval = 10;
            this.ShowAxisEndDateOnLoad = true;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether to show the normal range.
        /// </summary>
        /// <value>Boolean indicating whether to show the normal range.</value>
        [Category("Graph Appearance")]
        public bool ShowNormalRange
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the minimum value for normal range.
        /// </summary>
        /// <value>Minimum value of the normal range.</value>
        [Category("Graph Appearance")]
        public double NormalRangeMinimumValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the maximum value for the normal range.
        /// </summary>
        /// <value>Maximum value of the normal range.</value>
        [Category("Graph Appearance")]
        public double NormalRangeMaximumValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the brush used to fill normal range.
        /// </summary>
        /// <value>Brush used to paint normal range.</value>
        [Category("Graph Appearance")]
        public Brush NormalRangeBrush
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the major interval for Y axis.
        /// </summary>
        /// <value>Major interval for Y axis.</value>
        [Category("Axis Details")]
        public double YAxisMajorInterval
        {
            get { return (double)this.GetValue(YAxisMajorIntervalProperty); }
            set { this.SetValue(YAxisMajorIntervalProperty, value); }
        }

        /// <summary>
        /// Gets or sets the number of minor intervals in a major interval in Y axis.
        /// </summary>
        /// <value>Number of minor intervals present in a major interval on Y axis.</value>
        [Category("Axis Details")]
        public int YAxisMinorIntervalsCountInMajorInterval
        {
            get { return this.yaxisMinorIntervalsInMajorInterval; }
            set { this.yaxisMinorIntervalsInMajorInterval = value; }
        }

        /// <summary>
        /// Gets the minor interval value for Y axis.
        /// </summary>
        /// <value>Minor interval value for Y axis.</value>
        [Category("Axis Details")]
        public double YAxisMinorInterval
        {
            get { return (double)this.GetValue(YAxisMinorIntervalProperty); }
            internal set { this.SetValue(YAxisMinorIntervalProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value for minimum height for an interval in Y axis.
        /// </summary>
        /// <value>Minimum height for an interval in Y axis.</value>
        [Category("Axis Details")]
        public double YAxisIntervalMinimumHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the minimum value of the Y axis.
        /// </summary>
        /// <value>Minimum value of the Y axis.</value>
        [Category("Axis Details")]
        public double YAxisMinValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the maximum value of the Y axis.
        /// </summary>
        /// <value>Maximum value of the Y axis.</value>
        [Category("Axis Details")]
        public double YAxisMaxValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the padding to be applied for Y axis.
        /// </summary>
        /// <value>Padding to be applied for Y axis.</value>        
        [Category("Axis Details")]
        public Thickness YAxisPadding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the DataTemplate for the data marker(Symbol).
        /// </summary>
        /// <value>DataTemplate for the symbol.</value>
        [Category("Customization")]
        public DataTemplate DataMarkerTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to plot the data points.
        /// </summary>
        /// <value>Value indicating whether to plot the data points.</value>
        [Category("Graph Appearance")]
        public bool PlotData
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the YAxis min value plotted in the graph.
        /// </summary>
        /// <value>Actual YAxis minimum value plotted in the graph.</value>
        /// <remarks>This is a computed value and may vary from YAxisMinValue.</remarks>
        [Category("Axis Details")]
        public double ActualYAxisMinValue
        {
            get
            {
                return this.actualYAxisMinValue;
            }
        }

        /// <summary>
        /// Gets the YAxis max value plotted in the graph.
        /// </summary>
        /// <value>Actual YAxis maximum value plotted in the graph.</value>
        /// <remarks>This is a computed value and may vary from YAxisMaxValue.</remarks>
        [Category("Axis Details")]
        public double ActualYAxisMaxValue
        {
            get
            {
                return this.actualYAxisMaxValue;
            }
        }

        /// <summary>
        /// Gets or sets the Y axis position.
        /// </summary>
        /// <value>The Y axis position.</value>
        [Category("Customization")]
        public YAxisPosition YAxisPosition
        {
            get { return (YAxisPosition)this.GetValue(YAxisPositionProperty); }
            set { this.SetValue(YAxisPositionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the normal range description.
        /// </summary>
        /// <value>The normal range description.</value>
        [Category("Graph Appearance")]
        public string NormalRangeDescription
        {
            get { return (string)GetValue(NormalRangeDescriptionProperty); }
            set { SetValue(NormalRangeDescriptionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Arrow Up button.
        /// </summary>
        /// <value>Arrow Up Button.</value>
        [Category("Customization")]
        public Button ArrowButtonUp
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Arrow Down button.
        /// </summary>
        /// <value>Arrow Down Button.</value>
        [Category("Customization")]
        public Button ArrowButtonDown
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the units for the graph.
        /// </summary>
        /// <value>Units for the graph.</value>
        [Category("Graph Appearance")]
        public string Units
        {
            get { return (string)this.GetValue(UnitsProperty); }
            set { this.SetValue(UnitsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the description for the units.
        /// </summary>
        /// <value>Description for the units.</value>
        [Category("Graph Appearance")]
        public string UnitsDescription
        {
            get { return (string)this.GetValue(UnitsDescriptionProperty); }
            set { this.SetValue(UnitsDescriptionProperty, value); }
        }
        #endregion

        #region Overrides

        /// <summary>
        /// Overridden. Applies the specified template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.VerticalScrollBar.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.VerticalScrollBar_ValueChanged);
            this.VerticalScrollBar.Scroll += new ScrollEventHandler(this.VerticalScrollBar_Scroll);
            this.NonDynamicRightAxisViewPort.SizeChanged += new SizeChangedEventHandler(this.NonDynamicRightAxisViewPort_SizeChanged);
            this.ArrowButtonUp = StyleParser.FindName<Button>(this.LayoutRoot, ArrowButtonUpElementName, false);
            this.ArrowButtonDown = StyleParser.FindName<Button>(this.LayoutRoot, ArrowButtonDownElementName, false);

            if (this.ArrowButtonUp != null)
            {
                this.ArrowButtonUp.Click += new RoutedEventHandler(this.ArrowButtonUp_Click);
            }

            if (this.ArrowButtonDown != null)
            {
                this.ArrowButtonDown.Click += new RoutedEventHandler(this.ArrowButtonDown_Click);
            }

            if (this.DataMarkerTemplate == null)
            {
                this.DataMarkerTemplate = StyleParser.FindResource<DataTemplate>(this.LayoutRoot, DataMarkerResourceKey, true);
            }

            this.yaxisLabelTemplate = StyleParser.FindResource<DataTemplate>(this.LayoutRoot, YAxisLabelTemplateElementName, true);
        }

        /// <summary>
        /// Updates the non dynamic layers.
        /// </summary>
        protected override void UpdateNonDynamicLayers()
        {
            if (false == this.IsInitialized)
            {
                this.NonDynamicRightAxisLabelsLayer.Children.Clear();
                this.NonDynamicRightAxisLinesLayer.Children.Clear();
                this.InitializeGraph();
                
                if (this.ShowNormalRange)
                {
                    this.AddNormalRange();
                }

                this.DrawYAxis();
                this.GetLastPointToView();
                this.AddYAxisLabelsSeparator();

                if (!this.horizontalScrollBarInitialized && this.ShowAxisEndDateOnLoad)
                {
                    // Sets the scrollbar to max only on load for the first time.
                    this.horizontalScrollBarInitialized = true;
                    this.ScrollBar.Value = this.ScrollBar.Maximum;
                }
            }
        }

        /// <summary>
        /// Renders the Y axis.
        /// </summary>
        protected virtual void DrawYAxis()
        {
            double position;
            for (double i = this.actualYAxisMinValue; i <= this.actualYAxisMaxValue; i += this.YAxisMajorInterval)
            {
                // plot major axis
                position = i;
                this.DrawYAxisMarker(position, true);

                // now plot the neccessary minors
                for (int j = 1; j < this.YAxisMinorIntervalsCountInMajorInterval; j++)
                {
                    position += this.YAxisMinorInterval;
                    if (position <= this.actualYAxisMaxValue)
                    {
                        this.DrawYAxisMarker(position, false);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Positions the vertical layers.
        /// </summary>
        /// <param name="offset">The offset.</param>
        protected void PositionVerticalLayers(double offset)
        {
            if (null != this.CurrentPanel1)
            {
                Canvas dynamicPlotLayer = ((Grid)this.CurrentPanel1.Children[0]).FindName(DynamicPlotLayerElementName) as Canvas;
                Canvas dynamicPlotLayer2 = ((Grid)this.CurrentPanel2.Children[0]).FindName(DynamicPlotLayerElementName) as Canvas;
                Canvas.SetTop(dynamicPlotLayer, offset * -1);
                Canvas.SetTop(dynamicPlotLayer2, offset * -1);
                Canvas.SetTop(this.NonDynamicRightAxisLabelsLayer, offset * -1);
                Canvas.SetTop(this.NonDynamicRightAxisLinesLayer, offset * -1);

                double width = this.DynamicMainLayerViewport.ActualWidth;
                double height = this.DynamicMainLayerViewport.ActualHeight;

                if (null != this.ArrowButtonUp)
                {
                    Size size = GetArrowButtonSize(this.ArrowButtonUp);
                    Canvas.SetTop(this.ArrowButtonUp, 2);
                    Canvas.SetLeft(this.ArrowButtonUp, Math.Max(0, (width / 2) - (size.Width / 2)));
                }

                if (null != this.ArrowButtonDown)
                {
                    Size size = GetArrowButtonSize(this.ArrowButtonDown);
                    Canvas.SetTop(this.ArrowButtonDown, Math.Max(0, height - size.Height - 2));
                    Canvas.SetLeft(this.ArrowButtonDown, Math.Max(0, (width / 2) - (size.Width / 2)));
                }

                this.ProcessGraphPoints();
            }
        }

        /// <summary>
        /// Gets the symbol for the datapoint.
        /// </summary>
        /// <param name="graphPoint">Start point of the symbol.</param>
        /// <returns>A symbol at a specified position.</returns>
        protected virtual FrameworkElement GetPlottedDataMarker(GraphPoint graphPoint)
        {
            FrameworkElement dataMarkerTemplate = this.DataMarkerTemplate.LoadContent() as FrameworkElement;

            if (dataMarkerTemplate == null)
            {
                throw new ArgumentException(GraphingResources.GraphDisplayDataMarkerError);
            }

            Canvas.SetLeft(dataMarkerTemplate, graphPoint.X1Pixel + GraphBase.GetXOffset(dataMarkerTemplate));
            Canvas.SetTop(dataMarkerTemplate, graphPoint.Y1Pixel + GraphBase.GetYOffset(dataMarkerTemplate));

            GraphBase.SetDataPoint(dataMarkerTemplate, true);
            return dataMarkerTemplate;
        }

        /// <summary>
        /// Sets the summary of the graph.
        /// </summary>
        protected override void SetGraphSummary()
        {
            base.SetGraphSummary();
            this.GraphSummary.DataMarker = this.DataMarkerTemplate;
            this.GraphSummary.ShowNormalRange = this.ShowNormalRange ? Visibility.Visible : Visibility.Collapsed;
            this.GraphSummary.NormalRangeMaximumValue = this.NormalRangeMaximumValue;
            this.GraphSummary.NormalRangeMinimumValue = this.NormalRangeMinimumValue;
            this.GraphSummary.Units = this.Units;
            this.GraphSummary.UnitsDescription = this.UnitsDescription;
        }

        /// <summary>
        /// Gets the last point into view.
        /// </summary>
        protected void GetLastPointToView()
        {
            if (this.DataContext != null)
            {
                object o = DataHelper.GetLastDataPoint(this.DataContext);
                GraphPoint gp = this.GetBoundGraphPoint(o);

                double dataPointValue = 0;
                if (gp != null)
                {
                    dataPointValue = gp.Y1;

                    if (!double.IsNaN(gp.Y2))
                    {
                        dataPointValue = (gp.Y1 + gp.Y2) / 2;
                    }
                }

                double dataPointY = this.GetYForValue(dataPointValue);

                double totalYAxisHeight = (this.actualYAxisMaxValue - this.actualYAxisMinValue) * this.YAxisScale;
                if (totalYAxisHeight > this.NonDynamicRightAxisViewPort.ActualHeight)
                {
                    this.VerticalScrollBar.Value = dataPointY - (this.NonDynamicRightAxisViewPort.ActualHeight / 2);
                }
            }
        }

        /// <summary>
        /// Initializes the graph.
        /// </summary>
        protected override void InitializeGraph()
        {
            base.InitializeGraph();
            this.SetYAxisRange();
            this.YAxisScale = (this.NonDynamicRightAxisViewPort.ActualHeight - (this.YAxisPadding.Top + this.YAxisPadding.Bottom)) / (this.actualYAxisMaxValue - this.actualYAxisMinValue);
            if (this.ScaleToFit == false && (this.YAxisScale * this.YAxisMajorInterval < this.YAxisIntervalMinimumHeight))
            {
                this.YAxisScale = this.YAxisIntervalMinimumHeight / this.YAxisMajorInterval;
            }

            this.VerticalScrollBar.Minimum = 0;
            this.VerticalScrollBar.ViewportSize = this.NonDynamicRightAxisViewPort.ActualHeight;
            this.VerticalScrollBar.SmallChange = 1;
            this.VerticalScrollBar.LargeChange = this.YAxisMajorInterval * this.YAxisScale > 0 ? this.YAxisMajorInterval * this.YAxisScale : 1;
            double verticalScrollMax = ((this.actualYAxisMaxValue - this.actualYAxisMinValue) * this.YAxisScale + (this.YAxisPadding.Top + this.YAxisPadding.Bottom)) - this.VerticalScrollBar.ViewportSize;
            this.VerticalScrollBar.Maximum = verticalScrollMax > 0 ? verticalScrollMax : 0;

            if (this.DesignMode && string.IsNullOrEmpty(this.Units))
            {
                this.Units = GraphingResources.DesignModeUnitsLabel;
            }
        }

        /// <summary>
        /// Called when [layout panels complete].
        /// </summary>
        protected override void OnLayoutPanelsComplete()
        {
            base.OnLayoutPanelsComplete();
            this.PositionVerticalLayers((this.VerticalScrollBar != null) ? this.VerticalScrollBar.Value : 0);
        }

        /// <summary>
        /// Virtual. Plots a time graph with a filtered set of data.
        /// </summary>
        /// <param name="subSet">Filtered set of data.</param>
        /// <param name="panel">The panel.</param>
        protected override void DrawFilteredTimeGraph(IEnumerable subSet, PanelWrapper panel)
        {
            base.DrawFilteredTimeGraph(subSet, panel);

            // Maintain the previous and current point plotted for the virtual method call
            GraphPoint prevPoint = new GraphPoint();
            GraphPoint currPoint = new GraphPoint();

            System.Collections.Generic.Stack<FrameworkElement> dataPointStack =
                new System.Collections.Generic.Stack<FrameworkElement>();

            System.Collections.Generic.Stack<FrameworkElement> dataPointLabelStack =
                new System.Collections.Generic.Stack<FrameworkElement>();

            CollisionDetectionManager collisionManager = GraphBase.GetCollisionDetectionManager(this.DynamicPlotLayer);

            foreach (object o in subSet)
            {
                if (panel.AbortLayout)
                {
                    break;
                }

                GraphPoint gp = this.GetBoundGraphPoint(o);
                if (null == gp)
                {
                    return;
                }

                gp.X1Pixel = this.GetXForDate(gp.X1);
                gp.Y1Pixel = this.GetYForValue(gp.Y1);

                if (!Double.IsNaN(gp.Y2))
                {
                    gp.Y2Pixel = this.GetYForValue(gp.Y2);
                }

                currPoint = gp;

                // process the plotted data marker
                this.ProcessPlottedDataMarker(prevPoint, currPoint);

                if (!this.Minimized)
                {
                    if (!double.IsNaN(gp.Y1) && currPoint.X1Pixel >= 0 && currPoint.X1Pixel < this.DynamicMainLayerViewport.ActualWidth)
                    {
                        FrameworkElement marker = this.GetPlottedDataMarker(gp);
                        double left = Canvas.GetLeft(marker);
#if !SILVERLIGHT
                    // For WPF should snap to grid if requested
                    bool snap = GraphBase.GetSnapToPixels(marker);
                    if (marker.SnapsToDevicePixels != snap)
                    {
                        marker.SnapsToDevicePixels = snap;
                    }
#endif

                        if (left < 0 || (left + marker.Width) > this.DynamicMainLayerViewport.ActualWidth)
                        {
                            this.CurrentWorkingPanel.SeamedElementCollection.Add(marker);
                        }

                        this.DynamicPlotLayer.Children.Add(marker);
                        dataPointStack.Push(marker);

                        FrameworkElement labelElement = null;
                        if (null != this.LabelTemplate)
                        {
                            labelElement = this.LabelTemplate.LoadContent() as FrameworkElement;
                            labelElement.DataContext = gp;
                            dataPointLabelStack.Push(labelElement);
                        }

                        if (null != labelElement)
                        {
                            double offsetValueX = GraphBase.GetXOffset(labelElement);
                            double offsetValueY = GraphBase.GetYOffset(labelElement);

                            // define the label position
                            double labelY = currPoint.Y1Pixel;
                            if (!double.IsNaN(gp.Y2) && gp.Y1 < gp.Y2)
                            {
                                labelY = currPoint.Y2Pixel;
                            }

                            double markerOffset = Math.Abs(GraphBase.GetYOffset(marker)) + 2;
                            Canvas.SetTop(labelElement, (labelY - markerOffset) + offsetValueY);
                            Canvas.SetLeft(labelElement, currPoint.X1Pixel + offsetValueX);
                            labelElement.Visibility = this.ShowDataPointLabels;
                            this.DynamicPlotLayer.Children.Add(labelElement);
                            GraphBase.SetDataPointLabel(marker, labelElement);
                            panel.LabelElements.Add(labelElement);

                            marker.MouseEnter += new MouseEventHandler(this.DataPointMarker_MouseEnter);
                            marker.MouseLeave += new MouseEventHandler(this.DataPointMarker_MouseLeave);
                        }
                    }
                }

                prevPoint = currPoint;
            }

            if (!panel.AbortLayout && this.DetectCollisions)
            {
                Collision previousCollision = null;

                // get the last items in the collection for the data context
                object[] lastItems = this.GetLastDataPoints();
                object lastObject = lastItems[1];
                object secondLastObject = lastItems[0];

                byte lastItemsCheck = 0;

                while (dataPointStack.Count > 0)
                {
                    FrameworkElement ele = dataPointStack.Pop();
                    FrameworkElement labelEle = GraphBase.GetDataPointLabel(ele);

                    if (lastItemsCheck < 2 && null != labelEle && null != labelEle.DataContext)
                    {
                        // this is the last item on the page.
                        // we plot back to front to check if things overlap.
                        ++lastItemsCheck;
                        object gp = ((GraphPoint)labelEle.DataContext).DataContext;
                        if (lastObject == gp)
                        {
                            // this is the last item in the series.
                            GraphBase.SetLastItem(ele, true);
                            labelEle.Visibility = Visibility.Visible;
                            labelEle.RenderTransform = this.LabelTransform;
                        }

                        if (secondLastObject == gp)
                        {
                            // this is the second last item in the series.
                            GraphBase.SetSecondToLast(ele, true);
                            labelEle.Visibility = Visibility.Visible;
                        }
                    }

                    FrameworkElement clashElement = collisionManager.RegisterAndTestIfMarkerOverlaps(ele);

                    if (null != clashElement)
                    {
                        previousCollision = new Collision(clashElement, ele, true);
                        this.CurrentWorkingPanel.CollisionCollection.Add(previousCollision);

                        GraphBase.SetDataPointOverlapProperty(ele, true);
                        GraphBase.SetDataPointOverlapProperty(labelEle, true);
                        labelEle.Visibility = Visibility.Collapsed;

                        GraphBase.SetDataPointOverlapProperty(clashElement, true);
                        FrameworkElement clashElementLabel = GraphBase.GetDataPointLabel(clashElement);
                        GraphBase.SetDataPointOverlapProperty(clashElementLabel, true);
                        clashElementLabel.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        // this marker does not overlap
                        previousCollision = null;
                    }
                }

                while (dataPointLabelStack.Count > 0)
                {
                    FrameworkElement ele2 = dataPointLabelStack.Pop();
                    if (collisionManager.RegisterAndTestIfLabelOverlaps(ele2))
                    {
                        GraphBase.SetDataPointOverlapProperty(ele2, true);
                        ele2.Visibility = Visibility.Collapsed;
                    }
                }

                PanelWrapper otherPanel = null;

                // we get panel 2 then panel 1 when getting both panels.
                double markerOffsetToUse = 0;

                // perform seam detection
                // panel1 is to the left of panel2
                if (this.CurrentWorkingPanel == this.Panel1)
                {
                    // panel2 markers must be incremented by the width of the layer.
                    otherPanel = this.Panel2;
                    markerOffsetToUse = this.Panel1.Width;
                }
                else
                {
                    // panel1 markers must be decremented by the width of the plot layer.
                    // if we are renewing both panels, then do not do collisions detections on seams for
                    if (true != this.RenewingBothPanels)
                    {
                        otherPanel = this.Panel1;
                        markerOffsetToUse = this.Panel2.Width * -1;
                    }
                }

                // this gets run if we are renewing a panel, but if renewing both, only for panel 1
                if (null != otherPanel)
                {
                    LookThroughAdjacentPanelForOverlap(this.CurrentWorkingPanel, otherPanel, markerOffsetToUse);
                }

                double lastCollisionPosition = -1;

                foreach (Collision collision in this.CurrentWorkingPanel.CollisionCollection)
                {
                    if (null != this.CollisionTemplate)
                    {
                        FrameworkElement collisionIcon = this.CollisionTemplate.LoadContent() as FrameworkElement;
                        double theXOffset = GraphBase.GetXOffset(collisionIcon);
                        double theYOffset = GraphBase.GetYOffset(collisionIcon);
                        if (null != collisionIcon)
                        {
                            double localxPosition = Canvas.GetLeft(collision.ClusterStartElement) + theXOffset;
                            double localyPosition = Canvas.GetTop(collision.ClusterStartElement) + theYOffset;

                            if (lastCollisionPosition == -1 || localxPosition < lastCollisionPosition - 46)
                            {
                                lastCollisionPosition = localxPosition;

                                Canvas.SetLeft(collisionIcon, localxPosition);
                                Canvas.SetTop(collisionIcon, localyPosition);

                                if (true == collision.ClusterShowingIcon)
                                {
                                    this.DynamicPlotLayer.Children.Add(collisionIcon);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the value for Y co-ordinate.
        /// </summary>
        /// <param name="value">Value for which the co-ordinate needs to be determined.</param>
        /// <returns>Value of the Y co-ordinate.</returns>
        protected double GetYForValue(double value)
        {
            double totalYAxisHeight = (this.actualYAxisMaxValue - this.actualYAxisMinValue) * this.YAxisScale;
            return totalYAxisHeight - ((value - this.actualYAxisMinValue) * this.YAxisScale) + this.YAxisPadding.Top;
        }

        /// <summary>
        /// Gets the X axis grid line.
        /// </summary>
        /// <param name="axisOffset">The offset in the x axis.</param>
        /// <returns>
        /// Line for the X axis at specified horizontal offset.
        /// </returns>
        protected override LineGeometry GetXAxisGridLine(double axisOffset)
        {
            LineGeometry geo = new LineGeometry();

            double y = this.GetYForValue(this.actualYAxisMinValue);

            geo.StartPoint = new Point(axisOffset, this.YAxisPadding.Top);
            geo.EndPoint = new Point(axisOffset, y);

            return geo;
        }

        /// <summary>
        /// Determines if directions arrow are needed.
        /// </summary>
        protected override void ProcessGraphPoints()
        {
            // Assume the arrows will not be shown.
            bool showUpArrow = false;
            bool showDownArrow = false;

            if (null != this.DynamicPlotLayerViewport && this.DynamicPlotLayerViewport.ActualHeight != 0 && (null != this.ArrowButtonUp || null != this.ArrowButtonDown))
            {
                this.ArrowDownScrollAmount = 0;
                this.ArrowUpScrollAmount = 0;

                foreach (Panel panel in new Collection<Panel> { this.CurrentPanel1, this.CurrentPanel2 })
                {
                    if (null != panel)
                    {
                        double leftOffset = Canvas.GetLeft(panel);

                        // Define the bounds of the plot layer.
                        double viewMinY = -Canvas.GetTop(this.DynamicPlotLayer);
                        double viewMaxY = viewMinY + this.DynamicPlotLayerViewport.ActualHeight;
                        double posX, posY1, posY2;

                        Grid plotGrid = panel.Children[0] as Grid;
                        if (null != plotGrid)
                        {
                            Canvas canvas = plotGrid.FindName(DynamicPlotLayerElementName) as Canvas;
                            if (null != canvas)
                            {
                                foreach (FrameworkElement gp in canvas.Children)
                                {
                                    if (GraphBase.GetDataPoint(gp))
                                    {
                                        posX = Canvas.GetLeft(gp) + leftOffset;
                                        posY1 = Canvas.GetTop(gp);
                                        posY2 = posY1 + gp.ActualHeight;
                                        if (posX >= 0 && posX <= this.DynamicMainLayerViewport.ActualWidth)
                                        {
                                            // I can see the point on the X.                                                                      
                                            if (posY2 > viewMaxY)
                                            {
                                                this.ArrowDownScrollAmount = Math.Max(this.ArrowDownScrollAmount, (posY2 - viewMaxY + 1));
                                                showDownArrow = true;
                                            }

                                            if (posY1 < viewMinY)
                                            {
                                                this.ArrowUpScrollAmount = Math.Max(this.ArrowUpScrollAmount, (viewMinY - posY1 + 1));
                                                showUpArrow = true;
                                            }
#if SILVERLIGHT
                                            // Ensure the point is snapped to the grid
                                            this.SnapGraphPoint(gp);
#endif
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (null != this.ArrowButtonUp)
                {
                    if (showUpArrow)
                    {
                        this.ArrowButtonUp.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.ArrowButtonUp.Visibility = Visibility.Collapsed;
                    }
                }

                if (null != this.ArrowButtonDown)
                {
                    if (showDownArrow)
                    {
                        this.ArrowButtonDown.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.ArrowButtonDown.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        /// <summary>
        /// Adds a separator for Y axis labels.
        /// </summary>
        protected override void AddYAxisLabelsSeparator()
        {
            if (this.AddYAxisSeparator)
            {
                Line line = new Line();
                line.Stroke = new SolidColorBrush(Colors.Black);
                line.StrokeThickness = 1;
                line.X1 = line.X2 = this.YAxisPosition == YAxisPosition.Right ? this.DynamicMainLayerViewport.ActualWidth : this.DynamicMainLayerViewport.Margin.Left;
                line.Y1 = 0;

                if (!double.IsNaN(this.actualYAxisMaxValue) && !double.IsNaN(this.actualYAxisMinValue))
                {
                    line.Y2 = (this.actualYAxisMaxValue - this.actualYAxisMinValue) * this.YAxisScale + (this.YAxisPadding.Top + this.YAxisPadding.Bottom);
                }
                else
                {
                    line.Y2 = this.NonDynamicRightAxisViewPort.ActualHeight;
                }

                this.NonDynamicRightAxisLinesLayer.Children.Add(line);
            }
        }

        /// <summary>
        /// Sets the Y axis start and end values.
        /// </summary>
        protected virtual void SetYAxisRange()
        {
            double minValue = -1;
            double maxValue = -1;
            this.YAxisMinorInterval = this.YAxisMajorInterval / this.YAxisMinorIntervalsCountInMajorInterval;

            DependencyObject dp = this.PointTemplate.LoadContent();
            if (null == dp)
            {
                return;
            }

            GraphPoint gp = dp as GraphPoint;
            if (null == gp)
            {
                return;
            }

            if (this.DataContext != null)
            {
                IEnumerable dataSeries = this.DataContext as IEnumerable;
                if (dataSeries != null)
                {
                    foreach (object o in dataSeries)
                    {
                        gp = this.GetBoundGraphPoint(o);
                        if (gp.Y1 < minValue || minValue == -1)
                        {
                            minValue = gp.Y1;
                        }

                        if (gp.Y1 > maxValue || maxValue == -1)
                        {
                            maxValue = gp.Y1;
                        }

                        if (!Double.IsNaN(gp.Y2))
                        {
                            if (gp.Y2 < minValue || minValue == -1)
                            {
                                minValue = gp.Y2;
                            }

                            if (gp.Y2 > maxValue || maxValue == -1)
                            {
                                maxValue = gp.Y2;
                            }
                        }
                    }

                    this.dataSeriesMinValue = minValue;
                    this.dataSeriesMaxValue = maxValue;
                }

                if (this.ScaleToFit)
                {
                    // Scale to fit Y axis. Infer the range from data.
                    this.actualYAxisMinValue = this.dataSeriesMinValue;
                    this.actualYAxisMaxValue = this.dataSeriesMaxValue;
                }
                else
                {
                    if (!double.IsNaN(this.YAxisMaxValue) && !double.IsNaN(this.YAxisMinValue))
                    {
                        this.actualYAxisMaxValue = this.YAxisMaxValue;
                        this.actualYAxisMinValue = this.YAxisMinValue;
                    }
                    else
                    {
                        double rangeInc = this.YAxisMajorInterval * 10;
                        if (minValue > 0 && this.NormalRangeMinimumValue > 0)
                        {
                            minValue = Math.Min(minValue - minValue * 0.1, this.NormalRangeMinimumValue - this.NormalRangeMinimumValue * 0.2);
                        }
                        else
                        {
                            minValue = Math.Min(minValue + minValue * 0.1, this.NormalRangeMinimumValue + this.NormalRangeMinimumValue * 0.2);
                        }

                        // ensure values are within 10 times the y axis major interval; sanity check for very large numbers
                        if (minValue < (this.dataSeriesMinValue - rangeInc))
                        {
                            minValue = Math.Min(this.dataSeriesMinValue, this.NormalRangeMinimumValue) - rangeInc;
                        }

                        maxValue = Math.Max(maxValue + maxValue * 0.1, this.NormalRangeMaximumValue + this.NormalRangeMaximumValue * 0.2);

                        // ensure values are within 10 times the y axis major interval; sanity check for very large numbers
                        if (maxValue > (this.dataSeriesMaxValue + rangeInc))
                        {
                            maxValue = Math.Max(this.dataSeriesMaxValue, this.NormalRangeMaximumValue) + rangeInc;
                        }

                        this.actualYAxisMaxValue = maxValue;
                        this.actualYAxisMinValue = minValue;
                    }
                }

                if (this.actualYAxisMinValue % this.YAxisMajorInterval != 0)
                {
                    if (this.actualYAxisMinValue > 0)
                    {
                        this.actualYAxisMinValue -= this.actualYAxisMinValue % this.YAxisMajorInterval;
                    }
                    else
                    {
                        this.actualYAxisMinValue -= this.YAxisMajorInterval + (this.actualYAxisMinValue % this.YAxisMajorInterval);
                    }
                }

                if (this.actualYAxisMaxValue % this.YAxisMajorInterval != 0)
                {
                    if (this.actualYAxisMaxValue > 0)
                    {
                        this.actualYAxisMaxValue += this.YAxisMajorInterval - (this.actualYAxisMaxValue % this.YAxisMajorInterval);
                    }
                    else
                    {
                        this.actualYAxisMaxValue += this.actualYAxisMaxValue % this.YAxisMajorInterval;
                    }
                }

                if (this.actualYAxisMaxValue == this.actualYAxisMinValue)
                {
                    this.actualYAxisMaxValue = this.actualYAxisMinValue + this.YAxisMajorInterval;
                }
            }
            else
            {
                this.actualYAxisMaxValue = 1;
                this.actualYAxisMinValue = 0;
            }
        }
        #endregion

        #region Dependency Property Changed Callbacks
        /// <summary>
        /// Graph layout properties changed callback method.
        /// </summary>
        /// <param name="dependencyObject">DependencyObject for callback.</param>
        /// <param name="args">The arguments.</param>
        private static void GraphLayoutChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            TimeAndYGraphBase graph = dependencyObject as TimeAndYGraphBase;
            if (null != graph)
            {
                graph.Refresh(true);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets the Size element associated with a Button.
        /// </summary>
        /// <param name="button">The Button element to Measure.</param>
        /// <returns>The Button Size.</returns>
        private static Size GetArrowButtonSize(Button button)
        {
            button.Visibility = Visibility.Visible;
            Size size = new Size(button.ActualWidth, button.ActualHeight);

            if (0 == size.Width || 0 == size.Height)
            {
                button.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            }

            if (0 == size.Width)
            {
                size.Width = button.DesiredSize.Width;
            }

            if (0 == size.Height)
            {
                size.Height = button.DesiredSize.Height;
            }

            button.Visibility = Visibility.Collapsed;
            return size;
        }
        
        /// <summary>
        /// Renders the Y axis plot position.
        /// </summary>
        /// <param name="position">Position for the rendering.</param>
        /// <param name="majorInterval">Indicator for major interval.</param>
        private void DrawYAxisMarker(double position, bool majorInterval)
        {
            if (this.ShowGridLines == GridLineVisibility.Horizontal || this.ShowGridLines == GridLineVisibility.Both)
            {
                if (majorInterval || this.ShowMinorGridLines == GridLineVisibility.Horizontal || this.ShowMinorGridLines == GridLineVisibility.Both)
                {
                    this.DrawYAxisGridLines(position);
                }
            }

            this.PlotYAxisLabel(position, majorInterval);
        }

        /// <summary>
        /// Plots the labels for the y axis.
        /// </summary>
        /// <param name="value">Value of the label.</param>
        /// <param name="majorInterval">Boolean indicating whether the value is a major interval.</param>
        private void PlotYAxisLabel(double value, bool majorInterval)
        {
            double top = this.GetYForValue(value);
            double left = this.DynamicMainLayerViewport.ActualWidth;
            double tickLength = majorInterval ? this.MajorAxisTickLength : this.MinorAxisTickLength;

            bool showTicks = this.CanShowYAxisTicks(majorInterval);
            if (showTicks)
            {
                Line axisTick = new Line();
                axisTick.StrokeThickness = this.TickLineThickness;
                axisTick.Stroke = this.TickLineBrush;

                axisTick.Y1 = axisTick.Y2 = top;
                if (this.YAxisPosition == YAxisPosition.Right)
                {
                    axisTick.X1 = left;
                    axisTick.X2 = left + tickLength;
                }
                else
                {
                    axisTick.X1 = this.DynamicMainLayerViewport.Margin.Left;
                    axisTick.X2 = axisTick.X1 - tickLength;
                }

                this.NonDynamicRightAxisLabelsLayer.Children.Add(axisTick);

                left = axisTick.X2;
            }

            if (majorInterval)
            {
                TextBlock yaxisLabel = this.yaxisLabelTemplate.LoadContent() as TextBlock;
                yaxisLabel.FontFamily = this.FontFamily;
                if (yaxisLabel != null)
                {
                    yaxisLabel.Text = value.ToString(CultureInfo.CurrentCulture);
                    if (this.YAxisPosition == YAxisPosition.Right)
                    {
                        Canvas.SetLeft(yaxisLabel, left);
                    }
                    else
                    {
                        Canvas.SetLeft(yaxisLabel, 0);
                    }

                    Canvas.SetTop(yaxisLabel, top - TextBlockHelper.GetDesiredHeight(yaxisLabel) / 2);
                    this.NonDynamicRightAxisLabelsLayer.Children.Add(yaxisLabel);
                }
            }
        }

        /// <summary>
        /// Adds the normal range visual indicator to view port.
        /// </summary>
        private void AddNormalRange()
        {
            Canvas normalRangeCanvas = new Canvas();
            normalRangeCanvas.Background = this.NormalRangeBrush;

            double startYPosition = this.GetYForValue(this.NormalRangeMinimumValue);
            double endYPosition = this.GetYForValue(this.NormalRangeMaximumValue);

            if (startYPosition - endYPosition > 0)
            {
                normalRangeCanvas.Height = startYPosition - endYPosition;
            }
            else
            {
                normalRangeCanvas.Height = endYPosition - startYPosition;
            }

            normalRangeCanvas.Width = this.DynamicMainLayerViewport.ActualWidth;
            Canvas.SetTop(normalRangeCanvas, endYPosition);
            Canvas.SetLeft(normalRangeCanvas, 0);

            if (this.YAxisPosition == YAxisPosition.Left)
            {
                Canvas.SetLeft(normalRangeCanvas, this.DynamicMainLayerViewport.Margin.Left);
            }

            this.NonDynamicRightAxisLinesLayer.Children.Add(normalRangeCanvas);
        }

        /// <summary>
        /// Draws the Horizontal grid lines for Y axis at a specified value.
        /// </summary>
        /// <param name="value">Value in Y axis at which line needs to be drawn.</param>
        private void DrawYAxisGridLines(double value)
        {
            double left = this.DynamicMainLayerViewport.ActualWidth;
            double top = this.GetYForValue(value);

            Line gridLine = new Line();

            if (this.YAxisPosition == YAxisPosition.Left)
            {
                gridLine.X1 = this.DynamicMainLayerViewport.Margin.Left;
                gridLine.X2 = this.NonDynamicRightAxisViewPort.ActualWidth;
            }
            else
            {
                gridLine.X1 = 0;
                gridLine.X2 = left;
            }

            gridLine.Y1 = top;
            gridLine.Y2 = top;
            gridLine.Stroke = this.GridLineBrush;
            gridLine.StrokeThickness = this.GridLineThickness;
            this.NonDynamicRightAxisLinesLayer.Children.Add(gridLine);
        }

        /// <summary>
        /// Gets a value indicating whether the ticks can be shown for the current Y axis interval.
        /// </summary>
        /// <param name="majorInterval">Boolean indicating whether the current interval is a major interval.</param>
        /// <returns>Value indicating whether or not tick should be shown.</returns>
        private bool CanShowYAxisTicks(bool majorInterval)
        {
            bool showTicks = false;

            if (majorInterval)
            {
                showTicks = true;
            }
            else
            {
                if (this.ShowYAxisMinorIntervalTicks == true)
                {
                    showTicks = true;
                }
            }

            return showTicks;
        }               
        
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the ValueChanged event of the VerticalScrollBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void VerticalScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.PositionVerticalLayers(e.NewValue);
        }

        /// <summary>
        /// Handles the Scroll event of the VerticalScrollBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void VerticalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.Focus();
        }

        /// <summary>
        /// Handles the click event of the Arrow Up Button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void ArrowButtonUp_Click(object sender, RoutedEventArgs e)
        {
            if (null != this.VerticalScrollBar)
            {
                this.VerticalScrollBar.Value -= (this.ArrowUpScrollAmount == 0) ? this.VerticalScrollBar.LargeChange : this.ArrowUpScrollAmount;
            }

            this.Focus();
        }

        /// <summary>
        /// Handles the click event of the Arrow Down Button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void ArrowButtonDown_Click(object sender, RoutedEventArgs e)
        {
            if (null != this.VerticalScrollBar)
            {
                this.VerticalScrollBar.Value += (this.ArrowDownScrollAmount == 0) ? this.VerticalScrollBar.LargeChange : this.ArrowDownScrollAmount;
            }

            this.Focus();
        }

        /// <summary>
        /// Handles the SizeChanged event of the NonDynamicRightAxisViewPort control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        private void NonDynamicRightAxisViewPort_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RectangleGeometry clip = new RectangleGeometry();
            clip.Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height);
            this.NonDynamicRightAxisViewPort.Clip = clip;
        }
        #endregion
    }
}
