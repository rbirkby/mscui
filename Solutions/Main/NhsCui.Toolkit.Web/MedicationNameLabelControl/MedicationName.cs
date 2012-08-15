//-----------------------------------------------------------------------
// <copyright file="MedicationName.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>25-Jan-2007</date>
// <summary>Sealed Medication Name - Used to identify the drug name from other details which are a valid part of the medication name.</summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ComponentModel;
    using System.Runtime.Serialization;
    using System.Globalization;

    /// <summary>
    /// Used to identify the drug name from other details which are a valid part of the medication name. 
    /// </summary>    
    [Serializable]
    public sealed class MedicationName : INotifyPropertyChanged, ICloneable
    {
        #region Members Vars
        /// <summary>
        /// The separator character between the  <see cref="P:NhsCui.Toolkit.Web.MedicationName.Name">Name</see> and 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationName.Information">Information</see> used in HTML encoding. 
        /// </summary>
        /// <remarks>
        /// Defaults to two blank spaces. 
        /// </remarks>
        public const string SeparatorHtml = "&nbsp;&nbsp;";

        /// <summary>
        /// The separator between the <see cref="P:NhsCui.Toolkit.Web.MedicationName.Name">Name</see> and 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationName.Information">Information</see>. 
        /// </summary>
        /// <remarks>
        /// Defaults to a blank space. 
        /// </remarks>
        public const string Separator = "  ";

        /// <summary>
        /// The maximum display length allowed for the MedicationName. 
        /// </summary>
        /// <remarks> 
        /// This is set to 140 characters. 
        /// </remarks>
        public const int MaximumDisplayLength = 140;

        /// <summary>
        /// The name of the actual drug
        /// </summary>
        private string name;

        /// <summary>
        /// Additional information related to the medication name
        /// </summary>
        private string information;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a MedicationName object. 
        /// </summary>
        public MedicationName()
        {
        }

        /// <summary>
        /// Constructs a MedicationName object that passes name data on initialization. 
        /// </summary>
        /// <param name="name">The name of the medication. </param>        
        public MedicationName(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Constructs a MedicationName object that passes name and information data on initialization.  
        /// </summary>
        /// <param name="name">
        /// The name of the medication. </param>
        /// <param name="information">Information about the medication.</param>
        public MedicationName(string name, string information)
        {
            this.Name = name;
            this.Information = information;
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
        /// The name of the actual drug required. 
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new ArgumentNullException(MedicationNameLabelControl.Resources.NameField, MedicationNameLabelControl.Resources.NameCannotBeNullOrEmpty);
                }

                if ((this.name == null && value != null) || value != this.name)
                {
                    this.name = value;
                    this.EnsureValid();
                    this.NotifyPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Additional information related to the medication name. 
        /// </summary>
        /// <remarks>
        /// This is an optional field. 
        /// </remarks>
        public string Information
        {
            get
            {
                return this.information;
            }

            set
            {
                if ((this.information == null && value != null) || value != this.information)
                {
                    this.information = value;
                    this.EnsureValid();
                    this.NotifyPropertyChanged("Information");
                }
            }
        }

        /// <summary>
        /// Gets the length of the <see cref="P:NhsCui.Toolkit.Web.MedicationName.Name">Name</see> and 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationName.Information">Information</see>, including the
        /// <see cref="F:NhsCui.Toolkit.Web.MedicationName.Separator">Separator</see>.
        /// </summary>
        /// <returns>
        /// The length of the <see cref="P:NhsCui.Toolkit.Web.MedicationName.Name">Name</see> and 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationName.Information">Information</see>, including the
        /// <see cref="F:NhsCui.Toolkit.Web.MedicationName.Separator">Separator</see>.
        /// </returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int DisplayLength
        {
            get
            {
                int length = 0;
                if (false == string.IsNullOrEmpty(this.Name))
                {
                    length = this.Name.Length;
                }

                if (false == string.IsNullOrEmpty(this.Information))
                {
                    length += MedicationName.Separator.Length + this.Information.Length;
                }

                return length;
            }
        }

        /// <summary>
        /// Determines whether the data contained in the MedicationName meets the length criteria set 
        /// by <see cref="F:NhsCui.Toolkit.Web.MedicationName.MaximumDisplayLength">MaximumDisplayLength</see>.
        /// </summary>
        /// <returns>
        /// True if the data meets the <see cref="F:NhsCui.Toolkit.Web.MedicationName.MaximumDisplayLength">MaximumDisplayLength</see> criterion; 
        /// otherwise, false. 
        /// </returns>
        public bool IsValid
        {
            get
            {
                int totalDisplayLength = this.DisplayLength;
                return (totalDisplayLength <= MedicationName.MaximumDisplayLength);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Override method which returns the concatenated string. 
        /// </summary>
        /// <returns>The <see cref="P:NhsCui.Toolkit.Web.MedicationName.Name">Name</see> + the 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationName.Separator">Separator</see> + the 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationName.Information">Information</see>.
        /// </returns>
        public override string ToString()
        {
            // Note: This will return Separator + information if the name isn't specified.  
            // No assumption is being made in this class on the behavior if Name isn't provided.
            return string.Join(MedicationName.Separator, new string[] { this.Name, this.Information });
        }

        /// <summary>
        /// Makes a deep copy of the MedicationName object. 
        /// </summary>
        /// <returns>A deep copy of the MedicationName object. </returns>
        public object Clone()
        {
            return new MedicationName(this.Name, this.Information);
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

        /// <summary>
        /// Validate the contents to ensure they conform to the length limits
        /// </summary>
        private void EnsureValid()
        {
            if (!this.IsValid)
            {
                throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentUICulture, MedicationNameLabelControl.Resources.MedicationNamesDisplayLengthExceed, this.DisplayLength, MedicationName.MaximumDisplayLength));
            }
        }
        #endregion
    }
}
