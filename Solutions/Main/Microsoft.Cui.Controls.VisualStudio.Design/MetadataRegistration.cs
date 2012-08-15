//-----------------------------------------------------------------------------------
// <copyright file="MetadataRegistration.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// (c) 2007 Microsoft Corporation. 
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
// <date>20-Apr-2009</date>
// <summary>Implements a framework for design time metadata registration.</summary>
//-----------------------------------------------------------------------------------

namespace Microsoft.Cui.Controls.VisualStudio.Design
{
    using Microsoft.Windows.Design;
    using Microsoft.Windows.Design.Metadata;
    using Microsoft.Cui.Controls.Design.Common;

    /// <summary>
    /// MetadataRegistration class, for VS2008 and IRegisterMetadata.
    /// </summary>
    public class MetadataRegistration : MetadataRegistrationBase, IRegisterMetadata
    {
        /// <summary>
        /// Design time metadata registration class.
        /// </summary>
        public MetadataRegistration()
            : base()
        {
        }

        /// <summary>
        /// Provide a place to add custom attributes without creating an AttributeTableBuilder subclass.
        /// </summary>
        /// <param name="builder">The assembly attribute table builder.</param>
        protected override void AddAttributes(AttributeTableBuilder builder)
        {
            //// Note: everything here has been copied from the .Design project
            builder.AddCallback(typeof(AllergiesLabel), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(ColumnManager), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(ConceptListBox), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(CuiToggleButton), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(DataBoundCell), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(DataBoundRowGrouping), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(DataSelector), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(DataSelectorItem), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(DecoratorItemContainer), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(DecoratorItemsControl), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(DecoratorItemsWrapPanel), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(FilterControl), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(GraphBase), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(GraphPoint), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(GroupingControl), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(Label), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(LookAheadView), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(LookBehindView), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(MainView), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(MatchingTermItemContainer), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(MatchingTermItemsControl), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(PanelWrapper), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(TermItem), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(LevelOfDetailTick), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(TimeGraphBase), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(TimeIBarGraph), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(true))); // true required as inherits false from parent
            builder.AddCallback(typeof(TimeLineGraph), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(true))); // true required as inherits false from parent
            builder.AddCallback(typeof(TimeActivityGraph), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(true))); // true required as inherits false from parent
            builder.AddCallback(typeof(VisualFocusLine), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(WaitAnimation), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
            builder.AddCallback(typeof(MedicationLabel), b => b.AddCustomAttributes(new ToolboxBrowsableAttribute(false)));
        }
    }
}
