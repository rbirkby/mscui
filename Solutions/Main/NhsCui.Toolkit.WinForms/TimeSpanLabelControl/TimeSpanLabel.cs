//-----------------------------------------------------------------------
// <copyright file="TimeSpanLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to display a label representing a time span. </summary>
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
    using System.Globalization;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The control used to display a label representing a time span such as the patient's age. 
    /// </summary>
    [DefaultProperty("Value")]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "TimeSpanLabel.bmp")]
    public partial class TimeSpanLabel : Label, INotifyPropertyChanged
    {
        #region Member Vars

        /// <summary>
        ///The time span value displayed by TimeSpanLabel. 
        /// </summary>
        private NhsTimeSpan nhsTimeSpan = new NhsTimeSpan();

        /// <summary>
        /// Length of units to display, i.e. 'd' or 'days' etc
        /// </summary>
        private TimeSpanUnitLength unitLength;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a TimeSpanLabel object. 
        /// </summary>
        public TimeSpanLabel()
        {
            this.AccessibleName = TimeSpanLabelControl.Resources.AccessibleName;
            this.AccessibleDescription = TimeSpanLabelControl.Resources.AccessibleDescription;
            this.AccessibleRole = AccessibleRole.StaticText;

            this.unitLength = TimeSpanUnitLength.Short;

            this.InitializeComponent();
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
        /// The wrapper for Value.From.  
        /// </summary>
        /// <remarks>
        /// Represents the date and time which mark the start of the time span. 
        /// </remarks>
        [Bindable(true), Category("Behavior")]
        [Description("The DateTime that marks the beginning of the time span")]
        public DateTime From
        {
            get
            {
                return this.Value.From;
            }

            set
            {
                if (this.Value.From.Equals(value) == false)
                {
                    this.Value.From = value;
                    this.NotifyPropertyChanged("From");
                    this.RefreshDisplayText();
                }
            }
        }

        /// <summary>
        /// The wrapper for Value.IsAge. 
        /// </summary>
        /// <remarks>
        /// Specifies whether the time span represents an age. 
        /// </remarks> 
        [Bindable(true), Category("Behavior"), DefaultValue(false)]
        [Description("Specifies whether the time span represents an age")]
        public bool IsAge
        {
            get
            {
                return this.Value.IsAge;
            }

            set
            {
                if (value != this.Value.IsAge)
                {
                    this.Value.IsAge = value;
                    this.NotifyPropertyChanged("IsAge");
                    this.RefreshDisplayText();
                }
            }
        }

        /// <summary>
        /// The wrapper for Value.Granularity. 
        /// </summary>
        /// <remarks>
        /// Specifies the largest units to be displayed. If <see cref="P:NhsCui.Toolkit.Web.TimeSpanLabel.IsAge">IsAge</see> is true, the granularity 
        /// is set from the time span.
        /// If <see cref="P:NhsCui.Toolkit.Web.TimeSpanLabel.IsAge">IsAge</see> is false, the granularity is set from the assigned granularity value. 
        /// The granularity may be seconds, minutes,
        /// weeks, hours, days, weeks, months or years.
        /// </remarks>
        [Bindable(true), Category("Behavior"), DefaultValue(TimeSpanUnit.Years)]
        [Description("The granularity of the time span returned by ToString when IsAge is false")]
        public TimeSpanUnit Granularity
        {
            get
            {
                return this.Value.Granularity;
            }

            set
            {
                if (this.Value.Granularity.Equals(value) == false)
                {
                    this.Value.Granularity = value;
                    this.NotifyPropertyChanged("Granularity");
                    this.RefreshDisplayText();
                }
            }
        }

        /// <summary>
        /// The wrapper for Value.Threshold. 
        /// </summary>
        /// <remarks>
        /// Specifies the smallest units to be displayed. If <see cref="P:NhsCui.Toolkit.Web.TimeSpanLabel.IsAge">IsAge</see> is true, the threshold 
        /// is set from the time span.
        /// If <see cref="P:NhsCui.Toolkit.Web.TimeSpanLabel.IsAge">IsAge</see>
        /// is false, the threshold is set from the assigned threshold value. The threshold may be seconds, minutes,
        /// weeks, hours, days, weeks, months or years. 
        /// </remarks>
        [Bindable(true), Category("Behavior"), DefaultValue(TimeSpanUnit.Seconds)]
        [Description("The threshold of the time span returned by ToString when IsAge is false")]
        public TimeSpanUnit Threshold
        {
            get
            {
                return this.Value.Threshold;
            }

            set
            {
                if (this.Value.Threshold.Equals(value) == false)
                {
                    this.Value.Threshold = value;
                    this.NotifyPropertyChanged("Threshold");
                    this.RefreshDisplayText();
                }
            }
        }

        /// <summary>
        /// The wrapper for Value.To. 
        /// </summary>
        /// <remarks>
        /// Represents the date and time which mark the end of the time span. 
        /// </remarks>
        [Bindable(true), Category("Behavior")]
        [Description("The DateTime that marks the end of the time span")]
        public DateTime To
        {
            get
            {
                return this.Value.To;
            }

            set
            {
                if (this.Value.To.Equals(value) == false)
                {
                    this.Value.To = value;
                    this.NotifyPropertyChanged("To");
                    this.RefreshDisplayText();
                }
            }
        }

        /// <summary>
        /// Gets or sets the time span to be displayed in the time span label.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(true), Category("Behavior"), Browsable(false)]
        [Description("The time span to display in the label")]
        public NhsTimeSpan Value
        {
            get
            {
                return this.nhsTimeSpan;
            }

            set
            {
                if (this.nhsTimeSpan.Equals(value) == false)
                {
                    this.nhsTimeSpan = value;
                    this.NotifyPropertyChanged("Value");
                    this.RefreshDisplayText();
                }
            }
        }

        /// <summary>
        /// Specifies whether long or short units are displayed.
        /// </summary>
        /// <remarks>
        /// Defaults to "Short". If long units are specified, the entire unit name is included, such as "month" or "minute". 
        /// If short units are specified, an abbreviated form of the unit is included, for example "m" for month or "min" for minute. 
        /// Singular and plural versions of the short and long units are included automatically as appropriate. 
        /// </remarks>
        [Category("Behavior"), DefaultValue(TimeSpanUnitLength.Short)]
        [Description("The length of the time span units")]
        public TimeSpanUnitLength UnitLength
        {
            get
            {
                return this.unitLength;
            }

            set
            {
                if (!Enum.IsDefined(typeof(TimeSpanUnitLength), value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                if (value != this.UnitLength)
                {
                    this.unitLength = value;
                    this.RefreshDisplayText();
                    this.NotifyPropertyChanged("UnitLength");
                }
            }
        }

        /// <summary>
        /// Gets or sets the text associated with TimeSpanLabel.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [SuppressMessage("Microsoft.Usage", "CA2222:DoNotDecreaseInheritedMemberVisibility", Justification = "Required by spec.")]
        public new string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                if (value != base.Text)
                {
                    base.Text = value;
                    this.NotifyPropertyChanged("Text");
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
            this.RefreshDisplayText();
        }

        /// <summary>
        /// Refreshes the display text.
        /// </summary>
        private void RefreshDisplayText()
        {
            string timeSpan = this.Value.ToString();

            // If there is no span of time AND we are in design mode, render out the control
            // ID rather than an empty string.
            if (timeSpan.Length == 0 && this.DesignMode == true)
            {
                base.Text = string.Format(CultureInfo.InvariantCulture, "[{0}]", this.Name);
            }
            else
            {
                base.Text = this.nhsTimeSpan.ToString(this.UnitLength, CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Raises the property changed event.
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
