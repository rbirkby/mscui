//-----------------------------------------------------------------------
// <copyright file="IdentifierLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>03-Jan-2007</date>
// <summary>The control used to display a unique identifier. </summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.WinForms
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// The control used to display a unique identifier. 
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "IdentifierLabel.bmp")]
    public partial class IdentifierLabel : Label, INotifyPropertyChanged, ISupportInitialize
    {
        #region Member Vars

        /// <summary>
        /// IdentifierType for the Label
        /// </summary>
        private IdentifierType identifierType = IdentifierType.Other;

        /// <summary>
        /// nhsNumber mem var
        /// </summary>
        private NhsNumber nhsNumber;

        /// <summary>
        /// flag to indicate we are being initialized
        /// </summary>
        private bool initializing;
        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an IdentifierLabel object. 
        /// </summary>
        public IdentifierLabel()
            : base()
        {
            this.InitializeComponent();
            this.AccessibleName = IdentifierLabelControl.Resources.AccessibleName;
            this.AccessibleDescription = IdentifierLabelControl.Resources.AccessibleDescription;
            this.AccessibleRole = AccessibleRole.StaticText;
            this.SetInternalState(IdentifierType.Other, IdentifierLabelControl.Resources.IdentifierDefaultDesignTimeValue);
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes. 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets whether to process the identifier with the NhsNumber validation checksum. 
        /// </summary>
        /// <remarks>
        /// Defaults to "Other". If this is set to "Other", 
        /// no validation is performed. If this is set to "NhsNumber", the text must be a valid NHS number.
        /// </remarks>
        [Bindable(true), Category("Behavior"), DefaultValue(IdentifierType.Other)]
        [Description("The type of the identifier. If set to NhsNumber then the Text property must be set to a valid NhsNumber.")]
        [RefreshProperties(RefreshProperties.All)]
        public IdentifierType IdentifierType
        {
            get
            {
                return this.identifierType;
            }

            set
            {
                try
                {
                    if (value != this.identifierType)
                    {
                        this.SetInternalState(value, this.Text);
                        this.NotifyPropertyChanged("IdentifierType");
                    }
                }
                catch
                {
                    base.Text = string.Empty;
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets or sets a unique identifier.
        /// </summary>
        /// <remarks>
        /// Defaults to "xxx-xxx-xxxx". This property is mandatory if 
        /// <see cref="P:NhsCui.Toolkit.WinForms.IdentifierLabel.IdentifierType">IdentifierType</see> is set to "NhsNumber". 
        /// </remarks>
        [Bindable(true), Category("Behavior"), Localizable(true)]
        [Description("The text associated with the IdentifierLabel control. Must be set to a valid NhsNumber if the control's IdentifierType is NhsNumber.")]
        [RefreshProperties(RefreshProperties.All)]
        public new string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                try
                {
                    if (value != base.Text)
                    {
                        this.SetInternalState(this.IdentifierType, value);
                        this.NotifyPropertyChanged("Text");
                    }
                }
                catch
                {
                    base.Text = string.Empty;
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of a unique identifier.
        /// </summary>
        /// <remarks>
        /// If <see cref="P:NhsCui.Toolkit.WinForms.IdentifierLabel.IdentifierType">IdentifierType</see> is set to "NhsNumber", 
        /// the value will be the NHS number. 
        ///</remarks>
        [Bindable(true), Category("Behavior"), DefaultValue(null), Localizable(false), Browsable(false)]
        [Description("The NhsNumber associated with the IdentifierLabel control. Will be null if the control's IdentifierType is Other. Not browsable.")]
        [RefreshProperties(RefreshProperties.All)]
        public NhsNumber Value
        {
            get
            {
                return this.nhsNumber;
            }

            set
            {
                if (value != this.nhsNumber)
                {
                    this.nhsNumber = value;

                    if (value != null)
                    {
                        base.Text = this.nhsNumber.ToString();
                        this.identifierType = IdentifierType.NhsNumber;
                    }

                    this.NotifyPropertyChanged("Value");
                }
            }
        }

        #endregion

        #region Explict Property Resets
        /// <summary>
        /// A custom reset method for the <see cref="P:NhsCui.Toolkit.WinForms.IdentifierLabel.Text">Text</see> property.
        /// </summary>
        public override void ResetText()
        {
            this.IdentifierType = IdentifierType.Other;
            this.Text = string.Empty;
        }

        #endregion

        #region ISupportInitialize Members

        /// <summary>
        /// Signals to the object that initialization is starting.  
        /// </summary>
        public virtual void BeginInit()
        {
            this.initializing = true;
        }

        /// <summary>
        /// Signals to the object that initialization is complete. 
        /// </summary>
        public virtual void EndInit()
        {
            try
            {
                this.initializing = false;
                this.SetInternalState(this.IdentifierType, this.Text);
            }
            catch
            {
                // reset control back to valid state
                this.ResetText();
                throw;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raise property changed event
        /// </summary>
        /// <param name="propertyName">Property name</param>
        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Set internal state of the control.
        /// </summary>
        /// <param name="identifierType">identifer type</param>
        /// <param name="identifier">identifier</param>
        private void SetInternalState(IdentifierType identifierType, string identifier)
        {
            string text = string.Empty;
            NhsNumber nhsNumber = null;

            if (identifierType == IdentifierType.Other || this.initializing)
            {
                text = identifier;
            }
            else if (identifierType == IdentifierType.NhsNumber && identifier != IdentifierLabelControl.Resources.IdentifierDefaultDesignTimeValue)
            {
                nhsNumber = new NhsNumber(identifier);
                text = nhsNumber.ToString();
            }

            this.identifierType = identifierType;
            this.nhsNumber = nhsNumber;
            base.Text = text;
        }
        #endregion
    }
}
