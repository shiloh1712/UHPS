﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UHPostalService.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Supervisor")]
        public int SupID { get; set; } 
        public Employee Supervisor { get; set; }
        //list of regular employees
        //public List<Employee> Employees { get; set; }
        public int AddressID { get; set; }
        public Address Address { get; set; }

    }
}
