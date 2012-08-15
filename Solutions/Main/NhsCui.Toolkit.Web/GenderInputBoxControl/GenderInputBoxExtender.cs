//-----------------------------------------------------------------------
// <copyright file="GenderInputBoxExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>14-Aug-2007</date>
// <summary>GenderInputBox Extender, class to provide server-side configuration 
// of the GenderInputBox's AJAX functionality</summary>
//-----------------------------------------------------------------------

#region [ Resources ]

[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.GenderInputBoxControl.GenderInputBox.js", "text/javascript")]

#endregion
namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel;
    using System.Web.UI;
    using AjaxControlToolkit;
    using System.Web.UI.WebControls;
    using System.Web.Script.Serialization;
    using System.Globalization;

    /// <summary>
    /// GenderInputBox Extender, class to provide server-side configuration of 
    /// the GenderInputBox's AJAX functionality
    /// </summary>
    [ToolboxItem(false)]
    [Designer(typeof(GenderInputBoxDesigner))]
    [TargetControlType(typeof(DropDownList))]
    [RequiredScript(typeof(CommonToolkitScripts), 0)]
    [RequiredScript(typeof(CommonNhsCuiToolkitScripts), 1)]
    [ClientScriptResource("NhsCui.Toolkit.Web.GenderInputBox", "NhsCui.Toolkit.Web.GenderInputBoxControl.GenderInputBox.js")]
    public class GenderInputBoxExtender : ExtenderControlBase
    {
        /// <summary>
        /// object to hold our state
        /// </summary>
        private GenderInputClientState state;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public GenderInputBoxExtender()
        {
            this.EnableClientState = true;
            this.state = new GenderInputClientState();

            this.ClientStateValuesLoaded += new EventHandler(this.GenderInputBoxExtender_ClientStateValuesLoaded);
        }

        /// <summary>
        /// The gender entered in the ComboBox
        /// </summary>
        [Description("The gender entered in the ComboBox")]
        [DefaultValue(PatientGender.NotKnown)]
        [ExtenderControlProperty]
        public PatientGender Value
        {
            get
            {
                DropDownList targetControl = ((DropDownList)this.FindControl(TargetControlID));
                if (targetControl != null)
                {
                    return (PatientGender)targetControl.SelectedIndex;
                }

                return PatientGender.NotKnown;
            }

            set
            {
                DropDownList targetControl = ((DropDownList)this.FindControl(TargetControlID));
                if (targetControl != null)
                {
                    targetControl.SelectedIndex = (int)value;
                    this.SetPropertyValue("Value", targetControl.SelectedIndex);
                }
            }
        }

        /// <summary>
        /// Add the gender enum values to the combo
        /// </summary>
        /// <param name="e">event args</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            DropDownList targetControl = ((DropDownList)this.FindControl(TargetControlID));
            if (targetControl != null && Page.IsPostBack != true)
            {
                targetControl.Items.Add(new ListItem(GenderLabelControl.Resources.Male.ToString(), Convert.ToString((int)PatientGender.Male, CultureInfo.InvariantCulture)));
                targetControl.Items.Add(new ListItem(GenderLabelControl.Resources.Female.ToString(), Convert.ToString((int)PatientGender.Female, CultureInfo.InvariantCulture)));
                targetControl.Items.Add(new ListItem(GenderLabelControl.Resources.NotKnown.ToString(), Convert.ToString((int)PatientGender.NotKnown, CultureInfo.InvariantCulture)));
                targetControl.Items.Add(new ListItem(GenderLabelControl.Resources.NotSpecified.ToString(), Convert.ToString((int)PatientGender.NotSpecified, CultureInfo.InvariantCulture)));
            }
        }

        /// <summary>
        /// Handle loading of client state
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void GenderInputBoxExtender_ClientStateValuesLoaded(object sender, EventArgs e)
        {
            if (this.ClientState != null)
            {
                JavaScriptSerializer jss = new JavaScriptSerializer();

                // May need to create one...
                // jss.RegisterConverters(new JavaScriptConverter[] { new GenderJavascriptConverter() });

                this.state = jss.Deserialize<GenderInputClientState>(ClientState);
            }
        }
    }
}
