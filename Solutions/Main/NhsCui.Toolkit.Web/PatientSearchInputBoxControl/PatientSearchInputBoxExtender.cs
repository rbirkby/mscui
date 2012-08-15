//-----------------------------------------------------------------------
// <copyright file="PatientSearchInputBoxExtender.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>01-Feb-2007</date>
// <summary>PatientSearchInputBox Extender , based on the PatientSearchInputBoxExtender</summary>
//-----------------------------------------------------------------------

#region [ Resources ]

[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.PatientSearchInputBoxControl.PatientSearchInputBox.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.PatientSearchInputBoxControl.CommonFamilyNames.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.PatientSearchInputBoxControl.CommonThoroughfares.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.PatientSearchInputBoxControl.Parser.js", "text/javascript")]
[assembly: System.Web.UI.WebResource("NhsCui.Toolkit.Web.PatientSearchInputBoxControl.TitlesResource.js", "text/javascript")]

#endregion

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AjaxControlToolkit;
    using System.Web.UI;
    using System.ComponentModel;
    using System.Web.UI.WebControls;
    using NhsCui.Toolkit.DateAndTime;
    using System.Web;
    using System.Web.Script.Serialization;
    using NhsCui.Toolkit.PatientSearch;
    using System.Collections.ObjectModel;
    using Microsoft.Security.Application;

    /// <summary>
    /// PatientSearchInputBox Extender
    /// </summary>
    [ToolboxItem(false)]
    [TargetControlType(typeof(TextBox))]
    [RequiredScript(typeof(CommonToolkitScripts), 0)]
    [RequiredScript(typeof(CommonNhsCuiToolkitScripts), 1)]
    [RequiredScript(typeof(TimerScript), 2)]
    [RequiredScript(typeof(NhsDateScripts), 3)]
    [RequiredScript(typeof(PatientSearchScripts), 4)]
    [ClientScriptResource("NhsCui.Toolkit.Web.PatientSearchInputBox", "NhsCui.Toolkit.Web.PatientSearchInputBoxControl.PatientSearchInputBox.js")]
    internal class PatientSearchInputBoxExtender : ExtenderControlBase
    {
        #region Member Vars

        /// <summary>
        /// The parser instance
        /// </summary>
        private Parser parser = new Parser();

        /// <summary>
        /// object to hold our state
        /// </summary>
        private PatientSearchInputClientState state;

        /// <summary>
        /// The input text
        /// </summary>
        private string text;

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public PatientSearchInputBoxExtender()
        {
            this.EnableClientState = true;
            this.state = new PatientSearchInputClientState();

            this.ClientStateValuesLoaded += new EventHandler(this.PatientSearchInputBoxExtender_ClientStateValuesLoaded);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get and Sets the CSS class
        /// </summary>
        [DefaultValue("")]
        [ExtenderControlProperty]
        [ClientPropertyName("cssClass")]
        public virtual string CssClass
        {
            get
            {
                return GetPropertyValue("CssClass", string.Empty);
            }

            set
            {
                SetPropertyValue("CssClass", value);
            }
        }

        /// <summary>
        /// The Parser/ClientState synched copy of the CommonFamilyNames list.
        /// </summary>
        public List<string> CommonFamilyNames
        {
            get
            {
                return this.Parser.CommonFamilyNames;
            }

            set
            {
                this.Parser.CommonFamilyNames = value;
                value.CopyTo(this.state.CommonFamilyNames);
            }
        }

        /// <summary>
        /// The client-state copy of the character that is used to delimit the end of a group of words.
        /// </summary>
        [DefaultValue('"')]
        [Description("The character that is used to delimit the end of a group of words.")]
        public char EndGroupDelimiter
        {
            get
            {
                return this.Parser.EndGroupDelimiter;
            }

            set
            {
                this.state.EndGroupDelimiter = value;
                this.Parser.EndGroupDelimiter = value;
            }
        }

        /// <summary>
        /// The Parser
        /// </summary>
        public Parser Parser
        {
            get
            {
                return this.parser;
            }
        }

        /// <summary>
        /// The client-state copy of the character that is used to delimit a structured list of words
        /// </summary>
        [DefaultValue(',')]
        [Description("The character that is used to delimit a structured list of words")]
        public char InformationDelimiter
        {
            get
            {
                return this.Parser.InformationDelimiter;
            }

            set
            {
                this.Parser.InformationDelimiter = value;
                this.state.InformationDelimiter = value;
            }
        }

        /// <summary>
        /// The Parser/ClientState synched copy of the InformationFormat list.
        /// </summary>
        [Description("The Parser/ClientState synched copy of the InformationFormat list.")]
        public List<Information> InformationFormat
        {
            get
            {
                return this.Parser.InformationFormat;
            }

            set
            {
                this.Parser.InformationFormat = value;
                this.state.InformationFormat = PatientSearchInputBoxExtender.ConvertInformationListToArray(value);
            }
        }

        /// <summary>
        /// The Parser/ClientState synched copy of the MandatoryInformation list.
        /// </summary>
        [Description("The Parser/ClientState synched copy of the MandatoryInformation list.")]
        public List<Information> MandatoryInformation
        {
            get
            {
                return this.Parser.MandatoryInformation;
            }

            set
            {
                this.Parser.MandatoryInformation = value;
                this.state.MandatoryInformation = PatientSearchInputBoxExtender.ConvertInformationListToArray(value);
            }
        }

        /// <summary>
        /// The client-state copy of the maximum age recognised by the parser.
        /// </summary>
        [DefaultValue(130)]
        [Description("The maximum age recognised by the parser.")]
        public int MaximumAge
        {
            get
            {
                return this.Parser.MaximumAge;
            }

            set
            {
                this.Parser.MaximumAge = value;
                this.state.MaximumAge = value;
            }
        }

        /// <summary>
        /// The client-state copy of the character that is used to delimit the start of a group of words.
        /// </summary>
        [DefaultValue('"')]
        [Description("The character that is used to delimit the start of a group of words.")]
        public char StartGroupDelimiter
        {
            get
            {
                return this.Parser.StartGroupDelimiter;
            }

            set
            {
                this.Parser.StartGroupDelimiter = value;
                this.state.StartGroupDelimiter = value;
            }
        }

        /// <summary>
        /// The search string entered in the text box
        /// </summary>
        [Description("The search string entered in the TextBox")]
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
            }
        }

        /// <summary>
        /// The Parser/ClientState synched copy of the Titles list.
        /// </summary>
        [Description("The Parser/ClientState synched copy of the Titles list.")]
        public List<Title> Titles
        {
            get
            {
                return this.Parser.Titles;
            }

            set
            {
                this.Parser.Titles = value;
                this.state.Titles = PatientSearchInputBoxExtender.ConvertTitleListToArray(value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the PreRender event. 
        /// </summary>
        /// <param name="e">An EventArgs object that contains the event data. </param>
        protected override void OnPreRender(EventArgs e)
        {
            this.state.Text = this.Text;
            if (this.CommonFamilyNames != null)
            {
                this.state.CommonFamilyNames = new string[this.CommonFamilyNames.Count];
                this.CommonFamilyNames.CopyTo(this.state.CommonFamilyNames);
            }

            this.state.EndGroupDelimiter = this.EndGroupDelimiter;
            this.state.InformationDelimiter = this.InformationDelimiter;
            if (this.InformationFormat != null)
            {
                this.state.InformationFormat = new int[this.InformationFormat.Count];
                this.state.InformationFormat = PatientSearchInputBoxExtender.ConvertInformationListToArray(this.InformationFormat);
            }

            if (this.MandatoryInformation != null)
            {
                this.state.MandatoryInformation = new int[this.MandatoryInformation.Count];
                this.state.MandatoryInformation = PatientSearchInputBoxExtender.ConvertInformationListToArray(this.MandatoryInformation);
            }

            this.state.MaximumAge = this.MaximumAge;
            this.state.StartGroupDelimiter = this.StartGroupDelimiter;
            if (this.Titles != null)
            {
                this.state.Titles = new string[this.Titles.Count * 2];
                this.state.Titles = PatientSearchInputBoxExtender.ConvertTitleListToArray(this.Titles);
            }

            this.ClientState = new JavaScriptSerializer().Serialize(this.state);

            base.OnPreRender(e);
        }

        /// <summary>
        /// Utility method to transfer an Information List to an array of int
        /// </summary>
        /// <param name="source">Source Information List</param>
        /// <returns>Target int array</returns>
        private static int[] ConvertInformationListToArray(List<Information> source)
        {
            if (source == null)
            {
                return null;
            }

            int[] target = new int[source.Count];

            for (int sourceIndex = 0; sourceIndex < source.Count; sourceIndex++)
            {
                target[sourceIndex] = (int)source[sourceIndex];
            }

            return target;
        }

        /// <summary>
        /// Utility method to transfer a Titles List to an array of string
        /// </summary>
        /// <param name="source">Source Titles List</param>
        /// <returns>Target int array</returns>
        private static string[] ConvertTitleListToArray(List<Title> source)
        {
            if (source == null)
            {
                return null;
            }

            string[] target = new string[source.Count * 2];

            for (int sourceIndex = 0; sourceIndex < source.Count; sourceIndex++)
            {
                target[sourceIndex * 2] = source[sourceIndex].Name;
                target[sourceIndex * 2 + 1] = source[sourceIndex].Gender.ToString();
            }

            return target;
        }

        /// <summary>
        /// Utility method to transfer an array of int to an Information List
        /// </summary>
        /// <param name="source">Source int array</param>
        /// <returns>Target Information List</returns>
        private static List<Information> ConvertInformationArrayToList(int[] source)
        {
            if (source == null)
            {
                return null;
            }

            List<Information> target = new List<Information>();

            if (source != null)
            {
                for (int sourceIndex = 0; sourceIndex < source.Length; sourceIndex++)
                {
                    target.Add((Information)source[sourceIndex]);
                }
            }

            return target;
        }

        /// <summary>
        /// Utility method to transfer an array of strings to a Titles List 
        /// </summary>
        /// <param name="source">Source int array</param>
        /// <returns>Target Titles List</returns>
        private static List<Title> ConvertTitleArrayToList(string[] source)
        {
            if (source == null)
            {
                return null;
            }

            List<Title> target = new List<Title>();

            for (int sourceIndex = 0; sourceIndex < source.Length; sourceIndex += 2)
            {
                target.Add(new Title(source[sourceIndex], (Gender)(Enum.Parse(typeof(Gender), source[sourceIndex + 1]))));
            }

            return target;
        }

        /// <summary>ClientStateValuesLoaded event handler</summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void PatientSearchInputBoxExtender_ClientStateValuesLoaded(object sender, EventArgs e)
        {
            if (ClientState != null)
            {
                this.state = new JavaScriptSerializer().Deserialize<PatientSearchInputClientState>(ClientState);
                if (this.state.CommonFamilyNames != null)
                {
                    this.CommonFamilyNames = new List<string>(this.state.CommonFamilyNames);
                }

                this.EndGroupDelimiter = this.state.EndGroupDelimiter;
                this.InformationDelimiter = this.state.InformationDelimiter;

                if (this.state.InformationFormat != null)
                {
                    this.InformationFormat = PatientSearchInputBoxExtender.ConvertInformationArrayToList(this.state.InformationFormat);
                }

                if (this.state.MandatoryInformation != null)
                {
                    this.MandatoryInformation = PatientSearchInputBoxExtender.ConvertInformationArrayToList(this.state.MandatoryInformation);
                }

                this.MaximumAge = this.state.MaximumAge;
                this.StartGroupDelimiter = this.state.StartGroupDelimiter;

                if (this.state.Titles != null)
                {
                    this.Titles = PatientSearchInputBoxExtender.ConvertTitleArrayToList(this.state.Titles);
                }

                this.Text = this.state.Text;
            }
        }

        #endregion
    }
}
