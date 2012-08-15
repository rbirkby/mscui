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
// <date>20-April-2007</date>
// <summary>The control in which patient search criteria are entered. </summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.WinForms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;

    using NhsCui.Toolkit.DateAndTime;
    using NhsCui.Toolkit.PatientSearch;

    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The control in which patient search criteria are entered.
    /// </summary>
    [DefaultProperty("Value")]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "PatientSearchInputBox.bmp")]
    public partial class PatientSearchInputBox : System.Windows.Forms.TextBox
    {
        /// <summary>
        /// The parser instance
        /// </summary>
        private Parser parser = new Parser();

        /// <summary>
        /// Default ctor
        /// </summary>
        public PatientSearchInputBox()
        {
            this.InitializeComponent();
            this.AccessibleName = PatientSearchInputBoxControl.Resources.AccessibleName;
            this.AccessibleDescription = PatientSearchInputBoxControl.Resources.AccessibleDescription;
            this.AccessibleRole = AccessibleRole.Text;
        }

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
                return this.parser.Address;
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
                return this.parser.Age;
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
                return this.parser.AgeUpper;
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<string> CommonFamilyNames
        {
            get
            {
                return this.parser.CommonFamilyNames;
            }

            set
            {
                this.parser.CommonFamilyNames = value;
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
                return this.parser.DateOfBirth;
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
                return this.parser.DateOfBirthUpper;
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
                return this.parser.EndGroupDelimiter;
            }

            set
            {
                this.parser.EndGroupDelimiter = value;
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
                return this.parser.FamilyName;
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
                return this.parser.Gender;
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
                return this.parser.GivenName;
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
                return this.parser.InformationDelimiter;
            }

            set
            {
                this.parser.InformationDelimiter = value;
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
        public List<Information> InformationFormat
        {
            get
            {
                if (this.parser.InformationFormat == null)
                {
                    return new List<Information>();
                }

                return this.parser.InformationFormat;
            }

            set
            {
                this.parser.InformationFormat = value;
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
                return this.parser.IsCommonFamilyName;
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
                return this.parser.IsDateOfBirthAgeMismatch;
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
                return this.parser.IsGenderTitleMismatch;
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
                return this.parser.IsMandatoryInformationEntered;
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
        public List<Information> MandatoryInformation
        {
            get
            {
                if (this.parser.MandatoryInformation == null)
                {
                    return new List<Information>();
                }

                return this.parser.MandatoryInformation;
            }

            set
            {
                this.parser.MandatoryInformation = value;
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
                return this.parser.MaximumAge;
            }

            set
            {
                this.parser.MaximumAge = value;
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
                return this.parser.NhsNumber;
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
                return this.parser.Postcode;
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
                return this.parser.StartGroupDelimiter;
            }

            set
            {
                this.parser.StartGroupDelimiter = value;
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
                return this.parser.Title;
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Title> Titles
        {
            get
            {
                return this.parser.Titles;
            }

            set
            {
                this.parser.Titles = value;
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
                return this.parser.UnmatchedText;
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// Parses the string in the <see cref="P:NhsCui.Toolkit.WinForms.PatientSearchInputBox.Text">PatientSearchInputBox.Text</see> 
        /// property and populates the equivalent properties with the results.
        /// </summary>
        public void Parse()
        {
            this.parser.Text = this.Text;
            this.parser.Parse();
        }

        #endregion
    }
}

