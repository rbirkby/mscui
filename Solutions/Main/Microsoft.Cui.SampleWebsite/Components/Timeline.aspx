<%@ Page Title="" Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    CodeBehind="Timeline.aspx.cs" Inherits="ComponentsTimeline" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="leafPageContent" runat="Server">

    <script type="text/javascript">
        function openNewWindowInFullScreen(url) {
            window.open(url, "timelinecontrol", "status=0, toolbar=0, resizable=1, width=980, height=750");
        }
    </script>

    <div class="demoarea first section">
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The Timeline Control realizes a subset of the <i>Design Guidance &ndash; Timeline View</i>
            document as code. The sample control provides a mechanism to view and compare items
            with duration, and single events, against time. The control could be used to convey
            information either to the patient or to the clinician.</p>
        <p>
            Click here to open the Timeline Control in <a href="javascript:openNewWindowInFullScreen('Timeline_Fullscreen.htm');">full screen</a> mode.
        </p>
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelSilverlightControl" HeaderText="<a id='TimelineSilverlightTab' href=javascript:TabClick('TimelineSilverlightTab'); title='Silverlight Tab'>Silverlight</a>">
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
                                <param name="initParams" value="StartPage=TimeActivityGraphHost" />
                                <param name="minRuntimeVersion" value="3.0.40818.0" />
                                <div style="text-align: left; background-repeat: no-repeat; height: 750px; background-image: url(images/SL_NotPresent_Timeline.gif);">
                                    <div style="padding-left: 250px; padding-top: 250px;">
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
                            <br />
                            The sample application presents a set of features to promote quick understanding
                            of the high level picture as well as the identification of causal-effect relationships
                            and trends. Detailed information to support the implementation of the control can be
                            found in the sections below.
                        </div>
                    </div>
                    <br />
                    <!-- Area for Timeline Object Hierarchy Elements Section -->
                    <asp:Panel ID="TimelineObjects_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="TimelineObjects_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Object Hierarchy
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="TimelineObjects_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="section">
                            <p>
                                In reviewing the programmability of the Timeline control, an understanding of the
                                core object hierarchy of the implementation is required:</p>
                            <img class="controls_border" title="Timeline Control Object Hierarchy" alt="Timeline Control Object Hierarchy"
                                runat="server" src="~/Components/Images/TimelineObjectHierarchy.png" />
                            <p>
                                The TimeActivityGraphHost class is a composite control that represents the single
                                element to be consumed within an application for the representation of multiple
                                graphs. It is this control that manages instances of the graph controls and the
                                other key elements, such as state management, visual time window selection, and
                                graph selection.</p>
                            <p>
                                Instances of a graph control to which data is bound are either a TimeActivityGraph
                                or TimeLineGraph or TimeIBarGraph class. The TimeActivityGraph implementation allows
                                plotting of markers with start and end dates such as medical prescriptions. The
                                TimeLineGraph implementation allows plotting of a line graph with markers. The TimeIBarGraph
                                implementation allows plotting of data, where each point is represented by a high
                                and low value, such as in blood pressure.</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="TimelineObjects_Extender" runat="Server"
                        TargetControlID="TimelineObjects_ContentPanel" ExpandControlID="TimelineObjects_HeaderPanel"
                        CollapseControlID="TimelineObjects_HeaderPanel" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="TimelineObjects_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Additional Information section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Additional Information section" SuppressPostBack="true" />
                    <!-- Area for Timeline Keyboard Navigation Elements Section -->
                    <asp:Panel ID="TimelineKeyboard_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="TimelineKeyboard_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Keyboard Navigation (Shortcut Keys)
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="TimelineKeyboard_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="section">
                            <p>
                                When graphs are contained within a TimeActivityGraphHost control, the following
                                keyboard shortcuts are available:</p>
                            <table class="desc">
                                <thead>
                                    <tr>
                                        <td valign="top" width="178">
                                            <p>
                                                Key</p>
                                        </td>
                                        <td valign="top" width="550">
                                            <p>
                                                Meaning</p>
                                        </td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Space</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Select</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Tab</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Move to next control /group of controls</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Shift+Tab</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Move to previous control /group of controls</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Arrow keys</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Fine scrolling (when scroll bar is in focus)</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Ctrl+arrow keys</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Page scrolling (when scroll bar is in focus)</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Ctrl+Home</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Scroll to the far left on the X-axis (when the scroll bar is in focus)</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Ctrl+End</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Scroll to the far right on the X-axis (when the scroll bar is in focus)</p>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <p>
                                When focus in on an individual graph control, the following keyboard shortcuts are
                                available:</p>
                            <table border="1" class="desc">
                                <thead>
                                    <tr>
                                        <td valign="top" width="178">
                                            <p>
                                                Key</p>
                                        </td>
                                        <td valign="top" width="550">
                                            <p>
                                                Meaning</p>
                                        </td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Arrow keys</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Fine scroll</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Ctrl+arrow keys</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Page scroll on the X-axis, scroll by major interval on the Y-axis</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Page up and Page down</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Scroll by major interval on the Y-axis (duplicates Ctrl+arrow key vertical control)</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Home</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Scroll to the far left on the X-axis</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            End</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Scroll to the far right on the X-axis</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Ctrl+Home</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Scroll to the top on the Y-axis</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Ctrl+End</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Scroll to the bottom on the Y-axis</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Tab</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Move to next active graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Shift+Tab</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Move to previous active graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Ctrl+Shift+1</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Scale to fit (Only for the graphs with scale to fit button)</p>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="TimelineKeyboard_Extender" runat="Server"
                        TargetControlID="TimelineKeyboard_ContentPanel" ExpandControlID="TimelineKeyboard_HeaderPanel"
                        CollapseControlID="TimelineKeyboard_HeaderPanel" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="TimelineKeyboard_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Additional Information section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Additional Information section" SuppressPostBack="true" />
                    <!-- Area for Timeline Usage Hints Consuming the Control Elements Section -->
                    <asp:Panel ID="TimelineUsageHints01_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="TimelineUsageHints01_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Usage Hints - Consuming the Control
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="TimelineUsageHints01_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="section">
                            <p>
                                To consume the hosting control within Silverlight or WPF, a reference to the timeline
                                graph hosting class, TimeActivityGraphHost, needs to be created.</p>
                            <pre>&lt;local:TimeActivityGraphHost x:Name=&quot;TimeActivityGraphHost&quot;
&nbsp;&nbsp; Background=&quot;#F3F3F3&quot; TimeFrequencySelectedIndex=&quot;9&quot;
&nbsp;&nbsp; BorderBrush=&quot;#999999&quot; BorderThickness=&quot;1&quot; FontFamily=&quot;Arial&quot;&nbsp;&nbsp; ... ... ...
&lt;/local:TimeActivityGraphHost&gt;</pre>
                            <p>
                                The namespace element that needs to be added within the XAML is:</p>
                            <pre>xmlns:local=&quot;clr-namespace:Microsoft.Cui.Controls;assembly=Microsoft.Cui.Controls&quot;</pre>
                            <p>
                                At this point, the TimeActivityGraphHost control can be consumed within the application.
                                The next step will be to define the graph elements that are to be represented within
                                the control and, if necessary, override any necessary visual styles.
                            </p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="TimelineUsageHints01_Extender" runat="Server"
                        TargetControlID="TimelineUsageHints01_ContentPanel" ExpandControlID="TimelineUsageHints01_HeaderPanel"
                        CollapseControlID="TimelineUsageHints01_HeaderPanel" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="TimelineUsageHints01_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Additional Information section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Additional Information section" SuppressPostBack="true" />
                    <!-- Area for Timeline Usage Hints Timeline Control Properties Elements Section -->
                    <asp:Panel ID="TimelineUsageHints02_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="TimelineUsageHints02_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Usage Hints - TimeActivityGraphHost Control Properties
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="TimelineUsageHints02_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="section">
                            <p>
                                The key custom properties that are available for the TimeActivityGraphHost control
                                are as follows:</p>
                            <table class="desc">
                                <thead>
                                    <tr>
                                        <td valign="top" width="190">
                                            <p>
                                                Property</p>
                                        </td>
                                        <td valign="top" width="530">
                                            <p>
                                                Description</p>
                                        </td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            TimeFrequencySelectedIndex</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the index of the displayed time window, as relating to the list of time
                                            frequencies in the combo box for the Period</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            AxisStartDate</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the start date for the X-axis. (When not supplied the start date will be
                                            inferred from the individual graphs)</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            AxisEndDate</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the end date for the X-axis. (When not supplied the end date will be inferred
                                            from the individual graphs)</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            NowDateTime</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the date time for the Now indicator. When not supplied the current date
                                            time will be used</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            SectionName</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the section name for the graph. This is an attached property and has to
                                            be defined on the graph</p>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <p>
                                Other control properties that should be defined to control the visual features of
                                the TimeActivityGraphHost control are BorderThickness, BorderBrush, and FontFamily;
                                all of which are based off the standard Control definition.</p>
                            <p>
                                The FontFamily property, due to inheritance, defines the base font that will be
                                used by all graphs within the host. The corresponding FontSize will have little
                                effect as most elements independently define their own font size.</p>
                            <p>
                                Other properties, such as Background, will have little effect as they are explicitly
                                defined, within the XAML, for elements contained within the TimeActivityGraphHost
                                control.</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="TimelineUsageHints02_Extender" runat="Server"
                        TargetControlID="TimelineUsageHints02_ContentPanel" ExpandControlID="TimelineUsageHints02_HeaderPanel"
                        CollapseControlID="TimelineUsageHints02_HeaderPanel" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="TimelineUsageHints02_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Additional Information section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Additional Information section" SuppressPostBack="true" />
                    <!-- Area for Timeline Usage Hints Defining Graphs Elements Section -->
                    <asp:Panel ID="TimelineUsageHints03_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="TimelineUsageHints03_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Usage Hints - Defining Graphs
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="TimelineUsageHints03_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="section">
                            <p>
                                The possible set of graph panels that make up the data to be rendered by the Timeline
                                control should to be defined with the TimeActivityGraphHost element. This is achieved
                                through the TimeActivityGraphHost.Graphs collection property within the XAML definition.</p>
                            <p>
                                Currently, when defining a graph element, three supported graph types are allowed:</p>
                            <ul>
                                <li>TimeActivityGraph</li>
                                <li>TimeLineGraph</li>
                                <li>TimeIBarGraph</li>
                            </ul>
                            <p>
                                The XAML that would define the three supported graphs would be:</p>
                            <pre>&lt;local:TimeActivityGraphHost.Graphs&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeActivityGraph Title=&quot;Paracetamol&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Background=&quot;#EBCCCC&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Style=&quot;{StaticResource MedsTimelineStyle}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; local:TimeActivityGraphHost.SectionName=&quot;MEDICATIONS&quot; /&gt;
&nbsp;
&nbsp;&nbsp;&nbsp; &lt;local:TimeLineGraph Title=&quot;Temperature&quot; Units=&quot;°C&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; UnitsDescription=&quot;°C: Degrees Celsius&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Background=&quot;#EBCCCC&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; InterpolationLineColor=&quot;#990000&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; DataMarkerTemplate=&quot;{StaticResource Square}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Style=&quot;{StaticResource TimelineStyle}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Height=&quot;185&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ShowNormalRange=&quot;True&quot; </span>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NormalRangeMaximumValue=&quot;37&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NormalRangeMinimumValue=&quot;35&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; NormalRangeBrush=&quot;Beige&quot;
&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;YAxisMajorInterval=&quot;1&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisIntervalMinimumHeight=&quot;20&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMaxValue=&quot;45&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMinValue=&quot;30&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; local:TimeActivityGraphHost.SectionName=&quot;OBSERVATIONS&quot; /&gt;
&nbsp;
&nbsp;&nbsp;&nbsp; &lt;local:TimeIBarGraph Title=&quot;Blood pressure&quot; Units=&quot;mmHg&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; UnitsDescription=&quot;mmHg: Millimetres of mercury&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Background=&quot;#D6EBD6&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; PointTemplate=&quot;{StaticResource IBarPoint}&quot; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; DataMarkerTemplate=&quot;{StaticResource IBarMarker}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; LabelTemplate=&quot;{StaticResource IBarLabel}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Style=&quot;{StaticResource TimelineStyle}&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Height=&quot;210&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ShowNormalRange=&quot;False&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMajorInterval=&quot;10&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisIntervalMinimumHeight=&quot;17&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMaxValue=&quot;300&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; YAxisMinValue=&quot;0&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; local:TimeActivityGraphHost.SectionName=&quot;OBSERVATIONS&quot; /&gt;
&lt;/local:TimeActivityGraphHost.Graphs&gt;</pre>
                            <p>
                                In addition to being contained within a TimeActivityGraphHost element, graphs can
                                also be referenced individually. In this instance the hosting page will be responsible
                                for any necessary state management that is undertaken by the TimeActivityGraphHost
                                element.</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="TimelineUsageHints03_Extender" runat="Server"
                        TargetControlID="TimelineUsageHints03_ContentPanel" ExpandControlID="TimelineUsageHints03_HeaderPanel"
                        CollapseControlID="TimelineUsageHints03_HeaderPanel" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="TimelineUsageHints03_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Additional Information section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Additional Information section" SuppressPostBack="true" />
                    <!-- Area for Timeline Usage Hints Graph Properties Elements Section -->
                    <asp:Panel ID="TimelineUsageHints04_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="TimelineUsageHints04_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Usage Hints - Graph Properties
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="TimelineUsageHints04_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="section">
                            <p>
                                The key custom properties that are available to the graph control are as follows:</p>
                            <p>
                                Common properties available to all graphs</p>
                            <table class="desc">
                                <thead>
                                    <tr>
                                        <td valign="top" width="145">
                                            <p>
                                                Property</p>
                                        </td>
                                        <td valign="top" width="530">
                                            <p>
                                                Description</p>
                                        </td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Title</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the main title for the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Description</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Description of the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ShowXAxisMinorIntervalTicks</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Indicates whether the graph X-axis minor interval ticks marks are shown</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ShowYAxisMinorIntervalTicks</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Indicates whether the graph Y-axis minor interval ticks marks are shown</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ShowGridLines</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the visibility state of the graph major grid lines</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ShowMinorGridLines</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the visibility state of the graph minor grid lines</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ShowDataPointLabels</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the visibility state of the graph data point labels</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            GridLineBrush</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The color used to draw grid lines on the graph; the default value is Light Gray</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            GridLineThickness</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The line thickness used to draw grid lines on the graph; the default value is 0.5
                                            pixels</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            TickLineBrush</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The color used to draw tick lines on the graph; the default value is Dark Gray</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            TickLineThickness</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The line thickness used to draw tick lines on the graph; the default value is 1
                                            pixel</p>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <p>
                                Additional properties for TimeActivityGraph</p>
                            <table class="desc">
                                <thead>
                                    <tr>
                                        <td valign="top" width="145">
                                            <p>
                                                Property</p>
                                        </td>
                                        <td valign="top" width="530">
                                            <p>
                                                Description</p>
                                        </td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Activities</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the collection of sets of Activities for the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            StackLabels</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Indicates whether the labels should be stacked</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            MaxLabelStackLevels</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the maximum number of levels to stack the labels</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ShowActivities</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Indicates whether to show the activities</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ShowLabelOvercrowdingNotifications</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Indicates whether to show a notification when labels are overcrowded and cannot
                                            be displayed</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            LabelMode</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the mode of the label</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            InterpolationLineRowHeight</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the height of the interpolation line row</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            LabelRowHeight</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the height of each label row</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ActivityRowHeight</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the height of each activities row</p>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <p>
                                Additional properties for TimeLineGraph and TimeIBarGraph</p>
                            <table class="desc">
                                <thead>
                                    <tr>
                                        <td valign="top" width="145">
                                            <p>
                                                Property</p>
                                        </td>
                                        <td valign="top" width="530">
                                            <p>
                                                Description</p>
                                        </td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            Units</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Abbreviated string description of the unit of measure of the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            UnitsDescription</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Non abbreviated string description of the unit of measure of the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ShowNormalRange</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Indicates whether the normal range for the graph should be shown</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            NormalRangeDescription</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the description for the normal range</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            NormalRangeMaximumValue</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The upper bound for the normal range expected for the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            NormalRangeMinimumValue</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The lower bound for the normal range expected for the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            NormalRangeBrush</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The background color that represents the normal range within the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            YAxisMajorInterval</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The major interval defined for the graph's Y-axis, determining value labels and
                                            major tick marks for the axis and how grid lines are rendered</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            YAxisMinorIntervalsCountInMajorInterval</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the number of minor intervals within each major interval, determining the
                                            number of minor tick marks and how grid lines are rendered</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            YAxisIntervalMinimumHeight</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The value for the minimum height for an interval in the Y-axis</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            YAxisMinValue</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The minimum value of the Y-axis</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            YAxisMaxValue</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The maximum value of the Y-axis</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            YAxisPadding</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The padding value for the Y-axis, rendering as whitespace above and below the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ShowInterpolationLines</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the visibility state of the graph interpolation lines</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            InterpolationLineColor</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the color of the graph interpolation lines</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            InterpolationLineThickness</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            Defines the thickness of the graph interpolation lines</p>
                                    </td>
                                </tr>
                            </table>
                            <p>
                                In addition to the key custom properties, various visual elements of a graph can
                                be defined to alter the appearance of an individual graph. These visual elements
                                are as follows:</p>
                            <table class="desc">
                                <thead>
                                    <tr>
                                        <td valign="top" width="146">
                                            <p>
                                                Property</p>
                                        </td>
                                        <td valign="top" width="630">
                                            <p>
                                                Description</p>
                                        </td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            DataMarkerTemplate</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The template to be used for a point representation on the graph; if not specified,
                                            a default value of a square 5 pixels high and wide will be used</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            PointTemplate</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The template to be used that maps the elements of the objects to which the graph
                                            is data bound to the required properties for graph representation</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            LabelTemplate</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The template to be used for displaying the data point values as textual representations
                                            on the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            LabelTransform</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The render transform to be applied to a LabelTemplate element when the mouse is
                                            hovered over a data point on the graph; usually to make the textual representation
                                            larger and easier to read</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top"">
                                        <p>
                                            CollisionTemplate</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The template for the visual element that is used to represent an area of the graph
                                            in which there are data point collisions</p>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <p>
                                An additional property for the TimeIBarGraph is available, called:</p>
                            <ul>
                                <li>EnableIBarInterpolations</li>
                            </ul>
                            <p>
                                This property defines if interpolation lines are enabled for the TimeIBarGraph.
                                The default value for this property is false, and is used to prevent interpolation
                                lines from being displayed. This enables the TimeIBarGraph to be displayed without
                                interpolation lines, whilst hosted by a TimeActivityGraphHost control, yet have
                                them displayed for the TimeLineGraph.</p>
                            <p>
                                Data binding of the graph to the required data elements is achieved via the UserControl
                                DataContext.</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="TimelineUsageHints04_Extender" runat="Server"
                        TargetControlID="TimelineUsageHints04_ContentPanel" ExpandControlID="TimelineUsageHints04_HeaderPanel"
                        CollapseControlID="TimelineUsageHints04_HeaderPanel" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="TimelineUsageHints04_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Additional Information section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Additional Information section" SuppressPostBack="true" />
                    <!-- Area for Timeline Usage Hints Data Binding Elements Section -->
                    <asp:Panel ID="TimelineUsageHints05_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="TimelineUsageHints05_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Usage Hints - Data Binding
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="TimelineUsageHints05_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="section">
                            <p>
                                Binding data to a specific graph instance is best achieved through code; unless
                                binding to static data which can also be achieved through XAML as follows:</p>
                            <pre>DataContext=&quot;{StaticResource ParacetamolData}&quot;</pre>
                            <p>
                                To perform the data binding a reference to the required graph instance is required.
                                For an individual graph, this can be achieved via the control name. For a collection
                                of graphs within a TimeActivityGraphHost, these can be referenced using:</p>
                            <pre>ObservableCollection&lt;TimeGraphBase&gt; graphs = this.TimeActivityGraphHost.Graphs;</pre>
                            <p>
                                The DataContext for a graph must implement the IEnumerable interface. The Toolkit
                                provides the following collection types:</p>
                            <ul>
                                <li>FilteredCollection</li>
                                <li>NonFilteredCollection</li>
                            </ul>
                            <p>
                                The definition of a FilteredCollection within XAML, where the FilteredCollection
                                contains a collection of objects of type TimeActivityPoint, would be:</p>
                            <pre>&lt;local:FilteredCollection x:Key=&quot;ParacetamolData&quot;&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeActivityPoint StartDate=&quot;16-Mar-2006 12:10:00&quot; EndDate=&quot;23-Mar-2006 22:00:00&quot; 
&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataMarkerTemplate=&quot;{StaticResource DataMarker}&quot; /&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeActivityPoint StartDate=&quot;05-Jan-2008 11:40:00&quot; EndDate=&quot;05-Jan-2008 11:55:00&quot;
&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataMarkerTemplate=&quot;{StaticResource DataMarker}&quot; /&gt;
&nbsp;&nbsp;&nbsp; ...
&nbsp;&nbsp;&nbsp; &lt;local:TimeActivityPoint StartDate=&quot;05-Jan-2008 12:10:00&quot; EndDate=&quot;12-Jan-2008 22:00:00&quot;
&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataMarkerTemplate=&quot;{StaticResource DataMarker}&quot; /&gt;
&nbsp;&nbsp;&nbsp; &lt;local:TimeActivityPoint StartDate=&quot;16-Mar-2009 11:40:00&quot; EndDate=&quot;16-Mar-2009 11:55:00&quot;
&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataMarkerTemplate=&quot;{StaticResource DataMarker}&quot; /&gt;
&lt;/local:FilteredCollection&gt;</pre>
                            <p>
                                For TimeActivityGraph, the X-axis values of the point are determined through the
                                definition of the previously mentioned PointTemplate. This template merely has to
                                define the mapping of the X-axis values to the required internal GraphPoint properties
                                for X1 and X2.</p>
                            <p>
                                The XAML used to achieve this mapping for the TimeActivityGraph implementation is
                                as follows:</p>
                            <pre>&lt;DataTemplate x:Key=&quot;TimeActivityPoint&quot;&gt;
&nbsp;&nbsp;&nbsp; &lt;local:GraphPoint X1=&quot;{Binding StartDate}&quot; X2=&quot;{Binding EndDate}&quot; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataMarkerTemplate=&quot;{Binding DataMarkerTemplate}&quot; /&gt;
&lt;/DataTemplate&gt;</pre>
                            <p>
                                In this instance the properties StartDate, EndDate and DataMarkerTemplate from the
                                TimeActivityPoint object are mapped to the corresponding X1, X2 and DataMarkerTemplate
                                properties of GraphPoint.</p>
                            <p>
                                For TimeLineGraph and TimeIBarGraph, in addition to the X-axis values, it is also
                                required to map the Y-axis values to the required internal GraphPoint properties
                                for Y1 and Y2.</p>
                            <p>
                                The XAML used to achieve this mapping for the TimeIBarGraph implementation is as
                                follows:</p>
                            <pre>&lt;DataTemplate x:Key=&quot;IBarPoint&quot;&gt;
&nbsp;&nbsp;&nbsp; &lt;ctl:GraphPoint Y1=&quot;{Binding Y1}&quot; Y2=&quot;{Binding Y2}&quot; X1=&quot;{Binding DateTime}&quot; /&gt;
&lt;/DataTemplate&gt;</pre>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="TimelineUsageHints05_Extender" runat="Server"
                        TargetControlID="TimelineUsageHints05_ContentPanel" ExpandControlID="TimelineUsageHints05_HeaderPanel"
                        CollapseControlID="TimelineUsageHints05_HeaderPanel" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="TimelineUsageHints05_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Additional Information section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Additional Information section" SuppressPostBack="true" />
                    <!-- Area for Timeline Usage Hints Data Marker Templates Elements Section -->
                    <asp:Panel ID="TimelineUsageHints06_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="TimelineUsageHints06_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Usage Hints - Data Marker Templates
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="TimelineUsageHints06_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="section">
                            <p>
                                <b>TimeActivityGraph</b></p>
                            <p>
                                Unlike TimeLineGraph and TimeIBarGraph, TimeActivityGraph can have different data
                                marker for anything that is plotted on the graph and different label for each item.
                                For example, the sample application has different markers for items with different
                                types of duration (no duration, specific duration and open duration).
                            </p>
                            <p>
                                The Toolkit has provided the TimeActivityPoint object which exposes two properties
                                DataMarkerTemplate and LabelTemplate. As mentioned in the data binding section,
                                these properties need to be mapped with the internal GraphPoint class. The sample
                                application has an example DataMarker called CurvedInterpolationLine.
                            </p>
                            <p>
                                In order to provide a custom data marker we would need to define a DataTemplate
                                and then specify it in the TimeActivityPoint object.
                                <pre>
&lt;DataTemplate x:Key=&quot;DataMarker&quot;&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;samplepages:CurvedInterpolationLine /&gt;
&lt;/DataTemplate&gt;

&lt;local:TimeActivityPoint StartDate=&quot;16-Mar-2006 12:10:00&quot; EndDate=&quot;23-Mar-2006 22:00:00&quot; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DataMarkerTemplate=&quot;{StaticResource DataMarker}&quot; /&gt;
</pre>
                            </p>
                            <p>
                                <b>TimeLineGraph and TimeIBarGraph</b></p>
                            <p>
                                When defining a graph element one of the optional properties that can be defined
                                is a DataMarkerTemplate. By default, the template is defined as a square of 5 pixels;
                                achieved through the XAML specification:</p>
                            <pre>&lt;DataTemplate x:Key=&quot;ELEMENT_dataMarkerTemplate&quot;&gt;
&nbsp;&nbsp;&nbsp; &lt;Rectangle Canvas.ZIndex=&quot;10&quot; Height=&quot;5&quot; Width=&quot;5&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fill=&quot;Blue&quot; Stroke=&quot;Black&quot; StrokeThickness=&quot;3&quot;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; local:GraphBase.XOffset=&quot;-2.5&quot; local:GraphBase.YOffset=&quot;-2.5&quot; /&gt;
&lt;/DataTemplate&gt;</pre>
                            <p>
                                However, it is possible to define templates specific to a graph instance. This is
                                achieved by specifying the required DataTemplate within the application XAML. An
                                example of DataTemplate specification for a Square, Circle, Triangle and Diamond
                                would be:</p>
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
                            <p>
                                The specifications of the X and Y offset are necessary to ensure the center of the
                                data marker represents the actual point on the graph.</p>
                            <p>
                                For TimeIBarGraph implementation of the data marker template is a little different.
                                In this case the height of the template needs be determined by the data bounds values.
                                The XAML for the two common representations for range values, namely an IBar and
                                an Arrow, is defined as:</p>
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
                            <p>
                                Once the DataTemplate specifications have been made, the corresponding DataMarkerTemplate
                                property can be defined for each Graph; such as:</p>
                            <pre>DataMarkerTemplate=&quot;{StaticResource Square}&quot;
DataMarkerTemplate=&quot;{StaticResource Diamond}&quot;
DataMarkerTemplate=&quot;{StaticResource IBarMarker}&quot;</pre>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="TimelineUsageHints06_Extender" runat="Server"
                        TargetControlID="TimelineUsageHints06_ContentPanel" ExpandControlID="TimelineUsageHints06_HeaderPanel"
                        CollapseControlID="TimelineUsageHints06_HeaderPanel" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="TimelineUsageHints06_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Additional Information section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Additional Information section" SuppressPostBack="true" />
                    <!-- Area for Timeline Usage Hints Modifying the Graph Template Elements Section -->
                    <asp:Panel ID="TimelineUsageHints07_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="TimelineUsageHints07_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Usage Hints - Modifying the Graph Template
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="TimelineUsageHints07_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="section">
                            <p>
                                The source code for the control includes TimeActivityGraphStyles.xaml and generic.xaml
                                files that contain the full template specification for all the styles and data templates,
                                specifying the layout and visuals for the rendering of the Timeline control. The
                                key Style elements Target Type definitions are:</p>
                            <ul>
                                <li>TimeActivityGraphHost</li>
                                <li>TimeActivityGraph</li>
                                <li>TimeLineGraph</li>
                                <li>TimeIBarGraph</li>
                            </ul>
                            <p>
                                To change the layout of the elements within the control, the TimeActivityGraphHost
                                entry must be modified. It is this entry that defines the component of the TimeActivityGraphHost
                                and how graphs are laid out within the control.</p>
                            <p>
                                The TimeActivityGraph, TimeLineGraph and TimeIBarGraph derive from TimeGraphBase.
                                As such, a new style for the TimeGraphBase can be created that overrides any required
                                templates for a graph instance. For example, a new style can be defined in the application
                                XAML file, with the element:</p>
                            <pre>&lt;Style TargetType=&quot;ctl:TimeGraphBase&quot; x:Key=&quot;TimelineStyle&quot;&gt;
&nbsp;&nbsp; ... ... ...
&lt;/Style&gt;</pre>
                            <p>
                                This style can then be applied to a graph using the following Style property definition:</p>
                            <pre>Style=&quot;{StaticResource TimelineStyle}&quot;</pre>
                            <p>
                                Creating a new style in this fashion allows not only the style of a graph, but also
                                the layout to be changed. To change specific elements within the graph, such as
                                Title Area, Point Templates and Button Definitions, the corresponding XAML definition
                                would be taken from the generic version, copied into the application XAML file,
                                and modified accordingly.</p>
                            <p>
                                Within TimeActivityGraphHost, two special graph templates are defined that represent
                                the top and bottom axis markers. These graph templates are not data bound, but are
                                used solely to show a common set of axis markers; rather than each individual graph
                                having their own axis markers.</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="TimelineUsageHints07_Extender" runat="Server"
                        TargetControlID="TimelineUsageHints07_ContentPanel" ExpandControlID="TimelineUsageHints07_HeaderPanel"
                        CollapseControlID="TimelineUsageHints07_HeaderPanel" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="TimelineUsageHints07_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Additional Information section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Additional Information section" SuppressPostBack="true" />
                    <!-- Area for Timeline Usage Hints Defining Time Span Values Elements Section -->
                    <asp:Panel ID="TimelineUsageHints08_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="TimelineUsageHints08_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Usage Hints - Defining Time Span Values
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="TimelineUsageHints08_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="section">
                            <p>
                                By default, the time ranges defined for the control span a range covering 5 minutes
                                to 100 years. This definition is contained within the definition of the TimeActivityGraphHost
                                template within the TimeActivityGraphStyles.xaml file.</p>
                            <p>
                                To modify these values, the application XAML for the TimeActivityGraphHost element
                                should be modified. The element defining the time ranges is a TimeFrequency collection,
                                which is Data Bound to a ComboBox control:</p>
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
                            <p>
                                To modify the values that are allowed to be selected, this collection should be
                                modified. The supported units are Second, Minute, Hour, Day, Week, Month, and Year.
                                It is assumed there are 31 days in a month, 62 days in 2 months, 92 days in 3 months,
                                365 days in a year (corrected to 366 days for each possible interval of 4 years).</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="TimelineUsageHints08_Extender" runat="Server"
                        TargetControlID="TimelineUsageHints08_ContentPanel" ExpandControlID="TimelineUsageHints08_HeaderPanel"
                        CollapseControlID="TimelineUsageHints08_HeaderPanel" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="TimelineUsageHints08_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Additional Information section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Additional Information section" SuppressPostBack="true" />
                    <!-- Area for Graphing Key XAML Elements Section -->
                    <asp:Panel ID="TimelineKeyXaml_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="TimelineKeyXaml_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Key XAML Elements
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="TimelineKeyXaml_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="last section">
                            <p>
                                The tables below list some of the key XAML elements parts found within their control
                                templates.</p>
                            <p>
                                Within TimeActivityGraphHost, there are some key elements that define the layout
                                and visuals of the components of the control. The position and visual definition
                                of these elements can be modified as required. These elements are defined in the
                                table below:</p>
                            <table class="desc">
                                <thead>
                                    <tr>
                                        <td valign="top" width="270">
                                            <p>
                                                XAML Element</p>
                                        </td>
                                        <td valign="top" width="455">
                                            <p>
                                                Description</p>
                                        </td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_GraphArea</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The Grid into which the graphs are placed</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_TimeSelector</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The ComboBox control, bound to the TimeFrequency collection, which allows the selection
                                            of the visible time window</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_ScrollBar</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The single scrollbar that is used to scroll all the graphs placed within TimeActivityGraphHost</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_TopAxis</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The custom defined TimeGraphBase control that represents the top axis within TimeActivityGraphHost,
                                            that shows the consolidated time markers for all graphs</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_BottomAxis</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The custom defined TimeGraphBase control that represents the bottom axis within
                                            TimeActivityGraphHost, that shows the consolidated time markers for all graphs</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_VisualFocusLine</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The Now indicator</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_ScrollToNow</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The button that is used to scroll to the current date time within the TimeActivityGraphHost</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_Reset</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The button that is used to reset the state information for all of the graphs within
                                            the TimeActivityGraphHost</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_Refresh</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The button that is used to refresh the data for all of the graphs within the TimeActivityGraphHost</p>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <p>
                                Within each graph element, there are some key elements that define the layout and
                                visuals of the components of the control. The position and visual definition of
                                these elements can be modified as required. These elements are defined in the table
                                below:</p>
                            <table class="desc">
                                <thead>
                                    <tr>
                                        <td valign="top" width="270">
                                            <p>
                                                XAML Element</p>
                                        </td>
                                        <td valign="top" width="455">
                                            <p>
                                                Description</p>
                                        </td>
                                    </tr>
                                </thead>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_layoutRoot</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The root grid element containing all the other elements of the graph control</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_TitleArea</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The title region for a graph instance, showing the graph title along with the units
                                            of measure, and the toggle buttons for scale to fit, and for minimizing/maximizing
                                            the graph region</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_dynamicMainLayerViewport</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The viewport element containing the X-axis elements and the plot area of the graph
                                            control</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_nonDynamicRightAxisViewPort</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The viewport element containing the Y-axis elements of the graph control</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_scrollBar</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The horizontal scrollbar element</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_scrollBarVertical</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The vertical scrollbar element</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            FocusVisualElement</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The visual element that is shown when the graph has focus</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_XAxisTitleStartDate</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The text block that displays the start date for the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_XAxisTitleEndDate</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The text block that displays the end date for the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_directionArrowUp</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The template for a clickable button that is used to indicate, and perform, the necessary
                                            scroll up amount required to display the data that is above the visible window of
                                            the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_directionArrowDown</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The template for a clickable button that is used to indicate, and perform, the necessary
                                            scroll down amount required to display the data that is below the visible window
                                            of the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_dataMarkerTemplate</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The template to be used for a point representation on the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_PointTemplate</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The template to be used that maps the elements of the objects to which the graph
                                            is data bound to the required properties for graph representation</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_LabelTemplate</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The template to be used for a displayed the data point values as textual representations
                                            on the graph</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_LabelTransform</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The render transform to be applied to a LabelTemplate element when the mouse is
                                            hovered over a data point on the graph</p>
                                        <p>
                                            A RenderOrgin for the LabelTemplate may be necessary to ensure the transform is
                                            correctly applied; the default render location being the top left hand corner of
                                            the label</p>
                                        <p>
                                            Toolkit has used the render location as bottom left in order to not occlude other
                                            symbols when being transformed. This location is relative to the position of the
                                            label with respect to the data marker</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_collisionTemplate</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The template for the visual element that is used to represent an area of the graph
                                            in which there are data point collisions</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_XAxisLabelTemplate</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The text box definition for the X-axis scale markers</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <p>
                                            ELEMENT_YAxisLabelTemplate</p>
                                    </td>
                                    <td valign="top">
                                        <p>
                                            The text box definition for the Y-axis scale markers</p>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <p>
                                The definitions contained within generic.xaml for the base TimeActivityGraph, TimeLineGraph
                                and TimeIBarGraph are defined based on the assumption that graphs will be rendered
                                as independent elements. When graphs are hosted within TimeActivityGraphHost, overridden
                                style that hides certain elements, such as the start and end dates, should be used;
                                as these are managed as specialized graph entries within TimeActivityGraphHost.
                                The sample application has an example of such a styles; called MedsTimelineStyle
                                and TimelineStyle.</p>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="TimelineKeyXaml_Extender" runat="Server"
                        TargetControlID="TimelineKeyXaml_ContentPanel" ExpandControlID="TimelineKeyXaml_HeaderPanel"
                        CollapseControlID="TimelineKeyXaml_HeaderPanel" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="TimelineKeyXaml_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Additional Information section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Additional Information section" SuppressPostBack="true" />
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelWPF" HeaderText="<a id='TimelineWpfTab' href=javascript:TabClick('TimelineWpfTab'); title='WPF Tab'>WPF</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WPF control (screenshot):
                    <br />
                    <br />
                    <br />
                    <div>
                        <img class="controls_border" alt="Timeline WPF control screenshot" title="Timeline WPF control screenshot"
                            runat="server" src="~/Components/Images/Timeline_WPF.GIF" />
                    </div>
                    <br />
                    <p>
                        Further information on this control can be found on the Silverlight tab above. The
                        full source code can be found in the Microsoft Health Common User Interface Toolkit,
                        which can be downloaded from our <a href="http://www.codeplex.com/mscui/Release/ProjectReleases.aspx"
                            target="_blank" title="Link to releases page on the CodePlex site (New Window)">
                            CodePlex</a> site.
                    </p>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
</asp:Content>
