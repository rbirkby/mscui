<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchAndPrescribe.aspx.cs"
    Inherits="SearchAndPrescribe" MasterPageFile="~/LeafPage.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="leafPageContent" runat="Server">

    <script type="text/javascript">
        function onSilverlightError(sender, args) {
            var appSource = "";
            if (sender != null && sender != 0) {
                appSource = sender.getHost().Source;
            }

            var errorType = args.ErrorType;
            var iErrorCode = args.ErrorCode;

            if (errorType == "ImageError" || errorType == "MediaError") {
                return;
            }

            var errMsg = "Unhandled Error in Silverlight Application " + appSource + "\n";

            errMsg += "Code: " + iErrorCode + "    \n";
            errMsg += "Category: " + errorType + "       \n";
            errMsg += "Message: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "File: " + args.xamlFile + "     \n";
                errMsg += "Line: " + args.lineNumber + "     \n";
                errMsg += "Position: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {
                if (args.lineNumber != 0) {
                    errMsg += "Line: " + args.lineNumber + "     \n";
                    errMsg += "Position: " + args.charPosition + "     \n";
                }
                errMsg += "MethodName: " + args.methodName + "     \n";
            }

            throw new Error(errMsg);
        }

    </script>

    <div class="demoarea first section">
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The SearchAndPrescribe Control allows designers and developers to implement a set
            of prescribing functionality and is available in Silverlight and WPF. The control
            is aligned to all mandatory guidance points in the <i>Design Guidance &ndash; Search and
            Prescribe</i> document, but does not represent full coverage of the prescribing area
            (for example, asymmetric doses have not been addressed). Note that this
            sample contains only a limited set of drugs to prescribe. Refer to the Scenario
            Information section below for the sample drug list.
        </p>
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelSilverlightControl" HeaderText="<a id='SCMSilverlightTab' href=javascript:TabClick('SCMSilverlightTab'); title='Silverlight Tab'>Silverlight</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example Silverlight control (embedded):
                    <br />
                    <div>
                        <asp:Panel ID="DemoPanel1" runat="server" Width="740px">
                        </asp:Panel>
                        <br />
                        <div>
                            <object data="data:application/x-silverlight," type="application/x-silverlight-2"
                                width="100%" height="800px">
                                <param name="source" value="../ClientBin/Microsoft.Cui.SamplePages.xap" />
                                <param name="onError" value="onSilverlightError" />
                                <param name="initParams" value="StartPage=SearchAndPrescribe" />
                                <param name="minRuntimeVersion" value="3.0.40818.0" />
                                <div style="text-align: left; background-repeat: no-repeat; height: 800px; background-image: url(images/SearchAndPrescribeFaded.jpg);">
                                    <div style="padding-left: 257px; padding-top: 120px;">
                                        <div>
                                            <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration: none;">
                                                <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                                                    style="border-style: none" />
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </object>
                            <iframe style='visibility: hidden; height: 0; width: 0; border: 0px'></iframe>
                        </div>
                        <div class="resetFloatAfterdemoCCArea">
                            <p>
                                The Search and Prescribe control is implemented by hosting and configuring the application
                                code in a Microsoft Health CUI compliant manner. The sample application, implemented
                                alongside the Medications List View, represents a set of features to promote quick
                                understanding of the Search and Prescribe control, including support for template
                                prescriptions and prescribing rules for individual types of medications. Further
                                information relating to the implementation of the control is detailed in the sections
                                below.
                            </p>
                        </div>
                    </div>
                    <!-- Area for Introduction begins here -->
                    <asp:Panel ID="Introduction_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="Introduction_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
                            Introduction
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Introduction_ContentPanel" runat="server" Style="overflow: hidden;"
                        Height="0px">
                        <div class="last section">
                            <p>
                                The Search and Prescribe Control allows designers and developers to implement a
                                set of prescribing functionality and is available in Silverlight and WPF.
                            </p>
                            <p>
                                You can bind any data to the control, but this configuration of the control represents
                                data to support a limited set of specific scenarios. To integrate the control into
                                your application, you will need to provide relevant source data and prescribing
                                rules.</p>
                            <p>
                                Although the sample application conforms to all mandatory elements of the published
                                Search and Prescribe design guidance, the following point is not supported in the
                                sample control:
                            </p>
                            <table class="desc">
                                <thead>
                                    <tr>
                                        <td valign="top" width="15%">
                                            <p>
                                                Reference</p>
                                        </td>
                                        <td valign="top" width="55%">
                                            <p>
                                                Guidance Point</p>
                                        </td>
                                        <td valign="top" width="15%">
                                            <p>
                                                Conformance</p>
                                            <td valign="top" width="15%">
                                                <p>
                                                    Evidence</p>
                                    </tr>
                                </thead>
                                <tr>
                                    <tr>
                                        <td valign="top">
                                            <p>
                                                MSP-1640</p>
                                        </td>
                                        <td valign="top">
                                            <p>
                                                Where relevant, use supplementary text in a drop-down list of options if the selection
                                                will affect other options in the form</p>
                                        </td>
                                        <td valign="top">
                                            <p>
                                                Recommended</p>
                                            <td valign="top">
                                                <p>
                                                    Low</p>
                                    </tr>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="Introduction_CollapsiblePanelExtender"
                        runat="server" TargetControlID="Introduction_ContentPanel" ExpandControlID="Introduction_HeaderPanel"
                        CollapseControlID="Introduction_HeaderPanel" Collapsed="True" ImageControlID="Introduction_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Introduction section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Introduction section"
                        SuppressPostBack="True" Enabled="True" />
                    <!-- Area for Introduction ends here -->
                    <!-- Area for Scenarios begins here -->
                    <asp:Panel ID="Scenarios_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="Scenarios_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
                            Scenario Information
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Scenarios_ContentPanel" runat="server" Style="overflow: hidden;" Height="0px">
                        <div class="last section">
                            <b>Supported Scenarios</b>
                            <ul>
                                <li><b>Regular prescriptions</b>
                                    <ul>
                                        <li>Prescribe <b>clarithromycin</b> using the quick list and a template prescription
                                            and with more than one possible route</li>
                                        <li>Prescribe <b>salbutamol</b> metred dose inhaler with a starting condition</li>
                                        <li>Prescribe <b>salbutamol</b> nebuliser PRN with a starting condition</li>
                                        <li>Prescribe <b>fluticasone + salmeterol</b> &ndash; <i>by searching for salmeterol</i></li>
                                    </ul>
                                </li>
                            </ul>
                            <ul>
                                <li><b>Continue current medication</b>
                                    <ul>
                                        <li>Search for PREDSOL and prescribe a generic equivalent</li>
                                        <li>Prescribe <b>diltiazem</b> and select brand name (DILZEM)</li>
                                    </ul>
                                </li>
                            </ul>
                            <ul>
                                <li><b>Less common prescriptions</b>
                                    <ul>
                                        <li>Prescribe <b>chloramphenicol</b> by cutaneous route</li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeScenarios" runat="server" TargetControlID="Scenarios_ContentPanel"
                        ExpandControlID="Scenarios_HeaderPanel" CollapseControlID="Scenarios_HeaderPanel"
                        Collapsed="True" ImageControlID="Scenarios_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Scenarios section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Scenarios section" SuppressPostBack="True"
                        Enabled="True" />
                    <!-- Area for Scenarios ends here -->
                    <!-- Area for USAGE HINTS - GETTING STARTED begins here -->
                    <asp:Panel ID="Header_GettingStarted" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="ToggleImage_GettingStartedAccordion" runat="server" src="~/images/SFTheme/acc_h.png" />
                            Usage Hints - Getting Started
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Content_GettingStarted" runat="server" Style="overflow: hidden;" Height="0px">
                        <div class="last section">
                            <p>
                                The following steps show how to use the Search and Prescribe control to create a
                                very simple Silverlight application. The steps use a simplified data model that
                                would not be appropriate for a fully functioning search and prescribe system.</p>
                            <p>
                                <b>Note: </b>The steps assume additional logic to populate valid attributes for
                                a selected drug (for example routes, forms, strengths etc.)
                            </p>
                            <h3>
                                Create a Silverlight application and add references
                            </h3>
                            <ol>
                                <li>Create a new Silverlight application in either VS.NET or Blend</li>
                                <li>Add a reference to Microsoft.Cui.Controls.dll</li>
                            </ol>
                            <h3>
                                Add some classes to provide basic data
                            </h3>
                            <ol>
                                <li><b>Add a class named Drug.cs</b>
                                    <br />
                                    The following code describes a basic drug object, with members that can be populated
                                    from a drug data source. This object assumes that the possible attributes for a
                                    drug (for example routes, forms, strengths etc.) are known.
                                    <pre>
    public class Drug
    {
        // Basic drug information.
        public string Name { get; set; }

        // Attributes available for this drug.
        public string[] Routes { get; set; }
        public string[] Forms { get; set; }
        public string[] Strengths { get; set; }
        public string[] Dosages { get; set; }
        public string[] Frequencies { get; set; }
        public string[] StartingConditions { get; set; }
        public string[] AdministrationTimes { get; set; }
        public string[] Durations { get; set; }
    }
</pre>
                                </li>
                                <li><b>Add a class named TemplatePrescription.cs</b>
                                    <br />
                                    The following code describes a template prescription object. The class provides
                                    members that allow the selection of a strength, dose and frequency to be made with
                                    a single selection.
                                    <pre>
    public class TemplatePrescription
    {
        public string Dose { get; set; }
        public string Strength { get; set; }
        public string Frequency { get; set; }
        public bool IsAsRequired { get; set; }
    }
</pre>
                                </li>
                                <li><b>Add a class named Prescription.cs</b>
                                    <br />
                                    The following code describes a basic prescription object. The class contains members
                                    that store the selections (for example drug, route, form, strength etc.) as well
                                    as option members that provide a list of the valid options for each attribute, as
                                    well as the possible other options for each attribute. This example assumes that
                                    the logic is available to populate the lists of valid attributes and other attributes.
                                    <pre>
    public class Prescription
    {
        // Prescription selection members.
        public Drug Drug { get; set; }
        public string Route { get; set; }
        public bool IsFormMandatory { get; set; }
        public string Form { get; set; }
        public bool IsStrengthMandatory { get; set; }
        public TemplatePrescription TemplatePrescription { get; set; }
        public string Strength { get; set; }
        public string Dose { get; set; }
        public string Frequency { get; set; }
        public bool IsAsRequired { get; set; }
        public string StartingCondition { get; set; }
        public string AdministrationTimes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FirstDoseTime { get; set; }
        public string Duration { get; set; }

        // Prescription option members.
        public string[] ValidRoutes { get; set; }
        public string[] OtherRoutes { get; set; }
        public string[] ValidForms { get; set; }
        public string[] OtherForms { get; set; }
        public string[] ValidTemplatePrescriptions { get; set; }
        public string[] ValidStrengths { get; set; }
        public string[] OtherStrengths { get; set; }
        public string[] ValidDosages { get; set; }
        public string[] OtherDosages { get; set; }
        public string[] ValidFrequencies { get; set; }
        public string[] OtherFrequencies { get; set; }
        public string[] ValidStartingConditions { get; set; }
        public string[] ValidAdministrationTimes { get; set; }
        public DateTime[] ValidFirstDoseTimes { get; set; }
        public string[] ValidDurations { get; set; }
    }</pre>
                                </li>
                            </ol>
                            <h3>
                                Add the Search and Prescribe Control to the page</h3>
                            <ol>
                                <li>Add an XML namespace to the top of the XAML page
                                    <pre>
xmlns:Cui_Controls="clr-namespace:Microsoft.Cui.Controls;assembly=Microsoft.Cui.Controls"
</pre>
                                </li>
                                <li>Add an instance of the control to the page, set some initial properties and add
                                    some event handlers
                                    <pre>
    &lt;Cui_Controls:SearchAndPrescribeControl 
            x:Name="SearchAndPrescribeControl"
            IsDrugSearchButtonEnabled="True"
            VerticalAlignment="Top"
            CascadingListMaximumHeight="400"
            DrugSearchButtonClick="SearchAndPrescribeControl_DrugSearchButtonClick"
            Preview="SearchAndPrescribeControl_Preview"
            Authorize="SearchAndPrescribeControl_Authorize"
            Close="SearchAndPrescribeControl_Close"
            /&gt;
</pre>
                                </li>
                                <li>In the page's constructor method, give the Search and Prescribe Control a data context
                                    of a new prescription
                                    <pre>
        public MainPage()
        {
            InitializeComponent();
            this.SearchAndPrescribeControl.DataContext = new Prescription();
        }
</pre>
                                </li>
                            </ol>
                            <h3>
                                Enable searching for a drug</h3>
                            <ol>
                                <li>Add the following code to the page hosting the search and prescribe control, for
                                    example
                                    <pre>
	private void SearchAndPrescribeControl_DrugSearchButtonClick(object sender, RoutedEventArgs e)
	{
	    this.SearchAndPrescribeControl.PrimaryDrugsItemsSource = 
	        this.GetMatches(this.SearchAndPrescribeControl.DrugSearchText);
	
	    this.SearchAndPrescribeControl.IsShowingAllDrugResults = false;
	    this.SearchAndPrescribeControl.RefreshDrugsList();
	}
	
	private Drug[] GetMatches(string text)
	{
	    // Some logic to search for drugs.
	}
	</pre>
                                </li>
                                <li>Add a DataTemplate for displaying the drug search result
                                    <pre>
    &lt;DataTemplate x:Key="DrugDataTemplate"&gt;
        &lt;TextBlock Text="{Binding Name}"
                   FontWeight="Bold" /&gt;
    &lt;/DataTemplate&gt;
    
    &lt;Cui_Controls:SearchAndPrescribeControl 
            x:Name="SearchAndPrescribeControl"
            ...
            DrugItemTemplate="{StaticResource DrugDataTemplate}"
            /&gt;
</pre>
                                </li>
                            </ol>
                            <h3>
                                Bind the members of the Search and Prescribe Control</h3>
                            <ol>
                                <li>Add the following XAML properties to the Search and Prescribe Control to bind the
                                    members of the Precription class to the control. This will allow the control to
                                    read the possible options for selection and modify the selections in the Prescription.
                                    <pre>
    &lt;Cui_Controls:SearchAndPrescribeControl 
        x:Name="SearchAndPrescribeControl"
        ...
        SelectedDrug="{Binding Mode=TwoWay, Path=Drug}"
        SelectedRoute="{Binding Mode=TwoWay, Path=Route}"
        SelectedForm="{Binding Mode=TwoWay, Path=Form}"
        SelectedTemplatePrescription="{Binding Mode=TwoWay, Path=TemplatePrescription}"
        SelectedStrength="{Binding Mode=TwoWay, Path=Strength}"
        SelectedDose="{Binding Mode=TwoWay, Path=Dose}"
        SelectedFrequency="{Binding Mode=TwoWay, Path=Frequency}"
        SelectedAdministrationTimes="{Binding Mode=TwoWay, Path=AdministrationTimes}"
        SelectedStartDate="{Binding Mode=TwoWay, Path=StartDate}"
        SelectedFirstDoseTime="{Binding Mode=TwoWay, Path=FirstDoseTime}"
        SelectedDuration="{Binding Mode=TwoWay, Path=Duration}"
        
        ValidRoutesItemsSource="{Binding Path=ValidRoutes}"
        OtherRoutesItemsSource="{Binding Path=OtherRoutes}"
        ValidFormsItemsSource="{Binding Path=ValidForms}"
        OtherFormsItemsSource="{Binding Path=OtherForms}"
        ValidStrengthsItemsSource="{Binding Path=ValidStrengths}"
        OtherStrengthsItemsSource="{Binding Path=OtherStrengths}"
        ConciseDosagesItemsSource="{Binding Path=ValidDosages}"
        DetailedDosagesItemsSource="{Binding Path=OtherDosages}"
        ConciseFrequenciesItemsSource="{Binding Path=ValidFrequencies}"
        DetailedFrequenciesItemsSource="{Binding Path=OtherFrequencies}"
        ValidAdministrationTimesItemsSource="{Binding Path=ValidAdministrationTimes}"
        ValidFirstDoseTimesItemsSource="{Binding Path=ValidFirstDoseTimes}"
        ValidDurationsItemsSource="{Binding Path=ValidDurations}"
        /&gt;
</pre>
                                </li>
                            </ol>
                            <p>
                                Finally, adding handlers for the control's remaining events will allow the application
                                to handle Previewing, Closing and Authorising of the prescription.</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeGettingStarted" runat="server" TargetControlID="Content_GettingStarted"
                        ExpandControlID="Header_GettingStarted" CollapseControlID="Header_GettingStarted"
                        Collapsed="True" ImageControlID="ToggleImage_GettingStartedAccordion" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Getting Started section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Getting Started section" SuppressPostBack="True"
                        Enabled="True" />
                    <!-- Area for USAGE HINTS - GETTING STARTED ends here -->
                    <!-- Area for USAGE HINTS - DataModel begins here -->
                    <asp:Panel ID="DataModel_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="DataModel_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
                            USAGE HINTS - Data Model
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="DataModel_ContentPanel" runat="server" Style="overflow: hidden;" Height="0px">
                        <div class="last section">
                            <p>
                                The Search and Prescribe Control provides the user interface elements for searching
                                for a drug, and completing a prescription. However, the logic used to provide valid
                                attributes such as routes, forms, strengths, dosages etc. is provided by a data
                                model.</p>
                            <p>
                                The information below explains how the sample data and data model have been structured.</p>
                            <p>
                                <b>Note: </b>The sample data and data model are not representative of those that
                                should be used in a production search and prescribe system. They are used here to
                                demonstrate the control's functionality.</p>
                            <h3>
                                XML Data</h3>
                            <p>
                                The data for the sample application is stored in an XML file. The main body of the
                                XML data is made up of the drug data used in the sample scenarios. Below is an example
                                block of XML that contains some of the data for <b>paracetamol</b>.</p>
                            <pre>
    &lt;Drug Name="paracetamol" CommonlyPrescribed="True"&gt;
      &lt;Routes&gt;
        &lt;Route Value="oral" /&gt;
        &lt;Route Value="rectal" /&gt;
        ...
      &lt;/Routes&gt;
      &lt;TemplatePrescriptions&gt;
        &lt;TemplatePrescription Route="oral" Dose="500 mg" 
                              Frequency="four times a day" 
                              AdministrationTimes="08:00;12:00;18:00;22:00" 
                              Duration="Unlimited" /&gt;
        &lt;TemplatePrescription Route="rectal" Dose="500 mg" 
                              Frequency="four times a day" 
                              AdministrationTimes="08:00;12:00;18:00;22:00" 
                              Duration="Unlimited" /&gt;
        ...
      &lt;/TemplatePrescriptions&gt;
      &lt;Forms&gt;
        &lt;Form Value="tablet" ValidRoutes="oral" 
              ValidStrengths="" /&gt;
        &lt;Form Value="oral suspension" ValidRoutes="oral" 
              ValidStrengths="250 mg in 5 mL" /&gt;
        &lt;Form Value="suppository" ValidRoutes="rectal" 
              ValidStrengths="" /&gt;
        ...
      &lt;/Forms&gt;
      &lt;Strengths&gt;
        &lt;Strength Value="250 mg in 5 mL" ValidRoutes="oral" 
                  ValidForms="oral suspension"  /&gt;
        &lt;Strength Value="500 mg in 100 mL" ValidRoutes="intravenous" 
                  ValidForms="solution for injection" /&gt;
      &lt;/Strengths&gt;
      &lt;Dosages&gt;
        &lt;Dose Value="500 mg" ValidFrequencies="four times a day" /&gt;
        ...
      &lt;/Dosages&gt;
      &lt;Frequencies&gt;
        &lt;Frequency Value="four times a day" 
                   AdministrationTimes="08:00;12:00;16:00;20:00" 
                   ValidDosages="500 mg" Duration="Unlimited" /&gt;
      &lt;/Frequencies&gt;
    &lt;/Drug&gt;
</pre>
                            <p>
                                The drug data contains name information about the drug, some of the licensed routes
                                and some template prescriptions for the routes. The data also contains possible
                                attributes for the drug including forms, strengths, dosages and frequencies. Each
                                of these attributes contains a list of valid other attributes used to determine
                                if a selection is valid.</p>
                            <p>
                                For example, the strength data below specifies that this strength is valid when
                                'oral' is selected as a route and/or 'oral suspension' is selected as a form.</p>
                            <pre>
        &lt;Strength Value="250 mg in 5 mL" ValidRoutes="oral" 
                  ValidForms="oral suspension"  /&gt;
</pre>
                            <h3>
                                The Class Structure</h3>
                            <p>
                                The XML drug data is de-serialized into a drug class, which contains all of the
                                drug information and attributes. The diagram below shows the members of the drug
                                class.</p>
                            <div style="text-align: center; width: 100%;">
                                <img src="Images/SP_Drug.jpg" alt="An image of the drug class" />
                            </div>
                            <p>
                                The template prescriptions use a template prescription class, which contains each
                                of the drug attributes that will be set if that template prescription is selected.
                                The template prescription class is pictured below.</p>
                            <div style="text-align: center; width: 100%;">
                                <img src="Images/SP_TemplatePrescription.jpg" alt="An image of the template prescription class" />
                            </div>
                            <p>
                                The other drug attributes, for example form, strength, dose, frequency etc. have
                                a simple object hierarchy that allows each attribute to contain a list of validation
                                values, e.g. ValidBrands, ValidDosages etc. The validation values are used to determine
                                which attributes are valid selections based on other selections in the prescription.</p>
                            <div style="text-align: center; width: 100%;">
                                <img src="Images/SP_DrugAttributes.jpg" alt="An image of the class structure for the drug attributes" />
                            </div>
                            <h3>
                                The Prescription Class</h3>
                            <p>
                                The Prescription class contains the all of the selections for the prescription,
                                as well as the logic to determine which are the valid attributes.</p>
                            <p>
                                The image below shows the prescription class members that store the selections for
                                the prescription, and the properties exposed for providing lists of valid attributes.</p>
                            <div style="text-align: center; width: 100%;">
                                <img src="Images/SP_Prescription.jpg" alt="An image of the class structure for the drug attributes" />
                            </div>
                            <p>
                                The following code example shows how the valid dosages are determined:
                            </p>
                            <pre>
    public Dose[] ValidDosages
    {
        get
        {
            if (this.Drug != null &amp;&amp;
                this.Route != null &amp;&amp; 
                (!this.Route.MandatoryForm || this.Form != null) &amp;&amp; 
                this.Drug.Dosages != null)
            {
                DrugElement[] sourceAttributes = this.GetValidDrugElements(this.Drug.Dosages);

                // Convert sourceAttributes to a Dose[]
                // and return...
            }

            return null;
        }
    }
    
    private DrugElement[] GetValidDrugElements(DrugElement[] sourceAttributes)
    {
        DrugElement[] attributes = sourceAttributes;
        if (this.Route != null)
        {
            attributes = (from attribute in attributes
                          where attribute.ValidRoutes == null || 
                          attribute.ValidRoutes.Contains(this.Route.DisplayValue)
                          select attribute).ToArray();
        }

        if (this.Form != null &amp;&amp; !this.Form.IsCustomValue)
        {
            attributes = (from attribute in attributes
                          where attribute.ValidForms == null || 
                          attribute.ValidForms.Contains(this.Form.Value.Trim())
                          select attribute).ToArray();
        }

        // Continue for the other selected attributes.

        return attributes;
    }
</pre>
                            <p>
                                The GetValidDrugElements(...) method uses LINQ to filter down the source attributes
                                (in this case, all of the dosages for the selected drug) based on the selections
                                for the other attributes. The code above shows that if a route is selected, and
                                the dose doesn't contain the route in the ValidRoutes, it is not returned as a valid
                                attribute. This is then repeated for form, strength, frequency etc.</p>
                            <p>
                                The <b>Getting Started</b> section above shows how to implement a simple data model
                                with the control.</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeDataModel" runat="server" TargetControlID="DataModel_ContentPanel"
                        ExpandControlID="DataModel_HeaderPanel" CollapseControlID="DataModel_HeaderPanel"
                        Collapsed="True" ImageControlID="DataModel_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the DataModel section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the DataModel section" SuppressPostBack="True"
                        Enabled="True" />
                    <!-- Area for USAGE HINTS - DataModel ends here -->
                    <!-- Area for USAGE HINTS - Consuming the control begins here -->
                    <asp:Panel ID="ConsumingTheControl_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="ConsumingTheControl_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
                            Usage hints - Consuming the control
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="ConsumingTheControl_ContentPanel" runat="server" Style="overflow: hidden;"
                        Height="0px">
                        <div class="last section">
                            <p>
                                <p>
                                    While reviewing the programmability of the Search and Prescribe control, an understanding
                                    of the core object implementation is required. The Search and Prescribe control
                                    is implemented by the class SearchAndPrescribeControl. The SearchAndPrescribeControl
                                    class is a composite control and inherits from the base class SearchAndPrescribeBase,
                                    which in turn inherits from the base class Control.</p>
                                <p>
                                    To consume the control within a Silverlight application, a reference to the Search
                                    and Prescribe control needs to be created:</p>
                                <pre>
    &lt;Cui_Controls:SearchAndPrescribeControl 
            x:Name="SearchAndPrescribeControl"
            /&gt;
</pre>
                                <p>
                                    The namespace element that needs to be added within the XAML is:</p>
                                <pre>
xmlns:Cui_Controls="clr-namespace:Microsoft.Cui.Controls;assembly=Microsoft.Cui.Controls"
</pre>
                            </p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeConsumingTheControl" runat="server"
                        TargetControlID="ConsumingTheControl_ContentPanel" ExpandControlID="ConsumingTheControl_HeaderPanel"
                        CollapseControlID="ConsumingTheControl_HeaderPanel" Collapsed="True" ImageControlID="ConsumingTheControl_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Consuming the Control section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Consuming the Control section"
                        SuppressPostBack="True" Enabled="True" />
                    <!-- Area for USAGE HINTS - Consuming the control ends here -->
                    <!-- Area for USAGE HINTS - MEMBERS begins here -->
                    <asp:Panel ID="Members_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="Prop_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
                            Usage Hints - Members
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Members_ContentPanel" runat="server" Style="overflow: hidden;" Height="0px">
                        <div class="last section">
                            <p>
                                <b>SearchAndPrescribeBase Members</b></p>
                            <table class="desc">
                                <thead>
                                    <tr>
                                        <td valign="top" width="300">
                                            <p>
                                                Property</p>
                                        </td>
                                        <td valign="top" width="430">
                                            <p>
                                                Description</p>
                                        </td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td valign="top">
                                        AdministrationTimesItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for an administration times item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        AdministrationTimesLabel
                                    </td>
                                    <td valign="top">
                                        Gets or sets the administration times label
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        BrandItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for a brand item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        BrandedRoutesItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the branded routes items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        ConciseDosagesItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the concise view dosages items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        ConciseFormsItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the concise view forms items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        ConciseFrequenciesItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the concise view frequencies items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        ContinuingLabel
                                    </td>
                                    <td valign="top">
                                        Gets or sets the continuing label
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        CustomDoseItem
                                    </td>
                                    <td valign="top">
                                        Gets or sets the custom dose item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        CustomDoseItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for the custom dose item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        CustomDurationItem
                                    </td>
                                    <td valign="top">
                                        Gets or sets the custom duration item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        CustomDurationItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for the custom duration item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        CustomFrequencyItem
                                    </td>
                                    <td valign="top">
                                        Gets or sets the custom frequency item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        CustomFrequencyItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for the custom frequency item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        CustomReasonForPrescribingItem
                                    </td>
                                    <td valign="top">
                                        Gets or sets the custom reason for prescribing item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        CustomReasonForPrescribingItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for the custom reason for prescribing item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        CustomSiteItem
                                    </td>
                                    <td valign="top">
                                        Gets or sets the custom site item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        CustomSiteItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for the custom site item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        CustomStartingConditionItem
                                    </td>
                                    <td valign="top">
                                        Gets or sets the custom starting condition item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        CustomStartingConditionItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for the custom starting condition item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        DetailedDosagesItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the detailed view dosages items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        DetailedFrequenciesItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the detailed view frequencies items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        DoseItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for a dose item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        DrugsItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the drugs items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        DrugItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for a drug
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        DurationItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for a duration
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        FirstDoseTimeItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for a first dose item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        FrequencyItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for a frequency item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        FormItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for a form item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        IsAsRequired
                                    </td>
                                    <td valign="top">
                                        Gets or sets a value indicating whether the prescription is as required
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        IsAuthorizable
                                    </td>
                                    <td valign="top">
                                        Gets or sets a value indicating whether the prescription is authorizable
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        IsBrandMandatory
                                    </td>
                                    <td valign="top">
                                        Gets or sets a value indicating whether the brand is mandatory
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        IsFormMandatory
                                    </td>
                                    <td valign="top">
                                        Gets or sets a value indicating whether the form is mandatory
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        IsOnceOnly
                                    </td>
                                    <td valign="top">
                                        Gets or sets a value indicating whether the prescription is once only
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        IsSiteMandatory
                                    </td>
                                    <td valign="top">
                                        Gets or sets a value indicating whether the site is mandatory
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        IsStrengthMandatory
                                    </td>
                                    <td valign="top">
                                        Gets or sets a value indicating whether the strength is mandatory
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        IsUnlicensed
                                    </td>
                                    <td valign="top">
                                        Gets or sets a value indicating whether the prescription is unlicensed
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        OtherFormsItemsHeader
                                    </td>
                                    <td valign="top">
                                        Gets or sets the list header for the other form items
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        OtherFormsItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the other forms items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        OtherRoutesItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the other routes items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        OtherStrengthsItemsHeader
                                    </td>
                                    <td valign="top">
                                        Gets or sets the list header for the other strength items
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        OtherStrengthsItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the other strengths items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        PrimaryDrugsItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the primary drugs item source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        PrimaryDrugsItemsHeader
                                    </td>
                                    <td valign="top">
                                        Gets or sets the list header for the primary drug items
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        RouteItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for a route item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SecondaryDrugsItemsHeader
                                    </td>
                                    <td valign="top">
                                        Gets or sets the list header for the secondary drug items
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SecondaryDrugsItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the secondary drugs items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedAdministrationTimes
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected administration times
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedBrand
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected brand
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedDose
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected dose
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedDrug
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected drug
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedDuration
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected duration
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedFrequency
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected frequency
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedMethod
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected method
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedDrugItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for the selected drug item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedFirstDoseTime
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected first dose time
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedForm
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected form
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedReasonForPrescribing
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected reason for prescribing
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedRoute
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected route
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedRouteItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for the selected route item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedSite
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected site
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedStartDate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected start date
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedStartingCondition
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected starting condition
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedStrength
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected strength
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedTemplatePrescription
                                    </td>
                                    <td valign="top">
                                        Gets or sets the selected template prescription
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        StartingConditionItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for a starting condition item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        StartingLabel
                                    </td>
                                    <td valign="top">
                                        Gets or sets the starting date label
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        StrengthItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for a strength item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        TemplatePrescriptionItemTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for a template prescription item
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        UnlicensedReason
                                    </td>
                                    <td valign="top">
                                        Gets or sets the unlicensed reason
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        ValidAdministrationTimesItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the valid administration times items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        ValidBrandsItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the valid brands items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        ValidDurationsItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the valid durations items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        ValidFirstDoseTimesItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the valid first dose times items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        ValidFormsItemsHeader
                                    </td>
                                    <td valign="top">
                                        Gets or sets the list header for the valid forms items
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        ValidFormsItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the valid forms items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        ValidRoutesItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the valid routes items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        ValidStartingConditionsItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the valid starting conditions items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        ValidStrengthsItemsHeader
                                    </td>
                                    <td valign="top">
                                        Gets or sets the list header for the valid strengths items header
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        ValidStrengthsItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the valid strengths items source
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        ValidTemplatePrescriptionsItemsSource
                                    </td>
                                    <td valign="top">
                                        Gets or sets the valid template prescription items source
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <p>
                                <b>SearchAndPrescribe Members</b></p>
                            <table class="desc">
                                <thead>
                                    <tr>
                                        <td valign="top" width="300">
                                            <p>
                                                Property</p>
                                        </td>
                                        <td valign="top" width="430">
                                            <p>
                                                Description</p>
                                        </td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td valign="top">
                                        BasicDrugTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for the selected drug item (for use in the detailed
                                        view)
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        BasicRouteTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for the selected route item (for use in the detailed
                                        view)
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        CascadingListMaximumHeight
                                    </td>
                                    <td valign="top">
                                        Gets or sets the maximum height of the cascading list
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        DetailedViewMaxHeight
                                    </td>
                                    <td valign="top">
                                        Gets or sets the maximum height of the detailed view
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        DrugSearchText
                                    </td>
                                    <td valign="top">
                                        Gets or sets the entered drug search text
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        DrugsListCollapsedItemCount
                                    </td>
                                    <td valign="top">
                                        Gets or sets the initial number of drug results displayed
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        DrugsResultsListMessage
                                    </td>
                                    <td valign="top">
                                        Gets or sets the drug results list message (displayed when there are no results)
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        DrugsResultsListMessageTemplate
                                    </td>
                                    <td valign="top">
                                        Gets or sets the data template for displaying the drug results list message
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        IsConciseDetailedViewToggleEnabled
                                    </td>
                                    <td valign="top">
                                        Gets or sets a value indicating whether the concise / detailed view toggle is enabled
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        IsDetailedView
                                    </td>
                                    <td valign="top">
                                        Gets or sets a value indicating whether the control is in the detailed view
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        IsDrugSearchButtonEnabled
                                    </td>
                                    <td valign="top">
                                        Gets or sets a value indicating whether the drug search button is enabled
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        IsShowingAllDrugResults
                                    </td>
                                    <td valign="top">
                                        Gets or sets a value indicating whether all of the drugs results are displayed
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <p>
                                <b>SearchAndPrescribe Methods</b></p>
                            <table class="desc">
                                <thead>
                                    <tr>
                                        <td valign="top" width="300">
                                            <p>
                                                Method</p>
                                        </td>
                                        <td valign="top" width="430">
                                            <p>
                                                Description</p>
                                        </td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td valign="top">
                                        FocusCloseButton
                                    </td>
                                    <td valign="top">
                                        Gives focus to the close button
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        FocusDrugSearchTextBox
                                    </td>
                                    <td valign="top">
                                        Gives focus to the drug search text box
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        FocusNextControl
                                    </td>
                                    <td valign="top">
                                        Gives focus to the next mandatory field. Returns true if a control is given focus,
                                        false otherwise
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        FocusPreviewButton
                                    </td>
                                    <td valign="top">
                                        Gives focus to the preview button
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        RefreshDrugsList
                                    </td>
                                    <td valign="top">
                                        Refreshes the results in the drug list. Use after updating the primary / secondary
                                        headers or items sources
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        Reset
                                    </td>
                                    <td valign="top">
                                        Resets the control
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <p>
                                <b>SearchAndPrescribe Events</b></p>
                            <table class="desc">
                                <thead>
                                    <tr>
                                        <td valign="top" width="300">
                                            <p>
                                                Event</p>
                                        </td>
                                        <td valign="top" width="430">
                                            <p>
                                                Description</p>
                                        </td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td valign="top">
                                        IsAsRequiredChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when IsAsRequired changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        IsAuthorizableChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when IsAuthorizable changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        IsOnceOnlyChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when IsOnceOnly changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        IsUnlicensedChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when IsUnlicensed changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedAdministrationTimesChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected administration times changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedBrandChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected brand changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedDoseChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected dose changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedDrugChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected drug changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedDurationChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected duration changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedFirstDoseTimeChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected first dose time changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedFormChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected form changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedFrequencyChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected frequency changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedMethodChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected method changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedReasonForPrescribingChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected reason for prescribing changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedRouteChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected route changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedSiteChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected site changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedStartDateChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected start date changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedStartingConditionChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected starting condition changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedStrengthChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected strength changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        SelectedTemplatePrescriptionChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the selected template prescription changes
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        UnlicensedReasonChanged
                                    </td>
                                    <td valign="top">
                                        Occurs when the unlicensed reason changes
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeMembers" runat="server" TargetControlID="Members_ContentPanel"
                        ExpandControlID="Members_HeaderPanel" CollapseControlID="Members_HeaderPanel"
                        Collapsed="True" ImageControlID="Prop_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Members section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Members section" SuppressPostBack="True" Enabled="True" />
                    <!-- Area for USAGE HINTS - MEMBERS ends here -->
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
                        <img class="controls_border" alt="Search and Prescribe WPF control screenshot" title="Search and Prescribe WPF control screenshot"
                            runat="server" src="~/Components/Images/SearchAndPrescribe.jpg" />
                    </div>
                    <br />
                    <p>
                        Further information on this control, and the full source code, can be found in the
                        Microsoft Health Common User Interface Toolkit, which can be downloaded from our
                        <a href="http://www.codeplex.com/mscui/Release/ProjectReleases.aspx" target="_blank"
                            title="Link to releases page on the CodePlex site (New Window)">CodePlex</a>
                        site.
                    </p>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>
