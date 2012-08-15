<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" CodeBehind="prescriptionforms.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.PrescriptionFormsPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            The majority of prescriptions will be completed by selecting a template prescription 
            and entering values for any remaining required fields:
            </p>
            <img src="images/shortform.png" alt="Diagram of a prescrption form with a few fields" />
            <p>
             In some cases, additional specific fields or more detailed prescriptions may be 
             needed and forms with a large number of fields would be required to support these:
            </p>
            <img src="images/longform.png" alt="Diagram of a prescription form with many fields" />
        </div>
    </div>       
</asp:Content>
