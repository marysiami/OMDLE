using System;
using FluentAssertions;
using NUnit.Framework;
using Omdle.Account.Models;
using Omdle.Account.Services;
using Omdle.Common.Exceptions;

namespace Omdle.Tests.Omdle.Account.Tests
{
    public class AuthValidationServiceTests
    {
        [TestCase("userName", "", "", "", "")]
        [TestCase("", "email", "", "", "")]
        [TestCase("", "", "password", "", "")]
        [TestCase("", "", "", "firstName", "")]
        [TestCase("", "", "", "", "lastName")]
        public void ValidateRegisterViewModel_InvalidCredentials_ThrowsRegisterFailedException(string userName,
            string email, string password, string firstName, string lastName)
        {
            var authValidationService = new AuthValidationService();

            var model = new RegisterViewModel
            {
                UserName = userName,
                Password = password,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            Action result = () => authValidationService.ValidateRegisterViewModel(model);
            result.Should().Throw<RegistrationFailedException>();
        }

        [TestCase("", "password")]
        [TestCase("userName", "")]
        public void ValidateSignInViewModel_InvalidCredentials_ThrowsSignInFailedException(string userName, string password)
        {
            var authValidationService = new AuthValidationService();

            var model = new SignInViewModel
            {
                Password = password,
                UserName = userName
            };

            Action result = () => authValidationService.ValidateSignInViewModel(model);
            result.Should().Throw<SignInFailedException>();
        }

        [TestCase("userName", "", "", "")]
        [TestCase("", "email", "", "")]
        [TestCase("", "", "firstName", "")]
        [TestCase("", "", "", "lastName")]
        public void ValidateSocialSignInViewModel_InvalidCredentials_ThrowsSocialSignInFailedException(string userName, string email, string firstName, string lastName)
        {
            var authValidationService = new AuthValidationService();

            var model = new SocialSignInViewModel
            {
                UserName = userName,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            Action result = () => authValidationService.ValidateSocialSignInViewModel(model);
            result.Should().Throw<SocialSignInFailedException>();
        }

    }
}