using System.Collections.Generic;

namespace Dr.Core.Core;

public interface IComponentPool
{
    void Remove(int entityId);
    bool Has(int entityId);
    public IReadOnlyList<int> GetActiveEntityIds();
}