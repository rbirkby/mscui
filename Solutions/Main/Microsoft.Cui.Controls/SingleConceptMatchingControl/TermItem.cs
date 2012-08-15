//-----------------------------------------------------------------------
// <copyright file="TermItem.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>An item representing an term term.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Windows;

    /// <summary>
    /// An item representing an indexed term.
    /// </summary>
    public class TermItem : FrameworkElement
    {
        /// <summary>
        /// The start index Dependency Property.
        /// </summary>
        public static readonly DependencyProperty StartIndexProperty =
            DependencyProperty.Register("StartIndex", typeof(int), typeof(TermItem), new PropertyMetadata(-1));

        /// <summary>
        /// The length Dependency Property.
        /// </summary>
        public static readonly DependencyProperty LengthProperty =
            DependencyProperty.Register("Length", typeof(int), typeof(TermItem), null);

        /// <summary>
        /// The Is Selected Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(TermItem), new PropertyMetadata(new PropertyChangedCallback(IsSelected_Changed)));

        /// <summary>
        /// The selected changed event.
        /// </summary>
        public event EventHandler SelectedChanged;

        /// <summary>
        /// Gets or sets the indexed item's start index.
        /// </summary>
        /// <value>A int value.</value>
        public int StartIndex
        {
            get { return (int)GetValue(StartIndexProperty); }
            set { SetValue(StartIndexProperty, value); }
        }        

        /// <summary>
        /// Gets or sets the indexed item's length.
        /// </summary>
        /// <value>A int value.</value>
        public int Length
        {
            get { return (int)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item is selected.
        /// </summary>
        /// <value>A bool value.</value>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// Handles the Changed event of the IsSelected control.
        /// </summary>
        /// <param name="dependencyObject">The term item.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void IsSelected_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            TermItem termItem = (TermItem)dependencyObject;
            termItem.UpdateSelectedChanged();
        }

        /// <summary>
        /// Fires the Selected Change event.
        /// </summary>
        private void UpdateSelectedChanged()
        {
            if (this.SelectedChanged != null)
            {
                this.SelectedChanged(this, EventArgs.Empty);
            }
        }
    }
}
