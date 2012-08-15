//-----------------------------------------------------------------------
// <copyright file="DrugSearchHelper.cs" company="Microsoft Corporation and Crown copyright 2007 - 2010.">
// Copyright (c) Microsoft Corporation and Crown copyright 2007 - 2010.
// All rights reserved
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
// <date>24-Jul-2009</date>
// <summary>
//      A class containing helper functions for searching drug data.
// </summary>
//-----------------------------------------------------------------------
namespace Microsoft.Cui.SamplePages
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// A class containing helper functions for searching drug data.
    /// </summary>
    public static class DrugSearchHelper
    {
        /// <summary>
        /// Returns an array of drugs matching a search string.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns>An array of matched drugs.</returns>
        public static Drug[] Search(string searchText)
        {
            // The final set of results.
            List<Drug> results = new List<Drug>();

            // An intial filter on all of the drugs to those that definately contain a match.
            Drug[] matchedDrugs = (from drug in DrugDataHelper.Instance.AllDrugs
                                       where string.IsNullOrEmpty(drug.BrandName) ? DrugSearchHelper.ContainsStartsWith(drug.Name, searchText) : DrugSearchHelper.ContainsStartsWith(drug.BrandName, searchText)
                                       orderby drug.Name
                                       select drug).ToArray();

            // The search words are split.
            string[] words = searchText.Replace("+", " ").Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);            

            // A list of drugs added so far to avoid duplication.
            List<Drug> addedDrugs = new List<Drug>();

            // This for loop creates the generic and brand groups within the search results.
            // i == 0 : just generic matches.
            // i == 1 : brand matches where there is also a generic name.
            // i == 2 : brand matches where there is no generic name.
            for (int i = 0; i < 3; i++)
            {
                // A dictionary for storing the results for this group. The dictionary is initially keyed by
                // the position of a match (i.e. the first word, the second word etc.), it is then keyed
                // by the position of the ingredient the match was found in.
                Dictionary<int, Dictionary<int, List<Drug>>> orderedResults = new Dictionary<int, Dictionary<int, List<Drug>>>();

                foreach (string word in words)
                {
                    // This filters all matches to the current word.
                    Drug[] wordMatches = DrugSearchHelper.GetMatches(word, matchedDrugs, addedDrugs.ToArray(), i < 2, i > 0);

                    // This tracks how many results have been added so far, so that the loop can 
                    // break out once all matches in this group have been sorted.
                    int addedWordCount = 0;
                    int matchPosition = 0;
                    
                    // While all matches in the group have not been sorted...
                    while (addedWordCount != wordMatches.Length)
                    {
                        // Add a new dictionary, keyed by the position the ingredient matched.
                        if (!orderedResults.ContainsKey(matchPosition))
                        {
                            orderedResults.Add(matchPosition, new Dictionary<int, List<Drug>>());
                        }

                        int addedIngredientCount = 0;
                        int ingredientCount = 1;
                        
                        // This filters the matches to those that only have matches in a specific
                        // word location (i.e. the first word, the second word).
                        Drug[] positionMatches = DrugSearchHelper.GetMatches(matchPosition, word, wordMatches, addedDrugs.ToArray(), i < 2, i > 0);

                        // This loop moves through the matches first adding drugs with one ingredient,
                        // then adding drugs with two ingredients etc.
                        while (addedIngredientCount != positionMatches.Length)
                        {
                            // This filters the matches based on how many ingredients there should be.
                            Drug[] ingredientCountMatches = DrugSearchHelper.GetMatches(ingredientCount, positionMatches, addedDrugs.ToArray(), i < 2, i > 0);
                            int ingredientPosition = 0;
                            int addedWordIngredientCount = 0;

                            // This loop moves through the matches looking for a match in a specific
                            // word within a specific ingredient.
                            while (addedWordIngredientCount != ingredientCountMatches.Length)
                            {
                                // Adds a list of drugs based on the position of the ingredient where the match was found.
                                if (!orderedResults[matchPosition].ContainsKey(ingredientPosition))
                                {
                                    orderedResults[matchPosition].Add(ingredientPosition, new List<Drug>());
                                }

                                // This final filter gets matches for a specific word position, within a 
                                // specific ingredient position, with a specific ingredient count.
                                Drug[] wordIngredientCountMatches = DrugSearchHelper.GetMatches(matchPosition, ingredientPosition, word, ingredientCountMatches, addedDrugs.ToArray(), i < 2, i > 0);

                                // Add the results to the result set.
                                orderedResults[matchPosition][ingredientPosition].AddRange(wordIngredientCountMatches);

                                // Add the results to the tracking list.
                                addedDrugs.AddRange(wordIngredientCountMatches);

                                // Update the counts.
                                addedWordCount += wordIngredientCountMatches.Length;
                                addedIngredientCount += wordIngredientCountMatches.Length;
                                addedWordIngredientCount += wordIngredientCountMatches.Length;

                                // Increment the ingredient position.
                                ingredientPosition++;
                            }

                            // Increment the ingredient count.
                            ingredientCount++;
                        }

                        // Increment the match position.
                        matchPosition++;
                    }
                }

                // This loop moves through the dictionary and adds the results to 
                // a flat list.
                for (int matchPosition = 0; matchPosition < orderedResults.Count; matchPosition++)
                {
                    if (orderedResults.ContainsKey(matchPosition))
                    {
                        for (int ingredientPosition = 0; ingredientPosition < orderedResults[matchPosition].Count; ingredientPosition++)
                        {
                            if (orderedResults[matchPosition].ContainsKey(ingredientPosition))
                            {
                                results.AddRange(orderedResults[matchPosition][ingredientPosition].ToArray());
                            }
                        }
                    }
                }
            }

            return results.ToArray();
        }

        /// <summary>
        /// Within a set of terms, contains a starts with in a given position.
        /// </summary>
        /// <param name="terms">The set of terms.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="position">The desired position of the match within the terms.</param>
        /// <returns>Whether a match was found within the terms.</returns>
        public static bool ContainsStartsWithInPosition(string[] terms, string searchText, int position)
        {
            bool containsStartsWithInPosition = false;
            foreach (string text in terms)
            {
                string[] textParts = text.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (textParts.Length > position && DrugSearchHelper.StartsWith(textParts[position], searchText))
                {
                    containsStartsWithInPosition = true;
                }
            }

            return containsStartsWithInPosition;
        }

        /// <summary>
        /// Returns a value indicating if any part of a drug contains a starts with.
        /// </summary>
        /// <param name="text">The drug name text.</param>
        /// <param name="searchText">The search text.</param>
        /// <returns>Whether any part of the drug contains a start with.</returns>
        public static bool ContainsStartsWith(string text, string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            else
            {
                string[] searchTextParts = searchText.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);                
                foreach (string searchPart in searchTextParts)
                {
                    if (!StartsWith(text, searchPart))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Checks if a string starts with a specified string.
        /// </summary>
        /// <param name="text">The string to check contains a match at the beginning.</param>
        /// <param name="searchText">The specified start of the string.</param>
        /// <returns>Whether the string contains a match.</returns>
        public static bool StartsWith(string text, string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return true;
            }
            else if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            else
            {
                string lowerText = " " + text.ToLower(CultureInfo.CurrentCulture);
                if (!lowerText.Contains(" " + searchText.ToLower(CultureInfo.CurrentCulture)) &&
                        !DrugSearchHelper.FindMatch(lowerText, " " + searchText.ToLower(CultureInfo.CurrentCulture)))
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Returns whether a match is located within a string.
        /// </summary>
        /// <param name="text">The string to locate the match within.</param>
        /// <param name="searchText">The string to search for.</param>
        /// <returns>Whether a match was located.</returns>
        public static bool FindMatch(string text, string searchText)
        {
            string[] symbols = new string[] { "(", ")", "[", "]", "-", "+", "&" };
            foreach (string symbol in symbols)
            {
                if (text.Replace(symbol, string.Empty).Contains(searchText) || text.Replace(symbol, " ").Contains(searchText))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks to see if a string contains at least one word with a minimum character count.
        /// </summary>
        /// <param name="text">The string the check.</param>
        /// <param name="minimumCharacters">The minimum character count.</param>
        /// <returns>Whether the string contains at least one word with a minimum number of characters.</returns>
        public static bool ContainsWordWithMinimumCharacters(string text, int minimumCharacters)
        {
            string[] parts = text.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
            {
                if (part.Length >= minimumCharacters)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets matches from a list of drugs containing a StartsWith match.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="drugs">The drugs to search.</param>
        /// <param name="ignoreDrugs">The drugs to ignore.</param>
        /// <param name="hasGeneric">Whether drugs with generics are included.</param>
        /// <param name="hasBrand">Whether drugs with a brand are included.</param>
        /// <returns>An array of matches.</returns>
        private static Drug[] GetMatches(string searchText, Drug[] drugs, Drug[] ignoreDrugs, bool hasGeneric, bool hasBrand)
        {
            Drug[] matches = (from drug in drugs
                              where !ignoreDrugs.Contains(drug) &&
                              ((hasGeneric && !hasBrand) ? string.IsNullOrEmpty(drug.BrandName) && !string.IsNullOrEmpty(drug.Name) && DrugSearchHelper.ContainsStartsWith(drug.Name, searchText) :
                              (hasGeneric && hasBrand) ? !string.IsNullOrEmpty(drug.BrandName) && !string.IsNullOrEmpty(drug.Name) && DrugSearchHelper.ContainsStartsWith(drug.BrandName, searchText) :
                              (!hasGeneric && hasBrand) ? !string.IsNullOrEmpty(drug.BrandName) && string.IsNullOrEmpty(drug.Name) && DrugSearchHelper.ContainsStartsWith(drug.BrandName, searchText) :
                              false)
                              select drug).OrderBy(a => (!hasBrand) ? a.Name != null ? a.Name.Replace("-", "0") : a.Name : a.BrandName != null ? a.BrandName.Replace("-", "0") : a.BrandName).ThenBy(a => a.Name != null ? a.Name.Replace("-", "0") : a.Name).ToArray();

            return matches;
        }

        /// <summary>
        /// Gets matches from a list of drugs containing a StartsWith match where the match is in a specific position.
        /// </summary>
        /// <param name="matchPosition">The desired position of the match.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="drugs">The drugs to search.</param>
        /// <param name="ignoreDrugs">The drugs to ignore.</param>
        /// <param name="hasGeneric">Whether drugs with generics are included.</param>
        /// <param name="hasBrand">Whether drugs with a brand are included.</param>
        /// <returns>An array of matches.</returns>
        private static Drug[] GetMatches(int matchPosition, string searchText, Drug[] drugs, Drug[] ignoreDrugs, bool hasGeneric, bool hasBrand)
        {
            Drug[] matches = (from drug in drugs
                              where !ignoreDrugs.Contains(drug) &&
                              ((hasGeneric && !hasBrand) ? string.IsNullOrEmpty(drug.BrandName) && !string.IsNullOrEmpty(drug.Name) && DrugSearchHelper.ContainsStartsWithInPosition(drug.Name.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries), searchText, matchPosition) :
                              (hasGeneric && hasBrand) ? !string.IsNullOrEmpty(drug.BrandName) && !string.IsNullOrEmpty(drug.Name) && DrugSearchHelper.ContainsStartsWithInPosition(drug.BrandName.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries), searchText, matchPosition) :
                              (!hasGeneric && hasBrand) ? !string.IsNullOrEmpty(drug.BrandName) && string.IsNullOrEmpty(drug.Name) && DrugSearchHelper.ContainsStartsWithInPosition(drug.BrandName.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries), searchText, matchPosition) :
                              false)
                              select drug).OrderBy(a => (!hasBrand) ? a.Name != null ? a.Name.Replace("-", "0") : a.Name : a.BrandName != null ? a.BrandName.Replace("-", "0") : a.BrandName).ThenBy(a => a.Name != null ? a.Name.Replace("-", "0") : a.Name).ToArray();
            return matches;
        }    

        /// <summary>
        /// Gets a set of matches that have a specific ingredient count.
        /// </summary>        
        /// <param name="ingredientCount">The desired number of ingredients.</param>
        /// <param name="drugs">The drugs to search.</param>
        /// <param name="ignoreDrugs">The drugs to ignore.</param>
        /// <param name="hasGeneric">Whether drugs with generics are included.</param>
        /// <param name="hasBrand">Whether drugs with a brand are included.</param>
        /// <returns>An array of matches.</returns>
        private static Drug[] GetMatches(int ingredientCount, Drug[] drugs, Drug[] ignoreDrugs, bool hasGeneric, bool hasBrand)
        {
            if (hasGeneric && !hasBrand)
            {
                Drug[] matches = (from drug in drugs
                                  where drug.Name.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length == ingredientCount
                                  select drug).OrderBy(a => (!hasBrand) ? a.Name != null ? a.Name.Replace("-", "0") : a.Name : a.BrandName != null ? a.BrandName.Replace("-", "0") : a.BrandName).ThenBy(a => a.Name != null ? a.Name.Replace("-", "0") : a.Name).ToArray();

                return matches;
            }
            else
            {
                return (from drug in drugs
                        where !ignoreDrugs.Contains(drug)
                        select drug).OrderBy(a => (!hasBrand) ? a.Name != null ? a.Name.Replace("-", "0") : a.Name : a.BrandName != null ? a.BrandName.Replace("-", "0") : a.BrandName).ThenBy(a => a.Name != null ? a.Name.Replace("-", "0") : a.Name).ToArray();
            }
        }

        /// <summary>
        /// Gets matches from a list of drugs containing a StartsWith match where the match is in a specific position, in a specific ingredient position.
        /// </summary>
        /// <param name="matchPosition">The desired position of the match.</param>
        /// <param name="ingredientPosition">The desired position of the ingredient.</param>
        /// <param name="searchText">The search text.</param>
        /// <param name="drugs">The drugs to search.</param>
        /// <param name="ignoreDrugs">The drugs to ignore.</param>
        /// <param name="hasGeneric">Whether drugs with generics are included.</param>
        /// <param name="hasBrand">Whether drugs with a brand are included.</param>
        /// <returns>An array of matches.</returns>
        private static Drug[] GetMatches(int matchPosition, int ingredientPosition, string searchText, Drug[] drugs, Drug[] ignoreDrugs, bool hasGeneric, bool hasBrand)
        {
            Drug[] matches = (from drug in drugs
                              where !ignoreDrugs.Contains(drug) &&
                              ((hasGeneric && !hasBrand) ? string.IsNullOrEmpty(drug.BrandName) && !string.IsNullOrEmpty(drug.Name) && (drug.Name.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length > ingredientPosition && drug.Name.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[ingredientPosition].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length > matchPosition && DrugSearchHelper.StartsWith(drug.Name.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[ingredientPosition].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[matchPosition], searchText)) :
                              (hasGeneric && hasBrand) ? !string.IsNullOrEmpty(drug.BrandName) && !string.IsNullOrEmpty(drug.Name) && (drug.BrandName.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length > ingredientPosition && drug.BrandName.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[ingredientPosition].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length > matchPosition && DrugSearchHelper.StartsWith(drug.BrandName.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[ingredientPosition].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[matchPosition], searchText)) :
                              (!hasGeneric && hasBrand) ? !string.IsNullOrEmpty(drug.BrandName) && string.IsNullOrEmpty(drug.Name) && (drug.BrandName.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length > ingredientPosition && drug.BrandName.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[ingredientPosition].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length > matchPosition && DrugSearchHelper.StartsWith(drug.BrandName.Split("+".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[ingredientPosition].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[matchPosition], searchText)) :
                              false)
                              select drug).OrderBy(a => (!hasBrand) ? a.Name != null ? a.Name.Replace("-", "0") : a.Name : a.BrandName != null ? a.BrandName.Replace("-", "0") : a.BrandName).ThenBy(a => a.Name != null ? a.Name.Replace("-", "0") : a.Name).ToArray();

            return matches;
        }
    }
}
