using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Train2.Base;
using Train2.GroupsManagement;


namespace Train2.Business
{
    public class UserService : IUserService
    {
        private GroupsManagementDbEntities _context;


        public UserResult Add(BUser bUser)
        {
            UserResult userResult = new UserResult();
            using (_context = new GroupsManagementDbEntities())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // check if group exists
                        Group group = _context.Groups.FirstOrDefault(g => g.GroupId == bUser.GroupId);
                        if (group != null)
                        {
                            User user = _context.Users.Add(Conversions.ConvertBUserToUser(bUser));
                            _context.SaveChanges();
                            transaction.Commit();

                            _context.Entry(user).Reference(u => u.Group).Load();
                            BUser newBUser = Conversions.ConvertUserToBUser(user);
                            userResult.bUsers.Add(newBUser);
                        }
                        else
                        {
                            throw new Exception("Can't create the user because this group doesn't exist");
                        }
                    }
                    catch (Exception e)
                    {
                        userResult.IsValid = false;
                        userResult.ErrorMessages.Add(e.Message);
                    } 
                }
            }
            return userResult;
        }


        public UserResult Delete(int id)
        {
            UserResult userResult = new UserResult();
            using (_context = new GroupsManagementDbEntities())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        User user = _context.Users.Include("Group").FirstOrDefault(u => u.UserId == id);
                        BUser bUser = Conversions.ConvertUserToBUser(user);
                        bUser.GroupName = user.Group.Name;
                        userResult.bUsers.Add(bUser);

                        _context.Users.Remove(user);
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        userResult.IsValid = false;
                        userResult.ErrorMessages.Add(e.Message);
                    }
                } 
            }

            return userResult;
        }


        public UserResult Edit(int id, BUser bUser)
        {
            UserResult userResult = new UserResult();
            using (_context = new GroupsManagementDbEntities())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        User user = _context.Users.Find(id);
                        user.UserName = bUser.Name;
                        user.Email = bUser.Email;
                        user.GroupId = bUser.GroupId;
                        _context.SaveChanges();
                        transaction.Commit();

                        userResult.bUsers.Add(Conversions.ConvertUserToBUser(user));
                    }
                    catch (Exception e)
                    {
                        userResult.IsValid = false;
                        userResult.ErrorMessages.Add(e.Message);
                    }
                } 
            }

            return userResult;
        }

        // Get user by UserId
        public UserResult Get(int id)
        {
            UserResult userResult = new UserResult();
            using (_context = new GroupsManagementDbEntities())
            {
                try
                {
                    User user = _context.Users.FirstOrDefault(u => u.UserId == id);
                    if (user != null)
                    {
                        userResult.bUsers.Add(Conversions.ConvertUserToBUser(user));
                    }
                    else
                    {
                        userResult.IsValid = false;
                        userResult.ErrorMessages.Add("There is no user with this Id");
                    }

                }
                catch (Exception e)
                {
                    userResult.IsValid = false;
                    userResult.ErrorMessages.Add(e.Message);
                } 
            }

            return userResult;
        }


        public UserResult GetAll()
        {
            UserResult userResult = new UserResult();
            using (_context = new GroupsManagementDbEntities())
            {
                try
                {
                    IEnumerable<BUser> users = _context.Users.ToList().Select(u => Conversions.ConvertUserToBUser(u));
                    userResult.bUsers = users.ToList();
                }
                catch (Exception e)
                {
                    userResult.IsValid = false;
                    userResult.ErrorMessages.Add(e.Message);
                } 
            }

            return userResult;
        }


        public UserResult GetByEmail(string email)
        {
            UserResult userResult = new UserResult();
            using (_context = new GroupsManagementDbEntities())
            {
                try
                {
                    User user = _context.Users.FirstOrDefault(u => u.Email == email);
                    if (user != null)
                    {
                        userResult.bUsers.Add(Conversions.ConvertUserToBUser(user));
                    }
                    else
                    {
                        userResult.IsValid = false;
                        userResult.ErrorMessages.Add("There is no user with this email");
                    }
                }
                catch (Exception e)
                {
                    userResult.IsValid = false;
                    userResult.ErrorMessages.Add(e.Message);
                } 
            }
            return userResult;
        }


        public bool CheckPasswork(int userId, string password)
        {
            using (_context = new GroupsManagementDbEntities())
            {
                try
                {
                    var user = _context.Users.First(u => u.UserId == userId);
                    if (user.Password == password)
                    {
                        return true;
                    }

                }
                catch (Exception e)
                {
                    return false;
                } 
            }
            return false;
        }


        public bool UserHasRole(string email, string roleName)
        {
            UserResult userResult = GetByEmail(email);
            using (_context = new GroupsManagementDbEntities())
            {
                if (userResult.IsValid)
                {
                    BUser bUser = userResult.bUsers.Single();
                    // get the user group
                    Group group = _context.Groups.FirstOrDefault(g => g.GroupId == bUser.GroupId);
                    if (group != null)
                    {
                        var groupRole = _context.GroupsRoles.
                            Where(gr => gr.GroupId == group.GroupId).
                            Where(gr => gr.Role.RoleName == roleName).FirstOrDefault();
                        if (groupRole != null)
                        {
                            return true;
                        }
                    }
                } 
            }
            return false;
        }
    }
}
