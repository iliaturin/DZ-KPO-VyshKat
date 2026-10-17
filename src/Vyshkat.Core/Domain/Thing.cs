namespace Vyshkat.Core.Domain;

public abstract class Thing : IInventoryItem
{
    protected Thing(string name, string inventoryNumber)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(inventoryNumber);

        Name = name;
        InventoryNumber = inventoryNumber;
    }

    public string Name { get; }
    public string InventoryNumber { get; }
}
