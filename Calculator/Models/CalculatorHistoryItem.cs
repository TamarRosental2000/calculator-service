namespace Calculator.Models
{

    public class CalculatorHistoryItem
    {
        public string FieldA { get; set; }
        public string FieldB { get; set; }
        public string Operator { get; set; }
        public int OperatorId { get; set; }

        public DateTime UpdateDate { get; set; }

        public string Result { get; set; }
    }

}
