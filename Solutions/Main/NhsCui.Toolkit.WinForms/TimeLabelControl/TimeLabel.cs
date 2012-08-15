//-----------------------------------------------------------------------
// <copyright file="TimeLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>24-May-2007</date>
// <summary>The control used to display an NhsTime.</summary>
//-----------------------------------------------------------------------

//Although the control currently support both 24 hour and 12 hour clock the use of 12 hour clock is NOT supported 
//for safety critical environments, and the ISV has a responsibility to use the controls in a manner appropriate to 
//the clinical context.

namespace NhsCui.Toolkit.WinForms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using NhsCui.Toolkit;
    using NhsCui.Toolkit.DateAndTime;
    using System.Windows.Forms;
    using System.Globalization;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The control used to display an NhsTime.
    /// </summary>
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "TimeLabel.bmp")]    
    [DefaultProperty("Value")]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    public partial class TimeLabel : Label, INotifyPropertyChanged
    {
        #region Member Vars
        /// <summary>
        /// Toggle value to allow/forbid the seconds display in the control.
        /// </summary>
        /// <remarks>Saves the value of property DisplaySeconds</remarks>
        private bool displaySeconds;

        /// <summary>
        /// Toggle value to display time in 12 hour format. If false time is displayed in 24 hour format.
        /// </summary>
        /// <remarks>Saves the value of property Display12Hour</remarks>
        private bool display12Hour;

        /// <summary>
        /// Toggle value to allow/forbid the AM/PM display in the control.
        /// </summary>
        /// <remarks>Saves the value of property DisplayAMPM</remarks>
        private bool displayAMPM;

        /// <summary>
        /// Collection of null strings.
        /// </summary>
        /// <remarks>Saves the value of property NullStrings</remarks>
        private string[] nullStrings;

        /// <summary>
        /// The Time Value of the control.
        /// </summary>
        /// <remarks>Saves the Value of property TimeValue</remarks>
        private NhsTime timeValue;

        /// <summary>
        /// Tool Tip for the control.
        /// </summary>
        /// <remarks>
        /// Saves the value of ToolTip Property.
        /// </remarks>
        private String toolTip;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a TimeLabel object. 
        /// </summary>
        public TimeLabel()
        {
            this.InitializeComponent();
            this.AccessibleName = TimeLabelControl.Resources.AccessibleName;
            this.AccessibleDescription = TimeLabelControl.Resources.AccessibleDescription;
            this.AccessibleRole = AccessibleRole.StaticText;
            this.RefreshDisplayText();
            this.AutoSize = true;
        }
        #endregion       

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes. 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Public Properties
        /// <summary>
        /// Specifies whether seconds should be displayed. 
        /// </summary>
        /// <remarks>
        /// Defaults to false. 
        /// </remarks>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Specifies whether seconds should be displayed")]
        public bool DisplaySeconds
        {
            get
            {
                return this.displaySeconds;
            }

            set
            {
                if (this.DisplaySeconds != value)
                {
                    this.displaySeconds = value;
                    this.RefreshDisplayText();
                }               
            }
        }

        /// <summary>
        /// Specifies whether hours should be displayed in 12-hour or 24-hour format. 
        /// </summary>
        /// <remarks>
        /// Defaults to false. If the time is 1:30 in the afternoon, this displays as 01:30 in 12-hour format and 13:30 in 24-hour format.
        /// Although the control currently support both 24 hour and 12 hour clock the use of 12 hour clock is NOT supported 
        /// for safety critical environments, and the ISV has a responsibility to use the controls in a manner appropriate to 
        /// the clinical context.
        /// </remarks>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Specifies whether hours should be displayed as 12 hour or 24 hour")]
        public bool Display12Hour
        {
            get
            {
                return this.display12Hour;
            }

            set
            {
                if (this.display12Hour != value)
                {                                  
                    this.display12Hour = value;
                    this.RefreshDisplayText();
                }
            }
        }

        /// <summary>
        /// Specifies whether an AM/PM suffix should be included. 
        /// </summary>
        /// <remarks> 
        /// Defaults to false. AM refers to times between 12:00 midnight and 12 noon. PM refers to times between 12 noon and 12 midnight. 
        /// </remarks>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Specifies whether the AM/PM suffix should be included")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", Justification = "As specified")]
        public bool DisplayAMPM
        {
            get
            {
                return this.displayAMPM;
            }

            set
            {
                if (this.displayAMPM != value)
                {
                    this.displayAMPM = value;
                    this.RefreshDisplayText(); 
                }
            }
        }

        /// <summary>
        /// Gets or sets a list of localized strings which identify different types of null index times.
        /// </summary>
        /// <remarks>
        /// Defaults to an empty list.
        /// </remarks>
        [Category("Behavior"), DefaultValue(null), TypeConverter(typeof(StringArrayTypeConverter))]
        [Description("A list of localized strings that identify different types of null times (defaults to an empty list).")]
        [RefreshProperties(RefreshProperties.All)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "NullStrings array only expected to be small")]
        public string[] NullStrings
        {
            get
            {
                if (this.nullStrings != null)
                {
                    return this.nullStrings;
                }

                return new string[0];
            }

            set
            {
                if (value == null)
                {
                    value = new String[0];
                }

                if (this.nullStrings != value)
                {
                    NullStringUtil.TrimAndValidate(value, true);
                    this.nullStrings = value;
                    this.RefreshDisplayText();   
                }                
            }
        }

        /// <summary>
        /// Gets or sets the time entered in the input box.
        /// </summary>
        [Category("Behavior")]
        [Description("The time entered in the text box")]
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true), DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NhsTime Value
        {
            get
            {
                if (this.timeValue == null)
                {
                    this.timeValue = new NhsTime();
                }

                return this.timeValue;
            }

            set
            {
                if (this.timeValue != value)
                {
                    this.timeValue = value;
                    this.RefreshDisplayText();
                    this.NotifyPropertyChanged("Value"); 
                }
            }
        }      

        /// <summary>
        /// The wrapper for Value.TimeType. 
        /// </summary>
        [Category("Behavior")]
        [Description("Wrapper for Value.TimeType")]
        [RefreshProperties(RefreshProperties.All)]
        public TimeType TimeType
        {
            get
            {
                return this.Value.TimeType;
            }

            set
            {
                if (this.Value.TimeType != value)
                {
                    this.Value.TimeType = value;
                    this.RefreshDisplayText();
                    this.toolTip1.RemoveAll();
                    this.toolTip1.SetToolTip(this, this.ToolTip);
                    this.NotifyPropertyChanged("Value"); 
                }
            }
        }

        /// <summary>
        /// The wrapper for Value.TimeValue. 
        /// </summary>
        [Category("Behavior")]
        [Description("Wrapper for Value.TimeValue")]
        [RefreshProperties(RefreshProperties.All)]
        public DateTime TimeValue
        {
            get
            {
                return this.Value.TimeValue;
            }

            set
            {
                if (this.Value.TimeValue != value)
                {
                    this.Value.TimeValue = value;
                    this.RefreshDisplayText();
                    this.NotifyPropertyChanged("Value"); 
                }
            }
        }

        /// <summary>
        /// The wrapper for Value.NullIndex. 
        /// </summary>
        [Category("Behavior")]
        [Description("Wrapper for Value.NullIndex")]
        [RefreshProperties(RefreshProperties.All)]
        public int NullIndex
        {
            get
            {
                return this.Value.NullIndex;
            }

            set
            {
                if (this.Value.NullIndex != value)
                {
                    this.Value.NullIndex = value;
                    this.RefreshDisplayText();
                    this.NotifyPropertyChanged("Value"); 
                }
            }
        }

        /// <summary>
        /// The text displayed when the mouse pointer hovers over the control. 
        /// </summary>
        /// <remarks>
        /// The text for the ToolTip is drawn from TimeResources.MorningDescription, TimeResources.AfternoonDescription, 
        /// TimeResources.EveningDescription or TimeResources.NightDescription according to the 
        /// <see cref="P:NhsCui.Toolkit.WinForms.TimeLabel.TimePeriod"> TimePeriod</see> value.
        /// </remarks>
        [Category("Behavior")]
        [Description("The text that is displayed in the tooltip.")]
        public String ToolTip
        {
            get
            {                              
               return this.toolTip;               
            }

            set
            {
                if (this.toolTip != value)
                {                    
                    this.toolTip = value;                    
                    this.toolTip1.RemoveAll();
                    this.toolTip1.SetToolTip(this, this.toolTip); 
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
                if (base.Text != value)
                {
                    base.Text = value;    
                }                
            }
        }      
        #endregion

        #region Private Functions              

        /// <summary>
        /// Refresh the display text of the control.
        /// </summary>
        private void RefreshDisplayText()
        {
            NhsTime value = this.Value;
            string[] nullValues = this.NullStrings;

            if (value.TimeType != TimeType.NullIndex || nullValues == null ||
                        value.NullIndex < 0 || value.NullIndex >= nullValues.Length)
            {
                base.Text = value.ToString(
                                            (this.TimeType == TimeType.Approximate),
                                            CultureInfo.CurrentCulture,
                                            this.DisplaySeconds,
                                            this.Display12Hour,
                                            this.DisplayAMPM);
            }
            else
            {
                base.Text = nullValues[value.NullIndex];
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
        #endregion      
    }
}
