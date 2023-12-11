using Calculator.Table;

namespace Calculator.Models
{
    public class Response
    {
        public String Result { get; set; }
        public int Count { get; set; }
        public IEnumerable<CalculatorHistoryItem> LastOperations { get; set; }

    }
}
