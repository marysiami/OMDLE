using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Omdle.Account.Contracts;
using Omdle.Account.Models;
using Omdle.Account.Services;
using Omdle.Common.Exceptions;
using Omdle.Data.Contracts;
using Omdle.Data.Models.Account;
using System;
using System.Threading.Tasks;

namespace Omdle.Tests.Omdle.Account.Tests
{
    public class AuthServiceTests
    {
        [Test]
        public void Register_CreateAsyncFailed_ReturnsRegistrationFailedException()
        {
            var userManager = new FakeUserManager();
            var signInManager = new FakeSignInManager();
            var authValidationService = new Mock<IAuthValidationService>().Object;
            var configuration = new Mock<IConfiguration>().Object;
            var dataService = new Mock<IDataService>().Object;

            var authService = new AuthService(userManager, signInManager, authValidationService,
                configuration, dataService);

            var model = new RegisterViewModel
            {
                UserName = "userName",
                Password = "password",
                Email = "email",
                FirstName = "firstName",
                LastName = "lastName"
            };

            //Act
            Func<Task> result = async () =>
            {
                await authService.Register(model);
            };

            result.Should().Throw<RegistrationFailedException>();
        }

        [Test]
        public void SignIn_CreateAsyncFailed_ReturnsSignInFailedException()
        {
            var userManager = new FakeUserManager();
            var signInManager = new FakeSignInManager();
            var authValidationService = new Mock<IAuthValidationService>().Object;
            var configuration = new Mock<IConfiguration>().Object;
            var dataService = new Mock<IDataService>().Object;

            var authService = new AuthService(userManager, signInManager, authValidationService,
                configuration, dataService);

            var model = new SignInViewModel
            {
                UserName = "userName",
                Password = "password"
            };

            //Act
            Func<Task> result = async () =>
            {
                await authService.SignIn(model);
            };

            result.Should().Throw<SignInFailedException>();
        }

        [Test]
        public void SocialSignIn_CreateAsyncFailed_SocialSignInFailedException()
        {
            var userManager = new FakeUserManager();
            var signInManager = new FakeSignInManager();
            var authValidationService = new Mock<IAuthValidationService>().Object;
            var configuration = new Mock<IConfiguration>().Object;
            var dataService = new Mock<IDataService>().Object;

            var authService = new AuthService(userManager, signInManager, authValidationService,
                configuration, dataService);

            var model = new SocialSignInViewModel
            {
                UserName = "userName",
                Email = "email",
                FirstName = "firstName",
                LastName = "lastName"
            };

            //Act
            Func<Task> result = async () =>
            {
                await authService.SocialSignIn(model);
            };

            result.Should().Throw<SocialSignInFailedException>();
        }
    }

    public class FakeSignInManager : SignInManager<OmdleUser>
    {
        public FakeSignInManager()
            : base(new Mock<FakeUserManager>().Object,
                new Mock<IHttpContextAccessor>().Object,
                new Mock<IUserClaimsPrincipalFactory<OmdleUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<OmdleUser>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object)
        {
        }

        public override Task SignInAsync(OmdleUser user, bool isPersistent, string authenticationMethod = null)
        {
            return Task.FromResult(SignInResult.Success);
        }
    }

    public class FakeUserManager : UserManager<OmdleUser>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<OmdleUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<OmdleUser>>().Object,
                new IUserValidator<OmdleUser>[0],
                new IPasswordValidator<OmdleUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<OmdleUser>>>().Object)
        {
        }
        public override Task<IdentityResult> CreateAsync(OmdleUser user)
        {
            return Task.FromResult(IdentityResult.Failed());
        }

        public override Task<IdentityResult> CreateAsync(OmdleUser user, string password)
        {
            return Task.FromResult(IdentityResult.Failed());
        }

        public override Task<IdentityResult> AddToRoleAsync(OmdleUser user, string role)
        {
            return Task.FromResult(IdentityResult.Success);
        }

        public override Task<OmdleUser> FindByEmailAsync(string email)
        {
            //return Task.FromResult(new OmdleUser {Email=email});
            return Task.FromResult<OmdleUser>(null);
        }
    }
}
