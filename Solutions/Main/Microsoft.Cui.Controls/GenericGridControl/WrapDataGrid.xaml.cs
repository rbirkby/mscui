//-----------------------------------------------------------------------
// <copyright file="WrapDataGrid.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The WrapDataGrid Silverlight control. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Collections.Generic;
    using System.Collections;
    using System.Threading;
    using System.Windows.Threading;
    using Microsoft.Cui.Controls.GenericGridControl;   

    /// <summary>
    /// Sort directions.
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// Ascending order.
        /// </summary>
        Ascending,

        /// <summary>
        /// Descending order.
        /// </summary>
        Descending
    }

    /// <summary>
    /// Source of item selection.
    /// </summary>
    public enum SelectionSource
    {
        /// <summary>
        /// None - i.e. code.
        /// </summary>
        None = 0,

        /// <summary>
        /// The mouse.
        /// </summary>
        Mouse,

        /// <summary>
        /// The keyboard.
        /// </summary>
        Keyboard
    }

    /// <summary>
    /// Implementation of the WrapDataGrid class.
    /// </summary>
    public partial class WrapDataGrid : UserControl, IDisposable
    {
        #region Dependency Properties

        /// <summary>
        /// Number of columns in the grid to display.
        /// </summary>
        public static readonly DependencyProperty NumberOfColsProperty = DependencyProperty.Register("NumberOfCols", typeof(int), typeof(WrapDataGrid), new PropertyMetadata(new PropertyChangedCallback(NumberOfColsChanged)));

        /// <summary>
        /// Look Ahead Visibility Property.
        /// </summary>
        public static readonly DependencyProperty LookAheadVisibilityProperty = DependencyProperty.Register("LookAheadVisibility", typeof(Visibility), typeof(WrapDataGrid), new PropertyMetadata(new PropertyChangedCallback(ToggleLookAheadVisibility)));
        
        /// <summary>
        /// Look Behind Visibility Property.
        /// </summary>
        public static readonly DependencyProperty LookBehindVisibilityProperty = DependencyProperty.Register("LookBehindVisibility", typeof(Visibility), typeof(WrapDataGrid), new PropertyMetadata(new PropertyChangedCallback(ToggleLookBehindVisibility)));

        /// <summary>
        /// Row Background brush.
        /// </summary>
        public static readonly DependencyProperty RowBackgroundProperty = DependencyProperty.Register("RowBackground", typeof(Brush), typeof(WrapDataGrid), new PropertyMetadata(new PropertyChangedCallback(OnRowBackgroundPropertyChanged)));

        /// <summary>
        /// Alternating Row Background brush.
        /// </summary>
        public static readonly DependencyProperty AlternatingRowBackgroundProperty = DependencyProperty.Register("AlternatingRowBackground", typeof(Brush), typeof(WrapDataGrid), new PropertyMetadata(new PropertyChangedCallback(OnAlternatingRowBackgroundPropertyChanged)));

        /// <summary>
        /// Selected Row Background brush.
        /// </summary>
        public static readonly DependencyProperty SelectionBackgroundProperty = DependencyProperty.Register("SelectionBackground", typeof(Brush), typeof(WrapDataGrid), new PropertyMetadata(new PropertyChangedCallback(OnSelectionBackgroundPropertyChanged)));

        #endregion

        #region Private Member variables
        /// <summary>
        /// Main view row index.
        /// </summary>
        private const int MainViewRowIndex = 4;

        /// <summary>
        /// Look Ahead row index.
        /// </summary>
        private const int LookAheadRowIndex = 5;

        /// <summary>
        /// Look behind row index.
        /// </summary>
        private const int LookBehindRowIndex = 3;

        /// <summary>
        /// The mainView private member variable.
        /// </summary>
        private MainView mainView;

        /// <summary>
        /// The lookAheadView private member variable.
        /// </summary>
        private LookAheadView lookAheadView;

        /// <summary>
        /// The lookBehindView private member variable.
        /// </summary>
        private LookBehindView lookBehindView;

        /// <summary>
        /// The layoutRoot private member variable.
        /// </summary>
        private Grid layoutRoot;

        /// <summary>
        /// The provider private member variable.
        /// </summary>
        private IEnumerable provider;

        /// <summary>
        /// The scrollbar private member variable.
        /// </summary>
        private ScrollBar scrollBar;

        /// <summary>
        /// The groupingDataTemplateLogic private member variable.
        /// </summary>
        private DataTemplate groupingDataTemplateLogic;

        /// <summary>
        /// The groupingDataTemplatePresentation private member variable.
        /// </summary>
        private DataTemplate groupingDataTemplatePresentation;

        /// <summary>
        /// The selection mode for the control.
        /// </summary>
        private SelectionMode selectionMode = SelectionMode.One;

        /// <summary>
        /// The selected item.
        /// </summary>
        private DataBoundRow selectedItem;

        /// <summary>
        /// The index of the selected item.
        /// </summary>
        private int selectedIndex = -1;

        /// <summary>
        /// List of indices of selected row or rows when multi-selecting.
        /// </summary>
        private Collection<int> selectedIndices = new Collection<int>();

        /// <summary>
        /// List of selected row or rows when multi-selecting.
        /// </summary>
        private Collection<DataBoundRow> selectedItems = new Collection<DataBoundRow>();

        /// <summary>
        /// Member variable to hold the columns.
        /// </summary>
        private WrapDataGridColumnCollection columns = new WrapDataGridColumnCollection();

        /// <summary>
        /// Member variable to hold look ahead cell template.
        /// </summary>
        private DataTemplate lookAheadCellTemplate;

        /// <summary>
        /// Member variable to hold look behind cell template.
        /// </summary>
        private DataTemplate lookBehindCellTemplate;

        /// <summary>
        /// Member variable to hold look ahead summary cell template.
        /// </summary>
        private DataTemplate lookAheadSummaryCellTemplate;

        /// <summary>
        /// Member variable to hold look behind summary cell template.
        /// </summary>
        private DataTemplate lookBehindSummaryCellTemplate;

        /// <summary>
        /// Member variable to indicate whether to add column resizers.
        /// </summary>
        private bool allowColumnResizing = true;

        /// <summary>
        /// Member variable to indicate whether the ViewportSize has been loaded.
        /// </summary>
        private bool viewportLoaded;

        /// <summary>
        /// Member variable to hold column managers for main view.
        /// </summary>
        private Collection<ColumnManager> mainViewColumns = new Collection<ColumnManager>();

        /// <summary>
        /// Member variable to hold look ahead column manager.
        /// </summary>
        private ColumnManager lookAheadColumn;

        /// <summary>
        /// Member variable to hold look behind column manager.
        /// </summary>
        private ColumnManager lookBehindColumn;

        /// <summary>
        /// Member variable to hold column resizer template.
        /// </summary>
        private DataTemplate columnResizerTemplate;

        /// <summary>
        /// The ascendingOrderIndicatorDataTemplate private member variable.
        /// </summary>
        private DataTemplate ascendingOrderIndicatorDataTemplate;

        /// <summary>
        /// The descendingOrderIndicatorDataTemplate private member variable.
        /// </summary>
        private DataTemplate descendingOrderIndicatorDataTemplate;

        /// <summary>
        /// Timer control to invoke layout refresh.
        /// </summary>
        private Timer timer;

        /// <summary>
        /// Indicates whether the last row is occluded or completely visible.
        /// </summary>
        private bool lastRowOccluded;

        /// <summary>
        /// Member variable to indicate whether to select the row by force.
        /// </summary>
        private bool forceSelect;

        /// <summary>
        /// Member variable to indicate whether to scroll to a particular row.
        /// </summary>
        private bool scrollToRow;

        /// <summary>
        /// Member variable to indicate that the WrapDataGrid is currently being loaded.
        /// </summary>
        private bool gridIsBeingLoaded;

        /// <summary>
        /// Member variable to indicate that the WrapDataGrid rows are currently being paged.
        /// </summary>
        private bool pageOperationInvoked;

        /// <summary>
        /// Member variable to hold the row index to scroll.
        /// </summary>
        private int scrollToRowIndex;

        /// <summary>
        /// Timer to handle scroll events asynchronously.
        /// </summary>
        private DispatcherTimer scrollTimer;

        /// <summary>
        /// Value set while scrolling.
        /// </summary>
        private bool handlingScrollEvent;

        /// <summary>
        /// Index of the first row in a marquee selection.
        /// </summary>
        private int anchorRowIndex;

        /// <summary>
        /// Indicates if look ahead size changed should check for last row occluded or not.
        /// </summary>
        /// <remarks>
        /// Look ahead size change event uses this. We can't check for last row occlusion in measure grid
        /// because visual tree of look ahead is rendered later.
        /// </remarks>
        private bool checkLastRowOccluded;

        /// <summary>
        /// Variable to hold the reference to the generic wait animation control.
        /// </summary>
        private WaitAnimation genericAnimation = new WaitAnimation("Please Wait...");

        /// <summary>
        /// Specifies the gap of last row from bottom.
        /// </summary>
        /// <remarks>
        /// Look ahead size changed uses it to decide whether the last row is occluded or not.
        /// </remarks>
        private double lastRowGapFromBottom;

        /// <summary>
        /// Specifies the last selected row.
        /// </summary>
        private DataBoundRow lastHitRow;

        /// <summary>
        /// Indicates if column headers are visible.
        /// </summary>
        private bool columnHeadersVisible = true;

        /// <summary>
        /// Indicates if control is used to do non-contiguous selection.
        /// </summary>
        private bool handlingControlMultiSelection;

        /// <summary>
        /// Indicates if the spacebar is pressed.
        /// </summary>
        private bool spacebarKeyDown;

        /// <summary>
        /// The background for a highlighted even row.
        /// </summary>
        private Brush currentEvenRowBackground;

        /// <summary>
        /// The background for a highlighted odd row.
        /// </summary>
        private Brush currentOddRowBackground;

        /// <summary>
        /// The background for a highlighted and selected row.
        /// </summary>
        private Brush currentSelectedRowBackground;

        /// <summary>
        /// The width of the hidden row.
        /// </summary>
        private double hiddenRowHeight;

        /// <summary>
        /// The index of the first currently visible row from the top.
        /// </summary>
        private int currentRowIndex;

        /// <summary>
        /// Dictionary to hold the width of the hidden columns.
        /// </summary>
        private Dictionary<int, double> hiddenColumnWidthCollection = new Dictionary<int, double>();

        /// <summary>
        /// Dictionary to hold the minwidth of the hidden columns.
        /// </summary>
        private Dictionary<int, double> hiddenColumnMinWidthCollection = new Dictionary<int, double>();

        /// <summary>
        /// Dictionary to hold the maxwidth of the hidden columns.
        /// </summary>
        private Dictionary<int, double> hiddenColumnMaxWidthCollection = new Dictionary<int, double>();

        /// <summary>
        /// Indicates whether the scroll wait animation is currently being displayed.
        /// </summary>
        private bool scrollWaitAnimationIsBeingDisplayed;

        /// <summary>
        /// Variable to hold the reference to the wait animation control.
        /// </summary>
        private WaitAnimation scrollAnimation = new WaitAnimation();

        /// <summary>
        /// Variable to hold the reference to the look ahead control.
        /// </summary>
        private FrameworkElement lookAheadElement;

        /// <summary>
        /// Variable to hold the reference to the look behind control.
        /// </summary>
        private FrameworkElement lookBehindElement;

        /// <summary>
        /// Holds the height that was initially set for the scrollbar while the grid was being loaded.
        /// This is used instead of "ActualHeight" at places, because the "actualheight" may keep changing.
        /// </summary>
        private double heightSetForScrollbar;

        /// <summary>
        /// Stores the index that raised the last Scrollbar_scroll event.
        /// </summary>
        private int lastScrollIndex;

        /// <summary>
        /// Stores the time that the last SCrollbar_Scroll event took place.
        /// </summary>
        private DateTime lastScrollTime;

        /// <summary>
        /// Stores the previous width checked for LA.
        /// </summary>
        private double previousWidthForLookAhead = -1.0;

        /// <summary>
        /// Stores the number of times a fresh batch of Rows was loaded using the LA check function.
        /// </summary>
        private int numberOfTimesFreshBatchWasLoaded;

        /// <summary>
        /// All columns are hidden.
        /// </summary>
        private bool allColumnsHidden;

        /// <summary>
        /// Last Row having focus.
        /// </summary>
        private int lastRowHavingFocusIndex = -1;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WrapDataGrid"/> class.
        /// </summary>
        public WrapDataGrid()
        {
            // Required to initialize variables
            this.InitializeComponent();
            GradientStopCollection gSc = new GradientStopCollection();
            gSc.Add(new GradientStop()
            {
                Offset = 0,
                Color = Color.FromArgb(255, 187, 187, 187)
            });
            gSc.Add(new GradientStop()
            {
                Offset = 1,
                Color = Color.FromArgb(255, 222, 222, 222)
            });

            this.AlternatingRowBackground = new LinearGradientBrush(gSc, 90);
            this.RowBackground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

            GradientStopCollection grdStopCollection = new GradientStopCollection();
            grdStopCollection.Add(new GradientStop()
            {
                Offset = 0,
                Color = Colors.Black
            });
            grdStopCollection.Add(new GradientStop()
            {
                Offset = 0.02,
                Color = Color.FromArgb(255, 206, 226, 252)
            });
            grdStopCollection.Add(new GradientStop()
            {
                Offset = 0.5,
                Color = Color.FromArgb(255, 230, 240, 255)
            });
            grdStopCollection.Add(new GradientStop()
            {
                Offset = 0.98,
                Color = Color.FromArgb(255, 206, 226, 252)
            });
            grdStopCollection.Add(new GradientStop()
            {
                Offset = 1,
                Color = Colors.Black
            });

            this.SelectionBackground = new LinearGradientBrush(grdStopCollection, 90);
            this.timer = new Timer(new TimerCallback(this.RefreshLayout), null, Timeout.Infinite, Timeout.Infinite);
        }
        #endregion

        #region Delegates and Events
        /// <summary>
        /// Delegate to invoke refresh layout.
        /// </summary>
        /// <param name="startOffset">Start offset.</param>
        private delegate void RefreshLayoutDelegate(int startOffset);

        /// <summary>
        /// Column header click event which may be used in sorting the column data.
        /// </summary>
        public event System.EventHandler<EventArgs> OnColumnHeaderClick;

        /// <summary>
        /// Key pressed event which may be used to identify a key pressed inside the grid.
        /// </summary>
        public event System.EventHandler<EventArgs> OnKeyPress;

        /// <summary>
        /// Selection changed event which may be used when the selected item in the grid is changed.
        /// </summary>
        public event System.EventHandler<EventArgs> OnSelectionChanged;

        /// <summary>
        /// Grouping render event which may be used while grouping is rendered.
        /// </summary>
        public event System.EventHandler<EventArgs> OnGroupingRender;

        /// <summary>
        /// Occurs when a group header is clicked when grouping has been applied to the data in the grid.
        /// </summary>
        public event System.EventHandler<EventArgs> OnGroupHeaderClick;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the value of the LookAheadVisibility property.
        /// </summary>
        /// <value>
        /// Look Ahead Visibility.
        /// </value>
        public Visibility LookAheadVisibility
        {
            get { return (Visibility)GetValue(LookAheadVisibilityProperty); }
            set { SetValue(LookAheadVisibilityProperty, (Visibility)value); }
        }

        /// <summary>
        /// Gets or sets the value of the LookbehindVisibility property.
        /// </summary>
        /// <value>
        /// Look Behind Visibility.
        /// </value>
        public Visibility LookBehindVisibility
        {
            get { return (Visibility)GetValue(LookBehindVisibilityProperty); }
            set { SetValue(LookBehindVisibilityProperty, (Visibility)value); }
        }

        /// <summary>
        /// Gets or sets the GroupingDataTemplatePresentation for the grid.
        /// </summary>
        /// <value>Grouping data template presentation.</value>
        public DataTemplate GroupingDataTemplatePresentation
        {
            get
            {
                return this.groupingDataTemplatePresentation;
            }

            set
            {
                this.groupingDataTemplatePresentation = value;
            }
        }

        /// <summary>
        /// Gets or sets the DataTemplate specifying the logic for grouping data in the grid.
        /// </summary>
        /// <value>Logic of grouping data template.</value>
        public DataTemplate GroupingDataTemplateLogic
        {
            get
            {
                return this.groupingDataTemplateLogic;
            }

            set
            {
                this.groupingDataTemplateLogic = value;
            }
        }

        /// <summary>
        /// Gets the collection of selected rows in the grid.
        /// </summary>
        /// <value>The selected rows.</value>
        public Collection<DataBoundRow> SelectedRows
        {
            get
            {
                return this.SelectedItems;
            }
        }

        /// <summary>
        /// Gets the main view of the grid containing the rows of data.
        /// </summary>
        /// <value>Main display view.</value>
        public ColumnView MainView
        {
            get
            {
                if (this.mainView != null && this.mainView.View != null)
                {
                    return this.mainView.View;
                }

                return null;
            }
        }

        /// <summary> 
        ///  Gets or sets the brush specifying the background color of an odd row in the grid.
        /// </summary>
        /// <value>Row background.</value>
        public Brush RowBackground
        {
            get
            {
                return GetValue(RowBackgroundProperty) as Brush;
            }

            set
            {
                SetValue(RowBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the brush specifying the background color of an even row in the grid.
        /// </summary>
        /// <value>Alternating row background.</value>
        public Brush AlternatingRowBackground
        {
            get
            {
                return GetValue(AlternatingRowBackgroundProperty) as Brush;
            }

            set
            {
                SetValue(AlternatingRowBackgroundProperty, value);
            }
        }

        /// <summary>
        ///  Gets or sets the brush specifying the background color of a selected row in the grid.
        /// </summary>
        /// <value>Selection background.</value>
        public Brush SelectionBackground
        {
            get
            {
                return GetValue(SelectionBackgroundProperty) as Brush;
            }

            set
            {
                SetValue(SelectionBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the brush for the highlight of an even row in the grid.
        /// </summary>
        /// <value>
        /// Highlight Brush.
        /// </value>
        public Brush CurrentEvenRowBackground
        {
            get
            {
                return ((this.currentEvenRowBackground == null) ? new SolidColorBrush(Colors.White) : this.currentEvenRowBackground);
            }

            set
            {
                this.currentEvenRowBackground = (value == null) ? new SolidColorBrush(Colors.White) : value;
            }
        }

        /// <summary>
        /// Gets or sets the brush for the highlight of an odd row in the grid.
        /// </summary>
        /// <value>
        /// Highlight Brush.
        /// </value>
        public Brush CurrentOddRowBackground
        {
            get
            {
                return ((this.currentOddRowBackground == null) ? new SolidColorBrush(Colors.LightGray) : this.currentOddRowBackground);
            }

            set
            {
                this.currentOddRowBackground = (value == null) ? new SolidColorBrush(Colors.LightGray) : value;
            }
        }

        /// <summary>
        /// Gets or sets the brush for the highlight of a selected row in the grid.
        /// </summary>
        /// <value>
        /// Highlight Selected Background Brush.
        /// </value>
        public Brush CurrentSelectedRowBackground
        {
            get
            {
                return ((this.currentSelectedRowBackground == null) ? new SolidColorBrush(Colors.Gray) : this.currentSelectedRowBackground);
            }

            set
            {
                this.currentSelectedRowBackground = (value == null) ? new SolidColorBrush(Colors.Gray) : value;
            }
        }

        /// <summary>
        /// Gets or sets the selection mode for the control.
        /// </summary>
        /// <value>
        /// Selection mode.
        /// </value>
        public SelectionMode SelectionMode
        {
            get
            {
                return this.selectionMode;
            }

            set
            {
                this.selectionMode = value;
                if (this.mainView != null && this.mainView.View != null)
                {
                    this.mainView.View.SelectionMode = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected row in the grid.
        /// </summary>
        /// <value>The selected DataBoundRow item.</value>
        public DataBoundRow SelectedItem
        {
            get
            {
                return this.selectedItem;
            }

            set
            {
                this.selectedItem = value;
            }
        }

        /// <summary>
        /// Gets the list of selected row or rows when multi-selecting.
        /// </summary>
        /// <value>Selected items.</value>
        public Collection<DataBoundRow> SelectedItems
        {
            get
            {
                return this.selectedItems;
            }
        }

        /// <summary>
        /// Gets or sets the index of the selected row in the grid.
        /// </summary>
        /// <value>The index of the selected item.</value>
        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }

            set
            {
                if (value < -1 || value > this.mainView.View.Rows.Count)
                {
                    throw new ArgumentOutOfRangeException("value", "SelectedIndex out of range");
                }

                this.SelectedIndexes.Clear();
                this.SelectedIndexes.Add(value);
                this.selectedIndex = value;
            }
        }

        /// <summary>
        /// Gets the list of indices of the selected row or rows when multi-selecting.
        /// </summary>
        /// <value>Selected indexes.</value>
        public Collection<int> SelectedIndexes
        {
            get
            {
                return this.selectedIndices;
            }
        }

        /// <summary>
        /// Gets the Columns for the main view.
        /// </summary>
        /// <value>Grid columns.</value>
        public WrapDataGridColumnCollection WrapDataGridColumns
        {
            get
            {
                return this.columns;
            }
        }

        /// <summary>
        /// Gets or sets the cell template for Look ahead scrollbar.
        /// </summary>
        /// <value>Look ahead cell template.</value>
        public DataTemplate LookAheadCellTemplate
        {
            get
            {
                return this.lookAheadCellTemplate;
            }

            set
            {
                this.lookAheadCellTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets the cell template for Look behind scrollbar.
        /// </summary>
        /// <value>Look behind cell template.</value>
        public DataTemplate LookBehindCellTemplate
        {
            get
            {
                return this.lookBehindCellTemplate;
            }

            set
            {
                this.lookBehindCellTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets the summary cell template for the Look ahead view.
        /// </summary>
        /// <value>Look ahead summary cell template.</value>
        public DataTemplate LookAheadSummaryCellTemplate
        {
            get
            {
                return this.lookAheadSummaryCellTemplate;
            }

            set
            {
                this.lookAheadSummaryCellTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets the summary cell template for the Look behind view.
        /// </summary>
        /// <value>Look behind summary cell template.</value>
        public DataTemplate LookBehindSummaryCellTemplate
        {
            get
            {
                return this.lookBehindSummaryCellTemplate;
            }

            set
            {
                this.lookBehindSummaryCellTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the columns can be resized. This value needs to be set at design time.
        /// </summary>
        /// <value>Status of column resizing.</value>
        public bool AllowColumnResizing
        {
            get
            {
                return this.allowColumnResizing;
            }

            set
            {
                this.allowColumnResizing = value;
            }
        }           

        /// <summary>
        /// Gets or sets a value indicating whether all the columns are hidden.
        /// </summary>
        /// <value>True if all columns are hidden.</value>
        public bool AllColumnsHidden
        {
            get
            {
                return this.allColumnsHidden;
            }

            set
            {
                this.allColumnsHidden = value;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the columns headers are visible.
        /// </summary>
        /// <value>
        /// Column Header visibility status.
        /// </value>
        public bool ColumnHeadersVisible
        {
            get
            {
                return this.columnHeadersVisible;
            }

            set
            {
                if (this.columnHeadersVisible != value)
                {
                    this.columnHeadersVisible = value;
                    this.ShowColumnHeaders(this.columnHeadersVisible);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Column resizer template.
        /// </summary>
        /// <value>
        /// Column resize template.
        /// </value>
        public DataTemplate ColumnResizerTemplate
        {
            get
            {
                return this.columnResizerTemplate;
            }

            set
            {
                this.columnResizerTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets the DataTemplate for Ascending order indicator.
        /// </summary>
        /// <value>Ascending order indicator data template.</value>
        public DataTemplate AscendingOrderIndicatorDataTemplate
        {
            get
            {
                return this.ascendingOrderIndicatorDataTemplate;
            }

            set
            {
                this.ascendingOrderIndicatorDataTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets the DataTemplate for Descending order indicator.
        /// </summary>
        /// <value>Descending order indicator data template.</value>
        public DataTemplate DescendingOrderIndicatorDataTemplate
        {
            get
            {
                return this.descendingOrderIndicatorDataTemplate;
            }

            set
            {
                this.descendingOrderIndicatorDataTemplate = value;
            }
        }
        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets or sets the index of the first row in the anchored marquee selection.
        /// </summary>
        internal int AnchorRowIndex
        {
            get
            {
                return this.anchorRowIndex;
            }

            private set
            {
                this.anchorRowIndex = value;
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets or sets a value indicating whether the value of scroll to row is set.
        /// </summary>
        /// <value>
        /// Scroll to row.
        /// </value>
        private bool ScrollToRow
        {
            get
            {
                return this.scrollToRow;
            }

            set
            {
                if (this.scrollToRowIndex != -1)
                {
                    this.EnsureRowVisible(this.scrollToRowIndex);
                    this.scrollToRowIndex = -1;
                }

                this.scrollToRow = value;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Handle rows when grid looses focus.
        /// </summary>       
        public void HandleLostFocus()
        {
            foreach (DataBoundRow row in this.SelectedItems)
            {
                row.Select(false);
                row.Highlight(false);
                row.SelectCollectionRow(true);
                row.RowSelected = true;
                row.RowFocus = false;
            }

            if (this.lastRowHavingFocusIndex != -1 && this.SelectedItems.IndexOf(this.MainView.Rows[this.lastRowHavingFocusIndex]) == -1)
            {
                DataBoundRow row = this.MainView.Rows[this.lastRowHavingFocusIndex];
                row.Highlight(false);
                row.RowSelected = false;
                row.RowFocus = false;
            }
            else if (this.MainView.Rows.Count > 0 && this.MainView.Rows[0].RowFocus && !this.MainView.Rows[0].RowSelected)
            {
                this.MainView.Rows[0].Highlight(false);
                this.MainView.Rows[0].RowFocus = false;
            }
        }

        /// <summary>
        /// Reloads the grid, re-populates the data in the views and resets the scrollbar.
        /// </summary>
        public void Reload()
        {
#if !SILVERLIGHT

            if (this.mainView == null)
            {
                return;
            }
#endif
            this.lastRowHavingFocusIndex = -1;
            this.mainView.View.Clear();
            this.lookAheadView.View.Clear();
            this.lookBehindView.View.Clear();
            this.mainView.View.GroupingTemplateLogic = this.GroupingDataTemplateLogic;
            this.mainView.View.GroupingTemplatePresentation = this.groupingDataTemplatePresentation;

            this.scrollBar.Value = 0;
            this.scrollBar.Visibility = Visibility.Collapsed;
            this.RenderData();
            if (!this.AllColumnsHidden)
            {
                this.EnsureRowVisible(0);
            }
        }

        /// <summary>
        /// Sets the size of the grouping column.
        /// </summary>
        public void UpdateGroupingColumnSize()
        {
            this.SetSizeForFirstColumn();
        }

        /// <summary>
        ///  Show/ Hide a column, with the specified index, in the grid.
        /// </summary>
        /// <param name="columnIndex">Column Index.</param>
        /// <param name="columnShown">True if the column is shown, False if the column is to be hidden.</param>
        public void ShowColumn(int columnIndex, bool columnShown)
        {
            if (columnIndex >= this.WrapDataGridColumns.Count)
            {
                return;
            }
            else if (!columnShown)
            {
                this.WrapDataGridColumns[columnIndex].IsVisible = columnShown;
                columnIndex++;

                if (this.hiddenColumnWidthCollection.ContainsKey(columnIndex))
                {
                    return;
                }

                this.hiddenColumnWidthCollection.Add(columnIndex, this.layoutRoot.ColumnDefinitions[columnIndex].Width.Value);
                this.hiddenColumnMinWidthCollection.Add(columnIndex, this.layoutRoot.ColumnDefinitions[columnIndex].MinWidth);
                if (!double.IsInfinity(this.layoutRoot.ColumnDefinitions[columnIndex].MaxWidth))
                {
                    this.hiddenColumnMaxWidthCollection.Add(columnIndex, this.layoutRoot.ColumnDefinitions[columnIndex].MaxWidth);
                }
                else
                {
                    this.hiddenColumnMaxWidthCollection.Add(columnIndex, double.PositiveInfinity);
                }

                if (this.layoutRoot.ColumnDefinitions[columnIndex] != null)
                {
                    this.layoutRoot.ColumnDefinitions[columnIndex].MinWidth = 0;
                    this.layoutRoot.ColumnDefinitions[columnIndex].MaxWidth = 0;
                    if (this.layoutRoot.ColumnDefinitions[columnIndex].Width.IsStar)
                    {
                        this.layoutRoot.ColumnDefinitions[columnIndex].Width = new GridLength(0, GridUnitType.Star);
                    }
                    else
                    {
                        this.layoutRoot.ColumnDefinitions[columnIndex].Width = new GridLength(0, GridUnitType.Pixel);
                    }

                    this.mainView.View.Columns[columnIndex].StackPanel.Visibility = Visibility.Collapsed;
                    if (columnIndex == this.WrapDataGridColumns.Count)
                    {
                        for (int childcount = 0; childcount < this.mainView.View.Columns[columnIndex].ColumnHeader.Children.Count; childcount++)
                        {
                            this.mainView.View.Columns[columnIndex].ColumnHeader.Children[childcount].Opacity = 0;
                        }
                    }
                }
            }
            else if (columnShown)
            {
                this.WrapDataGridColumns[columnIndex].IsVisible = columnShown;
                if (!this.WrapDataGridColumns[columnIndex].SortIndicatorShownOnSort)
                {
                    this.AddSortIndicator(columnIndex + 1, this.WrapDataGridColumns[columnIndex].PresentSortDirection);
                }

                columnIndex++;
                if (!this.hiddenColumnWidthCollection.ContainsKey(columnIndex))
                {
                    return;
                }

                if (this.layoutRoot.ColumnDefinitions[columnIndex] != null)
                {
                    if (this.layoutRoot.ColumnDefinitions[columnIndex].Width.IsStar)
                    {
                        this.layoutRoot.ColumnDefinitions[columnIndex].Width = new GridLength(this.hiddenColumnWidthCollection[columnIndex], GridUnitType.Star);
                    }
                    else
                    {
                        this.layoutRoot.ColumnDefinitions[columnIndex].Width = new GridLength(this.hiddenColumnWidthCollection[columnIndex], GridUnitType.Pixel);
                    }

                    this.layoutRoot.ColumnDefinitions[columnIndex].MinWidth = this.hiddenColumnMinWidthCollection[columnIndex];
                    this.layoutRoot.ColumnDefinitions[columnIndex].MaxWidth = this.hiddenColumnMaxWidthCollection[columnIndex];
                    this.mainView.View.Columns[columnIndex].StackPanel.Visibility = Visibility.Visible;
                    for (int childcount = 0; childcount < this.mainView.View.Columns[columnIndex].ColumnHeader.Children.Count; childcount++)
                    {
                        this.mainView.View.Columns[columnIndex].ColumnHeader.Children[childcount].Opacity = 1;
                    }
                }

                this.hiddenColumnWidthCollection.Remove(columnIndex);
                this.hiddenColumnMinWidthCollection.Remove(columnIndex);
                this.hiddenColumnMaxWidthCollection.Remove(columnIndex);
            }

            this.HideScrollbar();
            this.ToggleGroupingColumnVisibility();
            this.UpdateLookAheadBar();
            this.CheckAndRemoveExtraItemsFromViews();
        }

        /// <summary>
        /// Show or hide the column headers in the grid.
        /// </summary>
        /// <param name="headerShown">
        /// Boolean value which removes column headers on being set to false.
        /// </param>
        public void ShowColumnHeaders(bool headerShown)
        {
            if (headerShown == false)
            {
                this.hiddenRowHeight = this.Row1Headers.Height.Value;
                this.Row1Headers.Height = new GridLength(0);
            }
            else if (headerShown == true)
            {
                this.Row1Headers.Height = new GridLength(this.hiddenRowHeight, GridUnitType.Auto);
            }

            this.UpdateLookAheadBar();
            this.CheckAndRemoveExtraItemsFromViews();
        }

        /// <summary>
        /// Scrolls the view to a specified row index.
        /// </summary>
        /// <param name="rowIndex">Index of the row to be made as the first item in the grid.</param>
        public void SetVisibleRow(int rowIndex)
        {
            this.scrollToRowIndex = rowIndex;
        }

        /// <summary>
        /// Selects a row in the grid.
        /// </summary>
        /// <param name="rowNumber">Row number in the grid.</param>
        public void SelectRow(int rowNumber)
        {
            this.forceSelect = true;

            // TODO: Check whether there is a actual routedeventargs to be passed here
            this.pageOperationInvoked = true;
            this.HandleSelect(rowNumber, SelectionSource.None, new RoutedEventArgs());
            this.pageOperationInvoked = false;
            this.forceSelect = false;
        }

        /// <summary>
        /// Adds a sort indicator to the specified column.
        /// </summary>
        /// <param name="columnIndex">Column number to add the sort indicator.</param>
        /// <param name="sortDirection">Direction of sorting.</param>
        public void AddSortIndicator(int columnIndex, SortDirection sortDirection)
        {
#if !SILVERLIGHT
            if (this.mainView == null)
            {
                return;
            }
#endif
            FrameworkElement sortIndicator = null;

            if (sortDirection == SortDirection.Ascending)
            {
                if (null == this.ascendingOrderIndicatorDataTemplate)
                {
                    return;
                }

                sortIndicator = DataTemplateHelper.LoadContent(this.ascendingOrderIndicatorDataTemplate) as FrameworkElement;
            }
            else
            {
                if (null == this.descendingOrderIndicatorDataTemplate)
                {
                    return;
                }

                sortIndicator = DataTemplateHelper.LoadContent(this.descendingOrderIndicatorDataTemplate) as FrameworkElement;
            }

            if (null != sortIndicator)
            {
                sortIndicator.SetValue(FrameworkElement.NameProperty, "SortIndicator");
            }

            if (columnIndex > 0)
            {
                this.WrapDataGridColumns[columnIndex - 1].PresentSortDirection = sortDirection;
                if (this.WrapDataGridColumns[columnIndex - 1].IsVisible)
                {
                    this.mainView.View.Columns[columnIndex].ColumnHeader.Children.Add(sortIndicator);
                    this.WrapDataGridColumns[columnIndex - 1].SortIndicatorShownOnSort = true;
                }
                else
                {
                    this.WrapDataGridColumns[columnIndex - 1].SortIndicatorShownOnSort = false;
                }
            }
        }

        /// <summary>
        /// Clears the sort indicators on all columns.
        /// </summary>
        public void ClearSortIndicators()
        {
            FrameworkElement sortingElement = null;

            foreach (ColumnManager manager in this.mainView.View.Columns)
            {
                sortingElement = null;
                foreach (FrameworkElement element in manager.ColumnHeader.Children)
                {
                    if (element.Name == "SortIndicator")
                    {
                        sortingElement = element;
                        break;
                    }
                }

                if (null != sortingElement)
                {
                    manager.ColumnHeader.Children.Remove(sortingElement);
                }
            }
        }

        /// <summary>
        /// Adds a given child element to the Wrapdatagrid children collection.
        /// </summary>
        /// <param name="element">Element to be added to Wrap data grid.</param>
        public void AddChild(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            this.layoutRoot.Children.Add(element);
        }

        /// <summary>
        /// Removes a given child element from the Wrapdatagrid children collection.
        /// </summary>
        /// <param name="element">Element to be removed from Wrap data grid.</param>
        public void RemoveChild(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            this.layoutRoot.Children.Remove(element);
        }

        /// <summary>
        /// Indicates whether the given element exists in the Wrap data grid children collection.
        /// </summary>
        /// <param name="element">Element to be searched within Wrap data grid children collection.</param>
        /// <returns>True if the element exists otherwise false.</returns>
        public bool ContainsChild(UIElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

#if !SILVERLIGHT
            // HACK-  as the ContainsChild is called before layout root is constructed
            if (this.layoutRoot == null)
            {
                return false;
            }
#endif
            return this.layoutRoot.Children.Contains(element);
        }

        /// <summary>
        /// Public method to enable external callers as well as internal callers to explicitly load up the WrapDataGrid control.
        /// </summary>
        public void LoadUpControl()
        {
            this.gridIsBeingLoaded = true; //// Start loading up the Grid
            this.provider = (this.DataContext as IEnumerable);
            this.layoutRoot = this.Content as Grid;
            this.scrollBar = this.ScrollBar;

            this.CreateGridColumns();
            this.CreateColumns();
            this.CreateLookAheadColumns();
            this.CreateLookBehindColumns();

            this.mainView.InitializeView(this.layoutRoot, this.groupingDataTemplateLogic, this.groupingDataTemplatePresentation, this.mainViewColumns);

            this.mainView.View.OnColumnHeaderClick += new EventHandler<EventArgs>(this.ColumnHeaderClick);
            this.mainView.View.SelectionMode = this.SelectionMode;
            this.mainView.View.HostingWrapDataGrid = this;

            this.lookAheadView.InitializeView(this.layoutRoot, this.lookAheadColumn);

            this.lookAheadView.View.HostingWrapDataGrid = this;

            this.lookBehindView.InitializeView(this.layoutRoot, this.lookBehindColumn);

            this.lookBehindView.View.HostingWrapDataGrid = this;
            this.lookBehindView.View.Columns[0].StackPanel.SizeChanged += new SizeChangedEventHandler(this.LookBehind_SizeChanged);
            this.lookAheadView.View.Columns[0].StackPanel.SizeChanged += new SizeChangedEventHandler(this.LookAhead_SizeChanged);

            this.RenderData();

            if (this.mainView.View.Rows.Count > 0)
            {
                // Generate the summary panels.
                this.lookBehindView.View.UpdateSummary(this.mainView.View.Rows[0].DataContext, 0, true);
                this.lookAheadView.View.UpdateSummary(this.mainView.View.Rows[0].DataContext, 0, true);
            }

#if !SILVERLIGHT

            //// ADDED an explicit call here to set up the "grouping stackpanel" sizes etc, 
            //// since it doesnt run on the first call due to scrollbar etc. not being initialised
           this.SetSizeForFirstColumn();
#endif

            this.mainView.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);

            this.mainView.View.OnGroupingRender += new EventHandler<EventArgs>(this.MainView_OnGroupingRender);
            this.mainView.View.OnGroupHeaderClick += new EventHandler<EventArgs>(this.MainView_OnGroupHeaderClick);

            this.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
            this.MouseLeftButtonDown += new MouseButtonEventHandler(this.OnMouseLeftButtonDown);
#if SILVERLIGHT
            this.KeyDown += new KeyEventHandler(this.OnKeyDown);
            this.IsTabStop = true;
#else
            this.PreviewKeyDown += new KeyEventHandler(this.OnKeyDown);
            this.Focusable = true;
#endif
            this.SetVisibilityOnLoad();

            this.LoadScrollAnimationControl();
            this.LoadGenericAnimationControl();
            this.EnsureRowVisible(0);
            this.gridIsBeingLoaded = false; //// Loading up of Grid ends
        }

        /// <summary>
        /// Show animation.
        /// </summary>
        /// <param name="show">
        /// Boolean flag.
        /// </param>
        public void ShowAnimation(bool show)
        {
            if (show)
            {
                this.genericAnimation.Visibility = Visibility.Visible;
            }
            else
            {
                this.genericAnimation.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Updates the layout of the Grid.
        /// </summary>
        public new void UpdateLayout()
        {
            base.UpdateLayout();
            this.UpdateSizesAndRefresh();
        }

        /// <summary>
        /// Used to handle parent's key events inside this grid.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Key event args for the key event.</param>
        public void HandleParentKeyEvent(object sender, KeyEventArgs e)
        {
            this.OnKeyDown(sender, e);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Toggles the group expanded or collapsed state.
        /// </summary>
        /// <param name="groupKey">Group key of the group to toggle the state.</param>
        public void ToggleGroupState(string groupKey)
        {
            bool nextGroupBlank = false;
            int nextGroupFirstRowIndex = 0;

            this.MainView.ToggleGroupState(groupKey, out nextGroupBlank, out nextGroupFirstRowIndex);

            // load the next group's rows below this group, so that no whitespace can be seen in the grid below the currently collapsed group
            if (nextGroupBlank)
            {
                this.LoadNextBatchOfRows(nextGroupFirstRowIndex, true, true);
            }

            //// Fire Measure grid to check that LA/LB and mainview data is complete and in order
            this.RefreshViews(this.MeasureGrid(this.mainView.View.FirstVisibleItemIndex));
            this.CheckAndRemoveExtraItemsFromViews();
            this.UpdateLookAheadBar();
        }

        /// <summary>
        /// Sets the Min Width of a Column to a specified value.
        /// </summary>
        /// <param name="columnIndex">Column Index.</param>
        /// <param name="width">Value for width. (0 to reset the MinWidth).</param>
        public void SetMinWidth(int columnIndex, double width)
        {
            checked
            {
                if (columnIndex + 1 < this.layoutRoot.ColumnDefinitions.Count)
                {
                    if (!this.WrapDataGridColumns[columnIndex].IsVisible && this.hiddenColumnMinWidthCollection.ContainsKey(columnIndex + 1))
                    {
                        this.hiddenColumnMinWidthCollection[columnIndex + 1] = width;
                    }
                    else
                    {
                        this.layoutRoot.ColumnDefinitions[columnIndex + 1].MinWidth = width;
                    }
                }
            }

            this.UpdateLookAheadBar();
            this.CheckAndRemoveExtraItemsFromViews();
        }

        /// <summary>
        /// Sets the Max Width of a Column to a specified value.
        /// </summary>
        /// <param name="columnIndex">Column Index.</param>
        /// <param name="width">Value for width (double.MaxValue to reset the MaxWidth).</param>
        public void SetMaxWidth(int columnIndex, double width)
        {
            checked
            {
                if (columnIndex + 1 < this.layoutRoot.ColumnDefinitions.Count)
                {
                    if (!this.WrapDataGridColumns[columnIndex].IsVisible && this.hiddenColumnMaxWidthCollection.ContainsKey(columnIndex + 1))
                    {
                        this.hiddenColumnMaxWidthCollection[columnIndex + 1] = width;
                    }
                    else
                    {
                        this.layoutRoot.ColumnDefinitions[columnIndex + 1].MaxWidth = width;
                    }
                }
            }

            this.UpdateLookAheadBar();
            this.CheckAndRemoveExtraItemsFromViews();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Ensures the selected items are displayed selected.
        /// </summary>
        internal void EnsureSelections()
        {
            foreach (DataBoundRow currentRow in this.SelectedItems)
            {
                currentRow.Highlight(false);
                currentRow.Select(true);                
            }
        }

        /// <summary>
        /// Clears the selected items which are currently displayed selected.
        /// </summary>
        internal void ClearSelections()
        {
            foreach (DataBoundRow currentRow in this.SelectedItems)
            {
                currentRow.Select(false);
                currentRow.Highlight(false);
                currentRow.Select(false);
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>True</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (null != this.timer)
                {
                    this.Dispose(true);
                    this.timer.Dispose();
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Called when the row background property changes.
        /// </summary>
        /// <param name="d">The WrapDataGrid.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnRowBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WrapDataGrid wrapDataGrid = d as WrapDataGrid;
            WrapDataGrid.ApplyRowBackgrounds(wrapDataGrid);
        }

        /// <summary>
        /// Called when the alternating row background property changes.
        /// </summary>
        /// <param name="d">The WrapDataGrid.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnAlternatingRowBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WrapDataGrid wrapDataGrid = d as WrapDataGrid;
            WrapDataGrid.ApplyRowBackgrounds(wrapDataGrid);
        }

        /// <summary>
        /// Called when the selected row background property changes.
        /// </summary>
        /// <param name="d">The WrapDataGrid.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnSelectionBackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WrapDataGrid wrapDataGrid = d as WrapDataGrid;
            WrapDataGrid.ApplyRowBackgrounds(wrapDataGrid);
        }

        /// <summary>
        /// Applies the row backgrounds.
        /// </summary>
        /// <param name="wrapDataGrid">The WrapDataGrid instance.</param>
        private static void ApplyRowBackgrounds(WrapDataGrid wrapDataGrid)
        {
            if (wrapDataGrid != null && wrapDataGrid.MainView != null)
            {
                // Go through the rows and update the background 
                for (int childIndex = 0; childIndex < wrapDataGrid.MainView.Rows.Count; childIndex++)
                {
                    DataBoundRow row = wrapDataGrid.MainView.Rows[childIndex] as DataBoundRow;
                    row.EnsureBackground();
                }
            }
        }

        /// <summary>
        /// Applies the row backgrounds for a specified set of rows.
        /// </summary>
        /// <param name="wrapDataGrid">The WrapDataGrid instance.</param>
        /// <param name="startRowIndex">Start row index.</param>
        /// <param name="endRowIndex">End row index.</param>
        private static void ApplyRowBackgrounds(WrapDataGrid wrapDataGrid, int startRowIndex, int endRowIndex)
        {
            if (wrapDataGrid != null && wrapDataGrid.MainView != null)
            {
                // Go through the rows and update the background 
                for (int childIndex = startRowIndex; childIndex <= endRowIndex; childIndex++)
                {
                    if (childIndex < wrapDataGrid.MainView.Rows.Count)
                    {
                        DataBoundRow row = wrapDataGrid.MainView.Rows[childIndex] as DataBoundRow;
                        row.EnsureBackground();
                    }
                }
            }
        }

        /// <summary>
        /// Validate Cell Template.
        /// </summary>
        /// <param name="cellTemplate">
        /// Column Cell Template.
        /// </param>
        private static void CheckCellTemplate(DataTemplate cellTemplate)
        {
            if (cellTemplate == null)
            {
                throw new ArgumentNullException("cellTemplate");
            }
        }

        /// <summary>
        /// Validate Header Template.
        /// </summary>
        /// <param name="headerTemplate">
        /// Column Header Template.
        /// </param>
        private static void CheckHeaderTemplate(DataTemplate headerTemplate)
        {
            if (headerTemplate == null)
            {
                throw new ArgumentNullException("headerTemplate");
            }
        }

        /// <summary>
        /// NumberOfCols changed callback.
        /// </summary>
        /// <param name="targetDependencyObject">The target dependency object.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void NumberOfColsChanged(DependencyObject targetDependencyObject, DependencyPropertyChangedEventArgs e)
        {
            WrapDataGrid currentWrapDataGrid = (WrapDataGrid)targetDependencyObject;
            currentWrapDataGrid.SetValue(NumberOfColsProperty, currentWrapDataGrid.GetValue(NumberOfColsProperty));
        }

        /// <summary>
        /// LookAheadVisibility changed callback.
        /// </summary>
        /// <param name="targetDependencyObject">The target dependency object.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void ToggleLookAheadVisibility(DependencyObject targetDependencyObject, DependencyPropertyChangedEventArgs e)
        {
            WrapDataGrid currentWrapDataGrid = (WrapDataGrid)targetDependencyObject;
            if (currentWrapDataGrid != null && currentWrapDataGrid.lookAheadView != null)
            {                
                currentWrapDataGrid.UpdateLookAheadBar();
            }
        }

        /// <summary>
        /// LookBehindVisibility changed callback.
        /// </summary>
        /// <param name="targetDependencyObject">The target dependency object.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void ToggleLookBehindVisibility(DependencyObject targetDependencyObject, DependencyPropertyChangedEventArgs e)
        {
            WrapDataGrid currentWrapDataGrid = (WrapDataGrid)targetDependencyObject;
            if (currentWrapDataGrid != null && currentWrapDataGrid.lookBehindView != null)
            {
                 currentWrapDataGrid.UpdateLookBehindBar();
            }
        }

        /// <summary>
        /// Finds the data cell under a mouse hit.
        /// </summary>
        /// <param name="hitSource">The mouse hit source.</param>
        /// <returns>The data cell Grid object.</returns>
        private static Grid FindDataCell(object hitSource)
        {
            // TODO: Refactor this method to be a bit more efficient/reliable - GMM
            FrameworkElement hitFrameworkElement = hitSource as FrameworkElement;

            if (hitFrameworkElement == null)
            {
                return null;
            }

            if (hitFrameworkElement.Tag != null && hitFrameworkElement.Tag.ToString().StartsWith("dataCell", StringComparison.OrdinalIgnoreCase) == true)
            {
                return hitFrameworkElement as Grid;
            }

            StackPanel hitStackPanel = hitSource as StackPanel;
            if (hitStackPanel != null && hitStackPanel.Children[0] as Grid != null)
            {
                Grid hitGrid = hitStackPanel.Children[0] as Grid;
                if (hitGrid.Tag != null && hitGrid.Tag.ToString().StartsWith("dataCell", StringComparison.OrdinalIgnoreCase) == true)
                {
                    return hitGrid;
                }
            }

            FrameworkElement parentElement = hitFrameworkElement.Parent as FrameworkElement;

            while (parentElement != null)
            {
                if (parentElement.Tag != null && parentElement.Tag.ToString().StartsWith("dataCell", StringComparison.OrdinalIgnoreCase) == true && parentElement.GetType() == typeof(Grid))
                {
                    return parentElement as Grid;
                }
                else
                {
                    parentElement = (FrameworkElement)parentElement.Parent;
                }
            }

            return null;
        }

        /// <summary>
        /// Finds the parent summary cell.
        /// </summary>
        /// <param name="hitSource">Object clicked.</param>
        /// <returns>The parent summary cell.</returns>
        private static Grid FindSummaryCell(object hitSource)
        {
            FrameworkElement hitFrameworkElement = hitSource as FrameworkElement;

            if (hitFrameworkElement == null)
            {
                return null;
            }

            FrameworkElement parentElement = hitFrameworkElement.Parent as FrameworkElement;

            while (parentElement != null)
            {
                if (parentElement.Tag != null && parentElement.Tag.ToString().StartsWith("SummaryRowIndex", StringComparison.OrdinalIgnoreCase) == true && parentElement.GetType() == typeof(Grid))
                {
                    return parentElement as Grid;
                }
                else
                {
                    parentElement = (FrameworkElement)parentElement.Parent;
                }
            }

            return null;
        }

        /// <summary>
        /// Creates a GridSplitter as the default resizer for the grid.
        /// </summary>
        /// <returns>Grid splitter.</returns>
        private GridSplitter GetDefaultResizer()
        {
            GridSplitter resizerSplitter = new GridSplitter();
            resizerSplitter.SetValue(Canvas.ZIndexProperty, 5);
            resizerSplitter.Height = 20;
            resizerSplitter.HorizontalAlignment = HorizontalAlignment.Right;
            resizerSplitter.Width = 2;
            resizerSplitter.Background = new SolidColorBrush(Colors.Gray);
            resizerSplitter.Margin = new Thickness(2, 0, 2, 0);
            resizerSplitter.IsTabStop = false;
            resizerSplitter.MouseLeftButtonUp += new MouseButtonEventHandler(this.ResizerSplitter_MouseLeftButtonUp);
            return resizerSplitter;
        }

        /// <summary>
        /// Set Column and Header visibilities on load.
        /// </summary>
        private void SetVisibilityOnLoad()
        {
            for (int count = 0; count < this.WrapDataGridColumns.Count; count++)
            {
                if (!this.WrapDataGridColumns[count].IsVisible)
                {
                    this.ShowColumn(count, false);
                }
            }

            if (!this.ColumnHeadersVisible)
            {
                this.ShowColumnHeaders(false);
            }
        }

        /// <summary>
        /// Called when the mouse left button is released on the grid splitters.
        /// </summary>
        /// <param name="sender">The Splitter.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void ResizerSplitter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ////Column headers geeting an opacity of 0 on columns being resized to 0 using splitters. A possible bug in Silverlight that column elements get an opacitiy of 0 when the column definition has a width of 0 and visibility of collapsed.
            for (int columncount = 0; columncount < this.WrapDataGridColumns.Count; columncount++)
            {
                if (this.WrapDataGridColumns[columncount].IsVisible)
                {
                    for (int childcount = 0; childcount < this.MainView.Columns[columncount + 1].ColumnHeader.Children.Count; childcount++)
                    {
                        this.MainView.Columns[columncount + 1].ColumnHeader.Children[childcount].Visibility = Visibility.Visible;
                        this.MainView.Columns[columncount + 1].ColumnHeader.Children[childcount].Opacity = 1;
                    }
                }
            }
        }

        /// <summary>
        /// When the size changes handler.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event arguments.</param>
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.PreviousSize != new Size(0, 0))
            {
                this.UpdateSizesAndRefresh();
            }
            else
            {
                this.UpdateGroupingColumnSize();
            }
        }

        /// <summary>
        /// Grid has focus.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event arguments.</param>
        private void WrapDataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            this.HandleTabKeyPress();
        }

        /// <summary>
        /// Grid looses focus.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event arguments.</param>
        private void WrapDataGrid_LostFocus(object sender, RoutedEventArgs e)
        {            
            this.HandleLostFocus();
        }

        /// <summary>
        /// Hides the scrollbar if all columns are hidden.
        /// </summary>
        private void HideScrollbar()
        {
            for (int columncount = 0; columncount < this.WrapDataGridColumns.Count; columncount++)
            {
                if (this.WrapDataGridColumns[columncount].IsVisible)
                {
                    if (this.layoutRoot.ColumnDefinitions[this.layoutRoot.ColumnDefinitions.Count - 1].Width.Value == 0)
                    {
                        this.layoutRoot.ColumnDefinitions[this.layoutRoot.ColumnDefinitions.Count - 1].Width = new GridLength(this.scrollBar.Width);
                    }

                    this.AllColumnsHidden = false;
                    return;
                }
            }

            this.layoutRoot.ColumnDefinitions[this.layoutRoot.ColumnDefinitions.Count - 1].Width = new GridLength(0);
            this.AllColumnsHidden = true;
        }

        /// <summary>
        /// Hides the grouping column if all columns are hidden.
        /// </summary>
        private void ToggleGroupingColumnVisibility()
        {
            for (int columncount = 0; columncount < this.WrapDataGridColumns.Count; columncount++)
            {
                if (this.WrapDataGridColumns[columncount].IsVisible)
                {
                    this.mainView.View.Columns[0].StackPanel.Visibility = Visibility.Visible;
                    this.mainView.View.Columns[0].ColumnHeader.Visibility = Visibility.Visible;
                    this.layoutRoot.ColumnDefinitions[0].Width = new GridLength(0);
                    if (this.lookAheadElement != null)
                    {
                        this.lookAheadElement.Visibility = Visibility.Visible;
                    }

                    if (this.lookBehindElement != null)
                    {
                        this.lookBehindElement.Visibility = Visibility.Visible;
                    }

                    return;
                }
            }

            this.mainView.View.Columns[0].StackPanel.Visibility = Visibility.Collapsed;
            this.mainView.View.Columns[0].ColumnHeader.Visibility = Visibility.Collapsed;
            this.layoutRoot.ColumnDefinitions[0].Width = new GridLength((this.Content as Grid).ActualWidth - (this.scrollBar.Parent as Grid).ColumnDefinitions[Grid.GetColumn(this.scrollBar)].ActualWidth);
            if (this.lookAheadElement != null)
            {
                this.lookAheadElement.Visibility = Visibility.Collapsed;
            }

            if (this.lookBehindElement != null)
            {
                this.lookBehindElement.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Onloaded event handler.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event arguments.</param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //// Do not load up the control automatically for the WPF version since it would load the WrapdataGrid after the 
            //// hosting page is loaded in that case. WPF methods should call the public "LoadUpControl" explictly.
#if SILVERLIGHT
            this.LoadUpControl();
#endif
        }

        /// <summary>
        /// Capture mouse down.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event arguments.</param>
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // e.Handled will be true in case of group header click.
            if (e.Handled == false)
            {
#if SILVERLIGHT
                Grid gridElement = FindDataCell(e.OriginalSource);
#else
                Grid gridElement = FindDataCell(e.Device.Target);
#endif

                if (gridElement != null)
                {
                    this.Focus();
                    string[] elementTagString = gridElement.Tag.ToString().Split('=');
                    string[] rowColString = elementTagString[1].Split(',');
                    int rowNumber = int.Parse(rowColString[0], System.Globalization.CultureInfo.CurrentCulture);
                    if (rowNumber >= 0 && this.MainView.Rows[0].RowFocus)
                    {
                        this.MainView.Rows[0].Highlight(false);
                        this.MainView.Rows[0].RowFocus = false;
                        this.MainView.Rows[0].RowSelected = false;
                    }

                    this.HandleSelect(rowNumber, SelectionSource.Mouse, e);
                }
                else
                {
#if SILVERLIGHT
                    gridElement = FindSummaryCell(e.OriginalSource);
#else
                    gridElement = FindSummaryCell(e.Device.Target);
#endif

                    if (gridElement != null)
                    {
                        string[] elementTagString = gridElement.Tag.ToString().Split('=');
                        int rowNumber = int.Parse(elementTagString[1], System.Globalization.CultureInfo.CurrentCulture);
                        this.SelectedIndex = rowNumber;
                        this.EnsureRowVisible(rowNumber);                        
                    }
                }
            }
        }

        /// <summary>
        /// Handles the select/de-select of a row.
        /// </summary>
        /// <param name="rowNumber">The row number.</param>
        /// <param name="selectionSource">Selection source.</param>
        /// <param name="e">Routed event arguments.</param>
        private void HandleSelect(int rowNumber, SelectionSource selectionSource, RoutedEventArgs e)
        {
            bool controlKeyDown;
            bool shiftKeyDown;          

            if (this.mainView.View.Rows.Count < 1 || rowNumber >= this.mainView.View.Rows.Count || rowNumber < 0)
            {
                return;
            }

            DataBoundRow hitRow = this.mainView.View.Rows[rowNumber];
            List<DataBoundRow> removedItems = new List<DataBoundRow>();
            List<DataBoundRow> addedItems = new List<DataBoundRow>();
            removedItems = new List<DataBoundRow>();
            addedItems = new List<DataBoundRow>();

            KeyboardHelper.GetMetaKeyState(out controlKeyDown, out shiftKeyDown);

            this.selectedIndex = rowNumber;
            if (shiftKeyDown == false)
            {
                this.AnchorRowIndex = rowNumber;
            }

            bool multiSelectAllowed = (this.SelectionMode == SelectionMode.MultipleExtended || this.SelectionMode == SelectionMode.MultipleSimple);

            // Multi-select?
            if ((controlKeyDown && multiSelectAllowed && selectionSource != SelectionSource.Keyboard) || (this.forceSelect && multiSelectAllowed))
            {
                if (this.spacebarKeyDown)
                {
                    return;
                }

                //// De-select via Control Key and mouse
                if (this.SelectedItems.Contains(hitRow) == true)
                {
                    removedItems.Add(hitRow);                    
                    this.SelectedItems.Remove(hitRow);                                    
                    this.SelectedItem = this.SelectedItems.Count > 0 ? this.SelectedItems[this.SelectedItems.Count - 1] : null;
                    hitRow.Select(false);
                    hitRow.Highlight(true);  
                    hitRow.RowFocus = true;
                    hitRow.RowSelected = false;
                }
                else
                {
                    //// Select via Control Key and mouse
                    addedItems.Add(hitRow);
                    this.SelectedItems.Add(hitRow);
                    this.SelectedItem = hitRow;
                    hitRow.RowFocus = true;
                    hitRow.RowSelected = false;
                }

                if (this.lastRowHavingFocusIndex != -1 && hitRow.Index != this.lastRowHavingFocusIndex)
                {
                    this.MainView.Rows[this.lastRowHavingFocusIndex].Highlight(false);
                }               
            }           
            else if (controlKeyDown && multiSelectAllowed && selectionSource == SelectionSource.Keyboard)
            {
                hitRow.Highlight(true);
                hitRow.RowFocus = true;
                this.handlingControlMultiSelection = true;
                if (this.spacebarKeyDown)
                {
                    //// Select via Control Key and space-bar
                    if (this.SelectedItems.IndexOf(hitRow) == -1)
                    {
                        this.SelectedItems.Add(hitRow);
                        hitRow.Highlight(false);
                        hitRow.Select(true);                       
                        hitRow.RowSelected = true;
                        hitRow.RowFocus = true;
                    }
                    else
                    {
                        //// De-select via Control Key and space-bar
                        this.SelectedItems.Remove(hitRow);
                        hitRow.Select(false);
                        hitRow.Highlight(true);
                        hitRow.RowSelected = false;
                        hitRow.RowFocus = true;
                    }
                }
                else if (this.SelectedItems.IndexOf(hitRow) != -1)
                {
                    hitRow.Select(true);
                    hitRow.RowFocus = true;
                }
               
                if (this.lastHitRow != null)
                {
                    if (this.lastHitRow != hitRow)
                    {
                        this.lastHitRow.Highlight(false);
                        this.lastHitRow.RowFocus = false;
                    }

                    if (this.SelectedItems.IndexOf(this.lastHitRow) != -1)
                    {
                        this.lastHitRow.Select(true);
                    }
                }

                this.lastHitRow = hitRow;
            }
            else if ((shiftKeyDown && multiSelectAllowed))
            {
                //// Handle shift based selected
                int startRow = this.anchorRowIndex <= rowNumber ? this.anchorRowIndex : rowNumber;
                int endRow = this.anchorRowIndex <= rowNumber ? rowNumber : this.anchorRowIndex;
                DataBoundRow unselectingRow;
               
                // Items before selection range...
                for (int beforeRowIndex = 0; beforeRowIndex < startRow; beforeRowIndex++)
                {
                    unselectingRow = this.mainView.View.Rows[beforeRowIndex];
                    if (this.SelectedItems.Contains(unselectingRow) == true)
                    {                        
                        this.SelectedItems.Remove(unselectingRow);
                        removedItems.Add(unselectingRow);
                        unselectingRow.Select(false);
                        unselectingRow.RowSelected = false;
                        unselectingRow.RowFocus = false;
                    }
                }

                // Items in selection range...
                for (int selectRowIndex = startRow; selectRowIndex <= endRow; selectRowIndex++)
                {
                    DataBoundRow selectingRow = this.mainView.View.Rows[selectRowIndex];
                    if (this.SelectedItems.Contains(selectingRow) == false)
                    {
                        this.SelectedItems.Add(selectingRow);
                        addedItems.Add(selectingRow);
                        selectingRow.RowSelected = true;
                        selectingRow.RowFocus = false;
                    }

                    this.SelectedItem = selectingRow;
                }

                // Items after selection range...
                for (int afterRowIndex = endRow + 1; afterRowIndex < this.mainView.View.Rows.Count - 1; afterRowIndex++)
                {
                    unselectingRow = this.mainView.View.Rows[afterRowIndex];
                    if (this.SelectedItems.Contains(unselectingRow) == true)
                    {                        
                        this.SelectedItems.Remove(unselectingRow);
                        removedItems.Add(unselectingRow);
                        unselectingRow.Select(false);
                        unselectingRow.RowSelected = false;
                        unselectingRow.RowFocus = false;
                    }
                }

                if (e.GetType() == typeof(System.Windows.Input.KeyEventArgs))
                {
                    if (endRow + 2 == this.mainView.View.Rows.Count && ((KeyEventArgs)e).Key == Key.Up)
                    {
                        unselectingRow = this.mainView.View.Rows[endRow + 1];
                        if (this.SelectedItems.Contains(unselectingRow) == true)
                        {                           
                            this.SelectedItems.Remove(unselectingRow);
                            removedItems.Add(unselectingRow);
                            unselectingRow.Select(false);
                            unselectingRow.RowSelected = false;
                            unselectingRow.RowFocus = false;
                        }
                    }
                }
                else if (e.GetType() == typeof(System.Windows.Input.MouseButtonEventArgs))
                {
                    if (endRow + 1 != this.mainView.View.Rows.Count)
                    {
                        unselectingRow = this.mainView.View.Rows[this.mainView.View.Rows.Count - 1];
                        if (this.SelectedItems.Contains(unselectingRow) == true)
                        {                           
                            this.SelectedItems.Remove(unselectingRow);
                            removedItems.Add(unselectingRow);
                            unselectingRow.Select(false);
                            unselectingRow.RowSelected = false;
                            unselectingRow.RowFocus = false;
                        }
                    }
                }
            }            
            else
            {
                if (this.lastHitRow != null)
                {
                    if (this.lastHitRow != hitRow)
                    {
                        this.lastHitRow.Highlight(false);
                        this.lastHitRow.RowSelected = false;
                        this.lastHitRow.RowFocus = false;
                    }
                }

                this.ClearSelections();
                removedItems.AddRange(this.selectedItems);
                this.SelectedItems.Clear();
                removedItems.Remove(hitRow);
                addedItems.Add(hitRow);
                this.SelectedItems.Add(hitRow);
                this.SelectedItem = hitRow;
                hitRow.RowFocus = true;
                hitRow.RowSelected = true;
            }

            if (!this.handlingControlMultiSelection)
            {
                this.EnsureSelections();
            }

            this.spacebarKeyDown = false;
            this.handlingControlMultiSelection = false;
            this.EnsureRowVisible(rowNumber);
            this.lastRowHavingFocusIndex = rowNumber;          
            this.HandleSelectedRowCollection();
            if (this.OnSelectionChanged != null)
            {
#if SILVERLIGHT
                SelectionChangedEventArgs newSelectionChangedEventArgs = new SelectionChangedEventArgs(removedItems, addedItems);
#else
                // TODO: Check whether this implementation is correct
                SelectionChangedEventArgs newSelectionChangedEventArgs = new SelectionChangedEventArgs(e.RoutedEvent, removedItems, addedItems);
#endif
                this.OnSelectionChanged(this, newSelectionChangedEventArgs);
            }
        }

        /// <summary>
        /// Handle other selected rows.
        /// </summary>       
        private void HandleSelectedRowCollection()
        {
            foreach (DataBoundRow row in this.SelectedItems)
            {
                if (row.Index != this.lastRowHavingFocusIndex)
                {
                    row.Select(false);
                    row.Highlight(false);
                    row.SelectCollectionRow(true);
                    row.RowSelected = true;
                    row.RowFocus = false;
                }
                else
                {
                    row.RowSelected = true;
                    row.RowFocus = true;
                }
            }            
        }

        /// <summary>
        /// On Key Down.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event arguments.</param>
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (this.MainView.Rows.Count == 0)
            {
                return;
            }
            else if (e.OriginalSource.GetType() == typeof(System.Windows.Controls.Button))
            {
                Button hitButton = e.OriginalSource as Button;
                if (!string.IsNullOrEmpty(hitButton.Name) && hitButton.Name == WrapDataGridResources.GroupButtonName)
                {
                    if (e.Key == Key.Tab) 
                    {
                        return;
                    }                    
                }
                else
                {
                    hitButton.Focus();
                }
            }

            int newIndex = -1;
            bool firstRowHighlighted = this.MainView.Rows[0].RowFocus;
           
            switch (e.Key)
            {
                case Key.Up:
                    newIndex = this.SelectedIndex;
                    if (this.SelectedIndex > 0)
                    {
                        newIndex -= 1;
                    }
                    else if (this.SelectedIndex == -1 && !firstRowHighlighted)
                    {
                        newIndex = 0;
                    }
                    else
                    {
                        e.Handled = true;
                        break;
                    }

                    this.pageOperationInvoked = true;
                    this.HandleSelect(newIndex, SelectionSource.Keyboard, e);
                    this.selectedIndex = newIndex;
                    this.pageOperationInvoked = false;
                    e.Handled = true;
                    break;
                case Key.Down:
                    if (firstRowHighlighted)
                    {
                        this.RemoveFirstRowFocus();
                        this.HandleSelect(1, SelectionSource.Keyboard, e);
                        this.selectedIndex = 1;                        
                    }
                    else
                    {
                        newIndex = this.SelectedIndex;
                        if (newIndex == this.MainView.Rows.Count - 1)
                        {
                            e.Handled = true;
                            break;
                        }
                        else if (newIndex == this.MainView.LastVisibleItemIndex - 1 && this.lastRowOccluded)
                        {
                            int pageIndex = this.mainView.View.LastVisibleItemIndex;
                            this.HandleSelect(pageIndex, SelectionSource.Keyboard, e);
                            this.selectedIndex = pageIndex;                           
                        }
                        else if (newIndex == this.MainView.LastVisibleItemIndex)
                        {
                            int pageIndex = this.mainView.View.LastVisibleItemIndex + 1;
                            this.HandleSelect(pageIndex, SelectionSource.Keyboard, e);
                            this.selectedIndex = pageIndex;
                            this.EnsureScroll(pageIndex);
                        }
                        else
                        {
                            newIndex += 1;
                            this.HandleSelect(newIndex, SelectionSource.Keyboard, e);
                            this.selectedIndex = newIndex;
                        }
                    }

                    e.Handled = true;
                    break;
                case Key.PageUp:
                    //// If first visible item index is preceded by a collapsed group, make that group visible.                   
                    newIndex = this.MainView.FirstVisibleItemIndex;
                    if (firstRowHighlighted)
                    {
                        this.RemoveFirstRowFocus();
                    }

                    this.HandleSelect(newIndex, SelectionSource.Keyboard, e);
                    this.selectedIndex = newIndex;
                    if (this.SelectedIndex == this.MainView.Rows.Count - 1)
                    {
                        this.EnsureScroll(newIndex + 1 < this.MainView.Rows.Count ? newIndex + 1 : newIndex);
                    }
                    else
                    {
                        this.EnsureScroll(newIndex);
                    }

                    e.Handled = true;
                    break;
                case Key.PageDown:
                    if (firstRowHighlighted)
                    {
                        this.RemoveFirstRowFocus();
                    }

                    newIndex = this.lastRowOccluded ? this.MainView.LastVisibleItemIndex : this.MainView.LastVisibleItemIndex + 1;
                    this.pageOperationInvoked = true;
                    int pagingIndex = newIndex < this.MainView.Rows.Count ? newIndex : newIndex - 1;
                    this.HandleSelect(pagingIndex, SelectionSource.Keyboard, e);
                    this.pageOperationInvoked = false;
                    this.selectedIndex = newIndex;
                    e.Handled = true;
                    break;
                case Key.Home:
                    newIndex = 0;
                    if (firstRowHighlighted)
                    {
                        this.RemoveFirstRowFocus();
                    }

                    this.HandleSelect(newIndex, SelectionSource.Keyboard, e);
                    this.selectedIndex = newIndex;
                    e.Handled = true;
                    break;
                case Key.End:
                    newIndex = this.mainView.View.Rows.Count - 1;
                    if (firstRowHighlighted)
                    {
                        this.RemoveFirstRowFocus();
                    }

                    this.EnsureRowVisible(this.MainView.Rows.Count - 1);
                    this.HandleSelect(newIndex, SelectionSource.Keyboard, e);
                    this.selectedIndex = newIndex;
                    e.Handled = true;
                    break;
                case Key.Space:
                    if (this.SelectedIndex >= 0)
                    {
                        newIndex = this.SelectedIndex;
                        this.spacebarKeyDown = true;
                        this.HandleSelect(newIndex, SelectionSource.Keyboard, e);
                    }
                    else if (firstRowHighlighted)
                    {
                        this.HandleSelect(0, SelectionSource.Keyboard, e);                        
                    }

#if !SILVERLIGHT   
                         e.Handled = true;                    
#endif
                    break;
                case Key.Tab:                    
                    e.Handled = this.HandleTabKeyPress();
                    break;
            }

            this.UpdateLookAheadBar();
            this.UpdateLookBehindBar();
            if (null != this.OnKeyPress)
            {
                this.OnKeyPress(this, e);
            }
        }

        /// <summary>
        /// Handles the TAB key press on the control.
        /// </summary>
        /// <returns>Boolean indicating whether the keypress is handled.</returns>
        private bool HandleTabKeyPress()
        {
            if (this.MainView.Rows.Count > 0)
            {
                bool firstRowHighlighted = this.MainView.Rows[0].RowFocus;
                bool handled = false;

                if (this.SelectedIndex == -1 && !firstRowHighlighted)
                {
                    this.MainView.Rows[0].Highlight(true);
                    this.MainView.Rows[0].RowSelected = false;
                    this.MainView.Rows[0].RowFocus = true;
                    this.EnsureRowVisible(0);
                    handled = true;
                }
                else if (this.SelectedItems.Count != 0 && this.lastRowHavingFocusIndex >= 0 && this.MainView.Rows.Count > this.lastRowHavingFocusIndex && !this.MainView.Rows[this.lastRowHavingFocusIndex].RowFocus && this.MainView.Rows[this.lastRowHavingFocusIndex].RowSelected)
                {
                    this.MainView.Rows[this.lastRowHavingFocusIndex].Select(true);
                    this.MainView.Rows[this.lastRowHavingFocusIndex].RowSelected = true;
                    this.MainView.Rows[this.lastRowHavingFocusIndex].RowFocus = true;
                    this.EnsureRowVisible(this.lastRowHavingFocusIndex);
                    handled = true;
                }
                else if (this.SelectedItems.Count != 0 && this.lastRowHavingFocusIndex >= 0 && this.MainView.Rows.Count > this.lastRowHavingFocusIndex && this.MainView.Rows[this.lastRowHavingFocusIndex].RowSelected && this.MainView.Rows[this.lastRowHavingFocusIndex].RowFocus)
                {
                    this.MainView.Rows[this.lastRowHavingFocusIndex].Select(false);
                    this.MainView.Rows[this.lastRowHavingFocusIndex].SelectCollectionRow(true);
                    this.MainView.Rows[this.lastRowHavingFocusIndex].RowSelected = true;
                    this.MainView.Rows[this.lastRowHavingFocusIndex].RowFocus = false;
                }
                else if (this.SelectedItems.Count != 0 && this.lastRowHavingFocusIndex >= 0 && this.MainView.Rows.Count > this.lastRowHavingFocusIndex && !this.MainView.Rows[this.lastRowHavingFocusIndex].RowSelected && this.MainView.Rows[this.lastRowHavingFocusIndex].RowFocus)
                {
                    this.MainView.Rows[this.lastRowHavingFocusIndex].Highlight(false);
                    this.MainView.Rows[this.lastRowHavingFocusIndex].RowSelected = false;
                    this.MainView.Rows[this.lastRowHavingFocusIndex].RowFocus = false;
                }
                else if (this.lastRowHavingFocusIndex != -1)
                {
                    if (this.MainView.Rows[this.lastRowHavingFocusIndex].RowSelected)
                    {
                        this.MainView.Rows[this.lastRowHavingFocusIndex].Highlight(true);
                        this.MainView.Rows[this.lastRowHavingFocusIndex].RowSelected = false;
                        this.MainView.Rows[this.lastRowHavingFocusIndex].RowFocus = true;
                        this.EnsureRowVisible(this.lastRowHavingFocusIndex);
                        handled = true;
                    }
                    else if (this.SelectedItems.Count > 0)
                    {
                        int rowIndex = this.SelectedItems[0].Index;
                        this.MainView.Rows[rowIndex].SelectCollectionRow(false);
                        this.MainView.Rows[rowIndex].Highlight(false);
                        this.MainView.Rows[rowIndex].Select(true);
                        this.MainView.Rows[rowIndex].RowSelected = true;
                        this.MainView.Rows[rowIndex].RowFocus = true;
                        this.SelectedItem = this.MainView.Rows[rowIndex];
                        this.lastRowHavingFocusIndex = rowIndex;
                        this.SelectedIndex = rowIndex;
                        this.EnsureRowVisible(this.lastRowHavingFocusIndex);
                        handled = true;
                    }
                }

                return handled;
            }

            return false;
        }

        /// <summary>
        /// Removes focus from first row.
        /// </summary>
        private void RemoveFirstRowFocus()
        {
            this.MainView.Rows[0].Highlight(false);
            this.MainView.Rows[0].RowSelected = false;
            this.MainView.Rows[0].RowFocus = false;
        }       

        /// <summary>
        /// Ensure scrolling inside the main view.
        /// </summary>
        /// <param name="pageIndex">Possible start index of the next set of rows.</param>
        private void EnsureScroll(int pageIndex)
        {
            int newIndex = pageIndex >= this.MainView.Rows.Count ? this.MainView.Rows.Count - 1 : this.InverseMeasureGrid(pageIndex);
            this.RefreshViews(this.MeasureGrid(newIndex));
            this.LoadNextBatchOfRows(this.MainView.FirstVisibleItemIndex, true, true);
        }

        /// <summary>
        /// Finds the no. of collapsed rows.
        /// </summary>
        /// <param name="startIndex">Start index of rows collection..</param>        
        /// <returns>Returns the no of collapsed rows.</returns>
        private int GetHiddenRows(int startIndex)
        {           
            Collection<DataBoundRow> currentGroup = this.mainView.View.GetGroupedRows(this.MainView.Rows[startIndex].Grouping.GroupingText);
            if (currentGroup[currentGroup.Count - 1].Index >= this.MainView.Rows.Count - 1)
            {
                return this.GetUpHiddenRows(startIndex);
            }
            else
            {
                return currentGroup[currentGroup.Count - 1].Index + 1;
            }
        }

        /// <summary>
        /// Finds the no. of collapsed rows.
        /// </summary>
        /// <param name="startIndex">Start index of rows collection.</param>
        /// <returns>Returns the no of collapsed rows.</returns>
        private int GetUpHiddenRows(int startIndex)
        {
            Collection<DataBoundRow> currentGroup = this.mainView.View.GetGroupedRows(this.MainView.Rows[startIndex].Grouping.GroupingText);
            return currentGroup[0].Index;
        }

        /// <summary>
        /// Scrollbar scroll event handler.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event arguments.</param>
        private void ScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if (this.handlingScrollEvent)
            {
                return;
            }
            else if (e.ScrollEventType == ScrollEventType.LargeDecrement || e.ScrollEventType == ScrollEventType.LargeIncrement)
            {
                this.handlingScrollEvent = true;
#if SILVERLIGHT
                this.scrollTimer = new DispatcherTimer();
                this.scrollTimer.Interval = TimeSpan.FromSeconds(0.2);
                this.scrollTimer.Tick += (e.ScrollEventType == ScrollEventType.LargeDecrement) ? new EventHandler(this.HandlePageScrollUp) : new EventHandler(this.HandlePageScrollDown);
                this.scrollTimer.Start();
#else
                if (e.ScrollEventType == ScrollEventType.LargeDecrement)
                {
                    this.HandlePageScrollUp(sender, e);                   
                }
                else
                {
                    this.HandlePageScrollDown(sender, e);
                }
#endif
            }
            else
            {
                int index = (int)e.NewValue;
                if (this.MainView.Rows.Count > 0 && this.MainView.LastVisibleItemIndex == this.MainView.Rows.Count - 2 && e.NewValue >= this.ScrollBar.Value && this.MainView.Rows[this.MainView.Rows.Count - 1].GroupingHeader != null && index >= this.lastScrollIndex)
                {
                    index = (index + 1) < this.MainView.Rows.Count ? index + 1 : index;
                }

                DateTime currentScrollTime = DateTime.Now;

                TimeSpan timeBetweenScrolls = currentScrollTime - this.lastScrollTime;

                //// Check for scroll being fired multiple times for same index
                if (e.ScrollEventType != ScrollEventType.ThumbTrack && e.ScrollEventType != ScrollEventType.EndScroll)
                {
                    if (index == this.lastScrollIndex && timeBetweenScrolls.Seconds <= 0)
                    {
                        //// Dont do further processing if the same index has been fired within under a second
                        return;
                    }
                }

                this.lastScrollTime = currentScrollTime;
                this.lastScrollIndex = index;

                //// Start display of wait animation if the thumb is being dragged
                if (e.ScrollEventType == ScrollEventType.ThumbTrack)
                {
                    if (!this.scrollWaitAnimationIsBeingDisplayed)
                    {
                        this.scrollAnimation.Visibility = Visibility.Visible;
                        this.scrollWaitAnimationIsBeingDisplayed = true;
                    }
                }

                //// Start scroll based calculations etc.
                if (e.ScrollEventType == ScrollEventType.EndScroll || e.ScrollEventType == ScrollEventType.SmallIncrement || e.ScrollEventType == ScrollEventType.SmallDecrement
                            || e.ScrollEventType == ScrollEventType.LargeIncrement || e.ScrollEventType == ScrollEventType.LargeDecrement)
                {
                    this.LoadNextBatchOfRows(index, true, true);

                   //// int firstIndex = this.mainView.View.FirstVisibleItemIndex;

                    if (index >= 0 && index < this.MainView.FirstVisibleItemIndex && this.MainView.Rows[index].Grouping != null && this.MainView.Rows[index].RowCollapsed && this.MainView.Rows[index].GroupingHeader == null)
                    {
                        index = this.GetUpHiddenRows(index);
                    }
                    else if (index > this.MainView.FirstVisibleItemIndex && index < this.MainView.Rows.Count && this.MainView.Rows[index].Grouping != null && this.MainView.Rows[index].RowCollapsed && this.MainView.Rows[index].GroupingHeader == null)
                    {
                        index = this.GetHiddenRows(index);
                    }

                    this.RefreshViews(this.MeasureGrid(index));

                    this.CheckAndRemoveExtraItemsFromViews();

                    this.UpdateLookAheadBar(); //// Look ahead bar should be updated after all MeasureGrid and MeasureViews calculations
                    this.UpdateLookBehindBar();
                }
                
                //// End the display of wait animation when the thumb is released
                if (this.scrollWaitAnimationIsBeingDisplayed && e.ScrollEventType == ScrollEventType.EndScroll)
                {
                    this.scrollAnimation.Visibility = Visibility.Collapsed;
                    this.scrollWaitAnimationIsBeingDisplayed = false;
                }
            }
        }

        /// <summary>
        /// Handles Paging Up using scrollbar.
        /// </summary>
        /// <param name="sender">
        /// Sender of the event.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void HandlePageScrollUp(object sender, EventArgs e)
        {
            int index = 0;
            //// Increase the interval so that multiple scrolls do not fire.
#if SILVERLIGHT
            this.scrollTimer.Interval = TimeSpan.FromDays(1);
#endif
            index = this.MainView.FirstVisibleItemIndex;
            if (index == this.MainView.Rows.Count - 1)
            {
                this.EnsureScroll(index + 1 < this.MainView.Rows.Count ? index + 1 : index);
            }           
            else
            {
                this.EnsureScroll(index);
            }

            this.UpdateLookBehindBar();
#if !SILVERLIGHT
            this.UpdateLookAheadBar();
            this.CheckAndRemoveExtraItemsFromViews();
#endif
            this.handlingScrollEvent = false;
        }

        /// <summary>
        /// Handles Paging Down using scrollbar.
        /// </summary>
        /// <param name="sender">
        /// Sender of the event.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private void HandlePageScrollDown(object sender, EventArgs e)
        {
            int index = 0;
            //// Increase the interval so that multiple scrolls do not fire.
#if SILVERLIGHT
            this.scrollTimer.Interval = TimeSpan.FromDays(1);
#endif
            index = this.lastRowOccluded ? this.MainView.LastVisibleItemIndex : this.MainView.LastVisibleItemIndex + 1;
            this.pageOperationInvoked = true;
            int pagingIndex = index < this.MainView.Rows.Count ? index : index - 1;
            this.EnsureRowVisible(pagingIndex);
            this.pageOperationInvoked = false;
            this.UpdateLookBehindBar();
#if !SILVERLIGHT
            this.UpdateLookAheadBar();
            this.CheckAndRemoveExtraItemsFromViews();
#endif
            this.handlingScrollEvent = false;
        } 

        /// <summary>
        /// Fix the grid if a blank page is displayed.
        /// </summary>
        /// <param name="index">
        /// Index of the first row of the collapsed group.
        /// </param>
        private void FixBlankPage(int index)
        {
            int rowIndex = this.mainView.View.GetGroupedRows(this.mainView.View.Rows[index].Grouping.GroupingText)[0].Index;
            this.EnsureRowVisible(rowIndex);           
            this.RefreshViews(this.MeasureGrid(rowIndex));
            this.CheckAndRemoveExtraItemsFromViews();
            this.UpdateLookAheadBar();
            this.UpdateLookBehindBar();
        }

        /// <summary>
        /// Remove extra items from Look ahead scrollbar (if any).
        /// </summary>
        private void CheckAndRemoveExtraItemsFromViews()
        {
            if (this.lookAheadView.View.Rows.Count > 0)
            {
                //// Look ahead bar is "right-to-left" - so reverse the indexes
                int firstVisibleIndex = this.lookAheadView.View.Rows.Count - 1 - this.mainView.View.FirstVisibleItemIndex;
                int lastVisibleIndex = this.lookAheadView.View.Rows.Count - 1 - this.mainView.View.LastVisibleItemIndex;

                if (firstVisibleIndex < 0 || lastVisibleIndex < 0)
                {
                    //// Safety check
                    return;
                }

                for (int loopCounter = firstVisibleIndex; loopCounter >= lastVisibleIndex; loopCounter--)
                {
                    if (loopCounter != lastVisibleIndex && this.lookAheadView.View.Columns[0].StackPanel.Children[loopCounter].Visibility == Visibility.Visible)
                    {
                        this.lookAheadView.View.Columns[0].StackPanel.Children[loopCounter].Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        /// <summary>
        /// Look ahead size changed event handler.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event arguments.</param>
        private void LookAhead_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateLookAheadBar();
            this.CheckIfLookAheadDataIsSufficient(this.currentRowIndex);            
        }

        /// <summary>
        /// Sets up the width of the Look ahead bar and also calculates the value for Look ahead's summary.
        /// </summary>
        private void UpdateLookAheadBar()
        {
            ColumnManager column = this.lookAheadView.View.Columns[0];
            StackPanel mainPanel = this.lookAheadView.View.Columns[0].SummaryManager.MainPanel;
            FrameworkElement parentElement = (null == column.GridLayout) ? (FrameworkElement)column.StackPanel : (FrameworkElement)column.GridLayout;
            StackPanel currentElement = (null == column.GridLayout) ? (StackPanel)column.StackPanel : (StackPanel)column.GridLayout.Children[0];

            double summaryMinWidth = 85d;
            double gridWidth = parentElement.ActualWidth;

            this.lookAheadView.View.VisibleItemCount = 0;
            this.lookAheadElement = parentElement;

            if (this.checkLastRowOccluded && this.lastRowGapFromBottom < currentElement.ActualHeight)
            {
                this.lastRowOccluded = true;
                this.HandleLastRowOcclusionCase();
            }

            parentElement.Margin = new Thickness(0, -1 * currentElement.ActualHeight, 0, 0);
            if (this.mainView.View.LastVisibleItemIndex <= 0)
            {
                return;
            }
            else if (currentElement.ActualWidth >= gridWidth && gridWidth != 0 && currentElement.Children.Count > 0)
            {
                mainPanel.Visibility = Visibility.Visible;

                double childWidth = 0;
                int optimizer = 0;
                int i;
                double clippedWidth = 0d;

                if (this.mainView.View.LastVisibleItemIndex > 0 && this.mainView.View.LastVisibleItemIndex < this.mainView.View.Rows.Count)
                {
                    optimizer = this.mainView.View.LastVisibleItemIndex;
                }

                for (i = (currentElement.Children.Count - 1) - optimizer; i >= 0; i--)
                {
                    clippedWidth = childWidth;

                    if (currentElement.Children[i].Visibility != Visibility.Collapsed)
                    {
                        DataBoundRow currentLARow = this.lookAheadView.View.Rows[this.lookAheadView.View.Rows.Count - 1 - i];
                        if (!currentLARow.IsBlank && ((FrameworkElement)currentElement.Children[i]).ActualWidth <= 0)
                        {
                            ((FrameworkElement)currentElement.Children[i]).UpdateLayout();
                        }  
                      
                        childWidth += ((FrameworkElement)currentElement.Children[i]).ActualWidth;
                        this.lookAheadView.View.VisibleItemCount++;
                    }

                    if (childWidth + summaryMinWidth >= gridWidth)
                    {
                        break;
                    }
                }

                // count -1 is to specify correct index. 
                // i + 1 is to make sure that the item partially hidden is included in the count.
                int rowIndex = i < currentElement.Children.Count - 1 ? (i + 1) : i;

                this.lookAheadView.View.UpdateSummary(this.mainView.View.Rows[currentElement.Children.Count - 1 - rowIndex].DataContext, currentElement.Children.Count - 1 - rowIndex, false);
                mainPanel.Width = gridWidth - clippedWidth;
            }
            else
            {
                mainPanel.Visibility = Visibility.Collapsed;
            }

            if (this.LookAheadVisibility != Visibility.Visible)
            {
                currentElement.Visibility = Visibility.Collapsed;
                mainPanel.Visibility = Visibility.Collapsed;  
            }
            else
            {
                currentElement.Visibility = Visibility.Visible;                   
            }
        }

        /// <summary>
        /// Sets up the width of the Look behind bar and also calculates the value for Look behind's summary.
        /// </summary>
        private void UpdateLookBehindBar()
        {
            ColumnManager column = this.lookBehindView.View.Columns[0];
            StackPanel mainPanel = this.lookBehindView.View.Columns[0].SummaryManager.MainPanel;
            mainPanel.Visibility = Visibility.Collapsed;
            if (this.mainView.View.FirstVisibleItemIndex <= 0)
            {
                //// No need to show the look behind if the grid is displaying the first row.               
                this.CheckAndClearLookBehindBar();
                return;
            }

            FrameworkElement parentElement = (null == column.GridLayout) ? (FrameworkElement)column.StackPanel : (FrameworkElement)column.GridLayout;
            StackPanel currentElement = (null == column.GridLayout) ? (StackPanel)column.StackPanel : (StackPanel)column.GridLayout.Children[0];

            double summaryMinWidth = 85d;
            double gridWidth = parentElement.ActualWidth;

            this.lookBehindElement = parentElement;

            parentElement.Margin = new Thickness(0, 0, 0, -1 * currentElement.ActualHeight);
            currentElement.UpdateLayout();
            if (currentElement.ActualWidth >= gridWidth && gridWidth != 0 && currentElement.Children.Count > 0)
            {
                mainPanel.Visibility = Visibility.Visible;

                double childWidth = 0;
                int optimizer;
                int i;
                double clippedWidth = 0d;

                ////Hide all rows from FirstVisibleItemIndex to LastVisibleItemIndex
                for (int count = this.mainView.View.FirstVisibleItemIndex + 1; count <= this.mainView.View.LastVisibleItemIndex; count++)
                {
                    if (count < currentElement.Children.Count)
                    {
                        currentElement.Children[count].Visibility = Visibility.Collapsed;
                    }
                }

                if (this.mainView.View.FirstVisibleItemIndex > 0 && this.mainView.View.FirstVisibleItemIndex < this.mainView.View.Rows.Count)
                {  
                    optimizer = this.mainView.View.FirstVisibleItemIndex;
                }
                else
                {
                    optimizer = currentElement.Children.Count - 1;
                }

                for (i = optimizer; i >= 0; i--)
                {
                    clippedWidth = childWidth;
                    if (currentElement.Children[i].Visibility == Visibility.Visible)
                    {
                        if (((FrameworkElement)currentElement.Children[i]).ActualWidth <= 0)
                        {
                            ((FrameworkElement)currentElement.Children[i]).UpdateLayout();
                        }

                        childWidth += ((FrameworkElement)currentElement.Children[i]).ActualWidth;
                    }

                    if (childWidth + summaryMinWidth >= gridWidth)
                    {
                        break;
                    }
                }

                int rowIndex = i < currentElement.Children.Count - 1 ? (i + 1) : i;
                this.lookBehindView.View.UpdateSummary(this.mainView.View.Rows[rowIndex].DataContext, rowIndex, true);
                mainPanel.Width = gridWidth - clippedWidth;
            }
            else
            {
                mainPanel.Visibility = Visibility.Collapsed;
            }

            if (this.LookBehindVisibility != Visibility.Visible)
            {
                currentElement.Visibility = Visibility.Collapsed;
                mainPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                currentElement.Visibility = Visibility.Visible;
            }
        }       

        /// <summary>
        /// Look behind size changed event handler.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event arguments.</param>
        private void LookBehind_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        }

        /// <summary>
        /// Surfaces the OnColumnClick event of the View control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Cui.Controls.ColumnHeaderClickEventArgs"/> instance containing the event data.</param>
        private void ColumnHeaderClick(object sender, EventArgs e)
        {
            ColumnHeaderClickEventArgs args = e as ColumnHeaderClickEventArgs;
            if (null != this.OnColumnHeaderClick && !(args.ColumnIndex == this.WrapDataGridColumns.Count && !this.WrapDataGridColumns[this.WrapDataGridColumns.Count - 1].IsVisible))
            {
                this.OnColumnHeaderClick(this, args);
            }
        }

        /// <summary>
        /// Forcibly collapses all items within the look behind bar.
        /// </summary>
        private void CheckAndClearLookBehindBar()
        {
            if (this.lookBehindView.View.Columns[0].StackPanel.Children.Count <= 0)
            {
                return;
            }

            foreach (UIElement currentElement in this.lookBehindView.View.Columns[0].StackPanel.Children)
            {
                if (currentElement.Visibility != Visibility.Collapsed)
                {
                    currentElement.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Surfaces the SelectionChanged event of the View control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void SelectionChanged(object sender, EventArgs e)
        {
            SelectionChangedEventArgs args = e as SelectionChangedEventArgs;
            if (null != this.OnSelectionChanged)
            {
                this.OnSelectionChanged(this, args);
            }
        }

        /// <summary>
        /// Ensures the row is visible.
        /// </summary>
        /// <param name="rowIndex">Index of the row.</param>
        private void EnsureRowVisible(int rowIndex)
        {
            if (this.mainView.View.Rows.Count == 0)
            {
                return;
            }

            // Need to scroll only if the item clicked is from LookAhead/LookBehind            
            if (rowIndex <= this.mainView.View.FirstVisibleItemIndex || rowIndex >= this.mainView.View.LastVisibleItemIndex)
            {
                DataBoundRow currentRow = this.mainView.View.Rows[rowIndex];
                if (currentRow.RowCollapsed && currentRow.Grouping != null && !String.IsNullOrEmpty(currentRow.Grouping.GroupingText))
                {   
                    rowIndex = this.mainView.View.GetGroupedRows(currentRow.Grouping.GroupingText)[0].Index;
                }

                // Make it as the second row, so that it is not occluded.
                if (this.pageOperationInvoked || rowIndex <= this.mainView.View.FirstVisibleItemIndex)
                {
                    rowIndex = rowIndex > 0 ? rowIndex - 1 : rowIndex;
                }
                else if (rowIndex != 0)
                {
                    rowIndex = this.InverseMeasureGrid(rowIndex);
                }

                this.LoadNextBatchOfRows(rowIndex, true, true);
                int startIndex = this.MeasureGrid(rowIndex);
                this.RefreshViews(startIndex);
                this.UpdateLookAheadBar();
                this.UpdateLookBehindBar();
            }
            else
            {
                this.UpdateLookAheadBar();
                this.UpdateLookBehindBar();
            }
        }

        /// <summary>
        /// Checks if any of the rows in the next batch of rows is blank. If any such rows are found, it loads data for those rows.
        /// </summary>
        /// <param name="rowIndex">Index of the row from where the check has to be started.</param>
        /// <param name="loadLookBehindRows">True, if the Look behind rows also need to be loaded.</param>
        /// <param name="loadMainViewRows">True, if main view rows also have to be loaded.</param>
        private void LoadNextBatchOfRows(int rowIndex, bool loadLookBehindRows, bool loadMainViewRows)
        {
            // Virtualization code START---------------------------------------
            if (rowIndex < this.mainView.View.Rows.Count)
            {
                ////load next batch of data

                int batchStartPosition = rowIndex;  //// Start Position(Main View)
                int batchEndPosition = rowIndex + 15; //// End Postion (Main View)

                int lookBehindStartPosition = (rowIndex - 15 >= 0) ? rowIndex - 15 : 0; //// negative positions not allowed
                int lookBehindEndPosition = rowIndex;

                int lookAheadStartPosition = rowIndex;
                int lookAheadEndPosition = (rowIndex + 25 < this.lookAheadView.View.Rows.Count) ? rowIndex + 25 : this.lookAheadView.View.Rows.Count - 1; //// adding check for max position(Max position cannot be greater than row count), although the loop can never reach the incorrect max value

                //// Below - Same Loop is used here for all 3 views to maintain a good speed of execution
                //// Loop starts from lowest value(LB start position) and goes upto the highest value (LA end position)
                for (int loopCounter = lookBehindStartPosition; loopCounter <= lookAheadEndPosition; loopCounter++)
                {
                    if (loadMainViewRows)
                    {
                        if (loopCounter >= batchStartPosition && loopCounter <= batchEndPosition)
                        {
                            //// If row is blank and also not part of a collapsed group, then load it's data
                            if (this.mainView.View.Rows[loopCounter].IsBlank
                                && (!this.mainView.View.Rows[loopCounter].RowCollapsed))
                            {
                                this.mainView.View.FillBlankRowWithData(loopCounter);

                                if (this.mainView.View.Rows[loopCounter].RowHeight <= 0 && !this.mainView.View.Rows[loopCounter].RowCollapsed)
                                {
                                    this.mainView.View.Rows[loopCounter].UpdateLayout();
                                }
                            }
                        }
                    }

                    if (loadLookBehindRows)
                    {
                        if (loopCounter >= lookBehindStartPosition && loopCounter <= lookBehindEndPosition)
                        {
                            if (this.lookBehindView.View.Rows[loopCounter].IsBlank)
                            {
                                this.lookBehindView.View.FillBlankRowWithData(loopCounter);
                            }
                        }
                    }

                    if (loopCounter >= lookAheadStartPosition && loopCounter <= lookAheadEndPosition)
                    {
                        if (this.lookAheadView.View.Rows[loopCounter].IsBlank)
                        {
                            this.lookAheadView.View.FillBlankRowWithData(loopCounter);
                        }
                    }
                }

                WrapDataGrid.ApplyRowBackgrounds(this, batchStartPosition, batchEndPosition);
                this.mainView.View.AddRowsComplete(); //// Group the last group
                this.mainView.View.UpdateGroupCounts(); //// update group counts

                this.UpdateLookAheadBar(); //// update LA and check if the data is sufficient for correct LA display
                this.CheckIfLookAheadDataIsSufficient(batchEndPosition + 1);
            }
            //// ----------------END
        }

        /// <summary>
        /// Checks if any of the rows in the next few rows is blank. If any such rows are found, it loads data for those rows.
        /// </summary>
        /// <param name="rowIndex">Index of the row from where the check has to be started.</param>
        /// <param name="loadLookBehindRows">True, if the Look behind rows also need to be loaded.</param>
        /// <param name="loadMainViewRows">True, if main view rows also have to be loaded.</param>
        private void LoadNextMinimalSetOfRows(int rowIndex, bool loadLookBehindRows, bool loadMainViewRows)
        {
            // Virtualization code START---------------------------------------
            if (rowIndex < this.mainView.View.Rows.Count)
            {
                ////load next batch of data

                int batchStartPosition = rowIndex;  //// Start Position(Main View)
                int batchEndPosition = rowIndex + 5; //// End Postion (Main View)

                int lookBehindStartPosition = (rowIndex - 5 >= 0) ? rowIndex - 5 : 0; //// negative positions not allowed
                int lookBehindEndPosition = rowIndex;

                int lookAheadStartPosition = rowIndex;
                int lookAheadEndPosition = (rowIndex + 5 < this.lookAheadView.View.Rows.Count) ? rowIndex + 5 : this.lookAheadView.View.Rows.Count - 1; //// adding check for max position(Max position cannot be greater than row count), although the loop can never reach the incorrect max value

                //// Below - Same Loop is used here for all 3 views to maintain a good speed of execution
                //// Loop starts from lowest value(LB start position) and goes upto the highest value (LA end position)
                for (int loopCounter = lookBehindStartPosition; loopCounter <= lookAheadEndPosition; loopCounter++)
                {
                    if (loadMainViewRows)
                    {
                        if (loopCounter >= batchStartPosition && loopCounter <= batchEndPosition)
                        {
                            //// If row is blank and also not part of a collapsed group, then load it's data
                            if (this.mainView.View.Rows[loopCounter].IsBlank
                                && (!this.mainView.View.Rows[loopCounter].RowCollapsed))
                            {
                                this.mainView.View.FillBlankRowWithData(loopCounter);

                                if (this.mainView.View.Rows[loopCounter].RowHeight <= 0 && !this.mainView.View.Rows[loopCounter].RowCollapsed)
                                {
                                    this.mainView.View.Rows[loopCounter].UpdateLayout();
                                }
                            }
                        }
                    }

                    if (loadLookBehindRows)
                    {
                        if (loopCounter >= lookBehindStartPosition && loopCounter <= lookBehindEndPosition)
                        {
                            if (this.lookBehindView.View.Rows[loopCounter].IsBlank)
                            {
                                this.lookBehindView.View.FillBlankRowWithData(loopCounter);
                            }
                        }
                    }

                    if (loopCounter >= lookAheadStartPosition && loopCounter <= lookAheadEndPosition)
                    {
                        if (this.lookAheadView.View.Rows[loopCounter].IsBlank)
                        {
                            this.lookAheadView.View.FillBlankRowWithData(loopCounter);
                        }
                    }
                }

                WrapDataGrid.ApplyRowBackgrounds(this, batchStartPosition, batchEndPosition);
                this.mainView.View.AddRowsComplete(); //// Group the last group
                this.mainView.View.UpdateGroupCounts(); //// update group counts

                this.UpdateLookAheadBar(); //// update LA and check if the data is sufficient for correct LA display
                this.CheckIfLookAheadDataIsSufficient(batchEndPosition + 1);
            }
            //// ----------------END
        }

        /// <summary>
        /// Checks the LA bar and adds more rows if required to the Look-ahead view.
        /// </summary>
        /// <param name="newBatchStartPosition">The start position for the fresh batch that should be loaded, if need be.</param>
        private void CheckIfLookAheadDataIsSufficient(int newBatchStartPosition)
        {
            StackPanel mainPanel = this.lookAheadView.View.Columns[0].SummaryManager.MainPanel;
            FrameworkElement lookAheadChild = (FrameworkElement)this.lookAheadView.View.Columns[0].GridLayout.Children[0];

            if (this.lookAheadView.View.Rows.Count > 0)
            {
                //// If the last lookahead row's data has not been loaded yet and the summary panel is empty
                //// run the "loadNext.." function to check that all rows are loaded.
                if (mainPanel.Visibility == Visibility.Collapsed && this.lookAheadView.View.Rows[this.lookAheadView.View.Rows.Count - 1].IsBlank)
                {
                    if (lookAheadChild.ActualWidth <= this.ActualWidth)
                    {
                        if (this.previousWidthForLookAhead == lookAheadChild.ActualWidth && this.numberOfTimesFreshBatchWasLoaded >= 2)
                        {
                            this.numberOfTimesFreshBatchWasLoaded = 0;
                            this.CheckAndRemoveExtraItemsFromViews();
                            return;
                        }

                        this.previousWidthForLookAhead = lookAheadChild.ActualWidth;
                        this.numberOfTimesFreshBatchWasLoaded++;
                        this.LoadNextBatchOfRows(newBatchStartPosition, false, true);
                    }
                }
            }
        }

        /// <summary>
        /// Measures the rows in the grid in an inverse manner.
        /// </summary>
        /// <param name="startOffset">Start offset.</param>       
        /// <returns>Feasible start index depending upon the grid height.</returns>        
        private int InverseMeasureGrid(int startOffset)
        {
            if (this.mainView.View.Columns.Count == 0)
            {
                return startOffset;
            }

            int feasibleStartOffset;
            int beginIndex = startOffset;
            int aboveRowIndex;
            ColumnManager specimenColumn = this.mainView.View.Columns[1];
            Grid specimenColumnGrid = specimenColumn.StackPanel.Parent as Grid;
            RowDefinition specimenMainViewRow = specimenColumnGrid.RowDefinitions[MainViewRowIndex];
            ColumnManager lookAheadColumn = this.lookAheadView.View.Columns[0];
            StackPanel lookAheadElement = (null == lookAheadColumn.GridLayout) ? (StackPanel)lookAheadColumn.StackPanel : (StackPanel)lookAheadColumn.GridLayout.Children[0];
            ColumnManager lookBehindColumn = this.lookBehindView.View.Columns[0];
            StackPanel lookBehindElement = (null == lookBehindColumn.GridLayout) ? (StackPanel)lookBehindColumn.StackPanel : (StackPanel)lookBehindColumn.GridLayout.Children[0];

            StackPanel currentElement = (lookAheadElement.ActualHeight > 0) ? lookAheadElement : lookBehindElement;

            if (this.MainView.Rows[startOffset].RowHeight == 0)
            {
                this.mainView.View.FillBlankRowWithData(startOffset);
                WrapDataGrid.ApplyRowBackgrounds(this, startOffset, startOffset);
                this.MainView.Rows[startOffset].UpdateLayout();
            }

            double lastVisibleRowTop = this.MainView.Rows[startOffset].RowHeight + currentElement.ActualHeight;
            int aboveRow = 0;

            if (null == specimenMainViewRow)
            {
                throw new System.ArgumentException("Row must be a GridRow");
            }

            this.mainView.View.VisibleItemCount = 0;

            //// calculate the feasible start offset by making the given row the second last in the current page.
            if (beginIndex > 0 && lastVisibleRowTop < specimenMainViewRow.ActualHeight)
            {
                for (aboveRowIndex = beginIndex - 1; aboveRowIndex >= 0; aboveRowIndex--)
                {
                    DataBoundRow row = this.mainView.View.Rows[aboveRowIndex];

                    if (row.IsBlank && !row.RowCollapsed)
                    {
                        this.mainView.View.FillBlankRowWithData(aboveRowIndex);
                        WrapDataGrid.ApplyRowBackgrounds(this, aboveRowIndex, aboveRowIndex);
                    }

                    if (!row.IsBlank && row.Visibility == Visibility.Visible && row.RowHeight <= 0)
                    {
                        //// The element is visible yet it's width is 0 - Check if it's layout has been updated properly
                        row.UpdateLayout();
                    }

                    if ((lastVisibleRowTop + row.RowHeight) <= specimenMainViewRow.ActualHeight)
                    {
                        aboveRow++;
                        this.mainView.View.VisibleItemCount++;
                        lastVisibleRowTop += row.RowHeight;
                    }
                    else
                    {
                        break;
                    }
                }

                feasibleStartOffset = startOffset - aboveRow < 0 ? 0 : startOffset - aboveRow;
            }
            else
            {
                feasibleStartOffset = startOffset;
            }

            if (this.MainView.Rows[feasibleStartOffset].Grouping != null && this.MainView.Rows[feasibleStartOffset].RowCollapsed && this.MainView.Rows[feasibleStartOffset].GroupingHeader == null)
            {
                Collection<DataBoundRow> currentGroup = this.mainView.View.GetGroupedRows(this.MainView.Rows[feasibleStartOffset].Grouping.GroupingText);
                return (currentGroup[currentGroup.Count - 1].Index + 1 < this.MainView.Rows.Count) ? currentGroup[currentGroup.Count - 1].Index + 1 : feasibleStartOffset;
            }
            else
            {
                return feasibleStartOffset;
            }
        }

        /// <summary>
        /// Measures the rows in the columns..
        /// </summary>
        /// <param name="startOffset">Start offset.</param>
        /// <returns>Feasible start index depending upon the grid height.</returns>
        private int MeasureGrid(int startOffset)
        {
            int feasibleStartOffset;
            int beginIndex = startOffset;
            this.currentRowIndex = startOffset; //// Set the value the class variable here, so that other functions can use it.

            if (this.mainView.View.Columns.Count == 0)
            {
                return startOffset;
            }

            double lastVisibleRowTop = 0;
          
            // Previously last row was occluded and hence displayed in lookahead. Remove it so that normal scrolling works.
            if (this.lastRowOccluded && this.mainView.View.Rows.Count > 0 && this.mainView.View.LastVisibleItemIndex >= 0 && this.mainView.View.LastVisibleItemIndex < this.mainView.View.Rows.Count)
            {
                this.lookAheadView.View.Columns[0].StackPanel.Children[(this.mainView.View.Rows.Count - 1) - this.mainView.View.LastVisibleItemIndex].Visibility = Visibility.Collapsed;
            }

            this.lastRowOccluded = false;
            this.checkLastRowOccluded = false;

            ColumnManager specimenColumn = this.mainView.View.Columns[1];
            Grid specimenColumnGrid = specimenColumn.StackPanel.Parent as Grid;

            // May need to find by name - i.e. Row3MainView
            RowDefinition specimenMainViewRow = specimenColumnGrid.RowDefinitions[MainViewRowIndex];
            if (null == specimenMainViewRow)
            {
                throw new System.ArgumentException("Row must be a GridRow");
            }

            this.mainView.View.VisibleItemCount = 0;

            int rowIndex;
            int indexOfFinalRow = beginIndex;
            bool stopLastVisibleRowTopCalculation = false;
            bool blankRowsExistInVisibleArea = false;
            double heightOfAllVisibleRows = 0;
            int lastRowWithLoadedData = 0;
            for (rowIndex = beginIndex; rowIndex < this.mainView.View.Rows.Count; rowIndex++)
            {
                DataBoundRow row = this.mainView.View.Rows[rowIndex];

                //// Added to use the same loop for the total visible row height calculation below
                if (!stopLastVisibleRowTopCalculation)
                {
                    if (row.IsBlank)
                    {
                         this.LoadNextMinimalSetOfRows(row.Index, true, true);
                    }

                    if (row.RowCollapsed && row.RowExpanded && row.GroupingHeader != null)
                    {
                         row.CollapseRow(true, false);
                    }

                    if ((lastVisibleRowTop + row.RowHeight) <= specimenMainViewRow.ActualHeight)
                    {
                        if (row.IsBlank)
                        {
                            blankRowsExistInVisibleArea = true;
                        }

                        this.mainView.View.VisibleItemCount++;
                        lastVisibleRowTop += row.RowHeight;
                    }
                    else
                    {
                        stopLastVisibleRowTopCalculation = true;
                    }

                    if (row.RowCollapsed && row.RowExpanded && row.GroupingHeader != null)
                    {
                        row.CollapseRow(false, false);
                    }

                    indexOfFinalRow = rowIndex;
                }

                //// Calculate height of all the data-loaded rows in the grid and also the index of the last data-loaded row
                if (!row.IsBlank)
                {
                    heightOfAllVisibleRows += row.RowHeight;
                    lastRowWithLoadedData = rowIndex;
                }
            }

            int aboveRowIndex;
            int aboveRow = 0;
            bool adjustedForWhiteSpace = false;

            //// there is some white space left in the grid. Need to include some above items.
            if (indexOfFinalRow == (this.mainView.View.Rows.Count - 1) && lastVisibleRowTop < specimenMainViewRow.ActualHeight)
            {
                for (aboveRowIndex = beginIndex - 1; aboveRowIndex >= 0; aboveRowIndex--)
                {
                    DataBoundRow row = this.mainView.View.Rows[aboveRowIndex];

                    if (row.IsBlank && !row.RowCollapsed)
                    {
                        this.mainView.View.FillBlankRowWithData(aboveRowIndex);
                        WrapDataGrid.ApplyRowBackgrounds(this, aboveRowIndex, aboveRowIndex);
                    }

                    if (!row.IsBlank && row.Visibility == Visibility.Visible && row.RowHeight <= 0)
                    {
                        //// The element is visible yet it's width is 0 - Check if it's layout has been updated properly
                        row.UpdateLayout();
                    }

                    if ((lastVisibleRowTop + row.RowHeight) <= specimenMainViewRow.ActualHeight)
                    {
                        aboveRow++;
                        this.mainView.View.VisibleItemCount++;
                        lastVisibleRowTop += row.RowHeight;
                    }
                    else
                    {
                        break;
                    }
                }

                if (aboveRow > 0)
                {
                    adjustedForWhiteSpace = true;
                }

                feasibleStartOffset = startOffset - aboveRow;
            }
            else
            {
                feasibleStartOffset = startOffset;               
            }

            // Current row is not the last row. It might be occluded by Look ahead scrollbar.
            if (startOffset + this.mainView.View.VisibleItemCount < this.mainView.View.Rows.Count - 1)
            {
                ColumnManager column = this.lookAheadView.View.Columns[0];
                StackPanel currentElement = (null == column.GridLayout) ? (StackPanel)column.StackPanel : (StackPanel)column.GridLayout.Children[0];
                this.lastRowGapFromBottom = specimenMainViewRow.ActualHeight - lastVisibleRowTop;

                if (this.lastRowGapFromBottom < currentElement.ActualHeight)
                {
                    this.lastRowOccluded = true;
                }
                else if (currentElement.ActualHeight == 0)
                {
                    this.checkLastRowOccluded = true;
                }
            }

            if (this.mainView.View.VisibleItemCount < this.mainView.View.Rows.Count && this.mainView.View.Rows.Count > 0)
            {
                this.scrollBar.Visibility = Visibility.Visible;
                int largeScrollStep = this.mainView.View.VisibleItemCount > 0 ? this.mainView.View.VisibleItemCount - 1 : 0;

                if (!blankRowsExistInVisibleArea && !adjustedForWhiteSpace)
                {
                    this.scrollBar.LargeChange = largeScrollStep;
                    this.scrollBar.Maximum = this.mainView.View.Rows.Count - this.mainView.View.VisibleItemCount;
                }

                this.scrollBar.Value = feasibleStartOffset;

                if (!this.viewportLoaded && this.scrollBar.ActualHeight > 0)
                {
                    this.heightSetForScrollbar = this.scrollBar.ActualHeight;
                    this.scrollBar.ViewportSize = this.scrollBar.ActualHeight / (this.scrollBar.Maximum - this.scrollBar.Minimum);
                    this.viewportLoaded = true;
                }
            }
            else
            {
                this.scrollBar.Value = 0;
                this.scrollBar.Visibility = Visibility.Collapsed;
            }

            int adjustmentVector = 0;
            for (int counter = feasibleStartOffset; counter < feasibleStartOffset + this.mainView.View.VisibleItemCount; counter++)
            {
                if (counter < this.MainView.Rows.Count)
                {
                    DataBoundRow objectRow = this.MainView.Rows[counter];
                    if (!objectRow.RowCollapsed && objectRow.RowHeight <= 0)
                    {
                        adjustmentVector += 1;
                    }
                }
            }

            this.CheckIfDataInMainViewIsSufficient(heightOfAllVisibleRows, lastRowWithLoadedData, blankRowsExistInVisibleArea); 
            return feasibleStartOffset;
        }

        /// <summary>
        /// Handles the display of last row for Look ahead/behind Scroll bars.
        /// </summary>
        private void HandleLastRowCase()
        {
            if (this.mainView.View.FirstVisibleItemIndex + this.mainView.View.VisibleItemCount < this.mainView.View.Rows.Count)
            {
                this.lookAheadView.View.Columns[0].StackPanel.Children[0].Visibility = Visibility.Visible;
            }

            this.HandleLastRowOcclusionCase();
        }

        /// <summary>
        /// Handles the display of last row in look ahead if it is occluded.
        /// </summary>
        private void HandleLastRowOcclusionCase()
        {
            if (this.lastRowOccluded && this.mainView.View.Rows.Count > 0 && this.mainView.View.LastVisibleItemIndex >= 0 && this.mainView.View.LastVisibleItemIndex < this.mainView.View.Rows.Count)
            {
                this.lookAheadView.View.Columns[0].StackPanel.Children[(this.mainView.View.Rows.Count - 1) - this.mainView.View.LastVisibleItemIndex].Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Arrange the items in the views from scratch.
        /// </summary>
        /// <param name="startOffset">Index of first visible item in the main view.</param>
        private void RefreshViews(int startOffset)
        {
            if (!this.gridIsBeingLoaded)
            {
                int rowCount = this.mainView.View.Rows.Count;

                this.mainView.View.FirstVisibleItemIndex = startOffset;
                if (this.mainView.View.VisibleItemCount > 0)
                {
                    this.mainView.View.LastVisibleItemIndex = startOffset + this.mainView.View.VisibleItemCount - 1;
                }
                else
                {
                    this.mainView.View.LastVisibleItemIndex = startOffset;
                }

                for (int i = 0; i < this.mainView.View.Rows.Count; i++)
                {
                    if (i < startOffset)
                    {
                        this.lookBehindView.View.Columns[0].StackPanel.Children[i].Visibility = Visibility.Visible;
                        this.lookBehindView.View.Columns[0].StackPanel.Children[i].UpdateLayout();
                        if (!this.mainView.View.Rows[i].IsBlank)
                        {
 #if SILVERLIGHT
                            if (this.mainView.View.Rows[i].RowCollapsed && this.mainView.View.Rows[i].GroupingHeader != null && !this.mainView.View.Rows[i].RowExpanded)
                            {
                                this.mainView.View.Rows[i].CollapseRow(false, false);
                                this.mainView.View.Rows[i].RowExpanded = true;
                            }
#endif

                            foreach (ColumnManager currentColumn in this.mainView.View.Columns)
                            {
                                currentColumn.StackPanel.Children[i].Visibility = Visibility.Collapsed;
                            }
                        }

                        this.lookAheadView.View.Columns[0].StackPanel.Children[rowCount - 1 - i].Visibility = Visibility.Collapsed;
                    }
                    else if (i > this.mainView.View.LastVisibleItemIndex)
                    {
                        this.lookBehindView.View.Columns[0].StackPanel.Children[i].Visibility = Visibility.Collapsed;
                        this.lookBehindView.View.Columns[0].StackPanel.Children[i].UpdateLayout();
                        if (!this.mainView.View.Rows[i].IsBlank)
                        {
                            foreach (ColumnManager currentColumn in this.mainView.View.Columns)
                            {
                                currentColumn.StackPanel.Children[i].Visibility = Visibility.Visible;
                            }
                        }

                        this.lookAheadView.View.Columns[0].StackPanel.Children[rowCount - 1 - i].Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.lookBehindView.View.Columns[0].StackPanel.Children[i].Visibility = Visibility.Collapsed;
                        this.lookBehindView.View.Columns[0].StackPanel.Children[i].UpdateLayout();
#if SILVERLIGHT
                        if (this.mainView.View.Rows[i].RowCollapsed && this.mainView.View.Rows[i].RowExpanded && this.mainView.View.Rows[i].GroupingHeader != null)
                        {
                            this.mainView.View.Rows[i].CollapseRow(true, false);
                            this.mainView.View.Rows[i].RowExpanded = false;
                        }
#endif
                        foreach (ColumnManager currentColumn in this.mainView.View.Columns)
                        {
                            if (i < currentColumn.StackPanel.Children.Count)
                            {
                                currentColumn.StackPanel.Children[i].Visibility = Visibility.Visible;
                            }
                        }

                        this.lookAheadView.View.Columns[0].StackPanel.Children[rowCount - 1 - i].Visibility = Visibility.Collapsed;
                    }
                }

                // Show the occluded item.
                if (startOffset > 0 && startOffset < this.MainView.Rows.Count)
                {
                    this.lookBehindView.View.Columns[0].StackPanel.Children[startOffset].Visibility = Visibility.Visible;
                    this.lookBehindView.View.Columns[0].StackPanel.Children[startOffset].UpdateLayout();
                }

                this.UpdateLookBehindBar();
                
                //// Don't display last row in look ahead if it is partially visible.
                this.HandleLastRowCase();                
            }
        }

        /// <summary>
        /// Raise the grouping render event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Cui.Controls.GroupingRenderEventArgs"/> instance containing the event data.</param>
        private void MainView_OnGroupingRender(object sender, EventArgs e)
        {
            GroupingRenderEventArgs args = e as GroupingRenderEventArgs;
            if (this.OnGroupingRender != null)
            {
                this.OnGroupingRender(this, args);
            }
        }

        /// <summary>
        /// Render the datasource.
        /// </summary>
        private void RenderData()
        {
            if (null != this.provider)
            {
                int counter = 0;
                foreach (object rowDictionary in this.provider)
                {
                    if (counter < 15)
                    {
                        this.mainView.View.AddRow(rowDictionary);
                        this.lookAheadView.View.AddRow(rowDictionary);
                        this.lookBehindView.View.AddRow(rowDictionary);
                    }
                    else
                    {
                        this.mainView.View.AddBlank(rowDictionary);
                        this.lookAheadView.View.AddBlank(rowDictionary);
                        this.lookBehindView.View.AddBlank(rowDictionary);
                    }

                    counter++;
                }

                this.mainView.View.AddRowsComplete();

                if (this.mainView.View.Rows.Count > 0)
                {
                    for (int i = 0; i < this.mainView.View.Rows.Count; i++)
                    {
                        this.lookBehindView.View.Columns[0].StackPanel.Children[i].Visibility = Visibility.Collapsed;                        
                        this.mainView.View.Rows[i].PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(this.WrapDataGrid_PropertyChanged);
                    }

                    this.mainView.View.UpdateGroupCounts();
                }

                // Setting the scrollbar position to first item on rendering data.
                this.scrollBar.Value = this.mainView.View.FirstVisibleItemIndex;
                WrapDataGrid.ApplyRowBackgrounds(this);
            }
        }

        /// <summary>
        /// Handles the property changed event of the main view rows.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing details of the property that was changed.</param>
        private void WrapDataGrid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "RowHeight")
            {
                this.timer.Change(100, Timeout.Infinite);
            }
        }

        /// <summary>
        /// Refreshes the layout of grid.
        /// </summary>
        /// <param name="startOffset">Index of start object.</param>
        private void RefreshLayoutOnDispatcher(int startOffset)
        { 
            if (!this.gridIsBeingLoaded)
            {
#if SILVERLIGHT
                int index = this.MainView.FirstVisibleItemIndex == 0 ? this.MeasureGrid(this.MainView.FirstVisibleItemIndex) : this.MainView.FirstVisibleItemIndex;
#else
                int index = this.MeasureGrid(Convert.ToInt32(this.ScrollBar.Value));
#endif
                this.RefreshViews(index);               
            }

              this.ScrollToRow = true;              
        }

        /// <summary>
        /// Refreshes the layout by invoking required methods on dispatcher.
        /// </summary>
        /// <param name="state">State of the call.</param>
        private void RefreshLayout(object state)
        {
#if SILVERLIGHT
            this.LayoutRootGrid.Dispatcher.BeginInvoke(new RefreshLayoutDelegate(this.RefreshLayoutOnDispatcher), new object[] { this.mainView.View.FirstVisibleItemIndex });
#else
            this.LayoutRootGrid.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new RefreshLayoutDelegate(this.RefreshLayoutOnDispatcher), this.mainView.View.FirstVisibleItemIndex);
#endif
        }

        /// <summary>
        /// Creates the columns in the main grid based on the columns defined in the UI.
        /// </summary>
        private void CreateGridColumns()
        {
            if (this.columns.Count == 0)
            {
                throw new ArgumentException("Grid must have atleast one column defined");
            }

            // Grouping col
            ColumnDefinition groupingCol = new ColumnDefinition();
            groupingCol.Width = new GridLength(0);
            this.layoutRoot.ColumnDefinitions.Add(groupingCol);

            // One column extra for grouping and one for scrollbar
            for (int i = 0; i < this.columns.Count; i++)
            {
                ColumnDefinition columnDef = new ColumnDefinition();
                columnDef.Width = this.columns[i].ColumnDefinition.Width;
                columnDef.MinWidth = this.columns[i].ColumnDefinition.MinWidth;
                columnDef.MaxWidth = this.columns[i].ColumnDefinition.MaxWidth;
                this.layoutRoot.ColumnDefinitions.Add(columnDef);
            }

            // Grouping col
            ColumnDefinition scrollBarCol = new ColumnDefinition();
            scrollBarCol.Width = new GridLength(this.scrollBar.Width);
            this.layoutRoot.ColumnDefinitions.Add(scrollBarCol);
            this.scrollBar.SetValue(Grid.ColumnProperty, this.layoutRoot.ColumnDefinitions.Count - 1);
            if (this.allowColumnResizing)
            {
                for (int i = 1; i < this.columns.Count; i++)
                {
                    UIElement resizer;
                    if (this.columnResizerTemplate == null)
                    {
                        resizer = this.GetDefaultResizer();
                    }
                    else
                    {
                        resizer = DataTemplateHelper.LoadContent(this.columnResizerTemplate) as UIElement;
                    }

                    resizer.SetValue(Grid.ColumnProperty, i);
                    resizer.SetValue(Grid.RowProperty, 1);
                    this.layoutRoot.Children.Add(resizer);
                }
            }
        }

        /// <summary>
        /// Creates a grouping column.        
        /// </summary>
        private void CreateGroupingColumn()
        {
            ColumnManager column = new ColumnManager();

            // Load our cell template, then the ISV cell template and set grid column/row properties to isv cell template.
            DataTemplate cellTemplate = this.layoutRoot.Resources["GroupingColumnTemplate"] as DataTemplate;
            Grid headerTemplate = DataTemplateHelper.LoadContent(this.layoutRoot.Resources["GroupingHeaderTemplate"] as DataTemplate) as Grid;
            headerTemplate.SetValue(Grid.RowProperty, 1);
            headerTemplate.SetValue(Grid.ColumnProperty, 0);

            // Column manager stack panel.
            StackPanel columnStackPanel = new StackPanel();
            columnStackPanel.SetValue(Canvas.ZIndexProperty, 9999);
            columnStackPanel.SetValue(Grid.ColumnProperty, 0);
            columnStackPanel.SetValue(Grid.RowProperty, 4);

            // Set column manager properties.
            column.MasterCellTemplate = null;
            column.CellTemplate = cellTemplate;
            column.ColumnHeader = headerTemplate;
            column.StackPanel = columnStackPanel;

            // Add Column manager to column manager collection
            this.mainViewColumns.Add(column);
        }

        /// <summary>
        /// Creates columns defined in the UI.
        /// </summary>
        private void CreateColumns()
        {
            if (this.columns.Count > 0)
            {
                this.mainView = new MainView();
                this.CreateGroupingColumn();

                for (int columnIndex = 0; columnIndex < this.columns.Count; columnIndex++)
                {
                    WrapDataGridColumn currentColumn = this.columns[columnIndex];

                    CheckCellTemplate(currentColumn.CellTemplate);
                    CheckHeaderTemplate(currentColumn.HeaderTemplate);

                    ColumnManager column = new ColumnManager();

                    // Load our cell template, then the ISV cell template and set grid column/row properties to isv cell template.                    
                    Grid headerTemplate = DataTemplateHelper.LoadContent((this.layoutRoot.Resources["HeaderTemplate"] as DataTemplate)) as Grid;
                    headerTemplate.SetValue(Grid.RowProperty, 1);
                    headerTemplate.SetValue(Grid.ColumnProperty, columnIndex + 1);

                    if (columnIndex + 1 == this.columns.Count)
                    {
                        // Setting colspan to 2 to cover scrollbar top
                        headerTemplate.SetValue(Grid.ColumnSpanProperty, 2);
                    }

                    // Header template
                    UIElement customHeaderTemplate = DataTemplateHelper.LoadContent(currentColumn.HeaderTemplate) as UIElement;
                    customHeaderTemplate.SetValue(Grid.ColumnProperty, 0);
                    customHeaderTemplate.SetValue(Grid.RowProperty, 0);
                    headerTemplate.Children.Add(customHeaderTemplate);

                    DataTemplate cellTemplate = this.layoutRoot.Resources["CellTemplate"] as DataTemplate;

                    // Column manager stack panel.
                    StackPanel columnStackPanel = new StackPanel();

                    columnStackPanel.SetValue(Grid.ColumnProperty, columnIndex + 1);
                    columnStackPanel.SetValue(Grid.RowProperty, 4);

                    // Set column manager properties.
                    column.MasterCellTemplate = cellTemplate;
                    column.CellTemplate = currentColumn.CellTemplate;
                    column.ColumnHeader = headerTemplate;
                    column.StackPanel = columnStackPanel;
                    column.BodyMargin = currentColumn.BodyMargin;
                    column.ToolTipTemplate = currentColumn.ToolTipTemplate;

                    // Add Column manager to column manager collection
                    this.mainViewColumns.Add(column);
                }
            }
        }

        /// <summary>
        /// Creates Look ahead column.
        /// </summary>
        private void CreateLookAheadColumns()
        {
            if (this.lookAheadCellTemplate == null)
            {
                throw new ArgumentNullException("Look ahead cell template must be defined", (Exception)null);
            }

            if (this.lookAheadSummaryCellTemplate == null)
            {
                throw new ArgumentNullException("Look ahead summary cell template must be defined", (Exception)null);
            }

            this.lookAheadView = new LookAheadView();
            this.lookAheadColumn = new ColumnManager();

            Grid lookAheadLayout = DataTemplateHelper.LoadContent((this.layoutRoot.Resources["LookAheadCellTemplateGridLayout"] as DataTemplate)) as Grid;
            lookAheadLayout.SetValue(Grid.ColumnSpanProperty, this.WrapDataGridColumns.Count + 1);
            DataTemplate cellTemplate = this.layoutRoot.Resources["LookAheadCellTemplate"] as DataTemplate;

            StackPanel mainPanel = new StackPanel();
            mainPanel.SetValue(Grid.RowProperty, 0);
            mainPanel.SetValue(Grid.ColumnProperty, 0);

            StackPanel columnPanel = new StackPanel();
            columnPanel.HorizontalAlignment = HorizontalAlignment.Right;
            columnPanel.Orientation = Orientation.Horizontal;
            columnPanel.SetValue(Grid.RowProperty, 0);
            columnPanel.SetValue(Grid.ColumnProperty, 1);

            SummaryManager summary = new SummaryManager();
            summary.CellTemplate = this.lookAheadSummaryCellTemplate;
            summary.MainPanel = mainPanel;
            this.lookAheadColumn.MasterCellTemplate = cellTemplate;
            this.lookAheadColumn.CellTemplate = this.lookAheadCellTemplate;
            this.lookAheadColumn.StackPanel = columnPanel;
            this.lookAheadColumn.SummaryManager = summary;
            this.lookAheadColumn.GridLayout = lookAheadLayout;
        }

        /// <summary>
        /// Creates Look behind column.
        /// </summary>
        private void CreateLookBehindColumns()
        {
            if (this.lookBehindCellTemplate == null)
            {
                throw new ArgumentNullException("Look behind cell template must be defined", (Exception)null);
            }

            if (this.lookBehindSummaryCellTemplate == null)
            {
                throw new ArgumentNullException("Look behind summary cell template must be defined", (Exception)null);
            }

            this.lookBehindView = new LookBehindView();
            this.lookBehindColumn = new ColumnManager();
            Grid lookBehindLayout = DataTemplateHelper.LoadContent((this.layoutRoot.Resources["LookBehindCellTemplateGridLayout"] as DataTemplate)) as Grid;
            lookBehindLayout.SetValue(Grid.ColumnSpanProperty, this.WrapDataGridColumns.Count + 1);
            DataTemplate cellTemplate = this.layoutRoot.Resources["LookBehindCellTemplate"] as DataTemplate;
            StackPanel summaryPanel = new StackPanel();
            summaryPanel.SetValue(Grid.RowProperty, 0);
            summaryPanel.SetValue(Grid.ColumnProperty, 0);

            StackPanel columnPanel = new StackPanel();
            columnPanel.HorizontalAlignment = HorizontalAlignment.Right;
            columnPanel.Orientation = Orientation.Horizontal;
            columnPanel.SetValue(Grid.RowProperty, 0);
            columnPanel.SetValue(Grid.ColumnProperty, 1);

            SummaryManager summary = new SummaryManager();
            summary.CellTemplate = this.lookBehindSummaryCellTemplate;
            summary.MainPanel = summaryPanel;

            this.lookBehindColumn.SetValue(Grid.ColumnSpanProperty, 5);
            this.lookBehindColumn.MasterCellTemplate = cellTemplate;
            this.lookBehindColumn.CellTemplate = this.lookBehindCellTemplate;
            this.lookBehindColumn.StackPanel = columnPanel;
            this.lookBehindColumn.SummaryManager = summary;
            this.lookBehindColumn.GridLayout = lookBehindLayout;
        }

        /// <summary>
        /// Handles the Group header click event.
        /// </summary>
        /// <param name="sender">Group header that was clicked.</param>
        /// <param name="e">Event args containing the group details.</param>
        private void MainView_OnGroupHeaderClick(object sender, EventArgs e)
        {
            GroupByEventArgs args = e as GroupByEventArgs;
            if (this.OnGroupHeaderClick != null)
            {
                this.OnGroupHeaderClick(sender, args);
            }
        }

        /// <summary>
        /// Checks that no blanks are left
        /// in the bottom of the grid because of that data not being loaded yet.
        /// </summary>
        /// <param name="heightOfAllVisibleRows">Height of all the data-loaded rows in the grid.</param>
        /// <param name="lastRowWithLoadedData">Index of the last row with loaded-data in the grid.</param>        
        /// <param name="blankRowsExistInVisibleArea">True, if blank rows exist in main display.</param>
        private void CheckIfDataInMainViewIsSufficient(double heightOfAllVisibleRows, int lastRowWithLoadedData, bool blankRowsExistInVisibleArea)
        {
            if ((heightOfAllVisibleRows < this.scrollBar.ActualHeight) || (heightOfAllVisibleRows < this.heightSetForScrollbar))
            {
                //// No need to load rows if the end of the grid is reached, as all data in that case is guaranteed to have been loaded.(even if done through scrollthumb-drag)
                if (this.scrollBar.Value < this.scrollBar.Maximum)
                {
                    //// 3rd check - Only load more rows into the visible area if there are more rows to bring into view from below
                    if ((this.scrollBar.Value + this.mainView.View.VisibleItemCount) < this.mainView.View.Rows.Count)
                    {
                        //// If there is space left in the grid, load the next set of rows (if any) to fill the bottom whitespace
                        this.LoadNextBatchOfRows(lastRowWithLoadedData, false, true);
                    }
                    else if ((blankRowsExistInVisibleArea && this.ScrollBar.ActualHeight == 0)
                                || (blankRowsExistInVisibleArea && this.ScrollBar.Visibility == Visibility.Collapsed))
                    {
                        //// If blank rows exist in main view and scrollbar is not visible - check and add more rows if any
                        this.LoadNextBatchOfRows(lastRowWithLoadedData, false, true);
                    }
                }
            }
        }

        /// <summary>
        /// Sets up the the animation screen shown for large delays.
        /// </summary>
        private void LoadGenericAnimationControl()
        {
            this.genericAnimation.SetValue(Grid.ColumnProperty, 0);
            this.genericAnimation.SetValue(Grid.RowProperty, 3);
            this.genericAnimation.SetValue(Grid.RowSpanProperty, 3);
            this.genericAnimation.SetValue(Grid.ColumnSpanProperty, this.WrapDataGridColumns.Count + 1);
            this.genericAnimation.SetValue(Canvas.ZIndexProperty, 99999);
            this.genericAnimation.Visibility = Visibility.Collapsed;
            this.layoutRoot.Children.Add(this.genericAnimation);
        }

        /// <summary>
        /// Sets up the the animation screen shown for large scrolls.
        /// </summary>
        private void LoadScrollAnimationControl()
        {
            this.scrollAnimation.SetValue(Grid.ColumnProperty, 0);
            this.scrollAnimation.SetValue(Grid.RowProperty, 3);
            this.scrollAnimation.SetValue(Grid.RowSpanProperty, 3);
            this.scrollAnimation.SetValue(Grid.ColumnSpanProperty, this.WrapDataGridColumns.Count + 1);
            this.scrollAnimation.SetValue(Canvas.ZIndexProperty, 99999);
            this.scrollAnimation.Visibility = Visibility.Collapsed;
            this.layoutRoot.Children.Add(this.scrollAnimation);
        }
        
        /// <summary>
        /// Updates sizes of few controls within the grid and then refreshes it.
        /// </summary>
        private void UpdateSizesAndRefresh()
        {
            this.SetSizeForFirstColumn();
            int index = this.MeasureGrid(this.mainView.View.FirstVisibleItemIndex);
            this.RefreshViews(index);
            this.LookAhead_SizeChanged(null, null);
            this.UpdateLookBehindBar();
        }

        /// <summary>
        /// Sets the size of the first column (usually the one that contains the Grouping header).
        /// </summary>
        private void SetSizeForFirstColumn()
        {
            double scrollbarWidth = 0.0;

#if SILVERLIGHT
            if (null != this.scrollBar.Parent as Grid && Grid.GetColumn(this.scrollBar) >= 0)
            {
                scrollbarWidth = (this.scrollBar.Parent as Grid).ColumnDefinitions[Grid.GetColumn(this.scrollBar)].ActualWidth;
            }
#else
            if (this.scrollBar == null)
            {
                return;
            }

            //// Using Width.Value instead of ActualWidth for WPF version
            if (null != this.scrollBar.Parent as Grid && Grid.GetColumn(this.scrollBar) >= 0)
            {
                scrollbarWidth = (this.scrollBar.Parent as Grid).ColumnDefinitions[Grid.GetColumn(this.scrollBar)].Width.Value;
            }
#endif

            double contentWidth = (this.Content as Grid).ActualWidth;

            this.mainView.View.Columns[0].StackPanel.Margin = new Thickness(0, 0, -1 * (contentWidth - scrollbarWidth), 0);
        }        
        #endregion        
    }
}

