<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Default.master" Title="Patient Banner Tests" CodeFile="PatientBannerTests.aspx.cs" Inherits="PatientBannerTests" %>
<%@ Register Assembly="NhsCui.Toolkit.Web" Namespace="NhsCui.Toolkit.Web" TagPrefix="NhsCui" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        
    <NhsCui:PatientBanner ID="patientBanner"
        FamilyName="Evans" GivenName="Jonathan" Title="Mr"
        Identifier="4372623623" KnownAs="Junior" Gender="Male"
        Carer="Dr Clift" DependencyStatus="Infant" 
        HomePhoneNumber="(01632) 960154" WorkPhoneNumber="(020) 7946 0901"
        MobilePhoneNumber="(07700) 900949" EmailAddress="jane.evans@hotmail.com"
        Address1="65 Acacia Drive" Address2="Hedle End" Town="Coventry"
        County="Warwickshire" PostCode="C48 9DB" AccessKey="W"
        DateOfBirth="01-Jan-2006" runat="server">

   </NhsCui:PatientBanner>

    <script type="text/javascript">    
        // Script objects that should be loaded before we run
        var typeDependencies = null;
    
        // Test Harness
        var testHarness = null;
        
        // patient banner control
        var patientBanner;
        // zone1
        var zone1;
        // zone2 permanently shown area
        var zone2Permanent;
        // zone2 non permanently shown area
        var zone2NonPermanent;
        // whether expand completed event received
        var expandCompleted;
        // whether collapse completed received
        var collapseCompleted;

        // Register the tests
        function registerTests(harness) 
        {
            var pollInterval = 200, timeout = 10000;
            testHarness = harness;

            // Get the controls on the page
            
            patientBanner = $find('ctl00_ContentPlaceHolder1_patientBanner');
            zone1 = $get('ctl00_ContentPlaceHolder1_patientBanner_zoneOne');
            zone2Permanent = $get('ctl00_ContentPlaceHolder1_patientBanner_zoneTwoPermanent');
            zone2NonPermanent = $get('ctl00_ContentPlaceHolder1_patientBanner_zoneTwoNonPermanent');
            
            // register to receive events
            patientBanner.add_expandComplete(function() { expandCompleted = true; });
            patientBanner.add_collapseComplete(function() { collapseCompleted = true; });

            var test = testHarness.addTest('Initial');
            // zone 2 should be initially collapsed
            test.addStep(function() { zone2NonPermanent.style.display === "none"; });
            
            test = testHarness.addTest('Expand / Collapse Zone 2');
            test.addStep(function() { zone2NonPermanent.style.display === "none"; });
            test.addStep(function() { expandCompleted = false; collapseCompleted = false;});
            test.addStep(function() { patientBanner.set_zoneTwoExpanded(true); }, 
                            function() { return zone2NonPermanent.style.display !== "none" && expandCompleted; }, 
                            pollInterval, timeout);
            test.addStep(function() { patientBanner.set_zoneTwoExpanded(false); }, 
                            function() { return zone2NonPermanent.style.display === "none" && collapseCompleted;}, 
                            pollInterval, timeout);

        }

    </script>

</asp:Content>