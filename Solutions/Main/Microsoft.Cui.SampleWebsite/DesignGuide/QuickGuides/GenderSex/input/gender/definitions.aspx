<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="definitions.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.GenderSex.Input.Gender.Definitions" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
   <div id="page">
        <div id="guidance">            
            
			<div class="line">
				<div class="illustration">
					<img src="../../images/cds0004.png"  
					alt="An option button group with the mouse cursor over the option &#39;Other Specific&#39; a tooltip displaying an example definition for &#39;Other Specific&#39" />
				</div>
				<div class="guidetext">
					<p class="number">CGS-0004</p>
					<p>
                    Make the definitions of the Current 
                    Gender status values accessible to the user
                    </p>
					<p class="mandatory">Mandatory</p>
                    <p class="note">
                    <strong>Note</strong> The tooltip is one example 
                    of how a definition might be displayed.  This example 
                    contains a definition of &#39;Other Specific&#39; that is not 
                    part of the guidance
                    </p>
				</div>
			</div>
            
			<div class="line">
				<div class="illustration">
					<table class="datavalues">
                        <tr><td class="term">Other Specific</td>
                            <td>
                            When the person has a clear idea of what their gender 
                            is, but is neither discretely male nor female, for 
                            example, &#39;intersex&#39;, &#39;transgender&#39;, &#39;third gender&#39;
                            </td>
                        </tr>
                        <tr><td class="term">Not Known</td>
                            <td>
                            Used when no information on this subject is known
                            </td>
                        </tr>
                        <tr><td class="term">Not Specified</td>
                            <td>
                            When the person is unable to specify their current 
                            gender or does not have a clear idea of their current 
                            gender
                            </td>
                        </tr>
                    </table>
				</div>
				<div class="guidetext">
                    <p class="note">
                    <strong>Note</strong> These definitions of Current Gender 
                    are examples only and do not form part of guidance.
                    </p>
				</div>
			</div>
        </div>
    </div>
</asp:Content>
