<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="checklist.aspx.cs" MasterPageFile="~/QIGs.Master" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.CheckList" %>

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
                	<td class="number"><a href="display/inline.aspx">ADR-0001</a></td>
                    <td>
                    When displaying an address horizontally, only use a single comma and a single space, 
                    in that order, to delimit the different fields
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="display/inform.aspx">ADR-0002</a></td>
                    <td>
                    When displaying an address vertically, do not use a comma at the end of a line
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="display/inform.aspx">ADR-0003</a></td>
                    <td>
                    When displaying an address vertically, 
                    left-align the text for ease of reading
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="display/inline.aspx">ADR-0004</a></td>
                    <td>
                    When truncating an address, add an ellipsis to indicate that the address is 
                    not displayed in full and, where appropriate, provide a means for the user 
                    to access the full address
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="display/inform.aspx">ADR-0005</a></td>
                    <td>
                    Do not split an address element when wrapping an address across multiple lines
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="display/general.aspx">ADR-0006</a></td>
                    <td>
                    Where part of an address is not available, do not display an empty string in its place
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/general.aspx">ADR-0007</a></td>
                    <td>
                    Display the postcode in all caps with a space between the first part (the outcode) 
                    and the second part (the incode)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="display/general.aspx">ADR-0008</a></td>
                    <td>
                    Do not display labels for individual address elements
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/ukinput/inputboxes.aspx">ADR-0011</a></td>
                    <td>
                    Provide the following text input boxes, in the stated order, for UK address input: 
                    three boxes for input for all details up to and including the street name, 
                    one box for input of the town or city, one box for the input of the county, 
                    one box for input of the postcode
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/ukinput/labels.aspx">ADR-0012</a></td>
                    <td>
                    Where text input boxes are used, they must be labelled as follows: 
                    the three boxes for input of all details up to and including the street name, 
                    must be labelled &#39;Line 1&#39;, &#39;Line 2&#39; and &#39;Line 3&#39; 
                    respectively; the box for input of the town or city should be labelled 
                    &#39;Town/City&#39;; the box for input of the county should be labelled 
                    &#39;County&#39;; the box for input of the postcode should be labelled 
                    &#39;Postcode&#39;.
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/ukinput/postcode.aspx">ADR-0013</a></td>
                    <td>
                    Provide a means to find a postcode, to enhance data quality
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/ukinput/postcode.aspx">ADR-0014</a></td>
                    <td>
                    Display a means to find a postcode only if such a service is supported, 
                    positioning it after the postcode input box and labelling it &#39;Find Postcode&#39;
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/ukinput/dimensions.aspx">ADR-0015</a></td>
                    <td>
                    Set the length of the postcode input box to 8 characters
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/ukinput/dimensions.aspx">ADR-0016</a></td>
                    <td>
                    Set the length of the &#39;County&#39; input box to 18 characters
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/ukinput/dimensions.aspx">ADR-0017</a></td>
                    <td>
                    Set the height of each text input box to the largest character height in the 
                    currently active display font, taking the user&#39;s settings into account
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/ukinput/layout.aspx">ADR-0018</a></td>
                    <td>
                    Display the text input boxes vertically with left alignment
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/ukinput/layout.aspx">ADR-0019</a></td>
                    <td>
                    Display the labels immediately to the left of their corresponding text input box, 
                    mutually right-aligning the labels
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/ukinput/inputmethods.aspx">ADR-0020</a></td>
                    <td>
                    Permit address input via all the mechanisms supported on a platform such as, but 
                    not limited to, typing on a keyboard, copy and paste, and handwriting with a stylus
                    </td>
                    <td class="rating">Recommended</td>
            	<tr class="recommended">
                	<td class="number"><a href="input/ukinput/datavalues.aspx">ADR-0021</a></td>
                    <td>
                    Permit the following characters in the address: uppercase and lowercase letters, 
                    numbers 0 to 9, the full stop, forward slash, comma, colon, apostrophe, space and 
                    the hyphen
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/finder/inputboxes.aspx">ADR-0031</a></td>
                    <td>
                    Provide the following text input boxes, in the stated order, 
                    for input of a UK address: one box for input of house or building number, 
                    one box for input of house or building name, one box for input of the postcode
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/finder/labels.aspx">ADR-0032</a></td>
                    <td>
                    Where text input boxes are used, they must be labelled as follows: 
                    the box for input of house or building number should be labelled 
                    &#39;House/Building Number&#39;, the box for input of house or building name should be 
                    labelled &#39;House/Building Name&#39;, the box for input of the postcode should 
                    be labelled &#39;Postcode&#39;
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/finder/findaddress.aspx">ADR-0033</a></td>
                    <td>
                    Display a means to find an address only if such a service is supported, 
                    positioning it after the postcode input box and labelling it &#39;Find Address&#39;
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/finder/dimensions.aspx">ADR-0034</a></td>
                    <td>
                    Set the length of the postcode input box to 8 characters
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/finder/dimensions.aspx">ADR-0035</a></td>
                    <td>
                    Set the height of each text input box to the largest character height in the 
                    currently active display font, taking the user's settings into account
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/finder/layout.aspx">ADR-0036</a></td>
                    <td>
                    Display the text input boxes vertically with left alignment
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/finder/layout.aspx">ADR-0037</a></td>
                    <td>
                    Display the labels immediately to the left of their corresponding text input box, 
                    mutually right-aligning the labels
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/finder/inputmethods.aspx">ADR-0038</a></td>
                    <td>
                    Permit address input via all the mechanisms supported on a platform such as, 
                    but not limited to, typing on a keyboard, copy and paste, and handwriting 
                    with a stylus
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/nonukinput/inputboxes.aspx">ADR-0050</a></td>
                    <td>
                    Provide the following boxes, in the stated order, for input of a non-UK address: 
                    one editable combo box for country selection, four boxes for input of all details 
                    up to and including the street name, one box for input of the town or city, one 
                    box for input of the postal code
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/nonukinput/labels.aspx">ADR-0051</a></td>
                    <td>
                    Where used, the boxes must be labelled as follows: the editable combo box for 
                    country selection should be labelled &#39;Country&#39;; 
                    the four boxes for input of all details up to and including the street name, 
                    should be labelled &#39;Line 1&#39;, &#39;Line 2&#39;, &#39;Line 3&#39; and 
                    &#39;Line 4&#39; respectively; 
                    the box for input of the town or city should be labelled &#39;Town/City&#39;; 
                    the box for input of the postal code should be labelled &#39;Postal Code&#39;
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/nonukinput/dimensions.aspx">ADR-0052</a></td>
                    <td>
                    Set the height of each text input box to the largest character height in the 
                    currently active display font, taking the user&#39;s settings into account
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/nonukinput/layout.aspx">ADR-0053</a></td>
                    <td>
                    Display the input boxes vertically with left alignment
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/nonukinput/layout.aspx">ADR-0054</a></td>
                    <td>
                    Display the labels immediately to the left of their corresponding text input box, 
                    mutually right-aligning the labels
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/nonukinput/inputmethods.aspx">ADR-0055</a></td>
                    <td>
                    Permit address input via all the mechanisms supported on a platform such as, 
                    but not limited to, typing on a keyboard, copy and paste, and handwriting with 
                    a stylus
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/nonukinput/country.aspx">ADR-0056</a></td>
                    <td>
                    Use an editable drop-down combo box for country names
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/nonukinput/country.aspx">ADR-0057</a></td>
                    <td>
                    Use the list of country names in ISO 3166-1 for the country selector drop-down combo box
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/nonukinput/country.aspx">ADR-0058</a></td>
                    <td>
                    Display the country names in alphabetic order
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/nonukinput/country.aspx">ADR-0059</a></td>
                    <td>
                    Display the country names with left alignment
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            </table>
            
      </div>
    </div>
</asp:Content>