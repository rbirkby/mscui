//----------------------------------------------------------------------- 
// <copyright file="ContactLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to hold contact details.</summary>
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

    /// <summary>
    /// The control used to display contact details. 
    /// </summary>
    [DefaultProperty("HomePhoneNumber")]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "ContactLabel.bmp")]
    public partial class ContactLabel : UserControl, INotifyPropertyChanged
    {
        #region Member Vars

        /// <summary>
        /// Tooltip to be displayed.
        /// </summary>
        /// <remarks>
        /// Saves the value of the property ToolTipText
        /// </remarks>
        private String tooltipText = String.Empty; 

        /// <summary>
        /// emailAddress mem var
        /// </summary>
        private string emailAddress = string.Empty;

        /// <summary>
        /// emailLabelText mem var
        /// </summary>
        private string emailLabelText;

        /// <summary>
        /// homePhoneLabelText mem var
        /// </summary>
        private string homePhoneLabelText;

        /// <summary>
        /// homePhoneNumber mem var
        /// </summary>
        private string homePhoneNumber = string.Empty;

        /// <summary>
        /// mobilePhoneLabelText mem var
        /// </summary>
        private string mobilePhoneLabelText;

        /// <summary>
        /// mobilePhoneNumber mem var
        /// </summary>
        private string mobilePhoneNumber = string.Empty;

        /// <summary>
        /// workPhoneLabelText mem var
        /// </summary>
        private string workPhoneLabelText;

        /// <summary>
        /// workPhoneLabelText mem var
        /// </summary>
        private string workPhoneNumber = string.Empty;

        /// <summary>
        /// Font to use for fields in the control AON: DEFECT #2750 RAISED AGAINST THIS.
        /// </summary>
        private Font dataFont;

        /// <summary>
        /// Font to use for labels in the control AON: DEFECT #2750 RAISED AGAINST THIS.
        /// </summary>
        private Font labelFont;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a ContactLabel object. 
        /// </summary>
        public ContactLabel()
        {
            this.InitializeComponent();
            this.AccessibleName = ContactLabelControl.Resources.AccessibleName;
            this.AccessibleDescription = ContactLabelControl.Resources.AccessibleDescription;
            this.AccessibleRole = AccessibleRole.StaticText;
            this.SetDefaultValues();
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes. 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text displayed in the tooltip.
        /// </summary>       
        [Category("Behavior")]
        [Description("The text that is displayed in the tooltip.")]
        [ResourceDefaultValue(typeof(DateInputBoxControl.Resources), "FirstUseToolTipSimple")]
        public string TooltipText
        {
            get
            {
                return this.tooltipText;
            }

            set
            {
                if (value == null)
                {
                    value = String.Empty;
                }

                if (this.tooltipText != value)
                {
                    this.tooltipText = value;
                    this.SetToolTip();
                }
            }
        }

        /// <summary>
        /// Gets or sets the contact's email address. 
        /// </summary>
        /// <remarks>
        /// Defaults to "". If this element is left empty, <see cref="P:NhsCui.Toolkit.WinForms.ContactLabel.EmailLabelText">EmailLabelText</see> 
        /// will be displayed with blank space next to it. 
        /// </remarks>
        [Bindable(true), Category("Patient Contact"), DefaultValue("")]
        [Description("The Email Address field of the Patient Contact.")]
        [RefreshProperties(RefreshProperties.All)]
        public string EmailAddress
        {
            get
            {
                return this.emailAddress;
            }

            set
            {
                if (value != this.emailAddress)
                {
                    this.emailAddress = value;
                    this.contactLabelTableLayoutPanel.Controls["EmailAddressField"].Text = this.emailAddress;
                    this.NotifyPropertyChanged("EmailAddress");
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the contact's email address.
        /// </summary>
        /// <remarks>
        /// Defaults to "Email". 
        /// </remarks>
        [Bindable(true), Category("Patient Contact"), Localizable(true)]
        [Description("The text associated with the Email Address field of the Patient Contact.")]
        [ResourceDefaultValue(typeof(ContactLabelControl.Resources), "EmailLabelText")]
        [RefreshProperties(RefreshProperties.All)]
        public string EmailLabelText
        {
            get
            {
                if (this.emailLabelText != null)
                {
                    return this.emailLabelText;
                }
                else
                {
                    return ContactLabelControl.Resources.EmailLabelText;
                }
            }

            set
            {
                if (value != this.emailLabelText)
                {
                    this.emailLabelText = value;
                    this.contactLabelTableLayoutPanel.Controls["EmailAddressLabel"].Text = this.EmailLabelText;
                    this.NotifyPropertyChanged("EmailLabelText");
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the contact's home phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Home". 
        /// </remarks>
        [Bindable(true), Category("Patient Contact"), Localizable(true)]
        [Description("The text associated with the Home Phone Number field of the Patient Contact.")]
        [RefreshProperties(RefreshProperties.All)]
        [ResourceDefaultValue(typeof(ContactLabelControl.Resources), "HomePhoneLabelText")]
        public string HomePhoneLabelText
        {
            get
            {
                if (this.homePhoneLabelText != null)
                {
                    return this.homePhoneLabelText;
                }
                else
                {
                    return ContactLabelControl.Resources.HomePhoneLabelText;
                }
            }

            set
            {
                if (value != this.homePhoneLabelText)
                {
                    this.homePhoneLabelText = value;
                    this.contactLabelTableLayoutPanel.Controls["HomePhoneLabel"].Text = this.HomePhoneLabelText;
                    this.NotifyPropertyChanged("HomePhoneLabelText");
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the contact's home phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "". If this element is left empty, <see cref="P:NhsCui.Toolkit.WinForms.ContactLabel.HomePhoneLabelText">HomePhoneLabelText</see> 
        /// will be displayed with blank space next to it.  
        /// </remarks>
        [Bindable(true), Category("Patient Contact"), DefaultValue("")]
        [Description("The Home Phone Number field of the Patient Contact.")]
        [RefreshProperties(RefreshProperties.All)]
        public string HomePhoneNumber
        {
            get
            {
                return this.homePhoneNumber;
            }

            set
            {
                if (value != null)
                {
                    if (value != this.homePhoneNumber)
                    {
                        this.homePhoneNumber = value;
                        this.contactLabelTableLayoutPanel.Controls["HomePhoneField"].Text = this.homePhoneNumber;
                        this.NotifyPropertyChanged("HomePhoneNumber");
                        this.Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the contact's mobile phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Mobile". 
        /// </remarks>
        [Bindable(true), Category("Patient Contact"), Localizable(true)]
        [Description("The text associated with the Mobile Phone Number field of the Patient Contact.")]
        [RefreshProperties(RefreshProperties.All)]
        [ResourceDefaultValue(typeof(ContactLabelControl.Resources), "MobilePhoneLabelText")]
        public string MobilePhoneLabelText
        {
            get
            {
                if (this.mobilePhoneLabelText != null)
                {
                    return this.mobilePhoneLabelText;
                }
                else
                {
                    return ContactLabelControl.Resources.MobilePhoneLabelText;
                }
            }

            set
            {
                if (value != this.mobilePhoneLabelText)
                {
                    this.mobilePhoneLabelText = value;
                    this.contactLabelTableLayoutPanel.Controls["MobilePhoneLabel"].Text = this.MobilePhoneLabelText;
                    this.NotifyPropertyChanged("MobilePhoneLabelText");
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the contact's mobile phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "". If this element is left empty, <see cref="P:NhsCui.Toolkit.WinForms.ContactLabel.MobilePhoneLabelText">MobilePhoneLabelText</see> 
        /// will be displayed with blank space next to it. 
        /// </remarks>
        [Bindable(true), Category("Patient Contact"), DefaultValue("")]
        [Description("The Mobile Phone Number field of the Patient Contact.")]
        [RefreshProperties(RefreshProperties.All)]
        public string MobilePhoneNumber
        {
            get
            {
                return this.mobilePhoneNumber;
            }

            set
            {
                if (value != null)
                {
                    if (value != this.mobilePhoneNumber)
                    {
                        this.mobilePhoneNumber = value;
                        this.contactLabelTableLayoutPanel.Controls["MobilePhoneField"].Text = this.mobilePhoneNumber;
                        this.NotifyPropertyChanged("MobilePhoneNumber");
                        this.Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the caption associated with the contact's work phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Work". 
        /// </remarks>
        [Bindable(true), Category("Patient Contact"), Localizable(true)]
        [Description("The text associated with the Work Phone Number field of the Patient Contact.")]
        [RefreshProperties(RefreshProperties.All)]
        [ResourceDefaultValue(typeof(ContactLabelControl.Resources), "WorkPhoneLabelText")]
        public string WorkPhoneLabelText
        {
            get
            {
                if (this.workPhoneLabelText != null)
                {
                    return this.workPhoneLabelText;
                }
                else
                {
                    return ContactLabelControl.Resources.WorkPhoneLabelText;
                }
            }

            set
            {
                if (value != this.workPhoneLabelText)
                {
                    this.workPhoneLabelText = value;
                    this.contactLabelTableLayoutPanel.Controls["WorkPhoneLabel"].Text = this.WorkPhoneLabelText;
                    this.NotifyPropertyChanged("WorkPhoneLabelText");
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the contact's work phone number. 
        /// </summary>
        ///  <remarks>
        /// Defaults to "". If this element is left empty, <see cref="P:NhsCui.Toolkit.WinForms.ContactLabel.WorkPhoneLabelText">WorkPhoneLabelText</see> 
        /// will be displayed with blank space next to it. 
        /// </remarks>
        [Bindable(true), Category("Patient Contact"), DefaultValue("")]
        [Description("The Work Phone Number field of the Patient Contact.")]
        [RefreshProperties(RefreshProperties.All)]
        public string WorkPhoneNumber
        {
            get
            {
                return this.workPhoneNumber;
            }

            set
            {
                if (value != null)
                {
                    if (value != this.workPhoneNumber)
                    {
                        this.workPhoneNumber = value;
                        this.contactLabelTableLayoutPanel.Controls["WorkPhoneField"].Text = this.workPhoneNumber;
                        this.NotifyPropertyChanged("WorkPhoneNumber");
                        this.Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the font for the data. 
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(null)]
        [Description("Font for data fields.")]
        [RefreshProperties(RefreshProperties.All)]
        public Font DataFont
        {
            get
            {
                return this.dataFont;
            }

            set
            {
                if (this.dataFont == null || this.dataFont.Equals(value) == false)
                {
                    this.dataFont = value;
                    this.ApplyExtraStyles();
                    this.NotifyPropertyChanged("DataFont");
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the font for the captions. 
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(null)]
        [Description("Font for data labels.")]
        [RefreshProperties(RefreshProperties.All)]
        public Font LabelFont
        {
            get
            {
                return this.labelFont;
            }

            set
            {
                if (this.labelFont == null || this.labelFont.Equals(value) == false)
                {
                    this.labelFont = value;
                    this.ApplyExtraStyles();
                    this.NotifyPropertyChanged("LabelFont");
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Gets the contact details summary
        /// </summary>
        /// <returns>
        /// returns the first non empty number from Home phone number/Work phone number/Mobile phone number
        /// </returns>
        internal string GetContactDetailsSummary()
        {
            string summary = string.Empty;
            if (!string.IsNullOrEmpty(this.HomePhoneNumber))
            {
                summary = this.HomePhoneNumber;
            }
            else if (!string.IsNullOrEmpty(this.MobilePhoneNumber))
            {
                summary = this.MobilePhoneNumber;
            }
            else if (!string.IsNullOrEmpty(this.WorkPhoneNumber))
            {
                summary = this.WorkPhoneNumber;
            }
            else if (!string.IsNullOrEmpty(this.EmailAddress))
            {
                summary = this.EmailAddress;
            }

            return summary;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Apply/refresh the area font styling
        /// </summary>
        private void ApplyExtraStyles()
        {
            // Fields
            if (this.DataFont != null)
            {
                this.emailAddressField.Font = this.DataFont;
                this.homePhoneField.Font = this.DataFont;
                this.mobilePhoneField.Font = this.DataFont;
                this.workPhoneField.Font = this.DataFont;
            }

            // Labels
            if (this.LabelFont != null)
            {
                this.emailAddressLabel.Font = this.LabelFont;
                this.homePhoneLabel.Font = this.LabelFont;
                this.mobilePhoneLabel.Font = this.LabelFont;
                this.workPhoneLabel.Font = this.LabelFont;
            }
        }

        /// <summary>
        /// Sets tooltip of the child controls.
        /// </summary>
        private void SetToolTip()
        {
            this.toolTip1.RemoveAll();
            this.toolTip1.SetToolTip(this.contactLabelTableLayoutPanel, this.TooltipText);
            foreach (Control ctrl in this.contactLabelTableLayoutPanel.Controls)
            {
                this.toolTip1.SetToolTip(ctrl, this.TooltipText);
            }
        }

        /// <summary>
        /// Set the default values to labels
        /// </summary>
        private void SetDefaultValues()
        {
            this.homePhoneLabel.Text = this.HomePhoneLabelText;
            this.mobilePhoneLabel.Text = this.MobilePhoneLabelText;
            this.workPhoneLabel.Text = this.WorkPhoneLabelText;
            this.emailAddressLabel.Text = this.EmailLabelText;
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
        #endregion
    }
}
