//-----------------------------------------------------------------------
// <copyright file="ContactLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>25-Apr-2008</date>
// <summary>The control used to hold contact details. </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using ContactLabelControl;
    using System.ComponentModel;
    using System.Windows.Media;
    using System.Text;
    using System.Windows.Automation.Peers;
    #endregion

    /// <summary>
    /// The control used to display contact details. 
    /// </summary>
    public class ContactLabel : Control
    {
        #region Dependency Properties
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.ContactLabel.HomePhoneLabelText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HomePhoneLabelTextProperty = DependencyProperty.Register(
                                                                                                    "HomePhoneLabelText",
                                                                                                    typeof(string),
                                                                                                    typeof(ContactLabel),
                                                                                                    null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.ContactLabel.WorkPhoneLabelText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty WorkPhoneLabelTextProperty = DependencyProperty.Register(
                                                                                                    "WorkPhoneLabelText",
                                                                                                    typeof(string),
                                                                                                    typeof(ContactLabel),
                                                                                                    null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.ContactLabel.MobilePhoneLabelText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MobilePhoneLabelTextProperty = DependencyProperty.Register(
                                                                                                    "MobilePhoneLabelText",
                                                                                                    typeof(string),
                                                                                                    typeof(ContactLabel),
                                                                                                    null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.ContactLabel.EmailLabelText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty EmailLabelTextProperty = DependencyProperty.Register(
                                                                                                    "EmailLabelText",
                                                                                                    typeof(string),
                                                                                                    typeof(ContactLabel),
                                                                                                    null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.ContactLabel.HomePhoneNumber"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HomePhoneNumberProperty = DependencyProperty.Register(
                                                                                                    "HomePhoneNumber",
                                                                                                    typeof(string),
                                                                                                    typeof(ContactLabel),
                                                                                                    null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.ContactLabel.WorkPhoneNumber"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty WorkPhoneNumberProperty = DependencyProperty.Register(
                                                                                                    "WorkPhoneNumber",
                                                                                                    typeof(string),
                                                                                                    typeof(ContactLabel),
                                                                                                    null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.ContactLabel.MobilePhoneNumber"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MobilePhoneNumberProperty = DependencyProperty.Register(
                                                                                                    "MobilePhoneNumber",
                                                                                                    typeof(string),
                                                                                                    typeof(ContactLabel),
                                                                                                    null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.ContactLabel.EmailAddress"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty EmailAddressProperty = DependencyProperty.Register(
                                                                                                    "EmailAddress",
                                                                                                    typeof(string),
                                                                                                    typeof(ContactLabel),
                                                                                                    null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.ContactLabel.LabelStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty LabelStyleProperty = DependencyProperty.Register(
                                                                                                    "LabelStyle",
                                                                                                    typeof(Style),
                                                                                                    typeof(ContactLabel),
                                                                                                    null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.ContactLabel.DataStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DataStyleProperty = DependencyProperty.Register(
                                                                                                    "DataStyle",
                                                                                                    typeof(Style),
                                                                                                    typeof(ContactLabel),
                                                                                                    null); 
        #endregion
        
        #region Constructors
        /// <summary>
        /// Initalizes a new Contact label control with default values.
        /// </summary>
        public ContactLabel()
        {            
            this.DefaultStyleKey = typeof(ContactLabel);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the caption associated with the contact's home phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Home". 
        /// </remarks>
        /// <value>Caption associated with home phone number.</value>
        [Category("Patient Contact")]
        public string HomePhoneLabelText
        {
            get { return (string)this.GetValue(HomePhoneLabelTextProperty); }
            set { this.SetValue(HomePhoneLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the caption associated with the contact's work phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Work". 
        /// </remarks>
        /// <value>Caption associated with work phone number.</value>
        [Category("Patient Contact")]
        public string WorkPhoneLabelText
        {
            get { return (string)this.GetValue(WorkPhoneLabelTextProperty); }
            set { this.SetValue(WorkPhoneLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the caption associated with the contact's mobile phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Mobile". 
        /// </remarks>
        /// <value>Caption associated with mobile phone number.</value>
        [Category("Patient Contact")]
        public string MobilePhoneLabelText
        {
            get { return (string)this.GetValue(MobilePhoneLabelTextProperty); }
            set { this.SetValue(MobilePhoneLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the caption associated with the contact's email address. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Email". 
        /// </remarks>
        /// <value>Caption associated with email address.</value>
        [Category("Patient Contact")]
        public string EmailLabelText
        {
            get { return (string)this.GetValue(EmailLabelTextProperty); }
            set { this.SetValue(EmailLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the contact's home phone number. 
        /// </summary>
        ///  <remarks>
        /// Defaults to "". If this element is left empty, <see cref="P:Microsoft.Cui.Controls.ContactLabel.HomePhoneLabelText">HomePhoneLabelText</see> 
        /// will be displayed with blank space next to it. 
        /// </remarks>    
        /// <value>Home phone number.</value>
        [Category("Patient Contact")]
        public string HomePhoneNumber
        {
            get { return (string)this.GetValue(HomePhoneNumberProperty); }
            set { this.SetValue(HomePhoneNumberProperty, value); }
        }

        /// <summary>
        /// Gets or sets the contact's work phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "". If this element is left empty, <see cref="P:Microsoft.Cui.Controls.ContactLabel.WorkPhoneLabelText">WorkPhoneLabelText</see> 
        /// will be displayed with blank space next to it. 
        /// </remarks>
        /// <value>Work phone number.</value>
        [Category("Patient Contact")]
        public string WorkPhoneNumber
        {
            get { return (string)this.GetValue(WorkPhoneNumberProperty); }
            set { this.SetValue(WorkPhoneNumberProperty, value); }
        }

        /// <summary>
        /// Gets or sets the contact's mobile phone number. 
        /// </summary>
        /// <remarks>
        /// Defaults to "". If this element is left empty, <see cref="P:Microsoft.Cui.Controls.ContactLabel.MobilePhoneLabelText">MobilePhoneLabelText</see> 
        /// will be displayed with blank space next to it. 
        /// </remarks>
        /// <value>Mobile phone number.</value>
        [Category("Patient Contact")]
        public string MobilePhoneNumber
        {
            get { return (string)this.GetValue(MobilePhoneNumberProperty); }
            set { this.SetValue(MobilePhoneNumberProperty, value); }
        }

        /// <summary>
        /// Gets or sets the contact's email address. 
        /// </summary>
        /// <remarks>
        /// Defaults to "". If this element is left empty, <see cref="P:Microsoft.Cui.Controls.ContactLabel.EmailLabelText">EmailLabelText</see> will be displayed 
        /// with blank space next to it. 
        /// </remarks>
        /// <value>Email address.</value>
        [Category("Patient Contact")]
        public string EmailAddress
        {
            get { return (string)this.GetValue(EmailAddressProperty); }
            set { this.SetValue(EmailAddressProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style for the label elements.
        /// </summary>
        /// <value>Style for the label elements.</value>
        /// <remarks>TargetType of the style should be TextBlock.</remarks>
        public Style LabelStyle
        {
            get { return (Style)this.GetValue(LabelStyleProperty); }
            set { this.SetValue(LabelStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style for the data elements.
        /// </summary>
        /// <value>Style for the data elements.</value>
        /// <remarks>TargetType of the style should be TextBlock.</remarks>
        public Style DataStyle
        {
            get { return (Style)this.GetValue(DataStyleProperty); }
            set { this.SetValue(DataStyleProperty, value); }
        }

        #endregion

        #region Internal Properties
        /// <summary>
        /// Gets the display text of the control.
        /// </summary>
        /// <value>Display text.</value>
        internal string DisplayText
        {
            get
            {
                StringBuilder text = new StringBuilder(this.HomePhoneLabelText);
                text.Append(" ");
                text.AppendLine(this.HomePhoneNumber);
                text.Append(this.WorkPhoneLabelText);
                text.Append(" ");
                text.AppendLine(this.WorkPhoneNumber);
                text.Append(this.MobilePhoneLabelText);
                text.Append(" ");
                text.AppendLine(this.MobilePhoneNumber);
                text.Append(this.EmailLabelText);
                text.Append(" ");
                text.AppendLine(this.EmailAddress);
                return text.ToString();
            }
        }
        #endregion        
        
        #region Automation

        /// <summary>
        /// Automation object for the contact label.
        /// </summary>
        /// <returns>Automation object for contact label.</returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new ContactLabelAutomationPeer(this);
        }

        #endregion        
    }
}
