//-----------------------------------------------------------------------
// <copyright file="TerminologyProvider.svc.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>18-Jan-2009</date>
// <summary>WCF Service Interface for a SNOMED CT terminology provider.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Services
{
    #region using

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.ServiceModel;
    using System.Xml.Linq;
    
    #endregion

    /// <summary>
    /// WCF Service Interface for a SNOMED CT terminology provider.
    /// </summary>
    [ServiceBehavior(Namespace = "http://www.mscui.net/Services")]
    public class TerminologyProvider : ITerminologyProvider
    {
        /// <summary>
        /// Indicates whether the static initialization has occured.
        /// </summary>
        private static bool initialized;

        /// <summary>
        /// Static List containing SearchByDescription results.
        /// </summary>
        private static ObservableCollection<TermResult> searchByDescriptionTerms = new ObservableCollection<TermResult>();

        /// <summary>
        /// Qualifiers list.
        /// </summary>
        private List<string> qualifiers = new List<string>() 
        {  
            "Left", "Right", "Right and left", "Left and right", "Left right", "Right left", "Unilateral",
            "Mild", "Mild to moderate", "Moderate", "Moderate to severe", "Severe", "Fatal", 
            "Brittle course", "Chronic", "Cyclic", "Gradual onset", "Subacute", "Subacute onset",
            "Sudden onset and/or short duration", "Sudden onset and short duration", "Sudden onset or short duration", "Sudden onset short duration",
            "First episode", "New episode", "Old episode", "Ongoing episode", "Undefined episode"
        };

        #region ITerminologyProvider Members

        /// <summary>
        /// Gets the root concept.
        /// </summary>
        /// <returns>SNOMED CT Root Concept.</returns>
        public string GetRootConcept()
        {
            string result = string.Empty;            

            return result;
        } 

        /// <summary>
        /// Searches the HLI SNOMED CT terminology service based on description.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="domains">The domains.</param>
        /// <param name="ancestorConceptId">The ancestor concept id.</param>
        /// <param name="includeAncestor">If set to <c>true</c> [include ancestor].</param>
        /// <param name="fullText">If set to <c>true</c> perform a [full text] search.  If set to <c>false</c> perform a [starts with] search.</param>
        /// <param name="locale">The locale.</param>
        /// <param name="fromIndex">The index of the first result to return.</param>
        /// <param name="endIndex">The index of the last result to return.</param>
        /// <param name="maxTotal">The maximum total.</param>
        /// <returns>SNOMED CT Search Result based on searchText.</returns>
        public SearchResult SearchByDescription(string searchText, string[] domains, string ancestorConceptId, bool includeAncestor, bool fullText, string locale, int fromIndex, int endIndex, int maxTotal)
        {
            if (!TerminologyProvider.initialized)
            {
                TerminologyProvider.Initialize();
            }

            SearchResult searchResult = new SearchResult();  
         
            var searchTerms = from term in TerminologyProvider.searchByDescriptionTerms
                              where term.Description.ToUpperInvariant().Contains(searchText.Replace("*", string.Empty).Trim())
                              select term;           
            
            searchResult.Terms = new ObservableCollection<TermResult>(searchTerms);
            
            return searchResult;
        }

        /// <summary>
        /// Gets the concept detail.
        /// </summary>
        /// <param name="snomedConceptId">The SNOMED concept id.</param>
        /// <param name="locale">The locale.</param>
        /// <returns>Concept Details of the given SNOMED CT concept id.</returns>
        public ConceptDetail GetConceptDetails(string snomedConceptId, string locale)
        {
            if (!TerminologyProvider.initialized)
            {
                TerminologyProvider.Initialize();
            }

            // Getting First rather than Select as for a snomedConceptId multiple entries can be found
            TermResult termResult = TerminologyProvider.searchByDescriptionTerms.FirstOrDefault<TermResult>(entry => entry.Concept.SnomedConceptId == snomedConceptId);
            ConceptDetail conceptDetail = null;

            if (termResult != null)
            {
                ConceptResult concept = termResult.Concept;
                if (concept != null)
                {
                    conceptDetail = new ConceptDetail()
                    {
                        SnomedConceptId = snomedConceptId,
                        PreferredTerm = concept.PreferredTerm,
                        FullySpecifiedName = concept.FullySpecifiedName,
                        Parents = concept.Parents,
                        AlternateDescriptions = concept.AlternateDescriptions,
                        Children = new ObservableCollection<ConceptResult>(),
                        RefinableCharacteristics = new ObservableCollection<RefinableCharacteristic>(),
                        Relations = new ObservableCollection<RelationResult>()
                    };
                }
            }

            if (null == conceptDetail)
            {
                conceptDetail = new ConceptDetail()
                {
                    SnomedConceptId = snomedConceptId,
                    PreferredTerm = "Sample Concept Preferred Term",
                    FullySpecifiedName = "Sample Concept (Fully Specified Name)",
                    Parents = new ObservableCollection<ConceptItem>() 
                    { 
                        new ConceptItem() { SnomedConceptId = snomedConceptId, FullySpecifiedName = "A Sample Concept (SampleConcept)", PreferredTerm = "A Sample Concept" },
                        new ConceptItem() { SnomedConceptId = snomedConceptId, FullySpecifiedName = "Another Sample Concept (SampleConcept)", PreferredTerm = "Another Sample Concept" }
                    },
                    AlternateDescriptions = new ObservableCollection<TermItem>()
                    {
                        new TermItem() { Description = "A sample synonym", SnomedDescriptionId = snomedConceptId },
                        new TermItem() { Description = "Another sample synonym", SnomedDescriptionId = snomedConceptId }
                    },
                    Children = new ObservableCollection<ConceptResult>(),
                    RefinableCharacteristics = new ObservableCollection<RefinableCharacteristic>(),
                    Relations = new ObservableCollection<RelationResult>()
                };
            }
             
            return conceptDetail;
        }

        /// <summary>
        /// Indexes the text.
        /// </summary>
        /// <param name="text">The text to index.</param>
        /// <param name="domains">The domains.</param>
        /// <param name="ancestorConceptId">The ancestor concept id.</param>
        /// <param name="includeAncestor">If set to <c>true</c> [include ancestor].</param>
        /// <param name="locale">The locale.</param>
        /// <returns>Array of Indexer Results indicating the positions of SNOMED CT concepts in a given string of text.</returns>
        public IndexerResult[] IndexText(string text, string[] domains, string ancestorConceptId, bool includeAncestor, string locale)
        {
            if (!TerminologyProvider.initialized)
            {
                TerminologyProvider.Initialize();
            }

            // look for possible qualifiers
            List<IndexerResult> possibleQuals = (from result in this.qualifiers
                                                 where text.ToUpperInvariant().Contains(result.ToUpperInvariant())
                                                 select new IndexerResult
                                                 {
                                                     Concept = new ConceptResult() { PreferredTerm = result, SnomedConceptId = "1234567890", FullySpecifiedName = result },
                                                     Description = result,
                                                     StartIndex = text.IndexOf(result, StringComparison.OrdinalIgnoreCase),
                                                     EndIndex = text.IndexOf(result, StringComparison.OrdinalIgnoreCase) + result.Length,
                                                     SnomedDescriptionId = "1234567890"
                                                 }).ToList();

            // look to ensure the correct terms are being used
            foreach (IndexerResult qual in possibleQuals)
            {
                // look for left and right terms
                if (qual.Description.Equals(this.qualifiers[3], StringComparison.OrdinalIgnoreCase) || qual.Description.Equals(this.qualifiers[4], StringComparison.OrdinalIgnoreCase) || qual.Description.Equals(this.qualifiers[5], StringComparison.OrdinalIgnoreCase))
                {
                    qual.Description = this.qualifiers[2];
                    qual.Concept.PreferredTerm = this.qualifiers[2];
                    qual.Concept.FullySpecifiedName = this.qualifiers[2];
                }

                // look for sudden onset terms
                if (qual.Description.Equals(this.qualifiers[20], StringComparison.OrdinalIgnoreCase) || qual.Description.Equals(this.qualifiers[21], StringComparison.OrdinalIgnoreCase) || qual.Description.Equals(this.qualifiers[22], StringComparison.OrdinalIgnoreCase))
                {
                    qual.Description = this.qualifiers[19];
                    qual.Concept.PreferredTerm = this.qualifiers[19];
                    qual.Concept.FullySpecifiedName = this.qualifiers[19];
                }
            }

            // Processes single qualifiers
            this.RemoveSingleQualifiers(possibleQuals);

            // return the complete set of qualifiers as an IndexerResult array
            return possibleQuals.ToArray();
        }

        /// <summary>
        /// Gets the domains.
        /// </summary>
        /// <returns>The list of supported SNOMED CT domains.</returns>
        public string[] GetDomains()
        {
            string[] domains = null;
                        
            return domains;
        }

        #endregion

        /// <summary>
        /// Initializes static members of the <see cref="TerminologyProvider"/> class.
        /// </summary>
        private static void Initialize()
        {
            XDocument loaded = XDocument.Load(@"http://localhost:1753/SearchByDescription.xml");

            var terms = from x in loaded.Descendants("TermResult")
                        select new TermResult
                        {
                            Description = x.Element("Description").Value,
                            SnomedDescriptionId = x.Element("SnomedDescriptionId").Value,
                            Concept = new ConceptResult
                            {
                                FullySpecifiedName = x.Element("Concept").Element("FullySpecifiedName").Value,
                                PreferredTerm = x.Element("Concept").Element("PreferredTerm").Value,
                                SnomedConceptId = x.Element("Concept").Element("SnomedConceptId").Value,
                                AlternateDescriptions = new ObservableCollection<TermItem>((from alternateDescriptions in x.Element("Concept").Descendants("TermItem")
                                                                                            select new TermItem
                                                                                            {
                                                                                                Description = alternateDescriptions.Element("Description").Value,
                                                                                                SnomedDescriptionId = alternateDescriptions.Element("SnomedDescriptionId").Value
                                                                                            }).ToList()),

                                Parents = new ObservableCollection<ConceptItem>((from parents in x.Element("Concept").Descendants("ConceptItem")
                                                                                 select new ConceptItem
                                                                                 {
                                                                                     FullySpecifiedName = parents.Element("FullySpecifiedName").Value,
                                                                                     SnomedConceptId = parents.Element("SnomedConceptId").Value,
                                                                                     PreferredTerm = parents.Element("PreferredTerm").Value
                                                                                 }).ToList())
                            }
                        };

            TerminologyProvider.searchByDescriptionTerms = new ObservableCollection<TermResult>(terms);

            TerminologyProvider.initialized = true;
        }

        /// <summary>
        /// Processes the qualifier list to ensure single words in a compound expression are removed.
        /// </summary>
        /// <param name="possibleQuals">The List of possible qualifiers.</param>
        private void RemoveSingleQualifiers(List<IndexerResult> possibleQuals)
        {
            // look to see if left and right exists and if so delete left and right qualifiers
            if (possibleQuals.Exists(qual => qual.Description.Equals(this.qualifiers[2], StringComparison.OrdinalIgnoreCase)))
            {
                possibleQuals.RemoveAll(qual => qual.Description.Equals(this.qualifiers[0], StringComparison.OrdinalIgnoreCase) || qual.Description.Equals(this.qualifiers[1], StringComparison.OrdinalIgnoreCase));
            }

            // look to see if mild moderate exists and if so delete mild and moderate qualifiers
            if (possibleQuals.Exists(qual => qual.Description.Equals(this.qualifiers[8], StringComparison.OrdinalIgnoreCase)))
            {
                possibleQuals.RemoveAll(qual => qual.Description.Equals(this.qualifiers[7], StringComparison.OrdinalIgnoreCase) || qual.Description.Equals(this.qualifiers[9], StringComparison.OrdinalIgnoreCase));
            }

            // look to see if moderate severe exists and if so delete moderate and severe qualifiers
            if (possibleQuals.Exists(qual => qual.Description.Equals(this.qualifiers[10], StringComparison.OrdinalIgnoreCase)))
            {
                possibleQuals.RemoveAll(qual => qual.Description.Equals(this.qualifiers[9], StringComparison.OrdinalIgnoreCase) || qual.Description.Equals(this.qualifiers[11], StringComparison.OrdinalIgnoreCase));
            }

            // look to see if subacute onset exists and if so delete onset qualifiers
            if (possibleQuals.Exists(qual => qual.Description.Equals(this.qualifiers[18], StringComparison.OrdinalIgnoreCase)))
            {
                possibleQuals.RemoveAll(qual => qual.Description.Equals(this.qualifiers[17], StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
