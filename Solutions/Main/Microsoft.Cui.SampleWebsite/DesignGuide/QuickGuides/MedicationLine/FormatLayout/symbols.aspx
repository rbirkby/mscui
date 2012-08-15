<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="symbols.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationLine.FormatLayout.Symbols" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            

            <div class="midline">
                <div class="illustration">
                    <table class="datavalues">
                        <tr>
                            <td>&#64;</td>
                            <td>At sign</td>
                        </tr>
                        <tr>
                            <td>&#124;</td>
                            <td>Vertical bar</td>
                        </tr>
                        <tr>
                            <td>&lt;</td>
                            <td>Less than bracket</td>
                        </tr>
                        <tr>
                            <td>&gt;</td>
                            <td>Greater than bracket</td>
                        </tr>
                        <tr>
                            <td>&#47;</td>
                            <td>Forward slash</td>
                        </tr>
                        <tr>
                            <td>&#92;</td>
                            <td>Back slash</td>
                        </tr>
                        <tr>
                            <td>&amp;</td>
                            <td>Ampersand</td>
                        </tr>
                        <tr>
                            <td>&#176;</td>
                            <td>Degree sign</td>
                        </tr>
                    </table>
                </div>
                <div class="guidetext">
                <p class="number">MEDi-026</p>
                <p>
                Do not use symbols that may be confused with numbers or otherwise misinterpreted, 
                including those illustrated
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>  

            <div class="midline">
                <div class="illustration">
                <img src="../images/medi027.png" alt="Diagram of a medication line for &#39;paracetamol 500 mg &#43; metoclopramide 5 mg&#39; with arrows pointing to the spaces either side of the &#39;&#43;&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MEDi-027</p>
                <p>
                Use the '&#43;' (plus symbol) only for multiple drug name medications and surround 
                it with spaces.  When a '&#43;' is displayed adjacent to a '4', separate the two with 
                a double space
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>   

            <div class="line">
                <div class="illustration">
                    <table class="datavalues">
                        <tr>
                            <td>&#40;&nbsp;&#41;</td>
                            <td>Parentheses</td>
                        </tr>
                        <tr>
                            <td>&#91;&nbsp;&#93;</td>
                            <td>Brackets</td>
                        </tr>
                        <tr>
                            <td>&#123;&nbsp;&#125;</td>
                            <td>Braces</td>
                        </tr>
                    </table>
                </div>
                <div class="guidetext">
                <p class="number">MEDi-028</p>
                <p>
                Use alternatives such as a dash or black dot (&#8226;)
                instead of brackets and separators such as those illustrated 
                that look like the number one
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>   
        </div>
    </div>
</asp:Content>
