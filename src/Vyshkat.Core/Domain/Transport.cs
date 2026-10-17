namespace Vyshkat.Core.Domain;

public abstract class Transport : IInventoryItem
{
    protected Transport(string name, string inventoryNumber, int beginnerScore)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(inventoryNumber);
        ArgumentOutOfRangeException.ThrowIfLessThan(beginnerScore, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(beginnerScore, 10);

        Name = name;
        InventoryNumber = inventoryNumber;
        BeginnerScore = beginnerScore;
    }

    public string Name { get; }
    public string InventoryNumber { get; }
    public int BeginnerScore { get; }
}
