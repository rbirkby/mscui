<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="family.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Input.Family" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
   <div id="page">
        <div id="guidance">            
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0020.png" alt="A text entry input box containing the prompt &#39;e.g. SMITH&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0020</p>
                <p>
                Family Name input must be via a free-text entry box
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">NID-0024</p>
                <p>
                Family Name input box should contain a relevant 
                prompt in its default state (for example, &#39;e.g. 
                SMITH&#39;) in occluded form
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0021.png" alt="A diagram of a text input box for Family Name showing the caption &#39;Up to 35 characters input&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0021</p>
                <p>
                Family Name input box must accept a maximum of 35 characters
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0022.png" alt="A minimum width input box for Family Name showing that at least 8 characters can be displayed" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0022</p>
                <p>
                Family Name input box should be capable of 
                displaying a minimum of eight characters without occlusion
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0023.png" alt="An input box for Family Name that is wide enough for 14 characters to be displayed" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0023</p>
                <p>
                Family Name input box should optimally display 
                14 characters without occlusion
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/nid0025.png" alt="An input box for Family Name in which the text &#39;WINSTANLEY&#39; is displayed" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0025</p>
                <p>
                When displaying a Family Name value, the 
                characters should all be in uppercase
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
