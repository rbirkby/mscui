//-----------------------------------------------------------------------
// <copyright file="MedicationNameCollection.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// </copyright>u
// <date>29/01/2007</date>
// <summary>Custom Collection of MedicationNames. Provides validation of the full text of the medication name.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    #region Using
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Web.Script.Serialization;
    using System.Globalization;
    using System.Web.UI;
    using System.Collections;
    using System.Web;
    using System.Security.Permissions;
    using System.Collections.Generic;
    #endregion

    /// <summary>
    /// A custom collection of MedicationNames which is used to provide validation of the full text of the medication name.
    /// </summary>    
    [Serializable]
    public class MedicationNameCollection : Collection<MedicationName>, INotifyPropertyChanged, ICloneable
    {
        #region Members Vars
        /// <summary>
        /// The separator between the MedicationNames.
        /// </summary>
        /// <remarks>
        /// Defaults to " + ". 
        /// </remarks>
        [NonSerializedAttribute()]
        public const string MedicationNameSeparator = " + ";
        #endregion

        #region INotifyPropertyChanged Members
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        [field: NonSerializedAttribute()] 
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion        

        #region Properties
        /// <summary>
        /// Gets the total display length of the MedicationName entries including separators. 
        /// </summary>        
        public int DisplayLength
        {
            get
            {
                int totalDisplayLength = 0;
                
                if (this.Items == null || this.Items.Count == 0)
                {
                    return 0;
                }

                foreach (MedicationName medicationName in this.Items)
                {
                    int itemDisplayLength = medicationName.DisplayLength;
                    if (itemDisplayLength > 0)
                    {
                        if (totalDisplayLength > 0)
                        {
                            totalDisplayLength += MedicationNameCollection.MedicationNameSeparator.Length;
                        }

                        totalDisplayLength += itemDisplayLength;
                    }
                }
                
                return totalDisplayLength;
            }
        }

        /// <summary>
        /// Determines whether the data contained in MedicationName meets the length criteria set 
        /// by <see cref="F:NhsCui.Toolkit.Web.MedicationName.MaximumDisplayLength">MaximumDisplayLength</see>. 
        /// </summary>
        /// <returns>
        /// True if the data meets the <see cref="F:NhsCui.Toolkit.Web.MedicationName.MaximumDisplayLength">MaximumDisplayLength</see> criterion.
        /// </returns>        
        public bool IsValid
        {
            get
            {
                if (!this.ValidateNames())
                {
                    return false;
                }

                if (!this.ValidateLength())
                {
                    return false;
                }

                return true;
            }
        }
        #endregion

        #region ICloneable Members
        /// <summary>
        /// Makes a deep copy of the MedicationNameCollection. 
        /// </summary>
        /// <returns>A deep copy of the MedicationNameCollection. </returns>        
        public object Clone()
        {
            MedicationNameCollection newCollection = new MedicationNameCollection();
            foreach (MedicationName medicationName in this.Items)
            {
                newCollection.Add(medicationName.Clone() as MedicationName);
            }

            return newCollection;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Renders the text to be displayed for the MedicationNames. 
        /// </summary>
        /// <returns>The individual <see cref="P:NhsCui.Toolkit.Web.MedicationName.Name">Name</see> and 
        /// <see cref="P:NhsCui.Toolkit.Web.MedicationName.Information">Information</see> fields
        /// concatenated. </returns>
        public override string ToString()
        {
            bool renderedFirstItem = false;
            StringBuilder fullName = new StringBuilder();

            foreach (MedicationName medicationName in this.Items)
            {
                if (renderedFirstItem)
                {
                    fullName.Append(MedicationNameCollection.MedicationNameSeparator);
                }

                fullName.Append(medicationName.Name);

                if (!string.IsNullOrEmpty(medicationName.Information))
                {
                    fullName.Append(MedicationName.Separator);
                    fullName.Append(medicationName.Information);
                }

                renderedFirstItem = true;
            }

            return fullName.ToString();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Add Range of Medication Names
        /// </summary>
        /// <param name="medicationNames">Array of Medication Names</param>
        internal void AddRange(MedicationName[] medicationNames)
        {
            if (medicationNames == null)
            {
                throw new ArgumentNullException("medicationNames");
            }

            foreach (MedicationName name in medicationNames)
            {
                this.Items.Add(name);
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Override insert new Medication Name Item. Add OnPropertyChangedEvent
        /// </summary>
        /// <param name="index">Item Index</param>
        /// <param name="item">Medication Name to insert</param>
        protected override void InsertItem(int index, MedicationName item)
        {            
            item.PropertyChanged += new PropertyChangedEventHandler(this.OnPropertyChanged);
            base.InsertItem(index, item);
            this.EnsureLengthIsValid();
        }

        /// <summary>
        /// Override setting Medication Name Item. Add OnPropertyChangedEvent.
        /// </summary>
        /// <param name="index">Item Index</param>
        /// <param name="item">Medication Name to insert</param>
        protected override void SetItem(int index, MedicationName item)
        {
            if (item != null)
            {                
                item.PropertyChanged += new PropertyChangedEventHandler(this.OnPropertyChanged);                
            }            

            base.SetItem(index, item);
            this.EnsureLengthIsValid();
        }

        /// <summary>
        /// Override removing Medication Name Item. Remove OnPropertyChangedEvent.
        /// </summary>
        /// <param name="index">Item Index</param>
        protected override void RemoveItem(int index)
        {
            if (base[index] != null)
            {
                base[index].PropertyChanged -= new PropertyChangedEventHandler(this.OnPropertyChanged);
            }

            base.RemoveItem(index);
        }

        /// <summary>
        /// Override clearing Medication Name Item collection. Clear all OnPropertyChangedEvents.
        /// </summary>
        protected override void ClearItems()
        {
            foreach (MedicationName item in this.Items)
            {
                if (item != null)
                {
                    item.PropertyChanged -= new PropertyChangedEventHandler(this.OnPropertyChanged);
                }
            }

            base.ClearItems();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Raised when a property on a member in the collection changes
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">A PropertyChangedEventsArgs that contains the event data</param>
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.EnsureLengthIsValid();

            if (this.PropertyChanged != null)
            {
                // Retain the original sender - this will allow event subscriber to determine exactly 
                this.PropertyChanged(sender, e);
            }
        }

        /// <summary>
        /// Validation Medication Names        
        /// </summary>
        /// <returns>True if no MedicationName.Name fields are empty empty or null</returns>
        private bool ValidateNames()
        {
            // If any medication Name is empty, the MedicationNames are invalid
            foreach (MedicationName medicationName in this.Items)
            {
                if (string.IsNullOrEmpty(medicationName.Name))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validation Collection Length
        /// </summary>
        /// <returns>True if the length of the MedicationNames combined do not exceed the Maximum display length</returns>
        private bool ValidateLength()
        {            
            return (this.DisplayLength <= MedicationName.MaximumDisplayLength);
        }

        /// <summary>
        /// Validate the contents to ensure they conform to the length limits
        /// </summary>
        private void EnsureLengthIsValid()
        {
            if (!this.ValidateLength())
            {                                
                throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentUICulture, MedicationNameLabelControl.Resources.MedicationNamesDisplayLengthExceed, this.DisplayLength, MedicationName.MaximumDisplayLength));                
            }
        }
        #endregion
    }
}
