//-----------------------------------------------------------------------
// <copyright file="MedicationLabel.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>28-Oct-2009</date>
// <summary>Medication label control used in timeline graphs.</summary>
//-----------------------------------------------------------------------

namespace Microsoft.Cui.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Enumeration for timeline medication label modes.
    /// </summary>
    public enum LabelMode
    {
        /// <summary>
        /// Simple mode.
        /// </summary>
        /// <remarks>Only Medication name is shown and no truncation rules are applied.</remarks>
        Simple,

        /// <summary>
        /// Partial mode. Medication name is shown along with any brand name or fluid strength.
        /// </summary>
        Partial,

        /// <summary>
        /// Full mode.
        /// </summary>
        /// <remarks>Full details of the medication are shown and truncation rules are applied.</remarks>
        Full
    }

    /// <summary>
    /// Medication label control.
    /// </summary>
    public class MedicationLabel : Control
    {
        #region Dependency Properties
        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.TimelineMedicationLabel.MedicationName" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty MedicationNameProperty =
            DependencyProperty.Register("MedicationName", typeof(string), typeof(MedicationLabel), null);

        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.TimelineMedicationLabel.Dose" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DoseProperty =
            DependencyProperty.Register("Dose", typeof(string), typeof(MedicationLabel), null);

        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.TimelineMedicationLabel.Frequency" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty FrequencyProperty =
            DependencyProperty.Register("Frequency", typeof(string), typeof(MedicationLabel), null);

        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.TimelineMedicationLabel.Route" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty RouteProperty =
            DependencyProperty.Register("Route", typeof(string), typeof(MedicationLabel), null);

        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.MedicationLabel.FluidStrength" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty FluidStrengthProperty =
            DependencyProperty.Register("FluidStrength", typeof(string), typeof(MedicationLabel), null);

        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.MedicationLabel.SolidStrength" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty SolidStrengthProperty =
            DependencyProperty.Register("SolidStrength", typeof(string), typeof(MedicationLabel), null);

        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.MedicationLabel.Form" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty FormProperty =
            DependencyProperty.Register("Form", typeof(string), typeof(MedicationLabel), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.MedicationLabel.Rate" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty RateProperty =
            DependencyProperty.Register("Rate", typeof(string), typeof(MedicationLabel), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.MedicationLabel.DoseDuration" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DoseDurationProperty =
            DependencyProperty.Register("DoseDuration", typeof(string), typeof(MedicationLabel), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.MedicationLabel.BrandName" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty BrandNameProperty =
            DependencyProperty.Register("BrandName", typeof(string), typeof(MedicationLabel), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.MedicationLabel.DoseLabel" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty DoseLabelProperty =
            DependencyProperty.Register("DoseLabel", typeof(string), typeof(MedicationLabel), new PropertyMetadata("DOSE"));
        
        /// <summary>
        /// Identifies the <c ref="Microsoft.Cui.Controls.LabelMode.Mode" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(LabelMode), typeof(MedicationLabel), new PropertyMetadata(LabelMode.Full, new PropertyChangedCallback(OnModeChanged)));
        #endregion  
     
        /// <summary>
        /// EmDash constant.
        /// </summary>
        private const string EmDash = " — ";

        #region Template Part Members
        /// <summary>
        /// Member variable to hold medication name element.
        /// </summary>
        private Panel medicationName;

        /// <summary>
        /// Member variable to hold medication details element.
        /// </summary>
        private Panel medicationDetails;

        /// <summary>
        /// Member variable to hold ellipsis element.
        /// </summary>
        private Panel ellipsis;

        /// <summary>
        /// Member variable to hold the medication name panel in partial mode.
        /// </summary>
        private Panel partialModeMedicationName;

        /// <summary>
        /// Member variable to hold the ellipsis in partial mode.
        /// </summary>
        private Panel partialModeEllipsis;

        /// <summary>
        /// Member variable to hold label to display the medication label in simple mode.
        /// </summary>
        private Label simpleLabel;

        /// <summary>
        /// Member variable to hold the simple mode visual element.
        /// </summary>
        private FrameworkElement simple;

        /// <summary>
        /// Member variable to hold the partial mode visual element.
        /// </summary>
        private FrameworkElement partial;

        /// <summary>
        /// Member variable to hold the full mode visual element.
        /// </summary>
        private FrameworkElement full;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MedicationLabel"/> class.
        /// </summary>
        public MedicationLabel()
        {
            this.DefaultStyleKey = typeof(MedicationLabel);
            this.IsTabStop = false;
#if !SILVERLIGHT
            this.Focusable = false;
#endif
        }
        #endregion

        /// <summary>
        /// Enumeration for display states of the label.
        /// </summary>
        private enum DisplayStates
        {
            /// <summary>
            /// Shows full details.
            /// </summary>
            FullDetails,

            /// <summary>
            /// Shows medication name only.
            /// </summary>
            MedicationNameOnly,

            /// <summary>
            /// Shows medication name with ellipsis.
            /// </summary>
            MedicationNameWithEllipsis,

            /// <summary>
            /// Shows ellipsis only.
            /// </summary>
            EllipsisOnly,

            /// <summary>
            /// Show the full details in partial mode.
            /// </summary>
            PartialMode,

            /// <summary>
            /// Shows only ellipsis in partial mode.
            /// </summary>
            PartialModeEllipsis
        }

        #region Public Properties
        /// <summary>
        /// Gets or sets the name of the medication.
        /// </summary>
        /// <value>The name of the medication.</value>
        public string MedicationName
        {
            get { return (string)GetValue(MedicationNameProperty); }
            set { SetValue(MedicationNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the dose.
        /// </summary>
        /// <value>The dose for the medication.</value>
        public string Dose
        {
            get { return (string)GetValue(DoseProperty); }
            set { SetValue(DoseProperty, value); }
        }

        /// <summary>
        /// Gets or sets the route.
        /// </summary>
        /// <value>The route of the medication.</value>
        public string Route
        {
            get { return (string)GetValue(RouteProperty); }
            set { SetValue(RouteProperty, value); }
        }

        /// <summary>
        /// Gets or sets the frequency.
        /// </summary>
        /// <value>The frequency of the medication.</value>
        public string Frequency
        {
            get { return (string)GetValue(FrequencyProperty); }
            set { SetValue(FrequencyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the fluid strength of the medication.
        /// </summary>
        /// <value>The fluid strength of the medication.</value>
        public string FluidStrength
        {
            get { return (string)GetValue(FluidStrengthProperty); }
            set { SetValue(FluidStrengthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the solid strength of the medication.
        /// </summary>
        /// <value>The solid strength of the medication.</value>
        public string SolidStrength
        {
            get { return (string)GetValue(SolidStrengthProperty); }
            set { SetValue(SolidStrengthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the name of the brand.
        /// </summary>
        /// <value>The name of the brand.</value>
        public string BrandName
        {
            get { return (string)GetValue(BrandNameProperty); }
            set { SetValue(BrandNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the duration of the dose.
        /// </summary>
        /// <value>The duration of the dose.</value>
        public string DoseDuration
        {
            get { return (string)GetValue(DoseDurationProperty); }
            set { SetValue(DoseDurationProperty, value); }
        }

        /// <summary>
        /// Gets or sets the rate.
        /// </summary>
        /// <value>The rate of the medication.</value>
        public string Rate
        {
            get { return (string)GetValue(RateProperty); }
            set { SetValue(RateProperty, value); }
        }

        /// <summary>
        /// Gets or sets the form.
        /// </summary>
        /// <value>The form of the medication.</value>
        public string Form
        {
            get { return (string)GetValue(FormProperty); }
            set { SetValue(FormProperty, value); }
        }

        /// <summary>
        /// Gets or sets the dose label.
        /// </summary>
        /// <value>The dose label.</value>
        public string DoseLabel
        {
            get { return (string)GetValue(DoseLabelProperty); }
            set { SetValue(DoseLabelProperty, value); }
        }

        /// <summary>
        /// Gets or sets the label mode.
        /// </summary>
        /// <value>The label mode.</value>
        public LabelMode Mode
        {
            get { return (LabelMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }        
        #endregion        

        #region Overrides
        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes (such as a rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.medicationName = this.GetTemplateChild("MedicationName") as Panel;
            this.medicationDetails = this.GetTemplateChild("MedicationDetails") as Panel;
            this.ellipsis = this.GetTemplateChild("Ellipsis") as Panel;
            this.simpleLabel = this.GetTemplateChild("SimpleLabel") as Label;
            this.partialModeMedicationName = this.GetTemplateChild("PartialModeMedicationName") as Panel;
            this.partialModeEllipsis = this.GetTemplateChild("PartialModeEllipsis") as Panel;
            this.simple = this.GetTemplateChild("Simple") as FrameworkElement;
            this.partial = this.GetTemplateChild("Partial") as FrameworkElement;
            this.full = this.GetTemplateChild("Full") as FrameworkElement;

            VisualStateManager.GoToState(this, this.Mode.ToString() + "Mode", true);
        }        

        /// <summary>
        /// Gets the desired width of the label.
        /// </summary>
        /// <param name="maxWidth">Max width for the label.</param>
        /// <returns>Desired width.</returns>
        public double GetDesiredWidth(double maxWidth)
        {
            Size desiredSize = new Size();
            maxWidth -= this.Padding.Left + this.Padding.Right + this.BorderThickness.Left + this.BorderThickness.Right;

            if (this.medicationName == null)
            {
                this.ApplyTemplate();
            }

            if (this.full != null)
            {
                this.simple.Visibility = Visibility.Collapsed;
                this.partial.Visibility = Visibility.Collapsed;
                this.full.Visibility = Visibility.Collapsed;

                switch (this.Mode)
                {
                    case LabelMode.Simple:
                        this.simple.Visibility = Visibility.Visible;
                        break;
                    case LabelMode.Partial:
                        this.partial.Visibility = Visibility.Visible;
                        break;
                    case LabelMode.Full:
                        this.full.Visibility = Visibility.Visible;
                        break;
                }
            }

            if (this.Mode == LabelMode.Full)
            {
                if (this.medicationName != null)
                {
                    this.medicationName.Visibility = Visibility.Visible;
                    this.medicationDetails.Visibility = Visibility.Visible;
                    this.ellipsis.Visibility = Visibility.Visible;

                    DisplayStates state = this.UpdateState(maxWidth);

                    switch (state)
                    {
                        case DisplayStates.FullDetails:
                            this.medicationName.Visibility = Visibility.Visible;
                            this.medicationDetails.Visibility = Visibility.Visible;
                            this.ellipsis.Visibility = Visibility.Collapsed;
                            break;
                        case DisplayStates.MedicationNameOnly:
                            this.medicationName.Visibility = Visibility.Visible;
                            this.medicationDetails.Visibility = Visibility.Collapsed;
                            this.ellipsis.Visibility = Visibility.Collapsed;
                            break;
                        case DisplayStates.MedicationNameWithEllipsis:
                            this.medicationName.Visibility = Visibility.Visible;
                            this.medicationDetails.Visibility = Visibility.Collapsed;
                            this.ellipsis.Visibility = Visibility.Visible;
                            break;
                        case DisplayStates.EllipsisOnly:
                            this.medicationName.Visibility = Visibility.Collapsed;
                            this.medicationDetails.Visibility = Visibility.Collapsed;
                            this.ellipsis.Visibility = Visibility.Visible;
                            break;
                    }
                }                
            }
            else if (this.Mode == LabelMode.Partial)
            {
                if (this.partialModeMedicationName != null)
                {
                    this.partialModeMedicationName.Visibility = Visibility.Visible;
                    this.partialModeEllipsis.Visibility = Visibility.Visible;

                    DisplayStates state = this.UpdateState(maxWidth);
                    switch (state)
                    {
                        case DisplayStates.PartialMode:
                            this.partialModeMedicationName.Visibility = Visibility.Visible;
                            this.partialModeEllipsis.Visibility = Visibility.Collapsed;
                            break;
                        case DisplayStates.PartialModeEllipsis:
                            this.partialModeMedicationName.Visibility = Visibility.Collapsed;
                            this.partialModeEllipsis.Visibility = Visibility.Visible;
                            break;
                    }
                }
            }
            else
            {
                if (this.simpleLabel != null)
                {
                    this.simpleLabel.ResetDisplayValue();
                    this.simpleLabel.AutoEllipse(maxWidth);
                }
            }

            desiredSize = this.MeasureOverride(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return desiredSize.Width;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Called when mode is changed.
        /// </summary>
        /// <param name="d">The object whose mode was changed.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MedicationLabel label = d as MedicationLabel;
            if (label != null)
            {
                VisualStateManager.GoToState(label, label.Mode.ToString() + "Mode", true);
            }
        }

        /// <summary>
        /// Gets the width of the panel desired.
        /// </summary>
        /// <param name="panel">The panel.</param>
        /// <returns>Returns the desired width of the child elements.</returns>
        private static double GetPanelDesiredWidth(Panel panel)
        {
            double desiredWidth = 0;
            desiredWidth = panel.DesiredSize.Width;
            if (desiredWidth == 0)
            {
                panel.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                desiredWidth = panel.DesiredSize.Width;
            }            

            return desiredWidth;
        }

        /// <summary>
        /// Updates the state.
        /// </summary>
        /// <param name="maxWidth">MaxWidth of the label.</param>
        /// <returns>Returns the desired display state.</returns>
        private DisplayStates UpdateState(double maxWidth)
        {
            if (this.Mode == LabelMode.Full)
            {
                DisplayStates stateName = DisplayStates.FullDetails;
                double medicationNameWidth = GetPanelDesiredWidth(this.medicationName);
                double medicationDetailsWidth = GetPanelDesiredWidth(this.medicationDetails);
                double ellipsisWidth = GetPanelDesiredWidth(this.ellipsis);

                if (medicationNameWidth + medicationDetailsWidth > maxWidth)
                {
                    if (medicationNameWidth + ellipsisWidth > maxWidth)
                    {
                        if (medicationNameWidth > maxWidth)
                        {
                            stateName = DisplayStates.EllipsisOnly;
                        }
                        else
                        {
                            stateName = DisplayStates.MedicationNameWithEllipsis;
                        }
                    }
                    else
                    {
                        stateName = DisplayStates.MedicationNameWithEllipsis;
                    }
                }

                return stateName;
            }
            else
            {
                DisplayStates stateName = DisplayStates.PartialMode;
                double nameWidth = GetPanelDesiredWidth(this.partialModeMedicationName);
                if (nameWidth > maxWidth)
                {
                    stateName = DisplayStates.PartialModeEllipsis;
                }

                return stateName;
            }
        }    
        #endregion        
    }   
}
