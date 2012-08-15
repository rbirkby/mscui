<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="checklist.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.GenderSex.CheckList" %>

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
                	<td class="number"><a href="input/gender/labeldefault.aspx">CGS-0001</a></td>
                    <td>Label the Current Gender input controls &#39;Current Gender&#39;</td>
                  <td class="rating">Mandatory</td>
                </tr>
                <tr>
                	<td class="number"><a href="input/gender/datavalues.aspx">CGS-0002</a></td>
                	<td>The Current Gender values are: Male, Female, Other Specific, 
                	Not Known, Not Specified</td>
                    <td class="rating">Mandatory</td>
                </tr>
                <tr>
                	<td class="number"><a href="display/statusvalues.aspx">CGS-0003</a></td>
                	<td>
                    The Current Gender status is one of the following values: Male, Female, 
                    Other Specific, Not Known, Not Specified
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
                <tr>
                	<td class="number"><a href="input/gender/definitions.aspx">CGS-0004</a></td>
                	<td>
                    Make the definitions of the Current Gender status values accessible to the user
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
                <tr>
                	<td class="number"><a href="input/gender/datavalues.aspx">CGS-0005</a></td>
                	<td>
                    Do not abbreviate Current Gender data values
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
                <tr>
                	<td class="number"><a href="input/gender/datavalues.aspx">CGS-0006</a></td>
                	<td>
                    Do not display the underlying coded representation of the Current Gender 
                    data values. For example, the standard code for &#39;Male&#39; may be the integer 
                    1, but this number should not appear
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
                <tr class="recommended">
                  <td class="number"><a href="display/labels.aspx">CGS-0007</a></td>
                  <td>Label the Current Gender status display &#39;Current Gender&#39;</td>
                  <td class="rating">Recommended</td>
                </tr>
                <tr class="recommended">
                  <td class="number"><a href="input/gender/labeldefault.aspx">CGS-0008</a></td>
                  <td>Use &#39;Not Known&#39; as the Current Gender default value</td>
                  <td class="rating">Recommended</td>
                </tr>
              <tr>
                <td class="number"><a href="input/sex/labeldefault.aspx">CGS-0009</a></td>
                <td>Label the Sex input controls &#39;Sex&#39; </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="input/sex/datavalues.aspx">CGS-0010</a></td>
                <td>
                The Sex values are: Male, Female, Not Known, Indeterminate
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="display/statusvalues.aspx">CGS-0011</a></td>
                <td>The Sex status must only contain one of the following values: 
                Male, Female, Not Known, Indeterminate
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="input/sex/datavalues.aspx">CGS-0012</a></td>
                <td>Sex data values must never be abbreviated</td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="input/sex/labeldefault.aspx">CGS-0013</a></td>
                <td>The Sex default state is null</td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="input/sex/datavalues.aspx">CGS-0014</a></td>
                <td>
                The application must not display the underlying coded representation of the Sex 
                data values. For example, the standard code for &#39;Male&#39; may be the integer 1, 
                but this number should not appear
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="input/sex/definitions.aspx">CGS-0015</a></td>
                <td>
                Make the definitions of the Sex status values accessible to the user
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr class="recommended">
                <td class="number"><a href="display/labels.aspx">CGS-0016</a></td>
                <td>Label the Sex status display &#39;Sex&#39;</td>
                <td class="rating">Recommended</td>
              </tr>
              <tr>
                <td class="number"><a href="input/inputdefinitions.aspx">CGS-0017</a></td>
                <td>Provide definitions for input controls </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="input/inputdefinitions.aspx">CGS-0018</a></td>
                <td>Provide access to definitions of the valid values</td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr class="recommended">
                <td class="number"><a href="input/inputdefinitions.aspx">CGS-0019</a></td>
                <td>Provide a shortened version of the definitions</td>
                <td class="rating">Recommended</td>
              </tr>
              <tr>
                <td class="number"><a href="input/gender/optionbuttons.aspx">CGS-0020</a></td>
                <td>
                Current Gender option button group input controls must consist of five option buttons
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="input/gender/optionbuttons.aspx">CGS-0021</a></td>
                <td>Current Gender option button group labels are in the following order: Male, 
                Female, Other Specific, Not Known, Not Specified
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr class="recommended">
                <td class="number"><a href="input/gender/optionbuttons.aspx">CGS-0022</a></td>
                <td>The Current Gender tab order is: Male, Female, Other Specific, Not Known, 
                Not Specified</td>
                <td class="rating">Recommended</td>
              </tr>
              <tr>
                <td class="number"><a href="input/sex/optionbuttons.aspx">CGS-0023</a></td>
                <td>
                Sex option button group input controls must consist of four option buttons
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr class="recommended">
                <td class="number"><a href="input/sex/optionbuttons.aspx">CGS-0024</a></td>
                <td>
                Sex option button group labels are in the following order (top to bottom first 
                followed by left to right): Male, Female, Not Known, Indeterminate
                </td>
                <td class="rating">Recommended</td>
              </tr>
              <tr>
                <td class="number"><a href="input/gender/dropdown.aspx">CGS-0025</a></td>
                <td>
                Current Gender drop-down list box options are in the following order: Male, 
                Female, Other Specific, Not Known, Not Specified
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr class="mandatory">
                <td class="number"><a href="input/gender/dropdown.aspx">CGS-0026</a></td>
                <td>
                Use a single drop-down list box for the Current Gender control
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="input/gender/dropdown.aspx">CGS-0027</a></td>
                <td>
                Do not use a prompt for the Current Gender control, due to its default 
                value of &#39;Not Known&#39;
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="input/sex/dropdown.aspx">CGS-0028</a></td>
                <td>
                Ensure that Sex controls have no value selected by default and no method 
                of returning to this &#39;null&#39; state
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="input/sex/dropdown.aspx">CGS-0029</a></td>
                <td>
                Use a single control for the Sex drop-down list box
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr class="recommended">
                <td class="number"><a href="input/sex/dropdown.aspx">CGS-0030</a></td>
                <td>
                Ensure that the Sex drop-down list box is blank by default and does not contain a prompt
                </td>
                <td class="rating">Recommended</td>
              </tr>
            </table>
            
      </div>
    </div>
</asp:Content>