//-----------------------------------------------------------------------
// <copyright file="LabelAutomationPeer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>19-Aug-2008</date>
// <summary>The base peer class for label controls.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    #region Using
    using System;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    #endregion

    /// <summary>
    /// Base automation peer class for CUI label controls.
    /// </summary>
    public class LabelAutomationPeer : FrameworkElementAutomationPeer, IValueProvider
    {
        #region Constructor
        /// <summary>
        /// Creates a new instance of label automation peer.
        /// </summary>
        /// <param name="owner">Owner control.</param>
        public LabelAutomationPeer(FrameworkElement owner)
            : base(owner)
        {
        }
        #endregion

        #region IValueProvider Properties

        /// <summary>
        /// Gets a value indicating whether the control is editable or not.
        /// </summary>
        /// <value>Is Read only.</value>
        bool IValueProvider.IsReadOnly
        {
            get { return LabelAutomationPeer.IsReadOnly; }
        }

        /// <summary>
        /// Gets the display value of automation peer.
        /// </summary>
        /// <value>Display value of control.</value>
        string IValueProvider.Value
        {
            get
            {
                return this.Value;
            }
        }
        #endregion

        #region Protected properties

        /// <summary>
        /// Gets a value indicating whether the control is editable or not.
        /// </summary>
        /// <value>Is Read only.</value>
        protected static bool IsReadOnly
        {
            get { return true; }
        }

        /// <summary>
        /// Gets the display value of automation peer.
        /// </summary>
        /// <value>Display value of control.</value>
        protected virtual string Value
        {
            get
            {
                BaseLabel owner = Owner as BaseLabel;
                if (owner != null)
                {
                    return owner.DisplayValue;
                }

                return string.Empty;
            }
        }
     
        #endregion       

        #region IValueProvider Methods

        /// <summary>
        /// Sets specified value to the owner automation element.
        /// </summary>
        /// <param name="value">Value of owner element.</param>
        void IValueProvider.SetValue(string value)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods      
        /// <summary>
        /// Gets the control pattern for the System.Windows.UIElement that is associated with the LabelAutomationPeer. 
        /// </summary>
        /// <param name="patternInterface">Specified pattern interface.</param>
        /// <returns>Control pattern.</returns>
        public override object GetPattern(PatternInterface patternInterface)
        {
            if (patternInterface == PatternInterface.Value)
            {
                return this;
            }

            return null;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// Raises the value changed event.
        /// </summary>
        /// <param name="oldValue">Old Value.</param>
        /// <param name="newValue">New Value.</param>
        internal void RaiseValueChangedEvent(string oldValue, string newValue)
        {
            RaisePropertyChangedEvent(System.Windows.Automation.ValuePatternIdentifiers.ValueProperty, oldValue, newValue);
        }
        #endregion      

        #region Protected Methods
        /// <summary>
        /// This method may be called by automation clients to retrieve the control type.
        /// </summary>
        /// <returns>Automation control type.</returns>
        protected override AutomationControlType GetAutomationControlTypeCore()
        {
            return AutomationControlType.Text;
        }       
        #endregion                             
    }
}
