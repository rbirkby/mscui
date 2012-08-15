//-----------------------------------------------------------------------
// <copyright file="DecoratorItemsControl.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The items control for display text item decorators.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// The items control for display text item decorators.
    /// </summary>
    [StyleTypedProperty(Property = "DecoratorItemContainerStyle", StyleTargetType = typeof(DecoratorItemContainer))]
    public class DecoratorItemsControl : ItemsControl
    {
        /// <summary>
        /// DecoratorItemContainerStyle Dependency Property.
        /// </summary>
        public static readonly DependencyProperty DecoratorItemContainerStyleProperty =
            DependencyProperty.Register("DecoratorItemContainerStyle", typeof(Style), typeof(DecoratorItemsControl), new PropertyMetadata(new PropertyChangedCallback(DecoratorItemContainerStyle_Changed)));

        /// <summary>
        /// Stores a list of the item containers.
        /// </summary>
        private Collection<DecoratorItemContainer> containers = new Collection<DecoratorItemContainer>();

        /// <summary>
        /// Stores the containers by item.
        /// </summary>
        private Dictionary<DecoratorTextBoxItem, DecoratorItemContainer> containersByItem = new Dictionary<DecoratorTextBoxItem, DecoratorItemContainer>();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DecoratorItemsControl"/> class.
        /// </summary>
        public DecoratorItemsControl()
        {
            this.DefaultStyleKey = typeof(DecoratorItemsControl);

            this.SizeChanged += new SizeChangedEventHandler(this.DecoratorItemsControl_SizeChanged);

            // Some differences in Margin rendering between Silverlight and WPF.
#if SILVERLIGHT
            this.Margin = new Thickness(0, 0, 1, 0);
            this.UseLayoutRounding = false;
#else
            this.Margin = new Thickness(1, 0, 1, 0);
#endif
        }

        /// <summary>
        /// Gets or sets the decorator item container style.
        /// </summary>
        /// <value>A style for a DecoratorItemContainer.</value>
        public Style DecoratorItemContainerStyle
        {
            get { return (Style)GetValue(DecoratorItemContainerStyleProperty); }
            set { SetValue(DecoratorItemContainerStyleProperty, value); }
        }

        /// <summary>
        /// Sets a containers ZIndex.
        /// </summary>
        /// <param name="item">The text box item.</param>
        /// <param name="index">The new Z-Index.</param>
        public void SetContainerZIndex(DecoratorTextBoxItem item, int index)
        {
            if (this.containersByItem.ContainsKey(item))
            {
                Canvas.SetZIndex(this.containersByItem[item], index);
            }
        }

        /// <summary>
        /// Gets a container from the item.
        /// </summary>
        /// <param name="item">The text item.</param>
        /// <returns>The holding container.</returns>
        internal DecoratorItemContainer GetContainerFromItem(DecoratorTextBoxItem item)
        {
            if (this.containersByItem.ContainsKey(item))
            {
                return this.containersByItem[item];
            }

            return null;
        }

        /// <summary>
        /// Updates the existing containers.
        /// </summary>
        internal void UpdateItemContainerStyle()
        {
            foreach (DecoratorItemContainer container in this.containers)
            {
                if (this.DecoratorItemContainerStyle != null)
                {
                    container.Style = this.DecoratorItemContainerStyle;
                }
            }
        }

        /// <summary>
        /// Generates a DecoratorItemContainer with template.
        /// </summary>
        /// <returns>A decorator item container.</returns>
        protected override DependencyObject GetContainerForItemOverride()
        {
            DecoratorItemContainer container = new DecoratorItemContainer();
            if (this.DecoratorItemContainerStyle != null)
            {
                container.Style = this.DecoratorItemContainerStyle;
            }

            if (this.containers.Count > 0)
            {
                container.Top = this.containers[containers.Count - 1].Top + this.containers[this.containers.Count - 1].ActualHeight;
            }

            this.containers.Add(container);
            return container;
        }

        /// <summary>
        /// Removes an entry from the item-container dictionary.
        /// </summary>
        /// <param name="element">The item container.</param>
        /// <param name="item">The text item.</param>
        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);
            DecoratorTextBoxItem textItem = (DecoratorTextBoxItem)item;

            DecoratorItemContainer container = (DecoratorItemContainer)element;
            this.containers.Remove(container);

            if (textItem != null && this.containersByItem.ContainsKey(textItem))
            {
                this.containersByItem.Remove(textItem);
            }
        }

        /// <summary>
        /// Sets up the bindings for whether a term is encodable or not, and stores
        /// in the item-container dictionary.
        /// </summary>
        /// <param name="element">The decorator container.</param>
        /// <param name="item">The text item.</param>
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            DecoratorItemContainer container = (DecoratorItemContainer)element;
            DecoratorTextBoxItem textItem = (DecoratorTextBoxItem)item;

            this.containersByItem.Add(textItem, container);

            container.DataContext = textItem;
            container.SetBinding(DecoratorItemContainer.IsEncodableProperty, new System.Windows.Data.Binding("IsEncodable"));
            container.SetBinding(DecoratorItemContainer.IsEncodedProperty, new System.Windows.Data.Binding("IsEncoded"));
            container.SetBinding(DecoratorItemContainer.ContainerPositionProperty, new System.Windows.Data.Binding("ContainerPosition"));
            container.SetBinding(DecoratorItemContainer.IsMouseHighlightedProperty, new System.Windows.Data.Binding("IsMouseHighlighted"));
            container.SetBinding(DecoratorItemContainer.IsFocusHighlightedProperty, new System.Windows.Data.Binding("IsFocusHighlighted"));
        }

        /// <summary>
        /// Updates the existing container styles.
        /// </summary>
        /// <param name="sender">The DecoratorItemsControl.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void DecoratorItemContainerStyle_Changed(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            DecoratorItemsControl itemsControl = (DecoratorItemsControl)sender;
            itemsControl.UpdateItemContainerStyle();
        }

        /// <summary>
        /// Updates the controls clip.
        /// </summary>
        /// <param name="sender">The decorator items control.</param>
        /// <param name="e">Size changed event args.</param>
        private void DecoratorItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Clip = new RectangleGeometry()
            {
                Rect = new Rect(-3.5, -3.5, e.NewSize.Width + 4, e.NewSize.Height + 4)
            };
        }
    }
}
