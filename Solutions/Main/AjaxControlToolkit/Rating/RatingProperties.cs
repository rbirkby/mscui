// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx.
// All other rights reserved.


using System.Web.UI.WebControls;
using System.Web.UI;
using System.ComponentModel;

using System.Web.UI.HtmlControls;
using System;
using System.Globalization;

namespace AjaxControlToolkit
{
    [DefaultProperty("Rating")]
    public class RatingProperties : TargetControlPropertiesBase
    {
        public RatingProperties()
        {
            EnableClientState = true;
        }

        public int Rating
        {
            get
            {
                string value = ClientState;
                if (value == null)
                {
                    value = "3";
                }
                return Int32.Parse(value, CultureInfo.InvariantCulture);
            }
            set
            {
                ClientState = value.ToString(CultureInfo.InvariantCulture);
            }

        }

        public string Tag
        {
            get 
            {
                return GetPropertyValue<string>("Tag", string.Empty);
            }
            set
            {
                SetPropertyValue<string>("Tag", value);
            }
        }

        public int RatingDirection
        {
            get
            {
                return GetPropertyValue<int>("RatingDirection", 0);
            }
            set
            {
                SetPropertyValue<int>("RatingDirection", value);
            }

        }
        public int MaxRating
        {
            get
            {
                return GetPropertyValue<int>("MaxRating", 5);
            }
            set
            {
                SetPropertyValue<int>("MaxRating", value);
            }
        }

        public string StarCssClass
        {
            get
            {
                return GetPropertyValue<string>("StarCssClass", String.Empty);
            }
            set
            {
                SetPropertyValue<string>("StarCssClass", value);
            }
        }

        public bool ReadOnly
        {
            get
            {
                return GetPropertyValue<bool>("ReadOnly", false);
            }
            set
            {
                SetPropertyValue<bool>("ReadOnly", value);
            }
        }

        public string FilledStarCssClass
        {
            get
            {
                return GetPropertyValue<string>("FilledStarCssClass", string.Empty);
            }
            set
            {
                SetPropertyValue<string>("FilledStarCssClass", value);
            }
        }

        public string EmptyStarCssClass
        {
            get
            {
                return GetPropertyValue<string>("EmptyStarCssClass", string.Empty);
            }
            set
            {
                SetPropertyValue<string>("EmptyStarCssClass", value);
            }
        }

        public string WaitingStarCssClass
        {
            get
            {
                return GetPropertyValue<string>("WaitingStarCssClass", string.Empty);
            }
            set
            {
                SetPropertyValue<string>("WaitingStarCssClass", value);
            }
        }
    }
}