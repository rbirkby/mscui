<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="middle.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Input.Middle" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0032.png" alt="A text input box for Middle Names in which the prompt text &#39;e.g. John&#39; is displayed" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0032</p>
                <p>
                Middle name input must be via a free-text entry box
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">NID-0036</p>
                <p>
                Middle name input box should contain a relevant prompt 
                in its default state (for example, &#39;e.g. David James&#39;) 
                in occluded form
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0033.png" alt="A diagram of a text input box for Middle Names showing the caption &#39;Up to 100 characters input&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0033</p>
                <p>
                Middle name input box must accept a maximum of 100 characters
                </p>
                <p class="mandatory">Mandatory </p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/nid0034.png" alt="A minimum width input box for Middle Name showing that at least 8 characters can be displayed" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0034</p>
                <p>
                Middle name input box should be capable of displaying 
                a minimum of eight characters without occlusion
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">NID-0035</p>
                <p>
                Middle name input box should optimally display 7 characters without occlusion
                </p>
                <p class="recommended">Recommended </p>
                </div>
            </div>
                        
        </div>
    </div>
</asp:Content>
