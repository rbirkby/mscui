//-----------------------------------------------------------------------
// <copyright file="MedicationNameLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>26/01/2007 10:00:36</date>
// <summary>The MedicationNameLabel controls and enforces the complex formatting rules for the display of Virtual Theraputic Moeity (VTM), Virtual Medical Product (VMP) and Actual Medical Product into element can be correctly formatted for display in the MedicationsLine and MedicationsGrid controls.  This allows the ISV to construct complex VTM entries and still allow the actual drug 
// name to have a seperate style associated with it.</summary>
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
    #endregion

    /// <summary>
    /// The MedicationNameLabel controls and enforces complex formatting rules for the display of Virtual Theraputic Moeity (VTM),
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
    public class MedicationNameLabel : WebControl, INotifyPropertyChanged, IPostBackEventHandler
    {
        #region Members Vars
        /// <summary>
        /// List of MedicationName records to be formatted into the drug name.
        /// </summary>
        private MedicationNameCollection medicationNames = new MedicationNameCollection();

        /// <summary>
        /// MedicationNameLabel Extender
        /// </summary>
        private MedicationNameLabelExtender medicationNameLabelExtender = new MedicationNameLabelExtender();

        /// <summary>
        /// Provide Extender
        /// </summary>
        private bool enableExtender = true;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a MedicationNameLabel object and overrides the WebControl constructor to change the default tag to a span. 
        /// </summary>
        public MedicationNameLabel()
            : base(HtmlTextWriterTag.A)
        {            
            this.medicationNames.PropertyChanged += new PropertyChangedEventHandler(this.OnMedicationNamesPropertyChanged);
        }

        /// <summary>
        /// Constructs a MedicationNameLabel object and passes the MedicationName on instantiation.
        /// </summary>
        /// <param name="names">MedicationNames</param>
        public MedicationNameLabel(MedicationNameCollection names)
            : base(HtmlTextWriterTag.A)        
        {
            this.medicationNames = names;
            if (names != null)
            {
                this.medicationNames.PropertyChanged += new PropertyChangedEventHandler(this.OnMedicationNamesPropertyChanged);
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when a property value changes. 
        /// </summary>        
        public event PropertyChangedEventHandler PropertyChanged;
       
        /// <summary>
        /// Occurs when the user clicks the MedicationNameLabel. 
        /// </summary>
        /// <returns>
        /// The MedicationLine as the value in the Sender parameter.
        /// </returns>
        public event EventHandler Click;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the bindable list of MedicationName records to be formatted into the drug name. 
        /// </summary>         
        [Category("MedicationDetails")]
        [Bindable(true)]
        [NotifyParentProperty(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [Description("Medication Names")]
        public MedicationNameCollection MedicationNames
        {
            get
            {
                return this.medicationNames;
            }
        }

        /// <summary>
        /// Gets or sets the URL of the graphic used for critical alerts. 
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(null)]
        [Description("Graphic used for critical alerts")]
        [Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
        [UrlProperty()]                
        public string CriticalAlertGraphic
        {
            get
            {
                return this.GetPropertyValue<string>("CriticalAlertGraphic", null);                                
            }

            set
            {
                this.SetPropertyValue<string>("CriticalAlertGraphic", value);                                                
            }
        }

        /// <summary>
        /// Gets or sets the URL of the graphic used for the secondary indicator. 
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(null)]
        [Description("Graphic used for the secondary indicator")]
        [Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
        [UrlProperty()]                
        public string IndicatorGraphic
        {
            get
            {
                return this.GetPropertyValue<string>("IndicatorGraphic", null);                                                
            }

            set
            {
                this.SetPropertyValue<string>("IndicatorGraphic", value);                                                
            }
        }

        /// <summary>
        /// Gets or sets the display of graphics. 
        /// </summary>
        /// <remarks>
        /// Defaults to true. 
        ///</remarks>
        [Bindable(true), Category("Appearance"), DefaultValue(true)]
        [Description("Place the critical and indicator graphics. If true, space for the image will be provided even if the image is not supplied.")]
        public bool ShowGraphics
        {
            get
            {
                return this.medicationNameLabelExtender.ShowGraphics;                
            }

            set
            {
                if (MedicationNameLabel.HasPropertyChanged(this.medicationNameLabelExtender.ShowGraphics, value))
                {
                    this.medicationNameLabelExtender.ShowGraphics = value;
                    this.NotifyPropertyChanged("ShowGraphics");                    
                }
            }
        }

        /// <summary>
        /// Gets or sets the CSS style used to render the drug names. 
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(null)]
        [Description("The CSS Style used to render the drug names")]        
        public string DrugNameStyle
        {
            get
            {
                return this.GetPropertyValue<string>("DrugNameStyle", null);
            }

            set
            {
                this.SetPropertyValue<string>("DrugNameStyle", value);                                                
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
        /// </remarks>
        [Bindable(true), Category("Behavior"), DefaultValue(null)]
        [Description("Client script function to handle On Click event")]
        public string OnClientClick
        {
            get
            {
                return this.GetPropertyValue<string>("OnClientClick", null);
            }

            set
            {
                this.SetPropertyValue<string>("OnClientClick", value);                                                                
            }
        }

        /// <summary>
        /// Allows the MedicationName to wrap at the <see cref="P:NhsCui.Toolkit.Web.MedicationName.Separator">Separator</see> 
        /// between the <see cref="P:NhsCui.Toolkit.Web.MedicationName.Name">Name</see> and the 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationName.Information">Information</see>.
        /// </summary>
        [Bindable(true), Category("Appearance"), DefaultValue(null)]
        [Description("Allow the Medication Name to wrap at the seperator between the Name and Information")]
        public bool AllowWrap
        {
            get
            {
                return this.GetPropertyValue<bool>("AllowWrap", true);
            }

            set
            {
                this.SetPropertyValue<bool>("AllowWrap", value);
            }
        }

        /// <summary>
        /// Overrides the ID property of the base class to ensure that an Extender can follow the pattern of the control.
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
                this.medicationNameLabelExtender.ID = value + "_Extender";
                this.medicationNameLabelExtender.TargetControlID = value;
            }
        }
        
        /// <summary>
        /// Get/Set Add the extender control. The extender is not required when contained within MedicationGrid
        /// </summary>
        [Browsable(false)]        
        internal bool EnableExtender
        {
            get
            {
                return this.enableExtender;
            }

            set
            {
                this.enableExtender = value;
            }
        }  
        #endregion

        #region IPostBackEventHandler Members
        /// <summary>
        /// Raises a post-back event when a medication line is clicked. 
        /// </summary>
        /// <param name="eventArgument">The event arguments passed from the client. </param>
        public void RaisePostBackEvent(string eventArgument)
        {
            EventArgs args = new EventArgs();
            this.OnClick(args);
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// This Method is required as MedicationNames cannot have a setter if used on the Design Surface. Assign        
        /// </summary>
        /// <param name="medicationNameCollection">Collection to update MedicationNames with</param>
        internal void UpdateMedicationNames(MedicationNameCollection medicationNameCollection)
        {
            this.medicationNames = medicationNameCollection;
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// On Click Event
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
        /// Create Extender as a child control
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            // Do not add Extender in design mode 
            if (!this.DesignMode)
            {
                this.Controls.Add(this.medicationNameLabelExtender);
            }          
        }

        /// <summary>
        /// On PreRender, transfer local copy of the properties into the extender
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnPreRender(EventArgs e)
        {
            if (this.EnableExtender)
            {
                this.EnsureID();
                this.medicationNameLabelExtender.Enabled = true;
                this.medicationNameLabelExtender.ID = this.ID + "_Extender";
                this.medicationNameLabelExtender.TargetControlID = this.ID;                
            }
            else
            {
                this.Controls.Remove(this.medicationNameLabelExtender);
                this.medicationNameLabelExtender.Enabled = false;
            }

            base.OnPreRender(e);
        }

        /// <summary>
        /// Add OnClick event Handler
        /// </summary>
        /// <param name="writer">HtmlTextWriter to render contents to</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            StringBuilder clickEvents = new StringBuilder();
            if (string.IsNullOrEmpty(this.OnClientClick) == false)
            {
                clickEvents.Append(this.OnClientClick);
                if (this.OnClientClick.EndsWith(";", StringComparison.Ordinal) == false)
                {
                    clickEvents.Append(";");
                }
            }

            if (this.Click != null)
            {
                clickEvents.Append("__doPostBack(\'" + this.UniqueID + "\', \'0\');");
            }

            if (clickEvents.Length != 0)
            {
                writer.AddAttribute("onclick", clickEvents.ToString());
            }

            base.AddAttributesToRender(writer);
        }

        /// <summary>
        /// If design-time, ender the contents directly rather than through the behavior
        /// </summary>
        /// <param name="writer">HtmlTextWriter to render contents to</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {            
            // Using rule as follows:

            // 1. If the MedicationNames are not valid
            //       - Any Name field is empty
            //       - Total length exceeds 140 characters
            //    ACTION Display nothing
            // 2. If no medication names have been specified
            //    ACTION Display Nothing (Unless in design-mode, then display the ID of the control)
            if (this.MedicationNames.IsValid)
            {                
                if (this.MedicationNames != null && this.MedicationNames.Count > 0)
                {
                    // If Design Mode, don't render any graphic. content if ShowGraphics is false
                    // For runtime, the image will rather be hidden to they can be shown again client-side
                    if (!this.DesignMode || (this.DesignMode && this.ShowGraphics))
                    {
                        if (!string.IsNullOrEmpty(this.IndicatorGraphic))
                        {
                            writer.WriteBeginTag(HtmlTextWriterTag.Img.ToString());
                            writer.WriteAttribute("id", this.ClientID + "_IndicatorGraphic");
                            writer.WriteAttribute("alt", "Indicator");
                            writer.WriteAttribute("src", this.ResolveUrlForClient(this.IndicatorGraphic));
                            writer.Write("style=\"");
                            writer.WriteStyleAttribute(HtmlTextWriterStyle.Display.ToString(), this.ShowGraphics ? "" : "none");
                            writer.Write(HtmlTextWriter.DoubleQuoteChar);

                            writer.Write(HtmlTextWriter.TagRightChar);
                            writer.WriteEndTag(HtmlTextWriterTag.Img.ToString());
                        }

                        // Render the images (as subcontrols or just as images?)
                        if (!string.IsNullOrEmpty(this.CriticalAlertGraphic))
                        {
                            writer.WriteBeginTag(HtmlTextWriterTag.Img.ToString());
                            writer.WriteAttribute("id", this.ClientID + "_CriticalAlertGraphic");
                            writer.WriteAttribute("alt", "Critical Alert");
                            writer.WriteAttribute("src", this.ResolveUrlForClient(this.CriticalAlertGraphic));
                            writer.Write("style=\"");
                            writer.WriteStyleAttribute(HtmlTextWriterStyle.Display.ToString(), this.ShowGraphics ? "" : "none");
                            writer.Write(HtmlTextWriter.DoubleQuoteChar);

                            writer.Write(HtmlTextWriter.TagRightChar);
                            writer.WriteEndTag(HtmlTextWriterTag.Img.ToString());
                        }
                    }

                    bool renderedFirstItem = false;

                    // Render the Medication Names
                    foreach (MedicationName medicationName in this.MedicationNames)
                    {
                        if (renderedFirstItem)
                        {
                            writer.Write(MedicationNameCollection.MedicationNameSeparator);
                        }

                        writer.WriteBeginTag(HtmlTextWriterTag.Span.ToString());
                        if (!string.IsNullOrEmpty(this.DrugNameStyle))
                        {
                            writer.WriteAttribute("class", this.DrugNameStyle);
                        }

                        writer.Write(HtmlTextWriter.TagRightChar);

                        writer.WriteFullBeginTag(HtmlTextWriterTag.Strong.ToString());
                        writer.Write(AntiXss.HtmlEncode(medicationName.Name));
                        writer.WriteEndTag(HtmlTextWriterTag.Strong.ToString());
                        writer.WriteEndTag(HtmlTextWriterTag.Span.ToString());

                        if (!string.IsNullOrEmpty(medicationName.Information))
                        {
                            writer.WriteFullBeginTag(HtmlTextWriterTag.Span.ToString());
                            if (this.AllowWrap)
                            {
                                writer.WriteFullBeginTag(HtmlTextWriterTag.Wbr.ToString());
                            }

                            writer.Write(MedicationName.SeparatorHtml);
                            writer.Write(AntiXss.HtmlEncode(medicationName.Information));
                            writer.WriteEndTag(HtmlTextWriterTag.Span.ToString());
                        }

                        renderedFirstItem = true;
                    }
                }
                else if (this.DesignMode)
                {
                    // If the MedicationName is contained with a MedicationLine, rather display the ID of the parent
                    Control medLineParent = this.SearchForParent(typeof(MedicationLine));
                    if (medLineParent != null)
                    {
                        writer.Write(medLineParent.ID);
                    }
                    else
                    {
                        writer.Write(this.ID);
                    }
                }
            }

            base.RenderContents(writer);
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
        /// Get property from View State
        /// </summary>
        /// <typeparam name="V">Property Type</typeparam>
        /// <param name="propertyName">PropertyNAme</param>
        /// <param name="nullValue">Default value if null</param>
        /// <returns>Property, or default value if not yet specified</returns>
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
            if ((originalValue != null && value == null) || (originalValue == null && value != null) || (value != null && !value.Equals(originalValue)))
            {                                
                this.ViewState[propertyName] = value;
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                this.OnPropertyChanged(e);
            }      
        }

        /// <summary>
        /// Implement the LoadViewState method. Loads the MedicationNames if saved
        /// </summary>
        /// <param name="savedState">Saved State</param>
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            MedicationNameCollection medicationNames = ViewState["MedicationNames"] as MedicationNameCollection;

            if (medicationNames != null)
            {
                this.medicationNames = medicationNames;
            }
        }

        /// <summary>
        /// Implement the SaveViewState method. Add the MedicationNames to the collection
        /// </summary>
        /// <returns>ViewState</returns>
        protected override object SaveViewState()
        {
            if (this.medicationNames != null && this.medicationNames.Count > 0)
            {
                ViewState.Add("MedicationNames", this.medicationNames);
            }

            return base.SaveViewState();
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
        /// Resolve supplied url (i.e. replace ~ character), is the supplied url is empty
        /// return default resource url
        /// </summary>
        /// <param name="url">url to resolve</param>        
        /// <returns>The resolved url.</returns>
        private string ResolveUrlForClient(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                return this.ResolveClientUrl(url);
            }

            return string.Empty;
        }

        /// <summary>
        /// Pass the property changed event from the collection up
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Property Changed Event Args</param>
        private void OnMedicationNamesPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(sender, e);
            }
        }

        /// <summary>
        /// Notify Collection that a property has changed
        /// </summary>
        /// <param name="propertyName">Property Name</param>
        private void NotifyPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Walk up Parent Nodes looking for a parent of the given type
        /// </summary>
        /// <param name="type">Type to search for</param>
        /// <returns>Control if found, null if not</returns>
        private Control SearchForParent(Type type)
        {
            Control parent = this.Parent;
            while (parent != null && parent.GetType() != type)
            {
                parent = parent.Parent;
            }

            if (parent != null && parent.GetType() == type)
            {
                return parent;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
