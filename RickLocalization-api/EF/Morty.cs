
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RickLocalization_api.EF
{
    public class Morty
    {
        public int Mortyid { get; set; }
        public string Name { get; set; }

        [DataType("image")]
        public byte[] Picture { get; set; }


    }
}