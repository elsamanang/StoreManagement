using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.App.DTOs
{
#nullable disable
    public class NewProduct
    {
        public string Label { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CategoryId { get; set; }
    }
}
