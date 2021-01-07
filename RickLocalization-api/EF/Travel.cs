using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System;

namespace RickLocalization_api.EF
{
    public class Travel
    {
        [Key]
        public int Traveid { get; set; }
        public int Rickid { get; set; }
        public Dimension DimensionIdSource { get; set; }
        public Dimension DimensionIdTarget { get; set; }
        public DateTime TravelDate { get; set; }
        public Rick Rick { get; set; }

        
        
    }
}