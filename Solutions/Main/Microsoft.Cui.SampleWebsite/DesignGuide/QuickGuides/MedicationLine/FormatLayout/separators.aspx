<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="separators.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationLine.FormatLayout.Separators" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           
            
            <div class="midline">
                <div class="illustration">
                <img src="../images/medi018.png" alt="A medication line: paracetamol &#150; 120 mg in 5 mL &#150; suspension &#150; DOSE 80 mg &#150; oral &#150; every 6 hours" />
                </div>
                <div class="guidetext">
                <p class="number">MEDi-018</p>
                <p>
                When combining attributes in a text string, use a long dash (em dash) surrounded by spaces 
                between the attributes
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
                <div class="illustration"><img src="../images/medi019.png" alt="Diagram of a medication line for &#39;paracetamol 500 mg &#43; metoclopramide 5 mg&#39; with arrows pointing to the double space between &#39;paracetamol&#39; and &#39;500&#39; and between &#39;metoclopramide&#39; and &#39;5&#39;" /></div>
                <div class="guidetext">
                <p class="number">MEDi-019</p>
                <p>
                Use a double space instead of a long dash or separator between a drug name and strength 
                when there are multiple drug names in one medication line
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="line">
                <div class="illustration"><img src="../images/medi020.png" alt="Diagram of a medication line for &#39;sodium chloride  0.9%&#39; with an arrow pointing to the double space between &#39;chloride&#39; and &#39;0.9%&#39;" /></div>
                <div class="guidetext">
                <p class="number">MEDi-020</p>
                <p>
                Use a double space instead of a long dash or separator between a drug name and strength 
                when the strength is expressed as a percentage
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
