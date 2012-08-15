// <copyright file="Default.aspx.cs" company="Microsoft Corporation copyright 2008.">
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
// <summary>Default page</summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;
using AjaxControlToolkit;
using NhsCui.Toolkit.Web;

/// <summary>
/// Default page partial class
/// </summary>
public partial class Default : System.Web.UI.Page
{
    /// <summary>
    /// Gets Blog Syndication Url
    /// </summary>
    public static string BlogSyndicationUrl
    {
        get
        {
            AppSettingsReader appsReader = new AppSettingsReader();
            return appsReader.GetValue("blogSyndicationUrl", typeof(string)).ToString();
        }
    }

    /// <summary>
    /// Default Page Load Method
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">Event Argument.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            XmlReader reader = XmlReader.Create(BlogSyndicationUrl);

            Rss20FeedFormatter rss = new Rss20FeedFormatter();

            if (reader != null)
            {
                if (rss.CanRead(reader))
                {
                    rss.ReadFrom(reader);

                    SyndicationItem item;
                    item = rss.Feed.Items.First<SyndicationItem>();
                    if (item != null)
                    {
                        this.latestBlogEntryTitle.Text = item.Title.Text;
                        this.latestBlogEntrySummary.Text = item.Summary.Text;
                        this.blogLink.NavigateUrl = item.Id;
                        this.blogFullPostLink.NavigateUrl = item.Id;
                        this.latestBlogPublishTime.Text = item.PublishDate.DateTime.ToString("dd-MMM-yyyy  HH:mm", CultureInfo.CurrentCulture) + " " + "by" + " " + item.Authors[0].Email;
                    }
                }
            }
        }
        catch (WebException)
        {
            AppSettingsReader appsReader = new AppSettingsReader();
            this.latestBlogEntrySummary.Text = appsReader.GetValue("LatestBlogAccessMessage", typeof(string)).ToString();
            this.blogFullPostLink.Visible = false;
        }
    }
}