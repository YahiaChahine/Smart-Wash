using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using SmartWash.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SmartWash.WebUI.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private string userRole = "Member";

        public RegisterModel( UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
            _roleManager = roleManager;
		}

        public InputModel input { get; set; }
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
                var userExists = await _userManager.FindByEmailAsync(input.Email);
                if (userExists != null)
                {
                    ModelState.AddModelError(string.Empty, "Email is already in use. Login or try another Email");
                    return Page();
                }
                var identity = new ApplicationUser { UserName = input.Fullname, Email = input.Email, PhoneNumber = input.PhoneNumber};
                var result = await _userManager.CreateAsync(identity, input.Password);

                var role = new IdentityRole(userRole);
                var roleExists = await _roleManager.FindByNameAsync(userRole);
                if (roleExists == null)
                {
                    var addRoleResults = await _roleManager.CreateAsync(role);
                    if (!addRoleResults.Succeeded)
                    {
                        return Page();
                    }
                }

                var addUserRoleResult = await _userManager.AddToRoleAsync(identity, userRole);

                if (result.Succeeded) {
                    await _signInManager.SignInAsync(identity, isPersistent: false);
                    return LocalRedirect(ReturnUrl);
                }
            }

            return Page();
        }

        //add inputmodel
        public class InputModel
        {
			[Required]
			[Display(Name = "Full Name")]
			public string Fullname { get; set; }

			[Required]
            [EmailAddress]
            [Display(Name = "Email")]
			public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
			public string Password { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			public string ConfirmPassword { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }
		}   

    }
}
