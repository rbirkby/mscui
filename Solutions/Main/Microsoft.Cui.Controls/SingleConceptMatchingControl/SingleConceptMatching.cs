//-----------------------------------------------------------------------
// <copyright file="SingleConceptMatching.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>27-Nov-2008</date>
// <summary>The control used for Single Concept Matching.</summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    #region Using

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    #endregion

    /// <summary>
    /// The control used to provide clinical noting based on a single concept.
    /// </summary>
    [StyleTypedProperty(Property = "InputBoxItemContainerStyle", StyleTargetType = typeof(ListBoxItem))]
    [StyleTypedProperty(Property = "MatchingTermItemsControlItemContainerStyle", StyleTargetType = typeof(MatchingTermItemContainer))]
    [StyleTypedProperty(Property = "AdditionalTextBoxDecoratorItemContainerStyle", StyleTargetType = typeof(DecoratorItemContainer))]
    [StyleTypedProperty(Property = "InputBoxStyle", StyleTargetType = typeof(EncodableInputBox))]
    [StyleTypedProperty(Property = "AdditionalTextBoxStyle", StyleTargetType = typeof(DecoratorTextBox))]
    [StyleTypedProperty(Property = "LabelStyle", StyleTargetType = typeof(TextBlock))]
    [TemplatePart(Name = SingleConceptMatching.ElementEncodableInputBox, Type = typeof(EncodableInputBox))]
    [TemplatePart(Name = SingleConceptMatching.ElementDecoratorTextBox, Type = typeof(DecoratorTextBox))]
    [TemplatePart(Name = SingleConceptMatching.ElementSaveButton, Type = typeof(Button))]
    [TemplatePart(Name = SingleConceptMatching.ElementClearButton, Type = typeof(Button))]
    [TemplatePart(Name = SingleConceptMatching.ElementFilterComboBox, Type = typeof(System.Windows.Controls.ComboBox))]
    [TemplatePart(Name = SingleConceptMatching.ElementMatchingTermItemsControl, Type = typeof(MatchingTermItemsControl))]
    [TemplatePart(Name = SingleConceptMatching.ElementFilterComboBoxGrid, Type = typeof(Grid))]
    public class SingleConceptMatching : Control
    {
        #region Dependency properties
        /// <summary>
        /// The InputBox Text Dependency Property.
        /// </summary>
        public static readonly DependencyProperty InputBoxTextProperty =
            DependencyProperty.Register("InputBoxText", typeof(string), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The InputBox Selected Item Dependency Property.
        /// </summary>
        public static readonly DependencyProperty InputBoxSelectedItemProperty =
            DependencyProperty.Register("InputBoxSelectedItem", typeof(object), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The InputBox Items Source Dependency Property.
        /// </summary>
        public static readonly DependencyProperty InputBoxItemsSourceProperty =
            DependencyProperty.Register("InputBoxItemsSource", typeof(IEnumerable), typeof(SingleConceptMatching), new PropertyMetadata(null));

        /// <summary>
        /// The InputBox Item Container Style Dependency Property.
        /// </summary>
        public static readonly DependencyProperty InputBoxItemContainerStyleProperty =
            DependencyProperty.Register("InputBoxItemContainerStyle", typeof(Style), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The InputBox Item Template Dependency Property.
        /// </summary>
        public static readonly DependencyProperty InputBoxItemTemplateProperty =
            DependencyProperty.Register("InputBoxItemTemplate", typeof(DataTemplate), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The InputBox Encoded Item Template Dependency Property.
        /// </summary>
        public static readonly DependencyProperty InputBoxEncodedItemTemplateProperty =
            DependencyProperty.Register("InputBoxEncodedItemTemplate", typeof(DataTemplate), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The InputBox Fly Out Template Dependency Property.
        /// </summary>
        public static readonly DependencyProperty InputBoxFlyOutTemplateProperty =
            DependencyProperty.Register("InputBoxFlyOutTemplate", typeof(DataTemplate), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The InputBox Is Search Button Visible Dependency Property.
        /// </summary>
        public static readonly DependencyProperty InputBoxIsSearchButtonVisibleProperty =
            DependencyProperty.Register("InputBoxIsSearchButtonVisible", typeof(bool), typeof(SingleConceptMatching), new PropertyMetadata(true));

        /// <summary>
        /// The InputBox Is Search Button Visible Dependency Property.
        /// </summary>
        public static readonly DependencyProperty InputBoxFlyOutWidthProperty =
            DependencyProperty.Register("InputBoxFlyOutWidth", typeof(double), typeof(SingleConceptMatching), new PropertyMetadata(250d));        

        /// <summary>
        /// The InputBox Watermark Dependency Property.
        /// </summary>
        public static readonly DependencyProperty InputBoxWatermarkProperty =
            DependencyProperty.Register("InputBoxWatermark", typeof(object), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The InputBox Watermark Template Dependency Property.
        /// </summary>
        public static readonly DependencyProperty InputBoxWatermarkTemplateProperty =
            DependencyProperty.Register("InputBoxWatermarkTemplate", typeof(DataTemplate), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The InputBox Maximum Length Dependency Property.
        /// </summary>
        public static readonly DependencyProperty InputBoxMaxLengthProperty =
            DependencyProperty.Register("InputBoxMaxLength", typeof(int), typeof(SingleConceptMatching), new PropertyMetadata(255));

        /// <summary>
        /// The InputBox Search Button ToolTip Dependency Property.
        /// </summary>
        public static readonly DependencyProperty InputBoxSearchButtonToolTipProperty =
            DependencyProperty.Register("InputBoxSearchButtonToolTip", typeof(object), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The Is Save Button Enabled Dependency Property.
        /// </summary>
        public static readonly DependencyProperty IsSaveButtonEnabledProperty =
            DependencyProperty.Register("IsSaveButtonEnabled", typeof(bool), typeof(SingleConceptMatching), new PropertyMetadata(false));

        /// <summary>
        /// The Additional TextBox Text Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AdditionalTextBoxTextProperty =
            DependencyProperty.Register("AdditionalTextBoxText", typeof(string), typeof(SingleConceptMatching), new PropertyMetadata(string.Empty));

        /// <summary>
        /// The Additional TextBox Watermark Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AdditionalTextBoxWatermarkProperty =
            DependencyProperty.Register("AdditionalTextBoxWatermark", typeof(object), typeof(SingleConceptMatching), new PropertyMetadata(null));

        /// <summary>
        /// The Additional TextBox Watermark Template Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AdditionalTextBoxWatermarkTemplateProperty =
            DependencyProperty.Register("AdditionalTextBoxWatermarkTemplate", typeof(DataTemplate), typeof(SingleConceptMatching), new PropertyMetadata(null));

        /// <summary>
        /// The AdditionalTextBoxMatchingTermItemsSource Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AdditionalTextBoxMatchingTermItemsSourceProperty =
            DependencyProperty.Register("AdditionalTextBoxMatchingTermItemsSource", typeof(IEnumerable), typeof(SingleConceptMatching), new PropertyMetadata(null));

        /// <summary>
        /// The Additional TextBox Matching Term Start Index Member Path Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AdditionalTextBoxMatchingTermStartIndexMemberPathProperty =
            DependencyProperty.Register("AdditionalTextBoxMatchingTermStartIndexMemberPath", typeof(string), typeof(SingleConceptMatching), new PropertyMetadata(null));

        /// <summary>
        /// The Additional TextBox Matching Term Length Member Path Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AdditionalTextBoxMatchingTermLengthMemberPathProperty =
            DependencyProperty.Register("AdditionalTextBoxMatchingTermLengthMemberPath", typeof(string), typeof(SingleConceptMatching), new PropertyMetadata(null));

        /// <summary>
        /// The Additional TextBox Matching Term IsSelected Member Path Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AdditionalTextBoxMatchingTermIsSelectedMemberPathProperty =
            DependencyProperty.Register("AdditionalTextBoxMatchingTermIsSelectedMemberPath", typeof(string), typeof(SingleConceptMatching), new PropertyMetadata(null));

        /// <summary>
        /// The Additional TextBox IsReadOnly Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AdditionalTextBoxIsReadOnlyProperty =
            DependencyProperty.Register("AdditionalTextBoxIsReadOnly", typeof(bool), typeof(SingleConceptMatching), new PropertyMetadata(false));        

        /// <summary>
        /// The Additional TextBox Maximum Length Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AdditionalTextBoxMaxLengthProperty =
            DependencyProperty.Register("AdditionalTextBoxMaxLength", typeof(int), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The InputBox Style Dependency Property.
        /// </summary>
        public static readonly DependencyProperty InputBoxStyleProperty =
            DependencyProperty.Register("InputBoxStyle", typeof(Style), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The Additional TextBox Style Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AdditionalTextBoxStyleProperty =
            DependencyProperty.Register("AdditionalTextBoxStyle", typeof(Style), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The Subset Picker Items Source Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SubsetPickerItemsSourceProperty =
            DependencyProperty.Register("SubsetPickerItemsSource", typeof(IEnumerable), typeof(SingleConceptMatching), new PropertyMetadata(new PropertyChangedCallback(SubsetPickerItemsSource_Changed)));
                
        /// <summary>
        /// The Subset Picker DataTemplate Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SubsetPickerDataTemplateProperty =
            DependencyProperty.Register("SubsetPickerDataTemplate", typeof(DataTemplate), typeof(SingleConceptMatching), new PropertyMetadata(SubsetPickerDataTemplate_Changed));

        /// <summary>
        /// The Matching Term Items Control ItemTemplate Property Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MatchingTermItemsControlItemTemplateProperty =
            DependencyProperty.Register("MatchingTermItemsControlItemTemplate", typeof(DataTemplate), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The Matching Term Items Control Encoded ItemTemplate Property Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MatchingTermItemsControlEncodedItemTemplateProperty =
            DependencyProperty.Register("MatchingTermItemsControlEncodedItemTemplate", typeof(DataTemplate), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The Matching Term Items Control ItemContainerStyle Property Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MatchingTermItemsControlItemContainerStyleProperty =
            DependencyProperty.Register("MatchingTermItemsControlItemContainerStyle", typeof(Style), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The Matching Term Selected Item Member Path Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MatchingTermSelectedItemMemberPathProperty =
            DependencyProperty.Register("MatchingTermSelectedItemMemberPath", typeof(string), typeof(SingleConceptMatching), new PropertyMetadata(string.Empty));

        /// <summary>
        /// The MatchingTermAlternateItemsMemberPath Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MatchingTermAlternateItemsMemberPathProperty =
            DependencyProperty.Register("MatchingTermAlternateItemsMemberPath", typeof(string), typeof(SingleConceptMatching), new PropertyMetadata(string.Empty));

        /// <summary>
        /// The Matching Term Selected Item Text Member Path Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MatchingTermSelectedItemTextMemberPathProperty =
            DependencyProperty.Register("MatchingTermSelectedItemTextMemberPath", typeof(string), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The Matching Term Fly Out Content Template Dependency Property.
        /// </summary>
        public static readonly DependencyProperty MatchingTermFlyOutContentTemplateProperty =
            DependencyProperty.Register("MatchingTermFlyOutContentTemplate", typeof(DataTemplate), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The Additional TextBox Displayed Matching Terms Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AdditionalTextBoxDisplayedMatchingTermsProperty =
            DependencyProperty.Register("AdditionalTextBoxDisplayedMatchingTerms", typeof(ObservableCollection<object>), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The Subset Picker Label Text Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SubsetPickerLabelTextProperty =
            DependencyProperty.Register("SubsetPickerLabelText", typeof(string), typeof(SingleConceptMatching), new PropertyMetadata("Search filter"));

        /// <summary>
        /// The InputBox Label Text Dependency Property.
        /// </summary>
        public static readonly DependencyProperty InputBoxLabelTextProperty =
            DependencyProperty.Register("InputBoxLabelText", typeof(string), typeof(SingleConceptMatching), new PropertyMetadata("Enter a term"));

        /// <summary>
        /// The Label Style Dependency Property.
        /// </summary>
        public static readonly DependencyProperty LabelStyleProperty =
            DependencyProperty.Register("LabelStyle", typeof(Style), typeof(SingleConceptMatching), null);

        /// <summary>
        /// The Subset Picker Selected Index Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SubsetPickerSelectedIndexProperty =
            DependencyProperty.Register("SubsetPickerSelectedIndex", typeof(int), typeof(SingleConceptMatching), new PropertyMetadata(-1, new PropertyChangedCallback(SubsetPickerSelectedIndex_Changed)));

        /// <summary>
        /// The Subset Picker Selected Item Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SubsetPickerSelectedItemProperty =
            DependencyProperty.Register("SubsetPickerSelectedItem", typeof(object), typeof(SingleConceptMatching), new PropertyMetadata(new PropertyChangedCallback(SubsetPickerSelectedItem_Changed)));

        /// <summary>
        /// The Additional TextBox Decorator Item Container Style Dependency Property.
        /// </summary>
        public static readonly DependencyProperty AdditionalTextBoxDecoratorItemContainerStyleProperty =
            DependencyProperty.Register("AdditionalTextBoxDecoratorItemContainerStyle", typeof(Style), typeof(SingleConceptMatching), null);

        #endregion

        #region Template part names
        /// <summary>
        /// Specifies the name of the EncodableInputBox TemplatePart.
        /// </summary>
        private const string ElementEncodableInputBox = "EncodableInputBox";

        /// <summary>
        /// Specifies the name of the DecoratorTextBox TemplatePart.
        /// </summary>
        private const string ElementDecoratorTextBox = "DecoratorTextBox";

        /// <summary>
        /// Specifies the name of the EncodeButton TemplatePart.
        /// </summary>
        private const string ElementSaveButton = "SaveButton";

        /// <summary>
        /// Specifies the name of the CancelButton TemplatePart.
        /// </summary>
        private const string ElementClearButton = "ClearButton";

        /// <summary>
        /// Specifies the name of the FilterComboBox TemplatePart.
        /// </summary>
        private const string ElementFilterComboBox = "FilterComboBox";

        /// <summary>
        /// Specifies the name of the MatchingTermItemsControl TemplatePart.
        /// </summary>
        private const string ElementMatchingTermItemsControl = "MatchingTermItemsControl";

        /// <summary>
        /// Specifies the name of the FilterComboBoxGrid TemplatePart.
        /// </summary>
        private const string ElementFilterComboBoxGrid = "FilterComboBoxGrid";

        #endregion

        #region Private Template Part Fields

        /// <summary>
        /// Backing field for private EncodableInputBox property.
        /// </summary>
        private EncodableInputBox encodableInputBox;

        /// <summary>
        /// Backing field for private DecoratorTextBox property.
        /// </summary>
        private DecoratorTextBox decoratorTextBox;

        /// <summary>
        /// Backing field for private SaveButton property.
        /// </summary>
        private Button saveButton;

        /// <summary>
        /// Backing field for private ClearButton property.
        /// </summary>
        private Button clearButton;

        /// <summary>
        /// Backing field for private FilterComboBox property.
        /// </summary>
        private System.Windows.Controls.ComboBox filterComboBox;

        /// <summary>
        /// Backing field for private MatchingTermItemsControl property.
        /// </summary>
        private MatchingTermItemsControl matchingTermItemsControl;

        /// <summary>
        /// Backing field for the private FilterComboBoxGrid property.
        /// </summary>
        private Grid filterComboBoxGrid;

        #endregion 

        /// <summary>
        /// Member variable to indicate whether all the template parts have been initialized.
        /// </summary>
        private bool templatePartsLoaded;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleConceptMatching"/> class.
        /// </summary>
        public SingleConceptMatching()
        {
            DefaultStyleKey = typeof(SingleConceptMatching);            
            this.IsTabStop = false;
        }

        #region Events

        /// <summary>
        /// Occurs when [saved].
        /// </summary>
        public event RoutedEventHandler Saved;

        /// <summary>
        /// Occurs when [cleared].
        /// </summary>
        public event RoutedEventHandler Cleared;

        /// <summary>
        /// Occurs when [filter selected index changed].
        /// </summary>
        public event SelectionChangedEventHandler FilterSelectedIndexChanged;

        /// <summary>
        /// Occurs when [additional text box text changed].
        /// </summary>
        public event TextChangedEventHandler AdditionalTextBoxTextChanged;

        /// <summary>
        /// Occurs when [the input box's selection changes].
        /// </summary>
        public event EventHandler InputBoxSelectionChanged;

        /// <summary>
        /// Occurs when [the input box's search button is clicked].
        /// </summary>
        public event RoutedEventHandler InputBoxSearchButtonClicked;

        /// <summary>
        /// Occurs when [the enter key is pressed in the input box's text field].
        /// </summary>
        public event KeyEventHandler InputBoxEnterPressed;

        /// <summary>
        /// Occurs when [the enter key is pressed in the additional text box].
        /// </summary>
        public event KeyEventHandler AdditionalTextBoxEnterPressed;

        /// <summary>
        /// Occurs when [the input box text changes].
        /// </summary>
        public event TextChangedEventHandler InputBoxTextChanged;
        #endregion

        #region DecoratorTextBox public  members
        /// <summary>
        /// Gets or sets the additional text.
        /// </summary>
        /// <value>The additional text.</value>
        public string AdditionalTextBoxText
        {
            get 
            {
                if (this.DecoratorTextBox != null)
                {
                    return this.DecoratorTextBox.Text;
                }

                return null;
            }

            set 
            {
                if (this.DecoratorTextBox != null)
                {
                    this.DecoratorTextBox.Text = value;
                }

                SetValue(AdditionalTextBoxTextProperty, value); 
            }
        }

        /// <summary>
        /// Gets or sets the additional text watermark.
        /// </summary>
        /// <value>The additional text watermark.</value>
        public object AdditionalTextBoxWatermark
        {
            get { return (object)GetValue(AdditionalTextBoxWatermarkProperty); }
            set { SetValue(AdditionalTextBoxWatermarkProperty, value); }
        }

        /// <summary>
        /// Gets or sets the additional text watermark template property.
        /// </summary>
        /// <value>The additional text watermark template.</value>
        public DataTemplate AdditionalTextBoxWatermarkTemplate
        {
            get { return (DataTemplate)GetValue(AdditionalTextBoxWatermarkTemplateProperty); }
            set { SetValue(AdditionalTextBoxWatermarkTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the additional text box's matching terms items source.
        /// </summary>
        /// <value>The matching terms items source.</value>
        public IEnumerable AdditionalTextBoxMatchingTermItemsSource
        {
            get 
            { 
                return (IEnumerable)GetValue(AdditionalTextBoxMatchingTermItemsSourceProperty); 
            }

            set 
            {
                if (this.DecoratorTextBox != null)
                {
                    this.DecoratorTextBox.MatchingTermItemsSource = value;
                }

                SetValue(AdditionalTextBoxMatchingTermItemsSourceProperty, value); 
            }
        }

        /// <summary>
        /// Gets or sets the additional text box's matching terms start index property name.
        /// </summary>
        /// <value>The matching terms start index property name.</value>
        public string AdditionalTextBoxMatchingTermStartIndexMemberPath
        {
            get { return (string)GetValue(AdditionalTextBoxMatchingTermStartIndexMemberPathProperty); }
            set { SetValue(AdditionalTextBoxMatchingTermStartIndexMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the additional text box's matching terms length member path.
        /// </summary>
        /// <value>The matching terms length property name.</value>
        public string AdditionalTextBoxMatchingTermLengthMemberPath
        {
            get { return (string)GetValue(AdditionalTextBoxMatchingTermLengthMemberPathProperty); }
            set { SetValue(AdditionalTextBoxMatchingTermLengthMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the additional text box's matching terms is selected member path.
        /// </summary>
        /// <value>The matching terms is selected property name.</value>
        public string AdditionalTextBoxMatchingTermIsSelectedMemberPath
        {
            get { return (string)GetValue(AdditionalTextBoxMatchingTermIsSelectedMemberPathProperty); }
            set { SetValue(AdditionalTextBoxMatchingTermIsSelectedMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the additional text box is read-only.
        /// </summary>
        /// <value>Whether the additional text box is read-only.</value>
        public bool AdditionalTextBoxIsReadOnly
        {
            get { return (bool)GetValue(AdditionalTextBoxIsReadOnlyProperty); }
            set { SetValue(AdditionalTextBoxIsReadOnlyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the additional text box maximum length.
        /// </summary>
        /// <value>The additional text box maximum length.</value>
        public int AdditionalTextBoxMaxLength
        {
            get { return (int)GetValue(AdditionalTextBoxMaxLengthProperty); }
            set { SetValue(AdditionalTextBoxMaxLengthProperty, value); }
        }

        /// <summary>
        /// Gets a list of selected terms.
        /// </summary>
        /// <value>The list of selected terms.</value>
        public IList AdditionalTextBoxSelectedTerms
        {
            get
            {
                if (this.DecoratorTextBox != null)
                {
                    return this.DecoratorTextBox.SelectedTerms;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the additional text box displayed matching terms property.
        /// </summary>
        /// <value>The additional text box displayed matching terms.</value>
        public ObservableCollection<object> AdditionalTextBoxDisplayedMatchingTerms
        {
            get { return (ObservableCollection<object>)GetValue(AdditionalTextBoxDisplayedMatchingTermsProperty); }
        }

        /// <summary>
        /// Gets or sets the additional text box decorator container item style.
        /// </summary>
        /// <value>The additional text box decorator container item style.</value>
        public Style AdditionalTextBoxDecoratorItemContainerStyle
        {
            get 
            {
                return (Style)GetValue(AdditionalTextBoxDecoratorItemContainerStyleProperty); 
            }

            set 
            {
                if (value.TargetType == typeof(DecoratorItemContainer))
                {
                    SetValue(AdditionalTextBoxDecoratorItemContainerStyleProperty, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the input box style.
        /// </summary>
        /// <value>The input box style.</value>
        public Style InputBoxStyle
        {
            get 
            { 
                return (Style)GetValue(InputBoxStyleProperty); 
            }

            set 
            {
                if (value.TargetType == typeof(EncodableInputBox))
                {
                    SetValue(InputBoxStyleProperty, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the additional text box style.
        /// </summary>
        /// <value>The additional text box style.</value>
        public Style AdditionalTextBoxStyle
        {
            get 
            {
                return (Style)GetValue(AdditionalTextBoxStyleProperty); 
            }

            set 
            {
                if (value.TargetType == typeof(DecoratorTextBox))
                {
                    SetValue(AdditionalTextBoxStyleProperty, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the matching term items control item template.
        /// </summary>
        /// <value>The matching term items control item template.</value>
        public DataTemplate MatchingTermItemsControlItemTemplate
        {
            get { return (DataTemplate)GetValue(MatchingTermItemsControlItemTemplateProperty); }
            set { SetValue(MatchingTermItemsControlItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the matching term items control encoded item template.
        /// </summary>
        /// <value>The matching term items control encoded item template.</value>
        public DataTemplate MatchingTermItemsControlEncodedItemTemplate
        {
            get { return (DataTemplate)GetValue(MatchingTermItemsControlEncodedItemTemplateProperty); }
            set { SetValue(MatchingTermItemsControlEncodedItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the matching term items control item container style property.
        /// </summary>
        /// <value>The matching term items control item container style.</value>
        public Style MatchingTermItemsControlItemContainerStyle
        {
            get 
            {
                return (Style)GetValue(MatchingTermItemsControlItemContainerStyleProperty); 
            }

            set 
            {
                if (value.TargetType == typeof(MatchingTermItemContainer))
                {
                    SetValue(MatchingTermItemsControlItemContainerStyleProperty, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected item member path for the matching term items.
        /// </summary>
        /// <value>The selected item member path.</value>
        public string MatchingTermSelectedItemMemberPath
        {
            get { return (string)GetValue(MatchingTermSelectedItemMemberPathProperty); }
            set { SetValue(MatchingTermSelectedItemMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the selected item text member path for the matching term items.
        /// </summary>
        /// <value>The selected item text member path.</value>
        public string MatchingTermSelectedItemTextMemberPath
        {
            get { return (string)GetValue(MatchingTermSelectedItemTextMemberPathProperty); }
            set { SetValue(MatchingTermSelectedItemTextMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the matching term alternate items member path.
        /// </summary>
        /// <value>The matching term alternate items member path.</value>
        public string MatchingTermAlternateItemsMemberPath
        {
            get { return (string)GetValue(MatchingTermAlternateItemsMemberPathProperty); }
            set { SetValue(MatchingTermAlternateItemsMemberPathProperty, value); }
        }

        /// <summary>
        /// Gets or sets the matching term fly out content template.
        /// </summary>
        /// <value>The matching term fly out content template.</value>
        public DataTemplate MatchingTermFlyOutContentTemplate
        {
            get { return (DataTemplate)GetValue(MatchingTermFlyOutContentTemplateProperty); }
            set { SetValue(MatchingTermFlyOutContentTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the subset picker label text.
        /// </summary>
        /// <value>The subset picker label text.</value>
        public string SubsetPickerLabelText
        {
            get { return (string)GetValue(SubsetPickerLabelTextProperty); }
            set { SetValue(SubsetPickerLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the input box label text.
        /// </summary>
        /// <value>The input box label text.</value>
        public string InputBoxLabelText
        {
            get { return (string)GetValue(InputBoxLabelTextProperty); }
            set { SetValue(InputBoxLabelTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the search button on the input box is visible.
        /// </summary>
        /// <value>Whether the search button is visible.</value>
        public bool InputBoxIsSearchButtonVisible
        {
            get { return (bool)GetValue(InputBoxIsSearchButtonVisibleProperty); }
            set { SetValue(InputBoxIsSearchButtonVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the input box watermark.
        /// </summary>
        /// <value>The input box watermark.</value>
        public object InputBoxWatermark
        {
            get { return (object)GetValue(InputBoxWatermarkProperty); }
            set { SetValue(InputBoxWatermarkProperty, value); }
        }

        /// <summary>
        /// Gets or sets the input box watermark template.
        /// </summary>
        /// <value>The input box watermark template.</value>
        public DataTemplate InputBoxWatermarkTemplate
        {
            get { return (DataTemplate)GetValue(InputBoxWatermarkTemplateProperty); }
            set { SetValue(InputBoxWatermarkTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the fly out width.
        /// </summary>
        /// <value>The fly out width.</value>
        public double InputBoxFlyOutWidth
        {
            get { return (double)GetValue(InputBoxFlyOutWidthProperty); }
            set { SetValue(InputBoxFlyOutWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the input box search button tooltip.
        /// </summary>
        /// <value>The input box search button tooltip.</value>
        public object InputBoxSearchButtonToolTip
        {
            get { return (object)GetValue(InputBoxSearchButtonToolTipProperty); }
            set { SetValue(InputBoxSearchButtonToolTipProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the save button is enabled.
        /// </summary>
        /// <value>A value indicating whether the save button is enabled.</value>
        public bool IsSaveButtonEnabled
        {
            get { return (bool)GetValue(IsSaveButtonEnabledProperty); }
            set { SetValue(IsSaveButtonEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets the label style.
        /// </summary>
        /// <value>A text block style.</value>
        public Style LabelStyle
        {
            get { return (Style)GetValue(LabelStyleProperty); }
            set { SetValue(LabelStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the filter combo box selected index.
        /// </summary>
        /// <value>The filter combo box selected index.</value>
        public int SubsetPickerSelectedIndex
        {
            get
            {
                if (this.FilterComboBox != null)
                {
                    return this.FilterComboBox.SelectedIndex;
                }

                return (int)GetValue(SubsetPickerSelectedIndexProperty);
            }

            set
            {
                SetValue(SubsetPickerSelectedIndexProperty, value);
            }
        }
        #endregion

        #region EncodableInputBox public members
        /// <summary>
        /// Gets or sets the input box text.
        /// </summary>
        /// <value>The input box text value.</value>
        public string InputBoxText
        {
            get 
            { 
                if (this.EncodableInputBox != null)
                {
                    return this.EncodableInputBox.Text; 
                }

                return (string)GetValue(InputBoxTextProperty);
            }

            set 
            {
                if (this.EncodableInputBox != null)
                {
                    this.EncodableInputBox.Text = value;
                }

                SetValue(InputBoxTextProperty, value); 
            }
        }

        /// <summary>
        /// Gets or sets the input box selected item.
        /// </summary>
        /// <value>The input box's selected item.</value>
        public object InputBoxSelectedItem
        {
            get 
            {
                if (this.EncodableInputBox != null)
                {
                    return this.EncodableInputBox.SelectedItem;
                }

                return (object)GetValue(InputBoxSelectedItemProperty);
            }
            
            set 
            {
                if (this.EncodableInputBox != null)
                {
                    this.EncodableInputBox.SelectedItem = value;
                }

                SetValue(InputBoxSelectedItemProperty, value); 
            }
        }

        /// <summary>
        /// Gets or sets the input box's items source.
        /// </summary>
        /// <value>The input box's items source.</value>
        public IEnumerable InputBoxItemsSource
        {
            get 
            { 
                return (IEnumerable)GetValue(InputBoxItemsSourceProperty); 
            }
            
            set 
            {
                if (this.EncodableInputBox != null)
                {
                    this.EncodableInputBox.ItemsSource = value;
                }

                SetValue(InputBoxItemsSourceProperty, value); 
            }
        }

        /// <summary>
        /// Gets or sets the input box's item container style.
        /// </summary>
        /// <value>The input box's item container style.</value>
        public Style InputBoxItemContainerStyle
        {
            get 
            {
                return (Style)GetValue(InputBoxItemContainerStyleProperty); 
            }

            set
            {
                if (value.TargetType == typeof(ListBoxItem))
                {
                    SetValue(InputBoxItemContainerStyleProperty, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the input box's item template.
        /// </summary>
        /// <value>The input box's item template.</value>
        public DataTemplate InputBoxItemTemplate
        {
            get { return (DataTemplate)GetValue(InputBoxItemTemplateProperty); }
            set { SetValue(InputBoxItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the input box's encoded item template.
        /// </summary>
        /// <value>The input box encoded item template.</value>
        public DataTemplate InputBoxEncodedItemTemplate
        {
            get { return (DataTemplate)GetValue(InputBoxEncodedItemTemplateProperty); }
            set { SetValue(InputBoxEncodedItemTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the input box fly out template.
        /// </summary>
        /// <value>The input box fly out template.</value>
        public DataTemplate InputBoxFlyOutTemplate
        {
            get { return (DataTemplate)GetValue(InputBoxFlyOutTemplateProperty); }
            set { SetValue(InputBoxFlyOutTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the max length of the input box.
        /// </summary>
        /// <value>The max length for the input box.</value>
        public int InputBoxMaxLength
        {
            get { return (int)GetValue(InputBoxMaxLengthProperty); }
            set { SetValue(InputBoxMaxLengthProperty, value); }
        }
        #endregion

        #region Filter public members

        /// <summary>
        /// Gets or sets the subset picker items source.
        /// </summary>
        /// <value>The subset picker items source.</value>
        public IEnumerable SubsetPickerItemsSource
        {
            get { return (IEnumerable)GetValue(SubsetPickerItemsSourceProperty); }
            set { SetValue(SubsetPickerItemsSourceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the subset picker data template.
        /// </summary>
        /// <value>The subset picker data template.</value>
        public DataTemplate SubsetPickerDataTemplate
        {
            get { return (DataTemplate)GetValue(SubsetPickerDataTemplateProperty); }
            set { SetValue(SubsetPickerDataTemplateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the subset picker selected item.
        /// </summary>
        /// <value>The subset picker selected item.</value>
        public object SubsetPickerSelectedItem
        {
            get
            {
                if (this.FilterComboBox != null)
                {
                    return this.FilterComboBox.SelectedItem;
                }

                return (object)GetValue(SubsetPickerSelectedItemProperty);
            }

            set
            {                
                SetValue(SubsetPickerSelectedItemProperty, value);
            }
        }
        #endregion

        #region Template part properties
        /// <summary>
        /// Gets or sets the encodable input box.
        /// </summary>
        /// <value>The encodable input box.</value>
        private EncodableInputBox EncodableInputBox
        {
            get
            {
                return this.encodableInputBox;
            }

            set
            {
                if (this.encodableInputBox != null)
                {
                    this.encodableInputBox.SelectionChanged -= new EventHandler(this.EncodableInputBox_SelectionChanged);
                    this.encodableInputBox.SearchClicked -= new RoutedEventHandler(this.EncodableInputBox_SearchClicked);
                    this.encodableInputBox.EnterPressed -= new KeyEventHandler(this.EncodableInputBox_EnterPressed);
                    this.encodableInputBox.TextChanged -= new TextChangedEventHandler(this.EncodableInputBox_TextChanged);
                }

                this.encodableInputBox = value;

                if (this.encodableInputBox != null)
                {
                    this.encodableInputBox.SelectionChanged += new EventHandler(this.EncodableInputBox_SelectionChanged);
                    this.encodableInputBox.SearchClicked += new RoutedEventHandler(this.EncodableInputBox_SearchClicked);
                    this.encodableInputBox.EnterPressed += new KeyEventHandler(this.EncodableInputBox_EnterPressed);
                    this.encodableInputBox.TextChanged += new TextChangedEventHandler(this.EncodableInputBox_TextChanged);
                }
            }
        }

        /// <summary>
        /// Gets or sets the decorator text box.
        /// </summary>
        /// <value>The decorator text box.</value>
        private DecoratorTextBox DecoratorTextBox
        {
            get
            {
                return this.decoratorTextBox;
            }

            set
            {
                if (this.decoratorTextBox != null)
                {
                    this.decoratorTextBox.TextChanged -= new TextChangedEventHandler(this.DecoratorTextBoxTextChanged);
                    this.DecoratorTextBox.ItemMouseEnter -= new MouseEventHandler(this.DecoratorTextBox_ItemMouseEnter);
                    this.DecoratorTextBox.ItemMouseLeave -= new MouseEventHandler(this.DecoratorTextBox_ItemMouseLeave);
                    this.DecoratorTextBox.EnterPressed -= new KeyEventHandler(this.DecoratorTextBox_EnterPressed);
                }

                this.decoratorTextBox = value;

                if (this.decoratorTextBox != null)
                {
                    this.DecoratorTextBox.TextChanged += new TextChangedEventHandler(this.DecoratorTextBoxTextChanged);
                    this.DecoratorTextBox.ItemMouseEnter += new MouseEventHandler(this.DecoratorTextBox_ItemMouseEnter);
                    this.DecoratorTextBox.ItemMouseLeave += new MouseEventHandler(this.DecoratorTextBox_ItemMouseLeave);
                    this.DecoratorTextBox.EnterPressed += new KeyEventHandler(this.DecoratorTextBox_EnterPressed);
                    SetValue(AdditionalTextBoxDisplayedMatchingTermsProperty, this.decoratorTextBox.DisplayedMatchingTerms);
                }
            }
        }

        /// <summary>
        /// Gets or sets the save button.
        /// </summary>
        /// <value>The save button.</value>
        private Button SaveButton
        {
            get
            {
                return this.saveButton;
            }

            set
            {
                if (this.saveButton != null)
                {
                    this.SaveButton.Click -= new RoutedEventHandler(this.SaveButton_Click);
                }

                this.saveButton = value;

                if (this.saveButton != null)
                {
                    this.SaveButton.Click += new RoutedEventHandler(this.SaveButton_Click);
                }
            }
        }

        /// <summary>
        /// Gets or sets the clear button.
        /// </summary>
        /// <value>The clear button.</value>
        private Button ClearButton
        {
            get
            {
                return this.clearButton;
            }

            set
            {
                if (this.clearButton != null)
                {
                    this.ClearButton.Click -= new RoutedEventHandler(this.ClearButton_Click);
                }

                this.clearButton = value;

                if (this.clearButton != null)
                {
                    this.ClearButton.Click += new RoutedEventHandler(this.ClearButton_Click);
                }
            }
        }

        /// <summary>
        /// Gets or sets the filter ComboBox.
        /// </summary>
        /// <value>The cancel button.</value>
        private System.Windows.Controls.ComboBox FilterComboBox
        {
            get
            {
                return this.filterComboBox;
            }

            set
            {
                if (this.filterComboBox != null)
                {
                    this.FilterComboBox.SelectionChanged -= new SelectionChangedEventHandler(this.FilterComboBoxSelectionChanged);
                }

                this.filterComboBox = value;

                if (this.FilterComboBox != null)
                {
#if SILVERLIGHT
                    // Required to address text wrapping issue in Silverlight 2.
                    this.FilterComboBox.UseLayoutRounding = false;
                    if (this.FilterComboBox.ItemContainerStyle == null)
                    {
                        this.FilterComboBox.ItemContainerStyle = new Style(typeof(ComboBoxItem));                        
                    }

                    this.FilterComboBox.ItemContainerStyle.Setters.Add(new Setter(UIElement.UseLayoutRoundingProperty, false));
#endif
                    this.FilterComboBox.SelectionChanged += new SelectionChangedEventHandler(this.FilterComboBoxSelectionChanged);
                }
            }
        }

        /// <summary>
        /// Gets or sets the filter ComboBox grid.
        /// </summary>
        /// <value>The filter ComboxBox grid.</value>
        private Grid FilterComboBoxGrid
        {
            get { return this.filterComboBoxGrid; }
            set { this.filterComboBoxGrid = value; }
        }

        /// <summary>
        /// Gets or sets the Matching Term Items Control.
        /// </summary>
        /// <value>The matching term items control.</value>
        private MatchingTermItemsControl MatchingTermItemsControl
        {
            get
            {
                return this.matchingTermItemsControl;
            }

            set
            {
                if (this.matchingTermItemsControl != null)
                {
                    this.matchingTermItemsControl.ItemMouseEnter -= new MouseEventHandler(this.MatchingTermItemsControl_ItemMouseEnter);
                    this.matchingTermItemsControl.ItemMouseLeave -= new MouseEventHandler(this.MatchingTermItemsControl_ItemMouseLeave);
                    this.matchingTermItemsControl.ItemGotFocus -= new RoutedEventHandler(this.MatchingTermItemsControl_ItemGotFocus);
                    this.matchingTermItemsControl.ItemLostFocus -= new RoutedEventHandler(this.MatchingTermItemsControl_ItemLostFocus);
                    this.matchingTermItemsControl.ItemSelectionChanged -= new EventHandler(this.MatchingTermItemsControl_ItemSelectionChanged);
                }

                this.matchingTermItemsControl = value;

                if (this.matchingTermItemsControl != null)
                {
                    this.matchingTermItemsControl.ItemMouseEnter += new MouseEventHandler(this.MatchingTermItemsControl_ItemMouseEnter);
                    this.matchingTermItemsControl.ItemMouseLeave += new MouseEventHandler(this.MatchingTermItemsControl_ItemMouseLeave);
                    this.matchingTermItemsControl.ItemGotFocus += new RoutedEventHandler(this.MatchingTermItemsControl_ItemGotFocus);
                    this.matchingTermItemsControl.ItemLostFocus += new RoutedEventHandler(this.MatchingTermItemsControl_ItemLostFocus);
                    this.matchingTermItemsControl.ItemSelectionChanged += new EventHandler(this.MatchingTermItemsControl_ItemSelectionChanged);
                }
            }
        }
        #endregion

        #region SingleConceptMatching public methods
        /// <summary>
        /// Clears the fields in the control.
        /// </summary>
        public void Clear()
        {
            if (this.templatePartsLoaded)
            {
                if (this.FilterComboBox.Items.Count > 0)
                {
                    this.FilterComboBox.SelectedIndex = 0;
                }

                this.InputBoxSelectedItem = null;
                this.InputBoxText = string.Empty;
                this.InputBoxItemsSource = null;
                this.AdditionalTextBoxText = string.Empty;
            }
        }

        /// <summary>
        /// Gives focus to the input box.
        /// </summary>
        public void FocusInputBox()
        {
            if (this.EncodableInputBox != null)
            {
                this.EncodableInputBox.Focus();
            }
        }

        /// <summary>
        /// Gives focus to the additional text box.
        /// </summary>
        public void FocusAdditionalTextBox()
        {
            if (this.DecoratorTextBox != null)
            {
                this.DecoratorTextBox.Focus();
            }
        }
        #endregion

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.EncodableInputBox = this.GetTemplateChild(SingleConceptMatching.ElementEncodableInputBox) as EncodableInputBox;
            
            this.DecoratorTextBox = this.GetTemplateChild(SingleConceptMatching.ElementDecoratorTextBox) as DecoratorTextBox;
            this.SaveButton = this.GetTemplateChild(SingleConceptMatching.ElementSaveButton) as Button;
            this.ClearButton = this.GetTemplateChild(SingleConceptMatching.ElementClearButton) as Button;
            this.FilterComboBox = this.GetTemplateChild(SingleConceptMatching.ElementFilterComboBox) as System.Windows.Controls.ComboBox;
            this.MatchingTermItemsControl = this.GetTemplateChild(SingleConceptMatching.ElementMatchingTermItemsControl) as MatchingTermItemsControl;
            this.FilterComboBoxGrid = this.GetTemplateChild(SingleConceptMatching.ElementFilterComboBoxGrid) as Grid;
            this.templatePartsLoaded = true;
        }

        /// <summary>
        /// Creates a new filter ComboBox.
        /// </summary>
        /// <param name="dependencyObject">The single concept matching control.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void SubsetPickerItemsSource_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            SingleConceptMatching singleConceptMatching = (SingleConceptMatching)dependencyObject;
            singleConceptMatching.CreateNewFilterComboBox();
        }

        /// <summary>
        /// Updates the Filter ComboBox data template.
        /// </summary>
        /// <param name="dependencyObject">The single concept matching control.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void SubsetPickerDataTemplate_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            SingleConceptMatching singleConceptMatching = (SingleConceptMatching)dependencyObject;
            if (singleConceptMatching.FilterComboBox != null)
            {
                singleConceptMatching.FilterComboBox.ItemTemplate = (DataTemplate)eventArgs.NewValue;
            }
        }

        /// <summary>
        /// Updates the Filter SelectedIndex data template.
        /// </summary>
        /// <param name="dependencyObject">The single concept matching control.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void SubsetPickerSelectedIndex_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            SingleConceptMatching singleConceptMatching = (SingleConceptMatching)dependencyObject;
            if (singleConceptMatching.FilterComboBox != null)
            {
                singleConceptMatching.FilterComboBox.SelectedIndex = (int)eventArgs.NewValue;
            }
        }

        /// <summary>
        /// Updates the Filter SelectedIndex data template.
        /// </summary>
        /// <param name="dependencyObject">The single concept matching control.</param>
        /// <param name="eventArgs">Dependency Property Changed Event Args.</param>
        private static void SubsetPickerSelectedItem_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            SingleConceptMatching singleConceptMatching = (SingleConceptMatching)dependencyObject;
            if (singleConceptMatching.FilterComboBox != null)
            {
                singleConceptMatching.FilterComboBox.SelectedItem = eventArgs.NewValue;
            }
        }

        /// <summary>
        /// Creates a new FilterComboBox. The control generates a new ComboBox each time due to a restriction
        /// in Silverlight 2, that only allows the drop down size to be set once.
        /// </summary>
        private void CreateNewFilterComboBox()
        {
            if (this.FilterComboBoxGrid != null)
            {
                this.FilterComboBoxGrid.Children.Clear();
                this.FilterComboBox = null;
                System.Windows.Controls.ComboBox newFilterComboBox = new System.Windows.Controls.ComboBox();
                newFilterComboBox.MinHeight = 25;
                newFilterComboBox.ItemsSource = this.SubsetPickerItemsSource;
                
                if (this.SubsetPickerDataTemplate != null)
                {
                    newFilterComboBox.ItemTemplate = this.SubsetPickerDataTemplate;
                }

                newFilterComboBox.Loaded += new RoutedEventHandler(this.NewFilterComboBox_Loaded);
                this.FilterComboBoxGrid.Children.Add(newFilterComboBox);
                this.FilterComboBox = newFilterComboBox;
            }
        }

        /// <summary>
        /// Sets the filter combo box selected item.
        /// </summary>
        /// <param name="sender">The new filter combo box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void NewFilterComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.FilterComboBox != null)
            {
                if (this.SubsetPickerSelectedItem != null)
                {
                    this.FilterComboBox.SelectedItem = this.SubsetPickerSelectedItem;
                }
                else if (this.SubsetPickerSelectedIndex >= 0)
                {
                    this.FilterComboBox.SelectedIndex = this.SubsetPickerSelectedIndex;
                }
                else if (this.FilterComboBox.Items.Count > 0)
                {
                    this.FilterComboBox.SelectedIndex = 0;
                }
            }
        }

        #region DecoratorTextBox event handlers
        /// <summary>
        /// Raises the AdditionalTextChanged event.
        /// </summary>
        /// <param name="sender">The additional text box.</param>
        /// <param name="e">Text Changed Event Args.</param>
        private void DecoratorTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.AdditionalTextBoxTextChanged != null)
            {
                this.AdditionalTextBoxTextChanged(this, e);
            }
        }

        /// <summary>
        /// Highlights the collection of terms under the mouse.
        /// </summary>
        /// <param name="sender">The collection of terms.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void DecoratorTextBox_ItemMouseEnter(object sender, MouseEventArgs e)
        {
            this.HighlightTermsInMatchingTermItemsControl((List<object>)sender, true);
        }

        /// <summary>
        /// Unhighlights the collection of terms that were under the mouse.
        /// </summary>
        /// <param name="sender">The collection of terms.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void DecoratorTextBox_ItemMouseLeave(object sender, MouseEventArgs e)
        {
            this.HighlightTermsInMatchingTermItemsControl((List<object>)sender, false);
        }

        /// <summary>
        /// Raises the additional text box enter pressed event.
        /// </summary>
        /// <param name="sender">The decorator text box.</param>
        /// <param name="e">Key Event Args.</param>
        private void DecoratorTextBox_EnterPressed(object sender, KeyEventArgs e)
        {
            if (this.AdditionalTextBoxEnterPressed != null)
            {
                this.AdditionalTextBoxEnterPressed(this, e);
            }
        }

        /// <summary>
        /// Sets an item highlight in the matching term items control.
        /// </summary>
        /// <param name="terms">The term item.</param>
        /// <param name="highlighted">Whether the item is highlighted or not.</param>
        private void HighlightTermsInMatchingTermItemsControl(List<object> terms, bool highlighted)
        {
            if (this.MatchingTermItemsControl != null)
            {
                foreach (object term in terms)
                {
                    this.MatchingTermItemsControl.SetTermHighlight(term, highlighted);
                }
            }
        }
        #endregion

        #region MatchingTermItemsControl event handlers
        /// <summary>
        /// Updates the text in the decorator text box.
        /// </summary>
        /// <param name="sender">The changed item.</param>
        /// <param name="e">Event Args.</param>
        private void MatchingTermItemsControl_ItemSelectionChanged(object sender, EventArgs e)
        {
            if (this.DecoratorTextBox != null)
            {
                this.DecoratorTextBox.ChangeText((MatchingTermItemContainer)sender);
            }   
        }

        /// <summary>
        /// Highlights the corresponding item (from under the mouse) in the text box.
        /// </summary>
        /// <param name="sender">The source item.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void MatchingTermItemsControl_ItemMouseEnter(object sender, MouseEventArgs e)
        {
            this.MouseHighlightTermInDecoratorTextBox(((FrameworkElement)sender).DataContext, true);
        }

        /// <summary>
        /// Unhighlights the corresponding item (from under the mouse) in the text box.
        /// </summary>
        /// <param name="sender">The source item.</param>
        /// <param name="e">Mouse Event Args.</param>
        private void MatchingTermItemsControl_ItemMouseLeave(object sender, MouseEventArgs e)
        {
            this.MouseHighlightTermInDecoratorTextBox(((FrameworkElement)sender).DataContext, false);
        }

        /// <summary>
        /// Highlights the corresponding item (that has focus) in the text box.
        /// </summary>
        /// <param name="sender">The source item.</param>
        /// <param name="e">Routed Event Args.</param>
        private void MatchingTermItemsControl_ItemGotFocus(object sender, RoutedEventArgs e)
        {
            this.FocusHighlightTermInDecoratorTextBox(((FrameworkElement)sender).DataContext, true);
        }

        /// <summary>
        /// Unhighlights the corresponding item (that has focus) in the text box.
        /// </summary>
        /// <param name="sender">The source item.</param>
        /// <param name="e">Routed Event Args.</param>
        private void MatchingTermItemsControl_ItemLostFocus(object sender, RoutedEventArgs e)
        {
            object term = ((FrameworkElement)sender).DataContext;
            if (this.DecoratorTextBox != null && !this.DecoratorTextBox.TermUnderMouse(term))
            {
                this.FocusHighlightTermInDecoratorTextBox(term, false);
            }
        }

        /// <summary>
        /// Sets an item mouse highlight in the decorator text box.
        /// </summary>
        /// <param name="term">The term item.</param>
        /// <param name="highlighted">Whether the item is highlighted or not.</param>
        private void MouseHighlightTermInDecoratorTextBox(object term, bool highlighted)
        {
            if (this.DecoratorTextBox != null)
            {
                this.DecoratorTextBox.SetTermMouseHighlight(term, highlighted);
            }
        }

        /// <summary>
        /// Sets an item focus highlight in the decorator text box.
        /// </summary>
        /// <param name="term">The term item.</param>
        /// <param name="highlighted">Whether the item is highlighted or not.</param>
        private void FocusHighlightTermInDecoratorTextBox(object term, bool highlighted)
        {
            if (this.DecoratorTextBox != null)
            {
                this.DecoratorTextBox.SetTermFocusHighlight(term, highlighted);
            }
        }
        #endregion

        #region EncodableInputBox event handlers
        /// <summary>
        /// Raises the input box text changed event.
        /// </summary>
        /// <param name="sender">The encodable input box.</param>
        /// <param name="e">Key Event Args.</param>
        private void EncodableInputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.DecoratorTextBox != null)
            {
                this.DecoratorTextBox.IsEnabled = !string.IsNullOrEmpty(this.EncodableInputBox.Text.Trim());
                this.DecoratorTextBox.MatchingTermItemsSource = null;
            }

            if (this.SaveButton != null)
            {
                this.IsSaveButtonEnabled = !string.IsNullOrEmpty(this.EncodableInputBox.Text.Trim());
            }

            if (this.InputBoxTextChanged != null)
            {
                this.InputBoxTextChanged(this, e);
            }
        }

        /// <summary>
        /// Raises the input box enter pressed event.
        /// </summary>
        /// <param name="sender">The encodable input box.</param>
        /// <param name="e">Key Event Args.</param>
        private void EncodableInputBox_EnterPressed(object sender, KeyEventArgs e)
        {
            if (this.InputBoxEnterPressed != null)
            {
                this.InputBoxEnterPressed(this, e);
            }
        }

        /// <summary>
        /// Raises the input box search clicked event.
        /// </summary>
        /// <param name="sender">The encodable input box.</param>
        /// <param name="e">Routed Event Args.</param>
        private void EncodableInputBox_SearchClicked(object sender, RoutedEventArgs e)
        {
            if (this.InputBoxSearchButtonClicked != null)
            {
                this.InputBoxSearchButtonClicked(this, e);
            }
        }

        /// <summary>
        /// Raises the input box selection changed event.
        /// </summary>
        /// <param name="sender">The encodable input box.</param>
        /// <param name="e">Event Args.</param>
        private void EncodableInputBox_SelectionChanged(object sender, EventArgs e)
        {
            if (this.DecoratorTextBox != null)
            {
                this.DecoratorTextBox.IsEnabled = (this.EncodableInputBox.SelectedItem != null || !string.IsNullOrEmpty(this.EncodableInputBox.Text));
                this.DecoratorTextBox.MatchingTermItemsSource = null;
            }

            if (this.SaveButton != null)
            {
                this.IsSaveButtonEnabled = (this.EncodableInputBox.SelectedItem != null || !string.IsNullOrEmpty(this.EncodableInputBox.Text));
            }

            if (this.InputBoxSelectionChanged != null)
            {
                this.InputBoxSelectionChanged(this, e);
            }
        }
        #endregion

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Saved != null)
            {
                this.Saved(this, e);
            }
        }

        /// <summary>
        /// Handles the Click event of the ClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            this.Clear();
            this.FocusInputBox();
            if (this.Cleared != null)
            {
                this.Cleared(this, e);
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the FilterComboBox control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void FilterComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.FilterSelectedIndexChanged != null)
            {
                this.FilterSelectedIndexChanged(this, e);
            }
        }
    }
}
