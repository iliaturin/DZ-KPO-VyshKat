using Vyshkat.Core.Domain;

namespace Vyshkat.Core.Services;

public sealed class ServiceCenter : IServiceCenter
{
    public bool Inspect(Transport transport)
    {
        ArgumentNullException.ThrowIfNull(transport);
        return transport.InventoryNumber.Length >= 3;
    }
}
