<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="dose.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationLine.Attributes.Dose" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            

            <div class="midline">
                <div class="illustration">
                <img src="../images/medi007.png" alt="A medication line: morphine &#150; 2 mg in 10 mL &#150; solution for injection &#150; DOSE 2 mg &#150; intravenous &#150; once only, with &#39;morphine&#39; and &#39;2 mg&#39; in bold" />
                </div>
                <div class="guidetext">
                <p class="number">MEDi-007</p>
                <p>Provide a text label that reads &#39;DOSE&#39; before a dose</p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MEDi-038</p>
                <p>Display the dose amount and units in bold</p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
                <div class="illustration">
                <img src="../images/medi039.png" alt="A medication line: sodium chloride  0.9% &#150; infusion &#150; VOLUME 1,000 mL &#150; 40 mL per hour &#150; over 12 hours &#150; intravenous &#150; once only" />
                </div>
                <div class="guidetext">
                <p class="number">MEDi-039</p>
                <p>
                When a dose is expressed as a volume, display the volume amount in bold
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MEDi-040</p>
                <p>
                When there is no dose or volume, display a dose equivalent in place of the dose and 
                subject to the same guidance points as a dose. Precede with an appropriate text label
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
                <div class="illustration"><img src="../images/medi041.png" alt="Diagram of a dose: &#39;DOSE 5,000 units&#39;, with an arrow pointing to the space between &#39;5,000&#39; and &#39;units&#39;" /></div>
                <div class="guidetext">
                <p class="number">MEDi-041</p>
                <p>Separate the dose amount from the dose units with a space</p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
                <div class="illustration"><img src="../images/medi042.png" alt="A medication line: INFANRIX-IPV Vaccine &#150; suspension for injection &#91;new line&#93; DOSE 0.5 mL &#150; oral &#150; every 6 hours" /></div>
                <div class="guidetext">
                <p class="number">MEDi-042</p>
                <p>
                Do not put a trailing zero after a sub-decimal value when displaying a dose 
                amount (that is, &#39;0.5&#39; is correct but &#39;0.50&#39; is incorrect)
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
                <div class="illustration"><img src="../images/medi043.png" alt="A medication line: &#39;INFANRIX-IPV Vaccine &#150; suspension for injection &#91;new line&#93; DOSE 0.5 mL &#150; oral &#150; every 6 hours&#39; with a callout labelled &#39;leading zero&#39; indicating the zero in &#39;0.5&#39;" /></div>
                <div class="guidetext">
                <p class="number">MEDi-043</p>
                <p>
                Put a leading zero before a decimal point for 
                values of less than one when displaying a dose value
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="line">
                <div class="illustration"><img src="../images/medi044.png" alt="Diagram of a dose &#39;DOSE 5,000&#39; with an arrow pointing to the comma after the &#39;5&#39;" /></div>
                <div class="guidetext">
                <p class="number">MEDi-044</p>
                <p>
                Use a comma to break up numeric values of one thousand 
                and above when displaying a dose value
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
