<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="controltype.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.InputControls.ControlType" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
        
            <div class="line">
            	<div class="illustration">
                	<img src="../images/msp2170.png" alt="Two step diagram illustrating the selection of an optional input control &#39;As Required&#39; and the subsequent display of two highlighted input controls containing &#39;as required&#39; in one and the prompt text &#39;Give when...&#39; in the other" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-2170</p>
                <p>
                Do not provide a check box for fields with two opposite states when one of
                those states causes a related field to be presented. (For example, do not
                provide a check box to set &#39;as required&#39; to on or off if a setting 
                of &#39;on&#39; requires another field to be presented to qualify the 
                conditions for administration)
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>
                        
        </div>
    </div>      
</asp:Content>
