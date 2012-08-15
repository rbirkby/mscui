<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="inform.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Display.InForm" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
<div id="page">
        <div id="guidance">           
			<div class="midline">
				<div class="illustration">
					<img src="../images/inform.png" alt="An address with each element on a new line" />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0002</p>
					<p>
                    When displaying an address vertically, do not use a 
                    comma at the end of a line
                    </p>
					<p class="recommended">Recommended</p>
					<p class="number">ADR-0003</p>
					<p>
                    When displaying an address vertically, 
                    left-align the text for ease of reading
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>

			<div class="line">
				<div class="illustration">
					<img src="../images/adr0005.png" alt="An address in which the elements are separated with commas. Both the road name and the county have wrapped onto a new line so the address covers three lines" />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0005</p>
					<p>
                    Do not split an address element when wrapping 
                    an address across multiple lines
                    </p>
					<p class="recommended">Recommended</p>
                    <p class="note">
                        <strong>Note</strong>
                        'King's Road' has been wrapped without splitting the two words
                    </p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>