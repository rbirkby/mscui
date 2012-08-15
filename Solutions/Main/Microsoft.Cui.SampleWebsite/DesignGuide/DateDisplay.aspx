<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="DateDisplay.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.DateDisplay"
    Title="Guidance - Date Input and Display" %>

<asp:Content ID="dateDisplayContent" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Pdfs/Design Guidance -- Date Display.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Date Display documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span style="padding-bottom: 12px;">Design Guidance &ndash; Date&nbsp;Display (PDF&nbsp;format)</span>
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
            The <i>Design Guidance &ndash; Date Display</i> document
            provides you with guidance and recommendations for displaying single, precise dates
            in clinical applications. It enables unambiguous date display, while enhancing patient
            safety and clinical application usability, by eliminating confusion between day,
            month and year elements.
        </p>
        <p>
            Dates can be displayed in short and long formats. The short format is commonly used
            as it prevents international users from confusing day and month elements and presents
            the dates in a concise, easily readable and unambiguous form.
        </p>
        <p>
            The <i>Design Guidance &ndash; Date and Time Input</i> document provides you with guidance
            and recommendations for entering date information in clinical applications.
        </p>
        <p>
            The accurate entry of dates is a fundamental function within clinical applications,
            and, in many cases, has safety implications for patients within clinical and administrative
            processes. The guidance given enables unambiguous date entry, while enhancing patient
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
            The <b>Date Display</b> guidance focuses on displaying the date element and includes recommendations
            for:
        </p>
        <ul>
            <li>Displaying the short date format (used where the clinical application displays patient-related
                dates)</li>
            <li>Displaying the long date format (used within application output for non-clinical
                readers)</li>
        </ul>
        <p>
            The <b>Date Input</b> guidance focuses on date input and includes recommendations for:
        </p>
        <ul>
            <li>Entering unknown, exact, fuzzy, or arithmetic shortcut values using free text</li>
            <li>Entering exact and fuzzy values using a calendar</li>
            <li>Editing the date elements independently</li>
            <li>Displaying instructional text for different types of date entry</li>
            <li>Labeling date entry controls</li>
            <li>Disambiguating free text date entries</li>
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
                        <a href="CribSheets/Crib Sheet for Date Display.pdf" target="_blank" title="Link to Date Display Crib Sheet">Date Display (PDF Format)</a>
                    </td>
                </tr>
                <tr>
                    <td>                        
                        <a href="QuickGuides/DateTimeInput" title="Link to Date and Time Input Quick Implementation Guide">Date and Time Input</a>
                    </td>
                    <td>
                        <a href="CribSheets/Crib Sheet for Free Text Date Input.pdf" target="_blank" title="Link to Free Text Date Input Crib Sheet">Free Text Date Input (PDF Format)</a>
                    </td>
                </tr>
                <tr>
                    <td />
                    <td>
                        <a href="CribSheets/Crib Sheet for Calendar Date Input.pdf" target="_blank" title="Link to Calendar Date Input Crib Sheet">Calendar Date Input (PDF Format)</a>
                    </td>
                </tr>
            </tbody>            
        </table>
    </div>  
    
</asp:Content>
