//-----------------------------------------------------------------------
// <copyright file="NameLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>03-Jan-2007</date>
// <summary>The control used to display a name.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Drawing;
    using Microsoft.Security.Application;

    /// <summary>
    /// The control used to display the patient's name. 
    /// </summary>
    [DefaultProperty("LastName")]
    [ToolboxData("<{0}:NameLabel FamilyName=\"FamilyName\" GivenName=\"GivenName\" Title=\"Title\" runat=server></{0}:NameLabel>")]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "NameLabel.bmp")]
    public class NameLabel : WebControl
    {
        #region Constructors
        /// <summary>
        /// Constructs a NameLabel object. 
        /// </summary>
        public NameLabel() : base(HtmlTextWriterTag.Span)
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the family name. 
        /// </summary>
        /// <remarks>
        /// The NameLabel control capitalizes the family name. The maximum display length is 40 characters. If the data exceeds 40 characters, 
        /// it is truncated to 37 characters plus an ellipsis.
        /// </remarks>
        [Bindable(true), Category("Patient Name"), DefaultValue("")]
        [Description("The Family Name field of the Patient Name.")]
        public string FamilyName
        {
            get
            {
                string familyName = (string)this.ViewState["FamilyName"];

                return familyName ?? string.Empty;
            }

            set
            {
                this.ViewState["FamilyName"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the given name. 
        /// </summary>
        /// <remarks>
        /// The maximum display length is 40 characters. If the data exceeds 40 characters, it is truncated to 
        /// 37 characters plus an ellipsis. 
        /// </remarks>
        [Bindable(true), Category("Patient Name"), DefaultValue("")]
        [Description("The Given Name field of the Patient Name.")]
        public string GivenName
        {
            get
            {
                string givenName = (string)this.ViewState["GivenName"];

                return givenName ?? string.Empty;
            }

            set
            {
                this.ViewState["GivenName"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the patient's title. 
        /// </summary>
        /// <remarks>
        /// The NameLabel control puts parentheses around the title. The title is only dispayed if one of 
        /// <see cref="P:NhsCui.Toolkit.Web.NameLabel.FamilyName">FamilyName</see> or 
        /// <see cref="P:NhsCui.Toolkit.Web.NameLabel.GivenName">GivenName</see>
        /// is provided. The maximum display length is 35 characters, including parentheses and three spaces.
        /// </remarks>
        [Bindable(true), Category("Patient Name"), DefaultValue("")]
        [Description("The Title field of the Patient Name.")]
        public string Title
        {
            get
            {
                string title = (string)this.ViewState["Title"];

                return title ?? string.Empty;
            }

            set
            {
                this.ViewState["Title"] = value;
            }
        }

        /// <summary>
        /// Gets the correctly-formatted aggregate value of the patient's name.  
        /// </summary>
        /// <remarks>
        /// The patient's name takes the form "FAMILYNAME, GivenName (Title)". The maximum total display length is 120 characters. 
        /// </remarks>
        [Category("Patient Name")]
        [Description("The ReadOnly DisplayValue field of the Patient Name.")]
        public string DisplayValue
        {
            get
            {
                return PatientName.Format(this.FamilyName, this.GivenName, this.Title);
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// true if any of the control's data properties have been set
        /// </summary>
        private bool HasData
        {
            get
            {
                return this.FamilyName.Length > 0 || 
                        this.GivenName.Length > 0 || 
                        this.Title.Length > 0;
            }
        }
        #endregion

        #region Protected Methods

        /// <summary>
        /// Renders the contents of the control to the specified writer
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output stream to render 
        /// HTML content on the client. </param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            string displayValue = this.DisplayValue;
            if (displayValue.Length > 0)
            {
                writer.Write(AntiXss.HtmlEncode(this.DisplayValue));
            }
            else
            {
                writer.Write("&nbsp;");
            }
        }

        #endregion
    }
}
