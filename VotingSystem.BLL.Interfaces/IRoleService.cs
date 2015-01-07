using System.Collections.Generic;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL.Interfaces
{
	public interface IRoleService
	{
		//REVIEW: Better name is GetAllRoles
		List<Role> GetRoles();
		Role GetRole(string roleName);
		void CreateRole(string roleName);
		//REVIEW: Nobody use it. Could be removed
		void UpdateRole(Role role);
		//REVIEW: Need to find one way in service methods naming. There method called DeleteRole in ICommentService we have Delete method.
		void DeleteRole(string roleName);
	}
}
