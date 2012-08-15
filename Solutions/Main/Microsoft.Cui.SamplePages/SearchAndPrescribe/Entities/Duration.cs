//-----------------------------------------------------------------------
// <copyright file="Duration.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010.
// All rights reserved
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
// <date>25-Sep-2009</date>
// <summary>
//      A class representing a duration (time span) with bindable value.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// A class representing a duration (time span) with bindable value.
    /// </summary>
    public class Duration : INotifyPropertyChanged
    {
        /// <summary>
        /// Stores the duration value.
        /// </summary>
        private TimeSpan? value;

        /// <summary>
        /// Stores the duration display value.
        /// </summary>
        private string displayValue;

        /// <summary>
        /// Stores if the duration is a custom value.
        /// </summary>
        private bool isCustomValue;

        /// <summary>
        /// The property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the duration value.
        /// </summary>
        public TimeSpan? Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
                this.RaisePropertyChanged("Value");
            }
        }

        /// <summary>
        /// Gets or sets the duration display value.
        /// </summary>
        public string DisplayValue
        {
            get
            {
                return this.displayValue;
            }

            set
            {
                this.displayValue = value;
                this.RaisePropertyChanged("DisplayValue");
            }
        }

        /// <summary>
        /// Gets or sets a value
        /// </summary>
        public bool IsCustomValue
        {
            get { return isCustomValue; }
            set { isCustomValue = value; }
        }

        /// <summary>
        /// Override for GetHashCode.
        /// </summary>
        /// <returns>The hash code value.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Overrides Equals.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>Whether the objects are equal.</returns>
        public override bool Equals(object obj)
        {
            Duration compareDuration = obj as Duration;

            if (compareDuration != null)
            {
                return this.IsCustomValue == compareDuration.IsCustomValue && this.Value.Equals(compareDuration.Value);
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <returns>The string value.</returns>
        public override string ToString()
        {
            return this.DisplayValue;
        }

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
