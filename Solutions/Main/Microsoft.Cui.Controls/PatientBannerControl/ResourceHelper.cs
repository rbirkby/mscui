//-----------------------------------------------------------------------
// <copyright file="ResourceHelper.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>02-July-2009</date>
// <summary>Helper class to get resources.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls.PatientBannerControl
{
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System;

    /// <summary>
    /// Helper class to get the resource.
    /// </summary>
    internal static class ResourceHelper
    {
        /// <summary>
        /// Gets the URL for default icon to indicate allergies are present.
        /// </summary>
        internal static ImageSource AllergiesPresentIcon
        {
            get
            {
                string uri = string.Empty;
#if SILVERLIGHT
                uri = PatientBannerResources.AllergiesPresentIconURI;
#else
                uri = PatientBannerResources.AllergiesPresentIconWPFURI;
#endif
                return ResourceHelper.GetImageSourceFromPath(uri);
            }
        }

        /// <summary>
        /// Gets the URL for default icon to indicate allergies are not present.
        /// </summary>
        internal static ImageSource AllergiesNotPresentIcon
        {
            get
            {
                string uri = string.Empty;
#if SILVERLIGHT
                uri = PatientBannerResources.AllergiesNotPresentIconURI;
#else
                uri = PatientBannerResources.AllergiesNotPresentIconWPFURI;
#endif
                return ResourceHelper.GetImageSourceFromPath(uri);
            }
        }

        /// <summary>
        /// Gets the URL for default icon to indicate allergy information is unavailable.
        /// </summary>
        internal static ImageSource AllergiesUnavailableIcon
        {
            get
            {
                string uri = string.Empty;
#if SILVERLIGHT
                uri = PatientBannerResources.AllergiesUnavailableIconURI;
#else
                uri = PatientBannerResources.AllergiesUnavailableIconWPFURI;
#endif
                return ResourceHelper.GetImageSourceFromPath(uri);
            }
        }

        /// <summary>
        /// Gets the URL for default icon to indicate zone two is collapsed.
        /// </summary>
        internal static ImageSource CollapseImage
        {
            get
            {
                string uri = string.Empty;
#if SILVERLIGHT
                uri = PatientBannerResources.CollapseImageURI;
#else
                uri = PatientBannerResources.CollapseImageWPFURI;
#endif
                return ResourceHelper.GetImageSourceFromPath(uri);
            }
        }

        /// <summary>
        /// Gets the URL for default icon to indicate zone two is expanded.
        /// </summary>
        internal static ImageSource DropDownImage
        {
            get
            {
                string uri = string.Empty;
#if SILVERLIGHT
                uri = PatientBannerResources.DropDownImageURI;
#else
                uri = PatientBannerResources.DropDownImageWPFURI;
#endif
                return ResourceHelper.GetImageSourceFromPath(uri);
            }
        }

        /// <summary>
        /// Gets the image source from path.
        /// </summary>
        /// <param name="imagePath">The image path.</param>
        /// <returns>Returns ImageSource from the path.</returns>
        private static ImageSource GetImageSourceFromPath(string imagePath)
        {
#if(SILVERLIGHT)            
            ImageSourceConverter converter = new ImageSourceConverter();
            return converter.ConvertFromString(imagePath) as ImageSource;
#else
            BitmapImage img = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            return img;
#endif
        }
    }
}
