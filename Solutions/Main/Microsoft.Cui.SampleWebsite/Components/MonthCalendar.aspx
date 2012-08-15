<%@ Page Language="C#" MasterPageFile="~/LeafPage.master" AutoEventWireup="true"
    Inherits="ComponentsMonthCalendar" Title="Untitled Page" CodeBehind="MonthCalendar.aspx.cs" %>

<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="DefaultContent" ContentPlaceHolderID="leafPageContent" runat="Server">
    <div class="demoarea first section">
        <SS:PageTitleControl ID="PageTitleControl1" runat="server"></SS:PageTitleControl>
        The MonthCalendar control allows an exact date to be entered unambiguously. The
        MonthCalendar control provides the user with a simple way of entering dates.
    </div>
    <ajaxToolkit:TabContainer runat="server" ID="Tabs" ActiveTabIndex="0" Width="770px">
        <ajaxToolkit:TabPanel runat="server" ID="panelASPNET" HeaderText="<a id='monthCalendarASPNETTab' href=javascript:TabClick('monthCalendarASPNETTab'); title='ASP.NET Tab' alt='ASP.NET Tab'>ASP.NET</a>">
            <ContentTemplate>
                <br />
                Example ASP.NET control (embedded):
                <br />
                <br />
                <table>
                    <tr>
                        <td style="background-color: #e4e4e4;">
                            <asp:Panel CssClass="demoControlarea" ID="demoPanel1" runat="server">
                                <asp:Panel ID="MonthCalendar1" runat="server">
                                </asp:Panel>
                                <NhsCui:MonthCalendarExtender TargetControlID="MonthCalendar1" runat="server" ID="MonthCalendarExtender1"
                                    Enabled="True" Value="25-Nov-2008">
                                </NhsCui:MonthCalendarExtender>
                            </asp:Panel>
                            <asp:Panel CssClass="demoControlarea" ID="demoPanel2" runat="server" Style="display: none">
                                <asp:Panel ID="MonthCalendar2Holder" runat="server">
                                </asp:Panel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <div class="resetFloatAfterdemoCCArea">
                </div>
                <p>
                    The MonthCalendar control supports dynamic creation and disposal via client-side
                    JavaScript code. Use these two buttons to see this working.
                </p>
                <input type="button" id="createButton" onclick="createSecondInstance(this)" value="Create"
                    title="Create a new instance dynamically" />
                <input type="button" id="disposeButton" onclick="disposeSecondInstance(this)" value="Dispose"
                    disabled="disabled" title="Dispose the newly created MonthCalendar control" />
                <table class="monthCalendarTable">
                    <tr>
                        <th>
                            <asp:Label ID="MonthCalendar1Text" runat="server" Text="First MonthCalendar value:"></asp:Label>
                        </th>
                        <td>
                            <asp:Label ID="MonthCalendar1Value" runat="server" Text="-"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="MonthCalendar2Text" runat="server" Text="Second MonthCalendar value:"
                                Style="display: none"></asp:Label>
                        </th>
                        <td>
                            <asp:Label ID="MonthCalendar2Value" runat="server" Text="-" Style="display: none"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <!-- Area for Usage Hints -->
                <asp:Panel ID="UsageHints_HeaderPanel" runat="server" Style="cursor: pointer;">
                    <div class="heading">
                        <input type="image" id="usageHints_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                        Usage Hints
                    </div>
                </asp:Panel>
                <asp:Panel ID="UsageHints_ContentPanel" runat="server" Style="overflow: hidden;">
                    <div class="section">
                        <p>
                            You can enter a date directly in the MonthCalendar by:
                        </p>
                        <ul>
                            <li>Clicking on a day to set the date</li>
                            <li>Clicking Today to set the current date</li>
                            <li>Using your arrow keys to navigate through years, months and days and then pressing
                                your enter key to select a date</li>
                            <li>Clicking the left and right arrows to navigate through months and years</li>
                        </ul>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeUsageHints" runat="server" TargetControlID="UsageHints_ContentPanel"
                    ExpandControlID="UsageHints_HeaderPanel" CollapseControlID="UsageHints_HeaderPanel"
                    Collapsed="True" ImageControlID="usageHints_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                    ExpandedText="Click to collapse the Usage Hints section" CollapsedImage="~/images/SFTheme/acc_h.png"
                    CollapsedText="Click to expand the Usage Hints section" SuppressPostBack="True"
                    Enabled="True" />
                <!-- Area for Properties -->
                <asp:Panel ID="Properties_HeaderPanel" runat="server" Style="cursor: pointer;">
                    <div class="heading">
                        <input type="image" id="properties_ToggleImage" runat="server" src="~/images/SFTheme/acc_v.png" />
                        Properties
                    </div>
                </asp:Panel>
                <asp:Panel ID="Properties_ContentPanel" runat="server" Style="overflow: hidden;"
                    Height="0px">
                    <div class="last section">
                        <p>
                            The MonthCalendar control is initialized with this code:</p>
                        <pre>&lt;NhsCui:MonthCalendar ID="MonthCalendar1" runat="server" /&gt;
            </pre>
                        <ul>
                            <li><strong>Value</strong> &ndash; the date selected in the control</li>
                        </ul>
                    </div>
                </asp:Panel>
                <ajaxToolkit:CollapsiblePanelExtender ID="cpeProperties" runat="server" TargetControlID="properties_ContentPanel"
                    ExpandControlID="properties_HeaderPanel" CollapseControlID="properties_HeaderPanel"
                    Collapsed="True" ImageControlID="properties_ToggleImage" ExpandedImage="~/images/SFTheme/acc_v.png"
                    ExpandedText="Click to collapse the Properties section" CollapsedImage="~/images/SFTheme/acc_h.png"
                    CollapsedText="Click to expand the Properties section" SuppressPostBack="True"
                    Enabled="True" />
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>

    <script type="text/javascript">
        // Global variables for the Controls
        var monthCalendar1Input;
        var monthCalendar1Label;

        var monthCalendar2Text;
        var monthCalendar2Label;

        // refreshing labels on property change
        function onPropertyChange(sender, args) {
            refreshDateLabels();
        }

        // on app load function adds property change handles
        function pageLoad() {
            // get control refrences
            monthCalendar1Input = $get('<%=MonthCalendar1.ClientID%>').MonthCalendar;
            monthCalendar1Label = $get("<%=MonthCalendar1Value.ClientID%>");

            monthCalendar2Label = $get("<%=MonthCalendar2Value.ClientID%>");
            monthCalendar2Text = $get("<%=MonthCalendar2Text.ClientID%>");

            // date change handlers
            monthCalendar1Input.add_propertyChanged(onPropertyChange);

            // refresh date time labels
            refreshDateLabels();
        }

        function refreshDateLabels() {
            monthCalendar1Label.replaceChild(document.createTextNode(monthCalendar1Input.get_value().toString()), monthCalendar1Label.firstChild);
            if (this.secondControl) {
                monthCalendar2Label.replaceChild(document.createTextNode(this.secondControl.get_value().toString()), monthCalendar2Label.firstChild);
            }
        }

        // Will be called by Atlas automatically when page is Unloaded
        function pageUnload() {
            monthCalendar1Input.remove_propertyChanged(onPropertyChange);
        }

        function createSecondInstance(buttonSender) {
            // The following code works here because the first instance of the MonthCalendarExtender
            // above brings down all the required JavaScript script and resource files for 
            // the MonthCalendar and the types it is dependent upon e.g. NhsDate
            // would need to arrange for these to be available if creating from scratch client-side

            // Create a second MonthCalendar dynamically using 
            // clientside-only code and set it to today's date...
            var secondMonthCalHolderDiv = $get("<%=MonthCalendar2Holder.ClientID%>");
            var disposeButton = $get("disposeButton");
            if (secondMonthCalHolderDiv) {
                this.newMonthCalendarDiv = document.createElement("div");
                this.newMonthCalendarDiv.id = "newMonthCalendarDiv"
                secondMonthCalHolderDiv.appendChild(newMonthCalendarDiv);

                this.secondControl = $create(NhsCui.Toolkit.Web.MonthCalendar,
                    { id: "MonthCalendarExtender2", value: new NhsDate(new Date()) },
                    null, null, newMonthCalendarDiv);
                buttonSender.disabled = true;
                disposeButton.disabled = false;

                monthCalendar2Label.style.display = "inline";
                monthCalendar2Text.style.display = "inline";

                $get("<%=demoPanel2.ClientID %>").style.display = "";

                // date change handlers
                this.secondControl.add_propertyChanged(onPropertyChange);
                refreshDateLabels();
            }
        }

        function disposeSecondInstance(buttonSender) {
            if (this.secondControl != null) {
                var secondMonthCalHolderDiv = $get("<%=MonthCalendar2Holder.ClientID%>");
                secondMonthCalHolderDiv.removeChild(this.newMonthCalendarDiv);

                var createButton = $get("createButton");
                this.secondControl.remove_propertyChanged(onPropertyChange);
                this.secondControl.dispose();
                this.secondControl = null;
                buttonSender.disabled = true;
                createButton.disabled = false;
                monthCalendar2Label.style.display = "none";
                monthCalendar2Text.style.display = "none";
                $get("<%=demoPanel2.ClientID %>").style.display = "none";
            }
        }

        function pageUnload() {
            monthCalendar1Input.remove_propertyChanged(onPropertyChange);
            if (this.secondControl) {
                this.secondControl.remove_propertyChanged(onPropertyChange);
            }
        } 
    </script>

</asp:Content>
