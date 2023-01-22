using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IdentityService.Application.Dtos;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Application.Interfaces.Service;
using IdentityService.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Application.Service;

 public class TokenHelper : ITokenHelper
   {
       private readonly IRoleRepository _roleRepository;
        public IConfiguration Configuration { get; }

        private TokenOption _tokenOption;

        private DateTime _accessTokenExpiration;

        public TokenHelper(IConfiguration configuration, IRoleRepository roleRepository)
        {
            Configuration = configuration;
            _roleRepository = roleRepository;
            _tokenOption = Configuration.GetSection("TokenOptions").Get<TokenOption>();
        }

        public AccessToken CreateToken(User user)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.Expiration);

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken jwt = CreateJwtSecurityToken(_tokenOption, user, signingCredentials);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            string token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken { Token = token, Expiration = _accessTokenExpiration };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOption tokenOptions, User user, SigningCredentials signingCredentials)
        {
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now, 
                claims: SetClaims(user).Result,
                signingCredentials: signingCredentials
                );
            return jwt;
        }

        private async  Task<IEnumerable<Claim>> SetClaims(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new(ClaimTypes.Name, user.FirstName),
                new(ClaimTypes.Role, (await _roleRepository.GetById(user.RoleId)).Name)
            };
            return claims;
        }
    }