//-----------------------------------------------------------------------
// <copyright file="TimeSpanInputBoxValidator.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>16-May-2007</date>
// <summary>Validation control for the NhsCui.Toolkit.Web.TimeSpanInputBox</summary>
//-----------------------------------------------------------------------
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.TimeSpanInputBoxControl.TimeSpanInputBoxValidator.js", "application/x-javascript")]
namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Drawing;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using NhsCui.Toolkit.DateAndTime;
    using NhsCui.Toolkit.Web;

    /// <summary>
    /// Validation control for the NhsCui.Toolkit.Web.TimeSpanInputBox
    /// </summary>
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "TimeSpanInputBoxValidator.bmp")]
    [ToolboxData("<{0}:TimeSpanInputBoxValidator runat=\"server\" ErrorMessage=\"TimeSpanInputBoxValidator\"></{0}:TimeSpanInputBoxValidator>")]
    public class TimeSpanInputBoxValidator : BaseValidator
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
                TimeSpanInputBox control = this.NamingContainer.FindControl(this.ControlToValidate) as TimeSpanInputBox;

                if (control == null)
                {
                    throw new HttpException(TimeSpanInputBoxControl.TimeSpanInputBoxValidatorResources.ControlToValidateInvalid);
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

            NhsTimeSpan tryParseResult;

            return (NhsTimeSpan.TryParse(value, out tryParseResult, System.Threading.Thread.CurrentThread.CurrentCulture, ((TimeSpanInputBox)this.NamingContainer.FindControl(this.ControlToValidate)).IsAge));
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
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "TimeSpanInputBoxValidatorEvaluateIsValid");
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "valtype", "NhsCui.TimeSpanInputBoxValidator");

            this.Page.ClientScript.RegisterClientScriptResource(typeof(NhsCui.Toolkit.Web.TimeSpanInputBoxValidator), "NhsCui.Toolkit.Web.TimeSpanInputBoxControl.TimeSpanInputBoxValidator.js");
        }
    }
}
