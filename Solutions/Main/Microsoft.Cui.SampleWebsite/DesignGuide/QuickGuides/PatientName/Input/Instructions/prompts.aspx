<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="prompts.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Input.Instructions.Prompts" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            

            <div class="midline">
            	<div class="illustration">
                	<img src="../../images/nid0055.png" alt="An input form showing the following input controls and prompts: Title, e.g. Mr; Family name, e.g. SMITH; Given name, e.g. John; Middle name(s), e.g. David James; Suffix, e.g. Junior; Known as, e.g. Johnny-Boy" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0055</p>
                <p>
                Each field in a name input control must have an associated prompt
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">NID-0058</p>
                <p>
                Prompt values should be those illustrated
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">NID-0059</p>
                <p>
                Prompts should be lighter in weight and colour than the input text, and italicized
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="midline">
            	<div class="illustration">
                	<img src="../../images/nid0056.png" alt="A Family Name input control in which the prompt &#39;e.g. SMITH&#39; is displayed" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0056</p>
                <p>
                Prompts for Family Name should be capitalized
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
 
            <div class="line">
            	<div class="illustration">
                	<img src="../../images/nid0057.png" alt="A Given Name input control in which the prompt &#39;e.g. John&#39; is displayed" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0057</p>
                <p>
                All prompts except Family Name should have sentence style capitalization
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
 

        </div>
    </div>
</asp:Content>
