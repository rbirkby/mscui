<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/Navigational.master" AutoEventWireup="true"
    Codebehind="lifecycle.aspx.cs" Inherits="RoadmapLifecycle" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="navigationPageContent" runat="server">
    <div class="first section">
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>    
        <p>
            The diagram below illustrates the design and development process that is used to
            create Design Guidance and Toolkit controls for the Microsoft Health Common User
            Interface (CUI).
        </p>
        <p>
            The Microsoft Health CUI Design Guidance has been developed through a rigorous,
            iterative design process which involved clinicians and other healthcare professionals,
            experts in human-computer interaction, usability and user interface design throughout
            the entire process. Once a topic area has been broadly researched and understood
            during the Envisioning phase, it enters the detailed Planning and Design phase.
            As the user interface design evolves during this phase, input and feedback are regularly
            sought from clinicians and other potential end users, through a variety of structured
            research and usability testing techniques.
        </p>
        <p>
            Throughout the iterative design process, draft user interface designs are reviewed
            through patient safety assessments by clinicians and informatics experts within the UK NHS. 
	        The internal standards body for the NHS, the Information
            Standards Board for Health and Social Care (ISB), is also involved in reviewing elements of the Design Guidance and its
            supporting rationale, to ensure it has sound justification and sufficient rigour
            to enable it to become a standard across all NHS IT systems.
        </p>
        <p>
            Once the user interface designs have been usability-tested with clinicians and approved
            by the relevant review panels within the NHS, the development of detailed Design
            Guidance commences. Early versions of the Design Guidance and the Toolkit controls
            derived from this are known as Community Technology Previews (CTPs) and are released
            to Independent Software Vendors (ISVs) who are part of the NHS Connecting for Health
            programme. The ISVs provide the team with feedback on the designs and controls including,
            for example, the feasibility of incorporating them into the clinical applications.
            <img src="../Images/SFTheme/lifec.png" title="Lifecycle diagram" alt="This diagram shows a number of process steps, with arrows between them indicating sequence. The first step is Envisioning, which leads to Scoping. After Scoping, 4 steps happen iteratively - Design, Prototype, Stakeholder Reviews, and Usability Tests. 2 process paths lead from these steps - one leads to Design Guidance deliverables, the other leads (via 3 additional steps such as Control assessment) to the Controls deliverables." />
        </p>
    </div>
</asp:Content>
