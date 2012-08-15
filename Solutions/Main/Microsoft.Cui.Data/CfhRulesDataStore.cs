//-----------------------------------------------------------------------
// <copyright file="CfhRulesDataStore.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>12-Feb-2008</date>
// <summary> Contains the methods to aceess xml data for rules processing</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Data
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.Xml;
    using System.Reflection;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Contains methods to access data for rules processing.
    /// </summary>
    public class CfhRulesDataStore
    {
        #region Member Vars

        /// <summary>
        /// Available rules list.
        /// </summary>
        private CfhRule[] rules = new CfhRule[3];

        /// <summary>
        /// List of templates.
        /// </summary>
        private List<CfhRuleOutput> templates;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new instance of RulesDataStore.
        /// </summary>
        public CfhRulesDataStore()
        {
            this.rules[0] = new StrengthRule();
            this.rules[1] = new SachetRule();
            this.rules[2] = new StrengthForGases();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Returns thge VMPName for the specified ID.
        /// </summary>
        /// <param name="vmpID">ID of VMP to search.</param>
        /// <returns>
        /// Corresponding drug name based upon the search rules.
        /// </returns>
        /// <remarks>
        /// If VTMID is null for a VMP then instead of drug name the name field from VMP table is returned.
        /// </remarks>
        public static string GetVMPName(string vmpID)
        {
            string result = null;
            bool found = false;
            using (XmlReader reader = GetReader("Microsoft.Cui.Data.VMP.xml"))
            {
                reader.MoveToContent();
                while (reader.ReadToFollowing("VMPID"))
                {
                    if (string.Compare(reader.ReadInnerXml(), vmpID, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        // Check if vtmid is defined, if found then use it to fetch the name.
                        if (reader.ReadToFollowing("VTMID"))
                        {
                            string vtm = reader.ReadInnerXml();
                            if (!string.IsNullOrEmpty(vtm))
                            {
                                result = GetVTMName(vtm);
                                found = true;
                            }
                        }
                        
                        if (!found && reader.ReadToFollowing("Name"))
                        {
                            result = reader.ReadInnerXml();
                        }

                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Finds the VTMName for the specified VTMID.
        /// </summary>
        /// <param name="vtmID">ID of the VTM to search.</param>
        /// <returns>Corresponding VTMName.</returns>
        public static string GetVTMName(string vtmID)
        {
            using (XmlReader reader = GetReader("Microsoft.Cui.Data.VTM.xml"))
            {
                reader.MoveToContent();
                while (reader.ReadToFollowing("VTMID"))
                {
                    if (string.Compare(reader.ReadInnerXml(), vtmID, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        reader.ReadToFollowing("Name");
                        return (reader.ReadInnerXml());
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Finds the AMPName for the specified AMPID.
        /// </summary>
        /// <param name="ampID">ID of the AMP to search.</param>
        /// <returns>Corresponding AMPName.</returns>
        public static string GetAMPName(string ampID)
        {
            using (XmlReader reader = GetReader("Microsoft.Cui.Data.AMP.xml"))
            {
                reader.MoveToContent();
                while (reader.ReadToFollowing("AMPID"))
                {
                    if (string.Compare(reader.ReadInnerXml(), ampID, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        reader.ReadToFollowing("Name");
                        return (reader.ReadInnerXml());
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Searches for the drug type for a given formCode and routeCode.
        /// </summary>
        /// <param name="form">FormCode to search.</param>
        /// <param name="route">RouteCode to search.</param>
        /// <returns>Drug type.</returns>
        public static string QueryDrugType(string form, string route)
        {
            using (XmlReader reader = GetReader("Microsoft.Cui.Data.DrugType.xml"))
            {
                reader.MoveToContent();

                while (reader.ReadToFollowing("FormCode"))
                {
                    if (string.Compare(reader.ReadInnerXml(), form, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        if (reader.ReadToFollowing("RouteCode") && string.Compare(reader.ReadInnerXml(), route, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            if (reader.ReadToFollowing("Type"))
                            {
                                return reader.ReadInnerXml();
                            }
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Finds the VTMID for a particular VMPID.
        /// </summary>
        /// <param name="vmpID">VMPID to search.</param>
        /// <returns>VTMID for the specified VMPID.</returns>
        public static string GetVTMID(string vmpID)
        {
            if (string.IsNullOrEmpty(vmpID))
            {
                return null;
            }

            using (XmlReader reader = GetReader("Microsoft.Cui.Data.VMP.xml"))
            {
                reader.MoveToContent();

                while (reader.ReadToFollowing("VMPID"))
                {
                    if (string.Compare(reader.ReadInnerXml(), vmpID, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        if (reader.ReadToFollowing("VTMID"))
                        {
                            return reader.ReadInnerXml();
                        }
                    }
                }

                return null;
            }
        }
        
        /// <summary>
        /// Executes rules for a given drug type.
        /// </summary>
        /// <param name="drugType">Drug type to execute.</param>
        /// <param name="inputRow">Input Row of data.</param>        
        /// <returns>Associated templates depending upon the successful rule.</returns>
        public List<CfhRuleOutput> ExecuteRules(string drugType, Dictionary<string, string> inputRow)
        {
            using (XmlReader reader = GetReader("Microsoft.Cui.Data.Rules.xml"))
            {
                bool found = false;
                reader.MoveToContent();
                while (reader.ReadToFollowing("DrugType"))
                {
                    if (string.Compare(reader.ReadInnerXml(), drugType, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        found = true;
                        if (this.ExecuteRuleForDrugType(reader, drugType, inputRow))
                        {
                            return this.templates;
                        }
                    }
                    else if (found)
                    {
                        return null;
                    }
                }
            }

            return null;
        }     
        #endregion

        #region Private Methods

        /// <summary>
        /// Returns Xml reader for the specified file name.
        /// </summary>
        /// <param name="fileName">XML File name to load.</param>
        /// <returns>XML Reader.</returns>
        private static XmlReader GetReader(string fileName)
        {
            System.Reflection.Assembly asm = Assembly.GetExecutingAssembly();
            System.IO.Stream xmlStream = asm.GetManifestResourceStream(fileName);
            return (XmlReader.Create(xmlStream));
        }

        /// <summary>
        /// Runs the specified rule.
        /// </summary>
        /// <param name="ruleName">Rule name to execute.</param>
        /// <param name="inputRow">Input row of data.</param>        
        /// <returns>Status of rule.</returns>
        private bool RunRule(string ruleName, Dictionary<string, string> inputRow)
        {
            switch (ruleName)
            {
                case "StrengthRule":
                    return this.rules[0].Execute(inputRow);
                case "SachetRule":
                    return this.rules[1].Execute(inputRow);
                case "StrengthForGases":
                    return this.rules[2].Execute(inputRow);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Creates template for specified drugtype and rule.
        /// </summary>
        /// <param name="drugType">Drug Type of row.</param>
        /// <param name="ruleID">Rule ID to execute.</param>
        private void CreateTemplate(string drugType, string ruleID)
        {
            this.templates = new List<CfhRuleOutput>();

            using (XmlReader reader = GetReader("Microsoft.Cui.Data.Template.xml"))
            {
                bool found = false;
                reader.MoveToContent();
                while (reader.ReadToFollowing("DrugType"))
                {
                    if (string.Compare(reader.ReadInnerXml(), drugType, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        found = true;
                        if (reader.ReadToFollowing("RuleID") && string.Compare(reader.ReadInnerXml(), ruleID, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            CfhRuleOutput output = new CfhRuleOutput();
                            reader.ReadToFollowing("LevelName");
                            output.LevelName = reader.ReadInnerXml();
                            reader.ReadToFollowing("TemplateName");
                            output.TemplateName = reader.ReadInnerXml();
                            this.templates.Add(output);
                        }
                    }
                    else if (found)
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Executes rules for a given drug type.
        /// </summary>
        /// <param name="reader">XML reader object.</param>
        /// <param name="drugType">Drug type to search.</param>
        /// <param name="inputRow">Input row of data.</param>        
        /// <returns>Execution status.</returns>
        private bool ExecuteRuleForDrugType(XmlReader reader, string drugType, Dictionary<string, string> inputRow)
        {
            reader.ReadToFollowing("RuleID");
            string ruleID = reader.ReadInnerXml();
            reader.ReadToFollowing("RuleName");
            string ruleName = reader.ReadInnerXml();
            reader.ReadToFollowing("IsDefault");
            bool defaultRule = Convert.ToBoolean(reader.ReadInnerXml(), CultureInfo.InvariantCulture);
            if (defaultRule || this.RunRule(ruleName, inputRow))
            {
                this.CreateTemplate(drugType, ruleID);
                return true;
            }

            return false;
        }
        #endregion        
    }
}
