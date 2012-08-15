//-----------------------------------------------------------------------
// <copyright file="NhsCalendar.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>3-May-2007</date>
// <summary>The calendar control used to select a date.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.WinForms
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Windows.Forms;
    using System.Globalization;
    using System.Security.Permissions;
    using NhsCui.Toolkit.DateAndTime;

    /// <summary>
    /// Control to display a date calendar.
    /// </summary>
    internal partial class NhsCalendar : Form
    {
        #region Constants

        /// <summary>
        /// top gap for main body of calendar.
        /// </summary>
        private const int TopGap = 28;

        /// <summary>
        /// Horizontol gap in top controls.
        /// </summary>
        private const int TopHorizontolGap = 4;

        /// <summary>
        /// Start for left most navigation button
        /// </summary>
        private const int LeftButtonStart = 4;

        /// <summary>
        /// Keydown message value.
        /// </summary>
        private const int WmKeyDown = 256;

        /// <summary>
        /// Top padding of border around selected calender date.
        /// </summary>
        private const int DayStringTopPad = 5;

        /// <summary>
        /// Height padding of border around selected calender date.
        /// </summary>
        private const int CoveringRectHeightPad = 2;

        /// <summary>
        /// Width padding of border around selected calender date.
        /// </summary>
        private const int CoveringRectWidthPad = 2;

        /// <summary>
        /// Top padding for covering rectangle of a date.
        /// </summary>
        private const int CoveringRectTopPad = 1;

        /// <summary>
        /// Right offset for combo box.
        /// </summary>
        private const int ComboOffset = 20;

        /// <summary>
        /// Offset of starting day of calendar.
        /// </summary>
        /// <remarks>
        /// In our case calendar starts from Monday, so offset is one day. (Sunday treated as start).
        /// </remarks>
        private const int DayOffset = 1;

        /// <summary>
        /// Width of day header.
        /// </summary>
        private const int DayHeaderWidth = 25;

        /// <summary>
        /// Margin for null list combo.
        /// </summary>
        private const int ComboMargin = 6;
        #endregion

        #region Member Vars

        /// <summary>
        /// Current selected X Axis.
        /// </summary>
        private int currentXAxis;

        /// <summary>
        /// Current selected Y Axis.
        /// </summary>
        private int currentYAxis;

        /// <summary>
        /// Current month
        /// </summary>
        private int currentMonth = DateTime.Now.Month;

        /// <summary>
        /// Current year.
        /// </summary>
        private int currentYear = DateTime.Now.Year;

        /// <summary>
        /// Current day.
        /// </summary>
        private int currentDay;

        /// <summary>
        /// Selected Date by the end user.
        /// </summary>       
        private DateTime selectedDate = DateTime.Now;

        /// <summary>
        /// Backcolor of selected day.
        /// </summary>
        private Color selectedDayBackColor = SystemColors.ScrollBar;

        /// <summary>
        /// Font color of selected day.
        /// </summary>
        private Color selectedDayFontColor = SystemColors.ControlText;      
     
        /// <summary>
        /// Font color for active day.
        /// </summary>
        private Color activeDayFontColor = SystemColors.ControlText;

        /// <summary>
        /// Back color for active day.
        /// </summary>
        private Color activeDayBackColor = SystemColors.ScrollBar;

        /// <summary>
        /// Font color for weekend.
        /// </summary>
        private Color weekendDayFontColor = SystemColors.WindowText;

        /// <summary>
        /// Font for weekend day.
        /// </summary>
        private Font weekendDayFont = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular);
        
        /// <summary>
        /// Background color of normal days in calendar.
        /// </summary>
        private Color normalDayBackColor = SystemColors.Window;        

        /// <summary>
        /// Font of normal days.
        /// </summary>
        private Font normalDayFont = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);

        /// <summary>
        /// Font color of normal days.
        /// </summary>
        private Color normalDayFontColor = SystemColors.WindowText;

        /// <summary>
        /// Back color of header displaying days of week.
        /// </summary>
        private Color dayHeaderBackColor = SystemColors.Control;

        /// <summary>
        /// Week days header font.
        /// </summary>
        private Font dayHeaderFont = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular);

        /// <summary>
        /// Font color for week day header.
        /// </summary>
        private Color dayHeaderFontColor = SystemColors.WindowText;

        /// <summary>
        /// Back color of header displaying month and year navigators.
        /// </summary>
        private Color mainHeaderBackColor = SystemColors.ScrollBar;
     
        /// <summary>
        /// Main header font.
        /// </summary>
        private Font mainHeaderFont = new Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular);

        /// <summary>
        /// Font for inactive day.
        /// </summary>
        private Font inactiveDayFont = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular);
        
        /// <summary>
        /// Font Color of inactive days.
        /// </summary>
        private Color inactiveDayFontColor = SystemColors.GrayText;

        /// <summary>
        /// Background color of calendar.
        /// </summary>
        private Color calendarBackColor = SystemColors.Window;        

        /// <summary>
        /// Array of rectangle representing the calendar.
        /// </summary>
        private Rectangle[][] rects;

        /// <summary>
        /// Stores the header rectangles.
        /// </summary>
        private Rectangle[] rectDays;

        /// <summary>
        /// List of abbreviated days.
        /// </summary>
        private string[] abbrDays = new string[7];

        /// <summary>
        /// List of days.
        /// </summary>
        private string[] days = new string[7];

        /// <summary>
        /// List of months.
        /// </summary>
        private string[] months = new string[12];

        /// <summary>
        /// List of abbreviated Months.
        /// </summary>
        private string[] abbrMonths = new string[12];

        /// <summary>
        /// holds the design flag.
        /// </summary>
        /// <remarks>
        /// Set to false from load event after first time rendering.
        /// </remarks>
        private bool design = true;       

        /// <summary>
        /// general string formatter.
        /// </summary>
        private StringFormat generalFormatter;

        /// <summary>
        /// Header string formatter.
        /// </summary>
        private StringFormat headerFormatter;

        /// <summary>
        /// Array of dates
        /// </summary>        
        private DateTime[][] arrDates;

        /// <summary>
        /// Collection of null strings.
        /// </summary>
        /// <remarks>Saves the value of property NullStrings</remarks>
        private string[] nullStrings;

        /// <summary>
        /// Current null index.
        /// </summary>
        private int nullIndex;      

        /// <summary>
        /// Selected row in the calendar.
        /// </summary>
        private int selectedRow;

        /// <summary>
        /// Selected column in the calendar.
        /// </summary>
        private int selectedCol;

        /// <summary>
        /// Array of top level controls which get key focus in the calendar.
        /// </summary>
        private FocusObjects[] focusControls;

        /// <summary>
        /// Flag to indicate whether the current focus is at top controls or at the calendar.
        /// </summary>
        private bool focusAtTop;

        /// <summary>
        /// Index of selected top level control.
        /// </summary>
        private int selectedTopControl;

        /// <summary>
        /// Index of selected row.
        /// </summary>
        private int selectedBorderRow = -1;

        /// <summary>
        /// Index of selected col.
        /// </summary>
        private int selectedBorderCol = -1;

        /// <summary>
        /// Index of selected mouse over row.
        /// </summary>
        private int mouseOverRow = -1;

        /// <summary>
        /// Index of selected mouse over col.
        /// </summary>
        private int mouseOverCol = -1;

        /// <summary>
        /// Flag to indicate if the current focus is at bottom controls.
        /// </summary>
        private bool focusAtBottom;

        /// <summary>
        /// Flag to indicate the focus status at PrevMonthNav button.
        /// </summary>
        private bool firstEntry = true;

        /// <summary>
        /// /// Parent control for the calendar
        /// </summary>
        private Control parentControl;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public NhsCalendar()
        {
            // This call is required by the Windows.Forms Form Designer.
            this.InitializeComponent();

            this.FillResource();
            this.focusControls = new FocusObjects[6];
            for (int i = 0; i < this.focusControls.Length; i++)
            {
                this.focusControls[i] = new FocusObjects();
            }

            this.focusControls[0].Ctrl = this.prevMonthNav;
            this.focusControls[1].Ctrl = this.btnMonth;
            this.focusControls[1].FunctionalitySensitive = true;
            this.focusControls[2].Ctrl = this.nextMonthNav;
            this.focusControls[3].Ctrl = this.prevYearNav;
            this.focusControls[4].Ctrl = this.btnYear;
            this.focusControls[4].FunctionalitySensitive = true;
            this.focusControls[5].Ctrl = this.nextYearNav;

            this.BackColor = this.calendarBackColor;
            this.pnlMonth.BackColor = this.mainHeaderBackColor;
            this.pnlYear.BackColor = this.mainHeaderBackColor;
            this.today.FlatStyle = FlatStyle.System;
        }

        #endregion Constructor

        #region Custom events

        /// <summary>
        /// Event handler definition
        /// </summary>        
        public event EventHandler<DateChangedEventArgs> SelectedDateChanged;

        #endregion Custom events

        #region Public Properties
        /// <summary>
        /// Gets or sets a list of localized strings that identify different types of null index dates. 
        /// </summary>
        /// <remarks>
        /// Defaults to an empty list. 
        /// </remarks>
        [TypeConverter(typeof(StringArrayTypeConverter))]        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "NullStrings array only expected to be small")]
        public string[] NullStrings
        {
            get
            {
                return this.nullStrings;
            }

            set
            {
                if (this.NullStrings != value)
                {
                    this.nullStrings = value;
                }
            }
        }      

        /// <summary>
        /// Parent control for the calendar
        /// </summary>
        public Control ParentControl
        {
            get 
            { 
                return this.parentControl; 
            }

            set 
            { 
                this.parentControl = value; 
            }
        }

        /// <summary>
        /// The wrapper for Value.NullIndex. 
        /// </summary>               
        public int NullIndex
        {
            get
            {
                return this.nullIndex;
            }

            set
            {
                if (this.nullIndex != value)
                {
                    this.nullIndex = value;                  
                }
            }
        }

        /// <summary>
        /// The year of the active month
        /// </summary>        
        public int CurrentYear
        {
            get
            {
                if (this.currentYear == 0)
                {
                    return DateTime.Now.Year;
                }
                else
                {
                    return this.currentYear;
                }
            }

            set
            {
                if (this.currentYear != value)
                {
                    this.currentYear = value;
                }
            }
        }

        /// <summary>
        /// The number of the active month.
        /// </summary>        
        public int CurrentMonth
        {
            get
            {
                if (this.currentMonth == 0)
                {
                    return DateTime.Now.Month;
                }
                else
                {
                    return this.currentMonth;
                }
            }

            set
            {
                if (this.currentMonth != value)
                {
                    if (value < 1)
                    {
                        this.currentMonth = 12;
                        this.currentYear--;
                    }
                    else if (value > 12)
                    {
                        this.currentMonth = 1;
                        this.currentYear++;
                    }
                    else
                    {
                        this.currentMonth = value;
                    }
                }
            }
        }

        /// <summary>
        /// The day number of the selected date.
        /// </summary>        
        public int CurrentDay
        {
            get
            {
                if (this.currentDay == 0)
                {
                    return DateTime.Now.Day;
                }
                else
                {
                    return this.currentDay;
                }
            }

            set
            {
                if (this.currentDay != value)
                {
                    this.currentDay = value;
                }
            }
        }

        /// <summary>
        /// The selected date.
        /// </summary>
        public DateTime SelectedDate
        {
            get
            {
                return this.selectedDate;
            }

            set
            {
                // Not checked for already set because calendar should raise event
                // even if the selected date is same.
                this.SetSelectedDate(false, false, false, false, false, value);
            }
        }

        /// <summary>
        /// Color of the background for selected day.
        /// </summary>
        public Color SelectedDayColor
        {
            get
            {
                return this.selectedDayBackColor;
            }

            set
            {
                if (this.selectedDayBackColor != value)
                {
                    this.selectedDayBackColor = value;
                }
            }
        }

        /// <summary>
        /// Color of the text for selected day.
        /// </summary>
        public Color SelectedDayFontColor
        {
            get
            {
                return this.selectedDayFontColor;
            }

            set
            {
                if (this.selectedDayFontColor != value)
                {
                    this.selectedDayFontColor = value;
                }
            }
        }

        /// <summary>
        /// Color of the text for non-selected days.
        /// </summary>
        public Color NormalDayFontColor
        {
            get
            {
                return this.normalDayFontColor;
            }

            set
            {
                if (this.normalDayFontColor != value)
                {
                    this.normalDayFontColor = value;
                }
            }
        }

        /// <summary>
        /// Font for not bolded days.
        /// </summary>
        public Font NormalDayFont
        {
            get
            {
                return this.normalDayFont;
            }

            set
            {
                if (this.normalDayFont != value)
                {
                    this.normalDayFont = value;
                }
            }
        }

        /// <summary>
        /// Header text font.
        /// </summary>
        public Font MainHeaderFont
        {
            get
            {
                return this.mainHeaderFont;
            }

            set
            {
                if (this.mainHeaderFont != value)
                {
                    this.mainHeaderFont = value;
                }
            }
        }
        #endregion Properties

        #region Protected functions
        /// <summary>
        /// Raises event by invoking delegate
        /// </summary>
        /// <param name="eventArgs"> Date changed event argument</param>
        protected virtual void OnSelectedDateChanged(DateChangedEventArgs eventArgs)
        {
            if (this.SelectedDateChanged != null)
            {
                this.SelectedDateChanged(this, eventArgs);
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Processes the entered key.
        /// </summary>
        /// <param name="msg"> key down message</param>
        /// <param name="keyData"> key data </param>
        /// <returns> The status of processing </returns>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int changed;
            if (this.selectedBorderRow == -1)
            {
                this.selectedBorderRow = this.selectedRow;
                this.selectedBorderCol = this.selectedCol;
            }            
            
            if (msg.Msg == WmKeyDown && !this.focusAtBottom)
            {
                if ((int)msg.WParam == (int)Keys.Enter && !this.focusAtTop)
                {
                    this.SelectedDate = this.arrDates[this.selectedBorderCol][this.selectedBorderRow];
                }

                if ((int)msg.WParam == (int)Keys.Left)
                {
                    if (this.focusAtTop)
                    {
                        changed = this.selectedTopControl - 1 < 0 ? 5 : this.selectedTopControl - 1;
                        if (this.focusControls[changed].FunctionalitySensitive && this.GetFunctionality() == DateFunctionality.Simple)
                        {
                            changed = changed - 1 < 0 ? 5 : changed - 1;
                        }

                        this.focusControls[changed].Ctrl.Focus();
                        this.selectedTopControl = changed;
                    }
                    else
                    {
                        changed = this.selectedBorderCol - 1 < 0 ? 6 : this.selectedBorderCol - 1;
                        this.SetSelection(changed, this.selectedBorderRow);
                    }

                    return true;
                }
                else if ((int)msg.WParam == (int)Keys.Right)
                {
                    if (this.focusAtTop)
                    {
                        changed = (this.selectedTopControl + 1) % 6;
                        if (this.focusControls[changed].FunctionalitySensitive && this.GetFunctionality() == DateFunctionality.Simple)
                        {
                            changed = (changed + 1) % 6;
                        }

                        this.focusControls[changed].Ctrl.Focus();
                        this.selectedTopControl = changed;
                    }
                    else
                    {
                        changed = (this.selectedBorderCol + 1) % 7;
                        this.SetSelection(changed, this.selectedBorderRow);
                    }

                    return true;
                }
                else if ((int)msg.WParam == (int)Keys.Up)
                {
                    if (this.focusAtTop)
                    {
                        this.focusAtTop = false;
                        this.SetSelection(this.selectedTopControl, 5);
                        this.mainCalendar.Focus();
                    }
                    else
                    {
                        if (this.selectedBorderRow == 0)
                        {
                            changed = this.selectedBorderCol % 6;
                            if (this.focusControls[changed].FunctionalitySensitive && this.GetFunctionality() == DateFunctionality.Simple)
                            {
                                changed = (changed + 1) % 6;
                            }

                            this.focusAtTop = true;
                            this.focusControls[changed].Ctrl.Focus();
                            this.selectedTopControl = changed;
                            this.ClearSelection();
                        }
                        else
                        {
                            changed = this.selectedBorderRow - 1;
                            this.SetSelection(this.selectedBorderCol, changed);
                        }
                    }

                    return true;
                }
                else if ((int)msg.WParam == (int)Keys.Down)
                {
                    if (this.focusAtTop)
                    {
                        this.focusAtTop = false;
                        this.SetSelection(this.selectedTopControl, 0);
                        this.mainCalendar.Focus();
                    }
                    else
                    {
                        if (this.selectedBorderRow == 5)
                        {
                            changed = this.selectedBorderCol % 6;
                            if (this.focusControls[changed].FunctionalitySensitive && this.GetFunctionality() == DateFunctionality.Simple)
                            {
                                changed = (changed + 1) % 6;
                            }

                            this.focusAtTop = true;
                            this.focusControls[changed].Ctrl.Focus();
                            this.selectedTopControl = changed;
                            this.ClearSelection();
                        }
                        else
                        {
                            changed = this.selectedBorderRow + 1;
                            this.SetSelection(this.selectedBorderCol, changed);
                        }
                    }

                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region Private functions      

        /// <summary>
        /// Returns boundaries of covering rectangle around a date.
        /// </summary>
        /// <param name="parent">Parent rectangle</param>
        /// <returns>Dimensions of covering rectangle.</returns>
        private static Rectangle GetCoveringRect(Rectangle parent)
        {
            return new Rectangle(parent.X, parent.Y + (int)DayStringTopPad / 2, parent.Width - CoveringRectWidthPad, parent.Height - CoveringRectHeightPad);
        }

        /// <summary>
        /// Returns boundaries of day string in a rectangle.
        /// </summary>
        /// <param name="parent">Parent rectangle</param>
        /// <returns>Dimensions of string rectangle.</returns>
        private static Rectangle GetStringRect(Rectangle parent)
        {
            return new Rectangle(parent.X, parent.Y + DayStringTopPad, parent.Width, (int)(parent.Height * 0.5));
        }

        /// <summary>
        /// Sets specified control as focussed.
        /// </summary>
        /// <param name="focussed">Control</param>
        private static void SetFocus(object focussed)
        {
            Control ctrl = focussed as Control;
            if (ctrl != null)
            {
                ctrl.BringToFront();
                Button btn = ctrl as Button;
                if (btn != null)
                {
                    btn.FlatStyle = FlatStyle.Popup;
                }
            }
        }       

        /// <summary>
        /// Remove focus settings of a control.
        /// </summary>
        /// <param name="focussed">Control</param>
        private static void RemoveFocus(object focussed)
        {
            Control ctrl = focussed as Control;
            if (ctrl != null)
            {
                ctrl.SendToBack();
                Button btn = ctrl as Button;
                if (btn != null)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                }
            }
        }

        /// <summary>
        /// Returns functionality for the control. If parent is null then functionality is always assumed as complex.
        /// </summary>
        /// <returns>Functionality of parent control.</returns>
        private DateFunctionality GetFunctionality()
        {
            DateInputBox prnt = this.parentControl as DateInputBox;
            if (prnt != null)
            {
                return prnt.Functionality;
            }

            return DateFunctionality.Complex;
        }

        /// <summary>
        /// Updates the value of selected date
        /// </summary>
        /// <param name="monthChanged">Flag indicates whether a month is changed by the end user</param>
        /// <param name="yearChanged">Flag indicates whether an year is changed by the end user</param>
        /// <param name="monthSelected">Flag indicates whether a month is selected by the end user</param>
        /// <param name="yearSelected">Flag indicates whether an year is selected by the end user</param>
        /// <param name="nullStringSelected">Flag indicates whether a null string is selected by the end user</param>
        /// <param name="value">Value to set</param>
        private void SetSelectedDate(bool monthChanged, bool yearChanged, bool monthSelected, bool yearSelected, bool nullStringSelected, DateTime value)
        {
            this.selectedDate = value;
            this.CurrentMonth = this.selectedDate.Month;
            this.CurrentYear = this.selectedDate.Year;
            DateChangedEventArgs eventArgs = new DateChangedEventArgs(this.SelectedDate);
            if (monthChanged)
            {
                eventArgs.Changes = eventArgs.Changes | ChangedDateParts.MonthChanged;
            }

            if (yearChanged)
            {
                eventArgs.Changes = eventArgs.Changes | ChangedDateParts.YearChanged;
            }

            if (monthSelected)
            {
                eventArgs.Changes = eventArgs.Changes | ChangedDateParts.MonthSelected;
            }

            if (yearSelected)
            {
                eventArgs.Changes = eventArgs.Changes | ChangedDateParts.YearSelected;
            }

            if (nullStringSelected)
            {
                eventArgs.Changes = eventArgs.Changes | ChangedDateParts.NullStringSelected;
            }            
   
            this.OnSelectedDateChanged(eventArgs);
        }

        /// <summary>
        /// Fills the resources for the form.
        /// </summary>
        private void FillResource()
        {
            string exact = "day";
            string abbreviated = "abbday";

            for (int i = 1; i <= 7; i++)
            {
                this.days[i - 1] = DateInputBoxControl.Resources.ResourceManager.GetString(exact + i.ToString(CultureInfo.CurrentCulture));
                this.abbrDays[i - 1] = DateInputBoxControl.Resources.ResourceManager.GetString(abbreviated + i.ToString(CultureInfo.CurrentCulture));
            }

            exact = "month";
            abbreviated = "abbmon";

            for (int i = 1; i <= 12; i++)
            {
                this.months[i - 1] = DateInputBoxControl.Resources.ResourceManager.GetString(exact + i.ToString(CultureInfo.CurrentCulture));
                this.abbrMonths[i - 1] = DateInputBoxControl.Resources.ResourceManager.GetString(abbreviated + i.ToString(CultureInfo.CurrentCulture));
            }

            this.closeButton.Text = DateInputBoxControl.Resources.close;
        }

        /// <summary>
        /// Places the controls depending upon the size of control.
        /// </summary>
        private void PlaceControls()
        {
            // picture box settings.                                                
            this.mainCalendar.Width = this.Size.Width;
            this.pnlBottom.Top = this.Size.Height - this.pnlBottom.Height;
            this.mainCalendar.Top = TopGap;
            this.mainCalendar.Height = this.Size.Height - TopGap - this.pnlBottom.Height;
        }

        /// <summary>
        /// Creates graphics objects to be used.
        /// </summary>
        private void CreateGraphicObjects()
        {                                   
            // stringformats for displaying text           
            this.generalFormatter = new StringFormat();
            this.generalFormatter.Alignment = StringAlignment.Near;
            this.generalFormatter.LineAlignment = StringAlignment.Center;
            this.generalFormatter.Trimming = StringTrimming.EllipsisCharacter;

            // this is used for day header and day numbers            
            this.headerFormatter = new StringFormat();
            this.headerFormatter.Alignment = StringAlignment.Center;
            this.headerFormatter.LineAlignment = StringAlignment.Center;
        }

        /// <summary>
        /// Creates rectangle grid.
        /// </summary>
        /// <param name="width"> Width </param>
        /// <param name="height"> Height </param>
        /// <returns>Array of rectangle pointing to calendar grid.</returns>              
        private Rectangle[][] CreateGrid(int width, int height)
        {
            // Array of rectangles representing the calendar
            Rectangle[][] rectTemp = new Rectangle[7][];

            for (int i = 0; i < 7; i++)
            {
                rectTemp[i] = new Rectangle[6];
            }

            // header rectangles           
            this.rectDays = new Rectangle[7];
            int intXX = 0;
            int intXSize = (int)Math.Floor((double)width / 7);
            int intYSize = (int)Math.Floor((double)(height - DayHeaderWidth) / 6);

            int intYY = 0;
            intXX = 0;

            for (int i = 0; i < 7; i++)
            {
                Rectangle r1 = new Rectangle(intXX, intYY, intXSize, DayHeaderWidth);
                intXX += intXSize;
                this.rectDays[i] = r1;
            }

            intYY = DayHeaderWidth;
            for (int j = 0; j < 6; j++)
            {
                intXX = 0;
                for (int i = 0; i < 7; i++)
                {
                    Rectangle r1 = new Rectangle(intXX, intYY, intXSize, intYSize);
                    intXX += intXSize;
                    rectTemp[i][j] = r1;
                }

                intYY += intYSize;
            }

            return rectTemp;
        }

        /// <summary>
        /// Method called to fill dates.
        /// </summary>
        /// <param name="datCurrent"> Current Date </param>               
        private void FillDates(DateTime datCurrent)
        {
            // grid column
            int intDayofWeek = 0;

            // grid row
            int intWeek = 0;

            // total day counter
            int intTotalDays = -1;

            int previousMonth = datCurrent.Month;
            int previousYear = datCurrent.Year;

            if (previousMonth == 1)
            {
                previousMonth = 12;
                previousYear = previousYear == 1 ? 9999 : --previousYear;
            }
            else
            {
                --previousMonth;
            }

            int nextMonth = datCurrent.Month;
            int nextYear = datCurrent.Year;

            if (nextMonth == 12)
            {
                nextMonth = 1;
                nextYear = nextYear == 9999 ? 1 : ++nextYear;
            }
            else
            {
                ++nextMonth;
            }

            DateTime datPrevMonth = new DateTime(previousYear, previousMonth, 1, datCurrent.Hour, datCurrent.Minute, datCurrent.Second, datCurrent.Millisecond, datCurrent.Kind);
            DateTime datNextMonth = new DateTime(nextYear, nextMonth, 1, datCurrent.Hour, datCurrent.Minute, datCurrent.Second, datCurrent.Millisecond, datCurrent.Kind);

            // number of days in active month
            int intCurrDays = DateTime.DaysInMonth(datCurrent.Year, datCurrent.Month);

            // number of days in previous month
            int intPrevDays = DateTime.DaysInMonth(datPrevMonth.Year, datPrevMonth.Month);

            // number of days in next month
            int intNextDays = DateTime.DaysInMonth(datNextMonth.Year, datNextMonth.Month);
            DateTime[] datesCurr = new DateTime[intCurrDays];
            DateTime[] datesPrev = new DateTime[intPrevDays];
            DateTime[] datesNext = new DateTime[intNextDays];

            for (int i = 0; i < intCurrDays; i++)
            {
                datesCurr[i] = new DateTime(datCurrent.Year, datCurrent.Month, i + 1);
            }

            for (int i = 0; i < intPrevDays; i++)
            {
                datesPrev[i] = new DateTime(datPrevMonth.Year, datPrevMonth.Month, i + 1);
            }

            for (int i = 0; i < intNextDays; i++)
            {
                datesNext[i] = new DateTime(datNextMonth.Year, datNextMonth.Month, i + 1);
            }

            // array to hold dates corresponding to grid
            this.arrDates = new DateTime[7][];

            for (int i = 0; i < 7; i++)
            {
                this.arrDates[i] = new DateTime[6];
            }

            // where does the first day of the week land?
            intDayofWeek = Array.IndexOf(this.days, datesCurr[0].DayOfWeek.ToString());

            // if first day is monday then we'll include last month's all seven days.
            intDayofWeek = Array.IndexOf(this.days, datesCurr[0].DayOfWeek.ToString());
            if (intDayofWeek == 1)
            {
                intWeek++;
            }

            for (int intDay = 0; intDay < intCurrDays; intDay++)
            {
                // populate array of dates for active month.
                intDayofWeek = Array.IndexOf(this.days, datesCurr[intDay].DayOfWeek.ToString());

                // fill the array with the day numbers
                if (intDayofWeek > 0)
                {
                    this.arrDates[intDayofWeek - 1][intWeek] = datesCurr[intDay];                    
                }
                else
                {
                    this.arrDates[6][intWeek] = datesCurr[intDay];
                }

                if (intDayofWeek == DayOffset - 1)
                {
                    intWeek++;
                }

                // Back fill any days from the previous month                
                if (intDay == 0)
                {
                    int daysToShow = intDayofWeek == DayOffset ? 7 : intDayofWeek == DayOffset - 1 ? 6 : intDayofWeek - DayOffset;

                    // Days in previous month
                    int intDaysPrev = DateTime.DaysInMonth(datPrevMonth.Year, datPrevMonth.Month);                    

                    for (int i = daysToShow - 1; i >= 0; i--)
                    {
                        this.arrDates[i][0] = datesPrev[intDaysPrev - 1];
                        intDaysPrev--;
                        intTotalDays++;
                    }
                }

                intTotalDays++;
            }

            // fill in the remaining days of the grid with the beginning of the next month
            intTotalDays++;

            // Row for the active month
            int intRow = intTotalDays / 7;

            int intCol;

            int intDayNext = 0;

            for (int i = intRow; i < 6; i++)
            {
                intCol = intTotalDays - (intRow * 7);
                for (int j = intCol; j < 7; j++)
                {
                    this.arrDates[j][i] = datesNext[intDayNext];
                    intDayNext++;
                    intTotalDays++;
                }

                intRow++;
            }
        }

        /// <summary>
        /// updates the selected date in the control.
        /// </summary>        
        /// <param name="col"> Selected column </param>
        /// <param name="row"> Selected row </param>
        /// <remarks>
        /// This function is called when the user presses arrow keys on the control.
        /// </remarks>
        private void SetSelection(int col, int row)
        {
            Graphics g = this.mainCalendar.CreateGraphics();
            this.ClearSelection();
            this.DrawDottedBorder(col, row, g);
            this.selectedBorderRow = row;
            this.selectedBorderCol = col;
        }

        /// <summary>
        /// Creates a dotted border around specified date.
        /// </summary>
        /// <param name="col"> col of date</param>
        /// <param name="row"> row of date</param>
        /// <param name="g"> Graphics object</param>
        private void DrawDottedBorder(int col, int row, Graphics g)
        {
            Pen dotted = new Pen(this.normalDayFontColor);
            dotted.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawRectangle(dotted, GetCoveringRect(this.rects[col][row]));
        }

        /// <summary>
        /// Draws the specified date in the selected format.
        /// </summary>
        /// <param name="col"> col of date</param>
        /// <param name="row"> row of date</param>
        /// <param name="g"> Graphics object</param>
        private void DrawSelectedDate(int col, int row, Graphics g)
        {
            using (Brush brushSelectedDay = new SolidBrush(this.selectedDayBackColor), brushSelectedDayFont = new SolidBrush(this.selectedDayFontColor), brushNonselectedDayFont = new SolidBrush(this.normalDayFontColor))
            {
                Rectangle selectedRectangle = this.rects[col][row];                
                String str = this.arrDates[col][row].Day.ToString(CultureInfo.CurrentCulture);
                g.FillRectangle(brushSelectedDay, GetCoveringRect(selectedRectangle));
                g.DrawString(str, this.normalDayFont, brushSelectedDayFont, GetStringRect(selectedRectangle), this.headerFormatter); 
            }
        }

        /// <summary>
        /// clears the border display from the calendar.
        /// </summary>
        private void ClearSelection()
        {
            if (this.selectedBorderRow == -1 || this.arrDates == null)
            {
                return;
            }

            Rectangle selectedRectangle = this.rects[this.selectedBorderCol][this.selectedBorderRow];
            Pen clean = new Pen(this.normalDayBackColor);
            Graphics g = this.mainCalendar.CreateGraphics();

            // clear the displayed border.
            g.DrawRectangle(clean, GetCoveringRect(selectedRectangle));

            // Redraw mouse over effect. Selected date condition is put in else to give precedence to mouse over case.
            if (this.selectedBorderRow == this.mouseOverRow && this.selectedBorderCol == this.mouseOverCol)
            {
                this.DrawMouseOverEffect(this.mouseOverCol, this.mouseOverRow, g, false);
            }

            // Redraw background and the string if selected date is acted upon.
            else if (this.selectedBorderCol == this.selectedCol && this.selectedBorderRow == this.selectedRow)
            {
                this.DrawSelectedDate(this.selectedBorderCol, this.selectedBorderRow, g);
            }

            this.selectedBorderCol = -1;
            this.selectedBorderRow = -1;
        }

        /// <summary>
        /// Creates mouse over effect on specified date
        /// </summary>
        /// <param name="col"> Col of the specified date</param>
        /// <param name="row"> Row of the specified date</param>
        /// <param name="g"> Graphics object</param>
        /// <param name="createBorder">Tracks if dotted border should be created or not</param>
        private void DrawMouseOverEffect(int col, int row, Graphics g, bool createBorder)
        {
            if (col < 0 || row < 0)
            {
                return;
            }

            using (Brush brushActiveFont = new SolidBrush(this.activeDayFontColor), brushActiveBack = new SolidBrush(this.activeDayBackColor))
            {
                Rectangle selectedRectangle = this.rects[col][row];
                String str = this.arrDates[col][row].Day.ToString(CultureInfo.CurrentCulture);
                g.FillRectangle(brushActiveBack, GetCoveringRect(selectedRectangle));
                g.DrawString(str, this.normalDayFont, brushActiveFont, GetStringRect(selectedRectangle), this.headerFormatter); 
            }

            if (this.selectedBorderCol == col && this.selectedBorderRow == row && createBorder)
            {
                this.DrawDottedBorder(col, row, g);
            }

            this.mouseOverCol = col;
            this.mouseOverRow = row;
        }

        /// <summary>
        /// Clears mouse over effect from current selection.
        /// </summary>
        private void ClearMouseOverEffect()
        {
            using (Brush brushNonselectedDayFont = new SolidBrush(this.normalDayFontColor), brushWeekendDayFont = new SolidBrush(this.weekendDayFontColor), brushGeneral = new SolidBrush(this.normalDayBackColor))
            {
                if (this.mouseOverCol == -1 || this.mouseOverRow == -1)
                {
                    return;
                }

                Rectangle selectedRectangle = this.rects[this.mouseOverCol][this.mouseOverRow];
                String str = this.arrDates[mouseOverCol][mouseOverRow].Day.ToString(CultureInfo.CurrentCulture);
                Graphics g = this.mainCalendar.CreateGraphics();

                // clear the mouse over effect              
                g.FillRectangle(brushGeneral, GetCoveringRect(selectedRectangle));
                if (this.arrDates[mouseOverCol][mouseOverRow].DayOfWeek != DayOfWeek.Saturday && this.arrDates[mouseOverCol][mouseOverRow].DayOfWeek != DayOfWeek.Sunday)
                {
                    g.DrawString(str, this.normalDayFont, brushNonselectedDayFont, GetStringRect(selectedRectangle), this.headerFormatter);                                            
                }
                else
                {
                    g.DrawString(str, this.weekendDayFont, brushWeekendDayFont, GetStringRect(selectedRectangle), this.headerFormatter);                    
                }                            

                // current mouse selected row is same as the border row.
                if (this.mouseOverRow == this.selectedBorderRow && this.mouseOverCol == this.selectedBorderCol)
                {
                    this.DrawDottedBorder(this.mouseOverCol, this.mouseOverRow, g);
                }

                this.mouseOverCol = -1;
                this.mouseOverRow = -1; 
            }
        }

        /// <summary>
        /// Shifts focus to the selected date of calendar.
        /// </summary>
        private void FocusCalendar()
        {
            this.focusAtBottom = false;
            this.focusAtTop = false;
            this.mainCalendar.Focus();
            this.selectedBorderRow = -1;
            this.selectedBorderCol = -1;
        }
        #endregion Private functions

        #region Event Handlers

        /// <summary>
        /// Handles the closed event of form
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void NhsCalendar_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            if (this.parentControl != null)
            {
                Form parentForm = this.parentControl.TopLevelControl as Form;
                if (parentForm != null)
                {
                    parentForm.Close();
                }
            }
        }

        /// <summary>
        /// Handles the size changed event of form.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void Nhscalendar_SizeChanged(object sender, System.EventArgs e)
        {
            this.PlaceControls();
            this.rects = this.CreateGrid(this.mainCalendar.Width, this.mainCalendar.Height);
            this.mainCalendar.Invalidate();
        }

        /// <summary>
        /// Handles the mouse down event on main calendar.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void Calendar_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.currentXAxis = e.X;
            this.currentYAxis = e.Y;
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (this.rects[i][j].Contains(this.currentXAxis, this.currentYAxis))
                    {
                        // update
                        this.SelectedDate = this.arrDates[i][j];

                        // update
                        this.CurrentMonth = this.arrDates[i][j].Month;
                    }
                }
            }

            this.mainCalendar.Invalidate();
        }

        /// <summary>
        /// Updates year and month buttons' font styling, based upon functionality setting.
        /// </summary>
        private void UpdateButtonStyles()
        {
            if (this.GetFunctionality() == DateFunctionality.Simple)
            {
                this.btnMonth.Font = new Font(this.btnMonth.Font.FontFamily, this.btnMonth.Font.Size, FontStyle.Regular);
                this.btnMonth.Cursor = Cursors.Default;
                this.toolTip1.SetToolTip(this.btnMonth, string.Empty);
                this.toolTip1.SetToolTip(this.btnYear, string.Empty);
            }
            else
            {
                this.btnMonth.Font = new Font(this.btnMonth.Font.FontFamily, this.btnMonth.Font.Size, FontStyle.Underline);
                this.btnMonth.Cursor = Cursors.Hand;
                this.toolTip1.SetToolTip(this.btnMonth, DateInputBoxControl.Resources.monthTip);
                this.toolTip1.SetToolTip(this.btnYear, DateInputBoxControl.Resources.yearTip);
            }

            this.btnYear.Font = this.btnMonth.Font;
            this.btnYear.Cursor = this.btnMonth.Cursor;
        }

        /// <summary>
        /// Handles the paint event of main calendar
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void Calendar_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            bool active = false;
            bool selected = false;
            bool weekend = false;
            this.UpdateButtonStyles();
            if (!this.design)
            {               
                using (Brush brushSelectedDay = new SolidBrush(this.selectedDayBackColor), brushSelectedDayFont = new SolidBrush(this.selectedDayFontColor), brushNonselectedDayFont = new SolidBrush(this.normalDayFontColor), brushBackColor = new SolidBrush(this.normalDayBackColor), brushDayHeaderForeColor = new SolidBrush(this.dayHeaderFontColor), brushInactiveDay = new SolidBrush(this.inactiveDayFontColor), brushDayHeaderBackColor = new SolidBrush(this.dayHeaderBackColor), brushWeekendDayFont = new SolidBrush(this.weekendDayFontColor))
                {
                    // the data to be displayed in the calendar is stored in a 7 x 6 array of arrays                
                    this.FillDates(this.SelectedDate);                    
                    this.btnMonth.Text = this.months[this.CurrentMonth - 1];
                    this.btnYear.Text = this.CurrentYear.ToString(CultureInfo.CurrentCulture);

                    Rectangle rect = e.ClipRectangle;
                    rect.Y += this.rectDays[0].Height;
                    rect.Height -= 20;

                    // graphics object for paint event
                    Graphics g = e.Graphics;                  
                    
                    // back color of days header.
                    g.FillRectangle(brushDayHeaderBackColor, 0, 0, this.Width, this.rectDays[0].Height);

                    // border of days header
                    g.DrawRectangle(new Pen(this.normalDayFontColor), 0, 2, this.Width, this.rectDays[0].Height - 2);

                    // back color of main calendar.
                    g.FillRectangle(brushBackColor, rect);

                    string str;

                    // day of week header
                    for (int i = 0; i < 7; i++)
                    {
                        // draw weekday header                    
                        g.DrawString(this.abbrDays[i], this.dayHeaderFont, brushDayHeaderForeColor, this.rectDays[i], this.headerFormatter);
                    }

                    // actual calendar 
                    for (int j = 0; j < 6; j++)
                    {
                        for (int i = 0; i < 7; i++)
                        {                            
                            // rects for text                                
                            Rectangle rectTopHalf = GetStringRect(this.rects[i][j]);
                            DateTime dateTest = this.arrDates[i][j];

                            // should box be filled as selected date
                            if (dateTest.Date == this.SelectedDate.Date)
                            {
                                selected = true;
                                this.selectedCol = i;
                                this.selectedRow = j;
                            }
                            else
                            {
                                selected = false;
                            }

                            str = this.arrDates[i][j].Day.ToString(CultureInfo.CurrentCulture);
                            
                            // check to see if day is an active one.
                            if (this.arrDates[i][j].Month == this.currentMonth)
                            {
                                if (this.arrDates[i][j].DayOfWeek == DayOfWeek.Saturday || this.arrDates[i][j].DayOfWeek == DayOfWeek.Sunday)
                                {
                                    weekend = true;
                                    active = false;
                                }
                                else
                                {
                                    active = true;
                                }
                            }

                            // not the active month
                            else
                            {
                                active = false;
                                weekend = false;
                            }

                            // draw rectangle and text
                            if (selected)
                            {
                                g.FillRectangle(brushSelectedDay, GetCoveringRect(this.rects[i][j]));
                                g.DrawString(str, this.normalDayFont, brushSelectedDayFont, rectTopHalf, this.headerFormatter);
                                this.DrawMouseOverEffect(this.mouseOverCol, this.mouseOverRow, g, false);
                                if (!this.focusAtBottom && !this.focusAtTop && this.selectedBorderRow == -1 && this.selectedBorderCol == -1)
                                {
                                    this.mainCalendar.Focus();
                                    this.selectedBorderCol = i;
                                    this.selectedBorderRow = j;
                                    Pen dotted = new Pen(this.normalDayFontColor);
                                    dotted.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                                    g.DrawRectangle(dotted, GetCoveringRect(this.rects[i][j]));
                                }
                            }

                            // active day (belongs to current month)
                            else if (active)
                            {
                                g.DrawString(str, this.normalDayFont, brushNonselectedDayFont, rectTopHalf, this.headerFormatter);
                            }

                            // weekend (belongs to current month)
                            else if (weekend)
                            {
                                g.DrawString(str, this.weekendDayFont, brushWeekendDayFont, rectTopHalf, this.headerFormatter);
                            }

                            // inactive day (previous or next month)
                            else
                            {
                                g.DrawString(str, this.inactiveDayFont, brushInactiveDay, rectTopHalf, this.headerFormatter);
                            }
                        }
                    }
                } 
            }
        }

        /// <summary>
        /// Handles the load event of calendar
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void Nhscalendar_Load(object sender, System.EventArgs e)
        {
            this.PlaceControls();
            this.rects = this.CreateGrid(this.mainCalendar.Width, this.mainCalendar.Height);
            this.CreateGraphicObjects();
            this.design = false;
            this.mainCalendar.Focus();
        }

        /// <summary>
        /// Handles the click event of previous year button.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        /// <remarks>
        /// Decrements the selected year by one.
        /// </remarks>
        private void BtnPrevYear_Click(object sender, EventArgs e)
        {
            // special case - rolling back from Jan 0001
            if (this.SelectedDate.Year == 1)
            {
                this.CurrentYear = 9999;
                this.SetSelectedDate(false, true, false, false, false, this.SelectedDate.AddYears(9998));
            }
            else
            {
                this.CurrentYear--;
                this.SetSelectedDate(false, true, false, false, false, this.SelectedDate.AddYears(-1));
            }

            this.mainCalendar.Invalidate();
        }

        /// <summary>
        /// Handles the click event of next year button.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        /// <remarks>
        /// Increments the selected year by one.
        /// </remarks>
        private void BtnNextYear_Click(object sender, EventArgs e)
        {
            // special case - rolling forward from Dec 9999
            if (this.SelectedDate.Year == 9999)
            {
                this.CurrentYear = 1;
                this.SetSelectedDate(false, true, false, false, false, this.SelectedDate.AddYears(-9998));
            }
            else
            {
                this.CurrentYear++;
                this.SetSelectedDate(false, true, false, false, false, this.SelectedDate.AddYears(1));
            }

            this.mainCalendar.Invalidate();
        }

        /// <summary>
        /// Handles the size changed event of the month label.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void LblMonth_SizeChanged(object sender, EventArgs e)
        {
            int start = this.prevMonthNav.Right;
            int end = this.nextMonthNav.Left;
            int offset = (end - start) - this.btnMonth.Width;
            offset = offset > 0 ? (int)offset / 2 : 0;
            this.btnMonth.Left = start + offset;            
        }

        /// <summary>
        /// Handles the size changed event of year label.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void LblYear_SizeChanged(object sender, EventArgs e)
        {
            this.btnYear.Left = this.nextYearNav.Left - TopHorizontolGap - this.btnYear.Width;
            this.prevYearNav.Left = this.btnYear.Left - TopHorizontolGap - this.prevYearNav.Width;
        }

        /// <summary>
        /// Handles the click event of the previous month click.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void BtnPrev_Click(object sender, System.EventArgs e)
        {
            // special case - rolling back from Jan 0001
            if (this.SelectedDate.Month == 1 && this.SelectedDate.Year == 1)
            {
                this.CurrentMonth = 12;
                this.CurrentYear = 9999;

                // update
                this.SetSelectedDate(false, true, false, false, false, this.SelectedDate.AddYears(9998));
                this.SetSelectedDate(true, false, false, false, false, this.SelectedDate.AddMonths(11));
            }
            else
            {
                if (this.CurrentMonth == 1)
                {
                    this.CurrentMonth = 12;
                    this.CurrentYear = this.CurrentYear - 1;
                }
                else
                {
                    this.CurrentMonth--;
                }

                // update
                this.SetSelectedDate(true, false, false, false, false, this.SelectedDate.AddMonths(-1));
            }

            this.mainCalendar.Invalidate();
        }

        /// <summary>
        /// Handles the click event of tody button
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void Today_Click(object sender, EventArgs e)
        {
            this.SelectedDate = DateTime.Now;
        }

        /// <summary>
        /// Handles the click event of the next month click.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void BtnNext_Click(object sender, System.EventArgs e)
        {
            // special case - rolling forward from Dec 9999
            if (this.SelectedDate.Month == 12 && this.SelectedDate.Year == 9999)
            {
                this.CurrentMonth = 1;
                this.CurrentYear = 1;

                // update
                this.SetSelectedDate(false, true, false, false, false, this.SelectedDate.AddYears(-9998));
                this.SetSelectedDate(true, false, false, false, false, this.SelectedDate.AddMonths(-11));
            }
            else
            {
                if (this.CurrentMonth == 12)
                {
                    this.CurrentMonth = 1;
                    this.CurrentYear++;
                }
                else
                {
                    this.CurrentMonth++;
                }

                // update
                this.SetSelectedDate(true, false, false, false, false, this.SelectedDate.AddMonths(1));
            }

            this.mainCalendar.Invalidate();
        }      

        /// <summary>
        /// Handles the visibility changed event of control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        /// <remarks>
        /// hides/ displays the null list combo box.
        /// </remarks>
        private void NhsCalendar_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {                
                this.FocusCalendar();
            }
        }       

        /// <summary>
        /// Handles the click event of month label.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        /// <remarks>
        /// Updates the selected date with date type as year month.
        /// </remarks>
        private void LblMonth_Click(object sender, EventArgs e)
        {
            this.SetSelectedDate(false, false, true, false, false, this.SelectedDate);
        }

        /// <summary>
        /// Handles the click event of year label.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        /// <remarks>
        /// Updates the selected date with date type as year.
        /// </remarks>
        private void LblYear_Click(object sender, EventArgs e)
        {
            this.SetSelectedDate(false, false, false, true, false, this.SelectedDate);
        }

        /// <summary>
        ///  Handles the enter event of PrevMonthNav.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void PrevMonthNav_Enter(object sender, EventArgs e)
        {            
            if (!this.focusAtBottom && !this.focusAtTop)
            {
                this.ClearSelection();
            }

            if (this.firstEntry)
            {
                this.FocusCalendar();
                this.firstEntry = false;
            }
            else
            {
                this.focusAtTop = true;
                this.focusAtBottom = false;
                this.selectedTopControl = 0;
                SetFocus(sender);
            }
        }

        /// <summary>
        /// Handles the enter event of today.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void Today_Enter(object sender, EventArgs e)
        {
            if (!this.focusAtBottom && !this.focusAtTop)
            {
                this.ClearSelection();
            }

            this.focusAtTop = false;
            this.focusAtBottom = true;
        }

        /// <summary>
        ///  Handles the enter event of nullList.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void NullList_Enter(object sender, EventArgs e)
        {
            if (!this.focusAtBottom && !this.focusAtTop)
            {
                this.ClearSelection();
            }

            this.focusAtTop = false;
            this.focusAtBottom = true;
        }

        /// <summary>
        ///  Handles the enter event of nextYearNav.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void NextYearNav_Enter(object sender, EventArgs e)
        {
            SetFocus(sender);
            if (!this.focusAtBottom && !this.focusAtTop)
            {
                this.ClearSelection();
            }

            this.focusAtTop = true;
            this.focusAtBottom = false;
            this.selectedTopControl = 5;
        }

        /// <summary>
        /// Handles the keypress event of calendar.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void NhsCalendar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Escape)
            {
                this.Hide();
            }
        }

        /// <summary>
        /// Handles the enter event of next month button
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void NextMonthNav_Enter(object sender, EventArgs e)
        {
            SetFocus(sender);
            this.focusAtTop = true;
            this.focusAtBottom = false;
            this.selectedTopControl = 2;
        }

        /// <summary>
        /// Handles the enter event of prev month button
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void PrevYearNav_Enter(object sender, EventArgs e)
        {
            SetFocus(sender);
            this.focusAtTop = true;
            this.focusAtBottom = false;
            this.selectedTopControl = 3;
        }

        /// <summary>
        /// Handles the enter event of year button
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void BtnYear_Enter(object sender, EventArgs e)
        {
            this.focusAtTop = true;
            this.focusAtBottom = false;
            this.selectedTopControl = 4;
            if (this.GetFunctionality() == DateFunctionality.Complex)
            {
                SetFocus(sender);
            }
        }

        /// <summary>
        /// Handles the enter event of month button
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void BtnMonth_Enter(object sender, EventArgs e)
        {
            this.focusAtTop = true;
            this.focusAtBottom = false;
            this.selectedTopControl = 1;
            if (this.GetFunctionality() == DateFunctionality.Complex)
            {
                SetFocus(sender);                
            }
        }

        /// <summary>
        /// Handles the leave event of month button
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void BtnMonth_Leave(object sender, EventArgs e)
        {
            RemoveFocus(sender);
        }

        /// <summary>
        /// Handles the leave event of year button
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void BtnYear_Leave(object sender, EventArgs e)
        {
            RemoveFocus(sender);
        }

        /// <summary>
        /// Handles the mouse move event of calendar
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void MainCalendar_MouseMove(object sender, MouseEventArgs e)
        {
            this.ClearMouseOverEffect();
            this.currentXAxis = e.X;
            this.currentYAxis = e.Y;
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (this.rects[i][j].Contains(this.currentXAxis, this.currentYAxis))
                    {
                        if (this.arrDates[i][j].Month == this.CurrentMonth)
                        {
                            this.DrawMouseOverEffect(i, j, this.mainCalendar.CreateGraphics(), true);                            
                        }

                        this.mainCalendar.Cursor = System.Windows.Forms.Cursors.Hand;
                        return;
                    }
                }
            }

            this.mainCalendar.Cursor = System.Windows.Forms.Cursors.Default;
        }

        /// <summary>
        /// Handles the mouse leave event of calendar.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void MainCalendar_MouseLeave(object sender, EventArgs e)
        {
            this.ClearMouseOverEffect();
        }

        /// <summary>
        /// Handles click event of close link
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Handles leave event of previous month navigation button.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void PrevMonthNav_Leave(object sender, EventArgs e)
        {
            RemoveFocus(sender);
        }

        /// <summary>
        /// Handles leave event of next month navigation button.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void NextMonthNav_Leave(object sender, EventArgs e)
        {
            RemoveFocus(sender);
        }

        /// <summary>
        /// Handles leave event of previous year navigation button.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>>
        private void PrevYearNav_Leave(object sender, EventArgs e)
        {
            RemoveFocus(sender);
        }

        /// <summary>
        /// Handles leave event of next year navigation button.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void NextYearNav_Leave(object sender, EventArgs e)
        {
            RemoveFocus(sender);
        }

        #endregion Event Handlers             

        #region Private Classes

        /// <summary>
        /// Class to define focussed header objects of calendar
        /// </summary>
        private class FocusObjects
        {
            #region Member Vars
            /// <summary>
            /// Control.
            /// </summary>
            private Control ctrl;           

            /// <summary>
            /// Flag to specify element is sensitive to functionality changes.
            /// </summary>
            private bool functionalitySensitive;
            #endregion

            #region Properties
            /// <summary>
            /// Control.
            /// </summary>
            public Control Ctrl
            {
                get { return this.ctrl; }
                set { this.ctrl = value; }
            }          

            /// <summary>
            /// Flag to specify element is sensitive to functionality changes.
            /// </summary>
            public bool FunctionalitySensitive
            {
                get { return this.functionalitySensitive; }
                set { this.functionalitySensitive = value; }
            }
            #endregion
        }

        #endregion        
    }    
}