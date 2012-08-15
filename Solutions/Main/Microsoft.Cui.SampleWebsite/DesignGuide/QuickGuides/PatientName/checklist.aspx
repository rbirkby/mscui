<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="checklist.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.CheckList" %>

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
                	<td class="number"><a href="display/family.aspx">NID-0001</a></td>
                    <td>
                    The display must present the Family Name in all uppercase letters to clearly 
                    distinguish it from the Given Name
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/separators.aspx">NID-0002</a></td>
                    <td>
                    The display must separate the Family Name and Given Name using a comma to further establish 
                    that the Family Name is being placed first
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>            	
            	<tr>
                	<td class="number"><a href="display/title.aspx">NID-0003</a></td>
                    <td>
                    The display must include parentheses around the Title to separate and distinguish it 
                    from the other name elements
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/elements.aspx">NID-0004</a></td>
                    <td>The display must present the name elements strictly in the order shown</td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/elements.aspx">NID-0005</a></td>
                    <td>
                    The display must present all data for each specified element (Family Name, Given Name and 
                    Title) of the Patient Name in full. Avoid truncation of information where possible
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/separators.aspx">NID-0006</a></td>
                    <td>
                    The display must separate the presentation of Given Name and Title by a single space
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/title.aspx">NID-0007</a></td>
                    <td>
                    The display must present the Title element in title case, for example, Sir not 
                    SIR, Mr not MR 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/title.aspx">NID-0008</a></td>
                    <td>
                    The display must present a single pair of parentheses around the Title element, for example, (Mr) 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/title.aspx">NID-0009</a></td>
                    <td>
                    The display must allow any free-text (up to 35 characters) to be presented in 
                    the Title element 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/title.aspx">NID-0010</a></td>
                    <td>
                    The display must omit a trailing full stop from the 
                    Title element (for example, &#39;Mr&#39; 
                not &#39;Mr.&#39;)
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/format.aspx">NID-0011</a></td>
                    <td>
                    The display must allow the Family Name, Given Name 
                and Title elements to present at least the maximum 
                field sizes specified in this guidance
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/multiplecomponents.aspx">NID-0012</a></td>
                    <td>
                    The display must allow for the Family Name and Given Name elements to consist of multiple 
                    components. Components are constituent parts of the name element that combine with other 
                    parts to form the element as a whole. Components have the following features: Family Name 
                    components must consist of UPPERCASE alphabetic characters only, for example, SMITH; Multiple 
                    Family Name components must be separated by a hyphen or a single space, for example, 
                    LIDMAN-SUN-DEWAR or EVANS WEST; Given Name components must display in title case, for example, 
                    Nadejda; Multiple Given Name components must be separated by a hyphen or a single space, for 
                    example, Anne-Jorun, Nis Bank
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="display/format.aspx">NID-0013</a></td>
                    <td>
                    The display should allow word wrapping to occur in instances where the field length exceeds 
                    the width allocated to it on the form. If word wrapping occurs, it should be applied only at 
                    the end of a whole field element or at the end of a field element component, if it comprises 
                    multiple parts (for example, Middle name(s) field) 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/title.aspx">NID-0014</a></td>
                    <td>
                    Input control must allow a maximum of 35 characters
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/title.aspx">NID-0015</a></td>
                    <td>
                    Minimum visual width of the input box must display four characters
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/title.aspx">NID-0016</a></td>
                    <td>
                    Suggested values are: &#39;Mr, &#39;Mrs&#39;, &#39;Ms&#39;, 
                    &#39;Dr&#39;, &#39;Rev&#39;, &#39;Sir&#39;, &#39;Lady&#39;, 
                    &#39;Lord&#39;, &#39;Dame&#39;, &#39;Other...&#39; 
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/title.aspx">NID-0017</a></td>
                    <td>
                    One value should allow the user to invoke free-text input mode (for example &#39;Other...&#39; 
                    in the illustrations)
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/title.aspx">NID-0018</a></td>
                    <td>
                    Input box should contain a relevant prompt, for example, Mr
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/title.aspx">NID-0019</a></td>
                    <td>
                    Input control should be in the form of a drop-down combo-box
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/family.aspx">NID-0020</a></td>
                    <td>
                    Family Name input must be via a free-text entry box
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/family.aspx">NID-0021</a></td>
                    <td>
                    Family Name input box must accept a maximum of 35 characters
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/family.aspx">NID-0022</a></td>
                    <td>
                    Family Name input box should be capable of displaying a minimum of eight characters 
                    without occlusion
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/family.aspx">NID-0023</a></td>
                    <td>
                    Family Name input box should optimally display 14 characters without occlusion
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/family.aspx">NID-0024</a></td>
                    <td>
                    Family Name input box should contain a relevant prompt in its default state (for example, 
                    &#39;e.g. SMITH&#39;) in occluded form
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/family.aspx">NID-0025</a></td>
                    <td>
                    When displaying a Family Name value, the characters should all be in uppercase
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/given.aspx">NID-0026</a></td>
                    <td>
                    Given Name input must be via a <span class="nowrap">free-text</span> entry box
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/given.aspx">NID-0027</a></td>
                    <td>
                    Given Name input box must accept a maximum of 35 characters 
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/given.aspx">NID-0028</a></td>
                    <td>
                    Given Name input box should be capable of displaying a minimum of eight characters 
                    without occlusion
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/given.aspx">NID-0029</a></td>
                    <td>
                    Given Name input box should optimally display 14 characters without occlusion
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/given.aspx">NID-0030</a></td>
                    <td>
                    Given Name input box should contain a relevant prompt in its default state (for example, 
                    &#39;e.g. John&#39;) in occluded form
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/given.aspx">NID-0031</a></td>
                    <td>
                    When displaying a Given Name value the first character should be in uppercase
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/middle.aspx">NID-0032</a></td>
                    <td>
                    Middle name input must be via a free-text entry box
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/middle.aspx">NID-0033</a></td>
                    <td>
                    Middle name input box must accept a maximum of 100 characters
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/middle.aspx">NID-0034</a></td>
                    <td>
                    Middle name input box should be capable of displaying a minimum of eight 
                    characters without occlusion
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/middle.aspx">NID-0035</a></td>
                    <td>
                    Middle name input box should optimally display 7 characters without occlusion
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/middle.aspx">NID-0036</a></td>
                    <td>
                    Middle name input box should contain a relevant prompt in its default state (for example, 
                    &#39;e.g. David James&#39;) in occluded form
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/suffix.aspx">NID-0037</a></td>
                    <td>
                    Suffix input must be via a free-text entry box
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/suffix.aspx">NID-0038</a></td>
                    <td>
                    Suffix input must accept a maximum of 35 characters
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/suffix.aspx">NID-0039</a></td>
                    <td>
                    Suffix input box should be capable of displaying a minimum of eight characters without occlusion
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/suffix.aspx">NID-0040</a></td>
                    <td>
                    Suffix input box should optimally display 14 characters without occlusion
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/suffix.aspx">NID-0041</a></td>
                    <td>
                    Suffix input box should contain a relevant prompt when in its default state (for example, 
                    &#39;e.g. Junior&#39;) in occluded form
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/knownas.aspx">NID-0042</a></td>
                    <td>
                    Preferred name input must be via a free-text entry box
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/knownas.aspx">NID-0043</a></td>
                    <td>
                    Preferred name input box must accept a maximum of 35 characters
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/knownas.aspx">NID-0044</a></td>
                    <td>
                    Preferred name input box should be capable of displaying a minimum of eight characters 
                    without occlusion
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/knownas.aspx">NID-0045</a></td>
                    <td>
                    Preferred name input box should optimally display 14 characters without occlusion
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/knownas.aspx">NID-0046</a></td>
                    <td>
                    Preferred name input box should contain a relevant prompt in its default state (for 
                    example, &#39;e.g. Johnny-Boy&#39;) in occluded form
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/layout/inform.aspx">NID-0047</a></td>
                    <td>
                    In-form field controls must be aligned on the left edge of the input boxes
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/layout/inform.aspx">NID-0048</a></td>
                    <td>
                    In-form field controls (where they exist) must be placed underneath each other in the 
                    following order: Title, Family Name, Given Name, Middle name(s), Suffix, Known as
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/layout/inline.aspx">NID-0049</a></td>
                    <td>Ensure wrapping only occurs on whole fields</td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/layout/inline.aspx">NID-0050</a></td>
                    <td>Correct presentation order is: Title, Family Name, Given Name, Middle Name(s), 
                    Suffix, Known as</td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/layout/inline.aspx">NID-0051</a></td>
                    <td>In-line design choice should only be used when 
                    <span class="nowrap">in-form</span> has been considered undesirable</td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/instructions/fieldlabels.aspx">NID-0052</a></td>
                    <td>Each field in a name input control must have an associated label</td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/instructions/fieldlabels.aspx">NID-0053</a></td>
                    <td>Labels must be programmatically linked to their associated input field</td>
                    <td class="rating">Mandatory</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/instructions/fieldlabels.aspx">NID-0054</a></td>
                    <td>Label values should be: Title:&#39;Title&#39;, Family Name:&#39;Family Name&#39;, 
                    Given Name: &#39;Given Name&#39;, Middle name: &#39;Middle name(s)&#39;, Suffix: 
                    &#39;Suffix&#39;, Preferred name: &#39;Known as&#39;</td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/instructions/prompts.aspx">NID-0055</a></td>
                    <td>Each field in a name input control must have an associated prompt</td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/instructions/prompts.aspx">NID-0056</a></td>
                    <td>Prompts for Family Name should be capitalized</td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/instructions/prompts.aspx">NID-0057</a></td>
                    <td>All prompts except Family Name should have sentence style capitalization</td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/instructions/prompts.aspx">NID-0058</a></td>
                    <td>Prompt values should be: Title: &#39;e.g. Mr&#39;, Family Name: &#39;e.g. SMITH&#39;, 
                    Given Name: &#39;e.g. John&#39;, Middle name(s): &#39;e.g. David James&#39;, Suffix: 
                    &#39;e.g. Junior&#39;, Known as: &#39;e.g. Johnny-Boy&#39;</td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/instructions/prompts.aspx">NID-0059</a></td>
                    <td>Prompts should be lighter in weight and colour than the input text, and italicized</td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/instructions/tooltips.aspx">NID-0060</a></td>
                    <td>Each field in a name input control should have instructional text (for 
                    example, a tooltip)</td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/instructions/tooltips.aspx">NID-0061</a></td>
                    <td>Tooltip values should be: Title:&#39;Select a Title from the list or simply 
                    type in a different Title&#39;, Family Name: &#39;Enter the person&#39;s Family 
                    Name (surname)&#39;, Given Name: &#39;Enter the person&#39;s Given Name (forename or 
                    Christian name)&#39;, Middle name(s): &#39;Enter the person&#39;s middle name(s)&#39;, 
                    Suffix: &#39;Enter the person&#39;s suffix name (e.g. &#39;Junior&#39; or &#39;The 
                    Third&#39;)&#39;, Known as: &#39;Enter the name a person likes to be referred to as&#39;
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/defaults.aspx">NID-0062</a></td>
                    <td>By default, include a prompt in the input boxes to 
                indicate to a user the information required</td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr class="recommended">
                	<td class="number"><a href="input/defaults.aspx">NID-0063</a></td>
                    <td>Present the default prompt in an occluded form to prevent 
                confusion with actual data input by a user</td>
                    <td class="rating">Recommended</td>
                </tr>
            	<tr>
                	<td class="number"><a href="input/defaults.aspx">NID-0064</a></td>
                    <td>Remove the default prompt when a user begins to input data</td>
                    <td class="rating">Mandatory</td>
                </tr>
            </table>
            
      </div>
    </div>
</asp:Content>
