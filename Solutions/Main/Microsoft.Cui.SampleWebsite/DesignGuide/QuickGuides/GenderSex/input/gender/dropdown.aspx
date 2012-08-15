<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="dropdown.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.GenderSex.Input.Gender.DropDown" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
   <div id="page">
        <div id="guidance">           
            
			<div class="line">
				<div class="illustration">
					<img src="../../images/cds0026.png"  alt="A drop-down list with the the label &#39;Current Gender&#39;, the value &#39;Not Known&#39; shown in the text box and the following values in the drop-down list: Male, Female, Other Specific, Not Known (selected), Not Specified" />
				</div>
				<div class="guidetext">
					<p class="number">CGS-0025</p>
					<p>
                    Current Gender drop-down list box options 
                    are in the order illustrated
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">CGS-0026</p>
					<p>
                    Use a single drop-down list box for 
                    the Current Gender control
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">CGS-0027</p>
					<p>
                    Do not use a prompt for the Current Gender 
                    control, due to its default value of &#39;Not 
                    Known&#39;
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>
