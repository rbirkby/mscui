//-----------------------------------------------------------------------
// <copyright file="DateInputBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010.
// All rights reserved
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
// <date>28-Aug-2009</date>
// <summary>
//      Date picker displaying formatted CUI dates.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.Globalization;
    using System.Collections.ObjectModel;
    using Microsoft.Cui.Controls.Common.DateAndTime;

#if !SILVERLIGHT
    using Microsoft.Windows.Controls;
    using System.Collections.Generic;
#endif

    /// <summary>
    /// Date picker displaying formatted CUI dates.
    /// </summary>
    [TemplateVisualState(Name = "SelectedDateCleared", GroupName = "SelectedDateStates")]
    [TemplateVisualState(Name = "SelectedDateChanged", GroupName = "SelectedDateStates")]
    public class DateInputBox : DatePicker
    { 
        /// <summary>
        /// The Watermark Dependency Property.
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(object), typeof(DateInputBox), new PropertyMetadata("Enter a date"));

        /// <summary>
        /// The ShowSelectedDateChangedState Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShowSelectedDateChangedStateProperty =
            DependencyProperty.Register("ShowSelectedDateChangedState", typeof(bool), typeof(DateInputBox), new PropertyMetadata(false));

        /// <summary>
        /// The TextBoxToolTip Dependency Property.
        /// </summary>
        public static readonly DependencyProperty TextBoxToolTipProperty =
            DependencyProperty.Register("TextBoxToolTip", typeof(object), typeof(DateInputBox), null);

        /// <summary>
        /// The CalendarButtonToolTip Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CalendarButtonToolTipProperty =
            DependencyProperty.Register("CalendarButtonToolTip", typeof(object), typeof(DateInputBox), null);

        #region Attached Properties
#if SILVERLIGHT
        /// <summary>
        /// The TodayButtonCalendar Attached Property.
        /// </summary>
        public static readonly DependencyProperty TodayButtonCalendarProperty =
            DependencyProperty.RegisterAttached("TodayButtonCalendar", typeof(System.Windows.Controls.Calendar), typeof(DateInputBox), new PropertyMetadata(null, new PropertyChangedCallback(TodayButtonCalendar_Changed)));

        /// <summary>
        /// The CloseButtonCalendar Attached Property.
        /// </summary>
        public static readonly DependencyProperty CloseButtonCalendarProperty =
            DependencyProperty.RegisterAttached("CloseButtonCalendar", typeof(System.Windows.Controls.Calendar), typeof(DateInputBox), new PropertyMetadata(null, new PropertyChangedCallback(CloseButtonCalendar_Changed)));
#else
        /// <summary>
        /// The TodayButtonCalendar Attached Property.
        /// </summary>
        public static readonly DependencyProperty TodayButtonCalendarProperty =
            DependencyProperty.RegisterAttached("TodayButtonCalendar", typeof(Microsoft.Windows.Controls.Calendar), typeof(DateInputBox), new PropertyMetadata(null, new PropertyChangedCallback(TodayButtonCalendar_Changed)));

        /// <summary>
        /// The CloseButtonCalendar Attached Property.
        /// </summary>
        public static readonly DependencyProperty CloseButtonCalendarProperty =
            DependencyProperty.RegisterAttached("CloseButtonCalendar", typeof(Microsoft.Windows.Controls.Calendar), typeof(DateInputBox), new PropertyMetadata(null, new PropertyChangedCallback(CloseButtonCalendar_Changed)));
#endif
        #endregion

        #region Template Part Names
#if !SILVERLIGHT
        /// <summary>
        /// The TextBox Element Name.
        /// </summary>
        private const string ElementTextBoxName = "PART_TextBox";
#endif

#if SILVERLIGHT
        /// <summary>
        /// The DropDownToggle Element Name.
        /// </summary>
        private const string ElementTextBoxName = "TextBox";
#endif

        /// <summary>
        /// The CalendarButton Element Name.
        /// </summary>
        private const string ElementCalendarButtonName = "CalendarButton";
        #endregion

        /// <summary>
        /// Stores the active date input boxes.
        /// </summary>
        private static Collection<DateInputBox> activeDateInputBoxes = new Collection<DateInputBox>();

#if !SILVERLIGHT
        /// <summary>
        /// Stores the today buttons by calendar.
        /// </summary>
        private static Dictionary<Microsoft.Windows.Controls.Calendar, Button> todayButtonsByCalendar = new Dictionary<Microsoft.Windows.Controls.Calendar, Button>();

        /// <summary>
        /// Stores the close buttons by calendar.
        /// </summary>
        private static Dictionary<Microsoft.Windows.Controls.Calendar, Button> closeButtonsByCalendar = new Dictionary<Microsoft.Windows.Controls.Calendar, Button>();
#endif

        /// <summary>
        /// Stores the text box.
        /// </summary>
        private WatermarkedTextBox textBox;

        /// <summary>
        /// Stores the calendar button.
        /// </summary>
        private Button calendarButton;

        /// <summary>
        /// Stores the selection start when the text changes.
        /// </summary>
        private int selectionStart;

        /// <summary>
        /// Stores the selection length for when the text changes.
        /// </summary>
        private int selectionLength;

#if SILVERLIGHT
        /// <summary>
        /// Stores the calendar.
        /// </summary>
        private System.Windows.Controls.Calendar calendar;
#else
        /// <summary>
        /// Stores the calendar.
        /// </summary>
        private Microsoft.Windows.Controls.Calendar calendar;
#endif

        /// <summary>
        /// DateInputBox constructor.
        /// </summary>
        public DateInputBox()
        {
            DateInputBox.ActiveDateInputBoxes.Add(this);
            this.DefaultStyleKey = typeof(DateInputBox);
            this.SelectedDateChanged += new EventHandler<SelectionChangedEventArgs>(this.DateInputBox_SelectedDateChanged);
            this.LostFocus += new RoutedEventHandler(this.DateInputBox_LostFocus);
            this.CalendarOpened += new RoutedEventHandler(this.DateInputBox_CalendarOpened);
            this.CalendarClosed += new RoutedEventHandler(this.DateInputBox_CalendarClosed);
            this.MouseEnter += new MouseEventHandler(this.DateInputBox_MouseEnter);
            this.MouseLeave += new MouseEventHandler(this.DateInputBox_MouseLeave);
           
#if !SILVERLIGHT
            KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.Local);
#endif
        }

        /// <summary>
        /// Gets or sets the water mark.
        /// </summary>
        /// <value>The watermark.</value>
        public object Watermark
        {
            get { return (object)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the selected date changed state.
        /// </summary>
        /// <value>Whether to show the selected date changed state.</value>
        public bool ShowSelectedDateChangedState
        {
            get { return (bool)GetValue(ShowSelectedDateChangedStateProperty); }
            set { SetValue(ShowSelectedDateChangedStateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text box tool tip.
        /// </summary>
        /// <value>The text box tool tip.</value>
        public object TextBoxToolTip
        {
            get { return (object)GetValue(TextBoxToolTipProperty); }
            set { SetValue(TextBoxToolTipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the calendar button tool tip.
        /// </summary>
        /// <value>The calendar button tool tip.</value>
        public object CalendarButtonToolTip
        {
            get { return (object)GetValue(CalendarButtonToolTipProperty); }
            set { SetValue(CalendarButtonToolTipProperty, value); }
        }

        /// <summary>
        /// Gets the selected date text.
        /// </summary>
        /// <value>The selected date text.</value>
        public string SelectedDateText
        {
            get
            {
                if (this.SelectedDate.HasValue)
                {
                    return new CuiDate(this.SelectedDate.Value).ToString();
                }

                return string.Empty;
            }
        }

#if SILVERLIGHT
        /// <summary>
        /// Gets or sets the controls calendar control.
        /// </summary>
        /// <value>The calendar control.</value>
        public System.Windows.Controls.Calendar Calendar 
        { 
            get
            {
                return this.calendar;
            }

            set
            {
                this.calendar = value;
                if (this.calendar != null)
                {
                    this.calendar.TabNavigation = KeyboardNavigationMode.Local;                    
                }
            }
        }
#else
        /// <summary>
        /// Gets or sets the controls calendar control.
        /// </summary>
        /// <value>The calendar control.</value>
        public Microsoft.Windows.Controls.Calendar Calendar 
        {
            get
            {
                return this.calendar;
            }

            set
            {
                this.calendar = value;

                if (this.calendar != null)
                {
                    KeyboardNavigation.SetTabNavigation(this.calendar, KeyboardNavigationMode.Local);
                }
            }
        }
#endif

        /// <summary>
        /// Gets or sets the active date input box collection.
        /// </summary>
        /// <value>The active date input boxes value.</value>
        internal static Collection<DateInputBox> ActiveDateInputBoxes
        {
            get { return DateInputBox.activeDateInputBoxes; }
            set { DateInputBox.activeDateInputBoxes = value; }
        }

#if !SILVERLIGHT
        /// <summary>
        /// Gets or sets the today buttons by calendar.
        /// </summary>
        /// <value>The today buttons by calendar.</value>
        internal static Dictionary<Microsoft.Windows.Controls.Calendar, Button> TodayButtonsByCalendar
        {
            get { return DateInputBox.todayButtonsByCalendar; }
            set { DateInputBox.todayButtonsByCalendar = value; }
        }

        /// <summary>
        /// Gets or sets the close buttons by calendar.
        /// </summary>
        /// <value>The close buttons by calendar.</value>
        internal static Dictionary<Microsoft.Windows.Controls.Calendar, Button> CloseButtonsByCalendar
        {
            get { return DateInputBox.closeButtonsByCalendar; }
            set { DateInputBox.closeButtonsByCalendar = value; }
        }
#endif

        #region Attached Property Helper Methods
#if SILVERLIGHT
        /// <summary>
        /// Gets the today button calendar.
        /// </summary>
        /// <param name="obj">The today button.</param>
        /// <returns>The today buttoncalendar.</returns>
        public static System.Windows.Controls.Calendar GetTodayButtonCalendar(DependencyObject obj)
        {
            return (System.Windows.Controls.Calendar)obj.GetValue(TodayButtonCalendarProperty);
        }

        /// <summary>
        /// Sets the today button calendar.
        /// </summary>
        /// <param name="obj">The today button.</param>
        /// <param name="value">The calendar.</param>
        public static void SetTodayButtonCalendar(DependencyObject obj, System.Windows.Controls.Calendar value)
        {
            obj.SetValue(TodayButtonCalendarProperty, value);
        }

        /// <summary>
        /// Gets the close button calendar.
        /// </summary>
        /// <param name="obj">The close button.</param>
        /// <returns>The calendar.</returns>
        public static System.Windows.Controls.Calendar GetCloseButtonCalendar(DependencyObject obj)
        {
            return (System.Windows.Controls.Calendar)obj.GetValue(CloseButtonCalendarProperty);
        }

        /// <summary>
        /// Sets the today button calendar.
        /// </summary>
        /// <param name="obj">The close button.</param>
        /// <param name="value">The calendar.</param>
        public static void SetCloseButtonCalendar(DependencyObject obj, System.Windows.Controls.Calendar value)
        {
            obj.SetValue(CloseButtonCalendarProperty, value);
        }
#else
                /// <summary>
        /// Gets the today button calendar.
        /// </summary>
        /// <param name="obj">The today button.</param>
        /// <returns>The today buttoncalendar.</returns>
        public static Microsoft.Windows.Controls.Calendar GetTodayButtonCalendar(DependencyObject obj)
        {
            return (Microsoft.Windows.Controls.Calendar)obj.GetValue(TodayButtonCalendarProperty);
        }

        /// <summary>
        /// Sets the today button calendar.
        /// </summary>
        /// <param name="obj">The today button.</param>
        /// <param name="value">The calendar.</param>
        public static void SetTodayButtonCalendar(DependencyObject obj, Microsoft.Windows.Controls.Calendar value)
        {
            obj.SetValue(TodayButtonCalendarProperty, value);
        }

        /// <summary>
        /// Gets the close button calendar.
        /// </summary>
        /// <param name="obj">The close button.</param>
        /// <returns>The calendar.</returns>
        public static Microsoft.Windows.Controls.Calendar GetCloseButtonCalendar(DependencyObject obj)
        {
            return (Microsoft.Windows.Controls.Calendar)obj.GetValue(CloseButtonCalendarProperty);
        }

        /// <summary>
        /// Sets the today button calendar.
        /// </summary>
        /// <param name="obj">The close button.</param>
        /// <param name="value">The calendar.</param>
        public static void SetCloseButtonCalendar(DependencyObject obj, Microsoft.Windows.Controls.Calendar value)
        {
            obj.SetValue(CloseButtonCalendarProperty, value);
        }
#endif
        #endregion

        /// <summary>
        /// Gets the template parts.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.textBox = this.GetTemplateChild(DateInputBox.ElementTextBoxName) as WatermarkedTextBox;
            if (this.textBox != null)
            {
                this.textBox.TextChanged += new TextChangedEventHandler(this.TextBox_TextChanged);
                this.textBox.LostFocus += new RoutedEventHandler(this.TextBox_LostFocus);
                this.textBox.Click += new MouseButtonEventHandler(this.TextBox_Click);
#if SILVERLIGHT
                this.textBox.KeyDown += new KeyEventHandler(this.TextBox_KeyDown);
#else
                this.textBox.PreviewKeyDown += new KeyEventHandler(this.TextBox_KeyDown);
#endif
                if (this.SelectedDate.HasValue)
                {
                    this.textBox.Text = new CuiDate(this.SelectedDate.Value).ToString();
                }                
            }

            this.calendarButton = this.GetTemplateChild(DateInputBox.ElementCalendarButtonName) as Button;
            if (this.calendarButton != null)
            {
                this.calendarButton.Click += new RoutedEventHandler(this.CalendarButton_Click);                
            }

            if (this.ShowSelectedDateChangedState && this.SelectedDate.HasValue)
            {
                VisualStateManager.GoToState(this, "SelectedDateCleared", false);
                VisualStateManager.GoToState(this, "SelectedDateChanged", true);
            }
        }

        /// <summary>
        /// Updates the today button.
        /// </summary>
        /// <param name="obj">The calendar.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void TodayButtonCalendar_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
#if SILVERLIGHT
            System.Windows.Controls.Calendar calendar = args.NewValue as System.Windows.Controls.Calendar;
#else
            Microsoft.Windows.Controls.Calendar calendar = args.NewValue as Microsoft.Windows.Controls.Calendar;
#endif
            Button button = obj as Button;
#if !SILVERLIGHT
            bool added = false;
#endif
            foreach (DateInputBox dateInputBox in DateInputBox.ActiveDateInputBoxes)
            {
                if (dateInputBox.Calendar == calendar)
                {
#if !SILVERLIGHT
                    added = true;
#endif
                    dateInputBox.UpdateTodayButton(button);
                }
            }

#if !SILVERLIGHT
            if (!added)
            {
                DateInputBox.TodayButtonsByCalendar.Add(calendar, button);
            }
#endif
        }

        /// <summary>
        /// Updates the close button.
        /// </summary>
        /// <param name="obj">The calendar.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void CloseButtonCalendar_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
#if SILVERLIGHT
            System.Windows.Controls.Calendar calendar = args.NewValue as System.Windows.Controls.Calendar;
#else
            Microsoft.Windows.Controls.Calendar calendar = args.NewValue as Microsoft.Windows.Controls.Calendar;
#endif
            Button button = obj as Button;
#if !SILVERLIGHT
                   bool added = false;
#endif
            foreach (DateInputBox dateInputBox in DateInputBox.ActiveDateInputBoxes)
            {
                if (dateInputBox.Calendar == calendar)
                {
#if !SILVERLIGHT
                    added = true;
#endif
                    dateInputBox.UpdateCloseButton(button);
                }
            }

#if !SILVERLIGHT
            if (!added)
            {
                DateInputBox.CloseButtonsByCalendar.Add(calendar, button);
            }
#endif
        }

        /// <summary>
        /// Adds the event handler to the today button.
        /// </summary>
        /// <param name="todayButton">The today button.</param>
        private void UpdateTodayButton(Button todayButton)
        {
            if (todayButton != null)
            {
                todayButton.Click += new RoutedEventHandler(this.TodayButton_Click);
            }
        }

        /// <summary>
        /// Sets the selected date to today.
        /// </summary>
        /// <param name="sender">The today button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void TodayButton_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedDate = DateTime.Today;
            this.IsDropDownOpen = false;
        }

        /// <summary>
        /// Adds the click event handler to the close button.
        /// </summary>
        /// <param name="closeButton">The close button.</param>
        private void UpdateCloseButton(Button closeButton)
        {
            if (closeButton != null)
            {
                closeButton.Click += new RoutedEventHandler(this.CloseButton_Click);
            }
        }

        /// <summary>
        /// Closes the calendar.
        /// </summary>
        /// <param name="sender">The close button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsDropDownOpen = false;
        }

        /// <summary>
        /// Opens the calendar.
        /// </summary>
        /// <param name="sender">The calendar button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void CalendarButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsDropDownOpen = true;
        }

        /// <summary>
        /// Adds event handlers to today and close buttons.
        /// </summary>
        /// <param name="sender">The calendar.</param>
        /// <param name="e">Routed Event Args.</param>
        private void DateInputBox_CalendarOpened(object sender, RoutedEventArgs e)
        {
#if SILVERLIGHT
            this.Calendar = ((this.GetTemplateChild("Popup") as System.Windows.Controls.Primitives.Popup).Child as Panel).Children[1] as System.Windows.Controls.Calendar;
#else
            this.Calendar = (this.GetTemplateChild("PART_Popup") as System.Windows.Controls.Primitives.Popup).Child as Microsoft.Windows.Controls.Calendar;

            if (this.Calendar != null)
            {   
                this.Calendar.SelectedDate = this.SelectedDate;
                FocusHelper.FocusControl(this.Calendar);
            }

            if (DateInputBox.TodayButtonsByCalendar.ContainsKey(this.Calendar))
            {
                this.UpdateTodayButton(DateInputBox.TodayButtonsByCalendar[this.Calendar]);
                DateInputBox.TodayButtonsByCalendar.Remove(this.Calendar);
            }

            if (DateInputBox.CloseButtonsByCalendar.ContainsKey(this.Calendar))
            {
                this.UpdateCloseButton(DateInputBox.CloseButtonsByCalendar[this.Calendar]);
                DateInputBox.CloseButtonsByCalendar.Remove(this.Calendar);
            }
#endif
            if (this.Calendar != null)
            {
                FocusHelper.FocusControl(this.Calendar);
            }
        }

        /// <summary>
        /// Sets focus back to the date input box.
        /// </summary>
        /// <param name="sender">The date input box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void DateInputBox_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (this.calendarButton != null)
            {
                this.calendarButton.Focus();
            }

            CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_SetTextBoxText);
            CompositionTarget.Rendering += new EventHandler(this.CompositionTargetRendering_SetTextBoxText);
        }

        /// <summary>
        /// Sets the text box text, and selects all.
        /// </summary>
        /// <param name="sender">The composition target.</param>
        /// <param name="e">Event Args.</param>
        private void CompositionTargetRendering_SetTextBoxText(object sender, EventArgs e)
        {
            CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_SetTextBoxText);
            this.textBox.Text = this.SelectedDateText;
            FocusHelper.FocusControl(this.textBox);
        }

        /// <summary>
        /// Updates the date or opens the calendar.
        /// </summary>
        /// <param name="sender">The text box.</param>
        /// <param name="e">Key Event Args.</param>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            bool textMatchesDate = this.textBox.Text == this.SelectedDateText;
            if (e.Key == Key.Down)
            {
                if (this.SelectedDate.HasValue && textMatchesDate && this.textBox.SelectionStart == 0 && this.textBox.SelectionLength == 2)
                {
                    this.selectionStart = 0;
                    this.selectionLength = 2;
                    this.SelectedDate = this.SelectedDate.Value.AddDays(1);
                }
                else if (this.SelectedDate.HasValue && textMatchesDate && this.textBox.SelectionStart == 3 && this.textBox.SelectionLength == 3)
                {
                    this.selectionStart = 3;
                    this.selectionLength = 3;
                    this.SelectedDate = this.SelectedDate.Value.AddMonths(1);
                }
                else if (this.SelectedDate.HasValue && textMatchesDate && this.textBox.SelectionStart == 7 && this.textBox.SelectionLength == 4)
                {
                    this.selectionStart = 7;
                    this.selectionLength = 4;
                    this.SelectedDate = this.SelectedDate.Value.AddYears(1);
                }
            }
            else if (e.Key == Key.Up)
            {
                if (this.SelectedDate.HasValue && textMatchesDate && this.textBox.SelectionStart == 0 && this.textBox.SelectionLength == 2)
                {
                    this.selectionStart = 0;
                    this.selectionLength = 2;
                    this.SelectedDate = this.SelectedDate.Value.AddDays(-1);
                }
                else if (this.SelectedDate.HasValue && textMatchesDate && this.textBox.SelectionStart == 3 && this.textBox.SelectionLength == 3)
                {
                    this.selectionStart = 3;
                    this.selectionLength = 3;
                    this.SelectedDate = this.SelectedDate.Value.AddMonths(-1);
                }
                else if (this.SelectedDate.HasValue && textMatchesDate && this.textBox.SelectionStart == 7 && this.textBox.SelectionLength == 4)
                {
                    this.selectionStart = 7;
                    this.selectionLength = 4;
                    this.SelectedDate = this.SelectedDate.Value.AddYears(-1);
                }
            }
        }

        /// <summary>
        /// Selects a specific part of the text.
        /// </summary>
        /// <param name="sender">The text box.</param>
        /// <param name="e">Mouse Button Event Args.</param>
        private void TextBox_Click(object sender, MouseButtonEventArgs e)
        {
            bool textMatchesDate = this.textBox.Text == this.SelectedDateText;
            if (textMatchesDate && this.textBox.SelectionStart >= 0 && this.textBox.SelectionStart < 3 && this.textBox.SelectionLength == 0)
            {
                this.textBox.Select(0, 2);
            }
            else if (textMatchesDate && this.textBox.SelectionStart >= 3 && this.textBox.SelectionStart < 7 && this.textBox.SelectionLength == 0)
            {
                this.textBox.Select(3, 3);
            }
            else if (textMatchesDate && this.textBox.SelectionStart >= 7 && this.textBox.SelectionStart < 11 && this.selectionLength == 0)
            {
                this.textBox.Select(7, 4);
            }
            else if (textMatchesDate && this.textBox.SelectionLength == 0)
            {
                this.textBox.SelectAll();
            }
        }

        /// <summary>
        /// Updates the selected date.
        /// </summary>
        /// <param name="sender">The text box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.UpdateSelectedDate();
        }

        /// <summary>
        /// Updates the selected date.
        /// </summary>
        /// <param name="sender">The text box.</param>
        /// <param name="e">Text Changed Event Args.</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.selectionStart > 0 || this.selectionLength > 0)
            {
                CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_SelectText);
                CompositionTarget.Rendering += new EventHandler(this.CompositionTargetRendering_SelectText);
            }
        }

        /// <summary>
        /// Selects the text after the target render.
        /// </summary>
        /// <param name="sender">The composition target.</param>
        /// <param name="e">Event Args.</param>
        private void CompositionTargetRendering_SelectText(object sender, EventArgs e)
        {
            CompositionTarget.Rendering -= new EventHandler(this.CompositionTargetRendering_SelectText);
            if (this.selectionStart > 0 || this.selectionLength > 0)
            {
                this.textBox.Select(this.selectionStart, this.selectionLength);
                this.selectionStart = 0;
                this.selectionLength = 0;
            }
        }

        /// <summary>
        /// Updates the selected date.
        /// </summary>
        /// <param name="sender">The date input box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void DateInputBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.UpdateSelectedDate();
        }

        /// <summary>
        /// Updates the selected date with the text box text.
        /// </summary>
        private void UpdateSelectedDate()
        {
            if (this.textBox != null)
            {
                CuiDate cuiDate = null;
                if (CuiDate.TryParseExact(this.textBox.Text, out cuiDate, CultureInfo.CurrentCulture))
                {
                }
                else if (CuiDate.IsAddValid(this.textBox.Text))
                {
                    cuiDate = CuiDate.Add(new CuiDate(DateTime.Now), this.textBox.Text);
                }

                if (cuiDate != null && !cuiDate.IsNull && cuiDate.ToString().Length >= 11 && (!this.SelectedDate.HasValue || cuiDate.DateValue != this.SelectedDate.Value.Date))
                {
                    this.SelectedDate = cuiDate.DateValue;
                }

                this.textBox.Text = this.SelectedDateText;
            }
        }

        /// <summary>
        /// Updates the date in the text box, if necessary.
        /// </summary>
        /// <param name="sender">The date input box.</param>
        /// <param name="e">Selection Changed Event Args.</param>
        private void DateInputBox_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.textBox != null)
            {
                CuiDate cuiDate = new CuiDate(this.textBox.Text);                
                if (string.IsNullOrEmpty(this.textBox.Text) || cuiDate.DateValue != this.SelectedDate)
                {
                    this.textBox.Text = this.SelectedDateText;
                }
            }

            if (this.ShowSelectedDateChangedState)
            {
                VisualStateManager.GoToState(this, "SelectedDateCleared", false);
                VisualStateManager.GoToState(this, "SelectedDateChanged", true);
            }
        }

        /// <summary>
        /// Goes to the normal visual state.
        /// </summary>
        /// <param name="sender">The Date Input Box.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void DateInputBox_MouseLeave(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", true);
        }

        /// <summary>
        /// Goes to the mouse over visual state.
        /// </summary>
        /// <param name="sender">The Date Input Box.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void DateInputBox_MouseEnter(object sender, MouseEventArgs e)
        {
            VisualStateManager.GoToState(this, "MouseOver", true);
        }
    }
}
