//-----------------------------------------------------------------------
// <copyright file="PatientSearchInputClientState.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <summary>Represents the state sent back and forth between client and server</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// PatientSearchInputClientState
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "FxCop gets it wrong again...")]
    internal class PatientSearchInputClientState
    {
        #region Member Vars

        /// <summary>
        /// The client-state copy of the CommonFamilyNames list.
        /// </summary>
        private string[] commonFamilyNames;

        /// <summary>
        /// The client-state copy of the character that is used to delimit the end of a group of words.
        /// </summary>
        private char endGroupDelimiter;

        /// <summary>
        /// The client-state copy of the firstUseTooltipText member var
        /// </summary>
        private string firstUseTooltipText;

        /// <summary>
        /// The client-state copy of the character that is used to delimit the end of a group of words.
        /// </summary>
        private char informationDelimiter;

        /// <summary>
        /// The client-state copy of the InformationFormat list.
        /// </summary>
        private int[] informationFormat;

        /// <summary>
        /// The client-state copy of the MandatoryInformation list.
        /// </summary>
        private int[] mandatoryInformation;

        /// <summary>
        /// The client-state copy of the maximum age recognised by the parser.
        /// </summary>
        private int maximumAge;

        /// <summary>
        /// The client-state copy of the character that is used to delimit the start of a group of words.
        /// </summary>
        private char startGroupDelimiter;

        ///<summary>
        /// The client-state copy of the patient search criteria to parse.
        ///</summary>
        private string text;

        /// <summary>
        /// The client-state copy of the Titles list.
        /// </summary>
        private string[] titles;

        #endregion
        
        #region Properties

        /// <summary>
        /// The client-state copy of the CommonFamilyNames list.
        /// </summary>
        public string[] CommonFamilyNames
        {
            get
            {
                return this.commonFamilyNames;
            }

            set
            {
                this.commonFamilyNames = value;
            }
        }

        /// <summary>
        /// The client-state copy of the character that is used to delimit the end of a group of words.
        /// </summary>
        public char EndGroupDelimiter
        {
            get
            {
                return this.endGroupDelimiter;
            }

            set
            {
                this.endGroupDelimiter = value;
            }
        }

        /// <summary>
        /// The client-state copy of the firstUseTooltipText member var
        /// </summary>
        public string FirstUseTooltipText
        {
            get
            {
                return this.firstUseTooltipText;
            }

            set
            {
                this.firstUseTooltipText = value;
            }
        }

        /// <summary>
        /// The client-state copy of the character that is used to delimit a structured list of words
        /// </summary>
        public char InformationDelimiter
        {
            get
            {
                return this.informationDelimiter;
            }

            set
            {
                this.informationDelimiter = value;
            }
        }

        /// <summary>
        /// The client-state copy of the InformationFormat list.
        /// </summary>
        public int[] InformationFormat
        {
            get
            {
                return this.informationFormat;
            }

            set
            {
                this.informationFormat = value;
            }
        }

        /// <summary>
        /// The client-state copy of the MandatoryInformation list.
        /// </summary>
        public int[] MandatoryInformation
        {
            get
            {
                return this.mandatoryInformation;
            }

            set
            {
                this.mandatoryInformation = value;
            }
        }

        /// <summary>
        /// The client-state copy of the maximum age recognised by the parser.
        /// </summary>
        public int MaximumAge
        {
            get
            {
                return this.maximumAge;
            }

            set
            {
                this.maximumAge = value;
            }
        }

        /// <summary>
        /// The client-state copy of the character that is used to delimit the start of a group of words.
        /// </summary>
        public char StartGroupDelimiter
        {
            get
            {
                return this.startGroupDelimiter;
            }

            set
            {
                this.startGroupDelimiter = value;
            }
        }

        ///<summary>
        /// The client-state copy of the patient search criteria to parse.
        ///</summary>
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
        /// The client-state copy of the Titles list.
        /// </summary>
        public string[] Titles
        {
            get
            {
                return this.titles;
            }

            set
            {
                this.titles = value;
            }
        }

        #endregion
    }
}