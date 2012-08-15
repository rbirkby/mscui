<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="truncation.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationLine.FormatLayout.Truncation" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           

            <div class="line">
                <div class="illustration"><img src="../images/medi025.png" alt="A medication line: cefotaxime &#150; powder for solution for injection &#150; intravenous &#150; DOSE 400 mg &#150; every 8 hours" /></div>
                <div class="guidetext">
                <p class="number">MEDi-025</p>
                <p>Do not truncate drug names</p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MEDi-012</p>
                <p>If necessary, wrap but do not truncate medication line information</p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MEDi-054</p>
                <p>
                Do not display a part of the medication line alone if its meaning relies on other 
                parts that are not displayed
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>   
        </div>
    </div>
</asp:Content>
