using System.Collections.Generic;

namespace Train2.Base
{
    public class RoleResult
    {
        public List<BRole> bRoles { get; set; }
        public bool IsValid { get; set; }
        public IList<string> ErrorMessages { get; set; }

        public RoleResult()
        {
            bRoles = new List<BRole>();
            IsValid = true;
            ErrorMessages = new List<string>();
        }
    }
}
