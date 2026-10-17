namespace Vyshkat.Core.Domain;

public sealed class Scooter : Transport, IEnergyConsumer
{
    public Scooter(string inventoryNumber, int beginnerScore, double dailyEnergyConsumption)
        : base("Самокат", inventoryNumber, beginnerScore)
    {
        if (!double.IsFinite(dailyEnergyConsumption) || dailyEnergyConsumption < 0)
            throw new ArgumentOutOfRangeException(nameof(dailyEnergyConsumption));

        DailyEnergyConsumption = dailyEnergyConsumption;
    }

    public double DailyEnergyConsumption { get; }
}
