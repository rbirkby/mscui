//-----------------------------------------------------------------------
// <copyright file="NameLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to display the patient's name. </summary>
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
    /// The control used to display the patient's name. 
    /// </summary>
    [DefaultProperty("FamilyName")]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "NameLabel.bmp")]
    public partial class NameLabel : UserControl, INotifyPropertyChanged
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
        /// givenName mem var
        /// </summary>
        private string givenName = string.Empty;

        /// <summary>
        /// familyName mem var
        /// </summary>
        private string familyName = string.Empty;

        /// <summary>
        /// title mem var
        /// </summary>
        private string title = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a NameLabel object.
        /// </summary>
        public NameLabel()
            : base()
        {
            this.InitializeComponent();
            this.AccessibleName = NameLabelControl.Resources.AccessibleName;
            this.AccessibleDescription = NameLabelControl.Resources.AccessibleDescription;
            this.AccessibleRole = AccessibleRole.StaticText;
            this.familyNameLabel.Text = NameLabelControl.Resources.FamilyNameDefaultDesignTimeValue;
            this.givenNameLabel.Text = NameLabelControl.Resources.GivenNameDefaultDesignTimeValue;
            this.titleLabel.Text = NameLabelControl.Resources.TitelDefaultDesignTimeValue;
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
        /// Gets the correctly-formatted aggregate value of the patient's name.  
        /// </summary>
        /// <remarks>
        /// The patient's name takes the form "FAMILYNAME, GivenName (Title)". The maximum total display length is 120 characters. 
        /// </remarks>
        [Bindable(true), Category("Patient Name")]
        [Description("The ReadOnly DisplayValue field of the Patient Name.")]
        [RefreshProperties(RefreshProperties.All)]
        public string DisplayValue
        {
            get
            {
                return PatientName.Format(this.FamilyName, this.GivenName, this.Title);
            }
        }

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
        /// Gets or sets the given name. 
        /// </summary>
        /// <remarks>
        /// The maximum display length is 40 characters. If the data exceeds 40 characters, it is truncated to 
        /// 37 characters plus an ellipsis. 
        /// </remarks>
        [Bindable(true), Category("Patient Name"), DefaultValue("")]
        [Description("The First Name field of the Patient Name.")]
        [RefreshProperties(RefreshProperties.All)]
        public string GivenName
        {
            get
            {
                return this.givenName;
            }

            set
            {
                if (value != this.givenName)
                {
                    this.givenName = value;
                    this.RefreshDisplayValueLayout();
                    this.NotifyPropertyChanged("GivenName");
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        ///  Gets or sets the family name. 
        /// </summary>
        /// <remarks>
        /// The NameLabel control capitalizes the family name. The maximum display length is 40 characters. If the data exceeds 40 characters, it is 
        /// truncated to 
        /// 37 characters plus an ellipsis. 
        /// </remarks>
        [Bindable(true), Category("Patient Name"), DefaultValue("")]
        [Description("The Last Name field of the Patient Name.")]
        [RefreshProperties(RefreshProperties.All)]
        public string FamilyName
        {
            get
            {
                return this.familyName;
            }

            set
            {
                if (value != this.familyName)
                {
                    this.familyName = value;
                    this.RefreshDisplayValueLayout();
                    this.NotifyPropertyChanged("FamilyName");
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the patient's title. 
        /// </summary>
        /// <remarks>
        /// Parentheses are automatically put around the title. The title is only dispayed if one of
        /// <see cref="P:NhsCui.Toolkit.WinForms.NameLabel.FamilyName">FamilyName</see> or
        /// <see cref="P:NhsCui.Toolkit.WinForms.NameLabel.GivenName">GivenName</see> is also provided. The maximum display length is 35 characters, 
        /// including parentheses and three spaces.
        /// </remarks>
        [Bindable(true), Category("Patient Name"), DefaultValue("")]
        [Description("The Title field of the Patient Name.")]
        [RefreshProperties(RefreshProperties.All)]
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                if (value != this.title)
                {
                    this.title = value;
                    this.RefreshDisplayValueLayout();
                    this.NotifyPropertyChanged("Title");
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Size the flow panel to the main control size
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void NameLabel_Resize(object sender, EventArgs e)
        {
            this.nameFlowLayoutPanel.Size = this.Size;
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
        /// Adjust formatting of combined LASTNAME, GivenName (Title) layout
        /// </summary>
        private void RefreshDisplayValueLayout()
        {
            string[] nameParts = PatientName.FormatNameArray(this.FamilyName, this.GivenName, this.Title);

            this.nameFlowLayoutPanel.Controls["FamilyNameLabel"].Text = nameParts[PatientName.FamilyNameIndex];
            this.nameFlowLayoutPanel.Controls["FamilyNameLabel"].Refresh();

            this.nameFlowLayoutPanel.Controls["GivenNameLabel"].Text = nameParts[PatientName.GivenNameIndex];
            this.nameFlowLayoutPanel.Controls["GivenNameLabel"].Refresh();

            this.nameFlowLayoutPanel.Controls["TitleLabel"].Text = nameParts[PatientName.TitleIndex];
            this.nameFlowLayoutPanel.Controls["TitleLabel"].Refresh();

            this.nameFlowLayoutPanel.Refresh();
        }

        /// <summary>
        /// Sets tooltip of the child controls.
        /// </summary>
        private void SetToolTip()
        {
            this.toolTip1.RemoveAll();
            this.toolTip1.SetToolTip(this.familyNameLabel, this.TooltipText);
            this.toolTip1.SetToolTip(this.givenNameLabel, this.TooltipText);
        }

        #endregion
    }
}
