using System;

namespace LifeSim;

public abstract class Organism
{
    protected Organism(World world, Point2D pos, Gender? gender = null)
    {
        World = world;
        Pos = world.Wrap(pos);
        Gender = gender ?? PickGender(world);
    }

    public World World { get; }

    public Point2D Pos { get; set; }

    public bool IsAlive { get; set; } = true;

    public int Age { get; private set; }

    public abstract char Glyph { get; }

    public Gender Gender { get; }

    public virtual void Tick() => Age++;

    private static Gender PickGender(World world) => world.Random.Chance(0.5) ? Gender.Female : Gender.Male;
}
