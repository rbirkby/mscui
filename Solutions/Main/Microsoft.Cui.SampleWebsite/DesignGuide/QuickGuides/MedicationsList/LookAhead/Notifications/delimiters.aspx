<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="delimiters.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.LookAhead.Notifications.Delimiters" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="midline">
				<div class="illustration">
					<img src="../../images/medv056.png" alt="Diagram of a notification containing the text &#39;aspirin &#149; paracetamol&#39; and an arrow labelled &#39;delimiter&#39; pointing to the &#39;&#149;&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-056</p>
					<p>
                    Use a delimiter that is unlikely to be interpreted as a character 
                    or number (such as a black dot &#39;&bull;&#39;), with a space either side to 
                    separate drug names and to separate the count from drug names
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
                        
			<div class="line">
				<div class="illustration">
					<img src="../../images/medv184.png" alt="Diagram of the top right hand corner of a list of medications in which a notification is visible that contains the text &#39;aspirin &#149; paracetamol&#39;. Two arrows labelled &#39;no delimiter&#39; point to the space before the word &#39;aspirin&#39; and the space after the word &#39;paracetamol&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-184</p>
					<p>
                    Do not use leading or trailing delimiters
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			            
        </div>
    </div>    
</asp:Content>
