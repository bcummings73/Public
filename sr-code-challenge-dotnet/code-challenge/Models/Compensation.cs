using Microsoft.Rest.Serialization;
using System;
using System.ComponentModel.DataAnnotations;

namespace challenge.Models
{
    public class Compensation
    {
       

        public Employee employee  { get; set; }
        public string salary {get; set; }
        public  string effectiveDate { get; set; }
        [Key]
        public string employeeID { get; set; }

    }

}
