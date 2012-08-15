//-----------------------------------------------------------------------
// <copyright file="DrugElement.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      A class representing a drug attribute.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    /// <summary>
    /// A class representing a drug attribute.
    /// </summary>
    public class DrugElement : ElementValue
    {
        /// <summary>
        /// Gets or sets the valid brands.
        /// </summary>
        public string[] ValidBrands
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the valid routes.
        /// </summary>
        public string[] ValidRoutes 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the valid forms.
        /// </summary>
        public string[] ValidForms 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the valid strengths.
        /// </summary>
        public string[] ValidStrengths 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the valid dosages.
        /// </summary>
        public string[] ValidDosages 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the valid volumes.
        /// </summary>
        public string[] ValidVolumes 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the valid dose durations.
        /// </summary>
        public string[] ValidDoseDurations 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the valid frequencies.
        /// </summary>
        public string[] ValidFrequencies 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description 
        { 
            get; set; 
        }

        /// <summary>
        /// Gets or sets a value indicating whether this is a custom value.
        /// </summary>
        public bool IsCustomValue 
        { 
            get; set; 
        }

        /// <summary>
        /// Copys the values of a specified drug attribute.
        /// </summary>
        /// <param name="drugAttribute">The drug attribute to copy.</param>
        public void Copy(DrugElement drugAttribute)
        {
            this.Value = drugAttribute.Value;
            this.ValidBrands = drugAttribute.ValidBrands;
            this.ValidDosages = drugAttribute.ValidDosages;
            this.ValidDoseDurations = drugAttribute.ValidDoseDurations;
            this.ValidForms = drugAttribute.ValidForms;
            this.ValidFrequencies = drugAttribute.ValidFrequencies;
            this.ValidRoutes = drugAttribute.ValidRoutes;
            this.ValidStrengths = drugAttribute.ValidStrengths;
            this.ValidVolumes = drugAttribute.ValidVolumes;
            this.Description = drugAttribute.Description;
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <returns>The string value.</returns>
        public override string ToString()
        {
            if (this.IsCustomValue)
            {
                string stringValue = "Custom Value: ";
                stringValue += !string.IsNullOrEmpty(base.ToString()) ? base.ToString() : "[not set]";
                return stringValue;
            }
            else
            {
                return base.ToString();
            }
        }
    }
}
