// <copyright file="ExtendedPatientBanner.aspx.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>10-Jan-2008</date>
// <summary>Extending patient banner sample page</summary>

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
/// Extended patient Banner
/// </summary>
public partial class SamplesPatientBannerExtendedPatientBanner : System.Web.UI.Page
{
    /// <summary>
    /// Event handler for View all addresses link
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">event args</param>
    protected void PatientBanner_ViewAllAddressesClick(object sender, NhsCui.Toolkit.PatientBannerEventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "ViewAllAddressesClick", "this.msg='View all addresses clicked';", true);
    }

    /// <summary>
    /// Event handler for View all contact details link
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">event args</param>    
    protected void PatientBanner_ViewAllContactDetailsClick(object sender, NhsCui.Toolkit.PatientBannerEventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "ViewAllContactDetailsClick", "this.msg='View all contact details clicked';", true);
    }

    /// <summary>
    /// Event handler for Gender value click
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">event args</param>  
    protected void PatientBanner_GenderValueClick(object sender, NhsCui.Toolkit.PatientBannerEventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "GenderValueClick", "this.msg='Gender value clicked';", true);
    }

    /// <summary>
    /// Event handler for Identifier click
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">event args</param>    
    protected void PatientBanner_IdentifierClick(object sender, NhsCui.Toolkit.PatientBannerEventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "IdentifierClick", "this.msg='Nhs number clicked';", true);
    }
}
