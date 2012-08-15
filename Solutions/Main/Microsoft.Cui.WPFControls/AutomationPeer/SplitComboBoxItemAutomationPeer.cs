//-----------------------------------------------------------------------
// <copyright file="SplitComboBoxItemAutomationPeer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
//      Automation peer for a split combo box item.
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
    /// Automation peer for a split combo box item.
    /// </summary>
    public sealed class SplitComboBoxItemAutomationPeer : FrameworkElementAutomationPeer, ISelectionItemProvider
    {
        /// <summary>
        /// The split combo box item instance.
        /// </summary>
        private SplitComboBoxItem splitComboBoxItem;

        /// <summary>
        /// SplitComboBoxItemAutomationPeer constructor.
        /// </summary>
        /// <param name="splitComboBoxItem">The split combo box item instance.</param>
        public SplitComboBoxItemAutomationPeer(SplitComboBoxItem splitComboBoxItem)
            : base(splitComboBoxItem)
        {
            this.splitComboBoxItem = splitComboBoxItem;
        }

        #region ISelectionItemProvider Members
        /// <summary>
        /// Gets a value indicating whether the item is selected.
        /// </summary>
        bool System.Windows.Automation.Provider.ISelectionItemProvider.IsSelected
        {
            get { return this.splitComboBoxItem.IsSelectedValue; }
        }

        /// <summary>
        /// Gets the selection container.
        /// </summary>
        public IRawElementProviderSimple SelectionContainer
        {
            get
            {
                SplitComboBox parent = this.splitComboBoxItem.ParentSplitComboBox;
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
            this.splitComboBoxItem.SelectItem();
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
            if (this.splitComboBoxItem != null && this.splitComboBoxItem.ParentSplitComboBox != null)
            {
                this.splitComboBoxItem.ParentSplitComboBox.ConfirmedSelectedItem = this.splitComboBoxItem.Content != null ? this.splitComboBoxItem.Content : this.splitComboBoxItem;
            }
        }

        /// <summary>
        /// Removes the item from the selection.
        /// </summary>
        public void RemoveFromSelection()
        {
            if (this.splitComboBoxItem != null && this.splitComboBoxItem.ParentSplitComboBox != null)
            {
                this.splitComboBoxItem.ParentSplitComboBox.ConfirmedSelectedItem = null;
            }
        }
        #endregion

        /// <summary>
        /// Gets the automation name.
        /// </summary>
        /// <returns>The automation name.</returns>
        protected override string GetNameCore()
        {
            if (this.splitComboBoxItem.Content != null)
            {
                UIElement contentElement = this.splitComboBoxItem.Content as UIElement;
                if (contentElement != null)
                {
                    return AutomationProperties.GetName(contentElement);
                }
                else
                {
                    return this.splitComboBoxItem.Content.ToString();
                }
            }

            return base.GetNameCore();
        }
    }
}
