
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RickLocalization_api.EF
{
    public class Rick
    {
        public int Rickid { get; set; }
        public string Name { get; set; }
        public int Dimensionid { get; set; }
        public int Mortyid { get; set; }
        public Dimension Dimension { get; set; }
        public Morty Morty { get; set; }

        [DataType("image")]
        public byte[] Picture { get; set; }




    }
}
