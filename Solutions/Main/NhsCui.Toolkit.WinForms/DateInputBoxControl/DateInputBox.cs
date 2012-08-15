//-----------------------------------------------------------------------
// <copyright file="DateInputBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to enter a date.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.WinForms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Globalization;
    using System.Windows.Forms;
    using NhsCui.Toolkit.DateAndTime;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The control used to get the date input from the user.
    /// </summary>
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "DateInputBox.bmp")]
    [DefaultProperty("Value")]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    public partial class DateInputBox : UserControl, INotifyPropertyChanged
    {
        #region Const Values
        /// <summary>
        /// Padding width for distance between chkapprox and control's right.
        /// </summary>
        private const int PadWidth = 5;

        /// <summary>
        /// Padding used to adjust the height of button and the text box.
        /// </summary>
        private const int PadSmall = 2;

        /// <summary>
        /// Additional height for tool tip form.
        /// </summary>
        private const int PadHeightToolTip = 52;
       
        /// <summary>
        /// Default height of picturebox.
        /// </summary>
        private const int DefaultPictureHeight = 18;

        /// <summary>
        /// Default width of picturebox.
        /// </summary>
        private const int DefaultPictureWidth = 18;

        /// <summary>
        /// Min width of control
        /// </summary>
        private const int MinWidth = 130;

        /// <summary>
        /// Min height of control
        /// </summary>
        private const int MinHeight = 22;

        /// <summary>
        /// Padding for control's border
        /// </summary>
        private const int BorderPadding = 4;
        #endregion

        #region Member Vars

        /// <summary>
        /// Flag to specify that no validation is required for the entered key.
        /// </summary>
        /// <remarks>
        /// TextChanged event of the txtInput validates the text entered. If this flag is set then
        /// no validation will be done and this flag will be unset again.
        /// </remarks>
        private bool acceptedKey;

        /// <summary>
        /// Keys directly allowed in the txtInput. No validation is required for these keys.
        /// </summary>
        private int[] allowedSpecialKeys = new int[] { 3, 22, 24 };

        /// <summary>
        /// Toggle value to specify text is pasted in txtInput with selection or not.
        /// </summary>
        /// <remarks>If text is selected and then pasted, then the text_change event is fired twice
        /// Once with removing the selected text and then with adding the new text. 
        /// </remarks>
        private bool selectedPaste;

        /// <summary>
        /// Toogle value to specify whether text changed occurred because of keypress or not.
        /// </summary>
        /// <remarks>If true it can be inferred that the text is changed because of key press and not cut/paste</remarks>
        private bool inlineEditing;

        /// <summary>
        /// Toggle value to specify current text is invalid w.r.t. to current editing date type.
        /// </summary>
        private bool invalidFormat;
       
        /// <summary>
        /// Tooltip to be displayed.
        /// </summary>
        /// <remarks>
        /// Saves the value of the property ToolTipText
        /// </remarks>
        private String tooltipText = String.Empty;     

        /// <summary>
        /// Collection of null strings.
        /// </summary>
        /// <remarks>Saves the value of property NullStrings</remarks>
        private string[] nullStrings;

        /// <summary>
        /// Calendar to be displayed
        /// </summary>
        private NhsCalendar calendar;     

        /// <summary>
        /// Toggle value to allow/forbid approximate input in the control.
        /// </summary>
        /// <remarks>Saves the value of property AllowApproximate</remarks>
        private bool approximate;

        /// <summary>
        /// Functionality of the control. Defaults to DateFunctionality.Simple.
        /// </summary>
        /// <remarks>Saves the value of property Functionality</remarks>
        private DateFunctionality functionality = DateFunctionality.Simple;

        /// <summary>
        /// The Date Value of the control.
        /// </summary>
        /// <remarks>Saves the Value of property DateValue</remarks>
        private NhsDate dateValue;

        /// <summary>
        /// Saves the date type when control is being edited.
        /// </summary>
        /// <remarks>
        /// Saves the value of property editingDateType
        /// </remarks>
        private DateType editingDateType = DateType.Exact;       

        /// <summary>
        /// Water mark text to be displayed, when the control's value is null and control doesn't have focus.
        /// </summary>
        /// <remarks>
        /// Saves the value of property WatermarkText.
        /// </remarks>
        private String watermarkText;

        /// <summary>
        /// Tracks whether day of week is displayed or not.
        /// </summary>
        /// <remarks>
        /// Saves the value of property DisplayDayOfWeek.
        /// </remarks>
        private bool displayDayOfWeek;

        /// <summary>
        /// Tracks whether Today, Tomorrow, Yesterday is substituted for actual dates or not.
        /// </summary>
        /// <remarks>
        /// Saves the value of property DisplayDateAsText.
        /// </remarks>
        private bool displayDateAsText;

        /// <summary>
        /// Saves the start index of different fields. Order of values is directly mapped to the enum Field.
        /// </summary>
        private int[] fieldStart = new int[] { 0, 4, 7, 11 };

        /// <summary>
        /// Saves the end index of different fields. Order of values is directly mapped to the enum Field.
        /// </summary>
        /// <remarks>Seconds field has two spaces after it, so there are two entries for seconds field [8, 9]
        /// </remarks>
        private int[] fieldEnd = new int[] { 3, 6, 10, 15 };

        /// <summary>
        /// Saves the Current Field of the control. 
        /// </summary>
        private DateField currentField = DateField.Day;

        /// <summary>
        /// Saves the Current input mode of the control.
        /// </summary>
        private InputMode currentMode = InputMode.Simple;

        /// <summary>
        /// Saves the Current Value of the control.
        /// </summary>
        /// <remarks>
        /// If control is assigned an invalid value while editing, then this value is used to restore the recent value
        /// in the control.
        /// </remarks>
        private NhsDate currentValue;    

        /// <summary>
        /// Status of the control's value.
        /// </summary>
        private bool valid = true;

        /// <summary>
        /// Validation manager of the control.
        /// </summary>
        private IValidationManager validationManager;

        /// <summary>
        /// Behavior of the control for invalid text.
        /// </summary>
        private bool cancelOnError = true;

        /// <summary>
        /// invalid text of the control.
        /// </summary>
        private String invalidText;

        /// <summary>
        /// Indicates if the control value is validated or not.
        /// </summary>
        /// <remarks>
        /// This flag is used to make sure that DateInputBox_Leave executes only when
        /// TxtInput_Leave is not executed.
        /// </remarks>
        private bool validated;

        /// <summary>
        /// Tracks if the end user has edited the date or not.
        /// </summary>
        /// <remarks>
        /// Used to control the scrolling of fields in exact/approx date. If date is being edited then
        /// scrolling is not permitted untill user commits the value
        /// </remarks>
        private bool dateEdited;

        /// <summary>
        /// Tracks the control is in watermark state or not.
        /// </summary>
        private bool watermarkMode;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a DateInputBox object.
        /// </summary>
        public DateInputBox()
        {
            this.InitializeComponent();
            this.AccessibleName = DateInputBoxControl.Resources.AccessibleName;
            this.AccessibleDescription = DateInputBoxControl.Resources.AccessibleDescription;
            this.AccessibleRole = AccessibleRole.Text;
            this.LoadResources();
            this.ResetFields();
            this.txtInput.MouseWheel += new MouseEventHandler(this.TxtInput_MouseWheel);
            this.SizeControls();
            this.RefreshDisplayText();
            base.AutoValidate = AutoValidate.EnablePreventFocusChange;
            this.SetToolTip();
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
        /// Gets the text associated with the control.
        /// </summary>
        public new String Text
        {
            get
            {
                if (this.IsValid)
                {
                    return this.GetDisplayString();
                }
                else
                {
                    return this.invalidText;
                }
            }
        }

        /// <summary>
        /// The wrapper for Value.DateType. 
        /// </summary>
        [Category("Behavior")]
        [Description("Wrapper for Value.DateType")]
        [RefreshProperties(RefreshProperties.All)]
        public DateType DateType
        {
            get
            {
                return this.Value.DateType;
            }

            set
            {
                if (this.Value.DateType != value)
                {
                    this.Value.DateType = value;
                    this.editingDateType = value;
                    this.NotifyPropertyChanged("Value");
                }

                this.RefreshDisplayText();
                this.HandleApproxState();
            }
        }

        /// <summary>
        /// Specifies whether the text "Today", "Tomorrow" or "Yesterday" should be displayed in place of the date. 
        /// </summary>
        /// <remarks>
        /// Defaults to false.
        /// </remarks>
        [Category("Behavior")]
        [Description("Specifies whether to substitute Today, Tomorrow or Yesterday for actual dates (defaults to false)")]
        [DefaultValue(false)]
        public bool DisplayDateAsText
        {
            get
            {
                return this.displayDateAsText;
            }

            set
            {
                if (this.displayDateAsText != value)
                {
                    this.displayDateAsText = value;
                    this.RefreshDisplayText();
                }
            }
        }

        /// <summary>
        /// Gets the text displayed in the ToolTip.
        /// </summary>
        /// <remarks>
        /// Decorated with the LocalizableAttribute and set to true. Defaults to �Enter a date 
        /// (e.g. '1624' or '16:24') or type a date period (e.g. 'afternoon', 'evening'). 
        /// Alternatively use the arrow keys or spinner to change the hours and minutes."
        /// </remarks>
        [Category("Behavior")]
        [Description("The text that is displayed in the tooltip.")]
        [ResourceDefaultValue(typeof(DateInputBoxControl.Resources), "FirstUseToolTipSimple")]
        public string TooltipText
        {
            get
            {
                if (this.tooltipText.Trim().Length > 0)
                {
                    return this.tooltipText;
                }

                return DateInputBoxControl.Resources.FirstUseToolTipSimple;
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
        /// The wrapper for Value.DateValue. 
        /// </summary>
        [Category("Behavior")]
        [Description("Wrapper for Value.DateValue")]
        [RefreshProperties(RefreshProperties.All)]
        public DateTime DateValue
        {
            get
            {
                return this.Value.DateValue;
            }

            set
            {
                if (this.Value.DateValue != value)
                {
                    this.Value.DateValue = value;
                    this.NotifyPropertyChanged("Value");
                }

                this.RefreshDisplayText();
            }
        }

        /// <summary>
        /// The wrapper for Value.Month. 
        /// </summary>
        [Category("Behavior")]
        [Description("Wrapper for Value.Month")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(1)]
        public int Month
        {
            get
            {
                return this.Value.Month;
            }

            set
            {
                if (this.Value.Month != value)
                {
                    this.Value.Month = value;
                    this.NotifyPropertyChanged("Value");
                }

                this.RefreshDisplayText();
            }
        }

        /// <summary>
        /// The wrapper for Value.NullIndex. 
        /// </summary>       
        [Category("Behavior")]
        [Description("Wrapper for Value.NullIndex")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(-1)]
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
                    this.NotifyPropertyChanged("Value");
                }

                this.RefreshDisplayText();
            }
        }       

        /// <summary>
        /// The wrapper for Value.Year. 
        /// </summary>
        [Category("Behavior")]
        [Description("Wrapper for Value.Year")]
        [RefreshProperties(RefreshProperties.All)]
        [DefaultValue(0)]
        public int Year
        {
            get
            {
                return this.Value.Year;
            }

            set
            {
                if (this.Value.Year != value)
                {
                    this.Value.Year = value;
                    this.NotifyPropertyChanged("Value");
                }

                this.RefreshDisplayText();
            }
        }

        /// <summary>
        /// Gets or sets the watermark text. 
        /// </summary>
        /// <remarks>
        /// Defaults to dd-MMM-yyyy. This property is localizable and is relevant only when DateType is Null and the control does not have focus. 
        /// In these conditions, the watermark text is displayed. This is visual behaviour only; 
        /// the value of the control is unaffected.
        /// </remarks>
        [Category("Behavior"), Localizable(true)]
        [Description("Watermark text to appear when the control has no value")]
        [ResourceDefaultValue(typeof(DateInputBoxControl.Resources), "DefaultWaterMark")]
        public string WatermarkText
        {
            get
            {
                if (String.IsNullOrEmpty(this.watermarkText))
                {
                    return DateInputBoxControl.Resources.DefaultWaterMark;
                }

                return this.watermarkText;
            }

            set
            {
                if (this.watermarkText != value)
                {
                    this.watermarkText = value;
                    this.RefreshDisplayText();
                }
            }
        }

        /// <summary>
        /// Gets or sets a list of localized strings that identify different types of null index dates. 
        /// </summary>
        /// <remarks>
        /// Defaults to an empty list. 
        /// </remarks>
        [Category("Behavior"), DefaultValue(null), TypeConverter(typeof(StringArrayTypeConverter))]
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
        /// Specifies whether the name of the day is displayed with the date.
        /// </summary>
        /// <remarks>
        /// Defaults to false. 
        /// </remarks>
        [Category("Behavior")]
        [Description("Specifies whether to display the day of week (defaults to false)")]
        [DefaultValue(false)]
        public bool DisplayDayOfWeek
        {
            get
            {
                return this.displayDayOfWeek;
            }

            set
            {
                if (this.displayDayOfWeek != value)
                {
                    this.displayDayOfWeek = value;
                    this.RefreshDisplayText();
                }
            }
        }

        /// <summary>
        /// Specifies whether to display a checkbox for the approximate flag. 
        /// </summary>
        /// <remarks>
        /// Defaults to false. 
        /// </remarks>
        [Category("Behavior")]
        [Description("Specifies whether to display a checkbox for the approximation")]
        [DefaultValue(false)]
        public bool AllowApproximate
        {
            get
            {
                return this.approximate;
            }

            set
            {
                if (this.approximate != value)
                {
                    this.approximate = value;
                    this.PositionCheckBox();
                }
            }
        }

        /// <summary>
        /// Specifies the functionality exposed by the DateInputBox as "Simple" or "Complex".
        /// </summary>
        /// <remarks>
        /// Defaults to "Complex" so the 
        /// DateInputBox�s complete functionality is exposed. If this is set to "Simple", only 
        /// a simple date can be entered such as DateInputBox.Date.DateValue. If the functionality is set to "Simple", the DateInputBox allows other 
        /// values, such as <see cref="P:NhsCui.Toolkit.Web.DateInputBox.AllowApproximate">AllowApproximate</see> and
        /// <see cref="P:NhsCui.Toolkit.Web.DateInputBox.DatePeriod">DatePeriod</see>, to be got and set; the control does not, however, respond to 
        /// these values. In addition, attempting 
        /// to set the Value.DateType or <see cref="P:NhsCui.Toolkit.Web.DateInputBox.DateType">DateType</see> to any value other than DateType.Exact 
        /// throws an argument exception.
        /// </remarks>
        [Category("Behavior")]
        [Description("Specifies the functionality exposed by the DateInputBox (defaults to Simple). Setting Functionality to Simple sets Value.DateType to Exact.")]
        [DefaultValue(DateFunctionality.Simple)]
        public DateFunctionality Functionality
        {
            get
            {
                return this.functionality;
            }

            set
            {
                if (this.functionality != value)
                {
                    this.functionality = value;
                    this.PositionCheckBox();
                    if (this.functionality == DateFunctionality.Simple && this.DateType == DateType.NullIndex)
                    {
                        this.DateType = DateType.Exact;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the date entered in the input box.
        /// </summary>
        [Category("Behavior")]
        [Description("The date entered in the text box")]
        [RefreshProperties(RefreshProperties.All)]
        [Bindable(true), DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public NhsDate Value
        {
            get
            {
                if (this.dateValue == null)
                {
                    this.dateValue = new NhsDate();
                }

                return this.dateValue;
            }

            set
            {
                if (this.dateValue != value)
                {
                    this.dateValue = value;
                    this.ResetFields();
                    this.NotifyPropertyChanged("Value");
                }

                this.RefreshDisplayText();
                this.HandleApproxState();
            }
        }

        /// <summary>
        /// Readonly. Returns the base value.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DefaultValue(false), EditorBrowsable(EditorBrowsableState.Never)]
        [SuppressMessage("Microsoft.Usage", "CA2222:DoNotDecreaseInheritedMemberVisibility", Justification = "Required by spec.")]
        public new bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
        }

        /// <summary>
        /// Readonly. Returns the base value.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        [SuppressMessage("Microsoft.Usage", "CA2222:DoNotDecreaseInheritedMemberVisibility", Justification = "Required by spec.")]
        public new AutoSizeMode AutoSizeMode
        {
            get
            {
                return base.AutoSizeMode;
            }
        }

        /// <summary>
        /// Readonly. Returns the base value.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        [SuppressMessage("Microsoft.Usage", "CA2222:DoNotDecreaseInheritedMemberVisibility", Justification = "Required by spec.")]
        public new Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
        }

        /// <summary>
        /// Readonly. Returns the base value.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        [SuppressMessage("Microsoft.Usage", "CA2222:DoNotDecreaseInheritedMemberVisibility", Justification = "Required by spec.")]
        public new ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
        }

        /// <summary>
        /// The foreground color of the component.
        /// </summary>
        [AmbientValue(typeof(Color), "ControlText")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }

            set
            {
                if (base.ForeColor != value)
                {
                    base.ForeColor = value;
                    if (value == SystemColors.ControlText)
                    {
                        this.txtInput.ForeColor = SystemColors.WindowText;
                    }
                    else
                    {
                        this.txtInput.ForeColor = value;
                    }

                    this.chkApprox.ForeColor = value;
                    this.btnCalendar.ForeColor = value;
                    this.mainPanel.ForeColor = value;
                }
            }
        }

        /// <summary>
        /// The background color of the component.
        /// </summary>
        [AmbientValue(typeof(Color), "Control")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }

            set
            {
                if (base.BackColor != value)
                {
                    base.BackColor = value;
                    if (value == SystemColors.Control)
                    {
                        this.txtInput.BackColor = SystemColors.Window;
                    }
                    else
                    {
                        this.txtInput.BackColor = value;
                    }

                    this.chkApprox.BackColor = value;
                    this.btnCalendar.BackColor = value;
                    this.mainPanel.BackColor = value;
                }
            }
        }

        /// <summary>
        /// Readonly. Returns the base value.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        [SuppressMessage("Microsoft.Usage", "CA2222:DoNotDecreaseInheritedMemberVisibility", Justification = "Required by spec.")]
        public new BorderStyle BorderStyle
        {
            get
            {
                return base.BorderStyle;
            }
        }

        /// <summary>
        /// Gets the status of the control's value.
        /// </summary>
        [DefaultValue(true)]
        [Category("Behavior")]
        [Description("Status of the value of the control.")]
        public bool IsValid
        {
            get
            {
                return this.valid;
            }
        }

        /// <summary>
        /// Gets or sets the validation manager of the control.
        /// </summary>
        [DefaultValue(null)]
        [Category("Behavior")]
        [Description("The validation manager for the control.")]
        public IValidationManager ValidationManager
        {
            get
            {
                return this.validationManager;
            }

            set
            {
                this.validationManager = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the behavior of control for invalid text.
        /// </summary>
        /// <remarks>
        /// If true user can not lose focus from the control if the text entered is invalid.
        /// </remarks>
        [DefaultValue(true)]
        [Category("Behavior")]
        [Description("Indicates whether this component allows focus change for invalid value or not.")]
        public bool CancelOnError
        {
            get
            {
                return this.cancelOnError;
            }

            set
            {
                this.cancelOnError = value;
            }
        }

        /// <summary>
        /// Readonly. Returns the base value.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        [SuppressMessage("Microsoft.Usage", "CA2222:DoNotDecreaseInheritedMemberVisibility", Justification = "Required by spec.")]
        public new AutoValidate AutoValidate
        {
            get
            {
                return base.AutoValidate;
            }
        }
        #endregion

        #region Protected Properties
        /// <summary>
        /// Overriden. Returns the default size for the control.
        /// </summary>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(MinWidth, MinHeight);
            }
        }
        #endregion     

        #region Public Methods
        /// <summary>
        /// Resets the value of backcolor.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override void ResetBackColor()
        {
            this.txtInput.BackColor = SystemColors.Window;
            base.BackColor = SystemColors.Control;
            this.chkApprox.BackColor = SystemColors.Control;
            this.btnCalendar.BackColor = SystemColors.Control;
            this.mainPanel.BackColor = SystemColors.Control;
        }

        /// <summary>
        /// Checks whether backcolor should be serialized or not.
        /// </summary>
        /// <returns>True if backcolor should be serialized else false.</returns>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeBackColor()
        {
            return base.BackColor != SystemColors.Control;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Retrieves the resource string for the specified key.
        /// </summary>
        /// <param name="key"> Key value for which resource string will be returned.</param>
        /// <returns> Resource value for the specified key</returns>
        private static String GetResourceString(String key)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("NhsCui.Toolkit.WinForms.Common.NhsDateResources", typeof(DateInputBox).Assembly);
            return rm.GetString(key, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Sets default tooltip for the control.
        /// </summary>
        private void SetToolTip()
        {
            if (!string.IsNullOrEmpty(this.TooltipText))
            {
                this.toolTip1.SetToolTip(this.txtInput, this.TooltipText);
            }
            else
            {
                this.toolTip1.RemoveAll();                
            }

            this.toolTip1.SetToolTip(this.pictureBox1, DateInputBoxControl.Resources.openCal);
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
        /// This function performs the clean up before exiting the keydown event of txtInput.
        /// </summary>
        /// <param name="e">Key down event arguments</param>
        /// <param name="handled">Specifies the event handled inline.</param>
        private void ExitKeyDownEvent(KeyEventArgs e, bool handled)
        {
            // If handled is false then it means, let the e.handled to its previous value.
            // it doesn't mean that assign false to e.handled.
            if (handled)
            {
                e.Handled = handled;
            }

            this.inlineEditing = false;
            this.OnKeyDown(e);
        }      
       
        /// <summary>
        /// Increments the specified field.
        /// </summary>        
        private void IncrementField()
        {                        
            NhsDate tmp;
            DateField fld = this.currentField;
            if (!NhsDate.TryParseExact(this.txtInput.Text, out tmp, CultureInfo.CurrentCulture))
            {
                this.invalidFormat = true;
                return;
            }

            int mon = tmp.DateValue.Month;

            int newDayValue;
            int daysInMonth;

            switch (this.currentField)
            {
                case DateField.DayName:
                case DateField.Day:
                    tmp.DateValue = tmp.DateValue.AddDays(1);

                    if (tmp.DateValue.Month != mon)
                    {
                        tmp.DateValue = tmp.DateValue.AddMonths(-1);
                    }

                    break;
                case DateField.Month:
                    int newMonthValue = tmp.DateValue.Month;
                    newMonthValue = newMonthValue == 12 ? 1 : ++newMonthValue;
                    daysInMonth = DateTime.DaysInMonth(tmp.DateValue.Year, newMonthValue);
                    newDayValue = daysInMonth < tmp.DateValue.Day ? daysInMonth : tmp.DateValue.Day;
                    tmp.DateValue = new DateTime(tmp.DateValue.Year, newMonthValue, newDayValue, tmp.DateValue.Hour, tmp.DateValue.Minute, tmp.DateValue.Second, tmp.DateValue.Millisecond, tmp.DateValue.Kind);

                    break;
                case DateField.Year:
                    int newYearValue = tmp.DateValue.Year;
                    newYearValue = newYearValue == 9999 ? 1 : ++newYearValue;
                    daysInMonth = DateTime.DaysInMonth(newYearValue, tmp.DateValue.Month);
                    newDayValue = daysInMonth < tmp.DateValue.Day ? daysInMonth : tmp.DateValue.Day;
                    tmp.DateValue = new DateTime(newYearValue, tmp.DateValue.Month, newDayValue, tmp.DateValue.Hour, tmp.DateValue.Minute, tmp.DateValue.Second, tmp.DateValue.Millisecond, tmp.DateValue.Kind);

                    break;
            }

            this.Value = tmp;
            this.FormatOnFocusValue();
            this.currentField = fld;
            this.SelectCurrentField();
        }

        /// <summary>
        /// Decrements the specified field.
        /// </summary>        
        private void DecrementField()
        {                      
            NhsDate tmp;
            DateField fld = this.currentField;
            if (!NhsDate.TryParseExact(this.txtInput.Text, out tmp, CultureInfo.CurrentCulture))
            {
                this.invalidFormat = true;
                return;
            }

            int newDayValue;
            int daysInMonth;

            switch (this.currentField)
            {
                case DateField.DayName:
                case DateField.Day:
                    if (tmp.DateValue.Day == 1)
                    {
                        tmp.DateValue = tmp.DateValue.AddDays(DateTime.DaysInMonth(tmp.DateValue.Year, tmp.DateValue.Month) - 1);
                    }
                    else
                    {
                        tmp.DateValue = tmp.DateValue.AddDays(-1);
                    }

                    break;
                case DateField.Month:
                    int newMonthValue = tmp.DateValue.Month;
                    newMonthValue = newMonthValue == 1 ? 12 : --newMonthValue;
                    daysInMonth = DateTime.DaysInMonth(tmp.DateValue.Year, newMonthValue);
                    newDayValue = daysInMonth < tmp.DateValue.Day ? daysInMonth : tmp.DateValue.Day;
                    tmp.DateValue = new DateTime(tmp.DateValue.Year, newMonthValue, newDayValue, tmp.DateValue.Hour, tmp.DateValue.Minute, tmp.DateValue.Second, tmp.DateValue.Millisecond, tmp.DateValue.Kind);

                    break;
                case DateField.Year:
                    int newYearValue = tmp.DateValue.Year;
                    newYearValue = newYearValue == 1 ? 9999 : --newYearValue;
                    daysInMonth = DateTime.DaysInMonth(newYearValue, tmp.DateValue.Month);
                    newDayValue = daysInMonth < tmp.DateValue.Day ? daysInMonth : tmp.DateValue.Day;
                    tmp.DateValue = new DateTime(newYearValue, tmp.DateValue.Month, newDayValue, tmp.DateValue.Hour, tmp.DateValue.Minute, tmp.DateValue.Second, tmp.DateValue.Millisecond, tmp.DateValue.Kind);

                    break;
            }

            this.Value = tmp;
            this.FormatOnFocusValue();
            this.currentField = fld;
            this.SelectCurrentField();
        }

        /// <summary>
        /// Decreases the current field.
        /// </summary>
        private void HandleIncrease()
        {
            this.inlineEditing = true;
            if (!this.Enabled || this.invalidFormat || (this.editingDateType != DateType.Exact &&
               this.editingDateType != DateType.Approximate))
            {
                this.inlineEditing = false;
                return;
            }
          
            this.IncrementField();          
            this.inlineEditing = false;
        }

        /// <summary>
        /// Decreases the current field.
        /// </summary>
        private void HandleDecrease()
        {
            this.inlineEditing = true;
            if (!this.Enabled || this.invalidFormat || (this.editingDateType != DateType.Exact &&
               this.editingDateType != DateType.Approximate))
            {
                this.inlineEditing = false;
                return;
            }
         
            this.DecrementField();         
            this.inlineEditing = false;
        }

        /// <summary>
        /// Selects the specified filed
        /// </summary>
        /// <param name="field">Field to be selected.</param>
        private void SelectField(DateField field)
        {
            if (field == DateField.Null)
            {
                field = this.SetDefaultField();
            }

            this.txtInput.SelectionStart = this.GetFieldStart(field);
            this.txtInput.SelectionLength = this.GetFieldEnd(field) - this.txtInput.SelectionStart;
        }

        /// <summary>
        /// Selects the next field (from the current field) in the control.
        /// </summary>
        private void SelectNextField()
        {
            if (!this.Enabled || this.invalidFormat || (this.editingDateType != DateType.Exact &&
                this.editingDateType != DateType.Approximate))
            {
                return;
            }

            this.currentField = (DateField)(((int)this.currentField + 1) % 4);
            if (this.currentField == DateField.DayName)
            {
                this.currentField = DateField.Day;
            }

            this.SelectCurrentField();
        }

        /// <summary>
        /// Selects the previous field (from the current field) in the control.
        /// </summary>
        private void SelectPreviousField()
        {
            if (!this.Enabled || this.invalidFormat || (this.editingDateType != DateType.Exact &&
               this.editingDateType != DateType.Approximate))
            {
                return;
            }

            if (this.currentField == DateField.DayName || this.currentField == DateField.Day)
            {
                this.currentField = DateField.Year;
            }
            else
            {
                this.currentField = (DateField)((int)this.currentField - 1);
            }

            this.SelectCurrentField();
        }

        /// <summary>
        /// Selects the current field in the control.
        /// </summary>
        private void SelectCurrentField()
        {
            if (!this.Enabled || this.invalidFormat || (this.editingDateType != DateType.Exact &&
                this.editingDateType != DateType.Approximate))
            {
                return;
            }

            if (this.currentField == DateField.Null)
            {
                this.SetDefaultField();
            }

            this.txtInput.SelectionStart = this.GetFieldStart(this.currentField);
            this.txtInput.SelectionLength = this.GetFieldEnd(this.currentField) - this.txtInput.SelectionStart;
        }

        /// <summary>
        /// Private method to place a form inside the screen.  This will prevent
        /// the form of going outside the limits of the screen.
        /// </summary>
        /// <param name="sizeOfForm">The size of the form</param>
        /// <returns>A point where the form should be place to stay in the screen</returns>
        private Point CalculateLocation(Size sizeOfForm)
        {
            Point resultPoint = this.PointToScreen(this.Location);
            resultPoint = new Point(resultPoint.X - this.Location.X, resultPoint.Y - this.Location.Y + this.Height);
            int beginXaxis = 0, beginYaxis = 0, endXaxis = 0, endYaxis = 0, i = 0;
            Rectangle tmp;

            do
            {
                tmp = Screen.AllScreens[i].Bounds;
                if (beginXaxis > tmp.X)
                {
                    beginXaxis = tmp.X;
                }

                if (beginYaxis > tmp.Y)
                {
                    beginYaxis = tmp.Y;
                }

                if (endXaxis < tmp.X + tmp.Width)
                {
                    endXaxis = tmp.X + tmp.Width;
                }

                if (endYaxis < tmp.Y + tmp.Height)
                {
                    endYaxis = tmp.Y + tmp.Height;
                }

                i++;
            }
            while (i < Screen.AllScreens.Length);

            if (resultPoint.Y + sizeOfForm.Height > endYaxis)
            {
                // We are near the bottom of the screen.  If the form appears, it won't
                // be entirely shown on the screen.  So place the form above the 
                // Date Picker control.
                resultPoint = new Point(resultPoint.X, resultPoint.Y - this.Height - sizeOfForm.Height);
            }

            if (resultPoint.X < beginXaxis)
            {
                // We are to much on the left side of the screen.  This causes the 
                // form to be outside the screen.  We have to push back the form 
                // on the edge of the screen.
                resultPoint = new Point(beginXaxis, resultPoint.Y);
            }
            else if (resultPoint.X + sizeOfForm.Width > endXaxis)
            {
                // This is the opposite of the previous statement.  We are now 
                // on the right side of the screen.  We have to stick the window to
                // the edge of the right border of the screen.
                resultPoint = new Point(endXaxis - sizeOfForm.Width, resultPoint.Y);
            }

            return resultPoint;
        }                

        /// <summary>
        /// Sets the size of child controls.
        /// </summary>
        private void SizeControls()
        {
            if (this.inlineEditing)
            {
                return;
            }

            this.mainPanel.Width = this.Width;
            this.mainPanel.Height = this.Height;

            this.txtInput.Width = this.mainPanel.Width - this.btnCalendar.Width;
            this.txtInput.Height = this.mainPanel.Height;
            this.txtInput.Top = 0;
            this.txtInput.Left = 0;

            this.btnCalendar.Top = -2;
            this.btnCalendar.Height = this.mainPanel.Height + 2;
            this.btnCalendar.Left = this.txtInput.Left + this.txtInput.Width;

            this.chkApprox.Top = 2;
            this.chkApprox.Left = this.Width + PadWidth;
            this.chkApprox.Visible = false;

            if (this.Height <= DefaultPictureHeight)
            {
                this.pictureBox1.Height = this.Height;
                this.pictureBox1.Top = 0;
            }
            else if (this.Height > DefaultPictureHeight)
            {
                this.pictureBox1.Top = (int)Math.Floor((double)((this.Height - DefaultPictureHeight) / 2));
            }

            this.pictureBox1.Left = this.btnCalendar.Left + 6;

            this.mainPanel.SendToBack();
            this.pictureBox1.BringToFront();

            this.PositionCheckBox();
        }

        /// <summary>
        /// Position the check box and other controls.
        /// </summary>
        private void PositionCheckBox()
        {
            this.inlineEditing = true;
            if (this.DesignMode || this.Functionality == DateFunctionality.Simple
                || (!this.AllowApproximate))
            {
                if (this.chkApprox.Visible)
                {
                    this.chkApprox.Visible = false;
                    this.chkApprox.Checked = false;
                    this.Width -= (this.chkApprox.Width + PadWidth);
                }
            }
            else if (!this.chkApprox.Visible)
            {
                this.chkApprox.Visible = true;
                this.Width += this.chkApprox.Width + PadWidth;
            }

            this.inlineEditing = false;
        }

        /// <summary>
        /// Resets the control's state fields to their default value.
        /// </summary>
        private void ResetFields()
        {
            this.SetDefaultField();
            this.currentMode = InputMode.Simple;
            this.currentValue = this.Value;
            this.editingDateType = this.DateType;
            if ((this.editingDateType == DateType.Approximate || this.editingDateType == DateType.Exact) && !this.IsCorrectFormatForExactDate())
            {
                this.invalidFormat = true;
            }
            else
            {
                this.invalidFormat = false;
            }

            this.LoadResources();
            this.SelectCurrentField();
        }

        /// <summary>
        /// updates the value of current field.
        /// </summary>
        /// <remarks>when the control gets focus, current field is updated.</remarks>
        private void SetCurrentField()
        {           
            if (this.txtInput.SelectionStart < this.GetFieldStart(DateField.Month))
            {
                this.currentField = DateField.Day;
            }
            else if (this.txtInput.SelectionStart < this.GetFieldStart(DateField.Year))
            {
                this.currentField = DateField.Month;
            }
            else
            {
                this.currentField = DateField.Year;
            }
        }

        /// <summary>
        /// Retrieves the start index of the specified field.
        /// </summary>
        /// <param name="field">Field to search.</param>
        /// <returns>The start of the specified field</returns>
        private int GetFieldStart(DateField field)
        {            
          return (this.fieldStart[(int)field] - this.fieldStart[1]);            
        }

        /// <summary>
        /// Retrieves the start index of the specified field.
        /// </summary>
        /// <param name="field">Field to search.</param>
        /// <returns>The end of the specified field</returns>
        private int GetFieldEnd(DateField field)
        {           
           return (this.fieldEnd[(int)field] - this.fieldStart[1]);           
        }

        /// <summary>
        /// Sets the value of current field to its default value.
        /// </summary>
        /// <returns>
        /// default field
        /// </returns>
        private DateField SetDefaultField()
        {          
           this.currentField = DateField.Day;          
            return this.currentField;
        }

        /// <summary>
        /// Returns the display string.
        /// </summary>
        /// <returns>String.</returns>
        private string GetDisplayString()
        {
            NhsDate value = this.Value;
            string[] nullValues = this.NullStrings;

            // this function assumes that always set text to watermarktext if DateType is null.
            // calling functions have to make sure that if the control has focus then set text to null.
            if (this.DateType == DateType.Null)
            {
                return (this.IsValid ? this.WatermarkText : this.invalidText);
            }
            else
            {
                if (value.DateType != DateType.NullIndex || nullValues == null ||
                       value.NullIndex < 0 || value.NullIndex >= nullValues.Length)
                {
                    return value.ToString(this.DisplayDayOfWeek, false, this.DisplayDateAsText, CultureInfo.CurrentCulture);
                }
                else
                {
                    return nullValues[value.NullIndex];
                }
            }
        }

        /// <summary>
        /// Sets watermark mode for the control.
        /// </summary>
        private void SetWatermarkMode()
        {
            this.watermarkMode = true;
            this.txtInput.ForeColor = SystemColors.GrayText;
        }

        /// <summary>
        /// Clears watermark mode for the control.
        /// </summary>
        private void ClearWatermarkMode()
        {
            this.watermarkMode = false;
            this.txtInput.ForeColor = this.ForeColor;
        }

        /// <summary>
        /// Refresh the display text of the control.
        /// </summary>
        private void RefreshDisplayText()
        {
            this.ClearWatermarkMode();
            this.inlineEditing = true;

            if (this.DateType != DateType.Null)
            {
                this.SetValid();
            }

            this.txtInput.Text = this.GetDisplayString();
            if (this.DateType == DateType.Null && this.IsValid)
            {
                this.SetWatermarkMode();
            }

            this.inlineEditing = false;           
        }

        /// <summary>
        /// Enables/Disables the chkapprox child control.
        /// </summary>
        private void HandleApproxState()
        {
            if (this.editingDateType == DateType.Approximate)
            {
                this.chkApprox.Checked = true;
            }            
            else if (this.editingDateType != DateType.Exact)
            {
                this.chkApprox.Checked = false;
            }

            this.chkApprox.Enabled = (this.editingDateType == DateType.Exact || this.editingDateType == DateType.Approximate);
        }

        /// <summary>
        /// Loads the resource strings and sets the field values.
        /// </summary>
        private void LoadResources()
        {
            this.chkApprox.Text = GetResourceString("Approximate");           
        }

        /// <summary>
        /// Checks if the current date value is appropriate for specified arithmetic operation
        /// </summary>
        /// <returns> True if value is correct for arithmetic operations, else false.</returns>
        private bool IsValidTextForArithmetic()
        {
            if (this.DateType == DateType.Approximate || this.DateType == DateType.Exact)
            {
                return true;
            }

            if (this.DateType == DateType.Year && this.txtInput.Text.EndsWith(GetResourceString("YearsUnit"), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (this.DateType == DateType.YearMonth && (this.txtInput.Text.EndsWith(GetResourceString("YearsUnit"), StringComparison.OrdinalIgnoreCase) || this.txtInput.Text.EndsWith(GetResourceString("MonthsUnit"), StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the control's current value as invalid.
        /// </summary>
        /// <param name="status">Update status. Set to false by the function.</param>
        private void SetInvalid(ref bool status)
        {
            this.valid = false;
            this.invalidText = this.txtInput.Text;
            this.Value.DateType = DateType.Null;
            this.chkApprox.Checked = false;
            this.chkApprox.Enabled = false;
            status = false;
            if (this.ValidationManager != null)
            {
                this.ValidationManager.SetError(this, DateInputBoxControl.Resources.invalidValue);
            }
        }

        /// <summary>
        /// Sets the control's current value as valid.
        /// </summary>
        private void SetValid()
        {
            this.valid = true;
            this.dateEdited = false;
            this.invalidText = String.Empty;
            if (this.ValidationManager != null)
            {
                this.ValidationManager.ClearError(this);
            }
        }

        /// <summary>
        /// Refresh the text displayed in the text box.
        /// </summary>
        /// <returns>
        /// Status of update.
        /// </returns>
        private bool ValidateAndUpdateValue()
        {
            NhsDate result;
            bool status = true;
            this.SetValid();
            this.txtInput.Text = this.txtInput.Text.Trim();
            if (this.editingDateType == DateType.NullIndex)
            {
                int index = this.GetNullIndexExact(this.txtInput.Text);
                if (index >= 0)
                {
                    this.NullIndex = index;
                    this.DateType = DateType.NullIndex;
                }
                else
                {
                    this.SetInvalid(ref status);
                }

                return status;
            }

            try
            {
                if (NhsDate.IsAddValid(this.txtInput.Text))
                {
                    if (this.Functionality == DateFunctionality.Complex && this.IsValidTextForArithmetic())
                    {
                        this.Value.Add(this.txtInput.Text);
                        this.RefreshDisplayText();
                        this.ResetFields();
                    }
                    else
                    {
                        this.SetInvalid(ref status);
                    }
                }
                else if (NhsDate.TryParseExact(this.txtInput.Text, out result, CultureInfo.CurrentCulture))
                {
                    // Check for functionality setting.
                    if (this.Functionality == DateFunctionality.Complex || result.DateType == DateType.Approximate || result.DateType == DateType.Exact || result.DateType == DateType.Null)
                    {
                        this.Value = result;
                    }
                    else
                    {
                        this.SetInvalid(ref status);
                    }
                }
                else
                {
                    this.SetInvalid(ref status);
                }
            }
            catch (FormatException)
            {
                this.SetInvalid(ref status);
            }
            catch (ArgumentOutOfRangeException)
            {
                this.SetInvalid(ref status);
            }

            this.txtInput.SelectionStart = this.txtInput.TextLength;
            this.txtInput.SelectionLength = 0;
            return status;
        }

        /// <summary>
        /// This function performs the clean up before exiting the keypress event of txtInput.
        /// </summary>
        /// <param name="e">Key Press event argument</param>
        /// <param name="handled">Specifies the event handled inline.</param>
        private void ExitKeyPressEvent(KeyPressEventArgs e, bool handled)
        {
            // If handled is false then it means, let the e.handled to its previous value.
            // it doesn't mean that assign false to e.handled.
            if (handled)
            {
                e.Handled = handled;               
            }            

            this.inlineEditing = false;
            this.OnKeyPress(e);
        }     

        /// <summary>
        /// Checks if null string input is enabled in the control.
        /// </summary>
        /// <returns>True if null strings can be entered in the control, else false.</returns>
        private bool IsNullEnabled()
        {
            return (this.Functionality == DateFunctionality.Complex && this.NullStrings.Length > 0 && this.currentMode == InputMode.Simple);
        }

        /// <summary>
        /// Displays the appropriate null string in the control
        /// </summary>
        private void DisplayNullString()
        {
            if (this.IsNullEnabled())
            {
                int index = this.GetNullIndexExact(this.txtInput.Text);
                index = index < 0 ? 0 : (index + 1) % this.NullStrings.Length;
                this.editingDateType = DateType.NullIndex;
                this.txtInput.Text = this.NullStrings[index];
                this.txtInput.SelectionStart = this.txtInput.TextLength;
            }
        }

        /// <summary>
        /// Validates the format of string in txtInput.
        /// </summary>
        /// <remarks>
        /// Cut/Paste can change the pattern of input string, however, dateType remains same. This
        /// function is called to validate the string format in txtInput for specified dateType. 
        /// </remarks>
        /// <returns>True if pattern is correct, else false.</returns>
        private bool IsCorrectFormatForExactDate()
        {
            NhsDate tmp;
            if (NhsDate.TryParseExact(this.txtInput.Text, out tmp, CultureInfo.CurrentCulture))
            {
                if (tmp.DateType == DateType.Exact || tmp.DateType == DateType.Approximate)
                {                    
                    if (tmp.ToString(false, false, false, CultureInfo.CurrentCulture) == this.txtInput.Text)
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }

            return false;          
        }

        /// <summary>
        /// Validates the format of string in txtInput.
        /// </summary>
        /// <remarks>
        /// Cut/Paste can change the pattern of input string, however, dateType remains same. This
        /// function is called to validate the string format in txtInput for specified dateType. 
        /// </remarks>
        /// <returns>True if pattern is correct, else false.</returns>
        private bool IsCorrectFormat()
        {
            if (this.IsArithmeticModeFormat())
            {
                this.currentMode = InputMode.Arithmetic;
                return true;
            }

            if (this.functionality == DateFunctionality.Simple)
            {
                return this.IsCorrectFormatForExactDate();
            }          

            if (this.GetNullIndexExact(this.txtInput.Text) >= 0)
            {
                this.editingDateType = DateType.NullIndex;
                return true;
            }

            NhsDate tmp;
            if (NhsDate.TryParseExact(this.txtInput.Text, out tmp, CultureInfo.CurrentCulture))
            {
                this.editingDateType = tmp.DateType;

                // entered date is valid, but it might not be in dd-MMM-yyyy format.
                if (this.editingDateType == DateType.Exact || this.editingDateType == DateType.Approximate)
                {                    
                    if (tmp.ToString(false, false, false, CultureInfo.CurrentCulture) == this.txtInput.Text)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }                        
        }

        /// <summary>
        /// Handles the key pressed when user is in arithmetic mode
        /// </summary>
        /// <param name="startsWithSpecialChar"> 
        /// Flag specifying if the input begins with a special character i.e. +/-
        /// </param>
        /// <param name="maxLength"> Max length of input</param>
        /// <param name="e">KeyPress event argument</param>
        private void HandleKeyInputInArithmeticMode(bool startsWithSpecialChar, int maxLength, KeyPressEventArgs e)
        {
            String currentInputTillSelection = this.txtInput.Text.Substring(0, this.txtInput.SelectionStart) + e.KeyChar.ToString(CultureInfo.CurrentCulture);
            String resultString = currentInputTillSelection + this.txtInput.Text.Substring(this.txtInput.SelectionStart);
            if (NhsDate.IsAddValid(resultString))
            {
                if (Char.IsDigit(e.KeyChar))
                {
                    if (startsWithSpecialChar)
                    {
                        if (this.txtInput.SelectionStart > 0 && this.txtInput.SelectionStart < this.txtInput.TextLength && resultString.Length <= maxLength)
                        {
                            this.acceptedKey = true;
                        }
                    }
                    else if (this.txtInput.SelectionStart < this.txtInput.TextLength && resultString.Length <= maxLength)
                    {
                        this.acceptedKey = true;
                    }
                }
                else
                {
                    this.acceptedKey = true;
                }
            }

            // Input string is not a valid add instruction. It infers that either h isn't present or h is present immediately after +.
            // We will allow only digits to be entered if the length permits. Maxlength in this case is maxlength - 1.
            else
            {
                if (Char.IsDigit(e.KeyChar))
                {
                    if (startsWithSpecialChar)
                    {
                        if (this.txtInput.SelectionStart > 0 && resultString.Length < maxLength)
                        {
                            this.acceptedKey = true;
                        }
                    }
                    else if (resultString.Length < maxLength)
                    {
                        this.acceptedKey = true;
                    }
                }
            }
        }

        /// <summary>
        /// Identifies if the key pressed in the txtInput is valid for arithmetic mode.
        /// </summary>
        /// <param name="e">Keypress event argument</param>
        /// <returns>True if the Key Pressed belongs to arithmetic mode, else false</returns>
        private bool IsArithmeticMode(KeyPressEventArgs e)
        {
            String currentInputTillSelection = this.txtInput.Text.Substring(0, this.txtInput.SelectionStart) + e.KeyChar.ToString(CultureInfo.CurrentCulture);
            bool startsWithSpecialChar = false;
            int maxLength = 3;
            this.acceptedKey = false;
            if (this.currentMode == InputMode.Arithmetic)
            {
                if (e.KeyChar == '+' || e.KeyChar == '-')
                {
                    e.Handled = true;
                    return true;
                }

                if (this.txtInput.Text.StartsWith("+", StringComparison.CurrentCultureIgnoreCase)
                    || this.txtInput.Text.StartsWith("-", StringComparison.CurrentCultureIgnoreCase))
                {
                    startsWithSpecialChar = true;
                    maxLength = 4;
                }

                this.HandleKeyInputInArithmeticMode(startsWithSpecialChar, maxLength, e);
                e.Handled = !this.acceptedKey;
                return true;
            }
            else
            {
                if (this.txtInput.Text.StartsWith("+", StringComparison.CurrentCultureIgnoreCase)
                  || this.txtInput.Text.StartsWith("-", StringComparison.CurrentCultureIgnoreCase))
                {
                    startsWithSpecialChar = true;
                }

                if ((NhsDate.IsAddValid(currentInputTillSelection) && startsWithSpecialChar) ||
                    ((e.KeyChar == '+' || e.KeyChar == '-') && this.txtInput.SelectionStart == 0))
                {
                    this.currentMode = InputMode.Arithmetic;
                    this.txtInput.Text = currentInputTillSelection;
                    this.txtInput.SelectionStart = this.txtInput.TextLength;
                    e.Handled = true;
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Identifies if the text in txtInput is an arithmetic mode string.
        /// </summary>        
        /// <returns>True if the text belongs to arithmetic mode, else false</returns>
        private bool IsArithmeticModeFormat()
        {
            int maxLength;
            bool startsWithSpecialChar = false;
            String resultString = this.txtInput.Text;

            if (resultString.StartsWith("+", StringComparison.CurrentCultureIgnoreCase)
                || resultString.StartsWith("-", StringComparison.CurrentCultureIgnoreCase))
            {
                maxLength = 4;
            }
            else
            {
                maxLength = 3;
            }

            if (this.txtInput.Text.StartsWith("+", StringComparison.CurrentCultureIgnoreCase)
                 || this.txtInput.Text.StartsWith("-", StringComparison.CurrentCultureIgnoreCase))
            {
                startsWithSpecialChar = true;
            }

            if (NhsDate.IsAddValid(resultString) && resultString.Length <= maxLength && startsWithSpecialChar)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Updates the Null String values in the child calendar.
        /// </summary>
        private void UpdateCalendar()
        {
            if (this.calendar != null)
            {
                if (this.IsNullEnabled())
                {
                    this.calendar.NullStrings = this.NullStrings;
                    this.calendar.NullIndex = this.NullIndex;
                }
                else
                {
                    this.calendar.NullStrings = null;
                }
            }
        }       
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the mouse wheel event of the txtinput.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TxtInput_MouseWheel(object sender, MouseEventArgs e)
        {          
            this.inlineEditing = true;

            if (!this.Enabled || this.currentMode == InputMode.Arithmetic || this.invalidFormat || this.editingDateType == DateType.Null
              || this.editingDateType == DateType.NullIndex || this.editingDateType == DateType.Year || this.editingDateType == DateType.YearMonth || this.dateEdited)
            {
                this.inlineEditing = false;
                return;
            }

            this.SetCurrentField();

            if (e.Delta < 0)
            {
                this.HandleDecrease();
            }
            else
            {
                this.HandleIncrease();
            }

            this.inlineEditing = false;
        }

        /// <summary>
        /// Hanldes the leave event of control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void DateInputBox_Leave(object sender, EventArgs e)
        {
            if (!this.validated)
            {
                if (!this.ValidateAndUpdateValue() && this.cancelOnError)
                {
                    this.txtInput.Focus();
                }

                this.validated = true;
            }
        }

        /// <summary>
        /// Handles the key down event of the child textbox.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            this.inlineEditing = true;

            // It is important to refresh the current field when a key is pressed.
            // because end user might have changed the current selection start by
            // selecting some text.
            this.SetCurrentField();

            if (e.KeyCode == Keys.Down && e.Alt)
            {
                this.ValidateAndUpdateValue();
                this.validated = true;
                this.Button1_Click(null, null);
                this.ExitKeyDownEvent(e, true);
                return;
            }

            if (e.KeyCode == Keys.Delete && (this.editingDateType == DateType.Exact || this.editingDateType == DateType.Approximate) && this.IsCorrectFormatForExactDate())
            {
                this.DateType = DateType.Null;

                // to ensure text is updated for null date type.
                this.inlineEditing = true;
                this.txtInput.Text = String.Empty;
                this.inlineEditing = false;

                this.ClearWatermarkMode();
                this.ExitKeyDownEvent(e, true);
            }

            if (e.KeyCode == Keys.Back)
            {
                this.ExitKeyDownEvent(e, false);
                return;
            }

            if (!this.Enabled || this.currentMode == InputMode.Arithmetic || this.invalidFormat || this.editingDateType == DateType.Null
                || this.editingDateType == DateType.NullIndex || this.editingDateType == DateType.Year || this.editingDateType == DateType.YearMonth || this.dateEdited)
            {
                this.ExitKeyDownEvent(e, false);
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Up:
                    this.HandleIncrease();
                    e.Handled = true;
                    break;
                case Keys.Down:
                    this.HandleDecrease();
                    e.Handled = true;
                    break;
                case Keys.Right:                   
                    this.SelectNextField();
                    e.Handled = true;                   
                    break;
                case Keys.Left:                    
                    this.SelectPreviousField();
                    e.Handled = true;                    
                    break;
            }

            this.ExitKeyDownEvent(e, false);
        }  

        /// <summary>
        /// Pops up the calendar.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void Button1_Click(object sender, EventArgs e)
        {            
            if (this.calendar == null)
            {
                this.calendar = new NhsCalendar();
                this.calendar.ParentControl = this;
                this.calendar.Deactivate += new EventHandler(this.Calendar_Deactivate);
                this.calendar.VisibleChanged += new EventHandler(this.Calendar_VisibleChanged);
                this.calendar.SelectedDateChanged += new EventHandler<DateChangedEventArgs>(this.Calendar_SelectedDateChanged);
            }

            // set selected date of calendar.

            this.inlineEditing = true;
            
            // Value is validated before calling the button click.
            switch (this.DateType)
            {
                case DateType.Exact:
                case DateType.Approximate:
                    this.calendar.SelectedDate = this.DateValue;
                    break;
                case DateType.Year:
                    this.calendar.SelectedDate = new DateTime(this.Year, this.DateValue.Month, this.DateValue.Day);
                    break;
                case DateType.YearMonth:
                    this.calendar.SelectedDate = new DateTime(this.Year, this.Month, this.DateValue.Day);
                    break;                                        
                default:
                    this.calendar.SelectedDate = DateTime.Now;
                    break;
            }                             

            this.inlineEditing = false;
            this.calendar.Location = this.CalculateLocation(this.calendar.Size);
            this.UpdateCalendar();
            this.calendar.Show();
            this.calendar.Focus();
        }

        /// <summary>
        /// Handles visibility changed event of the control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void Calendar_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.calendar.Visible)
            {
                this.Focus();
                this.FormatOnFocusValue();
            }
        }

        /// <summary>
        /// Handles the deactivate event of calendar control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void Calendar_Deactivate(object sender, EventArgs e)
        {
            this.calendar.Hide();
            this.txtInput.Focus();
        }

        /// <summary>
        /// Handles the selected date changed event of calendar.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void Calendar_SelectedDateChanged(object sender, DateChangedEventArgs e)
        {
            if (this.inlineEditing)
            {
                this.inlineEditing = false;
                return;
            }

            if ((e.Changes & ChangedDateParts.YearChanged) != ChangedDateParts.YearChanged && (e.Changes & ChangedDateParts.MonthChanged) != ChangedDateParts.MonthChanged)
            {
                if (this.Functionality == DateFunctionality.Complex || ((e.Changes & ChangedDateParts.YearSelected) != ChangedDateParts.YearSelected && (e.Changes & ChangedDateParts.MonthSelected) != ChangedDateParts.MonthSelected && (e.Changes & ChangedDateParts.NullStringSelected) != ChangedDateParts.NullStringSelected))
                {
                    this.Value = new NhsDate(e.SelectedDate);
                    if ((e.Changes & ChangedDateParts.YearSelected) == ChangedDateParts.YearSelected)
                    {
                        this.DateType = DateType.Year;
                        this.editingDateType = DateType.Year;
                        this.Year = e.SelectedDate.Year;
                    }
                    else if ((e.Changes & ChangedDateParts.MonthSelected) == ChangedDateParts.MonthSelected)
                    {
                        this.DateType = DateType.YearMonth;
                        this.editingDateType = DateType.YearMonth;
                        this.Year = e.SelectedDate.Year;
                        this.Month = e.SelectedDate.Month;
                    }
                    else if ((e.Changes & ChangedDateParts.NullStringSelected) == ChangedDateParts.NullStringSelected)
                    {
                        this.DateType = DateType.NullIndex;
                        this.editingDateType = DateType.NullIndex;
                        this.NullIndex = this.calendar.NullIndex;
                    }

                    this.calendar.Hide();
                    this.currentValue = this.Value;
                    this.txtInput.Focus();
                    this.validated = false;
                    this.FormatOnFocusValue();
                }
            }
        }      

        /// <summary>
        /// Handles the resize event of control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void DateInputBox_Resize(object sender, EventArgs e)
        {
            this.SizeControls();
        }

        /// <summary>
        /// Handles the checked changed event of chkapprox.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void ChkApprox_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.valid)
            {
                 this.chkApprox.Checked = false;
                 this.chkApprox.Enabled = false;
                 return;
            }

            if (this.chkApprox.Checked)
            {
                this.Value.DateType = DateType.Approximate;
            }
            else if (this.DateType == DateType.Approximate)
            {
                this.Value.DateType = DateType.Exact;
            }
        }

        /// <summary>
        /// Handles the enter event of the child textbox
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TxtInput_Enter(object sender, EventArgs e)
        {
            this.validated = false;
           
            this.FormatOnFocusValue();

            this.ResetFields();
            this.SetCurrentField();
           
            this.txtInput.SelectionStart = 0;
            this.txtInput.SelectionLength = this.txtInput.TextLength;
        }

        /// <summary>
        /// Checks the input for exact date format.
        /// </summary>
        /// <returns>True if input is valid exact date else false</returns>
        /// <remarks>
        /// It is different from IsCorrectFormatForExactDate.
        /// </remarks>
        private bool InputValidForApprox()
        {
            NhsDate tmp;
            if (NhsDate.TryParseExact(this.txtInput.Text, out tmp, CultureInfo.CurrentCulture))
            {
                return (tmp.DateType == DateType.Exact);                
            }

            return false;
        }
        
        /// <summary>
        /// Handles the text changed event of the child textbox
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TxtInput_TextChanged(object sender, EventArgs e)
        {
            this.validated = false;
            this.chkApprox.Enabled = this.InputValidForApprox();
            if (this.inlineEditing)
            {
                if (this.editingDateType == DateType.Exact || this.editingDateType == DateType.Approximate)
                {
                    this.invalidFormat = !this.IsCorrectFormatForExactDate();
                }

                return;
            }

            if (this.selectedPaste)
            {
                this.selectedPaste = false;
                return;
            }

            if (this.acceptedKey)
            {
                this.acceptedKey = false;
                return;
            }

            if (!this.IsCorrectFormat())
            {
                this.invalidFormat = true;
            }
            else
            {
                this.invalidFormat = false;
            }
        }

        /// <summary>
        /// Selects the text of txtinput control from specified start to the end.
        /// </summary>
        /// <param name="start">Selection Start</param>
        private void SelectToEnd(int start)
        {
            this.txtInput.SelectionStart = start;
            this.txtInput.SelectionLength = this.txtInput.TextLength - start;
        }

        /// <summary>
        /// Handles the key press in txt input when allow date change is true.
        /// </summary>
        /// <param name="e">Key Press Event Argument</param>
        /// <returns>True if the event is handled, else false.</returns>
        private bool HandleAllowDateChange(KeyPressEventArgs e)
        {
            int temp;
            int selectionStart = this.txtInput.SelectionStart;          

            // case for null index.                                    
            temp = this.GetNullIndex(this.txtInput.Text.Substring(0, this.txtInput.SelectionStart) + e.KeyChar.ToString());
            if (temp >= 0 && this.IsNullEnabled())
            {
                this.txtInput.Text = this.NullStrings[temp];
                this.editingDateType = DateType.NullIndex;
                if (this.GetNullIndex(this.txtInput.Text.Substring(0, this.txtInput.SelectionStart) + e.KeyChar.ToString(), temp + 1) >= 0)
                {
                    this.SelectToEnd(selectionStart + 1);
                }
                else
                {
                    this.txtInput.SelectionStart = this.txtInput.TextLength;
                }

                this.invalidFormat = false;
                e.Handled = true;
                return true;
            }

            return false;            
        }

        /// <summary>
        /// Retrieves the index from the collection Null Strings, which starts with the specified string.
        /// </summary>
        /// <param name="newString">String to be searched in the Null Strings collection.</param>
        /// <returns>Index of the collection where match is found, if not found then -1 is returned. </returns>
        private int GetNullIndex(String newString)
        {
            for (int i = 0; i < this.NullStrings.Length; i++)
            {
                if (this.NullStrings[i].StartsWith(newString, StringComparison.CurrentCultureIgnoreCase))
                {
                    return i;
                }
            }

            return (-1);
        }

        /// <summary>
        /// Retrieves the index from the collection Null Strings, which starts with the specified string.
        /// </summary>
        /// <param name="newString">String to be searched in the Null Strings collection.</param>
        /// <param name="start">Start Index</param>
        /// <returns>Index of the collection where match is found, if not found then -1 is returned. </returns>
        private int GetNullIndex(String newString, int start)
        {
            for (int i = start; i < this.NullStrings.Length; i++)
            {
                if (this.NullStrings[i].StartsWith(newString, StringComparison.CurrentCultureIgnoreCase))
                {
                    return i;
                }
            }

            return (-1);
        }

        /// <summary>
        /// Retrieves the index from the collection Null Strings, which matches the specified string.
        /// </summary>
        /// <param name="newString">String to be searched in the Null Strings array.</param>
        /// <returns>Index of the array where match is found, if not found then -1 is returned. </returns>
        private int GetNullIndexExact(String newString)
        {
            for (int i = 0; i < this.NullStrings.Length; i++)
            {
                if (this.NullStrings[i].Equals(newString, StringComparison.CurrentCultureIgnoreCase))
                {
                    return i;
                }
            }

            return (-1);
        }

        /// <summary>
        /// Formats the string in textbox when the control has focus.
        /// </summary>
        /// <remarks>
        /// There are two exceptional conditions if the control has focus
        /// a) Watermark text is not displayed if the datetype is null.
        /// b) DateAsText is always considered false, when the control has focus.
        /// </remarks>
        private void FormatOnFocusValue()
        {
            this.inlineEditing = true;

            // to ensure text is empty and not watermark text for null date type.                
            if (this.DateType == DateType.Null)
            {
                this.txtInput.Text = this.IsValid ? String.Empty : this.invalidText;
            }
            else if (this.Value.DateType != DateType.NullIndex)
            {
                this.txtInput.Text = this.Value.ToString(
                                           false,
                                           false,
                                           false,
                                           CultureInfo.CurrentCulture);
            }

            this.inlineEditing = false;
            this.txtInput.SelectionStart = this.txtInput.TextLength;
            this.txtInput.SelectionLength = 0;
            this.ClearWatermarkMode();
        }

        /// <summary>
        /// Handles the Key Press event of the child textbox
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.MSInternal", "CA908:UseApprovedGenericsForPrecompiledAssemblies", Justification = "Generics aren't being used")]
        private void TxtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.inlineEditing = true;

            // Backspace key
            if (e.KeyChar == '\b')
            {
                this.ExitKeyPressEvent(e, false);
                return;
            }

            if (Array.IndexOf(this.allowedSpecialKeys, (int)e.KeyChar) >= 0)
            {
                if ((int)e.KeyChar == 22 && this.txtInput.SelectedText.Length > 0)
                {
                    this.selectedPaste = true;
                }

                this.ExitKeyPressEvent(e, false);
                return;
            }

            // case to handle enter key
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                // Implement lost focus on enter key
                this.TopLevelControl.SelectNextControl(this.txtInput, true, true, true, true);

                // this.ValidateAndUpdateValue();
                // this.FormatOnFocusValue();

                this.ExitKeyPressEvent(e, true);
                return;
            }

            // Case to handle Null Index Values
            if (e.KeyChar == ' ')
            {
                // Show null strings only if cursor is at the beginning or current date type is null index
                if (this.IsNullEnabled() && (this.txtInput.SelectionStart == 0 || this.editingDateType == DateType.NullIndex))
                {
                    this.DisplayNullString();
                    this.ExitKeyPressEvent(e, true);
                }
                else
                {
                    this.ExitKeyPressEvent(e, false);
                }

                return;
            }

            if (this.Functionality == DateFunctionality.Complex)
            {
                // case to check whether current input belongs to arithmetic mode
                if (this.IsArithmeticMode(e))
                {
                    this.invalidFormat = false;
                    this.ExitKeyPressEvent(e, false);
                    return;
                }
                else
                {
                    this.currentMode = InputMode.Simple;
                }

                // case to check whether the entered key leds to some special type.
                // applies to all date type provided date type change is allowed.
                if (this.HandleAllowDateChange(e))
                {
                    this.ExitKeyPressEvent(e, false);
                    return;
                }
            }

            this.dateEdited = true;
            this.inlineEditing = false;
            if (!this.InputValidConsideringSpecialString(e))
            {
                if (this.ValidMonth(e))
                {
                    this.editingDateType = DateType.YearMonth;
                }

                this.ExitKeyPressEvent(e, true);
            }

            this.OnKeyPress(e);
        }

        /// <summary>
        /// Checks if the entered character forms a string which belongs to a month or not
        /// </summary>
        /// <param name="e">Key press event arguments</param>
        /// <returns>True if current input (including entered char) is part of a month, else false</returns>
        private bool ValidMonth(KeyPressEventArgs e)
        {
            string newString = this.txtInput.Text.Substring(0, this.txtInput.SelectionStart) + e.KeyChar.ToString();
            string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            for (int i = 0; i < monthNames.Length; i++)
            {
                if (monthNames[i].StartsWith(newString, StringComparison.CurrentCultureIgnoreCase))
                {
                    this.txtInput.Text = newString;
                    this.SelectToEnd(this.txtInput.TextLength);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the keypressed is valid for nullstring input or not.
        /// </summary>
        /// <param name="e">Keypress event arguments</param>
        /// <returns>True if entered key is valid, else false</returns>
        private Boolean InputValidConsideringSpecialString(KeyPressEventArgs e)
        {
            int index;
            int startIndex = this.txtInput.SelectionStart;            

            // return true for empty string
            if (this.txtInput.Text.Trim().Length == 0 || startIndex == 0)
            {
                return true;
            }            

            // case when character is not entered at first position.
            index = this.GetNullIndex(this.txtInput.Text.Substring(0, startIndex));
            if (index >= 0)
            {
                index = this.GetNullIndex(this.txtInput.Text.Substring(0, startIndex) + e.KeyChar.ToString());
                if (index >= 0)
                {
                    return true;
                }

                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Handles the mouse click event of the textbox.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>  
        /// <remarks>
        /// Selects the current field in the control.
        /// </remarks>
        private void TxtInput_MouseClick(object sender, MouseEventArgs e)
        {
            this.SetCurrentField();
            this.SelectCurrentField();
        }

        /// <summary>
        /// Handles the font changed event of the control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param> 
        private void DateInputBox_FontChanged(object sender, EventArgs e)
        {
            GlobalizationService gs = new GlobalizationService();
            string max = gs.ShortDatePatternWithDayOfWeek;
            Graphics g = this.CreateGraphics();
            SizeF newSize = g.MeasureString(max, this.Font);
            this.Width = newSize.Width + this.btnCalendar.Width > MinWidth ? (int)newSize.Width + this.btnCalendar.Width : MinWidth;
            this.Height = newSize.Height + BorderPadding > MinHeight ? (int)newSize.Height + BorderPadding : MinHeight;
        }

        /// <summary>
        /// Handles the forecolor changed event of the control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param> 
        private void DateInputBox_ForeColorChanged(object sender, EventArgs e)
        {
            if (!this.watermarkMode)
            {
                this.txtInput.ForeColor = this.ForeColor; 
            }
        }

        /// <summary>
        /// Handles the field double clicked event of the control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param> 
        private void TxtInput_FieldDoubleClicked(object sender, EventArgs e)
        {
            this.SetCurrentField();
            this.SelectCurrentField();
        }

        /// <summary>
        /// Handles the validating event of the textbox.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param> 
        private void TxtInput_Validating(object sender, CancelEventArgs e)
        {
            if (!this.validated)
            {
                if (!this.ValidateAndUpdateValue())
                {
                    e.Cancel = this.cancelOnError;
                }

                this.validated = true;
            }
        }

        /// <summary>
        /// Handles the click event of picture box
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param> 
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.Button1_Click(null, null);
        }

        /// <summary>
        /// Hanldes enter event of calendar button
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param> 
        private void BtnCalendar_Enter(object sender, EventArgs e)
        {
            this.inlineEditing = true;
            this.pictureBox1.Height -= 3;
            this.pictureBox1.Width -= 2;
            this.pictureBox1.Left += 1;
            this.pictureBox1.Top += 2;
            this.btnCalendar.Height -= 2;
            this.btnCalendar.Width -= 2;
            this.Width += 2;
            this.mainPanel.Width += 2;
            this.btnCalendar.FlatStyle = FlatStyle.Standard;
            this.inlineEditing = false;
        }

        /// <summary>
        /// /// Hanldes leave event of calendar button
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param> 
        private void BtnCalendar_Leave(object sender, EventArgs e)
        {
            this.inlineEditing = true;
            this.pictureBox1.Height += 3;
            this.pictureBox1.Width += 2;
            this.pictureBox1.Left -= 1;
            this.pictureBox1.Top -= 2;
            this.btnCalendar.Height += 2;
            this.btnCalendar.Width += 2;
            this.Width -= 2;
            this.mainPanel.Width -= 2;
            this.btnCalendar.FlatStyle = FlatStyle.Flat;
            this.inlineEditing = false;
        }

        /// <summary>
        /// Handles the key up event of textbox.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param> 
        private void TxtInput_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }
        #endregion       
    }
}
