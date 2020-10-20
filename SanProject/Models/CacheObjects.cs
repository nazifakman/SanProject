using SanProject.Models;
using SanProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SanProject.Models
{
    public static class CacheObject
    {

        public static string GetApplicationPath()
        {
            return string.Format("{0}://{1}{2}",
                    HttpContext.Current.Request.Url.Scheme,
                    HttpContext.Current.Request.ServerVariables["HTTP_HOST"],
                    (HttpContext.Current.Request.ApplicationPath.Equals("/")) ? string.Empty : HttpContext.Current.Request.ApplicationPath
                    );
        }


    }
    public static class CacheHelper
    {
        private static void InsertToCache(string cacheKey, object savedItem, int hour, int minute)
        {
            HttpContext.Current.Cache.Add(cacheKey, savedItem, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(hour, minute, 0), System.Web.Caching.CacheItemPriority.Default, null);
        }
        public static void SaveToCache(string cacheKey, object savedItem)
        {
            if (IsInCache(cacheKey))
            {
                HttpContext.Current.Cache.Remove(cacheKey);
            }
            InsertToCache(cacheKey, savedItem, 1, 0);
        }
        public static void SaveToCache(string cacheKey, object savedItem, int hour, int minute)
        {
            if (IsInCache(cacheKey))
            {
                HttpContext.Current.Cache.Remove(cacheKey);
            }
            InsertToCache(cacheKey, savedItem, hour, minute);
        }

        public static T GetFromCache<T>(string cacheKey) where T : class
        {
            return HttpContext.Current.Cache[cacheKey] as T;
        }

        public static void RemoveFromCache(string cacheKey)
        {
            HttpContext.Current.Cache.Remove(cacheKey);
        }

        public static bool IsInCache(string cacheKey)
        {
            return HttpContext.Current.Cache[cacheKey] != null;
        }
        public static void RemoveAllCacheKeys()
        {
            foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
            {
                HttpContext.Current.Cache.Remove((string)entry.Key);
            }
        }
    }
}