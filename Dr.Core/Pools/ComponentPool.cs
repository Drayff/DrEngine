using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Dr.Core.Core;

public class ComponentPool<T> : IComponentPool
{
    private readonly List<int> _activeEntityIds = [];
    private readonly List<T> _data = [];
    private readonly List<int> _entityToIndex = [];
    private readonly List<bool> _hasComponent = [];

    public void Remove(int entityId)
    {
        if (!Has(entityId)) return;

        _hasComponent[entityId] = false;
        _data[entityId] = default!;

        var removeIndex = _entityToIndex[entityId];
        var lastIndex = _activeEntityIds.Count - 1;

        if (removeIndex < lastIndex)
        {
            var lastEntityId = _activeEntityIds[lastIndex];
            _activeEntityIds[removeIndex] = lastEntityId;
            _entityToIndex[lastEntityId] = removeIndex;
        }

        _activeEntityIds.RemoveAt(lastIndex);
    }

    public bool Has(int entityId)
    {
        if (entityId < 0 || entityId >= _data.Count) return false;
        return _hasComponent[entityId];
    }

    public IReadOnlyList<int> GetActiveEntityIds()
    {
        return _activeEntityIds;
    }

    public void Add(int entityId, T component)
    {
        EnsureCapacity(entityId);

        _data[entityId] = component;

        if (Has(entityId)) return;

        _hasComponent[entityId] = true;
        _entityToIndex[entityId] = _activeEntityIds.Count;
        _activeEntityIds.Add(entityId);
    }

    public ref T Get(int entityId)
    {
        return ref CollectionsMarshal.AsSpan(_data)[entityId];
    }

    private void EnsureCapacity(int entityId)
    {
        while (_data.Count <= entityId)
        {
            _data.Add(default!);
            _hasComponent.Add(false);
            _entityToIndex.Add(-1);
        }
    }
}