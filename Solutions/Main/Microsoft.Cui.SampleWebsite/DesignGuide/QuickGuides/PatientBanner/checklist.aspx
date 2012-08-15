<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="checklist.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.CheckList" %>

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
            	<tr class="recommended">
                	<td class="number"><a href="structure/zones.aspx">PAB-0001</a></td>
                    <td>
                    The Patient Banner should consist of two zones, Zone 1 and Zone 2</td>
                  <td class="rating">Recommended</td>
                </tr>
                <tr>
                	<td class="number"><a href="structure/contents.aspx">PAB-0002</a></td>
                	<td>
                    Display information that facilitates patient identification in Zone 1</td>
                    <td class="rating">Mandatory</td>
                </tr>
                <tr>
                	<td class="number"><a href="structure/contents.aspx">PAB-0003</a></td>
                	<td>
                    Display supplementary information that either supports patient 
                    identification or assists patient care in Zone 2</td>
                    <td class="rating">Mandatory</td>
                </tr>
                <tr>
                	<td class="number"><a href="structure/expandcontrol.aspx">PAB-0004</a></td>
                	<td>
                    Where Zone 2 is used, in the default display of the Patient Banner, 
                    show Zone 1 and Zone 2, with Zone 2 in the collapsed state
                    </td>
                    <td class="rating">Mandatory</td>
                </tr>
                <tr>
                	<td class="number"><a href="structure/zones.aspx">PAB-0005</a></td>
                	<td>
                    Zone 2 consists of 5 sections</td>
                    <td class="rating">Mandatory</td>
                </tr>
                <tr>
                	<td class="number"><a href="structure/expandcontrol.aspx">PAB-0006</a></td>
                	<td>
                    All five sections in Zone 2 expand and collapse together</td>
                    <td class="rating">Mandatory</td>
                </tr>
                <tr>
                  <td class="number"><a href="structure/expandcontrol.aspx">PAB-0007</a></td>
                  <td>Display a tooltip when the mouse is positioned over Zone 2 while Zone 2 is 
                  collapsed, stating that Zone 2 can be expanded</td>
                  <td class="rating">Mandatory</td>
                </tr>
                <tr>
                  <td class="number"><a href="structure/contents.aspx">PAB-0008</a></td>
                  <td>The Patient Banner adheres to role-based access control, for example, do not 
                  display clinical information such as allergy propensities, to non-clinical users</td>
                  <td class="rating">Mandatory</td>
                </tr>
              <tr>
                <td class="number"><a href="structure/layout.aspx">PAB-0009</a></td>
                <td>Display the Patient Banner at the top of the application window</td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="structure/layout.aspx">PAB-0010</a></td>
                <td>Display the Patient Banner across the width of the screen rather than vertically</td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="structure/layout.aspx">PAB-0011</a></td>
                <td>Display the Patient Banner in a fixed position, unmovable by the user</td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="structure/layout.aspx">PAB-0012</a></td>
                <td>Display the Patient Banner so that it occupies the full width of the application window</td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr class="recommended">
                <td class="number"><a href="structure/context.aspx">PAB-0013</a></td>
                <td>Do not obscure the Patient Banner with other elements of the screen</td>
                <td class="rating">Recommended</td>
              </tr>
              <tr>
                <td class="number"><a href="structure/visualstyle.aspx">PAB-0014</a></td>
                <td>
                Apply visual styling such as a thick border or distinguishing background 
                colour, to the Patient Banner in contrast to other elements of the application&#39;s 
                user interface</td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr class="recommended">
                <td class="number"><a href="structure/context.aspx">PAB-0015</a></td>
                <td>
                Do not display the Patient Banner on screens that contain information 
                relating to more than one patient
                </td>
                <td class="rating">Recommended</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/contents/order.aspx">PAB-0016</a></td>
                <td>
                Always display the patient&#39;s name (family name, given name and title), date of birth, 
                gender and patient identification number in this order within the Patient Banner
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/contents.aspx">PAB-0017</a></td>
                <td>
                For a patient who is alive, the Patient Banner additionally displays contact 
                details (comprising the address and phone numbers) and the patient&#39;s age
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/contents.aspx">PAB-0018</a></td>
                <td>
                For a deceased patient, the Patient Banner additionally displays the last known 
                contact details (comprising the address and phone numbers), date of death and age at death
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="structure/datalabelformats.aspx">PAB-0019</a></td>
                <td>
                If an individual data item is not known, or is otherwise unavailable, a blank string 
                or appropriate self-explanatory text (such as &#39;Not Known&#39;, but not a 
                &#39;?&#39;) is to be displayed 
                immediately after the corresponding data label
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr class="recommended">
                <td class="number"><a href="zone1/contents/preferredname.aspx">PAB-0020</a></td>
                <td>Display the preferred name if available</td>
                <td class="rating">Recommended</td>
              </tr>
              <tr class="recommended">
                <td class="number"><a href="structure/contents.aspx">PAB-0021</a></td>
                <td>Do not display the patient&#39;s photograph in the Patient Banner</td>
                <td class="rating">Recommended</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/contents.aspx">PAB-0022</a></td>
                <td>
                Display the elements of the patient name, date of birth, gender and patient 
                identification number in Zone 1
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/contents/age.aspx">PAB-0023</a></td>
                <td>
                Display the age of a living patient in Zone 1
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/contents/age.aspx">PAB-0024</a></td>
                <td>
                For a deceased patient, display the date of death and the age at death in Zone 1
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/collapsed.aspx">PAB-0025</a></td>
                <td>
                Display as much of the address as possible in a single line, in the title of 
                the first section in Zone 2, displaying an ellipsis to show incomplete display of the address
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr class="mandatory">
                <td class="number"><a href="zone2/expanded.aspx">PAB-0026</a></td>
                <td>
                Display the full address including the postcode, in the first section of the expanded Zone 2
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/labels.aspx">PAB-0027</a></td>
                <td>
                Precede the full address with the label &#39;Usual address&#39; 
                or &#39;Temporary address&#39; as appropriate
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/collapsed.aspx">PAB-0028</a></td>
                <td>
                Display as much of a single phone number as possible in a single 
                line, in the title of the second section on Zone 2, displaying an 
                ellipsis to show incomplete display of the phone number
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/expanded.aspx">PAB-0029</a></td>
                <td>
                Display contact numbers and email addresses in the second section of the 
                expanded Zone 2, in the following order: Home, Work, Mobile, Email
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/labels.aspx">PAB-0030</a></td>
                <td>
                Precede each contact number and email address with the label &#39;Home&#39;, 
                &#39;Work&#39;, &#39;Mobile&#39;, or &#39;Email&#39;, as appropriate
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr class="recommended">
                <td class="number"><a href="zone2/collapsed/allergies.aspx">PAB-0031</a></td>
                <td>Optionally, display allergy propensity information in Zone 2 of the Patient Banner</td>
                <td class="rating">Recommended</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/collapsed/allergies.aspx">PAB-0032</a></td>
                <td>
                Reserve the fifth section of Zone 2 for the display of optional allergy propensity information
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/labels.aspx">PAB-0033</a></td>
                <td>
                Precede the date of birth with the label &#39;Born&#39;
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/contents/age.aspx">PAB-0034</a></td>
                <td>
                When displaying the age of a living patient, place it in parentheses 
                immediately following the date of birth, and without a label
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/labels.aspx">PAB-0035</a></td>
                <td>
                Precede the gender with the label &#39;Gender&#39;
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/labels.aspx">PAB-0036</a></td>
                <td>
                Precede the patient identification number with an appropriate label, 
                for example &#39;NHS No.&#39;
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr class="recommended">
                <td class="number"><a href="zone1/labels.aspx">PAB-0037</a></td>
                <td>
                Precede the preferred name with the label &#39;Preferred name&#39;
                </td>
                <td class="rating">Recommended</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/labels/deceased.aspx">PAB-0038</a></td>
                <td>
                Precede the date of death with the label &#39;Died&#39;
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/labels/deceased.aspx">PAB-0039</a></td>
                <td>
                Precede the age at death with the label &#39;Age at Death&#39;
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/labels.aspx">PAB-0040</a></td>
                <td>
                Precede the address displayed in the title of the first section in 
                Zone 2, with the label &#39;Address&#39;
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/labels.aspx">PAB-0041</a></td>
                <td>
                Precede the single phone number displayed in the title of the second 
                section in Zone 2, with the label &#39;Phone and email&#39;
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="structure/datalabelformats.aspx">PAB-0042</a></td>
                <td>Do not add a colon after the label text</td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr class="recommended">
                <td class="number"><a href="structure/datalabelformats.aspx">PAB-0043</a></td>
                <td>
                Do not include unnecessary punctuation in a label
                </td>
                <td class="rating">Recommended</td>
              </tr>
              <tr>
                <td class="number"><a href="structure/datalabelformats.aspx">PAB-0044</a></td>
                <td>
                Display labels in the style given to label text
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="structure/datalabelformats.aspx">PAB-0045</a></td>
                <td>
                Display values in the style given to data text
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="structure/datalabelformats.aspx">PAB-0046</a></td>
                <td>
                Give more emphasis to the value text style relative to the label text style
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr class="recommended">
                <td class="number"><a href="zone1/labels.aspx">PAB-0047</a></td>
                <td>
                For each label in Zone 1, provide a definition and a means to access the 
                definition, for example, by a tooltip
                </td>
                <td class="rating">Recommended</td>
              </tr>
              <tr class="recommended">
                <td class="number"><a href="zone1/contents.aspx">PAB-0048</a></td>
                <td>
                Provide a means to access the record for all data items in Zone 1
                </td>
                <td class="rating">Recommended</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/contents/order.aspx">PAB-0049</a></td>
                <td>
                Display the patient name elements and the title in the 
                    following order: Family name, given name, title
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/labels.aspx">PAB-0050</a></td>
                <td>
                Do not include labels for the patient name elements and the title
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/formatting.aspx">PAB-0051</a></td>
                <td>
                Display a comma after the family name
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/formatting.aspx">PAB-0052</a></td>
                <td>
                Display the title in parentheses
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/formatting.aspx">PAB-0053</a></td>
                <td>
                Display the patient's family name in upper case and the 
                patient's given name and title in title case
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/contents/preferredname.aspx">PAB-0054</a></td>
                <td>
                Display the patient&#39;s preferred name, if available, 
                immediately below the family name
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="structure/visualstyle.aspx">PAB-0055</a></td>
                <td>
                For a deceased patient, use a background area for Zone 1 in which both the colour 
                and the pattern are substantially different from those used for a living patient
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="structure/visualstyle.aspx">PAB-0056</a></td>
                <td>
                The choice of both background colour and pattern must be such as to differentiate 
                the Patient Banner of a deceased patient from that of a living patient, on all 
                display devices, including, but not limited to, desktop monitors and projected images
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/labels/deceased.aspx">PAB-0057</a></td>
                <td>
                Display the date of death along with its label
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/labels/deceased.aspx">PAB-0058</a></td>
                <td>
                Display the date of death below the date of birth
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/labels/deceased.aspx">PAB-0059</a></td>
                <td>Display the age at death, preceded by its label, immediately after the date of death</td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/contents/age.aspx">PAB-0060</a></td>
                <td>Display the age at death without parentheses</td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/contents/preferredname.aspx">PAB-0061</a></td>
                <td>
                Display the patient&#39;s preferred name, if available, immediately below the 
                given name, with both items left-aligned
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/contents/preferredname.aspx">PAB-0062</a></td>
                <td>
                When a patient&#39;s preferred name is not available, the patient's name 
                must be centred vertically and left-aligned in Zone 1
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/formatting.aspx">PAB-0063</a></td>
                <td>
                For a deceased patient, display the data labels and values corresponding 
                to the date of death and age at death in that order, immediately below the 
                label corresponding to the date of birth, with both the date labels being 
                <span class="nowrap">left-aligned</span>
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/labels/allergies.aspx">PAB-0064</a></td>
                <td>
                Use one of the following labels in the title for the Allergies section: &#39;Known allergies&#39;, 
                &#39;No known allergies&#39;, &#39;Allergies not recorded&#39;, and &#39;Allergies 
                unavailable&#39;
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/expanded.aspx">PAB-0065</a></td>
                <td>
                Display each allergy propensity in the expanded section in Zone 2, along 
                with the date when the record of that propensity was last updated
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/labels/allergies.aspx">PAB-0066</a></td>
                <td>
                Provide a means to enable the user to view the section of the record containing 
                Allergy propensity information, for all instances when the section title is one 
                of: &#39;Known allergies&#39;, &#39;No known allergies&#39;, or &#39;
                Allergies not recorded&#39;
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/labels/allergies.aspx">PAB-0067</a></td>
                <td>
                Emphasise the label &#39;Known allergies&#39; in relation to the other permitted labels
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/labels/allergies.aspx">PAB-0068</a></td>
                <td>
                Display the labels &#39;Known allergies&#39; and &#39;No known allergies&#39;
                 in data text style
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/labels/allergyicons.aspx">PAB-0069</a></td>
                <td>
                Precede the label &#39;Known allergies&#39; with a unique icon that 
                gives the label greater emphasis
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/labels/allergyicons.aspx">PAB-0070</a></td>
                <td>
                Precede the labels &#39;No known allergies&#39; and &#39;Allergies not 
                recorded&#39; with a unique icon that gives the label reduced emphasis
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/labels/allergies.aspx">PAB-0071</a></td>
                <td>
                Display the labels &#39;Allergies not recorded&#39; and &#39;Allergies 
                unavailable&#39; in label text style
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone2/labels/allergyicons.aspx">PAB-0072</a></td>
                <td>
                Precede the label &#39;Allergies unavailable&#39; with a unique icon 
                that gives the label reduced emphasis and that indicates allergies are not available
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="structure/zones.aspx">PAB-0073</a></td>
                <td>
                The Patient Banner must include Zone 1
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr class="recommended">
                <td class="number"><a href="structure/zones.aspx">PAB-0074</a></td>
                <td>
                The Patient Banner should include Zone 2
                </td>
                <td class="rating">Recommended</td>
              </tr>
              <tr>
                <td class="number"><a href="structure/expandcontrol.aspx">PAB-0075</a></td>
                <td>
                Zone 2 must have expand and collapse capability
                </td>
                <td class="rating">Mandatory</td>
              </tr>
              <tr>
                <td class="number"><a href="zone1/contents/order.aspx">PAB-0076</a></td>
                <td>
                Enable a user to tab between the patient identification data in the 
                same order as the displayed information as follows: the patient&#39;s name 
                (family name, given name and title), date of birth, gender and 
                patient identification number
                </td>
                <td class="rating">Mandatory</td>
              </tr>
            </table>
            
      </div>
    </div>
</asp:Content>