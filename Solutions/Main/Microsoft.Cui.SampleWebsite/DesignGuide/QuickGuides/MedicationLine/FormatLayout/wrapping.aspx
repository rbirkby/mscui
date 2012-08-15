<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="wrapping.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationLine.FormatLayout.Wrapping" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            

            <div class="midline">
                <div class="illustration"><img src="../images/medi010.png" alt="Diagram of a medication line with each element surrounded by a dotted border: &#91;insulin soluble human&#93; &#150; &#91;ACTRAPID&#93; &#150; &#91;100 units per mL&#93; &#150; &#91;solution for injection&#93; &#150; &#91;DOSE 12 units&#93; &#150; &#91;subcutaneous&#93; &#150; &#91;twice a day&#93;" /></div>
                <div class="guidetext">
                <p class="number">MEDi-010</p>
                <p>
                When wrapping the text of a medication line, do so without breaking up the contents of a 
                single attribute unless that single attribute will not fit on one line
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MEDi-008</p>
                <p>
                Do not allow wrapping to separate a label from a value
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>            

            <div class="midline">
                <div class="illustration"><img src="../images/medi011.png" alt="Diagram of a medication line: &#39; insulin soluble human &#150; &#91;new line&#93; ACTRAPID &#150; 100 units per mL &#150; &#91;new line&#93; solution for injection &#150; &#91;new line&#93; DOSE 12 units &#150; subcutaneous &#150; &#91;new line&#93; twice a day&#39; with an arrow pointing to the &#39;&#150&#39; after &#39;subcutaneous&#39;" /></div>
                <div class="guidetext">
                <p class="number">MEDi-011</p>
                <p>
                When wrapping the text of a medication line, keep trailing delimiters with the preceding 
                attribute
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>     
            
            <div class="line">
                <div class="illustration"><img src="../images/medi021.png" alt="A medication line: haemophilus influenzae &#91;new line&#93; type B vaccine &#150; &#91;new line&#93; solution for injection &#150; &#91;new line&#93; DOSE 0.5 mL &#150; &#91;new line&#93; intramuscular &#150; once only" /></div>
                <div class="guidetext">
                <p class="number">MEDi-021</p>
                <p>
                If a long drug name exceeds the available screen space and has to be wrapped, ensure that 
                the drug name is wrapped between words
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>              
        </div>
    </div>
</asp:Content>
