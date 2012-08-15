//-----------------------------------------------------------------------
// <copyright file="TimeActivityGraph.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>17-Sep-2009</date>
// <summary>TimeActivity graph class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// Class used to represent TimeActivityGraph.
    /// </summary>
    [TemplatePart(Name = LabelsViewportElementName, Type = typeof(Canvas))]
    [TemplatePart(Name = LabelsLayerElementName, Type = typeof(Canvas))]
    public class TimeActivityGraph : TimeGraphBase
    {
        #region Dependency Properties
        /// <summary>
        /// Identifies the IsLabel attached property.
        /// </summary>
        public static readonly DependencyProperty IsLabelProperty =
            DependencyProperty.RegisterAttached("IsLabel", typeof(bool), typeof(TimeActivityGraph), new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeActivityGraph.StackLabels"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty StackLabelsProperty = DependencyProperty.Register("StackLabels", typeof(bool), typeof(TimeActivityGraph), new PropertyMetadata(new PropertyChangedCallback(StackLabelsPropertyChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeActivityGraph.PlotAreaHeight"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PlotAreaHeightProperty =
            DependencyProperty.Register("PlotAreaHeight", typeof(double), typeof(TimeActivityGraph), null);

        /// <summary>
        /// Identifies the FlagStick attached property.
        /// </summary>
        private static readonly DependencyProperty FlagStickProperty =
            DependencyProperty.RegisterAttached("FlagStick", typeof(FrameworkElement), typeof(TimeActivityGraph), new PropertyMetadata(null));
        #endregion

        #region Template Part Names
        /// <summary>
        /// Template part name for labels layer viewport.
        /// </summary>
        private const string LabelsViewportElementName = "ELEMENT_LabelsLayerViewPort";

        /// <summary>
        /// Template part name for labels layer.
        /// </summary>
        private const string LabelsLayerElementName = "Element_LabelsLayer";               
        #endregion

        #region Template parts
        /// <summary>
        /// Member variable to hold labels layer.
        /// </summary>
        private Canvas labelsLayer;

        /// <summary>
        /// Member variable to hold labels layer viewport.
        /// </summary>
        private Canvas labelsViewport;
        #endregion

        #region Private members
        /// <summary>
        /// Member variable to indicate the start Y position for the labels.
        /// </summary>
        private double labelStartYPosition;

        /// <summary>
        /// Member variable to indicate whether the label is overlapping.
        /// </summary>
        private bool labelsOverlapping;

        /// <summary>
        /// Member variable to hold collection of label lines.
        /// </summary>
        private Dictionary<int, LabelLineDetails> labelLines = new Dictionary<int, LabelLineDetails>();

        /// <summary>
        /// Member variable to hold the zindex for the labels.
        /// </summary>
        private int labelZIndex = Int16.MaxValue;

        /// <summary>
        /// Member variable to hold the y co-ordinate of the interpolation lines.
        /// </summary>
        private double interpolationLineStartYPosition;

        /// <summary>
        /// Member variable to hold activities.
        /// </summary>
        private ActivityCollection activities = new ActivityCollection();

        /// <summary>
        /// Offset to be used when labels are overlaid.
        /// </summary>
        /// <remarks>Should be negative value to position the label on top.</remarks>
        private double labelOverlayOffset = -3;

        /// <summary>
        /// Member variable to hold start date from the data.
        /// </summary>
        private DateTime? dataStartDate;

        /// <summary>
        /// Member variable to hold the end date from the data.
        /// </summary>
        private DateTime? dataEndDate;       

        /// <summary>
        /// Collection to hold the actual data used in plotting the labels.
        /// </summary>
        private Collection<object> labelsLayerData = new Collection<object>();
       #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeActivityGraph"/> class.
        /// </summary>
        public TimeActivityGraph()
        {
            this.DefaultStyleKey = typeof(TimeActivityGraph);
            this.activities.CollectionChanged += new NotifyCollectionChangedEventHandler(this.Activities_CollectionChanged);            
            this.MaxLabelStackLevels = 2;
            this.ShowLabelOvercrowdingNotifications = true;

            this.InterpolationLineRowHeight = 12;
            this.LabelOvercrowdingNotificationsRowHeight = 20;
            this.LabelRowHeight = 25;
            this.ActivityRowHeight = 20;
        }                
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a label is clicked.
        /// </summary>
        public event RoutedEventHandler LabelClick;

        /// <summary>
        /// Occurs when an activity is clicked.
        /// </summary>
        public event RoutedEventHandler ActivityClick;

        /// <summary>
        /// Occurs when an interpolation line is clicked.
        /// </summary>
        public event RoutedEventHandler InterpolationLineClick;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether [stack labels].
        /// </summary>
        /// <value>If [stack labels] <c>true</c>; otherwise, <c>false</c>.</value>
        [Category("Graph Appearance")]
        public bool StackLabels
        {
            get { return (bool)this.GetValue(StackLabelsProperty); }
            set { this.SetValue(StackLabelsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Activities.
        /// </summary>
        /// <value>The Activities.</value>
        [Category("Graph Appearance")]
        public ActivityCollection Activities
        {
            get { return this.activities; }
            set { this.activities = value; }
        }

        /// <summary>
        /// Gets or sets the height of the plot area.
        /// </summary>
        /// <value>The height of the plot area.</value>
        [Category("Graph Appearance")]
        public double PlotAreaHeight
        {
            get { return (double)GetValue(PlotAreaHeightProperty); }
            set { SetValue(PlotAreaHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the height of the interpolation line row.
        /// </summary>
        /// <value>The height of the interpolation line row.</value>
        [Category("Graph Appearance")]
        public double InterpolationLineRowHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height of the label row.
        /// </summary>
        /// <value>The height of the label row.</value>
        [Category("Graph Appearance")]
        public double LabelRowHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height of the activity row.
        /// </summary>
        /// <value>The height of the activity row.</value>
        [Category("Graph Appearance")]
        public double ActivityRowHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height of the label overcrowding notifications row.
        /// </summary>
        /// <value>The height of the label overcrowding notifications row.</value>
        [Category("Graph Appearance")]
        public double LabelOvercrowdingNotificationsRowHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the max label stack levels.
        /// </summary>
        /// <value>The max label stack levels.</value>
        [Category("Graph Appearance")]
        public int MaxLabelStackLevels
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether to show activities.
        /// </summary>
        /// <value>
        /// <c>If show activities, true</c>; otherwise, <c>false</c>.
        /// </value>
        [Category("Graph Appearance")]
        public bool ShowActivities
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show label overcrowding notifications].
        /// </summary>
        /// <value>
        /// If show label overcrowding notifications then <c>true</c>; otherwise, <c>false</c>.
        /// </value>
        [Category("Graph Appearance")]
        public bool ShowLabelOvercrowdingNotifications
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the label mode.
        /// </summary>
        /// <value>The label mode.</value>
        [Category("Graph Appearance")]
        public LabelMode LabelMode
        {
            get;
            set;
        }
        #endregion

        #region Attached Props Set/Get
        /// <summary>
        /// Gets a value indicating whether the given dependency object is a label element.
        /// </summary>
        /// <param name="obj">DependencyObject which needs to be checked.</param>
        /// <returns>Value indicating whether the given dependency object is a label element.</returns>
        public static bool GetIsLabel(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsLabelProperty);
        }

        /// <summary>
        /// Sets the given dependency object as a label element.
        /// </summary>
        /// <param name="obj">The DependencyObject which needs to be set as label element.</param>
        /// <param name="value">Value indicating whether the given dependency object is a label element.</param>
        public static void SetIsLabel(DependencyObject obj, bool value)
        {
            obj.SetValue(IsLabelProperty, value);
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Overridden. Applies the specified template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.labelsViewport = this.GetTemplateChild<Canvas>(LabelsViewportElementName, false);
            this.labelsLayer = this.GetTemplateChild<Canvas>(LabelsLayerElementName, true);
            TimeGraphBase.SetCollisionDetectionManager(this.labelsLayer, new CollisionDetectionManager());

            if (this.labelsViewport != null)
            {
                this.labelsViewport.SizeChanged += new SizeChangedEventHandler(this.LabelsViewport_SizeChanged);
            }            
        }

        /// <summary>
        /// Gets the axis dates.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        internal override void GetAxisDates(out DateTime? startDate, out DateTime? endDate)
        {
            startDate = endDate = null;
            if (this.DataContext != null && this.VisibleWindow != null)
            {
                IEnumerable dataSeries = this.DataContext as IEnumerable;
                this.CalculateAxisDates(dataSeries);
                foreach (IEnumerable activitySet in this.activities)
                {
                    this.CalculateAxisDates(activitySet);
                }

                startDate = this.dataStartDate;
                if (this.dataEndDate.HasValue)
                {
                    endDate = this.GetAxisEndDate(this.dataEndDate.Value);
                }
                else if (this.dataStartDate.HasValue)
                {
                    endDate = this.GetAxisEndDate(this.dataStartDate.Value);
                }                
            }
        }
        
        /// <summary>
        /// Virtual. Plots a time graph with a filtered set of data.
        /// </summary>
        /// <param name="subSet">Filtered set of data.</param>
        /// <param name="panel">The panel to plot the data.</param>
        protected override void DrawFilteredTimeGraph(System.Collections.IEnumerable subSet, PanelWrapper panel)
        {
            if (!panel.AbortLayout)
            {
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

                    gp.X1Pixel = (int)this.GetXForDate(gp.X1);
                    gp.X2Pixel = gp.X2.HasValue ? this.GetXForDate(gp.X2.Value) : this.GetXForDate(this.AxisEndDate);

                    gp.Y1Pixel = this.interpolationLineStartYPosition;

                    if (!this.Minimized)
                    {
                        FrameworkElement element = this.AddMarkerFromGraphPoint(gp, false);
                        Canvas.SetLeft(element, gp.X1Pixel > 0 ? gp.X1Pixel : 0);
                        Canvas.SetTop(element, gp.Y1Pixel);
                        Canvas.SetZIndex(element, 10);
                        element.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.InterpolationLine_MouseLeftButtonDown);
                        this.AddLabelElement(gp, panel);
                    }
                    else
                    {
                        if ((gp.X1Pixel > 0 || gp.X2Pixel > 0) && gp.X1Pixel < this.DynamicMainLayerViewport.ActualWidth)
                        {
                            this.MinimizedPlotLayer.Children.Add(this.GetInterpolationLine(gp));
                        }
                    }
                }

                if (!this.Minimized && this.ShowActivities)
                {
                    this.RenderActivities(panel);
                }
            }
        }
                
        /// <summary>
        /// Called when [layout panels complete].
        /// </summary>
        protected override void OnLayoutPanelsComplete()
        {
            base.OnLayoutPanelsComplete();
            this.labelsLayer.Children.Clear();
            this.labelsLayerData.Clear();

            if (this.ShowDataPointLabels == Visibility.Visible)
            {
                this.IntializeLabelLines();
                this.labelZIndex = Int16.MaxValue;
                if (this.Panel1.StartDate < this.Panel2.StartDate)
                {
                    this.AdjustLabels(this.Panel1);
                    this.AdjustLabels(this.Panel2);
                }
                else
                {
                    this.AdjustLabels(this.Panel2);
                    this.AdjustLabels(this.Panel1);
                }

                this.DrawFlagSticks();
            }
        }               

        /// <summary>
        /// Initializes the graph.
        /// </summary>
        protected override void InitializeGraph()
        {
            base.InitializeGraph();
            if (this.Minimized)
            {
                this.interpolationLineStartYPosition = this.NonDynamicRightAxisViewPort.ActualHeight / 2;
            }
            else
            {
                this.interpolationLineStartYPosition = this.NonDynamicRightAxisViewPort.ActualHeight - this.InterpolationLineRowHeight;
                if (this.ShowActivities)
                {
                    this.interpolationLineStartYPosition -= (this.activities.Count * this.ActivityRowHeight);
                }
            }            
        }        

        /// <summary>
        /// Sets the start and end dates of the x axis.
        /// </summary>
        protected override void SetAxisDates()
        {
            this.GetAxisDates(out this.dataStartDate, out this.dataEndDate);
            if (this.dataStartDate.HasValue && this.dataEndDate.HasValue)
            {                
                DateTime graphAxisStartDate = AxisHelper.GetAxisStartDate(this.dataStartDate.Value, this.Frequency);
                DateTime graphAxisEndDate = AxisHelper.GetAxisEndDate(this.dataEndDate.Value, this.Frequency);
                AxisHelper.AdjustAxisStartDate(ref graphAxisStartDate, ref graphAxisEndDate, this.Frequency);

                this.AxisStartDate = graphAxisStartDate;
                this.AxisEndDate = graphAxisEndDate;
            }
        }
        
        /// <summary>
        /// Provides the behavior for the Measure pass.
        /// </summary>
        /// <param name="availableSize">The available size that this object can give to child objects. Infinity can be specified as a value to indicate that the object will size to whatever content is available.</param>
        /// <returns>
        /// The size that this object determines it needs during layout, based on its calculations of child object allotted sizes.
        /// </returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            this.PlotAreaHeight = 0;
            if (!this.Minimized)
            {
                this.PlotAreaHeight = this.InterpolationLineRowHeight + this.Padding.Top + this.Padding.Bottom;
                if (this.ShowDataPointLabels == Visibility.Visible)
                {
                    if (this.StackLabels)
                    {
                        this.PlotAreaHeight += this.MaxLabelStackLevels * this.LabelRowHeight;

                        if (this.ShowLabelOvercrowdingNotifications)
                        {
                            this.PlotAreaHeight += this.LabelOvercrowdingNotificationsRowHeight;
                        }
                    }
                    else
                    {
                        this.PlotAreaHeight += this.LabelRowHeight - this.labelOverlayOffset;
                    }
                }                

                if (this.activities.Count > 0 && this.ShowActivities)
                {
                    this.PlotAreaHeight += this.activities.Count * this.ActivityRowHeight;
                }

                if (this.DynamicTopAxisLayerViewport != null && this.DynamicTopAxisLayerViewport.Visibility == Visibility.Visible)
                {
                    this.PlotAreaHeight += this.DynamicTopAxisLayerViewport.Height;
                }
            }            

            Size desired = base.MeasureOverride(availableSize);
            return desired;
        }        
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets the flag stick.
        /// </summary>
        /// <param name="obj">The object for which the associate flag stick needs to be determined.</param>
        /// <returns>Gets the flagstick associated with the element.</returns>
        private static FrameworkElement GetFlagStick(DependencyObject obj)
        {
            return (FrameworkElement)obj.GetValue(FlagStickProperty);
        }

        /// <summary>
        /// Sets the flag stick.
        /// </summary>
        /// <param name="obj">The obj for which flagstick needs to associated.</param>
        /// <param name="value">The falg stick element.</param>
        private static void SetFlagStick(DependencyObject obj, FrameworkElement value)
        {
            obj.SetValue(FlagStickProperty, value);
        }

        /// <summary>
        /// Graph layout properties changed callback method.
        /// </summary>
        /// <param name="dependencyObject">DependencyObject for callback.</param>
        /// <param name="args">The arguments.</param>
        private static void StackLabelsPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            TimeActivityGraph graph = dependencyObject as TimeActivityGraph;
            if (null != graph)
            {
                graph.InvalidateMeasure();
                graph.Refresh(false);
            }
        }

        /// <summary>
        /// Gets the filtered activity data.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns>
        /// Returns the filtered activities data.
        /// </returns>
        private static IEnumerable GetFilteredActivityData(object source, DateTime startDate, DateTime endDate)
        {
            IEnumerable enumerable = null;

            // if the datacontext supports filtering then we should tell it to filter.
            if (null != source)
            {
                ISupportTimeWindow filter = source as ISupportTimeWindow;
                if (null != filter)
                {
                    enumerable = (System.Collections.IEnumerable)filter.Filter(startDate, endDate);
                }
                else
                {
                    // need to manually filter the graph datasource
                    enumerable = DataHelper.Filter(source, startDate, endDate);
                }
            }

            return enumerable;
        }

        /// <summary>
        /// Checks for collision.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>True if collides, otherwise false.</returns>
        private static bool CheckForCollision(FrameworkElement element)
        {
            bool collides = false;

#if SILVERLIGHT
            UIElement transformRoot = Application.Current.RootVisual;

            if (element.ActualHeight == 0 || element.ActualWidth == 0)
            {
                element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            }

            Point startPoint = element.TransformToVisual(transformRoot).Transform(new Point());
            Rect bounds = new Rect(startPoint, element.DesiredSize);
            if (bounds != Rect.Empty)
            {
                List<UIElement> hits = VisualTreeHelper.FindElementsInHostCoordinates(bounds, transformRoot) as List<UIElement>;
                foreach (UIElement hitElement in hits)
                {
                    if (hitElement != element && TimeActivityGraph.GetIsLabel(hitElement) == true)
                    {
                        collides = true;
                    }
                }
            }
#else
            if (element == null)
            {
                collides = true;
            }
#endif
            return collides;
        }

        /// <summary>
        /// Gets the flag stick.
        /// </summary>
        /// <param name="x">The x co-ordinate.</param>
        /// <param name="y1">The y1 co-ordinate.</param>
        /// <param name="y2">The y2 co-ordinate.</param>
        /// <returns>Returns the flag stick.</returns>
        private static FrameworkElement GetFlagStick(double x, double y1, double y2)
        {
            Rectangle flagStick = new Rectangle();           

            Canvas.SetLeft(flagStick, (int)x);
            Canvas.SetTop(flagStick, (int)y1);
            Canvas.SetZIndex(flagStick, 0);

            flagStick.Width = 1;
            flagStick.Height = Math.Abs(y1 - y2);

            flagStick.Fill = new SolidColorBrush(Colors.Black);           

            return flagStick;
        }        

        /// <summary>
        /// Looks the through adjacent panel for overlap.
        /// </summary>
        /// <param name="panel">The panel.</param>
        /// <param name="comparisonPanel">The comparison panel.</param>
        /// <param name="offset">The offset.</param>
        private static new void LookThroughAdjacentPanelForOverlap(PanelWrapper panel, PanelWrapper comparisonPanel, double offset)
        {
            // all we need to do is determine if the items on the edge of the panels overlap.
            // so, really we are talking about a datamarker symbol wide on either edge.
            // the seamedelementcolelction gives us the elements that are plotted
            // outside the bounds of our panel.
            foreach (FrameworkElement element in panel.SeamedElementCollection)
            {
                double elementLeft = Canvas.GetLeft(element);
                double elementWidth = element.Width;
                Collision collision;
                foreach (FrameworkElement comparisonElement in comparisonPanel.SeamedElementCollection)
                {
                    GraphPoint elementDataContext = element.DataContext as GraphPoint;
                    GraphPoint comparisionElementDataContext = comparisonElement.DataContext as GraphPoint;
                    if (elementDataContext != null && comparisionElementDataContext != null)
                    {
                        bool areDataContextsSame = false;
                        areDataContextsSame = elementDataContext.X1 == comparisionElementDataContext.X1;
                        if (elementDataContext.X2.HasValue && comparisionElementDataContext.X2.HasValue)
                        {
                            areDataContextsSame = elementDataContext.X2.Value == comparisionElementDataContext.X2.Value;
                        }
                        else if ((elementDataContext.X2.HasValue && !comparisionElementDataContext.X2.HasValue) || (!elementDataContext.X2.HasValue && comparisionElementDataContext.X2.HasValue))
                        {
                            areDataContextsSame = false;
                        }

                        if (null != element && null != comparisonElement && !areDataContextsSame)
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
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the axis end date.
        /// </summary>
        /// <param name="maxDataDateTime">The max data date time.</param>
        /// <returns>An end date for the axis.</returns>
        private DateTime GetAxisEndDate(DateTime maxDataDateTime)
        {
            DateTime dateTime = DateTime.MinValue;
            if (this.VisibleWindow != null && this.VisibleWindow.Ticks < TimeSpan.FromDays(36500).Ticks)
            {
                if (maxDataDateTime < DateTime.Now + TimeSpan.FromDays(3650))
                {
                    dateTime = maxDataDateTime.AddDays(3650);
                }
                else
                {
                    dateTime = maxDataDateTime.AddDays(365);
                }
            }
            else
            {
                dateTime = maxDataDateTime;
            }

            return dateTime;
        }
       
        /// <summary>
        /// Renders the activities.
        /// </summary>
        /// <param name="panel">The panel.</param>
        private void RenderActivities(PanelWrapper panel)
        {
            CollisionDetectionManager collisionManager = GraphBase.GetCollisionDetectionManager(this.DynamicPlotLayer);
            if (this.activities.Count > 0)
            {
                int activitiesCounter = this.activities.Count;
                foreach (IEnumerable<object> eventset in this.activities)
                {
                    if (panel.AbortLayout)
                    {
                        break;
                    }

                    IEnumerable eventSubSet = GetFilteredActivityData(eventset, panel.StartDate, panel.EndDate);
                    foreach (object o in eventSubSet)
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

                        gp.X1Pixel = (int)this.GetXForDateInPanel(gp.X1, panel.StartDate);
                        gp.X2Pixel = (int)(gp.X2.HasValue ? this.GetXForDate(gp.X2.Value) : this.GetXForDate(this.AxisEndDate));
                        gp.Y1Pixel = this.NonDynamicRightAxisViewPort.ActualHeight - (activitiesCounter * this.ActivityRowHeight);

                        bool addActivityToPanel = false;
                        if (gp.X1Pixel >= 0 && gp.X1Pixel <= this.DynamicMainLayerViewport.ActualWidth)
                        {
                            addActivityToPanel = true;
                        }
                        else if (gp.X2.HasValue && gp.X2Pixel >= 0 && gp.X2Pixel <= this.DynamicMainLayerViewport.ActualWidth)
                        {
                            addActivityToPanel = true;
                        }

                        if (!addActivityToPanel)
                        {
                            continue;
                        }

                        FrameworkElement element = this.AddMarkerFromGraphPoint(gp, true);
                        double left = gp.X1Pixel + GraphBase.GetXOffset(element);
                        double right = left + element.Width;
                        Canvas.SetLeft(element, left);
                        Canvas.SetTop(element, gp.Y1Pixel + GraphBase.GetYOffset(element));
                        element.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Activity_MouseLeftButtonDown);

                        if ((left < 0 && right > 0) || (left < this.DynamicMainLayerViewport.ActualWidth && right > this.DynamicMainLayerViewport.ActualWidth))
                        {
                            this.CurrentWorkingPanel.SeamedElementCollection.Add(element);
                        }

                        if (this.DetectCollisions)
                        {
                            FrameworkElement clashElement = collisionManager.RegisterAndTestIfEventMarkerOverlaps(element);
                            if (clashElement != null)
                            {                                
                                panel.CollisionCollection.Add(new Collision(clashElement, element, true));
                            }
                        }
                    }

                    activitiesCounter--;

                    if (this.DetectCollisions)
                    {
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
                            // if we are renewing both panels, then do not do collisions detections on seams.
                            if (true != this.RenewingBothPanels)
                            {
                                otherPanel = this.Panel1;
                                markerOffsetToUse = this.Panel2.Width * -1;
                            }
                        }

                        // this gets run if we are renewing a panel, but if renewing both, only for panel 1.
                        if (null != otherPanel)
                        {
                            TimeActivityGraph.LookThroughAdjacentPanelForOverlap(this.CurrentWorkingPanel, otherPanel, markerOffsetToUse);
                        }

                        double lastCollisionPosition = -1;

                        foreach (Collision collision in panel.CollisionCollection)
                        {
                            collision.ClusterStartElement.Visibility = Visibility.Collapsed;
                            collision.ClusterEndElement.Visibility = Visibility.Collapsed;
                            FrameworkElement flagStick = GetFlagStick(collision.ClusterStartElement);
                            if (flagStick != null)
                            {
                                flagStick.Visibility = Visibility.Collapsed;
                            }

                            flagStick = GetFlagStick(collision.ClusterEndElement);
                            if (flagStick != null)
                            {
                                flagStick.Visibility = Visibility.Collapsed;
                            }

                            if (null != this.CollisionTemplate)
                            {
                                FrameworkElement collisionIcon = this.CollisionTemplate.LoadContent() as FrameworkElement;
                                if (null != collisionIcon)
                                {
                                    double localxPosition = Canvas.GetLeft(collision.ClusterStartElement);
                                    double localyPosition = Canvas.GetTop(collision.ClusterStartElement);

                                    if (lastCollisionPosition == -1 || localxPosition > lastCollisionPosition + (1.5 * collisionIcon.Width))
                                    {
                                        if (true == collision.ClusterShowingIcon)
                                        {
                                            lastCollisionPosition = localxPosition;
                                            Canvas.SetLeft(collisionIcon, lastCollisionPosition);
                                            Canvas.SetTop(collisionIcon, localyPosition);
                                            this.DynamicPlotLayer.Children.Add(collisionIcon);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
                
        /// <summary>
        /// Adds the marker from graph point.
        /// </summary>
        /// <param name="gp">The GraphPoint.</param>
        /// <param name="renderFlagStick">If set to <c>true</c> [render flag stick].</param>
        /// <returns>The marker plotted from the graphpoint template.</returns>
        private FrameworkElement AddMarkerFromGraphPoint(GraphPoint gp, bool renderFlagStick)
        {
            FrameworkElement marker = null;
            if (gp.DataMarkerTemplate != null)
            {
                marker = gp.DataMarkerTemplate.LoadContent() as FrameworkElement;
                
                marker.DataContext = gp;
                marker.MaxWidth = this.DynamicMainLayerViewport.ActualWidth;
                this.DynamicPlotLayer.Children.Add(marker);

                if (renderFlagStick)
                {
                    FrameworkElement flagStick = GetFlagStick(gp.X1Pixel, this.interpolationLineStartYPosition, gp.Y1Pixel + GraphBase.GetYOffset(marker));
                    SetFlagStick(marker, flagStick);
                    this.DynamicPlotLayer.Children.Add(flagStick);
                }
            }

            return marker;
        }

        /// <summary>
        /// Adds the label element.
        /// </summary>
        /// <param name="gp">GraphPoint containing the co-ordinates for the label.</param>
        /// <param name="panel">The panel to add the label element.</param>
        private void AddLabelElement(GraphPoint gp, PanelWrapper panel)
        {
            FrameworkElement labelElement = null;
            if (gp.Label == null)
            {                
                if (null != this.LabelTemplate)
                {
                    labelElement = this.LabelTemplate.LoadContent() as FrameworkElement;
                }
            }
            else
            {
                labelElement = gp.Label;
            }

            if (null != labelElement)
            {
                labelElement.DataContext = gp;
                TimeActivityGraph.SetIsLabel(labelElement, true);
                panel.LabelElements.Add(labelElement);
                labelElement.Visibility = this.ShowDataPointLabels;
                labelElement.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.LabelElement_MouseLeftButtonDown);                
            }
        }

        /// <summary>
        /// Draws the flag sticks.
        /// </summary>
        private void DrawFlagSticks()
        {
            System.Collections.ObjectModel.Collection<UIElement> flagSticks = new System.Collections.ObjectModel.Collection<UIElement>();
            foreach (FrameworkElement label in this.labelsLayer.Children)
            {
                double left = (int)Canvas.GetLeft(label);
                double top = (int)Canvas.GetTop(label);
                double height = this.interpolationLineStartYPosition;

                FrameworkElement flagStick = GetFlagStick(left, top, height);
                SetFlagStick(label, flagStick);
                flagSticks.Add(flagStick);
            }

            foreach (UIElement element in flagSticks)
            {                
                this.labelsLayer.Children.Add(element);
            }
        }
        
        /// <summary>
        /// Adjusts the labels.
        /// </summary>
        /// <param name="panel">The panel.</param>
        private void AdjustLabels(PanelWrapper panel)
        {
            foreach (MedicationLabel label in panel.LabelElements)
            {
                GraphPoint gp = label.DataContext as GraphPoint;
                object actualData = gp.DataContext;
                
                if (!this.labelsLayerData.Contains(actualData))
                {
                    this.labelsOverlapping = false;
                    this.labelsLayerData.Add(actualData);

                    DateTime startDate = gp.X1;
                    DateTime endDate = gp.X1;

                    if (gp.X2.HasValue)
                    {
                        endDate = gp.X2.Value;
                    }
                    else
                    {
                        endDate = this.TitleEndDate > endDate ? this.TitleEndDate : endDate;
                    }

                    bool showInView = false;

                    if (startDate >= this.TitleStartDate && startDate <= this.TitleEndDate)
                    {
                        showInView = true;
                    }
                    else if (endDate >= this.TitleStartDate && endDate <= this.TitleEndDate)
                    {
                        showInView = true;
                        startDate = this.TitleStartDate;
                    }
                    else if (startDate < this.TitleStartDate && endDate >= this.TitleEndDate)
                    {
                        showInView = true;
                        startDate = this.TitleStartDate;
                    }

                    if (showInView)
                    {
                        label.Mode = this.LabelMode;
                        label.Visibility = this.ShowDataPointLabels;
                        label.Margin = new Thickness();
                        label.ClearValue(FrameworkElement.WidthProperty);

                        this.labelsLayer.Children.Add(label);
                        
                        double top;
                        double left = this.GetXForDateInPanel(startDate, panel.StartDate) + Canvas.GetLeft(panel);
                        
                        left = Math.Max(0, left);                        

                        double width = label.GetDesiredWidth(this.DynamicMainLayerViewport.ActualWidth - left); // label.DesiredSize.Width;
                        label.Width = width;
                        
                        if (this.StackLabels)
                        {                            
                            top = this.GetLabelTop(left, width);
                            Canvas.SetZIndex(label, this.labelZIndex);
                            GraphBase.SetDataPointOverlapProperty(label, false);
                        }
                        else
                        {
                            top = this.GetLabelTop(0);
                            double newLeft = this.GetLabelLeft(left, width, label);
                            if (newLeft != left)
                            {
                                FrameworkElement previousLabel = this.GetLabelAtLeft(newLeft);
                                if (previousLabel != null)
                                {
                                    label.Width = previousLabel.Width;
                                }

                                label.GetDesiredWidth(this.DynamicMainLayerViewport.ActualWidth - newLeft);

                                Canvas.SetZIndex(label, --this.labelZIndex);                                
                                label.Margin = new Thickness(newLeft - left, this.labelOverlayOffset, label.Margin.Right, label.Margin.Bottom);
                                GraphBase.SetDataPointOverlapProperty(label, true);
                            }
                            else
                            {
                                this.labelZIndex = Int16.MaxValue;
                                Canvas.SetZIndex(label, this.labelZIndex);
                                label.Margin = new Thickness(0, 0, label.Margin.Right, label.Margin.Bottom);
                                GraphBase.SetDataPointOverlapProperty(label, false);
                            }
                        }                        

                        if (!double.IsNaN(top))
                        {
                            Canvas.SetLeft(label, (int)left);
                            Canvas.SetTop(label, (int)top);
                        }

                        if (this.labelsOverlapping)
                        {
                            this.labelsLayer.Children.Remove(label);
                            this.AddLabelOvercrowdingNotification(left);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adds the label overcrowding notification.
        /// </summary>
        /// <param name="left">The left co-ordinate for the overcrowding notifcation.</param>
        private void AddLabelOvercrowdingNotification(double left)
        {
            if (this.ShowLabelOvercrowdingNotifications)
            {
                FrameworkElement overcrowdingIcon = null;
                if (this.CollisionTemplate != null)
                {
                    overcrowdingIcon = this.CollisionTemplate.LoadContent() as FrameworkElement;
                    double top = this.GetLabelTop(this.MaxLabelStackLevels - 1) - this.LabelOvercrowdingNotificationsRowHeight;
                    Canvas.SetLeft(overcrowdingIcon, left);
                    Canvas.SetTop(overcrowdingIcon, top);
                    this.labelsLayer.Children.Add(overcrowdingIcon);
                }
            }
        }

        /// <summary>
        /// Intializes the label lines.
        /// </summary>
        private void IntializeLabelLines()
        {
            this.labelStartYPosition = this.NonDynamicRightAxisViewPort.ActualHeight - this.InterpolationLineRowHeight - this.LabelRowHeight;
            if (this.ShowActivities && this.Activities.Count > 0)
            {
                this.labelStartYPosition -= this.activities.Count * this.ActivityRowHeight;
            }

            this.labelLines.Clear();

            for (int i = 0; i < this.MaxLabelStackLevels; i++)
            {
                this.labelLines.Add(i, new LabelLineDetails());
            }
        }

        /// <summary>
        /// Gets the label left.
        /// </summary>
        /// <param name="left">The left of the label.</param>
        /// <param name="width">The width of the label.</param>
        /// <param name="label">The label.</param>
        /// <returns>Gets the left position of the label.</returns>
        /// <remarks>This is used when StackLabels is false.</remarks>
        private double GetLabelLeft(double left, double width, FrameworkElement label)
        {
            double leftPos = 0;

            LabelLineDetails labelLine = this.labelLines[0];
            if (labelLine.MinX == 0 && labelLine.MaxX == 0)
            {
                labelLine.Label = label;
            }

            if (left > labelLine.MaxX)
            {                
                leftPos = left;
                labelLine.Label = label;          
            }
            else
            {
                leftPos = labelLine.MinX;
                FrameworkElement prevLabel = this.GetLabelAtLeft(leftPos);
                if (prevLabel != null)
                {
                    width = prevLabel.Width;
                }
            }

            labelLine.MinX = leftPos;
            labelLine.MaxX = Math.Ceiling(Math.Max(leftPos + width, labelLine.MaxX));
            return leftPos;
        }

        /// <summary>
        /// Gets the label at left.
        /// </summary>
        /// <param name="left">The left co-ordinate.</param>
        /// <returns>Label element at the left position.</returns>
        private FrameworkElement GetLabelAtLeft(double left)
        {
            FrameworkElement label = null;

            LabelLineDetails labelLine = this.labelLines[0];
            if (left == labelLine.MinX)
            {
                label = labelLine.Label;
            }

            return label;
        }

        /// <summary>
        /// Gets the label top.
        /// </summary>
        /// <param name="left">The left of the label.</param>
        /// <param name="width">The width of the label.</param>
        /// <returns>Gets the top position for the label.</returns>
        /// <remarks>This is used when StackLabels is true.</remarks>
        private double GetLabelTop(double left, double width)
        {
            double top = 0;
            int row = -1;
            
            for (int i = 0; i < this.labelLines.Count; i++)
            {
                LabelLineDetails labelLine = this.labelLines[i];

                if (left > labelLine.MaxX || left <= labelLine.MinX)
                {
                    labelLine.MinX = left;
                    labelLine.MaxX = left + width;
                    row = i;
                    
                    break;
                }
            }

            if (row != -1)
            {
                top = this.GetLabelTop(row);
            }
            else
            {
                this.labelsOverlapping = true;
                top = double.NaN;
            }

            return top;
        }

        /// <summary>
        /// Gets the label top.
        /// </summary>
        /// <param name="row">The row in which the label will be placed.</param>
        /// <returns>Gets the top position for the label based on the row in which the label will be placed.</returns>
        private double GetLabelTop(int row)
        {
            return this.labelStartYPosition - (row * this.LabelRowHeight);
        }       

        /// <summary>
        /// Calculates the axis dates.
        /// </summary>
        /// <param name="data">The dataset to be used to determine start and end dates.</param>
        /// <remarks>Can be the data context or the activity data.</remarks>
        private void CalculateAxisDates(IEnumerable data)
        {
            if (data != null)
            {
                foreach (object o in data)
                {
                    GraphPoint gp = this.GetBoundGraphPoint(o);
                    if (gp != null)
                    {
                        if (!this.dataStartDate.HasValue || this.dataStartDate.Value > gp.X1)
                        {
                            this.dataStartDate = gp.X1;
                        }

                        if (!this.dataEndDate.HasValue || this.dataEndDate.Value < gp.X2)
                        {
                            this.dataEndDate = gp.X2;
                        }                        
                    }
                }
            }
        }       
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the SizeChanged event of the LabelsViewport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        private void LabelsViewport_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RectangleGeometry clipBounds = new RectangleGeometry();
            clipBounds.Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height);

            FrameworkElement viewportElement = sender as FrameworkElement;
            viewportElement.Clip = clipBounds;
        }

        /// <summary>
        /// Handles the CollectionChanged event of the Activities.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void Activities_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.InvalidateMeasure();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the LabelElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void LabelElement_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!GraphBase.GetDataPointOverlapProperty(sender as DependencyObject))
            {
                // only raise event when the labels dont overlap.
                if (this.LabelClick != null)
                {
                    this.Focus();
                    this.LabelClick(sender, e);
                }
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Activity control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Activity_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.ActivityClick != null)
            {
                this.Focus();
                this.ActivityClick(sender, e);
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the InterpolationLine control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void InterpolationLine_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (this.InterpolationLineClick != null)
            {
                this.Focus();
                this.InterpolationLineClick(sender, e);
            }
        }
        #endregion        

        #region Sub Classes
        /// <summary>
        /// Class to hold the details about the labels co-ordinates in a line.
        /// </summary>
        private class LabelLineDetails
        {
            /// <summary>
            /// Gets or sets the min X.
            /// </summary>
            /// <value>The min X.</value>
            public double MinX
            {
                get;
                set;
            }

            /// <summary>
            /// Gets or sets the max X.
            /// </summary>
            /// <value>The max X.</value>
            public double MaxX
            {
                get;
                set;
            }

            /// <summary>
            /// Gets or sets the label.
            /// </summary>
            /// <value>The label.</value>
            /// <remarks>Label element last plotted in the line.</remarks>
            public FrameworkElement Label
            {
                get;
                set;
            }
        }
        #endregion
    }
}
