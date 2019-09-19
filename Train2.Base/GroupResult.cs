using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train2.Base
{
    public class GroupResult:IDisposable
    {
        public IList<BGroup> bGroups { get; set; }
        public bool IsValid { get; set; }
        public IList<string> ErrorMessages { get; set; }

        public GroupResult()
        {
            bGroups = new List<BGroup>();
            IsValid = true;
            ErrorMessages = new List<string>();
        }

        public void Dispose()
        {
            //Clear all memory object
        }
    }
}
