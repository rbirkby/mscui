<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="inputboxes.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.NonUKInput.InputBoxes" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
 <div id="page">
        <div id="guidance">            
            <div class="line">
				<div class="illustration">
					<table class="datavalues">
                        <tr>
                            <td class="lessnarrow">1 editable combo box</td>
                            <td class="wrap">For country selection</td>
                        </tr>
                        <tr>
                            <td class="lessnarrow">4 boxes</td>
                            <td>For all details up to and including the street name</td>
                        </tr>
                        <tr>
                            <td class="lessnarrow">1 box</td>
                            <td>For input of the town or city</td>
                        </tr>
                        <tr>
                            <td class="lessnarrow">1 box</td>
                            <td>For input of the postal code</td>
                        </tr>
                    </table>
				</div>
				<div class="guidetext">
					<p class="number">ADR-0050</p>
					<p>
                    Provide these text input boxes, in the stated order, 
                    for input of a <span class="nowrap">non-UK</span> address
                    </p>
					<p class="recommended">Recommended</p>
				</div>

			</div>
        </div>
    </div>
</asp:Content>