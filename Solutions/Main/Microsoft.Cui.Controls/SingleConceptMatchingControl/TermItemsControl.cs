//-----------------------------------------------------------------------
// <copyright file="TermItemsControl.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>17-Dec-2008</date>
// <summary>The items control for binding indexed terms to.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    /// <summary>
    /// The items control for binding indexed terms to.
    /// </summary>
    internal class TermItemsControl : ItemsControl
    {
        /// <summary>
        /// The Start Index Member Path Dependency Property.
        /// </summary>
        public static readonly DependencyProperty StartIndexMemberPathProperty =
            DependencyProperty.Register("StartIndexMemberPath", typeof(string), typeof(TermItemsControl), null);

        /// <summary>
        /// The Length Member Path Dependency Property.
        /// </summary>
        public static readonly DependencyProperty LengthMemberPathProperty =
           DependencyProperty.Register("LengthMemberPath", typeof(string), typeof(TermItemsControl), null);

        /// <summary>
        /// The Is Selected Member Path Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsSelectedMemberPathProperty =
            DependencyProperty.Register("IsSelectedMemberPath", typeof(string), typeof(TermItemsControl), null);

        /// <summary>
        /// Stores a collection of the term items.
        /// </summary>
        private ObservableCollection<TermItem> termItems = new ObservableCollection<TermItem>();

        /// <summary>
        /// Stores the items by container.
        /// </summary>
        private Dictionary<TermItem, object> itemsByContainer = new Dictionary<TermItem, object>();

        /// <summary>
        /// Stores the containers by items.
        /// </summary>
        private Dictionary<object, TermItem> containersByItem = new Dictionary<object, TermItem>();

        /// <summary>
        /// The TermItems collection changed event.
        /// </summary>
        public event EventHandler TermItemsCollectionChanged;

        /// <summary>
        /// Gets the terms items.
        /// </summary>
        /// <value>An observable collection of IndexerItem.</value>
        public ObservableCollection<TermItem> TermItems
        {
            get { return this.termItems; }
        }

        /// <summary>
        /// Gets or sets the start index member path.
        /// </summary>
        /// <value>A string value.</value>
        public string StartIndexMemberPath
        {
            get { return (string)GetValue(StartIndexMemberPathProperty); }
            set { SetValue(StartIndexMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the length member path.
        /// </summary>
        /// <value>A string value.</value>
        public string LengthMemberPath
        {
            get { return (string)GetValue(LengthMemberPathProperty); }
            set { SetValue(LengthMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the IsSelected member path.
        /// </summary>
        /// <value>A string value.</value>
        public string IsSelectedMemberPath
        {
            get { return (string)GetValue(IsSelectedMemberPathProperty); }
            set { SetValue(IsSelectedMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets a term data object from a container.
        /// </summary>
        /// <param name="container">The TernItem container.</param>
        /// <returns>A term data object.</returns>
        public object GetItemFromContainer(TermItem container)
        {
            if (this.itemsByContainer.ContainsKey(container))
            {
                return this.itemsByContainer[container];
            }

            return null;
        }

        /// <summary>
        /// Gets a term item from a term data object.
        /// </summary>
        /// <param name="item">The term data object.</param>
        /// <returns>The term item container.</returns>
        public TermItem GetContainerFromItem(object item)
        {
            if (this.containersByItem.ContainsKey(item))
            {
                return this.containersByItem[item];
            }

            return null;
        }

        /// <summary>
        /// Updates the collection of term items.
        /// </summary>
        /// <param name="e">Notify Collection Changed Event Args.</param>
        protected override void OnItemsChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            this.termItems.Clear();
            this.itemsByContainer.Clear();
            this.containersByItem.Clear();
            foreach (object item in this.Items)
            {
                TermItem termItem = new TermItem();
                termItem.DataContext = item;
                termItem.SetBinding(TermItem.StartIndexProperty, new System.Windows.Data.Binding(this.StartIndexMemberPath));

                Binding lengthBinding = new Binding(this.LengthMemberPath);
                lengthBinding.Mode = BindingMode.OneTime;
                termItem.SetBinding(TermItem.LengthProperty, lengthBinding);
                
                termItem.SetBinding(TermItem.IsSelectedProperty, new System.Windows.Data.Binding(this.IsSelectedMemberPath));
                this.termItems.Add(termItem);
                this.itemsByContainer.Add(termItem, item);
                this.containersByItem.Add(item, termItem);
            }

            if (this.TermItemsCollectionChanged != null)
            {
                this.TermItemsCollectionChanged(this, EventArgs.Empty);
            }
        }
    }
}
