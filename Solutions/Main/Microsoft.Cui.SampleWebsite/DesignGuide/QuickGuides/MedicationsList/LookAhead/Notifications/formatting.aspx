<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="formatting.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.LookAhead.Notifications.Formatting" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="midline">
				<div class="illustration">
					<img src="../../images/medv186.png" alt="Diagram of a look-ahead notification containing the text &#39;6 more &#143; metformin &#143; glicazide&#39; and an arrow labelled &#39;bold&#39; pointing to &#39;metformin&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-186</p>
					<p>
                    Display drug names in bold 
                    and in black text by default
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
                        
			<div class="midline">
				<div class="illustration">
					<img src="../../images/medv187.png" alt="Diagram of a look-ahead notification containing the text &#39;6 more &#143; metformin &#143; glicazide&#39; and an arrow labelled &#39;normal weight&#39; pointing to &#39;6 more&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-187</p>
					<p>
                    Display counts and descriptive text 
                    (such as &#39;more&#39;) in normal 
                    weight font
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
                        
			<div class="line">
				<div class="illustration">
					<img src="../../images/medv188.png" alt="Illustration of the top right hand corner of a list of medications with a callout labelled &#39;solid background in a light colour&#39; indicating the look-ahead scroll bar notification" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-188</p>
					<p>
                    Use a light solid background colour for the notifications 
                    that is both sufficiently different from the colour in the 
                    space reserved for notifications and sufficiently different 
                    from the black text in the notification
                    </p>
					<p class="recommended">Recommended</p>
					<p class="number">MEDv-189</p>
					<p>
                    Do not use a border in a dark colour or 
                    with a weight greater than 1 point for a 
                    look-ahead notification
                    </p>
					<p class="recommended">Recommended</p>
				</div>
	
			            
        </div>
    </div>   
   
</asp:Content>
