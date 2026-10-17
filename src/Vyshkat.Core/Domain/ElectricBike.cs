namespace Vyshkat.Core.Domain;

public sealed class ElectricBike : Transport, IEnergyConsumer
{
    public ElectricBike(string inventoryNumber, int beginnerScore, double dailyEnergyConsumption)
        : base("Электровелосипед", inventoryNumber, beginnerScore)
    {
        if (!double.IsFinite(dailyEnergyConsumption) || dailyEnergyConsumption < 0)
            throw new ArgumentOutOfRangeException(nameof(dailyEnergyConsumption));

        DailyEnergyConsumption = dailyEnergyConsumption;
    }

    public double DailyEnergyConsumption { get; }
}
