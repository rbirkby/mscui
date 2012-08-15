//-----------------------------------------------------------------------
// <copyright file="PatientBanner.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>15-May-2008</date>
// <summary>The control used to provide a consistent layout for common patient identification information within applications.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.Windows.Media.Imaging;
    using System.ComponentModel;
    using System.Globalization;
    using Microsoft.Cui.Controls.PatientBannerControl;
    using Microsoft.Cui.Controls.Common;
    using Microsoft.Cui.Controls.Common.DateAndTime;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Automation.Peers;

    /// <summary>
    /// The control used to provide a consistent layout for common patient identification information within applications.
    /// </summary> 
    /// <remarks>
    /// Setting the following properties have no visual impact on the Patient banner.
    /// - Background
    /// - FontFamily
    /// - FontSize
    /// - FontStretch
    /// - FontStyle
    /// - FontWeight
    /// - Foreground 
    /// - HorizontalContentAlignment
    /// - VerticalContentAlignment
    /// </remarks>
    public class PatientBanner : Control
    {
        #region Dependency Properties

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.FamilyName"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty FamilyNameProperty = DependencyProperty.Register(
            "FamilyName",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.FamilyName));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.GivenName"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty GivenNameProperty = DependencyProperty.Register(
            "GivenName",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.GivenName));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.Title"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.Title));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.PreferredName"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PreferredNameProperty = DependencyProperty.Register(
            "PreferredName",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnPreferredNameChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.PreferredNameLabelText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PreferredNameLabelTextProperty = DependencyProperty.Register(
            "PreferredNameLabelText",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.PreferredNameLabelText, new PropertyChangedCallback(OnPreferredNameChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.DateOfBirth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DateOfBirthProperty = DependencyProperty.Register(
            "DateOfBirth",
            typeof(DateTime),
            typeof(PatientBanner),
            new PropertyMetadata(DateTime.MinValue, new PropertyChangedCallback(OnDateChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.DateOfBirthLabelText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DateOfBirthLabelTextProperty = DependencyProperty.Register(
            "DateOfBirthLabelText",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.DateOfBirthLabelText));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.DateOfDeath"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DateOfDeathProperty = DependencyProperty.Register(
            "DateOfDeath",
            typeof(DateTime),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnDateChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.DateOfDeathLabelText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DateOfDeathLabelTextProperty = DependencyProperty.Register(
            "DateOfDeathLabelText",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.DateOfDeathLabelText));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.GenderLabelText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty GenderLabelTextProperty = DependencyProperty.Register(
            "GenderLabelText",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.GenderLabelText));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.IdentifierLabelText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IdentifierLabelTextProperty = DependencyProperty.Register(
            "IdentifierLabelText",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.IdentifierLabelText));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.AgeAtDeathLabelText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AgeAtDeathLabelTextProperty = DependencyProperty.Register(
            "AgeAtDeathLabelText",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.AgeAtDeathLabelText));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.PatientImage"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PatientImageProperty = DependencyProperty.Register(
            "PatientImage",
            typeof(ImageSource),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnPatientImageChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.PatientNameToolTip"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PatientNameToolTipProperty = DependencyProperty.Register(
            "PatientNameToolTip",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.PatientNameToolTip));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.PreferredNameLabelToolTip"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PreferredNameLabelToolTipProperty = DependencyProperty.Register(
            "PreferredNameLabelToolTip",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.PreferredNameLabelToolTip));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.PreferredNameToolTip"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PreferredNameToolTipProperty = DependencyProperty.Register(
            "PreferredNameToolTip",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.PreferredNameToolTip));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.DateOfBirthToolTip"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DateOfBirthToolTipProperty = DependencyProperty.Register(
            "DateOfBirthToolTip",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.DateOfBirthTooltip));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.DateOfBirthLabelToolTip"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DateOfBirthLabelToolTipProperty = DependencyProperty.Register(
            "DateOfBirthLabelToolTip",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.DateOfBirthLabelTooltip));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.DateOfDeathToolTip"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DateOfDeathToolTipProperty = DependencyProperty.Register(
            "DateOfDeathToolTip",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.DateOfDeathToolTip));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.DateOfDeathLabelToolTip"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DateOfDeathLabelToolTipProperty = DependencyProperty.Register(
            "DateOfDeathLabelToolTip",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.DateOfDeathLabelToolTip));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.GenderValueTooltip"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty GenderValueTooltipProperty = DependencyProperty.Register(
            "GenderValueTooltip",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.GenderTooltip));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.GenderLabelTooltip"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty GenderLabelTooltipProperty = DependencyProperty.Register(
            "GenderLabelTooltip",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.GenderLabelTooltip));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.IdentifierTooltip"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IdentifierTooltipProperty = DependencyProperty.Register(
            "IdentifierTooltip",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.IdentifierTooltip));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.IdentifierLabelTooltip"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IdentifierLabelTooltipProperty = DependencyProperty.Register(
            "IdentifierLabelTooltip",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.IdentifierLabelTooltip));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.AgeAtDeathTooltip"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AgeAtDeathTooltipProperty = DependencyProperty.Register(
            "AgeAtDeathTooltip",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.AgeAtDeathTooltip));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.AgeAtDeathLabelTooltip"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AgeAtDeathLabelTooltipProperty = DependencyProperty.Register(
            "AgeAtDeathLabelTooltip",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.AgeAtDeathLabelTooltip));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.ZoneTwoTooltip"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoneTwoTooltipProperty = DependencyProperty.Register(
            "ZoneTwoTooltip",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.ZoneTwoTooltip));    

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.SubSectionOneTitle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SubSectionOneTitleProperty = DependencyProperty.Register(
            "SubSectionOneTitle",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.SubSectionOneTitle));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.SubSectionTwoTitle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SubSectionTwoTitleProperty = DependencyProperty.Register(
            "SubSectionTwoTitle",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.SubSectionTwoTitle));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.SubSectionThreeTitle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SubSectionThreeTitleProperty = DependencyProperty.Register(
            "SubSectionThreeTitle",
            typeof(string),
            typeof(PatientBanner),
            null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.SubSectionFourTitle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SubSectionFourTitleProperty = DependencyProperty.Register(
            "SubSectionFourTitle",
            typeof(string),
            typeof(PatientBanner),
            null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.DropDownImage"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DropDownImageProperty = DependencyProperty.Register(
            "DropDownImage",
            typeof(ImageSource),
            typeof(PatientBanner),
            new PropertyMetadata(ResourceHelper.DropDownImage, new PropertyChangedCallback(OnExpandCollapseImageChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.CollapseImage"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CollapseImageProperty = DependencyProperty.Register(
            "CollapseImage",
            typeof(ImageSource),
            typeof(PatientBanner),
            new PropertyMetadata(ResourceHelper.CollapseImage, new PropertyChangedCallback(OnExpandCollapseImageChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.ZoneTwoExpanded"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoneTwoExpandedProperty = DependencyProperty.Register(
            "ZoneTwoExpanded",
            typeof(bool),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnZoneTwoExpandStateChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.SubSectionThreeContent"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SubSectionThreeContentProperty = DependencyProperty.Register(
            "SubSectionThreeContent",
            typeof(object),
            typeof(PatientBanner),
            null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.SubSectionFourContent"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SubSectionFourContentProperty = DependencyProperty.Register(
            "SubSectionFourContent",
            typeof(object),
            typeof(PatientBanner),
            null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.AllergyInformation"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AllergiesProperty = DependencyProperty.Register(
            "Allergies",
            typeof(AllergyCollection),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnAllergyInformationChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.AllergyInformation"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AllergyInformationProperty = DependencyProperty.Register(
            "AllergyInformation",
            typeof(AllergyInformation),
            typeof(PatientBanner),
            new PropertyMetadata(AllergyInformation.Unavailable, new PropertyChangedCallback(OnAllergyInformationChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.AllergiesNotPresentIcon"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AllergiesNotPresentIconProperty = DependencyProperty.Register(
            "AllergiesNotPresentIcon",
            typeof(ImageSource),
            typeof(PatientBanner),
            new PropertyMetadata(ResourceHelper.AllergiesNotPresentIcon, new PropertyChangedCallback(OnAllergyInformationChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.AllergiesPresentIcon"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AllergiesPresentIconProperty = DependencyProperty.Register(
            "AllergiesPresentIcon",
            typeof(ImageSource),
            typeof(PatientBanner),
            new PropertyMetadata(ResourceHelper.AllergiesPresentIcon, new PropertyChangedCallback(OnAllergyInformationChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.AllergiesUnavailableIcon"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AllergiesUnavailableIconProperty = DependencyProperty.Register(
            "AllergiesUnavailableIcon",
            typeof(ImageSource),
            typeof(PatientBanner),
            new PropertyMetadata(ResourceHelper.AllergiesUnavailableIcon, new PropertyChangedCallback(OnAllergyInformationChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.ViewAllAddressLinkText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ViewAllAddressLinkTextProperty = DependencyProperty.Register(
            "ViewAllAddressLinkText",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.ViewAllAddressesLinkText));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.Gender"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty GenderProperty = DependencyProperty.Register(
            "Gender",
            typeof(PatientGender),
            typeof(PatientBanner),
            new PropertyMetadata(PatientGender.NotKnown));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.ViewAllContactDetailsLinkText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ViewAllContactDetailsLinkTextProperty = DependencyProperty.Register(
            "ViewAllContactDetailsLinkText",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.ViewAllContactDetailsLinkText));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.AddressDisplayFormat"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AddressDisplayFormatProperty = DependencyProperty.Register(
                                                                                    "AddressDisplayFormat",
                                                                                    typeof(AddressDisplayFormat),
                                                                                    typeof(PatientBanner),
                                                                                    null);

        /// <summary>
        /// Identifies the AddressTypeLabelTextProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty AddressTypeLabelTextProperty = DependencyProperty.Register(
                                                                                    "AddressTypeLabelText",
                                                                                    typeof(string),
                                                                                    typeof(PatientBanner),
                                                                                    new PropertyMetadata(PatientBannerResources.AddressTypeLabelText));

        /// <summary>
        /// Identifies the Address1Property dependency property.
        /// </summary>
        public static readonly DependencyProperty Address1Property = DependencyProperty.Register(
                                                                                    "Address1",
                                                                                    typeof(string),
                                                                                    typeof(PatientBanner),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressChanged)));

        /// <summary>
        /// Identifies the Address2Property dependency property.
        /// </summary>
        public static readonly DependencyProperty Address2Property = DependencyProperty.Register(
                                                                                    "Address2",
                                                                                    typeof(string),
                                                                                    typeof(PatientBanner),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressChanged)));

        /// <summary>
        /// Identifies the Address3Property dependency property.
        /// </summary>
        public static readonly DependencyProperty Address3Property = DependencyProperty.Register(
                                                                                    "Address3",
                                                                                    typeof(string),
                                                                                    typeof(PatientBanner),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressChanged)));

        /// <summary>
        /// Identifies the TownProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty TownProperty = DependencyProperty.Register(
                                                                                    "Town",
                                                                                    typeof(string),
                                                                                    typeof(PatientBanner),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressChanged)));

        /// <summary>
        /// Identifies the CountyProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty CountyProperty = DependencyProperty.Register(
                                                                                    "County",
                                                                                    typeof(string),
                                                                                    typeof(PatientBanner),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressChanged)));

        /// <summary>
        /// Identifies the CountryProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty CountryProperty = DependencyProperty.Register(
                                                                                    "Country",
                                                                                    typeof(string),
                                                                                    typeof(PatientBanner),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressChanged)));

        /// <summary>
        /// Identifies the PostcodeProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty PostcodeProperty = DependencyProperty.Register(
                                                                                    "Postcode",
                                                                                    typeof(string),
                                                                                    typeof(PatientBanner),
                                                                                    new PropertyMetadata(new PropertyChangedCallback(OnAddressChanged)));

        /// <summary>
        /// Identifies the ViewAllergyRecordLinkTextProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty ViewAllergyRecordLinkTextProperty = DependencyProperty.Register(
            "ViewAllergyRecordLinkText",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.ViewAllergyRecordLinkText));

        /// <summary>
        /// Identifies the IdentifierProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty IdentifierProperty = DependencyProperty.Register(
            "Identifier",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.DefaultIdentifierValue));

        /// <summary>
        /// Identifies the IdentifierTypeProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty IdentifierTypeProperty = DependencyProperty.Register(
            "IdentifierType",
            typeof(IdentifierType),
            typeof(PatientBanner),
            new PropertyMetadata(IdentifierType.Other));

        /// <summary>
        /// Identifies the HomePhoneLabelTextProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty HomePhoneLabelTextProperty = DependencyProperty.Register(
            "HomePhoneLabelText",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.HomePhoneLabelText));

        /// <summary>
        /// Identifies the HomePhoneNumberProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty HomePhoneNumberProperty = DependencyProperty.Register(
            "HomePhoneNumber",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnContactDetailsChanged)));

        /// <summary>
        /// Identifies the MobilePhoneLabelTextProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty MobilePhoneLabelTextProperty = DependencyProperty.Register(
            "MobilePhoneLabelText",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.MobilePhoneLabelText));

        /// <summary>
        /// Identifies the MobilePhoneNumberProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty MobilePhoneNumberProperty = DependencyProperty.Register(
            "MobilePhoneNumber",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnContactDetailsChanged)));

        /// <summary>
        /// Identifies the WorkPhoneLabelTextProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty WorkPhoneLabelTextProperty = DependencyProperty.Register(
            "WorkPhoneLabelText",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.WorkPhoneLabelText));

        /// <summary>
        /// Identifies the WorkPhoneNumberProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty WorkPhoneNumberProperty = DependencyProperty.Register(
            "WorkPhoneNumber",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnContactDetailsChanged)));

        /// <summary>
        /// Identifies the EmailLabelTextProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty EmailLabelTextProperty = DependencyProperty.Register(
            "EmailLabelText",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(PatientBannerResources.EmailLabelText));

        /// <summary>
        /// Identifies the EmailAddressProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty EmailAddressProperty = DependencyProperty.Register(
            "EmailAddress",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnContactDetailsChanged)));          

        /// <summary>
        /// Identifies the BorderWidthProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty BorderWidthProperty = DependencyProperty.Register(
            "BorderWidth",
            typeof(double),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnBorderWidthChanged)));

        /// <summary>
        /// Identifies the ZoneOneMinHeightProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoneOneMinHeightProperty = DependencyProperty.Register(
            "ZoneOneMinHeight",
            typeof(double),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnZoneOneMinHeightChanged)));
               
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.AgeAtDeath"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AgeAtDeathProperty = DependencyProperty.Register(
            "AgeAtDeath",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnPropertyValueChanged)));

        /// <summary>
        /// Identifies the DateOfBirthTextProperty dependency property.
        /// </summary>
        public static readonly DependencyProperty DateOfBirthTextProperty = DependencyProperty.Register(
            "DateOfBirthText",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnPropertyValueChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.DateOfDeathText"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty DateOfDeathTextProperty = DependencyProperty.Register(
            "DateOfDeathText",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnPropertyValueChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.AddressPreview"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AddressPreviewProperty = DependencyProperty.Register(
            "AddressPreview",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnPropertyValueChanged)));

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.ContactPreview"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ContactPreviewProperty = DependencyProperty.Register(
            "ContactPreview",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnPropertyValueChanged)));
             
        /// <summary>
        /// Dependency property for allergy status.
        /// </summary>
        public static readonly DependencyProperty AllergyStatusProperty = DependencyProperty.Register(
            "AllergyStatus",
            typeof(string),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnPropertyValueChanged)));

        /// <summary>
        /// Property for patient image visibility.
        /// </summary>
        public static readonly DependencyProperty PatientImageVisibleProperty = DependencyProperty.Register(
            "PatientImageVisible",
            typeof(Visibility),
            typeof(PatientBanner),
            new PropertyMetadata(new PropertyChangedCallback(OnPatientImageChanged)));

        #region Style Dependency Properties
        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.PatientNameStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty PatientNameStyleProperty = DependencyProperty.Register(
            "PatientNameStyle", typeof(Style), typeof(PatientBanner), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.ZoneOneLabelStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoneOneLabelStyleProperty = DependencyProperty.Register(
            "ZoneOneLabelStyle", typeof(Style), typeof(PatientBanner), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.ZoneOneDataStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoneOneDataStyleProperty = DependencyProperty.Register(
            "ZoneOneDataStyle", typeof(Style), typeof(PatientBanner), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.ZoneTwoTitleLabelStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoneTwoTitleLabelStyleProperty = DependencyProperty.Register(
            "ZoneTwoTitleLabelStyle", typeof(Style), typeof(PatientBanner), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.ZoneTwoTitleDataStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoneTwoTitleDataStyleProperty = DependencyProperty.Register(
            "ZoneTwoTitleDataStyle", typeof(Style), typeof(PatientBanner), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.ZoneTwoLabelStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoneTwoLabelStyleProperty = DependencyProperty.Register(
            "ZoneTwoLabelStyle", typeof(Style), typeof(PatientBanner), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.ZoneTwoDataStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoneTwoDataStyleProperty = DependencyProperty.Register(
            "ZoneTwoDataStyle", typeof(Style), typeof(PatientBanner), null);

        /// <summary>
        /// Identifies the <see cref="Microsoft.Cui.Controls.PatientBanner.ZoneTwoLinksStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoneTwoLinksStyleProperty = DependencyProperty.Register(
            "ZoneTwoLinksStyle", typeof(Style), typeof(PatientBanner), null);
        #endregion
        #endregion

        #region Constants
        /// <summary>
        /// Padding used for margins.
        /// </summary>
        private const int MarginPadding = 5;

        /// <summary>
        /// Ellipsis characters.
        /// </summary>
        private const string Ellipsis = "...";

        /// <summary>
        /// Padding used in Zone one.
        /// </summary>
        private const double ZoneOnePadding = 7;

        /// <summary>
        /// Max width of a tool tip.
        /// </summary>
        /// <remarks>
        /// SL runtime is crashing when the tooltip width exceeds the width of the control.
        /// </remarks>
        private const double MaxToolTipWidth = 500;

        #endregion        

        #region Template Part Names

        #region Zone One Elements

        /// <summary>
        /// Template part name for zone one root element.
        /// </summary>
        private const string ZoneOneRootElementName = "ZoneOne";

        /// <summary>
        /// Template part name for patient name panel.
        /// </summary>
        private const string PatientNamePanel = "PatientNamePanel";

        /// <summary>
        /// Template part name for patient details panel.
        /// </summary>
        private const string PatientDetailsPanel = "PatientDetailsPanel";

        /// <summary>
        /// Template part name for preferred name details panel.
        /// </summary>
        private const string PreferredNameDetailsPanel = "PreferredNameDetails";

        /// <summary>
        /// Template part element name for patient image.
        /// </summary>
        private const string PatientImageName = "PatientImage";

        /// <summary>
        /// Template part element name for name label.
        /// </summary>
        private const string NameLabel = "NameLabel";

        /// <summary>
        /// Template part element name for preferred name label.
        /// </summary>
        private const string PreferredNameLabel = "PreferredNameLabelText";

        /// <summary>
        /// Template part element name for preferred name.
        /// </summary>
        private const string PreferredNameValue = "PreferredName";

        /// <summary>
        /// Template part element name for date of birth label.
        /// </summary>
        private const string DateOfBirthLabel = "DOBLabel";

        /// <summary>
        /// Template part element name for date of birth.
        /// </summary>
        private const string DateOfBirthValue = "DOBValue";

        /// <summary>
        /// Template part element name for gender label.
        /// </summary>
        private const string GenderLabel = "GenderLabel";

        /// <summary>
        /// Template part element name for gender value.
        /// </summary>
        private const string GenderValue = "GenderValue";

        /// <summary>
        /// Template part element name for identifier label.
        /// </summary>
        private const string IdentifierLabel = "IdentifierLabel";

        /// <summary>
        /// Template part element name for identifier value.
        /// </summary>
        private const string IdentifierValue = "IdentifierValue";

        /// <summary>
        /// Template part element name for date of death label.
        /// </summary>
        private const string DateOfDeathLabel = "DODLabel";

        /// <summary>
        /// Template part element name for date of death value.
        /// </summary>
        private const string DateOfDeathValue = "DODValue";

        /// <summary>
        /// Template part element name for age at death label.
        /// </summary>
        private const string AgeAtDeathLabel = "AgeAtDeathLabel";

        /// <summary>
        /// Template part element name for age at death value.
        /// </summary>
        private const string AgeAtDeathValue = "AgeAtDeathValue";
        #endregion

        #region Zone Two Title Elements
        /// <summary>
        /// Template part name for subsection one.
        /// </summary>
        private const string SubsectionOneElementName = "SubsectionOne";
        
        /// <summary>
        /// Template part name for subsection two.
        /// </summary>
        private const string SubsectionTwoElementName = "SubsectionTwo";

        /// <summary>
        /// Template part name for subsection three.
        /// </summary>
        private const string SubsectionThreeElementName = "SubsectionThree";

        /// <summary>
        /// Template part name for subsection four.
        /// </summary>
        private const string SubsectionFourElementName = "SubsectionFour";

        /// <summary>
        /// Template part name for subsection five.
        /// </summary>
        private const string SubsectionFiveElementName = "SubsectionFive";

        /// <summary>
        /// Template part name for subsection one title label.
        /// </summary>
        private const string SubsectionOneTitleLabelName = "SubsectionOneTitleLabel";

        /// <summary>
        /// Template part name for address preview label.
        /// </summary>
        private const string AddressPreviewLabelName = "AddressPreview";

        /// <summary>
        /// Template part name for subsection two title label.
        /// </summary>
        private const string SubsectionTwoTitleLabelName = "SubsectionTwoTitleLabel";

        /// <summary>
        /// Template part name for contact preview label.
        /// </summary>
        private const string ContactPreviewLabelName = "ContactPreview";

        /// <summary>
        /// Template part name for subsection three title label.
        /// </summary>
        private const string SubsectionThreeTitleLabelName = "SubsectionThreeTitleLabel";

        /// <summary>
        /// Template part name for subsection four title label.
        /// </summary>
        private const string SubsectionFourTitleLabelName = "SubsectionFourTitleLabel";

        /// <summary>
        /// Template part name for subsection five title label.
        /// </summary>
        private const string SubsectionFiveTitleLabelName = "SubsectionFiveTitleLabel";

        /// <summary>
        /// Template part name for allergies image element.
        /// </summary>
        private const string AllergiesImageElementName = "AllergiesImage";
        #endregion

        #region Zone Two Elements
        /// <summary>
        /// Template part name for subsection one content panel.
        /// </summary>
        private const string SubsectionOneContent = "SubsectionOneContent";

        /// <summary>
        /// Template part name for subsection two content panel.
        /// </summary>
        private const string SubsectionTwoContent = "SubsectionTwoContent";

        /// <summary>
        /// Template part name for allergies label control.
        /// </summary>
        private const string AllergiesLabelControlName = "AllergiesLabel";
        #endregion

        #region Zone Two Links

        /// <summary>
        /// Template part element name for view allergy record link.
        /// </summary>
        private const string ViewAllergyRecordLinkName = "ViewAllergyRecordLink";

        /// <summary>
        /// Template part element name for view all adresses link.
        /// </summary>
        private const string ViewAllAddressesLinkName = "ViewAllAddressLink";

        /// <summary>
        /// Template part element name for view contact details link.
        /// </summary>
        private const string ViewContactDetailsLinkName = "ViewContactDetailsLink";
        #endregion

        /// <summary>
        /// Template part name for root grid.
        /// </summary>
        private const string RootGridName = "PART_RootElement";

        /// <summary>
        /// Template part name for expand button.
        /// </summary>
        private const string ExpandButtonName = "ButtonExpand";

        /// <summary>
        /// Template part name for border.
        /// </summary>
        private const string BorderElementName = "GridBorder";

        /// <summary>
        /// Template part name for Zone two header border.
        /// </summary>
        private const string ZoneTwoHeaderBorderElementName = "ZoneTwoHeaderBorder";
        #endregion

        #region Error Messages
        /// <summary>
        /// Error message string used when the template part element is missing.
        /// </summary>
        private const string TemplatePartElementNullMessage = @"Could not find an element with name '{0}' in the template.";

        /// <summary>
        /// Error message string used when the template part element is of incorrect type.
        /// </summary>
        private const string TemplatePartElementTypeInvalidMessage = @"Element with name '{0}' in the template is of invalid type. Expected type is '{1}'.";
        #endregion

        #region Control State Names
        /// <summary>
        /// State name for deceased banner.
        /// </summary>
        private const string DeceasedState = "Deceased";

        /// <summary>
        /// State name for alive state.
        /// </summary>
        private const string AliveState = "Alive";

        /// <summary>
        /// Control state name for Zone two normal.
        /// </summary>
        private const string ZoneTwoNormal = "ZoneTwoNormal";

        /// <summary>
        /// Control state name for zone two hover.
        /// </summary>
        private const string ZoneTwoHover = "ZoneTwoHover";

        /// <summary>
        /// Control state name when allergy details are known.
        /// </summary>
        /// <remarks>Will be used if AllergyInformation is Present or NoneKnown.</remarks>
        private const string AllergyDetailsKnown = "AllergyDetailsKnown";

        /// <summary>
        /// Control state name when allergy details are not known.
        /// </summary>
        /// <remarks>Will be used if AllergyInformation is Unavailable or NotRecorded.</remarks>
        private const string AllergyDetailsNotKnown = "AllergyDetailsNotKnown";

        /// <summary>
        /// Control state name when patient image is Visisble.
        /// </summary>
        private const string PatientImageVisibleState = "PatientImageVisible";

        /// <summary>
        /// Control state name when patient image is Collapsed.
        /// </summary>
        private const string PatientImageCollapsedState = "PatientImageCollapsed";
        #endregion

        #region Template Elements

        /// <summary>
        /// Root grid.
        /// </summary>
        private Grid rootGrid;

        /// <summary>
        /// Expand button.
        /// </summary>
        private Button btnExpand;

        /// <summary>
        /// Patient Image.
        /// </summary>
        private Image imgPatient;

        /// <summary>
        /// Member variable to hold allergies image.
        /// </summary>
        private Image allergiesImage;

        /// <summary>
        /// Gender label.
        /// </summary>
        private Label genderLabel;

        /// <summary>
        /// Date of birth label.
        /// </summary>
        private Label dobLabel;

        /// <summary>
        /// Age at death label.
        /// </summary>
        private Label ageAtDeathLabel;

        /// <summary>
        /// Date of death label.
        /// </summary>
        private Label dodLabel;

        /// <summary>
        /// Identifier label.
        /// </summary>
        private Label identifierLabel;

        /// <summary>
        /// Expand collapse image.
        /// </summary>
        private Image imgExpandCollapse = new Image();

        /// <summary>
        /// Row heights collection.
        /// </summary>
        private GridLength zoneTwoHeight = new GridLength();

        /// <summary>
        /// Name label.
        /// </summary>
        private NameLabel nameLabel;

        /// <summary>
        /// Gender value.
        /// </summary>
        private GenderLabel genderValue;

        /// <summary>
        /// Identifier value.
        /// </summary>
        private IdentifierLabel identifierValue;

        /// <summary>
        /// Date of birth value.
        /// </summary>
        private Label dobValue;

        /// <summary>
        /// Date of death value.
        /// </summary>
        private Label dodValue;

        /// <summary>
        /// Age at death value.
        /// </summary>
        private Label ageAtDeath;
                
        /// <summary>
        /// View all addresses link.
        /// </summary>
        private Label viewAllAddressLink;

        /// <summary>
        /// View contact details link.
        /// </summary>
        private Label viewContactDetailsLink;

        /// <summary>
        /// View allergy record link.
        /// </summary>
        private Label viewAllergyRecordLink;       
                
        /// <summary>
        /// Member variable to hold preferred name label.
        /// </summary>
        private Label preferredNameLabel;

        /// <summary>
        /// Member variable to hold preferred name.
        /// </summary>
        private Label preferredName;        

        /// <summary>
        /// Member variable to hold Zone one root element.
        /// </summary>
        private FrameworkElement zoneOne;

        /// <summary>
        /// Member variable to hold patient name panel.
        /// </summary>
        private FrameworkElement patientNamePanel;

        /// <summary>
        /// Member variable to hold patient details panel.
        /// </summary>
        private FrameworkElement patientDetailsPanel;

        /// <summary>
        /// Member variable to hold subsection one title label.
        /// </summary>
        private Label subsectionOneTitleLabel;

        /// <summary>
        /// Member variable to hold subsection two title label.
        /// </summary>
        private Label subsectionTwoTitleLabel;

        /// <summary>
        /// Member variable to hold subsection three title label.
        /// </summary>
        private Label subsectionThreeTitleLabel;

        /// <summary>
        /// Member variable to hold subsection four title label.
        /// </summary>
        private Label subsectionFourTitleLabel;

        /// <summary>
        /// Member variable to hold subsection five title label.
        /// </summary>
        private Label subsectionFiveTitleLabel;

        /// <summary>
        /// Member variable to hold address preview. 
        /// </summary>
        private Label addressPreview;

        /// <summary>
        /// Member variable to hold contact preview.
        /// </summary>
        private Label contactPreview;

        /// <summary>
        /// Member variable to hold subsection one panel.
        /// </summary>
        private FrameworkElement subsectionOnePanel;

        /// <summary>
        /// Member variable to hold subsection two panel.
        /// </summary>
        private FrameworkElement subsectionTwoPanel;

        /// <summary>
        /// Member variable to hold subsection three panel.
        /// </summary>
        private FrameworkElement subsectionThreePanel;

        /// <summary>
        /// Member variable to hold subsection four panel.
        /// </summary>
        private FrameworkElement subsectionFourPanel;

        /// <summary>
        /// Member variable to hold subsection five panel.
        /// </summary>
        private FrameworkElement subsectionFivePanel;

        /// <summary>
        /// Member variable to hold subsection one content panel.
        /// </summary>
        private Panel subsectionOneContentPanel;

        /// <summary>
        /// Member variable to hold subsection two content panel.
        /// </summary>
        private Panel subsectionTwoContentPanel;

        /// <summary>
        /// Member variable to hold preferred name details.
        /// </summary>
        private FrameworkElement preferredNameDetailsPanel;

        /// <summary>
        /// Member variable to hold allergies label control.
        /// </summary>
        private AllergiesLabel allergiesLabelControl;

        /// <summary>
        /// Member variable to hold border for Zone two header elements.
        /// </summary>
        private Border zoneTwoHeaderBorder;
        #endregion                

        #region Private Vars
        /// <summary>
        /// Boolean indicating whether the patient is deceased.
        /// </summary>
        private bool deceased;

        /// <summary>
        /// Member variable to hold patient allergies.
        /// </summary>
        private AllergyCollection patientAllergies = new AllergyCollection();

        /// <summary>
        /// Member variable to indicate whether the readonly dependency properties can be changed.
        /// </summary>
        private bool ignorePropertyChange = true;

        /// <summary>
        /// Member variable to hold subsection one width.
        /// </summary>
        private GridLength subsectionOneWidth = new GridLength(2, GridUnitType.Star);

        /// <summary>
        /// Member variable to hold subsection two width.
        /// </summary>
        private GridLength subsectionTwoWidth = new GridLength(2, GridUnitType.Star);

        /// <summary>
        /// Member variable to hold subsection three width.
        /// </summary>
        private GridLength subsectionThreeWidth = new GridLength(1, GridUnitType.Star);

        /// <summary>
        /// Member variable to hold subsection four width.
        /// </summary>
        private GridLength subsectionFourWidth = new GridLength(1, GridUnitType.Star);

        /// <summary>
        /// Member variable to hold subsection five width.
        /// </summary>
        private GridLength subsectionFiveWidth = new GridLength(2, GridUnitType.Star);
        
        /// <summary>
        /// Member variable to indicate whether patient image is currently being displayed.
        /// </summary>
        private bool patientImageDisplayed;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of patient banner.
        /// </summary>
        public PatientBanner()
        {
            this.DefaultStyleKey = typeof(PatientBanner);           
            this.SetValue(AllergiesProperty, new AllergyCollection());            

            this.Allergies.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(this.Allergies_CollectionChanged);            
            this.SizeChanged += new SizeChangedEventHandler(this.PatientBanner_SizeChanged);
        }        
        #endregion

        #region Events
        /// <summary>
        /// Occurs when View allergy record link is clicked.
        /// </summary>
        public event RoutedEventHandler ViewAllergyRecordClick;

        /// <summary>
        /// Occurs when View all addresses link is clicked.
        /// </summary>
        public event RoutedEventHandler ViewAllAddressesClick;

        /// <summary>
        /// Occurs when View contact details link is clicked.
        /// </summary>
        public event RoutedEventHandler ViewContactDetailsClick;

        /// <summary>
        /// Occurs when Date of birth label is clicked.
        /// </summary>
        public event RoutedEventHandler DateOfBirthLabelClick;

        /// <summary>
        /// Occurs when Date of birth value is clicked.
        /// </summary>
        public event RoutedEventHandler DateOfBirthClick;

        /// <summary>
        /// Occurs when Date of death label is clicked.
        /// </summary>
        public event RoutedEventHandler DateOfDeathLabelClick;

        /// <summary>
        /// Occurs when Date of death value is clicked.
        /// </summary>
        public event RoutedEventHandler DateOfDeathClick;

        /// <summary>
        /// Occurs when Identifier label is clicked.
        /// </summary>
        public event RoutedEventHandler IdentifierLabelClick;

        /// <summary>
        /// Occurs when Identifier value is clicked.
        /// </summary>
        public event RoutedEventHandler IdentifierClick;

        /// <summary>
        /// Occurs when Gender label is clicked.
        /// </summary>
        public event RoutedEventHandler GenderLabelClick;

        /// <summary>
        /// Occurs when Gender value is clicked.
        /// </summary>
        public event RoutedEventHandler GenderClick;

        /// <summary>
        /// Occurs when Age at death label is clicked.
        /// </summary>
        public event RoutedEventHandler AgeAtDeathLabelClick;

        /// <summary>
        /// Occurs when Age at death value is clicked.
        /// </summary>
        public event RoutedEventHandler AgeAtDeathClick;

        /// <summary>
        /// Occurs when Preferred name is clicked.
        /// </summary>
        public event RoutedEventHandler PreferredNameClick;

        /// <summary>
        /// Occurs when Preferred name label is clicked.
        /// </summary>
        public event RoutedEventHandler PreferredNameLabelClick;

        /// <summary>
        /// Occurs when Name is clicked.
        /// </summary>
        public event RoutedEventHandler NameClick;

        /// <summary>
        /// Occurs when Zone two is expanded or collapsed.
        /// </summary>
        public event RoutedEventHandler ZoneTwoStateChanged;

        /// <summary>
        /// Occurs when patient image load fails.
        /// </summary>
        public event EventHandler<ExceptionRoutedEventArgs> PatientImageFailed;

        /// <summary>
        /// Occurs when drop down image load fails.
        /// </summary>
        public event EventHandler<ExceptionRoutedEventArgs> DropDownImageFailed;

        /// <summary>
        /// Occurs when collapse image load fails.
        /// </summary>
        public event EventHandler<ExceptionRoutedEventArgs> CollapseImageFailed;         
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the value of the complete patient name as displayed.
        /// </summary>
        /// <value>
        /// Name display value.
        /// </value>
        [Category("Appearance")]
        public string NameDisplayValue
        {
            get
            {
                return this.nameLabel.DisplayValue;
            }
        }        

        /// <summary>
        /// Gets or sets the family name of patient.
        /// </summary>
        /// <value>Family name.</value>
        [Category("Patient Details")]
        public string FamilyName
        {
            get { return (string)this.GetValue(FamilyNameProperty); }
            set { this.SetValue(FamilyNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the given name of patient.
        /// </summary>
        /// <value>Given name.</value>
        [Category("Patient Details")]
        public string GivenName
        {
            get { return (string)this.GetValue(GivenNameProperty); }
            set { this.SetValue(GivenNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the title of patient.
        /// </summary>
        /// <value>Patient title.</value>
        [Category("Patient Details")]
        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }            

        /// <summary>
        /// Gets or sets the preferred name of patient.
        /// </summary>
        /// <value>Preferred name.</value>
        [Category("Patient Details")]
        public string PreferredName
        {
            get { return (string)this.GetValue(PreferredNameProperty); }
            set { this.SetValue(PreferredNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text for preferred name label.
        /// </summary>
        /// <value>Preferred name label text..</value>
        [Category("Patient Details")]
        public string PreferredNameLabelText
        {
            get { return (string)this.GetValue(PreferredNameLabelTextProperty); }
            set { this.SetValue(PreferredNameLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the date of birth of patient.
        /// </summary>  
        /// <value>Date of birth.</value>
        [Category("Patient Details")]
        [TypeConverter(typeof(DateConverter))]
        public DateTime DateOfBirth
        {
            get { return (DateTime)this.GetValue(DateOfBirthProperty); }
            set { this.SetValue(DateOfBirthProperty, value); }
        }      

        /// <summary>
        /// Gets or sets the text for date of birth label.
        /// </summary>
        /// <value>Date of birth label text.</value>
        [Category("Patient Details")]
        public string DateOfBirthLabelText
        {
            get { return (string)this.GetValue(DateOfBirthLabelTextProperty); }
            set { this.SetValue(DateOfBirthLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the date of patient's death.
        /// </summary>
        /// <value>Date of death.</value>
        [Category("Patient Details")]
        [TypeConverter(typeof(DateConverter))]
        public DateTime DateOfDeath
        {
            get { return (DateTime)this.GetValue(DateOfDeathProperty); }
            set { this.SetValue(DateOfDeathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text for date of death label.
        /// </summary>
        /// <value>Date of death label text.</value>
        [Category("Patient Details")]
        public string DateOfDeathLabelText
        {
            get { return (string)this.GetValue(DateOfDeathLabelTextProperty); }
            set { this.SetValue(DateOfDeathLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the patient's gender.
        /// </summary>
        /// <value>Patient gender.</value>
        [Category("Patient Details")]
        public PatientGender Gender
        {
            get { return (PatientGender)this.GetValue(GenderProperty); }
            set { this.SetValue(GenderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text for gender label.
        /// </summary>
        /// <value>Gender label text.</value>
        [Category("Patient Details")]
        public string GenderLabelText
        {
            get { return (string)this.GetValue(GenderLabelTextProperty); }
            set { this.SetValue(GenderLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>Patient identifier.</value>
        [Category("Patient Details")]
        public string Identifier
        {
            get
            {
                return (string)this.GetValue(IdentifierProperty);
            }

            set
            {
                this.SetValue(IdentifierProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the text for identifier label.
        /// </summary>
        /// <value>Identifier label text.</value>
        [Category("Patient Details")]
        public string IdentifierLabelText
        {
            get { return (string)this.GetValue(IdentifierLabelTextProperty); }
            set { this.SetValue(IdentifierLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the identifier type.
        /// </summary>
        /// <value>Identifier type.</value>
        [Category("Patient Details")]
        public IdentifierType IdentifierType
        {
            get
            {
                return (IdentifierType)this.GetValue(IdentifierTypeProperty);
            }

            set
            {
                this.SetValue(IdentifierTypeProperty, value);
            }
        }       

        /// <summary>
        /// Gets or sets the text for age at death label.
        /// </summary>
        /// <value>Age at death label text.</value>
        [Category("Patient Details")]
        public string AgeAtDeathLabelText
        {
            get { return (string)this.GetValue(AgeAtDeathLabelTextProperty); }
            set { this.SetValue(AgeAtDeathLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the patient image.
        /// </summary>
        /// <value>Patient Image.</value>
        [Category("Appearance")]
        public ImageSource PatientImage
        {
            get { return (ImageSource)this.GetValue(PatientImageProperty); }
            set { this.SetValue(PatientImageProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ToolTip for patient name.
        /// </summary>
        /// <value>ToolTip message to be shown for patient name.</value>
        [Category("Behavior")]
        public string PatientNameToolTip
        {
            get { return (string)this.GetValue(PatientNameToolTipProperty); }
            set { this.SetValue(PatientNameToolTipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ToolTip for preferred name.
        /// </summary>
        /// <value>ToolTip message to be shown for preferred name.</value>
        [Category("Behavior")]
        public string PreferredNameToolTip
        {
            get { return (string)this.GetValue(PreferredNameToolTipProperty); }
            set { this.SetValue(PreferredNameToolTipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ToolTip for preferred name label.
        /// </summary>
        /// <value>ToolTip message to be shown for preferred name label.</value>
        [Category("Behavior")]
        public string PreferredNameLabelToolTip
        {
            get { return (string)this.GetValue(PreferredNameLabelToolTipProperty); }
            set { this.SetValue(PreferredNameLabelToolTipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip for date of birth.
        /// </summary>
        /// <value>Date of birth tooltip.</value>
        [Category("Behavior")]
        public string DateOfBirthToolTip
        {
            get { return (string)this.GetValue(DateOfBirthToolTipProperty); }
            set { this.SetValue(DateOfBirthToolTipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip for date of birth label.
        /// </summary>
        /// <value>Date of birth label tooltip.</value>
        [Category("Behavior")]
        public string DateOfBirthLabelToolTip
        {
            get { return (string)this.GetValue(DateOfBirthLabelToolTipProperty); }
            set { this.SetValue(DateOfBirthLabelToolTipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip for date of death.
        /// </summary>
        /// <value>Date of death tooltip.</value>
        [Category("Behavior")]
        public string DateOfDeathToolTip
        {
            get { return (string)this.GetValue(DateOfDeathToolTipProperty); }
            set { this.SetValue(DateOfDeathToolTipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip for date of death label.
        /// </summary>
        /// <value>Date of death label tooltip.</value>
        [Category("Behavior")]
        public string DateOfDeathLabelToolTip
        {
            get { return (string)this.GetValue(DateOfDeathLabelToolTipProperty); }
            set { this.SetValue(DateOfDeathLabelToolTipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip for gender.
        /// </summary>
        /// <value>Gender value tooltip.</value>
        [Category("Behavior")]
        public string GenderValueTooltip
        {
            get { return (string)this.GetValue(GenderValueTooltipProperty); }
            set { this.SetValue(GenderValueTooltipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip for gender label.
        /// </summary>
        /// <value>Gender label tooltip.</value>
        [Category("Behavior")]
        public string GenderLabelTooltip
        {
            get { return (string)this.GetValue(GenderLabelTooltipProperty); }
            set { this.SetValue(GenderLabelTooltipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip for identifier.
        /// </summary>
        /// <value>Identifier tooltip.</value>
        [Category("Behavior")]
        public string IdentifierTooltip
        {
            get { return (string)this.GetValue(IdentifierTooltipProperty); }
            set { this.SetValue(IdentifierTooltipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip for identifier label.
        /// </summary>
        /// <value>Identifier label tooltip.</value>
        [Category("Behavior")]
        public string IdentifierLabelTooltip
        {
            get { return (string)this.GetValue(IdentifierLabelTooltipProperty); }
            set { this.SetValue(IdentifierLabelTooltipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip for age at death value.
        /// </summary>
        /// <value>Age at death tooltip.</value>
        [Category("Behavior")]
        public string AgeAtDeathTooltip
        {
            get { return (string)this.GetValue(AgeAtDeathTooltipProperty); }
            set { this.SetValue(AgeAtDeathTooltipProperty, value); }
        }      

        /// <summary>
        /// Gets or sets the tooltip for Age at Death label.
        /// </summary>
        /// <value>Age at death label tooltip.</value>
        [Category("Behavior")]
        public string AgeAtDeathLabelTooltip
        {
            get { return (string)this.GetValue(AgeAtDeathLabelTooltipProperty); }
            set { this.SetValue(AgeAtDeathLabelTooltipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the tooltip for zone two header.
        /// </summary>
        /// <value>Zone two header tooltip.</value>
        [Category("Behavior")]
        public string ZoneTwoTooltip
        {
            get { return (string)this.GetValue(ZoneTwoTooltipProperty); }
            set { this.SetValue(ZoneTwoTooltipProperty, value); }
        }

        /// <summary>
        /// Gets or sets the width of sub section one.
        /// </summary>
        /// <value>Sub section one width.</value>
        [Category("Appearance")]
        public GridLength SubSectionOneWidth
        {
            get 
            { 
                return this.subsectionOneWidth; 
            }

            set
            {
                this.subsectionOneWidth = value;
                this.SetColumnWidth(0, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of sub section two.
        /// </summary>
        /// <value>Sub section two width.</value>
        [Category("Appearance")]
        public GridLength SubSectionTwoWidth
        {
            get
            {
                return this.subsectionTwoWidth;
            }

            set
            {
                this.subsectionTwoWidth = value;
                this.SetColumnWidth(1, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of sub section three.
        /// </summary>
        /// <value>Sub section three width.</value>
        [Category("Appearance")]
        public GridLength SubSectionThreeWidth
        {
            get
            {
                return this.subsectionThreeWidth;
            }

            set
            {
                this.subsectionThreeWidth = value;
                this.SetColumnWidth(2, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of sub section four.
        /// </summary>
        /// <value>Sub section four width.</value>
        [Category("Appearance")]
        public GridLength SubSectionFourWidth
        {
            get
            {
                return this.subsectionFourWidth;
            }

            set
            {
                this.subsectionFourWidth = value;
                this.SetColumnWidth(3, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of sub section five.
        /// </summary>
        /// <value>Sub section five width.</value>
        [Category("Appearance")]
        public GridLength SubSectionFiveWidth
        {
            get
            {
                return this.subsectionFiveWidth;
            }

            set
            {
                this.subsectionFiveWidth = value;
                this.SetColumnWidth(4, value);
            }
        }

        /// <summary>
        /// Gets or sets the title for sub section one.
        /// </summary>
        /// <value>Sub section one title.</value>
        [Category("Appearance")]
        public string SubSectionOneTitle
        {
            get { return (string)this.GetValue(SubSectionOneTitleProperty); }
            set { this.SetValue(SubSectionOneTitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the title for sub section two.
        /// </summary>
        /// <value>Sub section two title.</value>
        [Category("Appearance")]
        public string SubSectionTwoTitle
        {
            get { return (string)this.GetValue(SubSectionTwoTitleProperty); }
            set { this.SetValue(SubSectionTwoTitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the title for sub section three.
        /// </summary>
        /// <value>Sub section three title.</value>
        [Category("Appearance")]
        public string SubSectionThreeTitle
        {
            get { return (string)this.GetValue(SubSectionThreeTitleProperty); }
            set { this.SetValue(SubSectionThreeTitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the title for sub section four.
        /// </summary>
        /// <value>Sub section four title.</value>
        [Category("Appearance")]
        public string SubSectionFourTitle
        {
            get { return (string)this.GetValue(SubSectionFourTitleProperty); }
            set { this.SetValue(SubSectionFourTitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the url of image for drop down.
        /// </summary>
        /// <remarks>
        /// This image is set on the expand/collapse button when control is collapsed.
        /// </remarks>
        /// <value>Drop down image.</value>
        [Category("Appearance")]
        public ImageSource DropDownImage
        {
            get { return (ImageSource)this.GetValue(DropDownImageProperty); }
            set { this.SetValue(DropDownImageProperty, value); }
        }

        /// <summary>
        /// Gets or sets the url of image for collapse.
        /// </summary>
        /// <remarks>
        /// This image is set on the expand/collapse button when control is expanded.
        /// </remarks>
        /// <value>Collapse image.</value>
        [Category("Appearance")]
        public ImageSource CollapseImage
        {
            get { return (ImageSource)this.GetValue(CollapseImageProperty); }
            set { this.SetValue(CollapseImageProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether zone two is expanded or not.
        /// </summary>
        /// <value>Zone two status.</value>
        [Category("Appearance")]
        public bool ZoneTwoExpanded
        {
            get { return (bool)this.GetValue(ZoneTwoExpandedProperty); }
            set { this.SetValue(ZoneTwoExpandedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the address type of the patient’s address.
        /// </summary>
        /// <value>Address type label text.</value>
        [Category("Patient Address")]
        public string AddressTypeLabelText
        {
            get { return (string)this.GetValue(AddressTypeLabelTextProperty); }
            set { this.SetValue(AddressTypeLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the first line of the patient's address.
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
        /// Gets or sets the second line of the patient's address.
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
        /// Gets or sets the third line of the patient's address.
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
        /// Gets or sets the town field in the patient's address.
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        /// <value>Town field in address.</value>
        [Category("Patient Address")]
        public string Town
        {
            get { return (string)this.GetValue(TownProperty); }
            set { this.SetValue(TownProperty, value); }
        }

        /// <summary>
        /// Gets or sets the county field in the patient's address. 
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        /// <value>County field in address.</value>
        [Category("Patient Address")]
        public string County
        {
            get { return (string)this.GetValue(CountyProperty); }
            set { this.SetValue(CountyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the country field in the patient's address.
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        /// <value>Country field in address.</value>
        [Category("Patient Address")]
        public string Country
        {
            get { return (string)this.GetValue(CountryProperty); }
            set { this.SetValue(CountryProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Postcode of the patient's address.
        /// </summary>
        /// <remarks>
        /// All stored address data should display. If an address element is empty, it will not be displayed. 
        /// </remarks>
        /// <value>Postcode in address.</value>
        [Category("Patient Address")]
        public string Postcode
        {
            get { return (string)this.GetValue(PostcodeProperty); }
            set { this.SetValue(PostcodeProperty, string.IsNullOrEmpty(value) ? value : value.ToUpper(CultureInfo.CurrentCulture)); }
        }

        /// <summary>
        /// Gets or sets the home phone number of patient.
        /// </summary>
        /// <value>Home phone number.</value>
        [Category("Patient Details")]
        public string HomePhoneNumber
        {
            get { return (string)this.GetValue(HomePhoneNumberProperty); }
            set { this.SetValue(HomePhoneNumberProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text for home phone label.
        /// </summary>
        /// <value>Home phone label text.</value>
        [Category("Patient Details")]
        public string HomePhoneLabelText
        {
            get { return (string)this.GetValue(HomePhoneLabelTextProperty); }
            set { this.SetValue(HomePhoneLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the work phone number of patient.
        /// </summary>
        /// <value>Work phone number.</value>
        [Category("Patient Details")]
        public string WorkPhoneNumber
        {
            get { return (string)this.GetValue(WorkPhoneNumberProperty); }
            set { this.SetValue(WorkPhoneNumberProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text for work phone label.
        /// </summary>
        /// <value>Work phone label text.</value>
        [Category("Patient Details")]
        public string WorkPhoneLabelText
        {
            get { return (string)this.GetValue(WorkPhoneLabelTextProperty); }
            set { this.SetValue(WorkPhoneLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the mobile phone number of patient.
        /// </summary>
        /// <value>Mobile phone number.</value>
        [Category("Patient Details")]
        public string MobilePhoneNumber
        {
            get { return (string)this.GetValue(MobilePhoneNumberProperty); }
            set { this.SetValue(MobilePhoneNumberProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text for mobile phone label.
        /// </summary>
        /// <value>Mobile phone label text.</value>
        [Category("Patient Details")]
        public string MobilePhoneLabelText
        {
            get { return (string)this.GetValue(MobilePhoneLabelTextProperty); }
            set { this.SetValue(MobilePhoneLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the email address of patient.
        /// </summary>
        /// <value>Email address.</value>
        [Category("Patient Details")]
        public string EmailAddress
        {
            get { return (string)this.GetValue(EmailAddressProperty); }
            set { this.SetValue(EmailAddressProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text for email label.
        /// </summary>
        /// <value>Email label text.</value>
        [Category("Patient Details")]
        public string EmailLabelText
        {
            get { return (string)this.GetValue(EmailLabelTextProperty); }
            set { this.SetValue(EmailLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets an object that contains the content for subsection four.
        /// </summary>
        /// <remarks>The default value is null reference (Nothing in Visual Basic).</remarks>
        /// <value>Sub section three content.</value>
        [Category("Appearance")]
        public object SubSectionThreeContent
        {
            get { return (object)this.GetValue(SubSectionThreeContentProperty); }
            set { this.SetValue(SubSectionThreeContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets an object that contains the content for subsection four.
        /// </summary>
        /// <remarks>The default value is null reference (Nothing in Visual Basic).</remarks>
        /// <value>Sub section four content.</value>
        [Category("Appearance")]
        public object SubSectionFourContent
        {
            get { return (object)this.GetValue(SubSectionFourContentProperty); }
            set { this.SetValue(SubSectionFourContentProperty, value); }
        }

        /// <summary>
        /// Gets or sets a collection of patient allergies.
        /// </summary>        
        /// <value>Allergy collection.</value>        
        [Category("Patient Details")]
        public AllergyCollection Allergies
        {
            get
            {
                return (AllergyCollection)this.GetValue(AllergiesProperty);
            }

            set
            {
                this.SetValue(AllergiesProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the allergy information.
        /// </summary>
        /// <value>Allergy information.</value>
        [Category("Patient Details")]
        public AllergyInformation AllergyInformation
        {
            get { return (AllergyInformation)this.GetValue(AllergyInformationProperty); }
            set { this.SetValue(AllergyInformationProperty, value); }
        }

        /// <summary>
        /// Gets or sets the URL of icon to indicate allergy is not present.
        /// </summary>
        /// /// <value>Allergy not present icon.</value>
        [Category("Appearance")]
        public ImageSource AllergiesNotPresentIcon
        {
            get { return (ImageSource)this.GetValue(AllergiesNotPresentIconProperty); }
            set { this.SetValue(AllergiesNotPresentIconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the URL of icon to indicate allergy is present.
        /// </summary>
        /// <value>Allergy present icon.</value>
        [Category("Appearance")]
        public ImageSource AllergiesPresentIcon
        {
            get { return (ImageSource)this.GetValue(AllergiesPresentIconProperty); }
            set { this.SetValue(AllergiesPresentIconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the URL of icon to indicate allergy is unavailable.
        /// </summary>
        /// <value>Allergy unavailable icon.</value>
        [Category("Appearance")]
        public ImageSource AllergiesUnavailableIcon
        {
            get { return (ImageSource)this.GetValue(AllergiesUnavailableIconProperty); }
            set { this.SetValue(AllergiesUnavailableIconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text for view all address link.
        /// </summary>
        /// <value>View all address link text.</value>
        [Category("Appearance")]
        public string ViewAllAddressLinkText
        {
            get { return (string)this.GetValue(ViewAllAddressLinkTextProperty); }
            set { this.SetValue(ViewAllAddressLinkTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text for view all contact details link.
        /// </summary>
        /// <value>View all contaxt details link text.</value>
        [Category("Appearance")]
        public string ViewAllContactDetailsLinkText
        {
            get { return (string)this.GetValue(ViewAllContactDetailsLinkTextProperty); }
            set { this.SetValue(ViewAllContactDetailsLinkTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the text for view all allergy link.
        /// </summary>
        /// <value>View allergy record link text.</value>
        [Category("Appearance")]
        public string ViewAllergyRecordLinkText
        {
            get { return (string)this.GetValue(ViewAllergyRecordLinkTextProperty); }
            set { this.SetValue(ViewAllergyRecordLinkTextProperty, value); }
        }       

        /// <summary>
        /// Gets or sets the border width.
        /// </summary>
        /// <value>Border width.</value>
        [Category("Appearance")]
        public double BorderWidth
        {
            get 
            { 
                return (double)this.GetValue(BorderWidthProperty); 
            }

            set 
            {                
                this.SetValue(BorderWidthProperty, value); 
            }
        }        
        
        /// <summary>
        /// Gets or sets the format in which the address will be displayed. 
        /// </summary>        
        /// <value>Can be InLine or InForm.</value>
        [Category("Patient Address")]
        public AddressDisplayFormat AddressDisplayFormat
        {
            get { return (AddressDisplayFormat) this.GetValue(AddressDisplayFormatProperty); }
            set { this.SetValue(AddressDisplayFormatProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Minimum height for Zone one.
        /// </summary>
        /// <value>Minimum height of Zone one.</value>
        [Category("Appearance")]
        public double ZoneOneMinHeight
        {
            get { return (double)this.GetValue(ZoneOneMinHeightProperty); }
            set { this.SetValue(ZoneOneMinHeightProperty, value); }
        }

        /// <summary>
        /// Gets the string format of the date of birth.
        /// </summary>
        /// <value>String format of the date of birth.</value>
#if !SILVERLIGHT
        [ReadOnly(true)]
#endif
        [Category("Patient Details")]
        public string DateOfBirthText
        {
            get { return (string)this.GetValue(DateOfBirthTextProperty); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the patient image is visible.
        /// </summary>
        /// <value>Value indicating whether the patient image is visible.</value>
        [Category("Patient Details")]
        public Visibility PatientImageVisible
        {
            get { return (Visibility)this.GetValue(PatientImageVisibleProperty); }
            set { this.SetValue(PatientImageVisibleProperty, value); }
        }                      
                
        /// <summary>
        /// Gets the age at death.
        /// </summary>
        /// <value>Age at death.</value>
        public string AgeAtDeath
        {
            get { return (string)this.GetValue(AgeAtDeathProperty); }
        }        

        /// <summary>
        /// Gets a string representation of the date of death in Cui Date format.
        /// </summary>
        /// <value>Date of death value in CUI date format.</value>
        public string DateOfDeathText
        {
            get { return (string)this.GetValue(DateOfDeathTextProperty); }
        }        

        /// <summary>
        /// Gets the preview text for contact label control.
        /// </summary>
        /// <value>Preview value for the contact details.</value>
        public string ContactPreview
        {
            get { return (string)this.GetValue(ContactPreviewProperty); }
        }

        /// <summary>
        /// Gets the preview text for address label control.
        /// </summary>
        /// <value>Display value for the address in Inline form.</value>
        public string AddressPreview
        {
            get { return (string)this.GetValue(AddressPreviewProperty); }
        }

        /// <summary>
        /// Gets the allergy status message.
        /// </summary>
        /// <value>Display value for the allergy status.</value>
        public string AllergyStatus
        {
            get { return (string)this.GetValue(AllergyStatusProperty); }
        }

        #region Public Style Properties
        /// <summary>
        /// Gets or sets the style to be applied for patient name.
        /// </summary>
        /// <value>Style for patient name.</value>
        public Style PatientNameStyle
        {
            get { return (Style)this.GetValue(PatientNameStyleProperty); }
            set { this.SetValue(PatientNameStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style to be applied for zone one labels.
        /// </summary>
        /// <value>Style for zone one labels.</value>
        public Style ZoneOneLabelStyle
        {
            get { return (Style)this.GetValue(ZoneOneLabelStyleProperty); }
            set { this.SetValue(ZoneOneLabelStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style to be applied for zone one data.
        /// </summary>
        /// <value>Style for zone one data.</value>
        public Style ZoneOneDataStyle
        {
            get { return (Style)this.GetValue(ZoneOneDataStyleProperty); }
            set { this.SetValue(ZoneOneDataStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style to be applied for zone two tile labels.
        /// </summary>
        /// <value>Style for zone two title labels.</value>
        public Style ZoneTwoTitleLabelStyle
        {
            get { return (Style)this.GetValue(ZoneTwoTitleLabelStyleProperty); }
            set { this.SetValue(ZoneTwoTitleLabelStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style to be applied for zone two title data.
        /// </summary>
        /// <value>Style for zone two title data.</value>
        public Style ZoneTwoTitleDataStyle
        {
            get { return (Style)this.GetValue(ZoneTwoTitleDataStyleProperty); }
            set { this.SetValue(ZoneTwoTitleDataStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style to be applied for zone two labels.
        /// </summary>
        /// <value>Style for zone two labels.</value>
        public Style ZoneTwoLabelStyle
        {
            get { return (Style)this.GetValue(ZoneTwoLabelStyleProperty); }
            set { this.SetValue(ZoneTwoLabelStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style to be applied for zone two data.
        /// </summary>
        /// <value>Style for zone two data.</value>
        public Style ZoneTwoDataStyle
        {
            get { return (Style)this.GetValue(ZoneTwoDataStyleProperty); }
            set { this.SetValue(ZoneTwoDataStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the style to be applied for zone two links.
        /// </summary>
        /// <value>Style for zone two links.</value>
        public Style ZoneTwoLinksStyle
        {
            get { return (Style)this.GetValue(ZoneTwoLinksStyleProperty); }
            set { this.SetValue(ZoneTwoLinksStyleProperty, value); }
        }
        #endregion
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Overrides the on apply template method.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.rootGrid = this.GetTemplateChild<Grid>(RootGridName, true);
            this.btnExpand = GetTemplateChild<Button>(ExpandButtonName, true);
            this.imgPatient = GetTemplateChild<Image>(PatientImageName, true);

            this.btnExpand.Click += new RoutedEventHandler(this.BtnExpand_Click);
            this.btnExpand.GotFocus += new RoutedEventHandler(this.BtnExpand_GotFocus);
            this.btnExpand.LostFocus += new RoutedEventHandler(this.BtnExpand_LostFocus);
            
            this.imgPatient.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(this.ImgPatient_ImageFailed);
            this.imgExpandCollapse.ImageFailed += new EventHandler<ExceptionRoutedEventArgs>(this.ImgExpandCollapse_ImageFailed);

            this.SetPatientImage();           

            this.btnExpand.Content = this.imgExpandCollapse;
            this.imgExpandCollapse.Source = this.DropDownImage;

            this.imgExpandCollapse.Stretch = Stretch.None;

            this.ignorePropertyChange = false;
            this.SetValue(DateOfBirthTextProperty, new CuiDate(this.DateOfBirth).ToString());            
            this.SetValue(DateOfDeathTextProperty, new CuiDate(this.DateOfDeath).ToString());
            this.ignorePropertyChange = true;

            this.InitializeTemplateParts();
            this.RegisterZoneOneEvents();
            this.InitializeZoneTwoElements();
            this.InitializeZoneTwoHeaderElements();
            this.RegisterZoneTwoLinksEvents();

            this.SetBorderThickness();
            this.SetDatesAndAge();
            this.SetAllergyImage();
            this.SetAllergyInformationFont();
            this.ShowAllergies();
            this.SetPreferredLabelDisplay(this.PreferredName);
            VisualStateManager.GoToState(this, ZoneTwoNormal, true);

            this.SetColumnWidth(0, this.SubSectionOneWidth);
            this.SetColumnWidth(1, this.SubSectionTwoWidth);
            this.SetColumnWidth(2, this.SubSectionThreeWidth);
            this.SetColumnWidth(3, this.SubSectionFourWidth);
            this.SetColumnWidth(4, this.SubSectionFiveWidth);                     

            this.zoneOne.SizeChanged += new SizeChangedEventHandler(this.ZoneOne_SizeChanged);
            this.patientDetailsPanel.SizeChanged += new SizeChangedEventHandler(this.ZoneOne_SizeChanged);
            this.patientNamePanel.SizeChanged += new SizeChangedEventHandler(this.ZoneOne_SizeChanged);

            if (this.subsectionOneContentPanel != null)
            {
                this.subsectionOneContentPanel.SizeChanged += new SizeChangedEventHandler(this.ZoneTwoContent_SizeChanged);
            }

            if (this.subsectionTwoContentPanel != null)
            {
                this.subsectionTwoContentPanel.SizeChanged += new SizeChangedEventHandler(this.ZoneTwoContent_SizeChanged);
            }
        }            
        #endregion

        #region Automation

        /// <summary>
        /// Automation object for the patient banner control.
        /// </summary>
        /// <returns>Automation object for patient banner.</returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new PatientBannerAutomationPeer(this);
        }

        #endregion

        #region Property Changed Callbacks
        /// <summary>
        /// Handles the property value changed for border width.
        /// </summary>
        /// <param name="d">PatientBanner whose properties were changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnBorderWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                PatientBanner banner = d as PatientBanner;
                if (banner != null)
                {
                    banner.SetBorderThickness();
                }
            }
        }        

        /// <summary>
        /// Handles the property value changed for date of birth and date of death.
        /// </summary>
        /// <param name="d">PatientBanner whose properties were changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                PatientBanner banner = d as PatientBanner;
                if (banner != null)
                {
                    if (e.Property == DateOfBirthProperty)
                    {
                        if (((DateTime)e.NewValue) > DateTime.Now)
                        {
                            throw new ArgumentException(PatientBannerResources.FutureDateOfBirth);
                        }

                        banner.SetDatesAndAge();
                    }                    
                    else if (e.Property == DateOfDeathProperty)
                    {
                        if (((DateTime)e.NewValue) > DateTime.Now)
                        {
                            throw new ArgumentException(PatientBannerResources.FutureDateOfDeath);
                        }

                        banner.SetDatesAndAge();
                    }                    
                }
            }
        }        

        /// <summary>
        /// Handles the changes in address properties.
        /// </summary>
        /// <param name="d">PatientBanner whose properties were changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnAddressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                PatientBanner banner = d as PatientBanner;
                if (banner != null)
                {
                    banner.SetAddressPreview();                    
                }                
            }
        }

        /// <summary>
        /// Handles the changes in Contact details properties.
        /// </summary>
        /// <param name="d">PatientBanner whose properties were changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnContactDetailsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                PatientBanner banner = d as PatientBanner;
                if (banner != null)
                {
                    banner.SetContactPreview();
                }
            }
        }        

        /// <summary>
        /// Handles the allergy changes.
        /// </summary>
        /// <param name="d">PatientBanner whose properties were changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnAllergyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                PatientBanner banner = d as PatientBanner;
                if (banner != null && banner.rootGrid != null)
                {
                    banner.ShowAllergies();
                }
            }
        }

        /// <summary>
        /// Handles the allergy changes.
        /// </summary>
        /// <param name="d">PatientBanner whose properties were changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnAllergyInformationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                PatientBanner banner = d as PatientBanner;
                if (banner != null)
                {
                    banner.SetAllergyImage();
                    banner.SetAllergyInformationFont();
                    banner.ShowAllergies();
                }
            }
        }       

        /// <summary>
        /// Handles the preferred name changes.
        /// </summary>
        /// <param name="d">PatientBanner whose properties were changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnPreferredNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                PatientBanner banner = d as PatientBanner;
                if (banner != null)
                {
                    string preferredName = (string)e.NewValue;
                    banner.SetPreferredLabelDisplay(string.IsNullOrEmpty(preferredName) ? string.Empty : preferredName.Trim());                    
                }
            }
         }
                        
        /// <summary>
        /// Handles the property value changed callback events for Zone two expanded.
        /// </summary>
        /// <param name="d">PatientBanner whose properties were changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnZoneTwoExpandStateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                PatientBanner banner = d as PatientBanner;
                if (banner != null)
                {
                    banner.ExpandZoneTwo((bool)e.NewValue);
                    if (banner.ZoneTwoExpanded)
                    {
                        banner.imgExpandCollapse.Source = banner.CollapseImage;
                    }
                    else
                    {
                        banner.imgExpandCollapse.Source = banner.DropDownImage;
                    }
                }
            }
        }          
               
        /// <summary>
        /// Handles the data changed event for Zone one minimum height.
        /// </summary>
        /// <param name="d">Patient banner whose properties were changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnZoneOneMinHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                PatientBanner banner = d as PatientBanner;
                if (banner != null)
                {
                    banner.SetZoneOneMinHeight((double)e.NewValue);
                }
            }
        }

        /// <summary>
        /// Handles the data changed event for Expand collapse buttons.
        /// </summary>
        /// <param name="d">Patient banner whose properties were changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnExpandCollapseImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                PatientBanner banner = d as PatientBanner;
                if (banner != null && banner.imgExpandCollapse != null)
                {
                    if (e.Property == DropDownImageProperty)
                    {
                        if (!banner.ZoneTwoExpanded)
                        {
                            banner.imgExpandCollapse.Source = banner.DropDownImage;    
                        }
                    }

                    if (e.Property == CollapseImageProperty)
                    {
                        if (banner.ZoneTwoExpanded)
                        {
                            banner.imgExpandCollapse.Source = banner.CollapseImage;
                        }
                    } 
                }                
            }
        }

        /// <summary>
        /// Handles the property changed event for readonly dependency properties.
        /// </summary>
        /// <param name="d">Patient banner whose properties were changed.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnPropertyValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PatientBanner patientBanner = d as PatientBanner;
            if (patientBanner != null)
            {
                if (patientBanner.ignorePropertyChange)
                {
                    patientBanner.ignorePropertyChange = false;
                    patientBanner.SetValue(e.Property, e.OldValue);
                    patientBanner.ignorePropertyChange = true;
                    throw new InvalidOperationException("Property is readonly");
                }                
            }            
        }

        /// <summary>
        /// Handles the data changed event for patient image.
        /// </summary>
        /// <param name="d">Patient banner whose patient image ischanged.</param>
        /// <param name="e">Instance of <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> containing the data.</param>
        private static void OnPatientImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != e.NewValue)
            {
                PatientBanner banner = d as PatientBanner;
                if (banner != null && banner.imgPatient != null)
                {
                    banner.SetPatientImage();
                }
            }
        }
        #endregion        

        #region Private Methods

        /// <summary>
        /// Returns formatted age value.
        /// </summary>
        /// <param name="startDate">Start date value.</param>
        /// <param name="endDate">End date value.</param>
        /// <returns>Formatted age value.</returns>
        private static string GetFormattedAge(DateTime startDate, DateTime endDate)
        {
            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                // Handles the DoB not known and DoD known condition.
                return PatientBannerResources.AgeNotKnown;
            }

            CuiTimeSpan time = new CuiTimeSpan();
            time.IsAge = true;
            time.From = startDate;
            time.To = endDate;
            return time.ToString();
        }

        /// <summary>
        /// Gets the image from the path specified.
        /// </summary>
        /// <param name="imagePath">Path for the image.</param>
        /// <returns>Image from the path specified.</returns>
        private static BitmapImage GetImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                return null;
            }

            return new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// Ellipses the content for the label and data pair based on the available width.
        /// </summary>
        /// <param name="label">Label element.</param>
        /// <param name="data">Data element.</param>
        /// <param name="availableWidth">Available width.</param>
        private static void AutoEllipseContent(Label label, Label data, double availableWidth)
        {
            if (label != null && data != null)
            {
                label.ResetDisplayValue();
                data.ResetDisplayValue();

                label.Visibility = Visibility.Visible;
                data.Visibility = Visibility.Visible;

                if (label.RequiredWidth > availableWidth - label.Margin.Left - label.Margin.Right)
                {
                    data.Visibility = Visibility.Collapsed;
                    label.AutoEllipse(availableWidth - label.Margin.Left - label.Margin.Right);
                }
                else
                {
                    data.AutoEllipse(availableWidth - (label.RequiredWidth + label.Margin.Right + label.Margin.Left + data.Margin.Left + data.Margin.Right));
                }
            }
        }

        /// <summary>
        /// Ellipses the content for the label based on the available width.
        /// </summary>
        /// <param name="label">Label element.</param>
        /// <param name="availableWidth">Available width.</param>
        private static void AutoEllipseContent(Label label, double availableWidth)
        {
            if (label != null)
            {
                label.ResetDisplayValue();
                label.Visibility = Visibility.Visible;

                if (label.RequiredWidth > availableWidth - label.Margin.Left - label.Margin.Right)
                {
                    label.AutoEllipse(availableWidth - label.Margin.Left - label.Margin.Right);
                }
            }
        }

        /// <summary>
        /// Hides/Shows preferred name label .
        /// </summary>
        /// <param name="name">Preferred name value.</param>
        private void SetPreferredLabelDisplay(string name)
        {            
            if (this.preferredNameDetailsPanel != null)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    this.preferredNameDetailsPanel.Visibility = Visibility.Visible;
                    this.SetTabStopsForPreferredName(true);
                }
                else
                {
                    this.preferredNameDetailsPanel.Visibility = Visibility.Collapsed;
                    this.SetTabStopsForPreferredName(false);
                }
            }
        }

        /// <summary>
        /// Sets whether the preferred name and label can receive focus.
        /// </summary>
        /// <param name="isfocusable">Indicates whether elements can receive focus.</param>
        private void SetTabStopsForPreferredName(bool isfocusable)
        {
            // Hack: SL runtime is setting focus to elements when the parent is collapsed.
            // Can be removed when the SL runtime changes its behavior.
            this.preferredNameLabel.IsTabStop = isfocusable;
            this.preferredName.IsTabStop = isfocusable;
        }

        /// <summary>
        /// Sets the dates and age.
        /// </summary>
        private void SetDatesAndAge()
        {
            this.deceased = false;
            this.ignorePropertyChange = false;

            if (this.DateOfDeath == DateTime.MinValue)
            {
                if (this.DateOfBirth == DateTime.MinValue)
                {
                    this.SetValue(DateOfBirthTextProperty, PatientBannerResources.DateNotKnown);
                }
                else
                {
                    this.SetValue(DateOfBirthTextProperty, new CuiDate(this.DateOfBirth).ToString() + " (" + GetFormattedAge(this.DateOfBirth, DateTime.Now) + ")");
                }

                this.SetValue(DateOfDeathTextProperty, string.Empty);
                this.SetValue(AgeAtDeathProperty, string.Empty);                                
            }
            else
            {
                if (this.DateOfBirth == DateTime.MinValue)
                {
                    this.SetValue(DateOfBirthTextProperty, PatientBannerResources.DateNotKnown);
                }
                else
                {
                    this.SetValue(DateOfBirthTextProperty, new CuiDate(this.DateOfBirth).ToString());
                }

                if (this.DateOfDeath == DateTime.MinValue)
                {
                    this.SetValue(DateOfDeathTextProperty, PatientBannerResources.DateNotKnown);
                }
                else
                {
                    this.SetValue(DateOfDeathTextProperty, new CuiDate(this.DateOfDeath).ToString());
                }
                                
                this.SetValue(AgeAtDeathProperty, GetFormattedAge(this.DateOfBirth, this.DateOfDeath));
                this.deceased = true;
            }

            this.ignorePropertyChange = true;

            if (this.deceased)
            {
                VisualStateManager.GoToState(this, DeceasedState, true);
            }
            else
            {
                VisualStateManager.GoToState(this, AliveState, true);
            }            
        }      

        /// <summary>
        /// Show the allergies if allergy information is present.
        /// </summary>
        private void ShowAllergies()
        {            
            if (this.allergiesLabelControl != null)
            {
                if (this.AllergyInformation == AllergyInformation.Present)
                {
                    this.allergiesLabelControl.Allergies = this.Allergies;
                    this.allergiesLabelControl.Visibility = Visibility.Visible;                  
                }
                else
                {
                    this.allergiesLabelControl.Visibility = Visibility.Collapsed;                    
                }
            }            
        }

        /// <summary>
        /// Sets the allergy image based upon the allergy information.
        /// </summary>
        private void SetAllergyImage()
        {            
            if (this.allergiesImage != null)
            {
                this.ignorePropertyChange = false;
                switch (this.AllergyInformation)
                {
                    case AllergyInformation.Present:
                        this.allergiesImage.Source = this.AllergiesPresentIcon;
                        this.SetValue(AllergyStatusProperty, PatientBannerResources.AllergyPresent);
                        break;
                    case AllergyInformation.NoneKnown:
                        this.allergiesImage.Source = this.AllergiesNotPresentIcon;
                        this.SetValue(AllergyStatusProperty, PatientBannerResources.AllergyNotKnown);
                        break;
                    case AllergyInformation.NotRecorded:
                        this.allergiesImage.Source = this.AllergiesNotPresentIcon;
                        this.SetValue(AllergyStatusProperty, PatientBannerResources.AllergyNotRecorded);
                        break;
                    case AllergyInformation.Unavailable:
                        this.allergiesImage.Source = this.AllergiesUnavailableIcon;
                        this.SetValue(AllergyStatusProperty, PatientBannerResources.AllergyUnavailable);
                        break;
                }

                this.ignorePropertyChange = true;
            }         
        }        
        
        /// <summary>
        /// Sets the address preview text for subsection one header.
        /// </summary>
        private void SetAddressPreview()
        {
            StringBuilder sb = new StringBuilder();
            string separator = PatientBannerResources.AddressItemSeparator;
            string[] addressParts = 
                                    { 
                                    this.Address1, this.Address2, this.Address3, this.Town, 
                                    this.County, string.IsNullOrEmpty(this.Postcode) ? string.Empty : this.Postcode.ToUpper(CultureInfo.CurrentCulture),
                                    this.Country 
                                    };
            
            for (int i = 0; i < addressParts.Length; i++)
            {
                if (!string.IsNullOrEmpty(addressParts[i]))
                {
                    string trimmedAddressPart = addressParts[i].Trim();
                    if (trimmedAddressPart.Length > 0)
                    {
                        if (sb.Length > 0)
                        {
                            sb.Append(separator);
                        }

                        sb.Append(trimmedAddressPart);
                    }
                }
            }

            this.ignorePropertyChange = false;
            this.SetValue(AddressPreviewProperty, sb.ToString());
            this.ignorePropertyChange = true;
        }

        /// <summary>
        /// Sets the contact preview text for subsection two header.
        /// </summary>
        private void SetContactPreview()
        {
            string summary = string.Empty;
            if (!string.IsNullOrEmpty(this.HomePhoneNumber))
            {
                summary = this.HomePhoneNumber;
            }
            else if (!string.IsNullOrEmpty(this.MobilePhoneNumber))
            {
                summary = this.MobilePhoneNumber;
            }
            else if (!string.IsNullOrEmpty(this.WorkPhoneNumber))
            {
                summary = this.WorkPhoneNumber;
            }
            else if (!string.IsNullOrEmpty(this.EmailAddress))
            {
                summary = this.EmailAddress;
            }

            this.ignorePropertyChange = false;
            this.SetValue(ContactPreviewProperty, summary);
            this.ignorePropertyChange = true;
        }                      
       
        /// <summary>
        /// Handles the visibility of hyperlinks.
        /// </summary>
        /// <param name="status">Visibility status.</param>
        private void ShowHideLinks(Visibility status)
        {
            if (this.viewAllAddressLink != null)
            {
                this.viewAllAddressLink.Visibility = status;                
            }

            if (this.viewAllergyRecordLink != null)
            {
                if (this.AllergyInformation == AllergyInformation.Present || this.AllergyInformation == AllergyInformation.NoneKnown || this.AllergyInformation == AllergyInformation.NotRecorded)
                {
                    this.viewAllergyRecordLink.Visibility = status;
                }
                else
                {
                    this.viewAllergyRecordLink.Visibility = Visibility.Collapsed;
                }
            }

            if (this.viewContactDetailsLink != null)
            {
                this.viewContactDetailsLink.Visibility = status;                
            }
        }

        /// <summary>
        /// Expands or collapses zone two.
        /// </summary>
        /// <param name="expand">Boolean indicating whether to expand or collapse.</param>
        private void ExpandZoneTwo(bool expand)
        {
            if (this.rootGrid == null)
            {
                return;
            }

            if (expand)
            {
                this.rootGrid.RowDefinitions[2].Height = this.zoneTwoHeight;
                this.rootGrid.RowDefinitions[3].Height = new GridLength(1.0, GridUnitType.Auto);
                this.ShowHideLinks(Visibility.Visible);               
            }
            else
            {
                this.rootGrid.RowDefinitions[2].Height = new GridLength(0);
                this.rootGrid.RowDefinitions[3].Height = new GridLength(0);
                this.ShowHideLinks(Visibility.Collapsed);
            }

            if (this.ZoneTwoStateChanged != null)
            {
                this.ZoneTwoStateChanged(this, new RoutedEventArgs());
            }
        }

        /// <summary>
        /// Sets the minumum height of Zone one.
        /// </summary>
        /// <param name="minHeight">Minumum height of Zone one.</param>
        private void SetZoneOneMinHeight(double minHeight)
        {
            if (this.rootGrid != null)
            {
                this.rootGrid.RowDefinitions[0].MinHeight = minHeight;
            }
        }

        /// <summary>
        /// Sets the font for the Allergy information in subsection five title.
        /// </summary>        
        private void SetAllergyInformationFont()
        {
            if (this.subsectionFiveTitleLabel != null)
            {
                if (this.AllergyInformation == AllergyInformation.Present || this.AllergyInformation == AllergyInformation.NoneKnown)
                {
#if SILVERLIGHT
                    VisualStateManager.GoToState(this, AllergyDetailsKnown, true);
#else
                    this.subsectionFiveTitleLabel.Style = this.ZoneTwoTitleDataStyle;
#endif
                }
                else
                {
#if SILVERLIGHT
                    VisualStateManager.GoToState(this, AllergyDetailsNotKnown, true);
#else
                    this.subsectionFiveTitleLabel.Style = this.ZoneTwoTitleLabelStyle;
#endif
                }
            }
        }

        /// <summary>
        /// Sets the patient image to a specified URI. 
        /// </summary>
        private void SetPatientImage()
        {
            if (this.PatientImage != null)
            {
                if (this.PatientImageVisible == Visibility.Visible)
                {
                    VisualStateManager.GoToState(this, PatientImageVisibleState, true);
                    this.patientImageDisplayed = true;
                    return;
                }
            }
         
            VisualStateManager.GoToState(this, PatientImageCollapsedState, true);
            this.patientImageDisplayed = false;
        }

        /// <summary>
        /// Sets the thickness of the border.
        /// </summary>
        private void SetBorderThickness()
        {
            this.SetValue(BorderThicknessProperty, new Thickness(this.BorderWidth));
        }

        /// <summary>
        /// Check and initialize template parts.
        /// </summary>
        private void InitializeTemplateParts()
        {
            this.zoneOne = this.GetTemplateChild<FrameworkElement>(ZoneOneRootElementName, true);
            this.patientNamePanel = this.GetTemplateChild<FrameworkElement>(PatientNamePanel, true);
            this.patientDetailsPanel = this.GetTemplateChild<FrameworkElement>(PatientDetailsPanel, true);
            this.preferredNameDetailsPanel = this.GetTemplateChild<FrameworkElement>(PreferredNameDetailsPanel, false);

            this.imgPatient = this.GetTemplateChild<Image>(PatientImageName, true);
            this.nameLabel = this.GetTemplateChild<NameLabel>(NameLabel, true);
            this.preferredName = this.GetTemplateChild<Label>(PreferredNameValue, true);
            this.preferredNameLabel = this.GetTemplateChild<Label>(PreferredNameLabel, true);
            this.dobLabel = this.GetTemplateChild<Label>(DateOfBirthLabel, true);
            this.dobValue = this.GetTemplateChild<Label>(DateOfBirthValue, true);
            this.genderLabel = this.GetTemplateChild<Label>(GenderLabel, true);
            this.genderValue = this.GetTemplateChild<GenderLabel>(GenderValue, true);
            this.identifierLabel = this.GetTemplateChild<Label>(IdentifierLabel, true);
            this.identifierValue = this.GetTemplateChild<IdentifierLabel>(IdentifierValue, true);
            this.dodLabel = this.GetTemplateChild<Label>(DateOfDeathLabel, true);
            this.dodValue = this.GetTemplateChild<Label>(DateOfDeathValue, true);
            this.ageAtDeathLabel = this.GetTemplateChild<Label>(AgeAtDeathLabel, true);
            this.ageAtDeath = this.GetTemplateChild<Label>(AgeAtDeathValue, true);
            this.zoneTwoHeaderBorder = this.GetTemplateChild<Border>(ZoneTwoHeaderBorderElementName, true);
            this.viewAllAddressLink = this.GetTemplateChild<Label>(ViewAllAddressesLinkName, true);
            this.viewContactDetailsLink = this.GetTemplateChild<Label>(ViewContactDetailsLinkName, true);
            this.viewAllergyRecordLink = this.GetTemplateChild<Label>(ViewAllergyRecordLinkName, true);
        }

        /// <summary>
        /// Gets an element in the template.
        /// </summary>
        /// <typeparam name="T">Type of the element.</typeparam>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="raiseExceptions">Boolean to indicate whether to raise exceptions when not found.</param>
        /// <returns>If an element is found with the specified name and type then returns the element, else null.</returns>
        /// <remarks>If <paramref name="raiseExceptions"/> is true and the element is not found then an exception of type <see cref="System.ArgumentNullException"/> is thrown. 
        /// If an element is found but is not of specified type then an exception of type <see cref="System.ArgumentException"/> is thrown</remarks>                
        private T GetTemplateChild<T>(string elementName, bool raiseExceptions)
        {
            object obj = null;
            obj = this.GetTemplateChild(elementName);
            T typeCastedObj;

            if (raiseExceptions)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException(string.Format(System.Globalization.CultureInfo.CurrentCulture, TemplatePartElementNullMessage, elementName));
                }

                typeCastedObj = (T)obj;
                if (typeCastedObj == null)
                {
                    throw new ArgumentException(string.Format(System.Globalization.CultureInfo.CurrentCulture, TemplatePartElementTypeInvalidMessage, elementName, typeof(T).FullName));
                }
            }

            return (T)obj;
        }

        /// <summary>
        /// Registers the events for Zone one elements.
        /// </summary>
        private void RegisterZoneOneEvents()
        {
            this.nameLabel.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.NameClick != null)
                {
                    this.NameClick(this, e);
                }
            };

            this.preferredNameLabel.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.PreferredNameLabelClick != null)
                {
                    this.PreferredNameLabelClick(this, e);
                }
            };

            this.preferredName.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.PreferredNameClick != null)
                {
                    this.PreferredNameClick(this, e);
                }
            };

            this.dobLabel.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.DateOfBirthLabelClick != null)
                {
                    this.DateOfBirthLabelClick(this, e);
                }
            };

            this.dobValue.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.DateOfBirthClick != null)
                {
                    this.DateOfBirthClick(this, e);
                }
            };

            this.genderLabel.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.GenderLabelClick != null)
                {
                    this.GenderLabelClick(this, e);
                }
            };

            this.genderValue.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.GenderClick != null)
                {
                    this.GenderClick(this, e);
                }
            };

            this.identifierLabel.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.IdentifierLabelClick != null)
                {
                    this.IdentifierLabelClick(this, e);
                }
            };

            this.identifierValue.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.IdentifierClick != null)
                {
                    this.IdentifierClick(this, e);
                }
            };

            this.dodLabel.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.DateOfDeathLabelClick != null)
                {
                    this.DateOfDeathLabelClick(this, e);
                }
            };

            this.dodValue.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.DateOfDeathClick != null)
                {
                    this.DateOfDeathClick(this, e);
                }
            };

            this.ageAtDeathLabel.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.AgeAtDeathLabelClick != null)
                {
                    this.AgeAtDeathLabelClick(this, e);
                }
            };

            this.ageAtDeath.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.AgeAtDeathClick != null)
                {
                    this.AgeAtDeathClick(this, e);
                }
            };
        }

        /// <summary>
        /// Intializes the template elements for Zone two header and registers events.
        /// </summary>
        private void InitializeZoneTwoHeaderElements()
        {            
            this.subsectionOnePanel = this.GetTemplateChild<FrameworkElement>(SubsectionOneElementName, false);
            this.subsectionTwoPanel = this.GetTemplateChild<FrameworkElement>(SubsectionTwoElementName, false);
            this.subsectionThreePanel = this.GetTemplateChild<FrameworkElement>(SubsectionThreeElementName, false);
            this.subsectionFourPanel = this.GetTemplateChild<FrameworkElement>(SubsectionFourElementName, false);
            this.subsectionFivePanel = this.GetTemplateChild<FrameworkElement>(SubsectionFiveElementName, false);            
            
            this.subsectionOneTitleLabel = this.GetTemplateChild<Label>(SubsectionOneTitleLabelName, false);
            this.subsectionTwoTitleLabel = this.GetTemplateChild<Label>(SubsectionTwoTitleLabelName, false);
            this.subsectionThreeTitleLabel = this.GetTemplateChild<Label>(SubsectionThreeTitleLabelName, false);
            this.subsectionFourTitleLabel = this.GetTemplateChild<Label>(SubsectionFourTitleLabelName, false);
            this.subsectionFiveTitleLabel = this.GetTemplateChild<Label>(SubsectionFiveTitleLabelName, false);
            this.addressPreview = this.GetTemplateChild<Label>(AddressPreviewLabelName, false);
            this.contactPreview = this.GetTemplateChild<Label>(ContactPreviewLabelName, false);
            this.allergiesImage = this.GetTemplateChild<Image>(AllergiesImageElementName, false);

            this.RegisterTextChangedEvent(this.subsectionOneTitleLabel);
            this.RegisterTextChangedEvent(this.subsectionTwoTitleLabel);
            this.RegisterTextChangedEvent(this.subsectionThreeTitleLabel);
            this.RegisterTextChangedEvent(this.subsectionFourTitleLabel);
            this.RegisterTextChangedEvent(this.subsectionFiveTitleLabel);
            this.RegisterTextChangedEvent(this.addressPreview);
            this.RegisterTextChangedEvent(this.contactPreview);

            // Register events for header.
            this.RegisterZoneTwoHeaderEvents(this.zoneTwoHeaderBorder);
            this.RegisterZoneTwoHeaderEvents(this.subsectionOnePanel);
            this.RegisterZoneTwoHeaderEvents(this.subsectionTwoPanel);
            this.RegisterZoneTwoHeaderEvents(this.subsectionThreePanel);
            this.RegisterZoneTwoHeaderEvents(this.subsectionFourPanel);
            this.RegisterZoneTwoHeaderEvents(this.subsectionFivePanel);
            this.RegisterZoneTwoHeaderEvents(this.btnExpand);
        }

        /// <summary>
        /// Intializes the template elements for Zone two header and registers events.
        /// </summary>
        private void InitializeZoneTwoElements()
        {
            this.subsectionOneContentPanel = this.GetTemplateChild<Panel>(SubsectionOneContent, false);
            this.subsectionTwoContentPanel = this.GetTemplateChild<Panel>(SubsectionTwoContent, false);
            this.allergiesLabelControl = this.GetTemplateChild<AllergiesLabel>(AllergiesLabelControlName, false);
        }

        /// <summary>
        /// Registers events for the specified element name.
        /// </summary>
        /// <param name="element">Element to register events.</param>
        private void RegisterZoneTwoHeaderEvents(FrameworkElement element)
        {            
            if (element != null)
            {
                element.MouseEnter += new MouseEventHandler(this.ZoneTwoHeaderBorder_MouseEnter);
                element.MouseLeave += new MouseEventHandler(this.ZoneTwoHeaderBorder_MouseLeave);
                
                if (!(element is Button))
                {
                    element.MouseLeftButtonDown += new MouseButtonEventHandler(this.ZoneTwoHeaderBorder_MouseLeftButtonDown);
                }

                element.SizeChanged += new SizeChangedEventHandler(this.ZoneTwoHeaderPanel_SizeChanged);
            }
        }

        /// <summary>
        /// Registers the click events for Zone two links.
        /// </summary>
        private void RegisterZoneTwoLinksEvents()
        {            
            this.zoneTwoHeaderBorder.MouseLeftButtonDown += delegate(object sender, MouseButtonEventArgs e)
            {
                this.ZoneTwoExpanded = !this.ZoneTwoExpanded;
            };

            this.viewAllAddressLink.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.ViewAllAddressesClick != null)
                {
                    this.ViewAllAddressesClick(this, e);
                }
            };

            this.viewAllergyRecordLink.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.ViewAllergyRecordClick != null)
                {
                    this.ViewAllergyRecordClick(this, e);
                }
            };

            this.viewContactDetailsLink.Click += delegate(object sender, RoutedEventArgs e)
            {
                if (this.ViewContactDetailsClick != null)
                {
                    this.ViewContactDetailsClick(this, e);
                }
            };
        }

        /// <summary>
        /// Register the text changed event for the label.
        /// </summary>
        /// <param name="label">Label for which text changed event needs to be registered.</param>
        private void RegisterTextChangedEvent(Label label)
        {
            if (label != null)
            {
                label.TextChanged += new RoutedEventHandler(this.Label_TextChanged);
            }
        }        

        /// <summary>
        /// Sets the width for the subsection columns.
        /// </summary>
        /// <param name="index">Index of the column.</param>
        /// <param name="width">Width of the column.</param>
        private void SetColumnWidth(int index, GridLength width)
        {
            if (this.rootGrid != null && this.rootGrid.ColumnDefinitions.Count > index)
            {
                this.rootGrid.ColumnDefinitions[index].Width = width;
            }
        }

        /// <summary>
        /// Ellipse Zone two header content.
        /// </summary>
        private void AutoEllipseZoneTwoHeaders()
        {
            if (this.subsectionOnePanel != null)
            {
                PatientBanner.AutoEllipseContent(this.subsectionOneTitleLabel, this.addressPreview, this.subsectionOnePanel.ActualWidth);
            }

            if (this.subsectionTwoPanel != null)
            {
                PatientBanner.AutoEllipseContent(this.subsectionTwoTitleLabel, this.contactPreview, this.subsectionTwoPanel.ActualWidth);
            }

            if (this.subsectionThreePanel != null)
            {
                PatientBanner.AutoEllipseContent(this.subsectionThreeTitleLabel, this.subsectionThreePanel.ActualWidth);
            }

            if (this.subsectionFourPanel != null)
            {
                PatientBanner.AutoEllipseContent(this.subsectionFourTitleLabel, this.subsectionFourPanel.ActualWidth);
            }

            if (this.subsectionFivePanel != null)
            {
                PatientBanner.AutoEllipseContent(this.subsectionFiveTitleLabel, this.subsectionFivePanel.ActualWidth - this.allergiesImage.ActualWidth - this.allergiesImage.Margin.Left - this.allergiesImage.Margin.Right);
            }
        }

        #endregion      

        #region Event Handlers
        /// <summary>
        /// Handles image failed event of expand collapse image.
        /// </summary>
        /// <param name="sender">Sender of event.</param>
        /// <param name="e">Event arguments.</param>
        private void ImgExpandCollapse_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            if (this.ZoneTwoExpanded)
            {
                this.imgExpandCollapse.Source = ResourceHelper.CollapseImage;
                if (this.CollapseImageFailed != null)
                {
                    this.CollapseImageFailed(sender, e);
                }
            }
            else
            {
                this.imgExpandCollapse.Source = ResourceHelper.DropDownImage;
                if (this.DropDownImageFailed != null)
                {
                    this.DropDownImageFailed(sender, e);
                }
            }
        }

        /// <summary>
        /// Handles image failed event of patient image.
        /// </summary>
        /// <param name="sender">Sender of event.</param>
        /// <param name="e">Event arguments.</param>
        private void ImgPatient_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, PatientImageCollapsedState, true);
            this.patientImageDisplayed = false;

            if (this.PatientImageFailed != null)
            {
                this.PatientImageFailed(sender, e);
            }
        }           

        /// <summary>
        /// Handles the click event of expand button.
        /// </summary>
        /// <param name="sender">Sender of event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnExpand_Click(object sender, RoutedEventArgs e)
        {
            this.btnExpand.Focus();
            this.ZoneTwoExpanded = !this.ZoneTwoExpanded;           
        }

        /// <summary>
        /// Handles the Got focus event for the expand or collapse image.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnExpand_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ZoneTwoHeaderBorder_MouseEnter(sender, null);
        }

        /// <summary>
        /// Handles the Lost focus event for the expand or collapse image.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        private void BtnExpand_LostFocus(object sender, RoutedEventArgs e)
        {
            this.ZoneTwoHeaderBorder_MouseLeave(sender, null);
        }

        /// <summary>
        /// Handles the changes in allergies collection.
        /// </summary>
        /// <param name="sender">Sender of event.</param>
        /// <param name="e">Event arguments.</param>
        private void Allergies_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.ShowAllergies();
        }
                
        /// <summary>
        /// Handles the MouseLeave event on Zone two header row elements.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Mouse event args.</param>
        private void ZoneTwoHeaderBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            if (this.btnExpand.IsFocused)
            {
                return;
            }

            VisualStateManager.GoToState(this, ZoneTwoNormal, true);
        }

        /// <summary>
        /// Handles the MouseEnter event on Zone two header row elements.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Mouse event args.</param>
        private void ZoneTwoHeaderBorder_MouseEnter(object sender, MouseEventArgs e)
        {            
            VisualStateManager.GoToState(this, ZoneTwoHover, true);
            this.Cursor = Cursors.Hand;
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event on Zone two header row elements.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Mouse event args.</param>
        private void ZoneTwoHeaderBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.BtnExpand_Click(sender, null);
        }

        /// <summary>
        /// Event handler for Zone one size changed.
        /// </summary>
        /// <param name="sender">Element whose size changed.</param>
        /// <param name="e">Event args containing information about old and new sizes.</param>
        private void ZoneOne_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size infiniteSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            this.patientNamePanel.Measure(infiniteSize);
            this.patientDetailsPanel.Measure(infiniteSize);

            if (this.patientNamePanel.DesiredSize.Width + this.patientDetailsPanel.DesiredSize.Width > this.zoneOne.ActualWidth)
            {
                Grid.SetRow(this.patientDetailsPanel, 1);
                Grid.SetColumn(this.patientDetailsPanel, 0);
            }
            else
            {
                Grid.SetRow(this.patientDetailsPanel, 0);
                Grid.SetColumn(this.patientDetailsPanel, 1);
            }

            if (this.patientImageDisplayed)
            {
                PatientBanner.AutoEllipseContent(this.preferredNameLabel, this.preferredName, this.zoneOne.ActualWidth - this.imgPatient.ActualWidth);
            }
            else
            {
                PatientBanner.AutoEllipseContent(this.preferredNameLabel, this.preferredName, this.zoneOne.ActualWidth);
            }
        }

        /// <summary>
        /// Handles the size changed event of the patient banner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args containing information about old and new sizes.</param>
        private void PatientBanner_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.AutoEllipseZoneTwoHeaders();

            if (this.zoneOne != null)
            {
                PatientBanner.AutoEllipseContent(this.preferredNameLabel, this.preferredName, this.zoneOne.ActualWidth);
            }
        }

        /// <summary>
        /// Handles the size changed event for Zone two content.
        /// </summary>
        /// <param name="sender">Object whose size got changed.</param>
        /// <param name="e">Event args containing information about old and new sizes.</param>
        private void ZoneTwoContent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double subsectionOneContentHeight = this.subsectionOneContentPanel != null ? this.subsectionOneContentPanel.ActualHeight : 0;
            double subsectionTwoContentHeight = this.subsectionTwoContentPanel != null ? this.subsectionTwoContentPanel.ActualHeight : 0;

            this.zoneTwoHeight = new GridLength(Math.Max(subsectionOneContentHeight, subsectionTwoContentHeight));
            this.ExpandZoneTwo(this.ZoneTwoExpanded);
        }

        /// <summary>
        /// Handles the text changed event for Zone two header labels.
        /// </summary>
        /// <param name="sender">Object whose text got changed.</param>
        /// <param name="e">Event args containing instance information.</param>
        private void Label_TextChanged(object sender, RoutedEventArgs e)
        {
            this.AutoEllipseZoneTwoHeaders();
        }

        /// <summary>
        /// Handles the size changed event for Zone two header subsection panels.
        /// </summary>
        /// <param name="sender">Object whose size got changed.</param>
        /// <param name="e">Event args containing information about old and new sizes.</param>
        private void ZoneTwoHeaderPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.AutoEllipseZoneTwoHeaders();
        }
        #endregion        
    }
}
