<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="currentcol.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Columns.Mandatory.CurrentCol" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="landscape">
					<img src="../../images/medv149.png" alt="Rows from a medication list with column headings: Drug Details, Status, Start Date"/>
				</div>
				<div class="guidealone">
					<p class="number">MEDv-149</p>
					<p>
                    When displaying current medications, provide a column 
                    that contains an initiation date (such as the date of 
                    the first planned administration). The examples on 
                    these pages show a Start Date column
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>			

        </div>
    </div>
</asp:Content>
