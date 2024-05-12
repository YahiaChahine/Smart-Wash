using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartWash.Domain.Entities;

namespace SmartWash.Application.UserSystem
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUser();
        Task<string> GetUserId();
        Task<string> GetUserName();
        Task<string> GetUserEmail();
        Task<string> GetUserRole();
    }
}
