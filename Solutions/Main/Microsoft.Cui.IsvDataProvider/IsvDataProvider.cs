//-----------------------------------------------------------------------
// <copyright file="IsvDataProvider.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>IsvDataProvider class </summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.IsvData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using System.Xml;
    using System.Reflection;
    using System.Globalization;

    /// <summary>
    /// Sort types.
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// Ascending order.
        /// </summary>
        Ascending,

        /// <summary>
        /// Descending order.
        /// </summary>
        Descending
    }

    /// <summary>
    /// Data type of the column to be sorted by or grouped by.
    /// </summary>
    public enum ColumnDataType
    {
        /// <summary>
        /// Column is of type string.
        /// </summary>
        String,

        /// <summary>
        /// Column is of type Date.
        /// </summary>
        DateTime
    }

    /// <summary>
    /// Filter conditions. Specifies the conditions to filter the data.
    /// </summary>
    public enum FilterCondition
    {
        /// <summary>
        /// Show only current medications (Started/Suspended/Not started).
        /// </summary>
        Current,

        /// <summary>
        /// Show only past medications (Completed/Discontinued).
        /// </summary>
        Past,        

        /// <summary>
        /// Medications that have been completed or discontinued in the past two months.
        /// </summary>
        PastTwoMonths,

        /// <summary>
        /// Medications that have been completed or discontinued in the past six months.
        /// </summary>
        PastSixMonths
    }

    /// <summary>
    /// Sample ISV Data provider.
    /// </summary>
    public class IsvDataProvider
    {
        #region Private Members
        /// <summary>
        /// Member variable to hold patient id.
        /// </summary>
        private string patientID;

        /// <summary>
        /// List of dictionaries to hold the data read from XML.
        /// </summary>
        private List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();

        /// <summary>
        /// Member variable to hold sort column.
        /// </summary>
        private string sortColumn = string.Empty;

        /// <summary>
        /// Member variable to hold sort type.
        /// </summary>
        private SortDirection sortDirection = SortDirection.Ascending;

        /// <summary>
        /// Member variable to hold the sort column type.
        /// </summary>
        private ColumnDataType columnDataType = ColumnDataType.String;

        /// <summary>
        /// Member variable to hold filter condition.
        /// </summary>
        private FilterCondition filterCondition = FilterCondition.Current;

        /// <summary>
        /// Member variable to hold group by column name.
        /// </summary>
        private string groupByColumnName;

        /// <summary>
        /// Custom Date to use while filtering data.
        /// </summary>
        private DateTime customDate = DateTime.Now;

        /// <summary>
        /// Member variable to hold column name to sort or group by.
        /// </summary>
        private string sortOrGroupColumnName;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the Column to be sorted by. 
        /// </summary>
        /// <remarks>Removes sorting when set to empty. Defaults to Empty</remarks>
        public string SortColumn
        {
            get
            {
                return this.sortColumn;
            }

            set
            {
                this.sortColumn = value;
            }
        }

        /// <summary>
        /// Gets or sets the patient id.
        /// </summary>
        public string PatientId
        {
            get
            {
                return this.patientID;
            }

            set
            {
                this.patientID = value;
            }
        }

        /// <summary>
        /// Gets or sets the direction of sorting. (Ascending or Descending). Defaults to Ascending.
        /// </summary>
        public SortDirection SortDirection
        {
            get
            {
                return this.sortDirection;
            }

            set
            {
                this.sortDirection = value;
            }
        }

        /// <summary>
        /// Gets or sets the data type of the sorting column.
        /// </summary>
        public ColumnDataType ColumnDataType
        {
            get
            {
                return this.columnDataType;
            }

            set
            {
                this.columnDataType = value;
            }
        }

        /// <summary>
        /// Gets or sets the filter conditions to filter the data.
        /// </summary>
        public FilterCondition FilterCondition
        {
            get
            {
                return this.filterCondition;
            }

            set
            {
                this.filterCondition = value;
            }
        }

        /// <summary>
        /// Gets or sets the Column name to group by.
        /// </summary>
        public string GroupBy
        {
            get
            {
                return this.groupByColumnName;
            }

            set
            {
                this.groupByColumnName = value;
            }
        }

        /// <summary>
        /// Gets or sets the CustomDate.
        /// </summary>
        /// <value>
        /// Custom Date.
        /// </value>
        public DateTime CustomDate
        {
            get
            {
                return this.customDate;
            }

            set
            {
                this.customDate = value;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the data from XML file and returns the data to data manager in list of dictionary format.
        /// </summary>
        /// <param name="rows">List of dictionary object to hold the output data.</param>
        /// <param name="totalRowCount">Total number of rows returned.</param>
        /// <param name="startOrdinalPosition">Specifies the start position of the data from where to fetch the data.</param>
        /// <param name="pageSize">Specifies the number of records to be returned.</param>        
        public void GetPage(out List<Dictionary<string, string>> rows, out int totalRowCount, int startOrdinalPosition, int pageSize)
        {
            if (this.patientID == null)
            {
                rows = null;
                totalRowCount = -1;
                return;
            }

            this.rows.Clear();
            totalRowCount = 0;
            rows = this.rows;

            XmlReader dataReader = IsvDataProvider.GetReader("Microsoft.Cui.IsvData.BabyEvansData.xml");
            
            if (dataReader != null)
            {
                dataReader.MoveToContent();

                while (dataReader.ReadToFollowing(ISVDataProviderResources.Patient))
                {
                    if (dataReader.HasAttributes && dataReader.GetAttribute(ISVDataProviderResources.PatientId) != null)
                    {
                        if (string.Compare(dataReader.GetAttribute(ISVDataProviderResources.PatientId), this.patientID, StringComparison.CurrentCultureIgnoreCase) == 0)
                        {
                            dataReader = dataReader.ReadSubtree();

                            while (dataReader.ReadToFollowing(ISVDataProviderResources.Medication))
                            {
                                this.GetPatientMedications(dataReader.ReadSubtree());
                            }
                        }
                    }
                }

                this.SortOrGroup();
                totalRowCount = this.rows.Count;

                this.rows.RemoveRange(0, startOrdinalPosition);

                if (this.rows.Count > pageSize)
                {
                    this.rows.RemoveRange(pageSize, this.rows.Count - pageSize);
                }
            }
        }
        #endregion

        #region Private Static
        /// <summary>
        /// Returns Xml reader for the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the XML file where the data is present.</param>
        /// <returns>Returns a reader object which reads the XML file specified in the parameter.</returns>
        private static XmlReader GetReader(string fileName)
        {
            System.Reflection.Assembly asm = Assembly.GetExecutingAssembly();
            System.IO.Stream xmlStream = asm.GetManifestResourceStream(fileName);
            return (XmlReader.Create(xmlStream));
        }

        /// <summary>
        /// Gets a value indicating which type of Comparer to use. 
        /// </summary>
        /// <param name="column">Name of the column to compare.</param>
        /// <returns>True if the column is of type string, otherwise false.</returns>
        private static bool IsStringComparer(string column)
        {
            if (column.Contains("Date"))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks whether the medication is of type current or past.
        /// </summary>
        /// <param name="medicationStatus">Medication status.</param>
        /// <returns>True if current medication, False if past medication.</returns>
        private static bool IsCurrentMedication(string medicationStatus)
        {
            if (string.Compare(medicationStatus, ISVDataProviderResources.NotStarted, StringComparison.CurrentCultureIgnoreCase) == 0 || string.Compare(medicationStatus, ISVDataProviderResources.Started, StringComparison.CurrentCultureIgnoreCase) == 0 || string.Compare(medicationStatus, ISVDataProviderResources.Suspended, StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                return true;
            }

            return false;
        }
        #endregion

        #region Sorting & Grouping

        /// <summary>
        /// Sorts or Groups the data based on the specified column name.
        /// </summary>
        private void SortOrGroup()
        {
            if (!string.IsNullOrEmpty(this.groupByColumnName))
            {
                this.Group();
            }
            else if (!string.IsNullOrEmpty(this.sortColumn))
            {
                this.Sort();
            }

            for (int rowId = 0; rowId < this.rows.Count; rowId++)
            {
                this.rows[rowId][ISVDataProviderResources.RowId] = rowId.ToString(CultureInfo.CurrentCulture);
            }                        
        }

        /// <summary>
        /// Sorts the data
        /// </summary>
        private void Sort()
        {           
            if (!string.IsNullOrEmpty(this.sortColumn))
            {
                if (this.sortDirection == SortDirection.Ascending)
                {
                    if (IsvDataProvider.IsStringComparer(this.sortColumn))
                    {
                        this.rows = this.rows.OrderBy(row => row[this.sortColumn], new StringComparer()).ToList();
                    }
                    else
                    {
                        this.rows = this.rows.OrderBy(row => row[this.sortColumn], new DateComparer()).ToList();
                    }                    
                }
                else
                {
                    if (IsvDataProvider.IsStringComparer(this.sortColumn))
                    {
                        this.rows = this.rows.OrderByDescending(row => row[this.sortColumn], new StringComparer()).ToList();
                    }
                    else
                    {
                        this.rows = this.rows.OrderByDescending(row => row[this.sortColumn], new DateComparer()).ToList();
                    }                    
                }               
            }
        }

        /// <summary>
        /// Groups the data.
        /// </summary>
        private void Group()
        {
            if (string.IsNullOrEmpty(this.sortColumn))
            {
                // only grouping                
                if (IsvDataProvider.IsStringComparer(this.groupByColumnName))
                {
                    this.rows = this.rows.OrderBy(row => row[this.groupByColumnName], new StringComparer()).ToList();
                }
                else
                {
                    this.rows = this.rows.OrderBy(row => row[this.groupByColumnName], new DateComparer()).ToList();
                }                
            }
            else
            {
                // grouping and sorting.
                if (IsvDataProvider.IsStringComparer(this.groupByColumnName))
                {
                    if (IsvDataProvider.IsStringComparer(this.sortColumn))
                    {
                        if (this.sortDirection == SortDirection.Ascending)
                        {
                            this.rows = this.rows.OrderBy(row => row[this.groupByColumnName], new StringComparer()).ThenBy(row => row[this.sortColumn], new StringComparer()).ToList();
                        }
                        else
                        {
                            this.rows = this.rows.OrderBy(row => row[this.groupByColumnName], new StringComparer()).ThenByDescending(row => row[this.sortColumn], new StringComparer()).ToList();
                        }
                    }
                    else
                    {
                        if (this.sortDirection == SortDirection.Ascending)
                        {
                            this.rows = this.rows.OrderBy(row => row[this.groupByColumnName], new StringComparer()).ThenBy(row => row[this.sortColumn], new DateComparer()).ToList();
                        }
                        else
                        {
                            this.rows = this.rows.OrderBy(row => row[this.groupByColumnName], new StringComparer()).ThenByDescending(row => row[this.sortColumn], new DateComparer()).ToList();
                        }                        
                    }                     
                }
                else
                {
                    if (IsvDataProvider.IsStringComparer(this.sortColumn))
                    {
                        if (this.sortDirection == SortDirection.Ascending)
                        {
                            this.rows = this.rows.OrderBy(row => row[this.groupByColumnName], new DateComparer()).ThenBy(row => row[this.sortColumn], new StringComparer()).ToList();
                        }
                        else
                        {
                            this.rows = this.rows.OrderBy(row => row[this.groupByColumnName], new DateComparer()).ThenByDescending(row => row[this.sortColumn], new StringComparer()).ToList();
                        }                        
                    }
                    else
                    {
                        if (this.sortDirection == SortDirection.Ascending)
                        {
                            this.rows = this.rows.OrderBy(row => row[this.groupByColumnName], new DateComparer()).ThenBy(row => row[this.sortColumn], new DateComparer()).ToList();
                        }
                        else
                        {
                            this.rows = this.rows.OrderBy(row => row[this.groupByColumnName], new DateComparer()).ThenByDescending(row => row[this.sortColumn], new DateComparer()).ToList();
                        }                        
                    }
                }    
            }
        }          
        #endregion

        #region Filtering
        /// <summary>
        /// Specifies whether the row should be filtered or not.
        /// </summary>
        /// <param name="inputRow">Medications row.</param>
        /// <returns>True if the medications row has to be filtered, otherwise false.</returns>
        private bool FilterData(Dictionary<string, string> inputRow)
        {
            bool filter = true;
            DateTime dt = new DateTime();          
                        
            switch (this.filterCondition)
            {
                case FilterCondition.Current:                    
                    if (IsvDataProvider.IsCurrentMedication(inputRow[ISVDataProviderResources.MedicationStatus]))
                    {
                        filter = false;
                    }

                    break;
                case FilterCondition.Past:
                    if (IsvDataProvider.IsCurrentMedication(inputRow[ISVDataProviderResources.MedicationStatus]) == false)
                    {
                        filter = false;
                    }

                    break;
                case FilterCondition.PastTwoMonths:
                    if (IsvDataProvider.IsCurrentMedication(inputRow[ISVDataProviderResources.MedicationStatus]) == false && DateTime.TryParse(inputRow[ISVDataProviderResources.StopDate], CultureInfo.CurrentCulture, DateTimeStyles.None, out dt))
                    {
                        if (dt.AddMonths(2) >= this.CustomDate)
                        {
                            filter = false;
                        }
                        else
                        {
                            filter = true;
                        }
                    }

                    break;
                case FilterCondition.PastSixMonths:
                    if (IsvDataProvider.IsCurrentMedication(inputRow[ISVDataProviderResources.MedicationStatus]) == false && DateTime.TryParse(inputRow[ISVDataProviderResources.StopDate], CultureInfo.CurrentCulture, DateTimeStyles.None, out dt))
                    {
                        if (dt.AddMonths(6) >= this.CustomDate)
                        {
                            filter = false;
                        }
                        else
                        {
                            filter = true;
                        }
                    }

                    break;               
                default:
                    filter = false;
                    break;
            }

            return filter;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Reads the medication data from XML and populates the output data.
        /// </summary>
        /// <param name="dataReader">XML data reader object.</param>
        private void GetPatientMedications(XmlReader dataReader)
        {
            Dictionary<string, string> dataRow = new Dictionary<string, string>();
            dataReader.Read();

            while (dataReader.Read())
            {
                if (dataReader.NodeType == XmlNodeType.Element)
                {
                    string key = dataReader.Name;
                    string value = dataReader.ReadInnerXml();

                    dataRow.Add(key, value);
                }
            }

            if (dataRow.Count > 0)
            {
                bool filter = false;

                filter = this.FilterData(dataRow);                                    

                if (!filter)
                {
                    dataRow[ISVDataProviderResources.RowId] = this.rows.Count.ToString(CultureInfo.CurrentCulture);
                    this.rows.Add(dataRow);                    
                }
            }

            //// REPLICATEINNERDATA compiler definition allows the data to be replicated to create a set with more than 25 rows. 
            //// This way the grid was tested against a large amount of data.
            //// Current grid implementation at 29/04/2008 was not able to support any number greater than 50 as the _REPL_MULTIPLIER_
            //// in a sensible repsonse time.

#if (REPLICATEINNERDATA)
            const int _REPL_MULTIPLIER_ = 37;
            for (int i = 1; i < _REPL_MULTIPLIER_; i++)
            {
                Dictionary<string, string> _dataRow = new Dictionary<string, string>(dataRow);

                _dataRow[ISVDataProviderResources.RowId] = this.rows.Count.ToString(CultureInfo.CurrentCulture);
                _dataRow["MedicationId"] = dataRow["MedicationId"] + (1000 * i);
                this.rows.Add(_dataRow);
            }
#endif
        }        
        #endregion

        #region Comparers
        /// <summary>
        /// Implementation of date comparer.
        /// </summary>
        private class DateComparer : IComparer<string>
        {
            #region IComparer<string> Members

            /// <summary>
            /// Compares two dates.
            /// </summary>
            /// <param name="date1">Date to be compared against.</param>
            /// <param name="date2">Date to be compared with.</param>
            /// <returns>0 if both the dates are equal, 1 if first date is greater than second, -1 if second date is greater than first.</returns>
            public int Compare(string date1, string date2)
            {
                DateTime firstDate;
                DateTime secondDate;
                bool validFirstDate;
                bool validSecondDate;

                validFirstDate = DateTime.TryParse(date1, out firstDate);
                validSecondDate = DateTime.TryParse(date2, out secondDate);

                if (validFirstDate)
                {
                    if (validSecondDate)
                    {
                        // Compare if both dates has value
                        return DateTime.Compare(firstDate, secondDate);
                    }
                    else
                    {
                        // only first date has value
                        return 1;
                    }
                }
                else
                {
                    if (validSecondDate)
                    {
                        // only second date has value
                        return -1;
                    }
                    else
                    {
                        // both the dates are null
                        return 0;
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// Implementation of string comparer.
        /// </summary>
        private class StringComparer : IComparer<string>
        {
            #region IComparer<string> Members

            /// <summary>
            /// Compares two strings.
            /// </summary>
            /// <param name="x">String to be compared against.</param>
            /// <param name="y">String to be compared with.</param>
            /// <returns>0 if both the strings are equal, 1 if first string is greater than second, -1 if second string is greater than first.</returns>
            public int Compare(string x, string y)
            {
                return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
            }

            #endregion
        }
        #endregion        
    }
}
