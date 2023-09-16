using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        static List<string> SentencesInWords(string sentences)
        {
            var word = new StringBuilder();
            var words = new List<string>();

            foreach (char ch in sentences)
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
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var sentences = new List<string>(text.ToLower().Split(new string[] { ".", "!", "?", ";", ":", "(", ")" }, StringSplitOptions.RemoveEmptyEntries));
            foreach (var item in sentences)
            {
                if (!string.IsNullOrWhiteSpace(item) && SentencesInWords(item).Capacity > 0)
                    sentencesList.Add(SentencesInWords(item));
            }
            return sentencesList;
        }
    }
}