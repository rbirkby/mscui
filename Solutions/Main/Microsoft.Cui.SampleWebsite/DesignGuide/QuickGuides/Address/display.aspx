<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="display.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.DisplayPage" MasterPageFile="~/QIGs.Master" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            There are two forms of address display supported by this 
            guidance that apply to both UK and non-UK addresses:
            </p>
            <img src="images/inform.png" class="captioned" alt="An example address with each address element displayed on a new line" />
            <p class="caption">In-form or vertically aligned address</p>
            <img src="images/inline.png" class="captioned" alt="An example address displayed as a sentence with each element separated by a comma" />
            <p class="caption">In-line or horizontally aligned address</p>
        </div>
    </div>
</asp:Content>