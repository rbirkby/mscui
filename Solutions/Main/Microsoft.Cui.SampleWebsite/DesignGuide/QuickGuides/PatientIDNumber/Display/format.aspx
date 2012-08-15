<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="format.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientIDNumber.Display.Format" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           
            
            <div class="line">
                <div class="illustration">
                    <img src="../images/num0001.png" alt="123 456 7890" />
                </div>
                <div class="guidetext">
					<p class="number">NUM-0001</p>
                    <p>
                    Display the patient identification number in full, on a single line, 
                    without truncation or splitting it over multiple lines
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="line">
                <div class="illustration">
                    <img src="../images/num0002.png" alt="&#39;123 456 7890&#39; with brackets marking the 3 sections separated by spaces that have 3, 3 and 4 characters in each group respectively" />
                </div>
                <div class="guidetext">
					<p class="number">NUM-0002</p>
                    <p>
                    Display the NHS Number as three groups, with a 
                    single space included as a separator between groups, as illustrated
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="line">
                <div class="illustration">
                    <img src="../images/num0003.png" alt="A box containing highlighted (selected) text &#39;123 456 7890&#39;"  />
                </div>
                <div class="guidetext">
					<p class="number">NUM-0003</p>
                    <p>
                    Support the copying of patient identification numbers by the user 
                    as part of the &#39;Copy and Paste&#39; task
                    </p>
					<p class="recommended">Recommended</p>
                </div>
            </div>
 
        </div>
    </div>
</asp:Content>
