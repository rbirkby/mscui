//-----------------------------------------------------------------------
// <copyright file="Form.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      A class representing a prescription form.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    /// <summary>
    /// A class representing a prescription form.
    /// </summary>
    public class Form : DrugElement
    {
        /// <summary>
        /// Initializes a new instance of the Form class.
        /// </summary>
        public Form()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Form class with a drug attribute to copy.
        /// </summary>
        /// <param name="drugAttribute">The drug attribute to copy.</param>
        public Form(DrugElement drugAttribute)
        {
            this.Copy(drugAttribute);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this form has a mandatory strength.
        /// </summary>
        public bool MandatoryStrength 
        { 
            get; set; 
        }
    }
}
