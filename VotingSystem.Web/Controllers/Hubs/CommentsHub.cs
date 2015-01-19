using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using VotingSystem.Web.Filters;
using VotingSystem.Web.Models;

namespace VotingSystem.Web.Controllers.Hubs
{
	public class CommentsHub : Hub
	{
		private static readonly ConnectionMapping<string> Connections = new ConnectionMapping<string>();

		public override Task OnConnected()
		{
			Connections.Add(Context.ConnectionId, "");
			return base.OnConnected();
		}

		public override Task OnDisconnected()
		{
			Connections.Remove(Context.ConnectionId);
			return base.OnDisconnected();
		}

		public override Task OnReconnected()
		{
			Connections.AddIfNotExists(Context.ConnectionId, "");
			return base.OnReconnected();
		}

		[HttpPost]
		[CustomAuthorizeHub]
		public void CreateComment(CommentModel comment)
		{
			IList<string> clients= Connections.GetConnections(comment.VotingId.ToString());
			clients.Remove(Context.ConnectionId);
			Clients.Clients(clients).createComment(comment);
		}

		[HttpPost]
		public void DeleteComment(int commentId, string themeId)
		{
			IList<string> clients = Connections.GetConnections(themeId);
			clients.Remove(Context.ConnectionId);
			Clients.Clients(clients).deleteComment(commentId);
		}

		[HttpPost]
		public void ChangePage(string votingId)
		{
			Connections.Add(Context.ConnectionId, votingId);
		}
	}
}