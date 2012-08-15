<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="checklist.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientIDNumber.CheckList" %>

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
                	<td class="number"><a href="display/format.aspx">NUM-0001</a></td>
                    <td>
                    Display the patient identification number in full, on a single line, 
                    without truncation or splitting it over multiple lines
                    </td>
                  <td class="rating">Mandatory</td>
                </tr>
                <tr>
                	<td class="number"><a href="display/format.aspx">NUM-0002</a></td>
                	<td>
                    Display the NHS number as three groups, with a 
                    single space included as a separator between groups, as follows: 
                    the first group must consist of the first, second and third digits 
                    in order; the second group must consist of the fourth, fifth and 
                    sixth digits in order; the third group must consist of the seventh, 
                    eighth, ninth and tenth digits in order
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
                <tr class="recommended">
                	<td class="number"><a href="display/format.aspx">NUM-0003</a></td>
                	<td>
                    Support the copying of patient identification numbers by the user as 
                    part of the &#39;Copy and Paste&#39; task
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
                <tr class="recommended">
                	<td class="number"><a href="input/inputbox.aspx">NUM-0010</a></td>
                	<td>
                    Provide a single text input box for patient identification number entry
                    </td>
                    <td class="rating">Recommended</td>
                </tr>
                <tr>
                	<td class="number"><a href="input/inputbox.aspx">NUM-0011</a></td>
                	<td>
                    Permit only one patient identification number to be entered in a 
                    patient identification number input box
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
                <tr class="recommended">
                  <td class="number"><a href="input/dimensions.aspx">NUM-0012</a></td>
                  <td>
                  Set the length of the patient identification number input box such that 
                  the patient identification number is visible in full
                  </td>
                  <td class="rating">Recommended</td>
                </tr>
                <tr class="recommended">
                  <td class="number"><a href="input/dimensions.aspx">NUM-0013</a></td>
                  <td>
                  Set the height of the patient identification number input box to the largest 
                  character height in the currently active display font, taking the user&#39;s 
                  settings into account 
                  </td>
                  <td class="rating">Recommended</td>
                </tr>
              <tr class="recommended">
                <td class="number"><a href="input/inputmethods.aspx">NUM-0014</a></td>
                <td>
                Permit patient identification number input via all the mechanisms supported on 
                a platform such as, but not limited to, typing on a keyboard, copy and paste, 
                and handwriting with a stylus
                </td>
                <td class="rating">Recommended</td>
              </tr>
              <tr>
                <td class="number"><a href="input/inputbox.aspx">NUM-0018</a></td>
                <td>
                Do not permit input of old format and temporary patient identification numbers
                </td>
                <td class="rating">Mandatory</td>
              </tr>
            </table>
            
      </div>
    </div>
</asp:Content>
