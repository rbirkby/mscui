<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="title.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Input.Title" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0014.png" alt="A diagram of an maximum width editable combo box for Title labelled to show that it is wide enough to display 35 characters" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0014</p>
                <p>
                Input control must allow 
                a maximum of 35 characters
                </p>
                <p class="mandatory">Mandatory </p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0015.png" alt="A diagram of a minimum width editable combo box for Title labelled to show that it is wide enough to display 4 characters" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0015</p>
                <p>
                Minimum visual width of the input 
                box must display four characters
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                    <table class="datavalues">
                        <tr><td>Sir</td></tr>
                        <tr><td>Lady</td></tr>
                        <tr><td>Lord</td></tr>
                        <tr><td>Dame</td></tr>
                        <tr><td>Other...</td></tr>
                    <table class="leftdata">
                        <tr><td>Mr</td></tr>
                        <tr><td>Mrs</td></tr>
                        <tr><td>Ms</td></tr>
                        <tr><td>Dr</td></tr>
                        <tr><td>Rev</td></tr>
                    </table>
                </div>
                <div class="guidetext">
                <p class="number">NID-0016</p>
                <p>
                This table shows suggested values for &#39;Title&#39;
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/nid0017.png" alt="A drop-down box in which &#39;Other&#39; is shown selected at the bottom of a list of values for Title" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0017</p>
                <p>
                One value should allow the user to invoke 
                free-text input mode (for example &#39;Other...&#39; 
                in the illustrations)
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/nid0018.png" alt="An editable combo box for Title containing the prompt text &#39;e.g. Mr&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0018</p>
                <p>
                Input box should contain a relevant 
                prompt, for example, Mr
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">NID-0019</p>
                <p>
                Input control should be in the form of a drop-down combo-box
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
