// <copyright file="MedicationsReader.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>10-Jan-2008</date>
// <summary>Medications reader</summary>

namespace Microsoft.Cui.SampleWebsite
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml;
    using NhsCui.Toolkit.Web;

    /// <summary>  
    /// Read Medication Items from an Xml file 
    /// </summary>
    public class MedicationsReader
    {
        /// <summary>
        /// Xml Document
        /// </summary>
        private XmlDocument xmlDom;

        /// <summary>
        /// Load the xml Document.
        /// </summary>
        /// <param name="filename">Xml File Name.</param>
        public MedicationsReader(string filename)
        {
            this.xmlDom = new XmlDocument();
            this.xmlDom.Load(filename);
        }

        /// <summary>
        /// Enum to filter the Medications
        /// </summary>
        public enum StatusFilter
        {
            /// <summary>
            /// Only Active Medications
            /// </summary>
            Active = 0,

            /// <summary>
            /// Only Suspended Medications
            /// </summary>
            Suspended,

            /// <summary>
            /// Both Active/Suspended Medications
            /// </summary>
            Full
        }

        /// <summary>
        /// Parse document function added for backward functionality 
        /// </summary>
        /// <returns>Collection of Medications</returns>
        public Medication[] Parse()
        {
            return this.Parse(StatusFilter.Full, "Subramanyan");
        }

        /// <summary>
        /// Parse the XML document
        /// </summary>
        /// <param name="filter">Enum StatusFilter</param>
        /// <param name="familyName">Family Name of the Patien</param>
        /// <returns>Collection of Medication</returns>
        public Medication[] Parse(StatusFilter filter, string familyName)
        {
            List<Medication> medicationList = new List<Medication>();

            ////XmlNodeList medicationNodes = this.xmlDom.SelectNodes("/Patients/Patient/Medications/Medication");
            ////Get medications for a specific patient
            XmlNodeList medicationNodes = this.xmlDom.SelectNodes("//Medication[ancestor::*/@FamilyName='" + familyName + "']");
            bool medicationValid = false;

            int index = 1;
            if (medicationNodes.Count > 0)
            {
                foreach (XmlNode medicationNode in medicationNodes)
                {
                    switch (filter)
                    {
                        case StatusFilter.Full:
                            medicationValid = true;
                            break;
                        case StatusFilter.Active:
                            if ((MedicationStatus)Enum.Parse(typeof(MedicationStatus), this.GetAttribute(medicationNode, "Status")) == MedicationStatus.Active)
                            {
                                medicationValid = true;
                            }

                            break;
                        case StatusFilter.Suspended:
                            if ((MedicationStatus)Enum.Parse(typeof(MedicationStatus), this.GetAttribute(medicationNode, "Status")) == MedicationStatus.Suspended)
                            {
                                medicationValid = true;
                            }

                            break;
                    }

                    if (medicationValid)
                    {
                        Medication medication = new Medication();
                        XmlNodeList medicationNameNodes = medicationNode.SelectNodes("MedicationNames/MedicationName");
                        foreach (XmlNode medicationNameNode in medicationNameNodes)
                        {
                            medication.MedicationNames.Add(new MedicationName(this.GetAttribute(medicationNameNode, "Name"), this.GetAttribute(medicationNameNode, "Information")));
                            index++;
                        }

                        medication.CriticalAlertGraphic = this.GetAttribute(medicationNode, "CriticalAlertGraphic");
                        medication.IndicatorGraphic = this.GetAttribute(medicationNode, "IndicatorGraphic");
                        medication.Dose = this.GetAttribute(medicationNode, "Dose");
                        medication.Form = this.GetAttribute(medicationNode, "Form");
                        medication.Frequency = this.GetAttribute(medicationNode, "Frequency");
                        medication.Reason = this.GetAttribute(medicationNode, "Reason");
                        medication.Route = this.GetAttribute(medicationNode, "Route");
                        medication.MedicationTooltip = this.GetAttribute(medicationNode, "ToolTip");

                        medication.StartDate = DateTime.ParseExact(
                                                                    this.GetAttribute(medicationNode, "StartDate"),
                                                                    new string[] { "dd-MMM-yy", "dd/mm/yyyy" },
                                                                    CultureInfo.CurrentCulture,
                                                                    DateTimeStyles.None);

                        string statusDate = this.GetAttribute(medicationNode, "StatusDate");
                        if (!string.IsNullOrEmpty(statusDate))
                        {
                            medication.StatusDate = DateTime.ParseExact(
                                                                    statusDate,
                                                                    new string[] { "dd-MMM-yy", "dd/mm/yyyy" },
                                                                    CultureInfo.CurrentCulture,
                                                                    DateTimeStyles.None);
                        }

                        medication.Status = (MedicationStatus)Enum.Parse(typeof(MedicationStatus), this.GetAttribute(medicationNode, "Status"));
                        medicationList.Add(medication);
                    }

                    medicationValid = false;
                }
            }

            return medicationList.ToArray();
        }

        /// <summary>
        /// Get Node Attribute Value
        /// </summary>
        /// <param name="node">Xml Node to be checked</param>
        /// <param name="name">Attribute Name</param>
        /// <returns>Xml Node Value</returns>
        private string GetAttribute(XmlNode node, string name)
        {
            XmlAttribute attribute = node.Attributes[name];
            if (attribute == null || string.IsNullOrEmpty(attribute.Value))
            {
                return string.Empty;
            }
            else
            {
                return attribute.Value;
            }
        }
    }
}