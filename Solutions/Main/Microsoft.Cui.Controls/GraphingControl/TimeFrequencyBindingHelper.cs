//-----------------------------------------------------------------------
// <copyright file="TimeFrequencyBindingHelper.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>05-Sep-2008</date>
// <summary>Helper class to bind time frequency.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.ComponentModel;
    #endregion

    /// <summary>
    /// This is used to bind ui elements.
    /// </summary>
    public class TimeFrequencyBindingHelper : INotifyPropertyChanged
    {
        /// <summary>
        /// The TimeFrequency.
        /// </summary>
        private TimeFrequency timeFrequency;

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Gets or sets Access to the TimeFrequency.
        /// </summary>
        /// <value>The value.</value>
        public TimeFrequency TimeFrequency
        {
            get
            {
                return this.timeFrequency;
            }

            set
            {
                this.timeFrequency = value;
                if (null != this.PropertyChanged)
                {
                    PropertyChangedEventHandler handler;
                    handler = this.PropertyChanged;
                    handler.Invoke(this, new PropertyChangedEventArgs("TimeFrequency"));
                }
            }
        }

        /// <summary>
        /// Gets or sets Access to the TimeFrequency.
        /// </summary>
        /// <value>The value.</value>
        public object ViaDataContext
        {
            get
            {
                return this.timeFrequency;
            }

            set
            {
                FrameworkElement element = value as FrameworkElement;
                if (null != element)
                {
                    if (element.DataContext is TimeFrequency)
                    {
                        this.timeFrequency = (TimeFrequency)element.DataContext;
                        if (null != this.PropertyChanged)
                        {
                            PropertyChangedEventHandler handler;
                            handler = this.PropertyChanged;
                            handler.Invoke(this, new PropertyChangedEventArgs("TimeFrequency"));
                        }
                    }
                }
            }
        }
    }
}
