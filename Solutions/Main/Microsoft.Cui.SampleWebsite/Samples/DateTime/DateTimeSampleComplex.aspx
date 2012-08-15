<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" EnableEventValidation="true"
    AutoEventWireup="true" Inherits="SamplesDateTimeDateTimeSampleComplex" Title="Untitled Page"
    Codebehind="DateTimeSampleComplex.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <asp:UpdatePanel ID="CCUpdatePanel" runat="server">
            <ContentTemplate>
                <!-- Area for custom control -->
                <asp:Panel CssClass="demoCCarea" ID="baseDemoPanel" runat="server">
                    &nbsp;
                    <!-- Base page start Date/Time Controls -->
                    It occurred from:
                    <input type="button" id="optionButton" value="Date Input" onclick="showOptions();" />
                    <br />
                    <asp:Panel ID="fromLabels" runat="Server" Style="display: none;" Width="550px">
                        <hr />
                        Start:<b><NhsCui:DateLabel ID="fromDateLabel" runat="server" />
                        </b><span id="fromTimeDiv"><b>
                            <NhsCui:TimeLabel ID="fromTimeLabel" runat="server" />
                        </b></span>
                    </asp:Panel>
                    <asp:Panel ID="toLabels" runat="Server" Style="display: none;" Width="550px">
                        &nbsp;&nbsp;End:<b><NhsCui:DateLabel ID="toDateLabel" runat="server" />
                        </b><span id="toTimeDiv"><b>
                            <NhsCui:TimeLabel ID="toTimeLabel" runat="server" />
                        </b></span>
                    </asp:Panel>
                </asp:Panel>
                <div class="resetFloatAfterdemoCCArea"></div>
                            

                <!-- Panel displayed on Options button click -->
                <div id="info" style="display: none; border: solid 1px #CCC; background-color: #ccc;
                    padding: 3px; width:390px;">
                    <div style="background-color: #EFEFEF; margin-bottom: 5px; padding: 5px;display:table;width:380px">
                        <fieldset style="width: 115px;float:left;padding:4px 8px 10px 8px">
                            <input type="radio" name="grpFrom" id="exactFrom" value="exact" checked="checked"
                                onclick="fromFunctionalityChecked('exact')" /><label for="exactFrom">Exact</label>
                            <br />
                            <input type="radio" name="grpFrom" id="enhancedFrom" value="enhanced" onclick="fromFunctionalityChecked('enhanced')" /><label
                                for="enhancedFrom">Enhanced</label>
                            <hr />
                            <input type="checkbox" id="checkFromTo" onclick="checkFromAndTo();" /><label
                                for="checkFromTo">From and To</label>
                            <hr />
                            <div id="toFunctionality" style="display: none;">
                                <input type="radio" name="grpTo" id="exactTo" value="exact" checked="checked" onclick="ToFunctionalityChecked('exact')" /><label
                                    for="exactTo">Exact</label>
                                <br />
                                <input type="radio" name="grpTo" id="enhancedTo" value="enhanced" onclick="ToFunctionalityChecked('enhanced')" /><label
                                    for="enhancedTo">Enhanced</label>
                            </div>
                        </fieldset>
                        <div style="float:right;width:215px;">
                            <div style="font-weight: bold">
                                From:</div>
                            <div>
                                <NhsCui:DateInputBox ID="optionDateFrom" runat="server" Functionality="simple">
                                </NhsCui:DateInputBox>
                            </div>
                            <div>
                                <NhsCui:TimeInputBox ID="optionTimeFrom" AllowApproximate="true" Functionality="simple"
                                    runat="server">
                                </NhsCui:TimeInputBox>
                            </div>
                            <asp:Panel ID="optionToPanel" runat="server" Style="display: none;">
                                <div style="font-weight: bold">
                                    To:</div>
                                <div>
                                    <NhsCui:DateInputBox ID="optionDateTo" runat="server" Functionality="simple">
                                    </NhsCui:DateInputBox>
                                </div>
                                <div>
                                    <NhsCui:TimeInputBox ID="optionTimeTo" AllowApproximate="true" Functionality="simple"
                                        runat="server">
                                    </NhsCui:TimeInputBox>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div style="clear:both;float: right">
                        <asp:Button ID="okButton" runat="server" Text="Apply" OnClick="OkButton_Click" />
                        <input type="button" id="cancelButton" value="Cancel" onclick="setVisibility();" />
                    </div>
                </div>                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>        
    <!-- Area for Description -->
    <asp:Panel ID="description_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <input type="image" id="description_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
            Sample Description
        </div>
    </asp:Panel>
    <asp:Panel id="description_ContentPanel" runat="server" Style="overflow: hidden;">
        <div class="section">
            <p>
                This sample allows you to use the the DateInputBox and TimeInputBox controls in
                a scenario which requires more advanced end user functionality. You can enter exact,
                approximate and null dates using a variety of methods.
            </p>
        </div>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeDescription" runat="Server" TargetControlID="description_ContentPanel"
        ExpandControlID="description_HeaderPanel" CollapseControlID="description_HeaderPanel"
        Collapsed="True" ExpandDirection="Vertical" ImageControlID="description_ToggleImage"
        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Sample Description" CollapsedImage="~/images/SFTheme/acc_h.png"
        CollapsedText="Click to expand the Sample Description" SuppressPostBack="true" />
    <!-- Area for Properties -->
    <asp:Panel ID="properties_HeaderPanel" runat="server" Style="cursor: pointer;">
        <div class="heading">
            <input type="image" id="properties_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
            Sample Details
        </div>
    </asp:Panel>
    <asp:Panel ID="properties_ContentPanel" runat="server" Style="overflow: hidden;"
        Height="0px">
        <div class="last section">
            <p>
                This sample uses multiple instances of the DateInputBox, TimeInputBox, DateLabel
                and TimeLabel controls to illustrate some of the complex input scenarios that may
                be required of this advanced control. It initially presents you with a single date, 
                time combination and a button. The button initiates an additional dialog box
                where you can select whether to enter an exact date (allowing only full dates) or
                switch to the enhanced input box. If the enhanced mode is selected, you can enter
                fuzzy dates such as months (for example 'May-2006'),
                and years (for example '2006').
            </p>
            <p>
                This scenario also allows for a 'from and to' scenario, where two date and time
                combinations are used.</p>
            <p>
                In the enhanced mode, you can enable the Approx setting when the input is an exact
                time. The Approx checkbox
                lets you flag that your input is not exact.
            </p>
            <p>
                The following properties have been set:</p>
            <ul>
                <li>AllowApproximate has been set to &ldquo;True&rdquo; for time </li>
                <li>Default has been set to a watermark </li>
                <li>DisplayDayOfWeek has been set to include the day of the week </li>
                <li>Times and fuzzy dates are allowed </li>
                <li>Seconds are enabled </li>
                <li>AM/PM suffix is enabled </li>
                <li>12-hr clock is enabled</li>
            </ul>
        </div>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeProperties" runat="Server" TargetControlID="properties_ContentPanel"
        ExpandControlID="properties_HeaderPanel" CollapseControlID="properties_HeaderPanel"
        Collapsed="True" ExpandDirection="Vertical" ImageControlID="properties_ToggleImage"
        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Sample Details" CollapsedImage="~/images/SFTheme/acc_h.png"
        CollapsedText="Click to expand the Sample Details" SuppressPostBack="true" />
    
    
    <script type="text/javascript">
    // Global variables for controls
    var optionDateFrom;
    var optionDateTo;
    var optionTimeFrom; 
    var optionTimeTo;
    var isCheckChecked = false;
     
    // property change handler for date/time controls
    function onOptionPropertyChange(sender, args) 
    {
        enableTimeInputBoxes([optionDateFrom, optionDateTo], [optionTimeFrom, optionTimeTo]);    
    }

    // enable / disable time input boxes
    function enableTimeInputBoxes(dateInputBoxes, timeInputBoxes)
    {    
        for(var i = 0; i < dateInputBoxes.length; i++) 
        {
            if (timeInputBoxes[i].get_element())
            {
                timeInputBoxes[i].get_element().disabled = (dateInputBoxes[i].get_dateType() != DateType.Exact);
                timeInputBoxes[i].set_enabled(!timeInputBoxes[i].get_element().disabled);
            }
        }
     }
            	
    // Load event called automatically from Atlas
    function pageLoad() 
    { 
        // Get Control References
        optionDateFrom = $get("<%=optionDateFrom.ClientID%>" + '_optionDateFrom_TextBox').DateInputBox;
        optionDateTo = $get("<%=optionDateTo.ClientID%>" + '_optionDateTo_TextBox').DateInputBox;
        optionTimeFrom = $get("<%=optionTimeFrom.ClientID%>" + '_optionTimeFrom_TextBox').TimeInputBox;
        optionTimeTo = $get("<%=optionTimeTo.ClientID%>" + '_optionTimeTo_TextBox').TimeInputBox;
        
        // add event handlers, set functionality
        optionDateFrom.add_propertyChanged(onOptionPropertyChange);
        optionDateTo.add_propertyChanged(onOptionPropertyChange);
        
        optionTimeFrom.add_propertyChanged(onOptionPropertyChange);
        
        optionTimeTo.add_propertyChanged(onOptionPropertyChange);
        
        // check to display the labels on page reload
        if ($get('<%=fromLabels.ClientID %>').style.display === 'block') 
        {
            // Show no time values if the date is not exact
            if (optionTimeFrom.get_enabled() === false) 
            {
                $get('fromTimeDiv').style.display = 'none';   
            }
         
            // do we need to show the end time
            if (isCheckChecked) 
            {
                $get('<%=toLabels.ClientID %>').style.display = 'block';
                // Show no time values if the date is not exact
                if (optionTimeTo.get_enabled() === false) 
                {
                    $get('toTimeDiv').style.display = 'none';   
                }
            }
            else 
            {
                $get('<%=toLabels.ClientID %>').style.display = 'none';
            }
        }
        
        // select the correct radio buttons
        if (optionDateFrom.get_functionality() === DateFunctionality.Complex) 
        {
            $get('enhancedFrom').checked = true;    
            optionTimeFrom.set_allowApproximate(true);
        }
        
        if (optionDateTo.get_functionality() === DateFunctionality.Complex) 
        {
            $get('enhancedTo').checked = true;     
            optionTimeTo.set_allowApproximate(true);
        }
    }
         
   // To handle the funtionality change request for Option Panel From date/time control  
   function fromFunctionalityChecked(optionSelected) 
   {
        if (optionSelected == 'exact') 
        { 
            optionDateFrom.set_functionality(DateFunctionality.Simple);
            
            optionTimeFrom.set_allowApproximate(false);
            optionTimeFrom.set_functionality(TimeFunctionality.Simple);
         }
         else
         { 
            optionDateFrom.set_functionality(DateFunctionality.Complex); 
            
            optionTimeFrom.set_allowApproximate(true);
            optionTimeFrom.set_functionality(TimeFunctionality.Complex);
         }
         
        // Reset Tooltip shown flag
        optionDateFrom._tooltipShown = false;
        // Reset Tooltip shown flag
        optionTimeFrom._tooltipShown = false;                     
    }
    
   // To handle the funtionality change request for Option Panel To date/time control
   function ToFunctionalityChecked(optionSelected) 
   {
        if (optionSelected == 'exact') 
        { 
            optionDateTo.set_functionality(DateFunctionality.Simple); 
            
            optionTimeTo.set_allowApproximate(false);
            optionTimeTo.set_functionality(TimeFunctionality.Simple);
         }
         else 
         { 
            optionDateTo.set_functionality(DateFunctionality.Complex);
            
            optionTimeTo.set_allowApproximate(true);
            optionTimeTo.set_functionality(TimeFunctionality.Complex);
         }
         
        // Reset Tooltip shown flag
        optionDateTo._tooltipShown = false;
        // Reset Tooltip shown flag
        optionTimeTo._tooltipShown = false;                              
    }
  
  // To handle the checkfromto checkbox 
  function checkFromAndTo() 
  {
        var checkBoxControl = $get('checkFromTo');
        var toFuncDiv = $get('toFunctionality');
        var toPanel = $get('<%=optionToPanel.ClientID%>');

        if (checkBoxControl.checked) 
        {
            toPanel.style.display = '';
            toFuncDiv.style.display = '';
            ensurePositionedElements();
            isCheckChecked = true;
        }
        else 
        {
            toPanel.style.display = 'none';
            toFuncDiv.style.display = 'none';
            isCheckChecked = false;
        }
    }
  
  // enable.disable options panel 
  function setVisibility() 
  {
      var optionsPanel = document.getElementById('info');
      if(optionsPanel.style.display == 'none') 
      {
        optionsPanel.style.display = '';  
      }
      else 
      {
        optionsPanel.style.display = 'none';
        $get("optionButton").disabled = false;
      }
   } 
   
  function positionOptionsDialog() 
  {
        var buttonBounds = Sys.UI.DomElement.getBounds($get("optionButton"));
        var optionDialog=$get("info");        
        optionDialog.style.position = 'absolute';
        var positionedAncestorBounds = Sys.UI.DomElement.getBounds(optionDialog.offsetParent);
        
        optionDialog.style.top = (buttonBounds.y+buttonBounds.height-positionedAncestorBounds.y) + 'px';
        optionDialog.style.left = (buttonBounds.x-positionedAncestorBounds.x) + 'px';
        optionDialog.style.zIndex = 10000;
  }
    
  // to show the options div
  function showOptions() 
  {
    // If page not loaded, then fail fast
    if(!optionDateFrom) return;
  
    $get("info").style.display = "";
    positionOptionsDialog();
    $get("optionButton").disabled = true;
    // to ensure the correct panel is visible on page reload
    if (isCheckChecked) 
    {
         $get('checkFromTo').checked = true;
    } 
    checkFromAndTo();
    
    // select the correct radio buttons
    if (optionDateFrom.get_functionality() === DateFunctionality.Complex) 
    {
       $get('enhancedFrom').checked = true;     
    }
    
    if (optionDateTo.get_functionality() === DateFunctionality.Complex) 
    {
       $get('enhancedTo').checked = true;     
    }
    
    // ensure the spinner position
    ensurePositionedElements();
  }
    
    function ensurePositionedElements() 
    {
        if(Sys.Browser.agent === Sys.Browser.Firefox)
        {
            // firefox needs some help detecting the element
            // has changed
            triggerMutationEvent(optionTimeFrom.get_element());
            triggerMutationEvent(optionTimeTo.get_element());
            triggerMutationEvent(optionDateFrom.get_element());
            triggerMutationEvent(optionDateTo.get_element());
        }
    }

    function triggerMutationEvent(element) 
    {
        element.setAttribute("dummy", element.getAttribute("dummy") + "_");
    }
    </script>

</asp:Content>
