using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RickLocalization_api.EF
{
    public class Dimension
    {
        public int Dimensionid { get; set; }
        public string Name { get; set; }

        [DataType("image")]
        public byte[] Picture { get; set; }
        
    }
}