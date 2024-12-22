// -
using System;
using System.Collections.Generic;
using System.IO;

using WorkWithCollections.TextTools;
using WorkWithCollections.Utilities;


namespace WorkWithCollections.App;

/// <summary>
/// Определяем 10 слов чаще всего встречающихся в тексте
/// </summary>
internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Get most common words in the text");
        Console.WriteLine("---");
        int result = 0;

        (int outResult, string text) = GetText();
        result += outResult;

        if (result == 0)
        {
            TextAnalyser textAnalyser = new TextAnalyser(text);
            List<string> mostCommonWords = textAnalyser.GetMostCommonWords();

            foreach (string w in mostCommonWords)
            {
                Console.WriteLine($"{w}");
            }
        }

        Console.WriteLine("---");
        Console.WriteLine($"return: [{result}]");
    }


    /// <summary>
    /// Функция получения содержимого текста
    /// </summary>
    /// <returns></returns>
    internal static (int result, string text) GetText()
    {
        int result = 0;
        string text = string.Empty;

        string workingFolder = DirectoryUtil.GetRootForLibrary("work");

        if (!string.IsNullOrWhiteSpace(workingFolder))
        {
            workingFolder = Path.Combine(workingFolder, "work");
        }
        else
        {
            workingFolder = @"C:\Temp\Downloads";
            Directory.CreateDirectory(workingFolder);
        }

        string sFilePath = Path.Combine(workingFolder, "Text1.txt");

        if (!string.IsNullOrWhiteSpace(sFilePath) && File.Exists(sFilePath))
        {
            text = File.ReadAllText(sFilePath);
        }
        else
        {
            result = 1;
        }

        return (result, text);
    }
}
