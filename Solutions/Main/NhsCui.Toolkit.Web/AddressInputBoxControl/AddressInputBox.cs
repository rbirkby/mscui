//-----------------------------------------------------------------------
// <copyright file="AddressInputBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to enter an address.</summary>
//-----------------------------------------------------------------------
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.AddressInputBoxControl.AddressInputBox.js", "application/x-javascript")]

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.Design;
    using System.Web.Script;
    using System.Globalization;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The control used to enter an address. 
    /// </summary>
    [ParseChildren(true),
    ToolboxData("<{0}:AddressInputBox runat=\"server\"></{0}:AddressInputBox>")]
    [ToolboxItem(false)]
    public class AddressInputBox : CompositeControl, IScriptControl
    {
        /// <summary>
        /// The AddressLine for Address line 1 of an address
        /// </summary>
        private AddressLine addressLine1;

        /// <summary>
        /// The AddressLine for Address line 2 of an address
        /// </summary>
        private AddressLine addressLine2;

        /// <summary>
        /// The AddressLine for Address line 3 of an address
        /// </summary>
        private AddressLine addressLine3;

        /// <summary>
        /// The AddressLine for the town part of an address
        /// </summary>
        private AddressLine town;

        /// <summary>
        /// The AddressLine for the county part of an address
        /// </summary>
        private AddressLine county;

        /// <summary>
        /// The AddressLine for the country part of an address
        /// </summary>
        private AddressLine country;

        /// <summary>
        /// The AddressLine for the postcode part of an address
        /// </summary>
        private AddressLine postcode;

        /// <summary>
        /// Current page's Script manager.
        /// </summary>
        private ScriptManager pageLevelScriptManager;

        /// <summary>
        /// Gets or sets the AddressLine for Address line 1 of an address.
        /// </summary>
        [Category("Behavior"), Description("AddressLine for Address line 1 of an address"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public AddressLine AddressLine1
        {
            get
            {
                this.EnsureChildControls();

                if (this.addressLine1 == null)
                {
                    this.addressLine1 = new AddressLine(true, true);
                }

                return this.addressLine1;
            }

            set
            {
                this.addressLine1 = value;
            }
        }

        /// <summary>
        /// Gets or sets the AddressLine for Address line 2 of an address.
        /// </summary>
        [Category("Behavior"), Description("AddressLine for Address line 2 of an address"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public AddressLine AddressLine2
        {
            get
            {
                this.EnsureChildControls();

                if (this.addressLine2 == null)
                {
                    this.addressLine2 = new AddressLine(false, false);
                }

                return this.addressLine2;
            }

            set
            {
                this.addressLine2 = value;
            }
        }

        /// <summary>
        /// Gets or sets the AddressLine for Address line 3 of an address.
        /// </summary>
        [Category("Behavior"), Description("AddressLine for Address line 3 of an address"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public AddressLine AddressLine3
        {
            get
            {
                this.EnsureChildControls();

                if (this.addressLine3 == null)
                {
                    this.addressLine3 = new AddressLine(false, false);
                }

                return this.addressLine3;
            }

            set
            {
                this.addressLine3 = value;
            }
        }

        /// <summary>
        /// Gets or sets the AddressLine for the town part of an address.
        /// </summary>
        [Category("Behavior"), Description("AddressLine for for the town part of an address"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public AddressLine Town
        {
            get
            {
                this.EnsureChildControls();

                if (this.town == null)
                {
                    this.town = new AddressLine(true, false);
                }

                return this.town;
            }

            set
            {
                this.town = value;
            }
        }

        /// <summary>
        /// Gets or sets the AddressLine for the county part of an address.
        /// </summary>
        [Category("Behavior"), Description("AddressLine for for the county part of an address"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public AddressLine County
        {
            get
            {
                this.EnsureChildControls();

                if (this.county == null)
                {
                    this.county = new AddressLine(false, false);
                }

                return this.county;
            }

            set
            {
                this.county = value;
            }
        }

        /// <summary>
        /// Gets or sets the AddressLine for the country part of an address.
        /// </summary>
        [Category("Behavior"), Description("AddressLine for for the country part of an address"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public AddressLine Country
        {
            get
            {
                this.EnsureChildControls();

                if (this.country == null)
                {
                    this.country = new AddressLine(false, false);
                }

                return this.country;
            }

            set
            {
                this.country = value;
            }
        }

        /// <summary>
        /// Gets or sets the AddressLine for the postcode part of an address.
        /// </summary>
        [Category("Behavior"), Description("AddressLine for for the postcode part of an address"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public AddressLine Postcode
        {
            get
            {
                this.EnsureChildControls();

                if (this.postcode == null)
                {
                    this.postcode = new AddressLine(true, true);
                    this.postcode.ValidationExpression = "[a-zA-Z]{1,2}[0-9][0-9A-Za-z]?[ ]?[0-9][a-zA-Z]{2}";
                    this.postcode.ShowLabel = true;
                    this.postcode.LabelText = "Postcode";
                }

                return this.postcode;
            }

            set
            {
                this.postcode = value;
            }
        }

        #region IScriptControl Members

        /// <summary>
        /// Defers to TerinologyTextEditor.GetScriptDescriptors
        /// </summary>
        /// <returns>An IEnumerable collection of ScriptDescriptor objects</returns>
        IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
        {
            return this.GetScriptDescriptors();
        }

        /// <summary>
        /// Defers to TerinologyTextEditor.GetScriptReferences
        /// </summary>
        /// <returns>An IEnumerable collection of ScriptReference objects.</returns>
        IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
        {
            return this.GetScriptReferences();
        }

        /// <summary>
        /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
        /// </summary>
        /// <returns>An IEnumerable collection of ScriptDescriptor objects</returns>
        protected virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor = new ScriptControlDescriptor("NhsCui.Toolkit.Web.AddressInputBox", this.ClientID);

            descriptor.AddProperty("postcodeInputBoxId", this.FindControl(string.Format(CultureInfo.InvariantCulture, "{0}_TextBox", "postcode")).ClientID);

            return new ScriptDescriptor[] { descriptor };
        }

        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires
        /// </summary>
        /// <returns>An IEnumerable collection of ScriptReference objects.</returns>
        /// <remarks>Implemented this method so as to get the scripts, 
        /// TerminologyTextEditor.js, ChunkModel.js, DomCompatibility.js down to the client </remarks>
        protected virtual IEnumerable<ScriptReference> GetScriptReferences()
        {
            return new ScriptReference[] 
                                        { 
                                        new ScriptReference("NhsCui.Toolkit.Web.AddressInputBoxControl.AddressInputBox.js", "NhsCui.Toolkit.Web"), 
                                        };
        }
        #endregion

        /// <summary>
        /// Raises the PreRender event.
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            if (!this.DesignMode)
            {
                // Test for ScriptManager and register if it exists
                this.pageLevelScriptManager = ScriptManager.GetCurrent(this.Page);

                if (this.pageLevelScriptManager == null)
                {
                    throw new HttpException("A ScriptManager control must exist on the current page.");
                }

                this.pageLevelScriptManager.RegisterScriptControl(this);
            }

            base.OnPreRender(e);
        }

        /// <summary>
        /// Register the TelephoneInputbox ScriptControl with the ScriptManager
        /// </summary>
        /// <param name="writer">An HtmlTextWriter that represents the output stream to render HTML content on the client.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.DesignMode)
            {
                this.pageLevelScriptManager.RegisterScriptDescriptors(this);
            }

            base.Render(writer);
        }

        /// <summary>
        /// Writes the AddressInputBox content to the specified HtmlTextWriter object, for display on the client.
        /// </summary>
        /// <param name="writer">An HtmlTextWriter that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            this.EnsureChildControls();

            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "nhscui_addressControl");
            writer.RenderBeginTag("table");

            // Note that the css class name being passed into the RenderAddressLine calls is the correct one
            // for the address microformat

            this.RenderAddressLine(this.AddressLine1, "addressLine1", "street-address", writer);

            this.RenderAddressLine(this.AddressLine2, "addressLine2", "extended-address", writer);

            this.RenderAddressLine(this.AddressLine3, "addressLine3", "extended-address", writer);

            this.RenderAddressLine(this.Town, "town", "locality", writer);

            this.RenderAddressLine(this.County, "county", "region", writer);

            this.RenderAddressLine(this.Country, "country", "country-name", writer);

            this.RenderAddressLine(this.Postcode, "postcode", "postal-code", writer);

            writer.RenderEndTag();
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use 
        /// composition-based implementation to create any child controls they contain 
        /// in preparation for posting back or rendering. 
        /// </summary>
        protected override void CreateChildControls()
        {
            this.CreateAddressLineChildControls(this.AddressLine1, "addressLine1");

            this.CreateAddressLineChildControls(this.AddressLine2, "addressLine2");

            this.CreateAddressLineChildControls(this.AddressLine3, "addressLine3");

            this.CreateAddressLineChildControls(this.Town, "town");

            this.CreateAddressLineChildControls(this.County, "county");

            this.CreateAddressLineChildControls(this.Country, "country");

            this.CreateAddressLineChildControls(this.Postcode, "postcode");

            base.CreateChildControls();
        }

        /// <summary>
        /// Called from CreateChildControls to create the necessary child controls for an AddressLine
        /// </summary>
        /// <param name="addressLine">The AddressLine to create the controls for</param>
        /// <param name="name">Name of the AddressLine, e.g. addressLine1 or postcode</param>
        private void CreateAddressLineChildControls(AddressLine addressLine, string name)
        {
            if (addressLine.Visible == true)
            {
                if (addressLine.ShowLabel == true)
                {
                    // A label is requested for this address line is mandatory so create a Label
                    Label lbl = new Label();
                    lbl.ID = string.Format(CultureInfo.InvariantCulture, "{0}_Label", name);
                    lbl.AssociatedControlID = string.Format(CultureInfo.InvariantCulture, "{0}_TextBox", name);
                    lbl.Text = addressLine.LabelText;

                    this.Controls.Add(lbl);
                }

                if (addressLine.Mandatory == true)
                {
                    // Input for this address line is mandatory so create a RequiredFieldValidator
                    RequiredFieldValidator rfv = new RequiredFieldValidator();
                    rfv.ID = string.Format(CultureInfo.InvariantCulture, "{0}_RequiredFieldVal", name);
                    rfv.ControlToValidate = string.Format(CultureInfo.InvariantCulture, "{0}_TextBox", name);
                    rfv.Display = ValidatorDisplay.Dynamic;
                    rfv.ErrorMessage = "* required field";

                    this.Controls.Add(rfv);
                }

                if (addressLine.ValidationExpression != null)
                {
                    // A validation expression has been specified for this AddressLine so create a RegularExpressionValidator
                    RegularExpressionValidator regfv = new RegularExpressionValidator();
                    regfv.ID = string.Format(CultureInfo.InvariantCulture, "{0}_RegularExpressionVal", name);
                    regfv.ControlToValidate = string.Format(CultureInfo.InvariantCulture, "{0}_TextBox", name);
                    regfv.Display = ValidatorDisplay.Dynamic;
                    regfv.ValidationExpression = addressLine.ValidationExpression;
                    regfv.ErrorMessage = "* invalid data";

                    this.Controls.Add(regfv);
                }

                TextBox tb = new TextBox();
                tb.ID = string.Format(CultureInfo.InvariantCulture, "{0}_TextBox", name);

                this.Controls.Add(tb);

                // Pass the textbox down to the address line so it can refer to it internally
                addressLine.PassInputControl(tb);
            }
        }

        /// <summary>
        /// Writes the AddressLine content to the specified HtmlTextWriter object, for display on the client.
        /// </summary>
        /// <param name="addressLine">The AddressLine to Render</param>
        /// <param name="name">The name of the AddressLine, e.g. AddressLine1 or Postcode</param>
        /// <param name="cssClass">A CSS to attach to the address line's input element.</param>
        /// <param name="writer">An HtmlTextWriter that represents the output stream to render HTML content on the client.</param>
        private void RenderAddressLine(AddressLine addressLine, string name, string cssClass, HtmlTextWriter writer)
        {
            if (addressLine.Visible)
            {
                writer.RenderBeginTag("tr");
                writer.WriteLine();
                writer.Indent++;

                // Render the label cell
                writer.RenderBeginTag("td");
                if (addressLine.ShowLabel == true)
                {
                    // ShowLabel is true so render the label control into the label cell
                    this.FindControl(string.Format(CultureInfo.InvariantCulture, "{0}_Label", name)).RenderControl(writer);
                }

                writer.RenderEndTag();

                if (cssClass != null)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, cssClass);
                }

                writer.RenderBeginTag("td");

                this.FindControl(string.Format(CultureInfo.InvariantCulture, "{0}_TextBox", name)).RenderControl(writer);

                writer.RenderEndTag();

                // render the validation

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "errorCell");
                writer.RenderBeginTag("td");

                if (addressLine.Mandatory == true)
                {
                    // Input for this address line is mandatory so render out the RequiredFieldValidator
                    this.FindControl(string.Format(CultureInfo.InvariantCulture, "{0}_RequiredFieldVal", name)).RenderControl(writer);
                }

                if (addressLine.ValidationExpression != null)
                {
                    this.FindControl(string.Format(CultureInfo.InvariantCulture, "{0}_RegularExpressionVal", name)).RenderControl(writer);
                }

                writer.RenderEndTag();
                writer.Indent--;

                writer.Indent--;
                writer.RenderEndTag();
            }
        }
    }
}
