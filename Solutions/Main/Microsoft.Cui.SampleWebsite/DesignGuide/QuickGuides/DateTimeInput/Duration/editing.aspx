<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="editing.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.DateTimeInput.Duration.Editing" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
	<div id="page">
		<div id="guidance">
			
			<div class="midline">
				<div class="illustration">
					<img src="../images/dtc0032.png" alt="Two input boxes labelled &#39;Duration&#39; and showing &#39;30 sec&#39; and &#39;15min 30sec&#39; respectively" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0032</p>
					<p>
                    Allow entry of time duration units either singly 
                    or in combination
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="midline">
				<div class="illustration">
					<img src="../images/dtc0033.png" alt="A three step diagram with each step showing an input box labelled &#39;Duration&#39; and containing: 1. &#39;15min 30sec&#39;; 2. &#39;15min 30sec&#39;, with &#39;30&#39; selected; 3. &#39;15min 3&#39;, a text cursor followed by a space and then &#39;sec&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0033</p>
					<p>
                    Allow editing of the individual elements of a duration
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/dtc0036.png" alt="Two input boxes labelled &#39;Duration&#39; and showing &#39;15min 30sec&#39; and &#39;15 min 30 sec&#39; respectively" />
				</div>
				<div class="guidetext">
					<p class="number">D+Tc-0036</p>
					<p>
                    Allow the user to optionally enter white space within 
                    the duration input, for example &#39;3 hr 5 min&#39; as well 
                    as &#39;3hr 5min&#39;
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
			
		</div>
	</div>
</asp:Content>
