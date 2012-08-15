<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="allergies.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Zone2.Labels.Allergies" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           
            
			<div class="midline">
				<div class="illustration">
					<table class="datavalues">
                        <tr><td>Known allergies</td></tr>
                        <tr><td>No known allergies</td></tr>
                        <tr><td>Allergies not recorded</td></tr>
                        <tr><td>Allergies unavailable</td></tr> 
                    </table>
				</div>
				<div class="guidetext">
					<p class="number">PAB-0064</p>
					<p>
                    Use one of the labels in the table on the left in 
                    the title of the Allergies section
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
            </div>
            
			<div class="midline">
				<div class="illustration">
					<table class="datavalues">
                        <tr><td class="highlight">Known allergies</td></tr>
                        <tr><td class="highlight">No known allergies</td></tr>
                        <tr><td class="highlight">Allergies not recorded</td></tr>
                        <tr><td>Allergies unavailable</td></tr> 
                    </table>
				</div>
				<div class="guidetext">
					<p class="number">PAB-0066</p>
					<p>
                    Provide a means to enable the user to view the section 
                    of the record containing Allergy propensity information, 
                    for all instances when the section title is one of the three 
                    highlighted in this table
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
            </div>
            
			<div class="midline">
				<div class="illustration">
					<img src="../../images/pab0067.png" alt="A table of four rows each containing an icon followed by text. The first row is in larger text and has a callout labelled &#39;emphasised&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0067</p>
					<p>
                    Emphasise the label &#39;Known allergies&#39; in relation to 
                    the other permitted labels
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
             </div>           
            
			<div class="line">
				<div class="illustration">
					<img src="../../images/pab0068.png" alt="A table of four rows each containing an icon followed by text. A callout labelled &#39;data text style&#39;points to the first two rows in which the text is bold and a second callout labelled &#39;label text style&#39; points to the second two rows in which the text is italic" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0068</p>
					<p>
                    Display the labels &#39;Known allergies&#39; and 
                    &#39;No known allergies&#39; in data text style
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">PAB-0071</p>
					<p>
                    Display the labels &#39;Allergies not recorded&#39; and 
                    &#39;Allergies unavailable&#39; in label text style
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
             </div>   
                       
        </div>
    </div>
</asp:Content>
