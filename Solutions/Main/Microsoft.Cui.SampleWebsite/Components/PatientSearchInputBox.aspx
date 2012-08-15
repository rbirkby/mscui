<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="ComponentsPatientSearchInputBox" Title="Untitled Page" CodeBehind="PatientSearchInputBox.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The PatientSearchInputBox control allows you to enter textual search data for a
            patient in what appears to be a standard textbox. A parsing method allows the text
            search criteria to be parsed and exposed as properties.
        </p>
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelASPNET" HeaderText="<a id='patientSearchInputASPNETTab' href=javascript:TabClick('patientSearchInputASPNETTab'); title='ASP.NET Tab' alt='ASP.NET Tab'>ASP.NET</a>">
            <ContentTemplate>
                <br />
                Example ASP.NET control (embedded):
                <br />
                <br />
                <asp:Panel CssClass="demoCCarea" ID="demoPanel1" runat="server" Width="627px" DefaultButton="parseButton">
                    <em>(e.g. mr subramanyan chandrasekhar 14/07/1945 SO50 7TH)</em><input onclick="javascript:ToggleKeyStrokeUpdate();"
                        id="toggleClientParseCheckBox" name="toggleClientParseCheckBox" type="checkbox"
                        style="margin-left: 10px" /><label for="toggleClientParseCheckBox">Update on every keystroke</label>
                    <p />
                    <NhsCui:PatientSearchInputBox runat="server" ID="PatientSearchInputBox1" Width="400px"
                        Style="margin-right: 5px" MaximumAge="130" InformationDelimiter=",">
                    </NhsCui:PatientSearchInputBox>
                    <asp:Button ID="parseButton" runat="server" Text="GO" OnClick="ParseButton_Click" />
                    <asp:Table ID="Table1" runat="server" EnableViewState="False">
                        <asp:TableRow ID="TableRow1" runat="server">
                            <asp:TableCell ID="TableCell1" runat="server" HorizontalAlign="Right">
                                <asp:Label ID="FamilyNameLabel" runat="server" Text="Family name:" />
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell2" runat="server">
                                <asp:TextBox ID="FamilyNameTextBox" runat="server" ReadOnly="True" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow2" runat="server">
                            <asp:TableCell ID="TableCell3" runat="server" HorizontalAlign="Right">
                                <asp:Label ID="GivenNameLabel" runat="server" Text="Given name:" />
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell4" runat="server">
                                <asp:TextBox ID="GivenNameTextBox" runat="server" ReadOnly="True" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow3" runat="server">
                            <asp:TableCell ID="TableCell5" runat="server" HorizontalAlign="Right">
                                <asp:Label ID="NHSNumberLabel" runat="server" Text="NHS Number:" />
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell6" runat="server">
                                <asp:TextBox ID="NHSNumberTextBox" runat="server" ReadOnly="True" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow4" runat="server">
                            <asp:TableCell ID="TableCell7" runat="server" HorizontalAlign="Right">
                                <asp:Label ID="AgeLabel" runat="server" Text="Age:" />
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell8" runat="server">
                                <asp:TextBox ID="AgeTextBox" runat="server" ReadOnly="True" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow5" runat="server">
                            <asp:TableCell ID="TableCell9" runat="server" HorizontalAlign="Right">
                                <asp:Label ID="DOBLabel" runat="server" Text="Date of Birth:" />
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell10" runat="server">
                                <asp:TextBox ID="DOBTextBox" runat="server" ReadOnly="True" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow6" runat="server">
                            <asp:TableCell ID="TableCell11" runat="server" HorizontalAlign="Right">
                                <asp:Label ID="GenderLabel" runat="server" Text="Gender:" />
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell12" runat="server">
                                <asp:TextBox ID="GenderTextBox" runat="server" ReadOnly="True" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow7" runat="server">
                            <asp:TableCell ID="TableCell13" runat="server" HorizontalAlign="Right">
                                <asp:Label ID="TitleLabel" runat="server" Text="Title:" />
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell14" runat="server">
                                <asp:TextBox ID="TitleTextBox" runat="server" ReadOnly="True" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow8" runat="server">
                            <asp:TableCell ID="TableCell15" runat="server" HorizontalAlign="Right">
                                <asp:Label ID="AddressLabel" runat="server" Text="Address:" />
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell16" runat="server">
                                <asp:TextBox ID="AddressTextBox" runat="server" ReadOnly="True" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow9" runat="server">
                            <asp:TableCell ID="TableCell17" runat="server" HorizontalAlign="Right">
                                <asp:Label ID="PostcodeLabel" runat="server" Text="Postcode:" />
                            </asp:TableCell>
                            <asp:TableCell ID="TableCell18" runat="server">
                                <asp:TextBox ID="PostcodeTextBox" runat="server" ReadOnly="True" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
                <br />
                <!-- Area for Usage Hints -->
                <asp:Panel ID="UsageHints_HeaderPanel" runat="server" Style="cursor: pointer;">
                    <div class="heading">
                        <input type="image" id="usageHints_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
                        Usage Hints
                    </div>
                </asp:Panel>
                <asp:Panel ID="UsageHints_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                    <div class="section">
                        <ul>
                            <li>Enter search information for the patient in the entrybox </li>
                            <li>Check Update on every keystroke if you want the search to be performed as you type
                            </li>
                            <li>Click GO to perform your search if you do not have Update on every keystroke checked
                            </li>
                            <li>The data entered in the entrybox is parsed and the results displayed in the fields
                                associated with different data properties</li>
                        </ul>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeUsageHints" runat="Server" TargetControlID="UsageHints_ContentPanel"
                    ExpandControlID="UsageHints_HeaderPanel" CollapseControlID="UsageHints_HeaderPanel"
                    Collapsed="True" ExpandDirection="Vertical" ImageControlID="usageHints_ToggleImage"
                    ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Clikc to collapse the Usage Hints section"
                    CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Usage Hints section"
                    SuppressPostBack="true" />
                <!-- Area for Properties -->
                <asp:Panel ID="Properties_HeaderPanel" runat="server" Style="cursor: pointer;">
                    <div class="heading">
                        <input type="image" id="properties_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
                        Properties
                    </div>
                </asp:Panel>
                <asp:Panel ID="Properties_ContentPanel" runat="server" Style="overflow: hidden;"
                    Height="0px">
                    <div class="section">
                        <p>
                            The PatientSearchInputBox control is initialized with the following code:</p>
                        <p>
                        </p>
                        <pre>&lt;NhsCui:PatientSearchInputBox ID="PatientSearchInputBox1" Width="400px" 
    MaximumAge="130" InformationDelimiter="," runat="server" /&gt;
        </pre>
                        <ul>
                            <li><strong>Address</strong> &ndash; gets or sets the address</li>
                            <li><strong>Age</strong> &ndash; gets or sets the age</li>
                            <li><strong>AgeUpper</strong> &ndash; the upper value in an age range</li>
                            <li><strong>CommonFamilyNames</strong> &ndash; a list of common family names</li>
                            <li><strong>DateOfBirth</strong> &ndash; gets or sets the date of birth</li>
                            <li><strong>DateOfBirthUpper</strong> &ndash; the upper value in a date of birth range</li>
                            <li><strong>EndGroupDelimiter</strong> &ndash; the character that is used to delimit
                                the end of a group of words</li>
                            <li><strong>FamilyName</strong> &ndash; gets or sets the family name</li>
                            <li><strong>Gender</strong> &ndash; gets or sets the gender</li>
                            <li><strong>GivenName</strong> &ndash; gets or sets the given name</li>
                            <li><strong>InformationDelimiter</strong> &ndash; the character that is used to delimit
                                the end of a group of words </li>
                            <li><strong>InformationFormat</strong> &ndash; the list of PatientSearch. Information
                                enumeration values that are used to parse the Text property</li>
                            <li><strong>IsCommonFamilyName</strong> &ndash; &ldquo;True&rdquo; if FamilyName is
                                found in CommonFamilyNames</li>
                            <li><strong>IsDateOfBirthAgeMismatch</strong> &ndash; &ldquo;True&rdquo; if both DateOfBirth
                                and Age have been entered and the number of years of the Age is not the same number
                                of years for the DateOfBirth based on the current date; otherwise &ldquo;False&rdquo;</li>
                            <li><strong>IsGenderTitleMismatch</strong> &ndash; &ldquo;True&rdquo; if both Gender
                                and Title have been entered and the Title has a PatientSearch.Gender of Male or
                                Female and the Gender's PatientSearch.Gender is not the same; otherwise &ldquo;False&rdquo;</li>
                            <li><strong>IsMandatoryInformationEntered</strong> &ndash; &ldquo;True&rdquo; if all
                                of the values in MandatoryInformation are not the default values; if MandatoryInformation
                                is null, IsMandatoryInformationEntered is &ldquo;False&rdquo;</li>
                            <li><strong>MandatoryInformation</strong> &ndash; the list of PatientSearch.Information
                                enumeration values that are mandatory</li>
                            <li><strong>MaximumAge</strong> &ndash; gets or sets the maximum age recognized by the
                                parser</li>
                            <li><strong>NhsNumber</strong> &ndash; gets or sets the patient identification number
                            </li>
                            <li><strong>Postcode</strong> &ndash; gets or sets the postcode </li>
                            <li><strong>StartGroupDelimiter</strong> &ndash; the character that is used to delimit
                                the end of a group of words</li>
                            <li><strong>Text</strong> &ndash; the patient search criteria to be parsed</li>
                            <li><strong>Title</strong> &ndash; gets or sets the title</li>
                            <li><strong>Titles</strong> &ndash; gets or sets a list of Title objects</li>
                            <li><strong>UnmatchedText</strong> &ndash; the remaining text that could not be matched
                                after the parsing process has identified all other values</li>
                        </ul>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeProperties" runat="Server" TargetControlID="properties_ContentPanel"
                    ExpandControlID="properties_HeaderPanel" CollapseControlID="properties_HeaderPanel"
                    Collapsed="True" ExpandDirection="Vertical" ImageControlID="properties_ToggleImage"
                    ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Properties section"
                    CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Properties section"
                    SuppressPostBack="true" />
                <!-- Area for Additional Info -->
                <asp:Panel ID="AdditionalInfo_HeaderPanel" runat="server" Style="cursor: pointer;">
                    <div class="heading">
                        <input type="image" id="AdditionalInfo_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                        Additional Information
                    </div>
                </asp:Panel>
                <asp:Panel ID="AdditionalInfo_ContentPanel" runat="server" Style="overflow: hidden;
                    height: 0px">
                    <div class="last section">
                        All the PatientSearchInputBox properties are read-only apart from CommonFamilyNames,
                        EndGroupDelimiter, InformationDelimiter, InformationFormat, MandatoryInformation,
                        MaximumAge, StartGroupDelimiter and Titles.
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeAdditionalInfo" runat="Server" TargetControlID="AdditionalInfo_ContentPanel"
                    ExpandControlID="AdditionalInfo_HeaderPanel" CollapseControlID="AdditionalInfo_HeaderPanel"
                    Collapsed="True" ExpandDirection="Vertical" ImageControlID="AdditionalInfo_ToggleImage"
                    ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                    CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                    SuppressPostBack="true" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelWinformsControl" HeaderText="<a id='patientSearchInputWinFormsTab' href=javascript:TabClick('patientSearchInputWinFormsTab'); title='WinForms Tab' alt='WinForms Tab'>WinForms</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WinForms control (screenshot):
                    <br />
                    <br />
                    <img class="controls_border" alt="PatientSearchInputBox WinForms control screenshot" title="PatientSearchInputBox WinForms control screenshot" runat="server" src="~/Components/Images/patientsearchinputbox.GIF" />
                    <br />
                    <br />
                    <p>
                        The full source code for this control can be found in the
                        Microsoft Health Common User Interface Toolkit, which can be downloaded from our
                        <a href="http://www.codeplex.com/mscui/Release/ProjectReleases.aspx" target="_blank"
                            title="Link to releases page on the CodePlex site (New Window)">CodePlex</a>
                        site.
                    </p>
                    <!-- Area for Winforms Usage Section -->
                    <asp:Panel ID="WinformsUsage_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="WinformsUsage_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Usage Hints
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="WinformsUsage_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
                        <ul>
                            <li>Enter search information for the patient in the entrybox </li>
                            <li>Check Update on every keystroke if you want the search to be performed as you type
                            </li>
                            <li>Click GO to perform your search if you do not have Update on every keystroke checked
                            </li>
                            <li>The data entered in the entrybox is parsed and the results displayed in the fields
                                associated with different data properties</li>
                        </ul>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="WinformsUsage_Extender" runat="Server" TargetControlID="WinformsUsage_ContentPanel"
                        ExpandControlID="WinformsUsage_HeaderPanel" CollapseControlID="WinformsUsage_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="WinformsUsage_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Properties section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Properties section"
                        SuppressPostBack="true" />                                       
                    <!-- Area for Winforms Properties Section -->
                    <asp:Panel ID="WinformsProps_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="WinformsProps_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Properties
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="WinformsProps_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                    <div class="section">
                        <ul>
                            <li><strong>Address</strong> &ndash; gets or sets the address</li>
                            <li><strong>Age</strong> &ndash; gets or sets the age</li>
                            <li><strong>AgeUpper</strong> &ndash; the upper value in an age range</li>
                            <li><strong>CommonFamilyNames</strong> &ndash; a list of common family names</li>
                            <li><strong>DateOfBirth</strong> &ndash; gets or sets the date of birth</li>
                            <li><strong>DateOfBirthUpper</strong> &ndash; the upper value in a date of birth range</li>
                            <li><strong>EndGroupDelimiter</strong> &ndash; the character that is used to delimit
                                the end of a group of words</li>
                            <li><strong>FamilyName</strong> &ndash; gets or sets the family name</li>
                            <li><strong>Gender</strong> &ndash; gets or sets the gender</li>
                            <li><strong>GivenName</strong> &ndash; gets or sets the given name</li>
                            <li><strong>InformationDelimiter</strong> &ndash; the character that is used to delimit
                                the end of a group of words </li>
                            <li><strong>InformationFormat</strong> &ndash; the list of PatientSearch. Information
                                enumeration values that are used to parse the Text property</li>
                            <li><strong>IsCommonFamilyName</strong> &ndash; &ldquo;True&rdquo; if FamilyName is
                                found in CommonFamilyNames</li>
                            <li><strong>IsDateOfBirthAgeMismatch</strong> &ndash; &ldquo;True&rdquo; if both DateOfBirth
                                and Age have been entered and the number of years of the Age is not the same number
                                of years for the DateOfBirth based on the current date; otherwise &ldquo;False&rdquo;</li>
                            <li><strong>IsGenderTitleMismatch</strong> &ndash; &ldquo;True&rdquo; if both Gender
                                and Title have been entered and the Title has a PatientSearch.Gender of Male or
                                Female and the Gender's PatientSearch.Gender is not the same; otherwise &ldquo;False&rdquo;</li>
                            <li><strong>IsMandatoryInformationEntered</strong> &ndash; &ldquo;True&rdquo; if all
                                of the values in MandatoryInformation are not the default values; if MandatoryInformation
                                is null, IsMandatoryInformationEntered is &ldquo;False&rdquo;</li>
                            <li><strong>MandatoryInformation</strong> &ndash; the list of PatientSearch.Information
                                enumeration values that are mandatory</li>
                            <li><strong>MaximumAge</strong> &ndash; gets or sets the maximum age recognized by the
                                parser</li>
                            <li><strong>NhsNumber</strong> &ndash; gets or sets the patient identification number
                            </li>
                            <li><strong>Postcode</strong> &ndash; gets or sets the postcode </li>
                            <li><strong>StartGroupDelimiter</strong> &ndash; the character that is used to delimit
                                the end of a group of words</li>
                            <li><strong>Text</strong> &ndash; the patient search criteria to be parsed</li>
                            <li><strong>Title</strong> &ndash; gets or sets the title</li>
                            <li><strong>Titles</strong> &ndash; gets or sets a list of Title objects</li>
                            <li><strong>UnmatchedText</strong> &ndash; the remaining text that could not be matched
                                after the parsing process has identified all other values</li>
                        </ul>
                    </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="WinformsProps_Extender" runat="Server" TargetControlID="WinformsProps_ContentPanel"
                        ExpandControlID="WinformsProps_HeaderPanel" CollapseControlID="WinformsProps_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="WinformsProps_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Properties section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Properties section"
                        SuppressPostBack="true" />                    
                        <!-- Area for Winforms Additional Information Section -->
                        <asp:Panel ID="WinformsAddInfo_HeaderPanel" runat="server" Style="cursor: pointer;">
                            <div class="heading">
                                <input type="image" id="WinformsAddInfo_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                                Additional Information
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="WinformsAddInfo_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                            <div class="last section">
                                <p>
                                    All the PatientSearchInputBox properties are read-only apart from CommonFamilyNames,
                                    EndGroupDelimiter, InformationDelimiter, InformationFormat, MandatoryInformation,
                                    MaximumAge, StartGroupDelimiter and Titles.
                                </p>
                            </div>
                        </asp:Panel>
                        <ajaxToolkit:CollapsiblePanelExtender ID="WinformsAddInfo_Extender" runat="Server" TargetControlID="WinformsAddInfo_ContentPanel"
                            ExpandControlID="WinformsAddInfo_HeaderPanel" CollapseControlID="WinformsAddInfo_HeaderPanel"
                            Collapsed="True" ExpandDirection="Vertical" ImageControlID="WinformsAddInfo_ToggleImage"
                            ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                            CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                            SuppressPostBack="true" />
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>

    <script type="text/javascript">
        function pageLoad() 
        {
            var toggleClientParseCheckBox = $get('toggleClientParseCheckBox');
            var textBox = $get("<%=PatientSearchInputBox1.ClientID%>" + '__TextBox');
            var parseButton = $get('<%=parseButton.ClientID%>');

            if (toggleClientParseCheckBox.checked == true) 
            {
                parseButton.disabled = true;
                $addHandler(textBox, "keyup", ParseOnKeyStroke);
                ParseOnKeyStroke();
            }
        }

        function ToggleKeyStrokeUpdate() 
        {
            var toggleClientParseCheckBox = $get('toggleClientParseCheckBox');
            var parseButton = $get('<%=parseButton.ClientID%>');
            
            var textBox = $get("<%=PatientSearchInputBox1.ClientID%>" + '__TextBox');
            
            if (toggleClientParseCheckBox.checked == true) 
            {
                parseButton.disabled = true;
                $addHandler(textBox, "keyup", ParseOnKeyStroke);
            }
            else
            {
                parseButton.disabled = false;
                $removeHandler(textBox, "keyup", ParseOnKeyStroke);
            }
        }
        
        function SetValidValue(targetTextBox, value) 
        {
            if (value !== null && value !== -1 && value.toString() !== "01-Jan-1000" && value.toString() !== "31-Dec-9999") 
            {
                targetTextBox.value = value;
            }
            else 
            {
                targetTextBox.value = "";
            }
        }
        
        function ParseOnKeyStroke(e) 
        {
            var thePSIB = $find("<%=PatientSearchInputBox1.ClientID%>" + '__patientSearchInputBoxExtender');
            
            thePSIB.parse();
            
            var familyNameTextBox = $get("<%=FamilyNameTextBox.ClientID%>");
            var givenNameTextBox = $get("<%=GivenNameTextBox.ClientID%>");
            var nhsNumberTextBox = $get("<%=NHSNumberTextBox.ClientID%>");
            var ageTextBox = $get("<%=AgeTextBox.ClientID%>");
            var dobTextBox = $get("<%=DOBTextBox.ClientID%>");
            var genderTextBox = $get("<%=GenderTextBox.ClientID%>");
            var titleTextBox = $get("<%=TitleTextBox.ClientID%>");
            var addressTextBox = $get("<%=AddressTextBox.ClientID%>");
            var postcodeTextBox = $get("<%=PostcodeTextBox.ClientID%>");
            
            SetValidValue(familyNameTextBox, thePSIB.get_familyName());
            SetValidValue(givenNameTextBox, thePSIB.get_givenName());
            SetValidValue(nhsNumberTextBox, thePSIB.get_nhsNumber());
            SetValidValue(ageTextBox, thePSIB.get_age());
            
            SetValidValue(dobTextBox, thePSIB.get_dateOfBirth());

            switch (thePSIB.get_gender()) 
            {
                case 0:
                genderTextBox.value = 'Male';
                break;
                case 1:
                genderTextBox.value = 'Female';
                break;
                case 2:
                genderTextBox.value = 'None';
                break;
            }
            
            SetValidValue(titleTextBox, thePSIB.get_title());
            SetValidValue(addressTextBox, thePSIB.get_address());
            SetValidValue(postcodeTextBox, thePSIB.get_postcode());
        }
        
            
    </script>

</asp:Content>
