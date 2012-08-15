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
// <summary>The control used to display a label representing a time span such as the patient's age.</summary>
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
    using System.Globalization;
    using System.Drawing;

    using NhsCui.Toolkit.DateAndTime;

    /// <summary>
    /// The control used to display a label representing a time span such as the patient's age. 
    /// </summary>
    [DefaultProperty("Value")]
    [ToolboxData("<{0}:TimeSpanLabel runat=server></{0}:TimeSpanLabel>")]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "TimeSpanLabel.bmp")]
    [Designer(typeof(TimeSpanLabelDesigner))]
    public partial class TimeSpanLabel : WebControl
    {
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
                this.Value.From = value;
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
                this.Value.To = value;
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
                this.Value.IsAge = value;
            }
        }

        /// <summary>
        /// The wrapper for Value.Threshold. 
        /// </summary>
        /// <remarks>
        /// Specifies the smallest units to be displayed. If <see cref="P:NhsCui.Toolkit.Web.TimeSpanLabel.IsAge">IsAge</see> is true, the threshold 
        /// is set from the time span.
        /// If <see cref="P:NhsCui.Toolkit.Web.TimeSpanLabel.IsAge">IsAge</see> is false, the threshold  is set from the assigned threshold value. The 
        /// threshold may be seconds, minutes,
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
                this.Value.Threshold = value;
            }
        }

        /// <summary>
        /// The wrapper for Value.Granularity. 
        /// </summary>
        /// <remarks>
        /// Specifies the largest units to be displayed. If <see cref="P:NhsCui.Toolkit.Web.TimeSpanLabel.IsAge">IsAge</see> is true, the 
        /// granularity is set from the time span.
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
                this.Value.Granularity = value;
            }
        }

        /// <summary>
        /// Gets or sets the time span to be displayed in the time span label. 
        /// </summary>
        [Bindable(true), Category("Behavior"), Browsable(false)]
        [Description("The time span to display in the label")]
        public NhsTimeSpan Value
        {
            get
            {
                object nhsTimeSpanObject = ViewState["NhsTimeSpanValue"];

                if (nhsTimeSpanObject != null)
                {
                    return (NhsTimeSpan)nhsTimeSpanObject;
                }
                else
                {
                    NhsTimeSpan newTimeSpan = new NhsTimeSpan();

                    ViewState["NhsTimeSpanValue"] = newTimeSpan;

                    return newTimeSpan;
                }
            }

            set
            {
                ViewState["NhsTimeSpanValue"] = value;
            }
        }

        /// <summary>
        /// Specifies whether long or short units are displayed. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Short". If long units are specified, the entire unit name is included, such as "month" or "minute". 
        /// If short units are specified, an abbreviated 
        /// form of the unit is included, for example "m" for month or "min" for minute. Singular and plural versions of the 
        /// short and long units are included automatically as appropriate. 
        /// </remarks>
        [Category("Behavior"), DefaultValue(TimeSpanUnitLength.Short)]
        [Description("The length of the time span units")]
        public TimeSpanUnitLength UnitLength
        {
            get
            {
                object objUnitLength = this.ViewState["UnitLength"];

                return (objUnitLength == null ? TimeSpanUnitLength.Short : (TimeSpanUnitLength)objUnitLength);
            }

            set
            {
                if (!Enum.IsDefined(typeof(TimeSpanUnitLength), value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                this.ViewState["UnitLength"] = (value != TimeSpanUnitLength.Short ? (object)value : null);
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
                string timeSpan = this.Value.ToString(this.UnitLength, CultureInfo.CurrentCulture);

                // If there is no span of time AND we are in design mode, render out the control
                // ID rather than an empty string
                if (timeSpan.Length == 0 && this.DesignMode)
                {
                    writer.WriteEncodedText(string.Format(CultureInfo.InvariantCulture, "[{0}]", this.ID));
                }
                else
                {
                    writer.WriteEncodedText(timeSpan);
                }
            }
        }
    }
}
