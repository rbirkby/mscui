// <copyright file="PatientSearchInputBox.aspx.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>PatientSearchInputBox control sample page</summary>

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
using NhsCui.Toolkit.PatientSearch;
using System.Globalization;

/// <summary>
/// PatientSearchInput sample file
/// </summary>
public partial class SamplesPatientSearchInputBox : System.Web.UI.Page
{
    /// <summary>
    /// Parse the PatientSearchInputBox1.Text property on postback
    /// </summary>
    /// <param name="sender">Sender of the event.</param>
    /// <param name="e">event args</param>
    protected void ParseButton_Click(object sender, EventArgs e)
    {
            ////List<Information> val = new List<Information>(
            ////        new Information[] { 
            ////            Information.Title,
            ////            Information.FamilyName,
            ////            Information.GivenName,
            ////            Information.Address,
            ////            Information.Postcode,
            ////            Information.DateOfBirth,
            ////            Information.Age,
            ////            Information.Gender,
            ////            Information.NhsNumber
            ////        });
            ////this.PatientSearchInputBox1.InformationFormat = val;
            ////this.PatientSearchInputBox1.InformationDelimiter = '#';

        this.PatientSearchInputBox1.Parse();
        this.FamilyNameTextBox.Text = this.PatientSearchInputBox1.FamilyName;
        this.GivenNameTextBox.Text = this.PatientSearchInputBox1.GivenName;
        this.NHSNumberTextBox.Text = this.PatientSearchInputBox1.NhsNumber;
        if (this.PatientSearchInputBox1.Age != -1)
        {
            this.AgeTextBox.Text = this.PatientSearchInputBox1.Age.ToString(CultureInfo.CurrentCulture);
        }

        if (this.PatientSearchInputBox1.DateOfBirth.DateValue != DateTime.MinValue)
        {
            this.DOBTextBox.Text = this.PatientSearchInputBox1.DateOfBirth.ToString();
        }

        this.GenderTextBox.Text = this.PatientSearchInputBox1.Gender.ToString();
        this.TitleTextBox.Text = this.PatientSearchInputBox1.Title;
        this.AddressTextBox.Text = this.PatientSearchInputBox1.Address;
        this.PostcodeTextBox.Text = this.PatientSearchInputBox1.Postcode;
    }
}
