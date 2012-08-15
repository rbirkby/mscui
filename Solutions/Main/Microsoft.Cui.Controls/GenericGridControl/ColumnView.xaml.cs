//-----------------------------------------------------------------------
// <copyright file="ColumnView.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>11-Feb-2008</date>
// <summary>The ColumnView 'base' component used by the MainView, LookAhead and LookBehind view classes.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Collections;
    using Microsoft.Cui.Controls.GenericGridControl;

    /// <summary>
    /// Same as the enum in System.Windows.Forms - because Silverlight 
    /// equivalent enum only has Simple as a value i.e. 0 - so define our own here.
    /// </summary>
    public enum SelectionMode
    {
        /// <summary>
        /// No selection possible.
        /// </summary>
        None,

        /// <summary>
        /// Can select only one item at a time.
        /// </summary>
        One,

        /// <summary>
        /// Can select several contiguous items at a time.
        /// </summary>
        MultipleSimple,

        /// <summary>
        /// Can select several non-contiguous items at a time.
        /// </summary>
        MultipleExtended
    }

    /// <summary>
    /// The ColumnView 'base' component used by the MainView, LookAhead and LookBehind view classes.
    /// </summary>
    public class ColumnView
    {
        #region Private Members

        /// <summary>
        /// Member variable to uniquely identify a cell.
        /// </summary>
        private static int cellId;

        /// <summary>
        /// Member variable to indicate whether the current view is main view.
        /// </summary>
        private bool mainView;

        /// <summary>
        /// The ColumnManagers collection.
        /// </summary>
        private Collection<ColumnManager> columns;

        /// <summary>
        /// The layoutRoot Grid.
        /// </summary>
        private Grid grid;

        /// <summary>
        /// The groupingTemplateLogic DataTemplate.
        /// </summary>
        private DataTemplate groupingTemplateLogic;

        /// <summary>
        /// The GroupingTemplatePresentation DataTemplate.
        /// </summary>
        private DataTemplate groupingTemplatePresentation;

        /// <summary>
        /// The lastRow DataBoundRow.
        /// </summary>
        private DataBoundRow lastRow;

        /// <summary>
        /// The rows collection.
        /// </summary>
        private Collection<DataBoundRow> rows = new Collection<DataBoundRow>();

        /// <summary>
        /// The rows collection.
        /// </summary>
        private Dictionary<string, DataBoundRow> hashCells = new Dictionary<string, DataBoundRow>();

        /// <summary>
        /// Contains a dictionary og grouped rows with key as grouping text.
        /// </summary>
        private Dictionary<string, Collection<DataBoundRow>> hashGroupings = new Dictionary<string, Collection<DataBoundRow>>();

        /// <summary>
        /// Collection of grouping title textblocks.
        /// </summary>
        private Collection<TextBlock> groupingTitles = new Collection<TextBlock>();

        /// <summary>
        /// Flag to determine the direction of layout.
        /// </summary>
        private bool rightToLeft;

        /// <summary>
        /// The index of the first visible item in the list.
        /// </summary>
        private int firstVisibleItemIndex;

        /// <summary>
        /// The index of the last visible item in the list.
        /// </summary>
        private int lastVisibleItemIndex;

        /// <summary>
        /// The count of visible items.
        /// </summary>
        private int visibleItemCount;

        /// <summary>
        /// The SelectionMode for item selection in the control.
        /// </summary>
        private SelectionMode selectionMode = SelectionMode.One;

        /// <summary>
        /// Reference to the toplevel WrapDataGrid.
        /// </summary>
        private WrapDataGrid hostingWrapDataGrid;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnView"/> class.
        /// </summary>
        /// <param name="grid">The layoutRoot grid.</param>
        /// <param name="columnManagers">The column managers collection.</param>
        /// <param name="rightToLeft">If set to <c>true</c> layout right to left.</param>
        public ColumnView(Grid grid, Collection<ColumnManager> columnManagers, bool rightToLeft)
        {
            this.RightToLeft = rightToLeft;            
            this.columns = columnManagers;
            this.grid = grid;
            foreach (ColumnManager manager in this.columns)
            {
                if (null != manager.ColumnHeader)
                {
                    this.grid.Children.Add(manager.ColumnHeader);
                    manager.ColumnHeader.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.ColumnHeader_MouseLeftButtonUp);
                }

                if (null != manager.GridLayout)
                {
                    manager.GridLayout.Children.Add(manager.StackPanel);
                    manager.GridLayout.Children.Add(manager.SummaryManager.MainPanel);
                    this.grid.Children.Add(manager.GridLayout);
                }
                else
                {
                    this.grid.Children.Add(manager.StackPanel);
                }                   
            }
        }

        /// <summary>
        /// Overloaded Constructor. Initializes a new instance of the <see cref="ColumnView"/> class.
        /// </summary>
        /// <param name="grid">The layoutRoot grid.</param>
        /// <param name="columnManagers">The column managers collection.</param>
        /// <param name="groupingTemplateLogic">The groupingTemplateLogic DataTemplate.</param>
        /// <param name="groupingTemplatePresentation">The groupingTemplatePresentation DataTemplate.</param>
        public ColumnView(Grid grid, Collection<ColumnManager> columnManagers, DataTemplate groupingTemplateLogic, DataTemplate groupingTemplatePresentation)
            : this(grid, columnManagers)
        {
            this.groupingTemplatePresentation = groupingTemplatePresentation;
            this.groupingTemplateLogic = groupingTemplateLogic;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnView"/> class.
        /// </summary>
        /// <param name="grid">The layoutRoot grid.</param>
        /// <param name="columnManagers">The column managers collection.</param>
        public ColumnView(Grid grid, Collection<ColumnManager> columnManagers)
            : this(grid, columnManagers, false)
        {
        }
        #endregion

        #region Delgates and Events

        /// <summary>
        /// Column click event.
        /// </summary>
        public event System.EventHandler<System.EventArgs> OnColumnHeaderClick;

        /// <summary>
        /// GroupingRender event.
        /// </summary>
        internal event System.EventHandler<System.EventArgs> OnGroupingRender;

        /// <summary>
        /// GroupStateChanged event.
        /// </summary>
        internal event System.EventHandler<EventArgs> OnGroupStateChanged;

        /// <summary>
        /// Group header click event.
        /// </summary>
        internal event System.EventHandler<System.EventArgs> OnGroupHeaderClick;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether layout should run right to left.
        /// </summary>
        /// <value>Value is <c>true</c> if layout runs right to left; otherwise, <c>false</c>.</value>
        public bool RightToLeft
        {
            get
            {
                return this.rightToLeft;
            }

            set
            {
                this.rightToLeft = value;
            }
        }

        /// <summary>
        /// Gets the ColumnManagers collection.
        /// </summary>
        /// <value>The ColumnManagers columns collection.</value>
        public Collection<ColumnManager> Columns
        {
            get
            {
                return this.columns;
            }
        }

        /// <summary>
        /// Gets the rows collection.
        /// </summary>
        /// <value>The rows collection.</value>
        public Collection<DataBoundRow> Rows
        {
            get
            {
                return this.rows;
            }
        }

        /// <summary>
        /// Gets or sets the index of the first visible item in the list.
        /// </summary>
        /// <value>The index of the first visible item.</value>
        public int FirstVisibleItemIndex
        {
            get
            {
                return this.firstVisibleItemIndex;
            }

            set
            {
                this.firstVisibleItemIndex = value;
            }
        }

        /// <summary>
        ///  Gets or sets the index of the last visible item in the list.
        /// </summary>
        /// <value>Last visible item index.</value>
        public int LastVisibleItemIndex
        {
            get
            {
                return this.lastVisibleItemIndex;
            }

            set
            {
                this.lastVisibleItemIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets the visible item count.
        /// </summary>
        /// <value>The visible item count.</value>
        public int VisibleItemCount
        {
            get
            {
                return this.visibleItemCount;
            }

            set
            {
                this.visibleItemCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the SelectionMode for item selection in the control.
        /// </summary>
        /// <value>Selection mode.</value>
        public SelectionMode SelectionMode
        {
            get
            {
                return this.selectionMode;
            }

            set
            {
                this.selectionMode = value;
            }
        }

        /// <summary>
        /// Gets or sets the GroupingTemplateLogic which determines if we group in the grid.
        /// </summary>
        /// <value>Grouping template logic.</value>
        public DataTemplate GroupingTemplateLogic
        {
            get
            {
                return this.groupingTemplateLogic;
            }

            set
            {
                this.groupingTemplateLogic = value;
            }
        }

        /// <summary>
        /// Gets or sets the the template for the grouping.
        /// </summary>
        /// <value>Grouping template.</value>
        public DataTemplate GroupingTemplatePresentation
        {
            get
            {
                return this.groupingTemplatePresentation;
            }

            set
            {
                this.groupingTemplatePresentation = value;
            }
        }

        /// <summary>
        /// Gets or sets a reference to the toplevel WrapDataGrid.
        /// </summary>
        /// <value>Hosting Grid.</value>
        public WrapDataGrid HostingWrapDataGrid
        {
            get
            {
                return this.hostingWrapDataGrid;
            }

            set
            {
                this.hostingWrapDataGrid = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current view is main view.
        /// </summary>
        internal bool MainView
        {
            get { return this.mainView; }
            set { this.mainView = value; }
        }

        /*
        /// <summary>
        /// ColumnManagers collection.
        /// </summary>
        /// <param name="dictionary">The resources ResourceDictionary.</param>
        /// <returns>The collection of ColumnManagers.</returns>
        public static Collection<ColumnManager> ColumnManagers(ResourceDictionary dictionary)
        {
            Collection<ColumnManager> collection = new Collection<ColumnManager>();
            foreach (object item in dictionary.Values)
            {
                ColumnManager manager = item as ColumnManager;
                if (null != manager)
                {
                    collection.Add(manager);
                }
            }

            return collection;
        }*/
        #endregion             
        
        #region Public Methods
        /// <summary>
        /// Adds a row to the ColumnView.
        /// </summary>
        /// <param name="dataContext">The bound DataContext.</param>
        /// <returns>True if row was added successfully.</returns>
        public bool AddRow(object dataContext)
        {
            DataBoundRow row = new DataBoundRow(this.lastRow);
            row.HostingWrapDataGrid = this.HostingWrapDataGrid;

            row.Visibility = Visibility.Visible;
            row.DataContext = dataContext;
            bool addHeaderIntoPanel = false;
            DataBoundRowGrouping grouping = null;
            FrameworkElement groupingPresentation = null;

            if (null != this.groupingTemplateLogic)
            {
                grouping = DataTemplateHelper.LoadContent(this.groupingTemplateLogic) as DataBoundRowGrouping;
                if (null != grouping)
                {
                    grouping.DataSource = dataContext;
                    row.Grouping = grouping;                    

                    if (null == this.lastRow)
                    {
                        // new header as we are the first row and we have a grouping template
                        addHeaderIntoPanel = true;
                    }
                    else
                    {
                        if (this.lastRow.Grouping.GroupingText != row.Grouping.GroupingText)
                        {
                            // we have a grouping template and we are not the same as the last row.
                            addHeaderIntoPanel = true;
                        }
                    }

                    // Update the groupings hash table.
                    if (this.hashGroupings.ContainsKey(row.Grouping.GroupingText))
                    {
                        Collection<DataBoundRow> groupedRows = this.hashGroupings[row.Grouping.GroupingText];
                        groupedRows.Add(row);
                    }
                    else
                    {
                        Collection<DataBoundRow> groupedRows = new Collection<DataBoundRow>();
                        groupedRows.Add(row);
                        this.hashGroupings.Add(row.Grouping.GroupingText, groupedRows);
                    }
                }
            }

            // Indicates this row contains group header
            if (addHeaderIntoPanel && this.lastRow != null)
            {
                this.RaiseGroupingRenderEvent();
            }

            this.lastRow = row;

            bool addGroupingTitle = true;

            bool bindHeights = false;
            if (this.columns.Count > 1)
            {
                bindHeights = true;
            }

            foreach (ColumnManager column in this.columns)
            {
                cellId++;

                Panel dataTemplate = this.LoadContent(column) as Panel;

                dataTemplate.SetValue(FrameworkElement.NameProperty, String.Format(System.Globalization.CultureInfo.CurrentCulture, WrapDataGridResources.CellNameFormat, cellId));

                if (null == dataTemplate)
                {
                    throw new ArgumentException(WrapDataGridResources.InvalidCellTemplateRootElement);
                }

                StackPanel heightController = new StackPanel();
                heightController.Orientation = Orientation.Vertical;
                ToolTip currentTooltip = new ToolTip();
                dataTemplate.DataContext = dataContext;

                if (column.ToolTipTemplate != null)
                {
                    FrameworkElement tooltipElement = column.ToolTipTemplate.LoadContent() as FrameworkElement;
                    tooltipElement.DataContext = dataContext;
                    if (tooltipElement != null)
                    {
                        if (tooltipElement.GetType().ToString() == "System.Windows.Controls.ContentControl")
                        {
                            tooltipElement.Height = row.Height;
                            tooltipElement.Width = column.Width;
                        }

                        currentTooltip.Content = tooltipElement;
                    }

                    if (currentTooltip != null)
                    {
                        ToolTipService.SetToolTip(heightController, currentTooltip);
                    }
                }           
               
                dataTemplate.Tag = string.Format(System.Globalization.CultureInfo.CurrentCulture, WrapDataGridResources.CellTagFormat, this.rows.Count, this.columns.Count - 1);

                if (this.RightToLeft == true)
                {                    
                    column.StackPanel.Children.Insert(0, dataTemplate);
                }
                else
                {
                    if (true == addHeaderIntoPanel)
                    {
                        groupingPresentation = DataTemplateHelper.LoadContent(this.groupingTemplatePresentation) as FrameworkElement;

                        if (null != groupingPresentation)
                        {
#if SILVERLIGHT
                            if (null != WrapDataGridResources.GroupButtonName && null != WrapDataGridResources.GroupButtonName && null != groupingPresentation.FindName(WrapDataGridResources.GroupButtonName))
                            {
                                Button hitButton = groupingPresentation.FindName(WrapDataGridResources.GroupButtonName) as Button;
                                hitButton.IsTabStop = false;
                            }

                            TextBlock groupTitle = groupingPresentation.FindName(WrapDataGridResources.GroupTitleName) as TextBlock;
#else
                            // ADDED as FindName is not functioning in the WPF version
                            TextBlock groupTitle = (groupingPresentation as Grid).Children[1] as TextBlock;
#endif

                            if (groupTitle == null)
                            {                                
                                throw new ArgumentNullException(WrapDataGridResources.NoGroupTitleElementFound);
                            }                            
                                                        
                             ////only add the title to the first column
                            if (addGroupingTitle)
                            {
                                groupTitle.Text = row.Grouping.GroupingText;
                                this.groupingTitles.Add(groupTitle);
                                row.GroupingHeader = groupingPresentation;

#if SILVERLIGHT
                                if (null != WrapDataGridResources.GroupButtonName && null != groupingPresentation.FindName(WrapDataGridResources.GroupButtonName))
                                {
                                    Button hitButton = groupingPresentation.FindName(WrapDataGridResources.GroupButtonName) as Button;
                                    hitButton.IsTabStop = true;
                                }
#endif
                            }

                            groupingPresentation.MouseLeftButtonDown += new MouseButtonEventHandler(this.GroupingPresentation_MouseLeftButtonDown);
                            groupingPresentation.SetValue(Grid.TagProperty, row.Grouping.GroupingText);
                            groupingPresentation.SetValue(Grid.NameProperty, WrapDataGridResources.GroupingPresentationName);
#if !SILVERLIGHT
                            if (addGroupingTitle)
                            {
                              groupingPresentation.UpdateLayout();
                            }
#endif
                            dataTemplate.Children.Add(groupingPresentation);
                            addGroupingTitle = false;  
                        }
                    }

                    heightController.Children.Add(dataTemplate);                    
                    column.StackPanel.Children.Add(heightController);
                }

                if (true == bindHeights)
                {
                    System.Windows.Data.Binding binding = new System.Windows.Data.Binding(WrapDataGridResources.RowHeightPropertyName);
                    binding.Source = row;
                    heightController.SetBinding(StackPanel.HeightProperty, binding);
                    dataTemplate.Loaded += new RoutedEventHandler(this.DataTemplate_Loaded);
                    dataTemplate.SizeChanged += new SizeChangedEventHandler(this.DataTemplate_SizeChanged);
                    row.List.Add(heightController);
                    this.hashCells.Add(dataTemplate.Name, row);
                }
            }

            row.Index = this.rows.Count;
            this.rows.Add(row);
           
            return true;
        }

        /// <summary>
        /// Adds complete data to a blank row (row with partially filled data) created earlier.
        /// </summary>
        /// <param name="position">Position of the Blank Row.</param>
        public void FillBlankRowWithData(int position)
        {
            DataBoundRow row = this.rows[position];
            object dataContext = row.DataContext; //// retrieve the data context stored earlier in the row.                       
            FrameworkElement groupingPresentation = null;

            bool addHeaderIntoPanel = false;

            // if this is not the first row, get the previous row's reference
            if (position > 0) 
            {
                this.lastRow = this.rows[position - 1];
            }

            if (null != this.groupingTemplateLogic)
            {
                if (null == this.lastRow)
                {
                    // new header as we are the first row and we have a grouping template
                    addHeaderIntoPanel = true;
                }
                else
                {
                    if (this.lastRow.Grouping.GroupingText != row.Grouping.GroupingText)
                    {
                        // we have a grouping template and we are not the same as the last row.
                        addHeaderIntoPanel = true;
                    }
                }
            }

            // Indicates this row contains group header
            if (addHeaderIntoPanel && this.lastRow != null)
            {
                this.RaiseGroupingRenderEvent();
            }

            this.lastRow = row;

            bool addGroupingTitle = true;

            bool bindHeights = false;
            if (this.columns.Count > 1)
            {
                bindHeights = true;
            }

            int replacePositionForRightToLeft = 0;
            foreach (ColumnManager column in this.columns)
            {
                cellId++;

                Panel dataTemplate = this.LoadContent(column) as Panel;

                dataTemplate.SetValue(FrameworkElement.NameProperty, String.Format(System.Globalization.CultureInfo.CurrentCulture, WrapDataGridResources.CellNameFormat, cellId));

                if (null == dataTemplate)
                {
                    throw new ArgumentException(WrapDataGridResources.InvalidCellTemplateRootElement);
                }

                StackPanel heightController = new StackPanel();
                heightController.Orientation = Orientation.Vertical;
                ToolTip currentTooltip = new ToolTip();
                dataTemplate.DataContext = dataContext;
                dataTemplate.Tag = string.Format(System.Globalization.CultureInfo.CurrentCulture, WrapDataGridResources.CellTagFormat, position, this.columns.Count - 1);

                if (column.ToolTipTemplate != null)
                {
                    FrameworkElement tooltipElement = column.ToolTipTemplate.LoadContent() as FrameworkElement;
                    tooltipElement.DataContext = dataContext;
                    if (tooltipElement != null)
                    {
                        if (tooltipElement.GetType().ToString() == "System.Windows.Controls.ContentControl")
                        {
                            tooltipElement.Height = row.Height;
                            tooltipElement.Width = column.Width;
                        }

                        currentTooltip.Content = tooltipElement;
                    }

                    if (currentTooltip != null)
                    {
                        ToolTipService.SetToolTip(heightController, currentTooltip);
                    }
                }

                if (this.RightToLeft == true)
                {
                    //// Actual replace position is calculated as below, since this is a Right to Left Type
                    replacePositionForRightToLeft = this.rows.Count - 1 - position;
                    if (replacePositionForRightToLeft < column.StackPanel.Children.Count)
                    {
                        column.StackPanel.Children.RemoveAt(replacePositionForRightToLeft);
                        column.StackPanel.Children.Insert(replacePositionForRightToLeft, dataTemplate);
                    }
                }
                else
                {
                    if (true == addHeaderIntoPanel)
                    {
                        groupingPresentation = DataTemplateHelper.LoadContent(this.groupingTemplatePresentation) as FrameworkElement;

                        if (null != groupingPresentation)
                        {
#if SILVERLIGHT
                            if (null != groupingPresentation && null != WrapDataGridResources.GroupButtonName && null != groupingPresentation.FindName(WrapDataGridResources.GroupButtonName))
                            {
                                Button hitButton = groupingPresentation.FindName(WrapDataGridResources.GroupButtonName) as Button;
                                hitButton.IsTabStop = false;
                            }

                            TextBlock groupTitle = groupingPresentation.FindName(WrapDataGridResources.GroupTitleName) as TextBlock;
#else
                            // ADDED as FindName is not functioning in the WPF version
                            TextBlock groupTitle = (groupingPresentation as Grid).Children[1] as TextBlock;
#endif

                            if (groupTitle == null)
                            {
                                throw new ArgumentNullException(WrapDataGridResources.NoGroupTitleElementFound);
                            }

                            // only add the title to the first column
                            if (addGroupingTitle)
                            {
                                groupTitle.Text = row.Grouping.GroupingText;
                                this.groupingTitles.Add(groupTitle);
                                row.GroupingHeader = groupingPresentation;

#if SILVERLIGHT
                                if (null != WrapDataGridResources.GroupButtonName && null != groupingPresentation.FindName(WrapDataGridResources.GroupButtonName))
                                {
                                    Button hitButton = groupingPresentation.FindName(WrapDataGridResources.GroupButtonName) as Button;
                                    hitButton.IsTabStop = true;
                                }
#endif
                            }

                            groupingPresentation.MouseLeftButtonDown += new MouseButtonEventHandler(this.GroupingPresentation_MouseLeftButtonDown);
                            groupingPresentation.SetValue(Grid.TagProperty, row.Grouping.GroupingText);
                            groupingPresentation.SetValue(Grid.NameProperty, WrapDataGridResources.GroupingPresentationName);
                            groupingPresentation.UpdateLayout();
#if !SILVERLIGHT
                            if (addGroupingTitle)
                            {
                              groupingPresentation.UpdateLayout();
                            }
#endif
                            dataTemplate.Children.Add(groupingPresentation);
                            dataTemplate.UpdateLayout();
                            addGroupingTitle = false;
                        }
                    }
                    
                    heightController.Children.Add(dataTemplate);                    

                    // column.StackPanel.Children.Add(heightController);
                    column.StackPanel.Children.RemoveAt(position);
                    column.StackPanel.Children.Insert(position, heightController);
                }

                if (true == bindHeights)
                {
                    System.Windows.Data.Binding binding = new System.Windows.Data.Binding(WrapDataGridResources.RowHeightPropertyName);
                    binding.Source = row;
                    heightController.SetBinding(StackPanel.HeightProperty, binding);

                    dataTemplate.Loaded += new RoutedEventHandler(this.DataTemplate_Loaded);

                    dataTemplate.SizeChanged += new SizeChangedEventHandler(this.DataTemplate_SizeChanged);

                    row.List.Add(heightController);
                    this.hashCells.Add(dataTemplate.Name, row);
                }
            }

            row.Index = position;
            row.IsBlank = false;
        }

        /// <summary>
        /// Adds an almost blank row(row with bare minimum data) to the list of rows.
        /// </summary>
        /// <param name="dataContext">The Data Context for the row.</param>
        public void AddBlank(object dataContext)
        {
            DataBoundRow tempRow = new DataBoundRow(this.lastRow);
            tempRow.HostingWrapDataGrid = this.HostingWrapDataGrid;

            tempRow.Visibility = Visibility.Visible;
            tempRow.DataContext = dataContext;
            bool addHeaderIntoPanel = false;
            DataBoundRowGrouping grouping = null;
         
            if (null != this.groupingTemplateLogic)
            {
                grouping = DataTemplateHelper.LoadContent(this.groupingTemplateLogic) as DataBoundRowGrouping;
                if (null != grouping)
                {
                    grouping.DataSource = dataContext;
                    tempRow.Grouping = grouping;

                    if (null == this.lastRow)
                    {
                        // new header as we are the first row and we have a grouping template
                        addHeaderIntoPanel = true;
                    }
                    else
                    {
                        if (this.lastRow.Grouping.GroupingText != tempRow.Grouping.GroupingText)
                        {
                            // we have a grouping template and we are not the same as the last row.
                            addHeaderIntoPanel = true;
                        }
                    }

                    // Update the groupings hash table.
                    if (this.hashGroupings.ContainsKey(tempRow.Grouping.GroupingText))
                    {
                        Collection<DataBoundRow> groupedRows = this.hashGroupings[tempRow.Grouping.GroupingText];
                        groupedRows.Add(tempRow);
                    }
                    else
                    {
                        Collection<DataBoundRow> groupedRows = new Collection<DataBoundRow>();
                        groupedRows.Add(tempRow);
                        this.hashGroupings.Add(tempRow.Grouping.GroupingText, groupedRows);
                    }
                }
            }

            // Indicates this row contains group header
            if (addHeaderIntoPanel && this.lastRow != null)
            {
                this.RaiseGroupingRenderEvent();
            }

            foreach (ColumnManager column in this.columns)
            {
                if (this.rightToLeft)
                {
                    column.StackPanel.Children.Insert(0, new StackPanel());
                }
                else
                {
                    column.StackPanel.Children.Add(new StackPanel());
                }
            }

            tempRow.Index = this.rows.Count;
            tempRow.IsBlank = true;
            this.lastRow = tempRow;
            this.rows.Add(tempRow);
        }

        /// <summary>
        /// Updates the grouping title with the number of rows in the group.
        /// </summary>
        public void UpdateGroupCounts()
        {
            foreach (TextBlock tb in this.groupingTitles)
            {
                if (this.hashGroupings.ContainsKey(tb.Text))
                {
                    tb.Text = string.Format(System.Globalization.CultureInfo.CurrentCulture, WrapDataGridResources.GroupTitleFormat, tb.Text, this.hashGroupings[tb.Text].Count);
                }
            }
        }

        /// <summary>
        /// Clears the contents of our columns and resets our state.
        /// </summary>
        public void Clear()
        {
            foreach (ColumnManager manager in this.columns)
            {
                manager.StackPanel.Children.Clear();
            }

            this.rows = new Collection<DataBoundRow>();
            this.hashGroupings.Clear();
            this.groupingTitles.Clear();
            this.lastRow = null;
            this.firstVisibleItemIndex = 0;
            this.lastVisibleItemIndex = -1;
            SummaryManager summaryMgr = this.columns[0].SummaryManager;
            if (summaryMgr != null && summaryMgr.MainPanel != null)
            {               
               summaryMgr.MainPanel.Visibility = Visibility.Collapsed;               
            }
        }

        /// <summary>
        /// Clears the columns in this view.
        /// </summary>
        public void Remove()
        {
            this.Columns.Clear();
        }
        
        /// <summary>
        /// Expands all the groups.
        /// </summary>
        public void ExpandAllGroups()
        {
            if (this.hashGroupings.Count > 0)
            {
                foreach (string groupingKey in this.hashGroupings.Keys)
                {
                    Collection<DataBoundRow> groupedRows = this.hashGroupings[groupingKey];
                    foreach (DataBoundRow row in groupedRows)
                    {                        
                        row.CollapseRow(false, true);
                        row.Visibility = Visibility.Visible;                        
                    }
                }

                this.RaiseGroupStateChangedEvent();
            }
        }

        /// <summary>
        /// Collpases all the groups.
        /// </summary>
        public void CollapseAllGroups()
        {
            if (this.hashGroupings.Count > 0)
            {
                foreach (string groupingKey in this.hashGroupings.Keys)
                {
                    Collection<DataBoundRow> groupedRows = this.hashGroupings[groupingKey];
                    foreach (DataBoundRow row in groupedRows)
                    {
                        row.CollapseRow(true, true);
                        row.Visibility = Visibility.Collapsed;
                    }
                }

                this.RaiseGroupStateChangedEvent();
            }
        }

        /// <summary>
        /// Gets all the group keys.
        /// </summary>
        /// <returns>Returns an array of group keys.</returns>
        public string[] GetGroups()
        {
            string[] groupKeys = new string[this.hashGroupings.Count];

            if (this.hashGroupings.Count > 0)
            {
                this.hashGroupings.Keys.CopyTo(groupKeys, 0);
            }

            return groupKeys;
        }
        
        /// <summary>
        /// Gets rows under a specified group key.
        /// </summary>
        /// <param name="groupKey">Group key to use to filter the rows.</param>
        /// <returns>Returns a collection of rows that belongs to the specified group key.</returns>
        public Collection<DataBoundRow> GetGroupedRows(string groupKey)
        {
            if (this.hashGroupings.ContainsKey(groupKey))
            {
                return this.hashGroupings[groupKey];
            }

            return null;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Toggles the group expanded or collapsed state.
        /// </summary>
        /// <param name="groupKey">Group key of the group to toggle the state.</param>
        /// <param name="isnextgroupblank">Indicates whether the first row of the next group is blank.</param>
        /// <param name="nextgroupfirstrowindex">Returns index of the first row of the next group.</param>
        internal void ToggleGroupState(string groupKey, out bool isnextgroupblank, out int nextgroupfirstrowindex)
        {
            bool isgroupbeingcollapsed = false;
            isnextgroupblank = false;
            nextgroupfirstrowindex = 0;
            
            if (this.hashGroupings.ContainsKey(groupKey))
            {
                Collection<DataBoundRow> groupedRows = this.hashGroupings[groupKey];
                foreach (DataBoundRow row in groupedRows)
                {
                    if (row.Visibility == Visibility.Visible)
                    {
                        row.CollapseRow(true, true);
                        row.Visibility = Visibility.Collapsed;
                        isgroupbeingcollapsed = true;
                    }
                    else
                    {
                        row.CollapseRow(false, true);
                        row.Visibility = Visibility.Visible;
                    }
                }

                // only check for next group being blank, if the current group is being collapsed
                if (isgroupbeingcollapsed) 
                {
                    Collection<DataBoundRow> nextGroupRows = this.GetNextGroupRows(groupKey);
                    if (null != nextGroupRows && nextGroupRows.Count > 0)
                    {
                        // indicate to the caller that the next group's data is not loaded yet
                        if (nextGroupRows[0].IsBlank)
                        {
                            isnextgroupblank = true;
                            nextgroupfirstrowindex = nextGroupRows[0].Index;
                        }
                    }
                }

                this.RaiseGroupStateChangedEvent();
            }
        }

        /// <summary>
        /// Updates the summary for Look ahead/behind columns.
        /// </summary>
        /// <param name="dataContext">The data context for summary.</param>
        /// <param name="rowIndex">Row index for the current binding.</param>
        /// <param name="behindSummary">Specifies if the summary is for look behind or not.</param>
        internal void UpdateSummary(object dataContext, int rowIndex, bool behindSummary)
        {
            if (this.columns.Count > 0 && this.columns[0].SummaryManager != null)
            {
                SummaryManager summaryManager = this.columns[0].SummaryManager;
                Panel dataTemplate;
                if (summaryManager.MainPanel.Children.Count == 0)
                {
                    object templateObject = DataTemplateHelper.LoadContent(summaryManager.CellTemplate);
                    dataTemplate = templateObject as Panel;
                    dataTemplate.DataContext = dataContext;
                    summaryManager.MainPanel.Children.Insert(0, dataTemplate);
                }
                else
                {
                    dataTemplate = summaryManager.MainPanel.Children[0] as Panel;
                    dataTemplate.DataContext = dataContext;
                }

                dataTemplate.Tag = String.Format(System.Globalization.CultureInfo.CurrentCulture, WrapDataGridResources.SummaryCellTemplateTagFormat, rowIndex + (behindSummary ? -1 : 1));               
            }
        }

        /// <summary>
        /// Marks the add row process as complete and raises the group render event for the last group.
        /// </summary>
        internal void AddRowsComplete()
        {           
            if (this.lastRow != null && this.lastRow.Grouping != null)
            {
                this.RaiseGroupingRenderEvent();
            }
        }
        #endregion       

        #region Private Methods
        /// <summary>
        /// Loads the template content for a cell in main view.
        /// </summary>
        /// <param name="column">Template column.</param>        
        /// <returns>UIElement to be loaded into the cell.</returns>
        private static UIElement LoadMainViewContent(ColumnManager column)
        {
            UIElement customCellTemplate;

            if (column.MasterCellTemplate == null)
            {
                customCellTemplate = DataTemplateHelper.LoadContent(column.CellTemplate) as UIElement;
                return customCellTemplate;
            }

            Grid cellTemplate = DataTemplateHelper.LoadContent(column.MasterCellTemplate) as Grid;
            customCellTemplate = DataTemplateHelper.LoadContent(column.CellTemplate) as UIElement;
            if (column.BodyMargin != null)
            {
                customCellTemplate.SetValue(Canvas.MarginProperty, column.BodyMargin);
            }

            customCellTemplate.SetValue(Grid.ColumnProperty, 0);
            customCellTemplate.SetValue(Grid.RowProperty, 1);
            cellTemplate.Children.Add(customCellTemplate);

            return cellTemplate;
        }

        /// <summary>
        /// Loads the template content for a cell in look ahead/look behind view.
        /// </summary>
        /// <param name="column">Template column.</param>        
        /// <returns>UIElement to be loaded into the cell.</returns>
        private static UIElement LoadViewContent(ColumnManager column)
        {
            Grid cellTemplate = DataTemplateHelper.LoadContent(column.MasterCellTemplate) as Grid;
            UIElement customLookAheadCellTemplate = DataTemplateHelper.LoadContent(column.CellTemplate) as UIElement;
            cellTemplate.Children.Add(customLookAheadCellTemplate);
            return cellTemplate;
        }

        /// <summary>
        /// Capture the left mouse click on the column header.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void ColumnHeader_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            Grid columnHeader = sender as Grid;
            if (null != this.OnColumnHeaderClick && columnHeader != null)
            {                
                ColumnManager columnManager;

                for (int columnIndex = 0; columnIndex < this.Columns.Count; columnIndex++)
                {
                    columnManager = this.Columns[columnIndex];

                    if (columnManager.ColumnHeader == columnHeader)
                    {
                        this.OnColumnHeaderClick(this, new ColumnHeaderClickEventArgs(columnIndex, columnManager));
                    }                    
                }
            }
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event on Grouping header.
        /// </summary>
        /// <param name="sender">Grouping header.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void GroupingPresentation_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid groupingHeader = sender as Grid;
            if (null == groupingHeader)
            {
                throw new System.ArgumentException(WrapDataGridResources.InvalidGroupingPresentationTemplate);
            }

            e.Handled = true;
            
            if (this.OnGroupHeaderClick != null)
            {
                this.OnGroupHeaderClick(groupingHeader, new GroupByEventArgs(groupingHeader.Tag.ToString(), groupingHeader.Tag.ToString()));
            }                        
        }

        /// <summary>
        /// Handles the Loaded event for each cell.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void DataTemplate_Loaded(object sender, RoutedEventArgs e)
        {
           this.SetRowHeight(sender as FrameworkElement);
        }

        /// <summary>
        /// Handles the SizeChanged event for each cell.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="System.Windows.SizeChangedEventArgs"/> instance containing the event data.</param>
        private void DataTemplate_SizeChanged(object sender, SizeChangedEventArgs e)
        {
           this.SetRowHeight(sender as FrameworkElement);
        }

        /// <summary>
        /// Sets the Row height.
        /// </summary>
        /// <param name="dataTemplate">Cell template.</param>
        private void SetRowHeight(FrameworkElement dataTemplate)
        {
            if (null == dataTemplate)
            {
                throw new System.ArgumentException(WrapDataGridResources.InvalidCellDataTemplate);
            }

            DataBoundRow row = row = this.hashCells[dataTemplate.Name] as DataBoundRow;

            if (null == row)
            {
                throw new System.ArgumentException(WrapDataGridResources.NoDataRowFound);
            }

            if (!row.RowCollapsed)
            {
                if (!Double.IsNaN(dataTemplate.ActualHeight) && !Double.IsInfinity(dataTemplate.ActualHeight))
                {
                    row.RowHeight = dataTemplate.ActualHeight;
                }
            }
        }

        /// <summary>
        /// Raises the group state changed event.
        /// </summary>
        private void RaiseGroupStateChangedEvent()
        {
            if (this.OnGroupStateChanged != null)
            {
                this.OnGroupStateChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Raises the grouping render event.
        /// </summary>
        private void RaiseGroupingRenderEvent()
        {
            string groupingKey = this.lastRow.Grouping.GroupingText;
            Collection<DataBoundRow> rows = this.GetGroupedRows(groupingKey);
            UIElement groupHeader = rows[0].GroupingHeader;

            if (this.OnGroupingRender != null)
            {
                this.OnGroupingRender(this.hostingWrapDataGrid, new GroupingRenderEventArgs(groupingKey, rows, groupHeader));
            }
        }

        /// <summary>
        /// Loads the template content for a cell.
        /// </summary>
        /// <param name="column">Template column.</param>        
        /// <returns>UIElement to be loaded into the cell.</returns>
        private UIElement LoadContent(ColumnManager column)
        {
            if (this.MainView)
            {
                return ColumnView.LoadMainViewContent(column);
            }
            else
            {
                return ColumnView.LoadViewContent(column);
            }
        }

        /// <summary>
        /// Gets the set of grouped rows from the next group that follows the given groupKey.
        /// </summary>
        /// <param name="groupKey">The Group key whose next group is to be determined.</param>
        /// <returns>Collection of Rows of the next group.</returns>
        private Collection<DataBoundRow> GetNextGroupRows(string groupKey)
        {
            bool keyFound = false;
            foreach (KeyValuePair<string, Collection<DataBoundRow>> currentKeyValuePair in this.hashGroupings)
            {
                if (keyFound)
                {
                    return currentKeyValuePair.Value;
                }
                
                if (currentKeyValuePair.Key != null && currentKeyValuePair.Key.ToString().Equals(groupKey))
                {
                    keyFound = true;
                }
            }

            return null;
        }

        #endregion
    }
}
