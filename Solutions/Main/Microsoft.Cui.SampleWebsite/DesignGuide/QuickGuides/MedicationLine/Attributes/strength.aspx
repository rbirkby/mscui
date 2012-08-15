<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="strength.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationLine.Attributes.Strength" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           

            <div class="midline">
                <div class="illustration"><img src="../images/medi045.png" alt="A medication line: co-amoxiclav &#150; 400 and 57 mg in 5 mL suspension &#150; oral &#150; DOSE 1.2 mL &#150; every 12 hours" /></div>
                <div class="guidetext">
                <p class="number">MEDi-045</p>
                <p>
                When describing strengths with an active ingredient in a fluid, use &#39;in&#39; rather than 
                a forward slash ( &#39;&#47;&#39; ) before the fluid quantity
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
                <div class="illustration"><img src="../images/medi046.png" alt="A medication line: heparin &#150; 5,000 units per mL &#150; solution for injection &#150; DOSE 5,000 units - subcutaneous injection &#150; once only" /></div>
                <div class="guidetext">
                <p class="number">MEDi-046</p>
                <p>
                When describing strengths of an ingredient in a single unit of fluid, use the word 
                &#39;per&#39; to describe the unit of fluid
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
                <div class="illustration"><img src="../images/medi047.png" alt="A medication line: co-codamol &#150; 8 and 500 mg &#150; tablet &#150; oral &#150; DOSE 1 table &#150; every 4 to 6 hours as required" /></div>
                <div class="guidetext">
                <p class="number">MEDi-047</p>
                <p>
                When describing a strength for a combination drug whose two strength values use the 
                same unit (such as mg), use the word &#39;and&#39; in a smaller font to join the two strength 
                values and display the units after the second strength value
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
                <div class="illustration"><img src="../images/medi048.png" alt="An example strength: 0.5 mL" /></div>
                <div class="guidetext">
                <p class="number">MEDi-048</p>
                <p>
                Do not put a trailing zero after a decimal point when displaying numbers in a strength 
                value
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MEDi-049</p>
                <p>
                Put a leading zero before a decimal point for values of less than one when displaying 
                numbers in a strength value
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="line">
                <div class="illustration"><img src="../images/medi050.png" alt="A medication line: heparin &#150; 5,000 units per mL &#150; solution for injection &#150; DOSE 5,000 units - subcutaneous injection &#150; once only" /></div>
                <div class="guidetext">
                <p class="number">MEDi-050</p>
                <p>
                Use a comma to break up numeric values of one thousand and above when displaying numbers 
                in a strength value
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
