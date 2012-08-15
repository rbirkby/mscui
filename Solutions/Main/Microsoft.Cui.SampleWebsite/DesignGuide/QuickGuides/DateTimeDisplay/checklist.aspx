<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="checklist.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.CheckList" %>

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
                	<td class="number"><a href="date/month.aspx">D+Ta-0002</a></td>
                    <td>
                    Display the month textually, not numerically
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/month.aspx">D+Ta-0003</a></td>
                    <td>
                    Display the month with only the first letter in capitals
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/year.aspx">D+Ta-0004</a></td>
                    <td>
                    Display the year value numerically using four digits
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="date/alignment.aspx">D+Ta-0005</a></td>
                    <td>
                    Align dates when displaying dates in a vertical column, such as in a table
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/shortdate.aspx">D+Ta-0006</a></td>
                    <td>
                    Display dates using the short date format in all instances of clinical usage 
                    affecting patient treatment, including patient identification
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/shortdate/daymonth.aspx">D+Ta-0007</a></td>
                    <td>
                    Display the month as a three letter abbreviation: Jan, Feb, Mar, Apr, Jun, Jul, 
                    Aug, Sep, Oct, Nov and Dec, with May being displayed in full
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/month.aspx">D+Ta-0008</a></td>
                    <td>
                    When displaying the month, do not include any punctuation, such as a full stop
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/shortdate/separators.aspx">D+Ta-0009</a></td>
                    <td>
                    Use a single hyphen to separate the day and month, and the month and year
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/shortdate.aspx">D+Ta-0010</a></td>
                    <td>
                    When using the short date format, ignore the user's regional settings
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/longdate.aspx">D+Ta-0011</a></td>
                    <td>
                    Use the long date format when communicating with the patient
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/longdate/daymonth.aspx">D+Ta-0012</a></td>
                    <td>
                    Display the month name in full
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/longdate/separators.aspx">D+Ta-0013</a></td>
                    <td>
                    Use a single whitespace to separate the day and month, and the month and year
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/longdate.aspx">D+Ta-0014</a></td>
                    <td>
                    When using the long date format, follow the user's default regional 
                    settings ignoring any changes made by the user to these default settings
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="date/longdate.aspx">D+Ta-0015</a></td>
                    <td>
                    Use the long date format when interacting with screen readers
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="date/weekdays.aspx">D+Ta-0016</a></td>
                    <td>
                    When displaying the day of the week, use one of the following abbreviations: 
                    Mon, Tue, Wed, Thu, Fri, Sat and Sun
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="date/weekdays.aspx">D+Ta-0017</a></td>
                    <td>
                    Displaying the day of the week is optional, but when displayed, it must be 
                    placed immediately before the day value, with a single space separating the 
                    permitted abbreviated form of the day, from the day value
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/shortdate/daymonth.aspx">D+Ta-0018</a></td>
                    <td>
                    Display the day value using two digits (values less than 10 should appear 
                    with a zero in the first position)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/longdate/daymonth.aspx">D+Ta-0019</a></td>
                    <td>
                    Display the day value using two digits (values less than 10 should appear 
                    with a zero in the first position, unless the day value is displayed in 
                    ordinal form)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/longdate/daymonth.aspx">D+Ta-0020</a></td>
                    <td>
                    When displaying the day value as an ordinal number, the suffix used must be 
                    one of the following: st, nd, rd, th
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/longdate/daymonth.aspx">D+Ta-0021</a></td>
                    <td>
                    When displaying the day value as an ordinal number, the two-letter suffix must 
                    be displayed in lower case and as a superscript immediately after the number
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="date/nullvalues.aspx">D+Ta-0022</a></td>
                    <td>
                    Display null date using an appropriate value (for example 'Unknown' 
                    or 'Not recorded')
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/24hrclock.aspx">D+Tb-0001</a></td>
                    <td>
                    Display time using the 24-hour clock only
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/syntax.aspx">D+Tb-0002</a></td>
                    <td>
                    Display an exact time as HH:mm
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/syntax.aspx">D+Tb-0003</a></td>
                    <td>
                    Display hours using two digits (values less than 10 should appear with a 
                    zero in the first position)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/syntax.aspx">D+Tb-0004</a></td>
                    <td>
                    Display minutes using two digits (values less than 10 should appear with a 
                    zero in the first position)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/syntax.aspx">D+Tb-0005</a></td>
                    <td>
                    Display seconds as two digits (values less than 10 should appear with a 
                    zero in the first position)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/separators.aspx">D+Tb-0006</a></td>
                    <td>
                    Separate the hours and minutes with a colon
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/separators.aspx">D+Tb-0007</a></td>
                    <td>
                    Separate the minutes and seconds with a colon
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/separators.aspx">D+Tb-0008</a></td>
                    <td>
                    Separate date and time values with a white space
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/midnight.aspx">D+Tb-0009</a></td>
                    <td>
                    Display midnight as 00:00
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/midnight.aspx">D+Tb-0010</a></td>
                    <td>
                    Display the last minute in the day as 23:59
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/midnight.aspx">D+Tb-0011</a></td>
                    <td>
                    Display null times using an appropriate value, for example, 
                    'Unknown' and 'Not recorded'
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="time/syntax.aspx">D+Tb-0012</a></td>
                    <td>
                    Display seconds only if required
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="time/ranges.aspx">D+Tb-0013</a></td>
                    <td>
                    Display time ranges as two adjacent time displays, each identified by a 
                    contextually appropriate label, such as 'From' and 'To'
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/approx.aspx">D+Tb-0014</a></td>
                    <td>
                    Precede the display of an approximate time value with the word 'Approx'
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/approx.aspx">D+Tb-0015</a></td>
                    <td>
                    Display the time value using the guidance for exact time
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/approx.aspx">D+Tb-0016</a></td>
                    <td>
                    Leave a white space between the 'Approx' and the HH element of the time display
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="duration/abbreviations.aspx">D+Tb-0017</a></td>
                    <td>
                    Display durations using years, months, weeks, days, hours minutes and seconds, 
                    as appropriate
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="duration/syntax.aspx">D+Tb-0018</a></td>
                    <td>
                    Use whole numbers for time duration, for example, 1, 5 and 60. Do not use 
                    decimals or fractions, for example, 0.5, 1.5, &#190;
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="duration/nullvalues.aspx">D+Tb-0026</a></td>
                    <td>
                    Omit zero-valued units from the display 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="duration/separators.aspx">D+Tb-0027</a></td>
                    <td>
                    Display duration values and their respective units as pairs, 
                    with no intervening whitespace between the value and unit
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="duration/separators.aspx">D+Tb-0028</a></td>
                    <td>
                    Use a white space as the separator when displaying a duration 
                    composed of more than one unit
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="duration/syntax.aspx">D+Tb-0029</a></td>
                    <td>
                    Display time duration units in decreasing order of significance
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="duration/approx.aspx">D+Tb-0030</a></td>
                    <td>
                    Precede the display of an approximate duration value with the word 'Approx'
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="duration/approx.aspx">D+Tb-0031</a></td>
                    <td>
                    Leave a white space between the 'Approx' and the first element of an 
                    approximate duration value
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="time/24hrclock.aspx">D+Tb-0032</a></td>
                    <td>
                    Provide indication to the user that the 24-hour clock is in use
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="duration/abbreviations.aspx">D+Tb-0033</a></td>
                    <td>
                    Ensure that the following minimal set of duration unit abbreviations is supported: 
                    y for years, m for months, w for weeks, d for days, hr for hours, min for 
                    minutes and sec for seconds
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="duration/abbreviations.aspx">D+Tb-0034</a></td>
                    <td>
                    Allow the set of duration unit abbreviations to be extended appropriately, 
                    for example, 'hrs' as well as 'hr'. Ensure that any additions are unique 
                    within the entire set
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            </table>
            
      </div>
    </div>
</asp:Content>