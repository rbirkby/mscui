//-----------------------------------------------------------------------
// <copyright file="CompletedPrescription.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>12-Oct-2009</date>
// <summary>
//      A class representing a completed prescription.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;

    /// <summary>
    /// A class representing a completed prescription.
    /// </summary>
    public class CompletedPrescription
    {
        /// <summary>
        /// Gets or sets the drug name.
        /// </summary>
        public string Name 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the brand name.
        /// </summary>
        public string BrandName 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        public string Route 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the form.
        /// </summary>
        public string Form 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the strength.
        /// </summary>
        public string Strength 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the dose.
        /// </summary>
        public string Dose 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        public string Method 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the site.
        /// </summary>
        public string Site 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        public string Frequency 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets a value indicating whether this prescription is as required.
        /// </summary>
        public bool IsAsRequired 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime StartDate 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime? EndDate 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        public string Reason 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string Status 
        { 
            get; set; 
        }
    }
}
