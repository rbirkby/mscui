<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="simviews.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.Context.SimViews" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0020.png" alt="Outline diagram of a screen with a Patient Banner at the top and the remainder of the screen split vertically with &#39;Prescribing Area&#39; on the left and &#39;Medications List&#39; on the right" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0020</p>
                <p>
                Allow a patient&#39;s current medications to be accessed whilst prescribing, 
                preferably by allowing current medications to be displayed simultaneously
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0030.png" alt="Outline diagram of a screen with Patient Banner at the top, a rectangular area labelled &#39;Prescribing Area&#39; which partially obscures a rectangle behind labelled &#39;Medications List&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0030</p>
                <p>
                Support switching to, or simultaneous presentation of, 
                other views without losing prescription information entered so far
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0050.png" alt="Outline diagram of a screen with a Patient Banner across the top, a Medications List Toolbar below. The remainder of the screen is split vertically with &#39;Prescribing Area&#39; on the left and &#39;Medications List&#39; on the right" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0050</p>
                <p>
                Do not allow the prescribing area to be positioned such that it 
                separates the controls (such as those on a toolbar) from the view 
                to which they relate (see Design Guidance &#8211; 
                <a href="../../../MedicationsList.aspx" title="Links to Guidance - Medications List page">
                Medications List</a>)
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
