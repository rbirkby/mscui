//-----------------------------------------------------------------------
// <copyright file="MedicationGrid.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The MedicationGrid displays a collection of Medication objects and allows these objects to be sorted using exposed properties.</summary>
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
    using System.Globalization;
    #endregion

    /// <summary>The MedicationGrid displays a collection of Medication objects and allows these objects to be sorted using exposed properties.
    /// </summary>
    [ParseChildren(true)]
    [PersistChildren(false)]
    [ToolboxItemFilterAttribute("System.Web.UI", ToolboxItemFilterType.Prevent)]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Prevent)]
    [DefaultEvent("Click")]
    [DefaultProperty("Items")]
    public class MedicationGrid : WebControl, INamingContainer, INotifyPropertyChanged, IPostBackEventHandler
    {
        #region Member Vars
        /// <summary>
        /// Medication Grid Extendre
        /// </summary>
        private MedicationGridExtender medicationGridExtender = new MedicationGridExtender();

        /// <summary>
        /// Look Behind Medication Name List View
        /// </summary>
        private MedicationListView lookBehindListView;

        /// <summary>
        /// Look Ahead Medication Name List View
        /// </summary>
        private MedicationListView lookAheadListView;

        /// <summary>
        /// Flag to indicate of show rules should be applied. Disabled during updating of SimpleMode
        /// </summary>
        private bool applyShowRules = true;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a MedicationGrid object.
        /// </summary>
        public MedicationGrid() : base(HtmlTextWriterTag.Div)
        {
            this.Height = Unit.Pixel(200);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when the user clicks on an individual line.
        /// </summary>
        public event EventHandler<MedicationEventArgs> Click;

        /// <summary>
        /// Occurs when the user double-clicks on the grid surface. 
        /// </summary>
        public event EventHandler<MedicationEventArgs> DoubleClick;         
        #endregion

        #region INotifyPropertyChanged Members
        /// <summary>
        /// Occurs when a property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the collection of MedicationLines. 
        /// </summary>
        /// <remarks>
        /// Defaults to a null value. Available on the server-side only. 
        /// </remarks>
        [Category("MedicationDetails"), DefaultValue(null)]        
        [Description("Medication Lines")]
        [NotifyParentProperty(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Required.")]
        public List<Medication> Items
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.Items;
            }
        }

        /// <summary>
        /// Gets or sets the MedicationGrid display mode. 
        /// </summary>
        /// <remarks> 
        /// Defaults to false which means the lines are not displayed. If SimpleMode is set to true, 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationGrid.ShowDosageDetails">ShowDosageDetails</see>,
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationLine.ShowGraphics">ShowGraphics</see>, 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationLine.ShowReason">ShowReason</see>, 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationLine.ShowStatusDate">ShowStatusDate</see> and
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationLine.ShowStatus">ShowStatus</see> are all set to false. If any of these properties is 
        /// then set to true, 
        /// SimpleMode is set to false. Available on the client-side and the server-side. 
        /// </remarks>
        [Bindable(false), Category("Behavior"), DefaultValue(false)]
        [Localizable(true)]
        [Description("Flag to indcate whether the lines are shown in SimpleMode")]
        public bool SimpleMode
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.SimpleMode;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationGridExtender.SimpleMode = value;

                if (this.medicationGridExtender.SimpleMode)
                {
                    this.applyShowRules = false;
                    this.PublishDependentComponentChanges<bool>("ShowStatus", false);
                    this.medicationGridExtender.ShowStatus = false;

                    this.PublishDependentComponentChanges<bool>("ShowStatusDate", false);
                    this.medicationGridExtender.ShowStatusDate = false;

                    this.PublishDependentComponentChanges<bool>("ShowDosageDetails", false);
                    this.medicationGridExtender.ShowDosageDetails = false;

                    this.PublishDependentComponentChanges<bool>("ShowGraphics", false);
                    this.medicationGridExtender.ShowGraphics = false;

                    this.PublishDependentComponentChanges<bool>("ShowReason", false);
                    this.medicationGridExtender.ShowReason = false;

                    this.applyShowRules = true;
                }

                this.NotifyPropertyChanged("SimpleMode");
            }
        }

        /// <summary>
        /// Gets or sets whether the Look Ahead and Look Behind panels are displayed. 
        /// </summary>
        /// <remarks>
        /// Defaults to false. Available on the client-side and the server-side.
        /// </remarks>
        [Bindable(true), Category("Behavior"), DefaultValue(false)]        
        [Description("Flag to indicator that the Look Ahead and Look Behind panels are displayed")]
        public bool ShowLookAheadBehind
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.ShowLookAheadBehind;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationGridExtender.ShowLookAheadBehind = value;
                this.NotifyPropertyChanged("ShowLookAheadBehind");
            }
        }
        
        /// <summary>
        /// Gets or sets the caption for the drug details column. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Drug Details". Available on the server-side only. 
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue("Drug Details")]
        [Localizable(true)]
        [Description("Caption for the drug details column")]
        public string DrugDetailsColumnHeaderText
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.DrugDetailsColumnHeaderText;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationGridExtender.DrugDetailsColumnHeaderText = value;
                this.NotifyPropertyChanged("DrugDetailsColumnHeaderText");
            }
        }
        
        /// <summary>
        /// Gets or sets the caption for the reason column. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Reason". Available on the server-side only. 
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue("Reason")]
        [Localizable(true)]
        [Description("Caption for the reason column")]
        public string ReasonColumnHeaderText
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.ReasonColumnHeaderText;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationGridExtender.ReasonColumnHeaderText = value;
                this.NotifyPropertyChanged("ReasonColumnHeaderText");
            }
        }

        /// <summary>
        /// Gets or sets the caption of the start date column. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Start Date". Available on the server-side only. 
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue("Start Date")]
        [Localizable(true)]
        [Description("Caption of the start date column")]
        public string StartDateColumnHeaderText
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.StartDateColumnHeaderText;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationGridExtender.StartDateColumnHeaderText = value;
                this.NotifyPropertyChanged("StartDateColumnHeaderText");
            }
        }
        
        /// <summary>
        /// Gets or sets the caption for the status column. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Status". Available on the server-side only. 
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue("Status")]
        [Localizable(true)]
        [Description("Caption of the status column")]
        public string StatusColumnHeaderText
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.StatusColumnHeaderText;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationGridExtender.StatusColumnHeaderText = value;
                this.NotifyPropertyChanged("StatusColumnHeaderText");
            }
        }
        
        /// <summary>
        /// Gets or sets the display of dosage details for all lines. 
        /// </summary>
        /// <remarks>
        /// Defaults to true. Available on the client-side and the server-side. 
        /// </remarks>
        [Bindable(true), Category("Behavior")]        
        [Description("Show or hide the dosage text or Dose/Form/Frequency and Route Information")]
        public bool ShowDosageDetails
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.ShowDosageDetails;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationGridExtender.ShowDosageDetails = value;
                this.ApplyShowRules();
                this.NotifyPropertyChanged("ShowDosageDetails");
            }
        }
        
        /// <summary>
        /// Gets or sets the display of graphics for all lines. 
        /// </summary>
        /// <remarks>
        /// Defaults to true. Available on the client-side and the server-side. 
        /// </remarks>
        [Bindable(true), Category("Appearance"), DefaultValue(true)]
        [Localizable(true)]
        [Description("Show or hide the indicator and alert graphics")]
        public bool ShowGraphics
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.ShowGraphics;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationGridExtender.ShowGraphics = value;
                this.ApplyShowRules();
                this.NotifyPropertyChanged("ShowGraphics");
            }
        }
        
        /// <summary>
        /// Gets or sets the display of the reason for all lines.  
        /// </summary>
        /// <remarks>
        /// Defaults to true. Available on the client-side and the server-side. 
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [Description("Show or hide the Reason on the line")]
        public bool ShowReason
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.ShowReason;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationGridExtender.ShowReason = value;
                this.ApplyShowRules();
                this.NotifyPropertyChanged("ShowReason");
            }
        }

        /// <summary>
        /// Gets or sets the display of the status date for all lines. 
        /// </summary>
        /// <remarks>
        /// Defaults to false. Available on the client-side and the server-side. 
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [Description("Show or hide the StatusDate on the line")]
        public bool ShowStatusDate
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.ShowStatusDate;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationGridExtender.ShowStatusDate = value;
                this.ApplyShowRules();
                this.NotifyPropertyChanged("ShowStatusDate");
            }
        }

        /// <summary>
        /// Gets or sets the value to show the status for all lines. 
        /// </summary>
        /// <remarks>
        ///  Defaults to true. Available on the client-side and the server-side. 
        /// </remarks>
        [Bindable(true), Category("Appearance")]
        [Description("Show or hide the Status")]
        public bool ShowStatus
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.ShowStatus;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationGridExtender.ShowStatus = value;
                this.ApplyShowRules();
                this.NotifyPropertyChanged("ShowStatus");
            }
        }

        /// <summary>
        /// Gets or sets the start date column width. 
        /// </summary>
        /// <remarks>
        /// Available on the server-side only. 
        /// </remarks>
        [Bindable(true), Category("Layout")]
        [Localizable(true)]
        [Description("Start Date column width")]
        public Unit StartDateColumnWidth
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.StartDateColumnWidth;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationGridExtender.StartDateColumnWidth = value;
                this.NotifyPropertyChanged("StartDateColumnWidth");
            }
        }
        
        /// <summary>
        /// Gets or sets the drug details column width. 
        /// </summary>
        /// <remarks>
        /// Available on the server-side only.
        /// </remarks>
        [Bindable(true), Category("Layout")]
        [Localizable(true)]
        [Description("Drug Details column width")]
        public Unit DrugDetailsColumnWidth
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.DrugDetailsColumnWidth;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationGridExtender.DrugDetailsColumnWidth = value;
                this.NotifyPropertyChanged("DrugDetailsColumnWidth");
            }
        }

        /// <summary>
        /// Gets or sets the reason column width. 
        /// </summary>
        /// <remarks>
        /// Available on the server-side only. 
        /// </remarks>
        [Bindable(true), Category("Layout")]
        [Localizable(true)]
        [Description("Reason column width")]
        public Unit ReasonColumnWidth
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.ReasonColumnWidth;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationGridExtender.ReasonColumnWidth = value;
                this.NotifyPropertyChanged("ReasonColumnWidth");
            }
        }
        
        /// <summary>
        /// Gets or sets the status column width. 
        /// </summary>
        /// <remarks>
        /// Available on the server-side only. 
        /// </remarks>
        [Bindable(true), Category("Layout")]
        [Localizable(true)]
        [Description("Status column width")]
        public Unit StatusColumnWidth
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.StatusColumnWidth;
            }

            set
            {
                this.EnsureChildControls();
                this.medicationGridExtender.StatusColumnWidth = value;
                this.NotifyPropertyChanged("StatusColumnWidth");
            }
        }

        /// <summary>
        /// Ensures the extender identifier always follows the control's identifier.
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
                this.medicationGridExtender.ID = value + "_Extender";
                this.medicationGridExtender.TargetControlID = value;
            }
        }

        /// <summary>
        /// A JavaScript function which invokes an OnClick event on the client. 
        /// Occurs when the user clicks the MedicationLine for subscriptions on the client-side. 
        /// </summary>
        /// <remarks>
        /// OnClientClick is actually a behaviour event rather than a behaviour property. It has been specified here as a 
        /// property so the ExtenderBase writes it to the XML script and ASP.NET AJAX hooks up the event properly. Available on the server-side only. 
        /// </remarks>
        [Bindable(false), Category("Behavior"), DefaultValue(null)]
        [Description("Client script function to handle On Click event")]
        public string OnClientClick
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.OnClientClick;
            }

            set
            {
                this.EnsureChildControls();
                if (MedicationGrid.HasPropertyChanged(this.medicationGridExtender.OnClientClick, value))
                {
                    this.medicationGridExtender.OnClientClick = value;
                    this.NotifyPropertyChanged("OnClientClick");
                }
            }
        }

        /// <summary>
        /// A JavaScript function which invokes an OnClick event on the client. 
        /// Occurs when the user double-clicks the MedicationLine for subscriptions on the client-side.
        /// </summary>
        /// <remarks>
        /// OnClientClick is actually a behaviour event rather than a behaviour property. It has been specified here as a 
        /// property so the ExtenderBase writes it to the XML script and ASP.NET AJAX hooks up the event properly.  Available on the server-side only.
        /// </remarks>
        [Bindable(false), Category("Behavior"), DefaultValue(null)]
        [Description("Client script function to handle OnDouble Click event")]
        public string OnClientDoubleClick
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.OnClientDoubleClick;
            }

            set
            {
                this.EnsureChildControls();
                if (MedicationGrid.HasPropertyChanged(this.medicationGridExtender.OnClientDoubleClick, value))
                {
                    this.medicationGridExtender.OnClientDoubleClick = value;
                    this.NotifyPropertyChanged("OnClientDoubleClick");
                }
            }
        }

        /// <summary>
        /// A JavaScript function which invokes an OnClick event on the client. 
        /// Occurs when the user right-clicks the MedicationLine for subscriptions on the client-side. 
        /// </summary>
        /// <remarks>
        /// OnClientClick is actually a behaviour event rather than a behaviour property. It has been specified here as a 
        /// property so the ExtenderBase writes it to the XML script and ASP.NET AJAX hooks up the event properly. Available on the server-side only. 
        /// </remarks>
        [Bindable(false), Category("Behavior"), DefaultValue(null)]
        [Description("Client script function to handle On Right Click event")]
        public string OnClientRightClick
        {
            get
            {
                this.EnsureChildControls();
                return this.medicationGridExtender.OnClientRightClick;
            }

            set
            {
                this.EnsureChildControls();
                if (MedicationGrid.HasPropertyChanged(this.medicationGridExtender.OnClientRightClick, value))
                {
                    this.medicationGridExtender.OnClientRightClick = value;
                    this.NotifyPropertyChanged("OnClientRightClick");
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the selected item if a single item is selected or the item at the zero-ordinal position if more than one item is selected. 
        /// </summary>
        /// <remarks>
        /// Defaults to a null value. Available on the server-side only. 
        /// </remarks> 
        /// <returns>Selected item if a single item is selected or the item at the zero-ordinal position if more than one item is selected. </returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "FxCop recommended changing to Methods - performance implications if used as Property")]
        public Medication GetSelectedItem()
        {
            return this.Items.Find(delegate(Medication medication)
            {
                return medication.IsSelected;
            });
        }

        /// <summary>
        /// Gets the collection of selected medications. 
        /// </summary>
        /// <remarks>
        /// Defaults to an empty string. 
        /// </remarks> 
        /// <returns>An array of selected medications.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "FxCop recommended changing to Methods - performance implications if used as Property")]
        public Medication[] GetSelectedItems()
        {
            this.EnsureChildControls();
            List<Medication> selectedItems = this.Items.FindAll(delegate(Medication medication)
            {
                return medication.IsSelected;
            });

            if (selectedItems != null)
            {
                return selectedItems.ToArray();
            }
            else
            {
                // If no items are currently selected, an empty list is returned
                return new Medication[] { };
            }
        }

        /// <summary>
        /// Sorts the elements in ascending/descending order by start date. 
        /// </summary>
        /// <param name="ascending">The sorting order.</param>
        public void Sort(bool ascending)
        {            
            if (ascending)
            {
                this.Items.Sort(delegate(Medication x, Medication y) { return (x.StartDate.CompareTo(y.StartDate)); });
            }
            else
            {
                this.Items.Sort(delegate(Medication x, Medication y) { return (y.StartDate.CompareTo(x.StartDate)); });
            }
        }

        /// <summary>
        /// Sorts the elements in ascending/descending order by start date. 
        /// </summary>
        /// <param name="column">The column to be sorted. If this column is not the start date column, the start date column will be used as the 
        /// second column.</param>
        /// <param name="ascending">The sorting order.</param>
        public void Sort(MedicationGridColumn column, bool ascending)
        {
            // if sorting descending, reverse the compare result
            int orderMultiplier = ascending ? 1 : -1;

            this.Items.Sort(delegate(Medication x, Medication y)
            {
                switch (column)
                {
                    case MedicationGridColumn.StartDate:
                        return MedicationGrid.CompareStartDate(x, y, ascending);                            

                    case MedicationGridColumn.DrugDetails:
                        string drugDetailsX = x.MedicationNames.ToString() + x.GetDosage();
                        string drugDetailsY = y.MedicationNames.ToString() + y.GetDosage();
                        if (drugDetailsX.Equals(StringComparison.CurrentCulture))
                        {
                            return MedicationGrid.CompareStartDate(x, y, ascending);                            
                        }
                        else
                        {
                            return drugDetailsX.CompareTo(drugDetailsY) * orderMultiplier;
                        }                        

                    case MedicationGridColumn.Reason:
                        if (x.Reason.Equals(y.Reason))
                        {
                            return MedicationGrid.CompareStartDate(x, y, ascending);
                        }
                        else
                        {
                            return x.Reason.CompareTo(y.Reason) * orderMultiplier;
                        }                        

                    case MedicationGridColumn.Status:
                        // If Showing Status Date, sort by the start date. 
                        // Confusing to sort by StatusDate then Status if StatusDate is not visible
                        if (this.ShowStatusDate)
                        {
                            if (!x.StatusDate.Equals(y.StatusDate))
                            {
                                return x.StatusDate.CompareTo(y.StatusDate) * orderMultiplier;
                            }
                            else if (!this.ShowStatus)
                            {
                                // If Status is not visible, sort by Start Date
                                return MedicationGrid.CompareStartDate(x, y, ascending);
                            }
                        }

                        string statusX = x.Status.ToString();
                        string statusY = y.Status.ToString();

                        // Sort by Status Date
                        if (statusX.Equals(statusY))
                        {
                            return MedicationGrid.CompareStartDate(x, y, ascending);
                        }
                        else
                        {
                            return statusX.CompareTo(statusY) * orderMultiplier;
                        }                        
                }

                return 0;
            });
        }
        
        /// <summary>
        /// Sorts the elements using a custom sort method. 
        /// </summary>
        /// <param name="medicationSorter">A custom comparer used to sort the elements.</param>
        public void Sort(IComparer<Medication> medicationSorter)
        {
            this.Items.Sort(medicationSorter);
        }
        #endregion

        #region IPostBackEventHandler Members
        /// <summary>
        /// Raises a postback event when the MedicationLine is clicked. 
        /// </summary>
        /// <param name="eventArgument">The event arguments passed from the client. </param>
        public void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument != null)
            {
                MedicationEventArgs args = MedicationEventArgs.Unknown;

                string[] clientArg = eventArgument.Split(new char[] { ',' }, 2);
                if (clientArg.Length > 1)
                {
                    Medication med = this.FindMedication(clientArg[1]);
                    args = new MedicationEventArgs(med);
                }

                if (clientArg[0].Equals("1", StringComparison.Ordinal))
                {
                    this.OnDoubleClick(args);
                }

                if (clientArg[0].Equals("0", StringComparison.Ordinal))
                {
                    this.OnClick(args);
                }
            }
        }
        #endregion

        #region Protected Methods       
        /// <summary>
        /// Hide the Grid by default. Will be shown once initial scaling has completed
        /// </summary>
        /// <param name="writer">HtmlTextWriter</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            // Hide the Grid by default. Will be shown once initial scaling has completed
            if (!this.DesignMode)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, "0");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Visibility, "hidden");
            }
            
            base.AddAttributesToRender(writer);
        } 

        /// <summary>
        /// Recreate the MedicationGrid Extender early to ensure that the ViewState is loaded
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            
            // Do not add Extender in design mode 
            if (!this.DesignMode)
            {
                this.Controls.Add(this.medicationGridExtender);
            }
        }

        /// <summary>
        /// Only just prior to rendering should the controls be created
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnPreRender(EventArgs e)
        {
            this.EnsureID();

            if (!this.DesignMode)
            {
                this.AddChildControls();
                this.medicationGridExtender.CallbackID = this.UniqueID;
                this.medicationGridExtender.ClickPostBack = this.Click != null;
                this.medicationGridExtender.DoubleClickPostBack = this.DoubleClick != null;
            }

            base.OnPreRender(e);            
        }

        /// <summary>
        /// In Design-Mode OnPreRender is not called, 
        /// </summary>
        /// <param name="writer">Html Text Writer</param>
        protected override void Render(HtmlTextWriter writer)
        {            
            if (this.DesignMode)
            {                
                this.Controls.Clear();                
                this.AddChildControls();
            }

            base.Render(writer);
        }

        /// <summary>
        /// On Click
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected virtual void OnClick(MedicationEventArgs e)
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
        protected virtual void OnDoubleClick(MedicationEventArgs e)
        {
            if (this.DoubleClick != null)
            {
                this.DoubleClick(this, e);
            }
        }
        #endregion

        #region Private Methods
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
        /// Compare Start Dates
        /// </summary>
        /// <param name="x">First Medication to compare start date</param>
        /// <param name="y">Second Medication to compare start date</param>
        /// <param name="ascending">If true, sort in ascending order, if false sort in descending order</param>
        /// <returns>0 if the two objects are identical. Negative value if y is greater than the x. Positive value if y is less than the x. </returns>
        private static int CompareStartDate(Medication x, Medication y, bool ascending)
        {
            if (ascending)
            {
                return x.StartDate.CompareTo(y.StartDate);
            }
            else
            {
                return y.StartDate.CompareTo(x.StartDate);
            }
        }

        /// <summary>
        /// Create Extender as a child control
        /// </summary>
        private void AddChildControls()
        {
            // Look Behind - do not render in Design Mode
            if (!this.DesignMode)
            {
                this.lookBehindListView = new MedicationListView(this.Items);
                this.lookBehindListView.ID = this.ID + "_LookBehind";
                this.lookBehindListView.Mode = MedicationListViewMode.LookBehind;
                this.lookBehindListView.ShowGraphics = this.ShowGraphics;
                this.lookBehindListView.Style.Add(HtmlTextWriterStyle.Display, this.ShowLookAheadBehind ? "" : "none");
                this.Controls.Add(this.lookBehindListView);
                this.medicationGridExtender.LookBehindControlID = this.lookBehindListView.ClientID;
            }

            // Grid Contents
            MedicationGridHeader medicationGridHeader = new MedicationGridHeader();
            medicationGridHeader.ID = this.ID + "_Header";
            medicationGridHeader.ParentGrid = this;

            this.Controls.Add(medicationGridHeader);

            // Medication Lines - do not render in DesignMode
            if (!this.DesignMode)
            {                                  
                for (int index = 0; index < this.Items.Count; index++)
                {
                    Medication medication = this.Items[index];
                    MedicationLine line = new MedicationLine(medication);
                    line.ID = string.Concat(this.ID, "_medLine", index.ToString(CultureInfo.CurrentUICulture));
                    line.EnableViewState = false;
                    line.EnableExtender = false;
                    line.ShowDosageDetails = this.ShowDosageDetails;
                    line.ShowGraphics = this.ShowGraphics;
                    line.ShowReason = this.ShowReason;
                    line.ShowStatus = this.ShowStatus;
                    line.RenderTable = false;
                    line.ShowStatusDate = this.ShowStatusDate;
                    line.SimpleMode = this.SimpleMode;
                    line.CssClass = "nhscui_mg_ml";
                    line.ToolTip = medication.MedicationTooltip;
                    this.Controls.Add(line);
                }
            }

            MedicationGridFooter medicationGridFooter = new MedicationGridFooter();
            medicationGridFooter.ID = this.ID + "_Footer";
            this.Controls.Add(medicationGridFooter);

            // Look Ahead - not render Design Time
            if (!this.DesignMode)
            {
                this.lookAheadListView = new MedicationListView(this.Items);
                this.lookAheadListView.ID = this.ID + "_LookAhead";
                this.lookAheadListView.ShowGraphics = this.ShowGraphics;
                this.lookAheadListView.Style.Add(HtmlTextWriterStyle.Display, this.ShowLookAheadBehind ? "" : "none");
                this.lookAheadListView.Mode = MedicationListViewMode.LookAhead;
                this.Controls.Add(this.lookAheadListView);
                this.medicationGridExtender.LookAheadControlID = this.lookAheadListView.ClientID;
            }
        }

        /// <summary>
        /// On Property Changed
        /// </summary>
        /// <param name="propertyChangedEventArgs">PropertyChanged EventArgs</param>
        private void OnPropertyChanged(PropertyChangedEventArgs propertyChangedEventArgs)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, propertyChangedEventArgs);
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
        /// Search for medication in the medication list by the given id
        /// </summary>
        /// <param name="id">Medication ID</param>
        /// <returns>Medication if found.</returns>
        private Medication FindMedication(string id)
        {
            return this.Items.Find(delegate(Medication medication)
            {
                return medication.MedicationID == id;
            });
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
                    this.medicationGridExtender.SimpleMode = false;
                }
            }
        }

        /// <summary>
        /// Publish Dependant Component Changes
        /// </summary>
        /// <typeparam name="T">Type of property</typeparam>
        /// <param name="propertyName">Property Name that has changed</param>
        /// <param name="value">new value for Property</param>
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
