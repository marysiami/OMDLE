using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Omdle.Account.Contracts;
using Omdle.Account.Models;
using Omdle.Common.Exceptions;
using Omdle.Data.Contracts;
using Omdle.Data.Models.Account;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Omdle.Account.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthValidationService _authValidationService;
        private readonly IConfiguration _configuration;
        private readonly IDataService _dataService;
        private readonly SignInManager<OmdleUser> _signInManager;
        private readonly UserManager<OmdleUser> _userManager;

        public AuthService(UserManager<OmdleUser> userManager,
            SignInManager<OmdleUser> signInManager,
            IAuthValidationService authValidationService,
            IConfiguration configuration,
            IDataService dataService)
        {
            _authValidationService = authValidationService;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _dataService = dataService;
        }

        public async Task<string> Register(RegisterViewModel model)
        {
            await _authValidationService.ValidateRegisterViewModel(model);

            var newUser = new OmdleUser
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                throw new RegistrationFailedException(
                $"An error occured while registering user: {result.Errors.Select(e => e.Description).Join(", ")}");
            }

            await _userManager.AddToRoleAsync(newUser, "Student");
            await _signInManager.SignInAsync(newUser, false);

            var token = await GetToken(newUser);

            return token;
        }


        public async Task<string> SignIn(SignInViewModel model)
        {
            await _authValidationService.ValidateSignInViewModel(model);

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (!result.Succeeded)
            {
                throw new SignInFailedException(
                $"An error occured while signing in user: {model.UserName}");
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            var token = await GetToken(user);

            return token;
        }

        public async Task<string> SocialSignIn(SocialSignInViewModel model)
        {
            await _authValidationService.ValidateSocialSignInViewModel(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                user = new OmdleUser
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    throw new SocialSignInFailedException(
                        $"An error occured while signing in Facebook user: {result.Errors.Select(e => e.Description).Join(", ")}");
                }

                await _userManager.AddToRoleAsync(user, "Student");
            }

            await _signInManager.SignInAsync(user, false);

            var token = await GetToken(user);

            return token;
        }

        private async Task<string> GetToken(OmdleUser user)
        {
            var utcNow = DateTime.UtcNow;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName, $"{user.FirstName} {user.LastName }"),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString(CultureInfo.InvariantCulture))
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim("role", role));
            }

            var signingKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Tokens:Key")));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(_configuration.GetValue<int>("Tokens:Lifetime"))
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}