using FamWallet.IdentityServer.Models;
using System.Collections.Generic;

namespace FamWallet.IdentityServer.Services
{
    public interface IGroupService
    {
        public List<GroupModel> GetAllGroup();
        public void AddGroup(GroupModel model);
        public void RemoveGroup(GroupModel model);
        public List<ApplicationUser> GetAllUserInGroup(int groupId);

    }
}
