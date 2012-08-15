//-----------------------------------------------------------------------
// <copyright file="NameInputBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>10-Sep-2007</date>
// <summary>The control used to enter an name.</summary>
//-----------------------------------------------------------------------
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.NameInputBoxControl.NameInputBox.js", "application/x-javascript")]

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
    using System.Text.RegularExpressions;
    using System.Text;

    /// <summary>
    /// The control used to enter an name. 
    /// </summary>
    [ParseChildren(true),
    ToolboxData("<{0}:NameInputBox runat=\"server\"></{0}:NameInputBox>")]
    [ToolboxItem(false)]
    public class NameInputBox : CompositeControl, IScriptControl
    {
        /// <summary>
        /// Default sequence for the display of fields
        /// </summary>
        private const string DefaultFieldOrder = "1,2,3,4,5";

        /// <summary>
        /// The NameLine for title part of the name
        /// </summary>
        private NameLine title;

        /// <summary>
        /// The NameLine for givenName part the name
        /// </summary>
        private NameLine givenName;

        /// <summary>
        /// The NameLine for otherNames part of the name
        /// </summary>
        private NameLine otherNames;

        /// <summary>
        /// The NameLine for the familyName part of the name
        /// </summary>
        private NameLine familyName;

        /// <summary>
        /// The NameLine for the suffix part of the name
        /// </summary>
        private NameLine suffix;

        /// <summary>
        /// Current page's Script manager.
        /// </summary>
        private ScriptManager pageLevelScriptManager;

        /// <summary>
        /// Extender to provide title auto-complete
        /// </summary>
        private TitleAutoCompleteExtender titleAutoCompleteExtender;

        /// <summary>
        /// Sequence for the display of fields
        /// </summary>
        private string fieldOrder = DefaultFieldOrder;

        /// <summary>
        /// Regex to check fieldOrder value - long version
        /// </summary>
        private Regex validLongFieldOrderRegex = new Regex(@"^(\d,){4}\d$", RegexOptions.Compiled);

        /// <summary>
        /// Regex to check fieldOrder value - short version
        /// </summary>
        private Regex validShortFieldOrderRegex = new Regex(@"^\d{5}$", RegexOptions.Compiled);

        /// <summary>
        /// String representing the numerical sequence for the display of fields
        /// </summary>
        [Description("String representing the numerical sequence for the display of fields"), 
        RefreshProperties(RefreshProperties.All), 
        Category("Behavior"),
        DefaultValue("1,2,3,4,5")]
        public string FieldOrder
        {
            get
            {
                return this.fieldOrder;
            }

            set
            {
                if (this.FieldOrderIsValid(value) == true)
                {
                    this.fieldOrder = NameInputBox.EnsureCommasInFieldOrder(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the NameLine for title part of the name.
        /// </summary>
        [Category("Behavior"), Description("NameLine for title part of the name"),
        RefreshProperties(RefreshProperties.All),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public NameLine Title
        {
            get
            {
                this.EnsureChildControls();

                if (this.title == null)
                {
                    this.title = new NameLine(true, true);
                }

                return this.title;
            }

            set
            {
                this.title = value;
            }
        }

        /// <summary>
        /// Gets or sets the NameLine for givenName part of the name.
        /// </summary>
        [Category("Behavior"), Description("NameLine for givenName part of the name"),
        RefreshProperties(RefreshProperties.All), 
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public NameLine GivenName
        {
            get
            {
                this.EnsureChildControls();

                if (this.givenName == null)
                {
                    this.givenName = new NameLine(true, false);
                }

                return this.givenName;
            }

            set
            {
                this.givenName = value;
            }
        }

        /// <summary>
        /// Gets or sets the NameLine for OtherNames part of the name.
        /// </summary>
        [Category("Behavior"), Description("NameLine for OtherNames part of the name"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public NameLine OtherNames
        {
            get
            {
                this.EnsureChildControls();

                if (this.otherNames == null)
                {
                    this.otherNames = new NameLine(true, false);
                }

                return this.otherNames;
            }

            set
            {
                this.otherNames = value;
            }
        }

        /// <summary>
        /// Gets or sets the NameLine for the FamilyName part of the name.
        /// </summary>
        [Category("Behavior"), Description("NameLine for the FamilyName part of the name"),
        RefreshProperties(RefreshProperties.All),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public NameLine FamilyName
        {
            get
            {
                this.EnsureChildControls();

                if (this.familyName == null)
                {
                    this.familyName = new NameLine(true, false);
                }

                return this.familyName;
            }

            set
            {
                this.familyName = value;
            }
        }

        /// <summary>
        /// Gets or sets the NameLine for the suffix part of an name.
        /// </summary>
        [Category("Behavior"), Description("NameLine for the suffix part of an name"),
        RefreshProperties(RefreshProperties.All),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true),
        PersistenceMode(PersistenceMode.InnerProperty)]
        public NameLine Suffix
        {
            get
            {
                this.EnsureChildControls();

                if (this.suffix == null)
                {
                    this.suffix = new NameLine(true, false);
                }

                return this.suffix;
            }

            set
            {
                this.suffix = value;
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
            ScriptControlDescriptor descriptor = new ScriptControlDescriptor("NhsCui.Toolkit.Web.NameInputBox", this.ClientID);

            descriptor.AddProperty("familyNameInputBoxId", this.FindControl(string.Format(CultureInfo.InvariantCulture, "{0}_TextBox", "familyName")).ClientID);

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
                                        new ScriptReference("NhsCui.Toolkit.Web.NameInputBoxControl.NameInputBox.js", "NhsCui.Toolkit.Web"), 
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
        /// Writes the NameInputBox content to the specified HtmlTextWriter object, for display on the client.
        /// </summary>
        /// <param name="writer">An HtmlTextWriter that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            this.EnsureChildControls();

            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "nhscui_nameControl");
            writer.RenderBeginTag("table");

            // Note that the css class name being passed into the RenderNameLine calls is the correct one
            // for the name microformat

            string[] fields = this.FieldOrderIsValid(this.fieldOrder) == true ? this.fieldOrder.Split(',') : DefaultFieldOrder.Split(',');

            for (int fieldIndex = 0; fieldIndex < fields.Length; fieldIndex++)
            {
                switch (fields[fieldIndex])
                {
                    case "1":
                        (this.FindControl("title_TextBox") as TextBox).Attributes.Add("autocomplete", "off");
                        this.RenderNameLine(this.Title, "title", "name-title", writer);
                        break;
                    case "2":
                        this.RenderNameLine(this.GivenName, "givenName", "name-givenName", writer);
                        break;
                    case "3":
                        this.RenderNameLine(this.OtherNames, "otherNames", "name-otherNames", writer);
                        break;
                    case "4":
                        this.RenderNameLine(this.FamilyName, "familyName", "name-familyName", writer);
                        break;
                    case "5":
                        this.RenderNameLine(this.Suffix, "suffix", "name-suffix", writer);
                        break;
                }
            }

            this.titleAutoCompleteExtender.RenderControl(writer);

            writer.RenderEndTag();
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use 
        /// composition-based implementation to create any child controls they contain 
        /// in preparation for posting back or rendering. 
        /// </summary>
        protected override void CreateChildControls()
        {
            this.CreateNameLineChildControls(this.Title, "title");

            this.CreateNameLineChildControls(this.GivenName, "givenName");

            this.CreateNameLineChildControls(this.OtherNames, "otherNames");

            this.CreateNameLineChildControls(this.FamilyName, "familyName");

            this.CreateNameLineChildControls(this.Suffix, "suffix");

            base.CreateChildControls();

            this.AddTitleAutoCompletion();
        }

        /// <summary>
        /// Check that no field occurs more than once
        /// </summary>
        /// <param name="value">string to check</param>
        /// <returns>true if each field occurs once only, otherwise false</returns>
        private static bool NoRepeatingFields(string value)
        {
            if ((value.IndexOf('1') == value.LastIndexOf('1')) &&
                (value.IndexOf('2') == value.LastIndexOf('2')) &&
                (value.IndexOf('3') == value.LastIndexOf('3')) &&
                (value.IndexOf('4') == value.LastIndexOf('4')) &&
                (value.IndexOf('5') == value.LastIndexOf('5')))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Cast a short field order to a long one if necessary
        /// </summary>
        /// <param name="value">field order to expand if necessary</param>
        /// <returns>expanded field order</returns>
        private static string EnsureCommasInFieldOrder(string value)
        {
            Regex doubleDigits = new Regex(@"(?<=\d)(?!,)(?!$)");

            return doubleDigits.Replace(value, ",");
        }

        /// <summary>
        /// Called from CreateChildControls to create the necessary child controls for an NameLine
        /// </summary>
        /// <param name="nameLine">The NameLine to create the controls for</param>
        /// <param name="name">Name of the NameLine, e.g. title, givenName etc.</param>
        private void CreateNameLineChildControls(NameLine nameLine, string name)
        {
            if (nameLine.Visible == true)
            {
                if (nameLine.ShowLabel == true)
                {
                    // A label is requested for this name line is mandatory so create a Label
                    Label lbl = new Label();
                    lbl.ID = string.Format(CultureInfo.InvariantCulture, "{0}_Label", name);
                    lbl.AssociatedControlID = string.Format(CultureInfo.InvariantCulture, "{0}_TextBox", name);
                    lbl.Text = nameLine.LabelText;

                    this.Controls.Add(lbl);
                }

                if (nameLine.Mandatory == true)
                {
                    // Input for this name line is mandatory so create a RequiredFieldValidator
                    RequiredFieldValidator rfv = new RequiredFieldValidator();
                    rfv.ID = string.Format(CultureInfo.InvariantCulture, "{0}_RequiredFieldVal", name);
                    rfv.ControlToValidate = string.Format(CultureInfo.InvariantCulture, "{0}_TextBox", name);
                    rfv.Display = ValidatorDisplay.Dynamic;
                    rfv.ErrorMessage = "* required field";

                    this.Controls.Add(rfv);
                }

                if (nameLine.ValidationExpression != null)
                {
                    // A validation expression has been specified for this NameLine so create a RegularExpressionValidator
                    RegularExpressionValidator regfv = new RegularExpressionValidator();
                    regfv.ID = string.Format(CultureInfo.InvariantCulture, "{0}_RegularExpressionVal", name);
                    regfv.ControlToValidate = string.Format(CultureInfo.InvariantCulture, "{0}_TextBox", name);
                    regfv.Display = ValidatorDisplay.Dynamic;
                    regfv.ValidationExpression = nameLine.ValidationExpression;
                    regfv.ErrorMessage = "* invalid data";

                    this.Controls.Add(regfv);
                }

                TextBox tb = new TextBox();
                tb.ID = string.Format(CultureInfo.InvariantCulture, "{0}_TextBox", name);

                this.Controls.Add(tb);

                // Pass the textbox down to the name line so it can refer to it internally
                nameLine.PassInputControl(tb);
            }
        }

        /// <summary>
        /// Writes the NameLine content to the specified HtmlTextWriter object, for display on the client.
        /// </summary>
        /// <param name="nameLine">The NameLine to Render</param>
        /// <param name="name">The name of the NameLine, e.g. Title or Postcode</param>
        /// <param name="cssClass">A CSS to attach to the name line's input element.</param>
        /// <param name="writer">An HtmlTextWriter that represents the output stream to render HTML content on the client.</param>
        private void RenderNameLine(NameLine nameLine, string name, string cssClass, HtmlTextWriter writer)
        {
            if (nameLine.Visible)
            {
                writer.RenderBeginTag("tr");
                writer.WriteLine();
                writer.Indent++;

                // Render the label cell
                writer.RenderBeginTag("td");
                if (nameLine.ShowLabel == true)
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

                if (nameLine.Mandatory == true)
                {
                    // Input for this name line is mandatory so render out the RequiredFieldValidator
                    this.FindControl(string.Format(CultureInfo.InvariantCulture, "{0}_RequiredFieldVal", name)).RenderControl(writer);
                }

                if (nameLine.ValidationExpression != null)
                {
                    this.FindControl(string.Format(CultureInfo.InvariantCulture, "{0}_RegularExpressionVal", name)).RenderControl(writer);
                }

                writer.RenderEndTag();
                writer.Indent--;

                writer.Indent--;
                writer.RenderEndTag();
            }
        }

        /// <summary>
        /// Inject the autocomplete extender
        /// </summary>
        private void AddTitleAutoCompletion()
        {
            // Add a title extender for the title autocomplete
            this.titleAutoCompleteExtender = new TitleAutoCompleteExtender();
            this.titleAutoCompleteExtender.TargetControlID = this.FindControl("title_TextBox").ID;
            this.titleAutoCompleteExtender.ID = "title_titleAutoCompleteExtender";

            this.Controls.Add(this.titleAutoCompleteExtender);
        }

        /// <summary>
        /// Validate a FieldOrder string
        /// </summary>
        /// <param name="value">FieldOrder string to check</param>
        /// <returns>true if all fields present and each field occurs just once</returns>
        private bool FieldOrderIsValid(string value)
        {
            if (NameInputBox.NoRepeatingFields(value))
            {
                return this.validLongFieldOrderRegex.IsMatch(value) || this.validShortFieldOrderRegex.IsMatch(value);
            }

            return false;
        }
    }
}
