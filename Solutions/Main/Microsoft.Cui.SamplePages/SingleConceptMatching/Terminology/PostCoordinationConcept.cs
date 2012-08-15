//-----------------------------------------------------------------------
// <copyright file="PostCoordinationConcept.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>25-Jan-2009</date>
// <summary>Represents a post coordinated concept.</summary>
//-----------------------------------------------------------------------
#if SILVERLIGHT
namespace Microsoft.Cui.SamplePages
#else
namespace Microsoft.Cui.SampleWinform
#endif
{
    #region Using

#if SILVERLIGHT
    using Microsoft.Cui.SamplePages.TerminologyProvider;
#else
    using Microsoft.Cui.SampleWinform.TerminologyProvider;
#endif

    #endregion

    /// <summary>
    /// Represents a post coordinated concept.
    /// </summary>
    public class PostCoordinationConcept
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostCoordinationConcept"/> class.
        /// </summary>
        public PostCoordinationConcept()
        {
            this.ConceptDetail = new ConceptDetail();            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PostCoordinationConcept"/> class.
        /// </summary>
        /// <param name="conceptDetail">The concept detail.</param>
        /// <param name="isInitialized">If set to <c>true</c> [is initialized].</param>
        public PostCoordinationConcept(ConceptDetail conceptDetail, bool isInitialized)
        {
            this.ConceptDetail = conceptDetail;
            this.IsInitialized = isInitialized;
        }

        /// <summary>
        /// Gets or sets the concept detail.
        /// </summary>
        /// <value>The concept detail.</value>
        public ConceptDetail ConceptDetail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>Is <c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        public bool IsInitialized { get; set; }
    }
}
