//-----------------------------------------------------------------------
// <copyright file="GraphSection.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>09-Oct-2009</date>
// <summary>Class to hold graphs by section name.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// Class to hold graphs by section name.
    /// </summary>
    public class GraphSection : INotifyPropertyChanged
    {
        /// <summary>
        /// Member variable to hold the graphs.
        /// </summary>
        private ObservableCollection<TimeGraphBase> graphs = new ObservableCollection<TimeGraphBase>();

        /// <summary>
        /// Member variable to hold the section name.
        /// </summary>
        private string sectionName;

        /// <summary>
        /// Member variable to hold the header template.
        /// </summary>
        private DataTemplate headerTemplate;

        /// <summary>
        /// Member variable to hold the number of graphs out of view on top.
        /// </summary>
        private int itemsOutViewTop;

        /// <summary>
        /// Member variable to hold the number of graphs out of view at bottom.
        /// </summary>
        private int itemsOutViewBottom;

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion        
       
        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        /// <value>The name of the section.</value>
        public string SectionName
        {
            get
            {
                return this.sectionName;
            }

            set
            {
                if (this.sectionName != value)
                {
                    this.sectionName = value;
                    this.RaisePropertyChangedEvent("SectionName");
                }
            }
        }

        /// <summary>
        /// Gets the graphs.
        /// </summary>
        /// <value>The graphs.</value>
        public ObservableCollection<TimeGraphBase> Graphs
        {
            get
            {
                return this.graphs;
            }
        }

        /// <summary>
        /// Gets or sets the header template.
        /// </summary>
        /// <value>The header template.</value>
        public DataTemplate HeaderTemplate
        {
            get
            {
                return this.headerTemplate;
            }

            set
            {
                if (this.headerTemplate != value)
                {
                    this.headerTemplate = value;
                    this.RaisePropertyChangedEvent("HeaderTemplate");
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="GraphSection"/> is initialized.
        /// </summary>
        /// <value>If initialized <c>true</c>; otherwise, <c>false</c>.</value>
        public bool Initialized
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the number of graphs out of view on top.
        /// </summary>
        /// <value>Number of graphs out of view on top.</value>
        public int ItemsOutViewTop
        {
            get
            {
                return this.itemsOutViewTop;
            }

            set
            {
                if (this.itemsOutViewTop != value)
                {
                    this.itemsOutViewTop = value;
                    this.RaisePropertyChangedEvent("ItemsOutViewTop");
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of graphs out of view at bottom.
        /// </summary>
        /// <value>Number of graphs out of view at bottom.</value>
        public int ItemsOutViewBottom
        {
            get
            {
                return this.itemsOutViewBottom;
            }

            set
            {
                if (this.itemsOutViewBottom != value)
                {
                    this.itemsOutViewBottom = value;
                    this.RaisePropertyChangedEvent("ItemsOutViewBottom");
                }
            }
        }

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void RaisePropertyChangedEvent(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }        
    }
}
