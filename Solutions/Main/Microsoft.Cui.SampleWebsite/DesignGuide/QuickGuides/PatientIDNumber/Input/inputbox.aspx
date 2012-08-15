<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="inputbox.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientIDNumber.Input.InputBox" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           
            
			<div class="line">
				<div class="illustration">
					<img src="../images/num0010.png" alt="A text input box preceded by the label &#39;Patient ID Number&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">NUM-0010</p>
					<p>
                    Provide a single text input box for patient identification number entry
                    </p>
					<p class="recommended">Recommended</p>
					<p class="number">NUM-0011</p>
					<p>
                    Permit only one patient identification number to be entered in a 
                    patient identification number input box
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">NUM-0018</p>
					<p>
                    Do not permit input of old format and temporary patient identification numbers
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
         
        </div>
    </div>
</asp:Content>
