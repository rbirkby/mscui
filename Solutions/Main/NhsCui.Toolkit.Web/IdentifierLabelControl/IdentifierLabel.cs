//-----------------------------------------------------------------------
// <copyright file="IdentifierLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to display a unique identifier. </summary>
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

    /// <summary>
    /// The control used to display a unique identifier. 
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:IdentifierLabel IdentifierType=\"Other\" Text=\"XXX XXX XXXX\" runat=\"server\"></{0}:IdentifierLabel>")]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "IdentifierLabel.bmp")]
    public class IdentifierLabel : WebControl
    {
        #region Member Variables
        /// <summary>
        /// If the identifier type is nhsnumber keep a cache of the number in this
        /// format
        /// </summary>
        private NhsNumber nhsNumber;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs an IdentifierLabel object. 
        /// </summary>
        public IdentifierLabel() : base(HtmlTextWriterTag.Span)
        {
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether to process the identifier with the 
        /// NhsNumber validation checksum. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Other". If this is set to "Other", 
        /// no validation is performed. If this is set to "NhsNumber", the text must be a valid NHS number.
        /// </remarks>
        [Bindable(true), Category("Behavior"), DefaultValue(IdentifierType.Other)]
        [Description("The type of the identifier. If set to NhsNumber then the Text property must be set to a valid NhsNumber.")]
        public IdentifierType IdentifierType
        {
            get
            {
                object identifierTypeObject = this.ViewState["IdentifierType"];

                if (identifierTypeObject != null)
                {
                    return (IdentifierType)identifierTypeObject;
                }

                return IdentifierType.Other;
            }

            set
            {
                try
                {
                    if (!Enum.IsDefined(typeof(IdentifierType), value))
                    {
                        throw new ArgumentOutOfRangeException("value");
                    }

                    this.RefreshNhsNumberCache(value, this.Text);

                    if (value == IdentifierType.Other)
                    {
                        // no need to save default value in viewstate
                        this.ViewState["IdentifierType"] = null;
                    }
                    else
                    {
                        this.ViewState["IdentifierType"] = value;
                    }

                    this.LastIdentifierValid = true;
                }
                catch
                {
                    this.LastIdentifierValid = false;
                    throw;
                }
           }
        }

        /// <summary>
        /// Gets or sets a unique identifier. 
        /// </summary>
        /// <remarks>
        /// Defaults to "xxx-xxx-xxxx". This property is mandatory if 
        /// <see cref="P:NhsCui.Toolkit.Web.IdentifierLabel.IdentifierType">IdentifierType</see> is set to "NhsNumber". 
        /// </remarks>
        [Bindable(true), Category("Behavior"), Localizable(false)]
        [Description("The text associated with the IdentifierLabel control. Must be set to a valid NhsNumber if the control's IdentifierType is NhsNumber.")]
        public string Text
        {
            get
            {
                if (this.Value != null)
                {
                    return this.Value.ToString();
                }
                else
                {
                    string text = (string)this.ViewState["Text"];

                    return (text == null ? string.Empty : text);
                }
            }

            set
            {
                try
                {
                    this.RefreshNhsNumberCache(this.IdentifierType, value);
                    this.ViewState["Text"] = value;
                    this.LastIdentifierValid = true;
                }
                catch
                {
                    this.LastIdentifierValid = false;
                    throw;
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the value of the unique identifier, such as an NHS number.
        /// </summary>
        /// <remarks>
        /// If <see cref="P:NhsCui.Toolkit.Web.IdentifierLabel.IdentifierType">IdentifierType</see> is set to "NhsNumber", 
        /// the value will be the NHS number.
        ///</remarks>
        [Bindable(true), Category("Behavior"), DefaultValue(null)]
        [Description("The NhsNumber associated with the IdentifierLabel control. Will be null if the control's IdentifierType is Other. Not browsable.")]
        public NhsNumber Value
        {
            get
            {
                return this.nhsNumber;
            }

            set
            {
                this.nhsNumber = value;

                // the default identifier type is nhs number
                this.ViewState["IdentifierType"] = null;
                this.ViewState["Text"] = (value == null ? null : value.ToString());
            }
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// whether the last attempt to set the patient identifier was successful
        /// </summary>
        private bool LastIdentifierValid
        {
            get
            {
                return (this.ViewState["LastIdentifierValid"] != null);
            }

            set
            {
                if (value)
                {
                    this.ViewState["LastIdentifierValid"] = true;
                }
                else
                {
                    this.ViewState["LastIdentifierValid"] = null;
                }
            }
        }
        #endregion

        #region Protected Methods

        /// <summary>
        /// Restores view-state information from a previous page request 
        /// that was saved by the SaveViewState method. 
        /// </summary>
        /// <param name="savedState">An Object that represents the control state to be restored. </param>
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(null);

            // create nhs number object if required from values loaded from viewstate
            this.RefreshNhsNumberCache(this.IdentifierType, this.Text);
        }

        /// <summary>
        /// Adds HTML attributes and styles that need to be rendered to the specified HtmlTextWriterTag.
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output stream to render HTML content on the client. </param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            // nhs number should never wrap
            // writer.AddStyleAttribute(HtmlTextWriterStyle.WhiteSpace, "nowrap");
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output stream to render 
        /// HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (this.LastIdentifierValid)
            {
                writer.Write(AntiXss.HtmlEncode(this.Text));
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Refresh our cache of nhs number, this method will throw an exception if
        /// identifierType is NhsNumber and text is not a valid nhs number
        /// </summary>
        /// <param name="identifierType">Identifier type</param>
        /// <param name="text">identifier text</param>
        private void RefreshNhsNumberCache(IdentifierType identifierType, string text)
        {
            if (identifierType == IdentifierType.NhsNumber && text.Length > 0)
            {
                this.nhsNumber = new NhsNumber(text);
            }
            else
            {
                this.nhsNumber = null;
            }
        }
        #endregion

        #region Explict Property Resets
        /// <summary>
        /// custom reset method for the text property
        /// </summary>
        private void ResetText()
        {
            this.IdentifierType = IdentifierType.Other;
            this.Text = string.Empty;
        }

        #endregion
    }
}
