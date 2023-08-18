using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParalelAsync.Entities
{
    internal class Person
    {
        // Entidad Person de base de datos de ejemplo AdventureWorsk2022
        // [AdventureWorks2022].[Person].[Person]

        public int BusinessEntityID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime ModifiedDate { get; set; } = DateTime.MinValue;


    }
}
