//-----------------------------------------------------------------------
// <copyright file="CuiToggleButton.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>05-Sep-2008</date>
// <summary>Cui toggle button.</summary>
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
    using System.Windows.Controls.Primitives;

    /// <summary>
    /// CUI toggle button control.
    /// </summary>
    public class CuiToggleButton : ToggleButton
    {
        /// <summary>
        /// Initializes a new instance of CuiToggleButton control.
        /// </summary>
        public CuiToggleButton()
        {
            this.IsThreeState = false;
            this.Loaded += new RoutedEventHandler(this.CuiToggleButton_Loaded);          
        }      

        /// <summary>
        /// Gets the content in the normal state.
        /// </summary>
        /// <value>Content in the normal state.</value>
        public object NormalStateContent
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the content in the pressed state.
        /// </summary>
        /// <value>Content in the pressed state.</value>
        public object PressedStateContent
        {
            get;
            set;
        }

        /// <summary>
        /// Handles the loaded event for toggle button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void CuiToggleButton_Loaded(object sender, RoutedEventArgs e)
        {
            this.Checked += new RoutedEventHandler(this.CuiToggleButton_Checked);
            this.Unchecked += new RoutedEventHandler(this.CuiToggleButton_Unchecked);  

            if (true == this.IsChecked)
            {
                this.CuiToggleButton_Checked(sender, e);
            }
        }  

        /// <summary>
        /// Handles the unchecked event for toggle button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void CuiToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Content = this.NormalStateContent;
        }

        /// <summary>
        /// Handles the checked event for toggle button.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments containing instance data.</param>
        private void CuiToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.NormalStateContent == null)
            {
                this.NormalStateContent = this.Content;
            }

            this.Content = this.PressedStateContent;
        }
    }
}
