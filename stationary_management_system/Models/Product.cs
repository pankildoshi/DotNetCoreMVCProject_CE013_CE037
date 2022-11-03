using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace stationary_management_system.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Quantity { get; set; }

    }
    public class Billlocal
    { 
        public int Id { get; set; }
        [Key]
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Totalprice { get; set; }       
    }
    public class Bill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public int GrandTotal { get; set; }

        public IList<BillItem> BillItems { get; set; }
    }

    public class BillItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Totalprice { get; set; }

        [Required]
        public Bill Bill { get; set; }
    }
}
