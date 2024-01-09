using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace FileReadingWithMutua_Exclusion
{
    public static class EmployeeData
    {
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            Employee employee1 = new Employee {
                Id = 1,
                FirstName = "Tashanam",
                Department = "Software Dept",
                isMananger = true,
            };

            Employee employee2 = new Employee
            {
                Id = 2,
                FirstName = "Shahbaz",
                Department = "Admin",
                isMananger = true,
            };

            Employee employee3 = new Employee
            {
                Id = 3,
                FirstName = "Owais",
                Department = "SQA",
                isMananger = false,
            };

            Employee employee4 = new Employee
            {
                Id = 4,
                FirstName = "Sidra",
                Department = "Software Dept",
                isMananger = false,
            };

            Employee employee5 = new Employee
            {
                Id = 5,
                FirstName = "employee",
                Department = "Software Dept",
                isMananger = true,
            };

            Employee employee6 = new Employee
            {
                Id = 6,
                FirstName = "Hibba",
                Department = "Software Dept",
                isMananger = true,
            };

            Employee employee7 = new Employee
            {
                Id = 7,
                FirstName = "Rukhma",
                Department = "Software Dept",
                isMananger = false,
            };

            employees.Add(employee1);
            employees.Add(employee2);
            employees.Add(employee3);
            employees.Add(employee4);
            employees.Add(employee5);
            employees.Add(employee6);
            employees.Add(employee7);

            return employees;
        }

    }
}
