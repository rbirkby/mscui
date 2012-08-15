<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="detailscontrol.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.StepByStep.DetailsControl" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="landscape">
                	<img src="../images/msp1340.png" alt="A series of seven input controls, with the fifth control wrapping to a new line. A button below the form labelled &#39;Full Prescription Form&#39; has a callout labelled &#39;control for switching to a detailed view&#39;"  />
                </div>
                <div class="guidealone">
                <p class="number">MSP-1340</p>
                <p>
                Provide a control (such as a button) for switching to a detailed 
                view from which input controls for all valid fields for this 
                prescription can be accessed
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
            	<div class="landscape">
                	<img src="../images/msp1350.png" alt="A drug name and list of routes below which is a disabled button labelled &#39;Full Prescription Form&#39; with a callout labelled &#39;control disabled&#39;" />
                </div>
                <div class="guidealone">
                <p class="number">MSP-1350</p>
                <p>
                Disable the control for displaying all valid input controls until 
                at least a drug name and route (or attributes from which the type 
                of medication can be derived) have been selected
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="line">
            	<div class="landscape">
                	<img src="../images/msp1360.png" alt="Outline diagram with a rectangle representing a full prescription form and a button in the top right hand corner labelled &#39;Go Back&#39;" />
                </div>
                <div class="guideleft">
                <p class="number">MSP-1360</p>
                <p>
                Provide a control that allows the switch to a more detailed 
                prescription form to be undone, thus returning to the previous 
                view containing only the required fields
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-1400</p>
                <p>
                Do not display optional fields, or controls for accessing 
                optional fields (apart from the button for accessing a more 
                detailed prescription form)
                </p>
                <p class="recommended">Recommended</p>
                </div>
                <div class="guidetext">
                <p class="number">MSP-1410</p>
                <p>
                Ensure that no data is lost whilst switching from one form to another
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-1420</p>
                <p>
                Minimise the number of controls that are needed for navigation
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-1430</p>
                <p>
                Ensure that no data is lost whilst switching from one form to another
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-1440</p>
                <p>
                Minimise the number of controls that 
                are needed to navigate between forms
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

        </div>
    </div>      
</asp:Content>
