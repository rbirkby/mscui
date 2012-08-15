//-----------------------------------------------------------------------
// <copyright file="MedicationGridHeader.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>27/02/2007</date>
// <summary>The MedicationGridHeader contructs the header and start elements within with the MedicationLines are rendered</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Drawing.Design;
    using System.Drawing;
    using System.Security.Permissions;
    using System.Collections.ObjectModel;
    using Microsoft.Security.Application;
    using System.Web.UI.HtmlControls;
    using System.Diagnostics.CodeAnalysis;
    #endregion

    /// <summary>The MedicationGridHeader displays a colleciton of MedicationLine controls and enforces certain settings on them</summary>
    [Browsable(false)]
    [DesignTimeVisible(false)]
    internal class MedicationGridHeader : Control
    {
        #region Members Vars
        /// <summary>
        /// Parent Medication Grid
        /// </summary>
        private MedicationGrid parentGrid;

        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets a reference to the Parent medication grid 
        /// </summary>
        public MedicationGrid ParentGrid
        {
            get
            {
                return this.parentGrid;
            }

            set
            {
                this.parentGrid = value;
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Render Contents
        /// </summary>
        /// <param name="writer">HtmlTextWriter to render to</param>
        protected override void Render(HtmlTextWriter writer)
        {
            // Render directly to the writer for performance reasons. This should also make the control
            // easier to port to pure client-side rendering if required

            // Scroll Table
            writer.WriteBeginTag(HtmlTextWriterTag.Table.ToString());
            writer.WriteAttribute("id", this.parentGrid.ClientID + "_scrollTable");
            writer.WriteAttribute("cellspacing", "0");
            writer.WriteAttribute("cellpadding", "0");
            writer.WriteAttribute("border", "0");            
            writer.WriteAttribute("class", "nhscui_mg_scroll_table");
            if (!this.DesignMode)
            {
                writer.Write("style=\"");
                writer.WriteStyleAttribute(HtmlTextWriterStyle.Visibility.ToString(), "hidden");
                writer.WriteStyleAttribute(HtmlTextWriterStyle.Width.ToString(), "100%");
                writer.Write(HtmlTextWriter.DoubleQuoteChar);
            }

            writer.Write(HtmlTextWriter.TagRightChar);

            // Header 
            writer.WriteFullBeginTag(HtmlTextWriterTag.Tr.ToString());
            writer.WriteFullBeginTag(HtmlTextWriterTag.Td.ToString());
            writer.WriteBeginTag(HtmlTextWriterTag.Div.ToString());
            writer.WriteAttribute("class", "nhscui_mg_header");            

            writer.Write("style=\"");
            writer.WriteStyleAttribute("width", this.ParentGrid.Width.ToString());
            writer.Write(HtmlTextWriter.DoubleQuoteChar);

            writer.Write(HtmlTextWriter.TagRightChar);

            writer.WriteBeginTag(HtmlTextWriterTag.Table.ToString());
            writer.WriteAttribute("id", "headerTable");
            writer.WriteAttribute("class", "nhscui_mg_header_table");
            writer.WriteAttribute("cellspacing", "0");
            writer.WriteAttribute("cellpadding", "0");            
            writer.WriteAttribute("border", "0");

            // Apply BorderCollapse override. This Style attribute will be removed client-side
            // This is to resolve an IE Issue where collapsed borders are not hidden on render
            writer.Write("style=\"");
            if (!string.IsNullOrEmpty(this.ParentGrid.Width.ToString()))
            {
                writer.WriteStyleAttribute("width", "100%");            
            }

            writer.WriteStyleAttribute("border-collapse", "separate");            
            writer.Write(HtmlTextWriter.DoubleQuoteChar);
            writer.Write(HtmlTextWriter.TagRightChar);

            writer.WriteFullBeginTag(HtmlTextWriterTag.Tr.ToString());

            // StartDate Column Header
            MedicationGridHeader.WriteColumnHeader(writer, "nhscui_mg_header_startdate", this.parentGrid.StartDateColumnWidth, this.parentGrid.StartDateColumnHeaderText, true, 1);

            // DrugDetails Column Header
            MedicationGridHeader.WriteColumnHeader(writer, "nhscui_mg_header_drugdetails", this.parentGrid.DrugDetailsColumnWidth, this.parentGrid.DrugDetailsColumnHeaderText, true, 3);

            // Reason Column Header
            MedicationGridHeader.WriteColumnHeader(writer, "nhscui_mg_header_reason", this.parentGrid.ReasonColumnWidth, this.parentGrid.ReasonColumnHeaderText, this.parentGrid.ShowReason, 1);

            // Status Column Header
            MedicationGridHeader.WriteColumnHeader(writer, "nhscui_mg_header_status", this.parentGrid.StatusColumnWidth, this.parentGrid.StatusColumnHeaderText, this.parentGrid.ShowStatus || this.parentGrid.ShowStatusDate, 1);

            // Header End Tags
            writer.WriteEndTag(HtmlTextWriterTag.Table.ToString());
            writer.WriteEndTag(HtmlTextWriterTag.Div.ToString());
            writer.WriteEndTag(HtmlTextWriterTag.Td.ToString());
            writer.WriteEndTag(HtmlTextWriterTag.Tr.ToString());
                        
            // Body
            writer.WriteFullBeginTag(HtmlTextWriterTag.Tr.ToString());
            writer.WriteFullBeginTag(HtmlTextWriterTag.Td.ToString());
            writer.WriteBeginTag(HtmlTextWriterTag.Div.ToString());
            writer.WriteAttribute("id", this.parentGrid.ClientID + "_bodyDiv");
            writer.WriteAttribute("class", "nhscui_mg_body");
            writer.Write(" style=\"");
            writer.WriteStyleAttribute("overflow-x", "hidden");

            // For initial rendering hide y overflow. This will be set where applicable in the behavior.
            writer.WriteStyleAttribute("overflow-y", "hidden");
            writer.WriteStyleAttribute(HtmlTextWriterStyle.Height.ToString(), "0");
            writer.Write(HtmlTextWriter.DoubleQuoteChar);
            writer.Write(HtmlTextWriter.TagRightChar);

            // Body Table
            writer.WriteBeginTag(HtmlTextWriterTag.Table.ToString());
            writer.WriteAttribute("id", this.parentGrid.ClientID + "_bodyTable");
            writer.WriteAttribute("cellspacing", "0");
            writer.WriteAttribute("cellpadding", "0");
            writer.WriteAttribute("class", "nhscui_mg_body_table");
            writer.WriteAttribute("border", "0");
            writer.WriteAttribute("onselectstart", "return false;");            

            // Apply BorderCollapse override. This Style attribute will be removed client-side
            // This is to resolve an IE Issue where collapsed borders are not hidden on render
            writer.Write("style=\"");            
            writer.WriteStyleAttribute("-moz-user-select", "none");            
            writer.WriteStyleAttribute("border-collapse", "separate");            
            writer.Write(HtmlTextWriter.DoubleQuoteChar);
            writer.Write(HtmlTextWriter.TagRightChar);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Write a new Column Header
        /// </summary>
        /// <param name="writer">HtmlTextWriter to write to </param>        
        /// <param name="cssClass">CssClass</param>
        /// <param name="columnWidth">Column Width in Units</param>        
        /// <param name="headerText">Header text</param>
        /// <param name="display">Should the header be displayed</param>        
        /// <param name="columnSpan">Column Span</param>
        private static void WriteColumnHeader(HtmlTextWriter writer, string cssClass, Unit columnWidth, string headerText, bool display, int columnSpan)
        {
            // DrugDetails Column Header
            writer.WriteBeginTag(HtmlTextWriterTag.Th.ToString());
            if (columnSpan > 1)
            {
                writer.WriteAttribute("colspan", "3");
            }

            writer.WriteAttribute("class", cssClass);
            writer.WriteAttribute("nowrap", "nowrap");
            writer.Write(" style=\"");
            writer.WriteStyleAttribute(HtmlTextWriterStyle.Width.ToString(), columnWidth.ToString());

            if (!display)
            {
                writer.WriteStyleAttribute(HtmlTextWriterStyle.Display.ToString(), "none");
            }

            writer.Write(HtmlTextWriter.DoubleQuoteChar);
            writer.Write(HtmlTextWriter.TagRightChar);

            writer.WriteBeginTag(HtmlTextWriterTag.Div.ToString());
            writer.Write(" style=\"");
            writer.WriteStyleAttribute("white-space", "nowrap");
            writer.WriteStyleAttribute("word-break", "none");
            writer.Write(HtmlTextWriter.DoubleQuoteChar);
            writer.Write(HtmlTextWriter.TagRightChar);

            writer.Write(AntiXss.HtmlEncode(headerText));
            writer.WriteEndTag(HtmlTextWriterTag.Div.ToString());
            writer.WriteEndTag(HtmlTextWriterTag.Th.ToString());            
        }
        #endregion
    }
}