//-----------------------------------------------------------------------
// <copyright file="DateInputBoxValidator.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Validation control for the NhsCui.Toolkit.Web.DateInputBox</summary>
//-----------------------------------------------------------------------
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.DateInputBoxControl.DateInputBoxValidator.js", "application/x-javascript")]
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
    using NhsCui.Toolkit.DateAndTime;
    using NhsCui.Toolkit.Web;
    using System.Globalization;
            
    /// <summary>
    /// Validation control for the NhsCui.Toolkit.Web.DateInputBox
    /// </summary>
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "DateInputBoxValidator.bmp")]
    [ToolboxData("<{0}:DateInputBoxValidator runat=\"server\" ErrorMessage=\"DateInputBoxValidator\"></{0}:DateInputBoxValidator>")]
    public class DateInputBoxValidator : BaseValidator
    {
        /// <summary>
        /// Holds the watermark text
        /// </summary>
        private string watermarkText = "dd-MMM-yyyy";
        
        /// <summary>
        /// The DateInputBox uses a Watermark Extender which places text into the DateInputBox when it is empty.
        /// This text is more than likely an Invalid date 
        /// </summary>
        [Category("Behavior")]
        [Description("The DateInputBox uses a Watermark Extender which places text into the DateInputBox when it is empty. Use this Text as aprt of the IsEmpty check")]
        [DefaultValue("dd-MMM-yyyy")]
        public string WatermarkText
        {
            get
            {
                return this.watermarkText;
            }

            set
            {
                this.watermarkText = value;
            }
        }
        
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
                DateInputBox control = this.NamingContainer.FindControl(this.ControlToValidate) as DateInputBox;

                if (control == null)
                {
                    throw new HttpException(DateInputBoxControl.DateInputBoxValidatorResources.ControlToValidateInvalid);
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
            DateInputBox control = this.NamingContainer.FindControl(this.ControlToValidate) as DateInputBox;
            if (value.Length == 0)
            {
                return true;
            }

            if (value == this.WatermarkText)
            {
                // Essentially box is empty so return valid
                return true;
            }

            // Don't raise an error for Today, Tomorrow if value is also the same.
            if (value == control.Value.ToString(control.DisplayDayOfWeek, false, control.DisplayDateAsText, CultureInfo.CurrentCulture))
            {
                return true;
            }

            NhsDate tryParseResult;

            if (!NhsDate.TryParseExact(value, out tryParseResult, System.Threading.Thread.CurrentThread.CurrentCulture))
            {
                control.Value.DateType = DateType.Null;
                return false;                                
            }

            return true;
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
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "DateInputBoxValidatorEvaluateIsValid");
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "valtype", "NhsCui.DateInputBoxValidator");
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "watermarktext", this.WatermarkText);
            
            this.Page.ClientScript.RegisterClientScriptResource(typeof(NhsCui.Toolkit.Web.DateInputBoxValidator), "NhsCui.Toolkit.Web.DateInputBoxControl.DateInputBoxValidator.js");
        }       
    }
}
