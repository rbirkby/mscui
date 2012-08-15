<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="lookahead.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.LookAheadPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
    
        <div id="overview">
            <p>
            A look-ahead scroll bar (LASB) is a standard scroll 
            bar that is supplemented with notifications at the 
            top and bottom to indicate that there are items in 
            the list that are not currently visible:
            </p>
            <img src="images/lasb.png" alt="Diagram of a Medications List View with a look-ahead scroll bar. Callouts indicate: the look-ahead scroll bar, the two look-ahead notifications, the medications in the list and an alert in the lower look-ahead scroll bar notification" />
        </div>
        

        
    </div>
</asp:Content>