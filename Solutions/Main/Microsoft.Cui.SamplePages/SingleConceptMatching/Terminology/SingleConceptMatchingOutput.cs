//-----------------------------------------------------------------------
// <copyright file="SingleConceptMatchingOutput.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>22-Jan-2008</date>
// <summary>An representation of the SCM output.</summary>
//-----------------------------------------------------------------------
#if SILVERLIGHT
namespace Microsoft.Cui.SamplePages
#else
namespace Microsoft.Cui.SampleWinform
#endif
{
    #region Using
    using System;
    using System.Windows;
    #endregion

    /// <summary>
    /// An representation of an encoded concept.
    /// </summary>
    public class SingleConceptMatchingOutput
    {
        /// <summary>
        /// Gets or sets the encoded concept.
        /// </summary>
        /// <value>The encoded concept.</value>
        public EncodedConcept EncodedConcept { get; set; }

        /// <summary>
        /// Gets or sets the original input field text.
        /// </summary>
        /// <value>The original input field text.</value>
        public string OriginalInputFieldText { get; set; }

        /// <summary>
        /// Gets or sets the original additional text.
        /// </summary>
        /// <value>The original additional text.</value>
        public string OriginalAdditionalText { get; set; }

        /// <summary>
        /// Gets a value indicating whether the original text has been entered.
        /// </summary>
        /// <value>Is <c>true</c> if [have original text]; otherwise, <c>false</c>.</value>
        public bool HaveOriginalText
        {
            get
            {
                return (string.IsNullOrEmpty(this.OriginalInputFieldText) && string.IsNullOrEmpty(this.OriginalAdditionalText)) ? false : true;
            }
        }
    }
}
