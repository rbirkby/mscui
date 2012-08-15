<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="additionalinfo.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Status.AdditionalInfo" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="illustration">
					<img src="../images/medv164.png" alt="Single column from a list of medications with column heading &#39;Status&#39; and three cells below containing: 1. &#39;Not Started &#91;new line&#93; Verified&#39; 2. &#39;Started&#39; &#91;new line&#93; Not Verified 3. &#39;Started&#39; &#91;new line&#93;Verified" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-164</p>
					<p>
                    Allow status to be supplemented with 
                    additional information (such as pharmacy verified)
                    </p>
					<p class="recommended">Recommended</p>
					<p class="number">MEDv-165</p>
					<p>
                    Use the status description to differentiate 
                    between medications that have no recorded 
                    administration events and those that have
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>
