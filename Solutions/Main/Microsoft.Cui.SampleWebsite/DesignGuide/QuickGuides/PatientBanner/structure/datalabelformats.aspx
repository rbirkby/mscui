<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="datalabelformats.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Structure.DataLabelFormats" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">       
        
			<div class="midline">
				<div class="illustration">
					<img src="../images/pab0044.png" alt="Diagram of a patient identification number with the label &#39;NHS No.&#39; and the text &#39;129 728 7652&#39; in which a callout draws attention to the label text" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0044</p>
					<p>
                    Display labels in the style given to label text
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
            </div>

  
			<div class="midline">
				<div class="illustration">
					<img src="../images/pab0045.png" alt="Diagram of a patient identification number with the label &#39;NHS No.&#39; and the text &#39;129 728 7652&#39; in which a callout draws attention to the data text (the patient identification number)" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0045</p>
					<p>
                    Display values in the style given to data text
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">PAB-0046</p>
					<p>
                    Give more emphasis to the value text style 
                    relative to the label text style
                    </p>
					<p class="mandatory">Mandatory</p>
                </div>
            </div>
            
			<div class="midline">
				<div class="illustration">
					<img src="../images/pab0042.png" alt="Diagram of a date of birth with the label &#39;Born&#39; and the text &#39;14-Jul-1945 (61y)&#39; in which a callout draws attention to the space (and no colon) between the label and the date" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0042</p>
					<p>
                    Do not add a colon after the label text
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">PAB-0043</p>
					<p>
                    Do not include unnecessary punctuation in a label
                    </p>
					<p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="line">
				<div class="illustration">
					<img src="../images/pab0019.png" alt="Illustration showing a label of &#39;Phone and email&#39; followed by the text &#39;Not Known&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">PAB-0019</p>
					<p>
                    If an individual data item is not known, or is otherwise 
                    unavailable, a blank string or appropriate self explanatory 
                    text (such as &#39;Not Known&#39;, but not a &#39;?&#39;) is to be displayed 
                    immediately after the corresponding data label
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>

        </div>
    </div>
</asp:Content>
