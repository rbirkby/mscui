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
namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Drawing;

    using NhsCui.Toolkit.DateAndTime;
    using NhsCui.Toolkit;
    using NhsCui.Toolkit.Web.DateLabelControl;

    /// <summary>
    /// The control used to display a date. 
    /// </summary>
    [DefaultProperty("Value")]
    [ToolboxData("<{0}:DateLabel runat=server></{0}:DateLabel>")]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "DateLabel.bmp")]
    [Designer(typeof(DateLabelDesigner))]
    public class DateLabel : WebControl
    {                
        #region Constructors
        /// <summary>
        /// Constructs a DateLabel object. 
        /// </summary>
        public DateLabel() : base(HtmlTextWriterTag.Span)
        {
        }
        #endregion
        
        /// <summary>
        /// Gets or sets the date to be displayed in the label. 
        /// </summary>
        [Bindable(true), Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("The date to display in the label")]
        public NhsDate Value
        {
            get
            {
                object nhsDateObject = ViewState["NhsDateValue"];

                if (nhsDateObject != null)
                {
                    return (NhsDate)nhsDateObject;
                }
                else
                {
                    NhsDate newDate = new NhsDate();

                    ViewState["NhsDateValue"] = newDate;

                    return newDate;
                }
            }

            set
            {
                ViewState["NhsDateValue"] = value;
            }
        }

        /// <summary>
        /// Specifies whether the text "Today", "Tomorrow" or "Yesterday" is displayed in place of the date. 
        /// </summary>
        /// <remarks>Defaults to false. </remarks>
        [Category("Behavior"), DefaultValue(false)]
        [Description("Specifies whether or not to substitute Today, Tomorrow or Yesterday for actual dates")]
        public bool DisplayDateAsText
        {
            get
            {
                if (ViewState["DisplayDateAsText"] != null)
                {
                    return (bool)ViewState["DisplayDateAsText"];
                }
                else
                {
                    return false;
                }
            }

            set
            {
                ViewState["DisplayDateAsText"] = value;
            }
        }

        /// <summary>
        /// Specifies whether the name of the day is displayed with the date. 
        /// </summary>
        /// <remarks>
        /// Defaults to true. 
        /// </remarks>
        [Category("Appearance"), DefaultValue(false)]
        [Description("Specifies whether or not to display the day of week")]
        public bool DisplayDayOfWeek
        {
            get
            {
                object objDisplayDayOfWeek = this.ViewState["DisplayDayOfWeek"];
                return (objDisplayDayOfWeek == null ? false : (bool)objDisplayDayOfWeek);
            }

            set
            {
                if (value)
                {
                    this.ViewState["DisplayDayOfWeek"] = true;
                }
                else
                {
                    this.ViewState["DisplayDayOfWeek"] = null;
                }
            }
        }

        /// <summary>
        /// Gets or sets a list of localized strings that identify different types of null index dates. 
        /// </summary>
        /// <remarks>
        /// Defaults to an empty list. 
        /// </remarks>
        [Category("Behavior"), DefaultValue(null), TypeConverter(typeof(StringArrayConverter))]
        [Description("A list of localized strings that identify different types of null times (defaults to an empty list).")]
        [RefreshProperties(RefreshProperties.All)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "NullStrings array only expected to be small")]
        public string[] NullStrings
        {
            get
            {
                if (this.ViewState["NullStrings"] != null)
                {
                    return (string[])((string[])this.ViewState["NullStrings"]).Clone();
                }

                return new string[0];
            }

            set
            {
                NullStringUtil.TrimAndValidate(value, true);
                this.ViewState["NullStrings"] = value;
            }
        }

        /// <summary>
        /// The wrapper for Value.DateType. 
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue(DateType.Exact)]
        [Description("The DateType that represents the date")]
        public DateType DateType
        {
            get
            {
                return this.Value.DateType;
            }

            set
            {
                this.Value.DateType = value;
            }
        }

        /// <summary>
        /// The wrapper for Value.DateValue. 
        /// </summary>
        [Bindable(true), Category("Data")]
        [Description("The exact or approximate date value")]
        public System.DateTime DateValue
        {
            get
            {
                return this.Value.DateValue;
            }

            set
            {
                this.Value.DateValue = value;
            }
        }

        /// <summary>
        /// The wrapper for Value.Month. 
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue(1)]
        [Description("The month of a DateType.YearMonth date")]
        public int Month
        {
            get
            {
                return this.Value.Month;
            }

            set
            {
                this.Value.Month = value;
            }
        }

        /// <summary>
        /// The wrapper for Value.Year. 
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue(0)]
        [Description("The year of a DateType.Year, DateType.YearMonth or DateType")]
        public int Year
        {
            get
            {
                return this.Value.Year;
            }

            set
            {
                this.Value.Year = value;
            }
        }

        /// <summary>
        /// The wrapper for Value.NullIndex. 
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue(-1)]
        [Description("A number identifying a null index type (defaults to -1). The index has no intrinsic meaning by itself. The meaning is implied by the UI control")]
        public int NullIndex
        {
            get
            {
                return this.Value.NullIndex;
            }

            set
            {
                this.Value.NullIndex = value;
            }
        }  

        /// <summary>
        /// Renders the contents of the control to the specified writer
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output stream to render 
        /// HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (this.Value != null)
            {
                string[] nullStrings = this.NullStrings;

                // If null index is outside the bounds of null string then value.ToString takes care of that.
                if (this.Value.DateType == DateType.NullIndex && this.NullIndex >= 0 && this.NullIndex < nullStrings.Length)
                {                    
                    writer.WriteEncodedText(nullStrings[this.NullIndex]);
                }
                else
                {
                    // Note we do not pass True for the showApproxIndicatorWhenApproximate when the DateType is not Approximate because that 
                    // would be nonsensical
                    string formattedValue = this.Value.ToString(
                                                this.DisplayDayOfWeek, 
                                                (this.Value.DateType == DateType.Approximate),
                                                this.DisplayDateAsText, 
                                                CultureInfo.CurrentCulture);
                    writer.WriteEncodedText(formattedValue);
                }
            }
        }
    }
}
