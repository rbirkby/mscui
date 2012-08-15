<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="drugname.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationLine.Attributes.DrugName" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
 
        <div id="guidance"> 
        
            <div class="midline">
                <div class="illustration"><img src="../images/medi001.png" alt="Diagram of a drug name: diltiazem &#150; CALCICARD CR, with an arrow pointing at &#39;diltiazem&#39; and labelled &#39;bold&#39;" /></div>
                <div class="guidetext">
                <p class="number">MEDi-001</p>
                <p>Display generic drug names in bold</p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
                <div class="illustration"><img src="../images/medi002.png" alt="Diagram of a drug name: diltiazem &#150; CALCICARD CR, with an arrow pointing at &#39;diltiazem&#39; and labelled &#39;lowercase&#39;" /></div>
                <div class="guidetext">
                <p class="number">MEDi-002</p>
                <p>
                Display generic drug names in lowercase (capital letters may still be used for acronyms 
                and abbreviations in some drug names such as amphotericin B, factor VIII, carbomer 974P)
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
                <div class="illustration"><img src="../images/medi003.png" alt="Diagram of a drug name: diltiazem &#150; CALCICARD CR, with an arrow pointing at &#39;CALCICARD CR&#39; and labelled &#39;uppercase&#39;" /></div>
                <div class="guidetext">
                <p class="number">MEDi-003</p>
                <p>Display drug brand names in uppercase</p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="line">
                <div class="illustration"><img src="../images/medi013.png" alt="Diagram of a drug name: diltiazem &#150; CALCICARD CR, with a callout for &#39;diltiazem&#39; labelled &#39;generic name (first)&#39; and a callout for &#39;CALCICARD CR&#39; labelled &#39;brand name&#39;" /></div>
                <div class="guidetext">
                <p class="number">MEDi-013</p>
                <p>
                Where both the generic name and the brand name appear in a medication line, list the 
                generic name first
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
