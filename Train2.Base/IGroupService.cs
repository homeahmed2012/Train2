using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train2.Base
{
    public interface IGroupService
    {
        GroupResult Add(BGroup bGroup);
        GroupResult Get(int id);
        GroupResult Edit(int id, BGroup bGroup);
        GroupResult Delete(int id);
        GroupResult GetAll();
    }
}
