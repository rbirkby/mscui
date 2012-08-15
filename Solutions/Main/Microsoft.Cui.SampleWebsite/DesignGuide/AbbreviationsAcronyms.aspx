<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    CodeBehind="AbbreviationsAcronyms.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.AbbreviationsAndAcronyms"
    Title="Guidance - Abbreviations and Acronyms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Pdfs/Design Guidance Exploration -- Abbreviations and Acronyms.pdf"
                Target="_blank" ToolTip="Links to Design Guidance Exploration - Abbreviations and Acronyms documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance Exploration &ndash; Abbreviations and Acronyms (PDF&nbsp;format)</span>
            </asp:HyperLink>
            <hr />
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="Pdfs/Design Guidance Exploration -- Abbreviations and Acronyms in Free Text.pdf"
                Target="_blank" ToolTip="Links to Design Guidance Exploration - Abbreviations and Acronyms in Free Text documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" style="padding-top: 4px;"/>
                <span>Design Guidance Exploration &ndash; Abbreviations and Acronyms in Free Text (PDF&nbsp;format)</span>
            </asp:HyperLink>
        </div>
        <h1>
            Groundwork &ndash; Abbreviations and Acronyms</h1>
        <h2>
            Introduction
        </h2>
        <p>
            The design groundwork explorations in the <i>Design Guidance Exploration &ndash; Abbreviations
            and Acronyms</i> and <i>Design Guidance Exploration &ndash; Abbreviations and Acronyms in Free
            Text</i> documents provide you with design principles and guidance for displaying abbreviations
            and acronyms in clinical applications. These design explorations enable unambiguous
            display of acronyms and abbreviations in display fields and free input text, while
            enhancing patient safety and clinical application usability, by:
        </p>
        <ul>
            <li>Enabling consistent representation and handling of abbreviations and acronyms</li>
            <li>Minimizing the probability of confusion with similar terms</li>
            <li>Reducing the cognitive load of users, therefore making errors less likely</li>
        </ul>
    </div>
    <div class="update section">
        <label>
            Note:</label>
        <p>
            The ideas presented in these documents are for community preview and consultation
            only. Further design and patient safety assessments are required to finalize the
            content as CUI Design Guidance.
        </p>
    </div>
    <div class="last section">
        <h2>
            Summary
        </h2>
        <p>
            These design groundwork explorations focus on displaying acronyms and abbreviations, and include
            recommendations for:
        </p>
        <ul>
            <li>Displaying acronyms and abbreviations, including the application of capitalization
                and punctuation</li>
            <li>Resolving known dangerous or ambiguous abbreviations</li>
            <li>Providing potential abbreviation and acronym expansions for insertion</li>
            <li>Providing alerts for unexpanded abbreviations and acronyms</li>
            <li>Avoiding or displaying the meaning of acronyms and abbreviations</li>
        </ul>
    </div>
</asp:Content>
