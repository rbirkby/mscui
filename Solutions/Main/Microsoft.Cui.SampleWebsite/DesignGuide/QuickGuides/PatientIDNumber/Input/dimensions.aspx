<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="dimensions.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientIDNumber.Input.Dimensions" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
   <div id="page">
        <div id="guidance">           

		<div class="midline">
				<div class="illustration">
					<img src="../images/num0012.png" alt="A text input box containing &#39;123 456 7890&#39; with a horizontal arrow indicating the width of the input box" />
				</div>
				<div class="guidetext">
					<p class="number">NUM-0012</p>
					<p>
                    Set the length of the patient identification number input 
                    box such that the patient identification number is visible in full
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>

		<div class="line">
				<div class="illustration">
					<img src="../images/num0013.png" alt="A text input box containing &#39;123 456 7890&#39; with a vertical arrow indicating the height of the text input box" />
				</div>
				<div class="guidetext">
					<p class="number">NUM-0013</p>
					<p>
                    Set the height of the patient identification number input box to 
                    the largest character height in the currently 
                    active display font, taking the user&#39;s settings 
                    into account
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
			
        </div>
    </div>
</asp:Content>
