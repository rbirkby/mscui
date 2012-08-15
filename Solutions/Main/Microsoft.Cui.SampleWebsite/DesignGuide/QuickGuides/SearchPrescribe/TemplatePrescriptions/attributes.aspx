<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="attributes.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.TemplatePrescriptions.Attributes" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
     <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="landscape">
                	<img src="../images/msp1180.png" alt="Diagram of a list of (two) template prescriptions with information in each row aligned into three columns with callouts from left to right labelled &#39;dose&#39;, &#39;form&#39; and &#39;frequency&#39; respectively" />
                </div>
                <div class="guidealone">
                <p class="number">MSP-1180</p>
                <p>
                Where possible include dose (or equivalent) and 
                frequency in template prescriptions
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="landscape">
                	<img src="../images/msp1190.png" alt="Diagram of a list of (two) template prescriptions with information in each row aligned into three columns and callouts from left to right labelled &#39;dose&#39;, &#39;strength&#39; and &#39;frequency&#39; respectively" />
                </div>
                <div class="guidealone">
                <p class="number">MSP-1190</p>
                <p>
                Include strength in template prescriptions when 
                it is required for this drug
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1200.png" alt="Diagram of a list of three template prescriptions for modified-release morphine. Each row contains three parts. In the first row, the second part (DOSE 10 mg) has a callout labelled &#39;brand name&#39; and the third part (12-hourly preparation) in grey italic text has a callout labelled &#39;supplementary information&#39;" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1200</p>
                <p>
                Include brand in template prescriptions 
                when it is required for this drug
                </p>
                <p class="recommended">Recommended</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1210</p>
                <p>
                When a template prescription includes supplementary 
                information, display this information in grey italics
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>   
</asp:Content>
