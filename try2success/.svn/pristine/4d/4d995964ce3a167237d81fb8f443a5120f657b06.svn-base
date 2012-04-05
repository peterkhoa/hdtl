using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
namespace Library.Content_analizer
{

#region ... Copyright Notice ...
/*
   Copyright 2006-2008 Provo Labs    http://www.provolabs.com

	Author: Tyler Jensen    http://www.tsjensen.com/blog
	Released by the author with written permission of Provo Labs

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */
#endregion




	public class TermExtractor
	{
		int m_wordCount = 0;
		public int WordCount { get { return m_wordCount; } set { m_wordCount = value; } }

		public TermExtractor()
		{
			m_wordCount = 0;
		}

		public Dictionary<string, decimal> ExtractX2Values(string content)
		{
			m_wordCount = 0;
            content = content.ToLower();
          content =    content.Replace(","," ");
          content = content.Replace(".", " ");
          content = content.Replace(":", " ");
          content = content.Replace("(", " ");
          content = content.Replace(")", " ");
          content = content.Replace("!", " ");
          content = content.Replace("@", " ");
          content = content.Replace("?", " ");
            

			ProcessForTitles(content);
			WordStripper ws = new WordStripper();
			List<string> rawparas = ws.StripToParagraphs(content, out m_wordCount);
			PrepareDenseParagraphSentences(rawparas);
			return Extract();
		}

		private Dictionary<string, decimal> Extract()
		{
			/*	The paragraphs KeyTerm collection is complete. Process extraction algorithm here.
			 * 
			 * The algorithm used here was found in a paper published by the
			 * International Journal of Artificial Intelligence Tools 
			 * Copyright 2003 (C) World Scientific Publishing Company
			 * Authored by Yutaka Matsuo of the National Institute of Advanced Industrial 
			 * Science and Technology in Tokyo, Japan and by Mitsuru Ishizuka of the University of Tokyo
			 * and published either in July or December of 2003 (unsure)
			 * 
			 * G = set of most frequent terms	  --> termsG = a subset of probabilityTerms where probability is above the average
			 * 
			 * Pg = sum of the total number of terms in sentences where             Dictionary<string, decimal> termPg
			 *      g appears divided by the total number of terms in the document
			 * 
			 * nw = total number of terms in sentences where w appears	 --> termNw
			 * 
			 * Fwg = sentence count where w and g occur divided by the total number of sentences
			 *       = termFwg is Dictionary<string, Dictionary<string, decimal>>
			 * 
			 * X2(w) is the rank for a give word w
			 * 
			 * X2(w) = sum of Z for each g in G (g = term in G or most frequent terms)
			 *         EXCEPT for the MAX g -- to create what the authors call robustness
			 * 
			 * D = nw * Pg
			 * 
			 * T = (Fwg - D)
			 * 
			 * Z = (T * T) / D
			 * 
			 * X2(w) = calculate Z for each g for w and sum the total
			 *         EXCEPT for the MAX g -- to create what the authors call robustness
			 * 
			*/

			//run through each sentence and grab two and three word segments and add them to the termCount
			this.AddMultiWordTerms();
			//LabLogger.Instance.Write("TermExtractor AddMultiWordTerms ran successfully.", 411, 01, LoggingCategory.All);

			this.SortTermsIntoProbabilities();	  //this gets us termsG for frequent terms, and an initialized termsX2
			//LabLogger.Instance.Write("TermExtractor SortTermsIntoProbabilities ran successfully.", 411, 01, LoggingCategory.All);

			this.FillTermPgNwCollections();   //now we have termPg and termNw filled with values
			//LabLogger.Instance.Write("TermExtractor FillTermPgNwCollections ran successfully.", 411, 01, LoggingCategory.All);

			this.FillTermFwgCollection();		//now we have the termFgw collection filled
			//LabLogger.Instance.Write("TermExtractor FillTermFwgCollection ran successfully.", 411, 01, LoggingCategory.All);

			string[] terms = new string[termsG.Count];
			termsG.Values.CopyTo(terms, 0);  //gives terms array where last term is the MAX g in G
			foreach (string w in terms)
			{
				decimal sumZ = 0;
				for (int i = 0; i < terms.Length - 1; i++) //do calcs for all but MAX
				{
					string g = terms[i];
					if (w != g) //skip where on the diagonal
					{
						int nw = termNw[w];
						decimal Pg = termPg[g];
						decimal D = nw * Pg;
						if (D != 0.0m)
						{
							decimal Fwg = termFwg[w][terms[i]];
							decimal T = Fwg - D;
							decimal Z = (T * T) / D;
							sumZ += Z;
						}
					}
				}
				termsX2[w] = sumZ;
			}

			SortedDictionary<decimal, string> sortedX2 = new SortedDictionary<decimal, string>();
			foreach (KeyValuePair<string, decimal> pair in termsX2)
			{
				decimal x2 = pair.Value;
				while (sortedX2.ContainsKey(x2))
				{
					x2 = x2 - 0.00001m;
				}
				sortedX2.Add(x2, pair.Key);
			}

			//now get simple array of values as lowest to highest X2 terms
			string[] x2Terms = new string[sortedX2.Count];
			sortedX2.Values.CopyTo(x2Terms, 0);

			Dictionary<string, decimal> preres = new Dictionary<string, decimal>();
			for (int i = x2Terms.Length - 1; i > -1; i--)
			{
				string stemterm = x2Terms[i];
				string term = GetTermFromStemTerm(stemterm);
				preres.Add(term, termsX2[x2Terms[i]]);
			}

			//post process title case and caseSpecial words
			//titles = new Dictionary<string, int>();
			//caselist = new Dictionary<string, int>();
			//caseListWords -- so we don't have to regex slit the caselist words
			//for now, case list is going to be left alone since we split those and added them to the sentence end for ranking
			SortedDictionary<decimal, string> tsort = new SortedDictionary<decimal, string>();
			foreach (KeyValuePair<string, int> title in titles)
			{
				decimal tscore = 0.0m;
				MatchCollection mc = wordreg.Matches(title.Key);
				foreach (Match m in mc)
				{
					if (preres.ContainsKey(m.Value))
					{
						tscore += preres[m.Value];
					}
				}
				while (tsort.ContainsKey(tscore))
				{
					tscore = tscore - 0.00001m;
				}
				tsort.Add(tscore, title.Key);
			}

			//mix tsort with preres and return the top 50
			foreach (KeyValuePair<string, decimal> pre in preres)
			{
				decimal x = pre.Value;
				while (tsort.ContainsKey(x))
				{
					x = x - 0.00001m;
				}
				tsort.Add(x, pre.Key);
			}

			Dictionary<string, decimal> result = new Dictionary<string, decimal>();
			string[] resultTerms = new string[tsort.Count];
			tsort.Values.CopyTo(resultTerms, 0);
			decimal[] resultValues = new decimal[tsort.Count];
			tsort.Keys.CopyTo(resultValues, 0);
			int max = 0;
			for (int i = resultTerms.Length - 1; i > -1; i--)
			{
				if (!result.ContainsKey(resultTerms[i]))
				{
					result.Add(resultTerms[i], resultValues[i]);
				}
				//if (max > 50) break;
				max++;
			}

			return result;
		}

		private string GetTermFromStemTerm(string term)
		{
			if (term.IndexOf(" ") > -1)
			{
				string[] terms = term.Split(' ');
				string[] words = new string[terms.Length];
				for (int i = 0; i < terms.Length; i++)
				{
					words[i] = GetTermFromStem(terms[i]);
				}
				string retval = string.Join(" ", words);
				return retval;
			}
			else
			{
				return GetTermFromStem(term);
			}
		}

		private string GetTermFromStem(string stem)
		{
			if (stems.ContainsKey(stem))
			{
				Dictionary<string, int> words = stems[stem];
				string word = string.Empty;
				int count = 0;
				foreach (KeyValuePair<string, int> pair in words)
				{
					if (pair.Value > count)
					{
						word = pair.Key;
						count = pair.Value;
					}
				}
				return word;
			}
			else
				return string.Empty;
		}

		private void FillTermFwgCollection()
		{
			//termFwg
			// * Fwg = sentence count where w and g occur divided by the total number of sentences (sentenceCount)
			// *       = termFwg is Dictionary<string, Dictionary<string, decimal>>

			string[] terms = new string[termsG.Count];
			termsG.Values.CopyTo(terms, 0);
			foreach (string w in terms)
			{
				foreach (KeyValuePair<decimal, string> pair in termsG)
				{
					string g = pair.Value;
					if (g != w)
					{
						int sentCountWG = 0;
						foreach (List<List<KeyTerm>> paragraph in paragraphs)
						{
							foreach (List<KeyTerm> sentence in paragraph)
							{
								if (this.TermsCoOccur(sentence, w, g)) sentCountWG++;
							}
						}
						decimal Fwg = sentCountWG > 0 ? sentCountWG / (decimal)sentenceCount : 0.0m;
						if (!termFwg.ContainsKey(w))
							termFwg.Add(w, new Dictionary<string, decimal>()); //add if not there yet
						termFwg[w].Add(g, Fwg);
					}
				}
			}
		}

		private bool TermsCoOccur(List<KeyTerm> sentence, string w, string g)
		{
			if (TermInSentence(sentence, w) && TermInSentence(sentence, g))
				return true;
			else
				return false;
		}

		private bool TermInSentence(List<KeyTerm> sentence, string term)
		{
			bool found = false;
			//if term appears in this sentence, count the terms (words + 2 and 3 word terms)
			if (term.IndexOf(" ") > -1)
			{
				string[] termWords = term.Split(' ');
				for (int i = 0; i < sentence.Count; i++)
				{
					KeyTerm t = sentence[i];
					if (termWords.Length == 2 && i > 2)
					{
						KeyTerm t1 = sentence[i - 1];
						if (termWords[0] == t1.Stem && termWords[1] == t.Stem)
						{
							found = true;
							break;
						}
					}
					else if (termWords.Length == 3 && i > 3)
					{
						KeyTerm t1 = sentence[i - 1];
						KeyTerm t2 = sentence[i - 2];
						if (termWords[0] == t2.Stem && termWords[1] == t1.Stem && termWords[2] == t.Stem)
						{
							found = true;
							break;
						}
					}
				}
			}
			else
			{
				for (int i = 0; i < sentence.Count; i++)
				{
					KeyTerm t = sentence[i];
					if (t.Stem == term)
					{
						found = true;
						break;
					}
				}
			}
			return found;
		}

		private void FillTermPgNwCollections()
		{
			//termPg
			// * Pg = sum of the total number of terms in sentences where             Dictionary<string, decimal> termPg
			// *      g appears divided by the total number of terms in the document (termTotal)
			// total number of terms in sentence = word count + # of 2 and 3 word combos = termsInSentencesForTerm

			foreach (KeyValuePair<decimal, string> pair in termsG)
			{
				string term = pair.Value;
				int termsInSentencesForTerm = 0;
				foreach (List<List<KeyTerm>> paragraph in paragraphs)
				{
					foreach (List<KeyTerm> sentence in paragraph)
					{
						bool found = false;
						//if term appears in this sentence, count the terms (words + 2 and 3 word terms)
						if (term.IndexOf(" ") > -1)
						{
							string[] termWords = term.Split(' ');
							for (int i = 0; i < sentence.Count; i++)
							{
								KeyTerm t = sentence[i];
								if (termWords.Length == 2 && i > 2)
								{
									KeyTerm t1 = sentence[i - 1];
									if (termWords[0] == t1.Stem && termWords[1] == t.Stem)
									{
										found = true;
										break;
									}
								}
								else if (termWords.Length == 3 && i > 3)
								{
									KeyTerm t1 = sentence[i - 1];
									KeyTerm t2 = sentence[i - 2];
									if (termWords[0] == t2.Stem && termWords[1] == t1.Stem && termWords[2] == t.Stem)
									{
										found = true;
										break;
									}
								}
							}
						}
						else
						{
							for (int i = 0; i < sentence.Count; i++)
							{
								KeyTerm t = sentence[i];
								if (t.Stem == term)
								{
									found = true;
									break;
								}
							}
						}
						if (found)
						{
							//now get terms count (words + 2 and 3 word terms) and increment termsInSentencesForTerm
							termsInSentencesForTerm += sentence.Count;
							if (sentence.Count > 2) termsInSentencesForTerm += sentence.Count - 2; //all three word terms
							if (sentence.Count > 1) termsInSentencesForTerm += sentence.Count - 1; //all two word terms
						}
					}
				}
				termNw.Add(term, termsInSentencesForTerm);
				decimal pg = termsInSentencesForTerm / (decimal)this.termTotal;
				termPg.Add(term, pg);
			} //end foreach in termsG
		}

		private void SortTermsIntoProbabilities()
		{
			foreach (KeyValuePair<string, int> pair in termOccurrenceCounts)
			{
				termTotal += pair.Value;
			}
			decimal total = (decimal)termTotal;
			decimal probTotal = 0; //to be used for calculating the average probability
			foreach (KeyValuePair<string, int> pair in termOccurrenceCounts)
			{
				decimal prob = pair.Value / total;
				probTotal += prob;
				while (probabilityTerms.ContainsKey(prob))
				{
					prob = prob - 0.00001m; //offset by the slightest amount to get unique key
				}
				probabilityTerms.Add(prob, pair.Key);
			}
			decimal probAvg = termOccurrenceCounts.Count > 0 ? probTotal / termOccurrenceCounts.Count : 0;

			//only take the top 10% up to the top 30 terms and if top 10% is less than 10 then take up to 5
			int toptenCount = termOccurrenceCounts.Count / 10;
			if (toptenCount > 30)
				toptenCount = 30;
			else if (toptenCount < 10)
				toptenCount = 5;

			if (toptenCount > termOccurrenceCounts.Count) toptenCount = termOccurrenceCounts.Count; //just in case there are so few

			decimal[] ptkey = new decimal[probabilityTerms.Count];
			probabilityTerms.Keys.CopyTo(ptkey, 0);

			for (int i = ptkey.Length - 1; i > ptkey.Length - toptenCount - 1; i--)
			{
				decimal key = ptkey[i];
				string val = probabilityTerms[key];
				termsG.Add(key, val);
				termsX2.Add(val, 0); //initializes the list for storing X2 calculation results to be sorted later
			}

			//foreach (KeyValuePair<decimal, string> pair in probabilityTerms)
			//{
			//   if (pair.Key >= probAvg)
			//   {
			//      termsG.Add(pair.Key, pair.Value);
			//      termsX2.Add(pair.Value, 0); //initializes the list for storing X2 calculation results to be sorted later
			//   }
			//}
		}

		private void AddMultiWordTerms()
		{
			foreach (List<List<KeyTerm>> paragraph in paragraphs)
			{
				foreach (List<KeyTerm> sentence in paragraph)
				{
					for (int i = 0; i < sentence.Count; i++)
					{
						KeyTerm t = sentence[i];
						if (i > 0) //we can have a two word phrase
						{
							KeyTerm tm1 = sentence[i - 1];
							string term = tm1.Stem + " " + t.Stem;
							this.AddTerm(term);
						}
						if (i > 1) //we can have a three word phras
						{
							KeyTerm tm1 = sentence[i - 1];
							KeyTerm tm2 = sentence[i - 2];
							string term = tm2.Stem + " " + tm1.Stem + " " + t.Stem;
							this.AddTerm(term);
						}
					}
				}
			}
		}

		private void AddTerm(string stem)
		{
			if (termOccurrenceCounts.ContainsKey(stem))
				termOccurrenceCounts[stem]++;
			else
				termOccurrenceCounts.Add(stem, 1);
		}

		public List<List<List<KeyTerm>>> Paragraphs { get { return paragraphs; } }
		public Dictionary<string, Dictionary<string, int>> Stems { get { return stems; } }
		public Dictionary<string, int> TermCount { get { return termOccurrenceCounts; } }


		//private static Regex sentdiv = new Regex(@"(?<=(^|\s))((\S.*)(\.|\?|!))(?:(\s|$))", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex sentdiv = new Regex(@"(\.{0,1}[a-z0-9].*?(?=(\.|\?|!|$)(\]|\)|\s|$)))", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private void PrepareDenseParagraphSentences(List<string> rawparas)
		{
			/*
			 * remove all non-essential words: pronouns, helper verbs (to be forms, propositions, a, an, the, conjunctions
			 * 
			 * split text into sentences ( . ? ! ) and into words stemming each and adding
			 * to sentences, stems, and termCount (total occurence)
			*/
			//LabLogger.Instance.Write("TermExtractor PrepareTextToDenseParagraphs ran successfully with rawparas = " + rawparas.Count.ToString(), 411, 01, LoggingCategory.All);
			foreach (string rawpara in rawparas)
			{
				if (rawpara.Trim(trim).Length > 2) //ignore empty paragraphs
				{
					List<List<KeyTerm>> sentlist = new List<List<KeyTerm>>();
					//string[] sents = rawpara.Split(endsent);

					MatchCollection mcsent = sentdiv.Matches(rawpara);
					string[] sents = new string[mcsent.Count];
					int i = 0;
					foreach (Match ms in mcsent)
					{
						sents[i] = ms.Value;
						i++;
					}

					foreach (string s in sents)
					{
						if (s.Trim(trim).Length > 2)
						{
							//look for title case phrase and add to titles collection???
							string fxs = ProcessSpecialCase(s);

							//add individual words from this sentence
							List<KeyTerm> words = new List<KeyTerm>();
							//MatchCollection mc = wordreg.Matches(fxs);
							//foreach (Match m in mc)
                            string [] ws = fxs.Split(' ');
                            foreach (string word in ws)
							{
								//string word = m.Value.Trim(trim);
                               // string word = m.Value;
								if (word.Length > 2 || WordIsUncommon(word))	 //all two and one letter words are ignored
								{
									string stem = (word.Length > 2) ? stemmer.Porter.stemTerm(word) : word; //only stem if more than 2 characters
									KeyTerm term = new KeyTerm(word, stem);
									if (stems.ContainsKey(stem))
									{
										if (stems[stem].ContainsKey(word))
											stems[stem][word]++;
										else
											stems[stem].Add(word, 1);
									}
									else
									{
										stems.Add(stem, new Dictionary<string, int>());
										stems[stem].Add(word, 1);
									}
									this.AddTerm(stem);
									words.Add(term);
								}
							}
							if (words.Count > 0) //only add if we have words in the sentence
							{
								sentlist.Add(words);
								sentenceCount++; //increment total sentence count to be used later
							}
						}
					}
					if (sentlist.Count > 0) //only add paragraph if there are sentences
					{
						paragraphs.Add(sentlist);
					}
				}
			}
		}

		private string ProcessSpecialCase(string s)
		{
			//check each word for camelCase and TitleCase and add to special caselist split them into individual words
			//if so, add the split words to the sentence just after the combined word 

			MatchCollection mcc = regcase.Matches(s);
			foreach (Match m in mcc)
			{
				if (!caselist.ContainsKey(m.Value)) caselist.Add(m.Value, 0);
				caselist[m.Value]++;

				//split into individual words and add to sentence
				MatchCollection smc = splitcase.Matches(m.Value);
				foreach (Match inm in smc)
				{
					if (!caseListWords.ContainsKey(m.Value)) caseListWords.Add(m.Value, new List<string>());
					caseListWords[m.Value].Add(inm.Value);
					s += " " + inm.Value;
				}
			}
			return s;
		}

		private void ProcessForTitles(string s)
		{
			//check for title case in sentence and add to titles for post processing
			MatchCollection mc = regtitle.Matches(s);
			foreach (Match m in mc)
			{
				if (!titles.ContainsKey(m.Value)) titles.Add(m.Value, 0);
				titles[m.Value]++;
			}
		}

		private Dictionary<string, int> titles = new Dictionary<string, int>();
		private Dictionary<string, int> caselist = new Dictionary<string, int>();
		private Dictionary<string, List<string>> caseListWords = new Dictionary<string, List<string>>();
		private static Regex regtitle = new Regex(@"(?<=(\s|^))"
															 + @"[A-Z\.0-9][A-Za-z0-9]*?[\.\-]*[A-Za-z0-9]+?"
															 + @"((\s[a-z]{1,3}){0,2}\s[A-Z\.0-9][A-Za-z0-9]*?[\.\-]*[A-Za-z0-9]+?){1,4}"
															 + @"(?=(\.|\?|!|\s|$))", (RegexOptions.Compiled | RegexOptions.Multiline));
		private static Regex regcase = new Regex(@"(?<=(\s|^))"
															+ @"[A-Za-z]{1}[a-z\.0-9]+?[A-Z]{1}[A-Za-z\.0-9]+?"
															+ @"(?=(\s|$))", (RegexOptions.Compiled));

		private static Regex splitcase = new Regex(@"(^|[A-Z\.])[a-z0-9]+?(?=(\.|[A-Z]|$))", (RegexOptions.Compiled));

		//private static Regex regtitle = new Regex(@"((?<=(\s|^))[A-Z\.0-9]+[\S\.\-]*?)(\s+((((?<=(\s|^))[A-Z\.0-9]+[\S\.\-]*?))|(((?<=(\s|^))[\S\.\-]*{1,4}))))+((?<=(\s|^))[A-Z\.0-9]+[\S\.\-]*?)", (RegexOptions.Compiled));
		//((?<=(\s|^))[A-Z\.0-9]+[\S\.\-]*?)
		//(?<=(\s|^))[\S\.\-]*{1,4}

		private static Regex regtwo = new Regex(@"am|an|as|at|ax|be|by|do|go|he|if|in|is|it|me|my|no|of|on|or|ox|so|to|up|us|we|a|i", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private bool WordIsUncommon(string word)
		{
			word = word.Trim();
			if (word.Length < 2) return false;
			return (regtwo.IsMatch(word) == false); //a match means it is common
		}

		private Dictionary<string, Dictionary<string, decimal>> termFwg = new Dictionary<string, Dictionary<string, decimal>>();
		private Dictionary<string, int> termNw = new Dictionary<string, int>();
		private Dictionary<string, decimal> termPg = new Dictionary<string, decimal>();
		private Dictionary<string, decimal> termsX2 = new Dictionary<string, decimal>();
		private SortedDictionary<decimal, string> termsG = new SortedDictionary<decimal, string>();
		private SortedDictionary<decimal, string> probabilityTerms = new SortedDictionary<decimal, string>();
		private Stemmer stemmer = new Stemmer();
		private List<List<List<KeyTerm>>> paragraphs = new List<List<List<KeyTerm>>>();
		private Dictionary<string, Dictionary<string, int>> stems = new Dictionary<string, Dictionary<string, int>>(); //stem, words stemmed to it and the count
		private Dictionary<string, int> termOccurrenceCounts = new Dictionary<string, int>();
		private int termTotal = 0;
		//private HtmlDocument doc;
		private int sentenceCount = 0;
		//private static Regex wordreg = new Regex(@"\b\S+\b", RegexOptions.Compiled);
		//private static Regex wordreg = new Regex(@"(?<=(\s|^))([\S\.\-]+)(?=())", RegexOptions.Compiled);
		//private static Regex wordreg = new Regex(@"(?<=(\s|^))[A-Z0-9\.\-]+", (RegexOptions.Compiled | RegexOptions.IgnoreCase));
        private static Regex wordreg = new Regex(@"(?<=(\s|^))[.]+", (RegexOptions.Compiled | RegexOptions.IgnoreCase));

		private static char[] endsent = new char[3] { '.', '!', '?' };
		private static char[] trim = new char[11] { (char)247, ' ', '\t', '\n', '\r', '[', ']', '(', ')', '{', '}' };	//trim word
	}

	public struct KeyTerm
	{
		public string Term;
		public string Stem;
		public KeyTerm(string term, string stem)
		{
			this.Term = term;
			this.Stem = stem;
		}
	}

	public class WordStripper
	{
		private static char[] p = new char[1] { (char)247 };
		private static string ps = null;
		public List<string> StripToParagraphs(string text, out int wordCount)
		{
			wordCount = 0;
			ps = new string(p);
			//LabLogger.Instance.Write("StripToParagraphs text = " + text, 411, 01, LoggingCategory.All);
			List<string> retval = new List<string>();
			try
			{
				MatchCollection mc = RegWordCount.Matches(text);
				wordCount = mc.Count;
				text = para.Replace(text, ps); //replace decimal CRLF with a acsii 01 for later splitting
				text = crlftab.Replace(text, " "); //replace remaining line breaks with simple space
				//remove all right ' for finding it's 
				text = rsquote.Replace(text, "'");
				//pronouns, helper verbs (to be forms, prepositions, a, an, the, conjunctions

				//LabLogger.Instance.Write("StripToParagraphs nonwords called text = " + text, 411, 01, LoggingCategory.All);
				//text = nonwords.Replace(text, "");
				text = non1.Replace(text, "");
				text = non2.Replace(text, "");
				text = non3.Replace(text, "");
				text = non4.Replace(text, "");
				text = non5.Replace(text, "");
				text = non6.Replace(text, "");
				text = non7.Replace(text, "");
				text = non8.Replace(text, "");
				text = non9.Replace(text, "");
				text = non10.Replace(text, "");
				text = non11.Replace(text, "");
				text = non12.Replace(text, "");
				text = non13.Replace(text, "");
				text = non14.Replace(text, "");
				text = non15.Replace(text, "");
				text = non16.Replace(text, "");
				text = non17.Replace(text, "");
				text = non18.Replace(text, "");
				text = non19.Replace(text, "");
                text = nonvietnamese.Replace(text, "");
				//remove large pockets of whitespace and replace with single space
				//LabLogger.Instance.Write("StripToParagraphs white called text = " + text, 411, 01, LoggingCategory.All);
				text = white.Replace(text, " ");

				//LabLogger.Instance.Write("StripToParagraphs split called text = " + text, 411, 01, LoggingCategory.All);
				string[] paras = text.Split(p);

				retval = new List<string>(paras);
			}
			catch (Exception e)
			{
				throw e;
			}
			return retval;
		}
		public static Regex RegWordCount = new Regex(@"\b\S+?\b", RegexOptions.Compiled);
		private static Regex para = new Regex("\r\n\r\n|\n\n", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex crlftab = new Regex("(\r\n|\t)|(\n|\t)", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex lsquote = new Regex(@"\u2018", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex rsquote = new Regex(@"\u2019", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex ldquote = new Regex(@"\u201C", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex rdquote = new Regex(@"\u201D", (RegexOptions.IgnoreCase | RegexOptions.Compiled));


        private static Regex non1 = new Regex(@"\b(nbsp;|;|,|a|aboard|about|above|absent|according\sto|across|after|against|ago|ahead\sof|ain't|all|along|alongside)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non2 = new Regex(@"\b(also|although|am|amid|amidst|among|amongst|an|and|anti|anybody|anyone|anything|apart|apart\sfrom|are|been)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non3 = new Regex(@"\b(aren't|around|as|as\sfar\sas|as\ssoon\sas|as\swell\sas|aside|at|atop|away|be|because|because\sof|before)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non4 = new Regex(@"\b(behind|below|beneath|beside|besides|between|betwixt|beyond|but|by|by\smeans\sof|by\sthe\stime|can|cannot)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non5 = new Regex(@"\b(circa|close\sto|com|concerning|considering|could|couldn't|cum|'d|despite|did|didn't|do|does|doesn't|don't)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non6 = new Regex(@"\b(down|due\sto|during|each_other|'em|even\sif|even\sthough|ever|every|every\stime|everybody|everyone)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non7 = new Regex(@"\b(everything|except|far\sfrom|few|first\stime|following|for|from|get|got|had|hadn't|has|hasn't|have)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non8 = new Regex(@"\b(haven't|he|hence|her|here|hers|herself|him|himself|his|how|i|if|in|in\saccordance\swith|in\saddition\sto|in\scase)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non9 = new Regex(@"\b(in\sfront\sof|in\slieu\sof|in\splace\sof|in\sspite\sof|in\sthe\sevent\sthat|in\sto|inside|inside\sof)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non10 = new Regex(@"\b(instead\sof|into|is|isn't|it|itself|just\sin\scase|like|'ll|lots|may|me|mid|might|mightn't|mine|more|most)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non11 = new Regex(@"\b(must|mustn't|myself|near|near\sto|nearest|no|no\sone|nobody|none|not|nothing|notwithstanding|now\sthat|of)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non12 = new Regex(@"\b(off|on|on\sbehalf\sof|on\sto|on\stop\sof|once|one|one\sanother|only\sif|onto|opposite|or|org|other|our|any)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non13 = new Regex(@"\b(ours|ourselves|out|out\sof|outside|outside\sof|over|past|per|plenty|plus|prior\sto|qua|re|'re|really|set)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non14 = new Regex(@"\b(regarding|round|'s|said|sans|save|say|says|shall|shan't|she|should|shouldn't|since|so|somebody|its|only)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non15 = new Regex(@"\b(someone|something|than|that|the|thee|their|theirs|them|themselves|there|these|they|thine|this|thou)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non16 = new Regex(@"\b(though|through|throughout|till|to|toward|towards|under|underneath|unless|unlike|until|unto|up|upon|even)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non17 = new Regex(@"\b(us|'ve|versus|via|was|wasn't|we|were|weren't|what|when|whenever|where|whereas|whether\sor\snot|things)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non18 = new Regex(@"\b(while|who|whoever|whom|why|will|with|with\sregard\sto|withal|within|without|won't|would|wouldn't|mere)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
		private static Regex non19 = new Regex(@"\b(ya|ye|yes|you|your|yours|yourself)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));

//        private static Regex no1 = new Regex(@"\b()\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));
        private static Regex nonvietnamese = new Regex(@"\b(bị|các|cho|chứ|chứa|có|còn|con|của|cũng|đầy|đã|để|đó|được|gì|hay|hoặc|khi|quá|sẽ|khỏang|không|là|lại|này|mà|một|những|nhiều|nếu|nơi|rồi|sau|thì|trong|từ|và|vào|với)\b", (RegexOptions.IgnoreCase | RegexOptions.Compiled));

		private static Regex white = new Regex(@"\s+", RegexOptions.Compiled);
	}
}


