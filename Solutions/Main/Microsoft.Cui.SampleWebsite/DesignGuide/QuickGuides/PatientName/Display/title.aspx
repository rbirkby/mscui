<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="title.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Display.Title" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            

            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0003.png" alt="A Patient Name: &#39;CORDERO, Thelma (Ms)&#39; with arrows pointing at the two parentheses" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0003</p>
                <p>
                The display must include parentheses around 
                the Title to separate and distinguish it from 
                the other name elements
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">NID-0008</p>
                <p>
                The display must present a single pair of 
                parentheses around the Title element, for 
                example, (Mr)
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0007.png" alt="Three patient names: &#39;JOHNSON, Brian (Mr)&#39;, &#39;COOPER, Catherine (Dr)&#39;, &#39;EVANS WEST, Jonathan (Sir)&#39;, with an arrow pointing at &#39;(Sir)&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0007</p>
                <p>
                The display must present the Title element in 
                title case, for example, Sir not SIR, Mr not MR
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0009.png" alt="A patient name, with a callout for the Title &#39;(Mr)&#39; with the text &#39;Up to 35 characters&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0009</p>
                <p>
                The display must allow any free-text (up to 
                35 characters) to be presented in the Title element
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/nid0010.png" alt="An arrow pointing to the end of a patient name marking the absence of a full stop" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0010</p>
                <p>
                The display must omit a trailing 
                full stop from the Title element (for example, &#39;Mr&#39; 
                not &#39;Mr.&#39;)
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
