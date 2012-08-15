//-----------------------------------------------------------------------
// <copyright file="Strength.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      A class representing a prescription strength.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;

    /// <summary>
    ///  A class representing a prescription strength.
    /// </summary>
    public class Strength : DrugElement
    {
        /// <summary>
        /// Initializes a new instance of the Strength class.
        /// </summary>
        public Strength()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Strength class with a drug attribute to copy.
        /// </summary>
        /// <param name="drugAttribute">The drug attribute to copy.</param>
        public Strength(DrugElement drugAttribute)
        {
            this.Copy(drugAttribute);
        }

        /// <summary>
        /// Gets the meds line display value.
        /// </summary>
        public string MedsLineDisplayValue
        {
            get
            {
                string medsLineDisplayValue = string.Empty;
                medsLineDisplayValue += !this.DisplayValue.EndsWith("%", StringComparison.CurrentCulture) ? "― " : string.Empty;
                medsLineDisplayValue += this.DisplayValue;
                return medsLineDisplayValue;
            }
        }
    }
}
