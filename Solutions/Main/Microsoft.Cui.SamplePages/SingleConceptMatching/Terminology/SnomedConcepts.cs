//-----------------------------------------------------------------------
// <copyright file="SnomedConcepts.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
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
// <date>20-Jan-2008</date>
// <summary>Predefined Snomed Concepts for doing post-coordination.</summary>
//-----------------------------------------------------------------------
#if SILVERLIGHT
namespace Microsoft.Cui.SamplePages
#else
namespace Microsoft.Cui.SampleWinform
#endif
{
    #region Using

    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
#if SILVERLIGHT
    using Microsoft.Cui.SamplePages.TerminologyProvider;
#else
    using Microsoft.Cui.SampleWinform.TerminologyProvider;
#endif

    #endregion
    /// <summary>
    /// Predefined Snomed Concepts for doing post-coordination.
    /// </summary>
    internal static class SnomedConcepts
    {
        /// <summary>
        /// Known-absent Snomed Id.
        /// </summary>
        private const string KnownAbsentSnomedId = "410516002";

        /// <summary>
        /// Situation with explicit context Snomed Id.
        /// </summary>
        private const string SituationSnomedId = "243796009";

        /// <summary>
        /// Finding Context Snomed Id.
        /// </summary>
        private const string FindingContextSnomedId = "408729009";

        /// <summary>
        /// Finding Site Snomed Id.
        /// </summary>
        private const string FindingSiteSnomedId = "363698007";

        /// <summary>
        /// Associated Finding Snomed Id.
        /// </summary>
        private const string AssociatedFindingSnomedId = "246090004";

        /// <summary>
        /// Laterality Snomed Id.
        /// </summary>
        private const string LateralitySnomedId = "272741003";

        /// <summary>
        /// Severity Snomed Id.
        /// </summary>
        private const string SeveritySnomedId = "246112005";

        /// <summary>
        /// Episodicity Snomed Id.
        /// </summary>
        private const string EpisodicitySnomedId = "246456000";

        /// <summary>
        /// Clinical Course Snomed Id.
        /// </summary>
        private const string ClinicalCourseSnomedId = "263502005";

        /// <summary>
        /// Side Snomed Id.
        /// </summary>
        private const string SideSnomedId = "182353008";

        /// <summary>
        /// Left Snomed Id.
        /// </summary>
        private const string LeftSnomedId = "7771000";

        /// <summary>
        /// Right Snomed Id.
        /// </summary>
        private const string RightSnomedId = "24028007";

        /// <summary>
        /// Unilateral Snomed Id.
        /// </summary>
        private const string UnilateralSnomedId = "66459002";

        /// <summary>
        /// Episodicities Snomed Id.
        /// </summary>
        private const string EpisodicitiesSnomedId = "288526004";

        /// <summary>
        /// Courses Snomed Id.
        /// </summary>
        private const string CoursesSnomedId = "288524001";

        /// <summary>
        /// Chronic Snomed Id.
        /// </summary>
        private const string ChronicSnomedId = "90734009";

        /// <summary>
        /// Intermittent Snomed Id.
        /// </summary>
        private const string IntermittentSnomedId = "7087005";

        /// <summary>
        /// Recurrent Snomed Id.
        /// </summary>
        private const string RecurrentSnomedId = "255227004";

        /// <summary>
        /// Relapsing Remitting Snomed Id.
        /// </summary>
        private const string RelapsingRemittingSnomedId = "255318003";

        /// <summary>
        /// Cyclic Snomed Id.
        /// </summary>
        private const string CyclicSnomedId = "44180009";

        /// <summary>
        /// Sudden Onset And Or Short Duration SnomedId.
        /// </summary>
        private const string SuddenOnsetAndOrShortDurationSnomedId = "424124008";

        /// <summary>
        /// Clinical Course With Short Duration Snomed Id.
        /// </summary>
        private const string ClinicalCourseWithShortDurationSnomedId = "424572001";

        /// <summary>
        /// Sudden Onset.
        /// </summary>
        private const string SuddenOnset = "385315009";        

        /// <summary>
        /// Severities Snomed Id.
        /// </summary>
        private const string SeveritiesSnomedId = "272141005";

        /// <summary>
        /// Post Coordination Concepts List.
        /// </summary>
        private static ObservableCollection<PostCoordinationConcept> postCoordinationConceptsList = new ObservableCollection<PostCoordinationConcept>()
        {
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.AssociatedFindingSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.FindingContextSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.FindingSiteSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.KnownAbsentSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.SituationSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.LateralitySnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.LeftSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.RightSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.UnilateralSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.SeveritySnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.ClinicalCourseSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.ChronicSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.IntermittentSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.RecurrentSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.RelapsingRemittingSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.CyclicSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.SuddenOnsetAndOrShortDurationSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.ClinicalCourseWithShortDurationSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.SuddenOnset } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.EpisodicitySnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.SideSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.EpisodicitiesSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.CoursesSnomedId } },
            new PostCoordinationConcept() { ConceptDetail = new ConceptDetail() { SnomedConceptId = SnomedConcepts.SeveritiesSnomedId } }
        };

        /// <summary>
        /// Occurs when [initialized].
        /// </summary>
        internal static event EventHandler<BaseTerminologyEventArgs> Initialized;

        /// <summary>
        /// Gets the KnownAbsent ConceotDetail object.
        /// </summary>
        /// <value>The KnownAbsent ConceptDetail object.</value>
        public static ConceptDetail KnownAbsent
        {
            get
            {
                PostCoordinationConcept postCoordinationConcept = SnomedConcepts.postCoordinationConceptsList.SingleOrDefault(p => p.ConceptDetail.SnomedConceptId == SnomedConcepts.KnownAbsentSnomedId);

                if (postCoordinationConcept.IsInitialized)
                {
                    return postCoordinationConcept.ConceptDetail;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the Situation ConceptDetail object.
        /// </summary>
        /// <value>The situation ConceptDetail object.</value>
        public static ConceptDetail Situation
        {
            get
            {
                PostCoordinationConcept postCoordinationConcept = SnomedConcepts.postCoordinationConceptsList.SingleOrDefault(p => p.ConceptDetail.SnomedConceptId == SnomedConcepts.SituationSnomedId);

                if (postCoordinationConcept.IsInitialized)
                {
                    return postCoordinationConcept.ConceptDetail;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the finding site ConceptDetail object.
        /// </summary>
        /// <value>The finding site ConceptDetail object.</value>
        public static ConceptDetail FindingSite
        {
            get
            {
                PostCoordinationConcept postCoordinationConcept = SnomedConcepts.postCoordinationConceptsList.SingleOrDefault(p => p.ConceptDetail.SnomedConceptId == SnomedConcepts.FindingSiteSnomedId);

                if (postCoordinationConcept.IsInitialized)
                {
                    return postCoordinationConcept.ConceptDetail;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the finding context ConceptDetail object.
        /// </summary>
        /// <value>The finding context ConceptDetail object.</value>
        public static ConceptDetail FindingContext
        {
            get
            {
                PostCoordinationConcept postCoordinationConcept = SnomedConcepts.postCoordinationConceptsList.SingleOrDefault(p => p.ConceptDetail.SnomedConceptId == SnomedConcepts.FindingContextSnomedId);

                if (postCoordinationConcept.IsInitialized)
                {
                    return postCoordinationConcept.ConceptDetail;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the associated finding ConceptDetail object.
        /// </summary>
        /// <value>The associated finding ConceptDetail object.</value>
        public static ConceptDetail AssociatedFinding
        {
            get
            {
                PostCoordinationConcept postCoordinationConcept = SnomedConcepts.postCoordinationConceptsList.SingleOrDefault(p => p.ConceptDetail.SnomedConceptId == SnomedConcepts.AssociatedFindingSnomedId);

                if (postCoordinationConcept.IsInitialized)
                {
                    return postCoordinationConcept.ConceptDetail;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the laterality ConceptDetail object.
        /// </summary>
        /// <value>The laterality ConceptDetail object.</value>
        public static ConceptDetail Laterality
        {
            get
            {
                PostCoordinationConcept postCoordinationConcept = SnomedConcepts.postCoordinationConceptsList.SingleOrDefault(p => p.ConceptDetail.SnomedConceptId == SnomedConcepts.LateralitySnomedId);

                if (postCoordinationConcept.IsInitialized)
                {
                    return postCoordinationConcept.ConceptDetail;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the severity ConceptDetail object.
        /// </summary>
        /// <value>The severity ConceptDetail object.</value>
        public static ConceptDetail Severity
        {
            get
            {
                PostCoordinationConcept postCoordinationConcept = SnomedConcepts.postCoordinationConceptsList.SingleOrDefault(p => p.ConceptDetail.SnomedConceptId == SnomedConcepts.SeveritySnomedId);

                if (postCoordinationConcept.IsInitialized)
                {
                    return postCoordinationConcept.ConceptDetail;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the clinical course ConceptDetail object.
        /// </summary>
        /// <value>The clinical course ConceptDetail object.</value>
        public static ConceptDetail ClinicalCourse
        {
            get
            {
                PostCoordinationConcept postCoordinationConcept = SnomedConcepts.postCoordinationConceptsList.SingleOrDefault(p => p.ConceptDetail.SnomedConceptId == SnomedConcepts.ClinicalCourseSnomedId);

                if (postCoordinationConcept.IsInitialized)
                {
                    return postCoordinationConcept.ConceptDetail;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the episodicity ConceptDetail object.
        /// </summary>
        /// <value>The episodicity ConceptDetail object.</value>
        public static ConceptDetail Episodicity
        {
            get
            {
                PostCoordinationConcept postCoordinationConcept = SnomedConcepts.postCoordinationConceptsList.SingleOrDefault(p => p.ConceptDetail.SnomedConceptId == SnomedConcepts.EpisodicitySnomedId);

                if (postCoordinationConcept.IsInitialized)
                {
                    return postCoordinationConcept.ConceptDetail;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the side ConceptDetail object.
        /// </summary>
        /// <value>The side qualifier ConceptDetail object.</value>
        public static ConceptDetail Side
        {
            get
            {
                PostCoordinationConcept postCoordinationConcept = SnomedConcepts.postCoordinationConceptsList.SingleOrDefault(p => p.ConceptDetail.SnomedConceptId == SnomedConcepts.SideSnomedId);

                if (postCoordinationConcept.IsInitialized)
                {
                    return postCoordinationConcept.ConceptDetail;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the Episodicities ConceptDetail object.
        /// </summary>
        /// <value>The Episodicities qualifier ConceptDetail object.</value>
        public static ConceptDetail Episodicities
        {
            get
            {
                PostCoordinationConcept postCoordinationConcept = SnomedConcepts.postCoordinationConceptsList.SingleOrDefault(p => p.ConceptDetail.SnomedConceptId == SnomedConcepts.EpisodicitiesSnomedId);

                if (postCoordinationConcept.IsInitialized)
                {
                    return postCoordinationConcept.ConceptDetail;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the Courses ConceptDetail object.
        /// </summary>
        /// <value>The Courses qualifier ConceptDetail object.</value>
        public static ConceptDetail Courses
        {
            get
            {
                PostCoordinationConcept postCoordinationConcept = SnomedConcepts.postCoordinationConceptsList.SingleOrDefault(p => p.ConceptDetail.SnomedConceptId == SnomedConcepts.CoursesSnomedId);

                if (postCoordinationConcept.IsInitialized)
                {
                    return postCoordinationConcept.ConceptDetail;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the severities ConceptDetail object.
        /// </summary>
        /// <value>The severities qualifier ConceptDetail object.</value>
        public static ConceptDetail Severities
        {
            get
            {
                PostCoordinationConcept postCoordinationConcept = SnomedConcepts.postCoordinationConceptsList.SingleOrDefault(p => p.ConceptDetail.SnomedConceptId == SnomedConcepts.SeveritiesSnomedId);

                if (postCoordinationConcept.IsInitialized)
                {
                    return postCoordinationConcept.ConceptDetail;
                }
                else
                {
                    return null;
                }
            }
        }
        
        /// <summary>
        /// Finds the post coordination concept.
        /// </summary>
        /// <param name="snomedId">The snomed id.</param>
        /// <returns>The concept detail matching the snomed id.</returns>
        public static ConceptDetail FindPostCoordinationConcept(string snomedId)
        {
            return SnomedConcepts.postCoordinationConceptsList.SingleOrDefault(p => p.ConceptDetail.SnomedConceptId == snomedId).ConceptDetail;
        }

        /// <summary>
        /// Calls out to service provider to get the ConceptDetails needed for laterality, negation and refinable characteristics.
        /// </summary>
        public static void Initialize()
        {
            TerminologyProviderClient terminologyProviderClient = new TerminologyProviderClient();

            terminologyProviderClient.GetConceptDetailsCompleted += delegate(object sender, GetConceptDetailsCompletedEventArgs e)
            {
                if (e.Error == null && e.Result != null)
                {
                    int index = (int)e.UserState;

                    SnomedConcepts.postCoordinationConceptsList[index] = new PostCoordinationConcept(e.Result, true);

                    SnomedConcepts.CheckInitialized();
                }
                else
                {
                    if (SnomedConcepts.Initialized != null)
                    {
                        SnomedConcepts.Initialized(null, new BaseTerminologyEventArgs(false));
                    }
                }
            };

            for (int i = 0; i < SnomedConcepts.postCoordinationConceptsList.Count; i++)
            {
                ConceptDetail conceptDetail = SnomedConcepts.postCoordinationConceptsList[i].ConceptDetail;

                if (conceptDetail != null && !string.IsNullOrEmpty(conceptDetail.SnomedConceptId))
                {
                    terminologyProviderClient.GetConceptDetailsAsync(conceptDetail.SnomedConceptId, TerminologyManager.LocaleString, i);
                }
            }
        }

        /// <summary>
        /// Checks the initialized.
        /// </summary>
        private static void CheckInitialized()
        {
            bool initialized = false;

            foreach (PostCoordinationConcept postCoordinationConcept in SnomedConcepts.postCoordinationConceptsList)
            {
                if (postCoordinationConcept != null && postCoordinationConcept.IsInitialized)
                {
                    initialized = true;
                }
                else
                {
                    initialized = false;
                    break;
                }
            }

            if (initialized && SnomedConcepts.Initialized != null)
            {
                SnomedConcepts.Initialized(null, new BaseTerminologyEventArgs(true));
            }
        }        
    }
}