using System.Text;

namespace Calculator.Utils
{
    public  class CalculateOperator
    {
        public  string Add(string fieldA, string fieldB)
        {
            var IsADouble = double.TryParse(fieldA, out double doubleFieldA);
            var IsBDouble = double.TryParse(fieldB, out double doubleFieldB);
            if (IsADouble && IsBDouble)
            {
                return (doubleFieldA + doubleFieldB).ToString();
            }
            else
            {
                return fieldA + fieldB;
            }

        }
  
        public  string Subtract(string fieldA, string fieldB)
        {
            var IsADouble = double.TryParse(fieldA, out double doubleFieldA);
            var IsBDouble = double.TryParse(fieldB, out double doubleFieldB);
            if (IsADouble && IsBDouble)
            {
                return (doubleFieldA - doubleFieldB).ToString();
            }
            else
            {
                if (fieldA.Contains(fieldB))
                {
                  return  RemoveSubstring(fieldA, fieldB);
                }
                return fieldA;
            }

        }
        public  string Divide(string fieldA, string fieldB)
        {
            var IsADouble = double.TryParse(fieldA, out double doubleFieldA);
            var IsBDouble = double.TryParse(fieldB, out double doubleFieldB);
            if (IsADouble && IsBDouble)//Example  4/2
            {
                if (doubleFieldB != 0)
                {
                    return (doubleFieldA / doubleFieldB).ToString();
                }
                else
                {
                    throw new ArgumentException("Cannot divide by zero");// Invalid 4/0 
                }
            }
            else
            {
                if (!IsADouble && !IsBDouble)
                {
                    if (fieldA.Contains(fieldB))// abc:abc
                    {
                        int count = 0;
                        int index = 0;

                        while ((index = fieldA.IndexOf(fieldB, index)) != -1)
                        {
                            count++;
                            index += fieldB.Length;
                        }
                        return (count).ToString();
                    }
                    else
                    {
                        throw new ArgumentException("Cannot divide ");
                    }
                }
                else
                {
                    throw new ArgumentException("Cannot divide Sting and number");
                }
            }

        }
        public  string Multiply(string fieldA, string fieldB)
        {
            var IsADouble = double.TryParse(fieldA, out double doubleFieldA);
            var IsBDouble = double.TryParse(fieldB, out double doubleFieldB);
            if (IsADouble && IsBDouble)//Example  4*2
            {
                return (doubleFieldA * doubleFieldB).ToString();
            }
            else
            {
                if (IsADouble || !IsBDouble)
                {
                    return ConcatenateString(fieldB, Convert.ToInt32(doubleFieldA));
                }
                if (!IsADouble || IsBDouble)
                {
                    return ConcatenateString(fieldA, Convert.ToInt32(doubleFieldB));
                }
                else
                {
                    throw new ArgumentException("Cannot multiply 2 strings");
                }

            }
        }
        public string ConcatenateString(string inputString, int repeatCount)
        {
            if (repeatCount <= 0)
            {
                return string.Empty;
            }

            StringBuilder resultBuilder = new StringBuilder(inputString.Length * repeatCount);

            for (int i = 0; i < repeatCount; i++)
            {
                resultBuilder.Append(inputString);
            }

            return resultBuilder.ToString();
        }
        string RemoveSubstring(string originalString, string substringToRemove)
        {
            int lastIndexOfSubstring = originalString.IndexOf(substringToRemove);

            if (lastIndexOfSubstring != -1)
            {
                // Remove the last occurrence of the substring
                string resultString = originalString.Remove(lastIndexOfSubstring, substringToRemove.Length);
                return resultString;
            }
            else
            {
                // Substring not found, return the original string
                return originalString;
            }
        }
    }
}
