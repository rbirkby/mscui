//-----------------------------------------------------------------------
// <copyright file="StringUtil.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>15-Nov-2007</date>
// <summary>A utility class used to manipulate strings.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.Web
{
    #region "Using"
        using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Security.Application;
    #endregion

    /// <summary>
    /// A class used to manipulate strings. 
    /// </summary>
    internal static class StringUtil
    {
        /// <summary>
        /// Html Word break character
        /// </summary>
        private const string HtmlWordBreak = "<wbr>";

        /// <summary>
        /// Gets wrappable string
        /// </summary>
        /// <param name="stringToWrap">string that needs to be wrapped</param>
        /// <returns>wrappable string</returns>
        internal static string GetWrappableString(string stringToWrap)
        {
            char[] chars = stringToWrap.ToCharArray();
            StringBuilder wrappablestring = new StringBuilder();
            foreach (char ch in chars)
            {
                wrappablestring.Append(ch);
                wrappablestring.Append(HtmlWordBreak);
            }

            if (wrappablestring.ToString().EndsWith(HtmlWordBreak, StringComparison.OrdinalIgnoreCase))
            {
                wrappablestring.Remove(wrappablestring.Length - HtmlWordBreak.Length, HtmlWordBreak.Length);
            }

            return wrappablestring.ToString();
        }

        /// <summary>
        /// Gets AntiXss encoded wrappable string
        /// </summary>
        /// <param name="stringToWrap">string that needs to be wrapped</param>
        /// <returns>AntiXss encoded wrappable string</returns>
        internal static string GetAntiXssWrappableString(string stringToWrap)
        {
            char[] chars = stringToWrap.ToCharArray();
            StringBuilder wrappablestring = new StringBuilder();
            foreach (char ch in chars)
            {
                wrappablestring.Append(AntiXss.HtmlEncode(ch.ToString()));
                wrappablestring.Append(HtmlWordBreak);
            }

            if (wrappablestring.ToString().EndsWith(HtmlWordBreak, StringComparison.OrdinalIgnoreCase))
            {
                wrappablestring.Remove(wrappablestring.Length - HtmlWordBreak.Length, HtmlWordBreak.Length);
            }

            return wrappablestring.ToString();
        }
    }
}
