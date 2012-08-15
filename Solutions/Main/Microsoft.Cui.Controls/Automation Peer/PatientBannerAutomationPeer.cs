//-----------------------------------------------------------------------
// <copyright file="PatientBannerAutomationPeer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>21-Aug-2008</date>
// <summary>Automation peer class for PatientBanner control.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Automation;
    #endregion

    /// <summary>
    /// Automation peer class for PatientBanner control.
    /// </summary>
    public sealed class PatientBannerAutomationPeer : FrameworkElementAutomationPeer, IExpandCollapseProvider
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of patient banner automation peer.
        /// </summary>
        /// <param name="control">Owner control.</param>
        public PatientBannerAutomationPeer(PatientBanner control)
            : base(control)
        {
        }

        #endregion

        #region IExpandCollapseProvider Members

        /// <summary>
        /// Gets the state, expanded or collapsed, of the control. 
        /// </summary>
        ExpandCollapseState IExpandCollapseProvider.ExpandCollapseState
        {
            get
            {
                PatientBanner owner = (PatientBanner)Owner;
                if (owner.ZoneTwoExpanded)
                {
                    return ExpandCollapseState.Expanded;
                }
                else
                {
                    return ExpandCollapseState.Collapsed;
                }
            }
        }       
        
        /// <summary>
        /// Hides all nodes, controls, or content that are descendants of the control. 
        /// </summary>
        void IExpandCollapseProvider.Collapse()
        {
            PatientBanner owner = (PatientBanner)Owner;
            owner.ZoneTwoExpanded = false;
        }

        /// <summary>
        /// Displays all child nodes, controls, or content of the control. 
        /// </summary>
        void IExpandCollapseProvider.Expand()
        {
            PatientBanner owner = (PatientBanner)Owner;
            owner.ZoneTwoExpanded = true;
        }        
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the control pattern for the System.Windows.UIElement that is associated with the System.Windows.Automation.Peers.NameLabelAutomationPeer. 
        /// </summary>
        /// <param name="patternInterface">Specified pattern interface.</param>
        /// <returns>Control pattern.</returns>
        public override object GetPattern(PatternInterface patternInterface)
        {
            if (patternInterface == PatternInterface.ExpandCollapse)
            {
                return this;
            }

            return null;
        }
        #endregion

        #region Protected Methods

        /// <summary>
        /// This method may be called by automation clients to retrieve the control type.
        /// </summary>
        /// <returns>Automation control type.</returns>
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Group;
        }

        /// <summary>
        /// This method may be called by automation clients to retrieve the class name.
        /// </summary>
        /// <returns>Automation class name.</returns>
        protected override string GetClassNameCore()
        {
            return "PatientBanner";
        }

        /// <summary>
        /// This method may be called by automation clients to retrieve the control name.
        /// </summary>
        /// <returns>Automation control name.</returns>
        protected override string GetNameCore()
        {
            return "Patient banner control";
        }

        #endregion
    }
}
