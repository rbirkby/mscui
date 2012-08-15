//-----------------------------------------------------------------------
// <copyright file="LookBehindView.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The LookBehindView component for the WrapDataGrid Silverlight control. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Windows.Controls;

    /// <summary>
    /// The LookBehindView component for the WrapDataGrid Silverlight control.
    /// </summary>
    public partial class LookBehindView : UserControl
    {
        /// <summary>
        /// Private ColumnView instance.
        /// </summary>
        private ColumnView view;

        /// <summary>
        /// Initializes a new instance of the <see cref="LookBehindView"/> class.
        /// </summary>
        public LookBehindView()
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
        /// Initialises the view.
        /// </summary>
        /// <param name="grid">The layoutRoot grid.</param>
        /// <param name="columnManager">Look behind view column manager.</param>
        public void InitializeView(Grid grid, ColumnManager columnManager)
        {
            System.Collections.ObjectModel.Collection<ColumnManager> columns = new System.Collections.ObjectModel.Collection<ColumnManager>();
            columns.Add(columnManager);

            this.view = new ColumnView(grid, columns, false);
        }
    }
}
