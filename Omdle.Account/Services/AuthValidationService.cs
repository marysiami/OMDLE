using System;
using Omdle.Account.Contracts;
using Omdle.Account.Models;
using Omdle.Common.Exceptions;
using System.Threading.Tasks;

namespace Omdle.Account.Services
{
    /// <summary>Class AuthValidationService.
    /// Implements the <see cref="Omdle.Account.Contracts.IAuthValidationService"/></summary>
    public class AuthValidationService : IAuthValidationService
    {
        /// <summary>Validates the register view model.</summary>
        /// <param name="model">The model.</param>
        /// <returns>Task.</returns>
        /// <exception cref="RegistrationFailedException">Email cannot be null!
        /// or
        /// Password cannot be null!
        /// or
        /// UserName cannot be null!
        /// or
        /// First name cannot be null!
        /// or
        /// Last name cannot be null!</exception>
        public Task ValidateRegisterViewModel(RegisterViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                throw new RegistrationFailedException(
                    $"Email cannot be null!");
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                throw new RegistrationFailedException(
                    $"Password cannot be null!");
            }

            if (string.IsNullOrEmpty(model.UserName))
            {
                throw new RegistrationFailedException(
                    $"UserName cannot be null!");
            }
            if (string.IsNullOrEmpty(model.FirstName))
            {
                throw new RegistrationFailedException(
                    $"First name cannot be null!");
            }
            if (string.IsNullOrEmpty(model.LastName))
            {
                throw new RegistrationFailedException(
                    $"Last name cannot be null!");
            }
            return Task.CompletedTask;
        }

        /// <summary>Validates the sign in view model.</summary>
        /// <param name="model">The model.</param>
        /// <returns>Task.</returns>
        /// <exception cref="SignInFailedException">Password cannot be null!
        /// or
        /// UserName cannot be null!</exception>
        public Task ValidateSignInViewModel(SignInViewModel model)
        {
            if (string.IsNullOrEmpty(model.Password))
            {
                throw new SignInFailedException(
                    $"Password cannot be null!");
            }

            if (string.IsNullOrEmpty(model.UserName))
            {
                throw new SignInFailedException(
                    $"UserName cannot be null!");
            }
            return Task.CompletedTask;
        }

        /// <summary>Validates the social sign in view model.</summary>
        /// <param name="model">The model.</param>
        /// <returns>Task.</returns>
        /// <exception cref="SocialSignInFailedException">Email cannot be null!
        /// or
        /// UserName cannot be null!
        /// or
        /// First name cannot be null!
        /// or
        /// Last name cannot be null!</exception>
        public Task ValidateSocialSignInViewModel(SocialSignInViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                throw new SocialSignInFailedException(
                    $"Email cannot be null!");
            }

            if (string.IsNullOrEmpty(model.UserName))
            {
                throw new SocialSignInFailedException(
                    $"UserName cannot be null!");
            }
            if (string.IsNullOrEmpty(model.FirstName))
            {
                throw new SocialSignInFailedException(
                    $"First name cannot be null!");
            }
            if (string.IsNullOrEmpty(model.LastName))
            {
                throw new SocialSignInFailedException(
                    $"Last name cannot be null!");
            }
            return Task.CompletedTask;
        }
    }
}
