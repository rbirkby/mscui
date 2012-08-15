//-----------------------------------------------------------------------
// <copyright file="NameLine.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>14-Sep-2007</date>
// <summary>Simple class to hold configuration of NameInputBox's NameLines.</summary>
//-----------------------------------------------------------------------    
namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Text;
    using System.Web.UI.WebControls;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Web.UI;

    /// <summary>
    /// Simple class to hold configuration of NameInputBox's NameLines.
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class NameLine
    {
        /// <summary>
        /// Indicates whether an NameLine is rendered as UI on the page. 
        /// </summary>
        private bool visible;

        /// <summary>
        /// Indicates whether input into this AddrsssLine is required.
        /// </summary>
        private bool mandatory;

        /// <summary>
        /// Indicates whether a Label, for the NameLine, is rendered as UI on the page
        /// </summary>
        private bool showLabel;

        /// <summary>
        /// If a Label is being rendered this is the string to be used
        /// </summary>
        private string labelText;

        /// <summary>
        /// The regular expression assigned to be the validation criteria for this NameLine. The default is an empty string, which will mean no RegularExpressionValidator being rendered on the client
        /// </summary>
        private string validationExpression;

        /// <summary>
        /// Gets the TextBox control for this NameLine
        /// </summary>
        private TextBox inputBox;

        /// <summary>
        /// Default ctor
        /// </summary>
        public NameLine() : this(true, false)
        {
        }

        /// <summary>
        /// Ctor allowing the specifiying of the Visible and Mandatory flags
        /// </summary>
        /// <param name="visible">Indicates whether an NameLine is rendered as UI on the page.</param>
        /// <param name="mandatory">Indicates whether input into this AddrsssLine is required.</param>
        public NameLine(bool visible, bool mandatory)
        {
            this.visible = visible;
            this.mandatory = mandatory;
        }

        /// <summary>
        /// Get or sets a value that indicates whether input into this AddrsssLine is required.
        /// </summary>
        [Category("Behavior"), Description("Is input to this NameLine mandatory"), NotifyParentProperty(true), DefaultValue(false)]
        public bool Mandatory
        {
            get
            {
                return this.mandatory;
            }

            set
            {
                this.mandatory = value;
            }
        }

        /// <summary>
        /// Get or sets a value that indicates whether an NameLine is rendered as UI on the page.
        /// </summary>
        [Category("Behavior"), Description("Is this NameLine visible"), NotifyParentProperty(true), DefaultValue(true)]
        public bool Visible
        {
            get
            {
                return this.visible;
            }

            set
            {
                this.visible = value;
            }
        }

        /// <summary>
        /// Get or sets a value for the regular expression assigned to be the validation criteria for this NameLine. The default is an empty string, which will mean no RegularExpressionValidator being rendered on the client.
        /// </summary>
        [Category("Behavior"), Description("Regular expression to use to validate the format of the name line"), NotifyParentProperty(true), DefaultValue(null)]
        public string ValidationExpression
        {
            get
            {
                return this.validationExpression;
            }

            set
            {
                this.validationExpression = value;
            }
        }

        /// <summary>
        /// Get or sets a value that indicates whether a Label, for the NameLine, is rendered as UI on the page
        /// </summary>
        [Category("Appearance"), Description("Whether or not to show a Label for the NameLine"), NotifyParentProperty(true), DefaultValue(false)]
        public bool ShowLabel
        {
            get
            {
                return this.showLabel;
            }

            set
            {
                this.showLabel = value;
            }
        }

        /// <summary>
        /// Get or sets a string to be used as the Text for a Label is ShowLabel is true
        /// </summary>
        [Category("Appearance"), Description("The text for the label (if shown, see ShowLabel"), NotifyParentProperty(true), DefaultValue(null)]
        public string LabelText
        {
            get
            {
                return this.labelText;
            }

            set
            {
                this.labelText = value;
            }
        }

        /// <summary>
        /// Gets the TextBox control for this NameLine
        /// </summary>
        public TextBox InputBox
        {
            get
            {
                return this.inputBox;
            }
        }

        /// <summary>
        /// Internal only method that lets the NameInputBox control pass a reference to the TextBox it creates
        /// </summary>
        /// <param name="inputBox">The TextBox control for this NameLine</param>
        internal void PassInputControl(Control inputBox)
        {
            this.inputBox = (TextBox)inputBox;
        }
    }
}
