//-----------------------------------------------------------------------
// <copyright file="MedicationGridTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010.
// All rights reserved.
//
// CERTAIN PARTS OF THIS WORK CONTAIN SOFTWARE CODE THAT IS LICENSED 
// FOR USE UNDER THE MICROSOFT PUBLIC LICENSE. DISTRIBUTION, IN SOURCE CODE 
// OR OBJECT CODE FORM, OF THOSE PARTS MUST COMPLY WITH THE TERMS OF THE 
// PUBLIC LICENSE. SEE http://www.microsoft.com/opensource/licenses.mspx 
// FOR DETAILS.  
// IF YOU BRING A PATENT CLAIM AGAINST ANY CONTRIBUTOR OVER PATENTS THAT 
// YOU CLAIM ARE INFRINGED BY THE PUBLIC LICENSE SOFTWARE, YOUR PATENT 
// LICENSE FROM SUCH CONTRIBUTOR TO THE SOFTWARE ENDS AUTOMATICALLY.
//
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
// </copyright>
// <date>03-Jan-2007</date>
// <summary>
// This is a test class for NhsCui.Toolkit.Web.MedicationGrid and is intended to contain all NhsCui.Toolkit.Web.MedicationGrid Unit Tests
// </summary>
//-----------------------------------------------------------------------
namespace NhsCui.Toolkit.Web.Test
{
    #region Using
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Text;
    using System.Collections.Generic;
    using NhsCui.Toolkit.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.ComponentModel;
    using System.Threading;
    using System.Globalization;
    using System.Diagnostics.CodeAnalysis;
    #endregion

    /// <summary>
    ///This is a test class for NhsCui.Toolkit.Web.MedicationGrid and is intended
    ///to contain all NhsCui.Toolkit.Web.MedicationGrid Unit Tests
    ///</summary>
    [TestClass()]
    public class MedicationGridTest : IDisposable
    {
        /// <summary>
        /// Test Context
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Reset event to indicate that the propertychanged event was raised
        /// </summary>
        private ManualResetEvent propertyChangedThreadEvent = new ManualResetEvent(false);

        /// <summary>
        /// Reset event to indicate that an expected event was fired
        /// </summary>
        private ManualResetEvent eventRaisedEvent = new ManualResetEvent(false);

        /// <summary>
        /// Name of event that was raised
        /// </summary>
        private string raisedEventName = string.Empty;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return this.testContextInstance;
            }

            set
            {
                this.testContextInstance = value;
            }
        }  

        /// <summary>
        ///A test for MedicationGrid ()
        ///</summary>
        [TestMethod()]
        public void ConstructorTest()
        {
            MedicationGrid target = new MedicationGrid();
            Assert.AreEqual(target.Height, Unit.Pixel(200));
        }

        /// <summary>
        ///A test for DrugDetailsColumnHeaderText
        ///</summary>
        [TestMethod()]
        public void DrugDetailsColumnHeaderTextTest()
        {
            MedicationGrid target = new MedicationGrid();
            string val = "Test Text"; 
            target.DrugDetailsColumnHeaderText = val;
            Assert.AreEqual(val, target.DrugDetailsColumnHeaderText, "NhsCui.Toolkit.Web.MedicationGrid.DrugDetailsColumnHeaderText was not set correctly.");            
        }

        /// <summary>
        ///A test for DrugDetailsColumnWidth
        ///</summary>
        [TestMethod()]
        public void DrugDetailsColumnWidthTest()
        {
            MedicationGrid target = new MedicationGrid();
            Unit val = new Unit(100); 
            target.DrugDetailsColumnWidth = val;
            Assert.AreEqual(val, target.DrugDetailsColumnWidth, "NhsCui.Toolkit.Web.MedicationGrid.DrugDetailsColumnWidth was not set correctly.");            
        }

        /// <summary>
        ///A test for FindMedication (string)
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        public void FindMedicationTest()
        {
            MedicationGrid target = new MedicationGrid();
            NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_MedicationGridAccessor accessor = new NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_MedicationGridAccessor(target);            
            Medication expected = new Medication();
            string id = Guid.NewGuid().ToString();
            expected.MedicationID = id;
            target.Items.Add(expected);

            Medication actual;
            actual = accessor.FindMedication(id);
            Assert.AreEqual(expected, actual, "NhsCui.Toolkit.Web.MedicationGrid.FindMedication did not return the expected value.");            
        }

        /// <summary>
        ///A test for GetSelectedItems ()
        ///</summary>
        [TestMethod()]
        public void GetSelectedItemsTest()
        {
            MedicationGrid target = new MedicationGrid();
            Medication[] items = new Medication[3];
            items[0] = new Medication();
            items[0].IsSelected = true;
            items[1] = new Medication();
            items[1].IsSelected = false;
            items[2] = new Medication();
            items[2].IsSelected = true;

            target.Items.Add(items[0]);
            target.Items.Add(items[1]);
            target.Items.Add(items[2]);            
            
            Medication[] actual;

            actual = target.GetSelectedItems();
            Assert.AreEqual(items[0], actual[0], "NhsCui.Toolkit.Web.MedicationGrid.GetSelectedItems did not return the expected value.");
            Assert.AreEqual(items[2], actual[1], "NhsCui.Toolkit.Web.MedicationGrid.GetSelectedItems did not return the expected value.");                        
        }

        /// <summary>
        ///A test for GetSelectedItem ()
        ///</summary>
        [TestMethod()]
        public void GetSelectedItemTest()
        {
            MedicationGrid target = new MedicationGrid();

            target.Items.Add(new Medication());
            target.Items.Add(new Medication());
            target.Items.Add(new Medication());

            Medication expected = target.Items[2];
            expected.IsSelected = true;
                   
            Medication actual;
            
            actual = target.GetSelectedItem();

            Assert.AreEqual(expected, actual, "NhsCui.Toolkit.Web.MedicationGrid.GetSelectedItem did not return the expected value.");            
        }

        /// <summary>
        ///A test for ID
        ///</summary>
        [TestMethod()]
        [SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
        public void IDTest()
        {
            MedicationGrid target = new MedicationGrid();
            string val = "uniqueID"; 
            target.ID = val;
            Assert.AreEqual(val, target.ID, "NhsCui.Toolkit.Web.MedicationGrid.ID was not set correctly.");            
        }

        /// <summary>
        ///A test for Items
        ///</summary>
        [TestMethod()]
        public void ItemsTest()
        {
            MedicationGrid target = new MedicationGrid();

            int itemCount = 3;

            Medication[] items = new Medication[itemCount];

            for (int itemIndex = 0; itemIndex < itemCount; itemIndex++)
            {                
                items[itemIndex] = new Medication();
                items[itemIndex].IsSelected = true;
                target.Items.Add(items[itemIndex]);
                Assert.AreEqual(items[itemIndex], target.Items[itemIndex], "NhsCui.Toolkit.Web.MedicationGrid.Items was not set correctly.");            
            }                                     
        }

        /// <summary>
        ///A test for OnClick (MedicationEventArgs)
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        public void OnClickTest()
        {
            MedicationGrid target = new MedicationGrid();
            target.Click += new EventHandler<MedicationEventArgs>(this.Target_Click);
            NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_MedicationGridAccessor accessor = new NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_MedicationGridAccessor(target);
            MedicationEventArgs e = null; 
            this.eventRaisedEvent.Reset();
            accessor.OnClick(e);
            this.WaitForEvent("OnClick");
            target.Click -= new EventHandler<MedicationEventArgs>(this.Target_Click);
        }

        /// <summary>
        ///A test for OnDoubleClick (MedicationEventArgs)
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        public void OnDoubleClickTest()
        {
            MedicationGrid target = new MedicationGrid();
            target.DoubleClick += new EventHandler<MedicationEventArgs>(this.Target_DoubleClick);
            NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_MedicationGridAccessor accessor = new NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_MedicationGridAccessor(target);
            MedicationEventArgs e = null;
            this.eventRaisedEvent.Reset();
            accessor.OnDoubleClick(e);
            this.WaitForEvent("OnDoubleClick");
            target.DoubleClick -= new EventHandler<MedicationEventArgs>(this.Target_DoubleClick);
        }

        /// <summary>
        ///A test for ReasonColumnHeaderText
        ///</summary>
        [TestMethod()]
        public void ReasonColumnHeaderTextTest()
        {
            MedicationGrid target = new MedicationGrid();
            string val = "Reason Column Header"; 
            target.ReasonColumnHeaderText = val;
            Assert.AreEqual(val, target.ReasonColumnHeaderText, "NhsCui.Toolkit.Web.MedicationGrid.ReasonColumnHeaderText was not set correctly.");            
        }

        /// <summary>
        ///A test for ReasonColumnWidth
        ///</summary>
        [TestMethod()]
        public void ReasonColumnWidthTest()
        {
            MedicationGrid target = new MedicationGrid();
            Unit val = new Unit(100); 
            target.ReasonColumnWidth = val;
            Assert.AreEqual(val, target.ReasonColumnWidth, "NhsCui.Toolkit.Web.MedicationGrid.ReasonColumnWidth was not set correctly.");            
        }

        /// <summary>
        ///A test for Sort (bool)
        ///</summary>
        [TestMethod()]
        public void SortTestDescending()
        {
            MedicationGrid target = new MedicationGrid();
            Medication item1 = new Medication();
            item1.StartDate = new DateTime(2006, 1, 1);     // 1 Jan 2006
            item1.Reason = "ABCDEFB";
            item1.Route = "Route";
            item1.Status = MedicationStatus.Active;
            item1.StatusDate = new DateTime(2007, 4, 6);     // 6 April 2007            
            target.Items.Add(item1);
            
            Medication item2 = new Medication();
            item2.StartDate = new DateTime(2006, 4, 1);     // 1 April 2006
            item2.Reason = "123 678";
            item2.Route = "RouteB";
            item2.Status = MedicationStatus.Suspended;
            item2.StatusDate = new DateTime(2006, 5, 2);     // 2 May 2006
            target.Items.Add(item2);

            Medication item3 = new Medication();
            item3.StartDate = new DateTime(2001, 8, 10);     // 10 August 2001
            item3.Reason = "ABCDEFA";
            item3.Route = "RouteC";
            item3.Status = MedicationStatus.Active;
            item3.StatusDate = new DateTime(2007, 4, 4);     // 4 April 2007
            target.Items.Add(item3);

            Medication item4 = new Medication();
            item4.StartDate = new DateTime(2001, 8, 11);     // 11 August 2001
            item4.Reason = "";
            item4.Route = "RouteA";
            item4.Status = MedicationStatus.Suspended;
            item4.StatusDate = new DateTime(2002, 12, 12);     // 12 December 2002
            target.Items.Add(item4);
            
            bool ascending = false; 

            // Default - Sort by start date
            target.Sort(ascending);
            Assert.AreEqual<Medication>(target.Items[0], item2);
            Assert.AreEqual<Medication>(target.Items[1], item1);
            Assert.AreEqual<Medication>(target.Items[2], item4);
            Assert.AreEqual<Medication>(target.Items[3], item3);
        }

        /// <summary>
        ///A test for Sort (bool)
        ///</summary>
        [TestMethod()]
        public void SortTestAscending()
        {
            MedicationGrid target = new MedicationGrid();
            Medication item1 = new Medication();
            item1.StartDate = new DateTime(2006, 1, 1);     // 1 Jan 2006
            item1.Reason = "ABCDEFB";
            item1.Route = "Route";
            item1.Status = MedicationStatus.Active;
            item1.StatusDate = new DateTime(2007, 4, 6);     // 6 April 2007            
            target.Items.Add(item1);

            Medication item2 = new Medication();
            item2.StartDate = new DateTime(2006, 4, 1);     // 1 April 2006
            item2.Reason = "123 678";
            item2.Route = "RouteB";
            item2.Status = MedicationStatus.Suspended;
            item2.StatusDate = new DateTime(2006, 5, 2);     // 2 May 2006
            target.Items.Add(item2);

            Medication item3 = new Medication();
            item3.StartDate = new DateTime(2001, 8, 10);     // 10 August 2001
            item3.Reason = "ABCDEFA";
            item3.Route = "RouteC";
            item3.Status = MedicationStatus.Active;
            item3.StatusDate = new DateTime(2007, 4, 4);     // 4 April 2007
            target.Items.Add(item3);

            Medication item4 = new Medication();
            item4.StartDate = new DateTime(2001, 8, 11);     // 11 August 2001
            item4.Reason = "";
            item4.Route = "RouteA";
            item4.Status = MedicationStatus.Suspended;
            item4.StatusDate = new DateTime(2002, 12, 12);     // 12 December 2002
            target.Items.Add(item4);

            bool ascending = true; 

            // Default - Sort by start date
            target.Sort(ascending);
            Assert.AreEqual<Medication>(target.Items[0], item3);
            Assert.AreEqual<Medication>(target.Items[1], item4);
            Assert.AreEqual<Medication>(target.Items[2], item1);
            Assert.AreEqual<Medication>(target.Items[3], item2);                             
        }

        /// <summary>
        ///A test for Sort (IComparer&lt;Medication&gt;)
        ///</summary>
        [TestMethod()]
        public void SortTestByComparer()
        {
            MedicationGrid target = new MedicationGrid();
            Medication item1 = new Medication();
            item1.StartDate = new DateTime(2006, 1, 1);     // 1 Jan 2006
            item1.Reason = "ABCDEFB";
            item1.Route = "Route";
            item1.Status = MedicationStatus.Suspended;
            item1.StatusDate = new DateTime(2007, 4, 6);     // 6 April 2007            
            target.Items.Add(item1);

            Medication item2 = new Medication();
            item2.StartDate = new DateTime(2006, 4, 1);     // 1 April 2006
            item2.Reason = "123 678";
            item2.Route = "RouteB";
            item2.Status = MedicationStatus.Suspended;
            item2.StatusDate = new DateTime(2006, 5, 2);     // 2 May 2006
            target.Items.Add(item2);

            Medication item3 = new Medication();
            item3.StartDate = new DateTime(2001, 8, 10);     // 10 August 2001
            item3.Reason = "ABCDEFA";
            item3.Route = "Route";
            item3.Status = MedicationStatus.Active;
            item3.StatusDate = new DateTime(2007, 4, 4);     // 4 April 2007
            target.Items.Add(item3);

            Medication item4 = new Medication();
            item4.StartDate = new DateTime(2001, 8, 11);     // 11 August 2001
            item4.Reason = "";
            item4.Route = "RouteA";
            item4.Status = MedicationStatus.Suspended;
            item4.StatusDate = new DateTime(2002, 12, 12);     // 12 December 2002
            target.Items.Add(item4);

            System.Collections.Generic.IComparer<NhsCui.Toolkit.Web.Medication> medicationSorter = new SortByRouteThenStatus(); 
            target.Sort(medicationSorter);

            Assert.AreEqual<Medication>(target.Items[0], item3);
            Assert.AreEqual<Medication>(target.Items[1], item1);
            Assert.AreEqual<Medication>(target.Items[2], item4);
            Assert.AreEqual<Medication>(target.Items[3], item2);
        }

        /// <summary>
        ///A test for Sort (MedicationGridColumn, bool)
        ///</summary>
        [TestMethod()]
        public void SortTestByDrugDetailsColumn()
        {
            MedicationGrid target = new MedicationGrid();
            Medication item1 = new Medication();
            item1.StartDate = new DateTime(2006, 1, 1);     // 1 Jan 2006
            item1.Reason = "ABCDEFB";
            item1.Route = "Route";
            item1.Status = MedicationStatus.Suspended;
            item1.StatusDate = new DateTime(2007, 4, 6);     // 6 April 2007            
            item1.MedicationNames.Add(new MedicationName("ABC"));
            target.Items.Add(item1);

            Medication item2 = new Medication();
            item2.StartDate = new DateTime(2006, 4, 1);     // 1 April 2006
            item2.Reason = "123 678";
            item2.Route = "RouteB";
            item2.Status = MedicationStatus.Suspended;
            item2.MedicationNames.Add(new MedicationName("ABE"));
            item2.StatusDate = new DateTime(2006, 5, 2);     // 2 May 2006
            target.Items.Add(item2);

            Medication item3 = new Medication();
            item3.StartDate = new DateTime(2001, 8, 10);     // 10 August 2001
            item3.Reason = "ABCDEFA";
            item3.Route = "Route";
            item3.Status = MedicationStatus.Active;
            item3.MedicationNames.Add(new MedicationName("ABA"));
            item3.StatusDate = new DateTime(2007, 4, 4);     // 4 April 2007
            target.Items.Add(item3);

            Medication item4 = new Medication();
            item4.StartDate = new DateTime(2001, 8, 11);     // 11 August 2001
            item4.Reason = "";
            item4.Route = "RouteA";
            item4.Status = MedicationStatus.Suspended;
            item4.MedicationNames.Add(new MedicationName("ABD"));
            item4.StatusDate = new DateTime(2002, 12, 12);     // 12 December 2002
            target.Items.Add(item4);

            target.Sort(MedicationGridColumn.DrugDetails, true);

            Assert.AreEqual<Medication>(target.Items[0], item3);
            Assert.AreEqual<Medication>(target.Items[1], item1);
            Assert.AreEqual<Medication>(target.Items[2], item4);
            Assert.AreEqual<Medication>(target.Items[3], item2);
        }

        /// <summary>
        ///A test for Sort (MedicationGridColumn, bool)
        ///</summary>
        [TestMethod()]
        public void SortTestByReason()
        {
            MedicationGrid target = new MedicationGrid();
            Medication item1 = new Medication();
            item1.StartDate = new DateTime(2006, 1, 1);     // 1 Jan 2006
            item1.Reason = "ABCDEFB";
            item1.Route = "Route";
            item1.Status = MedicationStatus.Suspended;
            item1.StatusDate = new DateTime(2007, 4, 6);     // 6 April 2007            
            item1.MedicationNames.Add(new MedicationName("ABC"));
            target.Items.Add(item1);

            Medication item2 = new Medication();
            item2.StartDate = new DateTime(2006, 4, 1);     // 1 April 2006
            item2.Reason = "123 678";
            item2.Route = "RouteB";
            item2.Status = MedicationStatus.Suspended;
            item2.MedicationNames.Add(new MedicationName("ABE"));
            item2.StatusDate = new DateTime(2006, 5, 2);     // 2 May 2006
            target.Items.Add(item2);

            Medication item3 = new Medication();
            item3.StartDate = new DateTime(2001, 8, 10);     // 10 August 2001
            item3.Reason = "ABCDEFA";
            item3.Route = "Route";
            item3.Status = MedicationStatus.Active;
            item3.MedicationNames.Add(new MedicationName("ABA"));
            item3.StatusDate = new DateTime(2007, 4, 4);     // 4 April 2007
            target.Items.Add(item3);

            Medication item4 = new Medication();
            item4.StartDate = new DateTime(2001, 8, 11);     // 11 August 2001
            item4.Reason = "";
            item4.Route = "RouteA";
            item4.Status = MedicationStatus.Suspended;
            item4.MedicationNames.Add(new MedicationName("ABD"));
            item4.StatusDate = new DateTime(2002, 12, 12);     // 12 December 2002
            target.Items.Add(item4);

            target.Sort(MedicationGridColumn.Reason, true);

            Assert.AreEqual<Medication>(target.Items[0], item4);
            Assert.AreEqual<Medication>(target.Items[1], item2);
            Assert.AreEqual<Medication>(target.Items[2], item3);
            Assert.AreEqual<Medication>(target.Items[3], item1);
        }

        /// <summary>
        ///A test for Sort (MedicationGridColumn, bool)
        ///</summary>
        [TestMethod()]
        public void SortTestByStartDate()
        {
            MedicationGrid target = new MedicationGrid();
            Medication item1 = new Medication();
            item1.StartDate = new DateTime(2006, 1, 1);     // 1 Jan 2006
            item1.Reason = "ABCDEFB";
            item1.Route = "Route";
            item1.Status = MedicationStatus.Suspended;
            item1.StatusDate = new DateTime(2007, 4, 6);     // 6 April 2007            
            item1.MedicationNames.Add(new MedicationName("ABC"));
            target.Items.Add(item1);

            Medication item2 = new Medication();
            item2.StartDate = new DateTime(2006, 4, 1);     // 1 April 2006
            item2.Reason = "123 678";
            item2.Route = "RouteB";
            item2.Status = MedicationStatus.Suspended;
            item2.MedicationNames.Add(new MedicationName("ABE"));
            item2.StatusDate = new DateTime(2006, 5, 2);     // 2 May 2006
            target.Items.Add(item2);

            Medication item3 = new Medication();
            item3.StartDate = new DateTime(2001, 8, 10);     // 10 August 2001
            item3.Reason = "ABCDEFA";
            item3.Route = "Route";
            item3.Status = MedicationStatus.Active;
            item3.MedicationNames.Add(new MedicationName("ABA"));
            item3.StatusDate = new DateTime(2007, 4, 4);     // 4 April 2007
            target.Items.Add(item3);

            Medication item4 = new Medication();
            item4.StartDate = new DateTime(2001, 8, 11);     // 11 August 2001
            item4.Reason = "";
            item4.Route = "RouteA";
            item4.Status = MedicationStatus.Suspended;
            item4.MedicationNames.Add(new MedicationName("ABD"));
            item4.StatusDate = new DateTime(2002, 12, 12);     // 12 December 2002
            target.Items.Add(item4);

            target.Sort(MedicationGridColumn.StartDate, true);

            Assert.AreEqual<Medication>(target.Items[0], item3);
            Assert.AreEqual<Medication>(target.Items[1], item4);
            Assert.AreEqual<Medication>(target.Items[2], item1);
            Assert.AreEqual<Medication>(target.Items[3], item2);
        }

        /// <summary>
        ///A test for Sort (MedicationGridColumn, bool)
        ///</summary>
        [TestMethod()]
        public void SortTestByStatus()
        {
            MedicationGrid target = new MedicationGrid();
            Medication item1 = new Medication();
            item1.StartDate = new DateTime(2006, 1, 1);     // 1 Jan 2006
            item1.Reason = "ABCDEFB";
            item1.Route = "Route";
            item1.Status = MedicationStatus.Suspended;
            item1.StatusDate = new DateTime(2007, 4, 6);     // 6 April 2007            
            item1.MedicationNames.Add(new MedicationName("ABC"));
            target.Items.Add(item1);

            Medication item2 = new Medication();
            item2.StartDate = new DateTime(2006, 4, 1);     // 1 April 2006
            item2.Reason = "123 678";
            item2.Route = "RouteB";
            item2.Status = MedicationStatus.Suspended;
            item2.MedicationNames.Add(new MedicationName("ABE"));
            item2.StatusDate = new DateTime(2006, 5, 2);     // 2 May 2006
            target.Items.Add(item2);

            Medication item3 = new Medication();
            item3.StartDate = new DateTime(2001, 8, 10);     // 10 August 2001
            item3.Reason = "ABCDEFA";
            item3.Route = "Route";
            item3.Status = MedicationStatus.Active;
            item3.MedicationNames.Add(new MedicationName("ABA"));
            item3.StatusDate = new DateTime(2007, 4, 4);     // 4 April 2007
            target.Items.Add(item3);

            Medication item4 = new Medication();
            item4.StartDate = new DateTime(2001, 8, 11);     // 11 August 2001
            item4.Reason = "";
            item4.Route = "RouteA";
            item4.Status = MedicationStatus.Suspended;
            item4.MedicationNames.Add(new MedicationName("ABD"));
            item4.StatusDate = new DateTime(2002, 12, 12);     // 12 December 2002
            target.Items.Add(item4);

            target.Sort(MedicationGridColumn.Status, true);

            Assert.AreEqual<Medication>(target.Items[0], item3);
            Assert.AreEqual<Medication>(target.Items[1], item4);
            Assert.AreEqual<Medication>(target.Items[2], item1);
            Assert.AreEqual<Medication>(target.Items[3], item2);
        }

        /// <summary>
        ///A test for StartDateColumnHeaderText
        ///</summary>
        [TestMethod()]
        public void StartDateColumnHeaderTextTest()
        {
            MedicationGrid target = new MedicationGrid();
            string val = "Start Date Column Header Text Test";
            target.StartDateColumnHeaderText = val;
            Assert.AreEqual(val, target.StartDateColumnHeaderText, "NhsCui.Toolkit.Web.MedicationGrid.StartDateColumnHeaderText was not set correctly.");
        }

        /// <summary>
        ///A test for StartDateColumnWidth
        ///</summary>
        [TestMethod()]
        public void StartDateColumnWidthTest()
        {
            MedicationGrid target = new MedicationGrid();
            Unit val = new Unit(100); 
            target.StartDateColumnWidth = val;
            Assert.AreEqual(val, target.StartDateColumnWidth, "NhsCui.Toolkit.Web.MedicationGrid.StartDateColumnWidth was not set correctly.");            
        }

        /// <summary>
        ///A test for StatusColumnHeaderText
        ///</summary>
        [TestMethod()]
        public void StatusColumnHeaderTextTest()
        {
            MedicationGrid target = new MedicationGrid();
            string val = "Status Column Header Text";
            target.StatusColumnHeaderText = val;
            Assert.AreEqual(val, target.StatusColumnHeaderText, "NhsCui.Toolkit.Web.MedicationGrid.StatusColumnHeaderText was not set correctly.");            
        }

        /// <summary>
        ///A test for StatusColumnWidth
        ///</summary>
        [TestMethod()]
        public void StatusColumnWidthTest()
        {
            MedicationGrid target = new MedicationGrid();
            Unit val = new Unit(100); 
            target.StatusColumnWidth = val;
            Assert.AreEqual(val, target.StatusColumnWidth, "NhsCui.Toolkit.Web.MedicationGrid.StatusColumnWidth was not set correctly.");            
        }

        /// <summary>
        /// A test for ApplyShowRules - not tested directly, but rather through the properties.
        /// If SimpleMode is set to true, and any Show property is to true, then SimpleMode should be set to false
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        public void ApplyShowRulesTestShowOverride()
        {
            MedicationGrid target = new MedicationGrid();

            // Clear all properties
            target.SimpleMode = false;
            target.ShowDosageDetails = false;
            target.ShowGraphics = false;
            target.ShowReason = false;
            target.ShowStatus = false;
            target.ShowStatusDate = false;

            target.SimpleMode = true;
            target.ShowDosageDetails = true;
            Assert.IsFalse(target.SimpleMode);

            target.SimpleMode = true;
            target.ShowGraphics = true;
            Assert.IsFalse(target.SimpleMode);

            target.SimpleMode = true;
            target.ShowReason = true;
            Assert.IsFalse(target.SimpleMode);

            target.SimpleMode = true;
            target.ShowStatus = true;
            Assert.IsFalse(target.SimpleMode);

            target.SimpleMode = true;
            target.ShowStatusDate = true;
            Assert.IsFalse(target.SimpleMode);
        }

        /// <summary>
        /// A test for SimpeMode disabling
        /// If SimpleMode is set to true, all properties should be set to false
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        public void ApplyShowRulesTestSimpleModeOverride()
        {
            MedicationGrid target = new MedicationGrid();

            // Clear all properties
            target.SimpleMode = false;
            target.ShowDosageDetails = true;
            target.ShowGraphics = true;
            target.ShowReason = true;
            target.ShowStatus = true;
            target.ShowStatusDate = true;

            target.SimpleMode = true;
            Assert.IsFalse(target.ShowDosageDetails);
            Assert.IsFalse(target.ShowGraphics);
            Assert.IsFalse(target.ShowReason);
            Assert.IsFalse(target.ShowDosageDetails);
            Assert.IsFalse(target.ShowStatus);
            Assert.IsFalse(target.ShowStatusDate);
        }

        /// <summary>
        ///A test for OnClientClick
        ///</summary>
        [TestMethod()]
        public void OnClientClickTest()
        {
            MedicationGrid target = new MedicationGrid();
            string val = "clickHandler";
            target.OnClientClick = val;
            Assert.AreEqual(val, target.OnClientClick, "NhsCui.Toolkit.Web.MedicationGrid.OnClientClick was not set correctly.");
        }

        /// <summary>
        ///A test for OnClientDoubleClick
        ///</summary>
        [TestMethod()]
        public void OnClientDoubleClickTest()
        {
            MedicationGrid target = new MedicationGrid();
            string val = "doubleClickHander";
            target.OnClientDoubleClick = val;
            Assert.AreEqual(val, target.OnClientDoubleClick, "NhsCui.Toolkit.Web.MedicationGrid.OnClientDoubleClick was not set correctly.");
        }

        /// <summary>
        ///A test for OnClientRightClick
        ///</summary>
        [TestMethod()]
        public void OnClientRightClickTest()
        {
            MedicationGrid target = new MedicationGrid();
            string val = "onClientRightClick";
            target.OnClientRightClick = val;
            Assert.AreEqual(val, target.OnClientRightClick, "NhsCui.Toolkit.Web.MedicationGrid.OnClientRightClick was not set correctly.");
        }

        /// <summary>
        ///A test for ShowDosageDetails
        ///</summary>
        [TestMethod()]
        public void ShowDosageDetailsTest()
        {
            MedicationGrid target = new MedicationGrid();
            bool val = true;
            target.ShowDosageDetails = val;
            Assert.AreEqual(val, target.ShowDosageDetails, "NhsCui.Toolkit.Web.MedicationGrid.ShowDosageDetails was not set correctly.");
        }

        /// <summary>
        ///A test for ShowGraphics
        ///</summary>
        [TestMethod()]
        public void ShowGraphicsTest()
        {
            MedicationGrid target = new MedicationGrid();
            bool val = true;
            target.ShowGraphics = val;
            Assert.AreEqual(val, target.ShowGraphics, "NhsCui.Toolkit.Web.MedicationGrid.ShowGraphics was not set correctly.");
        }

        /// <summary>
        ///A test for ShowReason
        ///</summary>
        [TestMethod()]
        public void ShowReasonTest()
        {
            MedicationGrid target = new MedicationGrid();
            bool val = true;
            target.ShowReason = val;
            Assert.AreEqual(val, target.ShowReason, "NhsCui.Toolkit.Web.MedicationGrid.ShowReason was not set correctly.");
        }

        /// <summary>
        ///A test for ShowStatusDate
        ///</summary>
        [TestMethod()]
        public void ShowStatusDateTest()
        {
            MedicationGrid target = new MedicationGrid();
            bool val = true;
            target.ShowStatusDate = val;
            Assert.AreEqual(val, target.ShowStatusDate, "NhsCui.Toolkit.Web.MedicationGrid.ShowStatusDate was not set correctly.");
        }

        /// <summary>
        ///A test for ShowStatus
        ///</summary>
        [TestMethod()]
        public void ShowStatusTest()
        {
            MedicationGrid target = new MedicationGrid();
            bool val = true;
            target.ShowStatus = val;
            Assert.AreEqual(val, target.ShowStatus, "NhsCui.Toolkit.Web.MedicationGrid.ShowStatus was not set correctly.");
        }

        /// <summary>
        ///A test for SimpleMode
        ///</summary>
        [TestMethod()]
        public void SimpleModeTest()
        {
            MedicationGrid target = new MedicationGrid();
            bool val = true;
            target.SimpleMode = val;
            Assert.AreEqual(val, target.SimpleMode, "NhsCui.Toolkit.Web.MedicationGrid.SimpleMode was not set correctly.");
        }

        #region IDisposable Members
        /// <summary>
        /// Clean up Property Changed Event
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">Dispose</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.eventRaisedEvent != null)
                {
                    this.eventRaisedEvent.Close();
                }

                if (this.propertyChangedThreadEvent != null)
                {
                    this.propertyChangedThreadEvent.Close();
                }
            }
        }
        #endregion

        /// <summary>
        /// Wait for a property changed event to be raised, and ensure that the correct property event was raised
        /// </summary>
        /// <param name="eventName">Event Name</param>
        private void WaitForEvent(string eventName)
        {
            if (!this.eventRaisedEvent.WaitOne(500, false))
            {
                Assert.Fail(string.Format(CultureInfo.CurrentUICulture, "Event {0} was not raised ", eventName));
            }
        }

        /// <summary>
        /// Generic EventHandler 
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void Target_Click(object sender, MedicationEventArgs e)
        {
            this.eventRaisedEvent.Set();
        }

        /// <summary>
        /// Doulbe Click Test
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void Target_DoubleClick(object sender, MedicationEventArgs e)
        {
            this.eventRaisedEvent.Set();
        }

        /// <summary>
        /// Custom sorting - SortByRoute then Status Date
        /// </summary>
        private class SortByRouteThenStatus : IComparer<NhsCui.Toolkit.Web.Medication>
        {
            #region IComparer<Medication> Members

            /// <summary>
            /// Compare Route and Status of two medications
            /// </summary>
            /// <param name="x">First Medication</param>
            /// <param name="y">Second Medication</param>
            /// <returns>-1 if based on Route and Status x should be before y. 0 if Route and Status are equal. +1 if x should be after y based on Route and Status</returns>            
            [SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.ArgumentNullException.#ctor(System.String,System.String)", Justification = "Unit Test")]
            public int Compare(Medication x, Medication y)
            {
                if (x == null)
                {
                    throw new ArgumentNullException("x", "Argument cannot be null");
                }

                if (y == null)
                {
                    throw new ArgumentNullException("y", "Argument cannot be null");
                }

                int result = x.Route.CompareTo(y.Route);
                if (result == 0)
                {
                    return x.Status.CompareTo(y.Status);
                }
                else
                {
                    return result;
                }
            }
            #endregion
        }
    }
}
