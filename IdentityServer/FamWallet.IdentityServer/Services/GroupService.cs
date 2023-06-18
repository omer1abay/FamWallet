using FamWallet.IdentityServer.Data;
using FamWallet.IdentityServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FamWallet.IdentityServer.Services
{
    public class GroupService : IGroupService
    {

        private readonly ApplicationDbContext _applicationDbContext;

        public GroupService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void AddGroup(GroupModel model)
        {
            _applicationDbContext.Database.ExecuteSqlInterpolated($"INSERT INTO Groups (GroupName) VALUES({model.GroupName})");
        }

        public List<GroupModel> GetAllGroup()
        {
            var result = _applicationDbContext.Groups.FromSqlRaw("Select * From Groups");
            return result.ToList();
        }

        public List<ApplicationUser> GetAllUserInGroup(int groupId)
        {
            var result = _applicationDbContext.AspNetUsers.FromSqlRaw($"Select * From AspNetUser Where GroupId = {groupId}");
            return result.ToList();
        }

        public void RemoveGroup(GroupModel model)
        {
            _applicationDbContext.Groups.Remove(model);
        }
    }
}
