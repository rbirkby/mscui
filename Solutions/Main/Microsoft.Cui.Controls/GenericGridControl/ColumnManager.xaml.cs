//-----------------------------------------------------------------------
// <copyright file="ColumnManager.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The ColumnManager component for the WrapDataGrid Silverlight control. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// The ColumnManager component for the WrapDataGrid Silverlight control which is used while creating the data elements in the grid.
    /// </summary>
    public partial class ColumnManager : UserControl
    {       
        /// <summary>
        /// The cellTemplate private member variable which is used to define a cell's display content.
        /// </summary>
        private DataTemplate cellTemplate;

        /// <summary>
        /// The tooltipTemplate private member variable which is used to define a cell's tooltip's display content.
        /// </summary>
        private DataTemplate tooltipTemplate;

        /// <summary>
        /// A stackPanel private member variable used internally while creating cells.
        /// </summary>
        private StackPanel stackPanel;

        /// <summary>
        /// The summary manager private member variable used internally while creating look ahead/ look behind.
        /// </summary>        
        private SummaryManager summaryManager;
      
        /// <summary>
        /// Layout grid for the look ahead/look behind.
        /// </summary>
        private Grid gridLayout;     

        /// <summary>
        /// The column header of a grid column.
        /// </summary>
        private Grid columnHeader;

        /// <summary>
        /// Member variable to hold master cell template.
        /// </summary>
        private DataTemplate masterCellTemplate;

        /// <summary>
        /// Thickness of a cell's body margin.
        /// </summary>
        private Thickness bodyMargin;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnManager"/> class.
        /// </summary>
        public ColumnManager()
        {
            // Required to initialize variables
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the margin for the column's cells.
        /// </summary>
        /// <value>Body margin.</value>
        public Thickness BodyMargin
        {
            get { return this.bodyMargin; }
            set { this.bodyMargin = value; }
        }      

        /// <summary>
        /// Gets or sets the column header UI element.
        /// </summary>
        /// <value>The column header.</value>
        public Grid ColumnHeader
        {
            get
            {
                return this.columnHeader;
            }

            set
            {
                if (null != value)
                {
                    this.columnHeader = value;
                    this.columnHeader.Style = (Style)this.Resources["ColumnHeaderStyle"];
                }
            }
        }

        /// <summary>
        /// Gets or sets the summary manager for the look ahead/ behind.
        /// </summary>
        /// <value>
        /// Summary manager.
        /// </value>
        public SummaryManager SummaryManager
        {
            get { return this.summaryManager; }
            set { this.summaryManager = value; }
        }

        /// <summary>
        /// Gets or sets the layout grid for the look ahead/ behind.
        /// </summary> 
        /// <value>Grid layout.</value>
        public Grid GridLayout
        {
            get { return this.gridLayout; }
            set { this.gridLayout = value; }
        }

        /// <summary>
        /// Gets or sets the stack panel for the column's cells.
        /// </summary>
        /// <value>The stack panel.</value>
        public StackPanel StackPanel
        {
            get
            {
                return this.stackPanel;
            }

            set
            {
                this.stackPanel = value;
            }
        }

        /// <summary>
        /// Gets or sets the template which defines the layout on the column's cells.
        /// </summary>
        /// <value>The cell template.</value>
        public DataTemplate CellTemplate
        {
            get
            {
                return this.cellTemplate;
            }

            set
            {
                this.cellTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets the template which defines the layout on the column's tooltips.
        /// </summary>
        /// <value>The tooltip template.</value>
        public DataTemplate ToolTipTemplate
        {
            get
            { 
                return this.tooltipTemplate;
            }

            set
            {
                this.tooltipTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets the master template to be used for a cell.
        /// </summary>
        internal DataTemplate MasterCellTemplate
        {
            get
            {
                return this.masterCellTemplate;
            }

            set
            {
                this.masterCellTemplate = value;
            }
        }       
    }
}
