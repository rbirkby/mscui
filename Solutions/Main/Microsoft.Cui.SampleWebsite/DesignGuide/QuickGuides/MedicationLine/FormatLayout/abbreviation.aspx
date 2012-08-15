<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="abbreviation.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationLine.FormatLayout.Abbreviation" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           

            <div class="line">
                <div class="illustration">
                <img src="../images/medi022a.png" alt="A medication line: paracetamol 500 mg + metoclopramide 5 mg &#150; sachet &#150; oral &#150; DOSE 2 sachets &#150; every 4 hours as required &#150; maximum 6 doses in 24 hours" />
                <img src="../images/medi022b.png" alt="A medication line: sodium chloride  0.9% &#150; infusion &#150; VOLUME 1,000 mL &#150; 40 mL per hour &#150; over 8 hours &#150; intravenous &#150; once only" />
                </div>
                <div class="guidetext">
                <p class="number">MEDi-022</p>
                <p>Do not abbreviate drug names</p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MEDi-023</p>
                <p>Use long form names rather than abbreviations or symbols where possible</p>
                <p class="recommended">Recommended</p>
                <p class="number">MEDi-024</p>
                <p>
                Do not put a full stop after abbreviations for units (for example mg and mL)
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>   

        </div>
    </div>
</asp:Content>
