//-----------------------------------------------------------------------
// <copyright file="SplitListBoxItemAutomationPeer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      Automation peer for a split list box item.
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
    using System.Windows;
    using System.Windows.Automation;

    /// <summary>
    /// Automation peer for a split list box item.
    /// </summary>
    public sealed class SplitListBoxItemAutomationPeer : FrameworkElementAutomationPeer, ISelectionItemProvider, IScrollItemProvider
    {
        /// <summary>
        /// The split list box item instance.
        /// </summary>
        private SplitListBoxItem splitListBoxItem;

        /// <summary>
        /// SplitListBoxItemAutomationPeer constructor.
        /// </summary>
        /// <param name="splitListBoxItem">The split list box item instance.</param>
        public SplitListBoxItemAutomationPeer(SplitListBoxItem splitListBoxItem)
            : base(splitListBoxItem)
        {
            this.splitListBoxItem = splitListBoxItem;
        }

        #region ISelectionItemProvider Members
        /// <summary>
        /// Gets a value indicating whether the item is selected.
        /// </summary>
        bool System.Windows.Automation.Provider.ISelectionItemProvider.IsSelected
        {
            get { return this.splitListBoxItem.IsSelectedValue; }
        }

        /// <summary>
        /// Gets the selection container.
        /// </summary>
        public IRawElementProviderSimple SelectionContainer
        {
            get
            {
                SplitListBox parent = this.splitListBoxItem.ParentSplitListBox;
                if (parent != null)
                {
                    AutomationPeer peer = FrameworkElementAutomationPeer.FromElement(parent);
                    if (peer != null)
                    {
                        return ProviderFromPeer(peer);
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Selects the item.
        /// </summary>
        void System.Windows.Automation.Provider.ISelectionItemProvider.Select()
        {
            this.splitListBoxItem.SelectItem();
        }

        /// <summary>
        /// Gets the automation peer for the specified pattern interface.
        /// </summary>
        /// <param name="patternInterface">The pattern interface.</param>
        /// <returns>The automation peer.</returns>
        public override object GetPattern(PatternInterface patternInterface)
        {
            if (patternInterface == PatternInterface.SelectionItem ||
                patternInterface == PatternInterface.ScrollItem)
            {
                return this;
            }

            return null;
        }
        #endregion

        #region ISelectionItemProvider Members
        /// <summary>
        /// Adds the item to the selection.
        /// </summary>
        public void AddToSelection()
        {
            if (this.splitListBoxItem != null && this.splitListBoxItem.ParentSplitListBox != null)
            {
                this.splitListBoxItem.ParentSplitListBox.ConfirmedSelectedItem = this.splitListBoxItem.Content != null ? this.splitListBoxItem.Content : this.splitListBoxItem;
            }
        }

        /// <summary>
        /// Removes the item from the selection.
        /// </summary>
        public void RemoveFromSelection()
        {
            if (this.splitListBoxItem != null && this.splitListBoxItem.ParentSplitListBox != null)
            {
                this.splitListBoxItem.ParentSplitListBox.ConfirmedSelectedItem = null;
            }
        }
        #endregion

        #region IScrollItemProvider Members

        /// <summary>
        /// Scrolls the item into view.
        /// </summary>
        public void ScrollIntoView()
        {
            if (this.splitListBoxItem.ParentSplitListBox != null)
            {
                this.splitListBoxItem.ParentSplitListBox.ScrollIntoView(this.splitListBoxItem.Content != null ? this.splitListBoxItem.Content : this.splitListBoxItem);
            }
        }

        #endregion

        /// <summary>
        /// Gets the automation name.
        /// </summary>
        /// <returns>The automation name.</returns>
        protected override string GetNameCore()
        {
            if (this.splitListBoxItem.Content != null)
            {
                UIElement contentElement = this.splitListBoxItem.Content as UIElement;
                if (contentElement != null)
                {
                    return AutomationProperties.GetName(contentElement);
                }
                else
                {
                    return this.splitListBoxItem.Content.ToString();
                }
            }

            return base.GetNameCore();
        }
    }
}
