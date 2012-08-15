<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="SamplesPatientSearchInputBox" Title="Untitled Page" Codebehind="PatientSearchInputBox.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <asp:Panel CssClass="demoCCarea" ID="demoPanel1" runat="server" Width="480px">
            <b>Please enter the details for the patient to be searched:</b>
            <br />
            <asp:Label ID="exampleText" runat="server" Text="<i>(Use free text: e.g. mr subramanyan chandrasekhar 14/07/1945 SO50 7TH)</i>"></asp:Label>
            <br />
            <br />
            <div id="divOptions" style="overflow: hidden;">
                <div style="float: left">
                    <fieldset style="display: inline; border: solid 1px black;">
                        <legend>Search for my match:</legend>
                        <input onclick="javascript:ToggleKeyStrokeUpdate();" id="parseOnKey" name="toggleClientParse"
                            type="radio" style="margin-left: 4px" /><label for="parseOnKey">On every keystroke</label>
                        <br />
                        <input onclick="javascript:ToggleKeyStrokeUpdate();" id="parseOnGo" name="toggleClientParse"
                            type="radio" style="margin-left: 4px" checked="checked" /><label for="parseOnGo">When
                                'Go' is pressed</label>
                    </fieldset>
                </div>
                <div style="float: right">
                    <fieldset style="display: inline; border: solid 1px black;">
                        <legend>Enter search text as:</legend>
                        <input onclick="javascript:toggleStructuredInput();" id="parseUnstructured" name="toggleClientInput"
                            type="radio" style="margin-left: 4px" checked="checked" /><label for="parseUnstructured">Free
                                text input</label>
                        <br />
                        <input onclick="javascript:toggleStructuredInput();" id="parseStructured" name="toggleClientInput"
                            type="radio" style="margin-left: 4px" /><label for="parseStructured">Structured input</label>
                    </fieldset>
                </div>
            </div>
            <br />
            <div id="divControl" style="margin-top: 4px;">
                <NhsCui:PatientSearchInputBox runat="server" ID="PatientSearchInputBox1" WatermarkCssClass="PatientSearchInputWatermark"
                    Width="400px" Style="margin-right: 5px" EndGroupDelimiter="]" InformationDelimiter="#"
                    StartGroupDelimiter="[">
                </NhsCui:PatientSearchInputBox>
                <%--Only parsing on PostBack for now...--%>
                <asp:Button ID="parseButton" runat="server" Text="GO" OnClick="ParseButton_Click" />
            </div>
            <br />
            <div>
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
            </div>
        </asp:Panel>
        <p class="resetFloatAfterdemoCCArea"></p>
    </div>
    <!-- Area for Description -->
    <asp:Panel ID="description_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <input type="image" ID="description_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
            Sample Description
        </div>
    </asp:Panel>
    <asp:Panel ID="description_ContentPanel" runat="server" Style="overflow: hidden;">
        <div class="section">
            <p>
                The PatientSearchInputBox allows entry of freeform text representing search criteria
                for a patient. It parses the criteria and its resultant constituent parts are exposed
                as properties.
            </p>
        </div>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeDescription" runat="Server" TargetControlID="description_ContentPanel"
        ExpandControlID="description_HeaderPanel" CollapseControlID="description_HeaderPanel"
        Collapsed="True" ExpandDirection="Vertical" ImageControlID="description_ToggleImage"
        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Sample Description" CollapsedImage="~/images/SFTheme/acc_h.png"
        CollapsedText="Click to expand the Sample Description" SuppressPostBack="true" />
    <!-- Area for Properties -->
    <asp:Panel ID="Properties_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <input type="image" ID="properties_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
            Sample Details
        </div>
    </asp:Panel>
    <asp:Panel ID="Properties_ContentPanel" runat="server" Style="overflow: hidden;"
        Height="0px">
        <div class="last section">
            <p>
                The PatientSearchInputBox (PSIB) sample allows the user to enter the details
                of the patient in either a 'freetext' or 'structured' format. The 'freetext' format
                allows the user to input the patient details in whichever order is desired. The
                PSIB will attempt to correctly identify which detail belongs to which field. Alternately,
                the PSIB can switch to a more structured form of input that details the order in
                which the details should be input, with a comma used as a separator. This structured
                form of input can be set by the developer to whichever structure they deem appropriate
                for their application.
            </p>
            <p>
                Another feature of the PSIB is that it can be set to either process the details
                on a per keystroke basis or the user can actively initiate processing when they
                have finished their data entry.
            </p>
        </div>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeProperties" runat="Server" TargetControlID="properties_ContentPanel"
        ExpandControlID="properties_HeaderPanel" CollapseControlID="properties_HeaderPanel"
        Collapsed="True" ExpandDirection="Vertical" ImageControlID="properties_ToggleImage"
        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Sample Details" CollapsedImage="~/images/SFTheme/acc_h.png"
        CollapsedText="Click to expand the Sample Details" SuppressPostBack="true" />

    <script type="text/javascript">
//<![CDATA[
// validate the data entered on every key stroke        
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


// to parse the data entered on every key stroke
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

// enabling/disabling the keyhandler for the text box
function ToggleKeyStrokeUpdate() 
{
    var parseButton = $get('<%=parseButton.ClientID%>');
    var textBox = $get('<%=PatientSearchInputBox1.ClientID%>__TextBox');
    
    if ($get('parseOnKey').checked == true) 
    {
        if (parseButton.disabled === false) 
        {
            parseButton.disabled = true;
            $addHandler(textBox, "keyup", ParseOnKeyStroke);
        }
    } 
    else
    {
        if (parseButton.disabled === true) 
        {
            parseButton.disabled = false;
            $removeHandler(textBox, "keyup", ParseOnKeyStroke);
        }
    }
    clearValues();
}

// to control the settings for free text or structured data entry
function toggleStructuredInput() 
{
    var thePSIB = $find("<%=PatientSearchInputBox1.ClientID%>" + '__patientSearchInputBoxExtender');
    
    var exampleText = $get('<%=exampleText.ClientID%>');
    if ($get('parseUnstructured').checked == true) 
    {
        var informationFormat = null;
        thePSIB.set_informationFormat(informationFormat);
        exampleText.innerHTML = "<I>(Use free text: e.g. mr subramanyan chandrasekhar 14/07/1945 SO50 7TH)<\/I>";
    }
    else
    {
          var informationFormat = [
                            Information.Title,
                            Information.FamilyName,
                            Information.GivenName,
                            Information.Address,
                            Information.Postcode,
                            Information.DateOfBirth,
                            Information.Age,
                            Information.Gender,
                            Information.NhsNumber
                            ];
        
        thePSIB.set_informationFormat(informationFormat);
        
        exampleText.innerHTML = "<I>(Use following format (including #): Title# FamilyName# GivenName# Address# Postcode# DOB# Age# Gender# NhsNumber<\/I>";
    }
    clearValues();
 }
   
// on app load function adds property change handles
function pageLoad() 
{
    var thePSIB = $find("<%=PatientSearchInputBox1.ClientID%>" + '__patientSearchInputBoxExtender');
    
    // Enable the correct searchformymatch radio button    
    if (thePSIB.get_informationFormat() != null) 
    {
        $get('parseStructured').checked = true;
        var exampleText = $get('<%=exampleText.ClientID%>');
        exampleText.innerHTML = "<I>(Use following format (including #): Title# FamilyName# GivenName# Address# Postcode# DOB# Age# Gender# NhsNumber<\/I>";
    }
    else
    {
        $get('parseUnstructured').checked = true;
    }
    
    if ($get('parseOnKey').checked === true) 
    { 
        ToggleKeyStrokeUpdate();
    }
}

// Clear the parse result text boxes
function clearValues() 
{
    $get('<%=FamilyNameTextBox.ClientID%>').value = "";
    $get('<%=GivenNameTextBox.ClientID%>').value= "";
    $get('<%=NHSNumberTextBox.ClientID%>').value = "";
    $get('<%=AgeTextBox.ClientID%>').value = "";
    $get('<%=DOBTextBox.ClientID%>').value = "";
    $get('<%=GenderTextBox.ClientID%>').value = "";
    $get('<%=TitleTextBox.ClientID%>').value = "";
    $get('<%=AddressTextBox.ClientID%>').value = "";
    $get('<%=PostcodeTextBox.ClientID%>').value = "";
    var textBox = $get('<%=PatientSearchInputBox1.ClientID%>__TextBox');
    textBox.value="";
    textBox.focus();
 }
 //]]>   
    </script>

</asp:Content>
