<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="preferredname.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Zone1.Contents.PreferredName" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           
            
            <div class="midline">
            	<div class="illustration">
					<img src="../../images/pab0054.png" alt="Extract from a patient banner with the label &#39;Preferred Name&#39; and value &#39;Rama&#39; displayed below the patient&#39; name" />
				</div>
                <div class="guidetext">
                    <p class="number">PAB-0020</p>
                    <p>Display the preferred name if available</p>
                    <p class="recommended">Recommended</p>
					<p class="number">PAB-0054</p>
					<p>
                    Display the patient&#39;s preferred name, if available, 
                    immediately below the family name
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
            </div>

			<div class="midline">
				<div class="illustration">
					<img src="../../images/pab0061.png" alt="Extract from a patient banner with a vertical line showing that both the patient name and the preferred name (with label) are aligned to the left" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0061</p>
					<p>
                    Display the patient&#39;s preferred name, if available, 
                    immediately below the given name, with both items 
                    <span class="nowrap">left-aligned</span>
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
			<div class="line">
				<div class="illustration">
					<img src="../../images/pab0062.png" alt="Extract from a patient banner showing the name &#39;CHANDRASEKHAR, Subramanyan (Mr)&#39; left aligned and centred vertically" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0062</p>
					<p>
                    When a patient&#39;s preferred name is not available, 
                    the patient's name must be centred vertically and 
                    <span class="nowrap">left-aligned</span> in Zone 1
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>   

        </div>
    </div>
</asp:Content>
