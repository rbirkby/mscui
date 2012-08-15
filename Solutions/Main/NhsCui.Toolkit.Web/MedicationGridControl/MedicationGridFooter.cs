//------------------------------------------------------------------------
// <copyright file="MedicationGridFooter.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>27-Feb-2007</date>
// <summary>The MedicationGridFooter terminates tags opened in the MedicationGridHeader</summary>
//------------------------------------------------------------------------
namespace NhsCui.Toolkit.Web
{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel;
    using System.Web.UI;
    #endregion

    /// <summary>   
    /// The MedicationGridFooter terminates tags opened in the MedicationGridHeader
    /// </summary>
    [Browsable(false)]
    [DesignTimeVisible(false)]
    internal class MedicationGridFooter : Control
    {
        /// <summary>
        /// Render Contents
        /// </summary>
        /// <param name="writer">HtmlTextWriter to render to</param>
        protected override void Render(HtmlTextWriter writer)
        {
            // Body Table End Tag
            writer.WriteEndTag(HtmlTextWriterTag.Table.ToString());

            // Body End Tags
            writer.WriteEndTag(HtmlTextWriterTag.Div.ToString());
            writer.WriteEndTag(HtmlTextWriterTag.Td.ToString());
            writer.WriteEndTag(HtmlTextWriterTag.Tr.ToString());

            // Scroll Table End Tag
            writer.WriteEndTag(HtmlTextWriterTag.Table.ToString());
        }
    }
}