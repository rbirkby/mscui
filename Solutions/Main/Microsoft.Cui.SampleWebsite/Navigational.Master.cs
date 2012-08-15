// <copyright file="Navigational.Master.cs" company="Microsoft Corporation copyright 2008.">
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
// <summary>Navigational master</summary>

namespace Microsoft.Cui.SampleWebsite
{
    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    /// <summary>
    /// The master page for navigational pages within the Controls, Samples and Guidance areas
    /// </summary>
    public partial class Navigational : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Load time processing
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnLoad(EventArgs e)
        {
            SiteMapDataSource siteMapDS = (SiteMapDataSource)this.Master.FindControl("siteMapDS");
            if (String.IsNullOrEmpty(siteMapDS.StartingNodeUrl))
            {
                siteMapDS.StartFromCurrentNode = true;
                siteMapDS.StartingNodeOffset = -2;
            }

            this.siteMapListControl.DataSource = siteMapDS;

            base.OnLoad(e);
        }
    }
}
