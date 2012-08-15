//-----------------------------------------------------------------------
// <copyright file="WaitAnimation.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>01-Jul-2008</date>
// <summary>An Animation control that can be used to display indicate a wait. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    /// <summary>
    /// An Animation control that can be used to display indicate a wait.
    /// </summary>
    public partial class WaitAnimation : UserControl
    {
        /// <summary>
        /// Constructor for WaitAnimation control.
        /// </summary>
        public WaitAnimation()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Constructor for WaitAnimation control.
        /// </summary>
        /// <param name="message">
        /// Message to be displayed.
        /// </param>
        public WaitAnimation(string message)
        {
            this.InitializeComponent();
            this.LoadingTextBox.Text = message;
        }

        /// <summary>
        /// Start up the storyboard.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">EventArgs for the event.</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
#if SILVERLIGHT
            (this.Resources["OnLoaded1"] as Storyboard).Begin();
#else
            (this.Resources["OnLoaded1"] as Storyboard).Begin(this);
#endif
        }
    }
}
