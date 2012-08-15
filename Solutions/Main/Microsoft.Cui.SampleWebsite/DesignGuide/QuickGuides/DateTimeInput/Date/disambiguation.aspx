<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="disambiguation.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.Date.Disambiguation" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">		

			<div class="line">
				<div class="landscape">
					<img src="../images/dtc0005.png" alt="A two step diagram showing: 1. A date control containing the text &#39;05&#047;04&#047;2&#39; and a text cursor; 2. The control containing &#39;05&#047;04&#047;2009&#39; and a tooltip with the message &#39;Did you mean 05-Apr-2009 or 04-May-2009 or different date?&#39;" />
				</div>

				<div class="guideleft">
					<p class="number">D+Tc-0005</p>
					<p>Provide disambiguation of any free text date input</p>
					<p class="mandatory">Mandatory</p>
					<p class="number">D+Tc-0038</p>
					<p>
                    Provide the facility for a user to disambiguate a date entered 
                    via the date control
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
                
                <div class="guidetext">
					<p class="number">D+Tc-0039</p>
					<p>
                    Display a message dialog box with appropriate instructional 
                    text if the data is ambiguous or incomplete
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">D+Tc-0040</p>
					<p>
                    Display a maximum of two suggestions based on the data entered, 
                    plus an option to re-enter the value in the input field. Selection 
                    of a suggested value enters that value into the control
                    </p>
					<p class="recommended">Recommended</p>
				</div>            
			</div>

		</div>
	</div>
</asp:Content>
