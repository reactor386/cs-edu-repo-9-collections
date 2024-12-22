// -
using System;
using System.IO;

using WorkWithCollections.Utilities;
using WorkWithCollections.Benchmark;


namespace WorkWithCollections.App;

/// <summary>
/// Определяем производительность List, LinkedList
///  с помощью [Stopwatch], используя массив слов из текстового файла
/// </summary>
internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Stopwatch Benchmark for List, LinkedList");
        Console.WriteLine("---");
        int result = 0;

        (int outResult, string text) = GetText();
        result += outResult;

        if (result == 0)
        {
            Inserter inserter;
            (long milliSec, long numTick)[] benchmarkResult1;
            (long milliSec, long numTick)[] benchmarkResult2;

            Inserter.InsertOption[] insertOptions = [
                Inserter.InsertOption.NearLast,
                Inserter.InsertOption.NearFirst,
                Inserter.InsertOption.Middle];
            
            foreach (Inserter.InsertOption insertOption in insertOptions)
            {
                Console.WriteLine($"Insert option '{insertOption}':");
                Console.WriteLine("---");

                inserter = new Inserter(text, insertOption);

                benchmarkResult1 = EstimatorStopwatchWrapper
                    .EstimateByStopwatch(inserter.InsertInList, 5);
                
                benchmarkResult2 = EstimatorStopwatchWrapper
                    .EstimateByStopwatch(inserter.InsertInLinkedList, 5);

                Console.WriteLine("Insert In [List] results:");
                foreach ((long milliSec, long numTick) in benchmarkResult1)
                {
                    Console.WriteLine($". time: {milliSec}, ticks: {numTick}");
                }

                Console.WriteLine("---");

                Console.WriteLine("Insert In [LinkedList] results:");
                foreach ((long milliSec, long numTick) in benchmarkResult2)
                {
                    Console.WriteLine($". time: {milliSec}, ticks: {numTick}");
                }

                Console.WriteLine("---");
            }

            result = 0;
        }
        
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


/*
результаты

вставка на место предпоследнего элемента
вставка на место второго элемента
вставка в середину списка

Stopwatch Benchmark for List, LinkedList
---
Insert option 'NearLast':
---
Insert In [List] results:
. time: 5, ticks: 51975
. time: 4, ticks: 44404
. time: 7, ticks: 78187
. time: 6, ticks: 61502
. time: 3, ticks: 34465
---
Insert In [LinkedList] results:
. time: 16, ticks: 163744
. time: 13, ticks: 131980
. time: 27, ticks: 272733
. time: 23, ticks: 232244
. time: 11, ticks: 116074
---
Insert option 'NearFirst':
---
Insert In [List] results:
. time: 2719, ticks: 27195286
. time: 8159, ticks: 81598774
. time: 14682, ticks: 146827297
. time: 21227, ticks: 212275167
. time: 32296, ticks: 322969147
---
Insert In [LinkedList] results:
. time: 23, ticks: 232150
. time: 66, ticks: 667519
. time: 25, ticks: 253762
. time: 9, ticks: 99762
. time: 36, ticks: 369516
---
Insert option 'Middle':
---
Insert In [List] results:
. time: 1291, ticks: 12919443
. time: 4054, ticks: 40549061
. time: 6756, ticks: 67569316
. time: 9462, ticks: 94629967
. time: 12128, ticks: 121282267
---
Insert In [LinkedList] results:
. time: 33855, ticks: 338554998
. time: 126559, ticks: 1265598759
. time: 215692, ticks: 2156923201
. time: 302936, ticks: 3029361306
. time: 392140, ticks: 3921400025
---
return: [0]

выводы:
видим, что скорость вставки в связанный список драматически меняется,
 если нет способа определить прямую ссылку на узел списка,
 относительно которого будет выполняться вставка
*/
