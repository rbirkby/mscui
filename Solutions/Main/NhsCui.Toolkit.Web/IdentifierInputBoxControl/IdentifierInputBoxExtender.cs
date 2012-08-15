//-----------------------------------------------------------------------
// <copyright file="IdentifierInputBoxExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>IdentifierInputBox Extender, class to provide server-side configuration 
// of the IdentifierInputBox's AJAX functionality</summary>
//-----------------------------------------------------------------------

#region [ Resources ]

[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.IdentifierInputBoxControl.IdentifierInputBox.js", "text/javascript")]

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

    /// <summary>
    /// IdentifierInputBox Extender, class to provide server-side configuration of 
    /// the IdentifierInputBox's AJAX functionality
    /// </summary>
    [ToolboxItem(false)]
    [TargetControlType(typeof(TextBox))]
    [RequiredScript(typeof(CommonToolkitScripts), 0)]
    [RequiredScript(typeof(CommonNhsCuiToolkitScripts), 1)]
    [RequiredScript(typeof(NhsNumberScripts), 3)]
    [ClientScriptResource("NhsCui.Toolkit.Web.IdentifierInputBox", "NhsCui.Toolkit.Web.IdentifierInputBoxControl.IdentifierInputBox.js")]
    public class IdentifierInputBoxExtender : ExtenderControlBase
    {
        #region Member Variables

        /// <summary>
        /// If the identifier type is nhsnumber keep a cache of the number in this
        /// format
        /// </summary>
        private NhsNumber nhsNumber;

        /// <summary>
        /// object to hold our state
        /// </summary>
        private IdentifierInputClientState state;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public IdentifierInputBoxExtender()
        {
            this.EnableClientState = true;
            this.state = new IdentifierInputClientState();
            this.ClientStateValuesLoaded += new EventHandler(this.IdentifierInputBoxExtender_ClientStateValuesLoaded);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a unique identifier. 
        /// </summary>
        /// <remarks>
        /// Defaults to "xxx-xxx-xxxx". This property is mandatory if 
        /// <see cref="P:NhsCui.Toolkit.Web.IdentifierLabel.IdentifierType">IdentifierType</see> is set to "NhsNumber". 
        /// </remarks>
        [Bindable(true), Category("Behavior"), Localizable(false), DefaultValue(" ")]
        [Description("The text associated with the IdentifierInputBox control. Must be set to a valid NhsNumber if the control's IdentifierType is NhsNumber.")]
        [ExtenderControlProperty]
        [ClientPropertyName("text")]
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
                    this.ViewState["Text"] = new NhsNumber(value).ToString();
                    (this.TargetControl as TextBox).Text = this.ViewState["Text"].ToString();
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
        [Description("The NhsNumber associated with the IdentifierInputBox control. Will be null if the control's IdentifierType is Other. Not browsable.")]
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
                (this.TargetControl as TextBox).Text = this.ViewState["Text"].ToString();
            }
        }

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
        /// Value to use as the delimiter character.
        /// </summary>
        [DefaultValue(" ")]
        [ExtenderControlProperty]
        [ClientPropertyName("delimiterCharacter")]
        public virtual string DelimiterCharacter
        {
            get
            {
                return GetPropertyValue("DelimiterCharacter", " ");
            }

            set
            {
                SetPropertyValue("DelimiterCharacter", value);
            }
        }

        #endregion

        #region Private Properties

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

        #region Private Methods

        /// <summary>
        /// Need to make sure control is autocomplete-disabled
        /// </summary>
        /// <param name="writer">HTML TextWriter</param>
        public override void RenderControl(HtmlTextWriter writer)
        {
            base.RenderControl(writer);
            (this.TargetControl as TextBox).Attributes.Add("autocomplete", "off");
        }
        #endregion

        #region Public Methods

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

        /// <summary>
        /// /// Handle loading of client state
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void IdentifierInputBoxExtender_ClientStateValuesLoaded(object sender, EventArgs e)
        {
            if (this.ClientState != null)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();

                // May need to create one...
                // jss.RegisterConverters(new JavaScriptConverter[] { new IdentifierJavascriptConverter() });

                this.state = jss.Deserialize<IdentifierInputClientState>(ClientState);
            }
        }

        #endregion
    }
}
