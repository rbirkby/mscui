//-----------------------------------------------------------------------
// <copyright file="IdentifierInputBoxValidator.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>13-Sep-2007</date>
// <summary>Validation control for the NhsCui.Toolkit.Web.IdentifierInputBox</summary>
//-----------------------------------------------------------------------
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.IdentifierInputBoxControl.IdentifierInputBoxValidator.js", "application/x-javascript")]
namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.ComponentModel;
    using NhsCui.Toolkit.Web;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Validation control for the NhsCui.Toolkit.Web.IdentifierInputBox
    /// </summary>
    [ToolboxItem(false)]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "IdentifierInputBoxValidator.bmp")]
    [ToolboxData("<{0}:IdentifierInputBoxValidator runat=\"server\" ErrorMessage=\"IdentifierInputBoxValidator\"></{0}:IdentifierInputBoxValidator>")]
    public class IdentifierInputBoxValidator : BaseValidator
    {
        /// <summary>
        /// Determines whether the control specified by the ControlToValidate property is a valid control.
        /// </summary>
        /// <returns>True if control properties is valid</returns>
        protected override bool ControlPropertiesValid()
        {
            // Call the base implementation of ControlPropertiesValid first. 
            // If that passes run the extra checks 
            if (base.ControlPropertiesValid() == true)
            {
                TextBox control = this.NamingContainer.FindControl(this.ControlToValidate) as TextBox;

                if (control == null)
                {
                    throw new HttpException(IdentifierInputBoxControl.IdentifierInputBoxValidatorResources.ControlToValidateInvalid);
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if the ControlToValidate is valid
        /// </summary>
        /// <returns>True if control properties is valid</returns>
        protected override bool EvaluateIsValid()
        {
            string value = this.GetControlValidationValue(this.ControlToValidate);

            if (value.Length == 0)
            {
                return true;
            }

            string tryParseResult;

            return (NhsNumber.TryParseNhsNumber(value, out tryParseResult) == NhsNumber.NhsNumberParseResult.Success);
        }

        /// <summary>
        /// PreRender time processing
        /// </summary>
        /// <param name="e">e</param>
        protected override void OnPreRender(EventArgs e)
        {
            if (this.EnableClientScript == true)
            {
                if (HttpContext.Current.Request.Browser.EcmaScriptVersion.Major >= 1)
                {
                    this.RegisterClientScript();
                }
            }

            base.OnPreRender(e);
        }

        /// <summary>
        /// Set up client script 
        /// </summary>
        private void RegisterClientScript()
        {
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "IdentifierInputBoxValidatorEvaluateIsValid");
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "valtype", "NhsCui.IdentifierInputBoxValidator");

            this.Page.ClientScript.RegisterClientScriptResource(typeof(NhsCui.Toolkit.Web.IdentifierInputBoxValidator), "NhsCui.Toolkit.Web.IdentifierInputBoxControl.IdentifierInputBoxValidator.js");
        }
    }
}
