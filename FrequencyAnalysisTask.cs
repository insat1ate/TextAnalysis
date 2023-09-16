using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            //кладу в словарь биграммы
            var ngrams = FindMostFrequentNGramms(text, 2);
            //добавляю триграммы
            foreach (var trigram in FindMostFrequentNGramms(text, 3))
                ngrams.Add(trigram.Key, trigram.Value);
            return ngrams;
        }

        private static Dictionary<string, string> FindMostFrequentNGramms(List<List<string>> text, int n)
        {
            //словарь для определения частоты n-грам
            var ngramsFrequency = new Dictionary<(string, string), int>();
            //каждое предложение
            foreach (var sentence in text)
            {
                //собираю нграммы и определяю насколько часто они встречаются
                for (int i = 0; i < sentence.Count - n + 1; i++)
                {
                    //беру первую часть нграммы и преобразую в строку                         + вторая часть нграммы
                    var currNGram = (sentence.GetRange(i, n - 1).Aggregate((prev, curr) => prev + " " + curr), sentence[i + n - 1]);
                    if (!ngramsFrequency.ContainsKey(currNGram))
                        ngramsFrequency[currNGram] = 0;
                    ngramsFrequency[currNGram]++;
                }
            }
            //группирую по первой части нграммы
            var ngrams = ngramsFrequency.GroupBy(x => x.Key.Item1)
                                        //беру самый частый и минимальный лексикографически
                                        .Select(x => x.OrderByDescending(y => y.Value)
                                                      .ThenBy(z => z.Key.Item2, StringComparer.Ordinal)
                                                      .First())
                                        .ToDictionary(x => x.Key.Item1, x => x.Key.Item2);

            return ngrams;
        }
    }
}


