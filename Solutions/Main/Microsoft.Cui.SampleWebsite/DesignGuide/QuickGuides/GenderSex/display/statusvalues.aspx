<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="statusvalues.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.GenderSex.Display.StatusValues" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
			<div class="midline">
				<div class="illustration">
                    <table class="datavalues">
                        <tr><td>Male</td></tr>
                        <tr><td>Female</td></tr>
                        <tr><td>Other Specific</td></tr>
                        <tr><td>Not Known</td></tr>
                        <tr><td>Not Specified</td></tr>
                    </table>
				</div>
				<div class="guidetext">
					<p class="number">CGS-0003</p>
					<p>
                    The Current Gender status is one of the illustrated values
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
			<div class="line">
				<div class="illustration">
                    <table class="datavalues">
                        <tr><td>Male</td></tr>
                        <tr><td>Female</td></tr>
                        <tr><td>Not Known</td></tr>
                        <tr><td>Indeterminate</td></tr>
                    </table>
				</div>
				<div class="guidetext">
					<p class="number">CGS-0011</p>
					<p>
                    The Sex status must only contain one of the illustrated values
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
        </div>
    </div>
</asp:Content>
