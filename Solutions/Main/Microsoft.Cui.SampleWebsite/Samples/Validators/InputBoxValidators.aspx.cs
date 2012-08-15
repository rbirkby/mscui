// <copyright file="InputBoxValidators.aspx.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>InputBox validator control sample page</summary>

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
/// InputBoxValidators sample file
/// </summary>
public partial class SamplesValidatorsInputBoxValidators : System.Web.UI.Page
{
    /// <summary>
    /// Load time prcoessing
    /// </summary>
    /// <param name="e">Event Argument.</param>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }
    
    /// <summary>
    /// PreRender time processing
    /// </summary>
    /// <param name="e">Event Argument.</param>
    protected override void OnPreRender(EventArgs e)
    {
        if (this.ValidationSummaryOnRadioButton.Checked)
        {
            this.ValidationSummary1.Visible = true;
        }
        else
        {
            this.ValidationSummary1.Visible = false;
        }
                
        foreach (IValidator val in this.Page.Validators)
        {
            BaseValidator validator = val as BaseValidator;

            if (this.ToggleValidationCheckBox.Checked)
            {
                validator.Enabled = true;
                validator.Visible = true;
            }
            else
            {
                validator.Visible = false;
                validator.Enabled = false;
            }

            if (this.FocusOnErrorTrueRadioButton.Checked)
            {
                validator.SetFocusOnError = true;
            }
            else
            {
                validator.SetFocusOnError = false;
            }

            if (this.ValidateOnBlurRadioButton.Checked)
            {
                validator.EnableClientScript = true;
            }
            else
            {
                validator.EnableClientScript = false;
            }
        }

        base.OnPreRender(e);
    }
}

