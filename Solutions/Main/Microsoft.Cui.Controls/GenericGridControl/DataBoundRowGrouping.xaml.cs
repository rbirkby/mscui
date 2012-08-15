//-----------------------------------------------------------------------
// <copyright file="DataBoundRowGrouping.xaml.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>20-Feb-2008</date>
// <summary>The DataBoundRowGrouping class.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Windows.Controls;
    using System.Text.RegularExpressions;
    using System.Globalization;

    /// <summary>
    /// Implementation of the DataBoundRowGrouping class.
    /// </summary>
    public partial class DataBoundRowGrouping : UserControl
    {        
        /// <summary>
        /// Null grouping text member variable.
        /// </summary>
        private string nullGroupingText = "None";

        /// <summary>
        /// The groupingKey private member variable.
        /// </summary>
        private string groupingKey;

        /// <summary>
        /// The dataSource private member variable.
        /// </summary>
        private object dataSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBoundRowGrouping"/> class.
        /// </summary>
        public DataBoundRowGrouping()
        {
            // Required to initialize variables
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the data source.
        /// </summary>
        /// <value>The data source.</value>
        public object DataSource
        {
            get
            {
                return this.dataSource;
            }

            set
            {
                this.dataSource = value;
                if (null != this.BindingElement)
                {
                    DictionaryToStringConverter converter = this.Resources["DictionaryToStringConverter"] as DictionaryToStringConverter;
                    System.Windows.Data.Binding binding = new System.Windows.Data.Binding(string.Empty);
                    binding.Converter = converter;
                    binding.ConverterParameter = this.groupingKey;

                    binding.Source = this.dataSource;
                    binding.ConverterCulture = System.Globalization.CultureInfo.CurrentCulture;

                    this.BindingElement.SetBinding(System.Windows.Controls.TextBlock.TextProperty, binding);
                }
            }
        }

        /// <summary>
        /// Gets or sets the grouping key.
        /// </summary>
        /// <value>The grouping key.</value>
        public string GroupingKey
        {
            get
            {
                return this.BindingElement.Text;
            }

            set
            {
                this.groupingKey = value;
            }
        }

        /// <summary>
        /// Gets the grouping text.
        /// </summary>
        /// <value>The grouping text.</value>
        public string GroupingText
        {
            get
            {
                if (!string.IsNullOrEmpty(this.BindingElement.Text))
                {
                    string groupingTextTitle = Regex.Replace(
                        this.BindingElement.Text, 
                        @"\b(\w)", 
                    delegate(Match match)
                    {
                        return match.Groups[1].Value.ToUpper(CultureInfo.CurrentCulture);
                    });

                    return groupingTextTitle;
                }
                else
                {
                    return this.nullGroupingText;
                }
            }            
        }

        /// <summary>
        /// Gets or sets the text to be shown when Group header contains no value.
        /// </summary>
        /// <value>Null grouping text.</value>
        public string NullGroupingText
        {
            get
            {
                return this.nullGroupingText;
            }

            set
            {
                this.nullGroupingText = value;
            }
        }
    }
}
