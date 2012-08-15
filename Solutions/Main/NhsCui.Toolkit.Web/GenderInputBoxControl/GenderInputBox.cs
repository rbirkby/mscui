//-----------------------------------------------------------------------
// <copyright file="GenderInputBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to enter an Gender. </summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.UI.WebControls;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Drawing;
    using System.Globalization;

    /// <summary>
    /// The control used to enter an Gender. 
    /// </summary>
    [DefaultProperty("Value"), ParseChildren(true, "Value")]
    [ToolboxData("<{0}:GenderInputBox runat=server></{0}:GenderInputBox>")]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "GenderLabel.bmp")] // TODO: Needs bitmap
        [ToolboxItem(false)]
    public class GenderInputBox : WebControl
    {
        /// <summary>
        /// Private items collection for the gender values
        /// </summary>
        private ListItemCollection items = new ListItemCollection();

        /// <summary>
        /// Currently selected value index
        /// </summary>
        private int selectedIndex;

        /// <summary>
        /// Default constructor
        /// </summary>
        public GenderInputBox()
        {
            this.items.Clear();

            this.items.Add(new ListItem(GenderLabelControl.Resources.Male.ToString(), Convert.ToString((int)PatientGender.Male, CultureInfo.InvariantCulture)));
            this.items.Add(new ListItem(GenderLabelControl.Resources.Female.ToString(), Convert.ToString((int)PatientGender.Female, CultureInfo.InvariantCulture)));
            this.items.Add(new ListItem(GenderLabelControl.Resources.NotKnown.ToString(), Convert.ToString((int)PatientGender.NotKnown, CultureInfo.InvariantCulture)));
            this.items.Add(new ListItem(GenderLabelControl.Resources.NotSpecified.ToString(), Convert.ToString((int)PatientGender.NotSpecified, CultureInfo.InvariantCulture)));
            this.SelectedIndex = (int)this.Value;
        }

        /// <summary>
        /// Occurs when a property value changes. 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when the value of the ComboBox changes between posts to the server. 
        /// </summary>
        public event EventHandler ValueChanged;

        #region Properties

        /// <summary>
        /// Gets or sets the patient's gender. 
        /// </summary>
        /// <remarks>
        /// This may be "Female", "Male", "NotSpecified" or "NotKnown". 
        /// </remarks>
        [Bindable(true), Category("Behavior"), DefaultValue(PatientGender.NotKnown)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

                this.SelectedIndex = (int)value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs("Value");

                this.OnPropertyChanged(e);
            }
        }

        /// <summary>
        /// SelectedIndex property
        /// </summary>
        [DefaultValue(0)]
        public int SelectedIndex
        {
            get
            {
                if ((this.selectedIndex < 0) && (this.items.Count > 0))
                {
                    this.items[0].Selected = true;
                    this.selectedIndex = 0;
                }

                return this.selectedIndex;
            }

            set
            {
                this.selectedIndex = value;
                this.OnValueChanged(null);
            }
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Raise property changed event
        /// </summary>
        /// <param name="e">event args</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Use the load event to set the enum content
        /// </summary>
        /// <param name="e">event args</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // if (Page.IsPostBack != true)
            // {
            //    this.Items.Add(new ListItem(GenderLabelControl.Resources.Male.ToString(), Convert.ToString((int)PatientGender.Male)));
            //    this.Items.Add(new ListItem(GenderLabelControl.Resources.Female.ToString(), Convert.ToString((int)PatientGender.Female)));
            //    this.Items.Add(new ListItem(GenderLabelControl.Resources.NotKnown.ToString(), Convert.ToString((int)PatientGender.NotKnown)));
            //    this.Items.Add(new ListItem(GenderLabelControl.Resources.NotSpecified.ToString(), Convert.ToString((int)PatientGender.NotSpecified)));
            // }
        }

        /// <summary>
        /// Raise the ValueChanged event
        /// </summary>
        /// <param name="e">event args</param>
        protected virtual void OnValueChanged(EventArgs e)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(this, e);
            }
        }

        /// <summary>
        /// Create the content and render it
        /// </summary>
        /// <param name="writer">HTML Text Writer instance</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            base.RenderContents(writer);

            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID);
            writer.RenderBeginTag(HtmlTextWriterTag.Select);

            for (int genderIndex = 0; genderIndex < this.items.Count; genderIndex++)
            {
                if (genderIndex == this.SelectedIndex)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Selected, "true");
                }

                writer.AddAttribute(HtmlTextWriterAttribute.Value, genderIndex.ToString(CultureInfo.InvariantCulture));
                writer.RenderBeginTag(HtmlTextWriterTag.Option);
                writer.Write(this.items[genderIndex].Text);
                writer.RenderEndTag();
            }

            writer.RenderEndTag();
        }
        #endregion

        #region Private methods

        /// <summary>
        /// Handle value changed event of our underlying ComboBox
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void DropDownListValueChanged(object sender, EventArgs e)
        {
            this.OnValueChanged(e);
        }

        #endregion
    }
}
