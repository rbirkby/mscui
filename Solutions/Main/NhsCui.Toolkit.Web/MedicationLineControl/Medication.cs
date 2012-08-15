//-----------------------------------------------------------------------
// <copyright file="Medication.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>02-Feb-2007</date>
// <summary>A sealed class which is used to identify the full medication details for MedicationLine and MedicationGrid.
// </summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Globalization;
    using System.Diagnostics.CodeAnalysis;
    using NhsCui.Toolkit.DateAndTime;
    using System.Web.UI;
    using System.Drawing.Design;

    /// <summary>
    /// Used to identify the drug name from other details which are a valid part of the medication name.
    /// </summary>    
    [Serializable]
    [ParseChildren(true)]
    [PersistChildren(false)]    
    public sealed class Medication : INotifyPropertyChanged, ICloneable
    {
        #region Member Vars
        /// <summary>
        /// The separator character between the different doses. 
        /// </summary>
        /// <remarks>
        /// Defaults to a hyphen (-). 
        /// </remarks>
        public const string DosageSeparator = " - ";

        /// <summary>
        /// The ellipsis character displayed if the <see cref="F:NhsCui.Toolkit.Web.Medication.MaximumDisplayLength">MaximumDisplayLength</see>
        /// is exceeded. 
        /// </summary>
        /// <remarks>
        /// If the <see cref="P:NhsCui.Toolkit.Web.Medication.DisplayLength">DisplayLength</see> is greater than the
        /// <see cref="F:NhsCui.Toolkit.Web.Medication.MaximumDisplayLength">MaximumDisplayLength</see> of 512 characters, the medication truncates 
        /// to 509 characters plus the ellipsis.  
        /// </remarks>
        public const string Ellipsis = "...";

        /// <summary>
        /// The maximum display length for the medication. 
        /// </summary>
        /// <remarks>
        /// This is set to 512 characters. If this is greater than 512 characters, the displayed medication truncates to 509 characters plus an ellipsis.  
        /// </remarks>
        public const int MaximumDisplayLength = 512;        

        /// <summary>
        /// Temporary store for the CriticalAlertGraphicProperty until the Extender is created
        /// </summary>
        private string criticalAlertGraphic;

        /// <summary>
        /// Text description of the dosage for this medication
        /// </summary>
        private string dosageText;

        /// <summary>
        /// Dose of the medication
        /// </summary>
        private string dose;

        /// <summary>
        /// Form that the medication is delivered
        /// </summary>
        private string form;

        /// <summary>
        /// Frequency to administer the medication
        /// </summary>
        private string frequency;

        /// <summary>
        /// Temporary store for the IndicatorGraphic Property until the Extender is created
        /// </summary>
        private string indicatorGraphic;

        /// <summary>
        /// Value telling the line that it is selected
        /// </summary>
        private bool isselected;

        /// <summary>
        /// Medication Names
        /// </summary>
        private MedicationNameCollection medicationNames = new MedicationNameCollection();

        /// <summary>
        /// The tooltip associated with the drug name
        /// </summary>
        private string medicationTooltip;

        /// <summary>
        /// Reason field
        /// </summary>
        private string reason;

        /// <summary>
        /// Method by which the medication is delivered
        /// </summary>
        private string route;

        /// <summary>
        /// Significant date displayed in the medication. Default to current date.
        /// </summary>
        private DateTime startDate = DateTime.Now.Date;

        /// <summary>
        /// Specific keyword to be displayed for the lines status
        /// </summary>
        private MedicationStatus status;

        /// <summary>
        /// Date associated with the status. Default to current date.
        /// </summary>
        private DateTime statusDate = DateTime.Now.Date;

        /// <summary>
        /// Identifier for the Medication
        /// </summary>
        private string medicationID = Guid.NewGuid().ToString();
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a Medication object and hooks up the 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationNameCollection.PropertyChanged">PropertyChanged</see> event handler from the 
        /// MedicationNameCollection.
        /// </summary>
        public Medication()
        {
            this.medicationNames.PropertyChanged += new PropertyChangedEventHandler(this.OnMedicationNamesPropertyChanged);
        }

        #endregion

        #region INotifyPropertyChanged Members
        /// <summary>
        /// Occurs when a property value changes. 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the URL of the graphic used for critical alerts. 
        /// </summary>
        [Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
        [UrlProperty()]
        [Category("MedicationDetails")]
        public string CriticalAlertGraphic
        {
            get
            {
                return this.criticalAlertGraphic;
            }

            set
            {
                this.SetProperty<string>(ref this.criticalAlertGraphic, value, "CriticalAlertGraphic");
            }
        }

        /// <summary>
        /// Gets or sets a textual description of the medication's dosage. 
        /// </summary>
        [Category("MedicationDetails")]
        public string DosageText
        {
            get
            {
                return this.dosageText;
            }

            set
            {
                this.SetProperty<string>(ref this.dosageText, value, "DosageText");
                this.EnsureValid();
            }
        }

        /// <summary>
        /// Gets or sets the dose of the medication. 
        /// </summary>
        [Category("MedicationDetails")]
        public string Dose
        {
            get
            {
                return this.dose;
            }

            set
            {
                this.SetProperty<string>(ref this.dose, value, "Dose");
                this.EnsureValid();
            }
        }

        /// <summary>
        /// Gets or sets the form in which the medication is delivered. 
        /// </summary>
        [Category("MedicationDetails")]
        public string Form
        {
            get
            {
                return this.form;
            }

            set
            {
                this.SetProperty<string>(ref this.form, value, "Form");
                this.EnsureValid();
            }
        }

        /// <summary>
        /// Gets or sets the frequency with which the medication should be taken. 
        /// </summary>
        [Category("MedicationDetails")]
        public string Frequency
        {
            get
            {
                return this.frequency;
            }

            set
            {
                this.SetProperty<string>(ref this.frequency, value, "Frequency");
                this.EnsureValid();
            }
        }

        /// <summary>
        /// Gets or sets the URL of the graphic that displays in the indicator section of a medication line. 
        /// </summary>
        [Editor(typeof(System.Web.UI.Design.ImageUrlEditor), typeof(UITypeEditor))]
        [UrlProperty()]
        [Category("MedicationDetails")]
        public string IndicatorGraphic
        {
            get
            {
                return this.indicatorGraphic;
            }

            set
            {
                this.SetProperty<string>(ref this.indicatorGraphic, value, "IndicatorGraphic");
            }
        }

        /// <summary>
        /// Gets or sets the value indicating a medication line is selected. 
        /// </summary>        
        public bool IsSelected
        {
            get
            {
                return this.isselected;
            }

            set
            {
                this.SetProperty<bool>(ref this.isselected, value, "IsSelected");
            }
        }

        /// <summary>
        /// Gets or sets the list of MedicationName records to be formatted into the drug name. 
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
        /// Gets or sets the ToolTip associated with the drug name. 
        /// </summary>
        [Category("Behavior")]
        public string MedicationTooltip
        {
            get
            {
                return this.medicationTooltip;
            }

            set
            {
                this.SetProperty<string>(ref this.medicationTooltip, value, "MedicationTooltip");
            }
        }

        /// <summary>
        /// Gets or sets the Reason field. 
        /// </summary>
        [Category("MedicationDetails")]
        public string Reason
        {
            get
            {
                return this.reason;
            }

            set
            {                
                this.SetProperty<string>(ref this.reason, value, "Reason");

                // Validate Reason Length
                if (this.reason != null)
                {
                    if (this.reason.Length > Medication.MaximumDisplayLength)
                    {
                        throw new ArgumentOutOfRangeException(
                                string.Format(
                                    CultureInfo.CurrentUICulture,
                                    MedicationLineControl.Resources.ReasonDisplayLengthExceed,
                                    this.reason.Length,
                                    Medication.MaximumDisplayLength));
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the method by which the medication is delivered. 
        /// </summary>
        [Category("MedicationDetails")]
        public string Route
        {
            get
            {
                return this.route;
            }

            set
            {
                this.SetProperty<string>(ref this.route, value, "Route");
                this.EnsureValid();
            }
        }

        /// <summary>
        /// Gets or sets the significant date displayed in the MedicationLine. 
        /// </summary>    
        [Category("MedicationDetails")]
        public DateTime StartDate
        {
            get
            {
                return this.startDate;
            }

            set
            {
                this.SetProperty<DateTime>(ref this.startDate, value, "StartDate");
            }
        }

        /// <summary>
        /// Gets or sets the specific keyword to be displayed for the medication line's status. 
        /// </summary>
        [Category("MedicationDetails")]
        public MedicationStatus Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.SetProperty<MedicationStatus>(ref this.status, value, "Status");
            }
        }

        /// <summary>
        /// Gets or sets the date associated with the <see cref="P:NhsCui.Toolkit.Web.Medication.Status">Status</see>. 
        /// </summary>
        [Category("MedicationDetails")]
        public DateTime StatusDate
        {
            get
            {
                return this.statusDate;
            }

            set
            {
                this.SetProperty<DateTime>(ref this.statusDate, value, "StatusDate");
            }
        }

        /// <summary>
        /// Gets the displayed length of the medication.
        /// </summary>   
        public int DisplayLength
        {
            get
            {                    
                return this.medicationNames.DisplayLength + this.GetDosageInternal().Length;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the last medication name passed the validation rules. 
        /// </summary>        
        public bool IsValid
        {
            get
            {
                return (this.DisplayLength <= Medication.MaximumDisplayLength);
            }
        }

        /// <summary>
        /// Gets or sets a unique identifier for the medication.
        /// </summary>     
        /// <remarks>
        /// This has an initial value of a uniquely-generated GUID which can be overwritten. If a blank or empty value is assigned, a new GUID
        /// should be generated. 
        /// </remarks>
        [Category("MedicationDetails")]
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member", Justification = "As per framework ID is uppercase")]
        public string MedicationID
        {
            get
            {
                return this.medicationID;
            }

            set
            {
                this.SetProperty<string>(ref this.medicationID, value, "MedicationID");
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the <see cref="P:NhsCui.Toolkit.Web.Medication.DosageText">DosageText</see> if no 
        /// <see cref="P:NhsCui.Toolkit.Web.Medication.Dose">Dose</see>, <see cref="P:NhsCui.Toolkit.Web.Medication.Form">Form</see>, 
        /// <see cref="P:NhsCui.Toolkit.Web.Medication.Frequency">Frequency</see> or 
        /// <see cref="P:NhsCui.Toolkit.Web.Medication.Route">Route</see> properties are set. 
        /// </summary>
        /// <returns>The <see cref="P:NhsCui.Toolkit.Web.Medication.DosageText">DosageText</see> if no 
        /// <see cref="P:NhsCui.Toolkit.Web.Medication.Dose">Dose</see>, <see cref="P:NhsCui.Toolkit.Web.Medication.Form">Form</see>, 
        /// <see cref="P:NhsCui.Toolkit.Web.Medication.Frequency">Frequency</see> or 
        /// <see cref="P:NhsCui.Toolkit.Web.Medication.Route">Route</see> properties are set.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Dose and DosageText Properties already exist. To indicate that this is a worker method that determines the full string this has been defined as a method")]
        public string GetDosage()
        {
            string dosage = this.GetDosageInternal();

            if (!this.IsValid)
            {
                // If the maximum length has been exceeded, reduce the total to 509, and add the Ellipsis
                if (!this.MedicationNames.IsValid)
                {
                    // It is not defined what be done in this case. Just set the text to empty until clarified.
                    return string.Empty;
                }

                // The additional character is for the spacer between the MedicationNames and the dosage text                
                int dosageLength = (Medication.MaximumDisplayLength - this.MedicationNames.DisplayLength - 1 - Medication.Ellipsis.Length);
                dosage = string.Concat(dosage.Substring(0, dosageLength), Medication.Ellipsis);
            }

            return dosage;
        }

        /// <summary>
        /// Gets the <see cref="P:NhsCui.Toolkit.Web.Medication.DosageText">DosageText</see> if no 
        /// <see cref="P:NhsCui.Toolkit.Web.Medication.Dose">Dose</see>, <see cref="P:NhsCui.Toolkit.Web.Medication.Form">Form</see>, 
        /// <see cref="P:NhsCui.Toolkit.Web.Medication.Frequency">Frequency</see> or 
        /// <see cref="P:NhsCui.Toolkit.Web.Medication.Route">Route</see> properties are set. 
        /// </summary>
        /// <returns>The <see cref="P:NhsCui.Toolkit.Web.Medication.DosageText">DosageText</see> if no 
        /// <see cref="P:NhsCui.Toolkit.Web.Medication.Dose">Dose</see>, <see cref="P:NhsCui.Toolkit.Web.Medication.Form">Form</see>, 
        /// <see cref="P:NhsCui.Toolkit.Web.Medication.Frequency">Frequency</see> or 
        /// <see cref="P:NhsCui.Toolkit.Web.Medication.Route">Route</see> properties are set.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is an internal method")]
        public string GetDosageInternal()
        {
            string dosage = Medication.JoinStrings(Medication.DosageSeparator, new string[] { this.dose, this.frequency, this.form, this.route });

            if (string.IsNullOrEmpty(dosage))
            {
                if (this.dosageText == null)
                {
                    dosage = string.Empty;
                }
                else
                {
                    dosage = this.dosageText;
                }
            }

            return dosage;
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Makes a deep copy of the Medication. 
        /// </summary>
        /// <remarks>
        /// All fields are deep copies. 
        /// </remarks>
        /// <returns>A deep copy of the Medication. 
        /// </returns>
        public object Clone()
        {
            Medication medication = new Medication();
            medication.CriticalAlertGraphic = this.CriticalAlertGraphic;
            medication.DosageText = this.DosageText;
            medication.Dose = this.Dose;
            medication.Form = this.Form;
            medication.Frequency = this.Frequency;
            medication.IndicatorGraphic = this.IndicatorGraphic;
            medication.IsSelected = this.IsSelected;
            MedicationName[] medicationNames = new MedicationName[this.MedicationNames.Count];
            this.MedicationNames.CopyTo(medicationNames, 0);
            medication.MedicationNames.AddRange(medicationNames);
            medication.MedicationTooltip = this.MedicationTooltip;
            medication.Reason = this.Reason;
            medication.StartDate = this.StartDate;
            medication.Status = this.Status;

            return medication;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Limits the length of a string. 
        /// </summary>
        /// <remarks>
        /// If the length exceeds the <see cref="F:NhsCui.Toolkit.Web.Medication.MaximumDisplayLength">MaximumDisplayLength</see>,
        /// it is trimmed to the maximum length with an ellipsis included. 
        /// </remarks>
        /// <param name="text">The text to be limited. </param>
        /// <returns>The trimmed text. </returns>        
        internal static string LimitStringLength(string text)
        {
            // validate parameters
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }

            if (text.Length <= Medication.MaximumDisplayLength)
            {
                return text;
            }
            else
            {
                int length = (Medication.MaximumDisplayLength - Medication.Ellipsis.Length);
                return string.Concat(text.Substring(0, length), Medication.Ellipsis);
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
        /// Help class similar to string.Join except this version will only append the string if it is not empty or null
        /// </summary>
        /// <param name="seperator">Seperator string</param>
        /// <param name="values">Values to join</param>        
        /// <returns>joined string</returns>
        private static string JoinStrings(string seperator, string[] values)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string value in values)
            {
                if (builder.Length != 0 && !string.IsNullOrEmpty(value))
                {
                    builder.Append(seperator);
                }

                builder.Append(value);
            }
            
            return builder.ToString();
        }

        /// <summary>
        /// Notify Collection that a property has changed
        /// </summary>
        /// <param name="info">Property Name</param>
        private void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        /// <summary>
        /// Ensure that the Medication is valid according to the validation rules
        /// </summary>
        private void EnsureValid()
        {            
            // The total of the MedicationNames and the Dose/Form/Frequency/Route must not exceed the Maximum Length
            // Get the total of MedicationNames length and the Dosage Text or Dose/Form/Frequency/Route as per the rules;            
            if (!this.IsValid)
            {
                // Throw exception.  Should data be truncated at this point. If it isn't an error will again be thrown client-side
                throw new ArgumentOutOfRangeException(
                        string.Format(
                            CultureInfo.CurrentUICulture, 
                            MedicationLineControl.Resources.MedicationDisplayLengthExceed, 
                            this.DisplayLength, 
                            Medication.MaximumDisplayLength));
            }
        }

        /// <summary>
        /// Property Changed from the Medication Names - bubble up
        /// </summary>
        /// <param name="sender">MedicationNames Collection</param>
        /// <param name="e">rRoperty Changed Event Args</param>
        private void OnMedicationNamesPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(sender, e);
            }
        }

        /// <summary>
        /// Check if a property has changed value 
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="originalValue">Original Value</param>
        /// <param name="newValue">New Value</param>
        /// <param name="name">Property Name</param>        
        private void SetProperty<T>(ref T originalValue, T newValue, string name)
        {
            if (Medication.HasPropertyChanged(originalValue, newValue))
            {
                originalValue = newValue;
                this.NotifyPropertyChanged(name);
            }            
        }
        #endregion
    }
}
