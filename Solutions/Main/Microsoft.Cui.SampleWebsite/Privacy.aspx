<%@ Page Language="C#" MasterPageFile="~/DefaultMaster.master" AutoEventWireup="true" Inherits="Microsoft.Cui.SampleWebsite.Privacy" %>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="pageSpecificHeadTags">
    <style type="text/css">
        .section
        {
            overflow: hidden; /* New block formatting context */
        }
        .section ul
        {
            font-weight: bold;
        }
        .section ul ul
        {
            font-weight: normal;
        }
        .section ul ul ul
        {
            list-style-type: square;
            font-weight: normal;
            margin-left: 20px;
        }
        .section ul ul ul li
        {
            margin-bottom: 6px;
        }
        .videoSection
        {
            float: right;
            background-color: #052844;
            position: relative; /* Provide a positioned ancestor for the image */
            min-height: 176px;
            overflow: hidden;
        }
        .videoHeading
        {
            width: 163px;
            margin: 0 2px 0 0;
            color: #fff;
            text-align: center;
            cursor: pointer;
            display: block;
            padding: 125px 0 10px 65px;
            position: relative; /* Make positioned so it draw above the image */
        }
        .videoSection a
        {
            text-decoration: none;
            display: block;
        }
        .videoSection a img
        {
            border: none;
            position: absolute; /* Take the image out of the flow */
        }
        h1
        {
            margin: 0 0 8px 8px;
        }
        .relatedResources
        {            
            border-bottom-style: none;
            border: solid 1px #CCC;
            margin:0 4px 0 0;
        }
        .relatedResources p
        {
            background-color: #385484;
        }
        .relatedResources div
        {
            padding: 4px 4px 4px 8px;
        }
        .relatedResources table
        {
            border-collapse: collapse;
            width: 100%;
        }
        .relatedResources table thead
        {
            background-color: #57739B;
            color: White;
            border: solid 1px #888;
            text-transform: none;
            text-align: left;
        }
        .toptitle
        {
            background-color: #385484;
        }
        .blankheader
        {
            height: 2px;
            background-color: White;
        }
        .relatedResources table tbody td
        {
            background-color: #e4e4e4;
            border: solid 1px #ccc;
            padding: 2px 10px 2px 10px;
        }
        .relatedResources table th
        {
            color: White;
            padding-left: 20px;
            font-weight: normal;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="siteContentPlaceHolder" runat="server"> 
 
    
    <div class="section">    
    <h2>Microsoft Health Common User Interface Site Privacy Statement</h2>
    <h2>(Last updated: 10th November 2009)</h2>
    <p>
        Microsoft is committed to protecting your privacy. This Web site does not request or collect any personal information from you. This site may collect certain information about your visit, such as the name of the Internet service provider and the Internet Protocol (IP) address through which you access the Internet; the date and time you access the site; the pages that you access while at the site and the Internet address of the Web site from which you linked directly to our site. This information is used for the operation and improvement of the site. 
    </p>
    <p>
        Please note that this privacy statement applies only to the Microsoft Health Common User Interface Site. It does not apply to other online or offline Microsoft sites, products or services. 
    </p>   
    <p>
        This site may contain links to other sites and services. We encourage you to review the privacy statements of those sites and services that you choose to visit, so that you can understand how they may collect, use and share your personal information. Microsoft is not responsible for the privacy statements or practices of sites and services controlled by other companies or organizations.
    </p>
    </div>

</asp:Content>
