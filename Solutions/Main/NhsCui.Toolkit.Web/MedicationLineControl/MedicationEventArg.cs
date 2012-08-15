//------------------------------------------------------------------------
// <copyright file="MedicationEventArg.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>05-Mar-2007</date>
// <summary>Medication Event Arguments</summary>
//------------------------------------------------------------------------
namespace NhsCui.Toolkit.Web
{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.Text;
    #endregion

    /// <summary>   
    /// The event arguments for any event related to a medication. 
    /// </summary>
    public class MedicationEventArgs : EventArgs
    {
        /// <summary>
        /// Medication Member
        /// </summary>
        private Medication medication;

        #region Constructors
        /// <summary>
        /// Constructs a MedicationEventArgs object and passes in the medication. 
        /// </summary>
        /// <param name="medication">The medication to be passed from the event source.</param>
        public MedicationEventArgs(Medication medication)
        {
            this.medication = medication;
        }

        /// <summary>
        /// Do not allow MedicationEventArgs to be constructed publically without any arguments
        /// </summary>
        private MedicationEventArgs()
        {
        }
        #endregion        

        #region Properties
        /// <summary>
        /// Returns an empty MedicationEventArgs object.
        /// </summary>
        public static MedicationEventArgs Unknown
        {
            get
            {
                return new MedicationEventArgs();
            }
        }

        /// <summary>
        /// The Medication argument.
        /// </summary>
        public Medication Medication
        {
            get
            {
                return this.medication;
            }
        }
        #endregion
    }
}