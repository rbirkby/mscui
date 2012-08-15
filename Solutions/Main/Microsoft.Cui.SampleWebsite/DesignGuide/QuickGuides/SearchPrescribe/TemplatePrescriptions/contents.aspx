<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="contents.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.TemplatePrescriptions.Contents" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1160.png" alt="Diagram showing a drug (zolmitriptan) and a selection from a cascading list (oral) below which is a list of template prescriptions labelled &#39;template prescriptions for oral zolmitriptan&#39;. A callout labelled &#39;option for proceeding directly to the prescription form&#39; points to &#39;other...&#39; which is at the bottom of the list, below a horizontal line" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1160</p>
                <p>
                Display only template prescriptions relevant to 
                the drug and selections from cascading lists
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-1170</p>
                <p>
                Minimise (where possible, avoid) the number of template prescriptions 
                that have only one attribute that is different from other template 
                prescriptions in the same list
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1220</p>
                <p>
                Keep the number of attributes defined by a 
                template prescription to a minimum
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-1230</p>
                <p>
                Include an option to proceed directly to the prescription 
                form without selecting a template prescription
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-1240</p>
                <p>
                Display the option for proceeding directly to the prescription form 
                at the end of the list and separate it from the template prescriptions 
                with a horizontal line
                </p>
                <p class="mandatory">Mandatory</p> 
                </div>
            </div>         

        </div>
    </div>
</asp:Content>
