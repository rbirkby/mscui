<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LeafPage.Master"
    Inherits="SingleConceptMatching" CodeBehind="SingleConceptMatching.aspx.cs" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="leafPageContent" runat="Server">

    <div class="demoarea first section">
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The SingleConceptMatching control searches and encodes entered text as <a href="http://www.ihtsdo.org/snomed-ct/" target="_blank" title="Opens the SNOMED Clinical Terms website (New Window)">SNOMED&nbsp;CT&reg;</a> codes. The blue area below is what might be seen in an application. 
            The areas above and below the blue areas show possible contexts in which the control might appear, and the type of information which is output. 
            To use this sample, first set a context by selecting a scenario and the type of search to use.
        </p>
        <p>
            SNOMED&nbsp;CT&reg; searches on this page are provided by <a target="_blank" href="http://www.healthlanguage.com/" title="Link to the Health Language website (New Window)">Health Language, Inc</a>.
        </p>
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelSilverlightControl" HeaderText="<a id='SCMSilverlightTab' href=javascript:TabClick('SCMSilverlightTab'); title='Silverlight Tab'>Silverlight</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example Silverlight control (embedded):
                    <div>
                        <asp:Panel ID="DemoPanel1" runat="server" Width="740px">
                        </asp:Panel>
                        <br />
                        <div>
                            <object data="data:application/x-silverlight," type="application/x-silverlight-2"
                                width="100%" height="730px">
                                <param name="source" value="../ClientBin/Microsoft.Cui.SamplePages.xap" />
                                <param name="initParams" value="StartPage=SingleConceptMatching" />
                                <param name="minRuntimeVersion" value="3.0.40818.0" />
                                <div style="text-align:left; background-repeat:no-repeat; height:728px; background-image:url(images/SingleConceptMatching_Faded.png);">
                                <div style="padding-left:257px; padding-top:78px;">
                                    <div>
                                      <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration: none;">
                                        <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style: none" />
                                      </a>
                                    </div> 
                                </div>
                            </div>
                            </object>
                        <iframe style='visibility: hidden; height: 0; width: 0; border: 0px'></iframe>
                        </div>
                        <div class="resetFloatAfterdemoCCArea">
                        <br />
                        
                        <p>
                            The Single Concept Matching control is implemented by hosting and configuring 
                            the application code in a Microsoft Health CUI compliant manner. The sample 
                            application represents a set of features to promote quick understanding of the 
                            Single Concept Matching control, demonstrate the ability to search for clinical 
                            concepts using user initiated search and progressive search methods, and the 
                            ability to invoke a terminologies provider to parse and identify clinical 
                            concepts in the additional text box. The control is designed to be agnostic of 
                            the clinical information source.
                        </p>
                        </div>
                    </div>
                    <br />
                    
                    <!-- Area for Scenarios begins here -->
                    <asp:Panel ID="Scenarios_HeaderPanelSingleConcept" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="Scenarios_ToggleImageSingleConcept" runat="server" src="~/images/SFTheme/acc_h.png" />
                            Scenario Information
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Scenarios_ContentPanelSingleConcept" runat="server" Style="overflow: hidden;" Height="0px">
                        <div class="last section">
                        
                            <ul>
                                <li><b>Recording a risk</b>
                                    <p>This scenario demonstrates a simple case of a clinician 
                                    recording a risk without typing any additional text. 
                                    </p>
                                    <ol>
                                        <li>Select <b>Adverse Drug Reaction (ADR) entry</b> from the <b>Select a scenario</b> 
                                            drop-down list.</li>
                                        <li>Select <b>Drugs</b> from the <b>Search filter</b> drop-down list.</li>
                                        <li>Type the word &#39;Penicillin&#39; into the <b>Enter a substance</b> field. The search 
                                            is executed and the results are displayed in a list.</li>
                                        <li>Select <b>Penicillin</b> from the list of returned concepts. Leave the 
                                            additional qualifier text field blank.</li>
                                        <li>Click the <b>Save</b> button. The concept is added to the list of encoded terms.</li>
                                    </ol>
                                </li>

                                <li><b>Widening of subsets</b>
                                    <p>This scenario demonstrates a case where the scope of 
                                    terminology search can be changed by altering the subsets. 
                                    </p>
                                    <ol>
                                        <li>Select <b>Adverse Drug Reaction (ADR) entry</b> from the <b>Select a scenario</b> 
                                            drop-down list.</li>
                                        <li>Select <b>Food</b> from the <b>Search filter</b> drop-down list.</li>
                                        <li>Type the word &#39;Gaviscon&#39; into the <b>Enter a substance</b> field. The search is 
                                            executed, however no results are displayed.</li>
                                        <li>Select <b>All Allergies</b> from the <b>Search filter </b>drop-down list. The 
                                            search is re-executed and the results are displayed in a list.</li>
                                        <li>Select <b>Gaviscon</b> from the list of returned concepts. Leave the additional 
                                            qualifier text field blank.</li>
                                        <li>Click the <b>Save</b> button. The concept is added to the list of encoded terms.</li>
                                    </ol>
                                </li>

                                <li><b>No match found</b>
                                    <p>This scenario demonstrates a case where no results could be 
                                    found for the clinical term typed in the concept entry field.
                                    </p>
                                    <ol>
                                        <li>Select <b>Adverse Drug Reaction (ADR) entry</b> from the <b>Select a scenario</b> 
                                            drop-down list.</li>
                                        <li>Select <b>All Allergies</b> from the <b>Search filter</b> drop-down list.</li>
                                        <li>Type the word &#39;bcoprevir&#39; into the <b>Enter a substance</b> field. The search is 
                                            executed, however no results are displayed.</li>
                                        <li>Click the <b>Save</b> button. The concept is added to the list of encoded terms.</li>
                                    </ol>
                                </li>

                                <li><b>Reaction entry</b>
                                    <p>This scenario demonstrates an example of a clinician 
                                    recording a reaction entry which is also qualified 
                                    with a severity attribute.</p>
                                    <ol>
                                        <li>Select <b>Diagnosis entry</b> from the <b>Select a scenario</b> drop-down list.</li>
                                        <li>Select <b>Clinical Findings</b> from the <b>Search filter</b> drop-down list.</li>
                                        <li>Type the word &#39;Rash&#39; into the <b>Enter a diagnosis</b> field. The search is 
                                            executed and the results are displayed in a list.</li>
                                        <li>Select <b>Rash </b>from the list of returned concepts.
                                        </li>
                                        <li>Type the words &#39;reported as severe&#39; into the additional qualifier text field. 
                                            The text  &#39;severe&#39; is identified as a valid laterality qualifier and highlighted. 
                                            The concept <b>Severe</b> is shown with a check-box to the side of the 
                                            additional qualifier text field.</li>
                                        <li>Click the <b>Save</b> button. The concept is added to the list of encoded terms.</li>
                                    </ol>
                                </li>

                                <li><b>Diagnosis entry (involving laterality)</b>
                                    <p>This scenario demonstrates an example of a clinician recording a diagnosis 
                                    which is also qualified with a laterality attribute.</p>
                                    <ol>
                                        <li>Select <b>Diagnosis entry</b> from the <b>Select a scenario</b> drop-down list.</li>
                                        <li>Select <b>Clinical Findings</b> from the <b>Search filter</b> drop-down list.</li>
                                        <li>Type the phrase &#39;fracture of neck of femur&#39; into the <b>Enter a substance</b> 
                                            field. The search is executed and the results are displayed in a list.</li>
                                        <li>Select <b>Fracture of neck of femur</b> from the list of returned concepts.</li>
                                        <li>Type the word &#39;left&#39; in the additional qualifier text field. The word &#39;left&#39; is 
                                            identified as a valid laterality qualifier and highlighted. The concept <b>Left</b> 
                                            is shown with a check-box to the side of the additional qualifier text field.</li>
                                        <li>Check the <b>Left</b> check-box. The highlighted text &#39;left&#39; in the additional 
                                            qualifier text field changes from blue to grey.</li>
                                        <li>Click the <b>Save</b> button. The concept is added to the list of encoded terms.</li>
                                    </ol>
                                </li>

                                <li><b>Diagnosis entry (not body site)</b>
                                    <p>This scenario demonstrates an example of a clinician recording a diagnosis.</p>
                                    <ol>
                                        <li>Select <b>Diagnosis entry</b> from the <b>Select a scenario</b> drop-down list.</li>
                                        <li>Select Clinical Findings from the <b>Search filter</b> drop-down list.</li>
                                        <li>Type the word &#39;Gastroenteritis&#39; into the <b>Enter a diagnosis</b> field. The 
                                            search is executed and the results are displayed in a list.</li>
                                        <li>Select &#39;Gastroenteritis&#39; from the list of returned concepts.</li>
                                        <li>Type the phrase &#39;symptoms displayed for the last 3 days&#39; in the additional 
                                            qualifier text field. No text is highlighted in the additional qualifier text 
                                            field as no relevant matches are found.</li>
                                        <li>Click the <b>Save</b> button. The concept is added to the list of encoded terms.</li>
                                    </ol>
                                </li>

                                <li><b>Negation</b>
                                    <p>This scenario demonstrates a case of a clinician wanting to encode the absence 
                                    of an allergic reaction by prefixing the term with the word &#39;No&#39;.</p>
                                    <ol>
                                        <li>Select <b>Diagnosis entry</b> from the <b>Select a scenario</b> drop-down list.</li>
                                        <li>Select <b>Clinical Findings</b> from the <b>Search filter</b> drop-down list. </li>
                                        <li>Type the word &#39;No rash&#39; into the <b>Enter a diagnosis</b> field. The search is 
                                            executed and the results are displayed in a list.</li>
                                        <li>Select &#39;no allergic skin rash&#39; from the list of returned concepts. Leave the 
                                            additional qualifier text field blank.</li>
                                        <li>Click the <b>Save</b> button. The concept is added to the list of encoded terms.</li>
                                    </ol>
                                </li>

                                <li><b>Synonym</b>
                                    <p>This scenario demonstrates an example of a clinician recording a concept which 
                                    has a synonym.</p>
                                    <ol>
                                        <li>Select <b>Diagnosis entry</b> from the <b>Select a scenario</b> drop-down list.</li>
                                        <li>Select <b>Clinical Findings</b> from the <b>Search filter</b> drop-down list.</li>
                                        <li>Type the phrase &#39;tennis elbow&#39; into the <b>Enter a diagnosis</b> field. The 
                                            search is executed and the results are displayed in a list.</li>
                                        <li>Place the cursor over the concept <b>Tennis elbow</b> in the search results. The 
                                            flyout panel is displayed which indicates that &#39;tennis elbow&#39; is a synonym of 
                                            Lateral epicondylitis (disorder).</li>
                                        <li>Click anywhere on the screen to hide the flyout panel. Leave the additional 
                                            qualifier text field blank.</li>
                                        <li>Click the <b>Save</b> button. The concept is added to the list of encoded terms.</li>
                                    </ol>
                                </li>
                            </ul>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeScenariosSingleConcept" runat="server" 
                        TargetControlID="Scenarios_ContentPanelSingleConcept"
                        ExpandControlID="Scenarios_HeaderPanelSingleConcept" CollapseControlID="Scenarios_HeaderPanelSingleConcept"
                        Collapsed="True" ImageControlID="Scenarios_ToggleImageSingleConcept"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Scenarios section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Scenarios section"
                        SuppressPostBack="True" Enabled="True" />
                    <!-- Area for Scenarios ends here -->
                    
                    <!-- Area for Design Notes begins here -->
                   <asp:Panel ID="DesignNotes_HeaderPanelSingleConcept" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="DesignNotes_ToggleImageSingleConcept" runat="server" src="~/images/SFTheme/acc_h.png" />
                            Design Notes
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="DesignNotes_ContentPanelSingleConcept" runat="server" Style="overflow: hidden;"
                        Height="0px">
                        <div class="last section"> 
                            <p>This code shows how concepts within a clinical terminology (SNOMED&nbsp;CT&reg;) 
                               can be matched against text typed in by a clinician.</p>
                            <p>Although this code can be used without customization, it is recommended that the 
                               following factors be considered when implementing the desired functionality:</p>
                            <ul>
                                <li>Screen real estate availability</li>
                                <li>Order of fields on the screen</li>
                                <li>Relationships with other fields</li>
                                <li>Other user interface widgets in the vicinity</li>
                                <li>The specific tasks that clinicians perform with this control</li>
                                <li>Whether additional qualifier text is required</li>
                            </ul>
                            
                            <p>One or more of these factors may require the code to be customized to best match the specific requirements. However, it is recommended that only the following elements be changed (if at all):</p>
                            <ul>
                                <li>Presentation of the Single Concept Matching control;  for example whether it is presented as a pop-up dialog or whether it is part of the main screen</li>
                                <li>The &#39;additional qualifier text&#39; field:</li>
                                <ol>
                                    <li>Location</li>
                                    <li>Size</li>
                                    <li>Presentation; for example, whether it is always present or whether it is only 
                                        accessed by a user action (for example, a button click)</li>
                                </ol>
                                <li>Typeface formatting</li>
                                <li>Background color</li>
                                <li>Iconography</li>
                            </ul>

                            <p>It is recommended that the following customization not be made:</p>
                            <ul>   
                                <li>Minimum length of the matching entry field</li>
                            </ul> 
                        
                        </div>
                    </asp:Panel> 
                    
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeDesignNotesSingleConcept" runat="server" 
                        TargetControlID="DesignNotes_ContentPanelSingleConcept"
                        ExpandControlID="DesignNotes_HeaderPanelSingleConcept" CollapseControlID="DesignNotes_HeaderPanelSingleConcept"
                        Collapsed="True" ImageControlID="DesignNotes_ToggleImageSingleConcept"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Design Notes section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Design Notes section"
                        SuppressPostBack="True" Enabled="True" />
                    <!-- Area for Design Notes ends here -->
                    
                    <!-- Area for USAGE HINTS - GETTING STARTED begins here -->
                    <asp:Panel ID="Header_GettingStarted" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="ToggleImage_GettingStartedAccordion" runat="server" src="~/images/SFTheme/acc_h.png" />
                            Usage Hints - Getting Started
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Content_GettingStarted" runat="server" Style="overflow: hidden;" Height="0px">
                        <div class="last section"> 
                            <p>The following steps show how to use the Single Concept Matching control to create a very simple Silverlight application. 
                               The steps do not rely on the service of an external terminologies provider.</p>
                            <p><b>Note:</b> The steps do not rely on the services of an external terminologies provider.</p>
                            <b>Creating a Silverlight application and adding references</b>
                            <ol>
                                <li>Create a new Silverlight application in either VS.NET or Blend.</li>
                                <li>Add a reference to Microsoft.Cui.Controls.dll.</li>
                            </ol>
                            <b>Instantiating the Single Concept Matching control</b>
                            <p>Perform one of the following:</p>
                            <ul>
                                <li>If using Blend, drag the Single Concept Matching control on to the form</li>
                                <li>If using VS.NET, copy the XAML as produced from Blend</li>
                            </ul>
                            <b>Creating a C# class to represent a clinical concept</b>
<pre>
    public class ClinicalConcept
    {
        private int SnomedIDField;
        private string DescriptionField;
        private int SnomedDescriptionIDField;
        private string PreferredTermField;

        public ClinicalConcept()
        {
        }

        public ClinicalConcept(int snomedid, string description, int descripid)
        {
            this.SnomedID = snomedid;
            this.Description = description;
            this.SnomedDescriptionID = descripid;
            PreferredTermField=&quot;Preferred term &quot; + description;
        }

        public int SnomedID
        {
            get { return SnomedIDField; }
            set { SnomedIDField = value; }
        }

        public string Description
        {
            get { return DescriptionField; }
            set { DescriptionField = value; }
        }

        public int SnomedDescriptionID
        {
            get { return SnomedDescriptionIDField; }
            set { SnomedDescriptionIDField = value; }
        }

        public string PreferredTerm
        {
            get { return PreferredTermField; }
            set { PreferredTermField = value; }
        }
    }
</pre>
                            <b>Initiating a search and displaying the results</b>
                            <ol>
                                <li>Provide a handler for InputBoxEnterPressed.</li>
                                <li>In this handler, carry out a &#39;search&#39; using the text typed by the user and 
                                    assign a collection of ClinicalConcept objects to the InputBoxItemsSource 
                                    property of the Single Concept Matching control.</li>
                            </ol>
                            <b>Rendering the flyout and the search results</b>
                            <p>The following code calls the template for displaying the results from applying 
                                the selected subset picker (the&nbsp;Search filter):</p>
<pre>
&lt;DataTemplate x:Key=&quot;SubsetPickerItemTemplate&quot;&gt;
              &lt;Border&gt;
                  &lt;TextBlock Margin=&quot;12&quot; Text=&quot;{Binding Path=Value}&quot; FontSize=&quot;12&quot; /&gt;
              &lt/Border&gt;
&lt/DataTemplate&gt;
</pre>
                            <p>The following code calls the template for displaying the search results list:</p>
<pre>
&lt;DataTemplate x:Key=&quot;InputBoxItemTemplate&quot;&gt;
              &lt;Border&gt;
                  &lt;controls:IndentedLabel
                            MaxLines=&quot;2&quot; Text=&quot;{Binding Description}&quot; 
                            VerticalAlignment=&quot;Top&quot; FontSize=&quot;12&quot; /&gt;
              &lt/Border&gt;
&lt/DataTemplate&gt;
</pre>

                            <b>Parsing the additional text</b>
                            <ol>
                                <li>Create a class to represent a matched term in the additional text</li>
<pre>
    public class MatchingTerm
    {
        public MatchingTerm()
        {
        }

        public MatchingTerm(int startindex, bool isselected, int length)
        {
		///initialization code
        }

        private int StartIndexField;
        private int LengthField;
        private bool IsSelectedField;
        private ClinicalConcept SelectedItemField;
        private ObservableCollection&lt;ClinicalConcept&gt; alternateItems;
        
        public int StartIndex
        {
            get { return StartIndexField; }
            set { StartIndexField = value; }
        }

        public int Length
        {
            get { return LengthField; }
            set { LengthField = value; }
        }

        public bool IsSelected
        {
            get { return IsSelectedField; }
            set { IsSelectedField = value; }
        }

        public ClinicalConcept SelectedItem
        {
            get { return SelectedItemField; }
            set { SelectedItemField = value; }
        }

        public ObservableCollection&lt;ClinicalConcept&gt; AlternateItems
        {
            get
            {
                return this.alternateItems;
            }
        }
    }
</pre>
                                <li><p>Each of the matched terms is represented by:</p></li>
                                <ul>
                                    <li><b>StartIndex</b> - the numerical location of the first character of the matched term.</li>
                                    <li><b>Length</b> - the number of characters in the matched term.</li>
                                    <li><b>AlternateItems</b> - a collection of possible alternatives for the matched term. For example, left or right.</li>
                                    <li><b>SelectedItem</b> - the item in the collection that represents the matched term</li>
                                    <li><b>IsSelected</b> - indicates whether the user has accepted the matched term</li>
                                </ul>
                                <li><p>Provide a handler for the event AdditionalTextBoxEnterPressed.</p></li>
                                <li><p>In this handler, do the parsing of the additional text, initialize a collection of MatchingTerm, and assign this to AdditionalTextBoxMatchingTermItemsSource.</p></li>
                            </ol>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender 
                        ID="cpeGettingStarted" runat="server"
                        TargetControlID="Content_GettingStarted"
                        ExpandControlID="Header_GettingStarted" CollapseControlID="Header_GettingStarted"
                        Collapsed="True" ImageControlID="ToggleImage_GettingStartedAccordion"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Getting Started section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Getting Started section"
                        SuppressPostBack="True" Enabled="True" />
                    <!-- Area for USAGE HINTS - GETTING STARTED ends here -->
                    
                    <!-- Area for USAGE HINTS - MEMBERS begins here -->
                    <asp:Panel ID="Members_HeaderPanelSingleConcept" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="Prop_ToggleImageSingleConcept" runat="server" src="~/images/SFTheme/acc_h.png" />
                            Usage Hints - Members
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Members_ContentPanelSingleConcept" runat="server" Style="overflow: hidden;" Height="0px">
                        <div class="last section"> 
                            <b>Single Concept Matching Members</b>
                            <ul>
                                <li><strong>AdditionalTextBoxDecoratorItemContainerStyle</strong> &ndash; Gets or sets the additional text box&#39;s decorator container item style</li>
                                <li><strong>AdditionalTextBoxDisplayedMatchingTerms</strong> &ndash; Gets the additional text box&#39;s displayed matching terms property</li>
                                <li><strong>AdditionalTextBoxIsReadOnly</strong> &ndash; Gets or sets a value indicating whether the additional text box is read-only</li>
                                <li><strong>AdditionalTextBoxMatchingTermIsSelectedMemberPath</strong> &ndash; Gets or sets the additional text box&#39;s matching terms is selected member path</li>
                                <li><strong>AdditionalTextBoxMatchingTermItemsSource</strong> &ndash; Gets or sets the additional text box&#39;s matching terms items source</li>
                                <li><strong>AdditionalTextBoxMatchingTermLengthMemberPath</strong> &ndash; Gets or sets the additional text box&#39;s matching terms length member path</li>
                                <li><strong>AdditionalTextBoxMatchingTermStartIndexMemberPath</strong> &ndash; Gets or sets the additional text box&#39;s matching terms start index</li>
                                <li><strong>AdditionalTextBoxMaxLength</strong> &ndash; Gets or sets the additional text box&#39;s maximum length</li>
                                <li><strong>AdditionalTextBoxSelectedTerms</strong> &ndash; Gets a list of selected terms</li>
                                <li><strong>AdditionalTextBoxStyle</strong> &ndash; Gets or sets the additional text box&#39;s style</li>
                                <li><strong>AdditionalTextBoxText</strong> &ndash; Gets or sets the additional text</li>
                                <li><strong>AdditionalTextBoxWatermark</strong> &ndash; Gets or sets the additional text watermark</li>
                                <li><strong>AdditionalTextBoxWatermarkTemplate</strong> &ndash; Gets or sets the additional text watermark template property</li>
                                <li><strong>InputBoxEncodedItemTemplate</strong> &ndash; Gets or sets the input box&#39;s encoded item template</li>
                                <li><strong>InputBoxFlyOutTemplate</strong> &ndash; Gets or sets the input box&#39;s flyout template</li>
                                <li><strong>InputBoxFlyOutWidth</strong> &ndash; Gets or sets the flyout width</li>
                                <li><strong>InputBoxIsSearchButtonVisible</strong> &ndash; Gets or sets a value indicating whether the search button on the input box is visible</li>
                                <li><strong>InputBoxItemContainerStyle</strong> &ndash; Gets or sets the input box&#39;s item container style</li>
                                <li><strong>InputBoxItemsSource</strong> &ndash; Gets or sets the input box&#39;s items source</li>
                                <li><strong>InputBoxItemTemplate</strong> &ndash; Gets or sets the input box&#39;s item template</li>
                                <li><strong>InputBoxLabelText</strong> &ndash; Gets or sets the input box&#39;s label text</li>
                                <li><strong>InputBoxMaxLength </strong> &ndash; Gets or sets the maximum length of the input box</li>
                                <li><strong>InputBoxSearchButtonToolTip </strong> &ndash; Gets or sets the input box&#39;s search button tooltip</li>
                                <li><strong>InputBoxSelectedItem</strong> &ndash; Gets or sets the input box&#39;s selected item</li>
                                <li><strong>InputBoxStyle </strong> &ndash; Gets or sets the input box&#39;s style</li>
                                <li><strong>InputBoxText</strong> &ndash; Gets or sets the input box&#39;s text</li>
                                <li><strong>InputBoxWatermark</strong> &ndash; Gets or sets the input box&#39;s watermark</li>
                                <li><strong>InputBoxWatermarkTemplate</strong> &ndash; Gets or sets the input box&#39;s watermark template</li>
                                <li><strong>IsSaveButtonEnabled </strong> &ndash; Gets or sets a value indicating whether the save button is enabled</li>
                                <li><strong>LabelStyle</strong> &ndash; Gets or sets the label style</li>
                                <li><strong>MatchingTermAlternateItemsMemberPath </strong> &ndash; Gets or sets the matching term alternate items member path</li>
                                <li><strong>MatchingTermFlyOutContentTemplate </strong> &ndash; Gets or sets the matching term flyout content template</li>
                                <li><strong>MatchingTermItemsControlEncodedItemTemplate </strong> &ndash; Gets or sets the matching term items control item template</li>
                                <li><strong>MatchingTermItemsControlItemContainerStyle</strong> &ndash; Gets or sets the matching term items control item container style property</li>
                                <li><strong>MatchingTermItemsControlItemTemplate</strong> &ndash; Gets or sets the matching term items control item template</li>
                                <li><strong>MatchingTermSelectedItemMemberPath </strong> &ndash; Gets or sets the selected item member path for the matching term items</li>
                                <li><strong>MatchingTermSelectedItemTextMemberPath </strong> &ndash; Gets or sets the selected item text member path for the matching term items</li>
                                <li><strong>SubsetPickerDataTemplate</strong> &ndash; Gets or sets the subset picker&#39;s (Search filter) data template</li>
                                <li><strong>SubsetPickerItemsSource</strong> &ndash; Gets or sets the subset picker&#39;s (Search filter&#39;s) items source</li>
                                <li><strong>SubsetPickerLabelText</strong> &ndash; Gets or sets the subset picker&#39;s (Search filter&#39;s) label text</li>
                                <li><strong>SubsetPickerSelectedIndex</strong> &ndash; Gets or sets the subset picker&#39;s (Search filter&#39;s) selected index</li>
                                <li><strong>SubsetPickerSelectedItem</strong> &ndash; Gets or sets the subset picker&#39;s (Search filter&#39;s) selected item</li>
                            </ul>
                            <b>Single Concept Matching Methods</b>
                            <ul>
                                <li><strong>Clear</strong> &ndash; Clears the fields in the control</li>
                                <li><strong>FocusAdditionalTextBox</strong> &ndash; Gives focus to the additional text box</li>
                                <li><strong>FocusInputBox</strong> &ndash; Gives focus to the input box</li>
                            </ul>
                            <b>Single Concept Matching Events</b>
                            <ul>
                                <li><strong>AdditionalTextBoxEnterPressed</strong> &ndash; Occurs when the enter key is pressed in the additional text box</li>
                                <li><strong>AdditionalTextBoxTextChanged</strong> &ndash; Occurs when the additional text box&#39;s text is changed</li>
                                <li><strong>Cleared</strong> &ndash; Occurs when cleared</li>
                                <li><strong>FilterSelectedIndexChanged</strong> &ndash; Occurs when the filter selected index is changed</li>
                                <li><strong>InputBoxEnterPressed</strong> &ndash; Occurs when the enter key is pressed in the input box&#39;s text field</li>
                                <li><strong>InputBoxSearchButtonClicked</strong> &ndash; Occurs when the input box&#39;s search button is clicked</li>
                                <li><strong>InputBoxSelectionChanged</strong> &ndash; Occurs when the input box&#39;s selection is changed</li>
                                <li><strong>InputBoxTextChanged</strong> &ndash; Occurs when the input box&#39;s text is changed</li>
                                <li><strong>Saved</strong> &ndash; Occurs when saved</li>
                            </ul>
                        </div>
                    </asp:Panel>
                    
                    <ajaxToolkit:CollapsiblePanelExtender
                        ID="cpeMembersSingleConcept" runat="server" 
                        TargetControlID="Members_ContentPanelSingleConcept"
                        ExpandControlID="Members_HeaderPanelSingleConcept" CollapseControlID="Members_HeaderPanelSingleConcept"
                        Collapsed="True" ImageControlID="Prop_ToggleImageSingleConcept"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Members section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Members section"
                        SuppressPostBack="True" Enabled="True" />
                    <!-- Area for USAGE HINTS - MEMBERS ends here -->
                    
                    <!-- Area for USAGE HINTS - Consuming the control begins here -->
                    <asp:Panel ID="ConsumingTheControl_HeaderPanelSingleConcept" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="ConsumingTheControl_ToggleImageSingleConcept" runat="server" src="~/images/SFTheme/acc_h.png" />
                            Usage hints - Consuming the control
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="ConsumingTheControl_ContentPanelSingleConcept" runat="server" Style="overflow: hidden;" Height="0px">
                        <div class="last section">
                            <p>While reviewing the programmability of the Single Concept Matching control, an understanding of the core object implementation is required. The Single Concept Matching control is implemented by the class SingleConceptMatching. The SingleConceptMatching class is a composite control and inherits from the base class control.</p>
                            <br />
                            <p>To consume the control within a Silverlight application, a reference to the SingleConceptMatching control needs to be created:</p>
<pre>
&lt;mscui:SingleConceptMatching x:Name=&quot;MySingleConceptMatchingControl&quot;&gt; 
                             &lt;/mscui:SingleConceptMatching&gt;
</pre>
                            <p>The namespace element that needs to be added within the XAML is:</p>
<pre>
xmlns:mscui=&quot;clr-namespace:Microsoft.Cui.Controls;assembly=Microsoft.Cui.Controls&quot;
</pre>
                        </div>
                    </asp:Panel>
                    
                    <ajaxToolkit:CollapsiblePanelExtender 
                        ID="cpeConsumingTheControlSingleConcept" runat="server" 
                        TargetControlID="ConsumingTheControl_ContentPanelSingleConcept"
                        ExpandControlID="ConsumingTheControl_HeaderPanelSingleConcept" CollapseControlID="ConsumingTheControl_HeaderPanelSingleConcept"
                        Collapsed="True" ImageControlID="ConsumingTheControl_ToggleImageSingleConcept"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Consuming the Control section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Consuming the Control section"
                        SuppressPostBack="True" Enabled="True" />
                    <!-- Area for USAGE HINTS - Consuming the control ends here -->
                    
                    <!-- Area for USAGE HINTS - Configuring Subsets begins here -->
                    <asp:Panel ID="ConfiguringSubsets_HeaderPanelSingleConcept" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="ConfiguringSubsets_ToggleImageSingleConcept" runat="server" src="~/images/SFTheme/acc_h.png" />
                            Usage hints - Configuring SNOMED&nbsp;CT&reg; subsets
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="ConfiguringSubsets_ContentPanelSingleConcept" runat="server" Style="overflow: hidden;" Height="0px">
                        <div class="last section">
                            <p>The Single Concept Matching control contains a ComboBox control, called SubsetPicker, as one of its constituent controls. The purpose of this SubsetPicker is to allow the clinician to select a SNOMED&nbsp;CT&reg; subset, or a collection of SNOMED&nbsp;CT&reg; subsets and thereby change the scope of search.</p>
                            <br />
                            <p>The properties SubsetPickerItemsSource and SubsetPickerDataTemplate should be used to specify the data source of subsets, and the XAML template used for rendering the SubsetPicker respectively.</p>
                            <br />
                            <p>An example of a data template is shown here. This snippet assumes that the data source of subsets is a collection of custom objects, and that the custom class has a public property called Description.</p>
<pre>
&lt;DataTemplate x:Key=&quot;SubsetPickerItemTemplate&quot;&gt;
              &lt;Border&gt;
                  &lt;TextBlock Text=&quot;{Binding Path=Description}&quot; FontWeight=&quot;Normal&quot; /&gt;
              &lt;/Border&gt;
&lt;/DataTemplate&gt;
</pre>
                        </div>
                    </asp:Panel>
                    
                    <ajaxToolkit:CollapsiblePanelExtender 
                        ID="cpeConfiguringSubsetsSingleConcept" runat="server" 
                        TargetControlID="ConfiguringSubsets_ContentPanelSingleConcept"
                        ExpandControlID="ConfiguringSubsets_HeaderPanelSingleConcept" CollapseControlID="ConfiguringSubsets_HeaderPanelSingleConcept"
                        Collapsed="True" ImageControlID="ConfiguringSubsets_ToggleImageSingleConcept"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Configuring Subsets section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Configuring Subsets section"
                        SuppressPostBack="True" Enabled="True" />
                    <!-- Area for USAGE HINTS - Configuring Subsets ends here -->
                    
                    <!-- Area for USAGE HINTS - Searching for terminologies and displaying results begins here -->
                    <asp:Panel ID="InitiatingSearch_HeaderPanelSingleConcept" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="InitiatingSearch_ToggleImageSingleConcept" runat="server" src="~/images/SFTheme/acc_h.png" />
                            Usage hints - Searching for terminologies and displaying results list
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="InitiatingSearch_ContentPanelSingleConcept" runat="server" Style="overflow: hidden;" Height="0px">
                        <div class="last section">
                            <p>The Single Concept Matching control publishes several events which can be subscribed to through the container application.</p>
                            <br />
                            <p>The events InputBoxSearchButtonClicked, InputBoxEnterPressed and InputBoxTextChanged are triggered when a clinician explicitly clicks on the Search button, presses the Enter key or presses a key in the input text box respectively.</p>
                            <br />
                            <p>The control implements properties InputBoxItemTemplate and InputBoxFlyOutTemplate which should be used for configuring the presentation of the results list and the flyout respectively.</p>
                            <br />
                            <p>The control implements a property InputBoxItemsSource which should be initialized with a collection of custom objects, each of which represents a clinical concept. </p>
                            <br />
                            <p>The XAML snippets below assume that the custom class has properties SnomedID, 
                               Description and SnomedDescriptionID.</p> 
                            <br />
                            <p>An example of a DataTemplate for configuring the display of items in the results drop down list:</p>
<pre>
&lt;DataTemplate x:Key=&quot;InputBoxItemTemplate&quot;&gt;
              &lt;Border&gt;
                  &lt;mscui:IndentedLabel 
                         MaxLines=&quot;2&quot;  Text=&quot;{Binding Description}&quot; 
                         VerticalAlignment=&quot;Top&quot; FontWeight=&quot;Normal&quot; /&gt;
              &lt;/Border&gt;
&lt;/DataTemplate&gt;
</pre>
                            <p>An example of a DataTemplate for configuring the display of the flyout which pops up when the mouse cursor hovers over an item in the results:</p>
<pre>
&lt;DataTemplate x:Key=&quot;FlyoutTemplate&quot;&gt;
              &lt;StackPanel Orientation=&quot;Vertical&quot;&gt;
                  &lt;StackPanel Orientation=&quot;Horizontal&quot;&gt;
                      &lt;TextBlock Text=&quot;SNOMED Description:&quot; FontWeight=&quot;Normal&quot; /&gt;
                      &lt;TextBlock Text=&quot;{Binding Description}&quot; FontWeight=&quot;Bold&quot; /&gt;
                  &lt;/StackPanel &gt;
                  &lt;StackPanel Orientation=&quot;Horizontal&quot;&gt;
                      &lt;TextBlock Text=&quot;SNOMED Id:&quot; FontWeight=&quot;Normal&quot; /&gt;
                      &lt;TextBlock Text=&quot;{Binding SnomedID}&quot; FontWeight=&quot;Bold&quot; /&gt;
                  &lt;/StackPanel&gt;
              &lt;/StackPanel&gt;
&lt;/DataTemplate&gt;
</pre>
                        </div>
                    </asp:Panel>
                    
                    <ajaxToolkit:CollapsiblePanelExtender 
                        ID="cpeInitiatingSearchSingleConcept" runat="server" 
                        TargetControlID="InitiatingSearch_ContentPanelSingleConcept"
                        ExpandControlID="InitiatingSearch_HeaderPanelSingleConcept" CollapseControlID="InitiatingSearch_HeaderPanelSingleConcept"
                        Collapsed="True" ImageControlID="InitiatingSearch_ToggleImageSingleConcept"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Searching for Terminologies and Displaying Results section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Searching for Terminologies and Displaying Results section"
                        SuppressPostBack="True" Enabled="True" />
                    <!-- Area for USAGE HINTS - Searching for terminologies and displaying results ends here -->
                    
                    <!-- Area for USAGE HINTS - Parsing Additional Text begins here -->
                    <asp:Panel ID="ParsingText_HeaderPanelSingleConcept" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="ParsingText_ToggleImageSingleConcept" runat="server" src="~/images/SFTheme/acc_h.png" />
                            Usage hints - Parsing Additional Text
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="ParsingText_ContentPanelSingleConcept" runat="server" Style="overflow: hidden;" Height="0px">
                        <div class="last section">
                            <p>The Single Concept Matching control contains a multi-line text box control, called AdditionalTextBox, as one of its constituent controls. The purpose of this control is to let the clinician type free text.</p>
                            <br />
                            <p>The control fires the events AdditionalTextBoxEnterPressed and AdditionalTextBoxTextChanged when the clinician presses the Enter key and when any text update occurs within the additional qualifier text field respectively. These events must be handled and the additional text parsed to determining clinically encodable terms within the text.</p>
                            <br />
                            <p>If clinical terms are present in the text, the property AdditionalTextBoxMatchingTermItemsSource should be initialized with a collection of custom objects. Each of these objects represents a clinical concept and contains information regarding the position of these terms within the AdditionalTextBox&#39;s text.</p>
                            <br />
                            <p>The following snippets assume that a custom class representing a matched term has the properties StartIndex, Length and IsSelected.</p>
                            <br />
                            <p>The properties StartIndex and Length denote the zero based position of the first character of the matched term and the number of the characters in the matched term respectively.</p>
                            <br />
                            <p>The application logic for parsing the additional text is responsible for setting 
                               these properties so that the Single Concept Matching control can render the matched words.</p>
                            <br />
                            <p>The property MatchingTermAlternateItemsMemberPath has been initialized to the member name of the custom class which represents the SNOMED&nbsp;CT&reg; preferred name of the concept.</p>
                            <br />
                            <p>The property MatchingTermAlternateItemsMemberPath has been initialized to the member name of the custom class which returns a collection of all concepts that are possible choices for replacing the matched term.</p>
<pre>
&lt;mscui:SingleConceptMatching  
       ...  
       AdditionalTextBoxMatchingTermStartIndexMemberPath=&quot;StartIndex&quot;
       AdditionalTextBoxMatchingTermIsSelectedMemberPath=&quot;IsSelected&quot;
       AdditionalTextBoxMatchingTermLengthMemberPath=&quot;Length&quot;
       ...
       MatchingTermSelectedItemMemberPath=&quot;SelectedItem&quot;
       MatchingTermSelectedItemTextMemberPath=&quot;PreferredTerm&quot;
       MatchingTermAlternateItemsMemberPath=&quot;AlternateItems&quot;
       ...
       MatchingTermItemsControlItemTemplate=&quot;{StaticResource MatchingTermItemTemplate}&quot;
       MatchingTermItemsControlEncodedItemTemplate=&quot;{StaticResource MatchingTermEncodedItemTemplate}&quot;
       ...&gt;
&lt;/mscui:SingleConceptMatching&gt;
</pre>
                            <p>An example of a DataTemplate for configuring the display of the alternate concepts within the check box:</p>
<pre>
&lt;DataTemplate x:Key=&quot;MatchingTermItemTemplate&quot;&gt;
              &lt;Border&gt;
                  &lt;mscui:IndentedLabel
                         MaxLines=&quot;2&quot; IndentCharacterCount=&quot;2&quot;
                         Text=&quot;{Binding PreferredTerm}&quot; /&gt;
              &lt;/Border&gt;
&lt;/DataTemplate&gt;
</pre>
                            <p>An example of a DataTemplate for configuring the display of the matched word within the check box:</p>
<pre>
&lt;DataTemplate x:Key=&quot;MatchingTermEncodedItemTemplate&quot;&gt;
              &lt;Border&gt;
                  &lt;mscui:IndentedLabel
                         MaxLines=&quot;2&quot; IndentCharacterCount=&quot;2&quot;
                         Margin=&quot;2&quot; Text=&quot;{Binding PreferredTerm}&quot; /&gt; 
              &lt;/Border&gt;
&lt;/DataTemplate&gt;
</pre>
                        </div>
                    </asp:Panel>
                    
                    <ajaxToolkit:CollapsiblePanelExtender 
                        ID="cpeParsingTextSingleConcept" runat="server" 
                        TargetControlID="ParsingText_ContentPanelSingleConcept"
                        ExpandControlID="ParsingText_HeaderPanelSingleConcept" CollapseControlID="ParsingText_HeaderPanelSingleConcept"
                        Collapsed="True" ImageControlID="ParsingText_ToggleImageSingleConcept"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Parsing Additional Text section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Parsing Additional Text section"
                        SuppressPostBack="True" Enabled="True" />
                    <!-- Area for USAGE HINTS - Parsing Additional Text ends here-->

                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        
        <ajaxToolkit:TabPanel runat="server" ID="panelWPF" HeaderText="<a id='SCMWPFTab' href=javascript:TabClick('SCMWPFTab'); title='WPF Tab'>WPF</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WPF control (screenshot):
                    <br />
                    <br />
                    <div>
                       <img class="controls_border" alt="Single Concept Matching WPF control screenshot" title="Single Concept Matching WPF control screenshot" runat="server" src="~/Components/Images/SingleConceptMatching.jpg"
                            />
                    </div>
                    <br />
                     <p>
                        Further information on this control, and the full source code, can be found in the
                        Microsoft Health Common User Interface Toolkit, which can be downloaded from our
                        <a href="http://www.codeplex.com/mscui/Release/ProjectReleases.aspx" target="_blank" title="Link to releases page on the CodePlex site (New Window)">
                            CodePlex</a> site.
                    </p>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>
