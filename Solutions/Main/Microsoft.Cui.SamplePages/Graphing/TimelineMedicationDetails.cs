//-----------------------------------------------------------------------
// <copyright file="TimelineMedicationDetails.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>29-Oct-2009</date>
// <summary>Class to hold medication details.</summary>
//-----------------------------------------------------------------------

#if SILVERLIGHT
namespace Microsoft.Cui.SamplePages
#else
namespace Microsoft.Cui.SampleWinform
#endif
{
    using System;

    /// <summary>
    /// Class to hold medication details.
    /// </summary>
    public class TimelineMedicationDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineMedicationDetails"/> class.
        /// </summary>
        public TimelineMedicationDetails()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineMedicationDetails"/> class.
        /// </summary>
        /// <param name="medicationName">Name of the medication.</param>
        /// <param name="dose">The dose for the medication.</param>
        /// <param name="route">The route.</param>
        /// <param name="frequency">The frequency.</param>
        public TimelineMedicationDetails(string medicationName, string dose, string route, string frequency)
        {
            this.MedicationName = medicationName;
            this.Dose = dose;
            this.Route = route;
            this.Frequency = frequency;
        }

        /// <summary>
        /// Gets or sets the name of the medication.
        /// </summary>
        /// <value>The name of the medication.</value>
        public string MedicationName { get; set; }

        /// <summary>
        /// Gets or sets the dose.
        /// </summary>
        /// <value>The dose for the medication.</value>
        public string Dose { get; set; }

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        /// <value>The route.</value>
        public string Route { get; set; }

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        /// <value>The frequency.</value>
        public string Frequency { get; set; }

        /// <summary>
        /// Gets or sets the name of the brand.
        /// </summary>
        /// <value>The name of the brand.</value>
        public string BrandName { get; set; }

        /// <summary>
        /// Gets or sets the strength of the medication.
        /// </summary>
        /// <value>Strength of the medication.</value>        
        public string SolidStrength
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the strength.
        /// </summary>
        /// <value>The strength.</value>
        public string FluidStrength { get; set; }

        /// <summary>
        /// Gets or sets the form.
        /// </summary>
        /// <value>The form.</value>
        public string Form { get; set; }

        /// <summary>
        /// Gets or sets the duration of the dose.
        /// </summary>
        /// <value>The duration of the dose.</value>
        public string DoseDuration { get; set; }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>The rate.</value>
        public string Rate { get; set; }

        /// <summary>
        /// Gets or sets the dose label.
        /// </summary>
        /// <value>The dose label.</value>
        public string DoseLabel { get; set; }

        /// <summary>
        /// Gets or sets the planned start date.
        /// </summary>
        /// <value>The planned start date.</value>
        public DateTime? PlannedStartDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the planned end date.
        /// </summary>
        /// <value>The planned end date.</value>
        public DateTime? PlannedEndDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public string Status
        {
            get;
            set;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.MedicationName;
        }
    }
}
