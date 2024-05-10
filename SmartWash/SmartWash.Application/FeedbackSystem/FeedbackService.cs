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
	public class FeedbackService
	{
		private readonly IFeedbackRepository _feedbackRepository;
		private readonly UserManager<Admin> _adminManager;

		public FeedbackService(IFeedbackRepository feedbackRepository, UserManager<Admin> adminManager)
		{
			_feedbackRepository = feedbackRepository;
			_adminManager = adminManager;
		}
		public async Task SubmitFeedbackAsync(Feedback feedback)
		{
			// Validate feedback data if necessary
			await _feedbackRepository.AddAsync(feedback);

			// Notify all admins about the new feedback
			await NotifyAdminsAsync(feedback);
		}

		private async Task NotifyAdminsAsync(Feedback feedback)
		{
			var admins = await _adminManager.Users.ToListAsync(); // Assuming you have a method to fetch all admin users
			foreach (var admin in admins)
			{
				admin.Notifications.Add($"New feedback from user {feedback.User.UserName}");
			}
		}
	}
}
