//-----------------------------------------------------------------------
// <copyright file="DataBoundRow.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The DataBoundRow helper class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows;

    /// <summary>
    /// The DataBoundRow helper class.
    /// </summary>
    public class DataBoundRow : UserControl, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Row Background brush.
        /// </summary>
        public static readonly new DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(DataBoundRow), new PropertyMetadata(new PropertyChangedCallback(OnBackgroundPropertyChanged)));
  
        /// <summary>
        /// The cells list private member variable.
        /// </summary>
        private System.Collections.ObjectModel.Collection<Panel> list = new System.Collections.ObjectModel.Collection<Panel>();

        /// <summary>
        /// The previous row.
        /// </summary>
        private DataBoundRow previousRow;

        /// <summary>
        /// Member variable to hold the row height property.
        /// </summary>
        private double rowHeight;

        /// <summary>
        /// The next row.
        /// </summary>
        private DataBoundRow nextRow;

        /// <summary>
        /// The DataBoundRowGrouping for the row.
        /// </summary>
        private DataBoundRowGrouping grouping;

        /// <summary>
        /// Member variable to hold the row collpased state.
        /// </summary>
        private bool rowCollapsed;        
        
        /// <summary>
        /// Reference to the toplevel WrapDataGrid.
        /// </summary>
        private WrapDataGrid hostingWrapDataGrid;

        /// <summary>
        /// Collapsed row is internally expanded to retain data.
        /// </summary>
        private bool rowExpanded;

        /// <summary>
        /// The index of the current row.
        /// </summary>
        private int index;

        /// <summary>
        /// Member variable to hold grouping header.
        /// </summary>
        private UIElement groupingHeader;

        /// <summary>
        /// Indicates whether the row is blank.
        /// </summary>
        private bool isblank;

        /// <summary>
        /// Indicates whether the row is selected.
        /// </summary>
        private bool rowSelected;

        /// <summary>
        /// Indicates whether the row has focus.
        /// </summary>
        private bool rowFocus;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBoundRow"/> class.
        /// </summary>
        /// <param name="previousRow">The previous DataBoundRow.</param>
        public DataBoundRow(DataBoundRow previousRow)
        {
            this.previousRow = previousRow;
            if (null != previousRow)
            {
                this.previousRow.NextRow = this;
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Event handler to notify property changed events.
        /// </summary>
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Gets or sets the grouping.
        /// </summary>
        /// <value>The grouping for the row.</value>
        public DataBoundRowGrouping Grouping
        {
            get
            {
                return this.grouping;
            }

            set
            {
                this.grouping = value;
            }
        }

        /// <summary>
        /// Gets or sets the next row.
        /// </summary>
        /// <value>The next row.</value>
        public DataBoundRow NextRow
        {
            get
            {
                return this.nextRow;
            }

            set
            {
                this.nextRow = value;
            }
        }

        /// <summary>
        /// Gets the list of cells in this row.
        /// </summary>
        /// <value>The cells list.</value>
        public System.Collections.ObjectModel.Collection<Panel> List
        {
            get
            {
                return this.list;
            }            
        }

        /// <summary>
        /// Gets or sets the row height property.
        /// </summary>
        /// <value>Current row height.</value>
        public double RowHeight
        {
            get
            {
                return this.rowHeight;
            }

            set
            {
                bool largerCellExistsInRow = false;

                double height = value;
                foreach (StackPanel panel in this.List)
                {
                    if ((panel.Children[0] as FrameworkElement).ActualHeight > height)
                    {
                        largerCellExistsInRow = true;
                        break;
                    }
                }

                if (false == largerCellExistsInRow)
                {
                    this.rowHeight = height;
                    this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("RowHeight"));
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the row is in collpased state or not.
        /// </summary>
        /// <value>Row collapsed.</value>
        public bool RowCollapsed
        {
            get
            {
                return this.rowCollapsed;
            }

            set
            {
                this.rowCollapsed = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the row is selected.
        /// </summary>
        /// <value>Row collapsed.</value>
        public bool RowSelected
        {
            get
            {
                return this.rowSelected;
            }

            set
            {
                this.rowSelected = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the row has focus.
        /// </summary>
        /// <value>Row collapsed.</value>
        public bool RowFocus
        {
            get
            {
                return this.rowFocus;
            }

            set
            {
                this.rowFocus = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the row is in an internally expanded state or not.
        /// </summary>
        /// <value>Row collapsed.</value>
        public bool RowExpanded
        {
            get
            {
                return this.rowExpanded;
            }

            set
            {
                this.rowExpanded = value;
            }
        }

        /// <summary> 
        /// Gets or sets the row's Background Brush.
        /// </summary>
        /// <value>Background color.</value>
        public new Brush Background
        {
            get
            {
                return GetValue(BackgroundProperty) as Brush;
            }

            set
            {
                SetValue(BackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the row is blank.
        /// </summary>
        /// <value>Control state.</value>
        public bool IsBlank
        {
            get 
            {
                return this.isblank;
            }

            set
            {
                this.isblank = value;
            }
        }

        /// <summary>
        /// Gets or sets a reference to the toplevel WrapDataGrid.
        /// </summary>
        /// <value>Hosting wrap data grid.</value>
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
        /// Gets the index of the current row.
        /// </summary>
        /// <value>Current row index.</value>
        public int Index
        {
            get
            {
                return this.index;
            }

            internal set
            {
                this.index = value;
            }
        }

        /// <summary>
        /// Gets or sets the Grouping header element.
        /// </summary>
        /// <value>Grouping header.</value>
        public UIElement GroupingHeader
        {
            get 
            { 
                return this.groupingHeader; 
            }

            set 
            { 
                this.groupingHeader = value; 
            }
        }

        /// <summary>
        /// Gets or sets the Row Actual Height.
        /// </summary>
        /// <value>Actual height of row.</value>
        public double RowActualHeight 
        { 
            get;
            set;
        }

        /// <summary>
        /// Sets the selection state for the current row.
        /// </summary>
        /// <param name="selected">If set to <c>true</c> then the row is [selected].</param>
        public void Select(bool selected)
        {
            if (selected == true)
            {
                this.Background = this.hostingWrapDataGrid.CurrentSelectedRowBackground;
            }
            else
            {
                this.Background = null;
            }
            
            this.EnsureBackground();
        }

        /// <summary>
        /// Sets the selection state for the other rows apart from current row.
        /// </summary>
        /// <param name="selected">If set to <c>true</c> then the row is [selected].</param>
        public void SelectCollectionRow(bool selected)
        {
            if (selected == true)
            {
                this.Background = this.hostingWrapDataGrid.SelectionBackground;
            }
            else
            {
                this.Background = null;
            }

            this.EnsureBackground();
        }

        /// <summary>
        /// Sets the highlighted state for the current row.
        /// </summary>
        /// <param name="selected">If set to <c>true</c> then the row is [highlighted].</param>
        public void Highlight(bool selected)
        {
            if (selected == true)
            {
                if (this.Index % 2 == 0)
                {
                    this.Background = this.hostingWrapDataGrid.CurrentEvenRowBackground;
                }
                else
                {
                    this.Background = this.hostingWrapDataGrid.CurrentOddRowBackground;
                }
            }
            else
            {
                this.Background = null;
            }

            this.EnsureBackground();
        }       

        /// <summary>
        /// Expands or collapses a row.
        /// </summary>
        /// <param name="collapse">If set to true then the row is collapsed.</param>
        /// <param name="setCollapsed">
        /// If set to true then sets rowCollapsed accordingly.
        /// </param>
        public void CollapseRow(bool collapse, bool setCollapsed)
        {
            // Avoid setting the row height if the row is blank
            if (!this.IsBlank) 
            {
                if (collapse)
                {
                    double height = 0.0;
                    foreach (UIElement element in (this.list[0].Children[0] as Panel).Children)
                    {
                        FrameworkElement e = element as FrameworkElement;
                        if (e != null && e.Name == "Grouping")
                        {
                            height = e.ActualHeight;
                        }
                    }

                    this.rowHeight = height;
                    this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("RowHeight"));
                }
                else
                {
                    foreach (StackPanel panel in this.List)
                    {
                        this.RowHeight = (panel.Children[0] as FrameworkElement).ActualHeight;
                    }
                }
            }

            if (setCollapsed)
            {
                this.rowCollapsed = collapse;
            }
        }
       
        /// <summary>
        /// Ensures the background brush is correctly set.
        /// </summary>
        internal void EnsureBackground()
        {
            Brush newBackground = null;

            if (this.Background == null)
            {
                if (this.HostingWrapDataGrid != null)
                {
                    if (this.Index % 2 == 0 || this.HostingWrapDataGrid.AlternatingRowBackground == null)
                    {
                        // Use OwningGrid.RowBackground if the index is even or if the OwningGrid.AlternatingRowBackground is null 
                        if (this.HostingWrapDataGrid.RowBackground != null)
                        {
                            newBackground = this.HostingWrapDataGrid.RowBackground;
                        }
                    }
                    else
                    {
                        // Alternate row
                        if (this.HostingWrapDataGrid.AlternatingRowBackground != null)
                        {
                            newBackground = this.HostingWrapDataGrid.AlternatingRowBackground;
                        }
                    }
                }
            }
            else
            {
                newBackground = this.Background;
            }

            bool dogrouping = false;

            foreach (Panel cellPanel in this.List)
            {                
                if (true == dogrouping)
                {
                    cellPanel.Background = newBackground;                    
                }
                else
                {
                    dogrouping = true;
                }
            }
        }

        /// <summary>
        /// Called when the background property changes.
        /// </summary>
        /// <param name="rowInstance">The DataGridRow.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnBackgroundPropertyChanged(DependencyObject rowInstance, DependencyPropertyChangedEventArgs e)
        {
            DataBoundRow dataBoundRow = rowInstance as DataBoundRow;
            if (dataBoundRow != null && dataBoundRow.list != null && e.OldValue != e.NewValue)
            {
                bool processRow = false;
                foreach (Panel cellPanel in dataBoundRow.list)
                {
                    if (true == processRow)
                    {
                        cellPanel.Background = e.NewValue as Brush;
                    }
                    else
                    {
                        processRow = true;
                    }
                }
            }
        }
    }
}
