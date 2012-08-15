//-----------------------------------------------------------------------
// <copyright file="AllergiesLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>04-June-2008</date>
// <summary>The control used to display allergies.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.Globalization;
    using System.ComponentModel;
    #endregion

    /// <summary>
    /// The control used to display allergies.
    /// </summary>
    public class AllergiesLabel : ItemsControl
    {      
        #region Dependency Properties     

        /// <summary>
        /// Identifies the AllergyOneProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty AllergiesProperty = DependencyProperty.Register(
            "Allergies",
            typeof(AllergyCollection),
            typeof(AllergiesLabel),
            new PropertyMetadata(new AllergyCollection(), new PropertyChangedCallback(OnAllergiesChanged)));      
        #endregion       

        #region Member vars
        /// <summary>
        /// Member variable to hold display items.
        /// </summary>
        private AllergyCollection displayItems = new AllergyCollection();

        /// <summary>
        /// Member variable to hold value for max allergies to show.
        /// </summary>
        private int maxAllergiesToShow = 5;
        #endregion        

        #region Public Properties
        /// <summary>
        /// Gets or sets the allergy details.
        /// </summary>        
        /// <value>Allergy collection.</value>                
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "This property needs to settable through XAML")]
        public AllergyCollection Allergies
        {
            get 
            {
                return (AllergyCollection)this.GetValue(AllergiesProperty); 
            }

            set 
            {
                this.SetValue(AllergiesProperty, value);
                this.RefreshDisplayItems();
            }
        }

        /// <summary>
        /// Gets the allergies to display.
        /// </summary>
        /// <value>Allergies to display.</value>
        public AllergyCollection DisplayItems
        {
            get { return this.displayItems; }
        }

        /// <summary>
        /// Gets or sets a value indicating maximum number of allergies to show.
        /// </summary>
        /// <value>Number of allergies to display.</value>        
        public int MaxAllergiesToShow
        {
            get 
            { 
                return this.maxAllergiesToShow; 
            }

            set
            {
                this.maxAllergiesToShow = value;
                this.RefreshDisplayItems();
            }
        }
        #endregion

        /// <summary>
        /// Applies the template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.ItemsSource = this.displayItems;
        }

        #region Automation

        /// <summary>
        /// Automation object for the name label.
        /// </summary>
        /// <returns>Automation object for name label.</returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new AllergiesLabelAutomationPeer(this);
        }

        #endregion

        /// <summary>
        /// Handles the allergies changed event.
        /// </summary>
        /// <param name="d">Allergies label whose allergies got changed.</param>
        /// <param name="e">Event args containing instance data.</param>
        private static void OnAllergiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AllergiesLabel allergiesLabel = d as AllergiesLabel;
            if (allergiesLabel != null)
            {
                allergiesLabel.RefreshDisplayItems();
            }
        }

        /// <summary>
        /// Refreshes the display items in view.
        /// </summary>
        private void RefreshDisplayItems()
        {
            this.displayItems.Clear();
            
            if (this.Allergies != null)
            {
                if (this.Allergies.Count > this.MaxAllergiesToShow)
                {
                    for (int i = 0; i < this.MaxAllergiesToShow - 1; i++)
                    {
                        this.displayItems.Add(this.Allergies[i]);
                    }

                    this.displayItems.Add(new Allergy(AllergiesLabelControl.AllergiesLabelResources.MoreAllergies));
                }
                else
                {
                    foreach (Allergy allergy in this.Allergies)
                    {
                        this.displayItems.Add(allergy);
                    }
                }
            }
        }
    }
}
