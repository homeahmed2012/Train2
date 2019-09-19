using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Train2.Base;

namespace Train2.GroupsMVC.Serializers
{
    public class BUserSerializer
    {

        public BUser GetBUser(string serializedUser)
        {
            return JsonConvert.DeserializeObject<BUser>(serializedUser);            
        }


        public IList<BUser> GetBUsers(string serializedUsers)
        {
            return JsonConvert.DeserializeObject<List<BUser>>(serializedUsers);
        }
    }
}