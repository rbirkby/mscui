//-----------------------------------------------------------------------
// <copyright file="GenderLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation.
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
// <summary>The control used to display a patient's gender. </summary>
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
    using System.Drawing;

    /// <summary>
    /// The control used to display a patient's gender. 
    /// </summary>
    [DefaultProperty("Value")]
    [ToolboxData("<{0}:GenderLabel runat=server></{0}:GenderLabel>")]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "GenderLabel.bmp")]
    public class GenderLabel : WebControl
    {
        #region Properties

        /// <summary>
        /// Gets or sets the patient's gender. 
        /// </summary>
        /// <remarks>
        /// This may be "Female", "Male", "NotSpecified" or "NotKnown". 
        /// </remarks>
        [Bindable(true), Category("Behavior"), DefaultValue(PatientGender.NotKnown)]
        [Description("PatientGender enumeration value - Female, Male, NotSpecified or NotKnown.")]
        public PatientGender Value
        {
            get
            {
                object patientGenderObject = this.ViewState["Value"];

                if (patientGenderObject != null)
                {
                    return (PatientGender)patientGenderObject;
                }

                return PatientGender.NotKnown;
            }

            set
            {
                if (!Enum.IsDefined(typeof(PatientGender), value))
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                if (value != PatientGender.NotKnown)
                {
                    this.ViewState["Value"] = value;
                }
                else
                {
                    this.ViewState["Value"] = null;
                }
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Renders the contents of the control to the specified writer
        /// </summary>
        /// <param name="writer">A HtmlTextWriter that represents the output stream to render 
        /// HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            string valueToWrite;

            switch (this.Value)
            {
                case PatientGender.Male:
                    valueToWrite = GenderLabelControl.Resources.Male;
                    break;
                case PatientGender.Female:
                    valueToWrite = GenderLabelControl.Resources.Female;
                    break;
                case PatientGender.NotSpecified:
                    valueToWrite = GenderLabelControl.Resources.NotSpecified;
                    break;
                default:
                    valueToWrite = GenderLabelControl.Resources.NotKnown;
                    break;
            }

            writer.WriteEncodedText(valueToWrite);
        }
        #endregion
    }
}
