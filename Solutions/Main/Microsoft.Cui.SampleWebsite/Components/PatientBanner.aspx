<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Inherits="ComponentsPatientBanner" Title="Untitled Page" CodeBehind="PatientBanner.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="Server">
    <!-- Area for displaying the custom control -->
    <script>
     function reloadBanner()
     {
        ////reload the patient banner data
        var el2=$get("<%= patientBanner.ClientID %>");        
        if(el2!=null && el2.control != null)
        {
         el2.control._resizeHandler();          
        }  
     }
    </script>
    <div class="demoarea first section">
        <!-- Custom Control Heading -->
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        <br />
        <p>
            The PatientBanner control is formed from two zones:
        </p>
        <div>
            <ul>
                <li>Zone One, which displays the minimum data required for initial patient identification</li>
                <li>Zone Two, which displays patient contact information and can be expanded to show
                    additional information</li>
            </ul>
        </div>
    </div>
    <ajaxToolkit:TabContainer OnClientActiveTabChanged="reloadBanner" runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelSilverlightControl" HeaderText="<a id='patientBannerSilverlightTab' href=javascript:TabClick('patientBannerSilverlightTab'); title='Silverlight Tab'>Silverlight</a>">
            <ContentTemplate>
                <br />
                Example Silverlight control (embedded):
                <br />
                <br />
                <object data="data:application/x-silverlight," type="application/x-silverlight-2"
                    width="100%" height="175px">
                    <param name="source" value="../ClientBin/Microsoft.Cui.SamplePages.xap" />
                    <param name="initParams" value="StartPage=PatientBanner,AllowResize=False" />
                    <param name="minRuntimeVersion" value="3.0.40818.0" />
                    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration: none;">
                        <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight"
                            style="border-style: none" />
                    </a>
                </object>
                <iframe style='visibility: hidden; height: 0; width: 0; border: 0px'></iframe>
                <br />
                <div>
                    <!-- Area for Section 1 -->
                    <asp:Panel ID="Section1_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="Section1_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Properties
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Section1_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
                            The PatientBanner control is initialized with the following code:
                            <br />
                             <pre>
&lt;sl:PatientBanner Grid.Row="1" Grid.Column="1" 
          FamilyName="Evans" GivenName="Jonathan" Title="Mr" Gender="Male" 
          DateOfBirth="12-02-2006" PreferredName="Jon"
          Identifier="606 172 4098" IdentifierType="NhsNumber"
          Address1="98 Andover Place" County="Berkshire" Postcode="RG3 5AP" Town="Reading" 
          HomePhoneNumber="(0118) 496 0337" MobilePhoneNumber="(07700) 900555" 
          WorkPhoneNumber="(0118) 496 0338" 
          EmailAddress="jane.evans@abc.xyz" 
          AllergyInformation="Present"
          NameClick="PatientBanner_NameClick"
          PreferredNameClick="PatientBanner_PreferredNameClick"
          PreferredNameLabelClick="PatientBanner_PreferredNameLabelClick"
          IdentifierClick="PatientBanner_IdentifierClick"
          IdentifierLabelClick="PatientBanner_IdentifierLabelClick"
          DateOfBirthClick="PatientBanner_DateOfBirthClick"
          DateOfBirthLabelClick="PatientBanner_DateOfBirthLabelClick"
          DateOfDeathClick="PatientBanner_DateOfDeathClick"
          DateOfDeathLabelClick="PatientBanner_DateOfDeathLabelClick"
          GenderClick="PatientBanner_GenderClick"
          GenderLabelClick="PatientBanner_GenderLabelClick"
          AgeAtDeathClick="PatientBanner_AgeAtDeathClick"
          AgeAtDeathLabelClick="PatientBanner_AgeAtDeathLabelClick"
          ViewAllAddressesClick="PatientBanner_ViewAllAddressesClick" 
          ViewAllergyRecordClick="PatientBanner_ViewAllergyRecordClick"
          ViewContactDetailsClick="PatientBanner_ViewContactDetailsClick"                          
          >
    &lt;sl:PatientBanner.Allergies>
        &lt;sl:Allergy AllergyName="Dust" LastUpdatedOn="01-07-2007" />
        &lt;sl:Allergy AllergyName="Smoke" LastUpdatedOn="10-06-2007" />
        &lt;sl:Allergy AllergyName="Perfume" LastUpdatedOn="14-06-2006" />
        &lt;sl:Allergy AllergyName="Latex" LastUpdatedOn="21-06-2006" />
        &lt;sl:Allergy AllergyName="Peanuts" LastUpdatedOn="06-01-2007" />
        &lt;sl:Allergy AllergyName="Hay" LastUpdatedOn="06-03-2007" />
    &lt;/sl:PatientBanner.Allergies>
&lt;/sl:PatientBanner>
            </pre>
                            <p>
                                See individual controls for information about the properties associated with the
                                elements contained in the PatientBanner; only the properties associated with the
                                PatientBanner as a control are listed below. These have been categorized into:
                            </p>&nbsp
                            <p>
                                <b>Patient Details Properties</b></p>
                            <ul>
                                <li><strong>AddressTypeLabelText</strong> &ndash; gets or sets the caption associated
                                    with the patient's Address field</li>
                                <li><strong>AgeAtDeathLabelText</strong> &ndash; gets or sets the caption associated
                                    with the patient's Age at death field</li>
                                <li><strong>AgeAtDeathLabelTooltip</strong> &ndash; gets or sets the tooltip for the Age at death label</li>
                                    <li><strong>AgeAtDeathTooltip</strong> &ndash; gets or sets the tooltip for the Age at death value</li>
                                <li><strong>Allergies</strong> &ndash; gets or sets the patient's allergies</li>
                                <li><strong>AllergyInformation</strong> &ndash; gets or sets information about the patient's
                                    allergies</li>
                                <li><strong>DateofBirth</strong> &ndash; gets or sets the patient&#39;s date of birth</li>
                                <li><strong>DateofBirthLabelText</strong> &ndash; gets or sets the caption associated
                                    with the patient's date of birth</li>
                                 <li><strong>DateOfBirthLabelToolTip</strong> &ndash; gets or sets the tooltip for the Date of birth label</li>
                                <li><strong>DateOfBirthToolTip</strong> &ndash; gets or sets the tooltip for the Date of birth value</li>
                               <li><strong>DateofDeath</strong> &ndash; gets or sets the patient's date of death</li>
                                <li><strong>DateofDeathLabelText</strong> &ndash; gets or sets the caption associated
                                    with the patient's date of death</li>
                                <li><strong>DateOfDeathLabelToolTip</strong> &ndash; gets or sets the tooltip for the Date of death label</li>
                                <li><strong>DateOfDeathToolTip</strong> &ndash; gets or sets the tooltip for the Date of death value</li>
                                <li><strong>Gender</strong> &ndash; gets or sets the value of the patient's gender</li>
                                <li><strong>GenderLabelText</strong> &ndash; gets or sets the caption associated with
                                    the Gender field</li>
                                <li><strong>GenderLabelTooltip</strong> &ndash; gets or sets the tooltip for the Gender
                                    label</li>
                                <li><strong>GenderValueTooltip</strong> &ndash; gets or sets the tooltip for the
                                    patient's Gender value</li>
                                <li><strong>Identifier</strong> &ndash; gets or sets the patient's unique identifier</li>
                                <li><strong>IdentifierLabelText</strong> &ndash; gets or sets the caption associated
                                    with the patient&#39;s unique identifier or patient identification number field</li>
                                <li><strong>IdentifierLabelTooltip</strong> &ndash; gets or sets the tooltip for the
                                    Identifier label</li>
                                <li><strong>IdentifierTooltip</strong> &ndash; gets or sets the tooltip for the patient's
                                    Identifier value</li>
                                <li><strong>NameDisplayValue</strong></li>
                                &ndash; gets the complete patient name as displayed
                                    <li><strong>PatientNameTooltip</strong> &ndash; gets or sets the tooltip for the patient's name</li>
                                <li><strong>PreferredName</strong> &ndash; gets or sets the patient's Preferred name</li>
                                <li><strong>PreferredNameLabelText</strong> &ndash; gets or sets the caption associated
                                    with the patient's Preferred name</li>
                                    <li><strong>PreferredNameTooltip</strong> &ndash; gets or sets the tooltip for the Preferred name</li>
                                    <li><strong>PreferredNameLabelTooltip</strong> &ndash; gets or sets the tooltip for the Preferred name label</li>
                                    
                            </ul>
                            &nbsp
                            <p>
                                <b>Style Properties</b></p>
                            <ul>                                
                                <li><strong>SubsectionOneWidth</strong> &ndash; gets or sets the width for Subsection
                                    One of the Patient Banner</li>
                                <li><strong>SubsectionTwoWidth</strong> &ndash; gets or sets the width for Subsection
                                    Two of the Patient Banner</li>
                                <li><strong>SubsectionThreeWidth</strong> &ndash; gets or sets the width for Subsection
                                    Three of the Patient Banner</li>
                                <li><strong>SubsectionFourWidth</strong> &ndash; gets or sets the width for Subsection
                                    Four of the Patient Banner</li>
                                <li><strong>SubsectionFiveWidth</strong> &ndash; gets or sets the width for Subsection
                                    Five of the Patient Banner</li>
                                <li><strong>SubsectionOneTitle</strong> &ndash; gets or sets the caption associated
                                    with Subsection One of the Patient Banner</li>
                                <li><strong>SubsectionTwoTitle</strong> &ndash; gets or sets the caption associated
                                    with Subsection Two of the Patient Banner</li>
                                <li><strong>SubsectionThreeTitle</strong> &ndash; gets or sets the caption associated
                                    with Subsection Three of the Patient Banner</li>
                                <li><strong>SubsectionFourTitle</strong> &ndash; gets or sets the caption associated
                                    with Subsection Four of the Patient Banner</li>
                                <li><strong>ViewAllAddressLinkText</strong> &ndash; the text associated with the View
                                    All Addresses link of the Patient Banner</li>
                                <li><strong>ViewAllContactDetailsLinkText</strong> &ndash; the text associated with
                                    the View All Contact Details link of the Patient Banner</li>
                                <li><strong>ViewAllergyRecordLinkText</strong> &ndash; the text associated with the
                                    View Allergy Record link of the Patient Banner</li>
                                <li><strong>PatientNameStyle</strong> &ndash; gets or sets the font styles to be applied for PatientName</li>                                
                                <li><strong>ZoneOneLabelStyle</strong> &ndash; gets or sets the font styles for Zone
                                    One labels</li>
                                <li><strong>ZoneOneDataStyle</strong> &ndash; gets or sets the font styles for Zone One
                                    data, apart from the patient&#39;s name</li>                                
                                <li><strong>ZoneTwoTitleDataStyle</strong> &ndash; gets or sets the font styles for the
                                    data elements in Zone Two title area</li>
                                <li><strong>ZoneTwoTitleLabelStyle</strong> &ndash; gets or sets the font styles for
                                    the data elements in Zone Two of the Patient Banner</li>
                                <li><strong>ZoneTwoLabelStyle</strong> &ndash; gets or sets the font styles for labels
                                    in Zone Two</li>
                                <li><strong>ZoneTwoDataStyle</strong> &ndash; gets or sets the font styles for data in
                                    Zone Two</li>
                                
                            </ul>
                            &nbsp
                            <p>
                                <b>Appearance Properties</b></p>
                            <ul>
                                <li><strong>AllergiesNotPresentIcon</strong> &ndash; gets or sets the URL of the icon to indicate that an allergy is not present</li>
                                <li><strong>AllergiesPresentIcon</strong> &ndash; gets or sets the URL of the icon to indicate that an allergy is present</li>
                                <li><strong>AllergiesUnavailableIcon</strong> &ndash; gets or sets the URL of icon to indicate that allergy information is unavailable</li>                            
                                <li><strong>BorderWidth</strong> &ndash; gets or sets the border width</li>                            
                                <li><strong>CollapseImage</strong> &ndash; gets or sets the URL for the Patient Banner drop-down button to be used when Zone Two is expanded</li>
                                <li><strong>DropDownImage</strong> &ndash; gets or sets the URL for the Patient Banner drop-down image</li>
                                <li><strong>PatientImage</strong> &ndash; gets or sets the URL for the patient&#39;s image in the Patient Banner</li>
                                <li><strong>PatientImageVisible</strong> &ndash; gets or sets a value indicating whether the patient image is visible or not</li>                                    
                                <li><strong>ZoneTwoToolTip</strong> &ndash; gets or sets the value to be displayed in the tooltip for Zone Two</li>
                                <li><strong>ZoneOneMinHeight</strong> &ndash; gets or sets the minimum height for Zone One</li>
                                <li><strong>ZoneTwoExpanded</strong> &ndash; gets or sets the expanded state of Zone Two</li>     
                            </ul>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeSection1" runat="Server" TargetControlID="Section1_ContentPanel"
                        ExpandControlID="Section1_HeaderPanel" CollapseControlID="Section1_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="Section1_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Properties section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Properties section"
                        SuppressPostBack="true" />
                    <!-- Area for Section 2 -->
                    <asp:Panel ID="Section2_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="Section2_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Additional Information
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Section2_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="last section">
                            <ul>
                                <li><strong>AddressTypeLabelText</strong> has a default value of &ldquo;Usual address&rdquo;</li>
                                <li><strong>AgeAtDeathLabelText</strong> has a default value of &ldquo;Age at death&rdquo;</li>
                                <li><strong>DateofBirthLabelText</strong> has a default value of &ldquo;Born&rdquo;</li>
                                <li><strong>DateofDeathLabelText</strong> has a default value of &ldquo;Died&rdquo;</li>
                                <li><strong>GenderLabelText</strong> has a default value of &ldquo;Gender&rdquo;</li>
                                <li><strong>IdentifierLabelText</strong> has a default value of &ldquo;NHS No.&rdquo;</li>
                                <li><strong>PreferredNameLabelText</strong> has a default value of &ldquo;Preferred
                                    name&rdquo;</li>
                                <li><strong>SubsectionOneTitle</strong> has a default value of &ldquo;Address&rdquo;</li>
                                <li><strong>SubsectionTwoTitle</strong> has a default value of &ldquo;Phone and email&rdquo;</li>
                                <li><strong>SubsectionThreeTitle</strong> has a default value of &ldquo;&rdquo;</li>
                                <li><strong>SubsectionFourTitle</strong> has a default value of &ldquo;&rdquo;</li>
                                <li><strong>ViewAllAddressLinkText</strong> has a default value of &ldquo;View all addresses&rdquo;</li>
                                <li><strong>ViewAllContactDetailsLinkText</strong> has a default value of &ldquo;View
                                    all contact details&rdquo;</li>
                                <li><strong>ViewAllergyRecordLinkText</strong> has a default value of &ldquo;View allergy
                                    record&rdquo;</li>
                            </ul>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="cpeSection2" runat="Server" TargetControlID="Section2_ContentPanel"
                        ExpandControlID="Section2_HeaderPanel" CollapseControlID="Section2_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="Section2_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                        SuppressPostBack="true" />
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelWPF" HeaderText="<a id='patientBannerWPFTab' href=javascript:TabClick('patientBannerWPFTab'); title='WPF Tab'>WPF</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WPF control (screenshot):
                    <br />
                    <br />
                    <div>
                        <img id="Img1" alt="PatientBanner WPF Control screenshot" title="PatientBanner WPF Control screenshot"
                            runat="server" src="~/Components/Images/patientbanner_wpf.GIF" />
                    </div>
                    <br />
                    <p>
                        Further information on this control can be found on the Silverlight tab above.
                        The full source code can be found in the Microsoft Health Common User Interface Toolkit,
                        which can be downloaded from our
                        <a href="http://www.codeplex.com/mscui/Release/ProjectReleases.aspx" target="_blank"
                            title="Link to releases page on the CodePlex site (New Window)">CodePlex</a>
                        site.
                    </p>
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelASPNET" HeaderText="<a id='patientBannerASPNETTab' href=javascript:TabClick('patientBannerASPNETTab'); title='ASP.NET Tab' alt='ASP.NET Tab'>ASP.NET</a>">
            <ContentTemplate>
                <br />
                Example ASP.NET control (embedded):
                <br />
                <br />
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="demoPanel1" runat="server">                                                       
                            <NhsCui:PatientBanner ID="patientBanner" PatientImage="~/Images/PatientSnaps/JohnEvans.gif"
                                FamilyName="Evans" GivenName="Jonathan" Title="Mr" DateOfBirth="12-Feb-2006" PreferredName="Jon"
                                Identifier="606 172 4098" IdentifierType="NhsNumber" Gender="Male" HomePhoneNumber="(0118) 496 0337"
                                WorkPhoneNumber="(0118) 496 0338" MobilePhoneNumber="(07700) 900555" EmailAddress="jane.evans@abc.xyz"
                                Address1="98 Andover Place" Town="Reading" County="Berkshire" Postcode="RG3 5AP"
                                AccessKey="P" ZoneTwoTooltip="Click to expand"  runat="server"
                                OnViewAllAddressesClick="PatientBanner_ViewAllAddressesClick" OnViewAllergyRecordClick="PatientBanner_ViewAllergyRecordClick"
                                OnViewAllContactDetailsClick="PatientBanner_ViewAllContactDetailsClick" OnGenderValueClick="PatientBanner_GenderValueClick"
                                OnIdentifierClick="PatientBanner_IdentifierClick" AllergyInformation="Present">
                            </NhsCui:PatientBanner>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>                
                <br />
                <!-- Area for Properties -->
                <asp:Panel ID="SilverlightControl_Properties_HeaderPanel" runat="server" Style="cursor: pointer;">
                    <div class="heading">
                        <input type="image" id="SilverlightControl_Properties_ToggleImage" runat="server"
                            src="~/images/SFTheme/acc_h.png" />
                        Properties
                    </div>
                </asp:Panel>
                <asp:Panel ID="SilverlightControl_properties_ContentPanel" runat="server" Style="overflow: hidden;"
                    Height="0px">
                    <div class="section">
                        The PatientBanner control is initialized with the following code:
                        <br />
                        <pre>
     &lt;NhsCui:PatientBanner ID="patientBanner" PatientImage="~/images/PatientSnaps/JohnEvans.jpg"
            FamilyName="Evans" GivenName="Jonathan" Title="Mr" DateOfBirth="12-Feb-2006"
            Identifier="606 172 4098" IdentifierType="NhsNumber" Gender="Male" PreferredName="Jon"
            HomePhoneNumber="(0118) 496 0337" WorkPhoneNumber="(0118) 496 0338"
            MobilePhoneNumber="(07700) 900555" EmailAddress="jane.evans@abc.xyz"
            Address1="98 Andover Place" Town="Reading"
            County="Berkshire" PostCode="RG3 5AP" AccessKey="P" ZoneTwoTooltip="Click to expand" 
            runat="server"&gt;
     &lt;/NhsCui:PatientBanner&gt;

            </pre>
                        <p>
                            See individual controls for information about the properties associated with the
                            elements contained in the PatientBanner; only the properties associated with the
                            PatientBanner as a control are listed below. These have been categorized into:
                        </p> &nbsp
                        <p>
                            <b>Patient Details Properties</b></p>
                        <ul>
                            <li><strong>AddressTypeLabelText</strong> &ndash; gets or sets the caption associated
                                with the patient&#39;s address label</li>
                            <li><strong>AgeAtDeathLabelText</strong> &ndash; gets or sets the caption associated
                                with the patient&#39;s age at death</li>
                            <li><strong>Allergies</strong> &ndash; gets or sets the patient&#39;s allergies</li>
                            <li><strong>AllergyInformation</strong> &ndash; gets or sets information about the patient&#39;s
                                allergies</li>
                            <li><strong>DateofBirth</strong> &ndash; gets or sets the patient&#39;s date of birth</li>
                            <li><strong>DateofBirthLabelText</strong> &ndash; gets or sets the caption associated
                                with the patient&#39;s date of birth</li>
                            <li><strong>DateofDeath</strong> &ndash; gets or sets the patient&#39;s date of death</li>
                            <li><strong>DateofDeathLabelText</strong> &ndash; gets or sets the caption associated
                                with the patient&#39;s date of death</li>
                            <li><strong>Gender</strong> &ndash; gets or sets the value of the patient&#39;s gender</li>
                            <li><strong>GenderLabelText</strong> &ndash; gets or sets the caption associated with
                                the Gender field</li>
                            <li><strong>GenderLabelTooltip</strong> &ndash; gets or sets the tooltip for the Gender
                                label </li>
                            <li><strong>GenderValueTooltip</strong> &ndash; gets or sets the tooltip for the 
                                patient&#39;s gender</li>
                            <li><strong>Identifier</strong> &ndash; gets or sets the patient&#39;s unique identifier</li>
                            <li><strong>IdentifierLabelText</strong> &ndash; gets or sets the caption associated
                                with the patient&#39;s unique identifier or patient identification number</li>
                            <li><strong>IdentifierLabelTooltip</strong> &ndash; gets or sets the tooltip for the
                                Identifier label</li>
                            <li><strong>IdentifierTooltip</strong> &ndash; gets or sets the tooltip for the patient&#39;s
                                identifier</li>
                            <li><strong>NameDisplayValue</strong></li>
                            &ndash; gets the complete patient name as displayed
                            <li><strong>PreferredName</strong> &ndash; gets or sets the patient&#39;s preferred name</li>
                            <li><strong>PreferredNameLabelText</strong> &ndash; gets or sets the caption associated
                                with the patient&#39;s preferred name</li>
                        </ul>
                        &nbsp
                        <p>
                            <b>Style Properties</b></p>
                        <ul>
                            <li><strong>ActivePatientStyle</strong> &ndash; gets or sets the style used for active
                                patients</li>
                            <li><strong>DeadPatientStyle</strong> &ndash; gets or sets the style used for dead patients</li>
                            <li><strong>PatientNameStyle</strong> &ndash; gets or sets the style to be used for
                                the patient&#39;s name</li>
                            <li><strong>SubsectionOneWidth</strong> &ndash; gets or sets the width for Subsection
                                One of the Patient Banner</li>
                            <li><strong>SubsectionTwoWidth</strong> &ndash; gets or sets the width for Subsection
                                Two of the Patient Banner</li>
                            <li><strong>SubsectionThreeWidth</strong> &ndash; gets or sets the width for Subsection
                                Three of the Patient Banner</li>
                            <li><strong>SubsectionFourWidth</strong> &ndash; gets or sets the width for Subsection
                                Four of the Patient Banner</li>
                            <li><strong>SubsectionFiveWidth</strong> &ndash; gets or sets the width for Subsection
                                Five of the Patient Banner</li>
                            <li><strong>SubsectionOneTitle</strong> &ndash; gets or sets the caption associated
                                with Subsection One</li>
                            <li><strong>SubsectionTwoTitle</strong> &ndash; gets or sets the caption associated
                                with Subsection Two</li>
                            <li><strong>SubsectionThreeTitle</strong> &ndash; gets or sets the caption associated
                                with Subsection Three</li>
                            <li><strong>SubsectionFourTitle</strong> &ndash; gets or sets the caption associated
                                with Subsection Four</li>
                            <li><strong>ViewAllAddressLinkText</strong> &ndash; the text associated with the View
                                All Addresses link of the Patient Banner</li>
                            <li><strong>ViewAllContactDetailsLinkText</strong> &ndash; the text associated with
                                the View All Contact Details link of the Patient Banner</li>
                            <li><strong>ViewAllergyRecordLinkText</strong> &ndash; the text associated with the
                                View Allergy Record link of the Patient Banner</li>
                            <li><strong>ZoneOneStyle</strong> &ndash; gets or sets the default CSS style for Zone
                                One of the Patient Banner</li>
                            <li><strong>ZoneOneLabelStyle</strong> &ndash; gets or sets the style for Zone One labels</li>
                            <li><strong>ZoneOneDataStyle</strong> &ndash; gets or sets the style for Zone One data,
                                apart from the patient&#39;s name</li>
                            <li><strong>ZoneOneDataWithTooltipHoverStyle</strong> &ndash; gets or sets the style
                                that indicates when the data in Zone One has a tooltip</li>
                            <li><strong>ZoneOneLabelWithTooltipHoverStyle</strong> &ndash; gets or sets the style
                                that indicates when the labels in Zone One have a tooltip</li>
                            <li><strong>ZoneTwoTitleStyle</strong> &ndash; gets or sets the style for the Zone Two
                                title area</li>
                            <li><strong>ZoneTwoStyle</strong> &ndash; gets or sets the default CSS style for Zone
                                Two of the Patient Banner</li>
                            <li><strong>ZoneTwoLabelStyle</strong> &ndash; gets or sets the style for Zone Two labels</li>
                            <li><strong>ZoneTwoDataStyle</strong> &ndash; gets or sets the style for Zone Two data</li>
                            <li><strong>ZoneTwoHoverStyle</strong> &ndash; gets or sets the style to be used to
                                indicate Zone Two is clickable or a hyperlink is present</li>
                            <li><strong>ZoneTwoExpanded</strong> &ndash; gets or sets the expanded state of Zone
                                Two</li>
                        </ul>
                        &nbsp
                        <p>
                            <b>Appearance Properties</b></p>
                        <ul>
                            <li><strong>CollapseImage</strong> &ndash; gets or sets the URL for the Patient Banner
                                drop-down button to be used when Zone Two is expanded</li>
                            <li><strong>DropDownImage</strong> &ndash; gets or sets the URL for the Patient Banner
                                drop-down image</li>
                            <li><strong>PatientImage</strong> &ndash; gets or sets the URL for the patient&#39;s image
                                in the Patient Banner</li>
                            <li><strong>ZoneTwoToolTip</strong> &ndash; gets or sets the value to be displayed in
                                the tooltip for Zone Two</li>
                                
                        </ul>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="SilverlightControl_Properties_CollapsiblePanelExtender"
                    runat="Server" TargetControlID="SilverlightControl_Properties_ContentPanel" ExpandControlID="SilverlightControl_Properties_HeaderPanel"
                    CollapseControlID="SilverlightControl_Properties_HeaderPanel" Collapsed="True"
                    ExpandDirection="Vertical" ImageControlID="SilverlightControl_Properties_ToggleImage"
                    ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Properties section"
                    CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Properties section"
                    SuppressPostBack="true" />
                <!-- Area for Additional Info -->
                <asp:Panel ID="SilverlightControl_AdditionalInfo_HeaderPanel" runat="server" Style="cursor: pointer;">
                    <div class="heading">
                        <input type="image" id="SilverlightControl_AdditionalInfo_ToggleImage" runat="server"
                            src="~/images/SFTheme/acc_v.png" />
                        Additional Information
                    </div>
                </asp:Panel>
                <asp:Panel ID="SilverlightControl_AdditionalInfo_ContentPanel" runat="server" Style="overflow: hidden;
                    height: 0px">
                    <div class="last section">
                        <ul>
                            <li><strong>AddressTypeLabelText</strong> has a default value of &ldquo;Usual address&rdquo;</li>
                            <li><strong>AgeAtDeathLabelText</strong> has a default value of &ldquo;Age at death&rdquo;</li>
                            <li><strong>DateofBirthLabelText</strong> has a default value of &ldquo;Born&rdquo;</li>
                            <li><strong>DateofDeathLabelText</strong> has a default value of &ldquo;Died&rdquo;</li>
                            <li><strong>GenderLabelText</strong> has a default value of &ldquo;Gender&rdquo;</li>
                            <li><strong>IdentifierLabelText</strong> has a default value of &ldquo;NHS No.&rdquo;</li>
                            <li><strong>PreferredNameLabelText</strong> has a default value of &ldquo;Preferred
                                name&rdquo;</li>
                            <li><strong>SubsectionOneTitle</strong> has a default value of &ldquo;Address&rdquo;</li>
                            <li><strong>SubsectionTwoTitle</strong> has a default value of &ldquo;Phone and email&rdquo;</li>
                            <li><strong>SubsectionThreeTitle</strong> has a default value of &ldquo;&rdquo;</li>
                            <li><strong>SubsectionFourTitle</strong> has a default value of &ldquo;&rdquo;</li>
                            <li><strong>ViewAllAddressLinkText</strong> has a default value of &ldquo;View all addresses&rdquo;</li>
                            <li><strong>ViewAllContactDetailsLinkText</strong> has a default value of &ldquo;View
                                all contact details&rdquo;</li>
                            <li><strong>ViewAllergyRecordLinkText</strong> has a default value of &ldquo;View allergy
                                record&rdquo;</li>
                        </ul>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="SilverlightControl_AdditionalInfo_CollapsiblePanelExtender"
                    runat="Server" TargetControlID="SilverlightControl_AdditionalInfo_ContentPanel"
                    ExpandControlID="SilverlightControl_AdditionalInfo_HeaderPanel" CollapseControlID="SilverlightControl_AdditionalInfo_HeaderPanel"
                    Collapsed="True" ExpandDirection="Vertical" ImageControlID="SilverlightControl_AdditionalInfo_ToggleImage"
                    ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                    CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                    SuppressPostBack="true" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" ID="panelWinformsControl" HeaderText="<a id='patientBannerWinFormsTab' href=javascript:TabClick('patientBannerWinFormsTab'); title='WinForms Tab' alt='WinForms Tab'>WinForms</a>">
            <ContentTemplate>
                <div>
                    <br />
                    Example WinForms control (screenshot):
                    <br />
                    <br />
                    <div>
                        <img alt="PatientBanner WinForms control screenshot" title="PatientBanner WinForms control screenshot"
                            runat="server" src="~/Components/Images/patientbanner.GIF" />
                    </div>
                    <br />
                    <p>
                        The full source code for this control can be found in the
                        Microsoft Health Common User Interface Toolkit, which can be downloaded from our
                        <a href="http://www.codeplex.com/mscui/Release/ProjectReleases.aspx" target="_blank"
                            title="Link to releases page on the CodePlex site (New Window)">CodePlex</a>
                        site.
                    </p>
                    <!-- Area for Winforms Properties Section -->
                    <asp:Panel ID="WinformsProps_HeaderPanel" runat="server" Style="cursor: pointer;">
                        <div class="heading">
                            <input type="image" id="WinformsProps_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                            Properties
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="WinformsProps_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                        <div class="section">
                        <p>
                            See individual controls for information about the properties associated with the
                            elements contained in the PatientBanner; only the properties associated with the
                            PatientBanner as a control are listed below. These have been categorized into:
                        </p> &nbsp
                        <p>
                            <b>Patient Details Properties</b></p>
                        <ul>
                            <li><strong>AddressTypeLabelText</strong> &ndash; gets or sets the caption associated
                                with the patient&#39;s address label</li>
                            <li><strong>AgeAtDeathLabelText</strong> &ndash; gets or sets the caption associated
                                with the patient&#39;s age at death</li>
                            <li><strong>Allergies</strong> &ndash; gets or sets the patient&#39;s allergies</li>
                            <li><strong>AllergyInformation</strong> &ndash; gets or sets information about the patient&#39;s
                                allergies</li>
                            <li><strong>DateofBirth</strong> &ndash; gets or sets the patient&#39;s date of birth</li>
                            <li><strong>DateofBirthLabelText</strong> &ndash; gets or sets the caption associated
                                with the patient&#39;s date of birth</li>
                            <li><strong>DateofDeath</strong> &ndash; gets or sets the patient&#39;s date of death</li>
                            <li><strong>DateofDeathLabelText</strong> &ndash; gets or sets the caption associated
                                with the patient&#39;s date of death</li>
                            <li><strong>Gender</strong> &ndash; gets or sets the value of the patient&#39;s gender</li>
                            <li><strong>GenderLabelText</strong> &ndash; gets or sets the caption associated with
                                the Gender field</li>
                            <li><strong>GenderLabelTooltip</strong> &ndash; gets or sets the tooltip for the Gender
                                label </li>
                            <li><strong>GenderValueTooltip</strong> &ndash; gets or sets the tooltip for the 
                                patient&#39;s gender</li>
                            <li><strong>Identifier</strong> &ndash; gets or sets the patient&#39;s unique identifier</li>
                            <li><strong>IdentifierLabelText</strong> &ndash; gets or sets the caption associated
                                with the patient&#39;s unique identifier or patient identification number</li>
                            <li><strong>IdentifierLabelTooltip</strong> &ndash; gets or sets the tooltip for the
                                Identifier label</li>
                            <li><strong>IdentifierTooltip</strong> &ndash; gets or sets the tooltip for the patient&#39;s
                                identifier</li>
                            <li><strong>NameDisplayValue</strong></li>
                            &ndash; gets the complete patient name as displayed
                            <li><strong>PreferredName</strong> &ndash; gets or sets the patient&#39;s preferred name</li>
                            <li><strong>PreferredNameLabelText</strong> &ndash; gets or sets the caption associated
                                with the patient&#39;s preferred name</li>
                        </ul>
                        &nbsp
                        <p>
                            <b>Style Properties</b></p>
                        <ul>
                            <li><strong>SubsectionOneWidth</strong> &ndash; gets or sets the width for Subsection
                                One of the Patient Banner</li>
                            <li><strong>SubsectionTwoWidth</strong> &ndash; gets or sets the width for Subsection
                                Two of the Patient Banner</li>
                            <li><strong>SubsectionThreeWidth</strong> &ndash; gets or sets the width for Subsection
                                Three of the Patient Banner</li>
                            <li><strong>SubsectionFourWidth</strong> &ndash; gets or sets the width for Subsection
                                Four of the Patient Banner</li>
                            <li><strong>SubsectionFiveWidth</strong> &ndash; gets or sets the width for Subsection
                                Five of the Patient Banner</li>
                            <li><strong>SubsectionOneTitle</strong> &ndash; gets or sets the caption associated
                                with Subsection One</li>
                            <li><strong>SubsectionTwoTitle</strong> &ndash; gets or sets the caption associated
                                with Subsection Two</li>
                            <li><strong>SubsectionThreeTitle</strong> &ndash; gets or sets the caption associated
                                with Subsection Three</li>
                            <li><strong>SubsectionFourTitle</strong> &ndash; gets or sets the caption associated
                                with Subsection Four</li>
                            <li><strong>ViewAllAddressLinkText</strong> &ndash; the text associated with the View
                                All Addresses link of the Patient Banner</li>
                            <li><strong>ViewAllContactDetailsLinkText</strong> &ndash; the text associated with
                                the View All Contact Details link of the Patient Banner</li>
                            <li><strong>ViewAllergyRecordLinkText</strong> &ndash; the text associated with the
                                View Allergy Record link of the Patient Banner</li>
                            <li><strong>ZoneTwoExpanded</strong> &ndash; gets or sets the expanded state of Zone
                                Two</li>
                        </ul>
                        &nbsp
                        <p>
                            <b>Appearance Properties</b></p>
                        <ul>
                            <li><strong>CollapseImage</strong> &ndash; gets or sets the URL for the Patient Banner
                                drop-down button to be used when Zone Two is expanded</li>
                            <li><strong>DropDownImage</strong> &ndash; gets or sets the URL for the Patient Banner
                                drop-down image</li>
                            <li><strong>PatientImage</strong> &ndash; gets or sets the URL for the patient&#39;s image
                                in the Patient Banner</li>
                            <li><strong>ZoneTwoToolTip</strong> &ndash; gets or sets the value to be displayed in
                                the tooltip for Zone Two</li>                                
                        </ul>
                        </div>
                    </asp:Panel>
                    <ajaxToolkit:CollapsiblePanelExtender ID="WinformsProps_Extender" runat="Server" TargetControlID="WinformsProps_ContentPanel"
                        ExpandControlID="WinformsProps_HeaderPanel" CollapseControlID="WinformsProps_HeaderPanel"
                        Collapsed="True" ExpandDirection="Vertical" ImageControlID="WinformsProps_ToggleImage"
                        ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Properties section"
                        CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Properties section"
                        SuppressPostBack="true" />                    
                        <!-- Area for Winforms Additional Information Section -->
                        <asp:Panel ID="WinformsAddInfo_HeaderPanel" runat="server" Style="cursor: pointer;">
                            <div class="heading">
                                <input type="image" id="WinformsAddInfo_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                                Additional Information
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="WinformsAddInfo_ContentPanel" runat="server" Style="overflow: hidden; height: 0px">
                            <div class="last section">
                                <ul>
                                    <li>IdentifierLabelText has a default value of "NHS No."</li>
                                    <li>DateofBirthLabelText has a default value of "Born"</li>
                                    <li>DateofDeathLabelText has a default value of "Died"</li>
                                    <li>AgeatDeathLabelText has a default value of 'Age at Death'</li>
	                                <li>AddressTypeLabelText has a default value of 'Usual address'</li>                
                                    <li>HomePhoneLabelText has a default value of "Home"</li>
                                    <li>WorkPhoneLabelText has a default value of "Work"</li>
                                    <li>MobilePhoneLabelText has a default value of "Mobile"</li>
                                    <li>EmailLabelText has a default value of "Email"</li>              
                                    <li>SubsectionOneTitle has a default value of "Address"</li>
                                    <li>SubsectionTwoTitle has a default value of "Phone and email"</li>
                                    <li>SubsectionThreeTitle has a default value of ""</li>
                                    <li>SubsectionFourTitle has a default value of ""</li>  
	                                <li>AllergyInformation has a default value of 'Allergies unavailable'</li>
	                                <li>ViewAllAddressesLinkText has a default value of 'View all addresses'</li>
	                                <li>ViewAllContactDetailsLinkText has a default value of 'View all contact details'</li>
	                                <li>ViewAllergyRecordLinkText has a default value of 'View allergy record'</li>
                                </ul>
                            </div>
                        </asp:Panel>
                        <ajaxToolkit:CollapsiblePanelExtender ID="WinformsAddInfo_Extender" runat="Server" TargetControlID="WinformsAddInfo_ContentPanel"
                            ExpandControlID="WinformsAddInfo_HeaderPanel" CollapseControlID="WinformsAddInfo_HeaderPanel"
                            Collapsed="True" ExpandDirection="Vertical" ImageControlID="WinformsAddInfo_ToggleImage"
                            ExpandedImage="~/images/SFTheme/acc_v.png" ExpandedText="Click to collapse the Additional Information section"
                            CollapsedImage="~/images/SFTheme/acc_h.png" CollapsedText="Click to expand the Additional Information section"
                            SuppressPostBack="true" />
                </div>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>

    <script type="text/javascript">
    function pageLoad()
    {        
        if(this.msg != undefined && this.msg != "")
        {
            alert(this.msg);
            this.msg="";
        }
    }
    </script>

</asp:Content>
