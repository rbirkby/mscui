//-----------------------------------------------------------------------
// <copyright file="SearchAndPrescribeBase.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>02-Sep-2009</date>
// <summary>
//      The search and prescribe base control.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.Collections;

    /// <summary>
    /// The search and prescribe base control.
    /// </summary>
    public abstract class SearchAndPrescribeBase : Control
    {
        #region Drug Dependency Properties
        /// <summary>
        /// The DrugsItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty DrugsItemsSourceProperty =
            DependencyProperty.Register("DrugsItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(DrugsItemsSource_Changed)));

        /// <summary>
        /// The PrimaryDrugsItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty PrimaryDrugsItemsSourceProperty =
            DependencyProperty.Register("PrimaryDrugsItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(DrugsItemsSource_Changed)));

        /// <summary>
        /// The PrimaryDrugsItemsHeader Dependency Property.
        /// </summary>
        public static readonly DependencyProperty PrimaryDrugsItemsHeaderProperty =
            DependencyProperty.Register("PrimaryDrugsItemsHeader", typeof(object), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The SecondaryDrugsItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SecondaryDrugsItemsSourceProperty =
            DependencyProperty.Register("SecondaryDrugsItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(DrugsItemsSource_Changed)));

        /// <summary>
        /// The SecondaryDrugsItemsHeader Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SecondaryDrugsItemsHeaderProperty =
            DependencyProperty.Register("SecondaryDrugsItemsHeader", typeof(object), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The SelectedDrug Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedDrugProperty =
            DependencyProperty.Register("SelectedDrug", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedDrug_Changed)));

        /// <summary>
        /// The DrugItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty DrugItemTemplateProperty =
            DependencyProperty.Register("DrugItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(DrugItemTemplate_Changed)));

        /// <summary>
        /// The SelectedDrugItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedDrugItemTemplateProperty =
            DependencyProperty.Register("SelectedDrugItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), new PropertyMetadata(null));

        #endregion

        #region Route Dependency Properties
        /// <summary>
        /// The ValidRoutesItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ValidRoutesItemsSourceProperty =
            DependencyProperty.Register("ValidRoutesItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The BrandedRoutesItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty BrandedRoutesItemsSourceProperty =
            DependencyProperty.Register("BrandedRoutesItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The OtherRoutesItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty OtherRoutesItemsSourceProperty =
            DependencyProperty.Register("OtherRoutesItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The SelectedRoute Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedRouteProperty =
            DependencyProperty.Register("SelectedRoute", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedRoute_Changed)));

        /// <summary>
        /// The RouteItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty RouteItemTemplateProperty =
            DependencyProperty.Register("RouteItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(RouteItemTemplate_Changed)));

        /// <summary>
        /// The SelectedRouteItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedRouteItemTemplateProperty =
            DependencyProperty.Register("SelectedRouteItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), null);
        #endregion

        #region Brand Dependency Properties
        /// <summary>
        /// The ValidBrandsItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ValidBrandsItemsSourceProperty =
            DependencyProperty.Register("ValidBrandsItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The SelectedBrand Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedBrandProperty =
            DependencyProperty.Register("SelectedBrand", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedBrand_Changed)));

        /// <summary>
        /// The BrandItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty BrandItemTemplateProperty =
            DependencyProperty.Register("BrandItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The IsBrandMandatory Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsBrandMandatoryProperty =
            DependencyProperty.Register("IsBrandMandatory", typeof(bool), typeof(SearchAndPrescribeBase), new PropertyMetadata(false));

        #endregion

        #region Form Dependency Properties
        /// <summary>
        /// The ConciseFormsItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ConciseFormsItemsSourceProperty =
            DependencyProperty.Register("ConciseFormsItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The ValidFormsItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ValidFormsItemsSourceProperty =
            DependencyProperty.Register("ValidFormsItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The ValidFormsItemsHeader Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ValidFormsItemsHeaderProperty =
            DependencyProperty.Register("ValidFormsItemsHeader", typeof(object), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The OtherFormsItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty OtherFormsItemsSourceProperty =
            DependencyProperty.Register("OtherFormsItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The OtherFormsItemsHeader Dependency Property.
        /// </summary>
        public static readonly DependencyProperty OtherFormsItemsHeaderProperty =
            DependencyProperty.Register("OtherFormsItemsHeader", typeof(object), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The SelectedForm Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedFormProperty =
            DependencyProperty.Register("SelectedForm", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedForm_Changed)));

        /// <summary>
        /// The IsFormMandatory Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsFormMandatoryProperty =
            DependencyProperty.Register("IsFormMandatory", typeof(bool), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The FormItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty FormItemTemplateProperty =
            DependencyProperty.Register("FormItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), null);
        #endregion
        
        #region Strength Dependency Properties
        /// <summary>
        /// The ValidStrengthsItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ValidStrengthsItemsSourceProperty =
            DependencyProperty.Register("ValidStrengthsItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The ValidStrengthsItemsHeader Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ValidStrengthsItemsHeaderProperty =
            DependencyProperty.Register("ValidStrengthsItemsHeader", typeof(object), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The OtherStrengthsItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty OtherStrengthsItemsSourceProperty =
            DependencyProperty.Register("OtherStrengthsItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The OtherStrengthsItemsHeader Dependency Property.
        /// </summary>
        public static readonly DependencyProperty OtherStrengthsItemsHeaderProperty =
            DependencyProperty.Register("OtherStrengthsItemsHeader", typeof(object), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The SelectedStrength Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedStrengthProperty =
            DependencyProperty.Register("SelectedStrength", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedStrength_Changed)));

        /// <summary>
        /// The IsStrengthMandatory Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsStrengthMandatoryProperty =
            DependencyProperty.Register("IsStrengthMandatory", typeof(bool), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The StrengthItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty StrengthItemTemplateProperty =
            DependencyProperty.Register("StrengthItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), null);
        #endregion

        #region Dose Dependency Properties
        /// <summary>
        /// The ConciseDosagesItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ConciseDosagesItemsSourceProperty =
            DependencyProperty.Register("ConciseDosagesItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The DetailedDosagesItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty DetailedDosagesItemsSourceProperty =
            DependencyProperty.Register("DetailedDosagesItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The SelectedDose Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedDoseProperty =
            DependencyProperty.Register("SelectedDose", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedDose_Changed)));

        /// <summary>
        /// The DoseItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty DoseItemTemplateProperty =
            DependencyProperty.Register("DoseItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The CustomDoseItem Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomDoseItemProperty =
            DependencyProperty.Register("CustomDoseItem", typeof(object), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The CustomDoseItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomDoseItemTemplateProperty =
            DependencyProperty.Register("CustomDoseItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), null);
        #endregion

        #region Method Dependency Properties
        /// <summary>
        /// The SelectedMethod Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedMethodProperty =
            DependencyProperty.Register("SelectedMethod", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedMethod_Changed)));
        #endregion

        #region Site Dependency Properties
        /// <summary>
        /// The CustomSiteItem Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomSiteItemProperty =
            DependencyProperty.Register("CustomSiteItem", typeof(object), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The CustomSiteItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomSiteItemTemplateProperty =
            DependencyProperty.Register("CustomSiteItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The SelectedSite Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedSiteProperty =
            DependencyProperty.Register("SelectedSite", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedSite_Changed)));

        /// <summary>
        /// The IsSiteMandatory Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsSiteMandatoryProperty =
            DependencyProperty.Register("IsSiteMandatory", typeof(bool), typeof(SearchAndPrescribeBase), new PropertyMetadata(false));
        #endregion

        #region Frequency Dependency Properties
        /// <summary>
        /// The ConciseFrequenciesItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ConciseFrequenciesItemsSourceProperty =
            DependencyProperty.Register("ConciseFrequenciesItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The ConciseFrequenciesItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty DetailedFrequenciesItemsSourceProperty =
            DependencyProperty.Register("DetailedFrequenciesItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The SelectedFrequency Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedFrequencyProperty =
            DependencyProperty.Register("SelectedFrequency", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedFrequency_Changed)));

        /// <summary>
        /// The FrequencyItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty FrequencyItemTemplateProperty =
            DependencyProperty.Register("FrequencyItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The CustomFrequencyItem Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomFrequencyItemProperty =
            DependencyProperty.Register("CustomFrequencyItem", typeof(object), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The CustomFrequencyItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomFrequencyItemTemplateProperty =
            DependencyProperty.Register("CustomFrequencyItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The IsAsRequired Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsAsRequiredProperty =
            DependencyProperty.Register("IsAsRequired", typeof(bool), typeof(SearchAndPrescribeBase), new PropertyMetadata(false, new PropertyChangedCallback(IsAsRequired_Changed)));

        /// <summary>
        /// The IsOnceOnly Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsOnceOnlyProperty =
            DependencyProperty.Register("IsOnceOnly", typeof(bool), typeof(SearchAndPrescribeBase), new PropertyMetadata(false, new PropertyChangedCallback(IsOnceOnly_Changed)));
        #endregion

        #region Template Prescriptions Dependency Properties
        /// <summary>
        /// The ValidTemplatePrescriptionsItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ValidTemplatePrescriptionsItemsSourceProperty =
            DependencyProperty.Register("ValidTemplatePrescriptionsItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(TemplatePrescriptionsItemsSource_Changed)));

        /// <summary>
        /// The SelectedTemplatePrescription Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedTemplatePrescriptionProperty =
            DependencyProperty.Register("SelectedTemplatePrescription", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedTemplatePrescription_Changed)));

        /// <summary>
        /// The TemplatePrescriptionItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty TemplatePrescriptionItemTemplateProperty =
            DependencyProperty.Register("TemplatePrescriptionItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), null);
        #endregion

        #region Starting Condition Dependency Properties
        /// <summary>
        /// The ValidStartingConditionsItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ValidStartingConditionsItemsSourceProperty =
            DependencyProperty.Register("ValidStartingConditionsItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), new PropertyMetadata(null));

        /// <summary>
        /// The SelectedStartingCondition Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedStartingConditionProperty =
            DependencyProperty.Register("SelectedStartingCondition", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedStartingCondition_Changed)));

        /// <summary>
        /// The StartingConditionItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty StartingConditionItemTemplateProperty =
            DependencyProperty.Register("StartingConditionItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), new PropertyMetadata(null));

        /// <summary>
        /// The CustomStartingConditionItem Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomStartingConditionItemProperty =
            DependencyProperty.Register("CustomStartingConditionItem", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null));

        /// <summary>
        /// The CustomStartingConditionItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomStartingConditionItemTemplateProperty =
            DependencyProperty.Register("CustomStartingConditionItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), new PropertyMetadata(null));
        #endregion

        #region AdministrationTimes Dependency Properties
        /// <summary>
        /// The ValidAdministrationTimes Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ValidAdministrationTimesItemsSourceProperty =
            DependencyProperty.Register("ValidAdministrationTimesItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The SelectedAdministrationTimes Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedAdministrationTimesProperty =
            DependencyProperty.Register("SelectedAdministrationTimes", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedAdministrationTimes_Changed)));

        /// <summary>
        /// The SelectedAdministrationTimes Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AdministrationTimesItemTemplateProperty =
            DependencyProperty.Register("AdministrationTimesItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The AdministrationTimesLabel Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AdministrationTimesLabelProperty =
            DependencyProperty.Register("AdministrationTimesLabel", typeof(string), typeof(SearchAndPrescribeBase), null);
        #endregion

        #region StartDate Dependency Properties
        /// <summary>
        /// The SelectedStartDate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedStartDateProperty =
            DependencyProperty.Register("SelectedStartDate", typeof(DateTime?), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedStartDate_Changed)));

        /// <summary>
        /// The StartingLabel Dependency Property.
        /// </summary>
        public static readonly DependencyProperty StartingLabelProperty =
            DependencyProperty.Register("StartingLabel", typeof(object), typeof(SearchAndPrescribeBase), null);

        #endregion

        #region FirstDoseTime Dependency Properties
        /// <summary>
        /// The ValidFirstDoseTimes Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ValidFirstDoseTimesItemsSourceProperty =
            DependencyProperty.Register("ValidFirstDoseTimesItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The SelectedFirstDoseTime Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedFirstDoseTimeProperty =
            DependencyProperty.Register("SelectedFirstDoseTime", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedFirstDoseTime_Changed)));

        /// <summary>
        /// The FirstDoseTimeItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty FirstDoseTimeItemTemplateProperty =
            DependencyProperty.Register("FirstDoseTimeItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), null);
        #endregion

        #region Duration Dependency Properties
        /// <summary>
        /// The ValidDurations Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ValidDurationsItemsSourceProperty =
            DependencyProperty.Register("ValidDurationsItemsSource", typeof(IEnumerable), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The SelectedDuration Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedDurationProperty =
            DependencyProperty.Register("SelectedDuration", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedDuration_Changed)));

        /// <summary>
        /// The DurationItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty DurationItemTemplateProperty =
            DependencyProperty.Register("DurationItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The CustomDurationItem Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomDurationItemProperty =
            DependencyProperty.Register("CustomDurationItem", typeof(object), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The CustomDurationItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomDurationItemTemplateProperty =
            DependencyProperty.Register("CustomDurationItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The ContinuingLabel Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ContinuingLabelProperty =
            DependencyProperty.Register("ContinuingLabel", typeof(object), typeof(SearchAndPrescribeBase), null);
        #endregion

        #region Reason For Prescribing Dependency Properties
        /// <summary>
        /// The CustomReasonForPrescribingItem Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomReasonForPrescribingItemProperty =
            DependencyProperty.Register("CustomReasonForPrescribingItem", typeof(object), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The CustomReasonForPrescribingItemTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CustomReasonForPrescribingItemTemplateProperty =
            DependencyProperty.Register("CustomReasonForPrescribingItemTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeBase), null);

        /// <summary>
        /// The SelectedReasonForPrescribing Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedReasonForPrescribingProperty =
            DependencyProperty.Register("SelectedReasonForPrescribing", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(SelectedReasonForPrescribing_Changed)));
        #endregion

        #region Prescription Dependency Properties
        /// <summary>
        /// The IsUnlicensed Depedency Property.
        /// </summary>
        public static readonly DependencyProperty IsUnlicensedProperty =
            DependencyProperty.Register("IsUnlicensed", typeof(bool), typeof(SearchAndPrescribeBase), new PropertyMetadata(false, new PropertyChangedCallback(IsUnlicensed_Changed)));

        /// <summary>
        /// The UnlicensedReason Depedency Property.
        /// </summary>
        public static readonly DependencyProperty UnlicensedReasonProperty =
            DependencyProperty.Register("UnlicensedReason", typeof(object), typeof(SearchAndPrescribeBase), new PropertyMetadata(null, new PropertyChangedCallback(UnlicensedReason_Changed)));

        /// <summary>
        /// The IsAuthorizable Depedency Property.
        /// </summary>
        public static readonly DependencyProperty IsAuthorizableProperty =
            DependencyProperty.Register("IsAuthorizable", typeof(bool), typeof(SearchAndPrescribeBase), new PropertyMetadata(false, new PropertyChangedCallback(IsAuthorizable_Changed)));
        #endregion

        /// <summary>
        /// SearchAndPrescribe constructor.
        /// </summary>
        protected SearchAndPrescribeBase()
        {
        }

        #region Events
        /// <summary>
        /// Occurs when the selected drug changes.
        /// </summary>
        public event EventHandler SelectedDrugChanged;

        /// <summary>
        /// Occurs when the selected route changes.
        /// </summary>
        public event EventHandler SelectedRouteChanged;

        /// <summary>
        /// Occurs when the selected brand changes.
        /// </summary>
        public event EventHandler SelectedBrandChanged;

        /// <summary>
        /// Occurs when the selected form changes.
        /// </summary>
        public event EventHandler SelectedFormChanged;

        /// <summary>
        /// Occurs when the selected strength changes.
        /// </summary>
        public event EventHandler SelectedStrengthChanged;

        /// <summary>
        /// Occurs when the selected dose changes.
        /// </summary>
        public event EventHandler SelectedDoseChanged;

        /// <summary>
        /// Occurs when the selected method changes.
        /// </summary>
        public event EventHandler SelectedMethodChanged;

        /// <summary>
        /// Occurs when the selected site changes.
        /// </summary>        
        public event EventHandler SelectedSiteChanged;

        /// <summary>
        /// Occurs when the selected frequency changes.
        /// </summary>
        public event EventHandler SelectedFrequencyChanged;

        /// <summary>
        /// Occurs when the as required flag changes.
        /// </summary>
        public event EventHandler IsAsRequiredChanged;

        /// <summary>
        /// Occurs when the is once only flag changes.
        /// </summary>
        public event EventHandler IsOnceOnlyChanged;

        /// <summary>
        /// Occurs when the selected template prescription changes.
        /// </summary>
        public event EventHandler SelectedTemplatePrescriptionChanged;

        /// <summary>
        /// Occurs when the selected starting condition changes.
        /// </summary>
        public event EventHandler SelectedStartingConditionChanged;

        /// <summary>
        /// Occurs when the selected administration times changes.
        /// </summary>
        public event EventHandler SelectedAdministrationTimesChanged;

        /// <summary>
        /// Occurs when the selected start date changes.
        /// </summary>
        public event EventHandler SelectedStartDateChanged;

        /// <summary>
        /// Occurs when the selected first dose time changes.
        /// </summary>
        public event EventHandler SelectedFirstDoseTimeChanged;

        /// <summary>
        /// Occurs when the selected duration changes.
        /// </summary>
        public event EventHandler SelectedDurationChanged;

        /// <summary>
        /// Occurs when the selected reason for prescribing changes.
        /// </summary>
        public event EventHandler SelectedReasonForPrescribingChanged;

        /// <summary>
        /// Occurs when the is unlicensed flag changes.
        /// </summary>
        public event EventHandler IsUnlicensedChanged;

        /// <summary>
        /// Occurs when the unlicensed reason changes.
        /// </summary>
        public event EventHandler UnlicensedReasonChanged;

        /// <summary>
        /// Occurs when the is authorizable changes.
        /// </summary>
        public event EventHandler IsAuthorizableChanged;
        
        #endregion

        #region Drug Members
        /// <summary>
        /// Gets or sets the drugs items source.
        /// </summary>
        /// <value>The drugs items source value.</value>
        public IEnumerable DrugsItemsSource
        {
            get { return (IEnumerable)GetValue(DrugsItemsSourceProperty); }
            set { SetValue(DrugsItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the primary drugs items source.
        /// </summary>
        /// <value>The primary drugs items source.</value>
        public IEnumerable PrimaryDrugsItemsSource
        {
            get { return (IEnumerable)GetValue(PrimaryDrugsItemsSourceProperty); }
            set { SetValue(PrimaryDrugsItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the primary drugs items header.
        /// </summary>
        /// <value>The primary drugs items header.</value>
        public object PrimaryDrugsItemsHeader
        {
            get { return (object)GetValue(PrimaryDrugsItemsHeaderProperty); }
            set { SetValue(PrimaryDrugsItemsHeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the secondary drugs items source. 
        /// </summary>
        /// <value>The secondary drugs items source.</value>
        public IEnumerable SecondaryDrugsItemsSource
        {
            get { return (IEnumerable)GetValue(SecondaryDrugsItemsSourceProperty); }
            set { SetValue(SecondaryDrugsItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the secondary drugs items header.
        /// </summary>
        /// <value>The secondary drugs items header.</value>
        public object SecondaryDrugsItemsHeader
        {
            get { return (object)GetValue(SecondaryDrugsItemsHeaderProperty); }
            set { SetValue(SecondaryDrugsItemsHeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected drug.
        /// </summary>
        /// <value>The selected drug value.</value>
        public object SelectedDrug
        {
            get { return (object)GetValue(SelectedDrugProperty); }
            set { SetValue(SelectedDrugProperty, value); }
        }

        /// <summary>
        /// Gets or sets the drug item template.
        /// </summary>
        /// <value>The drug item template value.</value>
        public DataTemplate DrugItemTemplate
        {
            get { return (DataTemplate)GetValue(DrugItemTemplateProperty); }
            set { SetValue(DrugItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected drug item template.
        /// </summary>
        /// <value>The selected drug item template value.</value>
        public DataTemplate SelectedDrugItemTemplate
        {
            get { return (DataTemplate)GetValue(SelectedDrugItemTemplateProperty); }
            set { SetValue(SelectedDrugItemTemplateProperty, value); }
        }
        #endregion

        #region Route Members
        /// <summary>
        /// Gets or sets the valid routes items source.
        /// </summary>
        /// <value>The valid routes items source.</value>
        public IEnumerable ValidRoutesItemsSource
        {
            get { return (IEnumerable)GetValue(ValidRoutesItemsSourceProperty); }
            set { SetValue(ValidRoutesItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the branded routes items source.
        /// </summary>
        /// <value>The branded items source value.</value>
        public IEnumerable BrandedRoutesItemsSource
        {
            get { return (IEnumerable)GetValue(BrandedRoutesItemsSourceProperty); }
            set { SetValue(BrandedRoutesItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the other routes items source.
        /// </summary>
        /// <value>The other routes items source value.</value>
        public IEnumerable OtherRoutesItemsSource
        {
            get { return (IEnumerable)GetValue(OtherRoutesItemsSourceProperty); }
            set { SetValue(OtherRoutesItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected route.
        /// </summary>
        /// <value>The selected route value.</value>
        public object SelectedRoute
        {
            get { return (object)GetValue(SelectedRouteProperty); }
            set { SetValue(SelectedRouteProperty, value); }
        }

        /// <summary>
        /// Gets or sets the route item template.
        /// </summary>
        /// <value>The route item template value.</value>
        public DataTemplate RouteItemTemplate
        {
            get { return (DataTemplate)GetValue(RouteItemTemplateProperty); }
            set { SetValue(RouteItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected route item template.
        /// </summary>
        /// <value>The selected route item template value.</value>
        public DataTemplate SelectedRouteItemTemplate
        {
            get { return (DataTemplate)GetValue(SelectedRouteItemTemplateProperty); }
            set { SetValue(SelectedRouteItemTemplateProperty, value); }
        }
        #endregion

        #region Brand Members
        /// <summary>
        /// Gets or sets the valid brands items source.
        /// </summary>
        /// <value>The valid brands items source.</value>
        public IEnumerable ValidBrandsItemsSource
        {
            get { return (IEnumerable)GetValue(ValidBrandsItemsSourceProperty); }
            set { SetValue(ValidBrandsItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected brand.
        /// </summary>
        /// <value>The selected brand.</value>
        public object SelectedBrand
        {
            get { return (object)GetValue(SelectedBrandProperty); }
            set { SetValue(SelectedBrandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the brand item template.
        /// </summary>
        /// <value>The brand item template.</value>
        public DataTemplate BrandItemTemplate
        {
            get { return (DataTemplate)GetValue(BrandItemTemplateProperty); }
            set { SetValue(BrandItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the brand it mandatory.
        /// </summary>
        /// <value>Whether the brand is mandatory.</value>
        public bool IsBrandMandatory
        {
            get { return (bool)GetValue(IsBrandMandatoryProperty); }
            set { SetValue(IsBrandMandatoryProperty, value); }
        }
        #endregion

        #region Form Members
        /// <summary>
        /// Gets or sets the concise forms items source.
        /// </summary>
        /// <value>The concise forms items source value.</value>
        public IEnumerable ConciseFormsItemsSource
        {
            get { return (IEnumerable)GetValue(ConciseFormsItemsSourceProperty); }
            set { SetValue(ConciseFormsItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the valid forms items source.
        /// </summary>
        /// <value>The valid forms items source value.</value>
        public IEnumerable ValidFormsItemsSource
        {
            get { return (IEnumerable)GetValue(ValidFormsItemsSourceProperty); }
            set { SetValue(ValidFormsItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the valid forms items header.
        /// </summary>
        /// <value>The valid forms items header value.</value>
        public object ValidFormsItemsHeader
        {
            get { return (object)GetValue(ValidFormsItemsHeaderProperty); }
            set { SetValue(ValidFormsItemsHeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the other forms items source.
        /// </summary>
        /// <value>The other forms items source.</value>
        public IEnumerable OtherFormsItemsSource
        {
            get { return (IEnumerable)GetValue(OtherFormsItemsSourceProperty); }
            set { SetValue(OtherFormsItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the other forms items header.
        /// </summary>
        /// <value>The other forms items header.</value>
        public object OtherFormsItemsHeader
        {
            get { return (object)GetValue(OtherFormsItemsHeaderProperty); }
            set { SetValue(OtherFormsItemsHeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected form.
        /// </summary>
        /// <value>The selected form value.</value>
        public object SelectedForm
        {
            get { return (object)GetValue(SelectedFormProperty); }
            set { SetValue(SelectedFormProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether form is mandatory.
        /// </summary>
        /// <value>The is form mandatory value.</value>
        public bool IsFormMandatory
        {
            get { return (bool)GetValue(IsFormMandatoryProperty); }
            set { SetValue(IsFormMandatoryProperty, value); }
        }

        /// <summary>
        /// Gets or sets the form item template.
        /// </summary>
        /// <value>The form item template value.</value>
        public DataTemplate FormItemTemplate
        {
            get { return (DataTemplate)GetValue(FormItemTemplateProperty); }
            set { SetValue(FormItemTemplateProperty, value); }
        }
        #endregion

        #region Strength Members
        /// <summary>
        /// Gets or sets the valid strengths items source.
        /// </summary>
        /// <value>The valid strengths items source value.</value>
        public IEnumerable ValidStrengthsItemsSource
        {
            get { return (IEnumerable)GetValue(ValidStrengthsItemsSourceProperty); }
            set { SetValue(ValidStrengthsItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the valid strengths items header.
        /// </summary>
        /// <value>The valid strengths items header.</value>
        public object ValidStrengthsItemsHeader
        {
            get { return (object)GetValue(ValidStrengthsItemsHeaderProperty); }
            set { SetValue(ValidStrengthsItemsHeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the other strengths items source.
        /// </summary>
        /// <value>The other strengths items source value.</value>
        public IEnumerable OtherStrengthsItemsSource
        {
            get { return (IEnumerable)GetValue(OtherStrengthsItemsSourceProperty); }
            set { SetValue(OtherStrengthsItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the other strengths items header.
        /// </summary>
        /// <value>The other strengths items header.</value>
        public object OtherStrengthsItemsHeader
        {
            get { return (object)GetValue(OtherStrengthsItemsHeaderProperty); }
            set { SetValue(OtherStrengthsItemsHeaderProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected strength.
        /// </summary>
        /// <value>The selected strength value.</value>
        public object SelectedStrength
        {
            get { return (object)GetValue(SelectedStrengthProperty); }
            set { SetValue(SelectedStrengthProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the strength is mandatory.
        /// </summary>
        /// <value>Ths is strength mandatory value.</value>
        public bool IsStrengthMandatory
        {
            get { return (bool)GetValue(IsStrengthMandatoryProperty); }
            set { SetValue(IsStrengthMandatoryProperty, value); }
        }

        /// <summary>
        /// Gets or sets the strength item template.
        /// </summary>
        /// <value>The strength item template value.</value>
        public DataTemplate StrengthItemTemplate
        {
            get { return (DataTemplate)GetValue(StrengthItemTemplateProperty); }
            set { SetValue(StrengthItemTemplateProperty, value); }
        }
        #endregion

        #region Dose Members
        /// <summary>
        /// Gets or sets the concise dosages items source.
        /// </summary>
        /// <value>The concise dosages items source.</value>
        public IEnumerable ConciseDosagesItemsSource
        {
            get { return (IEnumerable)GetValue(ConciseDosagesItemsSourceProperty); }
            set { SetValue(ConciseDosagesItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the detailed dosages items source.
        /// </summary>
        /// <value>The detailed dosages items source.</value>
        public IEnumerable DetailedDosagesItemsSource
        {
            get { return (IEnumerable)GetValue(DetailedDosagesItemsSourceProperty); }
            set { SetValue(DetailedDosagesItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected dose.
        /// </summary>
        /// <value>The selected dose value.</value>
        public object SelectedDose
        {
            get { return (object)GetValue(SelectedDoseProperty); }
            set { SetValue(SelectedDoseProperty, value); }
        }

        /// <summary>
        /// Gets or sets the dose item template.
        /// </summary>
        /// <value>The dose item template value.</value>
        public DataTemplate DoseItemTemplate
        {
            get { return (DataTemplate)GetValue(DoseItemTemplateProperty); }
            set { SetValue(DoseItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the custom dose item.
        /// </summary>
        /// <value>The custom dose item value.</value>
        public object CustomDoseItem
        {
            get { return (object)GetValue(CustomDoseItemProperty); }
            set { SetValue(CustomDoseItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the custom dose item template.
        /// </summary>
        /// <value>The custom dose item template value.</value>
        public DataTemplate CustomDoseItemTemplate
        {
            get { return (DataTemplate)GetValue(CustomDoseItemTemplateProperty); }
            set { SetValue(CustomDoseItemTemplateProperty, value); }
        }
        #endregion

        #region Method Members
        /// <summary>
        /// Gets or sets the selected method.
        /// </summary>
        /// <value>The selected method value.</value>
        public object SelectedMethod
        {
            get { return (object)GetValue(SelectedMethodProperty); }
            set { SetValue(SelectedMethodProperty, value); }
        }
        #endregion

        #region Site Members
        /// <summary>
        /// Gets or sets the custom site item.
        /// </summary>
        /// <value>The custom site item.</value>
        public object CustomSiteItem
        {
            get { return (object)GetValue(CustomSiteItemProperty); }
            set { SetValue(CustomSiteItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the custom site item template.
        /// </summary>
        /// <value>The custom site item tempalte.</value>
        public DataTemplate CustomSiteItemTemplate
        {
            get { return (DataTemplate)GetValue(CustomSiteItemTemplateProperty); }
            set { SetValue(CustomSiteItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected site.
        /// </summary>
        /// <value>The selected site value.</value>
        public object SelectedSite
        {
            get { return (object)GetValue(SelectedSiteProperty); }
            set { SetValue(SelectedSiteProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the site is mandatory.
        /// </summary>
        /// <value>Whether the site is mandatory.</value>
        public bool IsSiteMandatory
        {
            get { return (bool)GetValue(IsSiteMandatoryProperty); }
            set { SetValue(IsSiteMandatoryProperty, value); }
        }
        #endregion

        #region Frequency Members
        /// <summary>
        /// Gets or sets the concise frequencies items source.
        /// </summary>
        /// <value>The concise frequencies items source.</value>
        public IEnumerable ConciseFrequenciesItemsSource
        {
            get { return (IEnumerable)GetValue(ConciseFrequenciesItemsSourceProperty); }
            set { SetValue(ConciseFrequenciesItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the detailed frequencies items source.
        /// </summary>
        /// <value>The detailed frequencies items source.</value>
        public IEnumerable DetailedFrequenciesItemsSource
        {
            get { return (IEnumerable)GetValue(DetailedFrequenciesItemsSourceProperty); }
            set { SetValue(DetailedFrequenciesItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected frequency.
        /// </summary>
        /// <value>The selected frequency value.</value>
        public object SelectedFrequency
        {
            get { return (object)GetValue(SelectedFrequencyProperty); }
            set { SetValue(SelectedFrequencyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the frequency item template.
        /// </summary>
        /// <value>The frequency item template value.</value>
        public DataTemplate FrequencyItemTemplate
        {
            get { return (DataTemplate)GetValue(FrequencyItemTemplateProperty); }
            set { SetValue(FrequencyItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the custom frequency item.
        /// </summary>
        /// <value>The custom frequency value item.</value>
        public object CustomFrequencyItem
        {
            get { return (object)GetValue(CustomFrequencyItemProperty); }
            set { SetValue(CustomFrequencyItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the custom frequency item template.
        /// </summary>
        /// <value>The custom frequency item template.</value>
        public DataTemplate CustomFrequencyItemTemplate
        {
            get { return (DataTemplate)GetValue(CustomFrequencyItemTemplateProperty); }
            set { SetValue(CustomFrequencyItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the prescription is 'as required'.
        /// </summary>
        /// <value>The is as required value.</value>
        public bool IsAsRequired
        {
            get { return (bool)GetValue(IsAsRequiredProperty); }
            set { SetValue(IsAsRequiredProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the prescription is once only.
        /// </summary>
        /// <value>Whether the prescription is once only.</value>
        public bool IsOnceOnly
        {
            get { return (bool)GetValue(IsOnceOnlyProperty); }
            set { SetValue(IsOnceOnlyProperty, value); }
        }
        #endregion

        #region Template Prescription Members
        /// <summary>
        /// Gets or sets the valid template prescriptions items source.
        /// </summary>
        /// <value>The valid template prescription items source value.</value>
        public IEnumerable ValidTemplatePrescriptionsItemsSource
        {
            get { return (IEnumerable)GetValue(ValidTemplatePrescriptionsItemsSourceProperty); }
            set { SetValue(ValidTemplatePrescriptionsItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected template prescription.
        /// </summary>
        /// <value>The selected template prescription value.</value>
        public object SelectedTemplatePrescription
        {
            get { return (object)GetValue(SelectedTemplatePrescriptionProperty); }
            set { SetValue(SelectedTemplatePrescriptionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the template prescription item template.
        /// </summary>
        /// <value>The template prescription item template value.</value>
        public DataTemplate TemplatePrescriptionItemTemplate
        {
            get { return (DataTemplate)GetValue(TemplatePrescriptionItemTemplateProperty); }
            set { SetValue(TemplatePrescriptionItemTemplateProperty, value); }
        }
        #endregion

        #region Starting Condition Members
        /// <summary>
        /// Gets or sets the valid starting conditions items source.
        /// </summary>
        /// <value>The valid starting conditions value.</value>
        public IEnumerable ValidStartingConditionsItemsSource
        {
            get { return (IEnumerable)GetValue(ValidStartingConditionsItemsSourceProperty); }
            set { SetValue(ValidStartingConditionsItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected starting condition.
        /// </summary>
        /// <value>The selected starting condition.</value>
        public object SelectedStartingCondition
        {
            get { return (object)GetValue(SelectedStartingConditionProperty); }
            set { SetValue(SelectedStartingConditionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the starting condition item template.
        /// </summary>
        /// <value>The starting condition item template.</value>
        public DataTemplate StartingConditionItemTemplate
        {
            get { return (DataTemplate)GetValue(StartingConditionItemTemplateProperty); }
            set { SetValue(StartingConditionItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the custom starting condition item.
        /// </summary>
        /// <value>The custom start condition item.</value>
        public object CustomStartingConditionItem
        {
            get { return (object)GetValue(CustomStartingConditionItemProperty); }
            set { SetValue(CustomStartingConditionItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the custom starting condition item template.
        /// </summary>
        /// <value>The custom starting condition item template.</value>
        public DataTemplate CustomStartingConditionItemTemplate
        {
            get { return (DataTemplate)GetValue(CustomStartingConditionItemTemplateProperty); }
            set { SetValue(CustomStartingConditionItemTemplateProperty, value); }
        }
        #endregion

        #region AdministrationTimes Members
        /// <summary>
        /// Gets or sets the valid administration times items source.
        /// </summary>
        /// <value>The valid administration times items source value.</value>
        public IEnumerable ValidAdministrationTimesItemsSource
        {
            get { return (IEnumerable)GetValue(ValidAdministrationTimesItemsSourceProperty); }
            set { SetValue(ValidAdministrationTimesItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected administration times.
        /// </summary>
        /// <value>The selected administration times value.</value>
        public object SelectedAdministrationTimes
        {
            get { return (object)GetValue(SelectedAdministrationTimesProperty); }
            set { SetValue(SelectedAdministrationTimesProperty, value); }
        }

        /// <summary>
        /// Gets or sets the administration times item template.
        /// </summary>
        /// <value>The administration times items template value.</value>
        public DataTemplate AdministrationTimesItemTemplate
        {
            get { return (DataTemplate)GetValue(AdministrationTimesItemTemplateProperty); }
            set { SetValue(AdministrationTimesItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the administration times label.
        /// </summary>
        /// <value>The administration times label.</value>
        public string AdministrationTimesLabel
        {
            get { return (string)GetValue(AdministrationTimesLabelProperty); }
            set { SetValue(AdministrationTimesLabelProperty, value); }
        }
        #endregion

        #region StartDate Members
        /// <summary>
        /// Gets or sets the selected start date.
        /// </summary>
        /// <value>The selected start date value.</value>
        public DateTime? SelectedStartDate
        {
            get { return (DateTime?)GetValue(SelectedStartDateProperty); }
            set { SetValue(SelectedStartDateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the starting label.
        /// </summary>
        /// <value>The starting label value.</value>
        public object StartingLabel
        {
            get { return (object)GetValue(StartingLabelProperty); }
            set { SetValue(StartingLabelProperty, value); }
        }
        #endregion

        #region FirstDoseTime Members
        /// <summary>
        /// Gets or sets the valid first dose times items source.
        /// </summary>
        /// <value>The valid first dose times item source value.</value>
        public IEnumerable ValidFirstDoseTimesItemsSource
        {
            get { return (IEnumerable)GetValue(ValidFirstDoseTimesItemsSourceProperty); }
            set { SetValue(ValidFirstDoseTimesItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected first dose time.
        /// </summary>
        /// <value>The selected first dose time value.</value>
        public object SelectedFirstDoseTime
        {
            get { return (object)GetValue(SelectedFirstDoseTimeProperty); }
            set { SetValue(SelectedFirstDoseTimeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the first dose time item template.
        /// </summary>
        /// <value>The first dose time item template value.</value>
        public DataTemplate FirstDoseTimeItemTemplate
        {
            get { return (DataTemplate)GetValue(FirstDoseTimeItemTemplateProperty); }
            set { SetValue(FirstDoseTimeItemTemplateProperty, value); }
        }
        #endregion

        #region Duration Members
        /// <summary>
        /// Gets or sets the valid durations items source.
        /// </summary>
        /// <value>The valid durations items source value.</value>
        public IEnumerable ValidDurationsItemsSource
        {
            get { return (IEnumerable)GetValue(ValidDurationsItemsSourceProperty); }
            set { SetValue(ValidDurationsItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected duration.
        /// </summary>
        /// <value>The selected duration value.</value>
        public object SelectedDuration
        {
            get { return (object)GetValue(SelectedDurationProperty); }
            set { SetValue(SelectedDurationProperty, value); }
        }

        /// <summary>
        /// Gets or sets the duration item template.
        /// </summary>
        /// <value>The duration item template value.</value>
        public DataTemplate DurationItemTemplate
        {
            get { return (DataTemplate)GetValue(DurationItemTemplateProperty); }
            set { SetValue(DurationItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the custom duration item.
        /// </summary>
        /// <value>The custom duration item value.</value>
        public object CustomDurationItem
        {
            get { return (object)GetValue(CustomDurationItemProperty); }
            set { SetValue(CustomDurationItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the custom duration item template.
        /// </summary>
        /// <value>The custom duration item template value.</value>
        public DataTemplate CustomDurationItemTemplate
        {
            get { return (DataTemplate)GetValue(CustomDurationItemTemplateProperty); }
            set { SetValue(CustomDurationItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the continuing label.
        /// </summary>
        /// <value>The continuing label value.</value>
        public object ContinuingLabel
        {
            get { return (object)GetValue(ContinuingLabelProperty); }
            set { SetValue(ContinuingLabelProperty, value); }
        }

        #endregion

        #region Reason For Prescribing Members
        /// <summary>
        /// Gets or sets the custom reason for prescribing item.
        /// </summary>
        /// <value>The reason for prescribing item.</value>
        public object CustomReasonForPrescribingItem
        {
            get { return (object)GetValue(CustomReasonForPrescribingItemProperty); }
            set { SetValue(CustomReasonForPrescribingItemProperty, value); }
        }

        /// <summary>
        /// Gets or sets the custom reason for prescribing item template.
        /// </summary>
        /// <value>The custom reason for prescribing item template.</value>
        public DataTemplate CustomReasonForPrescribingItemTemplate
        {
            get { return (DataTemplate)GetValue(CustomReasonForPrescribingItemTemplateProperty); }
            set { SetValue(CustomReasonForPrescribingItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected reason for prescribing.
        /// </summary>
        /// <value>The selected reason for prescribing.</value>
        public object SelectedReasonForPrescribing
        {
            get { return (object)GetValue(SelectedReasonForPrescribingProperty); }
            set { SetValue(SelectedReasonForPrescribingProperty, value); }
        }
        #endregion

        #region Prescription Members
        /// <summary>
        /// Gets or sets a value indicating whether this prescription is unlicensed.
        /// </summary>
        /// <value>Whether this prescription is unlicensed.</value>
        public bool IsUnlicensed
        {
            get { return (bool)GetValue(IsUnlicensedProperty); }
            set { SetValue(IsUnlicensedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the unlicensed reason.
        /// </summary>
        /// <value>The unlicensed reason value.</value>
        public object UnlicensedReason
        {
            get { return (object)GetValue(UnlicensedReasonProperty); }
            set { SetValue(UnlicensedReasonProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the prescription is authorizable.
        /// </summary>
        /// <value>The is authorizable value.</value>
        public bool IsAuthorizable
        {
            get { return (bool)GetValue(IsAuthorizableProperty); }
            set { SetValue(IsAuthorizableProperty, value); }
        }
        #endregion

        #region Overridable Virtual Methods
        /// <summary>
        /// Overridable method for OnDrugItemsSourceChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnDrugItemsSourceChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnTemplatePrescriptionsItemsSourceChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnTemplatePrescriptionsItemsSourceChanged(DependencyPropertyChangedEventArgs args)
        {            
        }

        /// <summary>
        /// Overridable method for OnSelectedDrugChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedDrugChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnSelectedRouteChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedRouteChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnSelectedBrandChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedBrandChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnSelectedFormChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedFormChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnSelectedStrengthChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedStrengthChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnSelectedDoseChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedDoseChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnSelectedFrequencyChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedFrequencyChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnIsAsRequiredChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnIsAsRequiredChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnIsOnceOnlyChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnIsOnceOnlyChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnSelectedAdministrationTimesChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedAdministrationTimesChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnSelectedStartDateChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedStartDateChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnSelectedTemplatePrescriptionChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedTemplatePrescriptionChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnSelectedStartingConditionChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedStartingConditionChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnSelectedFirstDoseTimeChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedFirstDoseTimeChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnSelectedDurationChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedDurationChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnSelectedReasonForPrescribingChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedReasonForPrescribingChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnIsUnlicensedChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnIsUnlicensedChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnIsAuthorizableChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnIsAuthorizableChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnSelectedMethodChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedMethodChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnSelectedSiteChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnSelectedSiteChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        /// <summary>
        /// Overridable method for OnUnlicensedReasonChanged.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected virtual void OnUnlicensedReasonChanged(DependencyPropertyChangedEventArgs args)
        {
        }
        #endregion

        #region Dependency Property Change Callbacks
        /// <summary>
        /// Updates the drugs items source.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void DrugsItemsSource_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnDrugItemsSourceChanged(args);
        }

        /// <summary>
        /// Updates the template prescriptions items source.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void TemplatePrescriptionsItemsSource_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnTemplatePrescriptionsItemsSourceChanged(args);
        }

        /// <summary>
        /// Updates the selected drug.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedDrug_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedDrugChanged(args);

            if (searchAndPrescribeBase.SelectedDrugChanged != null)
            {
                searchAndPrescribeBase.SelectedDrugChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected drug item template.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void DrugItemTemplate_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            if (searchAndPrescribeBase.SelectedRouteItemTemplate == null)
            {
                searchAndPrescribeBase.SelectedDrugItemTemplate = searchAndPrescribeBase.DrugItemTemplate;
            }
        }

        /// <summary>
        /// Updates the selected route.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedRoute_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedRouteChanged(args);

            if (searchAndPrescribeBase.SelectedRouteChanged != null)
            {
                searchAndPrescribeBase.SelectedRouteChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected brand.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedBrand_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedBrandChanged(args);

            if (searchAndPrescribeBase.SelectedBrandChanged != null)
            {
                searchAndPrescribeBase.SelectedBrandChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected form.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedForm_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedFormChanged(args);

            if (searchAndPrescribeBase.SelectedFormChanged != null)
            {
                searchAndPrescribeBase.SelectedFormChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected strength.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedStrength_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedStrengthChanged(args);

            if (searchAndPrescribeBase.SelectedStrengthChanged != null)
            {
                searchAndPrescribeBase.SelectedStrengthChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected dose.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedDose_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedDoseChanged(args);

            if (searchAndPrescribeBase.SelectedDoseChanged != null)
            {
                searchAndPrescribeBase.SelectedDoseChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected frequency.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedFrequency_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedFrequencyChanged(args);

            if (searchAndPrescribeBase.SelectedFrequencyChanged != null)
            {
                searchAndPrescribeBase.SelectedFrequencyChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the as required flag.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void IsAsRequired_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnIsAsRequiredChanged(args);

            if (searchAndPrescribeBase.IsAsRequiredChanged != null)
            {
                searchAndPrescribeBase.IsAsRequiredChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the in only flag.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void IsOnceOnly_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnIsOnceOnlyChanged(args);

            if (searchAndPrescribeBase.IsOnceOnlyChanged != null)
            {
                searchAndPrescribeBase.IsOnceOnlyChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected template prescription.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedTemplatePrescription_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedTemplatePrescriptionChanged(args);

            if (searchAndPrescribeBase.SelectedTemplatePrescriptionChanged != null)
            {
                searchAndPrescribeBase.SelectedTemplatePrescriptionChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected template prescription.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedStartingCondition_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedStartingConditionChanged(args);

            if (searchAndPrescribeBase.SelectedStartingConditionChanged != null)
            {
                searchAndPrescribeBase.SelectedStartingConditionChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected administration times.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedAdministrationTimes_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedAdministrationTimesChanged(args);

            if (searchAndPrescribeBase.SelectedAdministrationTimesChanged != null)
            {
                searchAndPrescribeBase.SelectedAdministrationTimesChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected start date.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedStartDate_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedStartDateChanged(args);

            if (searchAndPrescribeBase.SelectedStartDateChanged != null)
            {
                searchAndPrescribeBase.SelectedStartDateChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected first dose time.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedFirstDoseTime_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedFirstDoseTimeChanged(args);

            if (searchAndPrescribeBase.SelectedFirstDoseTimeChanged != null)
            {
                searchAndPrescribeBase.SelectedFirstDoseTimeChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected duration.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedDuration_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedDurationChanged(args);

            if (searchAndPrescribeBase.SelectedDurationChanged != null)
            {
                searchAndPrescribeBase.SelectedDurationChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected reason for prescribing.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedReasonForPrescribing_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedReasonForPrescribingChanged(args);

            if (searchAndPrescribeBase.SelectedReasonForPrescribingChanged != null)
            {
                searchAndPrescribeBase.SelectedReasonForPrescribingChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected route item template.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void RouteItemTemplate_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            if (searchAndPrescribeBase.SelectedRouteItemTemplate == null)
            {
                searchAndPrescribeBase.SelectedRouteItemTemplate = searchAndPrescribeBase.RouteItemTemplate;
            }
        }

        /// <summary>
        /// Updates the is unlicensed value.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void IsUnlicensed_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnIsUnlicensedChanged(args);

            if (searchAndPrescribeBase.IsUnlicensedChanged != null)
            {
                searchAndPrescribeBase.IsUnlicensedChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the is unlicensed value.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void IsAuthorizable_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnIsAuthorizableChanged(args);

            if (searchAndPrescribeBase.IsAuthorizableChanged != null)
            {
                searchAndPrescribeBase.IsAuthorizableChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected method.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedMethod_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedMethodChanged(args);

            if (searchAndPrescribeBase.SelectedMethodChanged != null)
            {
                searchAndPrescribeBase.SelectedMethodChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected site.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void SelectedSite_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnSelectedSiteChanged(args);

            if (searchAndPrescribeBase.SelectedSiteChanged != null)
            {
                searchAndPrescribeBase.SelectedSiteChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates the selected site.
        /// </summary>
        /// <param name="obj">The Search and Prescribe Control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void UnlicensedReason_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeBase searchAndPrescribeBase = obj as SearchAndPrescribeBase;
            searchAndPrescribeBase.OnUnlicensedReasonChanged(args);

            if (searchAndPrescribeBase.UnlicensedReasonChanged != null)
            {
                searchAndPrescribeBase.UnlicensedReasonChanged(searchAndPrescribeBase, EventArgs.Empty);
            }
        }
        #endregion
    }
}
