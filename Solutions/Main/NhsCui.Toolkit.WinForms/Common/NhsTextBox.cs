//-----------------------------------------------------------------------
// <copyright file="NhsTextBox.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>31-May-2007</date>
// <summary>Base text box for input controls.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.WinForms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Base text box for input controls.
    /// </summary>
    internal partial class NhsTextBox : TextBox
    {
        #region Constants

        /// <summary>
        /// Mouse double click message value.
        /// </summary>
        private const int WmDoubleClick = 515;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new instance of the NhsTextBox.
        /// </summary>
        public NhsTextBox()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.InitializeComponent();
        }
        #endregion

        #region Custom Event

        /// <summary>
        /// Occurs when the field is double clicked by the mouse.
        /// </summary>
        public event EventHandler FieldDoubleClicked;
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Overrides the base message handling for mouse double click.
        /// </summary>
        /// <param name="m"> Message </param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmDoubleClick)
            {
                if (this.FieldDoubleClicked != null)
                {
                    this.FieldDoubleClicked(this, new EventArgs());
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }
        #endregion
    }
}
