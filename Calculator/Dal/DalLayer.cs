using Calculator.Context;
using Calculator.Models;
using Calculator.Table;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;

namespace Calculator.Dal
{
    public class DalLayer
    {
        public int GetCounter(string operatorName)
        {
            using (var db = new CalculatorContext())
            {
                // Query to get the Counter value for the specific operation
                int counter = db.Operation
                    .Where(op => op.Operator == operatorName)
                    .Select(op => op.CountLastMonth)
                    .SingleOrDefault();

                // Now 'counter' contains the Counter value for the specified operation
                return counter;
            }
        }

        public int InsertResult(string operatorParam, string fieldA, string fieldB,string result)
        {
            using (var db = new CalculatorContext())
            {
                // Check if the connection is closed, open it
                if (db.Database.GetDbConnection().State != System.Data.ConnectionState.Open)
                {
                    db.Database.OpenConnection();
                }

                // Create a SQL command to execute the stored procedure
                var cmd = db.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "STP_Insert_Result";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Add parameters if needed
                 cmd.Parameters.Add(new SqlParameter("FieldA", fieldA));
                 cmd.Parameters.Add(new SqlParameter("FieldB", fieldB));
                 cmd.Parameters.Add(new SqlParameter("Operator", operatorParam));
                 cmd.Parameters.Add(new SqlParameter("Result", result));

                // Execute the command
                using (var reader = cmd.ExecuteReader())
                {
                    var CalculateMemories = new List<CalculatorHistoryItem>();
                    // Process the results if there are any
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            int index = reader.GetOrdinal("OperatorId");
                            var value = reader.GetInt32(index);

                            // Example condition: Process rows where Column1 equals a specific value
                            return value;

                            // ... process other conditions or skip rows as needed
                        }
                    }
                    return 0;

                }


            }
        }
        public IEnumerable<CalculatorHistoryItem> STPGetMonthlyOperation(string operatorValue)
        {
            using (var db = new CalculatorContext())
            {
                // Check if the connection is closed, open it
                if (db.Database.GetDbConnection().State != System.Data.ConnectionState.Open)
                {
                    db.Database.OpenConnection();
                }

                // Create a SQL command to execute the stored procedure
                var cmd = db.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "STP_Get_History_Operation";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // Add parameters if needed
                cmd.Parameters.Add(new SqlParameter("Operator", operatorValue));

                // Execute the command
                using (var reader = cmd.ExecuteReader())
                {
                    var CalculateMemories = new List<CalculatorHistoryItem>();
                    // Process the results if there are any
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            int fieldAColumnIndex = reader.GetOrdinal("FieldA");
                            int fieldBColumnIndex = reader.GetOrdinal("FieldB");
                            int resultIndex = reader.GetOrdinal("Result");
                            int CalculateDateIndex = reader.GetOrdinal("CalculateDate");
                            int operatorIdIndex = reader.GetOrdinal("OperationId");
                            var fieldAValue = reader.GetString(fieldAColumnIndex);
                            var fieldBValue = reader.GetString(fieldBColumnIndex);
                            var resultValue = reader.GetString(resultIndex);
                            var operatorIdValue = reader.GetInt32(operatorIdIndex);
                            var DateValue = reader.GetDateTime(CalculateDateIndex);

                            // Example condition: Process rows where Column1 equals a specific value
                            if (fieldAValue != null && fieldBValue != null && resultValue != null)
                            {
                                CalculateMemories.Add(new CalculatorHistoryItem()
                                {
                                    FieldA = fieldAValue,
                                    FieldB = fieldBValue,
                                    Operator = operatorValue,
                                    Result = resultValue,
                                    UpdateDate = DateValue,
                                    OperatorId = operatorIdValue
                                });
                            }

                            // ... process other conditions or skip rows as needed
                        }
                    }
                    return CalculateMemories;
                }
            }
        }

        internal IEnumerable<string> GetOpeation()
        {
            using (var db = new CalculatorContext())
            {
                List<string> distinctOperators = db.Operation
                             .Select(op => op.Operator)
                             .Distinct()
                             .ToList();

                return distinctOperators;
            }
        }
    }
}
