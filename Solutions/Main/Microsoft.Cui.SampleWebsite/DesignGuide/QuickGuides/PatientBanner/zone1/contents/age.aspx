<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="age.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Zone1.Contents.Age" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           
           
             <div class="midline">
             <div class="landscape">
                <img src="../../images/pab0023.png" alt="Diagram of Zone 1 of a patient banner in which the text &#39;14-Jul-1945 (61y)&#39; is displayed and an arrow points to &#39;61y&#39;"  />  
             </div>
                <div class="guideleft">
					<p class="number">PAB-0023</p>
					<p>
                    Display the age of a living patient in Zone 1
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
					<p class="number">PAB-0034</p>
					<p>
                    When displaying the age of a living patient, place 
                    it in parentheses immediately following the date of 
                    birth, and without a label
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>

			<div class="line">
				<div class="landscape">
					<img src="../../images/pab0018.png" alt="Diagram of Zone 1 of a patient banner for a deceased patient in which &#39;Died 06-Dec-2006&#39; is displayed immediately the date of birth and &#39;Age at Death 61y&#39; is displayed below the gender"  />
				</div>
				<div class="illustration">
					&nbsp;
				</div>
				<div class="guideleft">
					<p class="number">PAB-0024</p>
					<p>
                    For a deceased patient, display the date of death and the 
                    age at death in Zone 1
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
					<p class="number">PAB-0060</p>
					<p>
                    Display the age at death without parentheses
                    </p>
					<p class="mandatory">Mandatory</p>
                    </p>
				</div>
			</div>

        </div>
    </div>
</asp:Content>
