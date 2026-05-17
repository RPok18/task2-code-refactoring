namespace LifeSim;

public abstract partial class Animal
{
    private void SeekPreyAndEat()
    {
        var prey = FindPrey();
        if (prey != null)
        {
            StepToward(prey.Pos);
            if (AreNeighborsOrSame(Pos, prey.Pos) && prey.IsAlive)
            {
                World.Remove(prey);
                Energy += BiteGain;
            }
        }
        else
        {
            Wander();
        }
    }
}