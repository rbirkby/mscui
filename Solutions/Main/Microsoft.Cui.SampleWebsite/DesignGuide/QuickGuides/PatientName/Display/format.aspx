<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="format.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Display.Format" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">            
            
            <div class="midline">
                <div class="illustration">
                    <img src="../images/nid0013.png" alt="A patient name that wraps over two lines: &#39;SUBRAMANYAN,&#39; on the first line and &#39;Chandrasekhar (Mr)&#39; on the second" />
                </div>
                <div class="guidetext">
                    <p class="number">
                        NID-0013</p>
                    <p>
                        The display should allow word wrapping to occur in instances where the field length
                        exceeds the width allocated to it on the form. If word wrapping occurs, it should
                        be applied only at the end of a whole field element or at the end of a field element
                        component, if it comprises multiple parts (for example, Middle name(s) field)
                    </p>
                    <p class="recommended">Recommended</p>
                </div>
            </div>
            
            <div class="line">
                <div class="illustration">
                    <img src="../images/nid0011.png" alt="A Patient Name with each element (Family Name, Given Name and Title) marked using horizontal brackets" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0011</p>
                <p>
                The display must allow the Family Name, Given Name 
                and Title elements to present at least the maximum 
                field sizes specified in this guidance
                </p>
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
        </div>
    </div>
</asp:Content>
