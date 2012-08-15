<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="formatting.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Zone1.Formatting" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
			<div class="midline">
				<div class="illustration">
					<img src="../images/pab0049.png" alt="A patient name &#39;CHANDRASEKHAR, Subramanyan (Mr)&#39; with callouts for each section: Family Name, Given Name and Title" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0053</p>
					<p>
                    Display the patient's family name in upper case and the 
                    patient's given name and title in title case
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">PAB-0051</p>
					<p>
                    Display a comma after the family name
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">PAB-0052</p>
					<p>
                    Display the title in parentheses
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>

			<div class="line">
				<div class="illustration">
					<img src="../images/pab0063.png" alt="Expanded section of a patient banner for a deceased patient in which a vertical line indicates that &#39;Born 14-Jul-1945&#39; on the top line is left-aligned to the same point as &#39;Died 06-Dec-2006&#39; on the line below" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0063</p>
					<p>
                    For a deceased patient, display the data labels and values corresponding 
                    to the date of death and age at death in that order, immediately below the 
                    label corresponding to the date of birth, with both the date labels being 
                    left-aligned
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>
