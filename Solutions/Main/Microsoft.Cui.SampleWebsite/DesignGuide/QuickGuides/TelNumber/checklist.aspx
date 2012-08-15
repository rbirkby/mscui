<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="checklist.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.TelNumber.CheckList" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            The following table lists all of the guidance points in order. 
            Click on a guidance ID to find it in the guide.
            </p>
            <table class="checklist">
                	<th>ID</th>
                    <th>Guideline</th>
                    <th>Compliance</th>
                </tr>
            	<tr>
                	<td class="number"><a href="display/countrycode.aspx">TID-0001</a></td>
                    <td>
                    If the country code is for the UK, 
                    for example, &#39;+44&#39; or &#39;0044&#39;, 
                    then it must not be displayed
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/countrycode.aspx">TID-0002</a></td>
                    <td>
                    When displayed, the country code must always be displayed with 
                    a &#39;+&#39; sign in front of it
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/countrycode.aspx">TID-0003</a></td>
                    <td>
                    When displayed, the country code must not display any leading zeros
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/areacode.aspx">TID-0005</a></td>
                    <td>
                    For UK telephone numbers, the area code must not be displayed 
                    with brackets around it
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/areacode.aspx">TID-0006</a></td>
                    <td>
                    For UK telephone numbers, the area code must be separated from 
                    subsequent numbers by a space
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="display/extension.aspx">TID-0007</a></td>
                    <td>
                    For UK telephone numbers, extension numbers can be displayed 
                    with an 'x' preceding and adjacent to the number
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/extension.aspx">TID-0008</a></td>
                    <td>
                    For UK telephone numbers where the telephone and extension numbers 
                    are displayed within a single input box, the extension number must 
                    be separated from the rest of the telephone number by a single space 
                    that precedes the &#39;x&#39;
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/formatting.aspx">TID-0009</a></td>
                    <td>
                    For UK telephone numbers, if there are more than six digits in the local 
                    number, (in other words, not the country code, area code or extension 
                    number), then a space must be inserted before the final four digits
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/inputboxes.aspx">TID-0011</a></td>
                    <td>
                    Use a free-text input box for the entry of telephone number
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/entryformat.aspx">TID-0012</a></td>
                    <td>
                    Ensure the input box accepts formatted and unformatted entries
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/validation.aspx">TID-0013</a></td>
                    <td>
                    If the number can be identified as a valid type, the input box should strip out 
                    formatting upon losing focus and replace it with a reformatted equivalent
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/redisplay.aspx">TID-0014</a></td>
                    <td>
                    Display a reformatted entry to the user which places spaces in 
                    logical locations for readability
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/validation.aspx">TID-0015</a></td>
                    <td>
                    If the number cannot be identified as a valid type, display 
                    the entry to the user as it was entered
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/redisplay.aspx">TID-0016</a></td>
                    <td>
                    Remove the UK country code from display after it is committed
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/redisplay.aspx">TID-0017</a></td>
                    <td>
                    Retain all other country codes
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/redisplay.aspx">TID-0018</a></td>
                    <td>
                    Do not display UK numbers with the international prefix
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/redisplay.aspx">TID-0019</a></td>
                    <td>
                    Display non-UK numbers with a + prefixed to the country code
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="display/extension.aspx">TID-0026</a></td>
                    <td>
                    For UK telephone numbers where the extension number is displayed in 
                    a separate input box, a label must be shown above the input box to 
                    indicate the content
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/inputboxes.aspx">TID-0027</a></td>
                    <td>
                    Use a free-text input box where extension number is input into a separate input box
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            </table>
            
      </div>
    </div>
</asp:Content>