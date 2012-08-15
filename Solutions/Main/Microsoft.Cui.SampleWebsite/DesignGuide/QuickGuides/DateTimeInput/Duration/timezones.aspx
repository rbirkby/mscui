<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="timezones.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.Duration.TimeZones" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">
		
            <div class="line">
				<div class="landscape">
					<img src="../images/dtc0048.png" alt="Controls for Start Date, Start Time and Duration, with a tooltip appearing below the duration input box" />
				</div>
				<div class="guidealone">
					<p class="number">D+Tc-0048</p>
					<p>
                    Where a time duration spans the change between GMT and BST, 
                    show a pop-up to inform the user that the system will 
                    automatically handle the data within the appropriate time zone
                    </p>
					<p class="recommended">Recommended</p>
					<p class="number">D+Tc-0049</p>
					<p>
                    Where a user adjusts time manually resulting in a time duration 
                    spanning a change between GMT and BST, show a pop-up to inform 
                    that user that the system will automatically adjust the data 
                    according to the appropriate time zone
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
			
		</div> 
	</div>
</asp:Content>
