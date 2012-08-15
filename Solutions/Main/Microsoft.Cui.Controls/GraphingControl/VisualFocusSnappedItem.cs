//-----------------------------------------------------------------------
// <copyright file="VisualFocusSnappedItem.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Class used to denote a visual focus snapped data element.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.Globalization;
    using System.Text;
    using System.ComponentModel;
    using Microsoft.Cui.Controls.Common;
    #endregion

    /// <summary>
    /// Specifies a point on the graph that has been snapped by the Visual Focus Line.
    /// </summary>    
    internal class VisualFocusSnappedItem
    {
        /// <summary>
        /// Initializes a new instance of VisualFocusSnappedItem.
        /// </summary>
        public VisualFocusSnappedItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of VisualFocusSnappedItem.
        /// </summary>
        /// <param name="element">Snapped element.</param>
        /// <param name="graph">Graph to which the element belongs.</param>
        /// <param name="data">Data of the item snapped.</param>
        /// <param name="offset">Offset of the item snapped.</param>
        public VisualFocusSnappedItem(FrameworkElement element, TimeGraphBase graph, object data, double offset)
        {
            this.SnappedElement = element;
            this.Graph = graph;
            this.Data = data;
            this.Offset = offset;
        }

        /// <summary>
        /// Gets or sets the member variable to hold visual focus snapped point.
        /// </summary>
        public FrameworkElement SnappedElement
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the member variable to hold the Graph to which the snapped point belongs.
        /// </summary>
        public TimeGraphBase Graph
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the data of the item snapped.
        /// </summary>
        /// <value>Data of the item snapped.</value>
        public object Data
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the offset of the marker snapped.
        /// </summary>
        /// <value>Offset of the marker snapped.</value>
        public double Offset
        {
            get;
            set;
        }
    }
}