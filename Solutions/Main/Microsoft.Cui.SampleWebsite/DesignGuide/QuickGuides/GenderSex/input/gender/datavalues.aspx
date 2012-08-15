<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="datavalues.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.GenderSex.Input.Gender.DataValues" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           
            
			<div class="line">
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
					<p class="number">CGS-0002</p>
					<p>
                    The Current Gender values are as illustrated
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">CGS-0005</p>
					<p>
                    Do not abbreviate Current Gender data values
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">CGS-0006</p>
					<p>
                    Do not display the underlying coded representation 
                    of the Current Gender data values. For example, the 
                    standard code for &#39;Male&#39; may be the integer 1, but 
                    this number should not appear
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
                        
        </div>
    </div>
</asp:Content>
