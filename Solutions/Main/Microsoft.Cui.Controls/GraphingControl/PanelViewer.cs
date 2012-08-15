//-----------------------------------------------------------------------
// <copyright file="PanelViewer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Panel Viewer class.</summary>
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
using System.Windows.Threading;
    #endregion

    /// <summary>
    /// Panel viewer which supports virtualization.
    /// </summary>
    public class PanelViewer : Control
    {       
        #region Template Part Names
        /// <summary>
        /// Layout root element name.
        /// </summary>
        internal const string LayoutRootElementName = "ELEMENT_layoutRoot";

        /// <summary>
        /// Plot area viewport element name.
        /// </summary>
        internal const string DynamicMainLayerViewportElementName = "ELEMENT_dynamicMainLayerViewport";

        /// <summary>
        /// Main plot area element name.
        /// </summary>
        internal const string DynamicMainLayerElementName = "ELEMENT_dynamicMainLayer";

        /// <summary>
        /// Main dynamic plot layer.
        /// </summary>
        internal const string DynamicPlotLayerElementName = "ELEMENT_dynamicPlotLayer";

        /// <summary>
        /// Plot layer viewport element name.
        /// </summary>
        internal const string DynamicPlotLayerViewportElementName = "ELEMENT_dynamicPlotLayerViewport";

        /// <summary>
        /// Y Axis viewport element name.
        /// </summary>
        internal const string NonDynamicRightAxisViewPortElementName = "ELEMENT_nonDynamicRightAxisViewPort";

        /// <summary>
        /// Up arrow button element name.
        /// </summary>
        internal const string ArrowButtonUpElementName = "ELEMENT_directionArrowUp";

        /// <summary>
        /// Down arrow button element name.
        /// </summary>
        internal const string ArrowButtonDownElementName = "ELEMENT_directionArrowDown";

        /// <summary>
        /// Horizontal scrollbar element name.
        /// </summary>
        internal const string HorizontalScrollBarElementName = "ELEMENT_scrollBar";

        /// <summary>
        /// Vertical scrollbar element name.
        /// </summary>
        internal const string VerticalScrollBarElementName = "ELEMENT_scrollBarVertical";

        /// <summary>
        /// Focus indicator visual element name.
        /// </summary>
        internal const string FocusVisualElementName = "FocusVisualElement";
        #endregion

        #region Dependency Properties
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PanelViewer.ScrollBarValue"/> dependency property.
        /// </summary>
        private static readonly DependencyProperty ScrollBarValueProperty =
            DependencyProperty.Register("ScrollBarValue", typeof(double), typeof(PanelViewer), new PropertyMetadata(0d, new PropertyChangedCallback(OnScrollbarValueChanged)));
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

        #region Private Members
        /// <summary>
        /// Timer that will run on UI Thread and process actions.
        /// </summary>                
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;

        /// <summary>
        /// Member variable to hold layout root panel.
        /// </summary>        
        private Grid layoutRoot;

        /// <summary>
        /// Member variable to hold viewport.
        /// </summary>        
        private Canvas dynamicMainLayerViewport;        
                
        /// <summary>
        /// Member variable to hold scrollbar.
        /// </summary>        
        private ScrollBar scrollBar;

        /// <summary>
        /// Member variable to hold graph pages.
        /// </summary>        
        private RenderPageQueue queue = new RenderPageQueue();

        /// <summary>
        /// Current panel 1 Id.
        /// </summary>
        private double currentPanel1Id = -1;

        /// <summary>
        /// Current panel 2 Id.
        /// </summary>
        private double currentPanel2Id = -1;

        /// <summary>
        /// Panel 1 Id.
        /// </summary>
        private int panel1Id;

        /// <summary>
        /// Panel 2 Id.
        /// </summary>
        private int panel2Id;       

        /// <summary>
        /// Value to offset current panel.
        /// </summary>
        private double amountToOffsetPanel1;

        /// <summary>
        /// Member variable to hold Panel 2.
        /// </summary>
        private PanelWrapper panel1;

        /// <summary>
        /// Member variable to hold Panel 2.
        /// </summary>
        private PanelWrapper panel2;

        /// <summary>
        /// Member variable to hold focus visual.
        /// </summary>
        private UIElement focusVisual;

        /// <summary>
        /// Member variable to indicate whether the initialize event has already been raised.
        /// </summary>
        private bool initializeEventRaised;

        /// <summary>
        /// Member variable to indicate edit is in progress.
        /// </summary>
        private bool editInProgress;

        /// <summary>
        /// Member variable to indicate a refresh is pending.
        /// </summary>
        private bool pendingRefresh;

        /// <summary>
        /// Member variable to indicate that intialization is pending.
        /// </summary>
        private bool pendingInitialize;

        /// <summary>
        /// Member variable to hold dispatch timer control for linear progression of scroll movements.
        /// </summary>
        private DispatcherTimer scrollTimer = new DispatcherTimer();

        /// <summary>
        /// Member variable to indicate whether a scrollbar value has been changed.
        /// </summary>
        /// <remarks>Will be reset by scrollTimer dispatcher after processing.</remarks>
        private bool valueChanged;

        /// <summary>
        /// Member variable to hold the DateTime when the last scroll event was triggered.
        /// </summary>
        private DateTime lastScrollEvent;

        /// <summary>
        /// Member variable to indicate whether panel layout is in progress.
        /// </summary>
        private bool layoutInProgress;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PanelViewer"/> class.
        /// </summary>
        public PanelViewer()
        {
            this.BeginEdit();
            this.DefaultStyleKey = typeof(PanelViewer);

            this.MaxScrollBarSmallChange = 10;

            this.GotFocus += new RoutedEventHandler(this.PanelViewer_GotFocus);
            this.LostFocus += new RoutedEventHandler(this.PanelViewer_LostFocus);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(this.PanelViewer_MouseLeftButtonDown);
            this.KeyDown += new KeyEventHandler(this.PanelViewer_KeyDown);
            this.SizeChanged += new SizeChangedEventHandler(this.PanelViewerSizeChanged);

            this.scrollTimer.Interval = TimeSpan.FromMilliseconds(10);
            this.scrollTimer.Tick += new EventHandler(this.ScrollTimer_Tick);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the graph is initialized.
        /// </summary>
        public event EventHandler<RoutedEventArgs> Initialized;

        /// <summary>
        /// Occurs when [panel layout completed].
        /// </summary>
        public event EventHandler<RoutedEventArgs> PanelLayoutCompleted;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether [show seam boundaries].
        /// </summary>
        /// <value><c>True</c> If [show seam boundaries]; otherwise, <c>false</c>.</value>
        [Category("Graph Appearance")]
        public bool ShowSeamBoundaries
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the graph has been initialized.
        /// </summary>
        /// <value>Value indicating whether the graph has been initialized.</value>
        [Category("Behavior")]
        public bool IsInitialized
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the max scroll bar small change.
        /// </summary>
        /// <value>The max scroll bar small change.</value>
        /// <remarks>This maximum will be applied to accelerate.</remarks>
        public int MaxScrollBarSmallChange
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [renewing both panels].
        /// </summary>
        /// <value><c>True</c> If [renewing both panels]; otherwise, <c>false</c>.</value>
        internal bool RenewingBothPanels
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the scrollbar.
        /// </summary>
        /// <value>Scroll bar element.</value>
        internal ScrollBar ScrollBar
        {
            get { return this.scrollBar; }
            set { this.scrollBar = value; }
        }

        /// <summary>
        /// Gets or sets the Root layout element.
        /// </summary>
        /// <value>Root layout element.</value>
        internal Grid LayoutRoot
        {
            get { return this.layoutRoot; }
            set { this.layoutRoot = value; }
        }        
        
        /// <summary>
        /// Gets or sets a value indicating whether the bindings for the scrollbar have been created.
        /// </summary>
        /// <value>Value indicating whether the bindings for the scrollbar have been created.</value>
        internal bool ScrollBarBindingCreated
        {
            get;
            set;
        }       

        /// <summary>
        /// Gets or sets the Viewport panel element.
        /// </summary>
        /// <value>Viewport panel element.</value>
        protected Canvas DynamicMainLayerViewport
        {
            get { return this.dynamicMainLayerViewport; }
            set { this.dynamicMainLayerViewport = value; }
        }
        
        /// <summary>
        /// Gets or sets the current working panel.
        /// </summary>
        /// <value>The current working panel.</value>
        protected PanelWrapper CurrentWorkingPanel
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Current panel 1.
        /// </summary>
        /// <value>The current panel for panel 1.</value>
        protected PanelWrapper CurrentPanel1
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Current panel 2.
        /// </summary>
        /// /// <value>The current panel for panel 2.</value>
        protected PanelWrapper CurrentPanel2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the panel1.
        /// </summary>
        /// <value>The panel1.</value>
        protected PanelWrapper Panel1
        {
            get
            {
                return this.panel1;
            }

            set 
            {
                this.panel1 = value;
            }
        }

        /// <summary>
        /// Gets or sets the panel2.
        /// </summary>
        /// <value>The panel2.</value>
        protected PanelWrapper Panel2
        {
            get
            {
                return this.panel2;
            }

            set
            {
                this.panel2 = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current control is in design mode.
        /// </summary>
        /// <value>Value indicating whether the current control is in design mode.</value>
        protected bool DesignMode
        {
            get
            {
                return System.ComponentModel.DesignerProperties.GetIsInDesignMode(this);
            }
        }

        /// <summary>
        /// Gets a value indicating whether [initialize event raised].
        /// </summary>
        /// <value>Returns <c>true</c> if [initialize event raised]; otherwise, <c>false</c>.</value>
        protected bool InitializeEventRaised
        {
            get
            {
                return this.initializeEventRaised;
            }
        }
        #endregion       

        #region Private Properties
        /// <summary>
        /// Gets or sets the scroll bar value.
        /// </summary>
        /// <value>The scroll bar value.</value>
        private double ScrollBarValue
        {
            get { return (double)GetValue(ScrollBarValueProperty); }
            set { SetValue(ScrollBarValueProperty, value); }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Overridden. Applies a specified template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            this.LayoutRoot = this.GetTemplateChild<Grid>(LayoutRootElementName, true);
            this.DynamicMainLayerViewport = this.GetTemplateChild<Canvas>(DynamicMainLayerViewportElementName, true);
            this.ScrollBar = this.GetTemplateChild<ScrollBar>(HorizontalScrollBarElementName, true);
            this.focusVisual = this.GetTemplateChild<UIElement>(FocusVisualElementName, false);

            this.DynamicMainLayerViewport.SizeChanged += new SizeChangedEventHandler(this.DynamicMainLayerViewport_SizeChanged);            
            this.Loaded += new RoutedEventHandler(this.PanelViewer_Loaded);

            this.SetBinding(ScrollBarValueProperty, new System.Windows.Data.Binding("Value") { Source = this.scrollBar });
        }

        /// <summary>
        /// Refreshes the layout of the graph.
        /// </summary>
        public void Refresh()
        {
            this.Refresh(true);
        }
        
        /// <summary>
        /// Begins the edit.
        /// </summary>
        public void BeginEdit()
        {
            this.editInProgress = true;
        }

        /// <summary>
        /// Ends the edit.
        /// </summary>
        public void EndEdit()
        {
            this.editInProgress = false;
            if (this.pendingRefresh)
            {
                this.Refresh(this.pendingInitialize);
            }
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Refreshes the layout of the graph.
        /// </summary>
        /// <param name="initialize">Boolean indicating whether to initialize graph.</param>
        internal void Refresh(bool initialize)
        {
            if (!this.editInProgress)
            {
                if (null != this.ScrollBar && this.dynamicMainLayerViewport.ActualWidth != 0)
                {
                    this.OnBeforeRefresh();
                    if (initialize)
                    {
                        this.IsInitialized = false;
                    }

                    this.LayoutPanels(new GraphPage(this.ScrollBar.Value, false), true);

                    if (this.IsInitialized == true && this.Initialized != null)
                    {
                        this.Initialized(this, new RoutedEventArgs());
                        this.initializeEventRaised = true;
                    }
                }

                this.pendingRefresh = false;
                this.pendingInitialize = false;
            }
            else
            {
                this.pendingRefresh = true;                
            }

            if (initialize)
            {
                this.pendingInitialize = true;
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
        internal T GetTemplateChild<T>(string elementName, bool raiseExceptions)
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

        #region Protected Methods

        /// <summary>
        /// Called before refresh.
        /// </summary>
        protected virtual void OnBeforeRefresh()
        {
        }

        /// <summary>
        /// Sets the graph properties on horizontal scrollbar value changed.
        /// </summary>
        /// <remarks>Virtual method, child classes need to override to provide implementation.</remarks>
        protected virtual void OnHorizontalScrollbarValueChanged()
        {
        }

        /// <summary>
        /// Called before panels are refreshed on size changed.
        /// </summary>
        protected virtual void OnBeforeSizeChanged()
        {
        }

        /// <summary>
        /// Called when [size changed].
        /// </summary>
        protected virtual void OnSizeChanged()
        {
        }

        /// <summary>
        /// Checks and starts the dispatcher.
        /// </summary>
        protected void CheckDispatcher()
        {
            if (null == this.dispatcherTimer)
            {
                this.dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                this.dispatcherTimer.Tick += new EventHandler(this.DispatcherTimer_Tick);
                this.dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1);
            }

            this.dispatcherTimer.Start();
        }

        /// <summary>
        /// Updates the non dynamic layers.
        /// </summary>
        protected virtual void UpdateNonDynamicLayers()
        {
        }

        /// <summary>
        /// Draw into panel.
        /// </summary>
        /// <param name="panel">The panel.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="panelNumber">The panel number.</param>
        protected virtual void DrawPanel(PanelWrapper panel, long offset, int panelNumber)
        {
        }

        /// <summary>
        /// Handles the keydown events.
        /// </summary>
        /// <param name="e">KeyEventArgs containing information regarding keys pressed.</param>
        protected virtual void HandleKeyDown(KeyEventArgs e)
        {
            // Override to handle key down events.
            bool ctrlKeyPressed;
            bool shiftKeyPressed;
            KeyboardHelper.GetMetaKeyState(out ctrlKeyPressed, out shiftKeyPressed);

            if (e.Key == Key.Home)
            {
                e.Handled = true;
                this.MoveToStart();
            }
            else if (e.Key == Key.End)
            {
                e.Handled = true;
                this.MoveToEnd();
            }
            else if (e.Key == Key.Left && ctrlKeyPressed && !shiftKeyPressed)
            {
                e.Handled = true;
                this.MovePrevious();
            }
            else if (e.Key == Key.Right && ctrlKeyPressed && !shiftKeyPressed)
            {
                e.Handled = true;
                this.MoveNext();
            }            
        }

        /// <summary>
        /// Moves the graph to next page.
        /// </summary>
        protected void MoveNext()
        {
            if (this.scrollBar.IsEnabled && this.scrollBar.Value < this.scrollBar.Maximum)
            {
                this.scrollBar.Value += this.ScrollBar.LargeChange;
            }
        }

        /// <summary>
        /// Moves the graph to previous page.
        /// </summary>
        protected void MovePrevious()
        {
            if (this.scrollBar.IsEnabled && this.scrollBar.Value > this.scrollBar.Minimum)
            {
                this.scrollBar.Value -= this.ScrollBar.LargeChange;
            }
        }

        /// <summary>
        /// Moves the graph to first page.
        /// </summary>
        protected void MoveToStart()
        {
            if (this.scrollBar.IsEnabled && this.scrollBar.Value != this.scrollBar.Minimum)
            {
                this.scrollBar.Value = this.scrollBar.Minimum;
            }
        }

        /// <summary>
        /// Moves the graph to last page.
        /// </summary>
        protected void MoveToEnd()
        {
            if (this.scrollBar.IsEnabled && this.scrollBar.Value != this.scrollBar.Maximum)
            {
                this.scrollBar.Value = this.scrollBar.Maximum;
            }
        }

        /// <summary>
        /// Called when [layout panels complete].
        /// </summary>
        protected virtual void OnLayoutPanelsComplete()
        {            
            if (this.PanelLayoutCompleted != null)
            {
                this.PanelLayoutCompleted(this, new RoutedEventArgs());
            }

            this.layoutInProgress = false;
        }

        /// <summary>
        /// Gets the panel at the specied offset.
        /// </summary>
        /// <param name="offset">Offset at which panel needs to be determined.</param>
        /// <returns>Panel at the specified offset.</returns>
        protected virtual PanelWrapper GetPanel(double offset)
        {
            PanelWrapper panel = new PanelWrapper();
            panel.Height = this.DynamicMainLayerViewport.ActualHeight;
            panel.Width = this.DynamicMainLayerViewport.ActualWidth;

            TextBlock tb = new TextBlock();
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.FontSize = 70;
            tb.Text = offset.ToString(System.Globalization.CultureInfo.CurrentCulture);
            panel.Children.Add(tb);
            this.DynamicMainLayerViewport.Children.Add(panel);
            return panel;
        }

        /// <summary>
        /// Gets the panel at the specified offset.
        /// </summary>
        /// <param name="offset">Offset at which panel needs to be determined.</param>
        /// <returns>Panel at offset.</returns>
        protected Panel GetPanelAtOffset(double offset)
        {
            double viewPortWidth = this.DynamicMainLayerViewport.ActualWidth;
            Panel panel = null;

            if (offset < viewPortWidth)
            {
                double panel1Left = Canvas.GetLeft(this.CurrentPanel1);                

                if (offset < panel1Left + viewPortWidth)
                {
                    panel = this.CurrentPanel1;
                }
                else
                {
                    panel = this.CurrentPanel2;
                }
            }

            return panel;
        }        
        #endregion

        #region Private Methods
        /// <summary>
        /// Called when [scrollbar value changed].
        /// </summary>
        /// <param name="d">The dependency object whose values were changed.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnScrollbarValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PanelViewer panelViewer = d as PanelViewer;
            if (panelViewer != null && panelViewer.scrollBar != null)
            {
                panelViewer.queue.Enqueue(new GraphPage(panelViewer.ScrollBar.Value, true));
                panelViewer.CheckDispatcher();
                panelViewer.OnHorizontalScrollbarValueChanged();

                double oldValue = (double)e.OldValue;
                double newValue = (double)e.NewValue;

                if (Math.Abs(oldValue - newValue) != panelViewer.scrollBar.LargeChange)
                {
                    panelViewer.valueChanged = true;
                }

                if (!panelViewer.scrollTimer.IsEnabled)
                {
                    panelViewer.lastScrollEvent = DateTime.Now;
                    panelViewer.ScrollBar.SmallChange = 1;
                    panelViewer.scrollTimer.Start();
                }
            }
        }

        /// <summary>
        /// Processes the panel.
        /// </summary>
        /// <param name="panel">The panel.</param>
        /// <param name="panelNumber">The panel number.</param>
        private void ProcessPanel(PanelWrapper panel, int panelNumber)
        {
            this.CurrentWorkingPanel = panel;

            if (this.ShowSeamBoundaries)
            {
                panel.AddSeamBoundary();
            }

            if (panel.Tag != null)
            {
                if (panel.Children.Count < 1)
                {
                    return;
                }

                Grid elementDynamicMainLayer = panel.Children[0] as Grid;
                if (null == elementDynamicMainLayer || elementDynamicMainLayer.Name != DynamicMainLayerElementName)
                {
                    return;
                }

                if (elementDynamicMainLayer.Children.Count < 2)
                {
                    return;
                }

                Canvas elementDynamicPlotLayerViewport = elementDynamicMainLayer.FindName(DynamicPlotLayerViewportElementName) as Canvas;
                if (null == elementDynamicPlotLayerViewport)
                {
                    return;
                }

                this.DrawPanel(panel, (long)panel.Tag, panelNumber);
            }
        }

        /// <summary>
        /// Layouts the panels.
        /// </summary>
        /// <param name="page">The page which needs to be positioned.</param>
        /// <param name="forceRedraw">Indicates whether to force redraw.</param>
        private void LayoutPanels(GraphPage page, bool forceRedraw)
        {
            this.layoutInProgress = true;
            if (this.dynamicMainLayerViewport.ActualWidth == 0 || this.dynamicMainLayerViewport.ActualHeight == 0)
            {
                return;
            }

            this.UpdateNonDynamicLayers();

            double panelId = page.Key / this.dynamicMainLayerViewport.ActualWidth;            

            this.panel1Id = (int)panelId;
            this.panel2Id = (int)this.panel1Id + 1;
            
            double percentageScroll = (panelId % 1);            

            if (percentageScroll == 0)
            {
                // we only need show this.panel1Id as if fits exactly
                this.amountToOffsetPanel1 = 0;
            }
            else
            {
                this.amountToOffsetPanel1 = (int)(this.dynamicMainLayerViewport.ActualWidth * percentageScroll);
            }

            if (this.currentPanel1Id == -1 || true == forceRedraw)
            {
                // first draw
                this.PanelAnimation5();
            }
            else if (this.panel1Id == this.currentPanel1Id)
            {
                // we are not changing panels just adjusting positions
                this.PanelAnimation1();
            }
            else if (this.panel1Id == this.currentPanel2Id)
            {
                // we are replacing panel2 with a new panel and making panel1 the current panel2
                this.PanelAnimation2();
            }
            else if (this.panel2Id == this.currentPanel1Id)
            {
                // we are replacing panel1 with a new panel and making panel2 the current panel1
                this.PanelAnimation3();
            }
            else
            {
                // I am replacing both the panel 1 and panel 2
                this.PanelAnimation4();
            }

            this.OnLayoutPanelsComplete();
        }

        /// <summary>
        /// Sets the panels left positions.
        /// </summary>
        private void PanelAnimation1()
        {
            this.CurrentPanel1.SetValue(Canvas.LeftProperty, this.amountToOffsetPanel1 * -1);
            this.CurrentPanel2.SetValue(Canvas.LeftProperty, (this.amountToOffsetPanel1 * -1) + this.DynamicMainLayerViewport.ActualWidth);
        }

        /// <summary>
        /// Replaces panel 2 with a new panel and sets currentpanel 1 as panel2.
        /// </summary>
        private void PanelAnimation2()
        {
            this.RenewingBothPanels = false;

            // this animation in animating in from the right of the screen 
            this.Panel1 = this.CurrentPanel2;
            this.Panel2 = this.GetPanel(this.panel2Id);
            this.ProcessPanel(this.Panel2, 2);
                
            // when replacing with animation, in the callback we would detatch
            this.CurrentPanel1.SetValue(Canvas.LeftProperty, (this.DynamicMainLayerViewport.ActualWidth * -1));            
            this.dynamicMainLayerViewport.Children.Remove(this.CurrentPanel1);

            this.CurrentPanel1 = this.Panel1;
            this.CurrentPanel2 = this.Panel2;

            this.currentPanel1Id = this.panel1Id;
            this.currentPanel2Id = this.panel2Id;

            this.PanelAnimation1();                        
        }

        /// <summary>
        /// Replaces panel 1 with a new panel and sets currentpanel1 as panel2.
        /// </summary>
        private void PanelAnimation3()
        {
            this.RenewingBothPanels = false;

            // this animation in animating in from the left of the screen 
            this.Panel2 = this.CurrentPanel1;
            this.Panel1 = this.GetPanel(this.panel1Id);
            this.ProcessPanel(this.Panel1, 1);

            // when replacing with animation, in the callback we would detatch
            this.CurrentPanel2.SetValue(Canvas.LeftProperty, this.DynamicMainLayerViewport.ActualWidth);
            this.dynamicMainLayerViewport.Children.Remove(this.CurrentPanel2);

            this.CurrentPanel1 = this.Panel1;
            this.CurrentPanel2 = this.Panel2;

            this.currentPanel1Id = this.panel1Id;
            this.currentPanel2Id = this.panel2Id;

            this.PanelAnimation1();
        }     

        /// <summary>
        /// Replaces both panel 1 and panel 2.
        /// </summary>
        private void PanelAnimation4()
        {
            this.RenewingBothPanels = true;

            // this animation could be left or right, but new panels will be in place of 1 and 2
            this.Panel2 = this.GetPanel(this.panel2Id);
            this.ProcessPanel(this.Panel2, 2);
            this.Panel1 = this.GetPanel(this.panel1Id);
            this.ProcessPanel(this.Panel1, 1);

            // when replacing with animation, in the callback we would detatch
            this.CurrentPanel2.SetValue(Canvas.LeftProperty, this.DynamicMainLayerViewport.ActualWidth);
            this.CurrentPanel1.SetValue(Canvas.LeftProperty, this.DynamicMainLayerViewport.ActualWidth);
            this.dynamicMainLayerViewport.Children.Remove(this.CurrentPanel1);
            this.dynamicMainLayerViewport.Children.Remove(this.CurrentPanel2);

            this.CurrentPanel1 = this.Panel1;
            this.CurrentPanel2 = this.Panel2;

            this.currentPanel1Id = this.panel1Id;
            this.currentPanel2Id = this.panel2Id;

            this.PanelAnimation1();
        }

        /// <summary>
        /// Sets the clipping and intializes panel1 and panel2.
        /// </summary>
        private void PanelAnimation5()
        {
            this.RenewingBothPanels = true;

            // first draw
            this.Panel2 = this.GetPanel(this.panel2Id);            
            this.ProcessPanel(this.Panel2, 2);
            this.Panel1 = this.GetPanel(this.panel1Id);
            this.ProcessPanel(this.Panel1, 1);
            
            // remove the panels if they exist else you get ghosting and we leak!
            this.dynamicMainLayerViewport.Children.Remove(this.CurrentPanel1);
            this.dynamicMainLayerViewport.Children.Remove(this.CurrentPanel2);                      

            this.currentPanel1Id = this.panel1Id;
            this.currentPanel2Id = this.panel2Id;
            this.CurrentPanel1 = this.Panel1;
            this.CurrentPanel2 = this.Panel2;

            this.PanelAnimation1();
        }
        
        /// <summary>
        /// Show or hide the focus visual.
        /// </summary>
        /// <param name="show">Boolean indicating whether to show focus visual.</param>
        private void ShowFocusVisual(bool show)
        {
            if (this.focusVisual != null)
            {
                this.focusVisual.Opacity = show ? 1 : 0;
            }
        }
        
        /// <summary>
        /// Processes the pages in the queue.
        /// </summary>
        private void ProcessQueue()
        {
            while (true)
            {
                if (this.queue.Count > 1)
                {
                    this.queue.Dequeue();
                }
                else
                {
                    break;
                }
            }

            GraphPage page1 = this.queue.Dequeue();
            if (this.layoutInProgress)
            {
                if (this.panel1 != null)
                {
                    this.panel1.AbortLayout = true;
                }

                if (this.panel2 != null)
                {
                    this.panel2.AbortLayout = true;
                }
            }

            this.LayoutPanels(page1, false);
        }        
        #endregion

        #region Event handlers
        /// <summary>
        /// Handles the Tick event of the ScrollTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Subtract(this.lastScrollEvent) > TimeSpan.FromSeconds(0.5))
            {
                this.ScrollBar.SmallChange = 1;
                this.lastScrollEvent = DateTime.MinValue;
                this.scrollTimer.Stop();
            }
            else if (this.valueChanged)
            {
                this.lastScrollEvent = DateTime.Now;
                if (this.scrollBar.SmallChange < this.MaxScrollBarSmallChange)
                {
                    this.ScrollBar.SmallChange++;
                }

                this.valueChanged = false;
            }
        }

        /// <summary>
        /// Handles the size changed event of the layout root.
        /// </summary>
        /// <param name="sender">Element whose size was changed.</param>
        /// <param name="e">Event arguments containing new and old size information.</param>
        private void LayoutRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.OnBeforeSizeChanged();
            this.Refresh(true);
            this.OnSizeChanged();
        }

        /// <summary>
        /// Handles the dispatch timer tick event.
        /// </summary>
        /// <param name="sender">Timer which ticked.</param>
        /// <param name="e">Event arguments.</param>
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.ProcessQueue();
            this.dispatcherTimer.Stop();
        }

        /// <summary>
        /// Handles the mouse left button down event.
        /// </summary>
        /// <param name="sender">Object on which the mouse was clicked.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void PanelViewer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Focus();
            e.Handled = true;
        }

        /// <summary>
        /// Handles the got focus event.
        /// </summary>
        /// <param name="sender">Object which got the focus.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void PanelViewer_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ShowFocusVisual(true);
        }

        /// <summary>
        /// Handles the lost focus event.
        /// </summary>
        /// <param name="sender">Object which lost the focus.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void PanelViewer_LostFocus(object sender, RoutedEventArgs e)
        {
            this.ShowFocusVisual(false);
        }

        /// <summary>
        /// Handles the key down event.
        /// </summary>
        /// <param name="sender">Object on key was pressed.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void PanelViewer_KeyDown(object sender, KeyEventArgs e)
        {
            this.HandleKeyDown(e);
        }

        /// <summary>
        /// Handles the SizeChanged event of the PanelViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        private void PanelViewerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RectangleGeometry clip = new RectangleGeometry();
            clip.Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height);
            this.Clip = clip;
            this.EndEdit();
        }

        /// <summary>
        /// Handles the Loaded event of the PanelViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void PanelViewer_Loaded(object sender, RoutedEventArgs e)
        {
            this.LayoutRoot.SizeChanged += new SizeChangedEventHandler(this.LayoutRoot_SizeChanged);
        }

        /// <summary>
        /// Handles the SizeChanged event of the DynamicMainLayerViewport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        private void DynamicMainLayerViewport_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RectangleGeometry clip = new RectangleGeometry();
            clip.Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height);
            this.DynamicMainLayerViewport.Clip = clip;
        }
        #endregion

        #region Subclasses
        /// <summary>
        /// A page in the graph.
        /// </summary>
        /// <remarks>Used in virtualization.</remarks>
        protected class GraphPage
        {
            /// <summary>
            /// Member variable to hold the key.
            /// </summary>
            private double key;

            /// <summary>
            /// Member variable to hold animate flag.
            /// </summary>
            private bool animate;

            /// <summary>
            /// Initializes a new instance of the GraphPage class with the data specified.
            /// </summary>
            /// <param name="key">Key for the graph page.</param>
            /// <param name="animate">Value indicating whether to animate navigation to this page.</param>
            public GraphPage(double key, bool animate)
            {
                this.key = key;
                this.animate = animate;
            }

            /// <summary>
            /// Gets or sets a value indicating whether this page should be animated while getting into view.
            /// </summary>
            /// <value>Value indicating whether to animate.</value>
            public bool Animate
            {
                get { return this.animate; }
                set { this.animate = value; }
            }

            /// <summary>
            /// Gets or sets the key for the page.
            /// </summary>
            /// <value>Key uniquely identifying this page.</value>
            public double Key
            {
                get { return this.key; }
                set { this.key = value; }
            }
        }

        /// <summary>
        /// Queue containing the graph page objects.
        /// </summary>
        protected class RenderPageQueue : System.Collections.Generic.Queue<GraphPage>
        {
        }
        #endregion
    }   
}
