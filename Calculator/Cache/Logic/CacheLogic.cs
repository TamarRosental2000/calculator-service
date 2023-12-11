using Calculator.Models;
using Newtonsoft.Json;

namespace Calculator.Cache.Logic
{
    public  class CacheLogic
    {
        private  readonly object fileLock = new object();
        public  void SaveListToCache(IEnumerable<CalculatorHistoryItem> item)
        {
            string cachePath = GetCacheFilePath(item.FirstOrDefault().OperatorId);
            string json = JsonConvert.SerializeObject(item);

            lock (fileLock)
            {
                File.WriteAllText(cachePath, json);
            }
        }
        public  void SaveItemToCache(CalculatorHistoryItem item)
        {
            var cacheList = LoadFromCache(item.OperatorId);
            cacheList.ToList().Add(item);
            string cachePath = GetCacheFilePath(item.OperatorId);
            string json = JsonConvert.SerializeObject(cacheList);

            lock (fileLock)
            {
                File.WriteAllText(cachePath, json);
            }
        }

        public  IEnumerable<CalculatorHistoryItem> LoadFromCache(int operationId)
        {
            string cachePath = GetCacheFilePath(operationId);

            lock (fileLock)
            {
                try
                {
                    if (File.Exists(cachePath))
                    {
                        string json = File.ReadAllText(cachePath);
                        return JsonConvert.DeserializeObject<IEnumerable<CalculatorHistoryItem>>(json);
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            return null;
        }

        public  string GetCacheFilePath(int OperationId)
        {
            // Generate cache file path based on Operation ID
            string cacheFolder = "cache\\Data"; // Replace with your desired cache folder
            string cacheFileName = $"operation{OperationId}.json";
            return Path.Combine(cacheFolder, cacheFileName);
        }
    }
}
