using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using ResturantBooking.Models;
using ResturantBooking.Repositories.IRepositories;
using ResturantBooking.Services.IServices;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ResturantBooking.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepo;
        private readonly IConfiguration _config;

        public AdminService(IAdminRepository adminRepo, IConfiguration config)
        {
            _adminRepo = adminRepo;
            _config = config;
        }
        public async Task<int> CreateAdminAsync(string username, string password)
        {
            var hasher = new PasswordHasher<Admin>();
            var admin = new Admin { Username = username };
            {
                admin.PasswordHash = hasher.HashPassword(admin, password);
            };
            
            return await _adminRepo.CreateAdminAsync(admin);
        }

        public async Task<string?> LoginAsync(string username, string password)
        {
            var admin = await _adminRepo.GetAdminByUsernameAsync(username);
            if (admin == null) { return null; }

            var hasher = new PasswordHasher<Admin>();
            var result = hasher.VerifyHashedPassword(admin, admin.PasswordHash, password);

            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            //Skapar JWT 
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
