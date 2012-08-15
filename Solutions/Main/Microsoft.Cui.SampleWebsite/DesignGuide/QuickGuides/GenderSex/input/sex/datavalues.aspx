<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="datavalues.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.GenderSex.Input.Sex.DataValues" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
     <div id="page">
        <div id="guidance">            
            
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
					<p class="number">CGS-0010</p>
					<p>
                    The Sex values are as illustrated
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">CGS-0012</p>
					<p>
                    Sex data values must never be abbreviated
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">CGS-0014</p>
					<p>
                    The application must not display the underlying 
                    coded representation of the Sex data values. For 
                    example, the standard code for &#39;Male&#39; may be the 
                    integer 1, but this number should not appear
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
                      
        </div>
    </div>
</asp:Content>
