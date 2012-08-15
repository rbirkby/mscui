// <copyright file="CrossReferencesControl.cs" company="Microsoft Corporation copyright 2008.">
// (c) 2008 Microsoft Corporation. All rights reserved.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx.
//
// This document and/or software (�this Content�) has been created in partnership
// with the National Health Service (NHS) in England.  Intellectual Property Rights
// to this Content are jointly owned by Microsoft and the NHS in England, although 
// both Microsoft and the NHS are entitled to independently exercise their rights
// of ownership.  Microsoft acknowledges the contribution of the NHS in England
// through their Common User Interface programme to this Content.  Readers are 
// referred to www.cui.nhs.uk for further information on the NHS CUI Programme.
// </copyright>
// <date>10-Jan-2008</date>
// <summary>Cross references control</summary>

namespace Microsoft.Cui.SampleWebsite
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Control used to render out the Cross references HTML Table
    /// </summary>
    public class CrossReferencesControl : System.Web.UI.Control
    {
        /// <summary>
        /// Cross Reference Table Heading
        /// </summary>
        private const string CrossReferenceTableHeading = "Associated {0}";

        /// <summary>
        /// Cross Reference Table Summary
        /// </summary>
        private const string CrossReferenceTableSummary = "The table below lists associated {0}.";

        /// <summary>
        /// String constant used to format a cross reference's anchor title
        /// </summary>
        private const string CrossReferenceAnchorTitlePrefix = "Links to {0}";

        /// <summary>
        /// SiteMap Nodes
        /// </summary>
        private SiteMapNodeCollection siteMapNodes;

        /// <summary>
        /// Gets or sets the site map nodes that hold the cross reference links
        /// </summary>
        public SiteMapNodeCollection SiteMapNodes
        {
            get
            {
                return this.siteMapNodes;
            }

            set
            {
                this.siteMapNodes = value;
            }
        }

        /// <summary>
        /// Rip the crossref marker stuff off of any links before they are surfaced in the Cross References table
        /// </summary>
        /// <param name="url">url to be stripped</param>
        /// <returns>stripped and cleaned</returns>
        /// <remarks>The urls need cleaning because otherwise the SiteMap's CurrentNode will be set to the codeOnly reference not the proper high level one</remarks>
        public static string StripCrossRefMarkerOff(string url)
        {
            System.Text.RegularExpressions.Regex crossRefMarkerRegEx = new System.Text.RegularExpressions.Regex(@"\?(crossref)*(&id=\d)?");

            return crossRefMarkerRegEx.Replace(url, string.Empty);
        }

        /// <summary>
        /// Get SiteMapNodes to be used for Cross Reference UI
        /// </summary>
        /// <param name="dataSource">Site map DataSource</param>
        /// <returns>SiteMap Node Collection</returns>
        public static SiteMapNodeCollection GetSiteMapNodesForCrossReferences(SiteMapDataSource dataSource)
        {
            return System.Web.SiteMap.CurrentNode.ChildNodes;
        }

        /// <summary>
        /// Render out the TABLE
        /// </summary>
        /// <param name="writer">Html Text Writer</param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.SiteMapNodes.Count > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "relatedResources");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                this.RenderHeaderDiv(writer);

                this.RenderTable(writer);

                writer.RenderEndTag(); // Close wrapping div

                base.Render(writer);
            }
        }

        /// <summary>
        /// Build and return a string that will be used when listing the cross references categories
        /// </summary>
        /// <returns>If the cross reference categories are Controls &amp; Samples the returned string will be 'Controls and Samples'</returns>
        private string BuildCategoryText()
        {
            string categoryTitlesText = string.Empty;

            foreach (SiteMapNode crossReferenceCategory in this.SiteMapNodes)
            {
                if (categoryTitlesText.Length > 0)
                {
                    categoryTitlesText += " and ";
                }

                categoryTitlesText += crossReferenceCategory.Title;
            }

            return categoryTitlesText;
        }

        /// <summary>
        /// Render out the heading div for the main table
        /// </summary>
        /// <param name="writer">Html Text Writer</param>
        private void RenderHeaderDiv(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.RenderBeginTag(HtmlTextWriterTag.H2);
            writer.WriteEncodedText(string.Format(CultureInfo.CurrentCulture, CrossReferenceTableHeading, this.BuildCategoryText()));
            writer.RenderEndTag(); // Close the h2

            writer.RenderEndTag(); // Close the div
        }

        /// <summary>
        /// Render out the cross referneces table
        /// </summary>
        /// <param name="writer">Html Text Writer</param>
        private void RenderTable(HtmlTextWriter writer)
        {
            List<SiteMapNode> crossReferenceCategories = new List<SiteMapNode>();
            List<SiteMapNode> crossReferences = new List<SiteMapNode>();

            writer.AddAttribute("summary", string.Format(CultureInfo.CurrentCulture, CrossReferenceTableSummary, this.BuildCategoryText()));
            writer.RenderBeginTag(HtmlTextWriterTag.Table);

            writer.AddAttribute(HtmlTextWriterAttribute.Width, "50%");
            writer.RenderBeginTag(HtmlTextWriterTag.Col);
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Width, "50%");
            writer.RenderBeginTag(HtmlTextWriterTag.Col);
            writer.RenderEndTag();

            // Write out the Cross References table header
            writer.RenderBeginTag(HtmlTextWriterTag.Thead);

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

            foreach (SiteMapNode crossReferenceCategory in this.SiteMapNodes)
            {
                crossReferenceCategories.Add(crossReferenceCategory);

                writer.RenderBeginTag(HtmlTextWriterTag.Th);

                writer.WriteEncodedText(crossReferenceCategory.Title);

                writer.RenderEndTag();

                // Whilst iterating get all cross reference nodes into a single array
                foreach (SiteMapNode node in crossReferenceCategory.ChildNodes)
                {
                    crossReferences.Add(node);
                }
            }

            writer.RenderEndTag();

            writer.RenderEndTag();

            // THEAD rendered open a TBODY
            writer.RenderBeginTag(HtmlTextWriterTag.Tbody);

            ArrayList written = new ArrayList(crossReferences.Count);
            bool moreRowsNeeded = true;

            do
            {
                bool moveToNextRow = false;
                bool[] crossRefWrittenForCategory = new bool[crossReferenceCategories.Count];
                List<SiteMapNode> siteMapNodesForRow = new List<SiteMapNode>();
                int crossReferenceIndex = 0;

                while (moveToNextRow == false && crossReferenceIndex < crossReferences.Count)
                {
                    // If the cross reference has not been written...
                    if (written.Contains(crossReferences[crossReferenceIndex].Title) == false)
                    {
                        // ... check that we have not already written one for that Category
                        // and if not write it out
                        SiteMapNode crossReference = crossReferences[crossReferenceIndex];

                        if (crossRefWrittenForCategory[crossReferenceCategories.IndexOf(crossReference.ParentNode)] == false)
                        {
                            siteMapNodesForRow.Add(crossReference);
                        }
                        else if (crossRefWrittenForCategory.Length == crossReferenceCategories.Count)
                        {
                            // We have written ONE entry for each of the categories.
                            // We only write a SINGLE entryu per row per category so time to move to next Row
                            moveToNextRow = true;
                        }

                        if (written.Count + siteMapNodesForRow.Count == crossReferences.Count)
                        {
                            moveToNextRow = true;
                        }
                    }

                    crossReferenceIndex++;
                }

                // If we have some SiteMapNodes for the row so render them
                if (siteMapNodesForRow.Count > 0)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                    // Write out all cross references for row
                    for (int cellIndex = 0; cellIndex < crossReferenceCategories.Count; cellIndex++)
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);

                        foreach (SiteMapNode node in siteMapNodesForRow)
                        {
                            if (node.ParentNode == crossReferenceCategories[cellIndex])
                            {
                                // Render out the title and description for the Cross Reference link. Rather than use whatever text 
                                // is set in the Cross Reference SiteMapNode, always use that from the SiteMapNode
                                // that is being cross referenced i.e. the destination of the cross reference

                                // Get the destination node by looking up the cross reference's url minus its CrossRef marker
                                SiteMapNode crossReferenceDestinationNode = node.Provider.FindSiteMapNode(CrossReferencesControl.StripCrossRefMarkerOff(node.Url));

                                if (crossReferenceDestinationNode == null)
                                {
                                    throw new ApplicationException("Unable to find a cross-referenced item in the sitemap '" + node.Url + "'");
                                }

                                writer.AddAttribute(HtmlTextWriterAttribute.Href, CrossReferencesControl.StripCrossRefMarkerOff(node.Url));
                                writer.AddAttribute(HtmlTextWriterAttribute.Title, string.Format(CultureInfo.CurrentCulture, CrossReferencesControl.CrossReferenceAnchorTitlePrefix, crossReferenceDestinationNode.Description));
                                writer.RenderBeginTag(HtmlTextWriterTag.A);

                                writer.WriteEncodedText(crossReferenceDestinationNode.Title);

                                writer.RenderEndTag(); // close anchor

                                // Add the title of the node to the list of written nodes
                                written.Add(node.Title);

                                break;
                            }
                        }

                        writer.RenderEndTag(); // Close TD
                    }

                    writer.RenderEndTag(); // Close TR
                }

                // If we have written all the cross references no moreRowsNeeded
                if (written.Count == crossReferences.Count)
                {
                    moreRowsNeeded = false;
                }
            }
            while (moreRowsNeeded == true);

            writer.RenderEndTag(); // Closes TBODY

            writer.RenderEndTag(); // Close TABLE
        }
    }
}
