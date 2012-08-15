//-----------------------------------------------------------------------
// <copyright file="PatientBanner.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to provide a consistent layout for common patient identification information within applications. </summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.ComponentModel;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Globalization;
    using System.Resources;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.Design;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using NhsCui.Toolkit;
    using NhsCui.Toolkit.DateAndTime;
    using AjaxControlToolkit;

    /// <summary>
    /// The control used to provide a consistent layout for common patient identification information within applications.
    /// </summary>    
    [DefaultProperty("Identifier"), DefaultEvent("Load")]
    [ToolboxData("<{0}:PatientBanner IdentifierType=\"Other\" Identifier=\"XXX XXX XXXX\" FamilyName=\"FamilyName\" GivenName=\"GivenName\" Title=\"Title\" runat=\"server\"></{0}:PatientBanner>")]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "PatientBanner.bmp")]
    [Designer(typeof(PatientBannerDesigner))]
    [ParseChildren(true)]
    [PersistChildren(false)]
    [RequiredScript(typeof(CommonNhsCuiToolkitScripts), 1)]
    public class PatientBanner : CompositeControl, IScriptControl, INotifyPropertyChanged, IPostBackEventHandler
    {
        #region Const Values
        /// <summary>
        /// maximum number of characters for the address summary
        /// </summary>
        private const int AddressSummaryMaxChars = 20;

        /// <summary>
        /// Max chars that can be shown in preferred name
        /// </summary>
        private const int PreferredNameMaxChars = 35;
        #endregion

        #region Member Variables
        /// <summary>
        /// control to hold Zone One controls
        /// </summary>
        private PatientBannerZoneOneContainer zoneOneContainer;

        /// <summary>
        /// control to hold Zone Two controls
        /// </summary>
        private PatientBannerZoneTwoContainer zoneTwoContainer;

        /// <summary>
        /// Control used to hold client state, i.e. whether the panel was expanded
        /// on a postback
        /// </summary>
        private HiddenField clientState;

        /// <summary>
        /// Template for content to appear in subsection three
        /// </summary>
        private ITemplate subsectionThreeTemplate;

        /// <summary>
        /// Template for content to appear in subsection four
        /// </summary>
        private ITemplate subsectionFourTemplate;

        /// <summary>
        /// Patient allergies
        /// </summary>
        private AllergyCollection patientAllergies = new AllergyCollection();
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a PatientBanner object. 
        /// </summary>
        public PatientBanner()
        {
            // Hiding the patient banner to avoid flickering. Will be unhided in Init() on client side after the layout
            this.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");            
        }

        #endregion

        #region Events
        
        /// <summary>
        /// Occurs when a property value changes. 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when the link View all addresses is clicked
        /// </summary>
        public event EventHandler<PatientBannerEventArgs> ViewAllAddressesClick;

        /// <summary>
        /// Occurs when the link View all contact details is clicked
        /// </summary>
        public event EventHandler<PatientBannerEventArgs> ViewAllContactDetailsClick;

        /// <summary>
        /// Occurs when the link View allergy record is clicked
        /// </summary>
        public event EventHandler<PatientBannerEventArgs> ViewAllergyRecordClick;

        /// <summary>
        /// Occurs when the Gender value is clicked
        /// </summary>
        public event EventHandler<PatientBannerEventArgs> GenderValueClick;

        /// <summary>
        /// Occurs when Identifier is clicked
        /// </summary>
        public event EventHandler<PatientBannerEventArgs> IdentifierClick;

        #endregion

        #region Patient Image
        /// <summary>
        /// Gets or sets the URL of the patient's image displayed in the PatientBanner. 
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        [Description("The patient image displayed in the PatientBanner")]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        [UrlProperty()]
        public string PatientImage
        {
            get
            {
                return this.GetPropertyValue<string>("PatientImage", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("PatientImage", value);
            }
        }

        /// <summary>
        /// Gets or sets the whether the patient's image can be shown. 
        /// </summary>
        /// <remarks>
        /// Defaults to "false". If true but the file specified for <see cref="P:NhsCui.Toolkit.Web.PatientBanner.PatientImage">PatientImage</see> 
        /// cannot be not found, the PatientBanner control behaves as if this property is false.
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue(false)]
        [Description("The ImageDisplayed property determines if we can show the patient image.")]
        public bool ImageDisplayed
        {
            get
            {
                return this.GetPropertyValue<bool>("ImageDisplayed", false);
            }

            set
            {
                this.SetPropertyValue<bool>("ImageDisplayed", value);
            }
        }
        #endregion

        #region Patient Name
        /// <summary>
        /// Gets or sets the patient's family name. 
        /// </summary>
        [Bindable(true), Category("Patient Details")]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "FamilyName")]
        [Description("The family name associated with the patient")]
        public string FamilyName
        {
            get
            {
                return this.GetLocalizablePropertyValue("FamilyName");
            }

            set
            {
                this.SetPropertyValue<string>("FamilyName", value);
            }
        }

        /// <summary>
        /// Gets or sets the patient's given name. 
        /// </summary>
        [Bindable(true), Category("Patient Details")]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "GivenName")]
        [Description("The given name associated with the patient")]
        public string GivenName
        {
            get
            {
                return this.GetLocalizablePropertyValue("GivenName");
            }

            set
            {
                this.SetPropertyValue<string>("GivenName", value);
            }
        }

        /// <summary>
        /// Gets or sets the patient's title. 
        /// </summary>
        [Bindable(true), Category("Patient Details")]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "Title")]
        [Description("The title associated with the patient")]
        public string Title
        {
            get
            {
                return this.GetLocalizablePropertyValue("Title");
            }

            set
            {
                this.SetPropertyValue<string>("Title", value);
            }
        }

        /// <summary>
        /// Gets the complete patient name as displayed. 
        /// </summary>
        [Category("Patient Details")]
        [Description("The current formatted name for the patient")]
        public string NameDisplayValue
        {
            get
            {
                return PatientName.Format(this.FamilyName, this.GivenName, this.Title);
            }
        }

        /// <summary>
        /// Gets or sets the patient's preferred name.
        /// </summary>
        [Category("Patient Details")]
        [Description("The preferred name associated with the patient")]
        public string PreferredName
        {
            get
            {               
                return this.GetPropertyValue<string>("PreferredName", string.Empty); 
            }

            set
            {
                this.SetPropertyValue<string>("PreferredName", value);
            }
        }
        #endregion

        #region Patient Identifier (NHS Number)

        /// <summary>
        /// Gets or sets whether to process the <see cref="P:NhsCui.Toolkit.Web.PatientBanner.Identifier">Identifier</see> with the NhsNumber 
        /// validation checksum.
        /// </summary>
        /// <remarks>
        /// Defaults to "Other". If this is set to "Other", no validation is performed. If this is set to "NhsNumber", the text must be 
        /// a valid NHS number.
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue(IdentifierType.Other)]
        [Description("Determines whether to process the Identifier with the NhsNumber validation checksum")]
        public IdentifierType IdentifierType
        {
            get
            {
                return this.GetPropertyValue<IdentifierType>("IdentifierType", IdentifierType.Other);
            }

            set
            {
                try
                {
                    if (!Enum.IsDefined(typeof(IdentifierType), value))
                    {
                        throw new ArgumentOutOfRangeException("value");
                    }

                    if (value != this.IdentifierType)
                    {
                        if (value == IdentifierType.NhsNumber && this.Identifier.Length > 0)
                        {
                            NhsNumber.ParseNhsNumber(this.Identifier);
                        }

                        this.SetPropertyValue<IdentifierType>("IdentifierType", value);
                        this.LastIdentifierValid = true;
                    }
                }
                catch
                {
                    this.LastIdentifierValid = false;
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets or sets a unique identifier associated with the patient. 
        /// </summary>
        /// <remarks>
        /// If the associated <see cref="P:NhsCui.Toolkit.Web.PatientBanner.IdentifierType">IdentifierType</see> property is NhsNumber, 
        /// the Identifier must be a valid NHS Number. 
        /// </remarks>
        [Bindable(true), Category("Patient Details")]
        [Description("The unique NHS identifier associated with the patient")]
        public string Identifier
        {
            get
            {
                return this.GetPropertyValue<string>("Identifier", string.Empty);
            }

            set
            {
                try
                {
                    string identifierText = null;
                    if (this.IdentifierType == IdentifierType.NhsNumber)
                    {
                        identifierText = NhsNumber.ParseNhsNumber(value).ToString();
                    }
                    else
                    {
                        identifierText = value;
                    }

                    this.SetPropertyValue<string>("Identifier", identifierText);
                    this.LastIdentifierValid = true;
                }
                catch
                {
                    this.LastIdentifierValid = false;
                    throw;
                }
            }
        }

        #endregion

        #region Patient Date of Birth / Death

        /// <summary>
        /// Gets or sets the patient's date of birth. 
        /// </summary>
        [Bindable(true), Category("Patient Details"), DefaultValue(null)]
        [Description("The date of birth associated with the patient")]
        public DateTime DateOfBirth
        {
            get
            {
                return this.GetPropertyValue<DateTime>("DateOfBirth", DateTime.MinValue);
            }

            set
            {
                if (value > DateTime.Now)
                {
                    this.SetPropertyValue<DateTime>("DateOfBirth", DateTime.MinValue);
                    throw new ArgumentException(PatientBannerControl.Resources.InvalidDateOfBirthExcpetionMessage);
                }

                this.SetPropertyValue<DateTime>("DateOfBirth", value);
            }
        }

        /// <summary>
        /// Gets or sets the patient's date of death. 
        /// </summary>
        [Bindable(true), Category("Patient Details"), DefaultValue(null)]
        [Description("The date of death associated with the patient")]
        public DateTime DateOfDeath
        {
            get
            {
                return this.GetPropertyValue<DateTime>("DateOfDeath", DateTime.MinValue);
            }

            set
            {
                if (value > DateTime.Now)
                {
                    this.SetPropertyValue<DateTime>("DateOfDeath", DateTime.MinValue);
                    throw new ArgumentException(PatientBannerControl.Resources.InvalidDateOfDeathExceptionMessage);
                }

                this.SetPropertyValue<DateTime>("DateOfDeath", value);
            }
        }

        #endregion

        #region Patient Gender

        /// <summary>
        /// Gets or sets the patient�s gender. 
        /// </summary>
        /// <remarks>
        /// This may be "Female", "Male", "NotSpecified" or "NotKnown". 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue(PatientGender.NotKnown)]
        [Description("Determines the gender of the patient")]
        public PatientGender Gender
        {
            get
            {
                return this.GetPropertyValue<PatientGender>("Gender", PatientGender.NotKnown);
            }

            set
            {
                if (!Enum.IsDefined(typeof(PatientGender), value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                this.SetPropertyValue<PatientGender>("Gender", value);
            }
        }

        #endregion

        #region Patient Contact Details

        /// <summary>
        /// Gets or sets the patient's home phone number. 
        /// </summary>
        /// <remarks>
        /// If this element is left empty, <see cref="P:NhsCui.Toolkit.Web.PatientBanner.HomePhoneLabelText">HomePhoneLabelText</see> 
        /// will be displayed 
        /// with blank space next to it. 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("")]
        [Description("The home phone number associated with the patient")]
        public string HomePhoneNumber
        {
            get
            {
                return this.GetPropertyValue<string>("HomePhoneNumber", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("HomePhoneNumber", value);
            }
        }

        /// <summary>
        /// Gets or sets the patient's work phone number. 
        /// </summary>
        /// <remarks>
        /// If this element is left empty, <see cref="P:NhsCui.Toolkit.Web.PatientBanner.WorkPhoneLabelText">WorkPhoneLabelText</see> will be displayed 
        /// with blank space next to it. 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("")]
        [Description("TThe work phone number associated with the patient")]
        public string WorkPhoneNumber
        {
            get
            {
                return this.GetPropertyValue<string>("WorkPhoneNumber", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("WorkPhoneNumber", value);
            }
        }

        /// <summary>
        /// Gets or sets the patient's mobile phone number. 
        /// </summary>
        /// <remarks>
        /// If this element is left empty, <see cref="P:NhsCui.Toolkit.Web.PatientBanner.MobilePhoneLabelText">MobilePhoneLabelText</see> will be displayed 
        /// with blank space next to it.
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("")]
        [Description("The mobile phone number associated with the patient")]
        public string MobilePhoneNumber
        {
            get
            {
                return this.GetPropertyValue<string>("MobilePhoneNumber", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("MobilePhoneNumber", value);
            }
        }

        /// <summary>
        /// Gets or sets the patient's email address. 
        /// </summary>
        /// <remarks>
        /// If this element is left empty, <see cref="P:NhsCui.Toolkit.Web.PatientBanner.EmailLabelText">EmailLabelText</see> will be 
        /// displayed with blank space next to it.  
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("")]
        [Description("The email address associated with the patient ")]
        public string EmailAddress
        {
            get
            {
                return this.GetPropertyValue<string>("EmailAddress", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("EmailAddress", value);
            }
        }

        /// <summary>
        /// Gets or sets the whether the subsection two labels can be wrapped. 
        /// </summary>
        /// <remarks>
        /// Defaults to "false". If true Labels can be wrapped mid-word.
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue(false)]
        [Description("Indicates whether subsection two labels can be wrapped mid-word or not.")]
        public bool WrapSubsectionTwoLabels
        {
            get
            {
                return this.GetPropertyValue<bool>("WrapSubsectionTwoLabels", false);
            }

            set
            {
                this.SetPropertyValue<bool>("WrapSubsectionTwoLabels", value);
            }
        }

        /// <summary>
        /// Gets or sets the width of Subsection Two labels column. 
        /// </summary>        
        [Bindable(true), Category("Appearance")]
        [Description("Width of Subsection two labels column.")]
        [TypeConverter(typeof(UnitConverter))]
        public Unit SubsectionTwoLabelWidth
        {
            get
            {
                return this.GetPropertyValue<Unit>("SubsectionTwoLabelWidth", Unit.Empty);
            }

            set
            {
                this.SetPropertyValue<Unit>("SubsectionTwoLabelWidth", value);
            }
        }

        #endregion

        #region Patient Address

        /// <summary>
        /// Gets or sets the first line of the patient's address. 
        /// </summary>
        /// <remarks>
        /// At most, 8 lines of address data will be displayed. If an address element is empty, it will not be displayed.
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The first address line associated with the patient")]
        public string Address1
        {
            get
            {
                return this.GetPropertyValue<string>("Address1", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("Address1", value);
            }
        }

        /// <summary>
        /// Gets or sets the second line of the patient's address. 
        /// </summary>
        /// <remarks>
        /// At most, 8 lines of address data will be displayed. If an address element is empty, it will not be displayed.
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The second address line associated with the patient")]
        public string Address2
        {
            get
            {
                return this.GetPropertyValue<string>("Address2", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("Address2", value);
            }
        }

        /// <summary>
        /// Gets or sets the third line of the patient's address. 
        /// </summary>
        ///<remarks>
        /// At most, 8 lines of address data will be displayed. If an address element is empty, it will not be displayed.
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The third address line associated with the patient")]
        public string Address3
        {
            get
            {
                return this.GetPropertyValue<string>("Address3", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("Address3", value);
            }
        }

        /// <summary>
        /// Gets or sets the town in the patient�s address. 
        /// </summary>
        /// <remarks>
        /// At most, 8 lines of address data will be displayed. If an address element is empty, it will not be displayed.
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The town associated with the patient�s address")]
        public string Town
        {
            get
            {
                return this.GetPropertyValue<string>("Town", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("Town", value);
            }
        }

        /// <summary>
        /// Gets or sets the county in the patient�s address.  
        /// </summary>
        /// <remarks>
        /// At most, 8 lines of address data will be displayed. If an address element is empty, it will not be displayed.
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The county associated with the patient�s address")]
        public string County
        {
            get
            {
                return this.GetPropertyValue<string>("County", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("County", value);
            }
        }

        /// <summary>
        /// Gets or sets the patient�s postcode. 
        /// </summary>
        /// <remarks>
        /// At most, 8 lines of address data will be displayed. If the postcode 
        /// is present, it should display in capitalized form and as the final element before 
        /// <see cref="P:NhsCui.Toolkit.Web.PatientBanner.Country">Country</see>. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The postcode associated with the patient�s address")]
        public string Postcode
        {
            get
            {
                return this.GetPropertyValue<string>("Postcode", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("Postcode", value);
            }
        }

        /// <summary>
        /// Gets or sets the country in the patient�s address. 
        /// </summary>
        /// <remarks>
        /// At most, 8 lines of address data will be displayed. If an address element is empty, it will not be displayed.
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The country associated with the patient�s address")]
        public string Country
        {
            get
            {
                return this.GetPropertyValue<string>("Country", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("Country", value);
            }
        }

        /// <summary>
        /// Gets or sets the Address type of the patient�s address. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Usual address".
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("Usual address")]
        [Description("The address type of the patient�s address")]
        public string AddressTypeLabelText
        {
            get
            {
                return this.GetPropertyValue<string>("AddressTypeLabelText", AddressLabelControl.Resources.AddressType);
            }

            set
            {
                this.SetPropertyValue<string>("AddressTypeLabelText", value);
            }
        }

        #endregion

        #region Allergy Information

        /// <summary>
        /// Gets or sets the patient�s Allergy information. 
        /// </summary>
        /// <remarks>
        /// This may be "Present", "NotRecorded" or "NoneKnown", "Unavailable". Defaults to "Unavailable".
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue(AllergyInformation.Unavailable)]
        [Description("Determines the allergy information of the patient")]
        public AllergyInformation AllergyInformation
        {
            get
            {
                return this.GetPropertyValue<NhsCui.Toolkit.AllergyInformation>("AllergyInformation", AllergyInformation.Unavailable);
            }

            set
            {
                if (!Enum.IsDefined(typeof(AllergyInformation), value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                this.SetPropertyValue<NhsCui.Toolkit.AllergyInformation>("AllergyInformation", value);
            }
        }

        /// <summary>
        /// Gets a collection containing the list of any patient allergies.
        /// </summary>
        [Category("Patient Details"), DefaultValue("")]
        [Bindable(true)]
        [Localizable(true)]
        [Description("List of patient allergies")]
        [NotifyParentProperty(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(PatientBanner))]
        public AllergyCollection Allergies
        {
            get
            {
                return this.GetPropertyValue<AllergyCollection>("Allergies", this.patientAllergies);                
            }
        }
        #endregion

        #region ZoneTwo Subsections Details
        /// <summary>
        /// Gets or sets the width of Subsection One. 
        /// </summary>
        /// <remarks>
        /// Defaults to 25%. 
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [Description("Width of Subsection one.")]
        [TypeConverter(typeof(UnitConverter))]
        public Unit SubsectionOneWidth
        {
            get
            {
                return this.GetPropertyValue<Unit>("SubsectionOneWidth", Unit.Percentage(25));
            }

            set
            {
                this.SetPropertyValue<Unit>("SubsectionOneWidth", value);
            }
        }

        /// <summary>
        /// Gets or sets the width of Subsection Two. 
        /// </summary>
        /// <remarks>
        /// Defaults to 26%. 
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [Description("Width of Subsection two.")]
        [TypeConverter(typeof(UnitConverter))]
        public Unit SubsectionTwoWidth
        {
            get
            {
                return this.GetPropertyValue<Unit>("SubsectionTwoWidth", Unit.Percentage(26));
            }

            set
            {
                this.SetPropertyValue<Unit>("SubsectionTwoWidth", value);
            }
        }

        /// <summary>
        /// Gets or sets the width of Subsection Three. 
        /// </summary>
        /// <remarks>
        /// Defaults to 12%. 
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [Description("Width of Subsection three.")]
        [TypeConverter(typeof(UnitConverter))]
        public Unit SubsectionThreeWidth
        {
            get
            {
                return this.GetPropertyValue<Unit>("SubsectionThreeWidth", Unit.Percentage(12));
            }

            set
            {
                this.SetPropertyValue<Unit>("SubsectionThreeWidth", value);
            }
        }

        /// <summary>
        /// Gets or sets the width of Subsection Four. 
        /// </summary>
        /// <remarks>
        /// Defaults to 12%. 
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [Description("Width of Subsection four.")]
        [TypeConverter(typeof(UnitConverter))]
        public Unit SubsectionFourWidth
        {
            get
            {
                return this.GetPropertyValue<Unit>("SubsectionFourWidth", Unit.Percentage(12));
            }

            set
            {
                this.SetPropertyValue<Unit>("SubsectionFourWidth", value);
            }
        }

        /// <summary>
        /// Gets or sets the width of Subsection Five. 
        /// </summary>
        /// <remarks>
        /// Defaults to 25%. 
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [Description("Width of Subsection five.")]
        [TypeConverter(typeof(UnitConverter))]
        public Unit SubsectionFiveWidth
        {
            get
            {
                return this.GetPropertyValue<Unit>("SubsectionFiveWidth", Unit.Percentage(25));
            }

            set
            {
                this.SetPropertyValue<Unit>("SubsectionFiveWidth", value);
            }
        }
               
        /// <summary>
        /// Gets or sets the display text for View all addresses link. 
        /// </summary>
        /// <remarks>
        /// Defaults to "View all addresses". 
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "ViewAllAddressLinkText")]
        [Description("The display text associated with View all addresses link.")]
        public string ViewAllAddressLinkText
        {
            get
            {
                return this.GetLocalizablePropertyValue("ViewAllAddressLinkText");
            }

            set
            {
                this.SetPropertyValue<string>("ViewAllAddressLinkText", value);
            }
        }

        /// <summary>
        /// Gets or sets the display text for View all contact details link. 
        /// </summary>
        /// <remarks>
        /// Defaults to "View all contact details". 
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "ViewAllContactDetailsLinkText")]
        [Description("The display text associated with ViewAllContactDetails link.")]
        public string ViewAllContactDetailsLinkText
        {
            get
            {
                return this.GetLocalizablePropertyValue("ViewAllContactDetailsLinkText");
            }

            set
            {
                this.SetPropertyValue<string>("ViewAllContactDetailsLinkText", value);
            }
        }

        /// <summary>
        /// Gets or sets the display text for View allergy record link. 
        /// </summary>
        /// <remarks>
        /// Defaults to "View allergy record." 
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "ViewAllergyRecordLinkText")]
        [Description("The display text associated with View allergy record link.")]
        public string ViewAllergyRecordLinkText
        {
            get
            {
                return this.GetLocalizablePropertyValue("ViewAllergyRecordLinkText");
            }

            set
            {
                this.SetPropertyValue<string>("ViewAllergyRecordLinkText", value);
            }
        }

        /// <summary>
        /// Gets or sets the URL of the Allergies icon displayed in the PatientBanner when the allergies information is present. 
        /// </summary>
        /// <remarks>
        /// This icon is displayed when <see cref="P:NhsCui.Toolkit.Web.PatientBanner.AllergyInformation">AllergyInformation</see> is "Present".
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        [Description("The Allergies Icon displayed in the PatientBanner when the AllergiesInformation is Present")]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        [UrlProperty()]
        public string AllergiesPresentIcon
        {
            get
            {
                return this.GetPropertyValue<string>("AllergiesRecordedIcon", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("AllergiesRecordedIcon", value);
            }
        }

        /// <summary>
        /// Gets or sets the URL of the Allergies icon displayed in the PatientBanner when the allergies information is not recorded or none known. 
        /// </summary>
        /// <remarks>
        /// This icon is displayed when <see cref="P:NhsCui.Toolkit.Web.PatientBanner.AllergyInformation">AllergyInformation</see> is "NotRecorded" or "NoneKnown".
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        [Description("The Allergies Icon displayed in the PatientBanner when the AllergiesInformation is NotRecorded or NoneKnown")]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        [UrlProperty()]
        public string AllergiesNotPresentIcon
        {
            get
            {
                return this.GetPropertyValue<string>("AllergiesNotRecordedIcon", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("AllergiesNotRecordedIcon", value);
            }
        }

        /// <summary>
        /// Gets or sets the URL of the Allergies icon displayed in the PatientBanner when the allergies information is unavailable. 
        /// </summary>
        /// <remarks>
        /// This icon is displayed when <see cref="P:NhsCui.Toolkit.Web.PatientBanner.AllergyInformation">AllergyInformation</see> is "Unavailable".
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        [Description("The Allergies Icon displayed in the PatientBanner when the AllergiesInformation is unavailable")]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        [UrlProperty()]
        public string AllergiesUnavailableIcon
        {
            get
            {
                return this.GetPropertyValue<string>("AllergiesUnavailableIcon", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("AllergiesUnavailableIcon", value);
            }
        }
        #endregion
        
        #region Localization Properties
        /// <summary>
        /// Gets or sets the caption associated with the patient's unique identifier or NHS number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "NHS No.". 
        /// </remarks>
        [Category("Patient Details"), Localizable(true)]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "IdentifierLabelText")]
        [Description("The caption associated with the NhsNumber or Identifier")]
        public string IdentifierLabelText
        {
            get
            {
                return this.GetLocalizablePropertyValue("IdentifierLabelText");
            }

            set
            {
                this.SetPropertyValue<string>("IdentifierLabelText", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient's gender.
        /// </summary>
        /// <remarks>
        /// Defaults to "Gender". 
        /// </remarks>
        [Category("Patient Details"), Localizable(true)]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "GenderLabelText")]
        [Description("The caption associated with the Gender label")]
        public string GenderLabelText
        {
            get
            {
                return this.GetLocalizablePropertyValue("GenderLabelText");
            }

            set
            {
                this.SetPropertyValue<string>("GenderLabelText", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient's date of birth. 
        /// </summary>
        /// <remarks>
        /// Defaults to "DoB". 
        /// </remarks>
        [Category("Patient Details"), Localizable(true)]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "DateOfBirthLabelText")]
        [Description("The caption associated with the patient�s date of birth")]
        public string DateOfBirthLabelText
        {
            get
            {
                return this.GetLocalizablePropertyValue("DateOfBirthLabelText");
            }

            set
            {
                this.SetPropertyValue<string>("DateOfBirthLabelText", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient's date of death. 
        /// </summary>
        /// <remarks>
        /// Defaults to "DoD".
        /// </remarks>
        [Category("Patient Details"), Localizable(true)]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "DateOfDeathLabelText")]
        [Description("The caption associated with the patient�s date of death")]
        public string DateOfDeathLabelText
        {
            get
            {
                return this.GetLocalizablePropertyValue("DateOfDeathLabelText");
            }

            set
            {
                this.SetPropertyValue<string>("DateOfDeathLabelText", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with Subsection One. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Address". 
        /// </remarks>
        [Category("Appearance"), Localizable(true)]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "SubsectionOneTitle")]
        [Description("The caption associated with the subsection one")]
        public string SubsectionOneTitle
        {
            get
            {
                return this.GetLocalizablePropertyValue("SubsectionOneTitle");
            }

            set
            {
                this.SetPropertyValue<string>("SubsectionOneTitle", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with Subsection Two. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Phone and email". 
        /// </remarks>
        [Category("Appearance"), Localizable(true)]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "SubsectionTwoTitle")]
        [Description("The caption associated with the subsection two")]
        public string SubsectionTwoTitle
        {
            get
            {
                return this.GetLocalizablePropertyValue("SubsectionTwoTitle");
            }

            set
            {
                this.SetPropertyValue<string>("SubsectionTwoTitle", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with Subsection Three. 
        /// </summary>
        /// <remarks>
        /// Defaults to "". 
        /// </remarks>
        [Category("Appearance"), Localizable(true)]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "SubsectionThreeTitle")]
        [Description("The caption associated with subsection three")]
        public string SubsectionThreeTitle
        {
            get
            {
                return this.GetLocalizablePropertyValue("SubsectionThreeTitle");
            }

            set
            {
                this.SetPropertyValue<string>("SubsectionThreeTitle", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with Subsection Four. 
        /// </summary>
        /// <remarks>
        /// Defaults to "". 
        /// </remarks>
        [Category("Appearance"), Localizable(true)]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "SubsectionFourTitle")]
        [Description("The caption associated with subsection four")]
        public string SubsectionFourTitle
        {
            get
            {
                return this.GetLocalizablePropertyValue("SubsectionFourTitle");
            }

            set
            {
                this.SetPropertyValue<string>("SubsectionFourTitle", value);
            }
        }
               
        /// <summary>
        /// Gets or sets the caption associated with the patient's home phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Home". 
        /// </remarks>
        [Category("Patient Details"), Localizable(true)]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "HomePhoneLabelText")]
        [Description("The caption associated with the home phone number")]
        public string HomePhoneLabelText
        {
            get
            {
                return this.GetLocalizablePropertyValue("HomePhoneLabelText");
            }

            set
            {
                this.SetPropertyValue<string>("HomePhoneLabelText", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient's work phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Work". 
        /// </remarks>
        [Category("Patient Details"), Localizable(true)]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "WorkPhoneLabelText")]
        [Description("The caption associated with the Work phone number")]
        public string WorkPhoneLabelText
        {
            get
            {
                return this.GetLocalizablePropertyValue("WorkPhoneLabelText");
            }

            set
            {
                this.SetPropertyValue<string>("WorkPhoneLabelText", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient's mobile phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Mobile". 
        /// </remarks>
        [Category("Patient Details"), Localizable(true)]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "MobilePhoneLabelText")]
        [Description("The caption associated with the Mobile phone number")]
        public string MobilePhoneLabelText
        {
            get
            {
                return this.GetLocalizablePropertyValue("MobilePhoneLabelText");
            }

            set
            {
                this.SetPropertyValue<string>("MobilePhoneLabelText", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient's email address. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Email". 
        /// </remarks>
        [Category("Patient Details"), Localizable(true)]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "EmailLabelText")]
        [Description("The caption associated with the email address")]
        public string EmailLabelText
        {
            get
            {
                return this.GetLocalizablePropertyValue("EmailLabelText");
            }

            set
            {
                this.SetPropertyValue<string>("EmailLabelText", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient's preferred name. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Preferred name". 
        /// </remarks>
        [Category("Patient Details"), Localizable(true)]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "PreferredNameLabelText")]
        [Description("The caption associated with the patients preferred name")]
        public string PreferredNameLabelText
        {
            get
            {
                return this.GetLocalizablePropertyValue("PreferredNameLabelText");
            }

            set
            {
                this.SetPropertyValue<string>("PreferredNameLabelText", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient's age at death. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Age at death". 
        /// </remarks>
        [Category("Patient Details"), Localizable(true)]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "AgeAtDeathLabelText")]
        [Description("The caption associated with the patients preferred name")]
        public string AgeAtDeathLabelText
        {
            get
            {
                return this.GetLocalizablePropertyValue("AgeAtDeathLabelText");
            }

            set
            {
                this.SetPropertyValue<string>("AgeAtDeathLabelText", value);
            }
        }
        #endregion

        #region Expand / Collapse Properties
        /// <summary>
        /// Gets or sets the URL for the PatientBanner dropdown image. 
        /// </summary>
        [Category("Appearance"), DefaultValue("")]
        [Description("Gets or sets the url for the PatientBanner drop down image")]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        [UrlProperty()]
        public string DropDownImage
        {
            get
            {
                return this.GetPropertyValue<string>("DropDownImage", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("DropDownImage", value);
            }
        }

        /// <summary>
        /// Gets or sets the URL for the dropdown image to be displayed when Zone Two is expanded. 
        /// </summary>
        [Category("Appearance"), DefaultValue("")]
        [Description("Gets or sets the url for the PatientBanner drop down image to be used when Zone Two is expanded")]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        [UrlProperty()]
        public string CollapseImage
        {
            get
            {
                return this.GetPropertyValue<string>("CollapseImage", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("CollapseImage", value);
            }
        }

        /// <summary>
        /// Gets or sets the expanded state of Zone Two. 
        /// </summary> 
        /// <remarks>
        /// Defaults to false. 
        /// </remarks>
        [Category("Appearance"), DefaultValue(false)]
        [Description("Gets or sets the expanded state of Zone Two")]
        public bool ZoneTwoExpanded
        {
            get
            {
                if (this.Page.IsPostBack && this.clientState != null)
                {
                    return this.ClientZoneTwoExpanded;
                }

                return this.GetPropertyValue<bool>("ZoneTwoExpanded", false);
            }

            set
            {
                this.SetPropertyValue<bool>("ZoneTwoExpanded", value);
                this.ClientZoneTwoExpanded = value;
            }
        }
        #endregion

        #region Css Class Overrides

        /// <summary>
        /// Gets or sets the default CSS style for Zone One. 
        /// </summary>
        [Category("Appearance"), DefaultValue("")]
        [Description("Gets or sets the default CSS style for Zone One")]
        public string ZoneOneStyle
        {
            get
            {
                return this.GetCssClassOverride(PatientBannerCssClasses.ZoneOne);
            }

            set
            {
                this.SetCssClassOverride(PatientBannerCssClasses.ZoneOne, value);
            }
        }

        /// <summary>
        ///Gets or sets the style for labels in Zone One. 
        /// </summary>
        [Category("Appearance"), DefaultValue("")]
        [Description("Gets or sets the style for labels in Zone One")]
        public string ZoneOneLabelStyle
        {
            get
            {
                return this.GetCssClassOverride(PatientBannerCssClasses.ZoneOneLabel);
            }

            set
            {
                this.SetCssClassOverride(PatientBannerCssClasses.ZoneOneLabel, value);
            }
        }

        /// <summary>
        /// Gets or sets the style for data in Zone One. 
        /// </summary>
        [Category("Appearance"), DefaultValue("")]
        [Description("Gets or sets the style for data in Zone One")]
        public string ZoneOneDataStyle
        {
            get
            {
                return this.GetCssClassOverride(PatientBannerCssClasses.ZoneOneData);
            }

            set
            {
                this.SetCssClassOverride(PatientBannerCssClasses.ZoneOneData, value);
            }
        }

        /// <summary>
        /// Gets or sets the style for the patient's name. 
        /// </summary>
        [Category("Appearance"), DefaultValue("")]
        [Description("Gets or sets the style for the patient name")]
        public string PatientNameStyle
        {
            get
            {
                return this.GetCssClassOverride(PatientBannerCssClasses.PatientName);
            }

            set
            {
                this.SetCssClassOverride(PatientBannerCssClasses.PatientName, value);
            }
        }

        /// <summary>
        /// Gets or sets the style for the Zone Two title area. 
        /// </summary>
        [Category("Appearance"), DefaultValue("")]
        [Description("Gets or sets the style for the Zone Two title area")]
        public string ZoneTwoTitleStyle
        {
            get
            {
                return this.GetCssClassOverride(PatientBannerCssClasses.ZoneTwoTitle);
            }

            set
            {
                this.SetCssClassOverride(PatientBannerCssClasses.ZoneTwoTitle, value);
            }
        }

        /// <summary>
        /// Gets or sets the style for the Zone Two panel. 
        /// </summary>
        [Category("Appearance"), DefaultValue("")]
        [Description("Gets or sets the style for the Zone Two panel")]
        public string ZoneTwoStyle
        {
            get
            {
                return this.GetCssClassOverride(PatientBannerCssClasses.ZoneTwo);
            }

            set
            {
                this.SetCssClassOverride(PatientBannerCssClasses.ZoneTwo, value);
            }
        }

        /// <summary>
        /// Gets or sets the style for data in Zone Two. 
        /// </summary>
        [Category("Appearance"), DefaultValue("")]
        [Description("Gets or sets the style for the data in Zone Two")]
        public string ZoneTwoDataStyle
        {
            get
            {
                return this.GetCssClassOverride(PatientBannerCssClasses.ZoneTwoData);
            }

            set
            {
                this.SetCssClassOverride(PatientBannerCssClasses.ZoneTwoData, value);
            }
        }

        /// <summary>
        /// Gets or sets the style for labels in Zone Two. 
        /// </summary>
        [Category("Appearance"), DefaultValue("")]
        [Description("Gets or sets the style for labels in Zone Two")]
        public string ZoneTwoLabelStyle
        {
            get
            {
                return this.GetCssClassOverride(PatientBannerCssClasses.ZoneTwoLabel);
            }

            set
            {
                this.SetCssClassOverride(PatientBannerCssClasses.ZoneTwoLabel, value);
            }
        }

        /// <summary>
        /// Gets or sets the style to be used for living patients. 
        /// </summary>
        [Category("Appearance"), DefaultValue("")]
        [Description("Gets or sets the style to be used for active patients")]
        public string ActivePatientStyle
        {
            get
            {
                return this.GetCssClassOverride(PatientBannerCssClasses.ActivePatient);
            }

            set
            {
                this.SetCssClassOverride(PatientBannerCssClasses.ActivePatient, value);
            }
        }

        /// <summary>
        /// Gets or sets the style to be used for deceased patients. 
        /// </summary>
        [Category("Appearance"), DefaultValue("")]
        [Description("Gets or sets the style to be used for deceased patients")]
        public string DeadPatientStyle
        {
            get
            {
                return this.GetCssClassOverride(PatientBannerCssClasses.DeadPatient);
            }

            set
            {
                this.SetCssClassOverride(PatientBannerCssClasses.DeadPatient, value);
            }
        }

        /// <summary>
        /// Gets or sets the style that indicates Zone Two is available. 
        /// </summary>
        [Category("Appearance"), DefaultValue("")]
        [Description("Gets or sets the style to be used to indicate zone two can be expanded.")]
        public string ZoneTwoHoverStyle
        {
            get
            {
                return this.GetCssClassOverride(PatientBannerCssClasses.ZoneTwoHover);
            }

            set
            {
                this.SetCssClassOverride(PatientBannerCssClasses.ZoneTwoHover, value);
            }
        }

        /// <summary>
        /// Gets or sets the style that indicates when the data in Zone One has tooltip. 
        /// </summary>
        [Category("Appearance"), DefaultValue("")]
        [Description("Gets or sets the style that indicates when the data in Zone one has tooltip.")]
        public string ZoneOneDataWithTooltipHoverStyle
        {
            get
            {
                return this.GetCssClassOverride(PatientBannerCssClasses.ZoneOneDataWithTooltipHover);
            }

            set
            {
                this.SetCssClassOverride(PatientBannerCssClasses.ZoneOneDataWithTooltipHover, value);
            }
        }

        /// <summary>
        /// Gets or sets the style that indicates when labels in Zone One have a tooltip. 
        /// </summary>
        [Category("Appearance"), DefaultValue("")]
        [Description("Gets or sets the style that indicates when the labels in Zone one has tooltip.")]
        public string ZoneOneLabelWithTooltipHoverStyle
        {
            get
            {
                return this.GetCssClassOverride(PatientBannerCssClasses.ZoneOneLabelWithTooltipHover);
            }

            set
            {
                this.SetCssClassOverride(PatientBannerCssClasses.ZoneOneLabelWithTooltipHover, value);
            }
        }
        #endregion

        #region Template Properties
        /// <summary>
        /// Gets or sets the ITemplate used to extend functionality into Subsection Three. 
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(PatientBanner))]
        [Category("Appearance")]
        [Description("Gets or sets the ITemplate used to extend the functionality into Subsection Three on the PatientBanner expandable panel.")]
        public ITemplate SubsectionThreeTemplate
        {
            get
            {
                return this.subsectionThreeTemplate;
            }

            set
            {
                this.subsectionThreeTemplate = value;
            }
        }

        /// <summary>
        /// Gets or sets the ITemplate used to extend functionality into Subsection Four. 
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateContainer(typeof(PatientBanner))]
        [Category("Appearance")]
        [Description("Gets or sets the ITemplate used to extend the functionality into Subsection Four on the PatientBanner expandable panel.")]
        public ITemplate SubsectionFourTemplate
        {
            get
            {
                return this.subsectionFourTemplate;
            }

            set
            {
                this.subsectionFourTemplate = value;
            }
        }

        #endregion

        #region Tooltips       

        /// <summary>
        /// Gets or sets the tooltip for Zone Two. 
        /// </summary>
        [Category("Behavior")]
        [Description("Gets or sets the value to be displayed in the tooltip for Zone Two")]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "ZoneTwoToolTipText")]
        public string ZoneTwoTooltip
        {
            get
            {
                return this.GetPropertyValue<string>("ZoneTwoTooltip", PatientBannerControl.Resources.ZoneTwoToolTipText);
            }

            set
            {
                this.SetPropertyValue<string>("ZoneTwoTooltip", value);
            }
        }

        /// <summary>
        /// Gets or sets the tooltip for gender label.
        /// </summary>
        [Category("Behavior")]
        [Description("Gets or sets the value to be displayed in the tooltip for gender label")]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "GenderLabelTooltip")]
        public string GenderLabelTooltip
        {
            get
            {
                return this.GetLocalizablePropertyValue("GenderLabelTooltip");
            }

            set
            {
                this.SetPropertyValue<string>("GenderLabelTooltip", value);
            }
        }

        /// <summary>
        /// Gets or sets the tooltip for the patient's gender.
        /// </summary>
        [Category("Behavior")]
        [Description("Gets or sets the value to be displayed in the tooltip for patient gender")]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "GenderValueTooltip")]
        public string GenderValueTooltip
        {
            get
            {
                return this.GetLocalizablePropertyValue("GenderValueTooltip");
            }

            set
            {
                this.SetPropertyValue<string>("GenderValueTooltip", value);
            }
        }

        /// <summary>
        /// Gets or sets the tooltip for Identifier label.
        /// </summary>
        [Category("Behavior")]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "IdentifierLabelTooltip")]
        [Description("Gets or sets the value to be displayed in the tooltip for Identifier label")]
        public string IdentifierLabelTooltip
        {
            get
            {
                return this.GetLocalizablePropertyValue("IdentifierLabelTooltip");
            }

            set
            {
                this.SetPropertyValue<string>("IdentifierLabelTooltip", value);
            }
        }

        /// <summary>
        /// Gets or sets the tooltip for Identifier.
        /// </summary>
        [Category("Behavior")]
        [Description("Gets or sets the value to be displayed in the tooltip for Identifier")]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "IdentifierTooltip")]
        public string IdentifierTooltip
        {
            get
            {
                return this.GetLocalizablePropertyValue("IdentifierTooltip");
            }

            set
            {
                this.SetPropertyValue<string>("IdentifierTooltip", value);
            }
        }

        #endregion

        #region Baseclass Overrides
        /// <summary>
        /// Gets the font.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new FontInfo Font
        {
            get
            {
                return base.Font;
            }
        }

        /// <summary>
        /// Gets the foreground color.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
        }

        /// <summary>
        /// Gets the tooltip.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string ToolTip
        {
            get
            {
                return base.ToolTip;
            }
        }

        /// <summary>
        /// Gets the back color.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Color BackColor
        {
            get
            {
                return base.BackColor;
            }
        }

        /// <summary>
        /// Gets the CSS Class.
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string CssClass
        {
            get
            {
                return base.CssClass;
            }
        }        
        #endregion

        #region Internal Properties
        /// <summary>
        /// true if using default stylesheet
        /// </summary>
        internal bool IsUsingDefaultStyleSheet
        {
            get
            {
                return (this.CssClass.Length == 0 || 
                    this.CssClass.IndexOf(PatientBannerCssClasses.Main, StringComparison.OrdinalIgnoreCase) >= 0);
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
                return HtmlTextWriterTag.Div;
            }
        }
        #endregion

        #region Private Properties

        /// <summary>
        /// True if the patient is deceased.
        /// </summary>
        private bool IsDeceased
        {
            get
            {
                return (this.DateOfDeath != DateTime.MinValue);
            }
        }

        /// <summary>
        /// Control to hold Zone One controls.
        /// </summary>
        private PatientBannerZoneOneContainer ZoneOneContainer
        {
            get
            {
                this.EnsureChildControls();
                return this.zoneOneContainer;
            }
        }

        /// <summary>
        /// Control to hold Zone Two controls
        /// </summary>
        private PatientBannerZoneTwoContainer ZoneTwoContainer
        {
            get
            {
                this.EnsureChildControls();
                return this.zoneTwoContainer;
            }
        }

        /// <summary>
        /// Gets or sets the expanded state of Zone Two. Available on the client-side version of the Web control. 
        /// </summary> 
        /// <remarks>
        ///  Available on the client-side.
        /// </remarks>
        private bool ClientZoneTwoExpanded
        {
            get
            {
                this.EnsureChildControls();
                return (string.CompareOrdinal(this.clientState.Value, "1") == 0);
            }

            set
            {
                this.EnsureChildControls();
                this.clientState.Value = (value ? "1" : string.Empty);
            }
        }

        /// <summary>
        /// true if css class overrides have been set
        /// </summary>
        private bool HasCssOverrides
        {
            get
            {
                return (this.ViewState["CssClassOverrides"] != null);
            }
        }

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

        #region IPostBackEventHandler Members

        /// <summary>
        /// When implemented by a class, enables a server control to process an event.
        /// raised when a form is posted to the server. 
        /// </summary>
        /// <param name="eventArgument"> String that represents an optional event argument to be passed to the event handler. </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "Required to be private")]
        void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
        {
            this.Page.ClientScript.ValidateEvent(this.UniqueID, eventArgument);
                        
            if (string.Compare(eventArgument, "identifier", true, CultureInfo.CurrentCulture) == 0)
            {
                this.OnIdentifierClick(new PatientBannerEventArgs(this.Identifier, this.Gender));
            }
            else if (string.Compare(eventArgument, "gender", true, CultureInfo.CurrentCulture) == 0)
            {
                this.OnGenderValueClick(new PatientBannerEventArgs(this.Identifier, this.Gender));
            }
        }

        #endregion

        #region IScriptControl Members
        /// <summary>
        /// Gets a collection of objects derived from the abstract ScriptDescriptor class. 
        /// </summary>
        /// <returns>An IEnumerable collection of ScriptDescriptor objects.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "Required to be private")]
        IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);

            descriptor.AddProperty("dropDownImage", this.ResolveDefaultedUrl(this.DropDownImage, PatientBannerWebResources.DefaultExpandImage));
            descriptor.AddProperty("collapseImage", this.ResolveDefaultedUrl(this.CollapseImage, PatientBannerWebResources.DefaultCollapseImage));

            string zoneTwoHoverStyle = (this.ZoneTwoHoverStyle.Length == 0 && this.IsUsingDefaultStyleSheet ? PatientBannerCssClasses.ZoneTwoHover : this.ZoneTwoHoverStyle);
            string zoneOneLabelWithTooltipHoverStyle = (this.ZoneOneLabelWithTooltipHoverStyle.Length == 0 && this.IsUsingDefaultStyleSheet ? PatientBannerCssClasses.ZoneOneLabelWithTooltipHover : this.ZoneOneLabelWithTooltipHoverStyle);
            string zoneOneDataWithTooltipHoverStyle = (this.ZoneOneDataWithTooltipHoverStyle.Length == 0 && this.IsUsingDefaultStyleSheet ? PatientBannerCssClasses.ZoneOneDataWithTooltipHover : this.ZoneOneDataWithTooltipHoverStyle);

            descriptor.AddProperty("zoneTwoHoverStyle", zoneTwoHoverStyle);
            descriptor.AddProperty("zoneOneLabelWithTooltipHoverStyle", PatientBannerCssClasses.ZoneOneLabelWithTooltipHover);
            descriptor.AddProperty("zoneOneDataWithTooltipHoverStyle", PatientBannerCssClasses.ZoneOneDataWithTooltipHover);

            yield return descriptor;
        }

        /// <summary>
        /// Gets the collection of ScriptReference objects for the control. 
        /// </summary>
        /// <returns>An IEnumerable collection of ScriptReference objects.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Justification = "Required to be private")]
        IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
        {
            string assemblyName = typeof(PatientBannerWebResources).Assembly.FullName;

            yield return new ScriptReference(PatientBannerWebResources.Javascript, assemblyName);
            yield return new ScriptReference(PatientBannerWebResources.CommonJavascript, assemblyName);            

            // for ie pre version 7 we need to apply a fix otherwise if the
            // Zone Two area drops down over a select element it might show through
            if (ZIndexFixExtender.IsRequired(this.Page.Request))
            {
                yield return new ScriptReference(ZIndexFixExtender.Javascript, assemblyName);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls 
        /// that use composition-based implementation to create any child controls 
        /// they contain in preparation for posting back or rendering. 
        /// </summary>
        protected override void CreateChildControls()
        {
            ITemplate zoneOneTemplate = new PatientBannerZoneOneTemplate();
            ITemplate zoneTwoTemplate = new PatientBannerZoneTwoTemplate();

            this.Controls.Clear();

            // create container for Zone One section
            this.zoneOneContainer = new PatientBannerZoneOneContainer(this);
            this.InitializeTemplateContainer(this.zoneOneContainer, zoneOneTemplate, PatientBannerControlIds.ZoneOne, false, true);

            // create container for Zone Two section
            this.zoneTwoContainer = new PatientBannerZoneTwoContainer(this);
            this.InitializeTemplateContainer(this.zoneTwoContainer, zoneTwoTemplate, PatientBannerControlIds.ZoneTwo, false, true);

            // instantiate user defined templates
            if (this.subsectionThreeTemplate != null)
            {
                this.subsectionThreeTemplate.InstantiateIn(this.zoneTwoContainer.SubsectionThree);
            }

            if (this.subsectionFourTemplate != null)
            {
                this.subsectionFourTemplate.InstantiateIn(this.zoneTwoContainer.SubsectionFour);
            }

            this.clientState = new HiddenField();
            this.clientState.ID = PatientBannerControlIds.ClientState;
            this.clientState.EnableViewState = false;
            this.Controls.Add(this.clientState);

            this.zoneTwoContainer.ViewAllContactDetails.Click += new EventHandler(this.ViewAllContactDetails_Click);
            this.zoneTwoContainer.ViewAllergyRecord.Click += new EventHandler(this.ViewAllergyRecord_Click);
            this.zoneTwoContainer.ViewAllAddresses.Click += new EventHandler(this.ViewAllAddresses_Click);
        }

        /// <summary>
        /// Raises the PreRender event. 
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data. </param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            // register with script manager
            if (!this.DesignMode)
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
                if (scriptManager != null)
                {
                    scriptManager.RegisterScriptControl<PatientBanner>(this);
                }
            }

            if (this.patientAllergies.Count > 0)
            {
                this.SetPropertyValue<AllergyCollection>("Allergies", this.patientAllergies);
            }

            this.IncludeDefaultStyleSheet();
        }

        /// <summary>
        /// Renders the control to the specified HTML writer. 
        /// </summary>
        /// <param name="writer">The HtmlTextWriter object that receives the control content. </param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.DesignMode)
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(Page);
                if (scriptManager != null)
                {
                    scriptManager.RegisterScriptDescriptors(this);
                }
            }

            base.Render(writer);
        }

        /// <summary>
        /// Adds HTML attributes and styles that need to be rendered to the specified HtmlTextWriterTag.
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output stream to render HTML content on the client. Remarks</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            // we transfered access key to Zone One, so don't output on main control
            string accessKey = this.AccessKey;
            this.AccessKey = string.Empty;

            // Remove border width so as to remove border style property when border style is notset PS#6567
            Unit borderWidth = this.BorderWidth;
            bool renderBorderWidth = false;
            if (!borderWidth.IsEmpty && this.BorderStyle == BorderStyle.NotSet)
            {
                this.BorderWidth = Unit.Empty;
                renderBorderWidth = true;
            }

            base.AddAttributesToRender(writer);

            this.AccessKey = accessKey;

            if (renderBorderWidth)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, borderWidth.ToString(CultureInfo.CurrentCulture));
                this.BorderWidth = borderWidth;
            }
            
            if (this.IsUsingDefaultStyleSheet)
            {
                if (this.IsDeceased)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, PatientBannerCssClasses.JoinCssClasses(PatientBannerCssClasses.Main, PatientBannerCssClasses.MainDeceased));
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, PatientBannerCssClasses.Main);
                }
            }
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output stream to render HTML content on the client. </param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            this.SetChildControlProperties();
            base.RenderContents(writer);            
        }

        /// <summary>
        /// Raise property changed event
        /// </summary>
        /// <param name="e">event args</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }              

        /// <summary>
        /// Raise View all addresses click event
        /// </summary>
        /// <param name="e">Patient banner event args</param>
        protected virtual void OnViewAllAddressesClick(PatientBannerEventArgs e)
        {
            if (this.ViewAllAddressesClick != null)
            {
                this.ViewAllAddressesClick(this, e);
            }
        }

        /// <summary>
        /// Raise View all phone numbers click event
        /// </summary>
        /// <param name="e">Patient banner event args</param>
        protected virtual void OnViewAllContactDetailsClick(PatientBannerEventArgs e)
        {
            if (this.ViewAllContactDetailsClick != null)
            {
                this.ViewAllContactDetailsClick(this, e);
            }
        }

        /// <summary>
        /// Raise View allergy record click event
        /// </summary>
        /// <param name="e">Patient banner event args</param>
        protected virtual void OnViewAllergyRecordClick(PatientBannerEventArgs e)
        {
            if (this.ViewAllergyRecordClick != null)
            {
                this.ViewAllergyRecordClick(this, e);
            }
        }

        /// <summary>
        /// Raise gender value click event
        /// </summary>
        /// <param name="e">Patient banner event args</param>
        protected virtual void OnGenderValueClick(PatientBannerEventArgs e)
        {
            if (this.GenderValueClick != null)
            {
                this.GenderValueClick(this, e);
            }
        }

        /// <summary>
        /// Raise identifier click event
        /// </summary>
        /// <param name="e">Patient banner event args</param>
        protected virtual void OnIdentifierClick(PatientBannerEventArgs e)
        {
            if (this.IdentifierClick != null)
            {
                this.IdentifierClick(this, e);
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Get display text for given allergy information
        /// </summary>
        /// <param name="allergyInformation">Allergy information</param>
        /// <returns>display text for allergy information</returns>
        private static string GetAllergyInformation(AllergyInformation allergyInformation)
        {
            switch (allergyInformation)
            {
                case AllergyInformation.NoneKnown:
                    return PatientBannerControl.Resources.NoneKnown;
                case AllergyInformation.NotRecorded:
                    return PatientBannerControl.Resources.NotRecorded;
                case AllergyInformation.Present:
                    return PatientBannerControl.Resources.Present;
                case AllergyInformation.Unavailable:
                    return PatientBannerControl.Resources.AllergiesUnavailable;
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Gets the preferred name to be shown in patient banner
        /// </summary>
        /// <returns>Preferred name</returns>
        private string GetPreferredName()
        {
            string preferredName = this.GetPropertyValue<string>("PreferredName", string.Empty);

            if (!string.IsNullOrEmpty(preferredName))
            {
                if (preferredName.Trim().Length > PreferredNameMaxChars)
                {
                    preferredName = preferredName.Trim().Substring(0, PreferredNameMaxChars - PatientName.Ellipsis.Length) + PatientName.Ellipsis;
                }
            }

            return preferredName;
        }
                
        /// <summary>
        /// initialize template container and its contained controls
        /// </summary>
        /// <param name="container">container to initialize</param>
        /// <param name="template">template to instantiate in the container</param>
        /// <param name="controlId">id to give container</param>
        /// <param name="enableViewState">if true enable viewstate for container</param>
        /// <param name="visible">if true container initially visible</param>
        private void InitializeTemplateContainer(PatientBannerContainerBase container, ITemplate template, string controlId, bool enableViewState, bool visible)
        {
            container.ID = controlId;
            container.EnableViewState = enableViewState;
            template.InstantiateIn(container);
            container.Visible = visible;

            this.Controls.Add(container);
        }

        /// <summary>
        /// typesafe get control property value
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="propertyName">property name</param>
        /// <param name="defaultValue">value to return if property not set</param>
        /// <returns>property value</returns>
        private T GetPropertyValue<T>(string propertyName, T defaultValue)
        {
            object value = this.ViewState[propertyName];

            if (value == null)
            {
                return defaultValue;
            }

            return (T)value;
        }

        /// <summary>
        /// Typesafe set control property value
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="propertyName">property key</param>
        /// <param name="value">property value</param>
        private void SetPropertyValue<T>(string propertyName, T value)
        {
            object originalValue = this.ViewState[propertyName];

            // if the value has changed raise property changed event
            if (value == null || originalValue == null || !value.Equals(originalValue))
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);

                if (value == null)
                {
                    this.ViewState[propertyName] = string.Empty;
                }
                else
                {
                    this.ViewState[propertyName] = value;
                }

                this.OnPropertyChanged(e);
            }
        }

        /// <summary>
        /// get localizable control string property value, this method expects there
        /// will be a string resource of the same name as the property
        /// </summary>
        /// <param name="propertyName">property name</param>
        /// <returns>property value</returns>
        private string GetLocalizablePropertyValue(string propertyName)
        {
            string value = (string)this.ViewState[propertyName];

            if (value == null)
            {
                CultureInfo culture = PatientBannerControl.Resources.Culture;
                ResourceManager resourceManager = PatientBannerControl.Resources.ResourceManager;

                value = resourceManager.GetString(propertyName, culture);
            }

            return value;
        }

        /// <summary>
        /// set child control properties
        /// </summary>
        private void SetChildControlProperties()
        {
            PatientBannerZoneOneContainer zoneOneContainer = this.ZoneOneContainer;
            PatientBannerZoneTwoContainer zoneTwoContainer = this.ZoneTwoContainer;
            zoneTwoContainer.AddressControl.RenderWrappableStrings = true;
            zoneTwoContainer.ContactDetailsControl.RenderWrappableStrings = true;
            zoneTwoContainer.ContactDetailsControl.WrapLabels = this.WrapSubsectionTwoLabels;
                        
            zoneTwoContainer.ContactDetailsControl.LabelsColumnWidth = this.SubsectionTwoLabelWidth;
                     
            if (this.IdentifierClick != null)
            {
                zoneOneContainer.IdentifierControl.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(this, "identifier", true);
            }

            if (this.GenderValueClick != null)
            {
                zoneOneContainer.GenderControl.Attributes["onclick"] = this.Page.ClientScript.GetPostBackEventReference(this, "gender", true);
            }

            // access key goes on the expand button
            zoneOneContainer.AccessKey = this.AccessKey;

            // tool tips            
            zoneTwoContainer.PermanentControl.ToolTip = this.ZoneTwoTooltip;

            System.Web.UI.WebControls.Image patientImage = zoneOneContainer.PatientImage;
            patientImage.Visible = this.ImageDisplayed;
            if (patientImage.Visible)
            {
                // if no image supplied use default image
                patientImage.ImageUrl =
                    this.ResolveDefaultedUrl(this.PatientImage, PatientBannerWebResources.DefaultPatientImage);
                patientImage.AlternateText = string.Format(
                                                CultureInfo.CurrentCulture, 
                                                PatientBannerControl.Resources.PatientImageAltTextFormat,
                                                this.NameDisplayValue);
            }

            NameLabel nameControl = zoneOneContainer.NameControl;
            nameControl.FamilyName = this.FamilyName;
            nameControl.GivenName = this.GivenName;
            nameControl.Title = this.Title;

            System.Web.UI.WebControls.Image deceasedPatientTransparentIcon = zoneOneContainer.DeceasedPatientTransparentIcon;
            deceasedPatientTransparentIcon.Visible = this.IsDeceased;
            deceasedPatientTransparentIcon.ImageUrl = this.ResolveDefaultedUrl(string.Empty, PatientBannerWebResources.DeceasedPatientTransparentIcon);
            deceasedPatientTransparentIcon.AlternateText = "This patient is deceased";
            deceasedPatientTransparentIcon.ToolTip = "This patient is deceased";

            if (!string.IsNullOrEmpty(this.PreferredName.Trim()))
            {
                zoneOneContainer.PreferredNameControl.Text = this.GetPreferredName();
                zoneOneContainer.PreferredNameLabel.Text = this.PreferredNameLabelText;
            }
            else
            {
                ((Control)zoneOneContainer.PreferredNameLabel).Visible = false;
                zoneOneContainer.PreferredNameSeparator.Visible = false;
                zoneOneContainer.PreferredNameControl.Visible = false;
            }

            DateLabel dateOfBirthLabel = zoneOneContainer.DateOfBirthControl;
            dateOfBirthLabel.NullStrings = new string[] { PatientBannerControl.Resources.Unknown };
            dateOfBirthLabel.NullIndex = 0;
            string age = string.Empty;

            if (this.DateOfBirth != DateTime.MinValue)
            {                
                dateOfBirthLabel.DateType = DateType.Exact;
                dateOfBirthLabel.DateValue = this.DateOfBirth;

                DateTime now = DateTime.Now;
                                
                TimeSpanLabel ageControl = zoneOneContainer.AgeAtDeathControl;
                ageControl.IsAge = true;
                ageControl.Threshold = TimeSpanUnit.Days;
                ageControl.From = this.DateOfBirth;
                ageControl.To = (this.IsDeceased ? this.DateOfDeath : now);
                ageControl.UnitLength = TimeSpanUnitLength.Short;
                age = ageControl.Value.ToString(TimeSpanUnitLength.Short, CultureInfo.CurrentCulture);            
            }
            else
            {
                dateOfBirthLabel.DateType = DateType.NullIndex;
            }

            DateLabel dateOfDeathLabel = zoneOneContainer.DateOfDeathControl;
            if (this.IsDeceased)
            {
                dateOfDeathLabel.DateType = DateType.Exact;
                dateOfDeathLabel.DateValue = this.DateOfDeath;
            }
            else
            {
                if (this.DateOfBirth != DateTime.MinValue)
                {
                    zoneOneContainer.Age.Text = string.Format(CultureInfo.CurrentCulture, PatientBannerControl.Resources.AgeFormat, age);
                }

                zoneOneContainer.BornDiedSeparator.Visible = false;
                dateOfDeathLabel.Visible = false;
                zoneOneContainer.AgeAtDeathControl.Visible = false;
                zoneOneContainer.AgeAtDeathLabel.Visible = false;
            }
            
            zoneOneContainer.GenderControl.Value = this.Gender;

            // Set tooltip only if it is not null or empty so as to show parents tooltip
            if (!string.IsNullOrEmpty(this.GenderValueTooltip))
            {
                zoneOneContainer.GenderControl.ToolTip = this.GenderValueTooltip;
            }

            // Set tooltip only if it is not null or empty so as to show parents tooltip
            if (!string.IsNullOrEmpty(this.GenderLabelTooltip) && !string.IsNullOrEmpty(this.GenderLabelText))
            {
                zoneOneContainer.GenderLabel.ToolTip = this.GenderLabelTooltip;
            }            

            IdentifierLabel identifierControl = zoneOneContainer.IdentifierControl;
            if (this.LastIdentifierValid && this.Identifier.Length > 0)
            {
                identifierControl.IdentifierType = this.IdentifierType;
                identifierControl.Text = this.Identifier;

                // Set tooltip only if it is not null or empty so as to show parents tooltip
                if (!string.IsNullOrEmpty(this.IdentifierTooltip))
                {
                    identifierControl.ToolTip = this.IdentifierTooltip;
                }

                // Set tooltip only if it is not null or empty so as to show parents tooltip
                if (!string.IsNullOrEmpty(this.IdentifierLabelTooltip) && !string.IsNullOrEmpty(this.IdentifierLabelText))
                {
                    zoneOneContainer.IdentifierLabel.ToolTip = this.IdentifierLabelTooltip;
                }
            }

            AddressLabel addressControl = zoneTwoContainer.AddressControl;
            addressControl.AddressTypeStyle = string.IsNullOrEmpty(this.ZoneTwoLabelStyle) ? PatientBannerCssClasses.ZoneTwoLabel : this.ZoneTwoLabelStyle;
            addressControl.Address1 = this.Address1;
            addressControl.Address2 = this.Address2;
            addressControl.Address3 = this.Address3;
            addressControl.Town = this.Town;
            addressControl.County = this.County;
            addressControl.Postcode = this.Postcode;
            addressControl.Country = this.Country;

            zoneTwoContainer.AddressSummaryControl.Text = addressControl.GetSummary();

            ContactLabel contactNumbersControl = zoneTwoContainer.ContactDetailsControl;
            contactNumbersControl.HomePhoneNumber = this.HomePhoneNumber;
            contactNumbersControl.WorkPhoneNumber = this.WorkPhoneNumber;
            contactNumbersControl.MobilePhoneNumber = this.MobilePhoneNumber;
            contactNumbersControl.EmailAddress = this.EmailAddress;
            zoneTwoContainer.ContactDetailsSummaryControl.Text = contactNumbersControl.GetContactDetailsSummary();

            zoneTwoContainer.AllergySummaryControl.Text = PatientBanner.GetAllergyInformation(this.AllergyInformation);
            if (this.AllergyInformation == AllergyInformation.Unavailable)
            {
                zoneTwoContainer.AllergyIcon.ImageUrl = this.ResolveDefaultedUrl(this.AllergiesUnavailableIcon, PatientBannerWebResources.AllergiesUnavailableImage);
                zoneTwoContainer.ViewAllergyRecord.Visible = false;                
            }
            else if (this.AllergyInformation == AllergyInformation.NotRecorded || this.AllergyInformation == AllergyInformation.NoneKnown)
            {
                zoneTwoContainer.AllergyIcon.ImageUrl = this.ResolveDefaultedUrl(this.AllergiesNotPresentIcon, PatientBannerWebResources.AllergiesNotPresentImage);
            }
            else if (this.AllergyInformation == AllergyInformation.Present)
            {
                zoneTwoContainer.AllergyIcon.ImageUrl = this.ResolveDefaultedUrl(this.AllergiesPresentIcon, PatientBannerWebResources.AllergiesPresentImage);
            }

            zoneTwoContainer.AllergyIcon.ToolTip = zoneTwoContainer.AllergySummaryControl.Text;

            // Set Allergy summary in label style when allergies are not recorded or unavailable refer to PS#5778
            if (this.AllergyInformation == AllergyInformation.NotRecorded || this.AllergyInformation == AllergyInformation.Unavailable)
            {
                ((WebControl)zoneTwoContainer.AllergySummaryControl).CssClass = PatientBannerCssClasses.ZoneTwoLabel;
            }

            if (this.Allergies != null && this.Allergies.Count > 0 && this.AllergyInformation == AllergyInformation.Present)
            {
                WebControl allergyDetails = zoneTwoContainer.AllergyDetails;
                allergyDetails.CssClass = PatientBannerCssClasses.ZoneTwoData;  
                Panel allergyDetailsPanel = new Panel();
                allergyDetailsPanel.ID = PatientBannerControlIds.AllergyDetailsPanel;
                allergyDetailsPanel.Style.Add(HtmlTextWriterStyle.Overflow, "hidden");

                AllergiesLabelControl allergyDetailsControl = new AllergiesLabelControl(this.Allergies);

                allergyDetailsPanel.Controls.Add(allergyDetailsControl);
                allergyDetails.Controls.Add(allergyDetailsPanel);
            }

            // set visual state depending on whether the expandable panel is shown
            // by default
            System.Web.UI.WebControls.Image expandImage = zoneTwoContainer.ExpandImage;
            expandImage.AlternateText = this.ZoneTwoTooltip;

            this.ClientZoneTwoExpanded = this.ZoneTwoExpanded;
            if (this.ZoneTwoExpanded)
            {
                expandImage.ImageUrl = this.ResolveDefaultedUrl(this.CollapseImage, PatientBannerWebResources.DefaultCollapseImage);
            }
            else
            {
                expandImage.ImageUrl = this.ResolveDefaultedUrl(this.DropDownImage, PatientBannerWebResources.DefaultExpandImage);
            }            

            if (!this.DesignMode)
            {
                // PS#6572. Flickering effect in firefox.
                if (this.ZoneTwoExpanded)
                {
                    zoneTwoContainer.NonPermanentControl.Style[HtmlTextWriterStyle.Display] = "";
                    zoneTwoContainer.ZoneTwoNonPermanentLinksRow.Style[HtmlTextWriterStyle.Display] = "";
                }
                else
                {
                    zoneTwoContainer.NonPermanentControl.Style[HtmlTextWriterStyle.Display] = "none";
                    zoneTwoContainer.ZoneTwoNonPermanentLinksRow.Style[HtmlTextWriterStyle.Display] = "none";
                }
            }
            else
            {
                zoneTwoContainer.NonPermanentControl.Visible = this.ZoneTwoExpanded;
                zoneTwoContainer.ZoneTwoNonPermanentLinksRow.Visible = this.ZoneTwoExpanded;                
            }
                       
            // Set subsection widths
            ((WebControl)zoneTwoContainer.SubsectionOneTitle).Width = this.SubsectionOneWidth;
            ((WebControl)zoneTwoContainer.SubsectionTwoTitle).Width = this.SubsectionTwoWidth;
            ((WebControl)zoneTwoContainer.SubsectionThreeTitle).Width = this.SubsectionThreeWidth;
            ((WebControl)zoneTwoContainer.SubsectionFourTitle).Width = this.SubsectionFourWidth;
            ((WebControl)zoneTwoContainer.SubsectionFive).Width = this.SubsectionFiveWidth;
                      
            // apply additional style to Zone One depending on whether the patient is alive
            // or dead
            string additionalZone1Class = (this.IsDeceased ? PatientBannerCssClasses.DeadPatient : PatientBannerCssClasses.ActivePatient);
            this.ZoneOneContainer.CssClass = PatientBannerCssClasses.JoinCssClasses(this.ZoneOneContainer.CssClass, additionalZone1Class);

            this.SetLocalizableChildProperties();
            this.ApplyCssClassOverrides();
        }        

        /// <summary>
        /// set localizable child properties
        /// </summary>
        private void SetLocalizableChildProperties()
        {
            PatientBannerZoneOneContainer zoneOneContainer = this.ZoneOneContainer;
            PatientBannerZoneTwoContainer zoneTwoContainer = this.ZoneTwoContainer;
            ContactLabel contactDetails = zoneTwoContainer.ContactDetailsControl;

            zoneOneContainer.IdentifierLabel.Text = StringUtil.GetAntiXssWrappableString(this.IdentifierLabelText);
            zoneOneContainer.GenderLabel.Text = StringUtil.GetAntiXssWrappableString(this.GenderLabelText);
            zoneOneContainer.DateOfBirthLabel.Text = StringUtil.GetAntiXssWrappableString(this.DateOfBirthLabelText);
            zoneOneContainer.DateOfDeathLabel.Text = StringUtil.GetAntiXssWrappableString(this.DateOfDeathLabelText);
            ((Control)zoneOneContainer.DateOfDeathLabel).Visible = this.IsDeceased;
            zoneOneContainer.PreferredNameLabel.Text = this.PreferredNameLabelText;
            zoneOneContainer.AgeAtDeathLabel.Text = StringUtil.GetAntiXssWrappableString(this.AgeAtDeathLabelText);

            zoneTwoContainer.AddressControl.AddressType = this.AddressTypeLabelText;

            zoneTwoContainer.SubsectionTwoTitle.Text = this.SubsectionTwoTitle;
            zoneTwoContainer.SubsectionOneTitle.Text = this.SubsectionOneTitle;
            zoneTwoContainer.SubsectionThreeTitle.Text = this.SubsectionThreeTitle;
            zoneTwoContainer.SubsectionFourTitle.Text = this.SubsectionFourTitle;            

            contactDetails.HomePhoneLabelText = this.HomePhoneLabelText;
            contactDetails.WorkPhoneLabelText = this.WorkPhoneLabelText;
            contactDetails.MobilePhoneLabelText = this.MobilePhoneLabelText;
            contactDetails.EmailLabelText = this.EmailLabelText;

            zoneTwoContainer.ViewAllAddresses.Text = StringUtil.GetAntiXssWrappableString(this.ViewAllAddressLinkText);
            zoneTwoContainer.ViewAllergyRecord.Text = StringUtil.GetAntiXssWrappableString(this.ViewAllergyRecordLinkText);
            zoneTwoContainer.ViewAllContactDetails.Text = StringUtil.GetAntiXssWrappableString(this.ViewAllContactDetailsLinkText);
        }

        /// <summary>
        /// Resolve supplied url (i.e. replace ~ character), if the supplied url is empty
        /// return default resource url
        /// </summary>
        /// <param name="url">url to resolve</param>
        /// <param name="defaultResourceName">default resource name</param>
        /// <returns>The resolved url.</returns>
        private string ResolveDefaultedUrl(string url, string defaultResourceName)
        {
            if (!string.IsNullOrEmpty(url))
            {
                return this.ResolveClientUrl(url);
            }

            return this.Page.ClientScript.GetWebResourceUrl(typeof(PatientBanner), defaultResourceName);
        }

        /// <summary>
        /// Include our default style sheet unless an other style is being used
        /// </summary>
        private void IncludeDefaultStyleSheet()
        {
            if (this.IsUsingDefaultStyleSheet && this.Page.Header.FindControl(PatientBannerControlIds.StyleSheetLink) == null)
            {
                HtmlGenericControl cssLink = new HtmlGenericControl("link");
                cssLink.ID = PatientBannerControlIds.StyleSheetLink;
                cssLink.Attributes["rel"] = "stylesheet";
                cssLink.Attributes["type"] = "text/css";
                cssLink.Attributes["href"] = this.Page.ClientScript.GetWebResourceUrl(typeof(PatientBannerWebResources), PatientBannerWebResources.StyleSheet);
                this.Page.Header.Controls.Add(cssLink);
            }
        }

        /// <summary>
        /// Get override css class
        /// </summary>
        /// <param name="defaultCssClass">name of the default css class</param>
        /// <returns>overriden value, or empty string if none set</returns>
        private string GetCssClassOverride(string defaultCssClass)
        {
            string cssClassOverride = null;
            Hashtable cssClassOverrides = (Hashtable)this.ViewState["CssClassOverrides"];
            if (cssClassOverrides != null)
            {
                cssClassOverride = (string)cssClassOverrides[defaultCssClass];
            }

            return cssClassOverride ?? string.Empty;
        }

        /// <summary>
        /// Set css class overide
        /// </summary>
        /// <param name="defaultCssClass">default class to override</param>
        /// <param name="value">override value</param>
        private void SetCssClassOverride(string defaultCssClass, string value)
        {
            Hashtable cssClassOverrides = (Hashtable)this.ViewState["CssClassOverrides"];
            if (cssClassOverrides == null)
            {
                cssClassOverrides = new Hashtable();
                this.ViewState["CssClassOverrides"] = cssClassOverrides;
            }

            cssClassOverrides[defaultCssClass] = value;
        }

        /// <summary>
        /// Apply css class overrides
        /// </summary>
        private void ApplyCssClassOverrides()
        {
            if (this.HasCssOverrides || !this.IsUsingDefaultStyleSheet)
            {
                this.ApplyCssClassOverridesRecursive(this);
            }
        }

        /// <summary>
        /// Apply css class overrides recursively
        /// </summary>
        /// <param name="parentControl">parent control from which to start
        /// applyinig overrides</param>
        private void ApplyCssClassOverridesRecursive(Control parentControl)
        {
            bool removeDefaultClasses = !this.IsUsingDefaultStyleSheet;

            foreach (Control control in parentControl.Controls)
            {
                WebControl webControl = control as WebControl;

                if (webControl != null && webControl.CssClass.Length > 0)
                {
                    string[] cssClasses = PatientBannerCssClasses.SplitCssClasses(webControl.CssClass);
                    bool updateCssClass = false;
                    for (int j = 0; j < cssClasses.Length; j++)
                    {
                        string cssClassOverride = this.GetCssClassOverride(cssClasses[j]);
                        if (cssClassOverride.Length > 0)
                        {
                            cssClasses[j] = cssClassOverride;
                            updateCssClass = true;
                        }
                        else if (removeDefaultClasses)
                        {
                            cssClasses[j] = string.Empty;
                            updateCssClass = true;
                        }
                    }

                    if (updateCssClass)
                    {
                        webControl.CssClass = PatientBannerCssClasses.JoinCssClasses(cssClasses);
                    }
                }

                this.ApplyCssClassOverridesRecursive(control);
            }
        }
   
        /// <summary>
        /// Event Handler for ViewAllAddress click event
        /// </summary>
        /// <param name="sender">ViewAllAddresses link button</param>
        /// <param name="e">Event Args</param>
        private void ViewAllAddresses_Click(Object sender, EventArgs e)
        {
            this.OnViewAllAddressesClick(new PatientBannerEventArgs(this.Identifier, this.Gender));
        }

        /// <summary>
        /// Event Handler for ViewAllergyRecord click event
        /// </summary>
        /// <param name="sender">ViewAllergyRecord link button</param>
        /// <param name="e">Event Args</param>
        private void ViewAllergyRecord_Click(Object sender, EventArgs e)
        {
            this.OnViewAllergyRecordClick(new PatientBannerEventArgs(this.Identifier, this.Gender));
        }

        /// <summary>
        /// Event handler for ViewAllContact details click event
        /// </summary>
        /// <param name="sender">ViewAllContactDetails link button</param>
        /// <param name="e">Event Args</param>
        private void ViewAllContactDetails_Click(Object sender, EventArgs e)
        {
            this.OnViewAllContactDetailsClick(new PatientBannerEventArgs(this.Identifier, this.Gender));
        }       
        #endregion

        #region Explict Property Resets
        /// <summary>
        /// custom reset method for the identifier property
        /// </summary>
        private void ResetIdentifier()
        {
            this.IdentifierType = IdentifierType.Other;
            this.Identifier = string.Empty;
        }
        #endregion

        #region Allergy Details Control

        /// <summary>
        /// Private class for Allergies Label control
        /// </summary>
        /// <remarks>
        /// Class is marked private because this control is not available outside patient banner control
        /// </remarks>
        private class AllergiesLabelControl : HtmlContainerControl
        {
            /// <summary>
            /// Maximum allergies that can be shown
            /// </summary>
            private const int MaxAllergies = 5;

            /// <summary>
            /// patient allergies
            /// </summary>
            private AllergyCollection patientAllergies;

            /// <summary>
            /// Instantiates a Allergy label control with the patient allergies
            /// </summary>
            /// <param name="patientAllergies">Patient allergies</param>
            public AllergiesLabelControl(AllergyCollection patientAllergies)
            {
                this.patientAllergies = patientAllergies;
            }

            /// <summary>
            /// Render the content of Allergies control
            /// </summary>
            /// <param name="writer">writer to write the content</param>
            protected override void Render(HtmlTextWriter writer)
            {
                if (this.patientAllergies != null && this.patientAllergies.Count > 0)
                {
                    bool showAllAllergies = this.patientAllergies.Count > AllergiesLabelControl.MaxAllergies ? false : true;

                    int allergiesShownCount = showAllAllergies == true ? this.patientAllergies.Count : AllergiesLabelControl.MaxAllergies - 1;

                    writer.RenderBeginTag(HtmlTextWriterTag.Ol);

                    for (int i = 0; i < allergiesShownCount; i++)
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Li);
                        writer.Write(StringUtil.GetAntiXssWrappableString(this.patientAllergies[i].ToString()));
                        writer.RenderEndTag();
                    }

                    writer.RenderEndTag();

                    if (showAllAllergies == false)
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write(StringUtil.GetWrappableString(PatientBannerControl.Resources.MoreAllergiesLabel));
                        writer.RenderEndTag();
                    }
                }
            }            
        }
        #endregion
    }
}
