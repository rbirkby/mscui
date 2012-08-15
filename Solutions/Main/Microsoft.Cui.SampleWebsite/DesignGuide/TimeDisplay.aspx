<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="TimeDisplay.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.TimeDisplay"
    Title="Guidance - Time Input and Display" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Pdfs/Design Guidance -- Time Display.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Time Display documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span style="padding-bottom: 12px;">Design Guidance &ndash; Time&nbsp;Display (PDF&nbsp;format)</span>
            </asp:HyperLink>
            <hr/>
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="Pdfs/Design Guidance -- Date and Time Input.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Date and Time Input documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Date&nbsp;and&nbsp;Time Input (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <p>These Design Guides are two of nine documents relating to patient demographic data which have been adopted by the 
            Information Standards Board for Health and Social Care (ISB) 
            as a full standard for implementation in healthcare systems. 
            Further information can be found at 
            <a target="_blank" href="http://www.isb.nhs.uk/docs/cui" title="Link to the Information Standards Board website (New Window)">Information Standards Board</a>.</p>
        <h2>
            Introduction</h2>
        <p>
            The <i>Design Guidance &ndash; Time Display</i> document
            provides you with guidance and recommendations for implementing and displaying time
            in clinical applications. It enables unambiguous time display, while enhancing patient
            safety and clinical application usability, by:
        </p>
        <ul>
            <li>Eliminating ambiguity at noon and midnight</li>
            <li>Distinguishing between the last minute of the day and the first minute of the next
                day</li>
            <li>Representing clearly the individual elements of the time</li>
            <li>Displaying a period of time</li>
            <li>Displaying duration</li>
            <li>Displaying the clock using 24-hour notation unless otherwise stated</li>
            <li>Removing the confusion between AM and PM</li>
        </ul>
        <p>
            The <i>Design Guidance &ndash; Date and Time Input</i> document provides you with guidance and
            recommendations for entering time information in clinical applications.
        </p>
        <p>
            The accurate entry of time is a fundamental function within clinical applications,
            and, in many cases, has safety implications for patients within clinical and administrative
            processes. The guidance given enables unambiguous time entry, while enhancing patient
            safety and clinical application usability by:
        </p>
        <ul>
            <li>Providing a clear mechanism for guiding the user towards entry of an accurate value,
                therefore reducing human error</li>
            <li>Providing efficient input controls for the user to enter values in a fast and easy
                manner</li>
        </ul>
    </div>   
    <div class="last section">
        <h2>
            Summary</h2>
        <p>
            The <b>Time Display</b> guidance focuses on displaying the time element and includes recommendations
            for:
        </p>
        <ul>
            <li>Displaying exact and fuzzy time</li>
            <li>Displaying the time period</li>
            <li>Displaying the time duration</li>
            <li>Incorporating the date and time</li>
        </ul>
        <p>
            The <b>Time Input</b> guidance focuses on time input and includes recommendations for:
        </p>
        <ul>
            <li>Entering unknown, exact, fuzzy, or arithmetic shortcut values using free text</li>
            <li>Entering exact and fuzzy values using a &lsquo;spin&rsquo; control</li>
            <li>Entering the time elements independently</li>
            <li>Entering time periods</li>
            <li>Disambiguating free text time entries</li>
            <li>Displaying instructional text for the different types of time entry</li>
            <li>Labeling the time entry controls</li>
            <li>Combining date and time controls</li>
        </ul>
    </div>
    
    <div class="relatedResources">
        <div>
            <h2>Associated Quick Reference Guides</h2>
            <p>Quick Implementation Guides (QIG) present Design Guidance content in a more consumable, visual manner. Each QIG contains all design guidance points (mandatory and recommended) accompanied by example diagrams to illustrate their application.</p>  
            <p>A set of Crib Sheets also support Design Guidance interpretation, but concentrate on a specific areas of Guidance. Consequently users are able to download a crib sheet pertinent to a particular topic.</p>
            <p>These documents are offered by way of quick reference to already familiar guidance. However, remember that the full and definitive versions of the Design Guidance include further usage examples (showing both correct and incorrect application) and detail the rationale behind the guidance.</p>
        </div>
        <table>
            <colgroup>
                <col width="50%" />
                <col width="50%" />
            </colgroup>
            <thead>
                <tr>
                    <th>
                        Quick Implementation Guides
                    </th>
                    <th>
                        Crib Sheets
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <a href="QuickGuides/DateTimeDisplay" title="Link to Date and Time Display Quick Implementation Guide">Date and Time Display</a>                       
                    </td>
                    <td>
                        <a href="CribSheets/Crib Sheet for Time Display.pdf" target="_blank" title="Link to Time Display Crib Sheet">Time Display (PDF Format)</a>
                    </td>
                </tr>
                <tr>
                    <td>                        
                        <a href="QuickGuides/DateTimeInput" title="Link to Date and Time Input Quick Implementation Guide">Date and Time Input</a>
                    </td>
                    <td>
                        <a href="CribSheets/Crib Sheet for Time Input.pdf" target="_blank" title="Link to Time Input Crib Sheet">Time Input (PDF Format)</a>
                    </td>
                </tr>
                <tr>
                    <td />
                    <td>
                        <a href="CribSheets/Crib Sheet for Time Input - Duration.pdf" target="_blank" title="Link to Time Input - Duration Crib Sheet">Time Input - Duration (PDF Format)</a>
                    </td>
                </tr>
            </tbody>            
        </table>
    </div>  
    
</asp:Content>
