//-----------------------------------------------------------------------
// <copyright file="PatientBannerEventArgs.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>18-Aug-07</date>
// <summary>Patient banner event args. Passed as arguments for all the events</summary>
//-----------------------------------------------------------------------


namespace NhsCui.Toolkit
{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.Text;
    #endregion

    /// <summary>
    /// Event arguments for any event related to patient banner
    /// </summary>
    public class PatientBannerEventArgs : EventArgs
    {
        #region Private Properties

        /// <summary>
        /// Identifier
        /// </summary>
        private string identifier;

        /// <summary>
        /// Gender of the patient
        /// </summary>
        private PatientGender gender;

        #endregion               

        #region Constructors

        /// <summary>
        /// constructs a PatientBannerEventArgs with the given Identifier
        /// </summary>
        /// <param name="identifier">Identifier</param>
        public PatientBannerEventArgs(string identifier)
        {
            this.identifier = identifier;
        }

        /// <summary>
        /// Constructs a PatientBannerEventArgs object with the given Identifier and Patient gender
        /// </summary>
        /// <param name="identifier">Identifier</param>
        /// <param name="gender">Patient gender</param>
        public PatientBannerEventArgs(string identifier, PatientGender gender)
        {
            this.identifier = identifier;            
            this.gender = gender;
        }

        /// <summary>
        /// Do not allow PatientBannerEventArgs object to be constructed publically without any arguments
        /// </summary>
        private PatientBannerEventArgs()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the Identifier as part of PatientBannerEventArgs
        /// </summary>
        public string Identifier
        {
            get
            {
                return this.identifier ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets the patient gender as part of PatientBannerEventArgs
        /// </summary>
        public PatientGender Gender
        {
            get
            {
                return this.gender;
            }
        }
        #endregion
    }
}
