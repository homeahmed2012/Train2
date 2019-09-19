using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train2.Base
{
    //
    //
    //
    public interface IUserService
    {
        UserResult Add(BUser bUser);
        UserResult Get(int id);
        UserResult Edit(int id, BUser bUser);
        UserResult Delete(int id);
        UserResult GetAll();
    }
}
