//-----------------------------------------------------------------------
// <copyright file="GroupingControl.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>20-Feb-2008</date>
// <summary>The GroupingControl class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Implementation of the GroupingControl class.
    /// </summary>
    public partial class GroupingControl : UserControl
    {
        /// <summary>
        /// Member variable to hold group by column name.
        /// </summary>
        private string groupByColumnName;

        /// <summary>
        /// Member variable to hold group by column tag.
        /// </summary>
        private string groupByColumnTag;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupingControl"/> class.
        /// </summary>
        public GroupingControl()
        {
            // Required to initialize variables
            this.InitializeComponent();
        }

        /// <summary>
        /// OnNewGrouping event.
        /// </summary>
        public event System.EventHandler<System.EventArgs> OnNewGrouping;

        /// <summary>
        /// Gets the group by column name.
        /// </summary>
        /// <value>Column text.</value>
        public string ColumnText
        {
            get
            {
                return this.groupByColumnName;
            }            
        }

        /// <summary>
        /// Gets the group by column tag.
        /// </summary>
        /// <value>Column tag.</value>
        public string ColumnTag
        {
            get
            {
                return this.groupByColumnTag;
            }
        }

        /// <summary>
        /// Handles the checked event for group by Drug name.
        /// </summary>
        /// <param name="sender">Drug name radio button.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void GroupByDrugName_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.groupByColumnName = this.groupByDrugName.Content.ToString();
            this.groupByColumnTag = this.groupByDrugName.Tag.ToString();

            this.RaiseOnNewGroupingEvent();
        }

        /// <summary>
        /// Handles the checked event for group by Prescription.
        /// </summary>
        /// <param name="sender">Prescription radio button.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void GroupByPrescription_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.groupByColumnName = this.groupByPrescription.Content.ToString();
            this.groupByColumnTag = this.groupByPrescription.Tag.ToString();

            this.RaiseOnNewGroupingEvent();
        }

        /// <summary>
        /// Handles the checked event for group by Status.
        /// </summary>
        /// <param name="sender">Status radio button.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void GroupByStatus_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.groupByColumnName = this.groupByStatus.Content.ToString();
            this.groupByColumnTag = this.groupByStatus.Tag.ToString();

            this.RaiseOnNewGroupingEvent();
        }

        /// <summary>
        /// Handles the checked event for group by Medication type.
        /// </summary>
        /// <param name="sender">Medication type radio button.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void GroupByMedicationType_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.groupByColumnName = this.groupByMedicationType.Content.ToString();
            this.groupByColumnTag = this.groupByMedicationType.Tag.ToString();

            this.RaiseOnNewGroupingEvent();
        }

        /// <summary>
        /// Raises OnNewGrouping event.
        /// </summary>
        private void RaiseOnNewGroupingEvent()
        {
            if (this.OnNewGrouping != null)
            {
                this.OnNewGrouping(this, new GroupByEventArgs(this.groupByColumnName, this.groupByColumnTag));
            }
        }
    }
}
