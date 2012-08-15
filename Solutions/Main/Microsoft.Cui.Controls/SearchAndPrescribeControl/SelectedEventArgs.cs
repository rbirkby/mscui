//-----------------------------------------------------------------------
// <copyright file="SelectedEventArgs.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>19-Aug-2009</date>
// <summary>
//      RoutedEventArgs with handled and selected item members.
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

    /// <summary>
    /// RoutedEventArgs with handled and selected item members.
    /// </summary>
    public class SelectedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// SelectedEventArgs constructor.
        /// </summary>
        public SelectedEventArgs()
        {            
        }

        /// <summary>
        /// SelectedEventArgs constructor with selected item.
        /// </summary>
        /// <param name="selectedItem">The selected item.</param>
        public SelectedEventArgs(object selectedItem)
        {
            this.SelectedItem = selectedItem;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the event has been handled.
        /// </summary>
        /// <value>The Handled value.</value>
        public bool Handled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item value.</value>
        public object SelectedItem
        {
            get;
            set;
        }
    }
}
