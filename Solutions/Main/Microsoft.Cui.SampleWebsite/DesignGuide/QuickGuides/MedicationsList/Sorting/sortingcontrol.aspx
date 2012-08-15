<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="sortingcontrol.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Sorting.SortingControl" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="landscape">
					<img src="../images/medv101.png" alt="A two step diagram with each step showing three medications: 1. Sorted by Start Date, with the most recent first 2. Sorted by Start Date with the most recent last. A mouse cursor is displayed over the &#39;Start Date&#39; column heading in each." />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-101</p>
					<p>
                    Allow the sort order of a list in the medications 
                    list to be changed by clicking on a column heading
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-102</p>
					<p>
                    Allow the sort order of a list in the Medications 
                    List View to be reversed by clicking on the column 
                    heading for the column with the active sort applied
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>      
</asp:Content>
