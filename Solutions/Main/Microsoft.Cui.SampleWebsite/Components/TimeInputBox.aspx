<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="ComponentsTimeInputBox" Title="Untitled Page" CodeBehind="TimeInputBox.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ContentPlaceHolderID="leafPageSpecificHeadTags" runat="server">
    <style type="text/css">
        /*demoCCarea redefined here to remove the 7px of padding that caused PS#5147*/.demoCCarea
        {
            padding: 7px 0;
            margin: 0;
        }
        .paddingAdjustment
        {
            padding: 0 7px 0 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
         <p>
                The TimeInputBox allows you to enter a time in a clear and unambiguous manner. You
                can configure the TimeInputBox so the user can optionally specify if the time is
                approximate, using a checkbox positioned to the right of the input field.
            </p>
        
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px" OnClientActiveTabChanged="ActiveTabChanged">
       <ajaxToolkit:TabPanel runat="server" ID="panelASPNET" HeaderText="<a id='timeInputBoxASPNETTab' href=javascript:TabClick('timeInputBoxASPNETTab'); title='ASP.NET Tab' alt='ASP.NET Tab'>ASP.NET</a>">
            <ContentTemplate>
                <br />
                Example ASP.NET control (embedded):
                <br />
                <br />
                <table cellspacing="6" width="750px" class="sampleGrid">
            <col width="25%" />
            <col width="35%" />
            <col width="40%" />
            <tr>
                <td>
                    Simple Time:
                </td>
                <td colspan="2">
                    <asp:Panel CssClass="demoControlarea" ID="demoPanel1" runat="server">
                        <table>
                            <tr>
                                <td class="paddingAdjustment" />
                                <td>
                                    <NhsCui:TimeInputBox ID="TimeInputBox" runat="server" TimeType="Exact" TimeValue="12:15"
                                        Functionality="Simple" ToolTip="Enter a time, e.g. 02:05"></NhsCui:TimeInputBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div class="resetFloatAfterdemoCCArea">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Complex Time:
                </td>
                <td colspan="2">
                    <asp:Panel CssClass="demoControlarea" ID="Panel1" runat="server">
                        <table>
                            <tr>
                                <td>
                                </td>
                                <td colspan="2">
                                    <NhsCui:TimeInputBoxValidator runat="server" Display="Dynamic" ErrorMessage="Warning: Incorrect data"
                                        ControlToValidate="TimeInputBox1" ID="TimeInputBoxValidator2" SetFocusOnError="true"></NhsCui:TimeInputBoxValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="paddingAdjustment" />
                                <td>
                                    <NhsCui:TimeInputBox ID="TimeInputBox1" runat="server" NullStrings="Unknown,Nothing,NULL" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div class="resetFloatAfterdemoCCArea">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Display Seconds:
                </td>
                <td>
                    <asp:Panel CssClass="demoControlarea" ID="Panel2" runat="server">
                        <table>
                            <tr>
                                <td class="paddingAdjustment" />
                                <td>
                                    <NhsCui:TimeInputBox ID="TimeInputBox3" DisplaySeconds="true" runat="server" NullStrings="Unknown,Nothing,NULL" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div class="resetFloatAfterdemoCCArea">
                    </div>
                </td>
                <td>
                    (<a class="linkButton" id="toggleDisplaySecondsLink" onclick="toggle_DisplaySeconds();return false;"
                        href="javascript:void(0)">Hide Seconds</a>)
                </td>
            </tr>
            <tr>
                <td>
                    Approximate Time:
                </td>
                <td>
                    <asp:Panel CssClass="demoControlarea" ID="Panel3" runat="server">
                        <table>
                            <tr>
                                <td class="paddingAdjustment" />
                                <td>
                                    <NhsCui:TimeInputBox ID="TimeInputBox2" runat="server" Functionality="simple" NullStrings="Unknown,Nothing,NULL"
                                        ToolTip="Enter a time, e.g. 02:05"></NhsCui:TimeInputBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <div class="resetFloatAfterdemoCCArea">
                    </div>
                </td>
                <td>
                    (Set to <a class="linkButton" href="javascript:void(0)" onclick="exactTime_Click();return false">
                        23:59</a>)
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <input onclick="toggleFunctionality_Click()" type="checkbox" id="chkComplex"></input><label
                        for="chkComplex">Complex Functionality</label>
                    <br />
                    <div id="divApprox" style="display: none;">
                        <input onclick="allowApproximate_Click()" type="checkbox" id="chkApprox"></input><label
                            for="chkApprox">Allow Approximate Value</label></div>
                </td>
            </tr>
        </table>
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
            You can enter a time directly in the TimeInputBox by performing one of the following:
            <ul>
                <li>Entering the time from your keyboard</li>
                <li>Clicking the part of the time you want to change in the TimeInputBox and using your
                    up and down arrow keys to increment and decrement the values</li>
            </ul>
        </div>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeUsageHints" runat="Server" TargetControlID="UsageHints_ContentPanel"
        ExpandControlID="UsageHints_HeaderPanel" CollapseControlID="UsageHints_HeaderPanel"
        Collapsed="True" ExpandDirection="Vertical" ImageControlID="usageHints_ToggleImage"
        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Usage Hints section"
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
            The TimeInputBox control is initialized with the following code:
            <br />
            <pre>&lt;NhsCui:TimeInputBox ID="TimeInputBox1" runat="server" 
        AllowApproximate="true" Functionality="Complex"/&gt;</pre>
            <ul>
                <li><strong>AllowApproximate</strong> &ndash; specifies whether to display a checkbox
                    for the Approximate flag </li>
                <li><strong>DisplayAMPM</strong> &ndash; specifies whether an AM/PM suffix should be
                    included </li>
                <li><strong>Display12Hour</strong> &ndash; specifies whether hours should be displayed
                    in 12-hour or 24-hour format </li>
                <li><strong>DisplaySeconds</strong> &ndash; specifies whether seconds should be displayed
                </li>
                <li><strong>Functionality</strong> &ndash; specifies the functionality exposed by the
                    TimeInputBox </li>
                <li><strong>NullStrings</strong> &ndash; gets or sets a list of localized strings that
                    identify different types of null index times </li>
                <li><strong>Value</strong> &ndash; gets or sets the time entered in the input box
                </li>
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
            <ul>
                <li><strong>AllowApproximate</strong> a default value of &ldquo;False&rdquo; </li>
                <li><strong>DisplayAMPM</strong> has a default value of &ldquo;False&rdquo; </li>
                <li><strong>Display12Hour</strong> has a default value of &ldquo;False&rdquo; </li>
                <li><strong>DisplaySeconds</strong> has a default value of &ldquo;False&rdquo; </li>
                <li><strong>Functionality</strong> has a default value of &ldquo;Complex&ldquo;</li>
                <li><strong>NullStrings</strong> defaults to an empty list </li>
            </ul>
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
        <ajaxToolkit:TabPanel runat="server" ID="panelWinformsControl" HeaderText="<a id='timeInputBoxWinFormsTab' href=javascript:TabClick('timeInputBoxWinFormsTab'); title='WinForms Tab' alt='WinForms Tab'>WinForms</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WinForms control (screenshot):
                    <br />
                    <br />                  
                    <table cellspacing="6" width="750px" class="sampleGrid">
            <col width="25%" />
            <col width="35%" />
            <col width="40%" />
            <tr>
                <td>
                    Simple Time:
                </td>
                <td colspan="2">                    
                        <table>
                            <tr>
                                <td class="paddingAdjustment" />
                                <td>
                                   <img class="controls_border"  alt="TimeInputBox WinForms control screenshot" title="TimeInputBox WinForms control screenshot" runat="server" src="~/Components/Images/timeinputboxA.GIF" />
                                </td>
                            </tr>
                        </table>                    
                    <div class="resetFloatAfterdemoCCArea">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Complex Time:
                </td>
                <td colspan="2">                  
                        <table>
                            <tr>
                                <td class="paddingAdjustment" />                               
                                <td colspan="2">
                                   <img id="Img2" class="controls_border" alt="TimeInputBox WinForms control screenshot" title="TimeInputBox WinForms control screenshot" runat="server" src="~/Components/Images/timeinputboxB.GIF" />
                                </td>
                            </tr>                          
                        </table>
                    <div class="resetFloatAfterdemoCCArea">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Display Seconds:
                </td>
                <td>                   
                    <table>
                        <tr>
                            <td class="paddingAdjustment" />
                            <td>
                                <img id="Img4" class="controls_border" alt="TimeInputBox WinForms control screenshot" title="TimeInputBox WinForms control screenshot" runat="server" src="~/Components/Images/timeinputboxC.GIF" />
                            </td>
                        </tr>
                    </table>                   
                    <div class="resetFloatAfterdemoCCArea">
                    </div>
                </td>
                <td>                    
                </td>
            </tr>
            <tr>
                <td>
                    Approximate Time:
                </td>
                <td>                   
                    <table>
                        <tr>
                            <td class="paddingAdjustment" />
                            <td>
                                <img id="Img1" class="controls_border"  alt="TimeInputBox WinForms control screenshot" title="TimeInputBox WinForms control screenshot" runat="server" src="~/Components/Images/TimeInputBox_Approximate_WinFormsScreenshot.png" />
                            </td>
                        </tr>
                    </table>                    
                    <div class="resetFloatAfterdemoCCArea">
                    </div>
                </td>
                <td>                   
                </td>
            </tr>            
        </table>
                    
                    <!------------------------------------------------------------>
                   
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
                        You can enter a time directly in the TimeInputBox by performing one of the following:
                        <ul>
                            <li>Entering the time from your keyboard</li>
                            <li>Clicking the part of the time you want to change in the TimeInputBox and using your
                                up and down arrow keys to increment and decrement the values</li>
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
                            <li><strong>AllowApproximate</strong> &ndash; specifies whether to display a checkbox
                                for the Approximate flag </li>
                            <li><strong>DisplayAMPM</strong> &ndash; specifies whether an AM/PM suffix should be
                                included </li>
                            <li><strong>Display12Hour</strong> &ndash; specifies whether hours should be displayed
                                in 12-hour or 24-hour format </li>
                            <li><strong>DisplaySeconds</strong> &ndash; specifies whether seconds should be displayed
                            </li>
                            <li><strong>Functionality</strong> &ndash; specifies the functionality exposed by the
                                TimeInputBox </li>
                            <li><strong>NullStrings</strong> &ndash; gets or sets a list of localized strings that
                                identify different types of null index times </li>
                            <li><strong>Value</strong> &ndash; gets or sets the time entered in the input box
                            </li>
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
                                <ul>
                                    <li>AllowApproximate has a default of False</li>
                                    <li>DisplaySeconds has a default of False</li>
                                    <li>Functionality has a default of Complex</li>
                                    <li>NullStrings defaults to an empty list</li>                
                                    <li>TimeType has a default of Exact</li>
                                    <li>TimeValue has a default of DateTime.Now</li>
                                </ul>
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
                function ActiveTabChanged(sender, e) 
                {
                    var tabindex = sender.get_activeTab().get_tabIndex();
                   
                    if(tabindex == 1)
                    {      
                        try
                        {               
                            var validator = $get('<%=TimeInputBoxValidator2.ClientID%>');
                            var isValid = TimeInputBoxValidatorEvaluateIsValid(validator);
                                            
                            if(isValid == false)
                            {  
                               sender.set_activeTab(sender.get_tabs()[0]);   
                            }
                        }
                        catch(e)
                        {
                           return false;
                        }               
                    }
                }

                function exactTime_Click() {
                    var obj = $find('<%=TimeInputBox2.ClientID%>_TimeInputBox2_TimeInputBoxExtender');
                    obj.set_value(NhsTime.parse('23:59'));
                }
                function allowApproximate_Click() {
                    var obj = $find('<%=TimeInputBox2.ClientID%>_TimeInputBox2_TimeInputBoxExtender');
                    obj.set_allowApproximate(!obj.get_allowApproximate());
                }
                function toggleFunctionality_Click() {
                    var obj = $find('<%=TimeInputBox2.ClientID%>_TimeInputBox2_TimeInputBoxExtender');
                    if (obj.get_functionality() === TimeFunctionality.Simple)
                    {                        
                        obj.set_functionality(TimeFunctionality.Complex);
                        obj.set_TooltipText("Enter a time, e.g. 02:05 or a shortcut, e.g. +3h");                        
                        $get("divApprox").style.display='block';                        
                    }
                    else
                    {
                        obj.set_functionality(TimeFunctionality.Simple);
                        obj.set_TooltipText("Enter a time, e.g. 02:05");                                                 
                        $get("divApprox").style.display='none';                                   
                   }
                }                
                function toggle_DisplaySeconds() {
                    var obj = $find('<%=TimeInputBox3.ClientID%>_TimeInputBox3_TimeInputBoxExtender');
                    var link = $get('toggleDisplaySecondsLink');
                    obj.set_displaySeconds(!obj.get_displaySeconds());
                    var textNode = document.createTextNode((obj.get_displaySeconds() ? 'Hide Seconds' : 'Show Seconds'));
                    link.replaceChild(textNode, link.firstChild);
                }
    </script>

</asp:Content>
