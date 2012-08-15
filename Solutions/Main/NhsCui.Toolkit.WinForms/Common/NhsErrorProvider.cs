//-----------------------------------------------------------------------
// <copyright file="NhsErrorProvider.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>04-Jun-2007</date>
// <summary>A class to provide customized user interface with accessibility, for indicating that a control on a form has an error associated with it.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.WinForms
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.ComponentModel;
    using System.Drawing;
    using System.Collections;

    /// <summary>
    /// Provides a customized user interface with accessibility, for indicating that a control on a form has an error associated with it.
    /// </summary>
    [DefaultProperty("AccessibleName")]
    [ToolboxItemFilterAttribute("System.Windows.Forms", ToolboxItemFilterType.Require)]
    [ToolboxBitmap(typeof(ToolboxBitmaps.ResourceReference), "NhsErrorProvider.bmp")]
    public class NhsErrorProvider : ErrorProvider
    {
        #region Member Vars      

        /// <summary>
        /// offset from right of control.
        /// </summary>
        private const int RightOffset = 15;

        /// <summary>
        /// Collection of controls having error.
        /// </summary>
        private Hashtable errorList = new Hashtable();

        /// <summary>
        /// default action description of the control.
        /// </summary>
        private String accessibleDefaultActionDescription;

        /// <summary>
        /// default description of the control.
        /// </summary>
        private String accessibleDescription;

        /// <summary>
        /// accessible name of the control.
        /// </summary>
        private String accessibleName;

        /// <summary>
        /// accessible role of the control.
        /// </summary>
        private AccessibleRole accessibleRole;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a new instance of NhsErrorProvider.
        /// </summary>
        public NhsErrorProvider()
            : base()
        {
            this.AccessibleName = Common.ErrorProviderResources.AccessibleName;
            this.AccessibleDescription = Common.ErrorProviderResources.AccessibleDescription;
            this.AccessibleRole = AccessibleRole.StaticText;
        }
        #endregion      

        #region Public Properties    
        /// <summary>
        /// Gets or sets the default action description of the control for use by accessibility client applications.
        /// </summary>
        [Category("Accessibility"), Description("The default action description that will be reported to accessibility clients.")]
        public string AccessibleDefaultActionDescription
        {
            get
            {
                return this.accessibleDefaultActionDescription;
            }

            set
            {
                this.accessibleDefaultActionDescription = value;
            }
        }

        /// <summary>
        /// Gets or sets the description of the control used by accessibility client applications.
        /// </summary>
        [Localizable(true)]
        [Category("Accessibility"), Description("The description that will be reported to accessibility clients.")]
        public string AccessibleDescription
        {
            get
            {
                return this.accessibleDescription;
            }

            set
            {
                this.accessibleDescription = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the control used by accessibility client applications.
        /// </summary>
        [Localizable(true)]
        [Category("Accessibility"), Description("The name that will be reported to accessibility clients.")]
        public string AccessibleName
        {
            get
            {
                return this.accessibleName;
            }

            set
            {
                this.accessibleName = value;
            }
        }

        /// <summary>
        /// Gets or sets the accessible role of the control.
        /// </summary>
        [Category("Accessibility"), Description("The role that will be reported to accessibility clients.")]
        public AccessibleRole AccessibleRole
        {
            get
            {
                return this.accessibleRole;
            }

            set
            {
                this.accessibleRole = value;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the error message for the specified control.
        /// </summary>
        /// <param name="control"> Control to set error message. </param>
        /// <param name="value"> Error Message. </param>
        public new virtual void SetError(Control control, string value)
        {            
            base.SetError(control, value);

            if (value == null)
            {
                value = string.Empty;
            }

            if (control != null)
            {
                // clear error
                if (value.Length == 0)
                {
                    if (this.errorList.Contains(control.Name))
                    {
                        Label description = this.errorList[control.Name] as Label;
                        if (description != null)
                        {
                            description.Text = String.Empty;
                            description.Visible = false;
                            description.Dispose();
                        }

                        this.errorList.Remove(control.Name);
                    }                  
                }
                
                // set error
                else
                {
                    Label description;
                    if (this.errorList.Contains(control.Name))
                    {
                        description = this.errorList[control.Name] as Label;
                        if (description == null)
                        {
                            description = new Label();
                            this.SetAccessibleProperties(description);
                            description.AutoSize = true;
                            description.Font = new Font("Verdana", 8.25f);
                            this.errorList[control.Name] = description;
                        }
                    }
                    else
                    {
                        description = new Label();
                        this.SetAccessibleProperties(description);
                        description.AutoSize = true;
                        description.Font = new Font("Verdana", 8.25f);
                        this.errorList.Add(control.Name, description);
                    }

                    description.Parent = control.Parent;                    
                    description.Text = value;
                    description.Location = this.GetLocation(control, description);
                    description.Visible = true;
                }

                control.Invalidate();
            }           
        }      
        #endregion        

        #region Private Methods
        /// <summary>
        /// Sets accessible properties for the specified label.
        /// </summary>
        /// <param name="tmp"> Label to set the accessible properties.</param>
        private void SetAccessibleProperties(Label tmp)
        {
            tmp.AccessibleDefaultActionDescription = this.AccessibleDefaultActionDescription;
            tmp.AccessibleDescription = this.AccessibleDescription;
            tmp.AccessibleName = this.AccessibleName;
            tmp.AccessibleRole = this.AccessibleRole;
        }

        /// <summary>
        /// Calculates the desired location of error label.
        /// </summary>
        /// <param name="control">Control having error</param>
        /// <param name="description">Error label</param>
        /// <returns>Location for error label</returns>
        private Point GetLocation(Control control, Label description)
        {
            Point location = new Point(0, 0);
            int padding = this.GetIconPadding(control);
            switch (this.GetIconAlignment(control))
            {
                case ErrorIconAlignment.BottomLeft:                    
                    location.X = control.Left - padding - description.Width - NhsErrorProvider.RightOffset;
                    location.Y = control.Top + control.Height - description.Height;
                 break;
                case ErrorIconAlignment.BottomRight:
                    location.X = control.Right + this.GetIconPadding(control) + NhsErrorProvider.RightOffset;
                    location.Y = control.Top + control.Height - description.Height;
                 break;
                case ErrorIconAlignment.MiddleLeft:
                    location.X = control.Left - padding - description.Width - NhsErrorProvider.RightOffset;
                    location.Y = (control.Top + control.Height / 2) - (description.Height / 2);
                 break;
                case ErrorIconAlignment.MiddleRight:
                    location.X = control.Right + this.GetIconPadding(control) + NhsErrorProvider.RightOffset;
                    location.Y = (control.Top + control.Height / 2) - (description.Height / 2);
                 break;
                case ErrorIconAlignment.TopLeft:
                    location.X = control.Left - padding - description.Width - NhsErrorProvider.RightOffset;
                    location.Y = control.Top;
                 break;
                case ErrorIconAlignment.TopRight:
                    location.X = control.Right + this.GetIconPadding(control) + NhsErrorProvider.RightOffset;
                    location.Y = control.Top;
                 break;
            }

            return location;
        }
        #endregion
    }
}
