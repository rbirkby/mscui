<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="checklist.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.CheckList" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            The following table lists all of the guidance points in order. 
            Click on a guidance ID to find it in the guide.
            </p>
            <table class="checklist">
            	<tr>
                	<th>ID</th>
                    <th>Guideline</th>
                    <th>Compliance</th>
                </tr>
            	<tr>
                	<td class="number"><a href="structure/tabular.aspx">MEDv-020</a></td>
                    <td>
                    Present medications as lines of text within rows in a 
                    tabular format, where each row represents one medication
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="structure/gridlines.aspx">MEDv-021</a></td>
                    <td>
                    Avoid the use of strong grids and strong vertical lines 
                    (use subtle methods to support distinguishing between 
                    rows in the list)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="structure/gridlines.aspx">MEDv-022</a></td>
                    <td>
                    Use subtle alternate row shading
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="structure/icons.aspx">MEDv-023</a></td>
                    <td>
                    Support the display of icons following the 
                    text of the Drug Details column in the 
                    Medications List View
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="status/suspended.aspx">MEDv-024</a></td>
                    <td>
                    Use visual design to draw attention to suspended medications
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="status/currentpaststatus.aspx">MEDv-025</a></td>
                    <td>
                    Use visual design to distinguish a list of 
                    current medications from a list of past medications
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="columns/composite.aspx">MEDv-027</a></td>
                    <td>
                    Allow columns to contain more than 
                    one attribute for a single medication
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="status/formatting.aspx">MEDv-042</a></td>
                    <td>
                    Display the status of each medication in bold
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/notifications.aspx">MEDv-043</a></td>
                    <td>
                    The look-ahead notifications should be clearly 
                    joined to the &#39;up&#39; and &#39;down&#39; 
                    arrow controls of the scroll bar respectively
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/notifications.aspx">MEDv-044</a></td>
                    <td>
                    Restrict the look-ahead notifications 
                    to a single line each
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/notifications.aspx">MEDv-045</a></td>
                    <td>
                    Do not place controls or other notifications such 
                    that they separate the <span class="nowrap">look-ahead</span> 
                    notification from the medications in the Medications List View
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/Notifications/order.aspx">MEDv-048</a></td>
                    <td>
                    The <span class="nowrap">look-ahead</span> notification is populated from 
                    right to left such that the next drug in the list 
                    appears closest to the scroll bar
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/Notifications/order.aspx">MEDv-049</a></td>
                    <td>
                    The order of both the items in the <span class="nowrap">look-ahead</span> 
                    notification and the medications list should 
                    always be the same
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/Notifications/counts.aspx">MEDv-050</a></td>
                    <td>
                    When exceptionally long drug names require more space than is available 
                    in the <span class="nowrap">look-ahead</span> notification, display a 
                    count instead (as for past medications)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/Notifications/contents.aspx">MEDv-051</a></td>
                    <td>
                    Do not truncate or abbreviate drug names in the 
                    <span class="nowrap">look-ahead</span> notification
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/Notifications/counts.aspx">MEDv-052</a></td>
                    <td>
                    When there are more items than can be displayed 
                    in the <span class="nowrap">look-ahead</span> notification 
                    for current medications, 
                    display as many as possible and end the list with a 
                    count of the remaining items that could not be displayed
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/Notifications/counts.aspx">MEDv-053</a></td>
                    <td>
                    When a count is displayed in a 
                    <span class="nowrap">look-ahead</span> notification 
                    and one or more of the medications included in that count 
                    have decision support alerts, display a decision support 
                    alert icon next to the count
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/pastmedications.aspx">MEDv-054</a></td>
                    <td>
                    When displaying past medications only, display counts in 
                    the <span class="nowrap">look-ahead</span> notification 
                    and not drug names
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/current.aspx">MEDv-055</a></td>
                    <td>
                    When displaying current medications only, show drug names 
                    and decision support alert icons in the 
                    <span class="nowrap">look-ahead</span> notification
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/Notifications/delimiters.aspx">MEDv-056</a></td>
                    <td>
                    Use a delimiter that is unlikely to be interpreted as a character 
                    or number (such as a black dot &#39;&bull;&#39;), with a space either side to 
                    separate drug names and to separate the count from drug names
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/interaction.aspx">MEDv-058</a></td>
                    <td>
                    Update the <span class="nowrap">look-ahead</span> notifications 
                    dynamically in response to scrolling
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/interaction.aspx">MEDv-059</a></td>
                    <td>
                    Allow the <span class="nowrap">look-ahead</span> notification to change 
                    width dynamically to accommodate its contents 
                    up to the available width
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="currentpast/controls/defaults.aspx">MEDv-062</a></td>
                    <td>
                    Present the Medications List View 
                    with <strong>Current</strong> selected by default
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="currentpast/controls.aspx">MEDv-063</a></td>
                    <td>
                    Provide buttons for displaying current and past 
                    medications respectively in the Medications List 
                    View and label the buttons &#39;Current&#39; and 
                    &#39;Past&#39;
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="currentpast/controls/selection.aspx">MEDv-064</a></td>
                    <td>
                    Use the visual formatting of the <strong>Current</strong>
                    and <strong>Past</strong> buttons to indicate which is 
                    currently selected
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="currentpast/controls.aspx">MEDv-065</a></td>
                    <td>
                    Do not allow <strong>Current</strong> and <strong>Past</strong> buttons 
                    to be selected simultaneously
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="currentpast/controls.aspx">MEDv-066</a></td>
                    <td>
                    Supplement the <strong>Past</strong> button in the Medications 
                    List View with a <span class="nowrap">drop-down</span> control for 
                    displaying, selecting and applying a filter on the past medications view
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="currentpast/controls.aspx">MEDv-067</a></td>
                    <td>
                    Include an option for displaying all past 
                    medications in the drop-down control
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="filtering/notifications.aspx">MEDv-068</a></td>
                    <td>
                    When a filter notification is displayed, include 
                    a control for removing the filter within that notification
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="filtering/controlstate.aspx">MEDv-069</a></td>
                    <td>
                    When a filter is applied to past medications in Medications List 
                    View, the <strong>Past</strong> button should indicate that it is currently selected
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="filtering/controlstate.aspx">MEDv-070</a></td>
                    <td>
                    When a filter is applied to past medications in the Medications List 
                    View, display a filter notification at the top of the list below the 
                    column headings and above the scroll bar (thus 'pushing' the list of 
                    medications down a line)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="filtering/notifications.aspx">MEDv-071</a></td>
                    <td>
                    Display a description of the filter in use within 
                    the filter notification in the Medications List View
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="filtering/notifications.aspx">MEDv-072</a></td>
                    <td>
                    Include a count of the number of medications 
                    displayed and a count of the total (unfiltered) 
                    number of past medications in a filter notification
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="currentpast/currentmeds/recentpast.aspx">MEDv-074</a></td>
                    <td>
                    When displaying current medications, display 
                    a notification for medications that have been 
                    completed or discontinued within a specified 
                    time interval from the current time
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="currentpast/currentmeds/notificationcontents.aspx">MEDv-075</a></td>
                    <td>
                    Clearly display the time interval 
                    within the recent past notification
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="currentpast/currentmeds/notificationcontents.aspx">MEDv-077</a></td>
                    <td>
                    Display a count of the number of 
                    recently past medications within the recent 
                    past notification in the medication list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="Grouping/display.aspx">MEDv-083</a></td>
                    <td>
                    Present the Medications List View with no grouping active by default
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/groupingcontrol.aspx">MEDv-084</a></td>
                    <td>
                    Provide a standard drop-down list for displaying, selecting and 
                    applying grouping to the medications list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/groupingcontrol.aspx">MEDv-085</a></td>
                    <td>
                    Label the grouping control &#39;Group by&#39; 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/headings.aspx">MEDv-086</a></td>
                    <td>
                    Display clear and prominent headings for each group category
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/display.aspx">MEDv-087</a></td>
                    <td>
                    Retain the column sort order in the Medications List View when grouping is applied 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/expandcollapse.aspx">MEDv-089</a></td>
                    <td>
                    When a group is collapsed, supplement the group heading with a 
                    number representing a count of medications within that group
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="Grouping/headings.aspx">MEDv-090</a></td>
                    <td>
                    Do not display group headings for empty groups  
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/nullgroups.aspx">MEDv-091</a></td>
                    <td>
                    Provide &#39;null&#39; groups where necessary to support the display 
                    of medications that do not have a value for the attribute being used 
                    to group the medications
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/expandcollapse.aspx">MEDv-092</a></td>
                    <td>
                    Provide controls for expanding and collapsing individual groups. 
                    Place these controls at the beginning of the group heading
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="currentpast/currentmeds.aspx">MEDv-099</a></td>
                    <td>
                    By default, present current medications sorted 
                    reverse chronologically by a starting date, such 
                    that the most recent is first (top) in the list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="currentpast/pastmeds.aspx">MEDv-100</a></td>
                    <td>
                    By default, sort medications reverse 
                    chronologically by end date (or equivalent) 
                    such that the most recent is first (top) when 
                    the filter is set to &#39;Past&#39; in the 
                    Medications List View
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="sorting/sortingcontrol.aspx">MEDv-101</a></td>
                    <td>
                    Allow the sort order of a list in the medications list to be 
                    changed by clicking on a column heading
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="sorting/sortingcontrol.aspx">MEDv-102</a></td>
                    <td>
                    Allow the sort order of a list in the Medications List View 
                    to be reversed by clicking on the column heading for the column 
                    with the active sort applied
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="sorting/sortorder.aspx">MEDv-103</a></td>
                    <td>
                    Use formatting of the column heading to clearly indicate the 
                    column to which the sort order is currently applied
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="sorting/sortorder.aspx">MEDv-104</a></td>
                    <td>
                    Use an icon or symbol in the column heading to indicate the 
                    column by which the data is sorted and the direction of the sort
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="sorting/sortorder.aspx">MEDv-105</a></td>
                    <td>
                    When the sort order is changed from the default to another attribute 
                    in the Medications List View, retain the default as a secondary sort order
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Selection/highlighting.aspx">MEDv-122</a></td>
                    <td>
                    Support click (or keyboard selection using the spacebar) 
                    to select a medication in the list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Selection/highlighting.aspx">MEDv-123</a></td>
                    <td>
                    Clearly highlight selected medications in the medication list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="Selection/highlighting.aspx">MEDv-124</a></td>
                    <td>
                    Maintain the selection of a medication when switching between 
                    views of a patient&#39;s medications (such that a medication selected 
                    in a Medication List View is automatically selected when switching 
                    to the Drug Administration View)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Selection/highlighting.aspx">MEDv-125</a></td>
                    <td>
                    Maintain the selection of a medication when applying or changing 
                    a grouping or a sort order and ensure that the selection remains 
                    visible
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Selection/supporting.aspx">MEDv-126</a></td>
                    <td>
                    Support the selection of multiple items using CTRL and click for discrete 
                    selections, and SHIFT and click for contiguous selections
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Selection/supporting.aspx">MEDv-127</a></td>
                    <td>
                    Support keyboard-only equivalents such as SHIFT and arrow key for 
                    contiguous selection and the CTRL and SPACEBAR to toggle select 
                    and deselect when making non-contiguous selections
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Selection/contextmenu.aspx">MEDv-128</a></td>
                    <td>
                    Support the display of a context menu for selected 
                    medications in the Medications List View (for 
                    example, by right-clicking)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Selection/contextmenu.aspx">MEDv-129</a></td>
                    <td>
                    In the context menu, provide appropriate actions and options
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="Selection/contextmenu.aspx">MEDv-130</a></td>
                    <td>
                    In the context menu, support actions with icons where 
                    appropriate
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Selection/contextmenu.aspx">MEDv-131</a></td>
                    <td>
                    In the context menu, grey out actions that are unavailable 
                    or disallowed for one or more of the current selections
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Selection/contextmenu.aspx">MEDv-132</a></td>
                    <td>
                    In the context menu, define a consistent and static order 
                    of menu items in which frequently used actions are prioritised 
                    by placing them higher in the list
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Selection/contextmenu.aspx">MEDv-133</a></td>
                    <td>
                    In the context menu, group similar options so that direct 
                    actions, actions that permit addition of information, and 
                    actions that display more information, are each grouped 
                    together
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Selection/contextmenu.aspx">MEDv-135</a></td>
                    <td>
                    In the context menu for selections in the Medications 
                    List View, provide an option for displaying all details 
                    for the selected medication
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Selection/contextmenu.aspx">MEDv-136</a></td>
                    <td>
                    Include an option to access all details for 
                    one medication in the context menu
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="structure/tabular.aspx">MEDv-141</a></td>
                    <td>
                    Use composite columns to minimise the display of blank cells 
                    for some rows (that is, avoid placing each individual data 
                    point in a separate column)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="structure/listlength.aspx">MEDv-142</a></td>
                    <td>
                    When the list is scrolled to the end, display 
                    a space at the bottom of the list with a height 
                    equivalent to a line of text
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="structure/gridlines.aspx">MEDv-143</a></td>
                    <td>
                    Use at least alternate row shading or lines between rows
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="structure/gridlines.aspx">MEDv-144</a></td>
                    <td>
                    When using alternate row shading, ensure that colour and 
                    brightness of the background does not interfere with the 
                    readability of the foreground text
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="structure/gridlines.aspx">MEDv-145</a></td>
                    <td>
                    Supplement alternate shading with 1 point pale lines between rows
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="structure/emptylists.aspx">MEDv-146</a></td>
                    <td>
                    Display a message when a list is empty (for 
                    example, when there are no current medications)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="columns/mandatory.aspx">MEDv-147</a></td>
                    <td>
                    Provide a column that contains status information, 
                    including information that defines whether the 
                    medication is &#39;current&#39; or &#39;past&#39;
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="columns/mandatory.aspx">MEDv-148</a></td>
                    <td>
                    Provide a column that contains drug 
                    details according to 
                    <a href="../../MedicationLine.aspx" title="Links to Guidance - Medication Line page">
                    Medication Line</a> guidance
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="columns/mandatory/currentcol.aspx">MEDv-149</a></td>
                    <td>
                    When displaying current medications, provide a column that contains an 
                    initiation date (such as the date of the first planned administration). 
                    The examples on these pages show a Start Date column
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="columns/mandatory/pastcol.aspx">MEDv-150</a></td>
                    <td>
                    When displaying past medications, provide a column that contains a stop 
                    date (such as the date of the last administration, or the date that the 
                    medication was discontinued). The examples on these pages show an End 
                    Date column
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="columns/date.aspx">MEDv-151</a></td>
                    <td>
                    When an end date column is displayed, place a start date column 
                    before (to the left of) the end date column
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="columns/date.aspx">MEDv-152</a></td>
                    <td>
                    When an end date column is displayed, and there is no duration 
                    column, place a start date column adjacent to the end date column
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="columns/date.aspx">MEDv-153</a></td>
                    <td>
                    Use fixed width columns for dates
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="columns/date.aspx">MEDv-154</a></td>
                    <td>
                    Maintain consistent placement of date columns relative to one 
                    another and relative to the Drug Details column in both current 
                    and past medications
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="columns/headings.aspx">MEDv-155</a></td>
                    <td>
                    Label columns with text that describes the contents 
                    unambiguously and succinctly (such as &#39;Status&#39;, 
                    &#39;Date Prescribed&#39; or &#39;First Administration&#39;)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="columns/headings.aspx">MEDv-156</a></td>
                    <td>
                    Use a unique heading for each column
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="columns/composite.aspx">MEDv-157</a></td>
                    <td>
                    When combining two attributes that 
                    have the same data type (such as dates), 
                    include labels for both attributes in 
                    the column heading
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="columns/composite.aspx">MEDv-158</a></td>
                    <td>
                    When combining two attributes that have the same data 
                    types (such as dates), include labels for both attributes 
                    within the cell
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="columns/dimensions.aspx">MEDv-159</a></td>
                    <td>
                    Maintain the relative proportions of columns 
                    such that the Drug Details column is the widest
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="columns/dimensions.aspx">MEDv-160</a></td>
                    <td>
                    Avoid the need for horizontal scrolling by 
                    limiting the number of columns visible at 
                    any one time
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="columns/dimensions.aspx">MEDv-161</a></td>
                    <td>
                    Define minimum widths for all columns
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="status/formatting.aspx">MEDv-162</a></td>
                    <td>
                    Ensure that all medications have a status 
                    value and the status cannot be blank
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="status/formatting.aspx">MEDv-163</a></td>
                    <td>
                    Limit status descriptions to short 
                    phrases, preferably no more than two words
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="status/additionalinfo.aspx">MEDv-164</a></td>
                    <td>
                    Allow status to be supplemented with 
                    additional information (such as pharmacy verified)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="status/additionalinfo.aspx">MEDv-165</a></td>
                    <td>
                    Use the status description to differentiate 
                    between medications that have no recorded 
                    administration events and those that have
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="status/suspended.aspx">MEDv-166</a></td>
                    <td>
                    Support a status of &#39;suspended&#39; 
                    and include medications with this 
                    status in current medications
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="status/datavalues.aspx">MEDv-167</a></td>
                    <td>
                    Assign a status of &#39;Started&#39; to medications 
                    that have an administration event recorded 
                    and have further scheduled administrations
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="status/datavalues.aspx">MEDv-168</a></td>
                    <td>
                    Assign a status of &#39;Not Started&#39; 
                    to medications that have administration 
                    scheduled and a start date in the future
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="status/suspended.aspx">MEDv-169</a></td>
                    <td>
                    Assign a status of &#39;Suspended&#39; 
                    to medications that are marked as not 
                    to be administered, but which are 
                    intended to be resumed at a later date
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="status/datavalues.aspx">MEDv-170</a></td>
                    <td>
                    Assign a status of &#39;Completed&#39; 
                    to medications that have administration 
                    events recorded according to their schedule 
                    (within tolerances) and have an end date in 
                    the past
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="status/datavalues.aspx">MEDv-171</a></td>
                    <td>
                    Assign a status of &#39;Discontinued&#39; to medications 
                    that were stopped on a date that preceded one or more of 
                    the scheduled administrations
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="status/currentpaststatus.aspx">MEDv-172</a></td>
                    <td>
                    Define medications with a status of either &#39;Started&#39;, 
                    &#39;Not Started&#39; or &#39;Suspended&#39; as current 
                    medications
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="status/currentpaststatus.aspx">MEDv-173</a></td>
                    <td>
                    Define medications with a status of either &#39;Completed&#39; 
                    or &#39;Discontinued&#39; as past medications
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="currentpast/currentmeds.aspx">MEDv-173</a></td>
                    <td>
                    When displaying current medications, place the 
                    drug details in the first (furthest left) column
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="currentpast/controls.aspx">MEDv-174</a></td>
                    <td>
                    Ensure that either the <strong>Current</strong> 
                    or the <strong>Past</strong> button is selected at any one time
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="currentpast/currentmeds/recentpast.aspx">MEDv-174</a></td>
                    <td>
                    Use formatting to distinguish the recent 
                    past notifications from medications in the list
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="currentpast/pastmeds.aspx">MEDv-175</a></td>
                    <td>
                    When displaying past medications, place 
                    the status column first (furthest left) 
                    and the Drug Details column second
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="filtering/notifications.aspx">MEDv-176</a></td>
                    <td>
                    Clearly label the counts (number of medications 
                    displayed and total unfiltered number) with text 
                    that allows them to be differentiated
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/purpose.aspx">MEDv-177</a></td>
                    <td>
                    When displaying a list of current or past medications, 
                    and the scroll bar is active because the list is longer 
                    than the space available to display them, provide a clear 
                    indication that there are medications out of view
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="LookAhead/purpose.aspx">MEDv-178</a></td>
                    <td>
                    When displaying current medications, supplement the 
                    standard scroll bar with notifications that display 
                    the names of drugs that are out of view. This guide 
                    refers to this kind of scroll bar as a look-ahead 
                    scroll bar (LASB)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/layout.aspx">MEDv-179</a></td>
                    <td>
                    When displaying a LASB, reserve a space at 
                    the top and bottom of the list for look-ahead 
                    notifications
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="LookAhead/layout.aspx">MEDv-180</a></td>
                    <td>
                    Use a pale solid background colour for the 
                    space reserved for 
                    <span class="nowrap">look-ahead</span> notifications 
                    that is sufficient to distinguish the space 
                    from the background of the list
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="LookAhead/grouping.aspx">MEDv-181</a></td>
                    <td>
                    When grouping is applied, and there is a collapsed 
                    group out of view, display drug names in the look-ahead 
                    scroll bar for any drug that is out of view, irrespective 
                    of whether it is within a collapsed group or an expanded group
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/interaction.aspx">MEDv-182</a></td>
                    <td>
                    Do not allow the look-ahead notification to 
                    be used for navigation by clicking on areas 
                    of the notification, such as drug names or 
                    counts
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="LookAhead/notifications/contents.aspx">MEDv-183</a></td>
                    <td>
                    If any of the drug name text (other than the letter 
                    ascenders and descenders) is obscured by the boundaries 
                    of the list, include that drug in the look-ahead notification
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/Notifications/delimiters.aspx">MEDv-184</a></td>
                    <td>
                    Do not use leading or trailing delimiters
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/grouping.aspx">MEDv-185</a></td>
                    <td>
                    Do not include additional text or formatting to 
                    indicate grouping in the look-ahead notifications 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/Notifications/formatting.aspx">MEDv-186</a></td>
                    <td>
                    Display drug names in bold and in black text by default 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="LookAhead/Notifications/formatting.aspx">MEDv-187</a></td>
                    <td>
                    Display counts and descriptive text (such as &#39;more&#39;) in normal weight font  
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="LookAhead/Notifications/formatting.aspx">MEDv-188</a></td>
                    <td>
                    Use a light solid background colour for the notifications that is 
                    both sufficiently different from the colour in the space reserved 
                    for notifications and sufficiently different from the black text 
                    in the notification  
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="LookAhead/Notifications/formatting.aspx">MEDv-189</a></td>
                    <td>
                    Do not use a border in a dark colour or with a weight greater 
                    than <span class="nowrap">1 point</span> for a 
                    <span class="nowrap">look-ahead</span> notification 
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/groupingcontrol.aspx">MEDv-190</a></td>
                    <td>
                    Include an option in the drop-down list to set the grouping to &#39;None&#39; 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/display.aspx">MEDv-191</a></td>
                    <td>
                    Display groups expanded by default 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/rowshading.aspx">MEDv-192</a></td>
                    <td>
                    Re-start alternate row shading at the beginning of each group. 
                    (Alternate row shading is not needed if there is only one medication 
                    in each group) 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/rowshading.aspx">MEDv-193</a></td>
                    <td>
                    When a grouping is selected in the grouping control, ensure that at least 
                    one group heading is visible in the newly grouped list 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/nullgroups.aspx">MEDv-194</a></td>
                    <td>
                    Display the label for a &#39;null&#39; group heading in brackets 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="Grouping/nullgroups.aspx">MEDv-195</a></td>
                    <td>
                    Display &#39;null&#39; groups at the top of the list of groups 
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/expandcollapse.aspx">MEDv-196</a></td>
                    <td>
                    Support the selection of group headings and the display of a context 
                    menu that includes options for collapsing and expanding all columns
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/combining.aspx">MEDv-197</a></td>
                    <td>
                    When one or more medications belong to more than one group 
                    (such as analgesic and non-steroidal <span class="nowrap">
                    anti-inflammatory</span>), create a new group and 
                    label it with the group names combined (such as 
                    &#39;Analgesic; Non-steroidal Anti-inflammatory&#39;)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/combining.aspx">MEDv-198</a></td>
                    <td>
                    Display each medication in only one group (do 
                    not duplicate medications so that they can be 
                    displayed in more than one group)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Grouping/combining.aspx">MEDv-199</a></td>
                    <td>
                    When combining group names, display the names in the same order 
                    as they would appear in a list that is sorted by that attribute
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="Grouping/combining.aspx">MEDv-200</a></td>
                    <td>
                    When combining group names, separate the labels with a semi-colon
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="Structure/levelsofdetail.aspx">MEDv-201</a></td>
                    <td>
                    Provide a control that allows the type and quantity of information 
                    displayed to be changed, such that the rows and columns may change 
                    in number and be presented with a different layout
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Selection/highlighting.aspx">MEDv-202</a></td>
                    <td>
                    Ensure that there are no medications selected by default when a list is opened 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="Selection/supporting.aspx">MEDv-203</a></td>
                    <td>
                    When an action is applied to more than one medication, 
                    display a summary of the selected medications before allowing 
                    the user to complete the action
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            </table>            
      </div>
    </div>
</asp:Content>