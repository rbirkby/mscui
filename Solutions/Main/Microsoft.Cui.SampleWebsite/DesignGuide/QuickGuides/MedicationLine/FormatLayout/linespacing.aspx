<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="linespacing.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationLine.FormatLayout.LineSpacing" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           

            <div class="midline">
                <div class="landscape"><img src="../images/medi036.png" alt="Diagram of a medication line with horizontal lines marking the top and bottom of the first line of text and an arrow indicating the height of the line of text" /></div>
                <div class="guidealone">
                <p class="number">MEDi-036</p>
                <p>
                When displaying a medication as one or many lines of text, preserve white space 
                between the lines by ensuring that the line height is no less than 120&#37; 
                (120&#37; leading) and no greater than 140&#37; (140&#37; leading)
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="line">
                <div class="landscape"><img src="../images/medi037.png" alt="Diagram of two medication lines, one above the other, with horizontal lines marking the bottom of the last line of text in the first medication line and the top of the first line of text in the second medication line. An arrow indicates the height of the space between the two" /></div>
                <div class="guidealone">
                <p class="number">MEDi-037</p>
                <p>
                When displaying a list of medications, ensure that there is a space 
                equivalent to at least one line height of 100% between the last line 
                of one medication line and the first line of the medication line below
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>


        </div>
    </div>
</asp:Content>
