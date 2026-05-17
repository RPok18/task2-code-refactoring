using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeSim;

public abstract partial class Animal : Organism
{
    protected Animal(World world, Point2D pos, Gender? gender = null)
        : base(world, pos, gender)
    {
    }

    protected abstract int Vision { get; }

    protected abstract int MoveCost { get; }

    protected abstract int BiteGain { get; }

    protected abstract int ReproduceThreshold { get; }

    protected abstract int InitialEnergy { get; }

    protected abstract char SelfGlyph { get; }

    public override char Glyph => SelfGlyph;


    public int Energy { get; set; }

    public int MaxAge { get; set; } = 1000;

   

    protected abstract Organism? FindPrey();

    protected abstract Animal MakeChild(Point2D p);

    protected static bool AreNeighborsOrSame(Point2D a, Point2D b) =>
        Math.Abs(a.X - b.X) <= 1 && Math.Abs(a.Y - b.Y) <= 1;

    protected void StepToward(Point2D target)
    {
        var dx = BestToroidalStep(Pos.X, target.X, World.Width);
        var dy = BestToroidalStep(Pos.Y, target.Y, World.Height);

        var candidates = new List<Point2D>();
        if (dx != 0)
        {
            candidates.Add(World.Wrap(new Point2D(Pos.X + dx, Pos.Y)));
        }

        if (dy != 0)
        {
            candidates.Add(World.Wrap(new Point2D(Pos.X, Pos.Y + dy)));
        }

        if (dx != 0 && dy != 0)
        {
            candidates.Add(World.Wrap(new Point2D(Pos.X + dx, Pos.Y + dy)));
        }

        var free = candidates.Where(World.IsEmpty).ToList();
        if (free.Count == 0)
        {
            Wander();
            return;
        }

        World.MoveTo(this, free.Pick(World.Random)!);
    }

    protected void Wander()
    {
        var options = World.EmptyNeighbors8(Pos).ToList();
        if (options.Count > 0)
        {
            World.MoveTo(this, options.Pick(World.Random)!);
        }
    }

    private static int BestToroidalStep(int from, int to, int size)
    {
        var direct = to - from;
        var wrapA = (to + size) - from;
        var wrapB = to - (from + size);

        var best =
            Math.Abs(direct) <= Math.Abs(wrapA) && Math.Abs(direct) <= Math.Abs(wrapB)
                ? direct
                : Math.Abs(wrapA) < Math.Abs(wrapB)
                    ? wrapA
                    : wrapB;

        return Math.Sign(best);
    }
}
