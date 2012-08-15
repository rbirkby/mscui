<%@ Page Language="C#" MasterPageFile="~/Navigational.master" AutoEventWireup="true"
    CodeBehind="MSCUI.aspx.cs" EnableViewState="false" Inherits="IntroductionMscui" %>

<asp:Content ContentPlaceHolderID="navigationPageContent" runat="server">
    <div class="first section">
        <h1>
            What is Microsoft Health CUI? </h1>
        <p>
            To understand the reasoning behind Microsoft Health CUI, consideration needs to
            be given to clinical applications that are used within a healthcare setting, such
            as a doctor's practice, ambulance, hospital, or in the community.
        </p>
        <p>
            Healthcare professionals utilize a variety of clinical applications developed by
            different application providers, who have their own approach to user interface design.
            This results in an inherent level of inconsistency between user interfaces that
            has the potential for risk to the patient.
        </p>
        <img align="right" src="../images/what1.png" alt="Clinical Applications image" title="Clinical Applications image" style="padding: 0px 10px 0px 0px" />
        <p>
            To take a simple example, consider how a patient's name and date of birth could
            be displayed differently in three clinical applications. A healthcare professional
            might have difficulty identifying the same patient in each of these applications,
            and this could result in them administering the wrong care to that patient.
        </p>
        <p>
            This is where Microsoft Health CUI can help by promoting a Common User Interface
            (CUI) for clinical applications. Healthcare application developers can utilize two
            resources from Microsoft Health CUI that support them in developing safer, more
            consistent user interfaces.
        </p>
    </div>
    <div class="section">
        <h2>
        </h2>
        <p>
            Firstly, Microsoft Health CUI <a href="../DesignGuide/DesignGuide.aspx" title="Link to Design Guidance page">Design Guidance</a> provides a comprehensive set
            of user interface guidelines and recommendations. These can be used to evaluate the clinical safety of existing software 
            applications, as well as aid in the design of future clinical applications, with the intention to increase clinical effectiveness and improve patient safety.
        </p>
        <center>
            <img src="../images/what2.png" alt="Design Guidance image" title="Design Guidance image" style="padding: 0px 0px 0px 0px" /></center>
    </div>
    <div class="section">
        <h2>
          </h2>
        <p>
            Secondly, the Microsoft Health CUI <a href="../ControlsAndSamples.aspx" title="Link to Controls and Samples page">Toolkit</a> provides
            ready-made user interface controls that fully adopt the Design Guidance in a number
            of Microsoft technologies, including <a target="_blank" href="http://www.microsoft.com/silverlight/default.aspx"
                title="Link to Silverlight page on Microsoft.com (New Window)">Microsoft&reg; Silverlight</a> and <a target="_blank" href="http://windowsclient.net/WPF/"
                title="Link to Windows Presentation Foundation page on WindowsClient.NET (New Window)">
                Windows&reg; Presentation Foundation (WPF)</a>. 
        </p>
        <p>These controls can be customized and used within existing or new clinical applications.
        </p>
        <center>
            <img src="../images/what3.png" alt="Design Guidance image" title="Design Guidance image" style="padding: 0px 0px 0px 0px" /></center>
    </div>
    <div class="last section">
        <h2>
            </h2>
        <p>
            Microsoft Health CUI provides Design Guidance and Toolkit controls for many common clinical task-based scenarios, such as displaying patient personal data and allergies.  
        </p>
        <center>
            <img src="../images/what4.png" alt="Patient Banner image" title="Patient Banner image" style="padding: 0px 0px 0px 0px" /></center>
            <p>
            Future work includes:
            <ul>
                <li> Displaying tables and charts </li>
                <li> Terminology parsing </li>
                <li> Recording and displaying allergies </li>
                <li> Admissions noting </li>
                <li> Primary Care noting </li>
                <li> Handover</li>
            </ul>
        </p>        
   </div>
</asp:Content>
