//-----------------------------------------------------------------------
// <copyright file="RuleManager.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>10-Feb-2008</date>
// <summary> Rules manager class </summary>
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
    /// Rules manager.
    /// </summary>
    public class RuleManager
    {
        #region Member vars
        /// <summary>
        /// Strategy class.
        /// </summary>
        private RuleStrategy strategy;        
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the strategy object.
        /// </summary>
        /// <value>The strategy.</value>
        public RuleStrategy Strategy
        {
            get
            {
                return this.strategy;
            }

            set
            {
                this.strategy = value;
            }
        }
        #endregion        

        #region Public Methods
        // TODO : define exception handling policy. For now, rule manager 
        // is not handling exceptions and they will pass to the calling layer. 

        /// <summary>
        /// Applies rules on the input row.
        /// </summary>
        /// <param name="rows">Input rows collection.</param>                    
        public void ApplyRules(List<Dictionary<string, string>> rows)
        {
            if (this.strategy == null)
            {
                throw new NullReferenceException("No Strategy specified for the class");
            }

            foreach (Dictionary<string, string> inputRow in rows)
            {
                this.Strategy.Execute(inputRow);              
            }                       
        }      
        #endregion       
    }
}
