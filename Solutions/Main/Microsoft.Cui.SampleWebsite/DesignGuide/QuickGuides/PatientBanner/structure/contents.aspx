<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="contents.aspx.cs" 
Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Structure.Contents" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">       
        
            <div class="line">
                <div class="landscape">
    				<img src="../images/pab0002.png" alt="Zone 1 - Patient Identification Information. Top section of a patient banner containing the patient&#39;s name, date of birth, gender and identification number" />
    			</div>
				<div class="guideleft">
					<p class="number">PAB-0002</p>
					<p>
                    Display information that facilitates patient 
                    identification in Zone 1
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
					<p class="number">PAB-0021</p>
					<p>
                    Do not display the patient's photograph in the 
                    Patient Banner
                    </p>
					<p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="midline">
                <div class="landscape">
    				<img src="../images/pab0003.png" alt="Zone 2 - Supplementary Information. Lower section of a patient banner containing a (truncated) address, telephone number, two empty sections and the text &#39;Known Allergies&#39; with an icon" />
                </div>
                <div class="guideleft">
					<p class="number">PAB-0003</p>
					<p>
                    Display supplementary information that either supports 
                    patient identification or assists patient care in Zone 2
                    </p>
					<p class="mandatory">Mandatory</p>
                 </div>
                <div class="guidetext">
                    <p class="number">PAB-0008</p>
                    <p>
                    The Patient Banner adheres to role-based access control, for example, do not display
                    clinical information such as allergy propensities, to non-clinical users
                    </p>
                    <p class="mandatory">Mandatory</p>
                </div>
             </div>
            
        </div>
    </div>
    
</asp:Content>
