using System.Collections.Generic;

namespace LifeSim;

public interface IReadOnlyWorld
{
    int Width { get; }
    int Height { get; }
    int Tick { get; }
    IEnumerable<Organism> All { get; }
    IReadOnlyDictionary<Point2D, Organism> GridSnapshot();
}
