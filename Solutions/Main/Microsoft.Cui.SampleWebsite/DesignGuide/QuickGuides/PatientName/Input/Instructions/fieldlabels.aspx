<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="fieldlabels.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Input.Instructions.FieldLabels" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           

            <div class="midline">
            	<div class="illustration">
                	<img src="../../images/nid0052.png" alt="An input form with the following labels to the left of each input control: Title, FAMILY name, Given name, Middle name(s), Suffix, Known as."" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0052</p>
                <p>
                Each field in a name input control must have an associated label
                </p>
                <p class="mandatory">Mandatory</p>
                <p class="number">NID-0053</p>
                <p>
                Labels must be programmatically linked to their associated input field
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>

            <div class="line">
            	<div class="illustration">
                    <table class="datavalues">
                        <tr>
                            <th>Input Control</th>
                            <th>Label</th>
                        </tr>
                        <tr>
                            <td>Title</td>
                            <td>Title</td>
                        </tr>
                        <tr>
                            <td>Family Name</td>
                            <td>Family Name</td>
                        </tr>
                        <tr>
                            <td>Given Name</td>
                            <td>Given Name</td>
                        </tr>
                        <tr>
                            <td>Middle Name</td>
                            <td>Middle Name(s)</td>
                        </tr>
                        <tr>
                            <td>Suffix</td>
                            <td>Suffix</td>
                        </tr>
                        <tr>
                            <td>Preferred Name</td>
                            <td>Known as</td>
                        </tr>
                    </table>
                </div>
                <div class="guidetext">
                <p class="number">NID-0054</p>
                <p>
                Labels values should be those illustrated
                </p>
                <p class="recommended">Recommended</p>
                </div>
            </div>


        </div>
    </div>
</asp:Content>
