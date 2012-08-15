//-----------------------------------------------------------------------
// <copyright file="EncodedConcept.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>22-Jan-2008</date>
// <summary>An representation of an encoded concept.</summary>
//-----------------------------------------------------------------------
#if SILVERLIGHT
namespace Microsoft.Cui.SamplePages
#else
namespace Microsoft.Cui.SampleWinform
#endif
{
    #region Using

    using System.Collections.ObjectModel;
    using System.Text;
#if SILVERLIGHT
    using Microsoft.Cui.SamplePages.TerminologyProvider;
#else
    using Microsoft.Cui.SampleWinform.TerminologyProvider;
#endif

    #endregion

    /// <summary>
    /// An representation of an encoded concept.
    /// </summary>
    public class EncodedConcept
    {
        #region Private Const

        /// <summary>
        /// Unencoded Concept.
        /// </summary>
        private const string UnencodedConcept = "Unencoded Concept";

        #endregion

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodedConcept"/> class.
        /// </summary>
        public EncodedConcept()
        {
            this.AttributeCollection = new ObservableCollection<AttributeValuePair>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EncodedConcept"/> class.
        /// </summary>
        /// <param name="conceptDetail">The concept detail.</param>
        public EncodedConcept(ConceptItem conceptDetail) : this()
        {
            this.EncodedSingleConcept = conceptDetail;            
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the name of the EncodedConcept.
        /// </summary>
        /// <value>The name of the EncodedConcept.</value>
        public string Name
        {
            get
            {                
                return this.BuildName(this);
            }
        }

        /// <summary>
        /// Gets or sets the encoded single concept.
        /// </summary>
        /// <value>The encoded single concept.</value>
        public ConceptItem EncodedSingleConcept { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the additional encoded concepts.
        /// </summary>
        /// <value>The additional encoded concepts.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "Set in XAML")]
        public ObservableCollection<AttributeValuePair> AttributeCollection { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Inserts the tabs.
        /// </summary>
        /// <param name="numberOfTabs">The number of tabs.</param>
        /// <returns>Returns a string containing the required number of tabs.</returns>
        private static string InsertTabs(int numberOfTabs)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < numberOfTabs; i++)
            {
                stringBuilder.Append("\t\t");
            }

            return stringBuilder.ToString();
        }

         /// <summary>
        /// Builds the name from the encoded concept.
        /// </summary>
        /// <param name="encodedConcept">The encoded concept.</param>
        /// <returns>A build string representing the name of the concept.</returns>
        private string BuildName(EncodedConcept encodedConcept)
        {
            return this.BuildName(encodedConcept, 0);
        }

        /// <summary>
        /// Builds the name from the encoded concept for a specified depth.
        /// </summary>
        /// <param name="encodedConcept">The encoded concept.</param>
        /// <param name="depth">The depth.</param>
        /// <returns>A build string representing the name of the concept.</returns>
        private string BuildName(EncodedConcept encodedConcept, int depth)
        {
            string nameString = string.Empty;

            if (encodedConcept != null && encodedConcept.EncodedSingleConcept != null)
            {
                nameString += encodedConcept.EncodedSingleConcept.FullySpecifiedName + " (" + encodedConcept.EncodedSingleConcept.SnomedConceptId + ")";

                if (encodedConcept.AttributeCollection != null && encodedConcept.AttributeCollection.Count > 0)
                {
                    foreach (AttributeValuePair attributeValuePair in encodedConcept.AttributeCollection)
                    {
                        nameString += "\r\n" + EncodedConcept.InsertTabs(depth + 1) + attributeValuePair.Attribute.PreferredTerm + " (" + attributeValuePair.Attribute.SnomedConceptId + ") -> " + this.BuildName(attributeValuePair.Value, depth + 1);
                    }
                }
            }
            else
            {
                return EncodedConcept.UnencodedConcept;
            }

            return nameString;
        }
        
        #endregion
    }
}
