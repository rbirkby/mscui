<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="definitions.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.GenderSex.Input.Sex.Definitions" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
   <div id="page">
        <div id="guidance">            
            
			<div class="line">
				<div class="illustration">
					<img src="../../images/cds0015.png" alt="An option button group with the mouse cursor over the option &#39;Indeterminate&#39; a tooltip displaying an example definition for &#39;Indeterminate&#39" />
				</div>
				<div class="guidetext">
					<p class="number">CGS-0015</p>
					<p>
                    Make the definitions of the Sex status 
                    values accessible to the user
                    </p>
					<p class="mandatory">Mandatory</p>
                    <p class="note">
                    <strong>Note</strong>
                    The tooltip is one example of how a definition 
                    might be displayed.  The display style and definition 
                    are not part of the guidance
                    </p>
				</div>
			</div>
            
			<div class="line">
				<div class="illustration">
					<table class="datavalues">
                        <tr><td class="term">Not Known</td>
                            <td>
                            Used when no information on this subject is known
                            </td>
                        </tr>
                        <tr><td class="term">Indeterminate</td>
                            <td>
                            Used when the person is unable to be classified 
                            as either male or female
                            </td>
                        </tr>
                    </table>
                </div>
				<div class="guidetext">
                    <p class="note">
                    <strong>Note</strong> These definitions of Sex 
                    are examples only and do not form part of guidance.
                    </p>
				</div>
			</div>
            
        </div>
    </div>
</asp:Content>
