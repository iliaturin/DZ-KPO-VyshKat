namespace Vyshkat.Core.Domain;

public sealed class ChargingStation : Thing, IEnergyConsumer
{
    public ChargingStation(string inventoryNumber, double dailyEnergyConsumption)
        : base("Зарядная станция", inventoryNumber)
    {
        if (!double.IsFinite(dailyEnergyConsumption) || dailyEnergyConsumption < 0)
            throw new ArgumentOutOfRangeException(nameof(dailyEnergyConsumption));

        DailyEnergyConsumption = dailyEnergyConsumption;
    }

    public double DailyEnergyConsumption { get; }
}
