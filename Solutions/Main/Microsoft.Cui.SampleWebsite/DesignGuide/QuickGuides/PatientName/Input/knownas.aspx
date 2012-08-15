<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="knownas.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Input.KnownAs" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0042.png" alt="A text input box for &#39;Known as&#39; in which the prompt text &#39;e.g. Johnny-Boy&#39; is displayed"  />
                </div>
                <div class="guidetext">
                <p class="number">NID-0042</p>
                <p>
                Preferred name input must be via a free-text entry box
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">NID-0046</p>
                <p>
                Preferred name input box should contain a relevant 
                prompt in its default state (for example, &#39;e.g. 
                Johnny-Boy&#39;) in occluded form
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0043.png" alt="A diagram of a text input box for &#39;Known as&#39; showing the caption &#39;Up to 35 characters input&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0043</p>
                <p>
                Preferred name input box must accept a 
                maximum of 35 characters
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0044.png" alt="A minimum width input box for &#39;Known as&#39; showing that at least 8 characters can  be displayed" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0044</p>
                <p>
                Preferred name input box should be capable of 
                displaying a minimum of eight characters without 
                occlusion
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/nid0045.png" alt="An input box for &#39;Known as&#39; that is wide enough to display 14 characters" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0045</p>
                <p>
                Preferred name input box should optimally display 
                14 characters without occlusion
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
