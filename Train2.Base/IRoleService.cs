using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train2.Base
{
    public interface IRoleService
    {
        RoleResult Add(BRole bUser);
        RoleResult Get(int id);
        RoleResult Edit(int id, BRole bUser);
        RoleResult Delete(int id);
        RoleResult GetAll();
    }
}
