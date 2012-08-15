//-----------------------------------------------------------------------
// <copyright file="TemplatePrescription.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010.
// All rights reserved
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
// <date>24-Jul-2009</date>
// <summary>
//      A class representing a template prescription.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// A class representing a temaplate prescription.
    /// </summary>
    public class TemplatePrescription : INotifyPropertyChanged
    {
        /// <summary>
        /// Stores the route.
        /// </summary>
        private Route route;

        /// <summary>
        /// Stores the strength.
        /// </summary>
        private string strength;

        /// <summary>
        /// Stores the dose.
        /// </summary>
        private string dose;

        /// <summary>
        /// Stores whether the presciption is as required.
        /// </summary>
        private bool asRequired;

        /// <summary>
        /// Stores the frequency.
        /// </summary>
        private Frequency frequency;

        /// <summary>
        /// Stores the duration.
        /// </summary>
        private TimeSpan? duration = TimeSpan.MaxValue;

        /// <summary>
        /// Stores whether to show the duration.
        /// </summary>
        private bool showDuration;

        /// <summary>
        /// Stores the brand name.
        /// </summary>
        private string brandName;

        /// <summary>
        /// Stores the administration times.
        /// </summary>
        private AdministrationTimes administrationTimes;

        /// <summary>
        /// Stores the text.
        /// </summary>
        private string text;

        /// <summary>
        /// Stores the description.
        /// </summary>
        private string description;

        /// <summary>
        /// The property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the brand name.
        /// </summary>
        public string BrandName
        {
            get 
            { 
                return this.brandName; 
            }

            set
            {
                this.brandName = value;
                this.RaisePropertyChanged("BrandName");
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get 
            {
                return this.text; 
            }

            set 
            {
                this.text = value;
                this.RaisePropertyChanged("Text");
            }
        }

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        public Route Route
        {
            get 
            { 
                return this.route; 
            }

            set 
            { 
                this.route = value; 
            }
        }

        /// <summary>
        /// Gets or sets the strength.
        /// </summary>
        public string Strength
        {
            get 
            {
                return this.strength; 
            }
            
            set
            {
                this.strength = value;
                this.RaisePropertyChanged("Strength");
            }
        }

        /// <summary>
        /// Gets or sets the dose.
        /// </summary>
        public string Dose
        {
            get 
            {
                return this.dose; 
            }

            set 
            {
                this.dose = value; 
                this.RaisePropertyChanged("Dose"); 
            }
        }
       
        /// <summary>
        /// Gets or sets a value indicating whether the prescription is as required.
        /// </summary>
        public bool AsRequired
        {
            get 
            {
                return this.asRequired; 
            }

            set 
            {
                this.asRequired = value; 
                this.RaisePropertyChanged("AsRequired"); 
            }
        }

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        public Frequency Frequency
        {
            get 
            {
                return this.frequency; 
            }

            set 
            {
                this.frequency = value; 
                this.RaisePropertyChanged("Frequency");
            }
        }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public TimeSpan? Duration
        {
            get 
            {
                return this.duration; 
            }

            set 
            {
                this.duration = value;
                this.RaisePropertyChanged("Duration");               
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the duration should be displayed.
        /// </summary>
        public bool ShowDuration
        {
            get 
            {
                return this.showDuration; 
            }

            set 
            { 
                this.showDuration = value; 
            }
        }

        /// <summary>
        /// Gets or sets the administration times.
        /// </summary>
        public AdministrationTimes AdministrationTimes
        {
            get
            {
                return this.administrationTimes;
            }

            set
            {
                this.administrationTimes = value;
                this.RaisePropertyChanged("AdministrationTimes");
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                this.description = value;
                this.RaisePropertyChanged("Description");
            }
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <returns>The string value.</returns>
        public override string ToString()
        {
            string stringValue = string.Empty;

            if (!string.IsNullOrEmpty(this.Dose))
            {
                stringValue += "DOSE " + this.Dose;
            }

            if (!string.IsNullOrEmpty(this.Strength))
            {
                stringValue += " ― " + this.Strength;
            }

            if (this.Frequency != null && !string.IsNullOrEmpty(this.Frequency.ToString()))
            {
                stringValue += " ― " + this.Frequency.ToString();
            }

            if (!string.IsNullOrEmpty(this.BrandName))
            {
                stringValue += " ― " + this.BrandName;
            }

            if (!string.IsNullOrEmpty(this.Description))
            {
                stringValue += " ― " + this.Description;
            }

            return stringValue;
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">The property.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
