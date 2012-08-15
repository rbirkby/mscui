<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="dropdown.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.GenderSex.Input.Sex.DropDown" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
			<div class="line">
				<div class="illustration">
					<img src="../../images/cds0028.png" alt="A drop-down list with the the label &#39;Sex&#39;, no value displayed in the text box and the following values in the drop-down list: Male, Female, Not Known (selected), Indeterminate" />
				</div>
				<div class="guidetext">
					<p class="number">CGS-0028</p>
					<p>
                    Ensure that Sex controls have no value 
                    selected by default and no method of 
                    returning to this &#39;null&#39; state
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">CGS-0029</p>
					<p>
                    Use a single control for the Sex 
                    drop-down list box
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">CGS-0030</p>
					<p>
                    Ensure that the Sex drop-down list 
                    box is blank by default and does not 
                    contain a prompt
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>

        </div>
    </div>
</asp:Content>
