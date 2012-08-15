<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="checklist.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.CheckList" %>

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
                	<td class="number"><a href="date/general.aspx">D+Tc-0001</a></td>
                    <td>
                    Adopt the NHS Common User Interface standard for Date Display for the format 
                    of any dates displayed within the date input control
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/freetext.aspx">D+Tc-0002</a></td>
                    <td>
                    Allow for both free text input of dates and the input of dates using 
                    a calendar control 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/freetext/editing.aspx">D+Tc-0003</a></td>
                    <td>
                    Allow the date elements to be individually edited (day, month and year) 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/calendar.aspx">D+Tc-0004</a></td>
                    <td>
                    Include the calendar icon within the boundary of the date input field 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/disambiguation.aspx">D+Tc-0005</a></td>
                    <td>
                    Provide disambiguation of any free text date input 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/freetext/instructions.aspx">D+Tc-0006</a></td>
                    <td>
                    Provide instructions on how to use the control, for example, via the use 
                    of tooltips. The instructions must contain details of the different date 
                    types that can be entered
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/freetext/arithmetic.aspx">D+Tc-0007</a></td>
                    <td>
                    Allow users to enter arithmetic shortcuts, such as '+3m' for three 
                    months later or '-2d' for two days earlier. The relevant operators 
                    are: '&#43;' for later dates (this is optional, as whole numbers should 
                    be treated as positive by default), '-' for earlier dates, 'd' for days 
                    'w' for weeks, 'm' for months, 'y' for years
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/freetext/unknown.aspx">D+Tc-0008</a></td>
                    <td>
                    Allow the input of 'Unknown' (or similar) to specify dates not known to the user 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/freetext/defaults.aspx">D+Tc-0009</a></td>
                    <td>
                    Display a default input within the free text date input control
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/calendar.aspx">D+Tc-0010</a></td>
                    <td>
                    Provide access to the calendar control via a calendar icon
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/CalInput/editing.aspx">D+Tc-0011</a></td>
                    <td>
                    Provide the ability to select a month independently, and a year 
                    independently. Signify the interactivity of these elements by 
                    suitable styling, for example as buttons or links, and ensure 
                    that they have descriptive tooltips
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/CalInput/controls.aspx">D+Tc-0012</a></td>
                    <td>
                    Provide a button to allow the user to enter today's date
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/CalInput/controls.aspx">D+Tc-0013</a></td>
                    <td>
                    Provide a link or button to close the control
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/CalInput/elements.aspx">D+Tc-0014</a></td>
                    <td>
                    Provide a visual indication of the current date
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/CalInput/elements.aspx">D+Tc-0015</a></td>
                    <td>
                    Include the days of the week within the calendar view
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/CalInput/instructions.aspx">D+Tc-0016</a></td>
                    <td>
                    Provide access to relevant instructional text (for example, via tooltips) 
                    on the clickable elements in the calendar header
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/calendar.aspx">D+Tc-0017</a></td>
                    <td>
                    Allow the calendar to be closed either when the user clicks away from 
                    the calendar or clicks on the calendar icon
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/CalInput/keyboard.aspx">D+Tc-0018</a></td>
                    <td>
                    Display the appropriate value in the free text field following 
                    selection of the date
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/CalInput/keyboard.aspx">D+Tc-0019</a></td>
                    <td>
                    Ensure that the control can be operated effectively via the keyboard 
                    (for example, using arrow keys)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/inputbox.aspx">D+Tc-0020</a></td>
                    <td>
                    Adopt the guidance provided in
                    <a href="../../Pdfs/Design%20Guidance%20--%20Time%20Display.pdf">
                    Design Guidance - Time Display</a> 
                    for the format of any dates displayed within the time input control
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/displayformat.aspx">D+Tc-0021</a></td>
                    <td>
                    Use the 24-hour clock only (rather than the 12-hour clock)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/inputbox.aspx">D+Tc-0022</a></td>
                    <td>
                    Use an 'Approx' check box to allow the user to indicate an approximate time
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/editing.aspx">D+Tc-0023</a></td>
                    <td>
                    Allow the time elements to be individually edited (hours, minutes and seconds)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/instructions.aspx">D+Tc-0024</a></td>
                    <td>
                    Provide access to instructions on how to use the control, for example, 
                    via the use of tooltips. The instructions must contain details of 
                    different time types that can be entered
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/arithmetic.aspx">D+Tc-0025</a></td>
                    <td>
                    Allow users to enter arithmetic shortcuts such as '+3h' for three hours 
                    later and '-2m' for two minutes earlier. The relevant operators are: '&#43;' 
                    for later times (this is optional, as whole numbers should be treated as 
                    positive by default), '-' for earlier times, 'h' for hours, 'm' for minutes, 
                    's' for seconds
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/unknown.aspx">D+Tc-0026</a></td>
                    <td>
                    Allow the entry of 'Unknown' (or similar) to specify times not known to the user
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/defaults.aspx">D+Tc-0027</a></td>
                    <td>
                    Display a default input within the time input control
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/editing.aspx">D+Tc-0028</a></td>
                    <td>
                    Increase/decrease the whole time by the least significant time unit 
                    if the entire value is selected or if no unit is selected
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/editing.aspx">D+Tc-0029</a></td>
                    <td>
                    Provide the ability to spin individual time units when selected
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="duration/displayformat.aspx">D+Tc-0030</a></td>
                    <td>
                    Adopt the guidance provided in 
                    <a href="../../Pdfs/Design%20Guidance%20--%20Time%20Display.pdf">
                    Design Guidance - Time Display</a> 
                    for the format of any times displayed within the duration input control
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="duration/instructions.aspx">D+Tc-0031</a></td>
                    <td>
                    Provide access to instructions on how to use the control, for example, 
                    via the use of tooltips. The instructions must contain details of the 
                    different units that can be entered
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="duration/editing.aspx">D+Tc-0032</a></td>
                    <td>
                    Allow entry of time duration units either singly or in combination
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="duration/editing.aspx">D+Tc-0033</a></td>
                    <td>
                    Allow editing of the individual elements of a duration
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="duration/abbreviations.aspx">D+Tc-0034</a></td>
                    <td>
                    Ensure that the following minimal set of duration unit abbreviations 
                    is supported: &#39;y&#39; for years, &#39;m&#39; for months, &#39;w&#39; 
                    for weeks, &#39;d&#39; for days, &#39;hr&#39; for hours, &#39;min&#39; for 
                    minutes and &#39;sec&#39; for seconds
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="duration/abbreviations.aspx">D+Tc-0035</a></td>
                    <td>
                    Allow the set of duration unit abbreviations to be extended appropriately, 
                    for example, 'hrs' as well as 'hr'. Ensure that any additions are unique 
                    within the entire set
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="duration/editing.aspx">D+Tc-0036</a></td>
                    <td>
                    Allow the user to optionally enter white space within the duration 
                    input, for example '3 hr 5 min' as well as '3hr 5min'
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="duration/disambiguation.aspx">D+Tc-0037</a></td>
                    <td>
                    Provide the facility for a user to disambiguate input which could be 
                    interpreted in more than one way, for example, 'm' (which could represent 
                    months or minutes)'
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/disambiguation.aspx">D+Tc-0038</a></td>
                    <td>
                    Provide the facility for a user to disambiguate a date entered via the date control
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/disambiguation.aspx">D+Tc-0039</a></td>
                    <td>
                    Display a message dialog box with appropriate instructional text if the data is 
                    ambiguous or incomplete
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="date/disambiguation.aspx">D+Tc-0040</a></td>
                    <td>
                    Display a maximum of two suggestions based on the data entered, plus an option 
                    to re-enter the value in the input field. Selection of a suggested value enters 
                    that value into the control
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/freetext/arithmetic.aspx">D+Tc-0041</a></td>
                    <td>
                    Within the date input control, do not allow users to input arithmetic shortcuts 
                    relating to time 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="date/freetext/defaults.aspx">D+Tc-0042</a></td>
                    <td>
                    When displaying a default input within the free text input box, provide the user 
                    with an example of date with either a non-specific value (such as the input mask) 
                    or a date appropriate to the clinical context (for example, 'today's date')
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/instructions.aspx">D+Tc-0043</a></td>
                    <td>
                    Provide indication to the user that the 24-hour clock is in use 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/arithmetic.aspx">D+Tc-0044</a></td>
                    <td>
                    Within the time input control do not allow users to input arithmetic 
                    shortcuts relating to date 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="time/defaults.aspx">D+Tc-0045</a></td>
                    <td>
                    When displaying a default input within the free text input box, provide 
                    the user with an example of time with either a non-specific value (such 
                    as the input mask) or a time appropriate to the clinical context 
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/freetext/editing.aspx">D+Tc-0046</a></td>
                    <td>
                    Within the date input control, allow users to select the entire value to 
                    facilitate rapid editing or entry of arithmetic shortcuts relating to date 
                    (only) 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/arithmetic.aspx">D+Tc-0047</a></td>
                    <td>
                    Within the time input control, allow users to select the entire value to 
                    facilitate rapid editing  or entry of arithmetic shortcuts relating to time (only)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="duration/timezones.aspx">D+Tc-0048</a></td>
                    <td>
                    Where a time duration spans the change between GMT and BST, show a pop-up 
                    to inform the user that the system will automatically handle the data within 
                    the appropriate time zone 
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="duration/timezones.aspx">D+Tc-0049</a></td>
                    <td>
                    Where a user adjusts time manually resulting in a time duration spanning 
                    a change between GMT and BST, show a pop-up to inform that user that the 
                    system will automatically adjust the data according to the appropriate 
                    time zone 
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            </table>
            
      </div>
    </div>
</asp:Content>
