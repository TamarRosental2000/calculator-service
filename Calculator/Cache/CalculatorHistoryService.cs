using Calculator.Dal;
using Calculator.Models;
using Microsoft.Extensions.Caching.Memory;

public class CalculatorHistoryService
{
    private readonly IMemoryCache _memoryCache;
    private readonly DalLayer _dalLayer;

    public CalculatorHistoryService(IMemoryCache memoryCache,DalLayer dalLayer)
    {
        _memoryCache = memoryCache;
        _dalLayer = dalLayer;
    }

    public IEnumerable<CalculatorHistoryItem> GetCalculatorHistory(string operatorValue)
    {
        // Attempt to get the calculator history from the cache
        if (_memoryCache.TryGetValue("CalculatorHistory", out List<CalculatorHistoryItem> calculatorHistory))
        {
            // Return the cached data if available
            return calculatorHistory;
        }
        else
        {
            // If not in cache, query the database to get the data
            var getMonthlyOperation = _dalLayer.STPGetMonthlyOperation(operatorValue);

            // Cache the data for future use
            _memoryCache.Set("CalculatorHistory", calculatorHistory, TimeSpan.FromMinutes(10)); // Adjust the expiration time as needed

            return getMonthlyOperation;
        }
    }

    

}