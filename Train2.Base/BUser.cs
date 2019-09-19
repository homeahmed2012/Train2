using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Train2.Base
{
    public class BUser
    {
        public int UserId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        public int? GroupId { get; set; }

        public string GroupName { get; set; }

        [Required]
        [MaxLength(300)]
        public string Password { get; set; }
    }
}
