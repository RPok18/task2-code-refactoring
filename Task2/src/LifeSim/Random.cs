using System;
using System.Collections.Generic;

namespace LifeSim;

public interface IRandomProvider
{
    int Next(int min, int max);
    double NextDouble();
    bool Chance(double p);
}

public class DefaultRandomProvider : IRandomProvider
{
    private readonly System.Random _random = new();

    public int Next(int min, int max) => _random.Next(min, max);

    public double NextDouble() => _random.NextDouble();

    public bool Chance(double p) => NextDouble() < p;
}

public static class RandomExtensions
{
    public static T? Pick<T>(this IList<T> list, IRandomProvider random) => 
        list.Count == 0 ? default : list[random.Next(0, list.Count)];
}
