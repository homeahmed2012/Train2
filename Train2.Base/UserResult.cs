using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train2.Base
{
    public class UserResult
    {
        public List<BUser> bUsers { get; set; }
        public bool IsValid { get; set; }
        public IList<string> ErrorMessages { get; set; }

        public UserResult()
        {
            bUsers = new List<BUser>();
            IsValid = true;
            ErrorMessages = new List<string>();
        }
    }
}
