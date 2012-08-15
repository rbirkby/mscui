<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="abbreviations.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.Duration.Abbreviations" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">
		
			<div class="midline">
				<div class="illustration">
                     <table class="datavalues">
                        <tr>
                            <td class="narrow">y</td>
                            <td>years</td>
                        </tr>
                        <tr>
                            <td class="narrow">m</td>
                            <td>months</td>
                        </tr>
                        <tr>
                            <td class="narrow">w</td>
                            <td>weeks</td>
                        </tr>
                        <tr>
                            <td class="narrow">d</td>
                            <td>days</td>
                        </tr>
                        <tr>
                            <td class="narrow">hr</td>
                            <td>hours</td>
                        </tr>
                        <tr>
                            <td class="narrow">min</td>
                            <td>minutes</td>
                        </tr>
                        <tr>
                            <td class="narrow">sec</td>
                            <td>seconds</td>
                        </tr>
                    </table>
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0034</p>
					<p>
                    Ensure that the illustrated set of duration unit abbreviations is supported
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/dtc0035.png" alt="An input box labelled &#39;Duration&#39; and containing the text &#39;5y 6m 7d 4hrs&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0035</p>
					<p>
                    Allow the set of duration unit abbreviations to be extended 
                    appropriately, for example, &#39;hrs&#39; as well as &#39;hr&#39;. Ensure that 
                    any additions are unique within the entire set
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
			
		</div>
	</div>
</asp:Content>
