using Vyshkat.Core.Domain;

namespace Vyshkat.Core.Services;

public sealed class FleetService
{
    private readonly IServiceCenter _serviceCenter;
    private readonly BeginnerPolicy _beginnerPolicy;
    private readonly List<Transport> _transports = [];
    private readonly List<IInventoryItem> _inventoryItems = [];

    public FleetService(IServiceCenter serviceCenter, BeginnerPolicy beginnerPolicy)
    {
        _serviceCenter = serviceCenter ?? throw new ArgumentNullException(nameof(serviceCenter));
        _beginnerPolicy = beginnerPolicy ?? throw new ArgumentNullException(nameof(beginnerPolicy));
    }

    public int TransportCount => _transports.Count;
    public int ItemCount => _inventoryItems.Count;
    public IReadOnlyList<Transport> Transports => _transports.AsReadOnly();
    public IReadOnlyList<IInventoryItem> InventoryItems => _inventoryItems.AsReadOnly();

    public bool AddTransport(Transport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        if (IsRegistered(transport.InventoryNumber) || !_serviceCenter.Inspect(transport))
            return false;

        _transports.Add(transport);
        _inventoryItems.Add(transport);
        return true;
    }

    public bool AddThing(Thing thing)
    {
        ArgumentNullException.ThrowIfNull(thing);
        if (thing.InventoryNumber.Length < 3 || IsRegistered(thing.InventoryNumber))
            return false;

        _inventoryItems.Add(thing);
        return true;
    }

    public double TotalDailyEnergyConsumption() =>
        _inventoryItems.OfType<IEnergyConsumer>().Sum(item => item.DailyEnergyConsumption);

    public IReadOnlyList<Transport> BeginnerFriendlyTransports() =>
        _transports.Where(_beginnerPolicy.IsSuitable).ToList();

    public IReadOnlyDictionary<string, int> TransportCounts() =>
        _transports.GroupBy(transport => transport.Name)
            .ToDictionary(group => group.Key, group => group.Count());

    private bool IsRegistered(string inventoryNumber) =>
        _inventoryItems.Any(item => string.Equals(
            item.InventoryNumber, inventoryNumber, StringComparison.OrdinalIgnoreCase));
}
