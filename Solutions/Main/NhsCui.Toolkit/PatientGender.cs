//-----------------------------------------------------------------------
// <copyright file="PatientGender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>03-Jan-2007</date>
// <summary>Specifies a value indicating the gender of the patient. </summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit
{
    /// <summary>
    /// Specifies a value indicating the gender of the patient.
    /// </summary>
    public enum PatientGender
    {
        /// <summary>
        /// The patient is male.
        /// </summary>
        Male,

        /// <summary>
        /// The patient is female.
        /// </summary>
        Female,

        /// <summary>
        /// The gender of the patient is not known.
        /// </summary>
        NotKnown,

        /// <summary>
        /// The gender of the patient is not specified.
        /// </summary>
        NotSpecified        
    }
}