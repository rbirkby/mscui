<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="layout.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.TemplatePrescriptions.Layout" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="landscape">
                	<img src="../images/msp1110.png" alt="A list of (two) template prescriptions with the information in each aligned into three columns" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1110</p>
                <p>
                Present template prescriptions in a list without column headings
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1150</p>
                <p>
                Do not allow horizontal scrolling of a list of template prescriptions
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
            	<div class="landscape">
                	<img src="../images/msp1130.png" alt="Diagram of a list of template prescriptions in which &#39;oral solution &#8211; four times a day&#39; has a callout labelled &#39;attributes combined&#39; and a callout labelled &#39;dose&#39; indicates the first section of each row" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1130</p>
                <p>
                Display dose or a dose equivalent at the 
                beginning of each template prescription
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1120</p>
                <p>
                Where necessary, combine attributes into a single column 
                to reduce the number of columns
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp1140.png" alt="A list of (two) template prescriptions: &#39;DOSE 2.5 mg &#8211; tablet &#8211; once only&#39; and &#39;DOSE 1 mg &#8211; oro&#45;dispersible tablet &#8211; once only&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-1140</p>
                <p>
                When space is limited, display template prescriptions in 
                the style described in Design Guidance &#8211; 
                <a href="../../../MedicationLine.aspx" title="Links to Guidance - Medication Line page">
                Medication Line</a>
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>


        </div>
    </div>
</asp:Content>
