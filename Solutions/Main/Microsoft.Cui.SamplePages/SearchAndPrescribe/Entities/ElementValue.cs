//-----------------------------------------------------------------------
// <copyright file="ElementValue.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>24-Jul-2009</date>
// <summary>
//      A class representing an element value from an XML document.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System.ComponentModel;

    /// <summary>
    /// A class representing an element value from and XML document.
    /// </summary>
    public class ElementValue : INotifyPropertyChanged
    {
        /// <summary>
        /// Stores the value.
        /// </summary>
        private string value;

        /// <summary>
        /// The PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value 
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
                this.RaisePropertyChanged("Value");
                this.RaisePropertyChanged("DisplayValue");
            }
        }

        /// <summary>
        /// Gets the display value.
        /// </summary>
        public virtual string DisplayValue
        {
            get
            {
                return this.Value;
            }
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <returns>The string value.</returns>
        public override string ToString()
        {
            return this.DisplayValue;
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        internal void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
