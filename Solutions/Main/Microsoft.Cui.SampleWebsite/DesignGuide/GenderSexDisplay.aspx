

<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="GenderSexDisplay.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.GenderSexDisplay"
    Title="Guidance - Gender and Sex Display" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Sex and Current Gender Input and Display.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Sex and Current Gender Input and Display documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Sex and Current Gender Input and Display (PDF format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <p>This Design Guidance is one of nine documents relating to patient demographic data which have been adopted by the 
            Information Standards Board for Health and Social Care (ISB) 
            as a full standard for implementation in healthcare systems. 
            Further information can be found at 
            <a target="_blank" href="http://www.isb.nhs.uk/docs/cui" title="Link to the Information Standards Board website (New Window)">Information Standards Board</a>.</p>
        <h2>
            Introduction</h2>
        <p>
            The <i>Design Guidance &ndash; Sex and Current Gender Input and Display </i>document provides you with guidance and recommendations for implementing
		and displaying gender and sex in clinical applications. It enables unambiguous gender and sex display, while enhancing 
		patient safety and clinical application usability by:
        </p>
        <ul>
            <li>Ensuring a consistent visual representation for gender and sex data values</li>
            <li>Providing a clear and readable format, with concise, intuitive labeling</li>
            <li>Avoiding numerical representation and specific classifications, which could cause offence</li>
        </ul>
    </div>
    <div class="last section">
        <h2>
            Summary
        </h2>
        <p>
            The guidance describes the display of:
        </p>
        <ul>
            <li>Sex, namely a patient&rsquo;s gender classification at the point of birth registration, which should be one of &lsquo;Male&rsquo;, 
		&lsquo;Female&rsquo; or &lsquo;Not Specified&rsquo; </li>
            <li>Gender, namely a patient&rsquo;s current gender classification, which should be one of &lsquo;Male&rsquo;, 
		&lsquo;Female&rsquo;, &lsquo;Not Specified&rsquo; or &lsquo;Not Known&rsquo; </li>
        </ul>
    </div>
    
    <div class="relatedResources">
        <div>
            <h2>Associated Quick Reference Guides</h2>
            <p>Quick Implementation Guides (QIG) present Design Guidance content in a more consumable, visual manner. Each QIG contains all design guidance points (mandatory and recommended) accompanied by example diagrams to illustrate their application.</p>  
            <p>A set of Crib Sheets is provided that also supports Design Guidance interpretation. Crib Sheets are single page visual summaries of the key guidance points. Each crib sheet focuses on a specific area of guidance, so some design guidance documents have more than one. Hence, you can download the crib sheet that is pertinent to the particular topic you are interested in.</p>
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
                        <a href="QuickGuides/GenderSex" title="Link to Sex and Current Gender Input and Display Quick Implementation Guide">Sex and Current Gender Input and Display</a>
                    </td>
                    <td>
                        <a href="CribSheets/Crib Sheet for Sex and Current Gender Display.pdf" target="_blank" title="Link to Sex and Current Gender Display Crib Sheet">Sex and Current Gender Display (PDF Format)</a>
                    </td>
                </tr>
                <tr>
                    <td />
                    <td>
                        <a href="CribSheets/Crib Sheet for Sex and Current Gender Input.pdf" target="_blank" title="Link to Sex and Current Gender Input Crib Sheet">Sex and Current Gender Input (PDF Format)</a>
                    </td>
                </tr>
            </tbody>            
        </table>
    </div>
    
</asp:Content>
