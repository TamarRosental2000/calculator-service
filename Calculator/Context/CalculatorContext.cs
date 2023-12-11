using Calculator.Table;
using Microsoft.EntityFrameworkCore;

namespace Calculator.Context
{
    public class CalculatorContext : DbContext
    {
        public DbSet<CalculateMemory> CalculateMemory { get; set; }
        public DbSet<Operation> Operation { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-10JSI7Q1\\SQL2019;Database=Calculator;User Id=webuser;Trusted_Connection=True;Encrypt=false");
        }
    }
}
