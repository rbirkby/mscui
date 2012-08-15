<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="context.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Structure.Context" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                   
			<div class="line">
				<div class="illustration">
					<img src="../images/pab0015.png" alt="Diagram of a full screen with the patient banner shown full width, below a Title Bar and a Global Navigation bar. The remaining screen space is labelled &#39;Information for a single patient&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0015</p>
					<p>
                    Do not display the Patient Banner on screens that 
                    contain information relating to more than one patient
                    </p>
					<p class="recommended">Recommended</p>
                    <p class="number">PAB-0013</p>
					<p>
                    Do not obscure the Patient Banner with other elements 
                    of the screen
                    </p>
					<p class="recommended">Recommended</p>
                 </div>
            </div>
            
        </div>
    </div>
</asp:Content>
