<%@ Page Language="C#" MasterPageFile="~/LeafPage.Master" AutoEventWireup="true"
    Codebehind="IconsSymbology.aspx.cs" EnableViewState="false" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.IconsSymbology"
    Title="Guidance - Icons and Symbology" %>

<asp:Content ID="Content1" ContentPlaceHolderID="leafPageContent" runat="server">
    <div class="first section">
        <div class="downloadBoxBig">
            <h1>
                Download</h1>
            <%-- Use server-control to auto-escape the spaces --%>
           <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Pdfs/Design Guidance Exploration -- Icons and Symbology.pdf"
                Target="_blank" ToolTip="Links to Design Guidance Exploration - Icons and Symbology documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance Exploration &ndash; Icons and Symbology (PDF&nbsp;format)</span>
            </asp:HyperLink>
            <hr/>
             <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="Pdfs/Design Guidance Exploration -- Alert Symbol Design.pdf"
                Target="_blank" ToolTip="Links to Design Guidance Exploration - Alert Symbol Design documentation">
                <img src="../images/SFTheme/pdf.png" alt="PDF Download" />
                <span>Design Guidance Exploration &ndash; Alert Symbol Design (PDF&nbsp;format)</span>
            </asp:HyperLink>            
        </div>
        <h1>
            Groundwork &ndash; Icons and Symbology</h1>
        <h2>
            Introduction</h2>
        <p>
            	The design groundwork exploration in the <i>Design Guidance Exploration &ndash; Icons and Symbology</i> document provides you with design exploration and recommendations for displaying icons 
		and symbols in clinical applications. It enables unambiguous display of icons and symbols, while enhancing patient safety and 
		clinical application usability, by:
        </p>

	<ul>
		<li>Ensuring a consistent visual representation of icons and symbols</li>
		<li>Facilitating fast, accurate and intuitive interpretation of icons and symbols, in a manner which users find acceptable</li>
		<li>Complying with internationalization requirements and accessibility standards</li>
	</ul>
	<p>
		The design groundwork exploration is broken down further to provide recommendations for displaying alert symbols in the document 
		<i>Design Guidance Exploration &ndash; Alert Symbol Design</i>. This includes advice on defining a visual syntax for alert symbols, including the representation of prohibitions, mandatory actions, 
		warnings and suggested actions.
	</p>
	</div>
         <div class="update section">
		<label>Note:</label>
		<p>The ideas presented in these documents are for community preview and consultation only. Further design and patient safety assessments 
		are required to finalize the content as CUI Design Guidance.
	</p>

        </div><div class="last section">
        <h2>
            	Summary
        </h2>

	<p>
		The design exploration focuses on the key issues relating to the usage of image-related, concept-related and arbitary icons within clinical applications, 
		and includes recommendations for:	
	</p>
	<ul>
		<li>Identifying how to display icons and symbology, including the role that they should play in the application, the level of detail to display and the format</li>
		<li>Using icons and symbology, including how to convey different levels of intensity and importance</li>
		<li>Developing an icon grammar, including the design of basic symbols for vocabulary and the combining of symbols</li>
	</ul>
       </div>
</asp:Content>
