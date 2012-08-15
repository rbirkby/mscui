// <copyright file="PageTitleControl.cs" company="Microsoft Corporation copyright 2008.">
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
// <summary>Page title control</summary>

namespace Microsoft.Cui.SampleWebsite
{
    using System;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Collections.Generic;

    /// <summary>
    /// Control used to render out the leaf page title
    /// </summary>
    [ToolboxData("<{0}:PageTitleControl runat=\"server\"></{0}:PageTitleControl>")]
    public class PageTitleControl : CompositeControl
    {
        /// <summary>
        /// Gets this control after Converting into an H1 control
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.H1; }
        }

        /// <summary>
        /// Gets the category of the first parent sitemap node with a category, or null
        /// </summary>
        private static string CurrentNodeCategory
        {
            get
            {
                if (SiteMap.CurrentNode == null)
                {
                    return null;
                }

                SiteMapNode node = SiteMap.CurrentNode.ParentNode;
                while (node != null)
                {
                    if (node["Category"] != null)
                    {
                        return node["Category"];
                    }

                    node = node.ParentNode;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the title of the current node in the sitemap or null
        /// </summary>
        private static string CurrentNodeTitle
        {
            get
            {
                return SiteMap.CurrentNode == null ? null : SiteMap.CurrentNode.Title;
            }
        }

        /// <summary>
        /// Creates the child controls
        /// </summary>
        protected override void CreateChildControls()
        {
            // Good practice (pg304 Nikhil's book)
            Controls.Clear();

            LiteralControl heading = new LiteralControl();

            // Build breadcrumb array
            List<string> breadcrumbs = new List<string>();
            breadcrumbs.Add(CurrentNodeCategory);
            breadcrumbs.Add(CurrentNodeTitle ?? SiteMap.RootNode.Title);
            breadcrumbs.RemoveAll(delegate(string crumb) { return String.IsNullOrEmpty(crumb); });

            heading.Text = String.Join(" &ndash; ", breadcrumbs.ToArray());
            Controls.Add(heading);
        }
    }
}
