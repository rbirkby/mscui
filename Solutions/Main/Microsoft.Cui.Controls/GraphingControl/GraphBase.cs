//-----------------------------------------------------------------------
// <copyright file="GraphBase.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Base class for all the graphs.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    #endregion

    /// <summary>
    /// Base class for the graphs. This class is abstract and cannot be instantiated.
    /// </summary>
    public class GraphBase : PanelViewer
    {
        #region Dependency Properties
        
        /// <summary>
        /// Identifies the dependency property for x offset.
        /// </summary>
        public static readonly DependencyProperty XOffsetProperty =
            DependencyProperty.RegisterAttached("XOffset", typeof(double), typeof(GraphBase), null);

        /// <summary>
        /// Identifies the dependency property for y offset.
        /// </summary>
        public static readonly DependencyProperty YOffsetProperty =
            DependencyProperty.RegisterAttached("YOffset", typeof(double), typeof(GraphBase), null);
        
        /// <summary>
        /// Identifies the dependency property for data point overlap property.
        /// </summary>
        public static readonly DependencyProperty DataPointOverlapPropertyProperty =
            DependencyProperty.RegisterAttached("DataPointOverlapProperty", typeof(bool), typeof(GraphBase), null);

        /// <summary>
        /// Identifies the dependency property for Snap To Pixels Behaviour.
        /// </summary>
        public static readonly DependencyProperty SnapToPixelsProperty =
            DependencyProperty.RegisterAttached("SnapToPixels", typeof(bool), typeof(GraphBase), null);

        /// <summary>
        /// Identifies the dependency property for data point label.
        /// </summary>
        public static readonly DependencyProperty DataPointLabelProperty = DependencyProperty.RegisterAttached(
                 "DataPointLabel",
                 typeof(FrameworkElement),
                 typeof(GraphBase),   
                 null);

        /// <summary>
        /// Identifies the dependency property for data point.
        /// </summary>
        public static readonly DependencyProperty DataPointProperty =
            DependencyProperty.RegisterAttached("DataPoint", typeof(bool), typeof(GraphBase), null);

        /// <summary>
        /// Identifies the dependency property for collision detection manager.
        /// </summary>
        public static readonly DependencyProperty CollisionDetectionManagerProperty =
            DependencyProperty.RegisterAttached("CollisionDetectionManager", typeof(CollisionDetectionManager), typeof(GraphBase), null);

        /// <summary>
        /// Identifies the dependency property for the last item.
        /// </summary>
        public static readonly DependencyProperty LastItemProperty =
            DependencyProperty.RegisterAttached("LastItem", typeof(bool), typeof(GraphBase), null);

        /// <summary>
        /// Identifies the dependency property for second to last property.
        /// </summary>
        public static readonly DependencyProperty SecondToLastProperty =
            DependencyProperty.RegisterAttached("SecondTolast", typeof(bool), typeof(GraphBase), null);

        #endregion

        #region Template Part Names
        /// <summary>
        /// Plot layer template element name.
        /// </summary>
        internal const string DynamicMainLayerTemplateElementName = "ELEMENT_dynamicMainLayerTemplate";

        /// <summary>
        /// Collision template element name.
        /// </summary>
        internal const string CollisionTemplateElementName = "ELEMENT_collisionTemplate";

        /// <summary>
        /// Y axis labels layer element name.
        /// </summary>
        internal const string NonDynamicRightAxisLabelsLayerElementName = "ELEMENT_NonDynamicRightAxisLabels";

        /// <summary>
        /// Y axis grid lines layer element name.
        /// </summary>
        internal const string NonDynamicRightAxisLinesLayerElementName = "ELEMENT_NonDynamicRightAxisLines";

        /// <summary>
        /// Minimize to title element template name.
        /// </summary>
        internal const string MinimizeToTitleElementName = "ELEMENT_MinimizeToTitle";

        /// <summary>
        /// Scale to fit element template name.
        /// </summary>
        internal const string ScaleToFitElementName = "ELEMENT_ScaleToFit";

        /// <summary>
        /// Title area element name.
        /// </summary>
        internal const string TitleAreaElementName = "ELEMENT_TitleArea";

        /// <summary>
        /// Start date element in the title area.
        /// </summary>
        internal const string TitleStartDateElementName = "ELEMENT_XAxisTitleStartDate";

        /// <summary>
        /// End date element in the title area.
        /// </summary>
        internal const string TitleEndDateElementName = "ELEMENT_XAxisTitleEndDate";
        #endregion

        #region Private Members
        /// <summary>
        /// Member variable to hold if collapsed to title.
        /// </summary>
        private bool collapsedTitle;

        /// <summary>
        /// Member variable to hold previous Height.
        /// </summary>
        private double previousHeight;

        /// <summary>
        /// Member variable to hold plotting canvas.
        /// </summary>
        private FrameworkElement dynamicMainLayerHost;

        /// <summary>
        /// Member variable to hold non Dynamic Right Axis canvas.
        /// </summary>
        private Canvas nonDynamicRightAxisLabelsLayer;

        /// <summary>
        /// Member variable to hold data template for main layer.
        /// </summary>
        private DataTemplate dynamicMainLayerTemplate;

        /// <summary>
        /// Member variable to hold data template for point.
        /// </summary>
        private DataTemplate pointTemplate;

        /// <summary>
        /// Member variable to hold graph summary.
        /// </summary>
        private GraphSummary graphSummary = new GraphSummary();        
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphBase"/> class.
        /// </summary>
        public GraphBase()
        {
            this.ShowXAxisMinorIntervalTicks = true;
            this.ShowYAxisMinorIntervalTicks = true;
            this.MajorAxisTickLength = 8;
            this.MinorAxisTickLength = 4;
        }        
        #endregion

        /// <summary>
        /// Occurs when the ScaleToFit button is clicked.
        /// </summary>
        public event RoutedEventHandler ScaleToFitClick;

        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether [debug data point markers].
        /// </summary>
        /// <value>Turns on the debug data point markers.</value>
        [Category("Graph Appearance")]
        public bool DebugDataPointMarkers
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the label template.
        /// </summary>
        /// <value>The label template.</value>
        [Category("Customization")]
        public DataTemplate LabelTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the label transform.
        /// </summary>
        /// <value>The label transform.</value>
        [Category("Customization")]
        public Transform LabelTransform
        {
            get;
            set;
        }        

        /// <summary>
        /// Gets the summary of the graph.
        /// </summary>
        /// <value>Summary of the graph.</value>
        public GraphSummary GraphSummary
        {
            get { return this.graphSummary; }
            internal set { this.graphSummary = value; }
        }

        /// <summary>
        /// Gets or sets the data template to be applied for data point.
        /// </summary>
        /// <value>Data template for the data point.</value>
        [Category("Customization")]
        public DataTemplate PointTemplate
        {
            get { return this.pointTemplate; }
            set { this.pointTemplate = value; }
        }         
        
        /// <summary>
        /// Gets or sets the template.
        /// </summary>
        /// <value>Data template for the collision.</value>
        [Category("Customization")]
        public DataTemplate CollisionTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether minor interval ticks should be shown in X axis.
        /// </summary>
        /// <value>Value indicating whether minor interval ticks should be shown in X axis.</value>
        [Category("Graph Appearance")]
        public bool ShowXAxisMinorIntervalTicks
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether minor interval ticks should be shown in Y axis.
        /// </summary>
        /// <value>Value indicating whether minor interval ticks should be shown in Y axis.</value>
        [Category("Graph Appearance")]
        public bool ShowYAxisMinorIntervalTicks
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating the length of a tick for major interval.
        /// </summary>
        /// <value>Length of a tick for major interval.</value>
        [Category("Axis Details")]
        public double MajorAxisTickLength
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating the length of a tick for minor interval.
        /// </summary>
        /// <value>Length of a tick for minor interval.</value>
        [Category("Axis Details")]
        public double MinorAxisTickLength
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether ticks should be plotted before labels.
        /// </summary>
        /// <value>Value indicating whether ticks should be plotted before labels.</value>
        [Category("Axis Details")]
        public bool PlotTicksBeforeLabels
        {
            get;
            set;
        }
        #endregion

        #region Internal Properties
        /// <summary>
        /// Gets or sets a value indicating whether the Y axis being scaled to fit.
        /// </summary>
        /// <value>Value indicating whether the Y axis being scaled to fit.</value>
        internal bool ScaleToFit
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets the start date for the graph title.
        /// </summary>
        /// <value>Start date for the graph title.</value>
        internal DateTime TitleStartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the end date for the graph title.
        /// </summary>
        /// <value>End date for the graph title.</value>
        internal DateTime TitleEndDate
        {
            get;
            set;
        }
                
        /// <summary>
        /// Gets or sets the textblock element displaying start date in the title.
        /// </summary>
        internal TextBlock TitleStartDateTextBlock
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the textblock element displaying end date in the title.
        /// </summary>
        internal TextBlock TitleEndDateTextBlock
        {
            get;
            set;
        }
        #endregion

        #region Protected Properties       
        /// <summary>
        /// Gets or sets the data template.
        /// </summary>
        /// <value>Data template for the main layer.</value>
        protected DataTemplate DynamicMainLayerTemplate
        {
            get { return this.dynamicMainLayerTemplate; }
            set { this.dynamicMainLayerTemplate = value; }
        }

        /// <summary>
        /// Gets or sets the title area in the graph style.
        /// </summary>
        /// <value>Title are in the graph style.</value>
        protected FrameworkElement TitleArea
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the scale for the y axis.
        /// </summary>
        /// <value>Scale factor for Y axis.</value>
        protected double YAxisScale
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the button to minimize to title.
        /// </summary>
        /// <value>The Element to minimize to title.</value>
        protected ToggleButton MinimizeToTitle
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the button to scale to fit.
        /// </summary>
        /// <value>The Element to scale to fit.</value>
        protected ToggleButton ScaleToFitElement
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the plotting layer.
        /// </summary>
        /// <value>Plotting layer canvas.</value>
        protected FrameworkElement DynamicMainLayer
        {
            get { return this.dynamicMainLayerHost; }
            set { this.dynamicMainLayerHost = value; }
        }

        /// <summary>
        /// Gets or sets the Non Dynamic Right Axis layer.
        /// </summary>
        /// <value>Canvas of the layer.</value>
        protected Canvas NonDynamicRightAxisLabelsLayer
        {
            get { return this.nonDynamicRightAxisLabelsLayer; }
            set { this.nonDynamicRightAxisLabelsLayer = value; }
        }

        /// <summary>
        /// Gets or sets the non dynamic right axis lines layer.
        /// </summary>
        /// <value>The non dynamic right axis lines layer.</value>
        protected Canvas NonDynamicRightAxisLinesLayer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether Static layer content has been loaded.
        /// </summary>
        /// <value>Boolean indicating whether the static layer content has been loaded.</value>
        protected bool StaticLayerContentLoaded
        {
            get;
            set;
        }
        #endregion

        #region Get/Set Attached properties

        /// <summary>
        /// Gets the last item.
        /// </summary>
        /// <param name="theObject">The object.</param>
        /// <returns>The last item for the graph.</returns>
        public static bool GetLastItem(DependencyObject theObject)
        {
            return (bool)theObject.GetValue(LastItemProperty);
        }

        /// <summary>
        /// Sets the last item.
        /// </summary>
        /// <param name="theObject">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetLastItem(DependencyObject theObject, bool value)
        {
            theObject.SetValue(LastItemProperty, value);
        }

        /// <summary>
        /// Gets the second tolast property.
        /// </summary>
        /// <param name="objectValue">The object value.</param>
        /// <returns>True if the item is the second to last item.</returns>
        public static bool GetSecondToLast(DependencyObject objectValue)
        {
            return (bool)objectValue.GetValue(SecondToLastProperty);
        }

        /// <summary>
        /// Sets the second tolast property.
        /// </summary>
        /// <param name="objectValue">The object value.</param>
        /// <param name="value">If set to <c>true</c> [value].</param>
        public static void SetSecondToLast(DependencyObject objectValue, bool value)
        {
            objectValue.SetValue(SecondToLastProperty, value);
        }

        /// <summary>
        /// Gets the X offset.
        /// </summary>
        /// <param name="objectValue">The object.</param>
        /// <returns>The X Offset.</returns>
        public static double GetXOffset(DependencyObject objectValue)
        {
            return (double)objectValue.GetValue(XOffsetProperty);
        }

        /// <summary>
        /// Sets the X offset.
        /// </summary>
        /// <param name="objectValue">The object.</param>
        /// <param name="value">The X value.</param>
        public static void SetXOffset(DependencyObject objectValue, double value)
        {
            objectValue.SetValue(XOffsetProperty, value);
        }

        /// <summary>
        /// Gets the data point overlap property.
        /// </summary>
        /// <param name="objectValue">The object value.</param>
        /// <returns>The data point overlap.</returns>
        public static bool GetDataPointOverlapProperty(DependencyObject objectValue)
        {
            return (bool)objectValue.GetValue(DataPointOverlapPropertyProperty);
        }

        /// <summary>
        /// Sets the data point overlap property.
        /// </summary>
        /// <param name="objectValue">The object.</param>
        /// <param name="value">Sets the overlap property on the element.</param>
        public static void SetDataPointOverlapProperty(DependencyObject objectValue, bool value)
        {
            objectValue.SetValue(DataPointOverlapPropertyProperty, value);
        }

        /// <summary>
        /// Gets the Y offset.
        /// </summary>
        /// <param name="objectValue">The object.</param>
        /// <returns>The Y Offset used.</returns>
        public static double GetYOffset(DependencyObject objectValue)
        {
            return (double)objectValue.GetValue(YOffsetProperty);
        }

        /// <summary>
        /// Sets the Y offset.
        /// </summary>
        /// <param name="objectValue">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetYOffset(DependencyObject objectValue, double value)
        {
            objectValue.SetValue(YOffsetProperty, value);
        }

        /// <summary>
        /// Gets the Data Point Label.
        /// </summary>
        /// <param name="objectValue">The object.</param>
        /// <returns>The data point.</returns>
        public static FrameworkElement GetDataPointLabel(DependencyObject objectValue)
        {
            return (FrameworkElement)objectValue.GetValue(DataPointLabelProperty);
        }

        /// <summary>
        /// Gets the data point.
        /// </summary>
        /// <param name="objectValue">The object.</param>
        /// <returns>The data point.</returns>
        public static bool GetDataPoint(DependencyObject objectValue)
        {
            return (bool)objectValue.GetValue(DataPointProperty);
        }

        /// <summary>
        /// Sets the data point.
        /// </summary>
        /// <param name="objectValue">The object.</param>
        /// <param name="value">If set to <c>true</c> [value].</param>
        public static void SetDataPoint(DependencyObject objectValue, bool value)
        {
            objectValue.SetValue(DataPointProperty, value);
        }

        /// <summary>
        /// Sets the data point label.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="label">The label.</param>
        public static void SetDataPointLabel(DependencyObject element, FrameworkElement label)
        {
            element.SetValue(GraphBase.DataPointLabelProperty, label);
        }

        /// <summary>
        /// Gets the CollisionDetectionManager.
        /// </summary>
        /// <param name="objectValue">The object.</param>
        /// <returns>The CollisionDetectionManager.</returns>
        public static CollisionDetectionManager GetCollisionDetectionManager(DependencyObject objectValue)
        {
            return (CollisionDetectionManager)objectValue.GetValue(CollisionDetectionManagerProperty);
        }

        /// <summary>
        /// Sets the CollisionDetectionManager.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="collisionDetectionManager">The CollisionDetectionManager.</param>
        public static void SetCollisionDetectionManager(DependencyObject element, CollisionDetectionManager collisionDetectionManager)
        {
            element.SetValue(GraphBase.CollisionDetectionManagerProperty, collisionDetectionManager);
        }

        /// <summary>
        /// Gets the Snap To Pixels Behaviour.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The Snap To Pixels Behaviour Indicator.</returns>
        public static bool GetSnapToPixels(DependencyObject element)
        {
            object result = element.GetValue(SnapToPixelsProperty);
            return result == null ? false : (bool)result;
        }

        /// <summary>
        /// Sets the Snap To Pixels Behaviour.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The Snap To Pixels Behaviour indicator.</param>
        public static void SetSnapToPixels(DependencyObject element, bool value)
        {
            element.SetValue(SnapToPixelsProperty, value);
        }

        #endregion
                
        #region Public Methods
        /// <summary>
        /// Overridden. Applies the.Specified template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.DynamicMainLayerTemplate = StyleParser.FindResource<DataTemplate>(this.LayoutRoot, DynamicMainLayerTemplateElementName, true);
            this.NonDynamicRightAxisLabelsLayer = this.GetTemplateChild<Canvas>(NonDynamicRightAxisLabelsLayerElementName, true);
            this.NonDynamicRightAxisLinesLayer = this.GetTemplateChild<Canvas>(NonDynamicRightAxisLinesLayerElementName, true);
            this.TitleStartDateTextBlock = this.GetTemplateChild<TextBlock>(TitleStartDateElementName, true);
            this.TitleEndDateTextBlock = this.GetTemplateChild<TextBlock>(TitleEndDateElementName, true);

            this.CollisionTemplate = StyleParser.FindResource<DataTemplate>(this.LayoutRoot, CollisionTemplateElementName, false);
            this.TitleArea = this.GetTemplateChild<FrameworkElement>(TitleAreaElementName, false);
            this.MinimizeToTitle = this.GetTemplateChild<ToggleButton>(MinimizeToTitleElementName, false);
            this.ScaleToFitElement = this.GetTemplateChild<ToggleButton>(ScaleToFitElementName, false);

            if (this.MinimizeToTitle != null)
            {
                this.MinimizeToTitle.Click += new RoutedEventHandler(this.MinimizeToTitle_Click);    
            }

            if (this.ScaleToFitElement != null)
            {
                this.ScaleToFitElement.Click += new RoutedEventHandler(this.ScaleToFit_Click);    
            }            
        }

        /// <summary>
        /// Resets the graph. Restores the default layout of the graph.
        /// </summary>
        public virtual void Reset()
        {
            if (this.ScaleToFit == true && this.ScaleToFitElement != null)
            {
                this.ScaleToFitElement.IsChecked = false;
                this.ScaleToFit_Click(this, null);
            }

            if (this.collapsedTitle == true && this.MinimizeToTitle != null)
            {
                this.MinimizeToTitle.IsChecked = false;
                this.MinimizeToTitle_Click(this, null);
            }

            this.graphSummary.Show = true;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Gets the panel at the specified offset.
        /// </summary>
        /// <param name="offset">Offset distance.</param>
        /// <returns>Returns panel at the specified offset.</returns>
        protected override PanelWrapper GetPanel(double offset)
        {
            // cast this so the start position is always a full page from the start.            
            return this.DrawGraph((long)offset);
        }
        
        /// <summary>
        /// Plots the graph at specified offset.
        /// </summary>
        /// <param name="offset">Offset at which to plot the graph.</param>
        /// <returns>Returns the panel at the specified offset.</returns>
        protected PanelWrapper DrawGraph(long offset)
        {
            PanelWrapper panel = null;
                        
            panel = new PanelWrapper();
            panel.Height = this.DynamicMainLayerViewport.ActualHeight;
            panel.Width = this.DynamicMainLayerViewport.ActualWidth;

            this.DynamicMainLayer = this.DynamicMainLayerTemplate.LoadContent() as FrameworkElement;

            panel.Children.Add(this.DynamicMainLayer);
            
            this.DynamicMainLayerViewport.Children.Add(panel);

            panel.Tag = offset;

            return panel;
        }

        /// <summary>
        /// Handles the keypressed event on the control.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!e.Handled)
            {
                bool ctrlKeyPressed;
                bool shiftKeyPressed;
                bool altKeyPressed;

                KeyboardHelper.GetMetaKeyState(out ctrlKeyPressed, out shiftKeyPressed, out altKeyPressed);
                                
                if (ctrlKeyPressed && shiftKeyPressed && !altKeyPressed)
                {
                    switch (e.Key)
                    {
                        case Key.D1:
                            if (this.ScaleToFitElement != null)
                            {
                                this.ScaleToFit_Click(this, null);
                                this.ScaleToFitElement.IsChecked = !this.ScaleToFitElement.IsChecked;
                            }

                            e.Handled = true;
                            break;
                        case Key.D2:
                            if (this.MinimizeToTitle != null)
                            {
                                this.MinimizeToTitle_Click(this, null);
                                this.MinimizeToTitle.IsChecked = !this.MinimizeToTitle.IsChecked;
                            }

                            e.Handled = true;
                            break;                        
                    }
                }
            }
        }       
        #endregion

        #region Event handlers
        /// <summary>
        /// Handles the Click event of the MinimizeToTitle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void MinimizeToTitle_Click(object sender, RoutedEventArgs e)
        {
            if (true == this.collapsedTitle)
            {
                this.collapsedTitle = false;
                this.Height = this.previousHeight;
            }
            else
            {
                this.collapsedTitle = true;
                RowDefinition rd = this.LayoutRoot.RowDefinitions[0];
                if (null != rd)
                {                    
                    this.previousHeight = this.ActualHeight;
                    this.Height = rd.ActualHeight + 1;
                }
            }

            if (e != null)
            {
                // Will set the focus only if the button is clicked.
                // Will not set the focus, if invoked from the code directly.
                this.Focus();
            }
        }

        /// <summary>
        /// Handles the click event of ScaleToFit button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args containing instance data.</param>
        private void ScaleToFit_Click(object sender, RoutedEventArgs e)
        {
            this.ScaleToFit = !this.ScaleToFit;
            if (this.collapsedTitle == false)
            {
                this.Refresh(true);
            }

            if (this.ScaleToFitClick != null)
            {
                this.ScaleToFitClick(this, new RoutedEventArgs());
            }

            if (e != null)
            {
                // Will set the focus only if the button is clicked.
                // Will not set the focus, if invoked from the code directly.
                this.Focus();
            }
        }

        #endregion        
    }
}
