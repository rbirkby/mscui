<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="gridlines.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Structure.Gridlines" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="landscape">
					<img src="../images/medv021.png" alt="Rows from a Medications List View with a callout indicating the subtle gridlines between rows" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-021</p>
					<p>
                    Avoid the use of strong grids and strong vertical lines 
                    (use subtle methods to support distinguishing between 
                    rows in the list)
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-022</p>
					<p>
                    Use subtle alternate row shading
                    </p>
					<p class="recommended">Recommended</p>
					<p class="number">MEDv-143</p>
					<p>
                    Use at least alternate row shading or lines between rows
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-144</p>
					<p>
                    When using alternate row shading, ensure that colour and 
                    brightness of the background does not interfere with the 
                    readability of the foreground text
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-145</p>
					<p>
                    Supplement alternate shading with 1 point pale lines between rows
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>
