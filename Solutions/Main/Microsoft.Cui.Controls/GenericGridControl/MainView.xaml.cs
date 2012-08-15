//-----------------------------------------------------------------------
// <copyright file="MainView.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The MainView component for the WrapDataGrid Silverlight control. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// The MainView component for the WrapDataGrid Silverlight control.
    /// </summary>
    public partial class MainView : UserControl
    {
        /// <summary>
        /// Private ColumnView instance.
        /// </summary>
        private ColumnView view;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            // Required to initialize variables
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>The view instance.</value>
        public ColumnView View
        {
            get
            {
                return this.view;
            }

            set
            {
                this.view = value;
            }
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        /// <param name="grid">The main grid.</param>
        /// <param name="groupingTemplateLogic">The grouping template logic.</param>
        /// <param name="groupingTemplatePresentation">The grouping template presentation.</param>
        /// <param name="columnManagers">Main view column managers.</param>
        public void InitializeView(Grid grid, DataTemplate groupingTemplateLogic, DataTemplate groupingTemplatePresentation, System.Collections.ObjectModel.Collection<ColumnManager> columnManagers)
        {
            this.view = new ColumnView(grid, columnManagers, groupingTemplateLogic, groupingTemplatePresentation);
            this.view.MainView = true;
        }
    }
}
