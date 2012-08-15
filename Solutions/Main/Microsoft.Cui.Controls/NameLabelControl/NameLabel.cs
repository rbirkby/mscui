//-----------------------------------------------------------------------
// <copyright file="NameLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>23-Apr-2008</date>
// <summary>The control used to display a name.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using NameLabelControl;
    using System.Windows.Automation.Peers;
    using System.ComponentModel;
    #endregion

    /// <summary>
    /// The control used to display the patient's name. 
    /// </summary>
    public class NameLabel : BaseLabel
    {
        #region Dependency Properties
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.NameLabel.FamilyName"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FamilyNameProperty = DependencyProperty.Register(
                                                                "FamilyName",
                                                                typeof(string),
                                                                typeof(NameLabel),
                                                                new PropertyMetadata(NameLabelResources.FamilyName, new PropertyChangedCallback(OnNamePropertyChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.NameLabel.GivenName"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty GivenNameProperty = DependencyProperty.Register(
                                                                "GivenName",
                                                                typeof(string),
                                                                typeof(NameLabel),
                                                                new PropertyMetadata(NameLabelResources.GivenName, new PropertyChangedCallback(OnNamePropertyChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.NameLabel.Title"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
                                                                "Title",
                                                                typeof(string),
                                                                typeof(NameLabel),
                                                                new PropertyMetadata(NameLabelResources.Title, new PropertyChangedCallback(OnNamePropertyChanged)));        
        #endregion              
        
        #region Constructor
        /// <summary>
        /// Initializes a new instance of NameLabel control.
        /// </summary>
        public NameLabel()
        {
            this.ShowHandOnHover = false;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the family name. 
        /// </summary>
        /// <remarks>
        /// The NameLabel control capitalizes the family name. The maximum display length is 40 characters. If the data exceeds 40 characters, 
        /// it is truncated to 37 characters plus an ellipsis.
        /// </remarks>
        /// <value>Family name.</value>
        [Category("Patient Name")]
        public string FamilyName
        {
            get { return (string)this.GetValue(FamilyNameProperty); }
            set { this.SetValue(FamilyNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the given name. 
        /// </summary>
        /// <remarks>
        /// The maximum display length is 40 characters. If the data exceeds 40 characters, it is truncated to 
        /// 37 characters plus an ellipsis. 
        /// </remarks>
        /// <value>Given name.</value>
        [Category("Patient Name")]
        public string GivenName
        {
            get { return (string)this.GetValue(GivenNameProperty); }
            set { this.SetValue(GivenNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the patient's title. 
        /// </summary>
        /// <remarks>
        /// The NameLabel control puts parentheses around the title. The title is only dispayed if one of 
        /// <see cref="P:NhsCui.Toolkit.Web.NameLabel.FamilyName">FamilyName</see> or 
        /// <see cref="P:NhsCui.Toolkit.Web.NameLabel.GivenName">GivenName</see>
        /// is provided. The maximum display length is 35 characters, including parentheses and three spaces.
        /// </remarks>
        /// <value>Title of the name.</value>
        [Category("Patient Name")]
        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Gets the constructed name as displayed.  
        /// </summary>
        /// <remarks>
        /// The patient's name takes the form "FAMILYNAME, GivenName (Title)". The maximum total display length is 120 characters. 
        /// </remarks>
        /// <value>Display value.</value>
        [Category("Patient Name")]
        public new string DisplayValue
        {
            get { return (string)this.GetValue(DisplayValueProperty); }
            internal set { this.SetValue(DisplayValueProperty, value); }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Update the display value based on supplied values.
        /// </summary>
        protected override void UpdateDisplayValue()
        {
            base.UpdateDisplayValue();
            this.CanChangeDisplayValue = true;
            this.DisplayValue = PatientName.Format(this.FamilyName, this.GivenName, this.Title);
            this.CanChangeDisplayValue = false;
        }
        #endregion

        #region Automation

        /// <summary>
        /// Automation object for the name label.
        /// </summary>
        /// <returns>Automation object for name label.</returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new NameLabelAutomationPeer(this);
        }

        #endregion

        #region Property Changed Callbacks
        /// <summary>
        /// Handles the property value changed callback events for name elements.
        /// </summary>
        /// <param name="d">NameLabel whose properties were changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                NameLabel nameLabel = d as NameLabel;

                if (nameLabel != null)
                {
                    if (e.Property == FamilyNameProperty || e.Property == GivenNameProperty || e.Property == TitleProperty)
                    {
                        nameLabel.UpdateDisplayValue();
                        NameLabelAutomationPeer peer;
#if SILVERLIGHT
                        peer = FrameworkElementAutomationPeer.FromElement(nameLabel) as NameLabelAutomationPeer;
#else
                        peer = UIElementAutomationPeer.FromElement(nameLabel) as NameLabelAutomationPeer;
#endif

                        if (peer != null)
                        {
                            peer.RaiseValueChangedEvent(e.OldValue.ToString(), e.NewValue.ToString());
                        }
                    }
                }
            }
        }
        #endregion
    }
}
