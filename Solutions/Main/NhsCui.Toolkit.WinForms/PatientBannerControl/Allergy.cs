//-----------------------------------------------------------------------
// <copyright file="Allergy.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>30-Aug-2007</date>
// <summary>Sealed Allergy class- Used to identify the patient allergies.</summary>
//-----------------------------------------------------------------------


namespace NhsCui.Toolkit.WinForms
{
    #region Using
        using System;
        using System.Collections.Generic;
        using System.Text;
        using System.Runtime.Serialization;
        using System.ComponentModel;
    #endregion
    /// <summary>
    /// Allergy class
    /// </summary>
    public sealed class Allergy
    {
        #region Member Variables

        /// <summary>
        /// Allergy Separator
        /// </summary>
        public const string Separator = "  ";

        /// <summary>
        /// Allergetic to
        /// </summary>
        private string allergyName;

        /// <summary>
        /// Allergy last updated on
        /// </summary>
        private DateTime lastUpdated; 

        #endregion

        #region Constructors

        /// <summary>
        /// Create an empty allergy object
        /// </summary>
        public Allergy()
        {
        }

        /// <summary>
        /// Create an allergy object with allergy name
        /// </summary>
        /// <param name="allergyName">patient allergetic to</param>
        public Allergy(string allergyName)
        {
            this.allergyName = allergyName;
        }

        /// <summary>
        /// Create an allergy object with allergy name and last updated date
        /// </summary>
        /// <param name="allergyName">patient allergetic to</param>
        /// <param name="lastUpdatedOn">last updated on</param>
        public Allergy(string allergyName, DateTime lastUpdatedOn)
        {
            this.allergyName = allergyName;
            this.lastUpdated = lastUpdatedOn;
        }
        #endregion

        #region Public Properties
        
        /// <summary>
        /// Gets or Sets the substance to which the patient has an allergy propensity
        /// </summary>
        [DefaultValue("")]
        [Description("Allergy information")]
        [NotifyParentProperty(true)]
        public string AllergyName
        {
            get
            {
                return this.allergyName;
            }

            set
            {                
                this.allergyName = value;                
            }
        }
       
        /// <summary>
        /// Gets or sets the date when the entry in the record for this item was last updated
        /// </summary>
        [DefaultValue("")]
        [Description("Allergy last updated on")]
        [NotifyParentProperty(true)]
        public DateTime LastUpdatedOn
        {
            get
            {
                return this.lastUpdated;
            }

            set
            {
                this.lastUpdated = value;                 
            }
        }

        #endregion
               
        #region Public Methods
        /// <summary>
        /// Override method which returns the concatenated string. 
        /// </summary>
        /// <returns>The <see cref="P:NhsCui.Toolkit.Allergy.AllergyName">AllergyName</see> + the 
        /// <see cref="P:NhsCui.Toolkit.Allergy.Separator">Separator</see> + the 
        /// <see cref="P:NhsCui.Toolkit.Allergy.LastUpdatedOn">LastUpdatedOn</see>.
        /// </returns>
        public override string ToString()
        {
            return string.Join(Allergy.Separator, new string[] { this.allergyName, new DateAndTime.NhsDate(this.lastUpdated).ToString() });
        }

        /// <summary>
        /// Makes a deep copy of the Allergy object. 
        /// </summary>
        /// <returns>A deep copy of the Allergy object. </returns>
        public object Clone()
        {
            return new Allergy(this.allergyName, this.lastUpdated);
        }
        #endregion        
    }
}
