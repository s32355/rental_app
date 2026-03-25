using System.Text.Json;
using rental_app.entity;

namespace rental_app.repository;

public abstract class AbstractRepo<T> where T : IEntity
{
    protected Dictionary<long, T> _map = new Dictionary<long, T>();
    private readonly string _finalPath;

    protected AbstractRepo(string finalPath)
    {
        _finalPath = finalPath;
        LoadDataFromJson();
    }

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

    public bool CheckIfExists(long id)
    {
        return _map.ContainsKey(id);
    }

    public void SaveDataToJson()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_finalPath)!);
        File.WriteAllText(_finalPath,JsonSerializer.Serialize(_map));
    }

    private void LoadDataFromJson()
    {
        if (!File.Exists(_finalPath))
        {
            return;
        }

        _map = JsonSerializer.Deserialize<Dictionary<long, T>>(File.ReadAllText(_finalPath)) ??
               new Dictionary<long, T>();
    }
}