<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="deceased.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Zone1.Labels.Deceased" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
			<div class="line">
				<div class="landscape">
					<img src="../../images/pab0058.png" alt="Diagram in which a Date of Birth (Born &#39;14-Jul-1945&#39;), is displayed above a Date of Death (Died &#39;06-Dec-2006&#39;) which is followed by an Age at Death &#39;61y&#39;" />
				</div>
				<div class="illustration">
					&nbsp;
				</div>
				<div class="guideleft">
					<p class="number">PAB-0058</p>
					<p class="text">
                    Display the date of death below the date of birth
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">PAB-0057</p>
					<p>
                    Display the date of death along with its label
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">PAB-0038</p>
					<p>
                    Precede the date of death with the label &#39;Died&#39;
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
					<p class="number">PAB-0059</p>
					<p>
                    Display the age at death, preceded by its label, 
                    immediately after the date of death
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">PAB-0039</p>
					<p>
                    Precede the age at death with the label &#39;Age at Death&#39;
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>
