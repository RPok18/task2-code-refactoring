using System;
using System.Collections.Generic;

namespace LifeSim;

public static class Random
{
    private static readonly Random _random = new();

    public static int Next(int min, int max) => Random.Next(min, max);

    public static double NextDouble() => Random.NextDouble();

    public static T? Pick<T>(this IList<T> list) => list.Count == 0 ? default : list[Random.Next(0, list.Count)];

    public static bool Chance(double p) => NextDouble() < p;
}
