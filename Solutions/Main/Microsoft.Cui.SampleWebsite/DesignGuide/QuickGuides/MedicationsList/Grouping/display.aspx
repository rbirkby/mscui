<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="display.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Grouping.Display" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="midline">
				<div class="landscape">
					<img src="../images/medv083.png" alt="Three rows with column headings" />
				</div>
				<div class="guidealone">
					<p class="number">MEDv-083</p>
					<p>
                    Present the Medications List View 
                    with no grouping active by default
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
                         
			<div class="line">
				<div class="landscape">
					<img src="../images/medv087.png" alt="A list containing three rows with column headings in which the first two rows appear below a group heading of &#39;Inhaled&#39; and the third row appears below a group heading of &#39;Oral&#39;" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-087</p>
					<p>
                    Retain the column sort order in the 
                    Medications List View when grouping is applied
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-191</p>
					<p>
                    Display groups expanded by default
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			           
        </div>
    </div>      
</asp:Content>
