//-----------------------------------------------------------------------
// <copyright file="GenderLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to display a patient's gender.  </summary>
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
    using NhsCui.Toolkit;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The control used to display a patient's gender. 
    /// </summary>
    [DefaultProperty("Value")]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "GenderLabel.bmp")]
    public partial class GenderLabel : Label, INotifyPropertyChanged
    {
        #region Member Vars

        /// <summary>
        /// patientGenderValue mem var
        /// </summary>
        private PatientGender patientGenderValue = PatientGender.NotKnown;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a GenderLabel object. 
        /// </summary>
        public GenderLabel()
        {
            this.InitializeComponent();
            this.AccessibleName = GenderLabelControl.Resources.AccessibleName;
            this.AccessibleDescription = GenderLabelControl.Resources.AccessibleDescription;
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
        /// Gets or sets the patient's gender. 
        /// </summary>
        /// <remarks>
        /// This may be "Female", "Male", "NotSpecified" or "NotKnown".
        /// </remarks>
        [Bindable(true), Category("Behavior"), DefaultValue(PatientGender.NotKnown)]
        [RefreshProperties(RefreshProperties.All)]
        [Description("PatientGender enumeration value - Female, Male, NotSpecified or NotKnown.")]
        public PatientGender Value
        {
            get
            {
                if (this.Text.Length == 0)
                {
                    this.SetTextFromGenderValue();
                }

                return this.patientGenderValue;
            }

            set
            {
                if (Enum.IsDefined(typeof(PatientGender), value))
                {
                    this.patientGenderValue = value;
                    this.SetTextFromGenderValue();
                }
                else
                {
                    throw new ArgumentOutOfRangeException(GenderLabelControl.Resources.PatientGenderArgumentOutOfRangeException);
                }
            }
        }

        /// <summary>
        /// Hide the inherited Text property. 
        /// </summary>
        [Browsable(false)]
        [Description("New and hidden Text property; used to display the PatientGender value via base class functionality.")]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [SuppressMessage("Microsoft.Usage", "CA2222:DoNotDecreaseInheritedMemberVisibility", Justification = "Required by spec.")]
        private new string Text
        {
            get
            {
                if (base.Text.Length == 0)
                {
                    this.SetTextFromGenderValue();
                }

                return base.Text;
            }

            set
            {
                base.Text = value;
            }
        }

        #endregion

        #region Methods

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
        /// Set the text property from the using the current value of patientGenderValue
        /// </summary>
        private void SetTextFromGenderValue()
        {
            switch (this.patientGenderValue)
            {
                case PatientGender.Male:
                    this.Text = GenderLabelControl.Resources.Male;
                    break;
                case PatientGender.Female:
                    this.Text = GenderLabelControl.Resources.Female;
                    break;
                case PatientGender.NotSpecified:
                    this.Text = GenderLabelControl.Resources.NotSpecified;
                    break;                
                default:
                    this.Text = GenderLabelControl.Resources.NotKnown;
                    break;
            }
        }

        #endregion
    }
}
