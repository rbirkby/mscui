//-----------------------------------------------------------------------
// <copyright file="Title.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>22-Jan-2007</date>
// <summary>Title class used by the Parser class.</summary>
//-----------------------------------------------------------------------


namespace NhsCui.Toolkit.PatientSearch
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel;
    using System.Globalization;

    /// <summary>
    /// Represents an NHS Title. 
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Serializable]
    public class Title : INotifyPropertyChanged
    {
        #region Member Vars

        /// <summary>
        /// Backing member var for Gender property
        /// </summary>
        private Gender gender = Gender.None;

        /// <summary>
        /// Backing member var for Name property
        /// </summary>
        private string name;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a Title object. 
        /// </summary>
        public Title()
        {
        }

        /// <summary>
        /// Constructs a Title object and initializes name from the <see cref="P:NhsCui.Toolkit.Web.PatientSearch.Title.Name">Name</see> parameter.
        /// </summary>
        /// <param name="name">The name of the Title. </param>
        public Title(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Constructs a Title object and initializes name from the <see cref="P:NhsCui.Toolkit.Web.PatientSearch.Title.Name">Name</see> parameter 
        /// and gender from the <see cref="P:NhsCui.Toolkit.Web.PatientSearch.Title.Gender">Gender</see> parameter. 
        /// </summary>
        /// <param name="name">The name of the title. </param>
        /// <param name="gender">The gender for the title. </param>
        public Title(string name, Gender gender)
        {
            this.Name = name;
            this.Gender = gender;
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        /// The gender associated with the title. 
        /// </summary>
        /// <remarks>
        /// Defaults to PatientSearch.Gender.None. 
        /// </remarks>
        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            set
            {
                this.gender = value;
                this.NotifyPropertyChanged("Gender");
            }
        }

        /// <summary>
        /// The name of the title.
        /// </summary>
        /// <remarks> 
        /// This may be, for example, Doctor. 
        /// </remarks>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(value);
                }

                if (value == null || value.Trim().Length == 0)
                {
                    throw new ArgumentException(value);
                }

                this.name = value;
                this.NotifyPropertyChanged("Name");
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the concatenated string. 
        /// </summary>
        /// <returns>Name + "[" + Gender + "]" 
        /// </returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} [{1}]", this.Name, this.Gender.ToString());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Notify Collection that a property has changed
        /// </summary>
        /// <param name="info">Property Name</param>
        private void NotifyPropertyChanged(string info)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion
    }
}
