// <copyright file="PatientBanner.aspx.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>PatientBanner control</summary>

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
/// PatientBanner sample page code behind
/// </summary>
public partial class ComponentsPatientBanner : System.Web.UI.Page
{
    /// <summary>
    /// Default Page Load Function
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">Event Argument.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.AddAllergies();
        }
    }

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
    /// Event handler for View allergy record link
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">event args</param>    
    protected void PatientBanner_ViewAllergyRecordClick(object sender, NhsCui.Toolkit.PatientBannerEventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "ViewAllergyRecordClick", "this.msg='View allergy record clicked';", true);
    }

    /// <summary>
    /// Event handler for View all Contact details link
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

    /// <summary>
    /// Add allergies to the list.
    /// </summary>
    private void AddAllergies()
    {
        NhsCui.Toolkit.Web.Allergy allergy = new NhsCui.Toolkit.Web.Allergy();

        allergy.AllergyName = "Dust";
        allergy.LastUpdatedOn = new DateTime(2007, 7, 1);
        this.patientBanner.Allergies.Add(allergy);

        allergy = new NhsCui.Toolkit.Web.Allergy();
        allergy.AllergyName = "Smoke";
        allergy.LastUpdatedOn = new DateTime(2007, 6, 10);
        this.patientBanner.Allergies.Add(allergy);

        allergy = new NhsCui.Toolkit.Web.Allergy();
        allergy.AllergyName = "Perfume";
        allergy.LastUpdatedOn = new DateTime(2006, 6, 14);
        this.patientBanner.Allergies.Add(allergy);

        allergy = new NhsCui.Toolkit.Web.Allergy();
        allergy.AllergyName = "Latex";
        allergy.LastUpdatedOn = new DateTime(2006, 6, 21);
        this.patientBanner.Allergies.Add(allergy);

        allergy = new NhsCui.Toolkit.Web.Allergy();
        allergy.AllergyName = "Peanuts";
        allergy.LastUpdatedOn = new DateTime(2007, 1, 6);
        this.patientBanner.Allergies.Add(allergy);

        allergy = new NhsCui.Toolkit.Web.Allergy();
        allergy.AllergyName = "Hay";
        allergy.LastUpdatedOn = new DateTime(2007, 3, 6);
        this.patientBanner.Allergies.Add(allergy);
    }
}
