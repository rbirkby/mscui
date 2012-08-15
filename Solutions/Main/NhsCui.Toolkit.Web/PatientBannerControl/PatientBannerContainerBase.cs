//-----------------------------------------------------------------------
// <copyright file="PatientBannerContainerBase.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Base class for vontainer control to hold patient banner templates</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Base class for container control to hold patient banner templates
    /// </summary>
    internal abstract class PatientBannerContainerBase : WebControl
    {
        #region Private Variables
        /// <summary>
        /// owner control
        /// </summary>
        private PatientBanner owner;

        #endregion

        #region Constructors
        /// <summary>
        /// construct given owner control
        /// </summary>
        /// <param name="owner">owner patient banner control</param>
        /// <param name="tagKey">control tag</param>
        /// <param name="cssClass">css class</param>
        protected PatientBannerContainerBase(PatientBanner owner, HtmlTextWriterTag tagKey, string cssClass)
            : base(tagKey)
        {
            this.CssClass = cssClass;
            this.owner = owner;
        }
        #endregion

        #region Protected Properties

        /// <summary>
        /// Owner patient banner control
        /// </summary>
        protected PatientBanner Owner
        {
            get
            {
                return this.owner;
            }
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Find a control in within the container
        /// </summary>
        /// <typeparam name="T">type of the control to search for</typeparam>
        /// <param name="id">id of the control</param>
        /// <param name="throwExceptionIfNotFound">if true throw an exception if not found</param>
        /// <returns>control or null if not found (and throwExceptionIfNotFound is false)</returns>
        protected T FindControl<T>(string id, bool throwExceptionIfNotFound) where T: class
        {
            Control control = this.FindControl(id);
            T typedControl = control as T;

            if (typedControl == null && throwExceptionIfNotFound)
            {
                string messageFormat = PatientBannerControl.Resources.TemplateControlNotFoundMessage;
                throw new HttpException(string.Format(CultureInfo.CurrentCulture, messageFormat, id, this.Owner.ID));
            }

            return typedControl;
        }
        #endregion
    }
}
