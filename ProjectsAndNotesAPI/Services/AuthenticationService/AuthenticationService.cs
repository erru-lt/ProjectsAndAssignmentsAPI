using Microsoft.AspNetCore.Identity;
using ProjectsAndNotesAPI.Data.Identity;
using ProjectsAndNotesAPI.Models;

namespace ProjectsAndNotesAPI.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthenticationService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> LoginAsync(Login loginModel)
        {
            var user = await FindUserByEmailAsync(loginModel.Email);

            if (user == null)
            {
                Console.WriteLine("no user");
                return false;
            }

            return await PasswordCheck(loginModel, user);
        }

        public async Task<bool> RegisterAsync(Register registerModel)
        {
            IdentityUser? user = await FindUserByEmailAsync(registerModel.Email);

            if (user != null)
            {
                return false;
            }

            var newUser = new IdentityUser
            {
                UserName = registerModel.Email,
                Email = registerModel.Email,
            };

            var result = await _userManager.CreateAsync(newUser, registerModel.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, IdentityData.UserRole);
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                return true;
            }

            return false;
        }

        private async Task<bool> PasswordCheck(Login loginModel, IdentityUser user)
        {
            var isPasswordMatch = await _userManager.CheckPasswordAsync(user, loginModel.Password);

            if (isPasswordMatch)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private async Task<IdentityUser?> FindUserByEmailAsync(string emailAdress)
        {
            return await _userManager.FindByEmailAsync(emailAdress);
        }
    }
}
