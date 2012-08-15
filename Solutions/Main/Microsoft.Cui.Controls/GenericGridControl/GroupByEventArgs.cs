//-----------------------------------------------------------------------
// <copyright file="GroupByEventArgs.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The GroupBy event arguments class. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;

    /// <summary>
    /// Implementation of GroupByEventArgs.
    /// </summary>
    public class GroupByEventArgs : EventArgs
    {
        /// <summary>
        /// Member variable to hold text.
        /// </summary>
        private string text;

        /// <summary>
        /// Member variable to hold tag.
        /// </summary>
        private string tag;

        /// <summary>
        /// Instantiates a new GroupByEventArgs object with the specified text and tag.
        /// </summary>
        /// <param name="text">Group by column name.</param>
        /// <param name="tag">Group by column tag.</param>
        public GroupByEventArgs(string text, string tag)
        {
            this.text = text;
            this.tag = tag;
        }

        /// <summary>
        /// Gets the column name on which the grouping is to be done.
        /// </summary>
        /// <value>Text of column.</value>
        public string Text
        {
            get
            {
                return this.text;
            }
        }

        /// <summary>
        /// Gets the column tag on which the grouping is to be done.
        /// </summary>
        /// <value>Tag of column.</value>
        public string Tag
        {
            get
            {
                return this.tag;
            }
        }
    }
}
