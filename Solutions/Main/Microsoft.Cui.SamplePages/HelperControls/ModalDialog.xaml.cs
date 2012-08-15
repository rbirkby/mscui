//-----------------------------------------------------------------------
// <copyright file="ModalDialog.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>13-Nov-2009</date>
// <summary>Class used to denote a modal dialog.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.SamplePages
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media.Animation;
    using System.Windows.Input;

    /// <summary>
    /// Modal dialog control.
    /// </summary>
    public partial class ModalDialog : ContentControl
    {
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.SamplePages.ModalDialog.DialogContent"/> dependency property
        /// </summary>
        public static readonly DependencyProperty DialogContentProperty =
            DependencyProperty.Register("DialogContent", typeof(object), typeof(ModalDialog), null);
        

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalDialog"/> class.
        /// </summary>
        public ModalDialog()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ModalDialog_Loaded);
        }

        /// <summary>
        /// Gets or sets the content of the dialog.
        /// </summary>
        /// <value>The content of the dialog.</value>
        public object DialogContent
        {
            get { return (object)GetValue(DialogContentProperty); }
            set { SetValue(DialogContentProperty, value); }
        }        

        /// <summary>
        /// Shows this instance.
        /// </summary>
        public void Show()
        {
            (this.Resources["ShowDialog"] as Storyboard).Begin();
#if SILVERLIGHT
            this.TabNavigation = KeyboardNavigationMode.Cycle;
#else
            KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.Cycle);
#endif

        }

        /// <summary>
        /// Hides this instance.
        /// </summary>
        public void Hide()
        {
            (this.Resources["HideDialog"] as Storyboard).Begin();
#if SILVERLIGHT
            this.TabNavigation = KeyboardNavigationMode.Once;
#else
            KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.None);
#endif

        }

        /// <summary>
        /// Handles the Loaded event of the ModalDialog control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ModalDialog_Loaded(object sender, RoutedEventArgs e)
        {
            Binding contentBinding = new Binding("DialogContent");
            contentBinding.Mode = BindingMode.OneWay;
            contentBinding.Source = this;
            this.ContentPlaceHolder.SetBinding(ContentControl.ContentProperty, contentBinding);
        }
    }
}
