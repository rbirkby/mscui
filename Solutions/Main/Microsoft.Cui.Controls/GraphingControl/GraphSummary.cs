//-----------------------------------------------------------------------
// <copyright file="GraphSummary.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>24-Oct-2008</date>
// <summary>Class used for holding summary of the graph.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.ComponentModel;
    using System.Windows.Controls;

    /// <summary>
    /// Class used for holding summary of the graph.
    /// </summary>
    public class GraphSummary : INotifyPropertyChanged
    {
        #region Private Members
        /// <summary>
        /// Member variable to hold the visibility of a graph.
        /// </summary>
        private bool show = true;

        /// <summary>
        /// Member variable to hold visual focus line data point.
        /// </summary>
        private object visualFocusLineSelectedObject;

        /// <summary>
        /// Member variable to hold graph title.
        /// </summary>
        private string title;

        /// <summary>
        /// Member variable to hold graph units.
        /// </summary>
        private string units;

        /// <summary>
        /// Member variable to hold graph units description.
        /// </summary>
        private string unitsDescription;

        /// <summary>
        /// Member variable to hold graph graph data marker symbol template.
        /// </summary>
        private DataTemplate dataMarker;

        /// <summary>
        /// Member variable to hold graph normal range minimum value.
        /// </summary>
        private double normalRangeMinimumValue;

        /// <summary>
        /// Member variable to hold graph normal range maximum value.
        /// </summary>
        private double normalRangeMaximumValue;

        /// <summary>
        /// Member variable to indicate whether to show normal range.
        /// </summary>
        private Visibility showNormalRange;

        /// <summary>
        /// Member variable to hold graph background.
        /// </summary>
        private Brush background;

        /// <summary>
        /// Member variable to hold graph background on hover.
        /// </summary>
        private Brush hoverBackground;

        /// <summary>
        /// Member variable to hold visual focus line visibility.
        /// </summary>
        private Visibility visualFocusLineVisibility = Visibility.Collapsed;
        #endregion

        #region Events
        /// <summary>
        /// Raised when the visibility of a graph is changed.
        /// </summary>
        public event EventHandler<EventArgs> DataSelectorGraphVisibilityChanged;
                
        /// <summary>
        /// Occurs when the value of a property changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the name of the graph.
        /// </summary>
        /// <value>Name of the graph.</value>
        public string Name
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the title for the graph.
        /// </summary>
        /// <value>Title for the graph.</value>
        public string Title
        {
            get 
            { 
                return this.title; 
            }

            internal set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }

        /// <summary>
        /// Gets the units for the graph.
        /// </summary>
        /// <value>Units for the graph.</value>
        public string Units
        {
            get 
            { 
                return this.units; 
            }

            internal set
            {
                if (this.units != value)
                {
                    this.units = value;
                    this.RaisePropertyChanged("Units");
                }
            }
        }

        /// <summary>
        /// Gets the data marker template for the graph.
        /// </summary>
        /// <value>Data marker template for the graph.</value>
        public DataTemplate DataMarker
        {
            get 
            {
                return this.dataMarker; 
            }

            internal set
            {
                if (this.dataMarker != value)
                {
                    this.dataMarker = value;
                    this.RaisePropertyChanged("DataMarker");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the graph is currently visible.
        /// </summary>
        /// <value>Value indicating whether the graph is currently visible.</value>
        public bool Show
        {
            get
            {
                return this.show;
            }

            set
            {
                if (this.show != value)
                {
                    this.show = value;
                    if (this.DataSelectorGraphVisibilityChanged != null)
                    {                        
                        this.DataSelectorGraphVisibilityChanged(this, new EventArgs());
                        this.RaisePropertyChanged("Show");
                    }
                }
            }
        }

        /// <summary>
        /// Gets the background brush used for the graph.
        /// </summary>
        /// <value>Background brush used for the graph.</value>
        public Brush Background
        {
            get
            {
                return this.background; 
            }

            internal set
            {
                if (this.background != value)
                {
                    this.background = value;
                    this.RaisePropertyChanged("Background");
                }
            }
        }

        /// <summary>
        /// Gets the brush to be used for background on hover.
        /// </summary>
        /// <value>Background brush to be used on hover.</value>        
        public Brush HoverBackground
        {
            get
            {
                return this.hoverBackground;
            }

            internal set
            {
                if (this.hoverBackground != value)
                {
                    this.hoverBackground = value;
                    this.RaisePropertyChanged("HoverBackground");
                }
            }
        }

        /// <summary>
        /// Gets the minimum value for normal range.
        /// </summary>
        /// <value>Minimum value for normal range.</value>
        public double NormalRangeMinimumValue
        {
            get 
            {
                return this.normalRangeMinimumValue; 
            }

            internal set
            {
                if (this.normalRangeMinimumValue != value)
                {
                    this.normalRangeMinimumValue = value;
                    this.RaisePropertyChanged("NormalRangeMinimumValue");
                }
            }
        }

        /// <summary>
        /// Gets the maximum value for the normal range.
        /// </summary>
        /// <value>Maximum value for the normal range.</value>
        public double NormalRangeMaximumValue
        {
            get
            {
                return this.normalRangeMaximumValue; 
            }

            internal set
            {
                if (this.normalRangeMaximumValue != value)
                {
                    this.normalRangeMaximumValue = value;
                    this.RaisePropertyChanged("NormalRangeMaximumValue");
                }
            }
        }

        /// <summary>
        /// Gets the visiblity of the normal range.
        /// </summary>
        /// <value>Visiblity of the normal range.</value>
        public Visibility ShowNormalRange
        {
            get
            {
                return this.showNormalRange; 
            }

            internal set
            {
                if (this.showNormalRange != value)
                {
                    this.showNormalRange = value;
                    this.RaisePropertyChanged("ShowNormalRange");
                }
            }
        }

        /// <summary>
        /// Gets the description for the units.
        /// </summary>
        /// <value>Description for the units.</value>
        public string UnitsDescription
        {
            get 
            {
                return this.unitsDescription; 
            }

            internal set
            {
                if (this.unitsDescription != value)
                {
                    this.unitsDescription = value;
                    this.RaisePropertyChanged("UnitsDescription");
                }
            }
        }

        /// <summary>
        /// Gets the data for the point that visual focus line has selected.
        /// </summary>
        /// <value>Data for the point that visual focus line has selected.</value>
        public object VisualFocusLineSelectedObject
        {
            get
            {
                return this.visualFocusLineSelectedObject;
            }

            internal set
            {
                if (this.visualFocusLineSelectedObject != value)
                {
                    this.visualFocusLineSelectedObject = value;         
                    this.RaisePropertyChanged("VisualFocusLineSelectedObject");
                }
            }
        }

        /// <summary>
        /// Gets the visibility of the visual focus line.
        /// </summary>
        /// <value>Visibility of the visual focus line.</value>
        public Visibility VisualFocusLineVisibility
        {
            get
            {
                return this.visualFocusLineVisibility;
            }

            internal set
            {
                if (this.visualFocusLineVisibility != value)
                {
                    this.visualFocusLineVisibility = value;
                    this.RaisePropertyChanged("VisualFocusLineVisibility");
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property whose value got changed.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
