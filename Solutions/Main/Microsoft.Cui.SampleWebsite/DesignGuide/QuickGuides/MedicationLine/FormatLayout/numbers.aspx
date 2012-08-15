<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="numbers.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationLine.FormatLayout.Numbers" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">          
            
            <div class="midline">
                <div class="illustration">
                <img src="../images/medi014a.png" alt="A medication line: paracetamol &#150; tablet &#91;new line&#93; DOSE 1 g &#150; oral &#150; every 6 hours" />
                <img src="../images/medi014b.png" alt="A medication line: paracetamol &#150; tablet &#91;new line&#93; DOSE 500 mg &#150; oral &#150; every 6 hours" />
                </div>
                <div class="guidetext">
                <p class="number">MEDi-014</p>
                <p>
                Where possible, avoid the need for decimal points by changing the 
                units without breaking convention
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="midline">
                <div class="illustration">
                <img src="../images/medi015.png" alt="A medication line: INFANRIX-IPV Vaccine &#150; suspension for injection &#150; DOSE 0.5 mL &#150; every 6 hours" />
                </div>
                <div class="guidetext">
                <p class="number">MEDi-015</p>
                Do not put a trailing zero after a sub-decimal value (that is, 
                &#39;0.5&#39; is correct but &#39;0.50&#39; is incorrect)
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MEDi-016</p>
                Put a leading zero before a decimal point for values of less than one
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="line">
                <div class="illustration">
                <img src="../images/medi017.png" alt="Diagram of a dose: &#39;DOSE 5,000 units&#39;, with an arrow pointing to the comma between the &#39;5&#39; and the &#39;000&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MEDi-017</p>
                Use a comma to break up numeric values of one thousand and above
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>


        </div>
    </div>
</asp:Content>
