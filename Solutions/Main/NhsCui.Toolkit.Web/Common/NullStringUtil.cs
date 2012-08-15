//-----------------------------------------------------------------------
// <copyright file="NullStringUtil.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>14-Feb-2007</date>
// <summary>A utility class used to manipulate null strings. EDITED BY AON BUT NOT YET REVIEWED.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    using System;
    using System.Resources;

    /// <summary>
    /// A class used to manipulate null strings. EDITED BY AON BUT NOT YET REVIEWED
    /// </summary>
    internal static class NullStringUtil
    {
        #region Public Methods
        /// <summary>
        /// Trim and validate the supplied null AON: Checking this with ChrisS. 
        /// </summary>
        /// <param name="nullStrings">Null strings to trim and check. AON: Checking this with ChrisS.</param>
        /// <param name="throwException">If true and validation fails, throw an exception. EDITED BY AON BUT NOT YET REVIEWED</param>
        /// <returns>True if valid; otherwise false. EDITED BY AON BUT NOT YET REVIEWED</returns>
        public static bool TrimAndValidate(string[] nullStrings, bool throwException)
        {
            for (int i = 0; i < nullStrings.Length; i++)
            {
                string nullString = (nullStrings[i] == null ? null : nullStrings[i].Trim());

                if (string.IsNullOrEmpty(nullString))
                {
                    if (throwException)
                    {
                        ResourceManager rm = new ResourceManager("NhsCui.Toolkit.Web.Common.NhsDateResources", typeof(NullStringUtil).Assembly);
                        throw new ArgumentException(rm.GetString("NullStringNullOrEmpty"));
                    }

                    return false;
                }

                // checking for duplicate null string (case insensitive).
                for (int j = 0; j < i; j++)
                {                                        
                    if (String.Compare(nullStrings[j], nullString, true, System.Globalization.CultureInfo.CurrentCulture) == 0)
                    {
                        if (throwException)
                        {
                            ResourceManager rm = new ResourceManager("NhsCui.Toolkit.Web.Common.NhsDateResources", typeof(NullStringUtil).Assembly);
                            throw new ArgumentException(rm.GetString("NullStringDuplicate") + nullString);
                        }

                        return false;                        
                    }
                }              

                nullStrings[i] = nullString;
            }

            return true;
        }
        #endregion
    }
}
