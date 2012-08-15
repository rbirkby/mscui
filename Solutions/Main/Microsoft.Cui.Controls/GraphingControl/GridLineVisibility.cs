//-----------------------------------------------------------------------
// <copyright file="GridLineVisibility.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>24-Sep-2008</date>
// <summary>The GridLineVisibility enumeration.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    /// <summary>
    /// Enumeration to specify the grid lines visibility.
    /// </summary>
    public enum GridLineVisibility
    {
        /// <summary>
        /// No grid lines.
        /// </summary>
        None,

        /// <summary>
        /// Horizontal grid lines only.
        /// </summary>
        Horizontal,

        /// <summary>
        /// Vertical grid lines only.
        /// </summary>
        Vertical,

        /// <summary>
        /// Both horizontal and vertical grid lines.
        /// </summary>
        Both
    }
}
