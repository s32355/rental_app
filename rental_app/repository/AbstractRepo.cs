using rental_app.model;

namespace rental_app.repository;

public abstract class AbstractRepo<T> where T : IUser
{
    protected Dictionary<long, T> _map = new Dictionary<long, T>();

    public void AddObject(T obj)
    {
        _map.TryAdd(obj.Id, obj);
    }

    public void RemoveObject(T obj)
    {
        _map.Remove(obj.Id);
    }

    public Dictionary<long, T> GetObjects()
    {
        return _map;
    }

    public Dictionary<long, TK> GetObjectsByType<TK>() where TK : T
    {
        return _map
            .Where(pair => pair.Value is TK)
            .ToDictionary(pair => pair.Key, pair => (TK) pair.Value);
    }

    public T? GetById(long id)
    {
        _map.TryGetValue(id, out var obj);
        return obj;
    }
}