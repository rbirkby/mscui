//-----------------------------------------------------------------------
// <copyright file="CascadingListBoxAutomationPeer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010.
// All rights reserved
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
// <date>27-Oct-2009</date>
// <summary>
//      Automation peer for a cascading list box.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Automation;

    /// <summary>
    /// Automation peer for a cascading list box.
    /// </summary>
    public sealed class CascadingListBoxAutomationPeer : ListBoxAutomationPeer, IExpandCollapseProvider
    {
        /// <summary>
        /// The cascading list box instance.
        /// </summary>
        private CascadingListBox cascadingListBox;

        /// <summary>
        /// CascadingListBoxAutomationPeer constructor.
        /// </summary>
        /// <param name="cascadingListBox">The cascading list box instance.</param>        
        public CascadingListBoxAutomationPeer(CascadingListBox cascadingListBox)
            : base(cascadingListBox)
        {
            this.cascadingListBox = cascadingListBox;            
        }

        #region IExpandCollapseProvider Members
        /// <summary>
        /// Gets the current expanded state.
        /// </summary>
        /// <value>The current expanded state.</value>
        public ExpandCollapseState ExpandCollapseState
        {
            get 
            {
                return this.cascadingListBox.IsExpanded ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed;
            }
        }

        /// <summary>
        /// Collapses the cascading list box.
        /// </summary>
        public void Collapse()
        {
            this.cascadingListBox.IsExpanded = false;
        }

        /// <summary>
        /// Expands the cascading list box.
        /// </summary>
        public void Expand()
        {
            this.cascadingListBox.IsExpanded = true;
        }
        #endregion

        /// <summary>
        /// Gets the pattern for a specified pattern interface.
        /// </summary>
        /// <param name="patternInterface">The pattern interface.</param>
        /// <returns>The pattern peer.</returns>
        public override object GetPattern(PatternInterface patternInterface)
        {
            if (patternInterface == PatternInterface.ExpandCollapse)
            {
                return this;
            }

            return base.GetPattern(patternInterface);
        }
    }
}
