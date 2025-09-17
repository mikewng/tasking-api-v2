﻿
using tasking_api.Infrastructure.Utils;
using tasking_api.Main.Data.Contracts;
using tasking_api.Main.Models;
using tasking_api.Main.Models.DTO.Request;
using tasking_api.Main.Service.Contracts;

namespace tasking_api.Main.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                return Result.Fail("Email and password are required");
            }

            var user = await _unitOfWork.Users.GetByEmailAsync(loginRequest.Email, cancellationToken);
            if (user == null)
            {
                return Result.Fail("Invalid email or password");
            }

            if (!Cryptography.VerifyPassword(loginRequest.Password, user.PasswordHash))
            {
                return Result.Fail("Invalid email or password");
            }

            user.LastLoginAt = DateTime.UtcNow;
            var updateSuccess = await _unitOfWork.Users.UpdateAsync(user, cancellationToken);
            if (!updateSuccess)
            {
                return Result.Fail("Failed to update user login time");
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }

        public async Task<Result> RegisterAsync(RegisterRequest registerRequest, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(registerRequest.Username) || 
                string.IsNullOrWhiteSpace(registerRequest.Email) || 
                string.IsNullOrWhiteSpace(registerRequest.Password))
            {
                return Result.Fail("Username, email, and password are required");
            }

            if (registerRequest.Password.Length < 6)
            {
                return Result.Fail("Password must be at least 6 characters long");
            }

            if (await _unitOfWork.Users.EmailExistsAsync(registerRequest.Email, cancellationToken))
            {
                return Result.Fail("Email already exists");
            }

            if (await _unitOfWork.Users.UsernameExistsAsync(registerRequest.Username, cancellationToken))
            {
                return Result.Fail("Username already exists");
            }

            var passwordHash = Cryptography.HashPassword(registerRequest.Password);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = registerRequest.Username,
                Email = registerRequest.Email,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow
            };

            await _unitOfWork.Users.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }

        
    }
}
