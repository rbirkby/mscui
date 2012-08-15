//------------------------------------------------------------------------
// <copyright file="MedicationGridColumn.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>12-Mar-2007</date>
// <summary> MedicationGrid Header Column Enumeration</summary>
//------------------------------------------------------------------------
namespace NhsCui.Toolkit.Web
{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.Text;
    #endregion

    /// <summary>  
    /// Specifies a value for the MedicationGrid column headers. 
    /// </summary>
    public enum MedicationGridColumn
    {        
        /// <summary>
        /// The start date column.
        /// </summary>
        StartDate,

        /// <summary>
        /// The drug details column.
        /// </summary>
        DrugDetails,

        /// <summary>
        /// The reason column.
        /// </summary>
        Reason,

        /// <summary>
        /// The status column.
        /// </summary>
        Status
    }
}