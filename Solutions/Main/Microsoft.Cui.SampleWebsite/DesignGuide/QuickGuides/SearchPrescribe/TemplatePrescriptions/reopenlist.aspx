<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="reopenlist.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.TemplatePrescriptions.ReOpenList" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">

            <div class="midline">
            	<div class="landscape">
                	<img src="../images/msp1260.png" alt="A series of seven boxes, wrapping onto a new line after the fourth box. The third box has a drop-down list style of control with a callout labelled &#39;control for reopening the list of template prescriptions&#39;" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1260</p>
                <p>
                After a template prescription has been selected (and 
                one or more fields are displayed as a result) provide 
                a control that allows the list of template prescriptions 
                to be reopened
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1270</p>
                <p>
                After a template prescription has been selected, allow 
                the selection of an alternative template prescription
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
                        
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1280.png" alt="A drop-down list displayed below two adjacent boxes, one containing &#39;DOSE 250 mg&#39; and the other &#39;twice a day&#39;" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1280</p>
                <p>
                When the control for re-opening template prescriptions 
                has focus or is activated, draw attention to the fields 
                that are defined by the template prescriptions
                </p>
                <p class="recommended">Recommended</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1290</p>
                <p>
                When the template prescription control is selected, 
                provide visual cues to draw attention to the fields 
                that are defined by the template
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>    
</asp:Content>
