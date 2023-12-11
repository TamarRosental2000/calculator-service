using Calculator.Cache.Logic;
using Calculator.Dal;
using Calculator.Models;
using Calculator.Utils;
using System.Data;

namespace Calculator.Logic
{
    public class CalculatorLogic
    {

        private readonly CacheLogic _cacheLogic;
        private readonly DalLayer _dalLayer;

        public CalculatorLogic(CacheLogic cacheLogic, DalLayer dalLayer)
        {
            _cacheLogic = cacheLogic;
            _dalLayer = dalLayer;
        }
        public string Calculate(string fieldA, string fieldB, string operation)
        {
            var result = "";
            var calculateOperation = new CalculateOperator();

            switch (operation)
            {
                case "+":
                    result = calculateOperation.Add(fieldA, fieldB);
                    break;
                case "-":
                    result = calculateOperation.Subtract(fieldA, fieldB);
                    break;
                case "/":
                    result = calculateOperation.Divide(fieldA, fieldB);
                    break;
                case "*":
                    result = calculateOperation.Multiply(fieldA, fieldB);
                    break;
                default:
                    result = string.Empty;
                    break;
            }
            return result;


        }
        public void GetCountAndGetLast3Calculate(OperationRequest request, Response response, int operatorId)
        {
            var utcNow = DateTime.UtcNow;
            var calculatorHistories = _cacheLogic.LoadFromCache(operatorId);
            if (calculatorHistories != null && calculatorHistories.Where(item => item.UpdateDate.Year == utcNow.Year && item.UpdateDate.Month == utcNow.Month).ToList().Any())
            {
                response.LastOperations = calculatorHistories
                    .Where(item => item.UpdateDate.Year == utcNow.Year && item.UpdateDate.Month == utcNow.Month)
                    .OrderByDescending(item => item.UpdateDate)
                    .Take(3)
                    .ToList();
                _cacheLogic.SaveItemToCache(new CalculatorHistoryItem()
                {
                    FieldA = request.FieldA,
                    FieldB = request.FieldB,
                    Operator = request.Operation,
                    OperatorId = operatorId,
                    Result = response.Result,
                    UpdateDate = DateTime.Now,
                });
            }
            else
            {
                calculatorHistories = _dalLayer.STPGetMonthlyOperation(request.Operation).ToList();
                response.LastOperations = calculatorHistories
                       .Where(item => item.UpdateDate.Year == utcNow.Year && item.UpdateDate.Month == utcNow.Month)
                       .OrderByDescending(item => item.UpdateDate)
                       .Take(3)
                       .ToList();
                _cacheLogic.SaveListToCache(calculatorHistories);
            }
            response.Count = calculatorHistories.Count(item => item.UpdateDate.Year == utcNow.Year && item.UpdateDate.Month == utcNow.Month);
        }

        internal string IsValidRequest(OperationRequest request)
        {
            int number = 0;
            if (request == null) return "Request is null";
            if (request.FieldA == null) return "FieldA is null";
            if (request.FieldB == null) return "FieldB is null";
            if (request.Operation == null) return "Operation is null";
            if (request.Operation=="/"&&Int32.TryParse(request.FieldB, out number) && number == 0 && request.Operation == "/") return "Cannot divide by zero";
            if (request.Operation == "/" && !request.FieldA.Contains(request.FieldB)&&!(Int32.TryParse(request.FieldA,out number)&& (Int32.TryParse(request.FieldB, out number))) )return "Cannot divide";
            if (request.Operation == "*"&& !(Int32.TryParse(request.FieldB, out number)|| Int32.TryParse(request.FieldA, out number))) return "Operation is null";
            return string.Empty;

        }
    }

}
