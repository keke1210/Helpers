using System;
using System.Collections.Generic;

public static class CacheStore
{
    private static Dictionary<string, object> _cache;
    private readonly static object _sync;

    /// <summary>
    /// Cache initializer
    /// </summary>
    static CacheStore()
    {
        _cache = new Dictionary<string, object>();
        _sync = new object();
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
        lock (_sync)
        {
            return _cache.ContainsKey(key + type.FullName);
        }
    }

    /// <summary>
    /// Check if an object exists in cache
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    /// <returns>True, if yes; False, otherwise</returns>
    public static bool Exists<T>() where T : class
    {
        Type type = typeof(T);
        lock (_sync)
        {
            return _cache.ContainsKey(type.FullName);
        }
    }

    /// <summary>
    /// Get an object from cache
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    /// <returns>Object from cache</returns>
    public static T Get<T>() where T : class
    {
        Type type = typeof(T);

        lock (_sync)
        {
            if (_cache.ContainsKey(type.FullName) == false)
                return null;

            lock (_sync)
            {
                return (T)_cache[type.FullName];
            }
        }
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

        lock (_sync)
        {
            if (_cache.ContainsKey(key + type.FullName) == false)
                return null;

            lock (_sync)
            {
                return (T)_cache[key + type.FullName];
            }
        }
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

        lock (_sync)
        {
            if (_cache.ContainsKey(key + type.FullName))
                return null;

            lock (_sync)
            {
                _cache.Add(key + type.FullName, value);
            }
        }

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

        lock (_sync)
        {
            if (_cache.ContainsKey(type.FullName))
                return null;

            lock (_sync)
            {
                _cache.Add(type.FullName, value);
            }
        }

        return value;
    }

    public static void Add<T>(string key, T value)
    {
        Type type = typeof(T);

        lock (_sync)
        {
            if (_cache.ContainsKey(key + type.FullName))
                return;

            lock (_sync)
            {
                _cache.Add(key + type.FullName, value);
            }
        }
    }

    /// <summary>
    /// Remove an object type from cache
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    public static void Remove<T>()
    {
        Type type = typeof(T);

        lock (_sync)
        {
            if (_cache.ContainsKey(type.FullName) == false)
                return;

            lock (_sync)
            {
                _cache.Remove(type.FullName);
            }
        }
    }

    /// <summary>
    /// Remove an object stored with a key from cache
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    /// <param name="key">Key of the object</param>
    public static void Remove<T>(string key)
    {
        Type type = typeof(T);

        lock (_sync)
        {
            if (_cache.ContainsKey(key + type.FullName) == false)
                return;

            lock (_sync)
            {
                _cache.Remove(key + type.FullName);
            }
        }
    }
}