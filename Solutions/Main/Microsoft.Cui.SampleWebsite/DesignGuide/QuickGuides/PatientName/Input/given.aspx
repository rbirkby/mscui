<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="given.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Input.Given" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0026.png" alt="A text input box for Given Name in which the prompt text &#39;e.g. John&#39; is displayed" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0026</p>
                <p>
                Given Name input must be via a <span class="nowrap">free-text</span> entry box
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">NID-0030</p>
                <p>
                Given Name input box should contain a relevant 
                prompt in its default state (for example, &#39;e.g. 
                John&#39;) in occluded form
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0027.png" alt="A diagram of a text input box for Given Name showing the caption &#39;Up to 35 characters input&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0027</p>
                <p>
                Given Name input box must accept a maximum of 35 characters
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0028.png" alt="A minimum width input box for Given Name showing that at least 8 characters can  be displayed" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0028</p>
                <p>
                Given Name input box should be capable of displaying 
                a minimum of 8 characters without occlusion
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0029.png" alt="An input box for Given Name that is wide enough to display 14 characters" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0029</p>
                <p>
                Given Name input box should optimally display 
                14 characters without occlusion
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/nid0031.png" alt="An input box for Given Name in which the text &#39;Jonathan&#39; is displayed" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0031</p>
                <p>
                When displaying a Given Name value the 
                first character should be in uppercase
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
