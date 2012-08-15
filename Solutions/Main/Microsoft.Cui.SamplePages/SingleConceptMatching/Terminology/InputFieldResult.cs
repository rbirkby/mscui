//-----------------------------------------------------------------------
// <copyright file="InputFieldResult.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Contains the data required to hightlight terms in the input field textbox.</summary>
//-----------------------------------------------------------------------
#if SILVERLIGHT
namespace Microsoft.Cui.SamplePages
#else
namespace Microsoft.Cui.SampleWinform
#endif
{
    #region Using

#if SILVERLIGHT
    using Microsoft.Cui.SamplePages.TerminologyProvider;
#else
    using Microsoft.Cui.SampleWinform.TerminologyProvider;
#endif

    #endregion
    /// <summary>
    /// Contains the data required to hightlight terms in the input field textbox.
    /// </summary>
    public class InputFieldResult : TermResult
    {
        /// <summary>
        /// Gets or sets the prefix text.
        /// </summary>
        /// <value>The prefix text.</value>
        public string PrefixText { get; set; }

        /// <summary>
        /// Gets or sets the postfix text.
        /// </summary>
        /// <value>The postfix text.</value>
        public string PostfixText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is negated.
        /// </summary>
        /// <value>
        /// Is <c>true</c> if this instance is negated; otherwise, <c>false</c>.
        /// </value>
        public bool IsNegated { get; set; }
    }
}
