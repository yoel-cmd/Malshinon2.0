using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon2._0.Models
{
    internal class people
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecretCode { get; set; }
        public int ReportCount { get; set; }
        public string Status { get; set; }
        //------------------------------------------------------------------------
        public people(string FirstName, string LastName, string SecretCode, int ReportCount, string Status)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.SecretCode = SecretCode;
            this.ReportCount = ReportCount;
            this.Status = Status;
        }
        //------------------------------------------------------------------------
        public void PrintInfo()
        {
            Console.WriteLine($"FirstName:{FirstName}");
            Console.WriteLine($"LastName:{LastName}");
            Console.WriteLine($"SecretCode:{SecretCode}");
            Console.WriteLine($"ReportCount:{ReportCount}");
            Console.WriteLine($"Status:{Status}");
            Console.WriteLine("---------------");
        }
        //--------
    }
}
