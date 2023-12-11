using Calculator.Cache.Logic;
using Calculator.Dal;
using Calculator.Logic;
using Calculator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly CalculatorLogic _calculatorLogic;
        private readonly DalLayer _dalLayer;
        private readonly CacheLogic _cacheLogic;

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger, CalculatorLogic calculatorLogic,DalLayer dalLayer, CacheLogic cacheLogic)
        {
            _logger = logger;
            _calculatorLogic = calculatorLogic;
            _dalLayer = dalLayer;
            _cacheLogic = cacheLogic;
        }

        [HttpPost]
        [Route("execute")]

        public Response Post([FromBody] OperationRequest request)
        {
            var response = new Response();
            var message = "";
            //validation
           if(!string.IsNullOrEmpty(message = _calculatorLogic.IsValidRequest(request)))
            {
                throw new ArgumentException(message);
            }
            response.Result = _calculatorLogic.Calculate(request.FieldA, request.FieldB, request.Operation);
            if(string.IsNullOrEmpty( response.Result))
            {
                throw new ArgumentException("Result is cannot null or empty");

            }
            var operatorId = _dalLayer.InsertResult(request.Operation, request.FieldA, request.FieldB, response.Result);
            _calculatorLogic.GetCountAndGetLast3Calculate(request, response, operatorId);
            return response;

        }

     
        [HttpGet]
        [Route("getListOperations")]

        public IEnumerable<string> GetListOperations()
        {
            return _dalLayer.GetOpeation();

        }
    }
}