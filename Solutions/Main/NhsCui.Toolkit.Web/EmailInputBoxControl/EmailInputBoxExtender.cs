//-----------------------------------------------------------------------
// <copyright file="EmailInputBoxExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>14-Aug-2007</date>
// <summary>EmailInputBox Extender, class to provide server-side configuration 
// of the EmailInputBox's AJAX functionality</summary>
//-----------------------------------------------------------------------

#region [ Resources ]

[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.EmailInputBoxControl.EmailInputBox.js", "text/javascript")]

#endregion

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel;
    using System.Web.UI;
    using AjaxControlToolkit;
    using System.Web.UI.WebControls;
    using System.Web.Script.Serialization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// EmailInputBox Extender, class to provide server-side configuration of 
    /// the EmailInputBox's AJAX functionality
    /// </summary>
    [ToolboxItem(false)]
    [TargetControlType(typeof(TextBox))]
    [RequiredScript(typeof(CommonToolkitScripts), 0)]
    [RequiredScript(typeof(CommonNhsCuiToolkitScripts), 1)]
    [ClientScriptResource("NhsCui.Toolkit.Web.EmailInputBox", "NhsCui.Toolkit.Web.EmailInputBoxControl.EmailInputBox.js")]
    public class EmailInputBoxExtender : ExtenderControlBase
    {
        /// <summary>
        /// RegEx to check for valid email addresses
        /// </summary>
        private Regex emailRegEx = new Regex(@"^[A-Z0-9._%-]+@(?:[A-Z0-9-]+\.)+[A-Z]{2,4}$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);

        /// <summary>
        /// Default Constructor
        /// </summary>
        public EmailInputBoxExtender()
        {
        }

        /// <summary>
        /// The email address entered in the TextBox
        /// </summary>
        [Description("The email address entered in the TextBox")]
        public string Value
        {
            get
            {
                TextBox targetControl = ((TextBox)this.FindControl(TargetControlID));
                if (targetControl != null)
                {
                    return targetControl.Text;
                }

                return string.Empty;
            }

            set
            {
                TextBox targetControl = ((TextBox)this.FindControl(TargetControlID));
                if (targetControl != null)
                {
                    if (this.emailRegEx.IsMatch(value) == true)
                    {
                        this.SetPropertyValue("Text", targetControl.Text);
                    }
                    else
                    {
                        throw new ArgumentException("value");
                    }
                }
            }
        }

        /// <summary>
        /// Autocomplete interferes with any attached validators - so need to disable
        /// </summary>
        /// <param name="e">event args</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ((TextBox)this.FindControl(TargetControlID)).AutoCompleteType = AutoCompleteType.Disabled;
        }
    }
}
