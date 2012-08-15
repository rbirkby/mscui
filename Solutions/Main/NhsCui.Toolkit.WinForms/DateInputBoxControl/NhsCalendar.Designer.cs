//-----------------------------------------------------------------------
// <copyright file="NhsCalendar.Designer.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>3-May-2007</date>
// <summary>The designer for the calendar control used to select a date.</summary>
//-----------------------------------------------------------------------

namespace NhsCui.Toolkit.WinForms
{
    /// <summary>
    ///  Designer for the calendar control.
    /// </summary>
    internal partial class NhsCalendar
    {
        /// <summary>
        /// Componenets.
        /// </summary>
        private System.ComponentModel.IContainer components; 

        /// <summary>
        /// Main body of the calendar.
        /// </summary>
        private System.Windows.Forms.PictureBox mainCalendar;

        /// <summary>
        /// Close label.
        /// </summary>
        private System.Windows.Forms.LinkLabel closeButton;

        /// <summary>
        /// Tooltip extender for the control.
        /// </summary>
        private System.Windows.Forms.ToolTip toolTip1;

        /// <summary>
        /// navigates to the previous month.
        /// </summary>
        private System.Windows.Forms.Button prevMonthNav;

        /// <summary>
        /// navigates to the next month.
        /// </summary>
        private System.Windows.Forms.Button nextMonthNav;

        /// <summary>
        /// Label to display selected month.
        /// </summary>
        private System.Windows.Forms.Button btnMonth;

        /// <summary>
        /// Label to display selected year.
        /// </summary>
        private System.Windows.Forms.Button btnYear;

        /// <summary>
        /// Navigates to the previous year.
        /// </summary>
        private System.Windows.Forms.Button prevYearNav;

        /// <summary>
        /// Navigates to the next year.
        /// </summary>
        private System.Windows.Forms.Button nextYearNav;

        /// <summary>
        /// Button to select the current date.
        /// </summary>
        /// <remarks>
        /// Makes today as the selected date.
        /// </remarks>
        private System.Windows.Forms.Button today;       

        /// <summary>
        /// container for the bottom design.
        /// </summary>
        private System.Windows.Forms.Panel pnlBottom;

        /// <summary>
        /// container for the month controls.
        /// </summary>
        private System.Windows.Forms.Panel pnlMonth;

        /// <summary>
        /// container for the year controls.
        /// </summary>
        private System.Windows.Forms.Panel pnlYear;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">Disposing status</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainCalendar = new System.Windows.Forms.PictureBox();
            this.prevMonthNav = new System.Windows.Forms.Button();
            this.nextMonthNav = new System.Windows.Forms.Button();
            this.btnMonth = new System.Windows.Forms.Button();
            this.btnYear = new System.Windows.Forms.Button();
            this.prevYearNav = new System.Windows.Forms.Button();
            this.nextYearNav = new System.Windows.Forms.Button();
            this.today = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.closeButton = new System.Windows.Forms.LinkLabel();
            this.pnlMonth = new System.Windows.Forms.Panel();
            this.pnlYear = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)this.mainCalendar).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.pnlMonth.SuspendLayout();
            this.pnlYear.SuspendLayout();
            this.SuspendLayout();
             
            // mainCalendar
             
            this.mainCalendar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mainCalendar.Location = new System.Drawing.Point(0, 30);
            this.mainCalendar.Margin = new System.Windows.Forms.Padding(0);
            this.mainCalendar.Name = "mainCalendar";
            this.mainCalendar.Size = new System.Drawing.Size(215, 140);
            this.mainCalendar.TabIndex = 7;
            this.mainCalendar.TabStop = false;
            this.mainCalendar.MouseLeave += new System.EventHandler(this.MainCalendar_MouseLeave);
            this.mainCalendar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Calendar_MouseDown);
            this.mainCalendar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainCalendar_MouseMove);
            this.mainCalendar.Paint += new System.Windows.Forms.PaintEventHandler(this.Calendar_Paint);
             
            // prevMonthNav
             
            this.prevMonthNav.BackColor = System.Drawing.Color.Transparent;
            this.prevMonthNav.Cursor = System.Windows.Forms.Cursors.Hand;
            this.prevMonthNav.FlatAppearance.BorderSize = 0;
            this.prevMonthNav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prevMonthNav.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.prevMonthNav.ForeColor = System.Drawing.SystemColors.ControlText;
            this.prevMonthNav.Location = new System.Drawing.Point(2, 3);
            this.prevMonthNav.Name = "prevMonthNav";
            this.prevMonthNav.Size = new System.Drawing.Size(20, 24);
            this.prevMonthNav.TabIndex = 1;
            this.prevMonthNav.Text = global::NhsCui.Toolkit.WinForms.DateInputBoxControl.Resources.Prev;
            this.toolTip1.SetToolTip(this.prevMonthNav, global::NhsCui.Toolkit.WinForms.DateInputBoxControl.Resources.prevMonth);
            this.prevMonthNav.UseVisualStyleBackColor = false;
            this.prevMonthNav.Enter += new System.EventHandler(this.PrevMonthNav_Enter);
            this.prevMonthNav.Click += new System.EventHandler(this.BtnPrev_Click);
            this.prevMonthNav.Leave += new System.EventHandler(this.PrevMonthNav_Leave);
             
            // nextMonthNav
             
            this.nextMonthNav.BackColor = System.Drawing.Color.Transparent;
            this.nextMonthNav.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nextMonthNav.FlatAppearance.BorderSize = 0;
            this.nextMonthNav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextMonthNav.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.nextMonthNav.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nextMonthNav.Location = new System.Drawing.Point(101, 3);
            this.nextMonthNav.Name = "nextMonthNav";
            this.nextMonthNav.Size = new System.Drawing.Size(20, 24);
            this.nextMonthNav.TabIndex = 3;
            this.nextMonthNav.Text = global::NhsCui.Toolkit.WinForms.DateInputBoxControl.Resources.Next;
            this.toolTip1.SetToolTip(this.nextMonthNav, global::NhsCui.Toolkit.WinForms.DateInputBoxControl.Resources.nextMonth);
            this.nextMonthNav.UseVisualStyleBackColor = false;
            this.nextMonthNav.Enter += new System.EventHandler(this.NextMonthNav_Enter);
            this.nextMonthNav.Click += new System.EventHandler(this.BtnNext_Click);
            this.nextMonthNav.Leave += new System.EventHandler(this.NextMonthNav_Leave);
             
            // btnMonth
             
            this.btnMonth.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnMonth.BackColor = System.Drawing.Color.Transparent;
            this.btnMonth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMonth.FlatAppearance.BorderSize = 0;
            this.btnMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, (System.Drawing.FontStyle)System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
            this.btnMonth.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnMonth.Location = new System.Drawing.Point(18, 3);
            this.btnMonth.Name = "btnMonth";
            this.btnMonth.Size = new System.Drawing.Size(85, 24);
            this.btnMonth.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnMonth, global::NhsCui.Toolkit.WinForms.DateInputBoxControl.Resources.monthTip);
            this.btnMonth.UseVisualStyleBackColor = false;
            this.btnMonth.Enter += new System.EventHandler(this.BtnMonth_Enter);
            this.btnMonth.Click += new System.EventHandler(this.LblMonth_Click);
            this.btnMonth.Leave += new System.EventHandler(this.BtnMonth_Leave);
            this.btnMonth.SizeChanged += new System.EventHandler(this.LblMonth_SizeChanged);
             
            // btnYear
             
            this.btnYear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnYear.BackColor = System.Drawing.Color.Transparent;
            this.btnYear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYear.FlatAppearance.BorderSize = 0;
            this.btnYear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, (System.Drawing.FontStyle)System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
            this.btnYear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnYear.Location = new System.Drawing.Point(19, 3);
            this.btnYear.Name = "btnYear";
            this.btnYear.Size = new System.Drawing.Size(51, 24);
            this.btnYear.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnYear, global::NhsCui.Toolkit.WinForms.DateInputBoxControl.Resources.yearTip);
            this.btnYear.UseVisualStyleBackColor = false;
            this.btnYear.Enter += new System.EventHandler(this.BtnYear_Enter);
            this.btnYear.Click += new System.EventHandler(this.LblYear_Click);
            this.btnYear.Leave += new System.EventHandler(this.BtnYear_Leave);
            this.btnYear.SizeChanged += new System.EventHandler(this.LblYear_SizeChanged);
             
            // prevYearNav
             
            this.prevYearNav.BackColor = System.Drawing.Color.Transparent;
            this.prevYearNav.Cursor = System.Windows.Forms.Cursors.Hand;
            this.prevYearNav.FlatAppearance.BorderSize = 0;
            this.prevYearNav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.prevYearNav.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.prevYearNav.ForeColor = System.Drawing.SystemColors.ControlText;
            this.prevYearNav.Location = new System.Drawing.Point(0, 3);
            this.prevYearNav.Name = "prevYearNav";
            this.prevYearNav.Size = new System.Drawing.Size(20, 24);
            this.prevYearNav.TabIndex = 1;
            this.prevYearNav.Text = global::NhsCui.Toolkit.WinForms.DateInputBoxControl.Resources.Prev;
            this.toolTip1.SetToolTip(this.prevYearNav, global::NhsCui.Toolkit.WinForms.DateInputBoxControl.Resources.prevYear);
            this.prevYearNav.UseVisualStyleBackColor = false;
            this.prevYearNav.Enter += new System.EventHandler(this.PrevYearNav_Enter);
            this.prevYearNav.Click += new System.EventHandler(this.BtnPrevYear_Click);
            this.prevYearNav.Leave += new System.EventHandler(this.PrevYearNav_Leave);
             
            // nextYearNav
             
            this.nextYearNav.BackColor = System.Drawing.Color.Transparent;
            this.nextYearNav.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nextYearNav.FlatAppearance.BorderSize = 0;
            this.nextYearNav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextYearNav.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.nextYearNav.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nextYearNav.Location = new System.Drawing.Point(66, 3);
            this.nextYearNav.Name = "nextYearNav";
            this.nextYearNav.Size = new System.Drawing.Size(20, 24);
            this.nextYearNav.TabIndex = 3;
            this.nextYearNav.Text = global::NhsCui.Toolkit.WinForms.DateInputBoxControl.Resources.Next;
            this.toolTip1.SetToolTip(this.nextYearNav, global::NhsCui.Toolkit.WinForms.DateInputBoxControl.Resources.nextYear);
            this.nextYearNav.UseVisualStyleBackColor = false;
            this.nextYearNav.Enter += new System.EventHandler(this.NextYearNav_Enter);
            this.nextYearNav.Click += new System.EventHandler(this.BtnNextYear_Click);
            this.nextYearNav.Leave += new System.EventHandler(this.NextYearNav_Leave);
             
            // today
             
            this.today.Location = new System.Drawing.Point(3, 5);
            this.today.Name = "today";
            this.today.Size = new System.Drawing.Size(62, 21);
            this.today.TabIndex = 1;
            this.today.Text = global::NhsCui.Toolkit.WinForms.DateInputBoxControl.Resources.today;
            this.today.UseVisualStyleBackColor = true;
            this.today.Enter += new System.EventHandler(this.Today_Enter);
            this.today.Click += new System.EventHandler(this.Today_Click);
            this.toolTip1.SetToolTip(this.today, global::NhsCui.Toolkit.WinForms.DateInputBoxControl.Resources.todayTitle);
             
            // pnlBottom
             
            this.pnlBottom.Controls.Add(this.closeButton);
            this.pnlBottom.Controls.Add(this.today);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 175);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(213, 33);
            this.pnlBottom.TabIndex = 2;
             
            // closeButton
             
            this.closeButton.AutoSize = true;
            this.closeButton.LinkColor = System.Drawing.SystemColors.ControlText;
            this.closeButton.Location = new System.Drawing.Point(175, 10);
            this.closeButton.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(0, 13);
            this.closeButton.TabIndex = 3;
            this.closeButton.VisitedLinkColor = System.Drawing.SystemColors.ControlText;
            this.closeButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            this.toolTip1.SetToolTip(this.closeButton, global::NhsCui.Toolkit.WinForms.DateInputBoxControl.Resources.closeTitle);
             
            // pnlMonth
             
            this.pnlMonth.Controls.Add(this.btnMonth);
            this.pnlMonth.Controls.Add(this.prevMonthNav);
            this.pnlMonth.Controls.Add(this.nextMonthNav);
            this.pnlMonth.Location = new System.Drawing.Point(0, 0);
            this.pnlMonth.Name = "pnlMonth";
            this.pnlMonth.Size = new System.Drawing.Size(130, 30);
            this.pnlMonth.TabIndex = 0;
             
            // pnlYear
             
            this.pnlYear.Anchor = (System.Windows.Forms.AnchorStyles)System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.pnlYear.Controls.Add(this.btnYear);
            this.pnlYear.Controls.Add(this.prevYearNav);
            this.pnlYear.Controls.Add(this.nextYearNav);
            this.pnlYear.Location = new System.Drawing.Point(127, 0);
            this.pnlYear.Name = "pnlYear";
            this.pnlYear.Size = new System.Drawing.Size(88, 30);
            this.pnlYear.TabIndex = 1;
             
            // NhsCalendar
             
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(213, 208);
            this.ControlBox = false;
            this.Controls.Add(this.pnlYear);
            this.Controls.Add(this.pnlMonth);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.mainCalendar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "NhsCalendar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NhsCalendar_FormClosed);
            this.SizeChanged += new System.EventHandler(this.Nhscalendar_SizeChanged);
            this.VisibleChanged += new System.EventHandler(this.NhsCalendar_VisibleChanged);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NhsCalendar_KeyPress);
            this.Load += new System.EventHandler(this.Nhscalendar_Load);
            ((System.ComponentModel.ISupportInitialize)this.mainCalendar).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlMonth.ResumeLayout(false);
            this.pnlYear.ResumeLayout(false);
            this.ResumeLayout(false);
        }              
    }
}