<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="input.aspx.cs" MasterPageFile="~/QIGs.Master" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.Address.InputPage" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">
            <p>
            There are three types of address input covered by the guidance:
            </p>
            <img src="images/ukinput.png" class="captioned" alt="A form with labels and input controls for Line 1, Line 2, Town &#047; City, County and Postcode. Postcode is followed by a &#39;Find Postcode&#39; button." />
            <p class="caption">UK Address Input</p>
            <img src="images/ukfinder.png" class="captioned" alt="A form with labels and input controls for House &#47; Building Number, House &#47; Building Name and Postcode. Postcode is followed by a &#39;Find Address&#39; button." />
            <p class="caption">UK Address Finder</p>
            <img src="images/nonukinput.png" class="captioned" alt="A form with a label and drop-down control for Country followed by labels and input controls for Line 1, Line 2, Line 3, Line 4, Town &#47; City and Postcode. Postcode is followed by a &#39;Find Postcode&#39; button." />
            <p class="caption">Non-UK Address Input</p>
        </div>
    </div>
</asp:Content>
