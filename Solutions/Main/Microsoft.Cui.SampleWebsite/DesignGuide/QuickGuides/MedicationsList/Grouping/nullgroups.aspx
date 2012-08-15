<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="nullgroups.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Grouping.NullGroups" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="line">
				<div class="landscape">
					<img src="../images/medv091.png" alt="A grouped list with three group headings in which the first group heading &#39;(No Form Specified)&#39; has a callout labelled: &#39;null&#39; group" />
				</div>
				<div class="guideleft">
					<p class="number">MEDv-091</p>
					<p>
                    Provide &#39;null&#39; groups where necessary to support 
                    the display of medications that do not have a value 
                    for the attribute being used to group the medications
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
				<div class="guidetext">
					<p class="number">MEDv-194</p>
					<p>
                    Display the label for a &#39;null&#39; group heading in brackets
                    </p>
					<p class="mandatory">Mandatory</p>
					<p class="number">MEDv-195</p>
					<p>
                    Display &#39;null&#39; groups at the top of the list of groups
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
            
        </div>
    </div>      
</asp:Content>
