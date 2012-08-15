<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="visualstyle.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Structure.VisualStyle" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">        
        
            <div class="midline">
				<div class="illustration">
					<img src="../images/pab0014.png" alt="Diagram of a full screen with the patient banner shown full width and with a thick border" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0014</p>
					<p>
                    Apply visual styling such as a thick border or 
                    distinguishing background colour, to the Patient 
                    Banner in contrast to other elements of the application&#39;s 
                    user interface
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
             </div>

             <div class="line">
				<div class="landscape">
					<img src="../images/living.png" alt="Zone 1 of a patient banner, with a thin dark border and a white background" />
				</div>
				<div class="landscape">
					<img src="../images/deceased.png" alt="Zone 1 of a patient banner, with a thick dotted black border and a pale grey background" />
				</div>
				<div class="guideleft">
					<p class="number">PAB-0055</p>
					<p>
                    For a deceased patient, use a background area for Zone 1 
                    in which both the colour and the pattern are substantially 
                    different from those used for a living patient
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
					<p class="number">PAB-0056</p>
					<p>
                    The choice of both background colour and pattern must be 
                    such as to differentiate the Patient Banner of a deceased 
                    patient from that of a living patient, on all display 
                    devices, including, but not limited to, desktop monitors 
                    and projected images
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
             
        </div>
    </div>
</asp:Content>
