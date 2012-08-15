<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="checklist.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationLine.CheckList" %>

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
                	<td class="number"><a href="attributes/drugname.aspx">MEDi-001</a></td>
                    <td>
                    Display generic drug names in bold
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="attributes/drugname.aspx">MEDi-002</a></td>
                    <td>
                    Display generic drug names in lowercase (capital letters may still 
                    be used for acronyms and abbreviations in some drug names such as 
                    amphotericin B, factor VIII, carbomer 974P)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="attributes/drugname.aspx">MEDi-003</a></td>
                    <td>
                    Display drug brand names in uppercase
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="attributes/dose.aspx">MEDi-007</a></td>
                    <td>
                    Provide a text label that reads &#39;DOSE&#39; before a dose
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/wrapping.aspx">MEDi-008</a></td>
                    <td>
                    Do not allow wrapping to separate a label from a value
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/textlabels.aspx">MEDi-009</a></td>
                    <td>
                    Use a different font and colour to differentiate labels from values
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/wrapping.aspx">MEDi-010</a></td>
                    <td>
                    When wrapping the text of a medication line, do so 
                    without breaking up the contents of a single attribute 
                    unless that single attribute will not fit on one line
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/wrapping.aspx">MEDi-011</a></td>
                    <td>
                    When wrapping the text of a medication line, keep 
                    trailing delimiters with the preceding attribute
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/truncation.aspx">MEDi-012</a></td>
                    <td>
                    If necessary, wrap but do not truncate 
                    medication line information
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="attributes/drugname.aspx">MEDi-013</a></td>
                    <td>
                    Where both the generic name and the brand name appear in a 
                    medication line, list the generic name first
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="formatlayout/numbers.aspx">MEDi-014</a></td>
                    <td>
                    Where possible, avoid the need for decimal points 
                    by changing the units without breaking convention
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/numbers.aspx">MEDi-015</a></td>
                    <td>
                    Do not put a trailing zero after a sub-decimal value 
                    (that is, &#39;0.5&#39; is correct but &#39;0.50&#39; 
                    is incorrect)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/numbers.aspx">MEDi-016</a></td>
                    <td>
                    Put a leading zero before a decimal point for values of less than one
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/numbers.aspx">MEDi-017</a></td>
                    <td>
                    Use a comma to break up numeric values of one thousand and above
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/separators.aspx">MEDi-018</a></td>
                    <td>
                    When combining attributes in a text string, 
                    use a long dash (em dash) surrounded by spaces 
                    between the attributes
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="formatlayout/separators.aspx">MEDi-019</a></td>
                    <td>
                    Use a double space instead of a long dash or 
                    separator between a drug name and strength 
                    when there are multiple drug names in one medication line
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="formatlayout/separators.aspx">MEDi-020</a></td>
                    <td>
                    Use a double space instead of a long dash or 
                    separator between a drug name and strength when 
                    the strength is expressed as a percentage
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/wrapping.aspx">MEDi-021</a></td>
                    <td>
                    If a long drug name exceeds the available screen space 
                    and has to be wrapped, ensure that the drug name is wrapped 
                    between words
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/abbreviation.aspx">MEDi-022</a></td>
                    <td>
                    Do not abbreviate drug names
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="formatlayout/abbreviation.aspx">MEDi-023</a></td>
                    <td>
                    Use long form names rather than abbreviations where possible
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/abbreviation.aspx">MEDi-024</a></td>
                    <td>
                    Do not put a full stop after abbreviations 
                    for units (for example mg and mL)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/truncation.aspx">MEDi-025</a></td>
                    <td>
                    Do not truncate drug names
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/symbols.aspx">MEDi-026</a></td>
                    <td>
                    Do not use symbols that may be confused with numbers 
                    or otherwise misinterpreted, including &#64; &#124; &lt; &gt; 
                    &#47; &#92; &amp; &#176; (at sign, vertical bar, greater than bracket, 
                    less than bracket, forward slash, backslash, ampersand, degree)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/symbols.aspx">MEDi-027</a></td>
                    <td>
                    Use the '&#43;' (plus symbol) only for multiple drug name 
                    medications and surround it with spaces.  When a '&#43;' 
                    is displayed adjacent to a '4', separate the two with a 
                    double space
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/symbols.aspx">MEDi-028</a></td>
                    <td>
                    Use alternatives such as a dash or black dot (&#8226;) 
                    instead of brackets and separators such as &#40; &#41; &#91; &#93 &#123; &#125; 
                    that look like the number one
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/textlabels.aspx">MEDi-029</a></td>
                    <td>
                    When a medication is represented as a single-text sentence, 
                    use a label for dose only
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/textlabels.aspx">MEDi-030</a></td>
                    <td>
                    When a medication is represented as a series of lines with 
                    hard line breaks, labels should appear at the beginning of 
                    a new line after a hard line break
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/textlabels.aspx">MEDi-031</a></td>
                    <td>
                    Use a space to separate a label from a value
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/textlabels.aspx">MEDi-032</a></td>
                    <td>
                    Do not use a colon after a label
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="formatlayout/textlabels.aspx">MEDi-033</a></td>
                    <td>
                    Display labels in uppercase
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="formatlayout/textlabels.aspx">MEDi-034</a></td>
                    <td>
                    Keep the number of text labels in a medication represented 
                    as a <span class="nowrap">single-text</span> sentence to a minimum
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="formatlayout/linebreaks.aspx">MEDi-035</a></td>
                    <td>
                    When using hard line breaks at set points (such as before 
                    a dose), do not use a long dash at the end of a previous line
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="formatlayout/linespacing.aspx">MEDi-036</a></td>
                    <td>
                    When displaying a medication as one or many lines of text, 
                    preserve white space between the lines by ensuring that the 
                    line height is no less than 120&#37; (120&#37; leading) and 
                    no greater than 140&#37; (140&#37; leading)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="formatlayout/linespacing.aspx">MEDi-037</a></td>
                    <td>
                    When displaying a list of medications, ensure that there is a space 
                    equivalent to at least one line height of 100% between the last line 
                    of one medication line and the first line of the medication line below
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="attributes/dose.aspx">MEDi-038</a></td>
                    <td>
                    Display the dose amount and units in bold
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="attributes/dose.aspx">MEDi-039</a></td>
                    <td>
                    When a dose is expressed as a volume, display the volume amount in bold
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="attributes/dose.aspx">MEDi-040</a></td>
                    <td>
                    When there is no dose or volume, display a dose equivalent in place 
                    of the dose and subject to the same guidance points as a dose. Precede 
                    with an appropriate text label
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="attributes/dose.aspx">MEDi-041</a></td>
                    <td>
                    Separate the dose amount from the dose units with a space
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="attributes/dose.aspx">MEDi-042</a></td>
                    <td>
                    Do not put a trailing zero after a sub-decimal value when displaying 
                    a dose amount (that is, &#39;0.5&#39; is correct but &#39;0.50&#39; 
                    is incorrect)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="attributes/dose.aspx">MEDi-043</a></td>
                    <td>
                    Put a leading zero before a decimal point for 
                    values of less than one when displaying a dose value
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="attributes/dose.aspx">MEDi-044</a></td>
                    <td>
                    Use a comma to break up numeric values of one 
                    thousand and above when displaying a dose value
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="attributes/strength.aspx">MEDi-045</a></td>
                    <td>
                    When describing strengths with an active ingredient in a fluid, 
                    use &#39;in&#39; rather than a forward slash ( &#39;&#47;&#39; ) 
                    before the fluid quantity
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="attributes/strength.aspx">MEDi-046</a></td>
                    <td>
                    When describing strengths of an ingredient in a single unit of 
                    fluid, use the word &#39;per&#39; to describe the unit of fluid
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="attributes/strength.aspx">MEDi-047</a></td>
                    <td>
                    When describing a strength for a combination drug whose two strength 
                    values use the same unit (such as mg), use the word &#39;and&#39; 
                    in a smaller font to join the two strength values and display the 
                    units after the second strength value
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="attributes/strength.aspx">MEDi-048</a></td>
                    <td>
                    Do not put a trailing zero after a decimal point when displaying 
                    numbers in a strength value
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="attributes/strength.aspx">MEDi-049</a></td>
                    <td>
                    Put a leading zero before a decimal point for values of less 
                    than one when displaying numbers in a strength value
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="attributes/strength.aspx">MEDi-050</a></td>
                    <td>
                    Use a comma to break up numeric values of one thousand and 
                    above when displaying numbers in a strength value
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="attributes/order.aspx">MEDi-051</a></td>
                    <td>
                    When describing a medication as a line of text, adhere to the following 
                    order for the display of the medication attributes: drug name, brand 
                    name, strength, form, dose or volume, rate, dose duration, route, frequency 
                    (as applicable)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="attributes/order.aspx">MEDi-052</a></td>
                    <td>
                    When designing for specific contexts, especially those that need additional text 
                labels and line breaks, display drug name first and display other attributes (in 
                a different order if necessary) from the one defined above
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="attributes/order.aspx">MEDi-053</a></td>
                    <td>
                    When a medication is not displayed as a single line of text and the attributes 
                    of a medication are listed in a different order, use text labels for as many 
                    of these attributes as possible: strength, form, route and frequency
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="formatlayout/truncation.aspx">MEDi-054</a></td>
                    <td>
                    Do not display a part of the medication line alone if 
                    its meaning relies on other parts that are not displayed
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            </table>            
      </div>
    </div>
</asp:Content>