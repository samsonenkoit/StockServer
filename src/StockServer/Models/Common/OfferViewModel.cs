using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockServer.Models.Common
{
    public class OfferViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public int PlaceId { get; set; }
    }
}
