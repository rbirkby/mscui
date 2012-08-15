//-----------------------------------------------------------------------
// <copyright file="ColumnHeaderClickEventArgs.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>22-Feb-2008</date>
// <summary>The ColumnHeaderClickEventArgs event arguments class. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;

    /// <summary>
    /// Implementation of the ColumnHeaderClickEventArgs class which is used while handling the ColumnHeaderClick event.
    /// </summary>
    public class ColumnHeaderClickEventArgs : EventArgs
    {
        /// <summary>
        /// The index of the column whose header was clicked.
        /// </summary>
        private readonly int columnIndex;

        /// <summary>
        /// The ColumnManager whose header was clicked.
        /// </summary>
        private readonly ColumnManager columnManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnHeaderClickEventArgs"/> class.
        /// </summary>
        /// <param name="columnIndex">Index of the column whose header was clicked.</param>
        /// <param name="columnManager">The ColumnManager that was clicked.</param>
        public ColumnHeaderClickEventArgs(int columnIndex, ColumnManager columnManager)
        {
            this.columnIndex = columnIndex;
            this.columnManager = columnManager;
        }

        /// <summary>
        /// Gets the index of the clicked column header.
        /// </summary>
        /// <value>The index of the column.</value>
        public int ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        /// <summary>
        /// Gets the ColumnManager whose header was clicked.
        /// </summary>
        /// <value>Column manager.</value>
        public ColumnManager ColumnManager
        {
            get
            {
                return this.columnManager;
            }
        }
    }
}
