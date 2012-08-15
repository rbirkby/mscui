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

namespace NhsCui.Toolkit.WinForms
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using NhsCui.Toolkit.DateAndTime;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Text;
    using System.Collections;

    /// <summary>
    /// The control used to provide a consistent layout for common patient identification information within applications.
    /// </summary>
    [DefaultProperty("Identifier")]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "PatientBanner.bmp")]
    public partial class PatientBanner : UserControl, INotifyPropertyChanged, ISupportInitialize
    {
        #region Member Vars

        /// <summary>
        /// Internal spacing adjustment
        /// </summary>
        internal const int InternalControlPadding = 1;

        /// <summary>
        /// Maximum length of preferred name
        /// </summary>
        private const int MaxPreferredNameChars = 35;

        /// <summary>
        /// Maximum allergies that can be shown in subsection five
        /// </summary>
        private const int MaxAllergiesShown = 5;

        /// <summary>
        /// Border width
        /// </summary>
        private int borderWidth = 5;

        /// <summary>
        /// Border color
        /// </summary>
        private Color borderColor = SystemColors.ControlDarkDark;

        /// <summary>
        /// Hold the DoB internally as an NhsDate
        /// </summary>
        private NhsDate dateOfBirth;

        /// <summary>
        /// Hold the DoD internally as an NhsDate
        /// </summary>
        private NhsDate dateOfDeath;

        /// <summary>
        /// nameField member var
        /// </summary>
        private NameLabel nameField = new NameLabel();

        /// <summary>
        /// Hold the age internally as an NhsTimeSpan
        /// </summary>
        private NhsTimeSpan age;

        /// <summary>
        /// Expanded state of the control
        /// </summary>
        private bool panelExpanded;

        /// <summary>
        /// Show the patient image
        /// </summary>
        private bool imageDisplayed;

        /// <summary>
        /// Font to use for data in the top line of the control
        /// </summary>
        private Font zoneOneDataFont = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);

        /// <summary>
        /// Font to use for labels in the top line of the control
        /// </summary>
        private Font zoneOneLabelFont = new Font("Arial", 9F, FontStyle.Italic, GraphicsUnit.Point, (byte)0);

        /// <summary>
        /// Font to use for data in the head line of the control
        /// </summary>
        private Font zoneTwoDataFont = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);

        /// <summary>
        /// Font to use for labels in the head line of the control
        /// </summary>
        private Font zoneTwoLabelFont = new Font("Arial", 9F, FontStyle.Italic, GraphicsUnit.Point, (byte)0);

        /// <summary>
        /// Font to use for fields in the Zone Two titles of the control
        /// </summary>
        private Font zoneTwoTitleFont = new Font("Arial", 9F, FontStyle.Italic, GraphicsUnit.Point, (byte)0);

        /// <summary>
        /// Font to use for the patient name
        /// </summary>
        private Font patientNameFont = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);

        /// <summary>
        /// Font to use for Zone one labels when tooltip is present
        /// </summary>
        private Font zoneOneLabelWithTooltipFont = new Font("Arial", 9F, FontStyle.Italic, GraphicsUnit.Point, (byte)0);

        /// <summary>
        /// Font to use for Zone one data when tooltip is present
        /// </summary>
        private Font zoneOneDataWithTooltipFont = new Font("Arial", 9F, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.Point, (byte)0);

        /// <summary>
        /// Font color to use for Zone one labels when tooltip is present
        /// </summary>
        private Color zoneOneLabelWithTooltipFontColor = SystemColors.ControlText;

        /// <summary>
        /// Font color to use for Zone one data when tooltip is present
        /// </summary>
        private Color zoneOneDataWithTooltipFontColor = Color.Blue;

        /// <summary>
        /// Background colour of Zone One of the control
        /// </summary>
        private Color zoneOneBackColor = SystemColors.Control;

        /// <summary>
        /// Background colour of Zone One of the control if the patient is dead
        /// </summary>
        private Color deadPatientBackColor = SystemColors.ButtonShadow;

        /// <summary>
        /// Background colour of Zone Two of the control
        /// </summary>
        private Color zoneTwoBackColor = SystemColors.Control;

        /// <summary>
        /// Background colour of the Zone Two titles of the control
        /// </summary>
        private Color zoneTwoTitleBackColor = SystemColors.Control;
       
        /// <summary>
        /// Border colour for Zone One border - dead patient
        /// </summary>
        private Color deadPatientBorderColor = Color.Black;

        /// <summary>
        /// Hover colour for Zone Two border
        /// </summary>
        private Color zoneTwoHoverBorderColor = SystemColors.MenuHighlight;

        /// <summary>
        /// Has the PatientImage been set yet?
        /// </summary>
        private bool patientImageSet;

        /// <summary>
        /// Expanded Image
        /// </summary>
        private Image dropDownImage;

        /// <summary>
        /// Collapsed Image
        /// </summary>
        private Image collapseImage;

        /// <summary>
        /// Deceased patient background image
        /// </summary>
        private Image deceasedPatientBgImage;

        /// <summary>
        /// Tooltip text for zone two
        /// </summary>
        private string zoneTwoTooltip = PatientBannerControl.Resources.ZoneTwoToolTipText;

        /////// <summary>
        /////// Layout engine for the PatientBanner
        /////// </summary>
        ////private PatientBannerLayoutEngine patientBannerLayoutEngine;

        /// <summary>
        /// Allergy information of the patient
        /// </summary>
        private AllergyInformation allergyInformation = AllergyInformation.Unavailable;

        /// <summary>
        /// Allergies not recorded icon
        /// </summary>
        private Image allergiesNotRecordedIcon;

        /// <summary>
        /// Allergies recorded icon
        /// </summary>
        private Image allergiesRecordedIcon;

        /// <summary>
        /// Allergies unavailable icon
        /// </summary>
        private Image allergiesUnavailableIcon;

        /// <summary>
        /// Tooltip text for gender label
        /// </summary>
        private string genderLabelTooltipText = PatientBannerControl.Resources.GenderLabelTooltipText;

        /// <summary>
        /// Tooltip text for gender value
        /// </summary>
        private string genderValueTooltipText = PatientBannerControl.Resources.GenderValueTooltipText;

        /// <summary>
        /// Tooltip text for identifier label
        /// </summary>
        private string identifierLabelTooltipText = PatientBannerControl.Resources.IdentifierLabelTooltipText;

        /// <summary>
        /// Tooltip text for identifier
        /// </summary>
        private string identifierTooltipText = PatientBannerControl.Resources.IdentifierTooltipText;

        /// <summary>
        /// Patients allergies
        /// </summary>
        private AllergyCollection patientAllergies = new AllergyCollection();

        /// <summary>
        /// Internal variable to hold preferred name
        /// </summary>
        private string preferredNameText;
        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a PatientBanner object. 
        /// </summary>
        public PatientBanner()
        {
            this.InitializeComponent();
            this.AccessibleName = PatientBannerControl.Resources.AccessibleName;
            this.AccessibleDescription = PatientBannerControl.Resources.AccessibleDescription;
            this.AccessibleRole = AccessibleRole.Default;

            this.ToggleExpansion(this.panelExpanded);
            this.addressField.AddressDisplayFormat = AddressDisplayFormat.InForm;
            this.SetDefaultValues();
            this.SetLayout();
            this.ApplyExtraStyles();

            this.nameField.FamilyName = PatientBannerControl.Resources.FamilyName;
            this.nameField.GivenName = PatientBannerControl.Resources.GivenName;
            this.nameField.Title = PatientBannerControl.Resources.Title;
            this.Size = this.DefaultSize;
        }
                        
        #endregion

        #region Events

        /// <summary>
        /// Raised when gender value is clicked
        /// </summary>
        public event EventHandler GenderValueClick;

        /// <summary>
        /// Raised when Identifier is clicked
        /// </summary>
        public event EventHandler IdentifierClick;

        /// <summary>
        /// Raised when ViewAllAddresses link is clicked
        /// </summary>
        public event EventHandler ViewAllAddressesClick;

        /// <summary>
        /// Raised when ViewAllContactDetails link is clicked
        /// </summary>
        public event EventHandler ViewAllContactDetailsClick;

        /// <summary>
        /// Raised when ViewAllergyRecord link is clicked
        /// </summary>
        public event EventHandler ViewAllergyRecordClick;

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes. 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #endregion

        #region Properties
        
        /// <summary>
        /// Gets or sets the expanded state of Zone 2. 
        /// </summary>
        /// <remarks>
        /// Defaults to false. 
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue(false)]
        [Description("Determines whether the control is expanded or collapsed.")]
        [RefreshProperties(RefreshProperties.All)]
        public bool ZoneTwoExpanded
        {
            get
            {
                return this.panelExpanded;
            }

            set
            {
                if (value != this.panelExpanded)
                {
                    this.panelExpanded = value;
                    this.ToggleExpansion(value);
                    this.NotifyPropertyChanged("ZoneTwoExpanded");
                }
            }
        }

        /// <summary>
        /// Gets or sets the PatientBanner dropdown image. 
        /// </summary>
        [Category("Appearance")]
        public Image DropDownImage
        {
            get
            {
                return this.dropDownImage;
            }

            set
            {
                if (this.dropDownImage.Equals(value) == false)
                {
                    this.dropDownImage = value;
                    this.NotifyPropertyChanged("ExpandedImage");
                }
            }
        }

        /// <summary>
        /// Gets or sets the Background image applied for Zone 1 if the patient is deceased. 
        /// </summary>
        [Category("Appearance")]
        [Description("Background image applied for Zone 1 if the patient is deceased")]
        public Image DeadPatientBackgroundImage
        {
            get
            {
                return this.deceasedPatientBgImage;
            }

            set
            {
                if (this.deceasedPatientBgImage.Equals(value) == false)
                {
                    this.deceasedPatientBgImage = value;
                    this.zoneOnePanel.BackgroundImage = this.deceasedPatientBgImage;
                    this.NotifyPropertyChanged("DeadPatientBackgroundImage");

                    if (this.DesignMode)
                    {
                        this.SetAgeLabel();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the dropdown image to be displayed when Zone 2 is expanded. 
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("The dropdown Image to be displayed when Zone 2 is expanded.")]
        [RefreshProperties(RefreshProperties.All)]
        public Image CollapseImage
        {
            get
            {
                return this.collapseImage;
            }

            set
            {
                if (this.collapseImage.Equals(value) == false)
                {
                    this.collapseImage = value;
                    this.NotifyPropertyChanged("CollapsedImage");
                }
            }
        }

        /// <summary>
        /// Gets or sets the font for data in Zone 1. 
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("Font for Zone One data.")]
        [RefreshProperties(RefreshProperties.All)]
        public Font ZoneOneDataFont
        {
            get
            {
                return this.zoneOneDataFont;
            }

            set
            {
                if (value != this.zoneOneDataFont)
                {
                    this.zoneOneDataFont = value;
                    this.ApplyExtraStyles();
                    this.NotifyPropertyChanged("ZoneOneDataFont");

                    if (this.DesignMode)
                    {
                        this.Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the font for labels in Zone 1. 
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("Font for Zone One labels.")]
        [RefreshProperties(RefreshProperties.All)]
        public Font ZoneOneLabelFont
        {
            get
            {
                return this.zoneOneLabelFont;
            }

            set
            {
                if (value != this.zoneOneLabelFont)
                {
                    this.zoneOneLabelFont = value;
                    this.ApplyExtraStyles();
                    this.NotifyPropertyChanged("ZoneOneLabelFont");

                    if (this.DesignMode)
                    {
                        this.Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the font for labels in Zone 1 when tooltip is present 
        /// </summary>
        /// <remarks>
        /// ISVs can alter the behaviour of the font size on mouseover, however this is not a recommended approach.
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [Description("Font for Zone One labels when tooltip is present.")]
        [RefreshProperties(RefreshProperties.All)]
        public Font ZoneOneLabelWithTooltipFont
        {
            get
            {
                return this.zoneOneLabelWithTooltipFont;
            }

            set
            {
                if (value != this.zoneOneLabelWithTooltipFont)
                {
                    this.zoneOneLabelWithTooltipFont = value;
                    this.NotifyPropertyChanged("ZoneOneLabelWithTooltipFont");

                    if (this.DesignMode)
                    {
                        this.Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the font for data in Zone 1 when tooltip is present 
        /// </summary>
        /// <remarks>
        /// ISVs can alter the behaviour of the font size on mouseover, however this is not a recommended approach.
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [Description("Font for Zone One data when tooltip is present.")]
        [RefreshProperties(RefreshProperties.All)]
        public Font ZoneOneDataWithTooltipFont
        {
            get
            {
                return this.zoneOneDataWithTooltipFont;
            }

            set
            {
                if (value != this.zoneOneDataWithTooltipFont)
                {
                    this.zoneOneDataWithTooltipFont = value;
                    this.NotifyPropertyChanged("ZoneOneDataWithTooltipFont");

                    if (this.DesignMode)
                    {
                        this.Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the font color for labels in Zone 1 when tooltip is present 
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("Font color for Zone One labels when tooltip is present.")]
        [RefreshProperties(RefreshProperties.All)]
        public Color ZoneOneLabelWithTooltipFontColor
        {
            get
            {
                return this.zoneOneLabelWithTooltipFontColor;
            }

            set
            {
                if (value != this.zoneOneLabelWithTooltipFontColor)
                {
                    this.zoneOneLabelWithTooltipFontColor = value;
                    this.NotifyPropertyChanged("ZoneOneLabelWithTooltipFontColor");
                }
            }
        }

        /// <summary>
        /// Gets or sets the font color for data in Zone 1 when tooltip is present 
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("Font color for Zone One data when tooltip is present.")]
        [RefreshProperties(RefreshProperties.All)]
        public Color ZoneOneDataWithTooltipFontColor
        {
            get
            {
                return this.zoneOneDataWithTooltipFontColor;
            }

            set
            {
                if (value != this.zoneOneDataWithTooltipFontColor)
                {
                    this.zoneOneDataWithTooltipFontColor = value;
                    this.NotifyPropertyChanged("ZoneOneDataWithTooltipFontColor");
                }
            }
        }

        /// <summary>
        /// Gets or sets the font for data in Zone 2.  
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("Font for Zone Two data.")]
        [RefreshProperties(RefreshProperties.All)]
        public Font ZoneTwoDataFont
        {
            get
            {
                return this.zoneTwoDataFont;
            }

            set
            {
                if (value != this.zoneTwoDataFont)
                {
                    this.zoneTwoDataFont = value;
                    this.ApplyExtraStyles();
                    this.NotifyPropertyChanged("ZoneTwoDataFont");

                    if (this.DesignMode)
                    {
                        this.Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the border colour to be used when the mouse hovers or moves over Zone 2. 
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("Hover colour for Zone Two border.")]
        [RefreshProperties(RefreshProperties.All)]
        public Color ZoneTwoHoverBorderColor
        {
            get
            {
                return this.zoneTwoHoverBorderColor;
            }

            set
            {
                if (value != this.zoneTwoHoverBorderColor)
                {
                    this.zoneTwoHoverBorderColor = value;
                    this.NotifyPropertyChanged("ZoneTwoHoverBorderColor");
                }
            }
        }

        /// <summary>
        ///  Gets or sets the font for labels in Zone 2. 
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("Font for Zone Two labels.")]
        [RefreshProperties(RefreshProperties.All)]
        public Font ZoneTwoLabelFont
        {
            get
            {
                return this.zoneTwoLabelFont;
            }

            set
            {
                if (value != this.zoneTwoLabelFont)
                {
                    this.zoneTwoLabelFont = value;
                    this.ApplyExtraStyles();
                    this.NotifyPropertyChanged("ZoneTwoLabelFont");

                    if (this.DesignMode)
                    {
                        this.Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the font for the title area of Zone 2. 
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("Font for Zone Two titles.")]
        [RefreshProperties(RefreshProperties.All)]
        public Font ZoneTwoTitleFont
        {
            get
            {
                return this.zoneTwoTitleFont;
            }

            set
            {
                if (value != this.zoneTwoTitleFont)
                {
                    this.zoneTwoTitleFont = value;
                    this.ApplyExtraStyles();
                    this.SetLayout();
                    this.NotifyPropertyChanged("ZoneTwoTitleFont");
                }
            }
        }

        /// <summary>
        /// Gets or sets the font for the patient's name in Zone 1. 
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("Font for Patient Name.")]
        [RefreshProperties(RefreshProperties.All)]
        public Font PatientNameFont
        {
            get
            {
                return this.patientNameFont;
            }

            set
            {
                if (value != this.patientNameFont)
                {
                    this.patientNameFont = value;
                    this.ApplyExtraStyles();
                    this.NotifyPropertyChanged("PatientNameFont");

                    if (this.DesignMode)
                    {
                        this.Refresh();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the background colour for Zone 1. 
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("Background colour for Zone One.")]
        [RefreshProperties(RefreshProperties.All)]
        public Color ZoneOneBackColor
        {
            get
            {
                return this.zoneOneBackColor;
            }

            set
            {
                if (value != this.zoneOneBackColor)
                {
                    this.zoneOneBackColor = value;
                    this.ApplyExtraStyles();
                    this.NotifyPropertyChanged("ZoneOneBackColor");
                }
            }
        }

        /// <summary>
        /// Gets or sets the background colour for Zone 1. 
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("Background colour for Zone One.")]
        [RefreshProperties(RefreshProperties.All)]
        public Color ActivePatientBackColor
        {
            get
            {
                return this.zoneOneBackColor;
            }

            set
            {
                if (value != this.zoneOneBackColor)
                {
                    this.zoneOneBackColor = value;
                    this.ApplyExtraStyles();
                    this.NotifyPropertyChanged("ActivePatientBackColor");
                }
            }
        }

        /// <summary>
        /// Gets or sets the background colour to be used for deceased patients. 
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("Background colour for Zone One if the patient is dead.")]
        [RefreshProperties(RefreshProperties.All)]
        public Color DeadPatientBackColor
        {
            get
            {
                return this.deadPatientBackColor;
            }

            set
            {
                if (value != this.deadPatientBackColor)
                {
                    this.deadPatientBackColor = value;
                    this.ApplyExtraStyles();
                    this.NotifyPropertyChanged("DeadPatientBackColor");
                }
            }
        }

        /// <summary>
        /// Gets or sets the background colour for Zone Two. 
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("Background colour for Zone Two.")]
        [RefreshProperties(RefreshProperties.All)]
        public Color ZoneTwoBackColor
        {
            get
            {
                return this.zoneTwoBackColor;
            }

            set
            {
                if (value != this.zoneTwoBackColor)
                {
                    this.zoneTwoBackColor = value;
                    this.ApplyExtraStyles();
                    this.NotifyPropertyChanged("ZoneTwoBackColor");
                }
            }
        }

        /// <summary>
        /// Gets or sets the background colour for Zone 2 titles. 
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("Background colour for Zone Two.")]
        [RefreshProperties(RefreshProperties.All)]
        public Color ZoneTwoTitleBackColor
        {
            get
            {
                return this.zoneTwoTitleBackColor;
            }

            set
            {
                if (value != this.zoneTwoTitleBackColor)
                {
                    this.zoneTwoTitleBackColor = value;
                    this.ApplyExtraStyles();
                    this.NotifyPropertyChanged("ZoneTwoTitleBackColor");
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient�s address.
        /// </summary>
        /// <remarks>
        /// Defaults to "Address". 
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue("Address"), Localizable(true)]
        [Description("The text associated with the Address field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string SubsectionOneTitle
        {
            get
            {
                return this.subsectionOneTitleLabel.Text;
            }

            set
            {
                if (value != this.subsectionOneTitleLabel.Text)
                {
                    this.subsectionOneTitleLabel.Text = value;
                    this.NotifyPropertyChanged("AddressLabelText");

                    if (this.DesignMode)
                    {                     
                        this.SetLayout();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the first line of the patient's address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The first field of the Patient Address.")]
        [RefreshProperties(RefreshProperties.All)]
        public string Address1
        {
            get
            {
                return this.addressField.Address1;
            }

            set
            {
                if (value != this.addressField.Address1)
                {
                    this.addressField.Address1 = value;
                    this.UpdateAddressPreview();
                    this.NotifyPropertyChanged("Address1");
                }
            }
        }

        /// <summary>
        /// Gets or sets the second line of the patient's address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The second field of the Patient Address.")]
        [RefreshProperties(RefreshProperties.All)]
        public string Address2
        {
            get
            {
                return this.addressField.Address2;
            }

            set
            {
                if (value != this.addressField.Address2)
                {
                    this.addressField.Address2 = value;
                    this.UpdateAddressPreview();
                    this.NotifyPropertyChanged("Address2");
                }
            }
        }

        /// <summary>
        /// Gets or sets the third line of the patient's address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The third field of the Patient Address.")]
        [RefreshProperties(RefreshProperties.All)]
        public string Address3
        {
            get
            {
                return this.addressField.Address3;
            }

            set
            {
                if (value != this.addressField.Address3)
                {
                    this.addressField.Address3 = value;
                    this.UpdateAddressPreview();
                    this.NotifyPropertyChanged("Address3");
                }
            }
        }

        /// <summary>
        /// Gets or sets the town in the patient�s address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The Town field of the Patient Address.")]
        [RefreshProperties(RefreshProperties.All)]
        public string Town
        {
            get
            {
                return this.addressField.Town;
            }

            set
            {
                if (value != this.addressField.Town)
                {
                    this.addressField.Town = value;
                    this.UpdateAddressPreview();
                    this.NotifyPropertyChanged("Town");
                }
            }
        }

        /// <summary>
        /// Gets or sets the county in the patient�s address.  
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The County field of the Patient Address.")]
        [RefreshProperties(RefreshProperties.All)]
        public string County
        {
            get
            {
                return this.addressField.County;
            }

            set
            {
                if (value != this.addressField.County)
                {
                    this.addressField.County = value;
                    this.UpdateAddressPreview();
                    this.NotifyPropertyChanged("County");
                }
            }
        }

        /// <summary>
        /// Gets or sets the patient�s postcode. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// If the postcode is present, it should display in capitalized form and as the final element before the 
        /// <see cref="P:NhsCui.Toolkit.WinForms.PatientBanner.Country">Country</see>. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The Post Code field of the Patient Address.")]
        [RefreshProperties(RefreshProperties.All)]
        public string Postcode
        {
            get
            {
                return this.addressField.Postcode;
            }

            set
            {
                if (value != this.addressField.Postcode)
                {
                    this.addressField.Postcode = value;
                    this.UpdateAddressPreview();
                    this.NotifyPropertyChanged("Postcode");
                }
            }
        }

        /// <summary>
        /// Gets or sets the country in the patient�s address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed.  
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The Country field of the Patient Address.")]
        [RefreshProperties(RefreshProperties.All)]
        public string Country
        {
            get
            {
                return this.addressField.Country;
            }

            set
            {
                if (value != this.addressField.Country)
                {
                    this.addressField.Country = value;
                    this.NotifyPropertyChanged("Country");
                }
            }
        }

        /// <summary>
        /// Gets or sets the address type of the patient�s address. 
        /// </summary>
        /// <remarks>
        /// Defaults to Usual address
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("Usual address")]
        [Description("The Town field of the Patient Address.")]
        [RefreshProperties(RefreshProperties.All)]
        public string AddressTypeLabelText
        {
            get
            {
                return this.addressField.AddressType;
            }

            set
            {
                if (value != this.addressField.AddressType)
                {
                    this.addressField.AddressType = value;
                    this.NotifyPropertyChanged("AddressTypeLabelText");
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient's date of birth. 
        /// </summary>
        /// <remarks>
        /// Defaults to "DoB". 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("Born"), Localizable(true)]
        [Description("The text associated with the DateOfBirth field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string DateOfBirthLabelText
        {
            get
            {
                return this.dateOfBirthLabel.Text;
            }

            set
            {
                if (value != this.dateOfBirthLabel.Text)
                {
                    this.dateOfBirthLabel.Text = value;
                    this.NotifyPropertyChanged("DateOfBirthLabelText");

                    if (this.DesignMode)
                    {
                        this.SetLayout();
                    }
                }
            }
        }        

        /// <summary>
        /// Gets or sets the patient's date of birth. 
        /// </summary>
        [Bindable(true), Category("Patient Details"), DefaultValue(null)]
        [Description("The DateOfBirth field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public DateTime DateOfBirth
        {
            get
            {
                return this.dateOfBirth.DateValue;
            }

            set
            {
                if (value > DateTime.Now)
                {
                    this.dateOfBirth.DateValue = DateTime.MinValue;
                    throw new ArgumentException(PatientBannerControl.Resources.InvalidDateOfBirthExcpetionMessage);
                }

                if (value != this.dateOfBirth.DateValue)
                {
                    this.dateOfBirth.DateValue = value;
                    this.dateOfBirthData.Text = GetDateSpecificDisplay(this.dateOfBirth);
                                        
                    this.NotifyPropertyChanged("DateOfBirth");

                    if (this.DesignMode)
                    {
                        this.SetAgeLabel();
                        this.SetLayout();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient's date of death. 
        /// </summary>
        /// <remarks>
        /// Defaults to "DoD". 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("Died"), Localizable(true)]
        [Description("The text associated with the DateOfDeath field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string DateOfDeathLabelText
        {
            get
            {
                return this.dateOfDeathLabel.Text;
            }

            set
            {
                if (value != this.dateOfDeathLabel.Text)
                {
                    this.dateOfDeathLabel.Text = value;
                    this.NotifyPropertyChanged("DateOfDeathLabelText");

                    if (this.DesignMode)
                    {
                        this.SetLayout();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the patient's date of death. 
        /// </summary>
        [Bindable(true), Category("Patient Details"), DefaultValue(null)]
        [Description("The DateOfDeath field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public DateTime DateOfDeath
        {
            get
            {
                if (this.dateOfDeath != null)
                {
                    return this.dateOfDeath.DateValue;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }

            set
            {
                if (value > DateTime.Now)
                {
                    this.dateOfDeath.DateValue = DateTime.MinValue;
                    throw new ArgumentException(PatientBannerControl.Resources.InvalidDateOfDeathExcpetionMessage);
                }

                if (value != this.dateOfDeath.DateValue)
                {
                    this.dateOfDeath.DateValue = value;
                    if (this.dateOfDeath.DateValue != DateTime.MinValue)
                    {
                        this.dateOfDeathData.Text = this.dateOfDeath.ToString();
                        this.dateOfDeathData.Visible = true;
                        this.dateOfDeathLabel.Visible = true;
                        this.ageAtDeath.Visible = true;
                        this.ageAtDeathLabel.Visible = true;
                    }
                    else
                    {
                        this.dateOfDeathData.Visible = false;
                        this.dateOfDeathLabel.Visible = false;
                        this.ageAtDeath.Visible = false;
                        this.ageAtDeathLabel.Visible = false;
                    }
                    
                    this.NotifyPropertyChanged("DateOfDeath");

                    if (this.DesignMode)
                    {
                        this.SetAgeLabel();
                        this.SetLayout();
                    }                    
                }
            }
        }

        /// <summary>
       /// Gets or sets the caption associated with the patient's unique identifier or NHS number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "xxx-xxx-xxxx". 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("NHS No."), Localizable(true)]
        [Description("The text associated with the Identifier field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string IdentifierLabelText
        {
            get
            {
                return this.identifierLabel.Text;
            }

            set
            {
                if (value != this.identifierLabel.Text)
                {
                    this.identifierLabel.Text = value;
                    this.NotifyPropertyChanged("IdentifierLabelText");

                    if (this.DesignMode)
                    {
                        this.SetLayout();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a unique identifier associated with the patient. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Other". The patient's NHS number may be used if this is set to "NhsNumber". 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), Browsable(true)]
        [Description("The NhsNumber associated with the IdentifierLabel control.  Must be set to a valid NhsNumber if the control's IdentifierType is NhsNumber.")]
        [RefreshProperties(RefreshProperties.All)]
        public string Identifier
        {
            get
            {
                return this.identifierData.Text;
            }

            set
            {
                this.btnIdentifierLabel.Text = string.Empty;

                if (value != this.identifierData.Text)
                {
                    this.identifierData.Text = value;
                    this.btnIdentifierLabel.Text = this.identifierData.Text;
                    this.NotifyPropertyChanged("Identifier");

                    if (this.DesignMode)
                    {
                        this.SetLayout();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets whether to process the <see cref="P:NhsCui.Toolkit.WinForms.PatientBanner.Identifier">Identifier</see> with the NhsNumber 
        /// validation checksum.
        /// </summary>
        /// <remarks>
        /// Defaults to "Other". If this is set to "Other", no validation is performed. If this is set to "NhsNumber", the text must be 
        /// a valid NHS number.
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue(IdentifierType.Other)]
        [Description("The type of the identifier. If set to NhsNumber then the Text property must be set to a valid NhsNumber.")]
        [RefreshProperties(RefreshProperties.All)]
        public IdentifierType IdentifierType
        {
            get
            {
                return this.identifierData.IdentifierType;
            }

            set
            {
                if (value != this.identifierData.IdentifierType)
                {
                    if (!Enum.IsDefined(typeof(IdentifierType), value))
                    {
                        throw new ArgumentOutOfRangeException("value");
                    }

                    this.identifierData.IdentifierType = value;
                    this.NotifyPropertyChanged("IdentifierType");
                }
            }
        }

        /// <summary>
        /// Gets or sets the patient's image. 
        /// </summary>
        /// <remarks>
        /// Defaults to true.
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [Description("The PatientImage field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public Image PatientImage
        {
            get
            {
                return this.patientPicture.Image;
            }

            set
            {
                if (value != this.patientPicture.Image)
                {
                    this.patientImageSet = true;
                    this.patientPicture.Image = value;
                    this.NotifyPropertyChanged("PatientImage");
                }
            }
        }

        /// <summary>
        /// Specifies whether the patient's image is displayed. 
        /// </summary>
        /// <remarks>
        /// Defaults to false
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue(false)]
        [Description("Determines whether an image should be shown in the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public bool ImageDisplayed
        {
            get
            {
                return this.imageDisplayed;
            }

            set
            {
                if (value != this.imageDisplayed)
                {
                    this.imageDisplayed = value;
                    this.NotifyPropertyChanged("ImageDisplayed");
                    this.patientPicture.Visible = this.imageDisplayed;
                }

                if (this.DesignMode)
                {
                    this.SetLayout();                    
                }
            }
        }

        /// <summary>
        /// Gets the complete patient name as displayed. 
        /// </summary>
        [Bindable(false), Category("Patient Details")]
        [Description("The ReadOnly Display Value field of the Patient Name.")]
        [RefreshProperties(RefreshProperties.All)]
        public string NameDisplayValue
        {
            get
            {
                return this.nameField.DisplayValue;
            }
        }

        /// <summary>
        ///  Gets or sets the patient's family name. 
        /// </summary>
        [Bindable(true), Category("Patient Details")]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "FamilyName")]
        [Description("The Last Name field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string FamilyName
        {
            get
            {
                return this.nameField.FamilyName;
            }

            set
            {
                if (value != this.nameField.FamilyName)
                {
                    this.nameField.FamilyName = value;
                    this.patientName.Text = this.nameField.DisplayValue;
                    this.NotifyPropertyChanged("FamilyName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the patient's given name. 
        /// </summary>
        [Bindable(true), Category("Patient Details")]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "GivenName")]
        [Description("The First Name field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string GivenName
        {
            get
            {
                return this.nameField.GivenName;
            }

            set
            {
                if (value != this.nameField.GivenName)
                {
                    this.nameField.GivenName = value;
                    this.patientName.Text = this.nameField.DisplayValue;
                    this.NotifyPropertyChanged("GivenName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the patient's title. 
        /// </summary>
        [Bindable(true), Category("Patient Details")]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "Title")]
        [Description("The Title field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string Title
        {
            get
            {
                return this.nameField.Title;
            }

            set
            {
                if (value != this.nameField.Title)
                {
                    this.nameField.Title = value;
                    this.patientName.Text = this.nameField.DisplayValue;
                    this.NotifyPropertyChanged("Title");
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the overall patient contacts section. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Phone and email". 
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue("Phone and email"), Localizable(true)]
        [Description("The text associated with the phoneEmail field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string SubsectionTwoTitle
        {
            get
            {
                return this.subsectionTwoTitleLabel.Text;
            }

            set
            {
                if (value != this.subsectionTwoTitleLabel.Text)
                {
                    this.subsectionTwoTitleLabel.Text = value;
                    this.NotifyPropertyChanged("ContactDetailslLabelText");

                    if (this.DesignMode)
                    {
                        this.SetLayout();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the patient's email address. 
        /// </summary>
        /// <remarks>
        /// If this element is left empty, <see cref="P:NhsCui.Toolkit.WinForms.PatientBanner.EmailLabelText">EmailLabelText</see> 
        /// will be displayed with blank space next to it.  
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("")]
        [Description("The Email Address field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string EmailAddress
        {
            get
            {
                return this.phoneEmailData.EmailAddress;
            }

            set
            {
                if (value != this.phoneEmailData.EmailAddress)
                {
                    this.phoneEmailData.EmailAddress = value;
                    this.contactDetailsPreviewLabel.Text = this.phoneEmailData.GetContactDetailsSummary();
                    this.NotifyPropertyChanged("EmailAddress");
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient's email address.
        /// </summary>
        /// <remarks>
        /// Defaults to "email". 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("Email"), Localizable(true)]
        [Description("The text associated with the Email Address field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string EmailLabelText
        {
            get
            {
                return this.phoneEmailData.EmailLabelText;
            }

            set
            {
                if (value != this.phoneEmailData.EmailLabelText)
                {
                    this.phoneEmailData.EmailLabelText = value;
                    this.NotifyPropertyChanged("EmailLabelText");
                }
            }
        }

        /// <summary>
        /// Gets or sets the patient's home phone number. 
        /// </summary>
        /// <remarks>
        /// If this element is left empty, <see cref="P:NhsCui.Toolkit.WinForms.PatientBanner.HomePhoneLabelText">HomePhoneLabelText</see> 
        /// will be displayed with blank space next to it.
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("")]
        [Description("The Home Phone Number field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string HomePhoneNumber
        {
            get
            {
                return this.phoneEmailData.HomePhoneNumber;
            }

            set
            {
                if (value != this.phoneEmailData.HomePhoneNumber)
                {
                    this.phoneEmailData.HomePhoneNumber = value;
                    this.contactDetailsPreviewLabel.Text = this.phoneEmailData.GetContactDetailsSummary();
                    this.NotifyPropertyChanged("HomePhoneNumber");
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient's home phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Home". 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("Home"), Localizable(true)]
        [Description("The text associated with the Home Phone Number field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string HomePhoneLabelText
        {
            get
            {
                return this.phoneEmailData.HomePhoneLabelText;
            }

            set
            {
                if (value != this.phoneEmailData.HomePhoneLabelText)
                {
                    this.phoneEmailData.HomePhoneLabelText = value;
                    this.NotifyPropertyChanged("HomePhoneLabelText");
                }
            }
        }

        /// <summary>
        /// Gets or sets the patient's mobile phone number. 
        /// </summary>
        /// <remarks>
        /// If this element is left empty, <see cref="P:NhsCui.Toolkit.WinForms.PatientBanner.MobilePhoneLabelText">MobilePhoneLabelText</see> 
        /// will be displayed with blank space next to it. 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("")]
        [Description("The Mobile Phone Number field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string MobilePhoneNumber
        {
            get
            {
                return this.phoneEmailData.MobilePhoneNumber;
            }

            set
            {
                if (value != this.phoneEmailData.MobilePhoneNumber)
                {
                    this.phoneEmailData.MobilePhoneNumber = value;
                    this.contactDetailsPreviewLabel.Text = this.phoneEmailData.GetContactDetailsSummary();
                    this.NotifyPropertyChanged("MobilePhoneNumber");
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient's mobile phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Mobile". 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("Mobile"), Localizable(true)]
        [Description("The text associated with the Mobile Phone Number field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string MobilePhoneLabelText
        {
            get
            {
                return this.phoneEmailData.MobilePhoneLabelText;
            }

            set
            {
                if (value != this.phoneEmailData.MobilePhoneLabelText)
                {
                    this.phoneEmailData.MobilePhoneLabelText = value;
                    this.NotifyPropertyChanged("MobilePhoneLabelText");
                }
            }
        }

        /// <summary>
        /// Gets or sets the patient's work phone number. 
        /// </summary>
        /// <remarks>
        /// If this element is left empty, <see cref="P:NhsCui.Toolkit.WinForms.PatientBanner.WorkPhoneLabelText">WorkPhoneLabelText</see> 
        /// will be displayed with blank space next to it. 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("")]
        [Description("The Work Phone Number field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string WorkPhoneNumber
        {
            get
            {
                return this.phoneEmailData.WorkPhoneNumber;
            }

            set
            {
                if (value != this.phoneEmailData.WorkPhoneNumber)
                {
                    this.phoneEmailData.WorkPhoneNumber = value;
                    this.contactDetailsPreviewLabel.Text = this.phoneEmailData.GetContactDetailsSummary();
                    this.NotifyPropertyChanged("WorkPhoneNumber");
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient's work phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Work". 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("Work"), Localizable(true)]
        [Description("The text associated with the Work Phone Number field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string WorkPhoneLabelText
        {
            get
            {
                return this.phoneEmailData.WorkPhoneLabelText;
            }

            set
            {
                if (value != this.phoneEmailData.WorkPhoneLabelText)
                {
                    this.phoneEmailData.WorkPhoneLabelText = value;
                    this.NotifyPropertyChanged("WorkPhoneLabelText");
                }
            }
        }

        /// <summary>
        /// Gets or sets the patient's gender.
        /// </summary>
        /// <remarks>
        /// This may be "Female", "Male", "NotSpecified"  or "NotKnown".  
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue(PatientGender.NotKnown)]
        [Description("The PatientGender field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public PatientGender Gender
        {
            get
            {
                return this.genderData.Value;
            }

            set
            {
                if (value != this.genderData.Value)
                {
                    this.genderData.Value = value;
                    this.btnGenderData.Text = this.genderData.Text;
                    this.NotifyPropertyChanged("Gender");
                    if (this.patientImageSet == false)
                    {
                        this.ApplyDefaultPatientImage();
                    }

                    if (this.DesignMode)
                    {
                        this.SetLayout();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the patient's gender. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Gender". 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("Gender"), Localizable(true)]
        [Description("The text associated with the Gender field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string GenderLabelText
        {
            get
            {
                return this.genderLabel.Text;
            }

            set
            {
                if (value != this.genderLabel.Text)
                {
                    this.genderLabel.Text = value;
                    this.NotifyPropertyChanged("GenderLabelText");

                    if (this.DesignMode)
                    {
                        this.SetLayout();
                    }
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the ToolTip for Zone 2. 
        /// </summary>
        [Bindable(true), Category("Behavior"), Localizable(true)]
        [Description("The text to be displayed in the tooltip for Zone Two of the Patient Banner.")]
        [ResourceDefaultValue(typeof(PatientBannerControl.Resources), "ZoneTwoToolTipText")]
        [RefreshProperties(RefreshProperties.All)]
        public string ZoneTwoTooltip
        {
            get
            {
                return this.zoneTwoTooltip;
            }

            set
            {
                if (value != this.zoneTwoTooltip)
                {
                    this.zoneTwoTooltip = value;
                    this.ApplyZoneTwoTooltip();
                    this.NotifyPropertyChanged("ZoneTwoToolTip");
                }
            }
        }

        /// <summary>
        /// Gets or sets the ToolTip for Gender label. 
        /// </summary>
        [Bindable(true), Category("Behavior"), Localizable(true)]
        [Description("The text to be displayed in the tooltip for Gender label of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue("Gender: a person's current Gender. This may be different from a person's Sex which is a person's Gender defined at the point of birth registration.")]
        public string GenderLabelTooltip
        {
            get
            {
                return this.genderLabelTooltipText;
            }

            set
            {
                if (value != this.genderLabelTooltipText)
                {
                    this.genderLabelTooltipText = value;
                    this.ApplyZoneOneTooltip();
                    this.NotifyPropertyChanged("GenderLabelTooltip");
                }
            }
        }

        /// <summary>
        /// Gets or sets the ToolTip for Gender value. 
        /// </summary>
        [Bindable(true), Category("Behavior"), Localizable(true)]
        [Description("The text to be displayed in the tooltip for Gender value of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue("Click here to open this section of the record")]
        public string GenderValueTooltip
        {
            get
            {
                return this.genderValueTooltipText;
            }

            set
            {
                if (value != this.genderValueTooltipText)
                {
                    this.genderValueTooltipText = value;
                    this.ApplyZoneOneTooltip();
                    this.NotifyPropertyChanged("GenderValueTooltip");
                }
            }
        }

        /// <summary>
        /// Gets or sets the ToolTip for Identifier label. 
        /// </summary>
        [Bindable(true), Category("Behavior"), Localizable(true)]
        [Description("The text to be displayed in the tooltip for identifier label of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue("A ten-digit number used to identify a person uniquely within the NHS in England and Wales")]
        public string IdentifierLabelTooltip
        {
            get
            {
                return this.identifierLabelTooltipText;
            }

            set
            {
                if (value != this.identifierLabelTooltipText)
                {
                    this.identifierLabelTooltipText = value;
                    this.ApplyZoneOneTooltip();
                    this.NotifyPropertyChanged("IdentifierLabelTooltip");
                }
            }
        }

        /// <summary>
        /// Gets or sets the ToolTip for Identifier. 
        /// </summary>
        [Bindable(true), Category("Behavior"), Localizable(true)]
        [Description("The text to be displayed in the tooltip for identifier of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue("Click here to open this section of the record")]
        public string IdentifierTooltip
        {
            get
            {
                return this.identifierTooltipText;
            }

            set
            {
                if (value != this.identifierTooltipText)
                {
                    this.identifierTooltipText = value;
                    this.ApplyZoneOneTooltip();
                    this.NotifyPropertyChanged("IdentifierTooltip");
                }
            }
        }

        /// <summary>
        /// Gets or sets the border colour for Patient banner. 
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("The Border colour for the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public Color BorderColor
        {
            get
            {
                return this.borderColor;
            }

            set
            {
                if (value != this.borderColor)
                {
                    this.borderColor = value;
                    this.ApplyExtraStyles();
                    this.NotifyPropertyChanged("BorderColor");
                }
            }
        }

        /// <summary>
        /// Gets or sets the border width for Patient banner. 
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(5)]
        [Description("Gets or sets the border width for Patient banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public int BorderWidth
        {
            get
            {
                return this.borderWidth;
            }

            set
            {
                if (value != this.borderWidth)
                {
                    this.borderWidth = value;                    
                    this.NotifyPropertyChanged("BorderWidth");
                }

                if (this.DesignMode)
                {
                    this.SetLayout();
                    this.ApplyExtraStyles();
                }
            }
        }

        /// <summary>
        /// Gets or sets the border colour to be used for deceased patients.
        /// </summary>
        [Bindable(true), Category("Appearance")]
        [Description("The dead patient Border colour for Zone One of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public Color DeadPatientBorderColor
        {
            get
            {
                return this.deadPatientBorderColor;
            }

            set
            {
                if (value != this.deadPatientBorderColor)
                {
                    this.deadPatientBorderColor = value;
                    this.ApplyExtraStyles();
                    this.NotifyPropertyChanged("DeadPatientBorderColor");
                }
            }
        }       

        /// <summary>
        /// Gets or sets the title for Subsection three
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        [Description("The title for subsection three of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string SubsectionThreeTitle
        {
            get
            {
                return this.subsectionThreeTitle.Text;
            }

            set
            {
                this.subsectionThreeTitle.Text = value;
                this.NotifyPropertyChanged("SubsectionThreeTitle");

                if (this.DesignMode)
                {
                    this.SetLayout();
                }
            }
        }

        /// <summary>
        /// Gets or sets the title for Subsection four
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue("")]
        [Description("The title for subsection four of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string SubsectionFourTitle
        {
            get
            {
                return this.subsectionFourTitle.Text;
            }

            set
            {
                this.subsectionFourTitle.Text = value;
                this.NotifyPropertyChanged("SubsectionFourTitle");

                if (this.DesignMode)
                {
                    this.SetLayout();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the width for Subsection one
        /// </summary>
        /// <remarks>
        /// defaults to 206 (25%)
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue(206)]
        [Description("The width for subsection one of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public Int32 SubsectionOneWidth
        {
            get
            {
                return this.addressField.Width;
            }

            set
            {
                this.addressField.Width = value;
                this.NotifyPropertyChanged("SubsectionOneWidth");
                this.SetLayout();
            }
        }

        /// <summary>
        /// Gets or sets the width for Subsection two
        /// </summary>
        /// <remarks>
        /// defaults to 215 (26%)
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue(220)]
        [Description("The width for subsection two of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public Int32 SubsectionTwoWidth
        {
            get
            {
                return this.phoneEmailData.Width;
            }

            set
            {
                this.phoneEmailData.Width = value;
                this.NotifyPropertyChanged("SubsectionTwoWidth");
                this.SetLayout();
            }
        }
        
        /// <summary>
        /// Gets or sets the width for Subsection three
        /// </summary>
        /// <remarks>
        /// defaults to 99 (12%)
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue(94)]
        [Description("The width for subsection three of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public Int32 SubsectionThreeWidth
        {
            get
            {
                return this.subsectionThree.Width;
            }

            set
            {
                this.subsectionThree.Width = value;
                this.NotifyPropertyChanged("SubsectionThreeWidth");
                this.SetLayout();                
            }
        }

        /// <summary>
        /// Gets or sets the width for Subsection four
        /// </summary>
        /// <remarks>
        /// defaults to 99 (12%)
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue(94)]
        [Description("The width for subsection four of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public Int32 SubsectionFourWidth
        {
            get
            {
                return this.subsectionFour.Width;
            }

            set
            {
                this.subsectionFour.Width = value;
                this.NotifyPropertyChanged("SubsectionFourWidth");
                this.SetLayout();
            }
        }

        /// <summary>
        /// Gets or sets the width for Subsection five
        /// </summary>
        /// <remarks>
        /// defaults to 207 (25%)
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue(187)]
        [Description("The width for subsection five of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public Int32 SubsectionFiveWidth
        {
            get
            {
                return this.subsectionFive.Width;
            }

            set
            {
                this.subsectionFive.Width = value;
                this.NotifyPropertyChanged("SubsectionFiveWidth");
                this.SetLayout();
            }
        }
               
        /// <summary>
        /// Gets or sets the caption associated with the patient's preferred name. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Preferred name". 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("Preferred name"), Localizable(true)]
        [Description("The text associated with the PreferredName field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string PreferredNameLabelText
        {
            get
            {
                return this.preferredNameLabel.Text;
            }

            set
            {
                if (value != this.preferredNameLabel.Text)
                {
                    this.preferredNameLabel.Text = value;
                    this.NotifyPropertyChanged("PreferredNameLabelText");

                    if (this.DesignMode || this.IsDeceased)
                    {
                        this.SetLayout();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the patient's preferred name. 
        /// </summary>
        [Bindable(true), Category("Patient Details"), DefaultValue("")]
        [Description("The PreferredName field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string PreferredName
        {
            get
            {                
                return this.preferredNameText;
            }

            set
            {                
                if (value != this.preferredNameText)
                {
                    this.preferredNameText = value;
                    this.preferredName.Text = this.preferredNameText;

                    if (!string.IsNullOrEmpty(this.preferredNameText))
                    {
                        if (this.preferredNameText.Trim().Length > PatientBanner.MaxPreferredNameChars)
                        {
                            this.preferredName.Text = this.preferredNameText.Trim().Substring(0, PatientBanner.MaxPreferredNameChars - PatientName.Ellipsis.Length) + PatientName.Ellipsis;
                        }
                    }
                   
                    this.NotifyPropertyChanged("PreferredName");

                    /* Adding IsDeceased condition so as to show preferred name even when the patient is deceased. 
                    Not sure why this is not being shown need to investigate more. Adding this as a temporary work around */
                    if (this.DesignMode || this.IsDeceased)
                    {
                        this.SetPreferredNameVisibility();
                        this.SetLayout();
                    }
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the caption associated with the patient's Age at death. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Age at death". 
        /// </remarks>
        [Bindable(true), Category("Patient Details"), DefaultValue("Age at death"), Localizable(true)]
        [Description("The text associated with the AgeAtDeath field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string AgeAtDeathLabelText
        {
            get
            {
                return this.ageAtDeathLabel.Text;
            }

            set
            {
                if (value != this.ageAtDeathLabel.Text)
                {
                    this.ageAtDeathLabel.Text = value;
                    this.NotifyPropertyChanged("AgeAtDeathLabelText");

                    if (this.DesignMode)
                    {
                        this.SetLayout();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the patient's allergy information. 
        /// </summary>
        [Bindable(true), Category("Patient Details"), DefaultValue(AllergyInformation.Unavailable)]
        [Description("The AllergyInformation field of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public AllergyInformation AllergyInformation
        {
            get
            {
                return this.allergyInformation;
            }

            set
            {
                if (value != this.allergyInformation)
                {
                    this.allergyInformation = value;
                    this.allergiesInformationLabel.Text = PatientBanner.GetAllergyInformation(this.allergyInformation);
                    this.NotifyPropertyChanged("AllergyInformation");                    
                }
            }
        }

        /// <summary>
        /// Gets or Sets the patient allergies
        /// </summary>
        [Category("Patient Details"), DefaultValue(null)]
        [Bindable(true)]
        [Localizable(true)]
        [Description("List of patient allergies")]
        [NotifyParentProperty(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AllergyCollection Allergies
        {
            get
            {
                return this.patientAllergies;
            }
        }

        /// <summary>
        /// Gets the subsection three
        /// </summary>
        /// <remarks>
        /// This property allows us to add controls on to subsection three in Zone two. 
        /// Please use the controls property to add controls onto the subsection and recommend not to change any other properties.
        /// Changes to any other properties of the panel may result in undesired behavior.
        /// </remarks>
        [Description("The Subsection three in Zone 2 of the Patient Banner.")]
        public Panel SubsectionThree
        {
            get
            {
                return this.subsectionThree;
            }
        }

        /// <summary>
        /// Gets the subsection four
        /// </summary>
        /// <remarks>
        /// This property allows us to add controls on to subsection four in Zone two. 
        /// Please use the controls property to add controls onto the subsection and recommend not to change any other properties.
        /// Changes to any other properties of the panel may result in undesired behavior.
        /// </remarks>
        [Description("The Subsection four in Zone 2 of the Patient Banner.")]
        public Panel SubsectionFour
        {
            get
            {
                return this.subsectionFour;
            }
        }        

        /// <summary>
        /// Allergies present icon. Used when AllergyInformation is Present
        /// </summary>
        [Bindable(true), Category("Appearance"), Localizable(true)]
        [Description("Icon to be shown when Allergy information of the patient is present.")]
        [RefreshProperties(RefreshProperties.All)]
        public Image AllergiesPresentIcon
        {
            get
            {
                return this.allergiesRecordedIcon;
            }

            set
            {
                if (value != this.allergiesRecordedIcon)
                {
                    this.allergiesRecordedIcon = value;
                    this.NotifyPropertyChanged("AllergiesRecordedIcon");
                }
            }
        }

        /// <summary>
        /// Allergies not present icon. Used when AllergyInformation is either NotRecorded or NoneKnown
        /// </summary>
        [Bindable(true), Category("Appearance"), Localizable(true)]
        [Description("Icon to be shown when Allergy information of the patient is either NotRecorded or NoneKnown.")]
        [RefreshProperties(RefreshProperties.All)]
        public Image AllergiesNotPresentIcon
        {
            get
            {
                return this.allergiesNotRecordedIcon;
            }

            set
            {
                if (value != this.allergiesNotRecordedIcon)
                {
                    this.allergiesNotRecordedIcon = value;
                    this.NotifyPropertyChanged("AllergiesNotRecordedIcon");                    
                }
            }
        }

        /// <summary>
        /// Allergies Unavaialable icon. Used when AllergyInformation is Unavaialable.
        /// </summary>
        [Bindable(true), Category("Appearance"), Localizable(true)]
        [Description("Icon to be shown when Allergy information of the patient is Unavaialable.")]
        [RefreshProperties(RefreshProperties.All)]
        public Image AllergiesUnavailableIcon
        {
            get
            {
                return this.allergiesUnavailableIcon;
            }

            set
            {
                if (value != this.allergiesUnavailableIcon)
                {
                    this.allergiesUnavailableIcon = value;
                    this.NotifyPropertyChanged("AllergiesUnavailableIcon");
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the ViewAllAddresses link in address subsection. 
        /// </summary>
        /// <remarks>
        /// Defaults to "View all addresses". 
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue("View all addresses"), Localizable(true)]
        [Description("The text associated with the ViewAllAddresses link of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string ViewAllAddressesLinkText
        {
            get
            {
                return this.viewAllAddressesLink.Text;
            }

            set
            {
                if (value != this.viewAllAddressesLink.Text)
                {
                    this.viewAllAddressesLink.Text = value;
                    this.NotifyPropertyChanged("ViewAllAddressesLinkText");
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the ViewAllContactDetails link in contacts subsection. 
        /// </summary>
        /// <remarks>
        /// Defaults to "View all contact details". 
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue("View all contact details"), Localizable(true)]
        [Description("The text associated with the ViewAllContactDetails link of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string ViewAllContactDetailsLinkText
        {
            get
            {
                return this.viewAllContactDetailsLink.Text;
            }

            set
            {
                if (value != this.viewAllContactDetailsLink.Text)
                {
                    this.viewAllContactDetailsLink.Text = value;
                    this.NotifyPropertyChanged("ViewAllContactDetailsLinkText");
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the ViewAllergyRecord link in allergies subsection. 
        /// </summary>
        /// <remarks>
        /// Defaults to "View allergy record". 
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue("View allergy record"), Localizable(true)]
        [Description("The text associated with the ViewAllergyRecord link of the Patient Banner.")]
        [RefreshProperties(RefreshProperties.All)]
        public string ViewAllergyRecordLinkText
        {
            get
            {
                return this.viewAllergyRecordLink.Text;
            }

            set
            {
                if (value != this.viewAllergyRecordLink.Text)
                {
                    this.viewAllergyRecordLink.Text = value;
                    this.NotifyPropertyChanged("ViewAllergyRecordLinkText");
                }
            }
        }        

        /// <summary>
        /// Specifies the font
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Font Font
        {
            get
            {
                return base.Font;
            }
        }

        /// <summary>
        /// Specifies the font color
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
        /// Specifies the back color
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
        /// Specifies the background image
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
        }

        /// <summary>
        /// Specifies the layout of the background image
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
        }

        /// <summary>
        /// Specifies the border style
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new BorderStyle BorderStyle
        {
            get
            {
                return base.BorderStyle;
            }
        }

        /// <summary>
        /// Specifies the Right to left behavior
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
        }

        /// <summary>
        /// Specifies the cursor type to use when busy
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool UseWaitCursor
        {
            get
            {
                return base.UseWaitCursor;
            }
        }

        /// <summary>
        /// True if the patient is deceased
        /// </summary>
        internal bool IsDeceased
        {
            get
            {
                return (this.DateOfDeath != DateTime.MinValue);
            }
        }

        /// <summary>
        /// Default size of the patient banner
        /// </summary>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(this.Width, 90);
            }
        }

        /// <summary>
        /// AutoSize isn't supported 
        /// </summary>
        [Browsable(false)]
        [SuppressMessage("Microsoft.Usage", "CA2222:DoNotDecreaseInheritedMemberVisibility", Justification = "Required by spec.")]
        private new bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }

            set
            {
                base.AutoSize = value;
            }
        }

        /// <summary>
        /// AutoSizeMode isn't supported
        /// </summary>
        [Browsable(false)]
        [SuppressMessage("Microsoft.Usage", "CA2222:DoNotDecreaseInheritedMemberVisibility", Justification = "Required by spec.")]
        private new AutoSizeMode AutoSizeMode
        {
            get
            {
                return base.AutoSizeMode;
            }

            set
            {
                base.AutoSizeMode = value;
            }
        }        
        #endregion

        #region ISupportInitialize Members

        /// <summary>
        /// Signals to the PatientBanner that initialization is starting.  
        /// </summary>
        public virtual void BeginInit()
        {
            ((ISupportInitialize)this.identifierData).BeginInit();
        }

        /// <summary>
        /// Signals to the PatientBanner that initialization is complete. 
        /// </summary>
        public virtual void EndInit()
        {
            ((ISupportInitialize)this.identifierData).EndInit();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Causes a refresh of the patient banner controls.   
        /// </summary>
        public override void Refresh()
        {
            base.Refresh();

            this.patientName.Text = this.NameDisplayValue;
            this.btnGenderData.Text = this.genderData.Text;
            this.btnIdentifierLabel.Text = this.identifierData.Text;
                        
            if (this.ZoneTwoExpanded == true)
            {
                this.toggleDropButton.Image = this.dropDownImage;
            }
            else
            {
                this.toggleDropButton.Image = this.collapseImage;
            }

            this.patientPicture.Visible = (this.ImageDisplayed && this.patientPicture.Image != null) ? true : false;
            
            this.viewAllergyRecordLink.Visible = true;
            if (this.allergyInformation == AllergyInformation.NotRecorded || this.allergyInformation == AllergyInformation.NoneKnown)
            {
                this.allergyIcon.Image = this.allergiesNotRecordedIcon;                
            }
            else if (this.allergyInformation == AllergyInformation.Present)
            {
                this.allergyIcon.Image = this.allergiesRecordedIcon;                
            }
            else if (this.allergyInformation == AllergyInformation.Unavailable)
            {
                this.allergyIcon.Image = this.allergiesUnavailableIcon;
                this.viewAllergyRecordLink.Visible = false;
            }

            this.contactDetailsPreviewLabel.Text = this.phoneEmailData.GetContactDetailsSummary();
            this.allergiesInformationLabel.Text = PatientBanner.GetAllergyInformation(this.allergyInformation);
            this.SetAgeLabel();
            this.SetPreferredNameVisibility();            
            this.ApplyExtraStyles();
            this.SetLayout();
            this.SetBorder();

            if (this.allergyInformation == AllergyInformation.Present)
            {
                this.RenderAllergies();
                this.subsectionFive.Visible = true;
            }
            else
            {
                this.subsectionFive.Visible = false;
            }

            this.patientName.Focus();
        }
        #endregion
   
        #region Internal Methods

        /// <summary>
        /// Ensure that the height is set correctly after panel height change
        /// </summary>
        internal void AdjustControlHeight()
        {
            this.Height = this.borderPanel.Height;         
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
        /// Return display text based upon the value of specified date.
        /// </summary>
        /// <param name="dt">Date Considered</param>
        /// <returns>Display text</returns>
        private static string GetDateSpecificDisplay(NhsDate dt)
        {
            if (dt.DateValue != DateTime.MinValue)
            {
                return dt.ToString();
            }

            return PatientBannerControl.Resources.Unknown;
        }
        
        /// <summary>
        /// Triggers the GenderValueClick event
        /// </summary>
        /// <param name="e">event args</param>
        private void OnGenderValueClick(EventArgs e)
        {
            if (this.GenderValueClick != null)
            {
                this.GenderValueClick(this, e);
            }
        }

        /// <summary>
        /// Triggers the IdentifierClick event
        /// </summary>
        /// <param name="e">event args</param>
        private void OnIdentifierClick(EventArgs e)
        {
            if (this.IdentifierClick != null)
            {
                this.IdentifierClick(this, e);
            }
        }

        /// <summary>
        /// Triggers the ViewAllAddressesClick event
        /// </summary>
        /// <param name="e">Event args</param>
        private void OnViewAllAddressesClick(EventArgs e)
        {
            if (this.ViewAllAddressesClick != null)
            {
                this.ViewAllAddressesClick(this, e);
            }
        }

        /// <summary>
        /// Triggers the ViewAllContactDetailsClick event
        /// </summary>
        /// <param name="e">Event args</param>
        private void OnViewAllContactDetailsClick(EventArgs e)
        {
            if (this.ViewAllContactDetailsClick != null)
            {
                this.ViewAllContactDetailsClick(this, e);
            }
        }

        /// <summary>
        /// Triggers the ViewAllergyRecordClick event
        /// </summary>
        /// <param name="e">event args</param>
        private void OnViewAllergyRecordClick(EventArgs e)
        {
            if (this.ViewAllergyRecordClick != null)
            {
                this.ViewAllergyRecordClick(this, e);
            }
        }

        /// <summary>
        /// Set the appropriate border for Zone One
        /// </summary>
        private void ApplyZoneOneBorderColor()
        {
            if (this.IsDeceased)
            {
                this.borderPanel.BackColor = this.DeadPatientBorderColor;
            }
            else
            {
                this.borderPanel.BackColor = this.borderColor;
            }
        }        

        /// <summary>
        /// Raise property changed event
        /// </summary>
        /// <param name="propertyName">Property name</param>
        private void NotifyPropertyChanged(String propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Expand or contract the dropdown portion of the control
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void ToggleDropButton_Click(object sender, EventArgs e)
        {
            this.ZoneOneHeaderStripPanel_MouseLeave(this.zoneOneHeaderStripPanel, EventArgs.Empty);
            this.ZoneTwoExpanded = !this.ZoneTwoExpanded;
        }

        /// <summary>
        /// Change all fonts for the whole control
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void PatientBanner_FontChanged(object sender, EventArgs e)
        {
            this.ApplyGeneralFont(this);
        }

        /// <summary>
        /// Toggle the expansion
        /// </summary>
        /// <param name="expansion">true to expand, false to collapse</param>
        private void ToggleExpansion(bool expansion)
        {
            if (expansion == true)
            {
                this.toggleDropButton.Image = this.DropDownImage;
                this.zoneTwoPanel.Visible = true;
            }
            else
            {
                this.toggleDropButton.Image = this.CollapseImage;
                this.zoneTwoPanel.Visible = false;
            }

            this.PositionZoneTwoElements();
            this.SetBorder();
        }

        /// <summary>
        /// Apply/refresh the area font styling
        /// </summary>
        private void ApplyExtraStyles()
        {
            // Zone One Data
            if (this.ZoneOneDataFont != null)
            {
                this.dateOfDeathData.Font = this.ZoneOneDataFont;
                this.dateOfBirthData.Font = this.ZoneOneDataFont;
                this.genderData.Font = this.ZoneOneDataFont;
                this.btnGenderData.Font = this.ZoneOneDataFont;                
                this.identifierData.Font = this.ZoneOneDataFont;
                this.btnIdentifierLabel.Font = this.ZoneOneDataFont;
                this.preferredName.Font = this.ZoneOneDataFont;
                this.ageAtDeathLabel.Font = this.ZoneOneLabelFont;
                this.ageAtDeath.Font = this.ZoneOneDataFont;
            }

            if (this.patientNameFont != null)
            {
                this.patientName.Font = this.PatientNameFont;
            }

            // Zone One Labels
            if (this.ZoneOneLabelFont != null)
            {
                this.dateOfDeathLabel.Font = this.ZoneOneLabelFont;
                this.dateOfBirthLabel.Font = this.ZoneOneLabelFont;
                this.genderLabel.Font = this.ZoneOneLabelFont;
                this.identifierLabel.Font = this.ZoneOneLabelFont;
                this.preferredNameLabel.Font = this.ZoneOneLabelFont;
            }

            // Zone Two Data
            if (this.ZoneTwoDataFont != null)
            {
                this.phoneEmailData.DataFont = this.ZoneTwoDataFont;
                this.addressField.Font = this.ZoneTwoDataFont;
                this.addressField.AddressTypeLabelFont = this.ZoneTwoLabelFont;
                this.contactDetailsPreviewLabel.Font = this.ZoneTwoDataFont;
                this.viewAllergyRecordLink.Font = new Font(this.ZoneTwoLabelFont, FontStyle.Regular);
                this.viewAllContactDetailsLink.Font = new Font(this.ZoneTwoLabelFont, FontStyle.Regular);
                this.viewAllAddressesLink.Font = new Font(this.ZoneTwoLabelFont, FontStyle.Regular);

                foreach (Control child in this.subsectionFive.Controls)
                {
                    child.Font = this.ZoneTwoDataFont;
                }
            }

            // Zone Two Labels
            if (this.ZoneTwoLabelFont != null)
            {
                this.phoneEmailData.LabelFont = this.ZoneTwoLabelFont;
            }

            // Zone Two Titles
            if (this.ZoneTwoTitleFont != null)
            {
                this.subsectionTwoTitleLabel.Font = this.ZoneTwoTitleFont;
                this.subsectionOneTitleLabel.Font = this.ZoneTwoTitleFont;
                this.addressPreviewLabel.Font = this.zoneTwoDataFont;
                this.contactDetailsPreviewLabel.Font = this.zoneTwoDataFont;

                // Set data font only when allergies are present or None known
                if (this.allergyInformation == AllergyInformation.Present || this.allergyInformation == AllergyInformation.NoneKnown)
                {
                    this.allergiesInformationLabel.Font = this.zoneTwoDataFont;
                }
                else
                {
                    this.allergiesInformationLabel.Font = this.zoneTwoTitleFont;
                }

                this.subsectionThreeTitle.Font = this.ZoneTwoTitleFont;
                this.subsectionFourTitle.Font = this.ZoneTwoTitleFont;                
            }

            // Zone One Background Colour
            if (this.dateOfDeath.DateValue != DateTime.MinValue && this.DeadPatientBackColor != null)
            {
                this.zoneOnePanel.BackColor = this.DeadPatientBackColor;
            }
            else if (this.ZoneOneBackColor != null)
            {
                this.zoneOnePanel.BackColor = this.ZoneOneBackColor;
            }

            // Zone Two Title Background Colour
            if (this.ZoneTwoTitleBackColor != null)
            {
                this.zoneOneHeaderStripPanel.BackColor = this.ZoneTwoTitleBackColor;
            }

            // Zone Two Background Colour
            if (this.ZoneTwoBackColor != null)
            {
                this.zoneTwoPanel.BackColor = this.ZoneTwoBackColor;
            }

            this.ApplyZoneOneBorderColor();

            if (this.IsDeceased)
            {
                this.SetLayout();
            }
        }

        /// <summary>
        /// Apply the resource strings for the labels
        /// </summary>
        private void SetDefaultValues()
        {
            this.dateOfBirth = new NhsDate(DateTime.MinValue);
            this.dateOfDeath = new NhsDate(DateTime.MinValue);
            this.age = new NhsTimeSpan(this.dateOfBirth.DateValue, DateTime.Now);
            this.subsectionOneTitleLabel.Text = PatientBannerControl.Resources.SubsectionOneTitle;
            this.dateOfBirthLabel.Text = PatientBannerControl.Resources.DateOfBirthLabelText;
            this.dateOfDeathLabel.Text = PatientBannerControl.Resources.DateOfDeathLabelText;
            this.dateOfDeathData.Visible = false;
            this.dateOfDeathLabel.Visible = false;
            this.identifierLabel.Text = PatientBannerControl.Resources.IdentifierLabelText;
            this.subsectionTwoTitleLabel.Text = PatientBannerControl.Resources.SubsectionTwoTitle;
            this.genderLabel.Text = PatientBannerControl.Resources.GenderLabelText;
            this.ageAtDeathLabel.Text = PatientBannerControl.Resources.AgeAtDeathLabelText;
            this.preferredNameLabel.Text = PatientBannerControl.Resources.PreferredNameLabelText;
            this.addressField.AddressType = AddressLabelControl.Resources.AddressType;
            this.viewAllContactDetailsLink.Text = PatientBannerControl.Resources.ViewAllContactDetailsLinkText;
            this.viewAllergyRecordLink.Text = PatientBannerControl.Resources.ViewAllergyRecordLinkText;
            this.viewAllAddressesLink.Text = PatientBannerControl.Resources.ViewAllAddressLinkText;

            this.ApplyDefaultDeceasedPatientBackgroundImage();
            this.ApplyDefaultCollapsedImage();
            this.ApplyDefaultExpandedImage();
            this.ApplyZoneOneTooltip();
            this.ApplyZoneTwoTooltip();
            this.ApplyZoneOneBorderColor();
            this.ApplyDefaultAllergyIcons();
        }

        /// <summary>
        /// Custom reset for the patient image
        /// </summary>
        private void ResetPatientImage()
        {
            this.patientImageSet = false;
            this.ApplyDefaultPatientImage();
        }

        /// <summary>
        /// Custom reset for the patient image
        /// </summary>
        private void ResetDropDownImage()
        {            
            this.ApplyDefaultExpandedImage();
        }

        /// <summary>
        /// Custom reset for the patient image
        /// </summary>
        private void ResetCollapseImage()
        {
            this.ApplyDefaultCollapsedImage();
        }

        /// <summary>
        /// Custom reset for the ZoneOneDataFont
        /// </summary>
        private void ResetZoneOneDataFont()
        {
            this.ApplyZoneOneDataFont();
            this.Refresh();
        }

        /// <summary>
        /// Custom reset for the TopLineLabelFont
        /// </summary>
        private void ResetZoneOneLabelFont()
        {
            this.ApplyZoneOneLabelFont();
            this.Refresh();
        }

        /// <summary>
        /// Custom reset for the ZoneOneLabelWithTooltipFont
        /// </summary>
        private void ResetZoneOneLabelWithTooltipFont()
        {
            this.ApplyZoneOneLabelWithTooltipFont();
            this.Refresh();
        }

        /// <summary>
        /// Custom reset for the ZoneOneDataWithTooltipFont
        /// </summary>
        private void ResetZoneOneDataWithTooltipFont()
        {
            this.ApplyZoneOneDataWithTooltipFont();
            this.Refresh();
        }

        /// <summary>
        /// Custom reset for the ZoneTwoDataFont
        /// </summary>
        private void ResetZoneTwoDataFont()
        {
            this.ApplyZoneTwoDataFont();
            this.Refresh();
        }

        /// <summary>
        /// Custom reset for the ZoneTwoLabelFont
        /// </summary>
        private void ResetZoneTwoLabelFont()
        {
            this.ApplyZoneTwoLabelFont();
            this.Refresh();
        }

        /// <summary>
        /// Custom reset for the ZoneTwoTitleFont
        /// </summary>
        private void ResetZoneTwoTitleFont()
        {
            this.ApplyZoneTwoTitleFont();
            this.Refresh();
        }

        /// <summary>
        /// Custom reset for the PatientNameFont
        /// </summary>
        private void ResetPatientNameFont()
        {
            this.ApplyPatientNameFont();
            this.Refresh();
        }

        /// <summary>
        /// Custom reset for the ZoneOneDataWithTooltipFontColor
        /// </summary>
        private void ResetZoneOneDataWithTooltipFontColor()
        {
            this.ZoneOneDataWithTooltipFontColor = Color.Blue;
            this.Refresh();
        }

        /// <summary>
        /// Custom reset for the ZoneOneLabelWithTooltipFontColor
        /// </summary>
        private void ResetZoneOneLabelWithTooltipFontColor()
        {
            this.ZoneOneLabelWithTooltipFontColor = SystemColors.ControlText;
            this.Refresh();
        }

        /// <summary>
        /// Custom reset method for the identifier property
        /// </summary>
        private void ResetIdentifier()
        {
            this.IdentifierType = IdentifierType.Other;
            this.Identifier = string.Empty;
        }
        
        /// <summary>
        /// Custom reset method for the ZoneTwoHoverBorderColor property
        /// </summary>
        private void ResetZoneTwoHoverBorderColor()
        {
            this.ZoneTwoHoverBorderColor = SystemColors.MenuHighlight;
        }

        /// <summary>
        /// Custom reset method for the ZoneOneBorderColor property
        /// </summary>
        private void ResetZoneOneBackColor()
        {
            this.ZoneOneBackColor = SystemColors.Control;
        }

        /// <summary>
        /// Custom reset method for the ZoneTwoBorderColor property
        /// </summary>
        private void ResetZoneTwoBackColor()
        {
            this.ZoneTwoBackColor = SystemColors.Control;
        }

        /// <summary>
        /// Custom reset method for the ZoneTwoTitleBackColor property
        /// </summary>
        private void ResetZoneTwoTitleBackColor()
        {
            this.ZoneTwoTitleBackColor = SystemColors.Control;
        }

        /// <summary>
        /// Custom reset method for the DeadPatientBorderColor property
        /// </summary>
        private void ResetDeadPatientBorderColor()
        {
            this.DeadPatientBorderColor = Color.Black;
        }

        /// <summary>
        /// Custom reset method for the DeadPatientBackColor property
        /// </summary>
        private void ResetDeadPatientBackColor()
        {
            this.DeadPatientBackColor = SystemColors.ButtonShadow;
        }

        /// <summary>
        /// Custom reset method for the DeadPatientBackgroundImage property
        /// </summary>
        private void ResetDeadPatientBackgroundImage()
        {
            this.ApplyDefaultDeceasedPatientBackgroundImage();
            this.SetAgeLabel();
        }
        
        /// <summary>
        /// Custom reset method for the ZoneTwoToolTip property
        /// </summary>
        private void ResetZoneTwoToolTip()
        {
            this.ZoneTwoTooltip = PatientBannerControl.Resources.ZoneTwoToolTipText;
        }

        /// <summary>
        /// Explicit reset for border color property
        /// </summary>
        private void ResetBorderColor()
        {
            this.BorderColor = SystemColors.ControlDarkDark;
        }

        /// <summary>
        /// Indicates whether the property should be serialized
        /// </summary>
        /// <returns>returns true if the property needs to be serialized</returns>
        private bool ShouldSerializeActivePatientBackColor()
        {
            return this.ActivePatientBackColor != SystemColors.Control;
        }

        /// <summary>
        /// Explicit reset for border color property
        /// </summary>
        private void ResetActivePatientBackColor()
        {
            this.ActivePatientBackColor = SystemColors.Control;
        }
        
        /// <summary>
        /// Explicit reset for border color property
        /// </summary>
        private void ResetAllergiesNotPresentIcon()
        {
            this.AllergiesNotPresentIcon = this.allergyIconsImageList.Images[0];
        }

        /// <summary>
        /// Explicit reset for border color property
        /// </summary>
        private void ResetAllergiesPresentIcon()
        {
            this.AllergiesPresentIcon = this.allergyIconsImageList.Images[1];
        }

        /// <summary>
        /// Explicit reset for border color property
        /// </summary>
        private void ResetAllergiesUnavailableIcon()
        {
            this.AllergiesUnavailableIcon = this.allergyIconsImageList.Images[2];
        }

        /// <summary>
        /// Indicates whether the property should be serialized
        /// </summary>
        /// <returns>returns true if the property needs to be serialized</returns>
        private bool ShouldSerializeBorderColor()
        {
            return this.BorderColor != SystemColors.ControlDarkDark;
        }

        /// <summary>
        /// Indicates whether the property should be serialized
        /// </summary>
        /// <returns>returns true if the property needs to be serialized</returns>
        private bool ShouldSerializeAllergiesNotPresentIcon()
        {
            return this.AllergiesNotPresentIcon != this.allergyIconsImageList.Images[0];
        }

        /// <summary>
        /// Indicates whether the property should be serialized
        /// </summary>
        /// <returns>returns true if the property needs to be serialized</returns>
        private bool ShouldSerializeAllergiesPresentIcon()
        {
            return this.AllergiesPresentIcon != this.allergyIconsImageList.Images[1];
        }

        /// <summary>
        /// Indicates whether the property should be serialized
        /// </summary>
        /// <returns>returns true if the property needs to be serialized</returns>
        private bool ShouldSerializeAllergiesUnavailableIcon()
        {
            return this.AllergiesUnavailableIcon != this.allergyIconsImageList.Images[2];
        }        
                
        /// <summary>
        /// Determine whether or not to serialize the patient image
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeDropDownImage()
        {
            return !this.dropDownImage.Equals(this.toggleImageList.Images[1]);
        }

        /// <summary>
        /// Determine whether or not to serialize the patient image
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeCollapseImage()
        {
            return !this.collapseImage.Equals(this.toggleImageList.Images[0]);
        }

        /// <summary>
        /// Determine whether or not to serialize the ZoneOneDataFont
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeZoneOneDataFont()
        {
            return !this.ZoneOneDataFont.Equals(new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, (byte)0));
        }

        /// <summary>
        /// Determine whether or not to serialize the TopLineLabelFont
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeZoneOneLabelFont()
        {
            return !this.ZoneOneLabelFont.Equals(new Font("Arial", 9F, FontStyle.Italic, GraphicsUnit.Point, (byte)0));
        }

        /// <summary>
        /// Determine whether or not to serialize the ZoneTwoDataFont
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeZoneTwoDataFont()
        {
            return !this.ZoneTwoDataFont.Equals(new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, (byte)0));
        }

        /// <summary>
        /// Determine whether or not to serialize the ZoneTwoLabelFont
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeZoneTwoLabelFont()
        {
            return !this.ZoneTwoLabelFont.Equals(new Font("Arial", 9F, FontStyle.Italic, GraphicsUnit.Point, (byte)0));
        }

        /// <summary>
        /// Determine whether or not to serialize the ZoneTwoTitleFont
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeZoneTwoTitleFont()
        {
            return !this.ZoneTwoTitleFont.Equals(new Font("Arial", 9F, FontStyle.Italic, GraphicsUnit.Point, (byte)0));
        }

        /// <summary>
        /// Determine whether or not to serialize the PatientNameFont
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializePatientNameFont()
        {
            return !this.PatientNameFont.Equals(new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, (byte)0));
        }

        /// <summary>
        /// Determine whether or not to serialize the ZoneOneDataWithTooltipFont
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeZoneOneDataWithTooltipFont()
        {
            return !this.ZoneOneDataWithTooltipFont.Equals(new Font("Arial", 9F, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.Point, (byte)0));
        }

        /// <summary>
        /// Determine whether or not to serialize the ZoneOneDataWithTooltipFont
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeZoneOneLabelWithTooltipFontColor()
        {
            return !this.ZoneOneLabelWithTooltipFontColor.Equals(SystemColors.ControlText);
        }

        /// <summary>
        /// Determine whether or not to serialize the ZoneOneDataWithTooltipFont
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeZoneOneDataWithTooltipFontColor()
        {
            return !this.ZoneOneDataWithTooltipFontColor.Equals(Color.Blue);
        }

        /// <summary>
        /// Determine whether or not to serialize the ZoneOneDataWithTooltipFont
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeZoneOneLabelWithTooltipFont()
        {
            return !this.ZoneOneLabelWithTooltipFont.Equals(new Font("Arial", 9F, FontStyle.Italic, GraphicsUnit.Point, (byte)0));
        }

        /// <summary>
        /// Determine whether or not to serialize the ZoneTwoHoverBorderColor property
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeZoneTwoHoverBorderColor()
        {
            return this.ZoneTwoHoverBorderColor != SystemColors.MenuHighlight;
        }

        /// <summary>
        /// Determine whether or not to serialize the ZoneOneBorderColor property
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeZoneOneBackColor()
        {
            return this.ZoneOneBackColor != SystemColors.Control;
        }

        /// <summary>
        /// Determine whether or not to serialize the ZoneTwoBorderColor property
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeZoneTwoBackColor()
        {
            return this.ZoneTwoBackColor != SystemColors.Control;
        }

        /// <summary>
        /// Determine whether or not to serialize the ZoneTwoTitleBackColor property
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeZoneTwoTitleBackColor()
        {
            return this.ZoneTwoTitleBackColor != SystemColors.Control;
        }
        
        /// <summary>
        /// Determine whether or not to serialize the DeadPatientBorderColor property
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeDeadPatientBorderColor()
        {
            return this.DeadPatientBorderColor != Color.Black;
        }

        /// <summary>
        /// Determine whether or not to serialize the DeadPatientBackColor property
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeDeadPatientBackColor()
        {
            return this.DeadPatientBackColor != SystemColors.ButtonShadow;
        }

        /// <summary>
        /// Determine whether or not to serialize the DeadPatientBackgroundImage property
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeDeadPatientBackgroundImage()
        {
            return !this.DeadPatientBackgroundImage.Equals(Properties.Resources.Zone1DeadBackground);
        }

        /// <summary>
        /// Determine whether or not to serialize the ZoneTwoToolTip property
        /// </summary>
        /// <returns>True if the property value should be serialized, otherwise false</returns>
        private bool ShouldSerializeZoneTwoToolTip()
        {
            return this.ZoneTwoTooltip != PatientBannerControl.Resources.ZoneTwoToolTipText;
        }

        /// <summary>
        /// Apply a default image appropriate to PatientGender if not already set
        /// </summary>
        private void ApplyDefaultPatientImage()
        {
            if (this.genderData.Value == PatientGender.Female)
            {
                this.patientPicture.Image = this.genderImageList.Images[0];
            }
            else if (this.genderData.Value == PatientGender.Male)
            {
                this.patientPicture.Image = this.genderImageList.Images[1];
            }
            else
            {
                this.patientPicture.Image = this.genderImageList.Images[2];
            }            
        }

        /// <summary>
        /// Apply a default image to deceased patient background image
        /// </summary>
        private void ApplyDefaultDeceasedPatientBackgroundImage()
        {
            this.deceasedPatientBgImage = NhsCui.Toolkit.WinForms.Properties.Resources.Zone1DeadBackground;
        }

        /// <summary>
        /// Apply a default Image to CollapsedImage if not already set
        /// </summary>
        private void ApplyDefaultCollapsedImage()
        {
            this.collapseImage = this.toggleImageList.Images[0];
        }

        /// <summary>
        /// Apply a default Image to ExpandedImage if not already set
        /// </summary>
        private void ApplyDefaultExpandedImage()
        {
            this.dropDownImage = this.toggleImageList.Images[1];
        }
        
        /// <summary>
        /// Apply the default allergy icons
        /// </summary>
        private void ApplyDefaultAllergyIcons()
        {
            this.allergiesNotRecordedIcon = this.allergyIconsImageList.Images[0];
            this.allergiesRecordedIcon = this.allergyIconsImageList.Images[1];
            this.allergiesUnavailableIcon = this.allergyIconsImageList.Images[2];
        }

        /// <summary>
        /// Apply the default font for patient name
        /// </summary>
        private void ApplyPatientNameFont()
        {
            this.patientNameFont = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
        }

        /// <summary>
        /// Apply the default font for zone one data
        /// </summary>
        private void ApplyZoneOneDataFont()
        {
            this.zoneOneDataFont = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
        }

        /// <summary>
        /// Apply the default font for zone one labels
        /// </summary>
        private void ApplyZoneOneLabelFont()
        {
            this.zoneOneLabelFont = new Font("Arial", 9F, FontStyle.Italic, GraphicsUnit.Point, (byte)0);
        }

        /// <summary>
        /// Apply the default font for zone one data with tooltips
        /// </summary>
        private void ApplyZoneOneDataWithTooltipFont()
        {
            this.zoneOneDataWithTooltipFont = new Font("Arial", 9F, FontStyle.Underline | FontStyle.Bold, GraphicsUnit.Point, (byte)0);
        }

        /// <summary>
        /// Apply the default font for zone one labels with tooltips
        /// </summary>
        private void ApplyZoneOneLabelWithTooltipFont()
        {
            this.zoneOneLabelWithTooltipFont = new Font("Arial", 9F, FontStyle.Italic, GraphicsUnit.Point, (byte)0);
        }

        /// <summary>
        /// Apply the default font for zone two data
        /// </summary>
        private void ApplyZoneTwoDataFont()
        {
            this.zoneTwoDataFont = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
        }

        /// <summary>
        /// Apply the default font for zone two labels
        /// </summary>
        private void ApplyZoneTwoLabelFont()
        {
            this.zoneTwoLabelFont = new Font("Arial", 9F, FontStyle.Italic, GraphicsUnit.Point, (byte)0);
        }

        /// <summary>
        /// Apply the default font for zone two titles
        /// </summary>
        private void ApplyZoneTwoTitleFont()
        {
            this.zoneTwoTitleFont = new Font("Arial", 9F, FontStyle.Italic, GraphicsUnit.Point, (byte)0);
        }

        /// <summary>
        /// Apply the default fonts for patient banner
        /// </summary>
        private void ApplyFonts()
        {
            this.ApplyPatientNameFont();
            this.ApplyZoneOneLabelFont();
            this.ApplyZoneOneLabelWithTooltipFont();
            this.ApplyZoneOneDataFont();
            this.ApplyZoneOneDataWithTooltipFont();
            this.ApplyZoneTwoDataFont();
            this.ApplyZoneTwoLabelFont();
            this.ApplyZoneTwoTitleFont();
        }

        /// <summary>
        /// Walk all children and apply control-wide font unless explicitly set via specific font properties
        /// </summary>
        /// <param name="targetControl">Control to apply to</param>
        private void ApplyGeneralFont(Control targetControl)
        {
            foreach (Control childControl in targetControl.Controls)
            {
                childControl.Font = this.Font;
                if (childControl.Controls.Count > 0)
                {
                    this.ApplyGeneralFont(childControl);
                }
            }
        }

        /// <summary>
        /// Update the address preview snippet
        /// </summary>
        private void UpdateAddressPreview()
        {
            string paddedSeparator = AddressLabelControl.Resources.AddressItemSeparator + " ";
            StringBuilder addressPreview = new StringBuilder();

            if (this.addressField.Address1.Trim().Length != 0)
            {
                addressPreview.Append(this.addressField.Address1 + paddedSeparator);
            }

            if (this.addressField.Address2.Trim().Length != 0)
            {
                addressPreview.Append(this.addressField.Address2 + paddedSeparator);
            }

            if (this.addressField.Address3.Trim().Length != 0)
            {
                addressPreview.Append(this.addressField.Address3 + paddedSeparator);
            }

            if (this.addressField.Town.Trim().Length != 0)
            {
                addressPreview.Append(this.addressField.Town + paddedSeparator);
            }

            if (this.addressField.County.Trim().Length != 0)
            {
                addressPreview.Append(this.addressField.County + paddedSeparator);
            }

            if (this.addressField.Postcode.Trim().Length != 0)
            {
                addressPreview.Append(this.addressField.Postcode.ToUpperInvariant() + paddedSeparator);
            }

            if (this.addressField.Country.Trim().Length != 0)
            {
                addressPreview.Append(this.addressField.Country + paddedSeparator);
            }

            if (addressPreview.ToString().EndsWith(paddedSeparator, StringComparison.OrdinalIgnoreCase))
            {
                addressPreview.Remove(addressPreview.Length - 2, 2);
            }

            this.addressPreviewLabel.Text = addressPreview.ToString();
        }

        /// <summary>
        /// Set the tooltip for the contents of Zone One
        /// </summary>
        private void ApplyZoneOneTooltip()
        {   
            if (this.genderLabelTooltipText != null)
            {
                this.patientBannerToolTip.SetToolTip(this.genderLabel, this.genderLabelTooltipText);
            }

            if (this.genderValueTooltipText != null)
            {
                this.patientBannerToolTip.SetToolTip(this.btnGenderData, this.genderValueTooltipText);
            }

            if (this.identifierTooltipText != null)
            {
                this.patientBannerToolTip.SetToolTip(this.btnIdentifierLabel, this.identifierTooltipText);
            }

            if (this.identifierLabelTooltipText != null)
            {
                this.patientBannerToolTip.SetToolTip(this.identifierLabel, this.identifierLabelTooltipText);
            }

            this.patientBannerToolTip.SetToolTip(this.DeceasedPatientTransparentIcon, PatientBannerControl.Resources.DeceasedPatientTransparentIconTooltip);
        }

        /// <summary>
        /// Set the tooltip for the contents of Zone Two
        /// </summary>
        private void ApplyZoneTwoTooltip()
        {
            if (this.ZoneTwoTooltip == null)
            {
                return;
            }

            this.patientBannerToolTip.SetToolTip(this.zoneOneHeaderStripPanel, this.ZoneTwoTooltip);

            foreach (Control child in this.zoneOneHeaderStripPanel.Controls)
            {
                this.patientBannerToolTip.SetToolTip(child, this.ZoneTwoTooltip);
            }
        }        

        /// <summary>
        /// Loads the patient banner control. Initializes the Layout of all the controls
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event args</param>
        private void PatientBanner_Load(object sender, EventArgs e)
        {
            this.Refresh();
            this.SetLayout();
            this.SetBorder();
            this.Size = new Size(this.Width, this.borderPanel.Height);            
        }
        
        /// <summary>
        /// Set the control layout
        /// </summary>
        private void SetLayout()
        {
            this.zoneOneHeaderStripPanel.Width = this.ClientRectangle.Width - this.borderWidth;
            this.zoneTwoPanel.Width = this.ClientRectangle.Width - this.borderWidth;

            int largestControlHeight = 0;
            
            this.PositionZoneOneElements();          
           
            largestControlHeight = 0;

            foreach (Control child in this.zoneOneHeaderStripPanel.Controls)
            {
                child.Height = child.PreferredSize.Height;
                child.Top = InternalControlPadding;
                if (child.Height > largestControlHeight)
                {
                    largestControlHeight = child.Height;
                }
            }

            if (largestControlHeight > 0)
            {
                this.zoneOneHeaderStripPanel.Height = largestControlHeight + 2 * PatientBanner.InternalControlPadding;
            }

            // Align controls to bottom of panel
            foreach (Control child in this.zoneOneHeaderStripPanel.Controls)
            {
                if (child.Height + InternalControlPadding < largestControlHeight)
                {
                    child.Top = this.zoneOneHeaderStripPanel.Height - child.Height - InternalControlPadding;
                }                
            }
            
            // Adding 1pt extra so as to create a thin border between zone one and two
            this.zoneOneHeaderStripPanel.Top = this.borderWidth + this.zoneOnePanel.Height + 1;
            this.zoneOneHeaderStripPanel.Left = this.zoneOnePanel.Left;
            this.zoneOneHeaderStripPanel.Width = this.zoneOnePanel.Width;

            // Adding 2pt extra so as to create a thin border between zone two permanent and non permanent            
            this.zoneTwoPanel.Top = this.borderWidth + this.zoneOnePanel.Height + this.zoneOneHeaderStripPanel.Height + 2;
            this.zoneTwoPanel.Left = this.zoneOnePanel.Left;
            this.zoneTwoPanel.Width = this.zoneOnePanel.Width;

            this.SetZoneTwoHeight();            
            this.PositionZoneTwoElements();
            this.SetBorder();
        }

        /// <summary>
        /// Position the elements in Zone one.
        /// </summary>
        private void PositionZoneOneElements()
        {
            this.zoneOnePanel.Location = new Point(this.borderWidth, this.borderWidth);
            this.zoneOnePanel.Width = this.ClientRectangle.Width - 2 * this.borderWidth;
            this.zoneOnePanel.MinimumSize = new Size(0, 50);
            this.btnIdentifierLabel.Size = this.btnIdentifierLabel.PreferredSize;
            this.ageAtDeath.Size = this.ageAtDeath.PreferredSize;

            int patientNameLeft = InternalControlPadding;
            if (this.patientPicture.Visible == true)
            {
                this.patientPicture.Top = InternalControlPadding;
                this.patientPicture.Left = this.zoneOnePanel.Left + InternalControlPadding;
                patientNameLeft = this.patientPicture.Right + InternalControlPadding;
            }

            if (this.IsDeceased)
            {
                this.DeceasedPatientTransparentIcon.Visible = true;
                this.DeceasedPatientTransparentIcon.Left = patientNameLeft;
                patientNameLeft += 1;
            }
            else
            {
                this.DeceasedPatientTransparentIcon.Visible = false;
            }

            this.patientName.Left = patientNameLeft;
            this.patientName.AutoSize = true;
            this.patientName.MaximumSize = new Size(this.zoneOnePanel.ClientRectangle.Width - patientNameLeft, 0);

            int patientNameWidth = this.patientName.PreferredWidth;
            if (!string.IsNullOrEmpty(this.preferredNameText))
            {
                patientNameWidth = patientNameWidth > (this.preferredNameLabel.PreferredWidth + InternalControlPadding + this.preferredName.PreferredWidth) ? patientNameWidth : (this.preferredNameLabel.PreferredWidth + InternalControlPadding + this.preferredName.PreferredWidth);
            }

            bool showDataInOneLine = true;
            this.patientDataPanel.MaximumSize = new Size(0, 0);

            if (this.patientPicture.Visible == false)
            {
                if (patientNameWidth + this.patientDataPanel.PreferredSize.Width > this.zoneOnePanel.ClientRectangle.Width)
                {
                    showDataInOneLine = false;
                }
            }
            else
            {
                if (this.patientPicture.Width + patientNameWidth + this.patientDataPanel.PreferredSize.Width > this.zoneOnePanel.ClientRectangle.Width)
                {
                    showDataInOneLine = false;
                }
            }

            this.patientName.Top = InternalControlPadding;
            if (showDataInOneLine)
            {
                if (string.IsNullOrEmpty(this.preferredNameText))                
                {
                    this.patientName.Top = (this.zoneOnePanel.Height - this.patientName.Height) / 2;
                }
            }
                        
            this.preferredNameLabel.Top = this.patientName.Bottom + InternalControlPadding;
            this.preferredNameLabel.Left = patientNameLeft;
            this.preferredNameLabel.AutoEllipsis = true;
            this.preferredNameLabel.Height = this.preferredNameLabel.PreferredHeight;

            if (this.preferredNameLabel.PreferredWidth < this.zoneOnePanel.ClientRectangle.Width)
            {
                this.preferredNameLabel.Size = this.preferredNameLabel.PreferredSize;
                this.preferredName.Top = this.preferredNameLabel.Top;
                this.preferredName.Left = this.preferredNameLabel.Right + InternalControlPadding;
                this.preferredName.Height = this.preferredName.PreferredHeight;

                if (this.preferredName.PreferredWidth > this.zoneOnePanel.ClientRectangle.Width - this.preferredName.Left)
                {
                    this.preferredName.Width = this.zoneOnePanel.ClientRectangle.Width - this.preferredName.Left;
                    this.preferredName.AutoEllipsis = true;
                }
                else
                {
                    this.preferredName.Width = this.preferredName.PreferredWidth;                    
                }
            }
            else
            {
                this.preferredNameLabel.Width = this.zoneOnePanel.ClientRectangle.Width - patientNameLeft - InternalControlPadding;
                this.preferredName.Visible = false;
            }

            this.zoneOnePanel.Height = this.preferredName.Bottom + InternalControlPadding;

            this.patientDataPanel.Size = this.patientDataPanel.PreferredSize;

            // Check whether Born, Gender and Nhs Number can be shown in same line as patient name
            if (this.patientDataPanel.PreferredSize.Width > this.zoneOnePanel.ClientRectangle.Width)
            {
                this.patientDataPanel.MaximumSize = new Size(this.zoneOnePanel.ClientRectangle.Width, 0);
                this.patientDataPanel.Left = this.zoneOnePanel.ClientRectangle.Width - this.patientDataPanel.PreferredSize.Width;
            }            

            int patientDataTop = InternalControlPadding;
            if (showDataInOneLine)
            {
                this.patientDataPanel.Size = this.patientDataPanel.PreferredSize;
                this.patientDataPanel.Left = this.zoneOnePanel.ClientRectangle.Width - this.patientDataPanel.Width - InternalControlPadding;

                if (!this.IsDeceased)
                {
                    patientDataTop = (this.zoneOnePanel.Height - this.dateOfBirthData.Height) / 2;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.preferredNameText))
                {
                    patientDataTop = this.patientName.Bottom;
                }
                else
                {
                    patientDataTop = this.preferredNameLabel.Bottom > this.preferredName.Bottom ? this.preferredNameLabel.Bottom : this.preferredName.Bottom;
                }

                // PS # 6588.
                if (this.patientPicture.Visible && this.patientPicture.Bottom > patientDataTop)
                {
                    this.patientDataPanel.MaximumSize = new Size(this.zoneOnePanel.ClientRectangle.Width - this.patientPicture.Right - InternalControlPadding, 0);
                    this.patientDataPanel.Left = this.zoneOnePanel.ClientRectangle.Width - this.patientDataPanel.PreferredSize.Width - InternalControlPadding;
                }
                else
                {
                    this.patientDataPanel.MaximumSize = new Size(this.zoneOnePanel.ClientRectangle.Width, 0);
                    this.patientDataPanel.Left = this.zoneOnePanel.ClientRectangle.Width - this.patientDataPanel.PreferredSize.Width - InternalControlPadding;
                }
                                
                patientDataTop += InternalControlPadding;
            }

            this.patientDataPanel.Controls.SetChildIndex(this.dateOfDeathData, 9);
            this.patientDataPanel.Controls.SetChildIndex(this.dateOfDeathLabel, 8);
            
            this.patientDataPanel.Size = this.patientDataPanel.PreferredSize;
            this.patientDataPanel.Top = patientDataTop;
            this.patientDataPanel.Padding = new Padding(0);
            
            this.zoneOnePanel.Height = this.zoneOnePanel.Height > this.patientDataPanel.Bottom + InternalControlPadding ? this.zoneOnePanel.Height : this.patientDataPanel.Bottom + InternalControlPadding;

            if (this.IsDeceased)
            {            
                this.dateOfDeathLabel.Visible = true;
                this.dateOfDeathData.Visible = true;
                this.ageAtDeath.Visible = true;
                this.ageAtDeathLabel.Visible = true;
                this.patientDataPanel.BackColor = System.Drawing.Color.Transparent;
            }
            else
            {
                this.dateOfDeathLabel.Visible = false;
                this.dateOfDeathData.Visible = false;
                this.ageAtDeath.Visible = false;
                this.ageAtDeathLabel.Visible = false;
                this.patientDataPanel.BackColor = this.zoneOnePanel.BackColor;
            }

            if (this.btnIdentifierLabel.Right < this.patientDataPanel.Width)
            {
                this.btnIdentifierLabel.Height = this.btnIdentifierLabel.PreferredHeight;
                this.btnIdentifierLabel.Width = this.patientDataPanel.Width - this.btnIdentifierLabel.Left;
                this.btnIdentifierLabel.AutoSize = false;
                this.btnIdentifierLabel.Height = this.btnIdentifierLabel.PreferredHeight;
                this.btnIdentifierLabel.Width = this.patientDataPanel.Width - this.btnIdentifierLabel.Left;
            }

            if (this.ageAtDeath.Visible)
            {
                if (this.ageAtDeath.PreferredWidth < this.patientDataPanel.Width - this.ageAtDeath.Left)
                {
                    this.ageAtDeath.Height = this.ageAtDeath.PreferredHeight;
                    this.ageAtDeath.Width = this.patientDataPanel.Width - this.ageAtDeath.Left;
                    this.ageAtDeath.AutoSize = false;
                    this.ageAtDeath.Height = this.ageAtDeath.PreferredHeight;
                    this.ageAtDeath.Width = this.patientDataPanel.Width - this.ageAtDeath.Left;
                }
            } 
        }

        /// <summary>
        /// Position the elements in Zone two. Permanent and non permanent sections
        /// </summary>
        private void PositionZoneTwoElements()
        {
            int separatorBorderHeight = this.zoneOneHeaderStripPanel.Height;
            if (this.ZoneTwoExpanded)
            {
                separatorBorderHeight = this.zoneTwoPanel.Bottom - this.zoneOneHeaderStripPanel.Top;
            }            
            
            int separatorBorderWidth = 1;

            // Subsection one
            this.addressField.Left = 0;
            this.addressField.Width = this.SubsectionOneWidth;
            this.subsectionOneTitleLabel.Left = 1;

            if (this.subsectionOneTitleLabel.PreferredWidth > this.SubsectionOneWidth)
            {
                this.subsectionOneTitleLabel.Width = this.SubsectionOneWidth;
                this.addressPreviewLabel.Visible = false;
            }
            else
            {
                this.subsectionOneTitleLabel.Width = this.subsectionOneTitleLabel.PreferredWidth;
                this.addressPreviewLabel.Visible = true;
                this.addressPreviewLabel.Left = this.subsectionOneTitleLabel.Right + PatientBanner.InternalControlPadding;
                this.addressPreviewLabel.Width = this.SubsectionOneWidth - this.addressPreviewLabel.Left;
            }            

            this.subsectionOneBorder.BackColor = this.borderColor;
            this.subsectionOneBorder.Location = new Point(this.SubsectionOneWidth + this.borderWidth, this.zoneOneHeaderStripPanel.Top);
            this.subsectionOneBorder.Width = separatorBorderWidth;
            this.subsectionOneBorder.Height = separatorBorderHeight;

            // Subsection two
            this.phoneEmailData.Left = this.subsectionOneBorder.Right + 1 - this.borderWidth;
            this.phoneEmailData.Width = this.SubsectionTwoWidth;
            this.subsectionTwoTitleLabel.Left = this.phoneEmailData.Left;

            if (this.subsectionTwoTitleLabel.PreferredWidth > this.SubsectionTwoWidth)
            {
                this.subsectionTwoTitleLabel.Width = this.SubsectionTwoWidth;
                this.contactDetailsPreviewLabel.Visible = false;
            }
            else
            {
                this.contactDetailsPreviewLabel.Visible = true;
                this.subsectionTwoTitleLabel.Width = this.subsectionTwoTitleLabel.PreferredWidth;
                this.contactDetailsPreviewLabel.Left = this.subsectionTwoTitleLabel.Right + PatientBanner.InternalControlPadding;
                this.contactDetailsPreviewLabel.Width = this.SubsectionTwoWidth - this.subsectionTwoTitleLabel.Width - InternalControlPadding;
            }            

            this.subsectionTwoBorder.BackColor = this.borderColor;
            this.subsectionTwoBorder.Location = new Point(this.SubsectionOneWidth + this.SubsectionTwoWidth + this.borderWidth, this.zoneOneHeaderStripPanel.Top);
            this.subsectionTwoBorder.Width = separatorBorderWidth;
            this.subsectionTwoBorder.Height = separatorBorderHeight;

            // Subsection Three
            this.subsectionThree.Left = this.subsectionTwoBorder.Right + 1 - this.borderWidth;
            this.subsectionThree.Width = this.SubsectionThreeWidth;
            this.subsectionThreeTitle.Left = this.subsectionThree.Left;
            this.subsectionThreeTitle.Width = this.subsectionThree.Width;

            this.subsectionThreeBorder.BackColor = this.borderColor;
            this.subsectionThreeBorder.Location = new Point(this.SubsectionOneWidth + this.SubsectionTwoWidth + this.SubsectionThreeWidth + this.borderWidth, this.zoneOneHeaderStripPanel.Top);
            this.subsectionThreeBorder.Width = separatorBorderWidth;
            this.subsectionThreeBorder.Height = separatorBorderHeight;

            // Subsection Four
            this.subsectionFour.Left = this.subsectionThreeBorder.Right + 1 - this.borderWidth;
            this.subsectionFour.Width = this.SubsectionFourWidth;
            this.subsectionFourTitle.Left = this.subsectionFour.Left;
            this.subsectionFourTitle.Width = this.subsectionFour.Width;

            this.subsectionFourBorder.BackColor = this.borderColor;
            this.subsectionFourBorder.Location = new Point(this.SubsectionOneWidth + this.SubsectionTwoWidth + this.SubsectionThreeWidth + this.SubsectionFourWidth + this.borderWidth, this.zoneOneHeaderStripPanel.Top);
            this.subsectionFourBorder.Width = separatorBorderWidth;
            this.subsectionFourBorder.Height = separatorBorderHeight;

            // Subsection Five
            this.subsectionFive.Left = this.subsectionFourBorder.Right + 1 - this.borderWidth;
            this.subsectionFive.Width = this.zoneTwoPanel.ClientRectangle.Width - this.SubsectionOneWidth - this.SubsectionTwoWidth - this.SubsectionThreeWidth - this.SubsectionFourWidth;
            this.allergyIcon.Left = this.subsectionFive.Left;
            this.allergiesInformationLabel.Left = this.allergyIcon.Right + PatientBanner.InternalControlPadding;
            this.allergiesInformationLabel.Width = this.SubsectionFiveWidth - this.allergyIcon.Width - this.toggleDropButton.Width - 10 * InternalControlPadding;

            this.expandCollpaseImageBorder.BackColor = this.borderColor;
            this.expandCollpaseImageBorder.Location = new Point(this.allergiesInformationLabel.Right + this.borderWidth + InternalControlPadding, this.zoneOneHeaderStripPanel.Top);
            this.expandCollpaseImageBorder.Width = separatorBorderWidth;
            this.expandCollpaseImageBorder.Height = this.zoneOneHeaderStripPanel.Height;
            this.toggleDropButton.Left = this.expandCollpaseImageBorder.Right + InternalControlPadding - this.borderWidth;
                       
            if (this.viewAllAddressesLink.Width > this.SubsectionOneWidth)
            {
                this.viewAllAddressesLink.Left = this.addressField.Left;
            }
            else
            {
                this.viewAllAddressesLink.Left = this.subsectionOneBorder.Left - this.viewAllAddressesLink.Width - PatientBanner.InternalControlPadding - this.borderWidth;
            }
                      
            if (this.viewAllContactDetailsLink.Width > this.SubsectionTwoWidth)
            {
                this.viewAllContactDetailsLink.Left = this.phoneEmailData.Left;
            }
            else
            {
                this.viewAllContactDetailsLink.Left = this.subsectionTwoBorder.Left - this.viewAllContactDetailsLink.Width - PatientBanner.InternalControlPadding - this.borderWidth;
            }
                      
            if (this.viewAllergyRecordLink.Width > this.SubsectionFiveWidth)
            {
                this.viewAllergyRecordLink.Left = this.subsectionFive.Left;
            }
            else
            {
                this.viewAllergyRecordLink.Left = this.zoneTwoPanel.ClientRectangle.Right - this.viewAllergyRecordLink.Width - PatientBanner.InternalControlPadding - this.borderWidth;
            }

            this.viewAllAddressesLink.Top = this.zoneTwoPanel.Height - this.viewAllAddressesLink.Height - PatientBanner.InternalControlPadding;
            this.viewAllContactDetailsLink.Top = this.zoneTwoPanel.Height - this.viewAllContactDetailsLink.Height - PatientBanner.InternalControlPadding;
            this.viewAllergyRecordLink.Top = this.zoneTwoPanel.Height - this.viewAllergyRecordLink.Height - PatientBanner.InternalControlPadding;
        }

        /// <summary>
        /// Sets the border for the control
        /// </summary>
        private void SetBorder()
        {
            this.borderPanel.Location = new Point(0, 0);
            this.borderPanel.Width = this.ClientRectangle.Width;
            this.borderPanel.Height = this.borderWidth + this.zoneOnePanel.Height + 1 + this.zoneOneHeaderStripPanel.Height;

            if (this.ZoneTwoExpanded)
            {
                this.borderPanel.Height += this.zoneTwoPanel.Height + 1;
            }

            this.borderPanel.Height += this.borderWidth;
            this.AdjustControlHeight();

            this.borderPanel.SendToBack();
        }

        /// <summary>
        /// Set ZoneTwo height based on the height of subsections
        /// </summary>
        private void SetZoneTwoHeight()
        {
            int height = 0;
            int subsectionOneHeight;
            int subsectionTwoHeight;

            // Set SubsectionOnePanel Height
            height = this.addressField.PreferredSize.Height;
            
            this.addressField.Height = height;
            subsectionOneHeight = height;

            // Set SubsectionTwoPanel Height
            height = this.phoneEmailData.PreferredSize.Height;

            this.phoneEmailData.Height = height;
            subsectionTwoHeight = height;

            height = subsectionOneHeight > subsectionTwoHeight ? subsectionOneHeight : subsectionTwoHeight;
                        
            this.subsectionThree.Height = height;
            this.subsectionFour.Height = height;
            this.subsectionFive.Height = height;

            this.viewAllAddressesLink.MaximumSize = new Size(this.SubsectionOneWidth - InternalControlPadding, 0);
            this.viewAllAddressesLink.AutoSize = true;

            this.viewAllContactDetailsLink.MaximumSize = new Size(this.SubsectionTwoWidth - InternalControlPadding, 0);
            this.viewAllContactDetailsLink.AutoSize = true;

            this.viewAllergyRecordLink.MaximumSize = new Size(this.SubsectionFiveWidth - InternalControlPadding, 0);
            this.viewAllergyRecordLink.AutoSize = true;

            int linksHeight = this.viewAllAddressesLink.Height > this.viewAllContactDetailsLink.Height ? this.viewAllAddressesLink.Height : this.viewAllContactDetailsLink.Height;

            if (this.viewAllergyRecordLink.Visible)
            {
                linksHeight = linksHeight > this.viewAllergyRecordLink.Height ? linksHeight : this.viewAllergyRecordLink.Height;
            }

            this.zoneTwoPanel.Height = height + linksHeight + 2 * InternalControlPadding;
        } 

        /// <summary>
        /// Activate the hover border on Zone Two header strip
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void ZoneOneHeaderStripPanel_MouseEnter(object sender, EventArgs e)
        {
            Graphics g = this.zoneOneHeaderStripPanel.CreateGraphics();
            g.DrawRectangle(new Pen(this.zoneTwoHoverBorderColor, 2), this.zoneOneHeaderStripPanel.ClientRectangle);
        }

        /// <summary>
        /// Deactivate the hover border on Zone Two header strip
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void ZoneOneHeaderStripPanel_MouseLeave(object sender, EventArgs e)
        {
            Graphics g = this.zoneOneHeaderStripPanel.CreateGraphics();
            g.DrawRectangle(new Pen(this.zoneOneHeaderStripPanel.BackColor, 2), this.zoneOneHeaderStripPanel.ClientRectangle);
        }
        
        /// <summary>
        /// Show border and Apply the font ZoneOneLabelWithTooltipFont
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void GenderLabel_MouseEnter(object sender, EventArgs e)
        {
            this.genderLabel.Padding = new Padding(0);
            this.genderLabel.BorderStyle = BorderStyle.FixedSingle;
            this.genderLabel.Font = this.ZoneOneLabelWithTooltipFont;
            this.genderLabel.ForeColor = this.ZoneOneLabelWithTooltipFontColor;
        }

        /// <summary>
        /// Hide border and Apply the default font
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void GenderLabel_MouseLeave(object sender, EventArgs e)
        {
            this.genderLabel.Padding = new Padding(1);
            this.genderLabel.BorderStyle = BorderStyle.None;
            this.genderLabel.Font = this.ZoneOneLabelFont;
            this.genderLabel.ForeColor = this.dateOfBirthData.ForeColor;
        }

        /// <summary>
        /// Underline the data when tooltip is present
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void GenderValue_MouseEnter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.GenderValueTooltip) || (this.GenderValueClick != null))
            {
                this.btnGenderData.Font = this.zoneOneDataWithTooltipFont;
                this.btnGenderData.LinkColor = this.zoneOneDataWithTooltipFontColor;
            }
        }

        /// <summary>
        /// Remove the underline / apply the default font
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void GenderValue_MouseLeave(object sender, EventArgs e)
        {
            this.btnGenderData.Font = this.zoneOneDataFont;
            this.btnGenderData.LinkColor = this.dateOfBirthData.ForeColor;
        }

        /// <summary>
        /// Show border and Apply the font ZoneOneLabelWithTooltipFont
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void IdentifierLabel_MouseEnter(object sender, EventArgs e)
        {
            this.identifierLabel.Padding = new Padding(0);
            this.identifierLabel.BorderStyle = BorderStyle.FixedSingle;
            this.identifierLabel.Font = this.ZoneOneLabelWithTooltipFont;
            this.identifierLabel.ForeColor = this.ZoneOneLabelWithTooltipFontColor;
        }

        /// <summary>
        /// Hide border and apply the default font
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void IdentifierLabel_MouseLeave(object sender, EventArgs e)
        {
            this.identifierLabel.Padding = new Padding(1);
            this.identifierLabel.BorderStyle = BorderStyle.None;
            this.identifierLabel.Font = this.ZoneOneLabelFont;
            this.identifierLabel.ForeColor = this.dateOfBirthData.ForeColor;
        }

        /// <summary>
        /// Underline the data when tooltip is present
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void Identifier_MouseEnter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.IdentifierTooltip) || (this.IdentifierClick != null))
            {
                this.btnIdentifierLabel.Font = this.zoneOneDataWithTooltipFont;
                this.btnIdentifierLabel.LinkColor = this.zoneOneDataWithTooltipFontColor;
            }                      
        }

        /// <summary>
        /// Remove the underline / apply the default font
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void Identifier_MouseLeave(object sender, EventArgs e)
        {
            this.btnIdentifierLabel.Font = this.zoneOneDataFont;
            this.btnIdentifierLabel.LinkColor = this.dateOfBirthData.ForeColor;
        }

        /// <summary>
        /// Fire the gender click event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void GenderValue_Click(object sender, EventArgs e)
        {
            this.OnGenderValueClick(e);
        }

        /// <summary>
        /// Fire the identifier click event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void Identifier_Click(object sender, EventArgs e)
        {
            this.OnIdentifierClick(e);
        }

        /// <summary>
        /// Fire the View all addresses click event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void ViewAllAddresses_Click(object sender, EventArgs e)
        {
            this.OnViewAllAddressesClick(e);
        }

        /// <summary>
        /// Fire the View all phone numbers click event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void ViewAllContactDetails_Click(object sender, EventArgs e)
        {
            this.OnViewAllContactDetailsClick(e);
        }

        /// <summary>
        /// Fire the View allergy record click event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void ViewAllergyRecord_Click(object sender, EventArgs e)
        {
            this.OnViewAllergyRecordClick(e);
        }
                       
        /// <summary>
        /// Render Allergies
        /// </summary>
        private void RenderAllergies()
        {
            this.subsectionFive.Controls.Clear();
            if (this.patientAllergies != null && this.patientAllergies.Count > 0 && this.AllergyInformation == AllergyInformation.Present)
            {
                bool showAllAllergies = this.patientAllergies.Count > PatientBanner.MaxAllergiesShown ? false : true;

                int allergiesShownCount = showAllAllergies == true ? this.patientAllergies.Count : PatientBanner.MaxAllergiesShown - 1;
                
                int i = 0;
                int top = 0;
                for (; i < allergiesShownCount; i++)
                {
                    Label allergyLabel = new Label();
                    allergyLabel.AutoSize = true;
                    allergyLabel.Padding = new Padding(0);
                    allergyLabel.Margin = new Padding(0);
                    allergyLabel.Font = this.zoneTwoDataFont;
                    allergyLabel.Name = "allergyLabel" + i.ToString(CultureInfo.CurrentCulture);
                    allergyLabel.Text = this.patientAllergies[i].ToString();
                    allergyLabel.MaximumSize = new Size(this.subsectionFive.Width - InternalControlPadding, 0);
                    allergyLabel.Size = allergyLabel.PreferredSize;                  
                                        
                    this.subsectionFive.Controls.Add(allergyLabel);
                    allergyLabel.Location = new Point(0, top);
                    top = allergyLabel.Bottom + PatientBanner.InternalControlPadding;
                }                

                if (showAllAllergies == false)
                {
                    Label moreAllergiesLabel = new Label();
                    moreAllergiesLabel.AutoSize = true;
                    moreAllergiesLabel.Text = PatientBannerControl.Resources.MoreAllergiesPresent;
                    if (i > 0)
                    {
                        moreAllergiesLabel.Top = top;
                    }

                    this.subsectionFive.Controls.Add(moreAllergiesLabel);                    
                }
            }
        }

        /// <summary>
        /// Sets the age
        /// </summary>
        private void SetAgeLabel()
        {            
            this.age.From = this.dateOfBirth.DateValue;
            this.age.IsAge = true;
            if (this.dateOfBirth.DateValue != DateTime.MinValue)
            {
                if (this.dateOfDeath.DateValue != DateTime.MinValue)
                {
                    this.age.To = this.dateOfDeath.DateValue;
                }
                else
                {
                    this.age.To = DateTime.Now;
                }
            }
            else
            {
                this.age.To = this.age.From;
            }

            if (this.IsDeceased)
            {
                this.dateOfDeathData.Text = this.dateOfDeath.ToString();
                this.dateOfBirthData.Text = GetDateSpecificDisplay(this.dateOfBirth);
                this.ageAtDeath.Text = this.age.ToString(TimeSpanUnitLength.Short, CultureInfo.CurrentCulture);
                this.zoneOnePanel.BackgroundImage = this.DeadPatientBackgroundImage;
                this.zoneOnePanel.BackgroundImageLayout = ImageLayout.Tile;
                this.zoneOnePanel.BackColor = this.deadPatientBackColor;
                this.DeceasedPatientTransparentIcon.Visible = true;
            }
            else
            {
                this.dateOfBirthData.Text = GetDateSpecificDisplay(this.dateOfBirth);
                if (this.dateOfBirth.DateValue != DateTime.MinValue)
                {
                    this.dateOfBirthData.Text = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", this.dateOfBirth.ToString(), this.age.ToString(TimeSpanUnitLength.Short, CultureInfo.CurrentCulture));                    
                }                

                this.dateOfDeathData.Text = string.Empty;
                this.zoneOnePanel.BackgroundImageLayout = ImageLayout.None;
                this.zoneOnePanel.BackgroundImage = null;
                this.zoneOnePanel.BackColor = Color.Empty;
                this.DeceasedPatientTransparentIcon.Visible = true;
            }
        }

        /// <summary>
        /// Shows/Hides the preferred name and its label when preferred name is empty
        /// </summary>
        private void SetPreferredNameVisibility()
        {
            if (string.IsNullOrEmpty(this.preferredName.Text))
            {
                this.preferredName.Visible = false;
                this.preferredNameLabel.Visible = false;
            }
            else
            {
                this.preferredName.Visible = true;
                this.preferredNameLabel.Visible = true;
            }
        }
        #endregion 
       }
}
