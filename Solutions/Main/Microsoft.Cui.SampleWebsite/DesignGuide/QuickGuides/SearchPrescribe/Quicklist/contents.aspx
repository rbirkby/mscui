<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="contents.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.QuickList.Contents" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0060.png" alt="Diagram of a search text input box and a list of medications below in which the final line reads &#39;10 commonly prescribed medications&#39;. The list has a callout labelled &#39;Quick List&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0060</p>
                <p>
                Support the display of a Quick List containing preselected drug names
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0130</p>
                <p>
                Allow only items that can be displayed in a search results list 
                to be included in a Quick List
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0110</p>
                <p>
                Do not support navigation (such as expanding and collapsing 
                or drilling down) within a Quick List
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0120</p>
                <p>
                Limit the number of drugs in the Quick List such that they can 
                be displayed in full without a scroll bar
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp0090.png" alt="Illustration of the bottom of a Quick List in which the text &#39;10 commonly prescribed medications&#39; is displayed" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0090</p>
                <p>
                Include a description of the contents of the Quick 
                List at the top or bottom of the list
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
