//-----------------------------------------------------------------------
// <copyright file="IdentifierLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>23-Feb-2008</date>
// <summary>The control used to display a unique identifier. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Automation.Peers;
    using System.ComponentModel;

    #endregion

    /// <summary>
    /// The control used to display a unique identifier. 
    /// </summary>    
    public class IdentifierLabel : BaseLabel
    {
        #region Dependency Properties
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.IdentifierLabel.IdentifierType"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IdentifierTypeProperty = DependencyProperty.Register(
                                                                           "IdentifierType",
                                                                           typeof(IdentifierType),
                                                                           typeof(IdentifierLabel),
                                                                           new PropertyMetadata(new PropertyChangedCallback(OnIdentifierChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.IdentifierLabel.Text"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                                                                       "Text",
                                                                       typeof(string),
                                                                       typeof(IdentifierLabel),
                                                                       new PropertyMetadata(PatientBannerControl.PatientBannerResources.DefaultIdentifierValue, new PropertyChangedCallback(OnIdentifierChanged)));       

        #endregion

        #region Private members
        /// <summary>
        /// Member variable to hold whether the last identifier is valid.
        /// </summary>
        private bool lastIdentifierValid;
        #endregion        

        #region Constructor
        /// <summary>
        /// Initializes a new instance of IdentifierLabel control.
        /// </summary>
        public IdentifierLabel()
        {
            this.ShowHandOnHover = false;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets whether to process the identifier with the NhsNumber validation checksum. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Other". If this is set to "Other", 
        /// no validation is performed. If this is set to "NhsNumber", the text must be a valid NHS number.
        /// </remarks>
        /// <value>Identifier type.</value>
        [Category("Behavior")]
        public IdentifierType IdentifierType
        {
            get { return (IdentifierType)this.GetValue(IdentifierTypeProperty); }
            set { this.SetValue(IdentifierTypeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text associated with the IdentifierLabel control. Must be set to a valid NhsNumber if the control’s IdentifierType is NhsNumber.
        /// </summary>
        /// <remarks>
        /// Defaults to "xxx-xxx-xxxx". This property is mandatory if 
        /// <see cref="P:Microsoft.Cui.Controls.IdentifierLabel.IdentifierType">IdentifierType</see> is set to "NhsNumber". 
        /// </remarks>
        /// <value>String representing the identifier value.</value>
        [Category("Behavior")]        
        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets a value indicating whether the last identifier set is a valid identifier.
        /// </summary>
        /// <remarks>This will always return true if the IdentifierType is Other.</remarks>
        /// <value>Boolean indicating whether the last identifier is valid.</value>
        [Category("Behavior")]
        public bool LastIdentifierValid
        {
            get { return this.lastIdentifierValid; }
        }

        /// <summary>
        /// Gets the formatted Identifier text . If the Identifier is of type NhsNumber, then the identifier will be formatted as a NHS number.
        /// </summary>
        /// <remarks>If the IdentifierType is Other, then DisplayValue will be same as Text. If the IdentifierType is NhsNumber, then Identifier will be formatted in the form of a NHS number.</remarks>
        /// <value>String representing a formatted identifier.</value>
        [Category("Behavior")]
        public new string DisplayValue
        {
            get { return (string)this.GetValue(DisplayValueProperty); }
            internal set { this.SetValue(DisplayValueProperty, value); }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Updates the display value of the Identifier label control.
        /// </summary>
        protected override void UpdateDisplayValue()
        {
            try
            {
                this.Validate();
                this.CanChangeDisplayValue = true;

                if (this.IdentifierType == IdentifierType.NhsNumber && !string.IsNullOrEmpty(this.Text) && string.Compare(this.Text, PatientBannerControl.PatientBannerResources.DefaultIdentifierValue, StringComparison.CurrentCultureIgnoreCase) != 0)
                {
                    this.SetValue(DisplayValueProperty, new NhsNumber(this.Text).ToString());
                }
                else
                {
                    this.SetValue(DisplayValueProperty, this.Text);
                }

                this.CanChangeDisplayValue = false;
            }
            catch (ArgumentException)
            {
                this.CanChangeDisplayValue = true;
                this.SetValue(DisplayValueProperty, string.Empty);
                this.CanChangeDisplayValue = false;
                throw;
            }
        }
        #endregion

        #region Automation

        /// <summary>
        /// Automation object for the identifier label.
        /// </summary>
        /// <returns>Automation object for identifier label.</returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new IdentifierLabelAutomationPeer(this);
        }

        #endregion        

        #region Property Changed Callbacks
        /// <summary>
        /// Handles the Identifier changed event.
        /// </summary>
        /// <param name="d">Identifier Label whose Identifier is changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnIdentifierChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                IdentifierLabel identifierLabel = d as IdentifierLabel;
                if (identifierLabel != null || !typeof(IdentifierLabel).IsInstanceOfType(identifierLabel))
                {
                    identifierLabel.UpdateDisplayValue();
                }

                IdentifierLabelAutomationPeer peer;
#if SILVERLIGHT
                peer = FrameworkElementAutomationPeer.FromElement(identifierLabel) as IdentifierLabelAutomationPeer;
#else
                peer = UIElementAutomationPeer.FromElement(identifierLabel) as IdentifierLabelAutomationPeer;
#endif

                if (peer != null)
                {
                    peer.RaiseValueChangedEvent(e.OldValue.ToString(), e.NewValue.ToString());
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Validates whether a given identifier is a valid NhsNumber.
        /// </summary>        
        /// <remarks>ArgumentException will be raised if the IdentifierType is NhsNumber and Text is not a valid NhsNumber</remarks>
        private void Validate()
        {
            if (this.IdentifierType == IdentifierType.NhsNumber && string.Compare(this.Text, PatientBannerControl.PatientBannerResources.DefaultIdentifierValue, StringComparison.CurrentCultureIgnoreCase) != 0)
            {
                try
                {
                    NhsNumber.ParseNhsNumber(this.Text);
                }
                catch (ArgumentException)
                {
                    this.lastIdentifierValid = false;
                    throw;
                }                
            }

            this.lastIdentifierValid = true;            
        }
        #endregion
    }
}
