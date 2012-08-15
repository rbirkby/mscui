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
// <date>31-Jan-2007</date>
// <summary>The control used to display an NhsTime. </summary>
//-----------------------------------------------------------------------

//Although the control currently support both 24 hour and 12 hour clock the use of 12 hour clock is NOT supported 
//for safety critical environments, and the ISV has a responsibility to use the controls in a manner appropriate to 
//the clinical context.

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using System.Resources;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Drawing;
    using System.Drawing.Design;
    using NhsCui.Toolkit;
    using NhsCui.Toolkit.DateAndTime;
    using NhsCui.Toolkit.DateAndTime.Resources;

    // using NhsCui.Toolkit.DateAndTime.Resources;

    /// <summary>
    /// The control used to display an NhsTime.
    /// </summary>
    [DefaultProperty("Value")]
    [ToolboxData("<{0}:TimeLabel runat=server></{0}:TimeLabel>")]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "TimeLabel.bmp")]
    [Designer(typeof(TimeLabelDesigner))]
    public class TimeLabel : WebControl
    {       
        #region Constructors
        /// <summary>
        /// Constructs a TimeLabel object. 
        /// </summary>
        public TimeLabel() : base(HtmlTextWriterTag.Span)
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The NhsTime to be displayed in the label. 
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
                object nhsTimeObject = this.ViewState["Value"];

                if (nhsTimeObject == null)
                {
                    nhsTimeObject = new NhsTime();

                    this.ViewState["Value"] = nhsTimeObject;
                }

                return (NhsTime)nhsTimeObject;
            }

            set
            {
                ViewState["Value"] = value;
            }
        }

        /// <summary>
        /// A list of localized strings that identify different types of null index times. 
        /// </summary>
        /// <remarks>
        /// Defaults to an empty list.
        /// </remarks>
        [Category("Appearance"), DefaultValue(null), TypeConverter(typeof(StringArrayConverter))]
        [Description("A list of localized strings that identify different types of null times (defaults to an empty list).")]
        [RefreshProperties(RefreshProperties.All)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "NullStrings array only expected to be small")]
        public string[] NullStrings
        {
            get
            {
                string[] nullStrings = this.NullStringsCore;

                if (nullStrings != null)
                {
                    return (string[])nullStrings.Clone();
                }

                return new string[0];
            }

            set
            {
                NullStringUtil.TrimAndValidate(value, true);
                this.NullStringsCore = value;
            }
        }

        /// <summary>
        /// The TimeType that represents the time. 
        /// </summary>
        [Bindable(true), Category("Data"), DefaultValue(TimeType.Exact)]
        public TimeType TimeType
        {
            get
            {
                return this.Value.TimeType;
            }

            set
            {
                this.Value.TimeType = value;
            }
        }

        /// <summary>
        /// The exact or approximate time value. 
        /// </summary>
        [Bindable(true), Category("Data")]
        public DateTime TimeValue
        {
            get
            {
                return this.Value.TimeValue;
            }

            set
            {
                this.Value.TimeValue = value;
            }
        }       

        /// <summary>
        /// A number identifying a null index type.
        /// </summary>
        /// <remarks>
        /// Defaults to -1. The index has no intrinsic meaning in and of itself; the meaning is implied by the 
        /// <see cref="P:NhsCui.Toolkit.Web.TimeLabel.NullStrings"> NullStrings</see> property.
        /// </remarks>
        [Bindable(true), Category("Data"), DefaultValue(-1)]
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
                return (this.ViewState["DisplaySeconds"] != null);
            }

            set
            {
                if (value)
                {
                    this.ViewState["DisplaySeconds"] = true;
                }
                else
                {
                    this.ViewState["DisplaySeconds"] = null;
                }
            }
        }

        /// <summary>
        /// Specifies whether the hours value should be displayed in 12-hour or 24-hour format. 
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
                return (this.ViewState["Display12Hour"] != null);
            }

            set
            {
                if (value)
                {
                    this.ViewState["Display12Hour"] = true;
                }
                else
                {
                    this.ViewState["Display12Hour"] = null;
                }
            }
        }

        /// <summary>
        /// Specifies whether an AM/PM suffix should be included. 
        /// </summary>
        /// <remarks> 
        /// Defaults to false.  AM refers to times from 00:00 to 11:59:59.  PM refers to times from 12:00 to 23:59:59
        /// </remarks>
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Specifies whether the AM/PM suffix should be included")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased", Justification = "As specified")]
        public bool DisplayAMPM
        {
            get
            {
                return (this.ViewState["DisplayAMPM"] != null);
            }

            set
            {
                if (value)
                {
                    this.ViewState["DisplayAMPM"] = true;
                }
                else
                {
                    this.ViewState["DisplayAMPM"] = null;
                }
            }
        }

        /// <summary>
        /// The text displayed when the mouse pointer hovers over the Web server control. 
        /// </summary>
        /// <remarks>
        /// The text for the ToolTip is drawn from TimeResources.MorningDescription, TimeResources.AfternoonDescription, 
        /// TimeResources.EveningDescription or TimeResources.NightDescription according to the 
        /// <see cref="P:NhsCui.Toolkit.Web.TimeLabel.TimePeriod"> TimePeriod</see> value.
        /// </remarks>
        public override string ToolTip
        {
            get
            {               
               return base.ToolTip;               
            }

            set
            {                
               base.ToolTip = value;                
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Core implementation of NullStrings property
        /// </summary>
        /// <remarks>
        /// Defaults to an empty list.
        /// </remarks>
        private string[] NullStringsCore
        {
            get
            {
                return this.ViewState["NullStrings"] as string[];
            }

            set
            {
                this.ViewState["NullStrings"] = value;
            }
        }

        /// <summary>
        /// Value formatted as displayable string
        /// </summary>
        private string FormattedValue
        {
            get
            {
                string formattedValue = string.Empty;

                if (this.Value.TimeType == TimeType.NullIndex && this.NullStringsCore != null &&
                        this.NullIndex >= 0 && this.NullIndex < this.NullStringsCore.Length)
                {
                    formattedValue = this.NullStringsCore[this.NullIndex];
                }
                else
                {
                    formattedValue = this.Value.ToString(
                                                    (this.Value.TimeType == TimeType.Approximate),
                                                    CultureInfo.CurrentCulture,
                                                    this.DisplaySeconds,
                                                    this.Display12Hour,
                                                    this.DisplayAMPM);
                }

                return formattedValue;
            }
        }

        #endregion

        #region Protected Methods
        /// <summary>
        /// Adds HTML attributes and styles that need to be rendered to the specified HtmlTextWriterTag
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output stream to render HTML content on the client.</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);           
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output stream to render 
        /// HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.WriteEncodedText(this.FormattedValue);
        }

        #endregion

        #region Private Methods       
        /// <summary>
        /// Whether the designer should serializr the tooltip property
        /// </summary>
        /// <returns>true if should be serialized</returns>
        private bool ShouldSerializeToolTip()
        {
            return (base.ToolTip.Length > 0);
        }

        #endregion
    }
}
