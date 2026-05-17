namespace LifeSim;

public abstract partial class Animal
{
    private void InitializeEnergyOnBirth()
    {
        if (Age == 1 && Energy == 0)
            Energy = InitialEnergy;
    }
}