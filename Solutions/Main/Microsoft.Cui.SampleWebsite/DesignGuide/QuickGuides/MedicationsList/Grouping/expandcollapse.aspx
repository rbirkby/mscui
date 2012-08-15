<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="expandcollapse.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Grouping.ExpandCollapse" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="midline">
				<div class="landscape">
					<img src="../images/medv092.png" alt="Extract from a list of medications showing one collapsed group and one expanded group with a single row beneath. The first group heading has a button labelled &#39;&#43;&#39; followed by the text &#39;Oral (2)&#39;. A callout pointing to the button is labelled &#39;control for expanding a collapsed group&#39;" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-092</p>
					<p>
                    Provide controls for expanding and collapsing individual 
                    groups. Place these controls at the beginning of the group heading
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-089</p>
					<p>
                    When a group is collapsed, supplement the group heading with a 
                    number representing a count of medications within that group
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/medv196.png" alt="Extract from a list of medications in which a group heading is selected. A context menu containing two lines &#39;Collapse All Groups&#39; and &#39;Expand All Groups&#39; is displayed with the mouse cursor over the selected heading" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-196</p>
					<p>
                    Support the selection of group headings and the display 
                    of a context menu that includes options for collapsing 
                    and expanding all columns
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>      
</asp:Content>
