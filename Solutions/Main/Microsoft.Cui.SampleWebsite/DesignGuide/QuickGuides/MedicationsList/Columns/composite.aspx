<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="composite.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Columns.Composite" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="illustration">
					<img src="../images/medv027.png" alt="A single column from a list of medications, with column heading &#39;Review and End Dates&#39; and two cells below the first of which contains &#39;Review 06-Jun-2010 &#91;new line&#93; End 12-Jun-2010&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-027</p>
					<p>
                    Allow columns to contain more than 
                    one attribute for a single medication
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-157</p>
					<p>
                    When combining two attributes that 
                    have the same data type (such as dates), 
                    include labels for both attributes in 
                    the column heading
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-158</p>
					<p>
                    When combining two attributes that have the same data 
                    types (such as dates), include labels for both attributes 
                    within the cell
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>
