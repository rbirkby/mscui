<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="contents.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Zone1.ContentsPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
			<div class="midline">
				<div class="landscape">
					<img src="../images/pab0022.png" alt="Zone 1 of a patient banner with callouts for the following elements (in order left to right): Patient Name, Date of Birth, Age, Gender, Patient Identification Number" />
				</div>
				<div class="guideleft">
					<p class="number">PAB-0022</p>
					<p>
                    Display the elements of patient name, date of birth, 
                    gender and patient identification number in <span class="nowrap">Zone 1</span>
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
					<p class="number">PAB-0017</p>
					<p>
                    For a patient who is alive, the Patient Banner additionally 
                    displays contact details (comprising the address and phone 
                    numbers) and the patient's age
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
             </div>

			<div class="midline">
				<div class="landscape">
					<img src="../images/pab0018.png" alt="Zone 1 of a patient banner for a deceased patient with callouts for Date of Death and Age at Death" />
				</div>
				<div class="guidealone">
					<p class="number">PAB-0018</p>
					<p>
                    For a deceased patient, the Patient Banner additionally 
                    displays the last known contact details (comprising the 
                    address and phone numbers), the date of death and age at death
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
             </div>
             
			<div class="line">
				<div class="landscape">
					<img src="../images/pab0048.png" alt="Expanded section of a patient banner showing a mouse cursor over the value for Gender and a tooltip containing the text &#39;Click here to open this section of the record&#39;"/>
				</div>
				<div class="guidealone">
					<p class="number">PAB-0048</p>
					<p>
                    Provide a means to access the record 
                    for all data items in Zone 1
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>
