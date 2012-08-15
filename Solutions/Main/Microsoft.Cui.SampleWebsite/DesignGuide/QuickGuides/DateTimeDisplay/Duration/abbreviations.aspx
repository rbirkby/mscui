<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="abbreviations.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeDisplay.Duration.Abbreviations" %>
    
<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">	
				
			<div class="midline">
				<div class="illustration">
                    <table class="datavalues">
                        <tr>
                            <td>hr</td>
                            <td>hours</td>
                        </tr>
                        <tr>
                            <td>min</td>
                            <td>minutes</td>
                        </tr>
                        <tr>
                            <td>sec</td>
                            <td>seconds</td>
                        </tr>
                    </table>
                    <table class="leftdata">
                        <tr>
                            <td>y</td>
                            <td>years</td>
                        </tr>
                        <tr>
                            <td>m</td>
                            <td>months</td>
                        </tr>
                        <tr>
                            <td>w</td>
                            <td>weeks</td>
                        </tr>
                        <tr>
                            <td>d</td>
                            <td>days</td>
                        </tr>
                    </table>
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0017</p>
					<p>Display durations using the illustrated values, as appropriate</p>
					<p class="mandatory">Mandatory</p>
					<p class="number">D+Tb-0033</p>
					<p>Ensure that the illustrated minimal set of duration unit abbreviations is supported</p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/dtb0034.png" alt="8hrs 5mins" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tb-0034</p>
					<p>
					Allow the set of duration unit abbreviations to be extended appropriately, 
					for example, 'hrs' as well as 'hr'.  Ensure that any additions are unique 
					within the entire set 
					</p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
			
		</div>
	</div>
</asp:Content>