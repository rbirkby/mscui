//-----------------------------------------------------------------------
// <copyright file="TelephoneInputBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to enter an Telephone. </summary>
//-----------------------------------------------------------------------
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.TelephoneInputBoxControl.TelephoneInputBox.js", "application/x-javascript")]

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using AjaxControlToolkit;

    /// <summary>
    /// The control used to enter a telephone number. 
    /// </summary>
    [ToolboxData("<{0}:TelephoneInputBox runat=\"server\"></{0}:TelephoneInputBox>")]
    [ToolboxItem(false)]
    public class TelephoneInputBox : CompositeControl, IScriptControl
    {
        /// <summary>
        /// The main Label for the control e.g 'Telephone'
        /// </summary>
        private Label telephoneLabel;

        /// <summary>
        /// The country code Label
        /// </summary>
        private Label countryCodeLabel;
        
        /// <summary>
        /// The basic textbox used to hold the telephone number's country code.
        /// </summary>
        private TextBox countryCodeTextBox;
        
        /// <summary>
        /// The area code Label
        /// </summary>
        private Label areaCodeLabel;
        
        /// <summary>
        /// The basic textbox used to hold the telephone number's area code.
        /// </summary>
        private TextBox areaCodeTextBox;

        /// <summary>
        /// The basic textbox used to hold the telephone number's local number part
        /// </summary>
        private TextBox localNumberTextBox;

        /// <summary>
        /// Current page's Script manager.
        /// </summary>
        private ScriptManager pageLevelScriptManager;
        
        /// <summary>
        /// Extender to provide numeric character filtering to the country code
        /// </summary>
        private FilteredTextBoxExtender countryCodeFilteredTextBoxExtender;

        /// <summary>
        /// Extender to provide numeric character filtering to the area code
        /// </summary>
        private FilteredTextBoxExtender areaCodeFilteredTextBoxExtender;

        /// <summary>
        /// Extender to provide numeric character filtering to the local number
        /// </summary>
        private FilteredTextBoxExtender localNumberFilteredTextBoxExtender;

        /// <summary>
        /// Occurs when the value of the input box changes between posts to the server. 
        /// </summary>
        public event EventHandler ValueChanged;

        /// <summary>
        /// Gets or sets the text for the main Label for the control.
        /// </summary>
        [Category("Appearance"), Description("The text for the main Label of the control"), DefaultValue(null)]
        public string TelephoneLabelText
        {
            get
            {
                this.EnsureChildControls();
                return this.telephoneLabel.Text;
            }

            set
            {
                this.EnsureChildControls();
                this.telephoneLabel.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the country code part of the telephone number
        /// </summary>
        [Category("Data"), Description("The country code part of the telephone number")]
        public string CountryCode
        {
            get
            {
                this.EnsureChildControls();
                return this.countryCodeTextBox.Text;
            }

            set
            {
                this.EnsureChildControls();
                this.countryCodeTextBox.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the text for the country code label.
        /// </summary>
        [Category("Appearance"), Description("The text for the country code label"), DefaultValue(null)]
        public string CountryCodeLabelText
        {
            get
            {
                this.EnsureChildControls();
                return this.countryCodeLabel.Text;
            }

            set
            {
                this.EnsureChildControls();
                this.countryCodeLabel.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the area code part of the telephone number
        /// </summary>
        [Category("Data"), Description("The area code part of the telephone number")]
        public string AreaCode
        {
            get
            {
                this.EnsureChildControls();
                return this.areaCodeTextBox.Text;
            }

            set
            {
                this.EnsureChildControls();
                this.areaCodeTextBox.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the text for the area code label.
        /// </summary>
        [Category("Appearance"), Description("The text for the area code label"), DefaultValue(null)]
        public string AreaCodeLabelText
        {
            get
            {
                this.EnsureChildControls();
                return this.areaCodeLabel.Text;
            }

            set
            {
                this.EnsureChildControls();
                this.areaCodeLabel.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the local number part of the telephone number
        /// </summary>
        [Category("Data"), Description("The local number part of the telephone number")]
        public string LocalNumber
        {
            get
            {
                this.EnsureChildControls();
                return this.localNumberTextBox.Text;
            }

            set
            {
                this.EnsureChildControls();
                this.localNumberTextBox.Text = value;
            }
        }

        /// <summary>
        /// Gets the HtmlTextWriterTag value that corresponds to this Web server control. 
        /// </summary>
        protected override System.Web.UI.HtmlTextWriterTag TagKey
        {
            get
            {
                return System.Web.UI.HtmlTextWriterTag.Table;
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
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "GetScriptDescriptors is part of the ASP.NET Ajax IScriptControl interface")]
        protected virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor = new ScriptControlDescriptor("NhsCui.Toolkit.Web.TelephoneInputBox", this.ClientID);

            // Note this control does not send the countryCode, areaCode, and localNumber properties down
            // to the client as ASP.NET Ajax properties. It does not need to because in both the server and client 
            // implementations the values are read and written directly from/to the TextBoxes.

            return new ScriptDescriptor[] { descriptor };
        }

        /// <summary>
        /// Gets a collection of ScriptReference objects that define script resources that the control requires
        /// </summary>
        /// <returns>An IEnumerable collection of ScriptReference objects.</returns>
        /// <remarks>Implemented this method so as to get the scripts, 
        /// TerminologyTextEditor.js, ChunkModel.js, DomCompatibility.js down to the client </remarks>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "GetScriptDescriptors is part of the ASP.NET Ajax IScriptControl interface")]
        protected virtual IEnumerable<ScriptReference> GetScriptReferences()
        {
            return new ScriptReference[] 
                                        { 
                                        new ScriptReference("NhsCui.Toolkit.Web.TelephoneInputBoxControl.TelephoneInputBox.js", "NhsCui.Toolkit.Web"), 
                                        };
        }
        #endregion

        /// <summary>
        /// Add Class, Cellpadding and Cellspacing Attributes to the control's outer Table tag.
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the control content.</param>
        protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "telephone-input");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
            writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "2");
            base.AddAttributesToRender(writer);
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use 
        /// composition-based implementation to create any child controls they contain 
        /// in preparation for posting back or rendering. 
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.telephoneLabel = new Label();
            this.telephoneLabel.CssClass = "telephone-label";
            this.telephoneLabel.ID = "telephoneLabel";
            Controls.Add(this.telephoneLabel);

            this.countryCodeLabel = new Label();
            this.countryCodeLabel.CssClass = "telephone-label";
            this.countryCodeLabel.ID = "countryCodeLabel";
            Controls.Add(this.countryCodeLabel);

            // Note that the css class name being set on the TextBoxes is the correct one
            // for the telephone number microformat
                        
            this.countryCodeTextBox = new TextBox();
            this.countryCodeTextBox.CssClass = "country-code";
            this.countryCodeTextBox.ID = "countryCodeTextBox";
            this.countryCodeTextBox.TextChanged += new EventHandler(this.CountryCodeTextChanged);
            //// Link label to TextBox
            this.countryCodeLabel.AssociatedControlID = this.countryCodeTextBox.ID;
            Controls.Add(this.countryCodeTextBox);

            this.areaCodeLabel = new Label();
            this.areaCodeLabel.CssClass = "telephone-label";
            this.areaCodeLabel.ID = "areaCodeLabel";
            Controls.Add(this.areaCodeLabel);
            
            this.areaCodeTextBox = new TextBox();
            this.areaCodeTextBox.CssClass = "area-code";
            this.areaCodeTextBox.ID = "areaCodeTextBox";
            this.areaCodeTextBox.TextChanged += new EventHandler(this.AreaCodeTextBoxTextChanged);
            //// Link label to TextBox
            this.areaCodeLabel.AssociatedControlID = this.areaCodeTextBox.ID;
            Controls.Add(this.areaCodeTextBox);

            this.localNumberTextBox = new TextBox();
            this.localNumberTextBox.CssClass = "local-number";
            this.localNumberTextBox.ID = "localNumberTextBox";
            this.localNumberTextBox.TextChanged += new EventHandler(this.LocalNumberTextBoxTextChanged);
            Controls.Add(this.localNumberTextBox);

            this.AddFilteredTextBoxExtenders();
        }

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
        /// Renders the contents of the control to the specified writer. 
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the control content.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Thead);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                // empty first header col - because the main label is below...
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.RenderEndTag();

                writer.AddAttribute(HtmlTextWriterAttribute.Abbr, "country code");
                writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                this.countryCodeLabel.RenderControl(writer);
                writer.RenderEndTag();

                writer.AddAttribute(HtmlTextWriterAttribute.Abbr, "area code");
                writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                this.areaCodeLabel.RenderControl(writer);
                writer.RenderEndTag();

                writer.AddAttribute(HtmlTextWriterAttribute.Abbr, "local number");
                writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.RenderEndTag();

            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                this.telephoneLabel.RenderControl(writer);
                writer.RenderEndTag();

                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                this.countryCodeTextBox.RenderControl(writer);
                writer.RenderEndTag();

                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                this.areaCodeTextBox.RenderControl(writer);
                writer.RenderEndTag();

                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                this.localNumberTextBox.RenderControl(writer);
                writer.RenderEndTag();

            this.countryCodeFilteredTextBoxExtender.RenderControl(writer);
            this.areaCodeFilteredTextBoxExtender.RenderControl(writer);
            this.localNumberFilteredTextBoxExtender.RenderControl(writer);

            writer.RenderEndTag();
        }

        /// <summary>
        /// Raise the ValueChanged event
        /// </summary>
        /// <param name="e">event args</param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
        }

        /// <summary>
        /// Handle text changed event of our underlying text box for Country Code.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void CountryCodeTextChanged(object sender, EventArgs e)
        {
            this.OnValueChanged(e);
        }

        /// <summary>
        /// Handle text changed event of our underlying text box for Area Code.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void AreaCodeTextBoxTextChanged(object sender, EventArgs e)
        {
            this.OnValueChanged(e);
        }

        /// <summary>
        /// Handle text changed event of our underlying text box for Local Number.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void LocalNumberTextBoxTextChanged(object sender, EventArgs e)
        {
            this.OnValueChanged(e);
        }

        /// <summary>
        /// Inject the filtered textbox extenders
        /// </summary>
        private void AddFilteredTextBoxExtenders()
        {
            // Add a filtered TextBox extender for the country code
            this.countryCodeFilteredTextBoxExtender = new FilteredTextBoxExtender();
            this.countryCodeFilteredTextBoxExtender.FilterType = FilterTypes.Custom | FilterTypes.Numbers;
            this.countryCodeFilteredTextBoxExtender.ValidChars = "+";
            this.countryCodeFilteredTextBoxExtender.TargetControlID = this.countryCodeTextBox.ID;
            this.countryCodeFilteredTextBoxExtender.ID = "countryCodeFilteredTextBoxExtender";

            // Add a filtered TextBox extender for the area code
            this.areaCodeFilteredTextBoxExtender = new FilteredTextBoxExtender();
            this.areaCodeFilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
            this.areaCodeFilteredTextBoxExtender.TargetControlID = this.areaCodeTextBox.ID;
            this.areaCodeFilteredTextBoxExtender.ID = "areaCodeFilteredTextBoxExtender";

            // Add a filtered TextBox extender for the local number
            this.localNumberFilteredTextBoxExtender = new FilteredTextBoxExtender();
            this.localNumberFilteredTextBoxExtender.FilterType = FilterTypes.Numbers;
            this.localNumberFilteredTextBoxExtender.TargetControlID = this.localNumberTextBox.ID;
            this.localNumberFilteredTextBoxExtender.ID = "localNumberFilteredTextBoxExtender";

            this.Controls.Add(this.countryCodeFilteredTextBoxExtender);
            this.Controls.Add(this.areaCodeFilteredTextBoxExtender);
            this.Controls.Add(this.localNumberFilteredTextBoxExtender);
        }
    }
}
