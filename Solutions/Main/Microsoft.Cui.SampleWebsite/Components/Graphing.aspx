<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LeafPage.Master"
    Inherits="ComponentsGraphing" CodeBehind="Graphing.aspx.cs" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="leafPageContent" runat="Server">
<script type="text/javascript">
function openNewWindowInFullScreen(url)
{
    window.open(url, "graphcontrol", "status=0, toolbar=0, resizable=1, width=980, height=750");
}
</script>
    <div class="demoarea first section">
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The Graphing control allows designers and developers to display data series as graphs in their applications. The Graphing control is implemented by hosting and configuring the application code in a Microsoft Health CUI compliant manner. Click here to open the Graphing control in <a href="javascript:openNewWindowInFullScreen('Graphing_Fullscreen.htm');">full screen</a> mode.
        </p>        
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelSilverlightControl" HeaderText="<a id='GraphingSilverlightTab' href=javascript:TabClick('GraphingSilverlightTab'); title='Silverlight Tab'>Silverlight</a>">
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
                                <param name="initParams" value="StartPage=GraphingSample" />
                                <param name="minRuntimeVersion" value="3.0.40818.0" />
                                <div style="text-align:left; background-repeat:no-repeat; height:750px; background-image:url(images/SL_NotPresent_DCC.png);">
                                <div style="padding-left:250px; padding-top:250px;">
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
                            <br />The sample application presents a set of features to promote quick understanding and detailed interrogation of data, such as a focus line to reveal values across all graphed data, the ability to remove, replace and reorder the stacked graphs (via the check box and drag-and-drop using the data selector - see the top left of the control), and the ability to show or hide data values. The control is designed to be data agnostic within the clinical data arena, but was developed in the context of the vital signs data sets displayed in the sample application above.
                        </div>
                    </div>
                    <br />  
                    <!-- Area for Graphing Object Hierarchy Elements Section -->
                    <asp:Panel ID="GraphingObjects_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="GraphingObjects_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                                Object Hierarchy
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="GraphingObjects_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
			                <p>In reviewing the programmability of the Graphing control, an understanding of the core object hierarchy of the implementation is required:</p>
			                <img class="controls_border" title="Graphing Control Object Hierarchy" alt="Graphing Control Object Hierarchy" runat="server" src="~/Components/Images/GraphingObjectHierarchy.png"/>
			                <p>The GraphHost class is a composite control that represents the single element to be consumed within an application for the representation of multiple graphs. It is this control that manages instances of the graph controls and the other key elements, such as state management, visual time window selection, and graph selection.</p>
			                <p>Instances of a graph control to which data is bound are either a TimeLineGraph or TimeIBarGraph class. The TimeLineGraph implementation allows plotting of a line graph with markers. The TimeIBarGraph implementation allows plotting of data, where each point is represented by a high and low value, such as in blood pressure.</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="GraphingObjects_Extender" runat="Server" TargetControlID="GraphingObjects_ContentPanel"
                        ExpandControlID="GraphingObjects_HeaderPanel" CollapseControlID="GraphingObjects_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="GraphingObjects_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />                    
                    <!-- Area for Graphing Keyboard Navigation Elements Section -->
                    <asp:Panel ID="GraphingKeyboard_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="GraphingKeyboard_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                                Keyboard Navigation (Shortcut Keys)
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="GraphingKeyboard_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
			                <p>When graphs are contained within a GraphHost control, the following keyboard shortcuts are available:</p>
                            <table class="desc">
	                            <thead>
		                            <tr>
			                            <td valign="top" width="178">
			                            <p>Key</p>
			                            </td>
			                            <td valign="top" width="550">
			                            <p>Meaning</p>
			                            </td>
		                            </tr>
	                            </thead>
	                            <tr>
		                            <td valign="top">
		                            <p>Space</p>
		                            </td>
		                            <td valign="top">
		                            <p>Select</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Tab</p>
		                            </td>
		                            <td valign="top">
		                            <p>Move to next control /group of controls</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Shift+Tab</p>
		                            </td>
		                            <td valign="top">
		                            <p>Move to previous control /group of controls</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Arrow keys</p>
		                            </td>
		                            <td valign="top">
		                            <p>Fine scrolling (when scroll bar is in focus)</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Ctrl+arrow keys</p>
		                            </td>
		                            <td valign="top">
		                            <p>Page scrolling (when scroll bar is in focus)</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Ctrl+Home</p>
		                            </td>
		                            <td valign="top">
		                            <p>Scroll to the far left on the X-axis (when the 
		                            scroll bar is in focus)</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Ctrl+End</p>
		                            </td>
		                            <td valign="top">
		                            <p>Scroll to the far right on the X-axis (when the 
		                            scroll bar is in focus)</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Ctrl+5<span></p>
		                            </td>
		                            <td valign="top">
		                            <p>Page level function: Reset</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Ctrl+6<span></p>
		                            </td>
		                            <td valign="top">
		                            <p>Page level function: Visual Focus Line (VFL) 
		                            toggle</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Ctrl+7<span></p>
		                            </td>
		                            <td valign="top">
		                            <p>Page level function: Data value toggle</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Ctrl+8<span></p>
		                            </td>
		                            <td valign="top">
		                            <p>Page level function: Interpolation toggle</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Ctrl+9<span></p>
		                            </td>
		                            <td valign="top">
		                            <p>Page level function: Gridline toggle</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>PgUp &amp; PgDn</p>
		                            </td>
		                            <td valign="top">
		                            <p>Re-order graphs (when the Data Selector line is in 
		                            focus)</p>
		                            </td>
	                            </tr>
                            </table>
                            <br />
                            <p>When focus in on an individual graph control, the following keyboard shortcuts are available:</p>
                            <table border="1" class="desc">
	                            <thead>
		                            <tr>
			                            <td valign="top" width="178">
			                            <p>Key</p>
			                            </td>
			                            <td valign="top" width="550">
			                            <p>Meaning</p>
			                            </td>
		                            </tr>
	                            </thead>
	                            <tr>
		                            <td valign="top">
		                            <p>Arrow keys</p>
		                            </td>
		                            <td valign="top">
		                            <p>Fine scroll</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Ctrl+arrow keys</p>
		                            </td>
		                            <td valign="top">
		                            <p>Page scroll on the X-axis, scroll by major 
		                            interval on the Y-axis</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Page up and Page down</p>
		                            </td>
		                            <td valign="top">
		                            <p>Scroll by major interval on the Y-axis (duplicates 
		                            Ctrl+arrow key vertical control)</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Home</p>
		                            </td>
		                            <td valign="top">
		                            <p>Scroll to the far left on the X-axis</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>End</p>
		                            </td>
		                            <td valign="top">
		                            <p>Scroll to the far right on the X-axis</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Ctrl+Home</p>
		                            </td>
		                            <td valign="top">
		                            <p>Scroll to the top on the Y-axis</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Ctrl+End</p>
		                            </td>
		                            <td valign="top">
		                            <p>Scroll to the bottom on the Y-axis</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Tab</p>
		                            </td>
		                            <td valign="top">
		                            <p>Move to next active graph</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Shift+Tab</p>
		                            </td>
		                            <td valign="top">
		                            <p>Move to previous active graph</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Ctrl+Shift+1</p>
		                            </td>
		                            <td valign="top">
		                            <p>Scale to fit</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Ctrl+Shift+2</p>
		                            </td>
		                            <td valign="top">
		                            <p>Collapse/Expand graph</p>
		                            </td>
	                            </tr>
                            </table>
                            <br />
                            <p>When focus is on the VFL, the following keyboard shortcuts are available:</p>
                            <table class="desc" border="1">
	                            <thead>
		                            <tr>
			                            <td valign="top" width="178">
			                            <p>Key</p>
			                            </td>
			                            <td valign="top" width="550">
			                            <p>Meaning</p>
			                            </td>
		                            </tr>
	                            </thead>
	                            <tr>
		                            <td valign="top">
		                            <p>Arrow keys (left &amp; right)</p>
		                            </td>
		                            <td valign="top">
		                            <p>VFL move left and right by small interval</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Ctrl+arrow keys (left &amp; right)</p>
		                            </td>
		                            <td valign="top">
		                            <p>VFL move left and right by larger interval</p>
		                            </td>
	                            </tr>
                            </table>
                            <br />
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="GraphingKeyboard_Extender" runat="Server" TargetControlID="GraphingKeyboard_ContentPanel"
                        ExpandControlID="GraphingKeyboard_HeaderPanel" CollapseControlID="GraphingKeyboard_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="GraphingKeyboard_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />                                     
                    <!-- Area for Graphing Usage Hints Consuming the Control Elements Section -->
                    <asp:Panel ID="GraphingUsageHints01_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="GraphingUsageHints01_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                                Usage Hints - Consuming the Control
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="GraphingUsageHints01_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
                            <p>To consume the hosting control within Silverlight or WPF, a reference 
                            to the graph hosting class, GraphHost, needs to be created.</p>
<pre>&lt;local:GraphHost x:Name=&quot;GraphHost&quot;
&nbsp;&nbsp; Background=&quot;#F3F3F3&quot; TimeFrequencySelectedIndex=&quot;9&quot;
&nbsp;&nbsp; BorderBrush=&quot;#999999&quot; BorderThickness=&quot;1&quot; FontFamily=&quot;Arial&quot;
&nbsp;&nbsp; DataPointLabelsVisibility=&quot;Collapsed&quot;
&nbsp;&nbsp; InterpolationLinesVisibility=&quot;True&quot; GridLinesVisibility=&quot;Both&quot;&gt;
&nbsp;&nbsp; ... ... ...
&lt;/local:GraphHost&gt;</pre>
                            <p>The namespace element that needs to be added within the XAML is:</p>
                            <pre>xmlns:local=&quot;clr-namespace:Microsoft.Cui.Controls;assembly=Microsoft.Cui.Controls&quot;</pre>
                            <p>At this point, the GraphHost control can be consumed within the 
                            application. The next step will be to define the graph elements that are to be 
                            represented within the control and, if necessary, override any necessary visual 
                            styles.
                            </p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="GraphingUsageHints01_Extender" runat="Server" TargetControlID="GraphingUsageHints01_ContentPanel"
                        ExpandControlID="GraphingUsageHints01_HeaderPanel" CollapseControlID="GraphingUsageHints01_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="GraphingUsageHints01_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />
                    <!-- Area for Graphing Usage Hints GraphHost Control Properties Elements Section -->
                    <asp:Panel ID="GraphingUsageHints02_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="GraphingUsageHints02_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                                Usage Hints - GraphHost Control Properties
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="GraphingUsageHints02_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
                            <p>The key custom properties that are available for the GraphHost control are as follows:</p>
                            <table class="desc">
	                            <thead>
		                            <tr>
			                            <td valign="top" width="190">
			                            <p>Property</p>
			                            </td>
			                            <td valign="top" width="530">
			                            <p>Description</p>
			                            </td>
		                            </tr>
	                            </thead>
	                            <tr>
		                            <td valign="top">
		                            <p>TimeFrequencySelectedIndex</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines the index of the displayed time window, as relating to 
		                            the list of time frequencies in the combo box for the Period</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>DataPointLabelsVisibility</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines the visibility state of the data point labels within 
		                            all displayed graphs</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>InterpolationLinesVisibility</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines the visibility state of the interpolation lines within 
		                            all displayed graphs</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>GridLinesVisibility</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines the visibility state of the grid lines within all 
		                            displayed graphs</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>VisualFocusLineProxomity</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines the number of pixels within which the Visual Focus Line 
		                            (VFL) will snap to a graph data point</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>VisualFocusLineVisibility</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines the visibility state of the VFL</p>
		                            </td>
	                            </tr>
                            </table>
                            <br />
                            <p>Other control properties that should be defined to control the visual 
                            features of the GraphHost control are BorderThickness, BorderBrush, and 
                            FontFamily; all of which are based off the standard Control definition.</p>
                            <p>The FontFamily property, due to inheritance, defines the base font that 
                            will be used by all graphs within the host. The corresponding FontSize will have 
                            little effect as most elements independently define their own font size.</p>
                            <p>Other properties, such as Background, will have little effect as they 
                            are explicitly defined, within the XAML, for elements contained within the 
                            GraphHost control.</p>                           
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="GraphingUsageHints02_Extender" runat="Server" TargetControlID="GraphingUsageHints02_ContentPanel"
                        ExpandControlID="GraphingUsageHints02_HeaderPanel" CollapseControlID="GraphingUsageHints02_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="GraphingUsageHints02_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />  
                    <!-- Area for Graphing Usage Hints Defining Graphs Elements Section -->
                    <asp:Panel ID="GraphingUsageHints03_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="GraphingUsageHints03_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                                Usage Hints - Defining Graphs
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="GraphingUsageHints03_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
			                <p>The possible set of graph panels that make up the data to be rendered 
                            by the Graphing control should to be defined with the GraphHost element. This is 
                            achieved through the GraphHost.Graphs collection property within the XAML 
                            definition.</p>
                            <p>The XAML that would define the required graphs for vital statistics 
                            covering Temperature, Pulse Rate, Respiratory Rate, Blood Pressure, and Oxygen 
                            Saturation would be:</p>
<pre>&lt;local:GraphHost.Graphs&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeLineGraph Title=&quot;Temperature&quot; Units=&quot;°C&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; UnitsDescription=&quot;°C: Degrees Celsius&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Background=&quot;#EBCCCC&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; InterpolationLineColor=&quot;#990000&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; DataMarkerTemplate=&quot;{StaticResource Square}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Style=&quot;{StaticResource OverriddenDefaultStyle}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Height=&quot;185&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ShowNormalRange=&quot;True&quot; </span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NormalRangeMaximumValue=&quot;37&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NormalRangeMinimumValue=&quot;35&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NormalRangeBrush=&quot;Beige&quot;
&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;YAxisMajorInterval=&quot;1&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisIntervalMinimumHeight=&quot;20&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMaxValue=&quot;45&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMinValue=&quot;30&quot; /&gt;
&nbsp;
&nbsp;&nbsp;&nbsp; &lt;local:TimeLineGraph Title=&quot;Pulse rate&quot; Units=&quot;beats per minute&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Background=&quot;#F5E0CC&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; InterpolationLineColor=&quot;#CC6600&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; DataMarkerTemplate=&quot;{StaticResource Circle}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Style=&quot;{StaticResource OverriddenDefaultStyle}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Height=&quot;194&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ShowNormalRange=&quot;True&quot; </span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NormalRangeMaximumValue=&quot;100&quot; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NormalRangeMinimumValue=&quot;60&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NormalRangeBrush=&quot;Beige&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMajorInterval=&quot;10&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisIntervalMinimumHeight=&quot;25&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMaxValue=&quot;200&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMinValue=&quot;0&quot; /&gt;
&nbsp;
&nbsp;&nbsp;&nbsp; &lt;local:TimeIBarGraph Title=&quot;Blood pressure&quot; Units=&quot;mmHg&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; UnitsDescription=&quot;mmHg: Millimetres of mercury&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Background=&quot;#D6EBD6&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; PointTemplate=&quot;{StaticResource IBarPoint}&quot; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; DataMarkerTemplate=&quot;{StaticResource IBarMarker}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LabelTemplate=&quot;{StaticResource IBarLabel}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Style=&quot;{StaticResource OverriddenDefaultStyle}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Height=&quot;210&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ShowNormalRange=&quot;False&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMajorInterval=&quot;10&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisIntervalMinimumHeight=&quot;17&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMaxValue=&quot;300&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMinValue=&quot;0&quot; /&gt;
&nbsp;
&nbsp;&nbsp;&nbsp; &lt;local:TimeLineGraph Title=&quot;Respiratory rate&quot; Units=&quot;breaths per minute&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Background=&quot;#E7D3E7&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; InterpolationLineColor=&quot;#B966B9&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; DataMarkerTemplate=&quot;{StaticResource Triangle}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Style=&quot;{StaticResource OverriddenDefaultStyle}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Height=&quot;162&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ShowNormalRange=&quot;True&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NormalRangeMaximumValue=&quot;20&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NormalRangeMinimumValue=&quot;10&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NormalRangeBrush=&quot;Beige&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMajorInterval=&quot;10&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisIntervalMinimumHeight=&quot;40&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMaxValue=&quot;80&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMinValue=&quot;0&quot; /&gt;
&nbsp;
&nbsp;&nbsp;&nbsp; &lt;local:TimeLineGraph Title=&quot;Oxygen saturation&quot; Units=&quot;%&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Background=&quot;#96C4F3&quot; </span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; InterpolationLineColor=&quot;#2932E0&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; DataMarkerTemplate=&quot;{StaticResourceDiamond}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Style=&quot;{StaticResourceOverriddenDefaultStyle}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Height=&quot;144&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ShowNormalRange=&quot;True&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NormalRangeMaximumValue=&quot;100&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NormalRangeMinimumValue=&quot;90&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NormalRangeBrush=&quot;Beige&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMajorInterval=&quot;10&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisIntervalMinimumHeight=&quot;50&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMaxValue=&quot;100&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMinValue=&quot;0&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisPadding=&quot;0,50,0,5&quot; /&gt;
&nbsp;
&lt;/local:GraphHost.Graphs&gt;</pre>
                            <p>Currently, when defining a graph element, two supported graph types are 
                            allowed:</p>
                            <ul>
	                            <li>TimeLineGraph</li>
	                            <li>TimeIBarGraph</li>
                            </ul>
                            <p>In addition to being contained within a GraphHost element, graph can 
                            also be referenced individually. In this instance the hosting page will be 
                            responsible for any necessary state management that is undertaken by the 
                            GraphHost element.</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="GraphingUsageHints03_Extender" runat="Server" TargetControlID="GraphingUsageHints03_ContentPanel"
                        ExpandControlID="GraphingUsageHints03_HeaderPanel" CollapseControlID="GraphingUsageHints03_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="GraphingUsageHints03_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />
                    <!-- Area for Graphing Usage Hints Graph Properties Elements Section -->
                    <asp:Panel ID="GraphingUsageHints04_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="GraphingUsageHints04_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                                Usage Hints - Graph Properties
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="GraphingUsageHints04_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
                            <p>The key custom properties that are available to the graph control are 
                            as follows:</p>
                            <table class="desc">
	                            <thead>
		                            <tr>
			                            <td valign="top" width="275">
			                            <p>Property</p>
			                            </td>
			                            <td valign="top" width="450">
			                            <p>Description</p>
			                            </td>
		                            </tr>
	                            </thead>
	                            <tr>
		                            <td valign="top">
		                            <p>Title</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines the main title for the graph</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Units</p>
		                            </td>
		                            <td valign="top">
		                            <p>Abbreviated string description of the unit of measure of the 
		                            graph</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>UnitsDescription</p>
		                            </td>
		                            <td valign="top">
		                            <p>Non abbreviated string description of the unit of measure of 
		                            the graph</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ShowNormalRange</p>
		                            </td>
		                            <td valign="top">
		                            <p>Indicates whether the normal range for the graph should be 
		                            shown</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>NormalRangeMaximumValue</p>
		                            </td>
		                            <td valign="top">
		                            <p>The upper bound for the normal range expected for the graph</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>NormalRangeMinimumValue</p>
		                            </td>
		                            <td valign="top">
		                            <p>The lower bound for the normal range expected for the graph</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>NormalRangeBrush</p>
		                            </td>
		                            <td valign="top">
		                            <p>The background color that represents the normal range within 
		                            the graph</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>YAxisMajorInterval</p>
		                            </td>
		                            <td valign="top">
		                            <p>The major interval defined for the graph's Y-axis, determining 
		                            value labels and major tick marks for the axis and how grid lines are 
		                            rendered</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>YAxisMinorIntervalsCountInMajorInterval</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines the number of minor intervals within each major 
		                            interval, determining the number of minor tick marks and how grid lines 
		                            are rendered</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>YAxisIntervalMinimumHeight</p>
		                            </td>
		                            <td valign="top">
		                            <p>The value for the minimum height for an interval in the Y-axis</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>YAxisMinValue</p>
		                            </td>
		                            <td valign="top">
		                            <p>The minimum value of the Y-axis</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>YAxisMaxValue</p>
		                            </td>
		                            <td valign="top">
		                            <p>The maximum value of the Y-axis</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>YAxisPadding</p>
		                            </td>
		                            <td valign="top">
		                            <p>The padding value for the Y-axis, rendering as whitespace above 
		                            and below the graph</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ShowDataPointLablesOnHover</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines whether labels are shown when the mouse is moved over a 
		                            data point</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ShowXAxisMinorIntervalTicks</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines whether the graph X-axis minor interval ticks marks are 
		                            shown</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ShowYAxisMinorIntervalTicks</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines whether the graph Y-axis minor interval ticks marks are 
		                            shown</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ShowGridLines</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines the visibility state of the graph major grid lines</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ShowMinorGridLines</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines the visibility state of the graph minor grid lines</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ShowDataPointLabels</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines the visibility state of the graph data point labels</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ShowInterpolationLines</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines the visibility state of the graph interpolation lines</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>GridLineBrush</p>
		                            </td>
		                            <td valign="top">
		                            <p>The color used to draw grid lines on the graph; the default 
		                            value is Light Gray</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>GridLineThickness</p>
		                            </td>
		                            <td valign="top">
		                            <p>The line thickness used to draw grid lines on the graph; the 
		                            default value is 0.5 pixels</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>TickLineBrush</p>
		                            </td>
		                            <td valign="top">
		                            <p>The color used to draw tick lines on the graph; the default 
		                            value is Dark Gray</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>TickLineThickness</p>
		                            </td>
		                            <td valign="top">
		                            <p>The line thickness used to draw tick lines on the graph; the 
		                            default value is 1 pixel</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>InterpolationLineColor</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines the color of the graph interpolation lines</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>InterpolationLineThickness</p>
		                            </td>
		                            <td valign="top">
		                            <p>Defines the thickness of the graph interpolation lines</p>
		                            </td>
	                            </tr>
                            </table>
                            <br />
                            <p>In addition to the key custom properties, various visual elements of a 
                            graph can be defined to alter the appearance of an individual graph. These 
                            visual elements are as follows:</p>
                            <table class="desc">
	                            <thead>
		                            <tr>
			                            <td valign="top" width="275">
			                            <p>Property</p>
			                            </td>
			                            <td valign="top" width="450">
			                            <p>Description</p>
			                            </td>
		                            </tr>
	                            </thead>
	                            <tr>
		                            <td valign="top">
		                            <p>DataMarkerTemplate</p>
		                            </td>
		                            <td valign="top">
		                            <p>The template to be used for a point representation on the 
		                            graph; if not specified, a default value of a square 5 pixels high and 
		                            wide will be used</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>PointTemplate</p>
		                            </td>
		                            <td valign="top">
		                            <p>The template to be used that maps the elements of the objects 
		                            to which the graph is data bound to the required properties for graph 
		                            representation</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>LabelTemplate</p>
		                            </td>
		                            <td valign="top">
		                            <p>The template to be used for displaying the data point values as 
		                            textual representations on the graph</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>LabelTransform</p>
		                            </td>
		                            <td valign="top">
		                            <p>The render transform to be applied to a LabelTemplate element 
		                            when the mouse is hovered over a data point on the graph; usually to 
		                            make the textual representation larger and easier to read</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>CollisionTemplate</p>
		                            </td>
		                            <td valign="top">
		                            <p>The template for the visual element that is used to represent 
		                            an area of the graph in which there are data point collisions</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ArrowButtonUp</p>
		                            </td>
		                            <td valign="top">
		                            <p>The template for a clickable button that is used to indicate, 
		                            and perform, the necessary scroll up amount to display the data that is 
		                            above the visible window of the graph</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ArrowButtonDown</p>
		                            </td>
		                            <td valign="top">
		                            <p>The template for a clickable button that is used to indicate, 
		                            and perform the necessary scroll down amount to display the data that is 
		                            below the visible window of the graph</p>
		                            </td>
	                            </tr>
                            </table>
                            <br />
                            <p>An additional property for the TimeIBarGraph is available, called:</p>
                            <ul>
	                            <li>EnableIBarInterpolations</li>
                            </ul>
                            <p>This property defines if interpolation lines are enabled for the 
                            TimeIBarGraph. The default value for this property is false, and is used to 
                            prevent interpolation lines from being displayed. This enables the TimeIBarGraph 
                            to be displayed without interpolation lines, whilst hosted by a GraphHost 
                            control, yet have them displayed for the TimeLineGraph.</p>
                            <p>Data binding of the graph to the required data elements is achieved via 
                            the UserControl DataContext.</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="GraphingUsageHints04_Extender" runat="Server" TargetControlID="GraphingUsageHints04_ContentPanel"
                        ExpandControlID="GraphingUsageHints04_HeaderPanel" CollapseControlID="GraphingUsageHints04_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="GraphingUsageHints04_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />
                    <!-- Area for Graphing Usage Hints Data Binding Elements Section -->
                    <asp:Panel ID="GraphingUsageHints05_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="GraphingUsageHints05_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                                Usage Hints - Data Binding
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="GraphingUsageHints05_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
                            <p>Binding data to a specific graph instance is best achieved through 
                            code; unless binding to static data which can also be achieved through XAML as 
                            follows:</p>
                            <pre>DataContext=&quot;{StaticResource PatientTemperatureData}&quot;</pre>
                            <p>To perform the data binding a reference to the required graph instance 
                            is required. For an individual graph, this can be achieved via the control name. 
                            For a collection of graphs within a GraphHost, these can be referenced using:</p>
                            <pre>ObservableCollection&lt;TimeGraphBase&gt; graphs = this.GraphHost.Graphs;</pre>
                            <p>Once the collection of graph objects has been referenced, the 
                            DataContext for each graph can be defined. The method GetGraphByName(string), of 
                            GraphHost, allows a single graph within the collection to be directly accessed.</p>
                            <p>The DataContext for a graph must implement the IEnumerable interface. The Toolkit provides the following collection types:</p>
                            <ul>
	                            <li>FilteredCollection</li>
	                            <li>NonFilteredCollection</li>
                            </ul>
                            <p>The definition of a FilteredCollection within XAML, where the 
                            FilteredCollection contains a collection of objects of type TimePoint, would be:</p>
<pre>&lt;local:FilteredCollection x:Key=&quot;Patient1TemperatureData&quot;&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimePoint Y1=&quot;38.4&quot; Value=&quot;30-Oct-2008 21:37:00&quot; /&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimePoint Y1=&quot;38.5&quot; Value=&quot;30-Oct-2008 21:50:00&quot; /&gt;
&nbsp;&nbsp;&nbsp; ...
&nbsp;&nbsp;&nbsp; &lt;local:TimePoint Y1=&quot;36.4&quot; Value=&quot;03-Nov-2008 08:00:00&quot; /&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimePoint Y1=&quot;36.4&quot; Value=&quot;03-Nov-2008 14:00:00&quot; /&gt;
&lt;/local:FilteredCollection&gt;</pre>
                            <p>The objects that are returned from the IEnumerable interface must support casting to a 
                            DateTime value. This can be achieved either through the implementation of the 
                            IConvertible interface, or through support for an implicit cast to a DateTime 
                            value. This casted value is used to determine the X-axis value of the point.</p>
                            <p>The X-axis value of the point is determined through the definition of 
                            the previously mentioned PointTemplate. This template merely has to define the 
                            mapping of the Y-axis values to the required internal GraphPoint properties for 
                            Y1 and Y2.</p>
                            <p>The XAML used to achieve this mapping for the TimeIBarGraph 
                            implementation is as follows:</p>
<pre>&lt;DataTemplate x:Key=&quot;IBarPoint&quot;&gt;
&nbsp;&nbsp;&nbsp; &lt;ctl:GraphPoint Y1=&quot;{Binding Y1}&quot; Y2=&quot;{Binding Y2}&quot;/&gt;
&lt;/DataTemplate&gt;</pre>
                            <p>In this instance the properties Y1 and Y2 from the TimePoint object are 
                            mapped to the corresponding Y1 and Y2 properties of GraphPoint.</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="GraphingUsageHints05_Extender" runat="Server" TargetControlID="GraphingUsageHints05_ContentPanel"
                        ExpandControlID="GraphingUsageHints05_HeaderPanel" CollapseControlID="GraphingUsageHints05_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="GraphingUsageHints05_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />  
                    <!-- Area for Graphing Usage Hints Data Marker Templates Elements Section -->
                    <asp:Panel ID="GraphingUsageHints06_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="GraphingUsageHints06_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                                Usage Hints - Data Marker Templates
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="GraphingUsageHints06_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
                            <p>When defining a graph element one of the optional properties that can 
                            be defined is a DataMarkerTemplate. By default, the template is defined as a 
                            square of 5 pixels; achieved through the XAML specification:</p>
<pre>&lt;DataTemplate x:Key=&quot;ELEMENT_dataMarkerTemplate&quot;&gt;
&nbsp;&nbsp;&nbsp; &lt;Rectangle Canvas.ZIndex=&quot;10&quot; Height=&quot;5&quot; Width=&quot;5&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fill=&quot;Blue&quot; Stroke=&quot;Black&quot; StrokeThickness=&quot;3&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; local:GraphBase.XOffset=&quot;-2.5&quot; local:GraphBase.YOffset=&quot;-2.5&quot; /&gt;
&lt;/DataTemplate&gt;</pre>
                            <p>However, it is possible to define templates specific to a graph 
                            instance. This is achieved by specifying the required DataTemplate within the 
                            application XAML. An example of DataTemplate specification for a Square, Circle, 
                            Triangle and Diamond would be:</p>
<pre>&lt;DataTemplate x:Key=&quot;Square&quot;&gt;
&nbsp;&nbsp;&nbsp; &lt;Rectangle Canvas.ZIndex=&quot;10&quot; Width=&quot;5&quot; Height=&quot;5&quot; Fill=&quot;#990000&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Stroke=&quot;Black&quot; StrokeThickness=&quot;1&quot; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; local:GraphBase.XOffset=&quot;-2.5&quot; local:GraphBase.YOffset=&quot;-2.5&quot; /&gt;
&lt;/DataTemplate&gt;
&lt;DataTemplate x:Key=&quot;Circle&quot;&gt;
&nbsp;&nbsp;&nbsp; &lt;Ellipse Canvas.ZIndex=&quot;10&quot; Width=&quot;5&quot; Height=&quot;5&quot; Fill=&quot;#CC6600&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Stroke=&quot;Black&quot; StrokeThickness=&quot;1&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; local:GraphBase.XOffset=&quot;-2.5&quot; local:GraphBase.YOffset=&quot;-2.5&quot; /&gt;
&lt;/DataTemplate&gt;
&lt;DataTemplate x:Key=&quot;Triangle&quot;&gt;
&nbsp;&nbsp;&nbsp; &lt;Polygon Canvas.ZIndex=&quot;10&quot; Width=&quot;6&quot; Height=&quot;6&quot; Fill=&quot;#B966B9&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Stroke=&quot;Black&quot; StrokeThickness=&quot;1&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; local:GraphBase.XOffset=&quot;-3&quot; local:GraphBase.YOffset=&quot;-3&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Points=&quot;0,6 6,6 3,0&quot; /&gt;
&lt;/DataTemplate&gt;
&lt;DataTemplate x:Key=&quot;Diamond&quot;&gt;
&nbsp;&nbsp;&nbsp; &lt;Rectangle Canvas.ZIndex=&quot;10&quot; Width=&quot;5&quot; Height=&quot;5&quot; Fill=&quot;#2932E0&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Stroke=&quot;Black&quot; StrokeThickness=&quot;1&quot;
&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;local:GraphBase.XOffset=&quot;-2.5&quot; local:GraphBase.YOffset=&quot;-2.5&quot;&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;Rectangle.RenderTransform&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RotateTransform Angle=&quot;45&quot; CenterX=&quot;2.5&quot; CenterY=&quot;2.5&quot; /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;/Rectangle.RenderTransform&gt;
&nbsp;&nbsp;&nbsp; &lt;/Rectangle&gt;
&lt;/DataTemplate&gt;</pre>
                            <p>The specifications of the X and Y offset are necessary to ensure the 
                            center of the data marker represents the actual point on the graph.</p>
                            <p>For TimeIBarGraph implementation of the data marker template is a 
                            little different. In this case the height of the template needs be determined by 
                            the data bounds values. The XAML for the two common representations for range 
                            values, namely an IBar and an Arrow, is defined as:</p>
<pre>&lt;DataTemplate x:Key=&quot;IArrowMarker&quot;&gt;
&nbsp;&nbsp;&nbsp; &lt;Grid Canvas.ZIndex=&quot;10&quot; Height=&quot;10&quot; Width=&quot;8&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ctl:GraphBase.XOffset=&quot;-4&quot; ctl:GraphBase.YOffset=&quot;0&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ctl:GraphBase.SnapToPixels=&quot;True&quot;&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;Grid.RowDefinitions&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RowDefinition Height=&quot;4&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RowDefinition Height=&quot;*&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RowDefinition Height=&quot;4&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;/Grid.RowDefinitions&gt;
&nbsp;&nbsp;&nbsp; &lt;Grid Grid.Row=&quot;0&quot; Height=&quot;4&quot;&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;Polyline Points=&quot;0,4, 4,0, 8,4&quot; Stroke=&quot;Black&quot; StrokeThickness=&quot;1&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; StrokeStartLineCap=&quot;Round&quot; StrokeEndLineCap=&quot;Round&quot; /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;Polyline Points=&quot;4,4, 4,0&quot;&nbsp; Stroke=&quot;Black&quot; StrokeThickness=&quot;2&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; StrokeStartLineCap=&quot;Square&quot; StrokeEndLineCap=&quot;Round&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;/Grid&gt;
&nbsp;&nbsp;&nbsp; &lt;Rectangle Grid.Row=&quot;1&quot; HorizontalAlignment=&quot;Center&quot; Fill=&quot;Black&quot; Width=&quot;2&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;Grid Grid.Row=&quot;2&quot; Height=&quot;4&quot;&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;Polyline Points=&quot;0,0, 4,4, 8,0&quot; Stroke=&quot;Black&quot; StrokeThickness=&quot;1&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; StrokeStartLineCap=&quot;Round&quot; StrokeEndLineCap=&quot;Round&quot; /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;Polyline Points=&quot;4,0, 4,4&quot;&nbsp; Stroke=&quot;Black&quot; StrokeThickness=&quot;2&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; StrokeStartLineCap=&quot;Square&quot; StrokeEndLineCap=&quot;Round&quot;/&gt;
&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;/Grid&gt;
&nbsp;&nbsp;&nbsp; &lt;/Grid&gt;
&lt;/DataTemplate&gt;
&nbsp;
&lt;DataTemplate x:Key=&quot;IBarMarker&quot;&gt;
&nbsp;&nbsp;&nbsp; &lt;Grid Canvas.ZIndex=&quot;10&quot; Height=&quot;9&quot; Width=&quot;9&quot; ctl:GraphBase.XOffset=&quot;-4.5&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ctl:GraphBase.YOffset=&quot;-1.5&quot; ctl:GraphBase.SnapToPixels=&quot;True&quot;&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;Grid.RowDefinitions&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RowDefinition Height=&quot;3&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RowDefinition Height=&quot;*&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;RowDefinition Height=&quot;3&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;/Grid.RowDefinitions&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;Rectangle Grid.Row=&quot;0&quot; VerticalAlignment=&quot;Top&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Height=&quot;3&quot; Width=&quot;9&quot; Fill=&quot;Black&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;Rectangle Grid.Row=&quot;1&quot; HorizontalAlignment=&quot;Center&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fill=&quot;Black&quot; Width=&quot;1&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &lt;Rectangle Grid.Row=&quot;2&quot; VerticalAlignment=&quot;Bottom&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Height=&quot;3&quot; Width=&quot;9&quot; Fill=&quot;Black&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;/Grid&gt;
&lt;/DataTemplate&gt;</pre>

                            <p>Once the DataTemplate specifications have been made, the corresponding 
                            DataMarkerTemplate property can be defined for each Graph; such as:</p>
<pre>DataMarkerTemplate=&quot;{StaticResource Square}&quot;
DataMarkerTemplate=&quot;{StaticResource Diamond}&quot;
DataMarkerTemplate=&quot;{StaticResource IBarMarker}&quot;</pre>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="GraphingUsageHints06_Extender" runat="Server" TargetControlID="GraphingUsageHints06_ContentPanel"
                        ExpandControlID="GraphingUsageHints06_HeaderPanel" CollapseControlID="GraphingUsageHints06_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="GraphingUsageHints06_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />  
                    <!-- Area for Graphing Usage Hints Modifying the Graph Template Elements Section -->
                    <asp:Panel ID="GraphingUsageHints07_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="GraphingUsageHints07_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                                Usage Hints - Modifying the Graph Template
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="GraphingUsageHints07_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
                            <p>The source code for the control includes a generic.xaml file that 
                            contains the full template specification for all the styles and data templates, 
                            specifying the layout and visuals for the rendering of the Graphing control. The 
                            key Style elements Target Type definitions are:</p>
                            <ul>
	                            <li>GraphHost</li>
	                            <li>TimeLineGraph</li>
	                            <li>TimeIBarGraph</li>
                            </ul>
                            <p>To change the layout of the elements within the control, the GraphHost 
                            entry must be modified. It is this entry that defines the component of the 
                            GraphHost and how graphs are laid out within the control.</p>
                            <p>The TimeLineGraph and TimeIBarGraph both derive from TimeAndYGraphBase. As 
                            such, a new style for the TimeAndYGraphBase can be created that overrides any 
                            required templates for a graph instance. For example, a new style can be defined 
                            in the application XAML file, with the element:</p>
<pre>&lt;Style TargetType=&quot;ctl:TimeAndYGraphBase&quot; x:Key=&quot;OverriddenDefaultStyle&quot;&gt;
&nbsp;&nbsp; ... ... ...
&lt;/Style&gt;</pre>
                            <p>This style can then be applied to a graph using the following Style 
                            property definition:</p>
                            <pre>Style=&quot;{StaticResource OverriddenDefaultStyle}&quot;</pre>
                            <p>Creating a new style in this fashion allows not only the style of a 
                            graph, but also the layout to be changed. To change specific elements within the 
                            graph, such as Title Area, Point Templates and Button Definitions, the 
                            corresponding XAML definition would be taken from the generic version, copied 
                            into the application XAML file, and modified accordingly.</p>
                            <p>Within GraphHost, two special graph templates are defined that 
                            represent the top and bottom axis markers. These graph templates are not data 
                            bound, but are used solely to show a common set of axis markers; rather than 
                            each individual graph having their own axis markers.</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="GraphingUsageHints07_Extender" runat="Server" TargetControlID="GraphingUsageHints07_ContentPanel"
                        ExpandControlID="GraphingUsageHints07_HeaderPanel" CollapseControlID="GraphingUsageHints07_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="GraphingUsageHints07_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />     
                    <!-- Area for Graphing Usage Hints Defining Time Span Values Elements Section -->
                    <asp:Panel ID="GraphingUsageHints08_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="GraphingUsageHints08_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                                Usage Hints - Defining Time Span Values
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="GraphingUsageHints08_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
                            <p>By default, the time ranges defined for the control span a range 
                            covering 5 minutes to 100 years. This definition is contained within the 
                            definition of the GraphHost template within the generic.xaml file.</p>
                            <p>To modify these values, the application XAML for the GraphHost element 
                            should be modified. The element defining the time ranges is a TimeFrequency 
                            collection, which is Data Bound to a ComboBox control:</p>
<pre>&lt;local:TimeFrequencyCollection x:Key=&quot;tfc&quot;&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Minute&quot; Value=&quot;5&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Minute&quot; Value=&quot;15&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Hour&quot; Value=&quot;1&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Hour&quot; Value=&quot;2&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Hour&quot; Value=&quot;4&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Hour&quot; Value=&quot;6&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Hour&quot; Value=&quot;12&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Hour&quot; Value=&quot;24&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Hour&quot; Value=&quot;48&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Day&quot; Value=&quot;5&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Day&quot; Value=&quot;10&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Day&quot; Value=&quot;15&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Day&quot; Value=&quot;20&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Month&quot; Value=&quot;1&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Month&quot; Value=&quot;3&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Month&quot; Value=&quot;6&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Month&quot; Value=&quot;9&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Month&quot; Value=&quot;12&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Month&quot; Value=&quot;18&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Year&quot; Value=&quot;2&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Year&quot; Value=&quot;5&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Year&quot; Value=&quot;10&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Year&quot; Value=&quot;20&quot;/&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeFrequency Unit=&quot;Year&quot; Value=&quot;100&quot;/&gt;
&lt;/local:TimeFrequencyCollection&gt;</pre>
                            <p>To modify the values that are allowed to be selected, this collection 
                            should be modified. The supported units are Second, Minute, Hour, Day, Week, 
                            Month, and Year. It is assumed there are 31 days in a month, 62 days in 2 
                            months, 92 days in 3 months, 365 days in a year (corrected to 366 days for each 
                            possible interval of 4 years).</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="GraphingUsageHints08_Extender" runat="Server" TargetControlID="GraphingUsageHints08_ContentPanel"
                        ExpandControlID="GraphingUsageHints08_HeaderPanel" CollapseControlID="GraphingUsageHints08_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="GraphingUsageHints08_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />                                                                                                                                                             
                    <!-- Area for Graphing Key XAML Elements Section -->
                    <asp:Panel ID="GraphingKeyXaml_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="GraphingKeyXaml_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                                Key XAML Elements
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="GraphingKeyXaml_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
                            <p>The tables below list some of the key XAML elements parts found within generic.xaml.</p>
                            <p>Within GraphHost, there are some key elements that define the layout 
                            and visuals of the components of the control. The position and visual definition 
                            of these elements can be modified as required. These elements are defined in the 
                            table below:</p>
                            <table class="desc">
	                            <thead>
		                            <tr>
			                            <td valign="top" width="225">
			                            <p>XAML Element</p>
			                            </td>
			                            <td valign="top" width="530">
			                            <p>Description</p>
			                            </td>
		                            </tr>
	                            </thead>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_GraphArea</p>
		                            </td>
		                            <td valign="top">
		                            <p>The Grid into which the graphs are placed</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_DataSelector</p>
		                            </td>
		                            <td valign="top">
		                            <p>The control that allows the selection of which graphs are 
		                            rendered within GraphHost</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_TimeSelector</p>
		                            </td>
		                            <td valign="top">
		                            <p>The ComboBox control, bound to the TimeFrequency collection, 
		                            which allows the selection of the visible time window</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_ScrollBar</p>
		                            </td>
		                            <td valign="top">
		                            <p>The single scrollbar that is used to scroll all the graphs 
		                            placed within GraphHost</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_TopAxis</p>
		                            </td>
		                            <td valign="top">
		                            <p>The custom defined TimeGraphBase control that represents the 
		                            top axis within GraphHost, that shows the consolidated time markers for 
		                            all graphs</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_BottomAxis</p>
		                            </td>
		                            <td valign="top">
		                            <p>The custom defined TimeGraphBase control that represents the 
		                            bottom axis within GraphHost, that shows the consolidated time markers 
		                            for all graphs</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_VisualFocusLine</p>
		                            </td>
		                            <td valign="top">
		                            <p>The Visual Focus Line (VFL)</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_ShowVFL</p>
		                            </td>
		                            <td valign="top">
		                            <p>The toggle button that sets the visibility state of the VFL</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_ShowInterpolationLines</p>
		                            </td>
		                            <td valign="top">
		                            <p>The toggle button that allows the state of all of the graphs' 
		                            interpolations lines to be set</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_ShowGridLines</p>
		                            </td>
		                            <td valign="top">
		                            <p>The toggle button that allows the state of all of the graphs' 
		                            grid lines to be set</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_ShowDataPointLines</p>
		                            </td>
		                            <td valign="top">
		                            <p>The toggle button that allows the state of all of the graphs' 
		                            data point labels to be set</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_Reset</p>
		                            </td>
		                            <td valign="top">
		                            <p>The button that is used to reset the state information for 
		                            all of the graphs within the GraphHost</p>
		                            </td>
	                            </tr>
                            </table>
                            <br />
                            <p>Within each graph element, the public properties that can be specified 
                            are defined as XAML elements within the base Style. To override the default 
                            visuals for all graph instances, these values can be changed accordingly.&nbsp; 
                            These elements are defined in the table below.</p>
                            <table class="desc">
	                            <thead>
		                            <tr>
			                            <td valign="top" width="225">
			                            <p>XAML Element</p>
			                            </td>
			                            <td valign="top" width="140">
			                            <p>Property</p>
			                            </td>
			                            <td valign="top" width="390">
			                            <p>Description</p>
			                            </td>
		                            </tr>
	                            </thead>	                            
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_dataMarkerTemplate</p>
		                            </td>
		                            <td valign="top">
		                            <p>DataMarkerTemplate</p>
		                            </td>
		                            <td valign="top">
		                            <p>The template to be used for a point representation on the graph</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_PointTemplate</p>
		                            </td>
		                            <td valign="top">
		                            <p>PointTemplate</p>
		                            </td>
		                            <td valign="top">
		                            <p>The template to be used that maps the elements of the objects 
		                            to which the graph is data bound to the required properties for graph 
		                            representation</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_LabelTemplate</p>
		                            </td>
		                            <td valign="top">
		                            <p>LabelTemplate</p>
		                            </td>
		                            <td valign="top">
		                            <p>The template to be used for a displayed the data point values 
		                            as textual representations on the graph</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_LabelTransform</p>
		                            </td>
		                            <td valign="top">
		                            <p>LabelTransform</p>
		                            </td>
		                            <td valign="top">
		                            <p>The render transform to be applied to a LabelTemplate element 
		                            when the mouse is hovered over a data point on the graph</p>
		                            <p>A RenderOrgin for the LabelTemplate may be necessary to ensure 
		                            the transform is correctly applied; the default render location being 
		                            the top left hand corner of the label</p>
		                            <p>Toolkit has used the render location as bottom left in order to not occlude other symbols when being transformed. 
		                            This location is relative to the position of the label with respect to the data marker</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>ELEMENT_collisionTemplate</p>
		                            </td>
		                            <td valign="top">
		                            <p>CollisionTemplate</p>
		                            </td>
		                            <td valign="top">
		                            <p>The template for the visual element that is used to represent 
		                            an area of the graph in which there are data point collisions</p>
		                            </td>
	                            </tr>	                            
                            </table>
                            <br />
                            <p>The definitions contained within generic.xaml for the base 
                            TimeLineGraph and TimeIBarGraph are defined based on the assumption that graphs 
                            will be rendered as independent elements. When graphs are hosted within 
                            GraphHost, overridden style that hides certain elements, such as the start and 
                            end dates, should be used; as these are managed as specialized graph entries 
                            within GraphHost. The sample application has an example of such a style; called 
                            OverriddenDefaultStyle.</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="GraphingKeyXaml_Extender" runat="Server" TargetControlID="GraphingKeyXaml_ContentPanel"
                        ExpandControlID="GraphingKeyXaml_HeaderPanel" CollapseControlID="GraphingKeyXaml_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="GraphingKeyXaml_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />
                    <!-- Area for Graphing Additional Information Section -->
                    <asp:Panel ID="GraphingAddInfo_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="GraphingAddInfo_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                                Additional Information
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="GraphingAddInfo_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="last section">
                            <p>The Graphing Control is available in Silverlight and WPF. You can bind any data to the sample application, but this 
                            configuration of the application shows how to conform to the Design Guidance for <em>Displaying Graphs and Tables</em> for patient 
                            'vital signs' data (body temperature, pulse rate, respiratory rate, blood pressure and oxygen saturation). To integrate the control 
                            into your application, you will need to modify or replace the data access code, the data processing rules and some of the visual 
                            styling elements.</p>
                            <p>Although the sample application conforms to all mandatory elements of the published Design Guidance for <em>Displaying 
                            Graphs and Tables</em>, there are some areas of the control for which guidance currently does not exist, some features that 
                            challenge the feasibility of rendering the guidance as code, and some features that present alternative implementations of the 
                            guidance. These are outlined in the table below.</p>
                            <table class="desc">
	                            <thead>
		                            <tr>
			                            <td valign="top" width="100">
			                            <p>Feature</p>
			                            </td>
			                            <td valign="top" width="100">
			                            <p>Guidance References</p>
			                            </td>
			                            <td valign="top" width="250">
			                            <p>Non-Conformance/<br />
			                            Alternative Detail</p>
			                            </td>
			                            <td valign="top" width="300">
			                            <p>Rationale</p>
			                            </td>
		                            </tr>
	                            </thead>
	                            <tr>
		                            <td valign="top">
		                            <p>Data Selector</p>
		                            </td>
		                            <td valign="top">
		                            <p>None</p>
		                            </td>
		                            <td valign="top">
		                            <p>The data selector feature is not required or recommended by the guidance.</p>
		                            </td>
		                            <td valign="top">
		                            <p>The feature was added to provide a mechanism for reordering the vertical arrangement of graphs, and to remove and 
		                            re-add a graph from the control.</p>
		                            <p>It does support existing guidance points, such as permanently displaying graph titles and provides a grouped 
		                            location for displaying data values.</p>
		                            <p>In a future version of the control, the Data Selector could support more features, such as the ability to add a new 
		                            data series.</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Vertical visual cue arrows</p>
		                            </td>
		                            <td valign="top">
		                            <p>GTAB-079</p>
		                            </td>
		                            <td valign="top">
		                            <p>Vertical visual cue arrows replace thumbnails in the sample application. Thumbnails are the guidance recommended 
		                            solution and should be considered as the data context solution in place of visual cue arrows, screen space permitting (see 
		                            the rationale for omitting thumbnails from the sample application).</p>
		                            </td>
		                            <td valign="top">
		                            <p>Screen real estate is a key requirement for displaying the graphs, so visual cue arrows were used as they met the 
		                            mandatory requirement stated in the guidance whilst preserving real estate.</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Horizontal visual cue arrows</p>
		                            </td>
		                            <td valign="top">
		                            <p>None</p>
		                            </td>
		                            <td valign="top">
		                            <p>Although a situation could exist where the visible area of the Blood Pressure graph contains no data (or 
		                            interpolation line) in the middle of a data series, it is deemed that horizontal arrows (to indicate location of data) are 
		                            NOT required.</p>
		                            </td>
		                            <td valign="top">
		                            <ul>
			                            <li>No data in the visible area is LOW patient safety risk according to the guidance authors</li>
			                            <li>Horizontal arrows could introduce further confusion so need to be specified in detail before adding the 
			                            feature</li>
		                            </ul>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Data symbols</p>
		                            </td>
		                            <td valign="top">
		                            <p>Example 11</p>
		                            </td>
		                            <td valign="top">
		                            <p>In an overcrowding situation (where data symbols overlap) the guidance suggests that symbol shape should be 
		                            modified to avoid overlapping.</p>
		                            </td>
		                            <td valign="top">
		                            <p>In the sample application, the symbol size is not modified since the benefit of changing symbol size is deemed to 
		                            be inconsistent with the development effort.</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Data Selector</p>
		                            </td>
		                            <td valign="top">
		                            <p>None</p>
		                            </td>
		                            <td valign="top">
		                            <p>The vertical stacked nature of the Data Selector creates white space at the top right of the sample application.</p>
		                            </td>
		                            <td valign="top">
		                            <p>This item is listed for inclusion in the design review for the next release of this application.</p>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td valign="top">
		                            <p>Printing</p>
		                            </td>
		                            <td valign="top">
		                            <p>None</p>
		                            </td>
		                            <td valign="top">
		                            <p>Printing is not covered in the guidance and was de-scoped from the sample application.</p>
		                            </td>
		                            <td valign="top">
		                            <p>Printing will be considered for a future release of the sample application.</p>
		                            </td>
	                            </tr>
                            </table>
                            <br />		   
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="GraphingAddInfo_Extender" runat="Server" TargetControlID="GraphingAddInfo_ContentPanel"
                        ExpandControlID="GraphingAddInfo_HeaderPanel" CollapseControlID="GraphingAddInfo_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="GraphingAddInfo_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />                                 
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelWPF" HeaderText="<a id='graphingWPFTab' href=javascript:TabClick('graphingWPFTab'); title='WPF Tab'>WPF</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WPF control (screenshot):
                    <br />
                    <br />
                    <br />
                    <div>
                       <img class="controls_border" alt="Graphing WPF control screenshot" title="Graphing WPF control screenshot" runat="server" src="~/Components/Images/Graphing.GIF" />
                    </div>
                    <br />
                     <p>
                        Further information on this control can be found on the Silverlight tab above.
                        The full source code can be found in the Microsoft Health Common User Interface Toolkit,
                        which can be downloaded from our
                        <a href="http://www.codeplex.com/mscui/Release/ProjectReleases.aspx" target="_blank"
                            title="Link to releases page on the CodePlex site (New Window)">CodePlex</a>
                        site.
                     </p>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>      
    </ajaxToolkit:TabContainer>         
</asp:Content>
