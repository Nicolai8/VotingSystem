using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystem.BLL.Interfaces;
using VotingSystem.DAL;
using VotingSystem.DAL.Entities;

namespace VotingSystem.BLL
{
	public class RoleService : BaseService, IRoleService
	{
		public RoleService(IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
		}

		public List<Role> GetRoles()
		{
			return UnitOfWork.RoleRepository.Query()
				.OrderBy(r => r.OrderBy(role => role.RoleName))
				.Get().ToList();
		}

		public Role GetRole(string roleName)
		{
			return UnitOfWork.RoleRepository.Query()
				.Filter(r => r.RoleName.Equals(roleName, StringComparison.InvariantCultureIgnoreCase))
				.Include(r => r.Users).Get().SingleOrDefault();
		}

		public void CreateRole(string roleName)
		{
			UnitOfWork.RoleRepository.Insert(new Role
				{
					RoleName = roleName
				});
			UnitOfWork.Save();
		}

		public void UpdateRole(Role role)
		{
			UnitOfWork.RoleRepository.Update(role);
			UnitOfWork.Save();
		}

		public void DeleteRole(string roleName)
		{
			Role role = GetRole(roleName);
			if (role == null) return;
			UnitOfWork.RoleRepository.Delete(role);
			UnitOfWork.Save();
			//REVIEW: Better to rewrite like this:
			//Role role = GetRole(roleName);
			//if (role != null)
			//{
			//	UnitOfWork.RoleRepository.Delete(role);
			//	UnitOfWork.Save();
			//}
		}
	}
}
