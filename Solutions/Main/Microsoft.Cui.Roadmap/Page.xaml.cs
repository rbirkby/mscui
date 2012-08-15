//-----------------------------------------------------------------------
// <copyright file="Page.xaml.cs" company="Microsoft Corporation copyright 2008.">
// Copyright (c) Microsoft Corporation copyright 2008.
// All rights reserved.
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>23-Jan-2009</date>
// <summary>Page class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Roadmap
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

    /// <summary>
    /// Page class for the roadmap.
    /// </summary>
    public partial class Page : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Page"/> class.
        /// </summary>
        public Page()
        {
            // Required to initialize variables
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Loaded event of the Grid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Grid grid = sender as Grid;
            object tip = ToolTipService.GetToolTip(grid);
            FrameworkElement ele = tip as FrameworkElement;
            ele.DataContext = grid.DataContext;            
        }
    }
}