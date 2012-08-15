<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="notifications.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.SearchPrescribe.QuickList.Notifications" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0070.png" alt="Illustration of a notification containing the text &#39;The contents of the Quick List were updated on 23-Jun-2008.&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0070</p>
                <p>
                Minimise the frequency with which the contents of the Quick List change
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-0170</p>
                <p>
                Display a notification when the contents of a Quick List have changed 
                since it was last presented to the current user
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0200</p>
                <p>
                Display the Quick List notification every time the Quick List is displayed 
                (until the user selects an option that disables it)
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0210.png" alt="Outline diagram of the Quick List on the left and a Notification on the right, mutually aligned at the top" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0210</p>
                <p>
                Do not display the notification such that it obscures the contents of the Quick List
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0180.png" alt="Diagram of a notification with the text &#39;Close&#39; and a cross symbol (icon) in the top right hand corner" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0180</p>
                <p>
                Provide a control for closing the Quick List notification
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="midline">
            	<div class="illustration">
                	<img src="../images/msp0190.png" alt="Diagram of a notification with a checkbox in the bottom right hand corner labelled &#39;Don&#39;t show this message again&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">MSP-0190</p>
                <p>
                Provide a control for disabling the notification so that it is not 
                displayed again (until the Quick List is changed again)
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">MSP-0220</p>
                <p>
                Close the notification automatically when either a drug is selected 
                from the Quick List or text is entered into the search text input box
                </p>
                <p class="recommended">Recommended</p>
                <p class="number">MSP-0230</p>
                <p>
                Do not allow a drug to be selected from the Quick List by using the 
                keyboard until the notification has been closed
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>           
        </div>
    </div>
</asp:Content>
