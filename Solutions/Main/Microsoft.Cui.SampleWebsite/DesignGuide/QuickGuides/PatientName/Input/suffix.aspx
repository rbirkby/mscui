<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="suffix.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Input.Suffix" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0037.png" alt="A text input box for Suffix in which the prompt text &#39;e.g. Junior&#39; is displayed" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0037</p>
                <p>
                Suffix input must be via a free-text entry box
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">NID-0041</p>
                <p>
                Suffix input box should contain a relevant 
                prompt when in its default state (for example, 
                &#39;e.g. Junior&#39;) in occluded form
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0038.png" alt="A diagram of a text input box for Suffix showing the caption &#39;Up to 35 characters input&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0038</p>
                <p>
                Suffix input must accept a maximum of 35 characters
                </p>
                <p class="mandatory">Mandatory </p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0039.png" alt="A minimum width input box for Suffix showing that at least 8 characters can be displayed" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0039</p>
                <p>
                Suffix input box should be capable of displaying 
                a minimum of eight characters without occlusion
                </p>
                <p class="recommended">Recommended </p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/nid0040.png" alt="An input box for Suffix that is wide enough to display 14 characters" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0040</p>
                <p>
                Suffix input box should optimally 
                display 14 characters without occlusion
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
