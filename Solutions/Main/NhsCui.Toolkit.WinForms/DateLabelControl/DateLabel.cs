//-----------------------------------------------------------------------
// <copyright file="DateLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to display a date. </summary>
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

    using NhsCui.Toolkit.DateAndTime;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The control used to display a date. 
    /// </summary>
    [DefaultProperty("Value")]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "DateLabel.bmp")]
    public partial class DateLabel : Label, INotifyPropertyChanged
    {
        #region Member Vars

        /// <summary>
        /// A list of localized strings that identify different types of null index dates.
        /// </summary>
        /// <remarks>
        /// Defaults to an empty list. 
        /// </remarks>
        private string[] nullStrings;

        /// <summary>
        /// The underlying NhsDate
        /// </summary>
        private NhsDate nhsDate;

        /// <summary>
        /// Determines whether the name of the day will be displayed with the date. 
        /// </summary>
        /// <remarks>
        /// Defaults to true.
        /// </remarks>
        private bool displayDayOfWeek;
        
        /// <summary>
        /// Specifies whether the text "Today", "Tomorrow" or "Yesterday" should be displayed in place of the date. 
        /// </summary>
        /// <remarks>Defaults to false.</remarks>
        private bool displayDateAsText;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a DateLabel object. 
        /// </summary>
        public DateLabel()
        {
            this.InitializeComponent();
            this.AccessibleName = DateLabelControl.Resources.AccessibleName;
            this.AccessibleDescription = DateLabelControl.Resources.AccessibleDescription;
            this.AccessibleRole = AccessibleRole.StaticText;
            this.Value = new NhsDate();
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
        /// The wrapper for Value.DateType. 
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue(DateType.Exact)]
        [RefreshProperties(RefreshProperties.All)]
        [Description("The DateType that represents the date")]
        public DateType DateType
        {
            get
            {
                return this.Value.DateType;
            }

            set
            {
                if (value != this.Value.DateType)
                {
                    this.Value.DateType = value;
                    this.NotifyPropertyChanged("DateType");
                    this.RefreshText();
                }
            }
        }

        /// <summary>
        /// Specifies whether the name of the day is displayed with the date. 
        /// </summary>
        /// <remarks>
        /// Defaults to true. 
        /// </remarks>
        [Bindable(true), Category("Behavior"), DefaultValue(false)]
        [RefreshProperties(RefreshProperties.All)]
        [Description("Specifies whether or not to display the day of week")]
        public bool DisplayDayOfWeek
        {
            get
            {
                return this.displayDayOfWeek;
            }

            set
            {
                if (value != this.displayDayOfWeek)
                {
                    this.displayDayOfWeek = value;
                    this.NotifyPropertyChanged("DisplayDayOfWeek");
                    this.RefreshText();
                }
            }
        }

        /// <summary>
        /// Specifies whether the text "Today", "Tomorrow" or "Yesterday" is displayed in place of the date. 
        /// </summary>
        /// <remarks>Defaults to false. </remarks>
        [Bindable(true),
        Category("Behavior"), 
        DefaultValue(false),
        RefreshProperties(RefreshProperties.All)]
        [Description("Specifies whether or not to substitute Today, Tomorrow or Yesterday for actual dates")]
        public bool DisplayDateAsText
        {
            get
            {
                return this.displayDateAsText;
            }

            set
            {
                if (value != this.displayDateAsText)
                {
                    this.displayDateAsText = value;
                    this.NotifyPropertyChanged("DisplayDateAsText");
                    this.RefreshText();
                }
            }
        }

        /// <summary>
        /// The wrapper for Value.DateValue. 
        /// </summary>
        [Bindable(true), Category("Data")]
        [RefreshProperties(RefreshProperties.All)]
        [Description("The exact or approximate date value")]
        public System.DateTime DateValue
        {
            get
            {
                return this.Value.DateValue;
            }

            set
            {
                if (value != this.Value.DateValue)
                {
                    this.Value.DateValue = value;
                    this.NotifyPropertyChanged("DateValue");
                    this.RefreshText();
                }
            }
        }

        /// <summary>
        /// The wrapper for Value.Month. 
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue(1)]
        [RefreshProperties(RefreshProperties.All)]
        [Description("The month of a DateType.YearMonth date")]
        public int Month
        {
            get
            {
                return this.Value.Month;
            }

            set
            {
                if (value != this.Value.Month)
                {
                    this.Value.Month = value;
                    this.NotifyPropertyChanged("Month");
                    this.RefreshText();
                }
            }
        }

        /// <summary>
        /// The wrapper for Value.NullIndex. 
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue(-1)]
        [RefreshProperties(RefreshProperties.All)]
        [Description("A number identifying a null index type (defaults to -1). The index has no intrinsic meaning by itself. The meaning is implied by the UI control")]
        public int NullIndex
        {
            get
            {
                return this.Value.NullIndex;
            }

            set
            {
                if (value != this.Value.NullIndex)
                {
                    this.Value.NullIndex = value;
                    this.NotifyPropertyChanged("NullIndex");
                    this.RefreshText();
                }
            }
        }

        /// <summary>
        /// Gets or sets a list of localized strings that identify different types of null index dates. 
        /// </summary>
        /// <remarks>
        /// Defaults to an empty list. 
        /// </remarks>
        [Category("Behavior")]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(StringArrayTypeConverter))]
        [Description("A list of localised strings that identify different types of null index dates (defaults to an empty list)")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "NullStrings array only expected to be small")]
        public string[] NullStrings
        {
            get
            {
                if (this.nullStrings != null)
                {
                    return (string[])this.nullStrings.Clone();
                }

                return new string[0];
            }

            set
            {
                this.nullStrings = value;
            }
        }

        /// <summary>
        /// Gets or sets the date to be displayed in the label.
        /// </summary>
        [Bindable(true), Category("Behavior")]
        [RefreshProperties(RefreshProperties.All)]
        [Description("The date to display in the label")]
        public NhsDate Value
        {
            get
            {
                return this.nhsDate;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                if (!value.Equals(this.nhsDate))
                {
                    this.nhsDate = value;
                    this.NotifyPropertyChanged("Value");
                    this.RefreshText();
                }
            }
        }

        /// <summary>
        /// The wrapper for Value.Year. 
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue(0)]
        [RefreshProperties(RefreshProperties.All)]
        [Description("The year of a DateType.Year, DateType.YearMonth or DateType")]
        public int Year
        {
            get
            {
                return this.Value.Year;
            }

            set
            {
                if (value != this.Value.Year)
                {
                    this.Value.Year = value;
                    this.NotifyPropertyChanged("Year");
                    this.RefreshText();
                }
            }
        }       

        /// <summary>
        /// Hide the Text property by new-ing
        /// </summary>
        [Browsable(false), DefaultValue(""), Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [SuppressMessage("Microsoft.Usage", "CA2222:DoNotDecreaseInheritedMemberVisibility", Justification = "Required by spec.")]
        private new string Text
        {
            get
            {
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
        /// Refresh the text displayed by the control
        /// </summary>
        private void RefreshText()
        {
            if (this.Value.DateType == DateType.NullIndex && this.NullIndex >= 0 &&
                    this.nullStrings != null && this.NullIndex < this.nullStrings.Length)
            {
                base.Text = this.nullStrings[this.NullIndex];
            }
            else
            {
                base.Text = this.Value.ToString(
                                    this.DisplayDayOfWeek, 
                                    (this.Value.DateType == DateType.Approximate), 
                                    this.DisplayDateAsText, 
                                    CultureInfo.CurrentCulture);
            }
        }

        #endregion
    }
}
