<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="pastcol.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Columns.Mandatory.PastCol" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">		
                        
			<div class="line">
				<div class="landscape">
					<img src="../../images/medv150.png" alt="Extract of a Medications List View showing past medications and the following columns: Status, Drug Details, End Date" />
				</div>
				<div class="guidealone">
					<p class="number">MEDv-150</p>
					<p>
                    When displaying past medications, provide a column 
                    that contains a stop date (such as the date of the 
                    last administration, or the date that the medication 
                    was discontinued). The examples on these pages 
                    show an End Date column
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>                        
            
        </div>
    </div>
</asp:Content>
