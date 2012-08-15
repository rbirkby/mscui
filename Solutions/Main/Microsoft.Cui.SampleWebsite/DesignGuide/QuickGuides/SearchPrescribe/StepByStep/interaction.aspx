<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="interaction.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.StepByStep.Interaction" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="landscape">
                	<img src="../images/msp1300.png" alt="Three step diagram: 1. A drug name (clarithromycin) and a list of routes 2. A drug name, route (oral) and a list of doses 3. A drug name, route, dose (DOSE 250 mg) and a list containing one frequency" />
                </div>
                <div class="guidealone">
                <p class="number">MSP-1300</p>
                <p>
                When there are no template prescriptions to display and 
                a known set of safe values are available (for example, for 
                dose and frequency), present selection lists for those fields 
                sequentially (step by step)
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="midline">
            	<div class="landscape">
                	<img src="../images/msp1310.png" alt="Diagram of three boxes plus a list in a row. The first two (clarithromycin and oral) are labelled &#39;drug name and route&#39;, the third plus the list are labelled &#39;required fields (that won&#39;t be pre&#45;filled)&#39;" />
                </div>
                <div class="guidealone">
                <p class="number">MSP-1310</p>
                <p>
                Require the selection of at least drug name and route (or 
                attributes from which the type of medication can be derived) 
                before presenting input controls for any other values
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
            	<div class="landscape">
                	<img src="../images/msp1320.png" alt="Three step diagram: 1. A drug name and a cascading list containing routes 2. A drug name, route and list labelled &#39;required fields&#39; containing doses 3. A drug name, route, dose and list labelled &#39;required fields&#39; containing a frequency" />
                </div>
                <div class="guidealone">
                <p class="number">MSP-1320</p>
                <p>
                After selections have been made in all cascading lists, 
                if there are no template prescriptions, display any required 
                fields that will not be pre-filled in sequence such that a 
                field is displayed when the previous one has been completed
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1330.png" alt="A series of six input boxes with the fifth wrapped onto a new line. The fifth box contains the text &#39;four times a day as required&#39;. The sixth box contains the prompt &#39;Give when...&#39; and has a drop-down list containing &#39;Breathless&#39; and &#39;PEFR is below 200&#39;" />
                </div>
                <div class="guidealone">
                <p class="number">MSP-1330</p>
                <p>
                If a selection from a cascading list (such as frequency of 
                &#39;as required&#39;) requires a further field to be completed, 
                display that field before the remaining required fields
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

        </div>
    </div>    
</asp:Content>
