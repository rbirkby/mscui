// <copyright file="Demonstrators.aspx.cs" company="Microsoft Corporation copyright 2008.">
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
// <date>16-Mar-2008</date>
// <summary>Demonstrators page</summary>

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
using System.Text;

/// <summary>
/// Design Guide
/// </summary>
public partial class ShowcaseDemonstrators : System.Web.UI.Page
{
    /// <summary>
    /// Gets the initparams value for silverlight plugin
    /// </summary>
    protected static string InitParameters
    {
        get
        {
            AppSettingsReader appsReader = new AppSettingsReader();

            StringBuilder initParams = new StringBuilder();
            initParams.Append("HostPageName=Demonstrators");

            // Append the PatientJourneyDemonstrator relative url
            initParams.Append(",PatientJourneyDemonstrator=");
            initParams.Append(appsReader.GetValue("PatientJourneyDemonstrator", typeof(string)));

            // Add the demonstrators video
            initParams.Append(",demonstrators01=");
            initParams.Append(appsReader.GetValue("demonstrators01", typeof(string)));

            initParams.Append(",demonstrators02=");
            initParams.Append(appsReader.GetValue("demonstrators02", typeof(string)));

            initParams.Append(",demonstrators03=");
            initParams.Append(appsReader.GetValue("demonstrators03", typeof(string)));

            initParams.Append(",demonstrators04=");
            initParams.Append(appsReader.GetValue("demonstrators04", typeof(string)));

            initParams.Append(",demonstrators05=");
            initParams.Append(appsReader.GetValue("demonstrators05", typeof(string)));

            return initParams.ToString();
        }
    }

    /// <summary>
    /// Default Page Load Method
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">Event Argument.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
