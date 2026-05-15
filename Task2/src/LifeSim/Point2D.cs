namespace LifeSim;

public readonly record struct Point2D(int X, int Y)
{
    public override string ToString() => $"({X},{Y})";
}
