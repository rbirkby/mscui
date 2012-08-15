//-----------------------------------------------------------------------
// <copyright file="DataManager.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Data Manager class </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Data
{
    #region "using"
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using System.Reflection;
    using System.Xml;
    using System.Collections.ObjectModel;
    using System.Globalization;
    #endregion

    /// <summary>
    /// This class acts a broker between UI Layer and ISV dataprovider.
    /// </summary>
    public class DataManager : IEnumerable
    {
        #region Member Fields
        /// <summary>
        /// GetData method name.
        /// </summary>
        private string getDataMethod = "GetPage";
        
        /// <summary>
        /// Rule manager object for applying rules.
        /// </summary>
        private RuleManager ruleManager;

        /// <summary>
        /// ColumnMapping object.
        /// </summary>
        private ColumnMappingList columnMappings = new ColumnMappingList();

        /// <summary>
        /// Object to hold the rows returned by ISV.
        /// </summary>
        private List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();

        /// <summary>
        /// Total medication rows.
        /// </summary>
        private int totalRowCount = -1;

        /// <summary>
        /// Specifies the number of records to be fetched at one time.
        /// </summary>
        private int pageSize = 100;

        /// <summary>
        /// Specifies the limit after which we need to fetch the rows again.
        /// </summary>
        private int threshold = 75;

        /// <summary>
        /// Cache of the rows already retrieved.
        /// </summary>
        private VirtualSet cache = new VirtualSet();

        /// <summary>
        /// ISV data provider object.
        /// </summary>
        private object isvDataProvider;
        #endregion      

        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public DataManager()
        {           
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the IsvDataProvider object.
        /// </summary>
        /// <value>The ISV data provider.</value>
        public object IsvDataProvider
        {
            get
            {
                return this.isvDataProvider;
            }

            set
            {
                this.isvDataProvider = value;
            }
        }

        /// <summary>
        /// Gets or sets the Column mappings to be applied.
        /// </summary>
        /// <value>The column mappings.</value>
        public ColumnMappingList ColumnMappings
        {
            get
            {
                return this.columnMappings;
            }

            set
            {
                this.columnMappings = value;
            }
        }

        /// <summary>
        /// Gets the number of records in the cache.
        /// </summary>
        /// <value>The cache record count.</value>
        public int CacheRecordCount
        {
            get
            {
                return this.cache.DataRows.Count;
            }
        }

        /// <summary>
        /// Gets or sets the RuleManger.
        /// </summary>
        /// <value>The rule manager.</value>
        public RuleManager RuleManager
        {
            get
            {
                return this.ruleManager;
            }

            set
            {
                this.ruleManager = value;
            }
        }

        /// <summary>
        /// Gets or sets the GetData method specified in the ISV implementation.
        /// </summary>
        /// <value>The get data method.</value>
        public string GetDataMethod
        {
            get
            {
                return this.getDataMethod;
            }

            set
            {
                this.getDataMethod = value;
            }
        }       
        #endregion

        #region Public Methods
        /// <summary>
        /// Clears the local cache.
        /// </summary>        
        public void Flush()
        {
            if (this.rows != null)
            {
                this.rows.Clear();
                this.cache.DataRows.Clear();
                this.totalRowCount = -1;
            }
        }       

        /// <summary>
        /// Gets an enumerator to loop through the rows.
        /// </summary>
        /// <returns>Enumerator to loop through the rows.</returns>
        public System.Collections.IEnumerator GetEnumerator()
        {
            if (this.totalRowCount == -1)
            {
                this.GetData(0);
            }

            DataManagerEnumerator enumProvider = new DataManagerEnumerator();
            enumProvider.DataManager = this;
            return enumProvider;
        }
        
        #endregion

        #region Internal Methods
        /// <summary>
        /// Gets the row at a specified ordinal position.
        /// </summary>
        /// <param name="ordinalPosition">Ordinal position of the row to be returned.</param>
        /// <returns>Row at the specified position.</returns>
        internal Dictionary<string, string> GetItem(int ordinalPosition)
        {
            if (ordinalPosition >= this.cache.DataRows.Count)
            {
                this.GetData(ordinalPosition);
            }

            if (this.cache.DataRows[ordinalPosition.ToString(CultureInfo.CurrentCulture)] != null)
            {
                return this.cache.DataRows[ordinalPosition.ToString(CultureInfo.CurrentCulture)];
            }

            return null;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets the data from ISV data provider.
        /// </summary>
        /// <param name="ordinalPosition">Starting row position from which the data has to be returned.</param>
        private void GetData(int ordinalPosition)
        {
            this.GetIsvData(ordinalPosition);            
            this.ApplyRules();
            this.ApplyColumnMappings();
            this.UpdateCache();
        }

        /// <summary>
        /// Apply column mappings.
        /// </summary>
        private void ApplyColumnMappings()
        {
            if (this.rows != null && this.rows.Count > 0 && this.ColumnMappings != null && this.ColumnMappings.Count > 0)
            {
                foreach (Dictionary<string, string> medicationRow in this.rows)
                {
                    foreach (Data.ColumnMapping columnMapping in this.ColumnMappings)
                    {
                        if (medicationRow[columnMapping.DataColumnName] != null)
                        {
                            medicationRow.Add(columnMapping.GridColumnName, medicationRow[columnMapping.DataColumnName]);
                            medicationRow.Remove(columnMapping.DataColumnName);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Apply Rules to the rows provided by ISV.
        /// </summary>
        private void ApplyRules()
        {
            if (this.ruleManager != null && this.rows != null)
            {
                this.ruleManager.ApplyRules(this.rows);
            }
        }        

        /// <summary>
        /// Get data from ISV.
        /// </summary>
        /// <param name="startOrdinalPosition">Specifies the start position from which to fetch the rows.</param>
        private void GetIsvData(int startOrdinalPosition)
        {
            if (this.IsvDataProvider != null)
            {
                object[] args = new object[] { this.rows, this.totalRowCount, startOrdinalPosition, this.pageSize };

                this.IsvDataProvider.GetType().InvokeMember(this.GetDataMethod, BindingFlags.Default | BindingFlags.InvokeMethod, null, this.isvDataProvider, args, null, CultureInfo.CurrentCulture, null);

                this.rows = args[0] as List<Dictionary<string, string>>;
                this.totalRowCount = (int)args[1];                
            }
        }

        /// <summary>
        /// Update the local cache.
        /// </summary>
        private void UpdateCache()
        {
            if (this.rows != null)
            {
                foreach (Dictionary<string, string> dataRow in this.rows)
                {
                    this.cache.DataRows.Add(dataRow["RowId"], dataRow);
                }

                this.AddRowCounts();
            }
         }

        /// <summary>
        /// Adds row counts to the dictionary.
        /// </summary>
        private void AddRowCounts()
        {
            int rowCount = this.cache.DataRows.Count;
            for (int i = 0; i < rowCount; i++)
            {
              Dictionary<string, string> inputRow = this.cache.DataRows[i.ToString(CultureInfo.CurrentCulture)];
              inputRow.Add("RowCount", rowCount.ToString(CultureInfo.CurrentCulture));
              inputRow.Add("CurrentRow", (i + 1).ToString(CultureInfo.CurrentCulture));
              inputRow.Add("RowsBelow", ((rows.Count - 1) - i).ToString(CultureInfo.CurrentCulture));
              inputRow.Add("RowsAbove", i.ToString(CultureInfo.CurrentCulture));   
            }           
        }
        #endregion
    }
}
