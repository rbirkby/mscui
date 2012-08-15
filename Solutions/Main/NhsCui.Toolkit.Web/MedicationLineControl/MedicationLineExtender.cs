//-----------------------------------------------------------------------
// <copyright file="MedicationLineExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>01 Feb 2007</date>
// <summary>Extender Behavior for MedicationLine</summary>
//-----------------------------------------------------------------------

[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.MedicationLineControl.MedicationLineBehavior.js", "text/javascript")]

namespace NhsCui.Toolkit.Web
{
    #region Using
    using System.ComponentModel;
    using AjaxControlToolkit;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Diagnostics.CodeAnalysis;
    using System;
    #endregion

    /// <summary>
    /// Extender Behavior for MedicationLine
    /// </summary>
    [ToolboxItem(false)]
    [RequiredScript(typeof(CommonToolkitScripts), 0)]
    [RequiredScript(typeof(CommonNhsCuiToolkitScripts), 1)]
    [RequiredScript(typeof(NhsDateScripts), 2)]    
    [TargetControlType(typeof(MedicationLine))]
    [ClientScriptResource("NhsCui.Toolkit.Web.MedicationLineBehavior", "NhsCui.Toolkit.Web.MedicationLineControl.MedicationLineBehavior.js")]    
    internal class MedicationLineExtender : ExtenderControlBase
    {
        #region Members Variables
        /// <summary>
        /// Medication
        /// </summary>
        private Medication medication = new Medication();
        #endregion
        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MedicationLineExtender()
        {
            this.EnableClientState = true;
            this.ClientStateValuesLoaded += new EventHandler(this.MedicationLineExtender_ClientStateValuesLoaded);
        }

        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets value to show or hide the dosage text or Dose/Form/Frequency and Route information
        /// </summary>        
        [ExtenderControlProperty()]
        [ClientPropertyName("showDosageDetails")]
        public bool ShowDosageDetails
        {
            get
            {
                return this.GetPropertyValue<bool>("ShowDosageDetails", true);
            }

            set
            {
                this.SetPropertyValue<bool>("ShowDosageDetails", value);
            }
        }

        /// <summary>
        /// Gets or sets the value to show or hide the indicator and alert graphics
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
        /// Gets or sets the value to show or hide the StatusDate on the line
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
        /// Gets or sets the value to show or hide the status
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
        /// Gets or sets the value to show or hide the status
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
        /// Gets or sets the display mode of the medication line
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
        /// Gets or sets the display mode of the medication line
        /// </summary>        
        public Medication Medication
        {
            get
            {
                return this.medication;
            }

            set
            {
                this.medication = value;
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
        /// Implement the LoadViewState method. Loads the medication State if saved
        /// </summary>
        /// <param name="savedState">Saved State</param>
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            Medication medication = ViewState["Medication"] as Medication;
            if (medication != null)
            {
                this.medication = medication;
            }
        }

        /// <summary>
        /// Implement the SaveViewState method. 
        /// </summary>
        /// <returns>ViewState</returns>
        protected override object SaveViewState()
        {
            ViewState.Add("Medication", this.medication);
            return base.SaveViewState();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get Client State value from the Behavior
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void MedicationLineExtender_ClientStateValuesLoaded(object sender, EventArgs e)
        {
            if (this.ClientState != null)
            {
                // If extending beyond this single property, create a ClientState object and deserialize.
                // However currently only the isSelected is passed 
                this.Medication.IsSelected = this.ClientState.Equals("true");
            }
        }
        #endregion
    }
}
