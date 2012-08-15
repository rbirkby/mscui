//-----------------------------------------------------------------------
// <copyright file="SummaryManager.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>11-Mar-2008</date>
// <summary>The SummaryManager component for the WrapDataGrid Silverlight control. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region "Using"
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    #endregion    

    /// <summary>
    /// The wrapper class to display summary count.
    /// </summary>
    public class SummaryManager
    {
        /// <summary>
        /// Main panel for the summary.
        /// </summary>
        private StackPanel mainPanel;

        /// <summary>
        /// Summary template.
        /// </summary>
        private DataTemplate cellTemplate;

        /// <summary>
        /// Gets or sets the main panel for the summary.
        /// </summary>
        /// <value>Main display panel.</value>
        public StackPanel MainPanel
        {
            get { return this.mainPanel; }
            set { this.mainPanel = value; }
        }

        /// <summary>
        /// Gets or sets the summary template.
        /// </summary>
        /// <value>Cell template.</value>
        public DataTemplate CellTemplate
        {
            get { return this.cellTemplate; }
            set { this.cellTemplate = value; }
        }
    }
}
