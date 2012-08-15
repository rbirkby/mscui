//-----------------------------------------------------------------------
// <copyright file="PatientBannerCssClasses.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>16-Feb-2007</date>
// <summary>Patient Banner Css Classes</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;

    /// <summary>
    /// Patient Banner Css Classes
    /// </summary>
    internal static class PatientBannerCssClasses
    {
        #region Const Fields
        /// <summary>
        /// class of overall patient banner
        /// </summary>
        public const string Main = "nhscui_pb";

        /// <summary>
        /// Class of overall patient banner. applied for deceased patients
        /// </summary>
        public const string MainDeceased = "nhscui_pb_deceased";

        /// <summary>
        /// Zone1
        /// </summary>
        public const string ZoneOne = "nhscui_pb_zone1";

        /// <summary>
        /// Zone2
        /// </summary>
        public const string ZoneTwo = "nhscui_pb_zone2";

        /// <summary>
        /// Patient Name
        /// </summary>
        public const string PatientName = "nhscui_patient_name";

        /// <summary>
        /// Zone 1 Label
        /// </summary>
        public const string ZoneOneLabel = "nhscui_pb_zone1_label";

        /// <summary>
        /// Zone 1 Data
        /// </summary>
        public const string ZoneOneData = "nhscui_pb_zone1_data";

        /// <summary>
        /// Zone 2 Label
        /// </summary>
        public const string ZoneTwoLabel = "nhscui_pb_zone2_label";

        /// <summary>
        /// Zone 2 Data
        /// </summary>
        public const string ZoneTwoData = "nhscui_pb_zone2_data";

        /// <summary>
        /// Zone 2 Title
        /// </summary>
        public const string ZoneTwoTitle = "nhscui_pb_zone2_title";

        /// <summary>
        /// Active Patient
        /// </summary>
        public const string ActivePatient = "nhscui_pb_active";

        /// <summary>
        /// Dead Patient
        /// </summary>
        public const string DeadPatient = "nhscui_pb_dead";

        /// <summary>
        /// Zone 1 hover
        /// </summary>
        public const string ZoneOneHover = "nhscui_pb_zone1_hover";

        /// <summary>
        /// Zone 2 hover
        /// </summary>
        public const string ZoneTwoHover = "nhscui_pb_zone2_hover";

        /// <summary>
        /// Zone one label hover style. Applied only when the label has tooltip and on hover
        /// </summary>
        public const string ZoneOneLabelWithTooltipHover = "nhscui_pb_zone1_label_tooltip_hover";

        /// <summary>
        /// Zone one label hover style. Applied only when the data has tooltip and on hover
        /// </summary>
        public const string ZoneOneDataWithTooltipHover = "nhscui_pb_zone1_data_tooltip_hover";

        /// <summary>
        /// spearator for multiple css classes
        /// </summary>
        private const string CssClassSeparator = " ";

        #endregion
        #region Public Methods

        /// <summary>
        /// Split css class stirng into individual classes
        /// </summary>
        /// <param name="cssClass">class string to split</param>
        /// <returns>array of classes</returns>
        public static string[] SplitCssClasses(string cssClass)
        {
            return cssClass.Split(new string[] { CssClassSeparator }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Concatenate multiple css classes
        /// </summary>
        /// <param name="cssClasses">css classes</param>
        /// <returns>space spearated list</returns>
        public static string JoinCssClasses(params string[] cssClasses)
        {
            return string.Join(CssClassSeparator, cssClasses);
        }

        #endregion
    }
}
