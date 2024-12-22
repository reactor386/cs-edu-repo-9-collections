//-
using System;
using System.Collections;
using System.Diagnostics;


namespace WorkWithCollections.Benchmark;

/// <summary>
/// Класс для вывода отчета по производительности с помощью [Stopwatch]
/// </summary>
internal static class EstimatorStopwatchWrapper
{
    public delegate void MethodToApplyBenchmark();

    /// <summary>
    /// Метод сравнения производительности с помощью [Stopwatch]
    /// </summary>
    /// <param name="methodToApplyBenchmarkRunner">делегат исследуемой функции</param>
    /// <param name="n">количество повторений</param>
    /// <returns>массив кортежей - время операции, количество тактов</returns>
    public static (long milliSec, long numTick)[] EstimateByStopwatch(
        MethodToApplyBenchmark methodToApplyBenchmarkRunner, int n)
    {
        // для вывода используем массив кортежей
        // в учебных целях добавляем кортеж в динамический [ArrayList]
        //  с последующим преобразованием в [Array], содержащий кортеж
        // ArrayList res = [];
        // return ((long milliSec, long numTick)[])res.ToArray(typeof(ValueTuple<long, long>));
        // более простой путь - задать сразу [Array]
        //  с указанной в аргументе размерностью
        // (long milliSec, long numTick)[] res = new (long milliSec, long numTick)[n];
        // return res;

        ArrayList res = [];

        long milliSec;
        long numTick;

        // var stopWatchTimer = new Stopwatch();
        // stopWatchTimer.Start();
        var stopWatchTimer = Stopwatch.StartNew();

        for (int i = 0; i < n; i++)
        {
            stopWatchTimer.Restart();

            // Run stuff
            methodToApplyBenchmarkRunner.Invoke();

            stopWatchTimer.Stop();

            // long milliSec = stopWatchTimer.ElapsedMilliseconds;
            // double milliSec = stopWatchTimer.Elapsed.TotalMilliseconds;
            // TimeSpan timeTaken = stopWatchTimer.Elapsed;

            milliSec = stopWatchTimer.ElapsedMilliseconds;
            numTick = stopWatchTimer.ElapsedTicks;
            res.Add((milliSec, numTick));
        }

        return ((long milliSec, long numTick)[])res.ToArray(typeof(ValueTuple<long, long>));
    }
}
