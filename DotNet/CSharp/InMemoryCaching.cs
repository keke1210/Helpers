using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

public static class CacheStore
{
    private static ConcurrentDictionary<string, object> _cache;

    /// <summary>
    /// Cache initializer
    /// </summary>
    static CacheStore()
    {
        _cache = new ConcurrentDictionary<string, object>();
    }

    /// <summary>
    /// Check if an object exists in cache
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    /// <param name="key">Name of key in cache</param>
    /// <returns>True, if yes; False, otherwise</returns>
    public static bool Exists<T>(string key) where T : class
    {
        Type type = typeof(T);
        return _cache.ContainsKey(key + type.FullName);
    }

    /// <summary>
    /// Check if an object exists in cache
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    /// <returns>True, if yes; False, otherwise</returns>
    public static bool Exists<T>() where T : class
    {
        Type type = typeof(T);
        return _cache.ContainsKey(type.FullName);
    }

    /// <summary>
    /// Get an object from cache
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    /// <returns>Object from cache</returns>
    public static T Get<T>() where T : class
    {
        Type type = typeof(T);

        if (_cache.ContainsKey(type.FullName) == false)
            return null;

        return (T)_cache[type.FullName];
    }

    /// <summary>
    /// Get an object from cache
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    /// <param name="key">Name of key in cache</param>
    /// <returns>Object from cache</returns>
    public static T Get<T>(string key) where T : class
    {
        Type type = typeof(T);
       
        if (_cache.ContainsKey(key + type.FullName) == false)
            return null;
       
        return (T)_cache[key + type.FullName];
    }

    /// <summary>
    /// Create default instance of the object and add it in cache
    /// </summary>
    /// <typeparam name="T">Class whose object is to be created</typeparam>
    /// <returns>Object of the class</returns>
    public static T Create<T>(string key, params object[] constructorParameters) where T : class
    {
        Type type = typeof(T);
        T value = (T)Activator.CreateInstance(type, constructorParameters);

        if (_cache.ContainsKey(key + type.FullName))
            return null;

        _cache.TryAdd(key + type.FullName, value);

        return value;
    }

    /// <summary>
    /// Create default instance of the object and add it in cache
    /// </summary>
    /// <typeparam name="T">Class whose object is to be created</typeparam>
    /// <returns>Object of the class</returns>
    public static T Create<T>(params object[] constructorParameters) where T : class
    {
        Type type = typeof(T);
        T value = (T)Activator.CreateInstance(type, constructorParameters);

        if (_cache.ContainsKey(type.FullName))
            return null;

        _cache.TryAdd(type.FullName, value);

        return value;
    }

    public static void Add<T>(string key, T value)
    {
        Type type = typeof(T);

        if (_cache.ContainsKey(key + type.FullName))
            return;

        _cache.TryAdd(key + type.FullName, value);
    }

    /// <summary>
    /// Remove an object type from cache
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    public static void Remove<T>()
    {
        Type type = typeof(T);

        if (_cache.ContainsKey(type.FullName) == false)
            return;
            
		_cache.TryRemove(type.FullName, out object _);
    }

    /// <summary>
    /// Remove an object stored with a key from cache
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    /// <param name="key">Key of the object</param>
    public static void Remove<T>(string key)
    {
        Type type = typeof(T);

        if (_cache.ContainsKey(key + type.FullName) == false)
            return;

        _cache.TryRemove(key + type.FullName, out object _);
    }
}


public class Program 
{
    void Main()
    {
        var person = new Person("Skerdi");
        CacheStore.Add<Person>("key1", person);
        var obj = CacheStore.Get<Person>("key1");
        
        var anotherPerson = CacheStore.Create<Person>("key2", "Altjen");
        var obj2 = CacheStore.Get<Person>("key2");
        
        CacheStore.Remove<Person>("key1");
        var skerdiExists = CacheStore.Exists<Person>("key1");
        
        Console.WriteLine(skerdiExists);
        Console.WriteLine(obj?.Name);
        Console.WriteLine(obj2?.Name);
    }
    public class Person 
    {
        public Person() {}

        public Person(string name){
            Name = name;
        }

        public string Name;
    }
}