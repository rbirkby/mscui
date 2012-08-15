//-----------------------------------------------------------------------
// <copyright file="AddressLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to hold an address. </summary>
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
    /// The control used to display an address.
    /// </summary>
    [DefaultProperty("Address1")]
    [ToolboxData("<{0}:AddressLabel Address1=\"Address1\" Address2=\"Address2\" Town=\"Town\" Country=\"Country\" runat=server></{0}:AddressLabel>")]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "AddressLabel.bmp")]
    public class AddressLabel : WebControl
    {
        #region Private Member Variables
        /// <summary>
        /// Style to apply to individual contact items.
        /// </summary>
        private Style itemStyle;

        /// <summary>
        /// Indicates whether to render wrappable strings or normal string
        /// </summary>
        /// <remarks>
        /// Wrappable strings will be rendered when used within patient banner only. Defaults to false
        /// </remarks>
        private bool renderWrappableStrings;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs an AddressLabel object. 
        /// </summary>
        public AddressLabel() : base(HtmlTextWriterTag.Span)
        {
        }
        #endregion

        #region Private Delegates
        /// <summary>
        /// Delegate used to render an address item.
        /// </summary>
        /// <param name="writer">writer to output to</param>
        /// <param name="addressItem">address item to render</param>
        /// <param name="isfirstItem">true if first item to be rendered</param>
        private delegate void AddressItemRenderer(HtmlTextWriter writer, string addressItem, bool isfirstItem);
        #endregion

        #region Public Properties
        
        /// <summary>
        /// Gets or sets the first line of an address. 
        /// </summary>
        /// <remarks>
        ///All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The first field of the Patient Address.")]
        public string Address1
        {
            get
            {
                string address1 = (string)this.ViewState["Address1"];

                return address1 ?? string.Empty;
            }

            set
            {
                this.ViewState["Address1"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the second line of an address. 
        /// </summary>
        /// <remarks>
        ///All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The second field of the Patient Address.")]
        public string Address2
        {
            get
            {
                string address2 = (string)this.ViewState["Address2"];

                return address2 ?? string.Empty;
            }

            set
            {
                this.ViewState["Address2"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the third line of an address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The third field of the Patient Address.")]
        public string Address3
        {
            get
            {
                string address3 = (string)this.ViewState["Address3"];

                return address3 ?? string.Empty;
            }

            set
            {
                this.ViewState["Address3"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the town in an address. 
        /// </summary>
        /// <remarks>
        ///All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The Town field of the Patient Address.")]
        public string Town
        {
            get
            {
                string town = (string)this.ViewState["Town"];

                return town ?? string.Empty;
            }

            set
            {
                this.ViewState["Town"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the county in an address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The County field of the Patient Address.")]
        public string County
        {
            get
            {
                string county = (string)this.ViewState["County"];

                return county ?? string.Empty;
            }

            set
            {
                this.ViewState["County"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the postcode. 
        /// </summary> 
        ///<remarks>
        ///All stored address data should display. If an address element is empty, it will not be displayed. If the postcode is present, 
        /// it should display in capitalized form and as the final element before the <see cref="P:NhsCui.Toolkit.Web.AddressLabel.Country">Country</see>. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The Post Code field of the Patient Address.")]
        public string Postcode
        {
            get
            {
                string postCode = (string)this.ViewState["Postcode"];

                return postCode ?? string.Empty;
            }

            set
            {
                this.ViewState["Postcode"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the country in an address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The Country field of the Patient Address.")]
        public string Country
        {
            get
            {
                string country = (string)this.ViewState["Country"];

                return country ?? string.Empty;
            }

            set
            {
                this.ViewState["Country"] = value;
            }
        }

         /// <summary>
        /// Gets or sets the address layout to be either "inform" (vertical) or "inline" (horizontal).
        /// </summary>
        /// <remarks>
        /// If the address layout is set to "inform", each line contains a single, left-justified element with 
        /// no separator characters displayed. If the address layout is set to "inline", multiple elements display on a 
        /// single line, with address elements separated by a single comma and a single space. Individual address 
        /// elements should not split across multiple lines. 
        ///</remarks>
        [Category("Patient Address"), DefaultValue(AddressDisplayFormat.InForm)]
        [Description("Indicates whether the Patient Address is displayed in a vertical (InForm) or flowed (InLine) layout.")]
        public AddressDisplayFormat AddressDisplayFormat
        {
            get
            {
                object addressDisplayFormat = this.ViewState["AddressDisplayFormat"];

                return (addressDisplayFormat == null ? AddressDisplayFormat.InForm : 
                                    (AddressDisplayFormat)addressDisplayFormat);
            }

            set
            {
                if (!Enum.IsDefined(typeof(AddressDisplayFormat), value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                this.ViewState["AddressDisplayFormat"] = value;
            }
        }

        /// <summary>
        /// The style to be applied to individual address items. 
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty), Category("Styles")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [NotifyParentProperty(true)]
        public Style ItemStyle
        {
            get
            {
                if (this.itemStyle == null)
                {
                    this.itemStyle = new Style();
                    if (this.IsTrackingViewState)
                    {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         
                        ((IStateManager)this.itemStyle).TrackViewState();
                    }
                }

                return this.itemStyle;
            }
        }

        /// <summary>
        /// Gets or sets the address type of the patient address.
        /// </summary>
        /// <remarks>
        /// Defaults to Usual address
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The AddressType field of the Patient Address.")]
        public string AddressType
        {
            get
            {
                string addressType = (string)this.ViewState["AddressType"];

                return addressType ?? AddressLabelControl.Resources.AddressType;
            }

            set
            {
                this.ViewState["AddressType"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the CSS class that is applied to the address type label. 
        /// </summary>
        [Category("Styles")]
        [Bindable(true), DefaultValue("")]
        [Description("The Css Class for the AddressType field of the Patient Address.")]
        public string AddressTypeStyle
        {
            get
            {
                return (string)this.ViewState["AddressTypeStyle"];
            }

            set
            {
                this.ViewState["AddressTypeStyle"] = value;
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

        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets the HtmlTextWriterTag value that corresponds to this Web 
        /// server control
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                if (this.AddressDisplayFormat == AddressDisplayFormat.InForm)
                {
                    return HtmlTextWriterTag.Ol;
                }

                return base.TagKey;
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// get address parts as an array
        /// </summary>
        private string[] AddressParts
        {
            get
            {
                return new string[] 
                { 
                    this.Address1, this.Address2, this.Address3, this.Town, 
                    this.County, this.Postcode.ToUpper(CultureInfo.CurrentCulture),
                    this.Country 
                };
            }
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Get address summary
        /// </summary>        
        /// <returns>address summary</returns>
        internal string GetSummary()
        {
            StringBuilder sb = new StringBuilder();
            string separator = AddressLabelControl.Resources.AddressItemSeparator;
            string[] addressParts = this.AddressParts;

            for (int i = 0; i < addressParts.Length; i++)
            {
                string trimmedAddressPart = addressParts[i].Trim();
                if (trimmedAddressPart.Length > 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(separator);
                    }

                    sb.Append(trimmedAddressPart);
                }
            }

            return sb.ToString();
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Causes tracking of view-state changes to the server control 
        /// so they can be stored in the server control's StateBag object. This object is accessible through the Control.ViewState property. 
        /// </summary>
        protected override void TrackViewState()
        {
            base.TrackViewState();

            if (this.itemStyle != null)
            {
                ((IStateManager)this.itemStyle).TrackViewState();
            }
        }

        /// <summary>
        /// Saves any server control view-state changes that have occurred since
        /// the time the page was posted back to the server. 
        /// </summary>
        /// <returns>Returns the server control's current view state. 
        /// If there is no view state associated with the control, this method 
        /// returns a null reference. </returns>
        protected override object SaveViewState()
        {
            Pair state = new Pair();
            state.First = base.SaveViewState();
            state.Second = (this.itemStyle != null) ? ((IStateManager)this.itemStyle).SaveViewState() : null;

            return state;
        }

        /// <summary>
        /// Restores view-state information from a previous page request 
        /// that was saved by the SaveViewState method. 
        /// </summary>
        /// <param name="savedState">An Object that represents the control state to be restored. </param>
        protected override void LoadViewState(object savedState)
        {
            if (savedState == null)
            {
                base.LoadViewState(null);
            }
            else
            {
                Pair state = (Pair)savedState;

                base.LoadViewState(state.First);

                if (state.Second != null)
                {
                    ((IStateManager)this.ItemStyle).LoadViewState(state.Second);
                }
            }
        }

        /// <summary>
        /// Adds HTML attributes and styles that need to be rendered to the specified HtmlTextWriterTag. 
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output stream to render HTML content on the client. </param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            // if we are rendering as an ordered list don't display bullets
            if (this.TagKey == HtmlTextWriterTag.Ol)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.ListStyleType, "none");
            }
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output stream to render 
        /// HTML content on the client. </param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            // Render address type
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.AddressTypeStyle);
            writer.RenderBeginTag(HtmlTextWriterTag.Span);

            if (this.renderWrappableStrings)
            {
                writer.Write(StringUtil.GetAntiXssWrappableString(this.AddressType));
            }
            else
            {
                writer.Write(this.AddressType);
            }

            writer.Write("&nbsp;");
            writer.RenderEndTag();

            if (this.AddressDisplayFormat == AddressDisplayFormat.InForm)
            {
                RenderAddress(writer, this.AddressParts, this.RenderInFormAddressItem);
            }
            else
            {
                if (!RenderAddress(writer, this.AddressParts, this.RenderInLineAddressItem))
                {
                    writer.Write("&nbsp;");
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Render address to the supplied html text writer 
        /// </summary>
        /// <param name="writer">writer to output to</param>
        /// <param name="addressParts">address parts to output</param>
        /// <param name="addressItemRenderer">method to use to render each item</param>
        /// <returns>true if any content written</returns>
        private static bool RenderAddress(HtmlTextWriter writer, string[] addressParts, AddressItemRenderer addressItemRenderer)
        {
            bool writtenContent = false;            

            for (int i = 0; i < addressParts.Length; i++)
            {
                string trimmedAddressPart = addressParts[i].Trim();
                if (trimmedAddressPart.Length > 0)
                {
                    addressItemRenderer(writer, trimmedAddressPart, !writtenContent);
                    writtenContent = true;
                }
            }

            return writtenContent;
        }

        /// <summary>
        /// Render inline address item
        /// </summary>
        /// <param name="writer">writer to output to</param>
        /// <param name="addressPart">address part to render</param>
        /// <param name="isfirstItem">true if first item</param>
        private void RenderInLineAddressItem(HtmlTextWriter writer, string addressPart, bool isfirstItem)
        {
            // render as comma separated list
            if (!isfirstItem)
            {
                writer.WriteEncodedText(AddressLabelControl.Resources.AddressItemSeparator);
            }

            // if we have an item style render item in a span
            bool hasItemStyle = (this.itemStyle != null && !this.itemStyle.IsEmpty);
            if (hasItemStyle)
            {
                this.itemStyle.AddAttributesToRender(writer);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
            }

            if (this.renderWrappableStrings)
            {
                writer.Write(StringUtil.GetAntiXssWrappableString(addressPart));
            }
            else
            {
                writer.Write(addressPart);
            }

            if (hasItemStyle)
            {
                writer.RenderEndTag();
            }
        }

        /// <summary>
        /// Render inform address item
        /// </summary>
        /// <param name="writer">writer to output to</param>
        /// <param name="addressPart">address item to render</param>
        /// <param name="isfirstItem">true if first item</param>
        private void RenderInFormAddressItem(HtmlTextWriter writer, string addressPart, bool isfirstItem)
        {
            // render as li element
            if (this.itemStyle != null && !this.itemStyle.IsEmpty)
            {
                this.itemStyle.AddAttributesToRender(writer);
            }

            writer.RenderBeginTag(HtmlTextWriterTag.Li);

            if (this.renderWrappableStrings)
            {
                writer.Write(StringUtil.GetAntiXssWrappableString(addressPart));
            }
            else
            {
                writer.Write(addressPart);
            }

            writer.RenderEndTag();
            writer.WriteLine();
        }
        #endregion
    }
}
