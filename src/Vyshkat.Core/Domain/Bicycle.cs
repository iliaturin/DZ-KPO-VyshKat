namespace Vyshkat.Core.Domain;

public sealed class Bicycle : Transport
{
    public Bicycle(string inventoryNumber, int beginnerScore)
        : base("Велосипед", inventoryNumber, beginnerScore)
    {
    }
}
