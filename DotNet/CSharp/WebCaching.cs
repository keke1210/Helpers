using System;
using System.Web;

/**
* Example Usage:
* When you need know new cars are entered every week so you update cache every 7 days
* 
* var testData = CacheHelper.GetSetObjectFromCache(key + id, 7 * 24, () => context.Cars.GetAll(id));
* 
*/
public static class CacheHelper
{
    public static void SaveToCache(string cacheKey, int cacheTimeInHours, object savedItem)
    {
        if (IsIncache(cacheKey))
        {
            HttpContext.Current.Cache.Remove(cacheKey);
        }

        HttpContext.Current.Cache.Add(cacheKey, savedItem, null, DateTime.Now.AddHours(cacheTimeInHours), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);
    }

    public static T GetSetObjectFromCache<T>(string cacheKey, int cacheTimeInHours, Func<T> objectSettingFunction) where T : class
    {
        T cachedObject = HttpContext.Current.Cache[cacheKey] as T;
        if (cachedObject == null)
        {
            cachedObject = objectSettingFunction();

            if (cachedObject != null)
            {
                HttpContext.Current.Cache.Add(cacheKey, cachedObject, null, DateTime.Now.AddHours(cacheTimeInHours),
                System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);
            }
        }

        return cachedObject;
    }

    public static void RemoveFromCache(string cacheKey)
    {
        if (IsIncache(cacheKey))
            HttpContext.Current.Cache.Remove(cacheKey);
    }

    public static bool IsIncache(string cacheKey)
    {
        return HttpContext.Current.Cache[cacheKey] != null;
    }
}