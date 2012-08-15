// <copyright file="DefaultMaster.master.cs" company="Microsoft Corporation copyright 2008.">
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
// <summary>Master page</summary>

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NhsCui.Toolkit.Web;
using System.Collections.Generic;
using System.Globalization;

/// <summary>
/// Default Master Page
/// </summary>
public partial class DefaultMaster : System.Web.UI.MasterPage
{
    /// <summary>Gets a value indicating whether the flag indicating whether Analytics should be enabled or disabled</summary>
    protected static bool AnalyticsEnabled
    {
        get
        {
            bool analyticsEnabled = false;
            if (Boolean.TryParse(ConfigurationManager.AppSettings["enableAnalytics"], out analyticsEnabled))
            {
                return analyticsEnabled;
            }
            else
            {
                return false;
            }
        }
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
    /// Page Load handler
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">Event Argument.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["skipBlog"] != null)
        {
            bool skipBlog = Convert.ToBoolean(ConfigurationManager.AppSettings["skipBlog"], CultureInfo.CurrentCulture);
            this.blogLink.Visible = !skipBlog;
        }

        // Build breadcrumb array
        List<string> breadcrumbs = new List<string>();
        breadcrumbs.Add("Microsoft Health CUI");
        breadcrumbs.Add(CurrentNodeCategory);
        breadcrumbs.Add(CurrentNodeTitle ?? Page.Title);
        breadcrumbs.RemoveAll(delegate(string crumb) { return String.IsNullOrEmpty(crumb); });

        this.Page.Title = String.Join(" - ", breadcrumbs.ToArray());

        this.analyticsPanel.Visible = AnalyticsEnabled;

        this.RenderBuildNo();
    }

    /// <summary>
    /// Render the build no inside a span
    /// </summary>
    private void RenderBuildNo()
    {
        // Use the address label control to extract the build no. form nhscui.toolkit.web assembly
        AddressLabel addresslabel = new AddressLabel();
        string buildNumber = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetAssembly(addresslabel.GetType()).Location).FileVersion;

        this.buildNo.InnerText = string.Format(CultureInfo.CurrentCulture, "Build No: {0}", buildNumber);

        // Add the build number so a new version of the site won't use the old stylesheet
        if (!IsPostBack)
        {
            this.mainStylesheet.Href += "?" + buildNumber;
        }
    }
}
