//-----------------------------------------------------------------------
// <copyright file="DataSelectorItem.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>30-Oct-2008</date>
// <summary>Class used to represent a data selector item.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
using System.Windows.Media;

    /// <summary>
    /// Class used to represent a data selector item.
    /// </summary>
    public class DataSelectorItem : ContentControl
    {
        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.DataSelectorItem.HoverBackground" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register("HoverBackground", typeof(Brush), typeof(DataSelectorItem), null);

        /// <summary>
        /// Member variable to hold focus visual.
        /// </summary>
        private UIElement focusVisual;

        /// <summary>
        /// Member variable to hold the root element.
        /// </summary>
        private Panel layoutRoot;

        /// <summary>
        /// Initializes a new instance of Data Selector Item.
        /// </summary>
        public DataSelectorItem()
        {
            this.GotFocus += new RoutedEventHandler(this.DataSelectorItem_GotFocus);
            this.LostFocus += new RoutedEventHandler(this.DataSelectorItem_LostFocus);
        }        

        /// <summary>
        /// Gets or sets the background for the data selector item on hover.
        /// </summary>
        /// <value>Background brush to be applied on hover.</value>
        public Brush HoverBackground
        {
            get { return (Brush)this.GetValue(HoverBackgroundProperty); }
            set { this.SetValue(HoverBackgroundProperty, value); }
        }

        /// <summary>
        /// Overridden. Applies a specified template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.focusVisual = (this.Content as FrameworkElement).FindName("FocusVisualElement") as UIElement;
            this.layoutRoot = (this.Content as FrameworkElement).FindName("LayoutRoot") as Panel;
        }

        /// <summary>
        /// Clears the hover style applied to data selector item.
        /// </summary>
        internal void ClearHoverStyle()
        {
            if (this.layoutRoot != null)
            {
                this.layoutRoot.Background = this.Background;
            }
        }

        /// <summary>
        /// Handles the mouse enter event on the data selector item.
        /// </summary>
        /// <param name="e">Event args containing instance data.</param>
        protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.Cursor = Cursors.Hand;

            if (this.layoutRoot != null && this.HoverBackground != null)
            {
                this.layoutRoot.Background = this.HoverBackground;
            }
        }

        /// <summary>
        /// Handles the mouse leave event on the data selector item.
        /// </summary>
        /// <param name="e">Event args containing instance data.</param>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            bool clearHoverStyle = true;
#if !SILVERLIGHT
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                clearHoverStyle = false;
            }
#endif
            if (clearHoverStyle)
            {
                this.Cursor = null;
                this.ClearHoverStyle();
            }
        }

        /// <summary>
        /// Overridden. Handles the Mouse left button down event.
        /// </summary>
        /// <param name="e">Event arguments containing instance data.</param>
        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.Focus();
        } 

        /// <summary>
        /// Handles the Got focus event.
        /// </summary>
        /// <param name="sender">Sender of the Event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void DataSelectorItem_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.focusVisual != null)
            {
#if !SILVERLIGHT
                (e.OriginalSource as FrameworkElement).FocusVisualStyle = null;
#endif
                this.focusVisual.Opacity = 1;
            }
        }

        /// <summary>
        /// Handles the Lost focus event.
        /// </summary>
        /// <param name="sender">Sender of the Event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void DataSelectorItem_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.focusVisual != null)
            {
                this.focusVisual.Opacity = 0;
            }
        }       
    }
}
