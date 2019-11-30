using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Exam.OOP
{
    public class Mammal
    {
        // suppose it's too obvious
        public string ToString2()
        {
            return this.ToString();
        }
    }

    public class Employee: Mammal
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
    }

    public class SalesManager : Employee
    {
        public int BonusPerSale { get; set; }
        public int SalesThisMonth { get; set; }
    }

    public class CustomerServiceAgent : Employee
    {
        public int Customers { get; set; }
    }

    public class Dog: Mammal
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
