//-----------------------------------------------------------------------
// <copyright file="SingleConceptMatchingPage.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010..
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
// <summary>Sample page to host Single Concept Matching.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.SamplePages
{
    #region Using

    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Threading;    
    using Microsoft.Cui.SamplePages.TerminologyProvider;
    #endregion
    /// <summary>
    /// Sample page to host Single Concept Matching.
    /// </summary>
    public partial class SingleConceptMatchingPage : UserControl
    {
        /// <summary>
        /// The minimum number of characters required before a search will be carried out.
        /// </summary>
        private const int MinimumCharactersForSearch = 3;

        /// <summary>
        /// No matches found text.
        /// </summary>
        private const string NoMatchesFoundText = "No matches found.";

        /// <summary>
        /// More matches found.
        /// </summary>
        private const string MoreMatchesFoundText = "More than {0} matches found.";

        /// <summary>
        /// Matches found.
        /// </summary>
        private const string MatchesFound = "{0} matches found.";

        /// <summary>
        /// No matches found due to an error text.
        /// </summary>
        private const string NoMatchesFoundDueToErrorText = "Error, unable to retrieve results.";

        /// <summary>
        /// Connection Error Text.
        /// </summary>
        private const string ConnectionErrorText = "Terminology service not available.";

        /// <summary>
        /// Search in progress text.
        /// </summary>
        private const string SearchInProgressText = "Search in progress.";

        /// <summary>
        /// Saving encoded term text.
        /// </summary>
        private const string SavingEncodedTermText = "Saving encoded term.";

        /// <summary>
        /// Initializing text.
        /// </summary>
        private const string InitializingText = "Initializing.";

        /// <summary>
        /// Escape Strings.
        /// </summary>
        private static string[] escapeStrings = new string[] { " ", "+", "-", "&&", "||", "!", "(", ")", "{", "}", "[", "]", "^", "\"", "~", "*", "?", ":", "\\" };

        /// <summary>
        /// Search Mode Enum.
        /// </summary>
        private static SingleConceptMatchingPage.SearchMode searchMode = SearchMode.UserInitiated;

        /// <summary>
        /// Timer used to call out to Data Provider to process input field string.
        /// </summary>
        private DispatcherTimer encodableInputFieldTimer = new DispatcherTimer();

        /// <summary>
        /// Outputs list.
        /// </summary>
        private ObservableCollection<SingleConceptMatchingOutput> outputs = new ObservableCollection<SingleConceptMatchingOutput>();

        /// <summary>
        /// Timer used to call out to Data Provider to process additionalTextBox field string.
        /// </summary>
        private DispatcherTimer additionalTextBoxTimer = new DispatcherTimer();

        /// <summary>
        /// Indicates if we are connected to a TerminologyProvider, if not act like a dumb control.
        /// </summary>
        private bool connected;

        /// <summary>
        /// Stores the last input box search.
        /// </summary>
        private string lastInputBoxSearch = string.Empty;

        /// <summary>
        /// Stores the last input box results.
        /// </summary>
        private InputFieldSearchCompletedEventArgs lastInputBoxResults;

        /// <summary>
        /// Stores the last additional text box search.
        /// </summary>
        private string lastAdditionalTextBoxSearch = string.Empty;

        /// <summary>
        /// Stores the last additional text box results.
        /// </summary>
        private AdditionalTextBoxParseCompletedEventArgs lastAdditionalTextBoxResults;

        /// <summary>
        /// Stores the number of calls to the indexer.
        /// </summary>
        private int indexerCallCount;

        /// <summary>
        /// A Concept Details object for the currently selected term.
        /// </summary>
        private ConceptDetail conceptDetail = new ConceptDetail();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleConceptMatchingPage"/> class.
        /// </summary>
        public SingleConceptMatchingPage()
        {
            InitializeComponent();

            TerminologyManager.Initialized += new EventHandler<BaseTerminologyEventArgs>(this.TerminologyManagerInitialized);
            TerminologyManager.InputBoxSearchCompleted += new EventHandler<InputFieldSearchCompletedEventArgs>(this.TerminologyManagerInputBoxSearchCompleted);
            TerminologyManager.AdditionalTextBoxParseCompleted += new EventHandler<AdditionalTextBoxParseCompletedEventArgs>(this.TerminologyManagerAdditionalTextBoxParseCompleted);
            TerminologyManager.EncodeConceptCompleted += new EventHandler<EncodeConceptCompletedEventArgs>(this.TerminologyManagerEncodeConceptCompleted);
            TerminologyManager.GetConceptDetailsCompleted += new EventHandler<GetConceptDetailsCompletedEventArgs>(this.TerminologyManagerGetConceptDetailsCompleted);

            this.StatusText.Text = SingleConceptMatchingPage.InitializingText;
            this.ShowProgressBar();
            TerminologyManager.Initialize();

            this.SearchTypeComboBox.Loaded += new System.Windows.RoutedEventHandler(this.SearchTypeComboBox_Loaded);
            this.ScenarioComboBox.Loaded += new System.Windows.RoutedEventHandler(this.ScenarioComboBox_Loaded);

            this.encodableInputFieldTimer.Interval = TimeSpan.FromMilliseconds(500);
            this.encodableInputFieldTimer.Tick += new System.EventHandler(this.EncodableInputFieldTimer_Tick);

            this.additionalTextBoxTimer.Interval = TimeSpan.FromMilliseconds(2000);
            this.additionalTextBoxTimer.Tick += new EventHandler(this.AdditionalTextBoxTimerTick);

            this.outputs.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(this.Outputs_CollectionChanged);
            this.OutputListBox.ItemsSource = this.outputs;

            this.StatusText.UseLayoutRounding = false;
            this.SelectedOutputContentPresenter.Content = new SingleConceptMatchingOutput();
        }                 

        /// <summary>
        /// Search mode enumeration.
        /// </summary>
        private enum SearchMode
        {
            /// <summary>
            /// Submit a search on a timer tick.
            /// </summary>
            Progressive,

            /// <summary>
            /// Submit a search on a user initiated action.
            /// </summary>
            UserInitiated
        }

        /// <summary>
        /// Gets the Input Text Validation Failed text.
        /// </summary>
        /// <value>The input text validation failed.</value>
        public static string InputTextValidationFailed
        {
            get
            {
                return string.Format(
                    System.Globalization.CultureInfo.CurrentCulture,
                    "A minimum of {0} alpha-numeric characters in a single word is needed.",
                    SingleConceptMatchingPage.MinimumCharactersForSearch);
            }
        }  

        /// <summary>
        /// Creates an unencoded concept.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <returns>An EncodedConcept object which contains only a display name.  Used to represent text which hasn't been found through the terminology provider.</returns>
        private static EncodedConcept CreateUnencodedConcept(string displayName)
        {
            return new EncodedConcept()
            {
                DisplayName = "\"" + displayName + "\" (Unencoded Concept)"
            };
        }

        /// <summary>
        /// Validates the input string.
        /// </summary>
        /// <param name="inputText">The input text.</param>
        /// <returns>True if input text is valid, otherwise false.</returns>
        private static bool ValidateInputText(string inputText)
        {
            string[] nakedStrings = inputText.Split(SingleConceptMatchingPage.escapeStrings, StringSplitOptions.RemoveEmptyEntries);
            
            if (nakedStrings.Length == 0 || nakedStrings.Max(p => p.Length) < SingleConceptMatchingPage.MinimumCharactersForSearch)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Escapes all the characters/strings in the input text .
        /// </summary>
        /// <param name="inputText">The input text.</param>
        /// <returns>An escaped character string.</returns>
        private static string FixupInputText(string inputText)
        {
            foreach (string s in SingleConceptMatchingPage.escapeStrings)
            {
                if (!string.IsNullOrEmpty(s.Trim()))
                {
                    inputText = inputText.Replace(s, string.Empty);
                }
            }

            return inputText;
        }
    
        /// <summary>
        /// The handler for the Terminology Manager objects 'Initialized' event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="BaseTerminologyEventArgs"/> instance containing the event data.</param>
        private void TerminologyManagerInitialized(object sender, BaseTerminologyEventArgs e)
        {
            if (e.Successful)
            {
                this.connected = true;
                this.StatusText.Text = string.Empty;
            }
            else
            {
                this.StatusText.Text = SingleConceptMatchingPage.ConnectionErrorText;
            }
            
            this.SingleConceptMatching.IsEnabled = true;
            this.HideProgressBar();
        }  

        /// <summary>
        /// Handles the input box search completed event of the Terminology Manager.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="InputFieldSearchCompletedEventArgs"/> instance containing the event data.</param>
        private void TerminologyManagerInputBoxSearchCompleted(object sender, InputFieldSearchCompletedEventArgs e)
        {
            // Check search is for the latest value in the text box
            if (SingleConceptMatchingPage.FixupInputText(this.SingleConceptMatching.InputBoxText) != e.SearchTextOriginal)
            {
                return;
            }

            if (e.Successful)
            {
                if (e.InputFieldResults != null && e.InputFieldResults.Count > 0)
                {
                    this.lastInputBoxResults = e;
                    this.SingleConceptMatching.InputBoxItemsSource = e.InputFieldResults;

                    if (e.ExceedsMaxTotal)
                    {
                        this.StatusText.Text = string.Format(System.Globalization.CultureInfo.CurrentCulture, SingleConceptMatchingPage.MoreMatchesFoundText, e.InputFieldResults.Count);
                    }
                    else
                    {
                        this.StatusText.Text = string.Format(System.Globalization.CultureInfo.CurrentCulture, SingleConceptMatchingPage.MatchesFound, e.InputFieldResults.Count);
                    }
                }
                else
                {
                    this.StatusText.Text = SingleConceptMatchingPage.NoMatchesFoundText;
                }                
            }
            else
            {                
                this.StatusText.Text = SingleConceptMatchingPage.NoMatchesFoundDueToErrorText;
            }

            this.HideProgressBar();
        }        

        /// <summary>
        /// Handles the additional text box parse completed event of the Terminology Manager.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AdditionalTextBoxParseCompletedEventArgs"/> instance containing the event data.</param>
        private void TerminologyManagerAdditionalTextBoxParseCompleted(object sender, AdditionalTextBoxParseCompletedEventArgs e)
        {
            this.indexerCallCount--;

            if (this.indexerCallCount == 0)
            {
                this.HideProgressBar();
                this.StatusText.Text = string.Empty;
            }

            if (this.SingleConceptMatching.AdditionalTextBoxText != e.SearchTextOriginal || this.SingleConceptMatching.InputBoxSelectedItem == null || ((InputFieldResult)this.SingleConceptMatching.InputBoxSelectedItem).Concept.SnomedConceptId != e.MainConceptId)
            {
                return;
            }

            if (e.Successful)
            {
                if (e.AdditionalTextBoxResults.Count > 0)
                {
                    ObservableCollection<AdditionalTextBoxResult> results = new ObservableCollection<AdditionalTextBoxResult>();
                    foreach (AdditionalTextBoxResult result in e.AdditionalTextBoxResults)
                    {
                        results.Add(this.GetMatchingAdditionalTextBoxResultsFromLastResults(result));
                    }

                    this.lastAdditionalTextBoxResults = new AdditionalTextBoxParseCompletedEventArgs(
                        e.SearchTextOriginal,
                        results,
                        e.MainConceptId);
                    
                    this.SingleConceptMatching.AdditionalTextBoxMatchingTermItemsSource = results;
                    this.SingleConceptMatching.FocusAdditionalTextBox();
                    this.StatusText.Text = string.Empty;
                }
                else
                {
                    this.StatusText.Text = SingleConceptMatchingPage.NoMatchesFoundText;
                }
            }
            else
            {
                this.StatusText.Text = SingleConceptMatchingPage.NoMatchesFoundDueToErrorText;                
            }            
        }

        /// <summary>
        /// Handles the GetConceptDetailsCompleted event from TerminologyManager.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="Microsoft.Cui.SamplePages.TerminologyProvider.GetConceptDetailsCompletedEventArgs"/> instance containing the event data.</param>
        private void TerminologyManagerGetConceptDetailsCompleted(object sender, GetConceptDetailsCompletedEventArgs e)
        {
            this.conceptDetail = e.Result;

            if (this.SingleConceptMatching.InputBoxSelectedItem != null && ValidateInputText(this.SingleConceptMatching.AdditionalTextBoxText))
            {
                this.lastAdditionalTextBoxSearch = this.SingleConceptMatching.AdditionalTextBoxText;
                if (this.connected)
                {
                    this.StatusText.Text = SingleConceptMatchingPage.SearchInProgressText;
                    this.ShowProgressBar();
                    this.indexerCallCount++;
                    TerminologyManager.ParseAdditionalTextBox(this.SingleConceptMatching.AdditionalTextBoxText, this.SingleConceptMatching.InputBoxSelectedItem as InputFieldResult, this.conceptDetail);
                }
            }
        }

        /// <summary>
        /// Checks to see if a qualifier result was in the previous results.
        /// </summary>
        /// <param name="result">The result to check.</param>
        /// <returns>Either the old or new result.</returns>
        private AdditionalTextBoxResult GetMatchingAdditionalTextBoxResultsFromLastResults(AdditionalTextBoxResult result)
        {
            if (this.lastAdditionalTextBoxResults != null)
            {
                foreach (AdditionalTextBoxResult lastResult in this.lastAdditionalTextBoxResults.AdditionalTextBoxResults)
                {
                    if (lastResult.SelectedItem.SnomedConceptId == result.SelectedItem.SnomedConceptId && lastResult.StartIndex == result.StartIndex && lastResult.Length == result.Length)
                    {
                        return lastResult;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Handles the encode completed event of the Terminology Manager.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EncodeConceptCompletedEventArgs"/> instance containing the event data.</param>
        private void TerminologyManagerEncodeConceptCompleted(object sender, EncodeConceptCompletedEventArgs e)
        {
            if (e.Successful)
            {
                this.StatusText.Text = string.Empty;
                this.EncodeOutput(e.EncodedConcept);
            }
            else
            {
                this.StatusText.Text = NoMatchesFoundDueToErrorText;
                this.EncodeOutput(SingleConceptMatchingPage.CreateUnencodedConcept(this.SingleConceptMatching.InputBoxText));
            }            
        }

        /// <summary>
        /// Encodes the output to the single concept matcher.
        /// </summary>
        /// <param name="encodedConcept">The encoded concept.</param>
        private void EncodeOutput(EncodedConcept encodedConcept)
        {
            SingleConceptMatchingOutput output = new SingleConceptMatchingOutput()
            {
                OriginalInputFieldText = this.SingleConceptMatching.InputBoxText,
                OriginalAdditionalText = this.SingleConceptMatching.AdditionalTextBoxText,
                EncodedConcept = encodedConcept
            };

            this.outputs.Add(output);
            this.OutputListBox.SelectedIndex = this.OutputListBox.Items.Count - 1;

            this.SingleConceptMatching.Clear();
            this.SingleConceptMatching.IsEnabled = true;
            this.SingleConceptMatching.FocusInputBox();

            this.HideProgressBar();
        }  

        /// <summary>
        /// Handles the Tick event of the encodableInputFieldTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EncodableInputFieldTimer_Tick(object sender, System.EventArgs e)
        {
            this.encodableInputFieldTimer.Stop();

            this.Search();
        }

        /// <summary>
        /// Handles the Tick event of the additionalTextBoxTimer control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AdditionalTextBoxTimerTick(object sender, EventArgs e)
        {
            this.additionalTextBoxTimer.Stop();

            if (this.SingleConceptMatching.InputBoxSelectedItem != null && ValidateInputText(this.SingleConceptMatching.AdditionalTextBoxText) && this.SingleConceptMatching.AdditionalTextBoxText != this.lastAdditionalTextBoxSearch)
            {
                this.lastAdditionalTextBoxSearch = this.SingleConceptMatching.AdditionalTextBoxText;
                this.ParseAdditionalText();
            }
            else if (this.lastAdditionalTextBoxResults != null && this.SingleConceptMatching.AdditionalTextBoxText == this.lastAdditionalTextBoxSearch && this.SingleConceptMatching.AdditionalTextBoxText == this.lastAdditionalTextBoxResults.SearchTextOriginal)
            {
                this.SingleConceptMatching.AdditionalTextBoxMatchingTermItemsSource = null;
                this.SingleConceptMatching.AdditionalTextBoxMatchingTermItemsSource = this.lastAdditionalTextBoxResults.AdditionalTextBoxResults;
            }
        }

        /// <summary>
        /// Parses the additional text.
        /// </summary>
        private void ParseAdditionalText()
        {
            if (this.connected)
            {
                this.StatusText.Text = SingleConceptMatchingPage.SearchInProgressText;
                this.ShowProgressBar();
                this.indexerCallCount++;
                this.lastAdditionalTextBoxSearch = this.SingleConceptMatching.AdditionalTextBoxText;

                if (this.conceptDetail.SnomedConceptId == (this.SingleConceptMatching.InputBoxSelectedItem as InputFieldResult).Concept.SnomedConceptId)
                {
                    TerminologyManager.ParseAdditionalTextBox(this.SingleConceptMatching.AdditionalTextBoxText, this.SingleConceptMatching.InputBoxSelectedItem as InputFieldResult, this.conceptDetail);
                }
                else
                {
                    TerminologyManager.GetConceptDetails((this.SingleConceptMatching.InputBoxSelectedItem as InputFieldResult).SnomedDescriptionId);
                }
            }
        } 

        /// <summary>
        /// Handles the InputFieldTextChanged event of the SingleConceptMatching control.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.TextChangedEventArgs"/> instance containing the event data.</param>
        private void SingleConceptMatching_InputBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            this.SingleConceptMatching.InputBoxItemsSource = null;
            this.encodableInputFieldTimer.Stop();

            if (this.connected)
            {
                this.StatusText.Text = string.Empty;
            }

            this.HideProgressBar();

            if (!SingleConceptMatchingPage.ValidateInputText(this.SingleConceptMatching.InputBoxText))
            {
                return;
            }            

            switch (SingleConceptMatchingPage.searchMode)
            {
                case SearchMode.Progressive:
                    if (!this.encodableInputFieldTimer.IsEnabled)
                    {
                        this.encodableInputFieldTimer.Start();
                    }

                    break;
                case SearchMode.UserInitiated:
                    break;
                default:
                    break;
            }            
        }

        /// <summary>
        /// Handles the AdditionalTextBoxTextChanged event of the SingleConceptMatching control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.TextChangedEventArgs"/> instance containing the event data.</param>
        private void SingleConceptMatching_AdditionalTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            this.additionalTextBoxTimer.Stop();
            if (this.connected)
            {
                this.StatusText.Text = string.Empty;
            }

            this.HideProgressBar();
            if (!this.additionalTextBoxTimer.IsEnabled && ValidateInputText(this.SingleConceptMatching.AdditionalTextBoxText))
            {                
                this.additionalTextBoxTimer.Start();
            }            
        }

        /// <summary>
        /// Handles the AddtionalTextBoxEnterPressed event of the SingleConceptMatching control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void SingleConceptMatching_AdditionalTextBoxEnterPressed(object sender, KeyEventArgs e)
        {
            if (this.SingleConceptMatching.InputBoxSelectedItem != null && ValidateInputText(this.SingleConceptMatching.AdditionalTextBoxText) && this.SingleConceptMatching.AdditionalTextBoxText != this.lastAdditionalTextBoxSearch)
            {
                this.lastAdditionalTextBoxSearch = this.SingleConceptMatching.AdditionalTextBoxText;

                this.ParseAdditionalText();
            }
            else if (this.lastAdditionalTextBoxResults != null && this.SingleConceptMatching.AdditionalTextBoxText == this.lastAdditionalTextBoxSearch && this.SingleConceptMatching.AdditionalTextBoxText == this.lastAdditionalTextBoxResults.SearchTextOriginal)
            {
                this.SingleConceptMatching.AdditionalTextBoxMatchingTermItemsSource = null;
                this.SingleConceptMatching.AdditionalTextBoxMatchingTermItemsSource = this.lastAdditionalTextBoxResults.AdditionalTextBoxResults;
            }
        }

        /// <summary>
        /// Handles the InputBoxEnterPressed event of the SingleConceptMatching control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void SingleConceptMatching_InputBoxEnterPressed(object sender, System.Windows.Input.KeyEventArgs e)
        {
            this.Search();
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        private void Search()
        {
            if (this.SingleConceptMatching.InputBoxText != this.lastInputBoxSearch)
            {
                this.lastInputBoxSearch = this.SingleConceptMatching.InputBoxText;

                if (this.connected)
                {
                    if (!SingleConceptMatchingPage.ValidateInputText(this.SingleConceptMatching.InputBoxText))
                    {
                        this.StatusText.Text = SingleConceptMatchingPage.InputTextValidationFailed;
                        return;
                    }

                    this.StatusText.Text = SingleConceptMatchingPage.SearchInProgressText;
                    this.ShowProgressBar();

                    this.SingleConceptMatching.InputBoxItemsSource = null;

                    FilterItem filterItem = this.SingleConceptMatching.SubsetPickerSelectedItem as FilterItem;

                    TerminologyManager.SearchInputField(SingleConceptMatchingPage.FixupInputText(this.SingleConceptMatching.InputBoxText), filterItem.SubsetCollection);                    
                }
            }
            else if (this.lastInputBoxResults != null && this.SingleConceptMatching.InputBoxText == this.lastInputBoxResults.SearchTextOriginal)
            {
                this.SingleConceptMatching.InputBoxItemsSource = this.lastInputBoxResults.InputFieldResults;
            }
        }

        /// <summary>
        /// Shows the progress bar.
        /// </summary>
        private void ShowProgressBar()
        {
            this.SearchProgressBar.IsIndeterminate = true;
        }  

        /// <summary>
        /// Hides the progress bar.
        /// </summary>
        private void HideProgressBar()
        {            
            this.SearchProgressBar.IsIndeterminate = false;
        }

        /// <summary>
        /// Handles the InputBoxSearchButtonClicked event of the SingleConceptMatching control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SingleConceptMatching_InputBoxSearchButtonClicked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Search();
        }

        /// <summary>
        /// Handles the selection changed event of the Scenario ComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ScenarioComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.lastInputBoxResults = null;
            this.lastInputBoxSearch = string.Empty;
            this.lastAdditionalTextBoxSearch = string.Empty;
            this.lastAdditionalTextBoxResults = null;
            this.SingleConceptMatching.InputBoxSelectedItem = null;
            this.SingleConceptMatching.InputBoxItemsSource = null;
            this.SingleConceptMatching.AdditionalTextBoxMatchingTermItemsSource = null;
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {                
                Scenario scenario;
                if ((scenario = (e.AddedItems[0] as Scenario)) != null)
                {
                    this.SingleConceptMatching.SubsetPickerItemsSource = scenario.FilterItemCollection;
                    this.SingleConceptMatching.InputBoxLabelText = scenario.TitleText;
                    this.SingleConceptMatching.InputBoxWatermark = scenario.WatermarkText;
                    this.SingleConceptMatching.SubsetPickerSelectedIndex = 0;              
                }
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event on the SearchTypeComboBox object.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void SearchTypeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.SearchTypeComboBox != null)
            {
                switch (this.SearchTypeComboBox.SelectedItem.ToString())
                {
                    case "Progressive":
                        SingleConceptMatchingPage.searchMode = SearchMode.Progressive;                        
                        break;
                    case "User-initiated":
                        this.encodableInputFieldTimer.Stop();                        
                        SingleConceptMatchingPage.searchMode = SearchMode.UserInitiated;
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the Loaded event of the ScenarioComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ScenarioComboBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.ScenarioComboBox.Items.Count > 0)
            {
                this.ScenarioComboBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Handles the Loaded event of the SearchTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SearchTypeComboBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.SearchTypeComboBox.Items.Count > 0)
            {
                this.SearchTypeComboBox.SelectedIndex = 0;
            }
        } 

        /// <summary>
        /// Handles the Reset Button click event which clears the form.
        /// </summary>
        /// <param name="sender">The reset form button.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ResetFormButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.SingleConceptMatching.Clear();
            this.lastInputBoxResults = null;
            this.lastInputBoxSearch = string.Empty;
            this.lastAdditionalTextBoxSearch = string.Empty;
            this.lastAdditionalTextBoxResults = null;
            this.outputs.Clear();
            this.SelectedOutputContentPresenter.Content = new SingleConceptMatchingOutput();

            if (this.connected)
            {
                this.StatusText.Text = string.Empty;
            }
            
            this.ScenarioComboBox.SelectedIndex = 0;
            this.SearchTypeComboBox.SelectedIndex = 0;            
            this.ScenarioComboBox.Focus();
        }

        /// <summary>
        /// Handles the saved event of the Single Concept matching control.
        /// Adds the encoded term to the encoded list box.
        /// </summary>
        /// <param name="sender">The single concept matching control.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void SingleConceptMatching_Saved(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.connected && this.SingleConceptMatching.InputBoxSelectedItem != null)
            {
                ObservableCollection<AdditionalTextBoxResult> additionalTextBoxResults = new ObservableCollection<AdditionalTextBoxResult>();

                foreach (object o in this.SingleConceptMatching.AdditionalTextBoxSelectedTerms)
                {
                    AdditionalTextBoxResult additionalTextBoxResult = o as AdditionalTextBoxResult;

                    if (additionalTextBoxResult != null)
                    {
                        additionalTextBoxResults.Add(additionalTextBoxResult);
                    }
                }

                this.ShowProgressBar();

                TerminologyManager.EncodeConcept(this.SingleConceptMatching.InputBoxSelectedItem as InputFieldResult, additionalTextBoxResults);
            }
            else
            {
                this.EncodeOutput(CreateUnencodedConcept(this.SingleConceptMatching.InputBoxText));

                this.SingleConceptMatching.Clear();
                this.SingleConceptMatching.FocusInputBox();
            }

            this.lastInputBoxResults = null;
            this.lastInputBoxSearch = string.Empty;
            this.lastAdditionalTextBoxSearch = string.Empty;
            this.lastAdditionalTextBoxResults = null;
        }        

        /// <summary>
        /// Handles the get concept details completed of the Terminology Client control.
        /// Creates and output and adds it to the list box.
        /// </summary>
        /// <param name="sender">The terminology client.</param>
        /// <param name="e">The <see cref="GetConceptDetailsCompletedEventArgs"/> instance containing the event data.</param>
        private void Client_GetConceptDetailsCompleted(object sender, GetConceptDetailsCompletedEventArgs e)
        {
            if (e.Error == null && e.Result != null)
            {                
                SingleConceptMatchingOutput output = new SingleConceptMatchingOutput()
                    {
                        OriginalInputFieldText = this.SingleConceptMatching.InputBoxText,
                        OriginalAdditionalText = this.SingleConceptMatching.AdditionalTextBoxText,
                        EncodedConcept = new EncodedConcept()
                        {
                            EncodedSingleConcept = e.Result
                        }
                    };

                this.outputs.Add(output);
                this.SingleConceptMatching.Clear();
                this.SingleConceptMatching.IsEnabled = true;
            }

            this.HideProgressBar();
        }

        /// <summary>
        /// Handles the selection changed event of the Output listBox which updates the displayed output.
        /// </summary>
        /// <param name="sender">The output list box.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void OutputListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedOutputContentPresenter.Content = this.OutputListBox.SelectedItem;
            this.OutputListBox.UpdateLayout();
            this.OutputListBox.ScrollIntoView(this.OutputListBox.SelectedItem);

            // ensure scrolled to the top of the control
            ScrollViewer viewer = this.SelectedOutputContentPresenter.Parent as ScrollViewer;
            if (null != viewer)
            {
                viewer.ScrollToVerticalOffset(0);
            }
        }

        /// <summary>
        /// Handles the filter combo box selected index changed.
        /// </summary>
        /// <param name="sender">The Single Concept Matching control.</param>
        /// <param name="e">Selection Changed Event Args.</param>
        private void SingleConceptMatching_FilterSelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            this.lastInputBoxSearch = string.Empty;
            this.lastInputBoxResults = null;
            this.lastAdditionalTextBoxSearch = string.Empty;
            this.lastAdditionalTextBoxResults = null;
            this.SingleConceptMatching.InputBoxItemsSource = null;
            this.SingleConceptMatching.InputBoxSelectedItem = null;
            this.Search();            
        }

        /// <summary>
        /// Handles when the input field's selection changes.
        /// </summary>
        /// <param name="sender">The Single Concept Matching control.</param>
        /// <param name="e">Event Args.</param>
        private void SingleConceptMatching_InputBoxSelectionChanged(object sender, EventArgs e)
        {
            this.SingleConceptMatching.AdditionalTextBoxMatchingTermItemsSource = null;            
            if (this.SingleConceptMatching.InputBoxSelectedItem != null)
            {
                TerminologyManager.GetConceptDetails((this.SingleConceptMatching.InputBoxSelectedItem as InputFieldResult).Concept.SnomedConceptId);
            }           
        }

        /// <summary>
        /// Shows focus is on the scroll viewer.
        /// </summary>
        /// <param name="sender">The scroll viewer.</param>
        /// <param name="e">Routed Event Args.</param>
        private void EncodedTermDetailsScrollViewer_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EncodedTermDetailsFocusRectangle.Opacity = 1;
        }

        /// <summary>
        /// Shows focus is no longer on the scroll viewer.
        /// </summary>
        /// <param name="sender">The scroll viewer.</param>
        /// <param name="e">Routed Event Args.</param>
        private void EncodedTermDetailsScrollViewer_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EncodedTermDetailsFocusRectangle.Opacity = 0;
        }

        /// <summary>
        /// Enables or disables the scroll viewer.
        /// </summary>
        /// <param name="sender">The ouputs collection.</param>
        /// <param name="e">Notify Collection Changed Args.</param>
        private void Outputs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.EncodedTermDetailsScrollViewer.IsEnabled = this.outputs.Count > 0;
        }
    }
}
