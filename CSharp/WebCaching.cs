using System;
using System.Web;

/**
* Example Usage:
* 
* var testData = CacheHelper.GetSetObjectFromCache(key, 2 * 24, () => context.Cars.GetAll());
* 
*/
public static class CacheHelper
{
    public static void SaveTocache(string cacheKey, object savedItem)
    {
        if (IsIncache(cacheKey))
        {
            HttpContext.Current.Cache.Remove(cacheKey);
        }

        HttpContext.Current.Cache.Add(cacheKey, savedItem, null, DateTime.Now.AddDays(1), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);
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