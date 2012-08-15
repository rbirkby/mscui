//-----------------------------------------------------------------------
// <copyright file="ReorderedEventArgs.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>30-Oct-2008</date>
// <summary>Event arguments containing information about the reorder.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;

    /// <summary>
    /// Event arguments containing information about the reorder.
    /// </summary>
    public class ReorderedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes an instance of ReorderedEventArgs.
        /// </summary>
        public ReorderedEventArgs()
        {
            this.OldIndex = -1;
            this.NewIndex = -1;
        }

        /// <summary>
        /// Initializes an instance of ReorderedEventArgs.
        /// </summary>
        /// <param name="oldIndex">Current index of the element.</param>
        /// <param name="newIndex">New index of the element.</param>
        public ReorderedEventArgs(int oldIndex, int newIndex)
        {
            this.OldIndex = oldIndex;
            this.NewIndex = newIndex;
        }

        /// <summary>
        /// Gets the current index of the element.
        /// </summary>
        /// <value>Current index of the element.</value>
        public int OldIndex
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the new index of the element.
        /// </summary>
        /// <value>New index to which the item is being moved to.</value>
        public int NewIndex
        {
            get;
            internal set;
        }
    }
}
