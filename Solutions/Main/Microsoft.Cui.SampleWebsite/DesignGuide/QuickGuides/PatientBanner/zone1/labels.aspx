<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="labels.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Zone1.LabelsPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
			<div class="midline">
				<div class="landscape">
					<img src="../images/pab0050.png" alt="Zone 1 of a patient banner with callouts for the patient name &#39;no label&#39; and &#39;label&#39; for each of: Preferred name, Born, Gender, NHS No." />
				</div>
				<div class="guideleft">
					<p class="number">PAB-0050</p>
					<p>
                    Do not include labels for the patient name elements and the title
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">PAB-0037</p>
					<p>
                    Precede the preferred name with the label &#39;Preferred name&#39;
                    </p>
					<p class="recommended">Recommended</p>
                </div>
                <div class="guidetext">
					<p class="number">PAB-0035</p>
					<p>
                    Precede the gender with the label &#39;Gender&#39;
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">PAB-0033</p>
					<p>
                    Precede the date of birth with the label &#39;Born&#39;
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">PAB-0036</p>
					<p>
                    Precede the patient identification number with 
                    an appropriate label, for example, &#39;NHS No.&#39;
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
            </div>
                
			<div class="line">
				<div class="landscape">
					<img src="../images/pab0047.png" alt="Expanded section of the right hand side of a patient banner with a tooltip over the label &#39;Gender&#39; and a definition displayed in the tooltip" />
				</div>
				<div class="illustration">
					&nbsp;
				</div>
				<div class="guidetext">
					<p class="number">PAB-0047</p>
					<p>
                    For each label in Zone 1, provide a definition and a means to 
                    access the definition, for example by a tooltip
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>
