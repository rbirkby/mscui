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
// <date>27-May-2009</date>
// <summary>The control used to display date. </summary>
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
    /// The control used to display date.
    /// </summary>
    public class DateLabel : BaseLabel
    {
        #region Dependency Properties
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.DateLabel.DateTypeProperty"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DateTypeProperty =
            DependencyProperty.Register("DateType", typeof(DateType), typeof(DateLabel), new PropertyMetadata(DateType.Exact, new PropertyChangedCallback(OnPropertyChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.DateLabel.DateValueProperty"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DateValueProperty =
            DependencyProperty.Register("DateValue", typeof(DateTime), typeof(DateLabel), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.DateLabel.DisplayDateAsTextProperty"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplayDateAsTextProperty =
            DependencyProperty.Register("DisplayDateAsText", typeof(bool), typeof(DateLabel), new PropertyMetadata(false, new PropertyChangedCallback(OnPropertyChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.DateLabel.DisplayDayOfWeekProperty"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DisplayDayOfWeekProperty =
            DependencyProperty.Register("DisplayDayOfWeek", typeof(bool), typeof(DateLabel), new PropertyMetadata(false, new PropertyChangedCallback(OnPropertyChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.DateLabel.MonthProperty"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MonthProperty =
            DependencyProperty.Register("Month", typeof(int), typeof(DateLabel), new PropertyMetadata(1, new PropertyChangedCallback(OnPropertyChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.DateLabel.NullIndexProperty"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty NullIndexProperty =
            DependencyProperty.Register("NullIndex", typeof(int), typeof(DateLabel), new PropertyMetadata(-1, new PropertyChangedCallback(OnPropertyChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.DateLabel.NullStringsProperty"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty NullStringsProperty =
            DependencyProperty.Register("NullStrings", typeof(ObservableCollection<string>), typeof(DateLabel), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.DateLabel.YearProperty"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty YearProperty =
            DependencyProperty.Register("Year", typeof(int), typeof(DateLabel), new PropertyMetadata(0, new PropertyChangedCallback(OnPropertyChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.DateLabel.ValueProperty"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(CuiDate), typeof(DateLabel), new PropertyMetadata(new PropertyChangedCallback(OnPropertyChanged)));
        #endregion

        #region Member variables
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DateLabel"/> class.
        /// </summary>
        public DateLabel()
        {
            this.ShowHandOnHover = false;
            this.Value = new CuiDate();
            this.NullStrings = new ObservableCollection<string>();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the DateType that represents the date.
        /// </summary>
        /// <value>The type of the date.</value>
        [Category("Data")]
        [Description("Gets or sets the DateType that represents the date.")]
        public DateType DateType
        {
            get { return (DateType)GetValue(DateTypeProperty); }
            set { SetValue(DateTypeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the exact or approximate date value.
        /// </summary>
        /// <value>The date value.</value>
        /// <remarks>
        /// It is recommended that the property DateValue be used while specifying
        /// values through XAML. Example:DateValue="11/11/2009". The Value property
        /// cannot be initialized in this manner due to absence of a custom type converter
        /// that can convert a string to a valid instance of CuiDate.
        /// </remarks>
        [Category("Data")]
        [TypeConverter(typeof(DateTimeConverter))]
        [Description("Gets or sets the exact or approximate date value.")]
        public DateTime DateValue
        {
            get { return (DateTime)GetValue(DateValueProperty); }
            set { SetValue(DateValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text "Today", "Tomorrow" or "Yesterday" is displayed in place of the date.
        /// </summary>
        /// <value><c>True</c> if date displayed as text; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [Description("Gets or sets a value indicating whether the text Today, Tomorrow or Yesterday is displayed in place of the date.")]
        public bool DisplayDateAsText
        {
            get { return (bool)GetValue(DisplayDateAsTextProperty); }
            set { SetValue(DisplayDateAsTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the name of the day is displayed with the date.
        /// </summary>
        /// <value><c>True</c> if day of week to be displayed; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [Description("Gets or sets a value indicating whether the name of the day is displayed with the date.")]
        public bool DisplayDayOfWeek
        {
            get { return (bool)GetValue(DisplayDayOfWeekProperty); }
            set { SetValue(DisplayDayOfWeekProperty, value); }
        }

        /// <summary>
        /// Gets or sets the month of a DateType.YearMonth date.
        /// </summary>
        /// <value>The month.</value>
        [Category("Data")]
        [Description("Gets or sets the month of a DateType.YearMonth date.")]
        public int Month
        {
            get { return (int)GetValue(MonthProperty); }
            set { SetValue(MonthProperty, value); }
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
            get { return (int)GetValue(NullIndexProperty); }
            set { SetValue(NullIndexProperty, value); }
        }

        /// <summary>
        /// Gets or sets a list of localized strings that identify different types of null index dates (defaults to an empty list).
        /// </summary>
        /// <value>The list of localized strings that identify different types of null index dates (defaults to an empty list).</value>
        [Category("Data")]
        [Description("Gets or sets a list of localized strings that identify different types of null index dates (defaults to an empty list)")]
        public ObservableCollection<string> NullStrings
        {
            get { return (ObservableCollection<string>)GetValue(NullStringsProperty); }
            set { SetValue(NullStringsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the year of a DateType.Year, DateType.YearMonth or DateType.
        /// </summary>
        /// <value>The year of a DateType.Year, DateType.YearMonth or DateType.</value>
        [Category("Data")]
        [Description("Gets or sets the year of a DateType.Year, DateType.YearMonth or DateType.")]
        public int Year
        {
            get { return (int)GetValue(YearProperty); }
            set { SetValue(YearProperty, value); }
        }

        /// <summary>
        /// Gets or sets the date to display in the label.
        /// </summary>
        /// <value>The date to display in the label.</value>
        [Category("Data")]
        [Description("Gets or sets the date to display in the label.")]
        public CuiDate Value
        {
            get { return (CuiDate)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Updates the display value based on supplied values.
        /// </summary>
        protected override void UpdateDisplayValue()
        {
            base.UpdateDisplayValue();
            this.CanChangeDisplayValue = true;
            if (this.DateType == DateType.NullIndex && this.NullIndex >= 0 &&
                    this.NullStrings != null && this.NullIndex < this.NullStrings.Count)
            {
                this.DisplayValue = this.NullStrings[this.NullIndex];
            }
            else
            {
                this.DisplayValue = this.Value == null ? string.Empty : this.Value.ToString(this.DisplayDayOfWeek, this.DateType == DateType.Approximate, this.DisplayDateAsText, CultureInfo.CurrentCulture);
            }

            this.CanChangeDisplayValue = false;
        }
        #endregion

        #region Automation
        /// <summary>
        /// Automation object for the date label.
        /// </summary>
        /// <returns>
        /// Automation object for date label.
        /// </returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new DateLabelAutomationPeer(this);
        }
        #endregion

        #region Property Changed Callbacks
        /// <summary>
        /// Called when property changes.
        /// </summary>
        /// <param name="d">The dependency object.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                DateLabel dateLabel = d as DateLabel;
                if (dateLabel != null)
                {
                    if (dateLabel.Value != null)
                    {
                        if (e.Property == DateTypeProperty)
                        {
                            if (Enum.IsDefined(typeof(DateType), e.NewValue))
                            {
                                dateLabel.Value.DateType = (DateType)e.NewValue;
                            }
                            else
                            {
                                dateLabel.Value.DateType = DateType.Exact;
                            }
                        }
                        else if (e.Property == DateValueProperty)
                        {
                            dateLabel.Value.DateValue = (DateTime)e.NewValue;
                        }
                        else if (e.Property == MonthProperty)
                        {
                            dateLabel.Value.Month = (int)e.NewValue;
                        }
                        else if (e.Property == NullIndexProperty)
                        {
                            dateLabel.Value.NullIndex = (int)e.NewValue;
                        }
                        else if (e.Property == YearProperty)
                        {
                            dateLabel.Value.Year = (int)e.NewValue;
                        }
                        else if (e.Property == ValueProperty)
                        {
                            CuiDate cd = e.NewValue as CuiDate;
                            if (cd != null)
                            {
                                dateLabel.DateType = cd.DateType;
                                dateLabel.DateValue = cd.DateValue;
                                dateLabel.Month = cd.Month;
                                dateLabel.NullIndex = cd.NullIndex;
                                dateLabel.Year = cd.Year;
                            }
                        }
                    }

                    dateLabel.UpdateDisplayValue();
                    AutomationPeerValueChanged(e, dateLabel);
                }
            }
        }

        /// <summary>
        /// Raises the value changed event for the DateLabel automation peer.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        /// <param name="dateLabel">The date label.</param>
        private static void AutomationPeerValueChanged(DependencyPropertyChangedEventArgs e, DateLabel dateLabel)
        {
            DateLabelAutomationPeer peer;
#if SILVERLIGHT
            peer = FrameworkElementAutomationPeer.FromElement(dateLabel) as DateLabelAutomationPeer;
#else
                    peer = UIElementAutomationPeer.FromElement(dateLabel) as DateLabelAutomationPeer;
#endif

            if (peer != null)
            {
                peer.RaiseValueChangedEvent(e.OldValue.ToString(), e.NewValue.ToString());
            }
        }
        #endregion
    }
}
