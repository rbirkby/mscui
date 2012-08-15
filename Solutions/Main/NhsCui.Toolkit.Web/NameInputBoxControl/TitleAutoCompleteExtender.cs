//-----------------------------------------------------------------------
// <copyright file="TitleAutoCompleteExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>14-Aug-2007</date>
// <summary>TitleAutoComplete Extender, class to provide AutoComplete 
// functonality for the Title field of the NameInputBox </summary>
//-----------------------------------------------------------------------

[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.NameInputBoxControl.TitleAutoComplete.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("AjaxControlToolkit.PopupControl.PopupControlBehavior.js", "text/javascript")]

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AjaxControlToolkit;
    using System.Web.UI;
    using System.ComponentModel;
    using System.Web.UI.WebControls;

    /// <summary>
    /// Extender that provides suggestions to fill input in a textbox.
    /// </summary>
    [ClientScriptResource("NhsCui.Toolkit.Web.NameInputBoxControl.TitleAutoComplete", "NhsCui.Toolkit.Web.NameInputBoxControl.TitleAutoComplete.js")]
    [RequiredScript(typeof(CommonToolkitScripts))]
    [RequiredScript(typeof(PopupExtender))]
    [RequiredScript(typeof(TimerScript))]
    [RequiredScript(typeof(AnimationExtender))]
    [TargetControlType(typeof(TextBox))]
    public class TitleAutoCompleteExtender : AnimationExtenderControlBase
    {
        /// <summary>
        /// Animation on hiding
        /// </summary>
        private Animation animationOnHide;

        /// <summary>
        /// Animation on showing
        /// </summary>
        private Animation animationOnShow;

        /// <summary>
        /// Minimum length of text before the webservice provides suggestions.
        /// </summary>
        [DefaultValue(3)]
        [ExtenderControlProperty]
        [ClientPropertyName("minimumPrefixLength")]
        public virtual int MinimumPrefixLength
        {
            get
            {
                return GetPropertyValue("MinimumPrefixLength", 3);
            }

            set
            {
                SetPropertyValue("MinimumPrefixLength", value);
            }
        }

        /// <summary>
        /// Time in milliseconds when the timer will kick in to get suggestions using the web service. 
        /// </summary>
        [DefaultValue(1000)]
        [ExtenderControlProperty]
        [ClientPropertyName("completionInterval")]
        public virtual int CompletionInterval
        {
            get
            {
                return GetPropertyValue("CompletionInterval", 1000);
            }

            set
            {
                SetPropertyValue("CompletionInterval", value);
            }
        }

        /// <summary>
        /// Number of suggestions to be provided.
        /// </summary>
        [DefaultValue(10)]
        [ExtenderControlProperty]
        [ClientPropertyName("completionSetCount")]
        public virtual int CompletionSetCount
        {
            get
            {
                return GetPropertyValue("CompletionSetCount", 10);
            }

            set
            {
                SetPropertyValue("CompletionSetCount", value);
            }
        }

        /// <summary>
        /// ID of element that will serve as the completion list.
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty]
        [ClientPropertyName("completionListElementID")]
        [IDReferenceProperty(typeof(WebControl))]
        [Obsolete("Instead of passing in CompletionListElementID, use the default flyout and style that using the CssClass properties.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1706:ShortAcronymsShouldBeUppercase", MessageId = "Member")]
        public virtual string CompletionListElementID
        {
            get
            {
                return GetPropertyValue("CompletionListElementID", String.Empty);
            }

            set
            {
                SetPropertyValue("CompletionListElementID", value);
            }
        }

        /// <summary>
        /// Css Class that will be used to style the completion list flyout.
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty]
        [ClientPropertyName("completionListCssClass")]
        public string CompletionListCssClass
        {
            get
            {
                return GetPropertyValue("CompletionListCssClass", "");
            }

            set
            {
                SetPropertyValue("CompletionListCssClass", value);
            }
        }

        /// <summary>
        /// Css Class that will be used to style an item in the autocomplete list.
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty]
        [ClientPropertyName("completionListItemCssClass")]
        public string CompletionListItemCssClass
        {
            get
            {
                return GetPropertyValue("CompletionListItemCssClass", "");
            }

            set
            {
                SetPropertyValue("CompletionListItemCssClass", value);
            }
        }

        /// <summary>
        /// Css Class that will be used to style a highlighted item in the autocomplete list.
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty]
        [ClientPropertyName("highlightedItemCssClass")]
        public string CompletionListHighlightedItemCssClass
        {
            get
            {
                return GetPropertyValue("CompletionListHighlightedItemCssClass", "");
            }

            set
            {
                SetPropertyValue("CompletionListHighlightedItemCssClass", value);
            }
        }

        /// <summary>
        /// Gets or sets the character(s) used to separate words for autocomplete.
        /// </summary>
        [ExtenderControlProperty]
        [ClientPropertyName("delimiterCharacters")]
        public virtual string DelimiterCharacters
        {
            get
            {
                return GetPropertyValue("DelimiterCharacters", String.Empty);
            }

            set
            {
                SetPropertyValue("DelimiterCharacters", value);
            }
        }

        /// <summary>
        /// Determines if the First Row of the Search Results be selected by default
        /// </summary>
        [DefaultValue(false)]
        [ExtenderControlProperty]
        [ClientPropertyName("firstRowSelected")]
        public virtual bool FirstRowSelected
        {
            get
            {
                return GetPropertyValue("FirstRowSelected", false);
            }

            set
            {
                SetPropertyValue("FirstRowSelected", value);
            }
        }

        /// <summary>
        /// OnShow animation
        /// </summary>
        [ExtenderControlProperty]
        [ClientPropertyName("onShow")]
        [Browsable(false)]
        [DefaultValue(null)]
        public Animation OnShow
        {
            get
            {
                return GetAnimation(ref this.animationOnShow, "OnShow");
            }

            set
            {
                SetAnimation(ref this.animationOnShow, "OnShow", value);
            }
        }

        /// <summary>
        /// OnHide animation
        /// </summary>
        [ExtenderControlProperty]
        [ClientPropertyName("onHide")]
        [Browsable(false)]
        [DefaultValue(null)]
        public Animation OnHide
        {
            get
            {
                return GetAnimation(ref this.animationOnHide, "OnHide");
            }

            set
            {
                SetAnimation(ref this.animationOnHide, "OnHide", value);
            }
        }

        /// <summary>
        /// Don't serialize OnShow in the designer
        /// </summary>
        /// <returns>Whether we should serialize</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeOnShow()
        {
            return !DesignMode;
        }

        /// <summary>
        /// Don't serialize OnHide in the designer
        /// </summary>
        /// <returns>Whether we should serialize</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeOnHide()
        {
            return !DesignMode;
        }

        /// <summary>
        /// Convert server IDs into ClientIDs for animations
        /// </summary>
        /// <param name="e">event args</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            ResolveControlIDs(this.animationOnShow);
            ResolveControlIDs(this.animationOnHide);
        }
    }
}
