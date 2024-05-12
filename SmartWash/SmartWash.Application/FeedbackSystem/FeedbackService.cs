using SmartWash.Domain.Entities;
using SmartWash.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SmartWash.Application.FeedbackSystem
{
	public class FeedbackService : IFeedbackService
	{
		private readonly IFeedbackRepository _feedbackRepository;
		private readonly IReplyRepository _replyRepository;
		private readonly UserManager<ApplicationUser> _adminManager;

		public FeedbackService(IFeedbackRepository feedbackRepository, UserManager<ApplicationUser> adminManager,
			IReplyRepository replyRepository)
		{
			_feedbackRepository = feedbackRepository;
			_adminManager = adminManager;
			_replyRepository = replyRepository;
		}
		public async Task<Feedback> SubmitFeedbackAsync(Feedback feedback)
		{
			// Validate feedback data if necessary
			await _feedbackRepository.AddAsync(feedback);

			// Notify all admins about the new feedback
			await NotifyAdminsAsync(feedback);

			return feedback;
		}

		public async Task<Reply> ReplyToFeedback(Reply reply)
		{
			await _replyRepository.AddAsync(reply);

			await NotifyUserAsync(reply);
			return reply;
		}


		public async Task<Reply> NotifyUserAsync(Reply reply)
		{
			var feedback = await _feedbackRepository.GetByIdAsync(reply.FeedbackId);
			var user = await _adminManager.FindByIdAsync(feedback.UserId);
			if (user.Notifications == null)
			{
				user.Notifications = new List<string>();
			}
			user.Notifications.Add($"New reply from admin {reply.User.UserName} at {reply.ReplyDateTime}");
			return reply;
		}

		public async Task<Feedback> NotifyAdminsAsync(Feedback feedback)
		{

			var admins = _adminManager.Users.ToList();
			foreach (var admin in admins)
			{
				var validAdmin = await _adminManager.GetRolesAsync(admin);
				if (validAdmin.Contains("Admin"))
				{
                    if (admin.Notifications == null)
                    {
                        admin.Notifications = new List<string>();
                    }
                    admin.Notifications.Add($"New feedback from user {feedback.User.UserName} at {feedback.FeedbackDateTime}");
				}
			}

			return feedback;
		}
	}
}
