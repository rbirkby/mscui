//-----------------------------------------------------------------------
// <copyright file="AdditionalTextBoxParseCompletedEventArgs.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>09/12/2008</date>
// <summary>Contains the data required to hightlight terms in the additional textbox.</summary>
//-----------------------------------------------------------------------
#if SILVERLIGHT
namespace Microsoft.Cui.SamplePages
#else
namespace Microsoft.Cui.SampleWinform
#endif
{
    #region Using

    using System;
    using System.Collections.ObjectModel;
#if !SILVERLIGHT
    using Microsoft.Cui.SampleWinform.TerminologyProvider;    
#else

#endif

    #endregion
    /// <summary>
    /// Represents the results returned from a terminology provider when parsing the additional text box contents.
    /// </summary>
    public class AdditionalTextBoxParseCompletedEventArgs : BaseTerminologyEventArgs
    {
        /// <summary>
        /// Backing field for additional Text Box Results.
        /// </summary>
        private ObservableCollection<AdditionalTextBoxResult> additionalTextBoxResults;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalTextBoxParseCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="searchText">The original search text.</param>
        /// <param name="additionalTextBoxResults">The additional text box results.</param>
        /// <param name="mainConceptId">The main concept ID.</param>
        public AdditionalTextBoxParseCompletedEventArgs(string searchText, ObservableCollection<AdditionalTextBoxResult> additionalTextBoxResults, string mainConceptId)
        {
            this.SearchTextOriginal = searchText;
            this.additionalTextBoxResults = additionalTextBoxResults;
            this.MainConceptId = mainConceptId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalTextBoxParseCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="searchTextOriginal">The original search text.</param>
        /// <param name="successful">If set to <c>true</c> [successful].</param>
        /// <param name="mainConceptId">The main concept ID.</param>
        public AdditionalTextBoxParseCompletedEventArgs(string searchTextOriginal, bool successful, string mainConceptId)
            : base(successful)
        {
            this.SearchTextOriginal = searchTextOriginal;
            this.MainConceptId = mainConceptId;
        }

        /// <summary>
        /// Gets the additional text box results.
        /// </summary>
        /// <value>The additional text box results.</value>
        public ObservableCollection<AdditionalTextBoxResult> AdditionalTextBoxResults 
        {
            get
            {
                return this.additionalTextBoxResults;
            }            
        }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>The search text.</value>
        public string SearchTextOriginal { get; set; }

        /// <summary>
        /// Gets or sets the main concept ID.
        /// </summary>
        public string MainConceptId { get; set; }
    }
}
