using System;
using System.Linq;

namespace LifeSim;

public static partial class Program
{
    private static void RenderWorld(IReadOnlyWorld world)
    {
        Console.SetCursorPosition(0, 0);
    
        var stats = world.All
            .GroupBy(o => o.GetType().Name)
            .Select(g => $"{g.Key}s: {g.Count(),-5}")
            .ToList();

        Console.ResetColor();

        Console.WriteLine($"Tick: {world.Tick,-8}  {string.Join("  ", stats)}   [Space/P] pause, [Q/Esc] quit");

        var snapshot = world.GridSnapshot();

        for (int y = 0; y < world.Height; y++)
        {
            for (int x = 0; x < world.Width; x++)
            {
                if (snapshot.TryGetValue(new Point2D(x, y), out var organism))
                {
                    Console.ForegroundColor = GetColorFor(organism);
                    Console.Write(organism.Glyph);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(' ');
                }
            }

            Console.WriteLine();
        }
    }

    private static readonly System.Collections.Generic.Dictionary<Type, ConsoleColor> _organismColors = new()
    {
        { typeof(Plant), ConsoleColor.Green },
        { typeof(Herbivore), ConsoleColor.Yellow },
        { typeof(Predator), ConsoleColor.Red }
    };

    public static void RegisterColor<T>(ConsoleColor color) where T : Organism
    {
        _organismColors[typeof(T)] = color;
    }

    private static ConsoleColor GetColorFor(Organism organism)
    {
        return _organismColors.TryGetValue(organism.GetType(), out var color) ? color : ConsoleColor.White;
    }
}