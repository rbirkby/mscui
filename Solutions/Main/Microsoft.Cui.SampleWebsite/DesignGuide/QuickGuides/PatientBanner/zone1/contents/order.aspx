<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="order.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Zone1.Contents.Order" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           

			<div class="midline">
				<div class="landscape">
					<img src="../../images/pab0076.png" alt="Zone 1 of a patient banner with tab stops marked: 1.Name, 2.Date of Birth, 3.Gender 4.Patient Identification Number" />
				</div>
				<div class="guideleft">
					<p class="number">PAB-0076</p>
					<p>
                    Enable a user to tab between the patient identification data in the 
                    same order as the displayed information as follows: the patient&#39;s 
                    name (family name, given name and title), date of birth, gender and 
                    patient identification number
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
                <div class="guidetext">
					<p class="number">PAB-0016</p>
					<p>
                    Always display the patient&#39;s name (family name, given name and title), 
                    date of birth, gender and patient identification number in this order within 
                    the Patient Banner
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
             </div>

            <div class="line">
            	<div class="illustration">
					<img src="../../images/pab0049.png" alt="Diagram of a patient&#39;s name in which &#39;CHANDRASEKHAR&#39; is labelled &#39;Family Name&#39;, &#39;Subramanyan&#39; is labelled &#39;Given Name&#39; and &#39;(Mr)&#39; is labelled &#39;Title&#39;" />
				</div>
                <div class="guidetext">
					<p class="number">PAB-0049</p>
					<p>
                    Display the patient name elements and the title in the 
                    following order: family name, given name, title
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
            </div>
            
        </div>
    </div>
</asp:Content>
