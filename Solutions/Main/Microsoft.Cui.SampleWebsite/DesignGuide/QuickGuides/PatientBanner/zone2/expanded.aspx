<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="expanded.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Zone2.Expanded" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           

			<div class="line">
				<div class="landscape">
					<img src="../images/pabexpanded.png" alt="A patient banner with Zone 2 expanded. Within the five sections: the first shows an address, the second shows telephone numbers plus an email address and the last contains allergy information" />
				</div>
				<div class="guideleft">
					<p class="number">PAB-0026</p>
					<p>
                    Display the full address including the postcode, in 
                    the first section of the expanded Zone 2
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">PAB-0029</p>
					<p>
                    Display contact numbers and email addresses in 
                    the second section of the expanded Zone 2, in the 
                    following order: Home, Work, Mobile, Email
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
				<div class="guidetext">
					<p class="number">PAB-0065</p>
					<p>
                    Display each allergy propensity in the expanded section 
                    in Zone 2, along with the date when the record of that 
                    propensity was last updated
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
             </div>

        </div>
    </div>
</asp:Content>
