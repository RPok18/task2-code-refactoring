using System.Linq;

namespace LifeSim;

public abstract partial class Animal
{
    private void TryReproduce()
    {
        if (Energy < ReproduceThreshold) return;

        var empty = World.EmptyNeighbors8(Pos).ToList();
        if (empty.Count == 0) return;

        var child = MakeChild(empty.Pick(World.Random)!);
        Energy /= 2;
        World.Add(child);
    }
}