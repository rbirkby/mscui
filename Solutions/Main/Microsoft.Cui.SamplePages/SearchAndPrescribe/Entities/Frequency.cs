//-----------------------------------------------------------------------
// <copyright file="Frequency.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      A class representing a prescription frequency.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;

    /// <summary>
    /// A class representing a prescription frequency.
    /// </summary>
    public class Frequency : DrugElement
    {
        /// <summary>
        /// Stores whether admin times should be shown.
        /// </summary>
        private bool showAdminTimes = true;

        /// <summary>
        /// Stores the frequency duration.
        /// </summary>
        private TimeSpan? duration = TimeSpan.MaxValue;

        /// <summary>
        /// Stores the administration times for the frequency.
        /// </summary>
        private AdministrationTimes administrationTimes;

        /// <summary>
        /// Initializes a new instance of the Frequency class.
        /// </summary>
        public Frequency()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Frequency class with a drug attribute to copy.
        /// </summary>
        /// <param name="drugAttribute">The drug attribute to copy.</param>
        public Frequency(DrugElement drugAttribute)
        {
            this.Copy(drugAttribute);
        }

        /// <summary>
        /// Gets or sets a value indicating whether admin times should be shown.
        /// </summary>
        public bool ShowAdminTimes
        {
            get 
            {
                return this.showAdminTimes; 
            }

            set
            {
                this.showAdminTimes = value;
                this.RaisePropertyChanged("AdministrationTimes");
            }
        }

        /// <summary>
        /// Gets or sets the frequency duration.
        /// </summary>
        public TimeSpan? Duration
        {
            get { return this.duration; }
            set { this.duration = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the frequency has a duration.
        /// </summary>
        public bool HasDuration 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets a value indicating when the admin times should be shown.
        /// </summary>
        public AdministrationTimes AdministrationTimes
        {
            get 
            {
                if (this.ShowAdminTimes)
                {
                    return this.administrationTimes;
                }

                return null;
            }

            set 
            {
                this.administrationTimes = value;
                this.RaisePropertyChanged("AdministrationTimes");

                if (this.administrationTimes != null)
                {
                    this.administrationTimes.UpdateDisplayAdministrationTimes(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the frequency is as required.
        /// </summary>
        public bool AsRequired 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets the frequency display value.
        /// </summary>
        public override string DisplayValue
        {
            get
            {
                return this.Value + (this.AsRequired ? " - as required" : string.Empty);
            }
        }
    }
}
