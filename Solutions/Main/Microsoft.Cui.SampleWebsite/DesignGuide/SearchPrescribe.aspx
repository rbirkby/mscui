<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    CodeBehind="SearchPrescribe.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.SearchPrescribe"
    Title="Guidance - Search and Prescribe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink runat="server" NavigateUrl="Pdfs/Design Guidance -- Search and Prescribe.pdf"
                Target="_blank" ToolTip="Links to Design Guidance - Search and Prescribe documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance &ndash; Search and Prescribe (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <h2>
            Introduction
        </h2>
        The <i>Design Guidance &ndash; Search and Prescribe</i> document provides you with
        guidance and recommendations for the design of solutions whose aim is to support
        searching for and prescribing individual medications for single patients. It describes
        the area of focus, lists mandatory and recommended guidance points with usage examples
        and explains the rationale behind the guidance. This work describes a Prescribing
        Model that is designed to minimize the risk when prescribing (or &lsquo;ordering&rsquo;)
        medications in hospital and acute care. It advocates a dose-based prescribing system in which:
        <ul>
            <li>A prescriber specifies a drug by its generic name plus dose, route and frequency</li>
            <li>A nurse then selects the correct quantity of an actual product to give to the patient
            </li>
        </ul>
        The Prescribing Model aims to provide the safeguards needed to ensure dose-based
        prescribing results in prescriptions without any unsafe ambiguities for those giving
        medicines to patients.
    </div>
    <div class="last section">
        <h2>
            Summary</h2>
        <p>
            The guidance includes recommendations for:</p>
        <ul>
            <li>Text entry searching for drugs</li>
            <li>Differentiating between generic drugs and brand names in lists</li>
            <li>Displaying, ordering and formatting search results lists</li>
            <li>Navigating within and between search results lists</li>
            <li>Interacting with search result list items</li>
            <li>Indicating non-formulary drugs in search results lists</li>
            <li>Presentation of lists of predefined prescriptions</li>
            <li>Structure and layout of the prescription form</li>
            <li>Presentation of required and optional attributes</li>
            <li>Guidance for efficiently prescribing commonly prescribed medications</li>
            <li>Guidance for prescribing less commonly prescribed and more detailed medications</li>
            <li>Selection from a predefined set of administration times or the definition of an
                individual administrative event for a once only medication</li>
            <li>Guidance for supporting the review of a prescription before it is authorized</li>
        </ul>
    </div>
    
    <div class="relatedResources">
        <div>
            <h2>Associated Quick Reference Guides</h2>
            <p>Quick Implementation Guides (QIG) present Design Guidance content in a more consumable, visual manner. Each QIG contains all design guidance points (mandatory and recommended) accompanied by example diagrams to illustrate their application.</p>  
            <p>These documents are offered by way of quick reference to already familiar guidance. However, remember that the full and definitive versions of the Design Guidance include further usage examples (showing both correct and incorrect application) and detail the rationale behind the guidance.</p>
        </div>
        <table>
            <colgroup>
                <col width="100%" />
            </colgroup>
            <thead>
                <tr>
                    <th>
                        Quick Implementation Guides
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <a href="QuickGuides/SearchPrescribe/default.aspx" title="Link to Search and Prescribe Quick Implementation Guide">Search and Prescribe</a>
                    </td>
                </tr>                
            </tbody>            
        </table>
    </div> 
    
</asp:Content>
