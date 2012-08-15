//-----------------------------------------------------------------------
// <copyright file="TerminologyManager.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>A set of helper methods for searching.</summary>
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
    using System.Globalization;
    using System.Linq;
    using System.Text;
#if SILVERLIGHT
    using Microsoft.Cui.SamplePages.TerminologyProvider;
#else
    using Microsoft.Cui.SampleWinform.TerminologyProvider;
#endif

    #endregion

    /// <summary>
    /// A set of helper methods for searching.
    /// </summary>
    public static class TerminologyManager
    {
        #region Internal Const Fields

        /// <summary>
        /// Locale string.
        /// </summary>
        internal const string LocaleString = "en";

        #endregion

        #region Private Const Fields

        /// <summary>
        /// Qualifiers Domain String.
        /// </summary>
        private const string QualifiersDomainString = "QUALIFIERSMALL";

        /// <summary>
        /// Negation String.
        /// </summary>
        private const string NegationString = "NO";

        /// <summary>
        /// Negation Display String.
        /// </summary>
        private const string NegationDisplayString = "No";

        /// <summary>
        /// Prefix Search String.
        /// </summary>
        private const string PrefixSearchString = "*";

        /// <summary>
        /// Character used to allow wildcard searching on terms.
        /// </summary>
        private const char WildcardCharacter = '*';

        /// <summary>
        /// Number of terms to return on a SearchByDescription search.
        /// </summary>
        private const int MaximumNumberOfTerms = 100;
               
        /// <summary>
        /// The minimum length of a term in the input field.
        /// </summary>
        private const int MinimumSearchTermLength = 3;

        #endregion

        #region Private Static Fields

        /// <summary>
        /// Static backing field for TerminologyProviderClient property.
        /// </summary>
        private static TerminologyProviderClient terminologyProviderClient;

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when [initialized].
        /// </summary>
        public static event EventHandler<BaseTerminologyEventArgs> Initialized;

        /// <summary>
        /// Occurs when [input box search completed].
        /// </summary>
        public static event EventHandler<InputFieldSearchCompletedEventArgs> InputBoxSearchCompleted;

        /// <summary>
        /// Occurs when [additional text box parse completed].
        /// </summary>
        public static event EventHandler<AdditionalTextBoxParseCompletedEventArgs> AdditionalTextBoxParseCompleted;
        
        /// <summary>
        /// Occurs when [encode concept completed].
        /// </summary>
        public static event EventHandler<EncodeConceptCompletedEventArgs> EncodeConceptCompleted;

        /// <summary>
        /// Occurs when [get concept details completed].
        /// </summary>
        public static event EventHandler<GetConceptDetailsCompletedEventArgs> GetConceptDetailsCompleted;

        #endregion                

        #region Public Static Properties
        
        /// <summary>
        /// Gets or sets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>Is <c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        public static bool IsInitialized { get; set; }        

        #endregion

        #region Private Static Properties

        /// <summary>
        /// Gets or sets the terminology provider client.
        /// </summary>
        /// <value>The terminology provider client.</value>
        private static TerminologyProviderClient TerminologyProviderClient
        {
            get
            {
                return TerminologyManager.terminologyProviderClient;
            }

            set
            {
                if (TerminologyManager.terminologyProviderClient != null)
                {
                    TerminologyManager.terminologyProviderClient.SearchByDescriptionCompleted -= new System.EventHandler<SearchByDescriptionCompletedEventArgs>(TerminologyManager.TerminologyProviderClientSearchByDescriptionCompleted);
                    TerminologyManager.terminologyProviderClient.IndexTextCompleted -= new EventHandler<IndexTextCompletedEventArgs>(TerminologyManager.TerminologyProviderClientIndexTextCompleted);
                    TerminologyManager.terminologyProviderClient.GetConceptDetailsCompleted -= new System.EventHandler<GetConceptDetailsCompletedEventArgs>(TerminologyManager.TerminologyProviderClientGetConceptDetailsCompleted);
                }

                TerminologyManager.terminologyProviderClient = value;

                if (TerminologyManager.terminologyProviderClient != null)
                {
                    TerminologyManager.terminologyProviderClient.SearchByDescriptionCompleted += new System.EventHandler<SearchByDescriptionCompletedEventArgs>(TerminologyManager.TerminologyProviderClientSearchByDescriptionCompleted);
                    TerminologyManager.terminologyProviderClient.IndexTextCompleted += new EventHandler<IndexTextCompletedEventArgs>(TerminologyManager.TerminologyProviderClientIndexTextCompleted);
                    TerminologyManager.terminologyProviderClient.GetConceptDetailsCompleted += new System.EventHandler<GetConceptDetailsCompletedEventArgs>(TerminologyManager.TerminologyProviderClientGetConceptDetailsCompleted);
                }
            }
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Initialises this instance.
        /// </summary>
        public static void Initialize()
        {
            TerminologyManager.TerminologyProviderClient = new TerminologyProviderClient();

            SnomedConcepts.Initialized += new EventHandler<BaseTerminologyEventArgs>(SnomedConcepts_Initialized);
            SnomedConcepts.Initialize();            
        }

        /// <summary>
        /// Searches the input field.
        /// </summary>
        /// <param name="text">The text to be searched.</param>
        /// <param name="domains">The domains the Terminology Provider supports.</param>
        /// <returns>true if the call was executed.</returns>
        public static bool SearchInputField(string text, ObservableCollection<string> domains)
        {
            SearchState searchState = new SearchState();
            searchState.UserState = UserState.Normal;
            searchState.Domains = domains;   

            string normalisedSearchString = TerminologyManager.NormaliseTerm(text);

            if (TerminologyManager.ContainsNegative(normalisedSearchString))
            {
                searchState.UserState = UserState.Negated;
                normalisedSearchString = normalisedSearchString.Replace(TerminologyManager.NegationString, string.Empty);
            }

            searchState.SearchTextOriginal = text;

            terminologyProviderClient.SearchByDescriptionAsync(normalisedSearchString, domains, null, false, true, TerminologyManager.LocaleString, 1, TerminologyManager.MaximumNumberOfTerms, TerminologyManager.MaximumNumberOfTerms + 1, searchState);

            return true;
        }

        /// <summary>
        /// Parses the additional text box.
        /// </summary>
        /// <param name="text">The text to parse for qualifiers.</param>
        /// <param name="inputFieldResult">The input field result.</param>
        /// <param name="conceptDetail">The concept detail.</param>
        public static void ParseAdditionalTextBox(string text, InputFieldResult inputFieldResult, ConceptDetail conceptDetail)
        {           
#if SILVERLIGHT
            string normalisedSearchString = text.ToUpper(CultureInfo.InvariantCulture);
#else
            string normalisedSearchString = text.ToUpperInvariant();
#endif
            ObservableCollection<string> domains = new ObservableCollection<string>();

            domains.Add(TerminologyManager.QualifiersDomainString);

            SearchState searchState = new SearchState();
            searchState.SearchTextOriginal = text;
            searchState.ConceptDetail = conceptDetail;
            searchState.InputFieldResult = inputFieldResult;

            if (conceptDetail.RefinableCharacteristics.Count > 0)
            {
                TerminologyManager.terminologyProviderClient.IndexTextAsync(normalisedSearchString, domains, null, false, TerminologyManager.LocaleString, searchState);
            }
            else
            {
                TerminologyManager.RaiseAdditionalTextBoxCompleted(new ObservableCollection<AdditionalTextBoxResult>(), searchState);
            }
        }

        /// <summary>
        /// Gets the concept details.
        /// </summary>
        /// <param name="snomedConceptId">The snomed concept id.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "snomed", Justification = "This is the correct spelling of snomed.")]
        public static void GetConceptDetails(string snomedConceptId)
        {
            TerminologyProviderClient terminologyProviderClient = new TerminologyProviderClient();

            terminologyProviderClient.GetConceptDetailsCompleted += delegate(object sender, GetConceptDetailsCompletedEventArgs e)
            {
                if (e.Error == null && e.Result != null)
                {
                    if (TerminologyManager.GetConceptDetailsCompleted != null)
                    {
                        TerminologyManager.GetConceptDetailsCompleted(null, e);
                    }
                }
            };

            terminologyProviderClient.GetConceptDetailsAsync(snomedConceptId, TerminologyManager.LocaleString);
        }

        /// <summary>
        /// Encodes the concept.
        /// </summary>
        /// <param name="inputFieldResult">The input field result.</param>
        /// <param name="additionalTextBoxResults">The additional text box results.</param>
        public static void EncodeConcept(InputFieldResult inputFieldResult, ObservableCollection<AdditionalTextBoxResult> additionalTextBoxResults)
        {
            SearchState searchState = new SearchState();

            searchState.InputFieldResult = inputFieldResult;            
            searchState.AdditionalTextBoxResults = additionalTextBoxResults;
            searchState.UserState = UserState.EncodingMainConcept;

            terminologyProviderClient.GetConceptDetailsAsync(inputFieldResult.Concept.SnomedConceptId, TerminologyManager.LocaleString, searchState);
        }

        #endregion

        #region Private Static Methods
        
        /// <summary>
        /// Handles the Initialized event of the SnomedConcepts object.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="BaseTerminologyEventArgs"/> instance containing the event data.</param>
        private static void SnomedConcepts_Initialized(object sender, BaseTerminologyEventArgs e)
        {
            if (TerminologyManager.Initialized != null)
            {
                TerminologyManager.Initialized(null, e);
            }
        } 

        /// <summary>
        /// Takes the search term, splits it on a space character, adds a wildcard rejoins the terms and converts them to an uppercase culture invariant string.
        /// </summary>
        /// <param name="text">The text to convert to a normalised term.</param>
        /// <returns>A culture invariant uppercase string containing each word in the original search term with a wildcard at the end.</returns>
        private static string NormaliseTerm(string text)
        {
#if SILVERLIGHT
            string uppercaseTerm = text.ToUpper(CultureInfo.InvariantCulture);
#else
            string uppercaseTerm = text.ToUpperInvariant();
#endif
            StringBuilder stringBuilder = new StringBuilder();

            string[] splitString = uppercaseTerm.Split(new char[] { ' ' });

            foreach (string s in splitString)
            {
                if (s == TerminologyManager.NegationString)
                {
                    stringBuilder.Append(s + " ");
                }
                else
                {
                    stringBuilder.Append(s + "* ");
                }
            }

#if SILVERLIGHT
            return stringBuilder.ToString().Trim();
#else
            return stringBuilder.ToString();
#endif
        }       

        /// <summary>
        /// Handles the get concept details completed event for the Terminologies Provider Client.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GetConceptDetailsCompletedEventArgs"/> instance containing the event data.</param>
        private static void TerminologyProviderClientGetConceptDetailsCompleted(object sender, GetConceptDetailsCompletedEventArgs e)
        {
            if (e.Error == null && e.Result != null)
            {
                if (e.UserState is SearchState)
                {
                    SearchState searchState = e.UserState as SearchState;                  

                    switch (searchState.UserState)
                    {
                        case UserState.EncodingMainConcept:
                            TerminologyManager.EncodeMainConcept(e.Result, searchState);
                            break;
                        case UserState.EncodingAdditionalConcept:
                            searchState.ConceptDetail = e.Result;
                            TerminologyManager.EncodeAdditionalConcept(searchState);  
                            break;
                        case UserState.SearchingFindingSites:
                            RefinableCharacteristic lateralityRefChar = e.Result.RefinableCharacteristics.SingleOrDefault(p => p.Name == "Laterality");

                            searchState.RemainingItemsToFind--;

                            int index = searchState.RefinableCharacteristic.ValueConcepts.Count - searchState.RemainingItemsToFind;

                            if (lateralityRefChar == null)
                            {                                
                                if (searchState.RemainingItemsToFind > 0)
                                {
                                    TerminologyManager.terminologyProviderClient.GetConceptDetailsAsync(searchState.RefinableCharacteristic.ValueConcepts[index].SnomedConceptId, TerminologyManager.LocaleString, searchState);
                                }
                                else
                                {
                                    TerminologyManager.RaiseAdditionalTextBoxCompleted(searchState.AdditionalTextBoxResults, searchState);                        
                                }
                            }
                            else
                            {
                                searchState.LateralityFindingSites.Add(e.Result);

                                if (searchState.RemainingItemsToFind > 0)
                                {
                                    TerminologyManager.terminologyProviderClient.GetConceptDetailsAsync(searchState.RefinableCharacteristic.ValueConcepts[index].SnomedConceptId, TerminologyManager.LocaleString, searchState);                               
                                }
                                else
                                {
                                    TerminologyManager.RaiseAdditionalTextBoxCompleted(searchState.AdditionalTextBoxResults, searchState);
                                }
                            }
                           
                            break;
                        default:
                            break;
                    }

                    switch (searchState.UserState)
                    {                        
                        case UserState.EncodingMainConcept:                            
                        case UserState.EncodingAdditionalConcept:
                            if (searchState.AdditionalTextBoxResults == null || searchState.AdditionalTextBoxResults.Count == 0)
                            {
                                if (TerminologyManager.EncodeConceptCompleted != null)
                                {
                                    TerminologyManager.EncodeConceptCompleted(null, new EncodeConceptCompletedEventArgs(searchState.EncodedConcept));
                                }
                            }

                            break;                       
                    }
                }
            }
            else
            {
                if (TerminologyManager.EncodeConceptCompleted != null)
                {
                    TerminologyManager.EncodeConceptCompleted(null, new EncodeConceptCompletedEventArgs(false));
                }
            }
        }

        /// <summary>
        /// Encodes an additional concept.
        /// </summary>
        /// <param name="searchState">State of the search.</param>
        private static void EncodeAdditionalConcept(SearchState searchState)
        {
            AttributeValuePair attributeValuePair = new AttributeValuePair(TerminologyManager.ConceptDetailFromConceptItem(searchState.ConceptDetail.Parents[0]), new EncodedConcept(searchState.ConceptDetail));

            if (searchState.AdditionalTextBoxResults[0].IsSide)
            {             
                foreach (ConceptDetail conceptDetail in searchState.AdditionalTextBoxResults[0].FindingSites)
                {
                    AttributeValuePair lateralityAttributeValuePair = new AttributeValuePair(SnomedConcepts.FindingSite, new EncodedConcept(conceptDetail));
                    lateralityAttributeValuePair.Value.AttributeCollection.Add(new AttributeValuePair(SnomedConcepts.Laterality, new EncodedConcept(searchState.AdditionalTextBoxResults[0].SelectedItem)));

                    if (searchState.InputFieldResult.IsNegated)
                    {
                        if (searchState.EncodedConcept.AttributeCollection.Count > 1)
                        {
                            searchState.EncodedConcept.AttributeCollection[1].Value.AttributeCollection.Add(lateralityAttributeValuePair);
                        }
                    }
                    else
                    {
                        searchState.EncodedConcept.AttributeCollection.Add(lateralityAttributeValuePair);
                    }
                }
            }
            else
            {
                if (searchState.InputFieldResult.IsNegated)
                {
                    searchState.EncodedConcept.AttributeCollection[searchState.EncodedConcept.AttributeCollection.Count - 1].Value.AttributeCollection.Add(attributeValuePair);
                }
                else
                {
                    searchState.EncodedConcept.AttributeCollection.Add(attributeValuePair);
                }
            }

            searchState.UserState = UserState.EncodingAdditionalConcept;

            searchState.AdditionalTextBoxResults.RemoveAt(0);

            if (searchState.AdditionalTextBoxResults != null && searchState.AdditionalTextBoxResults.Count > 0)
            {
                AdditionalTextBoxResult additionalTextBoxResult = searchState.AdditionalTextBoxResults[0];

                terminologyProviderClient.GetConceptDetailsAsync(additionalTextBoxResult.SelectedItem.SnomedConceptId, TerminologyManager.LocaleString, searchState);
            }
        }

        /// <summary>
        /// Encodes the laterality concept detail as a situation with explicit context.
        /// </summary>
        /// <param name="conceptDetail">The concept detail.</param>
        /// <param name="searchState">State of the search.</param>
        /// <param name="lateralityConceptDetail">The laterality concept detail.</param>
        private static void EncodeLaterality(ConceptDetail conceptDetail, SearchState searchState, ConceptDetail lateralityConceptDetail)
        {        
            AttributeValuePair lateralityAttributeValuePair = new AttributeValuePair(SnomedConcepts.FindingSite, new EncodedConcept(conceptDetail));
            lateralityAttributeValuePair.Value.AttributeCollection.Add(new AttributeValuePair(SnomedConcepts.Laterality, new EncodedConcept(lateralityConceptDetail)));

            if (searchState.InputFieldResult.IsNegated)
            {
                if (searchState.EncodedConcept.AttributeCollection.Count > 1)
                {
                    searchState.EncodedConcept.AttributeCollection[1].Value.AttributeCollection.Add(lateralityAttributeValuePair);
                }
            }
            else
            {
                searchState.EncodedConcept.AttributeCollection.Add(lateralityAttributeValuePair);
            }

            searchState.UserState = UserState.EncodingAdditionalConcept;

            searchState.AdditionalTextBoxResults.RemoveAt(0);

            if (searchState.AdditionalTextBoxResults != null && searchState.AdditionalTextBoxResults.Count > 0)
            {
                AdditionalTextBoxResult additionalTextBoxResult = searchState.AdditionalTextBoxResults[0];

                terminologyProviderClient.GetConceptDetailsAsync(additionalTextBoxResult.SelectedItem.SnomedConceptId, TerminologyManager.LocaleString, searchState);
            }
        }

        /// <summary>
        /// Encodes the main concept.
        /// </summary>
        /// <param name="conceptDetail">The concept detail.</param>
        /// <param name="searchState">State of the search.</param>
        private static void EncodeMainConcept(ConceptDetail conceptDetail, SearchState searchState)
        {
            searchState.EncodedConcept = new EncodedConcept(SnomedConcepts.Situation);

            if (searchState.InputFieldResult.IsNegated)
            {                
                searchState.EncodedConcept.DisplayName = TerminologyManager.NegationDisplayString + " " + searchState.InputFieldResult.Description;
                searchState.EncodedConcept.AttributeCollection.Add(new AttributeValuePair(SnomedConcepts.FindingContext, new EncodedConcept(SnomedConcepts.KnownAbsent)));
                searchState.EncodedConcept.AttributeCollection.Add(new AttributeValuePair(SnomedConcepts.AssociatedFinding, new EncodedConcept(conceptDetail)));                
                searchState.UserState = UserState.EncodingAdditionalConcept;
            }
            else
            {
                searchState.EncodedConcept.EncodedSingleConcept = conceptDetail;
                searchState.EncodedConcept.DisplayName = searchState.InputFieldResult.Description;
                searchState.UserState = UserState.EncodingAdditionalConcept;
            }

            if (searchState.AdditionalTextBoxResults != null && searchState.AdditionalTextBoxResults.Count > 0)
            {
                AdditionalTextBoxResult additionalTextBoxResult = searchState.AdditionalTextBoxResults[0];

                terminologyProviderClient.GetConceptDetailsAsync(additionalTextBoxResult.SelectedItem.SnomedConceptId, TerminologyManager.LocaleString, searchState);
            }
        }

        /// <summary>
        /// Handles the index text completed event for the Terminology Provider Client.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="IndexTextCompletedEventArgs"/> instance containing the event data.</param>
        private static void TerminologyProviderClientIndexTextCompleted(object sender, IndexTextCompletedEventArgs e)
        {
            SearchState searchState = null;

            if (e.Error == null && e.Result != null && (searchState = e.UserState as SearchState) != null)
            {
                bool containsSide = false;               
                
                ObservableCollection<AdditionalTextBoxResult> additionalTextBoxResults = new ObservableCollection<AdditionalTextBoxResult>();

                foreach (IndexerResult indexerResult in e.Result)
                { 
                    AdditionalTextBoxResult additionalTextBoxResult = new AdditionalTextBoxResult();

                    additionalTextBoxResult.SelectedItem = indexerResult.Concept;
                    additionalTextBoxResult.StartIndex = indexerResult.StartIndex;
                    additionalTextBoxResult.Length = indexerResult.EndIndex - indexerResult.StartIndex;
                    additionalTextBoxResult.IsSide = TerminologyManager.IsConceptSide(indexerResult.Concept);

                    if (additionalTextBoxResult.IsSide)
                    {
                        containsSide = true;
                    }
                    else
                    {
                        ConceptItem foundConcept = null;

                        foreach (RefinableCharacteristic refinableCharacteristic in searchState.ConceptDetail.RefinableCharacteristics)
                        {
                            foundConcept = refinableCharacteristic.ValueConcepts.SingleOrDefault(p => p.SnomedConceptId == indexerResult.Concept.SnomedConceptId);

                            if (foundConcept != null)
                            {
                                break;
                            }
                        }

                        if (foundConcept == null)
                        {
                            continue;
                        }
                    }

                    additionalTextBoxResults.Add(additionalTextBoxResult);
                }

                if (!containsSide)
                {
                    TerminologyManager.RaiseAdditionalTextBoxCompleted(additionalTextBoxResults, searchState);
                }
                else
                {
                    TerminologyProviderClient terminologyProviderClient = new TerminologyProviderClient();

                    terminologyProviderClient.GetConceptDetailsCompleted += delegate(object newSender, GetConceptDetailsCompletedEventArgs newEventArgs)
                    {
                        searchState = newEventArgs.UserState as SearchState;                        
                    };

                    if (searchState.InputFieldResult != null)
                    {
                        searchState.AdditionalTextBoxResults = additionalTextBoxResults;

                        RefinableCharacteristic refChar = searchState.ConceptDetail.RefinableCharacteristics.SingleOrDefault(p => p.Name == "Finding site");

                        if (refChar == null)
                        {
                            TerminologyManager.RaiseAdditionalTextBoxCompleted(searchState.AdditionalTextBoxResults, searchState);
                        }
                        else
                        {
                            searchState.UserState = UserState.SearchingFindingSites;                            
                            searchState.RemainingItemsToFind = refChar.ValueConcepts.Count;
                            searchState.AdditionalTextBoxResults = additionalTextBoxResults;
                            searchState.RefinableCharacteristic = refChar;

                            TerminologyManager.terminologyProviderClient.GetConceptDetailsAsync(refChar.ValueConcepts[0].SnomedConceptId, TerminologyManager.LocaleString, searchState);
                        }
                    }
                }
            }
            else
            {
                if (TerminologyManager.AdditionalTextBoxParseCompleted != null)
                {
                    if (searchState != null)
                    {
                        TerminologyManager.AdditionalTextBoxParseCompleted(null, new AdditionalTextBoxParseCompletedEventArgs(searchState.SearchTextOriginal, false, searchState.InputFieldResult.Concept.SnomedConceptId));
                    }
                    else
                    {
                        TerminologyManager.AdditionalTextBoxParseCompleted(null, new AdditionalTextBoxParseCompletedEventArgs(string.Empty, false, string.Empty));               
                    }
                }
            }
        }

        /// <summary>
        /// Raises the additional text box completed event and doesn't strip laterality results.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="searchState">User state of search.</param>
        private static void RaiseAdditionalTextBoxCompleted(ObservableCollection<AdditionalTextBoxResult> results, SearchState searchState)
        {
            ObservableCollection<AdditionalTextBoxResult> newResults = new ObservableCollection<AdditionalTextBoxResult>();

            foreach (AdditionalTextBoxResult additionalTextBoxResult in results)
            {
                if (additionalTextBoxResult.IsSide && searchState.LateralityFindingSites.Count > 0)
                {
                    additionalTextBoxResult.FindingSites = searchState.LateralityFindingSites;
                    additionalTextBoxResult.AlternateItems = SnomedConcepts.FindPostCoordinationConcept(additionalTextBoxResult.SelectedItem.Parents[0].SnomedConceptId).Children;
                    newResults.Add(additionalTextBoxResult);
                }
                else if (!additionalTextBoxResult.IsSide)
                {
                    newResults.Add(additionalTextBoxResult);
                    additionalTextBoxResult.AlternateItems = SnomedConcepts.FindPostCoordinationConcept(additionalTextBoxResult.SelectedItem.Parents[0].SnomedConceptId).Children;
                }
            }

            if (TerminologyManager.AdditionalTextBoxParseCompleted != null)
            {
                TerminologyManager.AdditionalTextBoxParseCompleted(null, new AdditionalTextBoxParseCompletedEventArgs(searchState.SearchTextOriginal, newResults, searchState.ConceptDetail.SnomedConceptId));
            }
        }       

        /// <summary>
        /// Handles the search by description completed event for the Terminology Provider Client.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SearchByDescriptionCompletedEventArgs"/> instance containing the event data.</param>
        private static void TerminologyProviderClientSearchByDescriptionCompleted(object sender, SearchByDescriptionCompletedEventArgs e)
        {
            if (e.Error == null && e.Result != null && e.Result.Terms != null)
            {                
                if (e.UserState != null && e.UserState is SearchState)
                {
                    SearchState searchState = e.UserState as SearchState;

                    switch (searchState.UserState)
                    {
                        case UserState.Normal:
                            if (TerminologyManager.InputBoxSearchCompleted != null)
                            {
                                ObservableCollection<InputFieldResult> results = TerminologyManager.PrepareInputFieldResults(e.Result, false);
                                TerminologyManager.InputBoxSearchCompleted(null, new InputFieldSearchCompletedEventArgs(searchState.SearchTextOriginal, results, TerminologyManager.MaximumNumberOfTerms, e.Result.ExceedsMaxTotal));
                            }

                            break;
                        case UserState.Negated:
                            searchState.UserState = UserState.Combine;
                            searchState.OriginalSearchResults = e.Result;
                            terminologyProviderClient.SearchByDescriptionAsync(searchState.SearchTextOriginal, searchState.Domains, null, false, true, TerminologyManager.LocaleString, 1, 150, 150, searchState);
                            break;
                        case UserState.Combine:
                            if (TerminologyManager.InputBoxSearchCompleted != null)
                            {
                                ObservableCollection<InputFieldResult> results = TerminologyManager.PrepareInputFieldResults(searchState.OriginalSearchResults, true, e.Result, false, TerminologyManager.NegationDisplayString, string.Empty);
                                TerminologyManager.InputBoxSearchCompleted(null, new InputFieldSearchCompletedEventArgs(searchState.SearchTextOriginal, results, TerminologyManager.MaximumNumberOfTerms, e.Result.ExceedsMaxTotal));
                            }

                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    if (TerminologyManager.InputBoxSearchCompleted != null)
                    {
                        ObservableCollection<InputFieldResult> results = TerminologyManager.PrepareInputFieldResults(e.Result, false);
                        TerminologyManager.InputBoxSearchCompleted(null, new InputFieldSearchCompletedEventArgs(null, results, TerminologyManager.MaximumNumberOfTerms, e.Result.ExceedsMaxTotal));
                    }
                }
            }
            else
            {
                if (TerminologyManager.InputBoxSearchCompleted != null)
                {
                    SearchState searchState = e.UserState as SearchState;
                    TerminologyManager.InputBoxSearchCompleted(null, new InputFieldSearchCompletedEventArgs(searchState.SearchTextOriginal, false));
                }
            }
        }

        /// <summary>
        /// Prepares the input field results.
        /// </summary>
        /// <param name="firstResults">The first results.</param>
        /// <param name="firstResultsNegated">If set to <c>true</c> [first results negated].</param>
        /// <param name="secondResults">The second results.</param>
        /// <param name="secondResultsNegated">If set to <c>true</c> [second results negated].</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="postfix">The postfix.</param>
        /// <returns>A list of prepared input field results.</returns>
        private static ObservableCollection<InputFieldResult> PrepareInputFieldResults(SearchResult firstResults, bool firstResultsNegated, SearchResult secondResults, bool secondResultsNegated, string prefix, string postfix)
        {
            ObservableCollection<InputFieldResult> preparedResults = TerminologyManager.PrepareInputFieldResults(firstResults, prefix, postfix, firstResultsNegated);

            foreach (InputFieldResult inputFieldResult in TerminologyManager.PrepareInputFieldResults(secondResults, secondResultsNegated))
            {
                preparedResults.Add(inputFieldResult);
            }

            return preparedResults;
        }
              
        /// <summary>
        /// Prepares the input field results.
        /// </summary>
        /// <param name="searchResult">The search result.</param>
        /// <param name="isNegated">If set to <c>true</c> [is negated].</param>
        /// <returns>A list of prepared input field results.</returns>
        private static ObservableCollection<InputFieldResult> PrepareInputFieldResults(SearchResult searchResult, bool isNegated)
        {
            return TerminologyManager.PrepareInputFieldResults(searchResult, string.Empty, string.Empty, isNegated);
        }
        
        /// <summary>
        /// Prepares the input field results.
        /// </summary>
        /// <param name="searchResult">The search result.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="postfix">The postfix.</param>
        /// <param name="isNegated">If set to <c>true</c> [is negated].</param>
        /// <returns>A list of prepared input field results.</returns>
        private static ObservableCollection<InputFieldResult> PrepareInputFieldResults(SearchResult searchResult, string prefix, string postfix, bool isNegated)
        {
            ObservableCollection<InputFieldResult> preparedResults = new ObservableCollection<InputFieldResult>();

            foreach (TermResult termResult in searchResult.Terms)
            {
                InputFieldResult inputFieldResult = new InputFieldResult();
                
                inputFieldResult.Concept = termResult.Concept;
                inputFieldResult.Description = termResult.Description;
                inputFieldResult.SnomedDescriptionId = termResult.SnomedDescriptionId;
                inputFieldResult.PrefixText = prefix;
                inputFieldResult.PostfixText = postfix;
                inputFieldResult.IsNegated = isNegated;                                

                preparedResults.Add(inputFieldResult);
            }

            return preparedResults;
        }

        /// <summary>
        /// Calculates the Concept Detail from Concept Item.
        /// </summary>
        /// <param name="conceptItem">The concept item.</param>
        /// <returns>A concept detail object from a concept item.</returns>
        private static ConceptDetail ConceptDetailFromConceptItem(ConceptItem conceptItem)
        {
            return new ConceptDetail()
            {
                SnomedConceptId = conceptItem.SnomedConceptId,
                FullySpecifiedName = conceptItem.FullySpecifiedName,
                PreferredTerm = conceptItem.PreferredTerm
            };
        }

        /// <summary>
        /// Determines whether [is concept side] [the specified concept result].
        /// </summary>
        /// <param name="conceptResult">The concept result.</param>
        /// <returns>
        /// Is <c>true</c> if [is concept side] [the specified concept result]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsConceptSide(ConceptResult conceptResult)
        {
            ConceptResult conceptDetail = SnomedConcepts.Side.Children.SingleOrDefault(p => p.SnomedConceptId == conceptResult.SnomedConceptId);

            if (conceptDetail != null)
            {
                return true;
            }
            else
            {
                foreach (ConceptItem parent in conceptResult.Parents)
                {
                    conceptDetail = SnomedConcepts.Side.Children.SingleOrDefault(p => p.SnomedConceptId == parent.SnomedConceptId);

                    if (conceptDetail != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }  

        /// <summary>
        /// Determines whether the specified text contains negative.
        /// </summary>
        /// <param name="text">The text to check for negation.</param>
        /// <returns>Returns <c>true</c> if the specified text contains negative; otherwise, <c>false</c>.</returns>
        private static bool ContainsNegative(string text)
        {
            if (text.Split(new char[] { ' ' }).ToList().Contains(TerminologyManager.NegationString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
