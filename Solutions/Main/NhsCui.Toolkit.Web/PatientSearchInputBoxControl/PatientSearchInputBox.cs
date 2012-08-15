//-----------------------------------------------------------------------
// <copyright file="PatientSearchInputBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>01-Feb-2007</date>
// <summary>The control in which patient search criteria are entered. </summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Drawing;
    using AjaxControlToolkit;
    using System.Diagnostics.CodeAnalysis;
    using NhsCui.Toolkit.PatientSearch;
    using NhsCui.Toolkit.DateAndTime;
    using System.Collections.ObjectModel;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// The control in which patient search criteria are entered.
    /// </summary>
    [ParseChildren(true)]
    [PersistChildren(false)]
    [DefaultProperty("Text"), DefaultEvent("TextChanged")]
    [ToolboxData("<{0}:PatientSearchInputBox runat=\"server\"></{0}:PatientSearchInputBox>")]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "PatientSearchInputBox.bmp")]
    public class PatientSearchInputBox : CompositeControl
    {
        #region Member Vars

        /// <summary>
        /// The extender
        /// </summary>
        private PatientSearchInputBoxExtender patientSearchInputBoxExtender;

        /// <summary>
        /// The basic textbox used as the basis for the control.
        /// </summary>
        private TextBox textBox;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a PatientSearchInputBox object.
        /// </summary>
        public PatientSearchInputBox()
        {
            this.EnsureChildControls();
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the input box value changes between posts to the server. 
        /// </summary>
        public event EventHandler TextChanged;

        #endregion

        #region Properties

        /// <summary>
        /// The address. 
        /// </summary>
        /// <remarks>
        /// Read-only. 
        /// </remarks>
        [Description("Wrapper for Parser.Address")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public string Address
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.Address;
            }
        }

        /// <summary>
        /// The age.
        /// </summary>
        /// <remarks>
        /// Read-only.
        /// </remarks>
        [Description("Wrapper for Parser.Age")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public int Age
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.Age;
            }
        }

        /// <summary>
        /// The upper value in an age range.
        /// </summary>
        /// <remarks>
        ///  Read-only. 
        /// </remarks>
        [Description("Wrapper for Parser.AgeUpper")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public int AgeUpper
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.AgeUpper;
            }
        }

        /// <summary>
        /// A list of common family names.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Required by Spec.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Required by Spec.")]
        [Description("Wrapper for Parser.CommonFamilyNames")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<string> CommonFamilyNames
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.CommonFamilyNames;
            }

            set
            {
                this.patientSearchInputBoxExtender.CommonFamilyNames = value;
            }
        }

        /// <summary>
        /// The date of birth.
        /// </summary>
        /// <remarks>
        ///  Read-only and defaults to DateTime.MinValue. Used for both an exact date of birth and as the lower value in a date of birth range. 
        /// </remarks>
        [Description("Wrapper for Parser.DateOfBirth")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public NhsDate DateOfBirth
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.DateOfBirth;
            }
        }

        /// <summary>
        /// The upper value in a date of birth range. 
        /// </summary>
        /// <remarks>
        /// Read-only and defaults to null.
        /// </remarks>
        [Description("Wrapper for Parser.DateOfBirthUpper")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public NhsDate DateOfBirthUpper
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.DateOfBirthUpper;
            }
        }

        /// <summary>
        /// The character used to delimit the end of a group of words. 
        /// </summary>
        /// <remarks>
        /// Defaults to a double quote mark ("). Throws an ArgumentException if the value is a space, a dash (-), a forward slash (/) or is the 
        /// <see cref="P:NhsCui.Toolkit.Web.PatientSearchInputBox.InformationDelimiter">InformationDelimiter</see>. 
        /// </remarks>
        [Description("Wrapper for Parser.EndGroupDelimiter")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true), Category("Data")]
        public char EndGroupDelimiter
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.EndGroupDelimiter;
            }

            set
            {
                this.patientSearchInputBoxExtender.EndGroupDelimiter = value;
            }
        }

        /// <summary>
        /// The family name.
        /// </summary>
        /// <remarks>
        /// Read-only. 
        /// </remarks>
        [Description("Wrapper for Parser.FamilyName")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public string FamilyName
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.FamilyName;
            }
        }

        /// <summary>
        /// The gender. 
        /// </summary>
        /// <remarks>
        /// Read-only. 
        /// </remarks>
        [Description("Wrapper for Parser.Gender")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public Gender Gender
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.Gender;
            }
        }

        /// <summary>
        /// The given name. 
        /// </summary> 
        /// <remarks>
        /// Read-only. 
        /// </remarks>
        [Description("Wrapper for Parser.GivenName")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public string GivenName
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.GivenName;
            }
        }

        /// <summary>
        /// The character that is used to delimit the end of a group of words.
        ///</summary>
        [Description("Wrapper for Parser.InformationDelimiter")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true), Category("Data")]
        public char InformationDelimiter
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.InformationDelimiter;
            }

            set
            {
                this.patientSearchInputBoxExtender.InformationDelimiter = value;
            }
        }

        /// <summary>
        /// The list of PatientSearch.Information enumeration values that are used to parse the 
        /// <see cref="P:NhsCui.Toolkit.Web.PatientSearchInputBox.Text">Text</see> property.
        ///</summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Required by Spec.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Required by Spec.")]
        [Description("Wrapper for Parser.InformationFormat")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<Information> InformationFormat
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.InformationFormat;
            }

            set
            {
                this.patientSearchInputBoxExtender.InformationFormat = value;
            }
        }

        /// <summary>
        /// Returns true if the <see cref="P:NhsCui.Toolkit.Web.PatientSearchInputBox.FamilyName">FamilyName</see> is found in 
        /// <see cref="P:NhsCui.Toolkit.Web.PatientSearchInputBox.CommonFamilyNames">CommonFamilyNames</see>.
        /// </summary>
        /// <remarks>
        /// Read-only. 
        /// </remarks>
        [Description("Wrapper for Parser.IsCommonFamilyName")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public bool IsCommonFamilyName
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.IsCommonFamilyName;
            }
        }

        /// <summary>
        /// Returns true if both the <see cref="P:NhsCui.Toolkit.Web.PatientSearchInputBox.DateOfBirth">DateOfBirth</see> and 
        /// <see cref="P:NhsCui.Toolkit.Web.PatientSearchInputBox.Age">Age</see> have been entered and the number of years in the
        /// <see cref="P:NhsCui.Toolkit.Web.PatientSearchInputBox.Age">Age</see> is not the same as the number of years for the 
        /// <see cref="P:NhsCui.Toolkit.Web.PatientSearchInputBox.DateOfBirth">DateOfBirth</see> based on the current date; otherwise, it returns false.
        /// </summary>
        /// <remarks>        
        /// Read-only. 
        /// </remarks>
        [Description("Wrapper for Parser.IsDateOfBirthAgeMismatch")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public bool IsDateOfBirthAgeMismatch
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.IsDateOfBirthAgeMismatch;
            }
        }

        /// <summary>
        /// Returns true if both <see cref="P:NhsCui.Toolkit.Web.PatientSearchInputBox.Gender">Gender</see> and 
        /// <see cref="P:NhsCui.Toolkit.Web.PatientSearchInputBox.Title">Title</see> have been entered and the 
        /// <see cref="P:NhsCui.Toolkit.Web.PatientSearchInputBox.Title">Title</see> has a PatientSearch 
        /// <see cref="P:NhsCui.Toolkit.Web.PatientSearch.Enums.Gender">Gender</see> of Male or Female and the 
        /// PatientSearch 
        /// <see cref="P:NhsCui.Toolkit.Web.PatientSearch.Enums.Gender">Gender</see> is not the same; otherwise, false.
        /// </summary>
        /// <remarks>
        /// Read-only. 
        /// </remarks>
        [Description("Wrapper for Parser.IsGenderTitleMismatch")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public bool IsGenderTitleMismatch
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.IsGenderTitleMismatch;
            }
        }

        /// <summary>
        /// Returns true if all the values in <see cref="P:NhsCui.Toolkit.Web.PatientSearchInputBox.MandatoryInformation">MandatoryInformation</see> 
        /// are not their default values.
        /// </summary>
        /// <remarks>
        /// Read-only. Returns false if  <see cref="P:NhsCui.Toolkit.Web.PatientSearchInputBox.MandatoryInformation">MandatoryInformation</see> is null. 
        /// </remarks>
        [Description("Wrapper for Parser.IsMandatoryInformationEntered")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public bool IsMandatoryInformationEntered
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.IsMandatoryInformationEntered;
            }
        }

        /// <summary>
        /// The list of PatientSearch <see cref="P:NhsCui.Toolkit.PatientSearch.Enums.Information">Information</see> enumeration values that are 
        /// mandatory.
         /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Required by Spec.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Required by Spec.")]
        [Description("Wrapper for Parser.MandatoryInformation")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false), Category("Data")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<Information> MandatoryInformation
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.MandatoryInformation;
            }

            set
            {
                this.patientSearchInputBoxExtender.MandatoryInformation = value;
            }
        }

        /// <summary>
        /// The maximum age recognised by the parser.
        /// </summary>
        [Description("Wrapper for Parser.MaximumAge")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public int MaximumAge
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.MaximumAge;
            }

            set
            {
                this.patientSearchInputBoxExtender.MaximumAge = value;
            }
        }

        /// <summary>
        ///  The NHS number.
        /// </summary>
        /// <remarks>
        /// Read-only.
        /// </remarks>
        [Description("Wrapper for Parser.NhsNumber")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public string NhsNumber
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.NhsNumber;
            }
        }

        /// <summary>
        /// The postcode.
        /// </summary>
        /// <remarks>
        /// Read-only. 
        /// </remarks>
        [Description("Wrapper for Parser.Postcode")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public string Postcode
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.Postcode;
            }
        }

        /// <summary>
        /// The character that is used to delimit the start of a group of words.
        /// </summary>
        /// <remarks>
        /// Defaults to a double quote mark ("). Throws an 
        /// ArgumentException if the value is a space, a dash (-), a forward slash (/) or is the 
        /// <see cref="P:NhsCui.Toolkit.Web.PatientSearchInputBox.InformationDelimiter">InformationDelimiter</see>.
        /// </remarks>
        [Description("Wrapper for Parser.StartGroupDelimiter")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true), Category("Data")]
        public char StartGroupDelimiter
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.StartGroupDelimiter;
            }

            set
            {
                this.patientSearchInputBoxExtender.StartGroupDelimiter = value;
            }
        }

        /// <summary>
        /// The patient search criteria to parse. 
        /// </summary>
        [Description("Wrapper for Parser.Text")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(true)]
        public string Text
        {
            get
            {
                this.EnsureChildControls();
                return this.patientSearchInputBoxExtender.Parser.Text;
            }

            set
            {
                this.EnsureChildControls();
                this.textBox.Text = value;
                this.patientSearchInputBoxExtender.Parser.Text = value;
            }
        }

        /// <summary>
        /// The title.
        /// </summary>
        /// <remarks>
        /// Read-only. 
        /// </remarks>
        [Description("Wrapper for Parser.Title")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public string Title
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.Title;
            }
        }

        /// <summary>
        /// A list of known titles.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Required by Spec.")]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Required by Spec.")]
        [Description("Wrapper for Parser.Titles")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false), Category("Data")]
        [NotifyParentProperty(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<Title> Titles
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.Titles;
            }

            set
            {
                this.patientSearchInputBoxExtender.Titles = value;
            }
        }

        /// <summary>
        /// Any remaining text that could not not be matched after the parsing process has identified all other values. 
        /// </summary>
        /// <remarks>
        /// Read-only. 
        /// </remarks>
        [Description("Wrapper for Parser.UnmatchedText")]
        [RefreshProperties(RefreshProperties.All)]
        [Browsable(false)]
        public string UnmatchedText
        {
            get
            {
                return this.patientSearchInputBoxExtender.Parser.UnmatchedText;
            }
        }

        /// <summary>
        /// The underlying text box.
        /// </summary>
        private TextBox TextBox
        {
            get
            {
                this.EnsureChildControls();
                return this.textBox;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses the string in the <see cref="P:NhsCui.Toolkit.Web.PatientSearchInputBox.Text">PatientSearchInputBox.Text</see> 
        /// property and populates the equivalent properties with the results.
        /// </summary>
        public void Parse()
        {
            this.patientSearchInputBoxExtender.Parser.Text = this.textBox.Text;
            this.patientSearchInputBoxExtender.Parser.Parse();
        }

        /// <summary>
        /// CreateChildControls override
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.textBox = new TextBox();
            this.textBox.ID = ID + "_TextBox";
            this.textBox.AutoCompleteType = AutoCompleteType.Disabled;
            this.textBox.Attributes["oncontextmenu"] = "return false";
            this.textBox.Attributes["autocomplete"] = "off";
            this.textBox.TextChanged += new EventHandler(this.TextBoxTextChanged);
            Controls.Add(this.textBox);

            // Create the extender
            this.patientSearchInputBoxExtender = new PatientSearchInputBoxExtender();
            this.patientSearchInputBoxExtender.ID = ID + "_patientSearchInputBoxExtender";
            this.patientSearchInputBoxExtender.TargetControlID = this.textBox.ID;
            this.patientSearchInputBoxExtender.Text = this.textBox.Text;
            this.Controls.Add(this.patientSearchInputBoxExtender);
        }

        /// <summary>
        /// Adds HTML attributes and styles that need to be rendered to the 
        /// specified HtmlTextWriterTag. This method is used primarily by 
        /// control developers. 
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output 
        /// stream to render HTML content on the client. </param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            // since we have passed all our attributes and style onto the
            // textbox nothing else to do here
            if (this.ID != null)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            }
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer
        /// </summary>
        /// <param name="writer">An HtmlTextWriter that represents the output stream to render HTML content on the client. </param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            this.EnsureChildControls();

            // transfer our style to the textbox
            this.TextBox.ControlStyle.CopyFrom(this.ControlStyle);
            this.TextBox.CopyBaseAttributes(this);
            base.RenderContents(writer);
        }

        /// <summary>
        /// Recreates the child controls in a control derived from CompositeControl. 
        /// </summary>
        protected override void RecreateChildControls()
        {
            // no need to recreate child controls
            this.EnsureChildControls();
        }

        /// <summary>
        /// Raise the TextChanged event
        /// </summary>
        /// <param name="e">event args</param>
        protected virtual void OnTextChanged(EventArgs e)
        {
            if (this.TextChanged != null)
            {
                this.TextChanged(this, e);
            }
        }

        /// <summary>
        /// Handle text changed event of our underlying text box
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void TextBoxTextChanged(object sender, EventArgs e)
        {
            this.OnTextChanged(e);
        }

        #endregion
    }
}
