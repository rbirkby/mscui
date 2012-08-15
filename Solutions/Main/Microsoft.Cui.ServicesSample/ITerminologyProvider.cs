//-----------------------------------------------------------------------
// <copyright file="ITerminologyProvider.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
    #region Using

    using System.Diagnostics.CodeAnalysis;
    using System.ServiceModel;

    #endregion
    
    /// <summary>
    /// WCF Service Interface for a SNOMED CT terminology provider.
    /// </summary>
    [ServiceContract(Namespace = "http://www.mscui.net/Services")]    
    public interface ITerminologyProvider
    {
        /// <summary>
        /// Gets the root concept.
        /// </summary>
        /// <returns>SNOMED CT Root Concept.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "We need to mirror a 3rd Party web service exactly."), OperationContract]
        string GetRootConcept();      

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
        [OperationContract]
        SearchResult SearchByDescription(string searchText, string[] domains, string ancestorConceptId, bool includeAncestor, bool fullText, string locale, int fromIndex, int endIndex, int maxTotal);

        /// <summary>
        /// Gets the concept details.
        /// </summary>
        /// <param name="snomedConceptId">The snomed concept id.</param>
        /// <param name="locale">The locale.</param>
        /// <returns>Concept Details of the given SNOMED CT concept id.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "snomed", Justification = "SNOMED is a clinical terminology system"), OperationContract]
        ConceptDetail GetConceptDetails(string snomedConceptId, string locale);

        /// <summary>
        /// Indexes the text.
        /// </summary>
        /// <param name="text">The text to index.</param>
        /// <param name="domains">The domains.</param>
        /// <param name="ancestorConceptId">The ancestor concept id.</param>
        /// <param name="includeAncestor">If set to <c>true</c> [include ancestor].</param>
        /// <param name="locale">The locale.</param>
        /// <returns>Array of Indexer Results indicating the positions of SNOMED CT concepts in a given string of text.</returns>
        [OperationContract]
        IndexerResult[] IndexText(string text, string[] domains, string ancestorConceptId, bool includeAncestor, string locale);

        /// <summary>
        /// Gets the domains.
        /// </summary>
        /// <returns>The list of supported SNOMED CT domains.</returns>
        [OperationContract]
        string[] GetDomains();
    }
}
