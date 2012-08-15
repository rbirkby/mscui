<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="order.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationLine.Attributes.Order" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           

            <div class="midline">
                <div class="illustration">
                <table class="datavalues">
                    <tr>
                        <td>1</td>
                        <td>Drug name</td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td>Brand name</td>
                    </tr>
                    <tr>
                        <td>3</td>
                        <td>Strength</td>
                    </tr>
                    <tr>
                        <td>4</td>
                        <td>Form</td>
                    </tr>
                    <tr>
                        <td>5</td>
                        <td>Dose or volume</td>
                    </tr>
                    <tr>
                        <td>6</td>
                        <td>Rate</td>
                    </tr>
                    <tr>
                        <td>7</td>
                        <td>Dose duration</td>
                    </tr>
                    <tr>
                        <td>8</td>
                        <td>Route</td>
                    </tr>
                    <tr>
                        <td>9</td>
                        <td>Frequency</td>
                    </tr>
                </table>
                <img src="../images/medi051a.png" alt="A medication line: morphine &#150; 10 mg in 2 mL &#150; solution for injection &#150; DOSE 2 mg &#150; intravenous &#150; once only" />
                <img src="../images/medi051b.png" alt="A medication line: oxycodone &#150; OXYCONTIN &#150; modified-release tablet &#150; DOSE 10 mg &#150; oral &#150; once only" />
                </div>
                <div class="guidetext">
                <p class="number">MEDi-051</p>
                <p>
                When describing a medication as a line of text, adhere to the order illustrated for 
                the display of the medication attributes (as applicable)
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="line">
                <div class="illustration">
                <img src="../images/medi052.png" alt="A medication line: salbutamol &#150; metered dose inhaler &#150; &#91;new line&#93; STRENGTH 100 micrograms per dose &#91;new line&#93; DOSE 2 puffs &#91;new line&#93; ROUTE inhaled &#91;new line&#93; every 4 hours as required &#150; maximum 8 puffs in 24 hours" />
                </div>
                <div class="guidetext">
                <p class="number">MEDi-052</p>
                <p>
                When designing for specific contexts, especially those that need additional text 
                labels and line breaks, display drug name first and display other attributes (in 
                a different order if necessary) from the one defined above
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MEDi-053</p>
                <p>
                When a medication is not displayed as a single line of text and the attributes of a 
                medication are listed in a different order, use text labels for as many of the 
                illustrated attributes as possible
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
