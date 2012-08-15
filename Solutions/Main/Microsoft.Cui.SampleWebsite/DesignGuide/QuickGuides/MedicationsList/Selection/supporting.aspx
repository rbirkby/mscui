<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="supporting.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Selection.Supporting" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="illustration">
					<img src="../images/medv126.png" alt="Outline diagram of a list of medications with three medications selected (a darker background colour): the first two are adjacent and the second is lower down in the list" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-126</p>
					<p>
                    Support the selection of multiple items using 
                    CTRL and click for discrete selections, and 
                    SHIFT and click for contiguous selections
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-127</p>
					<p>
					Support 
					<span class="nowrap">keyboard-only</span> equivalents 
					such as SHIFT and 
					arrow key for contiguous selection and the CTRL and 
					SPACEBAR to toggle select and deselect when making 
					<span class="nowrap">non-contiguous</span> selections
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-203</p>
					<p>
                    When an action is applied to more than one 
                    medication, display a summary of the selected 
                    medications before allowing the user to complete 
                    the action
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>      
</asp:Content>
