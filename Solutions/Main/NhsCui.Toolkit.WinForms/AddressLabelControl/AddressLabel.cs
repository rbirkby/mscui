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
// <summary>The AddressLabel control.</summary>
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
    using System.Globalization;

    /// <summary>
    /// The control used to display an address. 
    /// </summary>
    [DefaultProperty("Address1")]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "AddressLabel.bmp")]
    public partial class AddressLabel : UserControl, INotifyPropertyChanged
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
        /// Gets or sets the first line of the patient's address. 
        /// </summary>
        /// <remarks>
        ///All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        private string address1 = string.Empty;

        /// <summary>
        /// Gets or sets the second line of the patient's address. 
        /// </summary>
        /// <remarks>
        ///All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        private string address2 = string.Empty;

        /// <summary>
        /// Gets or sets the third line of the patient's address. 
        /// </summary>
        /// <remarks>
        ///All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        private string address3 = string.Empty;

        /// <summary>
        /// Gets or sets the town in the patient's address. 
        /// </summary>
        /// <remarks>
        ///All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        private string town = string.Empty;

        /// <summary>
        /// Gets or sets the county in the patient's address. 
        /// </summary>
        /// <remarks>
        ///All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        private string county = string.Empty;

        /// <summary>
        /// Gets or sets the patient's postcode. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. If the postcode is present, 
        /// it should display in capitalized form and as the final element before the country. 
        /// </remarks>
        private string postcode = string.Empty;

        /// <summary>
        /// Gets or sets the country in the patient's address. 
        /// </summary>
        /// <remarks>
        ///All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        private string country = string.Empty;

        /// <summary>
        /// Gets or sets the appropriate method for displaying the patient's address. Sets the address layout to be either inform (vertical) or inline (horizontal). 
        /// </summary>
        /// <remarks>
        /// If the address layout is set to inform, each line contains a single, left-justified element with 
        /// no separator characters displayed. If the address layout is set to inline, multiple elements display on a 
        /// single line, with address elements separated by a single comma and a single space. Individual address 
        /// elements should not split across multiple lines.
        ///</remarks>
        private AddressDisplayFormat addressDisplayFormat = AddressDisplayFormat.InLine;

        /// <summary>
        /// Gets or sets the address type of the patient banner
        /// <remarks>
        /// Defaults to Usual address
        /// </remarks>
        /// </summary>
        private string addressType = AddressLabelControl.Resources.AddressType;

        /// <summary>
        /// Address type label font
        /// </summary>
        private Font addressTypeLabelFont;
        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an AddressLabel object. 
        /// </summary>
        public AddressLabel()
        {
            this.InitializeComponent();
            this.AccessibleName = AddressLabelControl.Resources.AccessibleName;
            this.AccessibleDescription = AddressLabelControl.Resources.AccessibleDescription;
            this.AccessibleRole = AccessibleRole.StaticText;
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
        /// Gets or sets the first line of an address. 
        /// </summary>
        /// <remarks>
        ///All stored address data should display. If an address element is empty, it will not be displayed.
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The first field of the Patient Address.")]
        [RefreshProperties(RefreshProperties.All)]
        public string Address1
        {
            get
            {
                return this.address1;
            }

            set
            {
                if (value != this.address1)
                {
                    this.address1 = value;
                    this.NotifyPropertyChanged("Address1");
                    this.RefreshDisplayLayout();
                    this.Invalidate();
                }
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
        [RefreshProperties(RefreshProperties.All)]
        public string Address2
        {
            get
            {
                return this.address2;
            }

            set
            {
                if (value != this.address2)
                {
                    this.address2 = value;
                    this.NotifyPropertyChanged("Address2");
                    this.RefreshDisplayLayout();
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the third line of an address. 
        /// </summary>
        /// <remarks>
        ///All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The third field of the Patient Address.")]
        [RefreshProperties(RefreshProperties.All)]
        public string Address3
        {
            get
            {
                return this.address3;
            }

            set
            {
                if (value != this.address3)
                {
                    this.address3 = value;
                    this.NotifyPropertyChanged("Address3");
                    this.RefreshDisplayLayout();
                    this.Invalidate();
                }
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
        [RefreshProperties(RefreshProperties.All)]
        public string Town
        {
            get
            {
                return this.town;
            }

            set
            {
                if (value != this.town)
                {
                    this.town = value;
                    this.NotifyPropertyChanged("Town");
                    this.RefreshDisplayLayout();
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the county in an address. 
        /// </summary>
        /// <remarks>
        ///All stored address data should display. If an address element is empty, it will not be displayed.
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The County field of the Patient Address.")]
        [RefreshProperties(RefreshProperties.All)]
        public string County
        {
            get
            {
                return this.county;
            }

            set
            {
                if (value != this.county)
                {
                    this.county = value;
                    this.NotifyPropertyChanged("County");
                    this.RefreshDisplayLayout();
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the postcode. 
        /// </summary>
        /// <remarks>
        ///All stored address data should display. If an address element is empty, it will not be displayed. If the postcode is present, 
        /// it should display in capitalized form and as the final element before the 
        /// <see cref="P:NhsCui.Toolkit.WinForms.AddressLabel.Country">Country</see>.
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The Post Code field of the Patient Address.")]
        [RefreshProperties(RefreshProperties.All)]
        public string Postcode
        {
            get
            {
                return this.postcode;
            }

            set
            {
                if (value != this.postcode)
                {
                    this.postcode = value;
                    this.NotifyPropertyChanged("Postcode");
                    this.RefreshDisplayLayout();
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the country in an address.
        /// </summary>
        /// <remarks>
        ///All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The Country field of the Patient Address.")]
        [RefreshProperties(RefreshProperties.All)]
        public string Country
        {
            get
            {
                return this.country;
            }

            set
            {
                if (value != this.country)
                {
                    this.country = value;
                    this.NotifyPropertyChanged("Country");
                    this.RefreshDisplayLayout();
                    this.Invalidate();
                }
            }
        }

        // Changed access from public to internal - see PS#2670 - AddressDisplayFormat property has been descoped
        // Leaving in for the moment pending clarification as to whether descoping is temporary
        // or permanent spec change - GMM
        // -------------------------------------------------------------------
        // Now re-instated - see PS#4663

        /// <summary>
        /// Sets the address layout to be either inform (vertical) or inline (horizontal). 
        /// </summary>
        /// <remarks>
        /// Defaults to inform. If the address layout is set to inform, each line contains a single, left-justified element with 
        /// no separator characters displayed. If the address layout is set to inline, multiple elements display on a 
        /// single line, with address elements separated by a single comma and a single space. Individual address 
        /// elements should not split across multiple lines. 
        ///</remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue(AddressDisplayFormat.InLine)]
        [Description("Indicates whether the Patient Address is displayed in a vertical (InForm) or flowed (InLine) layout.")]
        [RefreshProperties(RefreshProperties.All)]
        public AddressDisplayFormat AddressDisplayFormat
        {
            get
            {
                return this.addressDisplayFormat;
            }

            set
            {
                if (value != this.addressDisplayFormat)
                {
                    this.addressDisplayFormat = value;
                    this.NotifyPropertyChanged("AddressDisplayFormat");
                    if (this.addressDisplayFormat == AddressDisplayFormat.InForm)
                    {
                        this.informTableLayoutPanel.Visible = true;
                        this.inlineFlowLayoutPanel.Visible = false;
                    }
                    else
                    {
                        this.informTableLayoutPanel.Visible = false;
                        this.inlineFlowLayoutPanel.Visible = true;
                    }

                    this.RefreshDisplayLayout();
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the address type of the patient address.
        /// </summary>
        /// <remarks>
        /// Defaults to Usual Address
        /// </remarks>
        [Bindable(true), Category("Patient Address"), DefaultValue("")]
        [Description("The Address Type of the Patient Address.")]
        [RefreshProperties(RefreshProperties.All)]
        public string AddressType
        {
            get
            {
                return this.addressType;
            }

            set
            {
                if (value != this.addressType)
                {
                    this.addressType = value;
                    this.NotifyPropertyChanged("AddressType");
                    this.RefreshDisplayLayout();
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the font to be used for the address type label.
        /// </summary>
        public Font AddressTypeLabelFont
        {
            get
            {
                return this.addressTypeLabelFont;
            }

            set
            {
                if (value != this.addressTypeLabelFont)
                {
                    this.addressTypeLabelFont = value;
                    this.addressTypeLabel.Font = value;
                    this.addresslabelTypeInlineLayout.Font = value;
                    this.NotifyPropertyChanged("AddressTypeLabelFont");
                    this.RefreshDisplayLayout();
                    this.Invalidate();
                }
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// OnCreateControl override to ensure that the new, empty control displays its name...
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // @Design-Time default the text
            this.RefreshDisplayLayout();
        }

        /// <summary>
        /// Utility method to set value &amp; show/hide a field Label
        /// </summary>
        /// <param name="targetControl">Target Label to set Text &amp; Visibility</param>
        /// <param name="targetText">Target text for the Label</param>
        private static void DisplayField(Control targetControl, string targetText)
        {
            if (targetText != null && targetText.Trim().Length > 0)
            {               
                targetControl.Text = targetText;
                targetControl.Visible = true;
            }
            else
            {
                targetControl.Visible = false;
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
        /// Ensure 'empty fields' are not shown
        /// </summary>
        private void RefreshDisplayLayout()
        {
            // For FxCop's benefit...
            string upperPostcode = this.postcode.ToUpperInvariant();
            Label designTimeLabel;

            // Can't think of a way of doing this with references or lookups and loops
            // which isn't more complicated than just literally assigning values to each control from each field
            if (this.addressDisplayFormat == AddressDisplayFormat.InForm)
            {
                designTimeLabel = (Label)this.informTableLayoutPanel.Controls["tableAddressField1"];
                DisplayField(this.informTableLayoutPanel.Controls["addressTypeLabel"], this.addressType);
                DisplayField(this.informTableLayoutPanel.Controls["tableAddressField1"], this.address1);
                DisplayField(this.informTableLayoutPanel.Controls["tableAddressField2"], this.address2);
                DisplayField(this.informTableLayoutPanel.Controls["tableAddressField3"], this.address3);
                DisplayField(this.informTableLayoutPanel.Controls["tableAddressField4"], this.town);
                DisplayField(this.informTableLayoutPanel.Controls["tableAddressField5"], this.county);
                DisplayField(this.informTableLayoutPanel.Controls["tableAddressField6"], upperPostcode);
                DisplayField(this.informTableLayoutPanel.Controls["tableAddressField7"], this.country);
            }
            else
            {
                designTimeLabel = (Label)this.inlineFlowLayoutPanel.Controls["flowAddressField1"];
                DisplayField(this.inlineFlowLayoutPanel.Controls["addresslabelTypeInlineLayout"], this.addressType);
                DisplayField(this.inlineFlowLayoutPanel.Controls["flowAddressField1"], this.address1);
                DisplayField(this.inlineFlowLayoutPanel.Controls["flowAddressField2"], this.address2);
                DisplayField(this.inlineFlowLayoutPanel.Controls["flowAddressField3"], this.address3);
                DisplayField(this.inlineFlowLayoutPanel.Controls["flowAddressField4"], this.town);
                DisplayField(this.inlineFlowLayoutPanel.Controls["flowAddressField5"], this.county);
                DisplayField(this.inlineFlowLayoutPanel.Controls["flowAddressField6"], upperPostcode);
                DisplayField(this.inlineFlowLayoutPanel.Controls["flowAddressField7"], this.country);

                // Correct the last visible field's spurious delimiter
                bool lastVisibleLabelFound = false;
                for (int flowLabelIndex = this.inlineFlowLayoutPanel.Controls.Count - 1; flowLabelIndex >= 0; flowLabelIndex--)
                {
                    Label childLabel = (Label)this.inlineFlowLayoutPanel.Controls[flowLabelIndex];
                    if (childLabel.Text.Length > 0)
                    {
                        if (lastVisibleLabelFound == true && !childLabel.Equals(this.addresslabelTypeInlineLayout))
                        {
                            childLabel.Text += AddressLabelControl.Resources.AddressItemSeparator;
                        }

                        lastVisibleLabelFound = true;
                    }
                }
            }

            if (this.AllFieldsAreEmpty() == true && this.DesignMode == true)
            {
                designTimeLabel.Visible = true;
                designTimeLabel.Text = string.Format(CultureInfo.InvariantCulture, "[{0}]", this.Name);
            }
        }

        /// <summary>
        /// Sets tooltip of the child controls.
        /// </summary>
        private void SetToolTip()
        {
            this.toolTip1.RemoveAll();
            this.toolTip1.SetToolTip(this.informTableLayoutPanel, this.TooltipText);
            foreach (Control ctrl in this.informTableLayoutPanel.Controls)
            {
                this.toolTip1.SetToolTip(ctrl, this.TooltipText);    
            }

            this.toolTip1.SetToolTip(this.inlineFlowLayoutPanel, this.TooltipText);
            foreach (Control ctrl in this.inlineFlowLayoutPanel.Controls)
            {
                this.toolTip1.SetToolTip(ctrl, this.TooltipText);
            }
        }

        /// <summary>
        /// Establish whether we have an 'empty' control
        /// </summary>
        /// <returns>true if all fields are empty, otherwise false</returns>
        private bool AllFieldsAreEmpty()
        {
            if (this.address1.Trim().Length == 0 && this.address2.Trim().Length == 0 && this.address3.Trim().Length == 0 && this.town.Trim().Length == 0 && this.county.Trim().Length == 0 && this.postcode.Trim().Length == 0 && this.country.Trim().Length == 0)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}