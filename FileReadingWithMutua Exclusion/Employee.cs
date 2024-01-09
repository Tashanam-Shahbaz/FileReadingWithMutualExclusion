using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReadingWithMutua_Exclusion
{
    public class Employee
    {
        public int Id { get; set; }  
        public string FirstName { get; set; }= string.Empty;

        public string Department { get; set; } = string.Empty;

        public bool isMananger { get; set; }   = false;

    }
}
