<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="layout.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Structure.Layout" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">   

    <div id="page">
        <div id="guidance">           
            
			<div class="midline">
				<div class="illustration">
					<img src="../images/pab0009.png" alt="Diagram of a full screen with the patient banner shown full width. An arrow points to a line between the patient banner and the Global Navigation Bar. Another arrow points to a padlock symbol in the top left hand corner of the patient banner" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0009</p>
					<p>
                    Display the Patient Banner at the top of the application window
                    </p>
					<p class="mandatory">Mandatory</p>
                    <p class="number">PAB-0011</p>
                    <p>
                        Display the Patient Banner in a fixed position, unmoveable by the user
                    </p>
                    <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="line">
                <div class="illustration">
					<img src="../images/pab0010.png" alt="Diagram of a full screen with the patient banner shown full width, below a Title Bar and a Global Navigation bar" />
                </div>
				<div class="guidetext">
                    <p class="number">PAB-0010</p>
                    <p>
                        Display the Patient Banner across the width of the screen rather than vertically
                    </p>
                    <p class="mandatory">Mandatory</p>
					<p class="number">PAB-0012</p>
					<p>
                    Display the Patient Banner so that it occupies 
                    the full width of the application window
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
         
        </div>
    </div>
</asp:Content>
