<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="multiplelists.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.QuickList.MultipleLists" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0080.png" alt="Diagram with a drop-down list at the top labelled &#39;Quick List&#39; in which the text &#39;General Medicine&#39; is displayed. Below this is a search text input box and below that is a Quick List" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0080</p>
                <p>
                When one or more Quick Lists are provided, display one by default 
                when the prescribing process is started
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0240</p>
                <p>
                Limit the number of Quick Lists that are available to an 
                individual user to the minimum that are contextually appropriate
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0250.png" alt="A drop-down list labelled &#39;Quick List&#39; in which the text &#39;General Medicine&#39; is displayed" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0250</p>
                <p>
                When multiple Quick Lists are available to a single user, provide 
                a means of navigating between them
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0260.png" alt="Diagram of a drop-down list labelled &#39;Quick List&#39; in which the text &#39;General Medicine&#39; is displayed. A callout indicates that &#39;General Medicine&#39; is the currently selected Quick List" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0260</p>
                <p>
                When multiple Quick Lists are necessary, display the currently 
                selected Quick List in the control that is used to select a Quick List
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0270.png" alt="Diagram of a drop-down list labelled &#39;Quick List&#39; in which the text &#39;General Medicine&#39; is displayed. A callout indicates that &#39;General Medicine&#39; is the currently selected Quick List" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0270</p>
                <p>
                When multiple Quick Lists are necessary, use the words &#39;Quick List&#39; 
                in a label for the Quick List control or within the control
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
      
        </div>
    </div>
</asp:Content>
