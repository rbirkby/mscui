<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="authorising.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.PreviewAuthorise.Authorising" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
         
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp2280.png" alt="Diagram of a preview in which two buttons are displayed below the medication line. The first button is labelled &#39;Authorise&#39; and a callout labelled &#39;control for authorising the prescription &#39;points to this button" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-2280</p>
                <p>
                Provide a button for authorising the prescription 
                and label it &#39;Authorise&#39;
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-2290</p>
                <p>
                Place the Authorise button at the bottom right of the 
                prescription form such that it may be out of view if 
                the form is long enough to need a scroll bar
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-2270</p>
                <p>
                Place the preview button before the authorise button 
                and reflect this in the tabbing order
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-2300</p>
                <p>
                Do not set the focus to the Authorise button by default
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-2310</p>
                <p>
                Disable the Authorise button until all required 
                fields have been completed
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>  
                     
        </div>
    </div>       
</asp:Content>
