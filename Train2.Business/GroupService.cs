using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Train2.Base;
using Train2.GroupsManagement;

namespace Train2.Business
{
    public class GroupService : IGroupService
    {

        private GroupsManagementDbEntities _context;

        public GroupResult Add(BGroup bGroup)
        {
            var groupResult = new GroupResult();
            using (_context = new GroupsManagementDbEntities())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Group group = new Group
                        {
                            Name = bGroup.Name
                        };
                        _context.Groups.Add(group);
                        _context.SaveChanges();

                        transaction.Commit();

                        groupResult.bGroups.Add(new BGroup()
                        {
                            GroupId = group.GroupId,
                            Name = group.Name
                        });
                    }
                    catch (Exception e)
                    {
                        groupResult.IsValid = false;
                        groupResult.ErrorMessages.Add(e.Message);
                    }
                }
            }
            return groupResult;
        }

        public GroupResult Delete(int id)
        {
            var groupResult = new GroupResult();
            using (_context = new GroupsManagementDbEntities())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Group group = _context.Groups.Find(id);
                        _context.Groups.Remove(group);
                        _context.SaveChanges();

                        transaction.Commit();

                        groupResult.bGroups.Add(new BGroup()
                        {
                            GroupId = group.GroupId,
                            Name = group.Name
                        });
                    }
                    catch (Exception e)
                    {
                        groupResult.IsValid = false;
                        groupResult.ErrorMessages.Add(e.Message);
                    }
                } 
            }
            return groupResult;
        }

        public GroupResult Edit(int id, BGroup bGroup)
        {
            var groupResult = new GroupResult();
            using (_context = new GroupsManagementDbEntities())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Group group = _context.Groups.Find(id);
                        group.Name = bGroup.Name;
                        _context.SaveChanges();

                        transaction.Commit();

                        groupResult.bGroups.Add(new BGroup
                        {
                            GroupId = group.GroupId,
                            Name = group.Name
                        });
                    }
                    catch (Exception e)
                    {
                        groupResult.IsValid = false;
                        groupResult.ErrorMessages.Add(e.Message);
                    }
                } 
            }
            return groupResult;
        }

        public GroupResult Get(int id)
        {
            GroupResult groupResult = new GroupResult();

            using (_context = new GroupsManagementDbEntities())
            {
                try
                {
                    Group group = _context.Groups.Find(id);
                    groupResult.bGroups.Add(new BGroup()
                    {
                        GroupId = group.GroupId,
                        Name = group.Name
                    });
                }
                catch (Exception e)
                {
                    groupResult.IsValid = false;
                    groupResult.ErrorMessages.Add(e.Message);
                } 
            }
            return groupResult;
        }

        public GroupResult GetAll()
        {
            var groupReslut = new GroupResult();
            using (_context = new GroupsManagementDbEntities())
            {
                try
                {
                    IList<BGroup> groups = _context.Groups.Select(g => new BGroup { GroupId = g.GroupId, Name = g.Name }).ToList();
                    groupReslut.bGroups = groups;
                }
                catch (Exception e)
                {
                    groupReslut.IsValid = false;
                    groupReslut.ErrorMessages.Add(e.Message);
                }
            }
            return groupReslut;
        }
    }
}
