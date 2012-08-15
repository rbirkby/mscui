//-----------------------------------------------------------------------
// <copyright file="NhsDateEditor.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>07-Feb-2007</date>
// <summary>A property editor for editing NhsDate properties. </summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.DateAndTime
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// A property editor for editing NhsDate properties. 
    /// </summary>
    public partial class NhsDateEditor : UserControl
    {
        #region Member Variables
        /// <summary>
        /// Value being edited
        /// </summary>
        private NhsDate value;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs an NhsDateEditor object.
        /// </summary>
        public NhsDateEditor()
        {
            this.InitializeComponent();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The property value being edited. 
        /// </summary>
        /// <remarks>
        /// When the Property Editor dialog box is open, the DateInputBox <see cref="P:NhsCui.Toolkit.Web.DateInputBox.Value">Value</see> is set
        /// to the value in the Properties Window. When the Property Editor dialog box is closed, the value in the Properties Window is set to
        /// the DateInputBox <see cref="P:NhsCui.Toolkit.Web.DateInputBox.Value">Value</see>.
        /// </remarks>
        public NhsDate Value
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
                this.UpdateDisplay();
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Approximate checkbox changed event. 
        /// ‍</summary>
        /// <param name="sender">The checkbox.</param>
        /// <param name="e">The arguments.</param>
        private void ApproxCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Value.DateType == DateType.Approximate ||
                        this.Value.DateType == DateType.Exact)
            {
                this.Value.DateType = (this.approxCheckbox.Checked ? DateType.Approximate : DateType.Exact);
                this.UpdateDisplay();
            }
        }

        /// <summary>
        /// The lost focus event for the value textbox.
        /// </summary>
        /// <param name="sender">The textbox</param>
        /// <param name="e">args</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Not a public method!")]
        private void ValueTextBox_Leave(object sender, EventArgs e)
        {
            NhsDate date;
            if (NhsDate.TryParseExact(this.valueTextBox.Text, out date, CultureInfo.CurrentCulture))
            {
                if (date.DateType == DateType.Exact && this.approxCheckbox.Checked)
                {
                    date.DateType = DateType.Approximate;
                }

                this.Value = date;
            }

            this.UpdateDisplay();
        }

        /// <summary>
        /// Update display
        /// </summary>
        private void UpdateDisplay()
        {
            this.valueTextBox.Text = this.Value.ToString(false, false, false, CultureInfo.CurrentCulture);
            this.approxCheckbox.Checked = (this.Value.DateType == DateType.Approximate);
        }
        #endregion
    }
}