//-----------------------------------------------------------------------
// <copyright file="ContactLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to hold contact details. </summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Drawing;
    using Microsoft.Security.Application;
    using NhsCui.Toolkit;

    /// <summary>
    /// The control used to display contact details. 
    /// </summary>
    [DefaultProperty("HomePhoneNumber")]
    [ToolboxData("<{0}:ContactLabel runat=server></{0}:ContactLabel>")]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "ContactLabel.bmp")]
    public class ContactLabel : WebControl
    {
        #region Private Fields
        /// <summary>
        /// Indicates whether to render wrappable strings or normal string
        /// </summary>
        /// <remarks>
        /// Wrappable strings will be rendered when used within patient banner only. Defaults to false
        /// </remarks>
        private bool renderWrappableStrings;

        /// <summary>
        /// Indicates whether to wrap labels
        /// </summary>        
        private bool wrapLabels;

        /// <summary>
        /// Widths of the labels column
        /// </summary>
        private Unit labelsColumnWidth = Unit.Empty;

        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a ContactLabel object. 
        /// </summary>
        public ContactLabel() : base(HtmlTextWriterTag.Table)
        {
        }
        #endregion

        #region Public Properties        

        /// <summary>
        /// Gets or sets the contact's home phone number. 
        /// </summary>
        ///  <remarks>
        /// Defaults to "". If this element is left empty, <see cref="P:NhsCui.Toolkit.Web.ContactLabel.HomePhoneLabelText">HomePhoneLabelText</see> 
        /// will be displayed with blank space next to it. 
        /// </remarks>    
        [Bindable(true), Category("Patient Contact"), DefaultValue("")]
        [Description("The Home Phone Number field of the Patient Contact.")]
        public string HomePhoneNumber
        {
            get
            {
                string homePhoneNumber = (string)this.ViewState["HomePhoneNumber"];

                return homePhoneNumber ?? string.Empty;
            }

            set
            {
                if (value != null)
                {
                    this.ViewState["HomePhoneNumber"] = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the contact's work phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "". If this element is left empty, <see cref="P:NhsCui.Toolkit.Web.ContactLabel.WorkPhoneLabelText">WorkPhoneLabelText</see> 
        /// will be displayed with blank space next to it. 
        /// </remarks>
        [Bindable(true), Category("Patient Contact"), DefaultValue("")]
        [Description("The Work Phone Number field of the Patient Contact.")]
        public string WorkPhoneNumber
        {
            get
            {
                string workPhoneNumber = (string)this.ViewState["WorkPhoneNumber"];

                return workPhoneNumber ?? string.Empty;
            }

            set
            {
                if (value != null)
                {
                    this.ViewState["WorkPhoneNumber"] = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the contact's mobile phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "". If this element is left empty, <see cref="P:NhsCui.Toolkit.Web.ContactLabel.MobilePhoneLabelText">MobilePhoneLabelText</see> 
        /// will be displayed with blank space next to it. 
        /// </remarks>
        [Bindable(true), Category("Patient Contact"), DefaultValue("")]
        [Description("The Mobile Phone Number field of the Patient Contact.")]
        public string MobilePhoneNumber
        {
            get
            {
                string mobilePhoneNumber = (string)this.ViewState["MobilePhoneNumber"];

                return mobilePhoneNumber ?? string.Empty;
            }

            set
            {
                if (value != null)
                {
                    this.ViewState["MobilePhoneNumber"] = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the contact's email address. 
        /// </summary>
        /// <remarks>
        /// Defaults to "". If this element is left empty, <see cref="P:NhsCui.Toolkit.Web.ContactLabel.EmailLabelText">EmailLabelText</see> will be displayed 
        /// with blank space next to it. 
        /// </remarks>
        [Bindable(true), Category("Patient Contact"), DefaultValue("")]
        [Description("The Email Address field of the Patient Contact.")]
        public string EmailAddress
        {
            get
            {
                string emailAddress = (string)this.ViewState["EmailAddress"];

                return emailAddress ?? string.Empty;
            }

            set
            {
                this.ViewState["EmailAddress"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the contact's home phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Home". 
        /// </remarks>
        [Category("Patient Contact"), Localizable(true)]
        [ResourceDefaultValue(typeof(ContactLabelControl.Resources), "HomePhoneLabelText")]
        [Description("The text associated with the Home Phone Number field of the Patient Contact.")]
        public string HomePhoneLabelText
        {
            get
            {
                string homePhoneLabelText = (string)this.ViewState["HomePhoneLabelText"];

                return homePhoneLabelText ?? ContactLabelControl.Resources.HomePhoneLabelText;
            }

            set
            {
                this.ViewState["HomePhoneLabelText"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the contact's work phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Work". 
        /// </remarks>
        [Category("Patient Contact"), Localizable(true)]
        [ResourceDefaultValue(typeof(ContactLabelControl.Resources), "WorkPhoneLabelText")]
        [Description("The text associated with the Work Phone Number field of the Patient Contact.")]
        public string WorkPhoneLabelText
        {
            get
            {
                string workPhoneLabelText = (string)this.ViewState["WorkPhoneLabelText"];

                return workPhoneLabelText ?? ContactLabelControl.Resources.WorkPhoneLabelText;
            }

            set
            {
                this.ViewState["WorkPhoneLabelText"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the contact's mobile phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Mobile". 
        /// </remarks>
        [Category("Patient Contact"), Localizable(true)]
        [ResourceDefaultValue(typeof(ContactLabelControl.Resources), "MobilePhoneLabelText")]
        [Description("The text associated with the Mobile Phone Number field of the Patient Contact.")]
        public string MobilePhoneLabelText
        {
            get
            {
                string mobilePhoneLabelText = (string)this.ViewState["MobilePhoneLabelText"];

                return mobilePhoneLabelText ?? ContactLabelControl.Resources.MobilePhoneLabelText;
            }

            set
            {
                this.ViewState["MobilePhoneLabelText"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the contact's email address. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Email". 
        /// </remarks>
        [Category("Patient Contact"), Localizable(true)]
        [ResourceDefaultValue(typeof(ContactLabelControl.Resources), "EmailLabelText")]
        [Description("The text associated with the Email Address field of the Patient Contact.")]
        public string EmailLabelText
        {
            get
            {
                string emailLabelText = (string)this.ViewState["EmailLabelText"];

                return emailLabelText ?? ContactLabelControl.Resources.EmailLabelText;
            }

            set
            {
                this.ViewState["EmailLabelText"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the style for the captions. 
        /// </summary>
        [Category("Styles"), DefaultValue("")]
        [Description("Gets or sets the style for the labels")]
        public string LabelStyle
        {
            get
            {
                string labelStyle = (string)this.ViewState["LabelStyle"];

                return labelStyle ?? string.Empty;
            }

            set
            {
                this.ViewState["LabelStyle"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the style for the data. 
        /// </summary>
        [Category("Styles"), DefaultValue("")]
        [Description("Gets or sets the style for the data")]
        public string DataStyle
        {
            get
            {
                string dataStyle = (string)this.ViewState["DataStyle"];

                return dataStyle ?? string.Empty;
            }

            set
            {
                this.ViewState["DataStyle"] = value;
            }
        }

        #endregion

        #region Internal Properties
        /// <summary>
        /// Indicates whether to render wrappable strings or normal string
        /// </summary>
        /// <remarks>
        /// Wrappable strings will be rendered when used within patient banner only. Defaults to false
        /// </remarks>
        internal bool RenderWrappableStrings
        {
            get
            {
                return this.renderWrappableStrings;
            }

            set
            {
                this.renderWrappableStrings = value;
            }
        }

        /// <summary>
        /// Indicates whether to render wrappable strings or normal string
        /// </summary>
        /// <remarks>
        /// Wrap Labels will be allow the Labels to be wrapped in the middle of a word. Defaults to false
        /// </remarks>
        internal bool WrapLabels
        {
            get
            {
                return this.wrapLabels;
            }

            set
            {
                this.wrapLabels = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the labels column
        /// </summary>
        /// <remarks>
        /// Defaults to empty. Width will be set only when specified
        /// </remarks>
        internal Unit LabelsColumnWidth
        {
            get
            {
                return this.labelsColumnWidth;
            }

            set
            {
                this.labelsColumnWidth = value;
            }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Gets the contact details summary
        /// </summary>
        /// <returns>
        /// returns the first non empty number from Home phone number/Work phone number/Mobile phone number
        /// </returns>
        internal string GetContactDetailsSummary()
        {
            string summary = string.Empty;
            if (!string.IsNullOrEmpty(this.HomePhoneNumber))
            {
                summary = this.HomePhoneNumber;
            }
            else if (!string.IsNullOrEmpty(this.MobilePhoneNumber))
            {
                summary = this.MobilePhoneNumber;
            }
            else if (!string.IsNullOrEmpty(this.WorkPhoneNumber))
            {
                summary = this.WorkPhoneNumber;
            }
            else if (!string.IsNullOrEmpty(this.EmailAddress))
            {
                summary = this.EmailAddress;
            }

            return summary;
        }

        #endregion

        #region Protected Methods
        /// <summary>
        /// Adds HTML attributes and styles that need to be rendered to the specified HtmlTextWriterTag. 
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output stream to render HTML content on the client. </param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output stream to render 
        /// HTML content on the client. </param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            // label text
            string[] contactLabels = new string[]
                    {
                        this.HomePhoneLabelText, this.WorkPhoneLabelText,
                        this.MobilePhoneLabelText, this.EmailLabelText
                    };

            // values
            string[] contactValues = new string[]
                    {
                        this.HomePhoneNumber, this.WorkPhoneNumber,
                        this.MobilePhoneNumber, this.EmailAddress
                    };

            // id postfixes (requested by testing)
            string[] contactLabelIds = new string[]
                    {
                        "_homephone_label", "_workphone_label",
                        "_mobilephone_label", "_email_label"
                    };
            string[] contactValueIds = new string[]
                    {
                        "_homephone", "_workphone",
                        "_mobilephone", "_email"
                    };

            string dataStyle = this.DataStyle;
            string labelStyle = this.LabelStyle;

            for (int i = 0; i < contactLabels.Length; i++)
            {
                // render tr element for each contact item
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                // render span to hold label
                if (labelStyle.Length > 0)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, labelStyle);
                }

                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + contactLabelIds[i]);

                if (this.labelsColumnWidth != Unit.Empty)
                {
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.labelsColumnWidth.ToString(CultureInfo.CurrentCulture));
                }

                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                if (contactLabels[i] != null)
                {
                    if (this.wrapLabels)
                    {
                        writer.Write(StringUtil.GetWrappableString(contactLabels[i]));
                    }
                    else
                    {
                        writer.Write(contactLabels[i]);
                    }
                }

                writer.RenderEndTag();

                // render contact value, render in span with id to help testing
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + contactValueIds[i]);
                if (dataStyle.Length > 0)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, dataStyle);
                }

                writer.RenderBeginTag(HtmlTextWriterTag.Td);

                if (contactValues[i] != null)
                {
                    if (this.renderWrappableStrings)
                    {
                        writer.Write(StringUtil.GetAntiXssWrappableString(contactValues[i]));
                    }
                    else
                    {
                        writer.Write(contactValues[i]);
                    }                    
                }

                writer.RenderEndTag();

                writer.RenderEndTag();
                writer.WriteLine();
            }
        }
        #endregion
    }
}
