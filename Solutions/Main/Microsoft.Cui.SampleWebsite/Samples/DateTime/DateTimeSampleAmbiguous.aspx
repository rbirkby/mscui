<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="SamplesDateTimeDateTimeSampleAmbiguous" Title="Untitled Page" Codebehind="DateTimeSampleAmbiguous.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <asp:Panel ID="DemoPanel1" runat="server" CssClass="demoCCarea">
            <div>
                <NhsCui:DateInputBox ID="DateInputBox1" runat="server" Functionality="simple">
                </NhsCui:DateInputBox>
            </div>
            <div id="divAmbiguous" onblur='javascript:divOnBlur(this);' style="width: 300px;
                line-height: 95%; background-color: #FFFACD; border: 1px solid black; padding: 3px;
                font-size: 8pt; display: none; position: absolute; z-index: 99;">
                Ambiguous Date Detected</div>
                <asp:Label ID="dateLabelText" runat="server" Text="The date input was:">
                </asp:Label>
                <asp:Label ID="dateLabelValue" runat="server" Text="-">
                </asp:Label>
        </asp:Panel>
        <div class="resetFloatAfterdemoCCArea"></div>
        
    </div>
    <div class="resetFloatAfterdemoCCArea"></div>      
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
                This sample shows how 'ambiguous' dates can be handled. An example of an ambiguous
                date is 01 02 2006 which could be 1st February 2006 in English (United Kingdom)
                or the 2nd of January 2006 in English (United States).
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
                This sample shows how you could choose to handle an ambiguous date input. When a
                date is input which could be interpreted as valid in either United Kingdom English
                format or US English format, an event is raised. This displays a dialog box which
                lets you select either the GB or US interpretation. Alternatively, both these options
                can be rejected, instead returning you to the input box so you can change your input.
                If either of the GB or US options is selected, the dialog box closes and the selected
                date replaces the ambiguous input.
            </p>
            <p>
                The following properties have been set:</p>
            <ul>
                <li>AllowApproximate has been set to &ldquo;False&rdquo; for date </li>
                <li>Default has been set to the current date and time </li>
                <li>DisplayDayOfWeek has been set to not include the day of the week </li>
                <li>Functionality is set to &ldquo;Simple&rdquo;</li>
                <li>Month/Years, Years, Null Indexes and Nulls are not allowed</li>
            </ul>
        </div>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeProperties" runat="Server" TargetControlID="properties_ContentPanel"
        ExpandControlID="properties_HeaderPanel" CollapseControlID="properties_HeaderPanel"
        Collapsed="True" ExpandDirection="Vertical" ImageControlID="properties_ToggleImage"
        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Sample Details" CollapsedImage="~/images/SFTheme/acc_h.png"
        CollapsedText="Click to expand the Sample Details" SuppressPostBack="true" />


        
<script type="text/javascript">
  // refreshing labels on property change
  function onPropertyChange(sender, args) 
  {
       dateLabel.innerHTML = dateInput.get_value();
  }
   
   //Ambiguous date event handler  
   function onAmbiguousDate(sender, args)
   {
  	    var divAmbiguous = $get("divAmbiguous");
  	    var firstDate=args.firstDate.format("dd-MMM-yyyy");
        var secondDate=args.secondDate.format("dd-MMM-yyyy");
        if(args.selectedDate==args.firstDate) {
            firstDateHtml="<span style='font-weight:bold'>" + firstDate + "</span>";
        } else {
            secondDateHtml="<span style='font-weight:bold'>" + secondDate + "</span>";
        }
        divAmbiguous.innerHTML="Ambiguous date detected, did you mean: <br/><br/> <a id='firstDateLink' onBlur='javascript:divOnBlur(this);' href=\"javascript:changeDate('" + firstDate + "');\">" + firstDate + "</a> or <a id='secondDateLink' onBlur='javascript:divOnBlur(this);' href=\"javascript:changeDate('" + secondDate + "');\">" + secondDate + "</a> or <a id='thirdDateLink' onBlur='javascript:divOnBlur(this);' href=\"javascript:changeDate('d');\">a different date</a>?";
        divAmbiguous.style.display="block";
        divAmbiguous.focus();
        $get("firstDateLink").focus(); 
   }
   
    // divAmbiguous onBlur handler -- if none of the links have focus make it invisible
    function divOnBlur(sender)
    {
        // Not perfect, but will at least allow 'tab-through'
        if (sender.id === "thirdDateLink")
        {
            $get("divAmbiguous").style.display="none";
        }
    } 
    
    //divAmbiguous data
    function changeDate(dt)
    {
        // user picked the different date option. 
        if (dt == 'd')
        {
           var curDate = new Date(); 
           var currentDate = curDate.format("dd-MMM-yyyy");
           dateInput.set_value(NhsDate.parse(currentDate));
        }
        else
        {
           dateInput.set_value(NhsDate.parse(dt));
        }
        $get("divAmbiguous").style.display="none";
        $get('<%=DateInputBox1.ClientID%>_DateInputBox1_TextBox').focus();
        
    }
           	
   	    
    // on page load function adds property change handles
    function pageLoad() 
    {
        // get control refrences
        dateInput=$get('<%=DateInputBox1.ClientID%>_DateInputBox1_TextBox').DateInputBox;
        dateLabel = $get('<%=dateLabelValue.ClientID%>');
        
        // date time change handlers
        dateInput.add_propertyChanged(onPropertyChange);
        dateInput.add_ambiguousDate(onAmbiguousDate);

        // refresh date time labels
        var dateValue = dateInput.get_value();
        dateInput.set_value(dateValue);
        dateLabel.innerHTML = dateValue;        
     }
     
     // Will be called by Atlas automatically when page is Unloaded
    function pageUnload()
    {
        dateInput.remove_propertyChanged(onPropertyChange);            
        dateInput.remove_ambiguousDate(onAmbiguousDate);
    }
</script>

</asp:Content>
