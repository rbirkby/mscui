<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="grouping.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.LookAhead.Grouping" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="landscape">
					<img src="../images/medv181.png" class="captioned" alt="Illustration of a list of medications with a scroll bar indicating that the lower half of the list is currently visisble. The list contains two collapsed groups (&#39Rectal (1)&#39; and &#39;Intravenous (3)&#39;) and one expanded group &#39;Inhaled&#39; below which one medication line is visible" />
					<p class="caption">
					An example in which the first group in the list 
					has been scrolled out of view.
					</p>
				</div>
				<div class="guideleft">
					<p class="number">MEDv-181</p>
					<p>
                    When grouping is applied, and there is a collapsed 
                    group out of view, display drug names in the look-ahead 
                    scroll bar for any drug that is out of view, irrespective 
                    of whether it is within a collapsed group or an expanded group
                    </p>
					<p class="recommended">Recommended</p>
					</div>
					<div class="guidetext">
					<p class="number">MEDv-185</p>
					<p>
                    Do not include additional text or formatting to 
                    indicate grouping in the look-ahead notifications 
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>
