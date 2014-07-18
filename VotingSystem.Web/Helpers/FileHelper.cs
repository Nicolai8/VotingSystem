using System;
using System.IO;
using System.Web;

namespace VotingSystem.Web.Helpers
{
	public static class FileHelper
	{
		public static string SavePicture(HttpServerUtilityBase server, HttpPostedFileBase picture)
		{
			string fileName = String.Concat(Guid.NewGuid(), DateTime.UtcNow.Ticks);
			fileName += Path.GetExtension(picture.FileName);
			string path = Path.Combine(server.MapPath(GlobalVariables.PicturesFolder), fileName);
			picture.SaveAs(path);
			return String.Concat(GlobalVariables.PicturesFolder.Replace("~/", ""), fileName);
		}

		public static void DeletePicture(HttpServerUtilityBase server, string path)
		{
			string serverPath = server.MapPath("~/") + (path ?? " ").Substring(1);
			if (!String.IsNullOrEmpty(path) && File.Exists(serverPath))
			{
				File.Delete(serverPath);
			}
		}
	}
}