<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="headings.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Columns.Headings" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="landscape">
					<img src="../images/medv155.png" alt="Column headings for a medications list: Drug Details, Status, Start Date, End Date" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-155</p>
					<p>
                    Label columns with text that describes the contents 
                    unambiguously and succinctly (such as, &#39;Status&#39;, 
                    &#39;Date Prescribed&#39; or &#39;First Administration&#39;)
                    </p>
					<p class="recommended">Recommended</p>
			    </div>
			    <div class="guidetext">
					<p class="number">MEDv-156</p>
					<p>
                    Use a unique heading for each column
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>

        </div>
    </div>
</asp:Content>
