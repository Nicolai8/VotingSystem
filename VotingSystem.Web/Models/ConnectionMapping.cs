using System.Collections.Generic;
using System.Linq;

namespace VotingSystem.Web.Models
{
	public class ConnectionMapping<T>
	{
		private readonly Dictionary<T, string> _connections;

		public int Count
		{
			get
			{
				return _connections.Count;
			}
		}

		public ConnectionMapping()
		{
			_connections = new Dictionary<T, string>();
		}

		public void Add(T key, string value)
		{
			lock (_connections)
			{
				_connections[key] = value;
			}
		}

		public void AddIfNotExists(T key, string value)
		{
			if (!Exists(key))
			{
				Add(key, "");
			}
		}

		public IList<T> GetConnections(string value)
		{
			return _connections.Where(c => c.Value != null && c.Value.Equals(value)).Select(c => c.Key).ToList();
		}

		public bool Exists(T key)
		{
			return _connections.ContainsKey(key);
		}

		public void Remove(T key)
		{
			lock (_connections)
			{
				_connections.Remove(key);
			}
		}
	}
}