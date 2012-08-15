<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="collapsed.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Zone2.CollapsedPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
			<div class="line">
				<div class="landscape">
					<img src="../images/pab0017.png" alt="A patient banner with a callout labelled &#39;Contact Details&#39; that points to the first two areas of Zone 2" />
				</div>
				<div class="guideleft">
					<p class="number">PAB-0017</p>
					<p>
                    For a patient who is alive, the Patient Banner additionally 
                    displays contact details (comprising the address and phone 
                    numbers) and the patient&#39;s age
                    </p>
					<p class="mandatory">Mandatory</p>
                    <p class="note">
                    <strong>Note</strong> See 
                    <a href="../appendix.aspx" title="Links to the Appendix page">
                    Appendix</a> 
                    for the age display formatting rules
                    </p>
                </div>
                <div class="guidetext">
					<p class="number">PAB-0025</p>
					<p>
                    Display as much of the address as possible in a single line, 
                    in the title of the first section in Zone 2, displaying an 
                    ellipsis to show incomplete display of the address
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">PAB-0028</p>
					<p>
                    Display as much of a single phone number as possible in a 
                    single line, in the title of the second section in Zone 2, 
                    displaying an ellipsis to show incomplete display of the 
                    phone number
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>           

        </div>
    </div>
</asp:Content>
