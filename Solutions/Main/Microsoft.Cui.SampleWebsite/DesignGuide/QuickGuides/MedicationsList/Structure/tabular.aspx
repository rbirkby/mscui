<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="tabular.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Structure.Tabular" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="landscape">
					<img src="../images/medv020.png" alt="Rows from a Medications List View with details aligned into 3 columns. The first column has a callout labelled &#39;composite column&#39;" />
				</div>
			    <div class="guideleft">
					<p class="number">MEDv-020</p>
					<p>
                    Present medications as lines of text within rows 
                    in a tabular format, where each row represents one medication
                    </p>
					<p class="mandatory">Mandatory</p>
			    </div>
				<div class="guidetext">
					<p class="number">MEDv-141</p>
					<p>
                    Use composite columns to minimise the display of blank cells 
                    for some rows (that is, avoid placing each individual data 
                    point in a separate column)
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>
