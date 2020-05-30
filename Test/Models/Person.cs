using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Autorest;
using Microsoft.AspNetCore.Mvc;

namespace Test.Models
{
    [GenerateController("api/persoon")]
    public class Person
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
