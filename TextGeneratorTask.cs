using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string GetNextWord(Dictionary<string, string> nextWords, string phraseBeginning, int wordsCount)
        {
            for (int i = 0; i < wordsCount; i++)
            {
                List<string> listWords = ParserSentences_PhraseBeginning(phraseBeginning);
                if (listWords.Count > 0)
                {
                    if ((listWords.Count >= 2) && nextWords.ContainsKey(listWords[listWords.Count - 2] + " " + listWords[listWords.Count - 1]))
                    {
                        phraseBeginning += " " + nextWords[listWords[listWords.Count - 2] + " " + listWords[listWords.Count - 1]];

                    }
                    else if ((listWords.Count >= 1) && nextWords.ContainsKey(listWords[listWords.Count - 1]))
                    {
                        phraseBeginning += " " + nextWords[listWords[listWords.Count - 1]];
                    }
                }
            }
            return phraseBeginning;
        }
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
               return GetNextWord(nextWords, phraseBeginning, wordsCount);
        }
        public static List<string> ParserSentences_PhraseBeginning(string phraseBeginning)
        {
            var word = new StringBuilder();
            var words = new List<string>();

            foreach (char ch in phraseBeginning)
            {
                if ((char.IsLetter(ch) || ch == '\'') && ch != ' ')
                    word.Append(ch);
                else
                    word.Append(' ');
            }
            var arrWords = word.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in arrWords)
            {
                if (!string.IsNullOrWhiteSpace(item))
                    words.Add(item);
            }
            return words;
        }
    }
}