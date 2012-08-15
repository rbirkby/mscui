<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="defaults.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Input.Defaults" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">    

            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0062.png" alt="A patient name input form with six input controls, each of which has prompt text displayed in grey italics" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0062</p>
                <p>
                By default, include a prompt in the input boxes to 
                indicate to a user the information required
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">NID-0063</p>
                <p>
                Present the default prompt in an occluded form to prevent 
                confusion with actual data input by a user
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div> 

            <div class="line">
            	<div class="illustration">
                	<img src="../images/nid0064.png" alt="A three step diagram showing a text input box with: 1.The prompt &#39;e.g. John&#39;, 2.A text cursor (no prompt), 3. The text &#39;Jos&#39; and a text cursor" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0064</p>
                <p>
                Remove the default prompt when a user begins to input data
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div> 

        </div>
    </div>
</asp:Content>
