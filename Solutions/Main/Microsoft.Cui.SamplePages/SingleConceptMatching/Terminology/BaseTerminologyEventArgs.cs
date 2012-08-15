//-----------------------------------------------------------------------
// <copyright file="BaseTerminologyEventArgs.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>05-Feb-2009</date>
// <summary>BaseTerminologyEventArgs.</summary>
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
#else

#endif

    #endregion
    /// <summary>
    /// Base class for terminology provider events.  Indicates if the service call was successfull or not.
    /// </summary>
    public class BaseTerminologyEventArgs : EventArgs
    {
        /// <summary>
        /// Backing field for  property.
        /// </summary>
        private bool successful;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTerminologyEventArgs"/> class.
        /// </summary>
        /// <param name="successful">If set to <c>true</c> [successful].</param>
        public BaseTerminologyEventArgs(bool successful)
        {
            this.successful = successful;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTerminologyEventArgs"/> class.
        /// </summary>
        public BaseTerminologyEventArgs()
        {
            this.successful = true;
        }
        
        /// <summary>
        /// Gets a value indicating whether this <see cref="BaseTerminologyEventArgs"/> is successfull.
        /// </summary>
        /// <value>Is <c>true</c> If successful; otherwise, <c>false</c>.</value>
        public bool Successful
        {
            get
            {
                return this.successful;
            }            
        }        
    }
}
