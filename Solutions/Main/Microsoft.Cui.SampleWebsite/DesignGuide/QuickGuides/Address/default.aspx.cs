// <copyright file="default.aspx.cs" company="Microsoft Corporation copyright 2010.">
// (c) 2010 Microsoft Corporation. All rights reserved.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx.
//
// This document and/or software (“this Content”) has been created in partnership
// with the National Health Service (NHS) in England.  Intellectual Property Rights
// to this Content are jointly owned by Microsoft and the NHS in England, although 
// both Microsoft and the NHS are entitled to independently exercise their rights
// of ownership.  Microsoft acknowledges the contribution of the NHS in England
// through their Common User Interface programme to this Content.  Readers are 
// referred to www.cui.nhs.uk for further information on the NHS CUI Programme.
// </copyright>
// <date>15-April-2010</date>
// <summary>Address Overview Quick Implementation Guidance page.</summary>
namespace Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address
{
    #region Using...

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    #endregion

    /// <summary>
    /// Address Overview Quick Implementation Guidance page.
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Page_Load method for the Page.
        /// </summary>
        /// <param name="sender">Sender of the Event.</param>
        /// <param name="e">Event Args.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.Attributes.Add("PageHref", "QuickGuides/Address/default.aspx");
            Master.Attributes.Add("SubHeader1", "Overview");
        }
    }
}
