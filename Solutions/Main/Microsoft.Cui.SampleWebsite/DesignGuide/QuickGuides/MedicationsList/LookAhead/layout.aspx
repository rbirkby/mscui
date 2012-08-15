<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="layout.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.LookAhead.Layout" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="midline">
				<div class="illustration">
					<img src="../images/medv179.png" alt="Wireframe diagram of a list of medications with callouts indicating the full width row spaces at the top and bottom of the list" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-179</p>
					<p>
                    When displaying a LASB, reserve a space at 
                    the top and bottom of the list for look-ahead 
                    notifications
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
                        
			<div class="line">
				<div class="illustration">
					<img src="../images/medv180.png" alt="Illustration of the top right hand corner of a list of medications with a callout labelled &#39;pale colour&#39; indicating the space to the left of a look-ahead notification" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-180</p>
					<p>
                    Use a pale solid background colour for the 
                    space reserved for <span class="nowrap">look-ahead</span> 
                    notifications that is sufficient to distinguish the space 
                    from the background of the list
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>            
        </div>
    </div>
</asp:Content>
