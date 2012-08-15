<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="expandcontrol.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Structure.ExpandControl" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
            <div class="midline">
                <div class="landscape">
    				<img src="../images/pab0004.png" alt="A patient banner with Zone 2 in the collapsed state" />
    			</div>
                <div class="guidealone">
					<p class="number">PAB-0004</p>
					<p>
                    Where Zone 2 is used, in the default display of the Patient 
                    Banner, show Zone 1 and Zone 2, with Zone 2 in the collapsed state
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
            </div>
                
            <div class="midline">
                <div class="illustration">
    				<img src="../images/pab0075.png" alt="An enlarged view of the right hand side of the patient banner, with the expand / collapse control indicated" />
    			</div>                
                <div class="guidetext">
					<p class="number">PAB-0075</p>
					<p>
                    Zone 2 must have expand and collapse capability
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">PAB-0006</p>
					<p>
                    All five sections in Zone 2 expand and collapse together
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
            </div>

            
			<div class="line">
				<div class="illustration">
					<img src="../images/pab0007.png" alt="An enlarged view of the right hand side of the patient banner, with the mouse cursor over one of the blank areas in Zone 2 and a tooltip that contains the text &#39;Click to expand for more detail&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0007</p>
					<p>
                    Display a tooltip when the mouse is positioned over 
                    Zone 2 while Zone 2 is collapsed, stating that Zone 2 
                    can be expanded
                    </p>
					<p class="mandatory">Mandatory</p>
                 </div>
            </div>
              
        </div>
    </div>
</asp:Content>
