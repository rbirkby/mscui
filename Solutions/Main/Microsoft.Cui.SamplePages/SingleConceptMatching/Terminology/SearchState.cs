//-----------------------------------------------------------------------
// <copyright file="SearchState.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>20-Jan-2008</date>
// <summary>Represents the user state during a search operation.</summary>
//-----------------------------------------------------------------------
#if SILVERLIGHT
namespace Microsoft.Cui.SamplePages
#else
namespace Microsoft.Cui.SampleWinform
#endif
{
    #region Using
    using System.Collections.ObjectModel;
    using System.Text.RegularExpressions;
#if SILVERLIGHT
    using Microsoft.Cui.SamplePages.TerminologyProvider;
#else
    using Microsoft.Cui.SampleWinform.TerminologyProvider;
#endif

    #endregion
    /// <summary>
    /// Represents the user state during a search operation.
    /// </summary>
    internal class SearchState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchState"/> class.
        /// </summary>
        public SearchState()
        {
            this.LateralityFindingSites = new ObservableCollection<ConceptDetail>();
        }

        #region Public Properties       

        /// <summary>
        /// Gets or sets the state of the user.
        /// </summary>
        /// <value>The state of the user.</value>
        public UserState UserState { get; set; }

        /// <summary>
        /// Gets or sets the original search text.
        /// </summary>
        /// <value>The original search text.</value>
        public string SearchTextOriginal { get; set; }

        /// <summary>
        /// Gets or sets the domains.
        /// </summary>
        /// <value>The domains.</value>
        public ObservableCollection<string> Domains { get; set; }

        /// <summary>
        /// Gets or sets the original results.
        /// </summary>
        /// <value>The original results.</value>
        public SearchResult OriginalSearchResults { get; set; }

        /// <summary>
        /// Gets or sets the input field result.
        /// </summary>
        /// <value>The input field result.</value>
        public InputFieldResult InputFieldResult { get; set; }

        /// <summary>
        /// Gets or sets the additional text box results.
        /// </summary>
        /// <value>The additional text box results.</value>
        public ObservableCollection<AdditionalTextBoxResult> AdditionalTextBoxResults { get; set; }

        /// <summary>
        /// Gets or sets the remaining items to find.
        /// </summary>
        /// <value>The remaining items to find.</value>
        public int RemainingItemsToFind { get; set; }

        /// <summary>
        /// Gets or sets the encoded concept.
        /// </summary>
        /// <value>The encoded concept.</value>
        public EncodedConcept EncodedConcept { get; set; }
                
        /// <summary>
        /// Gets or sets a concept detail object.
        /// </summary>
        /// <value>The concept detail.</value>
        public ConceptDetail ConceptDetail { get; set; }

        /// <summary>
        /// Gets or sets the list of refinable characteristics to search for lateralities.
        /// </summary>
        /// <value>The refinable characteristics.</value>
        public RefinableCharacteristic RefinableCharacteristic { get; set; }

        /// <summary>
        /// Gets or sets the finding site snomed concept id for laterality.
        /// </summary>
        /// <value>The finding site snomed concept id.</value>
        public ObservableCollection<ConceptDetail> LateralityFindingSites { get; set; }

        #endregion
    }        
}
