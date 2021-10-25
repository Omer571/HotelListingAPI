using HotelListingAPI.Data;
using HotelListingAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPI.Services
{
    public class AuthManager : IAuthManager
    {
        // Dependency Inject user manager (DI inject instance of object)
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private ApiUser _user;

        public AuthManager(
            UserManager<ApiUser> userManager, 
            IConfiguration configuration
        )
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> CreateToken()
        {
            // since these are var, the function return types should
            // be well type to infer these types
            var signingCredentials = GetSigningCredentials();

            // Your driver's license has your date of birth on it. In this
            // case the claim name would be DateOfBirth, the claim value
            // would be your date of birth

            //For example if you want access to a night club the authorization
            //process might be:

            // 
            // The door security officer would evaluate the value of
            // your date of birth claim and whether they trust the
            // issuer(the driving license authority) before granting
            // you access.
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("Key");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var expirationTime = DateTime.Now.AddMinutes(Convert.ToDouble(
                    jwtSettings.GetSection("lifetime").Value));
            
            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("Issuer").Value,
                claims: claims,
                expires: expirationTime,
                signingCredentials: signingCredentials
                );

            return token;
        }

        public async Task<bool> ValidateUser(LoginUserDTO userDTO)
        {
            // Does user exist in the system and is password correct
            _user = await _userManager.FindByNameAsync(userDTO.Email);
            return _user != null && await _userManager.CheckPasswordAsync(_user, userDTO.Password);
        }

    }

}
