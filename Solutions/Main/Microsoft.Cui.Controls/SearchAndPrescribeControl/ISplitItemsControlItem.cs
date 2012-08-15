//-----------------------------------------------------------------------
// <copyright file="ISplitItemsControlItem.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      An interface defining a split items control item.
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
    /// An interface defining a split items control item.
    /// </summary>
    public interface ISplitItemsControlItem
    {
        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        object Header
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the header template.
        /// </summary>
        DataTemplate HeaderTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the computed header visibility.
        /// </summary>
        Visibility ComputedHeaderVisibility
        {
            get;
        }

        /// <summary>
        /// Gets or sets the split border thickness.
        /// </summary>
        Thickness SplitBorderThickness
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the split border brush.
        /// </summary>
        Brush SplitBorderBrush
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the shortcut key text.
        /// </summary>
        string ShortcutKeyText
        {
            get;
            set;
        }
    }
}
