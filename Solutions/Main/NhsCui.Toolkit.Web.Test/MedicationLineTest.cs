//------------------------------------------------------------------------------
// <copyright file="MedicationLineTest.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>16-Feb-2007</date>
// <summary>Unit tests for MedicationLine control</summary>
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
    using System.Web.UI.HtmlControls;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Globalization;
    #endregion

    /// <summary>
    ///This is a test class for NhsCui.Toolkit.Web.MedicationLine and is intended
    ///to contain all NhsCui.Toolkit.Web.MedicationLine Unit Tests
    ///</summary>
    [TestClass()]
    public class MedicationLineTest : IDisposable
    {
        /// <summary>
        /// Test Context Instance
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
        /// A test for ApplyShowRules - not tested directly, but rather through the properties.
        /// If SimpleMode is set to true, and any Show property is to true, then SimpleMode should be set to false
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        public void ApplyShowRulesTestShowOverride()
        {
            MedicationLine target = new MedicationLine();

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
            MedicationLine target = new MedicationLine();

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
        ///A test for CriticalAlertGraphic
        ///</summary>
        [TestMethod()]
        public void CriticalAlertGraphicTest()
        {
            MedicationLine target = new MedicationLine();
            string val = "~/images/icons/image.png";
            target.CriticalAlertGraphic = val;
            Assert.AreEqual(val, target.CriticalAlertGraphic, "NhsCui.Toolkit.Web.MedicationLine.CriticalAlertGraphic was not set correctly.");
        }

        /// <summary>
        ///A test for DosageText
        ///</summary>
        [TestMethod()]
        public void DosageTextTest()
        {
            MedicationLine target = new MedicationLine();
            string val = "Valid DosageText";
            target.DosageText = val;
            Assert.AreEqual(val, target.DosageText, "NhsCui.Toolkit.Web.MedicationLine.DosageText was not set correctly.");
        }

        /// <summary>
        ///A test for DosageText
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DosageTextTestException()
        {
            MedicationLine target = new MedicationLine();
            string val = "In molestie felis. Donec suscipit. Praesent ut nulla ut lectus facilisis adipiscing. Ut ac ipsum a erat tincidunt accumsan. Nam sed neque sit amet velit porta eleifend. Integer quis magna ut mauris rutrum dignissim. Nunc egestas erat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean facilisis est ut turpis. Ut blandit cursus felis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos hymenaeos. Nunc nisi augue, lacinia vitae, blandit nec, venenatis id, tellus. Nam fringilla, enim vitae scelerisque tristique, ipsum neque feugiat dui, vitae auctor enim diam non est. Fusce malesuada, nisl et varius accumsan, lorem ligula luctus est, non nonummy sem magna molestie sapien. ";
            target.DosageText = val;
            Assert.AreEqual(val, target.DosageText, "NhsCui.Toolkit.Web.MedicationLine.DosageText was not set correctly.");
        }

        /// <summary>
        ///A test for Dose
        ///</summary>
        [TestMethod()]
        public void DoseTest()
        {
            MedicationLine target = new MedicationLine();
            string val = "dose"; 
            target.Dose = val;
            Assert.AreEqual(val, target.Dose, "NhsCui.Toolkit.Web.MedicationLine.Dose was not set correctly.");
        }

        /// <summary>
        ///A test for Dose - exceeding the max length
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DoseTestException()
        {
            MedicationLine target = new MedicationLine();
            string val = "In molestie felis. Donec suscipit. Praesent ut nulla ut lectus facilisis adipiscing. Ut ac ipsum a erat tincidunt accumsan. Nam sed neque sit amet velit porta eleifend. Integer quis magna ut mauris rutrum dignissim. Nunc egestas erat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean facilisis est ut turpis. Ut blandit cursus felis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos hymenaeos. Nunc nisi augue, lacinia vitae, blandit nec, venenatis id, tellus. Nam fringilla, enim vitae scelerisque tristique, ipsum neque feugiat dui, vitae auctor enim diam non est. Fusce malesuada, nisl et varius accumsan, lorem ligula luctus est, non nonummy sem magna molestie sapien. ";
            target.Dose = val;
        }

        /// <summary>
        ///A test for Form
        ///</summary>
        [TestMethod()]
        public void FormTest()
        {
            MedicationLine target = new MedicationLine();
            string val = "FormTest";
            target.Form = val;
        }

        /// <summary>
        ///A test for Form
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FormTestException()
        {
            MedicationLine target = new MedicationLine();
            string val = "In molestie felis. Donec suscipit. Praesent ut nulla ut lectus facilisis adipiscing. Ut ac ipsum a erat tincidunt accumsan. Nam sed neque sit amet velit porta eleifend. Integer quis magna ut mauris rutrum dignissim. Nunc egestas erat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean facilisis est ut turpis. Ut blandit cursus felis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos hymenaeos. Nunc nisi augue, lacinia vitae, blandit nec, venenatis id, tellus. Nam fringilla, enim vitae scelerisque tristique, ipsum neque feugiat dui, vitae auctor enim diam non est. Fusce malesuada, nisl et varius accumsan, lorem ligula luctus est, non nonummy sem magna molestie sapien. ";
            target.Form = val;
        }

        /// <summary>
        ///A test for Frequency
        ///</summary>
        [TestMethod()]
        public void FrequencyTest()
        {
            MedicationLine target = new MedicationLine();
            string val = "FrequencyTest";
            target.Frequency = val;
            Assert.AreEqual(val, target.Frequency, "NhsCui.Toolkit.Web.MedicationLine.Frequency was not set correctly.");
        }

        /// <summary>
        ///A test for Frequency
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void FrequencyTestException()
        {
            MedicationLine target = new MedicationLine();
            string val = "In molestie felis. Donec suscipit. Praesent ut nulla ut lectus facilisis adipiscing. Ut ac ipsum a erat tincidunt accumsan. Nam sed neque sit amet velit porta eleifend. Integer quis magna ut mauris rutrum dignissim. Nunc egestas erat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean facilisis est ut turpis. Ut blandit cursus felis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos hymenaeos. Nunc nisi augue, lacinia vitae, blandit nec, venenatis id, tellus. Nam fringilla, enim vitae scelerisque tristique, ipsum neque feugiat dui, vitae auctor enim diam non est. Fusce malesuada, nisl et varius accumsan, lorem ligula luctus est, non nonummy sem magna molestie sapien. ";
            target.Frequency = val;
        }

        /// <summary>
        ///A test for IndicatorGraphic
        ///</summary>
        [TestMethod()]
        public void IndicatorGraphicTest()
        {
            MedicationLine target = new MedicationLine();
            string val = "~/images/icon/image.png";
            target.IndicatorGraphic = val;
            Assert.AreEqual(val, target.IndicatorGraphic, "NhsCui.Toolkit.Web.MedicationLine.IndicatorGraphic was not set correctly.");
        }

        /// <summary>
        ///A test for IsSelected
        ///</summary>
        [TestMethod()]
        public void IsSelectedTest()
        {
            MedicationLine target = new MedicationLine();
            bool val = true;
            target.IsSelected = val;
            Assert.AreEqual(val, target.IsSelected, "NhsCui.Toolkit.Web.MedicationLine.IsSelected was not set correctly.");

            val = false;
            target.IsSelected = val;
            Assert.AreEqual(val, target.IsSelected, "NhsCui.Toolkit.Web.MedicationLine.IsSelected was not set correctly.");
        }

        /// <summary>
        ///A test for MedicationNames
        ///</summary>
        [TestMethod()]
        public void MedicationNamesTest()
        {
            MedicationLine target = new MedicationLine();
            MedicationName name = new MedicationName("NameTest", "InfoTest");
            target.MedicationNames.Add(name);
            Assert.AreEqual(name, target.MedicationNames[0], "NhsCui.Toolkit.Web.MedicationLine.MedicationNames was not set correctly.");
        }

        /// <summary>
        ///A test for OnClick (EventArgs)
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        public void OnClickTest()
        {
            MedicationLine target = new MedicationLine();
            target.Click += new EventHandler(this.Target_Click);
            NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_MedicationLineAccessor accessor = new NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_MedicationLineAccessor(target);
            EventArgs e = null; 
            this.eventRaisedEvent.Reset();
            accessor.OnClick(e);
            this.WaitForEvent("OnClick");
            target.Click -= new EventHandler(this.Target_Click);
        }

        /// <summary>
        ///A test for OnClientClick
        ///</summary>
        [TestMethod()]
        public void OnClientClickTest()
        {
            MedicationLine target = new MedicationLine();
            string val = "clickHandler";
            target.OnClientClick = val;
            Assert.AreEqual(val, target.OnClientClick, "NhsCui.Toolkit.Web.MedicationLine.OnClientClick was not set correctly.");
        }

        /// <summary>
        ///A test for OnClientDoubleClick
        ///</summary>
        [TestMethod()]
        public void OnClientDoubleClickTest()
        {
            MedicationLine target = new MedicationLine();
            string val = "doubleClickHander";
            target.OnClientDoubleClick = val;
            Assert.AreEqual(val, target.OnClientDoubleClick, "NhsCui.Toolkit.Web.MedicationLine.OnClientDoubleClick was not set correctly.");
        }

        /// <summary>
        ///A test for OnClientRightClick
        ///</summary>
        [TestMethod()]
        public void OnClientRightClickTest()
        {
            MedicationLine target = new MedicationLine();
            string val = "onClientRightClick";
            target.OnClientRightClick = val;
            Assert.AreEqual(val, target.OnClientRightClick, "NhsCui.Toolkit.Web.MedicationLine.OnClientRightClick was not set correctly.");
        }

        /// <summary>
        ///A test for OnDoubleClick (EventArgs)
        ///</summary>
        [DeploymentItem("NhsCui.Toolkit.Web.dll")]
        [TestMethod()]
        public void OnDoubleClickTest()
        {
            MedicationLine target = new MedicationLine();
            target.DoubleClick += new EventHandler(this.Target_DoubleClick);
            NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_MedicationLineAccessor accessor = new NhsCui.Toolkit.Web.Test.NhsCui_Toolkit_Web_MedicationLineAccessor(target);
            EventArgs e = null; 
            this.eventRaisedEvent.Reset();
            accessor.OnDoubleClick(e);
            this.WaitForEvent("DoubleClick");
        }

        /// <summary>
        ///A test for Reason
        ///</summary>
        [TestMethod()]
        public void ReasonTest()
        {
            MedicationLine target = new MedicationLine();
            string val = "Valid Reason";
            target.Reason = val;
            Assert.AreEqual(val, target.Reason, "NhsCui.Toolkit.Web.MedicationLine.Reason was not set correctly.");
        }

        /// <summary>
        ///A test for Reason
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReasonTestException()
        {
            MedicationLine target = new MedicationLine();
            string val = "In molestie felis. Donec suscipit. Praesent ut nulla ut lectus facilisis adipiscing. Ut ac ipsum a erat tincidunt accumsan. Nam sed neque sit amet velit porta eleifend. Integer quis magna ut mauris rutrum dignissim. Nunc egestas erat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean facilisis est ut turpis. Ut blandit cursus felis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos hymenaeos. Nunc nisi augue, lacinia vitae, blandit nec, venenatis id, tellus. Nam fringilla, enim vitae scelerisque tristique, ipsum neque feugiat dui, vitae auctor enim diam non est. Fusce malesuada, nisl et varius accumsan, lorem ligula luctus est, non nonummy sem magna molestie sapien. ";
            target.Reason = val;
            Assert.AreEqual(val, target.Reason, "NhsCui.Toolkit.Web.MedicationLine.Reason was not set correctly.");
        }

        /// <summary>
        ///A test for Route
        ///</summary>
        [TestMethod()]
        public void RouteTest()
        {
            MedicationLine target = new MedicationLine();
            string val = "Route Test";
            target.Route = val;
            Assert.AreEqual(val, target.Route, "NhsCui.Toolkit.Web.MedicationLine.Route was not set correctly.");
        }

        /// <summary>
        ///A test for Route
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RouteTestException()
        {
            MedicationLine target = new MedicationLine();
            string val = "In molestie felis. Donec suscipit. Praesent ut nulla ut lectus facilisis adipiscing. Ut ac ipsum a erat tincidunt accumsan. Nam sed neque sit amet velit porta eleifend. Integer quis magna ut mauris rutrum dignissim. Nunc egestas erat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean facilisis est ut turpis. Ut blandit cursus felis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos hymenaeos. Nunc nisi augue, lacinia vitae, blandit nec, venenatis id, tellus. Nam fringilla, enim vitae scelerisque tristique, ipsum neque feugiat dui, vitae auctor enim diam non est. Fusce malesuada, nisl et varius accumsan, lorem ligula luctus est, non nonummy sem magna molestie sapien. ";
            target.Route = val;
            Assert.AreEqual(val, target.Route, "NhsCui.Toolkit.Web.MedicationLine.Route was not set correctly.");
        }

        /// <summary>
        ///A test for ShowDosageDetails
        ///</summary>
        [TestMethod()]
        public void ShowDosageDetailsTest()
        {
            MedicationLine target = new MedicationLine();
            bool val = true;
            target.ShowDosageDetails = val;
            Assert.AreEqual(val, target.ShowDosageDetails, "NhsCui.Toolkit.Web.MedicationLine.ShowDosageDetails was not set correctly.");
        }

        /// <summary>
        ///A test for ShowGraphics
        ///</summary>
        [TestMethod()]
        public void ShowGraphicsTest()
        {
            MedicationLine target = new MedicationLine();
            bool val = true;
            target.ShowGraphics = val;
            Assert.AreEqual(val, target.ShowGraphics, "NhsCui.Toolkit.Web.MedicationLine.ShowGraphics was not set correctly.");
        }

        /// <summary>
        ///A test for ShowReason
        ///</summary>
        [TestMethod()]
        public void ShowReasonTest()
        {
            MedicationLine target = new MedicationLine();
            bool val = true;
            target.ShowReason = val;
            Assert.AreEqual(val, target.ShowReason, "NhsCui.Toolkit.Web.MedicationLine.ShowReason was not set correctly.");
        }

        /// <summary>
        ///A test for ShowStatusDate
        ///</summary>
        [TestMethod()]
        public void ShowStatusDateTest()
        {
            MedicationLine target = new MedicationLine();
            bool val = true;
            target.ShowStatusDate = val;
            Assert.AreEqual(val, target.ShowStatusDate, "NhsCui.Toolkit.Web.MedicationLine.ShowStatusDate was not set correctly.");
        }

        /// <summary>
        ///A test for ShowStatus
        ///</summary>
        [TestMethod()]
        public void ShowStatusTest()
        {
            MedicationLine target = new MedicationLine();
            bool val = true;
            target.ShowStatus = val;
            Assert.AreEqual(val, target.ShowStatus, "NhsCui.Toolkit.Web.MedicationLine.ShowStatus was not set correctly.");
        }

        /// <summary>
        ///A test for SimpleMode
        ///</summary>
        [TestMethod()]
        public void SimpleModeTest()
        {
            MedicationLine target = new MedicationLine();
            bool val = true;
            target.SimpleMode = val;
            Assert.AreEqual(val, target.SimpleMode, "NhsCui.Toolkit.Web.MedicationLine.SimpleMode was not set correctly.");
        }

        /// <summary>
        ///A test for StartDate
        ///</summary>
        [TestMethod()]
        public void StartDateTest()
        {
            MedicationLine target = new MedicationLine();
            DateTime val = DateTime.Now;
            target.StartDate = val;
            Assert.AreEqual(val, target.StartDate, "NhsCui.Toolkit.Web.MedicationLine.StartDate was not set correctly.");
        }

        /// <summary>
        ///A test for StatusDate
        ///</summary>
        [TestMethod()]
        public void StatusDateTest()
        {
            MedicationLine target = new MedicationLine();
            DateTime val = DateTime.Now;
            target.StatusDate = val;
            Assert.AreEqual(val, target.StatusDate, "NhsCui.Toolkit.Web.MedicationLine.StatusDate was not set correctly.");
        }

        /// <summary>
        ///A test for Status
        ///</summary>
        [TestMethod()]
        public void StatusTest()
        {
            MedicationLine target = new MedicationLine();
            MedicationStatus val = MedicationStatus.Active;
            target.Status = val;
            Assert.AreEqual(val, target.Status, "NhsCui.Toolkit.Web.MedicationLine.Status was not set correctly.");
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
        private void Target_Click(object sender, EventArgs e)
        {
            this.eventRaisedEvent.Set();
        }

        /// <summary>
        /// Doulbe Click Test
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">EventArgs</param>
        private void Target_DoubleClick(object sender, EventArgs e)
        {
            this.eventRaisedEvent.Set();
        }
    }
}
