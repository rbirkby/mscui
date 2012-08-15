<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/LeafPage.Master"
    Inherits="ComponentsMedicationsListView" CodeBehind="MedicationsListView.aspx.cs" %>
    
<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content2" ContentPlaceHolderID="leafPageContent" runat="Server">

<script>
        function refreshData() 
        {         
          ////refresh the patient banner view in the patient banner control explicitly after the control has loaded.
          var sPath = window.location.pathname;
          if(sPath.indexOf("MedicationsListView")!=-1)
           {   
                var el1=$get("<%= patientBanner.ClientID %>");
                if(el1!=null && el1.control != null)
                {
                 el1.control._resizeHandler();          
                }
           }
        }  
          
       function reloadBanner()
       {  
          ////reload the patient banner data after a second       
           var sPath = window.location.pathname;
           if(sPath.indexOf("MedicationsListView")!=-1)
           {
             setTimeout( "refreshData()", 600 );
           }               
       }   
</script>

    <div class="demoarea first section">
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <p>
            The MedicationsListView control allows designers and developers to display medication lines in their 
            applications. The MedicationsListView control is implemented by hosting and configuring the WrapDataGrid control in a
            Microsoft Health CUI compliant manner.
        </p>        
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelSilverlightControl" HeaderText="<a id='medicationListViewSilverlightTab' href=javascript:TabClick('medicationListViewSilverlightTab'); title='Silverlight Tab'>Silverlight</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example Silverlight control (embedded):
                    <br />
                    <br />
                    <div>                        
                        <asp:Panel ID="DemoPanel1" runat="server" Width="740px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="patientSelectText" runat="server" Text="Please select a patient: ">
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Panel ID="matchOptions" runat="server" Style="font-size: x-small; float: left;
                                            border: 1px solid black; background: #FFFFFF">
                                            <asp:ListBox ID="patientListBox" Rows="2" runat="server" AutoPostBack="true" Font-Names="Verdana"
                                                ForeColor="Black" OnSelectedIndexChanged="PatientListBox_SelectedIndexChanged">
                                            </asp:ListBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        The <a title="Link to PatientBanner Control page" href="PatientBanner.aspx">PatientBanner</a>
                                        and MedicationsListView displaying a set of sample medications:
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <NhsCui:PatientBanner ID="patientBanner" AccessKey="P" runat="server" ZoneTwoTooltip="Click to expand or collapse"
                                            Width="735px">
                                        </NhsCui:PatientBanner>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />                       
                        <div>                               
                        <object data="data:application/x-silverlight," type="application/x-silverlight-2"
                            width="100%" height="500px">
                            <param name="source" value="../ClientBin/Microsoft.Cui.SamplePages.xap" />
                            <param name="initParams" value="StartPage=WrapDataGridPageSmall,TemplateSet=Alternative,PatientId=<%=(this.patientListBox.SelectedIndex + 1).ToString() %>" />
                            <param name="minRuntimeVersion" value="3.0.40818.0" />
                            <div style="text-align:left; background-repeat:no-repeat; height:477px; background-image:url(images/faded_preinstall.png);">
                                <div style="padding-left:300px; padding-top:150px;">
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
                            <b>Note:</b> If you have selected <b>Cooper</b> from the <b>Please select a patient</b> selection box, please be aware that the medications data displayed for the patient Catherine Cooper is not clinically valid.
                             It is provided for the purpose of demonstrating the various features of the MedicationsListView grid that cannot be shown with a smaller data set. 
                             Also, when the <b>Past <i>x</i> months</b> drop-down filter is applied, the following are used as reference dates against which the medications data is filtered:
                             <ul>
                             <li><b>Evans:</b> 01-Aug-2007 </li>
                             <li><b>Cooper:</b> 30-Mar-2008  </li>
                             </ul>
                        </div>
                    </div>
                    <br />                   
                     <!-- Area for Properties -->
                    <asp:Panel ID="Properties_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="properties_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
                            Members
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Properties_ContentPanel" runat="server" Style="overflow: hidden;"
                        Height="0px">
                        <div class="section">
                            WrapDataGrid : UserControl, IDisposable
                            <br />
                            <br />
                            <strong>Properties</strong>
                            <br />
                            <ul>
                                <li><strong>Boolean : AllowColumnResizing</strong> &#8211; gets or sets a value that
                                    indicates whether or not column resizing is allowed</li>
                                <li><strong>Brush : AlternatingRowBackground</strong> &#8211; gets or sets the background of alternating rows</li>
                                <li><strong>Int32 : AnchorRowIndex</strong> &#8211; gets the starting row index from
                                    which a multiple-row selection has been made</li>
                                <li><strong>Boolean : ColumnHeadersVisible</strong> &#8211; gets or sets a value that
                                indicates whether or not column headers are visible</li>
                                <li><strong>Brush : HighlightBackground</strong> &#8211; gets or sets the background for the row that has focus while selecting discontinuous rows</li>
                                <li><strong>Brush : HighlightSelectedBackground</strong> &#8211; gets or sets the background for a selected row that has focus while selecting discontinuous rows</li>
                                <li><strong>ColumnView : MainView</strong> &#8211; gets the main view of the WrapDataGrid</li>
                                <li><strong>Brush : RowBackground</strong> &#8211; gets or sets the row background</li>
                                <li><strong>Int32 : SelectedIndex</strong> &#8211; gets or sets the selected index</li>
                                <li><strong>Collection&lt; Int32 &gt; : SelectedIndexes</strong> &#8211; gets the selected
                                    indexes</li>
                                <li><strong>DataBoundRow : SelectedItem</strong> &#8211; gets the selected row</li>
                                <li><strong>Collection&lt; DataBoundRow &gt; : SelectedItems</strong> &#8211; gets the
                                    selected items</li>
                                <li><strong>Collection&lt; DataBoundRow &gt; : SelectedRows</strong> &#8211; gets the
                                    selected items, as above</li>
                                <li><strong>Brush : SelectionBackground</strong> &#8211; gets or sets the brush to use
                                    for row selection</li>
                                <li><strong>SelectionMode : SelectionMode</strong> &#8211; gets or sets the selection
                                    mode</li>
                                <li><strong>WrapDataGridColumnCollection : WrapDataGridColumns</strong> &#8211; gets
                                    the columns for the main view</li>
                            </ul>
                            + inherited properties from base class, UserControl
                            <br />
                            <br />
                            <strong>Events</strong>
                            <ul>
                                <li><strong>OnColumnHeaderClick</strong> &ndash; fires when a column header is clicked</li>
                                <li><strong>OnGroupingRender</strong> &ndash; fires as each grouping is rendered</li>
                                <li><strong>OnGroupHeaderClick</strong> &ndash; fires when a group header is clicked</li>
                                <li><strong>OnSelectionChanged</strong> &ndash; fires when the selection in the grid
                                    is changed</li>
                                <li><strong>OnKeyPress</strong> &ndash; fires when a key is pressed in the grid</li>
                            </ul>
                            <br />
                            <strong>Templates</strong>
                            <ul>
                                <li><strong>AscendingOrderIndicatorDataTemplate</strong> &#8211; provides the visual
                                    representation for the ascending order marker on a column</li>
                                <li><strong>ColumnResizerTemplate</strong> &#8211; provides the column resizer template</li>
                                <li><strong>DescendingOrderIndicatorDataTemplate</strong> &#8211; provides the visual
                                    representation for the descending order marker on a column</li>
                                <li><strong>GroupingDataTemplateLogic</strong> &#8211; provides the logic to determine
                                    when a new grouping header should be inserted into the WrapDataGrid</li>
                                <li><strong>GroupingDataTemplatePresentation</strong> &#8211; provides the visual representation
                                    of a grouping</li>
                                <li><strong>LookAheadCellTemplate</strong> &#8211; provides the visual representation
                                    of the look-ahead cell area</li>
                                <li><strong>LookAheadSummaryCellTemplate</strong> &#8211; provides the visual representation
                                    of the look-ahead summary cell area</li>
                                <li><strong>LookBehindCellTemplate</strong> &#8211; provides the visual representation
                                    of the look-behind cell area</li>
                                <li><strong>LookBehindSummaryCellTemplate</strong> &#8211; provides the visual representation
                                    of the look-behind summary cell area</li>
                            </ul>
                            <br />
                            <strong>Methods</strong>
                            <ul>
                                <li><strong>SetMaxWidth</strong> &#8211; sets the maximum width of a column to a specified value </li>
                                <li><strong>SetMinWidth</strong> &#8211; sets the minimum width of a column to a specified value </li>
                                <li><strong>ShowColumn</strong> &#8211; toggles the visibility of a column</li>
                                <li><strong>ShowColumnHeaders</strong> &#8211; toggles the visibility of the column headers </li>
                                <li><strong>AddSortIndicator</strong> &#8211; adds a sort indicator to the specified column </li>
                                <li><strong>ClearSortIndicators</strong> &#8211; clears the sort indicators on all the columns </li>
                                <li><strong>LoadUpControl</strong> &#8211; loads up the WrapDataGrid control (<strong>Note:</strong> 
                                in the Silverlight version of the control this method is automatically called by the OnLoaded event. 
                                When using the WPF version of the control, this method must be explicitly called from code)</li>
                            </ul>                            
                            <br>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeProperties" runat="Server" TargetControlID="properties_ContentPanel"
                        ExpandControlID="properties_HeaderPanel" CollapseControlID="properties_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="properties_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Members section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Members section"
                        SuppressPostBack="true" />
                    <!-- Area for Additional Info -->
                    <asp:Panel ID="AdditionalInfo_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="AdditionalInfo_ToggleImage" runat="server" src="~/images/SFTheme/acc_h.png" />
                            Additional Information
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="AdditionalInfo_ContentPanel" runat="server" Style="overflow: hidden;
                        height: 0px">
                         <div class="last section">
                         The MedicationsListView control is available in Silverlight and WPF. You can
                        bind any data to the control, but this configuration of the control shows how to
                        conform to the Design Guidance for <i>Medications Views</i>
                        by using a sample data provider (Microsoft.Cui.IsvData.IsvDataProvider), and a rules engine (Microsoft.Cui.Data.RuleManager), that provides the correctly formatted data 
                        and visual templates for presentation of the data. Each cell in the table can be <i>bound</i> to
                        a different visual style. This can be seen in the above sample, where different rows have
                        different visual styles for the Drug Details column. Each cell in the table may have a tooltip which can be <i>bound</i> to
                        a different data value. The visibility of any column in the control can be controlled dynamically. To integrate the control into your application, you  will need to modify, or replace, the data access code, the data processing rules and some of the visual styling elements.<br/><br/>
                         The control also has an innovative <i>look-ahead</i> and <i>look-behind</i> feature. This shows
                        the user a summary of the items that have been scrolled out of the main view of
                        the control. Clicking on these summary items brings them back into the main view.<br/>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeAdditionalInfo" runat="Server" TargetControlID="AdditionalInfo_ContentPanel"
                        ExpandControlID="AdditionalInfo_HeaderPanel" CollapseControlID="AdditionalInfo_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="AdditionalInfo_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />
                    <!-- Area for Usage Hints Providing Data -->
                    <asp:Panel ID="UsageHints_HeaderPanel1" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="UsageHints_ToggleImage1" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Usage Hints &#8211; Providing Data
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="UsageHints_ContentPanel1" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="last section">
                            The WrapDataGrid DataContext property can take any IEnumerable, rather than just
                            the DataManager shown in the sample XAML.
                            <br>
                            <br>
                            The WrapDataGrid will use the IEnumerator returned from the IEnumerable:GetEnumerator
                            method, and create a row (DataBoundRow) for each successful call to IEnumerator:MoveNext.
                            <br>
                            <br>
                            The row (DataBoundRow) will have its DataSource property set to the object returned
                            from the IEnumerator.
                            <br />
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtenderUsageHints_1" runat="Server"
                        TargetControlID="UsageHints_ContentPanel1" ExpandControlID="UsageHints_HeaderPanel1"
                        CollapseControlID="UsageHints_HeaderPanel1" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="UsageHints_ToggleImage1" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Usage Hints Providing Data section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Usage Hints Providing Data section" SuppressPostBack="true" />
                    <!-- Area for Usage Hints Showing Data -->
                    <asp:Panel ID="UsageHints_HeaderPanel2" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="UsageHints_ToggleImage2" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Usage Hints &#8211; Showing Data
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="UsageHints_ContentPanel2" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="last section">
                            The WrapDataGrid can have a variable number of columns. These are configured by
                            creating WrapDataGridColumn instances and assigning them to the WrapDataGridColumns
                            collection.<br>
                            <br>
                            Each object returned from the DataSource will cause a DataBoundRow to be created,
                            (as described in <i>Usage Hints - Providing Data</i> above), and the DataBoundRow
                            creates a DataBoundCell for every column. Like its DataBoundRow parent, the DataBoundCell
                            also has its DataSource property set to the same object.<br />
                            <ul>
                                <li>WrapDataGrid instances have many DataBoundRow instances</li>
                                <li>DataBoundRow instances have many DataBoundCell instances (one for every column)</li><br>
                                <br>
                            </ul>
                            Each column has a CellTemplate property, which specifies how the DataBoundCell will
                            be rendered.<br>
                            <br />
                            The XAML code below shows how to configure a column so that the DataBoundCell will
                            show its Description property.
                            <pre>
                        &lt;WrapDataGridColumn.CellTemplate&gt;
                            &lt;DataTemplate&gt;
                                &lt;TextBlock Margin=&quot;0,0,10,0&quot; HorizontalAlignment=&quot;Right&quot; 
                                Text=&quot;{Binding Description}&quot;/&gt;
                            &lt;/DataTemplate&gt;
                        &lt;/WrapDataGridColumn.CellTemplate&gt;
                        </pre>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtenderUsageHints_2" runat="Server"
                        TargetControlID="UsageHints_ContentPanel2" ExpandControlID="UsageHints_HeaderPanel2"
                        CollapseControlID="UsageHints_HeaderPanel2" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="UsageHints_ToggleImage2" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Usage Hints Showing Data section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Usage Hints Showing Data section" SuppressPostBack="true" />
                    <!-- Area for Usage Hints Binding Data -->
                    <asp:Panel ID="UsageHints_HeaderPanel3" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="UsageHints_ToggleImage3" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Usage Hints &#8211; Binding Data
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="UsageHints_ContentPanel3" runat="server" Style="overflow: hidden;
                        height: 0px">
                        <div class="last section">
                            The DataSource for a DataBoundRow needs to provide all the data for that row. Usually
                            the object will have different properties for each cell to use in binding display
                            data. The list below shows how an object with StartDate, DrugDetails and Reason
                            properties might be used by the cell templates for three columns:
                            <br />
                            <ul>
                                <li>Column1 might use : &lt;TextBlock&gt; Text=&quot;{Binding StartDate}&quot; ...
                                </li>
                                <li>Column2 might use : &lt;TextBlock&gt; Text=&quot;{Binding DrugDetails}&quot; ...
                                </li>
                                <li>Column3 might use : &lt;TextBlock&gt; Text=&quot;{Binding Reason}&quot; ...
                                </li>
                            </ul>
                            <br>
                            The data can be converted if needed; for example, if a DataSource for a row is an
                            object that consists of Name / Value pairs, like a dictionary, you may wish to convert
                            the data as it is rendered.<br>
                            <br />
                            The sample uses a dictionary to store its data as it allows other data values to
                            be added without the need to create strongly typed properties for them. The list
                            below shows how the same three values could be shown using a converter for a cell
                            that has a DataSource of type dictionary:
                            <br />
                            <ul>
                                <li>Column1 might use : &lt;TextBlock&gt; Text=&quot;{Binding Converter={StaticResource
                                    dictionaryToStringConverter}, ConverterParameter='StartDate'}&quot; ... </li>
                                <li>Column2 might use : &lt;TextBlock&gt; Text=&quot;{Binding Converter={StaticResource
                                    dictionaryToStringConverter}, ConverterParameter='DrugDetails'}&quot; ... </li>
                                <li>Column3 might use : &lt;TextBlock&gt; Text=&quot;{Binding Converter={StaticResource
                                    dictionaryToStringConverter}, ConverterParameter='Reason'}&quot; ... </li>
                            </ul>
                            <br />
                            If you require a cell to have more than a single layout, you can use this approach
                            to allow data in the DataSource to specify a layout. The WrapDataGrid above uses
                            this to provide different layouts for the cells in the Drug Details column.
                            <br />
                            <br />
                            Below is the XAML code that uses a converter which finds and loads a DataTemplate
                            per row. The TemplateName is provided in a dictionary in this example. This gives
                            the flexibility of allowing every row in this column (that is to say, each cell
                            in that column) to provide a custom layout. The DataTemplate must be defined within
                            the application at the correct scope so that it can be found by the converter.
                            <br />
                            <pre>
    &lt;WrapDataGridColumn.CellTemplate&gt;
        &lt;DataTemplate&gt;
            &lt;ContentControl 
            Content=&quot;{Binding Converter={StaticResource dictionaryToDataTemplateConverter}, 
            ConverterParameter='TemplateName'}&quot; /&gt;
        &lt;/DataTemplate&gt;
    &lt;/WrapDataGridColumn.CellTemplate&gt;
                        </pre>
                            Using this approach, the columns cells can be rendered with different templates.
                            Below are two Microsoft Health CUI compliant templates for the MedicationsListView
                            control Drug Details column, as used on this page.
                            <pre>
    &lt;DataTemplate x:Key=&quot;NameStrengthFrequency&quot;&gt;
    &lt;Grid Grid.Column=&quot;0&quot; Grid.Row=&quot;1&quot; Height=&quot;Auto&quot; Width=&quot;Auto&quot;&gt;
    &lt;Grid.RowDefinitions&gt;
        &lt;RowDefinition Height=&quot;*&quot;&gt;&lt;/RowDefinition&gt;
        &lt;RowDefinition Height=&quot;*&quot;&gt;&lt;/RowDefinition&gt;
        &lt;RowDefinition Height=&quot;*&quot;&gt;&lt;/RowDefinition&gt;
    &lt;/Grid.RowDefinitions&gt;
    &lt;Grid.ColumnDefinitions&gt;
        &lt;ColumnDefinition Width=&quot;*&quot;&gt;&lt;/ColumnDefinition&gt;
    &lt;/Grid.ColumnDefinitions&gt;
    &lt;Grid Grid.Column=&quot;0&quot; Grid.Row=&quot;1&quot; VerticalAlignment=&quot;Top&quot;&gt;
        &lt;ctl:WrapPanel  &gt;
            &lt;TextBlock  FontWeight=&quot;Bold&quot; Text=&quot;{Binding Converter={StaticResource 
            dictionaryToStringConverter}, ConverterParameter='DrugName'}&quot;/&gt;                   
                &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource 
                dictionaryToStringConverter}, ConverterParameter='StrengthSeparator'}&quot;/&gt;                   
                &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource 
                dictionaryToStringConverter}, ConverterParameter='Strength'}&quot;/&gt;                   
                &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource 
                dictionaryToStringConverter}, ConverterParameter='FormNameSeparator'}&quot;/&gt;                   
                &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource 
                dictionaryToStringConverter}, ConverterParameter='FormName'}&quot;/&gt;
            &lt;/ctl:WrapPanel&gt;
    &lt;/Grid&gt;
    &lt;Grid Grid.Column=&quot;0&quot; Grid.Row=&quot;2&quot; VerticalAlignment=&quot;Top&quot;&gt;
        &lt;ctl:WrapPanel &gt;
            &lt;StackPanel Orientation=&quot;Horizontal&quot; VerticalAlignment=&quot;Bottom&quot;&gt;
                &lt;TextBlock FontFamily=&quot;Courier New&quot; FontSize=&quot;16&quot; 
                Margin=&quot;0,2,0,0&quot; Text=&quot;{Binding 
                Converter={StaticResource dictionaryToStringConverter}, 
                ConverterParameter='DoseLabel'}&quot; Foreground=&quot;#336699&quot;/&gt;                                
                &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource dictionaryToStringConverter}, 
                ConverterParameter='DOSEQTY'}&quot;/&gt;                   
            &lt;/StackPanel&gt;
            &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource 
            dictionaryToStringConverter}, ConverterParameter='DOSEQTYSeparator'}&quot;/&gt;                   
            &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource 
            dictionaryToStringConverter}, ConverterParameter='RouteName'}&quot;/&gt;                   
            &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource 
            dictionaryToStringConverter}, ConverterParameter='RouteNameSeparator'}&quot;/&gt;                   
            &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource 
            dictionaryToStringConverter}, ConverterParameter='Frequency'}&quot;/&gt;                   
        &lt;/ctl:WrapPanel&gt;
    &lt;/Grid&gt;
    &lt;/Grid&gt;
    &lt;/DataTemplate&gt;

    &lt;DataTemplate x:Key=&quot;NameFormFrequency&quot;&gt;
    &lt;Grid Grid.Column=&quot;0&quot; Grid.Row=&quot;1&quot; Height=&quot;Auto&quot; Width=&quot;Auto&quot;&gt;
    &lt;Grid.RowDefinitions&gt;
        &lt;RowDefinition Height=&quot;*&quot;&gt;&lt;/RowDefinition&gt;
        &lt;RowDefinition Height=&quot;*&quot;&gt;&lt;/RowDefinition&gt;
        &lt;RowDefinition Height=&quot;*&quot;&gt;&lt;/RowDefinition&gt;
    &lt;/Grid.RowDefinitions&gt;
    &lt;Grid.ColumnDefinitions&gt;
        &lt;ColumnDefinition Width=&quot;*&quot;&gt;&lt;/ColumnDefinition&gt;
    &lt;/Grid.ColumnDefinitions&gt;
    &lt;Grid Grid.Column=&quot;0&quot; Grid.Row=&quot;1&quot; VerticalAlignment=&quot;Top&quot;&gt;
        &lt;ctl:WrapPanel&gt;
            &lt;TextBlock  TextWrapping=&quot;Wrap&quot; Text=&quot;{Binding Converter={StaticResource 
            dictionaryToStringConverter}, ConverterParameter='DrugName'}&quot;/&gt;                   
            &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource 
            dictionaryToStringConverter}, ConverterParameter='FormNameSeparator'}&quot;/&gt;                   
            &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource 
            dictionaryToStringConverter}, ConverterParameter='FormName'}&quot;/&gt;                   
        &lt;/ctl:WrapPanel&gt;
    &lt;/Grid&gt;
    &lt;Grid Grid.Column=&quot;0&quot; Grid.Row=&quot;2&quot; VerticalAlignment=&quot;Top&quot;&gt;
        &lt;ctl:WrapPanel&gt;
            &lt;StackPanel Orientation=&quot;Horizontal&quot; VerticalAlignment=&quot;Bottom&quot;&gt;
                &lt;TextBlock FontFamily=&quot;Courier New&quot; Margin=&quot;0,2,0,0&quot;  
                FontSize=&quot;16&quot; Text=&quot;{Binding 
                Converter={StaticResource dictionaryToStringConverter}, 
                ConverterParameter='DoseLabel'}&quot; Foreground=&quot;#336699&quot;/&gt;                                
                &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource 
                dictionaryToStringConverter}, ConverterParameter='DOSEQTY'}&quot;/&gt;                   
                &lt;/StackPanel&gt;
            &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource dictionaryToStringConverter}, 
            ConverterParameter='DOSEQTYSeparator'}&quot;/&gt;                   
                &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource 
                dictionaryToStringConverter}, ConverterParameter='RouteName'}&quot;/&gt;                   
                &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource 
                dictionaryToStringConverter}, ConverterParameter='RouteNameSeparator'}&quot;/&gt;                   
                &lt;TextBlock  Text=&quot;{Binding Converter={StaticResource 
                dictionaryToStringConverter}, ConverterParameter='Frequency'}&quot;/&gt;                   
         &lt;/ctl:WrapPanel&gt;
    &lt;/Grid&gt;
    &lt;/Grid&gt;
    &lt;/DataTemplate&gt;
                        </pre>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="CollapsiblePanelExtenderUsageHints_3" runat="Server"
                        TargetControlID="UsageHints_ContentPanel3" ExpandControlID="UsageHints_HeaderPanel3"
                        CollapseControlID="UsageHints_HeaderPanel3" Collapsed="True" ExpandDirection="Vertical"
                        ImageControlID="UsageHints_ToggleImage3" ExpandedImage="~/images/SFTheme/acc_v.png"
                        ExpandedText="Click to collapse the Usage Hints Binding Data section" CollapsedImage="~/images/SFTheme/acc_h.png"
                        CollapsedText="Click to expand the Usage Hints Binding Data section" SuppressPostBack="true" />
                    <!-- Area for Usage Hints Sample XAML -->
                    <asp:Panel ID="UsageHints_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="UsageHints_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Usage Hints &#8211; Sample XAML
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="UsageHints_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="last section">
                            <p>
                                The MedicationsListView control, as shown at the top of the page, is configured
                                with the following XAML code. Other supporting elements are needed, including converters,
                                templates, and data providers.
                            </p>
                            <pre>
&lt;ctl:WrapDataGrid Grid.Row=&quot;1&quot; Margin=&quot;4,0&quot; x:Name=&quot;WrapDataGrid&quot; 
          SelectionMode=&quot;MultipleSimple&quot; 
          LookAheadCellTemplate=&quot;{StaticResource LookAheadCellTemplate}&quot; 
          LookBehindCellTemplate=&quot;{StaticResource LookBehindCellTemplate}&quot;
          LookAheadSummaryCellTemplate=&quot;{StaticResource LookAheadSummaryCellTemplate}&quot; 
          LookBehindSummaryCellTemplate=&quot;{StaticResource LookBehindSummaryCellTemplate}&quot;                         
          OnColumnHeaderClick=&quot;WrapDataGrid_ColumnHeaderClick&quot;
          OnGroupingRender=&quot;WrapDataGrid_OnGroupingRender&quot;         
          OnGroupHeaderClick=&quot;WrapDataGrid_OnGroupHeaderClick&quot;
          OnSelectionChanged=&quot;WrapDataGrid_OnSelectionChanged&quot;
          GroupingDataTemplatePresentation=&quot;{StaticResource groupingTemplatePresentation}&quot;                                        
          DataContext=&quot;{StaticResource DataManager}&quot; 
          AscendingOrderIndicatorDataTemplate=&quot;{StaticResource AscendingIndicator}&quot; 
          DescendingOrderIndicatorDataTemplate=&quot;{StaticResource DescendingIndicator}&quot;
          AlternatingRowBackground=&quot;#f2f2f2&quot;
          &gt;
    
        &lt;WrapDataGrid.WrapDataGridColumns&gt;
            &lt;ctl:WrapDataGridColumn ColumnDefinition=&quot;{StaticResource WGColumn1}&quot; 
            BodyMarginAsString=&quot;0,0,8,0&quot;&gt;
                    &lt;WrapDataGridColumn.HeaderTemplate&gt;
                    &lt;DataTemplate&gt;
                        &lt;TextBlock HorizontalAlignment=&quot;Right&quot; Height=&quot;20&quot; Margin=&quot;0,2,5,0&quot; 
                        Text=&quot;Start Date&quot;/&gt;         
                        &lt;/DataTemplate&gt;
                    &lt;/WrapDataGridColumn.HeaderTemplate&gt;
                    &lt;WrapDataGridColumn.CellTemplate&gt;
                        &lt;DataTemplate&gt;
                            &lt;TextBlock Margin=&quot;0,0,10,0&quot; HorizontalAlignment=&quot;Right&quot; 
                            Text=&quot;{Binding Converter={StaticResource dictionaryToStringConverter}, 
                            ConverterParameter='StartDate'}&quot;/&gt;
                        &lt;/DataTemplate&gt;
                    &lt;/WrapDataGridColumn.CellTemplate&gt;
                &lt;/ctl:WrapDataGridColumn&gt;

                &lt;ctl:WrapDataGridColumn ColumnDefinition=&quot;{StaticResource WGColumn2}&quot; 
                BodyMarginAsString=&quot;8,0,0,0&quot;&gt;
                    &lt;WrapDataGridColumn.CellTemplate&gt;
                        &lt;DataTemplate&gt;
                            &lt;ContentControl 
                            Content=&quot;{Binding Converter={StaticResource 
                            dictionaryToDataTemplateConverter}, 
                            ConverterParameter='TemplateName'}&quot; /&gt;
                        &lt;/DataTemplate&gt;
                    &lt;/WrapDataGridColumn.CellTemplate&gt;
                    &lt;WrapDataGridColumn.HeaderTemplate&gt;
                        &lt;DataTemplate&gt;
                            &lt;TextBlock Margin=&quot;5,2,0,0&quot; Height=&quot;20&quot; Text=&quot;Drug Details&quot;/&gt;    
                        &lt;/DataTemplate&gt;
                    &lt;/WrapDataGridColumn.HeaderTemplate&gt;
                &lt;/ctl:WrapDataGridColumn&gt;

                &lt;ctl:WrapDataGridColumn ColumnDefinition=&quot;{StaticResource WGColumn3}&quot; 
                BodyMarginAsString=&quot;8,0,0,0&quot;&gt;
                    &lt;WrapDataGridColumn.CellTemplate&gt;
                        &lt;DataTemplate&gt;
                            &lt;TextBlock Margin=&quot;0,0,3,0&quot; FontStyle=&quot;Italic&quot; Foreground=&quot;#585858&quot; 
                            MarginTextWrapping=&quot;Wrap&quot; 
                            Text=&quot;{Binding Converter={StaticResource dictionaryToStringConverter}, 
                            ConverterParameter='Reason'}&quot;/&gt;                  
                        &lt;/DataTemplate&gt;
                    &lt;/WrapDataGridColumn.CellTemplate&gt;
                    &lt;WrapDataGridColumn.HeaderTemplate&gt;
                        &lt;DataTemplate&gt;
                            &lt;TextBlock Margin=&quot;5,2,0,0&quot; Height=&quot;20&quot; Text=&quot;Reason&quot;/&gt;    
                        &lt;/DataTemplate&gt;
                    &lt;/WrapDataGridColumn.HeaderTemplate&gt;
                &lt;/ctl:WrapDataGridColumn&gt;

                &lt;ctl:WrapDataGridColumn ColumnDefinition=&quot;{StaticResource WGColumn4}&quot; 
                BodyMarginAsString=&quot;8,0,0,0&quot;&gt;
                    &lt;WrapDataGridColumn.CellTemplate&gt;
                        &lt;DataTemplate&gt;
                            &lt;TextBlock Margin=&quot;0,0,10,0&quot; FontWeight=&quot;Bold&quot; 
                            Text=&quot;{Binding Converter={StaticResource dictionaryToStringConverter}, 
                            ConverterParameter='MedicationStatus'}&quot;/&gt;
                  &lt;/DataTemplate&gt;
                    &lt;/WrapDataGridColumn.CellTemplate&gt;
                    &lt;WrapDataGridColumn.HeaderTemplate&gt;
                        &lt;DataTemplate&gt;
                            &lt;TextBlock Margin=&quot;5,2,0,0&quot; Height=&quot;20&quot; Text=&quot;Status&quot;/&gt;    
                        &lt;/DataTemplate&gt;
                    &lt;/WrapDataGridColumn.HeaderTemplate&gt;
                &lt;/ctl:WrapDataGridColumn&gt;

                &lt;ctl:WrapDataGridColumn ColumnDefinition=&quot;{StaticResource WGColumn5}&quot; 
                BodyMarginAsString=&quot;0,0,8,0&quot;&gt;
                    &lt;WrapDataGridColumn.CellTemplate&gt;
                        &lt;DataTemplate&gt;
                            &lt;TextBlock Margin=&quot;0,0,4,0&quot; HorizontalAlignment=&quot;Right&quot; 
                            Text=&quot;{Binding Converter={StaticResource dictionaryToStringConverter}, 
                            ConverterParameter='ReviewDate'}&quot;/&gt;                                        
                        &lt;/DataTemplate&gt;
                    &lt;/WrapDataGridColumn.CellTemplate&gt;
                    &lt;WrapDataGridColumn.HeaderTemplate&gt;
                        &lt;DataTemplate&gt;
                            &lt;TextBlock HorizontalAlignment=&quot;Right&quot; Height=&quot;20&quot; Text=&quot;Review Date&quot; 
                            Margin=&quot;0,2,5,0&quot;/&gt;                                    
                        &lt;/DataTemplate&gt;
                    &lt;/WrapDataGridColumn.HeaderTemplate&gt;
                &lt;/ctl:WrapDataGridColumn&gt;
            &lt;/WrapDataGrid.WrapDataGridColumns&gt;
&lt;/ctl:WrapDataGrid&gt;
                    </pre>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeUsageHints" runat="Server" TargetControlID="UsageHints_ContentPanel"
                        ExpandControlID="UsageHints_HeaderPanel" CollapseControlID="UsageHints_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="UsageHints_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Usage Hints Sample XAML section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Usage Hints Sample XAML section"
                        SuppressPostBack="true" />                    
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelWPF" HeaderText="<a id='medicationListViewWPFTab' href=javascript:TabClick('medicationListViewWPFTab'); title='WPF Tab'>WPF</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WPF control (screenshot):
                    <br />
                    <br />
                    <div>
                        <img class="controls_border" alt="MedicationsListView WPF control screenshot" title="MedicationsListView WPF control screenshot" runat="server" src="~/Components/Images/medicationslistview.GIF" />
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
