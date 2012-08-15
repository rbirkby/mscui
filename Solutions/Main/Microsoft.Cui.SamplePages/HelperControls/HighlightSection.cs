//-----------------------------------------------------------------------
// <copyright file="HighlightSection.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>13-Aug-2009</date>
// <summary>Class to represent a number of characters to highlight.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.SamplePages
{
    /// <summary>
    /// Class to represent a number of characters to highlight.
    /// </summary>
    public class HighlightSection
    {
        /// <summary>
        /// Initializes a new instance of the HighlightSection class.
        /// </summary>
        /// <param name="termStartIndex">The index to start this highlight at</param>
        /// <param name="termIsSelected">Value indicating whether the highlight is selected</param>
        /// <param name="length">The number of characters to highlight</param>
        public HighlightSection(int termStartIndex, bool termIsSelected, int length)
        {
            this.StartIndex = termStartIndex;
            this.Length = length;
            this.Selected = termIsSelected;
        }

        /// <summary>
        /// Gets or sets the index to start the highlight at
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the highlight is selected
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// Gets or sets the number of characters to highlight
        /// </summary>
        public int Length { get; set; }
    }
}
