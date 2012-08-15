<%@ Page Language="C#" MasterPageFile="~/Navigational.master" AutoEventWireup="true"
    CodeBehind="Schedule.aspx.cs" EnableViewState="false" Inherits="RoadmapSchedule"
    Title="Untitled Page" %>

<asp:Content ContentPlaceHolderID="navigationSpecificHeadTags" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="navigationPageContent" runat="server">
    <div class="first section">
        <SS:PageTitleControl runat="server"></SS:PageTitleControl>
        <p>
            Following a successful second phase of Microsoft Health Common User Interface development, culminating 
            in the publication of some of our most wide-ranging guidance documents and most ambitious controls, we 
            are currently planning the next steps for CUI. Just as soon as they're finalized, we'll publish details 
            of our plans here. Watch this space to see what our future activities will be.
        </p>
        <br />
        <p>
            In the meantime, we hope that you continue to gain insights into patient-safe design by referring to the 
            library of Microsoft Health CUI guidance documents on this site and continue to achieve productivity and 
            quality pluses by employing our control source code in your applications.
        </p>
        <br />
        <p>
            For now though, we're not going away completely. Please carry on contacting us via the 
            MSCUI email address <a href="mailto:mscui@microsoft.com" title="Mail to mscui@microsoft.com">mscui@microsoft.com</a> or via the 
            <a target="_blank" href="http://www.codeplex.com/mscui/Thread/List.aspx" title="Link to CodePlex discussion forum (New Window)">CodePlex discussion forum</a>.
        </p>
    </div>
</asp:Content>
