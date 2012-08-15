//-----------------------------------------------------------------------
// <copyright file="AllergyInformation.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>14-Aug-2007</date>
// <summary>Specifies a value indicating the allergy information for a patient. </summary>
//-----------------------------------------------------------------------


namespace NhsCui.Toolkit
{
    /// <summary>
    /// Specifies a value indicating the allergy information for a patient.
    /// </summary>
    public enum AllergyInformation
    {
        /// <summary>
        /// Patient has allergies.
        /// </summary>
        Present,

        /// <summary>
        /// Patient has no known allergies.
        /// </summary>
        NoneKnown,

        /// <summary>
        /// Allergy information has not been recorded.
        /// </summary>
        NotRecorded,
       
        /// <summary>
        /// Allergy information unavailable.
        /// </summary>
        Unavailable
    } 
}
