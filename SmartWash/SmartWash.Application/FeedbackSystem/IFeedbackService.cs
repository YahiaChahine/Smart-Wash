﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Entities;

namespace SmartWash.Application.FeedbackSystem
{
    public interface IFeedbackService
    {
        Task<Feedback> SubmitFeedbackAsync(Feedback feedback);
        Task<Feedback> NotifyAdminsAsync(Feedback feedback);
    }
}
