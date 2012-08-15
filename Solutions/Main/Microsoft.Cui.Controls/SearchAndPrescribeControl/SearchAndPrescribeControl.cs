//-----------------------------------------------------------------------
// <copyright file="SearchAndPrescribeControl.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      The search and prescribe control.
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
    using System.Collections.Generic;
    using System.Collections;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows.Controls.Primitives;
    using System.Windows.Markup;

    /// <summary>
    /// The search and prescribe control.
    /// </summary>
    [TemplatePart(Name = SearchAndPrescribeControl.ElementRootBorder, Type = typeof(Border))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementBackgroundElement, Type = typeof(UIElement))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementCascadingListBoxGroup, Type = typeof(CascadingListBoxGroup))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementConciseDrugsListContainer, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDrugSearchTextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDrugSearchButton, Type = typeof(Button))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementShowAllDrugsButton, Type = typeof(Button))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementConciseDrugsList, Type = typeof(CascadingListBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementConciseRoutesList, Type = typeof(CascadingListBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementConciseFormsList, Type = typeof(CascadingListBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementConciseStrengthsList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementConciseDosagesList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementConciseFrequenciesList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementConciseBrandsList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementConciseTemplatePrescriptionsList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementConciseStartingConditionsList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementConciseAdministrationTimesList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementConciseDurationsList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementConciseDetailedViewToggleButton, Type = typeof(ToggleButton))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedDrugRouteList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementUnlicensedReasonTextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedReasonForPrescribingOptionalFieldContainer, Type = typeof(OptionalFieldContainer))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedReasonForPrescribingList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementScrollViewer, Type = typeof(ScrollViewer))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedStrengthsOptionalFieldContainer, Type = typeof(OptionalFieldContainer))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedStrengthsList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedFormsOptionalFieldContainer, Type = typeof(OptionalFieldContainer))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedFormsList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedSiteOptionalFieldContainer, Type = typeof(OptionalFieldContainer))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedSitesList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedDosagesList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedMethodTextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedFrequenciesList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedAsRequiredOptionFieldContainer, Type = typeof(OptionalFieldContainer))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedStartingConditionsList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedAdministrationTimesList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementDetailedDurationsList, Type = typeof(SplitComboBox))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementPreviewButton, Type = typeof(Button))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementAuthorizeButton, Type = typeof(Button))]
    [TemplatePart(Name = SearchAndPrescribeControl.ElementCloseButton, Type = typeof(Button))]
    public class SearchAndPrescribeControl : SearchAndPrescribeBase
    {
        #region Dependency Properties
        /// <summary>
        /// The IsShowingAllDrugResults Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsShowingAllDrugResultsProperty =
            DependencyProperty.Register("IsShowingAllDrugResults", typeof(bool), typeof(SearchAndPrescribeControl), new PropertyMetadata(true, new PropertyChangedCallback(IsShowingAllDrugResults_Changed)));

        /// <summary>
        /// The IsDetailedView Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsDetailedViewProperty =
            DependencyProperty.Register("IsDetailedView", typeof(bool), typeof(SearchAndPrescribeControl), new PropertyMetadata(false, new PropertyChangedCallback(IsDetailedView_Changed)));

        /// <summary>
        /// The IsConciseDetailedViewToggleEnabled Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsConciseDetailedViewToggleEnabledProperty =
            DependencyProperty.Register("IsConciseDetailedViewToggleEnabled", typeof(bool), typeof(SearchAndPrescribeControl), new PropertyMetadata(false));

        /// <summary>
        /// The DrugsListCollapsedItemCount Dependency Property.
        /// </summary>
        public static readonly DependencyProperty DrugsListCollapsedItemCountProperty =
            DependencyProperty.Register("DrugsListCollapsedItemCount", typeof(int), typeof(SearchAndPrescribeControl), new PropertyMetadata(10));

        /// <summary>
        /// The ShowAllDrugsText Dependency Property.
        /// </summary>
        public static readonly DependencyProperty ShowAllDrugsTextProperty =
            DependencyProperty.Register("ShowAllDrugsText", typeof(string), typeof(SearchAndPrescribeControl), null);

        /// <summary>
        /// The IsDrugSearchButtonEnabled Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsDrugSearchButtonEnabledProperty =
            DependencyProperty.Register("IsDrugSearchButtonEnabled", typeof(bool), typeof(SearchAndPrescribeControl), new PropertyMetadata(false));

        /// <summary>
        /// The DrugsResultsListMessage Dependency Property.
        /// </summary>
        public static readonly DependencyProperty DrugsResultsListMessageProperty =
            DependencyProperty.Register("DrugsResultsListMessage", typeof(object), typeof(SearchAndPrescribeControl), null);

        /// <summary>
        /// The DrugsResultsListMessageTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty DrugsResultsListMessageTemplateProperty =
            DependencyProperty.Register("DrugsResultsListMessageTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeControl), null);

        /// <summary>
        /// The CascadingListMaximumHeight Dependency Property.
        /// </summary>
        public static readonly DependencyProperty CascadingListMaximumHeightProperty =
            DependencyProperty.Register("CascadingListMaximumHeight", typeof(double), typeof(SearchAndPrescribeControl), new PropertyMetadata(0d));

        /// <summary>
        /// The BasicDrugTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty BasicDrugTemplateProperty =
            DependencyProperty.Register("BasicDrugTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeControl), null);

        /// <summary>
        /// The BasicRouteTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty BasicRouteTemplateProperty =
            DependencyProperty.Register("BasicRouteTemplate", typeof(DataTemplate), typeof(SearchAndPrescribeControl), null);
        
        /// <summary>
        /// The DrugSearchText Dependency Property.
        /// </summary>
        public static readonly DependencyProperty DrugSearchTextProperty =
            DependencyProperty.Register("DrugSearchText", typeof(string), typeof(SearchAndPrescribeControl), new PropertyMetadata(string.Empty, new PropertyChangedCallback(DrugSearchText_Changed)));

        /// <summary>
        /// The DetailedViewMaxHeight Dependency Property.
        /// </summary>
        public static readonly DependencyProperty DetailedViewMaxHeightProperty =
            DependencyProperty.Register("DetailedViewMaxHeight", typeof(double), typeof(SearchAndPrescribeControl), new PropertyMetadata(double.PositiveInfinity));

        #endregion

        #region Template Part Names
        /// <summary>
        /// The RootBorder Element Name.
        /// </summary>
        private const string ElementRootBorder = "RootBorder";

        /// <summary>
        /// The BackgroundElement Element Name.
        /// </summary>
        private const string ElementBackgroundElement = "BackgroundElement";

        /// <summary>
        /// The CascadingListBoxGroup Element Name.
        /// </summary>
        private const string ElementCascadingListBoxGroup = "CascadingListBoxGroup";

        /// <summary>
        /// The ConciseDrugsListContainer Element Name.
        /// </summary>
        private const string ElementConciseDrugsListContainer = "ConciseDrugsListContainer";

        /// <summary>
        /// The DrugSearchTextBox Element Name.
        /// </summary>
        private const string ElementDrugSearchTextBox = "DrugSearchTextBox";

        /// <summary>
        /// The DrugSearchButton Element Name.
        /// </summary>
        private const string ElementDrugSearchButton = "DrugSearchButton";

        /// <summary>
        /// The ShowAllDrugsButton Element Name.
        /// </summary>
        private const string ElementShowAllDrugsButton = "ShowAllDrugsButton";

        /// <summary>
        /// The ConciseDrugsList Element Name.
        /// </summary>
        private const string ElementConciseDrugsList = "ConciseDrugsList";

        /// <summary>
        /// The ConciseRoutesList Element Name.
        /// </summary>
        private const string ElementConciseRoutesList = "ConciseRoutesList";

        /// <summary>
        /// The ConciseFormsList Element Name.
        /// </summary>
        private const string ElementConciseFormsList = "ConciseFormsList";

        /// <summary>
        /// The ConciseStrengthsList Element Name.
        /// </summary>
        private const string ElementConciseStrengthsList = "ConciseStrengthsList";

        /// <summary>
        /// The ConciseDosagesList Element Name.
        /// </summary>
        private const string ElementConciseDosagesList = "ConciseDosagesList";

        /// <summary>
        /// The ConciseFrequenciesList Element Name.
        /// </summary>
        private const string ElementConciseFrequenciesList = "ConciseFrequenciesList";

        /// <summary>
        /// The ConciseBrandsList Element Name.
        /// </summary>
        private const string ElementConciseBrandsList = "ConciseBrandsList";

        /// <summary>
        /// The ConciseTemplatePrescriptionsList Element Name.
        /// </summary>
        private const string ElementConciseTemplatePrescriptionsList = "ConciseTemplatePrescriptionsList";

        /// <summary>
        /// The ConciseStartingConditionsList Element Name.
        /// </summary>
        private const string ElementConciseStartingConditionsList = "ConciseStartingConditionsList";

        /// <summary>
        /// The ConciseAdministrationTimesList Element Name.
        /// </summary>
        private const string ElementConciseAdministrationTimesList = "ConciseAdministrationTimesList";

        /// <summary>
        /// The ConciseDurationsList Element Name.
        /// </summary>
        private const string ElementConciseDurationsList = "ConciseDurationsList";

        /// <summary>
        /// The ConciseDetailedViewToggleButton Element Name.
        /// </summary>
        private const string ElementConciseDetailedViewToggleButton = "ConciseDetailedViewToggleButton";

        /// <summary>
        /// The DetailedDrugRouteList Element Name.
        /// </summary>
        private const string ElementDetailedDrugRouteList = "DetailedDrugRouteList";

        /// <summary>
        /// The UnlicensedReasonTextBox Element Name.
        /// </summary>
        private const string ElementUnlicensedReasonTextBox = "UnlicensedReasonTextBox";

        /// <summary>
        /// The DetailedReasonForPrescribingOptionalFieldContainer Element Name.
        /// </summary>        
        private const string ElementDetailedReasonForPrescribingOptionalFieldContainer = "DetailedReasonForPrescribingOptionalFieldContainer";

        /// <summary>
        /// The DetailedReasonForPrescribingList Element Name.
        /// </summary>
        private const string ElementDetailedReasonForPrescribingList = "DetailedReasonForPrescribingList";

        /// <summary>
        /// The ScrollViewer Element Name.
        /// </summary>
        private const string ElementScrollViewer = "ScrollViewer";

        /// <summary>
        /// The DetailedStrengthsList Element Name.
        /// </summary>
        private const string ElementDetailedStrengthsOptionalFieldContainer = "DetailedStrengthsOptionalFieldContainer";

        /// <summary>
        /// The DetailedStrengthsList Element Name.
        /// </summary>
        private const string ElementDetailedStrengthsList = "DetailedStrengthsList";

        /// <summary>
        /// The DetailedStrengthsList Element Name.
        /// </summary>
        private const string ElementDetailedFormsOptionalFieldContainer = "DetailedFormsOptionalFieldContainer";

        /// <summary>
        /// The DetailedFormsList Element Name.
        /// </summary>
        private const string ElementDetailedFormsList = "DetailedFormsList";

        /// <summary>
        /// The DetailedStrengthsList Element Name.
        /// </summary>        
        private const string ElementDetailedSiteOptionalFieldContainer = "DetailedSiteOptionalFieldContainer";

        /// <summary>
        /// The DetailedSitesList Element Name.
        /// </summary>
        private const string ElementDetailedSitesList = "DetailedSitesList";

        /// <summary>
        /// The DetailedDosagesList Element Name.
        /// </summary>
        private const string ElementDetailedDosagesList = "DetailedDosagesList";

        /// <summary>
        /// The DetailedMethodTextBox Element Name.
        /// </summary>
        private const string ElementDetailedMethodTextBox = "DetailedMethodTextBox";

        /// <summary>
        /// The DetailedFrequenciesList Element Name.
        /// </summary>
        private const string ElementDetailedFrequenciesList = "DetailedFrequenciesList";

        /// <summary>
        /// The DetailedSitesList Element Name.
        /// </summary>
        private const string ElementDetailedAsRequiredOptionFieldContainer = "DetailedAsRequiredOptionFieldContainer";

        /// <summary>
        /// The DetailedStartingConditionsList Element Name.
        /// </summary>
        private const string ElementDetailedStartingConditionsList = "DetailedStartingConditionsList";

        /// <summary>
        /// The DetailedAdministrationTimesList Element Name.
        /// </summary>
        private const string ElementDetailedAdministrationTimesList = "DetailedAdministrationTimesList";

        /// <summary>
        /// The DetailedDurationsList Element Name.
        /// </summary>
        private const string ElementDetailedDurationsList = "DetailedDurationsList";

        /// <summary>
        /// The PreviewButton Element Name.
        /// </summary>
        private const string ElementPreviewButton = "PreviewButton";

        /// <summary>
        /// The AuthorizeButton Element Name.
        /// </summary>
        private const string ElementAuthorizeButton = "AuthorizeButton";

        /// <summary>
        /// The CloseButton Element Name.
        /// </summary>
        private const string ElementCloseButton = "CloseButton";

        /// <summary>
        /// The TemplatePrescriptionPromptWatermarkTemplate Element Name.
        /// </summary>
        private const string ElementTemplatePrescriptionPromptWatermarkTemplate = "TemplatePrescriptionPromptWatermarkTemplate";

        /// <summary>
        /// The TemplatePrescriptionNoPromptWatermarkTemplate Element Name.
        /// </summary>
        private const string ElementTemplatePrescriptionNoPromptWatermarkTemplate = "TemplatePrescriptionNoPromptWatermarkTemplate";

        #endregion

        #region Private fields
        /// <summary>
        /// Stores the ConciseDrugsListContainer.
        /// </summary>
        private FrameworkElement conciseDrugsListContainer;

        /// <summary>
        /// Stores the cascading list box group.
        /// </summary>
        private CascadingListBoxGroup cascadingListBoxGroup;

        /// <summary>
        /// Stores the ConciseDrugsList.
        /// </summary>
        private CascadingListBox conciseDrugsList;

        /// <summary>
        /// Stores the ConciseRoutesList.
        /// </summary>
        private CascadingListBox conciseRoutesList;

        /// <summary>
        /// Stores the ConciseFormsList.
        /// </summary>
        private CascadingListBox conciseFormsList;

        /// <summary>
        /// Stores the ConciseStrengthsList.
        /// </summary>
        private SplitComboBox conciseStrengthsList;

        /// <summary>
        /// Stores the ConciseDosagesList.
        /// </summary>
        private SplitComboBox conciseDosagesList;

        /// <summary>
        /// Stores the ConciseFrequenciesList.
        /// </summary>
        private SplitComboBox conciseFrequenciesList;

        /// <summary>
        /// Stores the ConciseBrandsList.
        /// </summary>
        private SplitComboBox conciseBrandsList;

        /// <summary>
        /// Stores the ConciseTemplatePrescriptionsList.
        /// </summary>
        private SplitComboBox conciseTemplatePrescriptionsList;

        /// <summary>
        /// Stores the ConcisestartingConditionsList.
        /// </summary>
        private SplitComboBox conciseStartingConditionsList;

        /// <summary>
        /// Stores the ConciseDurationsList.
        /// </summary>
        private SplitComboBox conciseDurationsList;

        /// <summary>
        /// Stores the concise detailed view toggle button.
        /// </summary>
        private ToggleButton conciseDetailedViewToggleButton;

        /// <summary>
        /// Stores the DetailedDrugRouteList.
        /// </summary>
        private SplitComboBox detailedDrugRouteList;

        /// <summary>
        /// Stores the unlicensed reason text box.
        /// </summary>
        private TextBox unlicensedReasonTextBox;

        /// <summary>
        /// Stores the scroll viewer.
        /// </summary>
        private ScrollViewer scrollViewer;

        /// <summary>
        /// Stores the detailed strengths list.
        /// </summary>
        private SplitComboBox detailedStrengthsList;
        
        /// <summary>
        /// Stores the detailed forms list.
        /// </summary>
        private SplitComboBox detailedFormsList;
        
        /// <summary>
        /// Stores the detailed dosages list.
        /// </summary>
        private SplitComboBox detailedDosagesList;

        /// <summary>
        /// Stores the detailed method text box.
        /// </summary>
        private TextBox detailedMethodTextBox;

        /// <summary>
        /// Stores the detailed sites list.
        /// </summary>
        private SplitComboBox detailedSitesList;

        /// <summary>
        /// Stores the detailed frequencies list.
        /// </summary>
        private SplitComboBox detailedFrequenciesList;

        /// <summary>
        /// Stores the detailed starting conditions list.
        /// </summary>
        private SplitComboBox detailedStartingConditionsList;

        /// <summary>
        /// Stores the detailed durations list.
        /// </summary>
        private SplitComboBox detailedDurationsList;

        /// <summary>
        /// Stores the detailed reaons for prescribing optional field container.
        /// </summary>
        private OptionalFieldContainer detailedReasonForPrescribingOptionalFieldContainer;

        /// <summary>
        /// Stores the detailed strengths optional field container.
        /// </summary>
        private OptionalFieldContainer detailedStrengthsOptionalFieldContainer;

        /// <summary>
        /// Stores the detailed forms optional field container.
        /// </summary>
        private OptionalFieldContainer detailedFormsOptionalFieldContainer;

        /// <summary>
        /// Stores the detailed site optional field container.
        /// </summary>
        private OptionalFieldContainer detailedSiteOptionalFieldContainer;

        /// <summary>
        /// Stores the DrugSearchTextBox.
        /// </summary>
        private TextBox drugSearchTextBox;

        /// <summary>
        /// Stores the DrugSearchButton.
        /// </summary>
        private Button drugSearchButton;

        /// <summary>
        /// Stores the preview button.
        /// </summary>
        private Button previewButton;

        /// <summary>
        /// Stores the close button.
        /// </summary>
        private Button closeButton;

        /// <summary>
        /// Stores the ShowAllDrugsButton.
        /// </summary>
        private Button showAllDrugsButton;

        /// <summary>
        /// Stores if the detailed view is locked.
        /// </summary>
        private bool detailedViewLocked;

        /// <summary>
        /// Stores if a template prescription was selected.
        /// </summary>
        private bool templatePrescriptionSelected;

        /// <summary>
        /// Stores whether the focus drug search text box.
        /// </summary>
        private bool focusDrugSearchTextBoxOnLoad;

        /// <summary>
        /// Stores a list of expanded optional field containers.
        /// </summary>
        private List<OptionalFieldContainer> expandedOptionalFieldContainers = new List<OptionalFieldContainer>();

        /// <summary>
        /// Stores the template prescipription's list watermark template containing a prompt.
        /// </summary>
        private DataTemplate templatePrescriptionPromptWatermarkTemplate;

        /// <summary>
        /// Stores the template prescipription's list watermark template not containing a prompt.
        /// </summary>
        private DataTemplate templatePrescriptionNoPromptWatermarkTemplate;
        #endregion

        /// <summary>
        /// SearchAndPrescribeControl constructor.
        /// </summary>
        public SearchAndPrescribeControl()
        {
            this.DefaultStyleKey = typeof(SearchAndPrescribeControl);
            this.GotFocus += new RoutedEventHandler(this.SearchAndPrescribeControl_GotFocus);
        }

        #region Events
        /// <summary>
        /// The DrugSearchTextChanged Event.
        /// </summary>
        public event EventHandler DrugSearchTextChanged;

        /// <summary>
        /// The DrugSearchButtonClick Event.
        /// </summary>
        public event RoutedEventHandler DrugSearchButtonClick;

        /// <summary>
        /// The Preview Event.
        /// </summary>
        public event RoutedEventHandler Preview;

        /// <summary>
        /// The Authorize Event.
        /// </summary>
        public event RoutedEventHandler Authorize;

        /// <summary>
        /// The Clear Event.
        /// </summary>
        public event EventHandler Clear;

        /// <summary>
        /// The Close Event.
        /// </summary>
        public event RoutedEventHandler Close;

        /// <summary>
        /// The ConciseRoutesListOtherSelected Event.
        /// </summary>
        public event EventHandler ConciseRoutesListOtherSelected;

        /// <summary>
        /// The ConciseStrengthsListOtherSelected Event.
        /// </summary>
        public event EventHandler ConciseStrengthsListOtherSelected;
        
        /// <summary>
        /// The ConciseDosagesListOtherSelected Event.
        /// </summary>
        public event EventHandler ConciseDosagesListOtherSelected;

        /// <summary>
        /// The ConciseFrequenciesListOtherSelected Event.
        /// </summary>
        public event EventHandler ConciseFrequenciesListOtherSelected;

        /// <summary>
        /// The ConciseTemplatePrescriptionsListOtherSelected Event.
        /// </summary>
        public event EventHandler ConciseTemplatePrescriptionsListOtherSelected;

        /// <summary>
        /// The ConciseAdministrationTimesListOtherSelected Event.
        /// </summary>
        public event EventHandler ConciseAdministrationTimesListOtherSelected;

        /// <summary>
        /// The ConciseDurationsListOtherSelected Event.
        /// </summary>
        public event EventHandler ConciseDurationsListOtherSelected;

        /// <summary>
        /// The DetailedAdministrationTimesListOtherSelected Event.
        /// </summary>
        public event EventHandler DetailedAdministrationTimesListOtherSelected;
        #endregion

        #region Public members
        /// <summary>
        /// Gets or sets a value indicating whether all the drug search results are showing.
        /// </summary>
        /// <value>The is showing all drug results value.</value>
        public bool IsShowingAllDrugResults
        {
            get { return (bool)GetValue(IsShowingAllDrugResultsProperty); }
            set { SetValue(IsShowingAllDrugResultsProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the view is the detailed view.
        /// </summary>
        /// <value>The is detailed view value.</value>
        public bool IsDetailedView
        {
            get { return (bool)GetValue(IsDetailedViewProperty); }
            set { SetValue(IsDetailedViewProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the concise /detailed view toggle is enabled.
        /// </summary>
        /// <value>Whether the concise / detailed view toggle is enabled.</value>
        public bool IsConciseDetailedViewToggleEnabled
        {
            get { return (bool)GetValue(IsConciseDetailedViewToggleEnabledProperty); }
            set { SetValue(IsConciseDetailedViewToggleEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets the drug list collapsed item count.
        /// </summary>
        /// <value>The drug list collapsed item count value.</value>
        public int DrugsListCollapsedItemCount
        {
            get { return (int)GetValue(DrugsListCollapsedItemCountProperty); }
            set { SetValue(DrugsListCollapsedItemCountProperty, value); }
        }

        /// <summary>
        /// Gets the show all drugs text.
        /// </summary>
        /// <value>The show all drugs text value.</value>
        public string ShowAllDrugsText
        {
            get { return (string)GetValue(ShowAllDrugsTextProperty); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the search button is enabled.
        /// </summary>
        /// <value>Whether the search button is enabled.</value>
        public bool IsDrugSearchButtonEnabled
        {
            get { return (bool)GetValue(IsDrugSearchButtonEnabledProperty); }
            set { SetValue(IsDrugSearchButtonEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets the drugs results list message.
        /// </summary>
        /// <value>The drugs results list message value.</value>
        public object DrugsResultsListMessage
        {
            get { return (object)GetValue(DrugsResultsListMessageProperty); }
            set { SetValue(DrugsResultsListMessageProperty, value); }
        }

        /// <summary>
        /// Gets or sets the drugs results list message template.
        /// </summary>
        /// <value>The drugs results list message template value.</value>
        public DataTemplate DrugsResultsListMessageTemplate
        {
            get { return (DataTemplate)GetValue(DrugsResultsListMessageTemplateProperty); }
            set { SetValue(DrugsResultsListMessageTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the cascading list maximum height.
        /// </summary>
        /// <value>The cascading list maximum height value.</value>
        public double CascadingListMaximumHeight
        {
            get { return (double)GetValue(CascadingListMaximumHeightProperty); }
            set { SetValue(CascadingListMaximumHeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the basic drug template for the detailed view.
        /// </summary>
        /// <value>The basic drug template value.</value>
        public DataTemplate BasicDrugTemplate
        {
            get { return (DataTemplate)GetValue(BasicDrugTemplateProperty); }
            set { SetValue(BasicDrugTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the data template for displaying basic route information.
        /// </summary>
        /// <value>The basic route data template.</value>
        public DataTemplate BasicRouteTemplate
        {
            get { return (DataTemplate)GetValue(BasicRouteTemplateProperty); }
            set { SetValue(BasicRouteTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the drug search text.
        /// </summary>
        /// <value>The drug search text value.</value>
        public string DrugSearchText
        {
            get { return (string)GetValue(DrugSearchTextProperty); }
            set { SetValue(DrugSearchTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the detailed view maximum height.
        /// </summary>
        /// <value>The detailed view maximum height.</value>
        public double DetailedViewMaxHeight
        {
            get { return (double)GetValue(DetailedViewMaxHeightProperty); }
            set { SetValue(DetailedViewMaxHeightProperty, value); }
        }
        #endregion

        #region Private members
        /// <summary>
        /// Gets a value indicating whether the concise view has been initialised.
        /// </summary>
        private bool IsConciseViewInitialised
        {
            get
            {
                return this.conciseDrugsList != null && this.conciseRoutesList != null && this.conciseFormsList != null && this.conciseStrengthsList != null && this.conciseDosagesList != null && this.conciseFrequenciesList != null && this.conciseTemplatePrescriptionsList != null;
            }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Refreshes the drugs list.
        /// </summary>
        public void RefreshDrugsList()
        {
            this.UpdateDrugResultsList(true);
        }
        #endregion

        #region Public overrides
        /// <summary>
        /// Gets the template parts and adds event handlers.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.conciseDrugsListContainer = this.GetTemplateChild(SearchAndPrescribeControl.ElementConciseDrugsListContainer) as FrameworkElement;
            this.cascadingListBoxGroup = this.GetTemplateChild(SearchAndPrescribeControl.ElementCascadingListBoxGroup) as CascadingListBoxGroup;
            if (this.cascadingListBoxGroup != null)
            {
                this.cascadingListBoxGroup.SizeChanged += new SizeChangedEventHandler(this.CascadingListBoxGroup_SizeChanged);
            }

            this.conciseDrugsList = this.GetTemplateChild(SearchAndPrescribeControl.ElementConciseDrugsList) as CascadingListBox;
            if (this.conciseDrugsList != null)
            {                
                this.conciseDrugsList.GotFocus += new RoutedEventHandler(this.ConciseList_GotFocus);
                this.conciseDrugsList.Expanded += new EventHandler(this.ConciseDrugsList_Expanded);
                this.conciseDrugsList.ConfirmedSelectedItemChanged += new EventHandler(this.CascadingConciseList_ConfirmedSelectedItemChanged);
                this.conciseDrugsList.ItemSelected += new EventHandler(this.ConciseDrugsList_ItemSelected);
                this.conciseDrugsList.KeyDown += new KeyEventHandler(this.CascadingConciseList_KeyDown);
                this.UpdateDrugResultsList(true);
            }

            this.conciseRoutesList = this.GetTemplateChild(SearchAndPrescribeControl.ElementConciseRoutesList) as CascadingListBox;
            if (this.conciseRoutesList != null)
            {
                this.conciseRoutesList.GotFocus += new RoutedEventHandler(this.ConciseList_GotFocus);
                this.conciseRoutesList.Expanded += new EventHandler(this.CascadingConciseList_Expanded);
                this.conciseRoutesList.ConfirmedSelectedItemChanged += new EventHandler(this.CascadingConciseList_ConfirmedSelectedItemChanged);
                this.conciseRoutesList.ItemSelected += new EventHandler(this.ConciseRoutesList_ItemSelected);
                this.conciseRoutesList.OtherSelected += new EventHandler(this.ConciseRoutesList_OtherSelected);
                this.conciseRoutesList.KeyDown += new KeyEventHandler(this.CascadingConciseList_KeyDown);
            }

            this.conciseFormsList = this.GetTemplateChild(SearchAndPrescribeControl.ElementConciseFormsList) as CascadingListBox;
            if (this.conciseFormsList != null)
            {
                this.conciseFormsList.GotFocus += new RoutedEventHandler(this.ConciseList_GotFocus);
                this.conciseFormsList.Expanded += new EventHandler(this.CascadingConciseList_Expanded);
                this.conciseFormsList.ConfirmedSelectedItemChanged += new EventHandler(this.CascadingConciseList_ConfirmedSelectedItemChanged);
                this.conciseFormsList.ItemSelected += new EventHandler(this.ConciseFormsList_ItemSelected);
                this.conciseFormsList.KeyDown += new KeyEventHandler(this.CascadingConciseList_KeyDown);
            }

            this.conciseStrengthsList = this.GetTemplateChild(SearchAndPrescribeControl.ElementConciseStrengthsList) as SplitComboBox;
            if (this.conciseStrengthsList != null)
            {
                this.conciseStrengthsList.GotFocus += new RoutedEventHandler(this.ConciseList_GotFocus);
                this.conciseStrengthsList.ItemSelected += new EventHandler(this.ConciseStrengthsList_ItemSelected);
                this.conciseStrengthsList.OtherSelected += new EventHandler(this.ConciseStrengthsList_OtherSelected);
                CascadingListBoxGroup.SetListVisibility(this.conciseStrengthsList, Visibility.Collapsed);
            }

            this.conciseDosagesList = this.GetTemplateChild(SearchAndPrescribeControl.ElementConciseDosagesList) as SplitComboBox;
            if (this.conciseDosagesList != null)
            {
                this.conciseDosagesList.GotFocus += new RoutedEventHandler(this.ConciseList_GotFocus);
                this.conciseDosagesList.ItemSelected += new EventHandler(this.ConciseDosagesList_ItemSelected);
                this.conciseDosagesList.OtherSelected += new EventHandler(this.ConciseDosagesList_OtherSelected);
                CascadingListBoxGroup.SetListVisibility(this.conciseDosagesList, Visibility.Collapsed);
            }

            this.conciseFrequenciesList = this.GetTemplateChild(SearchAndPrescribeControl.ElementConciseFrequenciesList) as SplitComboBox;
            if (this.conciseFrequenciesList != null)
            {
                this.conciseFrequenciesList.GotFocus += new RoutedEventHandler(this.ConciseList_GotFocus);
                this.conciseFrequenciesList.ItemSelected += new EventHandler(this.ConciseFrequenciesList_ItemSelected);
                this.conciseFrequenciesList.OtherSelected += new EventHandler(this.ConciseFrequenciesList_OtherSelected);
                CascadingListBoxGroup.SetListVisibility(this.conciseFrequenciesList, Visibility.Collapsed);
            }

            this.conciseBrandsList = this.GetTemplateChild(SearchAndPrescribeControl.ElementConciseBrandsList) as SplitComboBox;
            if (this.conciseFrequenciesList != null)
            {
                this.conciseBrandsList.GotFocus += new RoutedEventHandler(this.ConciseList_GotFocus);
                CascadingListBoxGroup.SetListVisibility(this.conciseBrandsList, Visibility.Collapsed);
            }

            this.conciseTemplatePrescriptionsList = this.GetTemplateChild(SearchAndPrescribeControl.ElementConciseTemplatePrescriptionsList) as SplitComboBox;
            if (this.conciseTemplatePrescriptionsList != null)
            {
                this.conciseTemplatePrescriptionsList.GotFocus += new RoutedEventHandler(this.ConciseList_GotFocus);
                this.conciseTemplatePrescriptionsList.ItemsChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(this.ConciseTemplatePrescriptionsList_ItemsChanged);
                this.conciseTemplatePrescriptionsList.ItemSelected += new EventHandler(this.ConciseTemplatePrescriptionsList_ItemSelected);
                this.conciseTemplatePrescriptionsList.OtherSelected += new EventHandler(this.ConciseTemplatePrescriptionsList_OtherSelected);
            }

            this.conciseStartingConditionsList = this.GetTemplateChild(SearchAndPrescribeControl.ElementConciseStartingConditionsList) as SplitComboBox;
            if (this.conciseStartingConditionsList != null)
            {
                this.conciseStartingConditionsList.ItemSelected += new EventHandler(this.ConciseStartingConditionsList_ItemSelected);
            }

            SplitComboBox conciseAdministrationTimesList = this.GetTemplateChild(SearchAndPrescribeControl.ElementConciseAdministrationTimesList) as SplitComboBox;
            if (conciseAdministrationTimesList != null)
            {
                conciseAdministrationTimesList.OtherSelected += new EventHandler(this.ConciseAdministrationTimesList_OtherSelected);
            }

            this.conciseDurationsList = this.GetTemplateChild(SearchAndPrescribeControl.ElementConciseDurationsList) as SplitComboBox;
            if (this.conciseDurationsList != null)
            {
                this.conciseDurationsList.OtherSelected += new EventHandler(this.ConciseDurationsList_OtherSelected);
            }

            this.drugSearchTextBox = this.GetTemplateChild(SearchAndPrescribeControl.ElementDrugSearchTextBox) as TextBox;
            if (this.drugSearchTextBox != null)
            {
#if SILVERLIGHT
                this.drugSearchTextBox.KeyDown += new KeyEventHandler(this.DrugSearchTextBox_KeyDown);
#else
                this.drugSearchTextBox.PreviewKeyDown += new KeyEventHandler(this.DrugSearchTextBox_KeyDown);
#endif

                if (this.focusDrugSearchTextBoxOnLoad)
                {
                    FocusHelper.FocusControl(this.drugSearchTextBox);
                    this.focusDrugSearchTextBoxOnLoad = false;
                }

                this.drugSearchTextBox.GotFocus += new RoutedEventHandler(this.DrugsList_GotFocus);
                this.drugSearchTextBox.LostFocus += new RoutedEventHandler(this.DrugsList_LostFocus);
            }

            this.drugSearchButton = this.GetTemplateChild(SearchAndPrescribeControl.ElementDrugSearchButton) as Button;
            if (this.drugSearchButton != null)
            {
                this.drugSearchButton.Click += new RoutedEventHandler(this.DrugSearchButton_Click);
                this.drugSearchButton.KeyDown += new KeyEventHandler(this.DrugSearchButton_KeyDown);
                this.drugSearchButton.GotFocus += new RoutedEventHandler(this.DrugsList_GotFocus);
                this.drugSearchButton.LostFocus += new RoutedEventHandler(this.DrugsList_LostFocus);
            }

            this.showAllDrugsButton = this.GetTemplateChild(SearchAndPrescribeControl.ElementShowAllDrugsButton) as Button;
            if (this.showAllDrugsButton != null)
            {
                this.showAllDrugsButton.Click += new RoutedEventHandler(this.ShowAllDrugsButton_Click);
                this.showAllDrugsButton.KeyDown += new KeyEventHandler(this.DrugsList_KeyDown);
                this.showAllDrugsButton.GotFocus += new RoutedEventHandler(this.DrugsList_GotFocus);
                this.showAllDrugsButton.LostFocus += new RoutedEventHandler(this.DrugsList_LostFocus);
            }

            UIElement backgroundElement = this.GetTemplateChild(SearchAndPrescribeControl.ElementBackgroundElement) as UIElement;
            if (backgroundElement != null)
            {
                backgroundElement.MouseLeftButtonDown += new MouseButtonEventHandler(this.BackgroundElement_MouseLeftButtonDown);
            }

            this.conciseDetailedViewToggleButton = this.GetTemplateChild(SearchAndPrescribeControl.ElementConciseDetailedViewToggleButton) as ToggleButton;
            if (this.conciseDetailedViewToggleButton != null)
            {
                this.conciseDetailedViewToggleButton.KeyDown += new KeyEventHandler(this.ConciseDetailedViewToggleButton_KeyDown);
            }

            this.detailedDrugRouteList = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedDrugRouteList) as SplitComboBox;
            if (this.detailedDrugRouteList != null)
            {
                this.detailedDrugRouteList.OtherSelected += new EventHandler(this.DetailedDrugRouteList_OtherSelected);
            }

            this.unlicensedReasonTextBox = this.GetTemplateChild(SearchAndPrescribeControl.ElementUnlicensedReasonTextBox) as TextBox;

            this.detailedReasonForPrescribingOptionalFieldContainer = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedReasonForPrescribingOptionalFieldContainer) as OptionalFieldContainer;
            SplitComboBox detailedReasonForPrescribingList = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedReasonForPrescribingList) as SplitComboBox;
            if (detailedReasonForPrescribingList != null)
            {
                detailedReasonForPrescribingList.OtherSelected += new EventHandler(this.DetailedReasonForPrescribingList_OtherSelected);
            }

            this.scrollViewer = this.GetTemplateChild(SearchAndPrescribeControl.ElementScrollViewer) as ScrollViewer;

            this.detailedStrengthsOptionalFieldContainer = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedStrengthsOptionalFieldContainer) as OptionalFieldContainer;
            this.detailedStrengthsList = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedStrengthsList) as SplitComboBox;
            if (this.detailedStrengthsList != null)
            {
                this.detailedStrengthsList.OtherSelected += new EventHandler(this.DetailedStrengthsList_OtherSelected);
            }

            this.detailedFormsOptionalFieldContainer = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedFormsOptionalFieldContainer) as OptionalFieldContainer;
            this.detailedFormsList = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedFormsList) as SplitComboBox;
            if (this.detailedFormsList != null)
            {
                this.detailedFormsList.OtherSelected += new EventHandler(this.DetailedFormsList_OtherSelected);
            }

            this.detailedDosagesList = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedDosagesList) as SplitComboBox;
            this.detailedMethodTextBox = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedMethodTextBox) as TextBox;

            this.detailedSiteOptionalFieldContainer = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedSiteOptionalFieldContainer) as OptionalFieldContainer;
            this.detailedSitesList = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedSitesList) as SplitComboBox;
            if (this.detailedSitesList != null)
            {
                this.detailedSitesList.OtherSelected += new EventHandler(this.DetailedSitesList_OtherSelected);
            }

            OptionalFieldContainer detailedAsRequiredOptionalFieldContainer = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedAsRequiredOptionFieldContainer) as OptionalFieldContainer;
            if (detailedAsRequiredOptionalFieldContainer != null)
            {
                detailedAsRequiredOptionalFieldContainer.Expanded += new EventHandler(this.DetailedAsRequiredOptionalFieldContainer_Expanded);
            }

            this.detailedFrequenciesList = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedFrequenciesList) as SplitComboBox;
            this.detailedStartingConditionsList = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedStartingConditionsList) as SplitComboBox;

            SplitComboBox detailedAdministrationTimesList = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedAdministrationTimesList) as SplitComboBox;
            if (detailedAdministrationTimesList != null)
            {
                detailedAdministrationTimesList.OtherSelected += new EventHandler(this.DetailedAdministrationTimesList_OtherSelected);
            }

            this.detailedDurationsList = this.GetTemplateChild(SearchAndPrescribeControl.ElementDetailedDurationsList) as SplitComboBox;

            this.previewButton = this.GetTemplateChild(SearchAndPrescribeControl.ElementPreviewButton) as Button;
            if (this.previewButton != null)
            {
                this.previewButton.Click += new RoutedEventHandler(this.PreviewButton_Click);
            }

            Button authorizeButton = this.GetTemplateChild(SearchAndPrescribeControl.ElementAuthorizeButton) as Button;
            if (authorizeButton != null)
            {
                authorizeButton.Click += new RoutedEventHandler(this.AuthorizeButton_Click);
            }

            this.closeButton = this.GetTemplateChild(SearchAndPrescribeControl.ElementCloseButton) as Button;
            if (this.closeButton != null)
            {
                this.closeButton.Click += new RoutedEventHandler(this.CloseButton_Click);
            }

            Border rootBorder = this.GetTemplateChild(SearchAndPrescribeControl.ElementRootBorder) as Border;
            if (rootBorder != null)
            {
                this.templatePrescriptionPromptWatermarkTemplate = rootBorder.Resources[SearchAndPrescribeControl.ElementTemplatePrescriptionPromptWatermarkTemplate] as DataTemplate;
                this.templatePrescriptionNoPromptWatermarkTemplate = rootBorder.Resources[SearchAndPrescribeControl.ElementTemplatePrescriptionNoPromptWatermarkTemplate] as DataTemplate;
            }

            this.RegisterIsFieldShowingChangedEvents(rootBorder);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Focuses the drug search text box.
        /// </summary>
        public void FocusDrugSearchTextBox()
        {
            if (this.drugSearchTextBox != null)
            {
                FocusHelper.FocusControl(this.drugSearchTextBox);
            }
            else
            {
                this.focusDrugSearchTextBoxOnLoad = true;
            }
        }

        /// <summary>
        /// Focuses the preview button.
        /// </summary>
        public void FocusPreviewButton()
        {
            if (this.previewButton != null)
            {
                FocusHelper.FocusControl(this.previewButton);
            }
        }

        /// <summary>
        /// Focuses the close button.
        /// </summary>
        public void FocusCloseButton()
        {
            if (this.closeButton != null)
            {
                FocusHelper.FocusControl(this.closeButton);
            }
        }

        /// <summary>
        /// Resets the control.
        /// </summary>
        public void Reset()
        {
            // Closes the option field containers.
            this.expandedOptionalFieldContainers.Clear();
            this.IsConciseDetailedViewToggleEnabled = false;
            this.IsDetailedView = false;
            this.detailedViewLocked = false;
            this.DrugSearchText = string.Empty;
            this.detailedReasonForPrescribingOptionalFieldContainer.Collapse();
            this.detailedStrengthsOptionalFieldContainer.Collapse();
            this.detailedFormsOptionalFieldContainer.Collapse();
            this.detailedSiteOptionalFieldContainer.Collapse();
        }

        /// <summary>
        /// Moves focus to the next control on the form where a value has not been selected.
        /// </summary>
        /// <returns>Whether a control was focused.</returns>
        public virtual bool FocusNextControl()
        {
            if (this.IsDetailedView)
            {
                if (this.IsUnlicensed && this.unlicensedReasonTextBox != null && string.IsNullOrEmpty(this.unlicensedReasonTextBox.Text))
                {
                    FocusHelper.FocusControl(this.unlicensedReasonTextBox);
                    return true;
                }
                else if (this.IsStrengthMandatory && this.SelectedStrength == null && this.detailedStrengthsList != null)
                {
                    FocusHelper.FocusControl(this.detailedStrengthsList);
                    return true;
                }
                else if (this.IsFormMandatory && this.SelectedForm == null && this.detailedFormsList != null)
                {
                    FocusHelper.FocusControl(this.detailedFormsList);
                    return true;
                }
                else if (this.SelectedDose == null && this.detailedDosagesList != null)
                {
                    FocusHelper.FocusControl(this.detailedDosagesList);
                    return true;
                }
                else if (this.IsUnlicensed && this.detailedMethodTextBox != null && string.IsNullOrEmpty(this.detailedMethodTextBox.Text))
                {
                    FocusHelper.FocusControl(this.detailedMethodTextBox);
                    return true;
                }
                else if (this.SelectedFrequency == null && this.detailedFrequenciesList != null)
                {
                    FocusHelper.FocusControl(this.detailedFrequenciesList);
                    return true;
                }
                else if (this.IsAsRequired && this.SelectedStartingCondition == null && this.conciseStartingConditionsList != null)
                {
                    FocusHelper.FocusControl(this.conciseStartingConditionsList);
                    return true;
                }
                else if (this.SelectedDuration == null && this.conciseDurationsList != null)
                {
                    FocusHelper.FocusControl(this.detailedDurationsList);
                    return true;
                }                
            }
            else
            {
                if (this.SelectedDrug == null && this.drugSearchTextBox != null)
                {
                    FocusHelper.FocusControl(this.drugSearchTextBox);
                    return true;
                }
                else if (this.SelectedRoute == null && this.conciseRoutesList != null && this.conciseRoutesList.Visibility == Visibility.Visible)
                {
                    FocusHelper.FocusControl(this.conciseRoutesList);
                    return true;
                }
                else if (this.IsFormMandatory && this.SelectedForm == null && this.conciseFormsList != null)
                {
                    FocusHelper.FocusControl(this.conciseFormsList);
                    return true;
                }
                else
                {
                    if (this.SelectedStrength == null && this.SelectedDose == null && this.SelectedFrequency == null && this.conciseTemplatePrescriptionsList.Items.Count > 1)
                    {
                        FocusHelper.FocusControl(this.conciseTemplatePrescriptionsList);
                        return true;
                    }
                    else
                    {
                        if (this.IsStrengthMandatory && this.SelectedStrength == null)
                        {
                            FocusHelper.FocusControl(this.conciseTemplatePrescriptionsList);
                            return true;
                        }
                        else if (this.SelectedDose == null && this.conciseDosagesList != null && this.conciseDosagesList.Items.Count > 1)
                        {
                            FocusHelper.FocusControl(this.conciseDosagesList);
                            return true;
                        }
                        else if (this.SelectedFrequency == null && this.conciseFrequenciesList != null && this.conciseFrequenciesList.Items.Count > 1)
                        {
                            FocusHelper.FocusControl(this.conciseFrequenciesList);
                            return true;
                        }
                        else if (this.SelectedDuration == null && this.conciseDurationsList != null && this.conciseDurationsList.Items.Count > 1)
                        {
                            FocusHelper.FocusControl(this.conciseDurationsList);
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        #endregion

        /// <summary>
        /// Switches to the detailed view.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected override void OnIsUnlicensedChanged(DependencyPropertyChangedEventArgs args)
        {
            base.OnIsUnlicensedChanged(args);
            if (this.IsUnlicensed)
            {
                this.detailedViewLocked = true;
                this.IsDetailedView = true;
                SetValue(SearchAndPrescribeControl.IsConciseDetailedViewToggleEnabledProperty, false);
            }
        }

        #region Protected virtual methods
        /// <summary>
        /// Overridable method for updating the control for the detailed / concise view.
        /// </summary>
        protected virtual void OnIsDetailedViewChanged()
        {
            if (this.IsDetailedView)
            {
                if (this.detailedDrugRouteList != null && this.detailedDrugRouteList.Items.Count > 0)
                {
                    this.detailedDrugRouteList.ConfirmedSelectedItem = this.detailedDrugRouteList.Items[0];
                }
            }

            if (this.scrollViewer != null)
            {
                this.scrollViewer.ScrollToVerticalOffset(0);
            }

            this.FocusNextControl();
        }

        /// <summary>
        /// If a value is selected, hide the template prescriptions.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected override void OnSelectedStrengthChanged(DependencyPropertyChangedEventArgs args)
        {
            base.OnSelectedStrengthChanged(args);
            if (this.SelectedStrength != null)
            {
                this.HideTemplatePrescriptions();
            }
        }

        /// <summary>
        /// If a value is selected, hide the template prescriptions.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected override void OnSelectedDoseChanged(DependencyPropertyChangedEventArgs args)
        {
            base.OnSelectedDoseChanged(args);
            if (this.SelectedDose != null)
            {
                this.HideTemplatePrescriptions();
            }
        }

        /// <summary>
        /// If a value is selected, hide the template prescriptions.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected override void OnSelectedFrequencyChanged(DependencyPropertyChangedEventArgs args)
        {
            base.OnSelectedFrequencyChanged(args);
            if (this.SelectedFrequency != null)
            {
                this.HideTemplatePrescriptions();
            }
        }

        /// <summary>
        /// If a value is selected, hide the template prescriptions.
        /// </summary>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        protected override void OnSelectedBrandChanged(DependencyPropertyChangedEventArgs args)
        {
            base.OnSelectedBrandChanged(args);
            if (this.SelectedBrand != null)
            {
                this.HideTemplatePrescriptions();
            }
        }
        #endregion

        #region Dependency Property Changed callbacks
        /// <summary>
        /// Updates the drugs results list.
        /// </summary>
        /// <param name="obj">The search and prescribe control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void IsShowingAllDrugResults_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeControl searchAndPrescribeControl = obj as SearchAndPrescribeControl;
            searchAndPrescribeControl.UpdateDrugResultsList(false);
        }

        /// <summary>
        /// Updates the control for the detailed / concise view.
        /// </summary>
        /// <param name="obj">The search and prescribe control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void IsDetailedView_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeControl searchAndPrescribeControl = obj as SearchAndPrescribeControl;
            searchAndPrescribeControl.OnIsDetailedViewChanged();
        }

        /// <summary>
        /// Fires the DrugSearchTextChanged Event.
        /// </summary>
        /// <param name="obj">The search and prescribe control.</param>
        /// <param name="args">Dependency Property Changed Event Args.</param>
        private static void DrugSearchText_Changed(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SearchAndPrescribeControl searchAndPrescribeControl = obj as SearchAndPrescribeControl;
            if (searchAndPrescribeControl.DrugSearchTextChanged != null)
            {
                searchAndPrescribeControl.DrugSearchTextChanged(searchAndPrescribeControl, EventArgs.Empty);
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Updates the drugs list.
        /// </summary>
        /// <param name="updateIsShowingAllDrugResults">Whether this call should update the IsShowingAllDrugResults member.</param>
        private void UpdateDrugResultsList(bool updateIsShowingAllDrugResults)
        {
            if (this.conciseDrugsList != null)
            {
                if (this.IsShowingAllDrugResults)
                {
                    this.conciseDrugsList.PrimaryItemsSource = this.PrimaryDrugsItemsSource;
                    this.conciseDrugsList.SecondaryItemsSource = this.SecondaryDrugsItemsSource;
                }
                else
                {
                    if (this.PrimaryDrugsItemsSource != null || this.SecondaryDrugsItemsSource != null)
                    {
                        int drugItemsCount = IEnumerableHelper.GetItemCount(this.PrimaryDrugsItemsSource);
                        if (drugItemsCount > this.DrugsListCollapsedItemCount)
                        {
                            this.conciseDrugsList.PrimaryItemsSource = IEnumerableHelper.GetRangeOfItems(this.PrimaryDrugsItemsSource, 0, this.DrugsListCollapsedItemCount - 1);
                            SetValue(SearchAndPrescribeControl.ShowAllDrugsTextProperty, string.Format(CultureInfo.CurrentCulture, "Showing 1 to {0} of {1} results", this.DrugsListCollapsedItemCount, drugItemsCount + IEnumerableHelper.GetItemCount(this.SecondaryDrugsItemsSource)));
                        }
                        else
                        {
                            this.conciseDrugsList.PrimaryItemsSource = this.PrimaryDrugsItemsSource;
                            drugItemsCount += IEnumerableHelper.GetItemCount(this.SecondaryDrugsItemsSource);
                            if (drugItemsCount > this.DrugsListCollapsedItemCount)
                            {
                                this.conciseDrugsList.SecondaryItemsSource = IEnumerableHelper.GetRangeOfItems(this.SecondaryDrugsItemsSource, 0, this.DrugsListCollapsedItemCount - IEnumerableHelper.GetItemCount(this.PrimaryDrugsItemsSource) - 1);
                                SetValue(SearchAndPrescribeControl.ShowAllDrugsTextProperty, string.Format(CultureInfo.CurrentCulture, "Showing 1 to {0} of {1} results", this.DrugsListCollapsedItemCount, drugItemsCount));
                            }
                            else
                            {
                                this.conciseDrugsList.SecondaryItemsSource = this.SecondaryDrugsItemsSource;

                                if (updateIsShowingAllDrugResults)
                                {
                                    this.IsShowingAllDrugResults = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        this.conciseDrugsList.PrimaryItemsSource = null;
                        this.conciseDrugsList.SecondaryItemsSource = null;

                        if (updateIsShowingAllDrugResults)
                        {
                            this.IsShowingAllDrugResults = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region Control event handlers
        /// <summary>
        /// Shows all of the drug results.
        /// </summary>
        /// <param name="sender">The show all drugs button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void ShowAllDrugsButton_Click(object sender, RoutedEventArgs e)
        {
            object selectedDrug = this.SelectedDrug;
            if (this.drugSearchTextBox != null)
            {
                this.drugSearchTextBox.Focus();
            }

            this.IsShowingAllDrugResults = true;

            if (selectedDrug != null)
            {
                this.SelectedDrug = selectedDrug;
                if (this.conciseDrugsList != null)
                {
                    FocusHelper.FocusControl(this.conciseDrugsList);
                }
            }
        }

        /// <summary>
        /// Moves the selection up and down the concise drugs list.
        /// </summary>
        /// <param name="sender">The Drug Search Text Box.</param>
        /// <param name="e">Key Event Args.</param>
        private void DrugSearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.IsConciseViewInitialised)
            {
                if (Keyboard.Modifiers == ShortcutKeyHelper.ShortcutModifier)
                {
                    e.Handled = this.conciseDrugsList.SelectItemWithShortcutKey(e.Key);
                }
                else if (e.Key == Key.Up)
                {
                    this.conciseDrugsList.SelectedIndex = Math.Max(-1, this.conciseDrugsList.SelectedIndex - 1);
                    e.Handled = true;
                }
                else if (e.Key == Key.Down)
                {
                    this.conciseDrugsList.SelectedIndex = Math.Min(this.conciseDrugsList.Items.Count - 1, this.conciseDrugsList.SelectedIndex + 1);
                    e.Handled = true;
                }
                else if (e.Key == Key.Enter && this.conciseDrugsList.SelectedItem != null)
                {
                    this.conciseDrugsList.ConfirmedSelectedItem = this.conciseDrugsList.SelectedItem;
                    FocusHelper.FocusControl(this.conciseRoutesList);
                }
            }
        }

        /// <summary>
        /// If the shortcut modifier is pressed, an item is selected with the shortcut key.
        /// </summary>
        /// <param name="sender">The drugs list control.</param>
        /// <param name="e">Key Event Args.</param>
        private void DrugsList_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.conciseDrugsList != null && Keyboard.Modifiers == ShortcutKeyHelper.ShortcutModifier)
            {
                e.Handled = this.conciseDrugsList.SelectItemWithShortcutKey(e.Key);
            }
        }

        /// <summary>
        /// Hides the drugs list keyboard shortcuts.
        /// </summary>
        /// <param name="sender">The drug search text box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void DrugsList_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.conciseDrugsList != null)
            {
                this.conciseDrugsList.HideShortcutKeys();
            }
        }

        /// <summary>
        /// Shows the drugs list keyboard shortcuts.
        /// </summary>
        /// <param name="sender">The drug search text box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void DrugsList_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.conciseDrugsList != null)
            {
                this.conciseDrugsList.ShowShortcutKeys();
            }
        }

        /// <summary>
        /// Handles list navigation when the search button is focused.
        /// </summary>
        /// <param name="sender">The drug search button.</param>
        /// <param name="e">Key Event Args.</param>
        private void DrugSearchButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.IsConciseViewInitialised)
            {
                if (Keyboard.Modifiers == ShortcutKeyHelper.ShortcutModifier)
                {
                    e.Handled = this.conciseDrugsList.SelectItemWithShortcutKey(e.Key);
                }
                else if (e.Key == Key.Up)
                {
                    this.conciseDrugsList.SelectedIndex--;
                }
                else if (e.Key == Key.Down)
                {
                    this.conciseDrugsList.SelectedIndex++;
                }
            }
        }

        /// <summary>
        /// Raises the DrugSearchButtonClick event.
        /// </summary>
        /// <param name="sender">The drug search button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void DrugSearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DrugSearchButtonClick != null)
            {
                this.DrugSearchButtonClick(this, e);
            }
        }

        /// <summary>
        /// Collapses the cascading lists.
        /// </summary>
        /// <param name="sender">The background element.</param>
        /// <param name="e">Mouse Button Event Args.</param>
        private void BackgroundElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.SelectedDrug != null && this.SelectedRoute != null && (this.IsUnlicensed || (!this.IsFormMandatory || this.SelectedForm != null)))
            {
                CascadingListBox focusedListBox = this.conciseDrugsList.IsFocused ? this.conciseDrugsList : (this.conciseRoutesList.IsFocused ? this.conciseRoutesList : (this.conciseFormsList.IsFocused ? this.conciseFormsList : null));
                this.conciseDrugsList.IsExpanded = false;
                this.conciseRoutesList.IsExpanded = false;
                this.conciseFormsList.IsExpanded = false;

                if (focusedListBox != null)
                {
                    FocusHelper.FocusControl(focusedListBox);
                }
            }

            e.Handled = true;
        }

        /// <summary>
        /// Expands all cascading lists and drug search text box.
        /// </summary>
        /// <param name="sender">The concise drugs list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseDrugsList_Expanded(object sender, EventArgs e)
        {
            if (this.IsConciseViewInitialised)
            {
                this.conciseDrugsList.IsExpanded = true;
                this.conciseRoutesList.IsExpanded = true;
                this.conciseFormsList.IsExpanded = true;
            }
        }

        /// <summary>
        /// Raises the other selected event and switches to the detailed view.
        /// </summary>
        /// <param name="sender">The concise list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseRoutesList_OtherSelected(object sender, EventArgs e)
        {
            if (this.ConciseRoutesListOtherSelected != null)
            {
                this.ConciseRoutesListOtherSelected(this, e);
            }
        }

        /// <summary>
        /// Raises the other selected event and switches to the detailed view.
        /// </summary>
        /// <param name="sender">The concise list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseStrengthsList_OtherSelected(object sender, EventArgs e)
        {
            if (this.ConciseStrengthsListOtherSelected != null)
            {
                this.ConciseStrengthsListOtherSelected(this, e);
            }

            this.IsDetailedView = true;
        }

        /// <summary>
        /// Raises the other selected event and switches to the detailed view.
        /// </summary>
        /// <param name="sender">The concise list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseDosagesList_OtherSelected(object sender, EventArgs e)
        {
            if (this.ConciseDosagesListOtherSelected != null)
            {
                this.ConciseDosagesListOtherSelected(this, e);
            }

            this.IsDetailedView = true;
        }

        /// <summary>
        /// Raises the other selected event and switches to the detailed view.
        /// </summary>
        /// <param name="sender">The concise list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseFrequenciesList_OtherSelected(object sender, EventArgs e)
        {
            if (this.ConciseFrequenciesListOtherSelected != null)
            {
                this.ConciseFrequenciesListOtherSelected(this, e);
            }

            this.IsDetailedView = true;
        }

        /// <summary>
        /// Raises the other selected event and switches to the detailed view.
        /// </summary>
        /// <param name="sender">The concise list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseTemplatePrescriptionsList_OtherSelected(object sender, EventArgs e)
        {
            if (this.ConciseTemplatePrescriptionsListOtherSelected != null)
            {
                this.ConciseTemplatePrescriptionsListOtherSelected(this, e);
            }

            this.IsDetailedView = true;
        }

        /// <summary>
        /// Raises the other selected event.
        /// </summary>
        /// <param name="sender">The concise list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseAdministrationTimesList_OtherSelected(object sender, EventArgs e)
        {
            if (this.ConciseAdministrationTimesListOtherSelected != null)
            {
                this.ConciseAdministrationTimesListOtherSelected(this, e);
            }
        }

        /// <summary>
        /// Raises the other selected event.
        /// </summary>
        /// <param name="sender">The detailed list.</param>
        /// <param name="e">Event Args.</param>
        private void DetailedAdministrationTimesList_OtherSelected(object sender, EventArgs e)
        {
            if (this.DetailedAdministrationTimesListOtherSelected != null)
            {
                this.DetailedAdministrationTimesListOtherSelected(this, e);
            }
        }

        /// <summary>
        /// Raises the other selected event and switches to the detailed view.
        /// </summary>
        /// <param name="sender">The concise list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseDurationsList_OtherSelected(object sender, EventArgs e)
        {
            if (this.ConciseDurationsListOtherSelected != null)
            {
                this.ConciseDurationsListOtherSelected(this, e);
            }

            this.IsDetailedView = true;
        }

        /// <summary>
        /// Collapses the cascading lists if escape is pressed.
        /// </summary>
        /// <param name="sender">A cascading list.</param>
        /// <param name="e">Key Event Args.</param>
        private void CascadingConciseList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (this.SelectedDrug != null && this.SelectedRoute != null && (this.IsUnlicensed || (!this.IsFormMandatory || this.SelectedForm != null)))
                {
                    this.conciseDrugsList.IsExpanded = false;
                    this.conciseRoutesList.IsExpanded = false;
                    this.conciseFormsList.IsExpanded = false;

                    CascadingListBox cascadingListBox = sender as CascadingListBox;
                    if (cascadingListBox != null)
                    {
                        FocusHelper.FocusControl(cascadingListBox);
                    }
                }
            }
        }

        /// <summary>
        /// Moves focus to the routes list.
        /// </summary>
        /// <param name="sender">The concise drugs list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseDrugsList_ItemSelected(object sender, EventArgs e)
        {
            this.UpdateConciseDetailedViewToggle();

            if (this.SelectedDrug != null && this.SelectedRoute != null && (this.IsUnlicensed || (!this.IsFormMandatory || this.SelectedForm != null)))
            {
                this.conciseDrugsList.IsExpanded = false;
                this.conciseRoutesList.IsExpanded = false;
                this.conciseFormsList.IsExpanded = false;
            }

            if (this.SelectedRoute == null && this.conciseRoutesList.Items.Count > 0)
            {
                FocusHelper.FocusControl(this.conciseRoutesList);
            }

            if (this.cascadingListBoxGroup != null)
            {
                this.UpdateCascadingListBoxGroupWidth(new Size(this.cascadingListBoxGroup.ActualWidth, this.cascadingListBoxGroup.ActualHeight));
            }
        }

        /// <summary>
        /// Closes the initial concise lists.
        /// </summary>
        /// <param name="sender">The concise forms list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseRoutesList_ItemSelected(object sender, EventArgs e)
        {
            this.UpdateConciseDetailedViewToggle();

            if (this.SelectedDrug != null && this.SelectedRoute != null && (this.IsUnlicensed || (!this.IsFormMandatory || this.SelectedForm != null)))
            {
                this.conciseDrugsList.IsExpanded = false;
                this.conciseRoutesList.IsExpanded = false;
                this.conciseFormsList.IsExpanded = false;
            }

            if (!this.IsUnlicensed && !this.IsDetailedView)
            {
                if (this.IsFormMandatory && this.SelectedForm == null)
                {
                    FocusHelper.FocusControl(this.conciseFormsList);
                }
                else if (this.conciseTemplatePrescriptionsList.Items.Count > 1 && this.SelectedTemplatePrescription == null)
                {
                    this.conciseTemplatePrescriptionsList.OpenDropDownOnGotFocus = true;
                    FocusHelper.FocusControl(this.conciseTemplatePrescriptionsList);
                }
                else if (this.IsStrengthMandatory && this.SelectedStrength == null)
                {
                    FocusHelper.FocusControl(this.conciseStrengthsList);
                    this.conciseStrengthsList.IsDropDownOpen = true;
                }
                else if (this.SelectedDose == null)
                {
                    this.conciseDosagesList.OpenDropDownOnGotFocus = true;
                    FocusHelper.FocusControl(this.conciseDosagesList);                    
                }
                else
                {
                    FocusHelper.FocusControl(this.conciseRoutesList);
                }
            }
            else if (this.unlicensedReasonTextBox != null)
            {
                SetValue(SearchAndPrescribeControl.IsConciseDetailedViewToggleEnabledProperty, false);
                FocusHelper.FocusControl(this.unlicensedReasonTextBox);
            }

            if (this.cascadingListBoxGroup != null)
            {
                this.UpdateCascadingListBoxGroupWidth(new Size(this.cascadingListBoxGroup.ActualWidth, this.cascadingListBoxGroup.ActualHeight));
            }
        }

        /// <summary>
        /// Closes the initial concise lists.
        /// </summary>
        /// <param name="sender">The concise forms list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseFormsList_ItemSelected(object sender, EventArgs e)
        {
            this.UpdateConciseDetailedViewToggle();

            if (this.SelectedDrug != null && this.SelectedRoute != null && (this.IsUnlicensed || (!this.IsFormMandatory || this.SelectedForm != null)))
            {
                this.conciseDrugsList.IsExpanded = false;
                this.conciseRoutesList.IsExpanded = false;
                this.conciseFormsList.IsExpanded = false;
            }
            
            if (!this.IsDetailedView)
            {
                if (this.conciseTemplatePrescriptionsList.Items.Count > 1 && this.SelectedTemplatePrescription == null)
                {
                    this.conciseTemplatePrescriptionsList.OpenDropDownOnGotFocus = true;
                    FocusHelper.FocusControl(this.conciseTemplatePrescriptionsList);
                }
                else if (this.IsStrengthMandatory && this.SelectedStrength == null)
                {
                    this.conciseStrengthsList.OpenDropDownOnGotFocus = true;
                    FocusHelper.FocusControl(this.conciseStrengthsList);
                }
                else if (this.SelectedDose == null)
                {
                    this.conciseDosagesList.OpenDropDownOnGotFocus = true;
                    FocusHelper.FocusControl(this.conciseDosagesList);
                }
                else
                {
                    FocusHelper.FocusControl(this.conciseFormsList);
                }
            }

            if (this.cascadingListBoxGroup != null)
            {
                this.UpdateCascadingListBoxGroupWidth(new Size(this.cascadingListBoxGroup.ActualWidth, this.cascadingListBoxGroup.ActualHeight));
            }
        }

        /// <summary>
        /// Moves focus to the dose list, if a dose has not been selected.
        /// </summary>
        /// <param name="sender">The concise strengths list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseStrengthsList_ItemSelected(object sender, EventArgs e)
        {
            if (this.SelectedDose == null)
            {
                this.conciseDosagesList.OpenDropDownOnGotFocus = true;
                FocusHelper.FocusControl(this.conciseDosagesList);
            }
        }

        /// <summary>
        /// Moves focus to the frequency list if a frequency has not been selected.
        /// </summary>
        /// <param name="sender">The concise dosages list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseDosagesList_ItemSelected(object sender, EventArgs e)
        {
            if (this.SelectedFrequency == null)
            {
                this.conciseFrequenciesList.OpenDropDownOnGotFocus = true;
                FocusHelper.FocusControl(this.conciseFrequenciesList);
            }
        }

        /// <summary>
        /// Moves focus to the starting conditions list, if as required, otherwise, to the preview button.
        /// </summary>
        /// <param name="sender">The concise frequencies list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseFrequenciesList_ItemSelected(object sender, EventArgs e)
        {
            if (this.IsAsRequired && this.SelectedStartingCondition == null && this.conciseStartingConditionsList != null)
            {
                this.conciseStartingConditionsList.OpenDropDownOnGotFocus = true;
                FocusHelper.FocusControl(this.conciseStartingConditionsList);
            }
            else if (this.IsAuthorizable && !this.templatePrescriptionSelected && this.conciseDetailedViewToggleButton != null)
            {
                FocusHelper.FocusControl(this.previewButton);
            }

            this.templatePrescriptionSelected = true;
        }

        /// <summary>
        /// Hides the template prescriptions list,and moves focus on the the next field.
        /// </summary>
        /// <param name="sender">The concise template prescriptions list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseTemplatePrescriptionsList_ItemSelected(object sender, EventArgs e)
        {
            this.HideTemplatePrescriptions();

            if (this.IsAsRequired && this.SelectedStartingCondition == null && this.conciseStartingConditionsList != null)
            {
                this.conciseStartingConditionsList.OpenDropDownOnGotFocus = true;
                FocusHelper.FocusControl(this.conciseStartingConditionsList);
            }
            else if (this.IsAuthorizable && !this.templatePrescriptionSelected && this.conciseDetailedViewToggleButton != null)
            {
                FocusHelper.FocusControl(this.previewButton);
            }

            this.templatePrescriptionSelected = true;
        }

        /// <summary>
        /// Moves focus on the the next field.
        /// </summary>
        /// <param name="sender">The concise starting conditions list.</param>
        /// <param name="e">Event Args.</param>
        private void ConciseStartingConditionsList_ItemSelected(object sender, EventArgs e)
        {
            if (this.IsAuthorizable && this.conciseDetailedViewToggleButton != null)
            {
                FocusHelper.FocusControl(this.previewButton);
            }
        }

        /// <summary>
        /// Shows the template prescriptions list.
        /// </summary>
        /// <param name="sender">The concise template prescriptions list.</param>
        /// <param name="e">Notify Collection Changed Event Args.</param>
        private void ConciseTemplatePrescriptionsList_ItemsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.ShowTemplatePrescriptions();
        }

        /// <summary>
        /// Shows the template prescriptions lists.
        /// </summary>
        /// <param name="sender">A cascading list box.</param>
        /// <param name="e">Selection Changed Event Args.</param>
        private void CascadingConciseList_ConfirmedSelectedItemChanged(object sender, EventArgs e)
        {
            this.templatePrescriptionSelected = false;

            if (!this.IsDetailedView)
            {
                if (this.SelectedDrug != null && this.SelectedRoute != null && (!this.IsFormMandatory || this.SelectedForm != null))
                {
                    this.IsConciseDetailedViewToggleEnabled = true;
                }
                else
                {
                    this.IsConciseDetailedViewToggleEnabled = false;
                }
            }

            this.ShowTemplatePrescriptions();
        }

        /// <summary>
        /// Updates the concise list with focus.
        /// </summary>
        /// <param name="sender">The concise list.</param>
        /// <param name="e">Routed Event Args.</param>
        private void ConciseList_GotFocus(object sender, RoutedEventArgs e)
        {
            this.ShowConciseLists((sender as FrameworkElement).Name);
        }

        /// <summary>
        /// Expands all of the cascading lists.
        /// </summary>
        /// <param name="sender">A cascading list box.</param>
        /// <param name="e">Event Args.</param>
        private void CascadingConciseList_Expanded(object sender, EventArgs e)
        {
            if (this.IsConciseViewInitialised)
            {
                this.conciseDrugsList.IsExpanded = true;
                this.conciseRoutesList.IsExpanded = true;
                this.conciseFormsList.IsExpanded = true;

                if (this.SelectedStrength == null && this.SelectedDose == null && this.SelectedFrequency == null)
                {
                    if (this.IsFormMandatory)
                    {
                        this.SelectedForm = null;
                    }
                    else
                    {
                        this.SelectedRoute = null;
                    }
                }
            }
        }

        /// <summary>
        /// Collapses any open cascading lists.
        /// </summary>
        /// <param name="sender">The search and prescribe control.</param>
        /// <param name="e">Routed Event Args.</param>
        private void SearchAndPrescribeControl_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!this.IsDetailedView && this.IsConciseViewInitialised)
            {
                if (!this.conciseDrugsList.IsFocused && !this.conciseRoutesList.IsFocused && !this.conciseFormsList.IsFocused && e.OriginalSource != this.drugSearchTextBox && e.OriginalSource != this.drugSearchButton && e.OriginalSource != this.showAllDrugsButton &&
                    this.SelectedDrug != null && this.SelectedRoute != null && (!this.IsFormMandatory || this.SelectedForm != null))
                {
                    this.conciseDrugsList.IsExpanded = false;
                    this.conciseRoutesList.IsExpanded = false;
                    this.conciseFormsList.IsExpanded = false;
                }

                if (!this.conciseTemplatePrescriptionsList.IsFocused && this.conciseTemplatePrescriptionsList.ConfirmedSelectedItem != null)
                {
                    this.HideTemplatePrescriptions();
                }
            }
        }

        /// <summary>
        /// Toggles between the concise and detailed views.
        /// </summary>
        /// <param name="sender">The concise detailed view toggle.</param>
        /// <param name="e">Key Event Args.</param>
        private void ConciseDetailedViewToggleButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && this.IsConciseDetailedViewToggleEnabled)
            {
                this.IsDetailedView = !this.IsDetailedView;
            }
        }

        /// <summary>
        /// Raises the close event.
        /// </summary>
        /// <param name="sender">The close button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Close != null)
            {
                this.Close(this, e);
            }
        }

        /// <summary>
        /// Raises the authorize event.
        /// </summary>
        /// <param name="sender">The authorize button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void AuthorizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Authorize != null)
            {
                this.Authorize(this, e);
            }
        }

        /// <summary>
        /// Raises the preview event.
        /// </summary>
        /// <param name="sender">The preview button.</param>
        /// <param name="e">Routed Event Args.</param>
        private void PreviewButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Preview != null)
            {
                this.Preview(this, e);
            }
        }
        #endregion

        /// <summary>
        /// Registers field showing changes on option field containers to disable the concise / detailed view toggle.
        /// </summary>
        /// <param name="current">The current UI element.</param>
        private void RegisterIsFieldShowingChangedEvents(UIElement current)
        {
            if (current == null)
            {
                return;
            }

            OptionalFieldContainer optionalFieldContainer = current as OptionalFieldContainer;
            if (optionalFieldContainer != null)
            {
                optionalFieldContainer.Expanded += new EventHandler(this.OptionalFieldContainer_Expanded);
                optionalFieldContainer.Collapsed += new EventHandler(this.OptionalFieldContainer_Collapsed);
                return;
            }

            Panel panel = current as Panel;
            if (panel != null)
            {
                foreach (UIElement child in panel.Children)
                {
                    this.RegisterIsFieldShowingChangedEvents(child);
                }

                return;
            }

            ContentControl contentControl = current as ContentControl;
            if (contentControl != null)
            {
                this.RegisterIsFieldShowingChangedEvents(contentControl.Content as UIElement);
                return;
            }

            Border border = current as Border;
            if (border != null)
            {
                this.RegisterIsFieldShowingChangedEvents(border.Child);
                return;
            }
        }

        /// <summary>
        /// If in the detailed view, disables the concise / detailed view toggle.
        /// </summary>
        /// <param name="sender">An optional field container.</param>
        /// <param name="e">Event Args.</param>
        private void OptionalFieldContainer_Expanded(object sender, EventArgs e)
        {          
            if (this.IsDetailedView)
            {
                OptionalFieldContainer optionalFieldContainer = sender as OptionalFieldContainer;
                if (optionalFieldContainer != null && !this.expandedOptionalFieldContainers.Contains(optionalFieldContainer))
                {
                    this.expandedOptionalFieldContainers.Add(optionalFieldContainer);
                }

                this.detailedViewLocked = true;
                SetValue(SearchAndPrescribeControl.IsConciseDetailedViewToggleEnabledProperty, false);
            }
        }

        /// <summary>
        /// Removes the optional field container from the expanded list.
        /// 
        /// The concise / detail view toggle is re-enabled when all expanded optional field containers
        /// have been collapsed again.
        /// </summary>
        /// <param name="sender">The optional field container.</param>
        /// <param name="e">Event Args.</param>
        private void OptionalFieldContainer_Collapsed(object sender, EventArgs e)
        {
            OptionalFieldContainer optionalFieldContainer = sender as OptionalFieldContainer;
            if (optionalFieldContainer != null && this.expandedOptionalFieldContainers.Contains(optionalFieldContainer))
            {
                this.expandedOptionalFieldContainers.Remove(optionalFieldContainer);

                if (this.expandedOptionalFieldContainers.Count == 0)
                {
                    this.detailedViewLocked = false;
                    SetValue(SearchAndPrescribeControl.IsConciseDetailedViewToggleEnabledProperty, true);
                }
            }
        }

        /// <summary>
        /// Raises the clear event.
        /// </summary>
        /// <param name="sender">Detailed drug route list.</param>
        /// <param name="e">Event Args.</param>
        private void DetailedDrugRouteList_OtherSelected(object sender, EventArgs e)
        {
            if (this.Clear != null)
            {
                this.Clear(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Collapses the reason for prescribing list.
        /// </summary>
        /// <param name="sender">The detailed reason for prescribing list.</param>
        /// <param name="e">Event Args.</param>
        private void DetailedReasonForPrescribingList_OtherSelected(object sender, EventArgs e)
        {
            if (this.detailedReasonForPrescribingOptionalFieldContainer != null)
            {
                this.detailedReasonForPrescribingOptionalFieldContainer.Collapse();
                FocusHelper.FocusControl(this.detailedReasonForPrescribingOptionalFieldContainer);
            }
        }

        /// <summary>
        /// Collapses the detailed forms list.
        /// </summary>
        /// <param name="sender">The detailed forms list.</param>
        /// <param name="e">Event Args.</param>
        private void DetailedFormsList_OtherSelected(object sender, EventArgs e)
        {
            if (this.detailedFormsOptionalFieldContainer != null)
            {
                this.detailedFormsOptionalFieldContainer.Collapse();
                FocusHelper.FocusControl(this.detailedFormsOptionalFieldContainer);
            }
        }

        /// <summary>
        /// Collapses the detailed strengths list.
        /// </summary>
        /// <param name="sender">The detailed strengths list.</param>
        /// <param name="e">Event Args.</param>
        private void DetailedStrengthsList_OtherSelected(object sender, EventArgs e)
        {
            if (this.detailedStrengthsOptionalFieldContainer != null)
            {
                this.detailedStrengthsOptionalFieldContainer.Collapse();
                FocusHelper.FocusControl(this.detailedStrengthsOptionalFieldContainer);
            }
        }

        /// <summary>
        /// Collapses the sites list.
        /// </summary>
        /// <param name="sender">The detailed sites list.</param>
        /// <param name="e">Event Args.</param>
        private void DetailedSitesList_OtherSelected(object sender, EventArgs e)
        {
            if (this.detailedSiteOptionalFieldContainer != null)
            {
                this.detailedSiteOptionalFieldContainer.Collapse();
                FocusHelper.FocusControl(this.detailedSiteOptionalFieldContainer);
            }
        }

        /// <summary>
        /// Updates IsAsRequired to true.
        /// </summary>
        /// <param name="sender">The detailed is as required optional field container.</param>
        /// <param name="e">Event Args.</param>
        private void DetailedAsRequiredOptionalFieldContainer_Expanded(object sender, EventArgs e)
        {
            this.IsAsRequired = true;
        }

        /// <summary>
        /// Updates the cascading list box group size.
        /// </summary>
        /// <param name="sender">The cascading list box group.</param>
        /// <param name="e">Size Changed Event Args.</param>
        private void CascadingListBoxGroup_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateCascadingListBoxGroupWidth(e.NewSize);
        }        

        /// <summary>
        /// Resize the concise drugs list to fit all cascading lists into view.
        /// </summary>
        /// <param name="cascadingListBoxGroupSize">The cascading list group size.</param>
        private void UpdateCascadingListBoxGroupWidth(Size cascadingListBoxGroupSize)
        {
            if (this.IsConciseViewInitialised && this.conciseDrugsListContainer != null)
            {
                this.conciseDrugsListContainer.MaxWidth = double.PositiveInfinity;
                this.conciseDrugsListContainer.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                this.conciseRoutesList.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                this.conciseFormsList.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));                
                double controlsWidth = this.conciseDrugsListContainer.DesiredSize.Width + this.conciseRoutesList.DesiredSize.Width + this.conciseFormsList.DesiredSize.Width;

                if (controlsWidth > cascadingListBoxGroupSize.Width)
                {
                    this.conciseDrugsListContainer.MaxWidth = Math.Max(0, cascadingListBoxGroupSize.Width - this.conciseRoutesList.DesiredSize.Width - this.conciseFormsList.DesiredSize.Width);
                }
            }
        }
        #region Concise View List Visibility methods
        /// <summary>
        /// Updates whether the concise / detailed view toggle is enabled.
        /// </summary>
        private void UpdateConciseDetailedViewToggle()
        {
            if (this.SelectedDrug != null && this.SelectedRoute != null && !this.IsUnlicensed && !this.detailedViewLocked && (!this.IsFormMandatory || this.SelectedForm != null))
            {
                SetValue(SearchAndPrescribeControl.IsConciseDetailedViewToggleEnabledProperty, true);
            }
            else
            {
                SetValue(SearchAndPrescribeControl.IsConciseDetailedViewToggleEnabledProperty, false);
            }
        }

        /// <summary>
        /// Shows concise lists depending on currently focused list.
        /// </summary>
        /// <param name="currentList">The current list with focus.</param>
        private void ShowConciseLists(string currentList)
        {
            if (this.IsConciseViewInitialised)
            {
                switch (currentList)
                {
                    case SearchAndPrescribeControl.ElementConciseDrugsList:
                    case SearchAndPrescribeControl.ElementConciseRoutesList:
                    case SearchAndPrescribeControl.ElementConciseFormsList:
                        break;
                    case SearchAndPrescribeControl.ElementConciseStrengthsList:
                    case SearchAndPrescribeControl.ElementConciseDosagesList:
                    case SearchAndPrescribeControl.ElementConciseFrequenciesList:
                    case SearchAndPrescribeControl.ElementConciseBrandsList:
                    case SearchAndPrescribeControl.ElementConciseTemplatePrescriptionsList:
                        this.conciseDrugsList.IsExpanded = false;
                        this.conciseRoutesList.IsExpanded = false;
                        this.conciseFormsList.IsExpanded = false;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Shows the template prescription list.
        /// </summary>
        private void ShowTemplatePrescriptions()
        {
            if (this.IsConciseViewInitialised && this.conciseTemplatePrescriptionsList.Items.Count > 1 && this.SelectedStrength == null && this.SelectedDose == null && this.SelectedFrequency == null)
            {
                CascadingListBoxGroup.SetListVisibility(this.conciseStrengthsList, Visibility.Collapsed);
                CascadingListBoxGroup.SetListVisibility(this.conciseDosagesList, Visibility.Collapsed);
                CascadingListBoxGroup.SetListVisibility(this.conciseFrequenciesList, Visibility.Collapsed);
                CascadingListBoxGroup.SetListVisibility(this.conciseBrandsList, Visibility.Collapsed);

                if (this.templatePrescriptionPromptWatermarkTemplate != null)
                {
                    this.conciseTemplatePrescriptionsList.WatermarkTemplate = this.templatePrescriptionPromptWatermarkTemplate;
                }

                this.conciseTemplatePrescriptionsList.ResizeOnDropDownOpened = true;
                this.conciseTemplatePrescriptionsList.PopupVerticalAlignment = VerticalAlignment.Top;
                this.conciseTemplatePrescriptionsList.OpenDropDownOnGotFocus = true;
            }
            else
            {
                this.HideTemplatePrescriptions();
            }
        }

        /// <summary>
        /// Hides the template prescription list.
        /// </summary>
        private void HideTemplatePrescriptions()
        {
            if (this.IsConciseViewInitialised)
            {
                CascadingListBoxGroup.SetListVisibility(this.conciseStrengthsList, Visibility.Visible);
                CascadingListBoxGroup.SetListVisibility(this.conciseDosagesList, Visibility.Visible);
                CascadingListBoxGroup.SetListVisibility(this.conciseFrequenciesList, Visibility.Visible);
                CascadingListBoxGroup.SetListVisibility(this.conciseBrandsList, Visibility.Visible);

                if (this.templatePrescriptionNoPromptWatermarkTemplate != null)
                {
                    this.conciseTemplatePrescriptionsList.WatermarkTemplate = this.templatePrescriptionNoPromptWatermarkTemplate;
                }

                this.conciseTemplatePrescriptionsList.ResizeOnDropDownOpened = false;
                this.conciseTemplatePrescriptionsList.PopupVerticalAlignment = VerticalAlignment.Bottom;
            }
        }
        #endregion
    }
}
