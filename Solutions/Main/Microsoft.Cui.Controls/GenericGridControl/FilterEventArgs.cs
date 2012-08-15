//-----------------------------------------------------------------------
// <copyright file="FilterEventArgs.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The FilterBy event arguments class. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;
    
    /// <summary>
    /// Implementation of FilterEventArgs.
    /// </summary>
    public class FilterEventArgs : EventArgs
    {
        /// <summary>
        /// Member variable to hold filter condition.
        /// </summary>
        private string filterCondition;

        /// <summary>
        /// Instantiates a new FilterEventArgs object with the specified filter condition.
        /// </summary>
        /// <param name="filterCondition">Filter condition.</param>
        public FilterEventArgs(string filterCondition)
        {
            this.filterCondition = filterCondition;
        }

        /// <summary>
        /// Gets the filter condition.
        /// </summary>
        /// <value>Filter condition.</value>
        public string FilterCondition
        {
            get 
            { 
                return this.filterCondition; 
            }
        }
    }
}
