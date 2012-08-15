<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="mandatory.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Columns.MandatoryPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="landscape">
					<img src="../images/medv147.png" alt="Rows from a medications list with callouts for each column heading. Drug Details: &#39;drug details&#39;, Status: &#39;status information&#39;, Start Date: &#39;an initiation date&#39;" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-147</p>
					<p>
                    Provide a column that contains status information, 
                    including information that defines whether the 
                    medication is &#39;current&#39; or &#39;past&#39;
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			    <div class="guidetext">
					<p class="number">MEDv-148</p>
					<p>
                    Provide a column that contains drug 
                    details according to 
                    <a href="../../../MedicationLine.aspx" title="Links to Guidance - Medication Line page">
                    Medication Line</a> guidance
                    </p>
					<p class="mandatory">Mandatory</p>
			    </div>
			</div>                       
            
        </div>
    </div>
</asp:Content>
