using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace LifeSim;

public static partial class Program
{
   
    private const int Width = 50;
    private const int Height = 22;

    private const int InitialHerbivores = 28;
    private const int InitialPredators = 10;

    private const int DelayMs = 120;

    public static void Main()
    {
        SetupConsole();

        var world = CreateWorld();

        RunSimulation(world);

        CleanupConsole();
    }

    private static void SetupConsole()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;
    }

    private static void CleanupConsole()
    {
        Console.ResetColor();
        Console.CursorVisible = true;
    }

    private static World CreateWorld()
    {
        var random = new DefaultRandomProvider();
        var world = new World(Width, Height, random);

        int initialPlants = (int)(Width * Height * 0.22);

        world.Seed(initialPlants, pos => new Plant(world, pos));
        world.Seed(InitialHerbivores, pos => new Herbivore(world, pos));
        world.Seed(InitialPredators, pos => new Predator(world, pos));

        return world;
    }

    private static void RunSimulation(World world)
    {
        bool paused = false;

        while (true)
        {
            HandleInput(ref paused);

            if (!paused)
            {
                world.Step();
                RenderWorld(world);
            }

            Thread.Sleep(DelayMs);
        }
    }

    private static void HandleInput(ref bool paused)
    {
        while (!Console.IsInputRedirected && Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.Q:
                case ConsoleKey.Escape:
                    CleanupConsole();
                    Environment.Exit(0);
                    break;

                case ConsoleKey.Spacebar:
                case ConsoleKey.P:
                    paused = !paused;
                    break;
            }
        }
    }
}