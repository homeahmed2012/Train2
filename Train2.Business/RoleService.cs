using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Train2.Base;
using Train2.GroupsManagement;

namespace Train2.Business
{
    public class RoleService : IRoleService
    {
        private GroupsManagementDbEntities _context;

        public RoleResult Add(BRole bRole)
        {
            RoleResult roleResult = new RoleResult();
            using (_context = new GroupsManagementDbEntities())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Role role = new Role()
                        {
                            RoleName = bRole.Name
                        };
                        _context.Roles.Add(role);
                        _context.SaveChanges();
                        transaction.Commit();
                        roleResult.bRoles.Add(Conversions.ConvertRoleToBRole(role));
                    }
                    catch (Exception e)
                    {
                        roleResult.IsValid = false;
                        roleResult.ErrorMessages.Add(e.Message);
                    }
                } 
            }
            return roleResult;
        }

        public RoleResult Delete(int id)
        {
            RoleResult roleResult = new RoleResult();
            using (_context = new GroupsManagementDbEntities())
            {
                try
                {
                    Role role = _context.Roles.FirstOrDefault(r => r.RoleId == id);
                    if (role != null)
                    {
                        roleResult.bRoles.Add(Conversions.ConvertRoleToBRole(role));
                        _context.Roles.Remove(role);
                        _context.SaveChanges();
                    }
                    else
                    {
                        roleResult.IsValid = false;
                        roleResult.ErrorMessages.Add("This role doesn't exist");
                    }
                }
                catch (Exception e)
                {
                    roleResult.IsValid = false;
                    roleResult.ErrorMessages.Add(e.Message);
                } 
            }
            return roleResult;
        }

        public RoleResult Edit(int id, BRole bRole)
        {
            RoleResult roleResult = new RoleResult();
            using (_context = new GroupsManagementDbEntities())
            {
                try
                {
                    Role role = _context.Roles.FirstOrDefault(r => r.RoleId == id);
                    if (role != null)
                    {
                        role.RoleName = bRole.Name;
                        _context.SaveChanges();
                        roleResult.bRoles.Add(Conversions.ConvertRoleToBRole(role));
                    }
                    else
                    {
                        roleResult.IsValid = false;
                        roleResult.ErrorMessages.Add("This role doesn't exist.");
                    }
                }
                catch (Exception e)
                {
                    roleResult.IsValid = false;
                    roleResult.ErrorMessages.Add(e.Message);
                } 
            }
            return roleResult;
        }

        public RoleResult Get(int id)
        {
            var roleResult = new RoleResult();
            using (_context = new GroupsManagementDbEntities())
            {
                try
                {
                    Role role = _context.Roles.FirstOrDefault(r => r.RoleId == id);
                    if (role != null)
                    {
                        roleResult.bRoles.Add(Conversions.ConvertRoleToBRole(role));
                    }
                    else
                    {
                        roleResult.IsValid = false;
                        roleResult.ErrorMessages.Add("Role not found");
                    }
                }
                catch (Exception e)
                {
                    roleResult.IsValid = false;
                    roleResult.ErrorMessages.Add(e.Message);
                } 
            }
            return roleResult;
        }

        public RoleResult GetAll()
        {
            var roleResult = new RoleResult();
            using (_context = new GroupsManagementDbEntities())
            {
                try
                {
                    var roles = _context.Roles.ToList().Select(r => Conversions.ConvertRoleToBRole(r));
                    roleResult.bRoles = roles.ToList();
                }
                catch (Exception e)
                {
                    roleResult.IsValid = false;
                    roleResult.ErrorMessages.Add(e.Message);
                } 
            }
            return roleResult;
        }
    }
}
