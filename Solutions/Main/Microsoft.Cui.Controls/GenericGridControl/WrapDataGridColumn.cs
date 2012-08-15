//-----------------------------------------------------------------------
// <copyright file="WrapDataGridColumn.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>17-Mar-2008</date>
// <summary>Specifies a column in main view.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using Microsoft.Cui.Controls.GenericGridControl;

    /// <summary>
    /// Wrap data grid column.
    /// </summary>
    public class WrapDataGridColumn 
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
        /// The margin applied to this column's stack panel.
        /// </summary>
        private Thickness bodyMargin;

        /// <summary>
        /// Member variable to hold visibility.
        /// </summary>
        private bool columnVisibility = true;

        /// <summary>
        /// Member variable to hold sort indicator visibility.
        /// </summary>
        private bool sortIndicatorShownOnSort = true;

        /// <summary>
        /// Member variable to hold header template.
        /// </summary>
        private DataTemplate headerTemplate;

        /// <summary>
        /// Member variable to hold sort direction.
        /// </summary>
        private SortDirection presentSortDirection;

        /// <summary>
        /// Member variable to hold column definition.
        /// </summary>
        private ColumnDefinition columnDefinition = new ColumnDefinition();

        /// <summary>
        /// Initializes an instance of WrapDataGridColumn.
        /// </summary>
        public WrapDataGridColumn()
        {
        }       

        /// <summary>
        /// Gets or sets the body margin when one or more margins are specified as a comma separated string.
        /// </summary>
        /// <value>
        /// String value.
        /// </value>
        public string BodyMarginAsString
        {
            set 
            {
                if (null != value)
                {
                    string[] marginItems = value.Split(',');
                    if (marginItems.Length != 4)
                    {
                        throw new ArgumentException(WrapDataGridResources.BodyMarginFormat);
                    }

                    this.bodyMargin = new Thickness(Convert.ToDouble(marginItems[0], System.Globalization.CultureInfo.CurrentCulture), Convert.ToDouble(marginItems[1], System.Globalization.CultureInfo.CurrentCulture), Convert.ToDouble(marginItems[2], System.Globalization.CultureInfo.CurrentCulture), Convert.ToDouble(marginItems[3], System.Globalization.CultureInfo.CurrentCulture));
                }
            }

            get
            {
                return this.bodyMargin.ToString();
            }
        }

        /// <summary>
        /// Gets the body margin.
        /// </summary>
        /// <value>
        /// Body Margin.
        /// </value>
        public Thickness BodyMargin
        {
            get
            {
                return this.bodyMargin;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether column is visible in the grid.
        /// </summary>
        /// <value>
        /// True if visible.
        /// </value>
        public bool IsVisible
        {
            get
            {
                return this.columnVisibility;
            }

            set
            {
                this.columnVisibility = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether sort indicator is visible on the column header.
        /// </summary>
        /// <value>
        /// True if visible.
        /// </value>
        public bool SortIndicatorShownOnSort
        {
            get { return this.sortIndicatorShownOnSort; }
            set { this.sortIndicatorShownOnSort = value; }
        }

        /// <summary>
        /// Gets or sets the present sort direction by which the column has been sorted.
        /// </summary>
        /// <value>Sort direction.</value>
        public SortDirection PresentSortDirection
        {
            get { return this.presentSortDirection; }
            set { this.presentSortDirection = value; }
        }         

        /// <summary>
        /// Gets or sets the column definition.
        /// </summary>
        /// <value>Column definition.</value>
        public ColumnDefinition ColumnDefinition
        {
            get { return this.columnDefinition; }
            set { this.columnDefinition = value; }
        }

        /// <summary>
        /// Gets or sets the template which defines the layout on the column's cells.
        /// </summary>
        /// <value>Cell template.</value>
        public DataTemplate CellTemplate
        {
            get { return this.cellTemplate; }
            set { this.cellTemplate = value; }
        }

        /// <summary>
        /// Gets or sets the template which defines the layout on the column's tooltip.
        /// </summary>
        /// <value>The ToolTip template.</value>
        public DataTemplate ToolTipTemplate
        {
            get { return this.tooltipTemplate; }
            set { this.tooltipTemplate = value; }
        }

        /// <summary>
        /// Gets or sets the template which defines the layout on the column's header.
        /// </summary>
        /// <value>Header template.</value>
        public DataTemplate HeaderTemplate
        {
            get { return this.headerTemplate; }
            set { this.headerTemplate = value; }
        }
    }
}
