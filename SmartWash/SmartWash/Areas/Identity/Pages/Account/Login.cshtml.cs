using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartWash.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartWash.WebUI.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {

        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

        public void OnGet()
        {
            ReturnUrl = Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ReturnUrl = Url.Content("~/");

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: false);
				if (result.Succeeded)
                {
					return LocalRedirect(ReturnUrl);
				}
				else
                {
					ModelState.AddModelError(string.Empty, "Invalid login attempt.");
				}
					return Page();
            }
        }

        public class InputModel
        {
            [Required]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
			public string Password { get; set; }
        }
    }
}
