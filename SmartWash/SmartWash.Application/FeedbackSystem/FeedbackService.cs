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
		private readonly INotificationRepository _notificationRepository;
		private readonly UserManager<ApplicationUser> _adminManager;

		public FeedbackService(IFeedbackRepository feedbackRepository, UserManager<ApplicationUser> adminManager,
			IReplyRepository replyRepository, INotificationRepository notificationRepository)
		{
			_feedbackRepository = feedbackRepository;
			_adminManager = adminManager;
			_replyRepository = replyRepository;
			_notificationRepository = notificationRepository;
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
			var notification = await _notificationRepository.AddAsync(new Notification
			{
                UserID = user.Id,
                Content = $"New reply from admin at {reply.ReplyDateTime}: {reply.Content}",
				Created = DateTime.Now
            });

			return reply;
		}

		public async Task<Feedback> NotifyAdminsAsync(Feedback feedback)
		{

			var admins = _adminManager.Users.ToList();
			var user = await _adminManager.FindByIdAsync(feedback.UserId);
			foreach (var admin in admins)
			{
				var validAdmin = await _adminManager.GetRolesAsync(admin);
				if (validAdmin.Contains("Admin"))
				{

                    var notification = await _notificationRepository.AddAsync(new Notification
                    {
                        UserID = admin.Id,
                        Content = $"New reply from {user.FullName} at {feedback.FeedbackDateTime} - {feedback.Title}",
                        Created = DateTime.Now
                    });
				}
			}

			return feedback;
		}
	}
}
