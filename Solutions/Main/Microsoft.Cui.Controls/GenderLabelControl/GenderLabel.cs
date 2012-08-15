//-----------------------------------------------------------------------
// <copyright file="GenderLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to display a patient's gender. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using GenderLabelControl;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.ComponentModel;
    #endregion

    /// <summary>
    /// The control used to display a patient's gender. 
    /// </summary>
    public class GenderLabel : BaseLabel
    {
        #region Dependency Properties
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.GenderLabel.Value"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                                                                    "Value",
                                                                    typeof(PatientGender),
                                                                    typeof(GenderLabel),
                                                                    new PropertyMetadata(PatientGender.NotKnown, new PropertyChangedCallback(OnGenderPropertyChanged)));
        #endregion               

        #region Constructor
        /// <summary>
        /// Initializes a new instance of GenderLabel control.
        /// </summary>
        public GenderLabel()
        {
            this.ShowHandOnHover = false;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the patient's gender. 
        /// </summary>
        /// <remarks>
        /// This may be "Female", "Male", "NotSpecified" or "NotKnown". 
        /// </remarks>
        /// <value>Patient gender.</value>
        [Category("Behavior")]
        public PatientGender Value
        {
            get { return (PatientGender)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Gets the Patient gender display value.
        /// </summary>
        /// <value>Display value of the patient gender.</value>
        [Category("Behavior")]
        public new string DisplayValue
        {
            get { return (string)this.GetValue(DisplayValueProperty); }
            internal set { this.SetValue(DisplayValueProperty, value); }
        }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Overrides the on apply template method.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.CanChangeDisplayValue = true;
            this.SetValue(DisplayValueProperty, this.GetDisplayText());
            this.CanChangeDisplayValue = false;
        }
        #endregion

        #region Automation

        /// <summary>
        /// Automation object for the gender label.
        /// </summary>
        /// <returns>Automation object for gender label.</returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new GenderLabelAutomationPeer(this);
        }

        #endregion

        #region Property Changed Callbacks
        /// <summary>
        /// Handles the Gender value property changed event.
        /// </summary>
        /// <param name="d">Gender label whose gender has changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnGenderPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                GenderLabel genderLabel = d as GenderLabel;
                if (genderLabel != null || !typeof(GenderLabel).IsInstanceOfType(genderLabel))
                {
                   if (e.Property == ValueProperty)
                    {
                        if (Enum.IsDefined(typeof(PatientGender), e.NewValue))
                        {
                            genderLabel.CanChangeDisplayValue = true;
                            genderLabel.SetValue(DisplayValueProperty, genderLabel.GetDisplayText());
                            genderLabel.CanChangeDisplayValue = false;
                        }
                        else
                        {
                            genderLabel.Value = PatientGender.NotKnown;
                        }
                    }

                    GenderLabelAutomationPeer peer;
#if SILVERLIGHT
                    peer = FrameworkElementAutomationPeer.FromElement(genderLabel) as GenderLabelAutomationPeer;
#else
                    peer = UIElementAutomationPeer.FromElement(genderLabel) as GenderLabelAutomationPeer;
#endif

                    if (peer != null)
                    {
                        peer.RaiseValueChangedEvent(e.OldValue.ToString(), e.NewValue.ToString());
                    }
                }
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Gets the display text for a specified gender.
        /// </summary>
        /// <returns>Returns the Display text that can be shown.</returns>
        private string GetDisplayText()
        {
            string displayText = string.Empty;

            switch (this.Value)
            {
                case PatientGender.Female:
                    displayText = GenderControlResources.Female;
                    break;
                case PatientGender.Male:
                    displayText = GenderControlResources.Male;
                    break;
                case PatientGender.NotKnown:
                    displayText = GenderControlResources.NotKnown;
                    break;
                case PatientGender.NotSpecified:
                    displayText = GenderControlResources.NotSpecified;
                    break;
            }

            return displayText;
        }
        #endregion
    }
}
