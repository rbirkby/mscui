<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="highlighting.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Selection.Highlighting" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="guideleft">
					<p class="number">MEDv-202</p>
					<p>
                    Ensure that there are no medications selected 
                    by default when a list is opened
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-122</p>
					<p>
                    Support click (or keyboard selection using the 
                    spacebar) to select a medication in the list
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
				<div class="landscape">
					<img src="../images/medv202.png" alt="A two step diagram showing: 1. A list of three medications 2. The same list with the first row selected (a darker background colour)" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-123</p>
					<p>
                    Clearly highlight selected medications in the medication list
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-124</p>
					<p>
                    Maintain the selection of a medication when switching between 
                    views of a patient&#39;s medications (such that a medication selected 
                    in a Medication List View is automatically selected when switching 
                    to the Drug Administration View)
                    </p>
					<p class="recommended">Recommended</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-125</p>
					<p>
                    Maintain the selection of a medication when applying or changing 
                    a grouping or a sort order and ensure that the selection remains 
                    visible
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>      
</asp:Content>
