<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="cascadinglists.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.CascadingListsPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            After a drug has been selected, either from the Quick List or a list of search results, 
            a number of other attributes are needed to determine the type of medication that is 
            being prescribed. Cascading lists are a means of facilitating the definition of those 
            attributes.
            </p>
            <img src="images/cascading.png" alt="Diagram with four outlines representing lists with numbers and arrows implying a sequence from left to right: 1 is labelled Drug Names, 2, 3 and 4 are labelled Cascading Lists" />
        </div>
    </div>      
</asp:Content>
