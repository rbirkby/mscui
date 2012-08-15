<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="combining.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Grouping.Combining" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="midline">
				<div class="landscape">
					<img src="../images/medv198.png" alt="Diagram of three collapsed groups: 1. Analgesic (2) 2. Analgesic;Non-steroidal Anti-inflammatory (1) 3. Non-steroidal Anti-inflammatory (1). A callout labelled &#39;a combined group&#39; indicates the second group heading" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-198</p>
					<p>
                    Display each medication in only one group (do 
                    not duplicate medications so that they can be 
                    displayed in more than one group)
                    </p>
					<p class="mandatory">Mandatory</p>
			    </div>
			    <div class="guidetext">
					<p class="number">MEDv-197</p>
					<p>
                    When one or more medications belong to more than one group 
                    (such as analgesic and <span class="nowrap">non-steroidal</span> 
                    <span class="nowrap">anti-inflammatory</span>), 
                    create a new group and label it with the group names combined 
                    (such as &#39;Analgesic; 
                    <span class="nowrap">Non-steroidal</span> 
                    <span class="nowrap">Anti-inflammatory&#39;)</span>
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="landscape">
					<img src="../images/medv199.png" alt="A diagram of three collapsed groups. An arrow on the left indicates a sort order from top to bottom, a callout marks the semi-colon in the second group name: Analgesic; Non-steroidal Anti-inflammatory (1)" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-199</p>
					<p>
                    When combining group names, display the names in the 
                    same order as they would appear in a list that is 
                    sorted by that attribute
                    </p>
					<p class="mandatory">Mandatory</p>
			    </div>
			    <div class="guidetext">
					<p class="number">MEDv-200</p>
					<p>
                    When combining group names, separate the labels 
                    with a <span class="nowrap">semi-colon</span>
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
            
        </div>
    </div>      
</asp:Content>
