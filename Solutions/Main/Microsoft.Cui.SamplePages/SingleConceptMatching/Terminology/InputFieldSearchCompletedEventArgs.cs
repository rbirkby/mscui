//-----------------------------------------------------------------------
// <copyright file="InputFieldSearchCompletedEventArgs.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>09/12/2008</date>
// <summary>Event args that store the results for the inputfield.</summary>
//-----------------------------------------------------------------------
#if SILVERLIGHT
namespace Microsoft.Cui.SamplePages
#else
namespace Microsoft.Cui.SampleWinform
#endif
{
    #region Using

    using System;
    using System.Collections.ObjectModel;
#if !SILVERLIGHT
    using Microsoft.Cui.SampleWinform.TerminologyProvider;    
#endif

    #endregion
    /// <summary>
    /// Event args that store the results for the inputfield.
    /// </summary>
    public class InputFieldSearchCompletedEventArgs : BaseTerminologyEventArgs
    {
        /// <summary>
        /// Backing field for original search text property.
        /// </summary>
        private string searchTextOriginal;

        /// <summary>
        /// Backing field for additional Text Box Results property.
        /// </summary>
        private ObservableCollection<InputFieldResult> inputFieldResults;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputFieldSearchCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="searchTextOriginal">The original search text.</param>
        /// <param name="inputFieldResults">The input field results.</param>
        /// <param name="maxTerms">Maximum number of results returned.</param>
        /// <param name="exceedsMaxTotal">Boolean indicating whether more results are present.</param>
        public InputFieldSearchCompletedEventArgs(string searchTextOriginal, ObservableCollection<InputFieldResult> inputFieldResults, int maxTerms, bool exceedsMaxTotal)
        {
            this.searchTextOriginal = searchTextOriginal;
            this.inputFieldResults = inputFieldResults;
            this.MaximumNumberOfTerms = maxTerms;
            this.ExceedsMaxTotal = exceedsMaxTotal;
        }        

        /// <summary>
        /// Initializes a new instance of the <see cref="InputFieldSearchCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="searchTextOriginal">The original search text.</param>
        /// <param name="successful">If set to <c>true</c> [successful].</param>
        public InputFieldSearchCompletedEventArgs(string searchTextOriginal, bool successful)
            : base(successful)
        {
            this.searchTextOriginal = searchTextOriginal;
        }

        /// <summary>
        /// Gets the original search text.
        /// </summary>
        /// <value>The original search text.</value>
        public string SearchTextOriginal
        {
            get
            {
                return this.searchTextOriginal;
            }
        }

        /// <summary>
        /// Gets the input field results.
        /// </summary>
        /// <value>The input field results.</value>
        public ObservableCollection<InputFieldResult> InputFieldResults
        {
            get
            {
                return this.inputFieldResults;
            }
        }

        /// <summary>
        /// Gets the maximum number of terms returned.
        /// </summary>
        /// <value>Maximum number of terms returned.</value>
        public int MaximumNumberOfTerms
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether more results are present.
        /// </summary>
        /// <value>Value indicating whether more results are present.</value>
        public bool ExceedsMaxTotal
        {
            get;
            private set;
        }
    }
}
