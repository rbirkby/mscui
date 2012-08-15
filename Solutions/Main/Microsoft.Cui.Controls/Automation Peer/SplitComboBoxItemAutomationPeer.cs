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
    public sealed class SplitComboBoxItemAutomationPeer : ListBoxItemAutomationPeer, ISelectionItemProvider
    {
        /// <summary>
        /// The split list box item instance.
        /// </summary>
        private SplitComboBoxItem splitComboBoxItem;

        /// <summary>
        /// The parent selector automation peer.
        /// </summary>
        private SelectorAutomationPeer parent;

        /// <summary>
        /// SplitListBoxItemAutomationPeer constructor.
        /// </summary>
        /// <param name="splitComboBoxItem">The split combo box item instance.</param>
        /// <param name="parent">The parent selector peer.</param>
        public SplitComboBoxItemAutomationPeer(SplitComboBoxItem splitComboBoxItem, SelectorAutomationPeer parent)
            : base(splitComboBoxItem, parent)
        {
            this.splitComboBoxItem = splitComboBoxItem;
            this.parent = parent;
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
        /// Selects the item.
        /// </summary>
        void System.Windows.Automation.Provider.ISelectionItemProvider.Select()
        {
            this.splitComboBoxItem.SelectItem();
        }

        #endregion

        /// <summary>
        /// Gets the bounding rectangle.
        /// </summary>
        /// <returns>The bounding rectangle.</returns>
        protected override Rect GetBoundingRectangleCore()
        {
            if (this.parent != null)
            {
                return new Rect(
                    this.parent.GetBoundingRectangle().X + this.splitComboBoxItem.GetBoundingRectRelativeToParent().X,
                    this.parent.GetBoundingRectangle().Y + this.splitComboBoxItem.GetBoundingRectRelativeToParent().Y,
                    this.splitComboBoxItem.GetBoundingRectRelativeToParent().Width,
                    this.splitComboBoxItem.GetBoundingRectRelativeToParent().Height);
            }
            else
            {
                return base.GetBoundingRectangleCore();
            }
        }

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
