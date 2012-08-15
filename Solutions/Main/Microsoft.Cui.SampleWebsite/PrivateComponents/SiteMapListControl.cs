// <copyright file="SiteMapListControl.cs" company="Microsoft Corporation copyright 2008.">
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
// <summary>Sitemap List control</summary>

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
    using System.Globalization;

    /// <summary>
    /// Renders a hierarchical list from a SiteMap
    /// </summary>
    public class SiteMapListControl : Control
    {
        /// <summary>
        /// String constant used to format a site map node's anchor title
        /// </summary>
        private const string SiteMapAnchorTitlePrefix = "Link to {0}";

        /// <summary>
        /// SiteMap dataSource
        /// </summary>
        private SiteMapDataSource siteMapDataSource;

        /// <summary>
        /// Depth of SiteMap to travel down when writing out list of links
        /// </summary>
        private int depth;

        /// <summary>
        /// Gets or sets the site map nodes that hold the cross reference links
        /// </summary>
        public SiteMapDataSource DataSource
        {
            get
            {
                return this.siteMapDataSource;
            }

            set
            {
                this.siteMapDataSource = value;
            }
        }

        /// <summary>
        /// Gets or sets the depth of SiteMap to travel down when writing out list of links
        /// </summary>
        public int Depth
        {
            get
            {
                return this.depth;
            }

            set
            {
                this.depth = value;
            }
        }

        /// <summary>
        /// Render out the bulleted List.
        /// </summary>
        /// <param name="writer">Html Text Writer</param>
        protected override void Render(HtmlTextWriter writer)
        {
            int depthTravelled = 0;

            SiteMapDataSourceView view = (SiteMapDataSourceView)this.DataSource.GetView(String.Empty);
            SiteMapNodeCollection nodes = (SiteMapNodeCollection)view.Select(DataSourceSelectArguments.Empty);

            writer.RenderBeginTag(HtmlTextWriterTag.Ul);

            if (this.DataSource != null)
            {
                this.RenderSiteMapLevelAsBulletedList(writer, nodes, depthTravelled);
            }

            writer.RenderEndTag();
        }

        /// <summary>
        /// Render out the bullet list (ul , li)
        /// </summary>
        /// <param name="writer">Html Text Writer</param>
        /// <param name="nodes">SiteMapNodeCollection to traverse</param>
        /// <param name="depthTravelled">How far down the nodes to go</param>
        private void RenderSiteMapLevelAsBulletedList(HtmlTextWriter writer, SiteMapNodeCollection nodes, int depthTravelled)
        {
            depthTravelled++;

            foreach (SiteMapNode node in nodes)
            {
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["skipBlog"], CultureInfo.CurrentCulture) && node.Title == ConfigurationManager.AppSettings["blogNodeTitle"].ToString())
                {
                    continue;
                }

                if (node == this.DataSource.Provider.CurrentNode)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "currentNode");
                }

                writer.RenderBeginTag(HtmlTextWriterTag.Li);

                if (node.Url != null && node.Url.Length > 0)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, CrossReferencesControl.StripCrossRefMarkerOff(node.Url));
                    writer.AddAttribute(HtmlTextWriterAttribute.Title, string.Format(CultureInfo.CurrentCulture, SiteMapListControl.SiteMapAnchorTitlePrefix, node.Description));
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.WriteEncodedText(node.Title);
                    writer.RenderEndTag(); // close anchor
                }
                else
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.WriteEncodedText(node.Title);
                    writer.RenderEndTag(); // close span
                }

                // Add any children levels, if needed (recursively)
                if (node.HasChildNodes && (this.Depth == 0 || this.Depth > depthTravelled))
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Ul);

                    this.RenderSiteMapLevelAsBulletedList(writer, node.ChildNodes, depthTravelled);

                    writer.RenderEndTag();
                }

                writer.RenderEndTag();
            }
        }
    }
}
