namespace LifeSim;

public abstract partial class Animal
{
    public override void Tick()
    {
        base.Tick();
        InitializeEnergyOnBirth();
        SeekPreyAndEat();
        Energy -= MoveCost;
        TryReproduce();
        TryDie();
    }
}