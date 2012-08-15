<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="datavalues.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.Input.UKInput.DataValues" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            <div class="line">
                <div class="illustration">
					<table class="datavalues">
                        <tr>
                            <td class="narrow">Aa</td>
                            <td>Uppercase and lowercase letters</td>
                        </tr>
                        <tr>
                            <td class="narrow">0-9</td>
                            <td>Numbers 0 to 9</td>
                        </tr>
                        <tr>
                            <td class="narrow">.</td>
                            <td>The full stop</td>
                        </tr>
                        <tr>
                            <td class="narrow">&#47;</td>
                            <td>Forward slash</td>
                        </tr>
                        <tr>
                            <td class="narrow">,</td>
                            <td>Comma</td>
                        </tr>
                        <tr>
                            <td class="narrow">:</td>
                            <td>Colon</td>
                        </tr>
                        <tr>
                            <td class="narrow">&#39;</td>
                            <td>Apostrophe</td>
                        </tr>
                        <tr>
                            <td class="narrow">&nbsp;</td>
                            <td>Space</td>
                        </tr>
                        <tr>
                            <td class="narrow">&#45;</td>
                            <td>Hyphen</td>
                        </tr>
                    </table>
                </div>
                <div class="guidetext">
                    <p class="number">
                        ADR-0021</p>
                    <p>
                        Permit the illustrated characters in an address
                    </p>
                    <p class="recommended">Recommendation</p>
                </div>

            </div>
        </div>
    </div>
</asp:Content>