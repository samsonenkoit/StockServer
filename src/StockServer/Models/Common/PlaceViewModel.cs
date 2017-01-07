using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockServer.Models.Common
{
    public class PlaceViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        [Required]
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        [Required]
        public double Longitude { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Contact { get; set; }

        [Required]
        public string LogoUrl { get; set; }
        
        [Required]
        public string ViewUrl { get; set; }
    }
}
