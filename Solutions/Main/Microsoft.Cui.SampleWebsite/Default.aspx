<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/DefaultMaster.master"
    AutoEventWireup="true" Inherits="Default" CodeBehind="Default.aspx.cs" Title="Microsoft Health - CUI Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pageSpecificHeadTags" runat="server">
    <style type="text/css">
        ol li.section
        {
            /* Create a new block formatting context to clear the float */
            overflow: hidden;
            zoom: 1; /* hasLayout */
        }
        ol
        {
            padding: 0;
            list-style: none;
            float: left;
            width: 703px;
            margin: 0;
        }
        ol li.section img
        {
            float: right;
            margin: 4px 4px 2px 6px;
        }
        ol li.alternate img
        {
            float: left;
            margin-right: 10px;
            margin-left: 0;
        }
        ol p
        {
            margin-left: 0;
            margin-top: 2px;
        }
        ol li.section
        {
            border: solid 1px #CCC;
            padding: 10px;
            margin-bottom: 6px;
        }
        .videoSection img
        {
            border: none;
            position: absolute; /* Take the image out of the flow */
        }
        #sidebar
        {
            float: right;
        }
        #sidebar .downloadBox
        {
            border: solid 1px #ccc;
            padding: 4px;
            width: 270px;
            margin: 0 0 4px 4px;
        }
        #sidebar .downloadBox img
        {
            float: right;
        }
        #sidebar .sidebarsection
        {
            border: solid 1px #ccc;
            padding: 10px;
            width: 258px;
            margin: 0 0 6px 3px;
        }
        #sidebar .sidebarbottomsection
        {
            border: solid 1px #ccc;
            padding: 10px;
            width: 258px;
            margin: 0 0 6px 3px;
            height: 350px;
        }
        #sidebar .sidebarbottomsection img
        {
            float: right;
            margin-top: 3px;
            margin-right: 3px;
        }
        .sidebarsection_h1
        {
            margin: 0 0 6px 0;
            padding-top: 3px;
            padding-left: 10px;
            font-size: 13pt;
            color: #fff;
            text-transform: uppercase;
            background-color: #369;
            background-image: url(../Images/SFTheme/downloadheading.png);
            background-repeat: repeat-y;
            background-position: right;
            border: solid 1px #000;
            height: 27px;
        }
        .sidebarsection_latestblog
        {
            margin: 0 0 6px 3px;
            padding: 0 0 0 7px;
            font-size: 13pt;
            color: #fff;
            text-align: left;
            text-transform: uppercase;
            background-color: #369;
            background-image: url(../Images/SFTheme/downloadheading.png);
            background-repeat: repeat-y;
            background-position: right;
            border: solid 1px #000;
            height: 30px;
            width: 270px;
        }
        .subscribe
        {
            font-size: 8pt;
            text-transform: none;
        }
        .getinvolvedsection
        {
            margin-left: 215px;
            height: 110px;
        }
        .latestnewssection
        {
            border: solid 1px #ccc;
            padding: 10px;
            width: 325px;
            height: 145px;
            margin: 0 0 0 0;
            background-color: #EDF2F6;
        }
        .latestnewssection img
        {
            float: right;
            margin-top: 3px;
        }
        .latestblogPublishTime
        {
            font-size: 8pt;
            color: Gray;
        }
        .blogfullpostlink
        {
            background-image: url(../Images/SFTheme/blogBase.png);
            background-repeat: repeat;
            height: 21px;
            width: 270px;
            top: 463px;
            position: absolute;
            text-align: right;
            padding-right: 8px;
            padding-top: 8px;
            margin-left: -10px;
        }
        .sidebarsection ul
        {
            list-style-type: square;
            padding-left: 0;
            margin-left: 25px;
        }
        #getinvolved
        {
            height: 314px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="siteContentPlaceHolder" runat="Server">
    <ol id="homepageContent">
        <li class="section">
            <img src="images/SFTheme/photo1.jpg" width="201" height="146" title="'Welcome' image"
                alt="Photographic image accompanying the 'Welcome' paragraphs, no information conveyed." />
            <h1>
                Welcome</h1>
            <p>
                The Microsoft Health Common User Interface (MSCUI) offerings form part of the <a
                    href="http://www.microsoft.com/HealthICT" target="_blank" title="Link to Microsoft Health ICT Resource Center (New Window)">
                    Microsoft Health ICT Resource Center</a>. MSCUI provides User Interface <a href="DesignGuide/DesignGuide.aspx"
                        title="Link to Guidance section">Design Guidance</a> and <a title="Link to Controls and Samples section"
                            href="ControlsAndSamples.aspx">Toolkit controls</a> that address a wide
                range of patient safety issues faced by healthcare organizations worldwide. Those
                design guides and controls enable the quick and easy creation of a new generation
                of safer, more usable and compelling health applications.
            </p>
            <p>
                Visit the MSCUI Web site to:</p>
            <ul>
                <li>Read more about the project in the <a href="Introduction/Introduction.aspx" title="Link to Introduction section">
                    Introduction</a></li>
                <li>See scenario-based technology demonstrators and testimonials from clinical application
                    and healthcare providers in the <a href="Showcase/Showcase.aspx" title="Link to Showcase section">
                        Showcase</a></li>
                <li>Visit our <a target="_blank" href="http://www.mscui.net/Blog/Default.aspx" title="Link to Team Blog">
                    Team Blog</a> for news and announcements</li>
                <li>Access our <a target="_blank" href="http://www.codeplex.com/mscui/Thread/List.aspx"
                    title="Link to CodePlex forum (New Window)">CodePlex forum</a> to read related discussions</li>
            </ul>
            <p>
                Why not also take a look at the <a href="http://www.microsoft.com/industry/healthcare/technology/hpo/overview.aspx"
                    target="_blank" title="Link to Microsoft Connected Health Platform Resources (New Window)">
                    Microsoft Connected Health Platform Resources</a>, which offer prescriptive
                architecture, solution accelerators, design and deployment guidance that help you
                build optimized e-Health platforms and solutions?</p>
        </li>
        <li>
            <div class="sidebarsection">
                <div class="sidebarsection_h1">
                    Latest News
                </div>
            </div>
            <div>
                <div class="latestnewssection" style="width: 681px; height: 145px; margin-bottom: 5px;">
                    <img src="images/CDSA/CDSAThumbnail.jpg" style="margin-left: 10px" title="'Clinical Documentation Solution Accelerator' Image" />
                    <b>Clinical Documentation Solution Accelerator</b>
                    <p>
                        The Clinical Documentation Solution Accelerator (CDSA) for the Microsoft Office
                        system lets clinicians create familiar, human-readable documents, such as Discharge
                        Summaries, whilst at the same time having the data mapped to a machine-readable
                        encoding, such as <a href="http://www.ihtsdo.org/snomed-ct/" target="_blank" title="Opens the SNOMED Clinical Terms website (New Window)">
                            SNOMED CT</a><sup>&reg;</sup>.
                    </p>
                    <a href="CDSA.htm" target="_blank" title="Link to Clinical Documentation Solution Accelerator page (New Window)">
                        Go to the CDSA page to learn more, view videos and to download CDSA</a>
                </div>
                <div class="latestnewssection" style="height: 572px; float: left; margin-bottom: 5px;">
                    <b>Design Guidance Information Accelerators</b>
                    <img src="images/SFTheme/QIG-Thumb.jpg" width="325" height="105" style="margin-bottom: 5px; margin-top: 5px;" title="'Quick Implementation Guide' Image" />                    
                    <p>
                        Our new information accelerators enable rapid access to the user interface definitions. We&#39;ve created two types of accelerator:
                    </p>
                    <ul style="padding-left:0px; margin-left:18px; margin-bottom: 10px; list-style-type: square;">
                                <li>Interactive Quick Implementation Guides</li>
                                <li>Crib Sheets</li>                                
                    </ul>
                    <p>
                        Interactive Quick Implementation Guides present guidance in a visual, hyperlinked style. You jump directly to the relevant guidelines and instantly see them referenced against usage examples. We&#39;re offering interactive guides for:
                    </p>
                    <ul  style="padding-left:0px; margin-left:18px; margin-bottom: 10px; list-style-type: square;">
                                <li>All &#39;demographic&#39; design guides adopted by the UK&#39;s Information Standards Board for Health and Social Care (ISB) as standards throughout the National Health Service (NHS) in England</li>
                                <li><a href="DesignGuide/MedicationLine.aspx" title="Link to Medication Line Guidance page">Medication Line</a>, <a href="DesignGuide/MedicationsList.aspx" title="Link to Medications List Guidance page">Medications List</a> and <a href="DesignGuide/SearchPrescribe.aspx" title="Link to Search and Prescribe Guidance page">Search and Prescribe</a></li>                                
                    </ul>
                    <p>
                        Crib Sheets provide &#39;at-a-glance&#39; summaries of the key guidance points for those same guides (not the latter two). We&#39;re offering Crib Sheets as downloadable PDF files: just pick those focusing on your interests.
Explore the information accelerators by checking out the links on the relevant guidance pages.

                    </p>
                </div>
                <div class="latestnewssection" style="float: right; height: 355px; width: 330px;
                    margin-bottom: 5px;">
                    <b>Latest Toolkit Controls</b>
                    <p>
                        The <a href="Components/SearchAndPrescribe.aspx" title="Link to SearchAndPrescribe Control page">
                            SearchAndPrescribe</a> Control allows designers and developers to implement
                        a subset of prescribing functionality.
                        <img src="images/SFTheme/SearchAndPrescribe_small.jpg" style="width: 330px; margin-top: 10px;
                            margin-bottom: 10px" title="'SearchAndPrescribe Control' Image" style="padding-bottom: 10px;" />
                    </p>
                    <p>
                        The <a href="Components/Timeline.aspx" title="Link to Timeline Control page">Timeline</a>
                        Control provides a mechanism to view and compare single events and items of duration
                        against time.
                        <img src="images/SFTheme/Timeline_small.jpg" style="width: 330px; margin-top: 10px"
                            title="'Timeline Control' Image" />
                    </p>
                </div>
                <div class="latestnewssection" style="height: 190px; float: right;width: 330px; margin-bottom: 5px;">
                    <img src="images/partners.jpg" width="330" height="105" title="'Our Partners Talk' Image" />
                    <b>Our Partners Talk</b>
                    <p>
                        Visit our <a href="Showcase/Showcase.aspx" title="Link to Showcase section">Showcase</a>
                        to hear how Bluewire Technologies, in4tek (now known as Civica), Map of Medicine,
                        and The Learning Clinic are using our design guidance and controls.
                    </p>
                </div>
            </div>
        </li>
    </ol>
    <div id="sidebar">
        <div class="sidebarsection_latestblog">
            <table style="margin-top: 1px;">
                <tbody>
                    <tr>
                        <td style="width: 180px;">
                            Latest Blog
                        </td>
                        <td style="float: right; padding-top: 6px;">
                            <img alt="Feed" src="images/rssButton.gif">
                        </td>
                        <td class="subscribe">
                            <a href="<%=BlogSyndicationUrl%>" style="color: White;">Subscribe</a>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="sidebarsection" style="background-color: #EDF2F6; overflow: hidden; height: 315px;">
            <div>
                <asp:HyperLink runat="server" ID="blogLink">
                    <asp:Literal ID="latestBlogEntryTitle" runat="server" />
                </asp:HyperLink><br />
                <asp:Label CssClass="latestblogPublishTime" ID="latestBlogPublishTime" runat="server"></asp:Label><asp:Literal ID="latestBlogEntrySummary" runat="server" />
                <br />
            </div>
            <div class="blogfullpostlink">
                <asp:HyperLink Font-Underline="true" runat="server" ID="blogFullPostLink" Text="Click here to see the full post" />
            </div>
        </div>
        <div class="sidebarsection" style="height: 366px;">
            <b>Patient Journey Demonstrator</b> <div style="margin-top: 2px">
                Explore the exciting features in our <a target="_blank" href="<%=ConfigurationSettings.AppSettings["PatientJourneyDemonstrator"]%>"
                    title="Link to Patient Journey Demonstrator (New Window)">latest release</a> including SNOMED Clinical Terms<sup>&reg;</sup>, Clinical Noting, video inking and
                more. <br />
                <img src="images/pjd_mscui_thumb.jpg" style="margin-top:5px; margin-bottom:5px;" height="149" title="'Patient Journey Demonstrator' Image" /> <br />
                SNOMED Clinical Terms<sup>&reg;</sup> (SNOMED&nbsp;CT<sup>&reg;</sup>) is provided
                by <a target="_blank" href="http://www.healthlanguage.com" title="Link to Health Language, Inc (New Window)">Health Language, Inc</a> and used by permission of the <a target="_blank" href="http://www.ihtsdo.org/">International Health Terminology Standards Development Organisation (IHTSDO)</a>.
                All rights reserved. </div></div><div id="getinvolved" class="sidebarbottomsection section">
            <h1>
                Get Involved</h1><img src="images/SFTheme/Image2a.jpg" width="258" height="105" title="'Get Involved' image"
                style="float: left;margin-bottom:5px" /> <p style="margin-top: 20px;">
                Join Microsoft Health CUI discussions: </p><ul style="margin-top: 0px;">
                <li>If you are a user interface designer, application developer or patient safety expert,
                    please access the <a target="_blank" href="http://www.codeplex.com/mscui/Thread/List.aspx"
                        title="Link to CodePlex discussion forum (New Window)">CodePlex discussion forum</a> </li><li>If you represent an organization that is interested in adopting the Microsoft Health
                    CUI for use in your healthcare applications, contact us at <a href="mailto:mscui@microsoft.com"
                        title="Mail to mscui@microsoft.com">mscui@microsoft.com</a> </li></ul></div></div></asp:Content>