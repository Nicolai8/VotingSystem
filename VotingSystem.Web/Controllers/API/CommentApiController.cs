﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using VotingSystem.BLL.Interfaces;
using VotingSystem.DAL.Entities;
using VotingSystem.Web.Filters;
using VotingSystem.Web.Helpers;
using VotingSystem.Web.Models;

namespace VotingSystem.Web.Controllers.API
{
	[RoutePrefix("api/comment")]
	[CustomAuthorizeApi]
	public class CommentApiController : BaseApiController
	{
		private readonly ICommentService _commentService;

		public CommentApiController(ICommentService commentService)
		{
			_commentService = commentService;
		}

		[Route("")]
		[HttpGet]
		public IEnumerable<CommentModel> Get(int page, int size)
		{
			List<Comment> comments = _commentService.GetByUserId(UserId, page, size);
			return comments.Select(comment => comment.ToCommentModel(User.Identity.Name));
		}

		[Route("total", Name = "TotalComments")]
		[HttpGet]
		public int GetTotal()
		{
			return _commentService.GetMyTotal(UserId);
		}

		[Route("")]
		[HttpPost]
		public CommentModel Post([FromBody]Comment comment)
		{
			comment.UserId = UserId;
			comment.CreateDate = DateTime.UtcNow;
			_commentService.Insert(comment);
			return comment.ToCommentModel(User.Identity.Name);
		}

		[Route("{id:int}")]
		[OwnsApi(OwnsParameter = OwnsType.Comment)]
		[HttpDelete]
		public void Delete(int id)
		{
			_commentService.Delete(id);
		}
	}
}