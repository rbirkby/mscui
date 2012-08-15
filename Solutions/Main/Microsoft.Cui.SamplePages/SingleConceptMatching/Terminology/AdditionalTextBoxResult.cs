//-----------------------------------------------------------------------
// <copyright file="AdditionalTextBoxResult.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>09/12/2008</date>
// <summary>Contains the data required to hightlight terms in the additional textbox.</summary>
//-----------------------------------------------------------------------
#if SILVERLIGHT
namespace Microsoft.Cui.SamplePages
#else
namespace Microsoft.Cui.SampleWinform
#endif
{
    #region Using    
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
#if SILVERLIGHT
    using Microsoft.Cui.SamplePages.TerminologyProvider;
#else
    using Microsoft.Cui.SampleWinform.TerminologyProvider;
#endif

    #endregion
    /// <summary>
    /// Contains the data required to hightlight terms in the additional textbox.
    /// </summary>
    public class AdditionalTextBoxResult : INotifyPropertyChanged
    {
        /// <summary>
        /// Backing field for StartIndex property.
        /// </summary>
        private long startIndex;

        /// <summary>
        /// Backing field for length property.
        /// </summary>
        private long length;

        /// <summary>
        /// Backing field for IsSelected property.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// Backing field for IsSide property.
        /// </summary>
        private bool isSide;

        /// <summary>
        /// Backing field for SelectedItem property.
        /// </summary>
        private ConceptResult selectedItem;

        /// <summary>
        /// Backing field for FindingSite property.
        /// </summary>
        private ObservableCollection<ConceptDetail> findingSites = new ObservableCollection<ConceptDetail>();

        /// <summary>
        /// The PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the start index.
        /// </summary>
        /// <value>The start index.</value>
        public long StartIndex
        {
            get
            {
                return this.startIndex;
            }

            set
            {
                this.startIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        /// <value>The length.</value>
        public long Length
        {
            get
            {
                return this.length;                
            }

            set
            {
                this.length = value;
            }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public ConceptResult SelectedItem
        {
            get
            {
                return this.selectedItem;
            }

            set
            {                
                this.selectedItem = value;

                this.RaisePropertyChanged("SelectedItem");
            }
        }
        
        /// <summary>
        /// Gets or sets the alternate items.
        /// </summary>
        /// <value>The alternate items.</value>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Required by Spec.")]
        public ObservableCollection<ConceptResult> AlternateItems { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>Is <c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.isSelected = value;
                this.RaisePropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is side.
        /// </summary>
        /// <value>Is <c>true</c> if this instance is side; otherwise, <c>false</c>.</value>
        public bool IsSide
        {
            get
            {
                return this.isSide;
            }

            set
            {
                this.isSide = value;
                this.RaisePropertyChanged("IsSide");
            }
        }

        /// <summary>
        /// Gets or sets the finding site.
        /// </summary>
        /// <value>The finding site.</value>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Required by Spec.")]
        public ObservableCollection<ConceptDetail> FindingSites
        {
            get
            {
                return this.findingSites;
            }

            set
            {
                this.findingSites = value;
                this.RaisePropertyChanged("FindingSites");
            }
        }

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }        
    }
}
