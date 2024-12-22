//-
using System;
using System.Collections.Generic;
using System.Linq;


namespace WorkWithCollections.TextTools;

/// <summary>
/// Класс, обрабатывающий текст,
///  позволяющий определить 10 чаще всего встречающихся слов
/// </summary>
internal class TextAnalyser
{
    private string[] _words = [];
    private Dictionary<string, int> _distinctWords = [];

    internal TextAnalyser(string text)
    {
        // разделители
        char[] delimiters = [' ', '\r', '\n', '.', ',', '!', '?', ':', ';', '-', '–', '«', '»', '…', '(', ')', '[', ']'];

        // убираем символы пунктуации по подсказке к заданию
        var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
        text = noPunctuationText;

        // разбиваем текст на слова
        if (!string.IsNullOrWhiteSpace(text))
            _words = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
    }

    internal List<string> GetMostCommonWords()
    {
        List<string> res = [];

        foreach (string w in _words)
        {
            if (_distinctWords.ContainsKey(w))
            {
                _distinctWords[w]++;
            }
            else
            {
                _distinctWords.Add(w, 1);
            }
        }

        // прямое решение с помощью LINQ
        // res = _distinctWords.OrderByDescending(x => x.Value).Select(x => x.Key).Take(10).ToList();

        // в учебных целях решаем задачу, стараясь избежать LINQ
        //  (не считая вызовов [ToList])

        // descending sort
        List<int> rate = _distinctWords.Values.ToList();
        rate.Sort();
        rate.Reverse();
        rate = rate.GetRange(0, 10);
        Dictionary<string, int> resDict = [];

        // получаем список 10 используемых слов
        foreach (string key in _distinctWords.Keys)
        {
            if (rate.Contains(_distinctWords[key]))
            {
                resDict.Add(key, _distinctWords[key]);
            }
        }

        // дополнительно, выводим полученные слова в порядке убывания частоты их использования
        foreach (int i in rate)
        {
            // выполняем перечисление над копией списка ключей
            //  таким образом, можем вносить изменения в перечисляемый словарь
            foreach (string key in resDict.Keys.ToList())
            {
                if (resDict[key] == i)
                {
                    res.Add(key);
                    resDict.Remove(key);
                    break;
                }
            }
        }

        return res;
    }
}
