using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Train2.GroupsManagement;
using Train2.Base;

namespace Train2.Business
{
    public static class Conversions
    {
        public static BUser ConvertUserToBUser(User user)
        {
            BUser bUser = new BUser
            {
                UserId = user.UserId,
                Name = user.UserName,
                Email = user.Email,
                GroupId = user.GroupId
            };

            if (user.Group != null)
            {
                bUser.GroupName = user.Group.Name;
            }

            return bUser;
        }

        public static User ConvertBUserToUser(BUser bUser)
        {
            User user = new User
            {
                UserName = bUser.Name,
                Email = bUser.Email,
                GroupId = bUser.GroupId
            };
            if (!String.IsNullOrEmpty(bUser.Password))
            {
                user.Password = bUser.Password;
            }
            return user;
        }

        public static BGroup ConvertGroupToBGroup(Group group)
        {
            BGroup bGroup = new BGroup()
            {
                GroupId = group.GroupId,
                Name = group.Name
            };

            return bGroup;
        }

        public static  BRole ConvertRoleToBRole(Role role)
        {
            BRole bRole = new BRole()
            {
                RoleId = role.RoleId,
                Name = role.RoleName
            };
            return bRole;
        }
    }
}
