// <copyright file="XMLPatientBanner.aspx.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Patient banner sample page reading the data from XML</summary>

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
////XML namespaces
using System.IO;
using System.Xml;
using System.Xml.XPath;

/// <summary>
/// Patient Banner Data Binding Sample
/// </summary>
public partial class SamplesPatientBannerXmlPatientBanner : System.Web.UI.Page
{
    /// <summary>
    /// Default Page Load Function
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">Event Argument.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager sm = ScriptManager.GetCurrent(Page);
        sm.SetFocus(this.patientListBox);
        if (!IsPostBack)
        {
            this.patientListBox.DataSource = this.GetPatientNames();
            this.patientListBox.DataBind();
            this.patientListBox.SelectedIndex = 0;
            this.GetPatientDetails(this.patientListBox.SelectedItem.ToString());
        }
    }

    /// <summary>
    /// Handler for list box selection change
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">Event Argument.</param>
    protected void PatientListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetPatientDetails(this.patientListBox.SelectedItem.ToString());
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

    /// <summary>
    /// To retrieve the patient banner to bind to list box
    /// </summary>
    /// <returns>Patient element List<string></string></returns>
    private IEnumerable<string> GetPatientNames()
    {
        XmlTextReader reader = new XmlTextReader(this.Page.MapPath("~/Data/XMLPatientData.xml"));
        while (reader.Read())
        {
            // keep reading to find Patient elements
            if (reader.Name.Equals("Patient") && (reader.NodeType == XmlNodeType.Element))
            {
                if (reader.GetAttribute("FamilyName") != null)
                {
                    yield return reader.GetAttribute("FamilyName");
                }
                else
                {
                    reader.Skip();
                }
            }
        }
    }

    /// <summary>
    /// Get details of the selected patient
    /// </summary>
    /// <param name="patientFamilyName">patient Family Name</param>
    private void GetPatientDetails(string patientFamilyName)
    {
        XmlTextReader reader = new XmlTextReader(this.Page.MapPath("~/Data/XMLPatientData.xml"));
        while (reader.Read())
        {
            // keep reading to find Patient elements
            if (reader.Name.Equals("Patient") && (reader.NodeType == XmlNodeType.Element))
            {
                if (reader.GetAttribute("FamilyName") == patientFamilyName)
                {
                    this.ClearControl();
                    this.patientBanner.FamilyName = reader.GetAttribute("FamilyName");
                    while (reader.NodeType != XmlNodeType.EndElement && reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            switch (reader.Name)
                            {
                                case "GivenName":
                                    this.patientBanner.GivenName = reader.ReadString();
                                    this.patientBanner.BorderColor = System.Drawing.Color.Empty;
                                    reader.Read();
                                    break;
                                case "Title":
                                    this.patientBanner.Title = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "DateOfBirth":
                                    this.patientBanner.DateOfBirth = DateTime.Parse(reader.ReadString(), CultureInfo.CurrentCulture);
                                    reader.Read();
                                    break;
                                case "DateOfDeath":
                                    this.patientBanner.DateOfDeath = DateTime.Parse(reader.ReadString(), CultureInfo.CurrentCulture);
                                    this.patientBanner.BorderColor = System.Drawing.Color.Black;
                                    reader.Read();
                                    break;
                                case "Identifier":
                                    this.patientBanner.Identifier = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "Gender":
                                    this.patientBanner.Gender = (NhsCui.Toolkit.PatientGender)Enum.Parse(typeof(NhsCui.Toolkit.PatientGender), reader.ReadString(), true);
                                    reader.Read();
                                    break;
                                case "HomePhoneNumber":
                                    this.patientBanner.HomePhoneNumber = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "WorkPhoneNumber":
                                    this.patientBanner.WorkPhoneNumber = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "MobilePhoneNumber":
                                    this.patientBanner.MobilePhoneNumber = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "EmailAddress":
                                    this.patientBanner.EmailAddress = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "Address1":
                                    this.patientBanner.Address1 = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "Address2":
                                    this.patientBanner.Address2 = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "Address3":
                                    this.patientBanner.Address3 = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "Town":
                                    this.patientBanner.Town = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "County":
                                    this.patientBanner.County = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "PostCode":
                                    this.patientBanner.Postcode = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "AccessKey":
                                    this.patientBanner.AccessKey = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "PatientImage":
                                    this.patientBanner.PatientImage = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "AllergyInformation":
                                    this.patientBanner.AllergyInformation = (NhsCui.Toolkit.AllergyInformation)Enum.Parse(typeof(NhsCui.Toolkit.AllergyInformation), reader.ReadString(), true);
                                    if (this.patientBanner.AllergyInformation != NhsCui.Toolkit.AllergyInformation.Present)
                                    {
                                        this.patientBanner.Allergies.Clear();
                                    }
                                    else
                                    {
                                        this.AddAllergies();
                                    }

                                    reader.Read();
                                    break;
                                case "PreferredName":
                                    this.patientBanner.PreferredName = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "PreferredNameLabel":
                                    string preferredNameLabel = reader.ReadString();
                                    if (String.IsNullOrEmpty(preferredNameLabel))
                                    {
                                        this.patientBanner.PreferredNameLabelText = null;
                                    }
                                    else
                                    {
                                        this.patientBanner.PreferredNameLabelText = preferredNameLabel;
                                    }

                                    reader.Read();
                                    break;
                                case "GenderLabelTooltip":
                                    this.patientBanner.GenderLabelTooltip = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "GenderValueTooltip":
                                    this.patientBanner.GenderValueTooltip = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "IdentifierTooltip":
                                    this.patientBanner.IdentifierTooltip = reader.ReadString();
                                    reader.Read();
                                    break;
                                case "IdentifierLabelTooltip":
                                    this.patientBanner.IdentifierLabelTooltip = reader.ReadString();
                                    reader.Read();
                                    break;
                                default:
                                    reader.ReadString();
                                    reader.Read();
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    reader.Skip();
                }
            }
        }
    }

    /// <summary>
    /// Add patient allergies
    /// </summary>
    private void AddAllergies()
    {
        NhsCui.Toolkit.Web.Allergy allergy = new NhsCui.Toolkit.Web.Allergy();

        allergy.AllergyName = "Dust";
        allergy.LastUpdatedOn = new DateTime(2007, 6, 1);
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
        allergy.LastUpdatedOn = new DateTime(2004, 6, 21);
        this.patientBanner.Allergies.Add(allergy);

        allergy = new NhsCui.Toolkit.Web.Allergy();
        allergy.AllergyName = "Pollen";
        allergy.LastUpdatedOn = new DateTime(2005, 6, 11);
        this.patientBanner.Allergies.Add(allergy);

        allergy = new NhsCui.Toolkit.Web.Allergy();
        allergy.AllergyName = "Serum";
        allergy.LastUpdatedOn = new DateTime(2007, 1, 1);
        this.patientBanner.Allergies.Add(allergy);
    }

    /// <summary>
    /// function to clear out the previous values.
    /// </summary>
    private void ClearControl()
    {
        this.patientBanner.AccessKey = "";
        this.patientBanner.Address1 = "";
        this.patientBanner.Address2 = "";
        this.patientBanner.Address3 = "";
        this.patientBanner.DateOfBirth = new DateTime();
        this.patientBanner.DateOfDeath = DateTime.Parse("01/01/0001", CultureInfo.CurrentCulture);
    }
}
