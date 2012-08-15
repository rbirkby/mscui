//-----------------------------------------------------------------------
// <copyright file="ISplitItemsControl.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>25-Aug-2009</date>
// <summary>
//      An interface defining a split items control.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.Collections;

    /// <summary>
    /// An interface defining a split items control.
    /// </summary>
    public interface ISplitItemsControl
    {
        /// <summary>
        /// The OtherSelected Event.
        /// </summary>
        event EventHandler OtherSelected;

        /// <summary>
        /// Gets or sets the primary items source.
        /// </summary>
        IEnumerable ItemsSource
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        object SelectedItem
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the primary items source.
        /// </summary>
        IEnumerable PrimaryItemsSource
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the secondary items source.
        /// </summary>
        IEnumerable SecondaryItemsSource
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the other item.
        /// </summary>
        object OtherItem
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the other item template.
        /// </summary>
        DataTemplate OtherItemTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the other items source.
        /// </summary>
        IEnumerable OtherItemsSource
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the other option should be shown.
        /// </summary>
        bool ShowOtherOption
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the list box should show the other items.
        /// </summary>
        bool ShowOtherItems
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the custom item.
        /// </summary>
        object CustomValueItem
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show the custom value item.
        /// </summary>
        bool ShowCustomValueOption
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the custom value item template.
        /// </summary>
        DataTemplate CustomValueItemTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the primary row background.
        /// </summary>
        Brush RowBackground
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the primary alternate row background.
        /// </summary>
        Brush AlternateRowBackground
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the primary items source header.
        /// </summary>
        object PrimaryItemsSourceHeader
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the secondary items source header.
        /// </summary>
        object SecondaryItemsSourceHeader
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the secondary items source header.
        /// </summary>
        object OtherItemsSourceHeader
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether shortcut keys should be added to primary items.
        /// </summary>
        bool AddShortcutKeysToPrimaryItems
        {
            get;
            set;
        }

        /// <summary>
        /// Adds a header to an item.
        /// </summary>
        /// <param name="item">The item to add the header too.</param>
        /// <param name="header">The header to add.</param>
        void AddItemHeader(object item, object header);

        /// <summary>
        /// Adds a shortcut key to an item.
        /// </summary>
        /// <param name="item">The item to add the key too.</param>
        /// <param name="key">The short cut key.</param>
        void AddItemShortcutKey(object item, Key? key);

        /// <summary>
        /// Removes a shortcut key from an item.
        /// </summary>
        /// <param name="item">The item to remove the shortcut key from.</param>
        /// <param name="key">The shortcut key to remove.</param>
        void RemoveItemShortcutKey(object item, Key? key);

        /// <summary>
        /// Adds a split to an item.
        /// </summary>
        /// <param name="item">The item to add the split to.</param>
        void AddItemSplit(object item);

        /// <summary>
        /// Removes a split to an item.
        /// </summary>
        /// <param name="item">The item to remove the split from.</param>
        void RemoveItemSplit(object item);
    }
}
