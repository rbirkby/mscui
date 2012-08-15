<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="country.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.NonUKInput.Country" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
   <div id="page">
        <div id="guidance">           
            
            <div class="midline">
				<div class="illustration">
					<img src="../../images/adr0056.png" alt="An editable combo box labelled &#39;Country&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0056</p>
					<p>
                    Use an editable drop-down combo box for country names
                    </p>
					<p class="recommended">Recommended</p>
				</div>

			</div>
            <div class="line">
				<div class="illustration">
					<img src="../../images/adr0057.png" alt="An combo box with the drop-down list displayed and showing an alphabetical list of countries with a scroll bar" />
				</div>
				<div class="guidetext">
					<p class="number">ADR-0057</p>
					<p>
                    Use the list of country names in 
                    <a href="http://www.iso.org/iso/country_codes/iso_3166_code_lists/english_country_names_and_code_elements.htm" target="_blank" title="Link to a list of country names on the ISO website (New Window)">
                    ISO 3166-1</a> for the country selector drop-down combo box
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">ADR-0058</p>
					<p>
                    Display the country names in alphabetic order
                    </p>
					<p class="recommended">Recommended</p>
                    </p>
					<p class="number">ADR-0059</p>
					<p>
                    Display the country names with left alignment
                    </p>
					<p class="recommended">Recommended</p>
                    </p>
				</div>              
			</div>
			
        </div>
    </div>
</asp:Content>