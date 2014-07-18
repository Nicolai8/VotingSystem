using System;
using System.Collections.Generic;

namespace VotingSystem.DAL.Entities
{
	public class Role : BaseEntity, IEqualityComparer<Role>
	{
		public string RoleName { get; set; }

		public virtual ICollection<User> Users { get; set; }

		public bool Equals(Role x, Role y)
		{
			return x.RoleName.Equals(y.RoleName, StringComparison.InvariantCultureIgnoreCase) && x.Id.Equals(y.Id);
		}

		public int GetHashCode(Role obj)
		{
			return obj.RoleName.GetHashCode() + obj.Id.GetHashCode();
		}
	}
}
