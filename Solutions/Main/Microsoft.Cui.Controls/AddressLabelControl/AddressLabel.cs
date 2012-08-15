//-----------------------------------------------------------------------
// <copyright file="AddressLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>The control used to hold an address. </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Globalization;
    using System.Text;
    using System.Windows.Automation.Peers;
using System.ComponentModel;
    #endregion

    /// <summary>
    /// The control used to display an address.
    /// </summary>
    public class AddressLabel : Control
    {
        #region Dependency Properties
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.AddressLabel.AddressDisplayFormat"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AddressDisplayFormatProperty = DependencyProperty.Register(
                                                                                    "AddressDisplayFormat",
                                                                                    typeof(AddressDisplayFormat),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressDisplayFormatChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.AddressLabel.Address1"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty Address1Property = DependencyProperty.Register(
                                                                                    "Address1",
                                                                                    typeof(string),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.AddressLabel.Address2"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty Address2Property = DependencyProperty.Register(
                                                                                    "Address2",
                                                                                    typeof(string),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.AddressLabel.Address3"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty Address3Property = DependencyProperty.Register(
                                                                                    "Address3",
                                                                                    typeof(string),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.AddressLabel.Town"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TownProperty = DependencyProperty.Register(
                                                                                    "Town",
                                                                                    typeof(string),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.AddressLabel.County"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CountyProperty = DependencyProperty.Register(
                                                                                    "County",
                                                                                    typeof(string),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.AddressLabel.Country"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CountryProperty = DependencyProperty.Register(
                                                                                    "Country",
                                                                                    typeof(string),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.AddressLabel.Postcode"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PostcodeProperty = DependencyProperty.Register(
                                                                                    "Postcode",
                                                                                    typeof(string),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.AddressLabel.InlineAddressDisplayText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty InlineAddressDisplayTextProperty = DependencyProperty.Register(
                                                                                    "InlineAddressDisplayText",
                                                                                    typeof(string),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnInlineAddressDisplayTextChanged)));

        /// <summary>
        /// Identifies the PostcodeDisplayValue dependency property.
        /// </summary>
        public static readonly DependencyProperty PostcodeDisplayValueProperty = DependencyProperty.Register(
                                                                                    "PostcodeDisplayValue",
                                                                                    typeof(string),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnPostcodeDisplayValueChanged)));
                
        /// <summary>
        /// Identifies the AddressLine1VisibilityProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty AddressLine1VisibilityProperty = DependencyProperty.Register(
                                                                                    "AddressLine1Visibility",
                                                                                    typeof(Visibility),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressPartVisibilityChanged)));

        /// <summary>
        /// Identifies the AddressLine2VisibilityProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty AddressLine2VisibilityProperty = DependencyProperty.Register(
                                                                                    "AddressLine2Visibility",
                                                                                    typeof(Visibility),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressPartVisibilityChanged)));

        /// <summary>
        /// Identifies the AddressLine3VisibilityProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty AddressLine3VisibilityProperty = DependencyProperty.Register(
                                                                                    "AddressLine3Visibility",
                                                                                    typeof(Visibility),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressPartVisibilityChanged)));

        /// <summary>
        /// Identifies the TownVisibilityProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty TownVisibilityProperty = DependencyProperty.Register(
                                                                                    "TownVisibility",
                                                                                    typeof(Visibility),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressPartVisibilityChanged)));

        /// <summary>
        /// Identifies the CountyVisibilityProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty CountyVisibilityProperty = DependencyProperty.Register(
                                                                                    "CountyVisibility",
                                                                                    typeof(Visibility),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressPartVisibilityChanged)));

        /// <summary>
        /// Identifies the CountryVisibilityProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty CountryVisibilityProperty = DependencyProperty.Register(
                                                                                    "CountryVisibility",
                                                                                    typeof(Visibility),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressPartVisibilityChanged)));

        /// <summary>
        /// Identifies the PostcodeVisibilityProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty PostcodeVisibilityProperty = DependencyProperty.Register(
                                                                                    "PostcodeVisibility",
                                                                                    typeof(Visibility),
                                                                                    typeof(AddressLabel),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressPartVisibilityChanged)));
        #endregion

        #region Private Members
        /// <summary>
        /// Member variable to indicate whether postcode display value can be updated.
        /// </summary>
        private bool canChangePostcodeDisplayValue;

        /// <summary>
        /// Member variable to indicate whether Inline address display value can be updated.
        /// </summary>
        private bool canChangeInlineAddressDisplayValue;

        /// <summary>
        /// Member variable to indicate whether address parts visibility can be updated.
        /// </summary>
        private bool canChangeAddressPartVisibility;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new address label control with default values.
        /// </summary>
        public AddressLabel()
        {
            this.DefaultStyleKey = typeof(AddressLabel);
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the address layout to be either "inform" (vertical) or "inline" (horizontal).
        /// </summary>
        /// <remarks>
        /// If the address layout is set to "inform", each line contains a single, left-justified element with 
        /// no separator characters displayed. If the address layout is set to "inline", multiple elements display on a 
        /// single line, with address elements separated by a single comma and a single space. Individual address 
        /// elements should not split across multiple lines. 
        /// </remarks>
        /// <value>Type of address display format.</value>
        [Category("Patient Address")]
        public AddressDisplayFormat AddressDisplayFormat
        {
            get { return (AddressDisplayFormat)this.GetValue(AddressDisplayFormatProperty); }
            set { this.SetValue(AddressDisplayFormatProperty, value); }
        }

        /// <summary>
        /// Gets or sets the first line of an address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        /// <value>First line of address.</value>
        [Category("Patient Address")]
        public string Address1
        {
            get { return (string)this.GetValue(Address1Property); }
            set { this.SetValue(Address1Property, value); }
        }

        /// <summary>
        /// Gets or sets the second line of an address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        /// <value>Second line of address.</value>
        [Category("Patient Address")]
        public string Address2
        {
            get { return (string)this.GetValue(Address2Property); }
            set { this.SetValue(Address2Property, value); }
        }

        /// <summary>
        /// Gets or sets the third line of an address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        /// <value>Third line of address.</value>
        [Category("Patient Address")]
        public string Address3
        {
            get { return (string)this.GetValue(Address3Property); }
            set { this.SetValue(Address3Property, value); }
        }

        /// <summary>
        /// Gets or sets the town in an address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        /// <value>Town in address.</value>
        [Category("Patient Address")]
        public string Town
        {
            get { return (string)this.GetValue(TownProperty); }
            set { this.SetValue(TownProperty, value); }
        }

        /// <summary>
        /// Gets or sets the county in an address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        /// <value>County in address.</value>
        [Category("Patient Address")]
        public string County
        {
            get { return (string)this.GetValue(CountyProperty); }
            set { this.SetValue(CountyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the country in an address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        /// <value>Country in address.</value>
        [Category("Patient Address")]
        public string Country
        {
            get { return (string)this.GetValue(CountryProperty); }
            set { this.SetValue(CountryProperty, value); }
        }

        /// <summary>
        /// Gets or sets the postcode in an address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        /// <value>Postcode in address.</value>
        [Category("Patient Address")]
        public string Postcode
        {
            get { return (string)this.GetValue(PostcodeProperty); }
            set { this.SetValue(PostcodeProperty, value); }           
        }        

        /// <summary>
        /// Gets the visibility of the address line 1.
        /// </summary>
        /// <value>Visibility of the address line 1.</value>
#if !SILVERLIGHT
        [Browsable(false)]
#endif
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Visibility AddressLine1Visibility
        {
            get { return (Visibility)this.GetValue(AddressLine1VisibilityProperty); }            
        }

        /// <summary>
        /// Gets the visibility of the address line 2.
        /// </summary>
        /// <value>Visibility of the address line 2.</value>
#if !SILVERLIGHT
        [Browsable(false)]
#endif
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Visibility AddressLine2Visibility
        {
            get { return (Visibility)this.GetValue(AddressLine2VisibilityProperty); }
        }

        /// <summary>
        /// Gets the visibility of the address line 3.
        /// </summary>
        /// <value>Visibility of the address line 3.</value>
#if !SILVERLIGHT
        [Browsable(false)]
#endif
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Visibility AddressLine3Visibility
        {
            get { return (Visibility)this.GetValue(AddressLine3VisibilityProperty); }
        }

        /// <summary>
        /// Gets the visibility of the town in the address.
        /// </summary>
        /// <value>Visibility of the town in the address.</value>
#if !SILVERLIGHT
        [Browsable(false)]
#endif
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Visibility TownVisibility
        {
            get { return (Visibility)this.GetValue(TownVisibilityProperty); }
        }

        /// <summary>
        /// Gets the visibility of the county in the address.
        /// </summary>
        /// <value>Visibility of the county in the address.</value>
#if !SILVERLIGHT
        [Browsable(false)]
#endif
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Visibility CountyVisibility
        {
            get { return (Visibility)this.GetValue(CountyVisibilityProperty); }
        }

        /// <summary>
        /// Gets the visibility of the country in the address.
        /// </summary>
        /// <value>Visibility of the country in the address.</value>
#if !SILVERLIGHT
        [Browsable(false)]
#endif
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Visibility CountryVisibility
        {
            get { return (Visibility)this.GetValue(CountryVisibilityProperty); }
        }

        /// <summary>
        /// Gets the visibility of the postcode in the address.
        /// </summary>
        /// <value>Visibility of the postcode in the address.</value>
#if !SILVERLIGHT
        [Browsable(false)]
#endif
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Visibility PostcodeVisibility
        {
            get { return (Visibility)this.GetValue(PostcodeVisibilityProperty); }
        }

        /// <summary>
        /// Gets the display value of the postcode in the address.
        /// </summary>
        /// <value>Display value of the postcode in the address.</value>
#if !SILVERLIGHT
        [Browsable(false)]
#endif
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string PostcodeDisplayValue
        {
            get { return (string)this.GetValue(PostcodeDisplayValueProperty); }
        }
       
        /// <summary>
        /// Gets the AddressDisplayText when the address display format is InLine.
        /// </summary>
        /// <value>DisplayText of the address in InLine form.</value>
        /// <remarks>Returns an empty string when the address display format is InForm</remarks>
        public string InlineAddressDisplayText
        {
            get
            {
                if (this.AddressDisplayFormat == AddressDisplayFormat.InForm)
                {
                    return string.Empty;
                }

                return (string)this.GetValue(InlineAddressDisplayTextProperty);
            }
        }
        #endregion

        #region Baseclass Overrides
        /// <summary>
        /// Loads the relevant control template so that its parts can be referenced.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();            

            this.UpdateElementsVisibility();
            this.ShowAddress();
        }
        #endregion        

        #region Automation

        /// <summary>
        /// Automation object for the address label.
        /// </summary>
        /// <returns>Automation object for address label.</returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new AddressLabelAutomationPeer(this);
        }

        #endregion

        #region Property Changed Callbacks
        /// <summary>
        /// Handles the property changed event for address elements.
        /// </summary>
        /// <param name="d">AddressLabel whose address has changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnAddressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                AddressLabel addressLabel = d as AddressLabel;

                if (addressLabel != null)
                {
                    addressLabel.UpdateInlineAddress();
                }

                if (e.Property == PostcodeProperty)
                {
                    addressLabel.canChangePostcodeDisplayValue = true;
                    string postcode = e.NewValue as string;
                    if (string.IsNullOrEmpty(postcode))
                    {
                        addressLabel.SetValue(AddressLabel.PostcodeDisplayValueProperty, string.Empty);
                    }
                    else
                    {
                        addressLabel.SetValue(AddressLabel.PostcodeDisplayValueProperty, postcode.ToUpper(CultureInfo.CurrentCulture));
                    }

                    addressLabel.canChangePostcodeDisplayValue = false;
                }

                addressLabel.UpdateElementsVisibility();
            }
        }

        /// <summary>
        /// Handles the property changed event for address display format.
        /// </summary>
        /// <param name="d">AddressLabel whose address has changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnAddressDisplayFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                AddressLabel addressLabel = d as AddressLabel;

                if (addressLabel != null)
                {
                    addressLabel.ShowAddress();
                }
            }
        }

        /// <summary>
        /// Handles the property changed event for address part visibility.
        /// </summary>
        /// <param name="d">AddressLabel whose address part visibility has changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnAddressPartVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddressLabel addressLabel = d as AddressLabel;
            if (addressLabel != null && !addressLabel.canChangeAddressPartVisibility)
            {
                addressLabel.canChangeAddressPartVisibility = true;
                addressLabel.SetValue(e.Property, e.OldValue);
                addressLabel.canChangeAddressPartVisibility = false;
                throw new InvalidOperationException("Property is readonly");
            }
        }

        /// <summary>
        /// Handles the property changed event for PostcodeDisplayValue.
        /// </summary>
        /// <param name="d">AddressLabel whose postcode has changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnPostcodeDisplayValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddressLabel addressLabel = d as AddressLabel;
            if (addressLabel != null && !addressLabel.canChangePostcodeDisplayValue)
            {
                addressLabel.canChangePostcodeDisplayValue = true;
                addressLabel.SetValue(e.Property, e.OldValue);
                addressLabel.canChangePostcodeDisplayValue = false;
                throw new InvalidOperationException("Property is readonly");
            }
        }

        /// <summary>
        /// Handles the property changed event for InlineAddressDisplayText.
        /// </summary>
        /// <param name="d">AddressLabel whose InlineAddressDisplayText has changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnInlineAddressDisplayTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AddressLabel addressLabel = d as AddressLabel;
            if (addressLabel != null && !addressLabel.canChangeInlineAddressDisplayValue)
            {
                addressLabel.canChangeInlineAddressDisplayValue = true;
                addressLabel.SetValue(e.Property, e.OldValue);
                addressLabel.canChangeInlineAddressDisplayValue = false;
                throw new InvalidOperationException("Property is readonly");
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Updates the InlineAddressDisplayText by taking the newly changed properties.
        /// </summary>
        private void UpdateInlineAddress()
        {
            string[] addressParts = new string[] 
                { 
                    this.Address1, this.Address2, this.Address3, this.Town, 
                    this.County, string.IsNullOrEmpty(this.Postcode) ? string.Empty : this.Postcode.ToUpper(CultureInfo.CurrentCulture), this.Country 
                };
            StringBuilder address = new StringBuilder();
            foreach (string addressPart in addressParts)
            {
                if (!string.IsNullOrEmpty(addressPart))
                {
                    address.Append(addressPart);
                    address.Append(", ");
                }
            }

            this.canChangeInlineAddressDisplayValue = true;
            this.SetValue(InlineAddressDisplayTextProperty, address.ToString().TrimEnd(' ').TrimEnd(','));
            this.canChangeInlineAddressDisplayValue = false;
        }

        /// <summary>
        /// Shows the address either in InLine format or InForm format based on the AddressDisplayFormat property.
        /// </summary>
        private void ShowAddress()
        {
            if (this.AddressDisplayFormat == AddressDisplayFormat.InForm)
            {
                VisualStateManager.GoToState(this, "InForm", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "InLine", true);
            }
        }

        /// <summary>
        /// Updates the elements visibility dependency properties.
        /// </summary>
        /// <remarks>WPF elements will occupy space even if the text is not present, we need to set the visibility 
        /// to collapsed to make sure the elements won't occupy space.</remarks>
        private void UpdateElementsVisibility()
        {
            this.canChangeAddressPartVisibility = true;
            this.SetElementVisibility(AddressLabel.AddressLine1VisibilityProperty, this.Address1);
            this.SetElementVisibility(AddressLabel.AddressLine2VisibilityProperty, this.Address2);
            this.SetElementVisibility(AddressLabel.AddressLine3VisibilityProperty, this.Address3);
            this.SetElementVisibility(AddressLabel.TownVisibilityProperty, this.Town);
            this.SetElementVisibility(AddressLabel.CountryVisibilityProperty, this.Country);
            this.SetElementVisibility(AddressLabel.CountyVisibilityProperty, this.County);
            this.SetElementVisibility(AddressLabel.PostcodeVisibilityProperty, this.Postcode);
            this.canChangeAddressPartVisibility = false;
        }

        /// <summary>
        /// Sets the visibility of an specified property based on the value.
        /// </summary>
        /// <param name="property">Property whose value needs to be updated.</param>
        /// <param name="value">Value based on which visibility needs to be updated.</param>        
        /// <remarks>Visibility is set to Visible if the value is not null or empty string, else will be set to collapsed.</remarks>
        private void SetElementVisibility(DependencyProperty property, string value)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                this.SetValue(property, Visibility.Collapsed);
            }
            else
            {
                this.SetValue(property, Visibility.Visible);
            }
        }
        #endregion
    }
}
