using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train2.Base
{
    public class BGroup
    {
        public int GroupId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
    }
}
