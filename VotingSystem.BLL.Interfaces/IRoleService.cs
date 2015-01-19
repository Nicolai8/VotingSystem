using System.Collections.Generic;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface IRoleService
	{
		List<Role> GetAllRoles();

		Role GetRole(string roleName);

		void CreateRole(string roleName);

		void DeleteRole(string roleName);
	}
}
