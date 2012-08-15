//-----------------------------------------------------------------------
// <copyright file="EncodeConceptCompletedEventArgs.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>24-Jan-2009</date>
// <summary>Contains the data required to encode a term.</summary>
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

    #endregion
    /// <summary>
    /// Contains the data required to encode a term.
    /// </summary>
    public class EncodeConceptCompletedEventArgs : BaseTerminologyEventArgs
    {
        /// <summary>
        /// Backing field for EncodedConcept property.
        /// </summary>
        private EncodedConcept encodedConcept;

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodeConceptCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="encodedConcept">The encoded concept.</param>
        public EncodeConceptCompletedEventArgs(EncodedConcept encodedConcept)
        {
            this.encodedConcept = encodedConcept;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodeConceptCompletedEventArgs"/> class.
        /// </summary>
        /// <param name="successful">If set to <c>true</c> [successful].</param>
        public EncodeConceptCompletedEventArgs(bool successful)
            : base(successful)
        {
        }

        /// <summary>
        /// Gets the encoded concept.
        /// </summary>
        /// <value>The encoded concept.</value>
        public EncodedConcept EncodedConcept 
        {
            get
            {
                return this.encodedConcept;
            }            
        }        
    }
}
