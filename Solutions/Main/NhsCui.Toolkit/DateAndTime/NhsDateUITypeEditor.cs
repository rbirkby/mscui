//-----------------------------------------------------------------------
// <copyright file="NhsDateUITypeEditor.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>A property editor for editing NhsDate types. </summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.DateAndTime
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Security.Permissions;

    /// <summary>
    /// A property editor for NhsDate types. 
    /// </summary>
    public class NhsDatePropertyEditor : UITypeEditor
    {
        #region Public Methods
        /// <summary>
        /// Loads the NhsDateEditor form based on the value provided. 
        /// </summary>
        /// <param name="context">The PropertyType descriptor context.</param>
        /// <param name="provider">The PropertyEditor service provider. </param>
        /// <param name="value">The property's value. </param>
        /// <returns>The changed value. </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "FxCop Bug")]
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if ((context != null) && (provider != null) && (value != null))
            {
                IWindowsFormsEditorService svc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (svc != null)
                {
                    NhsDateEditor editor = new NhsDateEditor();
                    editor.Value = (NhsDate)value;

                    svc.DropDownControl(editor);
                    value = editor.Value;
                }
            }

            return value;
        }

        /// <summary>
        /// Makes the property editor modal. 
        /// </summary>
        /// <param name="context">The property type descriptor. </param>
        /// <returns>The changed value.</returns>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
        #endregion
    }
}
