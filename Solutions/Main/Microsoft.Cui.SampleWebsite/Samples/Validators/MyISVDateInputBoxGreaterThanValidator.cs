// <copyright file="MyIsvDateInputBoxGreaterThanValidator.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>MyIsvDateInputBoxGreaterThanValidator Validator sample</summary>

[assembly: System.Web.UI.WebResource("Microsoft.Cui.SampleWebsite.Samples.Validators.MyIsvDateInputBoxGreaterThanValidator.js", "application/x-javascript")]
namespace Microsoft.Cui.SampleWebsite.Samples.Validators
{
    using System;
    using System.Data;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using NhsCui.Toolkit.DateAndTime;
    using NhsCui.Toolkit.Web;
        
    /// <summary>
    /// Sample ISV InputBox Validator
    /// </summary>
    public class MyIsvDateInputBoxGreaterThanValidator : BaseValidator
    {
        /// <summary>
        /// Gets or sets the ID of the DateInputBox we are comparing to
        /// </summary>
        public string DateBoxToCompare
        {
            get
            {
                object obj2 = this.ViewState["DateBoxToCompare"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }

                return string.Empty;
            }

            set
            {
                this.ViewState["DateBoxToCompare"] = value;
            }
        }
        
        /// <summary>
        /// Determines whether the control specified by the ControlToValidate property is a valid control.
        /// </summary>
        /// <returns>True if control properties is valid</returns>
        protected override bool ControlPropertiesValid()
        {
            // Call the base implementation of ControlPropertiesValid first. If that passes run the extra checks 
            if (base.ControlPropertiesValid() == true)
            {
                DateInputBox control = this.NamingContainer.FindControl(this.ControlToValidate) as DateInputBox;

                if (control == null)
                {
                    throw new HttpException("Bad control type for ControlToValidate");
                }
                else
                {
                    DateInputBox controlToCompare = this.NamingContainer.FindControl(this.DateBoxToCompare) as DateInputBox;

                    if (control == null)
                    {
                        throw new HttpException("Bad control type for DateBoxToCompare");
                    }
                    else
                    {
                        return true;
                    }
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

            NhsDate tryParseResult;

            return (NhsDate.TryParseExact(value, out tryParseResult, System.Threading.Thread.CurrentThread.CurrentCulture));
        }

        /// <summary>
        /// PreRender time processing
        /// </summary>
        /// <param name="e">Event Argument.</param>
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
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "MyIsvDateInputBoxGreaterThanValidatorEvaluateIsValid");
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "valtype", "Microsoft.Cui.SampleWebsite.Samples.Validators.MyIsvDateInputBoxGreaterThanValidator");
            this.Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "comparedatebox", this.GetControlRenderID(this.DateBoxToCompare));

            this.Page.ClientScript.RegisterClientScriptResource(typeof(Microsoft.Cui.SampleWebsite.Samples.Validators.MyIsvDateInputBoxGreaterThanValidator), "Microsoft.Cui.SampleWebsite.Samples.Validators.MyIsvDateInputBoxGreaterThanValidator.js");
        }
    }
}
