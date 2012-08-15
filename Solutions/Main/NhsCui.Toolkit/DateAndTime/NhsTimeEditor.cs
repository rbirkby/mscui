//-----------------------------------------------------------------------
// <copyright file="NhsTimeEditor.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>A property editor for NhsTime types.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.DateAndTime
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.Globalization;

    /// <summary>
    /// A property editor for NhsTime types. 
    /// </summary>
    public partial class NhsTimeEditor : UserControl
    {
        #region Member Variables
        /// <summary>
        /// The property to be edited. 
        /// </summary>
        /// <remarks>
        /// TimeLabel.Value and TimeInputBox.Value can be edited using the property editor. 
        /// </remarks>
        private NhsTime value;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs an NhsTimeEditor object.
        /// </summary>
        public NhsTimeEditor()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the NhsTime value. 
        /// </summary>
        public NhsTime Value
        {
            get
            {
                return this.value;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                this.value = value;
                this.valueTextBox.Text = this.value.ToString(false);

                if (this.value.TimeType == TimeType.Approximate)
                {
                    this.approxCheckbox.Checked = true;
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Approximate checkbox changed event. 
        /// ‍</summary>
        /// <param name="sender">‍
        /// The checkbox.
        /// </param>
        /// <param name="e">
        /// The arguments.
        /// ‍</param>
        private void ApproxCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.approxCheckbox.Checked)
            {
                this.value.TimeType = TimeType.Approximate;
            }
            else
            {
                if (this.value.TimeType == TimeType.Approximate)
                {
                    this.value.TimeType = TimeType.Exact;
                }
            }

            this.valueTextBox.Text = this.value.ToString(false);
        }

        /// <summary>
        /// The lost focus event for the value textbox.
        /// ‍</summary>
        /// <param name="sender">
        /// The textbox
        /// ‍</param>
        /// <param name="e">‍
        /// The arguments.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Not a public method!")]
        private void ValueTextBox_Leave(object sender, EventArgs e)
        {
            NhsTime time;
            if (NhsTime.TryParseExact(this.valueTextBox.Text, out time, CultureInfo.CurrentCulture))
            {
                this.value = time;

                if (this.value.TimeType == TimeType.Exact && this.approxCheckbox.Checked)
                {
                    this.value.TimeType = TimeType.Approximate;
                }
            }

            this.valueTextBox.Text = this.value.ToString(false);

            if (this.value.TimeType != TimeType.Approximate)
            {
                this.approxCheckbox.Checked = false;
            }
        }
        #endregion
    }
}