<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="inline.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Input.Layout.InLine" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           
            
            <div class="line">
            	<div class="landscape">
                	<img src="../../images/nid0049.png" alt="An input form with the following input controls laid out horizontally with labels above each control: Title, FAMILY name, Given name, Middle name(s), Suffix, Known as." />
                </div>
                <div class="guidealone">
                <p class="number">NID-0049</p>
                <p>
                Ensure wrapping only occurs on whole fields
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">NID-0050</p>
                <p>
                Correct presentation order is as illustrated
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">NID-0051</p>
                <p>
                In-line design choice should only be used when 
                <span class="nowrap">in-form</span> has been considered undesirable
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
