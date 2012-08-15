<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="zones.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Structure.Zones" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                  
			<div class="line">
				<div class="landscape">
					<img src="../images/zone1.png" alt="A patient banner with the upper section, Zone 1, demarcated and labelled" />
				</div>
				<div class="guideleft">
					<p class="number">PAB-0001</p>
					<p>
                    The Patient Banner should consist of two zones, 
                    Zone 1 and Zone 2
                    </p>
					<p class="recommended">Recommended</p>
                </div>
                <div class="guidetext">
                    <p class="number">PAB-0073</p>
					<p>
                    The Patient Banner must include Zone 1
                    </p>
                    <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
			<div class="line">
				<div class="landscape">
					<img src="../images/zone2.png" alt="A patient banner with the lower section, Zone 2, demarcated and labelled" />
				</div>
                <div class="guideleft">
					<p class="number">PAB-0074</p>
					<p>The Patient Banner should include Zone 2</p>
					<p class="recommended">Recommended</p>
                </div>
                <div class="guidetext">
					<p class="number">PAB-0005</p>
					<p>Zone 2 consists of 5 sections</p>
					<p class="mandatory">Mandatory</p>
                 </div>
            </div>

        </div>
    </div>
</asp:Content>
