//-----------------------------------------------------------------------
// <copyright file="MedicationLine.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>01-Feb-2007</date>
// <summary>The MedicationLine is the next level of detail up from the MedicationNameLabel. It is based around the Medication Class (the data carrier)</summary>
// <remarks>In addition to the Simple and Standard Display modes, the MedicationLine also supports an additional hidden mode:
// by default the MedicationLine will display within a self-contained table, however if it is contained with the MedicationGrid, the grid
// will generated the row tags and the MedicationLine will only be responsible for generating the cells.</remarks>
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
    using System.Globalization;
    using System.Diagnostics.CodeAnalysis;
    #endregion

    /// <summary>
    /// The MedicationLine controls and enforces complex formatting rules for the display of Virtual Theraputic Moeity (VTM),
    /// Virtual Medical Product (VMP) and Actual Medical Product into elements which can be correctly formatted for display in the MedicationLine
    /// and MedicationGrid controls.  This allows the ISV to construct complex VTM entries, while allowing the actual drug 
    /// name to have a separate style associated with it.    
    /// </summary>
    [ParseChildren(true)]
    [PersistChildren(false)]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Prevent)]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Prevent)]
    [DefaultEvent("Click")]
    [DefaultProperty("MedicationNames")]
    public class MedicationLine : WebControl, INamingContainer, INotifyPropertyChanged, IPostBackEventHandler
    {
        #region Property Vars
        /// <summary>
        /// Medication Information
        /// </summary>
        private Medication medication = new Medication();

        /// <summary>
        /// Medication Line Extender
        /// </summary>
        private MedicationLineExtender medicationLineExtender = new MedicationLineExtender();

        /// <summary>
        /// Render a containing Table. For standalone MedicationLine this is preferred, however if inside a MedicationGrid or other table, then set to false
        /// </summary>
        private bool renderTable = true;
        #endregion

        #region Control Vars
        /// <summary>
        /// Start Date TableCell
        /// </summary>
        private HtmlTableCell startDateCell;

        /// <summary>
        /// Indicator Graphics Table Cell
        /// </summary>
        private HtmlTableCell indicatorGraphicCell;

        /// <summary>
        /// Critical Alert Graphics Table Cell
        /// </summary>
        private HtmlTableCell criticalAlertGraphicCell;

        /// <summary>
        /// Drug Details Table Cell
        /// </summary>
        private HtmlTableCell drugDetailsCell;

        /// <summary>
        /// Reason Table Cell
        /// </summary>
        private HtmlTableCell reasonCell;

        /// <summary>
        /// Status Table Cell
        /// </summary>
        private HtmlTableCell statusCell;

        /// <summary>
        /// Status Table Div
        /// </summary>
        private HtmlGenericControl statusDiv;

        /// <summary>
        /// Start Date Label
        /// </summary>
        private DateLabel startDateLabel;

        /// <summary>
        /// Status Date Label
        /// </summary>
        private DateLabel statusDateLabel;

        /// <summary>
        /// Indcoator Graphic
        /// </summary>
        private System.Web.UI.WebControls.Image indicatorGraphic;

        /// <summary>
        /// Crticial Alert Graphic
        /// </summary>
        private System.Web.UI.WebControls.Image criticalAlertGraphic;

        /// <summary>
        /// Medication Name Label
        /// </summary>
        private MedicationNameLabel medLabel;

        /// <summary>
        /// Dosage Text
        /// </summary>
        private HtmlGenericControl dosageText;

        /// <summary>
        /// Reason Text
        /// </summary>
        private HtmlGenericControl reasonText;

        /// <summary>
        /// Status Space - displayed if Status Date and Status are displayed
        /// </summary>
        private HtmlGenericControl statusSpacer;

        /// <summary>
        /// Drug Space - displayed if Dosage Text is displayed
        /// </summary>
        private HtmlGenericControl dosageSpacer;

        /// <summary>
        /// Status Text
        /// </summary>
        private HtmlGenericControl statusText;

        /// <summary>
        /// Provide Extender
        /// </summary>
        private bool enableExtender = true;

        /// <summary>
        /// Flag to indicate of show rules should be applied. Disabled during updating of SimpleMode
        /// </summary>
        private bool applyShowRules = true;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a MedicationLine object and sets the MedicationLine properties to their default values. 
        /// </summary>
        public MedicationLine()
        {
        }

        /// <summary>
        /// Constructs a MedicationLine object and sets the Medication properties to their default value.  
        /// </summary>
        /// <param name="medication">The medication to display in the line. </param>
        internal MedicationLine(Medication medication)
        {
            this.medicationLineExtender.Medication = medication;
        }
        #endregion

        #region INotifyPropertyChanged Members
        /// <summary>
        /// Occurs when a property value changes. 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Events
        ///<summary>
        /// Occurs when the user clicks the MedicationLine. 
        /// </summary>
        /// <returns>
        /// The MedicationLine as the value in the sender parameter. 
        /// </returns>
        public event EventHandler Click;

        /// <summary>
        /// Occurs when the user double-clicks the MedicationLine. 
        /// </summary>
        /// <returns>
        /// The MedicationLine as the value in the sender parameter. 
        /// </returns>
        public event EventHandler DoubleClick;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the value to show or hide the <see cref="P:NhsCui.Toolkit.Web.MedicationLine.DosageText">DosageText</see>
        /// or the <see cref="P:NhsCui.Toolkit.Web.MedicationLine.Dose">Dose</see>, 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationLine.Form">Form</see>, 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationLine.Frequency">Frequency</see> and 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationLine.Route">Route</see> information. 
        /// </summary>
        /// <remarks>
        /// Defaults to true. Available on the client-side and the server-side. 
        /// </remarks>
        [Bindable(true), Category("Appearance")]        
        [Description("Show or hide the dosage text or Dose/Form/Frequency and Route Information")]
        [RefreshProperties(RefreshProperties.All)]
        [NotifyParentProperty(true)]
        public bool ShowDosageDetails
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.ShowDosageDetails;
            }

            set
            {
                if (MedicationLine.HasPropertyChanged(this.medicationLineExtender.ShowDosageDetails, value))
                {
                    this.EnsureChildControls();
                    this.medicationLineExtender.ShowDosageDetails = value;
                    this.ApplyShowRules();
                    this.NotifyPropertyChanged("ShowDosageDetails");                    
                }
            }
        }

        /// <summary>
        /// Gets or sets the display of the indicator and alert graphics. 
        /// </summary>
        /// <remarks>
        /// Defaults to false. Available on the client-side and the server-side. 
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [Description("Show or hide the indicator and alert graphics")]
        [RefreshProperties(RefreshProperties.All)]
        [NotifyParentProperty(true)]
        public bool ShowGraphics
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.ShowGraphics;
            }

            set
            {
                if (MedicationLine.HasPropertyChanged(this.medicationLineExtender.ShowGraphics, value))
                {
                    this.EnsureChildControls();
                    this.medicationLineExtender.ShowGraphics = value;
                    this.ApplyShowRules();
                    this.NotifyPropertyChanged("ShowGraphics");                    
                }
            }
        }

        /// <summary>
        /// Gets or sets the value to show or hide the <see cref="P:NhsCui.Toolkit.Web.MedicationLine.Reason">Reason</see> on the line. 
        /// </summary>
        /// <remarks>
        /// Defaults to true. Available on the client-side and the server-side. 
        /// </remarks>
        [Bindable(true), Category("Appearance")]        
        [Description("Show or hide the Reason on the line")]
        [RefreshProperties(RefreshProperties.All)]
        [NotifyParentProperty(true)]
        public bool ShowReason
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.ShowReason;                
            }

            set
            {
                if (MedicationLine.HasPropertyChanged(this.medicationLineExtender.ShowReason, value))
                {
                    this.EnsureChildControls();
                    this.medicationLineExtender.ShowReason = value;
                    this.NotifyPropertyChanged("ShowReason");
                    this.ApplyShowRules();
                }
            }
        }

        /// <summary>
        /// Gets or sets the value to show or hide the <see cref="P:NhsCui.Toolkit.Web.MedicationLine.StatusDate">StatusDate</see>.
        /// </summary>
        /// <remarks>
        /// Defaults to false. Available on the client-side and the server-side.
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [Description("Show or hide the StatusDate on the line")]
        [RefreshProperties(RefreshProperties.All)]
        [NotifyParentProperty(true)]
        public bool ShowStatusDate
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.ShowStatusDate;
            }

            set
            {
                if (MedicationLine.HasPropertyChanged(this.medicationLineExtender.ShowStatusDate, value))
                {
                    this.EnsureChildControls();
                    this.medicationLineExtender.ShowStatusDate = value;
                    this.ApplyShowRules();
                    this.NotifyPropertyChanged("ShowStatusDate");                    
                }
            }
        }

        /// <summary>
        /// Gets or sets the value to show or hide the <see cref="P:NhsCui.Toolkit.Web.MedicationLine.Status">Status</see>. 
        /// </summary>
        /// <remarks>
        /// Defaults to true. Available on the client-side and the server-side. 
        /// </remarks>
        [Bindable(true), Category("Appearance")]        
        [Description("Show or hide the Status")]
        [RefreshProperties(RefreshProperties.All)]
        [NotifyParentProperty(true)]
        public bool ShowStatus
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.ShowStatus;
            }

            set
            {
                if (MedicationLine.HasPropertyChanged(this.medicationLineExtender.ShowStatus, value))
                {
                    this.EnsureChildControls();
                    this.medicationLineExtender.ShowStatus = value;
                    this.ApplyShowRules();
                    this.NotifyPropertyChanged("ShowStatus");                    
                }
            }
        }

        /// <summary>
        /// Gets or sets the display mode of the medication line.
        /// </summary>
        /// <remarks>
        /// Defaults to false. If SimpleMode is set to true, <see cref="P:NhsCui.Toolkit.Web.MedicationLine.ShowDosageDetails">ShowDosageDetails</see>,
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationLine.ShowGraphics">ShowGraphics</see>, 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationLine.ShowReason">ShowReason</see>, 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationLine.ShowStatusDate">ShowStatusDate</see> and
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationLine.ShowStatus">ShowStatus</see> are all set to false. 
        /// If any of these properties is then set to true, 
        /// SimpleMode is set to false. Available on the client-side and the server-side. 
        /// </remarks>
        [Bindable(true), Category("Behavior"), DefaultValue(false)]
        [Description("Display mode of the medication Line")]
        [RefreshProperties(RefreshProperties.All)]
        [NotifyParentProperty(true)]
        public bool SimpleMode
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.SimpleMode;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationLineExtender.SimpleMode = value;

                if (this.medicationLineExtender.SimpleMode)
                {
                    this.applyShowRules = false;
                    this.PublishDependentComponentChanges<bool>("ShowStatus", false);
                    this.medicationLineExtender.ShowStatus = false;

                    this.PublishDependentComponentChanges<bool>("ShowStatusDate", false);
                    this.medicationLineExtender.ShowStatusDate = false;

                    this.PublishDependentComponentChanges<bool>("ShowDosageDetails", false);
                    this.medicationLineExtender.ShowDosageDetails = false;

                    this.PublishDependentComponentChanges<bool>("ShowGraphics", false);
                    this.medicationLineExtender.ShowGraphics = false;

                    this.PublishDependentComponentChanges<bool>("ShowReason", false);
                    this.medicationLineExtender.ShowReason = false;
                    this.applyShowRules = true;
                }

                this.NotifyPropertyChanged("SimpleMode");
            }
        }

        /// <summary>
        /// Gets or sets the URL of the graphic used for critical alerts. 
        /// </summary>
        /// <remarks>
        /// Surfaces the MedicationNameLabel. Available on the server-side only. 
        /// </remarks>
        [Bindable(true), Category("MedicationDetails"), DefaultValue("")]
        [Localizable(true)]
        [Description("URL of the Graphic used for the critical alerts.")]
        [Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
        [UrlProperty()]
        public string CriticalAlertGraphic
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.Medication.CriticalAlertGraphic;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationLineExtender.Medication.CriticalAlertGraphic = value;
                this.NotifyPropertyChanged("CriticalAlertGraphic");                    
            }
        }

        /// <summary>
        /// Gets or sets the URL of the graphic to be displayed in the indicator section of the MedicationLine. 
        /// </summary>
        /// <remarks>
        /// Surfaces the MedicationNameLabel. Available on the server-side only. 
        /// </remarks>
        [Bindable(true), Category("MedicationDetails"), DefaultValue("")]
        [Localizable(true)]
        [Description("URL of the Graphic to display in the indicator section of the medication line")]
        [Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
        [UrlProperty()]
        public string IndicatorGraphic
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.Medication.IndicatorGraphic;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationLineExtender.Medication.IndicatorGraphic = value;
                this.NotifyPropertyChanged("IndicatorGraphic");                    
            }
        }

        /// <summary>
        /// Gets or sets the value telling the line that it is selected. 
        /// </summary>
        /// <remarks>
        /// Defaults to false. Available on the client-side and the server-side. 
        /// </remarks>
        [Bindable(true), Category("Behavior"), DefaultValue("")]        
        [Description("Value telling that the line is selected")]
        public bool IsSelected
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.Medication.IsSelected;                
            }

            set
            {
                this.EnsureChildControls();
                this.medicationLineExtender.Medication.IsSelected = value;
            }
        }

        /// <summary>
        /// Gets or sets the dose of the medication. 
        /// </summary>
        /// <remarks>
        /// Available on the server-side only. 
        /// </remarks>
        [Bindable(true), Category("MedicationDetails"), DefaultValue("")]
        [Localizable(true)]
        [Description("URL of the Graphic to display in the indciator section of the medication line")]
        public string Dose
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.Medication.Dose;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationLineExtender.Medication.Dose = value;
                this.NotifyPropertyChanged("Dose");                    
            }
        }

        /// <summary>
        /// Gets or sets the textual description of the dosage for the medication. 
        /// </summary>
        /// <remarks>
        /// The textual description is not displayed if a non-empty value is entered in <see cref="P:NhsCui.Toolkit.Web.MedicationLine.DosageText">DosageText</see>
        /// or as the <see cref="P:NhsCui.Toolkit.Web.MedicationLine.Dose">Dose</see>.  Available on the server-side only.
        /// </remarks>
        [Bindable(true), Category("MedicationDetails"), DefaultValue("")]
        [Localizable(true)]
        [Description("The dose of the medication")]
        public string DosageText
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.Medication.DosageText;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationLineExtender.Medication.DosageText = value;
                this.NotifyPropertyChanged("DosageText");
            }
        }

        /// <summary>
        /// Gets or sets the form in which the medication is delivered. 
        /// </summary>
        [Bindable(true), Category("MedicationDetails"), DefaultValue("")]
        [Localizable(true)]
        [Description("Form that the medication is delivered")]
        public string Form
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.Medication.Form;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationLineExtender.Medication.Form = value;
                this.NotifyPropertyChanged("Form");
            }
        }

        /// <summary>
        /// Gets or sets the frequency with which the medication should be taken. 
        /// </summary>
        /// <remarks>
        /// Available on the server-side only. 
        /// </remarks>
        [Bindable(true), Category("MedicationDetails"), DefaultValue("")]
        [Localizable(true)]
        [Description("Frequency to administer the medication")]
        public string Frequency
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.Medication.Frequency;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationLineExtender.Medication.Frequency = value;
                this.NotifyPropertyChanged("Frequency");
            }
        }

        /// <summary>
        /// Gets or sets the list of MedicationName records to be formatted into the drug name. 
        /// </summary>   
        /// <remarks>
        /// Defaults to a null value. Available on the server-side only. 
        /// </remarks>
        [Category("MedicationDetails"), DefaultValue("")]
        [Bindable(true)]
        [Localizable(true)]
        [Description("List of MedicationName records to be formatted into the drug name")]
        [NotifyParentProperty(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public MedicationNameCollection MedicationNames
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.Medication.MedicationNames;
            }
        }

        /// <summary>
        /// Gets or sets the Reason field. 
        /// </summary>
        /// <remarks>
        /// Available on the server-side only. 
        /// </remarks>
        [Bindable(true), Category("MedicationDetails"), DefaultValue("")]
        [Localizable(true)]
        [Description("Reason for medication")]
        public string Reason
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.Medication.Reason;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationLineExtender.Medication.Reason = value;
                this.NotifyPropertyChanged("Reason");
            }
        }

        /// <summary>
        /// Gets or sets the method by which the medication is delivered.
        /// </summary>
        /// <remarks>
        /// Available on the server-side only. 
        /// </remarks>
        [Bindable(true), Category("MedicationDetails"), DefaultValue("")]
        [Localizable(true)]
        [Description("Method by which the medication is delivered")]
        public string Route
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.Medication.Route;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationLineExtender.Medication.Route = value;
                this.NotifyPropertyChanged("Route");
            }
        }

        /// <summary>
        /// Gets or sets the significant date displayed in the MedicationLine. 
        /// </summary>
        /// <remarks>
        /// Available on the client-side and the server-side. 
        /// </remarks>
        [Bindable(true), Category("MedicationDetails"), DefaultValue("")]
        [Localizable(true)]
        [Description("Significant date displayed in the medications line")]
        public DateTime StartDate
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.Medication.StartDate;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationLineExtender.Medication.StartDate = value;
                this.NotifyPropertyChanged("StartDate");
            }
        }

        /// <summary>
        /// Gets or sets the specific keyword to be displayed for the line's status. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Status". Available on the client-side and the server-side. 
        /// </remarks>
        [Bindable(true), Category("MedicationDetails")]
        [Localizable(true)]
        [Description("Specific keyword to be displayed for the line's status")]
        public MedicationStatus Status
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.Medication.Status;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationLineExtender.Medication.Status = value;
                this.NotifyPropertyChanged("Status");
            }
        }

        /// <summary>
        /// Gets or sets the date associated with the <see cref="P:NhsCui.Toolkit.Web.MedicationLine.Status">Status</see>. 
        /// </summary>
        /// <remarks>
        /// Defaults to DateTime.Now. Available on the client-side. 
        /// </remarks>
        [Bindable(true), Category("MedicationDetails"), DefaultValue("")]
        [Localizable(true)]
        [Description("Date associated with the status")]
        public DateTime StatusDate
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.Medication.StatusDate;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationLineExtender.Medication.StatusDate = value;
                this.NotifyPropertyChanged("StatusDate");
            }
        }

        /// <summary>
        /// Override ID Property to ensure that the ID of the Extender always follows that of the control 
        /// </summary>
        public override string ID
        {
            get
            {
                return base.ID;
            }

            set
            {
                base.ID = value;
                EnsureChildControls();
                this.medicationLineExtender.ID = value + "_Extender";
                this.medicationLineExtender.TargetControlID = value;
            }
        }

        /// <summary>
        /// A JavaScript function which invokes an OnClick event on the client. Occurs when the user clicks the MedicationLine 
        /// for subscriptions on the client-side. 
        /// </summary>
        /// <remarks>
        /// OnClientClick is actually a behaviour event rather than a behaviour property. It has been specified here as a property
        /// so the ExtenderBase writes it to the XML script and ASP.NET AJAX hooks up the event
        /// properly. 
        /// 
        /// </remarks>
        [Bindable(false), Category("Behavior"), DefaultValue(null)]
        [Description("Client script function to handle On Click event")]
        public string OnClientClick
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.OnClientClick;
            }

            set
            {
                if (MedicationLine.HasPropertyChanged(this.medicationLineExtender.OnClientClick, value))
                {
                    this.EnsureChildControls();
                    this.medicationLineExtender.OnClientClick = value;
                    this.NotifyPropertyChanged("OnClientClick");
                }
            }
        }

        /// <summary>
        /// A JavaScript function which invokes an OnClick event on the client. Occurs when the user double-clicks the MedicationLine 
        /// for subscriptions on the client-side. 
        /// </summary>
        /// <remarks>
        /// OnClientClick is actually a behaviour event rather than a behaviour property. It has been specified here as a property
        /// so the ExtenderBase writes it to the XML script and ASP.NET AJAX hooks up the event
        /// properly. 
        /// </remarks>
        [Bindable(false), Category("Behavior"), DefaultValue(null)]
        [Description("Client script function to handle OnDouble Click event")]
        public string OnClientDoubleClick
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.OnClientDoubleClick;
            }

            set
            {
                if (MedicationLine.HasPropertyChanged(this.medicationLineExtender.OnClientDoubleClick, value))
                {
                    this.EnsureChildControls();
                    this.medicationLineExtender.OnClientDoubleClick = value;
                    this.NotifyPropertyChanged("OnClientDoubleClick");
                }
            }
        }

        /// <summary>
        /// A JavaScript function which invokes an OnClick event on the client. Occurs when the user right-clicks the MedicationLine 
        /// for subscriptions on the client-side. 
        /// </summary>
        /// <remarks>
        /// OnClientClick is actually a behaviour event rather than a behaviour property. It has been specified here as a property
        /// so the ExtenderBase writes it to the XML script and ASP.NET AJAX hooks up the event
        /// properly.
        /// </remarks>
        [Bindable(false), Category("Behavior"), DefaultValue(null)]
        [Description("Client script function to handle On Right Click event")]
        public string OnClientRightClick
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationLineExtender.OnClientRightClick;
            }

            set
            {
                if (MedicationLine.HasPropertyChanged(this.medicationLineExtender.OnClientRightClick, value))
                {
                    this.EnsureChildControls();
                    this.medicationLineExtender.OnClientRightClick = value;
                    this.NotifyPropertyChanged("OnClientRightClick");
                }
            }
        }

        /// <summary>
        /// Get/Set if the extender control is included.  This is not required if the MedicationLine is bound within a MedicationGrid
        /// </summary>
        [Browsable(false)]
        internal bool EnableExtender
        {
            get
            {
                this.EnsureChildControls();
                return this.enableExtender;
            }

            set
            {
                this.EnsureChildControls();

                this.enableExtender = value;
                if (this.medLabel != null)
                {
                    this.medLabel.EnableExtender = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the RenderTable flag. For standalone MedicationLine this is preferred, however if inside a MedicationGrid or other table, then set to false
        /// </summary>
        [Description("Render containing table tag")]
        [Browsable(false)]
        internal bool RenderTable
        {
            get
            {
                this.EnsureChildControls();
                return this.renderTable;
            }

            set
            {
                this.EnsureChildControls();
                this.renderTable = value;
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// The RenderBegin tag.
        /// </summary>
        /// <remarks>
        /// If the MedicationLine is contained within the MedicationGrid, the grid
        /// will generate the row tags, with the MedicationLine only responsible for generating the cells. 
        /// </remarks>
        /// <param name="writer">The HtmlTextWriter to render the contents to.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            if (writer != null)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID); 

                // Is the Medication Line inside a medication Grid, do not render the beginning table tags
                if (this.renderTable)
                {
                    writer.AddAttribute("class", this.CssClass);
                    writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                    writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                    writer.RenderBeginTag(HtmlTextWriterTag.Table);
                    writer.AddAttribute("class", this.GetCssDecoration());
                }
                else
                {
                    writer.AddAttribute("class", string.Concat(this.CssClass, " ", this.GetCssDecoration()));
                }
                
                writer.AddAttribute(HtmlTextWriterAttribute.Title, this.ToolTip); 
                writer.AddAttribute("isSelected", this.IsSelected ? "true" : "false");
                writer.AddAttribute("medicationId", this.medicationLineExtender.Medication.MedicationID); 
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            }
        }
        
        /// <summary>
        /// The RenderEnd tag. 
        /// </summary>
        /// <remarks>
        /// If the MedicationLine is contained within the MedicationGrid, the grid
        /// will generate the row tags, with the MedicationLine only responsible for generating the cells. 
        /// </remarks>
        /// <param name="writer">The HtmlTextWriter to render the contents to. </param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            if (writer != null)
            {
                writer.RenderEndTag();

                if (this.renderTable)
                {
                    writer.RenderEndTag();
                }
            }
        }
        #endregion

        #region IPostBackEventHandler Members
        /// <summary>
        /// Raises a postback event when a medication line is clicked. 
        /// </summary>
        /// <param name="eventArgument">The event arguments passed from the client. </param>
        public void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument != null)
            {
                EventArgs args = new EventArgs();
                if (eventArgument.Equals("1", StringComparison.Ordinal))
                {
                    this.OnDoubleClick(args);
                }

                if (eventArgument.Equals("0", StringComparison.Ordinal))
                {
                    this.OnClick(args);
                }
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Ensure ChildControls is not called if ParseChild is true - implicitly call at this point
        /// </summary>
        /// <param name="writer">HtmlTextWriter to render contents to</param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.DesignMode)
            {                
                this.ConfigureControls();
            }

            base.Render(writer);
        }

        /// <summary>
        /// Raise property changed event
        /// </summary>
        /// <param name="e">event args</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// On Click
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected virtual void OnClick(EventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }

        /// <summary>
        /// On Double Click
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected virtual void OnDoubleClick(EventArgs e)
        {
            if (this.DoubleClick != null)
            {
                this.DoubleClick(this, e);
            }
        }

        /// <summary>
        /// On PreRender, transfer local copy of the properties into the extender
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnPreRender(EventArgs e)
        {
            if (!this.DesignMode)
            {
                if (this.EnableExtender)
                {
                    this.EnsureID();
                    this.medicationLineExtender.ID = this.ID + "_Extender";
                    this.medicationLineExtender.TargetControlID = this.ID;
                    this.medicationLineExtender.Medication = this.medicationLineExtender.Medication;
                    this.medicationLineExtender.CallbackID = this.UniqueID;
                    this.medicationLineExtender.ClickPostBack = this.Click != null;
                    this.medicationLineExtender.DoubleClickPostBack = this.DoubleClick != null;
                }
                else
                {
                    this.medicationLineExtender.Enabled = false;                    
                }

                this.AddChildControls();
                this.ConfigureControls();
            }

            base.OnPreRender(e);
        }

        /// <summary>
        /// Create Extender as a child control
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            if (!this.DesignMode)
            {
                this.Controls.Add(this.medicationLineExtender);
            }            
        }

        /// <summary>
        /// Get property from View State
        /// </summary>
        /// <typeparam name="V">Property Type</typeparam>
        /// <param name="propertyName">PropertyName</param>
        /// <param name="nullValue">Default value if null</param>
        /// <returns>Property, or default value if not found</returns>
        protected V GetPropertyValue<V>(string propertyName, V nullValue)
        {
            if (ViewState[propertyName] == null)
            {
                return nullValue;
            }

            return (V)ViewState[propertyName];
        }

        /// <summary>
        /// Set Property to ViewState
        /// </summary>
        /// <typeparam name="V">Property Type</typeparam>
        /// <param name="propertyName">Property Name</param>
        /// <param name="value">Value to set</param>
        protected void SetPropertyValue<V>(string propertyName, V value)
        {
            object originalValue = this.ViewState[propertyName];

            // if the value has changed raise property changed event
            if ((originalValue == null && value != null) || !value.Equals(originalValue))
            {
                this.ViewState[propertyName] = value;
                this.NotifyPropertyChanged(propertyName);
            }
        }
        #endregion

        #region Private Static Methods
        /// <summary>
        /// Check if a property has changed value 
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="originalValue">Original Value</param>
        /// <param name="newValue">New Value</param>
        /// <returns>True if the property has changed</returns>
        private static bool HasPropertyChanged<T>(T originalValue, T newValue)
        {
            return (originalValue == null && newValue != null) || (originalValue != null && newValue == null) || (newValue != null && !newValue.Equals(originalValue));
        }

        /// <summary>
        /// Helper method to add a new column cell.
        /// </summary>
        /// <param name="id">ID of control</param>
        /// <param name="wrap">Wrap text flag</param>
        /// <param name="verticalAlign">Vertical alignment setting</param>
        /// <param name="horizontalAlign">Horizontaol alignment setting</param>
        /// <returns>Instance of the Column Cell</returns>
        private static HtmlTableCell AddColumn(string id, bool wrap, VerticalAlign verticalAlign, HorizontalAlign horizontalAlign)
        {
            HtmlTableCell control = new HtmlTableCell();
            control.ID = id;
            control.NoWrap = !wrap;
            control.VAlign = verticalAlign.ToString();
            control.Align = horizontalAlign.ToString();
            return control;
        }

        /// <summary>
        /// Helper method to add a new spacer text
        /// </summary>
        /// <param name="id">ID of control</param>
        /// <param name="allowWrap">Allow Wrapping of text</param>
        /// <returns>Instance of the Generic Control</returns>
        private static HtmlGenericControl AddSpacer(string id, bool allowWrap)
        {
            HtmlGenericControl spacer = new HtmlGenericControl();
            spacer.ID = id;
            if (allowWrap)
            {
                spacer.InnerHtml = "<wbr />&nbsp;";
            }
            else
            {
                spacer.InnerHtml = "&nbsp;";
            }
            
            return spacer;
        }

        /// <summary>
        /// Add Css attribute to HtmlControl
        /// </summary>
        /// <param name="control">HtmlControl to add class to</param>
        /// <param name="classname">Css Class Name</param>
        private static void AddCssClass(HtmlControl control, string classname)
        {
            if (string.IsNullOrEmpty(control.Attributes["class"]))
            {
                control.Attributes.Add("class", classname);
            }
            else
            {
                control.Attributes["class"] += " " + classname;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Generate css class names depending on the state of the line
        /// </summary>
        /// <returns>nhscui_ml_selected if the row is seelected, nhscui_ml_(status) when status is the current status of the row</returns>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Standard is to have all nhs css classes lower cased")]
        private string GetCssDecoration()
        {
            StringBuilder cssClass = new StringBuilder();
            if (this.IsSelected)
            {
                cssClass.Append("nhscui_ml_selected ");
            }

            cssClass.Append("nhscui_ml_");
            cssClass.Append(this.Status.ToString().ToLowerInvariant());
            return cssClass.ToString();
        }

        /// <summary>
        /// Add Controls. 
        /// </summary>
        /// <remarks>
        /// Child controls are used rather than rendering directly so as to reuse the DataLabel and MedicationNameLabel controls.
        /// </remarks>
        private void AddChildControls()
        {            
            // Add StartDate                   
            this.startDateCell = MedicationLine.AddColumn("strtCell", false, VerticalAlign.Top, HorizontalAlign.Center);

            // Start Date Text can never wrap
            
            MedicationLine.AddCssClass(this.startDateCell, "nhscui_ml_cell");
            MedicationLine.AddCssClass(this.startDateCell, "nhscui_ml_startdate");            
            this.Controls.Add(this.startDateCell);
            HtmlGenericControl startDateDiv = new HtmlGenericControl("div");
            startDateDiv.Style[HtmlTextWriterStyle.WhiteSpace] = "nowrap";            
            this.startDateCell.Controls.Add(startDateDiv);

            this.startDateLabel = new DateLabel();
            this.startDateLabel.ID = this.ID + "StartDateLabel";
            this.startDateLabel.DateType = NhsCui.Toolkit.DateAndTime.DateType.Exact;
            this.startDateLabel.DateValue = this.medicationLineExtender.Medication.StartDate;
            startDateDiv.Controls.Add(this.startDateLabel);

            // Add Graphics
            this.indicatorGraphicCell = MedicationLine.AddColumn("indicatorCell", false, VerticalAlign.Top, HorizontalAlign.NotSet);
            MedicationLine.AddCssClass(this.indicatorGraphicCell, "nhscui_ml_cell");
            MedicationLine.AddCssClass(this.indicatorGraphicCell, "nhscui_ml_indicator");            
            this.Controls.Add(this.indicatorGraphicCell);
            HtmlGenericControl indicatorGraphicDiv = new HtmlGenericControl("div");
            this.indicatorGraphicCell.Controls.Add(indicatorGraphicDiv);

            this.indicatorGraphic = new System.Web.UI.WebControls.Image();
            this.indicatorGraphic.ID = "ind";
            this.indicatorGraphic.AlternateText = "Indicator";
            this.indicatorGraphic.ImageUrl = this.medicationLineExtender.Medication.IndicatorGraphic;
            indicatorGraphicDiv.Controls.Add(this.indicatorGraphic);

            this.criticalAlertGraphicCell = MedicationLine.AddColumn("criticalCell", false, VerticalAlign.Top, HorizontalAlign.NotSet);
            MedicationLine.AddCssClass(this.criticalAlertGraphicCell, "nhscui_ml_cell");
            MedicationLine.AddCssClass(this.criticalAlertGraphicCell, "nhscui_ml_criticalalert");            
            this.Controls.Add(this.criticalAlertGraphicCell);
            HtmlGenericControl criticalAlertGraphicDiv = new HtmlGenericControl("div");
            this.criticalAlertGraphicCell.Controls.Add(criticalAlertGraphicDiv);

            this.criticalAlertGraphic = new System.Web.UI.WebControls.Image();
            this.criticalAlertGraphic.ID = "crit";
            this.criticalAlertGraphic.AlternateText = "Critical Alert";
            this.criticalAlertGraphic.ImageUrl = this.medicationLineExtender.Medication.CriticalAlertGraphic;
            criticalAlertGraphicDiv.Controls.Add(this.criticalAlertGraphic);

            // AddDrugDetails            
            this.drugDetailsCell = MedicationLine.AddColumn("drgCell", true, VerticalAlign.Top, HorizontalAlign.NotSet);
            MedicationLine.AddCssClass(this.drugDetailsCell, "nhscui_ml_cell");
            MedicationLine.AddCssClass(this.drugDetailsCell, "nhscui_ml_drugdetails");
            this.Controls.Add(this.drugDetailsCell);
            HtmlGenericControl drugDetailsDiv = new HtmlGenericControl("div");
            this.drugDetailsCell.Controls.Add(drugDetailsDiv);

            // MedLabel
            this.medLabel = new MedicationNameLabel();
            this.medLabel.ID = "medLabel";
            this.medLabel.CssClass = "nhscui_ml_mn";
            this.medLabel.EnableViewState = this.EnableViewState;
            this.medLabel.ShowGraphics = false;
            this.medLabel.EnableExtender = this.enableExtender;
            this.medLabel.DrugNameStyle = "nhscui_ml_mn_drugname";
            drugDetailsDiv.Controls.Add(this.medLabel);

            // Dosage Text
            this.dosageSpacer = MedicationLine.AddSpacer("dosspc", true);
            drugDetailsDiv.Controls.Add(this.dosageSpacer);

            this.dosageText = new HtmlGenericControl();
            MedicationLine.AddCssClass(this.dosageText, "nhscui_ml_dosage");
            this.dosageText.ID = "dostxt";
            drugDetailsDiv.Controls.Add(this.dosageText);

            // AddReason
            this.reasonCell = MedicationLine.AddColumn("rsCell", true, VerticalAlign.Top, HorizontalAlign.NotSet);
            MedicationLine.AddCssClass(this.reasonCell, "nhscui_ml_cell");
            MedicationLine.AddCssClass(this.reasonCell, "nhscui_ml_reason");            
            this.Controls.Add(this.reasonCell);
            HtmlGenericControl reasonDiv = new HtmlGenericControl("div");                        
            this.reasonCell.Controls.Add(reasonDiv);

            // Reason Text            
            this.reasonText = new HtmlGenericControl();
            this.reasonText.ID = "rstxt";
            reasonDiv.Controls.Add(this.reasonText);

            // AddStatus    
            this.statusCell = MedicationLine.AddColumn("stsCell", false, VerticalAlign.Top, HorizontalAlign.Right);

            // Status Text can neer wrap            
            MedicationLine.AddCssClass(this.statusCell, "nhscui_ml_cell");
            MedicationLine.AddCssClass(this.statusCell, "nhscui_ml_status");            
            this.Controls.Add(this.statusCell);
            this.statusDiv = new HtmlGenericControl("div");
            this.statusDiv.Style[HtmlTextWriterStyle.WhiteSpace] = "nowrap";
            this.statusDiv.Style.Add("word-break", "none");
            this.statusCell.Controls.Add(this.statusDiv);
            
            this.statusDateLabel = new DateLabel();
            this.statusDateLabel.CssClass = "nhscui_ml_statusdate";
            this.statusDateLabel.DateType = NhsCui.Toolkit.DateAndTime.DateType.Exact;
            this.statusDateLabel.ID = "statusDate";
            this.statusDiv.Controls.Add(this.statusDateLabel);

            this.statusSpacer = MedicationLine.AddSpacer("stsspc", false);
            this.statusDiv.Controls.Add(this.statusSpacer);

            // Status Text
            this.statusText = new HtmlGenericControl();
            MedicationLine.AddCssClass(this.statusText, "nhscui_ml_statustext");
            this.statusText.ID = "ststxt";
            this.statusDiv.Controls.Add(this.statusText);
        }

        /// <summary>
        /// Configure Control Settings.
        /// </summary>
        private void ConfigureControls()
        {
            // Readd Controls if not already instantiated
            if (this.startDateCell == null)
            {
                this.AddChildControls();
            }

            this.startDateLabel.DateValue = this.medicationLineExtender.Medication.StartDate;            

            // AddGraphics
            // If the Url is empty, do not set the ImageUrl. However the cell should still be visible to allow for correct alignment
            if (string.IsNullOrEmpty(this.CriticalAlertGraphic))
            {
                this.criticalAlertGraphic.Visible = false;
            }
            else
            {                
                this.criticalAlertGraphic.ImageUrl = this.CriticalAlertGraphic;                    
            }

            // If the Url is empty, do not set the ImageUrl. However the cell should still be visible to allow for corret alignment
            if (string.IsNullOrEmpty(this.IndicatorGraphic))
            {                
                this.indicatorGraphic.Visible = false;
            }
            else
            {
                this.indicatorGraphic.ImageUrl = this.IndicatorGraphic;                    
            }

            this.criticalAlertGraphicCell.Style[HtmlTextWriterStyle.Display] = this.ShowGraphics ? "" : "none";
            this.indicatorGraphicCell.Style[HtmlTextWriterStyle.Display] = this.ShowGraphics ? "" : "none";

            // Dosage Text         
            if (this.DesignMode)
            {
                this.dosageSpacer.Visible = this.ShowDosageDetails;
                this.dosageText.Visible = this.ShowDosageDetails;
            }
            else
            {
                this.dosageSpacer.Style[HtmlTextWriterStyle.Display] = this.ShowDosageDetails ? "" : "none";
                this.dosageText.Style[HtmlTextWriterStyle.Display] = this.ShowDosageDetails ? "" : "none";
            }

            this.dosageText.InnerText = AntiXss.HtmlEncode(this.medicationLineExtender.Medication.GetDosage());

            // AddReason            
            this.reasonCell.Style[HtmlTextWriterStyle.Display] = this.ShowReason ? "" : "none";

            if (string.IsNullOrEmpty(this.medicationLineExtender.Medication.Reason) == false)
            {
                this.reasonText.InnerText = AntiXss.HtmlEncode(Medication.LimitStringLength(this.medicationLineExtender.Medication.Reason));
            }

            // AddStatus    
            this.statusDateLabel.DateValue = this.StatusDate;
            this.statusText.InnerText = this.Status.ToString();
            bool showStatus = this.ShowStatus || this.ShowStatusDate;
            bool showSpacer = this.ShowStatus && this.ShowStatusDate;

            if (this.DesignMode)
            {
                this.statusCell.Visible = showStatus;
                this.statusDateLabel.Visible = this.ShowStatusDate;
                this.statusSpacer.Visible = showSpacer;
                this.statusText.Visible = this.ShowStatus;
            }
            else
            {
                this.statusCell.Style[HtmlTextWriterStyle.Display] = showStatus ? "" : "none";
                this.statusDateLabel.Style[HtmlTextWriterStyle.Display] = this.ShowStatusDate ? "" : "none";                
                this.statusSpacer.Style[HtmlTextWriterStyle.Display] = showSpacer ? "" : "none";
                this.statusText.Style[HtmlTextWriterStyle.Display] = this.ShowStatus ? "" : "none";
            }
            
            this.medLabel.UpdateMedicationNames(this.MedicationNames);            
        }

        /// <summary>
        /// Apply Rules that govern which show property sets is allowed
        /// 1. If Simple mode is true, all other modes must be false
        /// 2. If any of the another modes are true, then Simple Mode must be false
        /// </summary>
        private void ApplyShowRules()
        {
            if (this.applyShowRules)
            {
                // Access directly to prevent recursion
                if (this.ShowStatus || this.ShowStatusDate || this.ShowDosageDetails || this.ShowGraphics || this.ShowReason)
                {
                    this.PublishDependentComponentChanges<bool>("SimpleMode", false);
                    this.medicationLineExtender.SimpleMode = false;
                }
            }
        }

        /// <summary>
        /// Notify Collection that a property has changed
        /// </summary>
        /// <param name="parameterName">Property Name</param>
        private void NotifyPropertyChanged(string parameterName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(parameterName));
        }

        /// <summary>
        /// Publish Dependant Component Changes
        /// </summary>
        /// <typeparam name="T">Type of property value</typeparam>
        /// <param name="propertyName">Property Name that has changed</param>
        /// <param name="value">new value for Property</param>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private void PublishDependentComponentChanges<T>(string propertyName, T value)
        {
            System.ComponentModel.Design.IComponentChangeService changeService = null;
            System.ComponentModel.PropertyDescriptor property = null;
            T currentValue = default(T);

            if (DesignMode && this.Site != null)
            {
                property = System.ComponentModel.TypeDescriptor.GetProperties(this)[propertyName];
                changeService = (System.ComponentModel.Design.IComponentChangeService)this.Site.GetService(typeof(System.ComponentModel.Design.IComponentChangeService));

                if (changeService != null)
                {
                    currentValue = (T)property.GetValue(this);

                    if (currentValue == null)
                    {
                        currentValue = default(T);
                    }

                    // Trap any error and ignore it
                    changeService.OnComponentChanging(this, property);

                    // try to set new value
                    property.SetValue(this, value);                    
                    changeService.OnComponentChanged(this, property, currentValue, value);                    
                }
            }
        }
        #endregion
    }
}
