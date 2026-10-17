using Vyshkat.Core.Domain;

namespace Vyshkat.Core.Services;

public interface IServiceCenter
{
    bool Inspect(Transport transport);
}
