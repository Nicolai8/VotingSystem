using System.Collections.Generic;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface IRoleService
	{
		List<Role> GetRoles();
		Role GetRole(string roleName);
		void CreateRole(string roleName);
		void UpdateRole(Role role);
		void DeleteRole(string roleName);
	}
}
