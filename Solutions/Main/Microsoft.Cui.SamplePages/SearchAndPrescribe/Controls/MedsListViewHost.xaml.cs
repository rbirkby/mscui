//-----------------------------------------------------------------------
// <copyright file="MedsListViewHost.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      The meds list view host page.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
using System.Collections.ObjectModel;

    /// <summary>
    /// The meds list view host page.
    /// </summary>
    public partial class MedsListViewHost : UserControl
    {
        /// <summary>
        /// Stores the collection of prescriptions.
        /// </summary>
        private ObservableCollection<CompletedPrescription> prescriptions = new ObservableCollection<CompletedPrescription>();

        /// <summary>
        /// MedsListViewHost constructor.
        /// </summary>
        public MedsListViewHost()
        {
            InitializeComponent();            
            this.WrapDataGrid.DataContext = this.prescriptions;            
            this.Loaded += new RoutedEventHandler(this.MedsListViewHost_Loaded);
        }

        /// <summary>
        /// Adds a prescription to the meds list view.
        /// </summary>
        /// <param name="prescription">The prescription to add.</param>
        public void AddPrescription(CompletedPrescription prescription)
        {            
            int index = 0;
            foreach (CompletedPrescription existingPrescription in this.prescriptions)
            {
                if (existingPrescription.StartDate.Date > prescription.StartDate.Date)
                {
                    index++;
                }
                else
                {
                    break;
                }
            }

            this.prescriptions.Insert(index, prescription);
            this.WrapDataGrid.Reload();
        }

        /// <summary>
        /// Updates the main grid.
        /// </summary>
        /// <param name="sender">The meds list view host.</param>
        /// <param name="e">Routed Event Args.</param>
        private void MedsListViewHost_Loaded(object sender, RoutedEventArgs e)
        {
            this.WrapDataGrid.Reload();
        }
    }
}
