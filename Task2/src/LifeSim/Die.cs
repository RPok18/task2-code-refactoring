namespace LifeSim;

public abstract partial class Animal
{
    private void TryDie()
    {
        if (Energy <= 0 || (Age > MaxAge && World.Random.Chance(0.02)))
            World.Remove(this);
    }
}
