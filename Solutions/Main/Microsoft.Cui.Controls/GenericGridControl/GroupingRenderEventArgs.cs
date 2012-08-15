//-----------------------------------------------------------------------
// <copyright file="GroupingRenderEventArgs.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>13-Mar-2008</date>
// <summary>The GroupingRender event arguments class. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;

    /// <summary>
    /// Implementation of GroupingRenderEventArgs.
    /// </summary>
    public class GroupingRenderEventArgs : EventArgs
    {
        /// <summary>
        /// Member variable to hold grouping key.
        /// </summary>
        private string groupingKey;

        /// <summary>
        /// Member variable to hold group rows.
        /// </summary>
        private Collection<DataBoundRow> groupRows = new Collection<DataBoundRow>();

        /// <summary>
        /// Member variable to hold group header.
        /// </summary>
        private UIElement groupingHeader;

        /// <summary>
        /// Instantiates a new GroupingRenderEventArgs object. 
        /// </summary>
        public GroupingRenderEventArgs()
        {
        }

        /// <summary>
        /// Instantiates a new GroupingRenderEventArgs object with the specified parameters.
        /// </summary>
        /// <param name="groupingKey">Grouping key.</param>
        /// <param name="groupRows">Rows within the group.</param>
        /// <param name="groupingHeader">Group header element.</param>
        public GroupingRenderEventArgs(string groupingKey, Collection<DataBoundRow> groupRows, UIElement groupingHeader)
        {
            this.groupingHeader = groupingHeader;
            this.groupingKey = groupingKey;
            this.groupRows = groupRows;
        }

        /// <summary>
        /// Gets or rows within the group.
        /// </summary>
        /// <value>Group rows.</value>
        public Collection<DataBoundRow> GroupRows
        {
            get 
            {
                return this.groupRows; 
            }            
        }

        /// <summary>
        /// Gets the grouping key of the group.
        /// </summary>
        /// <value>Grouping key.</value>
        public string GroupingKey
        {
            get 
            { 
                return this.groupingKey; 
            }            
        }

        /// <summary>
        /// Gets the group header element within the group.
        /// </summary>
        /// <value>Grouping header.</value>
        public UIElement GroupingHeader
        {
            get 
            { 
                return this.groupingHeader; 
            }            
        }
    }
}
