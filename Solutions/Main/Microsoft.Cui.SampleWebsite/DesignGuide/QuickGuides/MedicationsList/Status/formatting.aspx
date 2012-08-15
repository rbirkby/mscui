<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="formatting.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Status.Formatting" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="illustration">
					<img src="../images/medv162.png" alt="Single column from a list of medications, with the column heading &#39;Status&#39; and three cells below showing &#39;Not Started&#39;, &#39;Started&#39; and &#39;Started&#39; respectively" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-162</p>
					<p>
                    Ensure that all medications have a status 
                    value and the status cannot be blank
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-163</p>
					<p>
                    Limit status descriptions to short 
                    phrases, preferably no more than two words
                    </p>
					<p class="recommended">Recommended</p>
					<p class="number">MEDv-042</p>
					<p>
                    Display the status of each medication in bold
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>
