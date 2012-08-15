//-----------------------------------------------------------------------
// <copyright file="MedicationListView.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>22-Jan-2007</date>
// <summary>Display a list of MedicationNames sourced from a list of Medications</summary>
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
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    
    using System.Web.Script;

    using AjaxControlToolkit;
    using System.Web.UI.HtmlControls;

    /// <summary>
    /// Display a list of MedicationNames sourced from a list of Medications
    /// </summary>    
    [Browsable(false)]
    [DesignTimeVisible(false)]
    internal class MedicationListView : WebControl, INamingContainer
    {        
        #region Private Members
        /// <summary>
        /// List of Medications
        /// </summary>
        private List<Medication> medicationList = new List<Medication>();

        /// <summary>
        /// Display Mode
        /// </summary>
        private MedicationListViewMode mode;

        /// <summary>
        /// Show/Hide Graphics
        /// </summary>
        private bool showGraphics;        
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor. Create a Div tag around the MedicationListView
        /// </summary>
        public MedicationListView()
            : base(HtmlTextWriterTag.Div)
        {
        }

        /// <summary>
        /// Initialize the MedicationList on initialization
        /// </summary>
        /// <param name="medicationList">MedicationList</param>
        public MedicationListView(List<Medication> medicationList)
            : this()
        {
            this.medicationList = medicationList;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Get/Set Medication List
        /// </summary>
        public List<Medication> Medications
        {
            get { return this.medicationList; }            
        }
         
        /// <summary>
        /// Get/Set Display Mode
        /// </summary>
        public MedicationListViewMode Mode
        {
            get { return this.mode; }
            set { this.mode  = value; }
        }

        /// <summary>
        /// Get/Set Show Hide Graphics
        /// </summary>
        public bool ShowGraphics
        {
            get { return this.showGraphics; }
            set { this.showGraphics = value; }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Add Attributes to Render
        /// </summary>
        /// <param name="writer">HtmlTextWriter to render to</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (this.Mode == MedicationListViewMode.LookBehind)
            {
                this.CssClass = "nhscui_mg_lookbehind";
            }
            else
            {
                this.CssClass = "nhscui_mg_lookahead";            
            }            
            
            base.AddAttributesToRender(writer);
        }

        /// <summary>
        /// Create Child COntrols
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (this.DesignMode)
            {
                // Render LookAhead/LookBehind text
                HtmlGenericControl controlName = new HtmlGenericControl();
                this.Controls.Add(controlName);

                if (this.mode == MedicationListViewMode.LookBehind)
                {
                    controlName.InnerText = MedicationGridControl.Resources.LookBehind;
                }
                else
                {
                    controlName.InnerText = MedicationGridControl.Resources.LookAhead;
                }
            }
            else
            {
                Panel drugWrapper = new Panel();
                drugWrapper.CssClass = "nhscui_mg_look_itemswrapper";
                drugWrapper.Style.Add(HtmlTextWriterStyle.Overflow, "hidden");
                drugWrapper.Style.Add(HtmlTextWriterStyle.OverflowX, "hidden");
                drugWrapper.Style.Add(HtmlTextWriterStyle.Position, "relative");

                // Default height of the line is 1px - this will be automatically resized within the behavior
                drugWrapper.Style.Add(HtmlTextWriterStyle.Width, "1px");

                Controls.Add(drugWrapper);

                Panel drugPanel = new Panel();
                drugPanel.Style.Add(HtmlTextWriterStyle.Position, "relative");
                drugPanel.Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
                drugPanel.Style.Add("word-break", "none");
                drugWrapper.Controls.Add(drugPanel);

                this.AddMedicationNameLabels(drugPanel);
            }
        }

        /// <summary>
        /// On Render ensure that the child controls have been created. 
        /// </summary>
        /// <param name="writer">Html Text Writer</param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.ChildControlsCreated)
            {
                this.CreateChildControls();
            }

            base.Render(writer);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Help Method: Add MedicationName Labels as child panels to a parent panel
        /// </summary>
        /// <param name="parent">Parent Panel</param>
        private void AddMedicationNameLabels(Panel parent)
        {
            if (this.mode == MedicationListViewMode.LookBehind)
            {
                if (this.medicationList != null)
                {
                    for (int index = 0; index < this.medicationList.Count; index++)
                    {
                        this.AddMedicationNameLabel(this.medicationList[index], parent, "nhscui_mg_look_cell");
                    }
                }
            }
            else
            {
                // Reverse order if LookAhead
                if (this.medicationList != null)
                {
                    for (int index = this.Medications.Count - 1; index >= 0; index--)
                    {
                        this.AddMedicationNameLabel(this.medicationList[index], parent, "nhscui_mg_look_cell");
                    }
                }
            }
        }

        /// <summary>
        /// Add a MedicationNameLabel to the view
        /// </summary>
        /// <param name="medication">Medication to extract MedicationName from</param>
        /// <param name="parent">Parent Panel</param>
        /// <param name="cssClass">CssClass to apply to the MedicationNameLable</param>
        private void AddMedicationNameLabel(Medication medication, Panel parent, string cssClass)
        {   
            MedicationNameLabel medicationNameLabel = new MedicationNameLabel(medication.MedicationNames);
            medicationNameLabel.EnableViewState = false;
            medicationNameLabel.EnableExtender = false;
            medicationNameLabel.ShowGraphics = this.showGraphics;
            medicationNameLabel.CriticalAlertGraphic = medication.CriticalAlertGraphic;
            medicationNameLabel.IndicatorGraphic = medication.IndicatorGraphic;
            medicationNameLabel.AllowWrap = false;
            medicationNameLabel.ToolTip = medication.MedicationTooltip;
            medicationNameLabel.CssClass = cssClass;

            // Add each DrugLabel control
            parent.Controls.Add(medicationNameLabel);            
        }
        #endregion
    }
}
