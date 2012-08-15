//---------------------------------------------------------------------------------------------------------
// <copyright file="MedicationGridExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>19-Feb-2007</date>
// <summary>Extend the MedicationGrid</summary>
//---------------------------------------------------------------------------------------------------------

[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.MedicationGridControl.MedicationGridBehavior.js", "text/javascript")]

namespace NhsCui.Toolkit.Web
{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AjaxControlToolkit;
    using System.ComponentModel;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    #endregion

    /// <summary>
    /// Extender Behavior for MedicationGrid
    /// </summary>
    [ToolboxItem(false)]
    [RequiredScript(typeof(CommonToolkitScripts), 0)]
    [RequiredScript(typeof(CommonNhsCuiToolkitScripts), 1)]
    [RequiredScript(typeof(NhsDateScripts), 2)]
    [RequiredScript(typeof(MedicationListViewExtender), 3)]
    [TargetControlType(typeof(MedicationGrid))]
    [ClientScriptResource("NhsCui.Toolkit.Web.MedicationGridBehavior", "NhsCui.Toolkit.Web.MedicationGridControl.MedicationGridBehavior.js")]    
    internal class MedicationGridExtender : ExtenderControlBase
    {
        #region Members Vars
        /// <summary>
        /// Medication Items
        /// </summary>
        private List<Medication> items = new List<Medication>();
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MedicationGridExtender()
        {
            this.EnableClientState = true;
            this.ClientStateValuesLoaded += new EventHandler(this.MedicationGridExtender_ClientStateValuesLoaded);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets Medication Lines
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Required.")]
        public List<Medication> Items
        {
            get
            {
                return this.items;
            }
        }

        /// <summary>
        /// Gets or sets Flag to indicate whether the lines are shown in SimpleMode
        /// </summary>
        [ExtenderControlProperty()]
        [ClientPropertyName("simpleMode")]
        public bool SimpleMode
        {
            get
            {
                return this.GetPropertyValue<bool>("SimpleMode", false);
            }

            set
            {
                this.SetPropertyValue<bool>("SimpleMode", value);
            }
        }

        /// <summary>
        /// Gets or sets whether the Look Ahead and Look Behind panels are displayed
        /// </summary>
        [ExtenderControlProperty()]
        [ClientPropertyName("showLookAheadBehind")]
        public bool ShowLookAheadBehind
        {
            get
            {
                return this.GetPropertyValue<bool>("ShowLookAheadBehind", false);
            }

            set
            {
                this.SetPropertyValue<bool>("ShowLookAheadBehind", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption for the drug details column
        /// </summary>
        public string DrugDetailsColumnHeaderText
        {
            get
            {
                return this.GetPropertyValue<string>("DrugDetailsColumnHeaderText", "Drug Details");
            }

            set
            {
                this.SetPropertyValue<string>("DrugDetailsColumnHeaderText", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption for the reason column
        /// </summary>
        public string ReasonColumnHeaderText
        {
            get
            {
                return this.GetPropertyValue<string>("ReasonColumnHeaderText", "Reason");
            }

            set
            {
                this.SetPropertyValue<string>("ReasonColumnHeaderText", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption of the status column
        /// </summary>
        public string StartDateColumnHeaderText
        {
            get
            {
                return this.GetPropertyValue<string>("StartDateColumnHeaderText", "Start Date");
            }

            set
            {
                this.SetPropertyValue<string>("StartDateColumnHeaderText", value);
            }
        }

        /// <summary>
        /// Gets or sets the caption of the status column
        /// </summary>
        public string StatusColumnHeaderText
        {
            get
            {
                return this.GetPropertyValue<string>("StatusColumnHeaderText", "Status");
            }

            set
            {
                this.SetPropertyValue<string>("StatusColumnHeaderText", value);
            }
        }

        /// <summary>
        /// Gets or sets the value to show dosage details for all lines
        /// </summary>
        /// <remarks>
        /// Default to true
        /// </remarks>
        [ExtenderControlProperty()]
        [ClientPropertyName("showDosageDetails")]
        public bool ShowDosageDetails
        {
            get
            {
                return this.GetPropertyValue<bool>("ShowDosageDetails", false);
            }

            set
            {
                this.SetPropertyValue<bool>("ShowDosageDetails", value);                
            }
        }

        /// <summary>
        /// Gets or sets the value to show graphics for all lines
        /// </summary>
        [ExtenderControlProperty()]
        [ClientPropertyName("showGraphics")]
        public bool ShowGraphics
        {
            get
            {
                return this.GetPropertyValue<bool>("ShowGraphics", false);
            }

            set
            {
                this.SetPropertyValue<bool>("ShowGraphics", value);                
            }
        }

        /// <summary>
        /// Gets or sets the value to show the reason for all lines
        /// </summary>
        [ExtenderControlProperty()]
        [ClientPropertyName("showReason")]
        public bool ShowReason
        {
            get
            {
                return this.GetPropertyValue<bool>("ShowReason", true);
            }

            set
            {
                this.SetPropertyValue<bool>("ShowReason", value);                
            }
        }

        /// <summary>
        /// Gets or sets the value to show the status date for all lines
        /// </summary>
        [ExtenderControlProperty()]
        [ClientPropertyName("showStatusDate")]
        public bool ShowStatusDate
        {
            get
            {
                return this.GetPropertyValue<bool>("ShowStatusDate", false);
            }

            set
            {
                this.SetPropertyValue<bool>("ShowStatusDate", value);                
            }
        }

        /// <summary>
        /// Gets or sets the value to show the status for all lines
        /// </summary>
        [ExtenderControlProperty()]
        [ClientPropertyName("showStatus")]
        public bool ShowStatus
        {
            get
            {
                return this.GetPropertyValue<bool>("ShowStatus", true);
            }

            set
            {
                this.SetPropertyValue<bool>("ShowStatus", value);                
            }
        }

        /// <summary>
        /// Gets or sets the LookBehind ControlID
        /// </summary>
        [ExtenderControlProperty()]
        [ClientPropertyName("lookbehindID")]
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member", Justification = "Using casing as per ASP.NET controls ID and ClientID")]
        public string LookBehindControlID
        {
            get
            {
                return this.GetPropertyValue<string>("LookBehindControlID", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("LookBehindControlID", value);
            }
        }

        /// <summary>
        /// Gets or sets the LookBehind ControlID
        /// </summary>
        [ExtenderControlProperty()]
        [ClientPropertyName("lookaheadID")]
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member", Justification = "Using casing as per ASP.NET controls ID and ClientID")]
        public string LookAheadControlID
        {
            get
            {
                return this.GetPropertyValue<string>("LookAheadControlID", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("LookAheadControlID", value);
            }
        }

        /// <summary>
        /// Gets or sets the start date column width
        /// </summary>
        [ExtenderControlProperty()]
        [ClientPropertyName("startDateColumnWidth")]
        public Unit StartDateColumnWidth
        {
            get
            {
                return this.GetPropertyValue<Unit>("StartDateColumnWidth", Unit.Pixel(50));
            }

            set
            {
                this.SetPropertyValue<Unit>("StartDateColumnWidth", value);
            }
        }

        /// <summary>
        /// Gets or sets the drug details column width
        /// </summary>
        [ExtenderControlProperty()]
        [ClientPropertyName("drugDetailsColumnWidth")]
        public Unit DrugDetailsColumnWidth
        {
            get
            {
                return this.GetPropertyValue<Unit>("DrugDetailsColumnWidth", Unit.Pixel(200));
            }

            set
            {
                this.SetPropertyValue<Unit>("DrugDetailsColumnWidth", value);
            }
        }

        /// <summary>
        /// Gets or sets the reason column width
        /// </summary>
        [ExtenderControlProperty()]
        [ClientPropertyName("reasonColumnWidth")]
        public Unit ReasonColumnWidth
        {
            get
            {
                return this.GetPropertyValue<Unit>("ReasonColumnWidth", Unit.Pixel(200));
            }

            set
            {
                this.SetPropertyValue<Unit>("ReasonColumnWidth", value);
            }
        }

        /// <summary>
        /// Gets or sets the status column width
        /// </summary>
        [ExtenderControlProperty()]
        [ClientPropertyName("statusColumnWidth")]
        public Unit StatusColumnWidth
        {
            get
            {
                return this.GetPropertyValue<Unit>("StatusColumnWidth", Unit.Pixel(50));
            }

            set
            {
                this.SetPropertyValue<Unit>("StatusColumnWidth", value);
            }
        }

        /// <summary>
        /// Gets or sets Click Handler
        /// </summary>        
        [DefaultValue("")]
        [ExtenderControlEvent()]
        [ClientPropertyName("click")]
        public string OnClientClick
        {
            get
            {
                return this.GetPropertyValue<string>("OnClientClick", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("OnClientClick", value);
            }
        }

        /// <summary>
        /// Gets or sets DoubleClick Handler
        /// </summary>    
        [DefaultValue("")]
        [ExtenderControlEvent()]
        [ClientPropertyName("doubleClick")]
        public string OnClientDoubleClick
        {
            get
            {
                return this.GetPropertyValue<string>("OnClientDoubleClick", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("OnClientDoubleClick", value);
            }
        }

        /// <summary>
        /// Gets or sets RightClick Handler
        /// </summary>   
        [DefaultValue("")]
        [ExtenderControlEvent()]
        [ClientPropertyName("rightClick")]
        public string OnClientRightClick
        {
            get
            {
                return this.GetPropertyValue<string>("OnClientRightClick", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("OnClientRightClick", value);
            }
        }

        /// <summary>
        /// Gets or sets the CallBack ID for Postbacks
        /// </summary>        
        [ExtenderControlProperty()]
        [ClientPropertyName("callbackID")]
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member", Justification = "As per UniqueID, ClientID")]
        public string CallbackID
        {
            get
            {
                return this.GetPropertyValue<string>("callbackID", string.Empty);
            }

            set
            {
                this.SetPropertyValue<string>("callbackID", value);
            }
        }

        /// <summary>
        /// Gets or sets Click PostBack Flag
        /// </summary>        
        [ExtenderControlProperty()]
        [ClientPropertyName("clickPostBack")]
        public bool ClickPostBack
        {
            get
            {
                return this.GetPropertyValue<bool>("ClickPostBack", false);
            }

            set
            {
                this.SetPropertyValue<bool>("ClickPostBack", value);
            }
        }

        /// <summary>
        /// Gets or sets DoubleClick PostBack Flag
        /// </summary>        
        [ExtenderControlProperty()]
        [ClientPropertyName("dblclickPostBack")]
        public bool DoubleClickPostBack
        {
            get
            {
                return this.GetPropertyValue<bool>("DoubleClickPostBack", false);
            }

            set
            {
                this.SetPropertyValue<bool>("DoubleClickPostBack", value);
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Implement the LoadViewState method. Loads the MedicationNames if saved
        /// </summary>
        /// <param name="savedState">Saved State</param>
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            List<Medication> items = ViewState["items"] as List<Medication>;

            if (items != null)
            {
                this.items = items;
            }
        }

        /// <summary>
        /// Implement the SaveViewState method.
        /// </summary>
        /// <returns>ViewState</returns>
        protected override object SaveViewState()
        {
            if (this.items != null && this.items.Count > 0)
            {
                ViewState.Add("items", this.items);
            }

            return base.SaveViewState();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get Client State value from the Behavior
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void MedicationGridExtender_ClientStateValuesLoaded(object sender, EventArgs e)
        {
            if (this.ClientState != null)
            {
                this.SetSelectedItems(this.ClientState);
            }
        }

        /// <summary>
        /// Set Selected Items
        /// </summary>
        /// <param name="clientState">Client State</param>
        private void SetSelectedItems(string clientState)
        {
            string[] idlist = clientState.Split(',');

            this.items.ForEach(delegate(Medication medication)
            {
                bool exists = Array.Exists(idlist, delegate(string id) { return id.Equals(medication.MedicationID); });
                medication.IsSelected = exists;
            });
        }
        #endregion
    }
}