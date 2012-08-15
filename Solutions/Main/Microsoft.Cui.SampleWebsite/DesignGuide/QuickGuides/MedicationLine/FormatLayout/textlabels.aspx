<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="textlabels.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationLine.FormatLayout.TextLabels" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            

            <div class="midline">
                <div class="illustration"><img src="../images/medi009.png" alt="Diagram of a dose: &#39;DOSE 5,000 units&#39;, with callouts labelling &#39;DOSE&#39; as &#39;label&#39; and &#39;5,000 units&#39; as &#39;value&#39;" /></div>
                <div class="guidetext">
                <p class="number">MEDi-009</p>
                <p>Use a different font and colour to differentiate labels from values</p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>   

            <div class="midline">
                <div class="illustration"><img src="../images/medi029.png" alt="A medication line: co-amoxiclav &#150; 400 mg and 57 mg in 5 mL &#150; suspension &#150; oral &#150; DOSE 1.2 mL &#150; every 12 hours" /></div>
                <div class="guidetext">
                <p class="number">MEDi-029</p>
                <p>
                When a medication is represented as a single-text sentence, use a label for dose only
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MEDi-030</p>
                <p>
                When a medication is represented as a series of lines with hard line breaks, labels 
                should appear at the beginning of a new line after a hard line break
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>   

            <div class="midline">
                <div class="illustration"><img src="../images/medi031.png" alt="Diagram of a dose: &#39;DOSE 5,000 units&#39;, with an arrow pointing to the space between &#39;DOSE&#39; and &#39;5,000 units&#39;" /></div>
                <div class="guidetext">
                <p class="number">MEDi-031</p>
                <p>
                Use a space to separate a label from a value
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MEDi-032</p>
                <p>
                Do not use a colon after a label
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
                <div class="illustration"><img src="../images/medi033.png" alt="Diagram of a medication line: &#39;olmesartan &#150; tablet &#91;new line&#93; DOSE 10 mg &#91;new line&#93; ROUTE oral &#91;new line&#93; FREQUENCY once a day&#39; with an arrow labelled &#39;uppercase&#39; pointing to the label &#39;FREQUENCY&#39;" /></div>
                <div class="guidetext">
                <p class="number">MEDi-033</p>
                <p>Display labels in uppercase</p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
 
            <div class="line">
                <div class="illustration"><img src="../images/medi034.png" alt="A medication line: co-amoxiclav &#150; 400 mg and 57 mg in 5 mL &#150; suspension &#150; oral &#150; DOSE 1.2 mL &#150; every 12 hours" /></div>
                <div class="guidetext">
                <p class="number">MEDi-034</p>
                <p>
                Keep the number of text labels in a medication represented as a 
                <span class="nowrap">single-text</span> 
                sentence to a minimum
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
           
        </div>
    </div>
</asp:Content>
