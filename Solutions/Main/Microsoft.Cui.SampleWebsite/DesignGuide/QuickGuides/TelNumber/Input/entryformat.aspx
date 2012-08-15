<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="entryformat.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.TelNumber.Input.EntryFormat" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
			<div class="line">
				<div class="illustration">
					<img src="../images/tid0012a.png" alt="A three step diagram showing the entry of a number without spaces or formatting that is then re-displayed with spaces on entry" />
					<img src="../images/tid0012b.png" alt="A three step diagram showing the entry of a number with hyphens that is then re-displayed with spaces instead of hyphens" />
				</div>
				<div class="guidetext">
					<p class="number">TID-0012</p>
					<p>
                    Ensure the input box accepts formatted and unformatted entries
                    </p>
					<p class="mandatory">Mandatory</p>
				</div>
			</div>
         </div>
    </div>
</asp:Content>
