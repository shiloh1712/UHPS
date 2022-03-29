using System;
using System.ComponentModel.DataAnnotations;

namespace test.Model
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        public string StatusOfPackage { get; set; } //Unsure of type

    }
}
