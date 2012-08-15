//-----------------------------------------------------------------------
// <copyright file="RuleStrategy.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
    #region "Using"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;
    #endregion

    /// <summary>
    /// Rules strategy.
    /// </summary>
    public class RuleStrategy
    {
        /// <summary>
        /// Valid separator between fields.
        /// </summary>
        private const string ValidSeparator = " - ";

        /// <summary>
        /// Null separator, used if a field is null.
        /// </summary>
        private const string NullSeparator = "";
        
        /// <summary>
        /// Level Name.
        /// </summary>
        private string levelName;

        /// <summary>
        /// Gets or sets the level name for the rules implementation.
        /// </summary>
        /// <value>The name of the level.</value>
        public string LevelName
        {
            get
            {
                return this.levelName;
            }

            set
            {
                this.levelName = value;
            }
        }

        /// <summary>
        /// Executes the rules and appends selected templates to the output row.
        /// </summary>
        /// <param name="inputRow">Input Row of data.</param>        
        public void Execute(Dictionary<string, string> inputRow)
        {
            CfhRulesDataStore rulesData = new CfhRulesDataStore();
            string drugType;
            string templateName = "DrugDetailsTemplateDefault";
            List<CfhRuleOutput> templates;

            if (inputRow["Form"] == null || inputRow["Route"] == null)
            {
                throw new ArgumentException("Invalid row specified");
            }

            this.GetDrugName(inputRow);
            
            drugType = CfhRulesDataStore.QueryDrugType(inputRow["Form"], inputRow["Route"]);
            if (!string.IsNullOrEmpty(drugType))
            {
                templates = rulesData.ExecuteRules(drugType, inputRow);

                if (templates != null)
                {
                    foreach (CfhRuleOutput output in templates)
                    {
                        if (output.LevelName.Equals(this.LevelName, StringComparison.OrdinalIgnoreCase))
                        {
                            templateName = output.TemplateName;
                            break;
                        }
                    }
                }
            }

            inputRow.Add("TemplateName", templateName);
            this.ApplyFormatting(inputRow);
        }

        /// <summary>
        /// Derives the value for DrugName.
        /// </summary>
        /// <param name="inputRow">Input row.</param>
        private void GetDrugName(Dictionary<string, string> inputRow)
        {
            string drugName = String.Empty;
            if (!string.IsNullOrEmpty(inputRow["VMPID"]))
            {
               drugName = CfhRulesDataStore.GetVMPName(inputRow["VMPID"]);
            }
            else if (!string.IsNullOrEmpty(inputRow["VTMID"]))
            {
                drugName = CfhRulesDataStore.GetVTMName(inputRow["VTMID"]); 
            }
            else if (!string.IsNullOrEmpty(inputRow["AMPID"]))
            {
                drugName = CfhRulesDataStore.GetAMPName(inputRow["AMPID"]);
            }

            if (inputRow.ContainsKey("DrugName"))
            {
                inputRow["DrugName"] = drugName;
            }
            else
            {
                inputRow.Add("DrugName", drugName);
            }
        }     

        /// <summary>
        /// Applies conditional formatting.
        /// </summary>
        /// <param name="inputRow">Input row of data.</param>                
        private void ApplyFormatting(Dictionary<string, string> inputRow)
        {
            this.ApplyCapitalization(inputRow);
            this.AddLabels(inputRow);
            this.UpdateFrequencyAttribute(inputRow);
            this.AddSeparatorAttributes(inputRow);
        }

        /// <summary>
        /// Adds the separator attributes to the input row.
        /// </summary>
        /// <param name="inputRow">Input row of data.</param>
        private void AddSeparatorAttributes(Dictionary<string, string> inputRow)
        {
            inputRow.Add("DrugNameSeparator", string.IsNullOrEmpty(inputRow["DrugName"]) ? NullSeparator : ValidSeparator);
            inputRow.Add("StrengthSeparator", string.IsNullOrEmpty(inputRow["Strength"]) ? NullSeparator : ValidSeparator);
            inputRow.Add("FormNameSeparator", string.IsNullOrEmpty(inputRow["FormName"]) ? NullSeparator : ValidSeparator);
            inputRow.Add("RouteNameSeparator", string.IsNullOrEmpty(inputRow["RouteName"]) ? NullSeparator : ValidSeparator);
            inputRow.Add("DOSEQTYSeparator", string.IsNullOrEmpty(inputRow["DOSEQTY"]) ? NullSeparator : ValidSeparator);
            inputRow.Add("DoseDurationSeparator", string.IsNullOrEmpty(inputRow["DoseDuration"]) ? NullSeparator : ValidSeparator);
            inputRow.Add("RateSeparator", string.IsNullOrEmpty(inputRow["Rate"]) ? NullSeparator : ValidSeparator);
            inputRow.Add("MethodSeparator", string.IsNullOrEmpty(inputRow["Method"]) ? NullSeparator : ValidSeparator);
            inputRow.Add("SiteSeparator", string.IsNullOrEmpty(inputRow["Site"]) ? NullSeparator : ValidSeparator);
            inputRow.Add("FrequencySeparator", string.IsNullOrEmpty(inputRow["Frequency"]) ? NullSeparator : ValidSeparator);
            inputRow.Add("CourseDurationSeparator", string.IsNullOrEmpty(inputRow["CourseDuration"]) ? NullSeparator : ValidSeparator);
            inputRow.Add("MedicationTypeSeparator", string.IsNullOrEmpty(inputRow["MedicationType"]) ? NullSeparator : ValidSeparator);
        }

        /// <summary>
        /// Applies capitalization to drug name.
        /// </summary>
        /// <param name="inputRow">Input row of data.</param>
        private void ApplyCapitalization(Dictionary<string, string> inputRow)
        {
            // check if DrugName should be changed to uppercase
            if (!string.IsNullOrEmpty(inputRow["VMPID"]))
            {
                string vtm = CfhRulesDataStore.GetVTMID(inputRow["VMPID"]);
                if (string.IsNullOrEmpty(vtm))
                {
                    inputRow["DrugName"] = inputRow["DrugName"].ToUpper();
                }
            }
        }

        /// <summary>
        /// Adds an attribute for end label.
        /// </summary>
        /// <param name="inputRow">Input row of data.</param>
        private void AddLabels(Dictionary<string, string> inputRow)
        {
            this.AddDoseLabel(inputRow);
            this.AddEndDateLabel(inputRow);
            this.AddReviewDateLabel(inputRow);
        }

        /// <summary>
        /// Adds the review label for UI.
        /// </summary>
        /// <param name="inputRow">Input row of data.</param>
        private void AddReviewDateLabel(Dictionary<string, string> inputRow)
        {
            if (!inputRow.ContainsKey("ReviewDate"))
            {
                inputRow.Add("ReviewDateLabel", "");
                inputRow.Add("ReviewDate", "");
            }
            else
            {
                if (!string.IsNullOrEmpty(inputRow["ReviewDate"]))
                {
                    inputRow.Add("ReviewDateLabel", "Review ");
                }
                else
                {
                    inputRow.Add("ReviewDateLabel", "");
                }
            }
        }
        
        /// <summary>
        /// Adds the labels for UI.
        /// </summary>
        /// <param name="inputRow">Input row of data.</param>
        private void AddEndDateLabel(Dictionary<string, string> inputRow)
        {
            if (!string.IsNullOrEmpty(inputRow["StopDate"]))
            {
                inputRow.Add("EndDateLabel", "End ");
            }
            else
            {
                inputRow.Add("EndDateLabel", "");
            }
        }
        
        /// <summary>
        /// Adds an attribute for dose label.
        /// </summary>
        /// <param name="inputRow">Input row of data.</param>
        private void AddDoseLabel(Dictionary<string, string> inputRow)
        {
            if (!string.IsNullOrEmpty(inputRow["DOSEQTY"]))
            {
                inputRow.Add("DoseLabel", "DOSE ");
            }
            else
            {
                inputRow.Add("DoseLabel", "");
            }
        }

        /// <summary>
        /// Updates the display value of frequency.
        /// </summary>
        /// <param name="inputRow">Input row of data.</param>
        private void UpdateFrequencyAttribute(Dictionary<string, string> inputRow)
        {
            if (!string.IsNullOrEmpty(inputRow["Frequency"]))
            {
                if (string.Compare(inputRow["Frequency"], "once", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    inputRow["Frequency"] = "once only";
                }
                else if (string.Compare(inputRow["Frequency"], "EveryNHours", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    int frequency;
                    if (!string.IsNullOrEmpty(inputRow["FrequencyAmount"]) && int.TryParse(inputRow["FrequencyAmount"], out frequency))
                    {
                        StringBuilder text = new StringBuilder("every ");
                        text.Append((frequency > 1 ? inputRow["FrequencyAmount"] + " hours" : "hour"));

                        // PS 7939 - Remove word "Regular" from co-amoxiclav medication line, removed this section of code
                        inputRow["Frequency"] = text.ToString();
                    }
                }
            }           
        }
    }
}
