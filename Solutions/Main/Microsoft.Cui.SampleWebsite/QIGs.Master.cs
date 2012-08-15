// <copyright file="QIGs.Master.cs" company="Microsoft Corporation copyright 2010.">
// (c) 2010 Microsoft Corporation. All rights reserved.
// This source is subject to the Microsoft Public License.
// See http://www.microsoft.com/opensource/licenses.mspx.
//
// This document and/or software (“this Content”) has been created in partnership
// with the National Health Service (NHS) in England.  Intellectual Property Rights
// to this Content are jointly owned by Microsoft and the NHS in England, although 
// both Microsoft and the NHS are entitled to independently exercise their rights
// of ownership.  Microsoft acknowledges the contribution of the NHS in England
// through their Common User Interface programme to this Content.  Readers are 
// referred to www.cui.nhs.uk for further information on the NHS CUI Programme.
// </copyright>
// <date>15-April-2010</date>
// <summary>QIG Master Page.</summary>
namespace Microsoft.Cui.SampleWebsite
{
    #region Using...

    using System;
    using System.Globalization;
    using System.IO;
    using System.Web.UI.WebControls;
    using System.Xml;

    #endregion

    /// <summary>
    /// QIG Master Page.
    /// </summary>
    public partial class QIGs : System.Web.UI.MasterPage
    {
        #region Private Members

        /// <summary>
        /// Member varialbe indicating whether the navigation menu should collapse items that are not currently selected.
        /// </summary>
        private bool collapseInactiveItems;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating the Page Title.
        /// </summary>
        public string PageTitle 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets a value indicating the Text that will appear on the Download link.
        /// </summary>
        public string GuidanceDownloadText 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets a value indicating the Url for the Download link.
        /// </summary>
        public Uri GuidanceDownloadUrl 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets a value indicating the Alt Text for the Secondary Download link.
        /// </summary>
        public string GuidanceSecondaryDownloadAltText 
        { 
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating the Text that will appear on the Secondary Download link.
        /// </summary>
        public string GuidanceSecondaryDownloadText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating the Url for the Secondary Download link.
        /// </summary>
        public Uri GuidanceSecondaryDownloadUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating the Alt Text for the Download link.
        /// </summary>
        public string GuidanceDownloadAltText
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating the Path of the current page relative to the server.
        /// </summary>
        public string Path 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets a value indicating the Text that will appear in the Back link.
        /// </summary>
        public string BackText 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets a value indicating the Path that the user will be taken to by the Back link.
        /// </summary>
        public string BackPath 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets a value indicating the Text that will appear in the SubHeader1 element.
        /// </summary>
        public string SubHeader1 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets a value indicating the Text that will appear in the SubHeader1 element.
        /// </summary>
        public string SubHeader2 
        { 
            get; 
            set; 
        }

        #endregion

        #region UI Event Handlers

        /// <summary>
        /// Page load method.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitialiseHeaders();
            this.InitialiseNavigation();            
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialises the Page Headers setting the values for SubHeader1 and SubHeader2 properties.
        /// </summary>
        private void InitialiseHeaders()
        {
            if (this.Attributes["SubHeader1"] != null)
            {
                this.SubHeader1 = this.Attributes["SubHeader1"].ToString(CultureInfo.CurrentCulture);
            }

            if (this.Attributes["SubHeader2"] != null)
            {
                this.SubHeader2 = this.Attributes["SubHeader2"].ToString(CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Initialises the Navigation using the XML file associated with the current Path.
        /// </summary>
        private void InitialiseNavigation()
        {
            XmlDocument xmlDoc = new XmlDocument();

            string[] xmlPath = this.Attributes["PageHref"].Split('/');
            string path = Server.MapPath(String.Format(CultureInfo.CurrentCulture, "/DesignGuide/QuickGuides/{0}/navigation.xml", xmlPath[1]));

            try
            {
                if (System.IO.File.Exists(path))
                {
                    xmlDoc.Load(path);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("File not found: " + path);
                }

                int navLevel = this.FindNavigationLevel(xmlDoc.GetElementsByTagName("navigation").Item(0), 0);

                for (int i = 0; i <= navLevel; i++)
                {
                    this.Path += "../";
                }

                XmlNode collapseMode = xmlDoc.GetElementsByTagName("navigation").Item(0);
                this.collapseInactiveItems = bool.Parse(collapseMode.Attributes["collapseInactiveItems"].Value);

                XmlNode title = xmlDoc.GetElementsByTagName("title").Item(0);
                this.PageTitle = title.InnerText;
                this.Page.Title = String.Format(CultureInfo.CurrentCulture, "Guidance - {0}", this.PageTitle);

                XmlNode back = xmlDoc.GetElementsByTagName("back").Item(0);
                this.BackText = back.InnerText;
                this.BackPath = String.Format(CultureInfo.CurrentCulture, "{0}{1}", this.Path, back.Attributes["href"].Value);

                XmlNode download = xmlDoc.GetElementsByTagName("download").Item(0);
                this.GuidanceDownloadText = download.InnerText;
                this.GuidanceDownloadAltText = download.Attributes["altText"].Value;
                this.GuidanceDownloadUrl = new Uri(String.Format(CultureInfo.CurrentCulture, "{0}{1}", this.Path, download.Attributes["href"].Value), UriKind.Relative);

                XmlNode secondaryDownload = xmlDoc.GetElementsByTagName("secondarydownload").Item(0);
                
                if (secondaryDownload != null)
                {
                    this.GuidanceSecondaryDownloadText = secondaryDownload.InnerText;
                    this.GuidanceSecondaryDownloadAltText = secondaryDownload.Attributes["altText"].Value;
                    this.GuidanceSecondaryDownloadUrl = new Uri(String.Format(CultureInfo.CurrentCulture, "{0}{1}", this.Path, secondaryDownload.Attributes["href"].Value), UriKind.Relative);
                    this.SecondaryDownloadContainer.Visible = true;
                }

                Literal openingLiteral = new Literal();
                openingLiteral.Text = "<ul>";
                this.leftnav.Controls.Add(openingLiteral);                

                string navContent = this.ParseXML(xmlDoc.GetElementsByTagName("navigation").Item(0), 1, this.collapseInactiveItems);

                Literal contentsLiteral = new Literal();
                contentsLiteral.Text = navContent;
                this.leftnav.Controls.Add(contentsLiteral);

                Literal closingLiteral = new Literal();
                closingLiteral.Text = "</ul>";
                this.leftnav.Controls.Add(closingLiteral);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                this.leftnav.Visible = false;
                System.Diagnostics.Debug.WriteLine(fileNotFoundException.Message);
            }            
        }

        /// <summary>
        /// Determines the navigation level for the specified XmlNode.
        /// </summary>
        /// <param name="node">The XmlNode</param>
        /// <param name="level">The initial Navigation Level.</param>
        /// <returns>The Navigation Level.</returns>
        private int FindNavigationLevel(XmlNode node, int level)
        {
            if (node.Attributes["href"] != null && string.Equals(this.Attributes["PageHref"], node.Attributes["href"].Value))
            {
                return level;
            }
            else
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    int newLevel = this.FindNavigationLevel(node.ChildNodes[i], level + 1);

                    if (newLevel != 0)
                    {
                        return newLevel;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Creates HTML to represent a given XmlNode according to it's Navigation Level.
        /// </summary>
        /// <param name="node">The XmlNode</param>
        /// <param name="level">Navigation Level</param>
        /// <param name="collapsed">The Collapsed state for the subsequently generated navigation items.</param>
        /// <returns>HTML Output</returns>
        private string ParseXML(XmlNode node, int level, bool collapsed)
        {
            string[] html = 
            { 
                "<li {0} class='level1'><div><a href='{1}' title='Link to {4} QIG Page'>{2}</a></div>{3}</li>",
                "<li {0}><a href='{1}' title='Link to {3} QIG Page' class='level2Link'>{2}</a></li>",
                "<li {0}><a href='{1}' title='Link to {3} QIG Page' class='level3Link'>{2}</a></li>" 
            };

            string childHtml = String.Empty;

            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                XmlNode childNode = node.ChildNodes.Item(i);

                string href = childNode.Attributes["href"].Value;
                string text = childNode.Attributes["text"].Value;
                string tooltip = childNode.Attributes["tooltip"].Value;
                string active = this.Attributes["PageHref"].ToString().Contains(href) ? "id='active'" : String.Empty;

                if (collapsed)
                {
                    collapsed = String.IsNullOrEmpty(active) ? true : false;
                }

                if (level == 1)
                {
                    if (collapsed)
                    {
                        collapsed = this.ParseXML(childNode, level + 1, false).Contains("id='active'") ? false : true;
                    }

                    string innerHtml = this.ParseXML(childNode, level + 1, collapsed);
                    if (String.IsNullOrEmpty(innerHtml))
                    {
                        childHtml += String.Format(CultureInfo.CurrentCulture, html[level - 1], active, this.Path + href, text, String.Empty, tooltip);
                    }
                    else
                    {
                        childHtml += String.Format(CultureInfo.CurrentCulture, html[level - 1], active, this.Path + href, text, String.Format(CultureInfo.CurrentCulture, "{0}{1}{2}", "<ul>", innerHtml, "</ul>"), tooltip);
                    }

                    collapsed = this.collapseInactiveItems;
                }
                else if (!collapsed)
                {
                    childHtml += String.Format(CultureInfo.CurrentCulture, html[level - 1], active, this.Path + href, text, tooltip);
                    childHtml += this.ParseXML(childNode, level + 1, collapsed);
                }
            }

            return childHtml;
        }       

        #endregion
    }
}