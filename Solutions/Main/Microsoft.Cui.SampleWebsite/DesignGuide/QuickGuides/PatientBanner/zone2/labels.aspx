<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="labels.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Zone2.LabelsPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           
            
			<div class="midline">
				<div class="illustration">
					<img src="../images/pab0040.png" alt="An example of Section 1 of Zone 2 of a patient banner showing the text&#39;Address 340 Gloucester R...&#39; with a callout pointing to the label &#39;Address&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0040</p>
					<p>
                    Precede the address displayed in the title of the first 
                    section in Zone 2 with the label &#39;Address&#39;
                    </p>
					<p class="recommended">Recommended</p>
                </div>
            </div>
            
 			<div class="midline">
				<div class="illustration">
					<img src="../images/pab0041.png" alt="An example of Section 2 of Zone 2 of a patient banner showing the text &#39;Phone and email 020 8123 4567&#39; with a callout pointing to the label &#39;Phone and email&#39;" />
				</div>
				<div class="guidetext">              
					<p class="number">PAB-0041</p>
					<p>
                    Precede the single phone number displayed in the title 
                    of the second section in Zone 2 with the label &#39;Phone 
                    and email&#39;
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
            
			<div class="midline">
				<div class="illustration">
					<img src="../images/pab0027.png" alt="An example of an expanded Section 1 in which the label &#39;Usual address&#39; and an address are displayed" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0027</p>
					<p>
                    Precede the full address with the label 
                    &#39;Usual address&#39; or &#39;Temporary address&#39; as appropriate
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
            </div>
            
			<div class="midline">
				<div class="illustration">
					<img src="../images/pab0030.png" alt="An example of an expanded Section 2 in which each line starts with these labels: &#39;Home&#39;, &#39;Work&#39;, &#39;Mobile&#39; and &#39;Email&#39; respectively" />
				</div>
				<div class="guidetext">
                <p class="number">PAB-0030</p>
					<p>
                    Precede each contact number and email address with 
                    the label &#39;Home&#39;, &#39;Work&#39;, 
                    &#39;Mobile&#39;, or &#39;Email&#39;, as 
                    appropriate
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
             </div>            
            
        </div>
    </div>
</asp:Content>
