//-----------------------------------------------------------------------
// <copyright file="DecoratorTextBoxItem.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>12-Dec-2008</date>
// <summary>The text item object for the decorator text box.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    /// <summary>
    /// The text item object for the decorator text box.
    /// </summary>
    public class DecoratorTextBoxItem : INotifyPropertyChanged
    {
        /// <summary>
        /// Stores the item's text.
        /// </summary>
        private string text;

        /// <summary>
        /// Stores the term items.
        /// </summary>
        private Collection<TermItem> termItems = new Collection<TermItem>();

        /// <summary>
        /// Stores the container position.
        /// </summary>
        private ContainerPosition containerPosition = ContainerPosition.None;

        /// <summary>
        /// Stores if the text item is mouse highlighted.
        /// </summary>
        private bool mouseHighlighted;

        /// <summary>
        /// Stores if the text item is focus highlighted.
        /// </summary>
        private bool focusHighlighted;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DecoratorTextBoxItem"/> class.
        /// </summary>
        public DecoratorTextBoxItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DecoratorTextBoxItem"/> class specifying the text.
        /// </summary>
        /// <param name="text">The text to enter in the decorator text box item.</param>
        public DecoratorTextBoxItem(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the item's text.
        /// </summary>
        /// <value>A string value.</value>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
                this.RaisePropertyChanged("Text");
            }
        }

        /// <summary>
        /// Gets text item's term items.
        /// </summary>
        /// <value>A Collection of TermItems.</value>
        public Collection<TermItem> TermItems
        {
            get
            {
                return this.termItems;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this text item is encodable.
        /// </summary>
        /// <value>A bool value.</value>
        public bool IsEncodable
        {
            get
            {
                if (this.termItems.Count > 0)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether is encoded.
        /// </summary>
        /// <value>A bool value.</value>
        public bool IsEncoded
        {
            get
            {
                foreach (TermItem termItem in this.termItems)
                {
                    if (termItem.IsSelected)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Gets or sets the container position.
        /// </summary>
        /// <value>A container position.</value>
        public ContainerPosition ContainerPosition
        {
            get
            {
                return this.containerPosition;
            }

            set
            {
                this.containerPosition = value;
                this.RaisePropertyChanged("ContainerPosition");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the term is mouse highlighted or not.
        /// </summary>
        /// <value>A bool value.</value>
        public bool IsMouseHighlighted
        {
            get
            {
                return this.mouseHighlighted;
            }

            set
            {
                this.mouseHighlighted = value;
                this.RaisePropertyChanged("IsMouseHighlighted");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the term is focus highlighted or not.
        /// </summary>
        /// <value>A bool value.</value>
        public bool IsFocusHighlighted
        {
            get
            {
                return this.focusHighlighted;
            }

            set
            {
                this.focusHighlighted = value;
                this.RaisePropertyChanged("IsFocusHighlighted");
            }
        }

        /// <summary>
        /// Adds a term item to the the TermItem collection.
        /// </summary>
        /// <param name="termItem">The term item to add.</param>
        public void AddTermItem(TermItem termItem)
        {
            this.termItems.Add(termItem);
            termItem.SelectedChanged += new EventHandler(this.TermItem_SelectedChanged);
            this.RaisePropertyChanged("IsEncodable");
            this.RaisePropertyChanged("IsEncoded");
        }

        /// <summary>
        /// Clears the term items for this text item.
        /// </summary>
        public void ClearTermItems()
        {
            this.termItems.Clear();
            this.IsMouseHighlighted = false;
            this.IsFocusHighlighted = false;
            this.RaisePropertyChanged("IsEncodable");
            this.RaisePropertyChanged("IsEncoded");
        }

        /// <summary>
        /// Raises the IsEncoded property changed event.
        /// </summary>
        /// <param name="sender">The term item.</param>
        /// <param name="e">Event args.</param>
        private void TermItem_SelectedChanged(object sender, EventArgs e)
        {
            this.RaisePropertyChanged("IsEncoded");
        }

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
