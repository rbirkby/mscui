//-----------------------------------------------------------------------
// <copyright file="Route.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      A class representing a prescription route.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System.Globalization;

    /// <summary>
    /// A class representing a prescription route.
    /// </summary>
    public class Route : DrugElement
    {
        /// <summary>
        /// Initializes a new instance of the Route class.
        /// </summary>
        public Route()
        {
        }

        /// <summary>
        ///Initializes a new instance of the Route class with a drug attribute to copy.
        /// </summary>
        /// <param name="drugAttribute">The drug attribute to copy.</param>
        public Route(DrugElement drugAttribute)
        {
            this.Copy(drugAttribute);
        }

        /// <summary>
        /// Gets or sets a value indicating whether a brand name is mandatory.
        /// </summary>
        public bool MandatoryBrandName 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets a value indicating whether a form is mandatory.
        /// </summary>
        public bool MandatoryForm 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets a value indicating whether a site is mandatory.
        /// </summary>
        public bool MandatorySite 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets a form.
        /// </summary>
        public string Form 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets a modifier.
        /// </summary>
        public string Modifier 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets a brand name.
        /// </summary>
        public string BrandName 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the drug brand name.
        /// </summary>
        public string DrugBrandName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a drug name.
        /// </summary>
        public string DrugName 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets a value indicating whether the template prescription is a brand generic.
        /// </summary>
        public bool IsABrandGeneric 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets a value indicating whether the template prescription allows quick prescription.
        /// </summary>
        public bool AllowQuickPrescription 
        {
            get; set; 
        }

        /// <summary>
        /// Gets the display route.
        /// </summary>
        public string DisplayRoute
        {
            get
            {
                string route = this.DisplayValue;
                route += !string.IsNullOrEmpty(this.Form) ? " ― " + this.Form : string.Empty;
                return route;
            }
        }

        /// <summary>
        /// Gets the display drug name.
        /// </summary>
        public string DisplayDrugName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.DrugName))
                {
                    return string.Format(CultureInfo.CurrentCulture, "{0} ― ", this.DrugName);
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the display brand name.
        /// </summary>
        public string DisplayBrandName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.BrandName))
                {
                    return string.Format(CultureInfo.CurrentCulture, "{0} ― ", this.BrandName);
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the display value.
        /// </summary>
        public override string DisplayValue
        {
            get
            {
                return this.Value + (!string.IsNullOrEmpty(this.Modifier) ? " ― " + this.Modifier : string.Empty);
            }
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <returns>The string value.</returns>
        public override string ToString()
        {
            string stringValue = string.Empty;

            if (this.IsABrandGeneric && string.IsNullOrEmpty(this.BrandName))
            {
                stringValue += this.DrugName;
            }
            else if (this.IsABrandGeneric && !string.IsNullOrEmpty(this.BrandName))
            {
                stringValue += this.BrandName;
            }

            if (!string.IsNullOrEmpty(stringValue))
            {
                stringValue += " ― ";
            }

            stringValue += this.DisplayValue;

            if (!string.IsNullOrEmpty(this.Description))
            {
                stringValue += " ― " + this.Description;
            }

            return stringValue;
        }
    }
}
