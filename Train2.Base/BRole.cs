using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train2.Base
{
    public class BRole
    {
        public int RoleId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}
