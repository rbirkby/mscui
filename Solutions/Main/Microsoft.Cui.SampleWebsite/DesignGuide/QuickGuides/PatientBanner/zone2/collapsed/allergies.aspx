<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="allergies.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Zone2.Collapsed.Allergies" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
           
			<div class="line">
				<div class="landscape">
					<img src="../../images/pab0031.png" alt="A patient banner with a callout labelled &#39;Allergy Propensity&#39; pointing to the final section of Zone 2 in which the text &#39;Known allergies&#39; is displayed" />
				</div>
				<div class="guideleft">
					<p class="number">PAB-0031</p>
					<p>
                    Optionally, display allergy propensity information 
                    in Zone 2 of the Patient Banner
                    </p>
					<p class="recommended">Recommended</p>
				</div>
				<div class="guidetext">
					<p class="number">PAB-0032</p>
					<p>
                    Reserve the fifth section of Zone 2 for the display of 
                    optional allergy propensity information
                    </p>
					<p class="mandatory">Mandatory </p>
				</div>
             </div>

        </div>
    </div>
</asp:Content>
