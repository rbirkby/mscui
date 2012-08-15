//-----------------------------------------------------------------------
// <copyright file="CascadingListBoxGroup.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>18-Aug-2009</date>
// <summary>
//      Cascading ListBox Group control.
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

    /// <summary>
    /// Cascading ListBox Group control.
    /// </summary>
    public class CascadingListBoxGroup : ItemsControl
    {
        /// <summary>
        /// The CascadingListBoxGroup Attached Property.
        /// </summary>
        public static readonly DependencyProperty CascadingListBoxGroupProperty =
            DependencyProperty.RegisterAttached("CascadingListBoxGroup", typeof(CascadingListBoxGroup), typeof(CascadingListBoxGroup), null);

        /// <summary>
        /// The ListVisibility Attached Property.
        /// </summary>
        public static readonly DependencyProperty ListVisibilityProperty =
            DependencyProperty.RegisterAttached("ListVisibility", typeof(Visibility), typeof(CascadingListBoxGroup), new PropertyMetadata(Visibility.Visible, new PropertyChangedCallback(ListVisibility_Changed)));

        /// <summary>
        /// The CascadingListBoxName Attached Property.
        /// </summary>
        public static readonly DependencyProperty CascadingListBoxNameProperty =
            DependencyProperty.RegisterAttached("CascadingListBoxName", typeof(string), typeof(CascadingListBoxGroup), new PropertyMetadata(null));

        /// <summary>
        /// Stores the containers by item.
        /// </summary>
        private Dictionary<object, ContentPresenter> containersByItem = new Dictionary<object, ContentPresenter>();

        /// <summary>
        /// Stores the containers by cascading list box.
        /// </summary>
        private Dictionary<ISplitItemsControl, ContentPresenter> containersBySplitItemsControl = new Dictionary<ISplitItemsControl, ContentPresenter>();

        /// <summary>
        /// CascadingListBoxGroup constructor.
        /// </summary>
        public CascadingListBoxGroup()
        {
            this.DefaultStyleKey = typeof(CascadingListBoxGroup);
        }

        /// <summary>
        /// Gets the cascading list box group from the element.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <returns>The cascading list box group.</returns>
        public static CascadingListBoxGroup GetCascadingListBoxGroup(DependencyObject obj)
        {
            return (CascadingListBoxGroup)obj.GetValue(CascadingListBoxGroupProperty);
        }

        /// <summary>
        /// Sets the cascading list box group.
        /// </summary>
        /// <param name="obj">The element.</param>
        /// <param name="value">The cascading list box group.</param>
        public static void SetCascadingListBoxGroup(DependencyObject obj, CascadingListBoxGroup value)
        {
            obj.SetValue(CascadingListBoxGroupProperty, value);
        }

        /// <summary>
        /// Gets the list visibility.
        /// </summary>
        /// <param name="obj">The element.</param>
        /// <returns>The visibility.</returns>
        public static Visibility GetListVisibility(DependencyObject obj)
        {
            return (Visibility)obj.GetValue(ListVisibilityProperty);
        }

        /// <summary>
        /// Sets a lists visibility.
        /// </summary>
        /// <param name="obj">The element.</param>
        /// <param name="value">The visibility.</param>
        public static void SetListVisibility(DependencyObject obj, Visibility value)
        {
            if (obj != null)
            {
                obj.SetValue(ListVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Gets the cascading list box name.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>A cascading list box name.</returns>
        public static string GetCascadingListBoxName(DependencyObject obj)
        {
            return (string)obj.GetValue(CascadingListBoxNameProperty);
        }

        /// <summary>
        /// Sets the cascading list box name.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The cascading list box name.</param>
        public static void SetCascadingListBoxName(DependencyObject obj, string value)
        {
            obj.SetValue(CascadingListBoxNameProperty, value);
        }

        /// <summary>
        /// Updates a cascading list boxes visibility.
        /// </summary>
        /// <param name="item">The sliding list box.</param>
        internal void UpdateListVisibility(DependencyObject item)
        {
            ISplitItemsControl splitItemsControl = item as ISplitItemsControl;
            if (splitItemsControl != null && this.containersBySplitItemsControl.ContainsKey(splitItemsControl))
            {
                this.containersBySplitItemsControl[splitItemsControl].Visibility = CascadingListBoxGroup.GetListVisibility(item);
            }
        }

        /// <summary>
        /// Always returns false to wrap the item in a cascading list box group item.
        /// </summary>
        /// <param name="item">The item object.</param>
        /// <returns>Always false.</returns>
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return false;
        }

        /// <summary>
        /// Override for preparing the container.
        /// </summary>
        /// <param name="element">The element dependency object.</param>
        /// <param name="item">The item object.</param>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            ContentPresenter container = element as ContentPresenter;
            this.containersByItem.Add(item, container);
            DependencyObject dependencyObject = item as DependencyObject;

            if (dependencyObject != null)
            {
                CascadingListBoxGroup.SetCascadingListBoxGroup(dependencyObject, this);
                LinkingWrapPanel.SetIsLinkedToNextChild(container, LinkingWrapPanel.GetIsLinkedToNextChild(dependencyObject));
                this.UpdateListVisibility(dependencyObject);
            }
            
#if SILVERLIGHT
            Canvas.SetZIndex(container, this.Items.Count - this.Items.IndexOf(item));
#endif
            ISplitItemsControl splitItemsControl = item as ISplitItemsControl;
            if (splitItemsControl == null)
            {
                FrameworkElement frameworkElement = item as FrameworkElement;

                if (frameworkElement != null && !string.IsNullOrEmpty(CascadingListBoxGroup.GetCascadingListBoxName(frameworkElement)))
                {
                     splitItemsControl = frameworkElement.FindName(CascadingListBoxGroup.GetCascadingListBoxName(frameworkElement)) as ISplitItemsControl;
                }
            }

            if (splitItemsControl != null)
            {
                CascadingListBoxGroup.SetCascadingListBoxGroup(splitItemsControl as DependencyObject, this);
                this.containersBySplitItemsControl.Add(splitItemsControl, container);
            }
        }

        /// <summary>
        /// Override for clearing a container.
        /// </summary>
        /// <param name="element">The element dependency object.</param>
        /// <param name="item">The item object.</param>
        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);
            this.containersByItem.Remove(item);

            ISplitItemsControl splitItemsControl = item as ISplitItemsControl;
            if (splitItemsControl == null)
            {
                FrameworkElement frameworkElement = item as FrameworkElement;
                if (frameworkElement != null && !string.IsNullOrEmpty(CascadingListBoxGroup.GetCascadingListBoxName(frameworkElement)))
                {
                    splitItemsControl = frameworkElement.FindName(CascadingListBoxGroup.GetCascadingListBoxName(frameworkElement)) as ISplitItemsControl;
                }
            }

            if (splitItemsControl != null)
            {
                this.containersBySplitItemsControl.Remove(splitItemsControl);
            }
        }

        /// <summary>
        /// Updates a list box's visibility.
        /// </summary>
        /// <param name="obj">The cascading list box.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void ListVisibility_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (CascadingListBoxGroup.GetCascadingListBoxGroup(obj) != null)
            {
                CascadingListBoxGroup.GetCascadingListBoxGroup(obj).UpdateListVisibility(obj);
            }
        }
    }
}
