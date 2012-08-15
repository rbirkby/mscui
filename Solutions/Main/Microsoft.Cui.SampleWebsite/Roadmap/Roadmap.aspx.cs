// <copyright file="RoadMap.aspx.cs" company="Microsoft Corporation copyright 2008.">
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
// <summary>RoadMap page</summary>

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

/// <summary>
/// Roadmap web page code behind
/// </summary>
public partial class Roadmap : System.Web.UI.Page
{
    /// <summary>
    /// Load time processing
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">Event Argument.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        SiteMapDataSource siteMapDS = (SiteMapDataSource)this.Master.Master.FindControl("siteMapDS");
        siteMapDS.StartingNodeUrl = siteMapDS.Provider.CurrentNode.Url;
    }
}

