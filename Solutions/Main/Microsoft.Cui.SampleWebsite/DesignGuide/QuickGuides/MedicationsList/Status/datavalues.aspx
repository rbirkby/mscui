<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/QIGs.master"
    CodeBehind="datavalues.aspx.cs" Inherits="Microsoft.Cui.SampleWebsite.DesignGuide.QuickGuides.MedicationsList.Status.DataValues" %>

<asp:Content ID="PageContent" ContentPlaceHolderID="QIGsPageContent" runat="server">
    <div id="page">
        <div id="guidance">
                        
			<div class="midline">
				<div class="illustration">
					<img src="../images/medv167.png" alt="A column heading (Status) with a single cell below containing &#39;Started&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-167</p>
					<p>
                    Assign a status of &#39;Started&#39; to medications 
                    that have an administration event recorded 
                    and have further scheduled administrations
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
            
			<div class="midline">
				<div class="illustration">
					<img src="../images/medv168.png" alt="A column heading (Status) with a single cell below containing &#39;Not Started&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-168</p>
					<p>
                    Assign a status of &#39;Not Started&#39; 
                    to medications that have administration 
                    scheduled and a start date in the future
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
			
			<div class="midline">
				<div class="illustration">
					<img src="../images/medv170.png" alt="A column heading (Status) with a single cell below containing &#39;Completed&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-170</p>
					<p>
                    Assign a status of &#39;Completed&#39; 
                    to medications that have administration 
                    events recorded according to their schedule 
                    (within tolerances) and have an end date in 
                    the past
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
			
			<div class="line">
				<div class="illustration">
					<img src="../images/medv171.png" alt="A column heading (Status) with a single cell below containing &#39;Discontinued&#39;" />
				</div>
				<div class="guidetext">
					<p class="number">MEDv-171</p>
					<p>
                    Assign a status of &#39;Discontinued&#39; to medications 
                    that were stopped on a date that preceded one or more of 
                    the scheduled administrations
                    </p>
					<p class="recommended">Recommended</p>
				</div>
			</div>
			

        </div>
    </div>
</asp:Content>
