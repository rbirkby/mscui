//-----------------------------------------------------------------------
// <copyright file="TimeInputBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>12-Apr-2007</date>
// <summary>The control used to enter a time.</summary>
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
    using System.Windows.Forms;
    using NhsCui.Toolkit.WinForms;
    using System.Globalization;
    using NhsCui.Toolkit;
    using NhsCui.Toolkit.DateAndTime;
    using NhsCui.Toolkit.WinForms.TimeInputBoxControl;
    using System.Diagnostics.CodeAnalysis;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The control used to get the time input from the user.
    /// </summary>
    [DefaultProperty("Value")]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "TimeInputBox.bmp")]
    public partial class TimeInputBox : UserControl, INotifyPropertyChanged
    {
        #region Const Values
        /// <summary>
        /// Padding width for distance between chkapprox and control's right.
        /// </summary>
        private const int PadWidth = 10;

        /// <summary>
        /// Specifies the distance of AM PM field from the last time field.
        /// </summary>
        /// <remarks>
        /// Last time field depends upon the display format. It may be minutes or seconds.
        /// </remarks>
        private const int DistOfAMPM = 4;      

        /// <summary>
        /// Spin padding.
        /// </summary>
        private const int SpinPadding = 1;

        /// <summary>
        /// Min width for up and down buttons
        /// </summary>
        private const int MinButtonWidth = 10;

        /// <summary>
        /// Min width of control
        /// </summary>
        private const int MinWidth = 110;

        /// <summary>
        /// Min height of control
        /// </summary>
        private const int MinHeight = 20;

        /// <summary>
        /// Padding for control's border
        /// </summary>
        private const int BorderPadding = 4;
        #endregion

        #region Member Vars
        /// <summary>
        /// Toggle value to specify current text is invalid w.r.t. to current editing time type.
        /// </summary>
        private bool invalidFormat;

        /// <summary>
        /// Flag to indicate control is getting focus after the spin.
        /// </summary>
        private bool focusAfterSpin;

        /// <summary>
        /// Toggle value to specify text is pasted in txtInput with selection or not.
        /// </summary>
        /// <remarks>If text is selected and then pasted, then the text_change event is fired twice
        /// Once with removing the selected text and then with adding the new text. 
        /// </remarks>
        private bool selectedPaste;

        /// <summary>
        /// Keys directly allowed in the txtInput. No validation is required for these keys.
        /// </summary>
        private int[] allowedSpecialKeys = new int[] { 3, 22, 24 };
        
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
        /// Toggle value to allow/forbid approximate input in the control.
        /// </summary>
        /// <remarks>Saves the value of property AllowApproximate</remarks>
        private bool approximate;

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
        /// Functionality of the control. Defaults to TimeFunctionality.Complex.
        /// </summary>
        /// <remarks>Saves the value of property Functionality</remarks>
        private TimeFunctionality functionality = TimeFunctionality.Complex;

        /// <summary>
        /// Saves the start index of different fields. Order of values is directly mapped to the enum Field.
        /// </summary>
        private int[] fieldStart = new int[] { 0, 3, 6 };

        /// <summary>
        /// Saves the end index of different fields. Order of values is directly mapped to the enum Field.
        /// </summary>
        /// <remarks>Seconds field has two spaces after it, so there are two entries for seconds field [8, 9]
        /// </remarks>
        private int[] fieldEnd = new int[] { 2, 5, 8, 9 };

        /// <summary>
        /// Saves the Current Field of the control. 
        /// </summary>
        private Field currentField = Field.Hours;

        /// <summary>
        /// Saves the Current input mode of the control.
        /// </summary>
        private InputMode currentMode = InputMode.Simple;

        /// <summary>
        /// Collection of Time Period strings. Loaded from the resource file.
        /// </summary>
        private String[] timePeriodValues = new String[4];

        /// <summary>
        /// Toggle value to identify whether Time Type can be changed in the current editing mode of the control.
        /// </summary>        
        private bool allowTimeTypeChange;

        /// <summary>
        /// Saves the Current Value of the control.
        /// </summary>
        /// <remarks>
        /// If control is assigned an invalid value while editing, then this value is used to restore the recent value
        /// in the control.
        /// </remarks>
        private NhsTime currentValue;

        /// <summary>
        /// Saves the time type when control is being edited.
        /// </summary>
        /// <remarks>
        /// Saves the value of property EditingTimeType
        /// </remarks>
        private TimeType editingTimeType = TimeType.Exact;

        /// <summary>
        /// Toogle value to specify whether text changed occurred because of keypress or not.
        /// </summary>
        /// <remarks>If true it can be inferred that the text is changed because of key press and not cut/paste</remarks>
        private bool inlineEditing;      

        /// <summary>
        /// Flag to specify that no validation is required for the entered key.
        /// </summary>
        /// <remarks>
        /// TextChanged event of the txtInput validates the text entered. If this flag is set then
        /// no validation will be done and this flag will be unset again.
        /// </remarks>
        private bool acceptedKey;

        /// <summary>
        /// First use tooltip to be displayed.
        /// </summary>
        /// <remarks>
        /// Saves the value of the property FirstUseToolTip.
        /// </remarks>
        private String tooltipText = String.Empty;      

        /// <summary>
        /// Tracks recent selection start.
        /// </summary>
        private int recentSelectionStart;

        /// <summary>
        /// Tracks recent selection length.
        /// </summary>
        private int recentSelectionLength;

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
        /// Tracks the status of focus in the control.
        /// </summary>
        private bool hasFocus;
        #endregion

        #region Constructors      
        
        /// <summary>
        /// Constructs a TimeInputBox object.
        /// </summary>
        public TimeInputBox()
        {
            this.InitializeComponent();

            this.AccessibleName = TimeInputBoxControl.Resources.AccessibleName;
            this.AccessibleDescription = TimeInputBoxControl.Resources.AccessibleDescription;
            this.AccessibleRole = AccessibleRole.Text;

            this.LoadResources();                               
            this.txtInput.MouseWheel += new MouseEventHandler(this.TxtInput_MouseWheel);
            this.SizeControls();
            base.AutoValidate = AutoValidate.EnablePreventFocusChange;
            this.SetToolTip();

            // Design guide states that if control doesn't have focus then up/down buttons should
            // act on least significant field.
            this.txtInput.SelectionStart = this.DisplaySeconds ? this.GetFieldStart(Field.Seconds) : this.GetFieldStart(Field.Minutes);
            this.txtInput.SelectionLength = 2;            
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
                if (this.displaySeconds != value)
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
        /// Defaults to false. AM refers to times from 00:00 to 11:59:59.  PM refers to times from 12:00 to 23:59:59
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
        /// Specifies the functionality exposed by the TimeInputBox as "Simple" or "Complex".
        /// </summary>
        /// <remarks>
        /// Defaults to "Complex" so the 
        /// TimeInputBox�s complete functionality is exposed. If this is set to "Simple", only 
        /// a simple time can be entered such as TimeInputBox.Time.TimeValue. If the functionality is set to "Simple", the TimeInputBox allows other 
        /// values, such as <see cref="P:NhsCui.Toolkit.Web.TimeInputBox.AllowApproximate">AllowApproximate</see> and
        /// <see cref="P:NhsCui.Toolkit.Web.TimeInputBox.TimePeriod">TimePeriod</see>, to be got and set; the control does not, however, respond to 
        /// these values. In addition, attempting 
        /// to set the Value.TimeType or <see cref="P:NhsCui.Toolkit.Web.TimeInputBox.TimeType">TimeType</see> to any value other than TimeType.Exact 
        /// throws an argument exception.
        /// </remarks>
        [Category("Behavior")]
        [Description("Specifies the functionality exposed by the TimeInputBox (defaults to Complex). Setting Functionality to Simple sets Value.TimeType to Exact.")]
        [DefaultValue(0)]
        public TimeFunctionality Functionality
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
                    if (this.functionality == TimeFunctionality.Simple && this.TimeType == TimeType.NullIndex)
                    {
                        this.TimeType = TimeType.Exact;
                    } 
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
                    this.ResetFields();
                    this.NotifyPropertyChanged("Value"); 
                }

                this.RefreshDisplayText();
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
                    this.EditingTimeType = value;                    
                    this.chkApprox.Checked = value == TimeType.Approximate;                    
                    this.NotifyPropertyChanged("Value"); 
                }

                this.RefreshDisplayText();
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
        /// Gets the text displayed in the 'first use' ToolTip.
        /// </summary>
        /// <remarks>
        /// Decorated with the LocalizableAttribute and set to true. Defaults to �Enter a time 
        /// (e.g. '1624' or '16:24') or type a time period (e.g. 'afternoon', 'evening'). 
        /// Alternatively use the arrow keys or spinner to change the hours and minutes."
        /// </remarks>
        [Category("Behavior")]
        [Description("The text that is displayed in the tooltip.")]
        [ResourceDefaultValue(typeof(TimeInputBoxControl.Resources), "FirstUseToolTipSimple")]    
        public string TooltipText
        {
            get
            {        
                if (this.tooltipText.Trim().Length > 0)
                {
                    return this.tooltipText; 
                }                                          
                    
                return Resources.FirstUseToolTipSimple;        
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
        /// The foreground color of the component.
        /// </summary>
        [AmbientValue(typeof(Color), "Empty")]
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
                }
            }
        }

        /// <summary>
        /// The background color of the component.
        /// </summary>
        [AmbientValue(typeof(Color), "Empty")]
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
                }
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
            get { return this.valid; }
        }

        /// <summary>
        /// Gets or sets the validation manager of the control.
        /// </summary>
        [DefaultValue(null)]
        [Category("Behavior")]
        [Description("The validation manager for the control.")]
        public IValidationManager ValidationManager
        {
            get { return this.validationManager; }
            set { this.validationManager = value; }
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
            get { return this.cancelOnError; }
            set { this.cancelOnError = value; }
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

        #region Private Properties

        /// <summary>
        /// Tracks the Time Type when the end user is editing the text in txtInput control.
        /// </summary>
        private TimeType EditingTimeType
        {
            get
            {
                return this.editingTimeType;
            }

            set
            {
                if (this.editingTimeType != value)
                {
                    this.editingTimeType = value;
                    this.HandleApproxEnabling(); 
                }
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

        #region Static Methods

        /// <summary>
        /// Retrieves the resource string for the specified key.
        /// </summary>
        /// <param name="key"> Key value for which resource string will be returned.</param>
        /// <returns> Resource value for the specified key</returns>
        private static String GetResourceString(String key)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("NhsCui.Toolkit.WinForms.Common.NhsTimeResources", typeof(TimeInputBox).Assembly);
            return rm.GetString(key, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns the string for AM depending upon the current culture.
        /// </summary>
        /// <returns>The string for AM depending upon the current culture.</returns>
        private static String GetCultureSpecificAmString()
        {
            GlobalizationService gs = new GlobalizationService();            
            DateTime sampleDate = new DateTime(2007, 12, 12, 4, 12, 12);
            return (sampleDate.ToString(gs.ShortTimePatternAMPM.Substring(gs.ShortTimePatternAMPM.Length - 3, 2), CultureInfo.CurrentCulture).ToLower(CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Returns the string for PM depending upon the current culture.
        /// </summary>
        /// <returns>The string for PM depending upon the current culture.</returns>
        private static String GetCultureSpecificPmString()
        {
            GlobalizationService gs = new GlobalizationService();
            DateTime sampleDate = new DateTime(2007, 12, 12, 14, 12, 12);
            return (sampleDate.ToString(gs.ShortTimePatternAMPM.Substring(gs.ShortTimePatternAMPM.Length - 3, 2), CultureInfo.CurrentCulture).ToLower(CultureInfo.CurrentCulture));
        }

        /// <summary>
        /// Returns the time Separator being used.
        /// </summary>
        /// <returns>The time Separator.</returns>
        private static String GetTimeSeparator()
        {
            GlobalizationService gs = new GlobalizationService();
            return gs.ShortTimePattern.Substring(2, 1);            
        }
        #endregion

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
        }     

        /// <summary>
        /// Checks if the current field is the last field or not.
        /// </summary>
        /// <returns> True if the current field is the last field, else False.</returns>
        private bool IsLastField()
        {
            if (this.currentField == Field.AmPM || 
                (this.currentField == Field.Minutes && !this.DisplaySeconds && !this.IsAmPmDisplayed()) ||
                (this.currentField == Field.Seconds && !this.IsAmPmDisplayed()))
            {
                this.txtInput.SelectionLength = 0;                
                this.txtInput.SelectionStart = this.txtInput.TextLength;
                return true;                
            }

            return false; 
        }

        /// <summary>
        /// This function handles the inline editing in the textbox, when the input mode is exact or approx.
        /// </summary>
        /// <param name="e">Key Press Event Argument</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.MSInternal", "CA908:UseApprovedGenericsForPrecompiledAssemblies", Justification = "Generics aren't being used")]
        private void HandleExactTimeInput(KeyPressEventArgs e)
        {
            int selectionStart = this.txtInput.SelectionStart;
            if (Char.IsDigit(e.KeyChar) && (this.currentField == Field.Hours || this.currentField == Field.Minutes ||
                this.currentField == Field.Seconds))
            {
                // cursor is at the end of a field or at one place behind the start of AM PM Field.
                if (System.Array.IndexOf(this.fieldEnd, selectionStart) >= 0 || 
                    (this.DisplaySeconds == false && selectionStart == this.GetFieldStart(Field.AmPM) - 1))
                {
                    if (this.currentField == Field.Hours || (this.currentField == Field.Minutes && this.DisplaySeconds == true))
                    {                       
                            this.txtInput.SelectionStart += 1;
                            selectionStart += 1;
                    }
                    
                    // Dealing with the last field 
                    else 
                    {
                        // AM PM Displayed so return.
                        if (this.IsAmPmDisplayed())
                        {
                            return;                                                   
                        }

                    this.txtInput.SelectionStart -= 1;
                    selectionStart -= 1;
                    }                                        
                }          

                this.txtInput.SelectionLength = 1;

                // this line increments the selection start by 1 as well so we store selection start
                // in a variable.
                this.txtInput.SelectedText = e.KeyChar.ToString();

                // selection start is the start of some field. 
                if (System.Array.IndexOf(this.fieldStart, selectionStart) >= 0)
                {
                    // no need to explicitely increment selection start it is done implicitely when
                    // we assign a character to textbox.
                    this.txtInput.SelectionLength = 1;
                }
                else
                {
                    if (this.currentField == Field.Hours)
                    {
                        this.HandleAmPmDisplay();
                    }

                    if (!this.IsLastField())
                    {
                        this.SelectNextField();  
                    } 
                }
            }

            if (this.currentField == Field.AmPM && this.txtInput.SelectionStart == this.GetFieldStart(Field.AmPM))
            {
                this.HandleKeyPressAmPMField(e);
            }

            // case to handle free form input
            if (e.KeyChar.ToString() == TimeInputBox.GetTimeSeparator())
            {
                this.HandleSeparatorKey(selectionStart);
            }
        }

        /// <summary>
        /// Handles the processing of seperator key in the txtInput
        /// </summary>
        /// <param name="selectionStart">Current Selection Start</param>
        private void HandleSeparatorKey(int selectionStart)
        {
            // selection start is the start of some field. 
            if (selectionStart == this.GetFieldStart(Field.Hours) + 1 || selectionStart == this.GetFieldStart(Field.Minutes) + 1
                || (selectionStart == this.GetFieldStart(Field.Seconds) + 1 && this.DisplaySeconds))
            {
                String remainingPart = this.txtInput.Text.Substring(selectionStart - 1, 1);
                remainingPart = "0" + remainingPart;
                this.txtInput.SelectionStart -= 1;
                this.txtInput.SelectionLength = 2;
                this.txtInput.SelectedText = remainingPart;
                if (this.currentField == Field.Hours)
                {
                    this.HandleAmPmDisplay();
                }

                if (!this.IsLastField())
                {
                    this.SelectNextField();
                }
            }
            else if (selectionStart == this.GetFieldStart(Field.Hours) + 2 || selectionStart == this.GetFieldStart(Field.Minutes) + 2
                || (selectionStart == this.GetFieldStart(Field.Seconds) + 2 && this.DisplaySeconds))
            {
                if (!this.IsLastField())
                {
                    this.SelectNextField();
                }
            }
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
        /// Validates the format of string in txtInput against the format for Exact or approximate time type.
        /// </summary>
        /// <remarks>
        /// Cut/Paste can change the pattern of input string, however, TimeType remains same. This
        /// function is called to validate the string format in txtInput for specified TimeType. 
        /// </remarks>
        /// <returns>True if pattern is correct, else false.</returns>        
        private bool IsCorrectFormatForExactTime()
        {
            String basicPattern = @"^\d{2}" + TimeInputBox.GetTimeSeparator() + @"\d{2}";
            if (this.DisplaySeconds)
            {
                basicPattern += TimeInputBox.GetTimeSeparator() + @"\d{2}";
            }

            String pattern = basicPattern + @"(\s\(" + GetCultureSpecificAmString() + @"\))?$|" + basicPattern + @"(\s\(" + GetCultureSpecificPmString() + @"\))?$";

            Regex regEx = new Regex(pattern, RegexOptions.IgnoreCase);
            Match m = regEx.Match(this.txtInput.Text);

            if (m.Success)
            {
                int selectionStart = this.txtInput.SelectionStart;
                this.txtInput.SelectionStart = selectionStart;
                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Validates the format of string in txtInput.
        /// </summary>
        /// <remarks>
        /// Cut/Paste can change the pattern of input string, however, TimeType remains same. This
        /// function is called to validate the string format in txtInput for specified TimeType. 
        /// </remarks>
        /// <returns>True if pattern is correct, else false.</returns>
        private bool IsCorrectFormat()
        {            
            this.txtInput.Text = this.txtInput.Text.Trim();

            // Simple functionality
            if (this.Functionality == TimeFunctionality.Simple)
            {
                return this.IsCorrectFormatForExactTime();
            }

            // Complex functionality 
            else
            {
                int index;               

                index = this.GetNullIndexExact(this.txtInput.Text);
                if (index >= 0)
                {
                    this.NullIndex = index;
                    this.EditingTimeType = TimeType.NullIndex;
                    this.allowTimeTypeChange = false;
                    this.TimeType = TimeType.NullIndex;
                    this.currentMode = InputMode.Simple;
                    return true;
                }

                if (this.IsArithmeticModeFormat())
                {
                    this.currentMode = InputMode.Arithmetic;
                    return true;
                }

                if (this.IsCorrectFormatForExactTime())
                {
                    this.EditingTimeType = this.chkApprox.Checked ? TimeType.Approximate : TimeType.Exact;
                    return true;
                }

                return false;
            }          
        }

        /// <summary>
        /// Displays the appropriate null string in the control
        /// </summary>
        private void DisplayNullString()
        {
            if (this.IsNullEnabled())
            {
                if (this.NullIndex < 0 || this.NullIndex >= this.NullStrings.Length)
                {
                    this.NullIndex = 0;
                }

                if (this.TimeType == TimeType.NullIndex)
                {
                    this.NullIndex = (this.NullIndex + 1) % this.NullStrings.Length;
                }
                else
                {
                    this.TimeType = TimeType.NullIndex;
                }

                this.EditingTimeType = TimeType.NullIndex;
            
                // this is to ensure that once user is in NullIndex mode then he can only switch to other
                // mode when the control gets focus next time. This is to match the functionality in web
                // counterpart.
                this.allowTimeTypeChange = false;
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
        /// Loads the resource strings and sets the field values.
        /// </summary>
        private void LoadResources()
        {           
            this.chkApprox.Text = GetResourceString("Approximate");
            this.timePeriodValues[0] = GetResourceString("Morning");
            this.timePeriodValues[1] = GetResourceString("Afternoon");
            this.timePeriodValues[2] = GetResourceString("Evening");
            this.timePeriodValues[3] = GetResourceString("Night");                        
        }

        /// <summary>
        /// Toggles the display of AM/PM in the control
        /// </summary>
        private void ToggleAMPM()
        {
            string formatAM = GetCultureSpecificAmString();
            string formatPM = GetCultureSpecificPmString();

            // AM
            if (this.txtInput.Text.Substring(this.GetFieldStart(Field.AmPM), 2) == formatAM)
            {
                this.txtInput.Text = this.txtInput.Text.Substring(0, this.GetFieldStart(Field.AmPM)) + formatPM + ")";                
            }

            // PM
            else
            {
                this.txtInput.Text = this.txtInput.Text.Substring(0, this.GetFieldStart(Field.AmPM)) + formatAM + ")";                               
            }
        }
        
        /// <summary>
        /// Checks whether AM/PM field is displayed in the control. If AM/PM field is not present then insert the 
        /// field with the value AM
        /// </summary>
        private void CheckAndAddAM()
        {      
             if (this.txtInput.TextLength < this.GetFieldStart(Field.AmPM))
             {
                 this.txtInput.Text += " (" + GetCultureSpecificAmString() + ")";
             }         
        }

        /// <summary>
        /// Checks whether AM/PM field is displayed in the control. If AM/PM field is present then it is removed.
        /// </summary>
        private void CheckAndRemoveAM()
        {             
                if (this.txtInput.TextLength > this.GetFieldStart(Field.AmPM))
                {
                    this.txtInput.Text = this.txtInput.Text.Substring(0, this.GetFieldStart(Field.AmPM) - 2);
                }          
        }

        /// <summary>
        /// Decides whether AM/PM field should be displayed or not.
        /// </summary>
        private void HandleAmPmDisplay()
        {
            this.inlineEditing = true;
            if (!this.DisplayAMPM || this.editingTimeType == TimeType.NullIndex) 
            {
                this.inlineEditing = false;
                return;
            }

            String hour = this.txtInput.Text.Substring(this.GetFieldStart(Field.Hours), 2);
            int hourValue = Convert.ToInt32(hour, CultureInfo.CurrentCulture);
            if (hourValue > 12)
            {
                this.CheckAndRemoveAM();
            }
            else
            {
                this.CheckAndAddAM();
            }

            this.inlineEditing = false;
        }

        /// <summary>
        /// Implements selection of smallest field if the complete text is selected.
        /// </summary>
        private void CheckCompleteSelection()
        {
            // As per DG if complete text is selected then spin will act on smallest unit.
            if (this.txtInput.SelectedText.Length == this.txtInput.TextLength)
            {
                if (this.DisplaySeconds)
                {
                    this.currentField = Field.Seconds;
                }
                else
                {
                    this.currentField = Field.Minutes;
                }
            }
            else
            {
                this.SetCurrentField();
            }
        }
        
        /// <summary>
        /// Increments the specified field.
        /// </summary>
        /// <param name="start"> Start Index of the control. </param>
        /// <param name="hour">Specifies whether the field being incremented is hour.</param>
        private void IncrementField(int start, bool hour)
        {
            int fieldValue;
            fieldValue = Convert.ToInt32(this.txtInput.Text.Substring(start, 2), CultureInfo.CurrentCulture);
            fieldValue += 1;           
            String startString = this.txtInput.Text.Substring(0, start);
            String endString = this.txtInput.Text.Substring(start + 2);
            String middleString;
            bool toggle = false;
            bool add = false;
            if (hour)
            {
                if (this.display12Hour)
                {
                    if (fieldValue == 12)
                    { 
                        toggle = true; 
                    }
                    else
                    { 
                        add = true; 
                    }

                    middleString = (fieldValue > 12 || fieldValue < 1) ? "01" :
                        fieldValue.ToString(CultureInfo.CurrentCulture).Length < 2 ? "0" + fieldValue.ToString(CultureInfo.CurrentCulture) : fieldValue.ToString(CultureInfo.CurrentCulture);
                }
                else
                {
                    if (fieldValue < 12 || fieldValue > 23)
                    { 
                        add = true; 
                    }

                    middleString = (fieldValue > 23 || fieldValue < 0) ? "00" :
                        fieldValue.ToString(CultureInfo.CurrentCulture).Length < 2 ? "0" + fieldValue.ToString(CultureInfo.CurrentCulture) : fieldValue.ToString(CultureInfo.CurrentCulture);                    
                }
            }
            else
            {
                middleString = (fieldValue > 59 || fieldValue < 0) ? "00" :
                    fieldValue.ToString(CultureInfo.CurrentCulture).Length < 2 ? "0" + fieldValue.ToString(CultureInfo.CurrentCulture) : fieldValue.ToString(CultureInfo.CurrentCulture);
            }

            this.txtInput.Text = startString + middleString + endString;
            if (this.DisplayAMPM && hour)
            {
                if (toggle)
                {
                    this.ToggleAMPM();
                }
                else if (add)
                {
                    this.CheckAndAddAM();
                }
                else
                {
                    this.CheckAndRemoveAM();
                }
            }
        }

        /// <summary>
        /// Decrements the specified field.
        /// </summary>
        /// <param name="start"> Start Index of the control. </param>
        /// <param name="hour">Specifies whether the field being incremented is hour.</param>
        private void DecrementField(int start, bool hour)
        {
            int fieldValue;
            fieldValue = Convert.ToInt32(this.txtInput.Text.Substring(start, 2), CultureInfo.CurrentCulture);
            fieldValue -= 1;
            String startString = this.txtInput.Text.Substring(0, start);
            String endString = this.txtInput.Text.Substring(start + 2);
            String middleString;
            bool toggle = false;
            bool add = false;
            if (hour)
            {
                if (this.display12Hour)
                {
                    if (fieldValue == 11)
                    {
                        toggle = true;
                    }
                    else
                    { 
                        add = true; 
                    }

                    middleString = (fieldValue > 12) ? "01" : (fieldValue < 1) ? "12" :
                    fieldValue.ToString(CultureInfo.CurrentCulture).Length < 2 ? "0" + fieldValue.ToString(CultureInfo.CurrentCulture) : fieldValue.ToString(CultureInfo.CurrentCulture);
                }
                else
                {
                    if ((fieldValue < 12 && fieldValue >= 0) || fieldValue > 23)
                    { 
                        add = true; 
                    }

                    middleString = (fieldValue > 23) ? "00" : (fieldValue < 0) ? "23" :
                    fieldValue.ToString(CultureInfo.CurrentCulture).Length < 2 ? "0" + fieldValue.ToString(CultureInfo.CurrentCulture) : fieldValue.ToString(CultureInfo.CurrentCulture);
                }
            }
            else
            {
                middleString = (fieldValue > 59) ? "00" : (fieldValue < 0) ? "59" :
                    fieldValue.ToString(CultureInfo.CurrentCulture).Length < 2 ? "0" + fieldValue.ToString(CultureInfo.CurrentCulture) : fieldValue.ToString(CultureInfo.CurrentCulture);
            }

            this.txtInput.Text = startString + middleString + endString;
            if (this.DisplayAMPM && hour)
            {
                if (toggle)
                {
                    this.ToggleAMPM();
                }
                else if (add)
                {
                    this.CheckAndAddAM();
                }
                else
                {
                    this.CheckAndRemoveAM();
                }
            }
        }       

        /// <summary>
        /// Increases the current field.
        /// </summary>
        private void HandleIncrease()
        {
            this.inlineEditing = true;            
            this.CheckCompleteSelection();
            this.SelectCurrentField();
            if (this.editingTimeType == TimeType.NullIndex || this.invalidFormat ||
                this.currentMode == InputMode.Arithmetic || this.editingTimeType == TimeType.Null) 
            {
                this.inlineEditing = false;                
                return; 
            }            

            int currentSelectionStart = this.txtInput.SelectionStart;
            int currentSelectionLength = this.txtInput.SelectionLength;
            switch (this.currentField)
            {
                case Field.Null:
                    break;
                case Field.Hours:
                    this.IncrementField(this.GetFieldStart(Field.Hours), true);                                  
                    break;
                case Field.Minutes:
                    this.IncrementField(this.GetFieldStart(Field.Minutes), false);
                    break;
                case Field.Seconds:
                    this.IncrementField(this.GetFieldStart(Field.Seconds), false);
                    break;
                case Field.AmPM:
                    if (this.Display12Hour)
                    {
                        this.ToggleAMPM();                        
                    }

                    break;
                default:
                    break;
            }

            this.txtInput.SelectionStart = currentSelectionStart;
            this.txtInput.SelectionLength = currentSelectionLength;
            this.inlineEditing = false;                
        }

        /// <summary>
        /// Decreases the current field.
        /// </summary>
        private void HandleDecrease()
        {
            this.inlineEditing = true;            
            this.CheckCompleteSelection();
            this.SelectCurrentField(); 
            if (this.editingTimeType == TimeType.NullIndex || this.invalidFormat
                || this.currentMode == InputMode.Arithmetic || this.editingTimeType == TimeType.Null) 
            {
                this.inlineEditing = false;                
                return; 
            }
          
            int currentSelectionStart = this.txtInput.SelectionStart;
            int currentSelectionLength = this.txtInput.SelectionLength;

            switch (this.currentField)
            {
                case Field.Null:
                    break;
                case Field.Hours:
                    this.DecrementField(this.GetFieldStart(Field.Hours), true);
                    break;
                case Field.Minutes:
                    this.DecrementField(this.GetFieldStart(Field.Minutes), false);
                    break;
                case Field.Seconds:
                    this.DecrementField(this.GetFieldStart(Field.Seconds), false);
                    break;
                case Field.AmPM:
                    if (this.Display12Hour)
                    {
                        this.ToggleAMPM();
                    }   
        
                    break;
                default:
                    break;
            }
            
            this.txtInput.SelectionStart = currentSelectionStart;
            this.txtInput.SelectionLength = currentSelectionLength;
            this.inlineEditing = false;                
        }

        /// <summary>
        /// Position the check box and other controls.
        /// </summary>
        private void PositionCheckBox()
        {
            this.inlineEditing = true;                        
            if (this.DesignMode || this.Functionality == TimeFunctionality.Simple
                || (!this.AllowApproximate))
            {
                if (this.chkApprox.Visible)
                {
                    // before hiding the check box, make sure it is unchecked.
                    this.chkApprox.Checked = false;
                    this.chkApprox.Visible = false;
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
        /// Checks if the input text contains pm string or not.
        /// </summary>
        /// <returns> True if textbox contains pm, else false.</returns>
        private bool ISPMContained()
        {
            if (this.txtInput.Text.ToLower(CultureInfo.CurrentCulture).Contains(GetCultureSpecificPmString()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Refresh the text displayed in the text box.
        /// </summary>
        /// <returns>
        /// Status of update.
        /// </returns>
        private bool ValidateAndUpdateValue()
        {
            NhsTime result;
            bool status = true;
            bool containedPM = this.ISPMContained();
            this.SetValid();
            this.recentSelectionStart = this.txtInput.SelectionStart;
            this.recentSelectionLength = this.txtInput.SelectionLength;

            // for null index nothing to be done as index is updated while setting.
            if (this.EditingTimeType != TimeType.NullIndex)
            {
                if (NhsTime.IsAddValid(this.txtInput.Text))
                {
                    this.Value.Add(this.txtInput.Text);
                    this.RefreshDisplayText();
                    this.ResetFields();
                    return status;
                }
                else if (NhsTime.TryParseExact(this.txtInput.Text, out result, CultureInfo.CurrentCulture))
                {
                    // Handles an exception case, TryParse always returns the text in am no matter
                    // input contains am or pm.
                    if (containedPM && result.TimeValue.Hour < 12)
                    {
                        result.TimeValue = result.TimeValue.AddHours(12);
                    }

                    this.Value = result;
                    this.invalidFormat = false;
                }
                else
                {
                    this.SetInvalid(ref status);
                } 
            }

            this.allowTimeTypeChange = true;
            this.currentValue = this.Value;
            this.EditingTimeType = this.TimeType;
            this.currentMode = InputMode.Simple;
            this.txtInput.SelectionStart = this.txtInput.TextLength;
            this.txtInput.SelectionLength = 0;
            return status;
        }
        
        /// <summary>
        /// Refresh the display text of the control.
        /// </summary>
        private void RefreshDisplayText()
        {
            this.inlineEditing = true;            

            if (this.TimeType != TimeType.Null)
            {
                this.SetValid();
            }

            this.txtInput.Text = this.GetDisplayString();           
            this.inlineEditing = false;
        }

        /// <summary>
        /// Returns the display string.
        /// </summary>
        /// <returns>String.</returns>
        private string GetDisplayString()
        {
            NhsTime value = this.Value;
            string[] nullValues = this.NullStrings;

            if (value.TimeType != TimeType.NullIndex || nullValues == null ||
                        value.NullIndex < 0 || value.NullIndex >= nullValues.Length)
            {
                return value.ToString(false, CultureInfo.CurrentCulture, this.DisplaySeconds, this.Display12Hour, this.DisplayAMPM);
            }
            else
            {
                return nullValues[value.NullIndex];
            }
        }

        /// <summary>
        /// Refresh the complete display of the control.
        /// </summary>
        /// <remarks>It includes refreshing the positioning, display text, and resource strings updation.</remarks>
        private void RefreshDisplay()
        {
            this.RefreshDisplayText();
            this.PositionCheckBox();
            if (this.DesignMode)
            {
                this.btnUp.Visible = false;
                this.btnDown.Visible = false;
            }
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
            
            this.txtInput.Width = this.Width;                      
            this.txtInput.Height = this.Height;
            this.btnUp.Height = (this.txtInput.Height / 2) - SpinPadding;
            this.btnDown.Height = this.txtInput.Height - this.btnUp.Height - (2 * SpinPadding);
            this.btnUp.Top = SpinPadding;
            this.btnDown.Top = this.btnUp.Top + this.btnUp.Height;
            this.btnUp.Left = this.txtInput.Left + this.txtInput.Width - this.btnUp.Width - SpinPadding;
            this.btnDown.Left = this.txtInput.Left + this.txtInput.Width - this.btnDown.Width - SpinPadding;
            this.chkApprox.Left = this.txtInput.Left + this.txtInput.Width + PadWidth;
            this.chkApprox.Top = 2;
            this.chkApprox.Visible = false;
            this.PositionCheckBox();
        }

        /// <summary>
        /// Selects the specified filed
        /// </summary>
        /// <param name="field">Field to be selected.</param>
        private void SelectField(Field field)
        {
            if (!this.Enabled || 
               this.editingTimeType == TimeType.NullIndex || this.invalidFormat || this.editingTimeType == TimeType.Null)
            {
                return;
            }

            if (field == Field.Null)
            { 
                field = Field.Hours; 
            }

            this.txtInput.SelectionStart = this.GetFieldStart(field);
            this.txtInput.SelectionLength = 2;
        }

        /// <summary>
        /// Checks whether AM/PM field is present in the control.
        /// </summary>
        /// <returns>True if AM/PM field is present else False</returns>
        private bool IsAmPmDisplayed()
        {
            return (this.txtInput.Text.Length > this.GetFieldStart(Field.AmPM));            
        }
        
        /// <summary>
        /// Selects the next field (from the current field) in the control.
        /// </summary>
        private void SelectNextField()
        {
            bool reallyDisplayAMPM = this.IsAmPmDisplayed();
            if (!this.Enabled ||
               this.editingTimeType == TimeType.NullIndex || this.invalidFormat || this.editingTimeType == TimeType.Null) 
            { 
                return; 
            }

            if (this.currentField == Field.Minutes)
            {
                this.currentField = this.DisplaySeconds ? Field.Seconds : reallyDisplayAMPM ? Field.AmPM : Field.Hours;
            }
            else if (this.currentField == Field.Seconds)
            {
                this.currentField = reallyDisplayAMPM ? Field.AmPM : Field.Hours;
            }
            else
            {
                this.currentField = (Field)(((int)this.currentField + 1) % 4);
            }

            this.SelectCurrentField();
        }

        /// <summary>
        /// Selects the previous field (from the current field) in the control.
        /// </summary>
        private void SelectPreviousField()
        {
            bool reallyDisplayAMPM = this.IsAmPmDisplayed();
            if (!this.Enabled ||
               this.editingTimeType == TimeType.NullIndex || this.invalidFormat || this.editingTimeType == TimeType.Null) 
            {
                return;
            }

            if (this.currentField == Field.Hours)
            {
                this.currentField = reallyDisplayAMPM ? Field.AmPM : this.DisplaySeconds ? Field.Seconds : Field.Minutes;
            }
            else if (this.currentField == Field.AmPM)
            {
                this.currentField = this.DisplaySeconds ? Field.Seconds : Field.Minutes;
            }
            else
            {
                this.currentField = (Field)((int)this.currentField - 1);
            }

            this.SelectCurrentField();
        }

        /// <summary>
        /// Selects the current field in the control.
        /// </summary>
        private void SelectCurrentField()
        {
            if (!this.Enabled || 
               this.editingTimeType == TimeType.NullIndex || this.invalidFormat || this.editingTimeType == TimeType.Null) 
            { 
                return; 
            }

            if (this.currentField == Field.Null)
            {
                this.currentField = Field.Hours;
            }

            this.txtInput.SelectionStart = this.GetFieldStart(this.currentField);
            this.txtInput.SelectionLength = 2;
        }

        /// <summary>
        /// Retrieves the start index of the specified field.
        /// </summary>
        /// <param name="field">Field to search.</param>
        /// <returns>The start of the specified field</returns>
        private int GetFieldStart(Field field)
        {
            if (field != Field.AmPM)
            {
                return (this.fieldStart[(int)field]);
            }

            return (this.DisplaySeconds ? this.fieldStart[(int)Field.Seconds] + DistOfAMPM : this.fieldStart[(int)Field.Minutes] + DistOfAMPM);
        }

        /// <summary>
        /// Enables/Disables the chkapprox child control.
        /// </summary>
        private void HandleApproxEnabling()
        {
            if (this.editingTimeType == TimeType.Exact || this.editingTimeType == TimeType.Approximate)
            {
                this.chkApprox.Enabled = true;
            }
            else
            {
                this.chkApprox.Checked = false;
                this.chkApprox.Enabled = false;
            }
        }

        /// <summary>
        /// Checks if null string input is enabled in the control.
        /// </summary>
        /// <returns>True if null strings can be entered in the control, else false.</returns>
        private bool IsNullEnabled()
        {
            return (this.Functionality == TimeFunctionality.Complex && this.NullStrings.Length > 0 && this.currentMode == InputMode.Simple);
        }

        /// <summary>
        /// Handles the key press in txt input when allow time change is true.
        /// </summary>
        /// <param name="e">Key Press Event Argument</param>
        /// <returns>True if the event is handled, else false.</returns>
        private bool HandleAllowTimeChange(KeyPressEventArgs e)
        {
            int temp;
            int selectionStart = this.txtInput.SelectionStart;           
            temp = this.GetNullIndex(this.txtInput.Text.Substring(0, this.txtInput.SelectionStart) + e.KeyChar.ToString());
            if (temp >= 0 && this.IsNullEnabled())
            {
                this.TimeType = TimeType.NullIndex;
                this.NullIndex = temp;
                this.EditingTimeType = TimeType.NullIndex;
                this.allowTimeTypeChange = false;
                if (this.GetNullIndex(this.txtInput.Text.Substring(0, this.txtInput.SelectionStart) + e.KeyChar.ToString(), temp + 1) >= 0)
                {
                    this.SelectToEnd(selectionStart + 1);
                }
                else
                {
                    this.txtInput.SelectionStart = this.txtInput.TextLength;
                }
                                
                e.Handled = true;
                return true;
            }

            return false;            
        }               

        /// <summary>
        /// Updates the field currentValue.
        /// </summary>
        /// <remarks>Called when user clicks in the control.</remarks>
        private void SetCurrentField()
        {
            if (!this.hasFocus)
            {
                this.txtInput.SelectionStart = this.DisplaySeconds ? this.GetFieldStart(Field.Seconds) : this.GetFieldStart(Field.Minutes);
                this.txtInput.SelectionLength = 2;
            }

            if (this.txtInput.SelectionStart < this.GetFieldStart(Field.Minutes))
            {
                this.currentField = Field.Hours;
            }
            else if ((this.txtInput.SelectionStart < this.GetFieldStart(Field.Seconds) && this.DisplaySeconds) ||
                (this.txtInput.SelectionStart < this.GetFieldStart(Field.AmPM) - 1 && (!this.DisplaySeconds)))
            {
                this.currentField = Field.Minutes;
            }
            else if ((this.txtInput.SelectionStart < this.GetFieldStart(Field.AmPM) - 1))
            {
                this.currentField = Field.Seconds;
            }
            else
            {
                this.currentField = Field.AmPM;
            }
        }      

        /// <summary>
        /// Retrieves the index from the collection Null Strings, which starts with the specified string.
        /// </summary>
        /// <param name="newString">String to be searched in the Null Strings collection.</param>
        /// <returns>Index of the collection where match is found, if not found then -1 is returned. </returns>
        private int GetNullIndex(String newString)
        {
            return this.GetNullIndex(newString, 0);
        }

        /// <summary>
        /// Retrieves the index from the collection Null Strings, which starts with the specified string.
        /// </summary>
        /// <param name="newString">String to be searched in the Null Strings collection.</param>
        /// <param name="start"> Start index</param>
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
        /// Overloaded. Sets the display text in the control.
        /// </summary>
        /// <param name="hours">Hours to be displayed.</param>
        /// <param name="minutes">Minutes to be displayed.</param>
        /// <param name="seconds">Seconds to be displayed.</param>
        private void SetDisplayText(String hours, string minutes, string seconds)
        {
            int hr = Convert.ToInt32(hours, CultureInfo.CurrentCulture);
            String basic = hours + GetTimeSeparator() + minutes;
            if (this.DisplaySeconds)
            {
                basic  = basic + GetTimeSeparator() + seconds;
            }

            if (hr <= 12 && this.DisplayAMPM)
            {
                basic = basic + " (" + GetCultureSpecificAmString() + ")";
            }

            this.txtInput.Text = basic;
            this.txtInput.SelectionStart = 1;
            this.txtInput.SelectionLength = 1;
        }

        /// <summary>
        /// Overloaded. Sets the display text in the control.
        /// </summary>
        /// <param name="hours">Hours to be displayed.</param>
        /// <param name="minutes">Minutes to be displayed.</param>
        private void SetDisplayText(String hours, string minutes)
        {
            int hr = Convert.ToInt32(hours, CultureInfo.CurrentCulture);

            // don't display am/pm even if it is specified.
            if (hr >= 12)
            {
                this.txtInput.Text = hours + this.txtInput.Text.Substring(this.GetFieldStart(Field.Hours) + 2, 1) +
                    minutes + this.txtInput.Text.Substring(this.GetFieldStart(Field.Minutes) + 2);
                if (this.txtInput.TextLength > this.GetFieldStart(Field.AmPM))
                {
                    this.txtInput.Text = this.txtInput.Text.Substring(0, this.GetFieldStart(Field.AmPM));
                }
            }

            // display am/pm if it is specified.
            else
            {
                this.txtInput.Text = hours + this.txtInput.Text.Substring(this.GetFieldStart(Field.Hours) + 2, 1) +
                  minutes + this.txtInput.Text.Substring(this.GetFieldStart(Field.Minutes) + 2);
                if (this.txtInput.TextLength < this.GetFieldStart(Field.AmPM))
                {
                    this.txtInput.Text += " (" + GetCultureSpecificAmString() + ")";
                }
            }
        }

        /// <summary>
        /// Overloaded. Sets the display text in the control.
        /// </summary>
        /// <param name="hours">Hours to be displayed.</param>        
        private void SetDisplayText(String hours)
        {
            int hr = Convert.ToInt32(hours, CultureInfo.CurrentCulture);

            // don't display am/pm even if it is specified.
            if (hr >= 12)
            {
                this.txtInput.Text = hours + this.txtInput.Text.Substring(this.GetFieldStart(Field.Hours) + 2);
                if (this.txtInput.TextLength > this.GetFieldStart(Field.AmPM))
                {
                    this.txtInput.Text = this.txtInput.Text.Substring(0, this.GetFieldStart(Field.AmPM));
                }
            }

            // display am/pm if it is specified.
            else
            {
                this.txtInput.Text = hours + this.txtInput.Text.Substring(this.GetFieldStart(Field.Hours) + 2);
                if (this.txtInput.TextLength < this.GetFieldStart(Field.AmPM))
                {
                    this.txtInput.Text += " (" + GetCultureSpecificAmString() + ")";
                }
            }
        }

        /// <summary>
        /// Handles the keypress when the focus in on AM/PM Field.
        /// </summary>
        /// <param name="e">Keypress event argument</param>
        private void HandleKeyPressAmPMField(KeyPressEventArgs e)
        {
            if (!this.Display12Hour)
            {                
                return;
            }

            string formatAM = GetCultureSpecificAmString();
            string formatPM = GetCultureSpecificPmString();

            // AM
            if (this.txtInput.Text.Substring(this.txtInput.SelectionStart, 2) == formatAM && 
                e.KeyChar.ToString().Equals(formatPM.Substring(0, 1), StringComparison.CurrentCultureIgnoreCase))
            {
                this.txtInput.SelectionLength = 2;
                this.txtInput.SelectedText = formatPM;
                if (!this.IsLastField())
                {
                    this.SelectNextField();
                }
            }

            // PM
            else if (e.KeyChar.ToString().Equals(formatAM.Substring(0, 1), StringComparison.CurrentCultureIgnoreCase))
            {
                this.txtInput.SelectionLength = 2;
                this.txtInput.SelectedText = formatAM;
                if (!this.IsLastField())
                {
                    this.SelectNextField();
                }
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
            if (NhsTime.IsAddValid(resultString))
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
            if (this.editingTimeType == TimeType.NullIndex || this.invalidFormat || this.Functionality == TimeFunctionality.Simple)
            {
                return false;
            }

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
                if (NhsTime.IsAddValid(currentInputTillSelection) || (e.KeyChar == '+' || e.KeyChar == '-'))
                {
                    this.currentMode = InputMode.Arithmetic;
                    this.txtInput.Text = (e.KeyChar == '+' || e.KeyChar == '-') ? e.KeyChar.ToString() : currentInputTillSelection;
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

            if (NhsTime.IsAddValid(resultString) && resultString.Length <= maxLength)
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
            this.Value.TimeType = TimeType.Null;
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
            this.invalidText = String.Empty;
            if (this.ValidationManager != null)
            {
                this.ValidationManager.ClearError(this);
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
            if (e.Delta < 0)
            {
                this.HandleDecrease();
            }
            else
            {
                this.HandleIncrease();
            }
        }

        /// <summary>
        /// Handles the mouse click event of the child textbox
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.SetCurrentField();
        }

        /// <summary>
        /// Handles the scroll event of the child scroll bar.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void ScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            // It is important to refresh the current field when a key is pressed.
            // because end user might have changed the current selection start by
            // selecting some text.
            this.SetCurrentField();
            this.SelectCurrentField();

            if (e.Type == ScrollEventType.SmallIncrement)
            {
                this.HandleDecrease();
            }
            else if (e.Type == ScrollEventType.SmallDecrement)
            {
                this.HandleIncrease();
            }
        }

        /// <summary>
        /// Handles the key down event of the child textbox.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {                               
            this.inlineEditing = true;

            // It is important to refresh the current field when a key is pressed.
            // because end user might have changed the current selection start by
            // selecting some text.
            if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
            {
                this.SetCurrentField();    
            }
            else
            {
                this.CheckCompleteSelection();
            }
            
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                this.ExitKeyDownEvent(e, true);             
                return;
            }

            if (!this.Enabled || this.editingTimeType == TimeType.NullIndex || this.currentMode == InputMode.Arithmetic || this.invalidFormat || this.editingTimeType == TimeType.Null)
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    e.Handled = true;
                }

                this.ExitKeyDownEvent(e, false);
                return;
            }           

            switch (e.KeyCode)
            {
                case Keys.Up:
                    this.SelectCurrentField();
                    this.HandleIncrease();
                    e.Handled = true;
                    break;
                case Keys.Down:
                    this.SelectCurrentField();
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
                case Keys.Back:
                    e.Handled = true;
                    break;
            }

            this.ExitKeyDownEvent(e, false);
        }

        /// <summary>        
        /// Handles the Load event of the control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void UserControl1_Load(object sender, EventArgs e)
        {
            this.RefreshDisplay();
        }

        /// <summary>
        /// Handles the Resize event of the control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void UserControl1_Resize(object sender, EventArgs e)
        {
            this.SizeControls();
        }

        /// <summary>
        /// Handles the enter event of the child textbox
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TextBox1_Enter(object sender, EventArgs e)
        {
            this.validated = false;
            this.hasFocus = true;
            if (this.focusAfterSpin)
            {
                this.focusAfterSpin = false;
                return;
            }

            this.ResetFields();

            if (this.EditingTimeType == TimeType.Null && this.IsCorrectFormatForExactTime())
            {
                this.EditingTimeType = TimeType.Exact;
            }

            if (this.editingTimeType == TimeType.Exact || this.editingTimeType == TimeType.Approximate)
            {
                this.SetCurrentField();                
            }
            
            this.txtInput.SelectionStart = 0;
            this.txtInput.SelectionLength = this.txtInput.TextLength;            
        }

        /// <summary>
        /// Resets the control's state fields to their default value.
        /// </summary>
        private void ResetFields()
        {
            this.currentField = Field.Hours;
            this.currentMode = InputMode.Simple;
            this.allowTimeTypeChange = true;
            this.currentValue = this.Value;
            this.EditingTimeType = this.TimeType;
            this.invalidFormat = false;
            this.LoadResources();
            this.SelectCurrentField();
        }

        /// <summary>
        /// Handles the Key Press event of the child textbox
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.MSInternal", "CA908:UseApprovedGenericsForPrecompiledAssemblies", Justification = "Generics aren't being used")]        
        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
       {           
           this.inlineEditing = true;
           if (Array.IndexOf(this.allowedSpecialKeys, (int)e.KeyChar) >= 0)
           {
               if ((int)e.KeyChar == 22 && this.txtInput.SelectedText.Length > 0)
               {
                   this.selectedPaste = true;
               }

               this.ExitKeyPressEvent(e, false);
               return;
           }

           if (e.KeyChar == '\r' || e.KeyChar == '\n')
           {
               // Implement lost focus on enter key
               this.TopLevelControl.SelectNextControl(this, true, true, true, true);
               
               // this.ValidateAndUpdateValue();

               this.ExitKeyPressEvent(e, true);
               return;
           }
            
           // If editing time type is null let the user enter values.
           if ((!this.Enabled || this.invalidFormat) && this.editingTimeType != TimeType.Null)
           {
               this.ExitKeyPressEvent(e, true);
               return;
           }

            // Backspace key
           if ((int)e.KeyChar == (int)Keys.Back)
           {
               if (this.currentMode == InputMode.Simple)
               {
                   this.ExitKeyPressEvent(e, true);                              
               }

               // Arithmetic mode
               else
               {
                   this.ExitKeyPressEvent(e, false);
                   this.acceptedKey = true;
               }

               return;
           }

            // Case to handle Null Index Values
            if (e.KeyChar == ' ')
            {
                this.DisplayNullString();
                this.ExitKeyPressEvent(e, true);
                return;
            }

            // case to check whether current input belongs to arithmetic mode
            if (this.IsArithmeticMode(e))
            {
                this.ExitKeyPressEvent(e, false);
                return;
            }

            // case to check whether the entered key leds to some special type.
            // applies to all time type provided time type change is allowed.
            if (this.Functionality == TimeFunctionality.Complex)
            {
                if (this.HandleAllowTimeChange(e))
                {
                    this.ExitKeyPressEvent(e, false);
                }
            }

            // Current editing format is number.            
            if (this.editingTimeType == TimeType.Exact ||
                this.editingTimeType == TimeType.Approximate)
            {
                this.HandleExactTimeInput(e);
                this.ExitKeyPressEvent(e, true);
                return;
            }

            // editing time type is null or null index.
            else 
            {                              
                // we can change the time type. If a digit is pressed accept it and change editingTimeType
                if (this.allowTimeTypeChange)
                {                  
                    // Case for normal digit.
                   if (char.IsDigit(e.KeyChar))
                        {
                            this.SetDisplayText(e.KeyChar.ToString() + "0", "00", "00");
                            this.EditingTimeType = this.chkApprox.Checked ? TimeType.Approximate : TimeType.Exact;
                            this.invalidFormat = false;
                            this.ExitKeyPressEvent(e, true);
                            return;
                        }                    
                }

                // editing time type is null index. we can't change it just check if entered character 
                // matches some null string.                
                else 
                {
                    int tmp = this.GetNullIndex(this.txtInput.Text.Substring(0, this.txtInput.SelectionStart) + e.KeyChar.ToString());                    
                    if (tmp >= 0)
                    {
                        this.NullIndex = tmp;
                    }
                }

                this.ExitKeyPressEvent(e, true);           
            }
        }             

        /// <summary>
        /// Handles the text changed event of the child textbox
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.selectedPaste)
            {
                this.selectedPaste = false;
                return;
            }

            if (this.inlineEditing)
            {
                return;
            }

            if (this.txtInput.TextLength == 0)
            {
                this.currentMode = InputMode.Simple;
                this.invalidFormat = false;
                this.editingTimeType = TimeType.Null;
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
                if (this.editingTimeType == TimeType.Exact || this.editingTimeType == TimeType.Approximate)
                {
                    this.HandleAmPmDisplay();                    
                }                
            }
        }

        /// <summary>
        /// Handles the keydown event of the control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TimeInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            this.SetCurrentField();            
        }

        /// <summary>
        /// Handles the Mouse click event of the control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TimeInputBox_MouseClick(object sender, MouseEventArgs e)
        {
            this.SetCurrentField();
        }     

        /// <summary>
        /// Handles the leave event of the Time Input Box.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void TimeInputBox_Leave(object sender, EventArgs e)
        {            
            if (!this.validated)
            {
                this.validated = true;
                if (!this.ValidateAndUpdateValue() && this.cancelOnError)
                {
                    this.txtInput.Focus();
                    return;
                }              
            }        
          
            this.hasFocus = false;
        }

        /// <summary>
        /// Handles the checked changed event of chkapprox.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param>
        private void ChkApprox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkApprox.Checked)
            {
                this.TimeType = TimeType.Approximate;
            }
            else if (this.TimeType == TimeType.Approximate && this.EditingTimeType == TimeType.Approximate)
            {
                this.TimeType = TimeType.Exact;
            }
        }     

        /// <summary>
        /// Handles the font changed event of the control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param> 
        private void TimeInputBox_FontChanged(object sender, EventArgs e)
        {
            GlobalizationService gs = new GlobalizationService();
            string max = gs.ShortTimePatternWithSecondsAMPM + "d";            
            Graphics g = this.CreateGraphics();
            SizeF newSize = g.MeasureString(max, this.Font);
            int singleCharWidth = (int)(newSize.Width / max.Length);
            this.btnDown.Width = singleCharWidth > MinButtonWidth ? singleCharWidth : MinButtonWidth;
            this.btnUp.Width = this.btnDown.Width;
            this.Width = newSize.Width > MinWidth ? (int)newSize.Width : MinWidth;
            this.Height = newSize.Height + BorderPadding > MinHeight ? (int)newSize.Height + BorderPadding : MinHeight;
        }

        /// <summary>
        /// Handles the click event of up button.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param> 
        private void BtnUp_Click(object sender, EventArgs e)
        {
            this.focusAfterSpin = true;
            if (this.editingTimeType == TimeType.NullIndex || this.invalidFormat ||
              this.currentMode == InputMode.Arithmetic || this.editingTimeType == TimeType.Null)
            {
                return;
            }

            this.txtInput.SelectionStart = this.recentSelectionStart;
            this.txtInput.SelectionLength = this.recentSelectionLength;
            this.HandleIncrease();
            this.txtInput.Focus();           
        }

        /// <summary>
        /// Handles the click event of down button.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param> 
        private void BtnDown_Click(object sender, EventArgs e)
        {
            this.focusAfterSpin = true;
            if (this.editingTimeType == TimeType.NullIndex || this.invalidFormat ||
             this.currentMode == InputMode.Arithmetic || this.editingTimeType == TimeType.Null)
            {
                return;
            }

            this.txtInput.SelectionStart = this.recentSelectionStart;
            this.txtInput.SelectionLength = this.recentSelectionLength;
            this.HandleDecrease();
            this.txtInput.Focus();
        }

        /// <summary>
        /// Handles the forecolor changed event of the control.
        /// </summary>
        /// <param name="sender">Object calling the event</param>
        /// <param name="e">Event arguments</param> 
        private void TimeInputBox_ForeColorChanged(object sender, EventArgs e)
        {
            this.txtInput.ForeColor = this.ForeColor;
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
            this.validated = true;
            if (!this.ValidateAndUpdateValue())
            {
                e.Cancel = this.cancelOnError;
            }
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
