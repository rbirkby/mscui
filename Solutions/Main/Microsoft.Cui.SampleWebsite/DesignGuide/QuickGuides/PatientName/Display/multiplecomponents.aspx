<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    Codebehind="multiplecomponents.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.PatientName.Display.MultipleComponents" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">           

            <div class="line">
            	<div class="illustration">
                	<img src="../images/nid0012.png" alt="A Patient Name with a two-part Family Name &#39;EVANS WEST, Jonathan (Sir)&#39; and one with a two-part Given Name: &#39;SMITH, Mary Jane (Mrs)&#39;" />
                </div>
                <div class="guidetext">
                <p class="number">NID-0012</p>
                <p>
                The display must allow for the Family Name and Given Name 
                elements to consist of multiple components. Components are 
                constituent parts of the name element that combine with 
                other parts to form the element as a whole
                </p>
                <p>
                Components have the following features:
                </p>
                <ul>
                    <li>
                    Family Name components must consist of UPPERCASE
                    alphabetic characters only, for example, SMITH
                    </li>
                    <li>
                    Multiple Family Name components must be separated by a 
                    hyphen or a single space
                    </li>
                    <li>
                    Given Name components must display in title case, for 
                    example, Nadejda
                    </li>
                    <li>
                    Multiple Given Name components must be separated 
                    by a hyphen or a single space
                    </li>
                </ul><br /> 
                <p class="mandatory">Mandatory</p>
                </div>
            </div>
            
        </div>
    </div>  
</asp:Content>
