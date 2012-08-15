<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master" Codebehind="appendix.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientBanner.Appendix" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="overview">            
            <p>
            Display the patient&#39;s age using the values and formats detailed here. 
            These take into account the range of patient ages whilst economising 
            on the display space used.
            </p>
            <table class="datavalues" title="Units to be used for displaying a patient's age">
                <tr>
                    <th>Age</th>
                    <th>Lower Unit</th>
                    <th>Higher Unit</th>
                </tr>
                <tr>
                    <td>&lt; 2 hours</td>
                    <td>Minutes</td>
                    <td>Minutes</td>
                </tr>
                <tr>
                    <td>&lt; 2 days</td>
                    <td>Hours</td>
                    <td>Hours</td>
                </tr>
                <tr>
                    <td>&lt; 4 weeks</td>
                    <td>Days</td>
                    <td>Days</td>
                </tr>
                <tr>
                    <td>&lt; 1 year</td>
                    <td>Weeks</td>
                    <td>Days</td>
                </tr>
                <tr>
                    <td>&lt; 2 years</td>
                    <td>Months</td>
                    <td>Days</td>
                </tr>
                <tr>
                    <td>&lt; 18 years</td>
                    <td>Years</td>
                    <td>Months</td>
                </tr>
                <tr>
                    <td>&gt;&#61; 18 years</td>
                    <td>Years</td>
                    <td>Years</td>
                </tr>
            </table>
            <p class="extrahigh">Examples of the display of a patient&#39;s age:</p>

            <table class="datavalues" title="Units to be used for displaying a patient's age">
                <tr>
                    <th>Age</th>
                    <th>Lower Unit</th>
                    <th>Higher Unit</th>
                    <th>Display</th>
                </tr>
                <tr>
                    <td>1 hour 30 minutes</td>
                    <td>Minutes</td>
                    <td>Minutes</td>
                    <td>90min</td>
                </tr>
                <tr>
                    <td>1 day 2 hours 5 minutes</td>
                    <td>Hours</td>
                    <td>Hours</td>
                    <td>26hrs</td>
                </tr>
                <tr>
                    <td>3 days 17 hours 7 minutes</td>
                    <td>Days</td>
                    <td>Days</td>
                    <td>3d</td>
                </tr>
                <tr>
                    <td>27 days 5 hours 2 minutes</td>
                    <td>Days</td>
                    <td>Days</td>
                    <td>27d</td>
                </tr>
                <tr>
                    <td>28 days 5 hours 2 minutes</td>
                    <td>Weeks</td>
                    <td>Days</td>
                    <td>4w</td>
                </tr>
                <tr>
                    <td>29 days 5 hours 2 minutes</td>
                    <td>Weeks</td>
                    <td>Days</td>
                    <td>4w 1d</td>
                </tr>
                <tr>
                    <td>1 year 1 day 5 hours</td>
                    <td>Months</td>
                    <td>Days</td>
                    <td>12m 1d</td>
                </tr>
                <tr>
                    <td>1 year 8 days 5 hours</td>
                    <td>Months</td>
                    <td>Days</td>
                    <td>12m 8d</td>
                </tr>
                <tr>
                    <td>1 year 39 days 5 hours</td>
                    <td>Months</td>
                    <td>Days</td>
                    <td>13m 8d</td>
                </tr>
                <tr>
                    <td>4 years 39 days</td>
                    <td>Years</td>
                    <td>Months</td>
                    <td>4y 1m</td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>