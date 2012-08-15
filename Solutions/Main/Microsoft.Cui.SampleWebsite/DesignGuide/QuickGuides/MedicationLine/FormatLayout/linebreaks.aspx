<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="linebreaks.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationLine.FormatLayout.LineBreaks" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            

            <div class="line">
                <div class="illustration">
                <img src="../images/medi035a.png" alt="A medication line: paracetamol &#150; tablet &#91;new line&#93; DOSE 1 g &#150; oral &#150; every 6 hours" />
                <img src="../images/medi035b.png" alt="A medication line: oxycodone &#150; OXYCONTIN &#150; modified-release tablet &#91;new line&#93; DOSE 10 mg &#150; oral &#150; once only" />
                <img src="../images/medi035c.png" alt="A medication line: DIORALYTE &#150; powder for oral solution &#91;new line&#93; DOSE 1 sachet &#150; oral &#150; once only" />
                </div>
                <div class="guidetext">
                <p class="number">MEDi-035</p>
                <p>
                When using hard line breaks at set points (such as before a dose), do not use 
                a long dash at the end of the previous line
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>   

        </div>
    </div>
</asp:Content>
