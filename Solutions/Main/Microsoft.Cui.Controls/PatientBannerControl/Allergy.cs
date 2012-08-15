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
// <date>21-May-2008</date>
// <summary>Sealed Allergy class- Used to identify the patient allergies.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.Serialization;
    using System.ComponentModel;
    #endregion

    /// <summary>
    /// Allergy Class. Used to identify patient allergies.
    /// </summary>
    public sealed class Allergy
    {
        #region Member Variables

        /// <summary>
        /// Separator for Allergy name and last updated date.
        /// </summary>
        public const string Separator = "  ";

        /// <summary>
        /// Allergy name.
        /// </summary>
        private string allergyName;

        /// <summary>
        /// Last updated on.
        /// </summary>
        private DateTime lastUpdated;

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates an empty allergy object.
        /// </summary>
        public Allergy()
        {
        }

        /// <summary>
        /// Instantiates an allergy object with the specified allergy name.
        /// </summary>
        /// <param name="allergyName">Allergy name.</param>
        public Allergy(string allergyName)
        {
            this.allergyName = allergyName;
        }

        /// <summary>
        /// Instantiates an allergy object with the specified allergy name and last updated time.
        /// </summary>
        /// <param name="allergyName">Allergy name.</param>
        /// <param name="lastUpdatedOn">Last updated on.</param>
        public Allergy(string allergyName, DateTime lastUpdatedOn)
        {
            this.allergyName = allergyName;
            this.lastUpdated = lastUpdatedOn;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the substance to which the patient has an allergy propensity.
        /// </summary>
        /// <value>Allergy name.</value>
        [DefaultValue("")]
        [Description("Allergy information")]
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
        /// Gets or sets the date when the entry in the record for this item was last updated.
        /// </summary>
        /// <value>Last updated date.</value>
        [DefaultValue("")]
        [Description("Allergy last updated on")]
        [TypeConverter(typeof(DateConverter))]
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
        /// <returns>The <see cref="P:Microsoft.Cui.Controls.Allergy.AllergyName">AllergyName</see> + the 
        /// <see cref="P:Microsoft.Cui.Controls.Allergy.Separator">Separator</see> + the 
        /// <see cref="P:Microsoft.Cui.Controls.Allergy.LastUpdatedOn">LastUpdatedOn</see>.
        /// </returns>
        public override string ToString()
        {
            if (this.lastUpdated == DateTime.MinValue)
            {
                return this.allergyName;
            }

            return string.Join(Allergy.Separator, new string[] { this.allergyName, new Microsoft.Cui.Controls.Common.DateAndTime.CuiDate(this.lastUpdated).ToString() });
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
