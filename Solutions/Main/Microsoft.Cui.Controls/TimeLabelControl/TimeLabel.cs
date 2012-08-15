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
// <date>28-May-2009</date>
// <summary>The control used to display time. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using Microsoft.Cui.Controls.Common.DateAndTime;

    #endregion

    /// <summary>
    /// The control used to display a timelabel. 
    /// </summary>    
    public class TimeLabel : BaseLabel
    {
        #region Dependency Properties

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeLabel.Display12Hour"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty Display12HourProperty = DependencyProperty.Register(
                                                                                    "Display12Hour",
                                                                                    typeof(bool),
                                                                                    typeof(TimeLabel),
                                                                                    new PropertyMetadata(false, new PropertyChangedCallback(OnTimeChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeLabel.DisplayAMPM"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplayAMPMProperty = DependencyProperty.Register(
                                                                                   "DisplayAMPM",
                                                                                   typeof(bool),
                                                                                   typeof(TimeLabel),
                                                                                   new PropertyMetadata(false, new PropertyChangedCallback(OnTimeChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeLabel.DisplaySeconds"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplaySecondsProperty = DependencyProperty.Register(
                                                                                   "DisplaySeconds",
                                                                                   typeof(bool),
                                                                                   typeof(TimeLabel),
                                                                                   new PropertyMetadata(false, new PropertyChangedCallback(OnTimeChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeLabel.NullIndex"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty NullIndexProperty = DependencyProperty.Register(
                                                                                   "NullIndex",
                                                                                   typeof(int),
                                                                                   typeof(TimeLabel),
                                                                                   new PropertyMetadata(-1, new PropertyChangedCallback(OnTimeChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeLabel.NullStrings"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty NullStringsProperty = DependencyProperty.Register(
                                                                                   "NullStrings",
                                                                                  typeof(ObservableCollection<string>),
                                                                                   typeof(TimeLabel),
                                                                                   new PropertyMetadata(new PropertyChangedCallback(OnTimeChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeLabel.TimeType"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TimeTypeProperty = DependencyProperty.Register(
                                                                                  "TimeType",
                                                                                  typeof(TimeType),
                                                                                  typeof(TimeLabel),
                                                                                  new PropertyMetadata(TimeType.Exact, new PropertyChangedCallback(OnTimeChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeLabel.TimeValue"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TimeValueProperty = DependencyProperty.Register(
                                                                                          "TimeValue",
                                                                                          typeof(DateTime),
                                                                                          typeof(TimeLabel),
                                                                                          new PropertyMetadata(new PropertyChangedCallback(OnTimeChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.TimeLabel.Value"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                                                                                          "Value",
                                                                                          typeof(CuiTime),
                                                                                          typeof(TimeLabel),
                                                                                          new PropertyMetadata(new PropertyChangedCallback(OnTimeChanged)));

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeLabel"/> class.
        /// </summary>
        public TimeLabel()
        {
            this.ShowHandOnHover = false;
            this.Value = new CuiTime();
            this.NullStrings = new ObservableCollection<string>();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets a value indicating whether the hours value should be displayed in 12-hour or 24-hour format.
        /// </summary>
        /// <value><c>True</c> hours value is displayed in 12-hour format.<c>False</c>hours value is displayed in 24-hour format.</value>
        [Category("Behavior")]
        [Description("Specifies whether hours should be displayed as 12 hour or 24 hour")]
        public bool Display12Hour
        {
            get { return (bool)this.GetValue(Display12HourProperty); }
            set { this.SetValue(Display12HourProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether an AM/PM suffix should be included.
        /// </summary>
        /// <value><c>True</c>specifies an AM/PM suffix is included.<c>False</c>specifies an AM/PM suffix is not included.</value>
        [Category("Behavior")]
        [Description("Specifies whether the AM/PM suffix should be included")]
        public bool DisplayAMPM
        {
            get { return (bool)this.GetValue(DisplayAMPMProperty); }
            set { this.SetValue(DisplayAMPMProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether seconds should be displayed.
        /// </summary>
        /// <value><c>True</c>seconds is displayed.<c>False</c>seconds is not displayed.</value>
        [Category("Behavior")]
        [Description("Specifies whether seconds should be displayed")]
        public bool DisplaySeconds
        {
            get { return (bool)this.GetValue(DisplaySecondsProperty); }
            set { this.SetValue(DisplaySecondsProperty, value); }
        }

        /// <summary>
        /// Gets or sets a number identifying a null index type (defaults to -1).
        /// The index has no intrinsic meaning by itself. The meaning is implied by the UI control.
        /// </summary>
        /// <value>The index of the null.</value>
        [Category("Data")]
        [Description("Gets or sets a number identifying a null index type (defaults to -1). The index has no intrinsic meaning by itself. The meaning is implied by the UI control")]
        public int NullIndex
        {
            get { return (int)this.GetValue(NullIndexProperty); }
            set { this.SetValue(NullIndexProperty, value); }
        }

        /// <summary>
        /// Gets or sets the null strings.
        /// </summary>
        /// <value>The null strings.</value>        
        [TypeConverter(typeof(StringArrayTypeConverter))]
        [Category("Data")]
        [Description("Gets or sets a list of localized strings that identify different types of null index times (defaults to an empty list)")]
        public ObservableCollection<string> NullStrings
        {
            get { return (ObservableCollection<string>)this.GetValue(NullStringsProperty); }
            set { this.SetValue(NullStringsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the type of the time.
        /// </summary>
        /// <value>The type of the time.</value>
        [Category("Data")]
        [Description("Gets or sets the TimeType that represents the time.")]
        public TimeType TimeType
        {
            get { return (TimeType)this.GetValue(TimeTypeProperty); }
            set { this.SetValue(TimeTypeProperty, value); }
        }
       
        /// <summary>
        /// Gets or sets the time value.
        /// </summary>
        /// <value>The time value.</value>
        /// <remarks>
        /// It is recommended that the property TimeValue be used while specifying
        /// values through XAML. Example:TimeValue="11:30:20". The Value property
        /// cannot be initialized in this manner due to absence of a custom type converter
        /// that can convert a string to a valid instance of CuiTime.
        /// </remarks>
        [Category("Data")]
        [TypeConverter(typeof(DateTimeConverter))]
        [Description("Gets or sets the exact or approximate time value.")]
        public DateTime TimeValue
        {
            get { return (DateTime)this.GetValue(TimeValueProperty); }
            set { this.SetValue(TimeValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>        
        [Category("Data")]
        [Description("Gets or sets the time to display in the label.")]
        public CuiTime Value
        {
            get { return (CuiTime)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }
        #endregion

        #region Overridden Methods

        /// <summary>
        /// Updates the display value of the Time label control.
        /// </summary>
        protected override void UpdateDisplayValue()
        {
            try
            {
                this.CanChangeDisplayValue = true;
                if (this.TimeType == TimeType.NullIndex && this.NullStrings != null &&
                        this.NullIndex >= 0 && this.NullIndex < this.NullStrings.Count)
                {
                    this.DisplayValue = this.NullStrings[this.NullIndex];
                }
                else
                {
                    this.DisplayValue = this.Value == null ? string.Empty : this.Value.ToString(this.TimeType == TimeType.Approximate, CultureInfo.CurrentCulture, this.DisplaySeconds, this.Display12Hour, this.DisplayAMPM);
                }

                this.CanChangeDisplayValue = false;
            }
            catch (ArgumentException)
            {
                this.CanChangeDisplayValue = true;
                this.SetValue(DisplayValueProperty, string.Empty);
                this.CanChangeDisplayValue = false;
                throw;
            }
        }

        #endregion

        #region Automation
        /// <summary>
        /// Automation object for the time label.
        /// </summary>
        /// <returns>Automation object for Time label.</returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new TimeLabelAutomationPeer(this);
        }

        #endregion

        #region Property Changed Callbacks

        /// <summary>
        /// Handle the Time property change event.
        /// </summary>
        /// <param name="d">Time label whose time is changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TimeLabel lblTime = d as TimeLabel;

            if (lblTime != null)
            {
                if (e.NewValue == null && e.Property == ValueProperty)
                {
                    throw new ArgumentNullException(e.Property.ToString());
                }

                if (e.OldValue != e.NewValue)
                {
                    if (e.Property == NullIndexProperty)
                    {
                        lblTime.Value.NullIndex = (int)e.NewValue;
                    }
                    else if (e.Property == TimeValueProperty)
                    {
                        lblTime.Value.TimeValue = (DateTime)e.NewValue;
                    }
                    else if (e.Property == TimeTypeProperty)
                    {
                        lblTime.Value.TimeType = (TimeType)e.NewValue;
                    }
                    else if (e.Property == ValueProperty)
                    {
                        CuiTime time = e.NewValue as CuiTime;
                        if (time != null)
                        {
                            lblTime.TimeType = time.TimeType;
                            lblTime.TimeValue = time.TimeValue;
                            lblTime.NullIndex = time.NullIndex;
                        }
                    }

                    lblTime.UpdateDisplayValue();

                    TimeLabelAutomationPeer peer;

#if SILVERLIGHT
                    peer = FrameworkElementAutomationPeer.FromElement(lblTime) as TimeLabelAutomationPeer;
#else
                peer = UIElementAutomationPeer.FromElement(lblTime) as TimeLabelAutomationPeer;
#endif

                    if (peer != null)
                    {
                        peer.RaiseValueChangedEvent(e.OldValue.ToString(), e.NewValue.ToString());
                    }
                }
            }
        }
        #endregion
    }
}
