using Dr.Core.Core;
using Dr.Core.Systems;

namespace Dr.Core;

public class World
{
    private readonly Dictionary<Type, IComponentPool> _componentPools = new();
    private readonly Queue<int> _freeIds = new();
    private readonly List<int> _generations = [];

    private readonly List<ISystem> _systems = [];
    private int _nextEntityId;

    // Entities control functions
    public Entity CreateEntity()
    {
        int id, generation;

        if (_freeIds.Count == 0)
        {
            id = _nextEntityId++;
            _generations.Add(0);
            generation = 0;
        }
        else
        {
            id = _freeIds.Dequeue();
            generation = _generations[id];
        }

        return new Entity(id, generation);
    }

    public void DestroyEntity(Entity entity)
    {
        if (!IsAlive(entity)) return;

        foreach (var pool in _componentPools.Values) pool.Remove(entity.Id);

        _generations[entity.Id]++;
        _freeIds.Enqueue(entity.Id);
    }

    public bool IsAlive(Entity entity)
    {
        return entity.Id >= 0 &&
               entity.Id < _nextEntityId &&
               entity.Generation == _generations[entity.Id];
    }

    // Components control functions
    private ComponentPool<T> GetPool<T>() where T : struct
    {
        var type = typeof(T);

        if (_componentPools.TryGetValue(type, out var pool)) return (ComponentPool<T>)pool;

        pool = new ComponentPool<T>();
        _componentPools[type] = pool;

        return (ComponentPool<T>)pool;
    }

    public void AddComponent<T>(Entity entity, T component) where T : struct
    {
        if (!IsAlive(entity)) return;
        GetPool<T>().Add(entity.Id, component);
    }

    public void RemoveComponent<T>(Entity entity) where T : struct
    {
        if (!IsAlive(entity)) return;
        GetPool<T>().Remove(entity.Id);
    }

    public ref T GetComponent<T>(Entity entity) where T : struct
    {
        return ref GetPool<T>().Get(entity.Id);
    }

    public bool HasComponent<T>(Entity entity) where T : struct
    {
        return GetPool<T>().Has(entity.Id);
    }

    public Entity GetEntity(int entityId)
    {
        return new Entity(entityId, _generations[entityId]);
    }

    // Systems control functions
    public World AddSystem(ISystem system)
    {
        _systems.Add(system);
        return this;
    }

    public void Update(float deltaTime)
    {
        foreach (var system in _systems) system.Update(this, deltaTime);
    }

    // Query functions
    public IEnumerable<int> Query(params IComponentPool[] pools)
    {
        if (pools.Length == 0) yield break;

        var smallestPool = pools[0];
        var minCount = smallestPool.GetActiveEntityIds().Count;

        for (var i = 1; i < pools.Length; i++)
        {
            var count = pools[i].GetActiveEntityIds().Count;

            if (count >= minCount) continue;

            minCount = count;
            smallestPool = pools[i];
        }

        foreach (var entityId in smallestPool.GetActiveEntityIds())
        {
            var hasAll = pools.All(pool => pool.Has(entityId));

            if (hasAll) yield return entityId;
        }
    }

    public IEnumerable<int> Query<T1>()
        where T1 : struct
    {
        return Query(GetPool<T1>());
    }

    public IEnumerable<int> Query<T1, T2>()
        where T1 : struct
        where T2 : struct
    {
        return Query(GetPool<T1>(), GetPool<T2>());
    }

    public IEnumerable<int> Query<T1, T2, T3>() 
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        return Query(GetPool<T1>(), GetPool<T2>(), GetPool<T3>());
    }
}