using System;
using System.Collections.Generic;
using System.Linq;

namespace LifeSim;

public partial class World
{
    public Organism? FindNearest<T>(Point2D from, int visionRange)
        where T : Organism
    {
        Organism? best = null;
        var bestDist = int.MaxValue;

        foreach (var o in All)
        {
            if (o is T)
            {
                var dx = ToroidalDistance(from.X, o.Pos.X, Width);
                var dy = ToroidalDistance(from.Y, o.Pos.Y, Height);
                var distance = dx + dy;
                if (distance <= visionRange && distance < bestDist)
                {
                    best = o;
                    bestDist = distance;
                }
            }
        }

        return best;
    }

    public void Seed(int count, Func<Point2D, Organism> factory)
    {
        for (var i = 0; i < count; i++)
        {
            var p = RandomEmptyCell();
            if (p == null) break;
            
            Add(factory(p.Value));
        }
    }
}