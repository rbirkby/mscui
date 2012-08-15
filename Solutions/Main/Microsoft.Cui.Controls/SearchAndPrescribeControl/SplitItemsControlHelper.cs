//-----------------------------------------------------------------------
// <copyright file="SplitItemsControlHelper.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      Helper methods for working with split items controls.
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
    using System.Collections.Generic;
    using System.Collections;

    /// <summary>
    /// Helper methods for working with split items controls.
    /// </summary>
    public static class SplitItemsControlHelper
    {
        /// <summary>
        /// Updates the items source of a split items control.
        /// </summary>
        /// <param name="splitItemsControl">The split items control.</param>
        public static void UpdateItemsSource(ISplitItemsControl splitItemsControl)
        {            
#if !SILVERLIGHT
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(splitItemsControl as DependencyObject))
            {
                return;
            }   
#endif

            List<object> items = new List<object>();
            if (splitItemsControl.PrimaryItemsSource != null)
            {
                items.AddRange(IEnumerableHelper.GetItems(splitItemsControl.PrimaryItemsSource));
                if (items.Count > 0)
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (splitItemsControl.PrimaryItemsSourceHeader != null)
                        {
                            splitItemsControl.AddItemHeader(items[i], null);
                        }

                        if (splitItemsControl.AddShortcutKeysToPrimaryItems && i <= 10)
                        {
                            splitItemsControl.AddItemShortcutKey(items[i], ShortcutKeyHelper.GetKeyFromIndex(i));
                        }
                        else
                        {
                            splitItemsControl.RemoveItemShortcutKey(items[i], ShortcutKeyHelper.GetKeyFromIndex(i));
                        }

                        splitItemsControl.RemoveItemSplit(items[i]);
                    }

                    splitItemsControl.AddItemHeader(items[0], splitItemsControl.PrimaryItemsSourceHeader);
                }
            }

            if (splitItemsControl.SecondaryItemsSource != null)
            {
                if (items.Count > 0)
                {
                    splitItemsControl.AddItemSplit(items[items.Count - 1]);
                }

                int itemCount = items.Count;
                items.AddRange(IEnumerableHelper.GetItems(splitItemsControl.SecondaryItemsSource));

                if (splitItemsControl.SecondaryItemsSourceHeader != null && items.Count > itemCount)
                {
                    for (int i = itemCount; i < items.Count; i++)
                    {
                        splitItemsControl.AddItemHeader(items[i], null);
                        splitItemsControl.RemoveItemSplit(items[i]);
                    }

                    splitItemsControl.AddItemHeader(items[itemCount], splitItemsControl.SecondaryItemsSourceHeader);
                }
            }

            if (splitItemsControl.ShowCustomValueOption && splitItemsControl.CustomValueItem != null)
            {
                if (items.Count > 0)
                {
                    splitItemsControl.AddItemSplit(items[items.Count - 1]);
                }

                items.Add(splitItemsControl.CustomValueItem);
            }

            if (splitItemsControl.OtherItemsSource != null && splitItemsControl.ShowOtherItems)
            {
                if (items.Count > 0)
                {
                    splitItemsControl.AddItemSplit(items[items.Count - 1]);
                }

                int itemCount = items.Count;
                items.AddRange(IEnumerableHelper.GetItems(splitItemsControl.OtherItemsSource));
                if (splitItemsControl.OtherItemsSourceHeader != null && items.Count > itemCount)
                {
                    for (int i = itemCount; i < items.Count; i++)
                    {
                        splitItemsControl.AddItemHeader(items[i], null);
                        splitItemsControl.RemoveItemSplit(items[i]);
                    }

                    splitItemsControl.AddItemHeader(items[itemCount], splitItemsControl.OtherItemsSourceHeader);
                }
            }
            else if (splitItemsControl.ShowOtherOption)
            {
                if (items.Count > 0)
                {
                    splitItemsControl.AddItemSplit(items[items.Count - 1]);
                }

                items.Add(splitItemsControl.OtherItem);
            }

            splitItemsControl.ItemsSource = items;

#if !SILVERLIGHT
            if (splitItemsControl is SplitComboBox)
            {
                object selectedItem = null;
                foreach (object item in items)
                {
                    SplitComboBoxItem splitComboBoxItem = item as SplitComboBoxItem;
                    if (splitComboBoxItem != null && splitComboBoxItem.IsSelected)
                    {
                        selectedItem = item;
                        break;
                    }
                }

                if (selectedItem != null)
                {
                    splitItemsControl.SelectedItem = selectedItem;
                }
            }
#endif
        }
    }
}
