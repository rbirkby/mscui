<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="structure.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.PrescriptionForms.Structure" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp1530.png" alt="Diagram of eight input boxes (drug, route, dose, frequency, give when..., administration times, first dose, duration) numbered sequentially from 1 to 8 and displayed so that the fourth, sixth and eighth have wrapped onto a new line" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1530</p>
                <p>
                Display fields (and controls for accessing individual 
                optional fields) in a consistent order for all 
                prescriptions
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-1540</p>
                <p>
                Minimise the number of different types of 
                input controls displayed in any one view
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

        </div>
    </div>    
</asp:Content>
