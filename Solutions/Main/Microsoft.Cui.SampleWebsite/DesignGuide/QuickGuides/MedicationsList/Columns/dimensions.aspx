<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="dimensions.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Columns.Dimensions" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="landscape">
					<img src="../images/medv160.png" alt="Rows and column headings from a list of medications with a callout indicating the widest column (Drug Details), and a callout marking the full width of the rows labelled &#39;limited number of columns&#39;" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-159</p>
					<p>
                    Maintain the relative proportions of columns 
                    such that the Drug Details column is the widest
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-160</p>
					<p>
                    Avoid the need for horizontal scrolling by 
                    limiting the number of columns visible at 
                    any one time
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-161</p>
					<p>
                    Define minimum widths for all columns
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>

            
        </div>
    </div>
</asp:Content>
